using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlEmpresa : IEmpresa
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsEmpresa emp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaEmpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("razonsocial", emp.RazonSocial);
			oParam = cmd.Parameters.AddWithValue("ruc", emp.Ruc);
			oParam = cmd.Parameters.AddWithValue("direccion", emp.Direccion);
			oParam = cmd.Parameters.AddWithValue("telefono", emp.Telefono);
			oParam = cmd.Parameters.AddWithValue("fax", emp.Fax);
			oParam = cmd.Parameters.AddWithValue("nombrecorto", emp.NombreCorto);
			oParam = cmd.Parameters.AddWithValue("representante", emp.Representante);
			oParam = cmd.Parameters.AddWithValue("estado", emp.Estado);
			oParam = cmd.Parameters.AddWithValue("codusu", emp.CodUser);
			oParam = cmd.Parameters.AddWithValue("logo", emp.Logo);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			emp.CodEmpresaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsEmpresa emp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEmpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codemp", emp.CodEmpresa);
			cmd.Parameters.AddWithValue("razonsocial", emp.RazonSocial);
			cmd.Parameters.AddWithValue("ruc", emp.Ruc);
			cmd.Parameters.AddWithValue("direccion", emp.Direccion);
			cmd.Parameters.AddWithValue("telefono", emp.Telefono);
			cmd.Parameters.AddWithValue("fax", emp.Fax);
			cmd.Parameters.AddWithValue("representante", emp.Representante);
			cmd.Parameters.AddWithValue("estado", emp.Estado);
			cmd.Parameters.AddWithValue("logo", emp.Logo);
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

	public bool Delete(int CodEmpresa)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarEmpresa", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codemp", CodEmpresa);
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

	public clsEmpresa CargaEmpresa(int Codigo)
	{
		clsEmpresa emp = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraEmpresa", con.conector);
			cmd.Parameters.AddWithValue("codempre", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					emp = new clsEmpresa();
					emp.CodEmpresa = Convert.ToInt32(dr.GetDecimal(0));
					emp.RazonSocial = dr.GetString(1);
					emp.Ruc = dr.GetString(2);
					emp.Direccion = dr.GetString(3);
					emp.Telefono = dr.GetString(4);
					emp.Fax = dr.GetString(5);
					emp.Representante = dr.GetString(6);
					emp.Estado = dr.GetBoolean(7);
					emp.FechaRegistro = dr.GetDateTime(8);
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

	public DataTable ListaEmpresas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaEmpresas", con.conector);
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

	public DataTable BuscaEmpresas(int Criterio, string Filtro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("FiltraEmpresa", con.conector);
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

	public DataTable CargaEmpresas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaEmpresas", con.conector);
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
			cmd.Parameters.AddWithValue("dias", Config.DiasVigencia);
			cmd.Parameters.AddWithValue("codusu", Config.CodUser);
			cmd.Parameters.AddWithValue("venfac", Config.FacturasVencidas);
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
					config.DiasVigencia = Convert.ToInt32(dr.GetDecimal(2));
					config.Estado = dr.GetBoolean(3);
					config.CodUser = Convert.ToInt32(dr.GetDecimal(4));
					config.FechaRegistro = dr.GetDateTime(5);
					config.FacturasVencidas = dr.GetBoolean(6);
					config.Autoguardado = dr.GetBoolean(7);
					config.Icbper = dr.GetDecimal(8);
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

	public clsParametros CargaParametroPorEmpresa(int codigoEmpresa, int codigoParametro)
	{
		clsParametros config = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraParametroPorEmpresa", con.conector);
			cmd.Parameters.AddWithValue("codigo_empresa", codigoEmpresa);
			cmd.Parameters.AddWithValue("codigo_parametro", codigoParametro);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					config = new clsParametros();
					config.Valor = dr.GetInt32(0);
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

	public clsEmpresa CargaEmpresa3(int Codigo)
	{
		clsEmpresa emp = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraEmpresa3", con.conector);
			cmd.Parameters.AddWithValue("codempre", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					emp = new clsEmpresa();
					emp.CodEmpresa = Convert.ToInt32(dr.GetDecimal(0));
					emp.RazonSocial = dr.GetString(1);
					emp.Ruc = dr.GetString(2);
					emp.Direccion = dr.GetString(3);
					emp.Telefono = dr.GetString(4);
					emp.Fax = dr.GetString(5);
					emp.Representante = dr.GetString(6);
					emp.Estado = dr.GetBoolean(7);
					emp.FechaRegistro = dr.GetDateTime(8);
					emp.Certificado = dr.GetString(9);
					emp.Contrasena = dr.GetString(10);
					emp.UsuarioSunat = dr.GetString(11);
					emp.ClaveSunat = dr.GetString(12);
					emp.Url = dr.GetString(13);
					emp.IDSUNATAPI = dr.GetString(15);
					emp.CLAVESUNATAPI = dr.GetString(16);
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

	public int empresaxalmacen(int codalmacen)
	{
		clsEmpresa emp = null;
		try
		{
			int cod = 0;
			con.conectarBD();
			cmd = new MySqlCommand("empresaxalmacen", con.conector);
			cmd.Parameters.AddWithValue("_codalmacen", codalmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			return Convert.ToInt32(cmd.ExecuteScalar());
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
