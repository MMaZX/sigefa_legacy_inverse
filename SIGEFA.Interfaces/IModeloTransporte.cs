using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IModeloTransporte
{
	bool Insert(clsModeloTransporte NuevaModeloTransporte);

	bool Update(clsModeloTransporte ModeloTransporte);

	bool Delete(int Codigo);

	clsModeloTransporte CargaModeloTransporte(int Codigo);

	DataTable ListaModeloTransportes(int codMarcaTransporte);
}
