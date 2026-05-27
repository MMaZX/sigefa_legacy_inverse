namespace SIGEFA.Entidades;

internal class clsDetalleCotizacionPropuestaDePedido
{
	private int _codigoDetallePropuesta;

	private int _codigoCotizacion;

	private int _codigoProducto;

	private double _precioCompra;

	public int CodigoDetallePropuesta
	{
		get
		{
			return _codigoDetallePropuesta;
		}
		set
		{
			_codigoDetallePropuesta = value;
		}
	}

	public int CodigoCotizacion
	{
		get
		{
			return _codigoCotizacion;
		}
		set
		{
			_codigoCotizacion = value;
		}
	}

	public double PrecioCompra
	{
		get
		{
			return _precioCompra;
		}
		set
		{
			_precioCompra = value;
		}
	}

	public int CodigoProducto
	{
		get
		{
			return _codigoProducto;
		}
		set
		{
			_codigoProducto = value;
		}
	}
}
