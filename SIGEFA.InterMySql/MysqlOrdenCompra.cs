using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlOrdenCompra : IOrdenCompra
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public int uno;

	public bool insert(clsOrdenCompra Orden)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codAlmacen_ex", Orden.CodAlmacen);
			if (Orden.CodProveedor != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codProveedor_ex", Orden.CodProveedor);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codProveedor_ex", null);
			}
			oParam = cmd.Parameters.AddWithValue("comentario_ex", Orden.Comentario);
			oParam = cmd.Parameters.AddWithValue("codTipoDocumento_ex", Orden.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codserie_ex", Orden.CodSerie);
			oParam = cmd.Parameters.AddWithValue("numeracion_ex", Orden.NumDoc);
			oParam = cmd.Parameters.AddWithValue("fechaorden_ex", Orden.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("codUsuario_ex", Orden.CodUser);
			oParam = cmd.Parameters.AddWithValue("moneda_ex", Orden.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio_ex", Orden.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("bruto_ex", Orden.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto_ex", Orden.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv_ex", Orden.Igv);
			oParam = cmd.Parameters.AddWithValue("total_ex", Orden.Total);
			oParam = cmd.Parameters.AddWithValue("formapago_ex", Orden.FormaPago);
			oParam = cmd.Parameters.AddWithValue("fechapago_ex", Orden.FechaPago);
			oParam = cmd.Parameters.AddWithValue("flete_ex", Orden.Flete);
			oParam = cmd.Parameters.AddWithValue("estadoOrden", Orden.estadoOrden);
			if (Orden.codPropuestaPedido == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codPropuestaPedido", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codPropuestaPedido", Orden.codPropuestaPedido);
			}
			if (Orden.codEmpresaTransporte == 0)
			{
				oParam = cmd.Parameters.AddWithValue("_codEmpTrans", null);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("_codEmpTrans", Orden.codEmpresaTransporte);
			}
			oParam = cmd.Parameters.AddWithValue("_fleteTrans", Orden.fleteTransportista);
			oParam = cmd.Parameters.AddWithValue("_tipCambioProv", Orden.tipCambioProv);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Orden.CodOrdenCompraNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool update(clsOrdenCompra Orden)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", Convert.ToInt32(Orden.CodOrdenCompra));
			cmd.Parameters.AddWithValue("codalma", Orden.CodAlmacen);
			if (Orden.CodProveedor != 0)
			{
				cmd.Parameters.AddWithValue("codprov", Orden.CodProveedor);
			}
			else
			{
				cmd.Parameters.AddWithValue("codprov", null);
			}
			cmd.Parameters.AddWithValue("moneda", Orden.Moneda);
			cmd.Parameters.AddWithValue("tipocambio", Orden.TipoCambio);
			cmd.Parameters.AddWithValue("fechaingreso", Orden.FechaIngreso);
			cmd.Parameters.AddWithValue("comentario", Orden.Comentario);
			cmd.Parameters.AddWithValue("bruto", Orden.MontoBruto);
			cmd.Parameters.AddWithValue("montodscto", Orden.MontoDscto);
			cmd.Parameters.AddWithValue("igv", Orden.Igv);
			cmd.Parameters.AddWithValue("total", Orden.Total);
			cmd.Parameters.AddWithValue("estado", Orden.Estado);
			cmd.Parameters.AddWithValue("formapago", Orden.FormaPago);
			cmd.Parameters.AddWithValue("fechapago", Orden.FechaPago);
			cmd.Parameters.AddWithValue("_codEmpTrans", Orden.codEmpresaTransporte);
			cmd.Parameters.AddWithValue("_fleteTrans", Orden.fleteTransportista);
			cmd.Parameters.AddWithValue("_tipCambioProv", Orden.tipCambioProv);
			cmd.Parameters.AddWithValue("titulo", Orden.TituloOc);
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

	public bool delete(int CodigoOrden)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
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

	public bool anular(int CodigoOrden)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
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

	public bool activar(int CodigoOrden)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActivarOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
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

	public bool insertdetalle(clsDetalleOrdenCompra Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", Detalle.CodProducto);
			if (Detalle.CodOrdenCompra == 0)
			{
				oParam = cmd.Parameters.AddWithValue("codOrd", Detalle.codOrdenNuevo);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codOrd", Detalle.CodOrdenCompra);
			}
			oParam = cmd.Parameters.AddWithValue("codalma", Detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			oParam = cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("cantidadpendiente", Detalle.cantidadpendiente);
			oParam = cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("codusu", Detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("codProve", Detalle.CodProveedor);
			oParam = cmd.Parameters.AddWithValue("estadoOrden", Detalle.estadoOrden);
			oParam = cmd.Parameters.AddWithValue("productosolicitado", Detalle.psoli);
			oParam = cmd.Parameters.AddWithValue("_tipovalor", Detalle.TipoPrecio);
			oParam = cmd.Parameters.AddWithValue("etiquetas", Detalle.etiqueta);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.CodDetalleOrdenCompra = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalle_1(clsDetalleOrdenCompra Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleOrden_1", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", Detalle.CodProducto);
			if (Detalle.CodOrdenCompra == 0)
			{
				oParam = cmd.Parameters.AddWithValue("codOrd", Detalle.codOrdenNuevo);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codOrd", Detalle.CodOrdenCompra);
			}
			oParam = cmd.Parameters.AddWithValue("codalma", Detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			oParam = cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("cantidadpendiente", Detalle.cantidadpendiente);
			oParam = cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("codusu", Detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("codProve", Detalle.CodProveedor);
			oParam = cmd.Parameters.AddWithValue("estadoOrden", Detalle.estadoOrden);
			oParam = cmd.Parameters.AddWithValue("productosolicitado", Detalle.psoli);
			oParam = cmd.Parameters.AddWithValue("_tipovalor", Detalle.TipoPrecio);
			oParam = cmd.Parameters.AddWithValue("etiquetas", Detalle.etiqueta);
			oParam = cmd.Parameters.AddWithValue("_codestado", Detalle.codEstadoo);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.CodDetalleOrdenCompra = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool Eliminar_detalleOC_Promocion_1(int orden, int codProd, int CodEstado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("Eliminar_detPromocionOC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codOrden", orden);
			cmd.Parameters.AddWithValue("_codProd", codProd);
			cmd.Parameters.AddWithValue("_estado_eti", CodEstado);
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

	public bool updatedetalle(clsDetalleOrdenCompra Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", Detalle.CodDetalleOrdenCompra);
			cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
			cmd.Parameters.AddWithValue("cantidadpendiente", Detalle.cantidadpendiente);
			cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			cmd.Parameters.AddWithValue("etiquetas", Detalle.etiqueta);
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

	public bool updatedetalle_1(clsDetalleOrdenCompra Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleOrden_1", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", Detalle.CodDetalleOrdenCompra);
			cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
			cmd.Parameters.AddWithValue("cantidadpendiente", Detalle.cantidadpendiente);
			cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			cmd.Parameters.AddWithValue("etiquetas", Detalle.etiqueta);
			cmd.Parameters.AddWithValue("_codestado", Detalle.codEstadoo);
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

	public bool actualizaestadorden(int CodDetalleOrdenCompra, int estado, int codGuia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEstadoDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodDetalleOrdenCompra);
			cmd.Parameters.AddWithValue("_estado", estado);
			cmd.Parameters.AddWithValue("CodGuia", codGuia);
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

	public bool actualizacantidadpendiente(int CodDetalleOrdenCompra, int estado, int CodGuia)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteEstadoDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodDetalleOrdenCompra);
			cmd.Parameters.AddWithValue("_estado", estado);
			cmd.Parameters.AddWithValue("CodGuia", CodGuia);
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

	public bool actualizaCantidadPendienteOrdenCompra(int CodOrdenCompra)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCantidadPendienteOrdenDeCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codOC", CodOrdenCompra);
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

	public bool registrarModificacionDeOC(int codOrdenCompra, int iCodUser, DateTime fecha)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("registroUpdateOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codOC", codOrdenCompra);
			cmd.Parameters.AddWithValue("_codUser", iCodUser);
			cmd.Parameters.AddWithValue("_fechaModificacion", fecha);
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

	public bool registrarAprobacionDeOC(int codOrdenCompra, int iCodUser, DateTime fecha)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("registroAprobarOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codOC", codOrdenCompra);
			cmd.Parameters.AddWithValue("_codUser", iCodUser);
			cmd.Parameters.AddWithValue("_fechaModificacion", fecha);
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

	public bool actualizanuevaestadorden(int CodDetalleOrdenCompra, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNuevaEstadoDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodDetalleOrdenCompra);
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

	public bool ActualizaOrdenCompra_Estado(int orden, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaOrdenCompra_Estado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", orden);
			cmd.Parameters.AddWithValue("estado", estado);
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

	public bool ActualizarDetOrdenCompra_Estado(int orden, int codProd, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetOrdenCompra_Estado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", orden);
			cmd.Parameters.AddWithValue("codProd", codProd);
			cmd.Parameters.AddWithValue("estado", estado);
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

	public bool actualizacantidad(int CodDetalleOrdenCompra, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNuevaEstadoDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodDetalleOrdenCompra);
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

	public bool actualizaestadocabeceraorden(int codOrdenCompra, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaEstadoCabeceraOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", codOrdenCompra);
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

	public bool actualizanuevaestadocabeceraorden(int codOrdenCompra, int estado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaNuevaEstadoCabeceraOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", codOrdenCompra);
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

	public bool deletedetalle(int CodigoDetalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarDetalleOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", CodigoDetalle);
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

	public bool deletedetalleorden(int Codproducto, int CodigoOrden)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminaProductoOrden", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpro", Codproducto);
			cmd.Parameters.AddWithValue("codOrden", CodigoOrden);
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

	public clsOrdenCompra CargaOrdenCompra(int CodOrden)
	{
		clsOrdenCompra orden = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraOrdenCompra", con.conector);
			cmd.Parameters.AddWithValue("codOrd", CodOrden);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					orden = new clsOrdenCompra();
					orden.CodOrdenCompra = dr.GetInt32(0);
					orden.CodAlmacen = dr.GetInt32(1);
					orden.CodProveedor = Convert.ToInt32(dr.GetString(2));
					orden.RazonSocialProveedor = dr.GetString(4);
					orden.Moneda = Convert.ToString(dr.GetString(5));
					orden.TipoCambio = dr.GetDecimal(6);
					orden.FechaIngreso = dr.GetDateTime(7);
					orden.Comentario = dr.GetString(8);
					orden.MontoBruto = dr.GetDecimal(9);
					orden.MontoDscto = dr.GetDecimal(10);
					orden.Igv = dr.GetDecimal(11);
					orden.Total = dr.GetDecimal(12);
					orden.Estado = dr.GetBoolean(13);
					orden.FormaPago = Convert.ToInt32(dr.GetString(14));
					orden.FechaPago = dr.GetDateTime(15);
					orden.CodUser = Convert.ToInt32(dr.GetDecimal(16));
					orden.FechaRegistro = dr.GetDateTime(17);
					orden.NumDoc = dr.GetString(18);
					orden.RUCProveedor = dr.GetString(19);
					orden.CodSerie = Convert.ToInt32(dr.GetString(20));
					orden.Serie = dr.GetString(21);
					orden.codEmpresaTransporte = ((!dr.IsDBNull(22)) ? dr.GetInt32(22) : 0);
					orden.fleteTransportista = ((!dr.IsDBNull(23)) ? dr.GetInt32(23) : 0);
					orden.estadoOrden = dr.GetInt32(24);
					orden.UsuarioModificador = ((!dr.IsDBNull(25)) ? dr.GetInt32(25) : 0);
					orden.fechaModificacion = (dr.IsDBNull(26) ? DateTime.Now : dr.GetDateTime(26));
					orden.Anulado = dr.GetInt32(27);
					orden.tipCambioProv = dr.GetDecimal(28);
					orden.TituloOc = dr.GetString(29);
				}
			}
			return orden;
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

	public List<clsDetalleOrdenCompra> cargadetalleorden(int CodOrden, int almacen)
	{
		clsDetalleOrdenCompra orden = null;
		List<clsDetalleOrdenCompra> listaorden = new List<clsDetalleOrdenCompra>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ListaDetalleOrdenCompra", con.conector);
			cmd.Parameters.AddWithValue("codOrd", CodOrden);
			cmd.Parameters.AddWithValue("almacen", almacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					orden = new clsDetalleOrdenCompra();
					orden.CodDetalleOrdenCompra = dr.GetInt32(0);
					orden.CodProducto = dr.GetInt32(1);
					orden.CodOrdenCompra = Convert.ToInt32(dr.GetString(2));
					orden.Cantidad = dr.GetDecimal(9);
					orden.cantidadpendiente = dr.GetDecimal(10);
					listaorden.Add(orden);
				}
			}
			return listaorden;
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

	public DataTable CargaDetalle(int CodOrdenn)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodOrdenn);
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

	public DataTable CargaDetalle_Factura_Venta(int CodOrdennVenta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleOrdenCompra_Genera_Venta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodOrdennVenta);
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

	public DataTable obtenerListadoProductoFleteDeGRC(int codigoOC)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoProductoFleteOC", con.conector);
			cmd.Parameters.AddWithValue("_codOrdenCompra", codigoOC);
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

	public DataTable listarGRCGeneradas(int codigoOC)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoGuiasRemisionCompraGeneradasPorOrdenDeCompra", con.conector);
			cmd.Parameters.AddWithValue("_codOrdenCompra", codigoOC);
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

	public DataTable CargaDetalleOrden(int CodOrden)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleOrdenCompra_SinCantidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codOrd", CodOrden);
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

	public DataTable ListaOrdenesCompra(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaOrdenes", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("criterio", Criterio);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", FechaInicial);
			cmd.Parameters.AddWithValue("fechafin", FechaFinal);
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

	public DataTable ListaOrdenes(int CodAlmacen, int tipoFecha, DateTime fechaInicio, DateTime fechaFinal, int codProd, int codEstado)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaOrdenesCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("_tipoFecha", tipoFecha);
			cmd.Parameters.AddWithValue("_fechaInicio", fechaInicio);
			cmd.Parameters.AddWithValue("_fechaFinal", fechaFinal);
			cmd.Parameters.AddWithValue("_codProd", codProd);
			cmd.Parameters.AddWithValue("_codEstado", codEstado);
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

	public DataTable ListaOrdenes_seteados(int CodAlmacen, int codemp)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaOrdenesCompra_seteados", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("CodEm", codemp);
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

	public clsOrdenCompra BuscaOrden(string CodOrden, int CodAlmacen)
	{
		clsOrdenCompra orden = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaOrden", con.conector);
			cmd.Parameters.AddWithValue("cod", Convert.ToInt32(CodOrden));
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					orden = new clsOrdenCompra();
					orden.CodOrdenCompra = dr.GetInt32(0);
					orden.CodProveedor = dr.GetInt32(1);
					orden.RUCProveedor = dr.GetString(2);
					orden.ReferenciaProveedor = dr.GetString(3);
					orden.RazonSocialProveedor = dr.GetString(4);
					orden.Moneda = dr.GetString(5);
					orden.TipoCambio = dr.GetDecimal(6);
					orden.FechaIngreso = dr.GetDateTime(7);
					orden.Comentario = dr.GetString(8);
					orden.MontoBruto = dr.GetDecimal(9);
					orden.MontoDscto = dr.GetDecimal(10);
					orden.Igv = dr.GetDecimal(11);
					orden.Total = dr.GetDecimal(12);
					orden.Estado = dr.GetBoolean(13);
					orden.FormaPago = dr.GetInt32(14);
					orden.FechaPago = dr.GetDateTime(15);
					orden.CodUser = dr.GetInt32(16);
					orden.FechaRegistro = dr.GetDateTime(17);
					orden.Pendiente = dr.GetBoolean(18);
				}
			}
			return orden;
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

	public DataTable ListaOrden()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaOrdenCompra", con.conector);
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

	public DataTable listacomboOrden()
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("listacomboOrdenes", con.conector);
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

	public DataTable generarGuiaRemisionCOmpra(int codOrdenCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("generarGuiaRemisionOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod_oc", codOrdenCompra);
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

	public DataTable StockActualProducto(int CodProducto)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("StockActualProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codprod", CodProducto);
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

	public string getEstadoOrdenCompra(int estadoOrden)
	{
		string estado = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetEstadoOrdenCompra", con.conector);
			cmd.Parameters.AddWithValue("estadoOrden", estadoOrden);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					estado = dr.GetString(0);
				}
			}
			return estado;
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

	public int getCodUltimaGuiaRemisionGenerada(int codOrdenCompra)
	{
		int codigo = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetCodigoUltimaGuiaRemisionOrdenCompra", con.conector);
			cmd.Parameters.AddWithValue("codOC", codOrdenCompra);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					codigo = dr.GetInt32(0);
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

	public bool setEstadoOrdenCompra(int codOrdenCompra, int nuevoEstado)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SetEstadoOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", codOrdenCompra);
			cmd.Parameters.AddWithValue("_nuevoestado", nuevoEstado);
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

	public DataTable ListaProductosModificarPrecio(int codOrdenCompra)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoProductosModificacionPreciosOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod_orden_compra", codOrdenCompra);
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

	public DataTable ListaProductosModificarPrecio_1(int codOrdenCompra, int codemp)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoProductosModificacionPreciosOC", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod_orden_compra", codOrdenCompra);
			cmd.Parameters.AddWithValue("CodEm", codemp);
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

	public bool enviarDatoModificarPrecio(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_fletenuevo", obj.fleteNuevo);
			cmd.Parameters.AddWithValue("_preciocompranuevo", obj.precioCompraNuevo);
			cmd.Parameters.AddWithValue("_precioventanuevo", obj.precioVentaNuevo);
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

	public bool enviarDatoModificarPrecio_fletenuevo(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra_fletenuevo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_fletenuevo", obj.fleteNuevo);
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

	public bool enviarDatoModificarPrecio_preciocompranuevo(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra_preciocompranuevo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_preciocompranuevo", obj.precioCompraNuevo);
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

	public bool enviarDatoModificarPrecio_precioventanuevo(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra_precioventanuevo", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_precioventanuevo", obj.precioVentaNuevo);
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

	public bool enviarDatoModificarPrecio_precioventa_competencia(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra_precioventa_competencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_precioventa_competencia", obj.precioVenta_competencia);
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

	public bool enviarDatoModificarPrecio_precioventa_SKU(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra_precioventa_SKU", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_sku", obj.precioVenta_sku);
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

	public bool enviarDatoModificarPrecio_precioventa_link(DetalleModificarPrecio obj)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("DatoModificarPrecioOrdenCompra_precioventa_link", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codordencompra", obj.codordencompra);
			cmd.Parameters.AddWithValue("_codproducto", obj.codProducto);
			cmd.Parameters.AddWithValue("_codunidadmedida", obj.codUnidadMedida);
			cmd.Parameters.AddWithValue("_codunidadequivalente", obj.codUnidadEquivalente);
			cmd.Parameters.AddWithValue("_link", obj.link_competencia);
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
