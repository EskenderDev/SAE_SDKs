"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SystemLogService = void 0;
class SystemLogService {
    constructor(client) {
        this.client = client;
    }
    async getLogs(filter) {
        const params = new URLSearchParams();
        if (filter.tenantId)
            params.append('tenantId', filter.tenantId);
        if (filter.level !== undefined)
            params.append('level', filter.level.toString());
        if (filter.module)
            params.append('module', filter.module);
        if (filter.fromDate)
            params.append('fromDate', filter.fromDate);
        if (filter.toDate)
            params.append('toDate', filter.toDate);
        params.append('page', filter.page.toString());
        params.append('pageSize', filter.pageSize.toString());
        return this.client.get(`v1/admin/logs?${params.toString()}`);
    }
    async getLogDetails(id) {
        return this.client.get(`v1/admin/logs/${id}`);
    }
    async getDeveloperLogs(filter) {
        const params = new URLSearchParams();
        if (filter.tenantId)
            params.append('tenantId', filter.tenantId);
        if (filter.level !== undefined)
            params.append('level', filter.level.toString());
        if (filter.module)
            params.append('module', filter.module);
        if (filter.fromDate)
            params.append('fromDate', filter.fromDate);
        if (filter.toDate)
            params.append('toDate', filter.toDate);
        params.append('page', filter.page.toString());
        params.append('pageSize', filter.pageSize.toString());
        return this.client.get(`v1/developer/logs?${params.toString()}`);
    }
}
exports.SystemLogService = SystemLogService;
