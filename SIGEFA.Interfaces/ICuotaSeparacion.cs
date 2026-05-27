using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICuotaSeparacion
{
	bool Insert(clsCuotasSeparacion cuotasepa);

	DataTable CargaCuotas(int codSeparacion);

	bool delete(int idValor);

	clsCuotasSeparacion BuscarCuotasSeparacion(int codSeparacion, int codAlmacen);
}
