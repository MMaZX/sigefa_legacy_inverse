using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteNotaCredito
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet NotaCredito(int cod, int alm)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteNotaCredito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("cod", cod);
			cmd.Parameters.AddWithValue("alm", alm);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_NotaCredito");
			set.WriteXml("C:\\XML\\NotaCreditoRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataTable ReporteNotaCreditoDiaria(DateTime fecha, int codalma)
	{
		try
		{
			DataTable tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteNotasCreditosExcel", con.conector);
			cmd.Parameters.AddWithValue("fecha", fecha);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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
