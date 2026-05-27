using System;

namespace SIGEFA.Entidades;

public class clsDetalleSeparacionVenta
{
	private int iCodDetalleSeparacion;

	private int icodSeparacion;

	private int iCodProducto;

	private int iCodAlmacen;

	private int iUnidadIngresada;

	private double dCantidad;

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

	private double dValorPromedio;

	private double dValorPromedioSoles;

	public int CodDetalleSeparacion
	{
		get
		{
			return iCodDetalleSeparacion;
		}
		set
		{
			iCodDetalleSeparacion = value;
		}
	}

	public int CodSeparacion
	{
		get
		{
			return icodSeparacion;
		}
		set
		{
			icodSeparacion = value;
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
}
