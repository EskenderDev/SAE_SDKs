package com.eskender.sae.sdk.models

/**
 * Tipo de Comprobante Electrónico (Códigos Hacienda)
 */
enum class TipoComprobante(val code: String, val description: String) {
    FacturaElectronica("01", "Factura Electrónica"),
    NotaDebitoElectronica("02", "Nota de Débito Electrónica"),
    NotaCreditoElectronica("03", "Nota de Crédito Electrónica"),
    TiqueteElectronico("04", "Tiquete Electrónico"),
    ConfirmacionAceptacion("05", "Confirmación de Aceptación"),
    ConfirmacionAceptacionParcial("06", "Confirmación de Aceptación Parcial"),
    ConfirmacionRechazo("07", "Confirmación de Rechazo"),
    FacturaElectronicaCompras("08", "Factura Electrónica de Compras"),
    FacturaElectronicaExportacion("09", "Factura Electrónica de Exportación"),
    ReciboElectronicoPago("10", "Recibo Electrónico de Pago")
}

/**
 * Tipo de Identificación (Códigos Hacienda)
 */
enum class TipoIdentificacion(val code: String, val description: String) {
    CedulaFisica("01", "Cédula Física"),
    CedulaJuridica("02", "Cédula Jurídica"),
    DIMEX("03", "DIMEX"),
    NITE("04", "NITE"),
    Extranjero("05", "Extranjero No Domiciliado"),
    NoContribuyente("06", "No Contribuyente")
}

/**
 * Condición de Venta (Códigos Hacienda)
 */
enum class CondicionVenta(val code: String, val description: String) {
    Contado("01", "Contado"),
    Credito("02", "Crédito"),
    Consignacion("03", "Consignación"),
    Apartado("04", "Apartado"),
    ArrendamientoOpcionCompra("05", "Arrendamiento con opción de compra"),
    ArrendamientoFuncionFinanciera("06", "Arrendamiento con función financiera"),
    CobroFavorTercero("07", "Cobro a favor de un tercero"),
    ServiciosPrestadosEstado("08", "Servicios prestados al Estado"),
    PagoServicosPrestadoEstado("09", "Pago por servicios al Estado"),
    VentaCreditoIVA90Dias("10", "Venta a crédito IVA a 90 días"),
    PagoVentaCreditoIVA90Dias("11", "Pago por venta a crédito IVA a 90 días"),
    VentaMercanciaNoNacionalizada("12", "Venta de mercancía no nacionalizada"),
    VentaBienesUsadosNoContribuyente("13", "Venta de bienes usados a no contribuyente"),
    ArrendamientoOperativo("14", "Arrendamiento operativo"),
    ArrendamientoFinanciero("15", "Arrendamiento financiero"),
    Otro("99", "Otro")
}

/**
 * Tipo de Medio de Pago (Códigos Hacienda)
 */
enum class TipoMedioPago(val code: String, val description: String) {
    Efectivo("01", "Efectivo"),
    Tarjeta("02", "Tarjeta"),
    Cheque("03", "Cheque"),
    Transferencia("04", "Transferencia"),
    RecaudadoTerceros("05", "Recaudado por Terceros"),
    SinpeMovil("06", "SINPE Móvil"),
    PlataformaDigital("07", "Plataforma Digital"),
    Otro("99", "Otro")
}

/**
 * Código de Moneda (ISO 4217)
 */
enum class CodigoMoneda(val code: String, val description: String, val symbol: String) {
    AED("AED", "Dirham de los Emiratos Árabes Unidos", "د.إ"),
    AFN("AFN", "Afghani", "؋"),
    ALL("ALL", "Lek", "L"),
    AMD("AMD", "Dram armenio", "֏"),
    ANG("ANG", "Florín antillano neerlandés", "ƒ"),
    AOA("AOA", "Kwanza", "Kz"),
    ARS("ARS", "Peso Argentino", "$"),
    AUD("AUD", "Dólar australiano", "A$"),
    AWG("AWG", "Florín arubeño", "ƒ"),
    AZN("AZN", "Manat azerbaiyano", "₼"),
    BAM("BAM", "Marco bosnioherzegovino", "KM"),
    BBD("BBD", "Dólar de Barbados", "Bds$"),
    BDT("BDT", "Taka", "৳"),
    BGN("BGN", "Lev búlgaro", "лв"),
    BHD("BHD", "Dinar de Bahrein", ".د.ب"),
    BIF("BIF", "Franco Burundi", "FBu"),
    BMD("BMD", "Dólar de Bermudas", "$"),
    BND("BND", "Dólar de Brunei", "$"),
    BOB("BOB", "Boliviano", "Bs."),
    BOV("BOV", "Mvdol", "Mvdol"),
    BRL("BRL", "Real Brasileño", "R$"),
    BSD("BSD", "Dólar de las Bahamas", "$"),
    BTN("BTN", "Ngultrum", "Nu."),
    BWP("BWP", "Pula", "P"),
    BYR("BYR", "Rublo bielorruso", "Br"),
    BZD("BZD", "Dólar de Belice", "BZ$"),
    CAD("CAD", "Dólar canadiense", "C$"),
    CDF("CDF", "Franco congoleño", "FC"),
    CHF("CHF", "Franco suizo", "CHF"),
    CLF("CLF", "Unidad de Fomento", "UF"),
    CLP("CLP", "Peso chileno", "$"),
    CNY("CNY", "Yuan", "¥"),
    COP("COP", "Peso Colombiano", "$"),
    COU("COU", "Unidad de Valor Real", "COU"),
    CRC("CRC", "Colón costarricense", "₡"),
    CUC("CUC", "Peso Convertible", "$"),
    CUP("CUP", "Peso Cubano", "$"),
    CVE("CVE", "Cabo Verde Escudo", "$"),
    CZK("CZK", "Corona checa", "Kč"),
    DJF("DJF", "Franco de Djibouti", "Fdj"),
    DKK("DKK", "Corona danesa", "kr"),
    DOP("DOP", "Peso Dominicano", "RD$"),
    DZD("DZD", "Dinar argelino", "د.ج"),
    EGP("EGP", "Libra egipcia", "£"),
    ERN("ERN", "Nakfa", "Nfk"),
    ETB("ETB", "Birr etíope", "Br"),
    EUR("EUR", "Euro", "€"),
    FJD("FJD", "Dólar de Fiji", "FJ$"),
    FKP("FKP", "Libra malvinense", "£"),
    GBP("GBP", "Libra esterlina", "£"),
    GEL("GEL", "Lari", "₾"),
    GHS("GHS", "Cedi de Ghana", "GH₵"),
    GIP("GIP", "Libra de Gibraltar", "£"),
    GMD("GMD", "Dalasi", "D"),
    GNF("GNF", "Franco guineano", "FG"),
    GTQ("GTQ", "Quetzal", "Q"),
    GYD("GYD", "Dólar guyanés", "$"),
    HKD("HKD", "Dolar de Hong Kong", "HK$"),
    HNL("HNL", "Lempira", "L"),
    HRK("HRK", "Kuna", "kn"),
    HTG("HTG", "Gourde", "G"),
    HUF("HUF", "Florín", "Ft"),
    IDR("IDR", "Rupia", "Rp"),
    ILS("ILS", "Nuevo Shekel Israelí", "₪"),
    INR("INR", "Rupia india", "₹"),
    IQD("IQD", "Dinar iraquí", "ع.د"),
    IRR("IRR", "Rial iraní", "﷼"),
    ISK("ISK", "Corona islandesa", "kr"),
    JMD("JMD", "Dólar jamaiquino", "J$"),
    JOD("JOD", "Dinar jordano", "د.ا"),
    JPY("JPY", "Yen", "¥"),
    KES("KES", "Chelín keniano", "KSh"),
    KGS("KGS", "Som", "сом"),
    KHR("KHR", "Riel", "៛"),
    KMF("KMF", "Franco Comoro", "CF"),
    KPW("KPW", "Won norcoreano", "₩"),
    KRW("KRW", "Won", "₩"),
    KWD("KWD", "Dinar kuwaití", "د.ك"),
    KYD("KYD", "Dólar de las Islas Caimán", "$"),
    KZT("KZT", "Tenge", "₸"),
    LAK("LAK", "Kip", "₭"),
    LBP("LBP", "Libra libanesa", "ل.ل"),
    LKR("LKR", "Rupia de Sri Lanka", "₨"),
    LRD("LRD", "Dólar liberiano", "$"),
    LSL("LSL", "Loti", "L"),
    LYD("LYD", "Dinar libio", "ل.د"),
    MAD("MAD", "Dirham marroquí", "د.م."),
    MDL("MDL", "Leu moldavo", "L"),
    MGA("MGA", "Ariary malgache", "Ar"),
    MKD("MKD", "Denar", "ден"),
    MMK("MMK", "Kyat", "K"),
    MNT("MNT", "Tugrik", "₮"),
    MOP("MOP", "Pataca", "MOP$"),
    MRO("MRO", "Ouguiya", "UM"),
    MUR("MUR", "Rupia de Mauricio", "₨"),
    MVR("MVR", "Rufiyaa", "Rf"),
    MWK("MWK", "Kwacha", "MK"),
    MXN("MXN", "Peso Mexicano", "$"),
    MXV("MXV", "Unidad de Inversion Mexicana (UDI)", "UDI"),
    MYR("MYR", "Ringgit malayo", "RM"),
    MZN("MZN", "Metical mozambiqueño", "MT"),
    NAD("NAD", "Dólar de Namibia", "$"),
    NGN("NGN", "Naira", "₦"),
    NIO("NIO", "Cordoba Oro", "C$"),
    NOK("NOK", "Corona noruega", "kr"),
    NPR("NPR", "Rupia nepalí", "₨"),
    NZD("NZD", "Dólar neozelandés", "NZ$"),
    OMR("OMR", "Rial omaní", "﷼"),
    PAB("PAB", "Balboa", "B/."),
    PEN("PEN", "Nuevo Sol", "S/"),
    PGK("PGK", "Kina", "K"),
    PHP("PHP", "Peso filipino", "₱"),
    PKR("PKR", "Rupia pakistaní", "₨"),
    PLN("PLN", "Zloty", "zł"),
    PYG("PYG", "Guaraní", "₲"),
    QAR("QAR", "Riyal catarí", "﷼"),
    RON("RON", "Leu rumano", "lei"),
    RSD("RSD", "Dinar serbio", "дин"),
    RUB("RUB", "Rublo ruso", "₽"),
    RWF("RWF", "Franco ruandés", "FRw"),
    SAR("SAR", "Riyal saudí", "﷼"),
    SBD("SBD", "Dólar de las Islas Salomón", "SI$"),
    SCR("SCR", "Rupia seychelense", "₨"),
    SDG("SDG", "Libra sudanesa", "ج.س."),
    SEK("SEK", "Corona sueca", "kr"),
    SGD("SGD", "Dolar de Singapur", "S$"),
    SHP("SHP", "Libra de Santa Helena", "£"),
    SLL("SLL", "Leone", "Le"),
    SOS("SOS", "Chelín somalí", "S"),
    SRD("SRD", "Dólar surinamés", "$"),
    SSP("SSP", "Libra sursudanesa", "£"),
    STD("STD", "Dobra", "Db"),
    SVC("SVC", "Colón", "₡"),
    SYP("SYP", "Libra Siria", "£"),
    SZL("SZL", "Lilangeni", "E"),
    THB("THB", "Baht", "฿"),
    TJS("TJS", "Somoni", "SM"),
    TMT("TMT", "Manat turcomano", "T"),
    TND("TND", "Dinar tunecino", "د.ت"),
    TOP("TOP", "Pa'anga", "T$"),
    TRY("TRY", "Lira turca", "₺"),
    TTD("TTD", "Dólar trinitense", "TT$"),
    TWD("TWD", "Nuevo dólar taiwanés", "NT$"),
    TZS("TZS", "Chelín tanzano", "TSh"),
    UAH("UAH", "Hryvnia", "₴"),
    UGX("UGX", "Chelín ugandés", "USh"),
    USD("USD", "Dólar estadounidense", "$"),
    USN("USN", "Dólar Americanó (Next day)", "$"),
    UYI("UYI", "Uruguay Peso en Unidades Indexadas (URUIURUI)", "UI"),
    UYU("UYU", "Peso Uruguayo", "$U"),
    UZS("UZS", "Som uzbeko", "soʻm"),
    VEF("VEF", "Bolívar", "Bs.F"),
    VND("VND", "Dong", "₫"),
    VUV("VUV", "Vatu", "VT"),
    WST("WST", "Tala", "WS$"),
    XAF("XAF", "Franco CFA BEAC", "FCFA"),
    XCD("XCD", "Dólar del Caribe Oriental", "$"),
    XDR("XDR", "SDR (Derechos Especiales de Giro)", "SDR"),
    XOF("XOF", "Franco CFA BCEAO", "CFA"),
    XPF("XPF", "Franco CFP", "₣"),
    XSU("XSU", "Sucre", "Sucre"),
    XUA("XUA", "Unidad de cuenta del BAD", "XUA"),
    YER("YER", "Rial yemení", "﷼"),
    ZAR("ZAR", "Rand", "R"),
    ZMW("ZMW", "Kwacha zambiano", "ZK"),
    ZWL("ZWL", "Dólar zimbabuense", "$")
}

/**
 * Código de Impuesto (Códigos Hacienda)
 */
enum class CodigoImpuesto(val code: String, val description: String) {
    ImpuestoValorAgregado("01", "Impuesto al Valor Agregado"),
    ImpuestoSelectivoConsumo("02", "Impuesto Selectivo de Consumo"),
    ImpuestoUnicoCombustibles("03", "Impuesto Único a los Combustibles"),
    ImpuestoEspecificoBebidasAlcoholicas("04", "Impuesto Específico a Bebidas Alcohólicas"),
    ImpuestoEspecificoBebidasEnvasadasSinAlcoholYJabones("05", "Impuesto Específico a Bebidas Envasadas sin Alcohol y Jabones"),
    ImpuestoProductosTabaco("06", "Impuesto a Productos de Tabaco"),
    ImpuestoIVACalculoEspecial("07", "IVA con Cálculo Especial"),
    IVARegimenBienesUsadosFactor("08", "IVA Régimen Bienes Usados - Factor"),
    ImpuestoEspecificoCemento("12", "Impuesto Específico al Cemento"),
    Otros("99", "Otros impuestos")
}

/**
 * Código de Descuento (Códigos Hacienda)
 */
enum class CodigoDescuento(val code: String, val description: String) {
    DescuentoRegalia("01", "Descuento por regalía"),
    DescuentoRegaliaIVACobradoCliente("02", "Descuento por regalía con IVA cobrado al cliente"),
    DescuentoBonificacion("03", "Descuento por bonificación"),
    DescuentoVolumen("04", "Descuento por volumen"),
    DescuentoTemporada("05", "Descuento por temporada"),
    DescuentoPromocional("06", "Descuento promocional"),
    DescuentoComercial("07", "Descuento comercial"),
    DescuentoFrecuencia("08", "Descuento por frecuencia"),
    DescuentoSostenido("09", "Descuento sostenido"),
    Otro("99", "Otro tipo de descuento")
}

/**
 * Código de Referencia (Códigos Hacienda)
 */
enum class CodigoReferencia(val code: String, val description: String) {
    AnulaDocumentoReferencia("01", "Anula documento de referencia"),
    CorrigeMonto("02", "Corrige monto"),
    CorrigeTextoDocumentoReferencia("03", "Corrige texto de documento de referencia"),
    ReferenciaOtroDocumento("04", "Referencia otro documento"),
    SustituyeComprobanteProvisionalContingencia("05", "Sustituye comprobante provisional por contingencia"),
    DevolucionMercancia("06", "Devolución de mercancía"),
    SustituyeComprobanteElectronico("07", "Sustituye comprobante electrónico"),
    FacturaEndosada("08", "Factura endosada"),
    NotaCreditoFinanciera("09", "Nota de crédito financiera"),
    NotaDebitoFinanciera("10", "Nota de débito financiera"),
    ProveedorNoDomiciliado("11", "Proveedor no domiciliado"),
    CreditoExoneracionPosteriorFacturacion("12", "Crédito por exoneración posterior a la facturación"),
    Otros("99", "Otros")
}

/**
 * Unidades de Medida (Códigos Hacienda)
 */
enum class UnidadMedida(val code: String, val description: String) {
    Uno("1", "Uno"),
    minuto("´", "minuto"),
    segundo("´´", "segundo"),
    GradoCelsius("°C", "Grado Celsius"),
    UnoPorMetro("1/m", "Uno por Metro"),
    Ampere("A", "Ampere"),
    AmperePorMetro("A/m", "Ampere por Metro"),
    AmperePorMetroCuadrado("A/m²", "Ampere por Metro Cuadrado"),
    ActivoVirtual("Acv", "Activo Virtual"),
    AlquilerHabitacional("Al", "Alquiler Habitacional"),
    AlquilerComercial("Alc", "Alquiler Comercial"),
    Bel("B", "Bel"),
    Becquerel("Bq", "Becquerel"),
    Coulomb("C", "Coulomb"),
    CoulombPorKilogramo("C/kg", "Coulomb por Kilogramo"),
    CoulombPorMetroCuadrado("C/m²", "Coulomb por Metro Cuadrado"),
    CoulombPorMetroCubico("C/m³", "Coulomb por Metro Cúbico"),
    CajuelaCafe("Cc", "Cajuela de Café"),
    Candela("Cd", "Candela"),
    CandelaPorMetroCuadrado("cd/m²", "Candela por Metro Cuadrado"),
    Comision("Cm", "Comisión"),
    Centimetro("cm", "Centímetro"),
    CuartilloCafe("Cu", "Cuartillo de Café"),
    Dia("D", "Día"),
    Electronvolt("eV", "Electronvolt"),
    Farad("F", "Farad"),
    FaradPorMetro("F/m", "Farad por Metro"),
    FanegaCafe("Fa", "Fanega de Café"),
    Gramo("G", "Gramo"),
    Galon("Gal", "Galón"),
    Gray("Gy", "Gray"),
    GrayPorSegundo("Gy/s", "Gray por Segundo"),
    Hora("H", "Hora"),
    Henry("h", "Henry"),
    HenryPorMetro("H/m", "Henry por Metro"),
    Hertz("Hz", "Hertz"),
    Intereses("I", "Intereses"),
    Joule("J", "Joule"),
    JoulePorKilogramoKelvin("J/(kg·K)", "Joule por Kilogramo Kelvin"),
    JoulePorMolKelvin("J/(mol·K)", "Joule por Mol Kelvin"),
    JoulePorKelvin("J/K", "Joule por Kelvin"),
    JoulePorKilogramo("J/kg", "Joule por Kilogramo"),
    JoulePorMetroCubico("J/m³", "Joule por Metro Cúbico"),
    JoulePorMol("J/mol", "Joule por Mol"),
    Kelvin("K", "Kelvin"),
    Katal("Kat", "Katal"),
    KatalPorMetroCubico("kat/m³", "Katal por Metro Cúbico"),
    Kilogramo("Kg", "Kilogramo"),
    KilogramoPorMetroCubico("kg/m³", "Kilogramo por Metro Cúbico"),
    Kilometro("Km", "Kilómetro"),
    Kilovatios("Kw", "Kilovatios"),
    KilovatiosHora("kWh", "Kilovatios Hora"),
    Litro("L", "Litro"),
    Lumen("Lm", "Lumen"),
    Pulgada("Ln", "Pulgada"),
    Lux("Lx", "Lux"),
    Metro("M", "Metro"),
    MetroPorSegundo("m/s", "Metro por Segundo"),
    MetroPorSegundoCuadrado("m/s²", "Metro por Segundo Cuadrado"),
    MetroCuadrado("m²", "Metro Cuadrado"),
    MetroCubico("m³", "Metro Cúbico"),
    Minuto("´", "Minuto"),
    Mililitro("mL", "Mililitro"),
    Milimetro("Mm", "Milímetro"),
    Mol("Mol", "Mol"),
    MolPorMetroCubico("mol/m³", "Mol por Metro Cúbico"),
    Newton("N", "Newton"),
    NewtonPorMetro("N/m", "Newton por Metro"),
    NewtonMetro("N·m", "Newton Metro"),
    Neper("Np", "Neper"),
    Grado("º", "Grado"),
    OtroServicio("Os", "Otro Servicio"),
    Otros("Otros", "Otros"),
    Onzas("Oz", "Onzas"),
    Pascal("Pa", "Pascal"),
    PascalSegundo("Pa·s", "Pascal Segundo"),
    Quintal("Qq", "Quintal"),
    Radian("Rad", "Radián"),
    RadianPorSegundo("rad/s", "Radián por Segundo"),
    RadianPorSegundoCuadrado("rad/s²", "Radián por Segundo Cuadrado"),
    Segundo("S", "Segundo"),
    Siemens("s", "Siemens"),
    ServiciosProfesionales("Sp", "Servicios Profesionales"),
    ServiciosPersonales("Spe", "Servicios Personales"),
    Estereorradian("Sr", "Estereorradián"),
    ServiciosTecnicos("St", "Servicios Técnicos"),
    Sievert("Sv", "Sievert"),
    Tesla("t", "Tesla"),
    Tonelada("T", "Tonelada"),
    UnidadMasaAtomica("U", "Unidad de Masa Atómica"),
    UnidadAstronomica("Ua", "Unidad Astronómica"),
    Unidad("Unid", "Unidad"),
    Volt("V", "Volt"),
    VoltPorMetro("V/m", "Volt por Metro"),
    Watt("W", "Watt"),
    WattPorMetroKelvin("W/(m·K)", "Watt por Metro Kelvin"),
    WattPorMetroCuadradoEstereorradian("W/(m²·sr)", "Watt por Metro Cuadrado Estereorradián"),
    WattPorMetroCuadrado("W/m²", "Watt por Metro Cuadrado"),
    WattPorEstereorradian("W/sr", "Watt por Estereorradián"),
    Weber("Wb", "Weber"),
    Ohm("Ω", "Ohm")
}

/**
 * Situación de Comprobante Electrónico (Códigos Hacienda)
 */
enum class SituacionComprobante(val code: String, val description: String) {
    Normal("1", "Normal"),
    Contingencia("2", "Contingencia"),
    SinInternet("3", "Sin Internet")
}

/**
 * Tipo de Mensaje (Hacienda)
 */
enum class TipoMensaje(val code: String, val description: String) {
    Aceptado("1", "Aceptado"),
    AceptacionParcial("2", "Aceptación Parcial"),
    Rechazado("3", "Rechazado")
}

/**
 * Estado de un envío a Hacienda
 */
enum class HaciendaEstado {
    Pendiente,
    Enviado,
    Aceptado,
    Rechazado,
    Error,
    Procesando
}
