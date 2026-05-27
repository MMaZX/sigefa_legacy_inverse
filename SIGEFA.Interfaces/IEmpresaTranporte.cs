using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IEmpresaTranporte
{
	bool Insert(clsEmpresaTransporte NuevoEmpresaTranporte);

	bool Update(clsEmpresaTransporte EmpresaTranporte);

	bool Delete(int Codigo);

	clsEmpresaTransporte CargaEmpresaTranporte(int Codigo);

	clsEmpresaTransporte BuscaEmpresaTransporte(string RUC);

	DataTable CargaEmpresasTransporte();

	DataTable ListaEmpresaTranportes();
}
