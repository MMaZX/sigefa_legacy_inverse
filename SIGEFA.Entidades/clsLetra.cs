using System;

namespace SIGEFA.Entidades;

public class clsLetra
{
	private int iCodLetra;

	private int iCodAlmacen;

	private int iCodDocumento;

	private string sSiglaDocumento;

	private int iCodSerie;

	private string sSerie;

	private string sNumDocumento;

	private int iCodNota;

	private string sDocumentoReferencia;

	private int iCodProveedor;

	private string sRazonSocialProveedor;

	private string sRucProveedor;

	private string sDireccionProveedor;

	private int iCodLiberado;

	private DateTime dtFechaEmision;

	private DateTime dtFechaVencimiento;

	private DateTime dtFechaCancelado;

	private bool bIngresoEgreso;

	private int iCodMoneda;

	private double dTipoCambio;

	private double dMonto;

	private double dMontoPendiente;

	private int iCodBanco;

	private string sNumeroUnico;

	private bool bCancelado;

	private bool bEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string sFechaCancelado2;

	private int codfactura;

	private int codfacturaventa;

	public int Codfacturaventa
	{
		get
		{
			return codfacturaventa;
		}
		set
		{
			codfacturaventa = value;
		}
	}

	public int Codfactura
	{
		get
		{
			return codfactura;
		}
		set
		{
			codfactura = value;
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

	public int CodDocumento
	{
		get
		{
			return iCodDocumento;
		}
		set
		{
			iCodDocumento = value;
		}
	}

	public string SiglaDocumento
	{
		get
		{
			return sSiglaDocumento;
		}
		set
		{
			sSiglaDocumento = value;
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

	public string NumDocumento
	{
		get
		{
			return sNumDocumento;
		}
		set
		{
			sNumDocumento = value;
		}
	}

	public int CodNota
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

	public string DocumentoReferencia
	{
		get
		{
			return sDocumentoReferencia;
		}
		set
		{
			sDocumentoReferencia = value;
		}
	}

	public int CodProveedor
	{
		get
		{
			return iCodProveedor;
		}
		set
		{
			iCodProveedor = value;
		}
	}

	public int CodLiberado
	{
		get
		{
			return iCodLiberado;
		}
		set
		{
			iCodLiberado = value;
		}
	}

	public string RazonSocialProveedor
	{
		get
		{
			return sRazonSocialProveedor;
		}
		set
		{
			sRazonSocialProveedor = value;
		}
	}

	public string RucProveedor
	{
		get
		{
			return sRucProveedor;
		}
		set
		{
			sRucProveedor = value;
		}
	}

	public string DireccionProveedor
	{
		get
		{
			return sDireccionProveedor;
		}
		set
		{
			sDireccionProveedor = value;
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

	public double TipoCambio
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

	public double Monto
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

	public double MontoPendiente
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

	public string NumeroUnico
	{
		get
		{
			return sNumeroUnico;
		}
		set
		{
			sNumeroUnico = value;
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

	public string FechaCancelado2
	{
		get
		{
			return sFechaCancelado2;
		}
		set
		{
			sFechaCancelado2 = value;
		}
	}
}
