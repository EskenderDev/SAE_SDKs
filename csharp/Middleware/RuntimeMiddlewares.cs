using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Diagnostics;
using Polly;
using SAE.Sdk.Core;
using SAE.Identity.Client;

namespace SAE.Sdk.Middleware;

public class AuthMiddleware : ISaeMiddleware
{
    private readonly ITokenProvider _tokenProvider;
    private readonly DPoPProofService _dpop;

    public AuthMiddleware(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
        _dpop = new DPoPProofService();
    }

    public async Task InvokeAsync(SaeContext context, Func<Task> next)
    {
        var token = await _tokenProvider.GetAccessTokenAsync(context.CancellationToken);

        context.Request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var proof = _dpop.CreateProof(
            context.Request.Method.Method,
            context.Request.RequestUri!.ToString());

        if (context.Request.Headers.Contains("DPoP"))
            context.Request.Headers.Remove("DPoP");
            
        context.Request.Headers.Add("DPoP", proof);

        await next();
    }
}

public class TelemetryMiddleware : ISaeMiddleware
{
    private static readonly ActivitySource Source = new("SAE.SDK");

    public async Task InvokeAsync(SaeContext context, Func<Task> next)
    {
        using var activity = Source.StartActivity("SAE HTTP CALL");

        activity?.SetTag("http.method", context.Request.Method);
        activity?.SetTag("http.url", context.Request.RequestUri);

        try
        {
            await next();

            if (context.Response != null)
            {
                activity?.SetTag("http.status_code", (int)context.Response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            activity?.SetTag("error", true);
            activity?.SetTag("exception", ex.Message);
            throw;
        }
    }
}

public class ResilienceMiddleware : ISaeMiddleware
{
    private readonly IAsyncPolicy _policy;

    public ResilienceMiddleware()
    {
        _policy = Policy.WrapAsync(
            Policy.Handle<Exception>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i))),

            Policy.Handle<Exception>()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30))
        );
    }

    public async Task InvokeAsync(SaeContext context, Func<Task> next)
    {
        await _policy.ExecuteAsync(async () =>
        {
            await next();
        });
    }
}
