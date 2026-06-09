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

	private Label lblMensaje;

	private Panel panel1;

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

	public frmLogin()
	{
		InitializeComponent();
	}

	private async void btnLogin_Click(object sender, EventArgs e)
	{
		lblMensaje.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
		lblMensaje.Text = "";

		if (string.IsNullOrWhiteSpace(txtUsuario.Text))
		{
			lblMensaje.Text = "Por favor, ingrese el nombre de usuario.";
			txtUsuario.Focus();
			return;
		}

		if (string.IsNullOrWhiteSpace(txtContra.Text))
		{
			lblMensaje.Text = "Por favor, ingrese la contraseña.";
			txtContra.Focus();
			return;
		}

		if (cmbEmpresa.SelectedIndex == -1)
		{
			lblMensaje.Text = "Por favor, seleccione una empresa.";
			cmbEmpresa.Focus();
			return;
		}

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
					lblMensaje.Text = "3 intentos fallidos. Saliendo...";
					await Task.Delay(1500);
					Application.Exit();
					return;
				}
				lblMensaje.Text = "Usuario o contraseña no coinciden.";
				return;
			}

			lblMensaje.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69);
			lblMensaje.Text = "Cargando configuración...";

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
			lblMensaje.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
			lblMensaje.Text = $"Error al iniciar sesión: {ex.Message}";
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

	private async void frmLogin_Load(object sender, EventArgs e)
	{
		await CargaEmpresasAsync();
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

	private async Task CargaEmpresasAsync()
	{
		try
		{
			txtUsuario.Enabled = false;
			txtContra.Enabled = false;
			cmbEmpresa.Enabled = false;
			btnLogin.Enabled = false;
			btnLogin.Text = "Cargando...";

			empresas = await Task.Run(() => AdmEmp.CargaEmpresas());
			if (empresas != null)
			{
				cmbEmpresa.DataSource = empresas;
				cmbEmpresa.DisplayMember = "razonsocial";
				cmbEmpresa.ValueMember = "codEmpresa";

				if (empresas.Rows.Count > 0)
				{
					cmbEmpresa.SelectedIndex = 0;
					CargarImagenEmpresa(0);
				}
			}
		}
		catch (Exception ex)
		{
			lblMensaje.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
			lblMensaje.Text = "Error al conectar con la base de datos.";
		}
		finally
		{
			txtUsuario.Enabled = true;
			txtContra.Enabled = true;
			cmbEmpresa.Enabled = true;
			btnLogin.Enabled = true;
			btnLogin.Text = "INGRESAR";
		}
	}

	private void CargarImagenEmpresa(int index)
	{
		try
		{
			if (empresas != null && empresas.Rows.Count > 0 && index >= 0 && index < empresas.Rows.Count)
			{
				if (empresas.Rows[index][2] != DBNull.Value)
				{
					byte[] imgData = (byte[])empresas.Rows[index][2];
					MemoryStream ms = new MemoryStream(imgData);
					pictureBox1.Image = Image.FromStream(ms);
				}
				else
				{
					pictureBox1.Image = SIGEFA.Properties.Resources.lmsac;
				}
			}
		}
		catch
		{
			pictureBox1.Image = SIGEFA.Properties.Resources.lmsac;
		}
	}

	private void frmLogin_Shown(object sender, EventArgs e)
	{
		// Carga ya realizada asíncronamente en Load para evitar bloqueo e innecesarias peticiones extras
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
		CargarImagenEmpresa(cmbEmpresa.SelectedIndex);
		lblMensaje.Text = "";
	}

	private void txtUsuario_TextChanged(object sender, EventArgs e)
	{
		lblMensaje.Text = "";
	}

	private void txtContra_TextChanged(object sender, EventArgs e)
	{
		lblMensaje.Text = "";
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
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.txtContra = new System.Windows.Forms.TextBox();
		this.txtUsuario = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbNivel = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.btnLogin = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.lblMensaje = new System.Windows.Forms.Label();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
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

		this.cmbEmpresa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbEmpresa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbEmpresa.Font = new System.Drawing.Font("Tahoma", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(280, 134);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(270, 26);
		this.cmbEmpresa.TabIndex = 2;
		this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(cmbEmpresa_SelectionChangeCommitted);
		this.cmbEmpresa.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbEmpresa_KeyDown);
		this.txtContra.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtContra.Location = new System.Drawing.Point(280, 78);
		this.txtContra.Name = "txtContra";
		this.txtContra.Size = new System.Drawing.Size(270, 26);
		this.txtContra.TabIndex = 1;
		this.txtContra.UseSystemPasswordChar = true;
		this.txtContra.KeyDown += new System.Windows.Forms.KeyEventHandler(txtContra_KeyDown);
		this.txtContra.Leave += new System.EventHandler(txtContra_Leave);
		this.txtContra.TextChanged += new System.EventHandler(txtContra_TextChanged);
		this.txtUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUsuario.Location = new System.Drawing.Point(280, 30);
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.Size = new System.Drawing.Size(270, 26);
		this.txtUsuario.TabIndex = 0;
		this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUsuario_KeyDown);
		this.txtUsuario.TextChanged += new System.EventHandler(txtUsuario_TextChanged);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(280, 170);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(50, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "Acceso:";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(278, 60);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(85, 14);
		this.label2.TabIndex = 1;
		this.label2.Text = "Contraseña:";
		this.cmbNivel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbNivel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbNivel.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbNivel.FormattingEnabled = true;
		this.cmbNivel.Items.AddRange(new object[2] { "Usuario", "Administrador" });
		this.cmbNivel.Location = new System.Drawing.Point(332, 168);
		this.cmbNivel.Name = "cmbNivel";
		this.cmbNivel.Size = new System.Drawing.Size(136, 22);
		this.cmbNivel.TabIndex = 4;
		this.cmbNivel.Visible = false;
		this.cmbNivel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbNivel_KeyPress);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(278, 116);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(65, 14);
		this.label4.TabIndex = 13;
		this.label4.Text = "Empresa:";
		this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.btnLogin.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLogin.Location = new System.Drawing.Point(370, 176);
		this.btnLogin.Name = "btnLogin";
		this.btnLogin.Size = new System.Drawing.Size(180, 34);
		this.btnLogin.TabIndex = 3;
		this.btnLogin.Text = "INGRESAR";
		this.btnLogin.UseVisualStyleBackColor = true;
		this.btnLogin.Click += new System.EventHandler(btnLogin_Click);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(278, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 14);
		this.label1.TabIndex = 0;
		this.label1.Text = "Usuario:";
		this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
		this.lblMensaje.Font = new System.Drawing.Font("Verdana", 9f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMensaje.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
		this.lblMensaje.Location = new System.Drawing.Point(12, 150);
		this.lblMensaje.Name = "lblMensaje";
		this.lblMensaje.Size = new System.Drawing.Size(340, 70);
		this.lblMensaje.TabIndex = 21;
		this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
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
		this.panel1.Controls.Add(this.lblMensaje);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Name = "panel1";
		this.panel1.TabIndex = 20;
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Image = SIGEFA.Properties.Resources.lmsac;
		this.pictureBox1.Location = new System.Drawing.Point(12, 12);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(250, 130);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 20;
		this.pictureBox1.TabStop = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(575, 230);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmLogin";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "SGE - SISTEMA DE GESTION EMPESARIAL";
		base.Load += new System.EventHandler(frmLogin_Load);
		base.Shown += new System.EventHandler(frmLogin_Shown);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
