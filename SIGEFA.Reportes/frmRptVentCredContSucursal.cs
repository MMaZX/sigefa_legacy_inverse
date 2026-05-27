using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptVentCredContSucursal : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVRptVentCredContSucursal;

	public frmRptVentCredContSucursal()
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
		this.cRVRptVentCredContSucursal = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVRptVentCredContSucursal.ActiveViewIndex = -1;
		this.cRVRptVentCredContSucursal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVRptVentCredContSucursal.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVRptVentCredContSucursal.Location = new System.Drawing.Point(0, 0);
		this.cRVRptVentCredContSucursal.Name = "cRVRptVentCredContSucursal";
		this.cRVRptVentCredContSucursal.SelectionFormula = "";
		this.cRVRptVentCredContSucursal.Size = new System.Drawing.Size(284, 262);
		this.cRVRptVentCredContSucursal.TabIndex = 0;
		this.cRVRptVentCredContSucursal.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.cRVRptVentCredContSucursal);
		base.Name = "frmRptVentCredContSucursal";
		this.Text = "frmRptVentCredContSucursal";
		base.ResumeLayout(false);
	}
}
