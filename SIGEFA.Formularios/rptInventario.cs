using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class rptInventario : Form
{
	public int alma;

	public bool costo;

	public bool art;

	public bool fam;

	public bool lin;

	public bool gru;

	public bool tip;

	public bool todo;

	public int art1;

	public int art2;

	public bool cero;

	public int orden;

	public bool activos;

	public double tipocambiomanual;

	private DataSet data = null;

	private IContainer components = null;

	private CrystalReportViewer crystalReportViewer1;

	public rptInventario()
	{
		InitializeComponent();
	}

	private void rptInventario_Load(object sender, EventArgs e)
	{
		generareporte();
	}

	private void generareporte()
	{
		try
		{
			clsReportProductos re = new clsReportProductos();
			data = re.Inventario(frmLogin.iCodEmpresa, alma, costo, art, fam, lin, gru, tip, todo, art1, art2, cero, orden, activos, tipocambiomanual);
			CRInventario2 myDataReport = new CRInventario2();
			myDataReport.SetDataSource(data.Tables[0].DefaultView);
			crystalReportViewer1.ReportSource = myDataReport;
		}
		catch (Exception ex)
		{
			MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void crystalReportViewer1_Load(object sender, EventArgs e)
	{
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
		this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crystalReportViewer1.ActiveViewIndex = -1;
		this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
		this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
		this.crystalReportViewer1.Name = "crystalReportViewer1";
		this.crystalReportViewer1.SelectionFormula = "";
		this.crystalReportViewer1.Size = new System.Drawing.Size(771, 485);
		this.crystalReportViewer1.TabIndex = 0;
		this.crystalReportViewer1.ViewTimeSelectionFormula = "";
		this.crystalReportViewer1.Load += new System.EventHandler(crystalReportViewer1_Load);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(771, 485);
		base.Controls.Add(this.crystalReportViewer1);
		base.Name = "rptInventario";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte Inventario";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(rptInventario_Load);
		base.ResumeLayout(false);
	}
}
