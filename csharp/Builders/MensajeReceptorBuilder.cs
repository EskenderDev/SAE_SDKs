using SAE.Sdk.Models;

namespace SAE.Sdk.Builders;

public class MensajeReceptorBuilder
{
    private string _clave = string.Empty;
    private string _numeroCedulaEmisor = string.Empty;
    private DateTime _fechaEmisionDoc = DateTime.UtcNow;
    private string _detalleMensaje = string.Empty;
    private decimal _montoTotalImpuesto = 0;
    private decimal _totalFactura = 0;
    private string _numeroCedulaReceptor = string.Empty;
    private string _numeroConsecutivoReceptor = string.Empty;
    private int _mensaje = 1; // Default to Accepted
    private string? _nombreReceptor;
    private string? _tipoIdentificacionReceptor;

    public static MensajeReceptorBuilder Nuevo() => new();

    public MensajeReceptorBuilder ConClave(string clave)
    {
        _clave = clave;
        return this;
    }

    public MensajeReceptorBuilder ConEmisor(string cedula)
    {
        _numeroCedulaEmisor = cedula;
        return this;
    }

    public MensajeReceptorBuilder ConFechaDocumento(DateTime fecha)
    {
        _fechaEmisionDoc = fecha;
        return this;
    }

    public MensajeReceptorBuilder ConDetalle(string detalle)
    {
        _detalleMensaje = detalle;
        return this;
    }

    public MensajeReceptorBuilder ConImpuestos(decimal monto)
    {
        _montoTotalImpuesto = monto;
        return this;
    }

    public MensajeReceptorBuilder ConTotal(decimal total)
    {
        _totalFactura = total;
        return this;
    }

    public MensajeReceptorBuilder ConReceptor(string cedula, string? nombre = null, string? tipoIdentificacion = null)
    {
        _numeroCedulaReceptor = cedula;
        _nombreReceptor = nombre;
        _tipoIdentificacionReceptor = tipoIdentificacion;
        return this;
    }

    public MensajeReceptorBuilder ConConsecutivoReceptor(string consecutivo)
    {
        _numeroConsecutivoReceptor = consecutivo;
        return this;
    }

    public MensajeReceptorBuilder ComoAceptacionTotal()
    {
        _mensaje = 1;
        if (string.IsNullOrEmpty(_detalleMensaje)) _detalleMensaje = "Factura aceptada en su totalidad";
        return this;
    }

    public MensajeReceptorBuilder ComoAceptacionParcial()
    {
        _mensaje = 2;
        return this;
    }

    public MensajeReceptorBuilder ComoRechazo()
    {
        _mensaje = 3;
        return this;
    }

    public MensajeReceptorBuilder ComoEndoso()
    {
        _mensaje = 4;
        if (string.IsNullOrEmpty(_detalleMensaje)) _detalleMensaje = "Aceptación por cesión de factura (endoso)";
        return this;
    }

    public MensajeEndosoRequest Build()
    {
        Validate();
        return new MensajeEndosoRequest(
            Clave: _clave,
            NumeroCedulaEmisor: _numeroCedulaEmisor,
            FechaEmisionDoc: _fechaEmisionDoc,
            DetalleMensaje: _detalleMensaje,
            MontoTotalImpuesto: _montoTotalImpuesto,
            TotalFactura: _totalFactura,
            NumeroCedulaReceptor: _numeroCedulaReceptor,
            NumeroConsecutivoReceptor: _numeroConsecutivoReceptor,
            Mensaje: _mensaje,
            NombreReceptor: _nombreReceptor,
            TipoIdentificacionReceptor: _tipoIdentificacionReceptor);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(_clave) || _clave.Length != 50)
            throw new InvalidOperationException("La Clave es requerida y debe tener 50 dígitos.");

        if (string.IsNullOrWhiteSpace(_numeroCedulaEmisor))
            throw new InvalidOperationException("La Cédula del Emisor es requerida.");

        if (string.IsNullOrWhiteSpace(_numeroCedulaReceptor))
            throw new InvalidOperationException("La Cédula del Receptor es requerida.");

        if (string.IsNullOrWhiteSpace(_numeroConsecutivoReceptor))
            throw new InvalidOperationException("El Consecutivo del Receptor es requerido.");

        if (string.IsNullOrWhiteSpace(_detalleMensaje))
            throw new InvalidOperationException("El Detalle del Mensaje es requerido.");
    }
}
