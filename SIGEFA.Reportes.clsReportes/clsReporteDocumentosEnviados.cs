using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteDocumentosEnviados
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet DocumentosEnviados(int codAlmacen, DateTime fechaInicio, DateTime fechaFin, int codDocumento)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteDocumentosElectronicosEnviados", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codigo_almacen", codAlmacen);
			cmd.Parameters.AddWithValue("fecha_inicio", fechaInicio);
			cmd.Parameters.AddWithValue("fecha_fin", fechaFin);
			cmd.Parameters.AddWithValue("codigo_documento", codDocumento);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\DocumentosEnviadosRPT.xml", XmlWriteMode.WriteSchema);
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
