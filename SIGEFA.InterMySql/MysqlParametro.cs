using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlParametro : IParametro
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool ActualizarParametroVenta(string valorParametro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaParametroVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("valor_parametro", valorParametro);
			return (cmd.ExecuteNonQuery() != 0) ? true : false;
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

	public bool ConsultarParametroVenta(int codigo)
	{
		bool todasLasVentas = false;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ConsultarParametroVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codParametro_ex", codigo);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					todasLasVentas = dr.GetBoolean(0);
				}
			}
			return todasLasVentas;
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

	public bool actualizaDocumentoVenta(string valorParametro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("actualizaDocumentoVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("valor_parametro", valorParametro);
			return (cmd.ExecuteNonQuery() != 0) ? true : false;
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

	public bool Insert(clsParametro ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaParametro", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_descripcion", ingreso.descripcion);
			oParam = cmd.Parameters.AddWithValue("_valor", ingreso.valor);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			ingreso.codParametro = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
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

	public bool Update(clsParametro ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaParametro", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_id", ingreso.codParametro);
			cmd.Parameters.AddWithValue("_descripcion", ingreso.descripcion);
			cmd.Parameters.AddWithValue("_valor", ingreso.valor);
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

	public DataTable ListadoParametros()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoParametros", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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

	public clsParametro ObtenerParametro(int codigo)
	{
		try
		{
			con.conectarBD();
			clsParametro param = null;
			cmd = new MySqlCommand("ObtenerParametro", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codigo", codigo);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					param = new clsParametro();
					param.codParametro = Convert.ToInt32(dr["codParametro"]);
					param.descripcion = Convert.ToString(dr["descripcion"]);
					param.valor = Convert.ToString(dr["valor"]);
				}
			}
			return param;
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
