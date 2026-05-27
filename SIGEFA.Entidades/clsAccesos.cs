using System;

namespace SIGEFA.Entidades;

internal class clsAccesos : IEquatable<clsAccesos>
{
	private int iCodAcceso;

	private int iCodNuevoAcceso;

	private int iCodUsuario;

	private int iCodAlmacen;

	private int iCodFormulario;

	private DateTime dFechaRegistro;

	private int iCodUser;

	public int CodAcceso
	{
		get
		{
			return iCodAcceso;
		}
		set
		{
			iCodAcceso = value;
		}
	}

	public int CodNuevoAcceso
	{
		get
		{
			return iCodNuevoAcceso;
		}
		set
		{
			iCodNuevoAcceso = value;
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

	public int CodFormulario
	{
		get
		{
			return iCodFormulario;
		}
		set
		{
			iCodFormulario = value;
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

	public bool Equals(clsAccesos other)
	{
		if (CodFormulario == other.CodFormulario && CodAlmacen == other.CodAlmacen)
		{
			return true;
		}
		return false;
	}
}
