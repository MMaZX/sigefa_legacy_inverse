using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IFormaPago
{
	bool Insert(clsFormaPago NuevoFormaPago);

	bool Update(clsFormaPago FormaPago);

	bool Delete(int Codigo);

	clsFormaPago CargaFormaPago(int Codigo);

	clsFormaPago BuscaFormaPago(string Codigo);

	DataTable ListaFormaPagos();

	DataTable CargaFormaPagos(int tip);

	DataTable CargaFormaPagosReportes();

	clsFormaPago BuscaFormaPagoVenta(int Codigo);

	DataTable CargaFormaPagosCuotas();
}
