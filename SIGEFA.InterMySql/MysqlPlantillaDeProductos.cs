using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlPlantillaDeProductos : IPlantillaDeProductos
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public int insertproductosagrupados(clsPlantillaDeProductos planilla_producto)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaProductosAgrupos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_nombre", planilla_producto.Nombre);
			oParamP = cmd.Parameters.AddWithValue("_descripcion", planilla_producto.Descripcion);
			oParamP = cmd.Parameters.AddWithValue("_fechaRegistro", planilla_producto.FechaRegistro);
			oParamP = cmd.Parameters.AddWithValue("_fechaEdicion", planilla_producto.FechaEdicion);
			oParamP = cmd.Parameters.AddWithValue("_codAlmacen", planilla_producto.Cod_almacen);
			oParamP = cmd.Parameters.AddWithValue("_descripAlmacen", planilla_producto.Descrip_almacen);
			oParamP = cmd.Parameters.AddWithValue("_codUsuario", planilla_producto.Cod_usuario);
			oParamP = cmd.Parameters.AddWithValue("_nombreUsuario", planilla_producto.Nombre_usuario);
			oParamP = cmd.Parameters.AddWithValue("_tipo", planilla_producto.Tipo);
			oParamP = cmd.Parameters.AddWithValue("newid", 0);
			oParamP.Direction = ParameterDirection.Output;
			int xP = cmd.ExecuteNonQuery();
			planilla_producto.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)Convert.ToString(planilla_producto.Codigo), (Func<char, bool>)char.IsDigit) || Convert.ToString(planilla_producto.Codigo) == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return -1;
			}
			foreach (clsDetallePlantillaDeProductos detalle in planilla_producto.LDetalle)
			{
				cmd = new MySqlCommand("GuardaDetalleProductosAgrupos", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_plantilla", planilla_producto.Codigo);
				oParam = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam = cmd.Parameters.AddWithValue("_undxpaquete", detalle.Cantidad);
				oParam = cmd.Parameters.AddWithValue("_marca", detalle.Marca);
				oParam = cmd.Parameters.AddWithValue("_familia", detalle.Famiilia);
				oParam = cmd.Parameters.AddWithValue("_linea", detalle.Linea);
				oParam = cmd.Parameters.AddWithValue("_grupo", detalle.Grupo);
				if (double.IsNaN(detalle.StockActual))
				{
					oParam = cmd.Parameters.AddWithValue("_stockactual", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockactual", detalle.StockActual);
				}
				if (double.IsNaN(detalle.StockMinimo))
				{
					oParam = cmd.Parameters.AddWithValue("_stockmin", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockmin", detalle.StockMinimo);
				}
				if (double.IsNaN(detalle.StockMaximo))
				{
					oParam = cmd.Parameters.AddWithValue("_stockmax", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockmax", detalle.StockMaximo);
				}
				oParam = cmd.Parameters.AddWithValue("_opcionRecuento", detalle.OpcionRecuentoCantidad);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return -1;
			}
			Scope.Complete();
			Scope.Dispose();
			return planilla_producto.Codigo;
		}
		catch (MySqlException)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			rpta = false;
			return -1;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualizaPlantilla(clsPlantillaDeProductos planilla_producto, List<clsDetallePlantillaDeProductos> listainsertar, List<clsDetallePlantillaDeProductos> listaeliminar, List<clsDetallePlantillaDeProductos> listaactualizar)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaProductosAgrupos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codigo", planilla_producto.Codigo);
			oParamP = cmd.Parameters.AddWithValue("_nombre", planilla_producto.Nombre);
			oParamP = cmd.Parameters.AddWithValue("_descripcion", planilla_producto.Descripcion);
			oParamP = cmd.Parameters.AddWithValue("_fechaRegistro", planilla_producto.FechaRegistro);
			oParamP = cmd.Parameters.AddWithValue("_fechaEdicion", planilla_producto.FechaEdicion);
			oParamP = cmd.Parameters.AddWithValue("_codAlmacen", planilla_producto.Cod_almacen);
			oParamP = cmd.Parameters.AddWithValue("_descripAlmacen", planilla_producto.Descrip_almacen);
			oParamP = cmd.Parameters.AddWithValue("_codUsuario", planilla_producto.Cod_usuario);
			oParamP = cmd.Parameters.AddWithValue("_nombreUsuario", planilla_producto.Nombre_usuario);
			oParamP = cmd.Parameters.AddWithValue("_tipo", planilla_producto.Tipo);
			if (cmd.ExecuteNonQuery() == 0)
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePlantillaDeProductos detalle in listainsertar)
			{
				cmd = new MySqlCommand("GuardaDetalleProductosAgrupos", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_plantilla", planilla_producto.Codigo);
				oParam = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam = cmd.Parameters.AddWithValue("_undxpaquete", detalle.Cantidad);
				oParam = cmd.Parameters.AddWithValue("_marca", detalle.Marca);
				oParam = cmd.Parameters.AddWithValue("_familia", detalle.Famiilia);
				oParam = cmd.Parameters.AddWithValue("_linea", detalle.Linea);
				oParam = cmd.Parameters.AddWithValue("_grupo", detalle.Grupo);
				if (double.IsNaN(detalle.StockActual))
				{
					oParam = cmd.Parameters.AddWithValue("_stockactual", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockactual", detalle.StockActual);
				}
				if (double.IsNaN(detalle.StockMinimo))
				{
					oParam = cmd.Parameters.AddWithValue("_stockmin", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockmin", detalle.StockMinimo);
				}
				if (double.IsNaN(detalle.StockMaximo))
				{
					oParam = cmd.Parameters.AddWithValue("_stockmax", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockmax", detalle.StockMaximo);
				}
				oParam = cmd.Parameters.AddWithValue("_opcionRecuento", detalle.OpcionRecuentoCantidad);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			foreach (clsDetallePlantillaDeProductos detalle2 in listaactualizar)
			{
				cmd = new MySqlCommand("ActualizaDetalleProductosAgrupos", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_plantilla", planilla_producto.Codigo);
				oParam2 = cmd.Parameters.AddWithValue("_id_detalle_plantilla", detalle2.Codigo);
				oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle2.Codigo_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle2.Ref_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle2.Descrip_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle2.Codigo_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle2.Descripcion_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_undxpaquete", detalle2.Cantidad);
				oParam2 = cmd.Parameters.AddWithValue("_marca", detalle2.Marca);
				oParam2 = cmd.Parameters.AddWithValue("_familia", detalle2.Famiilia);
				oParam2 = cmd.Parameters.AddWithValue("_linea", detalle2.Linea);
				oParam2 = cmd.Parameters.AddWithValue("_grupo", detalle2.Grupo);
				if (double.IsNaN(detalle2.StockActual))
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockactual", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockactual", detalle2.StockActual);
				}
				if (double.IsNaN(detalle2.StockMinimo))
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockmin", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockmin", detalle2.StockMinimo);
				}
				if (double.IsNaN(detalle2.StockMaximo))
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockmax", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockmax", detalle2.StockMaximo);
				}
				oParam2 = cmd.Parameters.AddWithValue("_opcionRecuento", detalle2.OpcionRecuentoCantidad);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
				}
			}
			foreach (clsDetallePlantillaDeProductos detalle3 in listaeliminar)
			{
				cmd = new MySqlCommand("EliminaDetalleProductosAgrupos", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam3 = cmd.Parameters.AddWithValue("_id_plantilla", planilla_producto.Codigo);
				oParam3 = cmd.Parameters.AddWithValue("_id_detalle_plantilla", detalle3.Codigo);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			Scope.Complete();
			Scope.Dispose();
			return rpta;
		}
		catch (MySqlException)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public DataTable listaplantillas(int codAlmacen, int codSucursal, int codProducto, int tipoPlantilla, int tipoFecha, DateTime fechaInicio, DateTime fechaFin, int codEm)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPlantillasConCaracteristicas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codalma", codAlmacen);
			cmd.Parameters.AddWithValue("_codsuc", codSucursal);
			cmd.Parameters.AddWithValue("_codprod", codProducto);
			cmd.Parameters.AddWithValue("_tipoPlantilla", tipoPlantilla);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_fechaInicio", fechaInicio);
			cmd.Parameters.AddWithValue("_fechaFin", fechaFin);
			cmd.Parameters.AddWithValue("_codEmp", codEm);
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

	public bool cambiaEstadoPlantilla(int codPlantilla, int estado, int codusuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CambiaEstadoProductoAgrupado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codPlantilla", codPlantilla);
			oParamP = cmd.Parameters.AddWithValue("_estado", estado);
			oParamP = cmd.Parameters.AddWithValue("_codusuario", codusuario);
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

	public DataTable listaPlantillasXProducto(int codAlmacen, int codSucursal, int codProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPlantillasPorProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codalma", codAlmacen);
			cmd.Parameters.AddWithValue("_codsuc", codSucursal);
			cmd.Parameters.AddWithValue("_codprod", codProducto);
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

	public DataTable listatipoplantillas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTipoPlantillas", con.conector);
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

	public DataTable listaplantillas(int codalma, int codsuc)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPlantillas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codalma", codalma);
			cmd.Parameters.AddWithValue("_codsuc", codsuc);
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

	public DataTable listaDetPlantillas()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarDetTotalProductos_Agrupados", con.conector);
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

	public DataTable listaPlantillasPorGenerar(int codalma, int codsuc, int tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoPlantillasSinPropuestaConStockMenor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codalma", codalma);
			cmd.Parameters.AddWithValue("_codsuc", codsuc);
			cmd.Parameters.AddWithValue("_tipo", tipo);
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

	public clsPlantillaDeProductos cargaProductoAgrupado(int CodPro)
	{
		clsPlantillaDeProductos pla = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaDatosProductoAgrupadosCabecera", con.conector);
			cmd.Parameters.AddWithValue("codPlantilla", CodPro);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pla = new clsPlantillaDeProductos();
					pla.Codigo = dr.GetInt32(0);
					pla.Nombre = dr.GetString(1);
					pla.Descripcion = dr.GetString(2);
					pla.FechaRegistro = dr.GetDateTime(3);
					pla.FechaEdicion = dr.GetDateTime(4);
					pla.Cod_almacen = dr.GetInt32(5);
					pla.Descrip_almacen = dr.GetString(6);
					pla.Cod_usuario = dr.GetInt32(7);
					pla.Nombre_usuario = dr.GetString(8);
					pla.Tipo = dr.GetInt32(9);
					pla.Estado = dr.GetInt32(10);
				}
			}
			return pla;
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

	public DataTable cargadetalleproductosagrupados(int codproductoo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleProductosAgrupados", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codproductoo);
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

	public DataTable cargadetalleproductosagrupados_111(int codproductoo, int codEmp)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleProductosAgrupados_111", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", codproductoo);
			cmd.Parameters.AddWithValue("CodEm", codEmp);
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

	public bool validarEliminacionPlantilla(int codigo, int tipo, out string mensaje)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("validarEliminacionPlantillaProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codPlantilla", codigo);
			oParamP = cmd.Parameters.AddWithValue("_tipo", tipo);
			dr = cmd.ExecuteReader();
			int cantidad = 0;
			mensaje = "No se ejecuto la validacion de eliminacion de plantilla";
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cantidad = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
					mensaje = (dr.IsDBNull(1) ? "Error mensaje vacio" : dr.GetString(1));
				}
			}
			if (cantidad == 0)
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

	public bool existeProducto(string codProducto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("existeProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codProducto", codProducto);
			dr = cmd.ExecuteReader();
			int cantidad = 0;
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cantidad = ((!dr.IsDBNull(0)) ? dr.GetInt32(0) : 0);
				}
			}
			if (cantidad == 0)
			{
				return false;
			}
			return true;
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

	public bool SetDataProductosPlantillas(clsDetallePlantillaDeProductos aux, int codAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ConfiguraDataPlantillasProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codProducto", aux.Codigo_Producto);
			oParamP = cmd.Parameters.AddWithValue("_stockMaximo", aux.StockMaximo);
			oParamP = cmd.Parameters.AddWithValue("_stockMinimo", aux.StockMinimo);
			oParamP = cmd.Parameters.AddWithValue("_ctdadxpaquete", aux.Cantidad);
			oParamP = cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
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

	public bool actualizaStockdeDetallePlantilla(clsDetallePlantillaDeProductos aux)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaStocksDetallePlantilla", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codDetallePlantilla", aux.Codigo);
			oParamP = cmd.Parameters.AddWithValue("_stockMaximo", aux.StockMaximo);
			oParamP = cmd.Parameters.AddWithValue("_stockMinimo", aux.StockMinimo);
			oParamP = cmd.Parameters.AddWithValue("_ctdadxpaquete", aux.Cantidad);
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

	public bool actualizadatosproducto(clsProducto producto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDatosProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_CodProducto", producto.CodProducto);
			oParamP = cmd.Parameters.AddWithValue("_stockMinimo", producto.StockMinimo);
			oParamP = cmd.Parameters.AddWithValue("_stockMaximo", producto.StockMaximo);
			oParamP = cmd.Parameters.AddWithValue("_descontinuado", producto.descontinuado);
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
