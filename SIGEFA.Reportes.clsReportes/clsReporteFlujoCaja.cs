using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteFlujoCaja
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet ReporteFlujoCaja(DateTime fecha1, DateTime fecha2, int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("Rprte_FlujoCaja", con.conector);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "FlujoCaja.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReportePagosFacturaVenta(int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportPagosFacturaVenta", con.conector);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "PagosFacturaVenta.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteMovimientosBancarios(DateTime fecha1, DateTime fecha2, int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportMovimientosBancarios", con.conector);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "MovimientosBancarios.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteLiquidacionCaja(DateTime fecha1, DateTime fecha2, int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportLiquidacionCajaDia", con.conector);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "LiquidacionCajaDia.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteImpresionPago(int codPago, int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ImprimirPago", con.conector);
			cmd.Parameters.AddWithValue("codpag", codPago);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "ImpresionPago.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteImpresionCobro(int codPago, int codAlmacen)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ImprimirCobro", con.conector);
			cmd.Parameters.AddWithValue("codpag", codPago);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "ImprimirCobro.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteLiquidacionCaja(int codSucursal, DateTime fecha)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportLiquidacion", con.conector);
			cmd.Parameters.AddWithValue("sucur", codSucursal);
			cmd.Parameters.AddWithValue("fec", fecha);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "LiquidacionCajaRP.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteLiquidacionCaja2(int codSucursal, DateTime fecha)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportLiquidacion2", con.conector);
			cmd.Parameters.AddWithValue("sucur", codSucursal);
			cmd.Parameters.AddWithValue("fec", fecha);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "LiquidacionCajaRP2.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReporteLiquidacionCaja3(int codSucursal, DateTime fecha)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportLiquidacion3", con.conector);
			cmd.Parameters.AddWithValue("sucur", codSucursal);
			cmd.Parameters.AddWithValue("fec", fecha);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "LiquidacionCajaRP3.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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

	public DataSet ReportLiquidacion(int codSucursal, DateTime fecha)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReportLiquidacionCaja", con.conector);
			cmd.Parameters.AddWithValue("sucur", codSucursal);
			cmd.Parameters.AddWithValue("fec", fecha);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			string archivo = "RptLiquidacionCaja.xml";
			string carpeta = Path.GetDirectoryName("C:\\XML\\");
			if (!Directory.Exists(carpeta))
			{
				Directory.CreateDirectory(carpeta);
			}
			if (Directory.Exists(carpeta))
			{
				if (File.Exists(carpeta + "\\" + archivo))
				{
					File.Delete(carpeta + "\\" + archivo);
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
				else
				{
					set.WriteXml("C:\\XML\\" + archivo, XmlWriteMode.WriteSchema);
				}
			}
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
