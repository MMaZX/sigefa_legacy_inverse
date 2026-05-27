using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITipoPagoCaja
{
	bool Insert(clsTipoPagoCaja NuevoTPcaja);

	bool Update(clsTipoPagoCaja TPcaja);

	bool Delete(int Codigo);

	DataTable ListaTipoPagoCaja();
}
