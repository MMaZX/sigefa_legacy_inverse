using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptMovimientosBancario : Form
{
	private IContainer components = null;

	public CrystalReportViewer cRVMovimientosBancarios;

	public frmRptMovimientosBancario()
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
		this.cRVMovimientosBancarios = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.cRVMovimientosBancarios.ActiveViewIndex = -1;
		this.cRVMovimientosBancarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.cRVMovimientosBancarios.Dock = System.Windows.Forms.DockStyle.Fill;
		this.cRVMovimientosBancarios.Location = new System.Drawing.Point(0, 0);
		this.cRVMovimientosBancarios.Name = "cRVMovimientosBancarios";
		this.cRVMovimientosBancarios.SelectionFormula = "";
		this.cRVMovimientosBancarios.Size = new System.Drawing.Size(690, 392);
		this.cRVMovimientosBancarios.TabIndex = 1;
		this.cRVMovimientosBancarios.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(690, 392);
		base.Controls.Add(this.cRVMovimientosBancarios);
		base.Name = "frmRptMovimientosBancario";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Form1";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
