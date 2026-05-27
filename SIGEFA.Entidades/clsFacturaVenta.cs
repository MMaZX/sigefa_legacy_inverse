using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsFacturaVenta
{
	private string sCodFacturaVenta;

	private int sCodSucursal;

	private int iCodAlmacen;

	private int iCodTipoTransaccion;

	private string sSiglaTransaccion;

	private string sDescripcionTransaccion;

	private int iCodPedido;

	private int iCodTipoDocumento;

	private int iCodListaPrecio;

	private string sSiglaDocumento;

	private int iCodSerie;

	private string sSerie;

	private string sNumDoc;

	private int iTipoCliente;

	private int iCodCliente;

	private string sRUCCliente;

	private string sDNI;

	private string sCodigoPersonalizado;

	private string sRazonSocialCliente;

	private string sNombre;

	private string sDireccion;

	private int iMoneda;

	private double dTipoCambio;

	private DateTime dtFechaSalida;

	private int iCodAutorizado;

	private string sNombreAutorizado;

	private int iFormaPago;

	private DateTime dtFechaPago;

	private string sComentario;

	private decimal dMontoBruto;

	private decimal dPorcDscto;

	private decimal dMontoDscto;

	private decimal dIgv;

	private decimal dTotal;

	private decimal dAbonado;

	private decimal dPendiente;

	private int iEstado;

	private int iEntregado;

	private int iCancelado;

	private int iAnulado;

	private DateTime dtFechaCancelado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int iCodNotacredito;

	private string sDocumentoReferencia;

	private int iCodVendedor;

	private int iCodCotizacion;

	private bool bImpreso;

	private double dLineaCreditoCliente;

	private List<clsDetalleFacturaVenta> lDetalle;

	private int codsalidaconsulext;

	private bool consultorext;

	private string sMotivo;

	private string detallecomentario;

	private int codventaentregar;

	private int iCodSeparacion;

	private int iCodVenta;

	private byte[] qr;

	private string sNumeroDocumentoCliente;

	public int CodigoDocumentoIdentidad { get; set; }

	public bool bUsaFechaActual { get; set; }

	public clsDocumentoIdentidad DocumentoIdentidad { get; set; }

	public int ventasinstock { get; set; }

	public decimal icbper { get; set; }

	public clsEmpresa empresa { get; set; }

	public string NumeroDocumentoCliente
	{
		get
		{
			return sNumeroDocumentoCliente;
		}
		set
		{
			sNumeroDocumentoCliente = value;
		}
	}

	public int CodVenta
	{
		get
		{
			return iCodVenta;
		}
		set
		{
			iCodVenta = value;
		}
	}

	public int CodSeparacion
	{
		get
		{
			return iCodSeparacion;
		}
		set
		{
			iCodSeparacion = value;
		}
	}

	public int Codventaentregar
	{
		get
		{
			return codventaentregar;
		}
		set
		{
			codventaentregar = value;
		}
	}

	public string Detallecomentario
	{
		get
		{
			return detallecomentario;
		}
		set
		{
			detallecomentario = value;
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

	public int valorRetencion { get; internal set; }

	public bool Consultorext
	{
		get
		{
			return consultorext;
		}
		set
		{
			consultorext = value;
		}
	}

	public int Codsalidaconsulext
	{
		get
		{
			return codsalidaconsulext;
		}
		set
		{
			codsalidaconsulext = value;
		}
	}

	public string CodFacturaVenta
	{
		get
		{
			return sCodFacturaVenta;
		}
		set
		{
			sCodFacturaVenta = value;
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

	public int CodPedido
	{
		get
		{
			return iCodPedido;
		}
		set
		{
			iCodPedido = value;
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

	public int CodListaPrecio
	{
		get
		{
			return iCodListaPrecio;
		}
		set
		{
			iCodListaPrecio = value;
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

	public int TipoCliente
	{
		get
		{
			return iTipoCliente;
		}
		set
		{
			iTipoCliente = value;
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

	public string DNI
	{
		get
		{
			return sDNI;
		}
		set
		{
			sDNI = value;
		}
	}

	public string CodigoPersonalizado
	{
		get
		{
			return sCodigoPersonalizado;
		}
		set
		{
			sCodigoPersonalizado = value;
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

	public string Nombre
	{
		get
		{
			return sNombre;
		}
		set
		{
			sNombre = value;
		}
	}

	public string Direccion
	{
		get
		{
			return sDireccion;
		}
		set
		{
			sDireccion = value;
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

	public DateTime FechaSalida
	{
		get
		{
			return dtFechaSalida;
		}
		set
		{
			dtFechaSalida = value;
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

	public decimal MontoBruto
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

	public decimal PorcDscto
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

	public decimal MontoDscto
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

	public decimal Igv
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

	public decimal Total
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

	public decimal Abonado
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

	public int Entregado
	{
		get
		{
			return iEntregado;
		}
		set
		{
			iEntregado = value;
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

	public int Anulado
	{
		get
		{
			return iAnulado;
		}
		set
		{
			iAnulado = value;
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

	public int CodNotaCredito
	{
		get
		{
			return iCodNotacredito;
		}
		set
		{
			iCodNotacredito = value;
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

	public int CodVendedor
	{
		get
		{
			return iCodVendedor;
		}
		set
		{
			iCodVendedor = value;
		}
	}

	public List<clsDetalleFacturaVenta> Detalle
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

	public int CodSucursal
	{
		get
		{
			return sCodSucursal;
		}
		set
		{
			sCodSucursal = value;
		}
	}

	public int CodCotizacion
	{
		get
		{
			return iCodCotizacion;
		}
		set
		{
			iCodCotizacion = value;
		}
	}

	public double LineaCreditoCliente
	{
		get
		{
			return dLineaCreditoCliente;
		}
		set
		{
			dLineaCreditoCliente = value;
		}
	}

	public bool Impreso
	{
		get
		{
			return bImpreso;
		}
		set
		{
			bImpreso = value;
		}
	}

	public string CodigoBarras { get; set; }

	public string TipoDocumentoAnticipo { get; set; }

	public string DocumentoReferenciaAnticipo { get; set; }

	public decimal MontoAnticipo { get; set; }

	public int Tipoventa { get; set; }

	public int CodEmpresa { get; set; }

	public decimal Gratuitas { get; set; }

	public decimal Gravadas { get; set; }

	public decimal Exoneradas { get; set; }

	public decimal Inafectas { get; set; }

	public int Boletafactura { get; set; }

	public string CodigoBarrasCifrado { get; set; }

	public byte[] Qr
	{
		get
		{
			return qr;
		}
		set
		{
			qr = value;
		}
	}

	public string Doc_Cliente { get; set; }

	public int idTecnico { get; internal set; }

	public int idZona { get; internal set; }

	public string CodCanalVenta { get; internal set; }

	public string TieneNotaCredito { get; internal set; }

	public string UsuarioAnulador { get; internal set; }

	public int codordencompra { get; set; }

	public clsFacturaVenta()
	{
		DocumentoIdentidad = new clsDocumentoIdentidad();
	}
}
