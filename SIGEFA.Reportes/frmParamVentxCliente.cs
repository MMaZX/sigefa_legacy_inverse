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

public class frmParamVentxCliente : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsReporteVentxCliente ds = new clsReporteVentxCliente();

	public clsCliente cli = new clsCliente();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private int Tipo = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label2;

	private ComboBox cmbFormaPago;

	private DateTimePicker dtpFecha1;

	private Label label1;

	private GroupBox groupBox2;

	private DateTimePicker dtpFecha2;

	private Label label7;

	private ComboBox cmbMoneda;

	private Label label8;

	private Button btnCancelar;

	private Button btnReporte;

	private RadioButton rbTodosCli;

	private RadioButton rbCli;

	public TextBox txtUnCli;

	private GroupBox groupBox4;

	public TextBox txtUnArt;

	private RadioButton rbArt;

	private RadioButton rbTodosArt;

	public TextBox txtCliente;

	public TextBox txtArticulo;

	public TextBox txtCodCli;

	public TextBox txtCodProd;

	public frmParamVentxCliente()
	{
		InitializeComponent();
	}

	private void frmParamVentxCliente_Load(object sender, EventArgs e)
	{
		CargaFormaPagos();
		cmbFormaPago.SelectedIndex = 0;
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

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = -1;
	}

	private void rbCli_CheckedChanged(object sender, EventArgs e)
	{
		txtUnCli.Text = "";
		txtCliente.Text = "";
		txtUnCli.Enabled = rbCli.Checked;
		txtUnCli.Focus();
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
		CRVentxCliente rpt = new CRVentxCliente();
		frmRptVentxCliente frm = new frmRptVentxCliente();
		rpt.SetDataSource(ds.ReporteVentasxCliente(frmLogin.iCodAlmacen, dtpFecha1.Value, dtpFecha2.Value, rbTodosCli.Checked, rbTodosArt.Checked, txtUnArt.Text, txtUnCli.Text).Tables[0]);
		frm.crvRptVentxCliente.ReportSource = rpt;
		frm.Show();
	}

	private void txtUnCli_KeyDown(object sender, KeyEventArgs e)
	{
		if (rbCli.Checked && e.KeyCode == Keys.F1)
		{
			frmClientesLista frm = new frmClientesLista();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				CargaCliente(frm.GetCodigoCliente());
			}
			else
			{
				txtUnCli.Focus();
			}
		}
	}

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		if (rbArt.Checked && e.KeyCode == Keys.F1)
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
		txtArticulo.Text = pro.Descripcion;
		txtCodProd.Text = pro.CodProducto.ToString();
	}

	private void CargaCliente(int Codigo)
	{
		cli = AdmCli.MuestraCliente(Codigo);
		txtUnCli.Text = cli.RucDni;
		txtCodCli.Text = cli.CodCliente.ToString();
		txtCliente.Text = cli.RazonSocial;
	}

	private void txtUnCli_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtUnCli.Text != "")
		{
			if (BuscaCliente())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "Facturacion Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtUnCli.Text, Tipo);
		if (cli != null)
		{
			txtUnCli.Text = cli.RucDni;
			txtCliente.Text = cli.RazonSocial;
			txtCodCli.Text = cli.CodCliente.ToString();
			return true;
		}
		txtUnCli.Text = "";
		txtCliente.Text = "";
		txtCodCli.Text = "";
		return false;
	}

	private void txtUnCli_TextChanged(object sender, EventArgs e)
	{
		txtCliente.Text = "";
		txtCodCli.Text = "";
	}

	private void txtUnArt_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtUnArt.Text != "")
		{
			if (BuscaProducto())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaProducto()
	{
		pro = AdmPro.CargaProductoDetalleR(txtUnArt.Text, frmLogin.iCodAlmacen, 1, 0);
		if (pro != null)
		{
			txtCodProd.Text = pro.CodProducto.ToString();
			txtUnArt.Text = pro.Referencia;
			txtArticulo.Text = pro.Descripcion;
			return true;
		}
		txtCodProd.Text = "";
		txtUnArt.Text = "";
		txtArticulo.Text = "";
		return false;
	}

	private void txtUnArt_TextChanged(object sender, EventArgs e)
	{
		txtCodProd.Text = "";
		txtArticulo.Text = "";
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
		this.label7 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtCodCli = new System.Windows.Forms.TextBox();
		this.txtCliente = new System.Windows.Forms.TextBox();
		this.txtUnCli = new System.Windows.Forms.TextBox();
		this.rbCli = new System.Windows.Forms.RadioButton();
		this.rbTodosCli = new System.Windows.Forms.RadioButton();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtCodProd = new System.Windows.Forms.TextBox();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(482, 102);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos";
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(17, 58);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 12);
		this.label7.TabIndex = 47;
		this.label7.Text = "Moneda";
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(19, 73);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(99, 20);
		this.cmbMoneda.TabIndex = 46;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(235, 16);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 12);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(235, 58);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(79, 12);
		this.label2.TabIndex = 43;
		this.label2.Text = "Forma de pago";
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(237, 73);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(239, 20);
		this.cmbFormaPago.TabIndex = 42;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(237, 31);
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
		this.label1.Location = new System.Drawing.Point(17, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.groupBox2.Controls.Add(this.txtCodCli);
		this.groupBox2.Controls.Add(this.txtCliente);
		this.groupBox2.Controls.Add(this.txtUnCli);
		this.groupBox2.Controls.Add(this.rbCli);
		this.groupBox2.Controls.Add(this.rbTodosCli);
		this.groupBox2.Location = new System.Drawing.Point(12, 120);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(482, 67);
		this.groupBox2.TabIndex = 11;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Por Cliente";
		this.txtCodCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCli.Enabled = false;
		this.txtCodCli.Location = new System.Drawing.Point(446, 39);
		this.txtCodCli.Name = "txtCodCli";
		this.txtCodCli.Size = new System.Drawing.Size(30, 20);
		this.txtCodCli.TabIndex = 69;
		this.txtCodCli.Visible = false;
		this.txtCliente.Enabled = false;
		this.txtCliente.Location = new System.Drawing.Point(194, 39);
		this.txtCliente.Name = "txtCliente";
		this.txtCliente.Size = new System.Drawing.Size(248, 20);
		this.txtCliente.TabIndex = 62;
		this.txtUnCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnCli.Enabled = false;
		this.txtUnCli.Location = new System.Drawing.Point(104, 39);
		this.txtUnCli.Name = "txtUnCli";
		this.txtUnCli.Size = new System.Drawing.Size(84, 20);
		this.txtUnCli.TabIndex = 61;
		this.txtUnCli.TextChanged += new System.EventHandler(txtUnCli_TextChanged);
		this.txtUnCli.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnCli_KeyDown);
		this.txtUnCli.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnCli_KeyPress);
		this.rbCli.AutoSize = true;
		this.rbCli.BackColor = System.Drawing.Color.Transparent;
		this.rbCli.Location = new System.Drawing.Point(19, 42);
		this.rbCli.Name = "rbCli";
		this.rbCli.Size = new System.Drawing.Size(74, 17);
		this.rbCli.TabIndex = 57;
		this.rbCli.Text = "Un Cliente";
		this.rbCli.UseVisualStyleBackColor = false;
		this.rbCli.CheckedChanged += new System.EventHandler(rbCli_CheckedChanged);
		this.rbTodosCli.AutoSize = true;
		this.rbTodosCli.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosCli.Checked = true;
		this.rbTodosCli.Location = new System.Drawing.Point(19, 19);
		this.rbTodosCli.Name = "rbTodosCli";
		this.rbTodosCli.Size = new System.Drawing.Size(110, 17);
		this.rbTodosCli.TabIndex = 54;
		this.rbTodosCli.TabStop = true;
		this.rbTodosCli.Text = "Todos los clientes";
		this.rbTodosCli.UseVisualStyleBackColor = false;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(389, 266);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(308, 266);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox4.Controls.Add(this.txtCodProd);
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(12, 193);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(482, 67);
		this.groupBox4.TabIndex = 62;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Por Artículo";
		this.txtCodProd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProd.Enabled = false;
		this.txtCodProd.Location = new System.Drawing.Point(446, 39);
		this.txtCodProd.Name = "txtCodProd";
		this.txtCodProd.Size = new System.Drawing.Size(30, 20);
		this.txtCodProd.TabIndex = 69;
		this.txtCodProd.Visible = false;
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(194, 39);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(248, 20);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Enabled = false;
		this.txtUnArt.Location = new System.Drawing.Point(104, 39);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(84, 20);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.TextChanged += new System.EventHandler(txtUnArt_TextChanged);
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(518, 294);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentxCliente";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reporte de Ventas por Cliente / Articulo";
		base.Load += new System.EventHandler(frmParamVentxCliente_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
