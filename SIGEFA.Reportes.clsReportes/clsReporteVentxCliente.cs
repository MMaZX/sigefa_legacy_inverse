using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteVentxCliente
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet Reporte(int emp, DateTime fecha1, DateTime fecha2, int forma, bool todoCli, bool todoArt, string refe, string cod, int moned)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportVentxClient", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("empre", emp);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("forma", forma);
			cmd.Parameters.AddWithValue("todoCli", todoCli);
			cmd.Parameters.AddWithValue("todoArt", todoArt);
			cmd.Parameters.AddWithValue("refe", refe);
			cmd.Parameters.AddWithValue("cod", cod);
			cmd.Parameters.AddWithValue("moned", moned);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\VentaxClienteDiaRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReporteVentasxCliente(int emp, DateTime fecha1, DateTime fecha2, bool todoCli, bool todoArt, string refe, string cod)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportVentxClient2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 150000;
			cmd.Parameters.AddWithValue("empre", emp);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("todoCli", todoCli);
			cmd.Parameters.AddWithValue("todoArt", todoArt);
			cmd.Parameters.AddWithValue("refe", refe);
			cmd.Parameters.AddWithValue("cod", cod);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\VentaxClienteRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet Reporte22(int codSu, DateTime fecha1, DateTime fecha2, int codVen, string codArticulo1, string codArticulo2, bool todoArt, bool unArt)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportVentxVendedorxArti", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("empre", codSu);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("cod", codVen);
			cmd.Parameters.AddWithValue("rango1", codArticulo1);
			cmd.Parameters.AddWithValue("rango2", codArticulo2);
			cmd.Parameters.AddWithValue("todoArt", todoArt);
			cmd.Parameters.AddWithValue("unArt", unArt);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\VentaxArticuloxVendedor.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReporteVentasCliente(int codSu, DateTime fecha1, DateTime fecha2, int forma, bool todoCli, string refe, int moned)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteVentasxCliente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("empre", codSu);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("forma", forma);
			cmd.Parameters.AddWithValue("todoCli", todoCli);
			cmd.Parameters.AddWithValue("refe", refe);
			cmd.Parameters.AddWithValue("moned", moned);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\VentasxCliente.xml", XmlWriteMode.WriteSchema);
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
