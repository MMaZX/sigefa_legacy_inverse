using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

public class clsReporteGananciaxArticulo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet ReportGananciaxArticulo(int codigoProducto, DateTime fecha_inicio, DateTime fecha_fin)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportGananciaPorArticulo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1500000000;
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("fecha_inicio", fecha_inicio);
			cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteGananciaPorArticulo");
			set.WriteXml("C:\\XML\\GananciaPorArticuloRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReportGananciaxArticulo2(int codigoProducto, DateTime fecha_inicio, DateTime fecha_fin, int todos, int cod1, int cod2, int cod3, int cod4, int marcas, int marca1, int marca2, int marca3, int marca4, int marca5, int familias, int fam1, int fam2, int fam3, int fam4, int fam5, int proveedores, int prov1, int prov2, int prov3, int prov4, int prov5)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteGananciaxArticulo2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1500000000;
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("fecha_inicio", fecha_inicio);
			cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
			cmd.Parameters.AddWithValue("todos", todos);
			cmd.Parameters.AddWithValue("cod1", cod1);
			cmd.Parameters.AddWithValue("cod2", cod2);
			cmd.Parameters.AddWithValue("cod3", cod3);
			cmd.Parameters.AddWithValue("cod4", cod4);
			cmd.Parameters.AddWithValue("marcas", marcas);
			cmd.Parameters.AddWithValue("marca1", marca1);
			cmd.Parameters.AddWithValue("marca2", marca2);
			cmd.Parameters.AddWithValue("marca3", marca3);
			cmd.Parameters.AddWithValue("marca4", marca4);
			cmd.Parameters.AddWithValue("marca5", marca5);
			cmd.Parameters.AddWithValue("familias", familias);
			cmd.Parameters.AddWithValue("fam1", fam1);
			cmd.Parameters.AddWithValue("fam2", fam2);
			cmd.Parameters.AddWithValue("fam3", fam3);
			cmd.Parameters.AddWithValue("fam4", fam4);
			cmd.Parameters.AddWithValue("fam5", fam5);
			cmd.Parameters.AddWithValue("proveedores", proveedores);
			cmd.Parameters.AddWithValue("prov1", prov1);
			cmd.Parameters.AddWithValue("prov2", prov2);
			cmd.Parameters.AddWithValue("prov3", prov3);
			cmd.Parameters.AddWithValue("prov4", prov4);
			cmd.Parameters.AddWithValue("prov5", prov5);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteGananciaPorArticulo2");
			set.WriteXml("C:\\XML\\GananciaPorArticulo2RPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReportGananciaxArticulo2_Clientes(int codigoProducto, DateTime fecha_inicio, DateTime fecha_fin, int todos, int cod1, int cod2, int cod3, int cod4, int clientes, int cli1, int cli2, int cli3, int cli4, int cli5, int todostec, int tec1, int tec2, int tec3, int tec4, int tec5)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteGananciaxArticulo2_Clientes", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1500000000;
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("fecha_inicio", fecha_inicio);
			cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
			cmd.Parameters.AddWithValue("todos", todos);
			cmd.Parameters.AddWithValue("cod1", cod1);
			cmd.Parameters.AddWithValue("cod2", cod2);
			cmd.Parameters.AddWithValue("cod3", cod3);
			cmd.Parameters.AddWithValue("cod4", cod4);
			cmd.Parameters.AddWithValue("clientes", clientes);
			cmd.Parameters.AddWithValue("cli1", cli1);
			cmd.Parameters.AddWithValue("cli2", cli2);
			cmd.Parameters.AddWithValue("cli3", cli3);
			cmd.Parameters.AddWithValue("cli4", cli4);
			cmd.Parameters.AddWithValue("cli5", cli5);
			cmd.Parameters.AddWithValue("tecnicos", todostec);
			cmd.Parameters.AddWithValue("tec1", tec1);
			cmd.Parameters.AddWithValue("tec2", tec2);
			cmd.Parameters.AddWithValue("tec3", tec3);
			cmd.Parameters.AddWithValue("tec4", tec4);
			cmd.Parameters.AddWithValue("tec5", tec5);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteGananciaPorArticulo2_Clientes");
			set.WriteXml("C:\\XML\\GananciaPorArticulo2RPT_clientes.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReportGananciaxArticulo2_Vendedores(int codigoProducto, DateTime fecha_inicio, DateTime fecha_fin, int todos, int cod1, int cod2, int cod3, int cod4, int vendedores, int vend1, int vend2, int vend3, int vend4, int vend5)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteGananciaxArticulo2_Vendedores", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 1500000000;
			cmd.Parameters.AddWithValue("codigo_producto", codigoProducto);
			cmd.Parameters.AddWithValue("fecha_inicio", fecha_inicio);
			cmd.Parameters.AddWithValue("fecha_fin", fecha_fin);
			cmd.Parameters.AddWithValue("todos", todos);
			cmd.Parameters.AddWithValue("cod1", cod1);
			cmd.Parameters.AddWithValue("cod2", cod2);
			cmd.Parameters.AddWithValue("cod3", cod3);
			cmd.Parameters.AddWithValue("cod4", cod4);
			cmd.Parameters.AddWithValue("vendedores", vendedores);
			cmd.Parameters.AddWithValue("vend1", vend1);
			cmd.Parameters.AddWithValue("vend2", vend2);
			cmd.Parameters.AddWithValue("vend3", vend3);
			cmd.Parameters.AddWithValue("vend4", vend4);
			cmd.Parameters.AddWithValue("vend5", vend5);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteGananciaPorArticulo2_Vendedores");
			set.WriteXml("C:\\XML\\GananciaPorArticulo2RPT_vendedores.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReporteGananciaCatalogo()
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteGananciaCatalogo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15000000;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_ReporteGananciaCatalogo");
			set.WriteXml("C:\\XML\\GananciaPorCatalogo.xml", XmlWriteMode.WriteSchema);
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
