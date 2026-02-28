namespace SAE.Sdk.Models;

/// <summary>
/// Evento disparado cuando se inicia un envío a Hacienda.
/// </summary>
public class HaciendaEnvioIniciadoEvent
{
    public Guid Id { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string TipoDocumento { get; set; } = string.Empty;
    public DateTime FechaEmision { get; set; }
}

/// <summary>
/// Evento disparado cuando se recibe una respuesta de Hacienda.
/// </summary>
public class HaciendaRespuestaRecibidaEvent
{
    public Guid Id { get; set; }
    public string Clave { get; set; } = string.Empty;
    public HaciendaEstado Estado { get; set; }
    public int? HttpStatus { get; set; }
    public string? MensajeEstado { get; set; }
    public string? ResponseJson { get; set; }
}
