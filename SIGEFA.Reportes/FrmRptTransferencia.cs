using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Reportes;

public class FrmRptTransferencia : OfficeForm
{
	private IContainer components = null;

	public CrystalReportViewer crvreportetrasferencia;

	public FrmRptTransferencia()
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
		this.crvreportetrasferencia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		base.SuspendLayout();
		this.crvreportetrasferencia.ActiveViewIndex = -1;
		this.crvreportetrasferencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvreportetrasferencia.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvreportetrasferencia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvreportetrasferencia.Location = new System.Drawing.Point(0, 0);
		this.crvreportetrasferencia.Name = "crvreportetrasferencia";
		this.crvreportetrasferencia.Size = new System.Drawing.Size(1087, 568);
		this.crvreportetrasferencia.TabIndex = 0;
		this.crvreportetrasferencia.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		base.ClientSize = new System.Drawing.Size(1087, 568);
		base.Controls.Add(this.crvreportetrasferencia);
		this.DoubleBuffered = true;
		base.Name = "FrmRptTransferencia";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "FrmRptTransferencia";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.ResumeLayout(false);
	}
}
