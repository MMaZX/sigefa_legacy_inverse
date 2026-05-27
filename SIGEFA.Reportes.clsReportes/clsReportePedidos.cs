using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReportePedidos
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet RptPedidosPen(int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPedidos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\PedidosRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet RptDetallePedido(int codPedido)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportePedidoDetalle", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codpedido", codPedido);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\PedidoDetalleRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet RptMuestraEntregaConsultorExt(int CodConsExt)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("valoresEntregaConsultor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codconext", CodConsExt);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_cabeceraConsultor");
			cmd = new MySqlCommand("MuestraConsultorProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("codconext", CodConsExt);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_consultorProductos");
			set.WriteXml("C:\\XML\\ConsultorEnt.xml", XmlWriteMode.WriteSchema);
			return set;
		}
		catch (Exception ex)
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
