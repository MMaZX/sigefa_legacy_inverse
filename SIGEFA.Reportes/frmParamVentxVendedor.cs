using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamVentxVendedor : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsReporteVentasxVendedor ds = new clsReporteVentasxVendedor();

	private clsVendedor vend = new clsVendedor();

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmVendedor admVen = new clsAdmVendedor();

	private clsAdmZona admZona = new clsAdmZona();

	private IContainer components = null;

	private DateTimePicker dtpFecha1;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private Label label8;

	private Button btnCancelar;

	private Button btnReporte;

	private Label label7;

	private ComboBox cmbZona;

	private GroupBox groupBox1;

	private Label label5;

	private ComboBox cmbVendedor;

	private ComboBox cmbFormaPago;

	private GroupBox groupBox2;

	private Label label2;

	public frmParamVentxVendedor()
	{
		InitializeComponent();
	}

	private void frmParamVentxVendedor_Load(object sender, EventArgs e)
	{
		CargaFormaPagos();
		CargaVendedores();
		cmbFormaPago.SelectedIndex = 0;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagosReporte();
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
	}

	private void CargaVendedores()
	{
		cmbVendedor.DataSource = admVen.CargaVendedoresReporte();
		cmbVendedor.DisplayMember = "apellido";
		cmbVendedor.ValueMember = "codVendedor";
		cmbVendedor.SelectedIndex = 0;
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRVentasPorVendedor rpt = new CRVentasPorVendedor();
		frmRptVentxVendedor frm = new frmRptVentxVendedor();
		DataTable dt = ds.Reporte(frmLogin.iCodAlmacen, dtpFecha1.Value, dtpFecha2.Value, Convert.ToInt32(cmbFormaPago.SelectedValue), Convert.ToInt32(cmbVendedor.SelectedValue)).Tables[0];
		if (dt.Rows.Count > 0)
		{
			rpt.SetDataSource(dt);
			frm.crvRptVentxVendedor.ReportSource = rpt;
			frm.Show();
		}
		else
		{
			MessageBox.Show("No se ha encontrado resultados con los filtros seleccionados", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void cmbVendedor_SelectedIndexChanged(object sender, EventArgs e)
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
		this.label7 = new System.Windows.Forms.Label();
		this.cmbZona = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cmbVendedor = new System.Windows.Forms.ComboBox();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(305, 31);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(30, 12);
		this.label7.TabIndex = 47;
		this.label7.Text = "Zona";
		this.label7.Visible = false;
		this.cmbZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbZona.FormattingEnabled = true;
		this.cmbZona.Location = new System.Drawing.Point(307, 47);
		this.cmbZona.Margin = new System.Windows.Forms.Padding(4);
		this.cmbZona.Name = "cmbZona";
		this.cmbZona.Size = new System.Drawing.Size(131, 20);
		this.cmbZona.TabIndex = 46;
		this.cmbZona.Visible = false;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(165, 28);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(44, 15);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(168, 47);
		this.dtpFecha2.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha2.TabIndex = 28;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(25, 47);
		this.dtpFecha1.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha1.TabIndex = 38;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(22, 28);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(48, 15);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(387, 295);
		this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(100, 28);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(279, 295);
		this.btnReporte.Margin = new System.Windows.Forms.Padding(4);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 28);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.cmbZona);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(475, 100);
		this.groupBox1.TabIndex = 66;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "FILTRO DE FECHAS";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(22, 30);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(68, 15);
		this.label5.TabIndex = 70;
		this.label5.Text = "Vendedor";
		this.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVendedor.FormattingEnabled = true;
		this.cmbVendedor.Location = new System.Drawing.Point(25, 49);
		this.cmbVendedor.Margin = new System.Windows.Forms.Padding(4);
		this.cmbVendedor.Name = "cmbVendedor";
		this.cmbVendedor.Size = new System.Drawing.Size(426, 24);
		this.cmbVendedor.TabIndex = 69;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(25, 103);
		this.cmbFormaPago.Margin = new System.Windows.Forms.Padding(4);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(154, 24);
		this.cmbFormaPago.TabIndex = 68;
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.cmbFormaPago);
		this.groupBox2.Controls.Add(this.cmbVendedor);
		this.groupBox2.Location = new System.Drawing.Point(12, 129);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(475, 159);
		this.groupBox2.TabIndex = 71;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "VENDEDOR Y FORMA DE PAGO";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(22, 84);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(105, 15);
		this.label2.TabIndex = 71;
		this.label2.Text = "Forma de Pago";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(498, 336);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentxVendedor";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte de Ventas por Vendedor";
		base.Load += new System.EventHandler(frmParamVentxVendedor_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
