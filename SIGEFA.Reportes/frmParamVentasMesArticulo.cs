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

public class frmParamVentasMesArticulo : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsReporteVentaMesArticulo ds = new clsReporteVentaMesArticulo();

	private clsAdmMoneda admMon = new clsAdmMoneda();

	private clsValidar ok = new clsValidar();

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private int m1 = 0;

	private int m2 = 0;

	public int criterio = 0;

	private IContainer components = null;

	private Button btnCancelar;

	private Button btnReporte;

	private GroupBox groupBox1;

	private Label label7;

	private ComboBox cmbMoneda;

	private Label label2;

	private ComboBox cmbFormaPago;

	private Label label9;

	private ComboBox cmbEmpresa;

	private ComboBox cmbCriterio;

	private GroupBox groupBox4;

	public TextBox txtArticulo;

	public TextBox txtUnArt;

	private RadioButton rbArt;

	private RadioButton rbTodosArt;

	private Label label8;

	private Label label1;

	private ComboBox cmbMes2;

	private ComboBox cmbMes1;

	private Label label3;

	private Label label4;

	private ComboBox cmbAños;

	public frmParamVentasMesArticulo()
	{
		InitializeComponent();
	}

	private void frmParamVentasMesArticulo_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		CargaFormaPagos();
		CargaMoneda();
		LlenarComboAños();
		cmbMes1.SelectedIndex = 0;
		cmbMes2.SelectedIndex = 0;
		cmbCriterio.SelectedIndex = criterio;
		posiciona_botones();
	}

	private void posiciona_botones()
	{
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = admMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
	}

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedIndex = 0;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagosReporte();
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
	}

	private void rbArt_CheckedChanged(object sender, EventArgs e)
	{
		txtUnArt.Text = "";
		txtUnArt.Enabled = rbArt.Checked;
		txtUnArt.Focus();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		if (!cmbAños.SelectedItem.Equals(""))
		{
			m1 = cmbMes1.SelectedIndex + 1;
			m2 = cmbMes2.SelectedIndex + 1;
			CRVentaMesArticulo rpt = new CRVentaMesArticulo();
			frmRptVentaMesArticulo frm = new frmRptVentaMesArticulo();
			rpt.SetDataSource(ds.Reporte(m1, m2, Convert.ToInt32(cmbFormaPago.SelectedValue), cmbCriterio.SelectedIndex, txtUnArt.Text, rbTodosArt.Checked, Convert.ToInt32(cmbMoneda.SelectedValue), frmLogin.iCodAlmacen, Convert.ToInt32(cmbAños.SelectedItem)).Tables[0]);
			frm.crvRptVentaMesArticulo.ReportSource = rpt;
			frm.Show();
		}
		else
		{
			MessageBox.Show("¡Ingrese Año!", "Reporte de Mes por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void txtAño_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 15;
			if (frm.ShowDialog() == DialogResult.OK && frm.pro.CodProducto != 0)
			{
				CargaProducto(frm.GetCodigoProducto());
			}
		}
	}

	private void CargaProducto(int Codigo)
	{
		pro = AdmPro.CargaProductoDetalle(Codigo, frmLogin.iCodAlmacen, 2, 0, 0);
		if (pro != null)
		{
			txtUnArt.Text = pro.Referencia;
			txtArticulo.Text = pro.Descripcion;
		}
		else
		{
			MessageBox.Show("Producto no encontrado", "Reporte de Mes por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
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
		pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtUnArt.Text), frmLogin.iCodAlmacen, 2, 0, 0);
		if (pro != null)
		{
			txtUnArt.Text = pro.Referencia;
			txtArticulo.Text = pro.Descripcion;
			return true;
		}
		txtUnArt.Text = "";
		txtArticulo.Text = "";
		return false;
	}

	public void LlenarComboAños()
	{
		int fechactual = DateTime.Now.Year;
		do
		{
			cmbAños.Items.Add(fechactual);
			fechactual--;
		}
		while (fechactual >= 2012);
		cmbAños.SelectedIndex = 0;
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void rbTodosArt_CheckedChanged(object sender, EventArgs e)
	{
		txtArticulo.Text = "";
		txtUnArt.Text = "";
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
		this.cmbAños = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbMes2 = new System.Windows.Forms.ComboBox();
		this.cmbMes1 = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbCriterio = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(516, 260);
		this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(100, 28);
		this.btnCancelar.TabIndex = 66;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(408, 260);
		this.btnReporte.Margin = new System.Windows.Forms.Padding(4);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 28);
		this.btnReporte.TabIndex = 65;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox1.Controls.Add(this.cmbAños);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.cmbMes2);
		this.groupBox1.Controls.Add(this.cmbMes1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbCriterio);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbEmpresa);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox1.Size = new System.Drawing.Size(603, 140);
		this.groupBox1.TabIndex = 63;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos";
		this.cmbAños.FormattingEnabled = true;
		this.cmbAños.Location = new System.Drawing.Point(297, 50);
		this.cmbAños.Margin = new System.Windows.Forms.Padding(4);
		this.cmbAños.Name = "cmbAños";
		this.cmbAños.Size = new System.Drawing.Size(133, 24);
		this.cmbAños.TabIndex = 60;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(154, 82);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(96, 16);
		this.label4.TabIndex = 59;
		this.label4.Text = "Tipo Articulo";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(294, 28);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 16);
		this.label3.TabIndex = 57;
		this.label3.Text = "Año";
		this.cmbMes2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMes2.FormattingEnabled = true;
		this.cmbMes2.Items.AddRange(new object[12]
		{
			"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE",
			"NOVIEMBRE", "DICIEMBRE"
		});
		this.cmbMes2.Location = new System.Drawing.Point(157, 50);
		this.cmbMes2.Margin = new System.Windows.Forms.Padding(4);
		this.cmbMes2.Name = "cmbMes2";
		this.cmbMes2.Size = new System.Drawing.Size(132, 24);
		this.cmbMes2.TabIndex = 56;
		this.cmbMes1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMes1.FormattingEnabled = true;
		this.cmbMes1.Items.AddRange(new object[12]
		{
			"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE",
			"NOVIEMBRE", "DICIEMBRE"
		});
		this.cmbMes1.Location = new System.Drawing.Point(17, 50);
		this.cmbMes1.Margin = new System.Windows.Forms.Padding(4);
		this.cmbMes1.Name = "cmbMes1";
		this.cmbMes1.Size = new System.Drawing.Size(132, 24);
		this.cmbMes1.TabIndex = 55;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(155, 28);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(49, 16);
		this.label8.TabIndex = 54;
		this.label8.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(14, 28);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 16);
		this.label1.TabIndex = 51;
		this.label1.Text = "Desde";
		this.cmbCriterio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbCriterio.FormattingEnabled = true;
		this.cmbCriterio.Items.AddRange(new object[6] { "ARTICULO", "FAMILIA", "LINEA", "GRUPO", "CLIENTE", "VENDEDOR" });
		this.cmbCriterio.Location = new System.Drawing.Point(157, 102);
		this.cmbCriterio.Margin = new System.Windows.Forms.Padding(4);
		this.cmbCriterio.Name = "cmbCriterio";
		this.cmbCriterio.Size = new System.Drawing.Size(132, 24);
		this.cmbCriterio.TabIndex = 50;
		this.label7.AutoSize = true;
		this.label7.BackColor = System.Drawing.Color.Transparent;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(15, 83);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(64, 16);
		this.label7.TabIndex = 47;
		this.label7.Text = "Moneda";
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(18, 102);
		this.cmbMoneda.Margin = new System.Windows.Forms.Padding(4);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(131, 24);
		this.cmbMoneda.TabIndex = 46;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(294, 82);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(114, 16);
		this.label2.TabIndex = 43;
		this.label2.Text = "Forma de pago";
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(297, 102);
		this.cmbFormaPago.Margin = new System.Windows.Forms.Padding(4);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(272, 24);
		this.cmbFormaPago.TabIndex = 42;
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(15, 144);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(70, 16);
		this.label9.TabIndex = 37;
		this.label9.Text = "Empresa";
		this.label9.Visible = false;
		this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(18, 166);
		this.cmbEmpresa.Margin = new System.Windows.Forms.Padding(4);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(131, 24);
		this.cmbEmpresa.TabIndex = 36;
		this.cmbEmpresa.Visible = false;
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(13, 160);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox4.Size = new System.Drawing.Size(603, 92);
		this.groupBox4.TabIndex = 67;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Por Artículo";
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(258, 52);
		this.txtArticulo.Margin = new System.Windows.Forms.Padding(4);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(329, 22);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Enabled = false;
		this.txtUnArt.Location = new System.Drawing.Point(139, 52);
		this.txtUnArt.Margin = new System.Windows.Forms.Padding(4);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(111, 22);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.txtUnArt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnArt_KeyPress);
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
		this.rbTodosArt.CheckedChanged += new System.EventHandler(rbTodosArt_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(631, 299);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParamVentasMesArticulo";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte de Ventas de Articulo por Mes";
		base.Load += new System.EventHandler(frmParamVentasMesArticulo_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
