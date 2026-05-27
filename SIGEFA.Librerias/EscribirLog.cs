using System;
using System.IO;

namespace SIGEFA.Librerias;

public class EscribirLog
{
	public string mensajeLog { get; set; }

	public bool mostrarConsola { get; set; }

	public EscribirLog(string mensajeEnviar, bool mostrarConsola)
	{
		mensajeLog = mensajeEnviar;
		if (mostrarConsola)
		{
			monstrarMensajeConsola();
		}
		escribirLineaFichero();
	}

	public EscribirLog()
	{
		if (mostrarConsola)
		{
			monstrarMensajeConsola();
		}
		escribirLineaFichero();
	}

	public void monstrarMensajeConsola()
	{
		mensajeLog = mensajeLog.Replace(Environment.NewLine, " | ");
		mensajeLog = mensajeLog.Replace("\r\n", " | ").Replace("\n", " | ").Replace("\r", " | ");
		Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + mensajeLog);
	}

	public void escribirLineaFichero()
	{
		try
		{
			FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "estado.log", FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter m_streamWriter = new StreamWriter(fs);
			m_streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
			mensajeLog = mensajeLog.Replace(Environment.NewLine, " | ");
			mensajeLog = mensajeLog.Replace("\r\n", " | ").Replace("\n", " | ").Replace("\r", " | ");
			m_streamWriter.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + mensajeLog);
			m_streamWriter.Flush();
			m_streamWriter.Close();
		}
		catch
		{
		}
	}
}
