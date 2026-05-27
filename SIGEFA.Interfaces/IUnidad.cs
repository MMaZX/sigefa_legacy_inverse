using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IUnidad
{
	bool Insert(clsUnidadMedida NuevaUnidad);

	bool Update(clsUnidadMedida Unidad);

	bool Delete(int Codigo);

	clsUnidadMedida CargaUnidad(int Codigo);

	DataTable ListaUnidades();

	DataTable ListaUnidades1();

	DataTable MuestraUnidadesEquivalentes();

	bool ActualizaPrecioEnDolares();

	int CantidadProductosDolares();

	int ValidaMoneda();
}
