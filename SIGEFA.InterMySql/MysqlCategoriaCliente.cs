using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlCategoriaCliente : ICategoriaCliente
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsCategoriaCliente ctgcliente)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaZona", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descripcion", ctgcliente.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", ctgcliente.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
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

	public bool Update(clsCategoriaCliente zon)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaZona", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codzon", zon.CodCategoriaCliente);
			cmd.Parameters.AddWithValue("descripcion", zon.Descripcion);
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

	public bool Delete(int CodZona)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarZona", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codzon", CodZona);
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

	public clsZona CargaZona(int Codigo)
	{
		clsZona zon = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraZona", con.conector);
			cmd.Parameters.AddWithValue("codzon", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					zon = new clsZona();
					zon.CodZona = Convert.ToInt32(dr.GetDecimal(0));
					zon.Descripcion = dr.GetString(1);
					zon.Estado = dr.GetBoolean(2);
					zon.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					zon.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return zon;
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

	public DataTable ListaCategoriasCliente()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCategoriaClientes", con.conector);
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
}
