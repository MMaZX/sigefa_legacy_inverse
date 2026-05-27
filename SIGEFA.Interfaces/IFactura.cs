using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IFactura
{
	bool insert(clsFactura Factura);

	bool update(clsFactura Factura);

	bool delete(string CodigoFactura);

	bool anular(int CodigoFactura);

	bool anular_1(int CodigoFactura, int coduser);

	bool activar(string CodigoFactura);

	bool insertdetalle(clsDetalleFactura Detalle);

	bool updatedetalle(clsDetalleFactura Detalle);

	bool deletedetalle(int CodigoDetalle);

	clsFactura CargaFactura(int CodFactura);

	DataTable CargaDetalle(int CodFactura);

	DataTable ListaFactura(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable ListaNotasCredito(int CodAlmacen, DateTime fecha1, DateTime fecha2);

	DataTable MuestraPagos(int Estado, int codEmpresa, DateTime Fecha1, DateTime Fecha2, int tipoBusqueda);

	DataTable ListaFactura(int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int alma);

	DataTable ListaFacturaCombo(DateTime FechaInicial, DateTime FechaFinal, int alma, int estado);

	DataTable ListaFacturaxProducto(DateTime FechaInicial, DateTime FechaFinal, int alma, int codprod);

	DataTable ListaFactura_Facturacion(DateTime FechaInicial, DateTime FechaFinal, int alma);

	DataTable MuestraFacturasProveedor(int alma, int codprom, int tipo);

	bool ActualizaPendienteCreditoCompra(double monto, int codcompra, int codalm);

	DataTable ListaNotasDebitoCompra(int CodAlmacen, DateTime fecha1, DateTime fecha2);

	string guiasPorFactura(int codfactura);

	bool verificaFleteFactura(int codFactura);
}
