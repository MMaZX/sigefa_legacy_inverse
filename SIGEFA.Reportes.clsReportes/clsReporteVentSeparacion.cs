using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteVentSeparacion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet Reporte(int emp, DateTime fecha1, DateTime fecha2, int forma, int suc, int almacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportVentSeparacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("empre", emp);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("forma", forma);
			cmd.Parameters.AddWithValue("su", suc);
			cmd.Parameters.AddWithValue("alma", almacen);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\VentaSeparacionRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReporteSeparacion(int alma, DateTime fecha1, DateTime fecha2, int estado)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraVentas_Xseparacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codalma", alma);
			cmd.Parameters.AddWithValue("fechaini", fecha1);
			cmd.Parameters.AddWithValue("fechafin", fecha2);
			cmd.Parameters.AddWithValue("estad", estado);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\ListaVentaSeparacionRPT.xml", XmlWriteMode.WriteSchema);
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
