using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmTipoCambioRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVTipoCambio;

	public frmTipoCambioRP()
	{
		InitializeComponent();
	}

	private void frmTipoCambioRP_Load(object sender, EventArgs e)
	{
		CRTipoCambio CRep = new CRTipoCambio();
		CRep.Load("CRTipoCambio.rpt");
		CRep.SetDataSource(DTable);
		cRVTipoCambio.ReportSource = CRep;
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
		this.cRVTipoCambio = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVTipoCambio.ActiveViewIndex = -1;
		this.cRVTipoCambio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVTipoCambio.DisplayGroupTree = false;
		this.cRVTipoCambio.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVTipoCambio.Location = new System.Drawing.Point(0, 0);
		this.cRVTipoCambio.Name = "cRVTipoCambio";
		this.cRVTipoCambio.SelectionFormula = "";
		this.cRVTipoCambio.Size = new System.Drawing.Size(615, 291);
		this.cRVTipoCambio.TabIndex = 0;
		this.cRVTipoCambio.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(615, 291);
		base.Controls.Add(this.cRVTipoCambio);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmTipoCambioRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmTipoCambioRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmTipoCambioRP_Load);
		base.ResumeLayout(false);
	}
}
