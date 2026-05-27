using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IZona
{
	bool Insert(clsZona NuevaZona);

	bool Update(clsZona Zona);

	bool Delete(int Codigo);

	clsZona CargaZona(int Codigo);

	DataTable ListaZonas();

	bool InsertDestaque(clsDestaque NuevaDestauque);

	bool DeleteDestaque(int Codigo);

	DataTable ListaDestaques();

	DataTable ListaZonaDestaque();

	DataTable CargaZonasReporte();
}
