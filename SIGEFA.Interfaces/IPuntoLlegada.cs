using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IPuntoLlegada
{
	bool Insert(clsPuntoLlegada punto);

	DataTable ListaPuntos();
}
