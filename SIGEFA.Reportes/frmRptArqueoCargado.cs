using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptArqueoCargado : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvArqueoCargado;

	public frmRptArqueoCargado()
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
		this.crvArqueoCargado = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvArqueoCargado.ActiveViewIndex = -1;
		this.crvArqueoCargado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvArqueoCargado.DisplayGroupTree = false;
		this.crvArqueoCargado.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvArqueoCargado.Location = new System.Drawing.Point(0, 0);
		this.crvArqueoCargado.Name = "crvArqueoCargado";
		this.crvArqueoCargado.SelectionFormula = "";
		this.crvArqueoCargado.Size = new System.Drawing.Size(629, 438);
		this.crvArqueoCargado.TabIndex = 0;
		this.crvArqueoCargado.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(629, 438);
		base.Controls.Add(this.crvArqueoCargado);
		base.Name = "frmRptArqueoCargado";
		this.Text = "Reporte de Arqueo";
		base.ResumeLayout(false);
	}
}
