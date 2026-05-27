using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlTarjetaPago : ITarjetaPago
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsTarjetaPago tar)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaTarjetaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("tipo", tar.Tipo);
			oParam = cmd.Parameters.AddWithValue("descripcion", tar.Descripcion);
			oParam = cmd.Parameters.AddWithValue("codUsu", tar.Coduser);
			oParam = cmd.Parameters.AddWithValue("codalma", tar.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			tar.CodTarjetaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsTarjetaPago tar)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaTarjetaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtar", tar.CodTarjeta);
			cmd.Parameters.AddWithValue("tip", tar.Tipo);
			cmd.Parameters.AddWithValue("descrip", tar.Descripcion);
			cmd.Parameters.AddWithValue("codalma", tar.CodAlmacen);
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

	public bool Delete(int CodTarjeta, int CodAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarTarjetaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codtar", CodTarjeta);
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

	public clsTarjetaPago CargaTarjeta(int CodTarjeta, int codAlmacen)
	{
		clsTarjetaPago tar = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraTarjetaPago", con.conector);
			cmd.Parameters.AddWithValue("codtar", CodTarjeta);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					tar = new clsTarjetaPago();
					tar.CodTarjeta = Convert.ToInt32(dr.GetDecimal(0));
					tar.Tipo = dr.GetString(1);
					tar.Descripcion = dr.GetString(2);
					tar.Estado = dr.GetBoolean(3);
					tar.Coduser = Convert.ToInt32(dr.GetDecimal(4));
					tar.Fecharegistro = dr.GetDateTime(5);
				}
			}
			return tar;
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

	public DataTable ListaTarjetas(int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTarjetasPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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

	public double SumaTotalTarjetas(string fecha1, string fecha2, int almacen, int codtarjeta, int sucursal, int codcaja)
	{
		double total = 0.0;
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("SumaTotalTarjetas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_fecha1", fecha1);
			cmd.Parameters.AddWithValue("_fecha2", fecha2);
			cmd.Parameters.AddWithValue("_almacen", almacen);
			cmd.Parameters.AddWithValue("_codtarjeta", codtarjeta);
			cmd.Parameters.AddWithValue("_sucursal", sucursal);
			cmd.Parameters.AddWithValue("_codcaja", codcaja);
			return Convert.ToDouble(cmd.ExecuteScalar());
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
