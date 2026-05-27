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

public class UtilidadVentas : Office2007Form
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

	private GroupBox groupBox1;

	private Label label2;

	private ComboBox cmbFormaPago;

	private DateTimePicker dtpFecha1;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private Label label8;

	private Button btnCancelar;

	private Button btnReporte;

	private ComboBox cmbVendedor;

	private Label label3;

	private Label label7;

	private ComboBox cmbZona;

	public UtilidadVentas()
	{
		InitializeComponent();
	}

	private void UtilidadVentas_Load_1(object sender, EventArgs e)
	{
		label3.Location = new Point(17, 58);
		cmbVendedor.Location = new Point(19, 78);
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
		try
		{
			CRVentasUtilidad rpt = new CRVentasUtilidad();
			frmRptVentxVendedor frm = new frmRptVentxVendedor();
			DataTable dt = ds.ReporteUtilidad(frmLogin.iCodAlmacen, dtpFecha1.Value, dtpFecha2.Value, Convert.ToInt32(cmbFormaPago.SelectedValue), Convert.ToInt32(cmbVendedor.SelectedValue)).Tables[0];
			if (dt.Rows.Count > 0)
			{
				rpt.SetDataSource(dt);
				frm.crvRptVentxVendedor.ReportSource = rpt;
				frm.Show();
			}
			else
			{
				MessageBox.Show("No se encontró resultados con los parámetros seleccionados", "Reporte Utilidad Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbVendedor = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbZona = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cmbVendedor);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.cmbZona);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(16, 15);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.groupBox1.Size = new System.Drawing.Size(435, 88);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "FILTRO DE FECHAS";
		this.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVendedor.FormattingEnabled = true;
		this.cmbVendedor.Location = new System.Drawing.Point(316, 109);
		this.cmbVendedor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmbVendedor.Name = "cmbVendedor";
		this.cmbVendedor.Size = new System.Drawing.Size(195, 24);
		this.cmbVendedor.TabIndex = 65;
		this.cmbVendedor.Visible = false;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(314, 90);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(76, 16);
		this.label3.TabIndex = 64;
		this.label3.Text = "Vendedor";
		this.label3.Visible = false;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(23, 93);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(43, 16);
		this.label7.TabIndex = 47;
		this.label7.Text = "Zona";
		this.label7.Visible = false;
		this.cmbZona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbZona.FormattingEnabled = true;
		this.cmbZona.Location = new System.Drawing.Point(25, 112);
		this.cmbZona.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmbZona.Name = "cmbZona";
		this.cmbZona.Size = new System.Drawing.Size(131, 20);
		this.cmbZona.TabIndex = 46;
		this.cmbZona.Visible = false;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(247, 23);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(49, 16);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(314, 142);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(114, 16);
		this.label2.TabIndex = 43;
		this.label2.Text = "Forma de pago";
		this.label2.Visible = false;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(317, 161);
		this.cmbFormaPago.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(194, 24);
		this.cmbFormaPago.TabIndex = 42;
		this.cmbFormaPago.Visible = false;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(250, 43);
		this.dtpFecha2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha2.TabIndex = 28;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(25, 43);
		this.dtpFecha1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha1.TabIndex = 38;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(24, 23);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 16);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(351, 120);
		this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(100, 28);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(243, 120);
		this.btnReporte.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 28);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(470, 160);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "UtilidadVentas";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte de Ventas ";
		base.Load += new System.EventHandler(UtilidadVentas_Load_1);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
