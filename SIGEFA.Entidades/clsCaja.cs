using System;

namespace SIGEFA.Entidades;

public class clsCaja
{
	private int codcaja;

	private int codcajaNuevo;

	private int codsucursal;

	private int tipo;

	private decimal montoapertura;

	private decimal montocierre;

	private DateTime fechaapertura;

	private DateTime fechacierre;

	private decimal totalIngreso;

	private decimal totalEgreso;

	private decimal totalVentaEfectivo;

	private decimal totalDisponible;

	private int codUser;

	private bool estado;

	private decimal totalventacredito;

	private decimal totalcheque;

	private decimal totaltarnsferencia;

	private decimal totalcobranza;

	private decimal totaldeposito;

	private int codalmacen;

	private decimal totalPendiente;

	private decimal totalEfectivo;

	public decimal IngresoEfectivo { get; set; }

	public decimal IngresoTarjeta { get; set; }

	public decimal IngresoTransferencia { get; set; }

	public decimal EgresoEfectivo { get; set; }

	public int Codalmacen
	{
		get
		{
			return codalmacen;
		}
		set
		{
			codalmacen = value;
		}
	}

	public decimal Totaldeposito
	{
		get
		{
			return totaldeposito;
		}
		set
		{
			totaldeposito = value;
		}
	}

	public decimal Totalcobranza
	{
		get
		{
			return totalcobranza;
		}
		set
		{
			totalcobranza = value;
		}
	}

	public decimal Totaltarnsferencia
	{
		get
		{
			return totaltarnsferencia;
		}
		set
		{
			totaltarnsferencia = value;
		}
	}

	public decimal Totalcheque
	{
		get
		{
			return totalcheque;
		}
		set
		{
			totalcheque = value;
		}
	}

	public decimal Totalventacredito
	{
		get
		{
			return totalventacredito;
		}
		set
		{
			totalventacredito = value;
		}
	}

	public int Codcaja
	{
		get
		{
			return codcaja;
		}
		set
		{
			codcaja = value;
		}
	}

	public int CodcajaNuevo
	{
		get
		{
			return codcajaNuevo;
		}
		set
		{
			codcajaNuevo = value;
		}
	}

	public int Codsucursal
	{
		get
		{
			return codsucursal;
		}
		set
		{
			codsucursal = value;
		}
	}

	public int Tipo
	{
		get
		{
			return tipo;
		}
		set
		{
			tipo = value;
		}
	}

	public decimal Montoapertura
	{
		get
		{
			return montoapertura;
		}
		set
		{
			montoapertura = value;
		}
	}

	public decimal Montocierre
	{
		get
		{
			return montocierre;
		}
		set
		{
			montocierre = value;
		}
	}

	public DateTime Fechaapertura
	{
		get
		{
			return fechaapertura;
		}
		set
		{
			fechaapertura = value;
		}
	}

	public DateTime Fechacierre
	{
		get
		{
			return fechacierre;
		}
		set
		{
			fechacierre = value;
		}
	}

	public decimal TotalIngreso
	{
		get
		{
			return totalIngreso;
		}
		set
		{
			totalIngreso = value;
		}
	}

	public decimal TotalEgreso
	{
		get
		{
			return totalEgreso;
		}
		set
		{
			totalEgreso = value;
		}
	}

	public decimal TotalVentaEfectivo
	{
		get
		{
			return totalVentaEfectivo;
		}
		set
		{
			totalVentaEfectivo = value;
		}
	}

	public decimal TotalDisponible
	{
		get
		{
			return totalDisponible;
		}
		set
		{
			totalDisponible = value;
		}
	}

	public int CodUser
	{
		get
		{
			return codUser;
		}
		set
		{
			codUser = value;
		}
	}

	public bool Estado
	{
		get
		{
			return estado;
		}
		set
		{
			estado = value;
		}
	}

	public decimal TotalPendiente
	{
		get
		{
			return totalPendiente;
		}
		set
		{
			totalPendiente = value;
		}
	}

	public decimal TotalEfectivo
	{
		get
		{
			return totalEfectivo;
		}
		set
		{
			totalEfectivo = value;
		}
	}
}
