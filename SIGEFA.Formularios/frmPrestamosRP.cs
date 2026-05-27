using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmPrestamosRP : Office2007Form
{
	public DataTable DTable;

	public int Tipo;

	private IContainer components = null;

	private RibbonBar ribbonBar1;

	public CrystalReportViewer cRVClientesRP;

	private ImageList imageList1;

	private ButtonItem buttonItem1;

	private ButtonItem buttonItem2;

	public frmPrestamosRP()
	{
		InitializeComponent();
	}

	private void frmClientesRP_Load(object sender, EventArgs e)
	{
		if (Tipo == 1)
		{
			CRClientesCompletos CRep = new CRClientesCompletos();
			CRep.Load("CRClientesCompletos.rpt");
			CRep.SetDataSource(DTable);
			cRVClientesRP.ReportSource = CRep;
		}
		if (Tipo == 2)
		{
			CRClientesCorporativos CRep2 = new CRClientesCorporativos();
			CRep2.Load("CRClientesCorporativos.rpt");
			CRep2.SetDataSource(DTable);
			cRVClientesRP.ReportSource = CRep2;
		}
		if (Tipo == 3)
		{
			CRClientesSimple CRep3 = new CRClientesSimple();
			CRep3.Load("CRClientesSimple.rpt");
			CRep3.SetDataSource(DTable);
			cRVClientesRP.ReportSource = CRep3;
		}
	}

	private void biExportExcel_Click(object sender, EventArgs e)
	{
	}

	private void buttonItem1_Click(object sender, EventArgs e)
	{
		CRClientesCompletos rptExcel = new CRClientesCompletos();
		rptExcel.Load("CRClientesCompletos.rpt");
		rptExcel.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
		rptExcel.ExportOptions.ExportFormatType = ExportFormatType.Excel;
		ExcelFormatOptions objExcelOptions = new ExcelFormatOptions();
		objExcelOptions.ExcelUseConstantColumnWidth = false;
		rptExcel.ExportOptions.ExportFormatOptions = objExcelOptions;
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.InitialDirectory = Environment.SpecialFolder.Personal.ToString();
		saveFileDialog.Filter = "Document (*.xls)|*.xls";
		saveFileDialog.FilterIndex = 1;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			DiskFileDestinationOptions objOptions = new DiskFileDestinationOptions();
			objOptions.DiskFileName = saveFileDialog.FileName;
			rptExcel.ExportOptions.ExportDestinationOptions = objOptions;
			rptExcel.Export();
			objOptions = null;
		}
		rptExcel = null;
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		CRClientesCompletos rptExcel = new CRClientesCompletos();
		rptExcel.Load("CRClientesCompletos.rpt");
		rptExcel.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
		rptExcel.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.InitialDirectory = Environment.SpecialFolder.Personal.ToString();
		saveFileDialog.Filter = "Document (*.pdf)|*.pdf";
		saveFileDialog.FilterIndex = 1;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			DiskFileDestinationOptions objOptions = new DiskFileDestinationOptions();
			objOptions.DiskFileName = saveFileDialog.FileName;
			rptExcel.ExportOptions.ExportDestinationOptions = objOptions;
			rptExcel.Export();
			objOptions = null;
		}
		rptExcel = null;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPrestamosRP));
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.cRVClientesRP = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.buttonItem1, this.buttonItem2 });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(762, 34);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.ribbonBar1.TabIndex = 0;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "ACP_PDF 2_file_document.png");
		this.imageList1.Images.SetKeyName(1, "Adobe_PDF_icon.png");
		this.imageList1.Images.SetKeyName(2, "excel_icon.png");
		this.imageList1.Images.SetKeyName(3, "Excel-icon.png");
		this.imageList1.Images.SetKeyName(4, "microsoft excel.png");
		this.imageList1.Images.SetKeyName(5, "pdfIcon.png");
		this.imageList1.Images.SetKeyName(6, "52ff0e80b07d28b590bbc4b30befde52 (1).png");
		this.imageList1.Images.SetKeyName(7, "52ff0e80b07d28b590bbc4b30befde52.png");
		this.imageList1.Images.SetKeyName(8, "647702-excel-512.png");
		this.imageList1.Images.SetKeyName(9, "pdf-512.png");
		this.imageList1.Images.SetKeyName(10, "unnamed.png");
		this.buttonItem1.ImageIndex = 8;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Visible = false;
		this.buttonItem1.Click += new System.EventHandler(buttonItem1_Click);
		this.buttonItem2.ImageIndex = 9;
		this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Visible = false;
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click);
		this.cRVClientesRP.ActiveViewIndex = -1;
		this.cRVClientesRP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVClientesRP.DisplayGroupTree = false;
		this.cRVClientesRP.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVClientesRP.Location = new System.Drawing.Point(0, 34);
		this.cRVClientesRP.Name = "cRVClientesRP";
		this.cRVClientesRP.SelectionFormula = "";
		this.cRVClientesRP.ShowCloseButton = false;
		this.cRVClientesRP.ShowExportButton = false;
		this.cRVClientesRP.ShowGotoPageButton = false;
		this.cRVClientesRP.ShowGroupTreeButton = false;
		this.cRVClientesRP.Size = new System.Drawing.Size(762, 306);
		this.cRVClientesRP.TabIndex = 2;
		this.cRVClientesRP.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(762, 340);
		base.Controls.Add(this.cRVClientesRP);
		base.Controls.Add(this.ribbonBar1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmPrestamosRP";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Prestamos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmClientesRP_Load);
		base.ResumeLayout(false);
	}
}
