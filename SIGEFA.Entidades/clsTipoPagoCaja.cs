using System;

namespace SIGEFA.Entidades;

internal class clsTipoPagoCaja
{
	private int iCodTipoPagoServicio;

	private int iCodTipoPagoServicioNuevo;

	private string sDescripcion;

	private bool bEstado;

	private int iCodUser;

	private DateTime dFechaRegistro;

	public int CodTipoPagoServicio
	{
		get
		{
			return iCodTipoPagoServicio;
		}
		set
		{
			iCodTipoPagoServicio = value;
		}
	}

	public int CodTipoPagoServicioNuevo
	{
		get
		{
			return iCodTipoPagoServicioNuevo;
		}
		set
		{
			iCodTipoPagoServicioNuevo = value;
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
			return bEstado;
		}
		set
		{
			bEstado = value;
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
			return dFechaRegistro;
		}
		set
		{
			dFechaRegistro = value;
		}
	}
}
