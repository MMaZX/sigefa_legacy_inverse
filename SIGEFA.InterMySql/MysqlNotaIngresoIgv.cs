using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlNotaIngresoIgv : INotaIngresoIgv
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsNotaIngresoIgv notaIngresoIgv)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNotaIngresoIgv", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codigo_nota_ingreso", notaIngresoIgv.CodNotaIngreso);
			oParam = cmd.Parameters.AddWithValue("igv", notaIngresoIgv.IncluyeIgv);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			notaIngresoIgv.CodNotaIngresoIgv = Convert.ToInt32(cmd.Parameters["newid"].Value);
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
}
