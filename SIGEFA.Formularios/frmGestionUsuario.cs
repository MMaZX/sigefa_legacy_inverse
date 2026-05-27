using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using RikTheVeggie;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionUsuario : Office2007Form
{
	public int Proceso = 0;

	private clsAdmUsuario admUsu = new clsAdmUsuario();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmAcceso admAcce = new clsAdmAcceso();

	public clsUsuario usu = new clsUsuario();

	private clsAccesos acce = new clsAccesos();

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public List<int> codigos = new List<int>();

	private IContainer components = null;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private GroupBox groupBox3;

	private TextBox txtCont3;

	private Label label11;

	private LinkLabel linkLabel1;

	private CheckBox cbActivoU;

	private TextBox txtCont2;

	private TextBox txtCont1;

	private TextBox txtUsuario;

	private TextBox txtApellidoUsuario;

	private TextBox txtNombreUsuario;

	private TextBox txtCodUsuario;

	private Label label10;

	private Label label9;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private TextBox txtEmail;

	private Label label3;

	private TextBox txtTelefono;

	private Label label2;

	private TextBox txtDni;

	private Label label1;

	private TextBox txtCelular;

	private Label label12;

	private TextBox txtDirección;

	private Label label4;

	private Button btnAceptar;

	private Button btnCancelar;

	private ImageList imageList1;

	private DateTimePicker dtpFechaNac;

	private Label label13;

	private RadioButton rbUsuario;

	private RadioButton rbAdministrador;

	private ComboBox cmbAlmacen;

	private ComboBox cmbEmpresa;

	private Label label15;

	private Label label14;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private CustomValidator customValidator5;

	private CustomValidator customValidator6;

	private CustomValidator customValidator8;

	private CustomValidator customValidator9;

	private CustomValidator customValidator10;

	private TextBox txtContraEmail;

	private Label label16;

	private TextBox txthost;

	private Label label17;

	private Label label18;

	private ComboBox cmbNivel;

	private TriStateTreeView tstvFormularios;

	private CheckBox chkcanalventa;

	private ComboBox cboCanalVenta;

	private Label label19;

	public frmGestionUsuario()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtNombreUsuario.Text != ""))
		{
			return;
		}
		usu.Dni = txtDni.Text;
		usu.FechaNac = dtpFechaNac.Value.Date;
		usu.Nombre = txtNombreUsuario.Text;
		usu.Apellido = txtApellidoUsuario.Text;
		usu.Direccion = txtDirección.Text;
		usu.Telefono = txtTelefono.Text;
		usu.Celular = txtCelular.Text;
		usu.Email = txtEmail.Text;
		usu.ContraEmail = txtContraEmail.Text;
		usu.Host = txthost.Text;
		usu.Usuario = txtUsuario.Text;
		usu.Estado = cbActivoU.Checked;
		usu.CodUser = frmLogin.iCodUser;
		usu.Nivel = Convert.ToInt32(cmbNivel.SelectedValue);
		usu.CodigoCanalVenta = cboCanalVenta.SelectedValue.ToString();
		usu.CanalVentaAcceso1 = chkcanalventa.Checked;
		RecorreArbol();
		if (Proceso == 1)
		{
			if (txtCont1.Text == txtCont2.Text && txtCont1.Text != "")
			{
				usu.Contraseña = txtCont1.Text;
				if (codigos.Count > 0)
				{
					if (!admUsu.insert(usu))
					{
						return;
					}
					if (codigos.Count > 0)
					{
						admAcce.InsertAccesoEmp(usu.CodUsuarioNuevo, frmLogin.iCodEmpresa, frmLogin.iCodUser);
						acce.CodUsuario = usu.CodUsuarioNuevo;
						acce.CodAlmacen = frmLogin.iCodAlmacen;
						acce.CodUser = frmLogin.iCodUser;
						foreach (int formu in usu.CodigosForm)
						{
							acce.CodFormulario = formu;
							admAcce.insert(acce);
						}
					}
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Usuario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Close();
				}
				else
				{
					MessageBox.Show("Debe brindar accesos al Usuario", "Gestion Usuario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show("Las contraseñas no coinciden", "Gestion Usuario", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			if (Proceso != 2)
			{
				return;
			}
			if (txtCont3.Visible)
			{
				if (!(usu.Contraseña == txtCont1.Text) || !(txtCont2.Text == txtCont3.Text) || !(txtCont2.Text != ""))
				{
					MessageBox.Show("Las contraseñas no coinciden", "Gestion Usuario", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				usu.Contraseña = txtCont2.Text;
			}
			if (!admUsu.update(usu))
			{
				return;
			}
			admAcce.LimpiarAccesos(usu.CodUsuario, frmLogin.iCodAlmacen);
			if (codigos.Count > 0)
			{
				admAcce.InsertAccesoEmp(usu.CodUsuario, frmLogin.iCodEmpresa, frmLogin.iCodUser);
				acce.CodUsuario = usu.CodUsuario;
				acce.CodAlmacen = frmLogin.iCodAlmacen;
				acce.CodUser = frmLogin.iCodUser;
				foreach (int formu2 in usu.CodigosForm)
				{
					acce.CodFormulario = formu2;
					admAcce.insert(acce);
				}
			}
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Usuario", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmGestionUsuario_Load(object sender, EventArgs e)
	{
		CargarCanaVenta();
		CargaNiveles();
		CargaEmpresas();
		cmbEmpresa.SelectedValue = frmLogin.iCodEmpresa;
		CargaAlmacenes();
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
		llenaarbol(0, null);
		if (Proceso == 2)
		{
			cargausuario();
			CargaAccesos();
		}
		else if (Proceso == 3)
		{
			cargausuario();
			ext.sololectura(groupBox3.Controls);
			linkLabel1.Visible = false;
			rbUsuario.Enabled = false;
			rbAdministrador.Enabled = false;
			tstvFormularios.Enabled = false;
			btnAceptar.Visible = false;
			btnCancelar.Text = "Aceptar";
			btnCancelar.ImageIndex = 1;
		}
	}

	private void CargaNiveles()
	{
		try
		{
			cmbNivel.DataSource = admUsu.ListaNiveles();
			cmbNivel.ValueMember = "idnivel";
			cmbNivel.DisplayMember = "nombre_nivel";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cargausuario()
	{
		usu = admUsu.MuestraUsuario(usu.CodUsuario);
		txtCodUsuario.Text = usu.CodUsuario.ToString();
		txtDni.Text = usu.Dni;
		txtNombreUsuario.Text = usu.Nombre;
		txtApellidoUsuario.Text = usu.Apellido;
		if (usu.FechaNac > dtpFechaNac.MinDate)
		{
			dtpFechaNac.Value = usu.FechaNac.Date;
		}
		else
		{
			dtpFechaNac.Text = "";
		}
		txtDirección.Text = usu.Direccion;
		txtTelefono.Text = usu.Telefono;
		txtCelular.Text = usu.Celular;
		txtEmail.Text = usu.Email;
		txtContraEmail.Text = usu.ContraEmail;
		txthost.Text = usu.Host;
		txtUsuario.Text = usu.Usuario;
		cbActivoU.Checked = usu.Estado;
		linkLabel1.Visible = true;
		label9.Visible = false;
		label10.Visible = false;
		txtCont1.Visible = false;
		txtCont2.Visible = false;
		cmbNivel.SelectedValue = usu.Nivel;
		cboCanalVenta.SelectedValue = usu.CodigoCanalVenta;
		chkcanalventa.Checked = usu.CanalVentaAcceso1;
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (label9.Visible)
		{
			label9.Visible = false;
			label10.Visible = false;
			label11.Visible = false;
			txtCont1.Visible = false;
			txtCont2.Visible = false;
			txtCont3.Visible = false;
		}
		else
		{
			label9.Visible = true;
			label10.Text = "Nueva Contraseña";
			label10.Visible = true;
			label11.Visible = true;
			txtCont1.Visible = true;
			txtCont2.Visible = true;
			txtCont3.Visible = true;
		}
	}

	private void radioButton1_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void radioButton2_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedIndex = -1;
	}

	private void CargaAlmacenes()
	{
		cmbAlmacen.DataSource = admAlm.CargaAlmacenes(frmLogin.iNivelUser, Convert.ToInt32(cmbEmpresa.SelectedValue), frmLogin.iCodUser);
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedIndex = -1;
	}

	private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaAlmacenes();
	}

	private void llenaarbol(int indicePadre, TreeNode nodoPadre)
	{
		DataView hijos = new DataView(admForm.MuestraFormularios());
		hijos.RowFilter = admForm.MuestraFormularios().Columns["padre"].ColumnName + " = " + indicePadre;
		foreach (DataRowView row in hijos)
		{
			TreeNode nuevonodo = new TreeNode();
			nuevonodo.Text = row["descripcion"].ToString();
			nuevonodo.Tag = row["codFormulario"].ToString();
			if (nodoPadre == null)
			{
				tstvFormularios.Nodes.Add(nuevonodo);
			}
			else
			{
				nodoPadre.Nodes.Add(nuevonodo);
			}
			llenaarbol(int.Parse(row["codFormulario"].ToString()), nuevonodo);
		}
	}

	private void RecorreArbol()
	{
		codigos.Clear();
		if (tstvFormularios.Enabled)
		{
			foreach (TreeNode nod in tstvFormularios.Nodes)
			{
				añadenodos(nod);
			}
		}
		usu.CodigosForm = codigos;
	}

	private void añadenodos(TreeNode nod)
	{
		int countChecked = 0;
		if (nod.Nodes.Count > 0)
		{
			foreach (TreeNode tnChild in nod.Nodes)
			{
				if (tnChild.StateImageIndex == 1)
				{
					countChecked++;
					break;
				}
			}
		}
		if (countChecked > 0)
		{
			codigos.Add(Convert.ToInt32(nod.Tag));
		}
		if (nod.StateImageIndex == 1)
		{
			codigos.Add(Convert.ToInt32(nod.Tag));
		}
		if (nod.Nodes.Count <= 0)
		{
			return;
		}
		foreach (TreeNode tnChild2 in nod.Nodes)
		{
			añadenodos(tnChild2);
		}
	}

	private void CargaAccesos()
	{
		codigos = admAcce.MuestraAccesos(usu.CodUsuario, frmLogin.iCodAlmacen);
		foreach (TreeNode nod in tstvFormularios.Nodes)
		{
			marcanodo(nod);
		}
	}

	private void marcanodo(TreeNode nod)
	{
		int codi = Convert.ToInt32(nod.Tag);
		if (codigos.Contains(codi))
		{
			nod.Checked = true;
		}
		else
		{
			nod.Checked = false;
		}
		if (nod.Nodes.Count <= 0)
		{
			return;
		}
		foreach (TreeNode tn in nod.Nodes)
		{
			marcanodo(tn);
		}
	}

	private void txtCodUsuario_TextChanged(object sender, EventArgs e)
	{
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

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
		{
			if (Proceso == 2)
			{
				if (e.ControlToValidate.Text.Equals(usu.Contraseña))
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

	private void customValidator6_ValidateValue_1(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
		{
			if (Proceso == 1)
			{
				if (e.ControlToValidate.Text.Equals(txtCont1.Text))
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

	private void customValidator9_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (e.ControlToValidate.Visible)
		{
			if (Proceso == 2)
			{
				if (e.ControlToValidate.Text.Equals(txtCont2.Text))
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

	private void cmbNivel_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbNivel.SelectedValue) == 1)
		{
			tstvFormularios.Enabled = false;
			return;
		}
		tstvFormularios.Enabled = true;
		CargaAccesos();
	}

	private void dtpFechaNac_ValueChanged(object sender, EventArgs e)
	{
		txtNombreUsuario.Focus();
	}

	private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtApellidoUsuario.Focus();
		}
	}

	private void txtApellidoUsuario_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDirección.Focus();
		}
	}

	private void txtDirección_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtUsuario.Focus();
		}
	}

	private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtCont1.Focus();
		}
	}

	private void txtCont1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtCont2.Focus();
		}
	}

	private void txtCont2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtCont3.Focus();
		}
	}

	private void txtCont3_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDni.Focus();
		}
	}

	private void txtDni_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtTelefono.Focus();
		}
	}

	private void txtCelular_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtEmail.Focus();
		}
	}

	private void txtEmail_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtContraEmail.Focus();
		}
	}

	private void txtContraEmail_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txthost.Focus();
		}
	}

	private void txthost_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnAceptar.Focus();
		}
	}

	private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtCelular.Focus();
		}
	}

	private void frmGestionUsuario_Shown(object sender, EventArgs e)
	{
		txtNombreUsuario.Focus();
	}

	private void CargarCanaVenta()
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		DataSet ds = new DataSet();
		try
		{
			dBAccess.AddParameter("pcodigotabla", "001");
			ds = dBAccess.ExecuteDataSet("sp_get_tablas");
			if (ds.Tables.Count > 0)
			{
				cboCanalVenta.DataSource = ds.Tables[0];
				cboCanalVenta.ValueMember = "codigo";
				cboCanalVenta.DisplayMember = "DescTablaDetalle";
			}
		}
		catch (Exception)
		{
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionUsuario));
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cmbNivel = new System.Windows.Forms.ComboBox();
		this.txthost = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtContraEmail = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.dtpFechaNac = new System.Windows.Forms.DateTimePicker();
		this.txtCelular = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtDirección = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtEmail = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtDni = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtCont3 = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.cbActivoU = new System.Windows.Forms.CheckBox();
		this.txtCont2 = new System.Windows.Forms.TextBox();
		this.txtCont1 = new System.Windows.Forms.TextBox();
		this.txtUsuario = new System.Windows.Forms.TextBox();
		this.txtApellidoUsuario = new System.Windows.Forms.TextBox();
		this.txtNombreUsuario = new System.Windows.Forms.TextBox();
		this.txtCodUsuario = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.tstvFormularios = new RikTheVeggie.TriStateTreeView();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.rbUsuario = new System.Windows.Forms.RadioButton();
		this.rbAdministrador = new System.Windows.Forms.RadioButton();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator8 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator9 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator10 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.chkcanalventa = new System.Windows.Forms.CheckBox();
		this.cboCanalVenta = new System.Windows.Forms.ComboBox();
		this.label19 = new System.Windows.Forms.Label();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 6);
		this.tabControl1.Multiline = true;
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(589, 327);
		this.tabControl1.TabIndex = 0;
		this.tabPage1.Controls.Add(this.groupBox3);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(581, 301);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Datos Generales";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.groupBox3.Controls.Add(this.chkcanalventa);
		this.groupBox3.Controls.Add(this.cboCanalVenta);
		this.groupBox3.Controls.Add(this.label19);
		this.groupBox3.Controls.Add(this.label18);
		this.groupBox3.Controls.Add(this.cmbNivel);
		this.groupBox3.Controls.Add(this.txthost);
		this.groupBox3.Controls.Add(this.label17);
		this.groupBox3.Controls.Add(this.txtContraEmail);
		this.groupBox3.Controls.Add(this.label16);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.Controls.Add(this.dtpFechaNac);
		this.groupBox3.Controls.Add(this.txtCelular);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.txtDirección);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.txtEmail);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.txtTelefono);
		this.groupBox3.Controls.Add(this.label2);
		this.groupBox3.Controls.Add(this.txtDni);
		this.groupBox3.Controls.Add(this.label1);
		this.groupBox3.Controls.Add(this.txtCont3);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.linkLabel1);
		this.groupBox3.Controls.Add(this.cbActivoU);
		this.groupBox3.Controls.Add(this.txtCont2);
		this.groupBox3.Controls.Add(this.txtCont1);
		this.groupBox3.Controls.Add(this.txtUsuario);
		this.groupBox3.Controls.Add(this.txtApellidoUsuario);
		this.groupBox3.Controls.Add(this.txtNombreUsuario);
		this.groupBox3.Controls.Add(this.txtCodUsuario);
		this.groupBox3.Controls.Add(this.label10);
		this.groupBox3.Controls.Add(this.label9);
		this.groupBox3.Controls.Add(this.label8);
		this.groupBox3.Controls.Add(this.label7);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Location = new System.Drawing.Point(3, 4);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(572, 291);
		this.groupBox3.TabIndex = 3;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Detalle del Usuario";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(385, 213);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(37, 13);
		this.label18.TabIndex = 32;
		this.label18.Text = "Nivel :";
		this.cmbNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbNivel.FormattingEnabled = true;
		this.cmbNivel.Location = new System.Drawing.Point(428, 210);
		this.cmbNivel.Name = "cmbNivel";
		this.cmbNivel.Size = new System.Drawing.Size(121, 21);
		this.cmbNivel.TabIndex = 16;
		this.cmbNivel.SelectionChangeCommitted += new System.EventHandler(cmbNivel_SelectionChangeCommitted);
		this.txthost.Location = new System.Drawing.Point(428, 156);
		this.txthost.Name = "txthost";
		this.txthost.Size = new System.Drawing.Size(100, 20);
		this.txthost.TabIndex = 14;
		this.txthost.KeyDown += new System.Windows.Forms.KeyEventHandler(txthost_KeyDown);
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(355, 159);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(67, 13);
		this.label17.TabIndex = 30;
		this.label17.Text = "Host Salida :";
		this.txtContraEmail.Location = new System.Drawing.Point(428, 130);
		this.txtContraEmail.MaxLength = 100;
		this.txtContraEmail.Name = "txtContraEmail";
		this.txtContraEmail.PasswordChar = '*';
		this.txtContraEmail.Size = new System.Drawing.Size(100, 20);
		this.txtContraEmail.TabIndex = 13;
		this.txtContraEmail.UseSystemPasswordChar = true;
		this.txtContraEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(txtContraEmail_KeyDown);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(351, 133);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(71, 13);
		this.label16.TabIndex = 27;
		this.label16.Text = "Contraseña* :";
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
		this.dtpFechaNac.TabIndex = 1;
		this.dtpFechaNac.ValueChanged += new System.EventHandler(dtpFechaNac_ValueChanged);
		this.txtCelular.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCelular.Location = new System.Drawing.Point(428, 78);
		this.txtCelular.MaxLength = 15;
		this.txtCelular.Name = "txtCelular";
		this.txtCelular.Size = new System.Drawing.Size(100, 20);
		this.txtCelular.TabIndex = 11;
		this.txtCelular.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCelular_KeyDown);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(377, 81);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(45, 13);
		this.label12.TabIndex = 24;
		this.label12.Text = "Celular :";
		this.txtDirección.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDirección.Location = new System.Drawing.Point(86, 129);
		this.txtDirección.Name = "txtDirección";
		this.txtDirección.Size = new System.Drawing.Size(237, 20);
		this.txtDirección.TabIndex = 4;
		this.txtDirección.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDirección_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 132);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(58, 13);
		this.label4.TabIndex = 22;
		this.label4.Text = "Dirección :";
		this.txtEmail.Location = new System.Drawing.Point(428, 104);
		this.txtEmail.MaxLength = 300;
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.Size = new System.Drawing.Size(100, 20);
		this.txtEmail.TabIndex = 12;
		this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(txtEmail_KeyDown);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(384, 107);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(38, 13);
		this.label3.TabIndex = 20;
		this.label3.Text = "Email :";
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(428, 52);
		this.txtTelefono.MaxLength = 15;
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(100, 20);
		this.txtTelefono.TabIndex = 10;
		this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelefono_KeyDown);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(367, 55);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(55, 13);
		this.label2.TabIndex = 18;
		this.label2.Text = "Teléfono :";
		this.txtDni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDni.Location = new System.Drawing.Point(428, 26);
		this.txtDni.MaxLength = 8;
		this.txtDni.Name = "txtDni";
		this.txtDni.Size = new System.Drawing.Size(100, 20);
		this.txtDni.TabIndex = 9;
		this.txtDni.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDni_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(393, 29);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(29, 13);
		this.label1.TabIndex = 16;
		this.label1.Text = "Dni :";
		this.txtCont3.Location = new System.Drawing.Point(120, 236);
		this.txtCont3.MaxLength = 100;
		this.txtCont3.Name = "txtCont3";
		this.txtCont3.Size = new System.Drawing.Size(120, 20);
		this.txtCont3.TabIndex = 8;
		this.txtCont3.UseSystemPasswordChar = true;
		this.superValidator1.SetValidator1(this.txtCont3, this.customValidator8);
		this.superValidator1.SetValidator2(this.txtCont3, this.customValidator9);
		this.txtCont3.Visible = false;
		this.txtCont3.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCont3_KeyDown);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(7, 239);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(103, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Repetir contraseña :";
		this.label11.Visible = false;
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(310, 190);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(102, 13);
		this.linkLabel1.TabIndex = 13;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "Cambiar Contraseña";
		this.linkLabel1.Visible = false;
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.cbActivoU.AutoSize = true;
		this.cbActivoU.Checked = true;
		this.cbActivoU.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbActivoU.Location = new System.Drawing.Point(428, 188);
		this.cbActivoU.Name = "cbActivoU";
		this.cbActivoU.Size = new System.Drawing.Size(56, 17);
		this.cbActivoU.TabIndex = 15;
		this.cbActivoU.Text = "Activo";
		this.cbActivoU.UseVisualStyleBackColor = true;
		this.txtCont2.Location = new System.Drawing.Point(120, 210);
		this.txtCont2.MaxLength = 100;
		this.txtCont2.Name = "txtCont2";
		this.txtCont2.Size = new System.Drawing.Size(120, 20);
		this.txtCont2.TabIndex = 7;
		this.txtCont2.UseSystemPasswordChar = true;
		this.superValidator1.SetValidator1(this.txtCont2, this.customValidator5);
		this.superValidator1.SetValidator2(this.txtCont2, this.customValidator6);
		this.superValidator1.SetValidator3(this.txtCont2, this.customValidator10);
		this.txtCont2.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCont2_KeyDown);
		this.txtCont1.Location = new System.Drawing.Point(120, 181);
		this.txtCont1.MaxLength = 100;
		this.txtCont1.Name = "txtCont1";
		this.txtCont1.Size = new System.Drawing.Size(120, 20);
		this.txtCont1.TabIndex = 6;
		this.txtCont1.UseSystemPasswordChar = true;
		this.txtCont1.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCont1_KeyDown);
		this.txtUsuario.Location = new System.Drawing.Point(120, 155);
		this.txtUsuario.MaxLength = 100;
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.Size = new System.Drawing.Size(120, 20);
		this.txtUsuario.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtUsuario, this.customValidator3);
		this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUsuario_KeyDown);
		this.txtApellidoUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtApellidoUsuario.Location = new System.Drawing.Point(86, 103);
		this.txtApellidoUsuario.Name = "txtApellidoUsuario";
		this.txtApellidoUsuario.Size = new System.Drawing.Size(237, 20);
		this.txtApellidoUsuario.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtApellidoUsuario, this.customValidator2);
		this.txtApellidoUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtApellidoUsuario_KeyDown);
		this.txtNombreUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreUsuario.Location = new System.Drawing.Point(86, 77);
		this.txtNombreUsuario.Name = "txtNombreUsuario";
		this.txtNombreUsuario.Size = new System.Drawing.Size(237, 20);
		this.txtNombreUsuario.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtNombreUsuario, this.customValidator1);
		this.txtNombreUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNombreUsuario_KeyDown);
		this.txtCodUsuario.Enabled = false;
		this.txtCodUsuario.Location = new System.Drawing.Point(86, 25);
		this.txtCodUsuario.Name = "txtCodUsuario";
		this.txtCodUsuario.Size = new System.Drawing.Size(100, 20);
		this.txtCodUsuario.TabIndex = 0;
		this.txtCodUsuario.TextChanged += new System.EventHandler(txtCodUsuario_TextChanged);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(7, 213);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(103, 13);
		this.label10.TabIndex = 5;
		this.label10.Text = "Repetir contraseña :";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(7, 184);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(71, 13);
		this.label9.TabIndex = 4;
		this.label9.Text = "Contraseña* :";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(7, 158);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(53, 13);
		this.label8.TabIndex = 3;
		this.label8.Text = "Usuario* :";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 106);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(54, 13);
		this.label7.TabIndex = 2;
		this.label7.Text = "Apellido* :";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 80);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(54, 13);
		this.label6.TabIndex = 1;
		this.label6.Text = "Nombre* :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 29);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(74, 13);
		this.label5.TabIndex = 0;
		this.label5.Text = "Cod. Usuario :";
		this.tabPage2.Controls.Add(this.tstvFormularios);
		this.tabPage2.Controls.Add(this.cmbAlmacen);
		this.tabPage2.Controls.Add(this.cmbEmpresa);
		this.tabPage2.Controls.Add(this.label15);
		this.tabPage2.Controls.Add(this.label14);
		this.tabPage2.Controls.Add(this.rbUsuario);
		this.tabPage2.Controls.Add(this.rbAdministrador);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(581, 301);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Accesos";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.tstvFormularios.Location = new System.Drawing.Point(139, 74);
		this.tstvFormularios.Name = "tstvFormularios";
		this.tstvFormularios.Size = new System.Drawing.Size(271, 198);
		this.tstvFormularios.TabIndex = 11;
		this.tstvFormularios.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Standard;
		this.cmbAlmacen.Enabled = false;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(325, 22);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(157, 21);
		this.cmbAlmacen.TabIndex = 7;
		this.cmbEmpresa.Enabled = false;
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(86, 22);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(157, 21);
		this.cmbEmpresa.TabIndex = 6;
		this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(cmbEmpresa_SelectionChangeCommitted);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(265, 25);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(54, 13);
		this.label15.TabIndex = 5;
		this.label15.Text = "Almacen :";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(26, 25);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 4;
		this.label14.Text = "Empresa :";
		this.rbUsuario.AutoSize = true;
		this.rbUsuario.Checked = true;
		this.rbUsuario.Location = new System.Drawing.Point(139, 51);
		this.rbUsuario.Name = "rbUsuario";
		this.rbUsuario.Size = new System.Drawing.Size(61, 17);
		this.rbUsuario.TabIndex = 3;
		this.rbUsuario.TabStop = true;
		this.rbUsuario.Text = "Usuario";
		this.rbUsuario.UseVisualStyleBackColor = true;
		this.rbUsuario.Visible = false;
		this.rbUsuario.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
		this.rbAdministrador.AutoSize = true;
		this.rbAdministrador.Location = new System.Drawing.Point(322, 51);
		this.rbAdministrador.Name = "rbAdministrador";
		this.rbAdministrador.Size = new System.Drawing.Size(88, 17);
		this.rbAdministrador.TabIndex = 2;
		this.rbAdministrador.Text = "Administrador";
		this.rbAdministrador.UseVisualStyleBackColor = true;
		this.rbAdministrador.Visible = false;
		this.rbAdministrador.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(428, 339);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 1;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(509, 339);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator8.ErrorMessage = "Repita la nueva contraseña.";
		this.customValidator8.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator8.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator8_ValidateValue);
		this.customValidator9.ErrorMessage = "La nueva contraseña no coincide.";
		this.customValidator9.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator9.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator9_ValidateValue);
		this.customValidator5.ErrorMessage = "Repita la contraseña.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator6.ErrorMessage = "La contraseña no coincide.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue_1);
		this.customValidator10.ErrorMessage = "Ingrese la nueva contraseña.";
		this.customValidator10.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator10.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator10_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el username.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese el apellido del usuario.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese el nombre del usuario.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.chkcanalventa.AutoSize = true;
		this.chkcanalventa.Checked = true;
		this.chkcanalventa.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkcanalventa.Location = new System.Drawing.Point(428, 241);
		this.chkcanalventa.Name = "chkcanalventa";
		this.chkcanalventa.Size = new System.Drawing.Size(104, 17);
		this.chkcanalventa.TabIndex = 38;
		this.chkcanalventa.Text = "Acceso a Todos";
		this.chkcanalventa.UseVisualStyleBackColor = true;
		this.cboCanalVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboCanalVenta.FormattingEnabled = true;
		this.cboCanalVenta.Location = new System.Drawing.Point(428, 264);
		this.cboCanalVenta.Name = "cboCanalVenta";
		this.cboCanalVenta.Size = new System.Drawing.Size(121, 21);
		this.cboCanalVenta.TabIndex = 37;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(347, 267);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(75, 13);
		this.label19.TabIndex = 36;
		this.label19.Text = "Canal Venta* :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(600, 369);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.tabControl1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionUsuario";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Usuario";
		base.Load += new System.EventHandler(frmGestionUsuario_Load);
		base.Shown += new System.EventHandler(frmGestionUsuario_Shown);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
