using System;

namespace SIGEFA.Entidades;

public class clsListaPrecio
{
	private int iCodListaPrecio;

	private int icodSucursal;

	private string sNombre;

	private bool bMargenProv;

	private double dMargen;

	private double dDescuento1;

	private double dDescuento2;

	private double dDescuento3;

	private bool bPrecioProm;

	private int iListaOrigen;

	private double dVariacion;

	private bool bUpdate;

	private int iDecimales;

	private bool bRedondear;

	private bool bEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int icodFormaPago;

	public int CodFormaPago
	{
		get
		{
			return icodFormaPago;
		}
		set
		{
			icodFormaPago = value;
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

	public int CodSucursal
	{
		get
		{
			return icodSucursal;
		}
		set
		{
			icodSucursal = value;
		}
	}

	public string Nombre
	{
		get
		{
			return sNombre;
		}
		set
		{
			sNombre = value;
		}
	}

	public bool MargenProv
	{
		get
		{
			return bMargenProv;
		}
		set
		{
			bMargenProv = value;
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

	public bool PrecioProm
	{
		get
		{
			return bPrecioProm;
		}
		set
		{
			bPrecioProm = value;
		}
	}

	public int ListaOrigen
	{
		get
		{
			return iListaOrigen;
		}
		set
		{
			iListaOrigen = value;
		}
	}

	public double Variacion
	{
		get
		{
			return dVariacion;
		}
		set
		{
			dVariacion = value;
		}
	}

	public bool Update
	{
		get
		{
			return bUpdate;
		}
		set
		{
			bUpdate = value;
		}
	}

	public int Decimales
	{
		get
		{
			return iDecimales;
		}
		set
		{
			iDecimales = value;
		}
	}

	public bool Redondear
	{
		get
		{
			return bRedondear;
		}
		set
		{
			bRedondear = value;
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
