using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmUnidadesRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVUnidades;

	public frmUnidadesRP()
	{
		InitializeComponent();
	}

	private void frmUnidadesRP_Load(object sender, EventArgs e)
	{
		CRUnidades CRep = new CRUnidades();
		CRep.Load("CRUnidades.rpt");
		CRep.SetDataSource(DTable);
		cRVUnidades.ReportSource = CRep;
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
		this.cRVUnidades = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVUnidades.ActiveViewIndex = -1;
		this.cRVUnidades.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVUnidades.DisplayGroupTree = false;
		this.cRVUnidades.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVUnidades.Location = new System.Drawing.Point(0, 0);
		this.cRVUnidades.Name = "cRVUnidades";
		this.cRVUnidades.SelectionFormula = "";
		this.cRVUnidades.Size = new System.Drawing.Size(644, 316);
		this.cRVUnidades.TabIndex = 0;
		this.cRVUnidades.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(644, 316);
		base.Controls.Add(this.cRVUnidades);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmUnidadesRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmUnidadesRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmUnidadesRP_Load);
		base.ResumeLayout(false);
	}
}
