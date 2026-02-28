import { ApiClient } from '../client/SaeClient';
import { LogFilter, LogResponse } from '../models/logs';

export class SystemLogService {
    private client: ApiClient;

    constructor(client: ApiClient) {
        this.client = client;
    }

    async getLogs(filter: LogFilter): Promise<LogResponse> {
        const params = new URLSearchParams();
        if (filter.tenantId) params.append('tenantId', filter.tenantId);
        if (filter.level !== undefined) params.append('level', filter.level.toString());
        if (filter.module) params.append('module', filter.module);
        if (filter.fromDate) params.append('fromDate', filter.fromDate);
        if (filter.toDate) params.append('toDate', filter.toDate);
        params.append('page', filter.page.toString());
        params.append('pageSize', filter.pageSize.toString());

        return this.client.get<LogResponse>(`v1/admin/logs?${params.toString()}`);
    }

    async getLogDetails(id: string): Promise<any> {
        return this.client.get<any>(`v1/admin/logs/${id}`);
    }

    async getDeveloperLogs(filter: LogFilter): Promise<LogResponse> {
        const params = new URLSearchParams();
        if (filter.tenantId) params.append('tenantId', filter.tenantId);
        if (filter.level !== undefined) params.append('level', filter.level.toString());
        if (filter.module) params.append('module', filter.module);
        if (filter.fromDate) params.append('fromDate', filter.fromDate);
        if (filter.toDate) params.append('toDate', filter.toDate);
        params.append('page', filter.page.toString());
        params.append('pageSize', filter.pageSize.toString());

        return this.client.get<LogResponse>(`v1/developer/logs?${params.toString()}`);
    }
}
