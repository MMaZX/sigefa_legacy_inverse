namespace SIGEFA.Entidades;

internal class clsDetalleEntrega
{
	private int codDetalleEntrega;

	private int codDetalleDespacho;

	private int codEntrega;

	private int codProducto;

	private int codUnidad;

	private int codAlmacenEntregar;

	private double cantidad;

	private string referenciaProducto;

	private string descripcionProducto;

	private string descripcionUnidad;

	public int CodDetalleEntrega
	{
		get
		{
			return codDetalleEntrega;
		}
		set
		{
			codDetalleEntrega = value;
		}
	}

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

	public int CodEntrega
	{
		get
		{
			return codEntrega;
		}
		set
		{
			codEntrega = value;
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
}
