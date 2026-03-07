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

### 3. Escuchar Eventos en Tiempo Real

```csharp
await client.StartRealtimeAsync("tu-tenant-id");

client.OnHaciendaNotification += (sender, e) => {
    Console.WriteLine($"Notificación recibida: {e.Clave} - Estado: {e.Estado}");
};
```

## Licencia
Este proyecto está bajo la licencia MIT.
