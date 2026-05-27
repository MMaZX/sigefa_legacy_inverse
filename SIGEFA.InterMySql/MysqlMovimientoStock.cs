using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.InterMySql;

internal class MysqlMovimientoStock
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public DataTable ListaMovimientoStock(int codAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoMovimientoStock", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			oParam = cmd.Parameters.AddWithValue("_desde", desde);
			oParam = cmd.Parameters.AddWithValue("_hasta", hasta);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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

	public bool LimpiarAccesos(int CodUsuario, int CodAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("LimpiaAccesos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codusua", CodUsuario);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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

	public List<int> MuestraAccesos(int CodUsuario, int CodAlmacen)
	{
		List<int> lista = new List<int>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaAccesos", con.conector);
			cmd.Parameters.AddWithValue("codusua", CodUsuario);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lista.Add(Convert.ToInt32(dr["codFormulario"]));
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

	public bool InsertAccesoEmp(int CodUsuario, int CodEmpresa, int codUser)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaAccesoEmpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codusu", CodUsuario);
			oParam = cmd.Parameters.AddWithValue("codemp", CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("coduser", codUser);
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
