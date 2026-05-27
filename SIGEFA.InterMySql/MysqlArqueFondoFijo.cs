using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlArqueFondoFijo : IArqueoFondoFijo
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsArqueoFondoFijo arqe)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaArqueoFondoFijo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("encargado_ex", arqe.Encargado);
			oParam = cmd.Parameters.AddWithValue("horainicio_ex", arqe.Horainicio);
			oParam = cmd.Parameters.AddWithValue("horafin_ex", arqe.Horafin);
			oParam = cmd.Parameters.AddWithValue("total_ex", arqe.Total);
			oParam = cmd.Parameters.AddWithValue("coduser_ex", arqe.Coduser);
			oParam = cmd.Parameters.AddWithValue("codsucursal_ex", arqe.Codsucursa);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			arqe.CodarqueofondodijoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertDetalle(clsDetalleArqueFondoFijo det)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleArqueoFondoFijo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codarqueofondofijo_ex", det.Codarqueofondofijo);
			oParam = cmd.Parameters.AddWithValue("coddinero_ex", det.Coddinero);
			oParam = cmd.Parameters.AddWithValue("cantidad_ex", det.Cantidad);
			oParam = cmd.Parameters.AddWithValue("importe_ex", det.Importe);
			oParam = cmd.Parameters.AddWithValue("coduser_ex", det.Coduser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			det.CoddetallearqueofondofijoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public DataTable ListaDinero(int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDinero", con.conector);
			cmd.Parameters.AddWithValue("tipo_ex", tipo);
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

	public decimal TraeValor(int codigo)
	{
		decimal total = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("TraeValorDinero", con.conector);
			cmd.Parameters.AddWithValue("coddinero_ex", codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					total = Convert.ToDecimal(dr.GetDecimal(0));
				}
			}
			return total;
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
