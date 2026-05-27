using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITipoPrecio
{
	bool insert(clsTipoPrecios tp);

	bool Update(clsTipoPrecios tp);

	bool eliminar(int codTipoPrecio);

	DataTable ListaPrecios();
}
