package com.eskender.sae.sdk.builders

import com.eskender.sae.sdk.models.*
import java.math.BigDecimal
import java.text.SimpleDateFormat
import java.util.*

class FacturaBuilder {
    private var tipoDocumento = TipoComprobanteExtension.FacturaElectronica
    private var codigoPais = 506
    private var establecimiento = 1
    private var terminal = 1
    private var consecutivo: Long = 1
    private var situacion = "1"
    private var cedulaEmisor = ""
    private var fechaEmision = Date()

    private var emisorNombre = ""
    private var emisorIdentificacionTipo = TipoIdentificacionExtension.CedulaJuridica
    private var emisorIdentificacionNumero = ""
    private var emisorCorreo = ""

    private var receptorNombre = ""
    private var receptorIdentificacionTipo = TipoIdentificacionExtension.CedulaFisica
    private var receptorIdentificacionNumero = ""
    private var receptorCorreo = ""

    private val lineas = mutableListOf<LineaDetalleRequest>()
    // private val informacionPago = mutableListOf<Triple<String, String, String>>() // Description, Identifier, Others

    fun conEmisor(nombre: String, numeroIdentificacion: String, correo: String, tipo: String = TipoIdentificacionExtension.CedulaJuridica): FacturaBuilder {
        this.emisorNombre = nombre
        this.emisorIdentificacionNumero = numeroIdentificacion
        this.emisorIdentificacionTipo = tipo
        this.emisorCorreo = correo
        this.cedulaEmisor = numeroIdentificacion
        return this
    }

    fun conReceptor(nombre: String, numeroIdentificacion: String, correo: String, tipo: String = TipoIdentificacionExtension.CedulaFisica): FacturaBuilder {
        this.receptorNombre = nombre
        this.receptorIdentificacionNumero = numeroIdentificacion
        this.receptorIdentificacionTipo = tipo
        this.receptorCorreo = correo
        return this
    }

    fun conConsecutivo(establecimiento: Int, terminal: Int, numero: Long): FacturaBuilder {
        this.establecimiento = establecimiento
        this.terminal = terminal
        this.consecutivo = numero
        return this
    }
    
    fun conTipoDocumento(tipo: String): FacturaBuilder {
        this.tipoDocumento = tipo
        return this
    }

    fun agregarLinea(
        detalle: String,
        cantidad: BigDecimal,
        precio: BigDecimal,
        codigoCabys: String,
        unidadMedida: String = "Unid",
        codigoImpuesto: String = "01", // Default IVA
        tarifaIva: BigDecimal = BigDecimal.ZERO // Default 0%
    ): FacturaBuilder {
        val linea = LineaDetalleRequest(
            numeroLinea = lineas.size + 1,
            detalle = detalle,
            cantidad = cantidad,
            precioUnitario = precio,
            codigoCabys = codigoCabys,
            unidadMedida = unidadMedida,
            tipoTransaccion = "01", // Venta normal
            impuestos = listOf(
                ImpuestoRequest(
                    codigo = codigoImpuesto,
                    codigoImpuestoOtro = null,
                    codigoTarifaIva = "01", // Validar tarifa logic
                    tarifa = tarifaIva,
                    factorCalculoIva = null,
                    monto = null,
                    exoneracion = null
                )
            )
        )
        lineas.add(linea)
        return this
    }

    fun build(): GenerarDocumentoRequest {
        // Calculate Totals
        var totalServGravados = BigDecimal.ZERO
        var totalMercGravadas = BigDecimal.ZERO
        var totalImpuesto = BigDecimal.ZERO
        var totalVenta = BigDecimal.ZERO

        lineas.forEach { linea ->
            val montoTotalLinea = linea.cantidad.multiply(linea.precioUnitario)
            // linea.montoTotal = montoTotalLinea // Update line total if not calling a setter

            var impuestoLinea = BigDecimal.ZERO
            linea.impuestos.forEach { imp ->
                imp.tarifa?.let { tarifa ->
                     impuestoLinea = impuestoLinea.add(montoTotalLinea.multiply(tarifa).divide(BigDecimal(100)))
                }
            }
            totalImpuesto = totalImpuesto.add(impuestoLinea)
            totalVenta = totalVenta.add(montoTotalLinea)
            
            // Simple logic for goods vs services
             if (linea.unidadMedida == "Unid" || linea.unidadMedida == "kg" || linea.unidadMedida == "m") {
                 totalMercGravadas = totalMercGravadas.add(montoTotalLinea)
             } else {
                 totalServGravados = totalServGravados.add(montoTotalLinea)
             }
        }

        val totalComprobante = totalVenta.add(totalImpuesto)

        val consecutivoReq = ConsecutivoRequest(establecimiento, terminal, consecutivo, tipoDocumento)
        
        // Simple manual date components
        val cal = Calendar.getInstance()
        cal.time = fechaEmision
        
        val claveReq = ClaveRequest(
            codigoPais,
            cal.get(Calendar.DAY_OF_MONTH),
            cal.get(Calendar.MONTH) + 1,
            cal.get(Calendar.YEAR) % 100,
            consecutivoReq,
            cedulaEmisor,
            situacion
        )

        val emisor = EmisorRequest(
            identificacion = IdentificacionRequest(emisorIdentificacionTipo, emisorIdentificacionNumero),
            nombre = emisorNombre,
            ubicacion = UbicacionRequest("1", "01", "01", "01", "Sin señas"),
            telefono = TelefonoRequest(506, 0),
            correoElectronico = listOf(emisorCorreo)
        )

        val receptor = ReceptorRequest(
            identificacion = IdentificacionRequest(receptorIdentificacionTipo, receptorIdentificacionNumero),
            nombre = receptorNombre,
            ubicacion = UbicacionRequest("1", "01", "01", "01", "Sin señas"),
            telefono = TelefonoRequest(506, 0),
            correoElectronico = receptorCorreo
        )
        
        val resumen = ResumenFacturaRequest(
            codigoTipoMoneda = CodigoTipoMonedaRequest(CodigoMonedaExtension.CRC, BigDecimal.ONE),
            totalServGravados = totalServGravados,
            totalServExentos = BigDecimal.ZERO,
            totalServExonerado = BigDecimal.ZERO,
            totalServNoSujeto = BigDecimal.ZERO,
            totalMercanciasGravadas = totalMercGravadas,
            totalMercanciasExentas = BigDecimal.ZERO,
            totalMercExonerada = BigDecimal.ZERO,
            totalMercNoSujeta = BigDecimal.ZERO,
            totalGravado = totalServGravados.add(totalMercGravadas),
            totalExento = BigDecimal.ZERO,
            totalExonerado = BigDecimal.ZERO,
            totalNoSujeto = BigDecimal.ZERO,
            totalVenta = totalVenta,
            totalDescuentos = BigDecimal.ZERO,
            totalVentaNeta = totalVenta,
            totalDesgloseImpuesto = null,
            totalImpuesto = totalImpuesto,
            totalImpAsumEmisorFabrica = BigDecimal.ZERO,
            totalIvaDevuelto = BigDecimal.ZERO,
            totalOtrosCargos = BigDecimal.ZERO,
            tipoMedioPago = null,
            totalComprobante = totalComprobante
        )

       // Date formatting ISO 8601
       val sdf = SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.US)
       val fechaString = sdf.format(fechaEmision)

        return GenerarDocumentoRequest(
            tipoDocumento = tipoDocumento,
            clave = claveReq,
            consecutivo = consecutivoReq,
            codigoActividadEmisor = "960900",
            codigoActividadReceptor = "960900",
            fechaEmision = fechaString,
            proveedorSistemas = "SAE_ANDROID_SDK",
            emisor = emisor,
            receptor = receptor,
            condicionVenta = CondicionVentaExtension.Contado,
            detalleServicio = DetalleServicioRequest(lineas),
            resumenFactura = resumen
        )
    }
}
