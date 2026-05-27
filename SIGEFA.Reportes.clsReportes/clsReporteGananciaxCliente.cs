using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

public class clsReporteGananciaxCliente
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet ReportGananciaxCliente(int codigoCliente, DateTime fecha_inicio, DateTime fecha_fin)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportGananciaPorCliente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codigo_cliente", codigoCliente);
			cmd.Parameters.AddWithValue("fecha_inicio", fecha_inicio);
			cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteGananciaPorCliente");
			set.WriteXml("C:\\XML\\GananciaPorClienteRPT.xml", XmlWriteMode.WriteSchema);
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
