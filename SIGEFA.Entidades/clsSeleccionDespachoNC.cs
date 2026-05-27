namespace SIGEFA.Entidades;

internal class clsSeleccionDespachoNC
{
	private int codigo;

	private int codNotaCredito;

	private int codDetalleNotaCredito;

	private int codAlmacen;

	private double ctdadPermitida;

	private double ctdadSeleccionada;

	private bool seleccionado;

	public int CodNotaCredito
	{
		get
		{
			return codNotaCredito;
		}
		set
		{
			codNotaCredito = value;
		}
	}

	public int CodDetalleNotaCredito
	{
		get
		{
			return codDetalleNotaCredito;
		}
		set
		{
			codDetalleNotaCredito = value;
		}
	}

	public int CodAlmacen
	{
		get
		{
			return codAlmacen;
		}
		set
		{
			codAlmacen = value;
		}
	}

	public double CtdadPermitida
	{
		get
		{
			return ctdadPermitida;
		}
		set
		{
			ctdadPermitida = value;
		}
	}

	public double CtdadSeleccionada
	{
		get
		{
			return ctdadSeleccionada;
		}
		set
		{
			ctdadSeleccionada = value;
		}
	}

	public bool Seleccionado
	{
		get
		{
			return seleccionado;
		}
		set
		{
			seleccionado = value;
		}
	}

	public int Codigo
	{
		get
		{
			return codigo;
		}
		set
		{
			codigo = value;
		}
	}
}
