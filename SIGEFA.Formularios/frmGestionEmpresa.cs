using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionEmpresa : Office2007Form
{
	public int Proceso = 0;

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	public clsEmpresa emp = new clsEmpresa();

	private byte[] logo = null;

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox txtRazonSocial;

	private TextBox txtCodEmpresa;

	private Label label2;

	private TextBox txtDireccion;

	private Label label4;

	private TextBox txtRUC;

	private Label label3;

	private TextBox txtRepresentante;

	private Label label7;

	private TextBox txtFax;

	private Label label6;

	private TextBox txtTelefono;

	private Label label5;

	private CheckBox cbActivoE;

	private Button btnCancelar;

	private Button btnAceptar;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private LabelX labelX1;

	private LinkLabel linkLabel1;

	private Label label8;

	private OpenFileDialog openFileDialog1;

	private TextBox txtcorto;

	private Label label9;

	public frmGestionEmpresa()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtRUC.Text != ""))
		{
			return;
		}
		double CodEmp = 0.0;
		if (txtCodEmpresa.Text == "")
		{
			CodEmp = 0.0;
		}
		else
		{
			CodEmp = Convert.ToDouble(txtCodEmpresa.Text);
		}
		emp.Ruc = txtRUC.Text;
		emp.RazonSocial = txtRazonSocial.Text;
		emp.Direccion = txtDireccion.Text;
		emp.Telefono = txtTelefono.Text;
		emp.NombreCorto = txtcorto.Text;
		emp.Fax = txtFax.Text;
		emp.Representante = txtRepresentante.Text;
		emp.CodUser = frmLogin.iCodUser;
		emp.Estado = cbActivoE.Checked;
		emp.Logo = logo;
		if (Proceso == 1)
		{
			if (admEmp.insert(emp))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
		}
		else if (Proceso == 2 && admEmp.update(emp))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Empresa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	private void frmGestionEmpresa_Load(object sender, EventArgs e)
	{
		if (Proceso == 2)
		{
			CargaEmpresa();
		}
		else if (Proceso == 3)
		{
			CargaEmpresa();
			ext.sololectura(groupBox1.Controls);
			btnAceptar.Visible = false;
			btnCancelar.Text = "Aceptar";
			btnCancelar.ImageIndex = 1;
		}
	}

	private void CargaEmpresa()
	{
		emp = admEmp.CargaEmpresa(emp.CodEmpresa);
		txtCodEmpresa.Text = emp.CodEmpresa.ToString();
		txtRUC.Text = emp.Ruc;
		txtRazonSocial.Text = emp.RazonSocial;
		txtDireccion.Text = emp.Direccion;
		txtTelefono.Text = emp.Telefono;
		txtcorto.Text = emp.NombreCorto;
		txtFax.Text = emp.Fax;
		txtRepresentante.Text = emp.Representante;
		cbActivoE.Checked = emp.Estado;
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
			cbActivoE.Checked = true;
		}
	}

	private void txtRUC_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F2)
		{
			if (ext.rucsunat(txtRUC.Text))
			{
				txtRazonSocial.Text = ext.RazonSocial;
				txtDireccion.Text = ext.DireccionLegal;
			}
			else
			{
				ext.limpiar(base.Controls);
				cbActivoE.Checked = true;
			}
		}
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

	private void txtRazonSocial_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtDireccion.Focus();
		}
	}

	private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
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
			txtFax.Focus();
		}
	}

	private void txtFax_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtRepresentante.Focus();
		}
	}

	private void txtRepresentante_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnAceptar.Focus();
		}
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		OpenFileDialog fileOpen = new OpenFileDialog();
		fileOpen.Title = "Seleccionar Imagen";
		fileOpen.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
		if (fileOpen.ShowDialog() != DialogResult.Cancel)
		{
			linkLabel1.Text = fileOpen.FileName;
			string fileName = fileOpen.FileName;
			try
			{
				logo = get_image(fileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al cargar imagen" + ex.Message.ToString());
			}
		}
	}

	private byte[] get_image(string ruta)
	{
		FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
		BinaryReader reader = new BinaryReader(stream);
		byte[] photo = reader.ReadBytes((int)stream.Length);
		reader.Close();
		stream.Close();
		return photo;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionEmpresa));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.label8 = new System.Windows.Forms.Label();
		this.cbActivoE = new System.Windows.Forms.CheckBox();
		this.txtRepresentante = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtFax = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtRUC = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtRazonSocial = new System.Windows.Forms.TextBox();
		this.txtCodEmpresa = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.txtcorto = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtcorto);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.linkLabel1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.cbActivoE);
		this.groupBox1.Controls.Add(this.txtRepresentante);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtFax);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtTelefono);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtRUC);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtRazonSocial);
		this.groupBox1.Controls.Add(this.txtCodEmpresa);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(465, 208);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle de la Empresa";
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(260, 131);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(98, 13);
		this.linkLabel1.TabIndex = 14;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "Selecciona Imagen";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(223, 132);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(31, 13);
		this.label8.TabIndex = 13;
		this.label8.Text = "Logo";
		this.cbActivoE.AutoSize = true;
		this.cbActivoE.Checked = true;
		this.cbActivoE.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbActivoE.Location = new System.Drawing.Point(340, 151);
		this.cbActivoE.Name = "cbActivoE";
		this.cbActivoE.Size = new System.Drawing.Size(56, 17);
		this.cbActivoE.TabIndex = 6;
		this.cbActivoE.Text = "Activo";
		this.cbActivoE.UseVisualStyleBackColor = true;
		this.txtRepresentante.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRepresentante.Location = new System.Drawing.Point(101, 178);
		this.txtRepresentante.Name = "txtRepresentante";
		this.txtRepresentante.Size = new System.Drawing.Size(336, 20);
		this.txtRepresentante.TabIndex = 7;
		this.txtRepresentante.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRepresentante_KeyDown);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(15, 181);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(80, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Representante:";
		this.txtFax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFax.Location = new System.Drawing.Point(101, 152);
		this.txtFax.MaxLength = 20;
		this.txtFax.Name = "txtFax";
		this.txtFax.Size = new System.Drawing.Size(100, 20);
		this.txtFax.TabIndex = 5;
		this.txtFax.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFax_KeyDown);
		this.txtFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFax_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(15, 155);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(27, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Fax:";
		this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTelefono.Location = new System.Drawing.Point(101, 126);
		this.txtTelefono.MaxLength = 20;
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(100, 20);
		this.txtTelefono.TabIndex = 4;
		this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTelefono_KeyDown);
		this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTelefono_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(15, 129);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(52, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Telefono:";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(101, 98);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(336, 20);
		this.txtDireccion.TabIndex = 3;
		this.txtDireccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDireccion_KeyDown);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(15, 101);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(55, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Dirección:";
		this.txtRUC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtRUC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRUC.Location = new System.Drawing.Point(334, 15);
		this.txtRUC.MaxLength = 11;
		this.txtRUC.Name = "txtRUC";
		this.txtRUC.Size = new System.Drawing.Size(103, 20);
		this.txtRUC.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtRUC, this.customValidator1);
		this.txtRUC.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUC_KeyDown);
		this.txtRUC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRUC_KeyPress);
		this.txtRUC.Leave += new System.EventHandler(txtRUC_Leave);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(276, 18);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(52, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "R.U.C. * :";
		this.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRazonSocial.Location = new System.Drawing.Point(101, 45);
		this.txtRazonSocial.Name = "txtRazonSocial";
		this.txtRazonSocial.Size = new System.Drawing.Size(336, 20);
		this.txtRazonSocial.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtRazonSocial, this.customValidator2);
		this.txtRazonSocial.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRazonSocial_KeyDown);
		this.txtCodEmpresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodEmpresa.Enabled = false;
		this.txtCodEmpresa.Location = new System.Drawing.Point(101, 15);
		this.txtCodEmpresa.Name = "txtCodEmpresa";
		this.txtCodEmpresa.Size = new System.Drawing.Size(100, 20);
		this.txtCodEmpresa.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(73, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Razon Social:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(15, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Codigo :";
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(402, 226);
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
		this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(321, 226);
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
		this.customValidator1.ErrorMessage = "El RUC ingresado no es valido";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese la Razon Social";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Location = new System.Drawing.Point(12, 226);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(288, 23);
		this.labelX1.TabIndex = 11;
		this.labelX1.Text = "(*) Presione F2 para verificar los datos con la SUNAT";
		this.labelX1.TextLineAlignment = System.Drawing.StringAlignment.Near;
		this.labelX1.Visible = false;
		this.openFileDialog1.FileName = "openFileDialog1";
		this.txtcorto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtcorto.Location = new System.Drawing.Point(101, 71);
		this.txtcorto.Name = "txtcorto";
		this.txtcorto.Size = new System.Drawing.Size(167, 20);
		this.txtcorto.TabIndex = 15;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(15, 74);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(75, 13);
		this.label9.TabIndex = 16;
		this.label9.Text = "Nombre Corto:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 269);
		base.Controls.Add(this.labelX1);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionEmpresa";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Empresa";
		base.Load += new System.EventHandler(frmGestionEmpresa_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
