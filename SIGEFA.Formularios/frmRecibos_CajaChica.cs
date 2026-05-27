using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmRecibos_CajaChica : Office2007Form
{
	private clsValidar val = new clsValidar();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmMoneda AdmMoned = new clsAdmMoneda();

	private clsCaja caja = new clsCaja();

	private clsCajaChicaMov cajachica = new clsCajaChicaMov();

	private clsAdmAperturaCierre admcajachica = new clsAdmAperturaCierre();

	public int CodtipoCajaChica = 0;

	public int Tipo = 0;

	public int Proceso = 0;

	public int tipocaja = 0;

	public int CodCaja = 0;

	public decimal SaldoCaja = default(decimal);

	private string sigl;

	public int CodDocumento = 0;

	public int codrecibo = 0;

	public int CodSerie;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnGuardar;

	private Label label2;

	private Label label3;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	public TextBox txtDescripcion;

	public DateTimePicker dtpFecha;

	private CustomValidator customValidator1;

	private CompareValidator compareValidator2;

	private CompareValidator compareValidator1;

	public TextBox txtMonto;

	private Label label4;

	private CustomValidator customValidator3;

	private CustomValidator customValidator2;

	private CustomValidator customValidator4;

	private GroupBox groupBox5;

	private TextBox txtdoc;

	private Label label18;

	private Label label15;

	public TextBox txtDni;

	private Label label17;

	public TextBox txtNombre;

	public ComboBox cmbTipo;

	private Label label8;

	public ComboBox cmbtipopagoser;

	public Label label1;

	private Button btnReporte2;

	public Label lblSaldoCaja;

	private Label label5;

	private TextBox txtDocRef;

	private TextBox txtNumero;

	public TextBox txtSerie;

	private Label label6;

	public ComboBox cmbMoneda;

	public TextBox txtTipoCambio;

	private Label label7;

	private Label label9;

	public frmRecibos_CajaChica()
	{
		InitializeComponent();
	}

	private void frmRecibos_CajaChica_Load(object sender, EventArgs e)
	{
		cargaMoneda();
		CargaTipoCambio();
		if (Proceso == 3)
		{
			SoloLectura(estado: true);
		}
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
	}

	private void CargaTipoCambio()
	{
		tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
			return;
		}
		txtTipoCambio.Text = "";
		txtTipoCambio.ReadOnly = false;
	}

	private void valida_serie(string sigl)
	{
	}

	private void SoloLectura(bool estado)
	{
		txtDescripcion.Enabled = !estado;
		txtMonto.Enabled = !estado;
		dtpFecha.Enabled = !estado;
		btnGuardar.Visible = !estado;
		txtNombre.Enabled = !estado;
		txtDni.Enabled = !estado;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0)
		{
			return;
		}
		cajachica.CodSucursal = frmLogin.iCodSucursal;
		cajachica.Codcaja = CodCaja;
		cajachica.CodPago = 0;
		cajachica.Concepto = txtDescripcion.Text;
		cajachica.Monto = Convert.ToDecimal(txtMonto.Text.Trim());
		cajachica.Nombre = txtNombre.Text;
		cajachica.Dni = txtDni.Text;
		if (cmbTipo.SelectedIndex == 0)
		{
			cajachica.Tipo = 1;
		}
		else if (cmbTipo.SelectedIndex == 1)
		{
			cajachica.Tipo = 2;
		}
		cajachica.Tipomovimiento = 2;
		cajachica.Fecha = dtpFecha.Value;
		cajachica.Tipodocumento = CodDocumento;
		cajachica.CodSerie = CodSerie;
		cajachica.Serie = txtSerie.Text;
		cajachica.NumDocumento1 = txtNumero.Text.Trim();
		cajachica.Toneladas = 0m;
		cajachica.CodTipoPagoCaja = 5;
		cajachica.Estado = 1;
		cajachica.CodUser = frmLogin.iCodUser;
		cajachica.Codalmacen = frmLogin.iCodAlmacen;
		cajachica.Codmoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		cajachica.Tcventa = Convert.ToDecimal(txtTipoCambio.Text);
		if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
		{
			cajachica.Monto *= cajachica.Tcventa;
		}
		if (Proceso == 1)
		{
			if (admcajachica.InsertMovCajaChica(cajachica))
			{
				codrecibo = cajachica.CodMovCaja;
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Recibos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else if (Proceso != 2)
		{
		}
		SoloLectura(estado: true);
		Proceso = 0;
		btnReporte2.Visible = true;
		btnReporte2.Enabled = true;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
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

	private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
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

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private bool BuscaSerie()
	{
		ser = Admser.BuscaSeriexDocumento(CodDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private bool BuscaSerie2()
	{
		ser = Admser.MuestraSerie(CodSerie, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void txtNumeracion_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.enteros(e);
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtMonto_KeyUp(object sender, KeyEventArgs e)
	{
		if (txtMonto.Text != "")
		{
			decimal Monto = Convert.ToDecimal(txtMonto.Text.Trim()) + Convert.ToDecimal(lblSaldoCaja.Text.Trim());
			if (Convert.ToDecimal(txtMonto.Text.Trim()) > Monto)
			{
				MessageBox.Show("Saldo Insuficiente en Caja Chica", "Gestion de Caja Chica", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtMonto.Text = Monto.ToString();
				txtMonto.SelectAll();
			}
		}
	}

	private void cmbTipo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbTipo.Text == "INGRESO")
		{
			txtDocRef.Text = "RCHI";
			doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
			CodDocumento = doc.CodTipoDocumento;
		}
		else if (cmbTipo.Text == "EGRESO")
		{
			txtDocRef.Text = "RCHE";
			doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
			CodDocumento = doc.CodTipoDocumento;
		}
	}

	private void carga(int tipo)
	{
	}

	private void btnReporte2_Click(object sender, EventArgs e)
	{
		try
		{
			if (cmbTipo.Text == "INGRESO")
			{
				clsReporteCaja dso = new clsReporteCaja();
				CRRecibodeEgresosCajaChica rpt = new CRRecibodeEgresosCajaChica();
				frmReporteReciboCajaChicaRPT frm = new frmReporteReciboCajaChicaRPT();
				rpt.SetDataSource(dso.ReciboCajaChica(1, codrecibo));
				frm.crvReciboCajaChica.ReportSource = rpt;
				frm.Show();
			}
			else if (cmbTipo.Text == "EGRESO")
			{
				clsReporteCaja dso2 = new clsReporteCaja();
				CRReciboEgresosMovilidadAgazajosFestividades rpt2 = new CRReciboEgresosMovilidadAgazajosFestividades();
				frmReporteReciboCajaChicaRPT frm2 = new frmReporteReciboCajaChicaRPT();
				rpt2.SetDataSource(dso2.ReciboCajaChica(2, codrecibo));
				frm2.crvReciboCajaChica.ReportSource = rpt2;
				frm2.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se presento el siguiente error" + ex.ToString(), "Cierre", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Close();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 3;
		form.Procedencia = 1;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtDocRef.Text = doc.Sigla;
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
		}
		else
		{
			txtDocRef.Text = "";
		}
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmSerie"] != null)
		{
			Application.OpenForms["frmSerie"].Activate();
			return;
		}
		frmSerie form = new frmSerie();
		form.DocSeleccionado = CodDocumento;
		form.Sigla = txtDocRef.Text;
		form.Proceso = 3;
		form.ShowDialog();
		ser = form.ser;
		CodSerie = ser.CodSerie;
		txtSerie.Text = ser.Serie;
		txtNumero.Text = ser.Numeracion.ToString().PadLeft(9, '0');
		txtdoc.Text = txtDocRef.Text + " " + ser.Serie + " - " + ser.Numeracion.ToString().PadLeft(9, '0');
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
			if (txtSerie.Text == "")
			{
				txtSerie.Focus();
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRecibos_CajaChica));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnReporte2 = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtMonto = new System.Windows.Forms.TextBox();
		this.compareValidator1 = new DevComponents.DotNetBar.Validator.CompareValidator();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.label4 = new System.Windows.Forms.Label();
		this.compareValidator2 = new DevComponents.DotNetBar.Validator.CompareValidator();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.lblSaldoCaja = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.cmbtipopagoser = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbTipo = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtdoc = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txtDni = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox5.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.groupBox3.Controls.Add(this.btnReporte2);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 297);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(538, 57);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.btnReporte2.ImageIndex = 3;
		this.btnReporte2.ImageList = this.imageList1;
		this.btnReporte2.Location = new System.Drawing.Point(291, 13);
		this.btnReporte2.Name = "btnReporte2";
		this.btnReporte2.Size = new System.Drawing.Size(78, 32);
		this.btnReporte2.TabIndex = 60;
		this.btnReporte2.Text = "Reporte";
		this.btnReporte2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte2.UseVisualStyleBackColor = true;
		this.btnReporte2.Visible = false;
		this.btnReporte2.Click += new System.EventHandler(btnReporte2_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(458, 13);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(375, 13);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 0;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtDescripcion.BackColor = System.Drawing.Color.LightBlue;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(92, 243);
		this.txtDescripcion.MaxLength = 100;
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(434, 48);
		this.txtDescripcion.TabIndex = 0;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator4);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(21, 257);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 19;
		this.label2.Text = "Descripcion:";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(401, 87);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(101, 20);
		this.dtpFecha.TabIndex = 3;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(353, 90);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 13);
		this.label3.TabIndex = 21;
		this.label3.Text = "Fecha:";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator4.ErrorMessage = "Ingrese Concepto";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.txtMonto.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtMonto.BackColor = System.Drawing.Color.LightBlue;
		this.txtMonto.Location = new System.Drawing.Point(92, 217);
		this.txtMonto.MaxLength = 10;
		this.txtMonto.Name = "txtMonto";
		this.txtMonto.Size = new System.Drawing.Size(118, 20);
		this.txtMonto.TabIndex = 1;
		this.txtMonto.Text = "0.00";
		this.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtMonto, this.compareValidator1);
		this.txtMonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMonto_KeyPress);
		this.txtMonto.KeyUp += new System.Windows.Forms.KeyEventHandler(txtMonto_KeyUp);
		this.compareValidator1.ErrorMessage = "Your error message here.";
		this.compareValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.compareValidator1.Operator = DevComponents.DotNetBar.Validator.eValidationCompareOperator.GreaterThan;
		this.compareValidator1.ValueToCompare = "0";
		this.txtTipoCambio.Location = new System.Drawing.Point(281, 87);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(66, 20);
		this.txtTipoCambio.TabIndex = 97;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtTipoCambio, this.customValidator2);
		this.customValidator2.ErrorMessage = "Ingrese Serie.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator3.ErrorMessage = "Numeracion";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese Nume de Documento";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(21, 221);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 13);
		this.label4.TabIndex = 25;
		this.label4.Text = "Monto:";
		this.compareValidator2.ErrorMessage = "Ingrese N° de Toneladas";
		this.compareValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.compareValidator2.Operator = DevComponents.DotNetBar.Validator.eValidationCompareOperator.GreaterThan;
		this.groupBox5.Controls.Add(this.txtDocRef);
		this.groupBox5.Controls.Add(this.txtNumero);
		this.groupBox5.Controls.Add(this.txtSerie);
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.cmbMoneda);
		this.groupBox5.Controls.Add(this.txtTipoCambio);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.lblSaldoCaja);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.cmbtipopagoser);
		this.groupBox5.Controls.Add(this.label1);
		this.groupBox5.Controls.Add(this.txtMonto);
		this.groupBox5.Controls.Add(this.cmbTipo);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.label8);
		this.groupBox5.Controls.Add(this.txtDescripcion);
		this.groupBox5.Controls.Add(this.label2);
		this.groupBox5.Controls.Add(this.txtdoc);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Controls.Add(this.label15);
		this.groupBox5.Controls.Add(this.dtpFecha);
		this.groupBox5.Controls.Add(this.label3);
		this.groupBox5.Controls.Add(this.txtDni);
		this.groupBox5.Controls.Add(this.label17);
		this.groupBox5.Controls.Add(this.txtNombre);
		this.groupBox5.Location = new System.Drawing.Point(0, -1);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(534, 299);
		this.groupBox5.TabIndex = 4;
		this.groupBox5.TabStop = false;
		this.txtDocRef.Location = new System.Drawing.Point(92, 58);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(41, 20);
		this.txtDocRef.TabIndex = 93;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtNumero.Location = new System.Drawing.Point(197, 58);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 95;
		this.txtSerie.Location = new System.Drawing.Point(139, 58);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(51, 20);
		this.txtSerie.TabIndex = 94;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(21, 65);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(34, 13);
		this.label6.TabIndex = 100;
		this.label6.Text = "Serie:";
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(92, 87);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(139, 20);
		this.cmbMoneda.TabIndex = 96;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(242, 90);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(27, 13);
		this.label7.TabIndex = 99;
		this.label7.Text = "T.C.";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(21, 89);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(46, 13);
		this.label9.TabIndex = 98;
		this.label9.Text = "Moneda";
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.Color.RoyalBlue;
		this.lblSaldoCaja.Location = new System.Drawing.Point(281, 14);
		this.lblSaldoCaja.Name = "lblSaldoCaja";
		this.lblSaldoCaja.Size = new System.Drawing.Size(157, 20);
		this.lblSaldoCaja.TabIndex = 40;
		this.lblSaldoCaja.Text = "0.000";
		this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(141, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(136, 15);
		this.label5.TabIndex = 39;
		this.label5.Text = "SALDO EN CAJA S/.:";
		this.cmbtipopagoser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbtipopagoser.Enabled = false;
		this.cmbtipopagoser.FormattingEnabled = true;
		this.cmbtipopagoser.Location = new System.Drawing.Point(270, 178);
		this.cmbtipopagoser.Name = "cmbtipopagoser";
		this.cmbtipopagoser.Size = new System.Drawing.Size(232, 21);
		this.cmbtipopagoser.TabIndex = 37;
		this.cmbtipopagoser.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(244, 186);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 13);
		this.label1.TabIndex = 38;
		this.label1.Text = "Tipo Pago:";
		this.label1.Visible = false;
		this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipo.FormattingEnabled = true;
		this.cmbTipo.Items.AddRange(new object[2] { "INGRESO", "EGRESO" });
		this.cmbTipo.Location = new System.Drawing.Point(92, 183);
		this.cmbTipo.Name = "cmbTipo";
		this.cmbTipo.Size = new System.Drawing.Size(139, 21);
		this.cmbTipo.TabIndex = 36;
		this.cmbTipo.SelectionChangeCommitted += new System.EventHandler(cmbTipo_SelectionChangeCommitted);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(21, 186);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(31, 13);
		this.label8.TabIndex = 35;
		this.label8.Text = "Tipo:";
		this.txtdoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtdoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtdoc.ForeColor = System.Drawing.Color.Red;
		this.txtdoc.Location = new System.Drawing.Point(367, 58);
		this.txtdoc.MaxLength = 50;
		this.txtdoc.Name = "txtdoc";
		this.txtdoc.Size = new System.Drawing.Size(135, 20);
		this.txtdoc.TabIndex = 34;
		this.txtdoc.Text = ".";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(278, 61);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(83, 13);
		this.label18.TabIndex = 33;
		this.label18.Text = "N° Documento :";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(287, 217);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(26, 13);
		this.label15.TabIndex = 32;
		this.label15.Text = "Dni:";
		this.txtDni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtDni.Location = new System.Drawing.Point(341, 214);
		this.txtDni.MaxLength = 8;
		this.txtDni.Name = "txtDni";
		this.txtDni.Size = new System.Drawing.Size(139, 20);
		this.txtDni.TabIndex = 31;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(21, 122);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(47, 13);
		this.label17.TabIndex = 28;
		this.label17.Text = "Nombre:";
		this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(92, 118);
		this.txtNombre.Multiline = true;
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(410, 37);
		this.txtNombre.TabIndex = 27;
		base.AcceptButton = this.btnGuardar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(538, 354);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmRecibos_CajaChica";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " ";
		base.Load += new System.EventHandler(frmRecibos_CajaChica_Load);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		base.ResumeLayout(false);
	}
}
