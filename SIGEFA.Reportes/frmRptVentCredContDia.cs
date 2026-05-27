using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class frmRptVentCredContDia : Office2007Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptVentCredContDia;

	public frmRptVentCredContDia()
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
		this.crvRptVentCredContDia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptVentCredContDia.ActiveViewIndex = -1;
		this.crvRptVentCredContDia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptVentCredContDia.DisplayGroupTree = false;
		this.crvRptVentCredContDia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptVentCredContDia.Location = new System.Drawing.Point(0, 0);
		this.crvRptVentCredContDia.Name = "crvRptVentCredContDia";
		this.crvRptVentCredContDia.SelectionFormula = "";
		this.crvRptVentCredContDia.Size = new System.Drawing.Size(683, 371);
		this.crvRptVentCredContDia.TabIndex = 0;
		this.crvRptVentCredContDia.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(683, 371);
		base.Controls.Add(this.crvRptVentCredContDia);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRptVentCredContDia";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Ventas al Credito/Contado";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
