namespace SIGEFA.Entidades;

internal class clsIngresoEgreso
{
	private int id;

	private string descripcion;

	private int tipo;

	private int estado;

	public int Id
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return descripcion;
		}
		set
		{
			descripcion = value;
		}
	}

	public int Tipo
	{
		get
		{
			return tipo;
		}
		set
		{
			tipo = value;
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
}
