using System;

namespace SIGEFA.Entidades;

public class clsFlujoCaja
{
	private int iCodFlujoCaja;

	private int iCodFlujoCajaNuevo;

	private DateTime dFechaRegistro;

	private int iCodUser;

	private int iCodSucursal;

	private DateTime dFechaApertura;

	private decimal dMontoApertura;

	private DateTime dFechaCierre;

	private decimal dMontoCierre;

	private decimal dMontoIngresado;

	private decimal dMontoDepositado;

	private decimal dMontoDisponible;

	private DateTime dFechaDeposito;

	private bool bEstado;

	private bool bDeposito;

	private string sConcepto;

	private decimal dMonto;

	private DateTime dFecha;

	private int iCodAlmacen;

	private int iTipo;

	private int iCodPagoServicio;

	public int CodFlujoCaja
	{
		get
		{
			return iCodFlujoCaja;
		}
		set
		{
			iCodFlujoCaja = value;
		}
	}

	public int CodFlujoCajaNuevo
	{
		get
		{
			return iCodFlujoCajaNuevo;
		}
		set
		{
			iCodFlujoCajaNuevo = value;
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

	public DateTime FechaApertura
	{
		get
		{
			return dFechaApertura;
		}
		set
		{
			dFechaApertura = value;
		}
	}

	public decimal MontoApertura
	{
		get
		{
			return dMontoApertura;
		}
		set
		{
			dMontoApertura = value;
		}
	}

	public DateTime FechaCierre
	{
		get
		{
			return dFechaCierre;
		}
		set
		{
			dFechaCierre = value;
		}
	}

	public decimal MontoCierre
	{
		get
		{
			return dMontoCierre;
		}
		set
		{
			dMontoCierre = value;
		}
	}

	public decimal MontoIngresado
	{
		get
		{
			return dMontoIngresado;
		}
		set
		{
			dMontoIngresado = value;
		}
	}

	public decimal MontoDepositado
	{
		get
		{
			return dMontoDepositado;
		}
		set
		{
			dMontoDepositado = value;
		}
	}

	public decimal MontoDisponible
	{
		get
		{
			return dMontoDisponible;
		}
		set
		{
			dMontoDisponible = value;
		}
	}

	public DateTime FechaDeposito
	{
		get
		{
			return dFechaDeposito;
		}
		set
		{
			dFechaDeposito = value;
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

	public bool Deposito
	{
		get
		{
			return bDeposito;
		}
		set
		{
			bDeposito = value;
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

	public string Concepto
	{
		get
		{
			return sConcepto;
		}
		set
		{
			sConcepto = value;
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

	public DateTime Fecha
	{
		get
		{
			return dFecha;
		}
		set
		{
			dFecha = value;
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

	public int Tipo
	{
		get
		{
			return iTipo;
		}
		set
		{
			iTipo = value;
		}
	}

	public int CodPagoServicio
	{
		get
		{
			return iCodPagoServicio;
		}
		set
		{
			iCodPagoServicio = value;
		}
	}
}
