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

internal class MysqlPedido : IPedido
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsPedido pedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalma", pedido.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codtipo", pedido.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codcoti", pedido.CodCotizacion);
			oParam = cmd.Parameters.AddWithValue("tipocliente", pedido.TipoCliente);
			if (pedido.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcli", pedido.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", pedido.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", pedido.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechapedido", pedido.FechaPedido);
			oParam = cmd.Parameters.AddWithValue("fechaentrega", pedido.FechaEntrega);
			oParam = cmd.Parameters.AddWithValue("auto", pedido.CodAutorizado);
			oParam = cmd.Parameters.AddWithValue("comentario", pedido.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", pedido.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", pedido.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", pedido.Igv);
			oParam = cmd.Parameters.AddWithValue("total", pedido.Total);
			oParam = cmd.Parameters.AddWithValue("estado", pedido.Estado);
			oParam = cmd.Parameters.AddWithValue("formapago", pedido.FormaPago);
			oParam = cmd.Parameters.AddWithValue("fechapago", pedido.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codusu", pedido.CodUser);
			oParam = cmd.Parameters.AddWithValue("nombrecliente", pedido.Nombrecliente);
			oParam = cmd.Parameters.AddWithValue("tipoventa_ex", pedido.Tipoventa);
			oParam = cmd.Parameters.AddWithValue("gravadas_ex", pedido.Gravadas);
			oParam = cmd.Parameters.AddWithValue("exoneradas_ex", pedido.Exoneradas);
			oParam = cmd.Parameters.AddWithValue("inafectas_ex", pedido.Inafectas);
			oParam = cmd.Parameters.AddWithValue("gratuitas_ex", pedido.Gratuitas);
			oParam = cmd.Parameters.AddWithValue("codEmpresa_ex", pedido.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("Boletafactura_ex", pedido.Boletafactura);
			oParam = cmd.Parameters.AddWithValue("codSerie_ex", pedido.CodSerie);
			oParam = cmd.Parameters.AddWithValue("seriedoc_ex", pedido.SerieDoc);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pedido.CodPedido = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool insertarOrdenVenta(clsPedido pedido)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("codalma", pedido.CodAlmacen);
			oParamP = cmd.Parameters.AddWithValue("codtipo", pedido.CodTipoDocumento);
			oParamP = cmd.Parameters.AddWithValue("codcoti", pedido.CodCotizacion);
			oParamP = cmd.Parameters.AddWithValue("tipocliente", pedido.TipoCliente);
			if (pedido.CodCliente != 0)
			{
				oParamP = cmd.Parameters.AddWithValue("codcli", pedido.CodCliente);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParamP = cmd.Parameters.AddWithValue("moneda", pedido.Moneda);
			oParamP = cmd.Parameters.AddWithValue("tipocambio", pedido.TipoCambio);
			oParamP = cmd.Parameters.AddWithValue("fechapedido", pedido.FechaPedido);
			oParamP = cmd.Parameters.AddWithValue("fechaentrega", pedido.FechaEntrega);
			oParamP = cmd.Parameters.AddWithValue("auto", pedido.CodAutorizado);
			oParamP = cmd.Parameters.AddWithValue("comentario", pedido.Comentario);
			oParamP = cmd.Parameters.AddWithValue("bruto", pedido.MontoBruto);
			oParamP = cmd.Parameters.AddWithValue("montodscto", pedido.MontoDscto);
			oParamP = cmd.Parameters.AddWithValue("igv", pedido.Igv);
			oParamP = cmd.Parameters.AddWithValue("total", pedido.Total);
			oParamP = cmd.Parameters.AddWithValue("estado", pedido.Estado);
			oParamP = cmd.Parameters.AddWithValue("_formapago", pedido.FormaPago);
			oParamP = cmd.Parameters.AddWithValue("fechapago", pedido.FechaPago);
			oParamP = cmd.Parameters.AddWithValue("codusu", pedido.CodUser);
			oParamP = cmd.Parameters.AddWithValue("nombrecliente", pedido.Nombrecliente);
			oParamP = cmd.Parameters.AddWithValue("tipoventa_ex", pedido.Tipoventa);
			oParamP = cmd.Parameters.AddWithValue("gravadas_ex", pedido.Gravadas);
			oParamP = cmd.Parameters.AddWithValue("exoneradas_ex", pedido.Exoneradas);
			oParamP = cmd.Parameters.AddWithValue("inafectas_ex", pedido.Inafectas);
			oParamP = cmd.Parameters.AddWithValue("gratuitas_ex", pedido.Gratuitas);
			oParamP = cmd.Parameters.AddWithValue("codEmpresa_ex", pedido.CodEmpresa);
			oParamP = cmd.Parameters.AddWithValue("Boletafactura_ex", pedido.Boletafactura);
			oParamP = cmd.Parameters.AddWithValue("codSerie_ex", pedido.CodSerie);
			oParamP = cmd.Parameters.AddWithValue("seriedoc_ex", pedido.SerieDoc);
			oParamP = cmd.Parameters.AddWithValue("ventasinstock_ex", pedido.ventasinstock);
			oParamP = cmd.Parameters.AddWithValue("_icbper", pedido.Icbper);
			oParamP = cmd.Parameters.AddWithValue("_valorRetencion", pedido.ValorRetencion);
			if (pedido.idTecnico > -1)
			{
				oParamP = cmd.Parameters.AddWithValue("_idTecnico", pedido.idTecnico);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_idTecnico", null);
			}
			if (pedido.idZona > 0)
			{
				oParamP = cmd.Parameters.AddWithValue("_idZona", pedido.idZona);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_idZona", null);
			}
			if (pedido.CodCanalVenta == null)
			{
				pedido.CodCanalVenta = "";
			}
			if (pedido.CodCanalVenta.Length == 0)
			{
				oParamP = cmd.Parameters.AddWithValue("_codCanalVenta", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_codCanalVenta", pedido.CodCanalVenta);
			}
			oParamP = cmd.Parameters.AddWithValue("newid", 0);
			oParamP.Direction = ParameterDirection.Output;
			int xP = cmd.ExecuteNonQuery();
			pedido.CodPedido = Convert.ToString(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)pedido.CodPedido, (Func<char, bool>)char.IsDigit) || pedido.CodPedido == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePedido detalle in pedido.Detalle)
			{
				if (detalle.Tipoimpuesto.Contains('1') && detalle.Igv == 0m)
				{
					throw new Exception("El producto con codigo: " + detalle.CodProducto + " tiene calculado el IGV en cero");
				}
				cmd = new MySqlCommand("GuardaDetallePedido", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
				oParam = cmd.Parameters.AddWithValue("codpedido", pedido.CodPedido);
				oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
				oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
				oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
				oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
				oParam = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
				oParam = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
				oParam = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
				oParam = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
				oParam = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
				oParam = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
				oParam = cmd.Parameters.AddWithValue("igv", detalle.Igv);
				oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
				oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
				oParam = cmd.Parameters.AddWithValue("codprov", detalle.CodProv);
				oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
				oParam = cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
				oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
				oParam = cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
				oParam = cmd.Parameters.AddWithValue("pmargen", detalle.PrecioMargen);
				oParam = cmd.Parameters.AddWithValue("codtidoc", detalle.Codtipodoc);
				oParam = cmd.Parameters.AddWithValue("tipoimpuesto_ex", detalle.Tipoimpuesto);
				oParam = cmd.Parameters.AddWithValue("codempresa_ex", detalle.CodEmpresa);
				oParam = cmd.Parameters.AddWithValue("_icbper", detalle.icbper);
				oParam = cmd.Parameters.AddWithValue("_icbper_band", detalle.icbper_band);
				oParam = cmd.Parameters.AddWithValue("codlinea", detalle.codlinea);
				oParam = cmd.Parameters.AddWithValue("codfamilia", detalle.codfamilia);
				oParam = cmd.Parameters.AddWithValue("_codcombo", detalle.codcombo);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle.CodDetallePedido = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.CodDetallePedido == 0)
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

	public bool insertarOrdenVentaSinStock(clsPedido pedido)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParamP = cmd.Parameters.AddWithValue("codalma", pedido.CodAlmacen);
			oParamP = cmd.Parameters.AddWithValue("codtipo", pedido.CodTipoDocumento);
			oParamP = cmd.Parameters.AddWithValue("codcoti", pedido.CodCotizacion);
			oParamP = cmd.Parameters.AddWithValue("tipocliente", pedido.TipoCliente);
			if (pedido.CodCliente != 0)
			{
				oParamP = cmd.Parameters.AddWithValue("codcli", pedido.CodCliente);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParamP = cmd.Parameters.AddWithValue("moneda", pedido.Moneda);
			oParamP = cmd.Parameters.AddWithValue("tipocambio", pedido.TipoCambio);
			oParamP = cmd.Parameters.AddWithValue("fechapedido", pedido.FechaPedido);
			oParamP = cmd.Parameters.AddWithValue("fechaentrega", pedido.FechaEntrega);
			oParamP = cmd.Parameters.AddWithValue("auto", pedido.CodAutorizado);
			oParamP = cmd.Parameters.AddWithValue("comentario", pedido.Comentario);
			oParamP = cmd.Parameters.AddWithValue("bruto", pedido.MontoBruto);
			oParamP = cmd.Parameters.AddWithValue("montodscto", pedido.MontoDscto);
			oParamP = cmd.Parameters.AddWithValue("igv", pedido.Igv);
			oParamP = cmd.Parameters.AddWithValue("total", pedido.Total);
			oParamP = cmd.Parameters.AddWithValue("estado", pedido.Estado);
			oParamP = cmd.Parameters.AddWithValue("_formapago", pedido.FormaPago);
			oParamP = cmd.Parameters.AddWithValue("fechapago", pedido.FechaPago);
			oParamP = cmd.Parameters.AddWithValue("codusu", pedido.CodUser);
			oParamP = cmd.Parameters.AddWithValue("nombrecliente", pedido.Nombrecliente);
			oParamP = cmd.Parameters.AddWithValue("tipoventa_ex", pedido.Tipoventa);
			oParamP = cmd.Parameters.AddWithValue("gravadas_ex", pedido.Gravadas);
			oParamP = cmd.Parameters.AddWithValue("exoneradas_ex", pedido.Exoneradas);
			oParamP = cmd.Parameters.AddWithValue("inafectas_ex", pedido.Inafectas);
			oParamP = cmd.Parameters.AddWithValue("gratuitas_ex", pedido.Gratuitas);
			oParamP = cmd.Parameters.AddWithValue("codEmpresa_ex", pedido.CodEmpresa);
			oParamP = cmd.Parameters.AddWithValue("Boletafactura_ex", pedido.Boletafactura);
			oParamP = cmd.Parameters.AddWithValue("codSerie_ex", pedido.CodSerie);
			oParamP = cmd.Parameters.AddWithValue("seriedoc_ex", pedido.SerieDoc);
			oParamP = cmd.Parameters.AddWithValue("ventasinstock_ex", pedido.ventasinstock);
			oParamP = cmd.Parameters.AddWithValue("_icbper", pedido.Icbper);
			oParamP = cmd.Parameters.AddWithValue("_valorRetencion", pedido.ValorRetencion);
			if (pedido.idTecnico > -1)
			{
				oParamP = cmd.Parameters.AddWithValue("_idTecnico", pedido.idTecnico);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_idTecnico", null);
			}
			if (pedido.idZona > 0)
			{
				oParamP = cmd.Parameters.AddWithValue("_idZona", pedido.idZona);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_idZona", null);
			}
			if (pedido.CodCanalVenta == null)
			{
				pedido.CodCanalVenta = "";
			}
			if (pedido.CodCanalVenta.Length == 0)
			{
				oParamP = cmd.Parameters.AddWithValue("_codCanalVenta", null);
			}
			else
			{
				oParamP = cmd.Parameters.AddWithValue("_codCanalVenta", pedido.CodCanalVenta);
			}
			oParamP = cmd.Parameters.AddWithValue("newid", 0);
			oParamP.Direction = ParameterDirection.Output;
			int xP = cmd.ExecuteNonQuery();
			pedido.CodPedido = Convert.ToString(cmd.Parameters["newid"].Value);
			if (!Enumerable.All<char>((IEnumerable<char>)pedido.CodPedido, (Func<char, bool>)char.IsDigit) || pedido.CodPedido == "0")
			{
				rpta = false;
			}
			if (!rpta)
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				return rpta;
			}
			foreach (clsDetallePedido detalle in pedido.Detalle)
			{
				if (detalle.Tipoimpuesto.Contains('1') && detalle.Igv == 0m)
				{
					throw new Exception("El producto con codigo: " + detalle.CodProducto + " tiene calculado el IGV en cero");
				}
				cmd = new MySqlCommand("GuardaDetallePedidoSinStock", con.conector);
				cmd.CommandType = CommandType.StoredProcedure;
				MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
				oParam = cmd.Parameters.AddWithValue("codpedido", pedido.CodPedido);
				oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
				oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
				oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
				oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
				oParam = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
				oParam = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
				oParam = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
				oParam = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
				oParam = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
				oParam = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
				oParam = cmd.Parameters.AddWithValue("igv", detalle.Igv);
				oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
				oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
				oParam = cmd.Parameters.AddWithValue("codprov", detalle.CodProv);
				oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
				oParam = cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
				oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
				oParam = cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
				oParam = cmd.Parameters.AddWithValue("pmargen", detalle.PrecioMargen);
				oParam = cmd.Parameters.AddWithValue("codtidoc", detalle.Codtipodoc);
				oParam = cmd.Parameters.AddWithValue("tipoimpuesto_ex", detalle.Tipoimpuesto);
				oParam = cmd.Parameters.AddWithValue("codempresa_ex", detalle.CodEmpresa);
				oParam = cmd.Parameters.AddWithValue("_icbper", detalle.icbper);
				oParam = cmd.Parameters.AddWithValue("_icbper_band", detalle.icbper_band);
				oParam = cmd.Parameters.AddWithValue("codlinea", detalle.codlinea);
				oParam = cmd.Parameters.AddWithValue("codfamilia", detalle.codfamilia);
				oParam = cmd.Parameters.AddWithValue("newid", 0);
				oParam.Direction = ParameterDirection.Output;
				int xDP = cmd.ExecuteNonQuery();
				detalle.CodDetallePedido = Convert.ToInt32(cmd.Parameters["newid"].Value);
				if (detalle.CodDetallePedido == 0)
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
			Scope.Complete();
			Scope.Dispose();
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

	public bool update(clsPedido pedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpedid", Convert.ToInt32(pedido.CodPedido));
			cmd.Parameters.AddWithValue("codalma", pedido.CodAlmacen);
			cmd.Parameters.AddWithValue("codtipo", pedido.CodTipoDocumento);
			cmd.Parameters.AddWithValue("codcoti", pedido.CodCotizacion);
			cmd.Parameters.AddWithValue("tipoclient", pedido.TipoCliente);
			if (pedido.CodCliente != 0)
			{
				cmd.Parameters.AddWithValue("codcli", pedido.CodCliente);
			}
			else
			{
				cmd.Parameters.AddWithValue("codcli", null);
			}
			cmd.Parameters.AddWithValue("moned", pedido.Moneda);
			cmd.Parameters.AddWithValue("tipocambi", pedido.TipoCambio);
			cmd.Parameters.AddWithValue("fechapedid", pedido.FechaPedido);
			cmd.Parameters.AddWithValue("fechaentreg", pedido.FechaEntrega);
			cmd.Parameters.AddWithValue("codlist", pedido.CodListaPrecio);
			cmd.Parameters.AddWithValue("auto", pedido.CodAutorizado);
			cmd.Parameters.AddWithValue("comentari", pedido.Comentario);
			cmd.Parameters.AddWithValue("brut", pedido.MontoBruto);
			cmd.Parameters.AddWithValue("montodsct", pedido.MontoDscto);
			cmd.Parameters.AddWithValue("ig", pedido.Igv);
			cmd.Parameters.AddWithValue("tota", pedido.Total);
			cmd.Parameters.AddWithValue("estad", pedido.Estado);
			cmd.Parameters.AddWithValue("formapag", pedido.FormaPago);
			cmd.Parameters.AddWithValue("fechapag", pedido.FechaPago);
			cmd.Parameters.AddWithValue("nomcliente", pedido.Nombrecliente);
			cmd.Parameters.AddWithValue("coduser", pedido.CodUser);
			cmd.Parameters.AddWithValue("_valorRetencion", pedido.ValorRetencion);
			if (pedido.idTecnico > -1)
			{
				cmd.Parameters.AddWithValue("_idTecnico", pedido.idTecnico);
			}
			else
			{
				cmd.Parameters.AddWithValue("_idTecnico", null);
			}
			if (pedido.idZona > 0)
			{
				cmd.Parameters.AddWithValue("_idZona", pedido.idZona);
			}
			else
			{
				cmd.Parameters.AddWithValue("_idZona", null);
			}
			if (pedido.CodCanalVenta == null)
			{
				pedido.CodCanalVenta = "";
			}
			if (pedido.CodCanalVenta.Length == 0)
			{
				cmd.Parameters.AddWithValue("_codCanalVenta", null);
			}
			else
			{
				cmd.Parameters.AddWithValue("_codCanalVenta", pedido.CodCanalVenta);
			}
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

	public bool delete(int CodigoPedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codped", CodigoPedido);
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

	public bool insertdetalle(clsDetallePedido detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetallePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codpedido", detalle.CodPedido);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("codprov", detalle.CodProv);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
			oParam = cmd.Parameters.AddWithValue("pmargen", detalle.PrecioMargen);
			oParam = cmd.Parameters.AddWithValue("codtidoc", detalle.Codtipodoc);
			oParam = cmd.Parameters.AddWithValue("tipoimpuesto_ex", detalle.Tipoimpuesto);
			oParam = cmd.Parameters.AddWithValue("codempresa_ex", detalle.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("_icbper", detalle.icbper);
			oParam = cmd.Parameters.AddWithValue("_icbper_band", detalle.icbper_band);
			oParam = cmd.Parameters.AddWithValue("codlinea", detalle.codlinea);
			oParam = cmd.Parameters.AddWithValue("codfamilia", detalle.codfamilia);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetallePedido = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalleSinStock(clsDetallePedido detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetallePedidoSinStock", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codpedido", detalle.CodPedido);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("codprov", detalle.CodProv);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("precioigv", detalle.Precioigv);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("promedio", detalle.Valorpromedio);
			oParam = cmd.Parameters.AddWithValue("pmargen", detalle.PrecioMargen);
			oParam = cmd.Parameters.AddWithValue("codtidoc", detalle.Codtipodoc);
			oParam = cmd.Parameters.AddWithValue("tipoimpuesto_ex", detalle.Tipoimpuesto);
			oParam = cmd.Parameters.AddWithValue("codempresa_ex", detalle.CodEmpresa);
			oParam = cmd.Parameters.AddWithValue("_icbper", detalle.icbper);
			oParam = cmd.Parameters.AddWithValue("_icbper_band", detalle.icbper_band);
			oParam = cmd.Parameters.AddWithValue("codlinea", detalle.codlinea);
			oParam = cmd.Parameters.AddWithValue("codfamilia", detalle.codfamilia);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetallePedido = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool updatedetalle(clsDetallePedido detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetallePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodDetallePedido);
			cmd.Parameters.AddWithValue("unida", detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielot", detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantida", detalle.Cantidad);
			cmd.Parameters.AddWithValue("preci", detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtota", detalle.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodsct", detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("ig", detalle.Igv);
			cmd.Parameters.AddWithValue("impor", detalle.Importe);
			cmd.Parameters.AddWithValue("preciorea", detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valorea", detalle.ValoReal);
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

	public bool updatedetalleadicional(clsDetallePedido detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetallePedidoAdicional", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod_detalle_pedido", detalle.CodDetallePedido);
			cmd.Parameters.AddWithValue("serie_", detalle.SerieMotor);
			cmd.Parameters.AddWithValue("nrochasis_", detalle.NroChasis);
			cmd.Parameters.AddWithValue("modelo_", detalle.Modelo);
			cmd.Parameters.AddWithValue("marca_", detalle.Marca);
			cmd.Parameters.AddWithValue("color_", detalle.Color);
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
			cmd = new MySqlCommand("EliminarDetallePedido", con.conector);
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

	public clsPedido CargaPedido(int CodPedido)
	{
		clsPedido pedido = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPedido", con.conector);
			cmd.Parameters.AddWithValue("codpedido", CodPedido);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pedido = new clsPedido();
					pedido.CodPedido = dr.GetString(0);
					pedido.CodAlmacen = dr.GetInt32(1);
					pedido.CodTipoDocumento = dr.GetInt32(2);
					pedido.SiglaDocumento = dr.GetString(3);
					pedido.DescripcionDocumento = dr.GetString(4);
					pedido.TipoCliente = dr.GetInt32(5);
					pedido.CodCliente = dr.GetInt32(6);
					pedido.DNI = dr.GetString(7);
					pedido.RUCCliente = dr.GetString(8);
					pedido.CodigoPersonalizado = dr.GetString(9);
					pedido.RazonSocialCliente = dr.GetString(10);
					pedido.Nombre = dr.GetString(11);
					pedido.Direccion = dr.GetString(12);
					pedido.Moneda = dr.GetInt32(13);
					pedido.TipoCambio = dr.GetDouble(14);
					pedido.FechaPedido = dr.GetDateTime(15);
					pedido.FechaEntrega = dr.GetDateTime(16);
					pedido.Comentario = dr.GetString(17);
					pedido.MontoBruto = dr.GetDecimal(18);
					pedido.MontoDscto = dr.GetDecimal(19);
					pedido.Igv = dr.GetDecimal(20);
					pedido.Total = dr.GetDecimal(21);
					pedido.Estado = dr.GetInt32(22);
					pedido.FormaPago = dr.GetInt32(23);
					pedido.FechaPago = dr.GetDateTime(24);
					pedido.CodUser = dr.GetInt32(25);
					pedido.FechaRegistro = dr.GetDateTime(26);
					pedido.CodCotizacion = dr.GetInt32(27);
					pedido.Nombrecliente = dr.GetString(28);
					pedido.Numeracion = dr.GetInt32(29);
					pedido.Tipoventa = dr.GetInt32(30);
					pedido.Gravadas = dr.GetDecimal(31);
					pedido.Exoneradas = dr.GetDecimal(32);
					pedido.Inafectas = dr.GetDecimal(33);
					pedido.Gratuitas = dr.GetDecimal(34);
					pedido.CodEmpresa = dr.GetInt32(35);
					pedido.Boletafactura = dr.GetInt32(36);
					pedido.CodSerie = dr.GetInt32(37);
					pedido.SerieDoc = dr.GetString(38);
					pedido.CodigoBarras = dr.GetString(39);
					pedido.CodigoBarrasCifrado = dr.GetString(40);
					pedido.CodVendedor = dr.GetInt32(41);
					pedido.Pendiente = dr.GetInt32(42);
					pedido.ventasinstock = dr.GetInt32(43);
					pedido.Icbper = dr.GetDecimal(44);
					pedido.ValorRetencion = dr.GetInt32(45);
					pedido.idTecnico = (dr.IsDBNull(46) ? (-1) : dr.GetInt32(46));
					pedido.idZona = ((!dr.IsDBNull(47)) ? dr.GetInt32(47) : 0);
					pedido.CodCanalVenta = (dr.IsDBNull(48) ? "0" : dr.GetString(48));
				}
			}
			return pedido;
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

	public List<clsDetallePedido> cargaDetallePedido(int codPedido)
	{
		List<clsDetallePedido> lista_detalle = new List<clsDetallePedido>();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("cargaDetallePedido", con.conector);
			cmd.Parameters.AddWithValue("_codpedido", codPedido);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					clsDetallePedido detalle = new clsDetallePedido();
					detalle.CodDetallePedido = dr.GetInt32("codDetallePedido");
					detalle.CodProducto = dr.GetInt32("codProducto");
					detalle.CodPedido = dr.GetInt32("codPedido");
					detalle.CodAlmacen = dr.GetInt32("codAlmacen");
					detalle.UnidadIngresada = dr.GetInt32("unidadingresada");
					detalle.Cantidad = dr.GetDecimal("cantidad");
					detalle.PrecioUnitario = dr.GetDecimal("preciounitario");
					detalle.Subtotal = dr.GetDecimal("subtotal");
					detalle.Descuento1 = dr.GetDecimal("descuento1");
					detalle.Descuento2 = dr.GetDecimal("descuento2");
					detalle.Descuento3 = dr.GetDecimal("descuento3");
					detalle.MontoDescuento = dr.GetDecimal("montodscto");
					detalle.Igv = dr.GetDecimal("igv");
					detalle.Importe = dr.GetDecimal("importe");
					detalle.PrecioReal = dr.GetDecimal("precioreal");
					detalle.ValoReal = dr.GetDecimal("valoreal");
					detalle.CodProv = dr.GetInt32("codprov");
					detalle.Precioigv = dr.GetBoolean("precioigv");
					detalle.CodUser = dr.GetInt32("codUser");
					detalle.Valorpromedio = dr.GetDecimal("valorpromedio");
					detalle.Tipoimpuesto = dr.GetString("tipoimpuesto");
					detalle.CodEmpresa = ((!dr.IsDBNull(dr.GetOrdinal("codempresa"))) ? dr.GetInt32("codempresa") : 0);
					detalle.icbper = dr.GetDecimal("icbper");
					detalle.icbper_band = dr.GetBoolean("icbper_band");
					detalle.codlinea = dr.GetInt32("codlinea");
					detalle.codfamilia = dr.GetInt32("codfamilia");
					lista_detalle.Add(detalle);
				}
			}
			return lista_detalle;
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

	public clsPedido BuscaPedido(string CodPedido, int CodAlmacen)
	{
		clsPedido pedido = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("BuscaPedido", con.conector);
			cmd.Parameters.AddWithValue("codpe", Convert.ToInt32(CodPedido));
			cmd.Parameters.AddWithValue("codalm", CodAlmacen);
			cmd.Parameters.AddWithValue("tipo", 1);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pedido = new clsPedido();
					pedido.CodPedido = dr.GetString(0);
					pedido.CodTipoDocumento = dr.GetInt32(1);
					pedido.SiglaDocumento = dr.GetString(2);
					pedido.TipoCliente = dr.GetInt32(3);
					pedido.CodCliente = dr.GetInt32(4);
					pedido.DNI = dr.GetString(5);
					pedido.RUCCliente = dr.GetString(6);
					pedido.CodigoPersonalizado = dr.GetString(7);
					pedido.RazonSocialCliente = dr.GetString(8);
					pedido.Nombre = dr.GetString(9);
					pedido.Direccion = dr.GetString(10);
					pedido.Moneda = dr.GetInt32(11);
					pedido.TipoCambio = dr.GetDouble(12);
					pedido.FechaPedido = dr.GetDateTime(13);
					pedido.FechaEntrega = dr.GetDateTime(14);
					pedido.Comentario = dr.GetString(15);
					pedido.MontoBruto = dr.GetDecimal(16);
					pedido.MontoDscto = dr.GetDecimal(17);
					pedido.Igv = dr.GetDecimal(18);
					pedido.Total = dr.GetDecimal(19);
					pedido.Estado = dr.GetInt32(20);
					pedido.Pendiente = dr.GetInt32(21);
					pedido.FormaPago = dr.GetInt32(22);
					pedido.FechaPago = dr.GetDateTime(23);
					pedido.CodUser = dr.GetInt32(24);
					pedido.FechaRegistro = dr.GetDateTime(25);
					pedido.CodListaPrecio = dr.GetInt32(26);
					pedido.CodCotizacion = dr.GetInt32(27);
					pedido.Numeracion = dr.GetInt32(28);
				}
			}
			return pedido;
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

	public DataTable CargaDetalle2(int CodPedido, int CodAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetallePedido2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpedido", CodPedido);
			cmd.Parameters.AddWithValue("codalmacen1", CodAlmacen);
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

	public DataTable CargaDetalle(int CodPedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetallePedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpedido", CodPedido);
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

	public DataTable CargaDetalleGuia(int CodPedido)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetallePedidoGuia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpedido", CodPedido);
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

	public DataTable ListaPedidos(int user, int CodAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPedidos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public clsPedido CargaEntrega(int CodEntrega)
	{
		clsPedido pedido = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraEntrega", con.conector);
			cmd.Parameters.AddWithValue("codentrega", CodEntrega);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pedido = new clsPedido();
					pedido.CodPedido = dr.GetString(0);
					pedido.CodAlmacen = Convert.ToInt32(dr.GetDecimal(1));
					pedido.CodTipoDocumento = Convert.ToInt32(dr.GetDecimal(2));
					pedido.SiglaDocumento = dr.GetString(3);
					pedido.DescripcionDocumento = dr.GetString(4);
					pedido.TipoCliente = Convert.ToInt32(dr.GetString(5));
					pedido.CodCliente = Convert.ToInt32(dr.GetString(6));
					pedido.DNI = dr.GetString(7);
					pedido.RUCCliente = dr.GetString(8);
					pedido.CodigoPersonalizado = dr.GetString(9);
					pedido.RazonSocialCliente = dr.GetString(10);
					pedido.Nombre = dr.GetString(11);
					pedido.Direccion = dr.GetString(12);
					pedido.Moneda = Convert.ToInt32(dr.GetString(13));
					pedido.TipoCambio = dr.GetDouble(14);
					pedido.FechaPedido = dr.GetDateTime(15);
					pedido.FechaEntrega = dr.GetDateTime(16);
					pedido.Comentario = dr.GetString(17);
					pedido.MontoBruto = dr.GetDecimal(18);
					pedido.MontoDscto = dr.GetDecimal(19);
					pedido.Igv = dr.GetDecimal(20);
					pedido.Total = dr.GetDecimal(21);
					pedido.Estado = Convert.ToInt32(dr.GetDecimal(22));
					pedido.FormaPago = Convert.ToInt32(dr.GetString(23));
					pedido.FechaPago = dr.GetDateTime(24);
					pedido.CodUser = Convert.ToInt32(dr.GetDecimal(25));
					pedido.FechaRegistro = dr.GetDateTime(26);
					pedido.CodAutorizado = Convert.ToInt32(dr.GetDecimal(27));
					pedido.NombreAutorizado = dr.GetString(28);
					pedido.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(29));
					pedido.CodCotizacion = Convert.ToInt32(dr.GetDecimal(30));
				}
			}
			return pedido;
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

	public DataTable CargaDetalleEntrega(int CodEntrega)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraDetalleEntrega", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codentrega", CodEntrega);
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

	public bool insertEntConsExt(clsPedido pedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaEntregaConsultorExt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codalma", pedido.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codtipo", pedido.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("codcoti", pedido.CodCotizacion);
			oParam = cmd.Parameters.AddWithValue("tipocliente", pedido.TipoCliente);
			if (pedido.CodCliente != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codcli", pedido.CodCliente);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codcli", null);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", pedido.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", pedido.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechapedido", pedido.FechaPedido);
			oParam = cmd.Parameters.AddWithValue("fechaentrega", pedido.FechaEntrega);
			oParam = cmd.Parameters.AddWithValue("codlista", pedido.CodListaPrecio);
			oParam = cmd.Parameters.AddWithValue("auto", pedido.CodAutorizado);
			oParam = cmd.Parameters.AddWithValue("comentario", pedido.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", pedido.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", pedido.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", pedido.Igv);
			oParam = cmd.Parameters.AddWithValue("total", pedido.Total);
			oParam = cmd.Parameters.AddWithValue("estado", pedido.Estado);
			oParam = cmd.Parameters.AddWithValue("formapago", pedido.FormaPago);
			oParam = cmd.Parameters.AddWithValue("fechapago", pedido.FechaPago);
			oParam = cmd.Parameters.AddWithValue("codusu", pedido.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pedido.CodPedido = Convert.ToString(cmd.Parameters["newid"].Value);
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

	public bool updatedetallesalidaconsultext(clsDetallePedido detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaDetalleconsultext", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("coddeta", detalle.CodDetallePedido);
			cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			cmd.Parameters.AddWithValue("igv", detalle.Igv);
			cmd.Parameters.AddWithValue("importe", detalle.Importe);
			cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			cmd.Parameters.AddWithValue("cantidaddevuelta", detalle.DCantidadDevuelta);
			cmd.Parameters.AddWithValue("cantidadvendida", detalle.DCantidadVendida);
			cmd.Parameters.AddWithValue("importedev", detalle.IImpDevuelto);
			cmd.Parameters.AddWithValue("importevend", detalle.IImpVend);
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

	public bool deleteEntConsExt(int CodEntConExt)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("EliminarEntregaConsultorExt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codentconext", CodEntConExt);
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

	public DataTable MuestraEntregasConsultorExt(int CodAlmacen, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaEntregasConsultorExt", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fecha1", Fecha1);
			cmd.Parameters.AddWithValue("fecha2", Fecha2);
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

	public bool insertdetalleconsultor(clsDetallePedido detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleConsultor", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codpedido", detalle.CodPedido);
			oParam = cmd.Parameters.AddWithValue("codalma", detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("unidad", detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("cantidad", detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("precio", detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("importe", detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("codusu", detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			detalle.CodDetallePedido = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public DataTable ListaPedidosTodos(int user, int CodAlmacen, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaPedidosTodos", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("usu", user);
			cmd.Parameters.AddWithValue("codalma", CodAlmacen);
			cmd.Parameters.AddWithValue("fechaini", desde);
			cmd.Parameters.AddWithValue("fechafin", hasta);
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

	public bool liquidar(int CodigoPedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("LiquidarPedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codped", CodigoPedido);
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

	public bool rollbackpedido(int codpedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("rollbackpedido", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codped", codpedido);
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

	public bool cambiaMotivoRetencion(int codPedido, bool valorRetencion)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("cambiaValorRetencionPedidoVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codped", codPedido);
			cmd.Parameters.AddWithValue("valorRet", valorRetencion ? 1 : 0);
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

	public bool activaPedidoVenta(int codpedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("activaPedidoVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codPedidoVenta", codpedido);
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

	public bool GuardaCodigoBarras(clsPedido pedido)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaCodigoBarras", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codPedido_ex", Convert.ToInt32(pedido.CodPedido));
			cmd.Parameters.AddWithValue("codigobarras_ex", pedido.CodigoBarras);
			cmd.Parameters.AddWithValue("codigoBarrasCifrado_ex", pedido.CodigoBarrasCifrado);
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

	public clsPedido CargaPedidoxAlmacen(int CodPedido, int codalma)
	{
		clsPedido pedido = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPedidoxAlmacen", con.conector);
			cmd.Parameters.AddWithValue("codpedido", CodPedido);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pedido = new clsPedido();
					pedido.CodPedido = dr.GetString(0);
					pedido.CodAlmacen = dr.GetInt32(1);
					pedido.CodTipoDocumento = dr.GetInt32(2);
					pedido.SiglaDocumento = dr.GetString(3);
					pedido.DescripcionDocumento = dr.GetString(4);
					pedido.TipoCliente = dr.GetInt32(5);
					pedido.CodCliente = dr.GetInt32(6);
					pedido.DNI = dr.GetString(7);
					pedido.RUCCliente = dr.GetString(8);
					pedido.CodigoPersonalizado = dr.GetString(9);
					pedido.RazonSocialCliente = dr.GetString(10);
					pedido.Nombre = dr.GetString(11);
					pedido.Direccion = dr.GetString(12);
					pedido.Moneda = dr.GetInt32(13);
					pedido.TipoCambio = dr.GetDouble(14);
					pedido.FechaPedido = dr.GetDateTime(15);
					pedido.FechaEntrega = dr.GetDateTime(16);
					pedido.Comentario = dr.GetString(17);
					pedido.MontoBruto = dr.GetDecimal(18);
					pedido.MontoDscto = dr.GetDecimal(19);
					pedido.Igv = dr.GetDecimal(20);
					pedido.Total = dr.GetDecimal(21);
					pedido.Estado = dr.GetInt32(22);
					pedido.FormaPago = dr.GetInt32(23);
					pedido.FechaPago = dr.GetDateTime(24);
					pedido.CodUser = dr.GetInt32(25);
					pedido.FechaRegistro = dr.GetDateTime(26);
					pedido.CodCotizacion = dr.GetInt32(27);
					pedido.Nombrecliente = dr.GetString(28);
					pedido.Numeracion = dr.GetInt32(29);
					pedido.Tipoventa = dr.GetInt32(30);
					pedido.Gravadas = dr.GetDecimal(31);
					pedido.Exoneradas = dr.GetDecimal(32);
					pedido.Inafectas = dr.GetDecimal(33);
					pedido.Gratuitas = dr.GetDecimal(34);
					pedido.CodEmpresa = dr.GetInt32(35);
					pedido.Boletafactura = dr.GetInt32(36);
					pedido.CodSerie = dr.GetInt32(37);
					pedido.SerieDoc = dr.GetString(38);
					pedido.CodigoBarras = dr.GetString(39);
					pedido.CodigoBarrasCifrado = dr.GetString(40);
					pedido.CodVendedor = dr.GetInt32(41);
					pedido.Pendiente = dr.GetInt32(42);
					pedido.ventasinstock = dr.GetInt32(43);
					pedido.ValorRetencion = dr.GetInt32(44);
				}
			}
			return pedido;
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
