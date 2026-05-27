using System;

namespace SIGEFA.Entidades;

internal class clsDetalleTransferencia
{
	private int iCodDetalleTransfer;

	private int iCodProducto;

	private int codProv;

	private string sReferencia;

	private string sDescripcion;

	private int iCodTransDir;

	private int iCodAlmacenOrigen;

	private int iCodAlmacenDestino;

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

	private bool precioigv;

	private decimal valorpromedio;

	private int estadoTrnas;

	private double cantidadEntrega;

	private string sDespachado;

	private int iCodUser;

	public string Despachado
	{
		get
		{
			return sDespachado;
		}
		set
		{
			sDespachado = value;
		}
	}

	public double CantidadEntrega
	{
		get
		{
			return cantidadEntrega;
		}
		set
		{
			cantidadEntrega = value;
		}
	}

	public decimal Valorpromedio
	{
		get
		{
			return valorpromedio;
		}
		set
		{
			valorpromedio = value;
		}
	}

	public bool Precioigv
	{
		get
		{
			return precioigv;
		}
		set
		{
			precioigv = value;
		}
	}

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

	public int CodDetalleTransfer
	{
		get
		{
			return iCodDetalleTransfer;
		}
		set
		{
			iCodDetalleTransfer = value;
		}
	}

	public int CodProv
	{
		get
		{
			return codProv;
		}
		set
		{
			codProv = value;
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

	public int CodTransDir
	{
		get
		{
			return iCodTransDir;
		}
		set
		{
			iCodTransDir = value;
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

	public int codDetalleReqAlm { get; internal set; }
}
