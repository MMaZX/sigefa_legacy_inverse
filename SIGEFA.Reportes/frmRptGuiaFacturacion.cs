using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptGuiaFacturacion : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvguia;

	private CRReporteGuiaFacturacion CRReporteGuiaFacturacion1;

	public frmRptGuiaFacturacion()
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
		this.crvguia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		this.CRReporteGuiaFacturacion1 = new SIGEFA.Reportes.CRReporteGuiaFacturacion();
		base.SuspendLayout();
		this.crvguia.ActiveViewIndex = 0;
		this.crvguia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvguia.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvguia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvguia.Location = new System.Drawing.Point(0, 0);
		this.crvguia.Name = "crvguia";
		this.crvguia.ReportSource = this.CRReporteGuiaFacturacion1;
		this.crvguia.Size = new System.Drawing.Size(1140, 719);
		this.crvguia.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1140, 719);
		base.Controls.Add(this.crvguia);
		base.Name = "frmRptGuiaFacturacion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmRptGuiaFacturacion";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
