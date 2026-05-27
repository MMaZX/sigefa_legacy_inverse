using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmTipoArticulosRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	private CrystalReportViewer cRVTipoArticulos;

	public frmTipoArticulosRP()
	{
		InitializeComponent();
	}

	private void frmTipoArticulosRP_Load(object sender, EventArgs e)
	{
		CRTipoArticulos CRep = new CRTipoArticulos();
		CRep.Load("CRTipoArticulos.rpt");
		CRep.SetDataSource(DTable);
		cRVTipoArticulos.ReportSource = CRep;
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
		this.cRVTipoArticulos = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVTipoArticulos.ActiveViewIndex = -1;
		this.cRVTipoArticulos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVTipoArticulos.DisplayGroupTree = false;
		this.cRVTipoArticulos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVTipoArticulos.Location = new System.Drawing.Point(0, 0);
		this.cRVTipoArticulos.Name = "cRVTipoArticulos";
		this.cRVTipoArticulos.SelectionFormula = "";
		this.cRVTipoArticulos.Size = new System.Drawing.Size(600, 306);
		this.cRVTipoArticulos.TabIndex = 0;
		this.cRVTipoArticulos.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(600, 306);
		base.Controls.Add(this.cRVTipoArticulos);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Name = "frmTipoArticulosRP";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmTipoArticulosRP";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmTipoArticulosRP_Load);
		base.ResumeLayout(false);
	}
}
