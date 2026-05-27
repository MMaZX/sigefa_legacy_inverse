using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsPlantillaDeProductos
{
	private int _codigo;

	private string _nombre;

	private string _descripcion;

	private DateTime _fecharegitro;

	private DateTime _fechaedicion;

	private int _cod_almacen;

	private string _descrip_almacen;

	private int _cod_usuario;

	private string _nombre_usuario;

	private int _tipo;

	private int _estado;

	private List<clsDetallePlantillaDeProductos> lDetalle;

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

	public string Nombre
	{
		get
		{
			return _nombre;
		}
		set
		{
			_nombre = value;
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

	public DateTime FechaRegistro
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

	public DateTime FechaEdicion
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

	public List<clsDetallePlantillaDeProductos> LDetalle
	{
		get
		{
			return lDetalle;
		}
		set
		{
			lDetalle = value;
		}
	}
}
