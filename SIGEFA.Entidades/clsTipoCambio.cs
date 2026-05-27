using System;

namespace SIGEFA.Entidades;

public class clsTipoCambio
{
	private int iCodTipoCambio;

	private int iCodMoneda;

	private string sMoneda;

	private double dCompra;

	private double dVenta;

	private DateTime dtFecha;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodTipoCambio
	{
		get
		{
			return iCodTipoCambio;
		}
		set
		{
			iCodTipoCambio = value;
		}
	}

	public int ICodMoneda
	{
		get
		{
			return iCodMoneda;
		}
		set
		{
			iCodMoneda = value;
		}
	}

	public string SMoneda
	{
		get
		{
			return sMoneda;
		}
		set
		{
			sMoneda = value;
		}
	}

	public double Compra
	{
		get
		{
			return dCompra;
		}
		set
		{
			dCompra = value;
		}
	}

	public double Venta
	{
		get
		{
			return dVenta;
		}
		set
		{
			dVenta = value;
		}
	}

	public DateTime Fecha
	{
		get
		{
			return dtFecha;
		}
		set
		{
			dtFecha = value;
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
