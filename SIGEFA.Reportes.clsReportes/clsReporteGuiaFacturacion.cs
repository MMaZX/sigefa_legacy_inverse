using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteGuiaFacturacion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet Imprimir(int codguia, int afectacion)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteGuiaFacturacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("cod", codguia);
			cmd.Parameters.AddWithValue("afectacion", afectacion);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dtGuia");
			set.WriteXml("C:\\XML\\GuiaRPT.xml", XmlWriteMode.WriteSchema);
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
