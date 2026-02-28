"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.SaeClient = void 0;
const axios_1 = __importDefault(require("axios"));
const systemLogService_1 = require("../services/systemLogService");
class SaeClient {
    constructor(baseUrl = "https://localhost:5000", token, apiKey) {
        this.baseUrl = baseUrl.replace(/\/$/, ""); // Remove trailing slash
        // Ensure /api suffix if not present (unless it's just the host, then we assume /api is needed)
        const apiBaseUrl = this.baseUrl.endsWith('/api') ? this.baseUrl : `${this.baseUrl}/api`;
        this.token = token;
        this.apiKey = apiKey;
        this.http = axios_1.default.create({
            baseURL: apiBaseUrl,
            headers: {
                'Content-Type': 'application/json'
            }
        });
        this.logs = new systemLogService_1.SystemLogService(this);
        this.updateHeaders();
    }
    updateHeaders() {
        if (this.token) {
            this.http.defaults.headers.common['Authorization'] = `Bearer ${this.token}`;
            delete this.http.defaults.headers.common['X-API-KEY'];
        }
        else if (this.apiKey) {
            this.http.defaults.headers.common['X-API-KEY'] = this.apiKey;
            delete this.http.defaults.headers.common['Authorization'];
        }
        else {
            delete this.http.defaults.headers.common['Authorization'];
            delete this.http.defaults.headers.common['X-API-KEY'];
        }
        if (this.tenantId) {
            this.http.defaults.headers.common['X-Tenant-Id'] = this.tenantId;
        }
        else {
            delete this.http.defaults.headers.common['X-Tenant-Id'];
        }
        if (this.terminalKey) {
            this.http.defaults.headers.common['X-Terminal-Key'] = this.terminalKey;
        }
        else {
            delete this.http.defaults.headers.common['X-Terminal-Key'];
        }
    }
    setToken(token) {
        this.token = token;
        this.apiKey = undefined;
        this.updateHeaders();
    }
    setApiKey(apiKey) {
        this.apiKey = apiKey;
        this.token = undefined;
        this.updateHeaders();
    }
    setTenantId(tenantId) {
        this.tenantId = tenantId;
        this.updateHeaders();
    }
    setTerminalKey(terminalKey) {
        this.terminalKey = terminalKey;
        this.updateHeaders();
    }
    // --- GENERIC HTTP METHODS ---
    async get(url, config) {
        const response = await this.http.get(url, config);
        return response.data;
    }
    async post(url, data, config) {
        const response = await this.http.post(url, data, config);
        return response.data;
    }
    async put(url, data, config) {
        const response = await this.http.put(url, data, config);
        return response.data;
    }
    async delete(url, config) {
        const response = await this.http.delete(url, config);
        return response.data;
    }
    // --- AUTH ---
    async login(email, password) {
        const response = await this.http.post('v1/auth/login', { Username: email, Password: password });
        if (response.data.token)
            this.setToken(response.data.token);
        return response.data;
    }
    async adminLogin(email, password) {
        const response = await this.http.post('v1/admin/auth/login', { Username: email, Password: password });
        if (response.data.token)
            this.setToken(response.data.token);
        return response.data;
    }
    async forgotPassword(email) {
        return await this.post('v1/auth/forgot-password', { email });
    }
    async resetPassword(token, newPassword) {
        return await this.post('v1/auth/reset-password', { token, newPassword });
    }
    async register(request) {
        await this.post('v1/auth/register', request);
    }
    async resendVerification(email) {
        await this.post('v1/auth/resend-verification', { email });
    }
    async confirmEmail(request) {
        await this.post('v1/auth/confirm-email', request);
    }
    async adminForgotPassword(email) {
        await this.post('v1/admin/auth/forgot-password', { email });
    }
    async adminResetPassword(request) {
        await this.post('v1/admin/auth/reset-password', request);
    }
    async adminMe() {
        return await this.get('v1/admin/auth/me');
    }
    async adminImpersonate(tenantId) {
        return await this.post(`v1/admin/auth/impersonate/${tenantId}`);
    }
    async adminGetPendingApprovals() {
        return await this.getPendingApprovals();
    }
    async adminApproveApproval(id, type, overrideTenantId) {
        return await this.approveApproval(id, type, overrideTenantId);
    }
    async adminRejectApproval(id, type, reason) {
        return await this.rejectApproval(id, type, reason);
    }
    async adminGetRedisQueues() {
        return await this.get('v1/admin/redis/queues');
    }
    async adminGetBillingSettings() {
        return await this.get('v1/admin/settings/billing');
    }
    async adminUpdateSettingsBatch(settings) {
        await this.updateSystemSettingsBatch(settings);
    }
    async adminGetPlans() {
        return await this.get('v1/subscription-plans/admin/list');
    }
    async adminGetPlan(id) {
        return await this.get(`v1/subscription-plans/${id}`);
    }
    async adminCreatePlan(data) {
        await this.post('v1/subscription-plans', data);
    }
    async adminUpdatePlan(id, data) {
        await this.put(`v1/subscription-plans/${id}`, data);
    }
    async adminDeletePlan(id) {
        await this.delete(`v1/subscription-plans/${id}`);
    }
    // --- MANAGEMENT ---
    async getCurrentTenant() {
        return await this.get('v1/tenants/current');
    }
    async updateCurrentTenant(request) {
        return await this.put('v1/tenants/current', request);
    }
    async regenerateApiKey() {
        return await this.post('v1/tenants/api-key/regenerate');
    }
    async getMyTenants() {
        return await this.get('v1/tenants/my-tenants');
    }
    async registerTenant(name, taxId) {
        return await this.post('v1/tenants/register', { name, taxId });
    }
    async getTenantBillingSettings(tenantId) {
        return await this.get(`v1/tenants/${tenantId}/billing-settings`);
    }
    async uploadTenantLogo(formData) {
        return await this.post('v1/tenants/current/logo', formData);
    }
    async deleteTenantLogo() {
        return await this.delete('v1/tenants/current/logo');
    }
    // --- TENANT USERS ---
    async getTenantUsers() {
        return await this.get('v1/tenant-users');
    }
    async inviteTenantUser(request) {
        return await this.post('v1/tenant-users/invite', request);
    }
    async acceptInvitation(request) {
        return await this.post('v1/tenant-users/accept-invite', request);
    }
    async removeTenantUser(id) {
        await this.delete(`v1/tenant-users/${id}`);
    }
    async updateTenantUserRole(id, request) {
        await this.put(`v1/tenant-users/${id}/role`, request);
    }
    // --- ROLES & PERMISSIONS ---
    async getRoles() {
        return await this.get('v1/tenant-roles');
    }
    async getPermissions() {
        return await this.get('v1/tenant-roles/permissions');
    }
    async createRole(request) {
        return await this.post('v1/tenant-roles', request);
    }
    async updateRole(id, request) {
        return await this.put(`v1/tenant-roles/${id}`, request);
    }
    async deleteRole(id) {
        await this.delete(`v1/tenant-roles/${id}`);
    }
    // --- CATALOGS ---
    async getProvincias() {
        return await this.get('v1/catalogs/provincias');
    }
    async getCantones(provinceCode) {
        return await this.get(`v1/catalogs/cantones/${provinceCode}`);
    }
    async getDistritos(provinceCode, cantonCode) {
        return await this.get(`v1/catalogs/distritos/${provinceCode}/${cantonCode}`);
    }
    async getCurrencies(search) {
        let url = 'v1/catalogs/monedas';
        if (search) {
            url += `?search=${encodeURIComponent(search)}`;
        }
        return await this.get(url);
    }
    async getEconomicActivities(search) {
        let url = 'v1/catalogs/economic-activities';
        if (search) {
            url += `?search=${encodeURIComponent(search)}`;
        }
        return await this.get(url);
    }
    // --- DOCUMENTS ---
    async generarDocumento(documento) {
        return await this.post('v1/documentos/generar', documento);
    }
    async enviarDocumento(documento) {
        return await this.post('v1/hacienda/enviar', documento);
    }
    async emitir(request) {
        return await this.post('v1/hacienda/emitir', request);
    }
    async consultarEstado(clave) {
        return await this.get(`v1/hacienda/estado/${clave}`);
    }
    async endosarDocumento(request) {
        return await this.post('v1/hacienda/endosar', request);
    }
    async aceptarDocumento(request) {
        return await this.post('v1/hacienda/aceptar', request);
    }
    async getUsedCurrencies() {
        return await this.get('v1/documentos-json/monedas');
    }
    async getDocumentById(id) {
        return await this.get(`v1/documentos-json/${id}`);
    }
    async getDocumentXml(id) {
        return await this.get(`v1/documentos-json/${id}/xml`, { responseType: 'arraybuffer' });
    }
    async getDocumentResponseXml(id) {
        return await this.get(`v1/documentos-json/${id}/respuesta`, { responseType: 'arraybuffer' });
    }
    async getDocumentPdf(id) {
        return await this.get(`v1/documentos-json/${id}/pdf`, { responseType: 'arraybuffer' });
    }
    async getDocumentPdfByClave(clave) {
        return await this.get(`v1/documents/print/${clave}`, { responseType: 'arraybuffer' });
    }
    async buscarDocumentos(params) {
        return await this.get('v1/documentos-json', { params });
    }
    async exportDocuments(format, filters) {
        return await this.get(`v1/documentos-json/export/${format}`, { params: filters, responseType: 'arraybuffer' });
    }
    async getDocumentSummary(fechaDesde, fechaHasta) {
        return await this.get('v1/documentos-json/resumen', { params: { fechaDesde, fechaHasta } });
    }
    async retryDocument(clave) {
        await this.post(`v1/documentos-json/${clave}/reprocess-rejection`);
    }
    async regenerateDocument(clave, newConsecutive) {
        return await this.post(`v1/documentos/regenerate/${clave}`, null, { params: { newConsecutive } });
    }
    async getNextConsecutive(sucursal, terminal, tipoDocumento) {
        return await this.get('v1/documentos/next-consecutive', { params: { sucursal, terminal, tipo: tipoDocumento } });
    }
    async resendDocumentEmail(id) {
        await this.post(`v1/documentos-json/${id}/resend-email`);
    }
    async forceDocumentRejection(id) {
        await this.post(`v1/documentos-json/${id}/force-rejection`);
    }
    async getDocumentSafetyScore(clave) {
        return await this.get(`v1/documents/safety-score/${clave}`);
    }
    // --- REPORTS ---
    async getReport(type, mes, anio) {
        return await this.get(`v1/reports/${type}`, { params: { mes, anio } });
    }
    async downloadReport(type, format, mes, anio) {
        return await this.get(`v1/reports/${type}/${format}`, { params: { mes, anio }, responseType: 'arraybuffer' });
    }
    // --- HACIENDA CONFIG ---
    async getHaciendaConfig() {
        return await this.get('v1/hacienda/config');
    }
    async saveHaciendaConfig(config) {
        return await this.put('v1/hacienda/config', config);
    }
    async getHaciendaEnvironments() {
        return await this.get('v1/hacienda/config/environments');
    }
    async getHaciendaConfigByEnvironment(environment) {
        return await this.get(`v1/hacienda/config/environment/${environment}`);
    }
    async switchHaciendaEnvironment(environment) {
        // Enforce numeric for backend if it's a number-like string, otherwise send as is
        const envVal = typeof environment === 'string' && !isNaN(Number(environment))
            ? Number(environment)
            : environment;
        return await this.post('v1/hacienda/config/environment/switch', { environment: envVal });
    }
    // --- BRANCHES & TERMINALS ---
    async getBranches() {
        return await this.get('v1/Branches');
    }
    async getBranch(id) {
        return await this.get(`v1/Branches/${id}`);
    }
    async createBranch(request) {
        return await this.post('v1/Branches', request);
    }
    async updateBranch(id, request) {
        await this.put(`v1/Branches/${id}`, request);
    }
    async deleteBranch(id) {
        await this.delete(`v1/Branches/${id}`);
    }
    async getTerminals(branchId) {
        return await this.get(`v1/terminals?branchId=${branchId}`);
    }
    async getTerminal(id) {
        return await this.get(`v1/terminals/${id}`);
    }
    async createTerminal(branchId, request) {
        return await this.post(`v1/terminals?branchId=${branchId}`, request);
    }
    async updateTerminal(id, request) {
        await this.put(`v1/terminals/${id}`, request);
    }
    async deleteTerminal(id) {
        await this.delete(`v1/terminals/${id}`);
    }
    async getTerminalSequences(terminalId) {
        return await this.get(`v1/terminals/${terminalId}/sequences`);
    }
    async updateTerminalSequence(terminalId, docType, request) {
        await this.put(`v1/terminals/${terminalId}/sequences/${docType}`, request);
    }
    async getNextConsecutiveManual(sucursal, terminal, tipoDocumento) {
        return await this.get('v1/documentos/next-consecutive', { params: { sucursal, terminal, tipo: tipoDocumento } });
    }
    async getNextConsecutiveByTerminal(terminalId, docType) {
        return await this.get(`v1/terminals/${terminalId}/sequences/${docType}/next`);
    }
    async generateTerminalLicense(terminalId) {
        return await this.post(`v1/terminals/${terminalId}/generate-license`);
    }
    // --- LICENSE PACKAGES ---
    async getLicensePackages(systemOnly = false, tenantId) {
        let url = `v1/license-packages?systemOnly=${systemOnly}`;
        if (tenantId)
            url += `&tenantId=${tenantId}`;
        return await this.get(url);
    }
    async getLicensePackage(id) {
        return await this.get(`v1/license-packages/${id}`);
    }
    async createLicensePackage(request) {
        return await this.post('v1/license-packages', request);
    }
    async updateLicensePackage(id, request) {
        await this.put(`v1/license-packages/${id}`, request);
    }
    async deleteLicensePackage(id) {
        await this.delete(`v1/license-packages/${id}`);
    }
    /** Updates the active modules for a specific license. Returns { activeModules, allowedModules }. */
    async updateLicenseModules(id, moduleCodes) {
        return await this.put(`v1/licenses/${id}/modules`, moduleCodes);
    }
    /** Gets the allowed and active modules for a specific license without modifying anything. */
    async getLicenseModules(id) {
        return await this.get(`v1/licenses/${id}/modules`);
    }
    // --- DEVELOPER API ---
    async getDeveloperClients() {
        return await this.get('v1/developer/clients');
    }
    async getDeveloperApplications() {
        return await this.get('v1/developer/applications');
    }
    async registerDeveloperApplication(request) {
        return await this.post('v1/developer/applications', request);
    }
    async deactivateDeveloperApplication(id) {
        await this.delete(`v1/developer/applications/${id}`);
    }
    async getSaeIdentifiers() {
        return await this.get('v1/developer/applications/sae-identifiers');
    }
    async getDeveloperModules() {
        return await this.get('v1/developer/modules');
    }
    async createDeveloperModule(request) {
        return await this.post('v1/developer/modules', request);
    }
    async validateModuleCode(code) {
        return await this.get(`v1/developer/modules/validate-code/${code}`);
    }
    async updateDeveloperModule(id, request) {
        return await this.put(`v1/developer/modules/${id}`, request);
    }
    async deleteDeveloperModule(id) {
        await this.delete(`v1/developer/modules/${id}`);
    }
    // --- LICENSE API ---
    async validateLicense(request) {
        return await this.post('v1/licenses/validate', request);
    }
    async activateLicense(request) {
        return await this.post('v1/licenses/activate', request);
    }
    async adminUpdateLicenseModules(licenseId, modules) {
        await this.put(`v1/admin/licenses/${licenseId}/modules`, { modules });
    }
    // --- ADMIN API ---
    async adminGetUsers() {
        return await this.get('v1/admin/users');
    }
    async adminUpgradeSubscription(tenantId, tier) {
        await this.post(`v1/admin/upgrade-subscription`, { tenantId, tier });
    }
    async adminUpdateTenant(id, request) {
        await this.put(`v1/admin/tenants/${id}`, request);
    }
    async adminGetTenantConfig(id) {
        return await this.get(`v1/admin/config/${id}`);
    }
    async adminGetGlobalConfig() {
        return await this.get('v1/admin/config');
    }
    // --- SUBSCRIPTION REQUESTS ---
    async getUnifiedPaymentHistory() {
        return await this.get('v1/subscription-requests/unified-history');
    }
    async approveSubscriptionRequest(id) {
        await this.post(`v1/subscription-requests/${id}/approve`);
    }
    async rejectSubscriptionRequest(id, reason) {
        await this.post(`v1/subscription-requests/${id}/reject`, { reason });
    }
    // --- SETTINGS ---
    async getSystemSettings() {
        return await this.get('v1/admin/settings');
    }
    async updateSystemSetting(key, value) {
        await this.put(`v1/admin/settings/${key}`, value, { headers: { 'Content-Type': 'application/json' } });
    }
    async updateSystemSettingsBatch(settings) {
        await this.post('v1/admin/settings/batch', settings);
    }
    // --- HACIENDA QUEUE ---
    async retryAllHaciendaDocuments() {
        await this.post('v1/hacienda/cola/reintentar-todo');
    }
    // --- ADMIN AUTH ---
    async getAdminUsers() {
        return await this.get('v1/admin/auth/users');
    }
    async registerAdminUser(request) {
        return await this.post('v1/admin/auth/register', request);
    }
    async updateAdminUser(id, request) {
        await this.put(`v1/admin/auth/users/${id}`, request);
    }
    async deleteAdminUser(id) {
        await this.delete(`v1/admin/auth/users/${id}`);
    }
    // --- ANALYTICS ---
    async getGlobalStats() {
        return await this.get('v1/admin/analytics/stats');
    }
    // --- SETTLEMENTS (ADMIN) ---
    async getAdminSettlements() {
        return await this.get('v1/admin/settlements');
    }
    async paySettlement(settlementId, request) {
        await this.post(`v1/admin/settlements/${settlementId}/pay`, request);
    }
    // --- APPROVALS ---
    async getPendingApprovals() {
        return await this.get('v1/admin/approvals/pending');
    }
    async approveApproval(id, type, overrideTenantId) {
        let url = `v1/admin/approvals/${id}/${type}/approve`;
        if (overrideTenantId)
            url += `?overrideTenantId=${overrideTenantId}`;
        await this.post(url, {});
    }
    async rejectApproval(id, type, reason) {
        await this.post(`v1/admin/approvals/${id}/${type}/reject`, { reason });
    }
    // --- INVOICES (ADMIN) ---
    async getPendingInvoices() {
        return await this.get('v1/admin/invoices/pending');
    }
    async retryInvoice(id) {
        return await this.post(`v1/admin/invoices/${id}/retry`, {});
    }
    // --- PLATFORM MODULES (ADMIN) ---
    async adminGetPlatformModules() {
        return await this.get('v1/admin/platform-modules');
    }
    async adminGetPlatformModule(id) {
        return await this.get(`v1/admin/platform-modules/${id}`);
    }
    async adminCreatePlatformModule(request) {
        return await this.post('v1/admin/platform-modules', request);
    }
    async adminUpdatePlatformModule(id, request) {
        return await this.put(`v1/admin/platform-modules/${id}`, request);
    }
    async adminDeletePlatformModule(id) {
        await this.delete(`v1/admin/platform-modules/${id}`);
    }
    // --- PARTNERS ---
    async getPartnerProfile() {
        try {
            return await this.get('partners/profile');
        }
        catch (e) {
            if (e.response && e.response.status === 404)
                return null;
            throw e;
        }
    }
    async registerPartner(request) {
        return await this.post('partners/register', request);
    }
    async getPartnerCommissions() {
        return await this.get('partners/commissions');
    }
    async getPartnerClients() {
        return await this.get('partners/clients');
    }
    async getSubscriptionPlans() {
        return await this.get('v1/subscription-plans');
    }
    async getSubscriptionPlanById(id) {
        return await this.get(`v1/subscription-plans/${id}`);
    }
    async getDashboardStats() {
        return await this.get('v1/dashboard/stats');
    }
    async getSubscriptionHistory() {
        return await this.get('v1/subscription-orders/my-history');
    }
    async createSubscriptionOrder(request) {
        return await this.post('v1/subscription-orders', request);
    }
    async reportSubscriptionPayment(request) {
        return await this.post('v1/subscription-orders/report-payment', request);
    }
    async getAdminBillingSettings() {
        return await this.get('v1/admin/settings/billing');
    }
    // --- MARKETING ---
    async registerLead(request) {
        await this.post('v1/marketing/leads/register', request);
    }
    async adminGetLeads() {
        return await this.get('v1/marketing/leads');
    }
    // --- PARTNERS / COMMISSIONS ---
    async getMySettlements() {
        return await this.get('v1/sellers/me/settlements');
    }
    async getAvailableCommissions() {
        return await this.get('v1/sellers/me/settlements/available');
    }
    async createSettlement(request) {
        return await this.post('v1/sellers/me/settlements', request);
    }
    async generateSettlementInvoice(settlementId) {
        return await this.post(`v1/sellers/me/settlements/${settlementId}/invoice`);
    }
    // --- LICENSES & ADDONS ---
    async getAvailablePackages() {
        return await this.get('v1/LicenseAddon/packages');
    }
    async purchaseAddon(request) {
        return await this.post('v1/LicenseAddon/purchase', request);
    }
    async reportAddonPurchase(request) {
        return await this.post('v1/LicenseAddon/report', request);
    }
    async getTenantAddons() {
        return await this.get('v1/LicenseAddon');
    }
    async cancelAddon(addonId) {
        await this.post(`v1/LicenseAddon/${addonId}/cancel`);
    }
    async adminGetLicenseLimits() {
        return await this.get('v1/admin/licenses/limits');
    }
    // --- DEVELOPER SETTLEMENTS ---
    async adminGetDeveloperSettlements() {
        return await this.get('v1/developer-settlements/admin/settlements');
    }
    async getMyDeveloperSettlements() {
        return await this.get('v1/developer-settlements/me/settlements');
    }
    async getDeveloperPendingCommissions() {
        return await this.get('v1/developer-settlements/me/commissions/pending');
    }
    async createDeveloperSettlement(commissionIds) {
        return await this.post('v1/developer-settlements/me/settlements', commissionIds);
    }
    async payDeveloperSettlement(id, reference, paymentMethod = 'Transferencia') {
        return await this.post(`v1/developer-settlements/me/settlements/${id}/pay`, { reference, paymentMethod });
    }
    async getBillingSummary() {
        return await this.get('v1/LicenseAddon/billing-summary');
    }
    async getAddonPackages() {
        return await this.get('v1/LicenseAddon/packages');
    }
    // --- ADMIN LICENSES ---
    async adminGetAllAddons(status) {
        return await this.get('v1/LicenseAddon/admin/addons', { params: { status } });
    }
    async adminGetAllPackages() {
        return await this.get('v1/LicenseAddon/admin/packages');
    }
    async adminGetPackage(id) {
        return await this.get(`v1/LicenseAddon/admin/packages/${id}`);
    }
    async adminCreatePackage(data) {
        await this.post('v1/LicenseAddon/admin/packages', data);
    }
    async adminUpdatePackage(id, data) {
        await this.put(`v1/LicenseAddon/admin/packages/${id}`, data);
    }
    async adminDeletePackage(id) {
        await this.delete(`v1/LicenseAddon/admin/packages/${id}`);
    }
    async adminGetPendingAddons() {
        return await this.get('v1/LicenseAddon/pending');
    }
    async adminApproveAddon(addonId, request) {
        await this.post(`v1/LicenseAddon/${addonId}/approve`, request);
    }
    async adminRejectAddon(addonId, request) {
        await this.post(`v1/LicenseAddon/${addonId}/reject`, request);
    }
    // --- LICENSES (MAIN) ---
    async getTenantLicenses() {
        return await this.get('v1/licenses');
    }
    async adminGetAllLicenses() {
        return await this.get('v1/admin/licenses');
    }
    async issueLicense(tenantId, request) {
        const originalTenantId = this.tenantId;
        try {
            this.setTenantId(tenantId);
            return await this.post('v1/admin/licenses', request);
        }
        finally {
            this.setTenantId(originalTenantId);
        }
    }
    async revokeLicense(licenseId) {
        await this.delete(`v1/licenses/${licenseId}`);
    }
    // --- Webhooks ---
    async getWebhooks() {
        return await this.get('v1/webhooks');
    }
    async createWebhook(data) {
        return await this.post('v1/webhooks', data);
    }
    async deleteWebhook(id) {
        return await this.delete(`v1/webhooks/${id}`);
    }
    async getDocumentsJson(filters) {
        return await this.get('v1/documentos-json', { params: filters });
    }
}
exports.SaeClient = SaeClient;
