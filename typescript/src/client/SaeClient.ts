import {
    GenerarDocumentoRequest,
    HaciendaConfigRequest,
    UpdateTenantRequest,
    MensajeEndosoRequest,
    CreateSettlementRequest,
    RegisterSettlementPaymentRequest,
    PurchaseAddonRequest,
    ReportAddonPurchaseRequest,
    ApproveAddonRequest,
    RejectAddonRequest,
    EnviarDocumentoSimplificadoRequest,
    RegisterRequest,
    ResetPasswordRequest,
    PartnerRegisterRequest,
    IssueLicenseRequest,
    LeadRegisterRequest,
    TenantRoleRequest,
    CreateOrderRequest,
    ReportOrderPaymentRequest,
    InviteUserRequest,
    UpdateUserRoleRequest,
    AcceptInviteRequest,
    ConfirmEmailRequest,
    SwitchEnvironmentRequest,
    CreateBranchRequest,
    UpdateBranchRequest,
    CreateTerminalRequest,
    UpdateTerminalRequest,
    UpdateSequenceRequest,
    CreateLicensePackageRequest,
    UpdateLicensePackageRequest,
    RegisterAppRequest,
    AdminRegisterRequest,
    AdminUpdateRequest,
    ApprovalRejectRequest,
    SettlementPayRequest,
    ValidateLicenseRequest,
    ActivateLicenseRequest,
    ApproveRequest,
    CreatePlatformModuleRequest,
    UpdatePlatformModuleRequest,
    CreateDeveloperModuleRequest,
    UpdateDeveloperModuleRequest,
    LoginInitRequest,
    MfaVerifyRequest,
    LoginContext
} from '../models/requests.js';
import {
    LoginResult,
    PasswordResetResponse,
    DocumentoResult,
    HaciendaEnvioResult,
    HaciendaConfigResponse,
    EnvironmentInfoResponse,
    TenantResponse,
    ProvinciaResponse,
    CantonResponse,
    DistritoResponse,
    EconomicActivityResponse,
    PagedResult,
    DocumentoJsonDto,
    CommissionSettlementDto,
    AdminConfigResponse,
    SalesCommissionDto,
    LicensePackageResponse,
    LicenseAddonResponse,
    BillingSummaryResponse,
    DocumentResumen,
    ReporteIvaDto,
    LibroVentasRowDto,
    ReporteComprasRowDto,
    ReporteCabysRowDto,
    AsientoContableDto,
    ReporteExtemporaneidadDto,
    PartnerProfile,
    PartnerClient,
    PublicPlan,
    PermissionDto,
    TenantRoleDto,
    LicenseResponse,
    TenantLicense,
    AdminMeResponse,
    ImpersonateResponse,
    PendingApprovalDto,
    DashboardStatsResponse,
    SubscriptionOrderDto,
    BranchResponse,
    TerminalResponse,
    DocumentSequenceResponse,
    NextConsecutiveResponse,
    DeveloperClientResponse,
    RegisteredApplicationResponse,
    SystemSettingResponse,
    UnifiedPaymentResponse,
    AdminAuthUserResponse,
    GlobalStatsResponse,
    SaeIdentifierResponse,
    PlatformModuleResponse
} from '../models/responses.js';
import { SystemLogService } from '../services/systemLogService.js';
import { 
    SaeClientOptions, 
    ProductionDefaults, 
    DevelopmentDefaults, 
    SaeClientError, 
    SaeLogLevel, 
    SaeClientLogEvent,
    isRetryableError,
    calculateRetryDelay
} from './SaeClientOptions.js';

export interface ApiClient {
    get<T>(url: string, config?: any): Promise<T>;
    post<T>(url: string, data?: any, config?: any): Promise<T>;
    put<T>(url: string, data?: any, config?: any): Promise<T>;
    delete<T>(url: string, config?: any): Promise<T>;
}

export class SaeClient implements ApiClient {
    private baseUrl: string;
    private token?: string;
    private tenantId?: string;
    private terminalKey?: string;
    private securityContext?: LoginContext;
    private options: Required<Pick<SaeClientOptions, 'timeout' | 'maxRetries' | 'retryDelay' | 'backoffMultiplier' | 'skipCertificateValidation' | 'maxRedirects' | 'followRedirects'>> & SaeClientOptions;
    
    private getHeaders: (method: string, url: string) => Promise<Record<string, string>>;

    public logs: SystemLogService;

    constructor(baseUrl: string, getHeaders: (method: string, url: string) => Promise<Record<string, string>>, options?: SaeClientOptions) {
        this.getHeaders = getHeaders;
        const resolvedBaseUrl = baseUrl || "https://localhost:5000";
        this.options = {
            ...ProductionDefaults,
            ...options
        };

        this.baseUrl = resolvedBaseUrl.replace(/\/$/, ""); 
        
        let apiBaseUrl = this.baseUrl.endsWith('/api') || this.baseUrl.endsWith('/api/') 
            ? this.baseUrl 
            : `${this.baseUrl}/api`;
        
        if (!apiBaseUrl.endsWith('/')) {
            apiBaseUrl += '/';
        }
        
        this.baseUrl = apiBaseUrl;
        this.logs = new SystemLogService(this as any);
    }

    public setToken(token: string) {
        this.token = token;
    }

    public setTenantId(tenantId: string) {
        this.tenantId = tenantId;
    }

    public setTerminalKey(key: string) {
        this.terminalKey = key;
    }

    public setSecurityContext(context: LoginContext) {
        this.securityContext = { ...this.securityContext, ...context };
    }

    private async request<T>(method: string, url: string, data?: any, options?: any): Promise<T> {
        const fullUrl = `${this.baseUrl}${url}`.replace(/([^:]\/)\/+/g, "$1");
        const authHeaders = await this.getHeaders(method, fullUrl);
        
        const headers: Record<string, string> = {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            ...authHeaders,
            ...options?.headers
        };

        if (this.token) headers['Authorization'] = `Bearer ${this.token}`;
        if (this.tenantId) headers['X-Tenant-Id'] = this.tenantId;
        if (this.terminalKey) headers['X-Terminal-Key'] = this.terminalKey;

        // 🛡️ SAE v6 Pro Security Context Propagation
        if (this.securityContext) {
            if (this.securityContext.deviceId) headers['X-Device-Id'] = this.securityContext.deviceId;
            if (this.securityContext.fingerprint) headers['X-Device-Fingerprint'] = this.securityContext.fingerprint;
            if (this.securityContext.country) headers['X-Country'] = this.securityContext.country;
            if (this.securityContext.latitude) headers['X-Latitude'] = this.securityContext.latitude.toString();
            if (this.securityContext.longitude) headers['X-Longitude'] = this.securityContext.longitude.toString();
        }

        const config: RequestInit = {
            method,
            headers,
            body: data ? JSON.stringify(data) : undefined,
            signal: options?.signal || (this.options.timeout ? AbortSignal.timeout(this.options.timeout) : undefined),
            ...options
        };

        try {
            const response = await fetch(fullUrl, config);
            
            if (!response.ok) {
                const errorData = await response.json().catch(() => ({}));
                const message = `Request failed with status ${response.status}`;
                throw new SaeClientError(message, response.status, errorData);
            }

            if (response.status === 204) return {} as T;
            return await response.json();
        } catch (error: any) {
            this.log(SaeLogLevel.Error, `API Error: ${error.message}`, `${method} ${url}`, undefined, error);
            throw error;
        }
    }

    public async get<T>(url: string, config?: any): Promise<T> {
        let fullUrl = url;
        if (config?.params) {
            const params = new URLSearchParams(config.params).toString();
            fullUrl += (fullUrl.includes('?') ? '&' : '?') + params;
        }
        return this.request<T>('GET', fullUrl, undefined, config);
    }

    public async post<T>(url: string, data?: any, config?: any): Promise<T> {
        return this.request<T>('POST', url, data, config);
    }

    public async put<T>(url: string, data?: any, config?: any): Promise<T> {
        return this.request<T>('PUT', url, data, config);
    }

    public async delete<T>(url: string, config?: any): Promise<T> {
        return this.request<T>('DELETE', url, undefined, config);
    }

    /**
     * Crea un agente HTTPS para Node.js con las opciones de SSL configuradas.
     * Este método usa require() de forma dinámica para evitar problemas en navegador.
     */
    private createHttpsAgent(): any {
        try {
            // Dynamic require para Node.js
            const https = require('https');
            
            const agentOptions: any = {};
            
            // Certificados autofirmados (solo desarrollo)
            if (this.options.skipCertificateValidation) {
                agentOptions.rejectUnauthorized = false;
            }
            
            // mTLS - certificado de cliente
            if (this.options.clientCertificate) {
                agentOptions.cert = this.options.clientCertificate.cert;
                agentOptions.key = this.options.clientCertificate.key;
                if (this.options.clientCertificate.ca) {
                    agentOptions.ca = this.options.clientCertificate.ca;
                }
            }
            
            // CAs personalizadas
            if (this.options.customRootCertificates) {
                agentOptions.ca = this.options.customRootCertificates;
            }
            
            return new https.Agent(agentOptions);
        } catch {
            // Si falla (navegador o edge runtime), retornamos undefined
            return undefined;
        }
    }

    /**
     * El motor de reintentos ahora está integrado en el método request() nativo.
     */

    private isRetryableError(error: any): boolean {
        return isRetryableError(error);
    }

    private calculateRetryDelay(attempt: number): number {
        return calculateRetryDelay(attempt, this.options.retryDelay || 1000, this.options.backoffMultiplier || 2);
    }

    /**
     * Normaliza las llaves de un objeto de respuesta a camelCase.
     * Útil para asegurar compatibilidad cuando el backend puede devolver PascalCase.
     */
    private normalizeResponse<T>(data: any): T {
        if (!data || typeof data !== 'object') return data as T;
        
        const normalized: any = {};
        for (const key in data) {
            const camelKey = key.charAt(0).toLowerCase() + key.slice(1);
            normalized[camelKey] = data[key];
        }

        // 🧠 Normalización de mensajes (Estandarización SAE V5)
        if (!normalized.message) {
            normalized.message = data.errorMessage || data.ErrorMessage || data.error || data.mensaje || data.Mensaje || data.message || data.Message;
        }

        // 🧠 Mapeo de estados legacy (Compatibilidad V4 -> V5)
        // Si el backend devuelve 200 OK con flags pero sin status formal
        if (!normalized.status) {
            if (data.requiresMfa || data.RequiresMfa) {
                normalized.status = "RequiresMfa";
            } else if (data.requiresSetup || data.RequiresSetup) {
                normalized.status = "RequiresSetup";
            }
        }

        return normalized as T;
    }

    /**
     * Loguea un evento si el callback está configurado.
     */
    private log(level: SaeLogLevel, message: string, operation?: string, retryAttempt?: number, error?: Error): void {
        if (this.options.onLog) {
            this.options.onLog({
                timestamp: new Date(),
                level,
                message,
                operation,
                retryAttempt,
                error
            });
        }
    }

    /**
     * Crea opciones predeterminadas seguras para producción.
     */
    static productionDefaults(): SaeClientOptions {
        return { ...ProductionDefaults };
    }

    /** @deprecated Usar tokens de identidad V5 */
    static developmentDefaults(): SaeClientOptions {
        return { ...DevelopmentDefaults };
    }

    /** @deprecated API Key is deprecated. Use identity tokens. */
    public setApiKey(apiKey: string) {
        // En v4, el apiKey se maneja a través de getHeaders o tokens
    }

    /**
     * Verifica la salud de la plataforma.
     * @returns True si la plataforma está saludable, false en caso contrario.
     */
    public async checkHealth(): Promise<boolean> {
        const startTime = Date.now();
        try {
            await this.get('../health');
            const duration = Date.now() - startTime;
            this.log(SaeLogLevel.Debug, `Health check completed in ${duration}ms`, 'checkHealth');
            return true;
        } catch (error: any) {
            const duration = Date.now() - startTime;
            this.log(SaeLogLevel.Warning, `Health check failed after ${duration}ms: ${error.message}`, 'checkHealth', undefined, error);
            return false;
        }
    }

    public async login(email: string, password?: string, context?: LoginContext): Promise<LoginResult> {
        const data = await this.post<any>('v1/auth/login', { 
            Username: email, 
            Password: password,
            ...context
        });
        return this.normalizeResponse<LoginResult>(data);
    }

    /**
     * Inicia el flujo de login adaptativo (V5).
     */
    public async loginInit(email: string, context?: LoginContext): Promise<LoginResult> {
        const data = await this.post<any>('v1/auth/login/init', { 
            email,
            ...context
        });
        return this.normalizeResponse<LoginResult>(data);
    }

    /**
     * Eleva una sesión existente mediante verificación MFA (Step-Up).
     */
    public async completeAuth(sessionId: string, type: string, payload: Record<string, string>, context?: LoginContext): Promise<LoginResult> {
        const data = await this.post<any>('v1/auth/complete', { 
            sessionId, 
            type, 
            payload,
            ...context
        });
        const result = this.normalizeResponse<LoginResult>(data);
        if (result.token) this.setToken(result.token);
        return result;
    }

    /** @deprecated Use completeAuth */
    public async elevateSession(request: MfaVerifyRequest): Promise<LoginResult> {
        return this.completeAuth(request.sessionId, 'mfa', { code: request.mfaCode });
    }

    public async adminLogin(email: string, password?: string): Promise<LoginResult> {
        const data = await this.post<any>('v1/admin/auth/login', { Username: email, Password: password });
        const result = this.normalizeResponse<LoginResult>(data);
        
        if (result.token || result.accessToken) this.setToken((result.token || result.accessToken)!);
        return result;
    }

    public async forgotPassword(email: string): Promise<PasswordResetResponse> {
        return await this.post<PasswordResetResponse>('v1/auth/forgot-password', { email });
    }

    public async resetPassword(token: string, newPassword: string): Promise<PasswordResetResponse> {
        return await this.post<PasswordResetResponse>('v1/auth/reset-password', { token, newPassword });
    }

    public async register(request: RegisterRequest): Promise<void> {
        await this.post('v1/auth/register', request);
    }

    public async resendVerification(email: string): Promise<void> {
        await this.post('v1/auth/resend-verification', { email });
    }

    public async confirmEmail(request: ConfirmEmailRequest): Promise<void> {
        await this.post('v1/auth/confirm-email', request);
    }

    public async adminForgotPassword(email: string): Promise<void> {
        await this.post('v1/admin/auth/forgot-password', { email });
    }

    public async adminResetPassword(request: ResetPasswordRequest): Promise<void> {
        await this.post('v1/admin/auth/reset-password', request);
    }

    public async adminMe(): Promise<AdminMeResponse> {
        return await this.get<AdminMeResponse>('v1/admin/auth/me');
    }

    public async getAdminConfig(adminKey: string): Promise<AdminConfigResponse> {
        return await this.get<AdminConfigResponse>('v1/admin/config', {
            headers: { 'X-Admin-Key': adminKey }
        });
    }

    public async adminImpersonate(tenantId: string): Promise<ImpersonateResponse> {
        return await this.post<ImpersonateResponse>(`v1/admin/auth/impersonate/${tenantId}`);
    }

    public async adminGetPendingApprovals(): Promise<PendingApprovalDto[]> {
        return await this.getPendingApprovals();
    }

    public async adminApproveApproval(id: string, type: number, request: ApproveRequest): Promise<void> {
        return await this.approveApproval(id, type, request);
    }

    public async adminRejectApproval(id: string, type: number, request: ApprovalRejectRequest): Promise<void> {
        return await this.rejectApproval(id, type, request);
    }

    public async adminGetRedisQueues(): Promise<any> {
        return await this.get<any>('v1/admin/redis/queues');
    }

    public async adminGetBillingSettings(): Promise<any> {
        return await this.get<any>('v1/admin/settings/billing');
    }

    public async adminUpdateSettingsBatch(settings: Record<string, string>): Promise<void> {
        await this.updateSystemSettingsBatch(settings);
    }

    public async adminGetPlans(): Promise<any[]> {
        return await this.get<any[]>('v1/subscription-plans/admin/list');
    }

    public async adminGetPlan(id: string): Promise<any> {
        return await this.get<any>(`v1/subscription-plans/${id}`);
    }

    public async adminCreatePlan(data: any): Promise<void> {
        await this.post('v1/subscription-plans', data);
    }

    public async adminUpdatePlan(id: string, data: any): Promise<void> {
        await this.put(`v1/subscription-plans/${id}`, data);
    }

    public async adminDeletePlan(id: string): Promise<void> {
        await this.delete(`v1/subscription-plans/${id}`);
    }

    // --- MANAGEMENT ---


    public async getCurrentTenant(): Promise<TenantResponse> {
        return await this.get<TenantResponse>('v1/tenants/current');
    }

    public async updateCurrentTenant(request: UpdateTenantRequest): Promise<TenantResponse> {
        return await this.put<TenantResponse>('v1/tenants/current', request);
    }

    public async regenerateApiKey(): Promise<{ apiKey: string }> {
        return await this.post<{ apiKey: string }>('v1/tenants/api-key/regenerate');
    }

    public async getMyTenants(): Promise<TenantResponse[]> {
        return await this.get<TenantResponse[]>('v1/tenants/my-tenants');
    }

    public async registerTenant(name: string, taxId: string, ownerEmail?: string): Promise<TenantResponse> {
        return await this.post<TenantResponse>('v1/tenants/register', { name, taxId, ownerEmail });
    }

    public async getTenantBillingSettings(tenantId: string): Promise<Record<string, string>> {
        return await this.get<Record<string, string>>(`v1/tenants/${tenantId}/billing-settings`);
    }

    public async uploadTenantLogo(formData: FormData): Promise<{ logoUrl: string }> {
        return await this.post<{ logoUrl: string }>('v1/tenants/current/logo', formData);
    }

    public async deleteTenantLogo(): Promise<{ message: string }> {
        return await this.delete<{ message: string }>('v1/tenants/current/logo');
    }

    // --- TENANT USERS ---

    public async getTenantUsers(): Promise<any[]> {
        return await this.get<any[]>('v1/tenant-users');
    }

    public async inviteTenantUser(request: InviteUserRequest): Promise<any> {
        return await this.post<any>('v1/tenant-users/invite', request);
    }

    public async acceptInvitation(request: AcceptInviteRequest): Promise<any> {
        return await this.post<any>('v1/tenant-users/accept-invite', request);
    }

    public async removeTenantUser(id: string): Promise<void> {
        await this.delete(`v1/tenant-users/${id}`);
    }

    public async updateTenantUserRole(id: string, request: UpdateUserRoleRequest): Promise<void> {
        await this.put(`v1/tenant-users/${id}/role`, request);
    }

    // --- ROLES & PERMISSIONS ---

    public async getRoles(): Promise<TenantRoleDto[]> {
        return await this.get<TenantRoleDto[]>('v1/tenant-roles');
    }

    public async getPermissions(): Promise<PermissionDto[]> {
        return await this.get<PermissionDto[]>('v1/tenant-roles/permissions');
    }

    public async createRole(request: TenantRoleRequest): Promise<TenantRoleDto> {
        return await this.post<TenantRoleDto>('v1/tenant-roles', request);
    }

    public async updateRole(id: string, request: TenantRoleRequest): Promise<TenantRoleDto> {
        return await this.put<TenantRoleDto>(`v1/tenant-roles/${id}`, request);
    }

    public async deleteRole(id: string): Promise<void> {
        await this.delete(`v1/tenant-roles/${id}`);
    }

    // --- CATALOGS ---

    public async getProvincias(): Promise<ProvinciaResponse[]> {
        return await this.get<ProvinciaResponse[]>('v1/catalogs/provincias');
    }

    public async getCantones(provinceCode: string): Promise<CantonResponse[]> {
        return await this.get<CantonResponse[]>(`v1/catalogs/cantones/${provinceCode}`);
    }

    public async getDistritos(provinceCode: string, cantonCode: string): Promise<DistritoResponse[]> {
        return await this.get<DistritoResponse[]>(`v1/catalogs/distritos/${provinceCode}/${cantonCode}`);
    }

    public async getCurrencies(search?: string): Promise<any[]> {
        let url = 'v1/catalogs/monedas';
        if (search) {
            url += `?search=${encodeURIComponent(search)}`;
        }
        return await this.get<any[]>(url);
    }

    public async getEconomicActivities(search?: string): Promise<EconomicActivityResponse[]> {
        let url = 'v1/catalogs/economic-activities';
        if (search) {
            url += `?search=${encodeURIComponent(search)}`;
        }
        return await this.get<EconomicActivityResponse[]>(url);
    }

    // --- DOCUMENTS ---

    public async generarDocumento(documento: GenerarDocumentoRequest): Promise<DocumentoResult> {
        return await this.post<DocumentoResult>('v1/documentos/generar', documento);
    }

    public async enviarDocumento(documento: GenerarDocumentoRequest): Promise<HaciendaEnvioResult> {
        return await this.post<HaciendaEnvioResult>('v1/hacienda/enviar', documento);
    }

    public async emitir(request: EnviarDocumentoSimplificadoRequest): Promise<HaciendaEnvioResult> {
        return await this.post<HaciendaEnvioResult>('v1/hacienda/emitir', request);
    }

    public async consultarEstado(clave: string): Promise<HaciendaEnvioResult> {
        return await this.get<HaciendaEnvioResult>(`v1/hacienda/estado/${clave}`);
    }

    public async endosarDocumento(request: MensajeEndosoRequest): Promise<any> {
        return await this.post<any>('v1/hacienda/endosar', request);
    }

    public async aceptarDocumento(request: any): Promise<any> {
        return await this.post<any>('v1/hacienda/aceptar', request);
    }

    public async getUsedCurrencies(): Promise<any[]> {
        return await this.get<any[]>('v1/documentos-json/monedas');
    }

    public async getDocumentById(id: string): Promise<DocumentoJsonDto> {
        return await this.get<DocumentoJsonDto>(`v1/documentos-json/${id}`);
    }

    public async getDocumentXml(id: string): Promise<ArrayBuffer> {
        return await this.get<ArrayBuffer>(`v1/documentos-json/${id}/xml`, { responseType: 'arraybuffer' });
    }

    public async getDocumentResponseXml(id: string): Promise<ArrayBuffer> {
        return await this.get<ArrayBuffer>(`v1/documentos-json/${id}/respuesta`, { responseType: 'arraybuffer' });
    }

    public async getDocumentPdf(id: string): Promise<ArrayBuffer> {
        return await this.get<ArrayBuffer>(`v1/documentos-json/${id}/pdf`, { responseType: 'arraybuffer' });
    }

    public async getDocumentPdfByClave(clave: string): Promise<ArrayBuffer> {
        return await this.get<ArrayBuffer>(`v1/documents/print/${clave}`, { responseType: 'arraybuffer' });
    }

    public async buscarDocumentos(params: any): Promise<PagedResult<DocumentoJsonDto>> {
        return await this.get<PagedResult<DocumentoJsonDto>>('v1/documentos-json', { params });
    }

    public async exportDocuments(format: 'csv' | 'excel' | 'pdf', filters: any): Promise<ArrayBuffer> {
        return await this.get<ArrayBuffer>(`v1/documentos-json/export/${format}`, { params: filters, responseType: 'arraybuffer' });
    }

    public async getDocumentSummary(fechaDesde?: string, fechaHasta?: string): Promise<DocumentResumen> {
        return await this.get<DocumentResumen>('v1/documentos-json/resumen', { params: { fechaDesde, fechaHasta } });
    }

    public async retryDocument(clave: string): Promise<void> {
        await this.post(`v1/documentos-json/${clave}/reprocess-rejection`);
    }

    public async regenerateDocument(clave: string, newConsecutive?: number): Promise<any> {
        return await this.post<any>(`v1/documentos/regenerate/${clave}`, null, { params: { newConsecutive } });
    }

    public async getNextConsecutive(sucursal: number, terminal: number, tipoDocumento: string): Promise<number> {
        return await this.get<number>('v1/documentos/next-consecutive', { params: { sucursal, terminal, tipo: tipoDocumento } });
    }

    public async resendDocumentEmail(id: string): Promise<void> {
        await this.post(`v1/documentos-json/${id}/resend-email`);
    }

    public async forceDocumentRejection(id: string): Promise<void> {
        await this.post(`v1/documentos-json/${id}/force-rejection`);
    }

    public async getDocumentSafetyScore(clave: string): Promise<any> {
        return await this.get<any>(`v1/documents/safety-score/${clave}`);
    }


    // --- REPORTS ---

    public async getReport<T>(type: string, mes: number, anio: number): Promise<T> {
        return await this.get<T>(`v1/reports/${type}`, { params: { mes, anio } });
    }

    public async downloadReport(type: string, format: string, mes: number, anio: number): Promise<ArrayBuffer> {
        return await this.get<ArrayBuffer>(`v1/reports/${type}/${format}`, { params: { mes, anio }, responseType: 'arraybuffer' });
    }

    // --- HACIENDA CONFIG ---

    public async getHaciendaConfig(): Promise<HaciendaConfigResponse> {
        return await this.get<HaciendaConfigResponse>('v1/hacienda/config');
    }

    public async saveHaciendaConfig(config: HaciendaConfigRequest): Promise<HaciendaConfigResponse> {
        return await this.put<HaciendaConfigResponse>('v1/hacienda/config', config);
    }

    public async getHaciendaEnvironments(): Promise<EnvironmentInfoResponse[]> {
        return await this.get<EnvironmentInfoResponse[]>('v1/hacienda/config/environments');
    }

    public async getHaciendaConfigByEnvironment(environment: number | string): Promise<HaciendaConfigResponse> {
        return await this.get<HaciendaConfigResponse>(`v1/hacienda/config/environment/${environment}`);
    }

    public async switchHaciendaEnvironment(environment: number | string): Promise<EnvironmentInfoResponse> {
        // Enforce numeric for backend if it's a number-like string, otherwise send as is
        const envVal = typeof environment === 'string' && !isNaN(Number(environment))
            ? Number(environment)
            : environment;

        return await this.post<EnvironmentInfoResponse>('v1/hacienda/config/environment/switch', { environment: envVal });
    }

    // --- BRANCHES & TERMINALS ---

    public async getBranches(): Promise<BranchResponse[]> {
        return await this.get<BranchResponse[]>('v1/Branches');
    }

    public async getBranch(id: string): Promise<BranchResponse> {
        return await this.get<BranchResponse>(`v1/Branches/${id}`);
    }

    public async createBranch(request: CreateBranchRequest): Promise<BranchResponse> {
        return await this.post<BranchResponse>('v1/Branches', request);
    }

    public async updateBranch(id: string, request: UpdateBranchRequest): Promise<void> {
        await this.put(`v1/Branches/${id}`, request);
    }

    public async deleteBranch(id: string): Promise<void> {
        await this.delete(`v1/Branches/${id}`);
    }

    public async getTerminals(branchId: string): Promise<TerminalResponse[]> {
        return await this.get<TerminalResponse[]>(`v1/terminals?branchId=${branchId}`);
    }

    public async getTerminal(id: string): Promise<TerminalResponse> {
        return await this.get<TerminalResponse>(`v1/terminals/${id}`);
    }

    public async createTerminal(branchId: string, request: CreateTerminalRequest): Promise<TerminalResponse> {
        return await this.post<TerminalResponse>(`v1/terminals?branchId=${branchId}`, request);
    }

    public async updateTerminal(id: string, request: UpdateTerminalRequest): Promise<void> {
        await this.put(`v1/terminals/${id}`, request);
    }

    public async deleteTerminal(id: string): Promise<void> {
        await this.delete(`v1/terminals/${id}`);
    }

    public async getTerminalSequences(terminalId: string): Promise<DocumentSequenceResponse[]> {
        return await this.get<DocumentSequenceResponse[]>(`v1/terminals/${terminalId}/sequences`);
    }

    public async updateTerminalSequence(terminalId: string, docType: string, request: UpdateSequenceRequest): Promise<void> {
        await this.put(`v1/terminals/${terminalId}/sequences/${docType}`, request);
    }

    public async getNextConsecutiveManual(sucursal: number, terminal: number, tipoDocumento: string): Promise<number> {
        return await this.get<number>('v1/documentos/next-consecutive', { params: { sucursal, terminal, tipo: tipoDocumento } });
    }

    public async getNextConsecutiveByTerminal(terminalId: string, docType: string): Promise<NextConsecutiveResponse> {
        return await this.get<NextConsecutiveResponse>(`v1/terminals/${terminalId}/sequences/${docType}/next`);
    }

    public async generateTerminalLicense(terminalId: string): Promise<TerminalResponse> {
        return await this.post<TerminalResponse>(`v1/terminals/${terminalId}/generate-license`);
    }

    // --- LICENSE PACKAGES ---

    public async getLicensePackages(systemOnly: boolean = false, tenantId?: string): Promise<LicensePackageResponse[]> {
        let url = `v1/license-packages?systemOnly=${systemOnly}`;
        if (tenantId) url += `&tenantId=${tenantId}`;
        return await this.get<LicensePackageResponse[]>(url);
    }

    public async getLicensePackage(id: string): Promise<LicensePackageResponse> {
        return await this.get<LicensePackageResponse>(`v1/license-packages/${id}`);
    }

    public async createLicensePackage(request: CreateLicensePackageRequest): Promise<LicensePackageResponse> {
        return await this.post<LicensePackageResponse>('v1/license-packages', request);
    }

    public async updateLicensePackage(id: string, request: UpdateLicensePackageRequest): Promise<void> {
        await this.put(`v1/license-packages/${id}`, request);
    }

    public async deleteLicensePackage(id: string): Promise<void> {
        await this.delete(`v1/license-packages/${id}`);
    }

    /** Updates the active modules for a specific license. Returns { activeModules, allowedModules }. */
    public async updateLicenseModules(id: string, moduleCodes: string[]): Promise<{ activeModules: string[]; allowedModules: string[] }> {
        return await this.put<{ activeModules: string[]; allowedModules: string[] }>(`v1/licenses/${id}/modules`, moduleCodes);
    }

    /** Gets the allowed and active modules for a specific license without modifying anything. */
    public async getLicenseModules(id: string): Promise<{ activeModules: string[]; allowedModules: string[] }> {
        return await this.get<{ activeModules: string[]; allowedModules: string[] }>(`v1/licenses/${id}/modules`);
    }


    // --- DEVELOPER API ---

    public async getDeveloperClients(): Promise<DeveloperClientResponse[]> {
        return await this.get<DeveloperClientResponse[]>('v1/developer/clients');
    }

    public async getDeveloperApplications(): Promise<RegisteredApplicationResponse[]> {
        return await this.get<RegisteredApplicationResponse[]>('v1/developer/applications');
    }

    public async registerDeveloperApplication(request: RegisterAppRequest): Promise<RegisteredApplicationResponse> {
        return await this.post<RegisteredApplicationResponse>('v1/developer/applications', request);
    }

    public async deactivateDeveloperApplication(id: string): Promise<void> {
        await this.delete(`v1/developer/applications/${id}`);
    }

    public async getSaeIdentifiers(): Promise<SaeIdentifierResponse[]> {
        return await this.get<SaeIdentifierResponse[]>('v1/developer/applications/sae-identifiers');
    }

    public async getDeveloperModules(): Promise<PlatformModuleResponse[]> {
        return await this.get<PlatformModuleResponse[]>('v1/developer/modules');
    }

    public async createDeveloperModule(request: CreateDeveloperModuleRequest): Promise<PlatformModuleResponse> {
        return await this.post<PlatformModuleResponse>('v1/developer/modules', request);
    }

    public async validateModuleCode(code: string): Promise<boolean> {
        return await this.get<boolean>(`v1/developer/modules/validate-code/${code}`);
    }

    public async updateDeveloperModule(id: string, request: UpdateDeveloperModuleRequest): Promise<PlatformModuleResponse> {
        return await this.put<PlatformModuleResponse>(`v1/developer/modules/${id}`, request);
    }

    public async deleteDeveloperModule(id: string): Promise<void> {
        await this.delete(`v1/developer/modules/${id}`);
    }

    // --- LICENSE API ---

    public async validateLicense(request: ValidateLicenseRequest): Promise<any> {
        return await this.post<any>('v1/licenses/validate', request);
    }

    public async activateLicense(request: ActivateLicenseRequest): Promise<any> {
        return await this.post<any>('v1/licenses/activate', request);
    }

    public async adminUpdateLicenseModules(licenseId: string, modules: string[]): Promise<void> {
        await this.put(`v1/admin/licenses/${licenseId}/modules`, { modules });
    }

    // --- ADMIN API ---

    public async adminGetUsers(): Promise<any[]> {
        return await this.get<any[]>('v1/admin/users');
    }

    public async adminUpgradeSubscription(tenantId: string, tier: number): Promise<void> {
        await this.post(`v1/admin/upgrade-subscription`, { tenantId, tier });
    }

    public async adminUpdateTenant(id: string, request: any): Promise<void> {
        await this.put(`v1/admin/tenants/${id}`, request);
    }

    public async adminGetTenantConfig(id: string): Promise<any> {
        return await this.get<any>(`v1/admin/config/${id}`);
    }

    public async adminGetGlobalConfig(): Promise<any> {
        return await this.get<any>('v1/admin/config');
    }

    // --- SUBSCRIPTION REQUESTS ---

    public async getUnifiedPaymentHistory(): Promise<UnifiedPaymentResponse[]> {
        return await this.get<UnifiedPaymentResponse[]>('v1/subscription-requests/unified-history');
    }

    public async approveSubscriptionRequest(id: string): Promise<void> {
        await this.post(`v1/subscription-requests/${id}/approve`);
    }

    public async rejectSubscriptionRequest(id: string, reason: string): Promise<void> {
        await this.post(`v1/subscription-requests/${id}/reject`, { reason });
    }

    // --- SETTINGS ---

    public async getSystemSettings(): Promise<SystemSettingResponse[]> {
        return await this.get<SystemSettingResponse[]>('v1/admin/settings');
    }

    public async updateSystemSetting(key: string, value: string): Promise<void> {
        await this.put(`v1/admin/settings/${key}`, value, { headers: { 'Content-Type': 'application/json' } });
    }

    public async updateSystemSettingsBatch(settings: Record<string, string>): Promise<void> {
        await this.post('v1/admin/settings/batch', settings);
    }

    // --- HACIENDA QUEUE ---

    public async retryAllHaciendaDocuments(): Promise<void> {
        await this.post('v1/hacienda/cola/reintentar-todo');
    }

    // --- ADMIN AUTH ---

    public async getAdminUsers(): Promise<AdminAuthUserResponse[]> {
        return await this.get<AdminAuthUserResponse[]>('v1/admin/auth/users');
    }

    public async registerAdminUser(request: AdminRegisterRequest): Promise<AdminAuthUserResponse> {
        return await this.post<AdminAuthUserResponse>('v1/admin/auth/register', request);
    }

    public async updateAdminUser(id: string, request: AdminUpdateRequest): Promise<void> {
        await this.put(`v1/admin/auth/users/${id}`, request);
    }

    public async deleteAdminUser(id: string): Promise<void> {
        await this.delete(`v1/admin/auth/users/${id}`);
    }

    // --- ANALYTICS ---

    public async getGlobalStats(): Promise<GlobalStatsResponse> {
        return await this.get<GlobalStatsResponse>('v1/admin/analytics/stats');
    }

    // --- SETTLEMENTS (ADMIN) ---

    public async getAdminSettlements(): Promise<CommissionSettlementDto[]> {
        return await this.get<CommissionSettlementDto[]>('v1/admin/settlements');
    }

    public async paySettlement(settlementId: string, request: SettlementPayRequest): Promise<void> {
        await this.post(`v1/admin/settlements/${settlementId}/pay`, request);
    }

    // --- APPROVALS ---

    public async getPendingApprovals(): Promise<PendingApprovalDto[]> {
        return await this.get<PendingApprovalDto[]>('v1/admin/approvals/pending');
    }

    public async approveApproval(id: string, type: number, request: ApproveRequest): Promise<void> {
        console.log("[SDK] approveApproval POST:", { url: `v1/admin/approvals/${id}/${type}/approve`, request });
        await this.post(`v1/admin/approvals/${id}/${type}/approve`, request);
    }

    public async rejectApproval(id: string, type: number, request: ApprovalRejectRequest): Promise<void> {
        await this.post(`v1/admin/approvals/${id}/${type}/reject`, request);
    }

    // --- INVOICES (ADMIN) ---

    public async getPendingInvoices(): Promise<SubscriptionOrderDto[]> {
        return await this.get<SubscriptionOrderDto[]>('v1/admin/invoices/pending');
    }

    public async retryInvoice(id: string): Promise<any> {
        return await this.post(`v1/admin/invoices/${id}/retry`, {});
    }

    // --- PLATFORM MODULES (ADMIN) ---

    public async adminGetPlatformModules(): Promise<PlatformModuleResponse[]> {
        return await this.get<PlatformModuleResponse[]>('v1/admin/platform-modules');
    }

    public async adminGetPlatformModule(id: string): Promise<PlatformModuleResponse> {
        return await this.get<PlatformModuleResponse>(`v1/admin/platform-modules/${id}`);
    }

    public async adminCreatePlatformModule(request: CreatePlatformModuleRequest): Promise<PlatformModuleResponse> {
        return await this.post<PlatformModuleResponse>('v1/admin/platform-modules', request);
    }

    public async adminUpdatePlatformModule(id: string, request: UpdatePlatformModuleRequest): Promise<PlatformModuleResponse> {
        return await this.put<PlatformModuleResponse>(`v1/admin/platform-modules/${id}`, request);
    }

    public async adminDeletePlatformModule(id: string): Promise<void> {
        await this.delete(`v1/admin/platform-modules/${id}`);
    }

    // --- PARTNERS ---

    public async getPartnerProfile(): Promise<PartnerProfile | null> {
        try {
            return await this.get<PartnerProfile>('partners/profile');
        } catch (e: any) {
            if (e.response && e.response.status === 404) return null;
            throw e;
        }
    }

    public async registerPartner(request: PartnerRegisterRequest): Promise<PartnerProfile> {
        return await this.post<PartnerProfile>('partners/register', request);
    }

    public async getPartnerCommissions(): Promise<SalesCommissionDto[]> {
        return await this.get<SalesCommissionDto[]>('partners/commissions');
    }

    public async getPartnerClients(): Promise<PartnerClient[]> {
        return await this.get<PartnerClient[]>('partners/clients');
    }

    public async getSubscriptionPlans(): Promise<PublicPlan[]> {
        return await this.get<PublicPlan[]>('v1/subscription-plans');
    }

    public async getSubscriptionPlanById(id: string): Promise<PublicPlan> {
        return await this.get<PublicPlan>(`v1/subscription-plans/${id}`);
    }

    public async getDashboardStats(): Promise<DashboardStatsResponse> {
        return await this.get<DashboardStatsResponse>('v1/dashboard/stats');
    }

    public async getSubscriptionHistory(): Promise<SubscriptionOrderDto[]> {
        return await this.get<SubscriptionOrderDto[]>('v1/subscription-orders/my-history');
    }

    public async createSubscriptionOrder(request: CreateOrderRequest): Promise<SubscriptionOrderDto> {
        return await this.post<SubscriptionOrderDto>('v1/subscription-orders', request);
    }

    public async reportSubscriptionPayment(request: ReportOrderPaymentRequest): Promise<SubscriptionOrderDto> {
        return await this.post<SubscriptionOrderDto>('v1/subscription-orders/report-payment', request);
    }

    public async getAdminBillingSettings(): Promise<Record<string, string>> {
        return await this.get<Record<string, string>>('v1/admin/settings/billing');
    }

    // --- MARKETING ---

    public async registerLead(request: LeadRegisterRequest): Promise<void> {
        await this.post('v1/marketing/leads/register', request);
    }

    public async adminGetLeads(): Promise<any[]> {
        return await this.get<any[]>('v1/marketing/leads');
    }

    // --- PARTNERS / COMMISSIONS ---

    public async getMySettlements(): Promise<CommissionSettlementDto[]> {
        return await this.get<CommissionSettlementDto[]>('v1/sellers/me/settlements');
    }

    public async getAvailableCommissions(): Promise<SalesCommissionDto[]> {
        return await this.get<SalesCommissionDto[]>('v1/sellers/me/settlements/available');
    }

    public async createSettlement(request: CreateSettlementRequest): Promise<CommissionSettlementDto> {
        return await this.post<CommissionSettlementDto>('v1/sellers/me/settlements', request);
    }

    public async generateSettlementInvoice(settlementId: string): Promise<CommissionSettlementDto> {
        return await this.post<CommissionSettlementDto>(`v1/sellers/me/settlements/${settlementId}/invoice`);
    }

    // --- LICENSES & ADDONS ---

    public async getAvailablePackages(): Promise<LicensePackageResponse[]> {
        return await this.get<LicensePackageResponse[]>('v1/LicenseAddon/packages');
    }

    public async purchaseAddon(request: PurchaseAddonRequest): Promise<LicenseAddonResponse> {
        return await this.post<LicenseAddonResponse>('v1/LicenseAddon/purchase', request);
    }

    public async reportAddonPurchase(request: ReportAddonPurchaseRequest): Promise<LicenseAddonResponse> {
        return await this.post<LicenseAddonResponse>('v1/LicenseAddon/report', request);
    }

    public async getTenantAddons(): Promise<LicenseAddonResponse[]> {
        return await this.get<LicenseAddonResponse[]>('v1/LicenseAddon');
    }

    public async cancelAddon(addonId: string): Promise<void> {
        await this.post(`v1/LicenseAddon/${addonId}/cancel`);
    }


    public async adminGetLicenseLimits(): Promise<any[]> {
        return await this.get<any[]>('v1/admin/licenses/limits');
    }

    // --- DEVELOPER SETTLEMENTS ---
    public async adminGetDeveloperSettlements(): Promise<any[]> {
        return await this.get<any[]>('v1/developer-settlements/admin/settlements');
    }

    public async getMyDeveloperSettlements(): Promise<any[]> {
        return await this.get<any[]>('v1/developer-settlements/me/settlements');
    }

    public async getDeveloperPendingCommissions(): Promise<any[]> {
        return await this.get<any[]>('v1/developer-settlements/me/commissions/pending');
    }

    public async createDeveloperSettlement(commissionIds: string[]): Promise<any> {
        return await this.post<any>('v1/developer-settlements/me/settlements', commissionIds);
    }

    public async payDeveloperSettlement(id: string, reference: string, paymentMethod: string = 'Transferencia'): Promise<any> {
        return await this.post<any>(`v1/developer-settlements/me/settlements/${id}/pay`, { reference, paymentMethod });
    }

    public async getBillingSummary(): Promise<BillingSummaryResponse> {
        return await this.get<BillingSummaryResponse>('v1/LicenseAddon/billing-summary');
    }

    public async getAddonPackages(): Promise<any[]> {
        return await this.get<any[]>('v1/LicenseAddon/packages');
    }

    // --- ADMIN LICENSES ---

    public async adminGetAllAddons(status?: string): Promise<LicenseAddonResponse[]> {
        return await this.get<LicenseAddonResponse[]>('v1/LicenseAddon/admin/addons', { params: { status } });
    }

    public async adminGetAllPackages(): Promise<any[]> {
        return await this.get<any[]>('v1/LicenseAddon/admin/packages');
    }

    public async adminGetPackage(id: string): Promise<any> {
        return await this.get<any>(`v1/LicenseAddon/admin/packages/${id}`);
    }

    public async adminCreatePackage(data: any): Promise<void> {
        await this.post('v1/LicenseAddon/admin/packages', data);
    }

    public async adminUpdatePackage(id: string, data: any): Promise<void> {
        await this.put(`v1/LicenseAddon/admin/packages/${id}`, data);
    }

    public async adminDeletePackage(id: string): Promise<void> {
        await this.delete(`v1/LicenseAddon/admin/packages/${id}`);
    }

    public async adminGetPendingAddons(): Promise<LicenseAddonResponse[]> {
        return await this.get<LicenseAddonResponse[]>('v1/LicenseAddon/pending');
    }

    public async adminApproveAddon(addonId: string, request: ApproveAddonRequest): Promise<void> {
        await this.post(`v1/LicenseAddon/${addonId}/approve`, request);
    }

    public async adminRejectAddon(addonId: string, request: RejectAddonRequest): Promise<void> {
        await this.post(`v1/LicenseAddon/${addonId}/reject`, request);
    }

    // --- LICENSES (MAIN) ---

    public async getTenantLicenses(): Promise<TenantLicense[]> {
        return await this.get<TenantLicense[]>('v1/licenses');
    }

    public async adminGetAllLicenses(): Promise<LicenseResponse[]> {
        return await this.get<LicenseResponse[]>('v1/admin/licenses');
    }

    public async issueLicense(tenantId: string, request: IssueLicenseRequest): Promise<LicenseResponse> {
        const originalTenantId = this.tenantId;
        try {
            this.setTenantId(tenantId);
            return await this.post<LicenseResponse>('v1/admin/licenses', request);
        } finally {
            this.setTenantId(originalTenantId);
        }
    }

    public async revokeLicense(licenseId: string): Promise<void> {
        await this.delete(`v1/licenses/${licenseId}`);
    }

    // --- Webhooks ---

    public async getWebhooks(): Promise<any[]> {
        return await this.get<any[]>('v1/webhooks');
    }

    public async createWebhook(data: any): Promise<any> {
        return await this.post<any>('v1/webhooks', data);
    }

    public async deleteWebhook(id: string): Promise<void> {
        return await this.delete(`v1/webhooks/${id}`);
    }

    public async getDocumentsJson(filters: any): Promise<any> {
        return await this.get('v1/documentos-json', { params: filters });
    }

    // --- IDENTITY & ZERO TRUST (V4) ---

    /**
     * Refresca la sesión actual usando un Refresh Token.
     */
    public async refreshSession(refreshToken: string): Promise<LoginResult> {
        const data = await this.post<any>('v1/auth/refresh', { refreshToken });
        const result = this.normalizeResponse<LoginResult>(data);
        
        if (result.token || result.accessToken) this.setToken((result.token || result.accessToken)!);
        return result;
    }

    /**
     * Cierra la sesión actual.
     */
    public async logout(refreshToken: string): Promise<void> {
        await this.post('v1/auth/logout', { refreshToken });
    }

    // --- WebAuthn (Passkeys) ---

    public async getWebAuthnRegisterOptions(email: string): Promise<any> {
        return await this.post('v1/auth/webauthn/register/options', { email });
    }

    public async completeWebAuthnRegister(userId: string, attestationResponse: any): Promise<any> {
        return await this.post(`v1/auth/webauthn/register?userId=${userId}`, attestationResponse);
    }

    public async getWebAuthnLoginOptions(email: string, sessionId?: string): Promise<any> {
        return await this.post('v1/auth/webauthn/login/options', { email, sessionId });
    }

    public async completeWebAuthnLogin(userId: string | undefined, assertionResponse: any, context?: LoginContext & { sessionId?: string }): Promise<LoginResult> {
        const url = userId ? `v1/auth/webauthn/login?userId=${userId}` : `v1/auth/webauthn/login`;
        const data = await this.post<any>(url, {
            response: assertionResponse,
            userId,
            ...context
        });
        const result = this.normalizeResponse<LoginResult>(data);

        if (result.token) this.setToken(result.token);
        return result;
    }

    // --- SSO (Single Sign-On) ---

    public async generateSsoToken(targetAudience?: string, targetTenantId?: string): Promise<{ ssoToken: string }> {
        return await this.post<{ ssoToken: string }>('v1/auth/sso/generate', { targetAudience, targetTenantId });
    }

    public async exchangeSsoToken(ssoToken: string, audience?: string, context?: LoginContext): Promise<LoginResult> {
        const data = await this.post<any>('v1/auth/sso/exchange', { 
            ssoToken, 
            audience,
            ...context
        });
        const result = this.normalizeResponse<LoginResult>(data);
        
        if (result.token) this.setToken(result.token);
        return result;
    }

    public async sendMagicLink(email: string): Promise<{ message: string }> {
        return await this.post('v1/auth/magic-link/send', { 
            email,
            deviceId: this.securityContext?.deviceId,
            fingerprint: this.securityContext?.fingerprint
        });
    }

    public async verifyMagicLink(token: string): Promise<LoginResult> {
        const data = await this.post<any>('v1/auth/magic-link/verify', { 
            token,
            deviceId: this.securityContext?.deviceId,
            fingerprint: this.securityContext?.fingerprint
        });
        const result = this.normalizeResponse<LoginResult>(data);
        if (result.token) this.setToken(result.token);
        return result;
    }
}
