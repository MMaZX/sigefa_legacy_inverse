using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptCobranzaSucursal : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVRptCobranzaSucursal;

	public frmRptCobranzaSucursal()
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
		this.cRVRptCobranzaSucursal = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVRptCobranzaSucursal.ActiveViewIndex = -1;
		this.cRVRptCobranzaSucursal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVRptCobranzaSucursal.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVRptCobranzaSucursal.Location = new System.Drawing.Point(0, 0);
		this.cRVRptCobranzaSucursal.Name = "cRVRptCobranzaSucursal";
		this.cRVRptCobranzaSucursal.SelectionFormula = "";
		this.cRVRptCobranzaSucursal.Size = new System.Drawing.Size(284, 262);
		this.cRVRptCobranzaSucursal.TabIndex = 0;
		this.cRVRptCobranzaSucursal.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.cRVRptCobranzaSucursal);
		base.Name = "frmRptCobranzaSucursal";
		this.Text = "frmRptCobranzaSucursal";
		base.ResumeLayout(false);
	}
}
