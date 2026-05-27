using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptVentasxCliente : OfficeForm
{
	private IContainer components = null;

	public CrystalReportViewer crvReporteVentasCliente;

	public frmRptVentasxCliente()
	{
		InitializeComponent();
	}

	private void frmRptVentasxCliente_Load(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmRptVentasxCliente));
		this.crvReporteVentasCliente = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvReporteVentasCliente.ActiveViewIndex = -1;
		this.crvReporteVentasCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReporteVentasCliente.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvReporteVentasCliente.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReporteVentasCliente.Location = new System.Drawing.Point(0, 0);
		this.crvReporteVentasCliente.Name = "crvReporteVentasCliente";
		this.crvReporteVentasCliente.Size = new System.Drawing.Size(744, 449);
		this.crvReporteVentasCliente.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(744, 449);
		base.Controls.Add(this.crvReporteVentasCliente);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmRptVentasxCliente";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reprote de Ventas por Cliente";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptVentasxCliente_Load);
		base.ResumeLayout(false);
	}
}
