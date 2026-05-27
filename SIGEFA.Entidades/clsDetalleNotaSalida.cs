using System;

namespace SIGEFA.Entidades;

public class clsDetalleNotaSalida
{
	private int iCodDetalleSalida;

	private int iCodProducto;

	private string sReferencia;

	private string sDescripcion;

	private int iCodNotaSalida;

	private int iCodAlmacen;

	private int iUnidadIngresada;

	private string sSerieLote;

	private DateTime dFechaSalida;

	private double dCantidad;

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

	private int iCodVenta;

	private int iCodCoti;

	private int iCodLista;

	private double dValorRealSoles;

	private int iCodDetalleCotizacion;

	private double dValorPromedio;

	private double dValorPromedioSoles;

	private double dCantidadPendiente;

	private string tipoImpuesto;

	private int tipoArticulo;

	private string descripcionAlmacen;

	public int TipoUnidad { get; set; }

	public int CodEmpresa { get; set; }

	public decimal Vbruto { get; set; }

	public decimal Vmontodescuento { get; set; }

	public decimal Vvalorventa { get; set; }

	public decimal Vigv { get; set; }

	public decimal Vprecioventa { get; set; }

	public decimal Vprecioreal { get; set; }

	public decimal Vvalorreal { get; set; }

	public decimal Vfactorigv { get; set; }

	public decimal VmaxPorcDescto { get; set; }

	public decimal Vmontogravadas { get; set; }

	public decimal Vmontogratuitas { get; set; }

	public decimal Vmontoinafectas { get; set; }

	public decimal Vmontoexoneradas { get; set; }

	public string DescripcionAlmacen
	{
		get
		{
			return descripcionAlmacen;
		}
		set
		{
			descripcionAlmacen = value;
		}
	}

	public int TipoArticulo
	{
		get
		{
			return tipoArticulo;
		}
		set
		{
			tipoArticulo = value;
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

	public int CodDetalleSalida
	{
		get
		{
			return iCodDetalleSalida;
		}
		set
		{
			iCodDetalleSalida = value;
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

	public int CodNotaSalida
	{
		get
		{
			return iCodNotaSalida;
		}
		set
		{
			iCodNotaSalida = value;
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

	public DateTime FechaSalida
	{
		get
		{
			return dFechaSalida;
		}
		set
		{
			dFechaSalida = value;
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

	public int CodCoti
	{
		get
		{
			return iCodCoti;
		}
		set
		{
			iCodCoti = value;
		}
	}

	public int CodLista
	{
		get
		{
			return iCodLista;
		}
		set
		{
			iCodLista = value;
		}
	}

	public double ValorRealSoles
	{
		get
		{
			return dValorRealSoles;
		}
		set
		{
			dValorRealSoles = value;
		}
	}

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

	public double ValorPromedio
	{
		get
		{
			return dValorPromedio;
		}
		set
		{
			dValorPromedio = value;
		}
	}

	public double ValorPromedioSoles
	{
		get
		{
			return dValorPromedioSoles;
		}
		set
		{
			dValorPromedioSoles = value;
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
}
