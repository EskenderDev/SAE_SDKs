using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SAE.Sdk.Core;

namespace SAE.Sdk.Client;

public class SaeRuntime
{
    private readonly HttpClient _http;
    private readonly SaePipeline _pipeline;
    private readonly InternalEventBus _bus;

    public SaeRuntime(
        HttpClient http,
        SaePipeline pipeline,
        InternalEventBus bus)
    {
        _http = http;
        _pipeline = pipeline;
        _bus = bus;
    }

    public InternalEventBus Events => _bus;

    public async Task<T> SendAsync<T>(
        HttpRequestMessage request,
        CancellationToken ct = default)
    {
        var context = new SaeContext
        {
            Request = request,
            CancellationToken = ct
        };

        await _pipeline.ExecuteAsync(context, async () =>
        {
            context.Response = await _http.SendAsync(request, ct);
        });

        if (context.Response == null)
        {
            throw new InvalidOperationException("Pipeline finished without a response.");
        }

        context.Response.EnsureSuccessStatusCode();

        var json = await context.Response.Content.ReadAsStringAsync(ct);

        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    // Helpers tipo SDK (Ejemplo)
    public Task<dynamic> GetProfileAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "tenants/current");
        return SendAsync<dynamic>(request);
    }

    /// <summary>
    /// 🔥 Ejecuta un request crudo a través del pipeline (usado por Outbox Worker)
    /// </summary>
    public async Task<HttpResponseMessage> SendRawAsync(HttpRequestMessage request)
    {
        var context = new SaeContext
        {
            Request = request,
            CancellationToken = CancellationToken.None
        };

        await _pipeline.ExecuteAsync(context, async () =>
        {
            context.Response = await _http.SendAsync(request);
        });

        return context.Response ?? new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable);
    }
}
