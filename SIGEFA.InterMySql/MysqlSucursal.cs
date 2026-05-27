using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlSucursal : ISucursal
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsSucursal suc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codemp", suc.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("nombre", suc.Nombre);
			oParam = cmd.Parameters.AddWithValue("ubicacion", suc.Ubicacion);
			oParam = cmd.Parameters.AddWithValue("telefono", suc.Telefono);
			oParam = cmd.Parameters.AddWithValue("descripcion", suc.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", suc.CodUser);
			oParam = cmd.Parameters.AddWithValue("estado", suc.Estado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			suc.CodSucursalNueva = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsSucursal suc)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codemp", suc.CodEmpresa);
			cmd.Parameters.AddWithValue("nombre", suc.Nombre);
			cmd.Parameters.AddWithValue("ubicacion", suc.Ubicacion);
			cmd.Parameters.AddWithValue("telefono", suc.Telefono);
			cmd.Parameters.AddWithValue("descripcion", suc.Descripcion);
			cmd.Parameters.AddWithValue("codusu", suc.CodUser);
			cmd.Parameters.AddWithValue("estado", suc.Estado);
			cmd.Parameters.AddWithValue("codsu", suc.CodSucursal);
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

	public bool Delete(int Codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarSucursal", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codSuc", Codigo);
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

	public clsSucursal CargaSucursal(int Codigo)
	{
		clsSucursal suc = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraSucursal", con.conector);
			cmd.Parameters.AddWithValue("CodSucu", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					suc = new clsSucursal();
					suc.CodSucursal = Convert.ToInt32(dr.GetInt32(0));
					suc.CodEmpresa = Convert.ToInt32(dr.GetInt32(1));
					suc.Nombre = dr.GetString(2);
					suc.Ubicacion = dr.GetString(3);
					suc.Telefono = dr.GetString(4);
					suc.Descripcion = dr.GetString(5);
					suc.Estado = dr.GetBoolean(6);
					suc.FechaRegistro = dr.GetDateTime(7);
				}
			}
			return suc;
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

	public bool VerificaRUC(string RUC)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaRUC", con.conector);
			cmd.Parameters.AddWithValue("rucingresado", RUC);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
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

	public DataTable ListaSucursales()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaSucursales", con.conector);
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

	public DataTable CargaSucursalesXempresa(int Codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaSucursalesXempresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodEmpre", Codigo);
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

	public DataTable CargaSucursales(int Codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaSucursales", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodEmpre", Codigo);
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

	public DataTable CargaSucursalesXusuario(int Codigo, int CodUsuario)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaSucursalesXUsuario", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodEmpre", Codigo);
			cmd.Parameters.AddWithValue("CodUsu", CodUsuario);
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

	public DataTable CargaSucursalesSeleccion(int Codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaSucursalesSeleccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodEmpre", Codigo);
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

	public DataTable BuscaSucursales(int Criterio, string Filtro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("FiltraSucursales", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@criterio", Criterio);
			cmd.Parameters.AddWithValue("@filtro", Filtro);
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

	public DataTable CargaSucursales()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaSucursales", con.conector);
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

	public bool UpdateConfiguracion(clsParametros Config)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaConfiguracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("config", Config.CodConfiguracion);
			cmd.Parameters.AddWithValue("igv", Config.IGV);
			cmd.Parameters.AddWithValue("codusu", Config.CodUser);
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

	public clsParametros CargaConfiguracion()
	{
		clsParametros config = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaConfiguracion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					config = new clsParametros();
					config.CodConfiguracion = Convert.ToInt32(dr.GetDecimal(0));
					config.IGV = Convert.ToDouble(dr.GetDecimal(1));
					config.Estado = dr.GetBoolean(2);
					config.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					config.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return config;
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

	public DataTable ListaSucursales_Empresa(int Codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaSucursales_Empresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CodEmpre", Codigo);
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

	public int sucursalxalmacen(int almacen)
	{
		try
		{
			int cod = 0;
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("sucursalxalmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codalmacen", almacen);
			return Convert.ToInt32(cmd.ExecuteScalar());
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
