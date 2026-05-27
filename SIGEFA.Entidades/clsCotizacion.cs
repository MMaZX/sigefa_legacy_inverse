using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsCotizacion
{
	private string sCodCotizacion;

	private int sCodSucursal;

	private int iCodAlmacen;

	private int iTipoCliente;

	private int iCodCliente;

	private string sRUCCliente;

	private string sLugarEntrega;

	private string sDNI;

	private string sCodigoPersonalizado;

	private string sRazonSocialCliente;

	private string sNombre;

	private string sDireccion;

	private decimal dLineaCredito;

	private int iDocRef;

	private string sSiglaDocRef;

	private int iMoneda;

	private decimal dTipoCambio;

	private DateTime dtFechaCotizacion;

	private int iCodAutorizado;

	private string sNombreAutorizado;

	private int iFormaPago;

	private int iCodListaPrecio;

	private DateTime dtFechaPago;

	private int iVigencia;

	private DateTime dtFechaVigencia;

	private string sComentario;

	private decimal dMontoBruto;

	private decimal dPorcDscto;

	private decimal dMontoDscto;

	private decimal dIgv;

	private decimal dTotal;

	private int iEstado;

	private int iPendiente;

	private int iAnulado;

	private int iVigente;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private decimal dLineaCreditoUso;

	private decimal dLineaCreditoDisponible;

	private bool ValidaDescuentos;

	private decimal margendegananciamonto;

	private decimal margendegananciaporcentaje;

	private List<clsDetalleCotizacion> lDetalle;

	public int codserie { get; set; }

	public string serie { get; set; }

	public int correlativo { get; set; }

	public string CodCotizacion
	{
		get
		{
			return sCodCotizacion;
		}
		set
		{
			sCodCotizacion = value;
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

	public DateTime FechaCotizacion
	{
		get
		{
			return dtFechaCotizacion;
		}
		set
		{
			dtFechaCotizacion = value;
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

	public int Vigencia
	{
		get
		{
			return iVigencia;
		}
		set
		{
			iVigencia = value;
		}
	}

	public DateTime FechaVigencia
	{
		get
		{
			return dtFechaVigencia;
		}
		set
		{
			dtFechaVigencia = value;
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

	public List<clsDetalleCotizacion> Detalle
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

	public int DocRef
	{
		get
		{
			return iDocRef;
		}
		set
		{
			iDocRef = value;
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

	public string SiglaDocRef
	{
		get
		{
			return sSiglaDocRef;
		}
		set
		{
			sSiglaDocRef = value;
		}
	}

	public int Vigente
	{
		get
		{
			return iVigente;
		}
		set
		{
			iVigente = value;
		}
	}

	public decimal LineaCredito
	{
		get
		{
			return dLineaCredito;
		}
		set
		{
			dLineaCredito = value;
		}
	}

	public decimal LineaCreditoUso
	{
		get
		{
			return dLineaCreditoUso;
		}
		set
		{
			dLineaCreditoUso = value;
		}
	}

	public decimal LineaCreditoDisponible
	{
		get
		{
			return dLineaCreditoDisponible;
		}
		set
		{
			dLineaCreditoDisponible = value;
		}
	}

	public string SLugarEntrega
	{
		get
		{
			return sLugarEntrega;
		}
		set
		{
			sLugarEntrega = value;
		}
	}

	public bool ValidaDescuentos1
	{
		get
		{
			return ValidaDescuentos;
		}
		set
		{
			ValidaDescuentos = value;
		}
	}

	public decimal Margendegananciamonto
	{
		get
		{
			return margendegananciamonto;
		}
		set
		{
			margendegananciamonto = value;
		}
	}

	public decimal Margendegananciaporcentaje
	{
		get
		{
			return margendegananciaporcentaje;
		}
		set
		{
			margendegananciaporcentaje = value;
		}
	}
}
