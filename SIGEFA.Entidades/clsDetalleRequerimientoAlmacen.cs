namespace SIGEFA.Entidades;

internal class clsDetalleRequerimientoAlmacen
{
	private int codigo;

	private int codRequerimiento;

	private int codProducto;

	private string refProducto;

	private string descripProducto;

	private int codUnidad;

	private string descripUnidad;

	private decimal cantidad;

	private decimal cantidadPedida;

	private decimal cantidadConfirmada;

	private decimal cantidadPendiente;

	private decimal cantidadPendienteAprobada;

	public int CodRequerimiento
	{
		get
		{
			return codRequerimiento;
		}
		set
		{
			codRequerimiento = value;
		}
	}

	public int CodProducto
	{
		get
		{
			return codProducto;
		}
		set
		{
			codProducto = value;
		}
	}

	public string RefProducto
	{
		get
		{
			return refProducto;
		}
		set
		{
			refProducto = value;
		}
	}

	public string DescripProducto
	{
		get
		{
			return descripProducto;
		}
		set
		{
			descripProducto = value;
		}
	}

	public int CodUnidad
	{
		get
		{
			return codUnidad;
		}
		set
		{
			codUnidad = value;
		}
	}

	public string DescripUnidad
	{
		get
		{
			return descripUnidad;
		}
		set
		{
			descripUnidad = value;
		}
	}

	public decimal Cantidad
	{
		get
		{
			return cantidad;
		}
		set
		{
			cantidad = value;
		}
	}

	public decimal CantidadPedida
	{
		get
		{
			return cantidadPedida;
		}
		set
		{
			cantidadPedida = value;
		}
	}

	public decimal CantidadConfirmada
	{
		get
		{
			return cantidadConfirmada;
		}
		set
		{
			cantidadConfirmada = value;
		}
	}

	public decimal CantidadPendiente
	{
		get
		{
			return cantidadPendiente;
		}
		set
		{
			cantidadPendiente = value;
		}
	}

	public int Codigo
	{
		get
		{
			return codigo;
		}
		set
		{
			codigo = value;
		}
	}

	public decimal CantidadPendienteAprobada
	{
		get
		{
			return cantidadPendienteAprobada;
		}
		set
		{
			cantidadPendienteAprobada = value;
		}
	}
}
