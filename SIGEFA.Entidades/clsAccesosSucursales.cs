using System;

namespace SIGEFA.Entidades;

internal class clsAccesosSucursales : IEquatable<clsAccesosSucursales>
{
	private int iCodAccesoSucursal;

	private int iCodAccesoSucursalNuevo;

	private int iCodUsuario;

	private int iCodEmpresa;

	private int iCodSucursal;

	private DateTime dFechaRegistro;

	private int iCodUser;

	public int CodAccesoSucursal
	{
		get
		{
			return iCodAccesoSucursal;
		}
		set
		{
			iCodAccesoSucursal = value;
		}
	}

	public int CodAccesoSucursalNuevo
	{
		get
		{
			return iCodAccesoSucursalNuevo;
		}
		set
		{
			iCodAccesoSucursalNuevo = value;
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

	public DateTime FechaRegistro
	{
		get
		{
			return dFechaRegistro;
		}
		set
		{
			dFechaRegistro = value;
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

	public bool Equals(clsAccesosSucursales other)
	{
		if (CodEmpresa == other.CodEmpresa && CodSucursal == other.CodSucursal)
		{
			return true;
		}
		return false;
	}
}
