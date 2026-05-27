using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IMetodoPago
{
	bool Insert(clsMetodoPago NuevoMetodoPago);

	bool Update(clsMetodoPago MetodoPago);

	bool Delete(int Codigo);

	clsMetodoPago CargaMetodoPago(int Codigo);

	clsMetodoPago BuscaMetodoPago(string Codigo);

	DataTable ListaMetodoPagos();

	DataTable CargaMetodoPagos();

	DataTable CargaMetodoPagosIE();
}
