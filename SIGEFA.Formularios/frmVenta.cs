using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.SunatFacElec;

namespace SIGEFA.Formularios;

public class frmVenta : Office2007Form
{
	private clsAdmSerie AdmSerie = new clsAdmSerie();

	private clsCaja aper = new clsCaja();

	private clsAdmAperturaCierre AdmAper = new clsAdmAperturaCierre();

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

	private clsSeparacion separacion = new clsSeparacion();

	public bool banderagrabada;

	public bool banderaexonerada;

	public bool banderainafecta;

	public bool banderadelete = false;

	public bool bandera = false;

	private clsAdmEmpresa admempre = new clsAdmEmpresa();

	private Facturacion conex = new Facturacion();

	private clsDetalleGuiaRemision detguia = new clsDetalleGuiaRemision();

	private DataTable datosAlmacena = new DataTable();

	private clsTransferencia transfer = new clsTransferencia();

	private clsAdmTransferencia admTransa = new clsAdmTransferencia();

	private clsReporteTransferencias dst = new clsReporteTransferencias();

	public decimal montogratuitas;

	public decimal montogravadas;

	public decimal montoexoneradas = default(decimal);

	public decimal montoinafectas = default(decimal);

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

	public int CodSeparacion = 0;

	private DataTable NuevaTabla = new DataTable();

	public bool consultorext;

	public static BindingSource data = new BindingSource();

	private int CodLista = 0;

	private bool Validacion = true;

	private decimal TipoCambio = default(decimal);

	private decimal ret = default(decimal);

	public int mon = 0;

	private int item = 1;

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

	private List<int> ListaEmpresa = new List<int>();

	private int tipounidad;

	public int impresion;

	private clsAdmEmpresa admempress = new clsAdmEmpresa();

	private clsEmpresa empress = new clsEmpresa();

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmNewConfiguracion newconfig = new clsAdmNewConfiguracion();

	private string tipoimpuesto;

	private int codtipoarticulo;

	private int i = 0;

	private List<clsNotaCredito> ncredito = new List<clsNotaCredito>();

	private clsAdmNotaCredito admNotac = new clsAdmNotaCredito();

	public DateTime fecha1;

	public DateTime fecha2;

	private string anulado = "VENTA ANULADA";

	private string activo_con_ncp = "VENTA CON NOTA DE CREDITO PARCIAL";

	private string activo_con_ncT = "VENTA CON NOTA DE CREDITO TOTAL";

	public bool xgenerar = false;

	private clsTipoDocumento doc2 = new clsTipoDocumento();

	private clsSerie seri2 = new clsSerie();

	private string siglaPago;

	private string seriePago;

	private string numeroPago;

	private int codDocumentoPago = 0;

	private bool rpta;

	private Facturacion facturacion = new Facturacion();

	private clsAdmSeparacion Admsepa = new clsAdmSeparacion();

	internal bool VerBotonDespacho = false;

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

	public DataGridView dgvDetalle;

	public TextBox txtDireccion;

	private TextBox txtCodDocumento;

	private TextBox txtDsctoGobal;

	private Label label21;

	private Button btnImprimir;

	private Button btnNuevaVenta;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	public Button btnEliminar;

	public Button btnGuardar;

	public Button btnNuevo;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CustomValidator customValidator2;

	private CustomValidator customValidator11;

	private CustomValidator customValidator7;

	private CustomValidator customValidator10;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo2;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo3;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo4;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo5;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo6;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo7;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo8;

	public CheckBox chkVentaDsctoGlobal;

	public CheckBox chkVentaGratuita;

	private GroupBox groupBox1;

	private RadioButton rbtnPendiente;

	private Label labelnotacredito;

	public Label label38;

	public TextBox txtcodpedido;

	private Label label37;

	public CheckBox checkBox1;

	private GroupBox groupBox3;

	private TextBox txttasa;

	private Label label30;

	private TextBox txtMoneda;

	private Label label28;

	private TextBox txtplazo;

	private Label lbLineaCredito;

	private TextBox txtLineaCredito;

	private TextBox txtLineaCreditoUso;

	private Label label23;

	private Label label25;

	private TextBox txtLineaCreditoDisponible;

	private ComboBox cmbAlmacen;

	private Label lblAlmacen;

	private TextBox txtDireccionCliente;

	private Label label16;

	private TextBox txtCodigoCli;

	public Label label4;

	public TextBox txtCotizacion;

	public TextBox txtNumero;

	private Label label8;

	public TextBox txtTipoCambio;

	private Label label24;

	private ComboBox cbovendedor;

	private TextBox txtPDescuento;

	private Label label6;

	private DateTimePicker dtpFechaPago;

	public ComboBox cmbFormaPago;

	private Label label3;

	private TextBox txtAutorizacion;

	private TextBox txtSerie;

	public ComboBox cmbMoneda;

	private Label label17;

	private TextBox txtNombreCliente;

	public TextBox txtCodCliente;

	private Label label15;

	private TextBox txtComentario;

	private Label label9;

	private TextBox txtNumDoc;

	private Label label7;

	private TextBox txtDocRef;

	private Label label5;

	private Label lbNombreTransaccion;

	public TextBox txtTransaccion;

	private Label label2;

	public DateTimePicker dtpFecha;

	private Label label1;

	private Label label19;

	private Label label22;

	public Label label26;

	private Label label27;

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

	private DataGridViewTextBoxColumn MiTipoImpuesto;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn stockPend;

	private DataGridViewTextBoxColumn dsctoMax;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private Label lblAnulado;

	private TextBox txticbper;

	private Label label18;

	private TextBox txtRetencion;

	private Label label20;

	private Button btnseparar;

	private Button btnCrearDespacho;

	private Button btnRegenerar;

	private Button btnImprimirPdf;

	private Label lblanulacion;

	public byte[] firmadigital { get; set; }

	public int Tipounidad
	{
		get
		{
			return tipounidad;
		}
		set
		{
			tipounidad = value;
		}
	}

	public string Tipoimpuesto
	{
		get
		{
			return tipoimpuesto;
		}
		set
		{
			tipoimpuesto = value;
		}
	}

	public int Codtipoarticulo
	{
		get
		{
			return codtipoarticulo;
		}
		set
		{
			codtipoarticulo = value;
		}
	}

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

	public frmVenta()
	{
		InitializeComponent();
	}

	public void frmVenta_Load(object sender, EventArgs e)
	{
		iniciaformulario();
		CargaTransportista();
		CargaVehiculoTrasnporte();
		CargaBoleta();
		if (venta != null && venta.CodAlmacen > 0)
		{
			cmbAlmacen.SelectedValue = venta.CodAlmacen;
		}
		int cod = admForm.getPermisoCrearDespachoDesdeVenta();
		btnCrearDespacho.Visible = (frmLogin.AcesosUsuario.Contains(cod) || frmLogin.iNivelUser == 1) && VerBotonDespacho && !lblAnulado.Visible;
		if (!btnCrearDespacho.Visible)
		{
			return;
		}
		int rpta = newconfig.getConfiguracion("DIASCREAR", "DESPACHO");
		if (rpta != -666 && venta != null)
		{
			DateTime fechaPuedeCrear = DateTime.Now.AddDays(-1 * rpta);
			if (venta.FechaRegistro < fechaPuedeCrear)
			{
				btnCrearDespacho.Visible = false;
			}
		}
	}

	public void x()
	{
		try
		{
			guia = AdmGuia.CargaGuiaRemision(Convert.ToInt32(CodGuia));
			if (CodGuia == 0)
			{
			}
			ser = Admser.MuestraSerie(guia.CodSerie, frmLogin.iCodAlmacen);
			if (guia != null)
			{
				txtNumDoc.Text = guia.CodGuiaRemision;
				if (txtCodCliente.Enabled)
				{
					if (guia.CodCliente != 0)
					{
						CodCliente = guia.CodCliente;
						if (guia.RUCCliente != "")
						{
							txtCodCliente.Text = guia.RUCCliente;
						}
						else
						{
							txtCodCliente.Text = guia.DNI;
						}
						txtNombreCliente.Text = guia.RazonSocialCliente;
						txtDireccion.Text = guia.Direccion;
					}
					else
					{
						CodCliente = guia.CodAlmacenDestino;
						txtCodCliente.Text = guia.RUCCliente;
						txtNombreCliente.Text = guia.NomAlmacenDestino;
						txtDireccion.Text = guia.UbicacionAlmacenDest;
					}
				}
				dtpFecha.MaxDate = guia.FechaEmision;
				if (guia.CodPedido != 0)
				{
					pedido = Admped.CargaPedido(Convert.ToInt32(guia.CodPedido));
				}
				txtSerie.Text = guia.Serie;
				txtNumero.Text = guia.NumDoc;
				txtComentario.Text = guia.Comentario;
				datosAlmacena = AdmGuia.CargaFacturasGuia(Convert.ToInt32(guia.CodGuiaRemision), frmLogin.iCodAlmacen);
				if (datosAlmacena == null || datosAlmacena.Rows.Count > 0)
				{
				}
				CargaDetalleGuiaVenta();
				List<clsDetalleGuiaRemision> detguia = listadetalleguiaventa();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	public List<clsDetalleGuiaRemision> listadetalleguia()
	{
		return AdmGuia.listaDetalleGuiaRemision(Convert.ToString(guia.CodGuiaRemision));
	}

	public List<clsDetalleGuiaRemision> listadetalleguiaventa()
	{
		return AdmGuia.listaDetalleGuiaRemisionventa(Convert.ToString(guia.CodGuiaRemision));
	}

	private void CargaBoleta()
	{
		try
		{
			txtTransaccion.Focus();
			txtTransaccion.Text = "FT";
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtTransaccion_KeyPress(txtTransaccion, ee);
			cmbAlmacen.Visible = true;
			cargaAlmacenes();
			btnNuevo.Focus();
			if (Proceso == 1)
			{
				txtDocRef.Text = "BV";
				txtCodCliente.Text = "C000001";
				KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
				txtDocRef_KeyPress(txtDocRef, ee2);
				txtSerie_KeyPress(txtDocRef, ee2);
				BuscaCliente();
				txtCodCliente.Focus();
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
						txtcodpedido.Text = "0";
						txtcodpedido.Enabled = false;
					}
				}
			}
			else if (Proceso == 4)
			{
				Proceso = 1;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmDetalleSalida"] != null)
			{
				Application.OpenForms["frmDetalleSalida"].Activate();
				return;
			}
			frmDetalleSalida form = new frmDetalleSalida();
			form.Procede = 2;
			form.Proceso = 1;
			form.consultorext = checkBox1.Checked;
			if (checkBox1.Checked)
			{
				form.CodVendedor = CodVendedor;
				form.Procede = 42;
				form.Proceso = 1;
				form.consultorext = checkBox1.Checked;
			}
			form.Tipo = 2;
			form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			form.Codlista = 0;
			form.tc = tc.Compra;
			form.productoscargados = detalle1;
			form.alma = Convert.ToInt32(cmbAlmacen.SelectedValue);
			form.cliEspecial = cli.CliEspecial;
			form.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
			calculatotales();
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			DataGridViewRow row = dgvDetalle.SelectedRows[0];
			if (Application.OpenForms["frmDetalleSalida"] != null)
			{
				Application.OpenForms["frmDetalleSalida"].Activate();
				return;
			}
			frmDetalleSalida form = new frmDetalleSalida();
			form.Proceso = 2;
			form.Procede = 2;
			form.alma = Convert.ToInt32(cmbAlmacen.SelectedValue);
			form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			form.tc = Convert.ToDouble(txtTipoCambio.Text);
			form.Codlista = 0;
			form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
			form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
			form.BuscaProducto();
			form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
			form.txtCantidad.Text = $"{row.Cells[cantidad.Name].Value:#,##0.00}";
			form.txtPrecio.Text = $"{row.Cells[preciounit.Name].Value:#,##0.00}";
			form.txtDscto1.Text = $"{row.Cells[dscto1.Name].Value:#,##0.00}";
			form.txtPrecioNeto.Text = $"{row.Cells[importe.Name].Value:#,##0.00}";
			form.ShowDialog();
		}
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
		cbovendedor.ValueMember = "codUsuario";
		cbovendedor.SelectedIndex = 0;
	}

	private void CargaVendedores2()
	{
		cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque2();
		cbovendedor.DisplayMember = "apellido";
		cbovendedor.ValueMember = "codUsuario";
		cbovendedor.SelectedIndex = 0;
	}

	private void CargaListaPrecios(int codForma)
	{
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
				ProcessTabKey(forward: true);
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
		try
		{
			cli = AdmCli.MuestraCliente(CodCliente);
			if (cli != null)
			{
				cli = AdmCli.CargaDeuda(cli);
				ncredito = admNotac.BuscarNotasXCliente(CodCliente);
				if (cli.Cantidad > 0)
				{
					DialogResult dlgResult = MessageBox.Show("El cliente selecionado presenta" + Environment.NewLine + "Facturas pendientes = " + cli.Cantidad + Environment.NewLine + "Deuda Total = " + cli.Deuda + " soles" + Environment.NewLine + "Linea de crédito = " + cli.LineaCredito + Environment.NewLine + " Desea continuar con la venta?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (dlgResult == DialogResult.No)
					{
						ret = 1m;
						txtCotizacion.Text = "";
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
				if (ncredito.Count > 0)
				{
					labelnotacredito.Visible = true;
				}
				else
				{
					labelnotacredito.Visible = false;
				}
			}
			else
			{
				MessageBox.Show("Cliente no identificado, Por favor verifique...!");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		txtCodCliente.Text = cli.RucDni.ToString();
		if (cli.Moneda == 1)
		{
			txtLineaCredito.Text = cli.LineaCredito.ToString();
			txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
			txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
			lbLineaCredito.Text = "Línea de Crédito (S/.):";
			label23.Text = "Línea Disponible (S/.):";
			label25.Text = "Línea C. en Uso (S/.):";
			if (cli.LineaCredito > 0m)
			{
				cmbFormaPago.Enabled = true;
			}
			else
			{
				cmbFormaPago.Enabled = false;
			}
		}
		else
		{
			txtLineaCredito.Text = cli.LineaCredito.ToString();
			txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
			txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
			lbLineaCredito.Text = "Línea de Crédito ($.):";
			label23.Text = "Línea Disponible ($.):";
			label25.Text = "Línea C. en Uso ($.):";
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
		txtPDescuento.Text = cli.Descuento.ToString();
		cmbMoneda.SelectedValue = cli.Moneda;
		mon = cli.Moneda;
		txtMoneda.Text = cmbMoneda.Text;
		txttasa.Text = cli.Tasa.ToString();
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
					txtPDescuento.Text = "";
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
		txtPDescuento.Text = "";
		return false;
	}

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
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
				btnNuevo.Enabled = true;
				ProcessTabKey(forward: true);
				txtDocRef.Focus();
			}
		}
		else if (e.KeyCode == Keys.Return && txtCodCliente.Text != "" && BuscaCliente())
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtCodCliente_Leave(object sender, EventArgs e)
	{
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
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
			txtCodDocumento.Text = CodDocumento.ToString();
			return true;
		}
		CodDocumento = 0;
		txtCodDocumento.Text = CodDocumento.ToString();
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
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 3;
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtCodDocumento.Text = CodDocumento.ToString();
		txtDocRef.Text = doc.Sigla;
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
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
		if (txtAutorizacion.Visible && CodAutorizado == 0)
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
		txtComentario.ReadOnly = estado;
		txtAutorizacion.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnNuevo.Visible = !estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
		cbovendedor.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtSerie.ReadOnly = estado;
		txtCotizacion.Enabled = !estado;
		lblAlmacen.Visible = !estado;
		cmbAlmacen.Visible = !estado;
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
		txtComentario.ReadOnly = estado;
		txtAutorizacion.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnNuevo.Visible = !estado;
		cbovendedor.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtSerie.ReadOnly = estado;
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmVenta.CargaDetalle(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen, 0);
	}

	private void CargaDetalleCotizacion()
	{
		dgvDetalle.DataSource = AdmCoti.CargaDetalle(Convert.ToInt32(coti.CodCotizacion), frmLogin.iCodAlmacen);
	}

	private void CargaDetalleGuia()
	{
		dgvDetalle.DataSource = AdmGuia.CargaDetalle(Convert.ToInt32(guia.CodGuiaRemision));
	}

	private void CargaDetalleGuiaVenta()
	{
		dgvDetalle.DataSource = AdmGuia.CargaDetalleGuiaVenta(Convert.ToInt32(guia.CodGuiaRemision));
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

	private void CargaTransportista()
	{
	}

	private void CargaVehiculoTrasnporte()
	{
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
			sololectura(estado: true);
			groupBox3.Visible = false;
			Button button = btnImprimirPdf;
			bool visible = (btnImprimir.Visible = true);
			button.Visible = visible;
		}
		else if (Proceso == 4)
		{
			txtcodpedido.Text = CodPedido.ToString().PadLeft(11, '0');
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtcodpedido_KeyPress(txtcodpedido, ee);
			Procede = 4;
		}
		else if (Proceso == 5)
		{
			txtcodpedido.Text = CodSeparacion.ToString().PadLeft(11, '0');
			Procede = 7;
			KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
			txtcodpedido_KeyPress(txtcodpedido, ee2);
		}
		txtCodigoCli.Visible = false;
	}

	private void cargaAlmacenes()
	{
		cmbAlmacen.DataSource = Admalmac.listaAlmacenxEmpresa();
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void frmVenta_Shown(object sender, EventArgs e)
	{
	}

	private void CargaVenta()
	{
		try
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodVenta));
			if (venta != null)
			{
				clsAdmDespacho admdesp = new clsAdmDespacho();
				clsDespacho desp = admdesp.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
				btnCrearDespacho.Visible = desp != null;
				if (venta.valorRetencion == 1)
				{
					txtRetencion.Text = Math.Round(0.03m * venta.Total, 2).ToString();
				}
				else
				{
					txtRetencion.Text = "0.00";
				}
				ser = Admser.MuestraSerie(venta.CodSerie, venta.CodAlmacen);
				guia = AdmGuia.CargaGuiaVenta(Convert.ToInt32(CodVenta));
				if (venta.Anulado == 1)
				{
					lblAnulado.Text = anulado;
					lblAnulado.Visible = true;
					lblanulacion.Visible = true;
					lblanulacion.Text = venta.UsuarioAnulador;
				}
				else
				{
					_ = venta.CodNotaCredito;
					if (venta.CodNotaCredito != 0)
					{
						if (venta.TieneNotaCredito == "P")
						{
							lblAnulado.Text = activo_con_ncp;
							lblAnulado.Visible = true;
						}
						else
						{
							lblAnulado.Text = activo_con_ncT;
							lblAnulado.Visible = true;
						}
					}
				}
				txtNumDoc.Text = venta.CodFacturaVenta;
				CodTransaccion = venta.CodTipoTransaccion;
				CargaTransaccion();
				if (txtCodCliente.Enabled)
				{
					CodCliente = venta.CodCliente;
					cli = AdmCli.MuestraCliente(CodCliente);
					txtCodCliente.Text = cli.RucDni;
					txtNombreCliente.Text = cli.RazonSocial;
					txtDireccionCliente.Text = cli.DireccionLegal;
					txtLineaCredito.Text = cli.LineaCredito.ToString();
					txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
					txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
				}
				dtpFecha.Value = venta.FechaSalida;
				cmbMoneda.SelectedValue = venta.Moneda;
				txtTipoCambio.Text = venta.TipoCambio.ToString();
				if (txtAutorizacion.Enabled)
				{
				}
				CodDocumento = venta.CodTipoDocumento;
				txtCodDocumento.Text = CodDocumento.ToString();
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
				if (guia == null || guia.CodFactura == Convert.ToInt32(venta.CodFacturaVenta))
				{
				}
				cmbFormaPago.SelectedValue = venta.FormaPago;
				forma = AdmPago.CargaFormaPago(venta.FormaPago);
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
				dtpFechaPago.Value = venta.FechaPago;
				txtComentario.Text = venta.Comentario;
				txtBruto.Text = $"{venta.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{venta.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{venta.Total - venta.Igv:#,##0.00}";
				txtIGV.Text = $"{venta.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{venta.Total:#,##0.00}";
				txticbper.Text = $"{venta.icbper:#,##0.00}";
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
		ser = Admser.MuestraSerie(codDocumento, venta.CodAlmacen);
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
		if (txtPDescuento.Text != "")
		{
			calculatotales();
			calculadescuentogeneral();
		}
		else
		{
			calculatotales();
		}
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
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtPrecioVenta.Text = $"{preciovent:#,##0.00}";
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private async void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			double totalsoles = 0.0;
			aper = AdmAper.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, frmLogin.iCodAlmacen, frmLogin.iCodUser);
			if (aper != null)
			{
				if (!superValidator1.Validate())
				{
					return;
				}
				if (Convert.ToInt32(cli.Moneda) != Convert.ToInt32(cmbMoneda.SelectedValue))
				{
					if (Convert.ToInt32(cli.Moneda) == 2 || Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
					{
						totalsoles = Convert.ToDouble(txtPrecioVenta.Text) / Convert.ToDouble(txtTipoCambio.Text);
					}
					else if (Convert.ToInt32(cli.Moneda) == 1 || Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
					{
						totalsoles = Convert.ToDouble(txtPrecioVenta.Text) * Convert.ToDouble(txtTipoCambio.Text);
					}
				}
				else
				{
					totalsoles = Convert.ToDouble(txtPrecioVenta.Text);
				}
				if (totalsoles > Convert.ToDouble(txtLineaCreditoDisponible.Text) && Convert.ToInt32(cmbFormaPago.SelectedValue) != 6 && txtCodigoCli.Text != "")
				{
					MessageBox.Show("El Monto Excede a la Línea de Crédito");
				}
				else
				{
					if (Proceso == 0)
					{
						return;
					}
					GeneraListaEnpresas();
					venta.CodSucursal = frmLogin.iCodSucursal;
					venta.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
					venta.CodTipoTransaccion = tran.CodTransaccion;
					if (txtCodigoCli.Text != "")
					{
						venta.CodCliente = Convert.ToInt32(txtCodigoCli.Text);
					}
					else
					{
						venta.CodCliente = 0;
					}
					venta.CodTipoDocumento = doc.CodTipoDocumento;
					venta.Detallecomentario = "";
					venta.CodCotizacion = 0;
					string text = txtDocRef.Text;
					string text2 = text;
					string text3 = text2;
					if (!(text3 == "BV"))
					{
						if (text3 == "FT")
						{
							venta.Boletafactura = 2;
						}
					}
					else
					{
						venta.Boletafactura = 1;
					}
					venta.CodSerie = CodSerie;
					venta.Serie = txtSerie.Text;
					venta.NumDoc = txtNumero.Text;
					venta.Estado = 1;
					venta.Consultorext = false;
					venta.Codsalidaconsulext = 0;
					venta.CodPedido = Convert.ToInt32(pedido.CodPedido);
					venta.Nombre = txtNombreCliente.Text.ToString();
					venta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
					if (txtTipoCambio.Visible)
					{
						venta.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
					}
					venta.FechaSalida = dtpFecha.Value;
					venta.FechaPago = dtpFechaPago.Value;
					venta.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
					venta.CodListaPrecio = 0;
					venta.CodVendedor = Convert.ToInt32(cbovendedor.SelectedValue);
					venta.Comentario = txtComentario.Text;
					venta.CodUser = frmLogin.iCodUser;
					venta.Entregado = Convert.ToInt32(rbtnPendiente.Checked);
					venta.CodSeparacion = Convert.ToInt32(separacion.CodSeparacion);
					venta.CodigoBarras = DateTime.Today.Year.ToString().Substring(2, 2) + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0') + DateTime.Now.ToShortTimeString().Substring(0, 2) + DateTime.Now.ToShortTimeString().Substring(3, 2) + venta.CodSerie.ToString().PadLeft(3, '0') + CodCliente;
					venta.CodigoBarrasCifrado = "00000000";
					pedido.CodigoBarras = venta.CodigoBarras;
					pedido.CodigoBarrasCifrado = venta.CodigoBarrasCifrado;
					if (CodTransaccion == 5)
					{
						venta.TipoDocumentoAnticipo = "";
						venta.DocumentoReferenciaAnticipo = "";
						venta.MontoAnticipo = 0m;
					}
					else
					{
						venta.TipoDocumentoAnticipo = "";
						venta.DocumentoReferenciaAnticipo = "";
						venta.MontoAnticipo = 0m;
					}
					new clsFacturaVenta();
					clsFacturaVenta factura = AdmVenta.FechaCorrelativoAnterior(venta.CodSerie);
					foreach (int lista in ListaEmpresa)
					{
						if (mdi_Menu.MontoTopeBoleta > 0)
						{
							CrearTablaTemporal();
							OrdenarTablaTemporal();
							CreaBoletas(lista);
						}
						else
						{
							ArmaCabecera(lista);
						}
						venta.CodEmpresa = lista;
						if (Proceso != 1)
						{
							continue;
						}
						if (factura.FechaSalida > venta.FechaSalida.Date)
						{
							MessageBox.Show("Error No se puede Registrar los Datos. Verifique Fecha");
							continue;
						}
						using (TransactionScope scope = new TransactionScope())
						{
							bool rpta = AdmVenta.insert(venta);
							if (rpta)
							{
								RecorreDetalle(lista);
								if (detalle1.Count > 0)
								{
									foreach (clsDetalleFacturaVenta det in detalle1)
									{
										rpta = AdmVenta.insertdetalle(det);
										if (!rpta)
										{
											break;
										}
									}
								}
							}
							if (!rpta)
							{
								Transaction.Current.Rollback();
								MessageBox.Show("Los datos no  se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
							scope.Complete();
							MessageBox.Show("Los datos se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
							ltaventa.Add(venta);
						}
						if (ncredito.Count > 0)
						{
							frmCancelarPago form = new frmCancelarPago();
							form.CodNota = venta.CodFacturaVenta;
							form.VentComp = 1;
							form.tipo = 3;
							form.CodCliente = cli.CodCliente;
							form.ShowDialog();
						}
						else
						{
							if (fpago.Dias == 0 && venta.CodTipoTransaccion == 7)
							{
								ingresarpago();
							}
							CodVenta = venta.CodFacturaVenta;
							if (venta.FormaPago != 6)
							{
								Button button = btnImprimirPdf;
								bool visible = (btnImprimir.Visible = true);
								button.Visible = visible;
							}
						}
						if (lista == 1)
						{
							await conex.GeneraDocumento(cli, venta, detalle1, 0);
							firmadigital = conex.LogoEmp;
							fnImprimir();
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Close();
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
		}
	}

	private void RecorreDetalle(int codigo)
	{
		detalle.Clear();
		detalle1.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle(row, codigo);
		}
	}

	private void añadedetalle(DataGridViewRow fila, int cod)
	{
		if (cod == frmLogin.iCodEmpresa)
		{
			clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
			deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
			deta.CodAlmacen = frmLogin.iCodAlmacen;
			deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.SerieLote = "";
			deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
			deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
			deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
			deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
			if (!chkVentaGratuita.Checked && !chkVentaDsctoGlobal.Checked)
			{
				deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
			}
			deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
			deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
			deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
			deta.CodUser = frmLogin.iCodUser;
			deta.CantidadPendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.Moneda = 1;
			deta.Descripcion = fila.Cells[descripcion.Name].Value.ToString();
			deta.CodTipoArticulo = codtipoarticulo;
			if (!chkVentaGratuita.Checked && !chkVentaDsctoGlobal.Checked)
			{
				deta.Tipoimpuesto = fila.Cells[MiTipoImpuesto.Name].Value.ToString();
			}
			else
			{
				deta.Tipoimpuesto = "21";
			}
			deta.Entregado = rbtnPendiente.Checked;
			deta.TipoUnidad = Tipounidad;
			deta.CodDetalleCotizacion = 0;
			deta.CodDetallePedido = 0;
			detalle1.Add(deta);
		}
	}

	private void ImprimeEspecial(int lista)
	{
		if (detalle1.Count > 0)
		{
			foreach (clsDetalleFacturaVenta det in detalle1)
			{
				AdmVenta.insertdetalle(det);
				if (det.CodDetalleVenta == 0)
				{
					MessageBox.Show("Error No se puede Registrar los Datos. Falta Stock de Productos");
					AdmVenta.rollback(Convert.ToInt32(venta.CodFacturaVenta), 0);
					return;
				}
			}
		}
		MessageBox.Show("Los datos se guardaron correctamente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
		ltaventa.Add(venta);
		if (ncredito.Count > 0)
		{
			frmCancelarPago form = new frmCancelarPago();
			form.CodNota = venta.CodFacturaVenta;
			form.VentComp = 1;
			form.tipo = 3;
			form.CodCliente = cli.CodCliente;
			form.ShowDialog();
		}
		else
		{
			if (fpago.Dias == 0 && venta.CodTipoTransaccion == 7)
			{
				ingresarpago();
			}
			CodVenta = venta.CodFacturaVenta;
			if (venta.FormaPago != 6)
			{
				Button button = btnImprimirPdf;
				bool visible = (btnImprimir.Visible = true);
				button.Visible = visible;
			}
		}
		if (lista != 3)
		{
			conex.GeneraDocumento(cli, venta, detalle1, 0);
			firmadigital = conex.LogoEmp;
		}
		fnImprimir();
	}

	private void RecorreDetalleEspecial(int codigo, int codEmpresa)
	{
		detalle.Clear();
		detalle1.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in NuevaTabla.Rows)
		{
			AñadeDetalleEspecial(row, codigo, codEmpresa);
		}
	}

	private void AñadeDetalleEspecial(DataRow row, int codigo, int codEmpresa)
	{
		if (codigo == Convert.ToInt32(row[0]) && codEmpresa == Convert.ToInt32(row[25]))
		{
			clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
			deta.CodProducto = Convert.ToInt32(row[1]);
			deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
			deta.CodAlmacen = Convert.ToInt32(row[22]);
			deta.UnidadIngresada = Convert.ToInt32(row[4]);
			deta.SerieLote = "";
			deta.Cantidad = Convert.ToDecimal(row[6]);
			deta.PrecioUnitario = Convert.ToDecimal(row[7]);
			deta.Subtotal = Convert.ToDecimal(row[8]);
			deta.Descuento1 = Convert.ToDecimal(row[9]);
			deta.MontoDescuento = Convert.ToDecimal(row[12]);
			deta.Igv = Convert.ToDecimal(row[14]);
			deta.Importe = Convert.ToDecimal(row[15]);
			deta.PrecioReal = Convert.ToDecimal(row[17]);
			deta.ValoReal = Convert.ToDecimal(row[16]);
			deta.CodUser = frmLogin.iCodUser;
			deta.CantidadPendiente = Convert.ToDecimal(row[6]);
			deta.Moneda = 1;
			deta.Descripcion = row[3].ToString();
			deta.CodTipoArticulo = Convert.ToInt32(row[20]);
			deta.Tipoimpuesto = row[21].ToString();
			deta.Entregado = rbtnPendiente.Checked;
			deta.TipoUnidad = Convert.ToInt32(row[24]);
			deta.CodDetalleCotizacion = 0;
			deta.CodDetallePedido = 0;
			detalle1.Add(deta);
		}
	}

	private void NuevaCabecera(int codbol)
	{
		ser = AdmSerie.CargaSerieEmpresa(venta.CodEmpresa, doc.CodTipoDocumento);
		venta.CodSerie = ser.CodSerie;
		venta.Serie = ser.Serie;
		venta.NumDoc = ser.Numeracion.ToString().PadLeft(8, '0');
		if (chkVentaGratuita.Checked)
		{
			venta.Tipoventa = 4;
		}
		else if (chkVentaDsctoGlobal.Checked)
		{
			venta.Tipoventa = 5;
		}
		detalle.Clear();
		detalle1.Clear();
		decimal bruto = default(decimal);
		decimal Dscto = default(decimal);
		decimal igv = default(decimal);
		decimal valor = default(decimal);
		montogratuitas = default(decimal);
		montoexoneradas = default(decimal);
		montogravadas = default(decimal);
		montoinafectas = default(decimal);
		banderagrabada = false;
		banderaexonerada = false;
		banderainafecta = false;
		if (NuevaTabla.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in NuevaTabla.Rows)
		{
			if (Convert.ToInt32(row[0]) == codbol && venta.CodEmpresa == Convert.ToInt32(row[25]))
			{
				bruto += Convert.ToDecimal(row[15]);
				Dscto += Convert.ToDecimal(row[12]);
				valor += Convert.ToDecimal(row[13]);
				if (Convert.ToString(row[21]) == "21")
				{
					montogratuitas += Convert.ToDecimal(row[7]) * Convert.ToDecimal(row[6]);
				}
				if (Convert.ToString(row[21]) == "10" || Convert.ToString(row[21]) == "11" || Convert.ToString(row[21]) == "12" || Convert.ToString(row[21]) == "13" || Convert.ToString(row[21]) == "14" || Convert.ToString(row[21]) == "15" || Convert.ToString(row[21]) == "16" || Convert.ToString(row[21]) == "17")
				{
					montogravadas += Convert.ToDecimal(row[13]);
					banderagrabada = true;
				}
				if (Convert.ToString(row[21]) == "20")
				{
					montoexoneradas += Convert.ToDecimal(row[7]) * Convert.ToDecimal(row[6]) - Convert.ToDecimal(row[12]);
					banderaexonerada = true;
				}
				if (Convert.ToString(row[21]) == "30" || Convert.ToString(row[21]) == "31" || Convert.ToString(row[21]) == "32" || Convert.ToString(row[21]) == "33" || Convert.ToString(row[21]) == "34" || Convert.ToString(row[21]) == "35" || Convert.ToString(row[21]) == "36")
				{
					montoinafectas += Convert.ToDecimal(row[7]) * Convert.ToDecimal(row[6]) - Convert.ToDecimal(row[12]);
					banderainafecta = true;
				}
			}
		}
		venta.Gratuitas = montogratuitas;
		venta.Exoneradas = montoexoneradas;
		venta.Gravadas = montogravadas;
		venta.Inafectas = montoinafectas;
		if (chkVentaGratuita.Checked)
		{
			venta.Tipoventa = 4;
		}
		else if (chkVentaDsctoGlobal.Checked)
		{
			venta.Tipoventa = 5;
		}
		else if (banderagrabada && !banderaexonerada && !banderainafecta)
		{
			venta.Tipoventa = 1;
		}
		else if (!banderagrabada && banderaexonerada && !banderainafecta)
		{
			venta.Tipoventa = 2;
		}
		else if (!banderagrabada && !banderaexonerada && banderainafecta)
		{
			venta.Tipoventa = 3;
		}
		else if (banderagrabada && banderaexonerada && !banderainafecta)
		{
			venta.Tipoventa = 6;
		}
		else if (banderagrabada && !banderaexonerada && banderainafecta)
		{
			venta.Tipoventa = 7;
		}
		string montodescuento = $"{Dscto:#,##0.00}";
		string montoBruto = $"{bruto:#,##0.00}";
		string montovv = $"{valor:#,##0.00}";
		string montoigv = $"{bruto - Dscto - valor:#,##0.00}";
		string montotal = $"{bruto - Dscto:#,##0.00}";
		venta.MontoBruto = Convert.ToDecimal(montoBruto);
		venta.MontoDscto = Convert.ToDecimal(montodescuento);
		venta.Igv = Convert.ToDecimal(montoigv);
		venta.Total = Convert.ToDecimal(montotal);
	}

	private void CrearTablaTemporal()
	{
		NuevaTabla = new DataTable("TablaDetalle");
		foreach (DataGridViewColumn column in dgvDetalle.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			NuevaTabla.Columns.Add(dc);
		}
		for (int i = 0; i < dgvDetalle.Rows.Count; i++)
		{
			DataGridViewRow row = dgvDetalle.Rows[i];
			DataRow dr = NuevaTabla.NewRow();
			for (int j = 0; j < dgvDetalle.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			NuevaTabla.Rows.Add(dr);
		}
	}

	private void OrdenarTablaTemporal()
	{
		DataTable t1 = new DataTable();
		t1 = new DataTable("Tabla1");
		foreach (DataGridViewColumn column in dgvDetalle.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			t1.Columns.Add(dc);
		}
		foreach (int cod in ListaEmpresa)
		{
			foreach (DataRow row in NuevaTabla.Rows)
			{
				if (Convert.ToInt32(row[25]) == cod)
				{
					DataRow dr = t1.NewRow();
					for (int i = 0; i < NuevaTabla.Columns.Count; i++)
					{
						dr[i] = row[i];
					}
					t1.Rows.Add(dr);
				}
			}
		}
		NuevaTabla.Clear();
		NuevaTabla = t1;
	}

	private void CreaBoletas(int lista)
	{
		DataTable dt1 = new DataTable();
		decimal bruto = default(decimal);
		decimal Ncantidad = default(decimal);
		decimal Nimporte = default(decimal);
		decimal valorV = default(decimal);
		item = 1;
		foreach (DataGridViewColumn column in dgvDetalle.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt1.Columns.Add(dc);
		}
		foreach (DataRow row in NuevaTabla.Rows)
		{
			if (Convert.ToInt32(row[25]) != lista)
			{
				continue;
			}
			bruto += Convert.ToDecimal(row[15]);
			if (bruto <= (decimal)mdi_Menu.MontoTopeBoleta)
			{
				DataRow dr = dt1.NewRow();
				for (int i = 0; i < NuevaTabla.Columns.Count; i++)
				{
					if (i == 0)
					{
						dr[i] = item;
					}
					else
					{
						dr[i] = row[i];
					}
				}
				dt1.Rows.Add(dr);
				continue;
			}
			bruto -= Convert.ToDecimal(row[15]);
			decimal ValorRestante = (decimal)mdi_Menu.MontoTopeBoleta - bruto;
			decimal cantidad = Math.Truncate(ValorRestante / Convert.ToDecimal(row[7]));
			decimal cantidadRestante = Convert.ToDecimal(row[6]) - cantidad;
			DataRow dr2 = dt1.NewRow();
			valorV = Convert.ToDecimal(row[13]) / Convert.ToDecimal(row[6]);
			bruto += Convert.ToDecimal(row[7]) * cantidad;
			for (int j = 0; j < NuevaTabla.Columns.Count; j++)
			{
				switch (j)
				{
				case 0:
					dr2[j] = item;
					break;
				case 6:
					dr2[j] = cantidad;
					break;
				case 13:
					dr2[j] = Math.Round(cantidad * valorV, 4);
					break;
				case 14:
					dr2[j] = cantidad * Convert.ToDecimal(dr2[7]) - Convert.ToDecimal(dr2[13]);
					break;
				case 15:
					dr2[j] = cantidad * Convert.ToDecimal(dr2[7]);
					break;
				default:
					dr2[j] = row[j];
					break;
				}
			}
			item++;
			bruto = default(decimal);
			dt1.Rows.Add(dr2);
			if (!(cantidadRestante > 0m))
			{
				continue;
			}
			DataRow dr3 = dt1.NewRow();
			bruto += cantidadRestante * Convert.ToDecimal(dr2[7]);
			for (int k = 0; k < NuevaTabla.Columns.Count; k++)
			{
				switch (k)
				{
				case 0:
					dr3[k] = item;
					break;
				case 6:
					dr3[k] = cantidadRestante;
					break;
				case 13:
					dr3[k] = cantidadRestante * valorV;
					break;
				case 14:
					dr3[k] = cantidadRestante * Convert.ToDecimal(dr2[7]) - Convert.ToDecimal(dr3[13]);
					break;
				case 15:
					dr3[k] = cantidadRestante * Convert.ToDecimal(dr2[7]);
					break;
				default:
					dr3[k] = row[k];
					break;
				}
			}
			dt1.Rows.Add(dr3);
		}
		NuevaTabla.Clear();
		NuevaTabla = dt1;
	}

	private void GeneraListaEnpresas()
	{
		bool bandera = false;
		ListaEmpresa.Clear();
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bandera = false;
			if (ListaEmpresa.Count == 0)
			{
				ListaEmpresa.Add(frmLogin.iCodEmpresa);
				continue;
			}
			foreach (int lista in ListaEmpresa)
			{
				if (lista == frmLogin.iCodEmpresa)
				{
					bandera = true;
				}
			}
			if (!bandera)
			{
				ListaEmpresa.Add(frmLogin.iCodEmpresa);
			}
		}
	}

	private void ArmaCabecera(int CodigoE)
	{
		venta.CodSerie = CodSerie;
		venta.Serie = txtSerie.Text;
		venta.NumDoc = txtNumero.Text;
		if (chkVentaGratuita.Checked)
		{
			venta.Tipoventa = 4;
			Tipoimpuesto = "21";
		}
		else if (chkVentaDsctoGlobal.Checked)
		{
			venta.Tipoventa = 5;
			Tipoimpuesto = "0";
		}
		detalle.Clear();
		detalle1.Clear();
		decimal bruto = default(decimal);
		decimal Dscto = default(decimal);
		decimal igv = default(decimal);
		decimal valor = default(decimal);
		string montoigv = "0";
		string montotal = "0";
		montogratuitas = default(decimal);
		montoexoneradas = default(decimal);
		montogravadas = default(decimal);
		montoinafectas = default(decimal);
		banderagrabada = false;
		banderaexonerada = false;
		banderainafecta = false;
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (CodigoE == frmLogin.iCodEmpresa)
			{
				if (Tipoimpuesto != "21")
				{
					Tipoimpuesto = row.Cells[MiTipoImpuesto.Name].Value.ToString();
				}
				bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
				Dscto += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
				valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
				if (Tipoimpuesto == "21")
				{
					montogratuitas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				}
				if (Tipoimpuesto == "10" || Tipoimpuesto == "11" || Tipoimpuesto == "12" || Tipoimpuesto == "13" || Tipoimpuesto == "14" || Tipoimpuesto == "15" || Tipoimpuesto == "16" || Tipoimpuesto == "17")
				{
					montogravadas += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
					banderagrabada = true;
				}
				if (Tipoimpuesto == "20")
				{
					montoexoneradas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value) - Convert.ToDecimal(row.Cells[montodscto.Name].Value);
					banderaexonerada = true;
				}
				if (Tipoimpuesto == "30" || Tipoimpuesto == "31" || Tipoimpuesto == "32" || Tipoimpuesto == "33" || Tipoimpuesto == "34" || Tipoimpuesto == "35" || Tipoimpuesto == "36")
				{
					montoinafectas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value) - Convert.ToDecimal(row.Cells[montodscto.Name].Value);
					banderainafecta = true;
				}
			}
		}
		venta.Gratuitas = montogratuitas;
		venta.Exoneradas = montoexoneradas;
		venta.Gravadas = montogravadas;
		venta.Inafectas = montoinafectas;
		if (banderagrabada && !banderaexonerada && !banderainafecta)
		{
			venta.Tipoventa = 1;
		}
		else if (!banderagrabada && banderaexonerada && !banderainafecta)
		{
			venta.Tipoventa = 2;
		}
		else if (!banderagrabada && !banderaexonerada && banderainafecta)
		{
			venta.Tipoventa = 3;
		}
		else if (banderagrabada && banderaexonerada && !banderainafecta)
		{
			venta.Tipoventa = 6;
		}
		else if (banderagrabada && !banderaexonerada && banderainafecta)
		{
			venta.Tipoventa = 7;
		}
		string montodescuento = $"{Dscto:#,##0.00}";
		string montoBruto = $"{bruto - Dscto:#,##0.00}";
		string montovv = $"{valor:#,##0.00}";
		if (Tipoimpuesto != "21" && Tipoimpuesto != "0")
		{
			montoigv = $"{bruto - Dscto - valor:#,##0.00}";
		}
		montotal = $"{bruto - Dscto:#,##0.00}";
		venta.MontoBruto = Convert.ToDecimal(montoBruto);
		venta.MontoDscto = Convert.ToDecimal(montodescuento);
		venta.Igv = Convert.ToDecimal(montoigv);
		venta.Total = Convert.ToDecimal(montotal);
	}

	private void VerificaSaldoCaja()
	{
		Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, frmLogin.iCodAlmacen, frmLogin.iCodUser);
		CodigoCaja = Caja.Codcaja;
	}

	private void ingresarpago()
	{
		frmCancelarPago form = new frmCancelarPago();
		form.CodNota = venta.CodFacturaVenta;
		form.tipo = 3;
		form.tip = 3;
		form.Monto = venta.Total;
		form.venta = venta;
		form.montoPag = 0;
		form.ShowDialog();
	}

	private void nuevaVenta()
	{
		frmVenta form = new frmVenta();
		form.MdiParent = base.MdiParent;
		form.Proceso = 1;
		form.Show();
		Close();
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
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
		deta.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.CantidadPendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		deta.Entregado = rbtnPendiente.Checked;
		deta.CodDetalleSeparacion = CodSeparacion;
		deta.Tipoimpuesto = "1";
		if (Procede == 3)
		{
			deta.CodDetalleCotizacion = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		}
		else
		{
			deta.CodDetalleCotizacion = 0;
		}
		if (Procede == 4)
		{
			deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		}
		else
		{
			deta.CodDetallePedido = 0;
		}
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

	private void txtPedido_Leave(object sender, EventArgs e)
	{
	}

	private void txtAutorizacion_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmAutorizado"] != null)
		{
			Application.OpenForms["frmAutorizado"].Activate();
			return;
		}
		frmAutorizado form = new frmAutorizado();
		form.Proceso = 3;
		form.ShowDialog();
		aut = form.aut;
		CodAutorizado = aut.CodAutorizado;
		if (CodAutorizado != 0)
		{
			CargaAutorizado();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaAutorizado()
	{
		aut = AdmAut.MuestraAutorizado(CodAutorizado);
		txtAutorizacion.Text = aut.CodAutorizado.ToString();
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

	private void txtComentario_Leave(object sender, EventArgs e)
	{
	}

	public void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
			if (!cli.CliEspecial)
			{
				if (fpago.Dias > forma.Dias)
				{
					DialogResult result = MessageBox.Show("Esta forma de pago excede a la Forma de Pago del Cliente" + Environment.NewLine + "Máx.FormaPago del Cliente = " + forma.Descripcion, "Facturación Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					if (result == DialogResult.OK)
					{
						cmbFormaPago.SelectedValue = forma.CodFormaPago;
					}
				}
			}
			else
			{
				DialogResult result2 = MessageBox.Show("Desea cambiar la forma de pago  del Cliente ?", "Facturación Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				if (result2 != DialogResult.OK)
				{
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtPDescuento_TextChanged(object sender, EventArgs e)
	{
		calculadescuentogeneral();
	}

	private void txtPDescuento_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r')
		{
			calculadescuentogeneral();
		}
	}

	private void calculadescuentogeneral()
	{
	}

	public void fnImprimir()
	{
		try
		{
			impresion = AdmVenta.chekeaImpresion(Convert.ToInt32(venta.CodFacturaVenta));
			empress = admempress.CargaEmpresa(venta.CodEmpresa);
			if (impresion == 0)
			{
				PrintaDocumento();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void PrintaDocumento()
	{
		try
		{
			if (frmLogin.iCodAlmacen == 0)
			{
				return;
			}
			DataSet jes = new DataSet();
			frmRptFactura frm = new frmRptFactura();
			CRFacturaInsumos rpt = new CRFacturaInsumos();
			rpt.Load("CRNotaCreditoVenta.rpt");
			jes = ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = firmadigital;
				}
				if (!mel.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow2 in mel.Rows)
				{
					if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
					{
						changesRow2.RejectChanges();
						changesRow2.ClearErrors();
					}
				}
			}
			rpt.SetDataSource(jes);
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(0, 0, 0, 5));
			rpt.PrintToPrinter(1, collated: false, 1, 1);
			rpt.Close();
			rpt.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public void btnImprimir_Click(object sender, EventArgs e)
	{
		if (venta.Anulado != 1)
		{
			imprime();
		}
		else
		{
			imprimeAnulado();
		}
	}

	public void imprimeAnulado()
	{
		try
		{
			if (venta.CodTipoDocumento == 7)
			{
				fnImprimir();
				return;
			}
			DataSet jes = new DataSet();
			frmRptFactura frm = new frmRptFactura();
			CRFacturaFomatoContinuoAnulado rpt = new CRFacturaFomatoContinuoAnulado();
			CRReporteFacturaAnulado rpt2 = new CRReporteFacturaAnulado();
			jes = ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
			string nombrearchivo = "";
			venta = AdmVenta.BuscaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta), venta.CodAlmacen);
			clsEmpresa empre = new clsEmpresa();
			empre = admempre.CargaEmpresa(venta.CodEmpresa);
			if (venta.CodTipoDocumento == 1)
			{
				nombrearchivo = empre.Ruc + "-03-B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
			}
			else if (venta.CodTipoDocumento == 2)
			{
				nombrearchivo = empre.Ruc + "-01-F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
			}
			firmadigital = CargarImagen("C:\\DOCUMENTOS-" + empre.Ruc + "\\CERTIFIK\\QR\\" + nombrearchivo + ".jpeg");
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = firmadigital;
				}
				if (!mel.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow2 in mel.Rows)
				{
					if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
					{
						changesRow2.RejectChanges();
						changesRow2.ClearErrors();
					}
				}
			}
			rpt.SetDataSource(jes);
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(50, 5, 0, 5));
			rpt.PrintToPrinter(1, collated: false, 1, 1);
			string RutaArch = "";
			string RutaXML = "";
			if (venta.CodTipoDocumento == 1)
			{
				RutaArch = "C:\\DOCUMENTOS-" + empre.Ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + nombrearchivo + ".xml";
				RutaXML = Program.CarpetaBoletas + "\\" + nombrearchivo + ".xml";
			}
			else
			{
				RutaArch = "C:\\DOCUMENTOS-" + empre.Ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + nombrearchivo + ".xml";
				RutaXML = Program.CarpetaFacturas + "\\" + nombrearchivo + ".xml";
			}
			rpt2.SetDataSource(jes);
			rpt2.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
			rpt2.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
			rpt2.Close();
			rpt2.Dispose();
			rpt.Close();
			rpt.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public void imprime()
	{
		try
		{
			if (venta.CodTipoDocumento == 7)
			{
				fnImprimir();
				return;
			}
			int formapago1 = int.Parse(cmbFormaPago.SelectedValue.ToString());
			DataSet jes = new DataSet();
			frmRptFactura frm = new frmRptFactura();
			if (formapago1 == 5 || formapago1 == 6)
			{
				CRFacturaFomatoContinuocont rpt1 = new CRFacturaFomatoContinuocont();
				jes = ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
				string nombrearchivo = "";
				venta = AdmVenta.BuscaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta), venta.CodAlmacen);
				clsEmpresa empre = new clsEmpresa();
				empre = admempre.CargaEmpresa(venta.CodEmpresa);
				if (venta.CodTipoDocumento == 1)
				{
					nombrearchivo = empre.Ruc + "-03-B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				}
				else if (venta.CodTipoDocumento == 2)
				{
					nombrearchivo = empre.Ruc + "-01-F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				}
				firmadigital = CargarImagen("C:\\DOCUMENTOS-" + empre.Ruc + "\\CERTIFIK\\QR\\" + nombrearchivo + ".jpeg");
				foreach (DataTable mel in jes.Tables)
				{
					foreach (DataRow changesRow in mel.Rows)
					{
						changesRow["firma"] = firmadigital;
					}
					if (!mel.HasErrors)
					{
						continue;
					}
					foreach (DataRow changesRow2 in mel.Rows)
					{
						if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
						{
							changesRow2.RejectChanges();
							changesRow2.ClearErrors();
						}
					}
				}
				rpt1.SetDataSource(jes);
				PrintOptions rptoption = rpt1.PrintOptions;
				rptoption.PrinterName = ser.NombreImpresora;
				rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rptoption.ApplyPageMargins(new PageMargins(0, 0, 0, 5));
				rpt1.PrintToPrinter(1, collated: false, 1, 1);
				rpt1.Close();
				rpt1.Dispose();
				return;
			}
			CRFacturaFomatoContinuo rpt2 = new CRFacturaFomatoContinuo();
			jes = ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta));
			string nombrearchivo2 = "";
			venta = AdmVenta.BuscaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta), venta.CodAlmacen);
			clsEmpresa empre2 = new clsEmpresa();
			empre2 = admempre.CargaEmpresa(venta.CodEmpresa);
			if (venta.CodTipoDocumento == 1)
			{
				nombrearchivo2 = empre2.Ruc + "-03-B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
			}
			else if (venta.CodTipoDocumento == 2)
			{
				nombrearchivo2 = empre2.Ruc + "-01-F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
			}
			firmadigital = CargarImagen("C:\\DOCUMENTOS-" + empre2.Ruc + "\\CERTIFIK\\QR\\" + nombrearchivo2 + ".jpeg");
			foreach (DataTable mel2 in jes.Tables)
			{
				foreach (DataRow changesRow3 in mel2.Rows)
				{
					changesRow3["firma"] = firmadigital;
				}
				if (!mel2.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow4 in mel2.Rows)
				{
					if ((int)changesRow4["Item", DataRowVersion.Current] > 100)
					{
						changesRow4.RejectChanges();
						changesRow4.ClearErrors();
					}
				}
			}
			rpt2.SetDataSource(jes);
			PrintOptions rptoption2 = rpt2.PrintOptions;
			rptoption2.PrinterName = ser.NombreImpresora;
			rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption2.ApplyPageMargins(new PageMargins(0, 0, 0, 5));
			rpt2.PrintToPrinter(1, collated: false, 1, 1);
			rpt2.Close();
			rpt2.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public static byte[] CargarImagen(string rutaArchivo)
	{
		if (rutaArchivo != "")
		{
			try
			{
				FileStream Archivo = new FileStream(rutaArchivo, FileMode.Open);
				BinaryReader binRead = new BinaryReader(Archivo);
				byte[] imagenEnBytes = new byte[Archivo.Length];
				binRead.Read(imagenEnBytes, 0, (int)Archivo.Length);
				binRead.Close();
				Archivo.Close();
				return imagenEnBytes;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return new byte[0];
			}
		}
		return new byte[0];
	}

	private void printaRecibo(string CodPago)
	{
		try
		{
			CRImpresionPago rpt = new CRImpresionPago();
			frmRptImpresionPago frm = new frmRptImpresionPago();
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rpt.SetDataSource(dsf.ReporteImpresionPago(Convert.ToInt32(CodPago), frmLogin.iCodAlmacen));
			frm.cRVImpresionPago.ReportSource = rpt;
			frm.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
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

	private void printaFactura()
	{
		try
		{
			if (frmLogin.iCodAlmacen != 0)
			{
				ser = Admser.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
				ReportDocument rd = new ReportDocument();
				CRFacturaFomatoContinuo rpt = new CRFacturaFomatoContinuo();
				rd.Load("CRFacturaFomatoContinuo.rpt");
				rd.SetDataSource(ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta)));
				PrintOptions rptoption = rd.PrintOptions;
				rptoption.PrinterName = ser.NombreImpresora;
				rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rd.PrintToPrinter(1, collated: false, 1, 1);
				if (AdmVenta.ActualizaEstadoImpreso(Convert.ToInt32(venta.CodFacturaVenta)))
				{
					rpta = true;
				}
				else
				{
					rpta = false;
				}
				rd.Close();
				rd.Dispose();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void printaBoleta()
	{
		try
		{
			if (ser.CodDocumento == 26)
			{
				ser = Admser.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
				ReportDocument rd = new ReportDocument();
				CRFacturaFomatoContinuo rpt = new CRFacturaFomatoContinuo();
				rd.Load("CRFacturaFomatoContinuo.rpt");
				rd.SetDataSource(ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta)));
				PrintOptions rptoption = rd.PrintOptions;
				rptoption.PrinterName = ser.NombreImpresora;
				rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rptoption.ApplyPageMargins(new PageMargins(0, 0, 0, 5));
				rd.PrintToPrinter(1, collated: false, 1, 1);
				rd.Close();
				rd.Dispose();
			}
			else if (ser.CodDocumento == 1)
			{
				ser = Admser.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
				ReportDocument rd2 = new ReportDocument();
				CRFacturaFomatoContinuo rpt2 = new CRFacturaFomatoContinuo();
				rd2.Load("CRFacturaFomatoContinuo.rpt");
				rd2.SetDataSource(ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta)));
				PrintOptions rptoption2 = rd2.PrintOptions;
				rptoption2.PrinterName = ser.NombreImpresora;
				rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rptoption2.ApplyPageMargins(new PageMargins(0, 0, 0, 5));
				rd2.PrintToPrinter(1, collated: false, 1, 1);
				if (AdmVenta.ActualizaEstadoImpreso(Convert.ToInt32(venta.CodFacturaVenta)))
				{
					rpta = true;
				}
				else
				{
					rpta = false;
				}
				rd2.Close();
				rd2.Dispose();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

	public void btnNuevaVenta_Click(object sender, EventArgs e)
	{
		frmVenta form2 = new frmVenta();
		form2.MdiParent = base.MdiParent;
		form2.consultorext = consultorext;
		form2.Proceso = 1;
		form2.Show();
		Close();
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Procede != 1 || Proceso != 1)
		{
			return;
		}
		if (txtPDescuento.Text != "")
		{
			calculatotales();
			calculadescuentogeneral();
		}
		else
		{
			calculatotales();
		}
		if (dgvDetalle.RowCount <= 0)
		{
			return;
		}
		int Indice = 0;
		Indice = dgvDetalle.RowCount - 1;
		if (cmbMoneda.SelectedIndex == 0)
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
		else if (cmbMoneda.SelectedIndex != 1)
		{
		}
	}

	private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if ((Procede == 1 || Procede == 2) && dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa" && (Proceso == 1 || Proceso == 2))
		{
			if (txtPDescuento.Text != "")
			{
				calculatotales();
				calculadescuentogeneral();
			}
			else
			{
				calculatotales();
			}
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
		txtComentario.Focus();
	}

	private void txtGuias_KeyDown(object sender, KeyEventArgs e)
	{
	}

	public void txtCotizacion_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtCotizacion.Text != "")
		{
			if (BuscaCotizacion())
			{
				CargaCotizacion();
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Cotizacion no existe o ya no esta vigente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				ext.limpiar(base.Controls);
				cargaAlmacenes();
			}
		}
	}

	private bool BuscaCotizacion()
	{
		coti = AdmCoti.BuscaCotizacion(txtCotizacion.Text, frmLogin.iCodAlmacen);
		if (coti != null)
		{
			codCotizacion = Convert.ToInt32(coti.CodCotizacion);
			return true;
		}
		codCotizacion = 0;
		return false;
	}

	private void CargaCotizacion()
	{
		try
		{
			coti = AdmCoti.CargaCotizacion(Convert.ToInt32(codCotizacion), frmLogin.iCodAlmacen);
			if (coti != null)
			{
				txtCotizacion.Text = coti.CodCotizacion;
				if (!txtCodCliente.Enabled)
				{
					return;
				}
				CodCliente = coti.CodCliente;
				CargaCliente();
				if (ret == 0m)
				{
					txtCodigoCli.Text = coti.CodCliente.ToString();
					txtCodCliente.Text = coti.CodigoPersonalizado;
					txtNombreCliente.Text = coti.Nombre;
					txtDireccionCliente.Text = coti.Direccion;
					txtLineaCredito.Text = cli.LineaCredito.ToString();
					txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
					txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
					if (coti.RUCCliente != "")
					{
						txtDocRef.Text = "FT";
						KeyPressEventArgs ee = new KeyPressEventArgs('\r');
						txtDocRef_KeyPress(txtDocRef, ee);
						txtSerie_KeyPress(txtDocRef, ee);
					}
					else
					{
						txtDocRef.Text = "BV";
						KeyPressEventArgs ee2 = new KeyPressEventArgs('\r');
						txtDocRef_KeyPress(txtDocRef, ee2);
						txtSerie_KeyPress(txtDocRef, ee2);
					}
					cmbMoneda.SelectedValue = coti.Moneda;
					txtTipoCambio.Text = coti.TipoCambio.ToString();
					txtComentario.Text = coti.Comentario;
					txtBruto.Text = $"{coti.MontoBruto:#,##0.00}";
					txtDscto.Text = $"{coti.MontoDscto:#,##0.00}";
					txtValorVenta.Text = $"{coti.Total - coti.Igv:#,##0.00}";
					txtIGV.Text = $"{coti.Igv:#,##0.00}";
					txtPrecioVenta.Text = $"{coti.Total:#,##0.00}";
					CargaDetalleCotizacion();
					BloquearEdicion(estado: true);
				}
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

	private void txtCotizacion_Leave(object sender, EventArgs e)
	{
	}

	public void txtGuias_KeyPress(object sender, KeyPressEventArgs e)
	{
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

	private void cmbAlmacen_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cmbAlmacen.Enabled = false;
		btnNuevo.Focus();
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		cmbAlmacen_SelectionChangeCommitted(sender, e);
	}

	private void ckbguia_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void txtRazonSocialTransporte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
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
		if (CodEmpresaTransporte != 0)
		{
			CargaEmpresaTransporte();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaEmpresaTransporte()
	{
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
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

	private void cmbFormaPago_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void cbListaPrecios_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cbovendedor.Focus();
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

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void button1_Click_1(object sender, EventArgs e)
	{
		frmVentadetalle formv = new frmVentadetalle();
		formv.MdiParent = base.MdiParent;
		formv.CodVenta = venta.CodFacturaVenta;
		formv.Proceso = 1;
		formv.Show();
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void btnCrearDespacho_Click(object sender, EventArgs e)
	{
		if (venta != null)
		{
			if (venta.CodFacturaVenta != null)
			{
				frmDespacho form = new frmDespacho();
				form.MdiParent = base.MdiParent;
				form.Dock = DockStyle.Fill;
				form.WindowState = FormWindowState.Maximized;
				form.Proceso = 3;
				form.codFacturaVenta = venta.CodFacturaVenta;
				int codPermiso = admForm.getPermisoCrearDespachoDesdeVenta();
				frmAutorizacion frm = new frmAutorizacion();
				frm.tipoAccion = 2;
				frm.permiso = codPermiso;
				frm.PermitirAdministradores = true;
				frm.tipoVentanaAAsignarUsuario = 3;
				frm.ventanaDespacho = form;
				DialogResult dr = frm.ShowDialog();
				if (dr == DialogResult.OK && form.usuario_click != null)
				{
					form.Show();
				}
			}
		}
		else
		{
			MessageBox.Show("No se pudo identificar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cmbMoneda_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			txtComentario.Focus();
		}
	}

	private async void btnRegenerar_Click(object sender, EventArgs e)
	{
		RecorreDetalle();
		await facturacion.GeneraDocumento(cli, venta, detalle1, 0);
	}

	private void btnImprimirPdf_Click(object sender, EventArgs e)
	{
		try
		{
			if (int.TryParse(venta.CodFacturaVenta, out var codigo))
			{
				frmRptFactura form = new frmRptFactura();
				DataSet jes = new DataSet();
				clsTipoDocumento tipodocumento = Admdoc.CargaTipoDocumento(venta.CodTipoDocumento);
				clsEmpresa empresa = admempre.CargaEmpresa(venta.CodEmpresa);
				string ruc = empresa.Ruc;
				int formpagofact = venta.FormaPago;
				string codDocumento = tipodocumento.Tipodoccodsunat.ToString();
				string iddocumento = "";
				string text = codDocumento;
				string text2 = text;
				if (!(text2 == "03"))
				{
					if (text2 == "01")
					{
						iddocumento = "F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
					}
				}
				else
				{
					iddocumento = "B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				}
				string RutaArch = "";
				string RutaXML = "";
				if (codDocumento == "01")
				{
					RutaArch = "C:\\DOCUMENTOS-" + ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + ruc + "-" + codDocumento + "-" + iddocumento + ".xml";
					RutaXML = Program.CarpetaFacturas + "\\" + ruc + "-" + codDocumento + "-" + iddocumento + ".xml";
				}
				else
				{
					RutaArch = "C:\\DOCUMENTOS-" + ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + ruc + "-" + codDocumento + "-" + iddocumento + ".xml";
					RutaXML = Program.CarpetaBoletas + "\\" + ruc + "-" + codDocumento + "-" + iddocumento + ".xml";
				}
				byte[] LogoEmp = Facturacion.CargarImagen("C:\\DOCUMENTOS-" + ruc + "\\CERTIFIK\\QR\\" + ruc + "-" + codDocumento + "-" + iddocumento + ".jpeg");
				if (formpagofact == 5 || formpagofact == 6)
				{
					CRReporteFacturacont rpt = new CRReporteFacturacont();
					jes = ds.ReporteFactura2(Convert.ToInt32(codigo));
					foreach (DataTable mel in jes.Tables)
					{
						foreach (DataRow changesRow in mel.Rows)
						{
							changesRow["firma"] = LogoEmp;
						}
						if (!mel.HasErrors)
						{
							continue;
						}
						foreach (DataRow changesRow2 in mel.Rows)
						{
							if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
							{
								changesRow2.RejectChanges();
								changesRow2.ClearErrors();
							}
						}
					}
					rpt.SetDataSource(jes);
					rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
					rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
					rpt.Close();
					rpt.Dispose();
				}
				else
				{
					CRReporteFactura rpt2 = new CRReporteFactura();
					jes = ds.ReporteFactura2(Convert.ToInt32(codigo));
					foreach (DataTable mel2 in jes.Tables)
					{
						foreach (DataRow changesRow3 in mel2.Rows)
						{
							changesRow3["firma"] = LogoEmp;
						}
						if (!mel2.HasErrors)
						{
							continue;
						}
						foreach (DataRow changesRow4 in mel2.Rows)
						{
							if ((int)changesRow4["Item", DataRowVersion.Current] > 100)
							{
								changesRow4.RejectChanges();
								changesRow4.ClearErrors();
							}
						}
					}
					rpt2.SetDataSource(jes);
					rpt2.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
					rpt2.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
					rpt2.Close();
					rpt2.Dispose();
				}
				Process p = new Process();
				p.StartInfo.FileName = RutaArch.Replace(".xml", ".pdf");
				p.Start();
				return;
			}
			throw new Exception("No se encontro un codigo de factura numerico");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtComentario_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmbAlmacen.Focus();
		}
	}

	private void cmbAlmacen_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			btnNuevo.Focus();
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox1.Checked)
		{
			checkBox1.Text = "ConsultorExterno";
		}
		else
		{
			checkBox1.Text = "Venta Normal";
		}
	}

	public void txtcodpedido_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r' || !(txtcodpedido.Text != ""))
		{
			return;
		}
		if (Procede == 4)
		{
			if (BuscaPedido())
			{
				if (pedido.Pendiente == 1)
				{
					CargaPedido();
					btnGuardar.Focus();
				}
				else
				{
					MessageBox.Show("Pedido ya esta facturado, ingresar datos correctamente!", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					LimpiarPedido();
				}
			}
			else
			{
				MessageBox.Show("Pedido no existe, ingresar datos correctamente!", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			if (Procede != 7)
			{
				return;
			}
			if (BuscarSeparacion())
			{
				if (separacion.Pendiente == 0)
				{
					CargaSeparacion();
					btnGuardar.Focus();
				}
				else
				{
					MessageBox.Show("Todavia no se Cancela!", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					LimpiarPedido();
				}
			}
			else
			{
				MessageBox.Show("no existe, ingresar datos correctamente!", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void CargaSeparacion()
	{
		try
		{
			separacion = Admsepa.BuscarSeparacion(CodSeparacion, frmLogin.iCodAlmacen);
			if (separacion != null && txtCodCliente.Enabled)
			{
				CodCliente = separacion.CodCliente;
				if (CodCliente > 0)
				{
					CargaCliente();
				}
				cmbMoneda.SelectedValue = separacion.Moneda;
				txtCodigoCli.Text = separacion.CodCliente.ToString();
				txtTipoCambio.Text = separacion.TipoCambio.ToString();
				txtComentario.Text = separacion.Comentario;
				txtBruto.Text = $"{separacion.Bruto:#,##0.00}";
				txtDscto.Text = $"{separacion.MontoDescuento:#,##0.00}";
				txtValorVenta.Text = $"{separacion.Total - separacion.Igv:#,##0.00}";
				txtIGV.Text = $"{separacion.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{separacion.Total:#,##0.00}";
				CargaDetalleSeparacion();
				btnGuardar.Focus();
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleSeparacion()
	{
		try
		{
			DataTable newDataDetalle = new DataTable();
			dgvDetalle.Rows.Clear();
			newDataDetalle = Admsepa.CargaDetalle(Convert.ToInt32(separacion.CodSeparacion));
			foreach (DataRow row in newDataDetalle.Rows)
			{
				dgvDetalle.Rows.Add(row[0].ToString(), "0", row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[18].ToString(), row[17].ToString(), "0", "0", row[22].ToString(), DateTime.Now);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private bool BuscarSeparacion()
	{
		separacion = Admsepa.BuscarSeparacionXid(Convert.ToInt32(txtcodpedido.Text), frmLogin.iCodAlmacen);
		if (separacion != null)
		{
			CodSeparacion = Convert.ToInt32(separacion.CodSeparacion);
			return true;
		}
		CodSeparacion = 0;
		return false;
	}

	private bool BuscaPedido()
	{
		pedido = Admped.BuscaPedido(txtcodpedido.Text, frmLogin.iCodAlmacen);
		if (pedido != null)
		{
			CodPedido = Convert.ToInt32(pedido.CodPedido);
			return true;
		}
		CodPedido = 0;
		return false;
	}

	private void CargaPedido()
	{
		try
		{
			pedido = Admped.CargaPedido(Convert.ToInt32(CodPedido));
			if (pedido != null)
			{
				if (txtCodCliente.Enabled)
				{
					CodCliente = pedido.CodCliente;
					if (CodCliente > 0)
					{
						CargaCliente();
					}
				}
				dtpFecha.Value = pedido.FechaPedido;
				cmbMoneda.SelectedValue = pedido.Moneda;
				txtCodigoCli.Text = pedido.CodCliente.ToString();
				txtTipoCambio.Text = pedido.TipoCambio.ToString();
				if (txtAutorizacion.Enabled)
				{
					txtAutorizacion.Text = pedido.CodAutorizado.ToString();
				}
				if (txtDocRef.Enabled)
				{
					txtDocRef.Focus();
				}
				txtComentario.Text = pedido.Comentario;
				txtBruto.Text = $"{pedido.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{pedido.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{pedido.Total - pedido.Igv:#,##0.00}";
				txtIGV.Text = $"{pedido.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{pedido.Total:#,##0.00}";
				CargaDetallePedido();
				btnGuardar.Focus();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaDetallePedido()
	{
		try
		{
			DataTable newDataDetalle = new DataTable();
			dgvDetalle.Rows.Clear();
			newDataDetalle = Admped.CargaDetalle(Convert.ToInt32(pedido.CodPedido));
			foreach (DataRow row in newDataDetalle.Rows)
			{
				dgvDetalle.Rows.Add(row[0].ToString(), "0", row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[18].ToString(), row[17].ToString(), "0", "0", row[22].ToString(), DateTime.Now);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void LimpiarPedido()
	{
		if (dgvDetalle.RowCount > 0)
		{
			DataTable dt1 = (DataTable)dgvDetalle.DataSource;
			dt1.Clear();
		}
		txtCodCliente.Text = "";
		CodCliente = 0;
		CodPedido = 0;
		txtNombreCliente.Text = "";
		txtTransaccion.Text = "";
		txtDocRef.Text = "";
		txtSerie.Text = "";
		txtNumero.Text = "";
		txtcodpedido.Text = "";
		txtcodpedido.Focus();
		if (cmbFormaPago.Items.Count > 0)
		{
			cmbFormaPago.SelectedIndex = 0;
		}
	}

	private void chkVentaGratuita_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaGratuita.Checked)
		{
			chkVentaDsctoGlobal.Checked = false;
		}
	}

	private void chkVentaDsctoGlobal_CheckedChanged(object sender, EventArgs e)
	{
		if (chkVentaDsctoGlobal.Checked)
		{
			chkVentaGratuita.Checked = false;
		}
	}

	private void groupBox3_Enter(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVenta));
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtRetencion = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.txticbper = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
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
		this.MiTipoImpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockPend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dsctoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnEliminar = new System.Windows.Forms.Button();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.btnNuevaVenta = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.txtCodDocumento = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.customValidator10 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.customValidator11 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.chkVentaGratuita = new System.Windows.Forms.CheckBox();
		this.chkVentaDsctoGlobal = new System.Windows.Forms.CheckBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.txtAutorizacion = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.label6 = new System.Windows.Forms.Label();
		this.txtPDescuento = new System.Windows.Forms.TextBox();
		this.cbovendedor = new System.Windows.Forms.ComboBox();
		this.label24 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtCotizacion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtCodigoCli = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.lblAlmacen = new System.Windows.Forms.Label();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txttasa = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.txtMoneda = new System.Windows.Forms.TextBox();
		this.label28 = new System.Windows.Forms.Label();
		this.txtplazo = new System.Windows.Forms.TextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.lbLineaCredito = new System.Windows.Forms.Label();
		this.txtLineaCredito = new System.Windows.Forms.TextBox();
		this.txtLineaCreditoUso = new System.Windows.Forms.TextBox();
		this.label23 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.txtLineaCreditoDisponible = new System.Windows.Forms.TextBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.label37 = new System.Windows.Forms.Label();
		this.txtcodpedido = new System.Windows.Forms.TextBox();
		this.label38 = new System.Windows.Forms.Label();
		this.labelnotacredito = new System.Windows.Forms.Label();
		this.rbtnPendiente = new System.Windows.Forms.RadioButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnImprimirPdf = new System.Windows.Forms.Button();
		this.btnRegenerar = new System.Windows.Forms.Button();
		this.btnCrearDespacho = new System.Windows.Forms.Button();
		this.btnseparar = new System.Windows.Forms.Button();
		this.lblAnulado = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo2 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo3 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo4 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo5 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo6 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo7 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo8 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.lblanulacion = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.txtRetencion);
		this.groupBox2.Controls.Add(this.label20);
		this.groupBox2.Controls.Add(this.txticbper);
		this.groupBox2.Controls.Add(this.label18);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.txtDsctoGobal);
		this.groupBox2.Controls.Add(this.label21);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Controls.Add(this.btnNuevo);
		this.groupBox2.Controls.Add(this.btnEliminar);
		this.groupBox2.Controls.Add(this.txtBruto);
		this.groupBox2.Controls.Add(this.txtDscto);
		this.groupBox2.Controls.Add(this.txtValorVenta);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Location = new System.Drawing.Point(0, 4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1427, 338);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.txtRetencion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtRetencion.BackColor = System.Drawing.Color.White;
		this.txtRetencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtRetencion.Location = new System.Drawing.Point(1203, 310);
		this.txtRetencion.Name = "txtRetencion";
		this.txtRetencion.ReadOnly = true;
		this.txtRetencion.Size = new System.Drawing.Size(78, 18);
		this.txtRetencion.TabIndex = 33;
		this.txtRetencion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(1129, 313);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(71, 12);
		this.label20.TabIndex = 32;
		this.label20.Text = "Retencion (3%):";
		this.txticbper.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txticbper.BackColor = System.Drawing.Color.White;
		this.txticbper.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txticbper.Location = new System.Drawing.Point(1036, 310);
		this.txticbper.Name = "txticbper";
		this.txticbper.ReadOnly = true;
		this.txticbper.Size = new System.Drawing.Size(78, 18);
		this.txticbper.TabIndex = 31;
		this.txticbper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(987, 313);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(43, 12);
		this.label18.TabIndex = 30;
		this.label18.Text = "ICBPER:";
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(2, -2);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(66, 20);
		this.label19.TabIndex = 29;
		this.label19.Text = "Detalle";
		this.txtDsctoGobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDsctoGobal.BackColor = System.Drawing.Color.White;
		this.txtDsctoGobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDsctoGobal.Location = new System.Drawing.Point(758, 310);
		this.txtDsctoGobal.Name = "txtDsctoGobal";
		this.txtDsctoGobal.ReadOnly = true;
		this.txtDsctoGobal.Size = new System.Drawing.Size(75, 18);
		this.txtDsctoGobal.TabIndex = 25;
		this.txtDsctoGobal.Tag = "7";
		this.txtDsctoGobal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(684, 313);
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
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codSalida1, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.MiTipoImpuesto, this.precioreal, this.valoreal, this.stockPend, this.dsctoMax, this.coduser, this.fecharegistro);
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle12;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 19);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
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
		this.dgvDetalle.Size = new System.Drawing.Size(1421, 277);
		this.dgvDetalle.TabIndex = 2;
		this.superValidator1.SetValidator1(this.dgvDetalle, this.customValidator5);
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codSalida1.HeaderText = "CodSalida";
		this.codSalida1.Name = "codSalida1";
		this.codSalida1.ReadOnly = true;
		this.codSalida1.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
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
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 75;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle14;
		this.cantidad.HeaderText = "Cantidad";
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
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 75;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle16;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Width = 85;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		dataGridViewCellStyle17.NullValue = null;
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle17;
		this.dscto1.HeaderText = "Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle18;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle19.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle19;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
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
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Width = 80;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle21.Format = "N2";
		dataGridViewCellStyle21.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle21;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Width = 85;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle22.Format = "N2";
		dataGridViewCellStyle22.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle22;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle23.Format = "N2";
		dataGridViewCellStyle23.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle23;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Width = 85;
		this.MiTipoImpuesto.DataPropertyName = "tipoimpuesto";
		this.MiTipoImpuesto.HeaderText = "TIPOIMPUESTO";
		this.MiTipoImpuesto.Name = "MiTipoImpuesto";
		this.MiTipoImpuesto.ReadOnly = true;
		this.precioreal.DataPropertyName = "precioreal";
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.ReadOnly = true;
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.stockPend.DataPropertyName = "stockdisponible";
		this.stockPend.HeaderText = "StockDisponible";
		this.stockPend.Name = "stockPend";
		this.stockPend.ReadOnly = true;
		this.stockPend.Visible = false;
		this.dsctoMax.DataPropertyName = "maxPorcDescto";
		this.dsctoMax.HeaderText = "%DsctoMax";
		this.dsctoMax.Name = "dsctoMax";
		this.dsctoMax.ReadOnly = true;
		this.dsctoMax.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(8, 300);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(84, 32);
		this.btnNuevo.TabIndex = 20;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(119, 302);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(87, 32);
		this.btnEliminar.TabIndex = 22;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.BackColor = System.Drawing.Color.White;
		this.txtBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtBruto.Location = new System.Drawing.Point(891, 310);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 18);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.BackColor = System.Drawing.Color.White;
		this.txtDscto.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDscto.Location = new System.Drawing.Point(596, 311);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 18);
		this.txtDscto.TabIndex = 24;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.BackColor = System.Drawing.Color.White;
		this.txtValorVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtValorVenta.Location = new System.Drawing.Point(1343, 307);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(78, 18);
		this.txtValorVenta.TabIndex = 26;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(1289, 313);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(47, 12);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(536, 313);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(55, 12);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(849, 313);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(32, 12);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.BackColor = System.Drawing.Color.White;
		this.txtPrecioVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVenta.Location = new System.Drawing.Point(1286, 27);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(78, 18);
		this.txtPrecioVenta.TabIndex = 28;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.Location = new System.Drawing.Point(1233, 30);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(46, 12);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.BackColor = System.Drawing.Color.White;
		this.txtIGV.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtIGV.Location = new System.Drawing.Point(1286, 3);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(78, 18);
		this.txtIGV.TabIndex = 27;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(1243, 6);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(36, 12);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.btnNuevaVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnNuevaVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnNuevaVenta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevaVenta.ImageIndex = 1;
		this.btnNuevaVenta.ImageList = this.imageList1;
		this.btnNuevaVenta.Location = new System.Drawing.Point(540, 205);
		this.btnNuevaVenta.Name = "btnNuevaVenta";
		this.btnNuevaVenta.Size = new System.Drawing.Size(38, 32);
		this.btnNuevaVenta.TabIndex = 25;
		this.btnNuevaVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevaVenta.UseVisualStyleBackColor = true;
		this.btnNuevaVenta.Visible = false;
		this.btnNuevaVenta.Click += new System.EventHandler(btnNuevaVenta_Click);
		this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(679, 202);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(158, 32);
		this.btnImprimir.TabIndex = 24;
		this.btnImprimir.Text = "IMPRIMIR TICKET";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.txtCodDocumento.Location = new System.Drawing.Point(584, 169);
		this.txtCodDocumento.Name = "txtCodDocumento";
		this.txtCodDocumento.Size = new System.Drawing.Size(39, 20);
		this.txtCodDocumento.TabIndex = 19;
		this.txtCodDocumento.Visible = false;
		this.txtCodDocumento.TextChanged += new System.EventHandler(txtCodDocumento_TextChanged);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1154, 202);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(32, 32);
		this.btnSalir.TabIndex = 26;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(1114, 202);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(34, 32);
		this.btnGuardar.TabIndex = 23;
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Location = new System.Drawing.Point(633, 169);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(66, 20);
		this.txtDireccion.TabIndex = 14;
		this.txtDireccion.Tag = "21";
		this.txtDireccion.Visible = false;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator5.ErrorMessage = "Ingrese Detalle.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodCliente.Location = new System.Drawing.Point(76, 88);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(100, 18);
		this.txtCodCliente.TabIndex = 1;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.superValidator1.SetValidator1(this.txtCodCliente, this.customValidator6);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.Leave += new System.EventHandler(txtCodCliente_Leave);
		this.customValidator6.ErrorMessage = "Ingrese Cliente";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.txtSerie.BackColor = System.Drawing.Color.White;
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(457, 62);
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
		this.customValidator10.ErrorMessage = "Ingrese Serie.";
		this.customValidator10.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator10.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator10_ValidateValue);
		this.cmbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormaPago.Enabled = false;
		this.cmbFormaPago.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(329, 137);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(161, 20);
		this.cmbFormaPago.TabIndex = 7;
		this.cmbFormaPago.Tag = "16";
		this.superValidator1.SetValidator1(this.cmbFormaPago, this.customValidator7);
		this.cmbFormaPago.Visible = false;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.customValidator7.ErrorMessage = "Ingrese Forma Pago";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.txtNumero.Enabled = false;
		this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumero.Location = new System.Drawing.Point(508, 62);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(69, 18);
		this.txtNumero.TabIndex = 6;
		this.superValidator1.SetValidator1(this.txtNumero, this.customValidator11);
		this.txtNumero.KeyDown += new System.Windows.Forms.KeyEventHandler(txtNumero_KeyDown);
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.customValidator11.ErrorMessage = "Ingrese Numero";
		this.customValidator11.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator11.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator11_ValidateValue);
		this.customValidator1.ErrorMessage = "codSerie";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese Numeracion";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese Transportista";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator4.ErrorMessage = "Ingrese Vehiculo";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.chkVentaGratuita.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaGratuita.AutoSize = true;
		this.chkVentaGratuita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkVentaGratuita.Location = new System.Drawing.Point(108, 15);
		this.chkVentaGratuita.Name = "chkVentaGratuita";
		this.chkVentaGratuita.Size = new System.Drawing.Size(117, 19);
		this.chkVentaGratuita.TabIndex = 107;
		this.chkVentaGratuita.Text = "Venta Gratuita";
		this.chkVentaGratuita.UseVisualStyleBackColor = true;
		this.chkVentaGratuita.Visible = false;
		this.chkVentaGratuita.CheckedChanged += new System.EventHandler(chkVentaGratuita_CheckedChanged);
		this.chkVentaDsctoGlobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaDsctoGlobal.AutoSize = true;
		this.chkVentaDsctoGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkVentaDsctoGlobal.Location = new System.Drawing.Point(231, 14);
		this.chkVentaDsctoGlobal.Name = "chkVentaDsctoGlobal";
		this.chkVentaDsctoGlobal.Size = new System.Drawing.Size(161, 19);
		this.chkVentaDsctoGlobal.TabIndex = 110;
		this.chkVentaDsctoGlobal.Text = "Venta con Descuento";
		this.chkVentaDsctoGlobal.UseVisualStyleBackColor = true;
		this.chkVentaDsctoGlobal.Visible = false;
		this.chkVentaDsctoGlobal.CheckedChanged += new System.EventHandler(chkVentaDsctoGlobal_CheckedChanged);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(1012, 115);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(36, 12);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFecha.Location = new System.Drawing.Point(1054, 110);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(89, 18);
		this.dtpFecha.TabIndex = 11;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyDown += new System.Windows.Forms.KeyEventHandler(dtpFecha_KeyDown);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.dtpFecha.Leave += new System.EventHandler(dtpFecha_Leave);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(6, 62);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(55, 12);
		this.label2.TabIndex = 2;
		this.label2.Text = "Transacción";
		this.txtTransaccion.BackColor = System.Drawing.Color.White;
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Enabled = false;
		this.txtTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTransaccion.Location = new System.Drawing.Point(76, 62);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.ReadOnly = true;
		this.txtTransaccion.Size = new System.Drawing.Size(28, 18);
		this.txtTransaccion.TabIndex = 17;
		this.txtTransaccion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtTransaccion_KeyDown);
		this.txtTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTransaccion_KeyPress);
		this.txtTransaccion.Leave += new System.EventHandler(txtTransaccion_Leave);
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(117, 64);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(160, 13);
		this.lbNombreTransaccion.TabIndex = 4;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(364, 65);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(45, 12);
		this.label5.TabIndex = 8;
		this.label5.Text = "Doc. Ref.";
		this.txtDocRef.BackColor = System.Drawing.Color.White;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(423, 62);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 18);
		this.txtDocRef.TabIndex = 4;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.txtDocRef.Leave += new System.EventHandler(txtDocRef_Leave);
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(998, 91);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(50, 12);
		this.label7.TabIndex = 12;
		this.label7.Text = "Num. Doc.";
		this.txtNumDoc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtNumDoc.BackColor = System.Drawing.Color.White;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumDoc.Location = new System.Drawing.Point(1054, 88);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(89, 18);
		this.txtNumDoc.TabIndex = 14;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(5, 169);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 12);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario";
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtComentario.Location = new System.Drawing.Point(76, 164);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(501, 37);
		this.txtComentario.TabIndex = 15;
		this.txtComentario.Tag = "21";
		this.txtComentario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtComentario_KeyDown);
		this.txtComentario.Leave += new System.EventHandler(txtComentario_Leave);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(6, 89);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(48, 12);
		this.label15.TabIndex = 20;
		this.label15.Text = "Doc. Ident";
		this.txtNombreCliente.BackColor = System.Drawing.Color.White;
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreCliente.Location = new System.Drawing.Point(182, 88);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(395, 18);
		this.txtNombreCliente.TabIndex = 2;
		this.txtNombreCliente.Tag = "3";
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(1004, 136);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(44, 12);
		this.label17.TabIndex = 31;
		this.label17.Text = "Moneda :";
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(1054, 133);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(89, 20);
		this.cmbMoneda.TabIndex = 12;
		this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(cmbMoneda_SelectionChangeCommitted);
		this.cmbMoneda.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbMoneda_KeyDown);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.cmbMoneda.Leave += new System.EventHandler(cmbMoneda_Leave);
		this.txtAutorizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtAutorizacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtAutorizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtAutorizacion.Location = new System.Drawing.Point(851, 143);
		this.txtAutorizacion.Name = "txtAutorizacion";
		this.txtAutorizacion.Size = new System.Drawing.Size(39, 18);
		this.txtAutorizacion.TabIndex = 15;
		this.txtAutorizacion.Tag = "22";
		this.txtAutorizacion.Visible = false;
		this.txtAutorizacion.KeyDown += new System.Windows.Forms.KeyEventHandler(txtAutorizacion_KeyDown);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(594, 254);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(67, 12);
		this.label3.TabIndex = 44;
		this.label3.Tag = "16";
		this.label3.Text = "Forma de Pago";
		this.label3.Visible = false;
		this.dtpFechaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFechaPago.Location = new System.Drawing.Point(496, 137);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 18);
		this.dtpFechaPago.TabIndex = 8;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(383, 212);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(60, 12);
		this.label6.TabIndex = 64;
		this.label6.Tag = "7";
		this.label6.Text = "% Descuento";
		this.label6.Visible = false;
		this.txtPDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPDescuento.Location = new System.Drawing.Point(456, 210);
		this.txtPDescuento.Name = "txtPDescuento";
		this.txtPDescuento.Size = new System.Drawing.Size(64, 18);
		this.txtPDescuento.TabIndex = 21;
		this.txtPDescuento.Tag = "7";
		this.txtPDescuento.Visible = false;
		this.txtPDescuento.TextChanged += new System.EventHandler(txtPDescuento_TextChanged);
		this.txtPDescuento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPDescuento_KeyPress);
		this.cbovendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbovendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cbovendedor.FormattingEnabled = true;
		this.cbovendedor.Items.AddRange(new object[1] { "Sin Vendedor" });
		this.cbovendedor.Location = new System.Drawing.Point(76, 138);
		this.cbovendedor.Name = "cbovendedor";
		this.cbovendedor.Size = new System.Drawing.Size(161, 20);
		this.cbovendedor.TabIndex = 10;
		this.cbovendedor.SelectionChangeCommitted += new System.EventHandler(cbovendedor_SelectionChangeCommitted);
		this.cbovendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(cbovendedor_KeyDown);
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(6, 141);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(48, 12);
		this.label24.TabIndex = 75;
		this.label24.Text = "Vendedor:";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.BackColor = System.Drawing.Color.White;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTipoCambio.Location = new System.Drawing.Point(1054, 157);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(90, 18);
		this.txtTipoCambio.TabIndex = 13;
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(989, 160);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(59, 12);
		this.label8.TabIndex = 80;
		this.label8.Text = "Tipo Cambio:";
		this.txtCotizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCotizacion.ForeColor = System.Drawing.Color.Red;
		this.txtCotizacion.Location = new System.Drawing.Point(733, 53);
		this.txtCotizacion.Multiline = true;
		this.txtCotizacion.Name = "txtCotizacion";
		this.txtCotizacion.Size = new System.Drawing.Size(144, 30);
		this.txtCotizacion.TabIndex = 1;
		this.txtCotizacion.Text = ".";
		this.txtCotizacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCotizacion.Visible = false;
		this.txtCotizacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCotizacion_KeyPress);
		this.txtCotizacion.Leave += new System.EventHandler(txtCotizacion_Leave);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(731, 33);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(52, 12);
		this.label4.TabIndex = 87;
		this.label4.Text = "Cotizacion:";
		this.label4.Visible = false;
		this.txtCodigoCli.Enabled = false;
		this.txtCodigoCli.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodigoCli.Location = new System.Drawing.Point(583, 88);
		this.txtCodigoCli.Name = "txtCodigoCli";
		this.txtCodigoCli.ReadOnly = true;
		this.txtCodigoCli.Size = new System.Drawing.Size(62, 18);
		this.txtCodigoCli.TabIndex = 88;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(6, 115);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 12);
		this.label16.TabIndex = 89;
		this.label16.Text = "Direccion";
		this.txtDireccionCliente.BackColor = System.Drawing.Color.White;
		this.txtDireccionCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDireccionCliente.Location = new System.Drawing.Point(76, 114);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(501, 18);
		this.txtDireccionCliente.TabIndex = 3;
		this.lblAlmacen.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.lblAlmacen.AutoSize = true;
		this.lblAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAlmacen.Location = new System.Drawing.Point(3, 211);
		this.lblAlmacen.Name = "lblAlmacen";
		this.lblAlmacen.Size = new System.Drawing.Size(72, 12);
		this.lblAlmacen.TabIndex = 100;
		this.lblAlmacen.Tag = "";
		this.lblAlmacen.Text = "Almacen Venta:";
		this.cmbAlmacen.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(76, 208);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(289, 20);
		this.cmbAlmacen.TabIndex = 16;
		this.cmbAlmacen.Tag = "0";
		this.cmbAlmacen.SelectionChangeCommitted += new System.EventHandler(cmbAlmacen_SelectionChangeCommitted);
		this.cmbAlmacen.KeyDown += new System.Windows.Forms.KeyEventHandler(cmbAlmacen_KeyDown);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.BackColor = System.Drawing.Color.DarkGray;
		this.groupBox3.Controls.Add(this.txttasa);
		this.groupBox3.Controls.Add(this.label30);
		this.groupBox3.Controls.Add(this.txtMoneda);
		this.groupBox3.Controls.Add(this.label28);
		this.groupBox3.Controls.Add(this.txtplazo);
		this.groupBox3.Controls.Add(this.label27);
		this.groupBox3.Controls.Add(this.lbLineaCredito);
		this.groupBox3.Controls.Add(this.txtLineaCredito);
		this.groupBox3.Controls.Add(this.txtLineaCreditoUso);
		this.groupBox3.Controls.Add(this.label23);
		this.groupBox3.Controls.Add(this.label25);
		this.groupBox3.Controls.Add(this.txtLineaCreditoDisponible);
		this.groupBox3.Enabled = false;
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(1149, 85);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(221, 149);
		this.groupBox3.TabIndex = 101;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Condiciones de Crédito:";
		this.groupBox3.Enter += new System.EventHandler(groupBox3_Enter);
		this.txttasa.BackColor = System.Drawing.Color.White;
		this.txttasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txttasa.Location = new System.Drawing.Point(103, 78);
		this.txttasa.Name = "txttasa";
		this.txttasa.ReadOnly = true;
		this.txttasa.Size = new System.Drawing.Size(112, 18);
		this.txttasa.TabIndex = 106;
		this.txttasa.Text = "0";
		this.txttasa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label30.Location = new System.Drawing.Point(29, 81);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(71, 12);
		this.label30.TabIndex = 105;
		this.label30.Text = "Tasa de Interés:";
		this.txtMoneda.BackColor = System.Drawing.Color.White;
		this.txtMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtMoneda.Location = new System.Drawing.Point(103, 99);
		this.txtMoneda.Name = "txtMoneda";
		this.txtMoneda.ReadOnly = true;
		this.txtMoneda.Size = new System.Drawing.Size(112, 18);
		this.txtMoneda.TabIndex = 102;
		this.txtMoneda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label28.Location = new System.Drawing.Point(58, 99);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(42, 12);
		this.label28.TabIndex = 101;
		this.label28.Text = "Moneda:";
		this.txtplazo.BackColor = System.Drawing.Color.White;
		this.txtplazo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtplazo.Location = new System.Drawing.Point(103, 122);
		this.txtplazo.Name = "txtplazo";
		this.txtplazo.ReadOnly = true;
		this.txtplazo.Size = new System.Drawing.Size(112, 18);
		this.txtplazo.TabIndex = 100;
		this.txtplazo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label27.Location = new System.Drawing.Point(63, 125);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(31, 12);
		this.label27.TabIndex = 99;
		this.label27.Text = "Plazo:";
		this.lbLineaCredito.AutoSize = true;
		this.lbLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbLineaCredito.Location = new System.Drawing.Point(6, 16);
		this.lbLineaCredito.Name = "lbLineaCredito";
		this.lbLineaCredito.Size = new System.Drawing.Size(94, 12);
		this.lbLineaCredito.TabIndex = 85;
		this.lbLineaCredito.Text = "Línea de Crédito (S/.):";
		this.txtLineaCredito.BackColor = System.Drawing.Color.White;
		this.txtLineaCredito.Enabled = false;
		this.txtLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCredito.Location = new System.Drawing.Point(103, 13);
		this.txtLineaCredito.Name = "txtLineaCredito";
		this.txtLineaCredito.ReadOnly = true;
		this.txtLineaCredito.Size = new System.Drawing.Size(112, 18);
		this.txtLineaCredito.TabIndex = 84;
		this.txtLineaCredito.Text = "0";
		this.txtLineaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtLineaCreditoUso.BackColor = System.Drawing.Color.White;
		this.txtLineaCreditoUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoUso.Location = new System.Drawing.Point(103, 57);
		this.txtLineaCreditoUso.Name = "txtLineaCreditoUso";
		this.txtLineaCreditoUso.ReadOnly = true;
		this.txtLineaCreditoUso.Size = new System.Drawing.Size(112, 18);
		this.txtLineaCreditoUso.TabIndex = 98;
		this.txtLineaCreditoUso.Text = "0";
		this.txtLineaCreditoUso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(5, 36);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(95, 12);
		this.label23.TabIndex = 95;
		this.label23.Text = "Línea Disponible (S/.):";
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.Location = new System.Drawing.Point(7, 60);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(93, 12);
		this.label25.TabIndex = 97;
		this.label25.Text = "Línea C. en Uso (S/.):";
		this.txtLineaCreditoDisponible.BackColor = System.Drawing.Color.White;
		this.txtLineaCreditoDisponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoDisponible.Location = new System.Drawing.Point(103, 36);
		this.txtLineaCreditoDisponible.Name = "txtLineaCreditoDisponible";
		this.txtLineaCreditoDisponible.ReadOnly = true;
		this.txtLineaCreditoDisponible.Size = new System.Drawing.Size(112, 18);
		this.txtLineaCreditoDisponible.TabIndex = 96;
		this.txtLineaCreditoDisponible.Text = "0";
		this.txtLineaCreditoDisponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(583, 214);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(90, 17);
		this.checkBox1.TabIndex = 102;
		this.checkBox1.Text = "Venta Normal";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Visible = false;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(497, 64);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(8, 12);
		this.label37.TabIndex = 103;
		this.label37.Text = "-";
		this.txtcodpedido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtcodpedido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtcodpedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcodpedido.ForeColor = System.Drawing.Color.Red;
		this.txtcodpedido.Location = new System.Drawing.Point(1200, 51);
		this.txtcodpedido.Multiline = true;
		this.txtcodpedido.Name = "txtcodpedido";
		this.txtcodpedido.Size = new System.Drawing.Size(164, 28);
		this.txtcodpedido.TabIndex = 104;
		this.txtcodpedido.Tag = "21";
		this.txtcodpedido.Text = ".";
		this.txtcodpedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtcodpedido.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtcodpedido_KeyPress);
		this.label38.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label38.AutoSize = true;
		this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label38.Location = new System.Drawing.Point(1158, 60);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(33, 12);
		this.label38.TabIndex = 105;
		this.label38.Tag = "21";
		this.label38.Text = "Pedido";
		this.labelnotacredito.AutoSize = true;
		this.labelnotacredito.ForeColor = System.Drawing.Color.OrangeRed;
		this.labelnotacredito.Location = new System.Drawing.Point(343, 38);
		this.labelnotacredito.Name = "labelnotacredito";
		this.labelnotacredito.Size = new System.Drawing.Size(234, 13);
		this.labelnotacredito.TabIndex = 106;
		this.labelnotacredito.Text = "Este Cliente tiene Notas de Credito no aplicadas";
		this.labelnotacredito.Visible = false;
		this.rbtnPendiente.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.rbtnPendiente.AutoSize = true;
		this.rbtnPendiente.ForeColor = System.Drawing.Color.OliveDrab;
		this.rbtnPendiente.Location = new System.Drawing.Point(583, 62);
		this.rbtnPendiente.Name = "rbtnPendiente";
		this.rbtnPendiente.Size = new System.Drawing.Size(140, 17);
		this.rbtnPendiente.TabIndex = 112;
		this.rbtnPendiente.Text = "Pendiente de Despacho";
		this.rbtnPendiente.UseVisualStyleBackColor = true;
		this.rbtnPendiente.Visible = false;
		this.groupBox1.Controls.Add(this.lblanulacion);
		this.groupBox1.Controls.Add(this.btnImprimirPdf);
		this.groupBox1.Controls.Add(this.btnRegenerar);
		this.groupBox1.Controls.Add(this.btnCrearDespacho);
		this.groupBox1.Controls.Add(this.btnseparar);
		this.groupBox1.Controls.Add(this.lblAnulado);
		this.groupBox1.Controls.Add(this.txtCodDocumento);
		this.groupBox1.Controls.Add(this.btnNuevaVenta);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.label26);
		this.groupBox1.Controls.Add(this.btnImprimir);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.chkVentaDsctoGlobal);
		this.groupBox1.Controls.Add(this.txtIGV);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.txtPrecioVenta);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.btnGuardar);
		this.groupBox1.Controls.Add(this.btnSalir);
		this.groupBox1.Controls.Add(this.chkVentaGratuita);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.rbtnPendiente);
		this.groupBox1.Controls.Add(this.labelnotacredito);
		this.groupBox1.Controls.Add(this.label38);
		this.groupBox1.Controls.Add(this.txtcodpedido);
		this.groupBox1.Controls.Add(this.label37);
		this.groupBox1.Controls.Add(this.checkBox1);
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.lblAlmacen);
		this.groupBox1.Controls.Add(this.txtDireccionCliente);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.txtCodigoCli);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtCotizacion);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.cbovendedor);
		this.groupBox1.Controls.Add(this.txtPDescuento);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtAutorizacion);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtNombreCliente);
		this.groupBox1.Controls.Add(this.txtCodCliente);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
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
		this.groupBox1.Location = new System.Drawing.Point(0, 338);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1370, 240);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.btnImprimirPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimirPdf.ImageIndex = 3;
		this.btnImprimirPdf.ImageList = this.imageList1;
		this.btnImprimirPdf.Location = new System.Drawing.Point(843, 202);
		this.btnImprimirPdf.Name = "btnImprimirPdf";
		this.btnImprimirPdf.Size = new System.Drawing.Size(127, 32);
		this.btnImprimirPdf.TabIndex = 118;
		this.btnImprimirPdf.Text = "IMPRIMIR PDF";
		this.btnImprimirPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimirPdf.UseVisualStyleBackColor = true;
		this.btnImprimirPdf.Visible = false;
		this.btnImprimirPdf.Click += new System.EventHandler(btnImprimirPdf_Click);
		this.btnRegenerar.Location = new System.Drawing.Point(733, 163);
		this.btnRegenerar.Name = "btnRegenerar";
		this.btnRegenerar.Size = new System.Drawing.Size(75, 23);
		this.btnRegenerar.TabIndex = 117;
		this.btnRegenerar.Text = "Regenerar";
		this.btnRegenerar.UseVisualStyleBackColor = true;
		this.btnRegenerar.Visible = false;
		this.btnRegenerar.Click += new System.EventHandler(btnRegenerar_Click);
		this.btnCrearDespacho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCrearDespacho.ImageIndex = 3;
		this.btnCrearDespacho.Location = new System.Drawing.Point(976, 201);
		this.btnCrearDespacho.Name = "btnCrearDespacho";
		this.btnCrearDespacho.Size = new System.Drawing.Size(131, 32);
		this.btnCrearDespacho.TabIndex = 116;
		this.btnCrearDespacho.Text = "CREAR DESPACHO";
		this.btnCrearDespacho.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCrearDespacho.UseVisualStyleBackColor = true;
		this.btnCrearDespacho.Click += new System.EventHandler(btnCrearDespacho_Click);
		this.btnseparar.Location = new System.Drawing.Point(652, 85);
		this.btnseparar.Name = "btnseparar";
		this.btnseparar.Size = new System.Drawing.Size(73, 30);
		this.btnseparar.TabIndex = 115;
		this.btnseparar.Text = "SEPARAR";
		this.btnseparar.UseVisualStyleBackColor = true;
		this.btnseparar.Visible = false;
		this.btnseparar.Click += new System.EventHandler(button1_Click_1);
		this.lblAnulado.AutoSize = true;
		this.lblAnulado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAnulado.ForeColor = System.Drawing.Color.Red;
		this.lblAnulado.Location = new System.Drawing.Point(480, 14);
		this.lblAnulado.Name = "lblAnulado";
		this.lblAnulado.Size = new System.Drawing.Size(143, 20);
		this.lblAnulado.TabIndex = 114;
		this.lblAnulado.Text = "VENTA ANULADA";
		this.lblAnulado.Visible = false;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.Location = new System.Drawing.Point(266, 140);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(60, 12);
		this.label26.TabIndex = 113;
		this.label26.Text = "Forma Pago :";
		this.label26.Visible = false;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(6, 13);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(86, 20);
		this.label22.TabIndex = 30;
		this.label22.Text = "Cabecera";
		this.lblanulacion.AutoSize = true;
		this.lblanulacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblanulacion.ForeColor = System.Drawing.Color.Red;
		this.lblanulacion.Location = new System.Drawing.Point(640, 16);
		this.lblanulacion.Name = "lblanulacion";
		this.lblanulacion.Size = new System.Drawing.Size(0, 20);
		this.lblanulacion.TabIndex = 119;
		this.lblanulacion.Visible = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(1370, 578);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmVenta";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Venta";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmVenta_Load);
		base.Shown += new System.EventHandler(frmVenta_Shown);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
