using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IGrupo
{
	bool Insert(clsGrupo NuevoGrupo);

	bool Update(clsGrupo Grupo);

	bool Delete(int Codigo);

	clsGrupo CargaGrupo(int Codigo);

	DataTable ListaGrupos(int codLinea);
}
