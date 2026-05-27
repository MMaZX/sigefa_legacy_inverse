namespace SIGEFA.Entidades;

internal class clsDosis
{
	private int iCodDosisNuevo;

	private int iCodDosis;

	private int iCodProducto;

	private string sCultivo;

	private string sAplicacion;

	private string sDosis;

	private string sLmrppm;

	private string sPc;

	public int CodDosisNuevo
	{
		get
		{
			return iCodDosisNuevo;
		}
		set
		{
			iCodDosisNuevo = value;
		}
	}

	public int CodDosis
	{
		get
		{
			return iCodDosis;
		}
		set
		{
			iCodDosis = value;
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

	public string Cultivo
	{
		get
		{
			return sCultivo;
		}
		set
		{
			sCultivo = value;
		}
	}

	public string Aplicacion
	{
		get
		{
			return sAplicacion;
		}
		set
		{
			sAplicacion = value;
		}
	}

	public string Dosis
	{
		get
		{
			return sDosis;
		}
		set
		{
			sDosis = value;
		}
	}

	public string Lmrppm
	{
		get
		{
			return sLmrppm;
		}
		set
		{
			sLmrppm = value;
		}
	}

	public string Pc
	{
		get
		{
			return sPc;
		}
		set
		{
			sPc = value;
		}
	}
}
