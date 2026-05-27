using System;

namespace SIGEFA.Entidades;

internal class clsVariante
{
	private int iCodVariante;

	private int iCodVarianteNuevo;

	private int iCodCaracteristica;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public string Etiqueta { get; set; }

	public int CodVarianteNuevo
	{
		get
		{
			return iCodVarianteNuevo;
		}
		set
		{
			iCodVarianteNuevo = value;
		}
	}

	public int CodVariante
	{
		get
		{
			return iCodVariante;
		}
		set
		{
			iCodVariante = value;
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
