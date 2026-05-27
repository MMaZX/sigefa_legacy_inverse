using System.Collections.Generic;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IAccesoSucursales
{
	bool Insert(clsAccesosSucursales NuevoAcceso);

	bool LimpiarAccesos(int CodUsuario, int CodEmpresa);

	List<int> MuestraAccesosSucursales(int CodUsuario, int codEmpresa);

	bool InsertAccesoEmp(int CodUsuario, int CodEmpresa);
}
