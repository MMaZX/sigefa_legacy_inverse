namespace SIGEFA.Entidades;

public class clsDetallePropuestaDePedido
{
	private int _codigo;

	private int _cod_Propuesta;

	private int _codigo_Producto;

	private string _ref_Producto;

	private string _descrip_Producto;

	private int _codigo_Unidad;

	private string _descripcion_Unidad;

	private double _cantidad_reponer;

	private double _cantidad_sugerida;

	private double _precio_unit_actual;

	private double _pedido_final;

	private double _stockDisponible;

	private int _cod_cotizacion_seleccionada;

	private int _cod_proveedor_seleccionado;

	private int _opcionRecuento;

	private double _stockMinimo;

	private double _stockMaximo;

	private double _unidadXPaquete;

	private int _nroItem;

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

	public int Cod_Propuesta
	{
		get
		{
			return _cod_Propuesta;
		}
		set
		{
			_cod_Propuesta = value;
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

	public double Cantidad_reponer
	{
		get
		{
			return _cantidad_reponer;
		}
		set
		{
			_cantidad_reponer = value;
		}
	}

	public double Cantidad_sugerida
	{
		get
		{
			return _cantidad_sugerida;
		}
		set
		{
			_cantidad_sugerida = value;
		}
	}

	public double Precio_unit_actual
	{
		get
		{
			return _precio_unit_actual;
		}
		set
		{
			_precio_unit_actual = value;
		}
	}

	public double Pedido_final
	{
		get
		{
			return _pedido_final;
		}
		set
		{
			_pedido_final = value;
		}
	}

	public int Cod_cotizacion_seleccionada
	{
		get
		{
			return _cod_cotizacion_seleccionada;
		}
		set
		{
			_cod_cotizacion_seleccionada = value;
		}
	}

	public double StockDisponible
	{
		get
		{
			return _stockDisponible;
		}
		set
		{
			_stockDisponible = value;
		}
	}

	public int Cod_proveedor_seleccionado
	{
		get
		{
			return _cod_proveedor_seleccionado;
		}
		set
		{
			_cod_proveedor_seleccionado = value;
		}
	}

	public int OpcionRecuento
	{
		get
		{
			return _opcionRecuento;
		}
		set
		{
			_opcionRecuento = value;
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

	public double UnidadXPaquete
	{
		get
		{
			return _unidadXPaquete;
		}
		set
		{
			_unidadXPaquete = value;
		}
	}

	public int NroItem
	{
		get
		{
			return _nroItem;
		}
		set
		{
			_nroItem = value;
		}
	}
}
