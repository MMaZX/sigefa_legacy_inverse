using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes;

internal class clsReporteTransferencias
{
	private clsConexionMysql con = new clsConexionMysql();

	private MySqlCommand cmd = null;

	private MySqlDataAdapter adap = null;

	private DataSet set = null;

	public DataSet RptTransferencia(int caso, int codAlmacen, int cod)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciaDirecta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codTranf", cod);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\RPTTransferencia.xml", XmlWriteMode.WriteSchema);
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

	public DataSet RptTransferenciaDirecta(int enviados, int caso, int codAlmacen, int cod)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciaDirectaVarios", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("estados", enviados);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codTranf", cod);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\RPTTransferenciaDirecta.xml", XmlWriteMode.WriteSchema);
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

	public DataSet RptTransferenciaFactura(int cod, int codtransfer)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ReporteFacturaTransferencia", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("cod", cod);
			cmd.Parameters.AddWithValue("codtransfer", codtransfer);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\RPTTransferenciaFactura.xml", XmlWriteMode.WriteSchema);
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

	public DataSet RptTransferenciaDirecta1(int caso, int codAlmacen, int cod, int codEmp)
	{
		try
		{
			set = new DataSet();
			con.conectarBD();
			cmd = new MySqlCommand("ListaTransferenciaDirecta", con.conector);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandTimeout = 15;
			cmd.Parameters.AddWithValue("caso", caso);
			cmd.Parameters.AddWithValue("codalma", codAlmacen);
			cmd.Parameters.AddWithValue("codTranf", cod);
			cmd.Parameters.AddWithValue("codEmp", codEmp);
			adap = new MySqlDataAdapter(cmd);
			adap.Fill(set);
			set.WriteXml("C:\\XML\\RPTTransferenciaDirecta.xml", XmlWriteMode.WriteSchema);
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
