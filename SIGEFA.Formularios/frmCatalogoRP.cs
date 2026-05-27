using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmCatalogoRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	public CrystalReportViewer cRVProductos;

	public frmCatalogoRP()
	{
		InitializeComponent();
	}

	private void frmProductosRP_Load(object sender, EventArgs e)
	{
		CRCatalogoPrecios CRep = new CRCatalogoPrecios();
		CRep.Load("CRCatalogoPrecios.rpt");
		CRep.SetDataSource(DTable);
		cRVProductos.ReportSource = CRep;
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
		this.cRVProductos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVProductos.ActiveViewIndex = -1;
		this.cRVProductos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVProductos.DisplayGroupTree = false;
		this.cRVProductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVProductos.Location = new System.Drawing.Point(0, 0);
		this.cRVProductos.Name = "cRVProductos";
		this.cRVProductos.SelectionFormula = "";
		this.cRVProductos.Size = new System.Drawing.Size(680, 323);
		this.cRVProductos.TabIndex = 0;
		this.cRVProductos.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(680, 323);
		base.Controls.Add(this.cRVProductos);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmCatalogoRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmProductosRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmProductosRP_Load);
		base.ResumeLayout(false);
	}
}
