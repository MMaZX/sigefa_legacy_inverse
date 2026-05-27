using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmReporteFlujoCaja : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVFlujoCaja;

	public frmReporteFlujoCaja()
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
		this.cRVFlujoCaja = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVFlujoCaja.ActiveViewIndex = -1;
		this.cRVFlujoCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVFlujoCaja.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVFlujoCaja.Location = new System.Drawing.Point(0, 0);
		this.cRVFlujoCaja.Name = "cRVFlujoCaja";
		this.cRVFlujoCaja.SelectionFormula = "";
		this.cRVFlujoCaja.Size = new System.Drawing.Size(451, 403);
		this.cRVFlujoCaja.TabIndex = 0;
		this.cRVFlujoCaja.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(451, 403);
		base.Controls.Add(this.cRVFlujoCaja);
		base.Name = "frmReporteFlujoCaja";
		this.Text = "frmReporteFlujoCaja";
		base.ResumeLayout(false);
	}
}
