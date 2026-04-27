using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace SAE.Identity.Client;

public class DPoPProofService
{
    private readonly ECDsa _key;
    private readonly string _jwk;

    public DPoPProofService()
    {
        _key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        
        var parameters = _key.ExportParameters(false);
        var jwkDict = new Dictionary<string, string>
        {
            ["kty"] = "EC",
            ["crv"] = "P-256",
            ["x"] = Base64UrlEncoder.Encode(parameters.Q.X),
            ["y"] = Base64UrlEncoder.Encode(parameters.Q.Y),
            ["use"] = "sig",
            ["alg"] = "ES256"
        };
        _jwk = JsonSerializer.Serialize(jwkDict);
    }

    public string CreateProof(string method, string url)
    {
        var handler = new JsonWebTokenHandler();

        var now = DateTimeOffset.UtcNow;
        
        var payload = new Dictionary<string, object>
        {
            ["jti"] = Guid.NewGuid().ToString("N"),
            ["htm"] = method.ToUpperInvariant(),
            ["htu"] = url,
            ["iat"] = now.ToUnixTimeSeconds()
        };

        var header = new Dictionary<string, object>
        {
            ["typ"] = "dpop+jwt",
            ["alg"] = "ES256",
            ["jwk"] = JsonSerializer.Deserialize<JsonElement>(_jwk)
        };

        var securityKey = new ECDsaSecurityKey(_key);
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.EcdsaSha256);

        return handler.CreateToken(new SecurityTokenDescriptor
        {
            Claims = payload,
            SigningCredentials = signingCredentials,
            AdditionalHeaderClaims = header
        });
    }
}
