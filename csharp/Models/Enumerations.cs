namespace SAE.Sdk.Models;

/// <summary>Tipo de Comprobante Electrónico (Códigos Hacienda)</summary>
public static class TipoComprobanteExtension
{
    public const string FacturaElectronica = "01";
    public const string NotaDebitoElectronica = "02";
    public const string NotaCreditoElectronica = "03";
    public const string TiqueteElectronico = "04";
    public const string ConfirmacionAceptacion = "05";
    public const string ConfirmacionAceptacionParcial = "06";
    public const string ConfirmacionRechazo = "07";
    public const string FacturaElectronicaCompras = "08";
    public const string FacturaElectronicaExportacion = "09";
    public const string ReciboElectronicoPago = "10";
}

/// <summary>Tipo de Identificación (Códigos Hacienda)</summary>
public static class TipoIdentificacionExtension
{
    public const string CedulaFisica = "01";
    public const string CedulaJuridica = "02";
    public const string DIMEX = "03";
    public const string NITE = "04";
    public const string Extranjero = "05";
}

/// <summary>Condición de Venta (Códigos Hacienda)</summary>
public static class CondicionVentaExtension
{
    public const string Contado = "01";
    public const string Credito = "02";
    public const string Consignacion = "03";
    public const string Apartado = "04";
    public const string ArrendamientoUso = "05";
    public const string ArrendamientoVenta = "06";
    public const string Otros = "99";
}

/// <summary>Tipo de Medio de Pago (Códigos Hacienda)</summary>
public static class TipoMedioPagoExtension
{
    public const string Efectivo = "01";
    public const string Tarjeta = "02";
    public const string Cheque = "03";
    public const string Transferencia = "04";
    public const string RecaudadoTerceros = "05";
    public const string Otros = "99";
}

/// <summary>Código de Moneda (ISO 4217)</summary>
public static class CodigoMonedaExtension
{
    public const string CRC = "CRC";
    public const string USD = "USD";
    public const string EUR = "EUR";
}

/// <summary>Código de Impuesto (Códigos Hacienda)</summary>
public static class CodigoImpuestoExtension
{
    public const string IVA = "01";
    public const string SelectivoConsumo = "02";
    public const string ImpuestoUnicoCombustibles = "03";
    public const string ImpuestoEspecificoBebidasAlcoholicas = "04";
    public const string ImpuestoBebidasEnvasadas = "05";
    public const string ImpuestoProductosTabaco = "06";
    public const string IVADevolucion = "07";
    public const string IVAPercepcion = "08";
    public const string Otros = "99";
}

/// <summary>Código de Descuento (Códigos Hacienda)</summary>
public static class CodigoDescuentoExtension
{
    public const string DescuentoComercial = "01";
    public const string DescuentoCasaMatriz = "02";
    public const string DescuentoPromocional = "03";
    public const string DescuentoBonificacion = "04";
    public const string DescuentoReintegro = "05";
    public const string Otros = "99";
}

/// <summary>Código de Referencia (Códigos Hacienda)</summary>
public static class CodigoReferenciaExtension
{
    public const string AnulaDocumentoReferencia = "01";
    public const string CorrigeTextoReferencia = "02";
    public const string CorrigeMontoReferencia = "03";
    public const string ReferenciaOtroDocumento = "04";
    public const string SustituyeComprobante = "05";
    public const string Otros = "99";
}

/// <summary>Tipo de Documento de Referencia (Códigos Hacienda)</summary>
public static class TipoDocumentoReferenciaExtension
{
    public const string FacturaElectronica = "01";
    public const string NotaDebito = "02";
    public const string NotaCredito = "03";
    public const string TiqueteElectronico = "04";
    public const string FacturaElectronicaCompra = "05";
    public const string FacturaElectronicaExportacion = "06";
    public const string ComprobanteEmitidoContingencia = "07";
    public const string FacturaElectronicaExportacionContingencia = "08";
    public const string Otros = "99";
}

/// <summary>Tipo de Documento de Autorización de Exoneración (Códigos Hacienda)</summary>
public static class TipoDocumentoAutorizacionExoneracionExtension
{
    public const string CompraAutorizada = "01";
    public const string OrdenCompra = "02";
    public const string ResolucionExoneracion = "03";
    public const string ConstanciaExoneracion = "04";
    public const string Otros = "99";
}

/// <summary>Nombre de Institución de Exoneración (Códigos Hacienda)</summary>
public static class NombreInstitucionExoneracionExtension
{
    public const string IMAS = "01";
    public const string CCSS = "02";
    public const string MINSA = "03";
    public const string MEP = "04";
    public const string UCR = "05";
    public const string ITCR = "06";
    public const string UNA = "07";
    public const string UNED = "08";
    public const string Otros = "99";
}

/// <summary>Tipo de Código de Producto o Servicio (Códigos Hacienda)</summary>
public static class TipoCodigoProductoServicioExtension
{
    public const string CodigoVendedor = "01";
    public const string CodigoComprador = "02";
    public const string CodigoAsignado = "03";
    public const string CodigoUsoInterno = "04";
    public const string Otros = "99";
}

/// <summary>
/// Representa el estado de un envío a Hacienda.
/// </summary>
public enum HaciendaEstado
{
    Pendiente = 0,
    Enviado = 1,
    Aceptado = 2,
    Rechazado = 3,
    Error = 4,
    Procesando = 5
}

/// <summary>
/// Tipo de licencia para aplicaciones externas
/// </summary>
public enum LicenseType
{
    Server = 0,
    AndroidPOS = 1,
    WindowsPOS = 2,
    Kiosk = 3,
    RestaurantPOS = 10,
    RetailPOS = 11
}

/// <summary>
/// Estado de una licencia específica
/// </summary>
public enum LicenseStatus
{
    Active = 0,
    Revoked = 1,
    Suspended = 2,
    Expired = 3
}

/// <summary>
/// Acción realizada sobre una licencia para auditoría
/// </summary>
public enum LicenseAction
{
    Issue = 0,
    Validate = 1,
    Activate = 2,
    Revoke = 3,
    Suspend = 4,
    OfflineToken = 5,
    ValidationFailed = 6
}

/// <summary>
/// Plataformas soportadas para aplicaciones
/// </summary>
public enum LicensePlatform
{
    Android = 0,
    Windows = 1
}

/// <summary>
/// Estado de un paquete de licencias adicional (Add-on)
/// </summary>
public enum LicenseAddonStatus
{
    PendingPayment = 0,
    Active = 1,
    Cancelled = 2
}

/// <summary>
/// Ambiente de Hacienda (Sandbox o Producción)
/// </summary>
public enum HaciendaEnvironment
{
    /// <summary>Ambiente de pruebas (Sandbox/ATV)</summary>
    Sandbox = 0,

    /// <summary>Ambiente de producción</summary>
    Production = 1
}

/// <summary>
/// Nivel de suscripción del tenant
/// </summary>
public enum SubscriptionTier
{
    Developer = 0,
    Starter100 = 10,
    Starter250 = 11,
    Starter500 = 12,
    Professional1K = 20,
    Professional2_5K = 21,
    Professional5K = 22,
    Business10K = 30,
    Business25K = 31,
    Business50K = 32,

    // Contador Variants
    Contador10 = 40,
    Contador25 = 41,
    Contador50 = 42,

    Enterprise = 50
}

/// <summary>
/// Categoría de plan de suscripción
/// </summary>
public enum PlanCategory
{
    Standard = 0,
    Professional = 1,
    Business = 2,
    Contado = 3,
    Partner = 10
}

/// <summary>
/// Periodo de facturación
/// </summary>
public enum BillingPeriod
{
    Monthly = 1,
    Quarterly = 3,
    SemiAnnual = 6,
    Annual = 12
}

/// <summary>
/// Modelo de pago
/// </summary>
public enum PaymentModel
{
    Prepaid = 0,
    Postpaid = 1
}

/// <summary>
/// Tipo de notificación de sistema
/// </summary>
public enum NotificationType
{
    Info = 0,
    Success = 1,
    Warning = 2,
    Error = 3,
    SystemAlert = 4
}

/// <summary>
/// Estado del tenant
/// </summary>
public enum TenantStatus
{
    Active = 0,
    Suspended = 1,
    Cancelled = 2,
    Trial = 3
}
