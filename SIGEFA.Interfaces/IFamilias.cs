using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IFamilias
{
	bool Insert(clsFamilia NuevaFamilia);

	bool Update(clsFamilia Familia);

	bool Delete(int Codigo);

	clsFamilia CargaFamilia(int Codigo);

	DataTable ListaFamilias();

	DataTable MuestraLinea();
}
