using System;

namespace SIGEFA.Entidades;

internal class clsMarca
{
	private int iCodMarca;

	private int iCodMarcaNuevo;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodMarcaNuevo
	{
		get
		{
			return iCodMarcaNuevo;
		}
		set
		{
			iCodMarcaNuevo = value;
		}
	}

	public int CodMarca
	{
		get
		{
			return iCodMarca;
		}
		set
		{
			iCodMarca = value;
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
