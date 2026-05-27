using System;

namespace SIGEFA.Entidades;

public class clsPrestamoBancario
{
	private int iCodPrestamoBancario;

	private int iCodBanco;

	private string sDescBanco;

	private int iCodMoneda;

	private string sDescMoneda;

	private decimal dTipoCambio;

	private decimal dMontoprestamo;

	private decimal dMontointeres;

	private decimal dMontodevolver;

	private decimal dPendiente;

	private DateTime dtFechaaprobacion;

	private DateTime dtFechavencimiento;

	private string sDescripcion;

	private bool bCronograma;

	private int iCancelado;

	private DateTime dtFechacancelado;

	private int iCantCuotas;

	public int CodPrestamoBancario
	{
		get
		{
			return iCodPrestamoBancario;
		}
		set
		{
			iCodPrestamoBancario = value;
		}
	}

	public int CodBanco
	{
		get
		{
			return iCodBanco;
		}
		set
		{
			iCodBanco = value;
		}
	}

	public string DescBanco
	{
		get
		{
			return sDescBanco;
		}
		set
		{
			sDescBanco = value;
		}
	}

	public int CodMoneda
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

	public string DescMoneda
	{
		get
		{
			return sDescMoneda;
		}
		set
		{
			sDescMoneda = value;
		}
	}

	public decimal TipoCambio
	{
		get
		{
			return dTipoCambio;
		}
		set
		{
			dTipoCambio = value;
		}
	}

	public decimal Montoprestamo
	{
		get
		{
			return dMontoprestamo;
		}
		set
		{
			dMontoprestamo = value;
		}
	}

	public decimal Montointeres
	{
		get
		{
			return dMontointeres;
		}
		set
		{
			dMontointeres = value;
		}
	}

	public decimal Montodevolver
	{
		get
		{
			return dMontodevolver;
		}
		set
		{
			dMontodevolver = value;
		}
	}

	public decimal Pendiente
	{
		get
		{
			return dPendiente;
		}
		set
		{
			dPendiente = value;
		}
	}

	public DateTime Fechaaprobacion
	{
		get
		{
			return dtFechaaprobacion;
		}
		set
		{
			dtFechaaprobacion = value;
		}
	}

	public DateTime Fechavencimiento
	{
		get
		{
			return dtFechavencimiento;
		}
		set
		{
			dtFechavencimiento = value;
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

	public bool Cronograma
	{
		get
		{
			return bCronograma;
		}
		set
		{
			bCronograma = value;
		}
	}

	public int Cancelado
	{
		get
		{
			return iCancelado;
		}
		set
		{
			iCancelado = value;
		}
	}

	public DateTime Fechacancelado
	{
		get
		{
			return dtFechacancelado;
		}
		set
		{
			dtFechacancelado = value;
		}
	}

	public int CantCuotas
	{
		get
		{
			return iCantCuotas;
		}
		set
		{
			iCantCuotas = value;
		}
	}
}
