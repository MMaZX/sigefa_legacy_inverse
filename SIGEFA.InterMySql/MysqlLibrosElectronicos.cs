using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlLibrosElectronicos : ILibrosElectronicos
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsLibrosElectronicos libro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaLibroElectronico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descripcion", libro.Descripcion);
			oParam = cmd.Parameters.AddWithValue("sig", libro.Codsunat);
			oParam = cmd.Parameters.AddWithValue("codusu", libro.Coduser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			libro.Codnuevolibro = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsLibrosElectronicos libro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaLibroElestronico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codban", libro.Codlibro);
			cmd.Parameters.AddWithValue("descripcion", libro.Descripcion);
			cmd.Parameters.AddWithValue("sig", libro.Codsunat);
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

	public bool Delete(int CodLibro)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarLibroElectronico", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codlib", CodLibro);
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

	public clsLibrosElectronicos MuestraLE(int Codigo)
	{
		clsLibrosElectronicos le = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraLibro", con.conector);
			cmd.Parameters.AddWithValue("codlibro_ex", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					le = new clsLibrosElectronicos();
					le.Codlibro = dr.GetInt32(0);
					le.Codsunat = dr.GetString(1);
					le.Descripcion = dr.GetString(2);
					le.Aplicaanio = dr.GetInt32(3);
					le.Aplicames = dr.GetInt32(4);
					le.Aplicadia = dr.GetInt32(5);
					le.Aplicaoportunidad = dr.GetInt32(6);
					le.Estado = dr.GetInt32(7);
					le.Coduser = dr.GetInt32(8);
					le.Fecharegistro = dr.GetDateTime(9);
				}
			}
			return le;
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

	public DataTable CargaLibrosElectronicos()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaLibrosElectronicos", con.conector);
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

	public DataTable CargaRegistrosElectronicos(int Codle)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaRegistrosElectronicos", con.conector);
			cmd.Parameters.AddWithValue("codlibros_ex", Codle);
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

	public clsRegistroElectronico MuestraRE(int Codigo)
	{
		clsRegistroElectronico re = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraRegistro", con.conector);
			cmd.Parameters.AddWithValue("codlibroregistro_ex", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					re = new clsRegistroElectronico();
					re.Codlibroregistro = dr.GetInt32(0);
					re.Codlibros = dr.GetInt32(1);
					re.Codsunat = dr.GetString(2);
					re.Descripcion = dr.GetString(3);
					re.Codigo = dr.GetString(4);
					re.Estado = dr.GetInt32(5);
					re.Coduser = dr.GetInt32(6);
					re.Fecharegistro = dr.GetDateTime(7);
				}
			}
			return re;
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

	public DataTable CargaOperaciones()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaOperaciones", con.conector);
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

	public DataTable CargaContenido()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaContenido", con.conector);
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

	public DataTable CargaGeneradoPor()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaGeneradoPor", con.conector);
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

	public DataTable GetVentas_Mes_LEV(int mes)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GetVentas_Mes_LEV", con.conector);
			cmd.Parameters.AddWithValue("mes_ex", mes);
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

	public DataTable GetVentas_Mes_LEV2(int mes, string periodo)
	{
		try
		{
			string ano = periodo.Substring(0, 4);
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GetVentas_Mes_LEV", con.conector);
			cmd.Parameters.AddWithValue("mes_ex", mes);
			cmd.Parameters.AddWithValue("ano_ex", Convert.ToInt32(ano));
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

	public DataTable FacturasComprasLE(int mes, int codalma, string cadena, string periodo)
	{
		try
		{
			string ola = periodo.Substring(0, 4);
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand(cadena, con.conector);
			cmd.Parameters.AddWithValue("mes_x", mes);
			cmd.Parameters.AddWithValue("alma_x", codalma);
			cmd.Parameters.AddWithValue("ano_x", Convert.ToInt32(ola));
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

	public int ValidaCampoTipoFacturacion(int mes, int Anio)
	{
		int valida = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidaCampoTipoFacturacion", con.conector);
			cmd.Parameters.AddWithValue("mes_ex", mes);
			cmd.Parameters.AddWithValue("anio_ex", Anio);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					valida = dr.GetInt32(0);
				}
			}
			return valida;
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
