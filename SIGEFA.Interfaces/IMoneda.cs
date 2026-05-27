using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IMoneda
{
	bool Insert(clsMoneda NuevaMoneda);

	bool Update(clsMoneda Moneda);

	bool Delete(int Codigo);

	clsMoneda CargaMoneda(int Codigo);

	DataTable CargaMonedasHabiles();

	DataTable ListaMonedas();

	DataTable CargaPais();

	clsMoneda Buscamoneda(string Moneda);

	int BuscamonedaXdescripcion(string descrip);
}
