using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Formularios;

namespace SIGEFA.InterMySql;

internal class MysqlDespacho
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private MySqlTransaction tra = null;

	private DataTable tabla = null;

	public bool insert(clsDespacho despacho)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codCliente", despacho.CodCliente);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenRegistro", despacho.CodAlmacenRegistro);
			oParam = cmd.Parameters.AddWithValue("_fechaDespacho", despacho.FechaDespacho);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", despacho.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_codTablaDocRelacionado", despacho.CodTablaDocRelacionada);
			oParam = cmd.Parameters.AddWithValue("_codDocRelacionado", despacho.CodDocRelacionado);
			if (despacho.codReqAlmRelacionado == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codReqAlmRelacionado", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codReqAlmRelacionado", despacho.codReqAlmRelacionado);
			}
			despacho.Estado = 1;
			oParam = cmd.Parameters.AddWithValue("_NombreContacto", despacho.NombreContacto);
			oParam = cmd.Parameters.AddWithValue("_TelefonoContacto", despacho.TelefonoContacto);
			oParam = cmd.Parameters.AddWithValue("_CodUserRegistro", despacho.CodUserRegistro);
			oParam = cmd.Parameters.AddWithValue("_DireccionDelivery", despacho.DireccionDelivery);
			oParam = cmd.Parameters.AddWithValue("_codSerie", despacho.CodSerie);
			oParam = cmd.Parameters.AddWithValue("_serie", despacho.Serie);
			oParam = cmd.Parameters.AddWithValue("_numeracion", despacho.Numeracion);
			oParam = cmd.Parameters.AddWithValue("_comentario", despacho.Comentario);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			despacho.CodDespacho = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal DataSet ExportarListadoReporteEntrega(DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen)
	{
		try
		{
			DataSet set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ExportacionListadoReporteEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("_codEmpresa", frmLogin.iCodEmpresa);
			cmd.Parameters.AddWithValue("_desde", desde);
			cmd.Parameters.AddWithValue("_hasta", hasta);
			cmd.Parameters.AddWithValue("_codAnalisis", codAnalisis);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_reporte_listado_entrega");
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

	internal DataTable ListadoReporteEntrega(DateTime desde, DateTime hasta, string codAnalisis, string codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoReporteEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_desde", desde);
			cmd.Parameters.AddWithValue("_hasta", hasta);
			cmd.Parameters.AddWithValue("_codAnalisis", codAnalisis);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
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

	public bool insertDetalle(clsDetalleDespacho detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codDespacho", detalle.CodDespacho);
			oParam = cmd.Parameters.AddWithValue("_codProducto", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.CodUnidad);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("_cantidadPendiente", detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenEntregar", detalle.CodAlmacenEntregar);
			oParam = cmd.Parameters.AddWithValue("_estado", detalle.Estado);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleDespacho = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal bool update(clsDespacho despacho)
	{
		try
		{
			return true;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal bool updateDetalle(clsDetalleDespacho detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codDetalle", detalle.CodDetalleDespacho);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("_cantidadPendiente", detalle.CantidadPendiente);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenEntregar", detalle.CodAlmacenEntregar);
			oParam = cmd.Parameters.AddWithValue("_estado", detalle.Estado);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal bool anularEntrega(int codEntrega, int CodUsuario, DateTime Fecha)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codEntrega", codEntrega);
			oParam = cmd.Parameters.AddWithValue("_codUsuario", CodUsuario);
			oParam = cmd.Parameters.AddWithValue("_fecha", Fecha);
			if (cmd.ExecuteNonQuery() != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal DataTable listaDetalleEntrega(int codEntrega)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarDetalleEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codEntrega", codEntrega);
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

	internal clsEntrega CargaEntrega(int codEntrega)
	{
		clsEntrega entrega = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraEntregaNueva", con.conector);
			cmd.Parameters.AddWithValue("_codEntrega", codEntrega);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					entrega = new clsEntrega();
					entrega.CodEntrega = dr.GetInt32(0);
					entrega.CodDespacho = dr.GetInt32(1);
					entrega.CodUsuario = dr.GetInt32(2);
					entrega.Estado = dr.GetInt32(3);
					entrega.Anulado = dr.GetInt32(4);
					entrega.CodAlmacenRegistro = dr.GetInt32(5);
					entrega.NombreUsuario = dr.GetString(6);
					entrega.NombreCliente = dr.GetString(7);
					entrega.FechaEntrega = dr.GetDateTime(8);
					entrega.FechaRegistro = dr.GetDateTime(9);
					entrega.CodUsuarioAnulado = ((!dr.IsDBNull(10)) ? dr.GetInt32(10) : 0);
					entrega.NombreUsuarioAnulacion = (dr.IsDBNull(11) ? "" : dr.GetString(11));
					entrega.FechaAnulacion = (dr.IsDBNull(12) ? DateTime.MinValue : dr.GetDateTime(12));
					entrega.CodSerie = ((!dr.IsDBNull(13)) ? dr.GetInt32(13) : 0);
					entrega.Serie = (dr.IsDBNull(14) ? "" : dr.GetString(14));
					entrega.Numeracion = (dr.IsDBNull(15) ? "" : dr.GetString(15));
				}
			}
			return entrega;
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

	internal int obtenerCodEstado(int codDespacho)
	{
		int rpta = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ObtenerCodigoEstadoDespacho", con.conector);
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rpta = dr.GetInt32(0);
				}
			}
			return rpta;
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

	internal DataTable listaEntregas(int codDespacho)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListarEntregasDeDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
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

	public clsDespacho CargaDespacho(int codDespacho)
	{
		clsDespacho despacho = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDespacho", con.conector);
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					despacho = new clsDespacho();
					despacho.CodDespacho = codDespacho;
					despacho.CodCliente = dr.GetInt32(0);
					despacho.RazonSocial = dr.GetString(1);
					despacho.CodAlmacenRegistro = dr.GetInt32(2);
					despacho.TituloAlmacenRegistro = dr.GetString(3);
					despacho.FechaDespacho = dr.GetDateTime(4);
					despacho.FechaRegistro = dr.GetDateTime(5);
					despacho.CodTablaDocRelacionada = dr.GetInt32(6);
					despacho.CodDocRelacionado = dr.GetInt32(7);
					despacho.TituloDocRelacionado = dr.GetString(8);
					despacho.NombreCliente = dr.GetString(9);
					despacho.RucDni = dr.GetString(10);
					despacho.codReqAlmRelacionado = ((!dr.IsDBNull(11)) ? dr.GetInt32(11) : 0);
					despacho.NombreContacto = (dr.IsDBNull(12) ? "" : dr.GetString(12));
					despacho.TelefonoContacto = (dr.IsDBNull(13) ? "" : dr.GetString(13));
					despacho.TituloReqAlmacen = (dr.IsDBNull(14) ? "" : dr.GetString(14));
					despacho.CodEstado = ((!dr.IsDBNull(15)) ? dr.GetInt32(15) : 0);
					despacho.DescripEstado = (dr.IsDBNull(16) ? "" : dr.GetString(16));
					despacho.Estado = ((!dr.IsDBNull(17)) ? dr.GetInt32(17) : 0);
					despacho.Anulado = ((!dr.IsDBNull(18)) ? dr.GetInt32(18) : 0);
					despacho.DescripNotaCredito = (dr.IsDBNull(19) ? "" : dr.GetString(19));
					despacho.CodUserRegistro = ((!dr.IsDBNull(20)) ? dr.GetInt32(20) : 0);
					despacho.UserRegistro = (dr.IsDBNull(21) ? "" : dr.GetString(21));
					despacho.DireccionDelivery = (dr.IsDBNull(22) ? "" : dr.GetString(22));
					despacho.CodNotaCredito = (dr.IsDBNull(23) ? "" : dr.GetString(23));
					despacho.CodNotaIngresoNC = (dr.IsDBNull(24) ? "" : dr.GetString(24));
					despacho.CodSerie = ((!dr.IsDBNull(25)) ? dr.GetInt32(25) : 0);
					despacho.Serie = (dr.IsDBNull(26) ? "" : dr.GetString(26));
					despacho.Numeracion = (dr.IsDBNull(27) ? "" : dr.GetString(27));
					despacho.Comentario = (dr.IsDBNull(28) ? "" : dr.GetString(28));
				}
			}
			return despacho;
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

	public DataTable VerificaRequerimientoAnularVenta(int codalmacen, int codfacturaventa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("VerificaRequerimientoEnDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalmacen", codalmacen);
			cmd.Parameters.AddWithValue("codfacturaventa", codfacturaventa);
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

	internal bool actualizaCantidadPendienteDespacho(int codDespacho)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
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

	internal DataTable generarDatosParaFormularioIntermedio(int codDespacho, int codAlmacen, int codSucursal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDatosParaGenerarEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
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

	internal clsDespacho CargaDespachoSegunDocRelacionado(int tipoDocRelacionado, string codDocRelacionado)
	{
		clsDespacho despacho = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDespachoSegunDocRelacionado", con.conector);
			cmd.Parameters.AddWithValue("_tipoDocRelacionado", tipoDocRelacionado);
			cmd.Parameters.AddWithValue("_codDocRelacionado", codDocRelacionado);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					despacho = new clsDespacho();
					despacho.CodDespacho = dr.GetInt32(0);
					despacho.CodCliente = dr.GetInt32(1);
					despacho.RazonSocial = dr.GetString(2);
					despacho.CodAlmacenRegistro = dr.GetInt32(3);
					despacho.TituloAlmacenRegistro = dr.GetString(4);
					despacho.FechaDespacho = dr.GetDateTime(5);
					despacho.FechaRegistro = dr.GetDateTime(6);
					despacho.CodTablaDocRelacionada = dr.GetInt32(7);
					despacho.CodDocRelacionado = dr.GetInt32(8);
					despacho.TituloDocRelacionado = dr.GetString(9);
					despacho.NombreCliente = dr.GetString(10);
					despacho.RucDni = dr.GetString(11);
					despacho.codReqAlmRelacionado = ((!dr.IsDBNull(12)) ? dr.GetInt32(12) : 0);
					despacho.NombreContacto = (dr.IsDBNull(13) ? "" : dr.GetString(13));
					despacho.TelefonoContacto = (dr.IsDBNull(14) ? "" : dr.GetString(14));
					despacho.TituloReqAlmacen = (dr.IsDBNull(15) ? "" : dr.GetString(15));
					despacho.CodEstado = ((!dr.IsDBNull(16)) ? dr.GetInt32(16) : 0);
					despacho.DescripEstado = (dr.IsDBNull(17) ? "" : dr.GetString(17));
					despacho.Estado = ((!dr.IsDBNull(18)) ? dr.GetInt32(18) : 0);
					despacho.Anulado = ((!dr.IsDBNull(19)) ? dr.GetInt32(19) : 0);
					despacho.DescripNotaCredito = (dr.IsDBNull(20) ? "" : dr.GetString(20));
					despacho.CodUserRegistro = ((!dr.IsDBNull(21)) ? dr.GetInt32(21) : 0);
					despacho.UserRegistro = (dr.IsDBNull(22) ? "" : dr.GetString(22));
					despacho.DireccionDelivery = (dr.IsDBNull(23) ? "" : dr.GetString(23));
					despacho.CodSerie = ((!dr.IsDBNull(24)) ? dr.GetInt32(24) : 0);
					despacho.Serie = (dr.IsDBNull(25) ? "" : dr.GetString(25));
					despacho.Numeracion = (dr.IsDBNull(26) ? "" : dr.GetString(26));
					despacho.Comentario = (dr.IsDBNull(27) ? "" : dr.GetString(27));
				}
			}
			return despacho;
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

	internal DataTable DetalleParaVerificarRetornoDeProductos(int codDespacho, int icodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("DetalleDespachoParaVerificarRetornoDeProductos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.Parameters.AddWithValue("_codAlmacen", icodAlmacen);
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

	internal bool cambioEstado(int codDespacho, int codEstado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CambioEstadoDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.Parameters.AddWithValue("_codEstado", codEstado);
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

	internal DataSet reporteResumenEntregas(int codDespacho)
	{
		try
		{
			DataSet set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ExportacionDataResumenEntregaDetalle", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("_codEmpresa", frmLogin.iCodEmpresa);
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_resumen_entrega");
			set.WriteXml("C:\\XML\\ReporteResumenEntrega.xml", XmlWriteMode.WriteSchema);
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

	internal int VerificaEntregasActivasDeDespacho(int codDespacho)
	{
		int rpta = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaEntregasActivasPorDespacho", con.conector);
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rpta = dr.GetInt32(0);
				}
			}
			return rpta;
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

	internal int GetDatoListadoDetalleDespachoSegun(int idTablaDocRelacionado, int codFacturaVenta)
	{
		int rpta = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetDatoListadoDetalleDespacho", con.conector);
			cmd.Parameters.AddWithValue("_idTablaDocRelacionado", idTablaDocRelacionado);
			cmd.Parameters.AddWithValue("_codFacturaVenta", codFacturaVenta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rpta = dr.GetInt32(0);
				}
			}
			return rpta;
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

	internal DataTable CargaListadoDetalleDespacho3(int idTablaDocRelacionado, int codFacturaVenta, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDetalleDespacho2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_idTablaDocRelacionado", idTablaDocRelacionado);
			cmd.Parameters.AddWithValue("_codFacturaVenta", codFacturaVenta);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
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

	internal int VerificaEntregasActivasRespectoADespacho(int tipoDocRelacionado, string codDocRelacionado)
	{
		int rpta = -1;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaEntregasActivasPorDocumRelacionadoDeDespacho", con.conector);
			cmd.Parameters.AddWithValue("_codTablaDocRelacionado", tipoDocRelacionado);
			cmd.Parameters.AddWithValue("_codDocRelacionado", codDocRelacionado);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					rpta = dr.GetInt32(0);
				}
			}
			return rpta;
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

	internal bool anular(int codDespacho)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnulacionDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
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

	public DataTable ListaDespacho(int codAlmacen, int codSucursal, int tipoFecha, DateTime desde, DateTime hasta, int codCliente, int tipoListado, int tipoEstado)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", codSucursal);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_desde", desde);
			cmd.Parameters.AddWithValue("_hasta", hasta);
			cmd.Parameters.AddWithValue("_codCliente", codCliente);
			cmd.Parameters.AddWithValue("_tipoListado", tipoListado);
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

	public List<clsDetalleDespacho> CargaListadoDetalleDespacho(int codDespacho)
	{
		List<clsDetalleDespacho> listado = new List<clsDetalleDespacho>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleDespacho", con.conector);
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsDetalleDespacho detalle = new clsDetalleDespacho();
					detalle.CodDetalleDespacho = dr.GetInt32(0);
					detalle.CodDespacho = dr.GetInt32(1);
					detalle.CodProducto = dr.GetInt32(2);
					detalle.ReferenciaProducto = dr.GetString(3);
					detalle.DescripcionProducto = dr.GetString(4);
					detalle.CodUnidad = dr.GetInt32(5);
					detalle.DescripcionUnidad = dr.GetString(6);
					detalle.Cantidad = dr.GetDouble(7);
					detalle.CantidadPendiente = dr.GetDouble(8);
					detalle.CodAlmacenEntregar = dr.GetInt32(9);
					listado.Add(detalle);
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

	public DataTable CargaListadoDetalleDespacho2(int codDespacho, int codalmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaDetalleDespacho", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			cmd.Parameters.AddWithValue("_codAlmacen", codalmacen);
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

	public bool insertEntrega(clsEntrega entrega)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codDespacho", entrega.CodDespacho);
			oParam = cmd.Parameters.AddWithValue("_codUsuario", entrega.CodUsuario);
			oParam = cmd.Parameters.AddWithValue("_nombreCliente", entrega.NombreCliente);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenRegistro", entrega.CodAlmacenRegistro);
			oParam = cmd.Parameters.AddWithValue("_fechaEntrega", entrega.FechaEntrega);
			oParam = cmd.Parameters.AddWithValue("_fechaRegistro", entrega.FechaRegistro);
			oParam = cmd.Parameters.AddWithValue("_codSerie", entrega.CodSerie);
			oParam = cmd.Parameters.AddWithValue("_serie", entrega.Serie);
			oParam = cmd.Parameters.AddWithValue("_numeracion", entrega.Numeracion);
			entrega.Estado = 1;
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			entrega.CodEntrega = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	public bool insertDetalleEntrega(clsDetalleEntrega detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("_codDetalleDespacho", detalle.CodDetalleDespacho);
			oParam = cmd.Parameters.AddWithValue("_codEntrega", detalle.CodEntrega);
			oParam = cmd.Parameters.AddWithValue("_codProducto", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("_codUnidad", detalle.CodUnidad);
			oParam = cmd.Parameters.AddWithValue("_codAlmacenEntregar", detalle.CodAlmacenEntregar);
			oParam = cmd.Parameters.AddWithValue("_cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetalleEntrega = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x != 0)
			{
				return true;
			}
			return false;
		}
		catch (MySqlException)
		{
			return false;
		}
		finally
		{
			con.conector.Dispose();
			cmd.Dispose();
			con.desconectarBD();
		}
	}

	internal DataSet reporteImprimirDatosDespacho(int codDespacho, int codEmpresa)
	{
		try
		{
			DataSet set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ExportacionDataDespachoDetalle", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("_codEmpresa", codEmpresa);
			cmd.Parameters.AddWithValue("_codDespacho", codDespacho);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_despacho");
			set.WriteXml("C:\\XML\\ReporteDespacho.xml", XmlWriteMode.WriteSchema);
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

	internal DataSet reporteImprimirDatosEntrega(int codEntrega)
	{
		try
		{
			DataSet set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ExportacionDataEntregaDetalle", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("_codEmpresa", frmLogin.iCodEmpresa);
			cmd.Parameters.AddWithValue("_codEntrega", codEntrega);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_entrega");
			set.WriteXml("C:\\XML\\ReporteEntrega.xml", XmlWriteMode.WriteSchema);
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
}
