using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IFlujoCaja
{
	bool Insert(clsFlujoCaja flu);

	bool Update(clsFlujoCaja flu);

	bool Delete(int CodFlujoCaja, int CodSucursal);

	clsFlujoCaja CargaFlujosCaja(DateTime fecha, int CodSucursal);

	DataTable ListaFlujosCaja(int codSucursal);

	DataTable ListaPagoCobro(int tipo);

	clsFlujoCaja VerificaSaldoCaja(int CodSucursal);

	int VerificaAperturaCaja(int codSucursal);

	clsFlujoCaja VerificaDepositoCaja(int CodSucursal, DateTime fecha);
}
