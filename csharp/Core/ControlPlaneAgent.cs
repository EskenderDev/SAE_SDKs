using System;
using System.Threading.Tasks;
using SAE.Sdk.Core;

namespace SAE.Sdk.Core;

public class SaeRuntimeConfig
{
    public bool DisableFacturacion { get; set; }
    public int MaxRetries { get; set; } = 3;
}

public class ControlPlaneAgent
{
    private readonly InternalEventBus _bus;

    public ControlPlaneAgent(InternalEventBus bus)
    {
        _bus = bus;
    }

    public async Task ApplyConfigAsync(SaeRuntimeConfig config)
    {
        // Ejemplo: cambiar comportamiento en runtime
        if (config.DisableFacturacion)
        {
            await _bus.PublishAsync("KillSwitch", "Facturacion");
        }
        
        await _bus.PublishAsync("ConfigUpdated", config);
    }
}
