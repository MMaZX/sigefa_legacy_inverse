using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteCuotas
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet CuotasPrestamo(int codPrestamo)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("CuotasPrestamo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1500;
			cmd.Parameters.AddWithValue("codpre", codPrestamo);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_cuotas");
			set.WriteXml("C:\\XML\\CuotasPrestamo.xml", XmlWriteMode.WriteSchema);
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
