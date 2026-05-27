using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IDosis
{
	bool Insert(clsDosis NuevaDosis);

	bool Delete(int Codigo);

	DataTable ListaDosis(int codPro);
}
