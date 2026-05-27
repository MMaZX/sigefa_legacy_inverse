namespace SIGEFA.Entidades;

public class clsDetalleConsolidado
{
	private int icodDetalle;

	private int iCodProducto;

	private double dCantidad;

	private int iCodAlmacen;

	private int iCodUsuario;

	public int CodDetalle
	{
		get
		{
			return icodDetalle;
		}
		set
		{
			icodDetalle = value;
		}
	}

	public int CodProducto
	{
		get
		{
			return iCodProducto;
		}
		set
		{
			iCodProducto = value;
		}
	}

	public double Cantidad
	{
		get
		{
			return dCantidad;
		}
		set
		{
			dCantidad = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return iCodAlmacen;
		}
		set
		{
			iCodAlmacen = value;
		}
	}

	public int CodUsuario
	{
		get
		{
			return iCodUsuario;
		}
		set
		{
			iCodUsuario = value;
		}
	}
}
