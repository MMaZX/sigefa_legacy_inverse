using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsSucursal
{
	private int iCodSucursal;

	private int iCodSucursalNueva;

	private int iCodEmpresa;

	private string sNombre;

	private string sUbicacion;

	private string sTelefono;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private List<int> lCodidosForm;

	public int CodSucursal
	{
		get
		{
			return iCodSucursal;
		}
		set
		{
			iCodSucursal = value;
		}
	}

	public int CodSucursalNueva
	{
		get
		{
			return iCodSucursalNueva;
		}
		set
		{
			iCodSucursalNueva = value;
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
}
