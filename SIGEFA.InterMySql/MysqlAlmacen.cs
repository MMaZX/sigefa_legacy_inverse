using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlAlmacen : IAlmacen
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsAlmacen alm)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codemp", alm.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("nombre", alm.Nombre);
			oParam = cmd.Parameters.AddWithValue("ubicacion", alm.Ubicacion);
			oParam = cmd.Parameters.AddWithValue("telefono", alm.Telefono);
			oParam = cmd.Parameters.AddWithValue("descripcion", alm.Descripcion);
			oParam = cmd.Parameters.AddWithValue("estado", alm.Estado);
			oParam = cmd.Parameters.AddWithValue("codusu", alm.CodUser);
			oParam = cmd.Parameters.AddWithValue("codSuc", alm.CodSuc);
			oParam = cmd.Parameters.AddWithValue("estado_principal_ex", alm.EstadoPrincipal);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			alm.CodAlmacenNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Update(clsAlmacen alm)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalm", alm.CodAlmacen);
			cmd.Parameters.AddWithValue("codSu", alm.CodSuc);
			cmd.Parameters.AddWithValue("nombre", alm.Nombre);
			cmd.Parameters.AddWithValue("ubicacion", alm.Ubicacion);
			cmd.Parameters.AddWithValue("telefono", alm.Telefono);
			cmd.Parameters.AddWithValue("descripcion", alm.Descripcion);
			cmd.Parameters.AddWithValue("estado", alm.Estado);
			cmd.Parameters.AddWithValue("estado_principal_ex", alm.EstadoPrincipal);
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

	public bool Delete(int CodAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
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

	public DataTable ListaAlmacenesEmp(int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacenesEmp", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable ListaAlmacenesEmp2(int codpedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacenesEmp2", con.conector);
			cmd.Parameters.AddWithValue("codpedido", codpedido);
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

	public DataTable AlmacenXUbicacion(int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("LIstadoAlmacenXUbicacionExcel", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("codalma", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(tabla);
			return tabla;
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

	public clsAlmacen CargaAlmacen(int Codigo)
	{
		clsAlmacen alm = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraAlmacen", con.conector);
			cmd.Parameters.AddWithValue("@codalm", Codigo);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					alm = new clsAlmacen();
					alm.CodAlmacen = dr.GetInt32(0);
					alm.CodEmpresa = dr.GetInt32(1);
					alm.CodSuc = dr.GetInt32(2);
					alm.Nombre = dr.GetString(3);
					alm.Ubicacion = dr.GetString(4);
					alm.Telefono = dr.GetString(5);
					alm.Descripcion = dr.GetString(6);
					alm.Estado = dr.GetBoolean(7);
					alm.CodUser = dr.GetInt32(8);
					alm.FechaRegistro = dr.GetDateTime(9);
					alm.EstadoPrincipal = dr.GetBoolean(10);
				}
			}
			return alm;
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

	public DataTable ListaAlmacenes(int codEmpresa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacenes", con.conector);
			cmd.Parameters.AddWithValue("codempre", codEmpresa);
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

	public DataTable AlmacenesDisponible(int iCodSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("SeleccionAlmacenSucursal", con.conector);
			cmd.Parameters.AddWithValue("sucur", iCodSucursal);
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

	public DataTable CargaAlmacenes(int iNivel, int iEmpresa, int iUsuario)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaAlmacenes", con.conector);
			cmd.Parameters.AddWithValue("nivel", iNivel);
			cmd.Parameters.AddWithValue("empre", iEmpresa);
			cmd.Parameters.AddWithValue("usu", iUsuario);
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

	public DataTable BuscaAlmacenes(int Criterio, string Filtro)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("FiltraAlmacen", con.conector);
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

	public DataTable ListaAlmacenes2()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacenes2", con.conector);
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

	public DataTable CargaAlmacen2(int codempre)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codempre", codempre);
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

	public DataTable RelacionProductosStockMin(int codTipo, int codAlm)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosStockMin", con.conector);
			cmd.Parameters.AddWithValue("tipo", codTipo);
			cmd.Parameters.AddWithValue("codalma", codAlm);
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

	public DataTable AlertaFacturasLetrasXVencer()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("AlertaFacturasLetrasVencidas", con.conector);
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

	public DataTable promedioVentasxAlmacen(int codProducto, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("getPromedioVentasxProductoSegunAlmacen", con.conector);
			cmd.Parameters.AddWithValue("codpro", codProducto);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public DataTable listaAlmacenxEmpresa()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacenxEmpresa", con.conector);
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

	public DataTable almacenxNombre(int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listAlmacenxnombres", con.conector);
			cmd.Parameters.AddWithValue("_codalma", codalma);
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

	public DataTable almacenxNombre2(int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listAlmacenxnombres2", con.conector);
			cmd.Parameters.AddWithValue("_codalma", codalma);
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

	public DataTable almacenesReporte()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaAlmacen_Reporte", con.conector);
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

	public DataTable MuestraAlmacenesConRA(int codPedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("AlmacenesConReqAlm", con.conector);
			cmd.Parameters.AddWithValue("_codPedido", codPedido);
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
