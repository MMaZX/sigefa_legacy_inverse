using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICuota
{
	bool Insert(clsCuota NuevoCuota);

	clsCuota CargaCuota(int CodCuota);

	DataTable MuestraListaCuotasPrestamo(int CodNotaIngreso);
}
