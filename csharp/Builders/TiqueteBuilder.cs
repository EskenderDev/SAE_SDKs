using SAE.Sdk.Models;

namespace SAE.Sdk.Builders;

public class TiqueteBuilder
{
    private string _tipoDocumento = TipoComprobanteExtension.TiqueteElectronico;
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

    private string? _receptorNombre;
    private string? _receptorIdentificacionTipo;
    private string? _receptorIdentificacionNumero;
    private string? _receptorCorreo;

    private List<LineaDetalleRequest> _lineas = new();
    private List<(string Descripcion, string Identificador, string Otros)> _informacionPago = new();

    public TiqueteBuilder ConEmisor(string nombre, string numeroIdentificacion, string correo, string tipo = TipoIdentificacionExtension.CedulaJuridica)
    {
        _emisorNombre = nombre;
        _emisorIdentificacionNumero = numeroIdentificacion;
        _emisorIdentificacionTipo = tipo;
        _emisorCorreo = correo;
        _cedulaEmisor = numeroIdentificacion;
        return this;
    }

    public TiqueteBuilder ConReceptor(string nombre, string numeroIdentificacion, string? correo = null, string tipo = TipoIdentificacionExtension.CedulaFisica)
    {
        _receptorNombre = nombre;
        _receptorIdentificacionNumero = numeroIdentificacion;
        _receptorIdentificacionTipo = tipo;
        _receptorCorreo = correo;
        return this;
    }

    public TiqueteBuilder ConConsecutivo(int establecimiento, int terminal, long numero)
    {
        _establecimiento = establecimiento;
        _terminal = terminal;
        _consecutivo = numero;
        return this;
    }

    public TiqueteBuilder AgregarLinea(string detalle, decimal cantidad, decimal precio, string codigoCabys, string unidadMedida = "Unid")
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

    public TiqueteBuilder ConImpuesto(string codigo, string tarifaIva, decimal tarifa)
    {
        if (_lineas.Count == 0) throw new InvalidOperationException("Debe agregar una línea antes de asignar un impuesto.");

        var ultimaLinea = _lineas[^1];
        ultimaLinea.Impuestos.Add(new ImpuestoRequest(
            Codigo: codigo,
            CodigoImpuestoOTRO: null,
            CodigoTarifaIVA: tarifaIva,
            Tarifa: tarifa,
            FactorCalculoIVA: null,
            Monto: null,
            Exoneracion: null
        ));

        return this;
    }

    public TiqueteBuilder AgregarInformacionPago(string descripcion, string identificador, string otros)
    {
        _informacionPago.Add((descripcion, identificador, otros));
        return this;
    }

    public GenerarDocumentoRequest Build()
    {
        // Calcular Totales
        decimal totalServGravados = 0;
        decimal totalMercGravadas = 0;
        decimal totalImpuesto = 0;
        decimal totalVenta = 0;

        foreach (var linea in _lineas)
        {
            var montoTotalLinea = linea.Cantidad * linea.PrecioUnitario;
            totalVenta += montoTotalLinea;

            decimal impuestoLinea = 0;
            foreach (var imp in linea.Impuestos)
            {
                if (imp.Tarifa.HasValue)
                {
                    impuestoLinea += montoTotalLinea * (imp.Tarifa.Value / 100);
                }
            }
            totalImpuesto += impuestoLinea;

            if (impuestoLinea > 0)
            {
                if (linea.UnidadMedida == "Unid" || linea.UnidadMedida == "kg" || linea.UnidadMedida == "m")
                    totalMercGravadas += montoTotalLinea;
                else
                    totalServGravados += montoTotalLinea;
            }
        }

        var totalComprobante = totalVenta + totalImpuesto;

        var consecutivoReq = new ConsecutivoRequest(_establecimiento, _terminal, _consecutivo);

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

        ReceptorRequest? receptor = null;
        if (!string.IsNullOrEmpty(_receptorNombre))
        {
            receptor = new ReceptorRequest
            {
                Identificacion = new IdentificacionRequest(_receptorIdentificacionTipo ?? TipoIdentificacionExtension.CedulaFisica, _receptorIdentificacionNumero ?? ""),
                Nombre = _receptorNombre,
                NombreComercial = null,
                Ubicacion = null,
                Telefono = new TelefonoRequest(506, 0),
                CorreoElectronico = _receptorCorreo ?? string.Empty
            };
        }

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
            Receptor = receptor ?? new ReceptorRequest
            {
                Identificacion = new IdentificacionRequest(TipoIdentificacionExtension.CedulaFisica, "000000000"),
                Nombre = "CLIENTE CONTADO",
                CorreoElectronico = string.Empty
            },
            CondicionVenta = CondicionVentaExtension.Contado,
            CondicionVentaOtros = null,
            PlazoCredito = null,
            DetalleServicio = new DetalleServicioRequest { LineasDetalle = _lineas },
            ResumenFactura = new ResumenFacturaRequest(
                new CodigoTipoMonedaRequest(CodigoMonedaExtension.CRC, 1),
                totalServGravados, 0, 0, 0,
                totalMercGravadas, 0, 0, 0,
                totalServGravados + totalMercGravadas, 0, 0, 0,
                totalVenta, 0, totalVenta,
                null,
                totalImpuesto, 0, 0, 0,
                null,
                totalComprobante),
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
