using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IArqueoFondoFijo
{
	bool Insert(clsArqueoFondoFijo NuevoArqueo);

	bool insertDetalle(clsDetalleArqueFondoFijo det);

	DataTable ListaDinero(int tipo);

	decimal TraeValor(int codigo);
}
