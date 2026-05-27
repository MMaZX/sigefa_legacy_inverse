using System;

namespace SIGEFA.Entidades;

public class clsVendedor
{
	private int iCodVendedor;

	private int iCodVendedorNuevo;

	private string sDni;

	private string sNombre;

	private string sApellido;

	private DateTime dtFechaNac;

	private string sDireccion;

	private string sTelefono;

	private string sCelular;

	private string sEmail;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodVendedorNuevo
	{
		get
		{
			return iCodVendedorNuevo;
		}
		set
		{
			iCodVendedorNuevo = value;
		}
	}

	public int CodVendedor
	{
		get
		{
			return iCodVendedor;
		}
		set
		{
			iCodVendedor = value;
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
