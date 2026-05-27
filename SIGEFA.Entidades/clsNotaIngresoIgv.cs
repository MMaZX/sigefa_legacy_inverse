namespace SIGEFA.Entidades;

public class clsNotaIngresoIgv
{
	private int iCodNotaIngresoIgv;

	private int iCodNotaIngreso;

	private bool bIncluyeIgv;

	public int CodNotaIngresoIgv
	{
		get
		{
			return iCodNotaIngresoIgv;
		}
		set
		{
			iCodNotaIngresoIgv = value;
		}
	}

	public int CodNotaIngreso
	{
		get
		{
			return iCodNotaIngreso;
		}
		set
		{
			iCodNotaIngreso = value;
		}
	}

	public bool IncluyeIgv
	{
		get
		{
			return bIncluyeIgv;
		}
		set
		{
			bIncluyeIgv = value;
		}
	}
}
