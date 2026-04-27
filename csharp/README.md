# SAE.Sdk

[![NuGet version](https://img.shields.io/nuget/v/CR.SAE.SDK)](https://www.nuget.org/packages/CR.SAE.SDK)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](./LICENSE)

SDK C# para interactuar con la API de **Hacienda Costa Rica (SAE System)**. 

Este paquete proporciona un cliente unificado (`SaeClient`) que maneja la comunicación REST y eventos en tiempo real a través de SignalR.

## Características

- ✅ Soporte para **API Key** y **JWT**.
- ✅ Generación y envío de documentos (Facturas, Notas, etc).
- ✅ Consulta de estado de documentos.
- ✅ Notificaciones en tiempo real vía **SignalR**.
- ✅ Compatibilidad con **.NET 8, 9 y 10**.
- ✅ **Soporte Completo Hacienda v4.4** (Exportación, VIN, Medicamentos).
- ✅ **Configuración avanzada SSL/TLS** (certificados, mTLS, timeouts, retry).

## Instalación

```bash
dotnet add package CR.SAE.SDK
```

## Uso Rápido

### 1. Inicializar el Cliente (Recomendado con API Key)

```csharp
using SAE.Sdk.Client;

var client = new SaeClient("https://tuservicio-api.com");
client.SetApiKey("sae_live_tu_api_key");
```

### 2. Enviar un Documento

```csharp
var request = new GenerarDocumentoRequest { ... };
var response = await client.EnviarDocumentoAsync(request);
```

### 3. Ejemplo de Línea con VIN (v4.4)

```csharp
var linea = new LineaDetalleRequest 
{
    // ...
    CodigoCABYS = "1234567890123", // Código de transporte
    NumeroVINoSerie = new List<string> { "VIN123456789" }, // Requerido para transporte
    ImpuestoAsumidoEmisorFabrica = 100 // Nuevo campo v4.4
};
```

### 4. Escuchar Eventos en Tiempo Real

```csharp
await client.StartRealtimeAsync("tu-tenant-id");

client.OnHaciendaNotification += (sender, e) => {
    Console.WriteLine($"Notificación recibida: {e.Clave} - Estado: {e.Estado}");
};
```

## Configuración Avanzada HTTPS

### Opciones de Cliente

El SDK soporta configuración avanzada de SSL/TLS, timeouts y reintentos automáticos:

```csharp
using SAE.Sdk.Client;
using System.Security.Cryptography.X509Certificates;

// Configuración para desarrollo (certificados autofirmados)
var devClient = new SaeClient("https://localhost:5001", options =>
{
    options.SkipCertificateValidation = true; // ⚠️ Solo desarrollo
    options.Timeout = TimeSpan.FromSeconds(30);
    options.MaxRetries = 1;
});

// Configuración personalizada para producción
var prodClient = new SaeClient("https://api.produccion.com", options =>
{
    options.Timeout = TimeSpan.FromSeconds(60);
    options.MaxRetries = 5;
    options.RetryDelay = TimeSpan.FromMilliseconds(500);
    options.BackoffMultiplier = 2.0;
    options.SkipCertificateValidation = false; // Validación estricta en producción
    
    // Callback de logging
    options.LogCallback = (logEvent) => 
    {
        Console.WriteLine($"[{logEvent.Level}] {logEvent.Operation}: {logEvent.Message}");
    };
});
```

### Certificados de Cliente (mTLS)

Para autenticación con certificados mutuos:

```csharp
var clientCert = new X509Certificate2("certificado.p12", "password");

var client = new SaeClient("https://api.mutual-tls.com", options =>
{
    options.ClientCertificate = clientCert;
    options.CustomRootCertificates = new X509Certificate2Collection 
    { 
        new X509Certificate2("ca-raiz.pem") 
    };
});
```

### Reintentos Automáticos (Retry)

El SDK incluye retry automático con backoff exponencial para errores transitorios:

```csharp
var client = new SaeClient("https://api.produccion.com", options =>
{
    options.MaxRetries = 3;                    // Máximo 3 reintentos
    options.RetryDelay = TimeSpan.FromSeconds(1); // Delay inicial: 1s
    options.BackoffMultiplier = 2.0;             // Multiplicador: 1s, 2s, 4s...
    
    // Se reintenta automáticamente en:
    // - Errores 5xx (500, 502, 503, 504)
    // - 429 Too Many Requests
    // - 408 Request Timeout
    // - Errores de red (timeout, conexión reset, etc.)
});
```

### Validación Personalizada de Certificados

```csharp
var client = new SaeClient("https://api.custom-ssl.com", options =>
{
    // Validación personalizada por thumbprint
    options.CustomCertificateValidation = (cert, chain, errors) =>
    {
        var expectedThumbprint = "A1B2C3D4E5F6...";
        return cert.Thumbprint.Equals(expectedThumbprint, StringComparison.OrdinalIgnoreCase);
    };
});
```

## Licencia
Este proyecto está bajo la licencia MIT.
