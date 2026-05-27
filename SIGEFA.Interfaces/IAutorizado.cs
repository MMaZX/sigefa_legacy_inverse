using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IAutorizado
{
	bool Insert(clsAutorizado NuevoAutorizado);

	bool Update(clsAutorizado Autorizado);

	bool Delete(int Codigo);

	clsAutorizado CargaAutorizado(int Codigo);

	DataTable ListaAutorizados();
}
