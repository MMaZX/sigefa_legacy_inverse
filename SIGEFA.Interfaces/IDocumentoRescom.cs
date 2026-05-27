using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IDocumentoRescom
{
	bool InsertRescom(clsDocumentorescom rescom);

	bool InsertDetRescom(clsDetalleDocumentoRescom detrescom);
}
