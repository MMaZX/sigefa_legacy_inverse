using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITipoDocumento
{
	bool Insert(clsTipoDocumento NuevoTipoDocumento);

	bool Update(clsTipoDocumento TipoDocumento);

	bool Delete(int Codigo);

	clsTipoDocumento CargaTipoDocumento(int Codigo);

	clsTipoDocumento BuscaTipoDocumento(string Sigla);

	DataTable ListaTipoDocumentos();

	DataTable ListaTipoDocumentosDeCaja();

	DataTable CargaTipoDocumentos();

	DataTable CargaTipoDocumentoBolFacNotCredito();

	DataTable ListaDocumentoNota();

	DataTable ListaTipoDocumentosElectronicos();

	DataTable ListaTipoDocumentosElectronicos_2();
}
