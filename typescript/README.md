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

## Licencia

MIT © [EskenderDev](https://github.com/EskenderDev)
