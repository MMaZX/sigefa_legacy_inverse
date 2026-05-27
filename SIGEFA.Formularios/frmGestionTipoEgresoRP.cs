using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmGestionTipoEgresoRP : Form
{
	public DataTable DTable;

	private IContainer components = null;

	public CrystalReportViewer cRVTipoEgresoCaja;

	public frmGestionTipoEgresoRP()
	{
		InitializeComponent();
	}

	private void frmGestionTipoEgresoRP_Load(object sender, EventArgs e)
	{
		CRTipoEgresoCaja CRep = new CRTipoEgresoCaja();
		CRep.Load("CRTipoEgresoCaja.rpt");
		CRep.SetDataSource(DTable);
		cRVTipoEgresoCaja.ReportSource = CRep;
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
		this.cRVTipoEgresoCaja = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVTipoEgresoCaja.ActiveViewIndex = -1;
		this.cRVTipoEgresoCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVTipoEgresoCaja.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVTipoEgresoCaja.Location = new System.Drawing.Point(0, 0);
		this.cRVTipoEgresoCaja.Name = "cRVTipoEgresoCaja";
		this.cRVTipoEgresoCaja.SelectionFormula = "";
		this.cRVTipoEgresoCaja.Size = new System.Drawing.Size(427, 340);
		this.cRVTipoEgresoCaja.TabIndex = 0;
		this.cRVTipoEgresoCaja.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(427, 340);
		base.Controls.Add(this.cRVTipoEgresoCaja);
		base.Name = "frmGestionTipoEgresoRP";
		this.Text = "frmGestionTipoEgresoRP";
		base.Load += new System.EventHandler(frmGestionTipoEgresoRP_Load);
		base.ResumeLayout(false);
	}
}
