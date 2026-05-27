using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteCaja
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet RptMuestraCierreCaja(int codSucursal, DateTime fecha1, int codcaja, int codalmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("listaDetallesCierre", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("codcaja_ex", codcaja);
			cmd.Parameters.AddWithValue("codalma_ex", codalmacen);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ingresosCierre");
			cmd = new MySqlCommand("GeneraCierreCabeceraSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_sucursalCierre");
			cmd = new MySqlCommand("valoresCierraCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("codcaja_ex", codcaja);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_totales");
			cmd = new MySqlCommand("MuestraTotalSeparacionCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fe", fecha1);
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("codcaja", codcaja);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_totalseparacion");
			set.WriteXml("C:\\XML\\CierreCajaDiario.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataSet RptMuestraCierreCajaPorUsuario(int codSucursal, DateTime fecha1, int codcaja, int codalmacen, int codUser)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("listaDetallesCajaxUsuario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("codcaja_ex", codcaja);
			cmd.Parameters.AddWithValue("codalma_ex", codalmacen);
			cmd.Parameters.AddWithValue("codUser", codUser);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ingresosCierrePorUsuario");
			cmd = new MySqlCommand("GeneraCierreCabeceraSucursalxUsuario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_sucursalCierrePorUsuario");
			cmd = new MySqlCommand("valoresCierraCajaxUsuario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("codcaja_ex", codcaja);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_totalesPorUsuario");
			set.WriteXml("C:\\XML\\CierreCajaDiarioPorUsuario.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataTable ListadoUsuariosCierreCaja(int codSucursal, DateTime fecha1, int codcaja, int codalmacen)
	{
		try
		{
			DataTable tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listaDetallesCajaListadoUsuarios", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("codcaja_ex", codcaja);
			cmd.Parameters.AddWithValue("codalma_ex", codalmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
		}
		catch (Exception ex)
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

	public DataSet ReporteMovimientosCajaVentas(int CodSucursal, DateTime fecha, int CodCaja, int codalma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteMovimientosCajaVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codSucur", CodSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha);
			cmd.Parameters.AddWithValue("codcaja_ex", CodCaja);
			cmd.Parameters.AddWithValue("codalma_ex", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_RTMovimientosCajaVentas");
			set.WriteXml("C:\\XML\\ReporteMovimientosCajaVentasRPT.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataSet DatosSucursal(int CodSucursal)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("GeneraCierreCabeceraSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codSucur", CodSucursal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_RTSucursal");
			set.WriteXml("C:\\XML\\SucursalRPT.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataSet ReciboDietasyEstimulo(int tipo, int Codigo)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReproteReciboEgresosRpt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codmovimiento_ex", Codigo);
			cmd.Parameters.AddWithValue("tipo_movimiento_ex", tipo);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReproteReciboEgresos");
			set.WriteXml("C:\\XML\\ReproteReciboEgresosRpt.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataSet ReciboCajaChica(int tipo, int Codigo)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReciboCajaChicaRpt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codRecibo_ex", Codigo);
			cmd.Parameters.AddWithValue("codtipopagocajachica_ex", tipo);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReciboCajaChica");
			set.WriteXml("C:\\XML\\ReciboCajaChicaRpt.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataSet ReporteMovimientosCajaChica(int CodSucursal, DateTime fecha, int codigocaja, int codalma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteMovimientosCajaChicaRpt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codSucur", CodSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha);
			cmd.Parameters.AddWithValue("codcaja_ex", codigocaja);
			cmd.Parameters.AddWithValue("codalma_ex", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteMovimientosCajaChica");
			set.WriteXml("C:\\XML\\ReporteMovimientosCajaChicaRpt.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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

	public DataSet ReporteArqueoFondoFijo(int CodArqueo)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteArqueoFondoFijoRpt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codarqueofondodijo_ex", CodArqueo);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteArqueoFondoFijo");
			set.WriteXml("C:\\XML\\ReporteArqueoFondoFijoRpt.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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
