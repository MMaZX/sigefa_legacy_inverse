using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Alertas;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SISTEMA_BUNIFU.Alertas;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCajaDiariaRegistro : RadForm
{
	private clsAdmMoneda AdmMoned = new clsAdmMoneda();

	private clsAdmBanco AdmBan = new clsAdmBanco();

	private clsAdmTarjetaPago AdmTar = new clsAdmTarjetaPago();

	private clsValidar val = new clsValidar();

	private clsAdmCtaCte AdmCtaCte = new clsAdmCtaCte();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmMetodoPago admMPago = new clsAdmMetodoPago();

	private AdmIngresosEgresos IngresoEgreso = new AdmIngresosEgresos();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private Info info;

	private Errors error;

	private Success success;

	public int direccioncaja = 0;

	public bool Tipo;

	public int codigocaja = 0;

	public int Proceso = 0;

	public decimal SaldoCaja = default(decimal);

	public int CodDocumento;

	public int CodSerie;

	public DateTime fechaRegistro = DateTime.Now;

	public int opcionSuma;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnGuardar;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private SuperValidator superValidator2;

	private ErrorProvider errorProvider2;

	private Highlighter highlighter2;

	private SuperValidator superValidator3;

	private ErrorProvider errorProvider3;

	private Highlighter highlighter3;

	private CustomValidator customValidator1;

	private CompareValidator compareValidator2;

	private CompareValidator compareValidator1;

	private Label lblTipo;

	public ComboBox cboTipo;

	private Label label8;

	public TextBox txtDocumento;

	public Label lblSaldoCaja;

	private Label label5;

	private GroupBox groupBox2;

	private TextBox txtDocRef;

	private TextBox txtNumero;

	public TextBox txtSerie;

	private Label label16;

	private TextBox txtMonedaCta;

	private Label label14;

	public ComboBox cboNumCta;

	private Label label13;

	public ComboBox cboBanco;

	private Label label12;

	public ComboBox cboTarjeta;

	public DateTimePicker dtpfecha;

	private Label label10;

	public TextBox txtMontoPago;

	private Label label9;

	private TextBox txtOperacion;

	private Label label17;

	public ComboBox cmbMetodoPago;

	public ComboBox cmbMoneda;

	private Label label18;

	public TextBox txtTipoCambio;

	private Label label19;

	private Label label20;

	private GroupBox groupBox1;

	private TextBox txtNc;

	private Label label15;

	private TextBox txtCheque;

	private Label label1;

	public TextBox txtDescripcion;

	private Label label2;

	public ComboBox cmbMotivo;

	private Label label3;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	public ComboBox cmbAlmacenes;

	private Label label4;

	public frmCajaDiariaRegistro()
	{
		InitializeComponent();
	}

	private void frmCajaChicaRegistro_Load(object sender, EventArgs e)
	{
		cargaMoneda();
		CargarBancos();
		CargarTarjetas();
		cboTarjeta.SelectedIndex = -1;
		cboBanco.SelectedIndex = -1;
		CargaTipoCambio();
		CargaMetodosPagos();
		cmbMetodoPago_SelectionChangeCommitted(cmbMetodoPago, null);
		txtDocRef.Visible = true;
		txtSerie.Visible = true;
		txtNumero.Visible = true;
		txtNumero.Enabled = false;
		label16.Visible = true;
		if (Tipo)
		{
			cboTipo.SelectedIndex = 0;
			CargaIngresoEgreso(1);
		}
		else
		{
			cboTipo.SelectedIndex = 1;
			CargaIngresoEgreso(2);
			cmbMetodoPago.Enabled = false;
		}
		cargaAlmacenes();
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre2(frmLogin.iCodAlmacen);
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void CargaMetodosPagos()
	{
		cmbMetodoPago.DataSource = admMPago.CargaMetodoPagosIE();
		cmbMetodoPago.DisplayMember = "descripcion";
		cmbMetodoPago.ValueMember = "codMetodoPago";
	}

	private void CargaIngresoEgreso(int tipo)
	{
		cmbMotivo.DataSource = IngresoEgreso.ListadoIngresosEgresos(tipo);
		cmbMotivo.DisplayMember = "descripcion";
		cmbMotivo.ValueMember = "id";
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

	private void CargarTarjetas()
	{
		try
		{
			cboTarjeta.DataSource = AdmTar.MuestraTarjetas(frmLogin.iCodAlmacen);
			cboTarjeta.DisplayMember = "tipo";
			cboTarjeta.ValueMember = "codtarjeta";
			cboTarjeta.SelectedIndex = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void CargarBancos()
	{
		try
		{
			cboBanco.DataSource = AdmCtaCte.ListaBancoxMoneda(Convert.ToInt32(cmbMoneda.SelectedValue));
			cboBanco.DisplayMember = "descripcion";
			cboBanco.ValueMember = "codbanco";
			if (cboBanco.Items.Count > 0)
			{
				cboBanco.SelectedIndex = 0;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToDateTime(fechaRegistro.ToString("yyyy/MM/dd")) < Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd")))
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro de realizar ingreso con fecha anterior?", "Caja Chica", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No)
				{
					if (Caja.Estado)
					{
						setDatosPago();
					}
					else
					{
						error = new Errors("No se puede realizar ingreso a unsa caja cerrada!");
					}
				}
			}
			else
			{
				setDatosPago();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void setDatosPago()
	{
		if (!Tipo && Convert.ToDouble(txtMontoPago.Text) > Convert.ToDouble(lblSaldoCaja.Text))
		{
			info = new Info("Egreso no puede ser mayor al monto actual en caja!");
			info.ShowDialog();
			txtMontoPago.Focus();
			return;
		}
		if (txtDocRef.Text == "")
		{
			info = new Info("Presione F1 para seleccionar Doc. Referencia!");
			info.ShowDialog();
			txtDocRef.Focus();
			return;
		}
		if (txtSerie.Text == "")
		{
			info = new Info("Ingrese serie al Doc. Referencia!");
			info.ShowDialog();
			txtSerie.Focus();
			return;
		}
		if (txtMontoPago.Text == "")
		{
			info = new Info("Ingrese monto!");
			info.ShowDialog();
			txtMontoPago.Focus();
			return;
		}
		Pag.TipoDescripcion = Convert.ToInt32(cmbMotivo.SelectedValue);
		Pag.OpcionSuma = opcionSuma;
		Pag.CodNota = null;
		Pag.CodLetra = 0;
		Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue);
		Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		Pag.CodCobrador = Convert.ToInt32(frmLogin.iCodUser);
		Pag.Tipo = true;
		Pag.IngresoEgreso = Tipo;
		if (txtTipoCambio.Text == "")
		{
			Pag.TipoCambio = 0m;
		}
		else
		{
			Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
		}
		Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
		Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
		Pag.Vuelto = 0m;
		Pag.codCtaCte = Convert.ToInt32(cboNumCta.SelectedValue);
		Pag.CtaCte = Convert.ToString(cboNumCta.Text);
		Pag.NOperacion = txtOperacion.Text;
		Pag.NCheque = txtCheque.Text;
		Pag.FechaPago = fechaRegistro;
		Pag.Observacion = txtDescripcion.Text;
		Pag.CodUser = frmLogin.iCodUser;
		Pag.CodAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
		Pag.CodSerie = CodSerie;
		Pag.CodSucursal = frmLogin.iCodSucursal;
		Pag.CodDoc = CodDocumento;
		Pag.Codcaja = codigocaja;
		Pag.Serie = txtSerie.Text;
		Pag.NumDoc = txtNumero.Text;
		Pag.Referencia = txtNc.Text;
		Pag.CodBanco = Convert.ToInt32(cboBanco.SelectedValue);
		Pag.NOperacion = Convert.ToString(txtOperacion.Text.Trim());
		Pag.CodTarjeta = Convert.ToInt32(cboTarjeta.SelectedValue);
		Pag.NCheque = Convert.ToString(txtCheque.Text.Trim());
		if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
		{
			if (txtOperacion.Text.Trim() == "" || cboBanco.Text == "" || txtMontoPago.Text == "")
			{
				info = new Info("Ingresar Datos Necesarios");
				info.ShowDialog();
			}
			else
			{
				Pagar();
			}
		}
		else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
		{
			if (txtOperacion.Text.Trim() == "" || cboTarjeta.Text == "" || txtMontoPago.Text == "")
			{
				info = new Info("Ingresar Datos Necesarios");
				info.ShowDialog();
			}
			else
			{
				Pagar();
			}
		}
		else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
		{
			if (txtCheque.Text.Trim() == "" || cboBanco.Text == "" || txtOperacion.Text.Trim() == "" || txtMontoPago.Text == "")
			{
				info = new Info("Ingresar Datos Necesarios");
				info.ShowDialog();
			}
			else
			{
				Pagar();
			}
		}
		else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 5 && Admpag.insert(Pag))
		{
			success = new Success("Los datos se guardaron correctamente");
			success.ShowDialog();
			Deshabilita_botones(Estado: false);
		}
	}

	private void Pagar()
	{
		if (Admpag.insert(Pag))
		{
			success = new Success("Pago Realizado Correctamente");
			success.ShowDialog();
			cargaPago(Pag);
			Deshabilita_botones(Estado: false);
		}
	}

	private void cargaPago(clsPago p)
	{
		p = Admpag.MuestraPagoVenta(frmLogin.iCodAlmacen, Pag.CodPago);
		if (p != null)
		{
			cmbMetodoPago.SelectedValue = p.CodTipoPago;
			cboBanco.SelectedValue = p.CodBanco;
			cboTarjeta.SelectedValue = p.CodTarjeta;
			cboNumCta.SelectedValue = p.codCtaCte;
			txtTipoCambio.Text = p.TipoCambio.ToString();
			txtCheque.Text = p.NCheque;
			txtDescripcion.Text = p.Observacion;
			txtOperacion.Text = p.NOperacion;
			txtMontoPago.Text = p.MontoCobrado.ToString();
			dtpfecha.Value = p.FechaPago;
			txtSerie.Text = p.Serie;
			txtNumero.Text = p.NumDoc;
		}
	}

	private void Deshabilita_botones(bool Estado)
	{
		cboBanco.Enabled = Estado;
		cboNumCta.Enabled = Estado;
		cboTarjeta.Enabled = Estado;
		txtCheque.Enabled = Estado;
		txtOperacion.Enabled = Estado;
		txtDescripcion.Enabled = Estado;
		txtMontoPago.Enabled = Estado;
		dtpfecha.Enabled = Estado;
		btnGuardar.Enabled = Estado;
		btnSalir.Enabled = !Estado;
		txtSerie.Enabled = Estado;
		txtNumero.Enabled = Estado;
		txtNumero.Visible = !Estado;
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

	private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtGuiaRemision_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumerosDoc(sender, e);
	}

	private void txtReciboLiquidacion_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumerosDoc(sender, e);
	}

	private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.SOLONumeros(sender, e);
	}

	private void cmbMetodoPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 5)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = false;
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtCheque.Text = "";
				txtNc.Text = "";
				txtMontoPago.Text = "";
				txtOperacion.Text = "";
				txtOperacion.Enabled = false;
				txtCheque.Enabled = false;
				txtMontoPago.Enabled = true;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = true;
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				cboBanco.Focus();
				txtCheque.Text = "";
				txtOperacion.Text = "";
				txtNc.Text = "";
				txtMontoPago.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = false;
				txtMontoPago.Enabled = true;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = true;
				cboBanco.Focus();
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtNc.Text = "";
				txtMontoPago.Text = "";
				txtCheque.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = true;
				txtMontoPago.Enabled = true;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
			{
				cboTarjeta.Enabled = true;
				cboBanco.Enabled = false;
				cboTarjeta.Focus();
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtNc.Text = "";
				txtMontoPago.Text = "";
				txtCheque.Text = "";
				txtOperacion.Enabled = true;
				txtCheque.Enabled = false;
				txtMontoPago.Enabled = true;
				cboNumCta.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
			else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 10)
			{
				cboTarjeta.Enabled = false;
				cboBanco.Enabled = false;
				cboBanco.SelectedIndex = -1;
				cboTarjeta.SelectedIndex = -1;
				txtOperacion.Text = "";
				txtCheque.Text = "";
				txtNc.Text = "";
				txtMontoPago.Text = "";
				txtOperacion.Enabled = false;
				txtCheque.Enabled = false;
				txtNc.Enabled = false;
				cboNumCta.Enabled = false;
				txtMontoPago.Enabled = false;
				cboNumCta.SelectedIndex = -1;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cboBanco_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
			{
				cboNumCta.Enabled = true;
				CargaCtaCte();
			}
			else
			{
				cboNumCta.SelectedIndex = -1;
				cboNumCta.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void CargaCtaCte()
	{
		try
		{
			cboNumCta.DataSource = AdmCtaCte.ListaCtaCtexBancoxMoneda(Convert.ToInt32(cboBanco.SelectedValue), Convert.ToInt32(cmbMoneda.SelectedValue));
			cboNumCta.DisplayMember = "cuentaCorriente";
			cboNumCta.ValueMember = "codCuentaCorriente";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
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
		form.direccioncaja = direccioncaja;
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
		txtDocumento.Text = txtDocRef.Text + " " + ser.Serie + " - " + ser.Numeracion.ToString().PadLeft(9, '0');
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
			if (txtSerie.Text == "")
			{
				txtSerie.Focus();
			}
		}
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, fechaRegistro, 1, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodUser);
			codigocaja = Caja.Codcaja;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaDiariaRegistro));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtMontoPago = new System.Windows.Forms.TextBox();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.compareValidator1 = new DevComponents.DotNetBar.Validator.CompareValidator();
		this.superValidator2 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter2 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtDocumento = new System.Windows.Forms.TextBox();
		this.superValidator3 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter3 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.compareValidator2 = new DevComponents.DotNetBar.Validator.CompareValidator();
		this.lblSaldoCaja = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.lblTipo = new System.Windows.Forms.Label();
		this.cboTipo = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtNc = new System.Windows.Forms.TextBox();
		this.cmbMotivo = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.cboTarjeta = new System.Windows.Forms.ComboBox();
		this.txtCheque = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.txtOperacion = new System.Windows.Forms.TextBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cboBanco = new System.Windows.Forms.ComboBox();
		this.dtpfecha = new System.Windows.Forms.DateTimePicker();
		this.label13 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.cboNumCta = new System.Windows.Forms.ComboBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtMonedaCta = new System.Windows.Forms.TextBox();
		this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider3).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.BackColor = System.Drawing.Color.White;
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 451);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(394, 63);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Image = SIGEFA.Properties.Resources.exit1;
		this.btnSalir.Location = new System.Drawing.Point(251, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(93, 41);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.BackColor = System.Drawing.Color.White;
		this.btnGuardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnGuardar.Location = new System.Drawing.Point(152, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(93, 41);
		this.btnGuardar.TabIndex = 0;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = false;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.txtMontoPago.BackColor = System.Drawing.Color.White;
		this.txtMontoPago.Location = new System.Drawing.Point(94, 188);
		this.txtMontoPago.Name = "txtMontoPago";
		this.txtMontoPago.Size = new System.Drawing.Size(118, 20);
		this.txtMontoPago.TabIndex = 14;
		this.txtMontoPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtMontoPago, this.customValidator1);
		this.customValidator1.ErrorMessage = "Ingrese Nume de Documento";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.compareValidator1.ErrorMessage = "Your error message here.";
		this.compareValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.compareValidator1.Operator = DevComponents.DotNetBar.Validator.eValidationCompareOperator.GreaterThan;
		this.compareValidator1.ValueToCompare = "0";
		this.superValidator2.ContainerControl = this;
		this.superValidator2.ErrorProvider = this.errorProvider2;
		this.superValidator2.Highlighter = this.highlighter2;
		this.errorProvider2.ContainerControl = this;
		this.errorProvider2.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider2.Icon");
		this.highlighter2.ContainerControl = this;
		this.txtDocumento.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtDocumento.BackColor = System.Drawing.Color.White;
		this.txtDocumento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocumento.Location = new System.Drawing.Point(190, 33);
		this.txtDocumento.MaxLength = 11;
		this.txtDocumento.Name = "txtDocumento";
		this.txtDocumento.Size = new System.Drawing.Size(180, 21);
		this.txtDocumento.TabIndex = 42;
		this.txtDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.superValidator2.SetValidator1(this.txtDocumento, this.customValidator1);
		this.superValidator3.ContainerControl = this;
		this.superValidator3.ErrorProvider = this.errorProvider3;
		this.superValidator3.Highlighter = this.highlighter3;
		this.errorProvider3.ContainerControl = this;
		this.errorProvider3.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider3.Icon");
		this.highlighter3.ContainerControl = this;
		this.compareValidator2.ErrorMessage = "Ingrese N° de Toneladas";
		this.compareValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.compareValidator2.Operator = DevComponents.DotNetBar.Validator.eValidationCompareOperator.GreaterThan;
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.Color.Gray;
		this.lblSaldoCaja.Location = new System.Drawing.Point(33, 33);
		this.lblSaldoCaja.Name = "lblSaldoCaja";
		this.lblSaldoCaja.Size = new System.Drawing.Size(98, 20);
		this.lblSaldoCaja.TabIndex = 35;
		this.lblSaldoCaja.Text = "0.000";
		this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(30, 17);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(79, 13);
		this.label5.TabIndex = 34;
		this.label5.Text = "Saldo Caja S/.:";
		this.lblTipo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTipo.AutoSize = true;
		this.lblTipo.Location = new System.Drawing.Point(222, 44);
		this.lblTipo.Name = "lblTipo";
		this.lblTipo.Size = new System.Drawing.Size(31, 13);
		this.lblTipo.TabIndex = 56;
		this.lblTipo.Text = "Tipo:";
		this.cboTipo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboTipo.Enabled = false;
		this.cboTipo.FormattingEnabled = true;
		this.cboTipo.Items.AddRange(new object[2] { "INGRESO", "EGRESO" });
		this.cboTipo.Location = new System.Drawing.Point(259, 41);
		this.cboTipo.Name = "cboTipo";
		this.cboTipo.Size = new System.Drawing.Size(101, 21);
		this.cboTipo.TabIndex = 47;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(190, 17);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(80, 13);
		this.label8.TabIndex = 54;
		this.label8.Text = "N° Documento:";
		this.groupBox2.Controls.Add(this.cmbAlmacenes);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.txtNc);
		this.groupBox2.Controls.Add(this.cmbMotivo);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.cboTipo);
		this.groupBox2.Controls.Add(this.lblTipo);
		this.groupBox2.Controls.Add(this.txtDocRef);
		this.groupBox2.Controls.Add(this.cboTarjeta);
		this.groupBox2.Controls.Add(this.txtCheque);
		this.groupBox2.Controls.Add(this.txtNumero);
		this.groupBox2.Controls.Add(this.label17);
		this.groupBox2.Controls.Add(this.txtSerie);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.label16);
		this.groupBox2.Controls.Add(this.txtOperacion);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.cboBanco);
		this.groupBox2.Controls.Add(this.dtpfecha);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.cboNumCta);
		this.groupBox2.Controls.Add(this.txtMontoPago);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.txtMonedaCta);
		this.groupBox2.Controls.Add(this.cmbMetodoPago);
		this.groupBox2.Controls.Add(this.cmbMoneda);
		this.groupBox2.Controls.Add(this.label18);
		this.groupBox2.Controls.Add(this.txtTipoCambio);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.label20);
		this.groupBox2.Location = new System.Drawing.Point(5, 76);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(385, 379);
		this.groupBox2.TabIndex = 59;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Datos de Pago";
		this.cmbAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbAlmacenes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbAlmacenes.BackColor = System.Drawing.Color.White;
		this.cmbAlmacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(94, 94);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(266, 20);
		this.cmbAlmacenes.TabIndex = 106;
		this.cmbAlmacenes.SelectedIndexChanged += new System.EventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(18, 96);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(48, 13);
		this.label4.TabIndex = 107;
		this.label4.Text = "Almacen";
		this.txtNc.Enabled = false;
		this.txtNc.Location = new System.Drawing.Point(96, 350);
		this.txtNc.Name = "txtNc";
		this.txtNc.Size = new System.Drawing.Size(264, 20);
		this.txtNc.TabIndex = 103;
		this.cmbMotivo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbMotivo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbMotivo.BackColor = System.Drawing.Color.White;
		this.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Location = new System.Drawing.Point(94, 116);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(266, 20);
		this.cmbMotivo.TabIndex = 93;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(18, 118);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(39, 13);
		this.label3.TabIndex = 94;
		this.label3.Text = "Motivo";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(21, 357);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(69, 13);
		this.label15.TabIndex = 105;
		this.label15.Text = "N° N. Credito";
		this.txtDocRef.BackColor = System.Drawing.Color.SkyBlue;
		this.txtDocRef.Location = new System.Drawing.Point(94, 15);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(29, 20);
		this.txtDocRef.TabIndex = 1;
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.cboTarjeta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboTarjeta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboTarjeta.FormattingEnabled = true;
		this.cboTarjeta.Location = new System.Drawing.Point(94, 216);
		this.cboTarjeta.Name = "cboTarjeta";
		this.cboTarjeta.Size = new System.Drawing.Size(266, 21);
		this.cboTarjeta.TabIndex = 7;
		this.txtCheque.Location = new System.Drawing.Point(95, 322);
		this.txtCheque.Name = "txtCheque";
		this.txtCheque.Size = new System.Drawing.Size(265, 20);
		this.txtCheque.TabIndex = 102;
		this.txtNumero.Location = new System.Drawing.Point(175, 15);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 3;
		this.txtNumero.Visible = false;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(18, 299);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(71, 13);
		this.label17.TabIndex = 29;
		this.label17.Text = "N° Operación";
		this.txtSerie.BackColor = System.Drawing.Color.SkyBlue;
		this.txtSerie.Location = new System.Drawing.Point(127, 15);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(43, 20);
		this.txtSerie.TabIndex = 2;
		this.txtSerie.Visible = false;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(19, 325);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 13);
		this.label1.TabIndex = 104;
		this.label1.Text = "N° Cheque";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(19, 18);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(34, 13);
		this.label16.TabIndex = 92;
		this.label16.Text = "Serie:";
		this.label16.Visible = false;
		this.txtOperacion.Location = new System.Drawing.Point(94, 296);
		this.txtOperacion.Name = "txtOperacion";
		this.txtOperacion.Size = new System.Drawing.Size(266, 20);
		this.txtOperacion.TabIndex = 11;
		this.txtDescripcion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtDescripcion.BackColor = System.Drawing.Color.White;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(94, 140);
		this.txtDescripcion.MaxLength = 100;
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(266, 40);
		this.txtDescripcion.TabIndex = 41;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(18, 219);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(43, 13);
		this.label12.TabIndex = 78;
		this.label12.Text = "Tarjeta:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(22, 146);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 49;
		this.label2.Text = "Descripcion:";
		this.cboBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboBanco.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboBanco.FormattingEnabled = true;
		this.cboBanco.Location = new System.Drawing.Point(94, 242);
		this.cboBanco.Name = "cboBanco";
		this.cboBanco.Size = new System.Drawing.Size(266, 21);
		this.cboBanco.TabIndex = 8;
		this.cboBanco.SelectionChangeCommitted += new System.EventHandler(cboBanco_SelectionChangeCommitted);
		this.dtpfecha.Checked = false;
		this.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpfecha.Location = new System.Drawing.Point(267, 190);
		this.dtpfecha.Name = "dtpfecha";
		this.dtpfecha.Size = new System.Drawing.Size(93, 20);
		this.dtpfecha.TabIndex = 16;
		this.dtpfecha.Visible = false;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(18, 245);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(41, 13);
		this.label13.TabIndex = 80;
		this.label13.Text = "Banco:";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(221, 193);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(37, 13);
		this.label10.TabIndex = 35;
		this.label10.Text = "Fecha";
		this.label10.Visible = false;
		this.cboNumCta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cboNumCta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cboNumCta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cboNumCta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cboNumCta.FormattingEnabled = true;
		this.cboNumCta.Location = new System.Drawing.Point(94, 269);
		this.cboNumCta.Name = "cboNumCta";
		this.cboNumCta.Size = new System.Drawing.Size(170, 21);
		this.cboNumCta.TabIndex = 9;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(18, 273);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(41, 13);
		this.label14.TabIndex = 82;
		this.label14.Text = "N° Cta:";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(18, 191);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(37, 13);
		this.label9.TabIndex = 33;
		this.label9.Text = "Monto";
		this.txtMonedaCta.Location = new System.Drawing.Point(270, 270);
		this.txtMonedaCta.Name = "txtMonedaCta";
		this.txtMonedaCta.ReadOnly = true;
		this.txtMonedaCta.Size = new System.Drawing.Size(90, 20);
		this.txtMonedaCta.TabIndex = 10;
		this.txtMonedaCta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbMetodoPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
		this.cmbMetodoPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMetodoPago.FormattingEnabled = true;
		this.cmbMetodoPago.Location = new System.Drawing.Point(94, 68);
		this.cmbMetodoPago.Name = "cmbMetodoPago";
		this.cmbMetodoPago.Size = new System.Drawing.Size(266, 20);
		this.cmbMetodoPago.TabIndex = 6;
		this.cmbMetodoPago.SelectionChangeCommitted += new System.EventHandler(cmbMetodoPago_SelectionChangeCommitted);
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(94, 41);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(106, 20);
		this.cmbMoneda.TabIndex = 4;
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(18, 70);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(70, 13);
		this.label18.TabIndex = 26;
		this.label18.Text = "Tipo de pago";
		this.txtTipoCambio.Location = new System.Drawing.Point(284, 15);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(76, 20);
		this.txtTipoCambio.TabIndex = 5;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(251, 18);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(27, 13);
		this.label19.TabIndex = 24;
		this.label19.Text = "T.C.";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(18, 43);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(46, 13);
		this.label20.TabIndex = 23;
		this.label20.Text = "Moneda";
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.lblSaldoCaja);
		this.groupBox1.Controls.Add(this.txtDocumento);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Location = new System.Drawing.Point(5, 8);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(385, 66);
		this.groupBox1.TabIndex = 60;
		this.groupBox1.TabStop = false;
		base.AcceptButton = this.btnGuardar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(394, 514);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCajaDiariaRegistro";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " Ingresos y Egresos de Caja";
		base.ThemeName = "TelerikMetroTouch";
		base.Load += new System.EventHandler(frmCajaChicaRegistro_Load);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider3).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
