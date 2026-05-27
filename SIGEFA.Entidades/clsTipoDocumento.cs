using System;

namespace SIGEFA.Entidades;

public class clsTipoDocumento
{
	private int iCodTipoDocumento;

	private int iCodTipoDocumentoNuevo;

	private string sSigla;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public string Tipodoccodsunat { get; set; }

	public int CodTipoDocumentoNuevo
	{
		get
		{
			return iCodTipoDocumentoNuevo;
		}
		set
		{
			iCodTipoDocumentoNuevo = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return iCodTipoDocumento;
		}
		set
		{
			iCodTipoDocumento = value;
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
