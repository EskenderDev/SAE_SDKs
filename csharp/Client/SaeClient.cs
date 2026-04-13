using Microsoft.AspNetCore.SignalR.Client;
using SAE.Sdk.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SAE.Sdk.Client;

/// <summary>
/// Cliente maestro para interactuar con la API de SAE (Hacienda CR).
/// Proporciona acceso a servicios REST y notificaciones en tiempo real vía SignalR.
/// </summary>
public class SaeClient : IAsyncDisposable
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;
    public string BaseUrl => _baseUrl;
    private readonly string _haciendaHubUrl;
    private readonly string _appHubUrl;
    private string? _token;
    private string? _apiKey;
    private string? _tenantId;

    // SignalR
    private HubConnection? _haciendaConnection;
    private HubConnection? _appConnection;

    /// <summary>
    /// Evento que se dispara cuando se inicia un envío de documento.
    /// </summary>
    public event Action<HaciendaEnvioIniciadoEvent>? OnEnvioIniciado;

    /// <summary>
    /// Evento que se dispara cuando se recibe una respuesta de Hacienda.
    /// </summary>
    public event Action<HaciendaRespuestaRecibidaEvent>? OnRespuestaRecibida;

    /// <summary>
    /// Evento que se dispara cuando se inicia un mensaje receptor (aceptación/endoso).
    /// </summary>
    public event Action<HaciendaMensajeIniciadoEvent>? OnMensajeIniciado;

    /// <summary>
    /// Evento que se dispara cuando se recibe la respuesta de un mensaje receptor.
    /// </summary>
    public event Action<HaciendaMensajeRespuestaEvent>? OnMensajeRespuestaRecibida;

    /// <summary>
    /// Evento que se dispara cuando se recibe una notificación de sistema (HaciendaHub).
    /// </summary>
    public event Action<SystemNotificationDto>? OnNotificationReceived;

    /// <summary>
    /// Evento que se dispara cuando se recibe una notificación de aplicación (AppHub).
    /// </summary>
    public event Action<string, object?>? OnAppEventReceived;
 
    /// <summary>
    /// Evento que se dispara cuando la conexión de Hacienda se pierde.
    /// </summary>
    public event Action<Exception?>? OnHaciendaRealtimeClosed;
 
    [Obsolete("Use OnHaciendaRealtimeClosed")]
    public event Action<Exception?>? OnRealtimeClosed;
 
    /// <summary>
    /// Evento que se dispara cuando la conexión de App se pierde.
    /// </summary>
    public event Action<Exception?>? OnAppRealtimeClosed;

    /// <summary>
    /// Inicializa una nueva instancia del cliente SAE.
    /// </summary>
    /// <param name="baseUrl">URL base del servicio (ej: https://api.tu-servicio.com)</param>
    /// <param name="httpClient">Opcional: HttpClient personalizado</param>
    public SaeClient(string baseUrl = "https://localhost:5000", HttpClient? httpClient = null)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _haciendaHubUrl = _baseUrl + "/hubs/hacienda";
        _appHubUrl = _baseUrl + "/hubs/app";

        var restBaseUrl = _baseUrl.EndsWith("/api") ? _baseUrl + "/" : _baseUrl + "/api/";
        _http = httpClient ?? new HttpClient { BaseAddress = new Uri(restBaseUrl) };
    }

    #region Authentication

    /// <summary>
    /// Establece el token de autenticación manualmente.
    /// </summary>
    public void SetToken(string token)
    {
        _token = token;
        _apiKey = null;
        _http.DefaultRequestHeaders.Remove("X-API-KEY");
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    /// <summary>
    /// Establece el API Key para autenticación.
    /// </summary>
    public void SetApiKey(string apiKey)
    {
        _apiKey = apiKey;
        _token = null;
        _http.DefaultRequestHeaders.Authorization = null;
        if (_http.DefaultRequestHeaders.Contains("X-API-KEY"))
        {
            _http.DefaultRequestHeaders.Remove("X-API-KEY");
        }
        _http.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
    }

    /// <summary>
    /// Establece el TenantId opcional para filtrar eventos de SignalR.
    /// </summary>
    public void SetTenantId(string tenantId) => _tenantId = tenantId;

    /// <summary>
    /// Establece el Terminal Key (X-Terminal-Key) para identificar la terminal en enviarHacienda.
    /// </summary>
    public void SetTerminalKey(string terminalKey)
    {
        if (_http.DefaultRequestHeaders.Contains("X-Terminal-Key"))
            _http.DefaultRequestHeaders.Remove("X-Terminal-Key");
        _http.DefaultRequestHeaders.Add("X-Terminal-Key", terminalKey);
    }

    /// <summary>
    /// Remueve el Terminal Key header.
    /// </summary>
    public void ClearTerminalKey()
    {
        if (_http.DefaultRequestHeaders.Contains("X-Terminal-Key"))
            _http.DefaultRequestHeaders.Remove("X-Terminal-Key");
    }

    /// <summary>
    /// Inicia sesión y obtiene el token de acceso.
    /// </summary>
    public async Task<string> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new { email, password });
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result == null || string.IsNullOrEmpty(result.Token))
        {
            throw new Exception("La respuesta de login no contenía un token válido.");
        }

        SetToken(result.Token);
        return result.Token;
    }

    /// <summary>
    /// Registra un nuevo tenant/usuario en la plataforma.
    /// </summary>
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var response = await _http.PostAsJsonAsync("auth/register", request);
        return await HandleResponseAsync<AuthResponse>(response);
    }

    /// <summary>
    /// Inicia sesión como administrador y obtiene el token de acceso.
    /// </summary>
    public async Task<string> AdminLoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("admin/auth/login", new { email, password });
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        if (result == null || string.IsNullOrEmpty(result.Token))
        {
            throw new Exception("La respuesta de login de admin no contenía un token válido.");
        }

        SetToken(result.Token);
        return result.Token;
    }

    /// <summary>
    /// Solicita un restablecimiento de contraseña.
    /// </summary>
    /// <param name="email">Correo del usuario</param>
    /// <param name="isAdmin">Indica si es para la plataforma de administración</param>
    public async Task<PasswordResetResponse> ForgotPasswordAsync(string email, bool isAdmin = false)
    {
        var endpoint = isAdmin ? "admin/auth/forgot-password" : "auth/forgot-password";
        var response = await _http.PostAsJsonAsync(endpoint, new ForgotPasswordRequest(email));
        return await HandleResponseAsync<PasswordResetResponse>(response);
    }

    /// <summary>
    /// Restablece la contraseña utilizando un token.
    /// </summary>
    public async Task<PasswordResetResponse> ResetPasswordAsync(string token, string newPassword, bool isAdmin = false)
    {
        var endpoint = isAdmin ? "admin/auth/reset-password" : "auth/reset-password";
        var response = await _http.PostAsJsonAsync(endpoint, new ResetPasswordRequest(token, newPassword));
        return await HandleResponseAsync<PasswordResetResponse>(response);
    }

    #endregion

    #region Health Check

    /// <summary>
    /// Verifica la salud de la plataforma.
    /// </summary>
    /// <returns>True si la plataforma está saludable, false en caso contrario.</returns>
    public async Task<bool> CheckHealthAsync()
    {
        try
        {
            var response = await _http.GetAsync("../health");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    #endregion

    #region Sequences

    public async Task<DocumentSequenceDto> UpdateSequenceAsync(Guid terminalId, string docType, UpdateSequenceRequest request)
    {
        var response = await _http.PutAsJsonAsync($"terminals/{terminalId}/sequences/{docType}", request);
        return await HandleResponseAsync<DocumentSequenceDto>(response);
    }

    /// <summary>
    /// Obtiene el siguiente consecutivo disponible para una terminal y tipo de documento.
    /// </summary>
    public async Task<NextConsecutiveResponse> GetNextConsecutiveAsync(Guid terminalId, string docType)
    {
        var response = await _http.GetAsync($"terminals/{terminalId}/sequences/{docType}/next");
        return await HandleResponseAsync<NextConsecutiveResponse>(response);
    }

    #endregion

    #region Management

    /// <summary>
    /// Invita a un nuevo usuario al tenant actual.
    /// </summary>
    public async Task InviteUserAsync(string email, string role)
    {
        var response = await _http.PostAsJsonAsync("tenants/invite", new InviteUserRequest(email, role));
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error invitando usuario: {response.StatusCode} - {error}");
        }
    }

    /// <summary>
    /// Obtiene información del tenant actual.
    /// </summary>
    public async Task<TenantResponse> GetCurrentTenantAsync()
    {
        var response = await _http.GetAsync("tenants/current");
        return await HandleResponseAsync<TenantResponse>(response);
    }

    /// <summary>
    /// Actualiza la información del tenant actual.
    /// </summary>
    public async Task<TenantResponse> UpdateCurrentTenantAsync(UpdateTenantRequest request)
    {
        var response = await _http.PutAsJsonAsync("tenants/current", request);
        return await HandleResponseAsync<TenantResponse>(response);
    }

    /// <summary>
    /// Obtiene la lista de sucursales del tenant.
    /// </summary>
    public async Task<List<BranchDto>> GetBranchesAsync()
    {
        var response = await _http.GetAsync("branches");
        return await HandleResponseAsync<List<BranchDto>>(response);
    }

    /// <summary>
    /// Obtiene las terminales de una sucursal.
    /// </summary>
    public async Task<List<TerminalDto>> GetTerminalsAsync(Guid branchId)
    {
        var response = await _http.GetAsync($"terminals?branchId={branchId}");
        return await HandleResponseAsync<List<TerminalDto>>(response);
    }

    #endregion

    #region Catalogs

    /// <summary>
    /// Obtiene la lista de todas las provincias.
    /// </summary>
    public async Task<List<ProvinciaResponse>> GetCatalogsProvinciasAsync()
    {
        var response = await _http.GetAsync("catalogs/provincias");
        return await HandleResponseAsync<List<ProvinciaResponse>>(response);
    }

    /// <summary>
    /// Obtiene la lista de cantones para una provincia específica.
    /// </summary>
    public async Task<List<CantonResponse>> GetCatalogsCantonesAsync(string provinceCode)
    {
        var response = await _http.GetAsync($"catalogs/cantones/{provinceCode}");
        return await HandleResponseAsync<List<CantonResponse>>(response);
    }

    /// <summary>
    /// Obtiene la lista de distritos para un cantón específico de una provincia.
    /// </summary>
    public async Task<List<DistritoResponse>> GetCatalogsDistritosAsync(string provinceCode, string cantonCode)
    {
        var response = await _http.GetAsync($"catalogs/distritos/{provinceCode}/{cantonCode}");
        return await HandleResponseAsync<List<DistritoResponse>>(response);
    }

    /// <summary>
    /// Busca actividades económicas por código o nombre.
    /// </summary>
    public async Task<List<EconomicActivityResponse>> GetEconomicActivitiesAsync(string search)
    {
        var query = "catalogs/economic-activities";
        if (!string.IsNullOrEmpty(search))
        {
            query += $"?search={Uri.EscapeDataString(search)}";
        }

        var response = await _http.GetAsync(query);
        return await HandleResponseAsync<List<EconomicActivityResponse>>(response);
    }

    #endregion

    #region REST Methods

    /// <summary>
    /// Genera un documento electrónico (XML firmado) a través de la API.
    /// OBSOLETO: Se recomienda usar EmitirAsync.
    /// </summary>
    [Obsolete("Use EmitirAsync.")]
    public async Task<DocumentoResult> GenerarDocumentoAsync(GenerarDocumentoRequest documento)
    {
        var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var response = await _http.PostAsJsonAsync("documentos/generar", documento, options);
        return await HandleResponseAsync<DocumentoResult>(response);
    }

    /// <summary>
    /// Envía un documento electrónico detallado a Hacienda.
    /// OBSOLETO: Se recomienda usar EmitirAsync para una integración más sencilla.
    /// </summary>
    [Obsolete("Use EmitirAsync para envíos simplificados o consulte con soporte si requiere envío detallado.")]
    public async Task<HaciendaEnvioResult> EnviarDocumentoAsync(GenerarDocumentoRequest documento)
    {
        var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var response = await _http.PostAsJsonAsync("v1/hacienda/enviar", documento, options);
        return await HandleResponseAsync<HaciendaEnvioResult>(response);
    }

    /// <summary>
    /// Punto de entrada principal para el envío de documentos electrónicos (Factura, Tiquete, Nota de Crédito).
    /// El servidor auto-rellena Emisor, Clave, Consecutivo desde datos del Tenant si no se proporcionan.
    /// </summary>
    public async Task<HaciendaEnvioResult> EmitirAsync(EnviarDocumentoSimplificadoRequest request)
    {
        var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var response = await _http.PostAsJsonAsync("v1/hacienda/emitir", request, options);
        return await HandleResponseAsync<HaciendaEnvioResult>(response);
    }

    /// <summary>
    /// Método de conveniencia para emitir una Nota de Crédito que anula una factura anterior.
    /// </summary>
    /// <param name="claveFacturaOriginal">Clave de 50 dígitos de la factura a anular.</param>
    /// <param name="razon">Razón de la anulación.</param>
    /// <param name="lineas">Opcional: Líneas que se reintegran (si es anulación total, omitir para que se asuman todas).</param>
    public async Task<HaciendaEnvioResult> EmitirNotaCreditoAnulasenAsync(string claveFacturaOriginal, string razon, List<LineaDetalleRequest>? lineas = null)
    {
        var request = new EnviarDocumentoSimplificadoRequest
        {
            TipoDocumento = "03",
            Referencia = new ReferenciaSimplificada
            {
                ClaveDocumentoReferencia = claveFacturaOriginal,
                Codigo = "01", // Anula documento de referencia
                Razon = razon
            },
            Lineas = lineas ?? new List<LineaDetalleRequest>() // Si va vacío, el backend debería manejarlo o requerir al menos una
        };

        return await EmitirAsync(request);
    }

    /// <summary>
    /// Consulta el estado de un documento por su clave de 50 dígitos.
    /// </summary>
    public async Task<HaciendaEnvioResult> ConsultarEstadoAsync(string clave)
    {
        var response = await _http.GetAsync($"v1/hacienda/estado/{clave}");
        return await HandleResponseAsync<HaciendaEnvioResult>(response);
    }

    /// <summary>
    /// Realiza el endoso (cesión de derechos) de un documento.
    /// </summary>
    public async Task<MensajeReceptorResponse> EndosarDocumentoAsync(MensajeEndosoRequest request)
    {
        var response = await _http.PostAsJsonAsync("v1/hacienda/endosar", request);
        return await HandleResponseAsync<MensajeReceptorResponse>(response);
    }

    /// <summary>
    /// Envía un mensaje de aceptación o rechazo (Mensaje de Receptor).
    /// </summary>
    public async Task<MensajeReceptorResponse> ConfirmarDocumentoAsync(MensajeEndosoRequest request)
    {
        var response = await _http.PostAsJsonAsync("v1/hacienda/aceptar", request);
        return await HandleResponseAsync<MensajeReceptorResponse>(response);
    }

    #endregion

    #region Stripe Integration

    /// <summary>
    /// Crea una sesión de Checkout de Stripe para suscripción.
    /// </summary>
    public async Task<StripeCheckoutResult> CreateCheckoutSessionAsync(StripeCheckoutRequest request)
    {
        var response = await _http.PostAsJsonAsync("stripe/create-checkout-session", request);
        return await HandleResponseAsync<StripeCheckoutResult>(response);
    }

    /// <summary>
    /// Crea una sesión del Portal de Cliente de Stripe.
    /// </summary>
    public async Task<StripePortalResult> CreatePortalSessionAsync()
    {
        var response = await _http.PostAsync("stripe/create-portal-session", null);
        return await HandleResponseAsync<StripePortalResult>(response);
    }

    /// <summary>
    /// Obtiene el historial de pagos del tenant actual.
    /// </summary>
    public async Task<List<StripePaymentDto>> GetPaymentHistoryAsync(int limit = 50)
    {
        var response = await _http.GetAsync($"stripe/payment-history?limit={limit}");
        return await HandleResponseAsync<List<StripePaymentDto>>(response);
    }

    /// <summary>
    /// Obtiene la lista de planes de suscripción disponibles.
    /// </summary>
    public async Task<List<SubscriptionPlanDto>> GetSubscriptionPlansAsync()
    {
        var response = await _http.GetAsync("subscription-plans");
        return await HandleResponseAsync<List<SubscriptionPlanDto>>(response);
    }

    /// <summary>
    /// Obtiene información de facturación detallada del tenant.
    /// </summary>
    public async Task<BillingInfoDto> GetBillingInfoAsync()
    {
        var response = await _http.GetAsync("v1/subscription/billing-info"); // Se ajustó a v1/
        return await HandleResponseAsync<BillingInfoDto>(response);
    }

    /// <summary>
    /// Compra un paquete de documentos extras.
    /// </summary>
    public async Task<T> PurchaseEnvelopeAsync<T>(object request)
    {
        var response = await _http.PostAsJsonAsync("v1/subscription/purchase-envelope", request);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Reporta un pago para la suscripción mensual.
    /// </summary>
    public async Task<T> ReportSubscriptionPaymentAsync<T>(object request)
    {
        var response = await _http.PostAsJsonAsync("v1/subscription/report-payment", request);
        return await HandleResponseAsync<T>(response);
    }

    #endregion

    #region Notifications & Email Engine

    /// <summary>
    /// Encola una notificación para ser enviada vía Email, SignalR o ambos de forma asíncrona (Outbox).
    /// </summary>
    public async Task EnqueueNotificationAsync(EnqueueNotificationRequest request)
    {
        var response = await _http.PostAsJsonAsync("v1/notifications/enqueue", request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error encolando notificación: {response.StatusCode} - {error}");
        }
    }

    /// <summary>
    /// Obtiene las notificaciones del usuario actual.
    /// </summary>
    public async Task<List<SystemNotificationDto>> GetNotificationsAsync(int take = 50)
    {
        var response = await _http.GetAsync($"notifications?take={take}");
        return await HandleResponseAsync<List<SystemNotificationDto>>(response);
    }

    /// <summary>
    /// Obtiene las notificaciones no leídas del tenant.
    /// </summary>
    public async Task<UnreadNotificationsResponse> GetUnreadNotificationsAsync()
    {
        var response = await _http.GetAsync("notifications/unread");
        return await HandleResponseAsync<UnreadNotificationsResponse>(response);
    }

    /// <summary>
    /// Marca una notificación como leída.
    /// </summary>
    public async Task MarkNotificationAsReadAsync(Guid notificationId)
    {
        var response = await _http.PostAsync($"notifications/{notificationId}/read", null);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error marcando notificación: {response.StatusCode} - {error}");
        }
    }

    /// <summary>
    /// Marca todas las notificaciones como leídas.
    /// </summary>
    public async Task MarkAllNotificationsAsReadAsync()
    {
        var response = await _http.PostAsync("notifications/read-all", null);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error marcando notificaciones: {response.StatusCode} - {error}");
        }
    }

    #endregion

    #region Dashboard & Analytics

    /// <summary>
    /// Obtiene estadísticas resumidas para el dashboard.
    /// </summary>
    public async Task<DashboardStatsResponse> GetDashboardStatsAsync()
    {
        var response = await _http.GetAsync("dashboard/stats");
        return await HandleResponseAsync<DashboardStatsResponse>(response);
    }

    #endregion

    #region Document Queries

    /// <summary>
    /// Busca comprobantes electrónicos registrados.
    /// </summary>
    public async Task<PagedResult<DocumentoJsonDto>> BuscarDocumentosAsync(
        string? clave = null,
        string? consecutivo = null,
        string? cliente = null,
        string? cedula = null,
        string? correo = null,
        DateTime? fechaDesde = null,
        DateTime? fechaHasta = null,
        int page = 1,
        int pageSize = 20)
    {
        var query = $"documentos-json?page={page}&pageSize={pageSize}";
        if (!string.IsNullOrEmpty(clave)) query += $"&clave={Uri.EscapeDataString(clave)}";
        if (!string.IsNullOrEmpty(consecutivo)) query += $"&consecutivo={Uri.EscapeDataString(consecutivo)}";
        if (!string.IsNullOrEmpty(cliente)) query += $"&cliente={Uri.EscapeDataString(cliente)}";
        if (!string.IsNullOrEmpty(cedula)) query += $"&cedula={Uri.EscapeDataString(cedula)}";
        if (!string.IsNullOrEmpty(correo)) query += $"&correo={Uri.EscapeDataString(correo)}";
        if (fechaDesde.HasValue) query += $"&fechaDesde={fechaDesde.Value:yyyy-MM-dd}";
        if (fechaHasta.HasValue) query += $"&fechaHasta={fechaHasta.Value:yyyy-MM-dd}";

        var response = await _http.GetAsync(query);
        return await HandleResponseAsync<PagedResult<DocumentoJsonDto>>(response);
    }

    /// <summary>
    /// Descarga el XML firmado de un documento.
    /// </summary>
    public async Task<byte[]> GetDocumentXmlAsync(Guid id)
    {
        var response = await _http.GetAsync($"documentos-json/{id}/xml");
        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// Descarga la respuesta XML de Hacienda para un documento.
    /// </summary>
    public async Task<byte[]> GetDocumentRespuestaAsync(Guid id)
    {
        var response = await _http.GetAsync($"documentos-json/{id}/respuesta");
        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// Genera y descarga el PDF de un documento.
    /// </summary>
    public async Task<byte[]> GetDocumentPdfAsync(Guid id)
    {
        var response = await _http.GetAsync($"documentos-json/{id}/pdf");
        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <summary>
    /// Solicita el re-procesamiento de un rechazo de Hacienda para extraer errores estructurados.
    /// Puede recibir el ID (GUID) o el Consecutivo del documento.
    /// </summary>
    public async Task<object> ReprocessRejectionAsync(string identifier)
    {
        var response = await _http.PostAsync($"documentos-json/{identifier}/reprocess-rejection", null);
        return await HandleResponseAsync<object>(response);
    }

    #endregion

    #region Hacienda Configuration

    /// <summary>
    /// Obtiene la configuración actual de Hacienda.
    /// </summary>
    public async Task<HaciendaConfigResponse> GetHaciendaConfigAsync()
    {
        var response = await _http.GetAsync("hacienda/config");
        return await HandleResponseAsync<HaciendaConfigResponse>(response);
    }

    /// <summary>
    /// Guarda la configuración de Hacienda (Certificado, PIN, Credenciales).
    /// </summary>
    public async Task<HaciendaConfigResponse> SaveHaciendaConfigAsync(HaciendaConfigRequest config)
    {
        var response = await _http.PutAsJsonAsync("hacienda/config", config);
        return await HandleResponseAsync<HaciendaConfigResponse>(response);
    }

    /// <summary>
    /// Lista los ambientes disponibles y su estado de configuración.
    /// </summary>
    public async Task<List<EnvironmentInfoResponse>> GetHaciendaEnvironmentsAsync()
    {
        var response = await _http.GetAsync("hacienda/config/environments");
        return await HandleResponseAsync<List<EnvironmentInfoResponse>>(response);
    }

    /// <summary>
    /// Obtiene el ambiente de Hacienda que está actualmente activo.
    /// </summary>
    public async Task<EnvironmentInfoResponse> GetActiveHaciendaEnvironmentAsync()
    {
        var response = await _http.GetAsync("hacienda/config/environment/active");
        return await HandleResponseAsync<EnvironmentInfoResponse>(response);
    }

    /// <summary>
    /// Cambia el ambiente activo (Sandbox/Production).
    /// </summary>
    public async Task<EnvironmentInfoResponse> SwitchHaciendaEnvironmentAsync(HaciendaEnvironment environment)
    {
        var response = await _http.PostAsJsonAsync("hacienda/config/environment/switch", new SwitchEnvironmentRequest(environment));
        return await HandleResponseAsync<EnvironmentInfoResponse>(response);
    }

    #endregion

    #region Developer API

    /// <summary>
    /// Obtiene las aplicaciones registradas por el desarrollador actual.
    /// </summary>
    public async Task<List<RegisteredApplicationResponse>> GetDeveloperApplicationsAsync()
    {
        var response = await _http.GetAsync("v1/developer/applications");
        return await HandleResponseAsync<List<RegisteredApplicationResponse>>(response);
    }

    /// <summary>
    /// Registra una nueva aplicación para el portal de desarrolladores.
    /// </summary>
    public async Task<RegisteredApplicationResponse> RegisterDeveloperApplicationAsync(RegisterAppRequest request)
    {
        var response = await _http.PostAsJsonAsync("v1/developer/applications", request);
        return await HandleResponseAsync<RegisteredApplicationResponse>(response);
    }

    /// <summary>
    /// Desactiva una aplicación registrada.
    /// </summary>
    public async Task DeactivateDeveloperApplicationAsync(Guid id)
    {
        var response = await _http.DeleteAsync($"v1/developer/applications/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error desactivando aplicación: {response.StatusCode} - {error}");
        }
    }

    /// <summary>
    /// Obtiene los identificadores SAE estándar disponibles.
    /// </summary>
    public async Task<List<SaeIdentifierResponse>> GetSaeIdentifiersAsync()
    {
        var response = await _http.GetAsync("v1/developer/applications/sae-identifiers");
        return await HandleResponseAsync<List<SaeIdentifierResponse>>(response);
    }

    #endregion

    #region Admin API

    /// <summary>
    /// Obtiene el perfil del administrador actual.
    /// </summary>
    public async Task<AdminMeResponse> GetAdminMeAsync()
    {
        var response = await _http.GetAsync("admin/auth/me");
        return await HandleResponseAsync<AdminMeResponse>(response);
    }

    /// <summary>
    /// Obtiene estadísticas globales de la plataforma (Solo Admin).
    /// </summary>
    public async Task<GlobalStatsResponse> GetGlobalStatsAsync()
    {
        var response = await _http.GetAsync("admin/analytics/global-stats");
        return await HandleResponseAsync<GlobalStatsResponse>(response);
    }

    /// <summary>
    /// Obtiene la configuración global de la plataforma (Solo Admin).
    /// </summary>
    public async Task<AdminConfigResponse> GetAdminConfigAsync(string adminKey)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "admin/config");
        request.Headers.Add("X-Admin-Key", adminKey);

        var response = await _http.SendAsync(request);
        return await HandleResponseAsync<AdminConfigResponse>(response);
    }

    #endregion

    #region License API

    /// <summary>
    /// Valida una licencia (Check-in) para una terminal o aplicación.
    /// </summary>
    public async Task<LicenseValidationResult> ValidateLicenseAsync(ValidateLicenseRequest request)
    {
        var response = await _http.PostAsJsonAsync("v1/licenses/validate", request);
        return await HandleLicenseResponseAsync(response);
    }

    /// <summary>
    /// Activa una licencia en una máquina vinculándola permanentemente.
    /// </summary>
    public async Task<LicenseValidationResult> ActivateLicenseAsync(ActivateLicenseRequest request)
    {
        var response = await _http.PostAsJsonAsync("v1/licenses/activate", request);
        return await HandleLicenseResponseAsync(response);
    }

    /// <summary>
    /// Obtiene los paquetes de licencias disponibles.
    /// </summary>
    public async Task<List<object>> GetLicensePackagesAsync(bool systemOnly = false, Guid? tenantId = null)
    {
        var query = $"v1/license-packages?systemOnly={systemOnly}";
        if (tenantId.HasValue) query += $"&tenantId={tenantId}";
        
        var response = await _http.GetAsync(query);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error SAE API: {response.StatusCode} - {error}");
        }
        return (await response.Content.ReadFromJsonAsync<List<object>>(_readOptions))!;
    }

    #endregion

    #region Realtime (SignalR) Methods

    /// <summary>
    /// Inicia la conexión en tiempo real con HaciendaHub.
    /// </summary>
    public async Task StartHaciendaRealtimeAsync()
    {
        if (_haciendaConnection != null && _haciendaConnection.State == HubConnectionState.Connected)
            return;

        _haciendaConnection = BuildHubConnection(_haciendaHubUrl);

        _haciendaConnection.On<HaciendaEnvioIniciadoEvent>("HaciendaEnvioIniciado", (e) => OnEnvioIniciado?.Invoke(e));
        _haciendaConnection.On<HaciendaRespuestaRecibidaEvent>("HaciendaRespuestaRecibida", (e) => OnRespuestaRecibida?.Invoke(e));
        _haciendaConnection.On<HaciendaMensajeIniciadoEvent>("HaciendaMensajeIniciado", (e) => OnMensajeIniciado?.Invoke(e));
        _haciendaConnection.On<HaciendaMensajeRespuestaEvent>("HaciendaMensajeRespuesta", (e) => OnMensajeRespuestaRecibida?.Invoke(e));
        _haciendaConnection.On<SystemNotificationDto>("ReceiveNotification", (n) => OnNotificationReceived?.Invoke(n));

        _haciendaConnection.Closed += (exception) =>
        {
            OnHaciendaRealtimeClosed?.Invoke(exception);
            OnRealtimeClosed?.Invoke(exception);
            return Task.CompletedTask;
        };

        await _haciendaConnection.StartAsync();
    }

    /// <summary>
    /// Inicia la conexión en tiempo real con AppHub (Notificaciones externas).
    /// </summary>
    public async Task StartAppRealtimeAsync()
    {
        if (_appConnection != null && _appConnection.State == HubConnectionState.Connected)
            return;

        _appConnection = BuildHubConnection(_appHubUrl);

        // Escuchar eventos de notificación por defecto
        _appConnection.On<object>("ReceiveNotification", (payload) => OnAppEventReceived?.Invoke("ReceiveNotification", payload));

        _appConnection.Closed += (exception) =>
        {
            OnAppRealtimeClosed?.Invoke(exception);
            return Task.CompletedTask;
        };

        await _appConnection.StartAsync();
    }

    /// <summary>
    /// Permite suscribirse a un evento específico del AppHub.
    /// </summary>
    public void OnAppEvent<T>(string methodName, Action<T> handler)
    {
        if (_appConnection == null) throw new InvalidOperationException("Debe llamar a StartAppRealtimeAsync primero.");
        _appConnection.On<T>(methodName, handler);
    }

    private HubConnection BuildHubConnection(string hubUrl)
    {
        var fullUrl = hubUrl;
        var queryParams = new List<string>();

        if (!string.IsNullOrEmpty(_tenantId)) queryParams.Add($"tenantId={Uri.EscapeDataString(_tenantId)}");
        if (!string.IsNullOrEmpty(_apiKey)) queryParams.Add($"apiKey={Uri.EscapeDataString(_apiKey)}");

        if (queryParams.Count > 0)
        {
            fullUrl += "?" + string.Join("&", queryParams);
        }

        return new HubConnectionBuilder()
            .WithUrl(fullUrl, options =>
            {
                if (!string.IsNullOrEmpty(_token))
                {
                    options.AccessTokenProvider = () => Task.FromResult<string?>(_token);
                }

                if (!string.IsNullOrEmpty(_apiKey))
                {
                    options.Headers.Add("X-API-KEY", _apiKey);
                }
            })
            .WithAutomaticReconnect()
            .Build();
    }

    /// <summary>
    /// Detiene todas las conexiones en tiempo real.
    /// </summary>
    public async Task StopRealtimeAsync()
    {
        if (_haciendaConnection != null) await _haciendaConnection.StopAsync();
        if (_appConnection != null) await _appConnection.StopAsync();
    }

    [Obsolete("Use StartHaciendaRealtimeAsync")]
    public Task StartRealtimeAsync() => StartHaciendaRealtimeAsync();

    #endregion

    #region Helpers

    private static readonly JsonSerializerOptions _readOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error SAE API: {response.StatusCode} - {error}");
        }
        return (await response.Content.ReadFromJsonAsync<T>(_readOptions))!;
    }

    private async Task<LicenseValidationResult> HandleLicenseResponseAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error SAE API: {response.StatusCode} - {error}");
        }

        using var doc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        var root = doc.RootElement;
        
        var result = new LicenseValidationResult
        {
            Status = root.TryGetProperty("status", out var s) ? (s.GetString() ?? "") : "",
            LicenseId = root.TryGetProperty("licenseId", out var lid) && lid.ValueKind != JsonValueKind.Null ? lid.GetGuid() : null,
            Type = root.TryGetProperty("type", out var typ) ? typ.GetString() : null,
            ExpirationDate = root.TryGetProperty("expirationDate", out var exp) && exp.ValueKind != JsonValueKind.Null ? exp.GetDateTime() : null,
            OfflineToken = root.TryGetProperty("offlineToken", out var tok) ? tok.GetString() : null,
            Message = root.TryGetProperty("message", out var msg) ? msg.GetString() : null,
            TerminalId = root.TryGetProperty("terminalId", out var tid) && tid.ValueKind != JsonValueKind.Null ? tid.GetGuid() : null,
            TerminalName = root.TryGetProperty("terminalName", out var tname) ? tname.GetString() : null,
            TerminalCode = root.TryGetProperty("terminalCode", out var tcode) ? tcode.GetString() : null,
            BranchCode = root.TryGetProperty("branchCode", out var bcode) ? bcode.GetString() : null
        };

        if (root.TryGetProperty("features", out var feat) && feat.ValueKind == JsonValueKind.Array)
        {
            result.Features = JsonSerializer.Deserialize<List<string>>(feat.GetRawText(), _readOptions);
        }

        if (root.TryGetProperty("modules", out var mod) && mod.ValueKind == JsonValueKind.Array)
        {
            result.Modules = JsonSerializer.Deserialize<List<string>>(mod.GetRawText(), _readOptions);
        }

        return result;
    }

    /// <summary>
    /// Ejecuta el proceso de 'Backfill' masivo de reportes en el backend.
    /// </summary>
    public async Task<string> EjecutarBackfillAsync(CancellationToken cancellationToken = default)
    {
        var response = await _http.PostAsync("reports/backfill", null, cancellationToken);
        if(!response.IsSuccessStatusCode)
        {
             var errorBody = await response.Content.ReadAsStringAsync();
             throw new Exception($"Error {response.StatusCode} al procesar backfill: {errorBody}");
        }
        return await response.Content.ReadAsStringAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_haciendaConnection != null) await _haciendaConnection.DisposeAsync();
        if (_appConnection != null) await _appConnection.DisposeAsync();
        _http.Dispose();
    }

    #endregion

    #region License Addons

    /// <summary>
    /// Obtiene el catálogo de paquetes de licencias disponibles para el tenant.
    /// </summary>
    public async Task<List<T>> GetAddonPackagesAsync<T>()
    {
        var response = await _http.GetAsync("v1/LicenseAddon/packages");
        return await HandleResponseAsync<List<T>>(response);
    }

    /// <summary>
    /// Compra un paquete de licencias addon directamente.
    /// </summary>
    public async Task<T> PurchaseAddonAsync<T>(Guid packageId)
    {
        var response = await _http.PostAsJsonAsync("v1/LicenseAddon/purchase", new { packageId });
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Obtiene todos los addons activos del tenant.
    /// </summary>
    public async Task<List<T>> GetTenantAddonsAsync<T>()
    {
        var response = await _http.GetAsync("v1/LicenseAddon");
        return await HandleResponseAsync<List<T>>(response);
    }

    /// <summary>
    /// Obtiene el resumen de facturación del tenant incluyendo addons.
    /// </summary>
    public async Task<T> GetBillingSummaryAsync<T>()
    {
        var response = await _http.GetAsync("v1/LicenseAddon/billing-summary");
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Reporta la compra de un addon por SINPE o transferencia (espera aprobación admin).
    /// </summary>
    public async Task<T> ReportAddonPurchaseAsync<T>(Guid packageId, string reference, string paymentMethod, decimal amount)
    {
        var response = await _http.PostAsJsonAsync("v1/LicenseAddon/report", new { packageId, reference, paymentMethod, amount });
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Cancela un addon activo.
    /// </summary>
    public async Task CancelAddonAsync(Guid addonId)
    {
        var response = await _http.PostAsync($"v1/LicenseAddon/{addonId}/cancel", null);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error cancelando addon: {response.StatusCode} - {error}");
        }
    }

    #endregion

    #region Reports

    /// <summary>
    /// Obtiene el reporte de IVA (D-104) para un período.
    /// </summary>
    public async Task<T> GetReporteIvaAsync<T>(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/iva?mes={mes}&anio={anio}", ct);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Obtiene el libro de ventas detallado para un período.
    /// </summary>
    public async Task<T> GetLibroVentasAsync<T>(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/libro-ventas?mes={mes}&anio={anio}", ct);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Obtiene el reporte de compras (crédito fiscal) para un período.
    /// </summary>
    public async Task<T> GetReporteComprasAsync<T>(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/compras?mes={mes}&anio={anio}", ct);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Obtiene el reporte CABYS para un período.
    /// </summary>
    public async Task<T> GetReporteCabysAsync<T>(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/cabys?mes={mes}&anio={anio}", ct);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Obtiene el asiento contable para un período.
    /// </summary>
    public async Task<T> GetAsientoContableAsync<T>(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/asiento?mes={mes}&anio={anio}", ct);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Obtiene el reporte de extemporaneidad (documentos tardíos) para un período.
    /// </summary>
    public async Task<T> GetReporteExtemporaneidadAsync<T>(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/extemporaneidad?mes={mes}&anio={anio}", ct);
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Descarga el PDF del reporte de IVA (D-104).
    /// </summary>
    public async Task<byte[]> GetReporteIvaPdfAsync(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/iva/pdf?mes={mes}&anio={anio}", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync(ct);
    }

    /// <summary>
    /// Descarga el Excel del libro de ventas.
    /// </summary>
    public async Task<byte[]> GetLibroVentasExcelAsync(int mes, int anio, CancellationToken ct = default)
    {
        var response = await _http.GetAsync($"v1/reports/libro-ventas/excel?mes={mes}&anio={anio}", ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync(ct);
    }

    #endregion

    #region Tenant Users

    /// <summary>
    /// Obtiene todos los usuarios del tenant actual.
    /// </summary>
    public async Task<List<T>> GetTenantUsersAsync<T>()
    {
        var response = await _http.GetAsync("v1/tenant-users");
        return await HandleResponseAsync<List<T>>(response);
    }

    /// <summary>
    /// Invita a un usuario al tenant. Recibe email, role y opcionalmente customRoleId y branchId.
    /// </summary>
    public async Task<T> InviteTenantUserAsync<T>(string email, string role, Guid? customRoleId = null, Guid? branchId = null)
    {
        var response = await _http.PostAsJsonAsync("v1/tenant-users/invite", new { email, role, customRoleId, branchId });
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Acepta una invitación a un tenant usando el token recibido por correo (no requiere auth).
    /// </summary>
    public async Task<T> AcceptTenantInvitationAsync<T>(string token, string password, string fullName)
    {
        var response = await _http.PostAsJsonAsync("v1/tenant-users/accept-invite", new { token, password, fullName });
        return await HandleResponseAsync<T>(response);
    }

    /// <summary>
    /// Elimina un usuario del tenant.
    /// </summary>
    public async Task RemoveTenantUserAsync(Guid userId)
    {
        var response = await _http.DeleteAsync($"v1/tenant-users/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error removiendo usuario: {response.StatusCode} - {error}");
        }
    }

    /// <summary>
    /// Actualiza el rol de un usuario dentro del tenant.
    /// </summary>
    public async Task UpdateTenantUserRoleAsync(Guid userId, string role, Guid? customRoleId = null)
    {
        var response = await _http.PutAsJsonAsync($"v1/tenant-users/{userId}/role", new { role, customRoleId });
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error actualizando rol: {response.StatusCode} - {error}");
        }
    }

    #endregion

    #region Backups

    /// <summary>
    /// Obtiene la configuración de respaldos del establecimiento.
    /// </summary>
    public async Task<BackupConfigDto> GetBackupConfigAsync(CancellationToken ct = default)
    {
        var response = await _http.GetAsync("v1/backups/config", ct);
        var wrapper = await HandleResponseAsync<BackupApiWrapper<BackupConfigDto>>(response);
        return wrapper.Resultado ?? new BackupConfigDto();
    }

    /// <summary>
    /// Obtiene el historial de respaldos del establecimiento.
    /// </summary>
    public async Task<List<BackupHistoryDto>> GetBackupHistoryAsync(CancellationToken ct = default)
    {
        var response = await _http.GetAsync("v1/backups/history", ct);
        var wrapper = await HandleResponseAsync<BackupApiWrapper<List<BackupHistoryDto>>>(response);
        return wrapper.Resultado ?? new List<BackupHistoryDto>();
    }

    /// <summary>
    /// Sube un archivo de respaldo al servidor SAE para su distribución a los proveedores de nube configurados.
    /// </summary>
    public async Task<BackupExecutionRespuesta> UploadBackupAsync(
        byte[] fileBytes,
        string fileName,
        string? checksum = null,
        bool esAuto = true,
        CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(fileBytes);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        content.Add(fileContent, "file", fileName);
        content.Add(new StringContent(esAuto.ToString().ToLower()), "esAuto");
        if (!string.IsNullOrEmpty(checksum))
            content.Add(new StringContent(checksum), "checksum");

        var response = await _http.PostAsync("v1/backups/upload", content, ct);
        var wrapper = await HandleResponseAsync<BackupApiWrapper<BackupExecutionRespuesta>>(response);
        return wrapper.Resultado ?? new BackupExecutionRespuesta { Success = false };
    }

    #endregion
}

/// <summary>Internal helper to unwrap SAE API envelope responses.</summary>
internal class BackupApiWrapper<T>
{
    public bool Error { get; set; }
    public string? Message { get; set; }
    public T? Resultado { get; set; }
}
