using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITipoArticulo
{
	bool Insert(clsTipoArticulo NuevaTipoArticulo);

	bool Update(clsTipoArticulo TipoArticulo);

	bool Delete(int Codigo);

	clsTipoArticulo CargaTipoArticulo(int Codigo);

	DataTable ListaTipoArticulos();
}
