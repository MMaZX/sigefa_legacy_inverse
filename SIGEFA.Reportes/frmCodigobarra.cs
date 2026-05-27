using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmCodigobarra : OfficeForm
{
	private IContainer components = null;

	public CrystalReportViewer crystalReportViewer1;

	public frmCodigobarra()
	{
		InitializeComponent();
	}

	private void frmCodigobarra_Load(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmCodigobarra));
		this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crystalReportViewer1.ActiveViewIndex = -1;
		this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
		this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
		this.crystalReportViewer1.Name = "crystalReportViewer1";
		this.crystalReportViewer1.Size = new System.Drawing.Size(369, 324);
		this.crystalReportViewer1.TabIndex = 0;
		this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(369, 324);
		base.Controls.Add(this.crystalReportViewer1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmCodigobarra";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Codigo de Barras";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmCodigobarra_Load);
		base.ResumeLayout(false);
	}
}
