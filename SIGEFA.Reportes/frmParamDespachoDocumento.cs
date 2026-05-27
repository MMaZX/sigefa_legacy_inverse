using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamDespachoDocumento : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsReporteDespacho ds = new clsReporteDespacho();

	private IContainer components = null;

	private Button btnCancelar;

	private Button btnReporte;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha1;

	private Label label9;

	private ComboBox cmbEmpresa;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private Label label2;

	public frmParamDespachoDocumento()
	{
		InitializeComponent();
	}

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedIndex = 0;
	}

	private void alinea_botones()
	{
		label1.Location = new Point(17, 21);
		label2.Location = new Point(157, 21);
		dtpFecha1.Location = new Point(19, 39);
		dtpFecha2.Location = new Point(159, 40);
	}

	private void frmParamDespachoDocumento_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		alinea_botones();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRDespachoDocumento rpt = new CRDespachoDocumento();
		frmRptDespachoDocumento frm = new frmRptDespachoDocumento();
		rpt.SetDataSource(ds.documento(frmLogin.iCodAlmacen, dtpFecha1.Value, dtpFecha2.Value).Tables[0]);
		frm.crvDespachoDocumento.ReportSource = rpt;
		frm.Show();
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(325, 92);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 17;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(244, 92);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 16;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbEmpresa);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(10, 7);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(388, 74);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(277, 39);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(105, 20);
		this.dtpFecha2.TabIndex = 40;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(275, 21);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 39;
		this.label2.Text = "Hasta";
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(159, 39);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(105, 20);
		this.dtpFecha1.TabIndex = 38;
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(17, 21);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(49, 12);
		this.label9.TabIndex = 37;
		this.label9.Text = "Empresa";
		this.label9.Visible = false;
		this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(19, 39);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(121, 20);
		this.cmbEmpresa.TabIndex = 36;
		this.cmbEmpresa.Visible = false;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(157, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(412, 123);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamDespachoDocumento";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Despachos por Documento";
		base.Load += new System.EventHandler(frmParamDespachoDocumento_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
