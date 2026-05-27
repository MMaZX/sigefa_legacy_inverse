using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;

namespace SIGEFA.InterMySql;

internal class MysqlOficio
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	internal DataTable listaOficios()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaOficios", con.conector);
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

	internal bool insert(clsOficio nuevo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaOficio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_descripcion", nuevo.Descripcion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			if (cmd.ExecuteNonQuery() != 0)
			{
				nuevo.Id = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	internal bool update(clsOficio oficio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaOficio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_idOficio", oficio.Id);
			cmd.Parameters.AddWithValue("_descripcion", oficio.Descripcion);
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

	internal bool delete(int codOIficio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DeleteOficio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_idOficio", codOIficio);
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

	internal List<clsOficio> listaOficios(int codTecnico)
	{
		List<clsOficio> lista = new List<clsOficio>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ListadoOficios", con.conector);
			cmd.Parameters.AddWithValue("_idTecnico", codTecnico);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsOficio oficio = new clsOficio();
					oficio.Id = dr.GetInt32(0);
					oficio.Descripcion = dr.GetString(1);
					lista.Add(oficio);
				}
			}
			return lista;
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

	internal bool getSiOficioUtilizado(int codOficio)
	{
		bool resultado = false;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("verificaOficioUtilizado", con.conector);
			cmd.Parameters.AddWithValue("_idOficio", codOficio);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					resultado = dr.GetBoolean(0);
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
