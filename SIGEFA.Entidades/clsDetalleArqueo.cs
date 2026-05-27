using System;

namespace SIGEFA.Entidades;

public class clsDetalleArqueo
{
	private int iCodDetalle;

	private int iCodUsuario;

	private int iCodArqueo;

	private int iCodProducto;

	private decimal dStockS;

	private decimal dStockF;

	private decimal dDiferencia;

	private int iEstado;

	private string sObservacion;

	private DateTime dFechaRegistro;

	private DateTime dFechaChekeo;

	private int iCodAlma;

	public int ICodDetalle
	{
		get
		{
			return iCodDetalle;
		}
		set
		{
			iCodDetalle = value;
		}
	}

	public int ICodUsuario
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

	public int ICodArqueo
	{
		get
		{
			return iCodArqueo;
		}
		set
		{
			iCodArqueo = value;
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

	public decimal DStockS
	{
		get
		{
			return dStockS;
		}
		set
		{
			dStockS = value;
		}
	}

	public decimal DStockF
	{
		get
		{
			return dStockF;
		}
		set
		{
			dStockF = value;
		}
	}

	public decimal DDiferencia
	{
		get
		{
			return dDiferencia;
		}
		set
		{
			dDiferencia = value;
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

	public string SObservacion
	{
		get
		{
			return sObservacion;
		}
		set
		{
			sObservacion = value;
		}
	}

	public DateTime DFechaRegistro
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

	public DateTime DFechaChekeo
	{
		get
		{
			return dFechaChekeo;
		}
		set
		{
			dFechaChekeo = value;
		}
	}

	public int ICodAlma
	{
		get
		{
			return iCodAlma;
		}
		set
		{
			iCodAlma = value;
		}
	}
}
