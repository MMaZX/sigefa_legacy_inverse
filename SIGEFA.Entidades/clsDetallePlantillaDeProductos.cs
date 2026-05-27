namespace SIGEFA.Entidades;

public class clsDetallePlantillaDeProductos
{
	private int _codigo;

	private int _cod_Plantilla;

	private int _opcionRecuentoCantidad;

	private int _codigo_Producto;

	private string _ref_Producto;

	private string _descrip_Producto;

	private int _codigo_Unidad;

	private string _descripcion_Unidad;

	private double _cantidad;

	private string _marca;

	private string _famiilia;

	private string _linea;

	private string _grupo;

	private double _stockActual;

	private double _stockMinimo;

	private double _stockMaximo;

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

	public int Cod_Plantilla
	{
		get
		{
			return _cod_Plantilla;
		}
		set
		{
			_cod_Plantilla = value;
		}
	}

	public int Codigo_Producto
	{
		get
		{
			return _codigo_Producto;
		}
		set
		{
			_codigo_Producto = value;
		}
	}

	public string Ref_Producto
	{
		get
		{
			return _ref_Producto;
		}
		set
		{
			_ref_Producto = value;
		}
	}

	public string Descrip_Producto
	{
		get
		{
			return _descrip_Producto;
		}
		set
		{
			_descrip_Producto = value;
		}
	}

	public int Codigo_Unidad
	{
		get
		{
			return _codigo_Unidad;
		}
		set
		{
			_codigo_Unidad = value;
		}
	}

	public string Descripcion_Unidad
	{
		get
		{
			return _descripcion_Unidad;
		}
		set
		{
			_descripcion_Unidad = value;
		}
	}

	public double Cantidad
	{
		get
		{
			return _cantidad;
		}
		set
		{
			_cantidad = value;
		}
	}

	public string Marca
	{
		get
		{
			return _marca;
		}
		set
		{
			_marca = value;
		}
	}

	public string Famiilia
	{
		get
		{
			return _famiilia;
		}
		set
		{
			_famiilia = value;
		}
	}

	public string Linea
	{
		get
		{
			return _linea;
		}
		set
		{
			_linea = value;
		}
	}

	public string Grupo
	{
		get
		{
			return _grupo;
		}
		set
		{
			_grupo = value;
		}
	}

	public double StockActual
	{
		get
		{
			return _stockActual;
		}
		set
		{
			_stockActual = value;
		}
	}

	public double StockMinimo
	{
		get
		{
			return _stockMinimo;
		}
		set
		{
			_stockMinimo = value;
		}
	}

	public double StockMaximo
	{
		get
		{
			return _stockMaximo;
		}
		set
		{
			_stockMaximo = value;
		}
	}

	public int OpcionRecuentoCantidad
	{
		get
		{
			return _opcionRecuentoCantidad;
		}
		set
		{
			_opcionRecuentoCantidad = value;
		}
	}
}
