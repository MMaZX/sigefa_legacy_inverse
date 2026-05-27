using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmCaracteristicasRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVCaracteristicas;

	public frmCaracteristicasRP()
	{
		InitializeComponent();
	}

	private void frmCaracteristicasRP_Load(object sender, EventArgs e)
	{
		CRCaracteristicas CRep = new CRCaracteristicas();
		CRep.Load("CRCaracteristicas.rpt");
		CRep.SetDataSource(DTable);
		cRVCaracteristicas.ReportSource = CRep;
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
		this.cRVCaracteristicas = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVCaracteristicas.ActiveViewIndex = -1;
		this.cRVCaracteristicas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVCaracteristicas.DisplayGroupTree = false;
		this.cRVCaracteristicas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVCaracteristicas.Location = new System.Drawing.Point(0, 0);
		this.cRVCaracteristicas.Name = "cRVCaracteristicas";
		this.cRVCaracteristicas.SelectionFormula = "";
		this.cRVCaracteristicas.Size = new System.Drawing.Size(667, 307);
		this.cRVCaracteristicas.TabIndex = 0;
		this.cRVCaracteristicas.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(667, 307);
		base.Controls.Add(this.cRVCaracteristicas);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmCaracteristicasRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmCaracteristicasRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCaracteristicasRP_Load);
		base.ResumeLayout(false);
	}
}
