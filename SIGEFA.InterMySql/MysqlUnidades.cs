using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlUnidades : IUnidad
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsUnidadMedida uni)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaUnidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("sigla", uni.Sigla);
			oParam = cmd.Parameters.AddWithValue("descripcion", uni.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", uni.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			uni.CodUnidadNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsUnidadMedida uni)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaUnidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coduni", uni.CodUnidad);
			cmd.Parameters.AddWithValue("sigla", uni.Sigla);
			cmd.Parameters.AddWithValue("descripcion", uni.Descripcion);
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

	public bool Delete(int CodUnidad)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarUnidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coduni", CodUnidad);
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

	public clsUnidadMedida CargaUnidad(int Codigo)
	{
		clsUnidadMedida uni = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUnidadMedida", con.conector);
			cmd.Parameters.AddWithValue("coduni", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					uni = new clsUnidadMedida();
					uni.CodUnidad = Convert.ToInt32(dr.GetDecimal(0));
					uni.Sigla = dr.GetString(1);
					uni.Descripcion = dr.GetString(2);
					uni.Estado = dr.GetBoolean(3);
					uni.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					uni.FechaRegistro = dr.GetDateTime(5);
				}
			}
			return uni;
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

	public DataTable ListaUnidades()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidades", con.conector);
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

	public DataTable ListaUnidades1()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaUnidades1", con.conector);
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

	public DataTable MuestraUnidadesEquivalentes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraUnidadesEquivalentes", con.conector);
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

	public bool ActualizaPrecioEnDolares()
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPrecioEnDolares", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public int CantidadProductosDolares()
	{
		try
		{
			int resultado = 0;
			con.conectarBD();
			cmd = new MySqlCommand("CantidadProductosDolares", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = Convert.ToInt32(dr.GetInt32(0));
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

	public int ValidaMoneda()
	{
		try
		{
			int resultado = 0;
			con.conectarBD();
			cmd = new MySqlCommand("ValidaMoneda", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = Convert.ToInt32(dr.GetInt32(0));
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
}
