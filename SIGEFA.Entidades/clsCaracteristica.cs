using System;

namespace SIGEFA.Entidades;

internal class clsCaracteristica
{
	private int iCodCaracteristica;

	private int iCodCaracteristicaNuevo;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodCaracteristicaNuevo
	{
		get
		{
			return iCodCaracteristicaNuevo;
		}
		set
		{
			iCodCaracteristicaNuevo = value;
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
