using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamVentxArticulo : Office2007Form
{
	private clsReporteVentaxArticulo ds = new clsReporteVentaxArticulo();

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmVendedor admVen = new clsAdmVendedor();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private IContainer components = null;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha1;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private Label label8;

	private Button btnCancelar;

	private Button btnReporte;

	private GroupBox groupBox4;

	public TextBox txtUnArt;

	private RadioButton rbArt;

	private RadioButton rbTodosArt;

	public TextBox txtArticulo;

	public TextBox txtCodProducto;

	private ComboBox cmbVendedor;

	private Label label3;

	private Label label7;

	private ComboBox cmbMoneda;

	public frmParamVentxArticulo()
	{
		InitializeComponent();
	}

	private void frmParamVentxArticulo_Load(object sender, EventArgs e)
	{
		CargaVendedores();
		CargaMoneda();
		cmbMoneda.SelectedIndex = 0;
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void CargaVendedores()
	{
		cmbVendedor.DataSource = admVen.CargaVendedoresReporte();
		cmbVendedor.DisplayMember = "apellido";
		cmbVendedor.ValueMember = "codVendedor";
		cmbVendedor.SelectedIndex = 0;
	}

	private void rbArt_CheckedChanged(object sender, EventArgs e)
	{
		txtUnArt.Text = "";
		txtArticulo.Text = "";
		txtUnArt.Enabled = rbArt.Checked;
		txtUnArt.Focus();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRVentaxArticulo rpt = new CRVentaxArticulo();
		FrmRptVentaxArticulo frm = new FrmRptVentaxArticulo();
		rpt.SetDataSource(ds.Reporte(Convert.ToInt32(txtCodProducto.Text), dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, Convert.ToInt32(cmbVendedor.SelectedValue), frmLogin.iCodAlmacen, Convert.ToInt32(cmbMoneda.SelectedValue)).Tables[0]);
		frm.crvVentaxArticulo.ReportSource = rpt;
		frm.Show();
	}

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 15;
			if (frm.ShowDialog() == DialogResult.OK && frm.pro.CodProducto != 0)
			{
				CargaProducto(frm.pro.CodProducto);
			}
		}
	}

	private void CargaProducto(int Codigo)
	{
		pro = AdmPro.CargaProducto(Codigo, frmLogin.iCodAlmacen);
		txtUnArt.Text = pro.Referencia;
		txtCodProducto.Text = pro.CodProducto.ToString();
		txtArticulo.Text = pro.Descripcion;
	}

	private void txtUnArt_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtUnArt.Text != "" && BuscaProducto())
		{
			ProcessTabKey(forward: true);
		}
	}

	private bool BuscaProducto()
	{
		pro = AdmPro.CargaProductoDetalleR(txtUnArt.Text, frmLogin.iCodAlmacen, 1, 0);
		if (pro != null)
		{
			txtCodProducto.Text = pro.CodProducto.ToString();
			txtUnArt.Text = pro.Referencia;
			txtArticulo.Text = pro.Descripcion;
			return true;
		}
		txtCodProducto.Text = "";
		txtUnArt.Text = "";
		txtArticulo.Text = "";
		return false;
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
		this.label8 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.cmbVendedor);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(371, 105);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos";
		this.cmbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVendedor.FormattingEnabled = true;
		this.cmbVendedor.Location = new System.Drawing.Point(245, 34);
		this.cmbVendedor.Name = "cmbVendedor";
		this.cmbVendedor.Size = new System.Drawing.Size(107, 20);
		this.cmbVendedor.TabIndex = 67;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(243, 19);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 66;
		this.label3.Text = "Vendedor";
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(130, 19);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 12);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(132, 34);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha2.TabIndex = 28;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(19, 34);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha1.TabIndex = 38;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(17, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(308, 233);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(227, 233);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox4.Controls.Add(this.txtCodProducto);
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(12, 124);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(371, 102);
		this.groupBox4.TabIndex = 62;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Por Artículo";
		this.txtCodProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProducto.Enabled = false;
		this.txtCodProducto.Location = new System.Drawing.Point(194, 39);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.Size = new System.Drawing.Size(84, 20);
		this.txtCodProducto.TabIndex = 64;
		this.txtCodProducto.Text = "0";
		this.txtCodProducto.Visible = false;
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(104, 65);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(248, 20);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Enabled = false;
		this.txtUnArt.Location = new System.Drawing.Point(104, 39);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(84, 20);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.txtUnArt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnArt_KeyPress);
		this.rbArt.AutoSize = true;
		this.rbArt.BackColor = System.Drawing.Color.Transparent;
		this.rbArt.Location = new System.Drawing.Point(19, 42);
		this.rbArt.Name = "rbArt";
		this.rbArt.Size = new System.Drawing.Size(79, 17);
		this.rbArt.TabIndex = 57;
		this.rbArt.Text = "Un Artículo";
		this.rbArt.UseVisualStyleBackColor = false;
		this.rbArt.CheckedChanged += new System.EventHandler(rbArt_CheckedChanged);
		this.rbTodosArt.AutoSize = true;
		this.rbTodosArt.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosArt.Checked = true;
		this.rbTodosArt.Location = new System.Drawing.Point(19, 19);
		this.rbTodosArt.Name = "rbTodosArt";
		this.rbTodosArt.Size = new System.Drawing.Size(115, 17);
		this.rbTodosArt.TabIndex = 54;
		this.rbTodosArt.TabStop = true;
		this.rbTodosArt.Text = "Todos los artículos";
		this.rbTodosArt.UseVisualStyleBackColor = false;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(17, 59);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 12);
		this.label7.TabIndex = 69;
		this.label7.Text = "Moneda";
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(19, 74);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(99, 20);
		this.cmbMoneda.TabIndex = 68;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(396, 267);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentxArticulo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Ventas por Articulo";
		base.Load += new System.EventHandler(frmParamVentxArticulo_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
