namespace SAE.Sdk.Models;

/// <summary>
/// Wrapper unificado para respuestas de Hacienda.
/// </summary>
public class HaciendaResponse
{
    public int StatusCode { get; init; }
    public string Body { get; init; } = string.Empty;
    public string? Clave { get; init; }

    public bool EsExitoso => StatusCode == 200 || StatusCode == 202;
    public bool EsPendiente => StatusCode == 202;
    public bool EsNoAutorizado => StatusCode == 401;
    public bool EsAceptado => Body.Contains("aceptado", StringComparison.OrdinalIgnoreCase);
    public bool EsRechazado => Body.Contains("rechazado", StringComparison.OrdinalIgnoreCase);
    public bool EsProcesado => EsAceptado || EsRechazado;
    public bool EsError => StatusCode >= 400;

    public static HaciendaResponse From(int statusCode, string body, string? clave = null)
        => new() { StatusCode = statusCode, Body = body, Clave = clave };

    public static HaciendaResponse Success(string body, string? clave = null)
        => new() { StatusCode = 200, Body = body, Clave = clave };

    public static HaciendaResponse Pending(string body, string? clave = null)
        => new() { StatusCode = 202, Body = body, Clave = clave };

    public static HaciendaResponse Error(int statusCode, string body)
        => new() { StatusCode = statusCode, Body = body };

    public static HaciendaResponse Unauthorized(string body)
        => new() { StatusCode = 401, Body = body };

    public override string ToString() => $"[{StatusCode}] {(Body.Length > 100 ? Body.Substring(0, 100) + "..." : Body)}";
}

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

/// <summary>
/// Evento disparado cuando se inicia un mensaje receptor (aceptación/endoso).
/// </summary>
public class HaciendaMensajeIniciadoEvent
{
    public Guid Id { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string TipoDocumento { get; set; } = string.Empty;
    public string ClaveOriginal { get; set; } = string.Empty;
    public int Mensaje { get; set; }
}

/// <summary>
/// Evento disparado cuando se recibe la respuesta de un mensaje receptor.
/// </summary>
public class HaciendaMensajeRespuestaEvent
{
    public Guid Id { get; set; }
    public string Clave { get; set; } = string.Empty;
    public string ClaveOriginal { get; set; } = string.Empty;
    public HaciendaEstado Estado { get; set; }
    public int? HttpStatus { get; set; }
    public string? MensajeEstado { get; set; }
    public string? ResponseJson { get; set; }
}
