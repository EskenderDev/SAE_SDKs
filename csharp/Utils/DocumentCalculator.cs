using SAE.Sdk.Models;
using System.Collections.Generic;
using System.Linq;

namespace SAE.Sdk.Utils;

/// <summary>
/// Utility for accurately calculating invoice totals based on Hacienda CR rules.
/// Emulates the internal logic of SAE.GENERATE to ensure the client-side totals 
/// match the backend exactly before submitting.
/// 
/// Uses 5-decimal precision logic as expected by Hacienda.
/// </summary>
public static class DocumentCalculator
{
    /// <summary>
    /// Rounds a number to a specified number of decimal places (default 5 for Hacienda)
    /// using standard half-up rounding (AwayFromZero) to emulate Hacienda behavior.
    /// </summary>
    public static decimal RoundHacienda(decimal value, int decimals = 5)
    {
        return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
    }

    /// <summary>
    /// Calculates missing totals for a single line item, exactly as SAE.GENERATE does.
    /// Mutates the input request with calculated values.
    /// </summary>
    public static LineaDetalleRequest CalculateLineItem(LineaDetalleRequest linea)
    {
        // 1. Calculate base amounts if not provided
        if (!linea.MontoTotal.HasValue || linea.MontoTotal.Value == 0)
        {
            linea.MontoTotal = RoundHacienda(linea.Cantidad * linea.PrecioUnitario);
        }

        decimal totalDescuentos = 0;
        if (linea.Descuentos != null && linea.Descuentos.Any())
        {
            totalDescuentos = linea.Descuentos.Sum(d => d.MontoDescuento ?? 0m);
        }

        // 2. SubTotal
        if (!linea.SubTotal.HasValue || linea.SubTotal.Value == 0)
        {
            linea.SubTotal = RoundHacienda(linea.MontoTotal.Value - totalDescuentos);
        }

        // 3. Impuestos
        decimal totalImpuesto = 0;
        decimal totalExoneracion = 0;

        if (linea.Impuestos != null && linea.Impuestos.Any())
        {
            for (int idx = 0; idx < linea.Impuestos.Count; idx++)
            {
                var i = linea.Impuestos[idx];
                
                // Auto calculate tax amount if missing based on subtotal
                if (!i.Monto.HasValue || i.Monto.Value == 0)
                {
                    i = i with { Monto = RoundHacienda(linea.SubTotal.Value * (i.Tarifa ?? 0m) / 100m) };
                    linea.Impuestos[idx] = i;
                }
                
                totalImpuesto += i.Monto.Value;

                if (i.Exoneracion != null)
                {
                    totalExoneracion += i.Exoneracion.MontoExoneracion;
                }
            }
        }

        // 4. Impuesto Neto
        decimal impuestoAsumido = linea.ImpuestoAsumidoEmisorFabrica ?? 0m;
        
        if (!linea.ImpuestoNeto.HasValue || linea.ImpuestoNeto.Value == 0)
        {
            linea.ImpuestoNeto = RoundHacienda(totalImpuesto - totalExoneracion - impuestoAsumido);
        }

        // 5. Monto Total Linea
        if (!linea.MontoTotalLinea.HasValue || linea.MontoTotalLinea.Value == 0)
        {
            linea.MontoTotalLinea = RoundHacienda(linea.SubTotal.Value + linea.ImpuestoNeto.Value);
        }

        return linea;
    }

    /// <summary>
    /// Processes a list of line items and calculates all missing totals for each.
    /// </summary>
    public static List<LineaDetalleRequest> CalculateLines(List<LineaDetalleRequest> lineas)
    {
        if (lineas == null) return new List<LineaDetalleRequest>();
        
        foreach (var linea in lineas)
        {
            CalculateLineItem(linea);
        }
        
        return lineas;
    }
}
