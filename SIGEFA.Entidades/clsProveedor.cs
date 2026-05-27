using System;

namespace SIGEFA.Entidades;

public class clsProveedor
{
	private int iCodProveedor;

	private int iCodProveedorNuevo;

	private string sRazonSocial;

	private string sRuc;

	private string sDireccion;

	private string sTelefono;

	private string sFax;

	private string sRepresentante;

	private string sContacto;

	private string sTelefonoContacto;

	private int iFrecuenciaVisita;

	private double dMargen;

	private string sBanco;

	private string sCtaCte;

	private string sComentario;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string iDepartamento;

	private string iProvincia;

	private string iDistrito;

	private string iMail;

	public string Mail
	{
		get
		{
			return iMail;
		}
		set
		{
			iMail = value;
		}
	}

	public string Departamento
	{
		get
		{
			return iDepartamento;
		}
		set
		{
			iDepartamento = value;
		}
	}

	public string Provincia
	{
		get
		{
			return iProvincia;
		}
		set
		{
			iProvincia = value;
		}
	}

	public string Distrito
	{
		get
		{
			return iDistrito;
		}
		set
		{
			iDistrito = value;
		}
	}

	public int CodProveedorNuevo
	{
		get
		{
			return iCodProveedorNuevo;
		}
		set
		{
			iCodProveedorNuevo = value;
		}
	}

	public int CodProveedor
	{
		get
		{
			return iCodProveedor;
		}
		set
		{
			iCodProveedor = value;
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

	public string Contacto
	{
		get
		{
			return sContacto;
		}
		set
		{
			sContacto = value;
		}
	}

	public string TelefonoContacto
	{
		get
		{
			return sTelefonoContacto;
		}
		set
		{
			sTelefonoContacto = value;
		}
	}

	public int FrecuenciaVisita
	{
		get
		{
			return iFrecuenciaVisita;
		}
		set
		{
			iFrecuenciaVisita = value;
		}
	}

	public double Margen
	{
		get
		{
			return dMargen;
		}
		set
		{
			dMargen = value;
		}
	}

	public string Banco
	{
		get
		{
			return sBanco;
		}
		set
		{
			sBanco = value;
		}
	}

	public string CtaCte
	{
		get
		{
			return sCtaCte;
		}
		set
		{
			sCtaCte = value;
		}
	}

	public string Comentario
	{
		get
		{
			return sComentario;
		}
		set
		{
			sComentario = value;
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
