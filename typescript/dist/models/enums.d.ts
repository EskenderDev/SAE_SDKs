export declare enum TipoComprobante {
    FacturaElectronica = "01",
    NotaDebitoElectronica = "02",
    NotaCreditoElectronica = "03",
    TiqueteElectronico = "04",
    ConfirmacionAceptacion = "05",
    ConfirmacionAceptacionParcial = "06",
    ConfirmacionRechazo = "07",
    FacturaElectronicaCompras = "08",
    FacturaElectronicaExportacion = "09",
    ReciboElectronicoPago = "10"
}
export declare enum TipoIdentificacion {
    CedulaFisica = "01",
    CedulaJuridica = "02",
    DIMEX = "03",
    NITE = "04",
    Extranjero = "05"
}
export declare enum CondicionVenta {
    Contado = "01",
    Credito = "02",
    Consignacion = "03",
    Apartado = "04",
    ArrendamientoUso = "05",
    ArrendamientoVenta = "06",
    Otros = "99"
}
export declare enum TipoMedioPago {
    Efectivo = "01",
    Tarjeta = "02",
    Cheque = "03",
    Transferencia = "04",
    RecaudadoTerceros = "05",
    Otros = "99"
}
export declare enum CodigoMoneda {
    CRC = "CRC",
    USD = "USD",
    EUR = "EUR"
}
export declare enum CodigoImpuesto {
    IVA = "01",
    SelectivoConsumo = "02",
    ImpuestoUnicoCombustibles = "03",
    ImpuestoEspecificoBebidasAlcoholicas = "04",
    ImpuestoBebidasEnvasadas = "05",
    ImpuestoProductosTabaco = "06",
    IVADevolucion = "07",
    IVAPercepcion = "08",
    Otros = "99"
}
export declare enum HaciendaEstado {
    Pendiente = 0,
    Enviado = 1,
    Aceptado = 2,
    Rechazado = 3,
    Error = 4,
    Procesando = 5
}
export declare enum LicenseType {
    Server = "Server",
    AndroidPOS = "AndroidPOS",
    WindowsPOS = "WindowsPOS",
    Kiosk = "Kiosk",
    RestaurantPOS = "RestaurantPOS",
    RetailPOS = "RetailPOS",
    Terminal = "Terminal"
}
export declare enum LicenseStatus {
    Active = "Active",
    Revoked = "Revoked",
    Suspended = "Suspended",
    Expired = "Expired"
}
export declare enum LicenseAction {
    Issue = "Issue",
    Validate = "Validate",
    Activate = "Activate",
    Revoke = "Revoke",
    Suspend = "Suspend",
    OfflineToken = "OfflineToken",
    ValidationFailed = "ValidationFailed"
}
export declare enum LicensePlatform {
    Android = "Android",
    Windows = "Windows"
}
export declare enum LicenseAddonStatus {
    PendingPayment = "PendingPayment",
    Active = "Active",
    Cancelled = "Cancelled"
}
export declare enum ModuleType {
    Platform = 0,
    License = 1
}
