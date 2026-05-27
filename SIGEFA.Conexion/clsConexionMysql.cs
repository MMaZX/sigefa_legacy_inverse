using System;
using System.Configuration;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SIGEFA.Conexion;

internal class clsConexionMysql
{
	public MySqlConnection conector = null;

	public static string sConex = ConfigurationManager.ConnectionStrings["ConnNegocio"].ConnectionString;

	public void GeneraraBackup(string file)
	{
		try
		{
			using MySqlConnection conn = new MySqlConnection(sConex);
			using MySqlCommand cmd = new MySqlCommand();
			using MySqlBackup mb = new MySqlBackup(cmd);
			cmd.Connection = conn;
			conn.Open();
			mb.ExportToFile(file);
			conn.Close();
		}
		catch (MySqlException ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public void ImportarBackup(string file)
	{
		try
		{
			using (MySqlConnection conn = new MySqlConnection(sConex))
			{
				using MySqlCommand cmd = new MySqlCommand();
				using MySqlBackup mb = new MySqlBackup(cmd);
				cmd.Connection = conn;
				conn.Open();
				mb.ImportFromFile(file);
				conn.Close();
			}
			MessageBox.Show("La importación de datos se realizó con ÉXITO!!!");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
	}

	public MySqlConnection conectarBD()
	{
		try
		{
			conector = new MySqlConnection(sConex);
			conector.Open();
			return conector;
		}
		catch (MySqlException ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public MySqlConnection desconectarBD()
	{
		conector.Close();
		return conector;
	}

	public string LocalIPAddress()
	{
		string localIP = "";
		IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
		IPAddress[] addressList = host.AddressList;
		foreach (IPAddress ip in addressList)
		{
			if (ip.AddressFamily.ToString() == "InterNetwork")
			{
				localIP = ip.ToString();
			}
		}
		return localIP;
	}
}
