using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITarjetaPago
{
	bool Insert(clsTarjetaPago NuevaTarjeta);

	bool Update(clsTarjetaPago Tarjeta);

	bool Delete(int CodTarjeta, int codAlmacen);

	clsTarjetaPago CargaTarjeta(int CodTarjeta, int codAlmacen);

	DataTable ListaTarjetas(int codAlmacen);

	double SumaTotalTarjetas(string fecha1, string fecha2, int almacen, int codtarjeta, int sucursal, int codcaja);
}
