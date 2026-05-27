using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmCuentasCorrienteRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	public CrystalReportViewer CRVCtasCte;

	public frmCuentasCorrienteRP()
	{
		InitializeComponent();
	}

	private void frmCuentasCorrienteRP_Load(object sender, EventArgs e)
	{
		CRCtasCte CRep = new CRCtasCte();
		CRep.Load("CRCtasCte.rpt");
		CRep.SetDataSource(DTable);
		CRVCtasCte.ReportSource = CRep;
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
		this.CRVCtasCte = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.CRVCtasCte.ActiveViewIndex = -1;
		this.CRVCtasCte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.CRVCtasCte.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CRVCtasCte.Location = new System.Drawing.Point(0, 0);
		this.CRVCtasCte.Name = "CRVCtasCte";
		this.CRVCtasCte.SelectionFormula = "";
		this.CRVCtasCte.Size = new System.Drawing.Size(733, 475);
		this.CRVCtasCte.TabIndex = 0;
		this.CRVCtasCte.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(733, 475);
		base.Controls.Add(this.CRVCtasCte);
		base.Name = "frmCuentasCorrienteRP";
		this.Text = "frmCuentasCorrienteRP";
		base.Load += new System.EventHandler(frmCuentasCorrienteRP_Load);
		base.ResumeLayout(false);
	}
}
