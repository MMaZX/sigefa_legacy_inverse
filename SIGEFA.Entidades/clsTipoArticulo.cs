using System;

namespace SIGEFA.Entidades;

internal class clsTipoArticulo
{
	private int iCodTipoArticulo;

	private int iCodTipoArticuloNuevo;

	private string sReferencia;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodTipoArticuloNuevo
	{
		get
		{
			return iCodTipoArticuloNuevo;
		}
		set
		{
			iCodTipoArticuloNuevo = value;
		}
	}

	public int CodTipoArticulo
	{
		get
		{
			return iCodTipoArticulo;
		}
		set
		{
			iCodTipoArticulo = value;
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
