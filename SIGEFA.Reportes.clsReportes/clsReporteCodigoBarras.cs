using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteCodigoBarras
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	private DataSet set = null;

	public DataSet CodigoBarras(int cod, int codalma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("Consultadatosetiqueta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codPedido_ex", cod);
			cmd.Parameters.AddWithValue("codAlmacen_ex", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "CodigodeBarras");
			set.WriteXml("C:\\XML\\RPTCodigoBarras.xml", XmlWriteMode.WriteSchema);
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
