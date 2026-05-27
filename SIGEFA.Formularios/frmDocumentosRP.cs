using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmDocumentosRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVDocumentos;

	public frmDocumentosRP()
	{
		InitializeComponent();
	}

	private void frmDocumentosRP_Load(object sender, EventArgs e)
	{
		CRDocumentos CRep = new CRDocumentos();
		CRep.Load("CRDocumentos.rpt");
		CRep.SetDataSource(DTable);
		cRVDocumentos.ReportSource = CRep;
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
		this.cRVDocumentos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVDocumentos.ActiveViewIndex = -1;
		this.cRVDocumentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVDocumentos.DisplayGroupTree = false;
		this.cRVDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVDocumentos.Location = new System.Drawing.Point(0, 0);
		this.cRVDocumentos.Name = "cRVDocumentos";
		this.cRVDocumentos.SelectionFormula = "";
		this.cRVDocumentos.Size = new System.Drawing.Size(655, 313);
		this.cRVDocumentos.TabIndex = 0;
		this.cRVDocumentos.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(655, 313);
		base.Controls.Add(this.cRVDocumentos);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmDocumentosRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmDocumentosRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmDocumentosRP_Load);
		base.ResumeLayout(false);
	}
}
