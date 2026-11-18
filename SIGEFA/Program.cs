using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores.RutasSistema;
using SIGEFA.Formularios;

namespace SIGEFA;

internal static class Program
{
	public static string CarpetaCdr => RutaDocumentos.Subcarpeta("CDR");

	public static string CarpetaBoletas => RutaDocumentos.Subcarpeta("Boletas");

	public static string CarpetaFacturas => RutaDocumentos.Subcarpeta("Facturas");

	public static string CarpetaNC => RutaDocumentos.Subcarpeta("NC");

	public static string CarpetaND => RutaDocumentos.Subcarpeta("ND");

	public static string CarpetaResumen => RutaDocumentos.Subcarpeta("RESUMEN");

	public static string CarpetaGR => RutaDocumentos.Subcarpeta("GUIAS");

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
			string temaVisual = (ConfigurationManager.AppSettings["TemaVisual"] ?? string.Empty).Trim().ToLowerInvariant();
			if (temaVisual == "fluent")
			{
				new Telerik.WinControls.Themes.FluentTheme();
				Telerik.WinControls.ThemeResolutionService.ApplicationThemeName = "Fluent";
				StyleManager.Style = eStyle.Metro;
			}
			else
			{
				StyleManager.Style = eStyle.Office2007Black;
			}
			Thread.CurrentThread.CurrentCulture = new CultureInfo("es-PE");
			IconoSistema.UsarComoIconoPorDefecto();
			Application.Run(new frmLogin());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message + ": " + ex.ToString(), "Linea 57 - Program.cs");
		}
	}
}
