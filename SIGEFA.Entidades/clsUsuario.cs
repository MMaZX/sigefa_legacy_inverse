using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsUsuario
{
	private int iCodUsuario;

	private int iCodUsuarioNuevo;

	private string sDni;

	private string sNombre;

	private string sApellido;

	private DateTime dtFechaNac;

	private string sDireccion;

	private string sTelefono;

	private string sCelular;

	private string sEmail;

	private string sUsuario;

	private string sContraseña;

	private string sContraemail;

	private string sHost;

	private int iNivel;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int iCodEmpresaLogin;

	private List<int> lCodidosForm;

	private int iCodSucursalLogin;

	private string sCodigoCanalVenta;

	private bool CanalVentaAcceso;

	public int EstadoIngreso { get; set; }

	public int CodUsuarioNuevo
	{
		get
		{
			return iCodUsuarioNuevo;
		}
		set
		{
			iCodUsuarioNuevo = value;
		}
	}

	public int CodUsuario
	{
		get
		{
			return iCodUsuario;
		}
		set
		{
			iCodUsuario = value;
		}
	}

	public string Dni
	{
		get
		{
			return sDni;
		}
		set
		{
			sDni = value;
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

	public string Apellido
	{
		get
		{
			return sApellido;
		}
		set
		{
			sApellido = value;
		}
	}

	public DateTime FechaNac
	{
		get
		{
			return dtFechaNac;
		}
		set
		{
			dtFechaNac = value;
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

	public string Celular
	{
		get
		{
			return sCelular;
		}
		set
		{
			sCelular = value;
		}
	}

	public string Email
	{
		get
		{
			return sEmail;
		}
		set
		{
			sEmail = value;
		}
	}

	public string Usuario
	{
		get
		{
			return sUsuario;
		}
		set
		{
			sUsuario = value;
		}
	}

	public string Contraseña
	{
		get
		{
			return sContraseña;
		}
		set
		{
			sContraseña = value;
		}
	}

	public string Host
	{
		get
		{
			return sHost;
		}
		set
		{
			sHost = value;
		}
	}

	public string ContraEmail
	{
		get
		{
			return sContraemail;
		}
		set
		{
			sContraemail = value;
		}
	}

	public int Nivel
	{
		get
		{
			return iNivel;
		}
		set
		{
			iNivel = value;
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

	public int CodEmpresaLogin
	{
		get
		{
			return iCodEmpresaLogin;
		}
		set
		{
			iCodEmpresaLogin = value;
		}
	}

	public List<int> CodigosForm
	{
		get
		{
			return lCodidosForm;
		}
		set
		{
			lCodidosForm = value;
		}
	}

	public int CodSucursalLogin
	{
		get
		{
			return iCodSucursalLogin;
		}
		set
		{
			iCodSucursalLogin = value;
		}
	}

	public string CodigoCanalVenta
	{
		get
		{
			return sCodigoCanalVenta;
		}
		set
		{
			sCodigoCanalVenta = value;
		}
	}

	public bool CanalVentaAcceso1
	{
		get
		{
			return CanalVentaAcceso;
		}
		set
		{
			CanalVentaAcceso = value;
		}
	}
}
