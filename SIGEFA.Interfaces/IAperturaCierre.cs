using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IAperturaCierre
{
	bool Insert(clsCaja AperturaNuevo);

	bool UpdateApertura(clsCaja Apertura);

	bool UpdateCierre(clsCaja Cierre);

	bool AnularCierre(int codAlmacen);

	clsCaja CargaAperturaCaja(int codAlmacen);

	clsCaja CargaCierreCaja(int codAlmacen);

	clsCaja GetUltimaCajaVentas(int codAlmacen, int tipocaja, int codalma);

	clsCaja ValidarAperturaDia(int codSucursal, DateTime fecha1, int tipocaja, int codalma, int CodUser);

	clsCaja ListaTotalesVentas(int codSucursal, DateTime fechaInicio, DateTime fechaFin, int tipocaja, int codalma, int CodUser);

	bool InsertAperturaCaja(clsCaja AperturaNuevo);

	clsCaja CargaCierreAnterior(int iCodSucursal, int tipocaja);

	DataTable ListaCierresDiarios(int codSucursal, DateTime desde, DateTime hasta);

	decimal SumaVentaEfectivoCaja(int codSuc, DateTime fech1, int codigocaja);

	decimal SumaVentasEfectivoCaja(int codSuc, DateTime fechaDesde, DateTime fechaHasta, int codigocaja);

	DataTable ListaCajaDiaria(int codSucursal, DateTime fecha1, int codigocaja, int codalma, int codEstadoCaja);

	bool CerrarCajaVentas(int codSucursal, DateTime fecha1, int codcaja, int codalma);

	bool InsertMovCajaChica(clsCajaChicaMov movchi);

	DataTable ListaCajaChica(int codSucursal, DateTime fecha1, int codigocaja, int codalma);

	DataTable ConsultaCajas(int almacen, DateTime fecha1, DateTime fecha2);

	decimal traersaldo();

	clsCaja GetCaja(int codSucursal, DateTime fecha1, int tipocaja, int codalma);

	DataTable getVentasNoEstanEnCajaMovimientos(DateTime fechaCaja, int codAlmacen, int iCodSucursal, int codCaja);
}
