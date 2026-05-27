using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRegistroChequeCaja : Office2007Form
{
	private clsAdmSucursal admSucur = new clsAdmSucursal();

	private clsValidar val = new clsValidar();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label2;

	private ComboBox cmbSucursal;

	private Label label1;

	private DateTimePicker dtpFechaLiquidacion;

	private Label label3;

	private TextBox txtMontoLiquidar;

	private Label label4;

	private TextBox txtNumCheque;

	private Label label5;

	private TextBox txtResponsable;

	private ImageList imageList2;

	private ImageList imageList1;

	private Button btnGuardar;

	private Button btnCancelar;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator4;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private CheckBox cbotraCaja;

	private ImageList imageList3;

	public frmRegistroChequeCaja()
	{
		InitializeComponent();
	}

	private void CargaSucursales()
	{
		cmbSucursal.DataSource = admSucur.ListaSucursales();
		cmbSucursal.DisplayMember = "nombre";
		cmbSucursal.ValueMember = "codSucursal";
		cmbSucursal.SelectedIndex = -1;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
	}

	private void Limpiar()
	{
		cmbSucursal.SelectedIndex = -1;
		txtMontoLiquidar.Text = "";
		txtNumCheque.Text = "";
		txtResponsable.Text = "";
		btnGuardar.Enabled = false;
	}

	private void frmRegitroChequeCaja_Load(object sender, EventArgs e)
	{
		CargaSucursales();
		dtpFechaLiquidacion.Value = DateTime.Now.Date;
		cmbSucursal.Focus();
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtResponsable_TextChanged(object sender, EventArgs e)
	{
		btnGuardar.Enabled = true;
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
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

	private void txtMontoLiquidar_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void txtNumCheque_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumerosDoc(sender, e);
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
		if (e.ControlToValidate.Text != "")
		{
			e.IsValid = true;
		}
		else
		{
			e.IsValid = false;
		}
	}

	private void cmbSucursal_SelectionChangeCommitted(object sender, EventArgs e)
	{
		btnGuardar.Enabled = true;
		txtMontoLiquidar.Focus();
	}

	private void txtMontoLiquidar_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtNumCheque.Focus();
		}
	}

	private void txtNumCheque_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			dtpFechaLiquidacion.Focus();
		}
	}

	private void dtpFechaLiquidacion_ValueChanged(object sender, EventArgs e)
	{
		txtResponsable.Focus();
	}

	private void txtResponsable_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnGuardar.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRegistroChequeCaja));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbotraCaja = new System.Windows.Forms.CheckBox();
		this.txtResponsable = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNumCheque = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.dtpFechaLiquidacion = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.txtMontoLiquidar = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbSucursal = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardar = new System.Windows.Forms.Button();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnCancelar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbotraCaja);
		this.groupBox1.Controls.Add(this.txtResponsable);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtNumCheque);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.dtpFechaLiquidacion);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtMontoLiquidar);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbSucursal);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(481, 170);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Nuevo Registro";
		this.cbotraCaja.AutoSize = true;
		this.cbotraCaja.Location = new System.Drawing.Point(331, 25);
		this.cbotraCaja.Name = "cbotraCaja";
		this.cbotraCaja.Size = new System.Drawing.Size(97, 17);
		this.cbotraCaja.TabIndex = 1;
		this.cbotraCaja.Text = "Otra CajaChica";
		this.cbotraCaja.UseVisualStyleBackColor = true;
		this.txtResponsable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtResponsable.Location = new System.Drawing.Point(132, 134);
		this.txtResponsable.Name = "txtResponsable";
		this.txtResponsable.Size = new System.Drawing.Size(334, 20);
		this.txtResponsable.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtResponsable, this.customValidator4);
		this.txtResponsable.KeyDown += new System.Windows.Forms.KeyEventHandler(txtResponsable_KeyDown);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(7, 137);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(117, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Responsable de Envio:";
		this.txtNumCheque.Location = new System.Drawing.Point(132, 75);
		this.txtNumCheque.MaxLength = 15;
		this.txtNumCheque.Name = "txtNumCheque";
		this.txtNumCheque.Size = new System.Drawing.Size(118, 20);
		this.txtNumCheque.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtNumCheque, this.customValidator3);
		this.txtNumCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNumCheque_KeyDown);
		this.txtNumCheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumCheque_KeyPress);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(32, 78);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(94, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "N° Cheque de Liq:";
		this.dtpFechaLiquidacion.Checked = false;
		this.dtpFechaLiquidacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaLiquidacion.Location = new System.Drawing.Point(132, 105);
		this.dtpFechaLiquidacion.Name = "dtpFechaLiquidacion";
		this.dtpFechaLiquidacion.Size = new System.Drawing.Size(118, 20);
		this.dtpFechaLiquidacion.TabIndex = 4;
		this.dtpFechaLiquidacion.ValueChanged += new System.EventHandler(dtpFechaLiquidacion_ValueChanged);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(86, 111);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Fecha:";
		this.txtMontoLiquidar.Location = new System.Drawing.Point(132, 49);
		this.txtMontoLiquidar.MaxLength = 15;
		this.txtMontoLiquidar.Name = "txtMontoLiquidar";
		this.txtMontoLiquidar.Size = new System.Drawing.Size(118, 20);
		this.txtMontoLiquidar.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtMontoLiquidar, this.customValidator2);
		this.txtMontoLiquidar.KeyDown += new System.Windows.Forms.KeyEventHandler(txtMontoLiquidar_KeyDown);
		this.txtMontoLiquidar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMontoLiquidar_KeyPress);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(37, 52);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(89, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Monto a Liquidar:";
		this.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSucursal.FormattingEnabled = true;
		this.cmbSucursal.Location = new System.Drawing.Point(132, 22);
		this.cmbSucursal.Name = "cmbSucursal";
		this.cmbSucursal.Size = new System.Drawing.Size(165, 21);
		this.cmbSucursal.TabIndex = 0;
		this.superValidator1.SetValidator1(this.cmbSucursal, this.customValidator1);
		this.cmbSucursal.SelectionChangeCommitted += new System.EventHandler(cmbSucursal_SelectionChangeCommitted);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(75, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(51, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Sucursal:";
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList1.Images.SetKeyName(19, "egreso.png");
		this.imageList1.Images.SetKeyName(20, "ingreso.png");
		this.imageList1.Images.SetKeyName(21, "icon_shelfs.png");
		this.imageList1.Images.SetKeyName(22, "EXIT2.png");
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList3;
		this.btnGuardar.Location = new System.Drawing.Point(145, 189);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(89, 34);
		this.btnGuardar.TabIndex = 1;
		this.btnGuardar.Text = "Guardar Registro";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageKey = "icon_shelfs.png";
		this.btnCancelar.ImageList = this.imageList1;
		this.btnCancelar.Location = new System.Drawing.Point(262, 189);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(86, 34);
		this.btnCancelar.TabIndex = 2;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator4.ErrorMessage = "Ingrese Responsable";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese N° Cheque";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese un Monto";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Seleccione una Sucursal";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(506, 231);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmRegistroChequeCaja";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "RegitroChequeCaja";
		base.Load += new System.EventHandler(frmRegitroChequeCaja_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
