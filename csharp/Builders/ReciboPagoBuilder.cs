using SAE.Sdk.Models;

namespace SAE.Sdk.Builders;

public class ReciboPagoBuilder
{
    private string _tipoDocumento = TipoComprobanteExtension.ReciboElectronicoPago;
    private int _codigoPais = 506;
    private int _establecimiento = 1;
    private int _terminal = 1;
    private long _consecutivo = 1;
    private string _situacion = "1";
    private string _cedulaEmisor = string.Empty;
    private DateTime _fechaEmision = DateTime.Now;

    private string _emisorNombre = string.Empty;
    private string _emisorIdentificacionTipo = TipoIdentificacionExtension.CedulaJuridica;
    private string _emisorIdentificacionNumero = string.Empty;
    private string _emisorCorreo = string.Empty;

    private string _receptorNombre = string.Empty;
    private string _receptorIdentificacionTipo = TipoIdentificacionExtension.CedulaFisica;
    private string _receptorIdentificacionNumero = string.Empty;
    private string _receptorCorreo = string.Empty;

    private List<LineaDetalleRequest> _lineas = new();
    private List<(string Descripcion, string Identificador, string Otros)> _informacionPago = new();

    public ReciboPagoBuilder ConEmisor(string nombre, string numeroIdentificacion, string correo, string tipo = TipoIdentificacionExtension.CedulaJuridica)
    {
        _emisorNombre = nombre;
        _emisorIdentificacionNumero = numeroIdentificacion;
        _emisorIdentificacionTipo = tipo;
        _emisorCorreo = correo;
        _cedulaEmisor = numeroIdentificacion;
        return this;
    }

    public ReciboPagoBuilder ConReceptor(string nombre, string numeroIdentificacion, string correo, string tipo = TipoIdentificacionExtension.CedulaFisica)
    {
        _receptorNombre = nombre;
        _receptorIdentificacionNumero = numeroIdentificacion;
        _receptorIdentificacionTipo = tipo;
        _receptorCorreo = correo;
        return this;
    }

    public ReciboPagoBuilder ConConsecutivo(int establecimiento, int terminal, long numero)
    {
        _establecimiento = establecimiento;
        _terminal = terminal;
        _consecutivo = numero;
        return this;
    }

    public ReciboPagoBuilder AgregarLinea(string detalle, decimal cantidad, decimal precio, string codigoCabys, string unidadMedida = "Unid")
    {
        var linea = new LineaDetalleRequest
        {
            NumeroLinea = _lineas.Count + 1,
            Detalle = detalle,
            Cantidad = cantidad,
            PrecioUnitario = precio,
            CodigoCABYS = codigoCabys,
            UnidadMedida = unidadMedida,
            TipoTransaccion = "01"
        };
        _lineas.Add(linea);
        return this;
    }

    public ReciboPagoBuilder AgregarInformacionPago(string descripcion, string identificador, string otros)
    {
        _informacionPago.Add((descripcion, identificador, otros));
        return this;
    }

    public GenerarDocumentoRequest Build()
    {
        var consecutivoReq = new ConsecutivoRequest(_establecimiento, _terminal, _consecutivo, _tipoDocumento);

        var claveReq = new ClaveRequest(
            _codigoPais,
            _fechaEmision.Day,
            _fechaEmision.Month,
            int.Parse(_fechaEmision.ToString("yy")),
            consecutivoReq,
            _cedulaEmisor,
            _situacion);

        var emisor = new EmisorRequest
        {
            Identificacion = new IdentificacionRequest(_emisorIdentificacionTipo, _emisorIdentificacionNumero),
            Nombre = _emisorNombre,
            NombreComercial = null,
            Ubicacion = new UbicacionRequest
            {
                Provincia = "1",
                Canton = "01",
                Distrito = "01",
                Barrio = "01",
                OtrasSenas = "Sin señas"
            },
            Telefono = new TelefonoRequest(506, 0),
            CorreoElectronico = new List<string> { _emisorCorreo }
        };

        var receptor = new ReceptorRequest
        {
            Identificacion = new IdentificacionRequest(_receptorIdentificacionTipo, _receptorIdentificacionNumero),
            Nombre = _receptorNombre,
            NombreComercial = null,
            Ubicacion = new UbicacionRequest
            {
                Provincia = "1",
                Canton = "01",
                Distrito = "01",
                Barrio = "01",
                OtrasSenas = "Sin señas"
            },
            Telefono = new TelefonoRequest(506, 0),
            CorreoElectronico = _receptorCorreo
        };

        return new GenerarDocumentoRequest
        {
            TipoDocumento = _tipoDocumento,
            Clave = claveReq,
            Consecutivo = consecutivoReq,
            CodigoActividadEmisor = "960900", // Default activity
            CodigoActividadReceptor = "960900",
            FechaEmision = _fechaEmision,
            ProveedorSistemas = "SAE_SYSTEM_SDK",
            Emisor = emisor,
            Receptor = receptor,
            CondicionVenta = CondicionVentaExtension.Contado,
            CondicionVentaOtros = null,
            PlazoCredito = null,
            DetalleServicio = new DetalleServicioRequest { LineasDetalle = _lineas },
            ResumenFactura = new ResumenFacturaRequest(new CodigoTipoMonedaRequest(CodigoMonedaExtension.CRC, 1), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 0, 0, 0, 0, null, 0),
            InformacionReferencia = null,
            OtrosCargos = null,
            OtrosDatos = GenerarOtrosDatos()
        };
    }

    private OtrosRequest? GenerarOtrosDatos()
    {
        if (_informacionPago.Count == 0) return null;

        var xml = new System.Text.StringBuilder();
        xml.Append("<InformacionPago>");
        foreach (var info in _informacionPago)
        {
            xml.Append("<Metodo>");
            xml.Append($"<Descripcion>{info.Descripcion}</Descripcion>");
            xml.Append($"<Identificador>{info.Identificador}</Identificador>");
            xml.Append($"<Otros>{info.Otros}</Otros>");
            xml.Append("</Metodo>");
        }
        xml.Append("</InformacionPago>");

        return new OtrosRequest(null, xml.ToString());
    }
}
