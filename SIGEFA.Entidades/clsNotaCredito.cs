using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsNotaCredito
{
	private string iCodNotaCredito;

	private int iCodNotaCreditoNueva;

	private string sDocumentoNotaCredito;

	private int iCodAlmacen;

	private int iCodNotaIngreso;

	private int iCodTipoTransaccion;

	private string sSiglaTransaccion;

	private string sDescripcionTransaccion;

	private int iCodOrdenCompra;

	private int iCodTipoDocumento;

	private string sSiglaDocumento;

	private int iCodSerie;

	private string sSerie;

	private string sNumFac;

	private int iCodReferencia;

	private int iCodCliente;

	private string sRUCCliente;

	private string sRazonSocialCliente;

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

	private string sMotivo;

	private List<clsDetalleFactura> lDetalle;

	private int imovimientonc;

	public decimal icbper { get; set; }

	public int MovimientoNC
	{
		get
		{
			return imovimientonc;
		}
		set
		{
			imovimientonc = value;
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

	public string NumFac
	{
		get
		{
			return sNumFac;
		}
		set
		{
			sNumFac = value;
		}
	}

	public int CodCliente
	{
		get
		{
			return iCodCliente;
		}
		set
		{
			iCodCliente = value;
		}
	}

	public string RUCCliente
	{
		get
		{
			return sRUCCliente;
		}
		set
		{
			sRUCCliente = value;
		}
	}

	public string RazonSocialCliente
	{
		get
		{
			return sRazonSocialCliente;
		}
		set
		{
			sRazonSocialCliente = value;
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

	public List<clsDetalleFactura> Detalle
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

	public int CodNotaIngreso
	{
		get
		{
			return iCodNotaIngreso;
		}
		set
		{
			iCodNotaIngreso = value;
		}
	}

	public string CodNotaCredito
	{
		get
		{
			return iCodNotaCredito;
		}
		set
		{
			iCodNotaCredito = value;
		}
	}

	public string DocumentoNotaCredito
	{
		get
		{
			return sDocumentoNotaCredito;
		}
		set
		{
			sDocumentoNotaCredito = value;
		}
	}

	public int CodNotaCreditoNueva
	{
		get
		{
			return iCodNotaCreditoNueva;
		}
		set
		{
			iCodNotaCreditoNueva = value;
		}
	}

	public int Tipofacturacion { get; set; }

	public decimal Inafectas { get; set; }

	public decimal Gravadas { get; set; }

	public decimal Exoneradas { get; set; }

	public decimal Gratuitas { get; set; }

	public int CodEmpresa { get; set; }

	public DateTime FechaEmision { get; internal set; }

	public bool ProductoDestruido { get; internal set; }

	public string serieGRSP { get; internal set; }

	public string numdocGRSP { get; internal set; }

	public int CodProveedor { get; internal set; }

	public string codNotaSalida { get; internal set; }
}
