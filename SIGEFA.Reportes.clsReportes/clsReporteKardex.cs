using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteKardex
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataReader dr = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet kardex(DateTime fecha1, DateTime fecha2, int codPro, int codalma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteKardex", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codPro", codPro);
			cmd.Parameters.AddWithValue("codalma", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardex");
			set.WriteXml("C:\\XML\\kardexRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet StockPorAgotar(int tipo, int codalma)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("RelacionProductosStockMin", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("tipo", tipo);
			cmd.Parameters.AddWithValue("codalma", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardex");
			set.WriteXml("C:\\XML\\StockPorAgotarRPT.xml", XmlWriteMode.WriteSchema);
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

	public DataSet kardex4(DateTime fecha1, DateTime fecha2, bool tod, string refe, int codalma, int codfam, int codsuc)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteKardex4_2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("todo", tod);
			cmd.Parameters.AddWithValue("ref", refe);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codfam", codfam);
			cmd.Parameters.AddWithValue("codsuc", codsuc);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardex4");
			set.WriteXml("C:\\XML\\kardexRPT4.xml", XmlWriteMode.WriteSchema);
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

	public DataSet kardexInterno(DateTime fecha1, DateTime fecha2, bool tod, string refe, int codalma, int codfam, int codSuc)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteKardex4_interno_2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("todo", tod);
			cmd.Parameters.AddWithValue("ref", refe);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codfam", codfam);
			cmd.Parameters.AddWithValue("codsuc", codSuc);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardex4");
			set.WriteXml("C:\\XML\\kardexRPT4.xml", XmlWriteMode.WriteSchema);
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

	public DataSet kardexInternoConTransferencia(DateTime fecha1, DateTime fecha2, bool tod, string refe, int codalma, int codfam, int codSuc, bool listarTransferencias)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteKardex4_interno_2", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("todo", tod);
			cmd.Parameters.AddWithValue("ref", refe);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codfam", codfam);
			cmd.Parameters.AddWithValue("codsuc", codSuc);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardex4");
			DataColumn column = new DataColumn();
			column.DataType = Type.GetType("System.Int32");
			column.AllowDBNull = false;
			column.Caption = "listarTransferencia";
			column.ColumnName = "listarTransferencia";
			column.DefaultValue = (listarTransferencias ? 1 : 0);
			set.Tables[0].Columns.Add(column);
			foreach (DataRow item in set.Tables[0].Rows)
			{
				item.SetField("listarTransferencia", listarTransferencias ? 1 : 0);
			}
			cmd = new MySqlCommand("ReporteKardexTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("fechaini", fecha1);
			cmd.Parameters.AddWithValue("fechafin", fecha2);
			cmd.Parameters.AddWithValue("todo", tod);
			cmd.Parameters.AddWithValue("ref", refe);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codfam", codfam);
			cmd.Parameters.AddWithValue("codsuc", codSuc);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardextransferencia2");
			set.WriteXml("C:\\XML\\ReporteKardexConTransferencia.xml", XmlWriteMode.WriteSchema);
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

	public DataSet kardexTransferencia(DateTime fecha1, DateTime fecha2, bool tod, string refe, int codalma, int codfam, int codSuc)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteKardexTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = int.MaxValue;
			cmd.Parameters.AddWithValue("fechaini", fecha1);
			cmd.Parameters.AddWithValue("fechafin", fecha2);
			cmd.Parameters.AddWithValue("todo", tod);
			cmd.Parameters.AddWithValue("ref", refe);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codfam", codfam);
			cmd.Parameters.AddWithValue("codsuc", codSuc);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_kardextransferencia_s");
			set.WriteXml("C:\\XML\\kardextransferencia_s.xml", XmlWriteMode.WriteSchema);
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

	public DataSet Utilidad(DateTime fecha1, DateTime fecha2, int codalma, int codSucur)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("GastosUtilidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 20;
			cmd.Parameters.AddWithValue("codSucur", codSucur);
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma_ex", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_gastosutilidadneta");
			set.WriteXml("C:\\XML\\Utilidad.xml", XmlWriteMode.WriteSchema);
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

	public DataSet Utilidad2(DateTime fecha1, DateTime fecha2, int codalma, int codSucur)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteUtilidad", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma", codalma);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_utilidadneta");
			set.WriteXml("C:\\XML\\Utilidad2.xml", XmlWriteMode.WriteSchema);
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

	public DataSet UtilidadProducto(DateTime fecha1, DateTime fecha2, int codalma, int codSucur, int codProd)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteUtilidadProducto", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 250;
			cmd.Parameters.AddWithValue("fecha1", fecha1);
			cmd.Parameters.AddWithValue("fecha2", fecha2);
			cmd.Parameters.AddWithValue("codalma", codalma);
			cmd.Parameters.AddWithValue("codProd", codProd);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set, "dt_utilidadProducto");
			set.WriteXml("C:\\XML\\UtilidadProducto.xml", XmlWriteMode.WriteSchema);
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
