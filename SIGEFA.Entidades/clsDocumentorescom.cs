using System;

namespace SIGEFA.Entidades;

public class clsDocumentorescom
{
	private int codigo;

	private int codSerie;

	private string numeracion;

	private string tipodocumento;

	private bool estado;

	private int codUser;

	private DateTime fecharegistro;

	private int codigonuevo;

	private int codtipodocumento;

	public int Codtipodocumento
	{
		get
		{
			return codtipodocumento;
		}
		set
		{
			codtipodocumento = value;
		}
	}

	public int Codigonuevo
	{
		get
		{
			return codigonuevo;
		}
		set
		{
			codigonuevo = value;
		}
	}

	public int Codigo
	{
		get
		{
			return codigo;
		}
		set
		{
			codigo = value;
		}
	}

	public int CodSerie
	{
		get
		{
			return codSerie;
		}
		set
		{
			codSerie = value;
		}
	}

	public string Numeracion
	{
		get
		{
			return numeracion;
		}
		set
		{
			numeracion = value;
		}
	}

	public string Tipodocumento
	{
		get
		{
			return tipodocumento;
		}
		set
		{
			tipodocumento = value;
		}
	}

	public bool Estado
	{
		get
		{
			return estado;
		}
		set
		{
			estado = value;
		}
	}

	public int CodUser
	{
		get
		{
			return codUser;
		}
		set
		{
			codUser = value;
		}
	}

	public DateTime Fecharegistro
	{
		get
		{
			return fecharegistro;
		}
		set
		{
			fecharegistro = value;
		}
	}
}
