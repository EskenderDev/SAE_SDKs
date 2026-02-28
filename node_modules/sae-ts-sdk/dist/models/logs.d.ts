export declare enum SystemLogLevel {
    Info = 0,
    Warning = 1,
    Error = 2,
    Critical = 3
}
export interface SystemLog {
    id: string;
    tenantId?: string;
    tenantName?: string;
    userId?: string;
    userName?: string;
    level: SystemLogLevel;
    module: string;
    action: string;
    message: string;
    exception?: string;
    data?: string;
    createdAt: string;
    ipAddress?: string;
}
export interface LogFilter {
    tenantId?: string;
    level?: SystemLogLevel;
    module?: string;
    fromDate?: string;
    toDate?: string;
    page: number;
    pageSize: number;
}
export interface LogResponse {
    data: SystemLog[];
    total: number;
    page: number;
    pageSize: number;
}
