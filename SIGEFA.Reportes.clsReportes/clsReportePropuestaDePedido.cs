using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReportePropuestaDePedido
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet propuestadepedidodeordendecompra(int codPropuesta, int visualizacion, int codEmpresa)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("Reporte_PropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("_codPropuesta", codPropuesta);
			cmd.Parameters.AddWithValue("_visualizacion", visualizacion);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_propuestadepedido");
			cmd = new MySqlCommand("MuestraEmpresaxAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("codempre", codEmpresa);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_tableempresa");
			set.WriteXml("C:\\XML\\PropuestaDePedidoOCRPT.xml", XmlWriteMode.WriteSchema);
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

	internal DataSet listadopropuestasdepedido(int tipo_prop, int codAlmacen, int codSucursal, int codEmpresa, int tipoFecha, DateTime desde, DateTime hasta)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("Reporte_ListadoPropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("_tipo_propuesta", tipo_prop);
			cmd.Parameters.AddWithValue("_codalma", codAlmacen);
			cmd.Parameters.AddWithValue("_codsuc", codSucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_fechaDesde", desde);
			cmd.Parameters.AddWithValue("_fechaHasta", hasta);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_listadopropuestadepedido");
			cmd = new MySqlCommand("MuestraEmpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("codempre", codEmpresa);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_tableempresa");
			set.WriteXml("C:\\XML\\ListadoPropuestaDePedidoOCRPT.xml", XmlWriteMode.WriteSchema);
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
