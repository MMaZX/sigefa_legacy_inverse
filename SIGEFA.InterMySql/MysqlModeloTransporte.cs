using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlModeloTransporte : IModeloTransporte
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsModeloTransporte mod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaModeloTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codmar", mod.CodMarcaTransporte);
			oParam = cmd.Parameters.AddWithValue("descripcion", mod.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", mod.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			mod.CodModeloTransporteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsModeloTransporte mod)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaModeloTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmod", mod.CodModeloTransporte);
			cmd.Parameters.AddWithValue("descripcion", mod.Descripcion);
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

	public bool Delete(int CodModeloTransporte)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarModeloTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmod", CodModeloTransporte);
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

	public clsModeloTransporte CargaModeloTransporte(int Codigo)
	{
		clsModeloTransporte mod = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraModeloTransporte", con.conector);
			cmd.Parameters.AddWithValue("codmod", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					mod = new clsModeloTransporte();
					mod.CodModeloTransporte = Convert.ToInt32(dr.GetDecimal(0));
					mod.CodMarcaTransporte = Convert.ToInt32(dr.GetDecimal(1));
					mod.Descripcion = dr.GetString(2);
					mod.Estado = dr.GetBoolean(3);
					mod.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					mod.FechaRegistro = dr.GetDateTime(5);
				}
			}
			return mod;
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

	public DataTable ListaModeloTransportes(int CodMar)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaModeloTransportes", con.conector);
			cmd.Parameters.AddWithValue("codmar", CodMar);
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
