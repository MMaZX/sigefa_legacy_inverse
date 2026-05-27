using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlAccesoSucursales : IAccesoSucursales
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsAccesosSucursales acce)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaAccesoSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codusua", acce.CodUsuario);
			oParam = cmd.Parameters.AddWithValue("codemp", acce.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("codsuc", acce.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codusu", acce.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			acce.CodAccesoSucursalNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool LimpiarAccesos(int CodUsuario, int CodEmpresa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("LimpiaAccesosSucursales", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codusua", CodUsuario);
			cmd.Parameters.AddWithValue("codEmpre", CodEmpresa);
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

	public List<int> MuestraAccesosSucursales(int CodUsuario, int CodEmpresa)
	{
		List<int> lista = new List<int>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaAccesosSucursales", con.conector);
			cmd.Parameters.AddWithValue("codusua", CodUsuario);
			cmd.Parameters.AddWithValue("codEmpre", CodEmpresa);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					lista.Add(Convert.ToInt32(dr["codSucursal"]));
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

	public bool InsertAccesoEmp(int CodUsuario, int CodEmpresa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaAccesoEmpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codusu", CodUsuario);
			oParam = cmd.Parameters.AddWithValue("codemp", CodEmpresa);
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
}
