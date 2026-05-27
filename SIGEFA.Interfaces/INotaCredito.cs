using System;
using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface INotaCredito
{
	bool insert(clsNotaCredito Factura);

	bool insertdetalle(clsDetalleNotaCredito Detalle);

	DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2, int codsuc);

	DataTable ListaNotasCreditoAplicadas(int CodAlmacen, DateTime fecha1, DateTime fecha2, int codsuc);

	clsNotaCredito CargaNotaCredito(int CodNotaCredito);

	clsNotaCredito CargaNotaCredito_Regeneracion(int CodNotaCredito);

	DataTable CargaDetalle(int CodNotaCredito);

	List<clsNotaCredito> BuscarNotasXCliente(int codCliente);

	bool anular(int codNotaCredito);

	bool anularFactura_venta(int codFactura_venta);

	bool actualizarCodNotaCreditoFV(int codFactura_venta, int codNota, string tipoNotaCredito);

	bool actualizarStockNotaCredito(int codpro, double valor);

	DataTable NCsinRepositorio(int alma, DateTime fechaini, DateTime fechafin);

	DataTable ListaNotasCreditoXProducto(int codAlmacen, DateTime fecha1, DateTime fecha2, int codsuc, int codprod);
}
