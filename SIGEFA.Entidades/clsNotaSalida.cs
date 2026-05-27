using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsNotaSalida
{
	private string sCodNotaSalida;

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

	private double dMontoBruto;

	private double dPorcDscto;

	private double dMontoDscto;

	private double dIgv;

	private double dTotal;

	private double dAbonado;

	private double dPendiente;

	private int iEstado;

	private int iEntregado;

	private int iCancelado;

	private int iAnulado;

	private DateTime dtFechaCancelado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int iCodNotacredito;

	private int sDocumentoReferencia;

	private int iCodVendedor;

	private int iCodStand;

	private int iAceptado;

	private int iCodSucursal;

	private List<clsDetalleNotaSalida> lDetalle;

	private int icodVehiculoTransporte;

	private int icodConductor;

	private int icodalmacenreceptor;

	private string docref;

	private int iAplicada;

	private int iCodAplicada;

	private string sMotivo;

	private int iCodProveedor;

	private string sDocumentoNotaSalida;

	private string sDocumentoFacturaAplicada;

	private int codtransferencia;

	public int responsable { get; set; }

	public string area { get; set; }

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

	public string DocumentoFacturaAplicada
	{
		get
		{
			return sDocumentoFacturaAplicada;
		}
		set
		{
			sDocumentoFacturaAplicada = value;
		}
	}

	public string DocumentoNotaSalida
	{
		get
		{
			return sDocumentoNotaSalida;
		}
		set
		{
			sDocumentoNotaSalida = value;
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

	public int codalmacenreceptor
	{
		get
		{
			return icodalmacenreceptor;
		}
		set
		{
			icodalmacenreceptor = value;
		}
	}

	public int codVehiculoTransporte
	{
		get
		{
			return icodVehiculoTransporte;
		}
		set
		{
			icodVehiculoTransporte = value;
		}
	}

	public int codConductor
	{
		get
		{
			return icodConductor;
		}
		set
		{
			icodConductor = value;
		}
	}

	public string CodNotaSalida
	{
		get
		{
			return sCodNotaSalida;
		}
		set
		{
			sCodNotaSalida = value;
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

	public int DocumentoReferencia
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

	public int CodStand
	{
		get
		{
			return iCodStand;
		}
		set
		{
			iCodStand = value;
		}
	}

	public List<clsDetalleNotaSalida> Detalle
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

	public string Docref
	{
		get
		{
			return docref;
		}
		set
		{
			docref = value;
		}
	}
}
