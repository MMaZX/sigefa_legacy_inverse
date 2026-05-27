using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptKardexTranseferencia : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvKardexTransferencia_s;

	public frmRptKardexTranseferencia()
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
		this.crvKardexTransferencia_s = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvKardexTransferencia_s.ActiveViewIndex = -1;
		this.crvKardexTransferencia_s.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvKardexTransferencia_s.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvKardexTransferencia_s.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvKardexTransferencia_s.Location = new System.Drawing.Point(0, 0);
		this.crvKardexTransferencia_s.Name = "crvKardexTransferencia_s";
		this.crvKardexTransferencia_s.Size = new System.Drawing.Size(800, 450);
		this.crvKardexTransferencia_s.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 450);
		base.Controls.Add(this.crvKardexTransferencia_s);
		base.Name = "frmRptKardexTranseferencia";
		this.Text = "frmRptKardexTranseferencia";
		base.ResumeLayout(false);
	}
}
