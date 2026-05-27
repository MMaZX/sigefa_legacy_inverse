using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsNotaIngreso
{
	private string sCodNotaIngreso;

	private int iCodAlmacen;

	private int iCodTipoTransaccion;

	private string sSiglaTransaccion;

	private string sDescripcionTransaccion;

	private int iCodOrdenCompra;

	private int iCodTipoDocumento;

	private string sSiglaDocumento;

	private int iCodSerie;

	private string sSerie;

	private string sNumDoc;

	private int iCodReferencia;

	private int iCodProveedor;

	private string sRUCProveedor;

	private string sRazonSocialProveedor;

	private int iMoneda;

	private double dTipoCambio;

	private DateTime dtFechaIngreso;

	private int iCodAutorizado;

	private string sNombreAutorizado;

	private int iFormaPago;

	private DateTime dtFechaPago;

	private string sComentario;

	private double dMontoBruto;

	private double dPorcDscto;

	private double dMontoDscto;

	private double dIgv;

	private double dFlete;

	private double dTotal;

	private double dAbonado;

	private double dPendiente;

	private int iEstado;

	private int iRecibido;

	private int iCancelado;

	private DateTime dtFechaCancelado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string sDocumentoOrden;

	private List<clsDetalleNotaIngreso> lDetalle;

	private int iAceptado;

	private int iordenOC;

	private int icodalmacenemisor;

	private int iCodconductor;

	private int iCodvehiculotransporte;

	private int iAplicada;

	private int iCodAplicada;

	private string sMotivo;

	private int codnotai;

	private int codtransferencia;

	private int iMovimientoNC;

	public int DocumentoReferencia { get; set; }

	public DateTime fechaingresoalmacen { get; set; }

	public int responsable { get; set; }

	public string area { get; set; }

	public int codsucu { get; set; }

	public int codguia { get; set; }

	public int MovimientoNC
	{
		get
		{
			return iMovimientoNC;
		}
		set
		{
			iMovimientoNC = value;
		}
	}

	public int Codtransferencia
	{
		get
		{
			return codtransferencia;
		}
		set
		{
			codtransferencia = value;
		}
	}

	public int Codnotai
	{
		get
		{
			return codnotai;
		}
		set
		{
			codnotai = value;
		}
	}

	public string Motivo
	{
		get
		{
			return sMotivo;
		}
		set
		{
			sMotivo = value;
		}
	}

	public int CodAplicada
	{
		get
		{
			return iCodAplicada;
		}
		set
		{
			iCodAplicada = value;
		}
	}

	public int Aplicada
	{
		get
		{
			return iAplicada;
		}
		set
		{
			iAplicada = value;
		}
	}

	public int Codconductor
	{
		get
		{
			return iCodconductor;
		}
		set
		{
			iCodconductor = value;
		}
	}

	public int Codvehiculotransporte
	{
		get
		{
			return iCodvehiculotransporte;
		}
		set
		{
			iCodvehiculotransporte = value;
		}
	}

	public int codalmacenemisor
	{
		get
		{
			return icodalmacenemisor;
		}
		set
		{
			icodalmacenemisor = value;
		}
	}

	public string CodNotaIngreso
	{
		get
		{
			return sCodNotaIngreso;
		}
		set
		{
			sCodNotaIngreso = value;
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

	public int CodTipoTransaccion
	{
		get
		{
			return iCodTipoTransaccion;
		}
		set
		{
			iCodTipoTransaccion = value;
		}
	}

	public string SiglaTransaccion
	{
		get
		{
			return sSiglaTransaccion;
		}
		set
		{
			sSiglaTransaccion = value;
		}
	}

	public string DescripcionTransaccion
	{
		get
		{
			return sDescripcionTransaccion;
		}
		set
		{
			sDescripcionTransaccion = value;
		}
	}

	public int CodOrdenCompra
	{
		get
		{
			return iCodOrdenCompra;
		}
		set
		{
			iCodOrdenCompra = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return iCodTipoDocumento;
		}
		set
		{
			iCodTipoDocumento = value;
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

	public string RUCProveedor
	{
		get
		{
			return sRUCProveedor;
		}
		set
		{
			sRUCProveedor = value;
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

	public int Moneda
	{
		get
		{
			return iMoneda;
		}
		set
		{
			iMoneda = value;
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

	public DateTime FechaIngreso
	{
		get
		{
			return dtFechaIngreso;
		}
		set
		{
			dtFechaIngreso = value;
		}
	}

	public int CodAutorizado
	{
		get
		{
			return iCodAutorizado;
		}
		set
		{
			iCodAutorizado = value;
		}
	}

	public string NombreAutorizado
	{
		get
		{
			return sNombreAutorizado;
		}
		set
		{
			sNombreAutorizado = value;
		}
	}

	public int FormaPago
	{
		get
		{
			return iFormaPago;
		}
		set
		{
			iFormaPago = value;
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

	public string Comentario
	{
		get
		{
			return sComentario;
		}
		set
		{
			sComentario = value;
		}
	}

	public double MontoBruto
	{
		get
		{
			return dMontoBruto;
		}
		set
		{
			dMontoBruto = value;
		}
	}

	public double PorcDscto
	{
		get
		{
			return dPorcDscto;
		}
		set
		{
			dPorcDscto = value;
		}
	}

	public double MontoDscto
	{
		get
		{
			return dMontoDscto;
		}
		set
		{
			dMontoDscto = value;
		}
	}

	public double Igv
	{
		get
		{
			return dIgv;
		}
		set
		{
			dIgv = value;
		}
	}

	public double Flete
	{
		get
		{
			return dFlete;
		}
		set
		{
			dFlete = value;
		}
	}

	public double Total
	{
		get
		{
			return dTotal;
		}
		set
		{
			dTotal = value;
		}
	}

	public double Pendiente
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

	public double Abonado
	{
		get
		{
			return dAbonado;
		}
		set
		{
			dAbonado = value;
		}
	}

	public int Estado
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

	public int Recibido
	{
		get
		{
			return iRecibido;
		}
		set
		{
			iRecibido = value;
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

	public int CodReferencia
	{
		get
		{
			return iCodReferencia;
		}
		set
		{
			iCodReferencia = value;
		}
	}

	public List<clsDetalleNotaIngreso> Detalle
	{
		get
		{
			return lDetalle;
		}
		set
		{
			lDetalle = value;
		}
	}

	public string SDocumentoOrden
	{
		get
		{
			return sDocumentoOrden;
		}
		set
		{
			sDocumentoOrden = value;
		}
	}

	public int Aceptado
	{
		get
		{
			return iAceptado;
		}
		set
		{
			iAceptado = value;
		}
	}

	public int ordenOC
	{
		get
		{
			return iordenOC;
		}
		set
		{
			iordenOC = value;
		}
	}
}
