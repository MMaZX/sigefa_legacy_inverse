using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptMercaderiaEntregar : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRMercaderiaEntregar;

	public frmRptMercaderiaEntregar()
	{
		InitializeComponent();
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
		this.cRMercaderiaEntregar = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRMercaderiaEntregar.ActiveViewIndex = -1;
		this.cRMercaderiaEntregar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRMercaderiaEntregar.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRMercaderiaEntregar.Location = new System.Drawing.Point(0, 0);
		this.cRMercaderiaEntregar.Name = "cRMercaderiaEntregar";
		this.cRMercaderiaEntregar.SelectionFormula = "";
		this.cRMercaderiaEntregar.Size = new System.Drawing.Size(404, 300);
		this.cRMercaderiaEntregar.TabIndex = 0;
		this.cRMercaderiaEntregar.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(404, 300);
		base.Controls.Add(this.cRMercaderiaEntregar);
		base.Name = "frmRptMercaderiaEntregar";
		this.Text = "RptMercaderiaPorEntregar";
		base.ResumeLayout(false);
	}
}
