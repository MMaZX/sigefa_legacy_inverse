using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmEmpresasRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVEmpresas;

	public frmEmpresasRP()
	{
		InitializeComponent();
	}

	private void frmEmpresasRP_Load(object sender, EventArgs e)
	{
		CREmpresas CRep = new CREmpresas();
		CRep.Load("CREmpresas.rpt");
		CRep.SetDataSource(DTable);
		cRVEmpresas.ReportSource = CRep;
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
		this.cRVEmpresas = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVEmpresas.ActiveViewIndex = -1;
		this.cRVEmpresas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVEmpresas.DisplayGroupTree = false;
		this.cRVEmpresas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVEmpresas.Location = new System.Drawing.Point(0, 0);
		this.cRVEmpresas.Name = "cRVEmpresas";
		this.cRVEmpresas.SelectionFormula = "";
		this.cRVEmpresas.Size = new System.Drawing.Size(625, 341);
		this.cRVEmpresas.TabIndex = 0;
		this.cRVEmpresas.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(625, 341);
		base.Controls.Add(this.cRVEmpresas);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmEmpresasRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmEmpresasRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmEmpresasRP_Load);
		base.ResumeLayout(false);
	}
}
