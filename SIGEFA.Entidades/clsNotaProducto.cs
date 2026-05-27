using System;

namespace SIGEFA.Entidades;

internal class clsNotaProducto
{
	private int iCodNotaProducto;

	private int iCodNotaProductoNuevo;

	private int iCodProducto;

	private string sNota;

	private bool bEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodNotaProducto
	{
		get
		{
			return iCodNotaProducto;
		}
		set
		{
			iCodNotaProducto = value;
		}
	}

	public int CodNotaProductoNuevo
	{
		get
		{
			return iCodNotaProductoNuevo;
		}
		set
		{
			iCodNotaProductoNuevo = value;
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

	public string Nota
	{
		get
		{
			return sNota;
		}
		set
		{
			sNota = value;
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
