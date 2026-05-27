using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlFormulario : IFormulario
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public DataTable MuestraFormularios()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("CargaFormularios", con.conector);
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

	public int getPermisoEditarPlantilla()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoEditarPlantilla", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoAnularOrdenCompra()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoAnularOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoGenerarPropuestaDeCompra()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoGenerarPropuestaCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoAprobarOrdenCompra()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoAprobarOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoCerrarOrdenCompra()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoCerrarOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public bool ejecutarActualizacionDatosProductosDePlantillas()
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizacionDatosProductosDePlantillas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public int getPermisoEditarPlantilladeReqAlmacen()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoEditarPlantillaReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoGenerarPropuestaDeReqAlmacen()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoGenerarPropuestaReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public DataTable cargaListadoProductosADespachar(int tipoFecha, DateTime fecha1, DateTime fecha2, int codAlmacen, int codSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaProductosADespachar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_fechaDesde", fecha1);
			cmd.Parameters.AddWithValue("_fechaHasta", fecha2);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", codSucursal);
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

	public DataTable reporteDetalladoListadoProductosADespachar(int tipoFecha, DateTime fecha1, DateTime fecha2, int codAlmacen, int codSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ExportacionDataDocSegunProductosADespachar", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_fechaDesde", fecha1);
			cmd.Parameters.AddWithValue("_fechaHasta", fecha2);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", codSucursal);
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

	public DataTable cargaDocumentosDeProductosADespachar(int codProducto, int codUnidad, int codAlmacen, int codSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocDespacharSegunProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codProducto", codProducto);
			cmd.Parameters.AddWithValue("_codUnidad", codUnidad);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", codSucursal);
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

	public DataTable cargaDocumentosDeProductosADespachar2(int codProducto, int codUnidad, int codAlmacen, int codSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDocDespacharSegunProductoUnidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codProducto", codProducto);
			cmd.Parameters.AddWithValue("_codUnidad", codUnidad);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", codSucursal);
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

	public int getPermisoCrearDespachoDesdeVenta()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoCrearDespachoDesdeVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoGenerarEntrega()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoGenerarEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoAnularEntrega()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoAnularEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoAnularVentas()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoAnularVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoPasarPendiente()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoPasarPendiente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoEliminarPlantillaAlmacen()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoEliminarPlantillaAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoEliminarPlantillaOrdenCompra()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoEliminarPlantillaOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoEditarReqReposStock()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoEditarReqReposStock", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoAceptarTransferencia()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoAceptarTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoRechazarTransferencia()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoRechazarTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoExportarExcelVentas()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoExportarExcelVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public int getPermisoAumentarPreciodeCompraFacturaGeneradaGR()
	{
		int codigo = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoPermisoAumentarPrecioCompraFacturaGeneradaGRC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			return codigo;
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

	public DataTable listadoTotalCanalesVenta()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listadoTotalDeCanalesVenta", con.conector);
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
