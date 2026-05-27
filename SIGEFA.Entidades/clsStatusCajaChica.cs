namespace SIGEFA.Entidades;

public class clsStatusCajaChica
{
	private decimal dAperturaCaja;

	private decimal dSumaAperturasCaja;

	private decimal dSumaCierresCaja;

	private decimal dIngresos;

	private decimal dEgresos;

	private decimal dTotalVentas;

	private decimal dPorCobrar;

	private decimal dTotalPagos;

	private decimal dPorPagar;

	private int iCodAlmacen;

	private int iCantidad;

	private decimal dSumaVentasEfectivoDia;

	private decimal dSumaVentasDepositoDia;

	private decimal dSumaVentasChequeDia;

	private decimal dSumaVentasTransferenciaDia;

	private decimal dSumaVentasTarjetaDia;

	private decimal dSumaVentasEfectivoMes;

	private decimal dSumaVentasDepositoMes;

	private decimal dSumaVentasChequeMes;

	private decimal dSumaVentasTransferenciaMes;

	private decimal dSumaVentasTarjetaMes;

	private decimal dVentasCreditoDia;

	private decimal dVentasContadoDia;

	private decimal dVentasCreditoMes;

	private decimal dVentasContadoMes;

	private decimal dTotalVentasMes;

	private decimal dPorCobrarMes;

	private decimal dMontoDepositado;

	private int iCodSucursal;

	private decimal dSumaVentasEfectivoDiaDolar;

	private decimal dSumaVentasDepositoDiaDolar;

	private decimal dSumaVentasChequeDiaDolar;

	private decimal dSumaVentasTransferenciaDiaDolar;

	private decimal dSumaVentasTarjetaDiaDolar;

	private decimal dSumaVentasEfectivoMesDolar;

	private decimal dSumaVentasDepositoMesDolar;

	private decimal dSumaVentasChequeMesDolar;

	private decimal dSumaVentasTransferenciaMesDolar;

	private decimal dSumaVentasTarjetaMesDolar;

	private decimal dVentasCreditoDiaDolar;

	private decimal dVentasContadoDiaDolar;

	private decimal dVentasCreditoMesDolar;

	private decimal dVentasContadoMesDolar;

	private decimal dTotalVentasMesDolar;

	private decimal dPorCobrarMesDolar;

	private decimal dTotalVentasDolar;

	private decimal dPorCobrarDolar;

	private decimal dTotalPagosDolar;

	private decimal dPorPagarDolar;

	private decimal dIngresosDolar;

	private decimal dEgresosDolar;

	private decimal dCobranzaSolesTotal;

	private decimal dCobranzaDolaresTotal;

	public decimal CobranzaDolaresTotal
	{
		get
		{
			return dCobranzaDolaresTotal;
		}
		set
		{
			dCobranzaDolaresTotal = value;
		}
	}

	public decimal CobranzaSolesTotal
	{
		get
		{
			return dCobranzaSolesTotal;
		}
		set
		{
			dCobranzaSolesTotal = value;
		}
	}

	public decimal AperturaCaja
	{
		get
		{
			return dAperturaCaja;
		}
		set
		{
			dAperturaCaja = value;
		}
	}

	public decimal SumaAperturasCaja
	{
		get
		{
			return dSumaAperturasCaja;
		}
		set
		{
			dSumaAperturasCaja = value;
		}
	}

	public decimal SumaCierresCaja
	{
		get
		{
			return dSumaCierresCaja;
		}
		set
		{
			dSumaCierresCaja = value;
		}
	}

	public decimal Ingresos
	{
		get
		{
			return dIngresos;
		}
		set
		{
			dIngresos = value;
		}
	}

	public decimal Egresos
	{
		get
		{
			return dEgresos;
		}
		set
		{
			dEgresos = value;
		}
	}

	public decimal TotalVentas
	{
		get
		{
			return dTotalVentas;
		}
		set
		{
			dTotalVentas = value;
		}
	}

	public decimal PorCobrar
	{
		get
		{
			return dPorCobrar;
		}
		set
		{
			dPorCobrar = value;
		}
	}

	public decimal TotalPagos
	{
		get
		{
			return dTotalPagos;
		}
		set
		{
			dTotalPagos = value;
		}
	}

	public decimal PorPagar
	{
		get
		{
			return dPorPagar;
		}
		set
		{
			dPorPagar = value;
		}
	}

	public int Cantidad
	{
		get
		{
			return iCantidad;
		}
		set
		{
			iCantidad = value;
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

	public decimal SumaVentasEfectivoDia
	{
		get
		{
			return dSumaVentasEfectivoDia;
		}
		set
		{
			dSumaVentasEfectivoDia = value;
		}
	}

	public decimal SumaVentasDepositoDia
	{
		get
		{
			return dSumaVentasDepositoDia;
		}
		set
		{
			dSumaVentasDepositoDia = value;
		}
	}

	public decimal SumaVentasChequeDia
	{
		get
		{
			return dSumaVentasChequeDia;
		}
		set
		{
			dSumaVentasChequeDia = value;
		}
	}

	public decimal SumaVentasTransferenciaDia
	{
		get
		{
			return dSumaVentasTransferenciaDia;
		}
		set
		{
			dSumaVentasTransferenciaDia = value;
		}
	}

	public decimal SumaVentasTarjetaDia
	{
		get
		{
			return dSumaVentasTarjetaDia;
		}
		set
		{
			dSumaVentasTarjetaDia = value;
		}
	}

	public decimal SumaVentasEfectivoMes
	{
		get
		{
			return dSumaVentasEfectivoMes;
		}
		set
		{
			dSumaVentasEfectivoMes = value;
		}
	}

	public decimal SumaVentasDepositoMes
	{
		get
		{
			return dSumaVentasDepositoMes;
		}
		set
		{
			dSumaVentasDepositoMes = value;
		}
	}

	public decimal SumaVentasChequeMes
	{
		get
		{
			return dSumaVentasChequeMes;
		}
		set
		{
			dSumaVentasChequeMes = value;
		}
	}

	public decimal SumaVentasTransferenciaMes
	{
		get
		{
			return dSumaVentasTransferenciaMes;
		}
		set
		{
			dSumaVentasTransferenciaMes = value;
		}
	}

	public decimal SumaVentasTarjetaMes
	{
		get
		{
			return dSumaVentasTarjetaMes;
		}
		set
		{
			dSumaVentasTarjetaMes = value;
		}
	}

	public decimal VentasCreditoDia
	{
		get
		{
			return dVentasCreditoDia;
		}
		set
		{
			dVentasCreditoDia = value;
		}
	}

	public decimal VentasContadoDia
	{
		get
		{
			return dVentasContadoDia;
		}
		set
		{
			dVentasContadoDia = value;
		}
	}

	public decimal VentasCreditoMes
	{
		get
		{
			return dVentasCreditoMes;
		}
		set
		{
			dVentasCreditoMes = value;
		}
	}

	public decimal VentasContadoMes
	{
		get
		{
			return dVentasContadoMes;
		}
		set
		{
			dVentasContadoMes = value;
		}
	}

	public decimal TotalVentasMes
	{
		get
		{
			return dTotalVentasMes;
		}
		set
		{
			dTotalVentasMes = value;
		}
	}

	public decimal PorCobrarMes
	{
		get
		{
			return dPorCobrarMes;
		}
		set
		{
			dPorCobrarMes = value;
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

	public decimal SumaVentasEfectivoDiaDolar
	{
		get
		{
			return dSumaVentasEfectivoDiaDolar;
		}
		set
		{
			dSumaVentasEfectivoDiaDolar = value;
		}
	}

	public decimal SumaVentasDepositoDiaDolar
	{
		get
		{
			return dSumaVentasDepositoDiaDolar;
		}
		set
		{
			dSumaVentasDepositoDiaDolar = value;
		}
	}

	public decimal SumaVentasChequeDiaDolar
	{
		get
		{
			return dSumaVentasChequeDiaDolar;
		}
		set
		{
			dSumaVentasChequeDiaDolar = value;
		}
	}

	public decimal SumaVentasTransferenciaDiaDolar
	{
		get
		{
			return dSumaVentasTransferenciaDiaDolar;
		}
		set
		{
			dSumaVentasTransferenciaDiaDolar = value;
		}
	}

	public decimal SumaVentasTarjetaDiaDolar
	{
		get
		{
			return dSumaVentasTarjetaDiaDolar;
		}
		set
		{
			dSumaVentasTarjetaDiaDolar = value;
		}
	}

	public decimal SumaVentasEfectivoMesDolar
	{
		get
		{
			return dSumaVentasEfectivoMesDolar;
		}
		set
		{
			dSumaVentasEfectivoMesDolar = value;
		}
	}

	public decimal SumaVentasDepositoMesDolar
	{
		get
		{
			return dSumaVentasDepositoMesDolar;
		}
		set
		{
			dSumaVentasDepositoMesDolar = value;
		}
	}

	public decimal SumaVentasChequeMesDolar
	{
		get
		{
			return dSumaVentasChequeMesDolar;
		}
		set
		{
			dSumaVentasChequeMesDolar = value;
		}
	}

	public decimal SumaVentasTransferenciaMesDolar
	{
		get
		{
			return dSumaVentasTransferenciaMesDolar;
		}
		set
		{
			dSumaVentasTransferenciaMesDolar = value;
		}
	}

	public decimal SumaVentasTarjetaMesDolar
	{
		get
		{
			return dSumaVentasTarjetaMesDolar;
		}
		set
		{
			dSumaVentasTarjetaMesDolar = value;
		}
	}

	public decimal VentasCreditoDiaDolar
	{
		get
		{
			return dVentasCreditoDiaDolar;
		}
		set
		{
			dVentasCreditoDiaDolar = value;
		}
	}

	public decimal VentasContadoDiaDolar
	{
		get
		{
			return dVentasContadoDiaDolar;
		}
		set
		{
			dVentasContadoDiaDolar = value;
		}
	}

	public decimal VentasCreditoMesDolar
	{
		get
		{
			return dVentasCreditoMesDolar;
		}
		set
		{
			dVentasCreditoMesDolar = value;
		}
	}

	public decimal VentasContadoMesDolar
	{
		get
		{
			return dVentasContadoMesDolar;
		}
		set
		{
			dVentasContadoMesDolar = value;
		}
	}

	public decimal TotalVentasMesDolar
	{
		get
		{
			return dTotalVentasMesDolar;
		}
		set
		{
			dTotalVentasMesDolar = value;
		}
	}

	public decimal PorCobrarMesDolar
	{
		get
		{
			return dPorCobrarMesDolar;
		}
		set
		{
			dPorCobrarMesDolar = value;
		}
	}

	public decimal TotalVentasDolar
	{
		get
		{
			return dTotalVentasDolar;
		}
		set
		{
			dTotalVentasDolar = value;
		}
	}

	public decimal PorCobrarDolar
	{
		get
		{
			return dPorCobrarDolar;
		}
		set
		{
			dPorCobrarDolar = value;
		}
	}

	public decimal TotalPagosDolar
	{
		get
		{
			return dTotalPagosDolar;
		}
		set
		{
			dTotalPagosDolar = value;
		}
	}

	public decimal PorPagarDolar
	{
		get
		{
			return dPorPagarDolar;
		}
		set
		{
			dPorPagarDolar = value;
		}
	}

	public decimal IngresosDolar
	{
		get
		{
			return dIngresosDolar;
		}
		set
		{
			dIngresosDolar = value;
		}
	}

	public decimal EgresosDolar
	{
		get
		{
			return dEgresosDolar;
		}
		set
		{
			dEgresosDolar = value;
		}
	}
}
