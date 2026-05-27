using System;

namespace SIGEFA.Entidades;

public class clsAutorizado
{
	private int iCodAutorizado;

	private int iCodAutorizadoNuevo;

	private string sNombre;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodAutorizadoNuevo
	{
		get
		{
			return iCodAutorizadoNuevo;
		}
		set
		{
			iCodAutorizadoNuevo = value;
		}
	}

	public int CodAutorizado
	{
		get
		{
			return iCodAutorizado;
		}
		set
		{
			iCodAutorizado = value;
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
