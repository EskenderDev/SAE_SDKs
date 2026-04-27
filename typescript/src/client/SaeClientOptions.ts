import { AxiosRequestConfig } from 'axios';

/**
 * Nivel de log para eventos del cliente SAE.
 */
export enum SaeLogLevel {
    Debug = 'debug',
    Info = 'info',
    Warning = 'warning',
    Error = 'error'
}

/**
 * Evento de log para el cliente SAE.
 */
export interface SaeClientLogEvent {
    timestamp: Date;
    level: SaeLogLevel;
    message: string;
    operation?: string;
    retryAttempt?: number;
    error?: Error;
    duration?: number; // en milisegundos
}

/**
 * Opciones de configuración avanzadas para el cliente SAE TypeScript.
 */
export interface SaeClientOptions {
    /**
     * Timeout para las peticiones HTTP en milisegundos.
     * Por defecto: 100000 (100 segundos).
     */
    timeout?: number;

    /**
     * Número máximo de reintentos en caso de fallos temporales (5xx, timeout, etc.).
     * Por defecto: 3 reintentos.
     */
    maxRetries?: number;

    /**
     * Delay inicial entre reintentos en milisegundos (backoff exponencial).
     * Por defecto: 1000 (1 segundo).
     */
    retryDelay?: number;

    /**
     * Factor de multiplicación para el backoff exponencial.
     * Por defecto: 2 (1s, 2s, 4s, etc.)
     */
    backoffMultiplier?: number;

    /**
     * Indica si se debe ignorar la validación de certificados SSL.
     * ⚠️ SOLO USAR EN DESARROLLO con certificados autofirmados.
     * Por defecto: false.
     * 
     * En Node.js, esto configura rejectUnauthorized en el HTTPS agent.
     * En navegador, esto no tiene efecto (la seguridad es manejada por el navegador).
     */
    skipCertificateValidation?: boolean;

    /**
     * Certificado de cliente para autenticación mTLS (solo Node.js).
     * Formato: { cert: string, key: string, ca?: string }
     */
    clientCertificate?: {
        cert: string;
        key: string;
        ca?: string;
    };

    /**
     * Certificados CA raíz de confianza personalizados (solo Node.js).
     * Array de strings PEM o buffers.
     */
    customRootCertificates?: string[] | Buffer[];

    /**
     * Agente HTTPS personalizado (solo Node.js).
     * Si se proporciona, se usa en lugar de las opciones de certificado.
     */
    httpsAgent?: any;

    /**
     * Número máximo de redirecciones permitidas.
     * Por defecto: 5.
     */
    maxRedirects?: number;

    /**
     * Indica si se deben seguir las redirecciones automáticamente.
     * Por defecto: true.
     */
    followRedirects?: boolean;

    /**
     * Callback para logging de eventos de red (debug, errores, reintentos).
     */
    onLog?: (event: SaeClientLogEvent) => void;

    /**
     * Configuración adicional de Axios.
     * Estas opciones se fusionan con la configuración interna.
     */
    axiosConfig?: Partial<AxiosRequestConfig>;
}

/**
 * Valores predeterminados para producción.
 */
export const ProductionDefaults: Required<Pick<SaeClientOptions, 
    'timeout' | 'maxRetries' | 'retryDelay' | 'backoffMultiplier' | 
    'skipCertificateValidation' | 'maxRedirects' | 'followRedirects'>> = {
    timeout: 100000,
    maxRetries: 3,
    retryDelay: 1000,
    backoffMultiplier: 2,
    skipCertificateValidation: false,
    maxRedirects: 5,
    followRedirects: true
};

/**
 * Valores predeterminados para desarrollo.
 * ⚠️ No usar en producción.
 */
export const DevelopmentDefaults: Required<Pick<SaeClientOptions,
    'timeout' | 'maxRetries' | 'retryDelay' | 'backoffMultiplier' | 
    'skipCertificateValidation' | 'maxRedirects' | 'followRedirects'>> = {
    timeout: 30000,
    maxRetries: 1,
    retryDelay: 500,
    backoffMultiplier: 2,
    skipCertificateValidation: true, // ⚠️ Solo para desarrollo
    maxRedirects: 5,
    followRedirects: true
};

/**
 * Excepción específica para errores del cliente SAE.
 */
export class SaeClientError extends Error {
    /**
     * Número de intentos realizados antes de fallar.
     */
    readonly retryCount: number;

    /**
     * Código de error HTTP si aplica.
     */
    readonly statusCode?: number;

    constructor(message: string, retryCount: number = 0, statusCode?: number) {
        super(message);
        this.name = 'SaeClientError';
        this.retryCount = retryCount;
        this.statusCode = statusCode;
    }
}

/**
 * Determina si un error de HTTP es retentable.
 */
export function isRetryableError(error: any): boolean {
    // Errores de red/timeout
    if (error.code === 'ECONNRESET' || 
        error.code === 'ETIMEDOUT' || 
        error.code === 'ECONNREFUSED' ||
        error.code === 'ENOTFOUND' ||
        error.code === 'EAI_AGAIN' ||
        error.code === 'ENETUNREACH') {
        return true;
    }

    // Errores de timeout de axios
    if (error.code === 'ECONNABORTED' && error.message?.includes('timeout')) {
        return true;
    }

    // Errores 5xx
    if (error.response && error.response.status >= 500 && error.response.status < 600) {
        return true;
    }

    // 429 Too Many Requests
    if (error.response && error.response.status === 429) {
        return true;
    }

    // 408 Request Timeout
    if (error.response && error.response.status === 408) {
        return true;
    }

    // 502 Bad Gateway, 503 Service Unavailable, 504 Gateway Timeout
    if (error.response && (error.response.status === 502 || 
                            error.response.status === 503 || 
                            error.response.status === 504)) {
        return true;
    }

    return false;
}

/**
 * Calcula el delay para el siguiente reintento usando backoff exponencial con jitter.
 */
export function calculateRetryDelay(attempt: number, baseDelay: number, multiplier: number): number {
    // Backoff exponencial: delay * (multiplier ^ attempt)
    const exponentialDelay = baseDelay * Math.pow(multiplier, attempt);
    
    // Agregar jitter aleatorio (±25%) para evitar thundering herd
    const jitter = exponentialDelay * 0.25 * (Math.random() * 2 - 1);
    const finalDelay = exponentialDelay + jitter;
    
    return Math.max(finalDelay, 100); // Mínimo 100ms
}
