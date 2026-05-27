using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class mysqlTipoPrecios : ITipoPrecio
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataAdapter adap = null;

	private DataTable listaP = null;

	public bool insert(clsTipoPrecios tp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("guardaTipoPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oparm = cmd.Parameters.AddWithValue("sigla", tp.Sigla);
			oparm = cmd.Parameters.AddWithValue("descripcion", tp.Descripcion);
			oparm = cmd.Parameters.AddWithValue("codalmacen", tp.CodAlmacen);
			oparm = cmd.Parameters.AddWithValue("codusu", tp.User1);
			oparm = cmd.Parameters.AddWithValue("newid", 0);
			oparm.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			tp.CodTipoPrecioNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsTipoPrecios tp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("updateTipoPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", tp.CodTipoPrecio);
			cmd.Parameters.AddWithValue("siglaTP", tp.Sigla);
			cmd.Parameters.AddWithValue("descrip", tp.Descripcion);
			cmd.Parameters.AddWithValue("almacen", tp.CodAlmacen);
			cmd.Parameters.AddWithValue("codusu", tp.User1);
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

	public bool eliminar(int codTipoPrecio)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("eliminarTipoPrecio", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", codTipoPrecio);
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

	public DataTable ListaPrecios()
	{
		try
		{
			con.conectarBD();
			listaP = new DataTable();
			cmd = new MySqlCommand("lisprecios", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(listaP);
			return listaP;
		}
		catch (MySqlException ex)
		{
			throw ex;
		}
	}
}
