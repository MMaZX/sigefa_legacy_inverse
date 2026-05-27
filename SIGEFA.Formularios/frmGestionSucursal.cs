using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionSucursal : Office2007Form
{
	public int Proceso = 0;

	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	public clsSucursal suc = new clsSucursal();

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox txtCodSucursal;

	private TextBox txtTelefono;

	private Label label5;

	private CheckBox cbActivo;

	private Button btnCancelar;

	private Button btnAceptar;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private TextBox txtUbicacion;

	private TextBox txtNombre;

	private Label label10;

	private Label label9;

	private Label label8;

	private TextBox txtDescripcion;

	private Label label2;

	private ComboBox cboEmpresa;

	private CustomValidator customValidator3;

	public frmGestionSucursal()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtNombre.Text != ""))
		{
			return;
		}
		if (txtCodSucursal.Text == "")
		{
			suc.CodSucursal = 0;
		}
		else
		{
			suc.CodSucursal = Convert.ToInt32(txtCodSucursal.Text);
		}
		suc.Nombre = txtNombre.Text;
		suc.Ubicacion = txtUbicacion.Text;
		suc.Telefono = txtTelefono.Text;
		suc.Descripcion = txtDescripcion.Text;
		suc.Estado = cbActivo.Checked;
		suc.CodEmpresa = Convert.ToInt32(cboEmpresa.SelectedValue);
		suc.CodUser = frmLogin.iCodUser;
		if (Proceso == 1)
		{
			if (AdmSuc.insert(suc))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Sucursal", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		else if (Proceso == 2 && AdmSuc.update(suc))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Sucursal", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void CargaEmpresas()
	{
		cboEmpresa.DataSource = AdmEmp.CargaEmpresas();
		cboEmpresa.DisplayMember = "razonsocial";
		cboEmpresa.ValueMember = "codEmpresa";
		cboEmpresa.SelectedIndex = 0;
	}

	private void frmGestionEmpresa_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		if (Proceso == 2)
		{
			CargaSucursal();
		}
		else if (Proceso == 3)
		{
			CargaSucursal();
			ext.sololectura(groupBox1.Controls);
			btnAceptar.Visible = false;
			btnCancelar.Text = "Aceptar";
			btnCancelar.ImageIndex = 1;
		}
	}

	private void CargaSucursal()
	{
		suc = AdmSuc.CargaSucursal(suc.CodSucursal);
		txtCodSucursal.Text = suc.CodSucursal.ToString();
		txtNombre.Text = suc.Nombre;
		txtUbicacion.Text = suc.Ubicacion;
		txtTelefono.Text = suc.Telefono;
		txtDescripcion.Text = suc.Descripcion;
		cbActivo.Checked = suc.Estado;
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
	}

	private void txtRUC_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private void txtFax_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0 && c.Visible)
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

	private void cboEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
	{
		txtNombre.Focus();
	}

	private void txtNombre_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtUbicacion.Focus();
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionSucursal));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cboEmpresa = new System.Windows.Forms.ComboBox();
		this.cbActivo = new System.Windows.Forms.CheckBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtCodSucursal = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtUbicacion = new System.Windows.Forms.TextBox();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cboEmpresa);
		this.groupBox1.Controls.Add(this.cbActivo);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtDescripcion);
		this.groupBox1.Controls.Add(this.txtTelefono);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtCodSucursal);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtUbicacion);
		this.groupBox1.Controls.Add(this.txtNombre);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(465, 151);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle de la Empresa";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(174, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(51, 13);
		this.label2.TabIndex = 20;
		this.label2.Text = "Empresa:";
		this.cboEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboEmpresa.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cboEmpresa.FormattingEnabled = true;
		this.cboEmpresa.Location = new System.Drawing.Point(231, 23);
		this.cboEmpresa.Name = "cboEmpresa";
		this.cboEmpresa.Size = new System.Drawing.Size(206, 21);
		this.cboEmpresa.TabIndex = 1;
		this.superValidator1.SetValidator1(this.cboEmpresa, this.customValidator3);
		this.cboEmpresa.SelectionChangeCommitted += new System.EventHandler(cboEmpresa_SelectionChangeCommitted);
		this.cbActivo.AutoSize = true;
		this.cbActivo.Checked = true;
		this.cbActivo.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbActivo.Location = new System.Drawing.Point(381, 0);
		this.cbActivo.Name = "cbActivo";
		this.cbActivo.Size = new System.Drawing.Size(56, 17);
		this.cbActivo.TabIndex = 0;
		this.cbActivo.Text = "Activo";
		this.cbActivo.UseVisualStyleBackColor = true;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(13, 131);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(66, 13);
		this.label9.TabIndex = 17;
		this.label9.Text = "Descripcion:";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(21, 80);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(58, 13);
		this.label10.TabIndex = 18;
		this.label10.Text = "Ubicacion:";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(85, 128);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(352, 20);
		this.txtDescripcion.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator2);
		this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDescripcion_KeyDown);
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(85, 102);
		this.txtTelefono.MaxLength = 15;
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(100, 20);
		this.txtTelefono.TabIndex = 4;
		this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelefono_KeyDown);
		this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTelefono_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(27, 105);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(52, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Telefono:";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(32, 54);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(47, 13);
		this.label8.TabIndex = 16;
		this.label8.Text = "Nombre:";
		this.txtCodSucursal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodSucursal.Enabled = false;
		this.txtCodSucursal.Location = new System.Drawing.Point(85, 24);
		this.txtCodSucursal.Name = "txtCodSucursal";
		this.txtCodSucursal.Size = new System.Drawing.Size(62, 20);
		this.txtCodSucursal.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(33, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Codigo :";
		this.txtUbicacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUbicacion.Location = new System.Drawing.Point(85, 76);
		this.txtUbicacion.Name = "txtUbicacion";
		this.txtUbicacion.Size = new System.Drawing.Size(352, 20);
		this.txtUbicacion.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtUbicacion, this.customValidator2);
		this.txtUbicacion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUbicacion_KeyDown);
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(85, 50);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(352, 20);
		this.txtNombre.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtNombre, this.customValidator2);
		this.txtNombre.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNombre_KeyDown);
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(374, 186);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(293, 186);
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
		this.customValidator3.ErrorMessage = "Ingrese Empresa";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese la Razon Social";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator1.ErrorMessage = "El RUC ingresado no es valido";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(489, 215);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionSucursal";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Sucursales";
		base.Load += new System.EventHandler(frmGestionEmpresa_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
