using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SpreadsheetLight;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmPropuestaDeOrdenCompra : Form
{
	public int CodProv = 0;

	public int CodPropuesta = 0;

	public int proceso = 0;

	public string proveedor;

	public string afectacion;

	public string afectacionseleccionada;

	private bool proviene_shown = false;

	private BindingSource data = new BindingSource();

	public DataTable tableDGV = new DataTable();

	public DataTable tableDatosTodos = new DataTable();

	private BindingSource dataTableDGV = new BindingSource();

	private clsValidar ok = new clsValidar();

	private clsAdmPropuestaDePedido admPropuestaDePedido = new clsAdmPropuestaDePedido();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion = new List<clsCotizacionPropuestaDePedido>();

	private clsPropuestaDePedido prop_pedi = new clsPropuestaDePedido();

	private List<clsDetallePropuestaDePedido> lista_detalle_prop = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_detalle_prop_insertar = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_detalle_prop_actualizar = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_detalle_prop_eliminar = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_detalle_prop_cot_selecc = new List<clsDetallePropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_insertar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_eliminar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_insertar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_actualizar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_eliminar = new List<clsCotizacionPropuestaDePedido>();

	public bool modoEdicion = false;

	private clsAdmPlantillaDeProductos admplanprod = new clsAdmPlantillaDeProductos();

	private System.Drawing.Color[] colores_columnas = new System.Drawing.Color[4]
	{
		System.Drawing.Color.FromArgb(0, 255, 179),
		System.Drawing.Color.FromArgb(226, 255, 0),
		System.Drawing.Color.FromArgb(255, 128, 0),
		System.Drawing.Color.FromArgb(255, 0, 202)
	};

	private int i = 0;

	private List<clsDetallePropuestaDePedido> lista_con_cotizacion = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_sin_cotizacion = new List<clsDetallePropuestaDePedido>();

	private TextBox txtedit = new TextBox();

	private System.Drawing.Color fondo_celda;

	private object valor_inicial_celda_editar;

	private object valor_correspondiente_a_precio;

	private int ind_col_correspondiente_a_precio = 0;

	private bool filas_añadidas = false;

	private DataTable tableCotizaciones = new DataTable();

	private BindingSource dataTableCotizacion = new BindingSource();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private List<clsDetallePropuestaDePedido> lista_vis_act = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_ins = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_act = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_del = new List<clsDetallePropuestaDePedido>();

	internal int codPlantilla;

	private Dictionary<int, int> valores_posiciones_nroitems = new Dictionary<int, int>();

	private Dictionary<int, int> codprod_valores_nroitems = new Dictionary<int, int>();

	private int ctdad_a_mostrar = 15;

	private DataTable dataComboModificaciones;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Button btnAnadirCotizacion;

	private Button btnNuevo;

	private Label label4;

	public TextBox txtEstadoPropuesta;

	private Label label3;

	public TextBox txtDescripPropuesta;

	private Label label2;

	public TextBox txtTituloPropuesta;

	private Label label1;

	public TextBox txtProveedor;

	private ContextMenuStrip contextMenuStrip1;

	public Button btnSalir;

	public Button btnGenerar;

	public Button btnGuardar;

	private Button btnQuitar;

	public TextBox txtDocCotizacion;

	private Label label5;

	private Label lblModoEdicion;

	private GroupBox gbCotizacion;

	private ComboBox cmbTipoValorCompra;

	private Label label7;

	private ComboBox cmbMoneda;

	private Label label6;

	public Button btnVerificadorParaCotizar;

	public Button btnVerListadoTotalPropuesta;

	private Button btnTotal;

	private GroupBox groupBox3;

	private DataGridView dgvCotizacion;

	public Button btnSeleccionarMontosMenores;

	public Button btnLimpiarPedidoFinal;

	public Button btnRellenarConCantidadSugerida;

	public Button btnRecalcularPropuesta;

	private ToolTip tTMensajes;

	private Label label8;

	public TextBox txtTipoCambio;

	public Button btnReportePDF;

	public Button btnExcel;

	private GroupBox gbcontienedgvordenesgeneradas;

	private DataGridView dgvOrdenesGeneradas;

	private Panel panelContenedorFormulario;

	private DataGridViewTextBoxColumn colDgvCotItem;

	private DataGridViewTextBoxColumn colDgvCotDocCot;

	private DataGridViewTextBoxColumn colDgvCotDesProveedor;

	private DataGridViewTextBoxColumn colDgvCotMoneda;

	private DataGridViewTextBoxColumn colDgvCotTipoPrecio;

	private DataGridViewTextBoxColumn colDgvCotOp_Grav;

	private DataGridViewTextBoxColumn colDgvCotIgv;

	private DataGridViewTextBoxColumn colDgvCotTotal;

	private RadGridView dgvlistadopropuesta;

	private Label label9;

	private ComboBox cmbcambiarmonedapuc;

	public Button btnAgregarProducto;

	private Label label10;

	public TextBox txtCodigoProp;

	private DataGridViewTextBoxColumn colNumOC;

	private DataGridViewTextBoxColumn colcodOC;

	private DataGridViewTextBoxColumn colNombreProveedor;

	private DataGridViewTextBoxColumn colEstado;

	public frmPropuestaDeOrdenCompra()
	{
		InitializeComponent();
	}

	private void txtProveedor_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmProveedoresLista"] != null)
			{
				Application.OpenForms["frmProveedoresLista"].Activate();
				return;
			}
			frmProveedoresLista form = new frmProveedoresLista();
			form.Proceso = 3;
			form.Procede = 10;
			form.ventana2 = this;
			form.ShowDialog();
		}
	}

	private void btnAnadirCotizacion_Click(object sender, EventArgs e)
	{
		if (CodProv != 0)
		{
			if (txtDocCotizacion.Text != "" && cmbTipoValorCompra.SelectedIndex != -1)
			{
				try
				{
					List<clsCotizacionPropuestaDePedido> elementos = Enumerable.Select<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>(Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == CodProv)), (Func<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>)((clsCotizacionPropuestaDePedido x) => x)).ToList();
					if (elementos.Count == 0)
					{
						añadiendoColumnaDeCotizacion(CodProv, txtProveedor.Text, txtDocCotizacion.Text);
						agregandoCotizacionaListado(CodProv, txtProveedor.Text, txtDocCotizacion.Text);
						btnNuevo.PerformClick();
						MessageBox.Show("La cotizacion a sido agregada exitosamente.", "Cotizacion Añadida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						recargaTablaCotizaciones();
					}
					else
					{
						MessageBox.Show("Proveedor ya agregado a cotizar", "Añadiendo Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			MessageBox.Show("Faltan datos para añadir la cotizacion", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtDocCotizacion.Focus();
		}
		else
		{
			MessageBox.Show("Seleccione un proveedor", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtProveedor.Focus();
		}
	}

	private void añadiendoColumnaDeCotizacion(int codProveedor, string descripProveedor, string docCotizacion)
	{
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
		BaseFormattingObject b1 = new BaseFormattingObject();
		b1.CellBackColor = colores_columnas[i];
		b1.ApplyOnSelectedRows = false;
		b1.Enabled = true;
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
		c2.RowBackColor = colores_columnas[i];
		c2.CellBackColor = colores_columnas[i];
		ConditionalFormattingObject c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
		c3.RowBackColor = colores_columnas[i];
		c3.CellBackColor = colores_columnas[i];
		GridViewCheckBoxColumn colCheckBox = new GridViewCheckBoxColumn();
		colCheckBox.HeaderText = "#";
		colCheckBox.FieldName = codProveedor.ToString();
		colCheckBox.Name = codProveedor.ToString();
		colCheckBox.Tag = descripProveedor;
		colCheckBox.ReadOnly = true;
		colCheckBox.Width = 50;
		colCheckBox.AllowFiltering = false;
		colCheckBox.AllowReorder = false;
		colCheckBox.AllowSort = false;
		colCheckBox.ConditionalFormattingObjectList.Add(c1);
		colCheckBox.ConditionalFormattingObjectList.Add(c2);
		colCheckBox.ConditionalFormattingObjectList.Add(c3);
		GridViewDecimalColumn colText = new GridViewDecimalColumn();
		colText.HeaderText = docCotizacion + "\n" + descripProveedor;
		colText.FieldName = descripProveedor;
		colText.Name = descripProveedor;
		colText.Width = 100;
		colText.DecimalPlaces = 4;
		colText.ReadOnly = false;
		colText.AllowFiltering = false;
		colText.AllowReorder = false;
		colText.AllowSort = false;
		colText.ConditionalFormattingObjectList.Add(c1);
		colText.ConditionalFormattingObjectList.Add(c2);
		colText.ConditionalFormattingObjectList.Add(c3);
		GridViewDecimalColumn colText2 = new GridViewDecimalColumn();
		colText2.HeaderText = docCotizacion + "\nTotalizado";
		colText2.FieldName = descripProveedor + " Totalizado";
		colText2.Name = descripProveedor + " Totalizado";
		colText2.Width = 100;
		colText2.DecimalPlaces = 4;
		colText2.ReadOnly = false;
		colText2.AllowFiltering = false;
		colText2.AllowReorder = false;
		colText2.AllowSort = false;
		colText2.ConditionalFormattingObjectList.Add(c1);
		colText2.ConditionalFormattingObjectList.Add(c2);
		colText2.ConditionalFormattingObjectList.Add(c3);
		dgvlistadopropuesta.Columns.Add(colCheckBox);
		dgvlistadopropuesta.Columns.Add(colText);
		dgvlistadopropuesta.Columns.Add(colText2);
		i++;
		if (i == 4)
		{
			i = 0;
		}
	}

	private void agregandoCotizacionaListado(int codProveedor, string descripProveedor, string docCotizacion)
	{
		clsCotizacionPropuestaDePedido aux = new clsCotizacionPropuestaDePedido();
		aux.CodigoProveedor = codProveedor;
		aux.DescripcionProveedor = descripProveedor;
		aux.DocCotizacion = docCotizacion;
		aux.CodigoMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
		aux.TipoPrecio = cmbTipoValorCompra.SelectedIndex;
		listado_cotizacion.Add(aux);
		aux.CodigoPropuesta = prop_pedi.Codigo;
		if (modoEdicion)
		{
			List<clsCotizacionPropuestaDePedido> item_cot_eli = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_eliminar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == aux.CodigoProveedor)).ToList();
			if (item_cot_eli.Count > 0)
			{
				aux.Codigo = item_cot_eli[0].Codigo;
				aux.ListadoPrecios = item_cot_eli[0].ListadoPrecios;
				listado_cotizacion_detalle_eliminar.Add(aux);
				listado_cotizacion_eliminar.Remove(item_cot_eli[0]);
			}
			else
			{
				aux.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
				listado_cotizacion_insertar.Add(aux);
			}
		}
	}

	private void frmPropuestaDeOrdenCompra_Load(object sender, EventArgs e)
	{
		txtCodigoProp.Text = "0";
		dgvlistadopropuesta.ReadOnly = false;
		cargacombomodificadorPUC();
		cargaMoneda();
		if (modoEdicion)
		{
			lblModoEdicion.Text = "MODO EDICION";
			lblModoEdicion.Visible = true;
			if (CodPropuesta != 0)
			{
				prop_pedi = admPropuestaDePedido.cargaPropuestaDePedido(CodPropuesta);
				llenarcabecerapropuesta(prop_pedi);
				dgvlistadopropuesta.AutoGenerateColumns = false;
				tableDGV = admPropuestaDePedido.cargaDetallePropuestaDePedido(prop_pedi.Codigo);
				data.DataSource = tableDGV;
				dgvlistadopropuesta.DataSource = data;
				clsPlantillaDeProductos plan = admplanprod.CargaProductoAgrupado(prop_pedi.CodPlantillaGenerada);
				if (prop_pedi.CodPlantillaGenerada == 0 || plan == null)
				{
					tableDatosTodos = admPropuestaDePedido.cargaDetallePropuestaDePedidoVisualizacion(prop_pedi.Codigo);
				}
				else
				{
					tableDatosTodos = admPropuestaDePedido.cargaDetallePropuestaDePedidoVisualizacionSegunPlantillaGenerada(prop_pedi.CodPlantillaGenerada, modoEdicion ? prop_pedi.Cod_almacen : frmLogin.iCodAlmacen, prop_pedi.Codigo);
				}
				int a = 1;
				if (tableDatosTodos.Rows.Count == 0 && prop_pedi.CodPlantillaGenerada == 0)
				{
					btnVerListadoTotalPropuesta.Visible = false;
					btnRecalcularPropuesta.Visible = false;
					btnRellenarConCantidadSugerida.Visible = false;
					if (prop_pedi.Estado != 1)
					{
						btnAgregarProducto.Visible = false;
					}
				}
				else
				{
					btnAgregarProducto.Visible = false;
				}
				listado_cotizacion = admPropuestaDePedido.cargaListaDeCotizacion(prop_pedi.Codigo);
				añadirListadoDeCotizacionADGV(listado_cotizacion);
				int au = tableDGV.Columns.Count;
				au++;
			}
			else
			{
				MessageBox.Show("CodPropuesta No Definida", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			txtEstadoPropuesta.Text = "NUEVO";
			gbCotizacion.Enabled = false;
			if (proceso == 3)
			{
				btnVerListadoTotalPropuesta.Visible = false;
				btnRecalcularPropuesta.Visible = false;
				btnRellenarConCantidadSugerida.Visible = false;
			}
			if (proceso == 0)
			{
				btnAgregarProducto.Visible = false;
			}
		}
		clsTipoCambio tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
		}
		else
		{
			txtTipoCambio.Text = "";
		}
		inicializatableCotizaciones();
	}

	private void cargacombomodificadorPUC()
	{
		dataComboModificaciones = admPropuestaDePedido.ListaTiposModificacionesParaPUC();
		cmbcambiarmonedapuc.DataSource = dataComboModificaciones;
		cmbcambiarmonedapuc.DisplayMember = "descripcion";
		cmbcambiarmonedapuc.ValueMember = "idTipoModificacion";
		cmbcambiarmonedapuc.SelectedIndex = 1;
	}

	private void llenarcabecerapropuesta(clsPropuestaDePedido prop_pedi)
	{
		txtDescripPropuesta.Text = prop_pedi.Descripcion;
		if (prop_pedi.Estado == 1)
		{
			txtEstadoPropuesta.Text = "NUEVO";
			gbCotizacion.Enabled = false;
		}
		else if (prop_pedi.Estado == 2)
		{
			txtEstadoPropuesta.Text = "COTIZANDO";
		}
		else if (prop_pedi.Estado == 3)
		{
			txtEstadoPropuesta.Text = "OC GENERADA";
			configurarParaEstadoGenerado();
			gbcontienedgvordenesgeneradas.Visible = true;
			dgvOrdenesGeneradas.DataSource = admPropuestaDePedido.listarOrdenesDeCompraGeneradas((CodPropuesta != 0) ? CodPropuesta : prop_pedi.Codigo);
			dgvOrdenesGeneradas.ClearSelection();
		}
		txtTituloPropuesta.Text = prop_pedi.Titulo;
		txtCodigoProp.Text = prop_pedi.Codigo.ToString();
	}

	private void configurarParaEstadoGenerado()
	{
		bool enable = false;
		dgvlistadopropuesta.ReadOnly = true;
		gbCotizacion.Enabled = enable;
		btnGuardar.Enabled = enable;
		btnVerificadorParaCotizar.Enabled = enable;
		btnGenerar.Enabled = enable;
		btnRecalcularPropuesta.Enabled = enable;
		btnRellenarConCantidadSugerida.Enabled = enable;
		btnLimpiarPedidoFinal.Enabled = enable;
		btnSeleccionarMontosMenores.Enabled = enable;
	}

	private void rellenarDatosDeCotizacionEnDGV(List<clsCotizacionPropuestaDePedido> listado_cotizacion)
	{
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
			{
				if (item.ListadoPrecios != null)
				{
					List<clsDetalleCotizacionPropuestaDePedido> elemento = Enumerable.Select<clsDetalleCotizacionPropuestaDePedido, clsDetalleCotizacionPropuestaDePedido>(Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item.ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == Convert.ToInt32(fila.Cells["colCodigoDetalle"].Value) && x.CodigoProducto == Convert.ToInt32(fila.Cells["colCodigoProducto"].Value))), (Func<clsDetalleCotizacionPropuestaDePedido, clsDetalleCotizacionPropuestaDePedido>)((clsDetalleCotizacionPropuestaDePedido x) => x)).ToList();
					if (elemento.Count > 0)
					{
						dgvlistadopropuesta.Rows[fila.Index].Cells[item.DescripcionProveedor].Value = elemento[0].PrecioCompra;
						GridViewCellEventArgs e = new GridViewCellEventArgs(fila, dgvlistadopropuesta.Columns[item.DescripcionProveedor], null);
						dgvlistadopropuesta_CellEndEdit(new object(), e);
					}
					continue;
				}
				break;
			}
		}
	}

	private void cargarCotizacionesMarcadas()
	{
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			clsCotizacionPropuestaDePedido aux = admPropuestaDePedido.cargaCotizacionDepropuestaDePedidoSeleccionada(Convert.ToInt32(fila.Cells["colCodigoDetalle"].Value ?? ((object)0)));
			if (aux != null)
			{
				fila.Cells[aux.CodigoProveedor.ToString()].Value = true;
			}
		}
	}

	private void añadirListadoDeCotizacionADGV(List<clsCotizacionPropuestaDePedido> listado_cotizacion)
	{
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			añadiendoColumnaDeCotizacion(item.CodigoProveedor, item.DescripcionProveedor, item.DocCotizacion);
		}
	}

	private void frmPropuestaDeOrdenCompra_Shown(object sender, EventArgs e)
	{
		proviene_shown = true;
		if (!modoEdicion)
		{
			btnVerListadoTotalPropuesta.PerformClick();
		}
		else if (listado_cotizacion != null)
		{
			rellenarDatosDeCotizacionEnDGV(listado_cotizacion);
			cargarCotizacionesMarcadas();
		}
		proviene_shown = false;
		recargaTablaCotizaciones();
	}

	private void cargaMoneda()
	{
		clsAdmMoneda AdmMoned = new clsAdmMoneda();
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CodProv = 0;
		txtProveedor.Text = "";
		txtDocCotizacion.Text = "";
		txtProveedor.Enabled = true;
	}

	public void conviertiendoDGVaData()
	{
		dgvlistadopropuesta.AutoGenerateColumns = true;
		foreach (GridViewDataColumn col in dgvlistadopropuesta.Columns)
		{
			if (col.FieldName != "")
			{
				tableDGV.Columns.Add(col.FieldName);
			}
		}
		dataTableDGV.DataSource = tableDGV;
		dgvlistadopropuesta.DataSource = dataTableDGV;
	}

	private void btnQuitar_Click(object sender, EventArgs e)
	{
		int AUX = tableDGV.Rows.Count;
		AUX++;
		if (CodProv != 0)
		{
			try
			{
				List<clsCotizacionPropuestaDePedido> elementos = Enumerable.Select<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>(Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == CodProv)), (Func<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>)((clsCotizacionPropuestaDePedido x) => x)).ToList();
				if (elementos.Count > 0)
				{
					quitandoColumnaDeCotizacion(elementos[0]);
					if (modoEdicion)
					{
						List<clsCotizacionPropuestaDePedido> item = Enumerable.Select<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>(Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == CodProv)), (Func<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>)((clsCotizacionPropuestaDePedido x) => x)).ToList();
						if (item.Count > 0)
						{
							listado_cotizacion_insertar.Remove(item[0]);
						}
						else
						{
							List<clsCotizacionPropuestaDePedido> item_1 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elementos[0].Codigo)).ToList();
							if (item_1.Count > 0)
							{
								listado_cotizacion_detalle_insertar.Remove(item_1[0]);
							}
							List<clsCotizacionPropuestaDePedido> item_2 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_actualizar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elementos[0].Codigo)).ToList();
							if (item_2.Count > 0)
							{
								listado_cotizacion_detalle_actualizar.Remove(item_2[0]);
							}
							List<clsCotizacionPropuestaDePedido> item_3 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_eliminar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elementos[0].Codigo)).ToList();
							if (item_3.Count > 0)
							{
								listado_cotizacion_detalle_eliminar.Remove(item_3[0]);
							}
							clsCotizacionPropuestaDePedido nuevo = obtenerNuevaCotizacion(elementos[0]);
							listado_cotizacion_eliminar.Add(nuevo);
						}
					}
					MessageBox.Show("Cotizacion de Proveedor Eliminada", "Quitando Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					recargaTablaCotizaciones();
				}
				else
				{
					MessageBox.Show("Proveedor no agregado a cotizar", "Quitando Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		MessageBox.Show("Seleccione un proveedor", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private void quitandoColumnaDeCotizacion(clsCotizacionPropuestaDePedido elemento)
	{
		listado_cotizacion.Remove(elemento);
		dgvlistadopropuesta.Columns.Remove(elemento.CodigoProveedor.ToString());
		dgvlistadopropuesta.Columns.Remove(elemento.DescripcionProveedor);
		dgvlistadopropuesta.Columns.Remove(elemento.DescripcionProveedor + " Totalizado");
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (verificadorDeGuardadoDePropuesta())
		{
			Close();
			return;
		}
		DialogResult rspta = MessageBox.Show("Esta seguro de salir. Los cambios que haya hecho sin guardar no se recuperaran.\n ¿Salir de todos modos?", Text + " dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (rspta == DialogResult.Yes)
		{
			Close();
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtTituloPropuesta.Text != "")
			{
				if (dgvlistadopropuesta.Rows.Count > 0)
				{
					bool band_good_job = false;
					using TransactionScope scope = new TransactionScope();
					if (modoEdicion)
					{
						obtenerDatosDePropuestaDeOrdenDeCompra();
						if (admPropuestaDePedido.actualizarPropuestaDePedido(prop_pedi))
						{
							if (!verificadorDeGuardadoDePropuesta())
							{
								int ins = 1;
								if (admPropuestaDePedido.actualizaDetallePropuestaDePedido(lista_detalle_prop_insertar, lista_detalle_prop_actualizar, lista_detalle_prop_cot_selecc, lista_detalle_prop_eliminar, listado_cotizacion_insertar, listado_cotizacion_eliminar, listado_cotizacion_detalle_insertar, listado_cotizacion_detalle_actualizar, listado_cotizacion_detalle_eliminar))
								{
									recargarPagina();
									MessageBox.Show("Propuesta Guardada Con Exito", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									band_good_job = true;
								}
								else
								{
									MessageBox.Show("Ocurrio Un Error Imprevisto Al Guardar La Propuesta de Orden De Compra", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
							else
							{
								MessageBox.Show("Propuesta Guardada Con Exito", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								band_good_job = true;
							}
						}
						else
						{
							MessageBox.Show("Ocurrio Un Error Imprevisto Al Guardar La Propuesta de Orden De Compra", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else
					{
						obtenerDatosDePropuestaDeOrdenDeCompra();
						añadiendoListadoAPropuestaDeCompra();
						obtenerDetalleListadoDeCotizacionesDeDGV(listado_cotizacion);
						prop_pedi.LCotizaciones = listado_cotizacion;
						obteniendoCotizacionSeleccionada();
						int codigo_propuesta_creada = -1;
						if (int.TryParse(admPropuestaDePedido.insertPropuestaDePedido_OC(prop_pedi).ToString(), out codigo_propuesta_creada))
						{
							if (codigo_propuesta_creada == -1)
							{
								MessageBox.Show("Ocurrio Un Error Imprevisto Al Guardar La Propuesta de Orden De Compra", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								List<clsDetallePropuestaDePedido> lista = obtenerListadoDeDataTableVisualizacionDetallePropuestaPedido(tableDatosTodos);
								admPropuestaDePedido.insertDetallePropuestaPedidoVisualizacion(codigo_propuesta_creada, lista);
								MessageBox.Show("Los datos se guardaron correctamente", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								band_good_job = true;
								lblModoEdicion.Text = "MODO EDICION";
								lblModoEdicion.Visible = true;
								modoEdicion = true;
								recargarPagina(codigo_propuesta_creada);
							}
						}
					}
					if (band_good_job)
					{
						scope.Complete();
					}
					else
					{
						Transaction.Current.Rollback();
					}
					return;
				}
				MessageBox.Show("No hay elemento que guardar en esta propuesta", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show("Indique un titulo de propuesta para guardar", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - " + Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void recargarPagina(int codPropuesta = 0)
	{
		frmPropuestaDeOrdenCompra form = new frmPropuestaDeOrdenCompra();
		form.Dock = DockStyle.Fill;
		form.WindowState = FormWindowState.Maximized;
		form.modoEdicion = true;
		form.MdiParent = base.MdiParent;
		if (codPropuesta == 0)
		{
			form.CodPropuesta = prop_pedi.Codigo;
			form.Text = prop_pedi.Titulo;
		}
		else
		{
			prop_pedi = admPropuestaDePedido.cargaPropuestaDePedido(codPropuesta);
			form.CodPropuesta = prop_pedi.Codigo;
			form.Text = prop_pedi.Titulo;
		}
		Close();
		form.Show();
	}

	private void recargarPaginaEstadoNueva(DataTable todos, DataTable table)
	{
		frmPropuestaDeOrdenCompra form = new frmPropuestaDeOrdenCompra();
		form.asignandoDatosDePlantilla(table);
		form.tableDatosTodos = todos;
		form.MdiParent = base.MdiParent;
		form.txtTituloPropuesta.Text = txtTituloPropuesta.Text;
		form.txtDescripPropuesta.Text = txtDescripPropuesta.Text;
		Close();
		form.Show();
	}

	private List<clsDetallePropuestaDePedido> obtenerListadoDeDataTableVisualizacionDetallePropuestaPedido(DataTable tableDatos)
	{
		List<clsDetallePropuestaDePedido> aux = new List<clsDetallePropuestaDePedido>();
		foreach (DataRow fila in tableDatos.Rows)
		{
			clsDetallePropuestaDePedido aux2 = new clsDetallePropuestaDePedido();
			aux2 = convertirFilaEnClase(fila);
			if (modoEdicion)
			{
				List<DataRow> det = (from x in tableDatosTodos.AsEnumerable()
					where Convert.ToInt32(x.Field<object>("codProd")) == aux2.Codigo_Producto
					select x).ToList();
				if (det.Count > 0)
				{
					aux2.Codigo = Convert.ToInt32(det[0].Field<object>("codigoDetallePropuesta"));
				}
			}
			aux2.OpcionRecuento = Convert.ToInt32(fila.Field<object>("opcionRecuento"));
			aux2.StockMinimo = Convert.ToDouble(fila.Field<object>("stockMinimo"));
			aux2.StockMaximo = Convert.ToDouble(fila.Field<object>("stockMaximo"));
			aux2.UnidadXPaquete = Convert.ToDouble(fila.Field<object>("undxPaquete"));
			aux.Add(aux2);
		}
		return aux;
	}

	private void obteniendoCotizacionSeleccionada()
	{
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			string nameColumna = getColumnaSeleccionada(fila.Index);
			if (nameColumna != "")
			{
				List<clsDetallePropuestaDePedido> elementos = Enumerable.Select<clsDetallePropuestaDePedido, clsDetallePropuestaDePedido>(Enumerable.Where<clsDetallePropuestaDePedido>(prop_pedi.LDetalle.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo_Producto == Convert.ToInt32(fila.Cells["colCodigoProducto"].Value))), (Func<clsDetallePropuestaDePedido, clsDetallePropuestaDePedido>)((clsDetallePropuestaDePedido x) => x)).ToList();
				if (elementos.Count != 1)
				{
					throw new ArgumentOutOfRangeException();
				}
				int ind = prop_pedi.LDetalle.IndexOf(elementos[0]);
				prop_pedi.LDetalle[ind].Cod_proveedor_seleccionado = Convert.ToInt32(nameColumna);
			}
		}
	}

	private void añadiendoListadoAPropuestaDeCompra()
	{
		dgvlistadopropuesta.ClearSelection();
		lista_detalle_prop.Clear();
		foreach (DataRow fila in tableDGV.Rows)
		{
			clsDetallePropuestaDePedido pdp = convertirFilaEnClase(fila);
			lista_detalle_prop.Add(pdp);
		}
		prop_pedi.LDetalle = lista_detalle_prop;
	}

	private clsDetallePropuestaDePedido convertirFilaEnClase(DataRow fila)
	{
		clsDetallePropuestaDePedido aux = new clsDetallePropuestaDePedido();
		aux.NroItem = Convert.ToInt32(fila.Field<object>("nroItem"));
		aux.Cantidad_reponer = Convert.ToDouble(fila.Field<object>("ctdadReponer"));
		aux.Cantidad_sugerida = Convert.ToDouble(fila.Field<object>("ctdadSugerida"));
		if (modoEdicion)
		{
			if (int.TryParse((fila.Field<object>("codigoDetallePropuesta") ?? " ").ToString(), out var codigoDetalle))
			{
				aux.Codigo = codigoDetalle;
			}
			else
			{
				aux.Codigo = -1;
			}
			aux.Cod_Propuesta = prop_pedi.Codigo;
		}
		aux.Codigo_Producto = Convert.ToInt32(fila.Field<object>("codProd"));
		aux.Codigo_Unidad = Convert.ToInt32(fila.Field<object>("codUnidad"));
		aux.Descripcion_Unidad = fila.Field<object>("descripUnidad").ToString();
		aux.Descrip_Producto = fila.Field<object>("descripProd").ToString();
		if (double.TryParse((fila.Field<object>("pedidoFinal") ?? " ").ToString(), out var pedido_final))
		{
			aux.Pedido_final = pedido_final;
		}
		else
		{
			aux.Pedido_final = double.NaN;
		}
		string ult_precio_unit = fila.Field<object>("precioUnitarioCompra").ToString();
		if (ult_precio_unit.Contains(" ") || ult_precio_unit.Contains("S"))
		{
			string nuevo = ult_precio_unit.Substring(2);
			ult_precio_unit = nuevo;
		}
		aux.Precio_unit_actual = Convert.ToDouble(ult_precio_unit);
		aux.Ref_Producto = fila.Field<object>("refProd").ToString();
		aux.StockDisponible = Convert.ToDouble(fila.Field<object>("stockDisponible"));
		return aux;
	}

	private void obtenerDatosDePropuestaDeOrdenDeCompra()
	{
		prop_pedi.Cod_usuario = frmLogin.iCodUser;
		prop_pedi.Nombre_usuario = frmLogin.sNombreUser + " " + frmLogin.sApellidoUSer;
		prop_pedi.Tipo = 2;
		prop_pedi.Titulo = txtTituloPropuesta.Text;
		prop_pedi.Fechaedicion = DateTime.Now;
		prop_pedi.Descripcion = txtDescripPropuesta.Text;
		if (!modoEdicion)
		{
			prop_pedi.CodPlantillaGenerada = codPlantilla;
			prop_pedi.Estado = 1;
			prop_pedi.Cod_almacen = frmLogin.iCodAlmacen;
			prop_pedi.Descrip_almacen = frmLogin.sAlmacen;
			prop_pedi.Fecharegitro = DateTime.Now;
		}
	}

	private void obtenerDetalleListadoDeCotizacionesDeDGV(List<clsCotizacionPropuestaDePedido> listado)
	{
		foreach (clsCotizacionPropuestaDePedido co_pdoc in listado)
		{
			co_pdoc.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
		}
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			int indice_cot = -1;
			int i = 0;
			foreach (clsCotizacionPropuestaDePedido co_pdoc2 in listado)
			{
				indice_cot = listado.IndexOf(co_pdoc2);
				clsDetalleCotizacionPropuestaDePedido dcpdp = new clsDetalleCotizacionPropuestaDePedido();
				if (double.TryParse((fila.Cells[co_pdoc2.DescripcionProveedor].Value ?? " ").ToString(), out var aux_precio_compra))
				{
					dcpdp.PrecioCompra = aux_precio_compra;
					dcpdp.CodigoCotizacion = co_pdoc2.Codigo;
					dcpdp.CodigoDetallePropuesta = Convert.ToInt32((fila.Cells["colCodigoDetalle"].Value == "") ? "0" : fila.Cells["colCodigoDetalle"].Value);
					dcpdp.CodigoProducto = Convert.ToInt32(fila.Cells["colCodigoProducto"].Value);
				}
				else
				{
					aux_precio_compra = double.NaN;
				}
				if (!double.IsNaN(aux_precio_compra))
				{
					if (co_pdoc2.ListadoPrecios == null)
					{
						co_pdoc2.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
					}
					co_pdoc2.ListadoPrecios.Add(dcpdp);
				}
				i++;
			}
		}
	}

	private void btnGenerar_Click(object sender, EventArgs e)
	{
		try
		{
			if (verificadorDeGuardadoDePropuesta())
			{
				string itemconpedidosvacios = validacionDePedidoFinal();
				if (itemconpedidosvacios == "")
				{
					string itemsincotizacionseleccionada = validaCotizacionSeleccionada();
					if (itemsincotizacionseleccionada != "")
					{
						DialogResult rpta = MessageBox.Show("ADVERTENCIA LOS SIGUIENTES PRODUCTOS NO TIENEN COTIZACION:" + itemsincotizacionseleccionada + "\nContinuar de todas formas?", "Generando Orden De Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (rpta == DialogResult.No)
						{
							return;
						}
					}
					int ctdadOCgenerar = 0;
					foreach (clsCotizacionPropuestaDePedido cot in listado_cotizacion)
					{
						List<clsDetalleOrdenCompra> detalle = obtenerDetalleOrdenDeCompraDeCotizacion(cot);
						if (detalle.Count > 0)
						{
							ctdadOCgenerar++;
						}
					}
					if (ctdadOCgenerar > 0)
					{
						DialogResult rpta2 = MessageBox.Show("Se GENERARAN " + ctdadOCgenerar + " Ordenes de Compra.\n Esta seguro de continuar?", "Generando Orden De Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
						if (rpta2 == DialogResult.No)
						{
							return;
						}
						clsAdmOrdenCompra admOrdCompra = new clsAdmOrdenCompra();
						string ordenes = "";
						bool band_m = false;
						foreach (clsCotizacionPropuestaDePedido cot2 in listado_cotizacion)
						{
							List<clsDetalleOrdenCompra> detalle2 = obtenerDetalleOrdenDeCompraDeCotizacion(cot2);
							if (detalle2.Count <= 0)
							{
								continue;
							}
							clsOrdenCompra orden = obtenerOrdenDeCompraDeCotizacion(detalle2, cot2);
							bool band = true;
							using (TransactionScope Scope = new TransactionScope())
							{
								if (admOrdCompra.insert(orden))
								{
									foreach (clsDetalleOrdenCompra det in detalle2)
									{
										det.CodOrdenCompra = orden.CodOrdenCompraNuevo;
										band = admOrdCompra.insertdetalle(det);
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
										ordenes = ordenes + "\n> " + orden.CodOrdenCompraNuevo.ToString().PadLeft(11, '0');
										band_m = true;
										Scope.Complete();
										Scope.Dispose();
									}
								}
								else
								{
									Transaction.Current.Rollback();
									Scope.Dispose();
								}
							}
							if (!band)
							{
								MessageBox.Show("Salio algo mal con respecto al guardado de orden de compra", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						if (band_m)
						{
							MessageBox.Show("Se generaron las siguientes ordenes de compra: " + ordenes + "\nSe cambiara el estado de la propuesta a \"OC GENERADO\"?", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							if (admPropuestaDePedido.cambiarEstadoPropuesta(prop_pedi.Codigo, 3))
							{
								prop_pedi.Fechageneracion = DateTime.Now;
								prop_pedi.Estado = 3;
								admPropuestaDePedido.actualizarPropuestaDePedido(prop_pedi);
								recargarPagina();
							}
							else
							{
								MessageBox.Show("Hubo Un problema al actualizar el estado de la propuesta.", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
					}
					else
					{
						MessageBox.Show("NO SE AH GENERADO NINGUNA ORDEN DE COMPRA. PROPUESTA NO ESTA COMPLETA", "Generando Orden De Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					MessageBox.Show("Los siguiente items no tienen pedido final: " + itemconpedidosvacios, "Generando Orden De Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("Guardar Los Cambios Para Poder Continuar", "Generando Orden De Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Generando Orden De Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private clsOrdenCompra obtenerOrdenDeCompraDeCotizacion(List<clsDetalleOrdenCompra> detalle, clsCotizacionPropuestaDePedido cot)
	{
		clsAdmTipoDocumento Admtipodoc = new clsAdmTipoDocumento();
		clsAdmSerie admser = new clsAdmSerie();
		clsTipoCambio tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
		clsTipoDocumento tidoc = Admtipodoc.CargaTipoDocumento(13);
		clsSerie serie = admser.BuscaSeriexDocumento(tidoc.CodTipoDocumento, prop_pedi.Cod_almacen);
		clsOrdenCompra or_com = new clsOrdenCompra();
		or_com.CodAlmacen = prop_pedi.Cod_almacen;
		or_com.CodProveedor = cot.CodigoProveedor;
		or_com.CodTipoDocumento = tidoc.CodTipoDocumento;
		or_com.CodSerie = serie.CodSerie;
		or_com.NumDoc = serie.Numeracion.ToString();
		or_com.Comentario = "Generado de Propuesta: " + prop_pedi.Titulo;
		or_com.FechaIngreso = DateTime.Now;
		or_com.CodUser = frmLogin.iCodUser;
		or_com.Moneda = cot.CodigoMoneda.ToString();
		or_com.TipoCambio = Convert.ToDecimal(tc.Venta);
		or_com.fleteTransportista = 1;
		or_com.codPropuestaPedido = prop_pedi.Codigo;
		foreach (clsDetalleOrdenCompra doc in detalle)
		{
			if (cot.TipoPrecio == 0)
			{
				or_com.MontoDscto += Convert.ToDecimal(doc.MontoDescuento);
				or_com.MontoBruto += Convert.ToDecimal(doc.Subtotal);
				or_com.Igv += Convert.ToDecimal(doc.Igv);
				or_com.Total += Convert.ToDecimal(doc.Importe);
			}
			else
			{
				or_com.MontoDscto += Convert.ToDecimal(doc.MontoDescuento);
				or_com.MontoBruto += Convert.ToDecimal(doc.Subtotal);
				or_com.Igv += Convert.ToDecimal(doc.Igv);
				or_com.Total += Convert.ToDecimal(doc.Importe);
			}
		}
		or_com.FormaPago = 0;
		or_com.estadoOrden = 1;
		return or_com;
	}

	private List<clsDetalleOrdenCompra> obtenerDetalleOrdenDeCompraDeCotizacion(clsCotizacionPropuestaDePedido cot)
	{
		List<clsDetalleOrdenCompra> detalle = new List<clsDetalleOrdenCompra>();
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			if (Convert.ToBoolean(fila.Cells[cot.CodigoProveedor.ToString()].Value ?? ((object)false)))
			{
				clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
				deta.CodProducto = Convert.ToInt32(fila.Cells["colCodigoProducto"].Value);
				deta.CodAlmacen = prop_pedi.Cod_almacen;
				deta.Moneda = cot.CodigoMoneda;
				deta.UnidadIngresada = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
				deta.SerieLote = "0";
				deta.Cantidad = Convert.ToDecimal(fila.Cells["colPedidoFinal"].Value);
				deta.cantidadpendiente = Convert.ToDecimal(fila.Cells["colPedidoFinal"].Value);
				deta.PrecioUnitario = Convert.ToDouble(fila.Cells[cot.DescripcionProveedor].Value);
				deta.Descuento1 = 0.0;
				deta.Descuento2 = 0.0;
				deta.Descuento3 = 0.0;
				double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
				double valorventa = 0.0;
				double precioventa = 0.0;
				if (cot.TipoPrecio == 0)
				{
					deta.ValoReal = Convert.ToDouble(fila.Cells[cot.DescripcionProveedor].Value);
					deta.PrecioReal = deta.ValoReal * factorigv;
				}
				else
				{
					deta.PrecioReal = Convert.ToDouble(fila.Cells[cot.DescripcionProveedor].Value);
					deta.ValoReal = deta.PrecioReal / factorigv;
				}
				valorventa = deta.ValoReal * Convert.ToDouble(deta.Cantidad);
				precioventa = deta.PrecioReal * Convert.ToDouble(deta.Cantidad);
				deta.Igv = precioventa - valorventa;
				deta.Importe = deta.PrecioReal * Convert.ToDouble(deta.Cantidad);
				deta.Subtotal = Math.Round(deta.PrecioReal * Convert.ToDouble(deta.cantidadpendiente), 2, MidpointRounding.AwayFromZero);
				deta.MontoDescuento = deta.Subtotal - deta.Importe;
				deta.FechaIngreso = DateTime.Now;
				deta.CodUser = frmLogin.iCodUser;
				deta.CodProveedor = Convert.ToInt32(cot.CodigoProveedor);
				deta.etiquetaint = 1;
				deta.etiqueta = "Prod. No Atendidos";
				deta.TipoPrecio = cot.TipoPrecio;
				deta.estadoOrden = 1;
				deta.psoli = 1;
				detalle.Add(deta);
			}
		}
		return detalle;
	}

	private string validaCotizacionSeleccionada()
	{
		string band = "";
		int ctdad_visibles = 5;
		int c_visibles = 0;
		int c_no_visibles = 0;
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			string codprov = getColumnaSeleccionada(fila.Index);
			string totalizado = "";
			if (codprov != "")
			{
				List<clsCotizacionPropuestaDePedido> coti = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == Convert.ToInt32(codprov))).ToList();
				totalizado = ((dgvlistadopropuesta.Rows[fila.Index].Cells[coti[0].DescripcionProveedor + " Totalizado"].Value == null) ? "" : dgvlistadopropuesta.Rows[fila.Index].Cells[coti[0].DescripcionProveedor + " Totalizado"].Value.ToString());
			}
			if (codprov == "" || totalizado == "")
			{
				c_visibles++;
				if (c_visibles <= ctdad_visibles)
				{
					band = band + "\n> " + fila.Cells["colReferenciaProducto"].Value.ToString();
				}
				else
				{
					c_no_visibles++;
				}
			}
		}
		if (c_no_visibles > 0)
		{
			band = band + " y " + c_no_visibles + " más.";
		}
		return band;
	}

	private string validacionDePedidoFinal()
	{
		string band = "";
		int ctdad_visibles = 5;
		int c_visibles = 0;
		int c_no_visibles = 0;
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			if (fila.Cells["colPedidoFinal"].Value == DBNull.Value || fila.Cells["colPedidoFinal"].Value == null || fila.Cells["colPedidoFinal"].Value == "")
			{
				c_visibles++;
				if (c_visibles <= ctdad_visibles)
				{
					band = band + "\n> " + fila.Cells["colReferenciaProducto"].Value.ToString();
				}
				else
				{
					c_no_visibles++;
				}
			}
		}
		if (c_no_visibles > 0)
		{
			band = band + " y " + c_no_visibles + " más.";
		}
		return band;
	}

	public void asignandoDatosDePlantilla(DataTable aux)
	{
		conviertiendoDGVaData();
		foreach (DataRow fila in aux.Rows)
		{
			tableDGV.Rows.Add(fila.ItemArray);
		}
	}

	private void dgvlistadopropuesta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvlistadopropuesta_celltextbox_KeyPress;
			txtedit.KeyPress += dgvlistadopropuesta_celltextbox_KeyPress;
		}
	}

	private void dgvlistadopropuesta_celltextbox_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosDecimales(e, sender as TextBox, 4);
	}

	private string getColumnaSeleccionada(int rowIndex)
	{
		string name_columna = "";
		foreach (clsCotizacionPropuestaDePedido elemento in listado_cotizacion)
		{
			if (Convert.ToBoolean(dgvlistadopropuesta.Rows[rowIndex].Cells[elemento.CodigoProveedor.ToString()].Value))
			{
				name_columna = elemento.CodigoProveedor.ToString();
				break;
			}
		}
		return name_columna;
	}

	private string getColumnaSeleccionada(int rowIndex, int columnIndex)
	{
		string name_columna = "";
		foreach (clsCotizacionPropuestaDePedido elemento in listado_cotizacion)
		{
			if (dgvlistadopropuesta.Columns[columnIndex].Name != elemento.CodigoProveedor.ToString() && Convert.ToBoolean(dgvlistadopropuesta.Rows[rowIndex].Cells[elemento.CodigoProveedor.ToString()].Value))
			{
				name_columna = elemento.CodigoProveedor.ToString();
				break;
			}
		}
		return name_columna;
	}

	private bool validaCheckBox(GridViewCellEventArgs e)
	{
		bool band = false;
		GridViewCellInfo aux = dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex];
		if (aux.ColumnInfo.GetType() == typeof(GridViewCheckBoxColumn))
		{
			band = true;
		}
		return band;
	}

	private bool isColumnaPrecioCotizacion(int columnIndex)
	{
		bool band = false;
		if (columnIndex > 10)
		{
			foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
			{
				if (columnIndex == dgvlistadopropuesta.Columns[item.DescripcionProveedor].Index)
				{
					band = true;
					break;
				}
			}
		}
		return band;
	}

	private bool isColumnaTotalCotizacion(int columnIndex)
	{
		bool band = false;
		if (columnIndex > 10)
		{
			foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
			{
				if (columnIndex == dgvlistadopropuesta.Columns[item.DescripcionProveedor + " Totalizado"].Index)
				{
					band = true;
					break;
				}
			}
		}
		return band;
	}

	private void gbCotizacion_Enter(object sender, EventArgs e)
	{
		if (!gbCotizacion.Enabled)
		{
			MessageBox.Show("Ingrese pedido final a productos");
		}
	}

	private void btnVerificadorParaCotizar_Click(object sender, EventArgs e)
	{
		if (verificadorDeGuardadoDePropuesta())
		{
			try
			{
				bool band = true;
				foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
				{
					if (!double.TryParse(fila.Cells["colPedidoFinal"].Value.ToString(), out var _))
					{
						band = false;
						break;
					}
				}
				if (band)
				{
					DialogResult rpta = MessageBox.Show("Los Pedidos Finales han sido verificados.\nDesea cambiar el estado de la propuesta a \"COTIZANDO\"?", Text + " dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					if (rpta == DialogResult.Yes)
					{
						if (admPropuestaDePedido.cambiarEstadoPropuesta(prop_pedi.Codigo, 2))
						{
							recargarPagina();
						}
						else
						{
							MessageBox.Show("Hubo Un problema al actualizar el estado de la propuesta.", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
				else
				{
					MessageBox.Show("Aun falta ingresar algunos pedidos finales.", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
		}
		MessageBox.Show("Hay datos sin guardar. Para poder verificar guarde los cambios.", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private bool verificadorDeGuardadoDePropuesta()
	{
		bool band = true;
		if (prop_pedi.Codigo != 0)
		{
			if (listado_cotizacion_insertar.Count > 0 || listado_cotizacion_eliminar.Count > 0 || listado_cotizacion_detalle_insertar.Count > 0 || lista_detalle_prop_actualizar.Count > 0 || lista_detalle_prop_eliminar.Count > 0 || lista_detalle_prop_insertar.Count > 0 || listado_cotizacion_detalle_insertar.Count > 0 || listado_cotizacion_detalle_actualizar.Count > 0 || listado_cotizacion_detalle_eliminar.Count > 0 || lista_detalle_prop_cot_selecc.Count > 0)
			{
				band = false;
			}
		}
		else
		{
			band = false;
		}
		return band;
	}

	private void limpiaCotizaciones(int filaIndex)
	{
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			dgvlistadopropuesta.Rows[filaIndex].Cells[item.DescripcionProveedor + " Totalizado"].Value = null;
		}
	}

	private clsCotizacionPropuestaDePedido obtenerNuevaCotizacion(clsCotizacionPropuestaDePedido elemento)
	{
		clsCotizacionPropuestaDePedido aux = new clsCotizacionPropuestaDePedido();
		aux.Codigo = elemento.Codigo;
		aux.CodigoMoneda = elemento.CodigoMoneda;
		aux.CodigoPropuesta = elemento.CodigoPropuesta;
		aux.CodigoProveedor = elemento.CodigoProveedor;
		aux.DescripcionProveedor = elemento.DescripcionProveedor;
		aux.DocCotizacion = elemento.DocCotizacion;
		aux.ListadoPrecios = elemento.ListadoPrecios;
		aux.TipoPrecio = elemento.TipoPrecio;
		return aux;
	}

	private void insertaOActualizaListadoDetalleCotizacion(List<clsCotizacionPropuestaDePedido> listado_actualizar, clsCotizacionPropuestaDePedido cot, clsDetalleCotizacionPropuestaDePedido detalle_cot)
	{
		List<clsCotizacionPropuestaDePedido> elemento = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_actualizar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == detalle_cot.CodigoCotizacion)).ToList();
		if (elemento.Count > 0)
		{
			List<clsDetalleCotizacionPropuestaDePedido> item_det = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(elemento[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == detalle_cot.CodigoDetallePropuesta)).ToList();
			if (item_det.Count > 0)
			{
				item_det[0].PrecioCompra = detalle_cot.PrecioCompra;
			}
			else
			{
				elemento[0].ListadoPrecios.Add(detalle_cot);
			}
		}
		else
		{
			cot.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
			cot.ListadoPrecios.Add(detalle_cot);
			listado_actualizar.Add(cot);
		}
	}

	private string getColumnaTotalizadoSegun(int columnIndex)
	{
		string nameColumna = "";
		string aux = "";
		foreach (GridViewDataColumn col in dgvlistadopropuesta.Columns)
		{
			if (col.Index == columnIndex)
			{
				aux = col.Name;
				break;
			}
		}
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			if (item.DescripcionProveedor == aux)
			{
				nameColumna = item.DescripcionProveedor + " Totalizado";
				break;
			}
		}
		return nameColumna;
	}

	private string getColumnaPrecioSegun(int columnIndex)
	{
		string nameColumna = "";
		string aux = "";
		foreach (GridViewDataColumn col in dgvlistadopropuesta.Columns)
		{
			if (col.Index == columnIndex)
			{
				aux = col.Name;
				break;
			}
		}
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			if (item.DescripcionProveedor + " Totalizado" == aux)
			{
				nameColumna = item.DescripcionProveedor;
				break;
			}
		}
		return nameColumna;
	}

	private void btnVerListadoTotalPropuesta_Click(object sender, EventArgs e)
	{
		try
		{
			object ola = dgvOrdenesGeneradas;
			frmTodosProductosPropuestaOrdenDeCompra form = new frmTodosProductosPropuestaOrdenDeCompra();
			form.tablaDatos = tableDatosTodos;
			form.ventana = this;
			if (prop_pedi.Fecharegitro != DateTime.MinValue)
			{
				form.fecha_mostrar = prop_pedi.Fecharegitro;
			}
			if (modoEdicion)
			{
				form.codigoPropuesta = prop_pedi.Codigo;
				form.estadoPropuesta = prop_pedi.Estado;
			}
			if (form.ShowDialog() == DialogResult.Yes)
			{
				recargarPagina();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void recargaFilasFinalesTotales()
	{
		if (filas_añadidas)
		{
			int cantidad_filas = tableDGV.Rows.Count;
			DataRow ultima = tableDGV.Rows[cantidad_filas - 1];
			DataRow penultima = tableDGV.Rows[cantidad_filas - 2];
			DataRow ante_penultima = tableDGV.Rows[cantidad_filas - 3];
		}
	}

	private void filasTotales(bool añadir)
	{
		if (añadir)
		{
			filas_añadidas = true;
			tableDGV.Rows.Add(3);
		}
		else
		{
			int cantidad_filas = tableDGV.Rows.Count;
			tableDGV.Rows.RemoveAt(cantidad_filas - 1);
			tableDGV.Rows.RemoveAt(cantidad_filas - 2);
			tableDGV.Rows.RemoveAt(cantidad_filas - 3);
			filas_añadidas = false;
		}
	}

	private void btnTotal_Click(object sender, EventArgs e)
	{
		object valor = dgvCotizacion.Rows[0].Cells[colDgvCotIgv.Name].Value;
		Type ter = valor.GetType();
		ter.ToString();
		recargaTablaCotizaciones();
	}

	private void inicializatableCotizaciones()
	{
		tableCotizaciones = new DataTable();
		tableCotizaciones.Columns.Add("item");
		tableCotizaciones.Columns.Add("docCotizacion");
		tableCotizaciones.Columns.Add("desProveedor");
		tableCotizaciones.Columns.Add("moneda");
		tableCotizaciones.Columns.Add("tipoprecio");
		tableCotizaciones.Columns.Add("op_grav");
		tableCotizaciones.Columns.Add("igv");
		tableCotizaciones.Columns.Add("total");
		dgvCotizacion.Columns[colDgvCotOp_Grav.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dgvCotizacion.Columns[colDgvCotTotal.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
		dgvCotizacion.Columns[colDgvCotIgv.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
	}

	private void recargaTablaCotizaciones()
	{
		tableCotizaciones.Clear();
		DataRow nueva = tableCotizaciones.NewRow();
		DataTable tmoneda = (DataTable)cmbMoneda.DataSource;
		int ind = 0;
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			ind++;
			List<DataRow> aux_mon = (from x in tmoneda.AsEnumerable()
				where Convert.ToInt32(x.ItemArray[0]) == item.CodigoMoneda
				select x).ToList();
			string moneda = "";
			if (aux_mon.Count > 0)
			{
				moneda = aux_mon[0].ItemArray[1].ToString();
			}
			string tipoprecio = cmbTipoValorCompra.Items[item.TipoPrecio].ToString();
			double suma = 0.0;
			foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
			{
				if (fila.Cells[item.DescripcionProveedor + " Totalizado"].Value != DBNull.Value && fila.Cells[item.DescripcionProveedor + " Totalizado"].Value != null && fila.Cells[item.DescripcionProveedor + " Totalizado"].Value != "")
				{
					suma += Convert.ToDouble(fila.Cells[item.DescripcionProveedor + " Totalizado"].Value);
				}
			}
			double op_grav = 0.0;
			double igv = 0.0;
			double total = 0.0;
			if (item.TipoPrecio == 1)
			{
				total = suma;
				op_grav = suma / (1.0 + frmLogin.Configuracion.IGV / 100.0);
				igv = total - op_grav;
			}
			else
			{
				op_grav = suma;
				igv = frmLogin.Configuracion.IGV / 100.0 * op_grav;
				total = op_grav + igv;
			}
			string simboloMoneda = ((item.CodigoMoneda == 1) ? "es-PE" : "en-US");
			string s_op_grav = op_grav.ToString("C4", CultureInfo.CreateSpecificCulture(simboloMoneda));
			string s_total = total.ToString("C4", CultureInfo.CreateSpecificCulture(simboloMoneda));
			string s_igv = igv.ToString("C4", CultureInfo.CreateSpecificCulture(simboloMoneda));
			tableCotizaciones.Rows.Add(ind, item.DocCotizacion, item.DescripcionProveedor, moneda, tipoprecio, s_op_grav, s_igv, s_total);
		}
		dataTableCotizacion.DataSource = tableCotizaciones;
		dgvCotizacion.DataSource = dataTableCotizacion;
		pintaFilasDGVCotizaciones();
	}

	private void pintaFilasDGVCotizaciones()
	{
		foreach (DataGridViewRow fila in (IEnumerable)dgvCotizacion.Rows)
		{
			System.Drawing.Color fondo = dgvlistadopropuesta.Columns[fila.Cells[colDgvCotDesProveedor.Name].Value.ToString()].ConditionalFormattingObjectList.First().CellBackColor;
			fila.DefaultCellStyle.BackColor = fondo;
		}
	}

	private DataTable recalculandoDatosParaPropuestaDeOrdenDeCompra(bool Visualizacion, DataTable TablaUsar = null)
	{
		if (TablaUsar == null)
		{
			TablaUsar = tableDatosTodos;
		}
		DataTable aux = new DataTable();
		aux.Columns.Add("nroItem");
		aux.Columns.Add("codigoDetallePropuesta");
		aux.Columns.Add("codProd");
		aux.Columns.Add("refProd");
		aux.Columns.Add("descripProd");
		aux.Columns.Add("codUnidad");
		aux.Columns.Add("descripUnidad");
		aux.Columns.Add("stockDisponible");
		aux.Columns.Add("ctdadReponer");
		aux.Columns.Add("ctdadSugerida");
		aux.Columns.Add("pedidoFinal");
		aux.Columns.Add("precioUnitarioCompra");
		if (Visualizacion)
		{
			aux.Columns.Add("opcionRecuento");
			aux.Columns.Add("stockMinimo");
			aux.Columns.Add("stockMaximo");
			aux.Columns.Add("undxPaquete");
		}
		int i = 0;
		int j = 0;
		foreach (DataRow fila in TablaUsar.Rows)
		{
			i++;
			clsDetallePropuestaDePedido detpro = convertirFilaEnClase(fila);
			if (modoEdicion)
			{
				List<DataRow> det = (from x in tableDGV.AsEnumerable()
					where Convert.ToInt32(x.Field<object>("codProd")) == detpro.Codigo_Producto
					select x).ToList();
				if (det.Count > 0)
				{
					detpro.Codigo = Convert.ToInt32(det[0].Field<object>("codigoDetallePropuesta"));
				}
				else
				{
					detpro.Codigo = 0;
				}
			}
			detpro.OpcionRecuento = Convert.ToInt32(fila.Field<object>("opcionRecuento"));
			detpro.StockMinimo = Convert.ToDouble(fila.Field<object>("stockMinimo") ?? ((object)double.NaN));
			detpro.StockMaximo = Convert.ToDouble(fila.Field<object>("stockMaximo"));
			detpro.UnidadXPaquete = Convert.ToDouble(fila.Field<object>("undxPaquete"));
			double ult_pre = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(detpro.Codigo_Producto, detpro.Codigo_Unidad, 0));
			clsProducto pro = AdmPro.CargaProductoDetalle(detpro.Codigo_Producto, modoEdicion ? prop_pedi.Cod_almacen : frmLogin.iCodAlmacen, 1, 0, 0);
			double ctdad_reponer = 0.0;
			double ctdad_sugerida = 0.0;
			if (detpro.OpcionRecuento == 0 && prop_pedi.CodPlantillaGenerada != 0)
			{
				throw new Exception("Plantilla Incompleta imposible recalcular propuesta");
			}
			if (detpro.OpcionRecuento == 1 && Convert.ToDouble(pro.StockDisponible) < detpro.StockMinimo)
			{
				ctdad_reponer = detpro.StockMaximo - Convert.ToDouble(pro.StockDisponible);
				ctdad_sugerida = ctdad_reponer / detpro.UnidadXPaquete;
				ctdad_sugerida = Math.Truncate(ctdad_sugerida) * detpro.UnidadXPaquete;
			}
			if (detpro.OpcionRecuento == 2 && Convert.ToDouble(pro.StockDisponible) <= detpro.StockMaximo)
			{
				ctdad_reponer = detpro.StockMaximo - Convert.ToDouble(pro.StockDisponible);
				ctdad_sugerida = ctdad_reponer / detpro.UnidadXPaquete;
				ctdad_sugerida = Math.Truncate(ctdad_sugerida) * detpro.UnidadXPaquete;
			}
			if (Visualizacion)
			{
				double undxPaquete = detpro.UnidadXPaquete;
				double stockmin = detpro.StockMinimo;
				if (double.IsNaN(detpro.StockMaximo))
				{
					throw new ArgumentOutOfRangeException();
				}
				double stockmax = detpro.StockMaximo;
				if (double.IsNaN(detpro.StockMaximo))
				{
					throw new ArgumentOutOfRangeException();
				}
				int opcionRecuento = detpro.OpcionRecuento;
				if (detpro.OpcionRecuento == 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				aux.Rows.Add(i, modoEdicion ? detpro.Codigo.ToString() : "", detpro.Codigo_Producto, detpro.Ref_Producto, detpro.Descrip_Producto, detpro.Codigo_Unidad, detpro.Descripcion_Unidad, pro.StockDisponible, ctdad_reponer, ctdad_sugerida, "", ult_pre, opcionRecuento, stockmin, stockmax, undxPaquete);
			}
			else if (ctdad_reponer > 0.0)
			{
				j++;
				aux.Rows.Add(j, modoEdicion ? detpro.Codigo.ToString() : "", detpro.Codigo_Producto, detpro.Ref_Producto, detpro.Descrip_Producto, detpro.Codigo_Unidad, detpro.Descripcion_Unidad, pro.StockDisponible, ctdad_reponer, ctdad_sugerida, "", ult_pre);
			}
		}
		return aux;
	}

	private void btnRecalcularPropuesta_Click(object sender, EventArgs e)
	{
		try
		{
			DataTable datosPlantilla = null;
			if (prop_pedi.CodPlantillaGenerada != 0)
			{
				datosPlantilla = admplanprod.cargadetalleproductosagrupados(prop_pedi.CodPlantillaGenerada);
				if (datosPlantilla != null && datosPlantilla.Rows.Count == 0)
				{
					DialogResult rpta = MessageBox.Show("PLANTILLA RELACIONADA NO EXISTE O AH SIDO ELIMINADA.\nDesea continuar el RECALCULAR en BASE al LISTADO TOTAL DE PROPUESTA?", "Recalcular Propuesta en Base de Listado Total de Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					datosPlantilla = null;
					if (rpta == DialogResult.No)
					{
						return;
					}
				}
			}
			else
			{
				DialogResult rpta2 = MessageBox.Show("PROPUESTA NO TIENE PLANTILLA RELACIONADA PARA RECALCULAR.\nDesea continuar el RECALCULAR en BASE al LISTADO TOTAL DE PROPUESTA?", "Recalcular Propuesta en Base de Listado Total de Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rpta2 == DialogResult.No)
				{
					return;
				}
			}
			DataTable nueva_vis = null;
			DataTable nueva = null;
			if (datosPlantilla == null)
			{
				nueva_vis = recalculandoDatosParaPropuestaDeOrdenDeCompra(Visualizacion: true);
				nueva = recalculandoDatosParaPropuestaDeOrdenDeCompra(Visualizacion: false);
			}
			else
			{
				ListadoProductos frm_aux = new ListadoProductos();
				frm_aux.dtDGgvDetalle1 = datosPlantilla;
				frm_aux.clsplantillaprod = admplanprod.CargaProductoAgrupado(prop_pedi.CodPlantillaGenerada);
				if (!frm_aux.validacionParaGeneracion())
				{
					throw new Exception("PLANTILLA RELACIONADA ESTA INCOMPLETA IMPOSIBLE RECALCULAR");
				}
				nueva_vis = frm_aux.creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: true);
				nueva = frm_aux.creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: false);
			}
			int aux = nueva.Rows.Count;
			if (modoEdicion)
			{
				obteniendoListadosDeDataTable(nueva, visualizacion: false);
				obteniendoListadosDeDataTable(nueva_vis, visualizacion: true);
				if (verificadorDeRecalculo() && nueva.Rows.Count > 0)
				{
					if (admPropuestaDePedido.actualizaDetallePropuestaRecalculo(lista_ins, lista_act, lista_del))
					{
						if (prop_pedi.CodPlantillaGenerada != 0 && datosPlantilla != null)
						{
							bool rpta3 = true;
							using (TransactionScope Scope = new TransactionScope())
							{
								rpta3 = admPropuestaDePedido.eliminaDetallePropuestaVisualizacion(prop_pedi.Codigo);
								if (!rpta3)
								{
									Transaction.Current.Rollback();
									Scope.Dispose();
									throw new Exception("Ocurrio un error al eliminar la propuesta de visualizacion");
								}
								rpta3 = admPropuestaDePedido.insertDetallePropuestaPedidoVisualizacion(prop_pedi.Codigo, lista_vis_act);
								if (!rpta3)
								{
									Transaction.Current.Rollback();
									Scope.Dispose();
									throw new Exception("Ocurrio un error al guardar la nueva propuesta de visualizacion");
								}
								Scope.Complete();
								Scope.Dispose();
							}
							if (rpta3)
							{
								MessageBox.Show("La recalculacion de la propuesta finalizado de forma exitosa", "REALCULO DE PROPUESTA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
						else
						{
							admPropuestaDePedido.actualizaDetallePropuestaRecalculoVisualizacion(lista_vis_act);
						}
						recargarPagina();
					}
					else
					{
						MessageBox.Show("Ocurrio Un Error al Recalcular Propuesta", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("No se encontraron cambios para recalcular, la recalculacion de propuesta no arrojo ningun item", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				recargarPaginaEstadoNueva(nueva_vis, nueva);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool verificadorDeRecalculo()
	{
		bool band = false;
		if (lista_ins.Count > 0 || lista_act.Count > 0 || lista_del.Count > 0 || lista_vis_act.Count > 0)
		{
			band = true;
		}
		return band;
	}

	private void obteniendoListadosDeDataTable(DataTable tabla, bool visualizacion)
	{
		if (visualizacion)
		{
			lista_vis_act = obtenerListadoDeDataTableVisualizacionDetallePropuestaPedido(tabla);
			return;
		}
		foreach (DataRow fila in tabla.Rows)
		{
			List<DataRow> algo = (from x in tableDGV.AsEnumerable()
				where Convert.ToInt32(x.Field<object>("codProd")) == Convert.ToInt32(fila.Field<object>("codProd"))
				select x).ToList();
			int codigoDetalle;
			if (algo.Count == 0)
			{
				clsDetallePropuestaDePedido aux = convertirFilaEnClase(fila);
				lista_ins.Add(aux);
			}
			else if (!int.TryParse((fila.Field<object>("codigoDetallePropuesta") ?? " ").ToString(), out codigoDetalle))
			{
				fila.SetField("codigoDetallePropuesta", algo[0].Field<object>("codigoDetallePropuesta"));
			}
		}
		foreach (DataRow fila2 in tableDGV.Rows)
		{
			List<DataRow> algo2 = (from x in tabla.AsEnumerable()
				where Convert.ToInt32(x.Field<object>("codProd")) == Convert.ToInt32(fila2.Field<object>("codProd"))
				select x).ToList();
			if (algo2.Count > 0)
			{
				clsDetallePropuestaDePedido aux2 = convertirFilaEnClase(algo2[0]);
				List<DataRow> det = (from x in tableDGV.AsEnumerable()
					where Convert.ToInt32(x.Field<object>("codProd")) == aux2.Codigo_Producto
					select x).ToList();
				if (det.Count > 0)
				{
					aux2.Pedido_final = Convert.ToInt32(det[0].Field<object>("pedidoFinal"));
					double valor = aux2.Pedido_final;
					if (aux2.Pedido_final == 0.0)
					{
						aux2.Pedido_final = double.NaN;
					}
				}
				lista_act.Add(aux2);
			}
			else
			{
				clsDetallePropuestaDePedido aux3 = convertirFilaEnClase(fila2);
				lista_del.Add(aux3);
			}
		}
	}

	private void btnRellenarConCantidadSugerida_Click(object sender, EventArgs e)
	{
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			if (Convert.ToInt32(fila.Cells["colCtdadSugerida"].Value) != 0)
			{
				fila.Cells["colPedidoFinal"].Value = fila.Cells["colCtdadSugerida"].Value;
				GridViewCellEventArgs e2 = new GridViewCellEventArgs(fila, dgvlistadopropuesta.Columns["colPedidoFinal"], null);
				dgvlistadopropuesta_CellEndEdit(new object(), e2);
			}
		}
	}

	private void btnLimpiarPedidoFinal_Click(object sender, EventArgs e)
	{
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			fila.Cells["colPedidoFinal"].Value = DBNull.Value;
			GridViewCellEventArgs e2 = new GridViewCellEventArgs(fila, dgvlistadopropuesta.Columns["colPedidoFinal"], null);
			dgvlistadopropuesta_CellEndEdit(new object(), e2);
		}
	}

	private void btnSeleccionarMontosMenores_Click(object sender, EventArgs e)
	{
		double tipocambio = Convert.ToDouble(txtTipoCambio.Text);
		foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
		{
			int codProveedor = 0;
			double totalAnterior = 0.0;
			foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
			{
				object valor = fila.Cells[item.DescripcionProveedor + " Totalizado"].Value;
				if (valor != DBNull.Value && valor != null && valor != "")
				{
					double totalSoles = Convert.ToDouble(valor) * ((item.CodigoMoneda == 1) ? 1.0 : tipocambio);
					if (item.TipoPrecio != 1)
					{
						totalSoles += 0.18 * totalSoles;
					}
					if (codProveedor == 0)
					{
						codProveedor = item.CodigoProveedor;
						totalAnterior = totalSoles;
					}
					else if (totalAnterior > totalSoles)
					{
						codProveedor = item.CodigoProveedor;
						totalAnterior = totalSoles;
					}
				}
			}
			if (codProveedor != 0)
			{
				fila.Cells[codProveedor.ToString()].Value = false;
				GridViewCellEventArgs e2 = new GridViewCellEventArgs(fila, dgvlistadopropuesta.Columns[codProveedor.ToString()], null);
				dgvlistadopropuesta_CellClick(new object(), e2);
				continue;
			}
			string colselect = getColumnaSeleccionada(fila.Index);
			if (colselect != "")
			{
				fila.Cells[colselect].Value = true;
				GridViewCellEventArgs e3 = new GridViewCellEventArgs(fila, dgvlistadopropuesta.Columns[colselect], null);
				dgvlistadopropuesta_CellClick(new object(), e3);
			}
		}
	}

	private void txtTipoCambio_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosDecimales(e, sender as TextBox);
	}

	private void btnReportePDF_Click(object sender, EventArgs e)
	{
		try
		{
			if (prop_pedi != null)
			{
				string ruta = "C:\\tmp\\PropuestaDeCompra";
				string nombreArchivo = "SOLICITUD DE COTIZACIÓN";
				clsReportePropuestaDePedido dso = new clsReportePropuestaDePedido();
				CRPropuestaDePedidoDeOrdenDeCompra rpt = new CRPropuestaDePedidoDeOrdenDeCompra();
				frmRptPropuestaDePedido frm = new frmRptPropuestaDePedido();
				rpt.SetDataSource(dso.propuestadepedidodeordendecompra(prop_pedi.Codigo, 0, frmLogin.iCodSucursal).Tables[0]);
				frm.crvPropuestaDePedido.ReportSource = rpt;
				Directory.CreateDirectory(ruta);
				rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
				Process p = new Process();
				p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
				p.Start();
			}
			else
			{
				MessageBox.Show("Guarde la Propuesta Para Generar El Reporte", "Reporte Propuesta de Pedido de Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnExcel_Click(object sender, EventArgs e)
	{
		try
		{
			SLDocument sl = new SLDocument();
			int indFilaInicial = 1;
			sl.SetCellValue(indFilaInicial, 1, "Item");
			sl.SetCellValue(indFilaInicial, 2, "Referencia");
			sl.SetCellValue(indFilaInicial, 3, "Descripcion");
			sl.SetCellValue(indFilaInicial, 4, "Unidad");
			sl.SetCellValue(indFilaInicial, 5, "Cantidad");
			int indFilaContenido = indFilaInicial;
			foreach (GridViewRowInfo fila in dgvlistadopropuesta.Rows)
			{
				indFilaContenido++;
				sl.SetCellValue(indFilaContenido, 1, Convert.ToInt32(fila.Cells["colNroItem"].Value));
				sl.SetCellValue(indFilaContenido, 2, fila.Cells["colReferenciaProducto"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 3, fila.Cells["colDescripProducto"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 4, fila.Cells["colDescripUnidad"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 5, Convert.ToInt32((fila.Cells["colPedidoFinal"].Value == DBNull.Value) ? ((object)0) : fila.Cells["colPedidoFinal"].Value));
			}
			SLStyle estilo = sl.CreateStyle();
			estilo.SetFontColor(System.Drawing.Color.White);
			estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(10, 153, 255), System.Drawing.Color.Blue);
			sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 5, estilo);
			SLStyle aux_style = sl.CreateStyle();
			asignarBordes(aux_style);
			sl.SetCellStyle(indFilaInicial, 1, indFilaContenido, 5, aux_style);
			sl.SetColumnWidth(1, 5.0);
			sl.SetColumnWidth(2, 11.0);
			sl.SetColumnWidth(3, 60.0);
			sl.SetColumnWidth(4, 18.0);
			sl.SetColumnWidth(5, 12.0);
			SLStyle style = sl.CreateStyle();
			style.SetWrapText(IsWrapped: true);
			sl.SetColumnStyle(3, style);
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar();
				if (cadenaGuardado != null)
				{
					sl.SaveAs(cadenaGuardado);
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, base.Name + " - Line 177");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void asignarBordes(SLStyle style)
	{
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.Black;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.Black;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.Black;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.Black;
	}

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Propuesta_Orden_Compra")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo Excel de Exportacion";
			sfd.FileName = namefile;
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Propuestad de orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void dgvOrdenesGeneradas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			frmOrdenCompra form = buscarFrmOC("frmOrdenCompra", Convert.ToInt32(dgvOrdenesGeneradas.Rows[e.RowIndex].Cells[colcodOC.Name].Value), 3);
			if (form != null)
			{
				form.Activate();
				return;
			}
			form = new frmOrdenCompra();
			form.MdiParent = base.MdiParent;
			form.CodOrdenCompra = Convert.ToInt32(dgvOrdenesGeneradas.Rows[e.RowIndex].Cells[colcodOC.Name].Value);
			form.Proceso = 3;
			form.Show();
		}
	}

	private frmOrdenCompra buscarFrmOC(string tipoFormulario, int codOC, int proceso)
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

	public void rellenarDiccionarioDeValoresYPosicionesNroItem()
	{
		valores_posiciones_nroitems.Clear();
		foreach (DataRow filita in tableDGV.Rows)
		{
			if (int.TryParse(filita.Field<object>("nroItem").ToString(), out var aux))
			{
				int valor = aux;
				int posicion = tableDGV.Rows.IndexOf(filita);
				valores_posiciones_nroitems.Add(valor, posicion);
				continue;
			}
			throw new Exception("No se pudo obtener el listado total de la columna con respecto a orden de Item");
		}
		codprod_valores_nroitems.Clear();
		foreach (DataRow filita2 in tableDGV.Rows)
		{
			if (int.TryParse(filita2.Field<object>("nroItem").ToString(), out var aux2))
			{
				int valor2 = aux2;
				int cod_prod = Convert.ToInt32(filita2.Field<object>("codProd").ToString());
				codprod_valores_nroitems.Add(cod_prod, valor2);
				continue;
			}
			throw new Exception("No se pudo obtener el listado total de la columna con respecto a orden de Item y el codigo de producto");
		}
	}

	private void dgvlistadopropuesta_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
	{
		try
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
			{
				if (dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
				{
					dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
				}
				Console.WriteLine("CellBeginEdit " + dgvlistadopropuesta.Columns[e.ColumnIndex].Name + ": " + dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
			}
			else
			{
				Console.WriteLine("CellBeginEdit " + dgvlistadopropuesta.Columns[e.ColumnIndex].Name + ": ");
			}
			if (e.ColumnIndex == 0)
			{
				rellenarDiccionarioDeValoresYPosicionesNroItem();
			}
			valor_inicial_celda_editar = dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (isColumnaTotalCotizacion(e.ColumnIndex))
			{
				string nameColumna = getColumnaPrecioSegun(e.ColumnIndex);
				if (nameColumna != "")
				{
					valor_correspondiente_a_precio = dgvlistadopropuesta.Rows[e.RowIndex].Cells[nameColumna].Value;
					ind_col_correspondiente_a_precio = dgvlistadopropuesta.Columns[nameColumna].Index;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error:" + ex.Message);
		}
	}

	private void dgvlistadopropuesta_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0 || e.ColumnIndex <= 1 || !validaCheckBox(e))
			{
				return;
			}
			if (!Convert.ToBoolean(dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
			{
				string name_columna = getColumnaSeleccionada(e.RowIndex, e.ColumnIndex);
				if (name_columna != "")
				{
					dgvlistadopropuesta.Rows[e.RowIndex].Cells[name_columna].Value = false;
				}
			}
			dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !Convert.ToBoolean(dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			if (!modoEdicion)
			{
				return;
			}
			bool valor_check = Convert.ToBoolean(dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			double Valor_seleccionado = Convert.ToDouble(dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value);
			double tipocambio = Convert.ToDouble(txtTipoCambio.Text);
			string nombrecolumna = dgvlistadopropuesta.Columns[e.ColumnIndex].Tag.ToString();
			foreach (DataGridViewRow pro1 in (IEnumerable)dgvCotizacion.Rows)
			{
				proveedor = pro1.Cells[colDgvCotDesProveedor.Name].Value.ToString();
				if (proveedor == nombrecolumna)
				{
					afectacionseleccionada = pro1.Cells[colDgvCotTipoPrecio.Name].Value.ToString();
					Valor_seleccionado *= ((pro1.Cells[colDgvCotMoneda.Name].Value.ToString() == "SOLES") ? 1.0 : tipocambio);
				}
			}
			if (afectacionseleccionada.Contains("Sin Igv"))
			{
				Valor_seleccionado = Math.Round(Valor_seleccionado * 1.18, 4);
			}
			if (valor_check)
			{
				foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
				{
					double valor = Convert.ToDouble(dgvlistadopropuesta.Rows[e.RowIndex].Cells[item.DescripcionProveedor + " Totalizado"].Value) * ((item.CodigoMoneda == 1) ? 1.0 : tipocambio);
					foreach (DataGridViewRow pro2 in (IEnumerable)dgvCotizacion.Rows)
					{
						proveedor = pro2.Cells[colDgvCotDesProveedor.Name].Value.ToString();
						if (proveedor == item.DescripcionProveedor)
						{
							afectacion = pro2.Cells[colDgvCotTipoPrecio.Name].Value.ToString();
						}
					}
					if (afectacion.Contains("Sin Igv"))
					{
						valor = Math.Round(valor * 1.18, 4);
					}
					if (!(Valor_seleccionado > valor) || valor == 0.0)
					{
						continue;
					}
					MessageBox.Show("EXISTE UN MÉJOR PRECIO , PROVEEDOR : " + item.DescripcionProveedor + "  NOTA : TODOS LOS PRECIOS EN EL COMPARATIVO INCLUYEN IGV");
					if (MessageBox.Show("DESEA MANTENER EL PRECIO SELECCIONADO", "PROVEEDOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						frmAutorizacion frm = new frmAutorizacion();
						DialogResult dr = DialogResult.None;
						dr = frm.ShowDialog();
						if (dr == DialogResult.OK)
						{
							dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
						}
						else
						{
							dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
						}
					}
					else
					{
						dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
					}
				}
			}
			List<clsCotizacionPropuestaDePedido> item_cot = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == Convert.ToInt32(dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].ColumnInfo.Name))).ToList();
			if (item_cot.Count > 0)
			{
				List<DataRow> fila = (from x in tableDGV.AsEnumerable()
					where Convert.ToInt32(x.Field<object>("codigoDetallePropuesta")) == Convert.ToInt32(dgvlistadopropuesta.Rows[e.RowIndex].Cells["colCodigoDetalle"].Value)
					select x).ToList();
				if (fila.Count > 0)
				{
					clsDetallePropuestaDePedido item_det_prop = convertirFilaEnClase(fila[0]);
					if (valor_check)
					{
						item_det_prop.Cod_cotizacion_seleccionada = item_cot[0].Codigo;
						item_det_prop.Cod_proveedor_seleccionado = item_cot[0].CodigoProveedor;
					}
					else
					{
						item_det_prop.Cod_cotizacion_seleccionada = 0;
						item_det_prop.Cod_proveedor_seleccionado = 0;
					}
					item_det_prop.Cod_Propuesta = prop_pedi.Codigo;
					List<clsDetallePropuestaDePedido> item_cot_sel = Enumerable.Where<clsDetallePropuestaDePedido>(lista_detalle_prop_cot_selecc.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo == item_det_prop.Codigo)).ToList();
					if (item_cot_sel.Count > 0)
					{
						item_cot_sel[0].Cod_cotizacion_seleccionada = item_det_prop.Cod_cotizacion_seleccionada;
						item_cot_sel[0].Cod_proveedor_seleccionado = item_det_prop.Cod_proveedor_seleccionado;
					}
					else
					{
						lista_detalle_prop_cot_selecc.Add(item_det_prop);
					}
				}
				else
				{
					MessageBox.Show("Error Al Cambiar Dato Seleccionado", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("Error Al Cambiar Dato Seleccionado", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvlistadopropuesta_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex == -1)
			{
				return;
			}
			if (e.ColumnIndex == 0)
			{
				string valor = dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
				bool error = false;
				int ingresado = -2;
				if (int.TryParse(valor, out ingresado))
				{
					if (ingresado > 0 && ingresado < dgvlistadopropuesta.Rows.Count + 1)
					{
						tableDGV.Rows[valores_posiciones_nroitems[ingresado]].SetField("nroItem", valor_inicial_celda_editar);
					}
					else
					{
						error = true;
					}
				}
				else
				{
					error = true;
				}
				if (error)
				{
					dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor_inicial_celda_editar;
					return;
				}
				lista_detalle_prop_actualizar.Clear();
				{
					foreach (DataRow fila in tableDGV.Rows)
					{
						clsDetallePropuestaDePedido item_prop_ins = convertirFilaEnClase(fila);
						if (item_prop_ins.Codigo > 0)
						{
							lista_detalle_prop_actualizar.Add(item_prop_ins);
							continue;
						}
						List<clsDetallePropuestaDePedido> buscar = Enumerable.Where<clsDetallePropuestaDePedido>(lista_detalle_prop_insertar.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo_Producto == item_prop_ins.Codigo_Producto)).ToList();
						if (buscar.Count > 0)
						{
							lista_detalle_prop_insertar.Remove(buscar[0]);
						}
						lista_detalle_prop_insertar.Add(item_prop_ins);
					}
					return;
				}
			}
			GridViewCellInfo casilla = dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex];
			bool bandCheckBox = false;
			if (casilla.ColumnInfo.GetType() == typeof(GridViewCheckBoxColumn))
			{
				bandCheckBox = true;
			}
			if (bandCheckBox)
			{
				return;
			}
			if (e.ColumnIndex == dgvlistadopropuesta.Columns["colPedidoFinal"].Index)
			{
				object valor_de_celda = dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
				if (Convert.ToDecimal((valor_de_celda == DBNull.Value) ? ((object)1) : valor_de_celda) == 0m)
				{
					dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor_inicial_celda_editar;
					MessageBox.Show("La Columna Pedido Final no permite ingresar la cantidad cero. Se recomienda dejar vacio la celda", "Valor No Admitido", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			GridViewRowInfo filaActual = dgvlistadopropuesta.Rows[e.RowIndex];
			double valor2 = Convert.ToDouble((filaActual.Cells[e.ColumnIndex].Value == DBNull.Value || filaActual.Cells[e.ColumnIndex].Value == null || filaActual.Cells[e.ColumnIndex].Value == "") ? ((object)double.NaN) : filaActual.Cells[e.ColumnIndex].Value);
			if (!double.IsNaN(valor2))
			{
				filaActual.Cells[e.ColumnIndex].Value = valor2;
				object valor_aux = filaActual.Cells["colPedidoFinal"].Value;
				double pedido = Convert.ToDouble((valor_aux == DBNull.Value || valor_aux == null || valor_aux == "") ? ((object)double.NaN) : valor_aux);
				if (!double.IsNaN(pedido))
				{
					string nameColumnaSubTotalizado = getColumnaTotalizadoSegun(e.ColumnIndex);
					if (nameColumnaSubTotalizado != "")
					{
						double resultado = valor2 * Convert.ToDouble(filaActual.Cells["colPedidoFinal"].Value);
						filaActual.Cells[nameColumnaSubTotalizado].Value = $"{resultado:0.0000}";
					}
					else if (ind_col_correspondiente_a_precio != 0)
					{
						string nameColumnaPrecio = getColumnaPrecioSegun(e.ColumnIndex);
						if (nameColumnaPrecio != "")
						{
							double resultado2 = valor2 / Convert.ToDouble(filaActual.Cells["colPedidoFinal"].Value);
							filaActual.Cells[nameColumnaPrecio].Value = $"{resultado2:0.0000}";
							valor2 = Convert.ToDouble($"{resultado2:0.0000}");
						}
					}
				}
			}
			else
			{
				string nameColumnaSubTotalizado2 = getColumnaTotalizadoSegun(e.ColumnIndex);
				if (nameColumnaSubTotalizado2 != "")
				{
					filaActual.Cells[nameColumnaSubTotalizado2].Value = null;
				}
				else if (ind_col_correspondiente_a_precio != 0)
				{
					string nameColumnaPrecio2 = getColumnaPrecioSegun(e.ColumnIndex);
					if (nameColumnaPrecio2 != "")
					{
						filaActual.Cells[nameColumnaPrecio2].Value = null;
					}
				}
			}
			if (e.ColumnIndex == dgvlistadopropuesta.Columns["colPedidoFinal"].Index)
			{
				if (dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != DBNull.Value && dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != "")
				{
					double pedido2 = Convert.ToDouble(dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
					foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
					{
						double resultado3 = pedido2 * Convert.ToDouble(filaActual.Cells[item.DescripcionProveedor].Value);
						if (resultado3 > 0.0)
						{
							filaActual.Cells[item.DescripcionProveedor + " Totalizado"].Value = $"{resultado3:0.0000}";
						}
					}
				}
				else
				{
					limpiaCotizaciones(e.RowIndex);
				}
				recargaTablaCotizaciones();
			}
			Console.WriteLine("CellEndEdit " + dgvlistadopropuesta.Columns[e.ColumnIndex].Name + ": " + dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			if (proviene_shown || !modoEdicion)
			{
				return;
			}
			if (e.ColumnIndex == dgvlistadopropuesta.Columns["colPedidoFinal"].Index)
			{
				int a = 8;
				if (filaActual.Cells["colCodigoDetalle"].Value == DBNull.Value || filaActual.Cells["colCodigoDetalle"].Value == null)
				{
					a = 9;
					List<DataRow> fila2 = (from x in tableDGV.AsEnumerable()
						where Convert.ToInt32(x.Field<object>("codProd")) == Convert.ToInt32(filaActual.Cells["colCodigoProducto"].Value)
						select x).ToList();
					clsDetallePropuestaDePedido item_prop_ins2 = convertirFilaEnClase(fila2[0]);
					List<clsDetallePropuestaDePedido> buscar2 = Enumerable.Where<clsDetallePropuestaDePedido>(lista_detalle_prop_insertar.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo_Producto == item_prop_ins2.Codigo_Producto)).ToList();
					if (buscar2.Count > 0)
					{
						lista_detalle_prop_insertar.Remove(buscar2[0]);
					}
					lista_detalle_prop_insertar.Add(item_prop_ins2);
					return;
				}
				List<DataRow> fila3 = (from x in tableDGV.AsEnumerable()
					where Convert.ToInt32(x.Field<object>("codigoDetallePropuesta")) == Convert.ToInt32(filaActual.Cells["colCodigoDetalle"].Value)
					select x).ToList();
				if (fila3.Count > 0)
				{
					clsDetallePropuestaDePedido item_prop_ins3 = convertirFilaEnClase(fila3[0]);
					List<clsDetallePropuestaDePedido> item_prop_aux = Enumerable.Where<clsDetallePropuestaDePedido>(lista_detalle_prop_actualizar.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo == item_prop_ins3.Codigo)).ToList();
					if (item_prop_aux.Count > 0)
					{
						if (!double.IsNaN(valor2))
						{
							item_prop_aux[0].Pedido_final = Convert.ToDouble($"{valor2:0.0000}");
						}
						else
						{
							item_prop_aux[0].Pedido_final = double.NaN;
						}
					}
					else
					{
						lista_detalle_prop_actualizar.Add(item_prop_ins3);
					}
				}
				else
				{
					MessageBox.Show("Error Al Cambiar  Dato de Objeto", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else if (isColumnaPrecioCotizacion(e.ColumnIndex) || (ind_col_correspondiente_a_precio != 0 && isColumnaPrecioCotizacion(ind_col_correspondiente_a_precio)))
			{
				List<clsCotizacionPropuestaDePedido> elemento_cot = Enumerable.Select<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>(Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.DescripcionProveedor == filaActual.Cells[(ind_col_correspondiente_a_precio != 0) ? ind_col_correspondiente_a_precio : e.ColumnIndex].ColumnInfo.Name)), (Func<clsCotizacionPropuestaDePedido, clsCotizacionPropuestaDePedido>)((clsCotizacionPropuestaDePedido x) => x)).ToList();
				if (elemento_cot.Count > 0)
				{
					clsDetalleCotizacionPropuestaDePedido obj_det_cot = new clsDetalleCotizacionPropuestaDePedido();
					obj_det_cot.CodigoCotizacion = elemento_cot[0].Codigo;
					obj_det_cot.CodigoDetallePropuesta = Convert.ToInt32(filaActual.Cells["colCodigoDetalle"].Value);
					obj_det_cot.CodigoProducto = Convert.ToInt32(filaActual.Cells["colCodigoProducto"].Value);
					obj_det_cot.PrecioCompra = Convert.ToDouble($"{valor2:0.0000}");
					if (valor_inicial_celda_editar == DBNull.Value || valor_inicial_celda_editar == null || valor_inicial_celda_editar == "")
					{
						if (!double.IsNaN(valor2))
						{
							if (elemento_cot[0].Codigo == 0)
							{
								List<clsCotizacionPropuestaDePedido> item_cot_ins = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == elemento_cot[0].CodigoProveedor)).ToList();
								if (item_cot_ins.Count > 0)
								{
									List<clsDetalleCotizacionPropuestaDePedido> item_cot_det = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_ins[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
									if (item_cot_det.Count > 0)
									{
										item_cot_det[0].PrecioCompra = obj_det_cot.PrecioCompra;
									}
									else
									{
										item_cot_ins[0].ListadoPrecios.Add(obj_det_cot);
									}
								}
								else
								{
									MessageBox.Show("Error Al Cambiar  Dato de Objeto", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
							else
							{
								List<clsCotizacionPropuestaDePedido> item_cot_det_del = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_eliminar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
								if (item_cot_det_del.Count > 0)
								{
									List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_del = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_del[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
									if (item_det_cot_det_del.Count > 0)
									{
										item_det_cot_det_del[0].PrecioCompra = obj_det_cot.PrecioCompra;
										clsCotizacionPropuestaDePedido nuevo = obtenerNuevaCotizacion(item_cot_det_del[0]);
										insertaOActualizaListadoDetalleCotizacion(listado_cotizacion_detalle_actualizar, nuevo, obj_det_cot);
										item_cot_det_del[0].ListadoPrecios.Remove(item_det_cot_det_del[0]);
										if (item_cot_det_del[0].ListadoPrecios.Count == 0)
										{
											listado_cotizacion_detalle_eliminar.Remove(item_cot_det_del[0]);
										}
									}
									else
									{
										List<clsCotizacionPropuestaDePedido> item_cot_det_ins = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
										if (item_cot_det_ins.Count > 0)
										{
											List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_ins = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_ins[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
											if (item_det_cot_det_ins.Count > 0)
											{
												item_det_cot_det_ins[0].PrecioCompra = obj_det_cot.PrecioCompra;
											}
											else
											{
												item_cot_det_ins[0].ListadoPrecios.Add(obj_det_cot);
											}
										}
										else
										{
											clsCotizacionPropuestaDePedido aux = obtenerNuevaCotizacion(elemento_cot[0]);
											aux.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
											aux.ListadoPrecios.Add(obj_det_cot);
											listado_cotizacion_detalle_insertar.Add(aux);
										}
									}
								}
								else
								{
									List<clsCotizacionPropuestaDePedido> item_cot_det_ins2 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
									if (item_cot_det_ins2.Count > 0)
									{
										List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_ins2 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_ins2[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
										if (item_det_cot_det_ins2.Count > 0)
										{
											item_det_cot_det_ins2[0].PrecioCompra = obj_det_cot.PrecioCompra;
										}
										else
										{
											item_cot_det_ins2[0].ListadoPrecios.Add(obj_det_cot);
										}
									}
									else
									{
										clsCotizacionPropuestaDePedido aux2 = obtenerNuevaCotizacion(elemento_cot[0]);
										aux2.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
										aux2.ListadoPrecios.Add(obj_det_cot);
										listado_cotizacion_detalle_insertar.Add(aux2);
									}
								}
							}
						}
					}
					else if (!double.IsNaN(valor2))
					{
						if (elemento_cot[0].Codigo == 0)
						{
							List<clsCotizacionPropuestaDePedido> item_cot_ins2 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == elemento_cot[0].CodigoProveedor)).ToList();
							if (item_cot_ins2.Count > 0)
							{
								List<clsDetalleCotizacionPropuestaDePedido> item_cot_det2 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_ins2[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
								if (item_cot_det2.Count > 0)
								{
									item_cot_det2[0].PrecioCompra = obj_det_cot.PrecioCompra;
								}
								else
								{
									item_cot_ins2[0].ListadoPrecios.Add(obj_det_cot);
								}
							}
							else
							{
								MessageBox.Show("Error Al Cambiar  Dato de Objeto", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						else
						{
							bool band_act = false;
							List<clsCotizacionPropuestaDePedido> item_cot_det_ins3 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
							if (item_cot_det_ins3.Count > 0)
							{
								List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_ins3 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_ins3[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
								if (item_det_cot_det_ins3.Count > 0)
								{
									item_det_cot_det_ins3[0].PrecioCompra = obj_det_cot.PrecioCompra;
								}
								else
								{
									band_act = true;
								}
							}
							else
							{
								band_act = true;
							}
							if (band_act)
							{
								bool band_ins_act = false;
								List<clsCotizacionPropuestaDePedido> item_cot_det_upd = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_actualizar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
								if (item_cot_det_upd.Count > 0)
								{
									List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_upd = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_upd[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
									if (item_det_cot_det_upd.Count > 0)
									{
										item_det_cot_det_upd[0].PrecioCompra = obj_det_cot.PrecioCompra;
									}
									else
									{
										band_ins_act = true;
									}
								}
								else
								{
									band_ins_act = true;
								}
								if (band_ins_act)
								{
									clsCotizacionPropuestaDePedido aux3 = obtenerNuevaCotizacion(elemento_cot[0]);
									aux3.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
									aux3.ListadoPrecios.Add(obj_det_cot);
									listado_cotizacion_detalle_actualizar.Add(aux3);
								}
							}
						}
					}
					else if (elemento_cot[0].Codigo == 0)
					{
						bool band = false;
						List<clsCotizacionPropuestaDePedido> item_cot_ins3 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.CodigoProveedor == elemento_cot[0].CodigoProveedor)).ToList();
						if (item_cot_ins3.Count > 0)
						{
							List<clsDetalleCotizacionPropuestaDePedido> item_cot_det3 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_ins3[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
							if (item_cot_det3.Count > 0)
							{
								item_cot_ins3[0].ListadoPrecios.Remove(item_cot_det3[0]);
							}
							else
							{
								band = true;
							}
						}
						else
						{
							band = true;
						}
						if (band)
						{
							MessageBox.Show("Error Al Cambiar  Dato de Objeto", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else
					{
						bool band_act2 = false;
						List<clsCotizacionPropuestaDePedido> item_cot_det_ins4 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_insertar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
						if (item_cot_det_ins4.Count > 0)
						{
							List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_ins4 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_ins4[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
							if (item_det_cot_det_ins4.Count > 0)
							{
								item_cot_det_ins4[0].ListadoPrecios.Remove(obj_det_cot);
								if (item_cot_det_ins4[0].ListadoPrecios.Count == 0)
								{
									listado_cotizacion_detalle_insertar.Remove(item_cot_det_ins4[0]);
								}
							}
							else
							{
								band_act2 = true;
							}
						}
						else
						{
							band_act2 = true;
						}
						if (band_act2)
						{
							bool band_eli = false;
							List<clsCotizacionPropuestaDePedido> item_cot_det_upd2 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_actualizar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
							if (item_cot_det_upd2.Count > 0)
							{
								List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_upd2 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_upd2[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
								if (item_det_cot_det_upd2.Count > 0)
								{
									item_cot_det_upd2[0].ListadoPrecios.Remove(obj_det_cot);
									if (item_cot_det_upd2[0].ListadoPrecios.Count == 0)
									{
										listado_cotizacion_detalle_actualizar.Remove(item_cot_det_upd2[0]);
									}
								}
								else
								{
									band_eli = true;
								}
							}
							else
							{
								band_eli = true;
							}
							if (band_eli)
							{
								List<clsCotizacionPropuestaDePedido> item_cot_det_del2 = Enumerable.Where<clsCotizacionPropuestaDePedido>(listado_cotizacion_detalle_eliminar.AsEnumerable(), (Func<clsCotizacionPropuestaDePedido, bool>)((clsCotizacionPropuestaDePedido x) => x.Codigo == elemento_cot[0].Codigo)).ToList();
								if (item_cot_det_del2.Count > 0)
								{
									List<clsDetalleCotizacionPropuestaDePedido> item_det_cot_det_del2 = Enumerable.Where<clsDetalleCotizacionPropuestaDePedido>(item_cot_det_del2[0].ListadoPrecios.AsEnumerable(), (Func<clsDetalleCotizacionPropuestaDePedido, bool>)((clsDetalleCotizacionPropuestaDePedido x) => x.CodigoDetallePropuesta == obj_det_cot.CodigoDetallePropuesta)).ToList();
									if (item_det_cot_det_del2.Count > 0)
									{
										MessageBox.Show("Error Al Quitar Dato de Objeto", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									}
									else
									{
										item_cot_det_del2[0].ListadoPrecios.Add(obj_det_cot);
									}
								}
								else
								{
									clsCotizacionPropuestaDePedido aux4 = obtenerNuevaCotizacion(elemento_cot[0]);
									aux4.ListadoPrecios = new List<clsDetalleCotizacionPropuestaDePedido>();
									aux4.ListadoPrecios.Add(obj_det_cot);
									listado_cotizacion_detalle_eliminar.Add(aux4);
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show("Ocurrio Un Error Al Cambiar el Dato Asignado\nSi este error persiste, cierre la ventana y vuelva abrir.");
				}
				recargaTablaCotizaciones();
			}
			else
			{
				MessageBox.Show("Ocurrio un error al tratar de guardar el dato ingresado.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			ind_col_correspondiente_a_precio = 0;
			valor_correspondiente_a_precio = null;
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ocurrio un error al tratar de guardar el dato ingresado.\n" + ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvlistadopropuesta_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex == -1)
		{
			return;
		}
		try
		{
			if (e.ColumnIndex != dgvlistadopropuesta.Columns["colReferenciaProducto"].Index)
			{
				return;
			}
			if (dgvlistadopropuesta.Rows.Count == 1)
			{
				MessageBox.Show("IMPOSIBLE ELIMINAR TODOS LOS ITEMS DE UNA PROPUESTA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			GridViewRowInfo fila = dgvlistadopropuesta.Rows[e.RowIndex];
			DialogResult rpta = MessageBox.Show("Esta seguro de eliminar el siguiente item: \n> " + fila.Cells["colReferenciaProducto"].Value.ToString(), "Eliminando item", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta != DialogResult.Yes)
			{
				return;
			}
			List<DataRow> det = (from x in tableDGV.AsEnumerable()
				where Convert.ToInt32(x.Field<object>("codProd")) == Convert.ToInt32(fila.Cells["colCodigoProducto"].Value)
				select x).ToList();
			if (det.Count > 0)
			{
				clsDetallePropuestaDePedido aux = convertirFilaEnClase(det[0]);
				rellenarDiccionarioDeValoresYPosicionesNroItem();
				if (int.TryParse(det[0].Field<object>("nroItem").ToString(), out var valor))
				{
					if (valor > 0 && valor < dgvlistadopropuesta.Rows.Count)
					{
						int ultimo_pos = valores_posiciones_nroitems[dgvlistadopropuesta.Rows.Count];
						tableDGV.Rows[ultimo_pos].SetField("nroItem", valor);
					}
					lista_detalle_prop_eliminar.Add(aux);
					tableDGV.Rows.Remove(det[0]);
					lista_detalle_prop_actualizar.Clear();
					{
						foreach (DataRow fila2 in tableDGV.Rows)
						{
							clsDetallePropuestaDePedido item_prop_ins = convertirFilaEnClase(fila2);
							lista_detalle_prop_actualizar.Add(item_prop_ins);
						}
						return;
					}
				}
				throw new Exception("Error al eliminar fila celda Item no contiene valor");
			}
			MessageBox.Show("Error Inesperado Al Eliminar el item", "Eliminando item", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "ERROR DE ELIMINACION", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvlistadopropuesta_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.Delete)
			{
				return;
			}
			if (dgvlistadopropuesta.Rows.Count == dgvlistadopropuesta.SelectedRows.Count)
			{
				MessageBox.Show("IMPOSIBLE ELIMINAR TODOS LOS ITEMS DE UNA PROPUESTA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				if (dgvlistadopropuesta.SelectedRows.Count > 0)
				{
					string items_seleccionados = "";
					foreach (GridViewRowInfo fila in dgvlistadopropuesta.SelectedRows)
					{
						ctdad_a_mostrar--;
						items_seleccionados = items_seleccionados + "\n>" + fila.Cells["colReferenciaProducto"].Value.ToString() + " -- " + fila.Cells["colDescripProducto"].Value.ToString();
						if (ctdad_a_mostrar == 0)
						{
							break;
						}
					}
					if (dgvlistadopropuesta.SelectedRows.Count > ctdad_a_mostrar)
					{
						items_seleccionados = items_seleccionados + "\n y " + (dgvlistadopropuesta.SelectedRows.Count - ctdad_a_mostrar) + " items más.";
					}
					DialogResult rpta = MessageBox.Show("Esta seguro de eliminar los siguientes items:" + items_seleccionados, "Eliminando item", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (rpta != DialogResult.Yes)
					{
						return;
					}
					List<int> eliminar = new List<int>();
					foreach (GridViewRowInfo fila2 in dgvlistadopropuesta.SelectedRows)
					{
						List<DataRow> det = (from x in tableDGV.AsEnumerable()
							where Convert.ToInt32(x.Field<object>("codProd")) == Convert.ToInt32(fila2.Cells["colCodigoProducto"].Value)
							select x).ToList();
						if (det.Count > 0)
						{
							eliminar.Add(Convert.ToInt32(det[0].Field<object>("codProd").ToString()));
							continue;
						}
						throw new Exception("Ocurrio un error  al intentar eliminar el item: " + fila2.Cells["colCodigoProducto"].Value.ToString() + " -- Referencia: " + fila2.Cells["colReferenciaProducto"].Value.ToString());
					}
					foreach (int codProd in eliminar)
					{
						rellenarDiccionarioDeValoresYPosicionesNroItem();
						int pos = valores_posiciones_nroitems[codprod_valores_nroitems[codProd]];
						clsDetallePropuestaDePedido aux = convertirFilaEnClase(tableDGV.Rows[pos]);
						if (int.TryParse(tableDGV.Rows[pos].Field<object>("nroItem").ToString(), out var valor))
						{
							if (valor > 0 && valor < dgvlistadopropuesta.Rows.Count)
							{
								int ultimo_pos = valores_posiciones_nroitems[dgvlistadopropuesta.Rows.Count];
								tableDGV.Rows[ultimo_pos].SetField("nroItem", valor);
							}
							lista_detalle_prop_eliminar.Add(aux);
							tableDGV.Rows.RemoveAt(pos);
							continue;
						}
						throw new Exception("Error al eliminar fila celda Item no contiene valor");
					}
					lista_detalle_prop_actualizar.Clear();
					{
						foreach (DataRow fila3 in tableDGV.Rows)
						{
							clsDetallePropuestaDePedido item_prop_ins = convertirFilaEnClase(fila3);
							lista_detalle_prop_actualizar.Add(item_prop_ins);
						}
						return;
					}
				}
				MessageBox.Show("Seleccione items para eliminar con la tecla DEL.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Eliminar Items", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnAgregarProducto_Click(object sender, EventArgs e)
	{
		frmDetalleGuia frm = new frmDetalleGuia();
		frm.Proceso = 6;
		frm.Procede = 1;
		frm.Codlista = 1;
		frm.ventana = this;
		frm.vieneDePropuesta = true;
		frm.codalmacen = ((prop_pedi.Cod_almacen == 0) ? frmLogin.iCodAlmacen : prop_pedi.Cod_almacen);
		frm.Text = "Insertar Producto y cantidad";
		if (frm.ShowDialog() == DialogResult.Yes)
		{
			int a = 1;
			clsDetallePropuestaDePedido detalle = convertirFilaEnClase(tableDGV.Rows[tableDGV.Rows.Count - 1]);
			lista_detalle_prop_insertar.Add(detalle);
			btnAgregarProducto.PerformClick();
		}
	}

	private void txtCodigoProp_TextChanged(object sender, EventArgs e)
	{
		txtCodigoProp.Text = txtCodigoProp.Text.PadLeft(7, '0');
	}

	private void txtCodigoProp_MouseDoubleClick(object sender, MouseEventArgs e)
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
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn3 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn4 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.Data.SortDescriptor sortDescriptor1 = new Telerik.WinControls.Data.SortDescriptor();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPropuestaDeOrdenCompra));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvlistadopropuesta = new Telerik.WinControls.UI.RadGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtCodigoProp = new System.Windows.Forms.TextBox();
		this.btnAgregarProducto = new System.Windows.Forms.Button();
		this.label9 = new System.Windows.Forms.Label();
		this.cmbcambiarmonedapuc = new System.Windows.Forms.ComboBox();
		this.gbcontienedgvordenesgeneradas = new System.Windows.Forms.GroupBox();
		this.dgvOrdenesGeneradas = new System.Windows.Forms.DataGridView();
		this.colNumOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colNombreProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnExcel = new System.Windows.Forms.Button();
		this.btnReportePDF = new System.Windows.Forms.Button();
		this.btnSeleccionarMontosMenores = new System.Windows.Forms.Button();
		this.btnLimpiarPedidoFinal = new System.Windows.Forms.Button();
		this.btnRellenarConCantidadSugerida = new System.Windows.Forms.Button();
		this.btnRecalcularPropuesta = new System.Windows.Forms.Button();
		this.btnVerListadoTotalPropuesta = new System.Windows.Forms.Button();
		this.btnVerificadorParaCotizar = new System.Windows.Forms.Button();
		this.gbCotizacion = new System.Windows.Forms.GroupBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.cmbTipoValorCompra = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtProveedor = new System.Windows.Forms.TextBox();
		this.btnAnadirCotizacion = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtDocCotizacion = new System.Windows.Forms.TextBox();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnQuitar = new System.Windows.Forms.Button();
		this.lblModoEdicion = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGenerar = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.txtEstadoPropuesta = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtDescripPropuesta = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtTituloPropuesta = new System.Windows.Forms.TextBox();
		this.btnTotal = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.dgvCotizacion = new System.Windows.Forms.DataGridView();
		this.colDgvCotItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotDocCot = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotDesProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotTipoPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotOp_Grav = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotIgv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDgvCotTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tTMensajes = new System.Windows.Forms.ToolTip(this.components);
		this.panelContenedorFormulario = new System.Windows.Forms.Panel();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		this.gbcontienedgvordenesgeneradas.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenesGeneradas).BeginInit();
		this.gbCotizacion.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCotizacion).BeginInit();
		this.panelContenedorFormulario.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.BackColor = System.Drawing.Color.Transparent;
		this.groupBox1.Controls.Add(this.dgvlistadopropuesta);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(6, 271);
		this.groupBox1.MaximumSize = new System.Drawing.Size(2000, 400);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1339, 234);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Propuesta De Orden De Compra";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.dgvlistadopropuesta.AutoScroll = true;
		this.dgvlistadopropuesta.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter;
		this.dgvlistadopropuesta.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvlistadopropuesta.EnableGestures = false;
		this.dgvlistadopropuesta.Location = new System.Drawing.Point(3, 16);
		this.dgvlistadopropuesta.MasterTemplate.AllowAddNewRow = false;
		this.dgvlistadopropuesta.MasterTemplate.AllowColumnChooser = false;
		this.dgvlistadopropuesta.MasterTemplate.AllowColumnReorder = false;
		this.dgvlistadopropuesta.MasterTemplate.AllowDeleteRow = false;
		this.dgvlistadopropuesta.MasterTemplate.AllowDragToGroup = false;
		this.dgvlistadopropuesta.MasterTemplate.AllowRowResize = false;
		gridViewDecimalColumn1.AllowFiltering = false;
		gridViewDecimalColumn1.AllowReorder = false;
		gridViewDecimalColumn1.AllowResize = false;
		gridViewDecimalColumn1.DataType = typeof(int);
		gridViewDecimalColumn1.DecimalPlaces = 0;
		gridViewDecimalColumn1.FieldName = "nroItem";
		gridViewDecimalColumn1.HeaderText = "Item";
		gridViewDecimalColumn1.Minimum = new decimal(new int[4]);
		gridViewDecimalColumn1.Name = "colNroItem";
		gridViewDecimalColumn1.SortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
		gridViewDecimalColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewDecimalColumn1.VisibleInColumnChooser = false;
		gridViewTextBoxColumn1.FieldName = "codigoDetallePropuesta";
		gridViewTextBoxColumn1.HeaderText = "CodigoDetallePropuesta";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodigoDetalle";
		gridViewTextBoxColumn2.FieldName = "codProd";
		gridViewTextBoxColumn2.HeaderText = "CodigoProducto";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodigoProducto";
		gridViewTextBoxColumn3.AllowReorder = false;
		gridViewTextBoxColumn3.AllowSort = false;
		gridViewTextBoxColumn3.FieldName = "refProd";
		gridViewTextBoxColumn3.HeaderText = "Referencia";
		gridViewTextBoxColumn3.Name = "colReferenciaProducto";
		gridViewTextBoxColumn3.ReadOnly = true;
		gridViewTextBoxColumn3.Width = 103;
		gridViewTextBoxColumn4.AllowReorder = false;
		gridViewTextBoxColumn4.AllowSort = false;
		gridViewTextBoxColumn4.FieldName = "descripProd";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "colDescripProducto";
		gridViewTextBoxColumn4.ReadOnly = true;
		gridViewTextBoxColumn4.Width = 259;
		gridViewTextBoxColumn5.FieldName = "codUnidad";
		gridViewTextBoxColumn5.HeaderText = "CodigoUnidad";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodUnidad";
		gridViewTextBoxColumn6.AllowReorder = false;
		gridViewTextBoxColumn6.AllowSort = false;
		gridViewTextBoxColumn6.FieldName = "descripUnidad";
		gridViewTextBoxColumn6.HeaderText = "Unidad";
		gridViewTextBoxColumn6.Name = "colDescripUnidad";
		gridViewTextBoxColumn6.ReadOnly = true;
		gridViewTextBoxColumn6.Width = 103;
		gridViewDecimalColumn2.AllowReorder = false;
		gridViewDecimalColumn2.AllowSort = false;
		gridViewDecimalColumn2.FieldName = "stockDisponible";
		gridViewDecimalColumn2.HeaderText = "Stock Disponible";
		gridViewDecimalColumn2.Name = "colStockDisponible";
		gridViewDecimalColumn2.ReadOnly = true;
		gridViewDecimalColumn2.Width = 100;
		gridViewDecimalColumn3.AllowReorder = false;
		gridViewDecimalColumn3.AllowSort = false;
		gridViewDecimalColumn3.FieldName = "ctdadReponer";
		gridViewDecimalColumn3.HeaderText = "Ctdad a Reponer";
		gridViewDecimalColumn3.Name = "colCtdadReponer";
		gridViewDecimalColumn3.ReadOnly = true;
		gridViewDecimalColumn3.Width = 100;
		gridViewTextBoxColumn7.AllowReorder = false;
		gridViewTextBoxColumn7.AllowSort = false;
		gridViewTextBoxColumn7.FieldName = "ctdadSugerida";
		gridViewTextBoxColumn7.HeaderText = "Ctdad Sugerida";
		gridViewTextBoxColumn7.Name = "colCtdadSugerida";
		gridViewTextBoxColumn7.ReadOnly = true;
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn7.Width = 103;
		gridViewDecimalColumn4.AllowReorder = false;
		gridViewDecimalColumn4.AllowResize = false;
		gridViewDecimalColumn4.AllowSort = false;
		gridViewDecimalColumn4.FieldName = "pedidoFinal";
		gridViewDecimalColumn4.HeaderText = "Pedido Final";
		gridViewDecimalColumn4.Name = "colPedidoFinal";
		gridViewDecimalColumn4.Width = 100;
		gridViewTextBoxColumn8.AllowReorder = false;
		gridViewTextBoxColumn8.AllowSort = false;
		gridViewTextBoxColumn8.FieldName = "precioUnitarioCompra";
		gridViewTextBoxColumn8.HeaderText = "Precio Unit. Compra";
		gridViewTextBoxColumn8.Name = "colPrecioUnit";
		gridViewTextBoxColumn8.ReadOnly = true;
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn8.Width = 104;
		gridViewTextBoxColumn8.WrapText = true;
		gridViewTextBoxColumn9.AllowReorder = false;
		gridViewTextBoxColumn9.AllowSort = false;
		gridViewTextBoxColumn9.FieldName = "opcionRecuento";
		gridViewTextBoxColumn9.HeaderText = "OpcionRecuento";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "colOpcionRecuento";
		gridViewTextBoxColumn10.FieldName = "stockMinimo";
		gridViewTextBoxColumn10.HeaderText = "StockMinimo";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "colStockMinimo";
		gridViewTextBoxColumn11.FieldName = "stockMaximo";
		gridViewTextBoxColumn11.HeaderText = "StockMaximo";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "colStockMaximo";
		gridViewTextBoxColumn12.FieldName = "undxPaquete";
		gridViewTextBoxColumn12.HeaderText = "undXPaquete";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "colUndXPaquete";
		this.dgvlistadopropuesta.MasterTemplate.Columns.AddRange(gridViewDecimalColumn1, gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewDecimalColumn2, gridViewDecimalColumn3, gridViewTextBoxColumn7, gridViewDecimalColumn4, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12);
		this.dgvlistadopropuesta.MasterTemplate.EnableFiltering = true;
		this.dgvlistadopropuesta.MasterTemplate.EnableGrouping = false;
		this.dgvlistadopropuesta.MasterTemplate.MultiSelect = true;
		this.dgvlistadopropuesta.MasterTemplate.ShowRowHeaderColumn = false;
		sortDescriptor1.PropertyName = "colNroItem";
		this.dgvlistadopropuesta.MasterTemplate.SortDescriptors.AddRange(sortDescriptor1);
		this.dgvlistadopropuesta.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvlistadopropuesta.Name = "dgvlistadopropuesta";
		this.dgvlistadopropuesta.Size = new System.Drawing.Size(1333, 215);
		this.dgvlistadopropuesta.TabIndex = 87;
		this.dgvlistadopropuesta.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(dgvlistadopropuesta_CellBeginEdit);
		this.dgvlistadopropuesta.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvlistadopropuesta_CellEndEdit);
		this.dgvlistadopropuesta.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvlistadopropuesta_CellClick);
		this.dgvlistadopropuesta.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvlistadopropuesta_CellDoubleClick);
		this.dgvlistadopropuesta.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvlistadopropuesta_KeyUp);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.txtCodigoProp);
		this.groupBox2.Controls.Add(this.btnAgregarProducto);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.cmbcambiarmonedapuc);
		this.groupBox2.Controls.Add(this.gbcontienedgvordenesgeneradas);
		this.groupBox2.Controls.Add(this.btnExcel);
		this.groupBox2.Controls.Add(this.btnReportePDF);
		this.groupBox2.Controls.Add(this.btnSeleccionarMontosMenores);
		this.groupBox2.Controls.Add(this.btnLimpiarPedidoFinal);
		this.groupBox2.Controls.Add(this.btnRellenarConCantidadSugerida);
		this.groupBox2.Controls.Add(this.btnRecalcularPropuesta);
		this.groupBox2.Controls.Add(this.btnVerListadoTotalPropuesta);
		this.groupBox2.Controls.Add(this.btnVerificadorParaCotizar);
		this.groupBox2.Controls.Add(this.gbCotizacion);
		this.groupBox2.Controls.Add(this.lblModoEdicion);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btnGenerar);
		this.groupBox2.Controls.Add(this.btnGuardar);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.txtEstadoPropuesta);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.txtDescripPropuesta);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtTituloPropuesta);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.MaximumSize = new System.Drawing.Size(2000, 265);
		this.groupBox2.MinimumSize = new System.Drawing.Size(0, 265);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1348, 265);
		this.groupBox2.TabIndex = 3;
		this.groupBox2.TabStop = false;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(12, 16);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(43, 13);
		this.label10.TabIndex = 69;
		this.label10.Text = "Codigo:";
		this.txtCodigoProp.Enabled = false;
		this.txtCodigoProp.Location = new System.Drawing.Point(15, 32);
		this.txtCodigoProp.Name = "txtCodigoProp";
		this.txtCodigoProp.Size = new System.Drawing.Size(271, 20);
		this.txtCodigoProp.TabIndex = 68;
		this.txtCodigoProp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodigoProp.TextChanged += new System.EventHandler(txtCodigoProp_TextChanged);
		this.txtCodigoProp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtCodigoProp_MouseDoubleClick);
		this.btnAgregarProducto.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAgregarProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnAgregarProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAgregarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAgregarProducto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAgregarProducto.Location = new System.Drawing.Point(305, 117);
		this.btnAgregarProducto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnAgregarProducto.Name = "btnAgregarProducto";
		this.btnAgregarProducto.Size = new System.Drawing.Size(178, 39);
		this.btnAgregarProducto.TabIndex = 67;
		this.btnAgregarProducto.Text = "Agregar Producto";
		this.btnAgregarProducto.UseVisualStyleBackColor = true;
		this.btnAgregarProducto.Click += new System.EventHandler(btnAgregarProducto_Click);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.2f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(501, 222);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(198, 12);
		this.label9.TabIndex = 66;
		this.label9.Text = "Cambiar moneda Precio Unit. Compra:";
		this.label9.Visible = false;
		this.cmbcambiarmonedapuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbcambiarmonedapuc.FormattingEnabled = true;
		this.cmbcambiarmonedapuc.Location = new System.Drawing.Point(502, 238);
		this.cmbcambiarmonedapuc.Name = "cmbcambiarmonedapuc";
		this.cmbcambiarmonedapuc.Size = new System.Drawing.Size(197, 21);
		this.cmbcambiarmonedapuc.TabIndex = 65;
		this.cmbcambiarmonedapuc.Visible = false;
		this.gbcontienedgvordenesgeneradas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gbcontienedgvordenesgeneradas.Controls.Add(this.dgvOrdenesGeneradas);
		this.gbcontienedgvordenesgeneradas.Location = new System.Drawing.Point(651, 64);
		this.gbcontienedgvordenesgeneradas.Name = "gbcontienedgvordenesgeneradas";
		this.gbcontienedgvordenesgeneradas.Size = new System.Drawing.Size(303, 117);
		this.gbcontienedgvordenesgeneradas.TabIndex = 64;
		this.gbcontienedgvordenesgeneradas.TabStop = false;
		this.gbcontienedgvordenesgeneradas.Text = "Ordenes de Compra Generadas";
		this.gbcontienedgvordenesgeneradas.Visible = false;
		this.dgvOrdenesGeneradas.AllowUserToAddRows = false;
		this.dgvOrdenesGeneradas.AllowUserToDeleteRows = false;
		this.dgvOrdenesGeneradas.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
		this.dgvOrdenesGeneradas.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dgvOrdenesGeneradas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvOrdenesGeneradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvOrdenesGeneradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvOrdenesGeneradas.Columns.AddRange(this.colNumOC, this.colcodOC, this.colNombreProveedor, this.colEstado);
		this.dgvOrdenesGeneradas.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.dgvOrdenesGeneradas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOrdenesGeneradas.EnableHeadersVisualStyles = false;
		this.dgvOrdenesGeneradas.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.dgvOrdenesGeneradas.Location = new System.Drawing.Point(3, 16);
		this.dgvOrdenesGeneradas.Name = "dgvOrdenesGeneradas";
		this.dgvOrdenesGeneradas.ReadOnly = true;
		this.dgvOrdenesGeneradas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvOrdenesGeneradas.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgvOrdenesGeneradas.RowHeadersVisible = false;
		this.dgvOrdenesGeneradas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dgvOrdenesGeneradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvOrdenesGeneradas.Size = new System.Drawing.Size(297, 98);
		this.dgvOrdenesGeneradas.TabIndex = 63;
		this.dgvOrdenesGeneradas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvOrdenesGeneradas_CellMouseDoubleClick);
		this.colNumOC.DataPropertyName = "numoc";
		this.colNumOC.FillWeight = 69.92731f;
		this.colNumOC.HeaderText = "Nro. OC";
		this.colNumOC.Name = "colNumOC";
		this.colNumOC.ReadOnly = true;
		this.colNumOC.Width = 115;
		this.colcodOC.DataPropertyName = "codOC";
		this.colcodOC.HeaderText = "colCodOC";
		this.colcodOC.Name = "colcodOC";
		this.colcodOC.ReadOnly = true;
		this.colcodOC.Visible = false;
		this.colcodOC.Width = 5;
		this.colNombreProveedor.DataPropertyName = "nombreProveedor";
		this.colNombreProveedor.FillWeight = 102.1539f;
		this.colNombreProveedor.HeaderText = "Proveedor";
		this.colNombreProveedor.Name = "colNombreProveedor";
		this.colNombreProveedor.ReadOnly = true;
		this.colNombreProveedor.Width = 94;
		this.colEstado.DataPropertyName = "estado";
		this.colEstado.FillWeight = 127.9188f;
		this.colEstado.HeaderText = "Estado";
		this.colEstado.Name = "colEstado";
		this.colEstado.ReadOnly = true;
		this.colEstado.Width = 87;
		this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExcel.Location = new System.Drawing.Point(652, 12);
		this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnExcel.Name = "btnExcel";
		this.btnExcel.Size = new System.Drawing.Size(82, 46);
		this.btnExcel.TabIndex = 62;
		this.btnExcel.Text = "Excel";
		this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExcel.UseVisualStyleBackColor = true;
		this.btnExcel.Click += new System.EventHandler(btnExcel_Click);
		this.btnReportePDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReportePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReportePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReportePDF.Image = SIGEFA.Properties.Resources.printer;
		this.btnReportePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReportePDF.Location = new System.Drawing.Point(570, 12);
		this.btnReportePDF.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnReportePDF.Name = "btnReportePDF";
		this.btnReportePDF.Size = new System.Drawing.Size(74, 46);
		this.btnReportePDF.TabIndex = 61;
		this.btnReportePDF.Text = "PDF";
		this.btnReportePDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReportePDF.UseVisualStyleBackColor = true;
		this.btnReportePDF.Click += new System.EventHandler(btnReportePDF_Click);
		this.btnSeleccionarMontosMenores.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnSeleccionarMontosMenores.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSeleccionarMontosMenores.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnSeleccionarMontosMenores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnSeleccionarMontosMenores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSeleccionarMontosMenores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSeleccionarMontosMenores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSeleccionarMontosMenores.Location = new System.Drawing.Point(349, 220);
		this.btnSeleccionarMontosMenores.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnSeleccionarMontosMenores.Name = "btnSeleccionarMontosMenores";
		this.btnSeleccionarMontosMenores.Size = new System.Drawing.Size(146, 39);
		this.btnSeleccionarMontosMenores.TabIndex = 60;
		this.btnSeleccionarMontosMenores.Text = "Seleccionar Cotizaciones Menores";
		this.tTMensajes.SetToolTip(this.btnSeleccionarMontosMenores, "Se seleccionan las cotizaciones con menor monto.");
		this.btnSeleccionarMontosMenores.UseVisualStyleBackColor = true;
		this.btnSeleccionarMontosMenores.Click += new System.EventHandler(btnSeleccionarMontosMenores_Click);
		this.btnLimpiarPedidoFinal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnLimpiarPedidoFinal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnLimpiarPedidoFinal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnLimpiarPedidoFinal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnLimpiarPedidoFinal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLimpiarPedidoFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLimpiarPedidoFinal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLimpiarPedidoFinal.Location = new System.Drawing.Point(250, 220);
		this.btnLimpiarPedidoFinal.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnLimpiarPedidoFinal.Name = "btnLimpiarPedidoFinal";
		this.btnLimpiarPedidoFinal.Size = new System.Drawing.Size(91, 39);
		this.btnLimpiarPedidoFinal.TabIndex = 59;
		this.btnLimpiarPedidoFinal.Text = "Limpiar Pedido Final";
		this.tTMensajes.SetToolTip(this.btnLimpiarPedidoFinal, "Limpia la columna Pedido Final");
		this.btnLimpiarPedidoFinal.UseVisualStyleBackColor = true;
		this.btnLimpiarPedidoFinal.Click += new System.EventHandler(btnLimpiarPedidoFinal_Click);
		this.btnRellenarConCantidadSugerida.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnRellenarConCantidadSugerida.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnRellenarConCantidadSugerida.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnRellenarConCantidadSugerida.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnRellenarConCantidadSugerida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRellenarConCantidadSugerida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRellenarConCantidadSugerida.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRellenarConCantidadSugerida.Location = new System.Drawing.Point(119, 220);
		this.btnRellenarConCantidadSugerida.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnRellenarConCantidadSugerida.Name = "btnRellenarConCantidadSugerida";
		this.btnRellenarConCantidadSugerida.Size = new System.Drawing.Size(123, 39);
		this.btnRellenarConCantidadSugerida.TabIndex = 58;
		this.btnRellenarConCantidadSugerida.Text = "Rellenar Con Cantidad Sugerida";
		this.tTMensajes.SetToolTip(this.btnRellenarConCantidadSugerida, "Rellena la columna Pedido Final con los datos de la Columna Cantidad Sugerida");
		this.btnRellenarConCantidadSugerida.UseVisualStyleBackColor = true;
		this.btnRellenarConCantidadSugerida.Click += new System.EventHandler(btnRellenarConCantidadSugerida_Click);
		this.btnRecalcularPropuesta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnRecalcularPropuesta.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnRecalcularPropuesta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnRecalcularPropuesta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnRecalcularPropuesta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRecalcularPropuesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRecalcularPropuesta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRecalcularPropuesta.Location = new System.Drawing.Point(15, 220);
		this.btnRecalcularPropuesta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnRecalcularPropuesta.Name = "btnRecalcularPropuesta";
		this.btnRecalcularPropuesta.Size = new System.Drawing.Size(96, 39);
		this.btnRecalcularPropuesta.TabIndex = 57;
		this.btnRecalcularPropuesta.Text = "Recalcular Propuesta";
		this.tTMensajes.SetToolTip(this.btnRecalcularPropuesta, "Recalcula la propuesta con el Stock Actual.");
		this.btnRecalcularPropuesta.UseVisualStyleBackColor = true;
		this.btnRecalcularPropuesta.Click += new System.EventHandler(btnRecalcularPropuesta_Click);
		this.btnVerListadoTotalPropuesta.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnVerListadoTotalPropuesta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnVerListadoTotalPropuesta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
		this.btnVerListadoTotalPropuesta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnVerListadoTotalPropuesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnVerListadoTotalPropuesta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVerListadoTotalPropuesta.Location = new System.Drawing.Point(305, 71);
		this.btnVerListadoTotalPropuesta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnVerListadoTotalPropuesta.Name = "btnVerListadoTotalPropuesta";
		this.btnVerListadoTotalPropuesta.Size = new System.Drawing.Size(178, 39);
		this.btnVerListadoTotalPropuesta.TabIndex = 56;
		this.btnVerListadoTotalPropuesta.Text = "LIstado Total De Propuesta";
		this.btnVerListadoTotalPropuesta.UseVisualStyleBackColor = true;
		this.btnVerListadoTotalPropuesta.Click += new System.EventHandler(btnVerListadoTotalPropuesta_Click);
		this.btnVerificadorParaCotizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnVerificadorParaCotizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnVerificadorParaCotizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnVerificadorParaCotizar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnVerificadorParaCotizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVerificadorParaCotizar.Location = new System.Drawing.Point(742, 12);
		this.btnVerificadorParaCotizar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnVerificadorParaCotizar.Name = "btnVerificadorParaCotizar";
		this.btnVerificadorParaCotizar.Size = new System.Drawing.Size(146, 46);
		this.btnVerificadorParaCotizar.TabIndex = 55;
		this.btnVerificadorParaCotizar.Text = "Verificador Para Cotizar";
		this.btnVerificadorParaCotizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVerificadorParaCotizar.UseVisualStyleBackColor = true;
		this.btnVerificadorParaCotizar.Click += new System.EventHandler(btnVerificadorParaCotizar_Click);
		this.gbCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gbCotizacion.Controls.Add(this.label8);
		this.gbCotizacion.Controls.Add(this.txtTipoCambio);
		this.gbCotizacion.Controls.Add(this.cmbTipoValorCompra);
		this.gbCotizacion.Controls.Add(this.label7);
		this.gbCotizacion.Controls.Add(this.cmbMoneda);
		this.gbCotizacion.Controls.Add(this.label6);
		this.gbCotizacion.Controls.Add(this.txtProveedor);
		this.gbCotizacion.Controls.Add(this.btnAnadirCotizacion);
		this.gbCotizacion.Controls.Add(this.label5);
		this.gbCotizacion.Controls.Add(this.label1);
		this.gbCotizacion.Controls.Add(this.txtDocCotizacion);
		this.gbCotizacion.Controls.Add(this.btnNuevo);
		this.gbCotizacion.Controls.Add(this.btnQuitar);
		this.gbCotizacion.Location = new System.Drawing.Point(960, 64);
		this.gbCotizacion.Name = "gbCotizacion";
		this.gbCotizacion.Size = new System.Drawing.Size(375, 187);
		this.gbCotizacion.TabIndex = 54;
		this.gbCotizacion.TabStop = false;
		this.gbCotizacion.Text = "Cotizacion";
		this.gbCotizacion.Enter += new System.EventHandler(gbCotizacion_Enter);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Blue;
		this.label8.Location = new System.Drawing.Point(53, 128);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(81, 13);
		this.label8.TabIndex = 58;
		this.label8.Text = "Tipo de Cambio";
		this.tTMensajes.SetToolTip(this.label8, "Este tipo de cambio solo se usa al momento de seleccionar automaticamente las cotizaciones menores.");
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.txtTipoCambio.ForeColor = System.Drawing.Color.Blue;
		this.txtTipoCambio.Location = new System.Drawing.Point(144, 125);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.Size = new System.Drawing.Size(225, 20);
		this.txtTipoCambio.TabIndex = 57;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.tTMensajes.SetToolTip(this.txtTipoCambio, "Este tipo de cambio solo se usa al momento de seleccionar automaticamente las cotizaciones menores.");
		this.txtTipoCambio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTipoCambio_KeyPress);
		this.cmbTipoValorCompra.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbTipoValorCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipoValorCompra.FormattingEnabled = true;
		this.cmbTipoValorCompra.Items.AddRange(new object[2] { "Valor Venta (Sin Igv)", "Precio Venta (Con igv)" });
		this.cmbTipoValorCompra.Location = new System.Drawing.Point(144, 98);
		this.cmbTipoValorCompra.Name = "cmbTipoValorCompra";
		this.cmbTipoValorCompra.Size = new System.Drawing.Size(225, 21);
		this.cmbTipoValorCompra.TabIndex = 56;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(41, 101);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(97, 13);
		this.label7.TabIndex = 55;
		this.label7.Text = "Tipo Valor Compra:";
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Location = new System.Drawing.Point(144, 71);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(225, 21);
		this.cmbMoneda.TabIndex = 54;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(89, 74);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(49, 13);
		this.label6.TabIndex = 53;
		this.label6.Text = "Moneda:";
		this.txtProveedor.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtProveedor.Location = new System.Drawing.Point(144, 19);
		this.txtProveedor.Name = "txtProveedor";
		this.txtProveedor.Size = new System.Drawing.Size(225, 20);
		this.txtProveedor.TabIndex = 1;
		this.txtProveedor.KeyUp += new System.Windows.Forms.KeyEventHandler(txtProveedor_KeyUp);
		this.btnAnadirCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnadirCotizacion.Location = new System.Drawing.Point(270, 151);
		this.btnAnadirCotizacion.Name = "btnAnadirCotizacion";
		this.btnAnadirCotizacion.Size = new System.Drawing.Size(99, 23);
		this.btnAnadirCotizacion.TabIndex = 0;
		this.btnAnadirCotizacion.Text = "Añadir Cotizacion";
		this.btnAnadirCotizacion.UseVisualStyleBackColor = true;
		this.btnAnadirCotizacion.Click += new System.EventHandler(btnAnadirCotizacion_Click);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(53, 48);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(85, 13);
		this.label5.TabIndex = 52;
		this.label5.Text = "Doc. Cotizacion:";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(129, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Seleccione un proveedor:";
		this.txtDocCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDocCotizacion.Location = new System.Drawing.Point(144, 45);
		this.txtDocCotizacion.Name = "txtDocCotizacion";
		this.txtDocCotizacion.Size = new System.Drawing.Size(225, 20);
		this.txtDocCotizacion.TabIndex = 51;
		this.btnNuevo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnNuevo.Location = new System.Drawing.Point(207, 151);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(57, 23);
		this.btnNuevo.TabIndex = 3;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnQuitar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnQuitar.Location = new System.Drawing.Point(144, 151);
		this.btnQuitar.Name = "btnQuitar";
		this.btnQuitar.Size = new System.Drawing.Size(57, 23);
		this.btnQuitar.TabIndex = 50;
		this.btnQuitar.Text = "Quitar";
		this.btnQuitar.UseVisualStyleBackColor = true;
		this.btnQuitar.Click += new System.EventHandler(btnQuitar_Click);
		this.lblModoEdicion.AutoSize = true;
		this.lblModoEdicion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblModoEdicion.ForeColor = System.Drawing.Color.Red;
		this.lblModoEdicion.Location = new System.Drawing.Point(342, 55);
		this.lblModoEdicion.Name = "lblModoEdicion";
		this.lblModoEdicion.Size = new System.Drawing.Size(99, 13);
		this.lblModoEdicion.TabIndex = 53;
		this.lblModoEdicion.Text = "MODO EDICION";
		this.lblModoEdicion.Visible = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.Location = new System.Drawing.Point(1226, 12);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(109, 46);
		this.btnSalir.TabIndex = 49;
		this.btnSalir.Text = "Cancelar";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGenerar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerar.Image = (System.Drawing.Image)resources.GetObject("btnGenerar.Image");
		this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerar.Location = new System.Drawing.Point(896, 12);
		this.btnGenerar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGenerar.Name = "btnGenerar";
		this.btnGenerar.Size = new System.Drawing.Size(216, 46);
		this.btnGenerar.TabIndex = 48;
		this.btnGenerar.Text = "Generar Orden de Compra";
		this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerar.UseVisualStyleBackColor = true;
		this.btnGenerar.Click += new System.EventHandler(btnGenerar_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.Location = new System.Drawing.Point(1120, 12);
		this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(98, 46);
		this.btnGuardar.TabIndex = 47;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(302, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(109, 13);
		this.label4.TabIndex = 9;
		this.label4.Text = "Estado de Propuesta:";
		this.txtEstadoPropuesta.Enabled = false;
		this.txtEstadoPropuesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtEstadoPropuesta.ForeColor = System.Drawing.Color.Red;
		this.txtEstadoPropuesta.Location = new System.Drawing.Point(305, 32);
		this.txtEstadoPropuesta.Name = "txtEstadoPropuesta";
		this.txtEstadoPropuesta.Size = new System.Drawing.Size(178, 20);
		this.txtEstadoPropuesta.TabIndex = 8;
		this.txtEstadoPropuesta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 94);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(66, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "Descripcion:";
		this.txtDescripPropuesta.Location = new System.Drawing.Point(15, 110);
		this.txtDescripPropuesta.Multiline = true;
		this.txtDescripPropuesta.Name = "txtDescripPropuesta";
		this.txtDescripPropuesta.Size = new System.Drawing.Size(271, 85);
		this.txtDescripPropuesta.TabIndex = 6;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 55);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(87, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Titulo Propuesta:";
		this.txtTituloPropuesta.Location = new System.Drawing.Point(15, 71);
		this.txtTituloPropuesta.Name = "txtTituloPropuesta";
		this.txtTituloPropuesta.Size = new System.Drawing.Size(271, 20);
		this.txtTituloPropuesta.TabIndex = 4;
		this.btnTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnTotal.Location = new System.Drawing.Point(1211, 19);
		this.btnTotal.Name = "btnTotal";
		this.btnTotal.Size = new System.Drawing.Size(131, 52);
		this.btnTotal.TabIndex = 57;
		this.btnTotal.Text = "Calcular Totales";
		this.btnTotal.UseVisualStyleBackColor = true;
		this.btnTotal.Click += new System.EventHandler(btnTotal_Click);
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
		this.groupBox3.Controls.Add(this.dgvCotizacion);
		this.groupBox3.Controls.Add(this.btnTotal);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 511);
		this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 50);
		this.groupBox3.MaximumSize = new System.Drawing.Size(2000, 150);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1348, 150);
		this.groupBox3.TabIndex = 4;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Listado De Cotizaciones";
		this.dgvCotizacion.AllowUserToAddRows = false;
		this.dgvCotizacion.AllowUserToDeleteRows = false;
		this.dgvCotizacion.AllowUserToResizeRows = false;
		this.dgvCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvCotizacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvCotizacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCotizacion.Columns.AddRange(this.colDgvCotItem, this.colDgvCotDocCot, this.colDgvCotDesProveedor, this.colDgvCotMoneda, this.colDgvCotTipoPrecio, this.colDgvCotOp_Grav, this.colDgvCotIgv, this.colDgvCotTotal);
		this.dgvCotizacion.Location = new System.Drawing.Point(9, 19);
		this.dgvCotizacion.Name = "dgvCotizacion";
		this.dgvCotizacion.ReadOnly = true;
		this.dgvCotizacion.RowHeadersVisible = false;
		this.dgvCotizacion.Size = new System.Drawing.Size(1196, 119);
		this.dgvCotizacion.TabIndex = 0;
		this.colDgvCotItem.DataPropertyName = "item";
		this.colDgvCotItem.FillWeight = 38.6993f;
		this.colDgvCotItem.HeaderText = "#";
		this.colDgvCotItem.MinimumWidth = 30;
		this.colDgvCotItem.Name = "colDgvCotItem";
		this.colDgvCotItem.ReadOnly = true;
		this.colDgvCotDocCot.DataPropertyName = "docCotizacion";
		this.colDgvCotDocCot.FillWeight = 30.77516f;
		this.colDgvCotDocCot.HeaderText = "Doc. Cotizacion";
		this.colDgvCotDocCot.MinimumWidth = 150;
		this.colDgvCotDocCot.Name = "colDgvCotDocCot";
		this.colDgvCotDocCot.ReadOnly = true;
		this.colDgvCotDesProveedor.DataPropertyName = "desProveedor";
		this.colDgvCotDesProveedor.FillWeight = 576.6497f;
		this.colDgvCotDesProveedor.HeaderText = "Proveedor";
		this.colDgvCotDesProveedor.MinimumWidth = 180;
		this.colDgvCotDesProveedor.Name = "colDgvCotDesProveedor";
		this.colDgvCotDesProveedor.ReadOnly = true;
		this.colDgvCotMoneda.DataPropertyName = "moneda";
		this.colDgvCotMoneda.FillWeight = 30.77516f;
		this.colDgvCotMoneda.HeaderText = "Moneda";
		this.colDgvCotMoneda.MinimumWidth = 110;
		this.colDgvCotMoneda.Name = "colDgvCotMoneda";
		this.colDgvCotMoneda.ReadOnly = true;
		this.colDgvCotTipoPrecio.DataPropertyName = "tipoprecio";
		this.colDgvCotTipoPrecio.FillWeight = 30.77516f;
		this.colDgvCotTipoPrecio.HeaderText = "Tipo Precio";
		this.colDgvCotTipoPrecio.MinimumWidth = 150;
		this.colDgvCotTipoPrecio.Name = "colDgvCotTipoPrecio";
		this.colDgvCotTipoPrecio.ReadOnly = true;
		this.colDgvCotOp_Grav.DataPropertyName = "op_grav";
		this.colDgvCotOp_Grav.FillWeight = 30.77516f;
		this.colDgvCotOp_Grav.HeaderText = "Op. Gravadas";
		this.colDgvCotOp_Grav.MinimumWidth = 110;
		this.colDgvCotOp_Grav.Name = "colDgvCotOp_Grav";
		this.colDgvCotOp_Grav.ReadOnly = true;
		this.colDgvCotIgv.DataPropertyName = "igv";
		this.colDgvCotIgv.FillWeight = 30.77516f;
		this.colDgvCotIgv.HeaderText = "IGV";
		this.colDgvCotIgv.MinimumWidth = 110;
		this.colDgvCotIgv.Name = "colDgvCotIgv";
		this.colDgvCotIgv.ReadOnly = true;
		this.colDgvCotTotal.DataPropertyName = "total";
		this.colDgvCotTotal.FillWeight = 30.77516f;
		this.colDgvCotTotal.HeaderText = "Total";
		this.colDgvCotTotal.MinimumWidth = 110;
		this.colDgvCotTotal.Name = "colDgvCotTotal";
		this.colDgvCotTotal.ReadOnly = true;
		this.panelContenedorFormulario.AutoScroll = true;
		this.panelContenedorFormulario.Controls.Add(this.groupBox2);
		this.panelContenedorFormulario.Controls.Add(this.groupBox1);
		this.panelContenedorFormulario.Controls.Add(this.groupBox3);
		this.panelContenedorFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panelContenedorFormulario.Location = new System.Drawing.Point(0, 0);
		this.panelContenedorFormulario.Name = "panelContenedorFormulario";
		this.panelContenedorFormulario.Size = new System.Drawing.Size(1348, 661);
		this.panelContenedorFormulario.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(1348, 661);
		base.Controls.Add(this.panelContenedorFormulario);
		this.MaximumSize = new System.Drawing.Size(1500, 1500);
		this.MinimumSize = new System.Drawing.Size(1364, 600);
		base.Name = "frmPropuestaDeOrdenCompra";
		this.Text = "Propuesta de Orden de Compra";
		base.Load += new System.EventHandler(frmPropuestaDeOrdenCompra_Load);
		base.Shown += new System.EventHandler(frmPropuestaDeOrdenCompra_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.gbcontienedgvordenesgeneradas.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenesGeneradas).EndInit();
		this.gbCotizacion.ResumeLayout(false);
		this.gbCotizacion.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCotizacion).EndInit();
		this.panelContenedorFormulario.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
