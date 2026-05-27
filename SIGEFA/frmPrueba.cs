using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

namespace SIGEFA;

public class frmPrueba : Form
{
	private IContainer components = null;

	private Button btnnro1;

	private Button btnAbrir;

	public frmPrueba()
	{
		InitializeComponent();
	}

	private void frmPrueba_Load(object sender, EventArgs e)
	{
	}

	private void btnnro1_Click(object sender, EventArgs e)
	{
		SLDocument sl = new SLDocument();
		sl.SetCellValue("E5", "Prison");
		SLStyle style = sl.CreateStyle();
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thick;
		style.Border.LeftBorder.Color = System.Drawing.Color.BlanchedAlmond;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.DashDotDot;
		style.Border.BottomBorder.Color = System.Drawing.Color.Brown;
		style.SetRightBorder(BorderStyleValues.Hair, System.Drawing.Color.Blue);
		style.SetTopBorder(BorderStyleValues.Double, SLThemeColorIndexValues.Accent6Color);
		style.SetDiagonalBorder(BorderStyleValues.MediumDashDotDot, SLThemeColorIndexValues.Accent3Color, 0.2);
		style.Border.DiagonalUp = true;
		style.Border.DiagonalDown = true;
		sl.SetCellStyle(5, 5, style);
		sl.SetCellValue("G7", "STEVE ");
		style = sl.CreateStyle();
		style.Border.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
		style.Border.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
		style.Border.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
		style.Border.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
		sl.SetCellStyle("G7", style);
		sl.SetRowHeight(7, 50.0);
		sl.SetColumnWidth(3, 22.0);
		sl.SetCellValue("K15", "Algunos de los ejemplos de C# de este artículo se ejecutan en el ejecutor de código en línea y área de juegos de Try.NET. Haga clic en el botón Ejecutar para ejecutar un ejemplo en una ventana interactiva. Una vez que se ejecuta el código, puede modificar y ejecutar el código modificado si vuelve a hacer clic en Ejecutar. El código modificado se ejecuta en la ventana interactiva o, si se produce un error en la compilación, en la ventana interactiva se muestran todos los mensajes de error del compilador de C#.");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.Solid);
		style.Fill.SetPatternBackgroundColor(System.Drawing.Color.Gray);
		sl.SetColumnWidth(11, 35.0);
		sl.SetRowHeight(15, 100.0);
		sl.SetCellStyle("K15", style);
		sl.SetCellValue("B20", "Hola");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.Solid);
		style.Fill.SetPatternBackgroundColor(ColorTranslator.FromHtml("#BDD7EE"));
		sl.SetCellStyle("B20", style);
		sl.SetCellValue("B21", "Hola");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.LightTrellis);
		style.Fill.SetPatternBackgroundColor(ColorTranslator.FromHtml("#A6A6A6"));
		sl.SetCellStyle("B21", style);
		sl.SetCellValue("B22", "Hola");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.MediumGray);
		style.Fill.SetPatternBackgroundColor(ColorTranslator.FromHtml("#A6A6A6"));
		sl.SetCellStyle("B22", style);
		sl.SetCellValue("B23", "Hola");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.None);
		style.Fill.SetPatternBackgroundColor(ColorTranslator.FromHtml("#A6A6A6"));
		sl.SetCellValue("B24", "Hola");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.LightDown);
		style.Fill.SetPatternBackgroundColor(ColorTranslator.FromHtml("#A6A6A6"));
		sl.SetCellStyle("B24", style);
		sl.SetCellValue("B25", "Hola");
		style = sl.CreateStyle();
		style.SetWrapText(IsWrapped: true);
		style.SetFontColor(System.Drawing.Color.Green);
		style.Fill.SetPatternType(PatternValues.LightVertical);
		style.Fill.SetPatternBackgroundColor(ColorTranslator.FromHtml("#A6A6A6"));
		sl.SetCellStyle("B25", style);
		string cadenaGuardado1 = obtenerRutaParaGuardar("Border");
		if (cadenaGuardado1 != null)
		{
			sl.SaveAs(cadenaGuardado1);
			Console.WriteLine(cadenaGuardado1);
		}
		Console.WriteLine("End of program");
		sl = new SLDocument();
		sl.SetCellValue("B2", "This is a merged cell");
		sl.SetCellValue("B5", "This is a merged cell");
		sl.SetCellValue("D5", "This is a merged cell");
		sl.MergeWorksheetCells("B2", "G8");
		sl.MergeWorksheetCells(10, 4, 12, 6);
		sl.MergeWorksheetCells(15, 12, 4, 9);
		sl.UnmergeWorksheetCells("D10", "F12");
		string cadenaGuardado2 = obtenerRutaParaGuardar("MergeCells.xlsx");
		if (cadenaGuardado2 != null)
		{
			sl.SaveAs(cadenaGuardado2);
		}
		Console.WriteLine("End of program");
		Console.ReadLine();
		sl = new SLDocument();
		sl.SetCellValue("A1", Data: true);
		for (int i = 1; i <= 20; i++)
		{
			sl.SetCellValue(2, i, i);
		}
		sl.SetCellValue("B3", 3.14159);
		sl.SetCellValueNumeric(4, 2, "3.14159");
		sl.SetCellValue("C6", "This is at C6!");
		sl.SetCellValue("I6", "Dinner & Dance costs < $10");
		sl.SetCellValue(7, 3, "=SUM(A2:T2)");
		sl.SetCellValue(SLConvert.ToCellReference(7, 4), $"=SUM({SLConvert.ToCellRange(2, 1, 2, 20)})");
		sl.SetCellValue("C8", new DateTime(3141, 5, 9));
		style = sl.CreateStyle();
		style.FormatCode = "d-mmm-yyyy";
		sl.SetCellStyle("C8", style);
		sl.SetCellValue(8, 6, "I predict this to be a significant date. Why, I do not know...");
		sl.SetCellValue(9, 4, 456.123789);
		style.FormatCode = "0.000%";
		sl.SetCellStyle(9, 4, style);
		sl.SetCellValue(9, 6, "Perhaps a phenomenal growth in something?");
		cadenaGuardado2 = obtenerRutaParaGuardar("Exportacion_1");
		if (cadenaGuardado2 != null)
		{
			sl.SaveAs(cadenaGuardado2);
		}
		Console.WriteLine("End of program - 1 ");
		SLDocument sl2 = new SLDocument();
		sl2.SetCellValue("B2", "This is a merged cell");
		sl2.MergeWorksheetCells("B2", "G8");
		sl2.MergeWorksheetCells(10, 4, 12, 6);
		sl2.MergeWorksheetCells(15, 12, 4, 9);
		sl2.UnmergeWorksheetCells("D10", "F12");
		cadenaGuardado1 = obtenerRutaParaGuardar("Exportacion_2");
		if (cadenaGuardado1 != null)
		{
			sl2.SaveAs(cadenaGuardado1);
			Console.WriteLine(cadenaGuardado1);
		}
		Console.WriteLine("End of program - 2");
		SLDocument sl3 = new SLDocument();
		SLStyle style2 = sl3.CreateStyle();
		style2.Fill.SetPattern(PatternValues.Solid, SLThemeColorIndexValues.Accent2Color, SLThemeColorIndexValues.Accent4Color);
		SLStyle style3 = sl3.CreateStyle();
		style3.SetFont(FontSchemeValues.Minor, 18.0);
		style3.Fill.SetGradient(SLGradientShadingStyleValues.Corner1, SLThemeColorIndexValues.Accent1Color, SLThemeColorIndexValues.Accent6Color);
		sl3.SetRowStyle(4, style2);
		sl3.SetRowStyle(5, 8, style3);
		SLStyle style4 = sl3.CreateStyle();
		style4.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Aqua, System.Drawing.Color.DarkSalmon);
		SLStyle style5 = sl3.CreateStyle();
		style5.Border.LeftBorder.BorderStyle = BorderStyleValues.Double;
		style5.Border.LeftBorder.SetBorderThemeColor(SLThemeColorIndexValues.Accent5Color);
		style5.Border.RightBorder.BorderStyle = BorderStyleValues.Double;
		style5.Border.RightBorder.SetBorderThemeColor(SLThemeColorIndexValues.Accent5Color);
		sl3.SetColumnStyle(5, style4);
		sl3.SetColumnStyle(7, 9, style5);
		SLStyle style6 = sl3.CreateStyle();
		style6.SetFont("Impact", 24.0);
		style6.Fill.SetPattern(PatternValues.LightTrellis, SLThemeColorIndexValues.Accent1Color, SLThemeColorIndexValues.Accent2Color);
		style6.Border.DiagonalBorder.BorderStyle = BorderStyleValues.DashDotDot;
		style6.Border.DiagonalBorder.SetBorderThemeColor(SLThemeColorIndexValues.Accent3Color);
		style6.Border.DiagonalUp = true;
		sl3.SetCellValue(3, 4, "Do you have style?");
		sl3.SetCellStyle("D3", style6);
		sl3.SetCellStyle("F3", "I1", style6);
		sl3.SetCellStyle(1, 11, 2, 13, style6);
		sl3.CopyCellStyle("D3", "A1", "B2");
		sl3.CopyRowStyle(4, 10);
		sl3.CopyColumnStyle(8, 1, 3);
		SLStyle modstyle = sl3.GetCellStyle("D3");
		modstyle.Fill.SetPattern(PatternValues.DarkHorizontal, SLThemeColorIndexValues.Accent6Color, SLThemeColorIndexValues.Accent5Color);
		modstyle.RemoveBorder();
		sl3.SetCellStyle("K15", modstyle);
		string cadenaGuardado3 = obtenerRutaParaGuardar();
		if (cadenaGuardado3 != null)
		{
			sl3.SaveAs(cadenaGuardado3);
		}
		Console.WriteLine("End of program - 3");
		Console.ReadLine();
	}

	private string obtenerRutaParaGuardar(string namefile = "Archivo_X")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo Excel de Exportacion";
			sfd.FileName = namefile;
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void btnAbrir_Click(object sender, EventArgs e)
	{
		try
		{
			FolderBrowserDialog Ruta3 = new FolderBrowserDialog();
			if (Ruta3.ShowDialog() == DialogResult.OK)
			{
				Process.Start("explorer.exe", Ruta3.SelectedPath);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.btnnro1 = new System.Windows.Forms.Button();
		this.btnAbrir = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnnro1.Location = new System.Drawing.Point(120, 63);
		this.btnnro1.Name = "btnnro1";
		this.btnnro1.Size = new System.Drawing.Size(75, 23);
		this.btnnro1.TabIndex = 0;
		this.btnnro1.Text = "CLICK";
		this.btnnro1.UseVisualStyleBackColor = true;
		this.btnnro1.Click += new System.EventHandler(btnnro1_Click);
		this.btnAbrir.Location = new System.Drawing.Point(120, 113);
		this.btnAbrir.Name = "btnAbrir";
		this.btnAbrir.Size = new System.Drawing.Size(75, 23);
		this.btnAbrir.TabIndex = 0;
		this.btnAbrir.Text = "ABRIR";
		this.btnAbrir.UseVisualStyleBackColor = true;
		this.btnAbrir.Click += new System.EventHandler(btnAbrir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(332, 301);
		base.Controls.Add(this.btnAbrir);
		base.Controls.Add(this.btnnro1);
		base.Name = "frmPrueba";
		this.Text = "frmPrueba";
		base.Load += new System.EventHandler(frmPrueba_Load);
		base.ResumeLayout(false);
	}
}
