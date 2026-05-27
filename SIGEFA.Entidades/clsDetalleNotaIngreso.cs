using System;

namespace SIGEFA.Entidades;

public class clsDetalleNotaIngreso
{
	private int iCodDetalleIngreso;

	private int iCodProducto;

	private int iCodNotaIngreso;

	private int iCodAlmacen;

	private int iUnidadIngresada;

	private int iMoneda;

	private string sSerieLote;

	private double dCantidad;

	private int iCodUnidad;

	private double dPrecioUnitario;

	private double dSubtotal;

	private double dDescuento1;

	private double dDescuento2;

	private double dDescuento3;

	private double dMontoDescuento;

	private double dIgv;

	private double dFlete;

	private double dImporte;

	private double dPrecioReal;

	private double dValoReal;

	private bool bEstado;

	private DateTime dFechaIngreso;

	private DateTime dFechaRegistro;

	private int iCodUser;

	private int coddetalleOrden;

	private double iValorrealSoles;

	private int iCodDetalleRequerimiento;

	private bool bBonificacion;

	private double dUltimoPrecioCompra;

	private string sDescripcionProducto;

	public decimal cantidadpendiente { get; set; }

	public string EstadoDeLaOrden { get; set; }

	public string ProductoSolicitado { get; set; }

	public int codguiaremision { get; set; }

	public string stado { get; set; }

	public int CoddetalleOrden
	{
		get
		{
			return coddetalleOrden;
		}
		set
		{
			coddetalleOrden = value;
		}
	}

	public int CodDetalleIngreso
	{
		get
		{
			return iCodDetalleIngreso;
		}
		set
		{
			iCodDetalleIngreso = value;
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

	public bool Estado
	{
		get
		{
			return bEstado;
		}
		set
		{
			bEstado = value;
		}
	}

	public DateTime FechaIngreso
	{
		get
		{
			return dFechaIngreso;
		}
		set
		{
			dFechaIngreso = value;
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

	public int CodProveedor { get; set; }

	public double ValorrealSoles
	{
		get
		{
			return iValorrealSoles;
		}
		set
		{
			iValorrealSoles = value;
		}
	}

	public int CodDetalleRequerimiento
	{
		get
		{
			return iCodDetalleRequerimiento;
		}
		set
		{
			iCodDetalleRequerimiento = value;
		}
	}

	public bool Bonificacion
	{
		get
		{
			return bBonificacion;
		}
		set
		{
			bBonificacion = value;
		}
	}

	public double UltimoPrecioCompra
	{
		get
		{
			return dUltimoPrecioCompra;
		}
		set
		{
			dUltimoPrecioCompra = value;
		}
	}

	public string DescripcionProducto
	{
		get
		{
			return sDescripcionProducto;
		}
		set
		{
			sDescripcionProducto = value;
		}
	}

	public bool Equals(clsDetalleNotaIngreso other)
	{
		if (CodDetalleIngreso == other.CodDetalleIngreso && CodAlmacen == other.CodAlmacen)
		{
			return true;
		}
		return false;
	}

	public bool CodProEquals(int codigo)
	{
		if (CodProducto == codigo)
		{
			return true;
		}
		return false;
	}
}
