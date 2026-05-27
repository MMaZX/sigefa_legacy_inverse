using System;

namespace SIGEFA.Entidades;

public class clsProducto
{
	private int iCodProducto;

	private int iCodProductoAlmacen;

	private int iCodAlmacen;

	private int iCodUsuario;

	private int iCodGrupo;

	private int iCodLinea;

	private int iCodFamilia;

	private int iCodUnidadMedida;

	private int iCodTipoArticulo;

	private int iCodMarca;

	private int iCodControlStock;

	private string sReferencia;

	private string sDescripcion;

	private decimal dPrecioCatalogo;

	private double dValorProm;

	private decimal dPrecioProm;

	private double dRecargo;

	private double dValorVenta;

	private double dPrecioVenta;

	private double dPrecioVentaSoles;

	private double dPDescuento;

	private double dMontoDscto;

	private double dPrecioOferta;

	private double dMaximoDscto;

	private double dStockActual;

	private decimal dStockDisponible;

	private double dStockMinimo;

	private double dStockMaximo;

	private double dStockReposicion;

	private double dSoles;

	private double dTotalIngresos;

	private double dTotalSalidas;

	private double dTotalSolesIngresos;

	private double dTotalSolesSalidas;

	private int tipoImpuesto;

	private bool bDetraccion;

	private bool bEstado;

	private bool bOferta;

	private bool bConIGV;

	private bool bPrecioVariable;

	private DateTime dtUltimaModificacion;

	private DateTime dtFechaRegistro;

	private string sUnidaddescrip;

	private decimal dComision;

	private decimal dstockFuturo;

	private decimal dstockPorRecibir;

	private decimal dvalorPromsoles;

	private decimal dMaxPorcDesc;

	private int iPorllegar;

	private int iPorAtender;

	private int iPorCompletar;

	private int icantidad;

	private decimal dPeso;

	private decimal preciocompra;

	private string codSunat;

	private decimal porcentajerentencion;

	private string sCodUniversal;

	private string sUbicacion;

	private decimal dUltimoPrecioCompra;

	private bool cotizacion;

	public string nombreplantilla { get; set; }

	public int codmarca { get; set; }

	public int codproveedor { get; set; }

	public int codfamilia { get; set; }

	public int codli { get; set; }

	public int codfami { get; set; }

	public string descripcionp { get; set; }

	public bool VentaConTicket { get; set; }

	public bool ICBPER { get; set; }

	public decimal Flete_estimado { get; set; }

	public decimal Desestiva { get; set; }

	public decimal Estiva { get; set; }

	public decimal Comision1 { get; set; }

	public decimal Comision2 { get; set; }

	public decimal Comision3 { get; set; }

	public decimal GastosAdmin { get; set; }

	public decimal GastosAdic { get; set; }

	public int Proveedor1 { get; set; }

	public int Proveedor2 { get; set; }

	public int Proveedor3 { get; set; }

	public bool descontinuado { get; set; }

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

	public decimal Porcentajerentencion
	{
		get
		{
			return porcentajerentencion;
		}
		set
		{
			porcentajerentencion = value;
		}
	}

	public string CodSunat
	{
		get
		{
			return codSunat;
		}
		set
		{
			codSunat = value;
		}
	}

	public decimal PrecioCompra
	{
		get
		{
			return preciocompra;
		}
		set
		{
			preciocompra = value;
		}
	}

	public int Cantidad
	{
		get
		{
			return icantidad;
		}
		set
		{
			icantidad = value;
		}
	}

	public int Porllegar
	{
		get
		{
			return iPorllegar;
		}
		set
		{
			iPorllegar = value;
		}
	}

	public int PorAtender
	{
		get
		{
			return iPorAtender;
		}
		set
		{
			iPorAtender = value;
		}
	}

	public int PorCompletar
	{
		get
		{
			return iPorCompletar;
		}
		set
		{
			iPorCompletar = value;
		}
	}

	public decimal MaxPorcDesc
	{
		get
		{
			return dMaxPorcDesc;
		}
		set
		{
			dMaxPorcDesc = value;
		}
	}

	public decimal StockPorRecibir
	{
		get
		{
			return dstockPorRecibir;
		}
		set
		{
			dstockPorRecibir = value;
		}
	}

	public decimal StockFuturo
	{
		get
		{
			return dstockFuturo;
		}
		set
		{
			dstockFuturo = value;
		}
	}

	public int CodProductoAlmacen
	{
		get
		{
			return iCodProductoAlmacen;
		}
		set
		{
			iCodProductoAlmacen = value;
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

	public int CodUsuario
	{
		get
		{
			return iCodUsuario;
		}
		set
		{
			iCodUsuario = value;
		}
	}

	public int CodGrupo
	{
		get
		{
			return iCodGrupo;
		}
		set
		{
			iCodGrupo = value;
		}
	}

	public int CodLinea
	{
		get
		{
			return iCodLinea;
		}
		set
		{
			iCodLinea = value;
		}
	}

	public int CodFamilia
	{
		get
		{
			return iCodFamilia;
		}
		set
		{
			iCodFamilia = value;
		}
	}

	public int CodMarca
	{
		get
		{
			return iCodMarca;
		}
		set
		{
			iCodMarca = value;
		}
	}

	public int CodUnidadMedida
	{
		get
		{
			return iCodUnidadMedida;
		}
		set
		{
			iCodUnidadMedida = value;
		}
	}

	public int CodTipoArticulo
	{
		get
		{
			return iCodTipoArticulo;
		}
		set
		{
			iCodTipoArticulo = value;
		}
	}

	public int CodControlStock
	{
		get
		{
			return iCodControlStock;
		}
		set
		{
			iCodControlStock = value;
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

	public decimal PrecioCatalogo
	{
		get
		{
			return dPrecioCatalogo;
		}
		set
		{
			dPrecioCatalogo = value;
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

	public bool ConIgv
	{
		get
		{
			return bConIGV;
		}
		set
		{
			bConIGV = value;
		}
	}

	public double ValorProm
	{
		get
		{
			return dValorProm;
		}
		set
		{
			dValorProm = value;
		}
	}

	public decimal PrecioProm
	{
		get
		{
			return dPrecioProm;
		}
		set
		{
			dPrecioProm = value;
		}
	}

	public double Recargo
	{
		get
		{
			return dRecargo;
		}
		set
		{
			dRecargo = value;
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

	public double PrecioVentaSoles
	{
		get
		{
			return dPrecioVentaSoles;
		}
		set
		{
			dPrecioVentaSoles = value;
		}
	}

	public bool Oferta
	{
		get
		{
			return bOferta;
		}
		set
		{
			bOferta = value;
		}
	}

	public double PDescuento
	{
		get
		{
			return dPDescuento;
		}
		set
		{
			dPDescuento = value;
		}
	}

	public double MontoDscto
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

	public double PrecioOferta
	{
		get
		{
			return dPrecioOferta;
		}
		set
		{
			dPrecioOferta = value;
		}
	}

	public bool PrecioVariable
	{
		get
		{
			return bPrecioVariable;
		}
		set
		{
			bPrecioVariable = value;
		}
	}

	public double MaximoDscto
	{
		get
		{
			return dMaximoDscto;
		}
		set
		{
			dMaximoDscto = value;
		}
	}

	public double StockActual
	{
		get
		{
			return dStockActual;
		}
		set
		{
			dStockActual = value;
		}
	}

	public decimal StockDisponible
	{
		get
		{
			return dStockDisponible;
		}
		set
		{
			dStockDisponible = value;
		}
	}

	public double StockMinimo
	{
		get
		{
			return dStockMinimo;
		}
		set
		{
			dStockMinimo = value;
		}
	}

	public double StockMaximo
	{
		get
		{
			return dStockMaximo;
		}
		set
		{
			dStockMaximo = value;
		}
	}

	public double StockReposicion
	{
		get
		{
			return dStockReposicion;
		}
		set
		{
			dStockReposicion = value;
		}
	}

	public double Soles
	{
		get
		{
			return dSoles;
		}
		set
		{
			dSoles = value;
		}
	}

	public double TotalIngresos
	{
		get
		{
			return dTotalIngresos;
		}
		set
		{
			dTotalIngresos = value;
		}
	}

	public double TotalSalidas
	{
		get
		{
			return dTotalSalidas;
		}
		set
		{
			dTotalSalidas = value;
		}
	}

	public double TotalSolesIngresos
	{
		get
		{
			return dTotalSolesIngresos;
		}
		set
		{
			dTotalSolesIngresos = value;
		}
	}

	public double TotalSolesSalidas
	{
		get
		{
			return dTotalSolesSalidas;
		}
		set
		{
			dTotalSolesSalidas = value;
		}
	}

	public int TipoImpuesto
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

	public bool Detraccion
	{
		get
		{
			return bDetraccion;
		}
		set
		{
			bDetraccion = value;
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

	public DateTime UltimaModificacion
	{
		get
		{
			return dtUltimaModificacion;
		}
		set
		{
			dtUltimaModificacion = value;
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

	public string UnidadDescrip
	{
		get
		{
			return sUnidaddescrip;
		}
		set
		{
			sUnidaddescrip = value;
		}
	}

	public decimal Comision
	{
		get
		{
			return dComision;
		}
		set
		{
			dComision = value;
		}
	}

	public decimal ValorPromsoles
	{
		get
		{
			return dvalorPromsoles;
		}
		set
		{
			dvalorPromsoles = value;
		}
	}

	public decimal Peso
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

	public string SCodUniversal
	{
		get
		{
			return sCodUniversal;
		}
		set
		{
			sCodUniversal = value;
		}
	}

	public string SUbicacion
	{
		get
		{
			return sUbicacion;
		}
		set
		{
			sUbicacion = value;
		}
	}

	public decimal UltimoPrecioCompra
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
}
