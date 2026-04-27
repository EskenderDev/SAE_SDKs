using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SAE.Sdk.Client;

public class SaeAuthenticationHandler : DelegatingHandler
{
    private readonly SAE.Identity.Client.ITokenProvider _tokenProvider;
    private readonly SAE.Identity.Client.DPoPProofService _dpopService;

    public SaeAuthenticationHandler(SAE.Identity.Client.ITokenProvider tokenProvider, HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
        _tokenProvider = tokenProvider;
        _dpopService = new SAE.Identity.Client.DPoPProofService();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenProvider.GetAccessTokenAsync(cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            // Add DPoP Proof
            var proof = _dpopService.CreateProof(request.Method.Method, request.RequestUri?.ToString() ?? "");
            request.Headers.Add("DPoP", proof);
        }

        // Add Device Context
        request.Headers.Add("X-SAE-Device-Fingerprint", SAE.Identity.Client.DeviceIdentityService.GetFingerprint());
        request.Headers.Add("X-SAE-Timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());

        return await base.SendAsync(request, cancellationToken);
    }
}
