using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlIngresosEgresos : IIngresosEgresos
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsIngresoEgreso ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCategoriaIngresoEgreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_tipo", ingreso.Tipo);
			oParam = cmd.Parameters.AddWithValue("_descripcion", ingreso.Descripcion);
			oParam = cmd.Parameters.AddWithValue("_estado", ingreso.Estado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			ingreso.Id = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsIngresoEgreso ingreso)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCategoriaIngresoEgreso", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_id", ingreso.Id);
			cmd.Parameters.AddWithValue("_tipo", ingreso.Tipo);
			cmd.Parameters.AddWithValue("_descripcion", ingreso.Descripcion);
			cmd.Parameters.AddWithValue("_estado", ingreso.Estado);
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

	public DataTable ListadoIngresosEgresos(int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoIngresosEgresos", con.conector);
			cmd.Parameters.AddWithValue("_tipo", tipo);
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
