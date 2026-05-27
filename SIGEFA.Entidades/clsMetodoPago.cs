using System;

namespace SIGEFA.Entidades;

public class clsMetodoPago
{
	private int iCodMetodoPago;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodMetodoPago
	{
		get
		{
			return iCodMetodoPago;
		}
		set
		{
			iCodMetodoPago = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return sDescripcion;
		}
		set
		{
			sDescripcion = value;
		}
	}

	public bool Estado
	{
		get
		{
			return iEstado;
		}
		set
		{
			iEstado = value;
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
