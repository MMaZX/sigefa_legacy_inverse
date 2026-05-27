using System;
using System.Collections.Generic;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IPedido
{
	bool insert(clsPedido Pedido);

	bool insertarOrdenVenta(clsPedido pedido);

	bool insertarOrdenVentaSinStock(clsPedido pedido);

	bool update(clsPedido Pedido);

	bool delete(int CodigoPedido);

	bool liquidar(int CodigoPedido);

	bool insertdetalle(clsDetallePedido Detalle);

	bool insertdetalleSinStock(clsDetallePedido Detalle);

	bool updatedetalle(clsDetallePedido Detalle);

	bool updatedetalleadicional(clsDetallePedido Detalle);

	bool deletedetalle(int CodigoDetalle);

	clsPedido CargaPedido(int CodPedido);

	List<clsDetallePedido> cargaDetallePedido(int codPedido);

	clsPedido BuscaPedido(string CodPedido, int CodAlmacen);

	DataTable CargaDetalle(int CodPedido);

	DataTable CargaDetalle2(int CodPedido, int CodAlmacen);

	DataTable CargaDetalleGuia(int CodPedido);

	DataTable ListaPedidos(int user, int CodAlmacen, DateTime desde, DateTime hasta);

	clsPedido CargaEntrega(int CodEntrega);

	DataTable CargaDetalleEntrega(int CodEntrega);

	bool insertEntConsExt(clsPedido Pedido);

	DataTable MuestraEntregasConsultorExt(int CodAlmacen, DateTime Fecha1, DateTime Fecha2);

	bool insertdetalleconsultor(clsDetallePedido Detalle);

	DataTable ListaPedidosTodos(int user, int CodAlmacen, DateTime desde, DateTime hasta);

	bool updatedetallesalidaconsultext(clsDetallePedido Detalle);

	bool deleteEntConsExt(int CodEntConExt);

	bool rollbackpedido(int codpedido);

	bool GuardaCodigoBarras(clsPedido pedido);

	clsPedido CargaPedidoxAlmacen(int CodPedido, int codalma);

	bool cambiaMotivoRetencion(int codPedido, bool valorRetencion);

	bool activaPedidoVenta(int codpedido);
}
