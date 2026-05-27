using System;

namespace SIGEFA.Entidades;

public class clsAlmacen
{
	private int iCodAlmacen;

	private int iCodAlmacenNuevo;

	private int iCodEmpresa;

	private int iCodUser;

	private int iCodSuc;

	private string sNombre;

	private string sUbicacion;

	private string sTelefono;

	private string sDescripcion;

	private bool iEstado;

	private DateTime dtFechaRegistro;

	private bool iEstadoPrincipal;

	public bool EstadoPrincipal
	{
		get
		{
			return iEstadoPrincipal;
		}
		set
		{
			iEstadoPrincipal = value;
		}
	}

	public int CodAlmacenNuevo
	{
		get
		{
			return iCodAlmacenNuevo;
		}
		set
		{
			iCodAlmacenNuevo = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return iCodAlmacen;
		}
		set
		{
			iCodAlmacen = value;
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

	public int CodSuc
	{
		get
		{
			return iCodSuc;
		}
		set
		{
			iCodSuc = value;
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

	public string Ubicacion
	{
		get
		{
			return sUbicacion;
		}
		set
		{
			sUbicacion = value;
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
