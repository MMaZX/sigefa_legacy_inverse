using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptImpresionPago : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVImpresionPago;

	public frmRptImpresionPago()
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
		this.cRVImpresionPago = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVImpresionPago.ActiveViewIndex = -1;
		this.cRVImpresionPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVImpresionPago.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVImpresionPago.Location = new System.Drawing.Point(0, 0);
		this.cRVImpresionPago.Name = "cRVImpresionPago";
		this.cRVImpresionPago.SelectionFormula = "";
		this.cRVImpresionPago.Size = new System.Drawing.Size(380, 386);
		this.cRVImpresionPago.TabIndex = 0;
		this.cRVImpresionPago.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(380, 386);
		base.Controls.Add(this.cRVImpresionPago);
		base.Name = "frmRptImpresionPago";
		this.Text = "frmRptImpresionPago";
		base.ResumeLayout(false);
	}
}
