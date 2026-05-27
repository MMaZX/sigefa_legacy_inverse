using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using Newtonsoft.Json;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmGuiaRemisionCompra : Form
{
	public DataTable dataGeneradaDeOC = null;

	public DataTable dataGRC = new DataTable();

	public DataTable dataDetOC = new DataTable();

	public int codUltimaGuiaRemisionGenerada = 0;

	public int codOrdenCompraGenerada = 0;

	public int CodEmpresaTransporte = -1;

	public int CodProv = 0;

	public bool Generada = false;

	public bool Editar = false;

	private List<clsDetalleGuiaRemisionCompra> listadoDetalle = new List<clsDetalleGuiaRemisionCompra>();

	public List<clsDetalleGuiaRemisionCompra> listadoProdNoAtendidos = new List<clsDetalleGuiaRemisionCompra>();

	public int codProdNoAtendido = -1;

	public clsDetalleGuiaRemisionCompra prodNoAtendido = null;

	public int codGuiaRemisionCompraAEditar = 0;

	public int codmonedaoc;

	public clsDetalleGuiaRemisionCompra detallegrc_añadir = null;

	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	public int OrdenOC = 0;

	private clsAdmNotaCreditoCompra admnccompra = new clsAdmNotaCreditoCompra();

	private clsNotaCredito nota_compra = null;

	private clsAdmFacturaVenta admventa = new clsAdmFacturaVenta();

	private clsFacturaVenta fact_venta = null;

	private clsAdmPedido admpedido = new clsAdmPedido();

	private clsPedido pedido = null;

	private clsAdmNotaIngreso admnotaingreso = new clsAdmNotaIngreso();

	private clsNotaIngreso fact_flete = null;

	private clsNotaIngreso fact_compra = null;

	private DataTable data = new DataTable();

	private clsGuiaRemision grc = new clsGuiaRemision();

	private clsAdmGuiaRemisionCompra admgrc = new clsAdmGuiaRemisionCompra();

	private clsOrdenCompra Ord = new clsOrdenCompra();

	private clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();

	private clsProveedor Prov = new clsProveedor();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmConductor AdmCond = new clsAdmConductor();

	private clsConductor cond = new clsConductor();

	private clsAdmVehiculoTransporte AdmVeh = new clsAdmVehiculoTransporte();

	private clsVehiculoTransporte veh = new clsVehiculoTransporte();

	private clsAdmProveedor AdmET = new clsAdmProveedor();

	private clsProveedor empT = new clsProveedor();

	private BindingSource enlace_datos = new BindingSource();

	private int in_fila_cms_dgv = -1;

	private int fila_cambio_estado = -1;

	private int cod_prod_cambio_estado = -1;

	private int cod_estado_cambio = -1;

	private bool guardar = false;

	private bool valueCheckTransportista = false;

	public int ctdaddocrelacionados = -1;

	private clsAdmCliente AdmCli = new clsAdmCliente();

	internal bool nuevoMetodo = false;

	private int ultima_fila_seleccionada = -1;

	private double cantidad_previa = double.NaN;

	private TextBox txtedit = new TextBox();

	private clsValidar ok = new clsValidar();

	private bool agregado_ultimo = false;

	private clsTipoDocumento tipoDoc = new clsTipoDocumento();

	private clsAdmTipoDocumento admtipoDoc = new clsAdmTipoDocumento();

	private clsAdmTipoCambio tc = new clsAdmTipoCambio();

	private Color fondo_celda;

	internal int valFactFlete;

	public bool mostrarMensajeGuardado = true;

	private IContainer components = null;

	private GroupBox groupBox1;

	public DataGridView dgvdetalleguiaremisioncompra;

	private GroupBox btnAgregarProdPromocion;

	private GroupBox gbopcionesgrgenerada;

	public Button btnAgregarProdNoSolicitado;

	public Button btnAgregarProductoPromocion;

	private DateTimePicker dtfechaingresoalmacen;

	private Label label21;

	public Label label20;

	public TextBox txtnumeroOc;

	private Label label5;

	private TextBox txtNumero;

	private ComboBox cmbMotivo;

	private Label label18;

	private DateTimePicker dtpFechaTransporte;

	private Label label17;

	private GroupBox gbTransporte;

	public TextBox txtDireccionTransporte;

	private Label label16;

	public TextBox txtRazonSocialTransporte;

	private Label label14;

	private Label label13;

	public TextBox txtRUCTransporte;

	private GroupBox groupBox4;

	private ComboBox cmbConductor;

	private ComboBox cmbVehiculos;

	private TextBox txtMarcaVehiculo;

	private TextBox txtLicencia;

	private Label label12;

	private TextBox txtConstancia;

	private Label label11;

	private Label label10;

	private Label label6;

	private Label label3;

	public TextBox txtDireccionProveedor;

	private Label label4;

	public TextBox txtRazonSocialProveedor;

	public TextBox txtRucProveedor;

	public Label label15;

	private Button btnDetalle;

	private TextBox txtComentario;

	private Label label9;

	private Label label7;

	private DateTimePicker dtpFecha;

	private Label label1;

	public Button btnRecargarConductor;

	public Button btnRecargarAutos;

	public Button btnSalir;

	public Button btnGuardar;

	private ToolTip tTMensajeInformacion;

	private ContextMenuStrip cmsFilaDgv;

	private ToolStripMenuItem eliminarToolStripMenuItem;

	public Button btnLIstadoProductosNoAtendidos;

	public Button btngenerarFactura;

	public Label label2;

	public TextBox txtFacturaCompra;

	public Button btnGenerarFacturaVenta;

	private GroupBox groupBox2;

	private TextBox txtTotalFleteConIgv;

	private TextBox txtTotalFleteSinIgv;

	private Label label19;

	private Label label8;

	private GroupBox groupBox3;

	private Button btnDsctotrans;

	private TextBox ctdadaEstado;

	private Label lblDetalleProdSeleccionado;

	private Button btnNoSolicAceptado;

	private Button btnDarEstadoPromocion;

	private Button btnDevProv;

	private Label label22;

	public Button btnGenerarNotaCredito;

	public Button btnGenerarFacturaFlete;

	public Button btnModificarFlete;

	private Label label23;

	private ComboBox cmbtipoflete;

	public Label lblnotacredito;

	public TextBox txtnotacredito;

	public Label lblfactflete;

	public TextBox txtfactventa;

	public Label lblfactventa;

	public TextBox txtfactflete;

	private DataGridViewTextBoxColumn colItem;

	private DataGridViewTextBoxColumn colCodNCC;

	private DataGridViewTextBoxColumn colNC;

	public Button btncrearproduct;

	public DataGridView dgvNCCGeneradas;

	public Label label24;

	private DataGridViewTextBoxColumn colCodDetalleGR;

	private DataGridViewTextBoxColumn colCodDetalleOrdenCompra;

	private DataGridViewTextBoxColumn colCodOrdenCompra;

	private DataGridViewTextBoxColumn colCodProducto;

	private DataGridViewTextBoxColumn colReferencia;

	private DataGridViewTextBoxColumn colDescripcion;

	private DataGridViewTextBoxColumn colMoneda;

	private DataGridViewTextBoxColumn colCodUnidad;

	private DataGridViewTextBoxColumn colUnidad;

	private DataGridViewTextBoxColumn colCantidad;

	private DataGridViewTextBoxColumn colCantidadRespaldo;

	private DataGridViewTextBoxColumn colfechaingreso;

	private DataGridViewTextBoxColumn colcoduser;

	private DataGridViewTextBoxColumn colfecharegistro;

	private DataGridViewTextBoxColumn colcodDocumentoRelacionado;

	private DataGridViewTextBoxColumn coletiqueta;

	private DataGridViewTextBoxColumn codEstado;

	private DataGridViewTextBoxColumn colFleteUnitario;

	private DataGridViewTextBoxColumn colFleteSubtotal;

	public void limpiaGBAsigancionDeEstado()
	{
		lblDetalleProdSeleccionado.Text = "Seleccione un item de la lista";
		ctdadaEstado.Text = "";
		fila_cambio_estado = -1;
	}

	public frmGuiaRemisionCompra()
	{
		InitializeComponent();
	}

	public void CargaDetalle()
	{
		data = AdmOrden.CargaDetalle(Convert.ToInt32(OrdenOC));
	}

	private void frmGuiaRemisionCompra_Load(object sender, EventArgs e)
	{
		CargaVehiculosTransporte();
		CargaConductores();
		EventArgs eee = new EventArgs();
		cmbConductor_SelectionChangeCommitted(cmbConductor, eee);
		cmbVehiculos_SelectionChangeCommitted(cmbVehiculos, eee);
		permitirEditarDGV();
		CargaDetalle();
		if (Generada)
		{
			valueCheckTransportista = true;
			RellenarFormularioConGuiaRemisionGeneradaPorOrdenDeCompra();
			if (CodEmpresaTransporte != -1)
			{
				empT.CodProveedor = CodEmpresaTransporte;
				CargaEmpresaTransporte();
			}
			cmbtipoflete.SelectedIndex = valFactFlete;
			GroupBox groupBox = gbTransporte;
			bool visible = (btnGenerarFacturaFlete.Visible = valFactFlete == 2);
			groupBox.Visible = visible;
			if (!nuevoMetodo)
			{
				listadoDetalle = ConvertirDataEnLista(dataGeneradaDeOC);
			}
			dgvNCCGeneradas.Visible = false;
		}
		else if (Editar)
		{
			valueCheckTransportista = true;
			grc = admgrc.CargaGuiaRemision(codGuiaRemisionCompraAEditar);
			dataGRC = admgrc.CargaDetalle(Convert.ToInt32(grc.CodGuiaRemision));
			Ord = AdmOrd.CargaOrdenCompra(grc.ICodOrdenCompra);
			listadoDetalle = ConvertirDataEnLista(dataGRC);
			enlace_datos.DataSource = dataGRC;
			dgvdetalleguiaremisioncompra.DataSource = enlace_datos;
			btnGenerarFacturaFlete.Visible = admgrc.getEstadoDocumentoRelacionado(Convert.ToInt32(grc.CodGuiaRemision), 4) == 2;
			btngenerarFactura.Visible = admgrc.getEstadoDocumentoRelacionado(Convert.ToInt32(grc.CodGuiaRemision), 1) == 2;
			btnGenerarFacturaVenta.Visible = admgrc.getEstadoDocumentoRelacionado(Convert.ToInt32(grc.CodGuiaRemision), 2) == 2;
			btnGenerarNotaCredito.Visible = admgrc.getEstadoDocumentoRelacionado(Convert.ToInt32(grc.CodGuiaRemision), 3) == 2;
			DataTable auxdata = admgrc.cargarNotasCreditoCompraGeneradas(Convert.ToInt32(grc.CodGuiaRemision));
			dgvNCCGeneradas.Visible = auxdata.Rows.Count > 0;
			if (auxdata.Rows.Count > 0)
			{
				dgvNCCGeneradas.DataSource = auxdata;
				btnGenerarNotaCredito.Visible = false;
			}
			cargarDatosDeGRC();
		}
		else
		{
			inicializaDataConDGV();
			gbopcionesgrgenerada.Visible = false;
			btnDetalle.Visible = true;
			btnDetalle.Enabled = true;
		}
		darFormatoADGV();
	}

	private void cargarDatosDeGRC()
	{
		txtnumeroOc.Text = Ord.Serie + "-" + Ord.NumDoc.ToString().PadLeft(8, '0');
		txtNumero.Text = grc.NumDoc;
		clsAdmNotaIngreso admni = new clsAdmNotaIngreso();
		if (grc.CodFactura != 0)
		{
			txtFacturaCompra.Text = admni.CargaNotaIngreso(grc.CodFactura).NumDoc;
		}
		cmbMotivo.SelectedIndex = 2;
		CodProv = grc.CodProveedor;
		CargaProveedor();
		cmbtipoflete.SelectedIndex = grc.OpcionFlete;
		if (cmbtipoflete.SelectedIndex == 0)
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
		if (cmbtipoflete.SelectedIndex == 1)
		{
			txtTotalFleteConIgv.Text = grc.FleteConIgv.ToString("#.00");
			txtTotalFleteSinIgv.Text = grc.FleteSinIgv.ToString("#.00");
		}
		if (cmbtipoflete.SelectedIndex == 2)
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
		gbTransporte.Visible = cmbtipoflete.SelectedIndex == 2;
		empT.CodProveedor = grc.CodEmpresaTransporte;
		CodEmpresaTransporte = empT.CodProveedor;
		CargaEmpresaTransporte();
		dtfechaingresoalmacen.Value = grc.fechaingresoalmacen;
		dtpFecha.Value = grc.FechaEmision;
		dtpFechaTransporte.Value = grc.FechaTraslado;
		txtComentario.Text = grc.Comentario;
		string info_ov = admgrc.getInfoDocumentoRelacionadoGRC(Convert.ToInt32(grc.CodGuiaRemision), 2);
		if (int.TryParse(info_ov, out var codov))
		{
			pedido = admpedido.CargaPedido(codov);
			if (pedido.Pendiente == 0)
			{
				fact_venta = admventa.CargaFacturaVentaSegunOV(Convert.ToInt32(pedido.CodPedido));
				txtfactventa.Text = fact_venta.SiglaDocumento + fact_venta.Serie + "-" + fact_venta.NumDoc;
			}
			else
			{
				txtfactventa.Text = pedido.SiglaDocumento + pedido.SerieDoc + "-" + pedido.Numeracion;
			}
		}
		else
		{
			txtfactventa.Text = info_ov;
		}
		string info_ncc = admgrc.getInfoDocumentoRelacionadoGRC(Convert.ToInt32(grc.CodGuiaRemision), 3);
		if (int.TryParse(info_ncc, out var codncc))
		{
			nota_compra = admnccompra.cargaNotaCredito(codncc);
			txtnotacredito.Text = nota_compra.SiglaDocumento + " " + nota_compra.Serie + "-" + nota_compra.DocumentoNotaCredito;
		}
		else
		{
			txtnotacredito.Text = info_ncc;
		}
		string info_ff = admgrc.getInfoDocumentoRelacionadoGRC(Convert.ToInt32(grc.CodGuiaRemision), 4);
		if (int.TryParse(info_ff, out var codff))
		{
			fact_flete = admnotaingreso.CargaNotaIngreso(codff);
			txtfactflete.Text = fact_flete.SiglaDocumento + fact_flete.Serie + "-" + fact_flete.NumDoc;
		}
		else
		{
			txtfactflete.Text = info_ff;
		}
	}

	private void permitirEditarDGV()
	{
		foreach (DataGridViewColumn col in dgvdetalleguiaremisioncompra.Columns)
		{
			col.ReadOnly = true;
			if (col.Name == colCantidad.Name)
			{
				col.ReadOnly = false;
			}
		}
	}

	private List<clsDetalleGuiaRemisionCompra> ConvertirDataEnLista(DataTable dataGeneradaDeOC)
	{
		List<clsDetalleGuiaRemisionCompra> lista = new List<clsDetalleGuiaRemisionCompra>();
		if (dataGeneradaDeOC.Rows.Count > 0)
		{
			try
			{
				foreach (DataRow fila in dataGeneradaDeOC.Rows)
				{
					clsDetalleGuiaRemisionCompra nuevo = new clsDetalleGuiaRemisionCompra();
					nuevo = getDetalleGRDeFilaDT(fila);
					lista.Add(nuevo);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Al Digitalizar Detalle de Guia de Remision de Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return null;
			}
		}
		return lista;
	}

	private clsDetalleGuiaRemisionCompra getDetalleGRDeFilaDT(DataRow fila)
	{
		clsDetalleGuiaRemisionCompra nuevo = new clsDetalleGuiaRemisionCompra();
		nuevo.ICodProducto = Convert.ToInt32(fila.Field<object>(colCodProducto.DataPropertyName));
		nuevo.ICodOrdenCOmpra = codOrdenCompraGenerada;
		nuevo.ICodDetalleOrdenCOmpra = Convert.ToInt32(fila.Field<object>(colCodDetalleOrdenCompra.DataPropertyName));
		object codDetalleGuiaRemisionCompra = fila.Field<object>(colCodDetalleGR.DataPropertyName);
		nuevo.ICodDetalleGuiaRemisionCompra = ((codDetalleGuiaRemisionCompra != "") ? Convert.ToInt32(codDetalleGuiaRemisionCompra) : 0);
		nuevo.IcodMoneda = Convert.ToInt32(fila.Field<object>(colMoneda.DataPropertyName));
		nuevo.IUnidadIngresada = Convert.ToInt32(fila.Field<object>(colCodUnidad.DataPropertyName));
		nuevo.DCantidad = Convert.ToDouble(fila.Field<object>(colCantidad.DataPropertyName) ?? ((object)double.NaN));
		nuevo.DCantidadRespaldo = Convert.ToDouble(fila.Field<object>(colCantidadRespaldo.DataPropertyName) ?? ((object)double.NaN));
		nuevo.FFechaIngreso = Convert.ToDateTime(fila.Field<object>(colfechaingreso.DataPropertyName));
		nuevo.IEstado = Convert.ToInt32(fila.Field<object>(codEstado.DataPropertyName));
		nuevo.ICOdUser = frmLogin.iCodUser;
		nuevo.FFechaRegistro = DateTime.Now;
		nuevo.SReferencia = fila.Field<object>(colReferencia.DataPropertyName).ToString();
		nuevo.SDescripcion = fila.Field<object>(colDescripcion.DataPropertyName).ToString();
		nuevo.SUnidad = fila.Field<object>(colUnidad.DataPropertyName).ToString();
		return nuevo;
	}

	private void inicializaDataConDGV()
	{
		foreach (DataGridViewColumn col in dgvdetalleguiaremisioncompra.Columns)
		{
			if (col.DataPropertyName != "")
			{
				dataGRC.Columns.Add(col.DataPropertyName);
			}
		}
	}

	private void RellenarFormularioConGuiaRemisionGeneradaPorOrdenDeCompra()
	{
		try
		{
			Ord = AdmOrd.CargaOrdenCompra(codOrdenCompraGenerada);
			txtnumeroOc.Text = Ord.CodOrdenCompra.ToString().PadLeft(11, '0');
			cmbMotivo.SelectedIndex = 2;
			CodProv = Ord.CodProveedor;
			CargaProveedor();
			if (nuevoMetodo)
			{
				conviertiendoDGVaData();
			}
			else
			{
				dataGRC = dataGeneradaDeOC;
				enlace_datos.DataSource = dataGRC;
				dgvdetalleguiaremisioncompra.DataSource = enlace_datos;
			}
			rellenarTotalesFlete();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void conviertiendoDGVaData()
	{
		dgvdetalleguiaremisioncompra.AutoGenerateColumns = true;
		foreach (DataGridViewColumn col in dgvdetalleguiaremisioncompra.Columns)
		{
			if (col.DataPropertyName != "")
			{
				dataGRC.Columns.Add(col.DataPropertyName);
			}
		}
		enlace_datos.DataSource = dataGRC;
		dgvdetalleguiaremisioncompra.DataSource = enlace_datos;
	}

	private void rellenarTotalesFlete()
	{
		double total_f_s_i = 0.0;
		double total_f_c_i = 0.0;
		foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
		{
			double flete_s_i = AdmPro.obtenerFleteDeProducto(Convert.ToDouble(fila.Cells[colCodProducto.Name].Value), 1, Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value), Convert.ToDouble(fila.Cells[colCantidad.Name].Value));
			double flete_c_i = AdmPro.obtenerFleteDeProducto(Convert.ToDouble(fila.Cells[colCodProducto.Name].Value), 2, Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value), Convert.ToDouble(fila.Cells[colCantidad.Name].Value));
			flete_s_i = Convert.ToDouble((flete_s_i == double.NaN) ? 0.0 : flete_s_i);
			flete_c_i = Convert.ToDouble((flete_c_i == double.NaN) ? 0.0 : flete_c_i);
			total_f_s_i += flete_s_i;
			total_f_c_i += flete_c_i;
			double cantidad = Convert.ToDouble(fila.Cells[colCantidad.Name].Value);
			fila.Cells[colFleteUnitario.Name].Value = (flete_s_i / ((cantidad == 0.0) ? 1.0 : cantidad)).ToString("0.00");
			fila.Cells[colFleteSubtotal.Name].Value = flete_s_i.ToString("0.00");
		}
		txtTotalFleteConIgv.Text = total_f_c_i.ToString("##0.00");
		txtTotalFleteSinIgv.Text = total_f_s_i.ToString("##0.00");
	}

	private void CargaVehiculosTransporte()
	{
		cmbVehiculos.DataSource = AdmVeh.CargaVehiculoTransportes();
		cmbVehiculos.DisplayMember = "placa";
		cmbVehiculos.ValueMember = "codVehiculoTransporte";
	}

	private void CargaConductores()
	{
		cmbConductor.DataSource = AdmCond.CargaConductores();
		cmbConductor.DisplayMember = "nombre";
		cmbConductor.ValueMember = "codConductor";
	}

	private void txtCodProveedor_KeyDown(object sender, KeyEventArgs e)
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
		form.Procede = 11;
		form.ShowDialog();
		if (CodProv != 0)
		{
			CargaProveedor();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaProveedor()
	{
		if (CodProv != 0)
		{
			Prov = AdmProv.MuestraProveedor(CodProv);
			txtRucProveedor.Text = Prov.Ruc;
			txtDireccionProveedor.Text = Prov.Direccion;
			txtRazonSocialProveedor.Text = Prov.RazonSocial;
		}
		else
		{
			txtRucProveedor.Text = "";
			txtDireccionProveedor.Text = "";
			txtRazonSocialProveedor.Text = "";
		}
	}

	private void cmbConductor_SelectionChangeCommitted(object sender, EventArgs e)
	{
		int adfs = listadoDetalle.Count;
		cond = AdmCond.MuestraConductor(Convert.ToInt32(cmbConductor.SelectedValue));
		if (cond != null)
		{
			txtLicencia.Text = cond.Licencia;
		}
		else
		{
			txtLicencia.Text = "";
		}
	}

	private void cmbVehiculos_SelectionChangeCommitted(object sender, EventArgs e)
	{
		veh = AdmVeh.MuestraVehiculoTransporte(Convert.ToInt32(cmbVehiculos.SelectedValue));
		if (veh != null)
		{
			txtMarcaVehiculo.Text = veh.Marca;
			txtConstancia.Text = veh.ConstanciaInscripcion;
		}
		else
		{
			txtMarcaVehiculo.Text = "";
			txtConstancia.Text = "";
		}
	}

	private void txtRUCTransporte_KeyDown(object sender, KeyEventArgs e)
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
		form.Procede = 12;
		form.ventana = this;
		if (form.ShowDialog() == DialogResult.OK)
		{
			CodEmpresaTransporte = form.GetCodProveeder();
			CargaEmpresaTransporte();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaEmpresaTransporte()
	{
		empT = AdmET.MuestraProveedor(CodEmpresaTransporte);
		if (empT != null)
		{
			txtRUCTransporte.Text = empT.Ruc;
			txtRazonSocialTransporte.Text = empT.RazonSocial;
			txtDireccionTransporte.Text = empT.Direccion;
		}
		else
		{
			txtRUCTransporte.Text = "";
			txtRazonSocialTransporte.Text = "";
			txtDireccionTransporte.Text = "";
		}
	}

	private void txtFactura_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtRucProveedor.Text != "")
		{
			if (e.KeyCode != Keys.F1)
			{
			}
		}
		else
		{
			MessageBox.Show("Debe elegir un Cliente", "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnRecargarAutos_Click(object sender, EventArgs e)
	{
		CargaVehiculosTransporte();
	}

	private void btnRecargarConductor_Click(object sender, EventArgs e)
	{
		CargaConductores();
	}

	private void btnAgregarProdPromocion_Enter(object sender, EventArgs e)
	{
		int asd = fila_cambio_estado;
		int dsa = cod_prod_cambio_estado;
		int sda = cod_estado_cambio;
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDetalleIngreso"] != null)
		{
			Application.OpenForms["frmDetalleIngreso"].Activate();
			return;
		}
		frmDetalleIngreso form = new frmDetalleIngreso();
		form.Procede = 90;
		form.Proceso = 1;
		form.ShowDialog();
		if (detallegrc_añadir != null)
		{
			agregarDetalleGRCaData(detallegrc_añadir);
			detallegrc_añadir = null;
		}
		else
		{
			MessageBox.Show("Ocurrio un error al registrar item seleccionado", "Ingreso de producto dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		int a = 1;
		var docu_relacionado = new
		{
			TipoDocumento = 12,
			CodDocumento = 12548,
			Anulado = 0
		};
		string hola = JsonConvert.SerializeObject(docu_relacionado);
		var j_doc = new { };
		string prueba = JsonConvert.SerializeObject(j_doc);
		hola = hola ?? "";
		object haber = JsonConvert.DeserializeObject(hola);
		object haber2 = JsonConvert.DeserializeObject(prueba);
		int u = 8;
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (dtpFecha.Value != dtpFecha.MinDate && dtfechaingresoalmacen.Value != dtfechaingresoalmacen.MinDate)
		{
			if (cmbtipoflete.SelectedIndex == 2 && !(txtRUCTransporte.Text != ""))
			{
				MessageBox.Show("Falta Seleccionar Empresa De Transporte", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRUCTransporte.Focus();
			}
			else if (txtNumero.Text != "")
			{
				llenarGuiaRemision();
				if (!verificaGuardar())
				{
					return;
				}
				bool rpta = true;
				using (TransactionScope Scope = new TransactionScope())
				{
					try
					{
						rpta = ((!Editar) ? admgrc.insert(grc) : admgrc.update(grc));
						if (rpta)
						{
							if (Generada)
							{
								AdmOrd.registrarModificacionDeOC(Ord.CodOrdenCompra, frmLogin.iCodUser, DateTime.Now);
							}
							List<clsDetalleGuiaRemisionCompra> listadoNuevo = ConvertirDataEnLista(dataGRC);
							if (Editar)
							{
								rpta = admgrc.deletedetalledegrc(Convert.ToInt32(grc.CodGuiaRemision));
							}
							if (rpta)
							{
								foreach (clsDetalleGuiaRemisionCompra item in listadoNuevo)
								{
									item.ICodGuiaRemisionCOmpra = Convert.ToInt32(grc.CodGuiaRemision);
									rpta = admgrc.insertdetalle(item);
									if (!rpta)
									{
										break;
									}
								}
								if (rpta)
								{
									if (Editar)
									{
									}
									if (rpta)
									{
										rpta = AdmOrd.actualizaCantidadPendienteordenCompra(grc.ICodOrdenCompra);
									}
								}
							}
						}
						if (!rpta)
						{
							Transaction.Current.Rollback();
							Scope.Dispose();
							MessageBox.Show("No se pudo guardar la guia de remision por problemas internos", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							Scope.Complete();
							Scope.Dispose();
							if (mostrarMensajeGuardado)
							{
								foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
								{
									string codProd = Convert.ToString(fila.Cells["colCodProducto"].Value);
									string CodEstado = Convert.ToString(fila.Cells["codEstado"].Value);
									DataTable dt1 = AdmOrden.CargaDetalle(Convert.ToInt32(grc.ICodOrdenCompra));
									if (Enumerable.Any<DataRow>(dt1.Rows.Cast<DataRow>(), (Func<DataRow, bool>)((DataRow x) => Convert.ToString(x["codProducto"]) == codProd && Convert.ToString(x["estado_etique"]) == CodEstado)))
									{
										if (CodEstado == Convert.ToString(1))
										{
											updateProductos();
										}
										if (CodEstado == Convert.ToString(3))
										{
											updateProductosPromocion();
										}
										if (CodEstado == Convert.ToString(6))
										{
											updateProductosNosolicitados_Aceptados();
										}
									}
									else
									{
										if (CodEstado == Convert.ToString(3))
										{
											guardarProductosPromocion();
										}
										if (CodEstado == Convert.ToString(6))
										{
											guardarProductosNosolicitados_Aceptados();
										}
									}
								}
								MessageBox.Show("Guia De Remision de Compra Guardada Con Exito", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								mostrarMensajeGuardado = true;
							}
						}
					}
					catch (Exception ex)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						rpta = false;
						MessageBox.Show(ex.Message, "Error Guardar Guia Remision Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				if (!rpta)
				{
					return;
				}
				DataTable noatend = AdmOrd.generarGuiaRemisionOrdenCompra(grc.ICodOrdenCompra);
				if (noatend != null)
				{
					if (noatend.Rows.Count > 0)
					{
						AdmOrd.actualizaestadocabeceraorden(Ord.CodOrdenCompra, 2);
						AdmOrd.registrarModificacionDeOC(Ord.CodOrdenCompra, frmLogin.iCodUser, DateTime.Now);
					}
					else
					{
						AdmOrd.actualizaestadocabeceraorden(Ord.CodOrdenCompra, 3);
						AdmOrd.registrarModificacionDeOC(Ord.CodOrdenCompra, frmLogin.iCodUser, DateTime.Now);
					}
				}
				else
				{
					AdmOrd.actualizaestadocabeceraorden(Ord.CodOrdenCompra, 3);
					AdmOrd.registrarModificacionDeOC(Ord.CodOrdenCompra, frmLogin.iCodUser, DateTime.Now);
				}
				recargarPagina();
			}
			else
			{
				MessageBox.Show("indique un numero de guia de remision", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtNumero.Focus();
			}
		}
		else
		{
			MessageBox.Show("Falta Indicar Las Fechas del documento", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void Eliminar_detalleOC_Promocion()
	{
		clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
		bool band = true;
		using TransactionScope Scope = new TransactionScope();
		Scope.Complete();
		Scope.Dispose();
	}

	private void guardarProductosPromocion()
	{
		clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
		string ordenes = "";
		bool band_m = false;
		List<clsDetalleOrdenCompra> detalle = obtenerDetalleGuiaRemision_InsertarPromocion();
		if (detalle.Count <= 0)
		{
			return;
		}
		bool band = true;
		using TransactionScope Scope = new TransactionScope();
		foreach (clsDetalleOrdenCompra det in detalle)
		{
			clsAdmOrdenCompra admOrdenCompra = new clsAdmOrdenCompra();
			admOrdenCompra.ActualizaOrdenCompra_Estado(Convert.ToInt32(grc.ICodOrdenCompra), 1);
			band = admOrdCompra.insertdetalle_1(det);
			if (!band)
			{
				break;
			}
		}
		if (!band)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
		}
		else
		{
			band_m = true;
			Scope.Complete();
			Scope.Dispose();
		}
	}

	private void updateProductos()
	{
		clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
		string ordenes = "";
		bool band_m = false;
		List<clsDetalleOrdenCompra> detalle = obtenerDetalleGuiaRemision_Actualizar();
		if (detalle.Count <= 0)
		{
			return;
		}
		bool band = true;
		using TransactionScope Scope = new TransactionScope();
		foreach (clsDetalleOrdenCompra det in detalle)
		{
			band = admOrdCompra.updatedetalle_1(det);
			if (!band)
			{
				break;
			}
		}
		if (!band)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
		}
		else
		{
			band_m = true;
			Scope.Complete();
			Scope.Dispose();
		}
	}

	private void updateProductosPromocion()
	{
		clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
		string ordenes = "";
		bool band_m = false;
		List<clsDetalleOrdenCompra> detalle = obtenerDetalleGuiaRemision_ActualizarPromocion();
		if (detalle.Count <= 0)
		{
			return;
		}
		bool band = true;
		using TransactionScope Scope = new TransactionScope();
		foreach (clsDetalleOrdenCompra det in detalle)
		{
			band = admOrdCompra.updatedetalle_1(det);
			if (!band)
			{
				break;
			}
		}
		if (!band)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
		}
		else
		{
			band_m = true;
			Scope.Complete();
			Scope.Dispose();
		}
	}

	private void updateProductosNosolicitados_Aceptados()
	{
		clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
		string ordenes = "";
		bool band_m = false;
		List<clsDetalleOrdenCompra> detalle = obtenerDetalleGuiaRemision_ProductosNosolicitados_Aceptados();
		if (detalle.Count <= 0)
		{
			return;
		}
		bool band = true;
		using TransactionScope Scope = new TransactionScope();
		foreach (clsDetalleOrdenCompra det in detalle)
		{
			band = admOrdCompra.updatedetalle_1(det);
			if (!band)
			{
				break;
			}
		}
		if (!band)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
		}
		else
		{
			band_m = true;
			Scope.Complete();
			Scope.Dispose();
		}
	}

	private void guardarProductosNosolicitados_Aceptados()
	{
		clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
		string ordenes = "";
		bool band_m = false;
		List<clsDetalleOrdenCompra> detalle = obtenerDetalleGuiaRemision_InsertarproductosNoSolicitados_Aceptados();
		if (detalle.Count <= 0)
		{
			return;
		}
		bool band = true;
		using TransactionScope Scope = new TransactionScope();
		foreach (clsDetalleOrdenCompra det in detalle)
		{
			clsAdmOrdenCompra admOrdenCompra = new clsAdmOrdenCompra();
			admOrdenCompra.ActualizaOrdenCompra_Estado(Convert.ToInt32(grc.ICodOrdenCompra), 1);
			band = admOrdCompra.insertdetalle_1(det);
			if (!band)
			{
				break;
			}
		}
		if (!band)
		{
			Transaction.Current.Rollback();
			Scope.Dispose();
		}
		else
		{
			band_m = true;
			Scope.Complete();
			Scope.Dispose();
		}
	}

	private List<clsDetalleOrdenCompra> obtenerDetalleGuiaRemision_InsertarPromocion()
	{
		List<clsDetalleOrdenCompra> detalleInsert = new List<clsDetalleOrdenCompra>();
		foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
		{
			clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
			if (Convert.ToInt32(fila.Cells["codEstado"].Value) == 3)
			{
				string codProd = Convert.ToString(fila.Cells["colCodProducto"].Value);
				string CodEstado = Convert.ToString(fila.Cells["codEstado"].Value);
				DataTable dt1 = AdmOrden.CargaDetalle(Convert.ToInt32(grc.ICodOrdenCompra));
				if (!Enumerable.Any<DataRow>(dt1.Rows.Cast<DataRow>(), (Func<DataRow, bool>)((DataRow x) => Convert.ToString(x["codProducto"]) == codProd && Convert.ToString(x["estado_etique"]) == CodEstado)))
				{
					deta.CodProducto = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
					deta.CodOrdenCompra = grc.ICodOrdenCompra;
					deta.CodAlmacen = grc.CodAlmacen;
					deta.Moneda = Convert.ToInt32(Ord.Moneda);
					deta.UnidadIngresada = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
					deta.SerieLote = "0";
					deta.Cantidad = Convert.ToDecimal(fila.Cells["colCantidad"].Value);
					deta.cantidadpendiente = 0m;
					deta.PrecioUnitario = 0.0;
					deta.Subtotal = 0.0;
					deta.Descuento1 = 0.0;
					deta.Descuento2 = 0.0;
					deta.Descuento3 = 0.0;
					deta.MontoDescuento = 0.0;
					deta.Igv = 0.0;
					deta.Importe = 0.0;
					deta.PrecioReal = 0.0;
					deta.ValoReal = 0.0;
					deta.FechaIngreso = DateTime.Now;
					deta.CodUser = grc.CodUser;
					deta.CodProveedor = grc.CodProveedor;
					deta.psoli = 1;
					deta.estadoOrden = 1;
					deta.etiqueta = "3 - OC. Con Promocion";
					deta.TipoPrecio = 1;
					deta.etiquetaint = 1;
					deta.codEstadoo = "3";
					detalleInsert.Add(deta);
				}
			}
		}
		return detalleInsert;
	}

	private List<clsDetalleOrdenCompra> obtenerDetalleGuiaRemision_InsertarproductosNoSolicitados_Aceptados()
	{
		List<clsDetalleOrdenCompra> detalleInsert = new List<clsDetalleOrdenCompra>();
		foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
		{
			clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
			if (Convert.ToInt32(fila.Cells["codEstado"].Value) == 6)
			{
				string CodProd = "";
				DataTable data1 = AdmOrden.CargaDetalle(Convert.ToInt32(grc.ICodOrdenCompra));
				deta.CodProducto = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
				deta.CodOrdenCompra = grc.ICodOrdenCompra;
				deta.CodAlmacen = grc.CodAlmacen;
				deta.Moneda = Convert.ToInt32(Ord.Moneda);
				deta.UnidadIngresada = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
				deta.SerieLote = "0";
				deta.Cantidad = Convert.ToDecimal(fila.Cells["colCantidad"].Value);
				deta.cantidadpendiente = 0m;
				deta.PrecioUnitario = 0.0;
				deta.Subtotal = 0.0;
				deta.Descuento1 = 0.0;
				deta.Descuento2 = 0.0;
				deta.Descuento3 = 0.0;
				deta.MontoDescuento = 0.0;
				deta.Igv = 0.0;
				deta.Importe = 0.0;
				deta.PrecioReal = 0.0;
				deta.ValoReal = 0.0;
				deta.FechaIngreso = DateTime.Now;
				deta.CodUser = grc.CodUser;
				deta.CodProveedor = grc.CodProveedor;
				deta.psoli = 1;
				deta.estadoOrden = 1;
				deta.etiqueta = "6 - OC. Prod. NS / A";
				deta.TipoPrecio = 1;
				deta.etiquetaint = 1;
				deta.codEstadoo = "6";
				detalleInsert.Add(deta);
			}
		}
		return detalleInsert;
	}

	private List<clsDetalleOrdenCompra> obtenerDetalleGuiaRemision_Actualizar()
	{
		List<clsDetalleOrdenCompra> detalleActualizar = new List<clsDetalleOrdenCompra>();
		DataTable data2 = AdmOrden.CargaDetalle(Convert.ToInt32(grc.ICodOrdenCompra));
		foreach (DataRow row1 in data2.Rows)
		{
			string codProd = Convert.ToString(row1["codProducto"].ToString());
			string estado_etique = Convert.ToString(row1["estado_etique"].ToString());
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
			{
				clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
				if (Enumerable.Any<DataGridViewRow>(dgvdetalleguiaremisioncompra.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToString(x.Cells["colCodProducto"].Value) == codProd && Convert.ToString(x.Cells["codEstado"].Value) == estado_etique)))
				{
					if (!(Convert.ToString(fila.Cells["colCodProducto"].Value) == codProd) || !(Convert.ToString(fila.Cells["codEstado"].Value) == estado_etique) || Convert.ToInt32(fila.Cells["codEstado"].Value) != 1)
					{
						continue;
					}
					deta.CodProducto = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
					deta.CodDetalleOrdenCompra = Convert.ToInt32(row1["codDetalle"].ToString());
					deta.UnidadIngresada = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
					deta.Moneda = Convert.ToInt32(fila.Cells["colMoneda"].Value.ToString());
					deta.SerieLote = "0";
					double _cantidad1 = 0.0;
					foreach (DataGridViewRow row11 in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
					{
						string _codProducto = Convert.ToString(row11.Cells["colCodProducto"].Value);
						double _cantidad2 = Convert.ToInt32(row11.Cells["colCantidad"].Value);
						string _codEstado = Convert.ToString(row11.Cells["codEstado"].Value);
						if ((Convert.ToString(fila.Cells["colCodProducto"].Value) == _codProducto && _codEstado == "4") || _codEstado == "5")
						{
							_cantidad1 += _cantidad2;
						}
					}
					deta.Cantidad = Convert.ToDecimal(fila.Cells["colCantidad"].Value.ToString()) + Convert.ToDecimal(_cantidad1);
					decimal cntpendiente = Convert.ToDecimal(row1["cantidadpendiente"].ToString());
					if (cntpendiente < 0m)
					{
						deta.cantidadpendiente = 0m;
					}
					else
					{
						deta.cantidadpendiente = cntpendiente;
					}
					deta.Subtotal = Convert.ToDouble(row1["subtotal"].ToString());
					deta.PrecioUnitario = Math.Round(deta.Subtotal / Convert.ToDouble(deta.Cantidad), 4);
					deta.Descuento1 = Convert.ToDouble(row1["descuento1"].ToString());
					deta.Descuento2 = Convert.ToDouble(row1["descuento2"].ToString());
					deta.Descuento3 = Convert.ToDouble(row1["descuento3"].ToString());
					deta.MontoDescuento = Convert.ToDouble(row1["montodscto"].ToString());
					deta.Importe = Convert.ToDouble(row1["subtotal"].ToString());
					double _igv = deta.Importe / 1.18;
					deta.Igv = deta.Importe - _igv;
					deta.PrecioReal = deta.PrecioUnitario;
					deta.ValoReal = deta.PrecioReal / 1.18;
					deta.FechaIngreso = DateTime.Now;
					deta.etiqueta = "Prod. No Atendidos";
					deta.codEstadoo = "1";
					detalleActualizar.Add(deta);
				}
				else
				{
					clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
					bool band = true;
					using TransactionScope Scope = new TransactionScope();
					admOrdCompra.Eliminar_detalleOC_Promocion_1(grc.ICodOrdenCompra, Convert.ToInt32(codProd), Convert.ToInt32(estado_etique));
					Scope.Complete();
					Scope.Dispose();
				}
			}
		}
		return detalleActualizar;
	}

	private List<clsDetalleOrdenCompra> obtenerDetalleGuiaRemision_ActualizarPromocion()
	{
		List<clsDetalleOrdenCompra> detalleActualizar1 = new List<clsDetalleOrdenCompra>();
		DataTable data2 = AdmOrden.CargaDetalle(Convert.ToInt32(grc.ICodOrdenCompra));
		foreach (DataRow row1 in data2.Rows)
		{
			string codProd = Convert.ToString(row1["codProducto"].ToString());
			string estado_etique = Convert.ToString(row1["estado_etique"].ToString());
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
			{
				clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
				if (Enumerable.Any<DataGridViewRow>(dgvdetalleguiaremisioncompra.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToString(x.Cells["colCodProducto"].Value) == codProd && Convert.ToString(x.Cells["codEstado"].Value) == estado_etique)))
				{
					if (Convert.ToString(fila.Cells["colCodProducto"].Value) == codProd && Convert.ToString(fila.Cells["codEstado"].Value) == estado_etique && Convert.ToInt32(fila.Cells["codEstado"].Value) == 3)
					{
						deta.CodProducto = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
						deta.CodDetalleOrdenCompra = Convert.ToInt32(row1["codDetalle"].ToString());
						deta.UnidadIngresada = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
						deta.Moneda = Convert.ToInt32(fila.Cells["colMoneda"].Value.ToString());
						deta.SerieLote = "0";
						deta.Cantidad = Convert.ToDecimal(fila.Cells["colCantidad"].Value.ToString());
						deta.cantidadpendiente = 0m;
						deta.Subtotal = Convert.ToDouble(row1["subtotal"].ToString());
						deta.PrecioUnitario = deta.Subtotal / Convert.ToDouble(deta.Cantidad);
						deta.Descuento1 = Convert.ToDouble(row1["descuento1"].ToString());
						deta.Descuento2 = Convert.ToDouble(row1["descuento2"].ToString());
						deta.Descuento3 = Convert.ToDouble(row1["descuento3"].ToString());
						deta.MontoDescuento = Convert.ToDouble(row1["montodscto"].ToString());
						deta.Importe = Convert.ToDouble(row1["subtotal"].ToString());
						double _igv = deta.Importe / 1.18;
						deta.Igv = deta.Importe - _igv;
						deta.PrecioReal = deta.PrecioUnitario;
						deta.ValoReal = deta.PrecioReal / 1.18;
						deta.FechaIngreso = DateTime.Now;
						deta.etiqueta = "3 - Con. Promocion";
						deta.codEstadoo = "3";
						detalleActualizar1.Add(deta);
					}
				}
				else
				{
					clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
					bool band = true;
					using TransactionScope Scope = new TransactionScope();
					admOrdCompra.Eliminar_detalleOC_Promocion_1(grc.ICodOrdenCompra, Convert.ToInt32(codProd), Convert.ToInt32(estado_etique));
					Scope.Complete();
					Scope.Dispose();
				}
			}
		}
		return detalleActualizar1;
	}

	private List<clsDetalleOrdenCompra> obtenerDetalleGuiaRemision_ProductosNosolicitados_Aceptados()
	{
		List<clsDetalleOrdenCompra> detalleActualizar2 = new List<clsDetalleOrdenCompra>();
		DataTable data2 = AdmOrden.CargaDetalle(Convert.ToInt32(grc.ICodOrdenCompra));
		foreach (DataRow row1 in data2.Rows)
		{
			string codProd = Convert.ToString(row1["codProducto"].ToString());
			string estado_etique = Convert.ToString(row1["estado_etique"].ToString());
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
			{
				clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
				if (Enumerable.Any<DataGridViewRow>(dgvdetalleguiaremisioncompra.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToString(x.Cells["colCodProducto"].Value) == codProd && Convert.ToString(x.Cells["codEstado"].Value) == estado_etique)))
				{
					if (Convert.ToString(fila.Cells["colCodProducto"].Value) == codProd && Convert.ToString(fila.Cells["codEstado"].Value) == estado_etique && Convert.ToInt32(fila.Cells["codEstado"].Value) == 6)
					{
						deta.CodProducto = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
						deta.CodDetalleOrdenCompra = Convert.ToInt32(row1["codDetalle"].ToString());
						deta.UnidadIngresada = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
						deta.Moneda = Convert.ToInt32(fila.Cells["colMoneda"].Value.ToString());
						deta.SerieLote = "0";
						deta.Cantidad = Convert.ToDecimal(fila.Cells["colCantidad"].Value.ToString());
						deta.cantidadpendiente = 0m;
						deta.Subtotal = Convert.ToDouble(row1["subtotal"].ToString());
						deta.PrecioUnitario = deta.Subtotal / Convert.ToDouble(deta.Cantidad);
						deta.Descuento1 = Convert.ToDouble(row1["descuento1"].ToString());
						deta.Descuento2 = Convert.ToDouble(row1["descuento2"].ToString());
						deta.Descuento3 = Convert.ToDouble(row1["descuento3"].ToString());
						deta.MontoDescuento = Convert.ToDouble(row1["montodscto"].ToString());
						deta.Importe = Convert.ToDouble(row1["subtotal"].ToString());
						double _igv = deta.Importe / 1.18;
						deta.Igv = deta.Importe - _igv;
						deta.PrecioReal = deta.PrecioUnitario;
						deta.ValoReal = deta.PrecioReal / 1.18;
						deta.FechaIngreso = DateTime.Now;
						deta.etiqueta = "6 - Prod. NS / A";
						deta.codEstadoo = "6";
						detalleActualizar2.Add(deta);
					}
				}
				else
				{
					clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
					bool band = true;
					using TransactionScope Scope = new TransactionScope();
					admOrdCompra.Eliminar_detalleOC_Promocion_1(grc.ICodOrdenCompra, Convert.ToInt32(codProd), Convert.ToInt32(estado_etique));
					Scope.Complete();
					Scope.Dispose();
				}
			}
		}
		return detalleActualizar2;
	}

	private bool verificaGuardar()
	{
		return guardar = true;
	}

	private void llenarGuiaRemision()
	{
		grc.NumDoc = txtNumero.Text;
		grc.ICodOrdenCompra = Ord.CodOrdenCompra;
		grc.numeroOc = txtnumeroOc.Text;
		grc.CodAlmacen = Ord.CodAlmacen;
		grc.CodMotivo = cmbMotivo.SelectedIndex;
		grc.FechaEmision = dtpFecha.Value;
		grc.FechaTraslado = dtpFechaTransporte.Value;
		grc.fechaingresoalmacen = dtfechaingresoalmacen.Value;
		if (empT != null)
		{
			grc.CodEmpresaTransporte = empT.CodProveedor;
		}
		else
		{
			grc.CodEmpresaTransporte = 0;
		}
		grc.Comentario = txtComentario.Text;
		grc.Estado = 1;
		grc.CodProveedor = Prov.CodProveedor;
		grc.OpcionFlete = cmbtipoflete.SelectedIndex;
		grc.FleteConIgv = Convert.ToDouble(txtTotalFleteConIgv.Text);
		grc.FleteSinIgv = Convert.ToDouble(txtTotalFleteSinIgv.Text);
		if (Editar)
		{
			grc.FechaModificacion = DateTime.Now;
			grc.CodUserModificacion = frmLogin.iCodUser;
		}
		else
		{
			grc.FechaRegistro = DateTime.Now;
			grc.CodUser = frmLogin.iCodUser;
		}
	}

	private void dgvdetalleguiaremisioncompra_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
		{
			Point aux = dgvdetalleguiaremisioncompra.PointToClient(Cursor.Position);
			dgvdetalleguiaremisioncompra.ClearSelection();
			dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Selected = true;
			in_fila_cms_dgv = e.RowIndex;
			cmsFilaDgv.Show(dgvdetalleguiaremisioncompra, aux);
		}
	}

	private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (in_fila_cms_dgv == -1)
		{
		}
	}

	private void dgvdetalleguiaremisioncompra_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1 || e.ColumnIndex == -1)
		{
			return;
		}
		try
		{
			if (e.ColumnIndex == coletiqueta.Index)
			{
				DataGridViewComboBoxCell a = (DataGridViewComboBoxCell)dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[coletiqueta.Name];
				Console.WriteLine("fila: " + e.RowIndex + " -- columna: " + e.ColumnIndex + " -- valoR:" + a.Value);
			}
			else if (Convert.ToInt32(dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[codEstado.Name].Value) == 1)
			{
				lblDetalleProdSeleccionado.Text = dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[colReferencia.Name].Value.ToString() + " - " + dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[colDescripcion.Name].Value.ToString();
				ctdadaEstado.Text = dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[colCantidad.Name].Value.ToString();
				fila_cambio_estado = e.RowIndex;
				btnDarEstadoPromocion.Enabled = true;
				btnDevProv.Enabled = true;
				btnDsctotrans.Enabled = true;
				btnNoSolicAceptado.Enabled = false;
				ultima_fila_seleccionada = e.RowIndex;
			}
			else if (Convert.ToInt32(dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[codEstado.Name].Value) == 7)
			{
				btnDarEstadoPromocion.Enabled = false;
				btnDevProv.Enabled = false;
				btnDsctotrans.Enabled = false;
				btnNoSolicAceptado.Enabled = true;
				lblDetalleProdSeleccionado.Text = dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[colReferencia.Name].Value.ToString() + " - " + dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[colDescripcion.Name].Value.ToString();
				ctdadaEstado.Text = dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[colCantidad.Name].Value.ToString();
				fila_cambio_estado = e.RowIndex;
				ultima_fila_seleccionada = e.RowIndex;
			}
			else
			{
				limpiaGBAsigancionDeEstado();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalleguiaremisioncompra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		try
		{
			DataGridViewRow filactual = dgvdetalleguiaremisioncompra.Rows[e.RowIndex];
			cantidad_previa = Convert.ToDouble(filactual.Cells[colCantidad.Name].Value);
			if (Convert.ToInt32(filactual.Cells[codEstado.Name].Value) != 1)
			{
				filactual.Cells[colCantidad.Name].Value = cantidad_previa;
				dgvdetalleguiaremisioncompra.EndEdit();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error al iniciar editar de celda", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalleguiaremisioncompra_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex == coletiqueta.Index)
		{
			return;
		}
		try
		{
			int indice_fila = e.RowIndex;
			DataGridViewRow fila_actual = dgvdetalleguiaremisioncompra.Rows[indice_fila];
			double cantidad_nueva = Convert.ToDouble(fila_actual.Cells[e.ColumnIndex].Value);
			double cantidad_respaldo = Convert.ToDouble(fila_actual.Cells[colCantidadRespaldo.Name].Value);
			if (cantidad_nueva <= cantidad_previa)
			{
				if (Convert.ToInt32(fila_actual.Cells[codEstado.Name].Value) != 1)
				{
					return;
				}
				double cantidad_restante = cantidad_previa - cantidad_nueva;
				clsDetalleGuiaRemisionCompra temp = obtenerDetalleGuiaRemisionDeDGV(indice_fila);
				temp.DCantidad = cantidad_restante;
				temp.DCantidadRespaldo = cantidad_respaldo - cantidad_restante;
				listadoProdNoAtendidos.Add(temp);
				List<DataRow> buscado_data = (from x in dataGRC.AsEnumerable()
					where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)) == temp.ICodDetalleOrdenCOmpra
					select x).ToList();
				foreach (DataRow fila in buscado_data)
				{
					int ind_modificar = dataGRC.Rows.IndexOf(fila);
					dataGRC.Rows[ind_modificar].SetField(colCantidadRespaldo.DataPropertyName, (object)(cantidad_respaldo - cantidad_restante));
				}
				rellenarTotalesFlete();
			}
			else
			{
				MessageBox.Show("No puede ingresar un numero mayor a la cantidad establecida, si quiere agregar productos.\nUse una de la \"OPCIONES\"", "Cantidad No Permitida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				fila_actual.Cells[e.ColumnIndex].Value = cantidad_previa;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error al terminar editar de celda", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalleguiaremisioncompra_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvdetalleguiaremisioncompra_celltextbox_KeyPress;
			txtedit.KeyPress += dgvdetalleguiaremisioncompra_celltextbox_KeyPress;
		}
	}

	private void dgvdetalleguiaremisioncompra_celltextbox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvdetalleguiaremisioncompra.CurrentCell.OwningColumn.Name == colCantidad.Name)
		{
			ok.NumerosDecimales(e, sender as TextBox);
		}
	}

	private void dgvdetalleguiaremisioncompra_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void btnLIstadoProductosNoAtendidos_Click(object sender, EventArgs e)
	{
		try
		{
			frmProductosNoAtendidosGRC form = new frmProductosNoAtendidosGRC();
			if (listadoProdNoAtendidos.Count == 0)
			{
				DataTable noatendidos = AdmOrd.generarGuiaRemisionOrdenCompra(grc.ICodOrdenCompra);
				if (nuevoMetodo)
				{
					noatendidos = dataGeneradaDeOC;
				}
				if (agregado_ultimo)
				{
					noatendidos = null;
				}
				if (noatendidos != null)
				{
					listadoProdNoAtendidos = ConvertirDataEnLista(noatendidos);
				}
			}
			form.listaDatos = listadoProdNoAtendidos;
			form.ventana = this;
			if (form.ShowDialog() != DialogResult.Yes)
			{
				return;
			}
			if (codProdNoAtendido != -1 && prodNoAtendido != null)
			{
				List<clsDetalleGuiaRemisionCompra> ele_n_a_add = Enumerable.Where<clsDetalleGuiaRemisionCompra>(listadoProdNoAtendidos.AsEnumerable(), (Func<clsDetalleGuiaRemisionCompra, bool>)((clsDetalleGuiaRemisionCompra x) => x.ICodProducto == codProdNoAtendido && x.ICodDetalleGuiaRemisionCompra == prodNoAtendido.ICodDetalleGuiaRemisionCompra && x.ICodDetalleOrdenCOmpra == prodNoAtendido.ICodDetalleOrdenCOmpra)).ToList();
				if (ele_n_a_add.Count > 0)
				{
					List<DataRow> fila_data = (from x in dataGRC.AsEnumerable()
						where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)).ToString() == ((ele_n_a_add[0].ICodDetalleOrdenCOmpra == 0) ? "" : ele_n_a_add[0].ICodDetalleOrdenCOmpra.ToString()) && Convert.ToInt32(x.Field<object>(colCodProducto.DataPropertyName)) == ele_n_a_add[0].ICodProducto && Convert.ToInt32(x.Field<object>(colCodUnidad.DataPropertyName)) == ele_n_a_add[0].IUnidadIngresada
						select x).ToList();
					bool ingreso_no_aten_a_aten = false;
					double nueva_cantidad_respaldo_pa_noingresado = -1.0;
					if (fila_data.Count > 0)
					{
						foreach (DataRow fila_temp in fila_data)
						{
							int ind_a_mod = dataGRC.Rows.IndexOf(fila_temp);
							double nueva_cantidad_respaldo = Convert.ToDouble(dataGRC.Rows[ind_a_mod].Field<object>(colCantidadRespaldo.DataPropertyName)) + ele_n_a_add[0].DCantidad;
							dataGRC.Rows[ind_a_mod].SetField(colCantidadRespaldo.DataPropertyName, nueva_cantidad_respaldo);
							nueva_cantidad_respaldo_pa_noingresado = nueva_cantidad_respaldo;
							if (Convert.ToInt32(dataGRC.Rows[ind_a_mod].Field<object>(codEstado.DataPropertyName)) == 1)
							{
								double nueva_cantidad = Convert.ToDouble(dataGRC.Rows[ind_a_mod].Field<object>(colCantidad.DataPropertyName)) + ele_n_a_add[0].DCantidad;
								dataGRC.Rows[ind_a_mod].SetField(colCantidad.DataPropertyName, nueva_cantidad);
								ingreso_no_aten_a_aten = true;
							}
						}
					}
					if (!ingreso_no_aten_a_aten)
					{
						ele_n_a_add[0].IEstado = 1;
						if (nueva_cantidad_respaldo_pa_noingresado != -1.0)
						{
							ele_n_a_add[0].DCantidadRespaldo = nueva_cantidad_respaldo_pa_noingresado;
						}
						agregarDetalleGRCaData(ele_n_a_add[0]);
					}
					listadoProdNoAtendidos.Remove(ele_n_a_add[0]);
					agregado_ultimo = listadoProdNoAtendidos.Count == 0;
					if (Generada)
					{
						btnLIstadoProductosNoAtendidos.PerformClick();
					}
					rellenarTotalesFlete();
				}
				else
				{
					MessageBox.Show("Ocurrio un error gravisimo al intentar agregar al item", "Agregando Producto a Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("No se pudo encontrar el producto a agregar", "Producto No Agregado a Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void agregarDetalleGRCaData(clsDetalleGuiaRemisionCompra deta)
	{
		DataRow fila = dataGRC.NewRow();
		fila.SetField(colCodDetalleGR.DataPropertyName, (object)deta.ICodDetalleGuiaRemisionCompra);
		fila.SetField(colCodDetalleOrdenCompra.DataPropertyName, (object)deta.ICodDetalleOrdenCOmpra);
		fila.SetField(colCodProducto.DataPropertyName, (object)deta.ICodProducto);
		fila.SetField(colReferencia.DataPropertyName, (object)deta.SReferencia);
		fila.SetField(colDescripcion.DataPropertyName, (object)deta.SDescripcion);
		fila.SetField(colMoneda.DataPropertyName, (object)((deta.IcodMoneda == 0) ? codmonedaoc : deta.IcodMoneda));
		fila.SetField(colCodUnidad.DataPropertyName, (object)deta.IUnidadIngresada);
		fila.SetField(colUnidad.DataPropertyName, (object)deta.SUnidad);
		fila.SetField(colCantidad.DataPropertyName, (object)deta.DCantidad);
		fila.SetField(colCantidadRespaldo.DataPropertyName, (object)deta.DCantidadRespaldo);
		fila.SetField(colfechaingreso.DataPropertyName, (object)deta.FFechaIngreso);
		fila.SetField(colcoduser.DataPropertyName, (object)deta.ICOdUser);
		fila.SetField(colfecharegistro.DataPropertyName, (object)deta.FFechaRegistro);
		fila.SetField(codEstado.DataPropertyName, (object)deta.IEstado);
		fila.SetField(coletiqueta.DataPropertyName, (object)admgrc.getEtiquetaGRC(deta.IEstado));
		dataGRC.Rows.Add(fila);
	}

	private void dgvdetalleguiaremisioncompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private clsDetalleGuiaRemisionCompra obtenerDetalleGuiaRemisionDeDGV(int indice_fila)
	{
		DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[indice_fila];
		clsDetalleGuiaRemisionCompra Dtemp = new clsDetalleGuiaRemisionCompra();
		object coddgrc = fila.Cells[colCodDetalleGR.Name].Value;
		Dtemp.ICodDetalleGuiaRemisionCompra = Convert.ToInt32((coddgrc == "" || coddgrc == DBNull.Value || coddgrc == null) ? ((object)0) : coddgrc);
		object coddoc = fila.Cells[colCodDetalleOrdenCompra.Name].Value;
		Dtemp.ICodDetalleOrdenCOmpra = Convert.ToInt32((coddoc == "" || coddoc == DBNull.Value || coddoc == null) ? ((object)0) : coddoc);
		string colCodOC = ((fila.Cells[colCodOrdenCompra.Name].Value == DBNull.Value || fila.Cells[colCodOrdenCompra.Name].Value == null) ? "0" : fila.Cells[colCodOrdenCompra.Name].Value.ToString());
		Dtemp.ICodOrdenCOmpra = Convert.ToInt32(colCodOC);
		Dtemp.ICodProducto = Convert.ToInt32(fila.Cells[colCodProducto.Name].Value ?? ((object)0));
		Dtemp.SReferencia = fila.Cells[colReferencia.Name].Value.ToString();
		Dtemp.SDescripcion = fila.Cells[colDescripcion.Name].Value.ToString();
		Dtemp.IcodMoneda = Convert.ToInt32(fila.Cells[colMoneda.Name].Value);
		Dtemp.IUnidadIngresada = Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value);
		Dtemp.SUnidad = fila.Cells[colUnidad.Name].Value.ToString();
		Dtemp.DCantidad = Convert.ToDouble(fila.Cells[colCantidad.Name].Value ?? ((object)0));
		Dtemp.DCantidadRespaldo = Convert.ToDouble(fila.Cells[colCantidad.Name].Value ?? ((object)0));
		Dtemp.FFechaIngreso = Convert.ToDateTime(fila.Cells[colfechaingreso.Name].Value);
		object fechaRegistro = fila.Cells[colfecharegistro.Name].Value;
		fechaRegistro = ((fechaRegistro == DBNull.Value || fechaRegistro == null) ? ((object)DateTime.Now) : fechaRegistro);
		Dtemp.FFechaRegistro = Convert.ToDateTime(fechaRegistro);
		return Dtemp;
	}

	private bool getOpcionDeQuitarSegunValordeEtiqueta(int indice_fila)
	{
		bool quitar = false;
		DataGridViewRow fila_actual = dgvdetalleguiaremisioncompra.Rows[indice_fila];
		if (Convert.ToInt32(fila_actual.Cells[codEstado.Name].Value ?? ((object)0)) == 1)
		{
			quitar = true;
		}
		if (Convert.ToInt32(fila_actual.Cells[codEstado.Name].Value ?? ((object)0)) == 2)
		{
			quitar = true;
		}
		if (Convert.ToInt32(fila_actual.Cells[codEstado.Name].Value ?? ((object)0)) == 4)
		{
			quitar = true;
		}
		if (Convert.ToInt32(fila_actual.Cells[codEstado.Name].Value ?? ((object)0)) == 5)
		{
			quitar = true;
		}
		return quitar;
	}

	private void btngenerarFactura_Click(object sender, EventArgs e)
	{
		try
		{
			DataTable facturaGenerada = admgrc.generaFacturaCompraDeGRC(Convert.ToInt32(grc.CodGuiaRemision), 0);
			DataTable data1 = AdmOrden.CargaDetalle(Convert.ToInt32(Ord.CodOrdenCompra));
			foreach (DataRow fila2 in data1.Rows)
			{
				int stadoOrden = Convert.ToInt32(fila2["EstadoDeLaOrden"].ToString());
				int codEstado = Convert.ToInt32(fila2["EstadoDeLaOrden"].ToString());
				if (stadoOrden == 1 || stadoOrden == 19)
				{
					MessageBox.Show("NO SE PUEDE GENERAR LA FACTURA DE COMPRA PORQUE EXISTEN PRODUCTOS QUE NO TIENEN PRECIOS SETEADOS EN LA ORDEN DE COMPRA.", "Generacion de Factura Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			if (facturaGenerada.Rows.Count > 0)
			{
				if (tc.VerificaTCFecha(DateTime.Now.Date))
				{
					tipoDoc = admtipoDoc.CargaTipoDocumento(2);
					frmNotaIngreso form = new frmNotaIngreso();
					form.MdiParent = base.MdiParent;
					form.Dock = DockStyle.Fill;
					form.WindowState = FormWindowState.Maximized;
					form.CodGuiaRemisionCompra = Convert.ToInt32(grc.CodGuiaRemision);
					form.ventana_grc = this;
					form.generadaGRC = true;
					form.Text = "Compra Directa";
					form.Proceso = 1;
					form.compra = 1;
					form.txtTransaccion.Text = "FT";
					form.txtTransaccion.ReadOnly = true;
					form.doc = tipoDoc;
					form.CodDocumento = tipoDoc.CodTipoDocumento;
					form.txtDocRef.Text = tipoDoc.Sigla;
					string[] subcadenas = txtnumeroOc.Text.Split('-');
					form.CodOrdenCompraNew = Convert.ToInt32(subcadenas[1]);
					KeyPressEventArgs ee = new KeyPressEventArgs('\r');
					form.txtTransaccion_KeyPress(form.txtTransaccion, ee);
					form.Show();
				}
				else
				{
					MessageBox.Show("Tipo de Cambio no registrado para el dia " + DateTime.Now.Date.ToString() + ".", "No se encontro Tipo De Cambio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("No hay datos suficientes para generar una factura, verifique que los articulos esten seteados", "Generacion de Factura Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void DespuesDeGenerarFactura(bool generado, clsGuiaRemisionCompraDocumentoRelacionado nuevo)
	{
		if (generado)
		{
			grc.EstadoGeneracion = 1;
			mostrarMensajeGuardado = false;
			btnGuardar.PerformClick();
		}
		btngenerarFactura.Visible = !generado;
		btnGenerarFacturaVenta.Visible = generado;
	}

	private void dgvdetalleguiaremisioncompra_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		if (Convert.ToInt32(dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[codEstado.Name].Value) == 1)
		{
			_ = fondo_celda;
			if (false)
			{
				fondo_celda = dgvdetalleguiaremisioncompra.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
			}
			dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = fondo_celda;
		}
	}

	private void dgvdetalleguiaremisioncompra_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		if (Convert.ToInt32(dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[codEstado.Name].Value) == 1)
		{
			fondo_celda = dgvdetalleguiaremisioncompra.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
			if (e.ColumnIndex == dgvdetalleguiaremisioncompra.Columns[colCantidad.Name].Index)
			{
				dgvdetalleguiaremisioncompra.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(0, 192, 0);
			}
		}
		else
		{
			dgvdetalleguiaremisioncompra.Rows[e.RowIndex].ReadOnly = true;
		}
	}

	private void btnGenerarDocumentosRelacionados_Click(object sender, EventArgs e)
	{
		if (tc.VerificaTCFecha(DateTime.Now.Date))
		{
			DataTable detalleOV = admgrc.generaFacturaVentaDeGRC(Convert.ToInt32(grc.CodGuiaRemision));
			if (detalleOV == null)
			{
				return;
			}
			int codcliente = obtenerCodClientedeProveedor(grc.CodEmpresaTransporte);
			int codcliente2 = obtenerCodClientedeProveedor(grc.CodProveedor);
			if (codcliente == 0 && codcliente2 == 0)
			{
				MessageBox.Show("Proveedor no registrado como cliente imposible generar factura venta", "Error al generar factura venta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			int codcliente3 = ((codcliente == 0) ? codcliente2 : codcliente);
			if (detalleOV.Rows.Count > 0)
			{
				if (Application.OpenForms["frmVenta2019"] != null)
				{
					Application.OpenForms["frmVenta2019"].Activate();
					MessageBox.Show("Se tiene que cerrar la ventana de Orden De Venta para poder generar", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				mdi_Menu casa = (mdi_Menu)Application.OpenForms["mdi_Menu"];
				frmVenta2019 form = new frmVenta2019(casa);
				casa.AddOwnedForm(form);
				form.MdiParent = base.MdiParent;
				form.CodClienteGeneradoGRC = codcliente3;
				form.WindowState = FormWindowState.Maximized;
				form.procedimientoASeguirPorGeneracionDesdeGuiaDeRemision(detalleOV, Convert.ToInt32(grc.CodGuiaRemision));
				form.generadoDeGuiadeRemisionCompra = true;
				form.ventanagrc = this;
				ctdaddocrelacionados = ((ctdaddocrelacionados == -1) ? 1 : ctdaddocrelacionados++);
				form.Show();
			}
			else
			{
				MessageBox.Show("NO Existen Datos Para Generar Factura Venta a Guia Remision Compra", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			MessageBox.Show("Tipo de Cambio no registrado para el dia " + DateTime.Now.Date.ToString() + ".", "No se encontro Tipo De Cambio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private int obtenerCodClientedeProveedor(int codProveedor)
	{
		int codcliente = 0;
		try
		{
			clsProveedor prov = AdmProv.MuestraProveedor(codProveedor);
			if (prov != null)
			{
				clsCliente cli = AdmCli.ConsultaCliente(prov.Ruc);
				if (cli != null)
				{
					codcliente = cli.CodCliente;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - " + Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		return codcliente;
	}

	public void DespuesDeGenerarDocumentoRelacionado(bool generado, clsGuiaRemisionCompraDocumentoRelacionado nuevo)
	{
		if (generado && ctdaddocrelacionados == 0)
		{
			grc.EstadoGeneracion = 2;
			ctdaddocrelacionados = -1;
			mostrarMensajeGuardado = false;
			btnGuardar.PerformClick();
		}
		btngenerarFactura.Visible = !generado;
		btnGenerarFacturaVenta.Visible = !generado;
	}

	private void dgvdetalleguiaremisioncompra_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex == -1)
		{
			return;
		}
		DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[e.RowIndex];
		string col_select = coletiqueta.Name;
		Console.WriteLine("Seleccionado en la fila 1: " + e.RowIndex + " --> " + fila.Cells[col_select].Value);
		if (e.ColumnIndex != coletiqueta.Index)
		{
			return;
		}
		int valor = Convert.ToInt32(fila.Cells[col_select].Value);
		if (valor != 2)
		{
			return;
		}
		if (dgvdetalleguiaremisioncompra.Rows.Count > 1)
		{
			if (getOpcionDeQuitarSegunValordeEtiqueta(e.RowIndex))
			{
				List<clsDetalleGuiaRemisionCompra> detalle = Enumerable.Where<clsDetalleGuiaRemisionCompra>(listadoDetalle.AsEnumerable(), (Func<clsDetalleGuiaRemisionCompra, bool>)((clsDetalleGuiaRemisionCompra x) => x.ICodProducto == Convert.ToInt32(fila.Cells[colCodProducto.Name].Value))).ToList();
				if (detalle.Count > 0)
				{
					listadoProdNoAtendidos.Add(detalle[0]);
				}
			}
			dgvdetalleguiaremisioncompra.Rows.RemoveAt(e.RowIndex);
			rellenarTotalesFlete();
		}
		else
		{
			MessageBox.Show("La guia remision no puede quedar sin elementos.\nEn ese caso elimine o anule la guia de remision.", "Problema de eliminar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalleguiaremisioncompra_Enter(object sender, EventArgs e)
	{
	}

	private void btnAgregarProductoPromocion_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmProductoIngreso"] != null)
			{
				Application.OpenForms["frmProductoIngreso"].Activate();
			}
			else
			{
				frmProductoIngreso form = new frmProductoIngreso();
				form.Procede = 91;
				form.Proceso = 1;
				form.ventana_grc = this;
				form.ShowDialog();
				if (detallegrc_añadir != null)
				{
					agregarDetalleGRCaData(detallegrc_añadir);
					detallegrc_añadir = null;
					darFormatoADGV();
				}
				else
				{
					MessageBox.Show("Ocurrio un error al intentar registrar item seleccionado", "Ingreso de producto dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			rellenarTotalesFlete();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnAgregarProdNoSolicitado_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmProductoIngreso"] != null)
		{
			Application.OpenForms["frmProductoIngreso"].Activate();
		}
		else
		{
			frmProductoIngreso form = new frmProductoIngreso();
			form.Procede = 92;
			form.Proceso = 1;
			form.ventana_grc = this;
			form.ShowDialog();
			if (detallegrc_añadir != null)
			{
				agregarDetalleGRCaData(detallegrc_añadir);
				detallegrc_añadir = null;
				darFormatoADGV();
			}
			else
			{
				MessageBox.Show("Ocurrio un error al intentar registrar item seleccionado", "Ingreso de producto dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		rellenarTotalesFlete();
	}

	private void btnDsctotrans_Click(object sender, EventArgs e)
	{
		if (fila_cambio_estado != -1)
		{
			try
			{
				int codigo_estado_insert = 4;
				DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[fila_cambio_estado];
				clsDetalleGuiaRemisionCompra dgrc = obtenerDetalleGuiaRemisionDeDGV(fila_cambio_estado);
				double cantidad = Convert.ToDouble(Convert.ToDecimal(ctdadaEstado.Text));
				if (Convert.ToDouble(ctdadaEstado.Text) < Convert.ToDouble(fila.Cells[colCantidad.Name].Value))
				{
					fila.Cells[colCantidad.Name].Value = dgrc.DCantidad - cantidad;
					dgrc.DCantidad = cantidad;
					dgrc.IEstado = codigo_estado_insert;
					añadirNuevaFilaConEstadoAListado(dgrc, fila_cambio_estado);
				}
				else if (Convert.ToDouble(fila.Cells[colCantidad.Name].Value) == Convert.ToDouble(ctdadaEstado.Text))
				{
					List<DataRow> fila_data = (from x in dataGRC.AsEnumerable()
						where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodDetalleOrdenCompra.Name].Value) && Convert.ToInt32(x.Field<object>(colCodProducto.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodProducto.Name].Value) && Convert.ToInt32(x.Field<object>(colCodUnidad.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value) && Convert.ToInt32(x.Field<object>(codEstado.DataPropertyName)) == codigo_estado_insert
						select x).ToList();
					if (fila_data.Count > 0)
					{
						fila.Cells[colCantidad.Name].Value = Convert.ToDouble(fila.Cells[colCantidad.Name].Value) + Convert.ToDouble(fila_data[0].Field<object>(colCantidad.DataPropertyName));
						fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
						fila.Cells[codEstado.Name].Value = codigo_estado_insert;
						dataGRC.Rows.Remove(fila_data[0]);
						dataGRC.AcceptChanges();
					}
					else
					{
						fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
						fila.Cells[codEstado.Name].Value = codigo_estado_insert;
					}
				}
				else
				{
					MessageBox.Show("No se puede asignar estado a una cantidad mayor que la columna", "Error Añadir Estado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				limpiaGBAsigancionDeEstado();
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Añadir Estado");
				return;
			}
		}
		MessageBox.Show("Seleccione un item", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private clsDetalleGuiaRemisionCompra copiaDetalleGuiaRemisionCompra(clsDetalleGuiaRemisionCompra deta)
	{
		clsDetalleGuiaRemisionCompra Dtemp = new clsDetalleGuiaRemisionCompra();
		Dtemp.ICodDetalleGuiaRemisionCompra = deta.ICodDetalleGuiaRemisionCompra;
		Dtemp.ICodDetalleOrdenCOmpra = deta.ICodDetalleOrdenCOmpra;
		Dtemp.ICodOrdenCOmpra = deta.ICodOrdenCOmpra;
		Dtemp.ICodProducto = deta.ICodProducto;
		Dtemp.SReferencia = deta.SReferencia;
		Dtemp.SDescripcion = deta.SDescripcion;
		Dtemp.IcodMoneda = deta.IcodMoneda;
		Dtemp.IUnidadIngresada = deta.IUnidadIngresada;
		Dtemp.SUnidad = deta.SUnidad;
		Dtemp.DCantidad = deta.DCantidad;
		Dtemp.DCantidadRespaldo = deta.DCantidadRespaldo;
		Dtemp.FFechaIngreso = deta.FFechaIngreso;
		Dtemp.FFechaRegistro = deta.FFechaRegistro;
		return Dtemp;
	}

	private void añadirNuevaFilaConEstadoAListado(clsDetalleGuiaRemisionCompra nuevo, int fila_cambio_estado)
	{
		DataRow fila = dataGRC.NewRow();
		fila.SetField(colCodDetalleOrdenCompra.DataPropertyName, (object)nuevo.ICodDetalleOrdenCOmpra);
		fila.SetField(colCodProducto.DataPropertyName, (object)nuevo.ICodProducto);
		fila.SetField(colMoneda.DataPropertyName, (object)nuevo.IcodMoneda);
		fila.SetField(colCodUnidad.DataPropertyName, (object)nuevo.IUnidadIngresada);
		fila.SetField(colCantidad.DataPropertyName, (object)nuevo.DCantidad);
		fila.SetField(colCantidadRespaldo.DataPropertyName, (object)nuevo.DCantidadRespaldo);
		fila.SetField(colfechaingreso.DataPropertyName, (object)nuevo.FFechaIngreso);
		fila.SetField(colReferencia.DataPropertyName, (object)nuevo.SReferencia);
		fila.SetField(colDescripcion.DataPropertyName, (object)nuevo.SDescripcion);
		fila.SetField(colUnidad.DataPropertyName, (object)nuevo.SUnidad);
		fila.SetField(coletiqueta.DataPropertyName, (object)admgrc.getEtiquetaGRC(nuevo.IEstado));
		fila.SetField(codEstado.DataPropertyName, (object)nuevo.IEstado);
		if (ultima_fila_seleccionada != -1 || ultima_fila_seleccionada != dataGRC.Rows.Count - 1)
		{
			dataGRC.Rows.InsertAt(fila, ultima_fila_seleccionada + 1);
			ultima_fila_seleccionada = -1;
		}
		else
		{
			dataGRC.Rows.Add(fila);
		}
		darFormatoADGV();
	}

	private void darFormatoADGV()
	{
		try
		{
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
			{
				int estado = Convert.ToInt32(fila.Cells[codEstado.Name].Value);
				int ind = fila.Index;
				switch (estado)
				{
				case 3:
				{
					string coddetalle = fila.Cells[colCodDetalleOrdenCompra.Name].Value.ToString();
					if (coddetalle != "0" && coddetalle != "")
					{
						Color fondo3 = btnDarEstadoPromocion.BackColor;
						fila.DefaultCellStyle.BackColor = fondo3;
					}
					else
					{
						Color fondo4 = btnAgregarProductoPromocion.BackColor;
						fila.DefaultCellStyle.BackColor = fondo4;
					}
					break;
				}
				case 4:
					fila.DefaultCellStyle.BackColor = btnDsctotrans.BackColor;
					break;
				case 5:
				{
					Color fondo2 = btnDevProv.BackColor;
					fila.DefaultCellStyle.BackColor = fondo2;
					break;
				}
				case 6:
					fila.DefaultCellStyle.BackColor = btnNoSolicAceptado.BackColor;
					break;
				case 7:
					fila.DefaultCellStyle.BackColor = btnAgregarProdNoSolicitado.BackColor;
					break;
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnDevProv_Click(object sender, EventArgs e)
	{
		if (fila_cambio_estado != -1)
		{
			try
			{
				int codigo_estado_insert = 5;
				DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[fila_cambio_estado];
				clsDetalleGuiaRemisionCompra dgrc = obtenerDetalleGuiaRemisionDeDGV(fila_cambio_estado);
				double cantidad = Convert.ToDouble(Convert.ToDecimal(ctdadaEstado.Text));
				if (Convert.ToDouble(ctdadaEstado.Text) < Convert.ToDouble(fila.Cells[colCantidad.Name].Value))
				{
					fila.Cells[colCantidad.Name].Value = dgrc.DCantidad - cantidad;
					dgrc.DCantidad = cantidad;
					dgrc.IEstado = codigo_estado_insert;
					añadirNuevaFilaConEstadoAListado(dgrc, fila_cambio_estado);
				}
				else if (Convert.ToDouble(fila.Cells[colCantidad.Name].Value) == Convert.ToDouble(ctdadaEstado.Text))
				{
					List<DataRow> fila_data = (from x in dataGRC.AsEnumerable()
						where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodDetalleOrdenCompra.Name].Value) && Convert.ToInt32(x.Field<object>(colCodProducto.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodProducto.Name].Value) && Convert.ToInt32(x.Field<object>(colCodUnidad.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value) && Convert.ToInt32(x.Field<object>(codEstado.DataPropertyName)) == codigo_estado_insert
						select x).ToList();
					if (fila_data.Count > 0)
					{
						fila.Cells[colCantidad.Name].Value = Convert.ToDouble(fila.Cells[colCantidad.Name].Value) + Convert.ToDouble(fila_data[0].Field<object>(colCantidad.DataPropertyName));
						fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
						fila.Cells[codEstado.Name].Value = codigo_estado_insert;
						dataGRC.Rows.Remove(fila_data[0]);
						dataGRC.AcceptChanges();
					}
					else
					{
						fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
						fila.Cells[codEstado.Name].Value = codigo_estado_insert;
					}
				}
				else
				{
					MessageBox.Show("No se puede asignar estado a una cantidad mayor que la columna", "Error Añadir Estado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				limpiaGBAsigancionDeEstado();
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Añadir Estado");
				return;
			}
		}
		MessageBox.Show("Seleccione un item", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void btnDarEstadoPromocion_Click(object sender, EventArgs e)
	{
		if (fila_cambio_estado != -1)
		{
			try
			{
				int codigo_estado_insert = 3;
				DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[fila_cambio_estado];
				clsDetalleGuiaRemisionCompra dgrc = obtenerDetalleGuiaRemisionDeDGV(fila_cambio_estado);
				double cantidad = Convert.ToDouble(Convert.ToDecimal(ctdadaEstado.Text));
				if (Convert.ToDouble(ctdadaEstado.Text) < Convert.ToDouble(fila.Cells[colCantidad.Name].Value))
				{
					fila.Cells[colCantidad.Name].Value = dgrc.DCantidad - cantidad;
					dgrc.DCantidad = cantidad;
					dgrc.IEstado = codigo_estado_insert;
					añadirNuevaFilaConEstadoAListado(dgrc, fila_cambio_estado);
				}
				else if (Convert.ToDouble(fila.Cells[colCantidad.Name].Value) == Convert.ToDouble(ctdadaEstado.Text))
				{
					List<DataRow> fila_data = (from x in dataGRC.AsEnumerable()
						where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodDetalleOrdenCompra.Name].Value) && Convert.ToInt32(x.Field<object>(colCodProducto.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodProducto.Name].Value) && Convert.ToInt32(x.Field<object>(colCodUnidad.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value) && Convert.ToInt32(x.Field<object>(codEstado.DataPropertyName)) == codigo_estado_insert
						select x).ToList();
					if (fila_data.Count > 0)
					{
						fila.Cells[colCantidad.Name].Value = Convert.ToDouble(fila.Cells[colCantidad.Name].Value) + Convert.ToDouble(fila_data[0].Field<object>(colCantidad.DataPropertyName));
						fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
						fila.Cells[codEstado.Name].Value = codigo_estado_insert;
						dataGRC.Rows.Remove(fila_data[0]);
						dataGRC.AcceptChanges();
					}
					else
					{
						fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
						fila.Cells[codEstado.Name].Value = codigo_estado_insert;
					}
				}
				else
				{
					MessageBox.Show("No se puede asignar estado a una cantidad mayor que la columna", "Error Añadir Estado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				limpiaGBAsigancionDeEstado();
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Añadir Estado");
				return;
			}
		}
		MessageBox.Show("Seleccione un item", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void btnNoSolicAceptado_Click(object sender, EventArgs e)
	{
		if (fila_cambio_estado != -1)
		{
			try
			{
				DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[fila_cambio_estado];
				if (Convert.ToInt32(fila.Cells[codEstado.Name].Value) == 7)
				{
					DialogResult dr = DialogResult.None;
					frmAutorizacion frm = new frmAutorizacion();
					dr = frm.ShowDialog();
					if (dr == DialogResult.OK)
					{
						int codigo_estado_insert = 6;
						clsDetalleGuiaRemisionCompra dgrc = obtenerDetalleGuiaRemisionDeDGV(fila_cambio_estado);
						double cantidad = Convert.ToDouble(Convert.ToDecimal(ctdadaEstado.Text));
						if (Convert.ToDouble(ctdadaEstado.Text) < Convert.ToDouble(fila.Cells[colCantidad.Name].Value))
						{
							fila.Cells[colCantidad.Name].Value = dgrc.DCantidad - cantidad;
							dgrc.DCantidad = cantidad;
							dgrc.IEstado = codigo_estado_insert;
							añadirNuevaFilaConEstadoAListado(dgrc, fila_cambio_estado);
						}
						else if (Convert.ToDouble(fila.Cells[colCantidad.Name].Value) == Convert.ToDouble(ctdadaEstado.Text))
						{
							List<DataRow> fila_data = (from x in dataGRC.AsEnumerable()
								where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodDetalleOrdenCompra.Name].Value) && Convert.ToInt32(x.Field<object>(colCodProducto.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodProducto.Name].Value) && Convert.ToInt32(x.Field<object>(colCodUnidad.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value) && Convert.ToInt32(x.Field<object>(codEstado.DataPropertyName)) == codigo_estado_insert
								select x).ToList();
							if (fila_data.Count > 0)
							{
								fila.Cells[colCantidad.Name].Value = Convert.ToDouble(fila.Cells[colCantidad.Name].Value) + Convert.ToDouble(fila_data[0].Field<object>(colCantidad.DataPropertyName));
								fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
								fila.Cells[codEstado.Name].Value = codigo_estado_insert;
								dataGRC.Rows.Remove(fila_data[0]);
								dataGRC.AcceptChanges();
							}
							else
							{
								fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
								fila.Cells[codEstado.Name].Value = codigo_estado_insert;
							}
							darFormatoADGV();
						}
						else
						{
							MessageBox.Show("No se puede asignar estado a una cantidad mayor que la columna", "Error Añadir Estado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						limpiaGBAsigancionDeEstado();
					}
				}
				else
				{
					string estado_fila = admgrc.getEtiquetaGRC(6);
					string estado_a_cambiar = admgrc.getEtiquetaGRC(7);
					MessageBox.Show("Solo se puede asignar el estado " + estado_a_cambiar + " a los productos con estado " + estado_fila, "Error al tratar Estado de prodcuto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Añadir Estado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		MessageBox.Show("Seleccione un item", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void ctdadaEstado_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosDecimales(e, sender as TextBox);
	}

	private void ctdadaEstado_KeyDown(object sender, KeyEventArgs e)
	{
		string asdn = (sender as TextBox).Text;
	}

	private void ctdadaEstado_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvdetalleguiaremisioncompra_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Delete)
			{
				if (dgvdetalleguiaremisioncompra.SelectedCells.Count == 1)
				{
					DataGridViewRow fila = dgvdetalleguiaremisioncompra.Rows[dgvdetalleguiaremisioncompra.SelectedCells[0].RowIndex];
					if (fila.Index != -1)
					{
						int indice_fila = fila.Index;
						int codProd = Convert.ToInt32(dgvdetalleguiaremisioncompra.Rows[indice_fila].Cells[colCodProducto.Name].Value);
						if (codProd != 0)
						{
							if (dgvdetalleguiaremisioncompra.Rows.Count > 1)
							{
								int cod_estado_de_fila_actual = Convert.ToInt32(fila.Cells[codEstado.Name].Value);
								if (cod_estado_de_fila_actual == 1)
								{
									string coddetallegr = ((fila.Cells[colCodDetalleGR.Name].Value.ToString() == "") ? "0" : fila.Cells[colCodDetalleGR.Name].Value.ToString());
									List<clsDetalleGuiaRemisionCompra> elementoanoatendido = Enumerable.Where<clsDetalleGuiaRemisionCompra>(listadoDetalle.AsEnumerable().AsEnumerable(), (Func<clsDetalleGuiaRemisionCompra, bool>)((clsDetalleGuiaRemisionCompra x) => x.ICodDetalleGuiaRemisionCompra.ToString() == coddetallegr && x.ICodDetalleOrdenCOmpra.ToString() == fila.Cells[colCodDetalleOrdenCompra.Name].Value.ToString() && x.ICodProducto == codProd)).ToList();
									if (elementoanoatendido.Count > 0)
									{
										if (getOpcionDeQuitarSegunValordeEtiqueta(indice_fila))
										{
											clsDetalleGuiaRemisionCompra deta = obtenerDetalleGuiaRemisionDeDGV(indice_fila);
											listadoProdNoAtendidos.Add(deta);
											dgvdetalleguiaremisioncompra.Rows.RemoveAt(indice_fila);
											dataGRC.AcceptChanges();
										}
									}
									else
									{
										dgvdetalleguiaremisioncompra.Rows.RemoveAt(dgvdetalleguiaremisioncompra.SelectedCells[0].RowIndex);
										agregado_ultimo = false;
									}
								}
								else if (cod_estado_de_fila_actual == 6 || cod_estado_de_fila_actual == 7 || (cod_estado_de_fila_actual == 3 && (fila.Cells[colCodDetalleOrdenCompra.Name].Value.ToString() == "" || fila.Cells[colCodDetalleOrdenCompra.Name].Value.ToString() == "0")))
								{
									dgvdetalleguiaremisioncompra.Rows.RemoveAt(indice_fila);
									dataGRC.AcceptChanges();
								}
								else
								{
									int codigo_estado_insert = 1;
									List<DataRow> fila_data = (from x in dataGRC.AsEnumerable()
										where Convert.ToInt32(x.Field<object>(colCodDetalleOrdenCompra.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodDetalleOrdenCompra.Name].Value) && Convert.ToInt32(x.Field<object>(colCodProducto.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodProducto.Name].Value) && Convert.ToInt32(x.Field<object>(colCodUnidad.DataPropertyName)) == Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value) && Convert.ToInt32(x.Field<object>(codEstado.DataPropertyName)) == codigo_estado_insert
										select x).ToList();
									if (fila_data.Count > 0)
									{
										fila.Cells[colCantidad.Name].Value = Convert.ToDouble(fila.Cells[colCantidad.Name].Value) + Convert.ToDouble(fila_data[0].Field<object>(colCantidad.DataPropertyName));
										fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
										fila.Cells[codEstado.Name].Value = codigo_estado_insert;
										dataGRC.Rows.Remove(fila_data[0]);
										dataGRC.AcceptChanges();
									}
									else
									{
										fila.Cells[coletiqueta.Name].Value = admgrc.getEtiquetaGRC(codigo_estado_insert);
										fila.Cells[codEstado.Name].Value = codigo_estado_insert;
									}
								}
								darFormatoADGV();
								rellenarTotalesFlete();
							}
							else
							{
								MessageBox.Show("La guia remision no puede quedar sin elementos.\nEn ese caso elimine o anule la guia de remision.", "Problema de eliminar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						else
						{
							MessageBox.Show("Codigo de Producto de fila no encontrado ", "Error al quitar la fila", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
				else
				{
					MessageBox.Show("Seleccione un item para quitar del listado con la tecla DEL.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			limpiaGBAsigancionDeEstado();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Eliminar Items", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void recargarPagina(int codGuiaRemisionCompra = 0)
	{
		frmGuiaRemisionCompra form = new frmGuiaRemisionCompra();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.Editar = true;
		form.MdiParent = base.MdiParent;
		if (codGuiaRemisionCompra == 0)
		{
			form.codGuiaRemisionCompraAEditar = Convert.ToInt32(grc.CodGuiaRemision);
		}
		else
		{
			form.codGuiaRemisionCompraAEditar = codGuiaRemisionCompra;
		}
		Close();
		form.Show();
	}

	private void cmbtipoflete_SelectedIndexChanged(object sender, EventArgs e)
	{
		rellenarTotalesFlete();
		gbTransporte.Visible = cmbtipoflete.SelectedIndex == 2;
		if (cmbtipoflete.SelectedIndex == 2 && !valueCheckTransportista)
		{
			CodEmpresaTransporte = 0;
			CargaEmpresaTransporte();
		}
		valueCheckTransportista = false;
		if (cmbtipoflete.SelectedIndex == 0)
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
		if (cmbtipoflete.SelectedIndex == 1)
		{
			txtTotalFleteConIgv.Text = grc.FleteConIgv.ToString("#.00");
			txtTotalFleteSinIgv.Text = grc.FleteSinIgv.ToString("#.00");
		}
		if (cmbtipoflete.SelectedIndex == 2)
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
	}

	private void frmGuiaRemisionCompra_Shown(object sender, EventArgs e)
	{
		darFormatoADGV();
		if (nuevoMetodo)
		{
			btnLIstadoProductosNoAtendidos.PerformClick();
			nuevoMetodo = false;
		}
	}

	private void dgvdetalleguiaremisioncompra_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
	{
	}

	private void dgvdetalleguiaremisioncompra_Sorted(object sender, EventArgs e)
	{
		darFormatoADGV();
	}

	private void btnModificarFlete_Click(object sender, EventArgs e)
	{
		bool band = true;
		foreach (DataGridViewRow fila in (IEnumerable)dgvdetalleguiaremisioncompra.Rows)
		{
			if (fila.Cells[colCodDetalleGR.Name].Value == "" || fila.Cells[colCodDetalleGR.Name].Value == DBNull.Value)
			{
				band = false;
			}
		}
		if (band)
		{
			frmListadoProductoFlete form = new frmListadoProductoFlete();
			form.data = admgrc.obtenerListadoProductoFleteDeGRC(Convert.ToInt32(grc.CodGuiaRemision));
			form.ShowDialog();
			rellenarTotalesFlete();
			return;
		}
		frmMostrarMensaje form2 = new frmMostrarMensaje();
		form2.Text = "Advertencia de Guardado";
		form2.colorTexto = Color.White;
		form2.textoColor = "Debe Guardar para abrir la ventana de Fletes";
		form2.lblTextoColor.BackColor = Color.Yellow;
		form2.Height -= form2.lblTextoNegro.Height;
		form2.lblTextoNegro.Height = 0;
		form2.Ok = true;
		form2.ShowDialog();
	}

	private void btnGenerarFacturaFlete_Click(object sender, EventArgs e)
	{
		DataTable hola = dataGRC;
		if (cmbtipoflete.SelectedIndex == 2)
		{
			if (empT != null)
			{
				frmNotaIngreso form1 = new frmNotaIngreso();
				tipoDoc = admtipoDoc.CargaTipoDocumento(2);
				form1.MdiParent = base.MdiParent;
				form1.Dock = DockStyle.Fill;
				form1.WindowState = FormWindowState.Maximized;
				form1.MinimumSize = form1.Size;
				form1.MaximumSize = form1.Size;
				form1.CodGuiaRemisionCompra = Convert.ToInt32(grc.CodGuiaRemision);
				form1.ventana_grc = this;
				form1.generadaGRCparaFlete = true;
				form1.Text = "Compra Directa";
				form1.Proceso = 1;
				form1.txtTransaccion.Text = "FT";
				form1.txtTransaccion.ReadOnly = true;
				KeyPressEventArgs ee1 = new KeyPressEventArgs('\r');
				form1.txtTransaccion_KeyPress(form1.txtTransaccion, ee1);
				form1.doc = tipoDoc;
				form1.CodDocumento = tipoDoc.CodTipoDocumento;
				form1.txtDocRef.Text = tipoDoc.Sigla;
				form1.Show();
			}
			else
			{
				MessageBox.Show("Factura de Flete Al Transportista No Generada. \nSe Recomienda cerrar ventanas generadas, regresar y volver a generar factura en Guia Remision Compra", "Error Generacion Factura Flete", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void btnGenerarNotaCredito_Click(object sender, EventArgs e)
	{
		DataTable detalleNC = admgrc.generaNotaCreditoDeGRC(Convert.ToInt32(grc.CodGuiaRemision));
		if (detalleNC == null)
		{
			return;
		}
		if (detalleNC.Rows.Count > 0)
		{
			frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
			form.Proceso = 1;
			form.generacion = true;
			form.dataOficial = detalleNC;
			form.ventana = this;
			form.codGuiaRemisionCompra = grc.CodGuiaRemision;
			int codFacturacion = admgrc.obtenerCodigoFacturacionSegunCodNotaDeIngreso(grc.CodFactura);
			if (codFacturacion == 0)
			{
				MessageBox.Show("Ocurrio un error al encontrar la factura relacionada a la nota de ingreso <" + grc.CodFactura + ">", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			form.cargarDatosDeGeneracion(Prov.CodProveedor, codFacturacion);
			form.MdiParent = base.MdiParent;
			form.Show();
		}
		else
		{
			MessageBox.Show("NO Existen Datos Para Generar Nota de Credito a Factura Compra", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	public void guardarsinmostrarmensaje()
	{
		mostrarMensajeGuardado = false;
		btnGuardar.PerformClick();
	}

	private void txtfactflete_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (fact_flete != null)
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = fact_flete.CodNotaIngreso.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void txtfactventa_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (fact_venta != null)
		{
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.CodVenta = fact_venta.CodFacturaVenta;
			form.Proceso = 3;
			form.Show();
		}
		else if (pedido == null)
		{
		}
	}

	private void txtnotacredito_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (nota_compra != null)
		{
			frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
			form.MdiParent = base.MdiParent;
			form.CodNotaCC = Convert.ToInt32(nota_compra.CodNotaCredito);
			form.Proceso = 2;
			form.Show();
		}
	}

	private void txtnumeroOc_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (grc.ICodOrdenCompra != 0)
		{
			frmOrdenCompra form = buscarFrmOC("frmOrdenCompra", grc.ICodOrdenCompra);
			if (form != null)
			{
				form.Activate();
				return;
			}
			form = new frmOrdenCompra();
			form.MdiParent = base.MdiParent;
			form.CodOrdenCompra = grc.ICodOrdenCompra;
			form.Proceso = 3;
			form.Show();
		}
	}

	private frmOrdenCompra buscarFrmOC(string tipoFormulario, int codOC)
	{
		frmOrdenCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmOrdenCompra)frm).CodOrdenCompra == codOC)
			{
				form = (frmOrdenCompra)frm;
				break;
			}
		}
		return form;
	}

	private void txtFacturaCompra_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (grc.CodFactura != 0)
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = grc.CodFactura.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void dgvNCCGeneradas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
			form.MdiParent = base.MdiParent;
			form.CodNotaCC = Convert.ToInt32(dgvNCCGeneradas.Rows[e.RowIndex].Cells[colCodNCC.Name].Value);
			form.Proceso = 2;
			form.Show();
		}
	}

	private void btncrearproduct_Click(object sender, EventArgs e)
	{
		try
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 1;
			frm.ShowDialog();
		}
		catch (Exception)
		{
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGuiaRemisionCompra));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvdetalleguiaremisioncompra = new System.Windows.Forms.DataGridView();
		this.colCodDetalleGR = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodDetalleOrdenCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodOrdenCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidadRespaldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcoduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodDocumentoRelacionado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coletiqueta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFleteUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFleteSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnAgregarProdPromocion = new System.Windows.Forms.GroupBox();
		this.label24 = new System.Windows.Forms.Label();
		this.dgvNCCGeneradas = new System.Windows.Forms.DataGridView();
		this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodNCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colNC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lblnotacredito = new System.Windows.Forms.Label();
		this.txtnotacredito = new System.Windows.Forms.TextBox();
		this.lblfactflete = new System.Windows.Forms.Label();
		this.txtfactventa = new System.Windows.Forms.TextBox();
		this.lblfactventa = new System.Windows.Forms.Label();
		this.txtfactflete = new System.Windows.Forms.TextBox();
		this.btnModificarFlete = new System.Windows.Forms.Button();
		this.btnGenerarNotaCredito = new System.Windows.Forms.Button();
		this.btnGenerarFacturaFlete = new System.Windows.Forms.Button();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnNoSolicAceptado = new System.Windows.Forms.Button();
		this.btnDarEstadoPromocion = new System.Windows.Forms.Button();
		this.btnDevProv = new System.Windows.Forms.Button();
		this.label22 = new System.Windows.Forms.Label();
		this.lblDetalleProdSeleccionado = new System.Windows.Forms.Label();
		this.btnDsctotrans = new System.Windows.Forms.Button();
		this.ctdadaEstado = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label23 = new System.Windows.Forms.Label();
		this.txtTotalFleteConIgv = new System.Windows.Forms.TextBox();
		this.cmbtipoflete = new System.Windows.Forms.ComboBox();
		this.txtTotalFleteSinIgv = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFacturaCompra = new System.Windows.Forms.TextBox();
		this.btnGenerarFacturaVenta = new System.Windows.Forms.Button();
		this.btngenerarFactura = new System.Windows.Forms.Button();
		this.btnLIstadoProductosNoAtendidos = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.gbopcionesgrgenerada = new System.Windows.Forms.GroupBox();
		this.btncrearproduct = new System.Windows.Forms.Button();
		this.btnAgregarProdNoSolicitado = new System.Windows.Forms.Button();
		this.btnAgregarProductoPromocion = new System.Windows.Forms.Button();
		this.dtfechaingresoalmacen = new System.Windows.Forms.DateTimePicker();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.txtnumeroOc = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.cmbMotivo = new System.Windows.Forms.ComboBox();
		this.label18 = new System.Windows.Forms.Label();
		this.dtpFechaTransporte = new System.Windows.Forms.DateTimePicker();
		this.label17 = new System.Windows.Forms.Label();
		this.gbTransporte = new System.Windows.Forms.GroupBox();
		this.txtDireccionTransporte = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtRazonSocialTransporte = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtRUCTransporte = new System.Windows.Forms.TextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnRecargarConductor = new System.Windows.Forms.Button();
		this.btnRecargarAutos = new System.Windows.Forms.Button();
		this.cmbConductor = new System.Windows.Forms.ComboBox();
		this.cmbVehiculos = new System.Windows.Forms.ComboBox();
		this.txtMarcaVehiculo = new System.Windows.Forms.TextBox();
		this.txtLicencia = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtConstancia = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtDireccionProveedor = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtRazonSocialProveedor = new System.Windows.Forms.TextBox();
		this.txtRucProveedor = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.tTMensajeInformacion = new System.Windows.Forms.ToolTip(this.components);
		this.cmsFilaDgv = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvdetalleguiaremisioncompra).BeginInit();
		this.btnAgregarProdPromocion.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNCCGeneradas).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.gbopcionesgrgenerada.SuspendLayout();
		this.gbTransporte.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.cmsFilaDgv.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.Transparent;
		this.groupBox1.Controls.Add(this.dgvdetalleguiaremisioncompra);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 372);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1348, 284);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle de Guia de Remision De Compra";
		this.dgvdetalleguiaremisioncompra.AllowUserToAddRows = false;
		this.dgvdetalleguiaremisioncompra.AllowUserToDeleteRows = false;
		this.dgvdetalleguiaremisioncompra.AllowUserToResizeRows = false;
		this.dgvdetalleguiaremisioncompra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvdetalleguiaremisioncompra.BackgroundColor = System.Drawing.Color.White;
		this.dgvdetalleguiaremisioncompra.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dgvdetalleguiaremisioncompra.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
		this.dgvdetalleguiaremisioncompra.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvdetalleguiaremisioncompra.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvdetalleguiaremisioncompra.Columns.AddRange(this.colCodDetalleGR, this.colCodDetalleOrdenCompra, this.colCodOrdenCompra, this.colCodProducto, this.colReferencia, this.colDescripcion, this.colMoneda, this.colCodUnidad, this.colUnidad, this.colCantidad, this.colCantidadRespaldo, this.colfechaingreso, this.colcoduser, this.colfecharegistro, this.colcodDocumentoRelacionado, this.coletiqueta, this.codEstado, this.colFleteUnitario, this.colFleteSubtotal);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvdetalleguiaremisioncompra.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgvdetalleguiaremisioncompra.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvdetalleguiaremisioncompra.EnableHeadersVisualStyles = false;
		this.dgvdetalleguiaremisioncompra.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dgvdetalleguiaremisioncompra.Location = new System.Drawing.Point(3, 16);
		this.dgvdetalleguiaremisioncompra.MultiSelect = false;
		this.dgvdetalleguiaremisioncompra.Name = "dgvdetalleguiaremisioncompra";
		this.dgvdetalleguiaremisioncompra.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvdetalleguiaremisioncompra.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvdetalleguiaremisioncompra.RowHeadersVisible = false;
		this.dgvdetalleguiaremisioncompra.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
		this.dgvdetalleguiaremisioncompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvdetalleguiaremisioncompra.Size = new System.Drawing.Size(1342, 265);
		this.dgvdetalleguiaremisioncompra.TabIndex = 0;
		this.dgvdetalleguiaremisioncompra.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvdetalleguiaremisioncompra_CellBeginEdit);
		this.dgvdetalleguiaremisioncompra.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellClick);
		this.dgvdetalleguiaremisioncompra.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellContentClick);
		this.dgvdetalleguiaremisioncompra.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellDoubleClick);
		this.dgvdetalleguiaremisioncompra.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellEndEdit);
		this.dgvdetalleguiaremisioncompra.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellEnter);
		this.dgvdetalleguiaremisioncompra.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellLeave);
		this.dgvdetalleguiaremisioncompra.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvdetalleguiaremisioncompra_CellMouseClick);
		this.dgvdetalleguiaremisioncompra.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalleguiaremisioncompra_CellValueChanged);
		this.dgvdetalleguiaremisioncompra.ColumnSortModeChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(dgvdetalleguiaremisioncompra_ColumnSortModeChanged);
		this.dgvdetalleguiaremisioncompra.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvdetalleguiaremisioncompra_EditingControlShowing);
		this.dgvdetalleguiaremisioncompra.Sorted += new System.EventHandler(dgvdetalleguiaremisioncompra_Sorted);
		this.dgvdetalleguiaremisioncompra.Enter += new System.EventHandler(dgvdetalleguiaremisioncompra_Enter);
		this.dgvdetalleguiaremisioncompra.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvdetalleguiaremisioncompra_KeyUp);
		this.colCodDetalleGR.DataPropertyName = "codDetalleGuiaRemision";
		this.colCodDetalleGR.HeaderText = "CodDetalleGuiaRemision";
		this.colCodDetalleGR.Name = "colCodDetalleGR";
		this.colCodDetalleGR.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCodDetalleGR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCodDetalleGR.Visible = false;
		this.colCodDetalleOrdenCompra.DataPropertyName = "codDetalleOrdenCompra";
		this.colCodDetalleOrdenCompra.HeaderText = "codDetalleOrdenCompra";
		this.colCodDetalleOrdenCompra.Name = "colCodDetalleOrdenCompra";
		this.colCodOrdenCompra.DataPropertyName = "CodOrdenCompra";
		this.colCodOrdenCompra.HeaderText = "CodOrdenCompra";
		this.colCodOrdenCompra.Name = "colCodOrdenCompra";
		this.colCodOrdenCompra.ReadOnly = true;
		this.colCodProducto.DataPropertyName = "codProducto";
		this.colCodProducto.HeaderText = "CodProducto";
		this.colCodProducto.Name = "colCodProducto";
		this.colCodProducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCodProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCodProducto.Visible = false;
		this.colReferencia.DataPropertyName = "referencia";
		this.colReferencia.FillWeight = 12.69035f;
		this.colReferencia.HeaderText = "Referencia";
		this.colReferencia.MinimumWidth = 100;
		this.colReferencia.Name = "colReferencia";
		this.colReferencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colDescripcion.DataPropertyName = "producto";
		this.colDescripcion.FillWeight = 449.2386f;
		this.colDescripcion.HeaderText = "Descripcion";
		this.colDescripcion.MinimumWidth = 300;
		this.colDescripcion.Name = "colDescripcion";
		this.colDescripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colMoneda.DataPropertyName = "moneda";
		this.colMoneda.HeaderText = "Moneda";
		this.colMoneda.Name = "colMoneda";
		this.colCodUnidad.DataPropertyName = "codUnidadMedida";
		this.colCodUnidad.HeaderText = "Cod. Unidad";
		this.colCodUnidad.Name = "colCodUnidad";
		this.colCodUnidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCodUnidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCodUnidad.Visible = false;
		this.colUnidad.DataPropertyName = "unidad";
		this.colUnidad.FillWeight = 12.69035f;
		this.colUnidad.HeaderText = "Unidad";
		this.colUnidad.MinimumWidth = 150;
		this.colUnidad.Name = "colUnidad";
		this.colUnidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colUnidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N2";
		dataGridViewCellStyle7.NullValue = null;
		this.colCantidad.DefaultCellStyle = dataGridViewCellStyle7;
		this.colCantidad.FillWeight = 12.69035f;
		this.colCantidad.HeaderText = "Cantidad";
		this.colCantidad.MinimumWidth = 50;
		this.colCantidad.Name = "colCantidad";
		this.colCantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCantidadRespaldo.DataPropertyName = "cantidadrespaldo";
		this.colCantidadRespaldo.HeaderText = "Cant. Respaldo";
		this.colCantidadRespaldo.Name = "colCantidadRespaldo";
		this.colCantidadRespaldo.Visible = false;
		this.colfechaingreso.DataPropertyName = "fechaingreso";
		this.colfechaingreso.HeaderText = "FechaIngre";
		this.colfechaingreso.Name = "colfechaingreso";
		this.colfechaingreso.Visible = false;
		this.colcoduser.DataPropertyName = "codUser";
		this.colcoduser.HeaderText = "CodUser";
		this.colcoduser.Name = "colcoduser";
		this.colcoduser.Visible = false;
		this.colfecharegistro.DataPropertyName = "fecharegistro";
		this.colfecharegistro.HeaderText = "Fecha Reg";
		this.colfecharegistro.Name = "colfecharegistro";
		this.colfecharegistro.Visible = false;
		this.colcodDocumentoRelacionado.DataPropertyName = "codDocumentoRelacionado";
		this.colcodDocumentoRelacionado.HeaderText = "codDocumentoRelacionado";
		this.colcodDocumentoRelacionado.Name = "colcodDocumentoRelacionado";
		this.colcodDocumentoRelacionado.Visible = false;
		this.coletiqueta.DataPropertyName = "setiqueta";
		this.coletiqueta.FillWeight = 12.69035f;
		this.coletiqueta.HeaderText = "Estado";
		this.coletiqueta.MinimumWidth = 150;
		this.coletiqueta.Name = "coletiqueta";
		this.coletiqueta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.coletiqueta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codEstado.DataPropertyName = "codetiqueta";
		this.codEstado.HeaderText = "codEstado";
		this.codEstado.Name = "codEstado";
		this.colFleteUnitario.DataPropertyName = "fleteUnitario";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.colFleteUnitario.DefaultCellStyle = dataGridViewCellStyle8;
		this.colFleteUnitario.HeaderText = "Flete Unitario";
		this.colFleteUnitario.MinimumWidth = 50;
		this.colFleteUnitario.Name = "colFleteUnitario";
		this.colFleteUnitario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colFleteSubtotal.DataPropertyName = "fleteSubTotal";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.colFleteSubtotal.DefaultCellStyle = dataGridViewCellStyle9;
		this.colFleteSubtotal.HeaderText = "Flete Subtotal";
		this.colFleteSubtotal.MinimumWidth = 50;
		this.colFleteSubtotal.Name = "colFleteSubtotal";
		this.colFleteSubtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.btnAgregarProdPromocion.Controls.Add(this.label24);
		this.btnAgregarProdPromocion.Controls.Add(this.dgvNCCGeneradas);
		this.btnAgregarProdPromocion.Controls.Add(this.lblnotacredito);
		this.btnAgregarProdPromocion.Controls.Add(this.txtnotacredito);
		this.btnAgregarProdPromocion.Controls.Add(this.lblfactflete);
		this.btnAgregarProdPromocion.Controls.Add(this.txtfactventa);
		this.btnAgregarProdPromocion.Controls.Add(this.lblfactventa);
		this.btnAgregarProdPromocion.Controls.Add(this.txtfactflete);
		this.btnAgregarProdPromocion.Controls.Add(this.btnModificarFlete);
		this.btnAgregarProdPromocion.Controls.Add(this.btnGenerarNotaCredito);
		this.btnAgregarProdPromocion.Controls.Add(this.btnGenerarFacturaFlete);
		this.btnAgregarProdPromocion.Controls.Add(this.groupBox3);
		this.btnAgregarProdPromocion.Controls.Add(this.groupBox2);
		this.btnAgregarProdPromocion.Controls.Add(this.label2);
		this.btnAgregarProdPromocion.Controls.Add(this.txtFacturaCompra);
		this.btnAgregarProdPromocion.Controls.Add(this.btnGenerarFacturaVenta);
		this.btnAgregarProdPromocion.Controls.Add(this.btngenerarFactura);
		this.btnAgregarProdPromocion.Controls.Add(this.btnLIstadoProductosNoAtendidos);
		this.btnAgregarProdPromocion.Controls.Add(this.btnSalir);
		this.btnAgregarProdPromocion.Controls.Add(this.btnGuardar);
		this.btnAgregarProdPromocion.Controls.Add(this.gbopcionesgrgenerada);
		this.btnAgregarProdPromocion.Controls.Add(this.dtfechaingresoalmacen);
		this.btnAgregarProdPromocion.Controls.Add(this.label21);
		this.btnAgregarProdPromocion.Controls.Add(this.label20);
		this.btnAgregarProdPromocion.Controls.Add(this.txtnumeroOc);
		this.btnAgregarProdPromocion.Controls.Add(this.label5);
		this.btnAgregarProdPromocion.Controls.Add(this.txtNumero);
		this.btnAgregarProdPromocion.Controls.Add(this.cmbMotivo);
		this.btnAgregarProdPromocion.Controls.Add(this.label18);
		this.btnAgregarProdPromocion.Controls.Add(this.dtpFechaTransporte);
		this.btnAgregarProdPromocion.Controls.Add(this.label17);
		this.btnAgregarProdPromocion.Controls.Add(this.gbTransporte);
		this.btnAgregarProdPromocion.Controls.Add(this.groupBox4);
		this.btnAgregarProdPromocion.Controls.Add(this.txtDireccionProveedor);
		this.btnAgregarProdPromocion.Controls.Add(this.label4);
		this.btnAgregarProdPromocion.Controls.Add(this.txtRazonSocialProveedor);
		this.btnAgregarProdPromocion.Controls.Add(this.txtRucProveedor);
		this.btnAgregarProdPromocion.Controls.Add(this.label15);
		this.btnAgregarProdPromocion.Controls.Add(this.btnDetalle);
		this.btnAgregarProdPromocion.Controls.Add(this.txtComentario);
		this.btnAgregarProdPromocion.Controls.Add(this.label9);
		this.btnAgregarProdPromocion.Controls.Add(this.label7);
		this.btnAgregarProdPromocion.Controls.Add(this.dtpFecha);
		this.btnAgregarProdPromocion.Controls.Add(this.label1);
		this.btnAgregarProdPromocion.Dock = System.Windows.Forms.DockStyle.Top;
		this.btnAgregarProdPromocion.Location = new System.Drawing.Point(0, 0);
		this.btnAgregarProdPromocion.Name = "btnAgregarProdPromocion";
		this.btnAgregarProdPromocion.Size = new System.Drawing.Size(1348, 372);
		this.btnAgregarProdPromocion.TabIndex = 22;
		this.btnAgregarProdPromocion.TabStop = false;
		this.btnAgregarProdPromocion.Text = "Datos de Guia de Remision de Compra";
		this.btnAgregarProdPromocion.Enter += new System.EventHandler(btnAgregarProdPromocion_Enter);
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(29, 127);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(41, 13);
		this.label24.TabIndex = 81;
		this.label24.Text = "label24";
		this.dgvNCCGeneradas.AllowUserToAddRows = false;
		this.dgvNCCGeneradas.AllowUserToDeleteRows = false;
		this.dgvNCCGeneradas.AllowUserToResizeRows = false;
		this.dgvNCCGeneradas.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ActiveCaption;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvNCCGeneradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
		this.dgvNCCGeneradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvNCCGeneradas.Columns.AddRange(this.colItem, this.colCodNCC, this.colNC);
		this.dgvNCCGeneradas.Location = new System.Drawing.Point(591, 77);
		this.dgvNCCGeneradas.MultiSelect = false;
		this.dgvNCCGeneradas.Name = "dgvNCCGeneradas";
		this.dgvNCCGeneradas.ReadOnly = true;
		this.dgvNCCGeneradas.RowHeadersVisible = false;
		this.dgvNCCGeneradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNCCGeneradas.Size = new System.Drawing.Size(174, 110);
		this.dgvNCCGeneradas.TabIndex = 80;
		this.dgvNCCGeneradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNCCGeneradas_CellDoubleClick);
		this.colItem.DataPropertyName = "nroItem";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.colItem.DefaultCellStyle = dataGridViewCellStyle11;
		this.colItem.HeaderText = "Item";
		this.colItem.Name = "colItem";
		this.colItem.ReadOnly = true;
		this.colItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colItem.Width = 35;
		this.colCodNCC.DataPropertyName = "codNCC";
		this.colCodNCC.HeaderText = "codNCC";
		this.colCodNCC.Name = "colCodNCC";
		this.colCodNCC.ReadOnly = true;
		this.colCodNCC.Visible = false;
		this.colNC.DataPropertyName = "docNCC";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.colNC.DefaultCellStyle = dataGridViewCellStyle12;
		this.colNC.HeaderText = "Nota de Credito de Compra";
		this.colNC.Name = "colNC";
		this.colNC.ReadOnly = true;
		this.colNC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colNC.Width = 135;
		this.lblnotacredito.AutoSize = true;
		this.lblnotacredito.Location = new System.Drawing.Point(501, 81);
		this.lblnotacredito.Name = "lblnotacredito";
		this.lblnotacredito.Size = new System.Drawing.Size(84, 13);
		this.lblnotacredito.TabIndex = 79;
		this.lblnotacredito.Text = "Nota de Credito:";
		this.txtnotacredito.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtnotacredito.ForeColor = System.Drawing.SystemColors.WindowFrame;
		this.txtnotacredito.Location = new System.Drawing.Point(591, 77);
		this.txtnotacredito.Name = "txtnotacredito";
		this.txtnotacredito.ReadOnly = true;
		this.txtnotacredito.Size = new System.Drawing.Size(115, 20);
		this.txtnotacredito.TabIndex = 78;
		this.txtnotacredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtnotacredito.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtnotacredito_MouseDoubleClick);
		this.lblfactflete.AutoSize = true;
		this.lblfactflete.Location = new System.Drawing.Point(513, 28);
		this.lblfactflete.Name = "lblfactflete";
		this.lblfactflete.Size = new System.Drawing.Size(72, 13);
		this.lblfactflete.TabIndex = 77;
		this.lblfactflete.Text = "Factura Flete:";
		this.txtfactventa.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtfactventa.ForeColor = System.Drawing.SystemColors.WindowFrame;
		this.txtfactventa.Location = new System.Drawing.Point(591, 51);
		this.txtfactventa.Name = "txtfactventa";
		this.txtfactventa.ReadOnly = true;
		this.txtfactventa.Size = new System.Drawing.Size(115, 20);
		this.txtfactventa.TabIndex = 76;
		this.txtfactventa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtfactventa.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtfactventa_MouseDoubleClick);
		this.lblfactventa.AutoSize = true;
		this.lblfactventa.Location = new System.Drawing.Point(508, 54);
		this.lblfactventa.Name = "lblfactventa";
		this.lblfactventa.Size = new System.Drawing.Size(77, 13);
		this.lblfactventa.TabIndex = 75;
		this.lblfactventa.Text = "Factura Venta:";
		this.txtfactflete.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtfactflete.ForeColor = System.Drawing.SystemColors.WindowFrame;
		this.txtfactflete.Location = new System.Drawing.Point(591, 25);
		this.txtfactflete.Name = "txtfactflete";
		this.txtfactflete.ReadOnly = true;
		this.txtfactflete.Size = new System.Drawing.Size(115, 20);
		this.txtfactflete.TabIndex = 74;
		this.txtfactflete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtfactflete.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtfactflete_MouseDoubleClick);
		this.btnModificarFlete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnModificarFlete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModificarFlete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModificarFlete.Image = SIGEFA.Properties.Resources.ganancia;
		this.btnModificarFlete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModificarFlete.Location = new System.Drawing.Point(773, 295);
		this.btnModificarFlete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnModificarFlete.Name = "btnModificarFlete";
		this.btnModificarFlete.Size = new System.Drawing.Size(135, 46);
		this.btnModificarFlete.TabIndex = 73;
		this.btnModificarFlete.Text = "Modificar Flete";
		this.btnModificarFlete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tTMensajeInformacion.SetToolTip(this.btnModificarFlete, "Ingresara los productos a almacen segun los estado.");
		this.btnModificarFlete.UseVisualStyleBackColor = true;
		this.btnModificarFlete.Click += new System.EventHandler(btnModificarFlete_Click);
		this.btnGenerarNotaCredito.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerarNotaCredito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerarNotaCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerarNotaCredito.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.btnGenerarNotaCredito.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerarNotaCredito.Location = new System.Drawing.Point(797, 64);
		this.btnGenerarNotaCredito.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGenerarNotaCredito.Name = "btnGenerarNotaCredito";
		this.btnGenerarNotaCredito.Size = new System.Drawing.Size(130, 46);
		this.btnGenerarNotaCredito.TabIndex = 72;
		this.btnGenerarNotaCredito.Text = "Generar Nota Credito";
		this.btnGenerarNotaCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tTMensajeInformacion.SetToolTip(this.btnGenerarNotaCredito, "Ingresara los productos a almacen segun los estado.");
		this.btnGenerarNotaCredito.UseVisualStyleBackColor = true;
		this.btnGenerarNotaCredito.Visible = false;
		this.btnGenerarNotaCredito.Click += new System.EventHandler(btnGenerarNotaCredito_Click);
		this.btnGenerarFacturaFlete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerarFacturaFlete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerarFacturaFlete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerarFacturaFlete.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.btnGenerarFacturaFlete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerarFacturaFlete.Location = new System.Drawing.Point(797, 19);
		this.btnGenerarFacturaFlete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGenerarFacturaFlete.Name = "btnGenerarFacturaFlete";
		this.btnGenerarFacturaFlete.Size = new System.Drawing.Size(177, 46);
		this.btnGenerarFacturaFlete.TabIndex = 71;
		this.btnGenerarFacturaFlete.Text = "Generar Factura Flete";
		this.btnGenerarFacturaFlete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tTMensajeInformacion.SetToolTip(this.btnGenerarFacturaFlete, "Ingresara los productos a almacen segun los estado.");
		this.btnGenerarFacturaFlete.UseVisualStyleBackColor = true;
		this.btnGenerarFacturaFlete.Click += new System.EventHandler(btnGenerarFacturaFlete_Click);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.btnNoSolicAceptado);
		this.groupBox3.Controls.Add(this.btnDarEstadoPromocion);
		this.groupBox3.Controls.Add(this.btnDevProv);
		this.groupBox3.Controls.Add(this.label22);
		this.groupBox3.Controls.Add(this.lblDetalleProdSeleccionado);
		this.groupBox3.Controls.Add(this.btnDsctotrans);
		this.groupBox3.Controls.Add(this.ctdadaEstado);
		this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.Location = new System.Drawing.Point(797, 116);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(337, 119);
		this.groupBox3.TabIndex = 70;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Asignacion de Estados";
		this.btnNoSolicAceptado.BackColor = System.Drawing.Color.Lime;
		this.btnNoSolicAceptado.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnNoSolicAceptado.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnNoSolicAceptado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
		this.btnNoSolicAceptado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btnNoSolicAceptado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnNoSolicAceptado.Location = new System.Drawing.Point(230, 93);
		this.btnNoSolicAceptado.Name = "btnNoSolicAceptado";
		this.btnNoSolicAceptado.Size = new System.Drawing.Size(97, 23);
		this.btnNoSolicAceptado.TabIndex = 7;
		this.btnNoSolicAceptado.Text = "No Solic. / A";
		this.btnNoSolicAceptado.UseVisualStyleBackColor = false;
		this.btnNoSolicAceptado.Click += new System.EventHandler(btnNoSolicAceptado_Click);
		this.btnDarEstadoPromocion.BackColor = System.Drawing.Color.Yellow;
		this.btnDarEstadoPromocion.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnDarEstadoPromocion.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnDarEstadoPromocion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
		this.btnDarEstadoPromocion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btnDarEstadoPromocion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDarEstadoPromocion.Location = new System.Drawing.Point(136, 93);
		this.btnDarEstadoPromocion.Name = "btnDarEstadoPromocion";
		this.btnDarEstadoPromocion.Size = new System.Drawing.Size(88, 23);
		this.btnDarEstadoPromocion.TabIndex = 6;
		this.btnDarEstadoPromocion.Text = "Promocion";
		this.btnDarEstadoPromocion.UseVisualStyleBackColor = false;
		this.btnDarEstadoPromocion.Click += new System.EventHandler(btnDarEstadoPromocion_Click);
		this.btnDevProv.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnDevProv.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnDevProv.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnDevProv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
		this.btnDevProv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btnDevProv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDevProv.Location = new System.Drawing.Point(230, 64);
		this.btnDevProv.Name = "btnDevProv";
		this.btnDevProv.Size = new System.Drawing.Size(97, 23);
		this.btnDevProv.TabIndex = 5;
		this.btnDevProv.Text = "Dev. Prov.";
		this.btnDevProv.UseVisualStyleBackColor = false;
		this.btnDevProv.Click += new System.EventHandler(btnDevProv_Click);
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(7, 69);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(61, 13);
		this.label22.TabIndex = 4;
		this.label22.Text = "Cantidad:";
		this.lblDetalleProdSeleccionado.AutoEllipsis = true;
		this.lblDetalleProdSeleccionado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblDetalleProdSeleccionado.Location = new System.Drawing.Point(7, 19);
		this.lblDetalleProdSeleccionado.Name = "lblDetalleProdSeleccionado";
		this.lblDetalleProdSeleccionado.Size = new System.Drawing.Size(320, 32);
		this.lblDetalleProdSeleccionado.TabIndex = 3;
		this.lblDetalleProdSeleccionado.Text = "Detalle de Producto Seleccionado";
		this.btnDsctotrans.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnDsctotrans.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnDsctotrans.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.btnDsctotrans.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(128, 255, 128);
		this.btnDsctotrans.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(128, 128, 255);
		this.btnDsctotrans.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDsctotrans.Location = new System.Drawing.Point(136, 64);
		this.btnDsctotrans.Name = "btnDsctotrans";
		this.btnDsctotrans.Size = new System.Drawing.Size(88, 23);
		this.btnDsctotrans.TabIndex = 2;
		this.btnDsctotrans.Text = "Dscto Trans";
		this.btnDsctotrans.UseVisualStyleBackColor = false;
		this.btnDsctotrans.Click += new System.EventHandler(btnDsctotrans_Click);
		this.ctdadaEstado.Location = new System.Drawing.Point(74, 66);
		this.ctdadaEstado.Name = "ctdadaEstado";
		this.ctdadaEstado.Size = new System.Drawing.Size(56, 20);
		this.ctdadaEstado.TabIndex = 0;
		this.ctdadaEstado.KeyPress += new System.Windows.Forms.KeyPressEventHandler(ctdadaEstado_KeyPress);
		this.groupBox2.Controls.Add(this.label23);
		this.groupBox2.Controls.Add(this.txtTotalFleteConIgv);
		this.groupBox2.Controls.Add(this.cmbtipoflete);
		this.groupBox2.Controls.Add(this.txtTotalFleteSinIgv);
		this.groupBox2.Controls.Add(this.label19);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Location = new System.Drawing.Point(18, 265);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(258, 100);
		this.groupBox2.TabIndex = 69;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Opciones de FLETE";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(7, 22);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(43, 13);
		this.label23.TabIndex = 82;
		this.label23.Text = "FLETE:";
		this.txtTotalFleteConIgv.Location = new System.Drawing.Point(127, 70);
		this.txtTotalFleteConIgv.Name = "txtTotalFleteConIgv";
		this.txtTotalFleteConIgv.Size = new System.Drawing.Size(117, 20);
		this.txtTotalFleteConIgv.TabIndex = 4;
		this.txtTotalFleteConIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.cmbtipoflete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbtipoflete.FormattingEnabled = true;
		this.cmbtipoflete.Items.AddRange(new object[3] { "Sin Flete", "Con Flete", "Flete Tercerizado" });
		this.cmbtipoflete.Location = new System.Drawing.Point(56, 19);
		this.cmbtipoflete.Name = "cmbtipoflete";
		this.cmbtipoflete.Size = new System.Drawing.Size(188, 21);
		this.cmbtipoflete.TabIndex = 81;
		this.cmbtipoflete.SelectedIndexChanged += new System.EventHandler(cmbtipoflete_SelectedIndexChanged);
		this.txtTotalFleteSinIgv.Location = new System.Drawing.Point(127, 44);
		this.txtTotalFleteSinIgv.Name = "txtTotalFleteSinIgv";
		this.txtTotalFleteSinIgv.Size = new System.Drawing.Size(117, 20);
		this.txtTotalFleteSinIgv.TabIndex = 3;
		this.txtTotalFleteSinIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(7, 74);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(106, 13);
		this.label19.TabIndex = 2;
		this.label19.Text = "Total Flete (Con IGV)";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(7, 47);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(102, 13);
		this.label8.TabIndex = 1;
		this.label8.Text = "Total Flete (Sin IGV)";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(275, 54);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(85, 13);
		this.label2.TabIndex = 68;
		this.label2.Text = "Factura Compra:";
		this.txtFacturaCompra.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtFacturaCompra.ForeColor = System.Drawing.SystemColors.WindowFrame;
		this.txtFacturaCompra.Location = new System.Drawing.Point(380, 51);
		this.txtFacturaCompra.Name = "txtFacturaCompra";
		this.txtFacturaCompra.ReadOnly = true;
		this.txtFacturaCompra.Size = new System.Drawing.Size(115, 20);
		this.txtFacturaCompra.TabIndex = 67;
		this.txtFacturaCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtFacturaCompra.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtFacturaCompra_MouseDoubleClick);
		this.btnGenerarFacturaVenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerarFacturaVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerarFacturaVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerarFacturaVenta.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.btnGenerarFacturaVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerarFacturaVenta.Location = new System.Drawing.Point(926, 64);
		this.btnGenerarFacturaVenta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGenerarFacturaVenta.Name = "btnGenerarFacturaVenta";
		this.btnGenerarFacturaVenta.Size = new System.Drawing.Size(192, 46);
		this.btnGenerarFacturaVenta.TabIndex = 66;
		this.btnGenerarFacturaVenta.Text = "Generar Factura Venta";
		this.btnGenerarFacturaVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tTMensajeInformacion.SetToolTip(this.btnGenerarFacturaVenta, "Ingresara los productos a almacen segun los estado.");
		this.btnGenerarFacturaVenta.UseVisualStyleBackColor = true;
		this.btnGenerarFacturaVenta.Visible = false;
		this.btnGenerarFacturaVenta.Click += new System.EventHandler(btnGenerarDocumentosRelacionados_Click);
		this.btngenerarFactura.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btngenerarFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btngenerarFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btngenerarFactura.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.btngenerarFactura.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btngenerarFactura.Location = new System.Drawing.Point(971, 19);
		this.btngenerarFactura.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btngenerarFactura.Name = "btngenerarFactura";
		this.btngenerarFactura.Size = new System.Drawing.Size(147, 46);
		this.btngenerarFactura.TabIndex = 65;
		this.btngenerarFactura.Text = "Generar Factura Compra";
		this.btngenerarFactura.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tTMensajeInformacion.SetToolTip(this.btngenerarFactura, "Ingresara los productos a almacen segun los estado.");
		this.btngenerarFactura.UseVisualStyleBackColor = true;
		this.btngenerarFactura.Click += new System.EventHandler(btngenerarFactura_Click);
		this.btnLIstadoProductosNoAtendidos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnLIstadoProductosNoAtendidos.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnLIstadoProductosNoAtendidos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnLIstadoProductosNoAtendidos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnLIstadoProductosNoAtendidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLIstadoProductosNoAtendidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLIstadoProductosNoAtendidos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLIstadoProductosNoAtendidos.Location = new System.Drawing.Point(1146, 235);
		this.btnLIstadoProductosNoAtendidos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnLIstadoProductosNoAtendidos.Name = "btnLIstadoProductosNoAtendidos";
		this.btnLIstadoProductosNoAtendidos.Size = new System.Drawing.Size(187, 37);
		this.btnLIstadoProductosNoAtendidos.TabIndex = 64;
		this.btnLIstadoProductosNoAtendidos.Text = "Listado de Prod. No Atendidos";
		this.btnLIstadoProductosNoAtendidos.UseVisualStyleBackColor = true;
		this.btnLIstadoProductosNoAtendidos.Click += new System.EventHandler(btnLIstadoProductosNoAtendidos_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.Location = new System.Drawing.Point(1232, 19);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(109, 46);
		this.btnSalir.TabIndex = 63;
		this.btnSalir.Text = "Cancelar";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.Location = new System.Drawing.Point(1126, 19);
		this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(98, 46);
		this.btnGuardar.TabIndex = 62;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.gbopcionesgrgenerada.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gbopcionesgrgenerada.Controls.Add(this.btncrearproduct);
		this.gbopcionesgrgenerada.Controls.Add(this.btnAgregarProdNoSolicitado);
		this.gbopcionesgrgenerada.Controls.Add(this.btnAgregarProductoPromocion);
		this.gbopcionesgrgenerada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.gbopcionesgrgenerada.Location = new System.Drawing.Point(1140, 116);
		this.gbopcionesgrgenerada.Name = "gbopcionesgrgenerada";
		this.gbopcionesgrgenerada.Size = new System.Drawing.Size(201, 113);
		this.gbopcionesgrgenerada.TabIndex = 61;
		this.gbopcionesgrgenerada.TabStop = false;
		this.gbopcionesgrgenerada.Text = "OPCIONES";
		this.btncrearproduct.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btncrearproduct.BackColor = System.Drawing.Color.LimeGreen;
		this.btncrearproduct.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btncrearproduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btncrearproduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btncrearproduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btncrearproduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btncrearproduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btncrearproduct.Location = new System.Drawing.Point(6, 82);
		this.btncrearproduct.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btncrearproduct.Name = "btncrearproduct";
		this.btncrearproduct.Size = new System.Drawing.Size(187, 25);
		this.btncrearproduct.TabIndex = 62;
		this.btncrearproduct.Text = "Crear Prod.";
		this.btncrearproduct.UseVisualStyleBackColor = false;
		this.btncrearproduct.Click += new System.EventHandler(btncrearproduct_Click);
		this.btnAgregarProdNoSolicitado.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAgregarProdNoSolicitado.BackColor = System.Drawing.SystemColors.ActiveBorder;
		this.btnAgregarProdNoSolicitado.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAgregarProdNoSolicitado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnAgregarProdNoSolicitado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnAgregarProdNoSolicitado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAgregarProdNoSolicitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAgregarProdNoSolicitado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAgregarProdNoSolicitado.Location = new System.Drawing.Point(7, 47);
		this.btnAgregarProdNoSolicitado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnAgregarProdNoSolicitado.Name = "btnAgregarProdNoSolicitado";
		this.btnAgregarProdNoSolicitado.Size = new System.Drawing.Size(187, 25);
		this.btnAgregarProdNoSolicitado.TabIndex = 61;
		this.btnAgregarProdNoSolicitado.Text = "Agregar Prod. No Solicitado";
		this.btnAgregarProdNoSolicitado.UseVisualStyleBackColor = false;
		this.btnAgregarProdNoSolicitado.Click += new System.EventHandler(btnAgregarProdNoSolicitado_Click);
		this.btnAgregarProductoPromocion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAgregarProductoPromocion.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.btnAgregarProductoPromocion.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAgregarProductoPromocion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnAgregarProductoPromocion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnAgregarProductoPromocion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAgregarProductoPromocion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAgregarProductoPromocion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAgregarProductoPromocion.Location = new System.Drawing.Point(7, 17);
		this.btnAgregarProductoPromocion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnAgregarProductoPromocion.Name = "btnAgregarProductoPromocion";
		this.btnAgregarProductoPromocion.Size = new System.Drawing.Size(187, 26);
		this.btnAgregarProductoPromocion.TabIndex = 60;
		this.btnAgregarProductoPromocion.Text = "Agregar Prod. Promocion";
		this.btnAgregarProductoPromocion.UseVisualStyleBackColor = false;
		this.btnAgregarProductoPromocion.Click += new System.EventHandler(btnAgregarProductoPromocion_Click);
		this.dtfechaingresoalmacen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfechaingresoalmacen.Location = new System.Drawing.Point(131, 79);
		this.dtfechaingresoalmacen.Name = "dtfechaingresoalmacen";
		this.dtfechaingresoalmacen.Size = new System.Drawing.Size(127, 20);
		this.dtfechaingresoalmacen.TabIndex = 55;
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(15, 81);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(104, 13);
		this.label21.TabIndex = 54;
		this.label21.Text = "F. Ingreso Almacen :";
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(268, 27);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(92, 13);
		this.label20.TabIndex = 53;
		this.label20.Text = "N. Orden Compra:";
		this.txtnumeroOc.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtnumeroOc.ForeColor = System.Drawing.SystemColors.WindowFrame;
		this.txtnumeroOc.Location = new System.Drawing.Point(380, 25);
		this.txtnumeroOc.Name = "txtnumeroOc";
		this.txtnumeroOc.ReadOnly = true;
		this.txtnumeroOc.Size = new System.Drawing.Size(115, 20);
		this.txtnumeroOc.TabIndex = 52;
		this.txtnumeroOc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtnumeroOc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtnumeroOc_MouseDoubleClick);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(128, 26);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(39, 13);
		this.label5.TabIndex = 49;
		this.label5.Text = "GRC - ";
		this.txtNumero.Location = new System.Drawing.Point(167, 23);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(91, 20);
		this.txtNumero.TabIndex = 2;
		this.tTMensajeInformacion.SetToolTip(this.txtNumero, "Indique el Numero de la Guia de Remision");
		this.cmbMotivo.Enabled = false;
		this.cmbMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMotivo.FormattingEnabled = true;
		this.cmbMotivo.Items.AddRange(new object[14]
		{
			"Venta", "Venta sujeta a confirmación del comprador", "Compra", "Consignación", "Devolución", "Traslado entre Establec. de la misma empresa", "Traslado de bienes para trasnformación", "Recojo de Bienes", "Traslado por bienes itinerante de comprob. de pago", "Traslado zona primaria",
			"Importacion", "Exportacion", "Venta con entrega a terceros", "Otros"
		});
		this.cmbMotivo.Location = new System.Drawing.Point(380, 79);
		this.cmbMotivo.Name = "cmbMotivo";
		this.cmbMotivo.Size = new System.Drawing.Size(115, 20);
		this.cmbMotivo.TabIndex = 4;
		this.cmbMotivo.Tag = "5";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(288, 80);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(72, 13);
		this.label18.TabIndex = 40;
		this.label18.Text = "Motivo Trans.";
		this.dtpFechaTransporte.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaTransporte.Location = new System.Drawing.Point(407, 104);
		this.dtpFechaTransporte.Name = "dtpFechaTransporte";
		this.dtpFechaTransporte.Size = new System.Drawing.Size(127, 20);
		this.dtpFechaTransporte.TabIndex = 3;
		this.dtpFechaTransporte.Visible = false;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(322, 110);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(73, 13);
		this.label17.TabIndex = 38;
		this.label17.Text = "F. Transporte:";
		this.label17.Visible = false;
		this.gbTransporte.Controls.Add(this.txtDireccionTransporte);
		this.gbTransporte.Controls.Add(this.label16);
		this.gbTransporte.Controls.Add(this.txtRazonSocialTransporte);
		this.gbTransporte.Controls.Add(this.label14);
		this.gbTransporte.Controls.Add(this.label13);
		this.gbTransporte.Controls.Add(this.txtRUCTransporte);
		this.gbTransporte.Location = new System.Drawing.Point(282, 266);
		this.gbTransporte.Name = "gbTransporte";
		this.gbTransporte.Size = new System.Drawing.Size(445, 100);
		this.gbTransporte.TabIndex = 37;
		this.gbTransporte.TabStop = false;
		this.gbTransporte.Text = "Empresa de Tranportes";
		this.txtDireccionTransporte.Location = new System.Drawing.Point(84, 71);
		this.txtDireccionTransporte.Name = "txtDireccionTransporte";
		this.txtDireccionTransporte.ReadOnly = true;
		this.txtDireccionTransporte.Size = new System.Drawing.Size(343, 20);
		this.txtDireccionTransporte.TabIndex = 20;
		this.txtDireccionTransporte.Tag = "5";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(17, 74);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(52, 13);
		this.label16.TabIndex = 16;
		this.label16.Text = "Dirección";
		this.txtRazonSocialTransporte.Location = new System.Drawing.Point(84, 45);
		this.txtRazonSocialTransporte.Name = "txtRazonSocialTransporte";
		this.txtRazonSocialTransporte.ReadOnly = true;
		this.txtRazonSocialTransporte.Size = new System.Drawing.Size(343, 20);
		this.txtRazonSocialTransporte.TabIndex = 19;
		this.txtRazonSocialTransporte.Tag = "5";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(17, 22);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(30, 13);
		this.label14.TabIndex = 14;
		this.label14.Text = "RUC";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(17, 48);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(61, 13);
		this.label13.TabIndex = 13;
		this.label13.Text = "Raz. Social";
		this.txtRUCTransporte.BackColor = System.Drawing.SystemColors.Info;
		this.txtRUCTransporte.Location = new System.Drawing.Point(84, 19);
		this.txtRUCTransporte.Name = "txtRUCTransporte";
		this.txtRUCTransporte.Size = new System.Drawing.Size(147, 20);
		this.txtRUCTransporte.TabIndex = 18;
		this.txtRUCTransporte.Tag = "5";
		this.txtRUCTransporte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUCTransporte_KeyDown);
		this.groupBox4.Controls.Add(this.btnRecargarConductor);
		this.groupBox4.Controls.Add(this.btnRecargarAutos);
		this.groupBox4.Controls.Add(this.cmbConductor);
		this.groupBox4.Controls.Add(this.cmbVehiculos);
		this.groupBox4.Controls.Add(this.txtMarcaVehiculo);
		this.groupBox4.Controls.Add(this.txtLicencia);
		this.groupBox4.Controls.Add(this.label12);
		this.groupBox4.Controls.Add(this.txtConstancia);
		this.groupBox4.Controls.Add(this.label11);
		this.groupBox4.Controls.Add(this.label10);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Location = new System.Drawing.Point(534, 380);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(485, 100);
		this.groupBox4.TabIndex = 36;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos del Transporte / Conductor";
		this.groupBox4.Visible = false;
		this.btnRecargarConductor.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnRecargarConductor.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnRecargarConductor.FlatAppearance.BorderSize = 0;
		this.btnRecargarConductor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnRecargarConductor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnRecargarConductor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRecargarConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRecargarConductor.Image = SIGEFA.Properties.Resources.cambio;
		this.btnRecargarConductor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRecargarConductor.Location = new System.Drawing.Point(438, 43);
		this.btnRecargarConductor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnRecargarConductor.Name = "btnRecargarConductor";
		this.btnRecargarConductor.Size = new System.Drawing.Size(41, 25);
		this.btnRecargarConductor.TabIndex = 66;
		this.tTMensajeInformacion.SetToolTip(this.btnRecargarConductor, "Recarga Lista Conductores");
		this.btnRecargarConductor.UseVisualStyleBackColor = true;
		this.btnRecargarConductor.Click += new System.EventHandler(btnRecargarConductor_Click);
		this.btnRecargarAutos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnRecargarAutos.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnRecargarAutos.FlatAppearance.BorderSize = 0;
		this.btnRecargarAutos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnRecargarAutos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnRecargarAutos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRecargarAutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRecargarAutos.Image = SIGEFA.Properties.Resources.cambio;
		this.btnRecargarAutos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRecargarAutos.Location = new System.Drawing.Point(219, 17);
		this.btnRecargarAutos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnRecargarAutos.Name = "btnRecargarAutos";
		this.btnRecargarAutos.Size = new System.Drawing.Size(41, 25);
		this.btnRecargarAutos.TabIndex = 62;
		this.tTMensajeInformacion.SetToolTip(this.btnRecargarAutos, "Recarga Lista Vehiculos");
		this.btnRecargarAutos.UseVisualStyleBackColor = true;
		this.btnRecargarAutos.Click += new System.EventHandler(btnRecargarAutos_Click);
		this.cmbConductor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbConductor.FormattingEnabled = true;
		this.cmbConductor.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cmbConductor.Location = new System.Drawing.Point(77, 46);
		this.cmbConductor.Name = "cmbConductor";
		this.cmbConductor.Size = new System.Drawing.Size(354, 20);
		this.cmbConductor.TabIndex = 65;
		this.cmbConductor.SelectionChangeCommitted += new System.EventHandler(cmbConductor_SelectionChangeCommitted);
		this.cmbVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbVehiculos.FormattingEnabled = true;
		this.cmbVehiculos.Items.AddRange(new object[5] { "EXCELENTE", "BUENO", "REGULAR", "MALO", "MOROSO" });
		this.cmbVehiculos.Location = new System.Drawing.Point(77, 20);
		this.cmbVehiculos.Name = "cmbVehiculos";
		this.cmbVehiculos.Size = new System.Drawing.Size(135, 20);
		this.cmbVehiculos.TabIndex = 64;
		this.cmbVehiculos.SelectionChangeCommitted += new System.EventHandler(cmbVehiculos_SelectionChangeCommitted);
		this.txtMarcaVehiculo.Location = new System.Drawing.Point(326, 19);
		this.txtMarcaVehiculo.Name = "txtMarcaVehiculo";
		this.txtMarcaVehiculo.ReadOnly = true;
		this.txtMarcaVehiculo.Size = new System.Drawing.Size(153, 20);
		this.txtMarcaVehiculo.TabIndex = 14;
		this.txtMarcaVehiculo.Tag = "20";
		this.txtLicencia.Location = new System.Drawing.Point(344, 73);
		this.txtLicencia.Name = "txtLicencia";
		this.txtLicencia.ReadOnly = true;
		this.txtLicencia.Size = new System.Drawing.Size(135, 20);
		this.txtLicencia.TabIndex = 17;
		this.txtLicencia.Tag = "20";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(274, 76);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(64, 13);
		this.label12.TabIndex = 35;
		this.label12.Tag = "20";
		this.label12.Text = "N° Lic. Con.";
		this.txtConstancia.Location = new System.Drawing.Point(77, 73);
		this.txtConstancia.Name = "txtConstancia";
		this.txtConstancia.ReadOnly = true;
		this.txtConstancia.Size = new System.Drawing.Size(135, 20);
		this.txtConstancia.TabIndex = 16;
		this.txtConstancia.Tag = "20";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(15, 76);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(54, 13);
		this.label11.TabIndex = 33;
		this.label11.Tag = "20";
		this.label11.Text = "Cons. Ins.";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(15, 49);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(56, 13);
		this.label10.TabIndex = 14;
		this.label10.Text = "Conductor";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(283, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(37, 13);
		this.label6.TabIndex = 12;
		this.label6.Text = "Marca";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(15, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(49, 13);
		this.label3.TabIndex = 0;
		this.label3.Text = "N° Placa";
		this.txtDireccionProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccionProveedor.Enabled = false;
		this.txtDireccionProveedor.Location = new System.Drawing.Point(101, 161);
		this.txtDireccionProveedor.Name = "txtDireccionProveedor";
		this.txtDireccionProveedor.ReadOnly = true;
		this.txtDireccionProveedor.Size = new System.Drawing.Size(484, 20);
		this.txtDireccionProveedor.TabIndex = 7;
		this.txtDireccionProveedor.Tag = "21";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(29, 164);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(52, 13);
		this.label4.TabIndex = 35;
		this.label4.Tag = "21";
		this.label4.Text = "Direccion";
		this.txtRazonSocialProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRazonSocialProveedor.Enabled = false;
		this.txtRazonSocialProveedor.Location = new System.Drawing.Point(196, 137);
		this.txtRazonSocialProveedor.Name = "txtRazonSocialProveedor";
		this.txtRazonSocialProveedor.ReadOnly = true;
		this.txtRazonSocialProveedor.Size = new System.Drawing.Size(389, 20);
		this.txtRazonSocialProveedor.TabIndex = 6;
		this.txtRazonSocialProveedor.Tag = "3";
		this.txtRucProveedor.BackColor = System.Drawing.Color.PeachPuff;
		this.txtRucProveedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRucProveedor.ForeColor = System.Drawing.Color.Black;
		this.txtRucProveedor.Location = new System.Drawing.Point(101, 137);
		this.txtRucProveedor.Name = "txtRucProveedor";
		this.txtRucProveedor.Size = new System.Drawing.Size(89, 20);
		this.txtRucProveedor.TabIndex = 3;
		this.txtRucProveedor.Tag = "5";
		this.txtRucProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtRucProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodProveedor_KeyDown);
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(29, 140);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(56, 13);
		this.label15.TabIndex = 20;
		this.label15.Tag = "5";
		this.label15.Text = "Proveedor";
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Enabled = false;
		this.btnDetalle.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnDetalle.Location = new System.Drawing.Point(1146, 87);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(188, 23);
		this.btnDetalle.TabIndex = 12;
		this.btnDetalle.Text = "Agregar Producto";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Visible = false;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(101, 186);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(484, 73);
		this.txtComentario.TabIndex = 11;
		this.txtComentario.Tag = "21";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(29, 189);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(40, 27);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(78, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Guia Remision:";
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(131, 53);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(127, 20);
		this.dtpFecha.TabIndex = 2;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(58, 53);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "F. Emision :";
		this.cmsFilaDgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.eliminarToolStripMenuItem });
		this.cmsFilaDgv.Name = "cmsFilaDgv";
		this.cmsFilaDgv.Size = new System.Drawing.Size(118, 26);
		this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
		this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
		this.eliminarToolStripMenuItem.Text = "Eliminar";
		this.eliminarToolStripMenuItem.Click += new System.EventHandler(eliminarToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(1365, 642);
		base.Controls.Add(this.btnAgregarProdPromocion);
		base.Controls.Add(this.groupBox1);
		this.MinimumSize = new System.Drawing.Size(1364, 39);
		base.Name = "frmGuiaRemisionCompra";
		this.Text = "Guia de Remision de Compra";
		base.Load += new System.EventHandler(frmGuiaRemisionCompra_Load);
		base.Shown += new System.EventHandler(frmGuiaRemisionCompra_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvdetalleguiaremisioncompra).EndInit();
		this.btnAgregarProdPromocion.ResumeLayout(false);
		this.btnAgregarProdPromocion.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNCCGeneradas).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.gbopcionesgrgenerada.ResumeLayout(false);
		this.gbTransporte.ResumeLayout(false);
		this.gbTransporte.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.cmsFilaDgv.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
