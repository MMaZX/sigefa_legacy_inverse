using System;

namespace SIGEFA.Entidades;

public class clsDetalleGuiaRemision
{
	private int iCodDetalleGuiaRemision;

	private int iCodProducto;

	private string sReferencia;

	private string sDescripcion;

	private int iCodGuiaRemision;

	private int iCodAlmacen;

	private int iUnidadIngresada;

	private string sSerieLote;

	private double dCantidad;

	private double dCantidadPendiente;

	private int iCodUnidad;

	private string sUnidad;

	private double dPeso;

	private bool bPendiente;

	private bool bEstado;

	private DateTime dFechaRegistro;

	private int iCodUser;

	private int iCodVenta;

	public int coddetalleproducto { get; set; }

	public int codnotaingreso { get; set; }

	public bool estadoOrden { get; set; }

	public string marca { get; set; }

	public string familia { get; set; }

	public string modelo { get; set; }

	public string proveedor { get; set; }

	public string etiquetastring { get; set; }

	public int etiqueta { get; set; }

	public bool productosolicitado { get; set; }

	public decimal cantidadpro { get; set; }

	public decimal stockactual { get; set; }

	public decimal preciocompra { get; set; }

	public decimal igv { get; set; }

	public decimal pventa { get; set; }

	public decimal vventa { get; set; }

	public decimal importe { get; set; }

	public decimal total { get; set; }

	public clsGuiaRemision guia { get; set; }

	public int codDtalleOrden { get; set; }

	public int CodDetalleGuiaRemision
	{
		get
		{
			return iCodDetalleGuiaRemision;
		}
		set
		{
			iCodDetalleGuiaRemision = value;
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

	public int CodGuiaRemision
	{
		get
		{
			return iCodGuiaRemision;
		}
		set
		{
			iCodGuiaRemision = value;
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

	public double Peso
	{
		get
		{
			return dPeso;
		}
		set
		{
			dPeso = value;
		}
	}

	public bool Pendiente
	{
		get
		{
			return bPendiente;
		}
		set
		{
			bPendiente = value;
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

	public decimal precio { get; set; }
}
