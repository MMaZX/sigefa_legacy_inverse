using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql;

internal class MysqlAperturaCierreCaja : IAperturaCierre
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataTable tabla = null;

	public bool Insert(clsCaja aper)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaApertura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("montoape", aper.Montoapertura);
			oParam = cmd.Parameters.AddWithValue("codusu", aper.CodUser);
			oParam = cmd.Parameters.AddWithValue("codalma", aper.Codsucursal);
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

	public bool UpdateApertura(clsCaja aper)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaApertura", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("mont", aper.Montoapertura);
			cmd.Parameters.AddWithValue("codalma", aper.Codsucursal);
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

	public bool UpdateCierre(clsCaja aper)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ActualizaCierreCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("montocie", aper.Montocierre);
			cmd.Parameters.AddWithValue("codusu", aper.CodUser);
			cmd.Parameters.AddWithValue("codalma", aper.Codsucursal);
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

	public bool AnularCierre(int codAlmacen)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("AnularCierreCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

	public clsCaja CargaAperturaCaja(int codAlmacen)
	{
		clsCaja aper = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraAperturaCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alma", codAlmacen);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					aper = new clsCaja();
					aper.Montoapertura = Convert.ToDecimal(dr.GetDecimal(0));
					aper.Montocierre = Convert.ToDecimal(dr.GetDecimal(1));
				}
			}
			return aper;
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

	public clsCaja CargaCierreCaja(int codAlmacen)
	{
		clsCaja aper = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCierreCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("alma", codAlmacen);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					aper = new clsCaja();
					aper.Montoapertura = Convert.ToDecimal(dr.GetDecimal(0));
					aper.Montocierre = Convert.ToDecimal(dr.GetDecimal(1));
				}
			}
			return aper;
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

	public clsCaja GetUltimaCajaVentas(int codsucursal, int tipocaja, int codalma)
	{
		clsCaja aper = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GetUltimaCajaVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codsucursal_ex", codsucursal);
			cmd.Parameters.AddWithValue("tipo_ex", tipocaja);
			cmd.Parameters.AddWithValue("codalmacen_ex", codalma);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					aper = new clsCaja();
					aper.Codcaja = dr.GetInt32(0);
					aper.Montoapertura = dr.GetDecimal(1);
					aper.Fechaapertura = dr.GetDateTime(2).Date;
					aper.Montocierre = dr.GetDecimal(3);
					aper.Fechacierre = dr.GetDateTime(4).Date;
					aper.Estado = dr.GetBoolean(5);
					aper.CodUser = dr.GetInt32(6);
					aper.Codsucursal = dr.GetInt32(7);
				}
			}
			return aper;
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

	public clsCaja ValidarAperturaDia(int codSucursal, DateTime fecha1, int tipocaja, int codalma, int coduser)
	{
		clsCaja aper = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ValidarAperturaDia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codsuc", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("tipo_ex", tipocaja);
			cmd.Parameters.AddWithValue("codalmacen_ex", codalma);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					if (!dr.IsDBNull(0))
					{
						aper = new clsCaja();
						if (dr.IsDBNull(0))
						{
							throw new Exception("No se encontro una caja aperturada para la fecha: " + fecha1.ToShortDateString());
						}
						aper.Codcaja = dr.GetInt32(0);
						aper.Montoapertura = dr.GetDecimal(1);
						aper.Fechaapertura = dr.GetDateTime(2);
						aper.Estado = dr.GetBoolean(3);
						aper.CodUser = dr.GetInt32(4);
						aper.Codsucursal = dr.GetInt32(5);
						aper.TotalIngreso = dr.GetDecimal(6);
						aper.TotalEgreso = dr.GetDecimal(7);
						aper.TotalVentaEfectivo = dr.GetDecimal(8);
						aper.Totalventacredito = dr.GetDecimal(9);
						aper.Totaldeposito = dr.GetDecimal(10);
						aper.Totalcheque = dr.GetDecimal(11);
						aper.Totaltarnsferencia = dr.GetDecimal(12);
						aper.Totalcobranza = dr.GetDecimal(13);
						aper.TotalDisponible = dr.GetDecimal(14);
						aper.TotalPendiente = dr.GetDecimal(15);
						aper.IngresoEfectivo = ((dr["ingreso_efectivo"] == DBNull.Value) ? 0.00m : Convert.ToDecimal(dr["ingreso_efectivo"]));
						aper.IngresoTarjeta = ((dr["ingreso_tarjeta"] == DBNull.Value) ? 0.00m : Convert.ToDecimal(dr["ingreso_tarjeta"]));
						aper.IngresoTransferencia = ((dr["ingreso_transferencia"] == DBNull.Value) ? 0.00m : Convert.ToDecimal(dr["ingreso_transferencia"]));
						aper.EgresoEfectivo = ((dr["egreso_efectivo"] == DBNull.Value) ? 0.00m : Convert.ToDecimal(dr["egreso_efectivo"]));
					}
				}
			}
			return aper;
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

	public clsCaja ListaTotalesVentas(int codSucursal, DateTime fechaInicio, DateTime fechaFin, int tipocaja, int codalma, int coduser)
	{
		clsCaja aper = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("TotalesVentas", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codsuc", codSucursal);
			cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
			cmd.Parameters.AddWithValue("fechaFin", fechaFin);
			cmd.Parameters.AddWithValue("tipo_ex", tipocaja);
			cmd.Parameters.AddWithValue("codalmacen_ex", codalma);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					aper = new clsCaja();
					aper.Codcaja = dr.GetInt32(0);
					aper.Montoapertura = dr.GetDecimal(1);
					aper.Fechaapertura = dr.GetDateTime(2);
					aper.Estado = dr.GetBoolean(3);
					aper.CodUser = dr.GetInt32(4);
					aper.Codsucursal = dr.GetInt32(5);
					aper.TotalIngreso = dr.GetDecimal(6);
					aper.TotalEgreso = dr.GetDecimal(7);
					aper.TotalVentaEfectivo = dr.GetDecimal(8);
					aper.Totalventacredito = dr.GetDecimal(9);
					aper.Totaldeposito = dr.GetDecimal(10);
					aper.Totalcheque = dr.GetDecimal(11);
					aper.Totaltarnsferencia = dr.GetDecimal(12);
					aper.Totalcobranza = dr.GetDecimal(13);
					aper.TotalDisponible = dr.GetDecimal(14);
					aper.TotalPendiente = dr.GetDecimal(15);
				}
			}
			return aper;
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

	public bool InsertAperturaCaja(clsCaja caja)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaAperturaCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codsucursal_ex", caja.Codsucursal);
			oParam = cmd.Parameters.AddWithValue("tipo_ex", caja.Tipo);
			oParam = cmd.Parameters.AddWithValue("montoapertura_ex", caja.Montoapertura);
			oParam = cmd.Parameters.AddWithValue("montocierre_ex", caja.Montocierre);
			oParam = cmd.Parameters.AddWithValue("totalIngreso_ex", caja.TotalIngreso);
			oParam = cmd.Parameters.AddWithValue("totalEgreso_ex", caja.TotalEgreso);
			oParam = cmd.Parameters.AddWithValue("totalVentaEfectivo_ex", caja.TotalVentaEfectivo);
			oParam = cmd.Parameters.AddWithValue("totalDisponible_ex", caja.TotalDisponible);
			oParam = cmd.Parameters.AddWithValue("codUser_ex", caja.CodUser);
			oParam = cmd.Parameters.AddWithValue("codalmacen_ex", caja.Codalmacen);
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

	public clsCaja CargaCierreAnterior(int iCodSucursal, int tipocaja)
	{
		clsCaja aper = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("MuestraCierreAnterior", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codsuc", iCodSucursal);
			cmd.Parameters.AddWithValue("tipo_ex", tipocaja);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					aper = new clsCaja();
					aper.Montocierre = Convert.ToDecimal(dr.GetDecimal(0));
				}
			}
			return aper;
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

	public DataTable ListaCierresDiarios(int codSucursal, DateTime desde, DateTime hasta)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCierresCaja", con.conector);
			cmd.Parameters.AddWithValue("codsuc", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", desde);
			cmd.Parameters.AddWithValue("fecha2", hasta);
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

	public decimal SumaVentaEfectivoCaja(int codSuc, DateTime fech1, int codigocaja)
	{
		decimal total = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SumaVentasEfectivoDia", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSuc);
			cmd.Parameters.AddWithValue("fecha1", fech1);
			cmd.Parameters.AddWithValue("codcaja_ex", codigocaja);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					total = Convert.ToDecimal(dr.GetDecimal(0));
				}
			}
			return total;
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

	public decimal SumaVentasEfectivoCaja(int codSuc, DateTime fechaDesde, DateTime fechaHasta, int codigocaja)
	{
		decimal total = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("SumaVentasEfectivo", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSuc);
			cmd.Parameters.AddWithValue("fechaDesde", fechaDesde);
			cmd.Parameters.AddWithValue("fechaHasta", fechaHasta);
			cmd.Parameters.AddWithValue("codcaja_ex", codigocaja);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					total = Convert.ToDecimal(dr.GetDecimal(0));
				}
			}
			return total;
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

	public DataTable ListaCajaDiaria(int codSucursal, DateTime fecha1, int codigocaja, int codalma, int codEstadoCaja)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCajaChicaDiaria", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("codcaja_ex", codigocaja);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("estado_caja", codEstadoCaja);
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

	public bool CerrarCajaVentas(int codSucursal, DateTime fecha1, int codcaja, int codalma)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("CerrarCajaDiaria", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("codcaja_ex", codcaja);
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

	public bool InsertMovCajaChica(clsCajaChicaMov movch)
	{
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("GuardaMovimientoCajaChica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			MySqlParameter oParam = cmd.Parameters.AddWithValue("codSucursal_ex", movch.CodSucursal);
			oParam = cmd.Parameters.AddWithValue("codcaja_ex", movch.Codcaja);
			oParam = cmd.Parameters.AddWithValue("codPago_ex", movch.CodPago);
			oParam = cmd.Parameters.AddWithValue("concepto_ex", movch.Concepto);
			oParam = cmd.Parameters.AddWithValue("monto_ex", movch.Monto);
			oParam = cmd.Parameters.AddWithValue("nombre_ex", movch.Nombre);
			oParam = cmd.Parameters.AddWithValue("dni_ex", movch.Dni);
			oParam = cmd.Parameters.AddWithValue("tipo_ex", movch.Tipo);
			oParam = cmd.Parameters.AddWithValue("tipomovimiento_ex", movch.Tipomovimiento);
			oParam = cmd.Parameters.AddWithValue("fecha_ex", movch.Fecha);
			oParam = cmd.Parameters.AddWithValue("tipodocumento_ex", movch.Tipodocumento);
			oParam = cmd.Parameters.AddWithValue("codSerie_ex", movch.CodSerie);
			oParam = cmd.Parameters.AddWithValue("serie_ex", movch.Serie);
			oParam = cmd.Parameters.AddWithValue("NumDocumento_ex", movch.NumDocumento1);
			oParam = cmd.Parameters.AddWithValue("toneladas_ex", movch.Toneladas);
			oParam = cmd.Parameters.AddWithValue("codTipoPagoCaja_ex", movch.CodTipoPagoCaja);
			oParam = cmd.Parameters.AddWithValue("codUser_ex", movch.CodUser);
			oParam = cmd.Parameters.AddWithValue("codalmacen_ex", movch.Codalmacen);
			oParam = cmd.Parameters.AddWithValue("codMoneda_ex", movch.Codmoneda);
			oParam = cmd.Parameters.AddWithValue("tcVenta_ex", movch.Tcventa);
			oParam = cmd.Parameters.AddWithValue("newid", 0);
			oParam.Direction = ParameterDirection.Output;
			int x = cmd.ExecuteNonQuery();
			movch.CodMovCaja = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

	public DataTable ListaCajaChica(int codSucursal, DateTime fecha1, int codigocaja, int codalma)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListaCajaChica", con.conector);
			cmd.Parameters.AddWithValue("codSucur", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("codcaja_ex", codigocaja);
			cmd.Parameters.AddWithValue("codalma_ex", codalma);
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

	public decimal traersaldo()
	{
		decimal saldo = default(decimal);
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("traersaldoCajaChica", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					saldo = Convert.ToDecimal(dr.GetDecimal(0));
				}
			}
			return saldo;
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

	public DataTable ConsultaCajas(int almacen, DateTime fecha1, DateTime fecha2)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("ListadoCajas", con.conector);
			cmd.Parameters.AddWithValue("alma", almacen);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
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

	public clsCaja GetCaja(int codSucursal, DateTime fecha1, int tipocaja, int codalma)
	{
		clsCaja caja = null;
		try
		{
			con.conectarBD();
			cmd = new MySqlCommand("ObtenerCaja", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("codsuc", codSucursal);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("tipo_ex", tipocaja);
			cmd.Parameters.AddWithValue("codalmacen_ex", codalma);
			dr = cmd.ExecuteReader();
			if (dr.HasRows)
			{
				while (dr.Read())
				{
					caja = new clsCaja();
					if (dr.IsDBNull(0))
					{
						throw new Exception("No se encontro una caja registrada para la fecha: " + fecha1.Date.ToShortDateString());
					}
					caja.Codcaja = dr.GetInt32(0);
					caja.Montoapertura = dr.GetDecimal(1);
					caja.Fechaapertura = dr.GetDateTime(2);
					caja.Estado = dr.GetBoolean(3);
					caja.CodUser = dr.GetInt32(4);
					caja.Codsucursal = dr.GetInt32(5);
					caja.TotalIngreso = dr.GetDecimal(6);
					caja.TotalEgreso = dr.GetDecimal(7);
					caja.TotalVentaEfectivo = dr.GetDecimal(8);
					caja.Totalventacredito = dr.GetDecimal(9);
					caja.Totaldeposito = dr.GetDecimal(10);
					caja.Totalcheque = dr.GetDecimal(11);
					caja.Totaltarnsferencia = dr.GetDecimal(12);
					caja.Totalcobranza = dr.GetDecimal(13);
					caja.TotalDisponible = dr.GetDecimal(14);
					caja.Fechacierre = dr.GetDateTime(15);
					caja.TotalPendiente = dr.GetDecimal(16);
				}
			}
			return caja;
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

	public DataTable getVentasNoEstanEnCajaMovimientos(DateTime fechaCaja, int codAlmacen, int iCodSucursal, int codCaja)
	{
		try
		{
			tabla = new DataTable();
			con.conectarBD();
			cmd = new MySqlCommand("VentasQueNoAparecenEnCajaMovimientoDia", con.conector);
			cmd.Parameters.AddWithValue("_fecha1", fechaCaja);
			cmd.Parameters.AddWithValue("_codAlmacen", codAlmacen);
			cmd.Parameters.AddWithValue("_codSucursal", iCodSucursal);
			cmd.Parameters.AddWithValue("_codCaja", codCaja);
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
