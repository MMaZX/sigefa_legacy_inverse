using System;

namespace SIGEFA.Entidades;

public class clsLinea
{
	private int iCodLinea;

	private int iCodLineaNuevo;

	private int iCodFamilia;

	private string sReferencia;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodLineaNuevo
	{
		get
		{
			return iCodLineaNuevo;
		}
		set
		{
			iCodLineaNuevo = value;
		}
	}

	public int CodLinea
	{
		get
		{
			return iCodLinea;
		}
		set
		{
			iCodLinea = value;
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
