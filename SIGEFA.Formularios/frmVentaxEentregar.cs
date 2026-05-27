using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmVentaxEentregar : Office2007Form
{
	private clsReporteFactura ds = new clsReporteFactura();

	private clsReporteFlujoCaja dsf = new clsReporteFlujoCaja();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	private clsAdmPedido Admped = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	private clsAutorizado aut = new clsAutorizado();

	private clsAdmNotaSalida AdmNota = new clsAdmNotaSalida();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsAdmGuiaRemision AdmGuia = new clsAdmGuiaRemision();

	private clsGuiaRemision guia = new clsGuiaRemision();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsListaPrecio Listap = new clsListaPrecio();

	private clsAdmVendedor AdmVen = new clsAdmVendedor();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsFacturaVenta factura = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsMoneda moneda = new clsMoneda();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsAdmListaPrecio admLista = new clsAdmListaPrecio();

	private clsValidar ok = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsCotizacion coti = new clsCotizacion();

	private clsDetalleCotizacion detaCoti = new clsDetalleCotizacion();

	private clsAdmCotizacion AdmCoti = new clsAdmCotizacion();

	private clsAdmAlmacen Admalmac = new clsAdmAlmacen();

	private clsPago Pag = new clsPago();

	private clsAdmPago AdmPagos = new clsAdmPago();

	public List<int> config = new List<int>();

	public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();

	public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>();

	public List<clsDetalleGuiaRemision> detalleg = new List<clsDetalleGuiaRemision>();

	public List<int> documento = new List<int>();

	public List<int> codsalida = new List<int>();

	private List<int> correlativo = new List<int>();

	private List<clsFacturaVenta> ltaventa = new List<clsFacturaVenta>();

	private List<int> codpro = new List<int>();

	private clsFormaPago forma = new clsFormaPago();

	public string CodNota;

	public string CodVenta;

	public int CodTransaccion;

	public int CodProveedor;

	public int CodCliente;

	public int CodDocumento;

	public int CodSerie;

	public int CodSerieG = 0;

	public int numG = 0;

	public int manual = 0;

	public string numSerie;

	public int CodAutorizado;

	public int CodPedido;

	public int CodGuia;

	public int Tipo;

	public int codForma;

	public int codListaP;

	public int Proceso = 0;

	public int Procede = 0;

	public DataTable datoscarga2 = new DataTable();

	public DataTable datos = new DataTable();

	public int tip;

	public int CodVendedor;

	public int CodSalConsulExt;

	public bool consultorext;

	public static BindingSource data = new BindingSource();

	private int CodLista = 0;

	private bool Validacion = true;

	private decimal TipoCambio = default(decimal);

	private decimal ret = default(decimal);

	public int mon = 0;

	public int CodEmpresaTransporte;

	private string Salida = "";

	private int codCotizacion;

	public string CodPago;

	private clsVehiculoTransporte vehiculotransporte = new clsVehiculoTransporte();

	private clsAdmVehiculoTransporte admVehiculoTransporte = new clsAdmVehiculoTransporte();

	private clsConductor conductor = new clsConductor();

	private clsAdmConductor admConductor = new clsAdmConductor();

	private clsAdmEmpresaTransporte AdmET = new clsAdmEmpresaTransporte();

	private clsEmpresaTransporte empT = new clsEmpresaTransporte();

	public int CodigoCaja = 0;

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	public decimal montogratuitas;

	public decimal montogravadas;

	public decimal montoexoneradas;

	public decimal montoinafectas = default(decimal);

	public bool banderadelete = false;

	private List<clsNotaCredito> ncredito = new List<clsNotaCredito>();

	private clsAdmNotaCredito admNotac = new clsAdmNotaCredito();

	public DateTime fecha1;

	public DateTime fecha2;

	public bool xgenerar = false;

	public List<int> listacodigos = new List<int>();

	private clsTipoDocumento doc2 = new clsTipoDocumento();

	private clsSerie seri2 = new clsSerie();

	private string siglaPago;

	private string seriePago;

	private string numeroPago;

	private int codDocumentoPago = 0;

	private bool rpta;

	private IContainer components = null;

	private GroupBox groupBox2;

	private TextBox txtBruto;

	private TextBox txtPrecioVenta;

	private Label label14;

	private TextBox txtIGV;

	private Label label13;

	private TextBox txtDscto;

	private TextBox txtValorVenta;

	private Label label12;

	private Label label11;

	private Label label10;

	private Button btnSalir;

	private ImageList imageList1;

	private GroupBox groupBox1;

	private TextBox txtNombreCliente;

	private Label label15;

	private Label label7;

	private TextBox txtDocRef;

	private Label label5;

	private Label lbNombreTransaccion;

	private Label label2;

	private Label label1;

	private Label label17;

	private TextBox txtSerie;

	public DataGridView dgvDetalle;

	public TextBox txtTransaccion;

	private DateTimePicker dtpFechaPago;

	private Label label3;

	private TextBox txtDsctoGobal;

	private Label label21;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Label label24;

	private ComboBox cbovendedor;

	private Label label8;

	private TextBox txtNumDoc;

	public ComboBox cmbFormaPago;

	public ComboBox cmbMoneda;

	public TextBox txtTipoCambio;

	private TextBox txtCodigoCli;

	public Button btnGuardar;

	public Button btnImprimir;

	private TextBox txtDireccionCliente;

	private Label label16;

	public TextBox txtNumero;

	public DateTimePicker dtpFecha;

	public TextBox txtCodCliente;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator2;

	private CustomValidator customValidator11;

	private CustomValidator customValidator7;

	private CustomValidator customValidator10;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private Label label37;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	public CheckBox chkVentaGratuita;

	private TextBox txtinafectas;

	private Label label26;

	private TextBox txtexoneradas;

	private Label label32;

	private TextBox txtgratuitas;

	private Label label19;

	private TextBox txtgravadas;

	private Label label22;

	public CheckBox chkVentaInafecta;

	public CheckBox chkVentaExonerada;

	public CheckBox chkVentaDsctoGlobal;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codSalida1;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn stockPend;

	private DataGridViewTextBoxColumn dsctoMax;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn tipoarticulo;

	private DataGridViewTextBoxColumn TipoImpuesto;

	private DataGridViewTextBoxColumn CantidadPendiente;

	private DataGridViewTextBoxColumn CantidadEntregada;

	private DataGridViewTextBoxColumn CantidadxSalir;

	private Button btnEliminar;

	public byte[] firmadigital { get; set; }

	private void VentaEnMoneda()
	{
		decimal TipoCambio = default(decimal);
		TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (mon == 1)
			{
				if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
				{
					row.Cells[preciounit.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) / TipoCambio;
					row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / TipoCambio;
					row.Cells[montodscto.Name].Value = Convert.ToDecimal(row.Cells[montodscto.Name].Value) / TipoCambio;
					row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / TipoCambio;
					row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[igv.Name].Value) / TipoCambio;
					row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) / TipoCambio;
				}
			}
			else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
			{
				row.Cells[preciounit.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) * TipoCambio;
				row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) * TipoCambio;
				row.Cells[montodscto.Name].Value = Convert.ToDecimal(row.Cells[montodscto.Name].Value) * TipoCambio;
				row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) * TipoCambio;
				row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[igv.Name].Value) * TipoCambio;
				row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) * TipoCambio;
			}
		}
	}

	public frmVentaxEentregar()
	{
		InitializeComponent();
	}

	public void frmVenta_Load(object sender, EventArgs e)
	{
		iniciaformulario();
	}

	private void txtTransaccion_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtTransaccion.ReadOnly || e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmTransacciones"] != null)
		{
			Application.OpenForms["frmTransacciones"].Activate();
			return;
		}
		frmTransacciones form = new frmTransacciones();
		form.Proceso = 4;
		form.ShowDialog();
		if (CodTransaccion != 0)
		{
			CargaTransaccion();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaTransaccion()
	{
		tran = AdmTran.MuestraTransaccion(CodTransaccion);
		tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
		txtTransaccion.Text = tran.Sigla;
		lbNombreTransaccion.Text = tran.Descripcion;
		lbNombreTransaccion.Visible = true;
		foreach (Control t in groupBox1.Controls)
		{
			if (t.Tag != null && t.Tag != "")
			{
				int con = Convert.ToInt32(t.Tag);
				if (tran.Configuracion.Contains(con))
				{
					t.Visible = true;
				}
				else
				{
					t.Visible = false;
				}
			}
		}
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
		cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
	}

	private void CargaVendedores()
	{
		cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque();
		cbovendedor.DisplayMember = "apellido";
		cbovendedor.ValueMember = "codVendedor";
		cbovendedor.SelectedIndex = 0;
	}

	private void CargaVendedores2()
	{
		cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque2();
		cbovendedor.DisplayMember = "apellido";
		cbovendedor.ValueMember = "codVendedor";
		cbovendedor.SelectedIndex = 0;
	}

	private void recalculadetalle()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[cantidad.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
			row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[cantidad.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
			row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) - Convert.ToDecimal(row.Cells[valorventa.Name].Value);
		}
	}

	private void txtTransaccion_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			txtTransaccion.Focus();
		}
	}

	private void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtTransaccion.Text != "")
		{
			if (BuscaTransaccion())
			{
				ProcessTabKey(forward: false);
			}
			else
			{
				MessageBox.Show("Codigo de transacción no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaTransaccion()
	{
		tran = AdmTran.MuestraTransaccionS(txtTransaccion.Text, 1);
		if (tran != null)
		{
			CodTransaccion = tran.CodTransaccion;
			tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
			txtTransaccion.Text = tran.Sigla;
			lbNombreTransaccion.Text = tran.Descripcion;
			lbNombreTransaccion.Visible = true;
			foreach (Control t in groupBox1.Controls)
			{
				if (t.Tag != null && t.Tag != "")
				{
					int con = Convert.ToInt32(t.Tag);
					if (tran.Configuracion.Contains(con))
					{
						t.Visible = true;
					}
					else
					{
						t.Visible = false;
					}
				}
			}
			return true;
		}
		lbNombreTransaccion.Text = "";
		lbNombreTransaccion.Visible = false;
		foreach (Control t2 in groupBox1.Controls)
		{
			if (t2.Tag != null)
			{
				t2.Visible = false;
			}
		}
		return false;
	}

	private void CargaCliente()
	{
		cli = AdmCli.MuestraCliente(CodCliente);
		cli = AdmCli.CargaDeuda(cli);
		ncredito = admNotac.BuscarNotasXCliente(CodCliente);
		if (cli.Cantidad > 0)
		{
			DialogResult dlgResult = MessageBox.Show("El cliente selecionado presenta" + Environment.NewLine + "Facturas pendientes = " + cli.Cantidad + Environment.NewLine + "Deuda Total = " + cli.Deuda + " soles" + Environment.NewLine + "Linea de crédito = " + cli.LineaCredito + Environment.NewLine + " Desea continuar con la venta?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				ret = 1m;
				return;
			}
			cargadatoscliente();
			ret = default(decimal);
		}
		else
		{
			cargadatoscliente();
			ret = default(decimal);
		}
	}

	private void cargadatoscliente()
	{
		txtCodCliente.Text = cli.Dni;
		if (cli.Ruc != "" && cli.Dni == "")
		{
			txtDocRef.Text = "FT";
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee);
			txtSerie_KeyPress(txtDocRef, ee);
			txtCodCliente.Text = cli.Ruc;
		}
		else if (cli.Dni != "" && cli.Ruc == "")
		{
			txtDocRef.Text = "BV";
			KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee2);
			txtSerie_KeyPress(txtDocRef, ee2);
			txtCodigoCli.Text = cli.Dni;
		}
		else if (cli.Ruc != "" && cli.Dni != "")
		{
			if (MessageBox.Show("Si para FT(Factura) o No para BV(Boleta)", "Seleccione Tipo de Doc. Ref.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				txtDocRef.Text = "BV";
				KeyPressEventArgs ee3 = new KeyPressEventArgs('\r');
				txtDocRef_KeyPress(txtDocRef, ee3);
				txtSerie_KeyPress(txtDocRef, ee3);
				txtCodigoCli.Text = cli.Dni;
			}
			else
			{
				txtDocRef.Text = "FT";
				KeyPressEventArgs ee4 = new KeyPressEventArgs('\r');
				txtDocRef_KeyPress(txtDocRef, ee4);
				txtSerie_KeyPress(txtDocRef, ee4);
				txtCodCliente.Text = cli.Ruc;
			}
		}
		txtNombreCliente.Text = cli.RazonSocial;
		txtDireccionCliente.Text = cli.DireccionLegal;
		txtCodigoCli.Text = cli.CodCliente.ToString();
		if (cli.Moneda == 1)
		{
			if (cli.LineaCredito > 0m)
			{
				cmbFormaPago.Enabled = true;
			}
			else
			{
				cmbFormaPago.Enabled = false;
			}
		}
		cmbFormaPago.SelectedIndex = 0;
		forma = AdmPago.BuscaFormaPagoVenta(cli.FormaPago);
		if (cli.FormaPago != 0)
		{
			cmbFormaPago.SelectedValue = cli.FormaPago;
			EventArgs ee5 = new EventArgs();
			cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee5);
		}
		else
		{
			dtpFechaPago.Value = DateTime.Today;
		}
		if (cli.CodVendedor != 0)
		{
			cbovendedor.SelectedValue = cli.CodVendedor;
		}
		cmbMoneda.SelectedValue = cli.Moneda;
		mon = cli.Moneda;
	}

	private bool BuscaCliente()
	{
		cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
		if (cli != null)
		{
			cli = AdmCli.CargaDeuda(cli);
			if (cli.Cantidad > 0)
			{
				DialogResult dlgResult = MessageBox.Show("El cliente selecionado presenta" + Environment.NewLine + "Facturas pendientes = " + cli.Cantidad + Environment.NewLine + "Deuda Total = " + cli.Deuda + " soles" + Environment.NewLine + "Linea de crédito = " + cli.LineaCredito + Environment.NewLine + " Desea continuar con la venta?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.No)
				{
					txtNombreCliente.Text = "";
					CodCliente = 0;
					return false;
				}
				CodCliente = cli.CodCliente;
				cargadatoscliente();
				return true;
			}
			CodCliente = cli.CodCliente;
			cargadatoscliente();
			return true;
		}
		MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		txtCodCliente.Text = "";
		txtNombreCliente.Text = "";
		CodCliente = 0;
		return false;
	}

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmClientesLista"] != null)
		{
			Application.OpenForms["frmClientesLista"].Activate();
			return;
		}
		frmClientesLista form = new frmClientesLista();
		form.Proceso = 3;
		form.ShowDialog();
		cli = form.cli;
		CodCliente = cli.CodCliente;
		if (CodCliente != 0)
		{
			CargaCliente();
			btnImprimir.Enabled = true;
			ProcessTabKey(forward: true);
			txtDocRef.Focus();
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtCodCliente.Text != "" && BuscaCliente())
		{
			ProcessTabKey(forward: true);
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtTipoCambio.Visible)
			{
				tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
				if (tc != null)
				{
					txtTipoCambio.Text = tc.Compra.ToString();
					dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
				}
				else
				{
					MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					dtpFecha.Value = DateTime.Now.Date;
					dtpFecha.Focus();
				}
			}
			cmbMoneda.Focus();
		}
		catch (Exception)
		{
			dtpFecha.Value = DateTime.Now.Date;
			dtpFecha.Focus();
		}
	}

	private void dtpFecha_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
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

	private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void cmbMoneda_Leave(object sender, EventArgs e)
	{
		if (CodTransaccion == 0)
		{
			cmbMoneda.Focus();
		}
		if (cmbMoneda.SelectedValue != null && cmbMoneda.SelectedText.Equals("NUEVOS SOLES"))
		{
			label8.Visible = false;
			txtTipoCambio.Visible = false;
		}
	}

	private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtDocRef.Text != "")
		{
			if (BuscaTipoDocumento())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			return true;
		}
		CodDocumento = 0;
		return false;
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

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Activate();
		}
		else if (cli.Ruc != "")
		{
			frmDocumentos form = new frmDocumentos();
			form.Proceso = 3;
			form.ShowDialog();
			doc = form.doc;
			CodDocumento = doc.CodTipoDocumento;
			txtDocRef.Text = doc.Sigla;
			if (CodDocumento != 0)
			{
				ProcessTabKey(forward: true);
			}
		}
	}

	private void VerificarCabecera()
	{
		Validacion = true;
		if (CodTransaccion == 0 || CodDocumento == 0)
		{
			Validacion = false;
		}
		if (txtCodCliente.Visible && CodCliente == 0)
		{
			Validacion = false;
		}
		if (Validacion && Proceso == 1)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void sololectura(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		cmbMoneda.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtNumero.Visible = estado;
		txtNumero.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnImprimir.Visible = !estado;
		btnGuardar.Visible = !estado;
		cbovendedor.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtSerie.ReadOnly = estado;
	}

	private void BloquearEdicion(bool estado)
	{
		txtTransaccion.ReadOnly = estado;
		dtpFecha.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		cmbMoneda.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtNumero.Visible = estado;
		txtNumero.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnImprimir.Visible = !estado;
		cbovendedor.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtSerie.ReadOnly = estado;
	}

	private void CargaDetalle()
	{
		try
		{
			DataTable newDataDetalle = new DataTable();
			dgvDetalle.Rows.Clear();
			newDataDetalle = AdmVenta.CargaDetallexEntregar(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen);
			foreach (DataRow row in newDataDetalle.Rows)
			{
				dgvDetalle.Rows.Add(row[0].ToString(), "0", row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[18].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		darformatogrilla();
	}

	private void darformatogrilla()
	{
		if (dgvDetalle.RowCount <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToDecimal(row.Cells[CantidadPendiente.Name].Value) > 0m)
			{
				row.Cells[CantidadEntregada.Name].Value = $"{Convert.ToDecimal(row.Cells[cantidad.Name].Value) - Convert.ToDecimal(row.Cells[CantidadPendiente.Name].Value):#,##0.00}";
			}
			row.Cells[CantidadxSalir.Name].Style.BackColor = Color.PeachPuff;
			row.Cells[CantidadxSalir.Name].Value = 0m;
		}
	}

	private void CargaDetalleGuia()
	{
		dgvDetalle.DataSource = AdmGuia.CargaDetalle(Convert.ToInt32(guia.CodGuiaRemision));
	}

	private void txtDocRef_Leave(object sender, EventArgs e)
	{
		BuscaTipoDocumento();
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 0;
	}

	private void iniciaformulario()
	{
		CargaMoneda();
		dtpFecha.MaxDate = DateTime.Today.Date;
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		CargaFormaPagos();
		CargaVendedores();
		if (Proceso == 2)
		{
			CargaVenta();
		}
		else if (Proceso == 3)
		{
			CargaVenta();
			btnGuardar.Enabled = false;
		}
		CargaVendedores();
	}

	private void frmVenta_Shown(object sender, EventArgs e)
	{
		txtTransaccion.Focus();
		txtTransaccion.Text = "FT";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtTransaccion_KeyPress(txtTransaccion, ee);
		txtCodCliente.Focus();
		if (Proceso == 1)
		{
			if (txtTipoCambio.Visible)
			{
				if (tc == null)
				{
					MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Close();
				}
				else
				{
					txtTipoCambio.Text = tc.Compra.ToString();
				}
			}
		}
		else if (Proceso == 4)
		{
			Proceso = 1;
		}
	}

	private void CargaVenta()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodVenta));
			ser = Admser.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
			guia = AdmGuia.CargaGuiaVenta(Convert.ToInt32(CodVenta));
			if (venta != null)
			{
				txtNumDoc.Text = venta.CodFacturaVenta;
				CodTransaccion = venta.CodTipoTransaccion;
				CargaTransaccion();
				if (txtCodCliente.Enabled)
				{
					CodCliente = venta.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = venta.DNI;
					txtNombreCliente.Text = venta.RazonSocialCliente;
					txtDireccionCliente.Text = venta.Direccion;
				}
				dtpFecha.Value = venta.FechaSalida;
				cmbMoneda.SelectedValue = venta.Moneda;
				txtTipoCambio.Text = venta.TipoCambio.ToString();
				CodDocumento = venta.CodTipoDocumento;
				txtDocRef.Text = venta.SiglaDocumento;
				txtSerie.Text = venta.Serie;
				if (Procede != 4)
				{
					txtNumero.Text = venta.NumDoc;
				}
				else
				{
					txtNumero.Text = numSerie;
				}
				if (txtSerie.Text == "" && txtNumero.Text == "")
				{
					xgenerar = true;
				}
				if (cbovendedor.Enabled && venta.CodVendedor != 0)
				{
					cbovendedor.SelectedValue = venta.CodVendedor;
				}
				cmbFormaPago.SelectedValue = venta.FormaPago;
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
				dtpFechaPago.Value = venta.FechaPago;
				txtBruto.Text = $"{venta.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{venta.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{venta.Total - venta.Igv:#,##0.00}";
				txtIGV.Text = $"{venta.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{venta.Total:#,##0.00}";
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && BuscaSerie())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Visible = true;
				txtNumero.Enabled = false;
				txtNumero.Focus();
				txtNumero.Text = "";
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Enabled = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
			ProcessTabKey(forward: true);
			cmbFormaPago.Focus();
		}
		if (e.KeyChar == '\r')
		{
			cmbFormaPago.Focus();
		}
	}

	private void txtSerie_Leave(object sender, EventArgs e)
	{
		if (BuscaSerie2())
		{
			txtSerie.Text = ser.Serie.ToString();
			if (ser.PreImpreso)
			{
				txtNumero.Visible = true;
				txtNumero.Text = "";
				txtNumero.Focus();
			}
			else
			{
				txtNumero.Text = "";
				txtNumero.Visible = false;
				txtNumero.Text = ser.Numeracion.ToString();
			}
		}
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

	private bool BuscaSerie3(int codDocumento)
	{
		ser = Admser.MuestraSerie(codDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			return true;
		}
		CodSerie = 0;
		return false;
	}

	private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtNumero_Leave(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if ((Procede == 2 && Procede == 3) || Proceso != 1)
		{
			return;
		}
		calculatotales();
		if (dgvDetalle.RowCount <= 0)
		{
			return;
		}
		int Indice = 0;
		Indice = dgvDetalle.RowCount - 1;
		if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
		{
			if (TipoCambio != 0m)
			{
				dgvDetalle[8, Indice].Value = Convert.ToDecimal(dgvDetalle[8, Indice].Value) * TipoCambio;
				dgvDetalle[9, Indice].Value = Convert.ToDecimal(dgvDetalle[9, Indice].Value) * TipoCambio;
				dgvDetalle[13, Indice].Value = Convert.ToDecimal(dgvDetalle[13, Indice].Value) * TipoCambio;
				dgvDetalle[14, Indice].Value = Convert.ToDecimal(dgvDetalle[14, Indice].Value) * TipoCambio;
				dgvDetalle[15, Indice].Value = Convert.ToDecimal(dgvDetalle[15, Indice].Value) * TipoCambio;
				dgvDetalle[16, Indice].Value = Convert.ToDecimal(dgvDetalle[16, Indice].Value) * TipoCambio;
			}
		}
		else if (Convert.ToInt32(cmbMoneda.SelectedValue) != 2)
		{
		}
	}

	public void calculatotales()
	{
		if (Proceso == 0 || Procede == 3)
		{
			return;
		}
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal preciovent = default(decimal);
		decimal igvt = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			preciovent += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			if (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "21" && banderadelete)
			{
				montogratuitas -= Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				montosventa();
				banderadelete = false;
			}
			if ((Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "10" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "11" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "12" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "13" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "14" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "15" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "16" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "17" && banderadelete))
			{
				montogravadas -= Convert.ToDecimal(row.Cells[valorventa.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				montosventa();
				banderadelete = false;
			}
			if (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "20" && banderadelete)
			{
				montoexoneradas -= Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				montosventa();
				banderadelete = false;
			}
			if ((Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "30" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "31" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "32" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "33" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "34" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "35" && banderadelete) || (Convert.ToString(row.Cells[TipoImpuesto.Name].Value) == "36" && banderadelete))
			{
				montoinafectas -= Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				montosventa();
				banderadelete = false;
			}
		}
		if (dgvDetalle.RowCount == 0)
		{
			montogratuitas = default(decimal);
			montoexoneradas = default(decimal);
			montogravadas = default(decimal);
			montoinafectas = default(decimal);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtPrecioVenta.Text = $"{preciovent:#,##0.00}";
	}

	public void montosventa()
	{
		if (Proceso != 0 && Proceso != 3)
		{
			if (montogravadas > 0m)
			{
				txtgravadas.Clear();
				txtgravadas.Text = $"{montogravadas:#,##0.00}";
			}
			if (montogratuitas > 0m)
			{
				txtgratuitas.Clear();
				txtgratuitas.Text = $"{montogratuitas:#,##0.00}";
			}
			if (montoexoneradas > 0m)
			{
				txtexoneradas.Clear();
				txtexoneradas.Text = $"{montoexoneradas:#,##0.00}";
			}
			if (montoinafectas > 0m)
			{
				txtinafectas.Clear();
				txtinafectas.Text = $"{montoinafectas:#,##0.00}";
			}
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Proceso == 0)
		{
			return;
		}
		venta.CodSucursal = frmLogin.iCodSucursal;
		venta.CodFacturaVenta = CodVenta;
		venta.CodCliente = Convert.ToInt32(venta.CodCliente);
		venta.CodAlmacen = frmLogin.iCodAlmacen;
		venta.CodUser = frmLogin.iCodUser;
		venta.Estado = 1;
		venta.FechaRegistro = DateTime.Now;
		if (Proceso != 3 || !AdmVenta.insertventaentregar(venta))
		{
			return;
		}
		RecorreDetalle();
		if (detalle1.Count > 0)
		{
			foreach (clsDetalleFacturaVenta det in detalle1)
			{
				AdmVenta.insertdetalleventaentregar(det);
				if (det.CodDetalleVenta == 0)
				{
					MessageBox.Show("Error No se puede Registrar los Datos. Falta Stock de Productos");
					AdmVenta.rollback(Convert.ToInt32(venta.CodFacturaVenta), 0);
					return;
				}
			}
		}
		MessageBox.Show("Los datos se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		VerificaFactura(Convert.ToInt32(CodVenta));
		btnImprimir.Visible = false;
		Close();
	}

	private void VerificaFactura(int codigo)
	{
		int contador = 0;
		DataTable newDataDetalle = new DataTable();
		dgvDetalle.Rows.Clear();
		newDataDetalle = AdmVenta.CargaDetallexEntregar(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen);
		foreach (DataRow row in newDataDetalle.Rows)
		{
			listacodigos.Add(Convert.ToInt32(row[0]));
		}
		if (listacodigos.Count > 0)
		{
			foreach (int lista in listacodigos)
			{
				if (AdmVenta.GetCantidadPendiente(lista) == 0 && AdmVenta.CambiaEstadoDetalle(lista))
				{
					contador++;
				}
			}
		}
		if (listacodigos.Count == contador && AdmVenta.CambiaEstadoFactura(Convert.ToInt32(CodVenta)))
		{
			contador = 0;
			listacodigos.Clear();
		}
	}

	private void VerificaSaldoCaja()
	{
		Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, frmLogin.iCodAlmacen, frmLogin.iCodUser);
		CodigoCaja = Caja.Codcaja;
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		detalle1.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
		deta.CodDetalleVenta = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
		deta.Codventaentregar = Convert.ToInt32(venta.Codventaentregar);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.CodUnidad = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDecimal(fila.Cells[CantidadxSalir.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.FechaRegistro = DateTime.Now;
		deta.CantidadPendiente = Convert.ToDecimal(fila.Cells[CantidadPendiente.Name].Value);
		detalle1.Add(deta);
	}

	private void RecorreDetalleGuia()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleguia(row);
		}
	}

	private void añadedetalleguia(DataGridViewRow fila)
	{
		clsDetalleGuiaRemision deta = new clsDetalleGuiaRemision();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodGuiaRemision = Convert.ToInt32(guia.CodGuiaRemision);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		if (Convert.ToBoolean(guia.Facturado))
		{
			deta.CantidadPendiente = 0.0;
			deta.Pendiente = false;
		}
		else
		{
			deta.CantidadPendiente = deta.Cantidad;
			deta.Pendiente = true;
		}
		deta.CodUser = frmLogin.iCodUser;
		detalleg.Add(deta);
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
		form.Proceso = 3;
		form.DocSeleccionado = CodDocumento;
		form.ShowDialog();
		ser = form.ser;
		CodSerie = ser.CodSerie;
		manual = Convert.ToInt32(ser.PreImpreso);
		if (CodSerie != 0)
		{
			txtSerie.Text = ser.Serie;
		}
		if (CodSerie != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtCodDocumento_TextChanged(object sender, EventArgs e)
	{
		txtSerie.Text = "";
		txtNumero.Text = "";
		CodSerie = 0;
	}

	public void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		if (fpago.Dias > forma.Dias)
		{
			DialogResult result = MessageBox.Show("Esta forma de pago excede a la Forma de Pago del Cliente" + Environment.NewLine + "Máx.FormaPago del Cliente = " + forma.Descripcion, "Facturación Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			if (result == DialogResult.OK)
			{
				cmbFormaPago.SelectedValue = forma.CodFormaPago;
			}
		}
	}

	private void txtPDescuento_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
	}

	private bool ActualizaCobro(string CodPago)
	{
		string sigl = "";
		bool devuelve = false;
		try
		{
			sigl = "RC";
			if (valida_serie(sigl))
			{
				seri2 = null;
				seri2 = Admser.BuscaSeriexDocumento(codDocumentoPago, frmLogin.iCodAlmacen);
				if (seri2 != null)
				{
					seriePago = seri2.Serie;
					numeroPago = seri2.Numeracion.ToString();
					devuelve = (AdmPagos.ActualizaPagoAprobado(seriePago, numeroPago, Convert.ToInt32(CodPago)) ? true : false);
				}
			}
			return devuelve;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private bool valida_serie(string sigl)
	{
		doc2 = null;
		try
		{
			doc2 = Admdoc.BuscaTipoDocumento(sigl);
			if (doc2 != null)
			{
				codDocumentoPago = doc2.CodTipoDocumento;
				siglaPago = doc2.Sigla;
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private bool ActualizaCorrelativos(int CodSerie, string txtSeries, string txtNumeros, string CodVenta)
	{
		try
		{
			if (AdmVenta.actualizaFactura_venta(CodSerie, txtSeries, txtNumeros, CodVenta))
			{
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
	}

	private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if ((Procede == 1 || Procede == 2) && dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa" && (Proceso == 1 || Proceso == 2))
		{
			calculatotales();
		}
	}

	private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (dgvDetalle.RowCount > 0)
		{
			VentaEnMoneda();
		}
		calculatotales();
		mon = Convert.ToInt32(cmbMoneda.SelectedValue);
		calculatotales();
	}

	public List<int> carga_Correlativos()
	{
		int j = 0;
		datos = AdmVenta.ListaFacturaVenta(frmLogin.iCodAlmacen);
		ser = Admser.MuestraSerie(8, frmLogin.iCodAlmacen);
		correlativo.Clear();
		for (int i = ser.Inicio; i < ser.Numeracion; i++)
		{
			if (j < datos.Rows.Count)
			{
				if (i == Convert.ToInt32(datos.Rows[j]["numDocumento"]))
				{
					j++;
					fecha1 = Convert.ToDateTime(datos.Rows[j - 1]["fechasalida"]);
				}
				else
				{
					correlativo.Add(i);
					fecha2 = Convert.ToDateTime(datos.Rows[j]["fechasalida"]);
				}
			}
		}
		return correlativo;
	}

	public bool valida_existente(int serie)
	{
		datos = AdmVenta.ListaFacturaVenta(frmLogin.iCodAlmacen);
		for (int j = 0; j < datos.Rows.Count; j++)
		{
			if (serie == Convert.ToInt32(datos.Rows[j]["numDocumento"]))
			{
				rpta = false;
			}
			else
			{
				rpta = true;
			}
		}
		return rpta;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		frmFacturasManuales frm = new frmFacturasManuales();
		carga_Correlativos();
		frm.num_correlativo = carga_Correlativos();
		frm.ShowDialog();
	}

	private void txtRazonSocialTransporte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmEmpresaTransporte"] != null)
			{
				Application.OpenForms["frmEmpresaTransporte"].Activate();
				return;
			}
			frmEmpresaTransporte form = new frmEmpresaTransporte();
			form.Proceso = 3;
			form.ShowDialog();
			empT = form.emp;
			CodEmpresaTransporte = empT.CodEmpresaTranporte;
		}
	}

	private void txtSerieG_KeyDown(object sender, KeyEventArgs e)
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
		form.DocSeleccionado = 11;
		form.Proceso = 3;
		form.ShowDialog();
		ser = form.ser;
		CodSerieG = ser.CodSerie;
		numG = ser.Numeracion;
		if (CodSerieG != 0)
		{
			if (ser.PreImpreso)
			{
				CodSerieG = ser.CodSerie;
			}
			else
			{
				CodSerieG = ser.CodSerie;
			}
		}
		if (CodSerieG != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	public bool VerificarDetracciones()
	{
		bool grav = false;
		decimal sumadet = default(decimal);
		if (CodDocumento == 2)
		{
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					if (Convert.ToDecimal(row.Cells[igv.Name].Value) != 0m)
					{
						grav = true;
					}
					else if (Convert.ToDecimal(row.Cells[precioventa.Name].Value) == 0m)
					{
						grav = true;
					}
					else
					{
						sumadet += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
					}
				}
			}
			if (sumadet <= 700m)
			{
				return true;
			}
			if (grav)
			{
				MessageBox.Show("Operacion no permitida, por estar afecta a detracción!", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return false;
			}
			return true;
		}
		return true;
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == (Keys.G | Keys.Control))
		{
			btnGuardar.PerformClick();
			return true;
		}
		return false;
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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
		if (Proceso != 0 && e.ControlToValidate.Visible)
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
		if (Proceso != 0)
		{
			if (dgvDetalle.Rows.Count > 0)
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

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator8_ValidateValue(object sender, ValidateValueEventArgs e)
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
					e.IsValid = true;
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

	private void customValidator9_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator10_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator11_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void txtNumero_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmbFormaPago.Focus();
		}
	}

	private void cbovendedor_SelectionChangeCommitted(object sender, EventArgs e)
	{
		dtpFecha.Focus();
	}

	private void cbovendedor_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			dtpFecha.Focus();
		}
	}

	private void dtpFecha_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmbMoneda.Focus();
		}
	}

	private void cmbAlmacen_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnImprimir.Focus();
		}
	}

	private void chkVentaGratuita_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaGratuita.Checked)
		{
			chkVentaInafecta.Checked = false;
			chkVentaExonerada.Checked = false;
			chkVentaDsctoGlobal.Checked = false;
		}
	}

	private void chkVentaInafecta_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaInafecta.Checked)
		{
			chkVentaGratuita.Checked = false;
			chkVentaExonerada.Checked = false;
			chkVentaDsctoGlobal.Checked = false;
		}
	}

	private void chkVentaExonerada_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaExonerada.Checked)
		{
			chkVentaGratuita.Checked = false;
			chkVentaInafecta.Checked = false;
			chkVentaDsctoGlobal.Checked = false;
		}
	}

	private void chkVentaDsctoGlobal_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaDsctoGlobal.Checked)
		{
			chkVentaGratuita.Checked = false;
			chkVentaInafecta.Checked = false;
			chkVentaExonerada.Checked = false;
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		decimal cantidadsal = default(decimal);
		if (dgvDetalle.RowCount <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			cantidadsal = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[CantidadxSalir.Name].Value);
			if (e.ColumnIndex == 28)
			{
				if (cantidadsal >= 0m && cantidadsal <= Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[CantidadPendiente.Name].Value))
				{
					row.Cells[CantidadxSalir.Name].Style.BackColor = Color.PeachPuff;
					btnGuardar.Enabled = true;
				}
				else
				{
					MessageBox.Show("La cantidad a entregar no debe superar" + Environment.NewLine + "la cantidad Pendiente ni ser menor que cero", "ADVERTENCIA", MessageBoxButtons.OK);
					row.Cells[CantidadxSalir.Name].Style.BackColor = Color.Red;
					btnGuardar.Enabled = false;
				}
			}
		}
	}

	private void dgvDetalle_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		if (dgvDetalle.IsCurrentCellDirty)
		{
			dgvDetalle.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			VerificaFactura(Convert.ToInt32(CodVenta));
			if (frmLogin.iCodAlmacen != 0)
			{
				frmRptFactura frm = new frmRptFactura();
				CRSalidaDespacho rpt = new CRSalidaDespacho();
				rpt.Load("CRSalidaDespacho.rpt");
				rpt.SetDataSource(ds.salidadespacho(Convert.ToInt32(venta.CodFacturaVenta), venta.Codventaentregar));
				frm.crvReporteFactura.ReportSource = rpt;
				frm.ShowDialog();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVentaxEentregar));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.chkVentaDsctoGlobal = new System.Windows.Forms.CheckBox();
		this.chkVentaExonerada = new System.Windows.Forms.CheckBox();
		this.chkVentaInafecta = new System.Windows.Forms.CheckBox();
		this.txtinafectas = new System.Windows.Forms.TextBox();
		this.chkVentaGratuita = new System.Windows.Forms.CheckBox();
		this.label26 = new System.Windows.Forms.Label();
		this.txtexoneradas = new System.Windows.Forms.TextBox();
		this.label32 = new System.Windows.Forms.Label();
		this.txtgratuitas = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.txtgravadas = new System.Windows.Forms.TextBox();
		this.label22 = new System.Windows.Forms.Label();
		this.txtDsctoGobal = new System.Windows.Forms.TextBox();
		this.label21 = new System.Windows.Forms.Label();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codSalida1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockPend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dsctoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoarticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TipoImpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CantidadPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CantidadEntregada = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CantidadxSalir = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label37 = new System.Windows.Forms.Label();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtCodigoCli = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label24 = new System.Windows.Forms.Label();
		this.cbovendedor = new System.Windows.Forms.ComboBox();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator11 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator10 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.btnEliminar);
		this.groupBox2.Controls.Add(this.chkVentaDsctoGlobal);
		this.groupBox2.Controls.Add(this.chkVentaExonerada);
		this.groupBox2.Controls.Add(this.chkVentaInafecta);
		this.groupBox2.Controls.Add(this.txtinafectas);
		this.groupBox2.Controls.Add(this.chkVentaGratuita);
		this.groupBox2.Controls.Add(this.label26);
		this.groupBox2.Controls.Add(this.txtexoneradas);
		this.groupBox2.Controls.Add(this.label32);
		this.groupBox2.Controls.Add(this.txtgratuitas);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.txtgravadas);
		this.groupBox2.Controls.Add(this.label22);
		this.groupBox2.Controls.Add(this.txtDsctoGobal);
		this.groupBox2.Controls.Add(this.label21);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Controls.Add(this.txtBruto);
		this.groupBox2.Controls.Add(this.txtPrecioVenta);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.txtIGV);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.txtDscto);
		this.groupBox2.Controls.Add(this.txtValorVenta);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1190, 402);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.highlighter1.SetHighlightOnFocus(this.btnEliminar, true);
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(12, 341);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 110;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.chkVentaDsctoGlobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaDsctoGlobal.AutoSize = true;
		this.chkVentaDsctoGlobal.Location = new System.Drawing.Point(402, 379);
		this.chkVentaDsctoGlobal.Name = "chkVentaDsctoGlobal";
		this.chkVentaDsctoGlobal.Size = new System.Drawing.Size(130, 17);
		this.chkVentaDsctoGlobal.TabIndex = 109;
		this.chkVentaDsctoGlobal.Text = "Venta con Descuento";
		this.chkVentaDsctoGlobal.UseVisualStyleBackColor = true;
		this.chkVentaDsctoGlobal.Visible = false;
		this.chkVentaDsctoGlobal.CheckedChanged += new System.EventHandler(chkVentaDsctoGlobal_CheckedChanged);
		this.chkVentaExonerada.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaExonerada.AutoSize = true;
		this.chkVentaExonerada.Location = new System.Drawing.Point(40, 381);
		this.chkVentaExonerada.Name = "chkVentaExonerada";
		this.chkVentaExonerada.Size = new System.Drawing.Size(108, 17);
		this.chkVentaExonerada.TabIndex = 108;
		this.chkVentaExonerada.Text = "Venta Exonerada";
		this.chkVentaExonerada.UseVisualStyleBackColor = true;
		this.chkVentaExonerada.Visible = false;
		this.chkVentaExonerada.CheckedChanged += new System.EventHandler(chkVentaExonerada_CheckedChanged);
		this.chkVentaInafecta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaInafecta.AutoSize = true;
		this.chkVentaInafecta.Location = new System.Drawing.Point(284, 381);
		this.chkVentaInafecta.Name = "chkVentaInafecta";
		this.chkVentaInafecta.Size = new System.Drawing.Size(96, 17);
		this.chkVentaInafecta.TabIndex = 107;
		this.chkVentaInafecta.Text = "Venta Inafecta";
		this.chkVentaInafecta.UseVisualStyleBackColor = true;
		this.chkVentaInafecta.Visible = false;
		this.chkVentaInafecta.CheckedChanged += new System.EventHandler(chkVentaInafecta_CheckedChanged);
		this.txtinafectas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtinafectas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtinafectas.Location = new System.Drawing.Point(756, 360);
		this.txtinafectas.Name = "txtinafectas";
		this.txtinafectas.ReadOnly = true;
		this.txtinafectas.Size = new System.Drawing.Size(75, 18);
		this.txtinafectas.TabIndex = 38;
		this.txtinafectas.Tag = "7";
		this.txtinafectas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtinafectas.Visible = false;
		this.chkVentaGratuita.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaGratuita.AutoSize = true;
		this.chkVentaGratuita.Location = new System.Drawing.Point(170, 381);
		this.chkVentaGratuita.Name = "chkVentaGratuita";
		this.chkVentaGratuita.Size = new System.Drawing.Size(94, 17);
		this.chkVentaGratuita.TabIndex = 106;
		this.chkVentaGratuita.Text = "Venta Gratuita";
		this.chkVentaGratuita.UseVisualStyleBackColor = true;
		this.chkVentaGratuita.Visible = false;
		this.chkVentaGratuita.CheckedChanged += new System.EventHandler(chkVentaGratuita_CheckedChanged);
		this.label26.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.Location = new System.Drawing.Point(701, 363);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(49, 12);
		this.label26.TabIndex = 36;
		this.label26.Tag = "7";
		this.label26.Text = "Inafectas :";
		this.label26.Visible = false;
		this.txtexoneradas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtexoneradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtexoneradas.Location = new System.Drawing.Point(756, 337);
		this.txtexoneradas.Name = "txtexoneradas";
		this.txtexoneradas.ReadOnly = true;
		this.txtexoneradas.Size = new System.Drawing.Size(75, 18);
		this.txtexoneradas.TabIndex = 37;
		this.txtexoneradas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtexoneradas.Visible = false;
		this.label32.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label32.Location = new System.Drawing.Point(694, 340);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(59, 12);
		this.label32.TabIndex = 35;
		this.label32.Text = "Exoneradas :";
		this.label32.Visible = false;
		this.txtgratuitas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgratuitas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgratuitas.Location = new System.Drawing.Point(603, 360);
		this.txtgratuitas.Name = "txtgratuitas";
		this.txtgratuitas.ReadOnly = true;
		this.txtgratuitas.Size = new System.Drawing.Size(75, 18);
		this.txtgratuitas.TabIndex = 34;
		this.txtgratuitas.Tag = "7";
		this.txtgratuitas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtgratuitas.Visible = false;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(548, 363);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(48, 12);
		this.label19.TabIndex = 32;
		this.label19.Tag = "7";
		this.label19.Text = "Gratuitas :";
		this.label19.Visible = false;
		this.txtgravadas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtgravadas.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgravadas.Location = new System.Drawing.Point(603, 337);
		this.txtgravadas.Name = "txtgravadas";
		this.txtgravadas.ReadOnly = true;
		this.txtgravadas.Size = new System.Drawing.Size(75, 18);
		this.txtgravadas.TabIndex = 33;
		this.txtgravadas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtgravadas.Visible = false;
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(545, 340);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(51, 12);
		this.label22.TabIndex = 30;
		this.label22.Text = "Gravadas :";
		this.label22.Visible = false;
		this.txtDsctoGobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDsctoGobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDsctoGobal.Location = new System.Drawing.Point(921, 358);
		this.txtDsctoGobal.Name = "txtDsctoGobal";
		this.txtDsctoGobal.ReadOnly = true;
		this.txtDsctoGobal.Size = new System.Drawing.Size(75, 18);
		this.txtDsctoGobal.TabIndex = 25;
		this.txtDsctoGobal.Tag = "7";
		this.txtDsctoGobal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(841, 361);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(63, 12);
		this.label21.TabIndex = 23;
		this.label21.Tag = "7";
		this.label21.Text = "Dscto Global :";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codSalida1, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.stockPend, this.dsctoMax, this.coduser, this.fecharegistro, this.tipoarticulo, this.TipoImpuesto, this.CantidadPendiente, this.CantidadEntregada, this.CantidadxSalir);
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle12;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1184, 315);
		this.dgvDetalle.TabIndex = 2;
		this.superValidator1.SetValidator1(this.dgvDetalle, this.customValidator5);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.CurrentCellDirtyStateChanged += new System.EventHandler(dgvDetalle_CurrentCellDirtyStateChanged);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codSalida1.HeaderText = "CodSalida";
		this.codSalida1.Name = "codSalida1";
		this.codSalida1.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 87;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 400;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 150;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle14;
		this.cantidad.HeaderText = "Cantidad Comprada";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 70;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N2";
		dataGridViewCellStyle15.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle15;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Visible = false;
		this.preciounit.Width = 75;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle16;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
		this.importe.Width = 85;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		dataGridViewCellStyle17.NullValue = null;
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle17;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle18;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle19;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle20.Format = "N2";
		dataGridViewCellStyle20.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle20;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.montodscto.Width = 80;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N2";
		dataGridViewCellStyle21.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle21;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.valorventa.Width = 85;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N2";
		dataGridViewCellStyle22.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle22;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.igv.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N2";
		dataGridViewCellStyle23.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Visible = false;
		this.precioventa.Width = 85;
		this.precioreal.DataPropertyName = "precioreal";
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.stockPend.DataPropertyName = "stockdisponible";
		this.stockPend.HeaderText = "StockDisponible";
		this.stockPend.Name = "stockPend";
		this.stockPend.Visible = false;
		this.dsctoMax.DataPropertyName = "maxPorcDescto";
		this.dsctoMax.HeaderText = "%DsctoMax";
		this.dsctoMax.Name = "dsctoMax";
		this.dsctoMax.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.tipoarticulo.DataPropertyName = "codTipoArticulo";
		this.tipoarticulo.HeaderText = "TipoArticulo";
		this.tipoarticulo.Name = "tipoarticulo";
		this.tipoarticulo.Visible = false;
		this.TipoImpuesto.DataPropertyName = "TipoImpuesto";
		this.TipoImpuesto.HeaderText = "Tipo Impuesto";
		this.TipoImpuesto.Name = "TipoImpuesto";
		this.TipoImpuesto.Visible = false;
		this.CantidadPendiente.DataPropertyName = "CantidadPendiente";
		this.CantidadPendiente.HeaderText = "Cantidad Pendiente";
		this.CantidadPendiente.Name = "CantidadPendiente";
		this.CantidadPendiente.ReadOnly = true;
		this.CantidadEntregada.DataPropertyName = "CantidadEntregada";
		this.CantidadEntregada.HeaderText = "Cantidad Entregada";
		this.CantidadEntregada.Name = "CantidadEntregada";
		this.CantidadEntregada.ReadOnly = true;
		this.CantidadxSalir.DataPropertyName = "CantidadxSalir";
		this.CantidadxSalir.HeaderText = "Cantidad x Salir";
		this.CantidadxSalir.Name = "CantidadxSalir";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBruto.Location = new System.Drawing.Point(921, 380);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(75, 18);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVenta.Location = new System.Drawing.Point(1067, 380);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 18);
		this.txtPrecioVenta.TabIndex = 28;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(1014, 383);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(46, 12);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtIGV.Location = new System.Drawing.Point(1067, 357);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 18);
		this.txtIGV.TabIndex = 27;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(1023, 363);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(36, 12);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDscto.Location = new System.Drawing.Point(921, 335);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 18);
		this.txtDscto.TabIndex = 24;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtValorVenta.Location = new System.Drawing.Point(1067, 334);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 18);
		this.txtValorVenta.TabIndex = 26;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(1014, 337);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(47, 12);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(850, 338);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(55, 12);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(870, 383);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(590, 110);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(79, 34);
		this.btnImprimir.TabIndex = 20;
		this.btnImprimir.Text = "Imprimir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1111, 112);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 26;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(1028, 112);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 23;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox1.Controls.Add(this.btnGuardar);
		this.groupBox1.Controls.Add(this.btnSalir);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.txtDireccionCliente);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.txtCodigoCli);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.cbovendedor);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.btnImprimir);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.lbNombreTransaccion);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 408);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1190, 150);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera:";
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(483, 22);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(8, 12);
		this.label37.TabIndex = 103;
		this.label37.Text = "-";
		this.txtDireccionCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDireccionCliente.Location = new System.Drawing.Point(76, 72);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(487, 18);
		this.txtDireccionCliente.TabIndex = 3;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(6, 72);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 12);
		this.label16.TabIndex = 89;
		this.label16.Text = "Direccion";
		this.txtCodigoCli.Enabled = false;
		this.txtCodigoCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodigoCli.Location = new System.Drawing.Point(572, 46);
		this.txtCodigoCli.Name = "txtCodigoCli";
		this.txtCodigoCli.ReadOnly = true;
		this.txtCodigoCli.Size = new System.Drawing.Size(19, 18);
		this.txtCodigoCli.TabIndex = 88;
		this.txtCodigoCli.Visible = false;
		this.txtNumero.Enabled = false;
		this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumero.Location = new System.Drawing.Point(494, 20);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(69, 18);
		this.txtNumero.TabIndex = 6;
		this.superValidator1.SetValidator1(this.txtNumero, this.customValidator11);
		this.txtNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNumero_KeyDown);
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(1024, 87);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(59, 12);
		this.label8.TabIndex = 80;
		this.label8.Text = "Tipo Cambio:";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTipoCambio.Location = new System.Drawing.Point(1089, 84);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(90, 18);
		this.txtTipoCambio.TabIndex = 13;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(332, 99);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(48, 12);
		this.label24.TabIndex = 75;
		this.label24.Text = "Vendedor:";
		this.cbovendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbovendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbovendedor.FormattingEnabled = true;
		this.cbovendedor.Items.AddRange(new object[1] { "Sin Vendedor" });
		this.cbovendedor.Location = new System.Drawing.Point(402, 100);
		this.cbovendedor.Name = "cbovendedor";
		this.cbovendedor.Size = new System.Drawing.Size(161, 20);
		this.cbovendedor.TabIndex = 10;
		this.cbovendedor.SelectionChangeCommitted += new System.EventHandler(cbovendedor_SelectionChangeCommitted);
		this.cbovendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(cbovendedor_KeyDown);
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFechaPago.Location = new System.Drawing.Point(242, 98);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 18);
		this.dtpFechaPago.TabIndex = 8;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Enabled = false;
		this.cmbFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(76, 98);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(161, 20);
		this.cmbFormaPago.TabIndex = 7;
		this.cmbFormaPago.Tag = "16";
		this.superValidator1.SetValidator1(this.cmbFormaPago, this.customValidator7);
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(6, 97);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(67, 12);
		this.label3.TabIndex = 44;
		this.label3.Tag = "16";
		this.label3.Text = "Forma de Pago";
		this.label3.Visible = false;
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(443, 20);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(38, 18);
		this.txtSerie.TabIndex = 5;
		this.txtSerie.Tag = "13";
		this.superValidator1.SetValidator1(this.txtSerie, this.customValidator10);
		this.txtSerie.Visible = false;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(1089, 60);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(89, 20);
		this.cmbMoneda.TabIndex = 12;
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(1039, 63);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(44, 12);
		this.label17.TabIndex = 31;
		this.label17.Text = "Moneda :";
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreCliente.Location = new System.Drawing.Point(182, 46);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(381, 18);
		this.txtNombreCliente.TabIndex = 2;
		this.txtNombreCliente.Tag = "3";
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodCliente.Location = new System.Drawing.Point(76, 46);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(100, 18);
		this.txtCodCliente.TabIndex = 1;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator6);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(6, 48);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(34, 12);
		this.label15.TabIndex = 20;
		this.label15.Text = "Cliente";
		this.txtNumDoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumDoc.Location = new System.Drawing.Point(1089, 15);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 18);
		this.txtNumDoc.TabIndex = 14;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(1033, 18);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(50, 12);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(409, 20);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 18);
		this.txtDocRef.TabIndex = 4;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(350, 23);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(45, 12);
		this.label5.TabIndex = 8;
		this.label5.Text = "Doc. Ref.";
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(110, 22);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(204, 13);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Enabled = false;
		this.txtTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTransaccion.Location = new System.Drawing.Point(76, 20);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.ReadOnly = true;
		this.txtTransaccion.Size = new System.Drawing.Size(28, 18);
		this.txtTransaccion.TabIndex = 17;
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(6, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(55, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFecha.Location = new System.Drawing.Point(1089, 37);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(89, 18);
		this.dtpFecha.TabIndex = 11;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyDown += new System.Windows.Forms.KeyEventHandler(dtpFecha_KeyDown);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(1047, 42);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator5.ErrorMessage = "Ingrese Detalle.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator11.ErrorMessage = "Ingrese Numero";
		this.customValidator11.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator11.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator11_ValidateValue);
		this.customValidator7.ErrorMessage = "Ingrese Forma Pago";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator10.ErrorMessage = "Ingrese Serie.";
		this.customValidator10.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator10.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator10_ValidateValue);
		this.customValidator6.ErrorMessage = "Ingrese Cliente";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator1.ErrorMessage = "codSerie";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese Numeracion";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese Transportista";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ErrorMessage = "Ingrese Vehiculo";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(1190, 558);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVentaxEentregar";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Venta Por Entregar";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmVenta_Load);
		base.Shown += new System.EventHandler(frmVenta_Shown);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
