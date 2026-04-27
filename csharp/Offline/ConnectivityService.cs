using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SAE.Sdk.Offline;

public class ConnectivityService : IConnectivityService, IDisposable
{
    private readonly HttpClient _http;
    private readonly ILogger<ConnectivityService> _logger;
    private readonly CancellationTokenSource _cts = new();
    private bool _isOnline = true;

    public bool IsOnline => _isOnline;

    public ConnectivityService(HttpClient http, ILogger<ConnectivityService> logger)
    {
        _http = http;
        _logger = logger;

        _ = MonitorAsync(_cts.Token);
    }

    private async Task MonitorAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                // Intentar ping a un endpoint de salud conocido
                // Nota: En un entorno real, esto debería ser configurable
                using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                timeoutCts.CancelAfter(TimeSpan.FromSeconds(3));

                var res = await _http.GetAsync("https://api.sae.system/health", timeoutCts.Token);
                
                var newStatus = res.IsSuccessStatusCode;
                if (newStatus != _isOnline)
                {
                    _isOnline = newStatus;
                    _logger.LogInformation("🌐 Estado de conectividad SAE cambiado a: {Status}", _isOnline ? "ONLINE" : "OFFLINE");
                }
            }
            catch
            {
                if (_isOnline)
                {
                    _isOnline = false;
                    _logger.LogWarning("🌐 Conectividad SAE perdida (OFFLINE)");
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), ct);
        }
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
