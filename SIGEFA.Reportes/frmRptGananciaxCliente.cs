using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptGananciaxCliente : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptGananciaxCliente;

	public frmRptGananciaxCliente()
	{
		InitializeComponent();
	}

	private void frmRptGananciaxCliente_Load(object sender, EventArgs e)
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
		this.crvRptGananciaxCliente = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptGananciaxCliente.ActiveViewIndex = -1;
		this.crvRptGananciaxCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptGananciaxCliente.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvRptGananciaxCliente.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptGananciaxCliente.Location = new System.Drawing.Point(0, 0);
		this.crvRptGananciaxCliente.Name = "crvRptGananciaxCliente";
		this.crvRptGananciaxCliente.Size = new System.Drawing.Size(419, 356);
		this.crvRptGananciaxCliente.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(419, 356);
		base.Controls.Add(this.crvRptGananciaxCliente);
		base.Name = "frmRptGananciaxCliente";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte Ganancia por Cliente";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmRptGananciaxCliente_Load);
		base.ResumeLayout(false);
	}
}
