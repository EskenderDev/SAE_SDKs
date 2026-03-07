# SAE SDKs — Monorepo

Este repositorio contiene todos los SDKs oficiales de la plataforma **SAE (Sistema de Administración de Empresa)** para integradores y desarrolladores.

## Paquetes

| SDK | Directorio | Package | Runtimes |
|-----|-----------|---------|----------|
| **C# / .NET** | [`csharp/`](./csharp/) | [`CR.SAE.SDK` (NuGet)](https://www.nuget.org/packages/CR.SAE.SDK) | net8 · net9 · net10 |
| **TypeScript / Node** | [`typescript/`](./typescript/) | [`sae-ts-sdk` (npm)](https://www.npmjs.com/package/sae-ts-sdk) | ESM · CJS |
| **Android / Kotlin** | [`android/`](./android/) | AAR import | Android API 21+ |

## Instalación Rápida

### C# / .NET
```bash
dotnet add package CR.SAE.SDK
```

### TypeScript / Node
```bash
npm install sae-ts-sdk
```

### Android (Gradle)
Agrega el módulo `android/` como dependencia local en tu proyecto Android.

## Arquitectura

Todos los SDKs consumen la misma API REST de SAE en:
```
https://api.sae.com   (producción)
https://localhost:55431  (desarrollo local)
```

El `TenantId` se deriva exclusivamente del JWT que emite la API. Los SDKs **nunca envían** el `tenantId` desde el cliente.

## Desarrollo Local

### TypeScript SDK
```bash
cd typescript
npm install
npm run build
npm link            # Para usarlo localmente en SAE_DASHBOARD
```

### C# SDK
```bash
cd csharp
dotnet build
dotnet pack         # Genera el .nupkg local
```

## Últimos Cambios (v0.3.1 / v1.1.0)

- **Licencias**: Se añadieron los campos `AppIdentifier` y `AppName` a la respuesta detallada de licencias en todos los SDKs.
- **Trazabilidad**: Mejora en la identificación de la aplicación de origen para activaciones de addons.
- **Correcciones**: Sincronización de modelos de respuesta entre C#, Kotlin y TypeScript.

## Contribuir

Los SDKs se versionan de forma independiente. Ubica el changelog en cada subdirectorio.
