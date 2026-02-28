package com.eskender.sae.sdk.models

object TipoComprobanteExtension {
    const val FacturaElectronica = "01"
    const val NotaDebitoElectronica = "02"
    const val NotaCreditoElectronica = "03"
    const val TiqueteElectronico = "04"
    const val ConfirmacionAceptacion = "05"
    const val ConfirmacionParcial = "06"
    const val ConfirmacionRechazo = "07"
    const val FacturaElectronicaCompra = "08"
    const val FacturaElectronicaExportacion = "09"
}

object TipoIdentificacionExtension {
    const val CedulaFisica = "01"
    const val CedulaJuridica = "02"
    const val DIMEX = "03"
    const val NITE = "04"
}

object CondicionVentaExtension {
    const val Contado = "01"
    const val Credito = "02"
    const val Consignacion = "03"
    const val Apartado = "04"
    const val Arrendamiento_Con_Opcion_De_Compra = "05"
    const val Arrendamiento_En_Funcion_Financiera = "06"
    const val Cobro_A_Favor_De_Un_Tercero = "07"
    const val Servicios_Prestados_Al_Estado_A_Credito = "08"
    const val Pago_Del_Servicio_Prestado_Al_Estado = "09"
    const val Otros = "99"
}

object CodigoMonedaExtension {
    const val CRC = "CRC"
    const val USD = "USD"
}
