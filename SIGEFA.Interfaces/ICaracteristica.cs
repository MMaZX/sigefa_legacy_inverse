using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICaracteristica
{
	bool Insert(clsCaracteristica NuevaCaracteristica);

	bool Update(clsCaracteristica Caracteristica);

	bool Delete(int Codigo);

	clsCaracteristica CargaCaracteristica(int Codigo);

	DataTable ListaCaracteristicas();
}
