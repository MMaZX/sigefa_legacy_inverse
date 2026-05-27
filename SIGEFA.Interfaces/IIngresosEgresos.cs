using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IIngresosEgresos
{
	bool Insert(clsIngresoEgreso ingreso);

	bool Update(clsIngresoEgreso ingreso);

	DataTable ListadoIngresosEgresos(int tipo);
}
