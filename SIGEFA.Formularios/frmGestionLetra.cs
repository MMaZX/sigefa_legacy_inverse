using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionLetra : Office2007Form
{
	private clsAdmLetra AdmLetra = new clsAdmLetra();

	public clsLetra letra = new clsLetra();

	private clsProveedor prov = new clsProveedor();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	public int Proceso = 0;

	public int CodProveedor = 0;

	private clsValidar valido = new clsValidar();

	private int CodDocumento = 9;

	private clsMoneda Mon = new clsMoneda();

	private clsAdmMoneda AdmMoned = new clsAdmMoneda();

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnGuardar;

	private GroupBox groupBox1;

	private TextBox txtMonto;

	private Label label5;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	private Label label16;

	private Label label17;

	private DateTimePicker dtpFechaVence;

	private Label label3;

	private DateTimePicker dtpFecha;

	private TextBox txtProveedor;

	private TextBox txtNumLetra;

	private Label label8;

	private Label label2;

	private Label label1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private TextBox txtDocRef;

	private Label label4;

	public frmGestionLetra()
	{
		InitializeComponent();
	}

	private void frmGestionLetra_Load(object sender, EventArgs e)
	{
		cargaMoneda();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
			txtTipoCambio.Visible = true;
		}
		else
		{
			txtTipoCambio.Text = "0";
		}
		if (Proceso == 2)
		{
			CargaLetra();
		}
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void CargaLetra()
	{
		letra = AdmLetra.CargaLetra(letra.CodLetra);
		if (letra != null)
		{
			txtNumLetra.Text = letra.NumDocumento;
			txtDocRef.Text = letra.DocumentoReferencia;
			txtProveedor.Text = letra.RazonSocialProveedor;
			dtpFecha.Value = letra.FechaEmision;
			dtpFechaVence.Value = letra.FechaVencimiento;
			cmbMoneda.SelectedValue = letra.CodMoneda;
			txtTipoCambio.Text = letra.TipoCambio.ToString();
			txtMonto.Text = letra.Monto.ToString();
		}
	}

	private void txtProveedor_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmProveedoresLista"] != null)
		{
			Application.OpenForms["frmProveedoresLista"].Activate();
			return;
		}
		frmProveedoresLista form = new frmProveedoresLista();
		form.Proceso = 3;
		form.Procede = 2;
		form.ShowDialog();
		if (CodProveedor != 0)
		{
			CargaProveedor();
			ProcessTabKey(forward: true);
		}
		else
		{
			BorrarProveedor();
		}
	}

	private void CargaProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtProveedor.Text = prov.RazonSocial;
	}

	private void BorrarProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtProveedor.Text = "";
	}

	private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void cmbMoneda_Leave(object sender, EventArgs e)
	{
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
			txtTipoCambio.Visible = true;
			label16.Visible = true;
		}
		else
		{
			txtTipoCambio.Text = "0";
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		if (txtTipoCambio.Visible)
		{
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Venta.ToString();
				return;
			}
			MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			dtpFecha.Value = DateTime.Now.Date;
			dtpFecha.Focus();
		}
	}

	private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void frmGestionLetra_Shown(object sender, EventArgs e)
	{
		if (Proceso == 1 && txtTipoCambio.Visible)
		{
			if (tc == null)
			{
				MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				txtTipoCambio.Text = tc.Venta.ToString();
			}
		}
		txtNumLetra.Focus();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate())
		{
			return;
		}
		letra.CodAlmacen = frmLogin.iCodAlmacen;
		letra.CodDocumento = CodDocumento;
		letra.NumDocumento = txtNumLetra.Text;
		letra.DocumentoReferencia = txtDocRef.Text;
		letra.CodProveedor = CodProveedor;
		letra.FechaEmision = dtpFecha.Value;
		letra.FechaVencimiento = dtpFechaVence.Value;
		letra.IngresoEgreso = false;
		letra.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		letra.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
		letra.Monto = Convert.ToDouble(txtMonto.Text);
		letra.MontoPendiente = Convert.ToDouble(txtMonto.Text);
		if (Proceso == 1)
		{
			if (AdmLetra.insert(letra))
			{
				MessageBox.Show("Se generó la letra correctamente", "Letra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				btnGuardar.Enabled = false;
			}
		}
		else if (Proceso == 2 && AdmLetra.update(letra))
		{
			MessageBox.Show("La letra se actualizó correctamente", "Letra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			btnGuardar.Enabled = false;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
	{
		valido.SOLONumeros(sender, e);
	}

	private void txtNumLetra_KeyPress(object sender, KeyPressEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionLetra));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtMonto = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.dtpFechaVence = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.txtProveedor = new System.Windows.Forms.TextBox();
		this.txtNumLetra = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 165);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(434, 50);
		this.groupBox3.TabIndex = 26;
		this.groupBox3.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(358, 12);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 22;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnGuardar.AllowDrop = true;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(266, 12);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(86, 32);
		this.btnGuardar.TabIndex = 21;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtMonto);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.dtpFechaVence);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.txtProveedor);
		this.groupBox1.Controls.Add(this.txtNumLetra);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(434, 165);
		this.groupBox1.TabIndex = 27;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Letra";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(266, 19);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(143, 20);
		this.txtDocRef.TabIndex = 71;
		this.superValidator1.SetValidator1(this.txtDocRef, this.customValidator1);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(206, 23);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(54, 12);
		this.label4.TabIndex = 72;
		this.label4.Text = "Doc. Ref.";
		this.txtMonto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtMonto.Location = new System.Drawing.Point(76, 123);
		this.txtMonto.Name = "txtMonto";
		this.txtMonto.Size = new System.Drawing.Size(91, 20);
		this.txtMonto.TabIndex = 7;
		this.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtMonto, this.customValidator3);
		this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMonto_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(13, 127);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(37, 12);
		this.label5.TabIndex = 70;
		this.label5.Text = "Monto";
		this.txtTipoCambio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(287, 97);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(91, 20);
		this.txtTipoCambio.TabIndex = 6;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtTipoCambio.Visible = false;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(76, 97);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(91, 20);
		this.cmbMoneda.TabIndex = 5;
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label16.ForeColor = System.Drawing.Color.SteelBlue;
		this.label16.Location = new System.Drawing.Point(213, 99);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(67, 12);
		this.label16.TabIndex = 68;
		this.label16.Text = "Tipo Cambio";
		this.label16.Visible = false;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label17.ForeColor = System.Drawing.Color.SteelBlue;
		this.label17.Location = new System.Drawing.Point(13, 101);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(45, 12);
		this.label17.TabIndex = 67;
		this.label17.Text = "Moneda";
		this.dtpFechaVence.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaVence.Location = new System.Drawing.Point(287, 71);
		this.dtpFechaVence.Name = "dtpFechaVence";
		this.dtpFechaVence.Size = new System.Drawing.Size(91, 20);
		this.dtpFechaVence.TabIndex = 4;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(179, 75);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(102, 12);
		this.label3.TabIndex = 63;
		this.label3.Text = "Fecha Vencimiento";
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(76, 71);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(91, 20);
		this.dtpFecha.TabIndex = 3;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.txtProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtProveedor.Location = new System.Drawing.Point(76, 45);
		this.txtProveedor.Name = "txtProveedor";
		this.txtProveedor.Size = new System.Drawing.Size(333, 20);
		this.txtProveedor.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtProveedor, this.customValidator2);
		this.txtProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtProveedor_KeyDown);
		this.txtNumLetra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumLetra.Location = new System.Drawing.Point(76, 19);
		this.txtNumLetra.Name = "txtNumLetra";
		this.txtNumLetra.Size = new System.Drawing.Size(100, 20);
		this.txtNumLetra.TabIndex = 1;
		this.superValidator1.SetValidator1(this.txtNumLetra, this.customValidator1);
		this.txtNumLetra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumLetra_KeyPress);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(13, 75);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(36, 12);
		this.label8.TabIndex = 59;
		this.label8.Text = "Fecha";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(13, 49);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(57, 12);
		this.label2.TabIndex = 58;
		this.label2.Text = "Proveedor";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(13, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(47, 12);
		this.label1.TabIndex = 57;
		this.label1.Text = "N° Letra";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Ingrese el numero de documento.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el monto.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Escoja un proveedor.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.AllowDrop = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(434, 215);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionLetra";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Letra";
		base.Load += new System.EventHandler(frmGestionLetra_Load);
		base.Shown += new System.EventHandler(frmGestionLetra_Shown);
		this.groupBox3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
