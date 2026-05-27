using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IEmpresa
{
	bool Insert(clsEmpresa EmpresaNueva);

	bool Update(clsEmpresa Empresa);

	bool Delete(int CodigoEmp);

	clsEmpresa CargaEmpresa(int CodigoEmp);

	bool VerificaRUC(string RUC);

	DataTable CargaEmpresas();

	DataTable ListaEmpresas();

	DataTable BuscaEmpresas(int Criterio, string Filtro);

	bool UpdateConfiguracion(clsParametros Configuracion);

	clsParametros CargaConfiguracion();

	clsParametros CargaParametroPorEmpresa(int codigoEmpresa, int codigoParametro);

	clsEmpresa CargaEmpresa3(int CodEmpresa);

	int empresaxalmacen(int codalmacen);
}
