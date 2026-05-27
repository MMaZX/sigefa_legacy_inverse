using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptLiquidacionCaja : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVLiquidacionCaja;

	public frmRptLiquidacionCaja()
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
		this.cRVLiquidacionCaja = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVLiquidacionCaja.ActiveViewIndex = -1;
		this.cRVLiquidacionCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVLiquidacionCaja.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVLiquidacionCaja.Location = new System.Drawing.Point(0, 0);
		this.cRVLiquidacionCaja.Name = "cRVLiquidacionCaja";
		this.cRVLiquidacionCaja.SelectionFormula = "";
		this.cRVLiquidacionCaja.Size = new System.Drawing.Size(386, 385);
		this.cRVLiquidacionCaja.TabIndex = 0;
		this.cRVLiquidacionCaja.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(386, 385);
		base.Controls.Add(this.cRVLiquidacionCaja);
		base.Name = "frmRptLiquidacionCaja";
		this.Text = "frmRptLiquidacionCaja";
		base.ResumeLayout(false);
	}
}
