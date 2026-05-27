using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmFlujoCajaRegistro : Office2007Form
{
	public int CodFlujoCaja = 0;

	public int Proceso = 0;

	public int Procede = 0;

	private clsAdmFlujoCaja AdmFlu = new clsAdmFlujoCaja();

	private clsFlujoCaja flu = new clsFlujoCaja();

	private clsValidar ok = new clsValidar();

	private clsCaja aper = new clsCaja();

	private clsAdmAperturaCierre AdmAper = new clsAdmAperturaCierre();

	private clsSucursal suc = new clsSucursal();

	private clsAdmSucursal admSuc = new clsAdmSucursal();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private Label label1;

	public TextBox txtconcepto;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private ComboBox cmbtipopagoser;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Button btnGuardar;

	private Button btnSalir;

	public DateTimePicker dtpfecha;

	public ComboBox cboTipo;

	public TextBox txtmonto;

	public frmFlujoCajaRegistro()
	{
		InitializeComponent();
	}

	private void HabilitaControles(bool Estado)
	{
		txtconcepto.Enabled = Estado;
		txtmonto.Enabled = Estado;
		dtpfecha.Enabled = Estado;
		cboTipo.Enabled = Estado;
		btnGuardar.Enabled = Estado;
		cmbtipopagoser.Enabled = Estado;
	}

	private void carga()
	{
		cmbtipopagoser.DataSource = AdmFlu.ListaPagoCobro(0);
		cmbtipopagoser.DisplayMember = "descripcion";
		cmbtipopagoser.ValueMember = "codtipopagoserv";
		cmbtipopagoser.SelectedIndex = -1;
	}

	private void frmFlujoCajaRegistro_Load(object sender, EventArgs e)
	{
		carga();
		if (Proceso == 1 || Proceso == 2)
		{
			HabilitaControles(Estado: true);
		}
		else if (Proceso == 3)
		{
			HabilitaControles(Estado: false);
		}
		if (Procede == 2)
		{
			posiciona_elementos();
			dtpfecha.MinDate = Convert.ToDateTime(DateTime.Now);
			dtpfecha.MaxDate = Convert.ToDateTime(DateTime.Now);
			dtpfecha.Value = Convert.ToDateTime(DateTime.Now);
		}
	}

	private void posiciona_elementos()
	{
		suc = admSuc.CargaSucursal(frmLogin.iCodSucursal);
		label2.Location = new Point(16, 128);
		txtmonto.Location = new Point(19, 144);
		label3.Location = new Point(169, 128);
		dtpfecha.Location = new Point(172, 144);
		label4.Visible = false;
		cboTipo.Visible = false;
		label5.Visible = false;
		cmbtipopagoser.Visible = false;
		btnGuardar.Location = new Point(138, 8);
		btnSalir.Location = new Point(221, 8);
		groupBox1.Size = new Size(467, 191);
		groupBox2.Size = new Size(468, 44);
		txtconcepto.Enabled = false;
		txtconcepto.Text = "APERTURA INICIAL DE CAJA DE LA SUCURSAL " + suc.Nombre;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (!superValidator1.Validate() || Proceso == 0)
			{
				return;
			}
			flu.CodSucursal = frmLogin.iCodSucursal;
			flu.FechaApertura = dtpfecha.Value;
			flu.MontoApertura = Convert.ToDecimal(txtmonto.Text);
			flu.CodUser = frmLogin.iCodUser;
			if (Proceso == 1)
			{
				if (AdmFlu.Insert(flu))
				{
					MessageBox.Show("Los datos se Guardaron Correctamente", "APERTURA CAJA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					frmListaCaja frm = (frmListaCaja)Application.OpenForms["frmListaCaja"];
					frm.biIngreso.Visible = false;
					frm.biStatusCaja.Visible = true;
					frm.VerificaSaldoCaja();
					frm.CalculaTotales();
					Close();
				}
				else
				{
					MessageBox.Show("Ocurrio un Error al Guardar los Datos", "CONTROL DE FLUJO DE CAJA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else if (Proceso == 2)
			{
				flu.CodFlujoCaja = CodFlujoCaja;
				if (AdmFlu.Update(flu))
				{
					MessageBox.Show("Los datos se Modificaron Correctamente", "CONTROL DE FLUJO DE CAJA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("Ocurrio un Error al Actualizar los Datos", "CONTROL DE FLUJO DE CAJA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message);
		}
	}

	private void cboTipo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cboTipo.SelectedIndex == 1 || cboTipo.SelectedIndex == 0)
		{
			cmbtipopagoser.Enabled = false;
			cmbtipopagoser.SelectedValue = 0;
		}
		else if (cboTipo.SelectedIndex == 2)
		{
			carga();
			cmbtipopagoser.Enabled = true;
		}
		btnGuardar.Enabled = true;
	}

	private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtmonto_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void txtmonto_Leave(object sender, EventArgs e)
	{
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Procede == 2)
		{
			return;
		}
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (c.SelectedIndex != -1)
				{
					e.IsValid = true;
				}
				else
				{
					e.IsValid = false;
				}
			}
			else
			{
				e.IsValid = true;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void txtconcepto_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.letras(e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmFlujoCajaRegistro));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbtipopagoser = new System.Windows.Forms.ComboBox();
		this.cboTipo = new System.Windows.Forms.ComboBox();
		this.dtpfecha = new System.Windows.Forms.DateTimePicker();
		this.txtmonto = new System.Windows.Forms.TextBox();
		this.txtconcepto = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox1.Controls.Add(this.cmbtipopagoser);
		this.groupBox1.Controls.Add(this.cboTipo);
		this.groupBox1.Controls.Add(this.dtpfecha);
		this.groupBox1.Controls.Add(this.txtmonto);
		this.groupBox1.Controls.Add(this.txtconcepto);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(10, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(826, 191);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.cmbtipopagoser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbtipopagoser.FormattingEnabled = true;
		this.cmbtipopagoser.Location = new System.Drawing.Point(513, 144);
		this.cmbtipopagoser.Name = "cmbtipopagoser";
		this.cmbtipopagoser.Size = new System.Drawing.Size(302, 21);
		this.cmbtipopagoser.TabIndex = 9;
		this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboTipo.FormattingEnabled = true;
		this.cboTipo.Items.AddRange(new object[3] { "-- SELECCIONAR TIPO --", "INGRESO", "EGRESO" });
		this.cboTipo.Location = new System.Drawing.Point(513, 94);
		this.cboTipo.Name = "cboTipo";
		this.cboTipo.Size = new System.Drawing.Size(302, 21);
		this.cboTipo.TabIndex = 8;
		this.cboTipo.SelectionChangeCommitted += new System.EventHandler(cboTipo_SelectionChangeCommitted);
		this.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha.Location = new System.Drawing.Point(666, 44);
		this.dtpfecha.Name = "dtpfecha";
		this.dtpfecha.Size = new System.Drawing.Size(149, 20);
		this.dtpfecha.TabIndex = 7;
		this.txtmonto.Location = new System.Drawing.Point(513, 44);
		this.txtmonto.Name = "txtmonto";
		this.txtmonto.Size = new System.Drawing.Size(121, 20);
		this.txtmonto.TabIndex = 6;
		this.superValidator1.SetValidator1(this.txtmonto, this.customValidator2);
		this.txtmonto.Leave += new System.EventHandler(txtmonto_Leave);
		this.txtmonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtmonto_KeyPress);
		this.txtconcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtconcepto.Location = new System.Drawing.Point(19, 44);
		this.txtconcepto.MaxLength = 100;
		this.txtconcepto.Multiline = true;
		this.txtconcepto.Name = "txtconcepto";
		this.txtconcepto.Size = new System.Drawing.Size(432, 71);
		this.txtconcepto.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtconcepto, this.customValidator1);
		this.txtconcepto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtconcepto_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(510, 128);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(97, 13);
		this.label5.TabIndex = 4;
		this.label5.Text = "Tipo Pago Servicio";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(512, 78);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(85, 13);
		this.label4.TabIndex = 3;
		this.label4.Text = "Tipo Movimiento";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(663, 28);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(37, 13);
		this.label3.TabIndex = 2;
		this.label3.Text = "Fecha";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(510, 28);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Monto";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Concepto";
		this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
		this.groupBox2.Controls.Add(this.btnGuardar);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Location = new System.Drawing.Point(10, 197);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(826, 44);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(621, 14);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 30);
		this.btnGuardar.TabIndex = 39;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(8, "close.png");
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(704, 14);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 30);
		this.btnSalir.TabIndex = 40;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.customValidator2.ErrorMessage = "Ingrese Monto";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese Concepto";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(847, 243);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmFlujoCajaRegistro";
		this.Text = "FLUJO DE CAJA - Nuevo Registro";
		base.Load += new System.EventHandler(frmFlujoCajaRegistro_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
