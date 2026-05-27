using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptHistoriaClinica : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crystalReportViewer1;

	public frmRptHistoriaClinica()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmRptHistoriaClinica));
		this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crystalReportViewer1.ActiveViewIndex = -1;
		this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
		this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
		this.crystalReportViewer1.Name = "crystalReportViewer1";
		this.crystalReportViewer1.Size = new System.Drawing.Size(972, 623);
		this.crystalReportViewer1.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(972, 623);
		base.Controls.Add(this.crystalReportViewer1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmRptHistoriaClinica";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte Historia Clínica";
		base.ResumeLayout(false);
	}
}
