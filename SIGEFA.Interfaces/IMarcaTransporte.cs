using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IMarcaTransporte
{
	bool Insert(clsMarcaTransporte NuevaMarcaTransporte);

	bool Update(clsMarcaTransporte MarcaTransporte);

	bool Delete(int Codigo);

	clsMarcaTransporte CargaMarcaTransporte(int Codigo);

	DataTable ListaMarcaTransportes();
}
