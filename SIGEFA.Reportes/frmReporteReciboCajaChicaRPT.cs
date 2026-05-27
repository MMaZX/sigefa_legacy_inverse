using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmReporteReciboCajaChicaRPT : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvReciboCajaChica;

	public frmReporteReciboCajaChicaRPT()
	{
		InitializeComponent();
	}

	private void frmReporteReciboCajaChicaRPT_Load(object sender, EventArgs e)
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
		this.crvReciboCajaChica = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvReciboCajaChica.ActiveViewIndex = -1;
		this.crvReciboCajaChica.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReciboCajaChica.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvReciboCajaChica.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReciboCajaChica.Location = new System.Drawing.Point(0, 0);
		this.crvReciboCajaChica.Name = "crvReciboCajaChica";
		this.crvReciboCajaChica.Size = new System.Drawing.Size(350, 273);
		this.crvReciboCajaChica.TabIndex = 0;
		this.crvReciboCajaChica.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(350, 273);
		base.Controls.Add(this.crvReciboCajaChica);
		base.Name = "frmReporteReciboCajaChicaRPT";
		this.Text = "Recibos Caja Chica";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmReporteReciboCajaChicaRPT_Load);
		base.ResumeLayout(false);
	}
}
