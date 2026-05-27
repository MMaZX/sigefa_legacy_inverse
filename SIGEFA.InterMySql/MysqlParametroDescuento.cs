using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlParametroDescuento : IParametroDescuento
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsParametroDescuento ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaParametroDescuento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_CodEmpresa", ingreso.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("_Desde", ingreso.Desde);
			oParam = cmd.Parameters.AddWithValue("_Hasta", ingreso.Hasta);
			oParam = cmd.Parameters.AddWithValue("_Valor", ingreso.Valor);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			ingreso.CodParametro = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsParametroDescuento ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaParametroDescuento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_CodParametro", ingreso.CodParametro);
			oParam = cmd.Parameters.AddWithValue("_Desde", ingreso.Desde);
			oParam = cmd.Parameters.AddWithValue("_Hasta", ingreso.Hasta);
			oParam = cmd.Parameters.AddWithValue("_Valor", ingreso.Valor);
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

	public DataTable ListadoParametros(int CodEmpresa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoParametrosDescuentos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_CodEmpresa", CodEmpresa);
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

	public DataTable ListadoParametroDescuento(int CodEmpresa, decimal valor)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoParametrosDescuentosValor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_CodEmpresa", CodEmpresa);
			cmd.Parameters.AddWithValue("_Valor", valor);
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
