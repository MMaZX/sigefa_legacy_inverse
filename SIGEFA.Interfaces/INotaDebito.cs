using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface INotaDebito
{
	bool insert(clsNotaDebito nota);

	bool actualizarCodNotaDebitoFV(int codFactura_venta, int codNota);

	bool insertdetalle(clsDetalleNotaDebito Detalle);
}
