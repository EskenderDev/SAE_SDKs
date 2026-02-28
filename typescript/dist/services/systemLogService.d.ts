import { ApiClient } from '../client/SaeClient';
import { LogFilter, LogResponse } from '../models/logs';
export declare class SystemLogService {
    private client;
    constructor(client: ApiClient);
    getLogs(filter: LogFilter): Promise<LogResponse>;
    getLogDetails(id: string): Promise<any>;
    getDeveloperLogs(filter: LogFilter): Promise<LogResponse>;
}
