using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteRankingxCliente
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet Reporte(DateTime fecha1, DateTime fecha2)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteRankingVentasCliente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_RankingCliente");
			set.WriteXml("C:\\XML\\RankingClienteRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet Reporte_Ranking(DateTime fecha1, DateTime fecha2, int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportRankingVentaxCliente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_RankingCliente");
			set.WriteXml("C:\\XML\\RankingClienteRPT.xml", XmlWriteMode.WriteSchema);
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
