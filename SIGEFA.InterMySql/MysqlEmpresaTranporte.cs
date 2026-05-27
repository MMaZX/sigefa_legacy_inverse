using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlEmpresaTranporte : IEmpresaTranporte
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsEmpresaTransporte emp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaEmpresaTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("ruc", emp.Ruc);
			oParam = cmd.Parameters.AddWithValue("razonsocial", emp.RazonSocial);
			oParam = cmd.Parameters.AddWithValue("direccion", emp.Direccion);
			oParam = cmd.Parameters.AddWithValue("telefono", emp.Telefono);
			oParam = cmd.Parameters.AddWithValue("codusu", emp.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			emp.CodEmpresaTranporteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsEmpresaTransporte emp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEmpresaTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codemp", emp.CodEmpresaTranporte);
			cmd.Parameters.AddWithValue("ruc", emp.Ruc);
			cmd.Parameters.AddWithValue("razonsocial", emp.RazonSocial);
			cmd.Parameters.AddWithValue("direccion", emp.Direccion);
			cmd.Parameters.AddWithValue("telefono", emp.Telefono);
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

	public bool Delete(int CodEmpresaTranporte)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarEmpresaTransporte", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codemp", CodEmpresaTranporte);
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

	public clsEmpresaTransporte CargaEmpresaTranporte(int Codigo)
	{
		clsEmpresaTransporte emp = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraEmpresaTransporte", con.conector);
			cmd.Parameters.AddWithValue("codemp", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					emp = new clsEmpresaTransporte();
					emp.CodEmpresaTranporte = Convert.ToInt32(dr.GetDecimal(0));
					emp.Ruc = dr.GetString(1);
					emp.RazonSocial = dr.GetString(2);
					emp.Direccion = dr.GetString(3);
					emp.Telefono = dr.GetString(4);
					emp.Estado = dr.GetBoolean(5);
					emp.CodUser = Convert.ToInt32(dr.GetDecimal(6));
					emp.FechaRegistro = dr.GetDateTime(7);
				}
			}
			return emp;
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

	public clsEmpresaTransporte BuscaEmpresaTransporte(string RUC)
	{
		clsEmpresaTransporte emp = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaEmpresaTransporte", con.conector);
			cmd.Parameters.AddWithValue("ru", RUC);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					emp = new clsEmpresaTransporte();
					emp.CodEmpresaTranporte = Convert.ToInt32(dr.GetDecimal(0));
					emp.Ruc = dr.GetString(1);
					emp.RazonSocial = dr.GetString(2);
					emp.Direccion = dr.GetString(3);
				}
			}
			return emp;
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

	public DataTable ListaEmpresaTranportes()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaEmpresasTransporte", con.conector);
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

	public DataTable CargaEmpresasTransporte()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaEmpresasTransporte", con.conector);
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
