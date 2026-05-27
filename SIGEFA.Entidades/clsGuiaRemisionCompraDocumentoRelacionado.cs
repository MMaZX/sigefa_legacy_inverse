namespace SIGEFA.Entidades;

public class clsGuiaRemisionCompraDocumentoRelacionado
{
	private int codGuiaRemisionCompraDocumentoRelacionado;

	private int codGuiaRemisionCompra;

	private int codDocumentoRelacionado;

	private int codTipoDocumento;

	private int tipoGRCDR;

	private int anulado;

	public int CodGuiaRemisionCompraDocumentoRelacionado
	{
		get
		{
			return codGuiaRemisionCompraDocumentoRelacionado;
		}
		set
		{
			codGuiaRemisionCompraDocumentoRelacionado = value;
		}
	}

	public int CodGuiaRemisionCompra
	{
		get
		{
			return codGuiaRemisionCompra;
		}
		set
		{
			codGuiaRemisionCompra = value;
		}
	}

	public int CodDocumentoRelacionado
	{
		get
		{
			return codDocumentoRelacionado;
		}
		set
		{
			codDocumentoRelacionado = value;
		}
	}

	public int CodTipoDocumento
	{
		get
		{
			return codTipoDocumento;
		}
		set
		{
			codTipoDocumento = value;
		}
	}

	public int Anulado
	{
		get
		{
			return anulado;
		}
		set
		{
			anulado = value;
		}
	}

	public int TipoGRCDR
	{
		get
		{
			return tipoGRCDR;
		}
		set
		{
			tipoGRCDR = value;
		}
	}
}
