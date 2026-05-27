using System;

namespace SIGEFA.Entidades;

public class clsDetalleListaPrecio
{
	private int iCodDetalleLista;

	private int iCodListaPrecio;

	private int iCodProducto;

	private double dValor;

	private double dMargen;

	private double dDescuento1;

	private double dDescuento2;

	private double dDescuento3;

	private double dPrecioNeto;

	private double dPrecio;

	private bool bEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodDetalleLista
	{
		get
		{
			return iCodDetalleLista;
		}
		set
		{
			iCodDetalleLista = value;
		}
	}

	public int CodListaPrecio
	{
		get
		{
			return iCodListaPrecio;
		}
		set
		{
			iCodListaPrecio = value;
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

	public double Valor
	{
		get
		{
			return dValor;
		}
		set
		{
			dValor = value;
		}
	}

	public double Margen
	{
		get
		{
			return dMargen;
		}
		set
		{
			dMargen = value;
		}
	}

	public double Descuento1
	{
		get
		{
			return dDescuento1;
		}
		set
		{
			dDescuento1 = value;
		}
	}

	public double Descuento2
	{
		get
		{
			return dDescuento2;
		}
		set
		{
			dDescuento2 = value;
		}
	}

	public double Descuento3
	{
		get
		{
			return dDescuento3;
		}
		set
		{
			dDescuento3 = value;
		}
	}

	public double PrecioNeto
	{
		get
		{
			return dPrecioNeto;
		}
		set
		{
			dPrecioNeto = value;
		}
	}

	public double Precio
	{
		get
		{
			return dPrecio;
		}
		set
		{
			dPrecio = value;
		}
	}

	public bool Estado
	{
		get
		{
			return bEstado;
		}
		set
		{
			bEstado = value;
		}
	}

	public int CodUser
	{
		get
		{
			return iCodUser;
		}
		set
		{
			iCodUser = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return dtFechaRegistro;
		}
		set
		{
			dtFechaRegistro = value;
		}
	}
}
