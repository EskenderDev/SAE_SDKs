using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace SAE.Identity.Client;

public static class DeviceIdentityService
{
    private static string? _cachedFingerprint;

    public static string GetFingerprint()
    {
        if (_cachedFingerprint != null) return _cachedFingerprint;

        var sb = new StringBuilder();
        sb.Append(Environment.MachineName);
        sb.Append(Environment.ProcessorCount);
        sb.Append(RuntimeInformation.OSDescription);
        sb.Append(RuntimeInformation.OSArchitecture);
        
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
        _cachedFingerprint = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        
        return _cachedFingerprint;
    }
}
