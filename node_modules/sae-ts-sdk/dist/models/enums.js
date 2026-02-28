"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ModuleType = exports.LicenseAddonStatus = exports.LicensePlatform = exports.LicenseAction = exports.LicenseStatus = exports.LicenseType = exports.HaciendaEstado = exports.CodigoImpuesto = exports.CodigoMoneda = exports.TipoMedioPago = exports.CondicionVenta = exports.TipoIdentificacion = exports.TipoComprobante = void 0;
// Tipo de Comprobante Electrónico (Códigos Hacienda)
var TipoComprobante;
(function (TipoComprobante) {
    TipoComprobante["FacturaElectronica"] = "01";
    TipoComprobante["NotaDebitoElectronica"] = "02";
    TipoComprobante["NotaCreditoElectronica"] = "03";
    TipoComprobante["TiqueteElectronico"] = "04";
    TipoComprobante["ConfirmacionAceptacion"] = "05";
    TipoComprobante["ConfirmacionAceptacionParcial"] = "06";
    TipoComprobante["ConfirmacionRechazo"] = "07";
    TipoComprobante["FacturaElectronicaCompras"] = "08";
    TipoComprobante["FacturaElectronicaExportacion"] = "09";
    TipoComprobante["ReciboElectronicoPago"] = "10";
})(TipoComprobante || (exports.TipoComprobante = TipoComprobante = {}));
// Tipo de Identificación (Códigos Hacienda)
var TipoIdentificacion;
(function (TipoIdentificacion) {
    TipoIdentificacion["CedulaFisica"] = "01";
    TipoIdentificacion["CedulaJuridica"] = "02";
    TipoIdentificacion["DIMEX"] = "03";
    TipoIdentificacion["NITE"] = "04";
    TipoIdentificacion["Extranjero"] = "05";
})(TipoIdentificacion || (exports.TipoIdentificacion = TipoIdentificacion = {}));
// Condición de Venta (Códigos Hacienda)
var CondicionVenta;
(function (CondicionVenta) {
    CondicionVenta["Contado"] = "01";
    CondicionVenta["Credito"] = "02";
    CondicionVenta["Consignacion"] = "03";
    CondicionVenta["Apartado"] = "04";
    CondicionVenta["ArrendamientoUso"] = "05";
    CondicionVenta["ArrendamientoVenta"] = "06";
    CondicionVenta["Otros"] = "99";
})(CondicionVenta || (exports.CondicionVenta = CondicionVenta = {}));
// Tipo de Medio de Pago (Códigos Hacienda)
var TipoMedioPago;
(function (TipoMedioPago) {
    TipoMedioPago["Efectivo"] = "01";
    TipoMedioPago["Tarjeta"] = "02";
    TipoMedioPago["Cheque"] = "03";
    TipoMedioPago["Transferencia"] = "04";
    TipoMedioPago["RecaudadoTerceros"] = "05";
    TipoMedioPago["Otros"] = "99";
})(TipoMedioPago || (exports.TipoMedioPago = TipoMedioPago = {}));
// Código de Moneda (ISO 4217)
var CodigoMoneda;
(function (CodigoMoneda) {
    CodigoMoneda["CRC"] = "CRC";
    CodigoMoneda["USD"] = "USD";
    CodigoMoneda["EUR"] = "EUR";
})(CodigoMoneda || (exports.CodigoMoneda = CodigoMoneda = {}));
// Código de Impuesto (Códigos Hacienda)
var CodigoImpuesto;
(function (CodigoImpuesto) {
    CodigoImpuesto["IVA"] = "01";
    CodigoImpuesto["SelectivoConsumo"] = "02";
    CodigoImpuesto["ImpuestoUnicoCombustibles"] = "03";
    CodigoImpuesto["ImpuestoEspecificoBebidasAlcoholicas"] = "04";
    CodigoImpuesto["ImpuestoBebidasEnvasadas"] = "05";
    CodigoImpuesto["ImpuestoProductosTabaco"] = "06";
    CodigoImpuesto["IVADevolucion"] = "07";
    CodigoImpuesto["IVAPercepcion"] = "08";
    CodigoImpuesto["Otros"] = "99";
})(CodigoImpuesto || (exports.CodigoImpuesto = CodigoImpuesto = {}));
// Estado Hacienda
var HaciendaEstado;
(function (HaciendaEstado) {
    HaciendaEstado[HaciendaEstado["Pendiente"] = 0] = "Pendiente";
    HaciendaEstado[HaciendaEstado["Enviado"] = 1] = "Enviado";
    HaciendaEstado[HaciendaEstado["Aceptado"] = 2] = "Aceptado";
    HaciendaEstado[HaciendaEstado["Rechazado"] = 3] = "Rechazado";
    HaciendaEstado[HaciendaEstado["Error"] = 4] = "Error";
    HaciendaEstado[HaciendaEstado["Procesando"] = 5] = "Procesando";
})(HaciendaEstado || (exports.HaciendaEstado = HaciendaEstado = {}));
// --- LICENCIAS ---
var LicenseType;
(function (LicenseType) {
    LicenseType["Server"] = "Server";
    LicenseType["AndroidPOS"] = "AndroidPOS";
    LicenseType["WindowsPOS"] = "WindowsPOS";
    LicenseType["Kiosk"] = "Kiosk";
    LicenseType["RestaurantPOS"] = "RestaurantPOS";
    LicenseType["RetailPOS"] = "RetailPOS";
    LicenseType["Terminal"] = "Terminal";
})(LicenseType || (exports.LicenseType = LicenseType = {}));
var LicenseStatus;
(function (LicenseStatus) {
    LicenseStatus["Active"] = "Active";
    LicenseStatus["Revoked"] = "Revoked";
    LicenseStatus["Suspended"] = "Suspended";
    LicenseStatus["Expired"] = "Expired";
})(LicenseStatus || (exports.LicenseStatus = LicenseStatus = {}));
var LicenseAction;
(function (LicenseAction) {
    LicenseAction["Issue"] = "Issue";
    LicenseAction["Validate"] = "Validate";
    LicenseAction["Activate"] = "Activate";
    LicenseAction["Revoke"] = "Revoke";
    LicenseAction["Suspend"] = "Suspend";
    LicenseAction["OfflineToken"] = "OfflineToken";
    LicenseAction["ValidationFailed"] = "ValidationFailed";
})(LicenseAction || (exports.LicenseAction = LicenseAction = {}));
var LicensePlatform;
(function (LicensePlatform) {
    LicensePlatform["Android"] = "Android";
    LicensePlatform["Windows"] = "Windows";
})(LicensePlatform || (exports.LicensePlatform = LicensePlatform = {}));
var LicenseAddonStatus;
(function (LicenseAddonStatus) {
    LicenseAddonStatus["PendingPayment"] = "PendingPayment";
    LicenseAddonStatus["Active"] = "Active";
    LicenseAddonStatus["Cancelled"] = "Cancelled";
})(LicenseAddonStatus || (exports.LicenseAddonStatus = LicenseAddonStatus = {}));
var ModuleType;
(function (ModuleType) {
    ModuleType[ModuleType["Platform"] = 0] = "Platform";
    ModuleType[ModuleType["License"] = 1] = "License";
})(ModuleType || (exports.ModuleType = ModuleType = {}));
