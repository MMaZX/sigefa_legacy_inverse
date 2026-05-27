using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmAutorizadoRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVAutorizado;

	public frmAutorizadoRP()
	{
		InitializeComponent();
	}

	private void frmAutorizadoRP_Load(object sender, EventArgs e)
	{
		CRAutorizado CRep = new CRAutorizado();
		CRep.Load("CRAutorizado.rpt");
		CRep.SetDataSource(DTable);
		cRVAutorizado.ReportSource = CRep;
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
		this.cRVAutorizado = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVAutorizado.ActiveViewIndex = -1;
		this.cRVAutorizado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVAutorizado.DisplayGroupTree = false;
		this.cRVAutorizado.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVAutorizado.Location = new System.Drawing.Point(0, 0);
		this.cRVAutorizado.Name = "cRVAutorizado";
		this.cRVAutorizado.SelectionFormula = "";
		this.cRVAutorizado.Size = new System.Drawing.Size(769, 334);
		this.cRVAutorizado.TabIndex = 0;
		this.cRVAutorizado.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(769, 334);
		base.Controls.Add(this.cRVAutorizado);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmAutorizadoRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmAutorizadoRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmAutorizadoRP_Load);
		base.ResumeLayout(false);
	}
}
