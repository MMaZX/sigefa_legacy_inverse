using System;

namespace SIGEFA.Entidades;

internal class clsBanco
{
	private int iCodBanco;

	private int iCodBancoNuevo;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string sSiglaBanco;

	public int CodBancoNuevo
	{
		get
		{
			return iCodBancoNuevo;
		}
		set
		{
			iCodBancoNuevo = value;
		}
	}

	public int CodBanco
	{
		get
		{
			return iCodBanco;
		}
		set
		{
			iCodBanco = value;
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

	public string SiglaBanco
	{
		get
		{
			return sSiglaBanco;
		}
		set
		{
			sSiglaBanco = value;
		}
	}
}
