import {
    LicenseType,
    LicenseStatus,
    LicensePlatform,
    HaciendaEstado
} from './enums.js';

// --- AUTH ---

export interface LoginResult {
    token: string;
    refreshToken?: string;
    tenantId?: string;
    requiresActivation?: boolean;
    message?: string;
}

export interface PasswordResetResponse {
    success: boolean;
    message: string;
}

export interface AdminMeResponse {
    id: string;
    email: string;
    role: string;
    isAdmin: string;
    permissions: string[];
}

export interface ImpersonateResponse {
    token: string;
    tenantId: string;
}

// --- DOCUMENTS ---

export interface DocumentoResult {
    xmlFirmado: string;
    clave: string;
}

export interface HaciendaEnvioResult {
    id: string;
    clave: string;
    tipoDocumento: string;
    estado: HaciendaEstado;
    mensajeEstado?: string;
    httpStatus?: number;
    responseJson?: string;
}

export interface DocumentoJsonDto {
    id: string;
    clave: string;
    consecutivo: string;
    clienteNombre?: string;
    clienteCedula?: string;
    clienteCorreo?: string;
    fechaEmision: string;
    fechaAceptacion?: string;
    tipoDocumento: string;
    totalComprobante: number;
    facturaJson: string;
    respuestaHaciendaJson?: string;
    estadoHacienda?: string;
    mensajeEstado?: string;
    createdAt: string;
    updatedAt?: string;
}

export interface PagedResult<T> {
    page: number;
    pageSize: number;
    totalItems: number;
    totalPages: number;
    isFirstPage: boolean;
    isLastPage: boolean;
    hasNext: boolean;
    hasPrev: boolean;
    items: T[];
    licenseLimits: any[];
    [key: string]: any;
}

// --- CATALOGS ---

export interface ProvinciaResponse {
    code: string;
    name: string;
}

export interface CantonResponse {
    code: string;
    name: string;
    provinceCode: string;
}

export interface DistritoResponse {
    code: string;
    name: string;
    provinceCode: string;
    cantonCode: string;
}

export interface EconomicActivityResponse {
    code: string;
    name: string;
    tribuName: string;
    type: string;
}

export interface BranchResponse {
    id: string;
    name: string;
    code: string;
    address?: string;
    phone?: string;
    managerName?: string;
    isActive: boolean;
}

export interface TerminalResponse {
    id: string;
    branchId: string;
    name: string;
    code: string;
    isActive: boolean;
    licenseKey?: string;
    branchName?: string;
    branchCode?: string;
}

export interface DocumentSequenceResponse {
    documentType: string;
    currentValue: number;
    description: string;
}

export interface NextConsecutiveResponse {
    consecutivo: string;
    currentValue: number;
}

// --- TENANTS ---

export interface TenantResponse {
    id: string;
    name: string;
    slug: string;
    taxId: string;
    tier: string;
    status: string; // TenantStatus enum ideally
    ownerEmail?: string;
    apiKey?: string;
    isActive: boolean;
    createdAt: string;
    identificationType: number;
    commercialName?: string;
    provinceCode?: string;
    cantonCode?: string;
    districtCode?: string;
    addressDetails?: string;
    economicActivityCode?: string;
}

// --- HACIENDA CONFIG ---

export interface HaciendaConfigResponse {
    isConfigured: boolean;
    isActive?: boolean;
    enabled?: boolean;
    idCia?: string;
    environment: string;
    baseUrl?: string;
    tokenEndpoint?: string;
    clientId?: string;
    scope?: string;
    audience?: string;
    idpUsername?: string;
    hasCertificate?: boolean;
    certFileName?: string;
    // Ubicación
    provinceCode?: string;
    cantonCode?: string;
    districtCode?: string;
    addressDetails?: string;
    locationNamesJson?: any;
    defaultCurrency?: string;
    defaultCurrencyRate?: number;
    economicActivityCode?: string;
    updatedAt?: string;
}

export interface EnvironmentInfoResponse {
    environment: string;
    environmentName: string;
    isConfigured: boolean;
    isActive: boolean;
    baseUrl: string;
}

// --- LICENSES ---

export interface LicenseResponse {
    id: string;
    tenantId: string;
    licenseKey: string;
    machineId?: string;
    type: LicenseType;
    platform: LicensePlatform;
    status: LicenseStatus;
    expirationDate?: string;
    lastCheckIn?: string;
    appVersion?: string;
    lastIpAddress?: string;
    deviceName?: string;
    appIdentifier?: string;
    appName?: string;
    offlineToken?: string;
    modules?: string[];
    allowedModules?: string[];
    packageName?: string;
}

export interface LicensePackageResponse {
    id: string;
    name: string;
    description: string;
    type: string;
    allowedPlatforms: string[];
    quantity: number;
    monthlyPrice: number;
    defaultModules?: string;
    iconUrl?: string;
    isPlatformPackage: boolean;
    tenantId?: string;
    tenantName?: string;
}

export interface LicenseAddonResponse {
    id: string;
    packageId?: string;
    type: string;
    allowedPlatforms: string[];
    quantity: number;
    monthlyPrice: number;
    status: string;
    purchasedAt: string;
    nextBillingDate?: string;
    cancelledAt?: string;
    paymentReference?: string;
    paymentMethod?: string;
    amount?: number;
    tenantId: string;
    tenantName?: string;
    packageName?: string;
}

export interface DashboardStatsResponse {
    tenantName: string;
    tier: number;
    tierName: string;
    monthlyPrice: number;
    status: number;
    documentsUsedMonth: number;
    monthlyLimit: number;
    usagePercentage: number;
    subscriptionExpiresAt?: string;
    recentActivity: any[];
    [key: string]: any;
}

export interface DeveloperLicenseResponse {
    licenseId: string;
    packageName: string;
    quantity: number;
    monthlyPrice: number;
    nextBillingDate?: string;
    status: string;
}

export interface DeveloperClientResponse {
    tenantId: string;
    tenantName: string;
    tenantEmail: string;
    memberSince: string;
    activeLicenses: DeveloperLicenseResponse[];
}

export interface SubscriptionOrderDto {
    id: string;
    totalAmount: number;
    currency: string;
    status: number;
    paymentReference?: string;
    invoiceError?: string;
    paymentMethod?: number;
    createdAt: string;
    items: SubscriptionOrderItemDto[];
    tenantId?: string;
    tenantName?: string;
}

export interface SystemSettingResponse {
    key: string;
    value: string;
    description: string | null;
    group: string;
    isSecret: boolean;
    type?: string;
}

export interface UnifiedPaymentResponse {
    id: string;
    type: string;
    date: string;
    tenantId: string;
    tenantName: string;
    amount: number;
    currency: string;
    reference: string;
    status: string;
    plan: string;
    method: string;
}

export interface AdminAuthUserResponse {
    id: string;
    email: string;
    fullName: string;
    role: string;
    isActive: boolean;
    createdAt: string;
}

export interface GlobalStatsResponse {
    totalDocuments: number;
    documentsLast30Days: number;
    totalRevenue: number;
    revenueLast30Days: number;
    activeTenants: number;
    globalSuccessRate: number;
    averageLatencySeconds: number;
    documentVolumeSeries: Array<{ date: string; value: number }>;
    revenueSeries: Array<{ date: string; value: number }>;
    topTenants: Array<{
        tenantId: string;
        tenantName: string;
        plan: string;
        usage: number;
        limit: number;
        usagePercentage: number;
    }>;
}

export interface SubscriptionOrderItemDto {
    type: number;
    name: string;
    quantity: number;
    unitPrice: number;
    subtotal: number;
}

export interface RegisteredApplicationResponse {
    id: string;
    appId: string;
    appName: string;
    developerSlug: string;
    appSlug: string;
    description: string | null;
    websiteUrl: string | null;
    requiredType: string | number;
    requiredPlatform: string | number;
    isActive: boolean;
    createdAt: string;
}

export interface SaeIdentifierResponse {
    appId: string;
    platform: string;
    type: string;
}

export interface BillingSummaryResponse {
    tenantId: string;
    totalMonthlyAddons: number;
    currency: string;
    nextBillingDate: string;
}

export interface TenantLimitDto {
    tenantId: string;
    licenseType: LicenseType;
    maxAllowed: number;
    currentUsage: number;
}

export interface LicenseAuditLogDto {
    id: string;
    licenseId: string;
    action: string; // LicenseAction
    machineId?: string;
    details?: string;
    isSuccess: boolean;
    timestamp: string;
}

export interface CommissionSettlementDto {
    id: string;
    sellerId: string;
    sellerName: string;
    netAmount: number;
    taxAmount: number;
    totalAmount: number;
    status: number; // Enum: Draft=0, Requested=1, Invoiced=2, Paid=3, Rejected=4
    generatedInvoiceId?: string;
    invoiceKey?: string;
    invoiceStatusHacienda?: string;
    paymentReference?: string;
    requestedAt?: string;
    paidAt?: string;
    createdAt: string;
    commissionCount: number;
}

export interface SalesCommissionDto {
    id: string;
    sellerId: string;
    invoiceId: string;
    baseAmount: number;
    rate: number;
    commissionAmount: number;
    type: number;
    status: number;
    approvedAt?: string;
    paidAt?: string;
    createdAt: string;
}

// --- DASHBOARD & REPORTS ---

export interface DailyStat {
    date: string;
    amount: number;
    count: number;
}

export interface DocumentTipoStats {
    tipo: string;
    cantidad: number;
    total: number;
}

export interface DocumentResumen {
    totalComprobante: number;
    totalVenta: number;
    totalDescuento: number;
    totalImpuesto: number;
    totalDocumentos: number;
    defaultCurrency: string;
    documentosPorTipo: DocumentTipoStats[];
    serieDiaria: DailyStat[];
}

export interface ReporteIvaDto {
    periodoMes: number;
    periodoAnio: number;
    bienesGravado13: number;
    bienesGravado8: number;
    bienesGravado4: number;
    bienesGravado2: number;
    bienesGravado1: number;
    bienesGravado05: number;
    bienesExento: number;
    bienesExonerado: number;
    serviciosGravado13: number;
    serviciosGravado8: number;
    serviciosGravado4: number;
    serviciosGravado2: number;
    serviciosGravado1: number;
    serviciosGravado05: number;
    serviciosExento: number;
    serviciosExonerado: number;
    totalGravado13: number;
    totalGravado8: number;
    totalGravado4: number;
    totalGravado2: number;
    totalGravado1: number;
    totalGravado05: number;
    totalExento: number;
    totalExonerado: number;
    totalNoSujeto: number;
    totalImpuesto: number;
    totalVentas: number;
    cantidadFacturas: number;
    cantidadNotasCredito: number;
    cantidadNotasDebito: number;
    montoRestadoNotasCredito: number;
    montoSumadoNotasDebito: number;
}

export interface LibroVentasRowDto {
    fechaEmision: string;
    tipoDocumento: string;
    consecutivo: string;
    receptorIdentificacion: string;
    receptorNombre: string;
    condicionVenta: string;
    totalGravado: number;
    totalExento: number;
    totalImpuesto: number;
    totalComprobante: number;
    estadoHacienda: string;
}

export interface ReporteComprasRowDto {
    fechaEmision: string;
    clave: string;
    proveedorIdentificacion: string;
    proveedorNombre: string;
    baseImponible: number;
    ivaAcreditable: number;
    totalExento: number;
    totalComprobante: number;
}

export interface ReporteCabysRowDto {
    cabys: string;
    descripcion: string;
    cantidadVendida: number;
    montoTotal: number;
    impuestoTotal: number;
}

export interface AsientoContableDto {
    periodoMes: number;
    periodoAnio: number;
    cuentasPorCobrar: number;
    efectivoYBancos: number;
    ingresosVentasGravadas: number;
    ingresosVentasExentas: number;
    ivaPorPagar: number;
}

export interface ReporteExtemporaneidadDto {
    fechaEmision: string;
    fechaEnvio?: string;
    clave: string;
    tipoDocumento: string;
    diasRetraso: number;
    estado: string;
}

// --- PARTNERS ---

export interface PartnerProfile {
    id: string;
    code: string;
    type: string;
    level: string;
    status: string;
    balance: number;
    phoneNumber?: string;
    website?: string;
    bankName?: string;
    bankAccount?: string;
    taxId?: string;
}

export interface PartnerClient {
    id: string;
    name: string;
    status: string;
    joinedAt: string;
    currentPlan: string;
}

export interface PublicPlan {
    id: string;
    name: string;
    description: string;
    category: string;
    tier: string;
    monthlyPrice: number;
    actualPrice: number;
    discountPercent: number;
    billingPeriod: string;
    features: string[];
    isPopular: boolean;
}

export interface PendingApprovalItemDto {
    name: string;
    quantity: number;
    unitPrice: number;
    subtotal: number;
}

export interface PendingApprovalDto {
    id: string;
    type: number;
    createdAt: string;
    tenantId: string;
    tenantName: string;
    itemName: string;
    amount: number;
    currency: string;
    referenceNumber: string;
    paymentMethod: string;
    notes: string;
    items: PendingApprovalItemDto[];
}

// --- ROLES & PERMISSIONS ---

export interface PermissionDto {
    id: string;
    name: string;
    description?: string;
    category?: string;
}

export interface TenantRoleDto {
    id: string;
    name: string;
    description?: string;
    isSystemRole: boolean;
    permissions: string[];
}

// --- LICENSES (EXTENDED) ---

export interface LicenseModuleInfo {
    moduleId: string;
    code: string;
    name: string;
    description?: string;
    category?: string;
    isActive: boolean;
    grantedAt: string;
    expiresAt?: string;
    maxUsageCount?: number;
    currentUsageCount: number;
    isExpired: boolean;
    isUsageLimitReached: boolean;
    requiresModules?: string[];
    metadata?: Record<string, any>;
}

export interface TenantLicense {
    status: string;
    licenseId?: string;
    type?: string;
    expirationDate?: string;
    offlineToken?: string;
    features?: string[];
    modules?: LicenseModuleInfo[];
    message?: string;
}
export interface AdminTenantResponse {
    id: string;
    name: string;
    taxId: string;
    email: string;
    status: string;
    plan: string;
    documents: number;
    limit: number;
    created: string;
    expires?: string | null;
    locationNamesJson?: string | null;
}

export interface PlatformModuleResponse {
    id: string;
    code: string;
    name: string;
    description: string;
    type: number;
    icon?: string;
    isActive: boolean;
    appIdentifier?: string;
    createdAt: string;
    updatedAt?: string;
}

export interface AdminConfigResponse {
    environment: string;
    haciendaUrl?: string;
    haciendaEnabled: boolean;
    frontendUrl?: string;
    smtpHost?: string;
    databaseProvider?: string;
    azureServiceBusConfigured: boolean;
    azureRedisConfigured: boolean;
    azureBlobConfigured: boolean;
    azureKeyVaultUrl?: string;
    appInsightsEnabled: boolean;
    jwtExpiration?: string;
    serverTime: string;
    dotNetVersion: string;
    os: string;
    processorCount: number;
}
