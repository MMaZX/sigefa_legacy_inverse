using System;

namespace SIGEFA.Entidades;

public class clsDetalleCotizacion
{
	private int iCodDetalleCotizacion;

	private int iCodProducto;

	private string sReferencia;

	private string sDescripcion;

	private int iCodCotizacion;

	private int iCodAlmacen;

	private int iUnidadIngresada;

	private string sSerieLote;

	private double dCantidad;

	private double dCantidadPendiente;

	private int iCodUnidad;

	private string sUnidad;

	private double dPrecioUnitario;

	private double dSubtotal;

	private double dDescuento1;

	private double dDescuento2;

	private double dDescuento3;

	private double dMontoDescuento;

	private double dIgv;

	private double dImporte;

	private double dPrecioVenta;

	private double dValorVenta;

	private double dPrecioReal;

	private double dValoReal;

	private DateTime dFechaRegistro;

	private int iCodUser;

	private int codmarca;

	private string diasentrega;

	private decimal stockdisponible;

	private bool validadescuento;

	private bool stockseparado;

	private decimal ganciamonto;

	private decimal ganciaporcentaje;

	private decimal totalcosto;

	private bool cotizacion;

	public int codtipoprecio { get; set; }

	public decimal preciocompra { get; set; }

	public string motivo { get; set; }

	public int CodDetalleCotizacion
	{
		get
		{
			return iCodDetalleCotizacion;
		}
		set
		{
			iCodDetalleCotizacion = value;
		}
	}

	public int CodProducto
	{
		get
		{
			return iCodProducto;
		}
		set
		{
			iCodProducto = value;
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

	public string Descripcion
	{
		get
		{
			return sDescripcion;
		}
		set
		{
			sDescripcion = value;
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

	public int UnidadIngresada
	{
		get
		{
			return iUnidadIngresada;
		}
		set
		{
			iUnidadIngresada = value;
		}
	}

	public string Unidad
	{
		get
		{
			return sUnidad;
		}
		set
		{
			sUnidad = value;
		}
	}

	public string SerieLote
	{
		get
		{
			return sSerieLote;
		}
		set
		{
			sSerieLote = value;
		}
	}

	public double Cantidad
	{
		get
		{
			return dCantidad;
		}
		set
		{
			dCantidad = value;
		}
	}

	public int CodUnidad
	{
		get
		{
			return iCodUnidad;
		}
		set
		{
			iCodUnidad = value;
		}
	}

	public double PrecioUnitario
	{
		get
		{
			return dPrecioUnitario;
		}
		set
		{
			dPrecioUnitario = value;
		}
	}

	public double Subtotal
	{
		get
		{
			return dSubtotal;
		}
		set
		{
			dSubtotal = value;
		}
	}

	public double Descuento1
	{
		get
		{
			return dDescuento1;
		}
		set
		{
			dDescuento1 = value;
		}
	}

	public double Descuento2
	{
		get
		{
			return dDescuento2;
		}
		set
		{
			dDescuento2 = value;
		}
	}

	public double Descuento3
	{
		get
		{
			return dDescuento3;
		}
		set
		{
			dDescuento3 = value;
		}
	}

	public double MontoDescuento
	{
		get
		{
			return dMontoDescuento;
		}
		set
		{
			dMontoDescuento = value;
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

	public double Importe
	{
		get
		{
			return dImporte;
		}
		set
		{
			dImporte = value;
		}
	}

	public double PrecioVenta
	{
		get
		{
			return dPrecioVenta;
		}
		set
		{
			dPrecioVenta = value;
		}
	}

	public double ValorVenta
	{
		get
		{
			return dValorVenta;
		}
		set
		{
			dValorVenta = value;
		}
	}

	public double PrecioReal
	{
		get
		{
			return dPrecioReal;
		}
		set
		{
			dPrecioReal = value;
		}
	}

	public double ValoReal
	{
		get
		{
			return dValoReal;
		}
		set
		{
			dValoReal = value;
		}
	}

	public DateTime FechaRegistro
	{
		get
		{
			return dFechaRegistro;
		}
		set
		{
			dFechaRegistro = value;
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

	public double CantidadPendiente
	{
		get
		{
			return dCantidadPendiente;
		}
		set
		{
			dCantidadPendiente = value;
		}
	}

	public int Codmarca
	{
		get
		{
			return codmarca;
		}
		set
		{
			codmarca = value;
		}
	}

	public string Diasentrega
	{
		get
		{
			return diasentrega;
		}
		set
		{
			diasentrega = value;
		}
	}

	public decimal Stockdisponible
	{
		get
		{
			return stockdisponible;
		}
		set
		{
			stockdisponible = value;
		}
	}

	public bool Validadescuento
	{
		get
		{
			return validadescuento;
		}
		set
		{
			validadescuento = value;
		}
	}

	public bool Stockseparado
	{
		get
		{
			return stockseparado;
		}
		set
		{
			stockseparado = value;
		}
	}

	public decimal Ganciamonto
	{
		get
		{
			return ganciamonto;
		}
		set
		{
			ganciamonto = value;
		}
	}

	public decimal Ganciaporcentaje
	{
		get
		{
			return ganciaporcentaje;
		}
		set
		{
			ganciaporcentaje = value;
		}
	}

	public bool Cotizacion
	{
		get
		{
			return cotizacion;
		}
		set
		{
			cotizacion = value;
		}
	}

	public decimal Totalcosto
	{
		get
		{
			return totalcosto;
		}
		set
		{
			totalcosto = value;
		}
	}
}
