using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmProveedoresRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVProveedores;

	public frmProveedoresRP()
	{
		InitializeComponent();
	}

	private void frmProveedoresRP_Load(object sender, EventArgs e)
	{
		CRProveedores CRep = new CRProveedores();
		CRep.Load("CRProveedores.rpt");
		CRep.SetDataSource(DTable);
		cRVProveedores.ReportSource = CRep;
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
		this.cRVProveedores = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVProveedores.ActiveViewIndex = -1;
		this.cRVProveedores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVProveedores.DisplayGroupTree = false;
		this.cRVProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVProveedores.Location = new System.Drawing.Point(0, 0);
		this.cRVProveedores.Name = "cRVProveedores";
		this.cRVProveedores.SelectionFormula = "";
		this.cRVProveedores.Size = new System.Drawing.Size(637, 322);
		this.cRVProveedores.TabIndex = 0;
		this.cRVProveedores.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(637, 322);
		base.Controls.Add(this.cRVProveedores);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmProveedoresRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmProveedoresRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmProveedoresRP_Load);
		base.ResumeLayout(false);
	}
}
