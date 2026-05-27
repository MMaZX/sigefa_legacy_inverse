using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;

namespace SIGEFA.InterMySql;

internal class MysqlPropuestaDePedido
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public int insertpropuestapedido_oc(clsPropuestaDePedido propuesta_pedido)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPropuestadePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_titulo", propuesta_pedido.Titulo);
			oParamP = cmd.Parameters.AddWithValue("_descripcion", propuesta_pedido.Descripcion);
			oParamP = cmd.Parameters.AddWithValue("_fechaRegistro", propuesta_pedido.Fecharegitro);
			oParamP = cmd.Parameters.AddWithValue("_fechaEdicion", propuesta_pedido.Fechaedicion);
			if (propuesta_pedido.Fechageneracion == DateTime.MinValue)
			{
				oParamP = cmd.Parameters.AddWithValue("_fechaGeneracion", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_fechaGeneracion", propuesta_pedido.Fechageneracion);
			}
			oParamP = cmd.Parameters.AddWithValue("_codAlmacen", propuesta_pedido.Cod_almacen);
			oParamP = cmd.Parameters.AddWithValue("_descripAlmacen", propuesta_pedido.Descrip_almacen);
			oParamP = cmd.Parameters.AddWithValue("_codUsuario", propuesta_pedido.Cod_usuario);
			oParamP = cmd.Parameters.AddWithValue("_nombreUsuario", propuesta_pedido.Nombre_usuario);
			oParamP = cmd.Parameters.AddWithValue("_tipo", propuesta_pedido.Tipo);
			oParamP = cmd.Parameters.AddWithValue("_estado", propuesta_pedido.Estado);
			if (propuesta_pedido.CodPlantillaGenerada == 0)
			{
				oParamP = cmd.Parameters.AddWithValue("_codplantillagenerada", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_codplantillagenerada", propuesta_pedido.CodPlantillaGenerada);
			}
			oParamP = cmd.Parameters.AddWithValue("newid", 0);
			oParamP.Direction = ParameterDirection.Output;
			int xP = cmd.ExecuteNonQuery();
			propuesta_pedido.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)Convert.ToString(propuesta_pedido.Codigo), (Func<char, bool>)char.IsDigit) || Convert.ToString(propuesta_pedido.Codigo) == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return -1;
			}
			foreach (clsCotizacionPropuestaDePedido item in propuesta_pedido.LCotizaciones)
			{
				cmd = new MySqlCommand("GuardaCotizacionPropuestadePedidoOrdenDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				item.CodigoPropuesta = propuesta_pedido.Codigo;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_propuesta_oc", item.CodigoPropuesta);
				oParam = cmd.Parameters.AddWithValue("_id_proveedor", item.CodigoProveedor);
				oParam = cmd.Parameters.AddWithValue("_descrip_proveedor", item.DescripcionProveedor);
				oParam = cmd.Parameters.AddWithValue("_doc_cotizacion", item.DocCotizacion);
				oParam = cmd.Parameters.AddWithValue("_codMoneda", item.CodigoMoneda);
				oParam = cmd.Parameters.AddWithValue("_tipoPrecio", item.TipoPrecio);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				item.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (item.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			foreach (clsDetallePropuestaDePedido detalle in propuesta_pedido.LDetalle)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				detalle.Cod_Propuesta = propuesta_pedido.Codigo;
				MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				oParam2 = cmd.Parameters.AddWithValue("_precioUnitActual", detalle.Precio_unit_actual);
				oParam2 = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				oParam2 = cmd.Parameters.AddWithValue("_nroItem", detalle.NroItem);
				oParam2 = cmd.Parameters.AddWithValue("newid", 0);
				oParam2.Direction = ParameterDirection.Output;
				int xDP2 = cmd.ExecuteNonQuery();
				detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			foreach (clsCotizacionPropuestaDePedido item2 in propuesta_pedido.LCotizaciones)
			{
				foreach (clsDetalleCotizacionPropuestaDePedido det_cpdp in item2.ListadoPrecios)
				{
					if (!rpta)
					{
						break;
					}
					List<clsDetallePropuestaDePedido> CodDetalles = Enumerable.Select<clsDetallePropuestaDePedido, clsDetallePropuestaDePedido>(Enumerable.Where<clsDetallePropuestaDePedido>(propuesta_pedido.LDetalle.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo_Producto == det_cpdp.CodigoProducto)), (Func<clsDetallePropuestaDePedido, clsDetallePropuestaDePedido>)((clsDetallePropuestaDePedido x) => x)).ToList();
					if (CodDetalles.Count == 1)
					{
						det_cpdp.CodigoDetallePropuesta = CodDetalles[0].Codigo;
						det_cpdp.CodigoCotizacion = item2.Codigo;
						cmd = new MySqlCommand("GuardaDetalleCotizacionPropuestaDePedidoCompra", con.conector);
						cmd.CommandType = CommandType.StoredProcedure;
						MySqlParameter oParam3 = cmd.Parameters.AddWithValue("_id_detalle_propuesta_oc", det_cpdp.CodigoDetallePropuesta);
						oParam3 = cmd.Parameters.AddWithValue("_id_cotizacion_pdoc", det_cpdp.CodigoCotizacion);
						oParam3 = cmd.Parameters.AddWithValue("_precio_compra_cotizacion", det_cpdp.PrecioCompra);
						if (cmd.ExecuteNonQuery() == 0)
						{
							rpta = false;
							break;
						}
						continue;
					}
					rpta = false;
					break;
				}
				if (!rpta)
				{
					break;
				}
			}
			foreach (clsDetallePropuestaDePedido item3 in propuesta_pedido.LDetalle)
			{
				if (item3.Cod_proveedor_seleccionado > 0)
				{
					List<clsCotizacionPropuestaDePedido> cotizaciones = Enumerable.Select<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>(Enumerable.Where<clsCotizacionPropuestaDePedido>(propuesta_pedido.LCotizaciones.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == item3.Cod_proveedor_seleccionado)), (Func<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>)((clsCotizacionPropuestaDePedido x) => x)).ToList();
					if (cotizaciones.Count <= 0)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						return -1;
					}
					cmd = new MySqlCommand("AsignaCotizacionSeleccionadaDetallePropuestaDeCompra", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					MySqlParameter oParamP2 = cmd.Parameters.AddWithValue("_cod_detalle_propuesta", item3.Codigo);
					oParamP2 = cmd.Parameters.AddWithValue("_cod_cotizacion", cotizaciones[0].Codigo);
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
					}
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
			return propuesta_pedido.Codigo;
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

	public int insertpropuestapedido_ra(clsPropuestaDePedido propuesta_pedido)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPropuestadePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_titulo", propuesta_pedido.Titulo);
			oParamP = cmd.Parameters.AddWithValue("_descripcion", propuesta_pedido.Descripcion);
			oParamP = cmd.Parameters.AddWithValue("_fechaRegistro", propuesta_pedido.Fecharegitro);
			oParamP = cmd.Parameters.AddWithValue("_fechaEdicion", propuesta_pedido.Fechaedicion);
			if (propuesta_pedido.Fechageneracion == DateTime.MinValue)
			{
				oParamP = cmd.Parameters.AddWithValue("_fechaGeneracion", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_fechaGeneracion", propuesta_pedido.Fechageneracion);
			}
			oParamP = cmd.Parameters.AddWithValue("_codAlmacen", propuesta_pedido.Cod_almacen);
			oParamP = cmd.Parameters.AddWithValue("_descripAlmacen", propuesta_pedido.Descrip_almacen);
			oParamP = cmd.Parameters.AddWithValue("_codUsuario", propuesta_pedido.Cod_usuario);
			oParamP = cmd.Parameters.AddWithValue("_nombreUsuario", propuesta_pedido.Nombre_usuario);
			oParamP = cmd.Parameters.AddWithValue("_tipo", propuesta_pedido.Tipo);
			oParamP = cmd.Parameters.AddWithValue("_estado", propuesta_pedido.Estado);
			if (propuesta_pedido.CodPlantillaGenerada == 0)
			{
				oParamP = cmd.Parameters.AddWithValue("_codplantillagenerada", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_codplantillagenerada", propuesta_pedido.CodPlantillaGenerada);
			}
			oParamP = cmd.Parameters.AddWithValue("newid", 0);
			oParamP.Direction = ParameterDirection.Output;
			int xP = cmd.ExecuteNonQuery();
			propuesta_pedido.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)Convert.ToString(propuesta_pedido.Codigo), (Func<char, bool>)char.IsDigit) || Convert.ToString(propuesta_pedido.Codigo) == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return -1;
			}
			foreach (clsAlmacenPropuestaDePedido item in propuesta_pedido.LAlmacenes)
			{
				cmd = new MySqlCommand("GuardaAlmacenPropuestadePedidoReqAlmacen", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				item.CodigoPropuesta = propuesta_pedido.Codigo;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_propuesta_ra", item.CodigoPropuesta);
				oParam = cmd.Parameters.AddWithValue("_id_almacen", item.CodigoAlmacen);
				oParam = cmd.Parameters.AddWithValue("_descrip_almacen", item.DescripcionAlmacen);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				item.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (item.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			foreach (clsDetallePropuestaDePedido detalle in propuesta_pedido.LDetalle)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				detalle.Cod_Propuesta = propuesta_pedido.Codigo;
				MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				oParam2 = cmd.Parameters.AddWithValue("_precioUnitActual", detalle.Precio_unit_actual);
				oParam2 = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				oParam2 = cmd.Parameters.AddWithValue("_nroItem", detalle.NroItem);
				oParam2 = cmd.Parameters.AddWithValue("newid", 0);
				oParam2.Direction = ParameterDirection.Output;
				int xDP2 = cmd.ExecuteNonQuery();
				detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			foreach (clsDetallePropuestaDePedido item2 in propuesta_pedido.LDetalle)
			{
				if (item2.Cod_proveedor_seleccionado > 0)
				{
					List<clsAlmacenPropuestaDePedido> almacenes_pdra = Enumerable.Select<clsAlmacenPropuestaDePedido, clsAlmacenPropuestaDePedido>(Enumerable.Where<clsAlmacenPropuestaDePedido>(propuesta_pedido.LAlmacenes.AsEnumerable(), (Func<clsAlmacenPropuestaDePedido, bool>)((clsAlmacenPropuestaDePedido x) => x.CodigoAlmacen == item2.Cod_proveedor_seleccionado)), (Func<clsAlmacenPropuestaDePedido, clsAlmacenPropuestaDePedido>)((clsAlmacenPropuestaDePedido x) => x)).ToList();
					if (almacenes_pdra.Count <= 0)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						return -1;
					}
					cmd = new MySqlCommand("AsignaAlmacenSeleccionadoDetallePropuestaDeReqAlmacen", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					MySqlParameter oParamP2 = cmd.Parameters.AddWithValue("_cod_detalle_propuesta", item2.Codigo);
					oParamP2 = cmd.Parameters.AddWithValue("_cod_almacen_pdra", almacenes_pdra[0].Codigo);
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
					}
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
			return propuesta_pedido.Codigo;
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

	internal DataTable lista_req_almacen_generados(int codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoRequerimientoAlmacenGeneradosPropuesta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codPropuestaPedido", codigo);
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

	internal DataTable cargaDetallePropuestaDePedidoVisualizacionSegunPlantillaGenerada(int codPlantillaGenerada, int codalmacen, int codPropPedi)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetallePropuestaDePedidoVisualizacionSegunPlantilla", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codplantilla", codPlantillaGenerada);
			cmd.Parameters.AddWithValue("_codalmacen", codalmacen);
			cmd.Parameters.AddWithValue("_codproppedi", codPropPedi);
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

	internal DataTable listadoPropuestaOrdenDeCompraSegunFecha(int tipo_propuesta, int cod_almacen, int cod_sucursal, int fecha, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoPropuestaDePedidoSegunFecha", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_tipo_propuesta", tipo_propuesta);
			cmd.Parameters.AddWithValue("_codalma", cod_almacen);
			cmd.Parameters.AddWithValue("_codsuc", cod_sucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", fecha);
			cmd.Parameters.AddWithValue("_fechaDesde", desde);
			cmd.Parameters.AddWithValue("_fechaHasta", hasta);
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

	internal DataTable listarOrdenesDeCompraGeneradas(int codPropPedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listarOrdenesDeCompraGeneradasPorPropuestaPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codProdPed", codPropPedido);
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

	internal DataTable ListaModificacionesParaPUC()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoModificacionParaPrecioUnitCompraPropuestadePedido", con.conector);
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

	internal DataTable ListaAlmacenesParaPropRA(int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoAlmacenesParaPropuestaReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_cod_almacen", codAlmacen);
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

	internal DataTable listarPropuestasXProducto(int tipo_propuesta, int tipoFecha, DateTime desde, DateTime hasta, int codAlmacen, int codSucursal, int codProd)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoPropuestaDePedidoPorProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_tipo_propuesta", tipo_propuesta);
			cmd.Parameters.AddWithValue("_codalma", codAlmacen);
			cmd.Parameters.AddWithValue("_codsuc", codSucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_fechaDesde", desde);
			cmd.Parameters.AddWithValue("_fechaHasta", hasta);
			cmd.Parameters.AddWithValue("_codProd", codProd);
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

	internal bool eliminarPropuestaDePedidoVisualizacion(int codigoProp)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaDetalleDePropuestaDePedidoVisualizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codigo_prop", codigoProp);
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

	public bool eliminarPropuestaDePedido(int codigo_prop, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEstadoEliminadoPropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codigo_prop", codigo_prop);
			oParamP = cmd.Parameters.AddWithValue("_estado", estado);
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

	internal clsDetallePropuestaDePedido getDetallePropuesta(int codigo_Producto, int codigoPropuesta)
	{
		clsDetallePropuestaDePedido det_pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetDetallePropuestaDePedidoSegunProducto", con.conector);
			cmd.Parameters.AddWithValue("_codProducto", codigo_Producto);
			cmd.Parameters.AddWithValue("_codPropuesta", codigoPropuesta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					det_pro = new clsDetallePropuestaDePedido();
					det_pro.Codigo = dr.GetInt32(0);
					det_pro.Cod_Propuesta = dr.GetInt32(1);
					det_pro.Codigo_Producto = dr.GetInt32(2);
					det_pro.Ref_Producto = dr.GetString(3);
					det_pro.Descrip_Producto = dr.GetString(4);
					det_pro.Codigo_Unidad = dr.GetInt32(5);
					det_pro.Descripcion_Unidad = dr.GetString(6);
					det_pro.Cantidad_reponer = dr.GetDouble(7);
					det_pro.Cantidad_sugerida = dr.GetDouble(8);
					det_pro.Pedido_final = (dr.IsDBNull(9) ? double.NaN : dr.GetDouble(9));
					det_pro.Precio_unit_actual = dr.GetDouble(10);
					det_pro.Cod_cotizacion_seleccionada = ((!dr.IsDBNull(11)) ? dr.GetInt32(11) : 0);
					det_pro.StockDisponible = dr.GetDouble(12);
				}
			}
			return det_pro;
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

	internal void actualizaDetallePropuestaRecalculoVisualizacion(List<clsDetallePropuestaDePedido> lista_vis_act)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaDetalleDePropuestaDePedidoVisualizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam1 = cmd.Parameters.AddWithValue("_id_propuesta", lista_vis_act.First().Cod_Propuesta);
			if (cmd.ExecuteNonQuery() == 0)
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				throw new Exception("Ocurrio un error al eliminar tabla de listado total de propuesta de pedido visualizacion");
			}
			foreach (clsDetallePropuestaDePedido detalle in lista_vis_act)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedidoVisualizacion2", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle.Codigo);
				oParam2 = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				oParam2 = cmd.Parameters.AddWithValue("_precioUnitActual", detalle.Precio_unit_actual);
				oParam2 = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				oParam2 = cmd.Parameters.AddWithValue("_opcionRecuento", detalle.OpcionRecuento);
				if (double.IsNaN(detalle.StockMinimo))
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockMinimo", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_stockMinimo", detalle.StockMinimo);
				}
				oParam2 = cmd.Parameters.AddWithValue("_stockMaximo", detalle.StockMaximo);
				oParam2 = cmd.Parameters.AddWithValue("_unidadXPaquete", detalle.UnidadXPaquete);
				oParam2 = cmd.Parameters.AddWithValue("newid", 0);
				oParam2.Direction = ParameterDirection.Output;
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
				if (detalle.Codigo == 0)
				{
					detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
		}
		catch (MySqlException ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			rpta = false;
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool cambiaEstadoPropuesta(int codigo_prop, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEstadoPropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codigo_prop", codigo_prop);
			oParamP = cmd.Parameters.AddWithValue("_estado", estado);
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

	internal bool actualizaDetallePropuestaRecalculo(List<clsDetallePropuestaDePedido> lista_ins, List<clsDetallePropuestaDePedido> lista_act, List<clsDetallePropuestaDePedido> lista_del)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			foreach (clsDetallePropuestaDePedido detalle in lista_act)
			{
				cmd = new MySqlCommand("ActualizaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle.Codigo);
				oParam = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				double valor = Math.Round(detalle.Precio_unit_actual, 2);
				oParam = cmd.Parameters.AddWithValue("_precioUnitActual", valor);
				oParam = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				if (detalle.NroItem == 0)
				{
					oParam = cmd.Parameters.AddWithValue("_nroItem", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_nroItem", detalle.NroItem);
				}
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido detalle2 in lista_ins)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_propuesta", detalle2.Cod_Propuesta);
				oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle2.Codigo_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle2.Ref_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle2.Descrip_Producto);
				oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle2.Codigo_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle2.Descripcion_Unidad);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadReponer", detalle2.Cantidad_reponer);
				oParam2 = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle2.Cantidad_sugerida);
				oParam2 = cmd.Parameters.AddWithValue("_precioUnitActual", detalle2.Precio_unit_actual);
				oParam2 = cmd.Parameters.AddWithValue("_stockActual", detalle2.StockDisponible);
				if (double.IsNaN(detalle2.Pedido_final))
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", detalle2.Pedido_final);
				}
				if (detalle2.NroItem == 0)
				{
					oParam2 = cmd.Parameters.AddWithValue("_nroItem", null);
				}
				else
				{
					oParam2 = cmd.Parameters.AddWithValue("_nroItem", detalle2.NroItem);
				}
				oParam2 = cmd.Parameters.AddWithValue("newid", 0);
				oParam2.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle2.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle2.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido detalle3 in lista_del)
			{
				cmd = new MySqlCommand("EliminaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam3 = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle3.Codigo);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
			return rpta;
		}
		catch (MySqlException ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			rpta = false;
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualizaPropuestaDePedido(clsPropuestaDePedido propuesta_pedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("_codigo", propuesta_pedido.Codigo);
			oParamP = cmd.Parameters.AddWithValue("_titulo", propuesta_pedido.Titulo);
			oParamP = cmd.Parameters.AddWithValue("_descripcion", propuesta_pedido.Descripcion);
			oParamP = cmd.Parameters.AddWithValue("_fechaRegistro", propuesta_pedido.Fecharegitro);
			oParamP = cmd.Parameters.AddWithValue("_fechaEdicion", propuesta_pedido.Fechaedicion);
			if (propuesta_pedido.Fechageneracion == DateTime.MinValue)
			{
				oParamP = cmd.Parameters.AddWithValue("_fechaGeneracion", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_fechaGeneracion", propuesta_pedido.Fechageneracion);
			}
			oParamP = cmd.Parameters.AddWithValue("_codAlmacen", propuesta_pedido.Cod_almacen);
			oParamP = cmd.Parameters.AddWithValue("_descripAlmacen", propuesta_pedido.Descrip_almacen);
			oParamP = cmd.Parameters.AddWithValue("_codUsuario", propuesta_pedido.Cod_usuario);
			oParamP = cmd.Parameters.AddWithValue("_nombreUsuario", propuesta_pedido.Nombre_usuario);
			oParamP = cmd.Parameters.AddWithValue("_tipo", propuesta_pedido.Tipo);
			oParamP = cmd.Parameters.AddWithValue("_estado", propuesta_pedido.Estado);
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

	public clsCotizacionPropuestaDePedido cargaCotizacionDepropuestaDePedidoSeleccionada(int codigo_detalle_propuesta)
	{
		clsCotizacionPropuestaDePedido cot_pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaCotizacionSeleccionadaEnDetallePropuestaDePedido", con.conector);
			cmd.Parameters.AddWithValue("_codDetallePropuesta", codigo_detalle_propuesta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cot_pro = new clsCotizacionPropuestaDePedido();
					cot_pro.Codigo = dr.GetInt32(0);
					cot_pro.CodigoMoneda = dr.GetInt32(1);
					cot_pro.CodigoPropuesta = dr.GetInt32(2);
					cot_pro.CodigoProveedor = dr.GetInt32(3);
					cot_pro.DescripcionProveedor = dr.GetString(4);
					cot_pro.DocCotizacion = dr.GetString(5);
					cot_pro.TipoPrecio = dr.GetInt32(6);
				}
				return cot_pro;
			}
			return null;
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

	public clsAlmacenPropuestaDePedido cargaStockAlmacenDepropuestaDePedidoSeleccionada(int codigo_detalle_propuesta)
	{
		clsAlmacenPropuestaDePedido alm_pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaStockAlmacenSeleccionadoEnDetallePropuestaDePedido", con.conector);
			cmd.Parameters.AddWithValue("_codDetallePropuesta", codigo_detalle_propuesta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					alm_pro = new clsAlmacenPropuestaDePedido();
					alm_pro.Codigo = dr.GetInt32(0);
					alm_pro.CodigoPropuesta = dr.GetInt32(1);
					alm_pro.CodigoAlmacen = dr.GetInt32(2);
					alm_pro.DescripcionAlmacen = dr.GetString(3);
				}
				return alm_pro;
			}
			return null;
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

	public List<clsCotizacionPropuestaDePedido> cargaListadoDeCotizacion(int codigo_propuesta)
	{
		try
		{
			bool rpta = true;
			List<clsCotizacionPropuestaDePedido> listado_cotizaciones = new List<clsCotizacionPropuestaDePedido>();
			con.conectarBD();
			cmd = new MySqlCommand("CargaListadoCotizaciones", con.conector);
			cmd.Parameters.AddWithValue("_codPropuesta", codigo_propuesta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsCotizacionPropuestaDePedido cot_pro = new clsCotizacionPropuestaDePedido();
					cot_pro.Codigo = dr.GetInt32(0);
					cot_pro.CodigoMoneda = dr.GetInt32(1);
					cot_pro.CodigoPropuesta = dr.GetInt32(2);
					cot_pro.CodigoProveedor = dr.GetInt32(3);
					cot_pro.DescripcionProveedor = dr.GetString(4);
					cot_pro.DocCotizacion = dr.GetString(5);
					cot_pro.TipoPrecio = dr.GetInt32(6);
					listado_cotizaciones.Add(cot_pro);
				}
			}
			else
			{
				rpta = false;
			}
			if (!rpta)
			{
				return listado_cotizaciones;
			}
			dr.Close();
			foreach (clsCotizacionPropuestaDePedido item in listado_cotizaciones)
			{
				List<clsDetalleCotizacionPropuestaDePedido> listado_detalle_cotizacion = new List<clsDetalleCotizacionPropuestaDePedido>();
				cmd = new MySqlCommand("CargaListadoDetalleCotizacionesPropuesta", con.conector);
				cmd.Parameters.AddWithValue("_codCotizacion", item.Codigo);
				cmd.CommandType = CommandType.StoredProcedure;
				dr = cmd.ExecuteReader();
				clsDetalleCotizacionPropuestaDePedido det_cot_pro = null;
				if (dr.HasRows)
				{
					while (dr.Read())
					{
						det_cot_pro = new clsDetalleCotizacionPropuestaDePedido();
						det_cot_pro.CodigoCotizacion = dr.GetInt32(0);
						det_cot_pro.CodigoDetallePropuesta = dr.GetInt32(1);
						det_cot_pro.CodigoProducto = dr.GetInt32(2);
						det_cot_pro.PrecioCompra = dr.GetDouble(3);
						listado_detalle_cotizacion.Add(det_cot_pro);
					}
					item.ListadoPrecios = listado_detalle_cotizacion;
				}
				else
				{
					item.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
				}
				dr.Close();
			}
			return listado_cotizaciones;
		}
		catch (MySqlException)
		{
			return new List<clsCotizacionPropuestaDePedido>();
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public List<clsAlmacenPropuestaDePedido> cargaListadoDeAlmacenes(int codigo_propuesta)
	{
		try
		{
			bool rpta = true;
			List<clsAlmacenPropuestaDePedido> listado_cotizaciones = new List<clsAlmacenPropuestaDePedido>();
			con.conectarBD();
			cmd = new MySqlCommand("CargaListadoAlmacenes", con.conector);
			cmd.Parameters.AddWithValue("_codPropuesta", codigo_propuesta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsAlmacenPropuestaDePedido cot_pro = new clsAlmacenPropuestaDePedido();
					cot_pro.Codigo = dr.GetInt32(0);
					cot_pro.CodigoPropuesta = dr.GetInt32(1);
					cot_pro.CodigoAlmacen = dr.GetInt32(2);
					cot_pro.DescripcionAlmacen = dr.GetString(3);
					listado_cotizaciones.Add(cot_pro);
				}
			}
			else
			{
				rpta = false;
			}
			return listado_cotizaciones;
		}
		catch (MySqlException)
		{
			return new List<clsAlmacenPropuestaDePedido>();
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal DataTable listadoPropuestaOrdenDeCompra(int tipo_propuesta, int cod_almacen, int cod_sucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoPropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_tipo_propuesta", tipo_propuesta);
			cmd.Parameters.AddWithValue("_codalma", cod_almacen);
			cmd.Parameters.AddWithValue("_codsuc", cod_sucursal);
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

	public DataTable cargaDetallePropuestaDePedidoVisualizacion(int codigo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetallePropuestaDePedidoVisualizacion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codpro", codigo);
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

	public clsPropuestaDePedido cargaPropuestaDePedido(int codPropuesta)
	{
		clsPropuestaDePedido pro = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaPropuestaDePedido", con.conector);
			cmd.Parameters.AddWithValue("_codPropuesta", codPropuesta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pro = new clsPropuestaDePedido();
					pro.Codigo = dr.GetInt32(0);
					pro.Cod_almacen = dr.GetInt32(1);
					pro.Cod_usuario = dr.GetInt32(2);
					pro.Descripcion = dr.GetString(3);
					pro.Descrip_almacen = dr.GetString(4);
					pro.Estado = dr.GetInt32(5);
					pro.Fechaedicion = dr.GetDateTime(6);
					pro.Fechageneracion = (dr.IsDBNull(7) ? DateTime.MinValue : dr.GetDateTime(7));
					pro.Fecharegitro = dr.GetDateTime(8);
					pro.Nombre_usuario = dr.GetString(9);
					pro.Tipo = dr.GetInt32(10);
					pro.Titulo = dr.GetString(11);
					pro.Eliminado = dr.GetInt32(12);
					pro.CodPlantillaGenerada = ((!dr.IsDBNull(13)) ? dr.GetInt32(13) : 0);
				}
			}
			return pro;
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

	public DataTable cargaDetallePropuestaDePedido(int codpropuesta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetallePropuestaDePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codpro", codpropuesta);
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

	public bool insertaDetallePropuestaPedidoVisualizacion(int codigo_propuesta_creada, List<clsDetallePropuestaDePedido> lista)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			foreach (clsDetallePropuestaDePedido detalle in lista)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedidoVisualizacion", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				detalle.Cod_Propuesta = codigo_propuesta_creada;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				oParam = cmd.Parameters.AddWithValue("_precioUnitActual", detalle.Precio_unit_actual);
				oParam = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				oParam = cmd.Parameters.AddWithValue("_opcionRecuento", detalle.OpcionRecuento);
				if (double.IsNaN(detalle.StockMinimo))
				{
					oParam = cmd.Parameters.AddWithValue("_stockMinimo", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_stockMinimo", detalle.StockMinimo);
				}
				oParam = cmd.Parameters.AddWithValue("_stockMaximo", detalle.StockMaximo);
				oParam = cmd.Parameters.AddWithValue("_unidadXPaquete", detalle.UnidadXPaquete);
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
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
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

	public bool actualizaDetallePropuestaDePedido(List<clsDetallePropuestaDePedido> lista_detalle_prop_insertar, List<clsDetallePropuestaDePedido> lista_detalle_prop_actualizar, List<clsDetallePropuestaDePedido> lista_detalle_prop_cot_selecc, List<clsDetallePropuestaDePedido> lista_detalle_prop_eliminar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_insertar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_eliminar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_insertar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_actualizar, List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_eliminar)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			foreach (clsDetallePropuestaDePedido detalle in lista_detalle_prop_insertar)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				oParam = cmd.Parameters.AddWithValue("_precioUnitActual", detalle.Precio_unit_actual);
				oParam = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				oParam = cmd.Parameters.AddWithValue("_nroItem", detalle.NroItem);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.Codigo <= 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido detalle2 in lista_detalle_prop_actualizar)
			{
				if (detalle2.Codigo > 0)
				{
					cmd = new MySqlCommand("ActualizaDetallePropuestaDePedido", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle2.Codigo);
					oParam2 = cmd.Parameters.AddWithValue("_id_propuesta", detalle2.Cod_Propuesta);
					oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle2.Codigo_Producto);
					oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle2.Ref_Producto);
					oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle2.Descrip_Producto);
					oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle2.Codigo_Unidad);
					oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle2.Descripcion_Unidad);
					oParam2 = cmd.Parameters.AddWithValue("_ctdadReponer", detalle2.Cantidad_reponer);
					oParam2 = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle2.Cantidad_sugerida);
					oParam2 = cmd.Parameters.AddWithValue("_precioUnitActual", detalle2.Precio_unit_actual);
					oParam2 = cmd.Parameters.AddWithValue("_stockActual", detalle2.StockDisponible);
					oParam2 = cmd.Parameters.AddWithValue("_nroItem", detalle2.NroItem);
					if (double.IsNaN(detalle2.Pedido_final))
					{
						oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", null);
					}
					else
					{
						oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", detalle2.Pedido_final);
					}
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion_insertar)
			{
				cmd = new MySqlCommand("GuardaCotizacionPropuestadePedidoOrdenDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam3 = cmd.Parameters.AddWithValue("_id_propuesta_oc", item.CodigoPropuesta);
				oParam3 = cmd.Parameters.AddWithValue("_id_proveedor", item.CodigoProveedor);
				oParam3 = cmd.Parameters.AddWithValue("_descrip_proveedor", item.DescripcionProveedor);
				oParam3 = cmd.Parameters.AddWithValue("_doc_cotizacion", item.DocCotizacion);
				oParam3 = cmd.Parameters.AddWithValue("_codMoneda", item.CodigoMoneda);
				oParam3 = cmd.Parameters.AddWithValue("_tipoPrecio", item.TipoPrecio);
				oParam3 = cmd.Parameters.AddWithValue("newid", 0);
				oParam3.Direction = ParameterDirection.Output;
				int xDP2 = cmd.ExecuteNonQuery();
				item.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (item.Codigo == 0)
				{
					rpta = false;
					break;
				}
				foreach (clsDetalleCotizacionPropuestaDePedido det_cpdp in item.ListadoPrecios)
				{
					if (!rpta)
					{
						break;
					}
					det_cpdp.CodigoCotizacion = item.Codigo;
					cmd = new MySqlCommand("GuardaDetalleCotizacionPropuestaDePedidoCompra", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					oParam3 = new MySqlParameter();
					oParam3 = cmd.Parameters.AddWithValue("_id_detalle_propuesta_oc", det_cpdp.CodigoDetallePropuesta);
					oParam3 = cmd.Parameters.AddWithValue("_id_cotizacion_pdoc", det_cpdp.CodigoCotizacion);
					oParam3 = cmd.Parameters.AddWithValue("_precio_compra_cotizacion", det_cpdp.PrecioCompra);
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsCotizacionPropuestaDePedido item2 in listado_cotizacion_detalle_insertar)
			{
				cmd = new MySqlCommand("ActualizaCotizacionPropuestaDePedidoOrdenDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam4 = cmd.Parameters.AddWithValue("_id_cotizacion", item2.Codigo);
				oParam4 = cmd.Parameters.AddWithValue("_id_propuesta_oc", item2.CodigoPropuesta);
				oParam4 = cmd.Parameters.AddWithValue("_id_proveedor", item2.CodigoProveedor);
				oParam4 = cmd.Parameters.AddWithValue("_descrip_proveedor", item2.DescripcionProveedor);
				oParam4 = cmd.Parameters.AddWithValue("_doc_cotizacion", item2.DocCotizacion);
				oParam4 = cmd.Parameters.AddWithValue("_codMoneda", item2.CodigoMoneda);
				oParam4 = cmd.Parameters.AddWithValue("_tipoPrecio", item2.TipoPrecio);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
				foreach (clsDetalleCotizacionPropuestaDePedido det_cpdp2 in item2.ListadoPrecios)
				{
					if (!rpta)
					{
						break;
					}
					det_cpdp2.CodigoCotizacion = item2.Codigo;
					cmd = new MySqlCommand("GuardaDetalleCotizacionPropuestaDePedidoCompra", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					oParam4 = new MySqlParameter();
					oParam4 = cmd.Parameters.AddWithValue("_id_detalle_propuesta_oc", det_cpdp2.CodigoDetallePropuesta);
					oParam4 = cmd.Parameters.AddWithValue("_id_cotizacion_pdoc", det_cpdp2.CodigoCotizacion);
					oParam4 = cmd.Parameters.AddWithValue("_precio_compra_cotizacion", det_cpdp2.PrecioCompra);
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsCotizacionPropuestaDePedido item3 in listado_cotizacion_detalle_actualizar)
			{
				cmd = new MySqlCommand("ActualizaCotizacionPropuestaDePedidoOrdenDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam5 = cmd.Parameters.AddWithValue("_id_cotizacion", item3.Codigo);
				oParam5 = cmd.Parameters.AddWithValue("_id_propuesta_oc", item3.CodigoPropuesta);
				oParam5 = cmd.Parameters.AddWithValue("_id_proveedor", item3.CodigoProveedor);
				oParam5 = cmd.Parameters.AddWithValue("_descrip_proveedor", item3.DescripcionProveedor);
				oParam5 = cmd.Parameters.AddWithValue("_doc_cotizacion", item3.DocCotizacion);
				oParam5 = cmd.Parameters.AddWithValue("_codMoneda", item3.CodigoMoneda);
				oParam5 = cmd.Parameters.AddWithValue("_tipoPrecio", item3.TipoPrecio);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
				foreach (clsDetalleCotizacionPropuestaDePedido det_cpdp3 in item3.ListadoPrecios)
				{
					if (!rpta)
					{
						break;
					}
					det_cpdp3.CodigoCotizacion = item3.Codigo;
					cmd = new MySqlCommand("ActualizaDetalleCotizacionPropuestaDePedidoOrdenDeCompra", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					oParam5 = new MySqlParameter();
					oParam5 = cmd.Parameters.AddWithValue("_id_detalle_propuesta_oc", det_cpdp3.CodigoDetallePropuesta);
					oParam5 = cmd.Parameters.AddWithValue("_id_cotizacion_pdoc", det_cpdp3.CodigoCotizacion);
					oParam5 = cmd.Parameters.AddWithValue("_precio_compra_cotizacion", det_cpdp3.PrecioCompra);
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsCotizacionPropuestaDePedido item4 in listado_cotizacion_detalle_eliminar)
			{
				if (!rpta)
				{
					break;
				}
				cmd = new MySqlCommand("ActualizaCotizacionPropuestaDePedidoOrdenDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam6 = cmd.Parameters.AddWithValue("_id_cotizacion", item4.Codigo);
				oParam6 = cmd.Parameters.AddWithValue("_id_propuesta_oc", item4.CodigoPropuesta);
				oParam6 = cmd.Parameters.AddWithValue("_id_proveedor", item4.CodigoProveedor);
				oParam6 = cmd.Parameters.AddWithValue("_descrip_proveedor", item4.DescripcionProveedor);
				oParam6 = cmd.Parameters.AddWithValue("_doc_cotizacion", item4.DocCotizacion);
				oParam6 = cmd.Parameters.AddWithValue("_codMoneda", item4.CodigoMoneda);
				oParam6 = cmd.Parameters.AddWithValue("_tipoPrecio", item4.TipoPrecio);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
				foreach (clsDetalleCotizacionPropuestaDePedido det_cpdp4 in item4.ListadoPrecios)
				{
					det_cpdp4.CodigoCotizacion = item4.Codigo;
					cmd = new MySqlCommand("EliminaDetalleCotizacionPropuestaDePedidoCompra", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					oParam6 = new MySqlParameter();
					oParam6 = cmd.Parameters.AddWithValue("_id_detalle_propuesta_oc", det_cpdp4.CodigoDetallePropuesta);
					oParam6 = cmd.Parameters.AddWithValue("_id_cotizacion_pdoc", det_cpdp4.CodigoCotizacion);
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsCotizacionPropuestaDePedido item5 in listado_cotizacion_eliminar)
			{
				cmd = new MySqlCommand("EliminaCotizacionPropuestaDePedidoOrdenDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam7 = cmd.Parameters.AddWithValue("_id_cotizacion", item5.Codigo);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido detalle3 in lista_detalle_prop_eliminar)
			{
				cmd = new MySqlCommand("EliminaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam8 = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle3.Codigo);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido item6 in lista_detalle_prop_cot_selecc)
			{
				if (item6.Cod_cotizacion_seleccionada == 0 && item6.Cod_proveedor_seleccionado != 0)
				{
					List<clsCotizacionPropuestaDePedido> item_cot_ins = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == item6.Cod_proveedor_seleccionado)).ToList();
					if (item_cot_ins.Count <= 0)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						return false;
					}
					item6.Cod_cotizacion_seleccionada = item_cot_ins[0].Codigo;
				}
				cmd = new MySqlCommand("AsignaCotizacionSeleccionadaDetallePropuestaDeCompra", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParamP1 = cmd.Parameters.AddWithValue("_cod_detalle_propuesta", item6.Codigo);
				if (item6.Cod_cotizacion_seleccionada == 0)
				{
					oParamP1 = cmd.Parameters.AddWithValue("_cod_cotizacion", null);
				}
				else
				{
					oParamP1 = cmd.Parameters.AddWithValue("_cod_cotizacion", item6.Cod_cotizacion_seleccionada);
				}
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
			return rpta;
		}
		catch (MySqlException ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			rpta = false;
			throw ex;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool actualizaDetallePropuestaDePedido(List<clsDetallePropuestaDePedido> lista_detalle_prop_insertar, List<clsDetallePropuestaDePedido> lista_detalle_prop_actualizar, List<clsDetallePropuestaDePedido> lista_detalle_prop_cot_selecc, List<clsDetallePropuestaDePedido> lista_detalle_prop_eliminar, List<clsAlmacenPropuestaDePedido> listado_cotizacion_insertar, List<clsAlmacenPropuestaDePedido> listado_cotizacion_eliminar, List<clsAlmacenPropuestaDePedido> listado_cotizacion_actualizar)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			foreach (clsDetallePropuestaDePedido detalle in lista_detalle_prop_insertar)
			{
				cmd = new MySqlCommand("GuardaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("_id_propuesta", detalle.Cod_Propuesta);
				oParam = cmd.Parameters.AddWithValue("_id_producto", detalle.Codigo_Producto);
				oParam = cmd.Parameters.AddWithValue("_referencia", detalle.Ref_Producto);
				oParam = cmd.Parameters.AddWithValue("_descripcion", detalle.Descrip_Producto);
				oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.Codigo_Unidad);
				oParam = cmd.Parameters.AddWithValue("_unidad", detalle.Descripcion_Unidad);
				oParam = cmd.Parameters.AddWithValue("_ctdadReponer", detalle.Cantidad_reponer);
				oParam = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle.Cantidad_sugerida);
				oParam = cmd.Parameters.AddWithValue("_precioUnitActual", detalle.Precio_unit_actual);
				oParam = cmd.Parameters.AddWithValue("_stockActual", detalle.StockDisponible);
				if (double.IsNaN(detalle.Pedido_final))
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", null);
				}
				else
				{
					oParam = cmd.Parameters.AddWithValue("_pedidoFinal", detalle.Pedido_final);
				}
				oParam = cmd.Parameters.AddWithValue("_nroItem", detalle.NroItem);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.Codigo <= 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido detalle2 in lista_detalle_prop_actualizar)
			{
				if (detalle2.Codigo > 0)
				{
					cmd = new MySqlCommand("ActualizaDetallePropuestaDePedido", con.conector);
					cmd.CommandType = CommandType.StoredProcedure;
					MySqlParameter oParam2 = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle2.Codigo);
					oParam2 = cmd.Parameters.AddWithValue("_id_propuesta", detalle2.Cod_Propuesta);
					oParam2 = cmd.Parameters.AddWithValue("_id_producto", detalle2.Codigo_Producto);
					oParam2 = cmd.Parameters.AddWithValue("_referencia", detalle2.Ref_Producto);
					oParam2 = cmd.Parameters.AddWithValue("_descripcion", detalle2.Descrip_Producto);
					oParam2 = cmd.Parameters.AddWithValue("_codUnidad", detalle2.Codigo_Unidad);
					oParam2 = cmd.Parameters.AddWithValue("_unidad", detalle2.Descripcion_Unidad);
					oParam2 = cmd.Parameters.AddWithValue("_ctdadReponer", detalle2.Cantidad_reponer);
					oParam2 = cmd.Parameters.AddWithValue("_ctdadSugerida", detalle2.Cantidad_sugerida);
					oParam2 = cmd.Parameters.AddWithValue("_precioUnitActual", detalle2.Precio_unit_actual);
					oParam2 = cmd.Parameters.AddWithValue("_stockActual", detalle2.StockDisponible);
					oParam2 = cmd.Parameters.AddWithValue("_nroItem", detalle2.NroItem);
					if (double.IsNaN(detalle2.Pedido_final))
					{
						oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", null);
					}
					else
					{
						oParam2 = cmd.Parameters.AddWithValue("_pedidoFinal", detalle2.Pedido_final);
					}
					if (cmd.ExecuteNonQuery() == 0)
					{
						rpta = false;
						break;
					}
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsAlmacenPropuestaDePedido item in listado_cotizacion_insertar)
			{
				cmd = new MySqlCommand("GuardaAlmacenPropuestadePedidoReqAlmacen", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam3 = cmd.Parameters.AddWithValue("_id_propuesta_ra", item.CodigoPropuesta);
				oParam3 = cmd.Parameters.AddWithValue("_id_almacen", item.CodigoAlmacen);
				oParam3 = cmd.Parameters.AddWithValue("_descrip_almacen", item.DescripcionAlmacen);
				oParam3 = cmd.Parameters.AddWithValue("newid", 0);
				oParam3.Direction = ParameterDirection.Output;
				int xDP2 = cmd.ExecuteNonQuery();
				item.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (item.Codigo == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsAlmacenPropuestaDePedido item2 in listado_cotizacion_actualizar)
			{
				cmd = new MySqlCommand("ActualizaAlmacenPropuestaDePedidoReqAlmacen", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam4 = cmd.Parameters.AddWithValue("_id_almacen_pdra", item2.Codigo);
				oParam4 = cmd.Parameters.AddWithValue("_id_propuesta_ra", item2.CodigoPropuesta);
				oParam4 = cmd.Parameters.AddWithValue("_id_almacen", item2.CodigoAlmacen);
				oParam4 = cmd.Parameters.AddWithValue("_descrip_almacen", item2.DescripcionAlmacen);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsAlmacenPropuestaDePedido item3 in listado_cotizacion_eliminar)
			{
				cmd = new MySqlCommand("EliminaAlmacenPropuestaDePedidoReqAlmacen", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam5 = cmd.Parameters.AddWithValue("_id_almacen_pdra", item3.Codigo);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido detalle3 in lista_detalle_prop_eliminar)
			{
				cmd = new MySqlCommand("EliminaDetallePropuestaDePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam6 = cmd.Parameters.AddWithValue("_id_detalle_propuesta", detalle3.Codigo);
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePropuestaDePedido item4 in lista_detalle_prop_cot_selecc)
			{
				if (item4.Cod_cotizacion_seleccionada == 0 && item4.Cod_proveedor_seleccionado != 0)
				{
					List<clsAlmacenPropuestaDePedido> item_cot_ins = Enumerable.Where<clsAlmacenPropuestaDePedido>(listado_cotizacion_insertar.AsEnumerable(), (Func<clsAlmacenPropuestaDePedido, bool>)((clsAlmacenPropuestaDePedido x) => x.CodigoAlmacen == item4.Cod_proveedor_seleccionado)).ToList();
					if (item_cot_ins.Count <= 0)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						return false;
					}
					item4.Cod_cotizacion_seleccionada = item_cot_ins[0].Codigo;
				}
				cmd = new MySqlCommand("AsignaAlmacenSeleccionadoDetallePropuestaDeReqAlmacen", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParamP1 = cmd.Parameters.AddWithValue("_cod_detalle_propuesta", item4.Codigo);
				if (item4.Cod_cotizacion_seleccionada == 0)
				{
					oParamP1 = cmd.Parameters.AddWithValue("_cod_almacen_pdra", null);
				}
				else
				{
					oParamP1 = cmd.Parameters.AddWithValue("_cod_almacen_pdra", item4.Cod_cotizacion_seleccionada);
				}
				if (cmd.ExecuteNonQuery() == 0)
				{
					rpta = false;
					break;
				}
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
			}
			else
			{
				Scope.Complete();
				Scope.Dispose();
			}
			return rpta;
		}
		catch (MySqlException ex)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
			rpta = false;
			throw ex;
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

	public bool insertdetalleproductosagrupados(clsDetallePlantillaDeProductos detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleProductosAgrupos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public bool updateproductosagrupados(clsPlantillaDeProductos producto)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaProductosAgrupos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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

	public bool updatedetalleproductosagrupados(clsDetallePlantillaDeProductos detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleProductosAgrupos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
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
}
