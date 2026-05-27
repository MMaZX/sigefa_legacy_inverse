using System.Collections.Generic;

namespace SIGEFA.Entidades;

internal class clsCotizacionPropuestaDePedido
{
	private int _codigo;

	private int _codigoPropuesta;

	private int _codigoProveedor;

	private string _descripcionProveedor;

	private string _docCotizacion;

	private int _codigoMoneda;

	private int _tipoPrecio;

	private List<clsDetalleCotizacionPropuestaDePedido> listadoPrecios;

	public int Codigo
	{
		get
		{
			return _codigo;
		}
		set
		{
			_codigo = value;
		}
	}

	public int CodigoPropuesta
	{
		get
		{
			return _codigoPropuesta;
		}
		set
		{
			_codigoPropuesta = value;
		}
	}

	public int CodigoProveedor
	{
		get
		{
			return _codigoProveedor;
		}
		set
		{
			_codigoProveedor = value;
		}
	}

	public string DescripcionProveedor
	{
		get
		{
			return _descripcionProveedor;
		}
		set
		{
			_descripcionProveedor = value;
		}
	}

	public string DocCotizacion
	{
		get
		{
			return _docCotizacion;
		}
		set
		{
			_docCotizacion = value;
		}
	}

	public List<clsDetalleCotizacionPropuestaDePedido> ListadoPrecios
	{
		get
		{
			return listadoPrecios;
		}
		set
		{
			listadoPrecios = value;
		}
	}

	public int CodigoMoneda
	{
		get
		{
			return _codigoMoneda;
		}
		set
		{
			_codigoMoneda = value;
		}
	}

	public int TipoPrecio
	{
		get
		{
			return _tipoPrecio;
		}
		set
		{
			_tipoPrecio = value;
		}
	}
}
