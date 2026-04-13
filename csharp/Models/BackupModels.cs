using System;
using System.Collections.Generic;

namespace SAE.Sdk.Models;

public class BackupHistoryDto
{
    public Guid Id { get; set; }
    public DateTime Fecha { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public long Tamaño { get; set; }
    public string DestinosExitosos { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public bool EsAuto { get; set; }
    public string? Checksum { get; set; }
}

public class CloudProviderConfigDto
{
    public string Provider { get; set; } = string.Empty; 
    public bool Enabled { get; set; } = true;
    public Dictionary<string, string> Credentials { get; set; } = new();
}

public class BackupConfigProgramacionDto
{
    public string Frecuencia { get; set; } = "Diario"; 
    public string? Hora { get; set; }
}

public class BackupConfigDto
{
    public string EstablecimientoId { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
    public BackupConfigProgramacionDto Programacion { get; set; } = new();
    public bool CifradoEnabled { get; set; }
    public List<CloudProviderConfigDto> CloudProviders { get; set; } = new();
}

public class BackupExecutionRespuesta
{
    public bool Success { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public string? BackupId { get; set; }
}
