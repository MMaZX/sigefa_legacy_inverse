using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVendedores : Office2007Form
{
	public int Proceso = 0;

	private clsAdmVendedor admVen = new clsAdmVendedor();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmAcceso admAcce = new clsAdmAcceso();

	public clsVendedor ven = new clsVendedor();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private IContainer components = null;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private CustomValidator customValidator4;

	private CustomValidator customValidator5;

	private CustomValidator customValidator6;

	private CustomValidator customValidator7;

	private CustomValidator customValidator8;

	private CustomValidator customValidator9;

	private CustomValidator customValidator10;

	private GroupBox groupBox3;

	private Label label13;

	private DateTimePicker dtpFechaNac;

	private TextBox txtCelular;

	private Label label12;

	private TextBox txtDireccion;

	private Label label4;

	private TextBox txtEmail;

	private Label label3;

	private TextBox txtTelefono;

	private Label label2;

	private TextBox txtDni;

	private Label label1;

	private TextBox txtApellidoUsuario;

	private TextBox txtNombreUsuario;

	private TextBox txtCodigo;

	private Label label7;

	private Label label6;

	private Label label5;

	private ImageList imageList1;

	private GroupBox groupBox1;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private DataGridView dgvVendedores;

	private Label label8;

	private Label label9;

	private TextBox txtFiltro;

	private Label label10;

	private CustomValidator customValidator11;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn apellido;

	private DataGridViewTextBoxColumn fechanac;

	private DataGridViewTextBoxColumn direccion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn celular;

	private DataGridViewTextBoxColumn email;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	public frmVendedores()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtNombreUsuario.Text != ""))
		{
			return;
		}
		ven.Dni = txtDni.Text;
		ven.FechaNac = dtpFechaNac.Value.Date;
		ven.Nombre = txtNombreUsuario.Text;
		ven.Apellido = txtApellidoUsuario.Text;
		ven.Direccion = txtDireccion.Text;
		ven.Telefono = txtTelefono.Text;
		ven.Celular = txtCelular.Text;
		ven.Email = txtEmail.Text;
		ven.CodUser = frmLogin.iCodUser;
		if (Proceso == 1)
		{
			if (admVen.insert(ven))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Vendedores", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
				Proceso = 0;
			}
		}
		else if (Proceso == 2 && admVen.update(ven))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Vendedores", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
			Proceso = 0;
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		if (groupBox2.Visible)
		{
			Close();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void frmVendedores_Load(object sender, EventArgs e)
	{
		CargaLista();
		label9.Text = "Codigo";
		label8.Text = "codVendedor";
		if (Proceso == 3)
		{
			bloquearbotones();
		}
	}

	private void cargavendedor()
	{
		ven = admVen.MuestraVendedor(ven.CodVendedor);
		if (ven != null)
		{
			txtCodigo.Text = ven.CodVendedor.ToString();
			txtDni.Text = ven.Dni;
			dtpFechaNac.Value = ven.FechaNac;
			txtNombreUsuario.Text = ven.Nombre;
			txtApellidoUsuario.Text = ven.Apellido;
			txtTelefono.Text = ven.Telefono;
			txtCelular.Text = ven.Celular;
			txtEmail.Text = ven.Email;
			txtDireccion.Text = ven.Direccion;
		}
	}

	private void txtCodUsuario_TextChanged(object sender, EventArgs e)
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
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
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

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
		{
			if (Proceso == 1)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator8_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
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

	private void customValidator10_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
		{
			if (Proceso == 2)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label8.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox3.Text = "Registro Nuevo";
		Proceso = 1;
		ext.limpiar(groupBox3.Controls);
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvVendedores.CurrentRow.Index != -1 && ven.CodVendedor != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Vendedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admVen.delete(ven.CodVendedor))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Vendedor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		ext.limpiar(groupBox2.Controls);
		CambiarEstados(Estado: false);
		groupBox3.Text = "Editar Registro";
		Proceso = 2;
		cargavendedor();
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox2.Visible = Estado;
		groupBox3.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		ext.limpiar(groupBox3.Controls);
		superValidator1.Validate();
	}

	private void CargaLista()
	{
		dgvVendedores.DataSource = data;
		data.DataSource = admVen.MuestraVendedores();
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void customValidator11_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void bloquearbotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
		btnReporte.Visible = false;
		btnGuardar.Text = "Aceptar";
		btnGuardar.ImageIndex = 6;
	}

	private void dgvVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void dgvVendedores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
		}
	}

	private void dgvVendedores_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label9.Text = dgvVendedores.Columns[e.ColumnIndex].HeaderText;
		label8.Text = dgvVendedores.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvVendedores_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvVendedores.Rows.Count >= 1 && e.Row.Selected)
		{
			ven.CodVendedor = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvVendedores.Rows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 13;
		frm.ShowDialog();
	}

	private void frmVendedores_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVendedores));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtApellidoUsuario = new System.Windows.Forms.TextBox();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.txtNombreUsuario = new System.Windows.Forms.TextBox();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.txtDni = new System.Windows.Forms.TextBox();
		this.customValidator11 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator8 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator9 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator10 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label13 = new System.Windows.Forms.Label();
		this.dtpFechaNac = new System.Windows.Forms.DateTimePicker();
		this.txtCelular = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtEmail = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvVendedores = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechanac = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.celular = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVendedores).BeginInit();
		base.SuspendLayout();
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.txtApellidoUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtApellidoUsuario.Location = new System.Drawing.Point(86, 103);
		this.txtApellidoUsuario.Name = "txtApellidoUsuario";
		this.txtApellidoUsuario.Size = new System.Drawing.Size(237, 20);
		this.txtApellidoUsuario.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtApellidoUsuario, this.customValidator2);
		this.customValidator2.ErrorMessage = "Ingrese el apellido.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.txtNombreUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreUsuario.Location = new System.Drawing.Point(86, 77);
		this.txtNombreUsuario.Name = "txtNombreUsuario";
		this.txtNombreUsuario.Size = new System.Drawing.Size(237, 20);
		this.txtNombreUsuario.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtNombreUsuario, this.customValidator1);
		this.customValidator1.ErrorMessage = "Ingrese el nombre.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.txtDni.Location = new System.Drawing.Point(422, 51);
		this.txtDni.Name = "txtDni";
		this.txtDni.Size = new System.Drawing.Size(100, 20);
		this.txtDni.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtDni, this.customValidator11);
		this.txtDni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDni_KeyPress);
		this.customValidator11.ErrorMessage = "Ingrese el DNI.";
		this.customValidator11.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator11.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator11_ValidateValue);
		this.customValidator8.ErrorMessage = "Repita la nueva contraseña.";
		this.customValidator8.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator8.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator8_ValidateValue);
		this.customValidator9.ErrorMessage = "La nueva contraseña no coincide.";
		this.customValidator9.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ErrorMessage = "Repita la contraseña.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator6.ErrorMessage = "La contraseña no coincide.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator10.ErrorMessage = "Ingrese la nueva contraseña.";
		this.customValidator10.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator10.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator10_ValidateValue);
		this.customValidator4.ErrorMessage = "Ingrese la contraseña.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator7.ErrorMessage = "La contraseña no es valida.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ErrorMessage = "Ingrese el username.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Controls.Add(this.dtpFechaNac);
		this.groupBox3.Controls.Add(this.txtCelular);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.txtDireccion);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.txtEmail);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.txtTelefono);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.txtDni);
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Controls.Add(this.txtApellidoUsuario);
		this.groupBox3.Controls.Add(this.txtNombreUsuario);
		this.groupBox3.Controls.Add(this.txtCodigo);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Location = new System.Drawing.Point(12, 12);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(556, 207);
		this.groupBox3.TabIndex = 16;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Nuevo Registro";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(6, 54);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(69, 13);
		this.label13.TabIndex = 26;
		this.label13.Text = "Fecha Nac. :";
		this.dtpFechaNac.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaNac.Location = new System.Drawing.Point(86, 51);
		this.dtpFechaNac.Name = "dtpFechaNac";
		this.dtpFechaNac.Size = new System.Drawing.Size(100, 20);
		this.dtpFechaNac.TabIndex = 2;
		this.txtCelular.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCelular.Location = new System.Drawing.Point(422, 103);
		this.txtCelular.Name = "txtCelular";
		this.txtCelular.Size = new System.Drawing.Size(100, 20);
		this.txtCelular.TabIndex = 7;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(361, 106);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(45, 13);
		this.label12.TabIndex = 24;
		this.label12.Text = "Celular :";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(86, 129);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(237, 20);
		this.txtDireccion.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 132);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(58, 13);
		this.label4.TabIndex = 22;
		this.label4.Text = "Dirección :";
		this.txtEmail.Location = new System.Drawing.Point(422, 129);
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.Size = new System.Drawing.Size(100, 20);
		this.txtEmail.TabIndex = 8;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(361, 132);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(38, 13);
		this.label3.TabIndex = 20;
		this.label3.Text = "Email :";
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(422, 77);
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(100, 20);
		this.txtTelefono.TabIndex = 6;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(361, 80);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(55, 13);
		this.label2.TabIndex = 18;
		this.label2.Text = "Teléfono :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(361, 54);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(33, 13);
		this.label1.TabIndex = 16;
		this.label1.Text = "Dni* :";
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(86, 25);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(100, 20);
		this.txtCodigo.TabIndex = 1;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 106);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(75, 13);
		this.label7.TabIndex = 2;
		this.label7.Text = "Nom. Comp.* :";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 80);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(54, 13);
		this.label6.TabIndex = 1;
		this.label6.Text = "Nombre* :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 29);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(52, 13);
		this.label5.TabIndex = 0;
		this.label5.Text = "Codidgo :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox1.Controls.Add(this.btnSalir);
		this.groupBox1.Controls.Add(this.btnNuevo);
		this.groupBox1.Controls.Add(this.btnGuardar);
		this.groupBox1.Controls.Add(this.btnEditar);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.btnEliminar);
		this.groupBox1.Location = new System.Drawing.Point(12, 225);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(556, 48);
		this.groupBox1.TabIndex = 17;
		this.groupBox1.TabStop = false;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(482, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 10;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnCancelar_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(399, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 11;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 10);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(236, 10);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 9;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(155, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.AccessibleDescription = "";
		this.groupBox2.Controls.Add(this.dgvVendedores);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtFiltro);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Location = new System.Drawing.Point(12, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(556, 207);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Conductor";
		this.dgvVendedores.AllowUserToAddRows = false;
		this.dgvVendedores.AllowUserToDeleteRows = false;
		this.dgvVendedores.AllowUserToOrderColumns = true;
		this.dgvVendedores.AllowUserToResizeColumns = false;
		this.dgvVendedores.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVendedores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvVendedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvVendedores.Columns.AddRange(this.codigo, this.dni, this.nombre, this.apellido, this.fechanac, this.direccion, this.telefono, this.celular, this.email, this.estado, this.coduser, this.fecha);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvVendedores.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvVendedores.Location = new System.Drawing.Point(6, 45);
		this.dgvVendedores.MultiSelect = false;
		this.dgvVendedores.Name = "dgvVendedores";
		this.dgvVendedores.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVendedores.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvVendedores.RowHeadersVisible = false;
		this.dgvVendedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVendedores.Size = new System.Drawing.Size(544, 156);
		this.dgvVendedores.TabIndex = 8;
		this.dgvVendedores.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVendedores_CellDoubleClick);
		this.dgvVendedores.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvVendedores_ColumnHeaderMouseClick);
		this.dgvVendedores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVendedores_CellClick);
		this.dgvVendedores.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvVendedores_RowStateChanged);
		this.codigo.DataPropertyName = "codVendedor";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 60;
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.dni.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 150;
		this.apellido.DataPropertyName = "apellido";
		this.apellido.HeaderText = "Nombre Completo";
		this.apellido.Name = "apellido";
		this.apellido.ReadOnly = true;
		this.apellido.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.apellido.Width = 150;
		this.fechanac.DataPropertyName = "fechanac";
		this.fechanac.HeaderText = "F. Nac.";
		this.fechanac.Name = "fechanac";
		this.fechanac.ReadOnly = true;
		this.fechanac.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fechanac.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccion.DataPropertyName = "direccion";
		this.direccion.HeaderText = "Direccion";
		this.direccion.Name = "direccion";
		this.direccion.ReadOnly = true;
		this.direccion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.direccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccion.Visible = false;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "Telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.telefono.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.telefono.Visible = false;
		this.celular.DataPropertyName = "celular";
		this.celular.HeaderText = "Celular";
		this.celular.Name = "celular";
		this.celular.ReadOnly = true;
		this.celular.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.celular.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.email.DataPropertyName = "email";
		this.email.HeaderText = "E-mail";
		this.email.Name = "email";
		this.email.ReadOnly = true;
		this.email.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fecha.Visible = false;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(427, 22);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(12, 13);
		this.label8.TabIndex = 6;
		this.label8.Text = "x";
		this.label8.Visible = false;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(96, 20);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(17, 16);
		this.label9.TabIndex = 5;
		this.label9.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(200, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(25, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(65, 13);
		this.label10.TabIndex = 3;
		this.label10.Text = "Buscar Por :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(580, 285);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVendedores";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Vendedor";
		base.Load += new System.EventHandler(frmVendedores_Load);
		base.Shown += new System.EventHandler(frmVendedores_Shown);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVendedores).EndInit();
		base.ResumeLayout(false);
	}
}
