using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsPedido
{
	private string sCodPedido;

	private int iCodAlmacen;

	private int iCodTipoDocumento;

	private string sSiglaDocumento;

	private string sDescripcionDocumento;

	private int iCodCotizacion;

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

	private DateTime dtFechaPedido;

	private DateTime dtFechaEntrega;

	private int iCodAutorizado;

	private string sNombreAutorizado;

	private int iFormaPago;

	private DateTime dtFechaPago;

	private int iCodListaPrecio;

	private string sComentario;

	private decimal dMontoBruto;

	private decimal dPorcDscto;

	private decimal dMontoDscto;

	private decimal dIgv;

	private decimal dTotal;

	private int iEstado;

	private int iPendiente;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private List<clsDetallePedido> lDetalle;

	private string sEntregado;

	private int iEntregado;

	private int numeracion;

	private string nombrecliente;

	private int tipoventa;

	private decimal gravadas;

	private decimal gratuitas;

	private decimal exoneradas;

	private decimal inafectas;

	private string tipoImpuesto;

	private int boletafactura;

	private int codEmpresa;

	private int codSerie;

	private string serieDoc;

	private string codigoBarras;

	private string codigoBarrasCifrado;

	private int codVendedor;

	public int ventasinstock { get; set; }

	public decimal Icbper { get; set; }

	public string Nombrecliente
	{
		get
		{
			return nombrecliente;
		}
		set
		{
			nombrecliente = value;
		}
	}

	public int IEntregado
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

	public string SEntregado
	{
		get
		{
			return sEntregado;
		}
		set
		{
			sEntregado = value;
		}
	}

	public string CodPedido
	{
		get
		{
			return sCodPedido;
		}
		set
		{
			sCodPedido = value;
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

	public string DescripcionDocumento
	{
		get
		{
			return sDescripcionDocumento;
		}
		set
		{
			sDescripcionDocumento = value;
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

	public DateTime FechaPedido
	{
		get
		{
			return dtFechaPedido;
		}
		set
		{
			dtFechaPedido = value;
		}
	}

	public DateTime FechaEntrega
	{
		get
		{
			return dtFechaEntrega;
		}
		set
		{
			dtFechaEntrega = value;
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

	public int Pendiente
	{
		get
		{
			return iPendiente;
		}
		set
		{
			iPendiente = value;
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

	public List<clsDetallePedido> Detalle
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

	public int Numeracion
	{
		get
		{
			return numeracion;
		}
		set
		{
			numeracion = value;
		}
	}

	public int Tipoventa
	{
		get
		{
			return tipoventa;
		}
		set
		{
			tipoventa = value;
		}
	}

	public decimal Gravadas
	{
		get
		{
			return gravadas;
		}
		set
		{
			gravadas = value;
		}
	}

	public decimal Gratuitas
	{
		get
		{
			return gratuitas;
		}
		set
		{
			gratuitas = value;
		}
	}

	public decimal Exoneradas
	{
		get
		{
			return exoneradas;
		}
		set
		{
			exoneradas = value;
		}
	}

	public decimal Inafectas
	{
		get
		{
			return inafectas;
		}
		set
		{
			inafectas = value;
		}
	}

	public string TipoImpuesto
	{
		get
		{
			return tipoImpuesto;
		}
		set
		{
			tipoImpuesto = value;
		}
	}

	public int Boletafactura
	{
		get
		{
			return boletafactura;
		}
		set
		{
			boletafactura = value;
		}
	}

	public int CodEmpresa
	{
		get
		{
			return codEmpresa;
		}
		set
		{
			codEmpresa = value;
		}
	}

	public int CodSerie
	{
		get
		{
			return codSerie;
		}
		set
		{
			codSerie = value;
		}
	}

	public string SerieDoc
	{
		get
		{
			return serieDoc;
		}
		set
		{
			serieDoc = value;
		}
	}

	public string CodigoBarras
	{
		get
		{
			return codigoBarras;
		}
		set
		{
			codigoBarras = value;
		}
	}

	public string CodigoBarrasCifrado
	{
		get
		{
			return codigoBarrasCifrado;
		}
		set
		{
			codigoBarrasCifrado = value;
		}
	}

	public int CodVendedor
	{
		get
		{
			return codVendedor;
		}
		set
		{
			codVendedor = value;
		}
	}

	public int ValorRetencion { get; internal set; }

	public int idTecnico { get; internal set; }

	public int idZona { get; internal set; }

	public string CodCanalVenta { get; internal set; }
}
