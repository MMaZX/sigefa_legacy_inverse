using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmReporteMovimientosCajaChica : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvMovimientosdecajachica;

	public frmReporteMovimientosCajaChica()
	{
		InitializeComponent();
	}

	private void frmReporteMovimientosCajaChica_Load(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmReporteMovimientosCajaChica));
		this.crvMovimientosdecajachica = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvMovimientosdecajachica.ActiveViewIndex = -1;
		this.crvMovimientosdecajachica.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvMovimientosdecajachica.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvMovimientosdecajachica.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvMovimientosdecajachica.Location = new System.Drawing.Point(0, 0);
		this.crvMovimientosdecajachica.Name = "crvMovimientosdecajachica";
		this.crvMovimientosdecajachica.Size = new System.Drawing.Size(401, 327);
		this.crvMovimientosdecajachica.TabIndex = 0;
		this.crvMovimientosdecajachica.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(401, 327);
		base.Controls.Add(this.crvMovimientosdecajachica);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmReporteMovimientosCajaChica";
		this.Text = "Reporte de Movimientos de Caja Chica";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmReporteMovimientosCajaChica_Load);
		base.ResumeLayout(false);
	}
}
