using System;

namespace SIGEFA.Entidades;

public class clsDetalleGuiaRemisionCompra
{
	private int iCodDetalleGuiaRemisionCompra;

	private int iCodProducto;

	private int iCodOrdenCOmpra;

	private int iCodDetalleOrdenCOmpra;

	private int iCodGuiaRemisionCOmpra;

	private int icodMoneda;

	private int iUnidadIngresada;

	private double dCantidad;

	private double dCantidadRespaldo;

	private DateTime fFechaIngreso;

	private int iEstado;

	private int iCOdUser;

	private DateTime fFechaRegistro;

	private int iAnulado;

	private string sReferencia;

	private string sDescripcion;

	private string sUnidad;

	public int ICodDetalleGuiaRemisionCompra
	{
		get
		{
			return iCodDetalleGuiaRemisionCompra;
		}
		set
		{
			iCodDetalleGuiaRemisionCompra = value;
		}
	}

	public int ICodProducto
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

	public int ICodOrdenCOmpra
	{
		get
		{
			return iCodOrdenCOmpra;
		}
		set
		{
			iCodOrdenCOmpra = value;
		}
	}

	public int ICodGuiaRemisionCOmpra
	{
		get
		{
			return iCodGuiaRemisionCOmpra;
		}
		set
		{
			iCodGuiaRemisionCOmpra = value;
		}
	}

	public int IcodMoneda
	{
		get
		{
			return icodMoneda;
		}
		set
		{
			icodMoneda = value;
		}
	}

	public int IUnidadIngresada
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

	public double DCantidad
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

	public double DCantidadRespaldo
	{
		get
		{
			return dCantidadRespaldo;
		}
		set
		{
			dCantidadRespaldo = value;
		}
	}

	public DateTime FFechaIngreso
	{
		get
		{
			return fFechaIngreso;
		}
		set
		{
			fFechaIngreso = value;
		}
	}

	public int IEstado
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

	public int ICOdUser
	{
		get
		{
			return iCOdUser;
		}
		set
		{
			iCOdUser = value;
		}
	}

	public DateTime FFechaRegistro
	{
		get
		{
			return fFechaRegistro;
		}
		set
		{
			fFechaRegistro = value;
		}
	}

	public int IAnulado
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

	public int ICodDetalleOrdenCOmpra
	{
		get
		{
			return iCodDetalleOrdenCOmpra;
		}
		set
		{
			iCodDetalleOrdenCOmpra = value;
		}
	}

	public string SReferencia
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

	public string SDescripcion
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

	public string SUnidad
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
}
