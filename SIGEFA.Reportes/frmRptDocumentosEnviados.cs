using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;

namespace SIGEFA.Reportes;

public class frmRptDocumentosEnviados : Form
{
	private IContainer components = null;

	public CrystalReportViewer crvRptDocumentosEnviados;

	public frmRptDocumentosEnviados()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmRptDocumentosEnviados));
		this.crvRptDocumentosEnviados = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvRptDocumentosEnviados.ActiveViewIndex = -1;
		this.crvRptDocumentosEnviados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvRptDocumentosEnviados.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvRptDocumentosEnviados.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvRptDocumentosEnviados.Location = new System.Drawing.Point(0, 0);
		this.crvRptDocumentosEnviados.Name = "crvRptDocumentosEnviados";
		this.crvRptDocumentosEnviados.Size = new System.Drawing.Size(361, 261);
		this.crvRptDocumentosEnviados.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(361, 261);
		base.Controls.Add(this.crvRptDocumentosEnviados);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmRptDocumentosEnviados";
		this.Text = "Reporte Documentos Electrónicos Enviados";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
