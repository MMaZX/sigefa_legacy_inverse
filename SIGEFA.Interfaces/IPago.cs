using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IPago
{
	bool Insert(clsPago NuevoPago);

	bool InsertPagoPendiente(clsPago NuevoPago);

	bool Insertpagomultiple(clsPago NuevoPago);

	bool InsertPagoDetraccion(clsPago pag);

	DataTable MuestraListaPagosNota(int CodNotaIngreso, bool InOut, int Tipo, int codAlmacen);

	DataTable MuestraPagoVentaAnula(int codAlmacen, int nota);

	bool AnularPago(int CodigoPago);

	bool AnularPagoPendiente(int CodigoPago);

	bool ActualizarSeleccion(int CodigoPago, string Color);

	clsPago MuestraPagoVenta(int codAlmacen, int venta);

	DataTable MuestraPagosPorAprobar(int Estado, DateTime Fecha1, DateTime Fecha2);

	DataTable MuestraPagosDetraccion(int Estado, DateTime Fecha1, DateTime Fecha2);

	bool AprobarPago(int codigo, int valor);

	DataTable MuestraListaPagosNota2(int CodNotaIngreso, bool InOut, int Tipo);

	bool ActualizaPagoAprobado(string ser, string numdoc, int codpag);

	DataTable GetPagosVenta(int codigoalmacen, int codigoventa);

	int obtenerOpcionSuma(int codpago);
}
