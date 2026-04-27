using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using SAE.Sdk.Core;
using SAE.Sdk.Client;
using SAE.Sdk.Middleware;
using SAE.Identity.Client;

namespace SAE.Sdk.Builders;

public class SaeRuntimeBuilder
{
    private readonly ServiceCollection _services = new();
    private string _baseUrl = "https://localhost:5001";
    private bool _enableDPoP = true;
    private bool _enableResilience = true;
    private bool _enableTelemetry = true;

    public SaeRuntimeBuilder WithBaseUrl(string baseUrl)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        return this;
    }

    public SaeRuntimeBuilder WithIdentity(ITokenProvider tokenProvider)
    {
        _services.AddSingleton(tokenProvider);
        return this;
    }

    public SaeRuntimeBuilder DisableDPoP()
    {
        _enableDPoP = false;
        return this;
    }

    public SaeRuntime Build()
    {
        _services.AddSingleton<InternalEventBus>();
        _services.AddSingleton<ControlPlaneAgent>();
        
        _services.AddTransient<AuthMiddleware>();
        _services.AddTransient<TelemetryMiddleware>();
        _services.AddTransient<ResilienceMiddleware>();

        _services.AddHttpClient<SaeRuntime>(client =>
        {
            client.BaseAddress = new Uri(_baseUrl.EndsWith("/api") ? _baseUrl + "/" : _baseUrl + "/api/");
        });

        _services.AddSingleton<SaePipeline>(sp =>
        {
            var pipeline = new SaePipeline();

            if (_enableTelemetry)
                pipeline.Use(async (ctx, next) => await sp.GetRequiredService<TelemetryMiddleware>().InvokeAsync(ctx, next));

            if (_enableResilience)
                pipeline.Use(async (ctx, next) => await sp.GetRequiredService<ResilienceMiddleware>().InvokeAsync(ctx, next));

            if (_enableDPoP)
                pipeline.Use(async (ctx, next) => await sp.GetRequiredService<AuthMiddleware>().InvokeAsync(ctx, next));

            return pipeline;
        });

        var provider = _services.BuildServiceProvider();
        return provider.GetRequiredService<SaeRuntime>();
    }
}
