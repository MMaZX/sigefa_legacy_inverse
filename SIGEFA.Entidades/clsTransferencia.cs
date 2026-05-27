using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

internal class clsTransferencia
{
	private string sCodTransDir;

	private int iCodAlmacenOrigen;

	private int iCodTipoDocumento;

	private string sSiglaDocumento;

	private string sDescripcionDocumento;

	private int iCodAlmacenDestino;

	private int iMoneda;

	private decimal dTipoCambio;

	private DateTime dtFechaEnvio;

	private DateTime dtFechaEntrega;

	private string sDescripcionRechazo;

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

	private int idcodProv;

	private int iPendiente;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private string nombretrans;

	private string telefonotrans;

	private string direcciontrans;

	private string autorizadopor;

	private int numpedido;

	private int docTransBF;

	private int estadoTrnas;

	private List<clsDetalleTransferencia> lDetalle;

	private int codserie;

	private string serie;

	private string numerodocumento;

	public int codtrans;

	public int EstadoTrnas
	{
		get
		{
			return estadoTrnas;
		}
		set
		{
			estadoTrnas = value;
		}
	}

	public int Numpedido
	{
		get
		{
			return numpedido;
		}
		set
		{
			numpedido = value;
		}
	}

	public int DocTransBF
	{
		get
		{
			return docTransBF;
		}
		set
		{
			docTransBF = value;
		}
	}

	public string Nombretrans
	{
		get
		{
			return nombretrans;
		}
		set
		{
			nombretrans = value;
		}
	}

	public string Autorizadopor
	{
		get
		{
			return autorizadopor;
		}
		set
		{
			autorizadopor = value;
		}
	}

	public string Telefonotrans
	{
		get
		{
			return telefonotrans;
		}
		set
		{
			telefonotrans = value;
		}
	}

	public string Direcciontrans
	{
		get
		{
			return direcciontrans;
		}
		set
		{
			direcciontrans = value;
		}
	}

	public string Numerodocumento
	{
		get
		{
			return numerodocumento;
		}
		set
		{
			numerodocumento = value;
		}
	}

	public string Serie
	{
		get
		{
			return serie;
		}
		set
		{
			serie = value;
		}
	}

	public int Codserie
	{
		get
		{
			return codserie;
		}
		set
		{
			codserie = value;
		}
	}

	public string CodTransDir
	{
		get
		{
			return sCodTransDir;
		}
		set
		{
			sCodTransDir = value;
		}
	}

	public int CodAlmacenOrigen
	{
		get
		{
			return iCodAlmacenOrigen;
		}
		set
		{
			iCodAlmacenOrigen = value;
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

	public int CodAlmacenDestino
	{
		get
		{
			return iCodAlmacenDestino;
		}
		set
		{
			iCodAlmacenDestino = value;
		}
	}

	public int IdcodProv
	{
		get
		{
			return idcodProv;
		}
		set
		{
			idcodProv = value;
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

	public DateTime FechaEnvio
	{
		get
		{
			return dtFechaEnvio;
		}
		set
		{
			dtFechaEnvio = value;
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

	public string DescripcionRechazo
	{
		get
		{
			return sDescripcionRechazo;
		}
		set
		{
			sDescripcionRechazo = value;
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

	public List<clsDetalleTransferencia> Detalle
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

	public int codReqAlm { get; internal set; }

	public int codTransAExtornar { get; internal set; }
}
