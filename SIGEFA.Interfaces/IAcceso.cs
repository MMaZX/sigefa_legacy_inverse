using System.Collections.Generic;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IAcceso
{
	bool Insert(clsAccesos NuevoAcceso);

	bool LimpiarAccesos(int CodUsuario, int CodAlmacen);

	List<int> MuestraAccesos(int CodUsuario, int CodAlmacen);

	bool InsertAccesoEmp(int CodUsuario, int CodEmpresa, int codUser);
}
