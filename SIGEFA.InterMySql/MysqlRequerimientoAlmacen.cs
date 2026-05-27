using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Formularios;

namespace SIGEFA.InterMySql;

internal class MysqlRequerimientoAlmacen
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsRequerimientoAlmacen ReqAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codTipoDocumento", ReqAlmacen.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("_numDocumento", ReqAlmacen.NumDocumento);
			oParam = cmd.Parameters.AddWithValue("_codSerie", ReqAlmacen.CodSerie);
			oParam = cmd.Parameters.AddWithValue("_numSerie", ReqAlmacen.NumSerie);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenRegistro", ReqAlmacen.CodAlmacenRegistro);
			oParam = cmd.Parameters.AddWithValue("_codUsuarioRegistro", ReqAlmacen.CodUserRegistro);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", ReqAlmacen.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_codUsuarioModifico", null);
			oParam = cmd.Parameters.AddWithValue("_fechaModifico", null);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenSolicitante", ReqAlmacen.CodAlmacenSolicitante);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenDespacho", ReqAlmacen.CodAlmacenDespacho);
			oParam = cmd.Parameters.AddWithValue("_fechaRequerimiento", ReqAlmacen.FechaRequerimiento);
			oParam = cmd.Parameters.AddWithValue("_codUsuarioAnulacion", null);
			oParam = cmd.Parameters.AddWithValue("_fechaAnulacion", null);
			oParam = cmd.Parameters.AddWithValue("_estado", ReqAlmacen.IEstado);
			oParam = cmd.Parameters.AddWithValue("_comentarioSolicitante", ReqAlmacen.ComentarioSolicitante);
			oParam = cmd.Parameters.AddWithValue("_comentarioDespacho", ReqAlmacen.ComentarioDespacho);
			oParam = cmd.Parameters.AddWithValue("_tipoReq", ReqAlmacen.Tipo);
			if (ReqAlmacen.CodPropuestaDePedido == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codPropuestaPedido", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codPropuestaPedido", ReqAlmacen.CodPropuestaDePedido);
			}
			if (ReqAlmacen.CodPedidoVenta == "")
			{
				oParam = cmd.Parameters.AddWithValue("_codPedidoVenta", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codPedidoVenta", ReqAlmacen.CodPedidoVenta);
			}
			oParam = cmd.Parameters.AddWithValue("_NombreContacto", ReqAlmacen.NombreContacto);
			oParam = cmd.Parameters.AddWithValue("_TelefonoContacto", ReqAlmacen.TelefonoContacto);
			oParam = cmd.Parameters.AddWithValue("_Delivery", ReqAlmacen.Delivery);
			oParam = cmd.Parameters.AddWithValue("_DireccionDelivery", ReqAlmacen.DireccionDelivery);
			oParam = cmd.Parameters.AddWithValue("_AutorizadoPor", ReqAlmacen.AutorizadoPor);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			ReqAlmacen.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	internal DataSet ListadoConsumoRequerimiento(int tipoFecha, DateTime desde, DateTime hasta)
	{
		try
		{
			DataSet data = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("listadoConsumoRequerimiento", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_desde", desde);
			cmd.Parameters.AddWithValue("_hasta", hasta);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(data);
			return data;
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

	internal bool deletedetalleRequerimiento(int codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaDetalleRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codRequerimiento", codigo);
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

	internal DataTable CargaRequerimientosSegunPedido(int codPedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoRequerimientoAlmacenSegunPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codPedido", codPedido);
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

	public bool update(clsRequerimientoAlmacen ReqAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlmacen", ReqAlmacen.Codigo);
			cmd.Parameters.AddWithValue("_codTipoDocumento", ReqAlmacen.CodTipoDocumento);
			cmd.Parameters.AddWithValue("_numDocumento", ReqAlmacen.NumDocumento);
			cmd.Parameters.AddWithValue("_codSerie", ReqAlmacen.CodSerie);
			cmd.Parameters.AddWithValue("_numSerie", ReqAlmacen.NumSerie);
			cmd.Parameters.AddWithValue("_codAlmacenRegistro", ReqAlmacen.CodAlmacenRegistro);
			cmd.Parameters.AddWithValue("_codUsuarioRegistro", ReqAlmacen.CodUserRegistro);
			cmd.Parameters.AddWithValue("_fechaRegistro", ReqAlmacen.FechaRegistro);
			if (ReqAlmacen.CodUserModifico > 0)
			{
				cmd.Parameters.AddWithValue("_codUsuarioModifico", ReqAlmacen.CodUserModifico);
				cmd.Parameters.AddWithValue("_fechaModifico", ReqAlmacen.FechaModifico);
			}
			else
			{
				cmd.Parameters.AddWithValue("_codUsuarioModifico", null);
				cmd.Parameters.AddWithValue("_fechaModifico", null);
			}
			cmd.Parameters.AddWithValue("_codAlmacenSolicitante", ReqAlmacen.CodAlmacenSolicitante);
			cmd.Parameters.AddWithValue("_codAlmacenDespacho", ReqAlmacen.CodAlmacenDespacho);
			cmd.Parameters.AddWithValue("_fechaRequerimiento", ReqAlmacen.FechaRequerimiento);
			if (ReqAlmacen.CodUserAnulacion > 0)
			{
				cmd.Parameters.AddWithValue("_fechaAnulacion", ReqAlmacen.FechaAnulacion);
				cmd.Parameters.AddWithValue("_codUsuarioAnulacion", ReqAlmacen.CodUserAnulacion);
			}
			else
			{
				cmd.Parameters.AddWithValue("_fechaAnulacion", null);
				cmd.Parameters.AddWithValue("_codUsuarioAnulacion", null);
			}
			cmd.Parameters.AddWithValue("_estado", ReqAlmacen.IEstado);
			cmd.Parameters.AddWithValue("_comentarioSolicitante", ReqAlmacen.ComentarioSolicitante);
			cmd.Parameters.AddWithValue("_comentarioDespacho", ReqAlmacen.ComentarioDespacho);
			cmd.Parameters.AddWithValue("_tipoReq", ReqAlmacen.Tipo);
			cmd.Parameters.AddWithValue("_Delivery", ReqAlmacen.Delivery);
			cmd.Parameters.AddWithValue("_DireccionDelivery", ReqAlmacen.DireccionDelivery);
			cmd.Parameters.AddWithValue("_AutorizadoPor", ReqAlmacen.AutorizadoPor);
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

	internal DataTable ListadoTransferenciasGeneradas(int codigoReq, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarTransferenciasGeneradasReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codReqAlmacen", codigoReq);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public bool anular(int CodigoRequerimiento, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlm", CodigoRequerimiento);
			cmd.Parameters.AddWithValue("_codUser", codUsuario);
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

	internal DataTable ListaTransferenciasAprobadas(int codReq)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarCodTransferenciasAprobadasDeReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlmacen", codReq);
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

	public bool aprobar(int CodigoRequerimiento, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AprobarRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlm", CodigoRequerimiento);
			cmd.Parameters.AddWithValue("_codUser", codUsuario);
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

	internal bool registrarTransferencia(int codigoRequerimiento, int codTransferencia, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("RegistrarTransferenciaRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlm", codigoRequerimiento);
			cmd.Parameters.AddWithValue("_codTrans", codTransferencia);
			cmd.Parameters.AddWithValue("_codUser", codUsuario);
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

	internal bool actualizaEstadoReqAlmacen(int codigo, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEstadoReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codra", codigo);
			cmd.Parameters.AddWithValue("_estado", estado);
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

	internal bool separarStock(int codAlmacenDespacho, int codProducto, int codUnidad, decimal cantidadConfirmada, int codDetalleReqAlm)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SeparandoStockAlAprobarReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_cod_almacen", codAlmacenDespacho);
			cmd.Parameters.AddWithValue("_cod_producto", codProducto);
			cmd.Parameters.AddWithValue("_cod_unidad", codUnidad);
			cmd.Parameters.AddWithValue("_cantidad_confirmada", cantidadConfirmada);
			cmd.Parameters.AddWithValue("_codDetalleReqAlm", codDetalleReqAlm);
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

	internal bool ModificarCtdadPendienteAprobadaDeDetalleReqAlmacen(int operacion, double cantidad, int codigoDetalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ModCtdadPendAprobDetalleReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_operacion", operacion);
			cmd.Parameters.AddWithValue("_cantidad", cantidad);
			cmd.Parameters.AddWithValue("_codigoDetalle", codigoDetalle);
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

	internal bool asignarFacturaVenta(int codReqAlm, int codVentaFV)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SetFacturaVentaEnRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlm", codReqAlm);
			cmd.Parameters.AddWithValue("_codFacturaVenta", codVentaFV);
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

	internal clsRequerimientoAlmacen CargaRequerimientosSegun(int codFacturaVenta)
	{
		clsRequerimientoAlmacen req_alm = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaRequerimientoTipoVentaSegunCodFacturaVenta", con.conector);
			cmd.Parameters.AddWithValue("_codFacturaVenta", codFacturaVenta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					req_alm = new clsRequerimientoAlmacen();
					req_alm.Codigo = dr.GetInt32(0);
					req_alm.CodTipoDocumento = dr.GetInt32(1);
					req_alm.NumDocumento = dr.GetString(2);
					req_alm.CodSerie = dr.GetInt32(3);
					req_alm.NumSerie = dr.GetString(4);
					req_alm.CodAlmacenRegistro = dr.GetInt32(5);
					req_alm.AlmacenRegistro = dr.GetString(6);
					req_alm.CodUserRegistro = dr.GetInt32(7);
					req_alm.UserRegistro = dr.GetString(8);
					req_alm.FechaRegistro = dr.GetDateTime(9);
					req_alm.CodUserModifico = ((!dr.IsDBNull(10)) ? dr.GetInt32(10) : 0);
					req_alm.UserModifico = (dr.IsDBNull(11) ? "" : dr.GetString(11));
					req_alm.FechaModifico = (dr.IsDBNull(12) ? DateTime.MinValue : dr.GetDateTime(12));
					req_alm.CodAlmacenSolicitante = dr.GetInt32(13);
					req_alm.AlmacenSolicitante = dr.GetString(14);
					req_alm.CodAlmacenDespacho = dr.GetInt32(15);
					req_alm.AlmacenDespacho = dr.GetString(16);
					req_alm.FechaRequerimiento = dr.GetDateTime(17);
					req_alm.CodUserAnulacion = ((!dr.IsDBNull(18)) ? dr.GetInt32(18) : 0);
					req_alm.UserAnulacion = (dr.IsDBNull(19) ? "" : dr.GetString(19));
					req_alm.FechaAnulacion = (dr.IsDBNull(20) ? DateTime.MinValue : dr.GetDateTime(20));
					req_alm.IEstado = dr.GetInt32(21);
					req_alm.SEstado = dr.GetString(22);
					req_alm.ComentarioSolicitante = dr.GetString(23);
					req_alm.ComentarioDespacho = (dr.IsDBNull(24) ? "" : dr.GetString(24));
					req_alm.Tipo = dr.GetInt32(25);
					req_alm.CodPropuestaDePedido = ((!dr.IsDBNull(26)) ? dr.GetInt32(26) : 0);
					req_alm.NombreContacto = dr.GetString(27);
					req_alm.TelefonoContacto = dr.GetString(28);
				}
			}
			return req_alm;
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

	internal bool cerrar(int codigoRequerimiento, int codUsuario)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CerrarRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlm", codigoRequerimiento);
			cmd.Parameters.AddWithValue("_codUser", codUsuario);
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

	internal double getCantidadProductoTransferenciasActivas(int codigoReq, int codProducto)
	{
		double cantidad = double.NaN;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("getCantidadProductoTransferenciasActivas", con.conector);
			cmd.Parameters.AddWithValue("_codReqAlm", codigoReq);
			cmd.Parameters.AddWithValue("_codProducto", codProducto);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					cantidad = dr.GetDouble(0);
				}
			}
			return cantidad;
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

	internal bool ModificarCantidadConfirmada(int idDetalle, double nuevaCtdadConfirmacion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadConfirmadaDetalleReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_id_det_ra", idDetalle);
			cmd.Parameters.AddWithValue("_cantidad", nuevaCtdadConfirmacion);
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

	internal bool actualizaCantidadPendienteAprobadaReqAlmacen(int codReqAlm, bool movimientostock)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteAprobadaReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codRA", codReqAlm);
			cmd.Parameters.AddWithValue("_bandMovimientoStock", movimientostock);
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

	internal bool asignarAutorizador(int CodigoRequerimiento, int idautorizador)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SetAutorizadorEnRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlm", CodigoRequerimiento);
			cmd.Parameters.AddWithValue("_idautorizador", idautorizador);
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

	internal clsRequerimientoAlmacen CargaRequerimientosSegun(int codPedidoVenta, int codAlmacenVenta, int estadoReq)
	{
		clsRequerimientoAlmacen req_alm = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaRequerimientoTipoVentaSegun", con.conector);
			cmd.Parameters.AddWithValue("_codPedidoVenta", codPedidoVenta);
			cmd.Parameters.AddWithValue("_codAlmacenVenta", codAlmacenVenta);
			cmd.Parameters.AddWithValue("_estado", estadoReq);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					req_alm = new clsRequerimientoAlmacen();
					req_alm.Codigo = dr.GetInt32(0);
					req_alm.CodTipoDocumento = dr.GetInt32(1);
					req_alm.NumDocumento = dr.GetString(2);
					req_alm.CodSerie = dr.GetInt32(3);
					req_alm.NumSerie = dr.GetString(4);
					req_alm.CodAlmacenRegistro = dr.GetInt32(5);
					req_alm.AlmacenRegistro = dr.GetString(6);
					req_alm.CodUserRegistro = dr.GetInt32(7);
					req_alm.UserRegistro = dr.GetString(8);
					req_alm.FechaRegistro = dr.GetDateTime(9);
					req_alm.CodUserModifico = ((!dr.IsDBNull(10)) ? dr.GetInt32(10) : 0);
					req_alm.UserModifico = (dr.IsDBNull(11) ? "" : dr.GetString(11));
					req_alm.FechaModifico = (dr.IsDBNull(12) ? DateTime.MinValue : dr.GetDateTime(12));
					req_alm.CodAlmacenSolicitante = dr.GetInt32(13);
					req_alm.AlmacenSolicitante = dr.GetString(14);
					req_alm.CodAlmacenDespacho = dr.GetInt32(15);
					req_alm.AlmacenDespacho = dr.GetString(16);
					req_alm.FechaRequerimiento = dr.GetDateTime(17);
					req_alm.CodUserAnulacion = ((!dr.IsDBNull(18)) ? dr.GetInt32(18) : 0);
					req_alm.UserAnulacion = (dr.IsDBNull(19) ? "" : dr.GetString(19));
					req_alm.FechaAnulacion = (dr.IsDBNull(20) ? DateTime.MinValue : dr.GetDateTime(20));
					req_alm.IEstado = dr.GetInt32(21);
					req_alm.SEstado = dr.GetString(22);
					req_alm.ComentarioSolicitante = dr.GetString(23);
					req_alm.ComentarioDespacho = (dr.IsDBNull(24) ? "" : dr.GetString(24));
					req_alm.Tipo = dr.GetInt32(25);
					req_alm.CodPropuestaDePedido = ((!dr.IsDBNull(26)) ? dr.GetInt32(26) : 0);
					req_alm.NombreContacto = dr.GetString(27);
					req_alm.TelefonoContacto = dr.GetString(28);
					req_alm.AutorizadoPor = (dr.IsDBNull(29) ? "" : dr.GetString(29));
					req_alm.DireccionDelivery = (dr.IsDBNull(30) ? "" : dr.GetString(30));
				}
			}
			return req_alm;
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

	internal DataTable ListaTransferenciasPendientes(int codReq)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarCodTransferenciasPendientesDeReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlmacen", codReq);
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

	internal DataSet reporteImprimirDatosRequerimiento(int codRequerimiento)
	{
		try
		{
			DataSet set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ExportacionDataReqAlmacenDetalle", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("_codEmpresa", frmLogin.iCodEmpresa);
			cmd.Parameters.AddWithValue("_codReqAlmacen", codRequerimiento);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_req_alm");
			set.WriteXml("C:\\XML\\ReporteRequerimientoAlmacen.xml", XmlWriteMode.WriteSchema);
			return set;
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

	internal bool retornarStock(int codAlmacenDespacho, int codProducto, int codUnidad, decimal cantidadPendienteAprobada, bool modificaStockActual, int codDetalleReqAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("RetornandoStockAlAnularReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_cod_almacen", codAlmacenDespacho);
			cmd.Parameters.AddWithValue("_cod_producto", codProducto);
			cmd.Parameters.AddWithValue("_cod_unidad", codUnidad);
			cmd.Parameters.AddWithValue("_cantidad_pend_aprob", cantidadPendienteAprobada);
			cmd.Parameters.AddWithValue("_bandSetStockActual", modificaStockActual ? 1 : 0);
			cmd.Parameters.AddWithValue("_codDetalleReqAlmacen", codDetalleReqAlmacen);
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

	internal bool actualizaCantidadPendienteReqAlmacen(int codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteReqAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codRA", codigo);
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

	internal DataTable generarDatosParaFormularioIntermedioTransferencia(int codReqAlm)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GenerarDatosParaIntermedioTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlmacen", codReqAlm);
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

	public bool insertdetalle(clsDetalleRequerimientoAlmacen Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam;
			if (Detalle.Codigo != 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codDetalleReqAlmacen", Detalle.Codigo);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codDetalleReqAlmacen", null);
			}
			oParam = cmd.Parameters.AddWithValue("_codReqAlmacen", Detalle.CodRequerimiento);
			oParam = cmd.Parameters.AddWithValue("_codProducto", Detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("_codUnidad", Detalle.CodUnidad);
			oParam = cmd.Parameters.AddWithValue("_cantidad", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("_cantidadPedida", Detalle.CantidadPedida);
			oParam = cmd.Parameters.AddWithValue("_cantidadPendiente", Detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("_cantidadConfirmada", Detalle.CantidadConfirmada);
			oParam = cmd.Parameters.AddWithValue("_cantidadPendienteAprobada", Detalle.CantidadPendienteAprobada);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			if (Detalle.Codigo == 0)
			{
				Detalle.Codigo = Convert.ToInt32(cmd.Parameters["newid"].Value);
			}
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

	public bool updatedetalleRequerimiento(clsDetalleRequerimientoAlmacen Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDetalleRequerim", Detalle.Codigo);
			cmd.Parameters.AddWithValue("_codReqAlmacen", Detalle.CodRequerimiento);
			cmd.Parameters.AddWithValue("_codProducto", Detalle.CodProducto);
			cmd.Parameters.AddWithValue("_codUnidad", Detalle.CodUnidad);
			cmd.Parameters.AddWithValue("_cantidad", Detalle.Cantidad);
			cmd.Parameters.AddWithValue("_cantidadPedida", Detalle.CantidadPedida);
			cmd.Parameters.AddWithValue("_cantidadPendiente", Detalle.CantidadPendiente);
			cmd.Parameters.AddWithValue("_cantidadConfirmada", Detalle.CantidadConfirmada);
			cmd.Parameters.AddWithValue("_cantidadPendienteAprobada", Detalle.CantidadPendienteAprobada);
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

	public bool deletedetalle(int CodigoDetalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDetalle", CodigoDetalle);
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

	public clsRequerimientoAlmacen CargaRequerimiento(int codRequerimiento)
	{
		clsRequerimientoAlmacen req_alm = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaRequerimientoAlmacen", con.conector);
			cmd.Parameters.AddWithValue("_codReqAlmacen", codRequerimiento);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					req_alm = new clsRequerimientoAlmacen();
					req_alm.Codigo = dr.GetInt32(0);
					req_alm.CodTipoDocumento = dr.GetInt32(1);
					req_alm.NumDocumento = dr.GetString(2);
					req_alm.CodSerie = dr.GetInt32(3);
					req_alm.NumSerie = dr.GetString(4);
					req_alm.CodAlmacenRegistro = dr.GetInt32(5);
					req_alm.AlmacenRegistro = dr.GetString(6);
					req_alm.CodUserRegistro = dr.GetInt32(7);
					req_alm.UserRegistro = dr.GetString(8);
					req_alm.FechaRegistro = dr.GetDateTime(9);
					req_alm.CodUserModifico = ((!dr.IsDBNull(10)) ? dr.GetInt32(10) : 0);
					req_alm.UserModifico = (dr.IsDBNull(11) ? "" : dr.GetString(11));
					req_alm.FechaModifico = (dr.IsDBNull(12) ? DateTime.MinValue : dr.GetDateTime(12));
					req_alm.CodAlmacenSolicitante = dr.GetInt32(13);
					req_alm.AlmacenSolicitante = dr.GetString(14);
					req_alm.CodAlmacenDespacho = dr.GetInt32(15);
					req_alm.AlmacenDespacho = dr.GetString(16);
					req_alm.FechaRequerimiento = dr.GetDateTime(17);
					req_alm.CodUserAnulacion = ((!dr.IsDBNull(18)) ? dr.GetInt32(18) : 0);
					req_alm.UserAnulacion = (dr.IsDBNull(19) ? "" : dr.GetString(19));
					req_alm.FechaAnulacion = (dr.IsDBNull(20) ? DateTime.MinValue : dr.GetDateTime(20));
					req_alm.IEstado = dr.GetInt32(21);
					req_alm.SEstado = dr.GetString(22);
					req_alm.ComentarioSolicitante = dr.GetString(23);
					req_alm.ComentarioDespacho = (dr.IsDBNull(24) ? "" : dr.GetString(24));
					req_alm.Tipo = dr.GetInt32(25);
					req_alm.CodPropuestaDePedido = ((!dr.IsDBNull(26)) ? dr.GetInt32(26) : 0);
					req_alm.NombreContacto = (dr.IsDBNull(27) ? "" : dr.GetString(27));
					req_alm.TelefonoContacto = (dr.IsDBNull(28) ? "" : dr.GetString(28));
					req_alm.Delivery = ((!dr.IsDBNull(29)) ? dr.GetInt32(29) : 0);
					req_alm.DireccionDelivery = (dr.IsDBNull(30) ? "" : dr.GetString(30));
					req_alm.AutorizadoPor = (dr.IsDBNull(31) ? "" : dr.GetString(31));
					req_alm.CodFacturaVenta = ((!dr.IsDBNull(32)) ? dr.GetInt32(32) : 0);
					req_alm.TituloFacturaVenta = (dr.IsDBNull(33) ? "" : dr.GetString(33));
					req_alm.CodUserAprobador = ((!dr.IsDBNull(34)) ? dr.GetInt32(34) : 0);
					req_alm.UserAprobador = (dr.IsDBNull(35) ? "" : dr.GetString(35));
					req_alm.FechaAprobador = (dr.IsDBNull(36) ? DateTime.MinValue : dr.GetDateTime(36));
				}
			}
			return req_alm;
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

	public List<clsDetalleRequerimientoAlmacen> CargaDetalleRequerimientoAlmacen(int codRequerimiento)
	{
		List<clsDetalleRequerimientoAlmacen> listado = new List<clsDetalleRequerimientoAlmacen>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CargaDetalleRequerimientoAlmacen", con.conector);
			cmd.Parameters.AddWithValue("_codReqAlmacen", codRequerimiento);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsDetalleRequerimientoAlmacen det_req_alm = new clsDetalleRequerimientoAlmacen();
					det_req_alm.Codigo = dr.GetInt32(0);
					det_req_alm.CodRequerimiento = dr.GetInt32(1);
					det_req_alm.CodProducto = dr.GetInt32(2);
					det_req_alm.RefProducto = dr.GetString(3);
					det_req_alm.DescripProducto = dr.GetString(4);
					det_req_alm.CodUnidad = dr.GetInt32(5);
					det_req_alm.DescripUnidad = dr.GetString(6);
					det_req_alm.Cantidad = dr.GetDecimal(7);
					det_req_alm.CantidadPedida = dr.GetDecimal(8);
					det_req_alm.CantidadConfirmada = dr.GetDecimal(9);
					det_req_alm.CantidadPendiente = dr.GetDecimal(10);
					det_req_alm.CantidadPendienteAprobada = dr.GetDecimal(11);
					listado.Add(det_req_alm);
				}
			}
			return listado;
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

	public DataTable ListaDetalleRequerimientoAlmacen(int codRequerimiento)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoDetalleRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codReqAlmacen", codRequerimiento);
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

	public DataTable ListadoRequerimientoAlmacen(int CodAlmacen, int CodSucursal, int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int codProducto, int tipoListado)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codAlmacen", CodAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", CodSucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("codProducto", codProducto);
			cmd.Parameters.AddWithValue("_tipoListado", tipoListado);
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

	public DataTable ListadoParaControlDeRequerimiento(int CodAlmacen, int CodSucursal, int tipoFecha, DateTime FechaInicial, DateTime FechaFinal, int codProducto, int tipoEstado)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoControlRequerimientoAlmacen", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codAlmacen", CodAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", CodSucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
			cmd.Parameters.AddWithValue("codProducto", codProducto);
			cmd.Parameters.AddWithValue("_tipoEstado", tipoEstado);
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
