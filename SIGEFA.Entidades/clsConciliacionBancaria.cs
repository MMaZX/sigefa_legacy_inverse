using System;

namespace SIGEFA.Entidades;

internal class clsConciliacionBancaria
{
	private int codconciliacion;

	private int codconciliacionNuevo;

	private int codbanco;

	private int codcuenta;

	private decimal saldoextracto;

	private decimal montonocobrado;

	private decimal saldolibro;

	private int codmoneda;

	private int estado;

	private int coduser;

	private DateTime fecharegistro;

	public int CodconciliacionNuevo
	{
		get
		{
			return codconciliacionNuevo;
		}
		set
		{
			codconciliacionNuevo = value;
		}
	}

	public int Codconciliacion
	{
		get
		{
			return codconciliacion;
		}
		set
		{
			codconciliacion = value;
		}
	}

	public int Codbanco
	{
		get
		{
			return codbanco;
		}
		set
		{
			codbanco = value;
		}
	}

	public int Codcuenta
	{
		get
		{
			return codcuenta;
		}
		set
		{
			codcuenta = value;
		}
	}

	public decimal Saldoextracto
	{
		get
		{
			return saldoextracto;
		}
		set
		{
			saldoextracto = value;
		}
	}

	public decimal Montonocobrado
	{
		get
		{
			return montonocobrado;
		}
		set
		{
			montonocobrado = value;
		}
	}

	public decimal Saldolibro
	{
		get
		{
			return saldolibro;
		}
		set
		{
			saldolibro = value;
		}
	}

	public int Codmoneda
	{
		get
		{
			return codmoneda;
		}
		set
		{
			codmoneda = value;
		}
	}

	public int Estado
	{
		get
		{
			return estado;
		}
		set
		{
			estado = value;
		}
	}

	public int Coduser
	{
		get
		{
			return coduser;
		}
		set
		{
			coduser = value;
		}
	}

	public DateTime Fecharegistro
	{
		get
		{
			return fecharegistro;
		}
		set
		{
			fecharegistro = value;
		}
	}
}
