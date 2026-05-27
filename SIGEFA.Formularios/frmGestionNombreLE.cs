using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionNombreLE : Office2007Form
{
	public int Proceso = 0;

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	public clsEmpresa emp = new clsEmpresa();

	private clsAdmLibrosElectronicos admlibro = new clsAdmLibrosElectronicos();

	private clsLibrosElectronicos libro = new clsLibrosElectronicos();

	private clsRegistroElectronico registro = new clsRegistroElectronico();

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsAdmMoneda admMoneda = new clsAdmMoneda();

	public DateTime Hoy = DateTime.Today;

	private decimal BI = default(decimal);

	private decimal DBI = default(decimal);

	public int contenidoLibro;

	public int Contenido;

	public int TipoLibro = 0;

	public int RegistroLibro = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox txtCodEmpresa;

	private Label label2;

	private Label label4;

	private TextBox txtRUC;

	private Label label3;

	private TextBox txtNombreNomenclatura;

	private Label label7;

	private Label label6;

	private Label label5;

	private Button btnCancelar;

	private Button btnGenerarNombre;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private LabelX labelX1;

	private ComboBox cmbOperaciones;

	private TextBox txtoperaciones;

	private Label label12;

	private TextBox txtCodOportunidad;

	private Label label11;

	private TextBox txtIndicadorLE;

	private Label label10;

	private Label label9;

	private ComboBox cmbRegistroElectronico;

	private ComboBox cmbLibrosElectronicos;

	private Label label8;

	private ComboBox cmbGenerado;

	private TextBox txtGenerado;

	private Label label15;

	private ComboBox cmbMoneda;

	private TextBox txtMoneda;

	private Label label14;

	private ComboBox cmbContenido;

	private TextBox txtContenido;

	private Label label13;

	private Button btnAceptar;

	public TextBox txtAnio;

	public TextBox txtDia;

	public TextBox txtMes;

	public frmGestionNombreLE()
	{
		InitializeComponent();
	}

	private void CargaEmpresa()
	{
		emp = admEmp.CargaEmpresa(frmLogin.iCodEmpresa);
		txtRUC.Text = emp.Ruc;
	}

	private void CargaLibrosElectronicos()
	{
		cmbLibrosElectronicos.DataSource = admlibro.CargaLibrosElectronicos();
		cmbLibrosElectronicos.DisplayMember = "descripcion";
		cmbLibrosElectronicos.ValueMember = "codlibro";
		cmbLibrosElectronicos.SelectedIndex = -1;
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtRUC_Leave(object sender, EventArgs e)
	{
		if (Proceso == 1 && txtCodEmpresa.Text == "" && admEmp.VerificaRUC(txtRUC.Text))
		{
			MessageBox.Show("El RUC ingresado ya se encuentra registrado", "Gestion Empresa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			txtRUC.Focus();
			ext.limpiar(groupBox1.Controls);
		}
	}

	private void txtRUC_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F2 && !ext.rucsunat(txtRUC.Text))
		{
			ext.limpiar(base.Controls);
		}
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Text.Length == 11)
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void CargaRegistrosElectronicos(int codlibro)
	{
		cmbRegistroElectronico.DataSource = admlibro.CargaRegistrosElectronicos(codlibro);
		cmbRegistroElectronico.DisplayMember = "descripcion";
		cmbRegistroElectronico.ValueMember = "codlibroregistro";
		cmbRegistroElectronico.SelectedIndex = -1;
	}

	private void frmGestionNombreLE_Load(object sender, EventArgs e)
	{
		CargaEmpresa();
		CargaLibrosElectronicos();
		CargaMoneda();
		CargaOperaciones();
		CargaContenido();
		CargaGenerado();
	}

	private void CargaGenerado()
	{
		cmbGenerado.DataSource = admlibro.CargaGeneradoPor();
		cmbGenerado.DisplayMember = "descripcion";
		cmbGenerado.ValueMember = "codigo";
		cmbGenerado.SelectedIndex = -1;
	}

	private void CargaContenido()
	{
		cmbContenido.DataSource = admlibro.CargaContenido();
		cmbContenido.DisplayMember = "descripcion";
		cmbContenido.ValueMember = "codigo";
		cmbContenido.SelectedIndex = -1;
	}

	private void CargaOperaciones()
	{
		cmbOperaciones.DataSource = admlibro.CargaOperaciones();
		cmbOperaciones.DisplayMember = "descripcion";
		cmbOperaciones.ValueMember = "codigo";
		cmbOperaciones.SelectedIndex = -1;
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = admMoneda.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = -1;
	}

	private void cmbLibrosElectronicos_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if ((int)cmbLibrosElectronicos.SelectedValue <= 0)
		{
			return;
		}
		libro = admlibro.MuestraLE((int)cmbLibrosElectronicos.SelectedValue);
		if (libro == null)
		{
			return;
		}
		CargaRegistrosElectronicos(libro.Codlibro);
		if (libro.Aplicaanio == 1)
		{
			txtAnio.Text = Hoy.Year.ToString();
		}
		else
		{
			txtAnio.Text = "AAAA";
		}
		if (libro.Aplicames == 1)
		{
			int mes = 0;
			mes = Convert.ToInt32(Hoy.Month);
			if (mes > 0 && mes < 13)
			{
				if (mes < 10)
				{
					txtMes.Text = "0" + mes;
				}
				else
				{
					txtMes.Text = mes.ToString();
				}
			}
		}
		else
		{
			txtMes.Text = "00";
		}
		if (libro.Aplicadia == 1)
		{
			int dia = 0;
			dia = Convert.ToInt32(Hoy.Day);
			if (dia > 0 && dia < 32)
			{
				if (dia < 10)
				{
					txtMes.Text = "0" + dia;
				}
				else
				{
					txtMes.Text = dia.ToString();
				}
			}
		}
		else
		{
			txtDia.Text = "00";
		}
		if (libro.Aplicaoportunidad == 1)
		{
			MessageBox.Show("solo para inventario y balance");
		}
		else
		{
			txtCodOportunidad.Text = "00";
		}
	}

	private void cmbRegistroElectronico_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if ((int)cmbRegistroElectronico.SelectedValue > 0)
		{
			registro = admlibro.MuestraRE((int)cmbRegistroElectronico.SelectedValue);
			if (registro != null)
			{
				txtIndicadorLE.Text = registro.Codigo;
			}
		}
	}

	private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if ((int)cmbMoneda.SelectedValue > 0)
		{
			txtMoneda.Text = cmbMoneda.SelectedValue.ToString();
		}
	}

	private void cmbOperaciones_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbOperaciones.SelectedValue) != -1)
		{
			txtoperaciones.Text = cmbOperaciones.SelectedValue.ToString();
		}
	}

	private void cmbContenido_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbContenido.SelectedValue) != -1)
		{
			txtContenido.Text = cmbContenido.SelectedValue.ToString();
		}
	}

	private void cmbGenerado_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbGenerado.SelectedValue) != -1)
		{
			txtGenerado.Text = cmbGenerado.SelectedValue.ToString();
		}
	}

	private void btnGenerarNombre_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtRUC.Text != "")
			{
				string NomenclaturaArchivo = "";
				if (txtCodEmpresa.Text != "")
				{
					NomenclaturaArchivo = ((!(cmbLibrosElectronicos.Text == "Registro de Compras")) ? (NomenclaturaArchivo + txtCodEmpresa.Text + txtRUC.Text + txtAnio.Text + txtMes.Text + txtDia.Text + txtIndicadorLE.Text + txtCodOportunidad.Text + txtoperaciones.Text + txtContenido.Text + txtMoneda.Text + txtGenerado.Text) : (NomenclaturaArchivo + txtCodEmpresa.Text + txtRUC.Text + txtAnio.Text + txtMes.Text + "00" + txtIndicadorLE.Text + txtCodOportunidad.Text + txtoperaciones.Text + txtContenido.Text + txtMoneda.Text + txtGenerado.Text));
				}
				txtNombreNomenclatura.Text = NomenclaturaArchivo;
				btnAceptar.Visible = true;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message);
		}
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			TipoLibro = Convert.ToInt32(cmbLibrosElectronicos.SelectedValue);
			RegistroLibro = Convert.ToInt32(cmbRegistroElectronico.SelectedValue);
			Contenido = Convert.ToInt32(txtContenido.Text);
			if (TipoLibro == 8)
			{
				if (admlibro.ValidaCampoTipoFacturacion(Convert.ToInt32(txtMes.Text), Convert.ToInt32(txtAnio.Text)) != 0)
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message);
		}
	}

	public string GetNombre()
	{
		return txtNombreNomenclatura.Text;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionNombreLE));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbGenerado = new System.Windows.Forms.ComboBox();
		this.txtGenerado = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.txtMoneda = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cmbContenido = new System.Windows.Forms.ComboBox();
		this.txtContenido = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.cmbOperaciones = new System.Windows.Forms.ComboBox();
		this.txtoperaciones = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtCodOportunidad = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtIndicadorLE = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtDia = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbRegistroElectronico = new System.Windows.Forms.ComboBox();
		this.cmbLibrosElectronicos = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtNombreNomenclatura = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtMes = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtAnio = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtRUC = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtCodEmpresa = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnGenerarNombre = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cmbGenerado);
		this.groupBox1.Controls.Add(this.txtGenerado);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.txtMoneda);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.cmbContenido);
		this.groupBox1.Controls.Add(this.txtContenido);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.cmbOperaciones);
		this.groupBox1.Controls.Add(this.txtoperaciones);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.txtCodOportunidad);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.txtIndicadorLE);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtDia);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cmbRegistroElectronico);
		this.groupBox1.Controls.Add(this.cmbLibrosElectronicos);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtNombreNomenclatura);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtMes);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtAnio);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtRUC);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtCodEmpresa);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(9, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(661, 352);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.cmbGenerado.FormattingEnabled = true;
		this.cmbGenerado.Location = new System.Drawing.Point(390, 246);
		this.cmbGenerado.Name = "cmbGenerado";
		this.cmbGenerado.Size = new System.Drawing.Size(133, 21);
		this.cmbGenerado.TabIndex = 33;
		this.cmbGenerado.SelectionChangeCommitted += new System.EventHandler(cmbGenerado_SelectionChangeCommitted);
		this.txtGenerado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtGenerado.Location = new System.Drawing.Point(529, 247);
		this.txtGenerado.MaxLength = 20;
		this.txtGenerado.Name = "txtGenerado";
		this.txtGenerado.Size = new System.Drawing.Size(58, 20);
		this.txtGenerado.TabIndex = 31;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(293, 250);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(78, 13);
		this.label15.TabIndex = 32;
		this.label15.Text = "Generado por :";
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(103, 245);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(121, 21);
		this.cmbMoneda.TabIndex = 30;
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.txtMoneda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtMoneda.Location = new System.Drawing.Point(228, 246);
		this.txtMoneda.MaxLength = 20;
		this.txtMoneda.Name = "txtMoneda";
		this.txtMoneda.Size = new System.Drawing.Size(41, 20);
		this.txtMoneda.TabIndex = 28;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(52, 247);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(52, 13);
		this.label14.TabIndex = 29;
		this.label14.Text = "Moneda :";
		this.cmbContenido.FormattingEnabled = true;
		this.cmbContenido.Location = new System.Drawing.Point(390, 214);
		this.cmbContenido.Name = "cmbContenido";
		this.cmbContenido.Size = new System.Drawing.Size(133, 21);
		this.cmbContenido.TabIndex = 27;
		this.cmbContenido.SelectionChangeCommitted += new System.EventHandler(cmbContenido_SelectionChangeCommitted);
		this.txtContenido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtContenido.Location = new System.Drawing.Point(529, 215);
		this.txtContenido.MaxLength = 20;
		this.txtContenido.Name = "txtContenido";
		this.txtContenido.Size = new System.Drawing.Size(58, 20);
		this.txtContenido.TabIndex = 25;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(222, 217);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(137, 13);
		this.label13.TabIndex = 26;
		this.label13.Text = "Identificador de Contenido :";
		this.cmbOperaciones.FormattingEnabled = true;
		this.cmbOperaciones.Location = new System.Drawing.Point(390, 188);
		this.cmbOperaciones.Name = "cmbOperaciones";
		this.cmbOperaciones.Size = new System.Drawing.Size(133, 21);
		this.cmbOperaciones.TabIndex = 24;
		this.cmbOperaciones.SelectionChangeCommitted += new System.EventHandler(cmbOperaciones_SelectionChangeCommitted);
		this.txtoperaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtoperaciones.Location = new System.Drawing.Point(529, 189);
		this.txtoperaciones.MaxLength = 20;
		this.txtoperaciones.Name = "txtoperaciones";
		this.txtoperaciones.Size = new System.Drawing.Size(58, 20);
		this.txtoperaciones.TabIndex = 22;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(222, 191);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(149, 13);
		this.label12.TabIndex = 23;
		this.label12.Text = "Identificador de Operaciones :";
		this.txtCodOportunidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodOportunidad.Location = new System.Drawing.Point(529, 163);
		this.txtCodOportunidad.MaxLength = 20;
		this.txtCodOportunidad.Name = "txtCodOportunidad";
		this.txtCodOportunidad.Size = new System.Drawing.Size(58, 20);
		this.txtCodOportunidad.TabIndex = 20;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(427, 166);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(96, 13);
		this.label11.TabIndex = 21;
		this.label11.Text = "Cod. Oportunidad :";
		this.txtIndicadorLE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtIndicadorLE.Location = new System.Drawing.Point(315, 163);
		this.txtIndicadorLE.MaxLength = 20;
		this.txtIndicadorLE.Name = "txtIndicadorLE";
		this.txtIndicadorLE.Size = new System.Drawing.Size(74, 20);
		this.txtIndicadorLE.TabIndex = 18;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(222, 166);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(87, 13);
		this.label10.TabIndex = 19;
		this.label10.Text = "Identificador LE :";
		this.txtDia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDia.Location = new System.Drawing.Point(103, 215);
		this.txtDia.MaxLength = 20;
		this.txtDia.Name = "txtDia";
		this.txtDia.Size = new System.Drawing.Size(76, 20);
		this.txtDia.TabIndex = 16;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(64, 218);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(29, 13);
		this.label9.TabIndex = 17;
		this.label9.Text = "Dia :";
		this.cmbRegistroElectronico.FormattingEnabled = true;
		this.cmbRegistroElectronico.Location = new System.Drawing.Point(55, 129);
		this.cmbRegistroElectronico.Name = "cmbRegistroElectronico";
		this.cmbRegistroElectronico.Size = new System.Drawing.Size(532, 21);
		this.cmbRegistroElectronico.TabIndex = 15;
		this.cmbRegistroElectronico.SelectionChangeCommitted += new System.EventHandler(cmbRegistroElectronico_SelectionChangeCommitted);
		this.cmbLibrosElectronicos.FormattingEnabled = true;
		this.cmbLibrosElectronicos.Location = new System.Drawing.Point(55, 82);
		this.cmbLibrosElectronicos.Name = "cmbLibrosElectronicos";
		this.cmbLibrosElectronicos.Size = new System.Drawing.Size(532, 21);
		this.cmbLibrosElectronicos.TabIndex = 14;
		this.cmbLibrosElectronicos.SelectionChangeCommitted += new System.EventHandler(cmbLibrosElectronicos_SelectionChangeCommitted);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(312, 36);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(92, 13);
		this.label8.TabIndex = 13;
		this.label8.Text = "Deudor Tributario ";
		this.txtNombreNomenclatura.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreNomenclatura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreNomenclatura.ForeColor = System.Drawing.Color.Red;
		this.txtNombreNomenclatura.Location = new System.Drawing.Point(55, 309);
		this.txtNombreNomenclatura.Multiline = true;
		this.txtNombreNomenclatura.Name = "txtNombreNomenclatura";
		this.txtNombreNomenclatura.Size = new System.Drawing.Size(532, 37);
		this.txtNombreNomenclatura.TabIndex = 7;
		this.txtNombreNomenclatura.Text = ".";
		this.txtNombreNomenclatura.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(53, 284);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(145, 20);
		this.label7.TabIndex = 12;
		this.label7.Text = "Nombre Archivo :";
		this.txtMes.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtMes.Location = new System.Drawing.Point(103, 189);
		this.txtMes.MaxLength = 20;
		this.txtMes.Name = "txtMes";
		this.txtMes.Size = new System.Drawing.Size(76, 20);
		this.txtMes.TabIndex = 5;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(64, 192);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(33, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Mes :";
		this.txtAnio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtAnio.Location = new System.Drawing.Point(103, 163);
		this.txtAnio.MaxLength = 20;
		this.txtAnio.Name = "txtAnio";
		this.txtAnio.Size = new System.Drawing.Size(76, 20);
		this.txtAnio.TabIndex = 4;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(64, 166);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(32, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Año :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(53, 66);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(97, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Libro Electronicos :";
		this.txtRUC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtRUC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRUC.Location = new System.Drawing.Point(461, 32);
		this.txtRUC.MaxLength = 11;
		this.txtRUC.Name = "txtRUC";
		this.txtRUC.Size = new System.Drawing.Size(126, 20);
		this.txtRUC.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtRUC, this.customValidator1);
		this.txtRUC.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUC_KeyDown);
		this.txtRUC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRUC_KeyPress);
		this.txtRUC.Leave += new System.EventHandler(txtRUC_Leave);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(403, 35);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(52, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "R.U.C. * :";
		this.txtCodEmpresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodEmpresa.Enabled = false;
		this.txtCodEmpresa.Location = new System.Drawing.Point(135, 32);
		this.txtCodEmpresa.Name = "txtCodEmpresa";
		this.txtCodEmpresa.Size = new System.Drawing.Size(32, 20);
		this.txtCodEmpresa.TabIndex = 0;
		this.txtCodEmpresa.Text = "LE";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(52, 113);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(118, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Registros Electronicos :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(53, 36);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(76, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Indicador Fijo :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "El RUC ingresado no es valido";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator2.ErrorMessage = "Ingrese la Razon Social";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Location = new System.Drawing.Point(12, 370);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(288, 23);
		this.labelX1.TabIndex = 11;
		this.labelX1.Text = "(*) Presione F2 para verificar los datos con la SUNAT";
		this.labelX1.TextLineAlignment = System.Drawing.StringAlignment.Near;
		this.labelX1.Visible = false;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(521, 368);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnGenerarNombre.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerarNombre.ImageList = this.imageList1;
		this.btnGenerarNombre.Location = new System.Drawing.Point(324, 368);
		this.btnGenerarNombre.Name = "btnGenerarNombre";
		this.btnGenerarNombre.Size = new System.Drawing.Size(109, 23);
		this.btnGenerarNombre.TabIndex = 1;
		this.btnGenerarNombre.Text = "Generar Nombre";
		this.btnGenerarNombre.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGenerarNombre.UseVisualStyleBackColor = true;
		this.btnGenerarNombre.Click += new System.EventHandler(btnGenerarNombre_Click);
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(439, 368);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 12;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Visible = false;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(679, 410);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.labelX1);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnGenerarNombre);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionNombreLE";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Nomenclatura de  Libros Electronicos ";
		base.Load += new System.EventHandler(frmGestionNombreLE_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
