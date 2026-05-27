using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlTransaccion : ITransaccion
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsTransaccion tran)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTransaccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("sigla", tran.Sigla);
			oParam = cmd.Parameters.AddWithValue("descripcion", tran.Descripcion);
			oParam = cmd.Parameters.AddWithValue("tipo", tran.Tipo);
			oParam = cmd.Parameters.AddWithValue("codusu", tran.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			tran.CodTransaccion = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsTransaccion tran)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTransaccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtran", tran.CodTransaccion);
			cmd.Parameters.AddWithValue("sigla", tran.Sigla);
			cmd.Parameters.AddWithValue("descripcion", tran.Descripcion);
			cmd.Parameters.AddWithValue("tipo", tran.Tipo);
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

	public bool Delete(int CodTransaccion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarTransaccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtran", CodTransaccion);
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

	public clsTransaccion CargaTransaccion(int Codigo)
	{
		clsTransaccion tran = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTransaccion", con.conector);
			cmd.Parameters.AddWithValue("codtran", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tran = new clsTransaccion();
					tran.CodTransaccion = Convert.ToInt32(dr.GetDecimal(0));
					tran.Descripcion = dr.GetString(1);
					tran.Sigla = dr.GetString(2);
					tran.Codsunat = dr.GetString(3);
					tran.Tipo = Convert.ToInt32(dr.GetDecimal(4));
					tran.Estado = dr.GetBoolean(5);
					tran.CodUser = Convert.ToInt32(dr.GetDecimal(6));
					tran.FechaRegistro = dr.GetDateTime(7);
				}
			}
			return tran;
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

	public clsTransaccion CargaTransaccionS(string Sigla, int Tipo)
	{
		clsTransaccion tran = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTransaccionS", con.conector);
			cmd.Parameters.AddWithValue("sig", Sigla);
			cmd.Parameters.AddWithValue("tip", Tipo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tran = new clsTransaccion();
					tran.CodTransaccion = Convert.ToInt32(dr.GetDecimal(0));
					tran.Descripcion = dr.GetString(1);
					tran.Sigla = dr.GetString(2);
					tran.Codsunat = dr.GetString(3);
					tran.Tipo = Convert.ToInt32(dr.GetDecimal(4));
					tran.Estado = dr.GetBoolean(5);
					tran.CodUser = Convert.ToInt32(dr.GetDecimal(6));
					tran.FechaRegistro = dr.GetDateTime(7);
				}
			}
			return tran;
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

	public DataTable ListaTransacciones(int Caso)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransacciones", con.conector);
			cmd.Parameters.AddWithValue("caso", Caso);
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

	public bool InsertConfiguracion(int CodTransaccion, int CodDetalle, int CodUser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaConfTran", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codtran", CodTransaccion);
			oParam = cmd.Parameters.AddWithValue("coddeta", CodDetalle);
			oParam = cmd.Parameters.AddWithValue("codusu", CodUser);
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

	public bool LimpiarConfiguracion(int CodTransaccion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("LimpiaConfTran", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtran", CodTransaccion);
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

	public List<int> MuestraConfiguracion(int CodTransaccion)
	{
		List<int> lista = new List<int>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaConfTran", con.conector);
			cmd.Parameters.AddWithValue("codTran", CodTransaccion);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lista.Add(Convert.ToInt32(dr["codDetalleTransaccion"]));
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
}
