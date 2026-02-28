package com.eskender.sae.sdk.models

import com.squareup.moshi.Json
import com.squareup.moshi.JsonClass
import java.math.BigDecimal
import java.time.OffsetDateTime
import java.util.Date

@JsonClass(generateAdapter = true)
data class GenerarDocumentoRequest(
    @Json(name = "TipoDocumento") val tipoDocumento: String,
    @Json(name = "Clave") val clave: ClaveRequest,
    @Json(name = "Consecutivo") val consecutivo: ConsecutivoRequest,
    @Json(name = "CodigoActividadEmisor") val codigoActividadEmisor: String,
    @Json(name = "CodigoActividadReceptor") val codigoActividadReceptor: String,
    @Json(name = "FechaEmision") val fechaEmision: String, // String for simplicity, or use Adapter
    @Json(name = "ProveedorSistemas") val proveedorSistemas: String,
    @Json(name = "Emisor") val emisor: EmisorRequest,
    @Json(name = "Receptor") val receptor: ReceptorRequest,
    @Json(name = "CondicionVenta") val condicionVenta: String,
    @Json(name = "CondicionVentaOtros") val condicionVentaOtros: CondicionVentaOtro? = null,
    @Json(name = "PlazoCredito") val plazoCredito: Int? = null,
    @Json(name = "DetalleServicio") val detalleServicio: DetalleServicioRequest,
    @Json(name = "ResumenFactura") val resumenFactura: ResumenFacturaRequest?,
    @Json(name = "InformacionReferencia") val informacionReferencia: List<InformacionReferenciaRequest>? = null,
    @Json(name = "OtrosCargos") val otrosCargos: List<OtrosCargosRequest>? = null,
    @Json(name = "OtrosDatos") val otrosDatos: OtrosRequest? = null
)

@JsonClass(generateAdapter = true)
data class ClaveRequest(
    @Json(name = "CodigoPais") val codigoPais: Int,
    @Json(name = "Dia") val dia: Int,
    @Json(name = "Mes") val mes: Int,
    @Json(name = "Ano") val ano: Int,
    @Json(name = "Consecutivo") val consecutivo: ConsecutivoRequest,
    @Json(name = "CedulaEmisor") val cedulaEmisor: String,
    @Json(name = "Situacion") val situacion: String
)

@JsonClass(generateAdapter = true)
data class ConsecutivoRequest(
    @Json(name = "Establecimiento") val establecimiento: Int,
    @Json(name = "Terminal") val terminal: Int,
    @Json(name = "NumeroConsecutivo") val numeroConsecutivo: Long,
    @Json(name = "TipoComprobante") val tipoComprobante: String
)

@JsonClass(generateAdapter = true)
data class IdentificacionRequest(
    @Json(name = "Tipo") val tipo: String,
    @Json(name = "Numero") val numero: String
)

@JsonClass(generateAdapter = true)
data class EmisorRequest(
    @Json(name = "Identificacion") val identificacion: IdentificacionRequest,
    @Json(name = "Nombre") val nombre: String,
    @Json(name = "NombreComercial") val nombreComercial: String? = null,
    @Json(name = "Ubicacion") val ubicacion: UbicacionRequest,
    @Json(name = "Telefono") val telefono: TelefonoRequest,
    @Json(name = "CorreoElectronico") val correoElectronico: List<String>,
    @Json(name = "RegistroFiscal8707") val registroFiscal8707: String? = null,
    @Json(name = "SeFacturanBebidasAlc") val seFacturanBebidasAlc: Boolean = false
)

@JsonClass(generateAdapter = true)
data class ReceptorRequest(
    @Json(name = "Identificacion") val identificacion: IdentificacionRequest,
    @Json(name = "Nombre") val nombre: String,
    @Json(name = "IdentificacionExtranjero") val identificacionExtranjero: String? = null,
    @Json(name = "NombreComercial") val nombreComercial: String? = null,
    @Json(name = "Ubicacion") val ubicacion: UbicacionRequest? = null,
    @Json(name = "OtrasSenasExtranjero") val otrasSenasExtranjero: String? = null,
    @Json(name = "Telefono") val telefono: TelefonoRequest? = null,
    @Json(name = "CorreoElectronico") val correoElectronico: String // Single string in C# Request
)

@JsonClass(generateAdapter = true)
data class TelefonoRequest(
    @Json(name = "CodigoPais") val codigoPais: Int,
    @Json(name = "Numero") val numero: Long
)

@JsonClass(generateAdapter = true)
data class UbicacionRequest(
    @Json(name = "Provincia") val provincia: String,
    @Json(name = "Canton") val canton: String,
    @Json(name = "Distrito") val distrito: String,
    @Json(name = "Barrio") val barrio: String,
    @Json(name = "OtrasSenas") val otrasSenas: String
)

@JsonClass(generateAdapter = true)
data class DetalleServicioRequest(
    @Json(name = "LineasDetalle") val lineasDetalle: List<LineaDetalleRequest> = emptyList()
)

@JsonClass(generateAdapter = true)
data class LineaDetalleRequest(
    @Json(name = "NumeroLinea") var numeroLinea: Int? = null,
    @Json(name = "PartidaArancelaria") val partidaArancelaria: String? = null,
    @Json(name = "CodigoCABYS") val codigoCabys: String? = null,
    @Json(name = "CodigosComerciales") val codigosComerciales: List<CodigoComercialRequest> = emptyList(),
    @Json(name = "Cantidad") val cantidad: BigDecimal,
    @Json(name = "UnidadMedida") val unidadMedida: String,
    @Json(name = "TipoTransaccion") val tipoTransaccion: String,
    @Json(name = "UnidadMedidaComercial") val unidadMedidaComercial: String? = null,
    @Json(name = "Detalle") val detalle: String? = null,
    @Json(name = "NumeroVINoSerie") val numeroVinOSerie: List<String>? = null,
    @Json(name = "RegistroMedicamento") val registroMedicamento: String? = null,
    @Json(name = "FormaFarmaceutica") val formaFarmaceutica: String? = null,
    // @Json(name = "DetalleSurtido") val detalleSurtido: DetalleSurtidoRequest? = null, // Skipping specific complex types for now if not used
    @Json(name = "PrecioUnitario") val precioUnitario: BigDecimal,
    @Json(name = "MontoTotal") val montoTotal: BigDecimal? = null,
    @Json(name = "Descuentos") val descuentos: List<DescuentoRequest> = emptyList(),
    @Json(name = "SubTotal") val subTotal: BigDecimal? = null,
    @Json(name = "IVACobradoFabrica") val ivaCobradoFabrica: String? = null,
    @Json(name = "BaseImponible") val baseImponible: BigDecimal? = null,
    @Json(name = "Impuestos") val impuestos: List<ImpuestoRequest> = emptyList(),
    @Json(name = "ImpuestoAsumidoEmisorFabrica") val impuestoAsumidoEmisorFabrica: BigDecimal? = null,
    @Json(name = "ImpuestoNeto") val impuestoNeto: BigDecimal? = null,
    @Json(name = "MontoTotalLinea") val montoTotalLinea: BigDecimal? = null
)

@JsonClass(generateAdapter = true)
data class CodigoComercialRequest(
    @Json(name = "Tipo") val tipo: String,
    @Json(name = "Codigo") val codigo: String
)

@JsonClass(generateAdapter = true)
data class DescuentoRequest(
    @Json(name = "MontoDescuento") val montoDescuento: BigDecimal?,
    @Json(name = "CodigoDescuento") val codigoDescuento: String?,
    @Json(name = "CodigoDescuentoOtro") val codigoDescuentoOtro: String?,
    @Json(name = "NaturalezaDescuento") val naturalezaDescuento: String?
)

@JsonClass(generateAdapter = true)
data class ImpuestoRequest(
    @Json(name = "Codigo") val codigo: String,
    @Json(name = "CodigoImpuestoOTRO") val codigoImpuestoOtro: String?,
    @Json(name = "CodigoTarifaIVA") val codigoTarifaIva: String,
    @Json(name = "Tarifa") val tarifa: BigDecimal?,
    @Json(name = "FactorCalculoIVA") val factorCalculoIva: BigDecimal?,
    @Json(name = "Monto") val monto: BigDecimal?,
    @Json(name = "Exoneracion") val exoneracion: ExoneracionRequest?
)

@JsonClass(generateAdapter = true)
data class ExoneracionRequest(
    @Json(name = "TipoDocumentoEX1") val tipoDocumentoEx1: String,
    @Json(name = "TipoDocumentoOTRO") val tipoDocumentoOtro: String?,
    @Json(name = "NumeroDocumento") val numeroDocumento: String,
    @Json(name = "Articulo") val articulo: Int,
    @Json(name = "Inciso") val inciso: Int,
    @Json(name = "NombreInstitucion") val nombreInstitucion: String,
    @Json(name = "NombreInstitucionOtros") val nombreInstitucionOtros: String?,
    @Json(name = "FechaEmisionEX") val fechaEmisionEx: String,
    @Json(name = "TarifaExonerada") val tarifaExonerada: BigDecimal,
    @Json(name = "MontoExoneracion") val montoExoneracion: BigDecimal
)

@JsonClass(generateAdapter = true)
data class ResumenFacturaRequest(
    @Json(name = "CodigoTipoMoneda") val codigoTipoMoneda: CodigoTipoMonedaRequest?,
    @Json(name = "TotalServGravados") val totalServGravados: BigDecimal?,
    @Json(name = "TotalServExentos") val totalServExentos: BigDecimal?,
    @Json(name = "TotalServExonerado") val totalServExonerado: BigDecimal?,
    @Json(name = "TotalServNoSujeto") val totalServNoSujeto: BigDecimal?,
    @Json(name = "TotalMercanciasGravadas") val totalMercanciasGravadas: BigDecimal?,
    @Json(name = "TotalMercanciasExentas") val totalMercanciasExentas: BigDecimal?,
    @Json(name = "TotalMercExonerada") val totalMercExonerada: BigDecimal?,
    @Json(name = "TotalMercNoSujeta") val totalMercNoSujeta: BigDecimal?,
    @Json(name = "TotalGravado") val totalGravado: BigDecimal?,
    @Json(name = "TotalExento") val totalExento: BigDecimal?,
    @Json(name = "TotalExonerado") val totalExonerado: BigDecimal?,
    @Json(name = "TotalNoSujeto") val totalNoSujeto: BigDecimal?,
    @Json(name = "TotalVenta") val totalVenta: BigDecimal?,
    @Json(name = "TotalDescuentos") val totalDescuentos: BigDecimal?,
    @Json(name = "TotalVentaNeta") val totalVentaNeta: BigDecimal?,
    @Json(name = "TotalDesgloseImpuesto") val totalDesgloseImpuesto: TotalDesgloseImpuestoRequest?,
    @Json(name = "TotalImpuesto") val totalImpuesto: BigDecimal?,
    @Json(name = "TotalImpAsumEmisorFabrica") val totalImpAsumEmisorFabrica: BigDecimal?,
    @Json(name = "TotalIvaDevuelto") val totalIvaDevuelto: BigDecimal?,
    @Json(name = "TotalOtrosCargos") val totalOtrosCargos: BigDecimal?,
    @Json(name = "TipoMedioPago") val tipoMedioPago: MedioPagoRequest?,
    @Json(name = "TotalComprobante") val totalComprobante: BigDecimal?
)

@JsonClass(generateAdapter = true)
data class CodigoTipoMonedaRequest(
    @Json(name = "CodigoMoneda") val codigoMoneda: String,
    @Json(name = "TipoCambio") val tipoCambio: BigDecimal
)

@JsonClass(generateAdapter = true)
data class TotalDesgloseImpuestoRequest(
    @Json(name = "Codigo") val codigo: String,
    @Json(name = "CodigoTarifaIVA") val codigoTarifaIva: String,
    @Json(name = "TotalMontoImpuesto") val totalMontoImpuesto: BigDecimal
)

@JsonClass(generateAdapter = true)
data class MedioPagoRequest(
    @Json(name = "TipoMedioPago") val tipoMedioPago: String?,
    @Json(name = "MedioPagoOtros") val medioPagoOtros: String?,
    @Json(name = "TotalMedioPago") val totalMedioPago: BigDecimal?
)

@JsonClass(generateAdapter = true)
data class InformacionReferenciaRequest(
    @Json(name = "TipoDocIR") val tipoDocIr: String,
    @Json(name = "TipoDocRefOTRO") val tipoDocRefOtro: String?,
    @Json(name = "Numero") val numero: String,
    @Json(name = "FechaEmisionIR") val fechaEmisionIr: String,
    @Json(name = "Codigo") val codigo: String,
    @Json(name = "CodigoReferenciaOTRO") val codigoReferenciaOtro: String?,
    @Json(name = "Razon") val razon: String
)

@JsonClass(generateAdapter = true)
data class OtrosCargosRequest(
    @Json(name = "TipoDocumentoOC") val tipoDocumentoOc: String,
    @Json(name = "TipoDocumentoOTROS") val tipoDocumentoOtros: String?,
    @Json(name = "IdentificacionTercero") val identificacionTercero: IdentificacionRequest,
    @Json(name = "NombreTercero") val nombreTercero: String,
    @Json(name = "Detalle") val detalle: String,
    @Json(name = "PorcentajeOC") val porcentajeOc: BigDecimal,
    @Json(name = "MontoCargo") val montoCargo: BigDecimal
)

@JsonClass(generateAdapter = true)
data class OtrosRequest(
    @Json(name = "OtroTexto") val otroTexto: List<String>?,
    @Json(name = "OtroContenido") val otroContenido: String?
)

@JsonClass(generateAdapter = true)
data class CondicionVentaOtro(
    @Json(name = "Descripcion") val descripcion: String
)

@JsonClass(generateAdapter = true)
data class EnviarDocumentoSimplificadoRequest(
    @Json(name = "TipoDocumento") val tipoDocumento: String = "01",
    @Json(name = "CondicionVenta") val condicionVenta: String = "01",
    @Json(name = "MedioPago") val medioPago: List<String> = listOf("01"),
    @Json(name = "MediosPago") val mediosPago: List<MedioPagoRequest>? = null,
    @Json(name = "CodigoMoneda") val codigoMoneda: String = "CRC",
    @Json(name = "TipoCambio") val tipoCambio: BigDecimal = BigDecimal.ONE,
    @Json(name = "PlazoCredito") val plazoCredito: Int? = null,
    @Json(name = "Receptor") val receptor: ReceptorRequest? = null,
    @Json(name = "Lineas") val lineas: List<LineaDetalleRequest> = emptyList(),
    @Json(name = "Referencia") val referencia: ReferenciaSimplificada? = null,
    @Json(name = "OtrosCargos") val otrosCargos: List<OtrosCargosRequest>? = null
)

@JsonClass(generateAdapter = true)
data class ReferenciaSimplificada(
    @Json(name = "ClaveDocumentoReferencia") val claveDocumentoReferencia: String,
    @Json(name = "Codigo") val codigo: String = "01",
    @Json(name = "Razon") val razon: String
)


@JsonClass(generateAdapter = true)
data class InviteUserRequest(@Json(name = "Email") val email: String, @Json(name = "Role") val role: String)

@JsonClass(generateAdapter = true)
data class HaciendaConfigRequest(
    @Json(name = "IdCia") val idCia: String? = null,
    @Json(name = "IdSucursal") val idSucursal: String? = null,
    @Json(name = "PinP12") val pinP12: String? = null,
    @Json(name = "CertP12Base64") val certP12Base64: String? = null,
    @Json(name = "UsuarioOauth") val usuarioOauth: String? = null,
    @Json(name = "PasswordOauth") val passwordOauth: String? = null,
    @Json(name = "Environment") val environment: String = "Sandbox"
)

@JsonClass(generateAdapter = true)
data class MensajeEndosoRequest(
    @Json(name = "Clave") val clave: String,
    @Json(name = "NumeroCedulaEmisor") val numeroCedulaEmisor: String,
    @Json(name = "FechaEmisionDoc") val fechaEmisionDoc: String, // ISO String
    @Json(name = "DetalleMensaje") val detalleMensaje: String,
    @Json(name = "MontoTotalImpuesto") val montoTotalImpuesto: BigDecimal,
    @Json(name = "TotalFactura") val totalFactura: BigDecimal,
    @Json(name = "NumeroCedulaReceptor") val numeroCedulaReceptor: String,
    @Json(name = "NumeroConsecutivoReceptor") val numeroConsecutivoReceptor: String,
    @Json(name = "Mensaje") val mensaje: Int = 4,
    @Json(name = "NombreReceptor") val nombreReceptor: String? = null,
    @Json(name = "TipoIdentificacionReceptor") val tipoIdentificacionReceptor: String? = null
)
