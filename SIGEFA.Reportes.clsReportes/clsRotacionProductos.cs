using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsRotacionProductos
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet ReportRotacionProductos(bool Alma, bool Cri, DateTime fecha1, DateTime fecha2, int mes1, int mes2, string Annio, int codAlma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportRotacionProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("alma", Alma);
			cmd.Parameters.AddWithValue("cri", Cri);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("mes1", mes1);
			cmd.Parameters.AddWithValue("mes2", mes2);
			cmd.Parameters.AddWithValue("annio", Annio);
			cmd.Parameters.AddWithValue("codalma", codAlma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_RotacionProducto");
			set.WriteXml("C:\\XML\\RotacionProductoRPT.xml", XmlWriteMode.WriteSchema);
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
