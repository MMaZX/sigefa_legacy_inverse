using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptArqueo : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvArqueo;

	public frmRptArqueo()
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
		this.crvArqueo = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvArqueo.ActiveViewIndex = -1;
		this.crvArqueo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvArqueo.DisplayGroupTree = false;
		this.crvArqueo.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvArqueo.Location = new System.Drawing.Point(0, 0);
		this.crvArqueo.Name = "crvArqueo";
		this.crvArqueo.SelectionFormula = "";
		this.crvArqueo.Size = new System.Drawing.Size(629, 438);
		this.crvArqueo.TabIndex = 0;
		this.crvArqueo.ViewTimeSelectionFormula = "";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(629, 438);
		base.Controls.Add(this.crvArqueo);
		base.Name = "frmRptArqueo";
		this.Text = "frmRptArqueo";
		base.ResumeLayout(false);
	}
}
