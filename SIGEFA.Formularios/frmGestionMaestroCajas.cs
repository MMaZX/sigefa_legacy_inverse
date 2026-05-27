using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionMaestroCajas : Office2007Form
{
	public int Proceso = 0;

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	public clsAlmacen alm = new clsAlmacen();

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private IContainer components = null;

	private GroupBox groupBox1;

	private CheckBox cbActivo;

	private TextBox txtDescripcion;

	private Label label5;

	private TextBox txtNombre;

	private TextBox txtCodAlmacen;

	private Label label2;

	private Label label1;

	private ImageList imageList1;

	private Button btnCancelar;

	private Button btnAceptar;

	private ComboBox cboSucursal;

	private Label label14;

	private TextBox txtTelefono;

	private Label label3;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CheckBox cbPrincipal;

	private RadioButton radioButton2;

	private RadioButton radioButton1;

	public frmGestionMaestroCajas()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtNombre.Text != ""))
		{
			return;
		}
		double CodAlm = 0.0;
		if (txtCodAlmacen.Text == "")
		{
			CodAlm = 0.0;
		}
		else
		{
			CodAlm = Convert.ToDouble(txtCodAlmacen.Text);
		}
		alm.Nombre = txtNombre.Text;
		alm.Telefono = txtTelefono.Text;
		alm.Descripcion = txtDescripcion.Text;
		alm.Estado = cbActivo.Checked;
		alm.CodEmpresa = frmLogin.iCodEmpresa;
		alm.CodUser = frmLogin.iCodUser;
		alm.CodSuc = Convert.ToInt32(cboSucursal.SelectedValue);
		alm.EstadoPrincipal = cbPrincipal.Checked;
		if (Proceso == 1)
		{
			if (admAlm.insert(alm))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Almacen", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		else if (Proceso == 2 && admAlm.update(alm))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Almacen", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void frmGestionAlmacen_Load(object sender, EventArgs e)
	{
		CargaSucursales();
		if (Proceso == 1)
		{
			cboSucursal.SelectedValue = frmLogin.iCodEmpresa;
		}
		else if (Proceso == 2)
		{
			CargaAlmacen();
		}
		else if (Proceso == 3)
		{
			CargaAlmacen();
			ext.sololectura(groupBox1.Controls);
			btnAceptar.Visible = false;
			btnCancelar.Text = "Aceptar";
			btnCancelar.ImageIndex = 1;
		}
	}

	private void CargaAlmacen()
	{
		alm = admAlm.CargaAlmacen(alm.CodAlmacen);
		cboSucursal.SelectedValue = alm.CodSuc;
		txtCodAlmacen.Text = alm.CodAlmacen.ToString();
		txtNombre.Text = alm.Nombre;
		txtTelefono.Text = alm.Telefono;
		txtDescripcion.Text = alm.Descripcion;
		cbActivo.Checked = alm.Estado;
		cbPrincipal.Checked = alm.EstadoPrincipal;
	}

	private void CargaSucursales()
	{
		cboSucursal.DataSource = AdmSuc.CargaSucursales(frmLogin.iCodEmpresa);
		cboSucursal.DisplayMember = "nombre";
		cboSucursal.ValueMember = "codSucursal";
		cboSucursal.SelectedIndex = 1;
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
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

	private void cboSucursal_SelectionChangeCommitted(object sender, EventArgs e)
	{
		txtNombre.Focus();
	}

	private void txtNombre_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtUbicacion_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtTelefono.Focus();
		}
	}

	private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDescripcion.Focus();
		}
	}

	private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnAceptar.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionMaestroCajas));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbPrincipal = new System.Windows.Forms.CheckBox();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cboSucursal = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.cbActivo = new System.Windows.Forms.CheckBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.txtCodAlmacen = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.radioButton2);
		this.groupBox1.Controls.Add(this.radioButton1);
		this.groupBox1.Controls.Add(this.cbPrincipal);
		this.groupBox1.Controls.Add(this.txtTelefono);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.cboSucursal);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.cbActivo);
		this.groupBox1.Controls.Add(this.txtDescripcion);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtNombre);
		this.groupBox1.Controls.Add(this.txtCodAlmacen);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(456, 176);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle del Almacen";
		this.cbPrincipal.AutoSize = true;
		this.cbPrincipal.Location = new System.Drawing.Point(270, 98);
		this.cbPrincipal.Name = "cbPrincipal";
		this.cbPrincipal.Size = new System.Drawing.Size(66, 17);
		this.cbPrincipal.TabIndex = 5;
		this.cbPrincipal.Text = "Principal";
		this.cbPrincipal.UseVisualStyleBackColor = true;
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(87, 94);
		this.txtTelefono.MaxLength = 15;
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(100, 20);
		this.txtTelefono.TabIndex = 4;
		this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelefono_KeyDown);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(26, 98);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(55, 13);
		this.label3.TabIndex = 20;
		this.label3.Text = "Teléfono :";
		this.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboSucursal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cboSucursal.FormattingEnabled = true;
		this.cboSucursal.Location = new System.Drawing.Point(220, 18);
		this.cboSucursal.Name = "cboSucursal";
		this.cboSucursal.Size = new System.Drawing.Size(206, 21);
		this.cboSucursal.TabIndex = 1;
		this.superValidator1.SetValidator1(this.cboSucursal, this.customValidator3);
		this.cboSucursal.SelectionChangeCommitted += new System.EventHandler(cboSucursal_SelectionChangeCommitted);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(163, 22);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(51, 13);
		this.label14.TabIndex = 9;
		this.label14.Text = "Sucursal:";
		this.cbActivo.AutoSize = true;
		this.cbActivo.Checked = true;
		this.cbActivo.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbActivo.Location = new System.Drawing.Point(351, 98);
		this.cbActivo.Name = "cbActivo";
		this.cbActivo.Size = new System.Drawing.Size(56, 17);
		this.cbActivo.TabIndex = 6;
		this.cbActivo.Text = "Activo";
		this.cbActivo.UseVisualStyleBackColor = true;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(87, 119);
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(339, 53);
		this.txtDescripcion.TabIndex = 7;
		this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDescripcion_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(15, 119);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(66, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Descripción:";
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(87, 44);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(339, 20);
		this.txtNombre.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtNombre, this.customValidator1);
		this.txtNombre.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNombre_KeyDown);
		this.txtCodAlmacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodAlmacen.Enabled = false;
		this.txtCodAlmacen.Location = new System.Drawing.Point(87, 19);
		this.txtCodAlmacen.Name = "txtCodAlmacen";
		this.txtCodAlmacen.Size = new System.Drawing.Size(54, 20);
		this.txtCodAlmacen.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(34, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(47, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Nombre:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(35, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Codigo :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(363, 206);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(282, 206);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(75, 23);
		this.btnAceptar.TabIndex = 1;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator3.ErrorMessage = "Seleccione una Empresa";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese la ubicación.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese el Nombre del Almacen.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.radioButton1.AutoSize = true;
		this.radioButton1.Location = new System.Drawing.Point(87, 71);
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.Size = new System.Drawing.Size(85, 17);
		this.radioButton1.TabIndex = 21;
		this.radioButton1.TabStop = true;
		this.radioButton1.Text = "radioButton1";
		this.radioButton1.UseVisualStyleBackColor = true;
		this.radioButton2.AutoSize = true;
		this.radioButton2.Location = new System.Drawing.Point(208, 71);
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.Size = new System.Drawing.Size(85, 17);
		this.radioButton2.TabIndex = 22;
		this.radioButton2.TabStop = true;
		this.radioButton2.Text = "radioButton2";
		this.radioButton2.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(480, 236);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionMaestroCajas";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Cajas";
		base.Load += new System.EventHandler(frmGestionAlmacen_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
