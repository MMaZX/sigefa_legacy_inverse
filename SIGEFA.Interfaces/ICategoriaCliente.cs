using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICategoriaCliente
{
	bool Insert(clsCategoriaCliente Nuevacatcliente);

	bool Update(clsCategoriaCliente catcliente);

	bool Delete(int Codigo);

	clsZona CargaZona(int Codigo);

	DataTable ListaCategoriasCliente();
}
