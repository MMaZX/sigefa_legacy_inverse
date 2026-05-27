using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmAlmacenesRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVAlmacenes;

	public frmAlmacenesRP()
	{
		InitializeComponent();
	}

	private void frmAlmacenesRP_Load(object sender, EventArgs e)
	{
		CRAlmacenes CRep = new CRAlmacenes();
		CRep.Load("CRAlmacenes.rpt");
		CRep.SetDataSource(DTable);
		cRVAlmacenes.ReportSource = CRep;
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
		this.cRVAlmacenes = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVAlmacenes.ActiveViewIndex = -1;
		this.cRVAlmacenes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVAlmacenes.DisplayGroupTree = false;
		this.cRVAlmacenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVAlmacenes.Location = new System.Drawing.Point(0, 0);
		this.cRVAlmacenes.Name = "cRVAlmacenes";
		this.cRVAlmacenes.SelectionFormula = "";
		this.cRVAlmacenes.Size = new System.Drawing.Size(632, 325);
		this.cRVAlmacenes.TabIndex = 0;
		this.cRVAlmacenes.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(632, 325);
		base.Controls.Add(this.cRVAlmacenes);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmAlmacenesRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmAlmacenesRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmAlmacenesRP_Load);
		base.ResumeLayout(false);
	}
}
