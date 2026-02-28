import {
    TipoComprobante,
    TipoIdentificacion,
    CondicionVenta,
    TipoMedioPago,
    CodigoMoneda,
    CodigoImpuesto,
    HaciendaEstado,
    LicenseType,
    LicensePlatform
} from './enums';

export interface CodigoComercialRequest {
    tipo: string;
    codigo: string;
}

export interface GenerarDocumentoRequest {
    tipoDocumento: string;
    clave: ClaveRequest;
    consecutivo: ConsecutivoRequest;
    codigoActividadEmisor: string;
    codigoActividadReceptor: string;
    fechaEmision: string; // ISO Date
    proveedorSistemas: string;
    emisor: EmisorRequest;
    receptor: ReceptorRequest;
    condicionVenta: string;
    condicionVentaOtros?: CondicionVentaOtro;
    plazoCredito?: number;
    detalleServicio: DetalleServicioRequest;
    resumenFactura?: ResumenFacturaRequest;
    informacionReferencia?: InformacionReferenciaRequest[];
    otrosCargos?: OtrosCargosRequest[];
    otrosDatos?: OtrosRequest;
}

export interface ClaveRequest {
    codigoPais: number;
    dia: number;
    mes: number;
    ano: number;
    consecutivo: ConsecutivoRequest;
    cedulaEmisor: string;
    situacion: string;
}

export interface ConsecutivoRequest {
    establecimiento: number;
    terminal: number;
    numeroConsecutivo: number;
    tipoComprobante: string;
}

export interface IdentificacionRequest {
    tipo: string;
    numero: string;
}

export interface EmisorRequest {
    identificacion: IdentificacionRequest;
    nombre: string;
    nombreComercial?: string;
    ubicacion: UbicacionRequest;
    telefono: TelefonoRequest;
    correoElectronico: string[];
    registroFiscal8707?: string;
    seFacturanBebidasAlc: boolean;
}

export interface TelefonoRequest {
    codigoPais: number;
    numero: number;
}

export interface ReceptorRequest {
    identificacion: IdentificacionRequest;
    nombre: string;
    identificacionExtranjero?: string;
    nombreComercial?: string;
    ubicacion?: UbicacionRequest;
    otrasSenasExtranjero?: string;
    telefono?: TelefonoRequest;
    correoElectronico: string;
}

export interface UbicacionRequest {
    provincia: string;
    canton: string;
    distrito: string;
    barrio: string;
    otrasSenas: string;
}

export interface DetalleServicioRequest {
    lineasDetalle: LineaDetalleRequest[];
}

export interface LineaDetalleRequest {
    numeroLinea?: number;
    partidaArancelaria?: string;
    codigoCABYS?: string;
    codigosComerciales: CodigoComercialRequest[];
    cantidad: number;
    unidadMedida: string;
    tipoTransaccion: string;
    unidadMedidaComercial?: string;
    detalle?: string;
    numeroVINoSerie?: string[];
    registroMedicamento?: string;
    formaFarmaceutica?: string;
    precioUnitario: number;
    montoTotal?: number;
    descuentos: DescuentoRequest[];
    subTotal?: number;
    ivaCobradoFabrica?: string;
    baseImponible?: number;
    impuestos: ImpuestoRequest[];
    impuestoAsumidoEmisorFabrica?: number;
    impuestoNeto?: number;
    montoTotalLinea?: number;
}

export interface DescuentoRequest {
    montoDescuento?: number;
    codigoDescuento?: string;
    codigoDescuentoOtro?: string;
    naturalezaDescuento?: string;
}

export interface ImpuestoRequest {
    codigo: string;
    codigoImpuestoOTRO?: string;
    codigoTarifaIVA: string;
    tarifa?: number;
    factorCalculoIVA?: number;
    monto?: number;
    exoneracion?: ExoneracionRequest;
}

export interface ExoneracionRequest {
    tipoDocumentoEX1: string;
    tipoDocumentoOTRO?: string;
    numeroDocumento: string;
    articulo: number;
    inciso: number;
    nombreInstitucion: string;
    nombreInstitucionOtros?: string;
    fechaEmisionEX: string; // ISO Date or DateOffset
    tarifaExonerada: number;
    montoExoneracion: number;
}

export interface ResumenFacturaRequest {
    codigoTipoMoneda?: CodigoTipoMonedaRequest;
    totalServGravados?: number;
    totalServExentos?: number;
    totalServExonerado?: number;
    totalServNoSujeto?: number;
    totalMercanciasGravadas?: number;
    totalMercanciasExentas?: number;
    totalMercExonerada?: number;
    totalMercNoSujeta?: number;
    totalGravado?: number;
    totalExento?: number;
    totalExonerado?: number;
    totalNoSujeto?: number;
    totalVenta?: number;
    totalDescuentos?: number;
    totalVentaNeta?: number;
    totalDesgloseImpuesto?: TotalDesgloseImpuestoRequest;
    totalImpuesto?: number;
    totalImpAsumEmisorFabrica?: number;
    totalIvaDevuelto?: number;
    totalOtrosCargos?: number;
    tipoMedioPago?: MedioPagoRequest;
    totalComprobante?: number;
}

export interface CodigoTipoMonedaRequest {
    codigoMoneda: string;
    tipoCambio: number;
}

export interface TotalDesgloseImpuestoRequest {
    codigo: string;
    codigoTarifaIVA: string;
    totalMontoImpuesto: number;
}

export interface MedioPagoRequest {
    tipoMedioPago?: string;
    medioPagoOtros?: string;
    totalMedioPago?: number;
}

export interface InformacionReferenciaRequest {
    tipoDocIR: string;
    tipoDocRefOTRO?: string;
    numero: string;
    fechaEmisionIR: string;
    codigo: string;
    codigoReferenciaOTRO?: string;
    razon: string;
}

export interface OtrosCargosRequest {
    tipoDocumentoOC: string;
    tipoDocumentoOTROS?: string;
    identificacionTercero: IdentificacionRequest;
    nombreTercero: string;
    detalle: string;
    porcentajeOC: number;
    montoCargo: number;
}

export interface OtrosRequest {
    otroTexto?: string[];
    otroContenido?: string;
}

export interface CondicionVentaOtro {
    descripcion: string;
}

/**
 * DTO simplificado para enviar documentos electrónicos.
 */
export interface EnviarDocumentoSimplificadoRequest {
    /** 01=FE, 02=ND, 03=NC, 04=Tiquete, 08=FEC, 09=FECE */
    tipoDocumento?: string;
    /** 01=Contado, 02=Crédito, etc. */
    condicionVenta?: string;
    /** Medios de pago: 01=Efectivo, 02=Tarjeta, etc. */
    medioPago?: string[];
    /** Lista detallada de medios de pago para facturas multipartes. */
    mediosPago?: MedioPagoRequest[];
    /** Código de moneda ISO (CRC, USD, EUR) */
    codigoMoneda?: string;
    /** Tipo de cambio respecto a CRC (1 si es CRC) */
    tipoCambio?: number;
    /** Plazo de crédito en días (solo si CondicionVenta=02) */
    plazoCredito?: number;
    /** Receptor/Cliente. Null = Consumidor Final / Sin Receptor. */
    receptor?: ReceptorRequest;
    /** Líneas de detalle del documento (requerido, mínimo 1) */
    lineas: LineaDetalleRequest[];
    /** Referencia para Notas de Crédito/Débito. */
    referencia?: ReferenciaSimplificada;
    /** Otros cargos (opcional) */
    otrosCargos?: OtrosCargosRequest[];
    /** Datos adicionales (opcional) */
    otrosDatos?: OtrosRequest;
    /** Código de actividad económica del emisor (opcional) */
    codigoActividadEmisor?: string;
    /** Código de actividad económica del receptor (opcional) */
    codigoActividadReceptor?: string;
}

/**
 * Referencia simplificada para NC/ND.
 */
export interface ReferenciaSimplificada {
    /** Clave numérica (50 dígitos) del documento que se referencia */
    claveDocumentoReferencia: string;
    /** Código de referencia Hacienda: 01=Anula, 02=Corrige texto, etc. */
    codigo?: string;
    /** Razón de la nota (descripción libre) */
    razon: string;
}

// --- TENANT ---

export interface UpdateTenantRequest {
    name?: string;
    phone?: string;
    taxId?: string;
    ownerEmail?: string;
    identificationType?: number;
    commercialName?: string;
    provinceCode?: string;
    cantonCode?: string;
    districtCode?: string;
    addressDetails?: string;
    economicActivityCode?: string;
    isActive?: boolean;
}

export interface RegisterTenantRequest {
    name: string;
    ownerEmail: string;
    taxId: string;
}

export interface HaciendaConfigRequest {
    idCia?: string;
    idSucursal?: string;
    pinP12?: string;
    certP12Base64?: string;
    usuarioOauth?: string;
    passwordOauth?: string;
    environment?: string; // "Sandbox" | "Production"
}

export interface SwitchEnvironmentRequest {
    environment: number | string;
}

export interface CreateBranchRequest {
    name: string;
    code: string;
    address?: string | null;
    phone?: string | null;
    managerName?: string | null;
}

export interface UpdateBranchRequest {
    name?: string | null;
    address?: string | null;
    phone?: string | null;
    managerName?: string | null;
    isActive?: boolean | null;
}

export interface CreateTerminalRequest {
    name: string;
    code: string;
}

export interface UpdateTerminalRequest {
    name?: string;
    isActive?: boolean;
}

export interface UpdateSequenceRequest {
    newCurrentValue: number;
}

// --- AUTH ---

export interface LoginRequest {
    username: string;
    password?: string;
    rememberMe?: boolean;
}

export interface ForgotPasswordRequest {
    email: string;
}

export interface ConfirmEmailRequest {
    email: string;
    token: string;
}

export interface ResetPasswordRequest {
    token: string;
    newPassword: string;
}

export interface RegisterRequest {
    companyName: string;
    taxId: string;
    email: string;
    password: string;
    tier?: number;
    sellerCode?: string;
}


export interface PartnerRegisterRequest {
    code: string;
    phoneNumber?: string;
    website?: string;
    bankName?: string;
    bankAccount?: string;
    taxId?: string;
}

export interface IssueLicenseRequest {
    type: LicenseType;
    platform: LicensePlatform | string | number;
    deviceName?: string;
    sourceAddonId?: string;
}

export interface LeadRegisterRequest {
    name: string;
    email: string;
    phone?: string;
    message?: string;
    source?: string;
}

export interface TenantRoleRequest {
    name: string;
    description: string;
    permissions: string[];
}

export interface MensajeEndosoRequest {
    clave: string;
    numeroCedulaEmisor: string;
    fechaEmisionDoc: string; // ISO Date
    detalleMensaje: string;
    montoTotalImpuesto: number;
    totalFactura: number;
    numeroCedulaReceptor: string;
    numeroConsecutivoReceptor: string;
    mensaje: number; // 1: Aceptado, 2: Parcial, 3: Rechazo, 4: Cesión
    nombreReceptor?: string;
    tipoIdentificacionReceptor?: string;
}

export interface CreateSettlementRequest {
    commissionIds: string[];
}

export interface RegisterSettlementPaymentRequest {
    paymentReference: string;
    paymentDate: string; // ISO
}

// --- LICENSES ---

export interface ValidateLicenseRequest {
    licenseKey: string;
    machineId: string;
    appVersion?: string;
    type?: number | string; // LicenseType enum
    platform?: number | string; // LicensePlatform enum
    appIdentifier?: string;
}

export interface ActivateLicenseRequest {
    licenseKey: string;
    machineId: string;
    deviceName?: string;
    type?: number | string;
    platform?: number | string;
    appIdentifier?: string;
}

export interface PurchaseAddonRequest {
    packageId: string;
}

export interface ReportAddonPurchaseRequest {
    packageId: string;
    reference: string;
    paymentMethod: string; // Use enum if available, or string
    amount: number;
}

export interface ApproveAddonRequest {
    adminNote?: string;
}

export interface RejectAddonRequest {
    reason: string;
}

export interface CreateLicensePackageRequest {
    name: string;
    description: string;
    type: string | number; // LicenseType enum value
    allowedPlatforms: (string | number)[]; // LicensePlatform enum value array
    quantity: number;
    monthlyPrice: number;
    currency?: string;
    isActive?: boolean;
    displayOrder?: number;
    defaultModules?: string | null;
    iconUrl?: string | null;
    appIdentifier?: string | null;
}

export interface UpdateLicensePackageRequest {
    name?: string;
    description?: string;
    type?: string | number;
    allowedPlatforms?: (string | number)[];
    quantity?: number;
    monthlyPrice?: number;
    currency?: string;
    isActive?: boolean;
    displayOrder?: number;
    defaultModules?: string | null;
    iconUrl?: string | null;
    appIdentifier?: string | null;
}

export interface RegisterAppRequest {
    appName: string;
    appSlug: string;
    description?: string;
    websiteUrl?: string | null;
    requiredType: string | number;
    requiredPlatform: string | number;
}

export interface CreateOrderRequest {
    items: CartItemDto[];
    sellerCode?: string;
}

export interface CartItemDto {
    type: number;
    id?: string;
    quantity: number;
}

export interface ReportOrderPaymentRequest {
    orderId: string;
    referenceNumber: string;
    paymentMethod: number;
}

export interface InviteUserRequest {
    email: string;
    role: number;
    customRoleId?: string | null;
    branchId?: string | null;
}

export interface UpdateUserRoleRequest {
    role: number;
    customRoleId?: string | null;
}

export interface AcceptInviteRequest {
    token: string;
    password?: string;
    fullName?: string;
}

export interface AdminRegisterRequest {
    email: string;
    password?: string;
    fullName: string;
    role: string;
}

export interface AdminUpdateRequest {
    email?: string;
    fullName?: string;
    role?: string;
    isActive?: boolean;
}

export interface ApprovalRejectRequest {
    reason: string;
}

export interface SettlementPayRequest {
    paymentReference: string;
    paymentDate: string;
}

// --- MODULES ---

export interface CreatePlatformModuleRequest {
    code: string;
    name: string;
    description: string;
    type: number; // ModuleType
    icon?: string;
    isActive: boolean;
    appIdentifier?: string;
}

export interface UpdatePlatformModuleRequest {
    name?: string;
    description?: string;
    type?: number;
    icon?: string;
    isActive?: boolean;
    appIdentifier?: string;
}
// --- DEVELOPER MODULES ---

export interface CreateDeveloperModuleRequest {
    code: string;
    name: string;
    description: string;
    type: number;
    isActive: boolean;
    appIdentifier?: string | null;
}

export interface UpdateDeveloperModuleRequest {
    name?: string;
    description?: string;
    type?: number;
    isActive?: boolean;
    appIdentifier?: string | null;
}
