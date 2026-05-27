using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IParametroDescuento
{
	bool Insert(clsParametroDescuento ingreso);

	bool Update(clsParametroDescuento ingreso);

	DataTable ListadoParametros(int CodEmpresa);

	DataTable ListadoParametroDescuento(int CodEmpresa, decimal valor);
}
