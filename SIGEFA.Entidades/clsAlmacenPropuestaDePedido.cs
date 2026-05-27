using System.Collections.Generic;

namespace SIGEFA.Entidades;

internal class clsAlmacenPropuestaDePedido
{
	private int _codigo;

	private int _codigoPropuesta;

	private int _codigoAlmacen;

	private string _descripcionAlmacen;

	private List<clsDetalleAlmacenPropuestaDePedido> listadoSeleccionados;

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

	public int CodigoAlmacen
	{
		get
		{
			return _codigoAlmacen;
		}
		set
		{
			_codigoAlmacen = value;
		}
	}

	public string DescripcionAlmacen
	{
		get
		{
			return _descripcionAlmacen;
		}
		set
		{
			_descripcionAlmacen = value;
		}
	}

	public List<clsDetalleAlmacenPropuestaDePedido> ListadoSeleccionados
	{
		get
		{
			return listadoSeleccionados;
		}
		set
		{
			listadoSeleccionados = value;
		}
	}
}
