using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IVariante
{
	bool Insert(clsVariante NuevoVariante);

	bool Update(clsVariante Variante);

	bool Delete(int Codigo);

	clsVariante CargaVariante(int Codigo);

	DataTable ListaVariantes(int codCaracteristica);
}
