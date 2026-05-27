using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IConductor
{
	bool Insert(clsConductor NuevoConductor);

	bool Update(clsConductor Conductor);

	bool Delete(int Codigo);

	clsConductor CargaConductor(int Codigo);

	clsConductor BuscaConductor(int tipodocumento, string documento);

	DataTable ListaConductores(int tipodocuemento);

	DataTable CargaConductores();
}
