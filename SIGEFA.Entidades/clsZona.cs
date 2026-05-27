using System;

namespace SIGEFA.Entidades;

internal class clsZona
{
	private int iCodZona;

	private int iCodZonaNuevo;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodZonaNuevo
	{
		get
		{
			return iCodZonaNuevo;
		}
		set
		{
			iCodZonaNuevo = value;
		}
	}

	public int CodZona
	{
		get
		{
			return iCodZona;
		}
		set
		{
			iCodZona = value;
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
