using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using DevComponents.Editors.DateTimeAdv;
using FinalXML.Librerias;
using Microsoft.VisualBasic;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Librerias;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.SunatFacElec;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;
using Tesseract;

namespace SIGEFA.Formularios;

public class frmVenta2019 : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private string label6 = "";

	private string label7 = "";

	private string subtotalfila = "0.00";

	private bool obligarElegir = false;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	public decimal puInicio = default(decimal);

	private clsCliente cli = new clsCliente();

	private int anulados = 0;

	public int CodCliente;

	public int CodigoCaja;

	public int Tipo = 0;

	public int SinStock;

	public string NombreCliente;

	public string CodPedido;

	private clsReporteTransferencias dst = new clsReporteTransferencias();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsAdmMoneda AdmMoned = new clsAdmMoneda();

	private clsFormaPago fpago = new clsFormaPago();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsAdmUnidad AdmUnidad = new clsAdmUnidad();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	private clsPedido pedido = new clsPedido();

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private clsAdmPago AdmPagos = new clsAdmPago();

	private List<clsNotaCredito> ncredito = new List<clsNotaCredito>();

	private clsValidar ok = new clsValidar();

	private clsAdmTipoCambio admtc = new clsAdmTipoCambio();

	private Sunat MyInfoSunat;

	private Reniec MyInfoReniec;

	private IntRange red = new IntRange(0, 255);

	private IntRange green = new IntRange(0, 255);

	private IntRange blue = new IntRange(0, 255);

	public int codPedidoVenta = 0;

	public bool esventa = false;

	public int CodSerie;

	public int manual = 0;

	public clsUsuario vendedor;

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	public int CodDocumento;

	public bool editar = false;

	public bool nuevaOV = false;

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	public decimal montogratuitas;

	public decimal montogravadas;

	public decimal montoexoneradas = default(decimal);

	public decimal montoinafectas = default(decimal);

	public bool banderagrabada;

	public bool banderaexonerada;

	public bool banderainafecta;

	public bool banderadelete = false;

	public bool bandera = false;

	public List<clsDetallePedido> detalle = new List<clsDetallePedido>();

	public mdi_Menu menu;

	private bool keyHold = false;

	public string timpuesto = "";

	private clsAdmParametro admParametro = new clsAdmParametro();

	private DataGridViewComboBoxEditingControl dgvCombo;

	private List<int> ListaEmpresa = new List<int>();

	private List<int> ListaCantDoc = new List<int>();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsTransaccion tran = new clsTransaccion();

	private List<clsPedido> PedidosIngresados = new List<clsPedido>();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsTransferencia transfer = new clsTransferencia();

	private clsAdmTransferencia admTransa = new clsAdmTransferencia();

	public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>();

	public DataTable d = null;

	private DataTable tablaFiltro = null;

	private List<DataTable> listatablas = new List<DataTable>();

	private Facturacion facturacion = new Facturacion();

	private clsPago Pag = new clsPago();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	public string CodVenta;

	public int impresion;

	private clsEmpresa empresa = new clsEmpresa();

	private clsAdmEmpresa admEmpresa = new clsAdmEmpresa();

	private clsReporteFactura ds1 = new clsReporteFactura();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsTipoCambio tc = new clsTipoCambio();

	private int mon = 1;

	private clsAdmUnidadEquivalente clsuniequ = new clsAdmUnidadEquivalente();

	private clsAdmSucursal admsucu = new clsAdmSucursal();

	private List<clsFacturaVenta> lista_facturas;

	private DataTable aux = new DataTable();

	public bool generadoDeGuiadeRemisionCompra = false;

	public frmGuiaRemisionCompra ventanagrc = null;

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmDespacho admdespacho = new clsAdmDespacho();

	private clsAdmFormulario admform = new clsAdmFormulario();

	private clsAdmNotaIngreso AdmIngreso = new clsAdmNotaIngreso();

	public bool generadoDeCotizacion = false;

	internal int CodClienteGeneradoCotizacion;

	private clsCotizacion coti = new clsCotizacion();

	private clsOrdenCompraCotizacion OrdenCompraCotizacion = new clsOrdenCompraCotizacion();

	private clsDetalleCotizacion detaCoti = new clsDetalleCotizacion();

	private clsAdmCotizacion AdmCoti = new clsAdmCotizacion();

	private clsAdmOrdenCompraCotizacion AdmOrdenC = new clsAdmOrdenCompraCotizacion();

	private int codCotizacion;

	private int CodOrdenCompra;

	private List<int> ls = new List<int>();

	private clsParametro param = new clsParametro();

	private clsAdmParametro admParam = new clsAdmParametro();

	private clsAdmParametroDescuento AdmDes = new clsAdmParametroDescuento();

	public int codcombo = 0;

	private List<int> lscombo = new List<int>();

	private clsGuiaRemision grc = new clsGuiaRemision();

	private clsAdmGuiaRemisionCompra admgrc = new clsAdmGuiaRemisionCompra();

	public bool bandNuevo = false;

	public bool bandEditar = false;

	internal int CodClienteGeneradoGRC;

	private clsTransferencia extornacion = new clsTransferencia();

	private List<DataGridViewRow> itemsEliminados = new List<DataGridViewRow>();

	private object valorInicial = null;

	private List<itemVerificarRA> listadoItemsVerificar = new List<itemVerificarRA>();

	public List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private int codproducto_error = 0;

	private List<clsDetalleTransferencia> detalleExtor = new List<clsDetalleTransferencia>();

	private clsAdmTecnico admtec = new clsAdmTecnico();

	private clsAdmZona admzona = new clsAdmZona();

	private clsAdmCategoriaCliente admctgcliente = new clsAdmCategoriaCliente();

	private IContainer components = null;

	private PanelEx panel;

	private Line line2;

	private ToolStripButton toolStripGuardar;

	private ToolStripButton toolStripButtonSalir;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Button btnRefrescar;

	private DataGridView dgvproductos;

	private GroupBox groupBox3;

	private GroupBox groupBox4;

	private GroupBox groupBox5;

	private TextBoxX txtDireccion;

	private TextBoxX txtNombreCliente;

	private DateTimeInput dtpFecha1;

	private TextBoxX txtPrecioVenta;

	private Label label9;

	private TextBoxX txtIGV;

	private Label label8;

	private TextBoxX txtValorVenta;

	private TextBox txtFiltro;

	private Label label10;

	private Label label11;

	private Label labelx;

	private Label label13;

	private System.Windows.Forms.ToolTip toolTip1;

	private GroupBox groupBox6;

	public DataGridView dgvdetalle;

	private RadioButton chkFactura;

	private RadioButton chkBoleta;

	private Label label14;

	private Label label5;

	private TextBoxX txtinafectas;

	private TextBoxX txtgratuitas;

	private Label label16;

	private Label label15;

	private GroupBox groupBox7;

	private TextBox txttasa;

	private Label label30;

	private Label lbLineaCredito;

	private TextBox txtLineaCredito;

	private TextBox txtLineaCreditoUso;

	private Label label23;

	private Label label25;

	private TextBox txtLineaCreditoDisponible;

	public TextBoxX txtCodCliente;

	private ComboBoxEx cmbFormaPago;

	private DateTimePicker dtpFechaPago;

	private TextBoxX txtCodigoVendedor;

	private TextBoxX txtNombreVendedor;

	private ButtonX btnInicioOV;

	private GroupBox groupBox8;

	private ButtonX btnAnulaOV;

	private ButtonX btnEditaOV;

	private TextBoxX txtSerie;

	private TextBoxX txtPedido;

	private TextBoxX txtDocRef;

	private Label label12;

	private Label lbDocumento;

	private Label label21;

	public CheckBoxX chkVentaSinStock;

	private TextBoxX txtCodigoBarras;

	private PictureBox pbCapchatS;

	private TextBox txtSunat_Capchat;

	private TextBox txtBruto;

	private TextBox txtDscto;

	private Label label19;

	private Label label20;

	private TextBoxX textBoxX2;

	private TextBoxX textBoxX1;

	private Label label22;

	private GroupBox groupBox9;

	private SuperValidator superValidator1;

	public CheckBoxX chkVentaDsctoGlobal;

	public CheckBoxX chkVentaGratuita;

	private TextBoxX txtexoneradas;

	private TextBoxX txtgravadas;

	private Label label24;

	private TextBoxX textBoxX5;

	private TextBoxX textBoxX6;

	private ToolStrip toolStrip2;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripSeparator toolStripSeparator3;

	private ToolStripButton toolStripIniciaov;

	private ToolStripSeparator toolStripSeparator4;

	private ToolStripButton toolStripEditaov;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripButton toolStripAnulaov;

	private ToolStripSeparator toolStripSeparator6;

	private DataGridView dgvStockAlmacenes;

	private GroupBox groupBox10;

	private RequiredFieldValidator requiredFieldValidator1;

	private ToolStripButton toolStripButtonPendiente;

	private ToolStripButton toolStripImprimir;

	private LabelX labelX1;

	private LabelX labelX5;

	private LabelX labelX4;

	private LabelX labelX3;

	private LabelX labelX2;

	private LabelX labelX6;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripSeparator toolStripSeparator7;

	private Label label1;

	private ComboBox cmbAlmacenes;

	private DateTimePicker dtpFecha;

	private LabelX labelX7;

	private RadDropDownList cmbMoneda;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripButton toolstripEfectivo;

	private Label label2;

	private TextBoxX txtIcbper;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn codUniversal;

	private DataGridViewTextBoxColumn nomAlma;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewComboBoxColumn unidad;

	private DataGridViewTextBoxColumn nmarca;

	private DataGridViewTextBoxColumn Modelo;

	private DataGridViewTextBoxColumn stockdisponible;

	private DataGridViewTextBoxColumn cant;

	private DataGridViewTextBoxColumn precio;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn codunidadmedida;

	private DataGridViewTextBoxColumn codsunatimpuesto;

	private DataGridViewTextBoxColumn codtimpuesto;

	private DataGridViewTextBoxColumn codalma;

	private DataGridViewTextBoxColumn unidadnombre;

	private DataGridViewTextBoxColumn icbper;

	private DataGridViewTextBoxColumn codlinea;

	private DataGridViewTextBoxColumn codfamilia;

	private PictureBox pictureBox1;

	private GroupBox groupBox11;

	private CheckBox chbxstock;

	private CheckBox chkRetencion;

	private Button btnreq;

	private DataGridViewTextBoxColumn idalmacen;

	private DataGridViewTextBoxColumn idproductoalmacen;

	private DataGridViewTextBoxColumn nomempresa;

	private DataGridViewTextBoxColumn nomalmacen;

	private DataGridViewTextBoxColumn stockalmacen;

	private DataGridViewTextBoxColumn colUnidad;

	private GroupBox gbTecnico;

	private ComboBox cmbTecnico;

	private Label lblusuariodesp;

	private ComboBox cmbZona;

	private Label label3;

	private ComboBox cmbCanalVenta;

	private Label label4;

	private Label lblcategoriacliente;

	private ComboBox cmbCategoriaCliente;

	private GroupBox groupBox12;

	public TextBoxX txtcodCotizacion;

	public Label lblcotizacion;

	private GroupBox gbordencompra;

	private LabelX labelX9;

	private LabelX lblnumero;

	public TextBoxX txtmontoordencompra;

	public TextBoxX txtnumeroordencompra;

	public CheckBox chbordencompra;

	private GroupBox groupBox13;

	public Button btndescuento;

	private CheckBox chkTodos;

	public TextBox txtdescuento;

	public CheckBox chkdescuento;

	private RadioButton chkTicket;

	private GroupBox groupBox14;

	private Button btnvercombos;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referenci;

	private DataGridViewTextBoxColumn product;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unida;

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

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn precioconigv;

	private DataGridViewTextBoxColumn valorpromedio;

	private DataGridViewTextBoxColumn Tipoarticulo;

	private DataGridViewTextBoxColumn Tipoimpuesto;

	private DataGridViewTextBoxColumn codalmacen;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn TipoUnidad;

	private DataGridViewTextBoxColumn empres;

	private DataGridViewTextBoxColumn icbper1;

	private DataGridViewTextBoxColumn icbper_band;

	private DataGridViewTextBoxColumn codline;

	private DataGridViewTextBoxColumn codfamili;

	private DataGridViewCheckBoxColumn Descuento;

	private DataGridViewTextBoxColumn combo;

	private DataGridViewTextBoxColumn stockdisponibleventa;

	public bool cargaPedido { get; set; }

	public byte[] firmadigital { get; set; }

	public DataTable unidadesequi { get; set; }

	public frmVenta2019()
	{
		InitializeComponent();
	}

	public frmVenta2019(mdi_Menu menu)
	{
		InitializeComponent();
		this.menu = menu;
	}

	private void frmVenta2019_Load(object sender, EventArgs e)
	{
		if (!chkFactura.Checked)
		{
			chkRetencion.Checked = false;
			chkRetencion.Enabled = false;
		}
		pictureBox1.Image = frmLogin.logo;
		Cursor = Cursors.WaitCursor;
		labelx.Text = "descripcion";
		label11.Text = "Descripción";
		toolStripButtonPendiente.Enabled = true;
		ponerestilo();
		cargaVendedor();
		CargaFormaPagos();
		cargaMoneda();
		cmbMoneda.Enabled = true;
		tc = mdi_Menu.clstc;
		dtpFecha.Value = DateTime.Now;
		Cursor = Cursors.Default;
		aux = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		aux.Rows.RemoveAt(0);
		cargaComboTecnicos();
		cargaComboZona();
		cargaCombocategoriaclientes();
		cargaComboCanalesVenta();
	}

	private void cargaComboCanalesVenta()
	{
		cmbCanalVenta.DataSource = admform.listadoTotalCanalesVenta();
		cmbCanalVenta.DisplayMember = "descripcion";
		cmbCanalVenta.ValueMember = "codigo";
		cmbCanalVenta.SelectedValue = 0;
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedValue = 1;
	}

	private void cargaVendedor()
	{
		int codigoUsuario = Convert.ToInt32(frmLogin.iCodUser);
		vendedor = admUsuario.MuestraUsuarioSinAdmin(codigoUsuario);
		if (vendedor != null)
		{
			txtCodigoVendedor.Text = vendedor.CodUsuario.ToString();
			txtNombreVendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
			procedimientoVendedorCanalDeVenta(vendedor);
		}
		else
		{
			txtCodigoVendedor.Text = "";
			txtNombreVendedor.Text = "";
			MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void ponerestilo()
	{
		dgvproductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
		dgvproductos.EnableHeadersVisualStyles = false;
		dgvproductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
		dgvdetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
		dgvdetalle.EnableHeadersVisualStyles = false;
		dgvdetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
	}

	private void CargaProductos(int codAlmacen)
	{
		Cursor = Cursors.WaitCursor;
		unidadesequi = clsuniequ.listar_unidad_equivalente(frmLogin.iCodAlmacen);
		dgvproductos.AutoGenerateColumns = false;
		dgvproductos.DataSource = null;
		dgvproductos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
		dgvproductos.RowHeadersVisible = false;
		dgvproductos.DataSource = data;
		if (SinStock == 1)
		{
			data.DataSource = AdmPro.RelacionSalidaTodoSinStock(1, codAlmacen, 1);
		}
		else
		{
			data.DataSource = AdmPro.RelacionSalidaTodo(1, codAlmacen, 1);
		}
		data.Filter = string.Empty;
		filtro = string.Empty;
		if (dgvproductos.Rows.Count > 0)
		{
			dgvproductos.Rows[0].Cells[codigo.Name].Selected = false;
			dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
			dgvproductos.CurrentCell = dgvproductos.CurrentRow.Cells[cant.Name];
			dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
		}
		dgvproductos.ClearSelection();
		dgvproductos.Focus();
		base.ActiveControl = dgvproductos;
		Cursor = Cursors.Default;
	}

	private void setUnidades()
	{
		d = null;
		if (dgvproductos.CurrentRow != null)
		{
			DataGridViewRow row = dgvproductos.CurrentRow;
			DataGridViewComboBoxCell a = (DataGridViewComboBoxCell)row.Cells["unidad"];
			var query = from x in unidadesequi.AsEnumerable()
				where x.Field<int>("codProducto").ToString() == row.Cells[codigo.Name].Value.ToString() && x.Field<int>("codstockalma").ToString() == row.Cells[codalma.Name].Value.ToString()
				select new
				{
					codUnidadEquivalente = x.Field<int>("codUnidadEquivalente"),
					codUnidadMedida = x.Field<int>("codUnidadMedida"),
					descripcion = x.Field<string>("descripcion"),
					precio = x.Field<decimal>("Precio")
				};
			var lista = query.ToList();
			a.DataSource = lista;
			a.DisplayMember = "descripcion";
			a.ValueMember = "codUnidadEquivalente";
			row.Cells["precio"].Value = decimal.Parse(lista[0].precio.ToString());
			row.Cells["codUnidadMedida"].Value = lista[0].codUnidadMedida.ToString();
			row.Cells["unidadnombre"].Value = lista[0].precio.ToString();
		}
	}

	private void sololectura(bool estado)
	{
		dtpFecha.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		txtDocRef.ReadOnly = estado;
		txtPedido.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		toolStripGuardar.Enabled = estado;
		toolStripEditaov.Enabled = estado;
		toolStripAnulaov.Enabled = estado;
		lbDocumento.Visible = estado;
		gbTecnico.Enabled = !estado;
	}

	private void lbl_Click(object sender, EventArgs e)
	{
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void groupBox4_Enter(object sender, EventArgs e)
	{
	}

	private void groupBox3_Enter(object sender, EventArgs e)
	{
	}

	private void frmVenta2019_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyData != Keys.Escape)
		{
		}
	}

	private void textBoxX1_KeyUp(object sender, KeyEventArgs e)
	{
	}

	public void buscar()
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			dgvproductos.AutoGenerateColumns = false;
			if (txtFiltro.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltro.Text.Trim() != "")
				{
					string filterCod = txtFiltro.Text.Trim();
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					int tipoDato = obtenerTipoDatoColumna(labelx.Text.Trim());
					string concatenador = "LIKE";
					string[] validadores = new string[2] { "'%", "%'" };
					if (tipoDato == 1)
					{
						concatenador = "=";
						validadores = new string[2] { "", "" };
						string[] array = cad;
						foreach (string c in array)
						{
							if (!decimal.TryParse(c, out var _))
							{
								Cursor = Cursors.Default;
								return;
							}
						}
					}
					if (cad.Count() > 1)
					{
						string[] array2 = cad;
						foreach (string c2 in array2)
						{
							if (cont == 1)
							{
								queries.Add(string.Format("[{0}] " + concatenador + " " + validadores[0] + "{1}" + validadores[1], labelx.Text.Trim(), c2));
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add(string.Format("[{0}] " + concatenador + " " + validadores[0] + "{1}" + validadores[1], labelx.Text.Trim(), c2));
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = string.Format("[{0}] " + concatenador + " " + validadores[0] + "{1}" + validadores[1], labelx.Text.Trim(), filterCod);
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			setUnidades();
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
			Cursor = Cursors.Default;
		}
	}

	private int obtenerTipoDatoColumna(string nombreColumna)
	{
		Type tipo = dgvproductos.Columns[nombreColumna].ValueType;
		return ((object)tipo == null) ? 1 : ((!(tipo.Name == "String")) ? 1 : 2);
	}

	private void dgvproductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label11.Text = dgvproductos.Columns[e.ColumnIndex].HeaderText;
		labelx.Text = dgvproductos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		buscar();
	}

	private void dgvproductos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		try
		{
			dgvCombo = e.Control as DataGridViewComboBoxEditingControl;
			if (dgvCombo != null)
			{
				dgvCombo.SelectedIndexChanged -= dvgCombo_SelectedIndexChanged;
				dgvCombo.SelectedIndexChanged += dvgCombo_SelectedIndexChanged;
			}
			if (!(e.Control is ComboBox))
			{
				DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
				dText.KeyPress -= dText_KeyPress;
				dText.KeyPress += dText_KeyPress;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dvgCombo_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox combo = sender as ComboBox;
		try
		{
			d = unidadesequi;
			if (d != null && combo.SelectedValue != null && combo.SelectedIndex != -1 && combo.SelectedValue.ToString() != "System.Data.DataRowView" && dgvproductos.CurrentCell != null)
			{
				EnumerableRowCollection<decimal> a = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString()
					select x.Field<decimal>("Precio");
				EnumerableRowCollection<string> b = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString()
					select x.Field<string>("descripcion");
				EnumerableRowCollection<int> c = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString()
					select x.Field<int>("codunidadmedida");
				EnumerableRowCollection<decimal> de = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString() && x.Field<int>("codstockalma") == Convert.ToInt32(dgvproductos.CurrentRow.Cells[codalma.Name].Value)
					select x.Field<decimal>("stockfactor");
				if (a.Any())
				{
					dgvproductos.CurrentRow.Cells["precio"].Value = a.ToList()[0];
					dgvproductos.CurrentRow.Cells["unidadnombre"].Value = b.ToList()[0].ToString();
					dgvproductos.CurrentRow.Cells["codunidadmedida"].Value = c.ToList()[0].ToString();
					dgvproductos.CurrentRow.Cells["stockdisponible"].Value = $"{de.ToList()[0]:0.00}";
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void LastColumnComboSelectionChanged(object sender, EventArgs e)
	{
		Point currentcell = dgvproductos.CurrentCellAddress;
		DataGridViewComboBoxEditingControl sendingCB = sender as DataGridViewComboBoxEditingControl;
		DataGridViewTextBoxCell cel = (DataGridViewTextBoxCell)dgvproductos.Rows[currentcell.Y].Cells[unidad.Name];
		cel.Value = sendingCB.EditingControlFormattedValue.ToString();
	}

	public void dText_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
		if (e.KeyChar == '-')
		{
			e.Handled = true;
		}
		if (e.KeyChar == ' ')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.Length == 0)
		{
			e.Handled = true;
		}
	}

	private void dgvproductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		if (dgvCombo != null)
		{
			dgvCombo.SelectedIndexChanged -= dvgCombo_SelectedIndexChanged;
		}
		string total = "";
		try
		{
			if (dgvproductos.RowCount > 0)
			{
				toolStripGuardar.Enabled = true;
				clsProducto product = new clsProducto();
				object valorCantidad = dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["cant"].Value;
				if (decimal.TryParse((valorCantidad != null) ? valorCantidad.ToString() : "null", out var cantidad_aux))
				{
					decimal redondeado = Math.Round(cantidad_aux, 2);
					if (redondeado != cantidad_aux)
					{
						MessageBox.Show("Se cambiara el numero ingresado " + cantidad_aux + " por el numero redondeado " + redondeado, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["cant"].Value = redondeado;
					}
				}
				decimal cant = Convert.ToDecimal(dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["cant"].Value);
				decimal stock = Convert.ToDecimal(dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["stockdisponible"].Value);
				if (SinStock == 1)
				{
					product = AdmPro.ListaTotalprod2(Convert.ToInt32(dgvproductos.CurrentRow.Cells[codigo.Name].Value.ToString()), Convert.ToInt32(dgvproductos.CurrentRow.Cells[codalma.Name].Value.ToString()), Convert.ToInt32(dgvproductos.CurrentRow.Cells[codunidadmedida.Name].Value.ToString()));
					decimal totalprod = Convert.ToDecimal(product.StockMaximo);
					if (cant <= totalprod)
					{
						if (dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["unidad"].Value == null)
						{
							Cursor = Cursors.Default;
							return;
						}
						if (dgvproductos.CurrentRow.Cells["precio"].Value != null)
						{
							decimal precioactual = (from x in unidadesequi.AsEnumerable()
								where x.Field<int>("codUnidadEquivalente").ToString() == dgvproductos.CurrentRow.Cells["unidad"].Value.ToString()
								select x.Field<decimal>("Precio")).ToList()[0];
							if (dgvproductos.CurrentRow.Cells["cant"].Value != null || Convert.ToString(dgvproductos.CurrentRow.Cells["cant"].Value) != "")
							{
								DataTable tablacantidades = AdmPro.validaPrecioCantidad(Convert.ToInt32(dgvproductos.CurrentRow.Cells["unidad"].Value), Convert.ToInt32(dgvproductos.CurrentRow.Cells["codigo"].Value), Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value));
								if (tablacantidades.Rows.Count > 0)
								{
									decimal cantidad = Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value);
									decimal max = Convert.ToDecimal(tablacantidades.Rows[0].ItemArray[3]);
									decimal min = Convert.ToDecimal(tablacantidades.Rows[0].ItemArray[2]);
									if (cantidad > max)
									{
										dgvproductos.CurrentRow.Cells["cant"].Value = max;
										return;
									}
									if (cantidad < min)
									{
										dgvproductos.CurrentRow.Cells["cant"].Value = min;
										return;
									}
									Cursor = Cursors.Default;
								}
							}
							if (dgvproductos.CurrentRow.Cells["precio"].Value.ToString() == "")
							{
								Cursor = Cursors.Default;
								return;
							}
							if (Convert.ToDecimal(dgvproductos.CurrentRow.Cells["precio"].Value) <= 0m)
							{
								Cursor = Cursors.Default;
								return;
							}
						}
						switch (e.ColumnIndex)
						{
						case 11:
							if (total != "" && Convert.ToDecimal(total) <= 0m)
							{
								Cursor = Cursors.Default;
								return;
							}
							if (total == "")
							{
								Cursor = Cursors.Default;
								return;
							}
							AgregaDetalleaGrilla(e.RowIndex);
							break;
						case 9:
							total = CalcularTotal();
							if (total != "" && Convert.ToDecimal(total) <= 0m)
							{
								Cursor = Cursors.Default;
								return;
							}
							if (total == "")
							{
								Cursor = Cursors.Default;
								return;
							}
							AgregaDetalleaGrilla(e.RowIndex);
							break;
						}
					}
					else
					{
						MessageBox.Show("La cantidad debe ser menor a: " + totalprod, "Cantidad Minima Para Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				else if (cant <= stock)
				{
					if (dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["unidad"].Value == null)
					{
						Cursor = Cursors.Default;
						return;
					}
					if (dgvproductos.CurrentRow.Cells["precio"].Value != null)
					{
						decimal precioactual2 = (from x in unidadesequi.AsEnumerable()
							where x.Field<int>("codUnidadEquivalente").ToString() == dgvproductos.CurrentRow.Cells["unidad"].Value.ToString()
							select x.Field<decimal>("Precio")).ToList()[0];
						if (dgvproductos.CurrentRow.Cells["cant"].Value != null || Convert.ToString(dgvproductos.CurrentRow.Cells["cant"].Value) != "")
						{
							DataTable tablacantidades2 = AdmPro.validaPrecioCantidad(Convert.ToInt32(dgvproductos.CurrentRow.Cells["unidad"].Value), Convert.ToInt32(dgvproductos.CurrentRow.Cells["codigo"].Value), Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value));
							if (tablacantidades2.Rows.Count > 0)
							{
								decimal cantidad2 = Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value);
								decimal max2 = Convert.ToDecimal(tablacantidades2.Rows[0].ItemArray[3]);
								decimal min2 = Convert.ToDecimal(tablacantidades2.Rows[0].ItemArray[2]);
								if (cantidad2 > max2)
								{
									dgvproductos.CurrentRow.Cells["cant"].Value = max2;
									return;
								}
								if (cantidad2 < min2)
								{
									dgvproductos.CurrentRow.Cells["cant"].Value = min2;
									return;
								}
								Cursor = Cursors.Default;
							}
						}
						if (dgvproductos.CurrentRow.Cells["precio"].Value.ToString() == "")
						{
							Cursor = Cursors.Default;
							return;
						}
						if (Convert.ToDecimal(dgvproductos.CurrentRow.Cells["precio"].Value) <= 0m)
						{
							Cursor = Cursors.Default;
							return;
						}
					}
					switch (e.ColumnIndex)
					{
					case 11:
						if (total != "" && Convert.ToDecimal(total) <= 0m)
						{
							Cursor = Cursors.Default;
							return;
						}
						if (total == "")
						{
							Cursor = Cursors.Default;
							return;
						}
						AgregaDetalleaGrilla(e.RowIndex);
						break;
					case 9:
						total = CalcularTotal();
						if (total != "" && Convert.ToDecimal(total) <= 0m)
						{
							Cursor = Cursors.Default;
							return;
						}
						if (total == "")
						{
							Cursor = Cursors.Default;
							return;
						}
						AgregaDetalleaGrilla(e.RowIndex);
						break;
					}
				}
				else
				{
					MessageBox.Show("Cantidad debe ser menor o igual al stock.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		Cursor = Cursors.Default;
	}

	public string CalcularTotal()
	{
		string cantidad = "0.00";
		subtotalfila = "0.00";
		try
		{
			if (SinStock == 1)
			{
				if (dgvproductos.CurrentRow.Cells["cant"].Value != null && dgvproductos.CurrentRow.Cells["cant"].Value.ToString() != "" && Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value) > 0m)
				{
					subtotalfila = string.Format("{0:##,##0.00}", Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value) * Convert.ToDecimal(dgvproductos.CurrentRow.Cells["precio"].Value));
					cantidad = decimal.Round(decimal.Parse(subtotalfila), 2).ToString();
					dgvproductos.CurrentRow.Cells["total"].Value = subtotalfila;
					dgvproductos.CurrentRow.DefaultCellStyle.BackColor = Color.LemonChiffon;
				}
			}
			else if (dgvproductos.CurrentRow.Cells["stockdisponible"].Value != null && dgvproductos.CurrentRow.Cells["stockdisponible"].Value.ToString() != "" && Convert.ToDecimal(dgvproductos.CurrentRow.Cells["stockdisponible"].Value) > 0m && dgvproductos.CurrentRow.Cells["cant"].Value != null && dgvproductos.CurrentRow.Cells["cant"].Value.ToString() != "" && Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value) > 0m)
			{
				subtotalfila = string.Format("{0:##,##0.00}", Convert.ToDecimal(dgvproductos.CurrentRow.Cells["cant"].Value) * Convert.ToDecimal(dgvproductos.CurrentRow.Cells["precio"].Value));
				cantidad = decimal.Round(decimal.Parse(subtotalfila), 2).ToString();
				dgvproductos.CurrentRow.Cells["total"].Value = subtotalfila;
				dgvproductos.CurrentRow.DefaultCellStyle.BackColor = Color.LemonChiffon;
			}
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		Cursor = Cursors.Default;
		return subtotalfila;
	}

	private void txtFiltro_Leave(object sender, EventArgs e)
	{
		dgvproductos.Focus();
		base.ActiveControl = dgvproductos;
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyData == Keys.Down)
		{
			dgvproductos.Focus();
			base.ActiveControl = dgvproductos;
			if (dgvproductos.Rows.Count > 0)
			{
				dgvproductos.Rows[0].Cells[codigo.Name].Selected = false;
				dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
				dgvproductos.CurrentCell = dgvproductos.CurrentRow.Cells[cant.Name];
			}
		}
		if (e.KeyData == Keys.Return)
		{
			string f = txtFiltro.Text.Trim();
		}
	}

	private void AgregaDetalleaGrilla(int index)
	{
		if (!Enumerable.Where<DataGridViewRow>(dgvdetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => int.Parse(x.Cells["codproducto"].Value.ToString()) == int.Parse(dgvproductos.Rows[index].Cells["codigo"].Value.ToString()))).Any())
		{
			decimal cantidad = default(decimal);
			decimal preciou = default(decimal);
			decimal total = default(decimal);
			decimal total_icbper = default(decimal);
			bool ibperband = false;
			int tipoImpuesto = 1;
			if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
			{
				preciou = Convert.ToDecimal(dgvproductos.CurrentRow.Cells["precio"].Value);
				total = Convert.ToDecimal(dgvproductos.CurrentRow.Cells["Total"].Value);
			}
			else
			{
				preciou = Math.Round(Convert.ToDecimal(dgvproductos.CurrentRow.Cells["precio"].Value) / Convert.ToDecimal(tc.Compra), 2);
				total = Math.Round(Convert.ToDecimal(dgvproductos.CurrentRow.Cells["Total"].Value) / Convert.ToDecimal(tc.Compra), 2);
			}
			cantidad = Convert.ToDecimal(dgvproductos.CurrentRow.Cells["Cant"].Value);
			object valor = Convert.ToInt32(dgvproductos.CurrentRow.Cells[codtimpuesto.Name].Value);
			valor = ((valor == null || valor == DBNull.Value || valor.ToString() == "") ? ((object)1) : valor);
			tipoImpuesto = Convert.ToInt32(valor);
			puInicio = preciou;
			decimal bruto = cantidad * puInicio;
			decimal montodescuento = default(decimal);
			decimal precioventa;
			decimal valorventa;
			if (tipoImpuesto == 1 || tipoImpuesto == 0)
			{
				precioventa = total;
				decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				valorventa = precioventa / factorigv;
			}
			else
			{
				valorventa = total;
				precioventa = valorventa;
			}
			decimal precioreal = precioventa / cantidad;
			decimal valorreal = valorventa / cantidad;
			decimal igv = precioventa - valorventa;
			if (Convert.ToInt32(dgvproductos.CurrentRow.Cells[icbper.Name].Value) > 0)
			{
				total_icbper = frmLogin.Configuracion.Icbper * cantidad;
				ibperband = true;
			}
			else
			{
				total_icbper = default(decimal);
				ibperband = false;
			}
			decimal maxPorcDescto = default(decimal);
			int codLinea = Convert.ToInt32(dgvproductos.CurrentRow.Cells[codlinea.Name].Value);
			object codFamilia = dgvproductos.CurrentRow.Cells[codfamilia.Name].Value;
			dgvdetalle.Rows.Add("0", dgvproductos.CurrentRow.Cells[codigo.Name].Value.ToString(), dgvproductos.CurrentRow.Cells[referencia.Name].Value.ToString(), dgvproductos.CurrentRow.Cells[descripcion.Name].Value.ToString(), dgvproductos.CurrentRow.Cells[codunidadmedida.Name].Value.ToString(), dgvproductos.CurrentRow.Cells[unidadnombre.Name].Value.ToString(), cantidad, puInicio, bruto, 0, 0, 0, montodescuento, valorventa, igv, precioventa, valorreal, precioreal, precioventa, 0, 0, dgvproductos.CurrentRow.Cells[codsunatimpuesto.Name].Value.ToString(), dgvproductos.CurrentRow.Cells[codalma.Name].Value.ToString(), dgvproductos.CurrentRow.Cells[nomAlma.Name].Value.ToString(), "", "", total_icbper, ibperband, dgvproductos.CurrentRow.Cells[codlinea.Name].Value, (codFamilia != null) ? dgvproductos.CurrentRow.Cells[codfamilia.Name].Value : ((object)0), 0, 0);
			calculatotales();
			montosventa();
		}
		else
		{
			MessageBox.Show("El producto ya se encuentra agregado, primero elimine y vuelva a agregarlo");
		}
	}

	public void calculatotales()
	{
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal preciovent = default(decimal);
		decimal igvt = default(decimal);
		decimal total_icbper = default(decimal);
		montogravadas = default(decimal);
		montoexoneradas = default(decimal);
		montogratuitas = default(decimal);
		montoinafectas = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			preciovent += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			total_icbper += Convert.ToDecimal(row.Cells[icbper1.Name].Value);
			timpuesto = row.Cells[Tipoimpuesto.Name].Value.ToString();
			if (timpuesto == "21")
			{
				montogratuitas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
			if (timpuesto == "10" || timpuesto == "11" || timpuesto == "12" || timpuesto == "13" || timpuesto == "14" || timpuesto == "15" || timpuesto == "16" || timpuesto == "17")
			{
				montogravadas += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			}
			if (timpuesto == "20")
			{
				montoexoneradas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
			if (timpuesto == "30" || timpuesto == "31" || timpuesto == "32" || timpuesto == "33" || timpuesto == "34" || timpuesto == "35" || timpuesto == "36")
			{
				montoinafectas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
		}
		txtBruto.Text = $"{bruto + total_icbper:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{igvt:#,##0.00}";
		txtIcbper.Text = $"{total_icbper:#,##0.00}";
		txtPrecioVenta.Text = $"{preciovent + total_icbper:#,##0.00}";
		txtgravadas.Text = $"{montogravadas:#,##0.00}";
		txtinafectas.Text = $"{montoinafectas:#,##0.00}";
		txtgratuitas.Text = $"{montogratuitas:#,##0.00}";
		txtexoneradas.Text = $"{montoexoneradas:#,##0.00}";
	}

	private void CargaCliente()
	{
		try
		{
			cli = AdmCli.MuestraCliente(CodCliente);
			if (cli.RucDni.Length == 8)
			{
				chkBoleta.Checked = true;
				cli.DocumentoIdentidad = new clsDocumentoIdentidad();
				cli.DocumentoIdentidad.CodDocumentoIdentidad = 1;
				cli.DocumentoIdentidad.CodigoSunat = 1;
				if (cli.codCategoriaCliente != 0)
				{
					cmbCategoriaCliente.Enabled = false;
					cmbCategoriaCliente.SelectedValue = cli.codCategoriaCliente;
				}
				else
				{
					cmbCategoriaCliente.Enabled = false;
					cmbCategoriaCliente.SelectedValue = 1;
					AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbCategoriaCliente.SelectedValue));
				}
			}
			else if (cli.RucDni.Length == 11)
			{
				chkFactura.Checked = true;
				cli.DocumentoIdentidad = new clsDocumentoIdentidad();
				cli.DocumentoIdentidad.CodDocumentoIdentidad = 3;
				cli.DocumentoIdentidad.CodigoSunat = 6;
				if (cli.codCategoriaCliente != 0)
				{
					cmbCategoriaCliente.Enabled = false;
					cmbCategoriaCliente.SelectedValue = cli.codCategoriaCliente;
				}
				else if (Strings.Left(cli.RucDni, 2) == "10")
				{
					cmbCategoriaCliente.Enabled = false;
					cmbCategoriaCliente.SelectedValue = 2;
					AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbCategoriaCliente.SelectedValue));
				}
				else
				{
					cmbCategoriaCliente.Enabled = true;
					cmbCategoriaCliente.SelectedValue = 0;
				}
			}
			txtCodCliente.Text = cli.RucDni;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
			CargaCreditoCliente(cli);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (cmbFormaPago.SelectedIndex != -1)
		{
			fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
			dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		ok.enteros(e);
		if (e.KeyChar == '\r')
		{
			try
			{
				frmGestionCliente frmGC = new frmGestionCliente();
				switch (txtCodCliente.Text.Length)
				{
				case 1:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo un digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 2:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo dos digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 3:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo tres digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 4:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cuatro digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 5:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cinco digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 6:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo seis digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 7:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo siete digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 8:
					cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
					if (cli != null)
					{
						CodCliente = cli.CodCliente;
						if (cli.Habilitado)
						{
							txtNombreCliente.Text = cli.RazonSocial;
							txtDireccion.Text = cli.DireccionLegal;
							if (cli.codCategoriaCliente != 0)
							{
								cmbCategoriaCliente.Enabled = false;
								cmbCategoriaCliente.SelectedValue = cli.codCategoriaCliente;
							}
							else
							{
								cmbCategoriaCliente.Enabled = false;
								cmbCategoriaCliente.SelectedValue = 1;
								AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbCategoriaCliente.SelectedValue));
							}
						}
						else
						{
							CargaDNI();
						}
						cli.DocumentoIdentidad = new clsDocumentoIdentidad();
						cli.DocumentoIdentidad.CodDocumentoIdentidad = 1;
						cli.DocumentoIdentidad.CodigoSunat = 1;
						CargaCreditoCliente(cli);
					}
					else
					{
						CargaDNI();
						cmbCategoriaCliente.Enabled = false;
						cmbCategoriaCliente.SelectedValue = 1;
						CodCliente = 0;
					}
					chkBoleta.Checked = true;
					cmbFormaPago.Enabled = true;
					break;
				case 9:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso nueve digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case 10:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso diez digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 11:
					cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
					if (cli != null)
					{
						cli = AdmCli.MuestraCliente(cli.CodCliente);
						if (cli != null)
						{
							CodCliente = cli.CodCliente;
							txtNombreCliente.Text = cli.RazonSocial;
							txtDireccion.Text = cli.DireccionLegal;
							cli.DocumentoIdentidad = new clsDocumentoIdentidad();
							cli.DocumentoIdentidad.CodDocumentoIdentidad = 3;
							cli.DocumentoIdentidad.CodigoSunat = 6;
							if (cli.codCategoriaCliente != 0)
							{
								cmbCategoriaCliente.Enabled = false;
								cmbCategoriaCliente.SelectedValue = cli.codCategoriaCliente;
							}
							else if (Strings.Left(cli.RucDni, 2) == "10")
							{
								cmbCategoriaCliente.Enabled = false;
								cmbCategoriaCliente.SelectedValue = 2;
								AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbCategoriaCliente.SelectedValue));
							}
							else
							{
								cmbCategoriaCliente.Enabled = true;
								cmbCategoriaCliente.SelectedValue = 0;
							}
						}
						else
						{
							CargaRUC();
							cmbCategoriaCliente.Enabled = true;
							cmbCategoriaCliente.SelectedValue = 0;
							CodCliente = 0;
						}
					}
					else
					{
						CargaRUC();
						CodCliente = 0;
						if (Strings.Left(txtCodCliente.Text, 2) == "10")
						{
							cmbCategoriaCliente.Enabled = false;
							cmbCategoriaCliente.SelectedValue = 2;
						}
						else
						{
							cmbCategoriaCliente.Enabled = true;
							cmbCategoriaCliente.SelectedValue = 0;
						}
					}
					CargaCreditoCliente(cli);
					chkFactura.Checked = true;
					cmbFormaPago.Enabled = true;
					break;
				default:
					ValidaLongitud();
					break;
				}
				txtCodigoVendedor.Focus();
				Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(ex.Message);
				CargarImagenSunat();
			}
		}
		Cursor = Cursors.Default;
	}

	private void CargaRUC()
	{
		if (txtCodCliente.Text.Length == 11)
		{
			try
			{
				ReniecAPI reniecAPI = new ReniecAPI();
				string result = reniecAPI.GetInfoRuc(txtCodCliente.Text);
				string[] array = result.Split('|');
				txtNombreCliente.Text = array[0];
				txtDireccion.Text = array[1];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR API RENIEC - frmVenta2019");
			}
		}
	}

	public void CargaCreditoCliente(clsCliente cli)
	{
		if (cli != null)
		{
			if (cli.Moneda == 1)
			{
				txtLineaCredito.Text = $"{cli.LineaCredito:#,##0.00}";
				txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible:#,##0.00}";
				txtLineaCreditoUso.Text = $"{cli.LineaCreditoUsado:#,##0.00}";
				txttasa.Text = $"{cli.Tasa:#,##0.00}";
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
				txtLineaCredito.Text = $"{cli.LineaCredito / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
				txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
				TextBox textBox = txtLineaCreditoUso;
				string text = (Text = $"{cli.LineaCreditoUsado / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}");
				textBox.Text = text;
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
			if (cli.FormaPago != 0)
			{
				cmbFormaPago.SelectedValue = cli.FormaPago;
				EventArgs ee = new EventArgs();
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
			}
			else
			{
				dtpFechaPago.Value = DateTime.Today;
			}
			return;
		}
		cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
		if (cli == null)
		{
			return;
		}
		if (cli.Moneda == 1)
		{
			txtLineaCredito.Text = $"{cli.LineaCredito:#,##0.00}";
			txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible:#,##0.00}";
			txtLineaCreditoUso.Text = $"{cli.LineaCreditoUsado:#,##0.00}";
			txttasa.Text = $"{cli.Tasa:#,##0.00}";
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
			txtLineaCredito.Text = $"{cli.LineaCredito / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
			txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
			TextBox textBox2 = txtLineaCreditoUso;
			string text = (Text = $"{cli.LineaCreditoUsado / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}");
			textBox2.Text = text;
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
		if (cli.FormaPago != 0)
		{
			cmbFormaPago.SelectedValue = cli.FormaPago;
			EventArgs ee2 = new EventArgs();
			cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee2);
		}
		else
		{
			dtpFechaPago.Value = DateTime.Today;
		}
	}

	private void LeerDatos()
	{
		string cadena = "";
		MyInfoSunat.GetInfo(txtCodCliente.Text, txtSunat_Capchat.Text);
		switch (MyInfoSunat.GetResul)
		{
		case Sunat.Resul.Ok:
		{
			limpiarSunat();
			txtCodCliente.Text = MyInfoSunat.Ruc.Trim();
			txtDireccion.Text = MyInfoSunat.Direcion.Trim();
			txtNombreCliente.Text = MyInfoSunat.RazonSocial.Trim();
			if (!txtCodCliente.Text.StartsWith("1"))
			{
				cadena = txtDireccion.Text;
				int indice = cadena.IndexOf('<');
				cadena = cadena.Substring(0, cadena.Length - (cadena.Length - indice));
				txtDireccion.Text = cadena;
			}
			else
			{
				txtDireccion.Text = "-";
			}
			string textoOriginal = txtNombreCliente.Text;
			string textoNormalizado = textoOriginal.Normalize(NormalizationForm.FormD);
			Regex reg = new Regex("[^a-zA-Z0-9 ]");
			string textoSinAcentos = reg.Replace(textoNormalizado, "");
			txtNombreCliente.Text = textoSinAcentos;
			BloqueaDatos();
			break;
		}
		case Sunat.Resul.NoResul:
			limpiarSunat();
			MessageBox.Show("No Existe RUC");
			break;
		case Sunat.Resul.ErrorCapcha:
			limpiarSunat();
			MessageBox.Show("Ingrese imagen correctamente");
			break;
		default:
			MessageBox.Show("Error Desconocido");
			break;
		}
	}

	private void Ciudad(string Direccion)
	{
		string[] array = Direccion.Split('-');
		if (array.Length > 1)
		{
			int a = array.Length;
			string DirTemp = array[a - 3].Trim();
			DirTemp = DirTemp.TrimEnd(' ');
			string[] ArrayDir = DirTemp.Split(' ');
			int i = ArrayDir.Length;
		}
	}

	private void BloqueaDatos()
	{
		txtNombreCliente.ReadOnly = false;
	}

	private void ValidaLongitud()
	{
		if (txtCodCliente.Text.Length == 0)
		{
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ningun digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (txtCodCliente.Text.Length > 11)
		{
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ha Ingresado " + txtCodCliente.Text.Length + " Digitos", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtCodCliente.SelectAll();
			txtCodCliente.Focus();
		}
	}

	private void LeerCaptchaSunat()
	{
		try
		{
			using TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
			using Bitmap image = new Bitmap(pbCapchatS.Image);
			using Pix pix = PixConverter.ToPix(image);
			using Page page = engine.Process(pix);
			string Porcentaje = $"{page.GetMeanConfidence():P}";
			string CaptchaTexto = page.GetText();
			char[] eliminarChars = new char[2] { '\n', ' ' };
			CaptchaTexto = CaptchaTexto.TrimEnd(eliminarChars);
			CaptchaTexto = CaptchaTexto.Replace(" ", string.Empty);
			CaptchaTexto = Regex.Replace(CaptchaTexto, "[^a-zA-Z]+", string.Empty);
			if ((CaptchaTexto != string.Empty) & (CaptchaTexto.Length == 4))
			{
				txtSunat_Capchat.Text = CaptchaTexto.ToUpper();
			}
			else
			{
				CargarImagenSunat();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargarImagenSunat()
	{
		try
		{
			if (MyInfoSunat == null)
			{
				MyInfoSunat = new Sunat();
			}
			pbCapchatS.Image = MyInfoSunat.GetCapcha;
			LeerCaptchaSunat();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargarImagenReniec()
	{
		try
		{
			if (MyInfoReniec == null)
			{
				MyInfoReniec = new Reniec();
			}
			pbCapchatS.Image = MyInfoReniec.GetCapcha;
			AplicacionFiltros();
			LeerCaptchaReniec();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void AplicacionFiltros()
	{
		Bitmap bmp = new Bitmap(pbCapchatS.Image);
		FiltroInvertir(bmp);
		ColorFiltros();
		Bitmap bmp2 = new Bitmap(pbCapchatS.Image);
		FiltroInvertir(bmp2);
		Bitmap bmp3 = new Bitmap(pbCapchatS.Image);
		FiltroSharpen(bmp3);
	}

	private void FiltroInvertir(Bitmap bmp)
	{
		IFilter Filtro = new Invert();
		Bitmap XImage = Filtro.Apply(bmp);
		pbCapchatS.Image = XImage;
	}

	private void FiltroSharpen(Bitmap bmp)
	{
		IFilter Filtro = new Sharpen();
		Bitmap XImage = Filtro.Apply(bmp);
		pbCapchatS.Image = XImage;
	}

	private void ColorFiltros()
	{
		red.Min = Math.Min(red.Max, byte.Parse("229"));
		red.Max = Math.Max(red.Min, byte.Parse("255"));
		green.Min = Math.Min(green.Max, byte.Parse("0"));
		green.Max = Math.Max(green.Min, byte.Parse("255"));
		blue.Min = Math.Min(blue.Max, byte.Parse("0"));
		blue.Max = Math.Max(blue.Min, byte.Parse("130"));
		ActualizarFiltro();
	}

	private void ActualizarFiltro()
	{
		ColorFiltering FiltroColor = new ColorFiltering();
		FiltroColor.Red = red;
		FiltroColor.Green = green;
		FiltroColor.Blue = blue;
		IFilter Filtro = FiltroColor;
		Bitmap bmp = new Bitmap(pbCapchatS.Image);
		Bitmap XImage = Filtro.Apply(bmp);
		pbCapchatS.Image = XImage;
	}

	private void LeerCaptchaReniec()
	{
		using TesseractEngine engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default);
		using Bitmap image = new Bitmap(pbCapchatS.Image);
		using Pix pix = PixConverter.ToPix(image);
		using Page page = engine.Process(pix);
		string Porcentaje = $"{page.GetMeanConfidence():P}";
		string CaptchaTexto = page.GetText();
		char[] eliminarChars = new char[2] { '\n', ' ' };
		CaptchaTexto = CaptchaTexto.TrimEnd(eliminarChars);
		CaptchaTexto = CaptchaTexto.Replace(" ", string.Empty);
		CaptchaTexto = Regex.Replace(CaptchaTexto, "[^a-zA-Z0-9]+", string.Empty);
		if ((CaptchaTexto != string.Empty) & (CaptchaTexto.Length == 4))
		{
			txtSunat_Capchat.Text = CaptchaTexto.ToUpper();
		}
	}

	private void CargaDNI()
	{
		try
		{
			ReniecAPI reniecAPI = new ReniecAPI();
			string result = reniecAPI.GetInfo(txtCodCliente.Text);
			string[] array = result.Split('|');
			txtNombreCliente.Text = ((array.Length != 0) ? array[0] : "");
			txtDireccion.Text = ((1 < array.Length) ? array[1] : "");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "ERROR API RENIEC - frmVenta2019");
		}
	}

	private void limpiarSunat()
	{
		txtNombreCliente.Text = "";
		txtSunat_Capchat.Text = string.Empty;
	}

	public void btnInicioOV_Click(object sender, EventArgs e)
	{
		bandNuevo = true;
		btnvercombos.Enabled = true;
		btnreq.Visible = false;
		limpiarVentana();
		dgvdetalleEditable(editable: true);
	}

	public void procedimientoASeguirPorGeneracionDesdeGuiaDeRemision(DataTable detalleOV, int codGuiaRemisionCompra)
	{
		grc = admgrc.CargaGuiaRemision(codGuiaRemisionCompra);
		btnInicioOV.PerformClick();
		CargaDetalle(detalleOV);
		calculatotales();
		montosventa();
		toolStripGuardar.Enabled = true;
	}

	public void limpiarVentana()
	{
		dgvdetalleEditable(editable: false);
		editar = false;
		cargaPedido = false;
		esventa = false;
		nuevaOV = true;
		Cursor = Cursors.WaitCursor;
		toolStripImprimir.Enabled = false;
		activaPaneles(estado: true);
		toolStripGuardar.Text = "Guardar";
		toolstripEfectivo.Enabled = false;
		toolStripGuardar.Enabled = false;
		dtpFecha.Value = DateTime.Now;
		CargaProductos(frmLogin.iCodAlmacen);
		txtgravadas.Enabled = true;
		txtexoneradas.Enabled = true;
		txtgratuitas.Enabled = true;
		txtinafectas.Enabled = true;
		txtinafectas.Enabled = true;
		txtPrecioVenta.Enabled = true;
		txtValorVenta.Enabled = true;
		txtIGV.Enabled = true;
		chbxstock.Checked = false;
		txtDocRef.Text = "OV";
		txtCodCliente.Text = "C000001";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtDocRef_KeyPress(txtDocRef, ee);
		BuscaCliente();
		chkBoleta.Checked = true;
		dgvdetalle.Rows.Clear();
		dgvStockAlmacenes.DataSource = null;
		dgvStockAlmacenes.Rows.Clear();
		calculatotales();
		textBoxX2.Text = "000";
		textBoxX1.Text = "00000000000";
		textBoxX1.ReadOnly = true;
		Cursor = Cursors.Default;
		txtCodCliente.Enabled = true;
		txtCodCliente.ReadOnly = false;
		chkBoleta.Checked = false;
		txtNombreCliente.ReadOnly = false;
		txtNombreCliente.Enabled = true;
		pedido.CodCanalVenta = null;
		cargaVendedor();
		gbTecnico.Enabled = true;
		if (frmLogin.accesocanalventas || frmLogin.iNivelUser == 1)
		{
			cmbCanalVenta.Enabled = true;
		}
		cmbTecnico.SelectedValue = -1;
		cmbZona.SelectedValue = 0;
		cmbCategoriaCliente.SelectedValue = 1;
	}

	public void inicializarForm()
	{
		toolStripGuardar.Text = "Guardar";
		toolstripEfectivo.Enabled = false;
		editar = false;
		cargaPedido = false;
		esventa = false;
		nuevaOV = false;
		pedido.CodCanalVenta = null;
		chkRetencion.Checked = false;
		chkBoleta.Checked = false;
		chbxstock.Checked = false;
		dgvproductos.DataSource = null;
		dgvproductos.Rows.Clear();
		dgvdetalle.DataSource = null;
		dgvdetalle.Rows.Clear();
		dgvStockAlmacenes.DataSource = null;
		dgvStockAlmacenes.Rows.Clear();
		txtCodCliente.Text = "";
		txtNombreCliente.Text = "";
		txtDireccion.Text = "";
		txtLineaCredito.Text = "";
		txtLineaCreditoDisponible.Text = "";
		txtLineaCreditoUso.Text = "";
		txtgravadas.Text = "";
		txtgratuitas.Text = "";
		txtinafectas.Text = "";
		txtIGV.Text = "";
		txtValorVenta.Text = "";
		txtPrecioVenta.Text = "";
		txtexoneradas.Text = "";
		activaPaneles(estado: false);
		toolStripEditaov.Enabled = false;
		dtpFecha.Value = DateTime.Now;
		dtpFechaPago.Value = DateTime.Now;
		txtDocRef.Text = "OV";
		txtCodCliente.Text = "C000001";
		KeyPressEventArgs ee = new KeyPressEventArgs('\r');
		txtDocRef_KeyPress(txtDocRef, ee);
		BuscaCliente();
		cmbMoneda.SelectedValue = 1;
		mon = 1;
		cargaVendedor();
		textBoxX2.Text = "000";
		textBoxX1.Text = "00000000000";
		textBoxX1.ReadOnly = true;
		venta = new clsFacturaVenta();
		cmbTecnico.SelectedValue = -1;
		cmbZona.SelectedValue = 0;
		cmbCanalVenta.SelectedValue = 0;
		cmbCategoriaCliente.SelectedIndex = -1;
	}

	private void activaPaneles(bool estado)
	{
		toolStripEditaov.Enabled = !estado;
		toolStripGuardar.Enabled = estado;
		bandera = estado;
		groupBox2.Enabled = estado;
		groupBox4.Enabled = estado;
	}

	private void btnEditaOV_Click(object sender, EventArgs e)
	{
		bandEditar = true;
		btnreq.Visible = false;
		toolStripImprimir.Enabled = false;
		toolstripEfectivo.Enabled = false;
		sololectura2(estado: false);
		cmbMoneda.Enabled = true;
		toolStripButtonPendiente.Enabled = false;
		toolStripGuardar.Enabled = true;
		CargaProductos(frmLogin.iCodAlmacen);
		groupBox2.Enabled = true;
		groupBox4.Enabled = true;
		bandera = true;
		calculatotales();
		editar = true;
		nuevaOV = false;
		toolStripGuardar.Text = "Guardar";
		txtNombreCliente.ReadOnly = false;
		txtNombreCliente.Enabled = true;
		dgvdetalleEditable(editable: true);
	}

	private void dgvdetalleEditable(bool editable)
	{
		foreach (DataGridViewColumn col in dgvdetalle.Columns)
		{
			col.ReadOnly = true;
			if (editable && (col.Name == cantidad.Name || col.Name == Descuento.Name))
			{
				col.ReadOnly = false;
			}
		}
	}

	private void sololectura2(bool estado)
	{
		dtpFecha.Enabled = !estado;
		txtCodCliente.ReadOnly = estado;
		txtDocRef.ReadOnly = estado;
		txtPedido.ReadOnly = estado;
		btnreq.Visible = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		gbTecnico.Enabled = !estado;
		toolStripEditaov.Enabled = estado;
		toolStripAnulaov.Enabled = estado;
		lbDocumento.Visible = estado;
	}

	private void txtNombreCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (e.KeyChar != '\r')
			{
				return;
			}
			if (Application.OpenForms["frmClientesLista"] != null)
			{
				Application.OpenForms["frmClientesLista"].Activate();
				return;
			}
			frmClientesLista form = new frmClientesLista();
			form.Proceso = 7;
			form.filtrocliente = txtNombreCliente.Text;
			if (form.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CodCliente = form.GetCodigoCliente();
			CargaCliente();
			if (cli == null)
			{
				return;
			}
			CodCliente = cli.CodCliente;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
			txtCodCliente.Text = cli.RucDni;
			cli = AdmCli.ConsultaCliente(cli.RucDni);
			if (cli.Moneda == 1)
			{
				txtLineaCredito.Text = $"{cli.LineaCredito:#,##0.00}";
				txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible:#,##0.00}";
				txtLineaCreditoUso.Text = $"{cli.LineaCreditoUsado:#,##0.00}";
				txttasa.Text = $"{cli.Tasa:#,##0.00}";
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
				txtLineaCredito.Text = $"{cli.LineaCredito / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
				txtLineaCreditoDisponible.Text = $"{cli.LineaCreditoDisponible / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}";
				TextBox textBox = txtLineaCreditoUso;
				string text = (Text = $"{cli.LineaCreditoUsado / Convert.ToDecimal(mdi_Menu.tc_hoy):#,##0.00}");
				textBox.Text = text;
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
			if (cli.FormaPago != 0)
			{
				cmbFormaPago.SelectedValue = 6;
				EventArgs ee = new EventArgs();
				cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
			}
			else
			{
				dtpFechaPago.Value = DateTime.Today;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtCodigoVendedor_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.F1)
			{
				frmVendedoresLista frmDialog = new frmVendedoresLista();
				frmDialog.proc = 2;
				frmDialog.ShowDialog();
				if (vendedor != null)
				{
					txtCodigoVendedor.Text = vendedor.CodUsuario.ToString();
					txtNombreVendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
					procedimientoVendedorCanalDeVenta(vendedor);
				}
				else
				{
					txtCodigoVendedor.Text = "";
					txtNombreVendedor.Text = "";
					MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error..." + ex.Message);
		}
	}

	private void txtCodigoVendedor_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Return && txtCodigoVendedor.Text.Trim() != "")
			{
				int codigoUsuario = Convert.ToInt32(txtCodigoVendedor.Text);
				vendedor = admUsuario.MuestraUsuarioSinAdmin(codigoUsuario);
				if (vendedor != null)
				{
					txtNombreVendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
					procedimientoVendedorCanalDeVenta(vendedor);
				}
				else
				{
					txtCodigoVendedor.Text = "";
					txtNombreVendedor.Text = "";
					MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void procedimientoVendedorCanalDeVenta(clsUsuario vendedor)
	{
		if (vendedor == null)
		{
			return;
		}
		if (pedido.CodCanalVenta != null)
		{
			if (frmLogin.accesocanalventas || frmLogin.iNivelUser == 1)
			{
				gbTecnico.Enabled = true;
				cmbCanalVenta.Enabled = true;
			}
			cmbCanalVenta.SelectedValue = pedido.CodCanalVenta;
			if (pedido.CodCanalVenta == "001002")
			{
				obligarElegir = true;
			}
			else if (pedido.CodCanalVenta == "001001")
			{
				obligarElegir = false;
			}
			return;
		}
		clsUsuario usuario = admUsuario.MuestraUsuario(vendedor.CodUsuario);
		if (bandNuevo)
		{
			cmbTecnico.SelectedValue = -1;
			cmbZona.SelectedValue = -1;
		}
		if (usuario.CodigoCanalVenta == "001002")
		{
			obligarElegir = true;
		}
		else if (usuario.CodigoCanalVenta == "001001")
		{
			obligarElegir = false;
		}
		cmbCanalVenta.SelectedValue = usuario.CodigoCanalVenta;
	}

	private void chkBoleta_Click(object sender, EventArgs e)
	{
		if (chkBoleta.Checked)
		{
			chkFactura.Checked = false;
			chkTicket.Checked = false;
		}
	}

	private void chkFactura_Click(object sender, EventArgs e)
	{
		chkBoleta.Checked = false;
		chkFactura.Checked = true;
		chkTicket.Enabled = false;
	}

	private void toolStripButtonSalir_Click(object sender, EventArgs e)
	{
		CerrarOV();
	}

	private void CerrarOV()
	{
		if (dgvdetalle.Rows.Count > 0 && txtPedido.Text == "")
		{
			if (MessageBox.Show("Existen productos pendientes por vender..! \n Desea cancelar el pedido?", "Pedido Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Close();
				Dispose();
			}
		}
		else
		{
			Close();
			Dispose();
		}
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
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
		if (CodSerie == 0)
		{
		}
	}

	private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtDocRef.Text != "" && !BuscaTipoDocumento())
		{
			MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			lbDocumento.Text = doc.Descripcion;
			lbDocumento.Visible = true;
			return true;
		}
		CodDocumento = 0;
		return false;
	}

	private void CargaNumeracionOV()
	{
		try
		{
			ser = AdmSerie.CargaSerieOV(frmLogin.iCodEmpresa, frmLogin.iCodAlmacen);
			if (ser != null)
			{
				CodSerie = ser.CodSerie;
				manual = Convert.ToInt32(ser.PreImpreso);
				if (CodSerie != 0)
				{
					txtSerie.Text = ser.Serie;
				}
			}
			else
			{
				MessageBox.Show("No existe numeración para la orden de venta", "Orden de venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			Cursor = Cursors.Default;
		}
	}

	private void frmVenta2019_Shown(object sender, EventArgs e)
	{
		if (nuevaOV)
		{
			txtDocRef.Text = "OV";
			txtCodCliente.Text = "C000001";
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			txtDocRef_KeyPress(txtDocRef, ee);
			BuscaCliente();
		}
		if (generadoDeGuiadeRemisionCompra)
		{
			CodCliente = CodClienteGeneradoGRC;
			CargaCliente();
		}
		if (generadoDeCotizacion)
		{
			CodCliente = CodClienteGeneradoCotizacion;
			CargaCliente();
		}
	}

	private bool BuscaCliente()
	{
		if (txtCodCliente.Text != "")
		{
			cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
			if (cli != null)
			{
				txtNombreCliente.Text = cli.RazonSocial;
				CodCliente = cli.CodCliente;
				txtDireccion.Text = cli.DireccionLegal;
				txtCodCliente.Text = cli.RucDni;
				if (cli.RucDni == "00000000")
				{
					txtNombreCliente.Enabled = true;
					cmbFormaPago.Enabled = false;
				}
				txtDocRef.Visible = false;
				label12.Visible = false;
				CargaCreditoCliente(cli);
				return true;
			}
			txtNombreCliente.Text = "";
			CodCliente = 0;
			txtDireccion.Text = "";
			return false;
		}
		return false;
	}

	public bool verificarPrecioVenta()
	{
		bool valor = false;
		if (mdi_Menu.MontoTopeBoleta > 0)
		{
			if (Convert.ToDecimal(txtPrecioVenta.Text) >= (decimal)mdi_Menu.MontoTopeBoleta && CodCliente == 1)
			{
				MessageBox.Show("Precio venta > S/. 700, registrar(DNI, datos completos del cliente) o seleccionar cliente para guardar pedido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (Application.OpenForms["frmClientesLista"] != null)
				{
					Application.OpenForms["frmClientesLista"].Activate();
				}
				else
				{
					frmClientesLista form = new frmClientesLista();
					form.Proceso = 3;
					form.ShowDialog();
					txtCodCliente.Text = "";
					txtDireccion.Text = "";
					txtNombreCliente.Text = "";
					if (form.exit)
					{
						cli = form.cli;
						CodCliente = cli.CodCliente;
						if (CodCliente != 0)
						{
							NombreCliente = cli.Nombre;
							CargaCliente();
							valor = true;
							ProcessTabKey(forward: true);
						}
					}
					else
					{
						txtCodCliente.Focus();
						valor = false;
					}
				}
			}
			else
			{
				valor = true;
			}
		}
		else
		{
			valor = true;
		}
		return valor;
	}

	private void frmVenta2019_KeyDown(object sender, KeyEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		switch (e.KeyCode)
		{
		case Keys.F6:
			btnInicioOV_Click(null, null);
			break;
		case Keys.F1:
			if (bandera && base.ActiveControl is TextBox)
			{
				TextBox campoTexto = base.ActiveControl as TextBox;
				if (campoTexto.Name == "txtCodigoVendedor")
				{
					txtCodigoVendedor_KeyUp(sender, e);
				}
				if (campoTexto.Name == "txtCodCliente")
				{
					txtCodCliente_KeyUp(sender, e);
				}
			}
			break;
		case Keys.F4:
			if (toolStripEditaov.Enabled)
			{
				btnEditaOV.PerformClick();
			}
			break;
		case Keys.N:
			if (e.Control)
			{
				chkTicket.Checked = true;
			}
			break;
		}
		Cursor = Cursors.Default;
	}

	private void LimpiaForm()
	{
		foreach (Control c in base.Controls)
		{
			if (c is TextBox)
			{
				c.Text = "";
			}
		}
		dgvdetalle.Rows.Clear();
		calculatotales();
	}

	private void Recalcular()
	{
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal figv = default(decimal);
		decimal pven = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			figv += Convert.ToDecimal(row.Cells[igv.Name].Value);
			pven += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtDscto.Text = $"{descuen:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		txtIGV.Text = $"{figv:#,##0.00}";
		txtPrecioVenta.Text = $"{pven:#,##0.00}";
		montosventa();
	}

	private void dgvdetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
	}

	private void dgvproductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex != -1)
			{
				if (dgvproductos.Rows[e.RowIndex].Cells[unidad.Name].ColumnIndex == e.ColumnIndex && dgvproductos.CurrentCell != null)
				{
					setUnidades();
				}
				dgvStockAlmacenes.AutoGenerateColumns = false;
				dgvStockAlmacenes.DataSource = AdmPro.muestraStockProducto_almacenes(int.Parse(dgvproductos.CurrentRow.Cells[codigo.Name].Value.ToString()));
				dgvStockAlmacenes.ClearSelection();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dataGridView1_SelectionChanged(object sender, EventArgs e)
	{
	}

	private void txtCodCliente_DoubleClick(object sender, EventArgs e)
	{
	}

	private void txtDireccion_DoubleClick(object sender, EventArgs e)
	{
	}

	private void ValidaCliente(string rucdni)
	{
		cli = AdmCli.ConsultaCliente(rucdni);
		if (cli == null)
		{
			cli = new clsCliente();
			int id = AdmCli.GetUltimoId() + 1;
			if (rucdni.Length == 8)
			{
				cli.Dni = rucdni;
				cli.DireccionEntrega = "-";
				cli.DireccionLegal = "-";
			}
			else if (rucdni.Length == 11)
			{
				cli.Ruc = rucdni;
				cli.DireccionEntrega = txtDireccion.Text;
				cli.DireccionLegal = txtDireccion.Text;
			}
			cli.Nombre = txtNombreCliente.Text;
			cli.RazonSocial = txtNombreCliente.Text;
			cli.CodigoPersonalizado = "C" + id.ToString().PadLeft(6, '0').Trim();
			cli.FormaPago = 6;
			cli.Moneda = 1;
			cli.LineaCredito = 0m;
			cli.LineaCreditoDisponible = 0m;
			cli.Habilitado = true;
			cli.CodUser = frmLogin.iCodUser;
			if (AdmCli.insert(cli))
			{
				CodCliente = cli.CodClienteNuevo;
				AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbCategoriaCliente.SelectedValue));
			}
		}
	}

	private void txtIGV_TextChanged(object sender, EventArgs e)
	{
	}

	private void label8_Click(object sender, EventArgs e)
	{
	}

	private void label9_Click(object sender, EventArgs e)
	{
	}

	private void btnAnulaOV_Click(object sender, EventArgs e)
	{
	}

	private void groupBox7_Enter(object sender, EventArgs e)
	{
	}

	private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
	{
	}

	private void chkTicket_CheckedChanged(object sender, EventArgs e)
	{
		chkBoleta.Checked = false;
		chkFactura.Checked = false;
		chkTicket.Checked = true;
	}

	private void toolStripGuardar_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		try
		{
			if (dgvdetalle.RowCount == 0)
			{
				MessageBox.Show("No se puede Guardar, no existen productos en el pedido!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (cmbCategoriaCliente.SelectedIndex == -1 && !cargaPedido && !editar && toolStripGuardar.Text != "Guardar Venta" && nuevaOV)
			{
				MessageBox.Show("No se puede Guardar, no selecciono Categoria Cliente en el pedido!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (chbordencompra.Checked)
			{
				if (Convert.ToDecimal(txtmontoordencompra.Text) == Convert.ToDecimal(txtPrecioVenta.Text))
				{
					if (verificarPrecioVenta() && superValidator1.Validate())
					{
						realizaProcesos();
					}
				}
				else
				{
					MessageBox.Show("No se puede Guardar, Monto total del pedido no es igual a monto de Orden Compra!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else if (verificarPrecioVenta() && superValidator1.Validate())
			{
				realizaProcesos();
			}
			dgvdetalleEditable(editable: false);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message.ToString());
			Cursor = Cursors.Default;
		}
		finally
		{
			Cursor.Current = Cursors.Default;
			Cursor = Cursors.Default;
		}
		Cursor = Cursors.Default;
	}

	private bool verificarSiReqPendiente(DataTable dat_req)
	{
		foreach (DataRow fila in dat_req.Rows)
		{
			if (Convert.ToInt32(fila.Field<object>("codEstado")) == 7)
			{
				return true;
			}
		}
		return false;
	}

	private bool verificarSiReqActivo(DataTable dat_req)
	{
		foreach (DataRow fila in dat_req.Rows)
		{
			if (Convert.ToInt32(fila.Field<object>("codEstado")) != 12)
			{
				return true;
			}
		}
		return false;
	}

	public void realizaProcesos()
	{
		if (!validarZonaYTecnico())
		{
			return;
		}
		if (cmbCategoriaCliente.Enabled)
		{
			cmbCategoriaCliente.Enabled = false;
		}
		if (!cargaPedido && !editar && toolStripGuardar.Text != "Guardar Venta" && nuevaOV)
		{
			CargaNumeracionOV();
			setPedido();
			if (SinStock == 1)
			{
				if (AdmPedido.insertarOrdenVentaSinStock(pedido))
				{
					CodPedido = pedido.CodPedido;
					if (CodPedido == "")
					{
						MessageBox.Show("Hubo un error al guardar pedido", "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					toolStripGuardar.Enabled = false;
					toolStripEditaov.Enabled = false;
					txtBruto.Enabled = false;
					txtDscto.Enabled = false;
					sololectura(estado: true);
					textBoxX2.Text = pedido.SerieDoc;
					textBoxX1.Text = Convert.ToString(pedido.Numeracion);
					nuevaOV = false;
					toolStripGuardar.Enabled = false;
					MessageBox.Show("Los datos se guardaron correctamente!\nPedido: " + Convert.ToInt32(pedido.Numeracion) + "\nTotal: " + pedido.Total + ((pedido.Moneda == 1) ? " Soles" : " Dolares"), "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					inicializarForm();
				}
			}
			else
			{
				if (!AdmPedido.insertarOrdenVenta(pedido))
				{
					MessageBox.Show("Ocurrió un error al registrar la operación", "Orden de Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				CodPedido = pedido.CodPedido;
				if (CodPedido == "")
				{
					MessageBox.Show("Hubo un error al guardar pedido", "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				toolStripGuardar.Enabled = false;
				toolStripEditaov.Enabled = false;
				txtBruto.Enabled = false;
				txtDscto.Enabled = false;
				sololectura(estado: true);
				btnvercombos.Enabled = false;
				textBoxX2.Text = pedido.SerieDoc;
				textBoxX1.Text = Convert.ToString(pedido.Numeracion);
				nuevaOV = false;
				toolStripGuardar.Enabled = false;
				MessageBox.Show("Los datos se guardaron correctamente!\nPedido: " + Convert.ToInt32(pedido.Numeracion) + "\nTotal: " + pedido.Total + ((pedido.Moneda == 1) ? " Soles" : " Dolares"), "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				if (generadoDeGuiadeRemisionCompra)
				{
					clsGuiaRemisionCompraDocumentoRelacionado nuevo = new clsGuiaRemisionCompraDocumentoRelacionado();
					nuevo.CodGuiaRemisionCompra = Convert.ToInt32(grc.CodGuiaRemision);
					nuevo.CodDocumentoRelacionado = Convert.ToInt32(pedido.CodPedido);
					nuevo.CodTipoDocumento = pedido.CodTipoDocumento;
					nuevo.TipoGRCDR = 2;
					nuevo.Anulado = 0;
					if (admgrc.insertarDocumentoRelacionado(nuevo))
					{
						if (ventanagrc != null)
						{
							ventanagrc.ctdaddocrelacionados--;
							ventanagrc.DespuesDeGenerarDocumentoRelacionado(generado: true, nuevo);
						}
					}
					else
					{
						MessageBox.Show("Orden de Venta NO VINCULADO a Guia De Remision Compra", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				inicializarForm();
			}
		}
		if (cargaPedido && !editar && toolStripGuardar.Text == "Guardar Venta")
		{
			param = admParam.ObtenerParametro(6);
			decimal ventaticket = Convert.ToDecimal(param.valor);
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			ds = dBAccess.ExecuteDataSet("SumTotalTickesMes");
			DataTable totalventalista = ds.Tables[0];
			decimal totalventa = Convert.ToDecimal(totalventalista.Rows[0]["total"]) + Convert.ToDecimal(txtPrecioVenta.Text);
			if (chkTicket.Checked)
			{
				if (!(ventaticket > totalventa))
				{
					MessageBox.Show("Supero Limite de Monto total Tickes", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				DataTable dat_req = admreqalm.CargaRequerimientosSegunPedido(Convert.ToInt32(pedido.CodPedido));
				if (dat_req != null && verificarSiReqPendiente(dat_req))
				{
					MessageBox.Show("Tiene Requerimientos de Almacen Pendientes de Aprobacion , Total mes : " + ventaticket, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				guardaVenta();
			}
			else
			{
				DataTable dat_req2 = admreqalm.CargaRequerimientosSegunPedido(Convert.ToInt32(pedido.CodPedido));
				if (dat_req2 != null && verificarSiReqPendiente(dat_req2))
				{
					MessageBox.Show("Tiene Requerimientos de Almacen Pendientes de Aprobacion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				guardaVenta();
			}
		}
		if (cargaPedido && editar)
		{
			if (!verificacionEdicionEliminacionOV())
			{
				return;
			}
			setPedido();
			toolStripGuardar.Enabled = true;
			if (AdmPedido.update(pedido))
			{
				RecorreDetalle();
				foreach (clsDetallePedido deta in detalle)
				{
					if (deta.Tipoimpuesto.Contains('1') && deta.Igv == 0m)
					{
						MessageBox.Show("El producto con codigo: " + deta.CodProducto + " tiene calculado el IGV en cero", "No se puede actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					if (deta.CodDetallePedido == 0)
					{
						if (SinStock == 1)
						{
							AdmPedido.insertdetalleSinStock(deta);
						}
						else
						{
							AdmPedido.insertdetalle(deta);
						}
					}
					else if (deta.CodDetallePedido > 0)
					{
						AdmPedido.updatedetalle(deta);
					}
				}
				CodPedido = pedido.CodPedido;
				MessageBox.Show("Los datos se actualizaron correctamente!\nPedido: " + Convert.ToInt32(pedido.Numeracion) + "\nTotal: " + pedido.Total + " Soles.", "Pedido Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				sololectura(estado: true);
				toolStripButtonPendiente.Enabled = true;
				inicializarForm();
			}
			itemsEliminados.Clear();
			listadoItemsVerificar.Clear();
			btnreq.Visible = false;
		}
		bandEditar = false;
		bandNuevo = false;
	}

	private bool validarZonaYTecnico()
	{
		bool rpta = true;
		if (obligarElegir)
		{
			rpta = cmbTecnico.SelectedValue != null;
			if (!rpta)
			{
				MessageBox.Show("Debe seleccionar un tecnico", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cmbTecnico.Focus();
			}
			else
			{
				rpta = cmbZona.SelectedValue != null;
				if (!rpta)
				{
					MessageBox.Show("Debe seleccionar una zona", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					cmbZona.Focus();
				}
			}
		}
		return rpta;
	}

	private bool verificacionEdicionEliminacionOV()
	{
		bool rpta = true;
		if (listadoItemsVerificar.Count > 0)
		{
			List<int> codReqAlm = new List<int>();
			List<int> AlmacenesItemEditados = Enumerable.Select<itemVerificarRA, int>((IEnumerable<itemVerificarRA>)listadoItemsVerificar, (Func<itemVerificarRA, int>)((itemVerificarRA x) => x.codAlmacen)).Distinct().ToList();
			for (int j = 0; j < listadoItemsVerificar.Count; j++)
			{
				itemVerificarRA item = listadoItemsVerificar[j];
				clsRequerimientoAlmacen reqAlm = admreqalm.CargaRequerimientosSegun(Convert.ToInt32(pedido.CodPedido), item.codAlmacen, -1);
				if (reqAlm == null)
				{
					continue;
				}
				List<clsDetalleRequerimientoAlmacen> detalle = admreqalm.CargaDetalleRequerimientoAlmacen(reqAlm.Codigo);
				List<clsDetalleRequerimientoAlmacen> detBuscado = Enumerable.Where<clsDetalleRequerimientoAlmacen>(detalle.AsEnumerable(), (Func<clsDetalleRequerimientoAlmacen, bool>)((clsDetalleRequerimientoAlmacen x) => x.CodProducto == item.codProducto && x.CodUnidad == item.codUnidad)).ToList();
				if (detBuscado.Count > 0)
				{
					itemVerificarRA aux = listadoItemsVerificar[listadoItemsVerificar.IndexOf(item)];
					aux.codReqAlm = reqAlm.Codigo;
					aux.codDetalleReqAlm = detBuscado[0].Codigo;
					aux.refProducto = detBuscado[0].RefProducto;
					aux.aprobada = reqAlm.IEstado == 13;
					if (reqAlm.IEstado != 13 && reqAlm.IEstado != 7)
					{
						MessageBox.Show("No se ah contemplado que un requerimiento tenga otro tipo de estado. Verificar Req ID: " + reqAlm.Codigo, "Error de Estado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return false;
					}
					listadoItemsVerificar[listadoItemsVerificar.IndexOf(item)] = aux;
				}
			}
			List<itemVerificarRA> listadoProdConRAAprob = Enumerable.OrderBy<itemVerificarRA, int>(Enumerable.Where<itemVerificarRA>((IEnumerable<itemVerificarRA>)listadoItemsVerificar, (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codReqAlm > 0 && x.aprobada)), (Func<itemVerificarRA, int>)((itemVerificarRA x) => x.codAlmacen)).ToList();
			string mensaje = "Los siguientes productos tienen requerimiento aprobados. Para realizar las modificaciones debe anular los requerimientos: ";
			int i = 0;
			foreach (itemVerificarRA item2 in listadoProdConRAAprob)
			{
				clsAlmacen alm = admalma.CargaAlmacen(item2.codAlmacen);
				clsRequerimientoAlmacen reqa = admreqalm.CargaRequerimiento(item2.codReqAlm);
				if (i == 0)
				{
					mensaje = mensaje + "\n" + alm.Descripcion + ":";
				}
				else if (listadoItemsVerificar[i].codAlmacen != listadoItemsVerificar[i - 1].codAlmacen)
				{
					mensaje = mensaje + "\n" + alm.Descripcion + ":";
				}
				mensaje = mensaje + "\n > " + item2.refProducto + " -> Req. " + reqa.SEstado + " -> " + reqa.NumSerie + "-" + reqa.NumDocumento;
				i++;
			}
			if (i != 0)
			{
				DialogResult rpta2 = MessageBox.Show(mensaje + "\nProcede la Anulacion?", "Req. Almacen Tipo Venta Aprobados", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rpta2 == DialogResult.Yes)
				{
					List<int> codReqsModif = Enumerable.Select<itemVerificarRA, int>((IEnumerable<itemVerificarRA>)listadoProdConRAAprob, (Func<itemVerificarRA, int>)((itemVerificarRA x) => x.codReqAlm)).Distinct().ToList();
					if (listadoProdConRAAprob.Count > 0)
					{
						foreach (int icodRA in codReqsModif)
						{
							bool badnAnulado = admreqalm.anular(icodRA, frmLogin.iCodUser);
							DataTable listadoCodTrans = admreqalm.cargaTransferenciasAprobadas(icodRA);
							if (listadoCodTrans != null)
							{
								if (listadoCodTrans.Rows.Count > 1)
								{
									MessageBox.Show("Un Requerimiento de Almacen de Tipo Venta no puede tener mas de una Transferencia Aprobada.\ncodReq: " + icodRA, "Error Requerimiento No Anulado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
								if (listadoCodTrans.Rows.Count == 1)
								{
									clsRequerimientoAlmacen req_alm = admreqalm.CargaRequerimiento(icodRA);
									int codTransDir = Convert.ToInt32(listadoCodTrans.Rows[0].Field<object>(0));
									clsTipoDocumento tipodoc = Admdoc.BuscaTipoDocumento("DET");
									clsTransferencia transf = admTransa.CargaTransferencia(codTransDir);
									extornacion = new clsTransferencia();
									extornacion.codTransAExtornar = codTransDir;
									extornacion.CodAlmacenOrigen = req_alm.CodAlmacenSolicitante;
									extornacion.CodAlmacenDestino = req_alm.CodAlmacenDespacho;
									extornacion.CodTipoDocumento = tipodoc.CodTipoDocumento;
									extornacion.FechaEnvio = DateTime.Now;
									extornacion.FechaEntrega = DateTime.Now;
									extornacion.FormaPago = 0;
									extornacion.FechaPago = DateTime.Now.Date;
									extornacion.CodListaPrecio = 0;
									string comentario = "Documento de Extornacion para Transf: " + codTransDir;
									extornacion.Comentario = comentario;
									extornacion.DescripcionRechazo = "";
									extornacion.CodUser = frmLogin.iCodUser;
									extornacion.Estado = 1;
									extornacion.Codserie = transf.Codserie;
									extornacion.Serie = transf.Serie;
									extornacion.Numerodocumento = transf.Numerodocumento;
									extornacion.Moneda = 1;
									obtenerDetalleParaTransferencia(req_alm);
									if (detalleExtor.Count > 0 && admTransa.insert(extornacion))
									{
										foreach (clsDetalleTransferencia det in detalleExtor)
										{
											det.CodTransDir = Convert.ToInt32(extornacion.CodTransDir);
											admTransa.insertdetalle(det);
										}
										apruebaTransferencia(extornacion);
									}
								}
							}
							List<itemVerificarRA> listadoItemsEliminar = Enumerable.Where<itemVerificarRA>((IEnumerable<itemVerificarRA>)listadoProdConRAAprob, (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codReqAlm == icodRA && x.proceso == 2)).ToList();
							foreach (itemVerificarRA item3 in listadoItemsEliminar)
							{
								List<DataGridViewRow> encontrado = Enumerable.Where<DataGridViewRow>(itemsEliminados.AsEnumerable(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToInt32(x.Cells[codproducto.Index].Value) == item3.codProducto && Convert.ToInt32(x.Cells[codunidad.Index].Value) == item3.codUnidad && Convert.ToInt32(x.Cells[codalmacen.Index].Value) == item3.codAlmacen)).ToList();
								if (encontrado.Count > 0)
								{
									int coddetallep = Convert.ToInt32(encontrado[0].Cells[coddetalle.Index].Value);
									AdmPedido.deletedetalle(coddetallep);
								}
							}
						}
					}
				}
				else
				{
					foreach (itemVerificarRA item4 in listadoProdConRAAprob)
					{
						if (item4.proceso == 1)
						{
							List<DataGridViewRow> encontrado2 = Enumerable.Where<DataGridViewRow>(dgvdetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToInt32(x.Cells[codproducto.Name].Value) == item4.codProducto && Convert.ToInt32(x.Cells[codunidad.Name].Value) == item4.codUnidad && Convert.ToInt32(x.Cells[codalmacen.Name].Value) == item4.codAlmacen)).ToList();
							if (encontrado2.Count > 0)
							{
								int indice = dgvdetalle.Rows.IndexOf(encontrado2[0]);
								DataGridViewRow fila = dgvdetalle.Rows[indice];
								decimal cantidad = default(decimal);
								decimal total = default(decimal);
								decimal total_icbper = default(decimal);
								bool ibperband = false;
								int tipoImpuesto = 0;
								decimal precioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
								cantidad = Convert.ToDecimal(item4.anteriorCantidad);
								total = Math.Round(Convert.ToDecimal(item4.anteriorCantidad) * precioUnitario, 2);
								tipoImpuesto = Convert.ToInt32(fila.Cells[Tipoimpuesto.Name].Value);
								decimal bruto = total;
								decimal precioventa;
								decimal valorventa;
								if (tipoImpuesto == 1)
								{
									precioventa = total;
									decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
									valorventa = precioventa / factorigv;
								}
								else
								{
									valorventa = total;
									precioventa = valorventa;
								}
								decimal precioreal = precioventa / cantidad;
								decimal valorreal = valorventa / cantidad;
								decimal igv = precioventa - valorventa;
								if (Convert.ToInt32(fila.Cells[icbper_band.Name].Value) > 0)
								{
									total_icbper = frmLogin.Configuracion.Icbper * cantidad;
									ibperband = true;
								}
								else
								{
									total_icbper = default(decimal);
									ibperband = false;
								}
								fila.Cells[this.cantidad.Name].Value = cantidad;
								fila.Cells[importe.Name].Value = bruto;
								fila.Cells[this.valorventa.Name].Value = valorventa;
								fila.Cells[this.igv.Name].Value = igv;
								fila.Cells[this.precioventa.Name].Value = precioventa;
								fila.Cells[valoreal.Name].Value = valorreal;
								fila.Cells[this.precioreal.Name].Value = precioreal;
								fila.Cells[precioconigv.Name].Value = precioventa;
								fila.Cells[icbper1.Name].Value = total_icbper;
								fila.Cells[icbper_band.Name].Value = ibperband;
							}
						}
						else if (item4.proceso == 2)
						{
							List<DataGridViewRow> encontrado3 = Enumerable.Where<DataGridViewRow>(itemsEliminados.AsEnumerable(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToInt32(x.Cells[codproducto.Index].Value) == item4.codProducto && Convert.ToInt32(x.Cells[codunidad.Index].Value) == item4.codUnidad && Convert.ToInt32(x.Cells[codalmacen.Index].Value) == item4.codAlmacen)).ToList();
							if (encontrado3.Count > 0)
							{
								dgvdetalle.Rows.Add(encontrado3[0]);
							}
						}
					}
				}
			}
			bool flag = true;
			List<itemVerificarRA> listadoProdConRAPend = Enumerable.Where<itemVerificarRA>((IEnumerable<itemVerificarRA>)listadoItemsVerificar, (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codReqAlm > 0 && !x.aprobada)).ToList();
			List<int> codReqsModif2 = Enumerable.Select<itemVerificarRA, int>((IEnumerable<itemVerificarRA>)listadoProdConRAPend, (Func<itemVerificarRA, int>)((itemVerificarRA x) => x.codReqAlm)).Distinct().ToList();
			if (listadoProdConRAPend.Count > 0)
			{
				foreach (int icodRA2 in codReqsModif2)
				{
					bool anular = false;
					clsRequerimientoAlmacen raux = admreqalm.CargaRequerimiento(icodRA2);
					List<clsDetalleRequerimientoAlmacen> ldetaux = admreqalm.CargaDetalleRequerimientoAlmacen(icodRA2);
					List<itemVerificarRA> listadoItemsEdit = Enumerable.Where<itemVerificarRA>((IEnumerable<itemVerificarRA>)listadoProdConRAPend, (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codReqAlm == icodRA2)).ToList();
					for (int j2 = 0; j2 < listadoItemsEdit.Count; j2++)
					{
						itemVerificarRA item5 = listadoItemsEdit[j2];
						clsDetalleRequerimientoAlmacen ideta = Enumerable.Where<clsDetalleRequerimientoAlmacen>(ldetaux.AsEnumerable(), (Func<clsDetalleRequerimientoAlmacen, bool>)((clsDetalleRequerimientoAlmacen x) => x.Codigo == item5.codDetalleReqAlm)).First();
						int pos = ldetaux.IndexOf(ideta);
						if (item5.proceso == 1)
						{
							decimal num = (ideta.CantidadPendiente = Convert.ToDecimal(item5.nuevaCantidad));
							decimal num3 = (ideta.CantidadPedida = num);
							decimal num5 = (ideta.CantidadConfirmada = num3);
							ideta.Cantidad = num5;
							ldetaux[pos] = ideta;
						}
						else
						{
							if (item5.proceso != 2)
							{
								continue;
							}
							if (ldetaux.Count > 1)
							{
								ldetaux.RemoveAt(pos);
								List<DataGridViewRow> encontrado4 = Enumerable.Where<DataGridViewRow>(itemsEliminados.AsEnumerable(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToInt32(x.Cells[codproducto.Index].Value) == item5.codProducto && Convert.ToInt32(x.Cells[codunidad.Index].Value) == item5.codUnidad && Convert.ToInt32(x.Cells[codalmacen.Index].Value) == item5.codAlmacen)).ToList();
								if (encontrado4.Count > 0)
								{
									int coddetallep2 = Convert.ToInt32(encontrado4[0].Cells[coddetalle.Index].Value);
									AdmPedido.deletedetalle(coddetallep2);
								}
							}
							else
							{
								anular = true;
							}
						}
					}
					if (anular)
					{
						if (!admreqalm.anular(icodRA2, frmLogin.iCodUser))
						{
							continue;
						}
						DataTable listadoCodTrans2 = admreqalm.cargaTransferenciasPendientes(icodRA2);
						if (listadoCodTrans2 != null)
						{
							foreach (DataRow fila2 in listadoCodTrans2.Rows)
							{
								int codTransDir2 = Convert.ToInt32(fila2.Field<object>(0));
								admTransa.rechazado(codTransDir2, "Transferencia Anulada por anulacion de Requerimiento de O.V N°: " + CodPedido);
							}
						}
						clsRequerimientoAlmacen req_alm2 = admreqalm.CargaRequerimiento(icodRA2);
						req_alm2.ListadoDetalle = admreqalm.CargaDetalleRequerimientoAlmacen(icodRA2);
						foreach (clsDetalleRequerimientoAlmacen item6 in req_alm2.ListadoDetalle)
						{
							admreqalm.retornarStock(req_alm2.CodAlmacenDespacho, item6.CodProducto, item6.CodUnidad, item6.CantidadPendienteAprobada, modificarStockActual: true, item6.Codigo);
						}
					}
					else
					{
						admreqalm.update(raux, ldetaux, ldetaux);
					}
				}
			}
			List<itemVerificarRA> listadoItemsEliminar2 = Enumerable.Where<itemVerificarRA>((IEnumerable<itemVerificarRA>)listadoItemsVerificar, (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codReqAlm == 0 && x.proceso == 2)).ToList();
			foreach (itemVerificarRA item7 in listadoItemsEliminar2)
			{
				List<DataGridViewRow> encontrado5 = Enumerable.Where<DataGridViewRow>(itemsEliminados.AsEnumerable(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToInt32(x.Cells[codproducto.Index].Value) == item7.codProducto && Convert.ToInt32(x.Cells[codunidad.Index].Value) == item7.codUnidad && Convert.ToInt32(x.Cells[codalmacen.Index].Value) == item7.codAlmacen)).ToList();
				if (encontrado5.Count > 0)
				{
					int coddetallep3 = Convert.ToInt32(encontrado5[0].Cells[coddetalle.Index].Value);
					AdmPedido.deletedetalle(coddetallep3);
				}
			}
		}
		calculatotales();
		montosventa();
		return rpta;
	}

	private bool validaapertura()
	{
		DataTable aux = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		aux.Rows.RemoveAt(0);
		int cont = 0;
		clsCaja aper = new clsCaja();
		List<string> alma = new List<string>();
		foreach (DataRow row in aux.Rows)
		{
			aper = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, Convert.ToInt32(row.ItemArray[0]), frmLogin.iCodUser);
			if (aper != null)
			{
				if (aper.Estado)
				{
					cont++;
				}
				else
				{
					alma.Add(row.ItemArray[1].ToString());
				}
			}
			else
			{
				alma.Add(row.ItemArray[1].ToString());
			}
		}
		if (cont != aux.Rows.Count)
		{
			string cad = "";
			foreach (string item in alma)
			{
				cad = " " + item + "-";
			}
			if (cad.Length > 0)
			{
				MessageBox.Show("Aun no aperturo las siguientes cajas: " + cad, "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			return false;
		}
		return true;
	}

	private void CargaTransaccion()
	{
		string codTransacion = "";
		tran = new clsTransaccion();
		codTransacion = "FT";
		tran = AdmTran.MuestraTransaccionS(codTransacion, 1);
		tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
	}

	public void ArmaCabecera(int codalma)
	{
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
		decimal total_icbper = default(decimal);
		if (dgvdetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			if (Convert.ToInt32(row.Cells[codalmacen.Name].Value) == codalma)
			{
				bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
				Dscto += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
				valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
				total_icbper += Convert.ToDecimal(row.Cells[icbper1.Name].Value);
				if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "21")
				{
					montogratuitas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				}
				if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "10" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "11" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "12" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "13" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "14" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "15" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "16" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "17")
				{
					montogravadas += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
					banderagrabada = true;
				}
				if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "20")
				{
					montoexoneradas += Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value) - Convert.ToDecimal(row.Cells[montodscto.Name].Value);
					banderaexonerada = true;
				}
				if (Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "30" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "31" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "32" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "33" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "34" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "35" || Convert.ToString(row.Cells[Tipoimpuesto.Name].Value) == "36")
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
		string montoBruto = $"{bruto + total_icbper:#,##0.00}";
		string montovv = $"{valor:#,##0.00}";
		string montoigv = $"{bruto - Dscto - valor:#,##0.00}";
		string montotal = $"{bruto - Dscto:#,##0.00}";
		venta.MontoBruto = Convert.ToDecimal(montoBruto);
		venta.MontoDscto = Convert.ToDecimal(montodescuento);
		venta.Igv = Convert.ToDecimal(montoigv);
		venta.Total = Convert.ToDecimal(montotal) + total_icbper;
		venta.icbper = Convert.ToDecimal(total_icbper);
	}

	private async void guardaVenta()
	{
		Cursor = Cursors.WaitCursor;
		bool bandTerminoGuardadoVenta = false;
		try
		{
			anulados = 0;
			if (superValidator1.Validate())
			{
				decimal totalsoles = ((Convert.ToInt32(cli.Moneda) != 1) ? (Convert.ToDecimal(txtPrecioVenta.Text) * Convert.ToDecimal(mdi_Menu.tc_hoy)) : Convert.ToDecimal(txtPrecioVenta.Text));
				if (totalsoles > Convert.ToDecimal(txtLineaCreditoDisponible.Text) && Convert.ToInt32(cmbFormaPago.SelectedValue) != 6)
				{
					MessageBox.Show("El Monto Excede a la Línea de Crédito");
				}
				else
				{
					Enumerable.Select<DataGridViewRow, object>(dgvdetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, object>)((DataGridViewRow z) => z.Cells[empres.Name].Value)).Distinct().ToList();
					List<object> alma = Enumerable.Select<DataGridViewRow, object>(dgvdetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, object>)((DataGridViewRow y) => y.Cells[codalmacen.Name].Value)).Distinct().ToList();
					lista_facturas = new List<clsFacturaVenta>();
					new clsCaja();
					int cont = 0;
					List<string> nomAlma = new List<string>();
					foreach (DataRow row in aux.Rows)
					{
						clsCaja aper = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, Convert.ToInt32(row.ItemArray[0]), frmLogin.iCodUser);
						if (aper != null)
						{
							if (!aper.Estado)
							{
								nomAlma.Add(row.ItemArray[1].ToString());
							}
							else
							{
								cont++;
							}
						}
						else
						{
							nomAlma.Add(row.ItemArray[1].ToString());
						}
					}
					if (cont != aux.Rows.Count)
					{
						string cad = "";
						foreach (string item in nomAlma)
						{
							cad = cad + "\n- " + item;
						}
						if (cad.Length > 0)
						{
							MessageBox.Show("Aun no aperturo las siguientes cajas: " + cad, "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
					}
					foreach (object e in alma)
					{
						admsucu.sucursalxalmacen(Convert.ToInt32(e));
						int codemp = admEmpresa.empresaxalmacen(Convert.ToInt32(e));
						this.venta = new clsFacturaVenta();
						this.venta = obtenerDatosVenta(Convert.ToInt32(e));
						foreach (clsPedido ped in PedidosIngresados)
						{
							ped.CodigoBarras = this.venta.CodigoBarras;
							ped.CodigoBarrasCifrado = this.venta.CodigoBarrasCifrado;
						}
						if (txtCodCliente.Text.ToString() == "00000000")
						{
							this.venta.Nombre = txtNombreCliente.Text;
						}
						else
						{
							this.venta.Nombre = txtNombreCliente.Text;
						}
						ArmaCabecera(Convert.ToInt32(e));
						ser = AdmSerie.CargaSerieEmpresa(Convert.ToInt32(e), doc.CodTipoDocumento);
						if (ser == null)
						{
							throw new Exception("\tError Serie de Venta\nNo se encontro una serie para registrar la venta.\nError: AdmSerie.CargaSerieEmpresa(" + Convert.ToInt32(e) + ", " + doc.CodTipoDocumento + ");");
						}
						this.venta.CodSerie = ser.CodSerie;
						this.venta.Serie = ser.Serie;
						this.venta.NumDoc = ser.Numeracion.ToString().PadLeft(8, '0');
						new clsFacturaVenta();
						clsFacturaVenta factura = AdmVenta.FechaCorrelativoAnterior(this.venta.CodSerie);
						this.venta.CodEmpresa = codemp;
						if (factura.FechaSalida.Date > this.venta.FechaSalida.Date)
						{
							throw new Exception("Error de Fecha de Venta\nError No se puede Registrar los Datos. Verifique Fecha");
						}
						RecorreDetalleVenta(Convert.ToInt32(e));
						this.venta.Detalle = detalle1;
						if (detalle1.Count > 0)
						{
							this.venta.Pendiente = Convert.ToDecimal(this.venta.Total);
							fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
							dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
							if (fpago.Dias > 0 && this.venta.CodTipoTransaccion == 7)
							{
								if (!AdmVenta.insertComprobante(this.venta))
								{
									throw new Exception("VENTA AL CREDITO INCOMPLETA\nOcurrio un error al guardar venta al credito.");
								}
								CodVenta = this.venta.CodFacturaVenta;
								lista_facturas.Add(this.venta);
								if (this.venta.FormaPago != 6)
								{
									toolStripImprimir.Visible = true;
								}
								if (!chkTicket.Checked)
								{
									await facturacion.GeneraDocumento(cli, this.venta, detalle1, 0);
									this.venta.Qr = facturacion.LogoEmp;
								}
								frmMensajeCredito men = new frmMensajeCredito();
								men.ShowDialog();
							}
							else
							{
								frmCancelarPago form = new frmCancelarPago();
								form.VentComp = 1;
								form.tipo = 3;
								form.CodCliente = cli.CodCliente;
								form.venta = this.venta;
								form.opcionSuma = 1;
								form.pagoventa = 1;
								form.ShowDialog();
								if (form.caja_aperturada)
								{
									if (!form.ventana_cobro)
									{
										throw new Exception("Se cancelo el registro del pago para la venta en el formulario de pagos.");
									}
									if (!form.ventaRecibida)
									{
										lista_facturas.Add(this.venta);
										throw new Exception("Ocurrió un problema al registrar la venta en el formulario de pagos.");
									}
									CodVenta = this.venta.CodFacturaVenta;
									lista_facturas.Add(this.venta);
									if (this.venta.FormaPago != 6)
									{
										toolStripImprimir.Visible = true;
									}
									if (!chkTicket.Checked)
									{
										await facturacion.GeneraDocumento(cli, this.venta, detalle1, 0);
										this.venta.Qr = facturacion.LogoEmp;
									}
								}
							}
						}
						else
						{
							MessageBox.Show("No hay items en el comprobante.", "Registro de Venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							this.venta = new clsFacturaVenta();
							new clsFacturaVenta();
						}
						foreach (clsPedido ped2 in PedidosIngresados)
						{
							AdmPedido.GuardaCodigoBarras(ped2);
						}
						PedidosIngresados = new List<clsPedido>();
					}
					foreach (clsFacturaVenta fv in lista_facturas)
					{
						if (fv.CodFacturaVenta != null)
						{
							fnImprimir(fv);
						}
					}
					if (anulados == 0)
					{
						textBoxX2.Text = this.venta.Serie;
						textBoxX1.Text = this.venta.NumDoc;
						toolStripImprimir.Enabled = true;
						toolStripGuardar.Enabled = false;
						toolStripEditaov.Enabled = false;
						toolstripEfectivo.Enabled = false;
						cmbMoneda.Enabled = false;
					}
					bandTerminoGuardadoVenta = true;
				}
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.Message.ToString(), "Error en la generacion de venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			Cursor = Cursors.WaitCursor;
			foreach (clsFacturaVenta venta in lista_facturas)
			{
				if (venta.CodFacturaVenta != null && !AdmVenta.ValidaAnulacionVenta(Convert.ToInt32(venta.CodFacturaVenta)) && !AnulandoVentaEnTryCatchGeneracionVenta(venta))
				{
					MessageBox.Show("No se anulo la siguiente venta: ", "Error al anular venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (lista_facturas.Count != 0)
			{
				AdmPedido.activaPedidoVenta(Convert.ToInt32(lista_facturas[0].CodPedido));
			}
			bandTerminoGuardadoVenta = false;
			Cursor = Cursors.Default;
		}
		finally
		{
			Cursor = Cursors.Default;
		}
		if (bandTerminoGuardadoVenta)
		{
			CreacionDespacho(lista_facturas);
		}
	}

	private bool AnulandoVentaEnTryCatchGeneracionVenta(clsFacturaVenta venta)
	{
		bool bandera = false;
		using (TransactionScope Scope = new TransactionScope())
		{
			if (!(bandera = AdmVenta.anular(Convert.ToInt32(venta.CodFacturaVenta))))
			{
				Transaction.Current.Rollback();
				Scope.Dispose();
				MessageBox.Show("La Venta No Ah Sido Anulada", "Anulacion Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return bandera;
			}
			DataTable dtPagos = AdmPagos.GetPagosVenta(Convert.ToInt32(venta.CodAlmacen), Convert.ToInt32(venta.CodFacturaVenta));
			foreach (DataRow fila in dtPagos.Rows)
			{
				bandera = AdmPagos.AnularPago(Convert.ToInt32(fila[0]));
				if (!bandera)
				{
					MessageBox.Show("No se pudo anular el pago especificado.\nCod Pago: " + Convert.ToInt32(fila[0]), "Anulacion de pago incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Transaction.Current.Rollback();
					Scope.Dispose();
					return bandera;
				}
			}
			int codNotaSalida = 0;
			clsNotaIngreso nota2 = AdmVenta.BuscaNotaSalida(Convert.ToInt32(venta.CodFacturaVenta), Convert.ToInt32(venta.CodAlmacen));
			if (nota2 == null)
			{
				MessageBox.Show("Error al consultar Venta", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Transaction.Current.Rollback();
				Scope.Dispose();
				return bandera;
			}
			codNotaSalida = Convert.ToInt32(nota2.CodNotaIngreso);
			clsTransaccion trans = AdmTran.MuestraTransaccion(11);
			nota2.CodTipoTransaccion = trans.CodTransaccion;
			clsTipoDocumento doc = Admdoc.BuscaTipoDocumento("DIA");
			clsSerie ser = AdmSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
			nota2.Serie = nota2.Serie;
			nota2.NumDoc = Convert.ToString(nota2.NumDoc);
			nota2.DescripcionTransaccion = trans.Descripcion;
			nota2.CodTipoDocumento = doc.CodTipoDocumento;
			nota2.CodSerie = ser.CodSerie;
			nota2.CodReferencia = nota2.DocumentoReferencia;
			if (!AdmIngreso.insert(nota2))
			{
				MessageBox.Show("No se pudo registrar el ingreso de productos!", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Transaction.Current.Rollback();
				Scope.Dispose();
				return bandera;
			}
			DataTable dt_AnulaVenta = AdmVenta.CargaDetalleNotaSalida(Convert.ToInt32(codNotaSalida), nota2.CodAlmacen);
			List<clsDetalleNotaIngreso> lstNotaIng = LeeProductos(dt_AnulaVenta, nota2);
			if (lstNotaIng == null)
			{
				bandera = false;
			}
			else
			{
				foreach (clsDetalleNotaIngreso det in lstNotaIng)
				{
					if (!AdmIngreso.insertdetalle(det))
					{
						MessageBox.Show("No se puede retornar el siguiente producto: \nProducto: " + det.CodProducto + " - " + det.DescripcionProducto, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						Transaction.Current.Rollback();
						Scope.Dispose();
						return bandera;
					}
				}
			}
			if (!bandera)
			{
				MessageBox.Show("ocurrio un error inesperado al anular venta", "Anulacion Incompleta de Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Transaction.Current.Rollback();
				Scope.Dispose();
				return bandera;
			}
			Scope.Complete();
			Scope.Dispose();
		}
		return bandera;
	}

	private List<clsDetalleNotaIngreso> LeeProductos(DataTable dt_AnulaVenta, clsNotaIngreso nota2)
	{
		try
		{
			List<clsDetalleNotaIngreso> lstNotaIng = new List<clsDetalleNotaIngreso>();
			foreach (DataRow row3 in dt_AnulaVenta.Rows)
			{
				clsDetalleNotaIngreso DetIng = new clsDetalleNotaIngreso();
				DetIng.CodProducto = Convert.ToInt32(row3[1]);
				DetIng.CodNotaIngreso = Convert.ToInt32(nota2.CodNotaIngreso);
				DetIng.CodAlmacen = Convert.ToInt32(row3[3]);
				DetIng.UnidadIngresada = Convert.ToInt32(row3[4]);
				DetIng.Cantidad = Convert.ToDouble(row3[6]);
				DetIng.PrecioUnitario = Convert.ToDouble(row3[29]);
				DetIng.Descuento1 = Convert.ToDouble(row3[9]);
				DetIng.Descuento2 = Convert.ToDouble(row3[10]);
				DetIng.Descuento3 = Convert.ToDouble(row3[11]);
				DetIng.MontoDescuento = Convert.ToDouble(row3[12]);
				DetIng.Importe = Convert.ToDouble(row3[29]) * DetIng.Cantidad;
				DetIng.Subtotal = DetIng.PrecioUnitario * DetIng.Cantidad;
				DetIng.Igv = DetIng.Importe - DetIng.Subtotal;
				DetIng.PrecioReal = Convert.ToDouble(row3[29]) * Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				DetIng.ValoReal = Convert.ToDouble(row3[29]);
				DetIng.CodUser = Convert.ToInt32(row3[17]);
				DetIng.Estado = true;
				lstNotaIng.Add(DetIng);
			}
			return lstNotaIng;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error al anular venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return null;
		}
	}

	private clsFacturaVenta obtenerDatosVenta(int codAlmacen)
	{
		venta = new clsFacturaVenta();
		int codsucu = admsucu.sucursalxalmacen(codAlmacen);
		int codemp = admEmpresa.empresaxalmacen(codAlmacen);
		venta.valorRetencion = (chkRetencion.Checked ? 1 : 0);
		venta.CodSucursal = codsucu;
		venta.CodAlmacen = codAlmacen;
		cli = AdmCli.MuestraCliente(pedido.CodCliente);
		if (cli.RucDni.Length == 8)
		{
			if (!chkTicket.Checked)
			{
				chkBoleta.Checked = true;
			}
			cli.DocumentoIdentidad = new clsDocumentoIdentidad();
			cli.DocumentoIdentidad.CodDocumentoIdentidad = 1;
			cli.DocumentoIdentidad.CodigoSunat = 1;
		}
		else if (cli.RucDni.Length == 11)
		{
			chkFactura.Checked = true;
			cli.DocumentoIdentidad = new clsDocumentoIdentidad();
			cli.DocumentoIdentidad.CodDocumentoIdentidad = 3;
			cli.DocumentoIdentidad.CodigoSunat = 6;
		}
		txtCodCliente.Text = cli.RucDni;
		txtNombreCliente.Text = cli.RazonSocial;
		txtDireccion.Text = cli.DireccionLegal;
		venta.DocumentoIdentidad = new clsDocumentoIdentidad();
		venta.DocumentoIdentidad = cli.DocumentoIdentidad;
		venta.CodCliente = pedido.CodCliente;
		venta.NumeroDocumentoCliente = txtCodCliente.Text;
		venta.idTecnico = pedido.idTecnico;
		venta.idZona = pedido.idZona;
		venta.CodCanalVenta = pedido.CodCanalVenta;
		venta.Detallecomentario = "";
		venta.CodCotizacion = 0;
		if (chkBoleta.Checked)
		{
			venta.Boletafactura = 1;
			doc.CodTipoDocumento = 1;
			venta.CodTipoDocumento = doc.CodTipoDocumento;
		}
		else if (chkFactura.Checked)
		{
			venta.Boletafactura = 2;
			doc.CodTipoDocumento = 2;
			venta.CodTipoDocumento = doc.CodTipoDocumento;
		}
		else if (chkTicket.Checked)
		{
			venta.Boletafactura = 3;
			doc.CodTipoDocumento = 8;
			venta.CodTipoDocumento = doc.CodTipoDocumento;
		}
		CargaTransaccion();
		venta.CodTipoTransaccion = tran.CodTransaccion;
		venta.NumDoc = txtPedido.Text;
		venta.Estado = 1;
		venta.Consultorext = false;
		venta.Codsalidaconsulext = 0;
		venta.CodPedido = Convert.ToInt32(pedido.CodPedido);
		venta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		venta.TipoCambio = mdi_Menu.tc_hoy;
		venta.FechaSalida = ((Convert.ToDateTime(dtpFecha.Value.Date) < DateTime.Now.Date) ? DateTime.Now : Convert.ToDateTime(dtpFecha.Value));
		venta.FechaPago = ((Convert.ToDateTime(dtpFechaPago.Value) < DateTime.Now) ? DateTime.Now : Convert.ToDateTime(dtpFechaPago.Value));
		venta.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
		venta.CodListaPrecio = 0;
		venta.CodVendedor = Convert.ToInt32(txtCodigoVendedor.Text);
		venta.Comentario = "";
		venta.CodUser = frmLogin.iCodUser;
		venta.Entregado = 1;
		venta.CodigoBarras = DateTime.Today.Year.ToString().Substring(2, 2) + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0') + DateTime.Now.ToShortTimeString().Substring(0, 2) + DateTime.Now.ToShortTimeString().Substring(3, 2) + venta.CodSerie.ToString().PadLeft(3, '0') + CodCliente;
		venta.CodigoBarrasCifrado = "";
		venta.ventasinstock = 0;
		return venta;
	}

	private void CreacionDespacho(List<clsFacturaVenta> lista_facturas)
	{
		try
		{
			int contador = 0;
			List<clsDespacho> lista_despacho = new List<clsDespacho>();
			foreach (clsFacturaVenta venta in lista_facturas)
			{
				if (venta.CodFacturaVenta == null)
				{
					continue;
				}
				int CodVentaFV = Convert.ToInt32(venta.CodFacturaVenta);
				int codAlmacenVenta = Convert.ToInt32(venta.CodAlmacen);
				int codPedidoVenta = Convert.ToInt32(pedido.CodPedido);
				int estadoReq = 13;
				clsRequerimientoAlmacen reqalm = admreqalm.CargaRequerimientosSegun(codPedidoVenta, codAlmacenVenta, estadoReq);
				if (reqalm == null)
				{
					continue;
				}
				clsDespacho despacho = new clsDespacho();
				try
				{
					clsTipoDocumento docDes = Admdoc.BuscaTipoDocumento("DESP");
					clsSerie serDes = AdmSerie.BuscaSeriexDocumento(docDes.CodTipoDocumento, codAlmacenVenta);
					despacho.CodSerie = serDes.CodSerie;
					despacho.Serie = serDes.Serie;
					despacho.Numeracion = serDes.Numeracion.ToString().PadLeft(8, '0');
				}
				catch (Exception)
				{
					despacho.CodSerie = 0;
					despacho.Serie = "000";
					despacho.Numeracion = "0000000";
				}
				despacho.CodAlmacenRegistro = codAlmacenVenta;
				despacho.CodCliente = venta.CodCliente;
				despacho.CodTablaDocRelacionada = 1;
				despacho.CodDocRelacionado = CodVentaFV;
				DateTime fechaDespacho = (despacho.FechaRegistro = DateTime.Now);
				despacho.FechaDespacho = fechaDespacho;
				despacho.codReqAlmRelacionado = reqalm.Codigo;
				despacho.CodUserRegistro = reqalm.CodUserRegistro;
				despacho.NombreContacto = reqalm.NombreContacto;
				despacho.TelefonoContacto = reqalm.TelefonoContacto;
				despacho.DireccionDelivery = reqalm.DireccionDelivery;
				despacho.Comentario = reqalm.ComentarioSolicitante;
				List<clsDetalleDespacho> detalleDespacho = obtenerDetalleDespacho(codAlmacenVenta, reqalm);
				using TransactionScope Scope = new TransactionScope();
				try
				{
					bool rpta = admdespacho.insert(despacho);
					if (rpta)
					{
						foreach (clsDetalleDespacho item in detalleDespacho)
						{
							item.CodDespacho = despacho.CodDespacho;
							rpta = admdespacho.insertDetalle(item);
							if (!rpta)
							{
								break;
							}
						}
					}
					if (!rpta)
					{
						throw new Exception("No se pudo guardar \nDespacho relacionado al Req: " + reqalm.Codigo + "\nCodAlmacen Solicitante y de Venta: " + codAlmacenVenta);
					}
					Scope.Complete();
					Scope.Dispose();
					admdespacho.cambioEstado(despacho.CodDespacho, 14);
					contador++;
					lista_despacho.Add(despacho);
					admreqalm.asignarFacturaVenta(reqalm.Codigo, CodVentaFV);
					admreqalm.actualizaEstadoReqAlmacen(reqalm.Codigo, 17);
				}
				catch (Exception ex2)
				{
					Transaction.Current.Rollback();
					Scope.Dispose();
					throw ex2;
				}
				btnreq.Visible = false;
			}
			if (contador == lista_facturas.Count)
			{
			}
			imprimirDespachos(lista_despacho);
		}
		catch (Exception ex3)
		{
			MessageBox.Show("Error: " + ex3.Message.ToString(), "Error Respecto a Despacho", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void imprimirDespachos(List<clsDespacho> lista_despacho)
	{
		try
		{
			foreach (clsDespacho item in lista_despacho)
			{
				try
				{
					clsTipoDocumento doc = Admdoc.BuscaTipoDocumento("DESP");
					clsSerie ser = AdmSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
					clsConsultasExternas ext = new clsConsultasExternas();
					CRDespachoFormatoContinuo rpt1 = new CRDespachoFormatoContinuo();
					clsAlmacen almac = admalma.CargaAlmacen(item.CodAlmacenRegistro);
					rpt1.SetDataSource(admdespacho.ReporteImprimirDespacho(Convert.ToInt32(item.CodDespacho), almac.CodEmpresa).Tables[0]);
					PrintOptions rptoption = rpt1.PrintOptions;
					rptoption.PrinterName = ser.NombreImpresora;
					rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
					rptoption.ApplyPageMargins(new PageMargins(40, 5, 0, 10));
					rpt1.PrintToPrinter(1, collated: false, 1, 1);
					rpt1.Close();
					rpt1.Dispose();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Despacho", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex2.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private List<clsDetalleDespacho> obtenerDetalleDespacho(int codAlmacenVenta, clsRequerimientoAlmacen reqalm)
	{
		List<clsDetalleRequerimientoAlmacen> detalleReqAlm = admreqalm.CargaDetalleRequerimientoAlmacen(reqalm.Codigo);
		List<clsDetalleDespacho> detalle = new List<clsDetalleDespacho>();
		if (dgvdetalle.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
			{
				if (Convert.ToInt32(row.Cells[codalmacen.Name].Value) != codAlmacenVenta)
				{
					continue;
				}
				int codProducto = Convert.ToInt32(row.Cells[codproducto.Name].Value);
				int codUnidad = Convert.ToInt32(row.Cells[codunidad.Name].Value);
				double cantidadVenta = Convert.ToDouble(row.Cells[cantidad.Name].Value);
				clsDetalleDespacho deta = new clsDetalleDespacho();
				deta.CodProducto = codProducto;
				deta.CodUnidad = codUnidad;
				deta.Estado = 1;
				List<clsDetalleRequerimientoAlmacen> item = Enumerable.Where<clsDetalleRequerimientoAlmacen>(detalleReqAlm.AsEnumerable(), (Func<clsDetalleRequerimientoAlmacen, bool>)((clsDetalleRequerimientoAlmacen x) => x.CodProducto == codProducto && x.CodUnidad == codUnidad)).ToList();
				if (item.Count > 0)
				{
					clsDetalleRequerimientoAlmacen detallereqalm = item[0];
					double resultado_resta_cantidad = cantidadVenta - Convert.ToDouble(detallereqalm.CantidadConfirmada);
					double num;
					if (resultado_resta_cantidad > 0.0)
					{
						clsDetalleDespacho aux = new clsDetalleDespacho();
						aux.CodProducto = codProducto;
						aux.CodUnidad = codUnidad;
						aux.Estado = 0;
						num = (aux.CantidadPendiente = resultado_resta_cantidad);
						aux.Cantidad = num;
						aux.CodAlmacenEntregar = codAlmacenVenta;
						cantidadVenta -= resultado_resta_cantidad;
						detalle.Add(aux);
					}
					num = (deta.CantidadPendiente = cantidadVenta);
					deta.Cantidad = num;
					deta.CodAlmacenEntregar = reqalm.CodAlmacenDespacho;
					admreqalm.ModificarCtdadPendienteAprobadaDeDetalleReqAlmacen(2, cantidadVenta, detallereqalm.Codigo);
				}
				else
				{
					deta.Estado = 0;
					double num = (deta.CantidadPendiente = cantidadVenta);
					deta.Cantidad = num;
					deta.CodAlmacenEntregar = codAlmacenVenta;
				}
				detalle.Add(deta);
			}
		}
		return detalle;
	}

	public void fnImprimir(clsFacturaVenta fv)
	{
		try
		{
			impresion = AdmVenta.chekeaImpresion(Convert.ToInt32(fv.CodFacturaVenta));
			empresa = admEmpresa.CargaEmpresa(fv.CodEmpresa);
			admTransa.TransFactura(venta.CodPedido);
			if (impresion == 0)
			{
				PrintaDocumento(fv);
				transfer = admTransa.CargaTransferenciaCodPedido(Convert.ToInt32(pedido.CodPedido));
				if (transfer != null)
				{
					transfer = null;
					PrintaDocumentoTrnas(fv);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void PrintaDocumentoTrnas(clsFacturaVenta fv)
	{
		try
		{
			if (frmLogin.iCodAlmacen == 0)
			{
				return;
			}
			DataSet jes = new DataSet();
			transfer = admTransa.CargaTransferenciaCodPedido(Convert.ToInt32(pedido.CodPedido));
			frmRptTransFactura frm = new frmRptTransFactura();
			CRTransferenciaFomatoContinuo rpt = new CRTransferenciaFomatoContinuo();
			jes = dst.RptTransferenciaFactura(Convert.ToInt32(fv.CodFacturaVenta), Convert.ToInt32(transfer.codtrans));
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = fv.Qr;
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
			rptoption.ApplyPageMargins(new PageMargins(40, 5, 0, 10));
			frm.crvfrmRptTransFactura.ReportSource = rpt;
			frm.ShowDialog();
			rpt.PrintToPrinter(1, collated: false, 0, 0);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void PrintaDocumento(clsFacturaVenta fv)
	{
		new EscribirLog("Imprimiento TICKET ", mostrarConsola: true);
		int VFormapago = venta.FormaPago;
		try
		{
			if (frmLogin.iCodAlmacen == 0)
			{
				return;
			}
			DataSet jes = new DataSet();
			frmRptFactura frm = new frmRptFactura();
			if (VFormapago == 5 || VFormapago == 6)
			{
				CRFacturaFomatoContinuocont rpt1 = new CRFacturaFomatoContinuocont();
				new EscribirLog("Cargando datos para el TICKET ", mostrarConsola: true);
				jes = ds1.ReporteFactura2(Convert.ToInt32(fv.CodFacturaVenta));
				foreach (DataTable mel in jes.Tables)
				{
					foreach (DataRow changesRow in mel.Rows)
					{
						changesRow["firma"] = fv.Qr;
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
				rpt1.PrintToPrinter(1, collated: false, 1, 1);
				rpt1.Close();
				rpt1.Dispose();
				new EscribirLog("TICKET Impreso ", mostrarConsola: true);
				return;
			}
			CRFacturaFomatoContinuo rpt2 = new CRFacturaFomatoContinuo();
			jes = ds1.ReporteFactura2(Convert.ToInt32(fv.CodFacturaVenta));
			foreach (DataTable mel2 in jes.Tables)
			{
				foreach (DataRow changesRow3 in mel2.Rows)
				{
					changesRow3["firma"] = fv.Qr;
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
			rpt2.PrintToPrinter(1, collated: false, 1, 1);
			rpt2.Close();
			rpt2.Dispose();
			new EscribirLog("TICKET Impreso ", mostrarConsola: true);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool VerificaSaldoCaja(int codsucu, int alma)
	{
		try
		{
			bool apert = false;
			Caja = AdmCaja.ValidarAperturaDia(codsucu, DateTime.Now.Date, 1, alma, frmLogin.iCodUser);
			if (Caja == null)
			{
				MessageBox.Show("Aperture caja, el pago no se a registrado", "SGE SYSTEM'S", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				CodigoCaja = Caja.Codcaja;
				apert = true;
			}
			return apert;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private bool ingresarpago(int codsucu, int alma)
	{
		if (!VerificaSaldoCaja(codsucu, alma))
		{
			return false;
		}
		if (AdmVenta.insertComprobante(venta))
		{
			venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
			CodVenta = venta.CodFacturaVenta;
			if (CodigoCaja != 0)
			{
				Pag.BanderaRetDet = "NAD";
				Pag.RetDet = 0m;
				Pag.MontoEnCuenta = 0m;
				Pag.OpcionSuma = 1;
				Pag.CodNota = venta.CodFacturaVenta.ToString();
				Pag.CodLetra = 0;
				Pag.CodTipoPago = 5;
				Pag.CodMoneda = venta.Moneda;
				Pag.CodCobrador = Convert.ToInt32(frmLogin.iCodUser);
				Pag.Tipo = true;
				Pag.IngresoEgreso = true;
				Pag.TipoCambio = Convert.ToDecimal(venta.TipoCambio);
				Pag.MontoPagado = Convert.ToDecimal(venta.Total);
				Pag.MontoCobrado = Convert.ToDecimal(venta.Total);
				Pag.Vuelto = 0m;
				Pag.codCtaCte = 0;
				Pag.CtaCte = "";
				Pag.NOperacion = "";
				Pag.NCheque = "";
				Pag.FechaPago = DateTime.Now;
				Pag.Observacion = "";
				Pag.CodUser = frmLogin.iCodUser;
				Pag.CodAlmacen = alma;
				Pag.CodSucursal = codsucu;
				Pag.CodDoc = venta.CodTipoDocumento;
				Pag.Serie = "";
				Pag.NumDoc = "";
				Pag.Referencia = "";
				Pag.Codcaja = CodigoCaja;
				Pag.CodBanco = 0;
				Pag.CodTarjeta = 0;
				Pag.Aprobado = 4;
				if (AdmPagos.insert(Pag))
				{
					return true;
				}
				MessageBox.Show("Pago en efectivo no registrado.");
				return false;
			}
			MessageBox.Show("Caja no aperturada.");
			return false;
		}
		MessageBox.Show("Hubo un error al insertar venta.", "Error en el Registro de Venta ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		return false;
	}

	private void VerificaDocumentosPorEmpresa(int codigoE)
	{
	}

	public void setPedido()
	{
		pedido.CodAlmacen = frmLogin.iCodAlmacen;
		if (CodCliente == 0)
		{
			ValidaCliente(txtCodCliente.Text);
		}
		else
		{
			cli = AdmCli.MuestraCliente(CodCliente);
			if (!cli.Habilitado)
			{
				string rucdni = txtCodCliente.Text;
				if (rucdni.Length == 8)
				{
					cli.Dni = rucdni;
					cli.DireccionEntrega = "-";
					cli.DireccionLegal = "-";
				}
				else if (rucdni.Length == 11)
				{
					cli.Ruc = rucdni;
					cli.DireccionEntrega = txtDireccion.Text;
					cli.DireccionLegal = txtDireccion.Text;
				}
				cli.Nombre = txtNombreCliente.Text;
				cli.RazonSocial = txtNombreCliente.Text;
				cli.Habilitado = true;
				if (!AdmCli.update(cli))
				{
					MessageBox.Show("No se actualizaron los datos de cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
		if (Convert.ToInt32(cmbTecnico.SelectedValue ?? ((object)(-1))) > -1)
		{
			pedido.idTecnico = Convert.ToInt32(cmbTecnico.SelectedValue);
		}
		else
		{
			pedido.idTecnico = -1;
		}
		if (Convert.ToInt32(cmbZona.SelectedValue) > 0)
		{
			pedido.idZona = Convert.ToInt32(cmbZona.SelectedValue);
		}
		if (Convert.ToInt32(cmbCanalVenta.SelectedValue) != 0)
		{
			pedido.CodCanalVenta = cmbCanalVenta.SelectedValue.ToString();
		}
		pedido.CodCliente = CodCliente;
		pedido.CodTipoDocumento = doc.CodTipoDocumento;
		pedido.Nombrecliente = txtNombreCliente.Text;
		pedido.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		if (mdi_Menu.tc_hoy > 0.0)
		{
			pedido.TipoCambio = mdi_Menu.tc_hoy;
		}
		pedido.FechaPedido = dtpFecha.Value.Date;
		pedido.FechaEntrega = dtpFecha.Value.Date;
		pedido.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
		pedido.FechaPago = dtpFecha.Value.AddDays(fpago.Dias);
		pedido.CodSerie = CodSerie;
		pedido.Comentario = "";
		if (editar)
		{
			pedido.SerieDoc = textBoxX2.Text;
			pedido.Numeracion = Convert.ToInt32(textBoxX1.Text);
		}
		else
		{
			pedido.SerieDoc = ser.Serie;
			pedido.Numeracion = ser.Numeracion;
			pedido.Numeracion = ser.Numeracion;
		}
		pedido.MontoBruto = Convert.ToDecimal(txtBruto.Text);
		pedido.MontoDscto = Convert.ToDecimal(txtDscto.Text);
		pedido.Igv = Convert.ToDecimal(txtIGV.Text);
		pedido.Total = Convert.ToDecimal(txtPrecioVenta.Text);
		pedido.Icbper = Convert.ToDecimal(txtIcbper.Text);
		pedido.CodVendedor = Convert.ToInt32(txtCodigoVendedor.Text);
		pedido.CodUser = Convert.ToInt32(txtCodigoVendedor.Text);
		pedido.Estado = 1;
		pedido.Gratuitas = montogratuitas;
		pedido.Exoneradas = montoexoneradas;
		pedido.Gravadas = montogravadas;
		pedido.Inafectas = montoinafectas;
		if (chkBoleta.Checked)
		{
			pedido.Boletafactura = 1;
		}
		else if (chkFactura.Checked)
		{
			pedido.Boletafactura = 2;
		}
		else if (chkTicket.Checked)
		{
			pedido.Boletafactura = 3;
		}
		else
		{
			pedido.Boletafactura = 1;
		}
		pedido.CodEmpresa = frmLogin.iCodEmpresa;
		pedido.CodigoBarras = "";
		pedido.CodigoBarrasCifrado = "";
		pedido.ventasinstock = Convert.ToInt32(chkVentaSinStock.Checked);
		banderagrabada = false;
		banderaexonerada = false;
		banderainafecta = false;
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			switch (Convert.ToInt32(row.Cells[Tipoimpuesto.Name].Value))
			{
			case 10:
				banderagrabada = true;
				break;
			case 11:
				banderagrabada = true;
				break;
			case 12:
				banderagrabada = true;
				break;
			case 13:
				banderagrabada = true;
				break;
			case 14:
				banderagrabada = true;
				break;
			case 15:
				banderagrabada = true;
				break;
			case 16:
				banderagrabada = true;
				break;
			case 17:
				banderagrabada = true;
				break;
			case 20:
				banderaexonerada = true;
				break;
			case 30:
				banderainafecta = true;
				break;
			case 31:
				banderainafecta = true;
				break;
			case 32:
				banderainafecta = true;
				break;
			case 33:
				banderainafecta = true;
				break;
			case 34:
				banderainafecta = true;
				break;
			case 35:
				banderainafecta = true;
				break;
			case 36:
				banderainafecta = true;
				break;
			}
		}
		if (chkVentaGratuita.Checked)
		{
			pedido.Tipoventa = 4;
		}
		else if (chkVentaDsctoGlobal.Checked)
		{
			pedido.Tipoventa = 5;
		}
		else if (banderagrabada && !banderaexonerada && !banderainafecta)
		{
			pedido.Tipoventa = 1;
		}
		else if (!banderagrabada && banderaexonerada && !banderainafecta)
		{
			pedido.Tipoventa = 2;
		}
		else if (!banderagrabada && !banderaexonerada && banderainafecta)
		{
			pedido.Tipoventa = 3;
		}
		else if (banderagrabada && banderaexonerada && !banderainafecta)
		{
			pedido.Tipoventa = 6;
		}
		else if (!banderagrabada && banderaexonerada && banderainafecta)
		{
			pedido.Tipoventa = 7;
		}
		pedido.ValorRetencion = (chkRetencion.Checked ? 1 : 0);
		RecorreDetalle();
		pedido.Detalle = detalle;
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
		cmbFormaPago_SelectionChangeCommitted(new object(), new EventArgs());
		cmbFormaPago.Visible = true;
		dtpFechaPago.Visible = true;
	}

	private void toolStripButtonPendiente_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPedidosPendientes"] != null)
		{
			Application.OpenForms["frmPedidosPendientes"].Activate();
			return;
		}
		cargaPedido = false;
		frmPedidosPendientes form = new frmPedidosPendientes();
		form.venta2019 = this;
		form.WindowState = FormWindowState.Normal;
		form.ShowDialog();
		venta = new clsFacturaVenta();
		if (cargaPedido)
		{
			bandEditar = false;
			bandNuevo = false;
			dgvproductos.DataSource = null;
			dgvproductos.Rows.Clear();
			dgvStockAlmacenes.DataSource = null;
			dgvStockAlmacenes.Rows.Clear();
			CargaPedidoVenta();
			if (frmLogin.iNivelUser == 3 || frmLogin.iNivelUser == 1 || frmLogin.iNivelUser == 2)
			{
				btnreq.Visible = true;
			}
			if (frmLogin.iNivelUser == 3 || frmLogin.iNivelUser == 1)
			{
				toolStripGuardar.Text = "Guardar Venta";
				toolStripGuardar.Enabled = true;
				Text = "VENTA";
				lbDocumento.Text = "VENTA";
			}
			else
			{
				toolStripGuardar.Text = "Guardar";
				toolStripGuardar.Enabled = false;
				Text = "ORDEN VENTA";
				lbDocumento.Text = "ORDEN VENTA";
			}
			toolStripEditaov.Enabled = true;
			toolstripEfectivo.Enabled = true;
		}
	}

	private void txtCodCliente_KeyUp(object sender, KeyEventArgs e)
	{
		try
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
				txtCodCliente.Text = "";
				txtDireccion.Text = "";
				txtNombreCliente.Text = "";
				NombreCliente = cli.Nombre;
				CargaCliente();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaPedido()
	{
		try
		{
			pedido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
			if (pedido != null)
			{
				doc.CodTipoDocumento = pedido.CodTipoDocumento;
				pedido.CodCotizacion = 0;
				txtSerie.Text = pedido.SerieDoc;
				CodSerie = pedido.CodSerie;
				txtPedido.Text = pedido.Numeracion.ToString().PadLeft(8, '0');
				txtCodigoBarras.Text = pedido.CodigoBarrasCifrado;
				cmbFormaPago.SelectedIndex = pedido.FormaPago;
				if (pedido.Boletafactura == 1)
				{
					CodCliente = pedido.CodCliente;
					cli.CodCliente = CodCliente;
					txtCodCliente.Text = pedido.DNI;
					if (pedido.Nombrecliente != "")
					{
						txtNombreCliente.Text = pedido.Nombrecliente;
					}
					else
					{
						txtNombreCliente.Text = pedido.Nombre;
					}
					txtDireccion.Text = pedido.Direccion;
				}
				else
				{
					CodCliente = pedido.CodCliente;
					cli.CodCliente = CodCliente;
					txtCodCliente.Text = pedido.RUCCliente;
					if (pedido.RazonSocialCliente != "")
					{
						txtNombreCliente.Text = pedido.RazonSocialCliente;
					}
					else
					{
						txtNombreCliente.Text = pedido.Nombre;
					}
					txtDireccion.Text = pedido.Direccion;
				}
				dtpFecha.Value = pedido.FechaPedido;
				if (txtDocRef.Enabled)
				{
					CodDocumento = pedido.CodTipoDocumento;
					txtDocRef.Text = pedido.SiglaDocumento;
					lbDocumento.Text = pedido.DescripcionDocumento;
				}
				txtBruto.Text = $"{pedido.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{pedido.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{pedido.Total - pedido.Igv:#,##0.00}";
				txtIGV.Text = $"{pedido.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{pedido.Total:#,##0.00}";
				montogravadas = pedido.Gravadas;
				montoexoneradas = pedido.Exoneradas;
				montoinafectas = pedido.Inafectas;
				montogratuitas = pedido.Gratuitas;
				txtPedido.Text = pedido.Numeracion.ToString().PadLeft(8, '0');
				montosventa();
				CargaDetalle();
				if (editar)
				{
					pedido.Detalle = new List<clsDetallePedido>();
					RecorreDetallePedido();
				}
				txtCodigoVendedor.Text = pedido.CodUser.ToString();
				txtCodigoVendedor.Focus();
				KeyEventArgs arg = new KeyEventArgs(Keys.Return);
				txtCodigoVendedor_KeyDown(new object(), arg);
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

	private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void toolStripImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			foreach (clsFacturaVenta fv in lista_facturas)
			{
				if (!cargaPedido || editar)
				{
					continue;
				}
				impresion = AdmVenta.chekeaImpresion(Convert.ToInt32(fv.CodFacturaVenta));
				empresa = admEmpresa.CargaEmpresa(fv.CodEmpresa);
				if (impresion == 0)
				{
					PrintaDocumento(fv);
					transfer = admTransa.CargaTransferenciaCodPedido(Convert.ToInt32(pedido.CodPedido));
					if (transfer != null)
					{
						transfer = null;
						PrintaDocumentoTrnas(fv);
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void labelX1_Click(object sender, EventArgs e)
	{
	}

	private void labelX5_Click(object sender, EventArgs e)
	{
	}

	private void toolStripAnulaov_Click(object sender, EventArgs e)
	{
	}

	private void dgvdetalle_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
	{
		if (e.KeyCode != Keys.Delete)
		{
			return;
		}
		if (dgvdetalle.Rows.Count > 0)
		{
			if (dgvdetalle.CurrentCell == null || dgvdetalle.CurrentCell.RowIndex == -1)
			{
				return;
			}
			if (editar && !nuevaOV)
			{
				int codProd = Convert.ToInt32(dgvdetalle.Rows[dgvdetalle.CurrentCell.RowIndex].Cells[codproducto.Name].Value);
				int codUnidad = Convert.ToInt32(dgvdetalle.Rows[dgvdetalle.CurrentCell.RowIndex].Cells[codunidad.Name].Value);
				int codAlmacen = Convert.ToInt32(dgvdetalle.Rows[dgvdetalle.CurrentCell.RowIndex].Cells[codalmacen.Name].Value);
				double cantidad = Convert.ToDouble(dgvdetalle.Rows[dgvdetalle.CurrentCell.RowIndex].Cells[this.cantidad.Name].Value);
				List<itemVerificarRA> itemBuscado = Enumerable.Where<itemVerificarRA>(listadoItemsVerificar.AsEnumerable(), (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codProducto == codProd && x.codUnidad == codUnidad && x.codAlmacen == codAlmacen)).ToList();
				if (itemBuscado.Count == 1)
				{
					itemVerificarRA aux = itemBuscado[0];
					int ind = listadoItemsVerificar.IndexOf(aux);
					aux.nuevaCantidad = cantidad;
					aux.proceso = 2;
					listadoItemsVerificar[ind] = aux;
				}
				else
				{
					if (itemBuscado.Count > 0)
					{
						MessageBox.Show("ocurrio un error inesperado se recomienda cerrar y volver abrir la ventana", "Error Cierre y Abra la Ventana", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
					listadoItemsVerificar.Add(new itemVerificarRA(codProd, codUnidad, codAlmacen, cantidad, cantidad, 2));
				}
				itemsEliminados.Add(dgvdetalle.Rows[dgvdetalle.CurrentCell.RowIndex]);
			}
			int codcombo = Convert.ToInt32(dgvdetalle.Rows[dgvdetalle.CurrentCell.RowIndex].Cells[combo.Name].Value);
			if (codcombo != 0)
			{
				lscombo.Clear();
				foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
				{
					if (Convert.ToInt32(row.Cells["combo"].Value) == codcombo)
					{
						lscombo.Add(row.Index);
					}
				}
				int contador = 1;
				foreach (int i in lscombo)
				{
					if (contador == 1)
					{
						dgvdetalle.Rows.RemoveAt(i);
					}
					else
					{
						int valor = i - 1;
						dgvdetalle.Rows.RemoveAt(valor);
					}
					contador++;
				}
			}
			else
			{
				dgvdetalle.Rows.RemoveAt(dgvdetalle.CurrentCell.RowIndex);
			}
			calculatotales();
			montosventa();
		}
		else
		{
			MessageBox.Show("No hay elementos para eliminar");
		}
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
	}

	private void cargaAlmacenes()
	{
		cmbAlmacenes.DataSource = admalma.ListaAlmacen2();
		cmbAlmacenes.ValueMember = "codAlmacen";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void cmbAlmacenes_SelectionChangeCommitted(object sender, EventArgs e)
	{
	}

	private void labelX7_Click(object sender, EventArgs e)
	{
	}

	private void cmbMoneda_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
	{
		if (cmbMoneda.SelectedValue != null && cmbMoneda.SelectedValue.ToString() != "System.Data.DataRowView" && Convert.ToInt32(cmbMoneda.SelectedIndex) != -1 && nuevaOV)
		{
			VentaEnMoneda();
		}
	}

	private void VentaEnMoneda()
	{
		if (mon == 1)
		{
			if (Convert.ToInt32(cmbMoneda.SelectedValue) != 1 && txtPrecioVenta.Text != "")
			{
				txtPrecioVenta.Text = $"{Convert.ToDecimal(txtPrecioVenta.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtValorVenta.Text = $"{Convert.ToDecimal(txtValorVenta.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtIGV.Text = $"{Convert.ToDecimal(txtIGV.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtinafectas.Text = $"{Convert.ToDecimal(txtinafectas.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtgratuitas.Text = $"{Convert.ToDecimal(txtgratuitas.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtexoneradas.Text = $"{Convert.ToDecimal(txtexoneradas.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtgravadas.Text = $"{Convert.ToDecimal(txtgravadas.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtBruto.Text = $"{Convert.ToDecimal(txtBruto.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				txtDscto.Text = $"{Convert.ToDecimal(txtDscto.Text) / Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
				mon = 2;
			}
		}
		else if (Convert.ToInt32(cmbMoneda.SelectedValue) != 2 && txtPrecioVenta.Text != "")
		{
			txtPrecioVenta.Text = $"{Convert.ToDecimal(txtPrecioVenta.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtValorVenta.Text = $"{Convert.ToDecimal(txtValorVenta.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtIGV.Text = $"{Convert.ToDecimal(txtIGV.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtinafectas.Text = $"{Convert.ToDecimal(txtinafectas.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtgratuitas.Text = $"{Convert.ToDecimal(txtgratuitas.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtexoneradas.Text = $"{Convert.ToDecimal(txtexoneradas.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtgravadas.Text = $"{Convert.ToDecimal(txtgravadas.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtBruto.Text = $"{Convert.ToDecimal(txtBruto.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			txtDscto.Text = $"{Convert.ToDecimal(txtDscto.Text) * Math.Round(Convert.ToDecimal(tc.Compra), 2):#,##0.00}";
			mon = 1;
		}
	}

	private void textBoxX1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (nuevaOV)
		{
			textBoxX1.ReadOnly = false;
		}
	}

	private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\r' || textBoxX1.Text.Equals(""))
		{
			return;
		}
		pedido = AdmPedido.CargaPedidoxAlmacen(Convert.ToInt32(textBoxX1.Text.Trim()), frmLogin.iCodAlmacen);
		clsCliente cli2 = null;
		if (pedido != null)
		{
			bandEditar = true;
			cmbMoneda.SelectedValue = pedido.Moneda;
			if (pedido.Boletafactura == 1)
			{
				cli2 = AdmCli.MuestraCliente(pedido.CodCliente);
				if (cli2.Dni != "")
				{
					txtCodCliente.Text = cli2.Dni;
				}
				else
				{
					txtCodCliente.Text = cli2.RucDni;
				}
				if (cli2.Nombre != "")
				{
					txtNombreCliente.Text = pedido.Nombre;
				}
				else
				{
					txtNombreCliente.Text = pedido.RazonSocialCliente;
				}
				txtDireccion.Text = pedido.Direccion;
				CargaCreditoCliente(cli2);
				chkBoleta.Checked = true;
				CodCliente = cli2.CodCliente;
			}
			else
			{
				cli2 = AdmCli.MuestraCliente(pedido.CodCliente);
				txtCodCliente.Text = cli2.RucDni;
				if (cli2.RazonSocial != "")
				{
					txtNombreCliente.Text = cli2.RazonSocial;
				}
				else
				{
					txtNombreCliente.Text = cli2.RucDni;
				}
				txtDireccion.Text = cli2.DireccionLegal;
				CargaCreditoCliente(cli2);
				chkFactura.Checked = true;
				CodCliente = cli2.CodCliente;
			}
			chkRetencion.Checked = pedido.ValorRetencion == 1;
			CargaDetalle();
			calculatotales();
			montosventa();
			toolStripGuardar.Enabled = true;
		}
		else
		{
			MessageBox.Show("El pedido no existe");
		}
	}

	private async void toolstripEfectivo_Click(object sender, EventArgs e)
	{
		MessageBox.Show("ESTA OPCION AH SIDO BLOQUEADA CONSULTAR CON SISTEMAS");
	}

	private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
	}

	private void txtCodCliente_TextChanged(object sender, EventArgs e)
	{
	}

	private void chbxstock_Click(object sender, EventArgs e)
	{
		if (chbxstock.Checked)
		{
			SinStock = 1;
			data.DataSource = AdmPro.RelacionSalidaTodoSinStock(1, frmLogin.iCodAlmacen, 1);
		}
		else
		{
			SinStock = 0;
			data.DataSource = AdmPro.RelacionSalidaTodo(1, frmLogin.iCodAlmacen, 1);
		}
	}

	private void chbxstock_CheckedChanged(object sender, EventArgs e)
	{
		if (chbxstock.Checked)
		{
			SinStock = 1;
		}
		else
		{
			SinStock = 0;
		}
	}

	private void chkRetencion_CheckedChanged(object sender, EventArgs e)
	{
		if (chkRetencion.Checked)
		{
			frmMostrarMensaje form1 = new frmMostrarMensaje();
			form1.Text = "Advertencia de Retencion";
			form1.colorTexto = Color.White;
			form1.textoColor = "La venta se hara con Retencion, es conforme?";
			form1.lblTextoColor.BackColor = Color.Yellow;
			form1.Height -= form1.lblTextoNegro.Height;
			form1.lblTextoNegro.Height = 0;
			form1.SiNo = true;
			form1.TextBtnSi = "Continuar";
			form1.TextBtnNo = "Desactivar";
			DialogResult rpta = form1.ShowDialog();
			if (rpta == DialogResult.No)
			{
				chkRetencion.Checked = false;
				return;
			}
		}
		if (!bandEditar && !bandNuevo && codPedidoVenta != 0 && !AdmPedido.cambiaMotivoRetencion(Convert.ToInt32(codPedidoVenta), chkRetencion.Checked))
		{
			MessageBox.Show("No se actualizo el tipo a retencion", "ERRORR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnreq_Click(object sender, EventArgs e)
	{
		if (pedido.CodPedido != "")
		{
			if (Application.OpenForms["FrmTPenPedido"] != null)
			{
				Application.OpenForms["FrmTPenPedido"].Close();
				return;
			}
			FrmTPenPedido form = new FrmTPenPedido();
			form.Proceso = 2;
			form.CodPedido = pedido.CodPedido;
			form.ShowDialog();
		}
	}

	private void dgvdetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			try
			{
				valorInicial = dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error al Editar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void dgvdetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		DataGridViewColumn column = dgvdetalle.Columns[e.ColumnIndex];
		if (e.RowIndex < 0 || !(column.Name == "cantidad"))
		{
			return;
		}
		try
		{
			if (pedido == null)
			{
				MessageBox.Show("PEDIDO NO ENCONTRADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
				valorInicial = null;
				return;
			}
			object valor = dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (double.TryParse(valor.ToString(), out var nuevaCantidad))
			{
				if (nuevaCantidad <= 0.0)
				{
					MessageBox.Show("La nueva cantidad debe ser mayor a cero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					if (valorInicial != null)
					{
						dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
						valorInicial = null;
					}
					return;
				}
				int codProd = Convert.ToInt32(dgvdetalle.Rows[e.RowIndex].Cells[codproducto.Name].Value);
				int codUnidad = Convert.ToInt32(dgvdetalle.Rows[e.RowIndex].Cells[codunidad.Name].Value);
				int codAlmacen = Convert.ToInt32(dgvdetalle.Rows[e.RowIndex].Cells[codalmacen.Name].Value);
				clsRequerimientoAlmacen reqAlm = admreqalm.CargaRequerimientosSegun(Convert.ToInt32(pedido.CodPedido), codAlmacen, -1);
				bool tieneRequerimientoAprobado = false;
				clsProducto product = AdmPro.ListaTotalprod2(codProd, codAlmacen, codUnidad);
				double totalprod = product.StockMaximo;
				if (nuevaCantidad == Convert.ToDouble((valorInicial == null) ? ((object)0) : valorInicial))
				{
					return;
				}
				if (bandEditar && !bandNuevo)
				{
					if (reqAlm != null)
					{
						List<clsDetalleRequerimientoAlmacen> detalle = admreqalm.CargaDetalleRequerimientoAlmacen(reqAlm.Codigo);
						List<clsDetalleRequerimientoAlmacen> detBuscado = Enumerable.Where<clsDetalleRequerimientoAlmacen>(detalle.AsEnumerable(), (Func<clsDetalleRequerimientoAlmacen, bool>)((clsDetalleRequerimientoAlmacen x) => x.CodProducto == codProd && x.CodUnidad == codUnidad)).ToList();
						if (detBuscado.Count > 0 && reqAlm.IEstado == 13)
						{
							if (!(nuevaCantidad <= totalprod + Convert.ToDouble(detBuscado[0].CantidadConfirmada)))
							{
								MessageBox.Show("Solo se permite la cantidad maxima de " + (totalprod + Convert.ToDouble(detBuscado[0].CantidadConfirmada)), "Sin Stock Suficiente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								if (valorInicial != null)
								{
									dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
									valorInicial = null;
								}
								return;
							}
							tieneRequerimientoAprobado = true;
						}
					}
					if (!tieneRequerimientoAprobado && !(nuevaCantidad <= totalprod))
					{
						MessageBox.Show("Solo se permite la cantidad maxima de " + totalprod, "Sin Stock Suficiente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						if (valorInicial != null)
						{
							dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
							valorInicial = null;
						}
						return;
					}
					List<itemVerificarRA> itemBuscado = Enumerable.Where<itemVerificarRA>(listadoItemsVerificar.AsEnumerable(), (Func<itemVerificarRA, bool>)((itemVerificarRA x) => x.codProducto == codProd && x.codUnidad == codUnidad && x.codAlmacen == codAlmacen)).ToList();
					if (itemBuscado.Count == 1)
					{
						itemVerificarRA aux = itemBuscado[0];
						int ind = listadoItemsVerificar.IndexOf(aux);
						aux.nuevaCantidad = nuevaCantidad;
						aux.proceso = 1;
						listadoItemsVerificar[ind] = aux;
					}
					else
					{
						if (itemBuscado.Count > 0)
						{
							MessageBox.Show("Error elementos iguales diferente tipo de edicion", "Error Cierre y Abra la Ventana", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							if (valorInicial != null)
							{
								dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
								valorInicial = null;
							}
							return;
						}
						listadoItemsVerificar.Add(new itemVerificarRA(codProd, codUnidad, codAlmacen, Convert.ToDouble(valorInicial), nuevaCantidad));
					}
				}
				else if (!bandEditar && bandNuevo && !(nuevaCantidad <= totalprod))
				{
					MessageBox.Show("Solo se permite la cantidad maxima de " + totalprod, "Sin Stock Suficiente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					if (valorInicial != null)
					{
						dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
						valorInicial = null;
					}
					return;
				}
				decimal cantidad = default(decimal);
				decimal total = default(decimal);
				decimal total_icbper = default(decimal);
				bool ibperband = false;
				int tipoImpuesto = 0;
				decimal precioUnitario = Convert.ToDecimal(dgvdetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value);
				total = Math.Round(Convert.ToDecimal(nuevaCantidad) * precioUnitario, 2);
				cantidad = Convert.ToDecimal(nuevaCantidad);
				tipoImpuesto = Convert.ToInt32(dgvdetalle.Rows[e.RowIndex].Cells[Tipoimpuesto.Name].Value);
				decimal bruto = total;
				decimal precioventa;
				decimal valorventa;
				if (tipoImpuesto.ToString().StartsWith("1"))
				{
					precioventa = total;
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa = precioventa / factorigv;
				}
				else
				{
					valorventa = total;
					precioventa = valorventa;
				}
				decimal precioreal = precioventa / cantidad;
				decimal valorreal = valorventa / cantidad;
				decimal igv = precioventa - valorventa;
				if (Convert.ToInt32(dgvdetalle.Rows[e.RowIndex].Cells[icbper_band.Name].Value) > 0)
				{
					total_icbper = frmLogin.Configuracion.Icbper * cantidad;
					ibperband = true;
				}
				else
				{
					total_icbper = default(decimal);
					ibperband = false;
				}
				DataGridViewRow fila = dgvdetalle.Rows[e.RowIndex];
				fila.Cells[importe.Name].Value = bruto;
				fila.Cells[this.valorventa.Name].Value = valorventa;
				fila.Cells[this.igv.Name].Value = igv;
				fila.Cells[this.precioventa.Name].Value = precioventa;
				fila.Cells[valoreal.Name].Value = valorreal;
				fila.Cells[this.precioreal.Name].Value = precioreal;
				fila.Cells[precioconigv.Name].Value = precioventa;
				fila.Cells[icbper1.Name].Value = total_icbper;
				fila.Cells[icbper_band.Name].Value = ibperband;
				calculatotales();
				montosventa();
			}
			else
			{
				MessageBox.Show("El valor tiene que ser un numero", "Error de edicion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (valorInicial != null)
				{
					dgvdetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
					valorInicial = null;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error al Editar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void chkFactura_CheckedChanged(object sender, EventArgs e)
	{
		if (chkFactura.Checked)
		{
			chkRetencion.Enabled = true;
			return;
		}
		chkRetencion.Checked = false;
		chkRetencion.Enabled = false;
	}

	private void CargaPedidoVenta()
	{
		try
		{
			clsCliente cli2 = null;
			pedido = AdmPedido.CargaPedido(Convert.ToInt32(codPedidoVenta));
			if (pedido != null)
			{
				int a = 1;
				DataTable dat_req = admreqalm.CargaRequerimientosSegunPedido(Convert.ToInt32(pedido.CodPedido));
				if (dat_req != null && verificarSiReqActivo(dat_req))
				{
					SinStock = 1;
					chbxstock.Checked = true;
				}
				cmbTecnico.SelectedValue = pedido.idTecnico;
				cmbZona.SelectedValue = pedido.idZona;
				cmbCanalVenta.SelectedValue = pedido.CodCanalVenta;
				textBoxX2.Text = pedido.SerieDoc;
				textBoxX1.Text = pedido.Numeracion.ToString();
				doc.CodTipoDocumento = pedido.CodTipoDocumento;
				pedido.CodCotizacion = 0;
				txtSerie.Text = pedido.SerieDoc;
				CodSerie = pedido.CodSerie;
				txtCodigoBarras.Text = pedido.CodigoBarrasCifrado;
				cmbMoneda.SelectedValue = pedido.Moneda;
				if (pedido.Boletafactura == 1 || pedido.Boletafactura == 3)
				{
					cli2 = AdmCli.MuestraCliente(pedido.CodCliente);
					if (cli2.Dni != "")
					{
						txtCodCliente.Text = cli2.Dni;
					}
					else
					{
						txtCodCliente.Text = cli2.RucDni;
					}
					if (cli2.Nombre != "")
					{
						txtNombreCliente.Text = pedido.Nombre;
					}
					else
					{
						txtNombreCliente.Text = pedido.RazonSocialCliente;
					}
					txtDireccion.Text = pedido.Direccion;
					cli2 = AdmCli.ConsultaCliente(pedido.DNI);
					CargaCreditoCliente(cli2);
					CodCliente = cli2.CodCliente;
					if (pedido.Boletafactura == 1)
					{
						chkBoleta.Checked = true;
					}
					else
					{
						chkTicket.Checked = true;
					}
				}
				else
				{
					cli2 = AdmCli.MuestraCliente(pedido.CodCliente);
					txtCodCliente.Text = cli2.RucDni;
					if (cli2.RazonSocial != "")
					{
						txtNombreCliente.Text = cli2.RazonSocial;
					}
					else
					{
						txtNombreCliente.Text = cli2.RucDni;
					}
					txtDireccion.Text = cli2.DireccionLegal;
					CargaCreditoCliente(cli2);
					chkFactura.Checked = true;
					CodCliente = cli2.CodCliente;
				}
				dtpFecha.Value = pedido.FechaPedido;
				if (txtDocRef.Enabled)
				{
					CodDocumento = pedido.CodTipoDocumento;
					txtDocRef.Text = pedido.SiglaDocumento;
					lbDocumento.Text = pedido.DescripcionDocumento;
				}
				txtBruto.Text = $"{pedido.MontoBruto:#,##0.00}";
				txtDscto.Text = $"{pedido.MontoDscto:#,##0.00}";
				txtValorVenta.Text = $"{pedido.Total - pedido.Igv:#,##0.00}";
				txtIGV.Text = $"{pedido.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{pedido.Total:#,##0.00}";
				txtIcbper.Text = $"{pedido.Icbper:#,##0.00}";
				montogravadas = pedido.Gravadas;
				montoexoneradas = pedido.Exoneradas;
				montoinafectas = pedido.Inafectas;
				montogratuitas = pedido.Gratuitas;
				txtPedido.Text = pedido.Numeracion.ToString().PadLeft(8, '0');
				chkRetencion.Checked = pedido.ValorRetencion == 1;
				CargaDetalle();
				calculatotales();
				montosventa();
				txtCodigoVendedor.Text = pedido.CodUser.ToString();
				txtCodigoVendedor.Focus();
				KeyEventArgs arg = new KeyEventArgs(Keys.Return);
				txtCodigoVendedor_KeyDown(new object(), arg);
				dtpFechaPago.Value = pedido.FechaPago;
				cmbFormaPago.SelectedValue = pedido.FormaPago;
				dtpFechaPago.Value = pedido.FechaPago;
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

	private void RecorreDetallePedido()
	{
		pedido.Detalle.Clear();
		if (dgvdetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			añadedetallePedido(row);
		}
	}

	private void añadedetallePedido(DataGridViewRow fila)
	{
		clsDetallePedido deta = new clsDetallePedido();
		deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodPedido = Convert.ToInt32(pedido.CodPedido);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDecimal(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDecimal(fila.Cells[dscto3.Name].Value);
		deta.Descuento3 = Convert.ToDecimal(fila.Cells[dscto3.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.Precioigv = Convert.ToBoolean(Convert.ToInt32(fila.Cells[precioconigv.Name].Value));
		deta.Valorpromedio = Convert.ToDecimal(fila.Cells[valorpromedio.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		pedido.Detalle.Add(deta);
	}

	private void CargaDetalle(DataTable generado = null)
	{
		DataTable newData = new DataTable();
		dgvdetalle.Rows.Clear();
		try
		{
			newData = ((generado != null) ? generado : AdmPedido.CargaDetalle(Convert.ToInt32(pedido.CodPedido)));
			foreach (DataRow row in newData.Rows)
			{
				dgvdetalle.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[35], row[36], row[37], row[38], row[39], row[40], row[41]);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void montosventa()
	{
		if (montogravadas > 0m)
		{
			txtgravadas.Clear();
			txtgravadas.Text = $"{montogravadas:#,##0.00}";
		}
		else
		{
			txtgravadas.Text = $"{0:#,##0.00}";
		}
		if (montogratuitas > 0m)
		{
			txtgratuitas.Clear();
			txtgratuitas.Text = $"{montogratuitas:#,##0.00}";
		}
		else
		{
			txtgratuitas.Text = $"{0:#,##0.00}";
		}
		if (montoexoneradas > 0m)
		{
			txtexoneradas.Clear();
			txtexoneradas.Text = $"{montoexoneradas:#,##0.00}";
		}
		else
		{
			txtexoneradas.Text = $"{0:#,##0.00}";
		}
		if (montoinafectas > 0m)
		{
			txtinafectas.Clear();
			txtinafectas.Text = $"{montoinafectas:#,##0.00}";
		}
		else
		{
			txtinafectas.Text = $"{0:#,##0.00}";
		}
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		if (dgvdetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetallePedido deta = new clsDetallePedido();
		deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodPedido = Convert.ToInt32(pedido.CodPedido);
		deta.CodAlmacen = Convert.ToInt32(fila.Cells[codalmacen.Name].Value);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDecimal(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = 0m;
		deta.Descuento3 = 0m;
		deta.MontoDescuento = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDecimal(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.Precioigv = Convert.ToBoolean(Convert.ToInt32(fila.Cells[precioconigv.Name].Value));
		deta.Valorpromedio = Convert.ToDecimal(fila.Cells[valorpromedio.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.Tipoimpuesto = fila.Cells[Tipoimpuesto.Name].Value.ToString();
		deta.icbper = Convert.ToDecimal(fila.Cells[icbper1.Name].Value.ToString());
		if (fila.Cells[icbper_band.Name].Value.GetType() == 1.GetType())
		{
			deta.icbper_band = ((Convert.ToInt32(fila.Cells[icbper_band.Name].Value) != 0) ? true : false);
		}
		else if (Convert.ToInt32(fila.Cells[icbper_band.Name].Value) == 0)
		{
			deta.icbper_band = false;
		}
		else
		{
			deta.icbper_band = Convert.ToBoolean(fila.Cells[icbper_band.Name].Value.ToString());
		}
		deta.codlinea = Convert.ToInt32(fila.Cells[codline.Name].Value);
		deta.codfamilia = Convert.ToInt32(fila.Cells[codfamili.Name].Value);
		deta.codcombo = ((fila.Cells[combo.Name].Value != DBNull.Value) ? Convert.ToInt32(fila.Cells[combo.Name].Value) : 0);
		detalle.Add(deta);
	}

	private void RecorreDetalleVenta(int codalma)
	{
		detalle.Clear();
		detalle1.Clear();
		if (dgvdetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
		{
			if (Convert.ToInt32(row.Cells[codalmacen.Name].Value) == codalma)
			{
				añadedetalleVenta(row);
			}
		}
	}

	private void añadedetalleVenta(DataGridViewRow fila)
	{
		clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
		deta.CodAlmacen = Convert.ToInt32(fila.Cells[codalmacen.Name].Value);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = "";
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
		deta.Moneda = 1;
		deta.Descripcion = fila.Cells[product.Name].Value.ToString();
		deta.Tipoimpuesto = fila.Cells[Tipoimpuesto.Name].Value.ToString();
		deta.Entregado = true;
		deta.TipoUnidad = Convert.ToInt32(fila.Cells[TipoUnidad.Name].Value);
		deta.CodDetalleCotizacion = 0;
		deta.ProductoVentaTicket = 0;
		deta.CodigoProductoSunat = "00000 ";
		deta.CodDetallePedido = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.icbper = Convert.ToDecimal(fila.Cells[icbper1.Name].Value);
		deta.icbper_band = Convert.ToBoolean(fila.Cells[icbper1.Name].Value);
		deta.codlinea = Convert.ToInt32(fila.Cells[codline.Name].Value);
		deta.codfamilia = Convert.ToInt32(fila.Cells[codfamili.Name].Value);
		detalle1.Add(deta);
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == (Keys.Q | Keys.Alt))
		{
			cancelaVenta();
		}
		if (keyData == (Keys.R | Keys.Alt))
		{
			txtCodCliente.Focus();
		}
		if (keyData == (Keys.C | Keys.Alt))
		{
			txtNombreCliente.Focus();
		}
		if (keyData == (Keys.D | Keys.Alt))
		{
			txtDireccion.Focus();
			base.ActiveControl = txtDireccion;
		}
		if (keyData == (Keys.G | Keys.Alt) && toolStripGuardar.Enabled)
		{
			toolStripGuardar_Click(null, null);
		}
		if (keyData == (Keys.E | Keys.Alt) && toolstripEfectivo.Enabled)
		{
			toolstripEfectivo_Click(null, null);
		}
		if (keyData == (Keys.B | Keys.Alt))
		{
			txtFiltro.Focus();
		}
		if (keyData == (Keys.P | Keys.Alt))
		{
			dgvproductos.Focus();
			if (dgvproductos.Rows.Count > 0)
			{
				dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
				dgvproductos.CurrentCell = dgvproductos.CurrentRow.Cells[cant.Name];
			}
			base.ActiveControl = dgvproductos;
		}
		if (keyData == (Keys.D | Keys.Alt))
		{
			dgvdetalle.Focus();
			if (dgvdetalle.Rows.Count > 0)
			{
				dgvdetalle.Rows[0].Cells[preciounit.Name].Selected = true;
				dgvdetalle.CurrentCell = dgvproductos.CurrentRow.Cells[preciounit.Name];
			}
			base.ActiveControl = dgvdetalle;
		}
		if (keyData == (Keys.V | Keys.Alt))
		{
			txtCodigoVendedor.Focus();
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void limpiarTotales()
	{
		foreach (Control c in groupBox5.Controls)
		{
			if (c is TextBox)
			{
				c.Text = "";
			}
		}
	}

	private void cancelaVenta()
	{
		sololectura(estado: true);
		activaPaneles(estado: false);
		dgvdetalle.DataSource = null;
		dgvdetalle.Rows.Clear();
		dgvStockAlmacenes.DataSource = null;
		dgvStockAlmacenes.Rows.Clear();
		limpiarTotales();
		toolStripAnulaov.Enabled = false;
		editar = false;
		cargaPedido = false;
		nuevaOV = false;
	}

	private void cmbTecnico_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.F1)
			{
				return;
			}
			if (Application.OpenForms["frmListadoTecnico"] != null)
			{
				Application.OpenForms["frmListadoTecnico"].Activate();
				return;
			}
			frmListadoTecnico form = new frmListadoTecnico();
			form.Proceso = 2;
			DialogResult rpta = form.ShowDialog();
			if (rpta == DialogResult.Yes)
			{
				int codTecnicoCreado = form.codTecnicoCreado;
				cargaComboTecnicos();
				cmbTecnico.SelectedValue = codTecnicoCreado;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cmbCategoriaCliente_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbCategoriaCliente.SelectedIndex) > 0 && Convert.ToInt32(cmbCategoriaCliente.SelectedValue) != 1)
		{
			AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbCategoriaCliente.SelectedValue));
		}
		else if (cmbCategoriaCliente.Enabled && Convert.ToInt32(cmbCategoriaCliente.SelectedValue) == 1 && cli.RucDni.Length == 11)
		{
			MessageBox.Show("Seleccionar una Categoria Nueva", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			cmbCategoriaCliente.SelectedValue = 0;
		}
	}

	private bool BuscaOrdenCompraCotizacion()
	{
		OrdenCompraCotizacion = AdmOrdenC.CargaOrdenCompraCotizacion(Convert.ToInt32(txtnumeroordencompra.Text), frmLogin.iCodAlmacen);
		if (coti != null)
		{
			CodOrdenCompra = Convert.ToInt32(OrdenCompraCotizacion.codOrdenCompra);
			return true;
		}
		CodOrdenCompra = 0;
		return false;
	}

	private void CargaCotizacion()
	{
		try
		{
			OrdenCompraCotizacion = AdmOrdenC.CargaOrdenCompraCotizacion(Convert.ToInt32(txtnumeroordencompra.Text), frmLogin.iCodAlmacen);
			if (OrdenCompraCotizacion != null)
			{
				txtcodCotizacion.Text = OrdenCompraCotizacion.codCotizacion.ToString();
				if (txtCodCliente.Enabled)
				{
					CodClienteGeneradoCotizacion = OrdenCompraCotizacion.codCliente;
					cmbMoneda.SelectedValue = OrdenCompraCotizacion.moneda;
					txtBruto.Text = $"{OrdenCompraCotizacion.total:#,##0.00}";
					txtDscto.Text = $"{OrdenCompraCotizacion.montodscto:#,##0.00}";
					txtgravadas.Text = $"{OrdenCompraCotizacion.subtotal:#,##0.00}";
					txtValorVenta.Text = $"{OrdenCompraCotizacion.subtotal:#,##0.00}";
					txtIGV.Text = $"{OrdenCompraCotizacion.igv:#,##0.00}";
					txtPrecioVenta.Text = $"{OrdenCompraCotizacion.total:#,##0.00}";
					CargaDetalleCotizacion();
					toolStripGuardar.Enabled = true;
					txtmontoordencompra.Enabled = false;
					txtnumeroordencompra.Enabled = false;
					chbordencompra.Enabled = false;
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

	private void CargaDetalleCotizacion()
	{
		DataTable cotizacion = new DataTable();
		dgvdetalle.Rows.Clear();
		try
		{
			cotizacion = AdmCoti.CargaDetalleVenta(CodOrdenCompra, frmLogin.iCodAlmacen);
			foreach (DataRow row in cotizacion.Rows)
			{
				dgvdetalle.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[15].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[26], row[27], row[28], row[29]);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvdetalle_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
	}

	private void chbordencompra_CheckedChanged(object sender, EventArgs e)
	{
		if (chbordencompra.Checked)
		{
			txtnumeroordencompra.ReadOnly = false;
			txtmontoordencompra.ReadOnly = false;
			txtmontoordencompra.Enabled = true;
			txtnumeroordencompra.Enabled = true;
		}
		else
		{
			txtnumeroordencompra.ReadOnly = true;
			txtmontoordencompra.ReadOnly = true;
			txtnumeroordencompra.Text = "";
			txtmontoordencompra.Text = "";
			txtmontoordencompra.Enabled = false;
			txtnumeroordencompra.Enabled = false;
		}
	}

	private void txtmontoordencompra_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsControl(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsSeparator(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsPunctuation(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	public void txtnumeroordencompra_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtnumeroordencompra.Text != "")
		{
			if (BuscaOrdenCompraCotizacion())
			{
				CargaCotizacion();
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("Comuniquese con el Area de Sistemas", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				ext.limpiar(base.Controls);
				cargaAlmacenes();
			}
		}
	}

	private void chkTodos_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataGridViewRow dr in (IEnumerable)dgvdetalle.Rows)
		{
			dr.Cells[30].Value = chkTodos.Checked;
		}
	}

	private void btndescuento_Click(object sender, EventArgs e)
	{
	}

	private void chkTicket_Click(object sender, EventArgs e)
	{
		if (chkTicket.Checked)
		{
			chkBoleta.Checked = false;
			chkBoleta.Enabled = true;
			chkFactura.Checked = false;
			chkFactura.Enabled = true;
		}
	}

	private void btnvercombos_Click(object sender, EventArgs e)
	{
		frmCombosVenta frm = new frmCombosVenta();
		DialogResult dr = frm.ShowDialog();
		codcombo = frm.codcombo;
		if (dr == DialogResult.OK)
		{
			cargaproductoscombo();
		}
	}

	public void cargaproductoscombo()
	{
		if (codcombo == 0)
		{
			return;
		}
		DataTable detallecombo = new DataTable();
		try
		{
			detallecombo = AdmPro.CargaDetalleComboVenta(codcombo, frmLogin.iCodAlmacen);
			foreach (DataRow row in detallecombo.Rows)
			{
				dgvdetalle.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[15].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString(), row[26], row[27], row[28], row[29], row[30], row[31], row[32]);
			}
			calculatotales();
			montosventa();
			toolStripGuardar.Enabled = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtdescuento_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void btndescuento_Click_1(object sender, EventArgs e)
	{
		try
		{
			int cantidaseleccionada = 0;
			decimal montoadescontar = default(decimal);
			decimal descuentopermitido = default(decimal);
			DataTable porcentajedescuento = new DataTable();
			ls.Clear();
			foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
			{
				if (Convert.ToInt32(row.Cells["descuento"].Value) == 1)
				{
					ls.Add(row.Index);
				}
			}
			cantidaseleccionada = ls.Count;
			if (cantidaseleccionada > 0)
			{
				foreach (int i in ls)
				{
					int codproducto = Convert.ToInt32(dgvdetalle.Rows[i].Cells[referenci.Name].Value);
					int codunidadMedida = Convert.ToInt32(dgvdetalle.Rows[i].Cells[codunidad.Name].Value);
					decimal preciocompra = AdmPro.UltimoPrecioCompraProductoVenta(codproducto, 0, codunidadMedida);
					decimal flete = default(decimal);
					decimal desestiva = default(decimal);
					DataTable costos = AdmPro.CostoTotalProductoVenta(codproducto, codunidadMedida);
					flete = Convert.ToDecimal(costos.Rows[0]["flete"]);
					desestiva = Convert.ToDecimal(costos.Rows[0]["desestiva"]);
					decimal totalcosto = Math.Round(flete + desestiva + preciocompra, 2);
					decimal valor = Convert.ToDecimal(1.18);
					decimal precioUnitario = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value);
					decimal Ganancia = (precioUnitario - totalcosto) / valor;
					decimal GananciaMonto = Math.Round(Convert.ToDecimal(dgvdetalle.Rows[i].Cells[cantidad.Name].Value) * Ganancia, 2);
					decimal GananciaPorcentaje = default(decimal);
					GananciaPorcentaje = ((!(preciocompra == 0m) || !(totalcosto == 0m)) ? Math.Round(Ganancia / (preciocompra / valor) * 100m, 2) : 100m);
					montoadescontar = default(decimal);
					int codemp = admEmpresa.empresaxalmacen(Convert.ToInt32(dgvdetalle.Rows[i].Cells[codalmacen.Name].Value));
					porcentajedescuento = AdmDes.ListadoParametroDescuento(codemp, GananciaPorcentaje);
					montoadescontar = Math.Round(Convert.ToDecimal(porcentajedescuento.Rows[0]["Valor"]) * Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value) / 100m, 2);
					param = admParam.ObtenerParametro(5);
					descuentopermitido = Convert.ToDecimal(param.valor);
					if (descuentopermitido <= Convert.ToDecimal(txtPrecioVenta.Text))
					{
						if (GananciaMonto > montoadescontar)
						{
							dgvdetalle.Rows[i].Cells[preciounit.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value) - montoadescontar;
							dgvdetalle.Rows[i].Cells[importe.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value) * Convert.ToDecimal(dgvdetalle.Rows[i].Cells[cantidad.Name].Value);
							dgvdetalle.Rows[i].Cells[valorventa.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[importe.Name].Value) / Convert.ToDecimal(1.18);
							dgvdetalle.Rows[i].Cells[precioventa.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[importe.Name].Value);
							dgvdetalle.Rows[i].Cells[montodscto.Name].Value = montoadescontar * Convert.ToDecimal(dgvdetalle.Rows[i].Cells[cantidad.Name].Value);
						}
						else
						{
							MessageBox.Show("Ganancia es Menor al monto a descontar  del producto : " + Convert.ToString(dgvdetalle.Rows[i].Cells[descripcion.Name].Value), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						MessageBox.Show("Monto Total de Venta es Menor a Monto configurado , Monto Total de venta Minimo para descuento es : " + param.valor.ToString(), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					calculatotales();
				}
				return;
			}
			MessageBox.Show("Seleccionar Productos Para Descuento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void chkTodos_CheckedChanged_1(object sender, EventArgs e)
	{
		foreach (DataGridViewRow dr in (IEnumerable)dgvdetalle.Rows)
		{
			dr.Cells[30].Value = chkTodos.Checked;
		}
	}

	private void txtdescuento_KeyDown_1(object sender, KeyEventArgs e)
	{
		try
		{
			int cantidaseleccionada = 0;
			decimal montoadescontar = default(decimal);
			decimal descuentopermitido = default(decimal);
			DataTable porcentajedescuento = new DataTable();
			if (e.KeyCode != Keys.Return || !(txtDscto.Text.Trim() != ""))
			{
				return;
			}
			ls.Clear();
			foreach (DataGridViewRow row in (IEnumerable)dgvdetalle.Rows)
			{
				if (Convert.ToInt32(row.Cells["descuento"].Value) == 1)
				{
					ls.Add(row.Index);
				}
			}
			cantidaseleccionada = ls.Count;
			if (cantidaseleccionada > 0)
			{
				foreach (int i in ls)
				{
					int codproducto = Convert.ToInt32(dgvdetalle.Rows[i].Cells[referenci.Name].Value);
					int codunidadMedida = Convert.ToInt32(dgvdetalle.Rows[i].Cells[codunidad.Name].Value);
					decimal preciocompra = AdmPro.UltimoPrecioCompraProductoVenta(codproducto, 0, codunidadMedida);
					decimal flete = default(decimal);
					decimal desestiva = default(decimal);
					DataTable costos = AdmPro.CostoTotalProductoVenta(codproducto, codunidadMedida);
					flete = Convert.ToDecimal(costos.Rows[0]["flete"]);
					desestiva = Convert.ToDecimal(costos.Rows[0]["desestiva"]);
					decimal totalcosto = Math.Round(flete + desestiva + preciocompra, 2);
					decimal valor = Convert.ToDecimal(1.18);
					decimal precioUnitario = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value);
					decimal Ganancia = (precioUnitario - totalcosto) / valor;
					decimal GananciaMonto = Math.Round(Convert.ToDecimal(dgvdetalle.Rows[i].Cells[cantidad.Name].Value) * Ganancia, 2);
					decimal GananciaPorcentaje = default(decimal);
					GananciaPorcentaje = ((!(preciocompra == 0m) || !(totalcosto == 0m)) ? Math.Round(Ganancia / (preciocompra / valor) * 100m, 2) : 100m);
					montoadescontar = default(decimal);
					int codemp = admEmpresa.empresaxalmacen(Convert.ToInt32(dgvdetalle.Rows[i].Cells[codalmacen.Name].Value));
					porcentajedescuento = AdmDes.ListadoParametroDescuento(codemp, GananciaPorcentaje);
					montoadescontar = default(decimal);
					montoadescontar = ((!chkdescuento.Checked) ? Convert.ToDecimal(txtdescuento.Text) : Math.Round(Convert.ToDecimal(txtdescuento.Text) * Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value) / 100m, 2));
					montoadescontar = Math.Round(montoadescontar / Convert.ToDecimal(1.18), 2);
					param = admParam.ObtenerParametro(5);
					descuentopermitido = Convert.ToDecimal(param.valor);
					if (descuentopermitido <= Convert.ToDecimal(txtPrecioVenta.Text))
					{
						if (GananciaMonto > montoadescontar)
						{
							dgvdetalle.Rows[i].Cells[preciounit.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value) - montoadescontar;
							dgvdetalle.Rows[i].Cells[importe.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[preciounit.Name].Value) * Convert.ToDecimal(dgvdetalle.Rows[i].Cells[cantidad.Name].Value);
							dgvdetalle.Rows[i].Cells[valorventa.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[importe.Name].Value) / Convert.ToDecimal(1.18);
							dgvdetalle.Rows[i].Cells[precioventa.Name].Value = Convert.ToDecimal(dgvdetalle.Rows[i].Cells[importe.Name].Value);
							dgvdetalle.Rows[i].Cells[montodscto.Name].Value = montoadescontar * Convert.ToDecimal(dgvdetalle.Rows[i].Cells[cantidad.Name].Value);
						}
						else
						{
							MessageBox.Show("Ganancia es Menor al monto a descontar  del producto : " + Convert.ToString(dgvdetalle.Rows[i].Cells[product.Name].Value), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						MessageBox.Show("Monto Total de Venta es Menor a Monto configurado , Monto Total de venta Minimo para descuento es : " + param.valor.ToString(), "Descuentos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					calculatotales();
				}
				return;
			}
			MessageBox.Show("Seleccionar Productos Para Descuento", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrió un error: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void chkTicket_CheckedChanged_1(object sender, EventArgs e)
	{
		if (chkTicket.Checked)
		{
			chkBoleta.Checked = false;
			chkBoleta.Enabled = true;
			chkFactura.Checked = false;
			chkFactura.Enabled = true;
		}
	}

	private void chkBoleta_CheckedChanged(object sender, EventArgs e)
	{
		if (chkBoleta.Checked)
		{
			chkFactura.Checked = false;
			chkTicket.Checked = false;
		}
	}

	private void apruebaTransferencia(clsTransferencia transfer)
	{
		try
		{
			clsTipoDocumento doc = new clsTipoDocumento();
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			tran = AdmTran.MuestraTransaccion(15);
			doc = admtd.BuscaTipoDocumento("DET");
			NS.NumDoc = transfer.Numerodocumento;
			NS.CodAlmacen = transfer.CodAlmacenOrigen;
			NS.CodCliente = 0;
			NS.CodNotaCredito = 0;
			NS.CodSucursal = transfer.CodAlmacenOrigen;
			NS.RazonSocialCliente = "";
			NS.CodAutorizado = 0;
			NS.FechaSalida = DateTime.Now.Date;
			NS.DocumentoReferencia = 0;
			NS.CodTipoTransaccion = tran.CodTransaccion;
			NS.CodTipoDocumento = doc.CodTipoDocumento;
			NS.CodSerie = transfer.Codserie;
			NS.Serie = transfer.Serie;
			NS.Moneda = 1;
			NS.FechaSalida = DateTime.Now.Date;
			NS.FormaPago = 0;
			NS.FechaPago = DateTime.Now.Date;
			NS.Comentario = "";
			NS.MontoBruto = Convert.ToDouble(transfer.MontoBruto);
			NS.MontoDscto = 0.0;
			NS.Igv = 0.0;
			NS.Total = Convert.ToDouble(transfer.Total);
			NS.CodUser = transfer.CodUser;
			NS.Estado = 1;
			NS.Codtransferencia = Convert.ToInt32(transfer.CodTransDir);
			using (TransactionScope Scope = new TransactionScope())
			{
				if (admNS.insert(NS))
				{
					RecorreDetalleNS();
					if (detalleNS.Count > 0)
					{
						foreach (clsDetalleNotaSalida det in detalleNS)
						{
							if (!admNS.insertdetalle(det))
							{
								bandera = false;
								codproducto_error = det.CodProducto;
								Transaction.Current.Rollback();
								Scope.Dispose();
								break;
							}
						}
						if (bandera)
						{
							Scope.Complete();
							Scope.Dispose();
						}
					}
				}
				else
				{
					Transaction.Current.Rollback();
					Scope.Dispose();
					MessageBox.Show("Hubo un error al registrar la salida de producto ", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (bandera)
			{
				NI.NumDoc = NS.NumDoc;
				NI.CodAlmacen = transfer.CodAlmacenDestino;
				NI.CodAutorizado = 0;
				NI.CodReferencia = 0;
				NI.CodTipoTransaccion = tran.CodTransaccion;
				NI.CodTipoDocumento = doc.CodTipoDocumento;
				NI.CodSerie = NS.CodSerie;
				NI.Serie = NS.Serie;
				NI.Moneda = 1;
				NI.FechaIngreso = DateTime.Now.Date;
				NI.FormaPago = 0;
				NI.FechaPago = DateTime.Now.Date;
				NI.Comentario = "";
				NS.MontoBruto = Convert.ToDouble(transfer.MontoBruto);
				NI.MontoDscto = 0.0;
				NI.Igv = 0.0;
				NI.Total = Convert.ToDouble(transfer.Total);
				NI.CodUser = transfer.CodUser;
				NI.Estado = 1;
				NI.Codtransferencia = Convert.ToInt32(transfer.CodTransDir);
				using (TransactionScope Scope2 = new TransactionScope())
				{
					if (admNI.insert(NI))
					{
						RecorreDetalleNI();
						if (detalleNI.Count > 0)
						{
							foreach (clsDetalleNotaIngreso det2 in detalleNI)
							{
								if (!admNI.insertdetalle(det2))
								{
									bandera = false;
									codproducto_error = det2.CodProducto;
									Transaction.Current.Rollback();
									Scope2.Dispose();
									break;
								}
							}
						}
						if (bandera)
						{
							Scope2.Complete();
							Scope2.Dispose();
						}
					}
					else
					{
						Transaction.Current.Rollback();
						Scope2.Dispose();
						MessageBox.Show("Hubo un error al registrar el ingreso de productos", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				if (bandera)
				{
					admTransa.Aprobar(Convert.ToInt32(transfer.CodTransDir));
				}
				else
				{
					MessageBox.Show("Hubo un error al guardar el Documento de Extornacion", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void RecorreDetalleNS()
	{
		detalleNS.Clear();
		if (detalleExtor.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalleExtor)
		{
			añadedetalleNS(row);
		}
	}

	private void añadedetalleNS(clsDetalleTransferencia fila)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = fila.CodProducto;
		deta.CodNotaSalida = Convert.ToInt32(NS.CodNotaSalida);
		deta.CodAlmacen = extornacion.CodAlmacenOrigen;
		deta.UnidadIngresada = fila.UnidadIngresada;
		deta.SerieLote = "0";
		deta.Cantidad = fila.Cantidad;
		deta.PrecioUnitario = fila.PrecioUnitario;
		deta.Subtotal = fila.Subtotal;
		deta.Descuento1 = fila.Descuento1;
		deta.Descuento2 = fila.Descuento2;
		deta.Descuento3 = fila.Descuento3;
		deta.Igv = fila.Igv;
		deta.Importe = fila.PrecioVenta;
		deta.PrecioReal = fila.PrecioReal;
		deta.ValoReal = fila.ValoReal;
		deta.ValorPromedio = Convert.ToDouble(fila.Valorpromedio);
		deta.CodUser = frmLogin.iCodUser;
		detalleNS.Add(deta);
	}

	private void RecorreDetalleNI()
	{
		detalleNI.Clear();
		if (detalleExtor.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalleExtor)
		{
			añadedetalleNI(row);
		}
	}

	private void añadedetalleNI(clsDetalleTransferencia fila)
	{
		clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
		deta1.CodProducto = fila.CodProducto;
		deta1.CodNotaIngreso = Convert.ToInt32(NI.CodNotaIngreso);
		deta1.CodAlmacen = extornacion.CodAlmacenDestino;
		deta1.UnidadIngresada = fila.UnidadIngresada;
		deta1.SerieLote = "0";
		deta1.Cantidad = fila.Cantidad;
		deta1.PrecioUnitario = fila.PrecioUnitario;
		deta1.Subtotal = fila.Subtotal;
		deta1.Descuento1 = fila.Descuento1;
		deta1.Descuento2 = fila.Descuento2;
		deta1.Descuento3 = fila.Descuento3;
		deta1.MontoDescuento = 0.0;
		deta1.ValoReal = deta1.PrecioUnitario / 1.18;
		deta1.Igv = deta1.ValoReal * 0.18;
		deta1.PrecioReal = deta1.ValoReal * 1.18;
		deta1.CodUser = frmLogin.iCodUser;
		deta1.Importe = deta1.PrecioUnitario * deta1.Cantidad;
		deta1.Subtotal = deta1.Importe;
		deta1.PrecioReal = fila.PrecioUnitario;
		deta1.ValoReal = fila.ValoReal;
		deta1.CodProveedor = 0;
		deta1.FechaIngreso = DateTime.Now;
		detalleNI.Add(deta1);
	}

	private void obtenerDetalleParaTransferencia(clsRequerimientoAlmacen req_alm)
	{
		DataTable detalleTransf = admTransa.CargaDetalle(extornacion.codTransAExtornar);
		detalleExtor.Clear();
		foreach (DataRow fila in detalleTransf.Rows)
		{
			clsDetalleTransferencia deta = new clsDetalleTransferencia();
			double _cantidad = Convert.ToDouble(fila.Field<object>("cantidad"));
			deta.CodProducto = Convert.ToInt32(fila.Field<object>("codProducto"));
			deta.CodAlmacenOrigen = req_alm.CodAlmacenSolicitante;
			deta.CodAlmacenDestino = req_alm.CodAlmacenDespacho;
			deta.UnidadIngresada = Convert.ToInt32(fila.Field<object>("codUnidadMedida"));
			deta.SerieLote = "";
			deta.Cantidad = _cantidad;
			deta.CantidadPendiente = _cantidad;
			double ult_pre = (deta.PrecioUnitario = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(deta.CodProducto, deta.UnidadIngresada, 0)));
			deta.Subtotal = ult_pre * deta.Cantidad;
			deta.Descuento1 = 0.0;
			deta.Descuento2 = 0.0;
			deta.Descuento3 = 0.0;
			deta.MontoDescuento = 0.0;
			bool flag = true;
			deta.PrecioVenta = deta.Subtotal;
			double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			deta.ValorVenta = deta.PrecioVenta / factorigv;
			deta.PrecioReal = deta.PrecioVenta / deta.Cantidad;
			deta.ValoReal = deta.ValorVenta / deta.Cantidad;
			deta.Igv = deta.PrecioVenta - deta.ValorVenta;
			deta.Importe = deta.Subtotal;
			deta.Valorpromedio = Convert.ToDecimal(deta.PrecioUnitario);
			deta.CodUser = frmLogin.iCodUser;
			detalleExtor.Add(deta);
			extornacion.MontoBruto += Convert.ToDecimal(deta.Importe);
			extornacion.MontoDscto += Convert.ToDecimal(deta.MontoDescuento);
			extornacion.Igv += Convert.ToDecimal(deta.Igv);
			extornacion.Total += Convert.ToDecimal(deta.Subtotal);
		}
	}

	public void cargaComboTecnicos()
	{
		DataTable aux = admtec.listaTecnicos();
		cmbTecnico.DataSource = aux;
		if (aux != null)
		{
			DataRow nueva = aux.NewRow();
			nueva.SetField("nombreCompleto", "Sin Tecnico");
			nueva.SetField("idTecnico", "0");
			aux.Rows.InsertAt(nueva, 0);
		}
		cmbTecnico.DisplayMember = "nombreCompleto";
		cmbTecnico.ValueMember = "idTecnico";
		cmbTecnico.SelectedValue = -1;
		cmbTecnico.AutoCompleteCustomSource = CargaAutoComplete(aux, "nombreCompleto");
		cmbTecnico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbTecnico.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	public void cargaComboZona()
	{
		DataTable aux = admzona.MuestraZonas();
		cmbZona.DataSource = aux;
		cmbZona.DisplayMember = "descripcion";
		cmbZona.ValueMember = "codZona";
		cmbZona.SelectedIndex = -1;
		cmbZona.AutoCompleteCustomSource = CargaAutoComplete(aux, "descripcion");
		cmbZona.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbZona.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	public void cargaCombocategoriaclientes()
	{
		DataTable aux = admctgcliente.MuestraCategoriasCliente();
		cmbCategoriaCliente.DataSource = aux;
		cmbCategoriaCliente.DisplayMember = "descripcion";
		cmbCategoriaCliente.ValueMember = "codCategoriaCliente";
		cmbCategoriaCliente.SelectedIndex = -1;
	}

	public static AutoCompleteStringCollection CargaAutoComplete(DataTable dt, string nameFila = "nombre")
	{
		AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
		foreach (DataRow row in dt.Rows)
		{
			stringCol.Add(Convert.ToString(row[nameFila]));
		}
		return stringCol;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVenta2019));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panel = new DevComponents.DotNetBar.PanelEx();
		this.line2 = new DevComponents.DotNetBar.Controls.Line();
		this.toolStrip2 = new System.Windows.Forms.ToolStrip();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripGuardar = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.toolstripEfectivo = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripIniciaov = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripEditaov = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButtonPendiente = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripAnulaov = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripButtonSalir = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripImprimir = new System.Windows.Forms.ToolStripButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.labelx = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.btnRefrescar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvproductos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUniversal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nomAlma = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.nmarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockdisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidadmedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codsunatimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codtimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codalma = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidadnombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codlinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgvdetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referenci = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.product = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unida = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioconigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tipoarticulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tipoimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.TipoUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.empres = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper_band = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codline = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfamili = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Descuento = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.combo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockdisponibleventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.chkRetencion = new System.Windows.Forms.CheckBox();
		this.labelX7 = new DevComponents.DotNetBar.LabelX();
		this.cmbMoneda = new Telerik.WinControls.UI.RadDropDownList();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.labelX6 = new DevComponents.DotNetBar.LabelX();
		this.labelX5 = new DevComponents.DotNetBar.LabelX();
		this.labelX4 = new DevComponents.DotNetBar.LabelX();
		this.labelX3 = new DevComponents.DotNetBar.LabelX();
		this.labelX2 = new DevComponents.DotNetBar.LabelX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.txtNombreVendedor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtCodigoVendedor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cmbFormaPago = new DevComponents.DotNetBar.Controls.ComboBoxEx();
		this.chkFactura = new System.Windows.Forms.RadioButton();
		this.chkBoleta = new System.Windows.Forms.RadioButton();
		this.dtpFecha1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
		this.txtDireccion = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtNombreCliente = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtCodCliente = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtIcbper = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtexoneradas = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtgravadas = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label16 = new System.Windows.Forms.Label();
		this.txtinafectas = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label15 = new System.Windows.Forms.Label();
		this.txtgratuitas = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label14 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtPrecioVenta = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label9 = new System.Windows.Forms.Label();
		this.txtIGV = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label8 = new System.Windows.Forms.Label();
		this.txtValorVenta = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.txtmontoordencompra = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtnumeroordencompra = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.btnAnulaOV = new DevComponents.DotNetBar.ButtonX();
		this.btnEditaOV = new DevComponents.DotNetBar.ButtonX();
		this.btnInicioOV = new DevComponents.DotNetBar.ButtonX();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.txtLineaCredito = new System.Windows.Forms.TextBox();
		this.label25 = new System.Windows.Forms.Label();
		this.txttasa = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.txtLineaCreditoUso = new System.Windows.Forms.TextBox();
		this.chkVentaDsctoGlobal = new DevComponents.DotNetBar.Controls.CheckBoxX();
		this.chkVentaGratuita = new DevComponents.DotNetBar.Controls.CheckBoxX();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.pbCapchatS = new System.Windows.Forms.PictureBox();
		this.txtSunat_Capchat = new System.Windows.Forms.TextBox();
		this.lbLineaCredito = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.txtLineaCreditoDisponible = new System.Windows.Forms.TextBox();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.txtCodigoBarras = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.chkVentaSinStock = new DevComponents.DotNetBar.Controls.CheckBoxX();
		this.label21 = new System.Windows.Forms.Label();
		this.lbDocumento = new System.Windows.Forms.Label();
		this.txtDocRef = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label12 = new System.Windows.Forms.Label();
		this.txtPedido = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtSerie = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label22 = new System.Windows.Forms.Label();
		this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.label24 = new System.Windows.Forms.Label();
		this.textBoxX5 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.textBoxX6 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator("Seleccionar Vendedor");
		this.dgvStockAlmacenes = new System.Windows.Forms.DataGridView();
		this.idalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.idproductoalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nomempresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nomalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockalmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox10 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.chbxstock = new System.Windows.Forms.CheckBox();
		this.btnreq = new System.Windows.Forms.Button();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.gbTecnico = new System.Windows.Forms.GroupBox();
		this.cmbCanalVenta = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.cmbZona = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbTecnico = new System.Windows.Forms.ComboBox();
		this.lblusuariodesp = new System.Windows.Forms.Label();
		this.cmbCategoriaCliente = new System.Windows.Forms.ComboBox();
		this.lblcategoriacliente = new System.Windows.Forms.Label();
		this.groupBox12 = new System.Windows.Forms.GroupBox();
		this.txtcodCotizacion = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lblcotizacion = new System.Windows.Forms.Label();
		this.gbordencompra = new System.Windows.Forms.GroupBox();
		this.labelX9 = new DevComponents.DotNetBar.LabelX();
		this.lblnumero = new DevComponents.DotNetBar.LabelX();
		this.chbordencompra = new System.Windows.Forms.CheckBox();
		this.groupBox13 = new System.Windows.Forms.GroupBox();
		this.btndescuento = new System.Windows.Forms.Button();
		this.chkTodos = new System.Windows.Forms.CheckBox();
		this.txtdescuento = new System.Windows.Forms.TextBox();
		this.chkdescuento = new System.Windows.Forms.CheckBox();
		this.chkTicket = new System.Windows.Forms.RadioButton();
		this.groupBox14 = new System.Windows.Forms.GroupBox();
		this.btnvercombos = new System.Windows.Forms.Button();
		this.panel.SuspendLayout();
		this.toolStrip2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvproductos).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvdetalle).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFecha1).BeginInit();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).BeginInit();
		this.groupBox8.SuspendLayout();
		this.groupBox9.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvStockAlmacenes).BeginInit();
		this.groupBox10.SuspendLayout();
		this.groupBox11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.gbTecnico.SuspendLayout();
		this.groupBox12.SuspendLayout();
		this.gbordencompra.SuspendLayout();
		this.groupBox13.SuspendLayout();
		this.groupBox14.SuspendLayout();
		base.SuspendLayout();
		this.panel.CanvasColor = System.Drawing.SystemColors.Control;
		this.panel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.panel.Controls.Add(this.line2);
		this.panel.Controls.Add(this.toolStrip2);
		this.panel.DisabledBackColor = System.Drawing.Color.Empty;
		this.panel.Location = new System.Drawing.Point(0, 0);
		this.panel.Name = "panel";
		this.panel.Size = new System.Drawing.Size(1170, 34);
		this.panel.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.panel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.panel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.panel.Style.BorderColor.Color = System.Drawing.Color.DarkCyan;
		this.panel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.panel.Style.GradientAngle = 90;
		this.panel.TabIndex = 12;
		this.line2.AutoSize = true;
		this.line2.BackColor = System.Drawing.SystemColors.Control;
		this.line2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.line2.ForeColor = System.Drawing.Color.DarkCyan;
		this.line2.Location = new System.Drawing.Point(0, 26);
		this.line2.Name = "line2";
		this.line2.Size = new System.Drawing.Size(1170, 8);
		this.line2.TabIndex = 8;
		this.line2.Text = "line2";
		this.toolStrip2.AllowMerge = false;
		this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
		this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
		{
			this.toolStripSeparator1, this.toolStripGuardar, this.toolStripSeparator7, this.toolstripEfectivo, this.toolStripSeparator3, this.toolStripIniciaov, this.toolStripSeparator4, this.toolStripEditaov, this.toolStripSeparator5, this.toolStripButtonPendiente,
			this.toolStripSeparator2, this.toolStripAnulaov, this.toolStripSeparator6, this.toolStripButtonSalir, this.toolStripSeparator8, this.toolStripImprimir
		});
		this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.toolStrip2.Location = new System.Drawing.Point(0, 0);
		this.toolStrip2.Name = "toolStrip2";
		this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 2);
		this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.toolStrip2.Size = new System.Drawing.Size(1170, 26);
		this.toolStrip2.Stretch = true;
		this.toolStrip2.TabIndex = 7;
		this.toolStrip2.Text = "toolStrip2";
		this.toolStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip2_ItemClicked);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(6, 24);
		this.toolStripGuardar.Enabled = false;
		this.toolStripGuardar.Image = (System.Drawing.Image)resources.GetObject("toolStripGuardar.Image");
		this.toolStripGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripGuardar.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripGuardar.Name = "toolStripGuardar";
		this.toolStripGuardar.Size = new System.Drawing.Size(120, 20);
		this.toolStripGuardar.Text = "Guardar  (Alt + G)";
		this.toolStripGuardar.ToolTipText = "Alt + G";
		this.toolStripGuardar.Click += new System.EventHandler(toolStripGuardar_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(6, 24);
		this.toolstripEfectivo.Enabled = false;
		this.toolstripEfectivo.Image = SIGEFA.Properties.Resources.imprimir2_24;
		this.toolstripEfectivo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolstripEfectivo.Name = "toolstripEfectivo";
		this.toolstripEfectivo.Size = new System.Drawing.Size(150, 21);
		this.toolstripEfectivo.Text = "Efectivo Directo (Alt+E)";
		this.toolstripEfectivo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.toolstripEfectivo.ToolTipText = "Efectivo Directo";
		this.toolstripEfectivo.Visible = false;
		this.toolstripEfectivo.Click += new System.EventHandler(toolstripEfectivo_Click);
		this.toolStripSeparator3.Name = "toolStripSeparator3";
		this.toolStripSeparator3.Size = new System.Drawing.Size(6, 24);
		this.toolStripIniciaov.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripIniciaov.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripIniciaov.Name = "toolStripIniciaov";
		this.toolStripIniciaov.Size = new System.Drawing.Size(84, 20);
		this.toolStripIniciaov.Text = "Inicia OV  (F6)";
		this.toolStripIniciaov.ToolTipText = "Inicia OV";
		this.toolStripIniciaov.Click += new System.EventHandler(btnInicioOV_Click);
		this.toolStripSeparator4.Name = "toolStripSeparator4";
		this.toolStripSeparator4.Size = new System.Drawing.Size(6, 24);
		this.toolStripEditaov.Enabled = false;
		this.toolStripEditaov.Image = SIGEFA.Properties.Resources.edit;
		this.toolStripEditaov.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripEditaov.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripEditaov.Name = "toolStripEditaov";
		this.toolStripEditaov.Size = new System.Drawing.Size(95, 20);
		this.toolStripEditaov.Text = "Edita OV (F4)";
		this.toolStripEditaov.ToolTipText = "F4";
		this.toolStripEditaov.Click += new System.EventHandler(btnEditaOV_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(6, 24);
		this.toolStripButtonPendiente.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButtonPendiente.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripButtonPendiente.Name = "toolStripButtonPendiente";
		this.toolStripButtonPendiente.Size = new System.Drawing.Size(69, 20);
		this.toolStripButtonPendiente.Text = "Pendientes";
		this.toolStripButtonPendiente.ToolTipText = "Anula OV";
		this.toolStripButtonPendiente.Click += new System.EventHandler(toolStripButtonPendiente_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(6, 24);
		this.toolStripAnulaov.Enabled = false;
		this.toolStripAnulaov.Image = SIGEFA.Properties.Resources.x_button;
		this.toolStripAnulaov.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripAnulaov.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripAnulaov.Name = "toolStripAnulaov";
		this.toolStripAnulaov.Size = new System.Drawing.Size(77, 20);
		this.toolStripAnulaov.Text = "Anula OV";
		this.toolStripAnulaov.ToolTipText = "Anula OV";
		this.toolStripAnulaov.Visible = false;
		this.toolStripAnulaov.Click += new System.EventHandler(toolStripAnulaov_Click);
		this.toolStripSeparator6.Name = "toolStripSeparator6";
		this.toolStripSeparator6.Size = new System.Drawing.Size(6, 24);
		this.toolStripSeparator6.Visible = false;
		this.toolStripButtonSalir.Image = SIGEFA.Properties.Resources.exit;
		this.toolStripButtonSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripButtonSalir.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripButtonSalir.Name = "toolStripButtonSalir";
		this.toolStripButtonSalir.Size = new System.Drawing.Size(49, 20);
		this.toolStripButtonSalir.Text = "Salir";
		this.toolStripButtonSalir.ToolTipText = "Esc";
		this.toolStripButtonSalir.Click += new System.EventHandler(toolStripButtonSalir_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(6, 24);
		this.toolStripImprimir.Enabled = false;
		this.toolStripImprimir.Image = (System.Drawing.Image)resources.GetObject("toolStripImprimir.Image");
		this.toolStripImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
		this.toolStripImprimir.Margin = new System.Windows.Forms.Padding(2, 4, 2, 0);
		this.toolStripImprimir.Name = "toolStripImprimir";
		this.toolStripImprimir.Size = new System.Drawing.Size(73, 20);
		this.toolStripImprimir.Text = "Imprimir";
		this.toolStripImprimir.Click += new System.EventHandler(toolStripImprimir_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.labelx);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.btnRefrescar);
		this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox1.Location = new System.Drawing.Point(13, 42);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(446, 51);
		this.groupBox1.TabIndex = 16;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "BUSCAR";
		this.labelx.AutoSize = true;
		this.labelx.Location = new System.Drawing.Point(396, 23);
		this.labelx.Name = "labelx";
		this.labelx.Size = new System.Drawing.Size(13, 13);
		this.labelx.TabIndex = 309;
		this.labelx.Text = "x";
		this.labelx.Visible = false;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(175, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(218, 22);
		this.txtFiltro.TabIndex = 306;
		this.toolTip1.SetToolTip(this.txtFiltro, "Alt + B");
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.txtFiltro.Leave += new System.EventHandler(txtFiltro_Leave);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(2, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(106, 13);
		this.label10.TabIndex = 307;
		this.label10.Text = "Buscar Por(Alt+B) :";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(107, 22);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(17, 16);
		this.label11.TabIndex = 308;
		this.label11.Text = "X";
		this.btnRefrescar.BackColor = System.Drawing.Color.Transparent;
		this.btnRefrescar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnRefrescar.FlatAppearance.BorderSize = 0;
		this.btnRefrescar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Khaki;
		this.btnRefrescar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(192, 192, 255);
		this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRefrescar.Image = (System.Drawing.Image)resources.GetObject("btnRefrescar.Image");
		this.btnRefrescar.Location = new System.Drawing.Point(469, 10);
		this.btnRefrescar.Margin = new System.Windows.Forms.Padding(0);
		this.btnRefrescar.Name = "btnRefrescar";
		this.btnRefrescar.Size = new System.Drawing.Size(48, 33);
		this.btnRefrescar.TabIndex = 305;
		this.toolTip1.SetToolTip(this.btnRefrescar, "Ctrl + R");
		this.btnRefrescar.UseVisualStyleBackColor = false;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvproductos);
		this.groupBox2.Enabled = false;
		this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox2.Location = new System.Drawing.Point(12, 94);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(878, 188);
		this.groupBox2.TabIndex = 17;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "PRODUCTOS (Alt+P)";
		this.dgvproductos.AllowUserToAddRows = false;
		this.dgvproductos.AllowUserToDeleteRows = false;
		this.dgvproductos.AllowUserToResizeRows = false;
		this.dgvproductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvproductos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(81, 124, 210);
		dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvproductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
		this.dgvproductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvproductos.Columns.AddRange(this.codigo, this.referencia, this.codUniversal, this.nomAlma, this.descripcion, this.unidad, this.nmarca, this.Modelo, this.stockdisponible, this.cant, this.precio, this.total, this.codunidadmedida, this.codsunatimpuesto, this.codtimpuesto, this.codalma, this.unidadnombre, this.icbper, this.codlinea, this.codfamilia);
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle19.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
		dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvproductos.DefaultCellStyle = dataGridViewCellStyle19;
		this.dgvproductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvproductos.EnableHeadersVisualStyles = false;
		this.dgvproductos.Location = new System.Drawing.Point(3, 18);
		this.dgvproductos.MultiSelect = false;
		this.dgvproductos.Name = "dgvproductos";
		this.dgvproductos.RowHeadersVisible = false;
		this.dgvproductos.RowTemplate.Height = 28;
		this.dgvproductos.Size = new System.Drawing.Size(872, 167);
		this.dgvproductos.TabIndex = 1;
		this.dgvproductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvproductos_CellClick);
		this.dgvproductos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvproductos_CellEndEdit);
		this.dgvproductos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvproductos_ColumnHeaderMouseClick);
		this.dgvproductos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvproductos_EditingControlShowing);
		this.codigo.DataPropertyName = "codProducto";
		this.codigo.HeaderText = "Codigo ";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.referencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 86;
		this.codUniversal.DataPropertyName = "codUniversal";
		this.codUniversal.HeaderText = "Cod Universal";
		this.codUniversal.Name = "codUniversal";
		this.codUniversal.ReadOnly = true;
		this.codUniversal.Visible = false;
		this.nomAlma.DataPropertyName = "nomAlma";
		this.nomAlma.HeaderText = "Almacen";
		this.nomAlma.Name = "nomAlma";
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.unidad.DisplayStyleForCurrentCellOnly = true;
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.nmarca.DataPropertyName = "nmarca";
		this.nmarca.HeaderText = "Marca";
		this.nmarca.Name = "nmarca";
		this.nmarca.ReadOnly = true;
		this.nmarca.Visible = false;
		this.Modelo.DataPropertyName = "modelo";
		this.Modelo.HeaderText = "Modelo";
		this.Modelo.Name = "Modelo";
		this.Modelo.ReadOnly = true;
		this.Modelo.Visible = false;
		this.stockdisponible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.stockdisponible.DataPropertyName = "stockdisponible";
		this.stockdisponible.HeaderText = "Stock";
		this.stockdisponible.Name = "stockdisponible";
		this.stockdisponible.ReadOnly = true;
		this.stockdisponible.Width = 60;
		this.cant.HeaderText = "Cantidad";
		this.cant.Name = "cant";
		this.precio.HeaderText = "Precio";
		this.precio.Name = "precio";
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.codunidadmedida.DataPropertyName = "codunidadmedida";
		this.codunidadmedida.HeaderText = "CodUnidad";
		this.codunidadmedida.Name = "codunidadmedida";
		this.codunidadmedida.ReadOnly = true;
		this.codunidadmedida.Visible = false;
		this.codsunatimpuesto.DataPropertyName = "codsunatimpuesto";
		this.codsunatimpuesto.HeaderText = "Codigo Sunat";
		this.codsunatimpuesto.Name = "codsunatimpuesto";
		this.codsunatimpuesto.ReadOnly = true;
		this.codsunatimpuesto.Visible = false;
		this.codtimpuesto.DataPropertyName = "codtimpuesto";
		this.codtimpuesto.HeaderText = "CodTipoImpuesto";
		this.codtimpuesto.Name = "codtimpuesto";
		this.codtimpuesto.ReadOnly = true;
		this.codtimpuesto.Visible = false;
		this.codalma.DataPropertyName = "codalma";
		this.codalma.HeaderText = "codalma";
		this.codalma.Name = "codalma";
		this.codalma.Visible = false;
		this.unidadnombre.DataPropertyName = "unidadnombre";
		this.unidadnombre.HeaderText = "UnidadNombre";
		this.unidadnombre.Name = "unidadnombre";
		this.unidadnombre.Visible = false;
		this.icbper.DataPropertyName = "icbper";
		this.icbper.HeaderText = "icbper";
		this.icbper.Name = "icbper";
		this.icbper.Visible = false;
		this.codlinea.DataPropertyName = "codlinea";
		this.codlinea.HeaderText = "codlinea";
		this.codlinea.Name = "codlinea";
		this.codlinea.Visible = false;
		this.codfamilia.DataPropertyName = "codfamilia";
		this.codfamilia.HeaderText = "codfamilia";
		this.codfamilia.Name = "codfamilia";
		this.codfamilia.Visible = false;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.dgvdetalle);
		this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox3.Location = new System.Drawing.Point(11, 459);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(878, 153);
		this.groupBox3.TabIndex = 18;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "DETALLE ORDEN";
		this.dgvdetalle.AllowUserToAddRows = false;
		this.dgvdetalle.AllowUserToDeleteRows = false;
		this.dgvdetalle.AllowUserToResizeRows = false;
		this.dgvdetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvdetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(81, 124, 210);
		dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvdetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
		this.dgvdetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvdetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referenci, this.product, this.codunidad, this.unida, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.valoreal, this.precioreal, this.precioconigv, this.valorpromedio, this.Tipoarticulo, this.Tipoimpuesto, this.codalmacen, this.almacen, this.TipoUnidad, this.empres, this.icbper1, this.icbper_band, this.codline, this.codfamili, this.Descuento, this.combo, this.stockdisponibleventa);
		dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle33.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvdetalle.DefaultCellStyle = dataGridViewCellStyle33;
		this.dgvdetalle.Dock = System.Windows.Forms.DockStyle.Top;
		this.dgvdetalle.EnableHeadersVisualStyles = false;
		this.dgvdetalle.Location = new System.Drawing.Point(3, 18);
		this.dgvdetalle.Name = "dgvdetalle";
		this.dgvdetalle.RowHeadersVisible = false;
		this.dgvdetalle.RowTemplate.Height = 28;
		this.dgvdetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvdetalle.Size = new System.Drawing.Size(872, 131);
		this.dgvdetalle.TabIndex = 3;
		this.dgvdetalle.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvdetalle_CellBeginEdit);
		this.dgvdetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalle_CellEndEdit);
		this.dgvdetalle.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(dgvdetalle_PreviewKeyDown);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "Código de Producto_";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referenci.DataPropertyName = "referencia";
		this.referenci.HeaderText = "Código de Producto";
		this.referenci.Name = "referenci";
		this.referenci.ReadOnly = true;
		this.referenci.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.product.DataPropertyName = "producto";
		this.product.HeaderText = "Descripción";
		this.product.Name = "product";
		this.product.ReadOnly = true;
		this.product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unida.DataPropertyName = "unidad";
		this.unida.HeaderText = "Unidad";
		this.unida.Name = "unida";
		this.unida.ReadOnly = true;
		this.unida.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle34.Format = "N2";
		dataGridViewCellStyle34.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle34;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle35.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle35;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle36.Format = "N2";
		dataGridViewCellStyle36.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle36;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle37.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle37;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle38.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle38;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle39.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle39;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle40.Format = "N2";
		dataGridViewCellStyle40.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle40;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle41.Format = "N2";
		dataGridViewCellStyle41.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle41;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle42.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle42;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle43.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle43;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.DataPropertyName = "valoreal";
		dataGridViewCellStyle44.Format = "N2";
		dataGridViewCellStyle44.NullValue = null;
		this.valoreal.DefaultCellStyle = dataGridViewCellStyle44;
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.precioreal.DataPropertyName = "precioreal";
		dataGridViewCellStyle45.Format = "N2";
		dataGridViewCellStyle45.NullValue = null;
		this.precioreal.DefaultCellStyle = dataGridViewCellStyle45;
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.ReadOnly = true;
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.precioconigv.DataPropertyName = "precioigv";
		this.precioconigv.HeaderText = "precioconigv";
		this.precioconigv.Name = "precioconigv";
		this.precioconigv.ReadOnly = true;
		this.precioconigv.Visible = false;
		this.valorpromedio.DataPropertyName = "valorpromedio";
		this.valorpromedio.HeaderText = "valorpromedio";
		this.valorpromedio.Name = "valorpromedio";
		this.valorpromedio.ReadOnly = true;
		this.valorpromedio.Visible = false;
		this.Tipoarticulo.DataPropertyName = "Tipoarticulo";
		this.Tipoarticulo.HeaderText = "Tipoarticulo";
		this.Tipoarticulo.Name = "Tipoarticulo";
		this.Tipoarticulo.ReadOnly = true;
		this.Tipoarticulo.Visible = false;
		this.Tipoimpuesto.DataPropertyName = "Tipoimpuesto";
		this.Tipoimpuesto.HeaderText = "Tipoimpuesto";
		this.Tipoimpuesto.Name = "Tipoimpuesto";
		this.Tipoimpuesto.ReadOnly = true;
		this.Tipoimpuesto.Visible = false;
		this.codalmacen.DataPropertyName = "codalmacen";
		this.codalmacen.HeaderText = "codalmacen";
		this.codalmacen.Name = "codalmacen";
		this.codalmacen.ReadOnly = true;
		this.codalmacen.Visible = false;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.TipoUnidad.HeaderText = "TipoUnidad";
		this.TipoUnidad.Name = "TipoUnidad";
		this.TipoUnidad.ReadOnly = true;
		this.TipoUnidad.Visible = false;
		this.empres.DataPropertyName = "empres";
		this.empres.HeaderText = "Empresa";
		this.empres.Name = "empres";
		this.empres.ReadOnly = true;
		this.empres.Visible = false;
		this.icbper1.DataPropertyName = "icbper";
		this.icbper1.HeaderText = "ICBPER";
		this.icbper1.Name = "icbper1";
		this.icbper1.Visible = false;
		this.icbper_band.DataPropertyName = "icbper_band";
		this.icbper_band.HeaderText = "ICBPER_BAND";
		this.icbper_band.Name = "icbper_band";
		this.icbper_band.Visible = false;
		this.codline.DataPropertyName = "codline";
		this.codline.HeaderText = "codlinea";
		this.codline.Name = "codline";
		this.codline.Visible = false;
		this.codfamili.DataPropertyName = "codfamili";
		this.codfamili.HeaderText = "codfamilia";
		this.codfamili.Name = "codfamili";
		this.codfamili.Visible = false;
		this.Descuento.HeaderText = "Descuento";
		this.Descuento.Name = "Descuento";
		this.combo.DataPropertyName = "combo";
		this.combo.HeaderText = "codcombo";
		this.combo.Name = "combo";
		this.combo.Visible = false;
		this.stockdisponibleventa.HeaderText = "stockdisponibleventa";
		this.stockdisponibleventa.Name = "stockdisponibleventa";
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.Controls.Add(this.chkRetencion);
		this.groupBox4.Controls.Add(this.labelX7);
		this.groupBox4.Controls.Add(this.cmbMoneda);
		this.groupBox4.Controls.Add(this.dtpFecha);
		this.groupBox4.Controls.Add(this.labelX6);
		this.groupBox4.Controls.Add(this.labelX5);
		this.groupBox4.Controls.Add(this.labelX4);
		this.groupBox4.Controls.Add(this.labelX3);
		this.groupBox4.Controls.Add(this.labelX2);
		this.groupBox4.Controls.Add(this.labelX1);
		this.groupBox4.Controls.Add(this.txtNombreVendedor);
		this.groupBox4.Controls.Add(this.txtCodigoVendedor);
		this.groupBox4.Controls.Add(this.dtpFechaPago);
		this.groupBox4.Controls.Add(this.cmbFormaPago);
		this.groupBox4.Controls.Add(this.chkFactura);
		this.groupBox4.Controls.Add(this.chkBoleta);
		this.groupBox4.Controls.Add(this.dtpFecha1);
		this.groupBox4.Controls.Add(this.txtDireccion);
		this.groupBox4.Controls.Add(this.txtNombreCliente);
		this.groupBox4.Controls.Add(this.txtCodCliente);
		this.groupBox4.Enabled = false;
		this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox4.Location = new System.Drawing.Point(905, 99);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(371, 218);
		this.groupBox4.TabIndex = 23;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "DATOS DEL CLIENTE";
		this.groupBox4.Enter += new System.EventHandler(groupBox4_Enter);
		this.chkRetencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkRetencion.Location = new System.Drawing.Point(26, 143);
		this.chkRetencion.Name = "chkRetencion";
		this.chkRetencion.Size = new System.Drawing.Size(152, 22);
		this.chkRetencion.TabIndex = 152;
		this.chkRetencion.Text = "Venta Con Retencion";
		this.chkRetencion.UseVisualStyleBackColor = true;
		this.chkRetencion.CheckedChanged += new System.EventHandler(chkRetencion_CheckedChanged);
		this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX7.Location = new System.Drawing.Point(200, 13);
		this.labelX7.Name = "labelX7";
		this.labelX7.Size = new System.Drawing.Size(47, 28);
		this.labelX7.TabIndex = 133;
		this.labelX7.Text = "Moneda:";
		this.labelX7.WordWrap = true;
		this.labelX7.Click += new System.EventHandler(labelX7_Click);
		this.cmbMoneda.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbMoneda.Location = new System.Drawing.Point(255, 16);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.RootElement.ControlBounds = new System.Drawing.Rectangle(255, 16, 125, 20);
		this.cmbMoneda.RootElement.StretchVertically = true;
		this.cmbMoneda.Size = new System.Drawing.Size(87, 24);
		this.cmbMoneda.TabIndex = 132;
		this.cmbMoneda.ThemeName = "Fluent";
		this.cmbMoneda.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cmbMoneda_SelectedIndexChanged);
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(228, 143);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(97, 22);
		this.dtpFecha.TabIndex = 131;
		this.dtpFecha.Tag = "16";
		this.dtpFecha.Value = new System.DateTime(2019, 5, 23, 0, 0, 0, 0);
		this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX6.Location = new System.Drawing.Point(21, 176);
		this.labelX6.Name = "labelX6";
		this.labelX6.Size = new System.Drawing.Size(49, 13);
		this.labelX6.TabIndex = 130;
		this.labelX6.Text = "F. Pago:";
		this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX5.Location = new System.Drawing.Point(184, 149);
		this.labelX5.Name = "labelX5";
		this.labelX5.Size = new System.Drawing.Size(38, 10);
		this.labelX5.TabIndex = 129;
		this.labelX5.Text = "Fecha:";
		this.labelX5.Click += new System.EventHandler(labelX5_Click);
		this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX4.Location = new System.Drawing.Point(11, 112);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(60, 33);
		this.labelX4.TabIndex = 128;
		this.labelX4.Text = "Vendedor: (Alt+V)";
		this.labelX4.WordWrap = true;
		this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX3.Location = new System.Drawing.Point(12, 76);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(55, 28);
		this.labelX3.TabIndex = 127;
		this.labelX3.Text = "Direccion: (Alt+D)";
		this.labelX3.WordWrap = true;
		this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX2.Location = new System.Drawing.Point(23, 53);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(48, 11);
		this.labelX2.TabIndex = 126;
		this.labelX2.Text = "Cliente:";
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX1.Location = new System.Drawing.Point(20, 15);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(47, 28);
		this.labelX1.TabIndex = 125;
		this.labelX1.Text = "Ruc/Dni (Alt+R):";
		this.labelX1.WordWrap = true;
		this.labelX1.Click += new System.EventHandler(labelX1_Click);
		this.txtNombreVendedor.Border.Class = "TextBoxBorder";
		this.txtNombreVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreVendedor.Location = new System.Drawing.Point(134, 115);
		this.txtNombreVendedor.Name = "txtNombreVendedor";
		this.txtNombreVendedor.PreventEnterBeep = true;
		this.txtNombreVendedor.Size = new System.Drawing.Size(191, 22);
		this.txtNombreVendedor.TabIndex = 123;
		this.txtNombreVendedor.Text = "<--  SELECCIONE UN VENDEDOR";
		this.txtCodigoVendedor.Border.Class = "TextBoxBorder";
		this.txtCodigoVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtCodigoVendedor.Location = new System.Drawing.Point(73, 115);
		this.txtCodigoVendedor.Name = "txtCodigoVendedor";
		this.txtCodigoVendedor.PreventEnterBeep = true;
		this.txtCodigoVendedor.Size = new System.Drawing.Size(54, 22);
		this.txtCodigoVendedor.TabIndex = 122;
		this.toolTip1.SetToolTip(this.txtCodigoVendedor, "Alt + V");
		this.superValidator1.SetValidator1(this.txtCodigoVendedor, this.requiredFieldValidator1);
		this.txtCodigoVendedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodigoVendedor_KeyDown);
		this.txtCodigoVendedor.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodigoVendedor_KeyUp);
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(263, 171);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(93, 22);
		this.dtpFechaPago.TabIndex = 120;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Value = new System.DateTime(2019, 5, 23, 0, 0, 0, 0);
		this.dtpFechaPago.Visible = false;
		this.cmbFormaPago.DisplayMember = "Text";
		this.cmbFormaPago.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.ItemHeight = 16;
		this.cmbFormaPago.Location = new System.Drawing.Point(73, 171);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(181, 22);
		this.cmbFormaPago.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.cmbFormaPago.TabIndex = 11;
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.chkFactura.Enabled = false;
		this.chkFactura.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkFactura.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkFactura.Location = new System.Drawing.Point(170, 199);
		this.chkFactura.Name = "chkFactura";
		this.chkFactura.Size = new System.Drawing.Size(80, 18);
		this.chkFactura.TabIndex = 9;
		this.chkFactura.Text = "FACTURA";
		this.chkFactura.UseVisualStyleBackColor = true;
		this.chkFactura.CheckedChanged += new System.EventHandler(chkFactura_CheckedChanged);
		this.chkFactura.Click += new System.EventHandler(chkFactura_Click);
		this.chkBoleta.AutoSize = true;
		this.chkBoleta.Checked = true;
		this.chkBoleta.Enabled = false;
		this.chkBoleta.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
		this.chkBoleta.FlatStyle = System.Windows.Forms.FlatStyle.System;
		this.chkBoleta.Location = new System.Drawing.Point(84, 199);
		this.chkBoleta.Name = "chkBoleta";
		this.chkBoleta.Size = new System.Drawing.Size(71, 18);
		this.chkBoleta.TabIndex = 8;
		this.chkBoleta.TabStop = true;
		this.chkBoleta.Text = "BOLETA";
		this.chkBoleta.UseVisualStyleBackColor = true;
		this.chkBoleta.CheckedChanged += new System.EventHandler(chkBoleta_CheckedChanged);
		this.chkBoleta.Click += new System.EventHandler(chkBoleta_Click);
		this.dtpFecha1.BackgroundStyle.Class = "DateTimeInputBackground";
		this.dtpFecha1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtpFecha1.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
		this.dtpFecha1.ButtonDropDown.Visible = true;
		this.dtpFecha1.DateTimeSelectorVisibility = DevComponents.Editors.DateTimeAdv.eDateTimeSelectorVisibility.Both;
		this.dtpFecha1.IsPopupCalendarOpen = false;
		this.dtpFecha1.Location = new System.Drawing.Point(281, 171);
		this.dtpFecha1.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtpFecha1.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
		this.dtpFecha1.MonthCalendar.ClearButtonVisible = true;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
		this.dtpFecha1.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtpFecha1.MonthCalendar.DayClickAutoClosePopup = false;
		this.dtpFecha1.MonthCalendar.DisplayMonth = new System.DateTime(2018, 12, 1, 0, 0, 0, 0);
		this.dtpFecha1.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.dtpFecha1.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
		this.dtpFecha1.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.dtpFecha1.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.dtpFecha1.MonthCalendar.TodayButtonVisible = true;
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(54, 22);
		this.dtpFecha1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.dtpFecha1.TabIndex = 7;
		this.dtpFecha1.Visible = false;
		this.txtDireccion.Border.Class = "TextBoxBorder";
		this.txtDireccion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtDireccion.Location = new System.Drawing.Point(73, 74);
		this.txtDireccion.Multiline = true;
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.PreventEnterBeep = true;
		this.txtDireccion.Size = new System.Drawing.Size(252, 35);
		this.txtDireccion.TabIndex = 5;
		this.toolTip1.SetToolTip(this.txtDireccion, "Alt + D");
		this.txtDireccion.DoubleClick += new System.EventHandler(txtDireccion_DoubleClick);
		this.txtNombreCliente.Border.Class = "TextBoxBorder";
		this.txtNombreCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreCliente.Location = new System.Drawing.Point(73, 46);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.PreventEnterBeep = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(252, 22);
		this.txtNombreCliente.TabIndex = 3;
		this.toolTip1.SetToolTip(this.txtNombreCliente, "Alt + C");
		this.txtNombreCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNombreCliente_KeyPress);
		this.txtCodCliente.Border.Class = "TextBoxBorder";
		this.txtCodCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtCodCliente.Location = new System.Drawing.Point(73, 18);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.PreventEnterBeep = true;
		this.txtCodCliente.Size = new System.Drawing.Size(119, 22);
		this.txtCodCliente.TabIndex = 1;
		this.toolTip1.SetToolTip(this.txtCodCliente, "Alt + R");
		this.txtCodCliente.TextChanged += new System.EventHandler(txtCodCliente_TextChanged);
		this.txtCodCliente.DoubleClick += new System.EventHandler(txtCodCliente_DoubleClick);
		this.txtCodCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyDown);
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyUp);
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.Controls.Add(this.label2);
		this.groupBox5.Controls.Add(this.txtIcbper);
		this.groupBox5.Controls.Add(this.txtexoneradas);
		this.groupBox5.Controls.Add(this.txtgravadas);
		this.groupBox5.Controls.Add(this.label16);
		this.groupBox5.Controls.Add(this.txtinafectas);
		this.groupBox5.Controls.Add(this.label15);
		this.groupBox5.Controls.Add(this.txtgratuitas);
		this.groupBox5.Controls.Add(this.label14);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.label13);
		this.groupBox5.Controls.Add(this.txtPrecioVenta);
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.txtIGV);
		this.groupBox5.Controls.Add(this.label8);
		this.groupBox5.Controls.Add(this.txtValorVenta);
		this.groupBox5.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox5.Location = new System.Drawing.Point(905, 485);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(371, 124);
		this.groupBox5.TabIndex = 24;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "TOTALES";
		this.groupBox5.Enter += new System.EventHandler(groupBox5_Enter);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(15, 106);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(45, 13);
		this.label2.TabIndex = 24;
		this.label2.Text = "Icbper :";
		this.txtIcbper.BackColor = System.Drawing.Color.AliceBlue;
		this.txtIcbper.Border.Class = "TextBoxBorder";
		this.txtIcbper.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtIcbper.Enabled = false;
		this.txtIcbper.Location = new System.Drawing.Point(81, 95);
		this.txtIcbper.Name = "txtIcbper";
		this.txtIcbper.PreventEnterBeep = true;
		this.txtIcbper.ReadOnly = true;
		this.txtIcbper.Size = new System.Drawing.Size(86, 22);
		this.txtIcbper.TabIndex = 23;
		this.txtexoneradas.BackColor = System.Drawing.Color.AliceBlue;
		this.txtexoneradas.Border.Class = "TextBoxBorder";
		this.txtexoneradas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtexoneradas.Enabled = false;
		this.txtexoneradas.Location = new System.Drawing.Point(265, 18);
		this.txtexoneradas.Name = "txtexoneradas";
		this.txtexoneradas.PreventEnterBeep = true;
		this.txtexoneradas.ReadOnly = true;
		this.txtexoneradas.Size = new System.Drawing.Size(96, 22);
		this.txtexoneradas.TabIndex = 21;
		this.txtgravadas.BackColor = System.Drawing.Color.AliceBlue;
		this.txtgravadas.Border.Class = "TextBoxBorder";
		this.txtgravadas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtgravadas.Enabled = false;
		this.txtgravadas.Location = new System.Drawing.Point(82, 18);
		this.txtgravadas.Name = "txtgravadas";
		this.txtgravadas.PreventEnterBeep = true;
		this.txtgravadas.ReadOnly = true;
		this.txtgravadas.Size = new System.Drawing.Size(86, 22);
		this.txtgravadas.TabIndex = 20;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(230, 46);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(33, 13);
		this.label16.TabIndex = 19;
		this.label16.Text = "Inaf :";
		this.txtinafectas.BackColor = System.Drawing.Color.AliceBlue;
		this.txtinafectas.Border.Class = "TextBoxBorder";
		this.txtinafectas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtinafectas.Enabled = false;
		this.txtinafectas.Location = new System.Drawing.Point(265, 43);
		this.txtinafectas.Name = "txtinafectas";
		this.txtinafectas.PreventEnterBeep = true;
		this.txtinafectas.ReadOnly = true;
		this.txtinafectas.Size = new System.Drawing.Size(96, 22);
		this.txtinafectas.TabIndex = 18;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(32, 46);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(35, 13);
		this.label15.TabIndex = 17;
		this.label15.Text = "Grat :";
		this.txtgratuitas.BackColor = System.Drawing.Color.AliceBlue;
		this.txtgratuitas.Border.Class = "TextBoxBorder";
		this.txtgratuitas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtgratuitas.Enabled = false;
		this.txtgratuitas.Location = new System.Drawing.Point(82, 43);
		this.txtgratuitas.Name = "txtgratuitas";
		this.txtgratuitas.PreventEnterBeep = true;
		this.txtgratuitas.ReadOnly = true;
		this.txtgratuitas.Size = new System.Drawing.Size(86, 22);
		this.txtgratuitas.TabIndex = 22;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(224, 19);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(39, 13);
		this.label14.TabIndex = 15;
		this.label14.Text = "Exon :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(29, 18);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(37, 13);
		this.label5.TabIndex = 13;
		this.label5.Text = "Grav :";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(9, 74);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(58, 13);
		this.label13.TabIndex = 11;
		this.label13.Text = "SubTotal :";
		this.txtPrecioVenta.BackColor = System.Drawing.Color.AliceBlue;
		this.txtPrecioVenta.Border.Class = "TextBoxBorder";
		this.txtPrecioVenta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtPrecioVenta.Enabled = false;
		this.txtPrecioVenta.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
		this.txtPrecioVenta.Location = new System.Drawing.Point(243, 91);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.PreventEnterBeep = true;
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(117, 32);
		this.txtPrecioVenta.TabIndex = 10;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
		this.label9.Location = new System.Drawing.Point(173, 98);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(68, 25);
		this.label9.TabIndex = 9;
		this.label9.Text = "TOTAL";
		this.label9.Click += new System.EventHandler(label9_Click);
		this.txtIGV.BackColor = System.Drawing.Color.AliceBlue;
		this.txtIGV.Border.Class = "TextBoxBorder";
		this.txtIGV.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtIGV.Enabled = false;
		this.txtIGV.Location = new System.Drawing.Point(264, 67);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.PreventEnterBeep = true;
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(96, 22);
		this.txtIGV.TabIndex = 8;
		this.txtIGV.TextChanged += new System.EventHandler(txtIGV_TextChanged);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(233, 70);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(29, 13);
		this.label8.TabIndex = 7;
		this.label8.Text = "Igv :";
		this.label8.Click += new System.EventHandler(label8_Click);
		this.txtValorVenta.BackColor = System.Drawing.Color.AliceBlue;
		this.txtValorVenta.Border.Class = "TextBoxBorder";
		this.txtValorVenta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtValorVenta.Enabled = false;
		this.txtValorVenta.Location = new System.Drawing.Point(81, 68);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.PreventEnterBeep = true;
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(86, 22);
		this.txtValorVenta.TabIndex = 6;
		this.txtmontoordencompra.Border.Class = "TextBoxBorder";
		this.txtmontoordencompra.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtmontoordencompra.Enabled = false;
		this.txtmontoordencompra.Location = new System.Drawing.Point(263, 28);
		this.txtmontoordencompra.Name = "txtmontoordencompra";
		this.txtmontoordencompra.PreventEnterBeep = true;
		this.txtmontoordencompra.ReadOnly = true;
		this.txtmontoordencompra.Size = new System.Drawing.Size(94, 20);
		this.txtmontoordencompra.TabIndex = 154;
		this.toolTip1.SetToolTip(this.txtmontoordencompra, "Alt + R");
		this.txtnumeroordencompra.Border.Class = "TextBoxBorder";
		this.txtnumeroordencompra.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtnumeroordencompra.Enabled = false;
		this.txtnumeroordencompra.Location = new System.Drawing.Point(143, 27);
		this.txtnumeroordencompra.Name = "txtnumeroordencompra";
		this.txtnumeroordencompra.PreventEnterBeep = true;
		this.txtnumeroordencompra.ReadOnly = true;
		this.txtnumeroordencompra.Size = new System.Drawing.Size(96, 20);
		this.txtnumeroordencompra.TabIndex = 153;
		this.toolTip1.SetToolTip(this.txtnumeroordencompra, "Alt + R");
		this.txtnumeroordencompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtnumeroordencompra_KeyPress);
		this.groupBox6.Controls.Add(this.btnAnulaOV);
		this.groupBox6.Controls.Add(this.btnEditaOV);
		this.groupBox6.Controls.Add(this.btnInicioOV);
		this.groupBox6.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox6.Location = new System.Drawing.Point(1077, 86);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(85, 19);
		this.groupBox6.TabIndex = 28;
		this.groupBox6.TabStop = false;
		this.groupBox6.Visible = false;
		this.btnAnulaOV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnAnulaOV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnAnulaOV.Enabled = false;
		this.btnAnulaOV.Location = new System.Drawing.Point(269, 18);
		this.btnAnulaOV.Name = "btnAnulaOV";
		this.btnAnulaOV.Size = new System.Drawing.Size(75, 23);
		this.btnAnulaOV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnAnulaOV.TabIndex = 2;
		this.btnAnulaOV.Text = "Anula OV";
		this.btnAnulaOV.Click += new System.EventHandler(btnAnulaOV_Click);
		this.btnEditaOV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnEditaOV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnEditaOV.Location = new System.Drawing.Point(153, 18);
		this.btnEditaOV.Name = "btnEditaOV";
		this.btnEditaOV.Size = new System.Drawing.Size(75, 23);
		this.btnEditaOV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnEditaOV.TabIndex = 1;
		this.btnEditaOV.Text = "F4 Edita  OV";
		this.btnEditaOV.Click += new System.EventHandler(btnEditaOV_Click);
		this.btnInicioOV.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnInicioOV.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnInicioOV.Location = new System.Drawing.Point(33, 18);
		this.btnInicioOV.Margin = new System.Windows.Forms.Padding(0);
		this.btnInicioOV.Name = "btnInicioOV";
		this.btnInicioOV.Size = new System.Drawing.Size(75, 23);
		this.btnInicioOV.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnInicioOV.TabIndex = 0;
		this.btnInicioOV.Text = "F6 Inicia OV";
		this.btnInicioOV.Click += new System.EventHandler(btnInicioOV_Click);
		this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox7.BackColor = System.Drawing.Color.White;
		this.groupBox7.Controls.Add(this.txtLineaCredito);
		this.groupBox7.Controls.Add(this.label25);
		this.groupBox7.Controls.Add(this.txttasa);
		this.groupBox7.Controls.Add(this.label30);
		this.groupBox7.Controls.Add(this.txtLineaCreditoUso);
		this.groupBox7.Controls.Add(this.chkVentaDsctoGlobal);
		this.groupBox7.Controls.Add(this.chkVentaGratuita);
		this.groupBox7.Controls.Add(this.txtBruto);
		this.groupBox7.Controls.Add(this.txtDscto);
		this.groupBox7.Controls.Add(this.label19);
		this.groupBox7.Controls.Add(this.label20);
		this.groupBox7.Controls.Add(this.pbCapchatS);
		this.groupBox7.Controls.Add(this.txtSunat_Capchat);
		this.groupBox7.Controls.Add(this.lbLineaCredito);
		this.groupBox7.Controls.Add(this.label23);
		this.groupBox7.Controls.Add(this.txtLineaCreditoDisponible);
		this.groupBox7.Enabled = false;
		this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox7.Location = new System.Drawing.Point(905, 424);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(371, 62);
		this.groupBox7.TabIndex = 133;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "Condiciones de Crédito:";
		this.groupBox7.Enter += new System.EventHandler(groupBox7_Enter);
		this.txtLineaCredito.Enabled = false;
		this.txtLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCredito.Location = new System.Drawing.Point(102, 17);
		this.txtLineaCredito.Name = "txtLineaCredito";
		this.txtLineaCredito.ReadOnly = true;
		this.txtLineaCredito.Size = new System.Drawing.Size(84, 18);
		this.txtLineaCredito.TabIndex = 84;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.Location = new System.Drawing.Point(186, 20);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(93, 12);
		this.label25.TabIndex = 97;
		this.label25.Text = "Línea C. en Uso (S/.):";
		this.txttasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txttasa.Location = new System.Drawing.Point(282, 38);
		this.txttasa.Name = "txttasa";
		this.txttasa.ReadOnly = true;
		this.txttasa.Size = new System.Drawing.Size(84, 18);
		this.txttasa.TabIndex = 106;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label30.Location = new System.Drawing.Point(208, 41);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(71, 12);
		this.label30.TabIndex = 105;
		this.label30.Text = "Tasa de Interés:";
		this.txtLineaCreditoUso.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoUso.Location = new System.Drawing.Point(282, 17);
		this.txtLineaCreditoUso.Name = "txtLineaCreditoUso";
		this.txtLineaCreditoUso.ReadOnly = true;
		this.txtLineaCreditoUso.Size = new System.Drawing.Size(84, 18);
		this.txtLineaCreditoUso.TabIndex = 98;
		this.chkVentaDsctoGlobal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaDsctoGlobal.AutoSize = true;
		this.chkVentaDsctoGlobal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.chkVentaDsctoGlobal.Location = new System.Drawing.Point(251, -9);
		this.chkVentaDsctoGlobal.Name = "chkVentaDsctoGlobal";
		this.chkVentaDsctoGlobal.Size = new System.Drawing.Size(100, 15);
		this.chkVentaDsctoGlobal.TabIndex = 131;
		this.chkVentaDsctoGlobal.Text = "Vta. Descuento";
		this.chkVentaDsctoGlobal.Visible = false;
		this.chkVentaGratuita.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaGratuita.AutoSize = true;
		this.chkVentaGratuita.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.chkVentaGratuita.Location = new System.Drawing.Point(251, -30);
		this.chkVentaGratuita.Name = "chkVentaGratuita";
		this.chkVentaGratuita.Size = new System.Drawing.Size(87, 15);
		this.chkVentaGratuita.TabIndex = 130;
		this.chkVentaGratuita.Text = "Vta. Gratuita";
		this.chkVentaGratuita.Visible = false;
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(263, 12);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(62, 20);
		this.txtBruto.TabIndex = 126;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtBruto.Visible = false;
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(262, 35);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(63, 20);
		this.txtDscto.TabIndex = 127;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDscto.Visible = false;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(214, 38);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(42, 13);
		this.label19.TabIndex = 129;
		this.label19.Text = "Dcto :";
		this.label19.Visible = false;
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(210, 15);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(45, 13);
		this.label20.TabIndex = 128;
		this.label20.Text = "Bruto :";
		this.label20.Visible = false;
		this.pbCapchatS.Location = new System.Drawing.Point(349, 55);
		this.pbCapchatS.Name = "pbCapchatS";
		this.pbCapchatS.Size = new System.Drawing.Size(19, 21);
		this.pbCapchatS.TabIndex = 123;
		this.pbCapchatS.TabStop = false;
		this.pbCapchatS.Visible = false;
		this.txtSunat_Capchat.Location = new System.Drawing.Point(331, 56);
		this.txtSunat_Capchat.Name = "txtSunat_Capchat";
		this.txtSunat_Capchat.Size = new System.Drawing.Size(16, 20);
		this.txtSunat_Capchat.TabIndex = 122;
		this.txtSunat_Capchat.Visible = false;
		this.lbLineaCredito.AutoSize = true;
		this.lbLineaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbLineaCredito.Location = new System.Drawing.Point(5, 20);
		this.lbLineaCredito.Name = "lbLineaCredito";
		this.lbLineaCredito.Size = new System.Drawing.Size(94, 12);
		this.lbLineaCredito.TabIndex = 85;
		this.lbLineaCredito.Text = "Línea de Crédito (S/.):";
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.Location = new System.Drawing.Point(4, 40);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(95, 12);
		this.label23.TabIndex = 95;
		this.label23.Text = "Línea Disponible (S/.):";
		this.txtLineaCreditoDisponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtLineaCreditoDisponible.Location = new System.Drawing.Point(102, 40);
		this.txtLineaCreditoDisponible.Name = "txtLineaCreditoDisponible";
		this.txtLineaCreditoDisponible.ReadOnly = true;
		this.txtLineaCreditoDisponible.Size = new System.Drawing.Size(84, 18);
		this.txtLineaCreditoDisponible.TabIndex = 96;
		this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox8.Controls.Add(this.txtCodigoBarras);
		this.groupBox8.Controls.Add(this.chkVentaSinStock);
		this.groupBox8.Controls.Add(this.label21);
		this.groupBox8.Controls.Add(this.lbDocumento);
		this.groupBox8.Controls.Add(this.txtDocRef);
		this.groupBox8.Controls.Add(this.label12);
		this.groupBox8.Controls.Add(this.txtPedido);
		this.groupBox8.Controls.Add(this.txtSerie);
		this.groupBox8.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox8.Location = new System.Drawing.Point(686, 42);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(208, 53);
		this.groupBox8.TabIndex = 134;
		this.groupBox8.TabStop = false;
		this.txtCodigoBarras.BackColor = System.Drawing.Color.PeachPuff;
		this.txtCodigoBarras.Border.Class = "TextBoxBorder";
		this.txtCodigoBarras.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodigoBarras.Location = new System.Drawing.Point(158, 17);
		this.txtCodigoBarras.Multiline = true;
		this.txtCodigoBarras.Name = "txtCodigoBarras";
		this.txtCodigoBarras.PreventEnterBeep = true;
		this.txtCodigoBarras.Size = new System.Drawing.Size(32, 23);
		this.txtCodigoBarras.TabIndex = 134;
		this.txtCodigoBarras.Text = "l";
		this.txtCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodigoBarras.Visible = false;
		this.chkVentaSinStock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkVentaSinStock.AutoSize = true;
		this.chkVentaSinStock.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.chkVentaSinStock.Location = new System.Drawing.Point(226, -130);
		this.chkVentaSinStock.Name = "chkVentaSinStock";
		this.chkVentaSinStock.Size = new System.Drawing.Size(94, 17);
		this.chkVentaSinStock.TabIndex = 133;
		this.chkVentaSinStock.Text = "Vta. Sin Stock";
		this.chkVentaSinStock.Visible = false;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Segoe UI Black", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.Location = new System.Drawing.Point(480, 17);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(15, 19);
		this.label21.TabIndex = 49;
		this.label21.Text = "-";
		this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label21.Visible = false;
		this.lbDocumento.AutoSize = true;
		this.lbDocumento.Font = new System.Drawing.Font("Segoe UI", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDocumento.Location = new System.Drawing.Point(35, 14);
		this.lbDocumento.Name = "lbDocumento";
		this.lbDocumento.Size = new System.Drawing.Size(117, 25);
		this.lbDocumento.TabIndex = 48;
		this.lbDocumento.Tag = "22";
		this.lbDocumento.Text = "Documento";
		this.lbDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lbDocumento.Visible = false;
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.Border.Class = "TextBoxBorder";
		this.txtDocRef.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(64, 14);
		this.txtDocRef.Multiline = true;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.PreventEnterBeep = true;
		this.txtDocRef.Size = new System.Drawing.Size(32, 23);
		this.txtDocRef.TabIndex = 47;
		this.txtDocRef.Text = "L";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtDocRef.Visible = false;
		this.txtDocRef.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDocRef_KeyPress);
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(6, 18);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(52, 13);
		this.label12.TabIndex = 46;
		this.label12.Text = "Doc. Ref.";
		this.label12.Visible = false;
		this.txtPedido.Border.Class = "TextBoxBorder";
		this.txtPedido.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtPedido.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPedido.Location = new System.Drawing.Point(501, 14);
		this.txtPedido.Multiline = true;
		this.txtPedido.Name = "txtPedido";
		this.txtPedido.PreventEnterBeep = true;
		this.txtPedido.Size = new System.Drawing.Size(117, 24);
		this.txtPedido.TabIndex = 45;
		this.txtPedido.Text = "00000000";
		this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtSerie.Border.Class = "TextBoxBorder";
		this.txtSerie.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtSerie.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(401, 14);
		this.txtSerie.Multiline = true;
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.PreventEnterBeep = true;
		this.txtSerie.Size = new System.Drawing.Size(68, 23);
		this.txtSerie.TabIndex = 0;
		this.txtSerie.Text = "000";
		this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Segoe UI Black", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(161, 20);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(15, 19);
		this.label22.TabIndex = 140;
		this.label22.Text = "-";
		this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.textBoxX1.Border.Class = "TextBoxBorder";
		this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX1.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX1.Location = new System.Drawing.Point(182, 17);
		this.textBoxX1.Name = "textBoxX1";
		this.textBoxX1.PreventEnterBeep = true;
		this.textBoxX1.ReadOnly = true;
		this.textBoxX1.Size = new System.Drawing.Size(117, 25);
		this.textBoxX1.TabIndex = 139;
		this.textBoxX1.Text = "00000000000";
		this.textBoxX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBoxX1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxX1_KeyPress);
		this.textBoxX1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(textBoxX1_MouseDoubleClick);
		this.textBoxX2.Border.Class = "TextBoxBorder";
		this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX2.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX2.Location = new System.Drawing.Point(82, 17);
		this.textBoxX2.Multiline = true;
		this.textBoxX2.Name = "textBoxX2";
		this.textBoxX2.PreventEnterBeep = true;
		this.textBoxX2.ReadOnly = true;
		this.textBoxX2.Size = new System.Drawing.Size(68, 30);
		this.textBoxX2.TabIndex = 138;
		this.textBoxX2.Text = "000";
		this.textBoxX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.groupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox9.Controls.Add(this.label24);
		this.groupBox9.Controls.Add(this.label22);
		this.groupBox9.Controls.Add(this.textBoxX5);
		this.groupBox9.Controls.Add(this.textBoxX1);
		this.groupBox9.Controls.Add(this.textBoxX2);
		this.groupBox9.Controls.Add(this.textBoxX6);
		this.groupBox9.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox9.Location = new System.Drawing.Point(905, 40);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(371, 53);
		this.groupBox9.TabIndex = 141;
		this.groupBox9.TabStop = false;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Segoe UI Black", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(480, 17);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(15, 19);
		this.label24.TabIndex = 49;
		this.label24.Text = "-";
		this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label24.Visible = false;
		this.textBoxX5.Border.Class = "TextBoxBorder";
		this.textBoxX5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX5.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX5.Location = new System.Drawing.Point(501, 14);
		this.textBoxX5.Multiline = true;
		this.textBoxX5.Name = "textBoxX5";
		this.textBoxX5.PreventEnterBeep = true;
		this.textBoxX5.Size = new System.Drawing.Size(117, 24);
		this.textBoxX5.TabIndex = 45;
		this.textBoxX5.Text = "00000000";
		this.textBoxX5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBoxX6.Border.Class = "TextBoxBorder";
		this.textBoxX6.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX6.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX6.Location = new System.Drawing.Point(401, 14);
		this.textBoxX6.Multiline = true;
		this.textBoxX6.Name = "textBoxX6";
		this.textBoxX6.PreventEnterBeep = true;
		this.textBoxX6.Size = new System.Drawing.Size(68, 23);
		this.textBoxX6.TabIndex = 0;
		this.textBoxX6.Text = "000";
		this.textBoxX6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.requiredFieldValidator1.ErrorMessage = "Seleccionar Vendedor";
		this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.dgvStockAlmacenes.AllowUserToAddRows = false;
		this.dgvStockAlmacenes.AllowUserToDeleteRows = false;
		dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle46.BackColor = System.Drawing.Color.FromArgb(81, 124, 210);
		dataGridViewCellStyle46.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvStockAlmacenes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
		this.dgvStockAlmacenes.ColumnHeadersHeight = 26;
		this.dgvStockAlmacenes.Columns.AddRange(this.idalmacen, this.idproductoalmacen, this.nomempresa, this.nomalmacen, this.stockalmacen, this.colUnidad);
		this.dgvStockAlmacenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvStockAlmacenes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvStockAlmacenes.EnableHeadersVisualStyles = false;
		this.dgvStockAlmacenes.Location = new System.Drawing.Point(3, 15);
		this.dgvStockAlmacenes.Name = "dgvStockAlmacenes";
		this.dgvStockAlmacenes.ReadOnly = true;
		this.dgvStockAlmacenes.RowHeadersVisible = false;
		this.dgvStockAlmacenes.Size = new System.Drawing.Size(469, 96);
		this.dgvStockAlmacenes.TabIndex = 146;
		this.idalmacen.DataPropertyName = "idalmacen";
		this.idalmacen.HeaderText = "idalmacen";
		this.idalmacen.Name = "idalmacen";
		this.idalmacen.ReadOnly = true;
		this.idalmacen.Visible = false;
		this.idproductoalmacen.DataPropertyName = "idproductoalmacen";
		this.idproductoalmacen.HeaderText = "idproductoalmacen";
		this.idproductoalmacen.Name = "idproductoalmacen";
		this.idproductoalmacen.ReadOnly = true;
		this.idproductoalmacen.Visible = false;
		this.nomempresa.DataPropertyName = "nomempresa";
		this.nomempresa.HeaderText = "Empresa";
		this.nomempresa.Name = "nomempresa";
		this.nomempresa.ReadOnly = true;
		this.nomalmacen.DataPropertyName = "nomalmacen";
		this.nomalmacen.HeaderText = "Almacen";
		this.nomalmacen.Name = "nomalmacen";
		this.nomalmacen.ReadOnly = true;
		this.nomalmacen.Width = 150;
		this.stockalmacen.DataPropertyName = "stockalmacen";
		this.stockalmacen.HeaderText = "Stock";
		this.stockalmacen.Name = "stockalmacen";
		this.stockalmacen.ReadOnly = true;
		this.colUnidad.DataPropertyName = "descUnidad";
		this.colUnidad.HeaderText = "Unidad Base";
		this.colUnidad.Name = "colUnidad";
		this.colUnidad.ReadOnly = true;
		this.groupBox10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox10.Controls.Add(this.dgvStockAlmacenes);
		this.groupBox10.Controls.Add(this.label1);
		this.groupBox10.Controls.Add(this.cmbAlmacenes);
		this.groupBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox10.Location = new System.Drawing.Point(13, 285);
		this.groupBox10.Name = "groupBox10";
		this.groupBox10.Size = new System.Drawing.Size(475, 114);
		this.groupBox10.TabIndex = 147;
		this.groupBox10.TabStop = false;
		this.groupBox10.Text = "DETALLE PRODUCTO";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(592, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(133, 13);
		this.label1.TabIndex = 148;
		this.label1.Text = "Seleccion de almacen:";
		this.label1.Visible = false;
		this.cmbAlmacenes.Enabled = false;
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(525, 44);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(310, 21);
		this.cmbAlmacenes.TabIndex = 147;
		this.cmbAlmacenes.Visible = false;
		this.cmbAlmacenes.SelectionChangeCommitted += new System.EventHandler(cmbAlmacenes_SelectionChangeCommitted);
		this.groupBox11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox11.Controls.Add(this.chbxstock);
		this.groupBox11.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox11.Location = new System.Drawing.Point(565, 44);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(115, 51);
		this.groupBox11.TabIndex = 310;
		this.groupBox11.TabStop = false;
		this.groupBox11.Text = "REQUERIMIENTO";
		this.chbxstock.AutoSize = true;
		this.chbxstock.Location = new System.Drawing.Point(52, 22);
		this.chbxstock.Name = "chbxstock";
		this.chbxstock.Size = new System.Drawing.Size(15, 14);
		this.chbxstock.TabIndex = 0;
		this.chbxstock.UseVisualStyleBackColor = true;
		this.chbxstock.CheckedChanged += new System.EventHandler(chbxstock_CheckedChanged);
		this.chbxstock.Click += new System.EventHandler(chbxstock_Click);
		this.btnreq.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnreq.Image = SIGEFA.Properties.Resources.acep;
		this.btnreq.Location = new System.Drawing.Point(371, 407);
		this.btnreq.Name = "btnreq";
		this.btnreq.Size = new System.Drawing.Size(114, 46);
		this.btnreq.TabIndex = 311;
		this.btnreq.Text = "Solicitar Requerimiento";
		this.btnreq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnreq.UseVisualStyleBackColor = true;
		this.btnreq.Visible = false;
		this.btnreq.Click += new System.EventHandler(btnreq_Click);
		this.pictureBox1.Location = new System.Drawing.Point(1168, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(137, 50);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 151;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
		this.gbTecnico.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gbTecnico.BackColor = System.Drawing.Color.White;
		this.gbTecnico.Controls.Add(this.cmbCanalVenta);
		this.gbTecnico.Controls.Add(this.label4);
		this.gbTecnico.Controls.Add(this.cmbZona);
		this.gbTecnico.Controls.Add(this.label3);
		this.gbTecnico.Controls.Add(this.cmbTecnico);
		this.gbTecnico.Controls.Add(this.lblusuariodesp);
		this.gbTecnico.Enabled = false;
		this.gbTecnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.gbTecnico.Location = new System.Drawing.Point(905, 322);
		this.gbTecnico.Name = "gbTecnico";
		this.gbTecnico.Size = new System.Drawing.Size(372, 96);
		this.gbTecnico.TabIndex = 133;
		this.gbTecnico.TabStop = false;
		this.gbTecnico.Enter += new System.EventHandler(groupBox7_Enter);
		this.cmbCanalVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbCanalVenta.Enabled = false;
		this.cmbCanalVenta.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.cmbCanalVenta.FormattingEnabled = true;
		this.cmbCanalVenta.Location = new System.Drawing.Point(93, 68);
		this.cmbCanalVenta.Name = "cmbCanalVenta";
		this.cmbCanalVenta.Size = new System.Drawing.Size(187, 21);
		this.cmbCanalVenta.TabIndex = 186;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(7, 74);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(80, 13);
		this.label4.TabIndex = 185;
		this.label4.Text = "Canal Venta:";
		this.cmbZona.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.cmbZona.FormattingEnabled = true;
		this.cmbZona.Location = new System.Drawing.Point(93, 41);
		this.cmbZona.Name = "cmbZona";
		this.cmbZona.Size = new System.Drawing.Size(187, 21);
		this.cmbZona.TabIndex = 186;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(7, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 13);
		this.label3.TabIndex = 185;
		this.label3.Text = "Zona:";
		this.cmbTecnico.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.cmbTecnico.FormattingEnabled = true;
		this.cmbTecnico.Location = new System.Drawing.Point(93, 14);
		this.cmbTecnico.Name = "cmbTecnico";
		this.cmbTecnico.Size = new System.Drawing.Size(187, 21);
		this.cmbTecnico.TabIndex = 186;
		this.cmbTecnico.KeyUp += new System.Windows.Forms.KeyEventHandler(cmbTecnico_KeyUp);
		this.lblusuariodesp.AutoSize = true;
		this.lblusuariodesp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblusuariodesp.Location = new System.Drawing.Point(6, 17);
		this.lblusuariodesp.Name = "lblusuariodesp";
		this.lblusuariodesp.Size = new System.Drawing.Size(57, 13);
		this.lblusuariodesp.TabIndex = 185;
		this.lblusuariodesp.Text = "Tecnico:";
		this.cmbCategoriaCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbCategoriaCliente.Enabled = false;
		this.cmbCategoriaCliente.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.cmbCategoriaCliente.FormattingEnabled = true;
		this.cmbCategoriaCliente.Location = new System.Drawing.Point(93, 15);
		this.cmbCategoriaCliente.Name = "cmbCategoriaCliente";
		this.cmbCategoriaCliente.Size = new System.Drawing.Size(170, 21);
		this.cmbCategoriaCliente.TabIndex = 188;
		this.cmbCategoriaCliente.SelectedIndexChanged += new System.EventHandler(cmbCategoriaCliente_SelectedIndexChanged);
		this.lblcategoriacliente.AutoSize = true;
		this.lblcategoriacliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblcategoriacliente.Location = new System.Drawing.Point(6, 19);
		this.lblcategoriacliente.Name = "lblcategoriacliente";
		this.lblcategoriacliente.Size = new System.Drawing.Size(87, 13);
		this.lblcategoriacliente.TabIndex = 187;
		this.lblcategoriacliente.Text = "Categ.Cliente:";
		this.groupBox12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox12.BackColor = System.Drawing.Color.White;
		this.groupBox12.Controls.Add(this.cmbCategoriaCliente);
		this.groupBox12.Controls.Add(this.lblcategoriacliente);
		this.groupBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox12.Location = new System.Drawing.Point(15, 409);
		this.groupBox12.Name = "groupBox12";
		this.groupBox12.Size = new System.Drawing.Size(271, 43);
		this.groupBox12.TabIndex = 187;
		this.groupBox12.TabStop = false;
		this.txtcodCotizacion.BackColor = System.Drawing.Color.AliceBlue;
		this.txtcodCotizacion.Border.Class = "TextBoxBorder";
		this.txtcodCotizacion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtcodCotizacion.Enabled = false;
		this.txtcodCotizacion.Location = new System.Drawing.Point(847, 441);
		this.txtcodCotizacion.Name = "txtcodCotizacion";
		this.txtcodCotizacion.PreventEnterBeep = true;
		this.txtcodCotizacion.ReadOnly = true;
		this.txtcodCotizacion.Size = new System.Drawing.Size(40, 20);
		this.txtcodCotizacion.TabIndex = 315;
		this.txtcodCotizacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtcodCotizacion.Visible = false;
		this.lblcotizacion.AutoSize = true;
		this.lblcotizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblcotizacion.Location = new System.Drawing.Point(773, 443);
		this.lblcotizacion.Name = "lblcotizacion";
		this.lblcotizacion.Size = new System.Drawing.Size(70, 13);
		this.lblcotizacion.TabIndex = 314;
		this.lblcotizacion.Text = "Cotización:";
		this.lblcotizacion.Visible = false;
		this.gbordencompra.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gbordencompra.BackColor = System.Drawing.Color.White;
		this.gbordencompra.Controls.Add(this.labelX9);
		this.gbordencompra.Controls.Add(this.lblnumero);
		this.gbordencompra.Controls.Add(this.txtmontoordencompra);
		this.gbordencompra.Controls.Add(this.txtnumeroordencompra);
		this.gbordencompra.Controls.Add(this.chbordencompra);
		this.gbordencompra.Enabled = false;
		this.gbordencompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.gbordencompra.Location = new System.Drawing.Point(491, 288);
		this.gbordencompra.Name = "gbordencompra";
		this.gbordencompra.Size = new System.Drawing.Size(396, 62);
		this.gbordencompra.TabIndex = 316;
		this.gbordencompra.TabStop = false;
		this.gbordencompra.Text = "Número Orden Compra:";
		this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX9.Location = new System.Drawing.Point(290, 13);
		this.labelX9.Name = "labelX9";
		this.labelX9.Size = new System.Drawing.Size(48, 11);
		this.labelX9.TabIndex = 155;
		this.labelX9.Text = "Monto:";
		this.lblnumero.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblnumero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.lblnumero.Location = new System.Drawing.Point(162, 12);
		this.lblnumero.Name = "lblnumero";
		this.lblnumero.Size = new System.Drawing.Size(48, 11);
		this.lblnumero.TabIndex = 153;
		this.lblnumero.Text = "Número:";
		this.chbordencompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chbordencompra.Location = new System.Drawing.Point(6, 26);
		this.chbordencompra.Name = "chbordencompra";
		this.chbordencompra.Size = new System.Drawing.Size(152, 22);
		this.chbordencompra.TabIndex = 153;
		this.chbordencompra.Text = "Orden De Compra";
		this.chbordencompra.UseVisualStyleBackColor = true;
		this.groupBox13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox13.Controls.Add(this.btndescuento);
		this.groupBox13.Controls.Add(this.chkTodos);
		this.groupBox13.Controls.Add(this.txtdescuento);
		this.groupBox13.Controls.Add(this.chkdescuento);
		this.groupBox13.Controls.Add(this.chkTicket);
		this.groupBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox13.Location = new System.Drawing.Point(491, 356);
		this.groupBox13.Name = "groupBox13";
		this.groupBox13.Size = new System.Drawing.Size(396, 79);
		this.groupBox13.TabIndex = 317;
		this.groupBox13.TabStop = false;
		this.groupBox13.Text = "Aplicar Descuento";
		this.btndescuento.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btndescuento.Image = (System.Drawing.Image)resources.GetObject("btndescuento.Image");
		this.btndescuento.Location = new System.Drawing.Point(225, 34);
		this.btndescuento.Name = "btndescuento";
		this.btndescuento.Size = new System.Drawing.Size(120, 32);
		this.btndescuento.TabIndex = 55;
		this.btndescuento.Text = "Descuento";
		this.btndescuento.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btndescuento.UseVisualStyleBackColor = true;
		this.btndescuento.Click += new System.EventHandler(btndescuento_Click_1);
		this.chkTodos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.chkTodos.AutoSize = true;
		this.chkTodos.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.chkTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkTodos.Location = new System.Drawing.Point(43, 29);
		this.chkTodos.Name = "chkTodos";
		this.chkTodos.Size = new System.Drawing.Size(46, 31);
		this.chkTodos.TabIndex = 11;
		this.chkTodos.Text = "Todos";
		this.chkTodos.UseVisualStyleBackColor = true;
		this.chkTodos.CheckedChanged += new System.EventHandler(chkTodos_CheckedChanged_1);
		this.txtdescuento.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtdescuento.Location = new System.Drawing.Point(94, 46);
		this.txtdescuento.Name = "txtdescuento";
		this.txtdescuento.Size = new System.Drawing.Size(114, 20);
		this.txtdescuento.TabIndex = 25;
		this.txtdescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtdescuento.KeyDown += new System.Windows.Forms.KeyEventHandler(txtdescuento_KeyDown_1);
		this.chkdescuento.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.chkdescuento.AutoSize = true;
		this.chkdescuento.Location = new System.Drawing.Point(95, 25);
		this.chkdescuento.Name = "chkdescuento";
		this.chkdescuento.Size = new System.Drawing.Size(113, 17);
		this.chkdescuento.TabIndex = 22;
		this.chkdescuento.Text = "Descuento (M):";
		this.chkdescuento.UseVisualStyleBackColor = true;
		this.chkdescuento.Visible = false;
		this.chkTicket.AutoSize = true;
		this.chkTicket.Enabled = false;
		this.chkTicket.Location = new System.Drawing.Point(195, 10);
		this.chkTicket.Name = "chkTicket";
		this.chkTicket.Size = new System.Drawing.Size(59, 17);
		this.chkTicket.TabIndex = 124;
		this.chkTicket.TabStop = true;
		this.chkTicket.Text = "SDNV";
		this.chkTicket.UseVisualStyleBackColor = true;
		this.chkTicket.CheckedChanged += new System.EventHandler(chkTicket_CheckedChanged_1);
		this.chkTicket.Click += new System.EventHandler(chkTicket_Click);
		this.groupBox14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox14.Controls.Add(this.btnvercombos);
		this.groupBox14.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox14.Location = new System.Drawing.Point(465, 44);
		this.groupBox14.Name = "groupBox14";
		this.groupBox14.Size = new System.Drawing.Size(94, 51);
		this.groupBox14.TabIndex = 318;
		this.groupBox14.TabStop = false;
		this.groupBox14.Text = "COMBOS";
		this.btnvercombos.Enabled = false;
		this.btnvercombos.Location = new System.Drawing.Point(13, 18);
		this.btnvercombos.Name = "btnvercombos";
		this.btnvercombos.Size = new System.Drawing.Size(75, 23);
		this.btnvercombos.TabIndex = 0;
		this.btnvercombos.Text = "Ver";
		this.btnvercombos.UseVisualStyleBackColor = true;
		this.btnvercombos.Click += new System.EventHandler(btnvercombos_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.CaptionAntiAlias = false;
		base.CaptionFont = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.ClientSize = new System.Drawing.Size(1299, 621);
		base.Controls.Add(this.groupBox14);
		base.Controls.Add(this.groupBox13);
		base.Controls.Add(this.gbordencompra);
		base.Controls.Add(this.txtcodCotizacion);
		base.Controls.Add(this.lblcotizacion);
		base.Controls.Add(this.groupBox12);
		base.Controls.Add(this.btnreq);
		base.Controls.Add(this.groupBox11);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.groupBox10);
		base.Controls.Add(this.groupBox9);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox8);
		base.Controls.Add(this.gbTecnico);
		base.Controls.Add(this.groupBox7);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.panel);
		this.DoubleBuffered = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.Name = "frmVenta2019";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ORDEN DE VENTA";
		base.Load += new System.EventHandler(frmVenta2019_Load);
		base.Shown += new System.EventHandler(frmVenta2019_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmVenta2019_KeyDown);
		base.KeyUp += new System.Windows.Forms.KeyEventHandler(frmVenta2019_KeyUp);
		this.panel.ResumeLayout(false);
		this.panel.PerformLayout();
		this.toolStrip2.ResumeLayout(false);
		this.toolStrip2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvproductos).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvdetalle).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFecha1).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCapchatS).EndInit();
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvStockAlmacenes).EndInit();
		this.groupBox10.ResumeLayout(false);
		this.groupBox10.PerformLayout();
		this.groupBox11.ResumeLayout(false);
		this.groupBox11.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.gbTecnico.ResumeLayout(false);
		this.gbTecnico.PerformLayout();
		this.groupBox12.ResumeLayout(false);
		this.groupBox12.PerformLayout();
		this.gbordencompra.ResumeLayout(false);
		this.groupBox13.ResumeLayout(false);
		this.groupBox13.PerformLayout();
		this.groupBox14.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
