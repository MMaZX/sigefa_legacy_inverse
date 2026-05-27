using System;

namespace SIGEFA.Entidades;

public class clsCuota
{
	private int iCodCuotaPrestamo;

	private int iCodPrestamoBancario;

	private int iNroCuota;

	private DateTime dtFechaEmision;

	private DateTime dtFechaVencimiento;

	private DateTime dtFechaCancelado;

	private int iCodMoneda;

	private string sDescMoneda;

	private decimal dTipoCambio;

	private decimal dMonto;

	private decimal dMontoPendiente;

	private decimal dMontoadicional;

	private bool bCancelado;

	private bool bEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	public int CodCuotaPrestamo
	{
		get
		{
			return iCodCuotaPrestamo;
		}
		set
		{
			iCodCuotaPrestamo = value;
		}
	}

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

	public int NroCuota
	{
		get
		{
			return iNroCuota;
		}
		set
		{
			iNroCuota = value;
		}
	}

	public DateTime FechaEmision
	{
		get
		{
			return dtFechaEmision;
		}
		set
		{
			dtFechaEmision = value;
		}
	}

	public DateTime FechaVencimiento
	{
		get
		{
			return dtFechaVencimiento;
		}
		set
		{
			dtFechaVencimiento = value;
		}
	}

	public DateTime FechaCancelado
	{
		get
		{
			return dtFechaCancelado;
		}
		set
		{
			dtFechaCancelado = value;
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

	public decimal Monto
	{
		get
		{
			return dMonto;
		}
		set
		{
			dMonto = value;
		}
	}

	public decimal MontoPendiente
	{
		get
		{
			return dMontoPendiente;
		}
		set
		{
			dMontoPendiente = value;
		}
	}

	public decimal Montoadicional
	{
		get
		{
			return dMontoadicional;
		}
		set
		{
			dMontoadicional = value;
		}
	}

	public bool Cancelado
	{
		get
		{
			return bCancelado;
		}
		set
		{
			bCancelado = value;
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
			return dtFechaRegistro;
		}
		set
		{
			dtFechaRegistro = value;
		}
	}
}
