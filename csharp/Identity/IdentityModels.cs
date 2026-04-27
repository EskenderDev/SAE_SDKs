using System;

namespace SAE.Identity.Client;

public class IdentityOptions
{
    public string Authority { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string[] Scopes { get; set; } = Array.Empty<string>();
}

public class TokenResponse
{
    public string access_token { get; set; } = default!;
    public int expires_in { get; set; }
    public string token_type { get; set; } = default!;
}
