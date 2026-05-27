using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptVentSeparacion : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptVentSeparacion;

	public frmRptVentSeparacion()
	{
		InitializeComponent();
	}

	private void frmRptVentCredContDia_Load(object sender, EventArgs e)
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
		this.crvRptVentSeparacion = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptVentSeparacion.ActiveViewIndex = -1;
		this.crvRptVentSeparacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptVentSeparacion.CachedPageNumberPerDoc = 10;
		this.crvRptVentSeparacion.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvRptVentSeparacion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptVentSeparacion.Location = new System.Drawing.Point(0, 0);
		this.crvRptVentSeparacion.Name = "crvRptVentSeparacion";
		this.crvRptVentSeparacion.SelectionFormula = "";
		this.crvRptVentSeparacion.Size = new System.Drawing.Size(683, 371);
		this.crvRptVentSeparacion.TabIndex = 0;
		this.crvRptVentSeparacion.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(683, 371);
		base.Controls.Add(this.crvRptVentSeparacion);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRptVentSeparacion";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Ventas por Separación";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptVentCredContDia_Load);
		base.ResumeLayout(false);
	}
}
