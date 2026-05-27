using System;

namespace SIGEFA.Entidades;

public class clsEmpresaTransporte
{
	private int iCodEmpresaTranporte;

	private int iCodEmpresaTranporteNuevo;

	private string sRuc;

	private string sRazonSocial;

	private string sTelefono;

	private string sDireccion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodEmpresaTranporteNuevo
	{
		get
		{
			return iCodEmpresaTranporteNuevo;
		}
		set
		{
			iCodEmpresaTranporteNuevo = value;
		}
	}

	public int CodEmpresaTranporte
	{
		get
		{
			return iCodEmpresaTranporte;
		}
		set
		{
			iCodEmpresaTranporte = value;
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
