using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteTransferencia
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet RptTransferencia(DateTime fecha1, DateTime fecha2, int codalmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteDeTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1500;
			cmd.Parameters.AddWithValue("codalma", codalmacen);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_transferenciaProductos");
			set.WriteXml("C:\\XML\\ReporteDeTransferencia.xml", XmlWriteMode.WriteSchema);
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
