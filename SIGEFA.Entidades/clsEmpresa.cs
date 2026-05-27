using System;

namespace SIGEFA.Entidades;

public class clsEmpresa
{
	private int iCodEmpresa;

	private int iCodEmpresaNueva;

	private string sRazonSocial;

	private string sRuc;

	private string sDireccion;

	private string sTelefono;

	private string sNombreCorto;

	private string sFax;

	private string sRepresentante;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private byte[] logo;

	public int CodEmpresaNuevo
	{
		get
		{
			return iCodEmpresaNueva;
		}
		set
		{
			iCodEmpresaNueva = value;
		}
	}

	public int CodEmpresa
	{
		get
		{
			return iCodEmpresa;
		}
		set
		{
			iCodEmpresa = value;
		}
	}

	public string RazonSocial
	{
		get
		{
			return sRazonSocial;
		}
		set
		{
			sRazonSocial = value;
		}
	}

	public string NombreCorto
	{
		get
		{
			return sNombreCorto;
		}
		set
		{
			sNombreCorto = value;
		}
	}

	public string Ruc
	{
		get
		{
			return sRuc;
		}
		set
		{
			sRuc = value;
		}
	}

	public string Direccion
	{
		get
		{
			return sDireccion;
		}
		set
		{
			sDireccion = value;
		}
	}

	public string Telefono
	{
		get
		{
			return sTelefono;
		}
		set
		{
			sTelefono = value;
		}
	}

	public string Fax
	{
		get
		{
			return sFax;
		}
		set
		{
			sFax = value;
		}
	}

	public string Representante
	{
		get
		{
			return sRepresentante;
		}
		set
		{
			sRepresentante = value;
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

	public string Certificado { get; set; }

	public string Contrasena { get; set; }

	public string UsuarioSunat { get; set; }

	public string ClaveSunat { get; set; }

	public string IDSUNATAPI { get; set; }

	public string CLAVESUNATAPI { get; set; }

	public string Url { get; set; }

	public byte[] Logo
	{
		get
		{
			return logo;
		}
		set
		{
			logo = value;
		}
	}
}
