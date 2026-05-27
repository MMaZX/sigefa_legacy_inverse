using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class FormRptInventarioVargas : OfficeForm
{
	private IContainer components = null;

	public CrystalReportViewer crvReporteInventarioVargas;

	public FormRptInventarioVargas()
	{
		InitializeComponent();
	}

	private void FormRptInventarioVargas_Load(object sender, EventArgs e)
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
		this.crvReporteInventarioVargas = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvReporteInventarioVargas.ActiveViewIndex = -1;
		this.crvReporteInventarioVargas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReporteInventarioVargas.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvReporteInventarioVargas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReporteInventarioVargas.Location = new System.Drawing.Point(0, 0);
		this.crvReporteInventarioVargas.Name = "crvReporteInventarioVargas";
		this.crvReporteInventarioVargas.Size = new System.Drawing.Size(604, 328);
		this.crvReporteInventarioVargas.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(604, 328);
		base.Controls.Add(this.crvReporteInventarioVargas);
		this.DoubleBuffered = true;
		base.Name = "FormRptInventarioVargas";
		this.Text = "Reporte Inventario";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FormRptInventarioVargas_Load);
		base.ResumeLayout(false);
	}
}
