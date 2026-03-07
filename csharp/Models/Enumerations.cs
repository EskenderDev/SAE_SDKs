using System.ComponentModel;
using System.Reflection;

namespace SAE.Sdk.Models;

/// <summary>Tipo de Comprobante Electrónico (Códigos Hacienda)</summary>
public enum TipoComprobanteElectronico
{
    [Description("Factura Electrónica")] FacturaElectronica,
    [Description("Nota de Débito Electrónica")] NotaDebitoElectronica,
    [Description("Nota de Crédito Electrónica")] NotaCreditoElectronica,
    [Description("Tiquete Electrónico")] TiqueteElectronico,
    [Description("Confirmación de Aceptación")] ConfirmacionAceptacion,
    [Description("Confirmación de Aceptación Parcial")] ConfirmacionAceptacionParcial,
    [Description("Confirmación de Rechazo")] ConfirmacionRechazo,
    [Description("Factura Electrónica de Compras")] FacturaElectronicaCompras,
    [Description("Factura Electrónica de Exportación")] FacturaElectronicaExportacion,
    [Description("Recibo Electrónico de Pago")] ReciboElectronicoPago
}

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

    public static string GetCode(this TipoComprobanteElectronico value) => value switch
    {
        TipoComprobanteElectronico.FacturaElectronica => "01",
        TipoComprobanteElectronico.NotaDebitoElectronica => "02",
        TipoComprobanteElectronico.NotaCreditoElectronica => "03",
        TipoComprobanteElectronico.TiqueteElectronico => "04",
        TipoComprobanteElectronico.ConfirmacionAceptacion => "05",
        TipoComprobanteElectronico.ConfirmacionAceptacionParcial => "06",
        TipoComprobanteElectronico.ConfirmacionRechazo => "07",
        TipoComprobanteElectronico.FacturaElectronicaCompras => "08",
        TipoComprobanteElectronico.FacturaElectronicaExportacion => "09",
        TipoComprobanteElectronico.ReciboElectronicoPago => "10",
        _ => ""
    };
}

/// <summary>Tipo de Identificación (Códigos Hacienda)</summary>
public enum TipoIdentificacion
{
    [Description("Cédula Física")] CedulaFisica,
    [Description("Cédula Jurídica")] CedulaJuridica,
    [Description("NITE")] NITE,
    [Description("DIMEX")] DIMEX,
    [Description("Extranjero No Domiciliado")] ExtranjeroNoDomiciliado,
    [Description("No Contribuyente")] NoContribuyente
}

public static class TipoIdentificacionExtension
{
    public const string CedulaFisica = "01";
    public const string CedulaJuridica = "02";
    public const string DIMEX = "03";
    public const string NITE = "04";
    public const string Extranjero = "05";
    public const string NoContribuyente = "06";

    public static string GetCode(this TipoIdentificacion value) => value switch
    {
        TipoIdentificacion.CedulaFisica => "01",
        TipoIdentificacion.CedulaJuridica => "02",
        TipoIdentificacion.NITE => "03",
        TipoIdentificacion.DIMEX => "04",
        TipoIdentificacion.ExtranjeroNoDomiciliado => "05",
        TipoIdentificacion.NoContribuyente => "06",
        _ => ""
    };
}

/// <summary>Condición de Venta (Códigos Hacienda)</summary>
public enum CondicionVenta
{
    [Description("Contado")] Contado,
    [Description("Crédito")] Credito,
    [Description("Consignación")] Consignacion,
    [Description("Apartado")] Apartado,
    [Description("Arrendamiento con opción de compra")] ArrendamientoOpcionCompra,
    [Description("Arrendamiento con función financiera")] ArrendamientoFuncionFinanciera,
    [Description("Cobro a favor de un tercero")] CobroFavorTercero,
    [Description("Servicios prestados al Estado")] ServiciosPrestadosEstado,
    [Description("Pago por servicios al Estado")] PagoServicosPrestadoEstado,
    [Description("Venta a crédito IVA a 90 días")] VentaCreditoIVA90Dias,
    [Description("Pago por venta a crédito IVA a 90 días")] PagoVentaCreditoIVA90Dias,
    [Description("Venta de mercancía no nacionalizada")] VentaMercanciaNoNacionalizada,
    [Description("Venta de bienes usados a no contribuyente")] VentaBienesUsadosNoContribuyente,
    [Description("Arrendamiento operativo")] ArrendamientoOperativo,
    [Description("Arrendamiento financiero")] ArrendamientoFinanciero,
    [Description("Otro")] Otro
}

public static class CondicionVentaExtension
{
    public const string Contado = "01";
    public const string Credito = "02";
    public const string Consignacion = "03";
    public const string Apartado = "04";
    public const string ArrendamientoUso = "05";
    public const string ArrendamientoVenta = "06";
    public const string Otros = "99";

    public static string GetCode(this CondicionVenta value) => value switch
    {
        CondicionVenta.Contado => "01",
        CondicionVenta.Credito => "02",
        CondicionVenta.Consignacion => "03",
        CondicionVenta.Apartado => "04",
        CondicionVenta.ArrendamientoOpcionCompra => "05",
        CondicionVenta.ArrendamientoFuncionFinanciera => "06",
        CondicionVenta.CobroFavorTercero => "07",
        CondicionVenta.ServiciosPrestadosEstado => "08",
        CondicionVenta.PagoServicosPrestadoEstado => "09",
        CondicionVenta.VentaCreditoIVA90Dias => "10",
        CondicionVenta.PagoVentaCreditoIVA90Dias => "11",
        CondicionVenta.VentaMercanciaNoNacionalizada => "12",
        CondicionVenta.VentaBienesUsadosNoContribuyente => "13",
        CondicionVenta.ArrendamientoOperativo => "14",
        CondicionVenta.ArrendamientoFinanciero => "15",
        CondicionVenta.Otro => "99",
        _ => ""
    };
}

/// <summary>Tipo de Medio de Pago (Códigos Hacienda)</summary>
public enum TipoMedioPago
{
    [Description("Efectivo")] Efectivo,
    [Description("Tarjeta")] Tarjeta,
    [Description("Cheque")] Cheque,
    [Description("Transferencia")] Transferencia,
    [Description("Recaudado por Terceros")] RecaudadoTerceros,
    [Description("SINPE Móvil")] SinpeMovil,
    [Description("Plataforma Digital")] PlataformaDigital,
    [Description("Otro")] Otro
}

public static class TipoMedioPagoExtension
{
    public const string Efectivo = "01";
    public const string Tarjeta = "02";
    public const string Cheque = "03";
    public const string Transferencia = "04";
    public const string RecaudadoTerceros = "05";
    public const string SinpeMovil = "06";
    public const string PlataformaDigital = "07";
    public const string Otros = "99";

    public static string GetCode(this TipoMedioPago value) => value switch
    {
        TipoMedioPago.Efectivo => "01",
        TipoMedioPago.Tarjeta => "02",
        TipoMedioPago.Cheque => "03",
        TipoMedioPago.Transferencia => "04",
        TipoMedioPago.RecaudadoTerceros => "05",
        TipoMedioPago.SinpeMovil => "06",
        TipoMedioPago.PlataformaDigital => "07",
        TipoMedioPago.Otro => "99",
        _ => ""
    };
}

/// <summary>Código de Moneda (ISO 4217)</summary>
public enum CodigoMoneda
{
    [Description("Dirham de los Emiratos Árabes Unidos")] AED,
    [Description("Afghani")] AFN,
    [Description("Lek")] ALL,
    [Description("Dram armenio")] AMD,
    [Description("Florín antillano neerlandés")] ANG,
    [Description("Kwanza")] AOA,
    [Description("Peso Argentino")] ARS,
    [Description("Dólar australiano")] AUD,
    [Description("Florín arubeño")] AWG,
    [Description("Manat azerbaiyano")] AZN,
    [Description("Marco bosnioherzegovino")] BAM,
    [Description("Dólar de Barbados")] BBD,
    [Description("Taka")] BDT,
    [Description("Lev búlgaro")] BGN,
    [Description("Dinar de Bahrein")] BHD,
    [Description("Franco Burundi")] BIF,
    [Description("Dólar de Bermudas")] BMD,
    [Description("Dólar de Brunei")] BND,
    [Description("Boliviano")] BOB,
    [Description("Mvdol")] BOV,
    [Description("Real Brasileño")] BRL,
    [Description("Dólar de las Bahamas")] BSD,
    [Description("Ngultrum")] BTN,
    [Description("Pula")] BWP,
    [Description("Rublo bielorruso")] BYR,
    [Description("Dólar de Belice")] BZD,
    [Description("Dólar canadiense")] CAD,
    [Description("Franco congoleño")] CDF,
    [Description("Franco suizo")] CHF,
    [Description("Unidad de Fomento")] CLF,
    [Description("Peso chileno")] CLP,
    [Description("Yuan")] CNY,
    [Description("Peso Colombiano")] COP,
    [Description("Unidad de Valor Real")] COU,
    [Description("Colón costarricense")] CRC,
    [Description("Peso Convertible")] CUC,
    [Description("Peso Cubano")] CUP,
    [Description("Cabo Verde Escudo")] CVE,
    [Description("Corona checa")] CZK,
    [Description("Franco de Djibouti")] DJF,
    [Description("Corona danesa")] DKK,
    [Description("Peso Dominicano")] DOP,
    [Description("Dinar argelino")] DZD,
    [Description("Libra egipcia")] EGP,
    [Description("Nakfa")] ERN,
    [Description("Birr etíope")] ETB,
    [Description("Euro")] EUR,
    [Description("Dólar de Fiji")] FJD,
    [Description("Libra malvinense")] FKP,
    [Description("Libra esterlina")] GBP,
    [Description("Lari")] GEL,
    [Description("Cedi de Ghana")] GHS,
    [Description("Libra de Gibraltar")] GIP,
    [Description("Dalasi")] GMD,
    [Description("Franco guineano")] GNF,
    [Description("Quetzal")] GTQ,
    [Description("Dólar guyanés")] GYD,
    [Description("Dolar de Hong Kong")] HKD,
    [Description("Lempira")] HNL,
    [Description("Kuna")] HRK,
    [Description("Gourde")] HTG,
    [Description("Florín")] HUF,
    [Description("Rupia")] IDR,
    [Description("Nuevo Shekel Israelí")] ILS,
    [Description("Rupia india")] INR,
    [Description("Dinar iraquí")] IQD,
    [Description("Rial iraní")] IRR,
    [Description("Corona islandesa")] ISK,
    [Description("Dólar jamaiquino")] JMD,
    [Description("Dinar jordano")] JOD,
    [Description("Yen")] JPY,
    [Description("Chelín keniano")] KES,
    [Description("Som")] KGS,
    [Description("Riel")] KHR,
    [Description("Franco Comoro")] KMF,
    [Description("Won norcoreano")] KPW,
    [Description("Won")] KRW,
    [Description("Dinar kuwaití")] KWD,
    [Description("Dólar de las Islas Caimán")] KYD,
    [Description("Tenge")] KZT,
    [Description("Kip")] LAK,
    [Description("Libra libanesa")] LBP,
    [Description("Rupia de Sri Lanka")] LKR,
    [Description("Dólar liberiano")] LRD,
    [Description("Loti")] LSL,
    [Description("Dinar libio")] LYD,
    [Description("Dirham marroquí")] MAD,
    [Description("Leu moldavo")] MDL,
    [Description("Ariary malgache")] MGA,
    [Description("Denar")] MKD,
    [Description("Kyat")] MMK,
    [Description("Tugrik")] MNT,
    [Description("Pataca")] MOP,
    [Description("Ouguiya")] MRO,
    [Description("Rupia de Mauricio")] MUR,
    [Description("Rufiyaa")] MVR,
    [Description("Kwacha")] MWK,
    [Description("Peso Mexicano")] MXN,
    [Description("Unidad de Inversion Mexicana (UDI)")] MXV,
    [Description("Ringgit malayo")] MYR,
    [Description("Metical mozambiqueño")] MZN,
    [Description("Dólar de Namibia")] NAD,
    [Description("Naira")] NGN,
    [Description("Cordoba Oro")] NIO,
    [Description("Corona noruega")] NOK,
    [Description("Rupia nepalí")] NPR,
    [Description("Dólar neozelandés")] NZD,
    [Description("Rial omaní")] OMR,
    [Description("Balboa")] PAB,
    [Description("Nuevo Sol")] PEN,
    [Description("Kina")] PGK,
    [Description("Peso filipino")] PHP,
    [Description("Rupia pakistaní")] PKR,
    [Description("Zloty")] PLN,
    [Description("Guaraní")] PYG,
    [Description("Riyal catarí")] QAR,
    [Description("Leu rumano")] RON,
    [Description("Dinar serbio")] RSD,
    [Description("Rublo ruso")] RUB,
    [Description("Franco ruandés")] RWF,
    [Description("Riyal saudí")] SAR,
    [Description("Dólar de las Islas Salomón")] SBD,
    [Description("Rupia seychelense")] SCR,
    [Description("Libra sudanesa")] SDG,
    [Description("Corona sueca")] SEK,
    [Description("Dolar de Singapur")] SGD,
    [Description("Libra de Santa Helena")] SHP,
    [Description("Leone")] SLL,
    [Description("Chelín somalí")] SOS,
    [Description("Dólar surinamés")] SRD,
    [Description("Libra sursudanesa")] SSP,
    [Description("Dobra")] STD,
    [Description("Colón")] SVC,
    [Description("Libra Siria")] SYP,
    [Description("Lilangeni")] SZL,
    [Description("Baht")] THB,
    [Description("Somoni")] TJS,
    [Description("Manat turcomano")] TMT,
    [Description("Dinar tunecino")] TND,
    [Description("Pa'anga")] TOP,
    [Description("Lira turca")] TRY,
    [Description("Dólar trinitense")] TTD,
    [Description("Nuevo dólar taiwanés")] TWD,
    [Description("Chelín tanzano")] TZS,
    [Description("Hryvnia")] UAH,
    [Description("Chelín ugandés")] UGX,
    [Description("Dólar estadounidense")] USD,
    [Description("Dólar Americanó (Next day)")] USN,
    [Description("Uruguay Peso en Unidades Indexadas (URUIURUI)")] UYI,
    [Description("Peso Uruguayo")] UYU,
    [Description("Som uzbeko")] UZS,
    [Description("Bolívar")] VEF,
    [Description("Dong")] VND,
    [Description("Vatu")] VUV,
    [Description("Tala")] WST,
    [Description("Franco CFA BEAC")] XAF,
    [Description("Dólar del Caribe Oriental")] XCD,
    [Description("SDR (Derechos Especiales de Giro)")] XDR,
    [Description("Franco CFA BCEAO")] XOF,
    [Description("Franco CFP")] XPF,
    [Description("Sucre")] XSU,
    [Description("Unidad de cuenta del BAD")] XUA,
    [Description("Rial yemení")] YER,
    [Description("Rand")] ZAR,
    [Description("Kwacha zambiano")] ZMW,
    [Description("Dólar zimbabuense")] ZWL
}

public static class CodigoMonedaExtension
{
    public const string CRC = "CRC";
    public const string USD = "USD";
    public const string EUR = "EUR";

    public static string GetCode(this CodigoMoneda value) => value.ToString();
}

/// <summary>Código de Impuesto (Códigos Hacienda)</summary>
public enum CodigoImpuesto
{
    [Description("Impuesto al Valor Agregado")] ImpuestoValorAgregado,
    [Description("Impuesto Selectivo de Consumo")] ImpuestoSelectivoConsumo,
    [Description("Impuesto Único a los Combustibles")] ImpuestoUnicoCombustibles,
    [Description("Impuesto Específico a Bebidas Alcohólicas")] ImpuestoEspecificoBebidasAlcoholicas,
    [Description("Impuesto Específico a Bebidas Envasadas sin Alcohol y Jabones")] ImpuestoEspecificoBebidasEnvasadasSinAlcoholYJabones,
    [Description("Impuesto a Productos de Tabaco")] ImpuestoProductosTabaco,
    [Description("IVA con Cálculo Especial")] ImpuestoIVACalculoEspecial,
    [Description("IVA Régimen Bienes Usados - Factor")] IVARegimenBienesUsadosFactor,
    [Description("Impuesto Específico al Cemento")] ImpuestoEspecificoCemento,
    [Description("Otros impuestos")] Otros
}

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

    public static string GetCode(this CodigoImpuesto value) => value switch
    {
        CodigoImpuesto.ImpuestoValorAgregado => "01",
        CodigoImpuesto.ImpuestoSelectivoConsumo => "02",
        CodigoImpuesto.ImpuestoUnicoCombustibles => "03",
        CodigoImpuesto.ImpuestoEspecificoBebidasAlcoholicas => "04",
        CodigoImpuesto.ImpuestoEspecificoBebidasEnvasadasSinAlcoholYJabones => "05",
        CodigoImpuesto.ImpuestoProductosTabaco => "06",
        CodigoImpuesto.ImpuestoIVACalculoEspecial => "07",
        CodigoImpuesto.IVARegimenBienesUsadosFactor => "08",
        CodigoImpuesto.ImpuestoEspecificoCemento => "12",
        CodigoImpuesto.Otros => "99",
        _ => ""
    };
}

/// <summary>Código de Descuento (Códigos Hacienda)</summary>
public enum CodigoDescuento
{
    [Description("Descuento por regalía")] DescuentoRegalia,
    [Description("Descuento por regalía con IVA cobrado al cliente")] DescuentoRegaliaIVACobradoCliente,
    [Description("Descuento por bonificación")] DescuentoBonificacion,
    [Description("Descuento por volumen")] DescuentoVolumen,
    [Description("Descuento por temporada")] DescuentoTemporada,
    [Description("Descuento promocional")] DescuentoPromocional,
    [Description("Descuento comercial")] DescuentoComercial,
    [Description("Descuento por frecuencia")] DescuentoFrecuencia,
    [Description("Descuento sostenido")] DescuentoSostenido,
    [Description("Otro tipo de descuento")] Otro
}

public static class CodigoDescuentoExtension
{
    public const string DescuentoComercial = "01";
    public const string DescuentoCasaMatriz = "02";
    public const string DescuentoPromocional = "03";
    public const string DescuentoBonificacion = "04";
    public const string DescuentoReintegro = "05";
    public const string Otros = "99";

    public static string GetCode(this CodigoDescuento value) => value switch
    {
        CodigoDescuento.DescuentoRegalia => "01",
        CodigoDescuento.DescuentoRegaliaIVACobradoCliente => "02",
        CodigoDescuento.DescuentoBonificacion => "03",
        CodigoDescuento.DescuentoVolumen => "04",
        CodigoDescuento.DescuentoTemporada => "05",
        CodigoDescuento.DescuentoPromocional => "06",
        CodigoDescuento.DescuentoComercial => "07",
        CodigoDescuento.DescuentoFrecuencia => "08",
        CodigoDescuento.DescuentoSostenido => "09",
        CodigoDescuento.Otro => "99",
        _ => ""
    };
}

/// <summary>Código de Referencia (Códigos Hacienda)</summary>
public enum CodigoReferencia
{
    [Description("Anula documento de referencia")] AnulaDocumentoReferencia,
    [Description("Corrige monto")] CorrigeMonto,
    [Description("Corrige texto de documento de referencia")] CorrigeTextoDocumentoReferencia,
    [Description("Referencia otro documento")] ReferenciaOtroDocumento,
    [Description("Sustituye comprobante provisional por contingencia")] SustituyeComprobanteProvisionalContingencia,
    [Description("Devolución de mercancía")] DevolucionMercancia,
    [Description("Sustituye comprobante electrónico")] SustituyeComprobanteElectronico,
    [Description("Factura endosada")] FacturaEndosada,
    [Description("Nota de crédito financiera")] NotaCreditoFinanciera,
    [Description("Nota de débito financiera")] NotaDebitoFinanciera,
    [Description("Proveedor no domiciliado")] ProveedorNoDomiciliado,
    [Description("Crédito por exoneración posterior a la facturación")] CreditoExoneracionPosteriorFacturacion,
    [Description("Otros")] Otros
}

public static class CodigoReferenciaExtension
{
    public const string AnulaDocumentoReferencia = "01";
    public const string CorrigeTextoReferencia = "02";
    public const string CorrigeMontoReferencia = "03";
    public const string ReferenciaOtroDocumento = "04";
    public const string SustituyeComprobante = "05";
    public const string Otros = "99";

    public static string GetCode(this CodigoReferencia value) => value switch
    {
        CodigoReferencia.AnulaDocumentoReferencia => "01",
        CodigoReferencia.CorrigeMonto => "02",
        CodigoReferencia.CorrigeTextoDocumentoReferencia => "03",
        CodigoReferencia.ReferenciaOtroDocumento => "04",
        CodigoReferencia.SustituyeComprobanteProvisionalContingencia => "05",
        CodigoReferencia.DevolucionMercancia => "06",
        CodigoReferencia.SustituyeComprobanteElectronico => "07",
        CodigoReferencia.FacturaEndosada => "08",
        CodigoReferencia.NotaCreditoFinanciera => "09",
        CodigoReferencia.NotaDebitoFinanciera => "10",
        CodigoReferencia.ProveedorNoDomiciliado => "11",
        CodigoReferencia.CreditoExoneracionPosteriorFacturacion => "12",
        CodigoReferencia.Otros => "99",
        _ => ""
    };
}

/// <summary>Tipo de Documento de Referencia (Códigos Hacienda)</summary>
public enum TipoDocumentoReferencia
{
    [Description("Factura Electrónica")] FacturaElectronica,
    [Description("Nota de Débito Electrónica")] NotaDebitoElectronica,
    [Description("Nota de Crédito Electrónica")] NotaCreditoElectronica,
    [Description("Tiquete Electrónico")] TiqueteElectronico,
    [Description("Nota de Despacho")] NotaDespacho,
    [Description("Contrato")] Contrato,
    [Description("Procedimiento")] Procedimiento,
    [Description("Comprobante Emitido en Contingencia")] ComprobanteEmitidoContingencia,
    [Description("Devolución de Mercadería")] DevolucionMercaderia,
    [Description("Comprobante Rechazado por Hacienda")] ComprobanteRechazadoHacienda,
    [Description("Sustituye Factura Rechazada por Receptor")] SustituyeFacturaRechazadaPorReceptor,
    [Description("Sustituye Factura de Exportación")] SustituyeFacturaExportacion,
    [Description("Facturación Mes Vencido")] FacturacionMesVencido,
    [Description("Comprobante Aportado Régimen Especial")] ComprobanteAportadoRegimenEspecial,
    [Description("Sustituye Factura Electrónica de Compra")] SustituyeFacturaElectronicaCompra,
    [Description("Comprobante Proveedor No Domiciliado")] ComprobanteProveedorNoDomiciliado,
    [Description("Nota de Crédito Factura Electrónica de Compra")] NotaCreditoFacturaElectronicaCompra,
    [Description("Nota de Débito Factura Electrónica de Compra")] NotaDebitoFacturaElectronicaCompra,
    [Description("Otros")] Otros
}

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

    public static string GetCode(this TipoDocumentoReferencia value) => value switch
    {
        TipoDocumentoReferencia.FacturaElectronica => "01",
        TipoDocumentoReferencia.NotaDebitoElectronica => "02",
        TipoDocumentoReferencia.NotaCreditoElectronica => "03",
        TipoDocumentoReferencia.TiqueteElectronico => "04",
        TipoDocumentoReferencia.NotaDespacho => "05",
        TipoDocumentoReferencia.Contrato => "06",
        TipoDocumentoReferencia.Procedimiento => "07",
        TipoDocumentoReferencia.ComprobanteEmitidoContingencia => "08",
        TipoDocumentoReferencia.DevolucionMercaderia => "09",
        TipoDocumentoReferencia.ComprobanteRechazadoHacienda => "10",
        TipoDocumentoReferencia.SustituyeFacturaRechazadaPorReceptor => "11",
        TipoDocumentoReferencia.SustituyeFacturaExportacion => "12",
        TipoDocumentoReferencia.FacturacionMesVencido => "13",
        TipoDocumentoReferencia.ComprobanteAportadoRegimenEspecial => "14",
        TipoDocumentoReferencia.SustituyeFacturaElectronicaCompra => "15",
        TipoDocumentoReferencia.ComprobanteProveedorNoDomiciliado => "16",
        TipoDocumentoReferencia.NotaCreditoFacturaElectronicaCompra => "17",
        TipoDocumentoReferencia.NotaDebitoFacturaElectronicaCompra => "18",
        TipoDocumentoReferencia.Otros => "99",
        _ => ""
    };
}

/// <summary>Tipo de Documento de Autorización de Exoneración (Códigos Hacienda)</summary>
public enum TipoDocumentoAutorizacionExoneracion
{
    [Description("Compras autorizadas por la DGT")] ComprasAutorizadasDGT,
    [Description("Ventas exentas a diplomáticos")] VentasExentasDiplomaticos,
    [Description("Autorizado por ley especial")] AutorizadoLeyEspecial,
    [Description("Exenciones DGH local genérica")] ExencionesDGHLocalGenerica,
    [Description("Exenciones DGH transitorio V")] ExencionesDGHTransitorioV,
    [Description("Servicios turísticos inscritos en ICT")] ServiciosTuristicosInscritosICT,
    [Description("Transitorio XVII reciclaje")] TransitorioXVIIReciclaje,
    [Description("Exoneración zona franca")] ExoneracionZonaFranca,
    [Description("Exoneración servicios complementarios exportación")] ExoneracionServiciosComplementariosExportacion,
    [Description("Órganos y corporaciones municipales")] OrganosCorporacionesMunicipales,
    [Description("Exenciones DGH impuesto local concreta")] ExencionesDGHImpuestoLocalConcreta,
    [Description("Otros")] Otros
}

public static class TipoDocumentoAutorizacionExoneracionExtension
{
    public const string CompraAutorizada = "01";
    public const string OrdenCompra = "02";
    public const string ResolucionExoneracion = "03";
    public const string ConstanciaExoneracion = "04";
    public const string Otros = "99";

    public static string GetCode(this TipoDocumentoAutorizacionExoneracion value) => value switch
    {
        TipoDocumentoAutorizacionExoneracion.ComprasAutorizadasDGT => "01",
        TipoDocumentoAutorizacionExoneracion.VentasExentasDiplomaticos => "02",
        TipoDocumentoAutorizacionExoneracion.AutorizadoLeyEspecial => "03",
        TipoDocumentoAutorizacionExoneracion.ExencionesDGHLocalGenerica => "04",
        TipoDocumentoAutorizacionExoneracion.ExencionesDGHTransitorioV => "05",
        TipoDocumentoAutorizacionExoneracion.ServiciosTuristicosInscritosICT => "06",
        TipoDocumentoAutorizacionExoneracion.TransitorioXVIIReciclaje => "07",
        TipoDocumentoAutorizacionExoneracion.ExoneracionZonaFranca => "08",
        TipoDocumentoAutorizacionExoneracion.ExoneracionServiciosComplementariosExportacion => "09",
        TipoDocumentoAutorizacionExoneracion.OrganosCorporacionesMunicipales => "10",
        TipoDocumentoAutorizacionExoneracion.ExencionesDGHImpuestoLocalConcreta => "11",
        TipoDocumentoAutorizacionExoneracion.Otros => "99",
        _ => ""
    };
}

/// <summary>Nombre de Institución de Exoneración (Códigos Hacienda)</summary>
public enum NombreInstitucionExoneracion
{
    [Description("Ministerio de Hacienda")] MinisterioHacienda,
    [Description("Ministerio de Relaciones Exteriores y Culto")] MinisterioRelacionesExterioresCulto,
    [Description("MAG")] MAG,
    [Description("Ministerio de Economía, Industria y Comercio")] MinisterioEconomiaIndustriaComercio,
    [Description("Cruz Roja")] CruzRoja,
    [Description("Bomberos")] Bomberos,
    [Description("Asociación Obras Espíritu Santo")] AsociacionObrasEspirituSanto,
    [Description("FECRUNAPA")] Fecrunapa,
    [Description("EARTH")] EARTH,
    [Description("INCAE")] INCAE,
    [Description("JPS")] JPS,
    [Description("ARESEP")] Aresep,
    [Description("Otros")] Otros
}

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

    public static string GetCode(this NombreInstitucionExoneracion value) => value switch
    {
        NombreInstitucionExoneracion.MinisterioHacienda => "01",
        NombreInstitucionExoneracion.MinisterioRelacionesExterioresCulto => "02",
        NombreInstitucionExoneracion.MAG => "03",
        NombreInstitucionExoneracion.MinisterioEconomiaIndustriaComercio => "04",
        NombreInstitucionExoneracion.CruzRoja => "05",
        NombreInstitucionExoneracion.Bomberos => "06",
        NombreInstitucionExoneracion.AsociacionObrasEspirituSanto => "07",
        NombreInstitucionExoneracion.Fecrunapa => "08",
        NombreInstitucionExoneracion.EARTH => "09",
        NombreInstitucionExoneracion.INCAE => "10",
        NombreInstitucionExoneracion.JPS => "11",
        NombreInstitucionExoneracion.Aresep => "12",
        NombreInstitucionExoneracion.Otros => "99",
        _ => ""
    };
}

/// <summary>Tipo de Código de Producto o Servicio (Códigos Hacienda)</summary>
public enum TipoCodigoProductoServicio
{
    [Description("Código vendedor")] CodigoVendedor,
    [Description("Código comprador")] CodigoComprador,
    [Description("Código industria")] CodigoIndustria,
    [Description("Código uso interno")] CodigoUsoInterno,
    [Description("Otros")] Otros
}

public static class TipoCodigoProductoServicioExtension
{
    public const string CodigoVendedor = "01";
    public const string CodigoComprador = "02";
    public const string CodigoAsignado = "03";
    public const string CodigoUsoInterno = "04";
    public const string Otros = "99";

    public static string GetCode(this TipoCodigoProductoServicio value) => value switch
    {
        TipoCodigoProductoServicio.CodigoVendedor => "01",
        TipoCodigoProductoServicio.CodigoComprador => "02",
        TipoCodigoProductoServicio.CodigoIndustria => "03",
        TipoCodigoProductoServicio.CodigoUsoInterno => "04",
        TipoCodigoProductoServicio.Otros => "99",
        _ => ""
    };
}

/// <summary>Tarifa del IVA (Códigos Hacienda)</summary>
public enum TarifaIVA
{
    [Description("Tarifa 0%")] Tarifa0PorCiento,
    [Description("Tarifa reducida 1%")] TarifaReducida1PorCiento,
    [Description("Tarifa reducida 2%")] TarifaReducida2PorCiento,
    [Description("Tarifa reducida 4%")] TarifaReducida4PorCiento,
    [Description("Tarifa transitorio 0%")] TarifaTransitorio0PorCiento,
    [Description("Tarifa transitorio 4%")] TarifaTransitorio4PorCiento,
    [Description("Tarifa transitoria 8%")] TarifaTransitoria8PorCiento,
    [Description("Tarifa general 13%")] TarifaGeneral13PorCiento,
    [Description("Tarifa reducida 0.5%")] TarifaReducida0Punto5PorCiento,
    [Description("Tarifa exenta")] TarifaExenta,
    [Description("Tarifa 0% sin crédito")] Tarifa0PorCientoSinCredito
}

public static class TarifaIVAExtension
{
    public static string GetCode(this TarifaIVA value) => value switch
    {
        TarifaIVA.Tarifa0PorCiento => "01",
        TarifaIVA.TarifaReducida1PorCiento => "02",
        TarifaIVA.TarifaReducida2PorCiento => "03",
        TarifaIVA.TarifaReducida4PorCiento => "04",
        TarifaIVA.TarifaTransitorio0PorCiento => "05",
        TarifaIVA.TarifaTransitorio4PorCiento => "06",
        TarifaIVA.TarifaTransitoria8PorCiento => "07",
        TarifaIVA.TarifaGeneral13PorCiento => "08",
        TarifaIVA.TarifaReducida0Punto5PorCiento => "09",
        TarifaIVA.TarifaExenta => "10",
        TarifaIVA.Tarifa0PorCientoSinCredito => "11",
        _ => ""
    };

    public static decimal GetPercentage(this TarifaIVA value) => value switch
    {
        TarifaIVA.Tarifa0PorCiento => 0m,
        TarifaIVA.TarifaReducida1PorCiento => 1m,
        TarifaIVA.TarifaReducida2PorCiento => 2m,
        TarifaIVA.TarifaReducida4PorCiento => 4m,
        TarifaIVA.TarifaTransitorio0PorCiento => 0m,
        TarifaIVA.TarifaTransitorio4PorCiento => 4m,
        TarifaIVA.TarifaTransitoria8PorCiento => 8m,
        TarifaIVA.TarifaGeneral13PorCiento => 13m,
        TarifaIVA.TarifaReducida0Punto5PorCiento => 0.5m,
        TarifaIVA.TarifaExenta => 0m,
        TarifaIVA.Tarifa0PorCientoSinCredito => 0m,
        _ => 0m
    };
}

/// <summary>Condición del Impuesto (Códigos Hacienda)</summary>
public enum CondicionImpuesto
{
    [Description("Genera crédito fiscal del IVA")] GeneraCreditoIVA,
    [Description("Crédito parcial del IVA")] CreditoParcialIVA,
    [Description("Bienes de capital")] BienesCapital,
    [Description("Gasto corriente")] GastoCorriente,
    [Description("Proporcionalidad")] Proporcionalidad
}

public static class CondicionImpuestoExtension
{
    public static string GetCode(this CondicionImpuesto value) => value switch
    {
        CondicionImpuesto.GeneraCreditoIVA => "01",
        CondicionImpuesto.CreditoParcialIVA => "02",
        CondicionImpuesto.BienesCapital => "03",
        CondicionImpuesto.GastoCorriente => "04",
        CondicionImpuesto.Proporcionalidad => "05",
        _ => ""
    };
}

/// <summary>IVA Cobrado a Nivel de Fábrica (Códigos Hacienda)</summary>
public enum IVACobradoNivelFabrica
{
    [Description("Venta de bienes")] VentaBienes,
    [Description("Venta exenta")] VentaExenta
}

public static class IVACobradoNivelFabricaExtension
{
    public static string GetCode(this IVACobradoNivelFabrica value) => value switch
    {
        IVACobradoNivelFabrica.VentaBienes => "01",
        IVACobradoNivelFabrica.VentaExenta => "02",
        _ => ""
    };
}

/// <summary>Situación de Comprobante Electrónico (Códigos Hacienda)</summary>
public enum SituacionComprobante
{
    [Description("Normal")] Normal = 1,
    [Description("Contingencia")] Contigencia = 2,
    [Description("Sin Internet")] SinInternet = 3
}

public static class SituacionComprobanteExtension
{
    public static string GetCode(this SituacionComprobante value) => ((int)value).ToString();
}

/// <summary>Unidades de Medida (Códigos Hacienda)</summary>
public enum UnidadMedida
{
    [Description("Uno")] Uno,
    [Description("minuto")] minuto,
    [Description("segundo")] segundo,
    [Description("Grado Celsius")] GradoCelsius,
    [Description("Uno por Metro")] UnoPorMetro,
    [Description("Ampere")] Ampere,
    [Description("Ampere por Metro")] AmperePorMetro,
    [Description("Ampere por Metro Cuadrado")] AmperePorMetroCuadrado,
    [Description("Activo Virtual")] ActivoVirtual,
    [Description("Alquiler Habitacional")] AlquilerHabitacional,
    [Description("Alquiler Comercial")] AlquilerComercial,
    [Description("Bel")] Bel,
    [Description("Becquerel")] Becquerel,
    [Description("Coulomb")] Coulomb,
    [Description("Coulomb por Kilogramo")] CoulombPorKilogramo,
    [Description("Coulomb por Metro Cuadrado")] CoulombPorMetroCuadrado,
    [Description("Coulomb por Metro Cúbico")] CoulombPorMetroCubico,
    [Description("Cajuela de Café")] CajuelaCafe,
    [Description("Candela")] Candela,
    [Description("Candela por Metro Cuadrado")] CandelaPorMetroCuadrado,
    [Description("Comisión")] Comision,
    [Description("Centímetro")] Centimetro,
    [Description("Cuartillo de Café")] CuartilloCafe,
    [Description("Día")] Dia,
    [Description("Electronvolt")] Electronvolt,
    [Description("Farad")] Farad,
    [Description("Farad por Metro")] FaradPorMetro,
    [Description("Fanega de Café")] FanegaCafe,
    [Description("Gramo")] Gramo,
    [Description("Galón")] Galon,
    [Description("Gray")] Gray,
    [Description("Gray por Segundo")] GrayPorSegundo,
    [Description("Hora")] Hora,
    [Description("Henry")] Henry,
    [Description("Henry por Metro")] HenryPorMetro,
    [Description("Hertz")] Hertz,
    [Description("Intereses")] Intereses,
    [Description("Joule")] Joule,
    [Description("Joule por Kilogramo Kelvin")] JoulePorKilogramoKelvin,
    [Description("Joule por Mol Kelvin")] JoulePorMolKelvin,
    [Description("Joule por Kelvin")] JoulePorKelvin,
    [Description("Joule por Kilogramo")] JoulePorKilogramo,
    [Description("Joule por Metro Cúbico")] JoulePorMetroCubico,
    [Description("Joule por Mol")] JoulePorMol,
    [Description("Kelvin")] Kelvin,
    [Description("Katal")] Katal,
    [Description("Katal por Metro Cúbico")] KatalPorMetroCubico,
    [Description("Kilogramo")] Kilogramo,
    [Description("Kilogramo por Metro Cúbico")] KilogramoPorMetroCubico,
    [Description("Kilómetro")] Kilometro,
    [Description("Kilovatios")] Kilovatios,
    [Description("Kilovatios Hora")] KilovatiosHora,
    [Description("Litro")] Litro,
    [Description("Lumen")] Lumen,
    [Description("Pulgada")] Pulgada,
    [Description("Lux")] Lux,
    [Description("Metro")] Metro,
    [Description("Metro por Segundo")] MetroPorSegundo,
    [Description("Metro por Segundo Cuadrado")] MetroPorSegundoCuadrado,
    [Description("Metro Cuadrado")] MetroCuadrado,
    [Description("Metro Cúbico")] MetroCubico,
    [Description("Minuto")] Minuto,
    [Description("Mililitro")] Mililitro,
    [Description("Milímetro")] Milimetro,
    [Description("Mol")] Mol,
    [Description("Mol por Metro Cúbico")] MolPorMetroCubico,
    [Description("Newton")] Newton,
    [Description("Newton por Metro")] NewtonPorMetro,
    [Description("Newton Metro")] NewtonMetro,
    [Description("Neper")] Neper,
    [Description("Grado")] Grado,
    [Description("Otro Servicio")] OtroServicio,
    [Description("Otros")] Otros,
    [Description("Onzas")] Onzas,
    [Description("Pascal")] Pascal,
    [Description("Pascal Segundo")] PascalSegundo,
    [Description("Quintal")] Quintal,
    [Description("Radián")] Radian,
    [Description("Radián por Segundo")] RadianPorSegundo,
    [Description("Radián por Segundo Cuadrado")] RadianPorSegundoCuadrado,
    [Description("Segundo")] Segundo,
    [Description("Siemens")] Siemens,
    [Description("Servicios Profesionales")] ServiciosProfesionales,
    [Description("Servicios Personales")] ServiciosPersonales,
    [Description("Estereorradián")] Estereorradian,
    [Description("Servicios Técnicos")] ServiciosTecnicos,
    [Description("Sievert")] Sievert,
    [Description("Tesla")] Tesla,
    [Description("Tonelada")] Tonelada,
    [Description("Unidad de Masa Atómica")] UnidadMasaAtomica,
    [Description("Unidad Astronómica")] UnidadAstronomica,
    [Description("Unidad")] Unidad,
    [Description("Volt")] Volt,
    [Description("Volt por Metro")] VoltPorMetro,
    [Description("Watt")] Watt,
    [Description("Watt por Metro Kelvin")] WattPorMetroKelvin,
    [Description("Watt por Metro Cuadrado Estereorradián")] WattPorMetroCuadradoEstereorradian,
    [Description("Watt por Metro Cuadrado")] WattPorMetroCuadrado,
    [Description("Watt por Estereorradián")] WattPorEstereorradian,
    [Description("Weber")] Weber,
    [Description("Ohm")] Ohm
}

public static class UnidadMedidaExtension
{
    public static string GetCode(this UnidadMedida value) => value switch
    {
        UnidadMedida.Uno => "1",
        UnidadMedida.minuto => "´",
        UnidadMedida.segundo => "´´",
        UnidadMedida.GradoCelsius => "°C",
        UnidadMedida.UnoPorMetro => "1/m",
        UnidadMedida.Ampere => "A",
        UnidadMedida.AmperePorMetro => "A/m",
        UnidadMedida.AmperePorMetroCuadrado => "A/m²",
        UnidadMedida.ActivoVirtual => "Acv",
        UnidadMedida.AlquilerHabitacional => "Al",
        UnidadMedida.AlquilerComercial => "Alc",
        UnidadMedida.Bel => "B",
        UnidadMedida.Becquerel => "Bq",
        UnidadMedida.Coulomb => "C",
        UnidadMedida.CoulombPorKilogramo => "C/kg",
        UnidadMedida.CoulombPorMetroCuadrado => "C/m²",
        UnidadMedida.CoulombPorMetroCubico => "C/m³",
        UnidadMedida.CajuelaCafe => "Cc",
        UnidadMedida.Candela => "Cd",
        UnidadMedida.CandelaPorMetroCuadrado => "cd/m²",
        UnidadMedida.Comision => "Cm",
        UnidadMedida.Centimetro => "cm",
        UnidadMedida.CuartilloCafe => "Cu",
        UnidadMedida.Dia => "D",
        UnidadMedida.Electronvolt => "eV",
        UnidadMedida.Farad => "F",
        UnidadMedida.FaradPorMetro => "F/m",
        UnidadMedida.FanegaCafe => "Fa",
        UnidadMedida.Gramo => "G",
        UnidadMedida.Galon => "Gal",
        UnidadMedida.Gray => "Gy",
        UnidadMedida.GrayPorSegundo => "Gy/s",
        UnidadMedida.Hora => "H",
        UnidadMedida.Henry => "h",
        UnidadMedida.HenryPorMetro => "H/m",
        UnidadMedida.Hertz => "Hz",
        UnidadMedida.Intereses => "I",
        UnidadMedida.Joule => "J",
        UnidadMedida.JoulePorKilogramoKelvin => "J/(kg·K)",
        UnidadMedida.JoulePorMolKelvin => "J/(mol·K)",
        UnidadMedida.JoulePorKelvin => "J/K",
        UnidadMedida.JoulePorKilogramo => "J/kg",
        UnidadMedida.JoulePorMetroCubico => "J/m³",
        UnidadMedida.JoulePorMol => "J/mol",
        UnidadMedida.Kelvin => "K",
        UnidadMedida.Katal => "Kat",
        UnidadMedida.KatalPorMetroCubico => "kat/m³",
        UnidadMedida.Kilogramo => "Kg",
        UnidadMedida.KilogramoPorMetroCubico => "kg/m³",
        UnidadMedida.Kilometro => "Km",
        UnidadMedida.Kilovatios => "Kw",
        UnidadMedida.KilovatiosHora => "kWh",
        UnidadMedida.Litro => "L",
        UnidadMedida.Lumen => "Lm",
        UnidadMedida.Pulgada => "Ln",
        UnidadMedida.Lux => "Lx",
        UnidadMedida.Metro => "M",
        UnidadMedida.MetroPorSegundo => "m/s",
        UnidadMedida.MetroPorSegundoCuadrado => "m/s²",
        UnidadMedida.MetroCuadrado => "m²",
        UnidadMedida.MetroCubico => "m³",
        UnidadMedida.Minuto => "´",
        UnidadMedida.Mililitro => "mL",
        UnidadMedida.Milimetro => "Mm",
        UnidadMedida.Mol => "Mol",
        UnidadMedida.MolPorMetroCubico => "mol/m³",
        UnidadMedida.Newton => "N",
        UnidadMedida.NewtonPorMetro => "N/m",
        UnidadMedida.NewtonMetro => "N·m",
        UnidadMedida.Neper => "Np",
        UnidadMedida.Grado => "º",
        UnidadMedida.OtroServicio => "Os",
        UnidadMedida.Otros => "Otros",
        UnidadMedida.Onzas => "Oz",
        UnidadMedida.Pascal => "Pa",
        UnidadMedida.PascalSegundo => "Pa·s",
        UnidadMedida.Quintal => "Qq",
        UnidadMedida.Radian => "Rad",
        UnidadMedida.RadianPorSegundo => "rad/s",
        UnidadMedida.RadianPorSegundoCuadrado => "rad/s²",
        UnidadMedida.Segundo => "S",
        UnidadMedida.Siemens => "s",
        UnidadMedida.ServiciosProfesionales => "Sp",
        UnidadMedida.ServiciosPersonales => "Spe",
        UnidadMedida.Estereorradian => "Sr",
        UnidadMedida.ServiciosTecnicos => "St",
        UnidadMedida.Sievert => "Sv",
        UnidadMedida.Tesla => "t",
        UnidadMedida.Tonelada => "T",
        UnidadMedida.UnidadMasaAtomica => "U",
        UnidadMedida.UnidadAstronomica => "Ua",
        UnidadMedida.Unidad => "Unid",
        UnidadMedida.Volt => "V",
        UnidadMedida.VoltPorMetro => "V/m",
        UnidadMedida.Watt => "W",
        UnidadMedida.WattPorMetroKelvin => "W/(m·K)",
        UnidadMedida.WattPorMetroCuadradoEstereorradian => "W/(m²·sr)",
        UnidadMedida.WattPorMetroCuadrado => "W/m²",
        UnidadMedida.WattPorEstereorradian => "W/sr",
        UnidadMedida.Weber => "Wb",
        UnidadMedida.Ohm => "Ω",
        _ => ""
    };
}

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}

/// <summary>Formas Farmacéuticas (Códigos Hacienda)</summary>
public enum FormasFarmaceutica
{
    [Description("Anillo vaginal")] AnilloVaginal,
    [Description("Apósito")] Aposito,
    [Description("Capleta")] Capleta,
    [Description("Cápsula")] Capsula,
    [Description("Cápsula blanda")] CapsulaBlanda,
    [Description("Jarabe")] Jarabe,
    [Description("Tableta")] Tableta,
    [Description("Ungüento")] Unguento,
    // ... Simplified for SDK, can be expanded if needed
}

public static class FormasFarmaceuticaExtension
{
    public static string GetCode(this FormasFarmaceutica value) => value switch
    {
        FormasFarmaceutica.AnilloVaginal => "01",
        FormasFarmaceutica.Aposito => "02",
        FormasFarmaceutica.Capleta => "03",
        FormasFarmaceutica.Capsula => "04",
        FormasFarmaceutica.CapsulaBlanda => "05",
        FormasFarmaceutica.Jarabe => "101",
        FormasFarmaceutica.Tableta => "199",
        FormasFarmaceutica.Unguento => "229",
        _ => ""
    };
}

/// <summary>Tipo de Transacción (Códigos Hacienda)</summary>
public enum TipoTransaccion
{
    [Description("Venta Normal de Bienes y Servicios")] VentaNormalBienesServicios,
    [Description("Mercancía para Autoconsumo Exenta")] MercanciaAutoconsumoExento,
    [Description("Mercancía para Autoconsumo Gravado")] MercanciaAutoconsumoGravado,
    [Description("Servicio para Autoconsumo Exento")] ServicoAutoconsumoExento,
    [Description("Servicio para Autoconsumo Gravado")] ServicioAutoconsumoGravado,
    [Description("Cuota de Afiliación")] CuotaAfiliacion,
    [Description("Cuota de Afiliación Exenta")] CuotaAfiliacionExenta,
    [Description("Bienes de Capital del Emisor")] BienesCapitalEmisor,
    [Description("Bienes de Capital del Receptor")] BienesCapitalReceptor,
    [Description("Bienes de Capital del Emisor y Receptor")] BienesCapitalEmisorReceptor,
    [Description("Bienes de Capital para Autoconsumo Exento del Emisor")] BienesCapitalAutoconsumoExentoEmisor,
    [Description("Bienes de Capital sin Contraprestación a Terceros Exento del Emisor")] BienesCapitalSinContraprestacionTerceroExentoEmisor,
    [Description("Sin Contraprestación a Terceros")] SinContraprestacionTerceros
}

public static class TipoTransaccionExtension
{
    public static string GetCode(this TipoTransaccion value) => value switch
    {
        TipoTransaccion.VentaNormalBienesServicios => "01",
        TipoTransaccion.MercanciaAutoconsumoExento => "02",
        TipoTransaccion.MercanciaAutoconsumoGravado => "03",
        TipoTransaccion.ServicoAutoconsumoExento => "04",
        TipoTransaccion.ServicioAutoconsumoGravado => "05",
        TipoTransaccion.CuotaAfiliacion => "06",
        TipoTransaccion.CuotaAfiliacionExenta => "07",
        TipoTransaccion.BienesCapitalEmisor => "08",
        TipoTransaccion.BienesCapitalReceptor => "09",
        TipoTransaccion.BienesCapitalEmisorReceptor => "10",
        TipoTransaccion.BienesCapitalAutoconsumoExentoEmisor => "11",
        TipoTransaccion.BienesCapitalSinContraprestacionTerceroExentoEmisor => "12",
        TipoTransaccion.SinContraprestacionTerceros => "13",
        _ => ""
    };
}

/// <summary>Tipo de Documento Terceros (Códigos Hacienda)</summary>
public enum TipoDocumentoTerceros
{
    [Description("Contribución Parafiscal")] ContribucionParafiscal,
    [Description("Timbre Cruz Roja")] TimbreCruzRoja,
    [Description("Timbre Bomberos")] TimbreBomberos,
    [Description("Cobro Tercero")] CobroTercero,
    [Description("Costos Exportación")] CostosExportacion,
    [Description("Impuesto Servicio")] ImpuestoServicio,
    [Description("Timbre Colegios Profesionales")] TimbreColegiosProfesionales,
    [Description("Depósitos Garantía")] DepositosGarantia,
    [Description("Multas Penalizaciones")] MultasPenalizaciones,
    [Description("Intereses Moratorios")] InteresesMoratorios,
    [Description("Otros Cargos")] OtrosCargos
}

public static class TipoDocumentoTercerosExtension
{
    public static string GetCode(this TipoDocumentoTerceros value) => value switch
    {
        TipoDocumentoTerceros.ContribucionParafiscal => "01",
        TipoDocumentoTerceros.TimbreCruzRoja => "02",
        TipoDocumentoTerceros.TimbreBomberos => "03",
        TipoDocumentoTerceros.CobroTercero => "04",
        TipoDocumentoTerceros.CostosExportacion => "05",
        TipoDocumentoTerceros.ImpuestoServicio => "06",
        TipoDocumentoTerceros.TimbreColegiosProfesionales => "07",
        TipoDocumentoTerceros.DepositosGarantia => "08",
        TipoDocumentoTerceros.MultasPenalizaciones => "09",
        TipoDocumentoTerceros.InteresesMoratorios => "10",
        TipoDocumentoTerceros.OtrosCargos => "99",
        _ => ""
    };
}

/// <summary>Tipo de Mensaje (Códigos Hacienda)</summary>
public enum TipoMensaje
{
    [Description("Aceptado")] Aceptado,
    [Description("Aceptación Parcial")] AceptacionParcial,
    [Description("Rechazado")] Rechazado
}

public static class TipoMensajeExtension
{
    public static string GetCode(this TipoMensaje value) => value switch
    {
        TipoMensaje.Aceptado => "1",
        TipoMensaje.AceptacionParcial => "2",
        TipoMensaje.Rechazado => "3",
        _ => ""
    };
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
    [Description("Información")] Info = 0,
    [Description("Éxito")] Success = 1,
    [Description("Advertencia")] Warning = 2,
    [Description("Error")] Error = 3,
    [Description("Alerta de Sistema")] SystemAlert = 4
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
