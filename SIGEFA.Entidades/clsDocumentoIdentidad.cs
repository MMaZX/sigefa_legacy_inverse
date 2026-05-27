namespace SIGEFA.Entidades;

public class clsDocumentoIdentidad
{
	private int iCodDocumentoIdentidad;

	private int iCodigoSunat;

	private int iCodigoTipoDocumento;

	private string sDescripcion;

	private int iLongitud;

	public int CodDocumentoIdentidad
	{
		get
		{
			return iCodDocumentoIdentidad;
		}
		set
		{
			iCodDocumentoIdentidad = value;
		}
	}

	public int CodigoSunat
	{
		get
		{
			return iCodigoSunat;
		}
		set
		{
			iCodigoSunat = value;
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

	public int Longitud
	{
		get
		{
			return iLongitud;
		}
		set
		{
			iLongitud = value;
		}
	}

	public int CodigoTipoDocumento
	{
		get
		{
			return iCodigoTipoDocumento;
		}
		set
		{
			iCodigoTipoDocumento = value;
		}
	}
}
