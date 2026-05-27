namespace SIGEFA.Entidades;

internal class clsComposicionQuimica
{
	private int iCodComposion;

	private int iCompQuimicaNuevo;

	private int iCodProducto;

	private string sComponente;

	private string sCantidad;

	public int CompQuimicaNuevo
	{
		get
		{
			return iCompQuimicaNuevo;
		}
		set
		{
			iCompQuimicaNuevo = value;
		}
	}

	public int CodComposion
	{
		get
		{
			return iCodComposion;
		}
		set
		{
			iCodComposion = value;
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

	public string Componente
	{
		get
		{
			return sComponente;
		}
		set
		{
			sComponente = value;
		}
	}

	public string Cantidad
	{
		get
		{
			return sCantidad;
		}
		set
		{
			sCantidad = value;
		}
	}
}
