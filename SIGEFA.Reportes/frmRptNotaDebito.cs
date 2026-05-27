using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptNotaDebito : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvNotaDebito;

	public frmRptNotaDebito()
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
		this.crvNotaDebito = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvNotaDebito.ActiveViewIndex = -1;
		this.crvNotaDebito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvNotaDebito.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvNotaDebito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvNotaDebito.Location = new System.Drawing.Point(0, 0);
		this.crvNotaDebito.Name = "crvNotaDebito";
		this.crvNotaDebito.Size = new System.Drawing.Size(284, 262);
		this.crvNotaDebito.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(284, 262);
		base.Controls.Add(this.crvNotaDebito);
		base.Name = "frmRptNotaDebito";
		this.Text = "frmRptNotaDebito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
