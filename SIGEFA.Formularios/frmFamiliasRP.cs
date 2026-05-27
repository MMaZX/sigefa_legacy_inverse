using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmFamiliasRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVFamilias;

	public frmFamiliasRP()
	{
		InitializeComponent();
	}

	private void frmFamiliasRP_Load(object sender, EventArgs e)
	{
		CRFamilias CRep = new CRFamilias();
		CRep.Load("CRFamilias.rpt");
		CRep.SetDataSource(DTable);
		cRVFamilias.ReportSource = CRep;
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
		this.cRVFamilias = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVFamilias.ActiveViewIndex = -1;
		this.cRVFamilias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVFamilias.DisplayGroupTree = false;
		this.cRVFamilias.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVFamilias.Location = new System.Drawing.Point(0, 0);
		this.cRVFamilias.Name = "cRVFamilias";
		this.cRVFamilias.SelectionFormula = "";
		this.cRVFamilias.Size = new System.Drawing.Size(652, 322);
		this.cRVFamilias.TabIndex = 0;
		this.cRVFamilias.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(652, 322);
		base.Controls.Add(this.cRVFamilias);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmFamiliasRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmFamiliasRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmFamiliasRP_Load);
		base.ResumeLayout(false);
	}
}
