using System;

namespace SIGEFA.Entidades;

public class clsConductor
{
	private int iCodConductor;

	private int iCodConductorNuevo;

	private string sDni;

	private string sRuc;

	private string sNombre;

	private string sLicencia;

	private string sTelefono;

	private string sDireccion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodConductorNuevo
	{
		get
		{
			return iCodConductorNuevo;
		}
		set
		{
			iCodConductorNuevo = value;
		}
	}

	public int CodConductor
	{
		get
		{
			return iCodConductor;
		}
		set
		{
			iCodConductor = value;
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

	public string Licencia
	{
		get
		{
			return sLicencia;
		}
		set
		{
			sLicencia = value;
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
