using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

Console.WriteLine("=== SAE RUNTIME GUARD: Risk Simulation ===");

var baseUrl = "https://localhost:5001/api"; // URL de la API local
var handler = new HttpClientHandler { 
    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true 
};
var client = new HttpClient(handler);

// Headers para identificar el dispositivo y el monto
var deviceId = $"SIM-DEVICE-{Guid.NewGuid().ToString().Substring(0, 8)}";
client.DefaultRequestHeaders.Add("X-Device-Id", deviceId);
client.DefaultRequestHeaders.Add("X-Source", "pos-simulator");

Console.WriteLine($"\n[1] TEST: High Amount Detection (Monto > 100,000)");
var requestHigh = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/tenants/current");
requestHigh.Headers.Add("X-Amount", "150000");
var responseHigh = await client.SendAsync(requestHigh);
Console.WriteLine($"Status: {responseHigh.StatusCode} (Esperado: OK con Alerta en Dashboard)");

Console.WriteLine($"\n[2] TEST: Velocity Detection (Throttling & Blocking)");
Console.WriteLine("Enviando 10 peticiones rápidas...");

for (int i = 1; i <= 10; i++)
{
    var sw = System.Diagnostics.Stopwatch.StartNew();
    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/tenants/current");
    request.Headers.Add("X-Amount", "100");
    
    var response = await client.SendAsync(request);
    sw.Stop();

    string action = "Allow";
    if (sw.ElapsedMilliseconds > 1500) action = "THROTTLED";
    if (response.StatusCode == HttpStatusCode.Forbidden) action = "BLOCKED";

    Console.WriteLine($"Req {i}: {response.StatusCode} | Time: {sw.ElapsedMilliseconds}ms | Action: {action}");

    if (action == "BLOCKED") 
    {
        Console.WriteLine("\n✅ SUCCESS: RuntimeGuard blocked the suspicious activity!");
        break;
    }
}

Console.WriteLine("\n=== Simulation Finished ===");
