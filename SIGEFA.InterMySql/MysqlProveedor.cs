using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlProveedor : IProveedor
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsProveedor prov)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("razonsocial", prov.RazonSocial);
			oParam = cmd.Parameters.AddWithValue("ruc", prov.Ruc);
			oParam = cmd.Parameters.AddWithValue("direccion", prov.Direccion);
			oParam = cmd.Parameters.AddWithValue("telefono", prov.Telefono);
			oParam = cmd.Parameters.AddWithValue("fax", prov.Fax);
			oParam = cmd.Parameters.AddWithValue("representante", prov.Representante);
			oParam = cmd.Parameters.AddWithValue("email", prov.Mail);
			oParam = cmd.Parameters.AddWithValue("contacto", prov.Contacto);
			oParam = cmd.Parameters.AddWithValue("telefonocon", prov.TelefonoContacto);
			oParam = cmd.Parameters.AddWithValue("frecuencia", prov.FrecuenciaVisita);
			oParam = cmd.Parameters.AddWithValue("margen", prov.Margen);
			oParam = cmd.Parameters.AddWithValue("banco", prov.Banco);
			oParam = cmd.Parameters.AddWithValue("ctacte", prov.CtaCte);
			oParam = cmd.Parameters.AddWithValue("comentario", prov.Comentario);
			oParam = cmd.Parameters.AddWithValue("estado", prov.Estado);
			oParam = cmd.Parameters.AddWithValue("codusu", prov.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			prov.CodProveedorNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsProveedor prov)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprov", prov.CodProveedor);
			cmd.Parameters.AddWithValue("razonsocial", prov.RazonSocial);
			cmd.Parameters.AddWithValue("ruc", prov.Ruc);
			cmd.Parameters.AddWithValue("direccion", prov.Direccion);
			cmd.Parameters.AddWithValue("telefono", prov.Telefono);
			cmd.Parameters.AddWithValue("fax", prov.Fax);
			cmd.Parameters.AddWithValue("representante", prov.Representante);
			cmd.Parameters.AddWithValue("email", prov.Mail);
			cmd.Parameters.AddWithValue("contacto", prov.Contacto);
			cmd.Parameters.AddWithValue("telefonocon", prov.TelefonoContacto);
			cmd.Parameters.AddWithValue("frecuencia", prov.FrecuenciaVisita);
			cmd.Parameters.AddWithValue("margen", prov.Margen);
			cmd.Parameters.AddWithValue("banco", prov.Banco);
			cmd.Parameters.AddWithValue("ctacte", prov.CtaCte);
			cmd.Parameters.AddWithValue("comentario", prov.Comentario);
			cmd.Parameters.AddWithValue("estado", prov.Estado);
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

	public bool Delete(int CodProveedor)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprov", CodProveedor);
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

	public clsProveedor CargaProveedor(int Codigo)
	{
		clsProveedor prov = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraProveedor", con.conector);
			cmd.Parameters.AddWithValue("codprov", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					prov = new clsProveedor();
					prov.CodProveedor = Convert.ToInt32(dr.GetDecimal(0));
					prov.RazonSocial = dr.GetString(1);
					prov.Ruc = dr.GetString(2);
					prov.Direccion = dr.GetString(3);
					prov.Telefono = dr.GetString(4);
					prov.Fax = dr.GetString(5);
					prov.Representante = dr.GetString(6);
					prov.Contacto = dr.GetString(7);
					prov.TelefonoContacto = dr.GetString(8);
					prov.FrecuenciaVisita = Convert.ToInt32(dr.GetInt32(9));
					prov.Margen = Convert.ToDouble(dr.GetDecimal(10));
					prov.Banco = dr.GetString(11);
					prov.CtaCte = dr.GetString(12);
					prov.Comentario = dr.GetString(13);
					prov.Estado = dr.GetBoolean(14);
					prov.FechaRegistro = dr.GetDateTime(15);
					prov.Departamento = dr.GetString(16);
					prov.Provincia = dr.GetString(17);
					prov.Distrito = dr.GetString(18);
					prov.Mail = dr.GetString(19);
				}
			}
			return prov;
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

	public clsProveedor BuscaProveedor(string RUC)
	{
		clsProveedor prov = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaProveedorNota", con.conector);
			cmd.Parameters.AddWithValue("ru", RUC);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					prov = new clsProveedor();
					prov.CodProveedor = Convert.ToInt32(dr.GetDecimal(0));
					prov.RazonSocial = dr.GetString(1);
					prov.Ruc = dr.GetString(2);
				}
			}
			return prov;
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

	public DataTable ListaProveedores()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProveedores", con.conector);
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

	public DataTable RelacionProveedores()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProveedores", con.conector);
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

	public DataTable BuscaProveedores(int Criterio, string Filtro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("FiltraProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("criterio", Criterio);
			cmd.Parameters.AddWithValue("filtro", Filtro);
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

	public DataTable CargaProveedores()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaProveedores", con.conector);
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

	public DataTable ListaCorreosProveedores(int codpro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCorreosProveedores", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codpro);
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

	public bool CambiaProveedor(int codProd, int codProv1, int codProv2)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CambiarProveedor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codProd);
			cmd.Parameters.AddWithValue("codprove", codProv1);
			cmd.Parameters.AddWithValue("codprov", codProv2);
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
}
