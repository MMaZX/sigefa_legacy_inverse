using System;

namespace SIGEFA.Entidades;

public class clsFamilia
{
	private int iCodFamilia;

	private int iCodFamiliaNuevo;

	private string sReferencia;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodFamiliaNuevo
	{
		get
		{
			return iCodFamiliaNuevo;
		}
		set
		{
			iCodFamiliaNuevo = value;
		}
	}

	public int CodFamilia
	{
		get
		{
			return iCodFamilia;
		}
		set
		{
			iCodFamilia = value;
		}
	}

	public string Referencia
	{
		get
		{
			return sReferencia;
		}
		set
		{
			sReferencia = value;
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
