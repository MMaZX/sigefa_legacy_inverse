using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteInformeSucursal
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet ReportVentasContCredSucursal(DateTime fecha1, DateTime fecha2, int CodSucursal)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportResumenVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fechaini", fecha1);
			cmd.Parameters.AddWithValue("fechafin", fecha2);
			cmd.Parameters.AddWithValue("codsucur", CodSucursal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ResumenVentas");
			set.WriteXml("C:\\XML\\ResumenVentasRPT.xml", XmlWriteMode.WriteSchema);
			return set;
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

	public DataSet ReportCobranzaSucursal(DateTime fecha1, DateTime fecha2, int CodSucursal)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportResumenCobranzas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fechaini", fecha1);
			cmd.Parameters.AddWithValue("fechafin", fecha2);
			cmd.Parameters.AddWithValue("codsucur", CodSucursal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ResumenCobranza");
			set.WriteXml("C:\\XML\\ResumenCobranzaRPT.xml", XmlWriteMode.WriteSchema);
			return set;
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
