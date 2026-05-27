using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICotizacion
{
	bool insert(clsCotizacion Cotizacion);

	bool update(clsCotizacion Cotizacion);

	bool updateAprobado(int CodCotizacion);

	bool DuplicarCotizacion(int CodCotizacion);

	bool ValidarDescuento(int CodCotizacion);

	bool delete(int CodigoCotizacion, int codusuario, string mensaje);

	bool insertdetalle(clsDetalleCotizacion Detalle);

	bool updatedetalle(clsDetalleCotizacion Detalle);

	bool deletedetalle(int CodigoDetalle);

	clsCotizacion CargaCotizacion(int CodCotizacion, int CodAlmacen);

	clsCotizacion BuscaCotizacion(string CodCotizacion, int CodAlmacen);

	clsDetalleCotizacion CargaDetalleCotizacion(int CodCotizacion, int CodAlmacen);

	DataTable CargaDetalle(int CodCotizacion, int CodAlmacen);

	DataTable CargaDatosRecalcular(int Codproducto, int Codunidad, int CodAlmacen);

	DataTable CargaDetalleVenta(int CodCotizacion, int CodAlmacen);

	DataTable ListaCotizaciones(int CodAlmacen);

	DataTable ListaCotizacionesxVigente(int CodAlmacen, int estado, DateTime fecha1, DateTime fecha2);

	bool CotizacionesVencidas();
}
