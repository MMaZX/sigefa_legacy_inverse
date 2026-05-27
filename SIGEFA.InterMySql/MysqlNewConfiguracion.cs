using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.InterMySql;

internal class MysqlNewConfiguracion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	internal bool guardaConfiguracion(string codconfig, string dataconfig, string origen, string comentario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNewConfiguracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codConf", codconfig);
			cmd.Parameters.AddWithValue("_dataConf", dataconfig);
			cmd.Parameters.AddWithValue("_origen", origen);
			cmd.Parameters.AddWithValue("_comentario", comentario);
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

	internal bool actualizaConfiguracion(string codconfig, string origen, string dataconfig)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNewConfiguracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codConf", codconfig);
			cmd.Parameters.AddWithValue("_origen", origen);
			cmd.Parameters.AddWithValue("_dataConf", dataconfig);
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

	internal int getConfiguracion(string codconfig, string origen)
	{
		int resultado = -666;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetNewConfiguracionInt", con.conector);
			cmd.Parameters.AddWithValue("_codconf", codconfig);
			cmd.Parameters.AddWithValue("_origen", origen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetInt32(0);
				}
			}
			return resultado;
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

	internal bool deleteConfiguracion(string codconfig, string origen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaNewConfiguracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codConf", codconfig);
			cmd.Parameters.AddWithValue("_origen", origen);
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
