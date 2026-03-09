using System;
using System.Collections.Generic;

namespace SAE.Sdk.Models;

/// <summary>
/// Solicitud para activar una licencia en una máquina específica.
/// </summary>
public class ActivateLicenseRequest
{
    public string LicenseKey { get; set; } = string.Empty;
    public string MachineId { get; set; } = string.Empty;
    public string? DeviceName { get; set; }
    public string? IpAddress { get; set; }
    public string? Platform { get; set; }
    public string? AppIdentifier { get; set; }
}

/// <summary>
/// Solicitud para validar una licencia (Check-in).
/// </summary>
public class ValidateLicenseRequest
{
    public string LicenseKey { get; set; } = string.Empty;
    public string MachineId { get; set; } = string.Empty;
    public string? AppVersion { get; set; }
    public string? IpAddress { get; set; }
    public string? Platform { get; set; }
    public string? AppIdentifier { get; set; }
}

/// <summary>
/// Respuesta detallada de una licencia.
/// </summary>
public class LicenseResponse
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string LicenseKey { get; set; } = string.Empty;
    public string? MachineId { get; set; }
    public LicenseType Type { get; set; }
    public LicensePlatform Platform { get; set; }
    public LicenseStatus Status { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public DateTime? LastCheckIn { get; set; }
    public string? AppVersion { get; set; }
    public string? LastIpAddress { get; set; }
    public string? DeviceName { get; set; }
    public string? AppIdentifier { get; set; }
    public string? AppName { get; set; }
    public string? OfflineToken { get; set; }
    public Guid? TerminalId { get; set; }
    public string? TerminalName { get; set; }
    public string? TerminalCode { get; set; }
}

/// <summary>
/// Log de auditoría de licencias.
/// </summary>
public class LicenseAuditLogDto
{
    public Guid Id { get; set; }
    public Guid LicenseId { get; set; }
    public LicenseAction Action { get; set; }
    public string? MachineId { get; set; }
    public string? Details { get; set; }
    public bool IsSuccess { get; set; }
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// Límite de licencias por inquilino.
/// </summary>
public class TenantLimitDto
{
    public Guid TenantId { get; set; }
    public LicenseType LicenseType { get; set; }
    public int MaxAllowed { get; set; }
    public int CurrentUsage { get; set; }
}

/// <summary>
/// Respuesta genérica de validación rápida.
/// </summary>
public class LicenseValidationResult
{
    public string Status { get; set; } = string.Empty;
    public Guid? LicenseId { get; set; }
    public string? Type { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? OfflineToken { get; set; }
    public List<string>? Features { get; set; }
    public List<LicenseModuleInfo>? Modules { get; set; }
    public string? Message { get; set; }
    public Guid? TerminalId { get; set; }
    public string? TerminalName { get; set; }
    public string? TerminalCode { get; set; }
}

/// <summary>
/// Información detallada de un módulo de licencia.
/// </summary>
public class LicenseModuleInfo
{
    public Guid ModuleId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public bool IsActive { get; set; }
    public DateTime GrantedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public int? MaxUsageCount { get; set; }
    public int CurrentUsageCount { get; set; }
    public bool IsExpired { get; set; }
    public bool IsUsageLimitReached { get; set; }
    public List<string>? RequiresModules { get; set; }
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Identificador SAE disponible para desarrolladores.
/// </summary>
public class SaeIdentifierResponse
{
    public string AppId { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// Solicitud para registrar una nueva aplicación de desarrollador.
/// </summary>
public class RegisterAppRequest
{
    public string AppName { get; set; } = string.Empty;
    public string AppSlug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? WebsiteUrl { get; set; }
    public LicenseType RequiredType { get; set; }
    public LicensePlatform RequiredPlatform { get; set; }
}

/// <summary>
/// Respuesta de una aplicación registrada.
/// </summary>
public class RegisteredApplicationResponse
{
    public Guid Id { get; set; }
    public string AppId { get; set; } = string.Empty;
    public string AppName { get; set; } = string.Empty;
    public string DeveloperSlug { get; set; } = string.Empty;
    public string AppSlug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? WebsiteUrl { get; set; }
    public string RequiredType { get; set; } = string.Empty;
    public string RequiredPlatform { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Configuración global de la plataforma (Solo Admin).
/// </summary>
public class AdminConfigResponse
{
    public string Environment { get; set; } = string.Empty;
    public string? HaciendaUrl { get; set; }
    public bool HaciendaEnabled { get; set; }
    public string? FrontendUrl { get; set; }
    public string? SmtpHost { get; set; }
    public string? DatabaseProvider { get; set; }
    public bool AzureServiceBusConfigured { get; set; }
    public bool AzureRedisConfigured { get; set; }
    public bool AzureBlobConfigured { get; set; }
    public string? AzureKeyVaultUrl { get; set; }
    public bool AppInsightsEnabled { get; set; }
    public string? JwtExpiration { get; set; }
    public DateTime ServerTime { get; set; }
    public string DotNetVersion { get; set; } = string.Empty;
    public string OS { get; set; } = string.Empty;
    public int ProcessorCount { get; set; }
}
