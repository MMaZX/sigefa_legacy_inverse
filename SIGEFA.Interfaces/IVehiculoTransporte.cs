using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IVehiculoTransporte
{
	bool Insert(clsVehiculoTransporte NuevoVehiculoTransporte);

	bool Update(clsVehiculoTransporte VehiculoTransporte);

	bool Delete(int Codigo);

	clsVehiculoTransporte CargaVehiculoTransporte(int Codigo);

	DataTable ListaVehiculoTransportes();

	DataTable CargaVehiculoTransportes();
}
