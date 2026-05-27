namespace SIGEFA.Entidades;

internal class clsTipoPrecios
{
	private int codTipoPrecio;

	private int codTipoPrecioNuevo;

	private string sigla;

	private string descripcion;

	private int User;

	private int codAlmacen;

	public int CodTipoPrecio
	{
		get
		{
			return codTipoPrecio;
		}
		set
		{
			codTipoPrecio = value;
		}
	}

	public int CodTipoPrecioNuevo
	{
		get
		{
			return codTipoPrecioNuevo;
		}
		set
		{
			codTipoPrecioNuevo = value;
		}
	}

	public string Sigla
	{
		get
		{
			return sigla;
		}
		set
		{
			sigla = value;
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

	public int User1
	{
		get
		{
			return User;
		}
		set
		{
			User = value;
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
}
