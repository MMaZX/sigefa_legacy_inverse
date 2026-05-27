using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlNotaDebito : INotaDebito
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool insert(clsNotaDebito nota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaNotaDebito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("docnota", nota.DocumentoNotaDebito);
			oParam = cmd.Parameters.AddWithValue("codtran", nota.CodTipoTransaccion);
			oParam = cmd.Parameters.AddWithValue("codtipo", nota.CodTipoDocumento);
			oParam = cmd.Parameters.AddWithValue("numdoc", nota.NumFac);
			oParam = cmd.Parameters.AddWithValue("moneda", nota.Moneda);
			oParam = cmd.Parameters.AddWithValue("tipocambio", nota.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("fechaingreso", nota.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("comentario", nota.Comentario);
			oParam = cmd.Parameters.AddWithValue("bruto", nota.MontoBruto);
			oParam = cmd.Parameters.AddWithValue("montodscto", nota.MontoDscto);
			oParam = cmd.Parameters.AddWithValue("igv", nota.Igv);
			oParam = cmd.Parameters.AddWithValue("flete", nota.Flete);
			oParam = cmd.Parameters.AddWithValue("total", nota.Total);
			oParam = cmd.Parameters.AddWithValue("pendiente", nota.Total);
			oParam = cmd.Parameters.AddWithValue("estado", nota.Estado);
			oParam = cmd.Parameters.AddWithValue("recibido", nota.Recibido);
			if (nota.FormaPago != 0)
			{
				oParam = cmd.Parameters.AddWithValue("formapago", nota.FormaPago);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("formapago", null);
			}
			oParam = cmd.Parameters.AddWithValue("fechapago", nota.FechaPago);
			oParam = cmd.Parameters.AddWithValue("fechacancelado", nota.FechaCancelado);
			oParam = cmd.Parameters.AddWithValue("cancelado", nota.Cancelado);
			oParam = cmd.Parameters.AddWithValue("codusu", nota.CodUser);
			oParam = cmd.Parameters.AddWithValue("codref", nota.CodReferencia);
			oParam = cmd.Parameters.AddWithValue("codser", nota.CodSerie);
			oParam = cmd.Parameters.AddWithValue("serie", nota.Serie);
			oParam = cmd.Parameters.AddWithValue("codcli", nota.CodCliente);
			oParam = cmd.Parameters.AddWithValue("codalma", nota.CodAlmacen);
			if (nota.Motivo != "")
			{
				cmd.Parameters.AddWithValue("motiv", nota.Motivo);
			}
			else
			{
				cmd.Parameters.AddWithValue("motiv", null);
			}
			oParam = cmd.Parameters.AddWithValue("codMovi", nota.MovimientoNC);
			oParam = cmd.Parameters.AddWithValue("gravadas_ex", nota.Gravadas);
			oParam = cmd.Parameters.AddWithValue("exoneradas_ex", nota.Exoneradas);
			oParam = cmd.Parameters.AddWithValue("inafectas_ex", nota.Inafectas);
			oParam = cmd.Parameters.AddWithValue("gratuitas_ex", nota.Gratuitas);
			oParam = cmd.Parameters.AddWithValue("tipofacturacion_ex", nota.Tipofacturacion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			nota.CodNotaDebitoNueva = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool insertdetalle(clsDetalleNotaDebito Detalle)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaDetalleNotaDebito", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codpro", Detalle.CodProducto);
			oParam = cmd.Parameters.AddWithValue("codnotadebito", Detalle.CodNotaDebito);
			if (Detalle.CodNotaIngreso != "")
			{
				cmd.Parameters.AddWithValue("codnota", Detalle.CodNotaIngreso);
			}
			else
			{
				cmd.Parameters.AddWithValue("codnota", null);
			}
			oParam = cmd.Parameters.AddWithValue("moneda", Detalle.Moneda);
			oParam = cmd.Parameters.AddWithValue("unidad", Detalle.UnidadIngresada);
			oParam = cmd.Parameters.AddWithValue("serielote", Detalle.SerieLote);
			oParam = cmd.Parameters.AddWithValue("precio", Detalle.PrecioUnitario);
			oParam = cmd.Parameters.AddWithValue("subtotal", Detalle.Subtotal);
			oParam = cmd.Parameters.AddWithValue("dscto1", Detalle.Descuento1);
			oParam = cmd.Parameters.AddWithValue("dscto2", Detalle.Descuento2);
			oParam = cmd.Parameters.AddWithValue("dscto3", Detalle.Descuento3);
			oParam = cmd.Parameters.AddWithValue("montodscto", Detalle.MontoDescuento);
			oParam = cmd.Parameters.AddWithValue("igv", Detalle.Igv);
			oParam = cmd.Parameters.AddWithValue("flete", Detalle.Flete);
			oParam = cmd.Parameters.AddWithValue("importe", Detalle.Importe);
			oParam = cmd.Parameters.AddWithValue("precioreal", Detalle.PrecioReal);
			oParam = cmd.Parameters.AddWithValue("valoreal", Detalle.ValoReal);
			oParam = cmd.Parameters.AddWithValue("fecha", Detalle.FechaIngreso);
			oParam = cmd.Parameters.AddWithValue("codusu", Detalle.CodUser);
			oParam = cmd.Parameters.AddWithValue("codalma", Detalle.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("cant", Detalle.Cantidad);
			oParam = cmd.Parameters.AddWithValue("descrip", Detalle.DescripcionND);
			oParam = cmd.Parameters.AddWithValue("tipoimpuesto_ex", Detalle.Tipoimpuesto);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			Detalle.CodDetalleNotaDebito = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public bool actualizarCodNotaDebitoFV(int codFactura_venta, int codNota)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizarCodNodebitoFV", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codfac", codFactura_venta);
			oParam = cmd.Parameters.AddWithValue("codnota", codNota);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
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
