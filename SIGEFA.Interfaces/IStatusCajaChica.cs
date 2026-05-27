using System;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IStatusCajaChica
{
	clsStatusCajaChica CargaStatusFlujosCajaChica(DateTime FechaInicio, DateTime FechaFin);

	clsStatusCajaChica CargaStatusFlujosCajaChica_SP(DateTime Fecha);

	clsStatusCajaChica VerificaStadoCajaChica();

	clsStatusCajaChica CargaStatusFlujosCaja(DateTime FechaInicio, DateTime FechaFin, int CodSucursal);

	clsStatusCajaChica CargaStatusFlujosCaja_SP(DateTime Fecha, int CodSucursal);

	clsStatusCajaChica VerificaStadoCaja();
}
