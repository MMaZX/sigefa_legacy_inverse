using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReportMercaderiaxEntregar
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet ReportMercaderiaEntregar(DateTime fecha1, DateTime fecha2, int CodAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportMercaderiaPorEntregar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fecini", fecha1);
			cmd.Parameters.AddWithValue("fecfin", fecha2);
			cmd.Parameters.AddWithValue("alma", CodAlmacen);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_MercaderiaxEntregar");
			set.WriteXml("C:\\XML\\MercaderiaxEntregarRPT.xml", XmlWriteMode.WriteSchema);
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
