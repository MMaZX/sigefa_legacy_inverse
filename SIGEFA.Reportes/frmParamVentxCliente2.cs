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

public class frmParamVentxCliente2 : Office2007Form
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

	private DateTimePicker dtpFecha1;

	private Label label1;

	private GroupBox groupBox2;

	private DateTimePicker dtpFecha2;

	private Label label8;

	private Button btnCancelar;

	private Button btnReporte;

	private RadioButton rbTodosCli;

	private RadioButton rbCli;

	public TextBox txtUnCli;

	public TextBox txtCliente;

	public TextBox txtCodCli;

	private RadioButton rbTodosArt;

	private RadioButton rbArt;

	public TextBox txtUnArt;

	public TextBox txtArticulo;

	public TextBox txtCodProd;

	private GroupBox groupBox4;

	public frmParamVentxCliente2()
	{
		InitializeComponent();
	}

	private void frmParamVentxCliente2_Load(object sender, EventArgs e)
	{
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRVentxCliente2 rpt = new CRVentxCliente2();
		frmRptVentxCliente frm = new frmRptVentxCliente();
		rpt.SetDataSource(ds.ReporteVentasxCliente(frmLogin.iCodAlmacen, dtpFecha1.Value, dtpFecha2.Value, rbTodosCli.Checked, rbTodosArt.Checked, txtUnArt.Text, txtUnCli.Text).Tables[0]);
		frm.crvRptVentxCliente.ReportSource = rpt;
		frm.Show();
	}

	private void frmParamVentxCliente2_Load_1(object sender, EventArgs e)
	{
		dtpFecha1.Value = DateTime.Now;
		dtpFecha2.Value = DateTime.Now;
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

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (rbArt.Checked && e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 15;
			if (frm.ShowDialog() == DialogResult.OK && frm.pro.CodProducto != 0)
			{
				CargaProducto(frm.pro.CodProducto);
			}
		}
		Cursor = Cursors.Default;
	}

	private void txtUnCli_TextChanged(object sender, EventArgs e)
	{
		txtCliente.Text = "";
		txtCodCli.Text = "";
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

	private void txtUnCli_KeyDown(object sender, KeyEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (rbCli.Checked && e.KeyCode == Keys.F1)
		{
			frmClientesLista frm = new frmClientesLista();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				CargaCliente(frm.GetCodigoCliente());
				Cursor = Cursors.Default;
			}
			else
			{
				txtUnCli.Focus();
			}
		}
		Cursor = Cursors.Default;
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
		this.label8 = new System.Windows.Forms.Label();
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
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtCodProd = new System.Windows.Forms.TextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(16, 15);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox1.Size = new System.Drawing.Size(643, 84);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "FILTRO DE FECHAS";
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(313, 20);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(49, 16);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(316, 38);
		this.dtpFecha2.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha2.TabIndex = 28;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(25, 40);
		this.dtpFecha1.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha1.TabIndex = 38;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(23, 20);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 16);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.groupBox2.Controls.Add(this.txtCodCli);
		this.groupBox2.Controls.Add(this.txtCliente);
		this.groupBox2.Controls.Add(this.txtUnCli);
		this.groupBox2.Controls.Add(this.rbCli);
		this.groupBox2.Controls.Add(this.rbTodosCli);
		this.groupBox2.Location = new System.Drawing.Point(16, 107);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox2.Size = new System.Drawing.Size(643, 82);
		this.groupBox2.TabIndex = 11;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "CLIENTE";
		this.txtCodCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCli.Enabled = false;
		this.txtCodCli.Location = new System.Drawing.Point(595, 48);
		this.txtCodCli.Margin = new System.Windows.Forms.Padding(4);
		this.txtCodCli.Name = "txtCodCli";
		this.txtCodCli.Size = new System.Drawing.Size(39, 22);
		this.txtCodCli.TabIndex = 69;
		this.txtCodCli.Visible = false;
		this.txtCliente.Enabled = false;
		this.txtCliente.Location = new System.Drawing.Point(259, 48);
		this.txtCliente.Margin = new System.Windows.Forms.Padding(4);
		this.txtCliente.Name = "txtCliente";
		this.txtCliente.Size = new System.Drawing.Size(329, 22);
		this.txtCliente.TabIndex = 62;
		this.txtUnCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnCli.Enabled = false;
		this.txtUnCli.Location = new System.Drawing.Point(139, 48);
		this.txtUnCli.Margin = new System.Windows.Forms.Padding(4);
		this.txtUnCli.Name = "txtUnCli";
		this.txtUnCli.Size = new System.Drawing.Size(111, 22);
		this.txtUnCli.TabIndex = 61;
		this.txtUnCli.TextChanged += new System.EventHandler(txtUnCli_TextChanged);
		this.txtUnCli.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnCli_KeyDown);
		this.txtUnCli.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnCli_KeyPress);
		this.rbCli.AutoSize = true;
		this.rbCli.BackColor = System.Drawing.Color.Transparent;
		this.rbCli.Location = new System.Drawing.Point(25, 52);
		this.rbCli.Margin = new System.Windows.Forms.Padding(4);
		this.rbCli.Name = "rbCli";
		this.rbCli.Size = new System.Drawing.Size(87, 20);
		this.rbCli.TabIndex = 57;
		this.rbCli.Text = "Un Cliente";
		this.rbCli.UseVisualStyleBackColor = false;
		this.rbCli.CheckedChanged += new System.EventHandler(rbCli_CheckedChanged);
		this.rbTodosCli.AutoSize = true;
		this.rbTodosCli.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosCli.Checked = true;
		this.rbTodosCli.Location = new System.Drawing.Point(25, 23);
		this.rbTodosCli.Margin = new System.Windows.Forms.Padding(4);
		this.rbTodosCli.Name = "rbTodosCli";
		this.rbTodosCli.Size = new System.Drawing.Size(136, 20);
		this.rbTodosCli.TabIndex = 54;
		this.rbTodosCli.TabStop = true;
		this.rbTodosCli.Text = "Todos los clientes";
		this.rbTodosCli.UseVisualStyleBackColor = false;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(559, 287);
		this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(100, 28);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(451, 287);
		this.btnReporte.Margin = new System.Windows.Forms.Padding(4);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 28);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.rbTodosArt.AutoSize = true;
		this.rbTodosArt.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosArt.Checked = true;
		this.rbTodosArt.Location = new System.Drawing.Point(25, 23);
		this.rbTodosArt.Margin = new System.Windows.Forms.Padding(4);
		this.rbTodosArt.Name = "rbTodosArt";
		this.rbTodosArt.Size = new System.Drawing.Size(140, 20);
		this.rbTodosArt.TabIndex = 54;
		this.rbTodosArt.TabStop = true;
		this.rbTodosArt.Text = "Todos los artículos";
		this.rbTodosArt.UseVisualStyleBackColor = false;
		this.rbArt.AutoSize = true;
		this.rbArt.BackColor = System.Drawing.Color.Transparent;
		this.rbArt.Location = new System.Drawing.Point(25, 52);
		this.rbArt.Margin = new System.Windows.Forms.Padding(4);
		this.rbArt.Name = "rbArt";
		this.rbArt.Size = new System.Drawing.Size(90, 20);
		this.rbArt.TabIndex = 57;
		this.rbArt.Text = "Un Artículo";
		this.rbArt.UseVisualStyleBackColor = false;
		this.rbArt.CheckedChanged += new System.EventHandler(rbArt_CheckedChanged);
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Enabled = false;
		this.txtUnArt.Location = new System.Drawing.Point(139, 48);
		this.txtUnArt.Margin = new System.Windows.Forms.Padding(4);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(111, 22);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.TextChanged += new System.EventHandler(txtUnArt_TextChanged);
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.txtUnArt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnArt_KeyPress);
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(259, 48);
		this.txtArticulo.Margin = new System.Windows.Forms.Padding(4);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(329, 22);
		this.txtArticulo.TabIndex = 63;
		this.txtCodProd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodProd.Enabled = false;
		this.txtCodProd.Location = new System.Drawing.Point(595, 48);
		this.txtCodProd.Margin = new System.Windows.Forms.Padding(4);
		this.txtCodProd.Name = "txtCodProd";
		this.txtCodProd.Size = new System.Drawing.Size(39, 22);
		this.txtCodProd.TabIndex = 69;
		this.txtCodProd.Visible = false;
		this.groupBox4.Controls.Add(this.txtCodProd);
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(16, 197);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox4.Size = new System.Drawing.Size(643, 82);
		this.groupBox4.TabIndex = 62;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "ARTÍCULO";
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(675, 331);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentxCliente2";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte de Ventas por Cliente / Articulo";
		base.Load += new System.EventHandler(frmParamVentxCliente2_Load_1);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
