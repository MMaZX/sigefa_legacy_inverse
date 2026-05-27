using System;

namespace SIGEFA.Entidades;

public class clsParametros
{
	private int iCodConfiguracion;

	private double dIGV;

	private bool iEstado;

	private int iCodUser;

	private DateTime dtFechaRegistro;

	private int idiasVigencia;

	private bool bFacturasVencidas;

	private double dValor;

	private bool autoguardado;

	private decimal icbper;

	public bool Autoguardado
	{
		get
		{
			return autoguardado;
		}
		set
		{
			autoguardado = value;
		}
	}

	public int CodConfiguracion
	{
		get
		{
			return iCodConfiguracion;
		}
		set
		{
			iCodConfiguracion = value;
		}
	}

	public double IGV
	{
		get
		{
			return dIGV;
		}
		set
		{
			dIGV = value;
		}
	}

	public bool Estado
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

	public int DiasVigencia
	{
		get
		{
			return idiasVigencia;
		}
		set
		{
			idiasVigencia = value;
		}
	}

	public bool FacturasVencidas
	{
		get
		{
			return bFacturasVencidas;
		}
		set
		{
			bFacturasVencidas = value;
		}
	}

	public double Valor
	{
		get
		{
			return dValor;
		}
		set
		{
			dValor = value;
		}
	}

	public decimal Icbper
	{
		get
		{
			return icbper;
		}
		set
		{
			icbper = value;
		}
	}
}
