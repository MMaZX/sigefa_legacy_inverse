using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ITipoCambio
{
	bool Insert(clsTipoCambio NuevoTipoCambio);

	bool Update(clsTipoCambio TipoCambio);

	bool Delete(int Codigo);

	clsTipoCambio CargaTipoCambio(DateTime Fecha, int codMoneda);

	bool VerificaTCfecha(DateTime Fecha);

	DataTable ListaTipoCambios(int Mes, int Año);
}
