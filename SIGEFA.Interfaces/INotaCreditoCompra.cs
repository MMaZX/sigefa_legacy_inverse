using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface INotaCreditoCompra
{
	bool insert(clsNotaSalida notaS);

	DataTable cargaTipoNCC();

	int getAccionSegunTipoSeleccionado(int seleccionado);

	bool verificarCodProductoAgregableANotaDeCredito(int codProducto);

	bool insert(clsNotaCredito nota_credito);

	bool insertdetalle(clsDetalleNotaCredito detalle);

	bool setCodNotaSalida(string codNotaSalida, int codNotaCreditoNueva);

	DataTable ListadoEstandarNotaCreditoCompra(int codAlmacen, int codSucursal, int tipoFecha, DateTime date1, DateTime date2, int codProd);

	clsNotaCredito cargaNotaCredito(int codNotaCC);

	DataTable cargaDetalleNCC(string codNotaCredito);

	bool actualizaSerieyCorrelativo(string codNotaCredito, string serie, string numdoc);

	bool actualizaAsignador(string codNotaCredito, int codUser, DateTime fecha);

	bool actualizaEstado(string codNotaCredito, int estado);

	bool verificarCodFacturaTieneNotaCredito(int codFactura);

	bool actualizaFormaPago(string codNotaCredito, int estadoFormaPago);
}
