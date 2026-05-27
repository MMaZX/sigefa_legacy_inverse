using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamCobranzaSucursal : Form
{
	private clsReporteInformeSucursal ds = new clsReporteInformeSucursal();

	private IContainer components = null;

	private Button btnCancelar;

	private Button btnReporte;

	private DateTimePicker dtpFecha2;

	private Label label3;

	private DateTimePicker dtpFecha1;

	private Label label1;

	public frmParamCobranzaSucursal()
	{
		InitializeComponent();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRResumenCobranza rpt = new CRResumenCobranza();
		frmRptCobranzaSucursal frm = new frmRptCobranzaSucursal();
		DataTable dt = ds.ReportCobranzaSucursal(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodSucursal).Tables[0];
		rpt.SetDataSource(dt);
		if (dt.Rows.Count > 0)
		{
			frm.cRVRptCobranzaSucursal.ReportSource = rpt;
			frm.Show();
		}
	}

	private void frmParamCobranzaSucursal_Load(object sender, EventArgs e)
	{
		dtpFecha1.MaxDate = Convert.ToDateTime(DateTime.Now.Date);
		dtpFecha2.MaxDate = Convert.ToDateTime(DateTime.Now.Date);
		dtpFecha1.Value = Convert.ToDateTime(DateTime.Now.Date);
		dtpFecha2.Value = Convert.ToDateTime(DateTime.Now.Date);
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
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
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(131, 64);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 57;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(50, 64);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 56;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(137, 28);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(104, 20);
		this.dtpFecha2.TabIndex = 55;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(135, 9);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 12);
		this.label3.TabIndex = 54;
		this.label3.Text = "Hasta";
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(14, 28);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(95, 20);
		this.dtpFecha1.TabIndex = 53;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(12, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 52;
		this.label1.Text = "Desde";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(259, 99);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.dtpFecha2);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.dtpFecha1);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmParamCobranzaSucursal";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte Cobranza Sucursal";
		base.Load += new System.EventHandler(frmParamCobranzaSucursal_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
