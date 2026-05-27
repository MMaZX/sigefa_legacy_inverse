using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;

namespace SIGEFA.InterMySql;

internal class MysqlSeleccionDespachoNC
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsSeleccionDespachoNC sel)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaSeleccionadoNC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codNC", sel.CodNotaCredito);
			oParam = cmd.Parameters.AddWithValue("_codDNC", sel.CodDetalleNotaCredito);
			oParam = cmd.Parameters.AddWithValue("_codAlma", sel.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("_ctdadPermitida", sel.CtdadPermitida);
			oParam = cmd.Parameters.AddWithValue("_ctdadSeleccionada", sel.CtdadSeleccionada);
			oParam = cmd.Parameters.AddWithValue("_seleccionado", sel.Seleccionado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			sel.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	internal DataTable cargaAlmacenesDeSeleccionDeNC(int codNotaCredito)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacenesDeSeleccionDespachoNC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codNotaCredito", codNotaCredito);
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

	internal bool tieneDataSeleccionada(string codNotaCredito)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCantidadListadoSeleccionDespachoNC", con.conector);
			cmd.Parameters.AddWithValue("_codNotaCredito", codNotaCredito);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows && dr.Read())
			{
				return dr.GetInt32(0) > 0;
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

	internal DataTable cargaDataParaInterfazSeleccionador(int codNotaCredito)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("DataParaInterfazSeleccionador", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codNotaCredito", codNotaCredito);
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
