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

public class frmParamVentxArticuloxVendedor : Office2007Form
{
	private clsReporteVentxCliente ds = new clsReporteVentxCliente();

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmVendedor admVen = new clsAdmVendedor();

	public int codArticulo1;

	public int codArticulo2;

	public string Referencia1;

	public string Referencia2;

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

	public TextBox txtArticulo2;

	public TextBox txtUnArt2;

	private Label label3;

	private Label label4;

	private ComboBox cmbVendedor;

	private Label label2;

	public TextBox txtRan2;

	public TextBox txtRan1;

	public frmParamVentxArticuloxVendedor()
	{
		InitializeComponent();
	}

	private void frmParamVentxCliente_Load(object sender, EventArgs e)
	{
		CargaVendedores();
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
		txtUnArt2.Text = "";
		txtArticulo2.Text = "";
		txtUnArt.Enabled = rbArt.Checked;
		txtUnArt2.Enabled = rbArt.Checked;
		txtUnArt.Focus();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRVentxArtixVend rpt = new CRVentxArtixVend();
		frmRptVentxArtixVendedor frm = new frmRptVentxArtixVendedor();
		DataTable dt = ds.Reporte22(frmLogin.iCodAlmacen, dtpFecha1.Value, dtpFecha2.Value, Convert.ToInt32(cmbVendedor.SelectedValue), txtUnArt.Text, txtUnArt2.Text, rbTodosArt.Checked, rbArt.Checked).Tables[0];
		if (dt.Rows.Count > 0)
		{
			rpt.SetDataSource(dt);
			frm.crvRptVentxCliente.ReportSource = rpt;
			frm.Show();
		}
	}

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosListaReport frm = new frmProductosListaReport();
			frm.Proceso = 3;
			frm.Inicio = 0;
			frm.codAlmacen = Convert.ToInt32(frmLogin.iCodAlmacen);
			if (frm.ShowDialog() == DialogResult.Yes)
			{
				codArticulo1 = frm.pro.CodProducto;
				Referencia1 = frm.pro.Referencia;
				txtUnArt.Text = frm.pro.Referencia;
				txtArticulo.Text = frm.pro.Descripcion;
				txtRan1.Text = frm.pro.CodProducto.ToString();
			}
		}
	}

	private void CargaProducto(int Codigo)
	{
		pro = AdmPro.CargaProducto(Codigo, frmLogin.iCodAlmacen);
		txtUnArt.Text = pro.Referencia;
		txtArticulo.Text = pro.Descripcion;
	}

	private void txtUnArt2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosListaReport frm = new frmProductosListaReport();
			frm.Proceso = 3;
			frm.Inicio = codArticulo1;
			frm.codAlmacen = frmLogin.iCodAlmacen;
			if (frm.ShowDialog() == DialogResult.Yes)
			{
				codArticulo2 = frm.pro.CodProducto;
				Referencia2 = frm.pro.Referencia;
				txtUnArt2.Text = frm.pro.Referencia;
				txtArticulo2.Text = frm.pro.Descripcion;
				txtRan2.Text = frm.pro.CodProducto.ToString();
			}
		}
	}

	private void txtUnArt_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtUnArt.Text != "")
		{
			if (BuscaProducto(txtUnArt.Text))
			{
				ProcessTabKey(forward: true);
				codArticulo1 = pro.CodProducto;
				Referencia1 = pro.Referencia;
				txtUnArt.Text = pro.Referencia;
				txtArticulo.Text = pro.Descripcion;
				txtRan1.Text = pro.CodProducto.ToString();
			}
			else
			{
				MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void txtUnArt2_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtUnArt2.Text != "")
		{
			if (BuscaProducto(txtUnArt2.Text))
			{
				ProcessTabKey(forward: true);
				codArticulo2 = pro.CodProducto;
				Referencia2 = pro.Referencia;
				txtUnArt2.Text = pro.Referencia;
				txtArticulo2.Text = pro.Descripcion;
				txtRan2.Text = pro.CodProducto.ToString();
			}
			else
			{
				MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaProducto(string referencia)
	{
		pro = AdmPro.CargaProductoDetalleR(referencia, frmLogin.iCodAlmacen, 2, 0);
		if (pro != null)
		{
			return true;
		}
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
		this.label2 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtRan2 = new System.Windows.Forms.TextBox();
		this.txtRan1 = new System.Windows.Forms.TextBox();
		this.txtArticulo2 = new System.Windows.Forms.TextBox();
		this.txtUnArt2 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cmbVendedor);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(471, 69);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos";
		this.cmbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVendedor.FormattingEnabled = true;
		this.cmbVendedor.Location = new System.Drawing.Point(229, 31);
		this.cmbVendedor.Name = "cmbVendedor";
		this.cmbVendedor.Size = new System.Drawing.Size(166, 20);
		this.cmbVendedor.TabIndex = 67;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(227, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(53, 12);
		this.label2.TabIndex = 66;
		this.label2.Text = "Vendedor";
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(122, 16);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 12);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(124, 31);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha2.TabIndex = 28;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(19, 31);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha1.TabIndex = 38;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(17, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(408, 238);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(327, 238);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox4.Controls.Add(this.txtRan2);
		this.groupBox4.Controls.Add(this.txtRan1);
		this.groupBox4.Controls.Add(this.txtArticulo2);
		this.groupBox4.Controls.Add(this.txtUnArt2);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(12, 87);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(471, 140);
		this.groupBox4.TabIndex = 62;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Por Artículo";
		this.txtRan2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRan2.Enabled = false;
		this.txtRan2.Location = new System.Drawing.Point(435, 92);
		this.txtRan2.Name = "txtRan2";
		this.txtRan2.Size = new System.Drawing.Size(30, 20);
		this.txtRan2.TabIndex = 69;
		this.txtRan2.Visible = false;
		this.txtRan1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRan1.Enabled = false;
		this.txtRan1.Location = new System.Drawing.Point(435, 66);
		this.txtRan1.Name = "txtRan1";
		this.txtRan1.Size = new System.Drawing.Size(30, 20);
		this.txtRan1.TabIndex = 68;
		this.txtRan1.Visible = false;
		this.txtArticulo2.Enabled = false;
		this.txtArticulo2.Location = new System.Drawing.Point(184, 92);
		this.txtArticulo2.Name = "txtArticulo2";
		this.txtArticulo2.Size = new System.Drawing.Size(248, 20);
		this.txtArticulo2.TabIndex = 67;
		this.txtUnArt2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt2.Enabled = false;
		this.txtUnArt2.Location = new System.Drawing.Point(94, 92);
		this.txtUnArt2.Name = "txtUnArt2";
		this.txtUnArt2.Size = new System.Drawing.Size(84, 20);
		this.txtUnArt2.TabIndex = 66;
		this.txtUnArt2.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt2_KeyDown);
		this.txtUnArt2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnArt2_KeyPress);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Enabled = false;
		this.label3.Location = new System.Drawing.Point(61, 95);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(27, 13);
		this.label3.TabIndex = 65;
		this.label3.Text = "Fin :";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Enabled = false;
		this.label4.Location = new System.Drawing.Point(50, 69);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(38, 13);
		this.label4.TabIndex = 64;
		this.label4.Text = "Inicio :";
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(184, 66);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(248, 20);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Enabled = false;
		this.txtUnArt.Location = new System.Drawing.Point(94, 66);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(84, 20);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.txtUnArt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnArt_KeyPress);
		this.rbArt.AutoSize = true;
		this.rbArt.BackColor = System.Drawing.Color.Transparent;
		this.rbArt.Location = new System.Drawing.Point(19, 42);
		this.rbArt.Name = "rbArt";
		this.rbArt.Size = new System.Drawing.Size(57, 17);
		this.rbArt.TabIndex = 57;
		this.rbArt.Text = "Rango";
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(495, 273);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentxArticuloxVendedor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Ventas Vendedor / Articulo";
		base.Load += new System.EventHandler(frmParamVentxCliente_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
