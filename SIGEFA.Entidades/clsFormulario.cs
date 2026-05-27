namespace SIGEFA.Entidades;

public class clsFormulario
{
	private int iCodFormulario;

	private string sDescripcion;

	private int iNivel;

	private int iPadre;

	public int CodFormulario
	{
		get
		{
			return iCodFormulario;
		}
		set
		{
			iCodFormulario = value;
		}
	}

	public string Descripcion
	{
		get
		{
			return sDescripcion;
		}
		set
		{
			sDescripcion = value;
		}
	}

	public int Nivel
	{
		get
		{
			return iNivel;
		}
		set
		{
			iNivel = value;
		}
	}

	public int Padre
	{
		get
		{
			return iPadre;
		}
		set
		{
			iPadre = value;
		}
	}
}
