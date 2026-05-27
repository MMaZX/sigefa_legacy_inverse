using System;

namespace SIGEFA.Entidades;

public class clsUnidadMedida
{
	private int iCodUnidad;

	private int iCodUnidadNuevo;

	private string sSigla;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodUnidadNuevo
	{
		get
		{
			return iCodUnidadNuevo;
		}
		set
		{
			iCodUnidadNuevo = value;
		}
	}

	public int CodUnidad
	{
		get
		{
			return iCodUnidad;
		}
		set
		{
			iCodUnidad = value;
		}
	}

	public string Sigla
	{
		get
		{
			return sSigla;
		}
		set
		{
			sSigla = value;
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
