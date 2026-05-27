using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptAlmacenVendeMas : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVAlmacenVendeMas;

	public frmRptAlmacenVendeMas()
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
		this.cRVAlmacenVendeMas = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVAlmacenVendeMas.ActiveViewIndex = -1;
		this.cRVAlmacenVendeMas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVAlmacenVendeMas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVAlmacenVendeMas.Location = new System.Drawing.Point(0, 0);
		this.cRVAlmacenVendeMas.Name = "cRVAlmacenVendeMas";
		this.cRVAlmacenVendeMas.SelectionFormula = "";
		this.cRVAlmacenVendeMas.Size = new System.Drawing.Size(365, 354);
		this.cRVAlmacenVendeMas.TabIndex = 0;
		this.cRVAlmacenVendeMas.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(365, 354);
		base.Controls.Add(this.cRVAlmacenVendeMas);
		base.Name = "frmRptAlmacenVendeMas";
		this.Text = "frmRptAlmacenVendeMas";
		base.ResumeLayout(false);
	}
}
