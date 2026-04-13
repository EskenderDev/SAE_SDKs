using SAE.Sdk.Models;

namespace SAE.Sdk.Utils;

/// <summary>
/// Clase de utilidad para facilitar la construcción de documentos electrónicos.
/// </summary>
public static class DocumentoHelper
{
    /// <summary>
    /// Crea una línea de detalle estándar.
    /// </summary>
    public static LineaDetalleRequest CrearLinea(
        string detalle, 
        decimal precioUnitario, 
        decimal cantidad = 1, 
        string unidadMedida = "Unid",
        string? codigoCabys = null)
    {
        return new LineaDetalleRequest
        {
            Detalle = detalle,
            PrecioUnitario = precioUnitario,
            Cantidad = cantidad,
            UnidadMedida = unidadMedida,
            CodigoCABYS = codigoCabys
        };
    }

    /// <summary>
    /// Crea un receptor rápido para Factura Electrónica.
    /// </summary>
    public static ReceptorRequest CrearReceptor(
        string nombre, 
        string identificacion, 
        string correo, 
        string tipoIdentificacion = "01")
    {
        return new ReceptorRequest
        {
            Nombre = nombre,
            Identificacion = new IdentificacionRequest(tipoIdentificacion, identificacion),
            CorreoElectronico = correo
        };
    }

    /// <summary>
    /// Agrega un impuesto de IVA estándar (13%) a una línea.
    /// </summary>
    public static LineaDetalleRequest ConIva(this LineaDetalleRequest linea, string codigoTarifa = "08", decimal tarifa = 13)
    {
        linea.Impuestos.Add(new ImpuestoRequest(
            Codigo: "01", // IVA
            CodigoImpuestoOTRO: null,
            CodigoTarifaIVA: codigoTarifa,
            Tarifa: tarifa,
            FactorCalculoIVA: null,
            Monto: null, // El servidor lo calcula si no se envía
            Exoneracion: null
        ));
        return linea;
    }
}
