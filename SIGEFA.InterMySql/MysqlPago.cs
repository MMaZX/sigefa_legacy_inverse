using System;
using System.Data;
using System.Transactions;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlPago : IPago
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsPago pag)
	{
		bool rpta = true;
		using TransactionScope Scope = new TransactionScope();
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codnot", Convert.ToInt32(pag.CodNota));
			oParam = cmd.Parameters.AddWithValue("codlet", Convert.ToInt32(pag.CodLetra));
			oParam = cmd.Parameters.AddWithValue("codcuopreban", Convert.ToInt32(pag.CodCuotaPreBan));
			oParam = cmd.Parameters.AddWithValue("codtipopago", pag.CodTipoPago);
			oParam = cmd.Parameters.AddWithValue("codmon", pag.CodMoneda);
			oParam = cmd.Parameters.AddWithValue("codtar", pag.CodTarjeta);
			oParam = cmd.Parameters.AddWithValue("tipo", pag.Tipo);
			oParam = cmd.Parameters.AddWithValue("ingegre", pag.IngresoEgreso);
			oParam = cmd.Parameters.AddWithValue("tipocambio", pag.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("montopa", pag.MontoPagado);
			oParam = cmd.Parameters.AddWithValue("montoco", pag.MontoCobrado);
			oParam = cmd.Parameters.AddWithValue("vuelto", pag.Vuelto);
			oParam = cmd.Parameters.AddWithValue("mora", pag.Mora);
			oParam = cmd.Parameters.AddWithValue("codalma", pag.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codcta", pag.codCtaCte);
			oParam = cmd.Parameters.AddWithValue("numcta", pag.CtaCte);
			oParam = cmd.Parameters.AddWithValue("noperacion", pag.NOperacion);
			oParam = cmd.Parameters.AddWithValue("ncheque", pag.NCheque);
			oParam = cmd.Parameters.AddWithValue("fecha", pag.FechaPago);
			oParam = cmd.Parameters.AddWithValue("observa", pag.Observacion);
			oParam = cmd.Parameters.AddWithValue("codusu", pag.CodUser);
			oParam = cmd.Parameters.AddWithValue("codban", pag.CodBanco);
			oParam = cmd.Parameters.AddWithValue("provi", pag.Provision);
			oParam = cmd.Parameters.AddWithValue("ctdadDetRet", pag.RetDet);
			oParam = cmd.Parameters.AddWithValue("tipoDetRet", pag.BanderaRetDet ?? "NAD");
			oParam = cmd.Parameters.AddWithValue("montoEnCuenta", pag.MontoEnCuenta);
			oParam = cmd.Parameters.AddWithValue("opcionSuma", pag.OpcionSuma);
			if (pag.CodNotaCredito != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codnotac", pag.CodNotaCredito);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codnotac", null);
			}
			if (pag.NotaCredito != 0)
			{
				oParam = cmd.Parameters.AddWithValue("notacre", pag.NotaCredito);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("notacre", 0);
			}
			if (pag.CodSerie != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codserie", pag.CodSerie);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codserie", null);
			}
			if (pag.Serie != "")
			{
				oParam = cmd.Parameters.AddWithValue("serie", pag.Serie);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("serie", null);
			}
			if (pag.NumDoc != "")
			{
				oParam = cmd.Parameters.AddWithValue("numdoc", pag.NumDoc);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("numdoc", null);
			}
			oParam = cmd.Parameters.AddWithValue("aprob", pag.Aprobado);
			if (pag.Referencia != "")
			{
				oParam = cmd.Parameters.AddWithValue("ref", pag.Referencia);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("ref", null);
			}
			if (pag.CodDoc != 0)
			{
				oParam = cmd.Parameters.AddWithValue("coddoc", pag.CodDoc);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("coddoc", 0);
			}
			oParam = cmd.Parameters.AddWithValue("codsucur", pag.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codCaja_ex", pag.Codcaja);
			oParam = cmd.Parameters.AddWithValue("tipo_descripcion", pag.TipoDescripcion);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pag.CodPago = Convert.ToInt32(cmd.Parameters["newid"].Value);
			if (x == 0)
			{
				rpta = false;
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

	public bool InsertPagoPendiente(clsPago pag)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPagoPendiente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnot", Convert.ToInt32(pag.CodNota));
			cmd.Parameters.AddWithValue("codlet", Convert.ToInt32(pag.CodLetra));
			cmd.Parameters.AddWithValue("codcuopreban", Convert.ToInt32(pag.CodCuotaPreBan));
			cmd.Parameters.AddWithValue("codtipopago", pag.CodTipoPago);
			cmd.Parameters.AddWithValue("codmon", pag.CodMoneda);
			cmd.Parameters.AddWithValue("codtar", pag.CodTarjeta);
			cmd.Parameters.AddWithValue("tipo", pag.Tipo);
			cmd.Parameters.AddWithValue("ingegre", pag.IngresoEgreso);
			cmd.Parameters.AddWithValue("tipocambio", pag.TipoCambio);
			cmd.Parameters.AddWithValue("montopa", pag.MontoPagado);
			cmd.Parameters.AddWithValue("montoco", pag.MontoCobrado);
			cmd.Parameters.AddWithValue("vuelto", pag.Vuelto);
			cmd.Parameters.AddWithValue("mora", pag.Mora);
			cmd.Parameters.AddWithValue("codalma", pag.CodAlmacen);
			cmd.Parameters.AddWithValue("codcta", pag.codCtaCte);
			cmd.Parameters.AddWithValue("numcta", pag.CtaCte);
			cmd.Parameters.AddWithValue("noperacion", pag.NOperacion);
			cmd.Parameters.AddWithValue("ncheque", pag.NCheque);
			cmd.Parameters.AddWithValue("fecha", pag.FechaPago);
			cmd.Parameters.AddWithValue("observa", pag.Observacion);
			cmd.Parameters.AddWithValue("codusu", pag.CodUser);
			cmd.Parameters.AddWithValue("codban", pag.CodBanco);
			cmd.Parameters.AddWithValue("provi", pag.Provision);
			cmd.Parameters.AddWithValue("ctdadDetRet", pag.RetDet);
			cmd.Parameters.AddWithValue("tipoDetRet", pag.BanderaRetDet ?? "NAD");
			cmd.Parameters.AddWithValue("montoEnCuenta", pag.MontoEnCuenta);
			cmd.Parameters.AddWithValue("opcionSuma", pag.OpcionSuma);
			if (pag.CodNotaCredito != 0)
			{
				cmd.Parameters.AddWithValue("codnotac", pag.CodNotaCredito);
			}
			else
			{
				cmd.Parameters.AddWithValue("codnotac", null);
			}
			if (pag.NotaCredito != 0)
			{
				cmd.Parameters.AddWithValue("notacre", pag.NotaCredito);
			}
			else
			{
				cmd.Parameters.AddWithValue("notacre", 0);
			}
			if (pag.CodSerie != 0)
			{
				cmd.Parameters.AddWithValue("codserie", pag.CodSerie);
			}
			else
			{
				cmd.Parameters.AddWithValue("codserie", null);
			}
			if (pag.Serie != "")
			{
				cmd.Parameters.AddWithValue("serie", pag.Serie);
			}
			else
			{
				cmd.Parameters.AddWithValue("serie", null);
			}
			if (pag.NumDoc != "")
			{
				cmd.Parameters.AddWithValue("numdoc", pag.NumDoc);
			}
			else
			{
				cmd.Parameters.AddWithValue("numdoc", null);
			}
			cmd.Parameters.AddWithValue("aprob", pag.Aprobado);
			if (pag.Referencia != "")
			{
				cmd.Parameters.AddWithValue("ref", pag.Referencia);
			}
			else
			{
				cmd.Parameters.AddWithValue("ref", null);
			}
			if (pag.CodDoc != 0)
			{
				cmd.Parameters.AddWithValue("coddoc", pag.CodDoc);
			}
			else
			{
				cmd.Parameters.AddWithValue("coddoc", 0);
			}
			cmd.Parameters.AddWithValue("codsucur", pag.CodSucursal);
			cmd.Parameters.AddWithValue("codCaja_ex", pag.Codcaja);
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

	public bool Insertpagomultiple(clsPago pag)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codnot", Convert.ToInt32(pag.CodNota));
			oParam = cmd.Parameters.AddWithValue("codlet", Convert.ToInt32(pag.CodLetra));
			oParam = cmd.Parameters.AddWithValue("codcuopreban", Convert.ToInt32(pag.CodCuotaPreBan));
			oParam = cmd.Parameters.AddWithValue("codtipopago", pag.CodTipoPago);
			oParam = cmd.Parameters.AddWithValue("codmon", pag.CodMoneda);
			oParam = cmd.Parameters.AddWithValue("codtar", pag.CodTarjeta);
			oParam = cmd.Parameters.AddWithValue("tipo", pag.Tipo);
			oParam = cmd.Parameters.AddWithValue("ingegre", pag.IngresoEgreso);
			oParam = cmd.Parameters.AddWithValue("tipocambio", pag.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("montopa", pag.MontoPagado);
			oParam = cmd.Parameters.AddWithValue("montoco", pag.MontoCobrado);
			oParam = cmd.Parameters.AddWithValue("vuelto", pag.Vuelto);
			oParam = cmd.Parameters.AddWithValue("mora", pag.Mora);
			oParam = cmd.Parameters.AddWithValue("codalma", pag.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codcta", pag.codCtaCte);
			oParam = cmd.Parameters.AddWithValue("numcta", pag.CtaCte);
			oParam = cmd.Parameters.AddWithValue("noperacion", pag.NOperacion);
			oParam = cmd.Parameters.AddWithValue("ncheque", pag.NCheque);
			oParam = cmd.Parameters.AddWithValue("fecha", pag.FechaPago);
			oParam = cmd.Parameters.AddWithValue("observa", pag.Observacion);
			oParam = cmd.Parameters.AddWithValue("codusu", pag.CodUser);
			oParam = cmd.Parameters.AddWithValue("codban", pag.CodBanco);
			oParam = cmd.Parameters.AddWithValue("provi", pag.Provision);
			if (pag.CodNotaCredito != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codnotac", pag.CodNotaCredito);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codnotac", null);
			}
			if (pag.NotaCredito != 0)
			{
				oParam = cmd.Parameters.AddWithValue("notacre", pag.NotaCredito);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("notacre", 0);
			}
			if (pag.CodSerie != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codserie", pag.CodSerie);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codserie", null);
			}
			if (pag.Serie != "")
			{
				oParam = cmd.Parameters.AddWithValue("serie", pag.Serie);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("serie", null);
			}
			if (pag.NumDoc != "")
			{
				oParam = cmd.Parameters.AddWithValue("numdoc", pag.NumDoc);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("numdoc", null);
			}
			oParam = cmd.Parameters.AddWithValue("aprob", pag.Aprobado);
			if (pag.Referencia != "")
			{
				oParam = cmd.Parameters.AddWithValue("ref", pag.Referencia);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("ref", null);
			}
			if (pag.CodDoc != 0)
			{
				oParam = cmd.Parameters.AddWithValue("coddoc", pag.CodDoc);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("coddoc", null);
			}
			oParam = cmd.Parameters.AddWithValue("codsucur", pag.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codCaja_ex", pag.Codcaja);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pag.CodPago = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public int obtenerOpcionSuma(int codpago)
	{
		int opcion = 0;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("obtenerOpcionSumaPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codpago", codpago);
			return Convert.ToInt32(cmd.ExecuteScalar() ?? ((object)0));
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

	public bool InsertPagoDetraccion(clsPago pag)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaPagoDetraccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codnot", Convert.ToInt32(pag.CodNota));
			oParam = cmd.Parameters.AddWithValue("codlet", Convert.ToInt32(pag.CodLetra));
			if (pag.CodTipoPago != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codtipopago", pag.CodTipoPago);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codtipopago", null);
			}
			oParam = cmd.Parameters.AddWithValue("codmon", pag.CodMoneda);
			oParam = cmd.Parameters.AddWithValue("codtar", pag.CodTarjeta);
			oParam = cmd.Parameters.AddWithValue("tipo", pag.Tipo);
			oParam = cmd.Parameters.AddWithValue("ingegre", pag.IngresoEgreso);
			oParam = cmd.Parameters.AddWithValue("tipocambio", pag.TipoCambio);
			oParam = cmd.Parameters.AddWithValue("montopa", pag.MontoPagado);
			oParam = cmd.Parameters.AddWithValue("montoco", pag.MontoCobrado);
			oParam = cmd.Parameters.AddWithValue("vuelto", pag.Vuelto);
			oParam = cmd.Parameters.AddWithValue("codalma", pag.CodAlmacen);
			oParam = cmd.Parameters.AddWithValue("codcta", pag.codCtaCte);
			oParam = cmd.Parameters.AddWithValue("numcta", pag.CtaCte);
			oParam = cmd.Parameters.AddWithValue("noperacion", pag.NOperacion);
			oParam = cmd.Parameters.AddWithValue("ncheque", pag.NCheque);
			oParam = cmd.Parameters.AddWithValue("fecha", pag.FechaPago);
			oParam = cmd.Parameters.AddWithValue("observa", pag.Observacion);
			oParam = cmd.Parameters.AddWithValue("codusu", pag.CodUser);
			oParam = cmd.Parameters.AddWithValue("codban", pag.CodBanco);
			oParam = cmd.Parameters.AddWithValue("provi", pag.Provision);
			oParam = cmd.Parameters.AddWithValue("pend", pag.Pendiente);
			if (pag.CodSerie != 0)
			{
				oParam = cmd.Parameters.AddWithValue("codserie", pag.CodSerie);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("codserie", null);
			}
			if (pag.Serie != "")
			{
				oParam = cmd.Parameters.AddWithValue("serie", pag.Serie);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("serie", null);
			}
			if (pag.NumDoc != "")
			{
				oParam = cmd.Parameters.AddWithValue("numdoc", pag.NumDoc);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("numdoc", null);
			}
			oParam = cmd.Parameters.AddWithValue("aprob", pag.Aprobado);
			if (pag.Referencia != "")
			{
				oParam = cmd.Parameters.AddWithValue("ref", pag.Referencia);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("ref", null);
			}
			if (pag.CodDoc != 0)
			{
				oParam = cmd.Parameters.AddWithValue("coddoc", pag.CodDoc);
			}
			else
			{
				oParam = cmd.Parameters.AddWithValue("coddoc", null);
			}
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			pag.CodPago = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public DataTable MuestraListaPagosNota(int CodNotaIngreso, bool InOut, int Tipo, int codAlmacen)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPagosPorNota", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnot", CodNotaIngreso);
			cmd.Parameters.AddWithValue("ingegre", InOut);
			cmd.Parameters.AddWithValue("tipo", Tipo);
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

	public bool AnularPago(int CodigoPago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpag", CodigoPago);
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

	public bool AnularPagoPendiente(int CodigoPago)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularPagoPendiente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codpag", CodigoPago);
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

	public clsPago MuestraPagoVenta(int codAlmacen, int venta)
	{
		clsPago pag = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPagoVenta", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codnota", venta);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					pag = new clsPago();
					pag.CodPago = Convert.ToInt32(dr.GetDecimal(0));
					pag.CodNota = dr.GetString(1);
					pag.MontoCobrado = dr.GetDecimal(2);
					pag.MontoPagado = dr.GetDecimal(3);
					pag.FechaPago = dr.GetDateTime(4);
					pag.CodTipoPago = Convert.ToInt32(dr.GetDecimal(5));
					pag.TipoCambio = dr.GetDecimal(6);
					pag.CodCobrador = Convert.ToInt32(dr.GetDecimal(7));
					pag.codCtaCte = Convert.ToInt32(dr.GetDecimal(8));
					pag.CodTarjeta = Convert.ToInt32(dr.GetDecimal(9));
					pag.CodBanco = Convert.ToInt32(dr.GetDecimal(10));
					pag.NOperacion = dr.GetString(11);
					pag.CtaCte = dr.GetString(12);
					pag.NCheque = dr.GetString(13);
					pag.Observacion = dr.GetString(14);
				}
			}
			return pag;
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

	public DataTable MuestraPagoVentaAnula(int codAlmacen, int nota)
	{
		clsPago pag = null;
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraPagoVentaAnula", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("nota", nota);
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

	public DataTable MuestraPagosPorAprobar(int Estado, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("pagoPorAprobarTesoreria", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("estado", Estado);
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

	public DataTable MuestraPagosDetraccion(int Estado, DateTime Fecha1, DateTime Fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("pagoDetraccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("estado", Estado);
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

	public bool AprobarPago(int codigo, int valor)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AprobarPago", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("cod", codigo);
			cmd.Parameters.AddWithValue("valor", valor);
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

	public bool VerificaDetraccionPendiente(int codigo)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("VerificaDetraccionPendiente", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codFactura", codigo);
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

	public DataTable MuestraListaPagosNota2(int CodNotaIngreso, bool InOut, int Tipo)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("MuestraListaPagosPorNota2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codnot", CodNotaIngreso);
			cmd.Parameters.AddWithValue("ingegre", InOut);
			cmd.Parameters.AddWithValue("tipo", Tipo);
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

	public bool ActualizaPagoAprobado(string ser, string numdoc, int codpag)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaPagoAprobado", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("ser", ser);
			cmd.Parameters.AddWithValue("numdoc", numdoc);
			cmd.Parameters.AddWithValue("codpag", codpag);
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

	public bool ActualizarSeleccion(int codigo, string color)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizarSeleccion", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("_codigo", codigo);
			cmd.Parameters.AddWithValue("_color", color);
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

	public DataTable GetPagosVenta(int codigoalmacen, int codigoventa)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("GetPagosVenta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codNota_ex", codigoventa);
			cmd.Parameters.AddWithValue("codAlmacen_ex", codigoalmacen);
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
