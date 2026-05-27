using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmBancosRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVMarcas;

	public frmBancosRP()
	{
		InitializeComponent();
	}

	private void frmMarcasRP_Load(object sender, EventArgs e)
	{
		CRBancos CRep = new CRBancos();
		CRep.Load("CRBancos.rpt");
		CRep.SetDataSource(DTable);
		cRVMarcas.ReportSource = CRep;
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
		this.cRVMarcas = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVMarcas.ActiveViewIndex = -1;
		this.cRVMarcas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVMarcas.DisplayGroupTree = false;
		this.cRVMarcas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVMarcas.Location = new System.Drawing.Point(0, 0);
		this.cRVMarcas.Name = "cRVMarcas";
		this.cRVMarcas.SelectionFormula = "";
		this.cRVMarcas.Size = new System.Drawing.Size(595, 354);
		this.cRVMarcas.TabIndex = 0;
		this.cRVMarcas.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(595, 354);
		base.Controls.Add(this.cRVMarcas);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmBancosRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "BANCOS";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmMarcasRP_Load);
		base.ResumeLayout(false);
	}
}
