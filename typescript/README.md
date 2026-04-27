# sae-ts-sdk

[![npm version](https://img.shields.io/npm/v/sae-ts-sdk)](https://www.npmjs.com/package/sae-ts-sdk)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

**TypeScript/JavaScript SDK oficial para la API SAE** — Sistema de Administración de Empresa con Facturación Electrónica de Hacienda Costa Rica.

## Instalación

```bash
npm install sae-ts-sdk
```

## Uso Básico

```typescript
import { SaeClient } from 'sae-ts-sdk';

const client = new SaeClient('https://api.tu-sae.com');

// Login
const result = await client.login('usuario@empresa.com', 'password');

// Emitir factura
const factura = await client.emitir({
  Emisor: { /* ... */ },
  Receptor: { /* ... */ },
  DetalleServicio: { /* ... */ }
});
```

## Autenticación con API Key

Si tu integración es **servidor a servidor** (backend, POS, script), usa el API Key en lugar de usuario/contraseña. El API Key se obtiene desde el Dashboard → Configuración → API Keys.

```typescript
import { SaeClient } from 'sae-ts-sdk';

const client = new SaeClient('https://api.tu-sae.com');

// Autenticación por API Key (header X-API-KEY)
client.setApiKey('sk-live-xxxxxxxxxxxxxxxxxxxxxxxx');

// Desde aquí todas las peticiones van autenticadas automáticamente
const factura = await client.emitir({ /* ... */ });
const docs = await client.buscarDocumentos({ page: 1 });
```

> **Tip**: Para terminales POS también puedes enviar el `X-Terminal-Key`:
> ```typescript
> client.setApiKey('sk-live-xxx');
> client.setTerminalKey('TK-SUCURSAL01-CAJA1');
> const resultado = await client.emitir({ /* ... */ });
> ```

## Módulos Disponibles

| Módulo | Descripción |
|--------|-------------|
| **Auth** | Login, registro, reset de contraseña |
| **Hacienda** | Emitir, consultar, endosar documentos |
| **Tenant Users** | Invitar usuarios, gestionar roles |
| **Branches & Terminals** | Sucursales, terminales, secuencias |
| **License Addons** | Compra/cancelación de paquetes |
| **Reports** | IVA, libro de ventas, compras, CABYS + PDF/Excel |
| **Developer API** | Aplicaciones registradas, módulos |
| **Admin** | Gestión global de plataforma (solo SuperAdmin) |
| **Webhooks** | Suscripción a eventos de Hacienda |

## SignalR (Tiempo Real)

```typescript
import { SaeClient } from 'sae-ts-sdk';

const client = new SaeClient('https://api.tu-sae.com');
await client.login('usuario@empresa.com', 'password');

// Conectar SignalR
const hubUrl = 'https://api.tu-sae.com/hubs/hacienda';
```

> El SDK usa Axios para HTTP. Para SignalR en tiempo real, usa la librería `@microsoft/signalr` directamente con el endpoint `/hubs/hacienda` autenticando con el JWT obtenido en `login()`.

## Variables de Entorno

Para desarrollo local:

```env
PUBLIC_API_URL=https://localhost:55431
```

## Reportes

```typescript
// Reporte IVA D-104 de enero 2025
const reporte = await client.getReport<ReporteIvaDto>('iva', 1, 2025);

// Descargar como PDF
const pdf = await client.downloadReport('iva', 'pdf', 1, 2025);
```

## Configuración Avanzada HTTPS

### Opciones de Cliente

El SDK soporta configuración avanzada de SSL/TLS, timeouts y reintentos automáticos:

```typescript
import { SaeClient, SaeClientOptions, SaeLogLevel } from 'sae-ts-sdk';

// Configuración para desarrollo (certificados autofirmados)
// ⚠️ Solo usar en desarrollo local
const devOptions: SaeClientOptions = {
  ...SaeClient.developmentDefaults(),
  onLog: (event) => console.log(`[${event.level}] ${event.message}`)
};
const devClient = new SaeClient('https://localhost:5001', devOptions);

// Configuración personalizada para producción
const prodOptions: SaeClientOptions = {
  timeout: 60000,           // 60 segundos
  maxRetries: 5,            // Máximo 5 reintentos
  retryDelay: 1000,         // Delay inicial: 1 segundo
  backoffMultiplier: 2,     // Backoff exponencial
  skipCertificateValidation: false, // Validación estricta SSL
  maxRedirects: 5,          // Máximo 5 redirecciones
  
  // Callback de logging para debug
  onLog: (event) => {
    if (event.level === SaeLogLevel.Error) {
      console.error(`[ERROR] ${event.operation}: ${event.message}`);
    }
  }
};

const prodClient = new SaeClient('https://api.produccion.com', prodOptions);
```

### Certificados de Cliente (mTLS) - Solo Node.js

Para autenticación con certificados mutuos:

```typescript
import { readFileSync } from 'fs';

const options: SaeClientOptions = {
  clientCertificate: {
    cert: readFileSync('client-cert.pem', 'utf8'),
    key: readFileSync('client-key.pem', 'utf8'),
    ca: readFileSync('ca-cert.pem', 'utf8') // Opcional
  }
};

const client = new SaeClient('https://api.mutual-tls.com', options);
```

### Certificados Autofirmados (Desarrollo) - Solo Node.js

Para desarrollo local con certificados autofirmados:

```typescript
const options: SaeClientOptions = {
  skipCertificateValidation: true, // ⚠️ Solo en desarrollo
  timeout: 30000,
  maxRetries: 1
};

const client = new SaeClient('https://localhost:5001', options);
```

> ⚠️ **ADVERTENCIA**: Nunca uses `skipCertificateValidation: true` en producción. Esta opción desactiva la validación SSL y expone tu aplicación a ataques man-in-the-middle.

### Reintentos Automáticos (Retry)

El SDK incluye retry automático con backoff exponencial y jitter para errores transitorios:

```typescript
const options: SaeClientOptions = {
  maxRetries: 3,        // Máximo 3 reintentos
  retryDelay: 1000,     // Delay inicial: 1 segundo  
  backoffMultiplier: 2, // Secuencia: 1s, 2s, 4s...
  
  // Se reintenta automáticamente en:
  // - Errores 5xx (500, 502, 503, 504)
  // - 429 Too Many Requests  
  // - 408 Request Timeout
  // - Errores de red (ECONNRESET, ETIMEDOUT, ECONNREFUSED, etc.)
};
```

El algoritmo de backoff incluye **jitter aleatorio** (±25%) para evitar el "thundering herd" cuando un servidor se recupera.

### Agente HTTPS Personalizado (Node.js)

Para control total sobre la configuración SSL:

```typescript
import https from 'https';

const agent = new https.Agent({
  rejectUnauthorized: false, // ⚠️ Solo desarrollo
  cert: readFileSync('cert.pem'),
  key: readFileSync('key.pem'),
  ca: readFileSync('ca.pem'),
  secureProtocol: 'TLSv1_2_method'
});

const options: SaeClientOptions = {
  httpsAgent: agent
};

const client = new SaeClient('https://api.custom-ssl.com', options);
```

## Licencia

MIT © [EskenderDev](https://github.com/EskenderDev)
