using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SAE.Sdk.Client;

namespace SAE.Sdk.Offline;

public class SaeOutboxWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConnectivityService _connectivity;
    private readonly ILogger<SaeOutboxWorker> _logger;

    public SaeOutboxWorker(
        IServiceScopeFactory scopeFactory,
        IConnectivityService connectivity,
        ILogger<SaeOutboxWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _connectivity = connectivity;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🚀 SaeOutboxWorker iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_connectivity.IsOnline)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                continue;
            }

            try
            {
                List<SaeOutboxMessage> batch;
                using (var scope = _scopeFactory.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetRequiredService<ISaeOutboxRepository>();
                    batch = await repo.GetBatchAsync(20);
                }

                if (!batch.Any())
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    continue;
                }

                _logger.LogInformation("🔁 Procesando batch de {Count} mensajes del outbox", batch.Count);

                var tasks = batch.Select(msg => ProcessMessageAsync(msg, stoppingToken));
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "💥 Error en el bucle principal de SaeOutboxWorker");
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }

    private async Task ProcessMessageAsync(SaeOutboxMessage msg, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<ISaeOutboxRepository>();
        var runtime = scope.ServiceProvider.GetRequiredService<SaeRuntime>();

        try
        {
            await repo.MarkProcessingAsync(msg.Id);

            _logger.LogDebug("🔁 Reintentando mensaje {Id}: {Method} {Endpoint}", msg.Id, msg.Method, msg.Endpoint);

            var request = new HttpRequestMessage(new HttpMethod(msg.Method), msg.Endpoint)
            {
                Content = new StringContent(msg.PayloadJson, System.Text.Encoding.UTF8, "application/json")
            };

            // 🔥 IDEMPOTENCY HEADER
            request.Headers.Add("X-Idempotency-Key", msg.IdempotencyKey);

            var response = await runtime.SendRawAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await repo.MarkAsSentAsync(msg.Id);
                _logger.LogInformation("✅ Mensaje {Id} sincronizado exitosamente.", msg.Id);
            }
            else
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception($"HTTP {(int)response.StatusCode}: {errorBody}");
            }
        }
        catch (Exception ex)
        {
            msg.RetryCount++;

            if (msg.RetryCount > 10)
            {
                _logger.LogError("💀 Mensaje {Id} alcanzó límite de reintentos. Marcando como DEAD. Error: {Error}", msg.Id, ex.Message);
                await repo.MarkAsDeadAsync(msg.Id, ex.Message);
            }
            else
            {
                msg.NextAttemptAt = BackoffStrategy.CalculateNext(msg.RetryCount);
                msg.LastError = ex.Message;
                await repo.UpdateRetryAsync(msg);
                _logger.LogWarning("⚠️ Fallo replay {Id} (intento {Retry}): {Error}", msg.Id, msg.RetryCount, ex.Message);
            }
        }
    }
}
