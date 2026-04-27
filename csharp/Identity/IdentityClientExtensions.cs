using Microsoft.Extensions.DependencyInjection;
using System;

namespace SAE.Identity.Client;

public static class IdentityClientExtensions
{
    public static IServiceCollection AddSaeIdentity(this IServiceCollection services, Action<IdentityOptions> config)
    {
        var options = new IdentityOptions();
        config(options);

        services.AddSingleton(options);
        services.AddMemoryCache();

        services.AddHttpClient<ClientCredentialsTokenProvider>();

        services.AddSingleton<ITokenProvider, ClientCredentialsTokenProvider>();

        return services;
    }
}
