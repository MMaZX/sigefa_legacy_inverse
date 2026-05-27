namespace SIGEFA.Entidades;

internal class clsDetalleDespacho
{
	private int codDetalleDespacho;

	private int codDespacho;

	private int codProducto;

	private int codUnidad;

	private double cantidad;

	private double cantidadPendiente;

	private int codAlmacenEntregar;

	private string referenciaProducto;

	private string descripcionProducto;

	private string descripcionUnidad;

	public int CodDetalleDespacho
	{
		get
		{
			return codDetalleDespacho;
		}
		set
		{
			codDetalleDespacho = value;
		}
	}

	public int CodDespacho
	{
		get
		{
			return codDespacho;
		}
		set
		{
			codDespacho = value;
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

	public double Cantidad
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

	public double CantidadPendiente
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

	public int CodAlmacenEntregar
	{
		get
		{
			return codAlmacenEntregar;
		}
		set
		{
			codAlmacenEntregar = value;
		}
	}

	public string ReferenciaProducto
	{
		get
		{
			return referenciaProducto;
		}
		set
		{
			referenciaProducto = value;
		}
	}

	public string DescripcionProducto
	{
		get
		{
			return descripcionProducto;
		}
		set
		{
			descripcionProducto = value;
		}
	}

	public string DescripcionUnidad
	{
		get
		{
			return descripcionUnidad;
		}
		set
		{
			descripcionUnidad = value;
		}
	}

	public int Estado { get; internal set; }
}
