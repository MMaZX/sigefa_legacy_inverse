using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

internal class clsPropuestaDePedido
{
	private int _codigo;

	private string _titulo;

	private string _descripcion;

	private DateTime _fecharegitro;

	private DateTime _fechaedicion;

	private DateTime _fechageneracion;

	private int _cod_almacen;

	private string _descrip_almacen;

	private int _cod_usuario;

	private string _nombre_usuario;

	private int _tipo;

	private int _estado = 1;

	private List<clsDetallePropuestaDePedido> lDetalle_Propuesta_De_Pedido;

	private List<clsCotizacionPropuestaDePedido> lCotizaciones;

	private List<clsAlmacenPropuestaDePedido> lAlmacenes;

	private int _eliminado;

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

	public string Titulo
	{
		get
		{
			return _titulo;
		}
		set
		{
			_titulo = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return _descripcion;
		}
		set
		{
			_descripcion = value;
		}
	}

	public DateTime Fecharegitro
	{
		get
		{
			return _fecharegitro;
		}
		set
		{
			_fecharegitro = value;
		}
	}

	public DateTime Fechaedicion
	{
		get
		{
			return _fechaedicion;
		}
		set
		{
			_fechaedicion = value;
		}
	}

	public DateTime Fechageneracion
	{
		get
		{
			return _fechageneracion;
		}
		set
		{
			_fechageneracion = value;
		}
	}

	public int Cod_almacen
	{
		get
		{
			return _cod_almacen;
		}
		set
		{
			_cod_almacen = value;
		}
	}

	public string Descrip_almacen
	{
		get
		{
			return _descrip_almacen;
		}
		set
		{
			_descrip_almacen = value;
		}
	}

	public int Cod_usuario
	{
		get
		{
			return _cod_usuario;
		}
		set
		{
			_cod_usuario = value;
		}
	}

	public string Nombre_usuario
	{
		get
		{
			return _nombre_usuario;
		}
		set
		{
			_nombre_usuario = value;
		}
	}

	public int Tipo
	{
		get
		{
			return _tipo;
		}
		set
		{
			_tipo = value;
		}
	}

	public int Estado
	{
		get
		{
			return _estado;
		}
		set
		{
			_estado = value;
		}
	}

	public List<clsDetallePropuestaDePedido> LDetalle
	{
		get
		{
			return lDetalle_Propuesta_De_Pedido;
		}
		set
		{
			lDetalle_Propuesta_De_Pedido = value;
		}
	}

	public int Eliminado
	{
		get
		{
			return _eliminado;
		}
		set
		{
			_eliminado = value;
		}
	}

	public int CodPlantillaGenerada { get; internal set; }

	internal List<clsCotizacionPropuestaDePedido> LCotizaciones
	{
		get
		{
			return lCotizaciones;
		}
		set
		{
			lCotizaciones = value;
		}
	}

	internal List<clsAlmacenPropuestaDePedido> LAlmacenes
	{
		get
		{
			return lAlmacenes;
		}
		set
		{
			lAlmacenes = value;
		}
	}
}
