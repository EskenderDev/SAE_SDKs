using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace SAE.Sdk.Client;

/// <summary>
/// Opciones de configuración avanzadas para el cliente SAE.
/// </summary>
public class SaeClientOptions
{
    /// <summary>
    /// Timeout para las peticiones HTTP. Por defecto: 100 segundos.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

    /// <summary>
    /// Número máximo de reintentos en caso de fallos temporales (5xx, timeout, etc.).
    /// Por defecto: 3 reintentos.
    /// </summary>
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// Delay inicial entre reintentos (backoff exponencial).
    /// Por defecto: 1 segundo.
    /// </summary>
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Factor de multiplicación para el backoff exponencial.
    /// Por defecto: 2 (1s, 2s, 4s, etc.)
    /// </summary>
    public double BackoffMultiplier { get; set; } = 2.0;

    /// <summary>
    /// Tiempo máximo de espera total para todos los reintentos.
    /// Por defecto: 30 segundos.
    /// </summary>
    public TimeSpan MaxRetryDuration { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Indica si se debe ignorar la validación de certificados SSL.
    /// ⚠️ SOLO USAR EN DESARROLLO con certificados autofirmados.
    /// Por defecto: false.
    /// </summary>
    public bool SkipCertificateValidation { get; set; } = false;

    /// <summary>
    /// Callback personalizado para validar certificados SSL.
    /// Permite validación personalizada de certificados (thumbprints, CAs específicos, etc.)
    /// </summary>
    public Func<X509Certificate2, X509Chain, SslPolicyErrors, bool>? CustomCertificateValidation { get; set; }

    /// <summary>
    /// Certificado de cliente para autenticación mTLS (mutual TLS).
    /// Opcional: se usa cuando el servidor requiere autenticación por certificado.
    /// </summary>
    public X509Certificate2? ClientCertificate { get; set; }

    /// <summary>
    /// Colección de certificos de CA raíz de confianza personalizados.
    /// Útil para validar certificados emitidos por CAs internas/empresariales.
    /// </summary>
    public X509Certificate2Collection? CustomRootCertificates { get; set; }

    /// <summary>
    /// Indica si se debe habilitar el Keep-Alive para conexiones persistentes.
    /// Por defecto: true.
    /// </summary>
    public bool EnableKeepAlive { get; set; } = true;

    /// <summary>
    /// Tiempo de vida máximo de una conexión persistente.
    /// Por defecto: 2 minutos.
    /// </summary>
    public TimeSpan PooledConnectionLifetime { get; set; } = TimeSpan.FromMinutes(2);

    /// <summary>
    /// Número máximo de conexiones por servidor.
    /// Por defecto: int.MaxValue (sin límite práctico).
    /// </summary>
    public int MaxConnectionsPerServer { get; set; } = int.MaxValue;

    /// <summary>
    /// Indica si se debe seguir redirecciones automáticamente.
    /// Por defecto: true.
    /// </summary>
    public bool AllowAutoRedirect { get; set; } = true;

    /// <summary>
    /// Número máximo de redirecciones permitidas.
    /// Por defecto: 50.
    /// </summary>
    public int MaxAutomaticRedirections { get; set; } = 50;

    /// <summary>
    /// Versión de TLS mínima permitida.
    /// Por defecto: TLS 1.2
    /// </summary>
    public SslProtocols MinSslProtocol { get; set; } = SslProtocols.Tls12;

    /// <summary>
    /// Callback para logging de eventos de red (debug, errores, reintentos).
    /// </summary>
    public Action<SaeClientLogEvent>? LogCallback { get; set; }

    /// <summary>
    /// Crea opciones predeterminadas seguras para producción.
    /// </summary>
    public static SaeClientOptions ProductionDefaults() => new()
    {
        Timeout = TimeSpan.FromSeconds(100),
        MaxRetries = 3,
        SkipCertificateValidation = false,
        MinSslProtocol = SslProtocols.Tls12
    };

    /// <summary>
    /// Crea opciones para desarrollo local con certificados autofirmados.
    /// ⚠️ No usar en producción.
    /// </summary>
    public static SaeClientOptions DevelopmentDefaults() => new()
    {
        Timeout = TimeSpan.FromSeconds(30),
        MaxRetries = 1,
        SkipCertificateValidation = true,
        MinSslProtocol = SslProtocols.Tls12
    };
}

/// <summary>
/// Evento de log para el cliente SAE.
/// </summary>
public class SaeClientLogEvent
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public SaeLogLevel Level { get; set; }
    public string Message { get; set; } = "";
    public string? Operation { get; set; }
    public int? RetryAttempt { get; set; }
    public Exception? Exception { get; set; }
    public TimeSpan? Duration { get; set; }
}

public enum SaeLogLevel
{
    Debug,
    Info,
    Warning,
    Error
}

/// <summary>
/// Excepción específica para errores del cliente SAE.
/// </summary>
public class SaeClientException : Exception
{
    /// <summary>
    /// Número de intentos realizados antes de fallar.
    /// </summary>
    public int RetryCount { get; }

    public SaeClientException(string message) : base(message)
    {
        RetryCount = 0;
    }

    public SaeClientException(string message, Exception? innerException) : base(message, innerException)
    {
        RetryCount = 0;
    }

    public SaeClientException(string message, Exception? innerException, int retryCount) : base(message, innerException)
    {
        RetryCount = retryCount;
    }
}
