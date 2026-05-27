using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReportProductos
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	private DataSet set = null;

	public DataSet Inventario(int codemp, int codalma, bool costo, bool art, bool fam, bool lin, bool gru, bool tip, bool todo, int art1, int art2, bool cero, int orden, bool activos, double tipocambiomanual)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteStock4", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codemp", codemp);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("costo", costo);
			cmd.Parameters.AddWithValue("art", art);
			cmd.Parameters.AddWithValue("fam", fam);
			cmd.Parameters.AddWithValue("lin", lin);
			cmd.Parameters.AddWithValue("gru", gru);
			cmd.Parameters.AddWithValue("tip", tip);
			cmd.Parameters.AddWithValue("todo", todo);
			cmd.Parameters.AddWithValue("art1", art1);
			cmd.Parameters.AddWithValue("art2", art2);
			cmd.Parameters.AddWithValue("cero", cero);
			cmd.Parameters.AddWithValue("v_orden", orden);
			cmd.Parameters.AddWithValue("activos", activos);
			cmd.Parameters.AddWithValue("tc_manual", tipocambiomanual);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\inventarioSP2.xml", XmlWriteMode.WriteSchema);
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

	public DataSet CatalogoConPrecio()
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("CatalogoconPrecios", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\catalogoprecios.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ReporteValorizacion(int codalma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ValorizacionAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codalma", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\ValorizacionAlmacen.xml", XmlWriteMode.WriteSchema);
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

	public DataSet ListaDetalleExistencia(int codalma, int codalma2, int codExist)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("listaDetalleExistencias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codalma2", codalma2);
			cmd.Parameters.AddWithValue("codExist", codExist);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\existenciasCR.xml", XmlWriteMode.WriteSchema);
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

	public DataTable ListaExistencia()
	{
		try
		{
			DataTable set = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listaExistencias", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
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

	public DataTable ListaExistencia(DateTime fechageneracion, DateTime fechageneracionfinal)
	{
		try
		{
			DataTable set = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listaExistencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fechagen", fechageneracion);
			cmd.Parameters.AddWithValue("fechagenfin", fechageneracionfinal);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
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

	public bool CrearExistencia(ref int codExiste)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("crearExistencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			if (cmd.ExecuteNonQuery() != 0)
			{
				int codExistencia = Convert.ToInt32(cmd.Parameters["newid"].Value);
				codExiste = codExistencia;
				return true;
			}
			return false;
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

	public bool EliminaExistencia(int codigo_existencia)
	{
		try
		{
			DataTable set = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("eliminaExistencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codigoExistencia", codigo_existencia);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
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
