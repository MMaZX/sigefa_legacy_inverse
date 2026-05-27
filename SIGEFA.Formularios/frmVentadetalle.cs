using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.SunatFacElec;

namespace SIGEFA.Formularios;

public class frmVentadetalle : Office2007Form
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

	private IContainer components = null;

	private GroupBox groupBox1;

	public DataGridView dgvDetalle;

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

	private DataGridViewCheckBoxColumn seleccionar;

	public Button btnGuardar;

	private Button btnSalir;

	public byte[] firmadigital { get; set; }

	private void frmVentadetalle_Load(object sender, EventArgs e)
	{
		CargaDetalle();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public frmVentadetalle()
	{
		InitializeComponent();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		frmSeparacionDetalle formv = new frmSeparacionDetalle();
		formv.Show();
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmVenta.CargaDetalle(Convert.ToInt32(CodVenta), frmLogin.iCodAlmacen, 0);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
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
		this.seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Location = new System.Drawing.Point(12, 22);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(740, 337);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle de Productos";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codSalida1, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.MiTipoImpuesto, this.precioreal, this.valoreal, this.stockPend, this.dsctoMax, this.coduser, this.fecharegistro, this.seleccionar);
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle25;
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(734, 318);
		this.dgvDetalle.TabIndex = 3;
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
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle27.Format = "N2";
		dataGridViewCellStyle27.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle27;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 70;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle28.Format = "N2";
		dataGridViewCellStyle28.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle28;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Visible = false;
		this.preciounit.Width = 75;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle29.Format = "N2";
		dataGridViewCellStyle29.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle29;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
		this.importe.Width = 85;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle30.Format = "N2";
		dataGridViewCellStyle30.NullValue = null;
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle30;
		this.dscto1.HeaderText = "Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle31.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle31;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle32.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle32;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle33.Format = "N2";
		dataGridViewCellStyle33.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle33;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.montodscto.Width = 80;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle34.Format = "N2";
		dataGridViewCellStyle34.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle34;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.valorventa.Width = 85;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle35.Format = "N2";
		dataGridViewCellStyle35.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle35;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.igv.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle36.Format = "N2";
		dataGridViewCellStyle36.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle36;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Visible = false;
		this.precioventa.Width = 85;
		this.MiTipoImpuesto.DataPropertyName = "tipoimpuesto";
		this.MiTipoImpuesto.HeaderText = "TIPOIMPUESTO";
		this.MiTipoImpuesto.Name = "MiTipoImpuesto";
		this.MiTipoImpuesto.ReadOnly = true;
		this.MiTipoImpuesto.Visible = false;
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
		this.seleccionar.HeaderText = "Seleccionar";
		this.seleccionar.Name = "seleccionar";
		this.seleccionar.ReadOnly = true;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = SIGEFA.Properties.Resources.acep;
		this.btnGuardar.Location = new System.Drawing.Point(652, 369);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(50, 32);
		this.btnGuardar.TabIndex = 27;
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnSalir.Location = new System.Drawing.Point(708, 369);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(44, 32);
		this.btnSalir.TabIndex = 28;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(766, 414);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmVentadetalle";
		this.Text = "frmVentadetalle";
		base.Load += new System.EventHandler(frmVentadetalle_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
