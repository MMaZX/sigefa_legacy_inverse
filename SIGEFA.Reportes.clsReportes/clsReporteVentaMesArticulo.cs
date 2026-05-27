using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteVentaMesArticulo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet Reporte(int mes1, int mes2, int forma, int cri, string refe, bool todo, int moned, int codAlmacen, int annio)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteVentaMesArticulo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("mes1", mes1);
			cmd.Parameters.AddWithValue("mes2", mes2);
			cmd.Parameters.AddWithValue("forma", forma);
			cmd.Parameters.AddWithValue("cri", cri);
			cmd.Parameters.AddWithValue("refe", refe);
			cmd.Parameters.AddWithValue("todo", todo);
			cmd.Parameters.AddWithValue("moned", moned);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("annio", annio);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\VentaMesArticuloRPT.xml", XmlWriteMode.WriteSchema);
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
