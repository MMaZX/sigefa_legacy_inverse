using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SIGEFA.Formularios;

namespace SIGEFA;

internal static class Program
{
	public static string CarpetaCdr => "./documentos/CDR";

	public static string CarpetaBoletas => "./documentos/Boletas";

	public static string CarpetaFacturas => "./documentos/Facturas";

	public static string CarpetaNC => "./documentos/NC";

	public static string CarpetaND => "./documentos/ND";

	public static string CarpetaResumen => "./documentos/RESUMEN";

	public static string CarpetaGR => "./documentos/GUIAS";

	[STAThread]
	private static void Main()
	{
		try
		{
			if (!Directory.Exists(CarpetaCdr))
			{
				Directory.CreateDirectory(CarpetaCdr);
			}
			if (!Directory.Exists(CarpetaBoletas))
			{
				Directory.CreateDirectory(CarpetaBoletas);
			}
			if (!Directory.Exists(CarpetaFacturas))
			{
				Directory.CreateDirectory(CarpetaFacturas);
			}
			if (!Directory.Exists(CarpetaNC))
			{
				Directory.CreateDirectory(CarpetaNC);
			}
			if (!Directory.Exists(CarpetaND))
			{
				Directory.CreateDirectory(CarpetaND);
			}
			if (!Directory.Exists(CarpetaResumen))
			{
				Directory.CreateDirectory(CarpetaResumen);
			}
			if (!Directory.Exists(CarpetaGR))
			{
				Directory.CreateDirectory(CarpetaGR);
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Thread.CurrentThread.CurrentCulture = new CultureInfo("es-PE");
			Application.Run(new frmLogin());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message + ": " + ex.ToString(), "Linea 57 - Program.cs");
		}
	}
}
