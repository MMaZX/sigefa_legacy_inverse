using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IRequerimiento
{
	bool insert(clsRequerimiento Requerimiento);

	bool update(clsRequerimiento Requerimiento);

	bool delete(int CodigoRequerimiento);

	bool envio(int codAlmaDestino, int codreq);

	bool Atender(int codreq);

	bool rechazado(int CodigoRequerimiento, string comentario);

	bool anular(int CodigoRequerimiento);

	bool anularDetalle(int CodigoDetalleRequerimiento);

	bool insertdetalleRequerimiento(clsDetalleRequerimiento Detalle);

	bool updatedetalleRequerimiento(clsDetalleRequerimiento Detalle);

	bool deletedetalle(int CodigoDetalle);

	clsRequerimiento CargaRequerimiento(int CodOrden);

	DataTable CargaDetalle(int codReq);

	DataTable Cargaconsolidado(int codalma, int codprov);

	DataTable cargaRequerimientosTotales(int alma, int almaori);

	DataTable ListaRequerimiento(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable ListaRequerimientoHistorial(int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);

	DataTable ListaRequerimientoHistorial_x_almacen(int CodAlmacen, int almades, DateTime FechaInicial, DateTime FechaFinal, int tipo);

	bool actualiza_det_requerimientos_vigentes(clsDetalleRequerimiento Detalle);

	bool actualiza_det_requerimientos_comentario(int coddeta, string coment);

	bool actualiza_requerimientos_vigentes(clsRequerimiento Requerimiento);

	DataTable cargaRequerimientosTotales_x_requerimiento(int codrequerimiento_ex);
}
