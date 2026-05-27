using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteVentaxArticulo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet Reporte(int codPro, DateTime fecha1, DateTime fecha2, bool todo, int cod, int codAlmacen, int moneda)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportVentxArticule", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codpro", codPro);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("todos", todo);
			cmd.Parameters.AddWithValue("cod", cod);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("moned", moneda);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_VentaxArticulo");
			set.WriteXml("C:\\XML\\VentaxArticuloRPT.xml", XmlWriteMode.WriteSchema);
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
