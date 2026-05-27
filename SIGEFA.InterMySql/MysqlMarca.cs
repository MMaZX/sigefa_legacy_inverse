using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlMarca : IMarca
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsMarca mar)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaMarca", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("descripcion", mar.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codusu", mar.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			mar.CodMarcaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsMarca mar)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaMarca", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmar", mar.CodMarca);
			cmd.Parameters.AddWithValue("descripcion", mar.Descripcion);
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

	public bool Delete(int CodMarca)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarMarca", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codmar", CodMarca);
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

	public clsMarca CargaMarca(int Codigo)
	{
		clsMarca mar = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraMarca", con.conector);
			cmd.Parameters.AddWithValue("codmar", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					mar = new clsMarca();
					mar.CodMarca = Convert.ToInt32(dr.GetDecimal(0));
					mar.Descripcion = dr.GetString(1);
					mar.Estado = dr.GetBoolean(2);
					mar.CodUser = Convert.ToInt32(dr.GetDecimal(3));
					mar.FechaRegistro = dr.GetDateTime(4);
				}
			}
			return mar;
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

	public DataTable ListaMarcas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaMarcas", con.conector);
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

	public DataTable listaproductosmarca(int codmarca, int almacen, int familia, int proveedor)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProductos_AgrupadoPorMarcas_2", con.conector);
			cmd.Parameters.AddWithValue("codmarca", codmarca);
			cmd.Parameters.AddWithValue("codalm", almacen);
			cmd.Parameters.AddWithValue("_codFamilia", familia);
			cmd.Parameters.AddWithValue("_codProveedor", proveedor);
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

	public DataTable listaproductoslineamarca(int linea, int almacen, DateTime fechainicio, DateTime fechafin, int familia, int sucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProductos_AgrupadoLineaFamilia", con.conector);
			cmd.Parameters.AddWithValue("_codlinea", linea);
			cmd.Parameters.AddWithValue("codalma", almacen);
			cmd.Parameters.AddWithValue("fechaini", fechainicio.ToString("yyyy-mm-dd"));
			cmd.Parameters.AddWithValue("fechafin", fechafin.ToString("yyyy-mm-dd"));
			cmd.Parameters.AddWithValue("_codFamilia", familia);
			cmd.Parameters.AddWithValue("codsucur", sucursal);
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
