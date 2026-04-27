using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SAE.Sdk.Core;

namespace SAE.Sdk.Offline;

public class OfflineMiddleware : ISaeMiddleware
{
    private readonly ISaeOutboxRepository _outbox;
    private readonly IConnectivityService _connectivity;
    private readonly ILogger<OfflineMiddleware> _logger;

    public OfflineMiddleware(
        ISaeOutboxRepository outbox, 
        IConnectivityService connectivity,
        ILogger<OfflineMiddleware> logger)
    {
        _outbox = outbox;
        _connectivity = connectivity;
        _logger = logger;
    }

    public async Task InvokeAsync(SaeContext context, Func<Task> next)
    {
        if (!_connectivity.IsOnline)
        {
            _logger.LogWarning("🌐 Offline detectado. Guardando en outbox: {Endpoint}", context.Request.RequestUri?.PathAndQuery);
            await SaveToOutbox(context, "offline-mode");

            // Simular respuesta Accepted para que la app continúe
            context.Response = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{\"offline\":true, \"message\":\"Request queued for later synchronization\"}", System.Text.Encoding.UTF8, "application/json")
            };

            return;
        }

        try
        {
            await next();

            // Si la respuesta falló por error de red/servidor (pero hubo conexión), 
            // opcionalmente podríamos guardar en outbox para reintento manual/automático si es crítico.
            // Por ahora seguimos el flujo estándar.
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "🌐 Error de red detectado. Guardando en outbox: {Endpoint}", context.Request.RequestUri?.PathAndQuery);

            await SaveToOutbox(context, ex.Message);

            context.Response = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{\"offline\":true, \"error\":\"" + ex.Message + "\"}", System.Text.Encoding.UTF8, "application/json")
            };
        }
    }

    private async Task SaveToOutbox(SaeContext context, string error)
    {
        var payload = "";
        if (context.Request.Content != null)
        {
            payload = await context.Request.Content.ReadAsStringAsync();
        }

        var message = new SaeOutboxMessage
        {
            Endpoint = context.Request.RequestUri!.PathAndQuery,
            Method = context.Request.Method.Method,
            PayloadJson = payload,
            LastError = error,
            Priority = context.Items.TryGetValue("Priority", out var p) && p is int priority ? priority : 1
        };

        await _outbox.AddAsync(message);
    }
}

public interface IConnectivityService
{
    bool IsOnline { get; }
}
