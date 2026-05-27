namespace SIGEFA.Entidades;

internal class clsDetalleConciliacion
{
	private int codconciliacion;

	private int codconciliacionNuevo;

	private int activo_conci;

	private int bandera;

	private int codctamovimiento;

	private decimal monto;

	private int estado;

	private int coduser;

	private int fecharegistro;

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

	public int Codctamovimiento
	{
		get
		{
			return codctamovimiento;
		}
		set
		{
			codctamovimiento = value;
		}
	}

	public decimal Monto
	{
		get
		{
			return monto;
		}
		set
		{
			monto = value;
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

	public int Fecharegistro
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

	public int Actico_conci
	{
		get
		{
			return activo_conci;
		}
		set
		{
			activo_conci = value;
		}
	}

	public int Bandera
	{
		get
		{
			return bandera;
		}
		set
		{
			bandera = value;
		}
	}
}
