using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmTransferenciaDirecta : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvTransferenciaPendiente;

	public frmTransferenciaDirecta()
	{
		InitializeComponent();
	}

	private void frmTransferenciaDirecta_Load(object sender, EventArgs e)
	{
		crvTransferenciaPendiente.RefreshReport();
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
		this.crvTransferenciaPendiente = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvTransferenciaPendiente.ActiveViewIndex = -1;
		this.crvTransferenciaPendiente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvTransferenciaPendiente.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvTransferenciaPendiente.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvTransferenciaPendiente.Location = new System.Drawing.Point(0, 0);
		this.crvTransferenciaPendiente.Name = "crvTransferenciaPendiente";
		this.crvTransferenciaPendiente.Size = new System.Drawing.Size(710, 496);
		this.crvTransferenciaPendiente.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(710, 496);
		base.Controls.Add(this.crvTransferenciaPendiente);
		base.Name = "frmTransferenciaDirecta";
		this.Text = "frmTransferenciaDirecta";
		base.Load += new System.EventHandler(frmTransferenciaDirecta_Load);
		base.ResumeLayout(false);
	}
}
