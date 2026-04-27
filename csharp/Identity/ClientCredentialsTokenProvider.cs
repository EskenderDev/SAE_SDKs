using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Extensions.Http;

namespace SAE.Identity.Client;

public class ClientCredentialsTokenProvider : ITokenProvider
{
    private readonly HttpClient _http;
    private readonly IMemoryCache _cache;
    private readonly IdentityOptions _options;

    private const string CACHE_KEY = "sae_identity_token";

    public ClientCredentialsTokenProvider(
        HttpClient http,
        IMemoryCache cache,
        IdentityOptions options)
    {
        _http = http;
        _cache = cache;
        _options = options;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken ct = default)
    {
        if (_cache.TryGetValue<string>(CACHE_KEY, out var token))
            return token!;

        var response = await _http.PostAsync($"{_options.Authority}/connect/token", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = _options.ClientId,
            ["client_secret"] = _options.ClientSecret,
            ["scope"] = string.Join(" ", _options.Scopes)
        }), ct);

        response.EnsureSuccessStatusCode();

        var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: ct);

        // Cache the token, expiring it 1 minute before the actual expiration to avoid edge cases
        var expires = TimeSpan.FromSeconds(tokenResponse!.expires_in - 60);
        if (expires < TimeSpan.Zero) expires = TimeSpan.FromSeconds(tokenResponse.expires_in);

        _cache.Set(CACHE_KEY, tokenResponse.access_token, expires);

        return tokenResponse.access_token;
    }
}
