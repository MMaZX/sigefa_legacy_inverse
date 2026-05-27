using System;

namespace SIGEFA.Entidades;

public class clsMarcaTransporte
{
	private int iCodMarcaTransporte;

	private int iCodMarcaTransporteNuevo;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodMarcaTransporteNuevo
	{
		get
		{
			return iCodMarcaTransporteNuevo;
		}
		set
		{
			iCodMarcaTransporteNuevo = value;
		}
	}

	public int CodMarcaTransporte
	{
		get
		{
			return iCodMarcaTransporte;
		}
		set
		{
			iCodMarcaTransporte = value;
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
}
