using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmTransaccionesRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVTransacciones;

	public frmTransaccionesRP()
	{
		InitializeComponent();
	}

	private void frmTransaccionesRP_Load(object sender, EventArgs e)
	{
		CRTransacciones CRep = new CRTransacciones();
		CRep.Load("CRTransacciones.rpt");
		CRep.SetDataSource(DTable);
		cRVTransacciones.ReportSource = CRep;
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
		this.cRVTransacciones = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVTransacciones.ActiveViewIndex = -1;
		this.cRVTransacciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVTransacciones.DisplayGroupTree = false;
		this.cRVTransacciones.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVTransacciones.Location = new System.Drawing.Point(0, 0);
		this.cRVTransacciones.Name = "cRVTransacciones";
		this.cRVTransacciones.SelectionFormula = "";
		this.cRVTransacciones.Size = new System.Drawing.Size(651, 336);
		this.cRVTransacciones.TabIndex = 0;
		this.cRVTransacciones.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(651, 336);
		base.Controls.Add(this.cRVTransacciones);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmTransaccionesRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmTransaccionesRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmTransaccionesRP_Load);
		base.ResumeLayout(false);
	}
}
