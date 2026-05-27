using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IBanco
{
	bool Insert(clsBanco NuevaBanco);

	bool Update(clsBanco Banco);

	bool Delete(int Codigo);

	clsBanco CargaBanco(int Codigo);

	DataTable ListaBancos();

	DataTable CargaBancos();
}
