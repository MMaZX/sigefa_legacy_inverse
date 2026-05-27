using System;

namespace SIGEFA.Entidades;

public class clsModeloTransporte
{
	private int iCodModeloTransporte;

	private int iCodModeloTransporteNuevo;

	private int iCodMarcaTransporte;

	private string sReferencia;

	private string sDescripcion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodModeloTransporteNuevo
	{
		get
		{
			return iCodModeloTransporteNuevo;
		}
		set
		{
			iCodModeloTransporteNuevo = value;
		}
	}

	public int CodModeloTransporte
	{
		get
		{
			return iCodModeloTransporte;
		}
		set
		{
			iCodModeloTransporte = value;
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
