using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.Editors;
using SIGEFA.Administradores;
using SIGEFA.Base.WinForm;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Formularios;
using SIGEFA.Librerias;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA;

public class mdi_Menu : Office2007RibbonForm
{
	private int childFormNumber = 0;

	public static bool Cambio = false;

	private clsAdmAcceso AdmAcce = new clsAdmAcceso();

	private bool FormEncontrado;

	private clsCaja aper = new clsCaja();

	private clsAdmAperturaCierre AdmAper = new clsAdmAperturaCierre();

	private clsAdmAlmacen admalm = new clsAdmAlmacen();

	private clsConexionMysql con = new clsConexionMysql();

	private clsAdmTipoCambio tc = new clsAdmTipoCambio();

	private clsAdmCotizacion admcot = new clsAdmCotizacion();

	private clsAdmParametro admParametro = new clsAdmParametro();

	private bool rpta;

	private List<ButtonItem> ListaControles = new List<ButtonItem>();

	public static double tc_hoy = 0.0;

	public int tcvalida;

	private clsTipoCambioSunat clstipoc = new clsTipoCambioSunat();

	private clsValidarSGE valida = new clsValidarSGE();

	private DateTime dia = default(DateTime).Date;

	private DateTime fechactual = default(DateTime).Date;

	private bool EstadoTC_BD = false;

	private DataTable tabla = new DataTable();

	public static clsParametros Configuracion = new clsParametros();

	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	public double ven;

	public double com;

	public double comp;

	public double vent;

	public bool EstadoTC = false;

	private bool mercadonegro;

	private bool eligedoc;

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private int tipocaja = 0;

	public bool bandcaja = false;

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsProducto pro = new clsProducto();

	public clsDetalleOrdenCompra dto = new clsDetalleOrdenCompra();

	private Atriform objfrm;

	private IContainer components = null;

	private RibbonControl ribbonControl1;

	private RibbonBar rbNegocio;

	private ButtonItem biProductos;

	private ButtonItem biClientes;

	private RibbonTabItem rtEntidades;

	private RibbonTabItem rtOperaciones;

	private ButtonItem biProveedores;

	private RibbonBar rbOperaciones;

	private ButtonItem biNotadeIngreso;

	private ButtonItem biNotadeSalida;

	private ButtonItem biTransferencia;

	private RibbonTabItem rtReportes;

	private RibbonTabItem rtAdministrador;

	private RibbonBar rbReportes;

	private ButtonItem biInventario;

	private ButtonItem biKardex;

	private RibbonBar rbConfigurar;

	private ButtonItem biEmpresa;

	private ButtonItem biAlmacen;

	private RibbonBar ribbonBar4;

	private ButtonItem biTablas;

	private ButtonItem biUnidades;

	private ButtonItem biFamilias;

	private ButtonItem biMarcas;

	private ButtonItem biTipoArticulo;

	private ButtonItem biCaracteristica;

	private ComboItem comboItem1;

	private ImageList imageList1;

	private StatusStrip statusStrip1;

	private TabStrip tabStrip1;

	private TabItem tabItem1;

	private ToolStripStatusLabel sEmpresa;

	private ToolStripStatusLabel sAlmacen;

	private ToolStripStatusLabel sUsuario;

	private ToolStripStatusLabel sIP;

	private ButtonItem biDocumentos;

	private RibbonTabItem rtVentas;

	private RibbonTabItem rtCompras;

	private ButtonItem biTransacciones;

	private RibbonBar rbVentas;

	private ButtonItem biVenta;

	private ButtonItem biVentaRapida;

	private RibbonBar rbCompras;

	private ButtonItem biPedidoCompra;

	private ButtonItem btnGuiaRemisionCompra;

	private ButtonItem biTipoCambio;

	private ButtonItem biAutorizado;

	private RibbonBar ribbonBar1;

	private ButtonItem biConsulta;

	private ButtonItem biModificar;

	private ButtonItem biEliminar;

	private ButtonItem biUsuarios;

	private ButtonItem biFormaPago;

	private RibbonBar ribbonBar2;

	private ButtonItem biBackup;

	private SaveFileDialog saveFileDialog1;

	private ButtonItem biImport;

	private OpenFileDialog openFileDialog1;

	private RibbonBar ribbonBar3;

	private ButtonItem biCobros;

	private ButtonItem biPagos;

	private ButtonItem biMetodoPago;

	private ButtonItem biGuia;

	private RibbonBar riReportesGeneral;

	private ButtonItem btnReporte;

	private ButtonItem biListasPrecios;

	private ButtonItem biVehiculosTransporte;

	private ButtonItem biConductores;

	private ButtonItem biEmpresasTransporte;

	private ButtonItem biZonas;

	private ButtonItem biDestaques;

	private RibbonBar ribbonBar5;

	private ButtonItem btArqueo;

	private ButtonItem biComision2;

	private ButtonItem biComisionVendedores;

	private ButtonItem biComisionVentas;

	private ButtonItem biGuias;

	private ButtonItem biAnular;

	private ButtonItem biNotaCredito;

	private ButtonItem ciNotasdeCredito;

	private ButtonItem biMuestraVentas;

	private RibbonBar ribbonBar6;

	private ButtonItem biCotizacion;

	private ButtonItem biCotizacionesVigentes;

	private ButtonItem biPedidoVenta;

	private ButtonItem biPedidosPendientes;

	private ButtonItem biCatalogo;

	private ButtonItem buttonItem16;

	private ButtonItem biBancos;

	private RibbonBar ribbonBar7;

	private ButtonItem bitermetro;

	private ButtonItem biBuscarGuia;

	private ButtonItem btnRequerimiento;

	private ButtonItem buttonItem9;

	private ButtonItem biOrdenCompra;

	private ButtonItem biOrdenesCompras;

	private ButtonItem biCompraOrden;

	private RibbonPanel ribbonPanel1;

	private RibbonPanel ribbonPanel2;

	private RibbonPanel ribbonPanel6;

	private RibbonPanel ribbonPanel3;

	private RibbonPanel ribbonPanel5;

	private ButtonItem biSucursal;

	private ButtonItem biTransferenciasPendientes;

	private ButtonItem biHistorialRequerimiento;

	private RibbonPanel ribbonPanel7;

	private RibbonTabItem rbCaja;

	private RibbonBar ribbonBar14;

	private RibbonBar ribbonBar9;

	private ImageList imageList2;

	private ButtonItem BiCajaVentasenEfectivo;

	private ButtonItem biHistorialFacturaciones;

	private ButtonItem biCotizacionesAprobadas;

	private ButtonItem biConsolidado;

	private ButtonItem biCuentasCorrientes;

	private ButtonItem biTarjetaPago;

	private ButtonItem biParametros;

	private ButtonItem biVigenciaCotizaciones;

	private ButtonItem biGuiasSinFacturar;

	private ButtonItem biStockAlmacenes;

	private ButtonItem biNotaCreditoCompra;

	private ButtonItem biNotasCreditoCompras;

	private ButtonItem btnNotaDebitoC;

	private ButtonItem biListadoND;

	private RibbonPanel ribbonPanel4;

	private ButtonItem biTipoEgresoCaja;

	private ButtonItem biNotaDebito;

	private ButtonItem ciNotasdeDebito;

	private ButtonItem btnMasivo;

	private ButtonItem biRotacionProducto;

	private ButtonItem BiAperturaCajaVentas;

	private ButtonItem biConsultorExterno;

	private ButtonItem buttonItem4;

	private ButtonItem buttonItem5;

	private ButtonItem biStockMinimos;

	private RibbonTabItem rbLibrosElectronicos;

	private RibbonPanel ribbonPanel10;

	private RibbonBar ribbonBar10;

	private ButtonItem biRegistroCompras;

	private ButtonItem biRegistroVentas;

	private RibbonTabItem Libros;

	private ButtonItem biLogout;

	private ButtonItem buttonItem1;

	private LabelItem liTipodeCambio;

	private ButtonItem biMovimientosCajaVentasEfectivo;

	private ButtonItem biCajaChica;

	private ButtonItem btnOtrosCajaChica;

	private RibbonBar ribbonBar8;

	private ButtonItem biIngresos;

	private ButtonItem buttonItem3;

	private ButtonItem buttonItem7;

	private ButtonItem buttonItem8;

	private ButtonItem BiAprobacionPago;

	private ButtonItem biPrestamosBancarios;

	private ButtonItem biListaPagPreBancarios;

	private ButtonItem biMovimientosBancarios;

	private ButtonItem BiRendiciones;

	private ButtonItem biAperturaCajachica;

	private RibbonBar ribbonBar11;

	private ButtonItem buttonItem6;

	private ButtonItem biVentasPendientesDespacho;

	private ButtonItem biListadoCompras;

	private ButtonItem biVentasporSeparacion;

	private ButtonItem biListaVentasSeparacion;

	private ButtonItem buttonItem2;

	private ButtonItem biUtilidad;

	private ButtonItem buttonItem11;

	private ButtonItem biEnviodeDocumentos;

	public ButtonItem biOrdenVenta;

	private ButtonItem biPedidoPendiente;

	private ButtonItem buttonItem10;

	private ButtonItem biVentasPorVendedor;

	private ButtonItem biUtilidadVentas;

	private ButtonItem biVentasPorCliente;

	private ButtonItem biStockProductos;

	private ButtonItem biKardexArticulos;

	private ButtonItem buttonItem12;

	private ButtonItem btn_generar_libro_electronico;

	private ButtonItem biTotalizadoVentasResumido;

	private ButtonItem biRpteDocumentosElectronicos;

	private SwitchButtonItem sbMercadoNegro;

	private SwitchButtonItem sbEligeDocumento;

	private LabelItem labelItem1;

	private LabelItem lineados;

	private ButtonItem biHistorialOrdenesVenta;

	private ButtonItem btnItemVenta;

	private ButtonItem biovpendientes;

	private ButtonItem biovhistorial;

	private ButtonItem buttonItem13;

	private ButtonItem ganacniaxcliente;

	private ButtonItem buttonItem14;

	private ToolStripStatusLabel sCaja;

	private Timer timer1;

	private ButtonItem biRegeneracion;

	private ButtonItem biReportesGenerales;

	private ButtonItem buttonItem15;

	private ButtonItem buttonItem18;

	private ButtonItem buttonItem19;

	private ButtonItem ventasdiarias;

	private ButtonItem resumendiarioventas;

	private ButtonItem buttonItem20;

	private ButtonItem buttonItem21;

	private ButtonItem btnResumen;

	private ButtonItem btnReporteVDiariaExcel;

	private RibbonBar ribbonBar12;

	private ButtonItem buttonItem24;

	private ButtonItem buttonItem22;

	private ButtonItem buttonItem23;

	private ButtonItem buttonItem25;

	private RibbonBar ribbonBar13;

	private ButtonItem btnPromedioProductosVendidos;

	private ButtonItem buttonItem27;

	private ButtonItem btnItemListadoGuiasCompra;

	private RibbonBar ribbonBar15;

	private ButtonItem buttonItem26;

	private ButtonItem btnActualizaProductosPlantilla;

	private ButtonItem btnItemNuevaPropuesta;

	private ButtonItem btnItemListadoPropuestas;

	private ButtonItem buttonItem17;

	private ButtonItem bitransferencias;

	private RibbonBar rbAreaRequerimientoAlmacen;

	private ButtonItem btnItemPlantillaReqAlmacen;

	private ButtonItem btnNuevaPRA;

	private ButtonItem btnListadoPRA;

	private ButtonItem btnItemPropuestaReqAlmacen;

	private ButtonItem btnNuevaPropuestaRA;

	private ButtonItem btnListadoPropuestaRA;

	private ButtonItem btnReqAlmacen;

	private ButtonItem btnListadoReqAlmacen;

	private ButtonItem btnNuevoReqAlmacen;

	private ButtonItem btnRequerimientosyTransferencias;

	private RibbonBar rbBDespacho;

	private ButtonItem btnIDespacho;

	private ButtonItem btnListadoProductosDespachar;

	private ButtonItem btnControlRequerimiento;

	private ButtonItem btnNuevoDespacho;

	private ButtonItem btnListadoDespachos;

	private ButtonItem btnTecnico;

	private ButtonItem btnCreaTecnico;

	private ButtonItem btnListaTecnicos;

	private ButtonItem btnDespacho;

	private ButtonItem btnConfiguraciones;

	private ButtonItem btnOrdenesDeVenta;

	private ButtonItem biTablaSistema;

	private ButtonItem mdiBtn_MaestroVentas;

	private ButtonItem mdiBtn_RptUtilidad;

	private ButtonItem btnIAnalisisDetalladoVenta;

	private ButtonItem btnIListadoReporteEntregas;

	private RibbonPanel ribbonPanel8;

	private RibbonTabItem rtiDesarrollador;

	private RibbonBar rbPrincipalDesarrollo;

	private ButtonItem btnIListadoMovimientosStock;

	private RibbonBar ribbonBar16;

	private ButtonItem buttonItem28;

	private ButtonItem buttonItem29;

	private ButtonItem buttonItem30;

	private ButtonItem buttonItem31;

	private ButtonItem ReporteAjustesInventario;

	private ButtonItem Detallado;

	private ButtonItem btndetalladoproductos;

	private ButtonItem biOrdenCompra_seteado;

	private ButtonItem btnparametrodescuento;

	private ButtonItem btnproductoscotizacion;

	private ButtonItem btnOrdenCCotizacion;

	private ButtonItem btnanalisiscotizaciones;

	private ButtonItem btnanalisisordencotizaciones;

	private ButtonItem btncombos;

	private ButtonItem btnguiasfacturacion;

	public static clsTipoCambio clstc { get; set; }

	public frmCajaVentasMovimientos formularioCajaMovimientos { get; set; }

	public frmVentas formularioListaVentas { get; set; }

	public frmRegistroProducto formularioRegProductos { get; set; }

	public frmCatalogo formularioCatalogo { get; set; }

	public frmProductos formularioProductoalmacen { get; set; }

	public frmOrdenesdeVenta formularioHistorialOV { get; set; }

	public frmPedidosPendientes formularioPedPendientes { get; set; }

	public frmGeneraVenta formularioGenVent { get; set; }

	public string estadoCaja { get; set; }

	public static int MontoTopeBoleta { get; set; }

	public void cargaconfiguracion()
	{
		Configuracion = AdmEmp.CargaConfiguracion();
	}

	private void GenerarLista()
	{
		ListaControles.Add(biVenta);
		ListaControles.Add(biMuestraVentas);
		ListaControles.Add(biVentasPendientesDespacho);
		ListaControles.Add(btnPromedioProductosVendidos);
		ListaControles.Add(biGuia);
		ListaControles.Add(biGuias);
		ListaControles.Add(biBuscarGuia);
		ListaControles.Add(biNotaCredito);
		ListaControles.Add(ciNotasdeCredito);
		ListaControles.Add(biNotaDebito);
		ListaControles.Add(ciNotasdeDebito);
		ListaControles.Add(biCobros);
		ListaControles.Add(biStockAlmacenes);
		ListaControles.Add(biPedidoCompra);
		ListaControles.Add(biListadoCompras);
		ListaControles.Add(biPagos);
		ListaControles.Add(biOrdenCompra);
		ListaControles.Add(biOrdenesCompras);
		ListaControles.Add(biCompraOrden);
		ListaControles.Add(biHistorialFacturaciones);
		ListaControles.Add(btnGuiaRemisionCompra);
		ListaControles.Add(biNotaCreditoCompra);
		ListaControles.Add(buttonItem18);
		ListaControles.Add(buttonItem27);
		ListaControles.Add(biNotadeIngreso);
		ListaControles.Add(biNotadeSalida);
		ListaControles.Add(biTransferencia);
		ListaControles.Add(biTransferenciasPendientes);
		ListaControles.Add(biConsulta);
		ListaControles.Add(biStockMinimos);
		ListaControles.Add(buttonItem24);
		ListaControles.Add(btnItemPlantillaReqAlmacen);
		ListaControles.Add(btnItemPropuestaReqAlmacen);
		ListaControles.Add(btnReqAlmacen);
		ListaControles.Add(btnIDespacho);
		ListaControles.Add(biProductos);
		ListaControles.Add(biCatalogo);
		ListaControles.Add(biClientes);
		ListaControles.Add(biProveedores);
		ListaControles.Add(btnTecnico);
		ListaControles.Add(biInventario);
		ListaControles.Add(biKardex);
		ListaControles.Add(btnReporte);
		ListaControles.Add(biVentasPorVendedor);
		ListaControles.Add(biUtilidadVentas);
		ListaControles.Add(biVentasPorCliente);
		ListaControles.Add(biStockProductos);
		ListaControles.Add(biKardexArticulos);
		ListaControles.Add(biTotalizadoVentasResumido);
		ListaControles.Add(buttonItem13);
		ListaControles.Add(ganacniaxcliente);
		ListaControles.Add(ventasdiarias);
		ListaControles.Add(buttonItem20);
		ListaControles.Add(mdiBtn_RptUtilidad);
		ListaControles.Add(btnIAnalisisDetalladoVenta);
		ListaControles.Add(ReporteAjustesInventario);
		ListaControles.Add(biUtilidad);
		ListaControles.Add(biReportesGenerales);
		ListaControles.Add(buttonItem15);
		ListaControles.Add(mdiBtn_MaestroVentas);
		ListaControles.Add(btnReporteVDiariaExcel);
		ListaControles.Add(buttonItem29);
		ListaControles.Add(biEmpresa);
		ListaControles.Add(biSucursal);
		ListaControles.Add(biAlmacen);
		ListaControles.Add(biUsuarios);
		ListaControles.Add(biTablas);
		ListaControles.Add(biUnidades);
		ListaControles.Add(biFamilias);
		ListaControles.Add(biMarcas);
		ListaControles.Add(biCaracteristica);
		ListaControles.Add(biDocumentos);
		ListaControles.Add(biTransacciones);
		ListaControles.Add(biTipoCambio);
		ListaControles.Add(biFormaPago);
		ListaControles.Add(biMetodoPago);
		ListaControles.Add(biListasPrecios);
		ListaControles.Add(biBancos);
		ListaControles.Add(biCuentasCorrientes);
		ListaControles.Add(biTarjetaPago);
		ListaControles.Add(biTipoEgresoCaja);
		ListaControles.Add(biTablaSistema);
		ListaControles.Add(btnConfiguraciones);
		ListaControles.Add(buttonItem31);
		ListaControles.Add(biTipoArticulo);
		ListaControles.Add(biVehiculosTransporte);
		ListaControles.Add(biConductores);
		ListaControles.Add(biEmpresasTransporte);
		ListaControles.Add(biZonas);
		ListaControles.Add(biDestaques);
		ListaControles.Add(biAutorizado);
		ListaControles.Add(biParametros);
		ListaControles.Add(biVigenciaCotizaciones);
		ListaControles.Add(biBackup);
		ListaControles.Add(biImport);
		ListaControles.Add(biEnviodeDocumentos);
		ListaControles.Add(biRegeneracion);
		ListaControles.Add(buttonItem28);
		ListaControles.Add(BiAperturaCajaVentas);
		ListaControles.Add(biMovimientosCajaVentasEfectivo);
		ListaControles.Add(biAperturaCajachica);
		ListaControles.Add(biCajaChica);
		ListaControles.Add(biMovimientosBancarios);
		ListaControles.Add(BiRendiciones);
		ListaControles.Add(biVentasPendientesDespacho);
		ListaControles.Add(biRegistroVentas);
		ListaControles.Add(buttonItem1);
	}

	public mdi_Menu()
	{
		InitializeComponent();
		GenerarLista();
	}

	private void ShowNewForm(object sender, EventArgs e)
	{
		Form childForm = new Form();
		childForm.MdiParent = this;
		childForm.Text = "Ventana " + childFormNumber++;
		childForm.Show();
	}

	private void OpenFile(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
		if (openFileDialog.ShowDialog(this) == DialogResult.OK)
		{
			string FileName = openFileDialog.FileName;
		}
	}

	private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
		if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
		{
			string FileName = saveFileDialog.FileName;
		}
	}

	private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CutToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.Cascade);
	}

	private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.TileVertical);
	}

	private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.TileHorizontal);
	}

	private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.ArrangeIcons);
	}

	private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Form[] mdiChildren = base.MdiChildren;
		foreach (Form childForm in mdiChildren)
		{
			childForm.Close();
		}
	}

	private void buttonItem21_Click(object sender, EventArgs e)
	{
		frmUsuarios form = new frmUsuarios();
		form.MdiParent = this;
		form.Show();
	}

	private void mdi_Menu_Load(object sender, EventArgs e)
	{
		tabStrip1.Hide();
		frmSeleccionarAlmacen frm = new frmSeleccionarAlmacen();
		frm.ShowDialog();
		Configuracion = frmLogin.Configuracion;
		frmLogin.AcesosUsuario = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		sUsuario.Text = "Usuario : " + frmLogin.sNombreUser + " " + frmLogin.sApellidoUSer;
		sEmpresa.Text = "Empresa : " + frmLogin.sEmpresa;
		sAlmacen.Text = "Almacen : " + frmLogin.sAlmacen;
		Text = frmLogin.sEmpresa + " - " + Text;
		sIP.Text = "IP : " + frmLogin.DirecIp;
		dia = DateTime.Now.Date;
		fechactual = DateTime.Now.Date;
		crearDirectorio();
		clstc = new clsTipoCambio();
		clsAdmPlantillaDeProductos admplan = new clsAdmPlantillaDeProductos();
		DataTable plant = admplan.listaPlantillasPorGenerar(0, frmLogin.iCodSucursal);
		if (plant != null)
		{
			if (plant.Rows.Count > 0)
			{
				listaplantillas form = new listaplantillas();
				form.tipoPlantillaReq = 1;
				form.MdiParent = this;
				form.listadoPorGenerar = true;
				form.Show();
			}
		}
		else
		{
			MessageBox.Show("Ocurrio un error al cargar plantillas pendientes de generar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (frmLogin.iNivelUser == 3 || frmLogin.iNivelUser == 1 || frmLogin.iNivelUser == 5)
		{
			ValidaTipoCambio();
			VerificaSaldoCaja();
		}
		muestraEstadocaja();
		mercadonegro = admParametro.consultarParametroVenta(1);
		sbMercadoNegro.Value = mercadonegro;
		sbEligeDocumento.Value = admParametro.consultarParametroVenta(2);
		objfrm = new Atriform();
		objfrm.codSucursal = frmLogin.iCodSucursal;
		objfrm.codAlmacen = frmLogin.iCodAlmacen;
		objfrm.codEmpresa = frmLogin.iCodEmpresa;
		objfrm.Usuario = frmLogin.sUsuario;
		objfrm.Empresa = frmLogin.sEmpresa;
		objfrm.Almacen = frmLogin.sAlmacen;
		objfrm.IP = frmLogin.DirecIp;
		base.Tag = objfrm;
		AplicacionBase.Aplicacion.Usuario.Login = frmLogin.sUsuario;
		AplicacionBase.Aplicacion.Usuario.WorkStation = frmLogin.DirecIp;
	}

	private void timer_Tick(object sender, EventArgs e)
	{
	}

	private void muestraEstadocaja()
	{
		tipocaja = 1;
		sCaja.Text = "Caja: ";
		clsCaja Caja2 = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, fechactual, tipocaja, frmLogin.iCodAlmacen, frmLogin.iCodUser);
		if (Caja2 == null)
		{
			estadoCaja = "Cerrada";
			sCaja.Text += estadoCaja;
		}
		else
		{
			estadoCaja = "Aperturada";
			sCaja.Text += estadoCaja;
		}
	}

	public void ValidaTipoCambio()
	{
		try
		{
			EstadoTC_BD = tc.VerificaTCFecha(dia);
			if (EstadoTC_BD)
			{
				tcvalida = 1;
				clstc = tc.CargaTipoCambio(dia, 2);
				tc_hoy = clstc.Venta;
				liTipodeCambio.Text = "Fecha TC:  " + clstc.Fecha.ToShortDateString() + "  Compra: " + clstc.Compra + " - Venta: " + clstc.Venta;
				return;
			}
			if (valida.AccesoInternet())
			{
				NuevoMetodoTipoCambio();
				if (liTipodeCambio.Text != "Tipo de Cambio" && Configuracion.Autoguardado)
				{
					tcvalida = 1;
					clstc = tc.CargaTipoCambio(dia, 2);
					tc_hoy = clstc.Venta;
					return;
				}
				MessageBox.Show("Ingresa Tipo de Cambio de Hoy");
				if (Application.OpenForms["frmTipoCambio"] != null)
				{
					Application.OpenForms["frmTipoCambio"].Activate();
					return;
				}
				frmTipoCambio form = new frmTipoCambio();
				form.btnNuevo_Click(null, null);
				form.ShowDialog();
				ValidaTipoCambio();
				return;
			}
			if (MessageBox.Show("Error al Registrar TIPO DE CAMBIO AUTOMATICO, por problemas en la red. Verifique su conexion a internet y de Click en Reintentar.", "CONEXION A INTERNET INESTABLE", MessageBoxButtons.RetryCancel, MessageBoxIcon.Asterisk).ToString() == "Retry")
			{
				ValidaTipoCambio();
			}
			else
			{
				MessageBox.Show("Ingresa Tipo de Cambio de Hoy", "Tipo De Cambio");
				if (Application.OpenForms["frmTipoCambio"] != null)
				{
					Application.OpenForms["frmTipoCambio"].Activate();
				}
				else
				{
					frmTipoCambio form2 = new frmTipoCambio();
					form2.btnNuevo_Click(new object(), new EventArgs());
					form2.ShowDialog();
				}
				ValidaTipoCambio();
			}
			ValidaTipoCambio();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void NuevoMetodoTipoCambio()
	{
		try
		{
			string url2 = "https://www.sunat.gob.pe/a/txt/tipoCambio.txt";
			WebClient wc = new WebClient();
			string t = wc.DownloadString(url2);
			string[] st = t.Split('|');
			bool auto = Configuracion.Autoguardado;
			liTipodeCambio.Text = "Fecha TC:  " + st[0] + " Compra: " + st[1] + " Venta: " + st[2];
			comp = Convert.ToDouble(st[1].Replace(",", "."));
			vent = Convert.ToDouble(st[2].Replace(",", "."));
			if (auto && liTipodeCambio.Text != "Tipo de Cambio")
			{
				clstc.ICodMoneda = 2;
				clstc.Compra = comp;
				clstc.Venta = vent;
				clstc.Fecha = DateTime.Now;
				clstc.CodUser = frmLogin.iCodUser;
				if (tc.insert(clstc))
				{
					EstadoTC = true;
					dia = DateTime.Now;
					ValidaTipoCambio();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error: mdi_Menu -> NuevoMetodoTipoCambio()");
		}
	}

	private void MetodoTipoCambio()
	{
		try
		{
			bool auto = Configuracion.Autoguardado;
			tabla = clstipoc.ConsultaTCSunat(dia);
			if (tabla != null && tabla.Rows.Count > 0)
			{
				string cadenabusqueda = "[Día] like '*" + dia.Date.Day + "*'";
				DataRow[] foundRows = tabla.Select(cadenabusqueda);
				if (foundRows.Length != 0)
				{
					foreach (DataRow r in tabla.Rows)
					{
						if (Convert.ToInt32(r[0]) == dia.Date.Day)
						{
							liTipodeCambio.Text = "Fecha TC:  " + dia.ToShortDateString() + " Compra: " + r[1].ToString() + " Venta: " + r[2].ToString();
							comp = Convert.ToDouble(r[1].ToString().Replace(",", "."));
							vent = Convert.ToDouble(r[2].ToString().Replace(",", "."));
						}
					}
					if (auto && liTipodeCambio.Text != "Tipo de Cambio")
					{
						clstc.ICodMoneda = 2;
						clstc.Compra = comp;
						clstc.Venta = vent;
						clstc.Fecha = DateTime.Now;
						clstc.CodUser = frmLogin.iCodUser;
						if (tc.insert(clstc))
						{
							EstadoTC = true;
							dia = DateTime.Now;
							ValidaTipoCambio();
						}
					}
				}
				else
				{
					dia = dia.AddDays(-1.0);
					MetodoTipoCambio();
				}
			}
			else
			{
				dia = dia.AddDays(-1.0);
				MetodoTipoCambio();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Problemas de Conexión : " + ex.Message, "Error en Hilo Tipo Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void crearDirectorio()
	{
		string XML = "C:\\XML";
		string CAPTCHA = "C:\\CAPTCHA";
		string DOCUMENTOS_ELECTRONICAS = "C:\\DOCUMENTOS-" + frmLogin.RUC;
		string FIRMA = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\CERTIFIK";
		string QR = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\CERTIFIK\\QR";
		string DOCUMENTOS_ENVIA = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOS ENVIAR";
		string NOTASDEBITO = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOS ENVIAR\\NOTAS DEBITO";
		string NOTASCREDITO = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOS ENVIAR\\NOTAS CREDITO";
		string FACTURAS = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOS ENVIAR\\FACTURAS";
		string BOLETAS = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOS ENVIAR\\BOLETAS";
		string GUIAS = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOS ENVIAR\\GUIAS";
		string CDR = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\CDR";
		string DOCUMENTOSBAJA = "C:\\DOCUMENTOS-" + frmLogin.RUC + "\\DOCUMENTOSBAJA";
		try
		{
			if (!Directory.Exists(XML))
			{
				Directory.CreateDirectory(XML);
			}
			if (!Directory.Exists(CAPTCHA))
			{
				Directory.CreateDirectory(CAPTCHA);
			}
			if (!Directory.Exists(DOCUMENTOS_ELECTRONICAS))
			{
				Directory.CreateDirectory(DOCUMENTOS_ELECTRONICAS);
			}
			if (!Directory.Exists(FIRMA))
			{
				Directory.CreateDirectory(FIRMA);
				if (!Directory.Exists(QR))
				{
					Directory.CreateDirectory(QR);
				}
			}
			if (!Directory.Exists(CDR))
			{
				Directory.CreateDirectory(CDR);
			}
			if (!Directory.Exists(DOCUMENTOSBAJA))
			{
				Directory.CreateDirectory(DOCUMENTOSBAJA);
			}
			if (!Directory.Exists(DOCUMENTOS_ENVIA))
			{
				Directory.CreateDirectory(DOCUMENTOS_ENVIA);
			}
			if (!Directory.Exists(NOTASDEBITO))
			{
				Directory.CreateDirectory(NOTASDEBITO);
			}
			if (!Directory.Exists(NOTASCREDITO))
			{
				Directory.CreateDirectory(NOTASCREDITO);
			}
			if (!Directory.Exists(FACTURAS))
			{
				Directory.CreateDirectory(FACTURAS);
			}
			if (!Directory.Exists(BOLETAS))
			{
				Directory.CreateDirectory(BOLETAS);
			}
			if (!Directory.Exists(GUIAS))
			{
				Directory.CreateDirectory(GUIAS);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error al crear fichero temporal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void buttonItem22_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmEmpresas"] != null)
		{
			Application.OpenForms["frmEmpresas"].Activate();
			return;
		}
		frmEmpresas form = new frmEmpresas();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void buttonItem1_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmProductos"] != null)
			{
				Application.OpenForms["frmProductos"].Activate();
				return;
			}
			frmProductos form = new frmProductos();
			form.MdiParent = this;
			form.tc_hoy = tc_hoy;
			form.Dock = DockStyle.Fill;
			form.Show();
		}
	}

	private void buttonItem26_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmUnidades"] != null)
		{
			Application.OpenForms["frmUnidades"].Activate();
			return;
		}
		frmUnidades form = new frmUnidades();
		form.ShowDialog();
	}

	private void buttonItem27_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmFamilias"] != null)
		{
			Application.OpenForms["frmFamilias"].Activate();
			return;
		}
		frmFamilias form = new frmFamilias();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem28_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmMarcas"] != null)
		{
			Application.OpenForms["frmMarcas"].Activate();
			return;
		}
		frmMarcas form = new frmMarcas();
		form.ShowDialog();
	}

	private void buttonItem29_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmTipoArticulos"] != null)
		{
			Application.OpenForms["frmTipoArticulos"].Activate();
			return;
		}
		frmTipoArticulos form = new frmTipoArticulos();
		form.ShowDialog();
	}

	private void buttonItem30_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCaracteristicas"] != null)
		{
			Application.OpenForms["frmCaracteristicas"].Activate();
			return;
		}
		frmCaracteristicas form = new frmCaracteristicas();
		form.ShowDialog();
	}

	private void mdi_Menu_FormClosed(object sender, FormClosedEventArgs e)
	{
		Application.Exit();
	}

	private void mdi_Menu_KeyDownAsync(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F11)
		{
			return;
		}
		frmSeleccionarAlmacen frm = new frmSeleccionarAlmacen();
		frm.ShowDialog();
		if (Cambio)
		{
			Form[] mdiChildren = base.MdiChildren;
			foreach (Form childForm in mdiChildren)
			{
				childForm.Close();
			}
			frmLogin.AcesosUsuario = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
			OtorgarAccesos(ListaControles);
			sAlmacen.Text = "Almacen : " + frmLogin.sAlmacen;
		}
		Cambio = false;
	}

	private void mdi_Menu_Shown(object sender, EventArgs e)
	{
		OtorgarAccesos(ListaControles);
		tabStrip1.Hide();
		if (tc.VerificaTCFecha(DateTime.Now))
		{
			tcvalida = 1;
			clstc = tc.CargaTipoCambio(DateTime.Now, 2);
			tc_hoy = clstc.Venta;
			return;
		}
		MessageBox.Show("Ingresa Tipo de Cambio de Hoy");
		if (Application.OpenForms["frmTipoCambio"] != null)
		{
			Application.OpenForms["frmTipoCambio"].Activate();
			return;
		}
		frmTipoCambio form = new frmTipoCambio();
		form.btnNuevo_Click(sender, e);
		form.ShowDialog();
	}

	private void buttonItem23_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmAlmacenes"] != null)
		{
			Application.OpenForms["frmAlmacenes"].Activate();
			return;
		}
		frmAlmacenes form = new frmAlmacenes();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void OtorgarAccesos(List<ButtonItem> Lista)
	{
		if (frmLogin.iNivelUser == 1)
		{
			return;
		}
		foreach (ButtonItem item in Lista)
		{
			if (frmLogin.AcesosUsuario.Contains(Convert.ToInt32(item.Tag)))
			{
				item.Enabled = true;
			}
			else
			{
				item.Enabled = false;
			}
		}
	}

	private void biNotadeIngreso_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotaIngreso"] != null)
			{
				Application.OpenForms["frmNotaIngreso"].Activate();
				return;
			}
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.MinimumSize = form.Size;
			form.MaximumSize = form.Size;
			form.Proceso = 1;
			form.Show();
		}
	}

	private void buttonItem25_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmUsuarios"] != null)
		{
			Application.OpenForms["frmUsuarios"].Activate();
			return;
		}
		frmUsuarios form = new frmUsuarios();
		form.MdiParent = this;
		form.Show();
	}

	private void biProveedores_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmProveedores"] != null)
		{
			Application.OpenForms["frmProveedores"].Activate();
			return;
		}
		frmProveedores form = new frmProveedores();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biClienteSimple_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmClientesSimples"] != null)
		{
			Application.OpenForms["frmClientesSimples"].Activate();
			return;
		}
		frmClientesSimples form = new frmClientesSimples();
		form.MdiParent = this;
		form.Tipo = 0;
		form.Show();
	}

	private void biClienteCompleto_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmClientesCompletos"] != null)
		{
			Application.OpenForms["frmClientesCompletos"].Activate();
			return;
		}
		frmClientesCompletos form = new frmClientesCompletos();
		form.MdiParent = this;
		form.Tipo = 1;
		form.Show();
	}

	private void biClienteEmpresa_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmClientesCorporativos"] != null)
		{
			Application.OpenForms["frmClientesCorporativos"].Activate();
			return;
		}
		frmClientesCorporativos form = new frmClientesCorporativos();
		form.MdiParent = this;
		form.Tipo = 2;
		form.Show();
	}

	private void biDocumentos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Activate();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.ShowDialog();
	}

	private void biTransacciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmTransacciones"] != null)
		{
			Application.OpenForms["frmTransacciones"].Activate();
			return;
		}
		frmTransacciones form = new frmTransacciones();
		form.ShowDialog();
	}

	private void biVenta_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		try
		{
			VerificaSaldoCaja();
			aper = AdmAper.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, frmLogin.iCodAlmacen, frmLogin.iCodUser);
			if (aper != null)
			{
				if (aper.Estado)
				{
					if (Application.OpenForms["frmGeneraVenta"] != null)
					{
						Application.OpenForms["frmGeneraVenta"].Activate();
						return;
					}
					frmGeneraVenta form1 = new frmGeneraVenta();
					AddOwnedForm(form1);
					form1.Proceso = 1;
					form1.Show();
				}
				else
				{
					MessageBox.Show("Ya ha realizado el cierre para el día de hoy", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error:  " + ex.Message);
		}
	}

	private void biTipoCambio_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmTipoCambio"] != null)
		{
			Application.OpenForms["frmTipoCambio"].Activate();
			return;
		}
		frmTipoCambio form = new frmTipoCambio();
		form.ShowDialog();
	}

	private void biAutorizado_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmAutorizado"] != null)
		{
			Application.OpenForms["frmAutorizado"].Activate();
			return;
		}
		frmAutorizado form = new frmAutorizado();
		form.ShowDialog();
	}

	private void BuscaFormulario(int Proceso)
	{
		foreach (Form formu in Application.OpenForms)
		{
			string typeform = formu.GetType().Name;
			if (typeform == "frmNotas")
			{
				frmNotas fo = (frmNotas)formu;
				if (fo.Proceso == Proceso)
				{
					fo.Activate();
					fo.WindowState = FormWindowState.Maximized;
					FormEncontrado = true;
					break;
				}
				FormEncontrado = false;
			}
		}
	}

	private void biConsulta_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		if (Application.OpenForms["frmNotas"] != null)
		{
			BuscaFormulario(3);
			if (!FormEncontrado)
			{
				frmNotas form1 = new frmNotas();
				form1.MdiParent = this;
				form1.Proceso = 3;
				form1.Text += " - CONSULTA";
				form1.Show();
			}
		}
		else
		{
			frmNotas form2 = new frmNotas();
			form2.MdiParent = this;
			form2.Proceso = 3;
			form2.Text += " - CONSULTA";
			form2.Show();
		}
	}

	private void biModificar_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		if (Application.OpenForms["frmNotas"] != null)
		{
			BuscaFormulario(2);
			if (!FormEncontrado)
			{
				frmNotas form = new frmNotas();
				form.MdiParent = this;
				form.Proceso = 2;
				form.Text += " - MODIFICAR";
				form.Show();
			}
		}
		else
		{
			frmNotas form2 = new frmNotas();
			form2.MdiParent = this;
			form2.Proceso = 2;
			form2.Text += " - MODIFICAR";
			form2.Show();
		}
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		if (Application.OpenForms["frmNotas"] != null)
		{
			BuscaFormulario(4);
			if (!FormEncontrado)
			{
				frmNotas form3 = new frmNotas();
				form3.MdiParent = this;
				form3.Proceso = 4;
				form3.Text += " - ELIMINAR";
				form3.Show();
			}
		}
		else
		{
			frmNotas form4 = new frmNotas();
			form4.MdiParent = this;
			form4.Proceso = 4;
			form4.Text += " - ELIMINAR";
			form4.Show();
		}
	}

	private void biNotadeSalida_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotaSalida"] != null)
			{
				Application.OpenForms["frmNotaSalida"].Activate();
				return;
			}
			frmNotaSalida form = new frmNotaSalida();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.MinimumSize = form.Size;
			form.MaximumSize = form.Size;
			form.Proceso = 1;
			form.Show();
		}
	}

	private void biParametros_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmParametros"] != null)
			{
				Application.OpenForms["frmParametros"].Activate();
				return;
			}
			frmParametros form = new frmParametros();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biUsuarios_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmUsuarios"] != null)
		{
			Application.OpenForms["frmUsuarios"].Activate();
			return;
		}
		frmUsuarios form = new frmUsuarios();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biPedidoCompra_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotaIngreso"] != null)
			{
				Application.OpenForms["frmNotaIngreso"].Activate();
				return;
			}
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = this;
			form.Text = "Compra Directa";
			form.Proceso = 1;
			form.txtTransaccion.Text = "FT";
			form.txtTransaccion.ReadOnly = true;
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			form.txtTransaccion_KeyPress(form.txtTransaccion, ee);
			form.txtCodProv.Focus();
			form.compra = 1;
			form.Show();
		}
	}

	private void biFormaPago_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmFormaPago"] != null)
		{
			Application.OpenForms["frmFormaPago"].Activate();
			return;
		}
		frmFormaPago form = new frmFormaPago();
		form.ShowDialog();
	}

	private void biPedidosPendientes_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPedidosPendientes"] != null)
		{
			Application.OpenForms["frmPedidosPendientes"].Activate();
			return;
		}
		frmPedidosPendientes form = new frmPedidosPendientes();
		form.MdiParent = this;
		form.Show();
	}

	private void biPedidoVenta_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmPedido"] != null)
			{
				Application.OpenForms["frmPedido"].Activate();
				return;
			}
			frmPedido form = new frmPedido();
			form.MdiParent = this;
			form.Proceso = 1;
			form.txtDocRef.Focus();
			form.Show();
		}
	}

	private void biInventario_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["ReporteInventario"] != null)
			{
				Application.OpenForms["ReporteInventario"].Activate();
				return;
			}
			frmReporteInventario form = new frmReporteInventario();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biBackup_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			saveFileDialog1.ShowDialog();
		}
	}

	private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		if (tcvalida == 1)
		{
			con.GeneraraBackup(saveFileDialog1.FileName);
		}
	}

	private void biImport_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			openFileDialog1.ShowDialog();
		}
	}

	private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
	{
		con.ImportarBackup(openFileDialog1.FileName);
	}

	private void biTransferencia_Click(object sender, EventArgs e)
	{
		bool flag = true;
		if (tcvalida == 1)
		{
			if (Application.OpenForms["F2TransferenciaEntreAlmacenes"] != null)
			{
				Application.OpenForms["F2TransferenciaEntreAlmacenes"].Activate();
				return;
			}
			F2TransferenciaEntreAlmacenes form = new F2TransferenciaEntreAlmacenes();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.MinimumSize = form.Size;
			form.MaximumSize = form.Size;
			form.Proceso = 1;
			form.txtDocRef.Focus();
			form.Show();
		}
	}

	private void biCobros_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmCobros"] != null)
			{
				Application.OpenForms["frmCobros"].Activate();
				return;
			}
			frmCobros form = new frmCobros();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.Show();
		}
	}

	private void biPagos_Click(object sender, EventArgs e)
	{
		frmPagos form1 = new frmPagos();
		form1.Close();
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmPagos"] != null)
			{
				Application.OpenForms["frmPagos"].Activate();
				return;
			}
			frmPagos form2 = new frmPagos();
			form2.MdiParent = this;
			form2.Dock = DockStyle.Fill;
			form2.Show();
		}
	}

	private void biMetodoPago_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmMetodoPago"] != null)
		{
			Application.OpenForms["frmMetodoPago"].Activate();
			return;
		}
		frmMetodoPago form = new frmMetodoPago();
		form.ShowDialog();
	}

	private void biCotizacion_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmGestionCotizacion"] != null)
			{
				Application.OpenForms["frmGestionCotizacion"].Activate();
				return;
			}
			frmGestionCotizacion form = new frmGestionCotizacion();
			form.MdiParent = this;
			form.Proceso = 1;
			form.txtDocRef.Text = "CT";
			form.txtDocRef.ReadOnly = true;
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			form.txtDocRef_KeyPress(form.txtDocRef, ee);
			form.txtCodCliente.Focus();
			form.Show();
		}
	}

	private void biCotizacionesVigentes_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmCotizacionesVigentes"] != null)
			{
				Application.OpenForms["frmCotizacionesVigentes"].Activate();
				return;
			}
			frmCotizacionesVigentes form = new frmCotizacionesVigentes();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
	}

	private void biClientes_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmClientesCompletos"] != null)
		{
			Application.OpenForms["frmClientesCompletos"].Activate();
			return;
		}
		frmClientesCompletos form = new frmClientesCompletos();
		form.MdiParent = this;
		form.Tipo = 1;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biListasPrecios_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmTipoPrecios"] != null)
			{
				Application.OpenForms["frmTipoPrecios"].Activate();
				return;
			}
			frmTipoPrecios form = new frmTipoPrecios();
			form.ShowDialog();
		}
	}

	private void biVehiculosTransporte_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmVehiculosTransporte"] != null)
		{
			Application.OpenForms["frmVehiculosTransporte"].Activate();
			return;
		}
		frmVehiculoTransporte form = new frmVehiculoTransporte();
		form.ShowDialog();
	}

	private void biConductores_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmConductores"] != null)
		{
			Application.OpenForms["frmConductores"].Activate();
			return;
		}
		frmConductores form = new frmConductores();
		form.ShowDialog();
	}

	private void biEmpresasTransporte_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmEmpresaTransporte"] != null)
		{
			Application.OpenForms["frmEmpresaTransporte"].Activate();
			return;
		}
		frmEmpresaTransporte form = new frmEmpresaTransporte();
		form.MdiParent = this;
		form.Show();
	}

	private void biGuia_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmGuiaFacturacion"] != null)
			{
				Application.OpenForms["frmGuiaFacturacion"].Activate();
				return;
			}
			frmGuiaFacturacion form1 = new frmGuiaFacturacion();
			form1.MdiParent = this;
			form1.Dock = DockStyle.Fill;
			form1.proceso = 1;
			form1.Show();
		}
	}

	private void biZonas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmZonas"] != null)
		{
			Application.OpenForms["frmZonas"].Activate();
			return;
		}
		frmZonas form = new frmZonas();
		form.ShowDialog();
	}

	private void biVendedores_Click(object sender, EventArgs e)
	{
	}

	private void biDestaques_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDestaques"] != null)
		{
			Application.OpenForms["frmDestaques"].Activate();
			return;
		}
		frmDestaques form = new frmDestaques();
		form.ShowDialog();
	}

	private void btArqueo_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmArqueos"] != null)
			{
				Application.OpenForms["frmArqueos"].Activate();
				return;
			}
			frmArqueos form = new frmArqueos();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biComisionVentas_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmCacularComision"] != null)
			{
				Application.OpenForms["frmCacularComision"].Activate();
				return;
			}
			frmCacularComision form = new frmCacularComision();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biComisionVentas_Click_1(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmComsionPorDocumento"] != null)
			{
				Application.OpenForms["frmComsionPorDocumento"].Activate();
				return;
			}
			frmComsionPorDocumento form = new frmComsionPorDocumento();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biGuias_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListaGuiasFacturacion"] != null)
		{
			Application.OpenForms["frmListaGuiasFacturacion"].Activate();
			return;
		}
		frmListaGuiasFacturacion frm = new frmListaGuiasFacturacion();
		frm.MdiParent = this;
		frm.Show();
	}

	private void biAnular_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		if (Application.OpenForms["frmNotas"] != null)
		{
			BuscaFormulario(5);
			if (!FormEncontrado)
			{
				frmNotas form3 = new frmNotas();
				form3.MdiParent = this;
				form3.Proceso = 5;
				form3.Text += " - ANULAR";
				form3.Show();
			}
		}
		else
		{
			frmNotas form4 = new frmNotas();
			form4.MdiParent = this;
			form4.Proceso = 5;
			form4.Text += " - ANULAR";
			form4.Show();
		}
	}

	public void biNotaCredito_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotadeCredito"] != null)
			{
				Application.OpenForms["frmNotadeCredito"].Activate();
				return;
			}
			frmNotadeCredito form = new frmNotadeCredito();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.Proceso = 1;
			form.Show();
		}
	}

	private void ciNotasdeCredito_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotasCredito"] != null)
			{
				Application.OpenForms["frmNotasCredito"].Activate();
				return;
			}
			frmNotasCredito form1 = new frmNotasCredito();
			form1.MdiParent = this;
			form1.Dock = DockStyle.Fill;
			form1.Proceso = 1;
			form1.Show();
		}
	}

	private void biMuestraVentas_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmVentas"] != null)
			{
				Application.OpenForms["frmVentas"].Activate();
				return;
			}
			frmVentas form = new frmVentas();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.Show();
		}
	}

	private void biCatalogo_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCatalogo"] != null)
		{
			Application.OpenForms["frmCatalogo"].Activate();
			return;
		}
		frmCatalogo form = new frmCatalogo();
		form.tc_hoy = tc_hoy;
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biKardex_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			frmParamKardexArticulo form = new frmParamKardexArticulo();
			form.ShowDialog();
		}
	}

	private void biBancos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmBancos"] != null)
		{
			Application.OpenForms["frmBancos"].Activate();
			return;
		}
		frmBancos form = new frmBancos();
		form.ShowDialog();
	}

	private void tabStrip1_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
	{
		if (tabStrip1.SelectedTab != null)
		{
			Form ventana = (Form)tabStrip1.SelectedTab.AttachedControl;
			if (ventana.WindowState == FormWindowState.Maximized)
			{
				ventana.WindowState = FormWindowState.Maximized;
			}
		}
	}

	private void mdi_Menu_MdiChildActivate(object sender, EventArgs e)
	{
		if (base.ActiveMdiChild != null)
		{
			base.ActiveMdiChild.WindowState = FormWindowState.Maximized;
		}
	}

	private void buttonItem1_Click_1(object sender, EventArgs e)
	{
	}

	private void biBuscarGuia_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmBuscarGuias"] != null)
			{
				Application.OpenForms["frmBuscarGuias"].Activate();
				return;
			}
			frmBuscarGuias form = new frmBuscarGuias();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void btnRequerimiento_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmRequerimiento"] != null)
			{
				Application.OpenForms["frmRequerimiento"].Activate();
				return;
			}
			frmRequerimiento form = new frmRequerimiento();
			form.MdiParent = this;
			form.txtSerie.Focus();
			form.Procede = 10;
			form.Proceso = 1;
			form.Show();
		}
	}

	private void buttonItem9_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmRequerimientosVigentes"] != null)
			{
				Application.OpenForms["frmRequerimientosVigentes"].Activate();
				return;
			}
			frmRequerimientosVigentes form = new frmRequerimientosVigentes();
			form.MdiParent = this;
			form.tipo = 1;
			form.Show();
		}
	}

	public static frmOrdenCompra buscarFrmOC(string tipoFormulario, int codOC, int proceso)
	{
		frmOrdenCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmOrdenCompra)frm).CodOrdenCompra == codOC && ((frmOrdenCompra)frm).Proceso == proceso)
			{
				form = (frmOrdenCompra)frm;
				break;
			}
		}
		return form;
	}

	internal static frmReqAlmacen buscarFrmReqAlmacen(string tipoFormulario, int codReqAlmacen, int proceso)
	{
		frmReqAlmacen form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmReqAlmacen)frm).codRequerimientoAlmacen == codReqAlmacen && ((frmReqAlmacen)frm).Proceso == proceso)
			{
				form = (frmReqAlmacen)frm;
				break;
			}
		}
		return form;
	}

	private void biOrdenCompra_Click(object sender, EventArgs e)
	{
		frmOrdenCompra form = buscarFrmOC("frmOrdenCompra", 0, 1);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmOrdenCompra();
		form.MdiParent = this;
		form.FormBorderStyle = FormBorderStyle.None;
		form.Dock = DockStyle.Fill;
		form.Procede = 10;
		form.Proceso = 1;
		form.uno = 1;
		if (form.uno == 1)
		{
			dto.uno = 1;
		}
		else
		{
			dto.uno = 2;
		}
		form.consultacombo = 80;
		form.Show();
		form.WindowState = FormWindowState.Maximized;
	}

	private void biOrdenesCompras_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmOrdenesVigentes"] != null)
		{
			Application.OpenForms["frmOrdenesVigentes"].Activate();
			return;
		}
		frmOrdenesVigentes form = new frmOrdenesVigentes();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotaIngresoPorOrden"] != null)
			{
				Application.OpenForms["frmNotaIngresoPorOrden"].Activate();
				return;
			}
			frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
			form.MdiParent = this;
			form.txtOrdenCompra.Focus();
			form.label19.Visible = false;
			form.txtFlete.Visible = false;
			form.Proceso = 1;
			form.FormBorderStyle = FormBorderStyle.None;
			form.Dock = DockStyle.Fill;
			form.Show();
			form.WindowState = FormWindowState.Maximized;
		}
	}

	private void BiMoneda_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmMoneda"] != null)
			{
				Application.OpenForms["frmMoneda"].Activate();
				return;
			}
			frmMoneda form = new frmMoneda();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biNotasOrden_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		if (Application.OpenForms["frmNotasOrden"] != null)
		{
			BuscaFormulario(3);
			if (!FormEncontrado)
			{
				frmNotasOrden form1 = new frmNotasOrden();
				form1.MdiParent = this;
				form1.Proceso = 3;
				form1.Text += " - CONSULTA";
				form1.Show();
			}
		}
		else
		{
			frmNotasOrden form2 = new frmNotasOrden();
			form2.MdiParent = this;
			form2.Proceso = 3;
			form2.Text += " - CONSULTA";
			form2.Show();
		}
	}

	private void biSucursal_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmSucursales"] != null)
		{
			Application.OpenForms["frmSucursales"].Activate();
			return;
		}
		frmSucursales form = new frmSucursales();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biTransferenciasPendientes_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["F2TransferenciasPendientes"] != null)
			{
				Application.OpenForms["F2TransferenciasPendientes"].Activate();
				return;
			}
			F2TransferenciasPendientes form = new F2TransferenciasPendientes();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biHistorialRequerimiento_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmOrdenesVigentes"] != null)
			{
				Application.OpenForms["frmOrdenesVigentes"].Activate();
				return;
			}
			frmRequerimientosVigentes form = new frmRequerimientosVigentes();
			form.MdiParent = this;
			form.tipo = 2;
			form.Show();
		}
	}

	private void biIngresos_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmTesoreria"] != null)
			{
				Application.OpenForms["frmTesoreria"].Activate();
				return;
			}
			frmTesoreria form = new frmTesoreria();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biCajaChica_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		try
		{
			tipocaja = 2;
			aper = AdmAper.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, tipocaja, frmLogin.iCodAlmacen, frmLogin.iCodUser);
			if (aper != null)
			{
				if (aper.Estado)
				{
					if (Application.OpenForms["frmCajaChica"] != null)
					{
						Application.OpenForms["frmCajaChica"].Activate();
						return;
					}
					frmCajaChica form = new frmCajaChica();
					form.tipo = 2;
					form.MdiParent = this;
					form.Show();
				}
				else
				{
					MessageBox.Show("Ya ha realizado el cierre para el día de hoy", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error:  " + ex.Message);
		}
	}

	private void biHistorialFacturaciones_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmFacturacionesVigentes"] != null)
			{
				Application.OpenForms["frmFacturacionesVigentes"].Activate();
				return;
			}
			frmFacturacionesVigentes form = new frmFacturacionesVigentes();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biCotizacionesAprobadas_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmCotizacionesAprobadas"] != null)
			{
				Application.OpenForms["frmCotizacionesAprobadas"].Activate();
				return;
			}
			frmCotizacionesAprobadas form = new frmCotizacionesAprobadas();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biConsolidado_Click(object sender, EventArgs e)
	{
	}

	private void biCuentasCorrientes_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmCuentasCte"] != null)
		{
			Application.OpenForms["frmCuentasCte"].Activate();
			return;
		}
		frmCuentasCte form = new frmCuentasCte();
		form.MdiParent = this;
		form.Show();
	}

	private void biTarjetaPago_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmTarjetasPago"] != null)
			{
				Application.OpenForms["frmTarjetasPago"].Activate();
				return;
			}
			frmTarjetasPago form = new frmTarjetasPago();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biVigenciaCotizaciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmVigenciaCotizacion"] != null)
		{
			Application.OpenForms["frmVigenciaCotizacion"].Activate();
			return;
		}
		frmVigenciaCotizacion form = new frmVigenciaCotizacion();
		form.MdiParent = this;
		form.Show();
	}

	private void biGuiasSinFacturar_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotaOrdenAlmacen"] != null)
			{
				Application.OpenForms["frmNotaOrdenAlmacen"].Activate();
				return;
			}
			frmNotaOrdenAlmacen form = new frmNotaOrdenAlmacen();
			form.proceso = 1;
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biStockAlmacenes_Click(object sender, EventArgs e)
	{
		frmStockAlmacenes form = new frmStockAlmacenes();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biParametros_Click_1(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParametros"] != null)
		{
			Application.OpenForms["frmParametros"].Activate();
			return;
		}
		frmParametros form = new frmParametros();
		form.MdiParent = this;
		form.Show();
	}

	private void btnNotaDebitoC_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotadeDebitoCompra"] != null)
			{
				Application.OpenForms["frmNotadeDebitoCompra"].Activate();
				return;
			}
			frmNotadeDebitoCompra form = new frmNotadeDebitoCompra();
			form.Proceso = 1;
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biNotaCreditoCompra_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotadeCreditoCompra"] != null)
			{
				Application.OpenForms["frmNotadeCreditoCompra"].Activate();
				return;
			}
			frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
			form.Proceso = 1;
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biNotasCreditoCompras_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotasCreditoCompras"] != null)
			{
				Application.OpenForms["frmNotasCreditoCompras"].Activate();
				return;
			}
			frmNotasCreditoCompras form = new frmNotasCreditoCompras();
			form.Proceso = 1;
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biTipoEgresoCaja_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmGestionTipoEgreso"] != null)
			{
				Application.OpenForms["frmGestionTipoEgreso"].Activate();
				return;
			}
			frmGestionTipoEgreso form = new frmGestionTipoEgreso();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biNotaDebito_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotadeDebito"] != null)
			{
				Application.OpenForms["frmNotadeDebito"].Activate();
				return;
			}
			frmNotadeDebito form1 = new frmNotadeDebito();
			form1.MdiParent = this;
			form1.Proceso = 1;
			form1.Show();
		}
	}

	private void ciNotasdeDebito_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotasDebitoVentas"] != null)
			{
				Application.OpenForms["frmNotasDebitoVentas"].Activate();
				return;
			}
			frmNotasDebitoVentas form1 = new frmNotasDebitoVentas();
			form1.MdiParent = this;
			form1.Proceso = 1;
			form1.Show();
		}
	}

	private void buttonItem2_Click_1(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotasDebitoCompras"] != null)
			{
				Application.OpenForms["frmNotasDebitoCompras"].Activate();
				return;
			}
			frmNotasDebitoCompras form1 = new frmNotasDebitoCompras();
			form1.MdiParent = this;
			form1.Proceso = 1;
			form1.Show();
		}
	}

	private void btnMasivo_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmCancelarPagoMasivo"] != null)
			{
				Application.OpenForms["frmCancelarPagoMasivo"].Activate();
				return;
			}
			frmCancelarPagoMasivo form1 = new frmCancelarPagoMasivo();
			form1.MdiParent = this;
			form1.WindowState = FormWindowState.Normal;
			form1.tipo = 3;
			form1.Show();
		}
	}

	private void biRotacionProducto_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmRotacionProductos"] != null)
			{
				Application.OpenForms["frmRotacionProductos"].Activate();
				return;
			}
			frmRotacionProductos form1 = new frmRotacionProductos();
			form1.MdiParent = this;
			form1.Show();
		}
	}

	private void buttonItem3_Click_3(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmTesoreria"] != null)
			{
				Application.OpenForms["frmTesoreria"].Activate();
				return;
			}
			frmTesoreria form = new frmTesoreria();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void buttonItem4_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmRegistroChequeCaja"] != null)
			{
				Application.OpenForms["frmRegistroChequeCaja"].Activate();
				return;
			}
			frmRegistroChequeCaja form = new frmRegistroChequeCaja();
			form.Show();
		}
	}

	private void buttonItem5_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmTesoreriaAnuPag"] != null)
			{
				Application.OpenForms["frmTesoreriaAnuPag"].Activate();
				return;
			}
			frmTesoreriaAnuPag form = new frmTesoreriaAnuPag();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void buttonItem6_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmPagosDetraccion"] != null)
			{
				Application.OpenForms["frmPagosDetraccion"].Activate();
				return;
			}
			frmPagosDetraccion form = new frmPagosDetraccion();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void buttonItem1_Click_3(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmAprobacionPagos"] != null)
			{
				Application.OpenForms["frmAprobacionPagos"].Activate();
				return;
			}
			frmAprobacionPagos form = new frmAprobacionPagos();
			form.Dock = DockStyle.Fill;
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biConsultorExterno_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmConsultorExt"] != null)
			{
				Application.OpenForms["frmConsultorExt"].Activate();
				return;
			}
			frmConsultorExt form = new frmConsultorExt();
			form.MdiParent = this;
			form.Proceso = 1;
			form.txtDocRef.Focus();
			form.Show();
		}
	}

	private void buttonItem4_Click_1(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmEntregasConsultExt"] != null)
		{
			Application.OpenForms["frmEntregasConsultExt"].Activate();
			return;
		}
		frmEntregasConsultExt form = new frmEntregasConsultExt();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem5_Click_1(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmVenta"] != null)
			{
				Application.OpenForms["frmVenta"].Activate();
				return;
			}
			frmVenta form1 = new frmVenta();
			form1.MdiParent = this;
			form1.consultorext = true;
			form1.Proceso = 1;
			form1.Show();
		}
	}

	private void biStockMinimos_Click(object sender, EventArgs e)
	{
		frmProductosStockMin frma = new frmProductosStockMin();
		frma.codalmacen = frmLogin.iCodAlmacen;
		frma.ShowDialog();
	}

	private void BiRendiciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmConciliacionesBancarias"] != null)
		{
			Application.OpenForms["frmConciliacionesBancarias"].Activate();
			return;
		}
		frmConciliacionesBancarias form = new frmConciliacionesBancarias();
		form.Proceso = 1;
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem1_Click_4(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmPrestamosBancarios"] != null)
			{
				Application.OpenForms["frmPrestamosBancarios"].Activate();
				return;
			}
			frmPrestamosBancarios form = new frmPrestamosBancarios();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void buttonItem3_Click_4(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmPagosPresBancarios"] != null)
			{
				Application.OpenForms["frmPagosPresBancarios"].Activate();
				return;
			}
			frmPagosPresBancarios form = new frmPagosPresBancarios();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biRegistroCompras_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRegistroComprasLE"] != null)
		{
			Application.OpenForms["frmRegistroComprasLE"].Activate();
			return;
		}
		frmRegistroComprasLE form = new frmRegistroComprasLE();
		form.MdiParent = this;
		form.Show();
	}

	private void biLogout_Click(object sender, EventArgs e)
	{
		Hide();
		frmLogin frm = new frmLogin();
		tipocaja = 1;
		frm.Show();
	}

	private void buttonItem1_Click_5(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRegistroComprasLE"] != null)
		{
			Application.OpenForms["frmRegistroComprasLE"].Activate();
			return;
		}
		frmPlanContable form = new frmPlanContable();
		form.MdiParent = this;
		form.Show();
	}

	private void BiAperturaCajaVentas_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			tipocaja = 1;
			bandcaja = true;
			VerificaSaldoCaja();
		}
	}

	private void BiCajaVentasenEfectivo_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmListaCaja"] != null)
			{
				Application.OpenForms["frmListaCaja"].Activate();
				return;
			}
			frmListaCaja form = new frmListaCaja();
			form.Proceso = 1;
			form.ShowDialog();
		}
	}

	private void biMovimientosCajaVentasEfectivo_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		try
		{
			aper = AdmAper.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, tipocaja, frmLogin.iCodAlmacen, frmLogin.iCodUser);
			if (aper != null)
			{
				if (aper.Estado)
				{
					if (Application.OpenForms["frmCajaVentasMovimientos"] != null)
					{
						Application.OpenForms["frmCajaVentasMovimientos"].Activate();
						return;
					}
					frmCajaVentasMovimientos form = new frmCajaVentasMovimientos();
					form.MdiParent = this;
					form.Dock = DockStyle.Fill;
					form.Show();
				}
				else
				{
					MessageBox.Show("Ya ha realizado el cierre para el día de hoy", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error:  " + ex.Message);
		}
	}

	private void btnOtrosCajaChica_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmCajaChica"] != null)
			{
				Application.OpenForms["frmCajaChica"].Activate();
				return;
			}
			frmCajaChica form = new frmCajaChica();
			form.tipo = 2;
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biMovimientosBancarios_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmMovimientos"] != null)
			{
				Application.OpenForms["frmMovimientos"].Activate();
				return;
			}
			frmMovimientos form = new frmMovimientos();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void biAperturaCajachica_Click(object sender, EventArgs e)
	{
		tipocaja = 2;
		VerificaSaldoCajaChica();
	}

	private void VerificaSaldoCaja()
	{
		tipocaja = 1;
		Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, fechactual, tipocaja, frmLogin.iCodAlmacen, frmLogin.iCodUser);
		if (Caja == null)
		{
			Caja = AdmCaja.GetUltimaCajaVentas(frmLogin.iCodSucursal, tipocaja, frmLogin.iCodAlmacen);
			if (Caja != null)
			{
				if (!Caja.Estado)
				{
					switch (MessageBox.Show("Existe una Apertura Caja" + Environment.NewLine + "Desea darle Cierre", "Apertura Caja", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk))
					{
					case DialogResult.OK:
					{
						if (Application.OpenForms["frmCajaVentasMovimientos"] != null)
						{
							Application.OpenForms["frmCajaVentasMovimientos"].Activate();
							break;
						}
						frmCajaVentasMovimientos form2 = new frmCajaVentasMovimientos();
						form2.MdiParent = this;
						form2.dtpfecha1.Value = Caja.Fechaapertura;
						form2.Show();
						break;
					}
					case DialogResult.Cancel:
					{
						if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
						{
							Application.OpenForms["frmAperturaCajaDiaria"].Activate();
							break;
						}
						frmAperturaCajaDiaria form = new frmAperturaCajaDiaria();
						form.Proceso = 1;
						form.tipocaja = tipocaja;
						form.txtmonto.Text = "0.00";
						form.ShowDialog();
						break;
					}
					}
				}
				else if (Caja.Estado)
				{
					if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
					{
						Application.OpenForms["frmAperturaCajaDiaria"].Activate();
						return;
					}
					frmAperturaCajaDiaria form3 = new frmAperturaCajaDiaria();
					form3.Proceso = 1;
					form3.ShowDialog();
				}
				return;
			}
			Caja = AdmCaja.CargaCierreAnterior(frmLogin.iCodSucursal, tipocaja);
			if (Caja == null)
			{
				if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
				{
					Application.OpenForms["frmAperturaCajaDiaria"].Activate();
					return;
				}
				frmAperturaCajaDiaria form4 = new frmAperturaCajaDiaria();
				form4.Proceso = 1;
				form4.tipocaja = tipocaja;
				form4.txtmonto.Text = "0.00";
				form4.ShowDialog();
			}
			else if (Caja != null)
			{
				if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
				{
					Application.OpenForms["frmAperturaCajaDiaria"].Activate();
					return;
				}
				frmAperturaCajaDiaria form5 = new frmAperturaCajaDiaria();
				form5.Proceso = 1;
				form5.tipocaja = tipocaja;
				form5.txtmonto.Text = $"{Caja.Montocierre:#,##0.00}";
				form5.ShowDialog();
			}
		}
		else if (bandcaja)
		{
			frmAperturaCajaDiaria form6 = new frmAperturaCajaDiaria();
			form6.Proceso = 1;
			form6.ShowDialog();
		}
	}

	private void VerificaSaldoCajaChica()
	{
		tipocaja = 2;
		Caja = AdmCaja.ValidarAperturaDia(frmLogin.iCodSucursal, fechactual, tipocaja, frmLogin.iCodAlmacen, frmLogin.iCodUser);
		if (Caja == null)
		{
			Caja = AdmCaja.GetUltimaCajaVentas(frmLogin.iCodSucursal, tipocaja, frmLogin.iCodAlmacen);
			if (Caja != null)
			{
				if (!Caja.Estado)
				{
					DialogResult result = MessageBox.Show("Existe una Apertura de Caja Chica" + Environment.NewLine + "Desea darle Cierre", "Apertura Caja Chica", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
					if (result == DialogResult.OK)
					{
						if (Application.OpenForms["frmCajaChica"] != null)
						{
							Application.OpenForms["frmCajaChica"].Activate();
							return;
						}
						frmCajaChica form = new frmCajaChica();
						form.tipo = 2;
						form.dtpfecha1.Value = Caja.Fechaapertura;
						form.MdiParent = this;
						form.Show();
					}
				}
				else if (Caja.Estado)
				{
					if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
					{
						Application.OpenForms["frmAperturaCajaDiaria"].Activate();
						return;
					}
					frmAperturaCajaDiaria form2 = new frmAperturaCajaDiaria();
					form2.Proceso = 1;
					form2.ShowDialog();
				}
				return;
			}
			Caja = AdmCaja.CargaCierreAnterior(frmLogin.iCodSucursal, tipocaja);
			if (Caja == null)
			{
				if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
				{
					Application.OpenForms["frmAperturaCajaDiaria"].Activate();
					return;
				}
				frmAperturaCajaDiaria form3 = new frmAperturaCajaDiaria();
				form3.Proceso = 1;
				form3.tipocaja = tipocaja;
				form3.txtmonto.Text = "0.00";
				form3.ShowDialog();
			}
			else if (Caja != null)
			{
				if (Application.OpenForms["frmAperturaCajaDiaria"] != null)
				{
					Application.OpenForms["frmAperturaCajaDiaria"].Activate();
					return;
				}
				frmAperturaCajaDiaria form4 = new frmAperturaCajaDiaria();
				form4.Proceso = 1;
				form4.tipocaja = tipocaja;
				form4.txtmonto.Text = $"{Caja.Montocierre:#,##0.00}";
				form4.ShowDialog();
			}
		}
		else
		{
			DialogResult result2 = MessageBox.Show("Ya Existe una Apertura de Caja Chica", "Apertura Caja Chica", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
		}
	}

	private void buttonItem6_Click_1(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmImportarExcelN1"] != null)
			{
				Application.OpenForms["frmImportarExcelN1"].Activate();
				return;
			}
			frmImportarExcelN1 form = new frmImportarExcelN1();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void ribbonControl1_Click(object sender, EventArgs e)
	{
	}

	private void rtCompras_Click(object sender, EventArgs e)
	{
	}

	private void buttonItem10_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmVentasPendientesdeDespacho"] != null)
			{
				Application.OpenForms["frmVentasPendientesdeDespacho"].Activate();
				return;
			}
			frmVentasPendientesdeDespacho form = new frmVentasPendientesdeDespacho();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void buttonItem11_Click(object sender, EventArgs e)
	{
		try
		{
			frmVerCompras form = new frmVerCompras();
			form.MdiParent = this;
			form.Show();
		}
		catch (Exception)
		{
		}
	}

	private void buttonItem12_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmVentaSeparacionAr"] != null)
			{
				Application.OpenForms["frmVentaSeparacionAr"].Activate();
				return;
			}
			frmVentaSeparacionAr form = new frmVentaSeparacionAr();
			form.MdiParent = this;
			form.Proceso = 1;
			form.Show();
		}
		catch (Exception)
		{
		}
	}

	private void buttonItem13_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmVentasSeparacioVer"] != null)
			{
				Application.OpenForms["frmVentasSeparacioVer"].Activate();
				return;
			}
			frmVentasSeparacioVer form = new frmVentasSeparacioVer();
			form.MdiParent = this;
			form.Show();
		}
	}

	private void rbCaja_Click(object sender, EventArgs e)
	{
	}

	private void buttonItem2_Click_2(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoDeCajas"] != null)
		{
			Application.OpenForms["frmListadoDeCajas"].Activate();
			return;
		}
		frmListadoDeCajas form = new frmListadoDeCajas();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem12_Click_1(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmUtilidad"] != null)
		{
			Application.OpenForms["frmUtilidad"].Activate();
			return;
		}
		frmUtilidad form = new frmUtilidad();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem15_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPaciente"] != null)
		{
			Application.OpenForms["frmPaciente"].Activate();
			return;
		}
		frmPaciente form = new frmPaciente();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem18_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmHistoriaClinica"] != null)
		{
			Application.OpenForms["frmHistoriaClinica"].Activate();
			return;
		}
		frmHistoriaClinica form = new frmHistoriaClinica();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem19_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmEnvioSunat"] != null)
			{
				Application.OpenForms["frmEnvioSunat"].Activate();
				return;
			}
			frmEnvioSunat form = new frmEnvioSunat();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.Show();
		}
	}

	public void biOrdenVenta_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			frmOrdenVenta form = new frmOrdenVenta(this);
			AddOwnedForm(form);
			form.Proceso = 1;
			form.Show();
		}
	}

	private void biPedidoPendiente_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPedidosPendientes"] != null)
		{
			Application.OpenForms["frmPedidosPendientes"].Activate();
			return;
		}
		frmPedidosPendientes form = new frmPedidosPendientes();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biVentasPorVendedor_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParamVentxVendedor"] != null)
		{
			Application.OpenForms["frmParamVentxVendedor"].Activate();
			return;
		}
		frmParamVentxVendedor form = new frmParamVentxVendedor();
		form.MdiParent = this;
		form.Show();
	}

	private void biUtilidadVentas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["UtilidadVentas"] != null)
		{
			Application.OpenForms["UtilidadVentas"].Activate();
			return;
		}
		UtilidadVentas form = new UtilidadVentas();
		form.MdiParent = this;
		form.Show();
	}

	private void biVentasPorCliente_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParamVentxCliente2"] != null)
		{
			Application.OpenForms["frmParamVentxCliente2"].Activate();
			return;
		}
		frmParamVentxCliente2 form = new frmParamVentxCliente2();
		form.MdiParent = this;
		form.Show();
	}

	private void biVentaMensualPorProducto_Click(object sender, EventArgs e)
	{
	}

	private void biStockProductos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmReporteInventario"] != null)
		{
			Application.OpenForms["frmReporteInventario"].Activate();
			return;
		}
		frmReporteInventario form = new frmReporteInventario();
		form.MdiParent = this;
		form.Show();
	}

	private void biKardexArticulos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParamKardexArticulo"] != null)
		{
			Application.OpenForms["frmParamKardexArticulo"].Activate();
			return;
		}
		frmParamKardexArticulo form = new frmParamKardexArticulo();
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem12_Click_2(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		try
		{
			VerificaSaldoCaja();
			aper = AdmAper.ValidarAperturaDia(frmLogin.iCodSucursal, DateTime.Now.Date, 1, frmLogin.iCodAlmacen, frmLogin.iCodUser);
			if (aper != null)
			{
				if (aper.Estado)
				{
					if (Application.OpenForms["frmVenta"] != null)
					{
						Application.OpenForms["frmVenta"].Activate();
						return;
					}
					frmVenta form1 = new frmVenta();
					form1.MdiParent = this;
					form1.Proceso = 1;
					form1.CodigoCaja = aper.Codcaja;
					form1.Show();
				}
				else
				{
					MessageBox.Show("Ya ha realizado el cierre para el día de hoy", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error:  " + ex.Message);
		}
	}

	private void btn_generar_libro_electronico_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmGestionNombreLE"] != null)
		{
			Application.OpenForms["frmGestionNombreLE"].Activate();
			return;
		}
		frmGestionNombreLE form = new frmGestionNombreLE();
		if (form.ShowDialog() == DialogResult.OK)
		{
			frmRegistroComprasLE frmGNLE = new frmRegistroComprasLE();
			frmGNLE.contenidoLibro = form.Contenido;
			frmGNLE.txtnombrelibro.Text = form.GetNombre();
			frmGNLE.tipoLibroRecibido = form.TipoLibro;
			frmGNLE.tipoRegistroRecibido = form.RegistroLibro;
			frmGNLE.Periodo = form.txtAnio.Text + form.txtMes.Text + form.txtDia.Text;
			frmGNLE.MesPeriodo = Convert.ToInt32(form.txtMes.Text);
			frmGNLE.btnGuardar.Enabled = true;
			frmGNLE.MdiParent = this;
			frmGNLE.Show();
		}
	}

	private void biTotalizadoVentasResumido_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParamVentaTotalizadoResumido"] != null)
		{
			Application.OpenForms["frmParamVentaTotalizadoResumido"].Activate();
			return;
		}
		frmParamVentaTotalizadoResumido form = new frmParamVentaTotalizadoResumido();
		form.MdiParent = this;
		form.Show();
	}

	private void biRpteDocumentosElectronicos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParamDocumentosEnviados"] != null)
		{
			Application.OpenForms["frmParamDocumentosEnviados"].Activate();
			return;
		}
		frmParamDocumentosEnviados form = new frmParamDocumentosEnviados();
		form.Show();
	}

	private void rtReportes_Click(object sender, EventArgs e)
	{
	}

	private void sbMercadoNegro_ValueChanged(object sender, EventArgs e)
	{
		string MN = "";
		if (sbMercadoNegro.Value)
		{
			MN = "true";
			sbEligeDocumento.Enabled = true;
			sbEligeDocumento.Visible = true;
			lineados.Visible = true;
		}
		else
		{
			MN = "false";
			sbEligeDocumento.Enabled = false;
			sbEligeDocumento.Visible = false;
			lineados.Visible = false;
		}
		if (!admParametro.actualizaParamentroVenta(MN))
		{
			MessageBox.Show("Ocurrió un error al aplicar el Filtro", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (Application.OpenForms["frmCajaVentasMovimientos"] != null)
		{
			formularioCajaMovimientos = (frmCajaVentasMovimientos)Application.OpenForms["frmCajaVentasMovimientos"];
		}
		if (Application.OpenForms["frmVentas"] != null)
		{
			formularioListaVentas = (frmVentas)Application.OpenForms["frmVentas"];
		}
		if (Application.OpenForms["frmCatalogo"] != null)
		{
			formularioCatalogo = (frmCatalogo)Application.OpenForms["frmCatalogo"];
		}
		if (Application.OpenForms["frmProductos"] != null)
		{
			formularioProductoalmacen = (frmProductos)Application.OpenForms["frmProductos"];
		}
		if (Application.OpenForms["frmOrdenesdeVenta"] != null)
		{
			formularioHistorialOV = (frmOrdenesdeVenta)Application.OpenForms["frmOrdenesdeVenta"];
		}
		if (Application.OpenForms["frmPedidosPendientes"] != null)
		{
			formularioPedPendientes = (frmPedidosPendientes)Application.OpenForms["frmPedidosPendientes"];
		}
		if (formularioCajaMovimientos != null)
		{
			formularioCajaMovimientos.biActualizar_Click(null, null);
		}
		if (formularioListaVentas != null)
		{
			formularioListaVentas.button1_Click(null, null);
			if (!(MN == "true"))
			{
			}
		}
		if (formularioRegProductos != null)
		{
			formularioRegProductos.ckbVentaTicket.Checked = sbMercadoNegro.Value;
		}
		if (formularioCatalogo != null)
		{
			formularioCatalogo.buttonItem4_Click(null, null);
		}
		if (formularioProductoalmacen != null)
		{
			formularioProductoalmacen.buttonItem4_Click(null, null);
		}
		if (formularioHistorialOV != null)
		{
			formularioHistorialOV.btnBusqueda_Click(null, null);
		}
		if (formularioPedPendientes != null)
		{
			formularioPedPendientes.button1_Click(null, null);
		}
	}

	private void biParametros_MouseHover(object sender, EventArgs e)
	{
		mercadonegro = admParametro.consultarParametroVenta(1);
		sbMercadoNegro.Value = mercadonegro;
	}

	private void biHistorialOrdenesVenta_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmOrdenesdeVenta"] != null)
		{
			Application.OpenForms["frmOrdenesdeVenta"].Activate();
			return;
		}
		frmOrdenesdeVenta form = new frmOrdenesdeVenta();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.MdiParent = this;
		form.Show();
	}

	private void sbEligeDocumento_ValueChanged(object sender, EventArgs e)
	{
		string ED = "";
		ED = ((!sbEligeDocumento.Value) ? "false" : "true");
		if (!admParametro.actualizaDocumentoVenta(ED))
		{
			MessageBox.Show("Ocurrió un error al aplicar el Filtro", "Filtro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		if (Application.OpenForms["frmGeneraVenta"] != null)
		{
			formularioGenVent = (frmGeneraVenta)Application.OpenForms["frmGeneraVenta"];
		}
		if (formularioGenVent != null)
		{
			formularioGenVent.chkTicket.Visible = true;
			formularioGenVent.chkBoleta.Checked = false;
			formularioGenVent.chkFactura.Checked = false;
			formularioGenVent.chkTicket.Checked = false;
		}
	}

	public void btnItemVenta_Click(object sender, EventArgs e)
	{
		try
		{
			frmVenta2019 form = new frmVenta2019(this);
			AddOwnedForm(form);
			form.MdiParent = this;
			ribbonControl1.Expanded = false;
			form.WindowState = FormWindowState.Maximized;
			form.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error:  " + ex.Message);
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.F9)
		{
			frmVenta2019 form = new frmVenta2019(this);
			AddOwnedForm(form);
			form.MdiParent = this;
			ribbonControl1.Expanded = false;
			form.WindowState = FormWindowState.Maximized;
			form.Show();
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void biovpendientes_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPedidosPendientes"] != null)
		{
			Application.OpenForms["frmPedidosPendientes"].Activate();
			return;
		}
		frmPedidosPendientes form = new frmPedidosPendientes();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void biovhistorial_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmOrdenesdeVenta"] != null)
		{
			Application.OpenForms["frmOrdenesdeVenta"].Activate();
			return;
		}
		frmOrdenesdeVenta form = new frmOrdenesdeVenta();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem13_Click_1(object sender, EventArgs e)
	{
		frmParamGananciaxArticulo2 form = new frmParamGananciaxArticulo2();
		form.ShowDialog();
	}

	private void ganacniaxcliente_Click(object sender, EventArgs e)
	{
		frmParamGananciaxCliente form = new frmParamGananciaxCliente();
		form.ShowDialog();
	}

	private void buttonItem14_Click(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmVentas"] != null)
			{
				Application.OpenForms["frmVentas"].Close();
			}
			frmVentas form = new frmVentas();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.Show();
		}
	}

	private void buttonItem16_Click(object sender, EventArgs e)
	{
	}

	private void biRegeneracion_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRegeneracionArchivos"] != null)
		{
			Application.OpenForms["frmRegeneracionArchivos"].Activate();
			return;
		}
		frmRegeneracionArchivos form = new frmRegeneracionArchivos();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.MdiParent = this;
		form.Show();
	}

	private void ribbonPanel3_Click(object sender, EventArgs e)
	{
	}

	private void biReportesGenerales_Click(object sender, EventArgs e)
	{
	}

	private void buttonItem15_Click_1(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmParamVentasMesArticulo"] != null)
		{
			Application.OpenForms["frmParamVentasMesArticulo"].Activate();
			return;
		}
		frmParamVentasMesArticulo form = new frmParamVentasMesArticulo();
		form.criterio = 0;
		form.MdiParent = this;
		form.Show();
	}

	private void buttonItem18_Click_1(object sender, EventArgs e)
	{
	}

	private void buttonItem18_Click_2(object sender, EventArgs e)
	{
	}

	private void buttonItem19_Click_1(object sender, EventArgs e)
	{
		listaplantillas form = buscarFrmListadoPlantilla("listaplantillas", 1);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new listaplantillas();
		form.tipoPlantillaReq = 1;
		form.MdiParent = this;
		form.Show();
	}

	private void ventasdiarias_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["reporteventasdiarias"] != null)
		{
			Application.OpenForms["reporteventasdiarias"].Activate();
			return;
		}
		reporteventasdiarias form = new reporteventasdiarias();
		form.MdiParent = this;
		form.Show();
	}

	private void resumendiarioventas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmresumenventas"] != null)
		{
			Application.OpenForms["frmresumenventas"].Activate();
			return;
		}
		frmresumenventas form = new frmresumenventas();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void buttonItem20_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmlistatranferencias"] != null)
		{
			Application.OpenForms["frmlistatranferencias"].Activate();
			return;
		}
		frmlistatranferencias form = new frmlistatranferencias();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void rtAdministrador_Click(object sender, EventArgs e)
	{
	}

	private void buttonItem21_Click_1(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frminventario"] != null)
			{
				Application.OpenForms["frminventario"].Activate();
				return;
			}
			frminventario form = new frminventario();
			form.MdiParent = this;
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.Show();
		}
	}

	private void rtVentas_Click(object sender, EventArgs e)
	{
	}

	private void btnResumen_Click(object sender, EventArgs e)
	{
		frmResumenDiario resumen = new frmResumenDiario();
		resumen.ShowDialog();
	}

	private void btnReporteVDiariaExcel_Click(object sender, EventArgs e)
	{
		reporteventasdiarias form = new reporteventasdiarias();
		form.ShowDialog();
	}

	private void buttonItem24_Click(object sender, EventArgs e)
	{
	}

	private void buttonItem23_Click_1(object sender, EventArgs e)
	{
		abrelistadoExistencias();
	}

	private void buttonItem22_Click_1(object sender, EventArgs e)
	{
		try
		{
			clsReportProductos rp = new clsReportProductos();
			int id_existencia = 0;
			if (rp.CrearExistencia(ref id_existencia))
			{
				rptExistencia frm = new rptExistencia();
				frm.codExist = id_existencia;
				frm.alma = 0;
				frm.ShowDialog();
			}
			else
			{
				MessageBox.Show("Ocurrio un Problema al Generar la Existencia", "mdi_Menu", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Reporte_Existencia [mdi_Menu]", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void abrelistadoExistencias()
	{
		if (Application.OpenForms["frmExistencias"] != null)
		{
			Application.OpenForms["frmExistencias"].Activate();
			return;
		}
		frmExistencias form = new frmExistencias();
		form.MdiParent = this;
		form.Dock = DockStyle.None;
		form.Show();
	}

	private void buttonItem25_Click_1(object sender, EventArgs e)
	{
		ListadoProductos form = buscarFrmPlantilla("ListadoProductos", 0, 1);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new ListadoProductos();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.tipoPlantillaReq = 1;
		form.Show();
	}

	private void btnPromedioProductosVendidos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPromedioProductosVendidos"] != null)
		{
			Application.OpenForms["frmPromedioProductosVendidos"].Activate();
			return;
		}
		frmPromedioProductosVendidos form = new frmPromedioProductosVendidos();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void buttonItem27_Click_1(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPropuestaDeOrdenCompra"] != null)
		{
			Application.OpenForms["frmPropuestaDeOrdenCompra"].Activate();
			return;
		}
		frmPropuestaDeOrdenCompra form = new frmPropuestaDeOrdenCompra();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.modoEdicion = false;
		form.proceso = 3;
		form.MdiParent = this;
		form.Text = "Nueva Propuesta de Orden de Compra";
		form.Show();
	}

	private void btnGuiaRemisionCompra_Click(object sender, EventArgs e)
	{
		frmGuiaRemisionCompra form = buscarFrmGRC("frmGuiaRemisionCompra", 0);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmGuiaRemisionCompra();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	public static frmGuiaRemisionCompra buscarFrmGRC(string tipoFormulario, int codGRC)
	{
		frmGuiaRemisionCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmGuiaRemisionCompra)frm).codGuiaRemisionCompraAEditar == codGRC)
			{
				form = (frmGuiaRemisionCompra)frm;
				break;
			}
		}
		return form;
	}

	private void btnItemListadoGuiasCompra_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoGuiaRemisionCompra"] != null)
		{
			Application.OpenForms["frmListadoGuiaRemisionCompra"].Activate();
			return;
		}
		frmListadoGuiaRemisionCompra form = new frmListadoGuiaRemisionCompra();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void btnActualizaProductosPlantilla_Click(object sender, EventArgs e)
	{
		DialogResult rpta = MessageBox.Show("Seguro de actualizar los datos de Plantillas para Compras", "Aviso de Ejecucion de Procedimieno Automantico", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (rpta != DialogResult.Yes)
		{
			return;
		}
		try
		{
			clsAdmFormulario admform = new clsAdmFormulario();
			if (admform.ejecutarActualizacionDatosProductosDePlantillas())
			{
				MessageBox.Show("Los Datos fueron actualizados correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Ocurrio algo imprevisto al actualizar los datos", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ocurrio El Siguiente Error:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnItemNuevaPropuesta_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPropuestaDeOrdenCompra"] != null)
		{
			Application.OpenForms["frmPropuestaDeOrdenCompra"].Activate();
			return;
		}
		frmPropuestaDeOrdenCompra form = new frmPropuestaDeOrdenCompra();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.modoEdicion = false;
		form.proceso = 3;
		form.MdiParent = this;
		form.Text = "Nueva Propuesta de Orden de Compra";
		form.Show();
	}

	private void btnItemListadoPropuestas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoDePropuestasDeOrdenDeCompra"] != null)
		{
			Application.OpenForms["frmListadoDePropuestasDeOrdenDeCompra"].Activate();
			return;
		}
		frmListadoDePropuestasDeOrdenDeCompra form = new frmListadoDePropuestasDeOrdenDeCompra();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void bitransferencias_Click(object sender, EventArgs e)
	{
		if (tcvalida != 1)
		{
			return;
		}
		if (Application.OpenForms["FrmTransferenciaLista"] != null)
		{
			BuscaFormulario(3);
			if (!FormEncontrado)
			{
				FrmTransferenciaLista form1 = new FrmTransferenciaLista();
				form1.MdiParent = this;
				form1.Proceso = 3;
				form1.Text += " - CONSULTA";
				form1.Show();
			}
		}
		else
		{
			FrmTransferenciaLista form2 = new FrmTransferenciaLista();
			form2.MdiParent = this;
			form2.Proceso = 3;
			form2.Text += " - CONSULTA";
			form2.Show();
		}
	}

	private void buttonItem17_Click(object sender, EventArgs e)
	{
		frmStockAlmacenesPendiente form = new frmStockAlmacenesPendiente();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void btnNuevaPRA_Click(object sender, EventArgs e)
	{
		ListadoProductos form = buscarFrmPlantilla("ListadoProductos", 0);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new ListadoProductos();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.Show();
	}

	private void btnListadoPRA_Click(object sender, EventArgs e)
	{
		listaplantillas form = buscarFrmListadoPlantilla("listaplantillas", 0);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new listaplantillas();
		form.MdiParent = this;
		form.Show();
	}

	public static ListadoProductos buscarFrmPlantilla(string tipoFormulario, int codPlantilla, int tipoPlantilla = 0)
	{
		ListadoProductos form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((ListadoProductos)frm).codpla == codPlantilla && ((ListadoProductos)frm).tipoPlantillaReq == tipoPlantilla)
			{
				form = (ListadoProductos)frm;
				break;
			}
		}
		return form;
	}

	public static listaplantillas buscarFrmListadoPlantilla(string tipoFormulario, int tipoPlantilla)
	{
		listaplantillas form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((listaplantillas)frm).tipoPlantillaReq == tipoPlantilla)
			{
				form = (listaplantillas)frm;
				break;
			}
		}
		return form;
	}

	private void btnNuevaPropuestaRA_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPropuestaDeReqAlmacen"] != null)
		{
			Application.OpenForms["frmPropuestaDeReqAlmacen"].Activate();
			return;
		}
		frmPropuestaDeReqAlmacen form = new frmPropuestaDeReqAlmacen();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.modoEdicion = false;
		form.proceso = 3;
		form.MdiParent = this;
		form.Text = "Nueva Propuesta de Requerimiento de Almacen";
		form.Show();
	}

	private void btnListadoPropuestaRA_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoDePropuestasDeReqAlmacen"] != null)
		{
			Application.OpenForms["frmListadoDePropuestasDeReqAlmacen"].Activate();
			return;
		}
		frmListadoDePropuestasDeReqAlmacen form = new frmListadoDePropuestasDeReqAlmacen();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void btnReqAlmacen_Click(object sender, EventArgs e)
	{
	}

	private void btnNuevoReqAlmacen_Click(object sender, EventArgs e)
	{
		frmReqAlmacen form = new frmReqAlmacen();
		form.MdiParent = this;
		form.Show();
	}

	private void btnListadoReqAlmacen_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmLIstadoReqAlmacen"] != null)
		{
			Application.OpenForms["frmLIstadoReqAlmacen"].Activate();
			return;
		}
		frmLIstadoReqAlmacen form = new frmLIstadoReqAlmacen();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void btnRequerimientosyTransferencias_Click(object sender, EventArgs e)
	{
		frmListaRequerimientoCompleta frm = new frmListaRequerimientoCompleta();
		if (Application.OpenForms["frmListaRequerimientoCompleta"] != null)
		{
			Application.OpenForms["frmListaRequerimientoCompleta"].Activate();
			return;
		}
		frm.MdiParent = this;
		frm.Show();
	}

	private void btnIDespacho_Click(object sender, EventArgs e)
	{
	}

	private void btnListadoProductosDespachar_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListaProductoDespachar"] != null)
		{
			Application.OpenForms["frmListaProductoDespachar"].Activate();
			return;
		}
		frmListaProductoDespachar form = new frmListaProductoDespachar();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void btnControlRequerimiento_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmControlRequerimientosAlmacen"] != null)
		{
			Application.OpenForms["frmControlRequerimientosAlmacen"].Activate();
			return;
		}
		frmControlRequerimientosAlmacen form = new frmControlRequerimientosAlmacen();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	public static frmDespacho buscarFrmDespacho(string tipoFormulario, int Proceso)
	{
		frmDespacho form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmDespacho)frm).Proceso == Proceso)
			{
				form = (frmDespacho)frm;
				break;
			}
		}
		return form;
	}

	private void btnNuevoDespacho_Click(object sender, EventArgs e)
	{
		frmDespacho form = buscarFrmDespacho("frmDespacho", 3);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmDespacho();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Proceso = 3;
		form.Show();
	}

	private void btnListadoDespachos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoDespacho"] != null)
		{
			Application.OpenForms["frmListadoDespacho"].Activate();
			return;
		}
		frmListadoDespacho form = new frmListadoDespacho();
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	public static frmTecnico buscarFrmTecnico(string tipoFormulario, int Proceso)
	{
		frmTecnico form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmTecnico)frm).Proceso == Proceso)
			{
				form = (frmTecnico)frm;
				break;
			}
		}
		return form;
	}

	private void btnCreaTecnico_Click(object sender, EventArgs e)
	{
		frmTecnico form = buscarFrmTecnico("frmTecnico", 1);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmTecnico();
		form.Proceso = 1;
		form.ShowDialog();
	}

	private void btnListaTecnicos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoTecnico"] != null)
		{
			Application.OpenForms["frmListadoTecnico"].Activate();
			return;
		}
		frmListadoTecnico form = new frmListadoTecnico();
		form.Proceso = 1;
		form.MdiParent = this;
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
	}

	private void btnDespacho_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmConfiguracionDespacho"] != null)
		{
			Application.OpenForms["frmConfiguracionDespacho"].Activate();
			return;
		}
		frmConfiguracionDespacho form = new frmConfiguracionDespacho();
		form.MdiParent = this;
		form.Show();
		form.WindowState = FormWindowState.Normal;
		form.StartPosition = FormStartPosition.CenterParent;
	}

	private void btnOrdenesDeVenta_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmConfiguracionOrdenDeVenta"] != null)
		{
			Application.OpenForms["frmConfiguracionOrdenDeVenta"].Activate();
			return;
		}
		frmConfiguracionOrdenDeVenta form = new frmConfiguracionOrdenDeVenta();
		form.MdiParent = this;
		form.Show();
		form.WindowState = FormWindowState.Normal;
		form.StartPosition = FormStartPosition.CenterParent;
	}

	private void btnmnuMaestroVentas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["WFRptMaestroVentas"] != null)
		{
			Application.OpenForms["WFRptMaestroVentas"].Activate();
			return;
		}
		string wObj = "SIGEFA.Reports.WFRptMaestroVentas";
		string wDll = "SIGEFA.Reports.dll";
		Assembly objAsm = Assembly.LoadFrom(wDll);
		Form f = (Form)objAsm.CreateInstance(wObj);
		f.Tag = objfrm;
		f.MdiParent = this;
		f.Show();
	}

	private void biTablaSistema_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["WFTablas"] != null)
		{
			Application.OpenForms["WFTablas"].Activate();
			return;
		}
		string wObj = "SIGEFA.Base.Utilitarios.WFTablas";
		string wDll = "SIGEFA.Base.Utilitarios.dll";
		Assembly objAsm = Assembly.LoadFrom(wDll);
		Form f = (Form)objAsm.CreateInstance(wObj);
		f.Tag = objfrm;
		f.MdiParent = this;
		f.Show();
	}

	private void biTablas_Click(object sender, EventArgs e)
	{
	}

	private void mdiBtn_MaestroVentas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["WFRptMaestroVentas"] != null)
		{
			Application.OpenForms["WFRptMaestroVentas"].Activate();
			return;
		}
		string wObj = "SIGEFA.Reports.WFRptMaestroVentas";
		string wDll = "SIGEFA.Reports.dll";
		string sPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		string sPathOrign = sPath + "\\" + wDll;
		Assembly objAsm = Assembly.LoadFrom(sPathOrign);
		Form f = (Form)objAsm.CreateInstance(wObj);
		f.Tag = objfrm;
		f.MdiParent = this;
		f.Show();
	}

	private void mdiBtn_RptUtilidad_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["WFRptUtilidades"] != null)
		{
			Application.OpenForms["WFRptUtilidades"].Activate();
			return;
		}
		string wObj = "SIGEFA.Reports.WFRptUtilidades";
		string wDll = "SIGEFA.Reports.dll";
		string sPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		string sPathOrign = sPath + "\\" + wDll;
		Assembly objAsm = Assembly.LoadFrom(sPathOrign);
		Form f = (Form)objAsm.CreateInstance(wObj);
		f.Tag = objfrm;
		f.MdiParent = this;
		f.Show();
	}

	private void btniListadoEntregas_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoReporteEntrega"] != null)
		{
			Application.OpenForms["frmListadoReporteEntrega"].Activate();
			return;
		}
		frmListadoReporteEntrega frm3 = new frmListadoReporteEntrega();
		frm3.MdiParent = this;
		frm3.Show();
	}

	private void btnListadoMovimientos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListadoMovimientoSegunTiposStock"] != null)
		{
			Application.OpenForms["frmListadoMovimientoSegunTiposStock"].Activate();
			return;
		}
		frmListadoMovimientoSegunTiposStock frm1 = new frmListadoMovimientoSegunTiposStock();
		frm1.MdiParent = this;
		frm1.Show();
	}

	private void btnIAnalisisDetalladoVenta_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmAnalisisDetalladoDeVenta"] != null)
		{
			Application.OpenForms["frmAnalisisDetalladoDeVenta"].Activate();
			return;
		}
		frmAnalisisDetalladoDeVenta frm4 = new frmAnalisisDetalladoDeVenta();
		frm4.MdiParent = this;
		frm4.Show();
	}

	private void mdi_Menu_SizeChanged(object sender, EventArgs e)
	{
	}

	private void buttonItem28_Click_1(object sender, EventArgs e)
	{
		frmIngresosEgresos frm = new frmIngresosEgresos();
		frm.ShowDialog();
	}

	private void buttonItem29_Click_1(object sender, EventArgs e)
	{
		reporteventasdiarias2 form = new reporteventasdiarias2();
		form.ShowDialog();
	}

	private void buttonItem30_Click_1(object sender, EventArgs e)
	{
		if (tcvalida == 1)
		{
			if (Application.OpenForms["frmNotasCreditoAplicadas"] != null)
			{
				Application.OpenForms["frmNotasCreditoAplicadas"].Activate();
				return;
			}
			frmNotasCreditoAplicadas form1 = new frmNotasCreditoAplicadas();
			form1.MdiParent = this;
			form1.Dock = DockStyle.Fill;
			form1.Proceso = 1;
			form1.Show();
		}
	}

	private void buttonItem31_Click(object sender, EventArgs e)
	{
		frmParametrosAdicional frm = new frmParametrosAdicional();
		frm.ShowDialog();
	}

	private void ReporteAjustesInventario_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["FrmReporteAjustesInventario"] != null)
		{
			Application.OpenForms["FrmReporteAjustesInventario"].Activate();
			return;
		}
		FrmReporteAjustesInventario frm4 = new FrmReporteAjustesInventario();
		frm4.MdiParent = this;
		frm4.Show();
	}

	private void Detallado_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["FrmReporteDetalladoCompras"] != null)
		{
			Application.OpenForms["FrmReporteDetalladoCompras"].Activate();
			return;
		}
		FrmReporteDetalladoCompras frm4 = new FrmReporteDetalladoCompras();
		frm4.MdiParent = this;
		frm4.Show();
	}

	private void btndetalladoproductos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmRptDetalladoProductos"] != null)
		{
			Application.OpenForms["frmRptDetalladoProductos"].Activate();
			return;
		}
		frmRptDetalladoProductos frm4 = new frmRptDetalladoProductos();
		frm4.MdiParent = this;
		frm4.Show();
	}

	private void biOrdenCompra_seteado_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmOrdenesSeteados"] != null)
		{
			Application.OpenForms["frmOrdenesSeteados"].Activate();
		}
	}

	private void btnparametrodescuento_Click(object sender, EventArgs e)
	{
		frmParametrosDescuentos frm = new frmParametrosDescuentos();
		frm.ShowDialog();
	}

	private void btnproductoscotizacion_Click(object sender, EventArgs e)
	{
		frmProductosCotizacion frm = new frmProductosCotizacion();
		frm.MdiParent = this;
		frm.Show();
	}

	private void btnOrdenCCotizacion_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmOrdenesCompraCotizaciones"] != null)
		{
			Application.OpenForms["frmOrdenesCompraCotizaciones"].Activate();
			return;
		}
		frmOrdenesCompraCotizaciones frm4 = new frmOrdenesCompraCotizaciones();
		frm4.MdiParent = this;
		frm4.Show();
	}

	private void btnanalisiscotizaciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmReporteControlCotizaciones"] != null)
		{
			Application.OpenForms["frmReporteControlCotizaciones"].Activate();
			return;
		}
		frmReporteControlCotizaciones frm = new frmReporteControlCotizaciones();
		frm.MdiParent = this;
		frm.Show();
	}

	private void btnanalisisordencotizaciones_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmReporteControlOcCotizaciones"] != null)
		{
			Application.OpenForms["frmReporteControlOcCotizaciones"].Activate();
			return;
		}
		frmReporteControlOcCotizaciones frm = new frmReporteControlOcCotizaciones();
		frm.MdiParent = this;
		frm.Show();
	}

	private void btncombos_Click(object sender, EventArgs e)
	{
		frmListaCombosProductos frm = new frmListaCombosProductos();
		frm.MdiParent = this;
		frm.Show();
	}

	private void btnguiasfacturacion_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmListaGuiasFacturacion"] != null)
		{
			Application.OpenForms["frmListaGuiasFacturacion"].Activate();
			return;
		}
		frmListaGuiasFacturacion frm = new frmListaGuiasFacturacion();
		frm.MdiParent = this;
		frm.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.mdi_Menu));
		this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
		this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
		this.rbNegocio = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.biProductos = new DevComponents.DotNetBar.ButtonItem();
		this.biCatalogo = new DevComponents.DotNetBar.ButtonItem();
		this.btndetalladoproductos = new DevComponents.DotNetBar.ButtonItem();
		this.btnproductoscotizacion = new DevComponents.DotNetBar.ButtonItem();
		this.btncombos = new DevComponents.DotNetBar.ButtonItem();
		this.biClientes = new DevComponents.DotNetBar.ButtonItem();
		this.biProveedores = new DevComponents.DotNetBar.ButtonItem();
		this.btnTecnico = new DevComponents.DotNetBar.ButtonItem();
		this.btnCreaTecnico = new DevComponents.DotNetBar.ButtonItem();
		this.btnListaTecnicos = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
		this.rbBDespacho = new DevComponents.DotNetBar.RibbonBar();
		this.btnIDespacho = new DevComponents.DotNetBar.ButtonItem();
		this.btnNuevoDespacho = new DevComponents.DotNetBar.ButtonItem();
		this.btnListadoDespachos = new DevComponents.DotNetBar.ButtonItem();
		this.btnListadoProductosDespachar = new DevComponents.DotNetBar.ButtonItem();
		this.btnIListadoReporteEntregas = new DevComponents.DotNetBar.ButtonItem();
		this.rbAreaRequerimientoAlmacen = new DevComponents.DotNetBar.RibbonBar();
		this.btnItemPlantillaReqAlmacen = new DevComponents.DotNetBar.ButtonItem();
		this.btnNuevaPRA = new DevComponents.DotNetBar.ButtonItem();
		this.btnListadoPRA = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemPropuestaReqAlmacen = new DevComponents.DotNetBar.ButtonItem();
		this.btnNuevaPropuestaRA = new DevComponents.DotNetBar.ButtonItem();
		this.btnListadoPropuestaRA = new DevComponents.DotNetBar.ButtonItem();
		this.btnReqAlmacen = new DevComponents.DotNetBar.ButtonItem();
		this.btnNuevoReqAlmacen = new DevComponents.DotNetBar.ButtonItem();
		this.btnListadoReqAlmacen = new DevComponents.DotNetBar.ButtonItem();
		this.btnRequerimientosyTransferencias = new DevComponents.DotNetBar.ButtonItem();
		this.btnControlRequerimiento = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar12 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem24 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem22 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem23 = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar5 = new DevComponents.DotNetBar.RibbonBar();
		this.btArqueo = new DevComponents.DotNetBar.ButtonItem();
		this.biStockMinimos = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem21 = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.biConsulta = new DevComponents.DotNetBar.ButtonItem();
		this.biModificar = new DevComponents.DotNetBar.ButtonItem();
		this.biAnular = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.bitransferencias = new DevComponents.DotNetBar.ButtonItem();
		this.rbOperaciones = new DevComponents.DotNetBar.RibbonBar();
		this.biNotadeIngreso = new DevComponents.DotNetBar.ButtonItem();
		this.biNotadeSalida = new DevComponents.DotNetBar.ButtonItem();
		this.biTransferencia = new DevComponents.DotNetBar.ButtonItem();
		this.biTransferenciasPendientes = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel6 = new DevComponents.DotNetBar.RibbonPanel();
		this.rbCompras = new DevComponents.DotNetBar.RibbonBar();
		this.biPedidoCompra = new DevComponents.DotNetBar.ButtonItem();
		this.biListadoCompras = new DevComponents.DotNetBar.ButtonItem();
		this.Detallado = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
		this.btnGuiaRemisionCompra = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemListadoGuiasCompra = new DevComponents.DotNetBar.ButtonItem();
		this.biPagos = new DevComponents.DotNetBar.ButtonItem();
		this.btnRequerimiento = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialRequerimiento = new DevComponents.DotNetBar.ButtonItem();
		this.biConsolidado = new DevComponents.DotNetBar.ButtonItem();
		this.biOrdenCompra = new DevComponents.DotNetBar.ButtonItem();
		this.biOrdenesCompras = new DevComponents.DotNetBar.ButtonItem();
		this.biOrdenCompra_seteado = new DevComponents.DotNetBar.ButtonItem();
		this.biCompraOrden = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialFacturaciones = new DevComponents.DotNetBar.ButtonItem();
		this.biGuiasSinFacturar = new DevComponents.DotNetBar.ButtonItem();
		this.biNotaCreditoCompra = new DevComponents.DotNetBar.ButtonItem();
		this.biNotasCreditoCompras = new DevComponents.DotNetBar.ButtonItem();
		this.btnNotaDebitoC = new DevComponents.DotNetBar.ButtonItem();
		this.biListadoND = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem18 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem25 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem19 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem27 = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemNuevaPropuesta = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemListadoPropuestas = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel5 = new DevComponents.DotNetBar.RibbonPanel();
		this.ribbonBar13 = new DevComponents.DotNetBar.RibbonBar();
		this.btnPromedioProductosVendidos = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar6 = new DevComponents.DotNetBar.RibbonBar();
		this.biPedidoVenta = new DevComponents.DotNetBar.ButtonItem();
		this.biPedidosPendientes = new DevComponents.DotNetBar.ButtonItem();
		this.biCotizacion = new DevComponents.DotNetBar.ButtonItem();
		this.biCotizacionesVigentes = new DevComponents.DotNetBar.ButtonItem();
		this.biCotizacionesAprobadas = new DevComponents.DotNetBar.ButtonItem();
		this.btnOrdenCCotizacion = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar3 = new DevComponents.DotNetBar.RibbonBar();
		this.biCobros = new DevComponents.DotNetBar.ButtonItem();
		this.btnMasivo = new DevComponents.DotNetBar.ButtonItem();
		this.biComision2 = new DevComponents.DotNetBar.ButtonItem();
		this.biComisionVendedores = new DevComponents.DotNetBar.ButtonItem();
		this.biComisionVentas = new DevComponents.DotNetBar.ButtonItem();
		this.biStockAlmacenes = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
		this.biVentasporSeparacion = new DevComponents.DotNetBar.ButtonItem();
		this.biListaVentasSeparacion = new DevComponents.DotNetBar.ButtonItem();
		this.rbVentas = new DevComponents.DotNetBar.RibbonBar();
		this.biVenta = new DevComponents.DotNetBar.ButtonItem();
		this.biMuestraVentas = new DevComponents.DotNetBar.ButtonItem();
		this.biVentasPendientesDespacho = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemVenta = new DevComponents.DotNetBar.ButtonItem();
		this.biovpendientes = new DevComponents.DotNetBar.ButtonItem();
		this.biovhistorial = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
		this.btnguiasfacturacion = new DevComponents.DotNetBar.ButtonItem();
		this.biVentaRapida = new DevComponents.DotNetBar.ButtonItem();
		this.biGuia = new DevComponents.DotNetBar.ButtonItem();
		this.biGuias = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscarGuia = new DevComponents.DotNetBar.ButtonItem();
		this.biNotaCredito = new DevComponents.DotNetBar.ButtonItem();
		this.ciNotasdeCredito = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem30 = new DevComponents.DotNetBar.ButtonItem();
		this.biNotaDebito = new DevComponents.DotNetBar.ButtonItem();
		this.ciNotasdeDebito = new DevComponents.DotNetBar.ButtonItem();
		this.biConsultorExterno = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
		this.biOrdenVenta = new DevComponents.DotNetBar.ButtonItem();
		this.biPedidoPendiente = new DevComponents.DotNetBar.ButtonItem();
		this.biHistorialOrdenesVenta = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel4 = new DevComponents.DotNetBar.RibbonPanel();
		this.ribbonBar16 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem28 = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar15 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem26 = new DevComponents.DotNetBar.ButtonItem();
		this.btnActualizaProductosPlantilla = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar11 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar7 = new DevComponents.DotNetBar.RibbonBar();
		this.biEnviodeDocumentos = new DevComponents.DotNetBar.ButtonItem();
		this.biRpteDocumentosElectronicos = new DevComponents.DotNetBar.ButtonItem();
		this.btnResumen = new DevComponents.DotNetBar.ButtonItem();
		this.biRegeneracion = new DevComponents.DotNetBar.ButtonItem();
		this.bitermetro = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.biBackup = new DevComponents.DotNetBar.ButtonItem();
		this.biImport = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar4 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.biTablas = new DevComponents.DotNetBar.ButtonItem();
		this.biUnidades = new DevComponents.DotNetBar.ButtonItem();
		this.biFamilias = new DevComponents.DotNetBar.ButtonItem();
		this.biMarcas = new DevComponents.DotNetBar.ButtonItem();
		this.biTipoArticulo = new DevComponents.DotNetBar.ButtonItem();
		this.biCaracteristica = new DevComponents.DotNetBar.ButtonItem();
		this.biDocumentos = new DevComponents.DotNetBar.ButtonItem();
		this.biTransacciones = new DevComponents.DotNetBar.ButtonItem();
		this.biTipoCambio = new DevComponents.DotNetBar.ButtonItem();
		this.biAutorizado = new DevComponents.DotNetBar.ButtonItem();
		this.biFormaPago = new DevComponents.DotNetBar.ButtonItem();
		this.biMetodoPago = new DevComponents.DotNetBar.ButtonItem();
		this.biListasPrecios = new DevComponents.DotNetBar.ButtonItem();
		this.biVehiculosTransporte = new DevComponents.DotNetBar.ButtonItem();
		this.biConductores = new DevComponents.DotNetBar.ButtonItem();
		this.biEmpresasTransporte = new DevComponents.DotNetBar.ButtonItem();
		this.biZonas = new DevComponents.DotNetBar.ButtonItem();
		this.biDestaques = new DevComponents.DotNetBar.ButtonItem();
		this.biBancos = new DevComponents.DotNetBar.ButtonItem();
		this.biCuentasCorrientes = new DevComponents.DotNetBar.ButtonItem();
		this.biTarjetaPago = new DevComponents.DotNetBar.ButtonItem();
		this.biTipoEgresoCaja = new DevComponents.DotNetBar.ButtonItem();
		this.biTablaSistema = new DevComponents.DotNetBar.ButtonItem();
		this.btnConfiguraciones = new DevComponents.DotNetBar.ButtonItem();
		this.btnDespacho = new DevComponents.DotNetBar.ButtonItem();
		this.btnOrdenesDeVenta = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem31 = new DevComponents.DotNetBar.ButtonItem();
		this.btnparametrodescuento = new DevComponents.DotNetBar.ButtonItem();
		this.biParametros = new DevComponents.DotNetBar.ButtonItem();
		this.biVigenciaCotizaciones = new DevComponents.DotNetBar.ButtonItem();
		this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
		this.sbMercadoNegro = new DevComponents.DotNetBar.SwitchButtonItem();
		this.lineados = new DevComponents.DotNetBar.LabelItem();
		this.sbEligeDocumento = new DevComponents.DotNetBar.SwitchButtonItem();
		this.rbConfigurar = new DevComponents.DotNetBar.RibbonBar();
		this.biEmpresa = new DevComponents.DotNetBar.ButtonItem();
		this.biSucursal = new DevComponents.DotNetBar.ButtonItem();
		this.biAlmacen = new DevComponents.DotNetBar.ButtonItem();
		this.biUsuarios = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel3 = new DevComponents.DotNetBar.RibbonPanel();
		this.riReportesGeneral = new DevComponents.DotNetBar.RibbonBar();
		this.btnReporte = new DevComponents.DotNetBar.ButtonItem();
		this.biVentasPorVendedor = new DevComponents.DotNetBar.ButtonItem();
		this.biUtilidadVentas = new DevComponents.DotNetBar.ButtonItem();
		this.biVentasPorCliente = new DevComponents.DotNetBar.ButtonItem();
		this.biStockProductos = new DevComponents.DotNetBar.ButtonItem();
		this.biKardexArticulos = new DevComponents.DotNetBar.ButtonItem();
		this.biTotalizadoVentasResumido = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
		this.ganacniaxcliente = new DevComponents.DotNetBar.ButtonItem();
		this.ventasdiarias = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem20 = new DevComponents.DotNetBar.ButtonItem();
		this.mdiBtn_RptUtilidad = new DevComponents.DotNetBar.ButtonItem();
		this.btnIAnalisisDetalladoVenta = new DevComponents.DotNetBar.ButtonItem();
		this.ReporteAjustesInventario = new DevComponents.DotNetBar.ButtonItem();
		this.btnanalisiscotizaciones = new DevComponents.DotNetBar.ButtonItem();
		this.btnanalisisordencotizaciones = new DevComponents.DotNetBar.ButtonItem();
		this.biUtilidad = new DevComponents.DotNetBar.ButtonItem();
		this.biReportesGenerales = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
		this.mdiBtn_MaestroVentas = new DevComponents.DotNetBar.ButtonItem();
		this.btnReporteVDiariaExcel = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem29 = new DevComponents.DotNetBar.ButtonItem();
		this.rbReportes = new DevComponents.DotNetBar.RibbonBar();
		this.biInventario = new DevComponents.DotNetBar.ButtonItem();
		this.biKardex = new DevComponents.DotNetBar.ButtonItem();
		this.biRotacionProducto = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel7 = new DevComponents.DotNetBar.RibbonPanel();
		this.ribbonBar8 = new DevComponents.DotNetBar.RibbonBar();
		this.biIngresos = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
		this.BiAprobacionPago = new DevComponents.DotNetBar.ButtonItem();
		this.biPrestamosBancarios = new DevComponents.DotNetBar.ButtonItem();
		this.biListaPagPreBancarios = new DevComponents.DotNetBar.ButtonItem();
		this.biMovimientosBancarios = new DevComponents.DotNetBar.ButtonItem();
		this.BiRendiciones = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar14 = new DevComponents.DotNetBar.RibbonBar();
		this.biAperturaCajachica = new DevComponents.DotNetBar.ButtonItem();
		this.biCajaChica = new DevComponents.DotNetBar.ButtonItem();
		this.btnOtrosCajaChica = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar9 = new DevComponents.DotNetBar.RibbonBar();
		this.BiAperturaCajaVentas = new DevComponents.DotNetBar.ButtonItem();
		this.BiCajaVentasenEfectivo = new DevComponents.DotNetBar.ButtonItem();
		this.biMovimientosCajaVentasEfectivo = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.resumendiarioventas = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel10 = new DevComponents.DotNetBar.RibbonPanel();
		this.ribbonBar10 = new DevComponents.DotNetBar.RibbonBar();
		this.biRegistroCompras = new DevComponents.DotNetBar.ButtonItem();
		this.biRegistroVentas = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.btn_generar_libro_electronico = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonPanel8 = new DevComponents.DotNetBar.RibbonPanel();
		this.rbPrincipalDesarrollo = new DevComponents.DotNetBar.RibbonBar();
		this.btnIListadoMovimientosStock = new DevComponents.DotNetBar.ButtonItem();
		this.biLogout = new DevComponents.DotNetBar.ButtonItem();
		this.rtVentas = new DevComponents.DotNetBar.RibbonTabItem();
		this.rtCompras = new DevComponents.DotNetBar.RibbonTabItem();
		this.rtOperaciones = new DevComponents.DotNetBar.RibbonTabItem();
		this.rtEntidades = new DevComponents.DotNetBar.RibbonTabItem();
		this.rtReportes = new DevComponents.DotNetBar.RibbonTabItem();
		this.rtAdministrador = new DevComponents.DotNetBar.RibbonTabItem();
		this.rbCaja = new DevComponents.DotNetBar.RibbonTabItem();
		this.Libros = new DevComponents.DotNetBar.RibbonTabItem();
		this.rtiDesarrollador = new DevComponents.DotNetBar.RibbonTabItem();
		this.rbLibrosElectronicos = new DevComponents.DotNetBar.RibbonTabItem();
		this.liTipodeCambio = new DevComponents.DotNetBar.LabelItem();
		this.comboItem1 = new DevComponents.Editors.ComboItem();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.sUsuario = new System.Windows.Forms.ToolStripStatusLabel();
		this.sEmpresa = new System.Windows.Forms.ToolStripStatusLabel();
		this.sAlmacen = new System.Windows.Forms.ToolStripStatusLabel();
		this.sIP = new System.Windows.Forms.ToolStripStatusLabel();
		this.sCaja = new System.Windows.Forms.ToolStripStatusLabel();
		this.tabStrip1 = new DevComponents.DotNetBar.TabStrip();
		this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.ribbonControl1.SuspendLayout();
		this.ribbonPanel1.SuspendLayout();
		this.ribbonPanel2.SuspendLayout();
		this.ribbonPanel6.SuspendLayout();
		this.ribbonPanel5.SuspendLayout();
		this.ribbonPanel4.SuspendLayout();
		this.ribbonPanel3.SuspendLayout();
		this.ribbonPanel7.SuspendLayout();
		this.ribbonPanel10.SuspendLayout();
		this.ribbonPanel8.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		base.SuspendLayout();
		this.ribbonControl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonControl1.CaptionVisible = true;
		this.ribbonControl1.Controls.Add(this.ribbonPanel6);
		this.ribbonControl1.Controls.Add(this.ribbonPanel1);
		this.ribbonControl1.Controls.Add(this.ribbonPanel2);
		this.ribbonControl1.Controls.Add(this.ribbonPanel5);
		this.ribbonControl1.Controls.Add(this.ribbonPanel4);
		this.ribbonControl1.Controls.Add(this.ribbonPanel3);
		this.ribbonControl1.Controls.Add(this.ribbonPanel7);
		this.ribbonControl1.Controls.Add(this.ribbonPanel10);
		this.ribbonControl1.Controls.Add(this.ribbonPanel8);
		this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonControl1.Expanded = false;
		this.ribbonControl1.Images = this.imageList1;
		this.ribbonControl1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[12]
		{
			this.biLogout, this.rtVentas, this.rtCompras, this.rtOperaciones, this.rtEntidades, this.rtReportes, this.rtAdministrador, this.rbCaja, this.Libros, this.rtiDesarrollador,
			this.rbLibrosElectronicos, this.liTipodeCambio
		});
		this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("Tahoma", 7f);
		this.ribbonControl1.Location = new System.Drawing.Point(5, 1);
		this.ribbonControl1.Name = "ribbonControl1";
		this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
		this.ribbonControl1.Size = new System.Drawing.Size(855, 172);
		this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonControl1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
		this.ribbonControl1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
		this.ribbonControl1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
		this.ribbonControl1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
		this.ribbonControl1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
		this.ribbonControl1.SystemText.QatDialogAddButton = "&Add >>";
		this.ribbonControl1.SystemText.QatDialogCancelButton = "Cancel";
		this.ribbonControl1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
		this.ribbonControl1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
		this.ribbonControl1.SystemText.QatDialogOkButton = "OK";
		this.ribbonControl1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
		this.ribbonControl1.SystemText.QatDialogRemoveButton = "&Remove";
		this.ribbonControl1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
		this.ribbonControl1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
		this.ribbonControl1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
		this.ribbonControl1.TabGroupHeight = 14;
		this.ribbonControl1.TabIndex = 4;
		this.ribbonControl1.Text = "ribbonControl1";
		this.ribbonControl1.Click += new System.EventHandler(ribbonControl1_Click);
		this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel1.Controls.Add(this.rbNegocio);
		this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel1.Name = "ribbonPanel1";
		this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel1.Size = new System.Drawing.Size(855, 117);
		this.ribbonPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel1.TabIndex = 1;
		this.ribbonPanel1.Visible = false;
		this.rbNegocio.AutoOverflowEnabled = true;
		this.rbNegocio.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbNegocio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbNegocio.ContainerControlProcessDialogKey = true;
		this.rbNegocio.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbNegocio.DragDropSupport = true;
		this.rbNegocio.Images = this.imageList1;
		this.rbNegocio.Items.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.biProductos, this.biClientes, this.biProveedores, this.btnTecnico });
		this.rbNegocio.Location = new System.Drawing.Point(3, 0);
		this.rbNegocio.Name = "rbNegocio";
		this.rbNegocio.Size = new System.Drawing.Size(277, 114);
		this.rbNegocio.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbNegocio.TabIndex = 0;
		this.rbNegocio.Text = "Negocio";
		this.rbNegocio.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbNegocio.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbNegocio.TitleVisible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "products.png");
		this.imageList1.Images.SetKeyName(1, "cliente.png");
		this.imageList1.Images.SetKeyName(2, "1211811759.png");
		this.imageList1.Images.SetKeyName(3, "user-icon-512.png");
		this.imageList1.Images.SetKeyName(4, "1325228163.jpg");
		this.imageList1.Images.SetKeyName(5, "cal3png.png");
		this.imageList1.Images.SetKeyName(6, "Get Document.ico");
		this.imageList1.Images.SetKeyName(7, "Send Document.ico");
		this.imageList1.Images.SetKeyName(8, "Transfer Document.ico");
		this.imageList1.Images.SetKeyName(9, "compras_a_proveedores_01.png");
		this.imageList1.Images.SetKeyName(10, "boxes copy_thumb.png");
		this.imageList1.Images.SetKeyName(11, "TarjetasKardex-1-PNG.png");
		this.imageList1.Images.SetKeyName(12, "a-propos-bleu-utilisateur-icone-7595-96.png");
		this.imageList1.Images.SetKeyName(13, "inventarios1.jpg");
		this.imageList1.Images.SetKeyName(14, "d9dc81882f20a4fb51dadd294dd1b4d5.png");
		this.imageList1.Images.SetKeyName(15, "Almacen.png");
		this.imageList1.Images.SetKeyName(16, "proveedor.png");
		this.imageList1.Images.SetKeyName(17, "company_256.png");
		this.imageList1.Images.SetKeyName(18, "iEngrenages.png");
		this.imageList1.Images.SetKeyName(19, "bag.png");
		this.imageList1.Images.SetKeyName(20, "venta.png");
		this.imageList1.Images.SetKeyName(21, "boleta-link.png");
		this.imageList1.Images.SetKeyName(22, "cotizacion.png");
		this.imageList1.Images.SetKeyName(23, "factura-icon.jpg");
		this.imageList1.Images.SetKeyName(24, "icon_shippingbox_withcalendar.png");
		this.imageList1.Images.SetKeyName(25, "images (1).jpg");
		this.imageList1.Images.SetKeyName(26, "pedido.png");
		this.imageList1.Images.SetKeyName(27, "pedidos.png");
		this.imageList1.Images.SetKeyName(28, "DocumentSearch.png");
		this.imageList1.Images.SetKeyName(29, "editar-una-pluma-para-escribir-icono-6827-96.png");
		this.imageList1.Images.SetKeyName(30, "Icono-Borrar-Anuncio.gif");
		this.imageList1.Images.SetKeyName(31, "lista-de-regalos.png");
		this.imageList1.Images.SetKeyName(32, "database-backup-cd-512.png");
		this.imageList1.Images.SetKeyName(33, "database-backup-icon-512.png");
		this.imageList1.Images.SetKeyName(34, "pagos.png");
		this.imageList1.Images.SetKeyName(35, "pagossol.png");
		this.imageList1.Images.SetKeyName(36, "lista-de-regalos.png");
		this.imageList1.Images.SetKeyName(37, "ICONO-INVENTARIO.jpg");
		this.imageList1.Images.SetKeyName(38, "Porcentaje (1).png");
		this.imageList1.Images.SetKeyName(39, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(40, "credit-note.png");
		this.imageList1.Images.SetKeyName(41, "Report.ico");
		this.imageList1.Images.SetKeyName(42, "stocks-icon.png");
		this.imageList1.Images.SetKeyName(43, "icon-requerimiento.ico");
		this.imageList1.Images.SetKeyName(44, "logog.png");
		this.imageList1.Images.SetKeyName(45, "ReporteProblemas.png");
		this.imageList1.Images.SetKeyName(46, "sucursales.png");
		this.imageList1.Images.SetKeyName(47, "stock.jpg");
		this.imageList1.Images.SetKeyName(48, "stock2.png");
		this.imageList1.Images.SetKeyName(49, "productoscarga.jpg");
		this.imageList1.Images.SetKeyName(50, "libro.png");
		this.imageList1.Images.SetKeyName(51, "logout1.png");
		this.imageList1.Images.SetKeyName(52, "bloggif_57aa8110e7163.jpeg");
		this.imageList1.Images.SetKeyName(53, "119aac0aa4ed9b90205078ecda0550af.png");
		this.biProductos.ImageIndex = 0;
		this.biProductos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biProductos.Name = "biProductos";
		this.biProductos.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.biCatalogo, this.btndetalladoproductos, this.btnproductoscotizacion, this.btncombos });
		this.biProductos.SubItemsExpandWidth = 14;
		this.biProductos.Tag = "42";
		this.biProductos.Text = "Productos";
		this.biProductos.Click += new System.EventHandler(buttonItem1_Click);
		this.biCatalogo.Name = "biCatalogo";
		this.biCatalogo.Tag = "43";
		this.biCatalogo.Text = "Catalogo";
		this.biCatalogo.Click += new System.EventHandler(biCatalogo_Click);
		this.btndetalladoproductos.Name = "btndetalladoproductos";
		this.btndetalladoproductos.Text = "Detallado Precios Productos";
		this.btndetalladoproductos.Click += new System.EventHandler(btndetalladoproductos_Click);
		this.btnproductoscotizacion.Name = "btnproductoscotizacion";
		this.btnproductoscotizacion.Text = "Productos Cotización";
		this.btnproductoscotizacion.Click += new System.EventHandler(btnproductoscotizacion_Click);
		this.btncombos.Name = "btncombos";
		this.btncombos.Text = "Combos";
		this.btncombos.Click += new System.EventHandler(btncombos_Click);
		this.biClientes.ImageIndex = 4;
		this.biClientes.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biClientes.Name = "biClientes";
		this.biClientes.SubItemsExpandWidth = 14;
		this.biClientes.Tag = "44";
		this.biClientes.Text = "Clientes";
		this.biClientes.Click += new System.EventHandler(biClientes_Click);
		this.biProveedores.ImageIndex = 5;
		this.biProveedores.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biProveedores.Name = "biProveedores";
		this.biProveedores.SubItemsExpandWidth = 14;
		this.biProveedores.Tag = "45";
		this.biProveedores.Text = "Proveedores";
		this.biProveedores.Click += new System.EventHandler(biProveedores_Click);
		this.btnTecnico.Image = SIGEFA.Properties.Resources.grupo;
		this.btnTecnico.ImageFixedSize = new System.Drawing.Size(52, 52);
		this.btnTecnico.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnTecnico.Name = "btnTecnico";
		this.btnTecnico.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.btnCreaTecnico, this.btnListaTecnicos });
		this.btnTecnico.SubItemsExpandWidth = 14;
		this.btnTecnico.Tag = "162";
		this.btnTecnico.Text = "Tecnicos";
		this.btnCreaTecnico.Name = "btnCreaTecnico";
		this.btnCreaTecnico.Text = "Crear Tecnico";
		this.btnCreaTecnico.Click += new System.EventHandler(btnCreaTecnico_Click);
		this.btnListaTecnicos.Name = "btnListaTecnicos";
		this.btnListaTecnicos.Text = "Listado Tecnicos";
		this.btnListaTecnicos.Click += new System.EventHandler(btnListaTecnicos_Click);
		this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel2.Controls.Add(this.rbBDespacho);
		this.ribbonPanel2.Controls.Add(this.rbAreaRequerimientoAlmacen);
		this.ribbonPanel2.Controls.Add(this.ribbonBar12);
		this.ribbonPanel2.Controls.Add(this.ribbonBar5);
		this.ribbonPanel2.Controls.Add(this.ribbonBar1);
		this.ribbonPanel2.Controls.Add(this.rbOperaciones);
		this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel2.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel2.Name = "ribbonPanel2";
		this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel2.Size = new System.Drawing.Size(871, 117);
		this.ribbonPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel2.TabIndex = 2;
		this.ribbonPanel2.Visible = false;
		this.rbBDespacho.AutoOverflowEnabled = true;
		this.rbBDespacho.BackColor = System.Drawing.Color.Transparent;
		this.rbBDespacho.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbBDespacho.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbBDespacho.ContainerControlProcessDialogKey = true;
		this.rbBDespacho.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbBDespacho.DragDropSupport = true;
		this.rbBDespacho.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.btnIDespacho });
		this.rbBDespacho.Location = new System.Drawing.Point(1050, 0);
		this.rbBDespacho.Name = "rbBDespacho";
		this.rbBDespacho.Size = new System.Drawing.Size(76, 114);
		this.rbBDespacho.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbBDespacho.TabIndex = 5;
		this.rbBDespacho.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbBDespacho.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbBDespacho.TitleVisible = false;
		this.btnIDespacho.Image = SIGEFA.Properties.Resources.generar_2;
		this.btnIDespacho.ImageIndex = 20;
		this.btnIDespacho.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
		this.btnIDespacho.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnIDespacho.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
		this.btnIDespacho.Name = "btnIDespacho";
		this.btnIDespacho.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.btnNuevoDespacho, this.btnListadoDespachos, this.btnListadoProductosDespachar, this.btnIListadoReporteEntregas });
		this.btnIDespacho.SubItemsExpandWidth = 14;
		this.btnIDespacho.Tag = "130";
		this.btnIDespacho.Text = "Despachos";
		this.btnIDespacho.Click += new System.EventHandler(btnIDespacho_Click);
		this.btnNuevoDespacho.Name = "btnNuevoDespacho";
		this.btnNuevoDespacho.Text = "Nuevo Despacho";
		this.btnNuevoDespacho.Visible = false;
		this.btnNuevoDespacho.Click += new System.EventHandler(btnNuevoDespacho_Click);
		this.btnListadoDespachos.Name = "btnListadoDespachos";
		this.btnListadoDespachos.Text = "Listado de Despachos";
		this.btnListadoDespachos.Click += new System.EventHandler(btnListadoDespachos_Click);
		this.btnListadoProductosDespachar.Name = "btnListadoProductosDespachar";
		this.btnListadoProductosDespachar.Text = "Listado de Productos a Despachar";
		this.btnListadoProductosDespachar.Click += new System.EventHandler(btnListadoProductosDespachar_Click);
		this.btnIListadoReporteEntregas.Name = "btnIListadoReporteEntregas";
		this.btnIListadoReporteEntregas.Text = "Listado De Entregas";
		this.btnIListadoReporteEntregas.Click += new System.EventHandler(btniListadoEntregas_Click);
		this.rbAreaRequerimientoAlmacen.AutoOverflowEnabled = true;
		this.rbAreaRequerimientoAlmacen.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbAreaRequerimientoAlmacen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbAreaRequerimientoAlmacen.ContainerControlProcessDialogKey = true;
		this.rbAreaRequerimientoAlmacen.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbAreaRequerimientoAlmacen.DragDropSupport = true;
		this.rbAreaRequerimientoAlmacen.Images = this.imageList1;
		this.rbAreaRequerimientoAlmacen.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.btnItemPlantillaReqAlmacen, this.btnItemPropuestaReqAlmacen, this.btnReqAlmacen });
		this.rbAreaRequerimientoAlmacen.Location = new System.Drawing.Point(792, 0);
		this.rbAreaRequerimientoAlmacen.Name = "rbAreaRequerimientoAlmacen";
		this.rbAreaRequerimientoAlmacen.Size = new System.Drawing.Size(258, 114);
		this.rbAreaRequerimientoAlmacen.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbAreaRequerimientoAlmacen.TabIndex = 4;
		this.rbAreaRequerimientoAlmacen.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbAreaRequerimientoAlmacen.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbAreaRequerimientoAlmacen.TitleVisible = false;
		this.btnItemPlantillaReqAlmacen.ImageIndex = 29;
		this.btnItemPlantillaReqAlmacen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnItemPlantillaReqAlmacen.Name = "btnItemPlantillaReqAlmacen";
		this.btnItemPlantillaReqAlmacen.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.btnNuevaPRA, this.btnListadoPRA });
		this.btnItemPlantillaReqAlmacen.SubItemsExpandWidth = 14;
		this.btnItemPlantillaReqAlmacen.Tag = "126";
		this.btnItemPlantillaReqAlmacen.Text = "Plantilla de Req. Almacen";
		this.btnNuevaPRA.Name = "btnNuevaPRA";
		this.btnNuevaPRA.Text = "Nueva Plantilla";
		this.btnNuevaPRA.Click += new System.EventHandler(btnNuevaPRA_Click);
		this.btnListadoPRA.Name = "btnListadoPRA";
		this.btnListadoPRA.Tag = "115";
		this.btnListadoPRA.Text = "Listado de Plantillas";
		this.btnListadoPRA.Click += new System.EventHandler(btnListadoPRA_Click);
		this.btnItemPropuestaReqAlmacen.ImageIndex = 31;
		this.btnItemPropuestaReqAlmacen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnItemPropuestaReqAlmacen.Name = "btnItemPropuestaReqAlmacen";
		this.btnItemPropuestaReqAlmacen.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.btnNuevaPropuestaRA, this.btnListadoPropuestaRA });
		this.btnItemPropuestaReqAlmacen.SubItemsExpandWidth = 14;
		this.btnItemPropuestaReqAlmacen.Tag = "133";
		this.btnItemPropuestaReqAlmacen.Text = "Propuestas de Req. Almacen";
		this.btnNuevaPropuestaRA.Name = "btnNuevaPropuestaRA";
		this.btnNuevaPropuestaRA.Text = "Nueva Propuesta";
		this.btnNuevaPropuestaRA.Visible = false;
		this.btnNuevaPropuestaRA.Click += new System.EventHandler(btnNuevaPropuestaRA_Click);
		this.btnListadoPropuestaRA.Name = "btnListadoPropuestaRA";
		this.btnListadoPropuestaRA.Text = "Listado de Propuestas";
		this.btnListadoPropuestaRA.Click += new System.EventHandler(btnListadoPropuestaRA_Click);
		this.btnReqAlmacen.ImageIndex = 45;
		this.btnReqAlmacen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnReqAlmacen.Name = "btnReqAlmacen";
		this.btnReqAlmacen.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.btnNuevoReqAlmacen, this.btnListadoReqAlmacen, this.btnRequerimientosyTransferencias, this.btnControlRequerimiento });
		this.btnReqAlmacen.SubItemsExpandWidth = 14;
		this.btnReqAlmacen.Tag = "133";
		this.btnReqAlmacen.Text = "Requerimiento de Almacen";
		this.btnReqAlmacen.Click += new System.EventHandler(btnReqAlmacen_Click);
		this.btnNuevoReqAlmacen.Name = "btnNuevoReqAlmacen";
		this.btnNuevoReqAlmacen.Text = "Nuevo Req. Almacen";
		this.btnNuevoReqAlmacen.Click += new System.EventHandler(btnNuevoReqAlmacen_Click);
		this.btnListadoReqAlmacen.Name = "btnListadoReqAlmacen";
		this.btnListadoReqAlmacen.Tag = "31";
		this.btnListadoReqAlmacen.Text = "Atencion Req Reposicion Stock";
		this.btnListadoReqAlmacen.Click += new System.EventHandler(btnListadoReqAlmacen_Click);
		this.btnRequerimientosyTransferencias.Name = "btnRequerimientosyTransferencias";
		this.btnRequerimientosyTransferencias.Text = "Listado Req. y Transf.";
		this.btnRequerimientosyTransferencias.Click += new System.EventHandler(btnRequerimientosyTransferencias_Click);
		this.btnControlRequerimiento.Name = "btnControlRequerimiento";
		this.btnControlRequerimiento.Text = "Control de Requerimientos";
		this.btnControlRequerimiento.Click += new System.EventHandler(btnControlRequerimiento_Click);
		this.ribbonBar12.AutoOverflowEnabled = true;
		this.ribbonBar12.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar12.ContainerControlProcessDialogKey = true;
		this.ribbonBar12.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar12.DragDropSupport = true;
		this.ribbonBar12.Images = this.imageList1;
		this.ribbonBar12.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem24 });
		this.ribbonBar12.Location = new System.Drawing.Point(719, 0);
		this.ribbonBar12.Name = "ribbonBar12";
		this.ribbonBar12.Size = new System.Drawing.Size(73, 114);
		this.ribbonBar12.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar12.TabIndex = 3;
		this.ribbonBar12.Text = "Arqueo";
		this.ribbonBar12.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar12.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar12.TitleVisible = false;
		this.buttonItem24.ImageIndex = 24;
		this.buttonItem24.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem24.Name = "buttonItem24";
		this.buttonItem24.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.buttonItem22, this.buttonItem23 });
		this.buttonItem24.SubItemsExpandWidth = 14;
		this.buttonItem24.Tag = "163";
		this.buttonItem24.Text = "Existencias";
		this.buttonItem24.Click += new System.EventHandler(buttonItem24_Click);
		this.buttonItem22.Name = "buttonItem22";
		this.buttonItem22.Text = "Generar Existencia Actual";
		this.buttonItem22.Click += new System.EventHandler(buttonItem22_Click_1);
		this.buttonItem23.Name = "buttonItem23";
		this.buttonItem23.Text = "Lista de Existencias";
		this.buttonItem23.Click += new System.EventHandler(buttonItem23_Click_1);
		this.ribbonBar5.AutoOverflowEnabled = true;
		this.ribbonBar5.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar5.ContainerControlProcessDialogKey = true;
		this.ribbonBar5.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar5.DragDropSupport = true;
		this.ribbonBar5.Images = this.imageList1;
		this.ribbonBar5.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.btArqueo, this.biStockMinimos, this.buttonItem21 });
		this.ribbonBar5.Location = new System.Drawing.Point(508, 0);
		this.ribbonBar5.Name = "ribbonBar5";
		this.ribbonBar5.Size = new System.Drawing.Size(211, 114);
		this.ribbonBar5.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar5.TabIndex = 2;
		this.ribbonBar5.Text = "Arqueo";
		this.ribbonBar5.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar5.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar5.TitleVisible = false;
		this.ribbonBar5.Visible = false;
		this.btArqueo.Enabled = false;
		this.btArqueo.ImageIndex = 36;
		this.btArqueo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btArqueo.Name = "btArqueo";
		this.btArqueo.SubItemsExpandWidth = 14;
		this.btArqueo.Tag = "41";
		this.btArqueo.Text = "Arqueo";
		this.btArqueo.Visible = false;
		this.btArqueo.Click += new System.EventHandler(btArqueo_Click);
		this.biStockMinimos.ImageIndex = 48;
		this.biStockMinimos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biStockMinimos.Name = "biStockMinimos";
		this.biStockMinimos.SubItemsExpandWidth = 14;
		this.biStockMinimos.Tag = "40";
		this.biStockMinimos.Text = "Reporte Stock Minimo";
		this.biStockMinimos.Visible = false;
		this.biStockMinimos.Click += new System.EventHandler(biStockMinimos_Click);
		this.buttonItem21.ImageIndex = 26;
		this.buttonItem21.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem21.Name = "buttonItem21";
		this.buttonItem21.SubItemsExpandWidth = 14;
		this.buttonItem21.Tag = "37";
		this.buttonItem21.Text = "Reporte de Inventario";
		this.buttonItem21.Click += new System.EventHandler(buttonItem21_Click_1);
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar1.DragDropSupport = true;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[5] { this.biConsulta, this.biModificar, this.biAnular, this.biEliminar, this.bitransferencias });
		this.ribbonBar1.Location = new System.Drawing.Point(193, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(315, 114);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 1;
		this.ribbonBar1.Text = "Operaciones";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.biConsulta.ImageIndex = 28;
		this.biConsulta.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biConsulta.Name = "biConsulta";
		this.biConsulta.SubItemsExpandWidth = 14;
		this.biConsulta.Tag = "37";
		this.biConsulta.Text = "Consulta";
		this.biConsulta.Click += new System.EventHandler(biConsulta_Click);
		this.biModificar.Enabled = false;
		this.biModificar.ImageIndex = 29;
		this.biModificar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biModificar.Name = "biModificar";
		this.biModificar.SubItemsExpandWidth = 14;
		this.biModificar.Tag = "38";
		this.biModificar.Text = "Modificar";
		this.biModificar.Visible = false;
		this.biModificar.Click += new System.EventHandler(biModificar_Click);
		this.biAnular.Enabled = false;
		this.biAnular.ImageIndex = 39;
		this.biAnular.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAnular.Name = "biAnular";
		this.biAnular.SubItemsExpandWidth = 14;
		this.biAnular.Tag = "39";
		this.biAnular.Text = "Anular";
		this.biAnular.Visible = false;
		this.biAnular.Click += new System.EventHandler(biAnular_Click);
		this.biEliminar.Enabled = false;
		this.biEliminar.ImageIndex = 30;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Tag = "40";
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Visible = false;
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.bitransferencias.ImageIndex = 0;
		this.bitransferencias.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.bitransferencias.Name = "bitransferencias";
		this.bitransferencias.SubItemsExpandWidth = 14;
		this.bitransferencias.Tag = "37";
		this.bitransferencias.Text = "Transferencia";
		this.bitransferencias.Visible = false;
		this.bitransferencias.Click += new System.EventHandler(bitransferencias_Click);
		this.rbOperaciones.AutoOverflowEnabled = true;
		this.rbOperaciones.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbOperaciones.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbOperaciones.ContainerControlProcessDialogKey = true;
		this.rbOperaciones.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbOperaciones.DragDropSupport = true;
		this.rbOperaciones.Images = this.imageList1;
		this.rbOperaciones.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biNotadeIngreso, this.biNotadeSalida, this.biTransferencia });
		this.rbOperaciones.Location = new System.Drawing.Point(3, 0);
		this.rbOperaciones.Name = "rbOperaciones";
		this.rbOperaciones.Size = new System.Drawing.Size(190, 114);
		this.rbOperaciones.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbOperaciones.TabIndex = 0;
		this.rbOperaciones.Text = "Transacciones";
		this.rbOperaciones.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbOperaciones.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbOperaciones.TitleVisible = false;
		this.biNotadeIngreso.ImageIndex = 6;
		this.biNotadeIngreso.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNotadeIngreso.Name = "biNotadeIngreso";
		this.biNotadeIngreso.SubItemsExpandWidth = 14;
		this.biNotadeIngreso.Tag = "34";
		this.biNotadeIngreso.Text = "Nota de Ingreso";
		this.biNotadeIngreso.Click += new System.EventHandler(biNotadeIngreso_Click);
		this.biNotadeSalida.ImageIndex = 7;
		this.biNotadeSalida.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNotadeSalida.Name = "biNotadeSalida";
		this.biNotadeSalida.SubItemsExpandWidth = 14;
		this.biNotadeSalida.Tag = "35";
		this.biNotadeSalida.Text = "Nota de Salida";
		this.biNotadeSalida.Click += new System.EventHandler(biNotadeSalida_Click);
		this.biTransferencia.ImageIndex = 8;
		this.biTransferencia.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biTransferencia.Name = "biTransferencia";
		this.biTransferencia.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biTransferenciasPendientes });
		this.biTransferencia.SubItemsExpandWidth = 14;
		this.biTransferencia.Tag = "36";
		this.biTransferencia.Text = "Tranferencia  Directa";
		this.biTransferencia.Click += new System.EventHandler(biTransferencia_Click);
		this.biTransferenciasPendientes.Name = "biTransferenciasPendientes";
		this.biTransferenciasPendientes.Tag = "116";
		this.biTransferenciasPendientes.Text = "Transferencias Pendientes";
		this.biTransferenciasPendientes.Click += new System.EventHandler(biTransferenciasPendientes_Click);
		this.ribbonPanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel6.Controls.Add(this.rbCompras);
		this.ribbonPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel6.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel6.Name = "ribbonPanel6";
		this.ribbonPanel6.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel6.Size = new System.Drawing.Size(855, 117);
		this.ribbonPanel6.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel6.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel6.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel6.TabIndex = 6;
		this.rbCompras.AutoOverflowEnabled = true;
		this.rbCompras.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbCompras.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbCompras.ContainerControlProcessDialogKey = true;
		this.rbCompras.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbCompras.DragDropSupport = true;
		this.rbCompras.Images = this.imageList1;
		this.rbCompras.Items.AddRange(new DevComponents.DotNetBar.BaseItem[12]
		{
			this.biPedidoCompra, this.buttonItem16, this.btnGuiaRemisionCompra, this.biPagos, this.btnRequerimiento, this.biConsolidado, this.biOrdenCompra, this.biCompraOrden, this.biNotaCreditoCompra, this.btnNotaDebitoC,
			this.buttonItem18, this.buttonItem27
		});
		this.rbCompras.Location = new System.Drawing.Point(3, 0);
		this.rbCompras.Name = "rbCompras";
		this.rbCompras.Size = new System.Drawing.Size(856, 114);
		this.rbCompras.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbCompras.TabIndex = 3;
		this.rbCompras.Text = "Documentos de venta";
		this.rbCompras.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbCompras.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbCompras.TitleVisible = false;
		this.biPedidoCompra.ImageIndex = 20;
		this.biPedidoCompra.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biPedidoCompra.Name = "biPedidoCompra";
		this.biPedidoCompra.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biListadoCompras, this.Detallado });
		this.biPedidoCompra.SubItemsExpandWidth = 14;
		this.biPedidoCompra.Tag = "23";
		this.biPedidoCompra.Text = "Compra Directa";
		this.biPedidoCompra.Click += new System.EventHandler(biPedidoCompra_Click);
		this.biListadoCompras.Name = "biListadoCompras";
		this.biListadoCompras.Tag = "115";
		this.biListadoCompras.Text = "Ver Compras";
		this.biListadoCompras.Click += new System.EventHandler(buttonItem11_Click);
		this.Detallado.Name = "Detallado";
		this.Detallado.Text = "Rpt. Detallado";
		this.Detallado.Click += new System.EventHandler(Detallado_Click);
		this.buttonItem16.Enabled = false;
		this.buttonItem16.ImageIndex = 26;
		this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem16.Name = "buttonItem16";
		this.buttonItem16.SubItemsExpandWidth = 14;
		this.buttonItem16.Text = "Pedidos";
		this.buttonItem16.Visible = false;
		this.buttonItem16.Click += new System.EventHandler(buttonItem16_Click);
		this.btnGuiaRemisionCompra.ImageIndex = 24;
		this.btnGuiaRemisionCompra.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnGuiaRemisionCompra.Name = "btnGuiaRemisionCompra";
		this.btnGuiaRemisionCompra.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.btnItemListadoGuiasCompra });
		this.btnGuiaRemisionCompra.SubItemsExpandWidth = 14;
		this.btnGuiaRemisionCompra.Tag = "24";
		this.btnGuiaRemisionCompra.Text = "Guia Remision Compra";
		this.btnGuiaRemisionCompra.Click += new System.EventHandler(btnGuiaRemisionCompra_Click);
		this.btnItemListadoGuiasCompra.Name = "btnItemListadoGuiasCompra";
		this.btnItemListadoGuiasCompra.Text = "Listado Guias Remision Compra";
		this.btnItemListadoGuiasCompra.Click += new System.EventHandler(btnItemListadoGuiasCompra_Click);
		this.biPagos.ImageIndex = 35;
		this.biPagos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biPagos.Name = "biPagos";
		this.biPagos.SubItemsExpandWidth = 14;
		this.biPagos.Tag = "25";
		this.biPagos.Text = "Pagos";
		this.biPagos.Click += new System.EventHandler(biPagos_Click);
		this.btnRequerimiento.Enabled = false;
		this.btnRequerimiento.ImageIndex = 44;
		this.btnRequerimiento.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnRequerimiento.Name = "btnRequerimiento";
		this.btnRequerimiento.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.buttonItem9, this.biHistorialRequerimiento });
		this.btnRequerimiento.SubItemsExpandWidth = 14;
		this.btnRequerimiento.Tag = "28";
		this.btnRequerimiento.Text = "Requerimiento";
		this.btnRequerimiento.Visible = false;
		this.btnRequerimiento.Click += new System.EventHandler(btnRequerimiento_Click);
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.Tag = "29";
		this.buttonItem9.Text = "Requerimientos Borradores";
		this.buttonItem9.Click += new System.EventHandler(buttonItem9_Click);
		this.biHistorialRequerimiento.Name = "biHistorialRequerimiento";
		this.biHistorialRequerimiento.Tag = "79";
		this.biHistorialRequerimiento.Text = "Historial Requerimientos";
		this.biHistorialRequerimiento.Click += new System.EventHandler(biHistorialRequerimiento_Click);
		this.biConsolidado.Enabled = false;
		this.biConsolidado.ImageIndex = 10;
		this.biConsolidado.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biConsolidado.Name = "biConsolidado";
		this.biConsolidado.SubItemsExpandWidth = 14;
		this.biConsolidado.Tag = "90";
		this.biConsolidado.Text = "Consolidado de Requerimientos";
		this.biConsolidado.Visible = false;
		this.biConsolidado.Click += new System.EventHandler(biConsolidado_Click);
		this.biOrdenCompra.ImageIndex = 45;
		this.biOrdenCompra.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biOrdenCompra.Name = "biOrdenCompra";
		this.biOrdenCompra.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biOrdenesCompras, this.biOrdenCompra_seteado });
		this.biOrdenCompra.SubItemsExpandWidth = 14;
		this.biOrdenCompra.Tag = "30";
		this.biOrdenCompra.Text = "Orden Compra";
		this.biOrdenCompra.Click += new System.EventHandler(biOrdenCompra_Click);
		this.biOrdenesCompras.Name = "biOrdenesCompras";
		this.biOrdenesCompras.Tag = "31";
		this.biOrdenesCompras.Text = "Ordenes Compra";
		this.biOrdenesCompras.Click += new System.EventHandler(biOrdenesCompras_Click);
		this.biOrdenCompra_seteado.Name = "biOrdenCompra_seteado";
		this.biOrdenCompra_seteado.Text = "Ordenes Compra Seteados";
		this.biOrdenCompra_seteado.Click += new System.EventHandler(biOrdenCompra_seteado_Click);
		this.biCompraOrden.ImageIndex = 19;
		this.biCompraOrden.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biCompraOrden.Name = "biCompraOrden";
		this.biCompraOrden.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biHistorialFacturaciones, this.biGuiasSinFacturar });
		this.biCompraOrden.SubItemsExpandWidth = 14;
		this.biCompraOrden.Tag = "32";
		this.biCompraOrden.Text = "Ingreso de Factura";
		this.biCompraOrden.Click += new System.EventHandler(buttonItem2_Click);
		this.biHistorialFacturaciones.Name = "biHistorialFacturaciones";
		this.biHistorialFacturaciones.Tag = "33";
		this.biHistorialFacturaciones.Text = "Historial Facturaciones";
		this.biHistorialFacturaciones.Click += new System.EventHandler(biHistorialFacturaciones_Click);
		this.biGuiasSinFacturar.Enabled = false;
		this.biGuiasSinFacturar.Name = "biGuiasSinFacturar";
		this.biGuiasSinFacturar.Tag = "91";
		this.biGuiasSinFacturar.Text = "Guias Sin Facturar";
		this.biGuiasSinFacturar.Visible = false;
		this.biGuiasSinFacturar.Click += new System.EventHandler(biGuiasSinFacturar_Click);
		this.biNotaCreditoCompra.ImageIndex = 40;
		this.biNotaCreditoCompra.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNotaCreditoCompra.Name = "biNotaCreditoCompra";
		this.biNotaCreditoCompra.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biNotasCreditoCompras });
		this.biNotaCreditoCompra.SubItemsExpandWidth = 14;
		this.biNotaCreditoCompra.Tag = "26";
		this.biNotaCreditoCompra.Text = "Notas de Crédito";
		this.biNotaCreditoCompra.Click += new System.EventHandler(biNotaCreditoCompra_Click);
		this.biNotasCreditoCompras.Name = "biNotasCreditoCompras";
		this.biNotasCreditoCompras.Tag = "27";
		this.biNotasCreditoCompras.Text = "Notas de Crédito";
		this.biNotasCreditoCompras.Click += new System.EventHandler(biNotasCreditoCompras_Click);
		this.btnNotaDebitoC.Enabled = false;
		this.btnNotaDebitoC.ImageIndex = 40;
		this.btnNotaDebitoC.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnNotaDebitoC.Name = "btnNotaDebitoC";
		this.btnNotaDebitoC.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biListadoND });
		this.btnNotaDebitoC.SubItemsExpandWidth = 14;
		this.btnNotaDebitoC.Tag = "93";
		this.btnNotaDebitoC.Text = "Nota de Débito";
		this.btnNotaDebitoC.Visible = false;
		this.btnNotaDebitoC.Click += new System.EventHandler(btnNotaDebitoC_Click);
		this.biListadoND.Name = "biListadoND";
		this.biListadoND.Tag = "94";
		this.biListadoND.Text = "Notas de Débito";
		this.biListadoND.Click += new System.EventHandler(buttonItem2_Click_1);
		this.buttonItem18.ImageIndex = 29;
		this.buttonItem18.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem18.Name = "buttonItem18";
		this.buttonItem18.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.buttonItem25, this.buttonItem19 });
		this.buttonItem18.SubItemsExpandWidth = 14;
		this.buttonItem18.Tag = "120";
		this.buttonItem18.Text = "Plantilla de Orden de Compra";
		this.buttonItem18.Click += new System.EventHandler(buttonItem18_Click_2);
		this.buttonItem25.Name = "buttonItem25";
		this.buttonItem25.Text = "Nueva Plantilla";
		this.buttonItem25.Click += new System.EventHandler(buttonItem25_Click_1);
		this.buttonItem19.Name = "buttonItem19";
		this.buttonItem19.Tag = "115";
		this.buttonItem19.Text = "Listado de Plantillas";
		this.buttonItem19.Click += new System.EventHandler(buttonItem19_Click_1);
		this.buttonItem27.ImageIndex = 31;
		this.buttonItem27.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem27.Name = "buttonItem27";
		this.buttonItem27.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.btnItemNuevaPropuesta, this.btnItemListadoPropuestas });
		this.buttonItem27.SubItemsExpandWidth = 14;
		this.buttonItem27.Tag = "166";
		this.buttonItem27.Text = "Propuestas de Orden De Compra";
		this.buttonItem27.Click += new System.EventHandler(buttonItem27_Click_1);
		this.btnItemNuevaPropuesta.Name = "btnItemNuevaPropuesta";
		this.btnItemNuevaPropuesta.Text = "Nueva Propuesta de OC";
		this.btnItemNuevaPropuesta.Click += new System.EventHandler(btnItemNuevaPropuesta_Click);
		this.btnItemListadoPropuestas.Name = "btnItemListadoPropuestas";
		this.btnItemListadoPropuestas.Text = "Listado de Propuestas de OC";
		this.btnItemListadoPropuestas.Click += new System.EventHandler(btnItemListadoPropuestas_Click);
		this.ribbonPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel5.Controls.Add(this.ribbonBar13);
		this.ribbonPanel5.Controls.Add(this.ribbonBar6);
		this.ribbonPanel5.Controls.Add(this.ribbonBar3);
		this.ribbonPanel5.Controls.Add(this.rbVentas);
		this.ribbonPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel5.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel5.Name = "ribbonPanel5";
		this.ribbonPanel5.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel5.Size = new System.Drawing.Size(903, 117);
		this.ribbonPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel5.TabIndex = 5;
		this.ribbonPanel5.Visible = false;
		this.ribbonBar13.AutoOverflowEnabled = true;
		this.ribbonBar13.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar13.ContainerControlProcessDialogKey = true;
		this.ribbonBar13.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar13.DragDropSupport = true;
		this.ribbonBar13.Images = this.imageList1;
		this.ribbonBar13.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.btnPromedioProductosVendidos });
		this.ribbonBar13.Location = new System.Drawing.Point(763, 0);
		this.ribbonBar13.Name = "ribbonBar13";
		this.ribbonBar13.Size = new System.Drawing.Size(121, 114);
		this.ribbonBar13.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar13.TabIndex = 4;
		this.ribbonBar13.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar13.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar13.TitleVisible = false;
		this.btnPromedioProductosVendidos.ImageIndex = 31;
		this.btnPromedioProductosVendidos.ImagePaddingHorizontal = 15;
		this.btnPromedioProductosVendidos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnPromedioProductosVendidos.Name = "btnPromedioProductosVendidos";
		this.btnPromedioProductosVendidos.SubItemsExpandWidth = 14;
		this.btnPromedioProductosVendidos.Tag = "159";
		this.btnPromedioProductosVendidos.Text = "Promedio de Productos Vendidos";
		this.btnPromedioProductosVendidos.Click += new System.EventHandler(btnPromedioProductosVendidos_Click);
		this.ribbonBar6.AutoOverflowEnabled = true;
		this.ribbonBar6.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar6.ContainerControlProcessDialogKey = true;
		this.ribbonBar6.Dock = System.Windows.Forms.DockStyle.Right;
		this.ribbonBar6.DragDropSupport = true;
		this.ribbonBar6.Images = this.imageList1;
		this.ribbonBar6.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biPedidoVenta, this.biCotizacion });
		this.ribbonBar6.Location = new System.Drawing.Point(764, 0);
		this.ribbonBar6.Name = "ribbonBar6";
		this.ribbonBar6.Size = new System.Drawing.Size(136, 114);
		this.ribbonBar6.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar6.TabIndex = 3;
		this.ribbonBar6.Text = "ribbonBar6";
		this.ribbonBar6.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar6.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar6.TitleVisible = false;
		this.biPedidoVenta.Enabled = false;
		this.biPedidoVenta.ImageIndex = 26;
		this.biPedidoVenta.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biPedidoVenta.Name = "biPedidoVenta";
		this.biPedidoVenta.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biPedidoVenta.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biPedidosPendientes });
		this.biPedidoVenta.SubItemsExpandWidth = 14;
		this.biPedidoVenta.Tag = "21";
		this.biPedidoVenta.Text = "Pedidos";
		this.biPedidoVenta.Visible = false;
		this.biPedidoVenta.Click += new System.EventHandler(biPedidoVenta_Click);
		this.biPedidosPendientes.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biPedidosPendientes.Name = "biPedidosPendientes";
		this.biPedidosPendientes.SubItemsExpandWidth = 14;
		this.biPedidosPendientes.Tag = "114";
		this.biPedidosPendientes.Text = "Pedidos Pendientes";
		this.biPedidosPendientes.Click += new System.EventHandler(biPedidosPendientes_Click);
		this.biCotizacion.ImageIndex = 22;
		this.biCotizacion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biCotizacion.Name = "biCotizacion";
		this.biCotizacion.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biCotizacionesVigentes, this.biCotizacionesAprobadas, this.btnOrdenCCotizacion });
		this.biCotizacion.SubItemsExpandWidth = 14;
		this.biCotizacion.Tag = "19";
		this.biCotizacion.Text = "Cotizaciones";
		this.biCotizacion.Click += new System.EventHandler(biCotizacion_Click);
		this.biCotizacionesVigentes.Name = "biCotizacionesVigentes";
		this.biCotizacionesVigentes.Tag = "20";
		this.biCotizacionesVigentes.Text = "Lista de Cotizaciones";
		this.biCotizacionesVigentes.Click += new System.EventHandler(biCotizacionesVigentes_Click);
		this.biCotizacionesAprobadas.Name = "biCotizacionesAprobadas";
		this.biCotizacionesAprobadas.Tag = "0";
		this.biCotizacionesAprobadas.Text = "Cotizaciones Aprobadas";
		this.biCotizacionesAprobadas.Visible = false;
		this.biCotizacionesAprobadas.Click += new System.EventHandler(biCotizacionesAprobadas_Click);
		this.btnOrdenCCotizacion.Name = "btnOrdenCCotizacion";
		this.btnOrdenCCotizacion.Text = "Lista Ordenes Compra";
		this.btnOrdenCCotizacion.Click += new System.EventHandler(btnOrdenCCotizacion_Click);
		this.ribbonBar3.AutoOverflowEnabled = true;
		this.ribbonBar3.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar3.ContainerControlProcessDialogKey = true;
		this.ribbonBar3.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar3.DragDropSupport = true;
		this.ribbonBar3.Images = this.imageList1;
		this.ribbonBar3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.biCobros, this.biComision2, this.biStockAlmacenes, this.biVentasporSeparacion });
		this.ribbonBar3.Location = new System.Drawing.Point(490, 0);
		this.ribbonBar3.Name = "ribbonBar3";
		this.ribbonBar3.Size = new System.Drawing.Size(273, 114);
		this.ribbonBar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar3.TabIndex = 2;
		this.ribbonBar3.Text = "ribbonBar3";
		this.ribbonBar3.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar3.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar3.TitleVisible = false;
		this.biCobros.ImageIndex = 34;
		this.biCobros.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biCobros.Name = "biCobros";
		this.biCobros.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.btnMasivo });
		this.biCobros.SubItemsExpandWidth = 14;
		this.biCobros.Tag = "119";
		this.biCobros.Text = "Cobros";
		this.biCobros.Click += new System.EventHandler(biCobros_Click);
		this.btnMasivo.Enabled = false;
		this.btnMasivo.Name = "btnMasivo";
		this.btnMasivo.Tag = "0";
		this.btnMasivo.Text = "Cobro Masivo";
		this.btnMasivo.Visible = false;
		this.btnMasivo.Click += new System.EventHandler(btnMasivo_Click);
		this.biComision2.Enabled = false;
		this.biComision2.ImageIndex = 38;
		this.biComision2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biComision2.Name = "biComision2";
		this.biComision2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biComisionVendedores, this.biComisionVentas });
		this.biComision2.SubItemsExpandWidth = 14;
		this.biComision2.Tag = "16";
		this.biComision2.Text = "Comisiones";
		this.biComision2.Visible = false;
		this.biComisionVendedores.Name = "biComisionVendedores";
		this.biComisionVendedores.Tag = "17";
		this.biComisionVendedores.Text = "Comisiones x Vendedor";
		this.biComisionVendedores.Click += new System.EventHandler(biComisionVentas_Click);
		this.biComisionVentas.Name = "biComisionVentas";
		this.biComisionVentas.Tag = "18";
		this.biComisionVentas.Text = "Comisiones x Ventas";
		this.biComisionVentas.Click += new System.EventHandler(biComisionVentas_Click_1);
		this.biStockAlmacenes.ImageIndex = 48;
		this.biStockAlmacenes.ImagePaddingHorizontal = 15;
		this.biStockAlmacenes.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biStockAlmacenes.Name = "biStockAlmacenes";
		this.biStockAlmacenes.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem17 });
		this.biStockAlmacenes.SubItemsExpandWidth = 14;
		this.biStockAlmacenes.Tag = "92";
		this.biStockAlmacenes.Text = "Stock de Almacenes";
		this.biStockAlmacenes.Click += new System.EventHandler(biStockAlmacenes_Click);
		this.buttonItem17.Name = "buttonItem17";
		this.buttonItem17.Text = "Control de Almacenes";
		this.buttonItem17.Click += new System.EventHandler(buttonItem17_Click);
		this.biVentasporSeparacion.Enabled = false;
		this.biVentasporSeparacion.ImageIndex = 53;
		this.biVentasporSeparacion.ImagePaddingHorizontal = 15;
		this.biVentasporSeparacion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biVentasporSeparacion.Name = "biVentasporSeparacion";
		this.biVentasporSeparacion.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biListaVentasSeparacion });
		this.biVentasporSeparacion.SubItemsExpandWidth = 14;
		this.biVentasporSeparacion.Tag = "105";
		this.biVentasporSeparacion.Text = "Venta Por Separacion";
		this.biVentasporSeparacion.Visible = false;
		this.biVentasporSeparacion.Click += new System.EventHandler(buttonItem12_Click);
		this.biListaVentasSeparacion.Name = "biListaVentasSeparacion";
		this.biListaVentasSeparacion.Tag = "113";
		this.biListaVentasSeparacion.Text = "Ver Ventas Por Separacion";
		this.biListaVentasSeparacion.Click += new System.EventHandler(buttonItem13_Click);
		this.rbVentas.AutoOverflowEnabled = true;
		this.rbVentas.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbVentas.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbVentas.ContainerControlProcessDialogKey = true;
		this.rbVentas.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbVentas.DragDropSupport = true;
		this.rbVentas.Images = this.imageList1;
		this.rbVentas.Items.AddRange(new DevComponents.DotNetBar.BaseItem[8] { this.biVenta, this.btnItemVenta, this.biVentaRapida, this.biGuia, this.biNotaCredito, this.biNotaDebito, this.biConsultorExterno, this.biOrdenVenta });
		this.rbVentas.Location = new System.Drawing.Point(3, 0);
		this.rbVentas.Name = "rbVentas";
		this.rbVentas.Size = new System.Drawing.Size(487, 114);
		this.rbVentas.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbVentas.TabIndex = 0;
		this.rbVentas.Text = "Documentos de Ventas";
		this.rbVentas.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbVentas.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbVentas.TitleVisible = false;
		this.biVenta.ImageIndex = 20;
		this.biVenta.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biVenta.Name = "biVenta";
		this.biVenta.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biMuestraVentas, this.biVentasPendientesDespacho, this.buttonItem12 });
		this.biVenta.SubItemsExpandWidth = 14;
		this.biVenta.Tag = "7";
		this.biVenta.Text = "Venta Rápida";
		this.biVenta.Visible = false;
		this.biVenta.Click += new System.EventHandler(biVenta_Click);
		this.biMuestraVentas.Name = "biMuestraVentas";
		this.biMuestraVentas.Tag = "112";
		this.biMuestraVentas.Text = "Lista Ventas";
		this.biMuestraVentas.Click += new System.EventHandler(biMuestraVentas_Click);
		this.biVentasPendientesDespacho.Name = "biVentasPendientesDespacho";
		this.biVentasPendientesDespacho.Tag = "0";
		this.biVentasPendientesDespacho.Text = "Ventas Pendientes de Despacho";
		this.biVentasPendientesDespacho.Visible = false;
		this.biVentasPendientesDespacho.Click += new System.EventHandler(buttonItem10_Click);
		this.buttonItem12.Name = "buttonItem12";
		this.buttonItem12.Text = "Venta rapida";
		this.buttonItem12.Click += new System.EventHandler(buttonItem12_Click_2);
		this.btnItemVenta.Image = (System.Drawing.Image)resources.GetObject("btnItemVenta.Image");
		this.btnItemVenta.ImageIndex = 5;
		this.btnItemVenta.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnItemVenta.Name = "btnItemVenta";
		this.btnItemVenta.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.biovpendientes, this.biovhistorial, this.buttonItem14, this.btnguiasfacturacion });
		this.btnItemVenta.SubItemsExpandWidth = 14;
		this.btnItemVenta.Tag = "45";
		this.btnItemVenta.Text = "Genera Venta";
		this.btnItemVenta.Click += new System.EventHandler(btnItemVenta_Click);
		this.biovpendientes.Name = "biovpendientes";
		this.biovpendientes.Text = "OV Pendientes";
		this.biovpendientes.Visible = false;
		this.biovpendientes.Click += new System.EventHandler(biovpendientes_Click);
		this.biovhistorial.Name = "biovhistorial";
		this.biovhistorial.Text = "OV Historial";
		this.biovhistorial.Click += new System.EventHandler(biovhistorial_Click);
		this.buttonItem14.Name = "buttonItem14";
		this.buttonItem14.Text = "Lista Ventas";
		this.buttonItem14.Click += new System.EventHandler(buttonItem14_Click);
		this.btnguiasfacturacion.Name = "btnguiasfacturacion";
		this.btnguiasfacturacion.Text = "Lista Guias";
		this.btnguiasfacturacion.Click += new System.EventHandler(btnguiasfacturacion_Click);
		this.biVentaRapida.Enabled = false;
		this.biVentaRapida.ImageIndex = 19;
		this.biVentaRapida.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biVentaRapida.Name = "biVentaRapida";
		this.biVentaRapida.SubItemsExpandWidth = 14;
		this.biVentaRapida.Tag = "9";
		this.biVentaRapida.Text = "Venta";
		this.biVentaRapida.Visible = false;
		this.biGuia.ImageIndex = 24;
		this.biGuia.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biGuia.Name = "biGuia";
		this.biGuia.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biGuias, this.biBuscarGuia });
		this.biGuia.SubItemsExpandWidth = 14;
		this.biGuia.Tag = "10";
		this.biGuia.Text = "Guia Remision Venta";
		this.biGuia.Click += new System.EventHandler(biGuia_Click);
		this.biGuias.Name = "biGuias";
		this.biGuias.Tag = "11";
		this.biGuias.Text = "Guias de Remision";
		this.biGuias.Click += new System.EventHandler(biGuias_Click);
		this.biBuscarGuia.Name = "biBuscarGuia";
		this.biBuscarGuia.Tag = "12";
		this.biBuscarGuia.Text = "Buscar Guias";
		this.biBuscarGuia.Visible = false;
		this.biBuscarGuia.Click += new System.EventHandler(biBuscarGuia_Click);
		this.biNotaCredito.ImageIndex = 40;
		this.biNotaCredito.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNotaCredito.Name = "biNotaCredito";
		this.biNotaCredito.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.ciNotasdeCredito, this.buttonItem30 });
		this.biNotaCredito.SubItemsExpandWidth = 14;
		this.biNotaCredito.Tag = "13";
		this.biNotaCredito.Text = "Notas de Crédito";
		this.biNotaCredito.Click += new System.EventHandler(biNotaCredito_Click);
		this.ciNotasdeCredito.Name = "ciNotasdeCredito";
		this.ciNotasdeCredito.Tag = "14";
		this.ciNotasdeCredito.Text = "Notas de Crédito";
		this.ciNotasdeCredito.Click += new System.EventHandler(ciNotasdeCredito_Click);
		this.buttonItem30.Name = "buttonItem30";
		this.buttonItem30.Text = "Notas de Crédito Aplicadas";
		this.buttonItem30.Click += new System.EventHandler(buttonItem30_Click_1);
		this.biNotaDebito.ImageIndex = 40;
		this.biNotaDebito.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNotaDebito.Name = "biNotaDebito";
		this.biNotaDebito.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.ciNotasdeDebito });
		this.biNotaDebito.SubItemsExpandWidth = 14;
		this.biNotaDebito.Tag = "105";
		this.biNotaDebito.Text = "Nota de Débito";
		this.biNotaDebito.Click += new System.EventHandler(biNotaDebito_Click);
		this.ciNotasdeDebito.Name = "ciNotasdeDebito";
		this.ciNotasdeDebito.Text = "Notas de Débito";
		this.ciNotasdeDebito.Click += new System.EventHandler(ciNotasdeDebito_Click);
		this.biConsultorExterno.Enabled = false;
		this.biConsultorExterno.ImageIndex = 26;
		this.biConsultorExterno.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biConsultorExterno.Name = "biConsultorExterno";
		this.biConsultorExterno.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biConsultorExterno.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.buttonItem4, this.buttonItem5 });
		this.biConsultorExterno.SubItemsExpandWidth = 14;
		this.biConsultorExterno.Tag = "103";
		this.biConsultorExterno.Text = "Consultor Externo";
		this.biConsultorExterno.Visible = false;
		this.biConsultorExterno.Click += new System.EventHandler(biConsultorExterno_Click);
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Tag = "22";
		this.buttonItem4.Text = "Ver Entregas";
		this.buttonItem4.Click += new System.EventHandler(buttonItem4_Click_1);
		this.buttonItem5.Name = "buttonItem5";
		this.buttonItem5.Text = "Venta";
		this.buttonItem5.Click += new System.EventHandler(buttonItem5_Click_1);
		this.biOrdenVenta.ImageIndex = 26;
		this.biOrdenVenta.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biOrdenVenta.Name = "biOrdenVenta";
		this.biOrdenVenta.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biPedidoPendiente, this.biHistorialOrdenesVenta });
		this.biOrdenVenta.SubItemsExpandWidth = 14;
		this.biOrdenVenta.Text = "Órdenes de Venta";
		this.biOrdenVenta.Visible = false;
		this.biOrdenVenta.Click += new System.EventHandler(biOrdenVenta_Click);
		this.biPedidoPendiente.Name = "biPedidoPendiente";
		this.biPedidoPendiente.Text = "Órdenes de Venta Pendientes";
		this.biPedidoPendiente.Click += new System.EventHandler(biPedidoPendiente_Click);
		this.biHistorialOrdenesVenta.Name = "biHistorialOrdenesVenta";
		this.biHistorialOrdenesVenta.Text = "OV Historial";
		this.biHistorialOrdenesVenta.Click += new System.EventHandler(biHistorialOrdenesVenta_Click);
		this.ribbonPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel4.Controls.Add(this.ribbonBar16);
		this.ribbonPanel4.Controls.Add(this.ribbonBar15);
		this.ribbonPanel4.Controls.Add(this.ribbonBar11);
		this.ribbonPanel4.Controls.Add(this.ribbonBar7);
		this.ribbonPanel4.Controls.Add(this.ribbonBar2);
		this.ribbonPanel4.Controls.Add(this.ribbonBar4);
		this.ribbonPanel4.Controls.Add(this.rbConfigurar);
		this.ribbonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel4.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel4.Name = "ribbonPanel4";
		this.ribbonPanel4.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel4.Size = new System.Drawing.Size(967, 117);
		this.ribbonPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel4.TabIndex = 4;
		this.ribbonPanel4.Visible = false;
		this.ribbonBar16.AutoOverflowEnabled = true;
		this.ribbonBar16.BackColor = System.Drawing.Color.Transparent;
		this.ribbonBar16.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar16.ContainerControlProcessDialogKey = true;
		this.ribbonBar16.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar16.DragDropSupport = true;
		this.ribbonBar16.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem28 });
		this.ribbonBar16.Location = new System.Drawing.Point(761, 0);
		this.ribbonBar16.Name = "ribbonBar16";
		this.ribbonBar16.Size = new System.Drawing.Size(109, 114);
		this.ribbonBar16.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar16.TabIndex = 7;
		this.ribbonBar16.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar16.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.buttonItem28.Image = SIGEFA.Properties.Resources.data_transfer_38px;
		this.buttonItem28.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem28.Name = "buttonItem28";
		this.buttonItem28.SubItemsExpandWidth = 14;
		this.buttonItem28.Tag = "161";
		this.buttonItem28.Text = "Categoría Ingresos-Egresos";
		this.buttonItem28.Click += new System.EventHandler(buttonItem28_Click_1);
		this.ribbonBar15.AutoOverflowEnabled = true;
		this.ribbonBar15.BackColor = System.Drawing.Color.Transparent;
		this.ribbonBar15.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar15.ContainerControlProcessDialogKey = true;
		this.ribbonBar15.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar15.DragDropSupport = true;
		this.ribbonBar15.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem26 });
		this.ribbonBar15.Location = new System.Drawing.Point(697, 0);
		this.ribbonBar15.Name = "ribbonBar15";
		this.ribbonBar15.Size = new System.Drawing.Size(64, 114);
		this.ribbonBar15.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar15.TabIndex = 2;
		this.ribbonBar15.Text = "ribbonBar15";
		this.ribbonBar15.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar15.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar15.TitleVisible = false;
		this.ribbonBar15.Visible = false;
		this.buttonItem26.Image = SIGEFA.Properties.Resources.agregar;
		this.buttonItem26.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem26.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
		this.buttonItem26.Name = "buttonItem26";
		this.buttonItem26.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.btnActualizaProductosPlantilla });
		this.buttonItem26.SubItemsExpandWidth = 14;
		this.buttonItem26.Text = "Opciones";
		this.btnActualizaProductosPlantilla.Name = "btnActualizaProductosPlantilla";
		this.btnActualizaProductosPlantilla.Text = "Actualizar Datos de Productos en Plantillas";
		this.btnActualizaProductosPlantilla.Click += new System.EventHandler(btnActualizaProductosPlantilla_Click);
		this.ribbonBar11.AutoOverflowEnabled = true;
		this.ribbonBar11.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar11.ContainerControlProcessDialogKey = true;
		this.ribbonBar11.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar11.DragDropSupport = true;
		this.ribbonBar11.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.buttonItem6 });
		this.ribbonBar11.Location = new System.Drawing.Point(622, 0);
		this.ribbonBar11.Name = "ribbonBar11";
		this.ribbonBar11.Size = new System.Drawing.Size(75, 114);
		this.ribbonBar11.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar11.TabIndex = 6;
		this.ribbonBar11.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar11.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar11.Visible = false;
		this.buttonItem6.Enabled = false;
		this.buttonItem6.Image = (System.Drawing.Image)resources.GetObject("buttonItem6.Image");
		this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem6.Name = "buttonItem6";
		this.buttonItem6.RibbonWordWrap = false;
		this.buttonItem6.SubItemsExpandWidth = 14;
		this.buttonItem6.Tag = "77";
		this.buttonItem6.Text = "Excel 1";
		this.buttonItem6.Visible = false;
		this.buttonItem6.Click += new System.EventHandler(buttonItem6_Click_1);
		this.ribbonBar7.AutoOverflowEnabled = true;
		this.ribbonBar7.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar7.ContainerControlProcessDialogKey = true;
		this.ribbonBar7.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar7.DragDropSupport = true;
		this.ribbonBar7.Images = this.imageList1;
		this.ribbonBar7.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biEnviodeDocumentos, this.biRegeneracion, this.bitermetro });
		this.ribbonBar7.Location = new System.Drawing.Point(467, 0);
		this.ribbonBar7.Name = "ribbonBar7";
		this.ribbonBar7.Size = new System.Drawing.Size(155, 114);
		this.ribbonBar7.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar7.TabIndex = 4;
		this.ribbonBar7.Text = "ribbonBar7";
		this.ribbonBar7.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar7.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar7.TitleVisible = false;
		this.biEnviodeDocumentos.ImageIndex = 31;
		this.biEnviodeDocumentos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEnviodeDocumentos.Name = "biEnviodeDocumentos";
		this.biEnviodeDocumentos.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biRpteDocumentosElectronicos, this.btnResumen });
		this.biEnviodeDocumentos.SubItemsExpandWidth = 14;
		this.biEnviodeDocumentos.Tag = "119";
		this.biEnviodeDocumentos.Text = "Envio de Documentos";
		this.biEnviodeDocumentos.Click += new System.EventHandler(buttonItem19_Click);
		this.biRpteDocumentosElectronicos.Name = "biRpteDocumentosElectronicos";
		this.biRpteDocumentosElectronicos.Text = "Reporte de Documentos Electrónicos";
		this.biRpteDocumentosElectronicos.Click += new System.EventHandler(biRpteDocumentosElectronicos_Click);
		this.btnResumen.Name = "btnResumen";
		this.btnResumen.Tag = "119";
		this.btnResumen.Text = "Resumen diario";
		this.btnResumen.Click += new System.EventHandler(btnResumen_Click);
		this.biRegeneracion.Image = SIGEFA.Properties.Resources.regeneracion;
		this.biRegeneracion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRegeneracion.Name = "biRegeneracion";
		this.biRegeneracion.SubItemsExpandWidth = 14;
		this.biRegeneracion.Tag = "160";
		this.biRegeneracion.Text = "Regeneracion de Archivos";
		this.biRegeneracion.Click += new System.EventHandler(biRegeneracion_Click);
		this.bitermetro.Enabled = false;
		this.bitermetro.ImageIndex = 42;
		this.bitermetro.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.bitermetro.Name = "bitermetro";
		this.bitermetro.SubItemsExpandWidth = 14;
		this.bitermetro.Tag = "0";
		this.bitermetro.Text = "Termometro de ventas";
		this.bitermetro.Visible = false;
		this.bitermetro.Click += new System.EventHandler(buttonItem1_Click_1);
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biBackup, this.biImport });
		this.ribbonBar2.Location = new System.Drawing.Point(401, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(66, 114);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 3;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.biBackup.ImageIndex = 32;
		this.biBackup.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBackup.Name = "biBackup";
		this.biBackup.SubItemsExpandWidth = 14;
		this.biBackup.Tag = "75";
		this.biBackup.Text = "Generar Backup";
		this.biBackup.Click += new System.EventHandler(biBackup_Click);
		this.biImport.ImageIndex = 33;
		this.biImport.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImport.Name = "biImport";
		this.biImport.SubItemsExpandWidth = 14;
		this.biImport.Tag = "76";
		this.biImport.Text = "Importar BD";
		this.biImport.Visible = false;
		this.biImport.Click += new System.EventHandler(biImport_Click);
		this.ribbonBar4.AutoOverflowEnabled = true;
		this.ribbonBar4.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar4.ContainerControlProcessDialogKey = true;
		this.ribbonBar4.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar4.DragDropSupport = true;
		this.ribbonBar4.Images = this.imageList2;
		this.ribbonBar4.Items.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.biTablas, this.biParametros });
		this.ribbonBar4.Location = new System.Drawing.Point(263, 0);
		this.ribbonBar4.Name = "ribbonBar4";
		this.ribbonBar4.Size = new System.Drawing.Size(138, 114);
		this.ribbonBar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar4.TabIndex = 2;
		this.ribbonBar4.Text = "Configuración";
		this.ribbonBar4.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar4.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar4.TitleVisible = false;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "products.png");
		this.imageList2.Images.SetKeyName(1, "cliente.png");
		this.imageList2.Images.SetKeyName(2, "1211811759.png");
		this.imageList2.Images.SetKeyName(3, "user-icon-512.png");
		this.imageList2.Images.SetKeyName(4, "1325228163.jpg");
		this.imageList2.Images.SetKeyName(5, "cal3png.png");
		this.imageList2.Images.SetKeyName(6, "Get Document.ico");
		this.imageList2.Images.SetKeyName(7, "Send Document.ico");
		this.imageList2.Images.SetKeyName(8, "Transfer Document.ico");
		this.imageList2.Images.SetKeyName(9, "compras_a_proveedores_01.png");
		this.imageList2.Images.SetKeyName(10, "boxes copy_thumb.png");
		this.imageList2.Images.SetKeyName(11, "TarjetasKardex-1-PNG.png");
		this.imageList2.Images.SetKeyName(12, "a-propos-bleu-utilisateur-icone-7595-96.png");
		this.imageList2.Images.SetKeyName(13, "inventarios1.jpg");
		this.imageList2.Images.SetKeyName(14, "d9dc81882f20a4fb51dadd294dd1b4d5.png");
		this.imageList2.Images.SetKeyName(15, "Almacen.png");
		this.imageList2.Images.SetKeyName(16, "proveedor.png");
		this.imageList2.Images.SetKeyName(17, "company_256.png");
		this.imageList2.Images.SetKeyName(18, "iEngrenages.png");
		this.imageList2.Images.SetKeyName(19, "bag.png");
		this.imageList2.Images.SetKeyName(20, "venta.png");
		this.imageList2.Images.SetKeyName(21, "boleta-link.png");
		this.imageList2.Images.SetKeyName(22, "cotizacion.png");
		this.imageList2.Images.SetKeyName(23, "factura-icon.jpg");
		this.imageList2.Images.SetKeyName(24, "icon_shippingbox_withcalendar.png");
		this.imageList2.Images.SetKeyName(25, "images (1).jpg");
		this.imageList2.Images.SetKeyName(26, "pedido.png");
		this.imageList2.Images.SetKeyName(27, "pedidos.png");
		this.imageList2.Images.SetKeyName(28, "DocumentSearch.png");
		this.imageList2.Images.SetKeyName(29, "editar-una-pluma-para-escribir-icono-6827-96.png");
		this.imageList2.Images.SetKeyName(30, "Icono-Borrar-Anuncio.gif");
		this.imageList2.Images.SetKeyName(31, "lista-de-regalos.png");
		this.imageList2.Images.SetKeyName(32, "database-backup-cd-512.png");
		this.imageList2.Images.SetKeyName(33, "database-backup-icon-512.png");
		this.imageList2.Images.SetKeyName(34, "pagos.png");
		this.imageList2.Images.SetKeyName(35, "pagossol.png");
		this.imageList2.Images.SetKeyName(36, "lista-de-regalos.png");
		this.imageList2.Images.SetKeyName(37, "ICONO-INVENTARIO.jpg");
		this.imageList2.Images.SetKeyName(38, "Porcentaje (1).png");
		this.imageList2.Images.SetKeyName(39, "DeleteRed.png");
		this.imageList2.Images.SetKeyName(40, "credit-note.png");
		this.imageList2.Images.SetKeyName(41, "ventass.png");
		this.imageList2.Images.SetKeyName(42, "reporte.png");
		this.imageList2.Images.SetKeyName(43, "control_asistencia.png");
		this.imageList2.Images.SetKeyName(44, "caja-fuerte.png");
		this.imageList2.Images.SetKeyName(45, "1407444313_3294.ico");
		this.imageList2.Images.SetKeyName(46, "1407444370_3472.png");
		this.imageList2.Images.SetKeyName(47, "1407444353_17840.ico");
		this.imageList2.Images.SetKeyName(48, "recycle_bin_full.ico");
		this.imageList2.Images.SetKeyName(49, "nissan_qashqai_2008.png");
		this.imageList2.Images.SetKeyName(50, "3606.png");
		this.imageList2.Images.SetKeyName(51, "folder_black_configure_88705.png");
		this.imageList2.Images.SetKeyName(52, "AndroMoneyss.png");
		this.imageList2.Images.SetKeyName(53, "DSCN3324s.png");
		this.imageList2.Images.SetKeyName(54, "9544028-banco-render-3dss.png");
		this.imageList2.Images.SetKeyName(55, "ReporteProblemas2.png");
		this.imageList2.Images.SetKeyName(56, "pedidos2.png");
		this.imageList2.Images.SetKeyName(57, "400_F_2.png");
		this.imageList2.Images.SetKeyName(58, "caja-chica-pymess2.png");
		this.imageList2.Images.SetKeyName(59, "cajaFuertes.png");
		this.imageList2.Images.SetKeyName(60, "caja_registradora2.png");
		this.imageList2.Images.SetKeyName(61, "caja_registradora.png");
		this.imageList2.Images.SetKeyName(62, "iconos_software_pro_.png");
		this.imageList2.Images.SetKeyName(63, "programacion2.png");
		this.imageList2.Images.SetKeyName(64, "calculadora2.png");
		this.imageList2.Images.SetKeyName(65, "compras2.png");
		this.imageList2.Images.SetKeyName(66, "shopping2.png");
		this.imageList2.Images.SetKeyName(67, "buck up generar.png");
		this.imageList2.Images.SetKeyName(68, "buck up guarda.png");
		this.imageList2.Images.SetKeyName(69, "configurationes.png");
		this.imageList2.Images.SetKeyName(70, "configuration1.png");
		this.imageList2.Images.SetKeyName(71, "Kyo-Tux-Aeon-Folder-Black-Configure.ico");
		this.imageList2.Images.SetKeyName(72, "control_panel1.png");
		this.imageList2.Images.SetKeyName(73, "cajachicaotros.png");
		this.imageList2.Images.SetKeyName(74, "libro.png");
		this.imageList2.Images.SetKeyName(75, "LE.png");
		this.imageList2.Images.SetKeyName(76, "logout1.png");
		this.imageList2.Images.SetKeyName(77, "bloggif_57aa8110e7163.jpeg");
		this.imageList2.Images.SetKeyName(78, "cajaven.png");
		this.imageList2.Images.SetKeyName(79, "cierre.png");
		this.imageList2.Images.SetKeyName(80, "15.png");
		this.biTablas.ImageIndex = 18;
		this.biTablas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biTablas.Name = "biTablas";
		this.biTablas.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[25]
		{
			this.biUnidades, this.biFamilias, this.biMarcas, this.biTipoArticulo, this.biCaracteristica, this.biDocumentos, this.biTransacciones, this.biTipoCambio, this.biAutorizado, this.biFormaPago,
			this.biMetodoPago, this.biListasPrecios, this.biVehiculosTransporte, this.biConductores, this.biEmpresasTransporte, this.biZonas, this.biDestaques, this.biBancos, this.biCuentasCorrientes, this.biTarjetaPago,
			this.biTipoEgresoCaja, this.biTablaSistema, this.btnConfiguraciones, this.buttonItem31, this.btnparametrodescuento
		});
		this.biTablas.SubItemsExpandWidth = 14;
		this.biTablas.Tag = "53";
		this.biTablas.Text = "Tablas";
		this.biTablas.Click += new System.EventHandler(biTablas_Click);
		this.biUnidades.Name = "biUnidades";
		this.biUnidades.Tag = "54";
		this.biUnidades.Text = "Unidades";
		this.biUnidades.Click += new System.EventHandler(buttonItem26_Click);
		this.biFamilias.Name = "biFamilias";
		this.biFamilias.Tag = "55";
		this.biFamilias.Text = "Familias";
		this.biFamilias.Click += new System.EventHandler(buttonItem27_Click);
		this.biMarcas.Name = "biMarcas";
		this.biMarcas.Tag = "56";
		this.biMarcas.Text = "Marcas";
		this.biMarcas.Click += new System.EventHandler(buttonItem28_Click);
		this.biTipoArticulo.Name = "biTipoArticulo";
		this.biTipoArticulo.Tag = "57";
		this.biTipoArticulo.Text = "Tipo de Articulos";
		this.biTipoArticulo.Click += new System.EventHandler(buttonItem29_Click);
		this.biCaracteristica.Name = "biCaracteristica";
		this.biCaracteristica.Tag = "58";
		this.biCaracteristica.Text = "Caracteristicas";
		this.biCaracteristica.Click += new System.EventHandler(buttonItem30_Click);
		this.biDocumentos.Name = "biDocumentos";
		this.biDocumentos.Tag = "59";
		this.biDocumentos.Text = "Documentos";
		this.biDocumentos.Click += new System.EventHandler(biDocumentos_Click);
		this.biTransacciones.Name = "biTransacciones";
		this.biTransacciones.Tag = "60";
		this.biTransacciones.Text = "Transacciones";
		this.biTransacciones.Click += new System.EventHandler(biTransacciones_Click);
		this.biTipoCambio.Name = "biTipoCambio";
		this.biTipoCambio.Tag = "61";
		this.biTipoCambio.Text = "Tipo de Cambio";
		this.biTipoCambio.Click += new System.EventHandler(biTipoCambio_Click);
		this.biAutorizado.Name = "biAutorizado";
		this.biAutorizado.Tag = "62";
		this.biAutorizado.Text = "Autorizado";
		this.biAutorizado.Click += new System.EventHandler(biAutorizado_Click);
		this.biFormaPago.Name = "biFormaPago";
		this.biFormaPago.Tag = "63";
		this.biFormaPago.Text = "Forma de Pago";
		this.biFormaPago.Click += new System.EventHandler(biFormaPago_Click);
		this.biMetodoPago.Name = "biMetodoPago";
		this.biMetodoPago.Tag = "64";
		this.biMetodoPago.Text = "Metodos de Pago";
		this.biMetodoPago.Click += new System.EventHandler(biMetodoPago_Click);
		this.biListasPrecios.Name = "biListasPrecios";
		this.biListasPrecios.Tag = "65";
		this.biListasPrecios.Text = "Tipo Precios";
		this.biListasPrecios.Click += new System.EventHandler(biListasPrecios_Click);
		this.biVehiculosTransporte.Name = "biVehiculosTransporte";
		this.biVehiculosTransporte.Tag = "66";
		this.biVehiculosTransporte.Text = "Vehiculos Transporte";
		this.biVehiculosTransporte.Click += new System.EventHandler(biVehiculosTransporte_Click);
		this.biConductores.Name = "biConductores";
		this.biConductores.Tag = "67";
		this.biConductores.Text = "Conductores";
		this.biConductores.Click += new System.EventHandler(biConductores_Click);
		this.biEmpresasTransporte.Name = "biEmpresasTransporte";
		this.biEmpresasTransporte.Tag = "68";
		this.biEmpresasTransporte.Text = "Empresas Transporte";
		this.biEmpresasTransporte.Click += new System.EventHandler(biEmpresasTransporte_Click);
		this.biZonas.Name = "biZonas";
		this.biZonas.Tag = "69";
		this.biZonas.Text = "Zonas";
		this.biZonas.Click += new System.EventHandler(biZonas_Click);
		this.biDestaques.Name = "biDestaques";
		this.biDestaques.Tag = "71";
		this.biDestaques.Text = "Destaques";
		this.biDestaques.Click += new System.EventHandler(biDestaques_Click);
		this.biBancos.Name = "biBancos";
		this.biBancos.Tag = "72";
		this.biBancos.Text = "Bancos";
		this.biBancos.Click += new System.EventHandler(biBancos_Click);
		this.biCuentasCorrientes.Name = "biCuentasCorrientes";
		this.biCuentasCorrientes.Tag = "81";
		this.biCuentasCorrientes.Text = "Cuentas Bancarias";
		this.biCuentasCorrientes.Click += new System.EventHandler(biCuentasCorrientes_Click);
		this.biTarjetaPago.Name = "biTarjetaPago";
		this.biTarjetaPago.Tag = "82";
		this.biTarjetaPago.Text = "Tarjetas de Pago";
		this.biTarjetaPago.Click += new System.EventHandler(biTarjetaPago_Click);
		this.biTipoEgresoCaja.Name = "biTipoEgresoCaja";
		this.biTipoEgresoCaja.Tag = "95";
		this.biTipoEgresoCaja.Text = "Tipo de Egreso Caja";
		this.biTipoEgresoCaja.Click += new System.EventHandler(biTipoEgresoCaja_Click);
		this.biTablaSistema.Name = "biTablaSistema";
		this.biTablaSistema.Text = "Tabla del Sistema";
		this.biTablaSistema.Click += new System.EventHandler(biTablaSistema_Click);
		this.btnConfiguraciones.Name = "btnConfiguraciones";
		this.btnConfiguraciones.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.btnDespacho, this.btnOrdenesDeVenta });
		this.btnConfiguraciones.Text = "Configuraciones";
		this.btnDespacho.Name = "btnDespacho";
		this.btnDespacho.Text = "Despacho";
		this.btnDespacho.Click += new System.EventHandler(btnDespacho_Click);
		this.btnOrdenesDeVenta.Name = "btnOrdenesDeVenta";
		this.btnOrdenesDeVenta.Text = "Ordenes de Venta";
		this.btnOrdenesDeVenta.Click += new System.EventHandler(btnOrdenesDeVenta_Click);
		this.buttonItem31.Name = "buttonItem31";
		this.buttonItem31.Tag = "142";
		this.buttonItem31.Text = "Parámetro NC";
		this.buttonItem31.Click += new System.EventHandler(buttonItem31_Click);
		this.btnparametrodescuento.Name = "btnparametrodescuento";
		this.btnparametrodescuento.Text = "Parametros Descuentos";
		this.btnparametrodescuento.Click += new System.EventHandler(btnparametrodescuento_Click);
		this.biParametros.ImageFixedSize = new System.Drawing.Size(48, 48);
		this.biParametros.ImageIndex = 72;
		this.biParametros.ImageListSizeSelection = DevComponents.DotNetBar.eButtonImageListSelection.Default;
		this.biParametros.ImagePaddingHorizontal = 10;
		this.biParametros.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biParametros.Name = "biParametros";
		this.biParametros.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[5] { this.biVigenciaCotizaciones, this.labelItem1, this.sbMercadoNegro, this.lineados, this.sbEligeDocumento });
		this.biParametros.SubItemsExpandWidth = 14;
		this.biParametros.Tag = "74";
		this.biParametros.Text = "Parametros";
		this.biParametros.Click += new System.EventHandler(biParametros_Click_1);
		this.biParametros.MouseHover += new System.EventHandler(biParametros_MouseHover);
		this.biVigenciaCotizaciones.Name = "biVigenciaCotizaciones";
		this.biVigenciaCotizaciones.Tag = "80";
		this.biVigenciaCotizaciones.Text = "Vigencia de Cotizaciones";
		this.biVigenciaCotizaciones.Click += new System.EventHandler(biVigenciaCotizaciones_Click);
		this.labelItem1.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		this.labelItem1.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
		this.labelItem1.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.labelItem1.Enabled = false;
		this.labelItem1.ForeColor = System.Drawing.Color.FromArgb(0, 21, 110);
		this.labelItem1.Name = "labelItem1";
		this.labelItem1.PaddingBottom = 1;
		this.labelItem1.PaddingLeft = 10;
		this.labelItem1.PaddingTop = 1;
		this.labelItem1.SingleLineColor = System.Drawing.Color.FromArgb(197, 197, 197);
		this.labelItem1.Text = "----------------------------------------------------------------";
		this.sbMercadoNegro.Name = "sbMercadoNegro";
		this.sbMercadoNegro.Tag = "";
		this.sbMercadoNegro.Text = "Mercado Inependiente";
		this.sbMercadoNegro.ValueChanged += new System.EventHandler(sbMercadoNegro_ValueChanged);
		this.lineados.BackColor = System.Drawing.Color.FromArgb(221, 231, 238);
		this.lineados.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
		this.lineados.BorderType = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.lineados.Enabled = false;
		this.lineados.ForeColor = System.Drawing.Color.FromArgb(0, 21, 110);
		this.lineados.Name = "lineados";
		this.lineados.PaddingBottom = 1;
		this.lineados.PaddingLeft = 10;
		this.lineados.PaddingTop = 1;
		this.lineados.SingleLineColor = System.Drawing.Color.FromArgb(197, 197, 197);
		this.lineados.Text = "----------------------------------------------------------------";
		this.sbEligeDocumento.Enabled = false;
		this.sbEligeDocumento.Name = "sbEligeDocumento";
		this.sbEligeDocumento.Text = "Documento";
		this.sbEligeDocumento.ValueChanged += new System.EventHandler(sbEligeDocumento_ValueChanged);
		this.rbConfigurar.AutoOverflowEnabled = true;
		this.rbConfigurar.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbConfigurar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbConfigurar.ContainerControlProcessDialogKey = true;
		this.rbConfigurar.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbConfigurar.DragDropSupport = true;
		this.rbConfigurar.Images = this.imageList1;
		this.rbConfigurar.Items.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.biEmpresa, this.biSucursal, this.biAlmacen, this.biUsuarios });
		this.rbConfigurar.Location = new System.Drawing.Point(3, 0);
		this.rbConfigurar.Name = "rbConfigurar";
		this.rbConfigurar.Size = new System.Drawing.Size(260, 114);
		this.rbConfigurar.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbConfigurar.TabIndex = 0;
		this.rbConfigurar.Text = "Configurar";
		this.rbConfigurar.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbConfigurar.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbConfigurar.TitleVisible = false;
		this.biEmpresa.ImageIndex = 17;
		this.biEmpresa.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEmpresa.Name = "biEmpresa";
		this.biEmpresa.SubItemsExpandWidth = 14;
		this.biEmpresa.Tag = "49";
		this.biEmpresa.Text = "Empresa";
		this.biEmpresa.Click += new System.EventHandler(buttonItem22_Click);
		this.biSucursal.ImageIndex = 46;
		this.biSucursal.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biSucursal.Name = "biSucursal";
		this.biSucursal.SubItemsExpandWidth = 14;
		this.biSucursal.Tag = "50";
		this.biSucursal.Text = "Sucursales";
		this.biSucursal.Click += new System.EventHandler(biSucursal_Click);
		this.biAlmacen.ImageIndex = 15;
		this.biAlmacen.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAlmacen.Name = "biAlmacen";
		this.biAlmacen.SubItemsExpandWidth = 14;
		this.biAlmacen.Tag = "51";
		this.biAlmacen.Text = "Almacen";
		this.biAlmacen.Click += new System.EventHandler(buttonItem23_Click);
		this.biUsuarios.ImageIndex = 12;
		this.biUsuarios.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biUsuarios.Name = "biUsuarios";
		this.biUsuarios.SubItemsExpandWidth = 14;
		this.biUsuarios.Tag = "52";
		this.biUsuarios.Text = "Usuarios";
		this.biUsuarios.Click += new System.EventHandler(biUsuarios_Click);
		this.ribbonPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel3.Controls.Add(this.riReportesGeneral);
		this.ribbonPanel3.Controls.Add(this.rbReportes);
		this.ribbonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel3.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel3.Name = "ribbonPanel3";
		this.ribbonPanel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel3.Size = new System.Drawing.Size(1015, 117);
		this.ribbonPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel3.TabIndex = 3;
		this.ribbonPanel3.Visible = false;
		this.ribbonPanel3.Click += new System.EventHandler(ribbonPanel3_Click);
		this.riReportesGeneral.AutoOverflowEnabled = true;
		this.riReportesGeneral.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.riReportesGeneral.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.riReportesGeneral.ContainerControlProcessDialogKey = true;
		this.riReportesGeneral.DragDropSupport = true;
		this.riReportesGeneral.Images = this.imageList1;
		this.riReportesGeneral.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.btnReporte, this.biUtilidad, this.biReportesGenerales });
		this.riReportesGeneral.Location = new System.Drawing.Point(206, 0);
		this.riReportesGeneral.Name = "riReportesGeneral";
		this.riReportesGeneral.Size = new System.Drawing.Size(187, 114);
		this.riReportesGeneral.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.riReportesGeneral.TabIndex = 0;
		this.riReportesGeneral.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.riReportesGeneral.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.riReportesGeneral.TitleVisible = false;
		this.btnReporte.ImageIndex = 41;
		this.btnReporte.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[15]
		{
			this.biVentasPorVendedor, this.biUtilidadVentas, this.biVentasPorCliente, this.biStockProductos, this.biKardexArticulos, this.biTotalizadoVentasResumido, this.buttonItem13, this.ganacniaxcliente, this.ventasdiarias, this.buttonItem20,
			this.mdiBtn_RptUtilidad, this.btnIAnalisisDetalladoVenta, this.ReporteAjustesInventario, this.btnanalisiscotizaciones, this.btnanalisisordencotizaciones
		});
		this.btnReporte.SubItemsExpandWidth = 14;
		this.btnReporte.Tag = "48";
		this.btnReporte.Text = "Reportes";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.biVentasPorVendedor.Name = "biVentasPorVendedor";
		this.biVentasPorVendedor.Tag = "142";
		this.biVentasPorVendedor.Text = "VENTAS POR VENDEDOR";
		this.biVentasPorVendedor.Click += new System.EventHandler(biVentasPorVendedor_Click);
		this.biUtilidadVentas.Name = "biUtilidadVentas";
		this.biUtilidadVentas.Tag = "143";
		this.biUtilidadVentas.Text = "UTILIDAD DE VENTAS";
		this.biUtilidadVentas.Click += new System.EventHandler(biUtilidadVentas_Click);
		this.biVentasPorCliente.Name = "biVentasPorCliente";
		this.biVentasPorCliente.Tag = "144";
		this.biVentasPorCliente.Text = "VENTAS POR CLIENTE";
		this.biVentasPorCliente.Click += new System.EventHandler(biVentasPorCliente_Click);
		this.biStockProductos.Name = "biStockProductos";
		this.biStockProductos.Tag = "145";
		this.biStockProductos.Text = "STOCK DE PRODUCTOS";
		this.biStockProductos.Click += new System.EventHandler(biStockProductos_Click);
		this.biKardexArticulos.Name = "biKardexArticulos";
		this.biKardexArticulos.Tag = "146";
		this.biKardexArticulos.Text = "KARDEX DE ARTICULOS";
		this.biKardexArticulos.Click += new System.EventHandler(biKardexArticulos_Click);
		this.biTotalizadoVentasResumido.Name = "biTotalizadoVentasResumido";
		this.biTotalizadoVentasResumido.Tag = "147";
		this.biTotalizadoVentasResumido.Text = "TOTALIZADO DE VENTAS - RESUMIDO";
		this.biTotalizadoVentasResumido.Click += new System.EventHandler(biTotalizadoVentasResumido_Click);
		this.buttonItem13.Name = "buttonItem13";
		this.buttonItem13.Tag = "148";
		this.buttonItem13.Text = "UTILIDAD BRUTA";
		this.buttonItem13.Click += new System.EventHandler(buttonItem13_Click_1);
		this.ganacniaxcliente.Name = "ganacniaxcliente";
		this.ganacniaxcliente.Tag = "149";
		this.ganacniaxcliente.Text = "GANANCIA X CLIENTE";
		this.ganacniaxcliente.Click += new System.EventHandler(ganacniaxcliente_Click);
		this.ventasdiarias.Name = "ventasdiarias";
		this.ventasdiarias.Tag = "150";
		this.ventasdiarias.Text = "VENTAS DIARIAS";
		this.ventasdiarias.Visible = false;
		this.ventasdiarias.Click += new System.EventHandler(ventasdiarias_Click);
		this.buttonItem20.Name = "buttonItem20";
		this.buttonItem20.Tag = "151";
		this.buttonItem20.Text = "TRANSFERENCIAS DIRECTAS";
		this.buttonItem20.Click += new System.EventHandler(buttonItem20_Click);
		this.mdiBtn_RptUtilidad.Name = "mdiBtn_RptUtilidad";
		this.mdiBtn_RptUtilidad.Tag = "152";
		this.mdiBtn_RptUtilidad.Text = "REPORTE UTILIDAD";
		this.mdiBtn_RptUtilidad.Click += new System.EventHandler(mdiBtn_RptUtilidad_Click);
		this.btnIAnalisisDetalladoVenta.Name = "btnIAnalisisDetalladoVenta";
		this.btnIAnalisisDetalladoVenta.Tag = "153";
		this.btnIAnalisisDetalladoVenta.Text = "ANALISIS DETALLADO DE VENTA";
		this.btnIAnalisisDetalladoVenta.Click += new System.EventHandler(btnIAnalisisDetalladoVenta_Click);
		this.ReporteAjustesInventario.Name = "ReporteAjustesInventario";
		this.ReporteAjustesInventario.Tag = "154";
		this.ReporteAjustesInventario.Text = "AJUSTES DE INVENTARIO";
		this.ReporteAjustesInventario.Click += new System.EventHandler(ReporteAjustesInventario_Click);
		this.btnanalisiscotizaciones.Name = "btnanalisiscotizaciones";
		this.btnanalisiscotizaciones.Text = "ANALISIS DETALLADO DE COTIZACIONES";
		this.btnanalisiscotizaciones.Click += new System.EventHandler(btnanalisiscotizaciones_Click);
		this.btnanalisisordencotizaciones.Name = "btnanalisisordencotizaciones";
		this.btnanalisisordencotizaciones.Text = "ANALISIS DETALLADO DE OC COTIZACIONES";
		this.btnanalisisordencotizaciones.Click += new System.EventHandler(btnanalisisordencotizaciones_Click);
		this.biUtilidad.ImageIndex = 34;
		this.biUtilidad.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biUtilidad.Name = "biUtilidad";
		this.biUtilidad.SubItemsExpandWidth = 14;
		this.biUtilidad.Tag = "49";
		this.biUtilidad.Text = "Utilidad";
		this.biUtilidad.Click += new System.EventHandler(buttonItem12_Click_1);
		this.biReportesGenerales.Image = SIGEFA.Properties.Resources.reporte_general48;
		this.biReportesGenerales.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biReportesGenerales.Name = "biReportesGenerales";
		this.biReportesGenerales.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.buttonItem15, this.mdiBtn_MaestroVentas, this.btnReporteVDiariaExcel, this.buttonItem29 });
		this.biReportesGenerales.SubItemsExpandWidth = 14;
		this.biReportesGenerales.Tag = "110";
		this.biReportesGenerales.Text = "Reportes Generales";
		this.biReportesGenerales.Click += new System.EventHandler(biReportesGenerales_Click);
		this.buttonItem15.Name = "buttonItem15";
		this.buttonItem15.Tag = "155";
		this.buttonItem15.Text = "VENTAS MENSUALES POR PRODUCTO";
		this.buttonItem15.Click += new System.EventHandler(buttonItem15_Click_1);
		this.mdiBtn_MaestroVentas.Name = "mdiBtn_MaestroVentas";
		this.mdiBtn_MaestroVentas.PopupAnimation = DevComponents.DotNetBar.ePopupAnimation.None;
		this.mdiBtn_MaestroVentas.Tag = "156";
		this.mdiBtn_MaestroVentas.Text = "MAESTRO VENTAS";
		this.mdiBtn_MaestroVentas.Click += new System.EventHandler(mdiBtn_MaestroVentas_Click);
		this.btnReporteVDiariaExcel.Name = "btnReporteVDiariaExcel";
		this.btnReporteVDiariaExcel.Tag = "157";
		this.btnReporteVDiariaExcel.Text = "VENTAS DIARIAS EXCEL";
		this.btnReporteVDiariaExcel.Click += new System.EventHandler(btnReporteVDiariaExcel_Click);
		this.buttonItem29.Name = "buttonItem29";
		this.buttonItem29.Tag = "158";
		this.buttonItem29.Text = "VENTAS DIARIAS - NC APLICADAS";
		this.buttonItem29.Click += new System.EventHandler(buttonItem29_Click_1);
		this.rbReportes.AutoOverflowEnabled = true;
		this.rbReportes.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbReportes.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbReportes.ContainerControlProcessDialogKey = true;
		this.rbReportes.DragDropSupport = true;
		this.rbReportes.Images = this.imageList1;
		this.rbReportes.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biInventario, this.biKardex, this.biRotacionProducto });
		this.rbReportes.Location = new System.Drawing.Point(3, 0);
		this.rbReportes.Name = "rbReportes";
		this.rbReportes.Size = new System.Drawing.Size(203, 114);
		this.rbReportes.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbReportes.TabIndex = 0;
		this.rbReportes.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbReportes.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbReportes.TitleVisible = false;
		this.biInventario.ImageIndex = 10;
		this.biInventario.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biInventario.Name = "biInventario";
		this.biInventario.SubItemsExpandWidth = 14;
		this.biInventario.Tag = "46";
		this.biInventario.Text = "Inventario";
		this.biInventario.Click += new System.EventHandler(biInventario_Click);
		this.biKardex.ImageIndex = 11;
		this.biKardex.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biKardex.Name = "biKardex";
		this.biKardex.SubItemsExpandWidth = 14;
		this.biKardex.Tag = "47";
		this.biKardex.Text = "Kardex";
		this.biKardex.Click += new System.EventHandler(biKardex_Click);
		this.biRotacionProducto.Enabled = false;
		this.biRotacionProducto.ImageIndex = 49;
		this.biRotacionProducto.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRotacionProducto.Name = "biRotacionProducto";
		this.biRotacionProducto.SubItemsExpandWidth = 14;
		this.biRotacionProducto.Tag = "101";
		this.biRotacionProducto.Text = "Rotacion de Productos";
		this.biRotacionProducto.Visible = false;
		this.biRotacionProducto.Click += new System.EventHandler(biRotacionProducto_Click);
		this.ribbonPanel7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel7.Controls.Add(this.ribbonBar8);
		this.ribbonPanel7.Controls.Add(this.ribbonBar14);
		this.ribbonPanel7.Controls.Add(this.ribbonBar9);
		this.ribbonPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel7.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel7.Name = "ribbonPanel7";
		this.ribbonPanel7.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel7.Size = new System.Drawing.Size(1119, 117);
		this.ribbonPanel7.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel7.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel7.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel7.TabIndex = 7;
		this.ribbonPanel7.Visible = false;
		this.ribbonBar8.AutoOverflowEnabled = true;
		this.ribbonBar8.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar8.ContainerControlProcessDialogKey = true;
		this.ribbonBar8.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar8.DragDropSupport = true;
		this.ribbonBar8.Images = this.imageList2;
		this.ribbonBar8.Items.AddRange(new DevComponents.DotNetBar.BaseItem[5] { this.biIngresos, this.BiAprobacionPago, this.biPrestamosBancarios, this.biMovimientosBancarios, this.BiRendiciones });
		this.ribbonBar8.Location = new System.Drawing.Point(420, 0);
		this.ribbonBar8.Name = "ribbonBar8";
		this.ribbonBar8.Size = new System.Drawing.Size(352, 114);
		this.ribbonBar8.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar8.TabIndex = 4;
		this.ribbonBar8.Text = "Tesoreria";
		this.ribbonBar8.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar8.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.biIngresos.Enabled = false;
		this.biIngresos.ImageIndex = 34;
		this.biIngresos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biIngresos.Name = "biIngresos";
		this.biIngresos.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlI);
		this.biIngresos.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.buttonItem3, this.buttonItem7, this.buttonItem8 });
		this.biIngresos.SubItemsExpandWidth = 14;
		this.biIngresos.Tag = "89";
		this.biIngresos.Text = "Tesorería";
		this.biIngresos.Visible = false;
		this.buttonItem3.Name = "buttonItem3";
		this.buttonItem3.Tag = "96";
		this.buttonItem3.Text = "Registra Cheque";
		this.buttonItem7.Name = "buttonItem7";
		this.buttonItem7.Text = "Anular Pago";
		this.buttonItem8.Name = "buttonItem8";
		this.buttonItem8.Text = "Detracciones";
		this.BiAprobacionPago.Enabled = false;
		this.BiAprobacionPago.ImageIndex = 57;
		this.BiAprobacionPago.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.BiAprobacionPago.Name = "BiAprobacionPago";
		this.BiAprobacionPago.SubItemsExpandWidth = 14;
		this.BiAprobacionPago.Tag = "106";
		this.BiAprobacionPago.Text = "Aprobación Pago";
		this.BiAprobacionPago.Visible = false;
		this.biPrestamosBancarios.Enabled = false;
		this.biPrestamosBancarios.ImageIndex = 54;
		this.biPrestamosBancarios.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biPrestamosBancarios.Name = "biPrestamosBancarios";
		this.biPrestamosBancarios.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.biListaPagPreBancarios });
		this.biPrestamosBancarios.SubItemsExpandWidth = 14;
		this.biPrestamosBancarios.Tag = "108";
		this.biPrestamosBancarios.Text = "Prestamos Bancarios";
		this.biPrestamosBancarios.Visible = false;
		this.biListaPagPreBancarios.Name = "biListaPagPreBancarios";
		this.biListaPagPreBancarios.Text = "Pagos";
		this.biMovimientosBancarios.ImageIndex = 43;
		this.biMovimientosBancarios.ImagePaddingHorizontal = 20;
		this.biMovimientosBancarios.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biMovimientosBancarios.Name = "biMovimientosBancarios";
		this.biMovimientosBancarios.SubItemsExpandWidth = 14;
		this.biMovimientosBancarios.Tag = "87";
		this.biMovimientosBancarios.Text = "Movimientos Bancarios";
		this.biMovimientosBancarios.Click += new System.EventHandler(biMovimientosBancarios_Click);
		this.BiRendiciones.ImageIndex = 36;
		this.BiRendiciones.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.BiRendiciones.Name = "BiRendiciones";
		this.BiRendiciones.SubItemsExpandWidth = 14;
		this.BiRendiciones.Tag = "107";
		this.BiRendiciones.Text = "Conciliacion Bancaria";
		this.BiRendiciones.Click += new System.EventHandler(BiRendiciones_Click);
		this.ribbonBar14.AutoOverflowEnabled = true;
		this.ribbonBar14.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar14.ContainerControlProcessDialogKey = true;
		this.ribbonBar14.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar14.DragDropSupport = true;
		this.ribbonBar14.Images = this.imageList2;
		this.ribbonBar14.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.biAperturaCajachica, this.biCajaChica, this.btnOtrosCajaChica });
		this.ribbonBar14.Location = new System.Drawing.Point(215, 0);
		this.ribbonBar14.Name = "ribbonBar14";
		this.ribbonBar14.Size = new System.Drawing.Size(205, 114);
		this.ribbonBar14.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar14.TabIndex = 3;
		this.ribbonBar14.Text = "Caja Chica";
		this.ribbonBar14.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar14.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.biAperturaCajachica.ImageIndex = 78;
		this.biAperturaCajachica.ImagePaddingHorizontal = 20;
		this.biAperturaCajachica.ImagePaddingVertical = 10;
		this.biAperturaCajachica.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAperturaCajachica.Name = "biAperturaCajachica";
		this.biAperturaCajachica.SubItemsExpandWidth = 14;
		this.biAperturaCajachica.Tag = "86";
		this.biAperturaCajachica.Text = "Aperturar   Caja Chica";
		this.biAperturaCajachica.Click += new System.EventHandler(biAperturaCajachica_Click);
		this.biCajaChica.ImageIndex = 80;
		this.biCajaChica.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biCajaChica.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
		this.biCajaChica.Name = "biCajaChica";
		this.biCajaChica.SubItemsExpandWidth = 14;
		this.biCajaChica.Tag = "117";
		this.biCajaChica.Text = "Movimientos Caja Chica";
		this.biCajaChica.Click += new System.EventHandler(biCajaChica_Click);
		this.btnOtrosCajaChica.ImageIndex = 73;
		this.btnOtrosCajaChica.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnOtrosCajaChica.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
		this.btnOtrosCajaChica.Name = "btnOtrosCajaChica";
		this.btnOtrosCajaChica.SubItemsExpandWidth = 14;
		this.btnOtrosCajaChica.Text = "Otros Gastos";
		this.btnOtrosCajaChica.Visible = false;
		this.btnOtrosCajaChica.Click += new System.EventHandler(btnOtrosCajaChica_Click);
		this.ribbonBar9.AutoOverflowEnabled = true;
		this.ribbonBar9.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar9.ContainerControlProcessDialogKey = true;
		this.ribbonBar9.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar9.DragDropSupport = true;
		this.ribbonBar9.Images = this.imageList2;
		this.ribbonBar9.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.BiAperturaCajaVentas, this.BiCajaVentasenEfectivo, this.biMovimientosCajaVentasEfectivo });
		this.ribbonBar9.Location = new System.Drawing.Point(3, 0);
		this.ribbonBar9.Name = "ribbonBar9";
		this.ribbonBar9.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.ribbonBar9.Size = new System.Drawing.Size(212, 114);
		this.ribbonBar9.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar9.TabIndex = 1;
		this.ribbonBar9.Text = "Caja Ventas";
		this.ribbonBar9.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar9.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.BiAperturaCajaVentas.ImageIndex = 60;
		this.BiAperturaCajaVentas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.BiAperturaCajaVentas.Name = "BiAperturaCajaVentas";
		this.BiAperturaCajaVentas.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlI);
		this.BiAperturaCajaVentas.SubItemsExpandWidth = 14;
		this.BiAperturaCajaVentas.Tag = "84";
		this.BiAperturaCajaVentas.Text = "Apertura Caja";
		this.BiAperturaCajaVentas.Click += new System.EventHandler(BiAperturaCajaVentas_Click);
		this.BiCajaVentasenEfectivo.ImageIndex = 59;
		this.BiCajaVentasenEfectivo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.BiCajaVentasenEfectivo.Name = "BiCajaVentasenEfectivo";
		this.BiCajaVentasenEfectivo.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlI);
		this.BiCajaVentasenEfectivo.SubItemsExpandWidth = 14;
		this.BiCajaVentasenEfectivo.Tag = "88";
		this.BiCajaVentasenEfectivo.Text = "Caja";
		this.BiCajaVentasenEfectivo.Visible = false;
		this.BiCajaVentasenEfectivo.Click += new System.EventHandler(BiCajaVentasenEfectivo_Click);
		this.biMovimientosCajaVentasEfectivo.ImageIndex = 35;
		this.biMovimientosCajaVentasEfectivo.ImagePaddingHorizontal = 20;
		this.biMovimientosCajaVentasEfectivo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biMovimientosCajaVentasEfectivo.Name = "biMovimientosCajaVentasEfectivo";
		this.biMovimientosCajaVentasEfectivo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[2] { this.buttonItem2, this.resumendiarioventas });
		this.biMovimientosCajaVentasEfectivo.SubItemsExpandWidth = 14;
		this.biMovimientosCajaVentasEfectivo.Tag = "85";
		this.biMovimientosCajaVentasEfectivo.Text = "Movimientos Caja";
		this.biMovimientosCajaVentasEfectivo.Click += new System.EventHandler(biMovimientosCajaVentasEfectivo_Click);
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.Tag = "108";
		this.buttonItem2.Text = "Listado de cajas";
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click_2);
		this.resumendiarioventas.Name = "resumendiarioventas";
		this.resumendiarioventas.Text = "Resumen Diario";
		this.resumendiarioventas.Click += new System.EventHandler(resumendiarioventas_Click);
		this.ribbonPanel10.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel10.Controls.Add(this.ribbonBar10);
		this.ribbonPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel10.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel10.Name = "ribbonPanel10";
		this.ribbonPanel10.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel10.Size = new System.Drawing.Size(923, 117);
		this.ribbonPanel10.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel10.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel10.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel10.TabIndex = 10;
		this.ribbonPanel10.Visible = false;
		this.ribbonBar10.AutoOverflowEnabled = true;
		this.ribbonBar10.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar10.ContainerControlProcessDialogKey = true;
		this.ribbonBar10.Dock = System.Windows.Forms.DockStyle.Left;
		this.ribbonBar10.DragDropSupport = true;
		this.ribbonBar10.Images = this.imageList2;
		this.ribbonBar10.Items.AddRange(new DevComponents.DotNetBar.BaseItem[4] { this.biRegistroCompras, this.biRegistroVentas, this.buttonItem1, this.btn_generar_libro_electronico });
		this.ribbonBar10.Location = new System.Drawing.Point(3, 0);
		this.ribbonBar10.Name = "ribbonBar10";
		this.ribbonBar10.Size = new System.Drawing.Size(399, 114);
		this.ribbonBar10.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar10.TabIndex = 2;
		this.ribbonBar10.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar10.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar10.TitleVisible = false;
		this.biRegistroCompras.ImageIndex = 74;
		this.biRegistroCompras.ImagePaddingHorizontal = 20;
		this.biRegistroCompras.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRegistroCompras.Name = "biRegistroCompras";
		this.biRegistroCompras.SubItemsExpandWidth = 14;
		this.biRegistroCompras.Tag = "110";
		this.biRegistroCompras.Text = "Registro de \r\nCompras";
		this.biRegistroCompras.Visible = false;
		this.biRegistroCompras.Click += new System.EventHandler(biRegistroCompras_Click);
		this.biRegistroVentas.ImageIndex = 75;
		this.biRegistroVentas.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biRegistroVentas.Name = "biRegistroVentas";
		this.biRegistroVentas.SubItemsExpandWidth = 14;
		this.biRegistroVentas.Tag = "111";
		this.biRegistroVentas.Text = "Registro de \r\nVentas";
		this.biRegistroVentas.Visible = false;
		this.buttonItem1.ImageIndex = 77;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Tag = "118";
		this.buttonItem1.Text = "Plan Contable";
		this.buttonItem1.Visible = false;
		this.buttonItem1.Click += new System.EventHandler(buttonItem1_Click_5);
		this.btn_generar_libro_electronico.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btn_generar_libro_electronico.Name = "btn_generar_libro_electronico";
		this.btn_generar_libro_electronico.SubItemsExpandWidth = 14;
		this.btn_generar_libro_electronico.Text = "Generar Libros Electronicos";
		this.btn_generar_libro_electronico.Click += new System.EventHandler(btn_generar_libro_electronico_Click);
		this.ribbonPanel8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonPanel8.Controls.Add(this.rbPrincipalDesarrollo);
		this.ribbonPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ribbonPanel8.Location = new System.Drawing.Point(0, 53);
		this.ribbonPanel8.Name = "ribbonPanel8";
		this.ribbonPanel8.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ribbonPanel8.Size = new System.Drawing.Size(1019, 117);
		this.ribbonPanel8.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel8.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel8.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonPanel8.TabIndex = 11;
		this.ribbonPanel8.Visible = false;
		this.rbPrincipalDesarrollo.AutoOverflowEnabled = true;
		this.rbPrincipalDesarrollo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.rbPrincipalDesarrollo.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbPrincipalDesarrollo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbPrincipalDesarrollo.ContainerControlProcessDialogKey = true;
		this.rbPrincipalDesarrollo.Dock = System.Windows.Forms.DockStyle.Left;
		this.rbPrincipalDesarrollo.DragDropSupport = true;
		this.rbPrincipalDesarrollo.Items.AddRange(new DevComponents.DotNetBar.BaseItem[1] { this.btnIListadoMovimientosStock });
		this.rbPrincipalDesarrollo.Location = new System.Drawing.Point(3, 0);
		this.rbPrincipalDesarrollo.Name = "rbPrincipalDesarrollo";
		this.rbPrincipalDesarrollo.Size = new System.Drawing.Size(85, 114);
		this.rbPrincipalDesarrollo.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.rbPrincipalDesarrollo.TabIndex = 0;
		this.rbPrincipalDesarrollo.Text = "ribbonBar16";
		this.rbPrincipalDesarrollo.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbPrincipalDesarrollo.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.rbPrincipalDesarrollo.TitleVisible = false;
		this.btnIListadoMovimientosStock.Name = "btnIListadoMovimientosStock";
		this.btnIListadoMovimientosStock.SubItemsExpandWidth = 14;
		this.btnIListadoMovimientosStock.Text = "Listado\r\nMovimientos\r\nde\r\nStock";
		this.btnIListadoMovimientosStock.Click += new System.EventHandler(btnListadoMovimientos_Click);
		this.biLogout.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
		this.biLogout.Checked = true;
		this.biLogout.ForeColor = System.Drawing.Color.Maroon;
		this.biLogout.Image = (System.Drawing.Image)resources.GetObject("biLogout.Image");
		this.biLogout.ImageIndex = 51;
		this.biLogout.Name = "biLogout";
		this.biLogout.Text = "Log Out";
		this.biLogout.Click += new System.EventHandler(biLogout_Click);
		this.rtVentas.Name = "rtVentas";
		this.rtVentas.Panel = this.ribbonPanel5;
		this.rtVentas.Tag = "1";
		this.rtVentas.Text = "Ventas";
		this.rtVentas.Click += new System.EventHandler(rtVentas_Click);
		this.rtCompras.Checked = true;
		this.rtCompras.Name = "rtCompras";
		this.rtCompras.Panel = this.ribbonPanel6;
		this.rtCompras.Tag = "2";
		this.rtCompras.Text = "Compras";
		this.rtCompras.Click += new System.EventHandler(rtCompras_Click);
		this.rtOperaciones.Name = "rtOperaciones";
		this.rtOperaciones.Panel = this.ribbonPanel2;
		this.rtOperaciones.Tag = "3";
		this.rtOperaciones.Text = "Almacen";
		this.rtEntidades.Name = "rtEntidades";
		this.rtEntidades.Panel = this.ribbonPanel1;
		this.rtEntidades.Tag = "4";
		this.rtEntidades.Text = "Entidades";
		this.rtReportes.Name = "rtReportes";
		this.rtReportes.Panel = this.ribbonPanel3;
		this.rtReportes.Tag = "5";
		this.rtReportes.Text = "Reportes";
		this.rtReportes.Click += new System.EventHandler(rtReportes_Click);
		this.rtAdministrador.Name = "rtAdministrador";
		this.rtAdministrador.Panel = this.ribbonPanel4;
		this.rtAdministrador.Tag = "6";
		this.rtAdministrador.Text = "Administrador";
		this.rtAdministrador.Click += new System.EventHandler(rtAdministrador_Click);
		this.rbCaja.Name = "rbCaja";
		this.rbCaja.Panel = this.ribbonPanel7;
		this.rbCaja.Tag = "83";
		this.rbCaja.Text = "CajaBancos";
		this.rbCaja.Click += new System.EventHandler(rbCaja_Click);
		this.Libros.Name = "Libros";
		this.Libros.Panel = this.ribbonPanel10;
		this.Libros.Tag = "109";
		this.Libros.Text = "LibrosElectronicos";
		this.Libros.Visible = false;
		this.rtiDesarrollador.Name = "rtiDesarrollador";
		this.rtiDesarrollador.Panel = this.ribbonPanel8;
		this.rtiDesarrollador.Text = "Desarrollador";
		this.rbLibrosElectronicos.Name = "rbLibrosElectronicos";
		this.rbLibrosElectronicos.Visible = false;
		this.liTipodeCambio.BackColor = System.Drawing.Color.Transparent;
		this.liTipodeCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.liTipodeCambio.ForeColor = System.Drawing.Color.Maroon;
		this.liTipodeCambio.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
		this.liTipodeCambio.Name = "liTipodeCambio";
		this.liTipodeCambio.PaddingBottom = 5;
		this.liTipodeCambio.SingleLineColor = System.Drawing.SystemColors.WindowFrame;
		this.liTipodeCambio.Text = "Tipo de Cambio";
		this.comboItem1.ForeColor = System.Drawing.Color.White;
		this.comboItem1.Text = "comboItem1";
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.sUsuario, this.sEmpresa, this.sAlmacen, this.sIP, this.sCaja });
		this.statusStrip1.Location = new System.Drawing.Point(5, 422);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.Size = new System.Drawing.Size(855, 24);
		this.statusStrip1.TabIndex = 8;
		this.statusStrip1.Text = "statusStrip1";
		this.sUsuario.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.sUsuario.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
		this.sUsuario.Name = "sUsuario";
		this.sUsuario.Size = new System.Drawing.Size(168, 19);
		this.sUsuario.Spring = true;
		this.sUsuario.Text = "Usuario";
		this.sEmpresa.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.sEmpresa.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
		this.sEmpresa.Name = "sEmpresa";
		this.sEmpresa.Size = new System.Drawing.Size(168, 19);
		this.sEmpresa.Spring = true;
		this.sEmpresa.Text = "Empresa";
		this.sAlmacen.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.sAlmacen.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
		this.sAlmacen.Name = "sAlmacen";
		this.sAlmacen.Size = new System.Drawing.Size(168, 19);
		this.sAlmacen.Spring = true;
		this.sAlmacen.Text = "Almacen";
		this.sIP.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.All;
		this.sIP.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
		this.sIP.Name = "sIP";
		this.sIP.Size = new System.Drawing.Size(168, 19);
		this.sIP.Spring = true;
		this.sIP.Text = "IP";
		this.sCaja.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
		this.sCaja.Name = "sCaja";
		this.sCaja.Size = new System.Drawing.Size(168, 19);
		this.sCaja.Spring = true;
		this.sCaja.Text = "Caja: ";
		this.tabStrip1.AutoHideSystemBox = true;
		this.tabStrip1.AutoSelectAttachedControl = true;
		this.tabStrip1.CanReorderTabs = true;
		this.tabStrip1.CloseButtonOnTabsAlwaysDisplayed = false;
		this.tabStrip1.CloseButtonVisible = false;
		this.tabStrip1.Cursor = System.Windows.Forms.Cursors.Default;
		this.tabStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.tabStrip1.Location = new System.Drawing.Point(5, 399);
		this.tabStrip1.MdiForm = this;
		this.tabStrip1.MdiNoFormActivateFlicker = false;
		this.tabStrip1.MdiTabbedDocuments = true;
		this.tabStrip1.Name = "tabStrip1";
		this.tabStrip1.SelectedTab = null;
		this.tabStrip1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.tabStrip1.Size = new System.Drawing.Size(855, 23);
		this.tabStrip1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
		this.tabStrip1.TabIndex = 9;
		this.tabStrip1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
		this.tabStrip1.Text = "tabStrip1";
		this.tabStrip1.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(tabStrip1_SelectedTabChanged);
		this.saveFileDialog1.FileName = "sigefa.sql";
		this.saveFileDialog1.Title = "Exportar backup";
		this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(saveFileDialog1_FileOk);
		this.openFileDialog1.FileName = "openFileDialog1";
		this.openFileDialog1.Title = "Importar BD";
		this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);
		this.buttonItem11.ImageIndex = 41;
		this.buttonItem11.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem11.Name = "buttonItem11";
		this.buttonItem11.SubItemsExpandWidth = 14;
		this.buttonItem11.Tag = "48";
		this.buttonItem11.Text = "Reportes";
		this.buttonItem10.Name = "buttonItem10";
		this.buttonItem10.SubItemsExpandWidth = 14;
		this.buttonItem10.Text = "Órdenes de Venta";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		base.ClientSize = new System.Drawing.Size(865, 448);
		base.Controls.Add(this.tabStrip1);
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.ribbonControl1);
		this.EnableGlass = false;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.IsMdiContainer = true;
		base.KeyPreview = true;
		this.MinimumSize = new System.Drawing.Size(650, 449);
		base.Name = "mdi_Menu";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "++++";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(mdi_Menu_FormClosed);
		base.Load += new System.EventHandler(mdi_Menu_Load);
		base.MdiChildActivate += new System.EventHandler(mdi_Menu_MdiChildActivate);
		base.Shown += new System.EventHandler(mdi_Menu_Shown);
		base.SizeChanged += new System.EventHandler(mdi_Menu_SizeChanged);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(mdi_Menu_KeyDownAsync);
		this.ribbonControl1.ResumeLayout(false);
		this.ribbonControl1.PerformLayout();
		this.ribbonPanel1.ResumeLayout(false);
		this.ribbonPanel2.ResumeLayout(false);
		this.ribbonPanel6.ResumeLayout(false);
		this.ribbonPanel5.ResumeLayout(false);
		this.ribbonPanel4.ResumeLayout(false);
		this.ribbonPanel3.ResumeLayout(false);
		this.ribbonPanel7.ResumeLayout(false);
		this.ribbonPanel10.ResumeLayout(false);
		this.ribbonPanel8.ResumeLayout(false);
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
