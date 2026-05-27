using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionPrestamoBancario : Form
{
	private clsAdmMoneda AdmMoned = new clsAdmMoneda();

	private clsAdmBanco AdmBan = new clsAdmBanco();

	private clsValidar val = new clsValidar();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	public clsPrestamoBancario presBanc = new clsPrestamoBancario();

	private clsAdmPrestamoBancario admPreBan = new clsAdmPrestamoBancario();

	public int Proceso = 0;

	private IContainer components = null;

	private TabControl tabControl1;

	private Button btnCancelar;

	private Button btnAceptar;

	private TabPage tabPage1;

	private TextBox txtDescripcion;

	private TextBox txtPrestamo;

	private TextBox txtCodigo;

	private ComboBox cbMoneda;

	private ComboBox cbBanco;

	private Label label8;

	private Label label6;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private ImageList imageList1;

	private TextBox textInteres;

	private Label label13;

	private DateTimePicker dtFecVencimiento;

	private Label label5;

	private DateTimePicker dtFecAprobacion;

	private TextBox txtDevolver;

	private Label label14;

	private TextBox txtTipoCambio;

	private Label label7;

	public frmGestionPrestamoBancario()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (cbMoneda.SelectedIndex > -1 && cbBanco.SelectedIndex > -1 && Convert.ToDecimal(txtPrestamo.Text) > 0m)
		{
			presBanc.CodBanco = Convert.ToInt32(cbBanco.SelectedValue);
			presBanc.CodMoneda = Convert.ToInt32(cbMoneda.SelectedValue);
			presBanc.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
			presBanc.Montoprestamo = Convert.ToDecimal(txtPrestamo.Text);
			presBanc.Montointeres = Convert.ToDecimal(textInteres.Text);
			presBanc.Montodevolver = Convert.ToDecimal(txtDevolver.Text);
			presBanc.Fechaaprobacion = dtFecAprobacion.Value;
			presBanc.Fechavencimiento = dtFecVencimiento.Value;
			presBanc.Descripcion = txtDescripcion.Text;
			if ((Proceso == 1 || Proceso == 3 || Proceso == 4) && admPreBan.insert(presBanc))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		else
		{
			MessageBox.Show("Datos Incompletos", "Prestamo Bancario", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cargaMoneda()
	{
		cbMoneda.DataSource = AdmMoned.ListaMonedas();
		cbMoneda.DisplayMember = "descripcion";
		cbMoneda.ValueMember = "codMoneda";
	}

	private void CargarBancos()
	{
		try
		{
			cbBanco.DataSource = AdmBan.MuestraBancos();
			cbBanco.DisplayMember = "descripcion";
			cbBanco.ValueMember = "codbanco";
			cbBanco.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void calculaDevolver()
	{
		decimal total = Convert.ToDecimal(txtPrestamo.Text) + Convert.ToDecimal(textInteres.Text);
		txtDevolver.Text = $"{total:#,##0.00}";
	}

	private void frmGestionPrestamoBancario_Load(object sender, EventArgs e)
	{
		cargaMoneda();
		CargarBancos();
		txtPrestamo.Text = "0.00";
		textInteres.Text = "0.00";
		tc = AdmTc.CargaTipoCambio(DateTime.Today.Date, 2);
		txtTipoCambio.Text = tc.Compra.ToString();
	}

	private void txtPrestamo_TextChanged(object sender, EventArgs e)
	{
		if (txtPrestamo.Text == "")
		{
			txtPrestamo.Text = "0.00";
		}
	}

	private void txtPrestamo_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void textInteres_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void textInteres_TextChanged(object sender, EventArgs e)
	{
		if (textInteres.Text == "")
		{
			textInteres.Text = "0.00";
		}
	}

	private void txtPrestamo_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void frmGestionPrestamoBancario_Shown(object sender, EventArgs e)
	{
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtPrestamo_Leave(object sender, EventArgs e)
	{
		calculaDevolver();
	}

	private void textInteres_Leave(object sender, EventArgs e)
	{
		calculaDevolver();
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionPrestamoBancario));
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.txtDevolver = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.textInteres = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.dtFecVencimiento = new System.Windows.Forms.DateTimePicker();
		this.label5 = new System.Windows.Forms.Label();
		this.dtFecAprobacion = new System.Windows.Forms.DateTimePicker();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtPrestamo = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.cbMoneda = new System.Windows.Forms.ComboBox();
		this.cbBanco = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		base.SuspendLayout();
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Location = new System.Drawing.Point(12, 12);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(633, 193);
		this.tabControl1.TabIndex = 2;
		this.tabPage1.Controls.Add(this.txtTipoCambio);
		this.tabPage1.Controls.Add(this.label7);
		this.tabPage1.Controls.Add(this.txtDevolver);
		this.tabPage1.Controls.Add(this.label14);
		this.tabPage1.Controls.Add(this.textInteres);
		this.tabPage1.Controls.Add(this.label13);
		this.tabPage1.Controls.Add(this.dtFecVencimiento);
		this.tabPage1.Controls.Add(this.label5);
		this.tabPage1.Controls.Add(this.dtFecAprobacion);
		this.tabPage1.Controls.Add(this.txtDescripcion);
		this.tabPage1.Controls.Add(this.txtPrestamo);
		this.tabPage1.Controls.Add(this.txtCodigo);
		this.tabPage1.Controls.Add(this.cbMoneda);
		this.tabPage1.Controls.Add(this.cbBanco);
		this.tabPage1.Controls.Add(this.label8);
		this.tabPage1.Controls.Add(this.label6);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.label3);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(625, 167);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Datos Generales";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.txtDevolver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDevolver.Location = new System.Drawing.Point(516, 46);
		this.txtDevolver.MaxLength = 11;
		this.txtDevolver.Name = "txtDevolver";
		this.txtDevolver.Size = new System.Drawing.Size(97, 20);
		this.txtDevolver.TabIndex = 5;
		this.txtDevolver.Tag = "5";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(428, 49);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(89, 13);
		this.label14.TabIndex = 28;
		this.label14.Text = "Monto Devolver :";
		this.textInteres.Location = new System.Drawing.Point(303, 46);
		this.textInteres.MaxLength = 11;
		this.textInteres.Name = "textInteres";
		this.textInteres.Size = new System.Drawing.Size(97, 20);
		this.textInteres.TabIndex = 4;
		this.textInteres.Tag = "4";
		this.textInteres.TextChanged += new System.EventHandler(textInteres_TextChanged);
		this.textInteres.Leave += new System.EventHandler(textInteres_Leave);
		this.textInteres.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textInteres_KeyPress);
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(221, 49);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(78, 13);
		this.label13.TabIndex = 26;
		this.label13.Text = "Monto Interes :";
		this.dtFecVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtFecVencimiento.Location = new System.Drawing.Point(345, 73);
		this.dtFecVencimiento.Name = "dtFecVencimiento";
		this.dtFecVencimiento.Size = new System.Drawing.Size(97, 20);
		this.dtFecVencimiento.TabIndex = 7;
		this.dtFecVencimiento.Tag = "7";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(221, 76);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(118, 13);
		this.label5.TabIndex = 24;
		this.label5.Text = "Ultima Fecha de Pago :";
		this.dtFecAprobacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtFecAprobacion.Location = new System.Drawing.Point(116, 73);
		this.dtFecAprobacion.Name = "dtFecAprobacion";
		this.dtFecAprobacion.Size = new System.Drawing.Size(97, 20);
		this.dtFecAprobacion.TabIndex = 6;
		this.dtFecAprobacion.Tag = "6";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(89, 104);
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(524, 42);
		this.txtDescripcion.TabIndex = 8;
		this.txtDescripcion.Tag = "8";
		this.txtPrestamo.Location = new System.Drawing.Point(103, 46);
		this.txtPrestamo.MaxLength = 11;
		this.txtPrestamo.Name = "txtPrestamo";
		this.txtPrestamo.Size = new System.Drawing.Size(93, 20);
		this.txtPrestamo.TabIndex = 3;
		this.txtPrestamo.Tag = "3";
		this.txtPrestamo.TextChanged += new System.EventHandler(txtPrestamo_TextChanged);
		this.txtPrestamo.Leave += new System.EventHandler(txtPrestamo_Leave);
		this.txtPrestamo.KeyUp += new System.Windows.Forms.KeyEventHandler(txtPrestamo_KeyUp);
		this.txtPrestamo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrestamo_KeyPress);
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(67, 17);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(68, 20);
		this.txtCodigo.TabIndex = 0;
		this.cbMoneda.FormattingEnabled = true;
		this.cbMoneda.Location = new System.Drawing.Point(492, 17);
		this.cbMoneda.Name = "cbMoneda";
		this.cbMoneda.Size = new System.Drawing.Size(121, 21);
		this.cbMoneda.TabIndex = 2;
		this.cbMoneda.Tag = "2";
		this.cbBanco.FormattingEnabled = true;
		this.cbBanco.Location = new System.Drawing.Point(207, 17);
		this.cbBanco.Name = "cbBanco";
		this.cbBanco.Size = new System.Drawing.Size(202, 21);
		this.cbBanco.TabIndex = 1;
		this.cbBanco.Tag = "1";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(440, 20);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(52, 13);
		this.label8.TabIndex = 12;
		this.label8.Text = "Moneda :";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(162, 20);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(44, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Banco :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 107);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Descripción :";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 76);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Fecha Aprobacion :";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 49);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(88, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Monto Prestado :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Codigo :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.imageList1.Images.SetKeyName(3, "sunat (1).png");
		this.imageList1.Images.SetKeyName(4, "sunat.png");
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(554, 216);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 10;
		this.btnCancelar.Tag = "10";
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(473, 216);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 9;
		this.btnAceptar.Tag = "9";
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.txtTipoCambio.Location = new System.Drawing.Point(535, 72);
		this.txtTipoCambio.MaxLength = 11;
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.Size = new System.Drawing.Size(78, 20);
		this.txtTipoCambio.TabIndex = 30;
		this.txtTipoCambio.Tag = "3";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(460, 76);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(72, 13);
		this.label7.TabIndex = 29;
		this.label7.Text = "Tipo Cambio :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(647, 247);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.tabControl1);
		base.Name = "frmGestionPrestamoBancario";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Prestamo Bancario";
		base.Load += new System.EventHandler(frmGestionPrestamoBancario_Load);
		base.Shown += new System.EventHandler(frmGestionPrestamoBancario_Shown);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		base.ResumeLayout(false);
	}
}
