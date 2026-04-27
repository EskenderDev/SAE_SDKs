using System.ComponentModel.DataAnnotations;

namespace SAE.Sdk.Models;

public record CodigoComercialRequest(
    string Tipo,
    string Codigo);

public class GenerarDocumentoRequest
{
    [Required] public string TipoDocumento { get; set; } = string.Empty;
    public ClaveRequest Clave { get; set; } = null!;
    public ConsecutivoRequest Consecutivo { get; set; } = null!;
    public string CodigoActividadEmisor { get; set; } = string.Empty;
    [Required] public string CodigoActividadReceptor { get; set; } = string.Empty;
    [Required] public DateTime FechaEmision { get; set; }
    public string ProveedorSistemas { get; set; } = string.Empty;
    public EmisorRequest Emisor { get; set; } = null!;
    [Required] public ReceptorRequest Receptor { get; set; } = null!;
    [Required] public string CondicionVenta { get; set; } = string.Empty;
    public CondicionVentaOtro? CondicionVentaOtros { get; set; }
    public int? PlazoCredito { get; set; }
    [Required] public DetalleServicioRequest DetalleServicio { get; set; } = null!;
    public ResumenFacturaRequest? ResumenFactura { get; set; }
    public List<InformacionReferenciaRequest>? InformacionReferencia { get; set; }
    public List<OtrosCargosRequest>? OtrosCargos { get; set; }
    public OtrosRequest? OtrosDatos { get; set; }
}

public record ClaveRequest(
    [Range(1, 999)] int CodigoPais,
    [Range(1, 31)] int Dia,
    [Range(1, 12)] int Mes,
    [Range(0, 99)] int Ano,
    ConsecutivoRequest Consecutivo,
    [StringLength(12, MinimumLength = 9)] string CedulaEmisor,
    string Situacion);

public record ConsecutivoRequest(
    [Range(1, 999)] int Establecimiento,
    [Range(1, 99999)] int Terminal,
    [Range(1, 9999999999)] long NumeroConsecutivo);

public record IdentificacionRequest(
    string Tipo,
    string Numero);

public class EmisorRequest
{
    [Required] public IdentificacionRequest Identificacion { get; set; } = null!;
    [Required] public string Nombre { get; set; } = string.Empty;
    public string? NombreComercial { get; set; }
    [Required] public UbicacionRequest Ubicacion { get; set; } = null!;
    [Required] public TelefonoRequest Telefono { get; set; } = null!;
    [Required] public List<string> CorreoElectronico { get; set; } = new();
    public string? RegistroFiscal8707 { get; set; }
    public bool SeFacturanBebidasAlc { get; set; } = false;
}

public record TelefonoRequest(
    int CodigoPais,
    long Numero);

public class ReceptorRequest
{
    [Required] public IdentificacionRequest Identificacion { get; set; } = null!;
    [Required] public string Nombre { get; set; } = string.Empty;
    public string? IdentificacionExtranjero { get; set; }
    public string? NombreComercial { get; set; }
    public UbicacionRequest? Ubicacion { get; set; }
    public string? OtrasSenasExtranjero { get; set; }
    public TelefonoRequest? Telefono { get; set; }
    [Required] public string CorreoElectronico { get; set; } = string.Empty;
}

public class UbicacionRequest
{
    [Required] public string Provincia { get; set; } = string.Empty;
    [Required] public string Canton { get; set; } = string.Empty;
    [Required] public string Distrito { get; set; } = string.Empty;
    [Required] public string Barrio { get; set; } = string.Empty;
    [Required] public string OtrasSenas { get; set; } = string.Empty;
}

public record DetalleServicioRequest
{
    public List<LineaDetalleRequest> LineasDetalle { get; set; } = new();
}

public record LineaDetalleRequest
{
    public int? NumeroLinea { get; set; }
    public string? PartidaArancelaria { get; set; }
    public string? CodigoCABYS { get; set; }
    public List<CodigoComercialRequest> CodigosComerciales { get; set; } = new();
    public decimal Cantidad { get; set; }
    public string UnidadMedida { get; set; }
    public string TipoTransaccion { get; set; }
    public string? UnidadMedidaComercial { get; set; }
    public string? Detalle { get; set; }
    public List<string>? NumeroVINoSerie { get; set; }
    public string? RegistroMedicamento { get; set; }
    public string? FormaFarmaceutica { get; set; }
    public DetalleSurtidoRequest? DetalleSurtido { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal? MontoTotal { get; set; }
    public List<DescuentoRequest> Descuentos { get; set; } = new();
    public decimal? SubTotal { get; set; }
    public string? IVACobradoFabrica { get; set; }
    public decimal? BaseImponible { get; set; }
    public List<ImpuestoRequest> Impuestos { get; set; } = new();
    public decimal? ImpuestoAsumidoEmisorFabrica { get; set; }
    public decimal? ImpuestoNeto { get; set; }
    public decimal? MontoTotalLinea { get; set; }
}

public record DescuentoRequest(
    decimal? MontoDescuento,
    string? CodigoDescuento,
    string? CodigoDescuentoOtro,
    string? NaturalezaDescuento);

public record DetalleSurtidoRequest(
    List<LineaDetalleSurtidoRequest> Lineas);

public record LineaDetalleSurtidoRequest(
    string CodigoCABYSSurtido,
    List<CodigoComercialSurtidoRequest>? CodigosComerciales,
    decimal Cantidad,
    string UnidadMedida,
    string? UnidadMedidaComercial,
    string Detalle,
    decimal PrecioUnitario,
    decimal? MontoTotal,
    List<DescuentoSurtidoRequest>? Descuentos,
    decimal? Subtotal,
    string? IVACobradoFabrica,
    decimal? BaseImponible,
    List<ImpuestoSurtidoRequest>? Impuestos,
    decimal? ImpuestoNeto,
    decimal? MontoTotalLinea);

public record CodigoComercialSurtidoRequest(
    string Tipo,
    string Codigo);

public record DescuentoSurtidoRequest(
    decimal Monto,
    string Codigo,
    string? DescuentoOtros);

public record ImpuestoSurtidoRequest(
    string Codigo,
    string? CodigoOTRO,
    string? CodigoTarifaIVA,
    decimal? Tarifa,
    decimal Monto,
    DatosImpuestoEspecificoSurtidoRequest? DatosImpuestoEspecifico);

public record DatosImpuestoEspecificoSurtidoRequest(
    decimal CantidadUnidadMedida,
    decimal? Porcentaje,
    decimal? Proporcion,
    decimal? VolumenUnidadConsumo,
    decimal ImpuestoUnidad);

public record ImpuestoRequest(
    string Codigo,
    string? CodigoImpuestoOTRO,
    string CodigoTarifaIVA,
    decimal? Tarifa,
    decimal? FactorCalculoIVA,
    decimal? Monto,
    ExoneracionRequest? Exoneracion);

public record ExoneracionRequest(
    string TipoDocumentoEX1,
    string? TipoDocumentoOTRO,
    string NumeroDocumento,
    int Articulo,
    int Inciso,
    string NombreInstitucion,
    string? NombreInstitucionOtros,
    DateTimeOffset FechaEmisionEX,
    decimal TarifaExonerada,
    decimal MontoExoneracion);

public record ResumenFacturaRequest(
    CodigoTipoMonedaRequest? CodigoTipoMoneda,
    decimal? TotalServGravados,
    decimal? TotalServExentos,
    decimal? TotalServExonerado,
    decimal? TotalServNoSujeto,
    decimal? TotalMercanciasGravadas,
    decimal? TotalMercanciasExentas,
    decimal? TotalMercExonerada,
    decimal? TotalMercNoSujeta,
    decimal? TotalGravado,
    decimal? TotalExento,
    decimal? TotalExonerado,
    decimal? TotalNoSujeto,
    decimal? TotalVenta,
    decimal? TotalDescuentos,
    decimal? TotalVentaNeta,
    TotalDesgloseImpuestoRequest? TotalDesgloseImpuesto,
    decimal? TotalImpuesto,
    decimal? TotalImpAsumEmisorFabrica,
    decimal? TotalIvaDevuelto,
    decimal? TotalOtrosCargos,
    List<MedioPagoRequest>? MediosPago,
    decimal? TotalComprobante);

public record CodigoTipoMonedaRequest(
    string CodigoMoneda,
    decimal TipoCambio);

public record TotalDesgloseImpuestoRequest(
    string Codigo,
    string CodigoTarifaIVA,
    decimal TotalMontoImpuesto);

public record MedioPagoRequest(
    string? TipoMedioPago,
    string? MedioPagoOtros,
    decimal? TotalMedioPago);

public record InformacionReferenciaRequest(
    string TipoDocIR,
    string? TipoDocRefOTRO,
    string Numero,
    DateTimeOffset FechaEmisionIR,
    string Codigo,
    string? CodigoReferenciaOTRO,
    string Razon);

public record OtrosCargosRequest(
    string TipoDocumentoOC,
    string? TipoDocumentoOTROS,
    IdentificacionRequest IdentificacionTercero,
    string NombreTercero,
    string Detalle,
    decimal PorcentajeOC,
    decimal MontoCargo);

public record OtrosRequest(
    List<string>? OtroTexto,
    string? OtroContenido);

public record CondicionVentaOtro(
    string Descripcion);

public record StripeCheckoutRequest(
    string PlanId);

public record StripeCheckoutResult(
    string Url);

public record StripePortalResult(
    string Url);

public record StripePaymentDto(
    Guid Id,
    Guid TenantId,
    string StripePaymentIntentId,
    string? StripeInvoiceId,
    decimal Amount,
    string Currency,
    string Status,
    string? Description,
    DateTime CreatedAt);

public record SubscriptionPlanDto(
    Guid Id,
    string Name,
    string Tier,
    decimal PriceMonthly,
    decimal PriceActual,
    int MonthlyLimit,
    string? Description,
    string? Category = null,
    string? BillingPeriod = null,
    decimal DiscountPercent = 0,
    List<string>? Features = null);

public record DocumentoJsonDto(
    Guid Id,
    string? Clave,
    string? Consecutivo,
    string? ClienteNombre,
    string? ClienteCedula,
    string? ClienteCorreo,
    DateTime FechaEmision,
    DateTime? FechaAceptacion,
    string TipoDocumento,
    decimal TotalComprobante,
    string Moneda,
    decimal TipoCambio,
    string? RequestPayload,
    string? JsonCalculado,
    string? RespuestaHaciendaJson,
    string? EstadoHacienda,
    string? MensajeEstado,
    object? ErrorDetails,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record DashboardStatsResponse(
    string TenantName,
    string TierName,
    decimal MonthlyPrice,
    int DocumentsUsedMonth,
    int MonthlyLimit,
    double UsagePercentage,
    DateTime? SubscriptionExpiresAt,
    string BillingPeriod,
    int RolloverDocuments,
    int PrepaidDocumentBags,
    List<RecentActivityDto> RecentActivity);

public record RecentActivityDto(
    Guid Id,
    string TipoDocumento,
    string Consecutivo,
    string ReceptorNombre,
    DateTime FechaEmision,
    decimal MontoTotal,
    string Moneda,
    string MonedaSimbolo,
    decimal TipoCambio,
    string EstadoHacienda);

public record SystemNotificationDto(
    Guid Id,
    string Title,
    string Message,
    NotificationType Type,
    string? ActionUrl,
    bool IsRead,
    DateTime CreatedAt);

public record BillingInfoDto(
    string CurrentPlan,
    string Tier,
    string BillingPeriod,
    decimal CurrentPrice,
    decimal MonthlyPrice,
    decimal DiscountPercent,
    string Currency,
    DateTime? NextBillingDate,
    int DocumentsUsed,
    int MonthlyLimit,
    int RolloverDocuments,
    int DocumentsRemaining,
    double UsagePercentage,
    int OverageBuffer,
    int OverageBufferPercent,
    decimal OverageRate,
    decimal ProjectedOverageCharge,
    bool IsOverLimit,
    bool IsInBufferZone);

public record PagedResult<T>(
    int Page,
    int PageSize,
    long TotalItems,
    int TotalPages,
    bool IsFirstPage,
    bool IsLastPage,
    bool HasNext,
    bool HasPrev,
    IEnumerable<T> Items);

public record HaciendaConfigRequest(
    string? IdCia,
    string? IdSucursal,
    string? PinP12,
    string? CertP12Base64,
    string? UsuarioOauth,
    string? PasswordOauth,
    string Environment = "Sandbox");

public record HaciendaConfigResponse(
    bool IsConfigured,
    bool? IsActive,
    bool? Enabled,
    string? IdCia,
    string Environment,
    string? BaseUrl,
    string? ClientId,
    string? IdpUsername,
    bool? HasCertificate,
    string? CertFileName,
    // Ubicación
    string? ProvinceCode,
    string? CantonCode,
    string? DistrictCode,
    string? AddressDetails,
    string? DefaultCurrency,
    decimal? DefaultCurrencyRate,
    string? EconomicActivityCode,
    DateTime? UpdatedAt);

public record EnvironmentInfoResponse(
    HaciendaEnvironment Environment,
    string EnvironmentName,
    bool IsConfigured,
    bool IsActive,
    string BaseUrl);

public record SwitchEnvironmentRequest(
    HaciendaEnvironment Environment);

public record HaciendaEnvioResult(
    Guid Id,
    string Clave,
    string TipoDocumento,
    HaciendaEstado Estado,
    string? MensajeEstado,
    object? ErrorDetails,
    int? HttpStatus,
    string? ResponseJson);

public record ProvinciaResponse(string Code, string Name);
public record CantonResponse(string Code, string Name, string ProvinceCode);
public record DistritoResponse(string Code, string Name, string ProvinceCode, string CantonCode);
public record EconomicActivityResponse(string Code, string Name, string TribuName, string Type);

public class UpdateTenantRequest
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? TaxId { get; set; }
    public string? OwnerEmail { get; set; }
    public int? IdentificationType { get; set; }
    public string? CommercialName { get; set; }
    public string? ProvinceCode { get; set; }
    public string? CantonCode { get; set; }
    public string? DistrictCode { get; set; }
    public string? AddressDetails { get; set; }
    public string? EconomicActivityCode { get; set; }
    public bool? IsActive { get; set; }
    public string? LocationNamesJson { get; set; }
    public string? DefaultCurrency { get; set; }
    public decimal? DefaultCurrencyRate { get; set; }
    public string? PrimaryColor { get; set; }
    public string? PaymentInstructions { get; set; }
}

public class TenantResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string TaxId { get; set; } = string.Empty;
    public SubscriptionTier Tier { get; set; }
    public TenantStatus Status { get; set; }
    public string? OwnerEmail { get; set; }
    public string? ApiKey { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IdentificationType { get; set; }
    public string? CommercialName { get; set; }
    public string? ProvinceCode { get; set; }
    public string? CantonCode { get; set; }
    public string? DistrictCode { get; set; }
    public string? AddressDetails { get; set; }
    public string? EconomicActivityCode { get; set; }
}

public class RegisterTenantRequest
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string TaxId { get; set; } = string.Empty;
    public string? OwnerEmail { get; set; }
}

public class RegisterRequest
{
    [Required] public string CompanyName { get; set; } = string.Empty;
    [Required] public string TaxId { get; set; } = string.Empty;
    [Required] [EmailAddress] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string? ReferralCode { get; set; }
}

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Guid? TenantId { get; set; }
    public bool RequiresActivation { get; set; }
    public bool RequiresSetup { get; set; }
    public bool RequiresMfa { get; set; }
}

public record WebAuthnOptionsResponse(object PublicKey, string? UserHandle);
public record WebAuthnRegistrationRequest(string UserId, object Attestation);
public record WebAuthnLoginRequest(string? UserId, object Assertion);

public record LoginResult 
{ 
    public string Token { get; set; } = string.Empty; 
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public bool RequiresSetup { get; set; }
    public bool RequiresMfa { get; set; }
    public bool RequiresActivation { get; set; }
}
public record DocumentoResult { public string XmlFirmado { get; set; } = string.Empty; public string Clave { get; set; } = string.Empty; }

// Auth & Management
public record ForgotPasswordRequest(string Email);
public record ResetPasswordRequest(string Token, string NewPassword);
public record PasswordResetResponse(bool Success, string Message);
public record InviteUserRequest(string Email, string Role);

public record MensajeEndosoRequest(
    string Clave,
    string NumeroCedulaEmisor,
    DateTime FechaEmisionDoc,
    string DetalleMensaje,
    decimal MontoTotalImpuesto,
    decimal TotalFactura,
    string NumeroCedulaReceptor,
    string NumeroConsecutivoReceptor,
    int Mensaje = 4, // 1: Aceptado, 2: Parcial, 3: Rechazo, 4: Cesión
    string? NombreReceptor = null,
    string? TipoIdentificacionReceptor = null);

/// <summary>
/// Respuesta del endpoint de aceptación/endoso de documentos.
/// </summary>
public record MensajeReceptorResponse(
    bool Success,
    string Message,
    string XmlFirmado,
    HaciendaEnvioResult? HaciendaEnvio);

public record DocumentSequenceDto(
    string DocumentType,
    long CurrentValue,
    string Description);

public record UpdateSequenceRequest(
    long NewCurrentValue);

public record NextConsecutiveResponse(
    string Consecutivo,
    long CurrentValue);

// ── Simplified Document Sending (enviarHacienda) ──

/// <summary>
/// Simplified request for sending documents via enviarHacienda.
/// Server auto-fills: Emisor, Clave, Consecutivo, CodigoActividad, FechaEmision, ResumenFactura.
/// Terminal resolved via X-Terminal-Key header (or defaults to 001-00001).
/// </summary>
public class EnviarDocumentoSimplificadoRequest
{
    /// <summary>01=FE, 02=ND, 03=NC, 04=Tiquete, 08=FEC, 09=FECE</summary>
    public string TipoDocumento { get; set; } = "01";

    /// <summary>Clave numérica de 50 dígitos (opcional, se genera si no se provee)</summary>
    public string? Clave { get; set; }

    /// <summary>Consecutivo personalizado (opcional, se genera si no se provee)</summary>
    public ConsecutivoRequest? Consecutivo { get; set; }

    /// <summary>01=Contado, 02=Crédito, etc.</summary>
    public string CondicionVenta { get; set; } = "01";

    /// <summary>Medios de pago sencillos: 01=Efectivo, 02=Tarjeta, etc.</summary>
    public List<string> MedioPago { get; set; } = ["01"];

    /// <summary>Medios de pago detallados para facturas multipartes.</summary>
    public List<MedioPagoRequest>? MediosPago { get; set; }

    /// <summary>Código de moneda ISO (CRC, USD, EUR)</summary>
    public string CodigoMoneda { get; set; } = "CRC";

    /// <summary>Tipo de cambio respecto a CRC (1 si es CRC)</summary>
    public decimal TipoCambio { get; set; } = 1;

    /// <summary>Plazo de crédito en días (solo si CondicionVenta=02)</summary>
    public int? PlazoCredito { get; set; }

    /// <summary>Receptor/Cliente. Null = Consumidor Final.</summary>
    public ReceptorRequest? Receptor { get; set; }

    /// <summary>Líneas de detalle del documento (mínimo 1)</summary>
    [Required]
    public List<LineaDetalleRequest> Lineas { get; set; } = [];

    /// <summary>Referencia para NC/ND (solo clave del doc original)</summary>
    public ReferenciaSimplificada? Referencia { get; set; }

    /// <summary>Otros cargos (opcional)</summary>
    public List<OtrosCargosRequest>? OtrosCargos { get; set; }

}

/// <summary>
/// Simplified reference for credit/debit notes.
/// Server auto-fills FechaEmision, TipoDoc, etc. from the referenced document.
/// </summary>
public class ReferenciaSimplificada
{
    /// <summary>Clave numérica (50 dígitos) del documento referenciado</summary>
    [Required]
    public string ClaveDocumentoReferencia { get; set; } = string.Empty;

    /// <summary>01=Anula, 02=Corrige texto, 03=Corrige monto, 04=Referencia, 99=Otros</summary>
    public string Codigo { get; set; } = "01";

    /// <summary>Razón de la nota</summary>
    [Required]
    public string Razon { get; set; } = string.Empty;
}

// ── Branch / Terminal DTOs ──

public class BranchDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? ManagerName { get; set; }
    public bool IsActive { get; set; }
}

public class TerminalDto
{
    public Guid Id { get; set; }
    public Guid BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? LicenseKey { get; set; }
    public string? BranchName { get; set; }
    public string? BranchCode { get; set; }
}

// ── Admin & Analytics ──

public record GlobalStatsResponse(
    long TotalDocuments,
    long DocumentsLast30Days,
    decimal TotalRevenue,
    decimal RevenueLast30Days,
    int ActiveTenants,
    double GlobalSuccessRate,
    double AverageLatencySeconds);

public record AdminMeResponse(
    string Id,
    string Email,
    string Role,
    bool IsAdmin,
    List<string> Permissions);

public record PartnerProfileDto(
    Guid Id,
    string Code,
    string Type,
    string Level,
    string Status,
    decimal Balance,
    string? Website);

public record EnqueueNotificationRequest(
    string Subject,
    string Body,
    string? ToEmail = null,
    string? UserId = null,
    string? Role = null,
    bool SendEmail = true,
    bool SendPush = true,
    string TemplateName = "Default",
    Dictionary<string, string>? TemplateData = null
);

public record UnreadNotificationsResponse(
    int Count, 
    List<SystemNotificationDto> Items);

public class ApproveRequest
{
    public string? Note { get; set; }
    public Guid? OverrideTenantId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
}

public class ApprovalRejectRequest
{
    public string Reason { get; set; } = string.Empty;
}

