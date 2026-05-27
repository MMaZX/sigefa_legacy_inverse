using System;

namespace SIGEFA.Entidades;

public class clsDetallePedido
{
	private int iCodDetallePedido;

	private int iCodProducto;

	private string sReferencia;

	private string sDescripcion;

	private int iCodPedido;

	private int iCodAlmacen;

	private int iUnidadIngresada;

	private string sSerieLote;

	private decimal dCantidad;

	private decimal dCantidadPendiente;

	private int iCodUnidad;

	private string sUnidad;

	private decimal dPrecioUnitario;

	private decimal dSubtotal;

	private decimal dDescuento1;

	private decimal dDescuento2;

	private decimal dDescuento3;

	private decimal dMontoDescuento;

	private decimal dIgv;

	private decimal dImporte;

	private decimal dPrecioVenta;

	private decimal dValorVenta;

	private decimal dPrecioReal;

	private decimal dValoReal;

	private DateTime dFechaRegistro;

	private int iCodUser;

	private decimal dCantidadVendida;

	private decimal dCantidadDevuelta;

	private decimal iImpDevuelto;

	private decimal iImpVend;

	private int codtipodoc;

	private bool precioigv;

	private decimal valorpromedio;

	private decimal precioMargen;

	private int codProv;

	private string tipoimpuesto;

	private int codEmpresa;

	private string sSerieMotor;

	private string sNroChasis;

	private string sModelo;

	private string sMarca;

	private string sColor;

	public int TipoUnidad { get; set; }

	public decimal icbper { get; set; }

	public bool icbper_band { get; set; }

	public int codlinea { get; set; }

	public int codfamilia { get; set; }

	public int codcombo { get; set; }

	public string SerieMotor
	{
		get
		{
			return sSerieMotor;
		}
		set
		{
			sSerieMotor = value;
		}
	}

	public string NroChasis
	{
		get
		{
			return sNroChasis;
		}
		set
		{
			sNroChasis = value;
		}
	}

	public string Modelo
	{
		get
		{
			return sModelo;
		}
		set
		{
			sModelo = value;
		}
	}

	public string Marca
	{
		get
		{
			return sMarca;
		}
		set
		{
			sMarca = value;
		}
	}

	public string Color
	{
		get
		{
			return sColor;
		}
		set
		{
			sColor = value;
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

	public string Tipoimpuesto
	{
		get
		{
			return tipoimpuesto;
		}
		set
		{
			tipoimpuesto = value;
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

	public decimal PrecioMargen
	{
		get
		{
			return precioMargen;
		}
		set
		{
			precioMargen = value;
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

	public int Codtipodoc
	{
		get
		{
			return codtipodoc;
		}
		set
		{
			codtipodoc = value;
		}
	}

	public decimal DCantidadVendida
	{
		get
		{
			return dCantidadVendida;
		}
		set
		{
			dCantidadVendida = value;
		}
	}

	public decimal DCantidadDevuelta
	{
		get
		{
			return dCantidadDevuelta;
		}
		set
		{
			dCantidadDevuelta = value;
		}
	}

	public decimal IImpDevuelto
	{
		get
		{
			return iImpDevuelto;
		}
		set
		{
			iImpDevuelto = value;
		}
	}

	public decimal IImpVend
	{
		get
		{
			return iImpVend;
		}
		set
		{
			iImpVend = value;
		}
	}

	public int CodDetallePedido
	{
		get
		{
			return iCodDetallePedido;
		}
		set
		{
			iCodDetallePedido = value;
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

	public decimal Cantidad
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

	public decimal CantidadPendiente
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

	public decimal PrecioUnitario
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

	public decimal Subtotal
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

	public decimal Descuento1
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

	public decimal Descuento2
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

	public decimal Descuento3
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

	public decimal MontoDescuento
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

	public decimal Importe
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

	public decimal PrecioVenta
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

	public decimal ValorVenta
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

	public decimal PrecioReal
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

	public decimal ValoReal
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
}
