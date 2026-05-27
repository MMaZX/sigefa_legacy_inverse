using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmUsuariosRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVUsuarios;

	public frmUsuariosRP()
	{
		InitializeComponent();
	}

	private void frmUsuariosRP_Load(object sender, EventArgs e)
	{
		CRUsuarios CRep = new CRUsuarios();
		CRep.Load("CRUsuarios.rpt");
		CRep.SetDataSource(DTable);
		cRVUsuarios.ReportSource = CRep;
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
		this.cRVUsuarios = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVUsuarios.ActiveViewIndex = -1;
		this.cRVUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVUsuarios.DisplayGroupTree = false;
		this.cRVUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVUsuarios.Location = new System.Drawing.Point(0, 0);
		this.cRVUsuarios.Name = "cRVUsuarios";
		this.cRVUsuarios.SelectionFormula = "";
		this.cRVUsuarios.Size = new System.Drawing.Size(794, 352);
		this.cRVUsuarios.TabIndex = 0;
		this.cRVUsuarios.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(794, 352);
		base.Controls.Add(this.cRVUsuarios);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmUsuariosRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmUsuariosRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmUsuariosRP_Load);
		base.ResumeLayout(false);
	}
}
