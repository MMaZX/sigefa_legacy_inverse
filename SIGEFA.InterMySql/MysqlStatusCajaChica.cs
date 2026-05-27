using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlStatusCajaChica : IStatusCajaChica
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public clsStatusCajaChica CargaStatusFlujosCajaChica(DateTime FechaInicio, DateTime FechaFin)
	{
		clsStatusCajaChica sta = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ReporteStatusCajaChica", con.conector);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("fechafin", FechaFin);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					sta = new clsStatusCajaChica();
					sta.AperturaCaja = dr.GetDecimal(0);
					sta.Ingresos = dr.GetDecimal(1);
					sta.Egresos = dr.GetDecimal(2);
					sta.TotalVentas = dr.GetDecimal(3);
					sta.PorCobrar = dr.GetDecimal(4);
					sta.TotalPagos = dr.GetDecimal(5);
					sta.PorPagar = dr.GetDecimal(6);
					sta.SumaAperturasCaja = dr.GetDecimal(7);
					sta.SumaCierresCaja = dr.GetDecimal(8);
				}
			}
			return sta;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsStatusCajaChica CargaStatusFlujosCajaChica_SP(DateTime Fecha)
	{
		clsStatusCajaChica sta = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ReporteStatusCajaChica_SP", con.conector);
			cmd.Parameters.AddWithValue("fec", Fecha);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					sta = new clsStatusCajaChica();
					sta.AperturaCaja = dr.GetDecimal(0);
					sta.Ingresos = dr.GetDecimal(1);
					sta.Egresos = dr.GetDecimal(2);
					sta.TotalVentas = dr.GetDecimal(3);
					sta.PorCobrar = dr.GetDecimal(4);
					sta.TotalPagos = dr.GetDecimal(5);
					sta.PorPagar = dr.GetDecimal(6);
				}
			}
			return sta;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsStatusCajaChica CargaStatusFlujosCaja_SP(DateTime Fecha, int CodSucursal)
	{
		clsStatusCajaChica sta = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ReporteStatusCaja_SP", con.conector);
			cmd.Parameters.AddWithValue("fec", Fecha);
			cmd.Parameters.AddWithValue("sucur", CodSucursal);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					sta = new clsStatusCajaChica();
					sta.AperturaCaja = dr.GetDecimal(0);
					sta.Ingresos = dr.GetDecimal(1);
					sta.Egresos = dr.GetDecimal(2);
					sta.TotalVentas = dr.GetDecimal(3);
					sta.PorCobrar = dr.GetDecimal(4);
					sta.TotalPagos = dr.GetDecimal(5);
					sta.PorPagar = dr.GetDecimal(6);
					sta.VentasCreditoDia = dr.GetDecimal(7);
					sta.SumaVentasEfectivoDia = dr.GetDecimal(8);
					sta.SumaVentasDepositoDia = dr.GetDecimal(9);
					sta.SumaVentasChequeDia = dr.GetDecimal(10);
					sta.SumaVentasTarjetaDia = dr.GetDecimal(11);
					sta.SumaVentasTransferenciaDia = dr.GetDecimal(12);
					sta.MontoDepositado = dr.GetDecimal(13);
					sta.SumaVentasEfectivoMes = dr.GetDecimal(14);
					sta.SumaVentasDepositoMes = dr.GetDecimal(15);
					sta.SumaVentasChequeMes = dr.GetDecimal(16);
					sta.SumaVentasTarjetaMes = dr.GetDecimal(17);
					sta.SumaVentasTransferenciaMes = dr.GetDecimal(18);
					sta.TotalVentasMes = dr.GetDecimal(19);
					sta.PorCobrarMes = dr.GetDecimal(20);
					sta.VentasCreditoMes = dr.GetDecimal(21);
					sta.TotalVentasDolar = dr.GetDecimal(22);
					sta.PorCobrarDolar = dr.GetDecimal(23);
					sta.TotalPagosDolar = dr.GetDecimal(24);
					sta.PorPagarDolar = dr.GetDecimal(25);
					sta.VentasCreditoDiaDolar = dr.GetDecimal(26);
					sta.SumaVentasEfectivoDiaDolar = dr.GetDecimal(27);
					sta.SumaVentasDepositoDiaDolar = dr.GetDecimal(28);
					sta.SumaVentasChequeDiaDolar = dr.GetDecimal(29);
					sta.SumaVentasTarjetaDiaDolar = dr.GetDecimal(30);
					sta.SumaVentasTransferenciaDiaDolar = dr.GetDecimal(31);
					sta.SumaVentasEfectivoMesDolar = dr.GetDecimal(32);
					sta.SumaVentasDepositoMesDolar = dr.GetDecimal(33);
					sta.SumaVentasChequeMesDolar = dr.GetDecimal(34);
					sta.SumaVentasTarjetaMesDolar = dr.GetDecimal(35);
					sta.SumaVentasTransferenciaMesDolar = dr.GetDecimal(36);
					sta.TotalVentasMesDolar = dr.GetDecimal(37);
					sta.PorCobrarMesDolar = dr.GetDecimal(38);
					sta.VentasCreditoMesDolar = dr.GetDecimal(39);
					sta.CobranzaSolesTotal = dr.GetDecimal(40);
					sta.CobranzaDolaresTotal = dr.GetDecimal(41);
				}
			}
			return sta;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsStatusCajaChica VerificaStadoCajaChica()
	{
		clsStatusCajaChica sta = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaStatusCajaChica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					sta = new clsStatusCajaChica();
					sta.Cantidad = dr.GetInt32(0);
				}
			}
			return sta;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsStatusCajaChica VerificaStadoCaja()
	{
		clsStatusCajaChica sta = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaStatusCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					sta = new clsStatusCajaChica();
					sta.Cantidad = dr.GetInt32(0);
				}
			}
			return sta;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public clsStatusCajaChica CargaStatusFlujosCaja(DateTime FechaInicio, DateTime FechaFin, int CodSucursal)
	{
		clsStatusCajaChica sta = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ReporteStatusCaja", con.conector);
			cmd.Parameters.AddWithValue("fechaini", FechaInicio);
			cmd.Parameters.AddWithValue("fechafin", FechaFin);
			cmd.Parameters.AddWithValue("sucur", CodSucursal);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					sta = new clsStatusCajaChica();
					sta.AperturaCaja = dr.GetDecimal(0);
					sta.Ingresos = dr.GetDecimal(1);
					sta.Egresos = dr.GetDecimal(2);
					sta.TotalVentas = dr.GetDecimal(3);
					sta.PorCobrar = dr.GetDecimal(4);
					sta.TotalPagos = dr.GetDecimal(5);
					sta.PorPagar = dr.GetDecimal(6);
					sta.VentasCreditoDia = dr.GetDecimal(7);
					sta.SumaVentasEfectivoDia = dr.GetDecimal(8);
					sta.SumaVentasDepositoDia = dr.GetDecimal(9);
					sta.SumaVentasChequeDia = dr.GetDecimal(10);
					sta.SumaVentasTarjetaDia = dr.GetDecimal(11);
					sta.SumaVentasTransferenciaDia = dr.GetDecimal(12);
					sta.MontoDepositado = dr.GetDecimal(13);
					sta.SumaVentasEfectivoMes = dr.GetDecimal(14);
					sta.SumaVentasDepositoMes = dr.GetDecimal(15);
					sta.SumaVentasChequeMes = dr.GetDecimal(16);
					sta.SumaVentasTarjetaMes = dr.GetDecimal(17);
					sta.SumaVentasTransferenciaMes = dr.GetDecimal(18);
					sta.TotalVentasMes = dr.GetDecimal(19);
					sta.PorCobrarMes = dr.GetDecimal(20);
					sta.VentasCreditoMes = dr.GetDecimal(21);
					sta.TotalVentasDolar = dr.GetDecimal(22);
					sta.PorCobrarDolar = dr.GetDecimal(23);
					sta.TotalPagosDolar = dr.GetDecimal(24);
					sta.PorPagarDolar = dr.GetDecimal(25);
					sta.VentasCreditoDiaDolar = dr.GetDecimal(26);
					sta.SumaVentasEfectivoDiaDolar = dr.GetDecimal(27);
					sta.SumaVentasDepositoDiaDolar = dr.GetDecimal(28);
					sta.SumaVentasChequeDiaDolar = dr.GetDecimal(29);
					sta.SumaVentasTarjetaDiaDolar = dr.GetDecimal(30);
					sta.SumaVentasTransferenciaDiaDolar = dr.GetDecimal(31);
					sta.SumaVentasEfectivoMesDolar = dr.GetDecimal(32);
					sta.SumaVentasDepositoMesDolar = dr.GetDecimal(33);
					sta.SumaVentasChequeMesDolar = dr.GetDecimal(34);
					sta.SumaVentasTarjetaMesDolar = dr.GetDecimal(35);
					sta.SumaVentasTransferenciaMesDolar = dr.GetDecimal(36);
					sta.TotalVentasMesDolar = dr.GetDecimal(37);
					sta.PorCobrarMesDolar = dr.GetDecimal(38);
					sta.VentasCreditoMesDolar = dr.GetDecimal(39);
					sta.CobranzaSolesTotal = dr.GetDecimal(40);
					sta.CobranzaDolaresTotal = dr.GetDecimal(41);
				}
			}
			return sta;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}
}
