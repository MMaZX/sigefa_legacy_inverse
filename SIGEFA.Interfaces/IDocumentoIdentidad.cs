using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IDocumentoIdentidad
{
	DataTable ListaDocumentoIdentidad(int codigoTipoDocumento);

	clsDocumentoIdentidad MuestraDocumentoIdentidad(int codigoDocumentoIdentidad);

	clsDocumentoIdentidad ObtenerDocumentoIdentidadDeVenta(int codigoFacturaVenta);
}
