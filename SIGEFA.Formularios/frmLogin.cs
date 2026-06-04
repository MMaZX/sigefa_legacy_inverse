using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmLogin : Office2007Form
{
	public static int iCodUser;

	public static int iCodEmpresa;

	public static int iCodSucursal;

	public static string sEmpresa;

	public static int iCodAlmacen;

	public static string sAlmacen;

	public static string sUsuario = "";

	public static string sNombreUser = "";

	public static string sApellidoUSer = "";

	public static int iNivelUser;

	public static string DirecIp = "";

	public static string RUC = "";

	public static Image logo;

	public static bool accesocanalventas;

	public static int estadoIngreso;

	private clsAdmUsuario AdmUser = new clsAdmUsuario();

	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private clsAdmAlmacen AdmAlm = new clsAdmAlmacen();

	private clsUsuario Login = new clsUsuario();

	private clsEmpresa EmpreLogin = new clsEmpresa();

	private clsSucursal SucurLogin = new clsSucursal();

	public static clsAlmacen AlmacenLogin = new clsAlmacen();

	public static clsParametros Configuracion = new clsParametros();

	private clsConexionMysql con = new clsConexionMysql();

	private int iContador;

	public static List<int> AcesosUsuario = new List<int>();

	private DataTable empresas = new DataTable();

	private IContainer components = null;

	private ImageList imageList1;

	private ImageList imageList2;

	private SuperValidator superValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private Panel panel1;

	private Label lblCancelar;

	private TextBox txtUsuario;

	private Label label1;

	private Button btnLogin;

	private Label label4;

	private TextBox txtContra;

	private ComboBox cmbEmpresa;

	private ComboBox cmbNivel;

	private Label label2;

	private Label label3;

	private PictureBox pictureBox1;

	private PictureBox pictureBox2;

	private Label label6;

	private Label label5;

	public frmLogin()
	{
		base.FormBorderStyle = FormBorderStyle.None;
		BackColor = Color.DimGray;
		base.TransparencyKey = Color.DimGray;
		InitializeComponent();
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private async void btnLogin_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate())
			return;

		btnLogin.Enabled = false;
		btnLogin.Text = "INGRESANDO...";
		Cursor = Cursors.WaitCursor;

		try
		{
			Login.Usuario = txtUsuario.Text;
			Login.Contraseña = txtContra.Text;
			Login.CodEmpresaLogin = Convert.ToInt32(cmbEmpresa.SelectedValue);

			var loginOk = await Task.Run(() => AdmUser.EstableceLogin(Login));

			if (!loginOk)
			{
				iContador++;
				if (iContador == 3)
				{
					MessageBox.Show("Ha realizado 3 intentos de Logueo Erróneos, consulte con el Área de TI", "Logueo Fallido!");
					Application.Exit();
					return;
				}
				MessageBox.Show("Usuario o Contraseña no coinciden", "Logueo Fallido!");
				return;
			}

			var datos = await Task.Run(() => new
			{
				Empresa = AdmEmp.CargaEmpresa(Login.CodEmpresaLogin),
				Sucursal = AdmSuc.CargaSucursal(iCodSucursal),
				Config = AdmEmp.CargaConfiguracion()
			});

			iCodUser = Login.CodUsuario;
			sUsuario = Login.Usuario;
			sNombreUser = Login.Nombre;
			sApellidoUSer = Login.Apellido;
			iNivelUser = Login.Nivel;
			iCodEmpresa = Login.CodEmpresaLogin;
			EmpreLogin = datos.Empresa;
			SucurLogin = datos.Sucursal;
			Configuracion = datos.Config;
			sEmpresa = datos.Empresa.RazonSocial;
			DirecIp = con.LocalIPAddress();
			RUC = datos.Empresa.Ruc;
			estadoIngreso = Login.EstadoIngreso;
			logo = pictureBox1.Image;
			accesocanalventas = Login.CanalVentaAcceso1;

			var frm = new mdi_Menu();
			Hide();
			frm.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error");
		}
		finally
		{
			if (!IsDisposed)
			{
				btnLogin.Enabled = true;
				btnLogin.Text = "INGRESAR";
				Cursor = Cursors.Default;
			}
		}
	}

	private void frmLogin_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		if (empresas != null && empresas.Rows.Count > 0 && empresas.Rows[cmbEmpresa.SelectedIndex][2] != DBNull.Value)
		{
			MemoryStream ms1 = new MemoryStream((byte[])empresas.Rows[cmbEmpresa.SelectedIndex][2]);
			Image returnImage = Image.FromStream(ms1);
			pictureBox1.Image = returnImage;
		}
		if (cmbEmpresa.Items.Count > 0)
			cmbEmpresa.SelectedIndex = 0;
		btnLogin.FlatAppearance.BorderSize = 0;
	}

	private void CargaNiveles()
	{
		try
		{
			cmbNivel.DataSource = AdmUser.ListaNiveles();
			cmbNivel.ValueMember = "idnivel";
			cmbNivel.DisplayMember = "nombre_nivel";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cmbNivel_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			btnLogin.PerformClick();
		}
	}

	private void CargaEmpresas()
	{
		empresas = AdmEmp.CargaEmpresas();
		if (empresas != null)
		{
			cmbEmpresa.DataSource = empresas;
		}
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.SelectedIndex != -1)
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
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.SelectedIndex != -1)
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void frmLogin_Shown(object sender, EventArgs e)
	{
		CargaEmpresas();
		if (cmbEmpresa.Items.Count > 0)
		{
			cmbEmpresa.SelectedIndex = 0;
		}
	}

	private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtContra.Focus();
		}
	}

	private void txtContra_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmbEmpresa.Focus();
		}
	}

	private void cmbEmpresa_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnLogin.Focus();
		}
	}

	private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
	{
		btnLogin.Focus();
		if (empresas != null && empresas.Rows.Count > 0 && empresas.Rows[cmbEmpresa.SelectedIndex][2] != DBNull.Value)
		{
			MemoryStream ms = new MemoryStream((byte[])empresas.Rows[cmbEmpresa.SelectedIndex][2]);
			Image returnImage = Image.FromStream(ms);
			pictureBox1.Image = returnImage;
		}
	}

	private void panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void lblCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtContra_Leave(object sender, EventArgs e)
	{
		btnLogin.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmLogin));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.txtContra = new System.Windows.Forms.TextBox();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.txtUsuario = new System.Windows.Forms.TextBox();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbNivel = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.btnLogin = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblCancelar = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.imageList1.Images.SetKeyName(3, "Donate.ico");
		this.imageList1.Images.SetKeyName(4, "lock.png");
		this.imageList1.Images.SetKeyName(5, "lock-icon.png");
		this.imageList1.Images.SetKeyName(6, "olvidaste-tu-contrasena-de-bloqueo-de-seguridad-icono-4928-128.png");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "lock.png");
		this.imageList2.Images.SetKeyName(1, "lock-icon.png");
		this.imageList2.Images.SetKeyName(2, "olvidaste-tu-contrasena-de-bloqueo-de-seguridad-icono-4928-128.png");
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.cmbEmpresa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbEmpresa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbEmpresa.BackColor = System.Drawing.Color.White;
		this.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbEmpresa.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(308, 165);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(299, 27);
		this.cmbEmpresa.TabIndex = 2;
		this.superValidator1.SetValidator1(this.cmbEmpresa, this.customValidator3);
		this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(cmbEmpresa_SelectionChangeCommitted);
		this.cmbEmpresa.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbEmpresa_KeyDown);
		this.customValidator3.ErrorMessage = "Seleccione una empresa.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.txtContra.BackColor = System.Drawing.Color.White;
		this.txtContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtContra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.txtContra.Location = new System.Drawing.Point(308, 106);
		this.txtContra.Name = "txtContra";
		this.txtContra.PasswordChar = '*';
		this.txtContra.Size = new System.Drawing.Size(206, 31);
		this.txtContra.TabIndex = 1;
		this.txtContra.UseSystemPasswordChar = true;
		this.superValidator1.SetValidator1(this.txtContra, this.customValidator2);
		this.txtContra.KeyDown += new System.Windows.Forms.KeyEventHandler(txtContra_KeyDown);
		this.txtContra.Leave += new System.EventHandler(txtContra_Leave);
		this.customValidator2.ErrorMessage = "Ingrese la contraseña.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.txtUsuario.BackColor = System.Drawing.Color.White;
		this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUsuario.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.txtUsuario.Location = new System.Drawing.Point(308, 49);
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.Size = new System.Drawing.Size(206, 31);
		this.txtUsuario.TabIndex = 0;
		this.superValidator1.SetValidator1(this.txtUsuario, this.customValidator1);
		this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUsuario_KeyDown);
		this.customValidator1.ErrorMessage = "Ingrese el nombre de usuario.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.label3.Location = new System.Drawing.Point(330, 177);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 16);
		this.label3.TabIndex = 7;
		this.label3.Text = "Acceso:";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label2.Location = new System.Drawing.Point(305, 87);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(101, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "Contraseña :";
		this.cmbNivel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbNivel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbNivel.BackColor = System.Drawing.Color.Silver;
		this.cmbNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbNivel.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbNivel.FormattingEnabled = true;
		this.cmbNivel.Items.AddRange(new object[2] { "Usuario", "Administrador" });
		this.cmbNivel.Location = new System.Drawing.Point(434, 167);
		this.cmbNivel.Name = "cmbNivel";
		this.cmbNivel.Size = new System.Drawing.Size(136, 24);
		this.cmbNivel.TabIndex = 4;
		this.cmbNivel.Visible = false;
		this.cmbNivel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbNivel_KeyPress);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label4.Location = new System.Drawing.Point(305, 146);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(77, 16);
		this.label4.TabIndex = 13;
		this.label4.Text = "Empresa:";
		this.btnLogin.BackColor = System.Drawing.SystemColors.Highlight;
		this.btnLogin.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
		this.btnLogin.FlatAppearance.CheckedBackColor = System.Drawing.Color.White;
		this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLogin.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLogin.ForeColor = System.Drawing.Color.Transparent;
		this.btnLogin.ImageList = this.imageList1;
		this.btnLogin.Location = new System.Drawing.Point(428, 213);
		this.btnLogin.Name = "btnLogin";
		this.btnLogin.Size = new System.Drawing.Size(179, 34);
		this.btnLogin.TabIndex = 3;
		this.btnLogin.Text = "INGRESAR";
		this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnLogin.UseVisualStyleBackColor = false;
		this.btnLogin.Click += new System.EventHandler(btnLogin_Click);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.label1.Location = new System.Drawing.Point(305, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(72, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Usuario :";
		this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel1.Controls.Add(this.pictureBox2);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.lblCancelar);
		this.panel1.Controls.Add(this.txtUsuario);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.btnLogin);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.txtContra);
		this.panel1.Controls.Add(this.cmbEmpresa);
		this.panel1.Controls.Add(this.cmbNivel);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.pictureBox1);
		this.panel1.Location = new System.Drawing.Point(8, 11);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(617, 256);
		this.panel1.TabIndex = 20;
		this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
		this.lblCancelar.BackColor = System.Drawing.Color.Transparent;
		this.lblCancelar.Image = (System.Drawing.Image)resources.GetObject("lblCancelar.Image");
		this.lblCancelar.Location = new System.Drawing.Point(571, 1);
		this.lblCancelar.Name = "lblCancelar";
		this.lblCancelar.Size = new System.Drawing.Size(42, 38);
		this.lblCancelar.TabIndex = 21;
		this.lblCancelar.Click += new System.EventHandler(lblCancelar_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Image = SIGEFA.Properties.Resources.lmsac;
		this.pictureBox1.Location = new System.Drawing.Point(8, 66);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(294, 96);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 20;
		this.pictureBox1.TabStop = false;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(6, 239);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(200, 10);
		this.label6.TabIndex = 23;
		this.label6.Text = "Nº DE REGISTROS DE MARCA: 0086488";
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Verdana", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(18, 225);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(174, 12);
		this.label5.TabIndex = 22;
		this.label5.Text = "COPYRIGHT © SGE SYSTEM'S SAC";
		this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(540, 49);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(67, 88);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 19;
		this.pictureBox2.TabStop = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.DimGray;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		base.ClientSize = new System.Drawing.Size(633, 277);
		base.ControlBox = false;
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmLogin";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SGE - SISTEMA DE GESTION EMPESARIAL";
		base.Load += new System.EventHandler(frmLogin_Load);
		base.Shown += new System.EventHandler(frmLogin_Shown);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		base.ResumeLayout(false);
	}
}
