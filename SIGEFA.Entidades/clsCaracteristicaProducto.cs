using System;

namespace SIGEFA.Entidades;

internal class clsCaracteristicaProducto
{
	private int iCodCaracteristicaProducto;

	private int iCodCaracteristicaProductoNuevo;

	private int iCodProducto;

	private int iCodCaracteristica;

	private string sValor;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodCaracteristicaProducto
	{
		get
		{
			return iCodCaracteristicaProducto;
		}
		set
		{
			iCodCaracteristicaProducto = value;
		}
	}

	public int CodCaracteristicaProductoNuevo
	{
		get
		{
			return iCodCaracteristicaProductoNuevo;
		}
		set
		{
			iCodCaracteristicaProductoNuevo = value;
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

	public int CodCaracteristica
	{
		get
		{
			return iCodCaracteristica;
		}
		set
		{
			iCodCaracteristica = value;
		}
	}

	public string Valor
	{
		get
		{
			return sValor;
		}
		set
		{
			sValor = value;
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
