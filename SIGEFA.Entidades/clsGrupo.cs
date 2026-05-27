using System;

namespace SIGEFA.Entidades;

public class clsGrupo
{
	private int iCodGrupo;

	private int iCodGrupoNuevo;

	private int iCodLinea;

	private string sReferencia;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodGrupoNuevo
	{
		get
		{
			return iCodGrupoNuevo;
		}
		set
		{
			iCodGrupoNuevo = value;
		}
	}

	public int CodGrupo
	{
		get
		{
			return iCodGrupo;
		}
		set
		{
			iCodGrupo = value;
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
