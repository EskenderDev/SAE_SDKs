
// Tipo de Comprobante Electrónico (Códigos Hacienda)
/**
 * Tipo de Comprobante Electrónico (Códigos Hacienda)
 */
export enum TipoComprobante {
  /** Factura Electrónica */
  FacturaElectronica = '01',
  /** Nota de Débito Electrónica */
  NotaDebitoElectronica = '02',
  /** Nota de Crédito Electrónica */
  NotaCreditoElectronica = '03',
  /** Tiquete Electrónico */
  TiqueteElectronico = '04',
  /** Confirmación de Aceptación */
  ConfirmacionAceptacion = '05',
  /** Confirmación de Aceptación Parcial */
  ConfirmacionAceptacionParcial = '06',
  /** Confirmación de Rechazo */
  ConfirmacionRechazo = '07',
  /** Factura Electrónica de Compras */
  FacturaElectronicaCompras = '08',
  /** Factura Electrónica de Exportación */
  FacturaElectronicaExportacion = '09',
  /** Recibo Electrónico de Pago */
  ReciboElectronicoPago = '10',
}

/**
 * Tipo de Identificación (Códigos Hacienda)
 */
export enum TipoIdentificacion {
  /** Cédula Física */
  CedulaFisica = '01',
  /** Cédula Jurídica */
  CedulaJuridica = '02',
  /** DIMEX */
  DIMEX = '03',
  /** NITE */
  NITE = '04',
  /** Extranjero */
  Extranjero = '05',
  /** No Contribuyente */
  NoContribuyente = '06',
}

/**
 * Condición de Venta (Códigos Hacienda)
 */
export enum CondicionVenta {
  /** Contado */
  Contado = '01',
  /** Crédito */
  Credito = '02',
  /** Consignación */
  Consignacion = '03',
  /** Apartado */
  Apartado = '04',
  /** Arrendamiento con opción de compra */
  ArrendamientoOpcionCompra = '05',
  /** Arrendamiento con función financiera */
  ArrendamientoFuncionFinanciera = '06',
  /** Cobro a favor de un tercero */
  CobroFavorTercero = '07',
  /** Servicios prestados al Estado */
  ServiciosPrestadosEstado = '08',
  /** Pago por servicios al Estado */
  PagoServicosPrestadoEstado = '09',
  /** Venta a crédito IVA a 90 días */
  VentaCreditoIVA90Dias = '10',
  /** Pago por venta a crédito IVA a 90 días */
  PagoVentaCreditoIVA90Dias = '11',
  /** Venta de mercancía no nacionalizada */
  VentaMercanciaNoNacionalizada = '12',
  /** Venta de bienes usados a no contribuyente */
  VentaBienesUsadosNoContribuyente = '13',
  /** Arrendamiento operativo */
  ArrendamientoOperativo = '14',
  /** Arrendamiento financiero */
  ArrendamientoFinanciero = '15',
  /** Otro */
  Otro = '99',
}

/**
 * Tipo de Medio de Pago (Códigos Hacienda)
 */
export enum TipoMedioPago {
  /** Efectivo */
  Efectivo = '01',
  /** Tarjeta */
  Tarjeta = '02',
  /** Cheque */
  Cheque = '03',
  /** Transferencia */
  Transferencia = '04',
  /** Recaudado por Terceros */
  RecaudadoTerceros = '05',
  /** SINPE Móvil */
  SinpeMovil = '06',
  /** Plataforma Digital */
  PlataformaDigital = '07',
  /** Otro */
  Otro = '99',
}

/**
 * Código de Moneda (ISO 4217)
 */
export enum CodigoMoneda {
  /** Dirham de los Emiratos Árabes Unidos */
  AED = 'AED',
  /** Afghani */
  AFN = 'AFN',
  /** Lek */
  ALL = 'ALL',
  /** Dram armenio */
  AMD = 'AMD',
  /** Florín antillano neerlandés */
  ANG = 'ANG',
  /** Kwanza */
  AOA = 'AOA',
  /** Peso Argentino */
  ARS = 'ARS',
  /** Dólar australiano */
  AUD = 'AUD',
  /** Florín arubeño */
  AWG = 'AWG',
  /** Manat azerbaiyano */
  AZN = 'AZN',
  /** Marco bosnioherzegovino */
  BAM = 'BAM',
  /** Dólar de Barbados */
  BBD = 'BBD',
  /** Taka */
  BDT = 'BDT',
  /** Lev búlgaro */
  BGN = 'BGN',
  /** Dinar de Bahrein */
  BHD = 'BHD',
  /** Franco Burundi */
  BIF = 'BIF',
  /** Dólar de Bermudas */
  BMD = 'BMD',
  /** Dólar de Brunei */
  BND = 'BND',
  /** Boliviano */
  BOB = 'BOB',
  /** Mvdol */
  BOV = 'BOV',
  /** Real Brasileño */
  BRL = 'BRL',
  /** Dólar de las Bahamas */
  BSD = 'BSD',
  /** Ngultrum */
  BTN = 'BTN',
  /** Pula */
  BWP = 'BWP',
  /** Rublo bielorruso */
  BYR = 'BYR',
  /** Dólar de Belice */
  BZD = 'BZD',
  /** Dólar canadiense */
  CAD = 'CAD',
  /** Franco congoleño */
  CDF = 'CDF',
  /** Franco suizo */
  CHF = 'CHF',
  /** Unidad de Fomento */
  CLF = 'CLF',
  /** Peso chileno */
  CLP = 'CLP',
  /** Yuan */
  CNY = 'CNY',
  /** Peso Colombiano */
  COP = 'COP',
  /** Unidad de Valor Real */
  COU = 'COU',
  /** Colón costarricense */
  CRC = 'CRC',
  /** Peso Convertible */
  CUC = 'CUC',
  /** Peso Cubano */
  CUP = 'CUP',
  /** Cabo Verde Escudo */
  CVE = 'CVE',
  /** Corona checa */
  CZK = 'CZK',
  /** Franco de Djibouti */
  DJF = 'DJF',
  /** Corona danesa */
  DKK = 'DKK',
  /** Peso Dominicano */
  DOP = 'DOP',
  /** Dinar argelino */
  DZD = 'DZD',
  /** Libra egipcia */
  EGP = 'EGP',
  /** Nakfa */
  ERN = 'ERN',
  /** Birr etíope */
  ETB = 'ETB',
  /** Euro */
  EUR = 'EUR',
  /** Dólar de Fiji */
  FJD = 'FJD',
  /** Libra malvinense */
  FKP = 'FKP',
  /** Libra esterlina */
  GBP = 'GBP',
  /** Lari */
  GEL = 'GEL',
  /** Cedi de Ghana */
  GHS = 'GHS',
  /** Libra de Gibraltar */
  GIP = 'GIP',
  /** Dalasi */
  GMD = 'GMD',
  /** Franco guineano */
  GNF = 'GNF',
  /** Quetzal */
  GTQ = 'GTQ',
  /** Dólar guyanés */
  GYD = 'GYD',
  /** Dolar de Hong Kong */
  HKD = 'HKD',
  /** Lempira */
  HNL = 'HNL',
  /** Kuna */
  HRK = 'HRK',
  /** Gourde */
  HTG = 'HTG',
  /** Florín */
  HUF = 'HUF',
  /** Rupia */
  IDR = 'IDR',
  /** Nuevo Shekel Israelí */
  ILS = 'ILS',
  /** Rupia india */
  INR = 'INR',
  /** Dinar iraquí */
  IQD = 'IQD',
  /** Rial iraní */
  IRR = 'IRR',
  /** Corona islandesa */
  ISK = 'ISK',
  /** Dólar jamaiquino */
  JMD = 'JMD',
  /** Dinar jordano */
  JOD = 'JOD',
  /** Yen */
  JPY = 'JPY',
  /** Chelín keniano */
  KES = 'KES',
  /** Som */
  KGS = 'KGS',
  /** Riel */
  KHR = 'KHR',
  /** Franco Comoro */
  KMF = 'KMF',
  /** Won norcoreano */
  KPW = 'KPW',
  /** Won */
  KRW = 'KRW',
  /** Dinar kuwaití */
  KWD = 'KWD',
  /** Dólar de las Islas Caimán */
  KYD = 'KYD',
  /** Tenge */
  KZT = 'KZT',
  /** Kip */
  LAK = 'LAK',
  /** Libra libanesa */
  LBP = 'LBP',
  /** Rupia de Sri Lanka */
  LKR = 'LKR',
  /** Dólar liberiano */
  LRD = 'LRD',
  /** Loti */
  LSL = 'LSL',
  /** Dinar libio */
  LYD = 'LYD',
  /** Dirham marroquí */
  MAD = 'MAD',
  /** Leu moldavo */
  MDL = 'MDL',
  /** Ariary malgache */
  MGA = 'MGA',
  /** Denar */
  MKD = 'MKD',
  /** Kyat */
  MMK = 'MMK',
  /** Tugrik */
  MNT = 'MNT',
  /** Pataca */
  MOP = 'MOP',
  /** Ouguiya */
  MRO = 'MRO',
  /** Rupia de Mauricio */
  MUR = 'MUR',
  /** Rufiyaa */
  MVR = 'MVR',
  /** Kwacha */
  MWK = 'MWK',
  /** Peso Mexicano */
  MXN = 'MXN',
  /** Unidad de Inversion Mexicana (UDI) */
  MXV = 'MXV',
  /** Ringgit malayo */
  MYR = 'MYR',
  /** Metical mozambiqueño */
  MZN = 'MZN',
  /** Dólar de Namibia */
  NAD = 'NAD',
  /** Naira */
  NGN = 'NGN',
  /** Cordoba Oro */
  NIO = 'NIO',
  /** Corona noruega */
  NOK = 'NOK',
  /** Rupia nepalí */
  NPR = 'NPR',
  /** Dólar neozelandés */
  NZD = 'NZD',
  /** Rial omaní */
  OMR = 'OMR',
  /** Balboa */
  PAB = 'PAB',
  /** Nuevo Sol */
  PEN = 'PEN',
  /** Kina */
  PGK = 'PGK',
  /** Peso filipino */
  PHP = 'PHP',
  /** Rupia pakistaní */
  PKR = 'PKR',
  /** Zloty */
  PLN = 'PLN',
  /** Guaraní */
  PYG = 'PYG',
  /** Riyal catarí */
  QAR = 'QAR',
  /** Leu rumano */
  RON = 'RON',
  /** Dinar serbio */
  RSD = 'RSD',
  /** Rublo ruso */
  RUB = 'RUB',
  /** Franco ruandés */
  RWF = 'RWF',
  /** Riyal saudí */
  SAR = 'SAR',
  /** Dólar de las Islas Salomón */
  SBD = 'SBD',
  /** Rupia seychelense */
  SCR = 'SCR',
  /** Libra sudanesa */
  SDG = 'SDG',
  /** Corona sueca */
  SEK = 'SEK',
  /** Dolar de Singapur */
  SGD = 'SGD',
  /** Libra de Santa Helena */
  SHP = 'SHP',
  /** Leone */
  SLL = 'SLL',
  /** Chelín somalí */
  SOS = 'SOS',
  /** Dólar surinamés */
  SRD = 'SRD',
  /** Libra sursudanesa */
  SSP = 'SSP',
  /** Dobra */
  STD = 'STD',
  /** Colón */
  SVC = 'SVC',
  /** Libra Siria */
  SYP = 'SYP',
  /** Lilangeni */
  SZL = 'SZL',
  /** Baht */
  THB = 'THB',
  /** Somoni */
  TJS = 'TJS',
  /** Manat turcomano */
  TMT = 'TMT',
  /** Dinar tunecino */
  TND = 'TND',
  /** Pa\'anga */
  TOP = 'TOP',
  /** Lira turca */
  TRY = 'TRY',
  /** Dólar trinitense */
  TTD = 'TTD',
  /** Nuevo dólar taiwanés */
  TWD = 'TWD',
  /** Chelín tanzano */
  TZS = 'TZS',
  /** Hryvnia */
  UAH = 'UAH',
  /** Chelín ugandés */
  UGX = 'UGX',
  /** Dólar estadounidense */
  USD = 'USD',
  /** Dólar Americanó (Next day) */
  USN = 'USN',
  /** Uruguay Peso en Unidades Indexadas (URUIURUI) */
  UYI = 'UYI',
  /** Peso Uruguayo */
  UYU = 'UYU',
  /** Som uzbeko */
  UZS = 'UZS',
  /** Bolívar */
  VEF = 'VEF',
  /** Dong */
  VND = 'VND',
  /** Vatu */
  VUV = 'VUV',
  /** Tala */
  WST = 'WST',
  /** Franco CFA BEAC */
  XAF = 'XAF',
  /** Dólar del Caribe Oriental */
  XCD = 'XCD',
  /** SDR (Derechos Especiales de Giro) */
  XDR = 'XDR',
  /** Franco CFA BCEAO */
  XOF = 'XOF',
  /** Franco CFP */
  XPF = 'XPF',
  /** Sucre */
  XSU = 'XSU',
  /** Unidad de cuenta del BAD */
  XUA = 'XUA',
  /** Rial yemení */
  YER = 'YER',
  /** Rand */
  ZAR = 'ZAR',
  /** Kwacha zambiano */
  ZMW = 'ZMW',
  /** Dólar zimbabuense */
  ZWL = 'ZWL',
}

/**
 * Información extendida de las monedas (Descripción y Símbolo)
 */
export const CodigoMonedaInfo: Record<CodigoMoneda, { description: string, symbol: string }> = {
  [CodigoMoneda.AED]: { description: 'Dirham de los Emiratos Árabes Unidos', symbol: 'د.إ' },
  [CodigoMoneda.AFN]: { description: 'Afghani', symbol: '؋' },
  [CodigoMoneda.ALL]: { description: 'Lek', symbol: 'L' },
  [CodigoMoneda.AMD]: { description: 'Dram armenio', symbol: '֏' },
  [CodigoMoneda.ANG]: { description: 'Florín antillano neerlandés', symbol: 'ƒ' },
  [CodigoMoneda.AOA]: { description: 'Kwanza', symbol: 'Kz' },
  [CodigoMoneda.ARS]: { description: 'Peso Argentino', symbol: '$' },
  [CodigoMoneda.AUD]: { description: 'Dólar australiano', symbol: 'A$' },
  [CodigoMoneda.AWG]: { description: 'Florín arubeño', symbol: 'ƒ' },
  [CodigoMoneda.AZN]: { description: 'Manat azerbaiyano', symbol: '₼' },
  [CodigoMoneda.BAM]: { description: 'Marco bosnioherzegovino', symbol: 'KM' },
  [CodigoMoneda.BBD]: { description: 'Dólar de Barbados', symbol: 'Bds$' },
  [CodigoMoneda.BDT]: { description: 'Taka', symbol: '৳' },
  [CodigoMoneda.BGN]: { description: 'Lev búlgaro', symbol: 'лв' },
  [CodigoMoneda.BHD]: { description: 'Dinar de Bahrein', symbol: '.د.ب' },
  [CodigoMoneda.BIF]: { description: 'Franco Burundi', symbol: 'FBu' },
  [CodigoMoneda.BMD]: { description: 'Dólar de Bermudas', symbol: '$' },
  [CodigoMoneda.BND]: { description: 'Dólar de Brunei', symbol: '$' },
  [CodigoMoneda.BOB]: { description: 'Boliviano', symbol: 'Bs.' },
  [CodigoMoneda.BOV]: { description: 'Mvdol', symbol: 'Mvdol' },
  [CodigoMoneda.BRL]: { description: 'Real Brasileño', symbol: 'R$' },
  [CodigoMoneda.BSD]: { description: 'Dólar de las Bahamas', symbol: '$' },
  [CodigoMoneda.BTN]: { description: 'Ngultrum', symbol: 'Nu.' },
  [CodigoMoneda.BWP]: { description: 'Pula', symbol: 'P' },
  [CodigoMoneda.BYR]: { description: 'Rublo bielorruso', symbol: 'Br' },
  [CodigoMoneda.BZD]: { description: 'Dólar de Belice', symbol: 'BZ$' },
  [CodigoMoneda.CAD]: { description: 'Dólar canadiense', symbol: 'C$' },
  [CodigoMoneda.CDF]: { description: 'Franco congoleño', symbol: 'FC' },
  [CodigoMoneda.CHF]: { description: 'Franco suizo', symbol: 'CHF' },
  [CodigoMoneda.CLF]: { description: 'Unidad de Fomento', symbol: 'UF' },
  [CodigoMoneda.CLP]: { description: 'Peso chileno', symbol: '$' },
  [CodigoMoneda.CNY]: { description: 'Yuan', symbol: '¥' },
  [CodigoMoneda.COP]: { description: 'Peso Colombiano', symbol: '$' },
  [CodigoMoneda.COU]: { description: 'Unidad de Valor Real', symbol: 'COU' },
  [CodigoMoneda.CRC]: { description: 'Colón costarricense', symbol: '₡' },
  [CodigoMoneda.CUC]: { description: 'Peso Convertible', symbol: '$' },
  [CodigoMoneda.CUP]: { description: 'Peso Cubano', symbol: '$' },
  [CodigoMoneda.CVE]: { description: 'Cabo Verde Escudo', symbol: '$' },
  [CodigoMoneda.CZK]: { description: 'Corona checa', symbol: 'Kč' },
  [CodigoMoneda.DJF]: { description: 'Franco de Djibouti', symbol: 'Fdj' },
  [CodigoMoneda.DKK]: { description: 'Corona danesa', symbol: 'kr' },
  [CodigoMoneda.DOP]: { description: 'Peso Dominicano', symbol: 'RD$' },
  [CodigoMoneda.DZD]: { description: 'Dinar argelino', symbol: 'د.ج' },
  [CodigoMoneda.EGP]: { description: 'Libra egipcia', symbol: '£' },
  [CodigoMoneda.ERN]: { description: 'Nakfa', symbol: 'Nfk' },
  [CodigoMoneda.ETB]: { description: 'Birr etíope', symbol: 'Br' },
  [CodigoMoneda.EUR]: { description: 'Euro', symbol: '€' },
  [CodigoMoneda.FJD]: { description: 'Dólar de Fiji', symbol: 'FJ$' },
  [CodigoMoneda.FKP]: { description: 'Libra malvinense', symbol: '£' },
  [CodigoMoneda.GBP]: { description: 'Libra esterlina', symbol: '£' },
  [CodigoMoneda.GEL]: { description: 'Lari', symbol: '₾' },
  [CodigoMoneda.GHS]: { description: 'Cedi de Ghana', symbol: 'GH₵' },
  [CodigoMoneda.GIP]: { description: 'Libra de Gibraltar', symbol: '£' },
  [CodigoMoneda.GMD]: { description: 'Dalasi', symbol: 'D' },
  [CodigoMoneda.GNF]: { description: 'Franco guineano', symbol: 'FG' },
  [CodigoMoneda.GTQ]: { description: 'Quetzal', symbol: 'Q' },
  [CodigoMoneda.GYD]: { description: 'Dólar guyanés', symbol: '$' },
  [CodigoMoneda.HKD]: { description: 'Dolar de Hong Kong', symbol: 'HK$' },
  [CodigoMoneda.HNL]: { description: 'Lempira', symbol: 'L' },
  [CodigoMoneda.HRK]: { description: 'Kuna', symbol: 'kn' },
  [CodigoMoneda.HTG]: { description: 'Gourde', symbol: 'G' },
  [CodigoMoneda.HUF]: { description: 'Florín', symbol: 'Ft' },
  [CodigoMoneda.IDR]: { description: 'Rupia', symbol: 'Rp' },
  [CodigoMoneda.ILS]: { description: 'Nuevo Shekel Israelí', symbol: '₪' },
  [CodigoMoneda.INR]: { description: 'Rupia india', symbol: '₹' },
  [CodigoMoneda.IQD]: { description: 'Dinar iraquí', symbol: 'ع.د' },
  [CodigoMoneda.IRR]: { description: 'Rial iraní', symbol: '﷼' },
  [CodigoMoneda.ISK]: { description: 'Corona islandesa', symbol: 'kr' },
  [CodigoMoneda.JMD]: { description: 'Dólar jamaiquino', symbol: 'J$' },
  [CodigoMoneda.JOD]: { description: 'Dinar jordano', symbol: 'د.ا' },
  [CodigoMoneda.JPY]: { description: 'Yen', symbol: '¥' },
  [CodigoMoneda.KES]: { description: 'Chelín keniano', symbol: 'KSh' },
  [CodigoMoneda.KGS]: { description: 'Som', symbol: 'сом' },
  [CodigoMoneda.KHR]: { description: 'Riel', symbol: '៛' },
  [CodigoMoneda.KMF]: { description: 'Franco Comoro', symbol: 'CF' },
  [CodigoMoneda.KPW]: { description: 'Won norcoreano', symbol: '₩' },
  [CodigoMoneda.KRW]: { description: 'Won', symbol: '₩' },
  [CodigoMoneda.KWD]: { description: 'Dinar kuwaití', symbol: 'د.ك' },
  [CodigoMoneda.KYD]: { description: 'Dólar de las Islas Caimán', symbol: '$' },
  [CodigoMoneda.KZT]: { description: 'Tenge', symbol: '₸' },
  [CodigoMoneda.LAK]: { description: 'Kip', symbol: '₭' },
  [CodigoMoneda.LBP]: { description: 'Libra libanesa', symbol: 'ل.ل' },
  [CodigoMoneda.LKR]: { description: 'Rupia de Sri Lanka', symbol: '₨' },
  [CodigoMoneda.LRD]: { description: 'Dólar liberiano', symbol: '$' },
  [CodigoMoneda.LSL]: { description: 'Loti', symbol: 'L' },
  [CodigoMoneda.LYD]: { description: 'Dinar libio', symbol: 'ل.د' },
  [CodigoMoneda.MAD]: { description: 'Dirham marroquí', symbol: 'د.م.' },
  [CodigoMoneda.MDL]: { description: 'Leu moldavo', symbol: 'L' },
  [CodigoMoneda.MGA]: { description: 'Ariary malgache', symbol: 'Ar' },
  [CodigoMoneda.MKD]: { description: 'Denar', symbol: 'ден' },
  [CodigoMoneda.MMK]: { description: 'Kyat', symbol: 'K' },
  [CodigoMoneda.MNT]: { description: 'Tugrik', symbol: '₮' },
  [CodigoMoneda.MOP]: { description: 'Pataca', symbol: 'MOP$' },
  [CodigoMoneda.MRO]: { description: 'Ouguiya', symbol: 'UM' },
  [CodigoMoneda.MUR]: { description: 'Rupia de Mauricio', symbol: '₨' },
  [CodigoMoneda.MVR]: { description: 'Rufiyaa', symbol: 'Rf' },
  [CodigoMoneda.MWK]: { description: 'Kwacha', symbol: 'MK' },
  [CodigoMoneda.MXN]: { description: 'Peso Mexicano', symbol: '$' },
  [CodigoMoneda.MXV]: { description: 'Unidad de Inversion Mexicana (UDI)', symbol: 'UDI' },
  [CodigoMoneda.MYR]: { description: 'Ringgit malayo', symbol: 'RM' },
  [CodigoMoneda.MZN]: { description: 'Metical mozambiqueño', symbol: 'MT' },
  [CodigoMoneda.NAD]: { description: 'Dólar de Namibia', symbol: '$' },
  [CodigoMoneda.NGN]: { description: 'Naira', symbol: '₦' },
  [CodigoMoneda.NIO]: { description: 'Cordoba Oro', symbol: 'C$' },
  [CodigoMoneda.NOK]: { description: 'Corona noruega', symbol: 'kr' },
  [CodigoMoneda.NPR]: { description: 'Rupia nepalí', symbol: '₨' },
  [CodigoMoneda.NZD]: { description: 'Dólar neozelandés', symbol: 'NZ$' },
  [CodigoMoneda.OMR]: { description: 'Rial omaní', symbol: '﷼' },
  [CodigoMoneda.PAB]: { description: 'Balboa', symbol: 'B/.' },
  [CodigoMoneda.PEN]: { description: 'Nuevo Sol', symbol: 'S/' },
  [CodigoMoneda.PGK]: { description: 'Kina', symbol: 'K' },
  [CodigoMoneda.PHP]: { description: 'Peso filipino', symbol: '₱' },
  [CodigoMoneda.PKR]: { description: 'Rupia pakistaní', symbol: '₨' },
  [CodigoMoneda.PLN]: { description: 'Zloty', symbol: 'zł' },
  [CodigoMoneda.PYG]: { description: 'Guaraní', symbol: '₲' },
  [CodigoMoneda.QAR]: { description: 'Riyal catarí', symbol: '﷼' },
  [CodigoMoneda.RON]: { description: 'Leu rumano', symbol: 'lei' },
  [CodigoMoneda.RSD]: { description: 'Dinar serbio', symbol: 'дин' },
  [CodigoMoneda.RUB]: { description: 'Rublo ruso', symbol: '₽' },
  [CodigoMoneda.RWF]: { description: 'Franco ruandés', symbol: 'FRw' },
  [CodigoMoneda.SAR]: { description: 'Riyal saudí', symbol: '﷼' },
  [CodigoMoneda.SBD]: { description: 'Dólar de las Islas Salomón', symbol: 'SI$' },
  [CodigoMoneda.SCR]: { description: 'Rupia seychelense', symbol: '₨' },
  [CodigoMoneda.SDG]: { description: 'Libra sudanesa', symbol: 'ج.س.' },
  [CodigoMoneda.SEK]: { description: 'Corona sueca', symbol: 'kr' },
  [CodigoMoneda.SGD]: { description: 'Dolar de Singapur', symbol: 'S$' },
  [CodigoMoneda.SHP]: { description: 'Libra de Santa Helena', symbol: '£' },
  [CodigoMoneda.SLL]: { description: 'Leone', symbol: 'Le' },
  [CodigoMoneda.SOS]: { description: 'Chelín somalí', symbol: 'S' },
  [CodigoMoneda.SRD]: { description: 'Dólar surinamés', symbol: '$' },
  [CodigoMoneda.SSP]: { description: 'Libra sursudanesa', symbol: '£' },
  [CodigoMoneda.STD]: { description: 'Dobra', symbol: 'Db' },
  [CodigoMoneda.SVC]: { description: 'Colón', symbol: '₡' },
  [CodigoMoneda.SYP]: { description: 'Libra Siria', symbol: '£' },
  [CodigoMoneda.SZL]: { description: 'Lilangeni', symbol: 'E' },
  [CodigoMoneda.THB]: { description: 'Baht', symbol: '฿' },
  [CodigoMoneda.TJS]: { description: 'Somoni', symbol: 'SM' },
  [CodigoMoneda.TMT]: { description: 'Manat turcomano', symbol: 'T' },
  [CodigoMoneda.TND]: { description: 'Dinar tunecino', symbol: 'د.ت' },
  [CodigoMoneda.TOP]: { description: 'Pa\'anga', symbol: 'T$' },
  [CodigoMoneda.TRY]: { description: 'Lira turca', symbol: '₺' },
  [CodigoMoneda.TTD]: { description: 'Dólar trinitense', symbol: 'TT$' },
  [CodigoMoneda.TWD]: { description: 'Nuevo dólar taiwanés', symbol: 'NT$' },
  [CodigoMoneda.TZS]: { description: 'Chelín tanzano', symbol: 'TSh' },
  [CodigoMoneda.UAH]: { description: 'Hryvnia', symbol: '₴' },
  [CodigoMoneda.UGX]: { description: 'Chelín ugandés', symbol: 'USh' },
  [CodigoMoneda.USD]: { description: 'Dólar estadounidense', symbol: '$' },
  [CodigoMoneda.USN]: { description: 'Dólar Americanó (Next day)', symbol: '$' },
  [CodigoMoneda.UYI]: { description: 'Uruguay Peso en Unidades Indexadas (URUIURUI)', symbol: 'UI' },
  [CodigoMoneda.UYU]: { description: 'Peso Uruguayo', symbol: '$U' },
  [CodigoMoneda.UZS]: { description: 'Som uzbeko', symbol: 'soʻm' },
  [CodigoMoneda.VEF]: { description: 'Bolívar', symbol: 'Bs.F' },
  [CodigoMoneda.VND]: { description: 'Dong', symbol: '₫' },
  [CodigoMoneda.VUV]: { description: 'Vatu', symbol: 'VT' },
  [CodigoMoneda.WST]: { description: 'Tala', symbol: 'WS$' },
  [CodigoMoneda.XAF]: { description: 'Franco CFA BEAC', symbol: 'FCFA' },
  [CodigoMoneda.XCD]: { description: 'Dólar del Caribe Oriental', symbol: '$' },
  [CodigoMoneda.XDR]: { description: 'SDR (Derechos Especiales de Giro)', symbol: 'SDR' },
  [CodigoMoneda.XOF]: { description: 'Franco CFA BCEAO', symbol: 'CFA' },
  [CodigoMoneda.XPF]: { description: 'Franco CFP', symbol: '₣' },
  [CodigoMoneda.XSU]: { description: 'Sucre', symbol: 'Sucre' },
  [CodigoMoneda.XUA]: { description: 'Unidad de cuenta del BAD', symbol: 'XUA' },
  [CodigoMoneda.YER]: { description: 'Rial yemení', symbol: '﷼' },
  [CodigoMoneda.ZAR]: { description: 'Rand', symbol: 'R' },
  [CodigoMoneda.ZMW]: { description: 'Kwacha zambiano', symbol: 'ZK' },
  [CodigoMoneda.ZWL]: { description: 'Dólar zimbabuense', symbol: '$' },
};

/**
 * Código de Impuesto (Códigos Hacienda)
 */
export enum CodigoImpuesto {
  /** Impuesto al Valor Agregado */
  ImpuestoValorAgregado = '01',
  /** Impuesto Selectivo de Consumo */
  ImpuestoSelectivoConsumo = '02',
  /** Impuesto Único a los Combustibles */
  ImpuestoUnicoCombustibles = '03',
  /** Impuesto Específico a Bebidas Alcohólicas */
  ImpuestoEspecificoBebidasAlcoholicas = '04',
  /** Impuesto Específico a Bebidas Envasadas sin Alcohol y Jabones */
  ImpuestoEspecificoBebidasEnvasadasSinAlcoholYJabones = '05',
  /** Impuesto a Productos de Tabaco */
  ImpuestoProductosTabaco = '06',
  /** IVA con Cálculo Especial */
  ImpuestoIVACalculoEspecial = '07',
  /** IVA Régimen Bienes Usados - Factor */
  IVARegimenBienesUsadosFactor = '08',
  /** Impuesto Específico al Cemento */
  ImpuestoEspecificoCemento = '12',
  /** Otros impuestos */
  Otros = '99',
}

/**
 * Código de Descuento (Códigos Hacienda)
 */
export enum CodigoDescuento {
  /** Descuento por regalía */
  DescuentoRegalia = '01',
  /** Descuento por regalía con IVA cobrado al cliente */
  DescuentoRegaliaIVACobradoCliente = '02',
  /** Descuento por bonificación */
  DescuentoBonificacion = '03',
  /** Descuento por volumen */
  DescuentoVolumen = '04',
  /** Descuento por temporada */
  DescuentoTemporada = '05',
  /** Descuento promocional */
  DescuentoPromocional = '06',
  /** Descuento comercial */
  DescuentoComercial = '07',
  /** Descuento por frecuencia */
  DescuentoFrecuencia = '08',
  /** Descuento sostenido */
  DescuentoSostenido = '09',
  /** Otro tipo de descuento */
  Otro = '99',
}

/**
 * Código de Referencia (Códigos Hacienda)
 */
export enum CodigoReferencia {
  /** Anula documento de referencia */
  AnulaDocumentoReferencia = '01',
  /** Corrige monto */
  CorrigeMonto = '02',
  /** Corrige texto de documento de referencia */
  CorrigeTextoDocumentoReferencia = '03',
  /** Referencia otro documento */
  ReferenciaOtroDocumento = '04',
  /** Sustituye comprobante provisional por contingencia */
  SustituyeComprobanteProvisionalContingencia = '05',
  /** Devolución de mercancía */
  DevolucionMercancia = '06',
  /** Sustituye comprobante electrónico */
  SustituyeComprobanteElectronico = '07',
  /** Factura endosada */
  FacturaEndosada = '08',
  /** Nota de crédito financiera */
  NotaCreditoFinanciera = '09',
  /** Nota de débito financiera */
  NotaDebitoFinanciera = '10',
  /** Proveedor no domiciliado */
  ProveedorNoDomiciliado = '11',
  /** Crédito por exoneración posterior a la facturación */
  CreditoExoneracionPosteriorFacturacion = '12',
  /** Otros */
  Otros = '99',
}

/**
 * Tipo de Documento de Referencia (Códigos Hacienda)
 */
export enum TipoDocumentoReferencia {
  /** Factura Electrónica */
  FacturaElectronica = '01',
  /** Nota de Débito Electrónica */
  NotaDebitoElectronica = '02',
  /** Nota de Crédito Electrónica */
  NotaCreditoElectronica = '03',
  /** Tiquete Electrónico */
  TiqueteElectronico = '04',
  /** Nota de Despacho */
  NotaDespacho = '05',
  /** Contrato */
  Contrato = '06',
  /** Procedimiento */
  Procedimiento = '07',
  /** Comprobante Emitido en Contingencia */
  ComprobanteEmitidoContingencia = '08',
  /** Devolución de Mercadería */
  DevolucionMercaderia = '09',
  /** Comprobante Rechazado por Hacienda */
  ComprobanteRechazadoHacienda = '10',
  /** Sustituye Factura Rechazada por Receptor */
  SustituyeFacturaRechazadaPorReceptor = '11',
  /** Sustituye Factura de Exportación */
  SustituyeFacturaExportacion = '12',
  /** Facturación Mes Vencido */
  FacturacionMesVencido = '13',
  /** Comprobante Aportado Régimen Especial */
  ComprobanteAportadoRegimenEspecial = '14',
  /** Sustituye Factura Electrónica de Compra */
  SustituyeFacturaElectronicaCompra = '15',
  /** Comprobante Proveedor No Domiciliado */
  ComprobanteProveedorNoDomiciliado = '16',
  /** Nota de Crédito Factura Electrónica de Compra */
  NotaCreditoFacturaElectronicaCompra = '17',
  /** Nota de Débito Factura Electrónica de Compra */
  NotaDebitoFacturaElectronicaCompra = '18',
  /** Otros */
  Otros = '99',
}

/**
 * Tipo de Documento de Autorización de Exoneración (Códigos Hacienda)
 */
export enum TipoDocumentoAutorizacionExoneracion {
  /** Compras autorizadas por la DGT */
  ComprasAutorizadasDGT = '01',
  /** Ventas exentas a diplomáticos */
  VentasExentasDiplomaticos = '02',
  /** Autorizado por ley especial */
  AutorizadoLeyEspecial = '03',
  /** Exenciones DGH local genérica */
  ExencionesDGHLocalGenerica = '04',
  /** Exenciones DGH transitorio V */
  ExencionesDGHTransitorioV = '05',
  /** Servicios turísticos inscritos en ICT */
  ServiciosTuristicosInscritosICT = '06',
  /** Transitorio XVII reciclaje */
  TransitorioXVIIReciclaje = '07',
  /** Exoneración zona franca */
  ExoneracionZonaFranca = '08',
  /** Exoneración servicios complementarios exportación */
  ExoneracionServiciosComplementariosExportacion = '09',
  /** Órganos y corporaciones municipales */
  OrganosCorporacionesMunicipales = '10',
  /** Exenciones DGH impuesto local concreta */
  ExencionesDGHImpuestoLocalConcreta = '11',
  /** Otros */
  Otros = '99',
}

/**
 * Tarifa del IVA (Códigos Hacienda)
 */
export enum TarifaIVA {
  /** Tarifa 0% */
  Tarifa0PorCiento = '01',
  /** Tarifa reducida 1% */
  TarifaReducida1PorCiento = '02',
  /** Tarifa reducida 2% */
  TarifaReducida2PorCiento = '03',
  /** Tarifa reducida 4% */
  TarifaReducida4PorCiento = '04',
  /** Tarifa transitorio 0% */
  TarifaTransitorio0PorCiento = '05',
  /** Tarifa transitorio 4% */
  TarifaTransitorio4PorCiento = '06',
  /** Tarifa transitoria 8% */
  TarifaTransitoria8PorCiento = '07',
  /** Tarifa general 13% */
  TarifaGeneral13PorCiento = '08',
  /** Tarifa reducida 0.5% */
  TarifaReducida0Punto5PorCiento = '09',
  /** Tarifa exenta */
  TarifaExenta = '10',
  /** Tarifa 0% sin crédito */
  Tarifa0PorCientoSinCredito = '11',
}

/**
 * Situación de Comprobante Electrónico (Códigos Hacienda)
 */
export enum SituacionComprobante {
  /** Normal */
  Normal = '1',
  /** Contingencia */
  Contingencia = '2',
  /** Sin Internet */
  SinInternet = '3',
}

/**
 * Unidades de Medida (Códigos Hacienda)
 */
export enum UnidadMedida {
  /** Uno */
  Uno = '1',
  /** minuto */
  minuto = '´',
  /** segundo */
  segundo = '´´',
  /** Grado Celsius */
  GradoCelsius = '°C',
  /** Uno por Metro */
  UnoPorMetro = '1/m',
  /** Ampere */
  Ampere = 'A',
  /** Ampere por Metro */
  AmperePorMetro = 'A/m',
  /** Ampere por Metro Cuadrado */
  AmperePorMetroCuadrado = 'A/m²',
  /** Activo Virtual */
  ActivoVirtual = 'Acv',
  /** Alquiler Habitacional */
  AlquilerHabitacional = 'Al',
  /** Alquiler Comercial */
  AlquilerComercial = 'Alc',
  /** Bel */
  Bel = 'B',
  /** Becquerel */
  Becquerel = 'Bq',
  /** Coulomb */
  Coulomb = 'C',
  /** Coulomb por Kilogramo */
  CoulombPorKilogramo = 'C/kg',
  /** Coulomb por Metro Cuadrado */
  CoulombPorMetroCuadrado = 'C/m²',
  /** Coulomb por Metro Cúbico */
  CoulombPorMetroCubico = 'C/m³',
  /** Cajuela de Café */
  CajuelaCafe = 'Cc',
  /** Candela */
  Candela = 'Cd',
  /** Candela por Metro Cuadrado */
  CandelaPorMetroCuadrado = 'cd/m²',
  /** Comisión */
  Comision = 'Cm',
  /** Centímetro */
  Centimetro = 'cm',
  /** Cuartillo de Café */
  CuartilloCafe = 'Cu',
  /** Día */
  Dia = 'D',
  /** Electronvolt */
  Electronvolt = 'eV',
  /** Farad */
  Farad = 'F',
  /** Farad por Metro */
  FaradPorMetro = 'F/m',
  /** Fanega de Café */
  FanegaCafe = 'Fa',
  /** Gramo */
  Gramo = 'G',
  /** Galón */
  Galon = 'Gal',
  /** Gray */
  Gray = 'Gy',
  /** Gray por Segundo */
  GrayPorSegundo = 'Gy/s',
  /** Hora */
  Hora = 'H',
  /** Henry */
  Henry = 'h',
  /** Henry por Metro */
  HenryPorMetro = 'H/m',
  /** Hertz */
  Hertz = 'Hz',
  /** Intereses */
  Intereses = 'I',
  /** Joule */
  Joule = 'J',
  /** Joule por Kilogramo Kelvin */
  JoulePorKilogramoKelvin = 'J/(kg·K)',
  /** Joule por Mol Kelvin */
  JoulePorMolKelvin = 'J/(mol·K)',
  /** Joule por Kelvin */
  JoulePorKelvin = 'J/K',
  /** Joule por Kilogramo */
  JoulePorKilogramo = 'J/kg',
  /** Joule por Metro Cúbico */
  JoulePorMetroCubico = 'J/m³',
  /** Joule por Mol */
  JoulePorMol = 'J/mol',
  /** Kelvin */
  Kelvin = 'K',
  /** Katal */
  Katal = 'Kat',
  /** Katal por Metro Cúbico */
  KatalPorMetroCubico = 'kat/m³',
  /** Kilogramo */
  Kilogramo = 'Kg',
  /** Kilogramo por Metro Cúbico */
  KilogramoPorMetroCubico = 'kg/m³',
  /** Kilómetro */
  Kilometro = 'Km',
  /** Kilovatios */
  Kilovatios = 'Kw',
  /** Kilovatios Hora */
  KilovatiosHora = 'kWh',
  /** Litro */
  Litro = 'L',
  /** Lumen */
  Lumen = 'Lm',
  /** Pulgada */
  Pulgada = 'Ln',
  /** Lux */
  Lux = 'Lx',
  /** Metro */
  Metro = 'M',
  /** Metro por Segundo */
  MetroPorSegundo = 'm/s',
  /** Metro por Segundo Cuadrado */
  MetroPorSegundoCuadrado = 'm/s²',
  /** Metro Cuadrado */
  MetroCuadrado = 'm²',
  /** Metro Cúbico */
  MetroCubico = 'm³',
  /** Minuto */
  Minuto = '´',
  /** Mililitro */
  Mililitro = 'mL',
  /** Milímetro */
  Milimetro = 'Mm',
  /** Mol */
  Mol = 'Mol',
  /** Mol por Metro Cúbico */
  MolPorMetroCubico = 'mol/m³',
  /** Newton */
  Newton = 'N',
  /** Newton por Metro */
  NewtonPorMetro = 'N/m',
  /** Newton Metro */
  NewtonMetro = 'N·m',
  /** Neper */
  Neper = 'Np',
  /** Grado */
  Grado = 'º',
  /** Otro Servicio */
  OtroServicio = 'Os',
  /** Otros */
  Otros = 'Otros',
  /** Onzas */
  Onzas = 'Oz',
  /** Pascal */
  Pascal = 'Pa',
  /** Pascal Segundo */
  PascalSegundo = 'Pa·s',
  /** Quintal */
  Quintal = 'Qq',
  /** Radián */
  Radian = 'Rad',
  /** Radián por Segundo */
  RadianPorSegundo = 'rad/s',
  /** Radián por Segundo Cuadrado */
  RadianPorSegundoCuadrado = 'rad/s²',
  /** Segundo */
  Segundo = 'S',
  /** Siemens */
  Siemens = 's',
  /** Servicios Profesionales */
  ServiciosProfesionales = 'Sp',
  /** Servicios Personales */
  ServiciosPersonales = 'Spe',
  /** Estereorradián */
  Estereorradian = 'Sr',
  /** Servicios Técnicos */
  ServiciosTecnicos = 'St',
  /** Sievert */
  Sievert = 'Sv',
  /** Tesla */
  Tesla = 't',
  /** Tonelada */
  Tonelada = 'T',
  /** Unidad de Masa Atómica */
  UnidadMasaAtomica = 'U',
  /** Unidad Astronómica */
  UnidadAstronomica = 'Ua',
  /** Unidad */
  Unidad = 'Unid',
  /** Volt */
  Volt = 'V',
  /** Volt por Metro */
  VoltPorMetro = 'V/m',
  /** Watt */
  Watt = 'W',
  /** Watt por Metro Kelvin */
  WattPorMetroKelvin = 'W/(m·K)',
  /** Watt por Metro Cuadrado Estereorradián */
  WattPorMetroCuadradoEstereorradian = 'W/(m²·sr)',
  /** Watt por Metro Cuadrado */
  WattPorMetroCuadrado = 'W/m²',
  /** Watt por Estereorradián */
  WattPorEstereorradian = 'W/sr',
  /** Weber */
  Weber = 'Wb',
  /** Ohm */
  Ohm = 'Ω',
}

/**
 * Estado de una notificación
 */
export enum NotificationType {
  Info = 0,
  Success = 1,
  Warning = 2,
  Error = 3,
  SystemAlert = 4,
}

/**
 * Estado de un envío a Hacienda
 */
export enum HaciendaEstado {
  Pendiente = 0,
  Enviado = 1,
  Aceptado = 2,
  Rechazado = 3,
  Error = 4,
  Procesando = 5,
}

/**
 * Tipo de Mensaje (Hacienda)
 */
export enum TipoMensaje {
  Aceptado = '1',
  AceptacionParcial = '2',
  Rechazado = '3',
}

// --- LICENCIAS ---

export enum LicenseType {
    Server = "Server",
    AndroidPOS = "AndroidPOS",
    WindowsPOS = "WindowsPOS",
    Kiosk = "Kiosk",
    RestaurantPOS = "RestaurantPOS",
    RetailPOS = "RetailPOS",
    Terminal = "Terminal"
}

export enum LicenseStatus {
    Active = "Active",
    Revoked = "Revoked",
    Suspended = "Suspended",
    Expired = "Expired"
}

export enum LicenseAction {
    Issue = "Issue",
    Validate = "Validate",
    Activate = "Activate",
    Revoke = "Revoke",
    Suspend = "Suspend",
    OfflineToken = "OfflineToken",
    ValidationFailed = "ValidationFailed"
}

export enum LicensePlatform {
    Android = "Android",
    Windows = "Windows"
}

export enum LicenseAddonStatus {
    PendingPayment = "PendingPayment",
    Active = "Active",
    Cancelled = "Cancelled"
}

export enum ModuleType {
    Platform = 0,
    License = 1
}
