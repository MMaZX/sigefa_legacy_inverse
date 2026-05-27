using System;
using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface ICtaCte
{
	bool Insert(clsCtaCte cta);

	bool Update(clsCtaCte cta);

	bool Delete(int codCtaCte, int codAlmacen);

	DataTable ListaCtasBanco(int CodBanco, int codAlmacen);

	DataTable ListaCtaCte(int codAlmacen);

	clsCtaCte CargaTipoCuenta(int CodCuenta, int codAlmacen);

	decimal TotalConciliacion(int codAlmacen, int codBanco, int codCuenta);

	DataTable CargarMovxCuenta(string Cuenta, int codAlmacen);

	clsCtaCte BuscaMovimiento(int codMov, int codAlmacen);

	bool InsertMovi(clsCtaCte cta);

	bool UpdateMovi(clsCtaCte cta);

	bool DeleteMov(int CodMov, int codAlmacen);

	DataTable ListaMovimientos(int codAlmacen);

	DataTable ListarMovientoscta(int codAlmacen, int codBanco, int codCuenta);

	DataTable ListaEgresosCaja(int CodSucursal, DateTime Fecha);

	DataTable ListatipoCtas_x_Banco(int CodBanco, int CodAlmacen);

	DataTable ListanumCta_x_tipocta(int CodBanco, string tipocuenta, int codAlmacen);

	DataTable ListaCaja(int codSucursal, DateTime fecha);

	clsCtaCte VerificaEgresoCaja(int CodSucursal, DateTime fecha);

	DataTable ListaCtaCtexBancoxMoneda(int codBanco, int codMoneda);

	DataTable ListaBancoxMoneda(int codMoneda);

	int Correlativo(int codtipo);

	bool activar(int codtipo);

	bool desactivar(int codigo);

	DataTable ListaMovimientosDesactivos(int codbanco, int codcuenta, int codAlmacen);
}
