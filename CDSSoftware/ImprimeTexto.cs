using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CDSSoftware;

public class ImprimeTexto
{
	private int GENERIC_WRITE = 1073741824;

	private int OPEN_EXISTING = 3;

	private int FILE_SHARE_WRITE = 2;

	private string sPorta;

	private int hPort;

	private FileStream outFile;

	private StreamWriter fileWriter;

	private IntPtr hPortP;

	private bool lOK = false;

	private string GeraArquivoLPT;

	public string Normal => Chr(18);

	public string Comprimido => Chr(15);

	public string Expandido => Chr(14);

	public string ExpandidoNormal => Chr(20);

	public string NegritoOn => Chr(27) + Chr(69);

	public string NegritoOff => Chr(27) + Chr(70);

	private string Chr(int asc)
	{
		string ret = "";
		return ret + (char)asc;
	}

	[DllImport("kernel32.dll")]
	private static extern int CreateFileA(string lpFileName, int dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

	[DllImport("kernel32.dll")]
	private static extern int CloseHandle(int hObject);

	public bool Inicio(string sPortaInicio)
	{
		GeraArquivoLPT = "";
		sPortaInicio.ToUpper();
		outFile = null;
		if (sPortaInicio.Substring(0, 3) == "LPT")
		{
			if (sPortaInicio == "LPT")
			{
				sPortaInicio = "LPT1";
			}
			sPorta = sPortaInicio;
			sPortaInicio = "LPT-" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".TXT";
			GeraArquivoLPT = sPortaInicio;
			fileWriter = new StreamWriter(sPortaInicio);
			lOK = true;
		}
		else
		{
			fileWriter = new StreamWriter(sPortaInicio);
			lOK = true;
		}
		return lOK;
	}

	public void Fim()
	{
		if (lOK)
		{
			fileWriter.Close();
			if (outFile != null)
			{
				outFile.Close();
				CloseHandle(hPort);
			}
			lOK = false;
			if (GeraArquivoLPT != string.Empty)
			{
				File.Copy(GeraArquivoLPT, sPorta);
				File.Delete(GeraArquivoLPT);
				GeraArquivoLPT = "";
			}
		}
	}

	public void Imp(string sLinha)
	{
		if (lOK)
		{
			fileWriter.Write(sLinha);
			fileWriter.Flush();
		}
	}

	public void ImpLF(string sLinha)
	{
		if (lOK)
		{
			fileWriter.WriteLine(sLinha);
			fileWriter.Flush();
		}
	}

	public void ImpCol(int nCol, string sLinha)
	{
		string Cols = "";
		Cols = Cols.PadLeft(nCol, ' ');
		Imp(Chr(13) + Cols + sLinha);
	}

	public void ImpColLF(int nCol, string sLinha)
	{
		ImpCol(nCol, sLinha);
		Pula(1);
	}

	public void Pula(int nLinha)
	{
		for (int i = 0; i < nLinha; i++)
		{
			ImpLF("");
		}
	}

	public void Eject()
	{
		Imp(Chr(12));
	}

	public ImprimeTexto()
	{
		sPorta = "LPT1";
	}
}
