using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmIngresoBanco : Office2007Form
{
	private clsAdmBanco AdmBanco = new clsAdmBanco();

	private clsBanco banco = new clsBanco();

	private clsAdmLetra AdmLetra = new clsAdmLetra();

	private clsLetra letra = new clsLetra();

	private clsValidar valido = new clsValidar();

	public int CodLetra = 0;

	public int Proceso = 0;

	private IContainer components = null;

	private ComboBox cmbBancos;

	private TextBox txtnumero;

	private Label label1;

	private Label label2;

	private ImageList imageList1;

	private Button btnSalir;

	private Button btnGuardar;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	public frmIngresoBanco()
	{
		InitializeComponent();
	}

	private void frmIngresoBanco_Load(object sender, EventArgs e)
	{
		CargaBancos();
		CargaLetra();
	}

	private void CargaBancos()
	{
		cmbBancos.DataSource = AdmBanco.CargaBancos();
		cmbBancos.DisplayMember = "descripcion";
		cmbBancos.ValueMember = "codBanco";
	}

	private void CargaLetra()
	{
		letra = AdmLetra.CargaLetra(CodLetra);
		if (letra != null)
		{
			cmbBancos.SelectedValue = letra.CodBanco;
			txtnumero.Text = letra.NumeroUnico;
		}
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

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (superValidator1.Validate())
		{
			letra.CodBanco = Convert.ToInt32(cmbBancos.SelectedValue);
			letra.NumeroUnico = txtnumero.Text;
			if (Proceso == 1 && AdmLetra.update(letra))
			{
				MessageBox.Show("Se realizó en registro correctamente", "Letra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				btnGuardar.Enabled = false;
				Close();
			}
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtnumero_KeyPress(object sender, KeyPressEventArgs e)
	{
		valido.SOLONumeros(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmIngresoBanco));
		this.cmbBancos = new System.Windows.Forms.ComboBox();
		this.txtnumero = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.cmbBancos.FormattingEnabled = true;
		this.cmbBancos.Location = new System.Drawing.Point(92, 12);
		this.cmbBancos.Name = "cmbBancos";
		this.cmbBancos.Size = new System.Drawing.Size(302, 21);
		this.cmbBancos.TabIndex = 0;
		this.superValidator1.SetValidator1(this.cmbBancos, this.customValidator2);
		this.txtnumero.Location = new System.Drawing.Point(92, 39);
		this.txtnumero.Name = "txtnumero";
		this.txtnumero.Size = new System.Drawing.Size(191, 20);
		this.txtnumero.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtnumero, this.customValidator1);
		this.txtnumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtnumero_KeyPress);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(27, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 12);
		this.label1.TabIndex = 58;
		this.label1.Text = "Banco";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(27, 43);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(50, 12);
		this.label2.TabIndex = 59;
		this.label2.Text = "N° Unico";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(355, 72);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 61;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.AllowDrop = true;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(263, 72);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(86, 32);
		this.btnGuardar.TabIndex = 60;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator2.ErrorMessage = "Seleccione un banco.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese el N° único.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(435, 116);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.txtnumero);
		base.Controls.Add(this.cmbBancos);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmIngresoBanco";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ingreso Banco";
		base.Load += new System.EventHandler(frmIngresoBanco_Load);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
