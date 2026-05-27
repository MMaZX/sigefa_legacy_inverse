using System;

namespace SIGEFA.Entidades;

public class clsUnidadEquivalente
{
	private int iCodUnidadEquivalente;

	private int iCodProducto;

	private int iCodUnidad;

	private decimal dFactor;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private decimal precio;

	private int iCodMoneda;

	public string nombreUnd { get; set; }

	public int Tipo { get; set; }

	public int CodAlmacen { get; set; }

	public decimal Stock { get; set; }

	public int CodEquivalente { get; set; }

	public decimal Precio
	{
		get
		{
			return precio;
		}
		set
		{
			precio = value;
		}
	}

	public int CompraVenta { get; set; }

	public int CodUnidadEquivalente
	{
		get
		{
			return iCodUnidadEquivalente;
		}
		set
		{
			iCodUnidadEquivalente = value;
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

	public decimal Factor
	{
		get
		{
			return dFactor;
		}
		set
		{
			dFactor = value;
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

	public int ICodMoneda
	{
		get
		{
			return iCodMoneda;
		}
		set
		{
			iCodMoneda = value;
		}
	}
}
