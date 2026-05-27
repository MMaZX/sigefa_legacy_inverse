using System;

namespace SIGEFA.Entidades;

public class clsPago
{
	private int iCodPago;

	private string iCodNota;

	private int iCodLetra;

	private int iCodCuotaPreBan;

	private int iCodTipoPago;

	private int iCodMoneda;

	private int iCodCobrador;

	private bool bTipo;

	private bool bIngresoEgreso;

	private decimal dTipoCambio;

	private decimal dMontoPagado;

	private decimal dMontoCobrado;

	private decimal dVuelto;

	private decimal dMora;

	private string sNOperacion;

	private string sNCheque;

	private DateTime dtFechaPago;

	private string sObservacion;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int iCodAlmacen;

	private int iCodSerie;

	private string sSerie;

	private string sNumDoc;

	private int iCodDoc;

	private string sSiglaDoc;

	private int iCodSucursal;

	private bool bProvision;

	private bool bPendiente;

	private int codcaja;

	private int iCodMetPago;

	private int iCodBanco;

	private int iCodTarjeta;

	private int icodCtaCte;

	private string sCtaCte;

	private int iAprobado;

	private string sReferencia;

	private int iNotaCredito;

	private int icodNotaCredito;

	private decimal retDet;

	private string bandRetDet;

	private decimal montoEnCuenta;

	private int opcionSuma = 0;

	private int tipoDescripcion;

	public int OpcionSuma
	{
		get
		{
			return opcionSuma;
		}
		set
		{
			opcionSuma = value;
		}
	}

	public decimal RetDet
	{
		get
		{
			return retDet;
		}
		set
		{
			retDet = value;
		}
	}

	public string BanderaRetDet
	{
		get
		{
			return bandRetDet;
		}
		set
		{
			bandRetDet = value;
		}
	}

	public decimal MontoEnCuenta
	{
		get
		{
			return montoEnCuenta;
		}
		set
		{
			montoEnCuenta = value;
		}
	}

	public string accion { get; set; }

	public int CodNotaCredito
	{
		get
		{
			return icodNotaCredito;
		}
		set
		{
			icodNotaCredito = value;
		}
	}

	public int NotaCredito
	{
		get
		{
			return iNotaCredito;
		}
		set
		{
			iNotaCredito = value;
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

	public string Referencia
	{
		get
		{
			return sReferencia;
		}
		set
		{
			sReferencia = value;
		}
	}

	public int Aprobado
	{
		get
		{
			return iAprobado;
		}
		set
		{
			iAprobado = value;
		}
	}

	public bool Provision
	{
		get
		{
			return bProvision;
		}
		set
		{
			bProvision = value;
		}
	}

	public bool Pendiente
	{
		get
		{
			return bPendiente;
		}
		set
		{
			bPendiente = value;
		}
	}

	public int CodPago
	{
		get
		{
			return iCodPago;
		}
		set
		{
			iCodPago = value;
		}
	}

	public string CodNota
	{
		get
		{
			return iCodNota;
		}
		set
		{
			iCodNota = value;
		}
	}

	public int CodLetra
	{
		get
		{
			return iCodLetra;
		}
		set
		{
			iCodLetra = value;
		}
	}

	public int CodCuotaPreBan
	{
		get
		{
			return iCodCuotaPreBan;
		}
		set
		{
			iCodCuotaPreBan = value;
		}
	}

	public int CodTipoPago
	{
		get
		{
			return iCodTipoPago;
		}
		set
		{
			iCodTipoPago = value;
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

	public int CodCobrador
	{
		get
		{
			return iCodCobrador;
		}
		set
		{
			iCodCobrador = value;
		}
	}

	public bool Tipo
	{
		get
		{
			return bTipo;
		}
		set
		{
			bTipo = value;
		}
	}

	public bool IngresoEgreso
	{
		get
		{
			return bIngresoEgreso;
		}
		set
		{
			bIngresoEgreso = value;
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

	public decimal MontoPagado
	{
		get
		{
			return dMontoPagado;
		}
		set
		{
			dMontoPagado = value;
		}
	}

	public decimal MontoCobrado
	{
		get
		{
			return dMontoCobrado;
		}
		set
		{
			dMontoCobrado = value;
		}
	}

	public decimal Vuelto
	{
		get
		{
			return dVuelto;
		}
		set
		{
			dVuelto = value;
		}
	}

	public decimal Mora
	{
		get
		{
			return dMora;
		}
		set
		{
			dMora = value;
		}
	}

	public string NOperacion
	{
		get
		{
			return sNOperacion;
		}
		set
		{
			sNOperacion = value;
		}
	}

	public string NCheque
	{
		get
		{
			return sNCheque;
		}
		set
		{
			sNCheque = value;
		}
	}

	public DateTime FechaPago
	{
		get
		{
			return dtFechaPago;
		}
		set
		{
			dtFechaPago = value;
		}
	}

	public string Observacion
	{
		get
		{
			return sObservacion;
		}
		set
		{
			sObservacion = value;
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

	public int CodMetPago
	{
		get
		{
			return iCodMetPago;
		}
		set
		{
			iCodMetPago = value;
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

	public int CodTarjeta
	{
		get
		{
			return iCodTarjeta;
		}
		set
		{
			iCodTarjeta = value;
		}
	}

	public int codCtaCte
	{
		get
		{
			return icodCtaCte;
		}
		set
		{
			icodCtaCte = value;
		}
	}

	public string CtaCte
	{
		get
		{
			return sCtaCte;
		}
		set
		{
			sCtaCte = value;
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

	public int CodSerie
	{
		get
		{
			return iCodSerie;
		}
		set
		{
			iCodSerie = value;
		}
	}

	public string NumDoc
	{
		get
		{
			return sNumDoc;
		}
		set
		{
			sNumDoc = value;
		}
	}

	public string Serie
	{
		get
		{
			return sSerie;
		}
		set
		{
			sSerie = value;
		}
	}

	public int CodDoc
	{
		get
		{
			return iCodDoc;
		}
		set
		{
			iCodDoc = value;
		}
	}

	public string SiglaDoc
	{
		get
		{
			return sSiglaDoc;
		}
		set
		{
			sSiglaDoc = value;
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

	public int TipoDescripcion
	{
		get
		{
			return tipoDescripcion;
		}
		set
		{
			tipoDescripcion = value;
		}
	}
}
