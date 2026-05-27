using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class FormularioParaPruebas : Form
{
	public int CodProv = 0;

	public int CodPropuesta = 0;

	private bool proviene_shown = false;

	private BindingSource data = new BindingSource();

	private DataTable tableDGV = new DataTable();

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

	private List<clsDetallePropuestaDePedido> lista_detalle_prop_cot_selecc = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_detalle_prop_eliminar = new List<clsDetallePropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_insertar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_eliminar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_insertar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_actualizar = new List<clsCotizacionPropuestaDePedido>();

	private List<clsCotizacionPropuestaDePedido> listado_cotizacion_detalle_eliminar = new List<clsCotizacionPropuestaDePedido>();

	public bool modoEdicion = false;

	private DataTable tableCotizaciones = new DataTable();

	private BindingSource dataTableCotizacion = new BindingSource();

	private Color[] colores_columnas = new Color[4]
	{
		Color.FromArgb(0, 255, 179),
		Color.FromArgb(226, 255, 0),
		Color.FromArgb(255, 128, 0),
		Color.FromArgb(255, 0, 202)
	};

	private int i = 0;

	private List<clsDetallePropuestaDePedido> lista_vis_act = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_ins = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_act = new List<clsDetallePropuestaDePedido>();

	private List<clsDetallePropuestaDePedido> lista_del = new List<clsDetallePropuestaDePedido>();

	internal int codPlantilla;

	private object valor_inicial_celda_editar;

	private object valor_correspondiente_a_precio;

	private int ind_col_correspondiente_a_precio = 0;

	private TextBox txtedit = new TextBox();

	private RadTextBoxEditor txtedit1 = new RadTextBoxEditor();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private IContainer components = null;

	private RadGridView dgvlistadopropuesta;

	private GroupBox groupBox2;

	private Label label9;

	private Label label10;

	private Label label11;

	private Label label12;

	private TextBox txtFiltro;

	private GroupBox gbcontienedgvordenesgeneradas;

	private DataGridView dgvOrdenesGeneradas;

	private DataGridViewTextBoxColumn colNumOC;

	private DataGridViewTextBoxColumn colcodOC;

	private DataGridViewTextBoxColumn colEstado;

	public Button btnExcel;

	public Button btnReportePDF;

	public Button btnSeleccionarMontosMenores;

	public Button btnLimpiarPedidoFinal;

	public Button btnRellenarConCantidadSugerida;

	public Button btnRecalcularPropuesta;

	public Button btnVerListadoTotalPropuesta;

	public Button btnVerificadorParaCotizar;

	private GroupBox gbCotizacion;

	private Label label8;

	public TextBox txtTipoCambio;

	private ComboBox cmbTipoValorCompra;

	private Label label7;

	private ComboBox cmbMoneda;

	private Label label6;

	public TextBox txtProveedor;

	private Button btnAnadirCotizacion;

	private Label label5;

	private Label label1;

	public TextBox txtDocCotizacion;

	private Button btnNuevo;

	private Button btnQuitar;

	private Label lblModoEdicion;

	public Button btnSalir;

	public Button btnGenerar;

	public Button btnGuardar;

	private Label label4;

	public TextBox txtEstadoPropuesta;

	private Label label3;

	public TextBox txtDescripPropuesta;

	private Label label2;

	public TextBox txtTituloPropuesta;

	public FormularioParaPruebas()
	{
		InitializeComponent();
	}

	private void btnExportar_Click(object sender, EventArgs e)
	{
		MessageBox.Show("DATA NO CARGADA", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	}

	private void FormularioParaPruebas_Load(object sender, EventArgs e)
	{
		dgvlistadopropuesta.ReadOnly = false;
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
				clsAdmPlantillaDeProductos admplanprod = new clsAdmPlantillaDeProductos();
				clsPlantillaDeProductos plan = admplanprod.CargaProductoAgrupado(prop_pedi.CodPlantillaGenerada);
				if (prop_pedi.CodPlantillaGenerada == 0 || plan == null)
				{
					tableDatosTodos = admPropuestaDePedido.cargaDetallePropuestaDePedidoVisualizacion(prop_pedi.Codigo);
				}
				else
				{
					tableDatosTodos = admPropuestaDePedido.cargaDetallePropuestaDePedidoVisualizacionSegunPlantillaGenerada(prop_pedi.CodPlantillaGenerada, modoEdicion ? prop_pedi.Cod_almacen : frmLogin.iCodAlmacen, prop_pedi.Codigo);
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

	private void inicializatableCotizaciones()
	{
		tableCotizaciones.Columns.Add("item");
		tableCotizaciones.Columns.Add("docCotizacion");
		tableCotizaciones.Columns.Add("desProveedor");
		tableCotizaciones.Columns.Add("moneda");
		tableCotizaciones.Columns.Add("tipoprecio");
		tableCotizaciones.Columns.Add("op_grav");
		tableCotizaciones.Columns.Add("igv");
		tableCotizaciones.Columns.Add("total");
	}

	private void añadiendoColumnaDeCotizacion(int codProveedor, string descripProveedor, string docCotizacion)
	{
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.None, "", "", applyToRow: false);
		BaseFormattingObject b1 = new BaseFormattingObject();
		b1.CellBackColor = colores_columnas[i];
		b1.ApplyOnSelectedRows = false;
		b1.Enabled = true;
		c1.RowBackColor = colores_columnas[i];
		c1.CellBackColor = colores_columnas[i];
		c1.RowForeColor = colores_columnas[i];
		c1.CellForeColor = colores_columnas[i];
		GridViewCheckBoxColumn colCheckBox = new GridViewCheckBoxColumn();
		colCheckBox.HeaderText = "#";
		colCheckBox.FieldName = codProveedor.ToString();
		colCheckBox.Name = codProveedor.ToString();
		colCheckBox.Tag = descripProveedor;
		colCheckBox.ReadOnly = false;
		colCheckBox.Width = 50;
		colCheckBox.AllowFiltering = false;
		colCheckBox.ConditionalFormattingObjectList.Add(b1);
		GridViewDecimalColumn colText = new GridViewDecimalColumn();
		colText.HeaderText = docCotizacion + "\n" + descripProveedor;
		colText.FieldName = descripProveedor;
		colText.Name = descripProveedor;
		colText.Width = 100;
		colText.DecimalPlaces = 4;
		colText.ReadOnly = false;
		colText.AllowFiltering = false;
		colText.ConditionalFormattingObjectList.Add(b1);
		GridViewDecimalColumn colText2 = new GridViewDecimalColumn();
		colText2.HeaderText = docCotizacion + "\nTotalizado";
		colText2.FieldName = descripProveedor + " Totalizado";
		colText2.Name = descripProveedor + " Totalizado";
		colText2.Width = 100;
		colText2.DecimalPlaces = 4;
		colText2.ReadOnly = false;
		colText2.AllowFiltering = false;
		colText2.ConditionalFormattingObjectList.Add(b1);
		dgvlistadopropuesta.Columns.Add(colCheckBox);
		dgvlistadopropuesta.Columns.Add(colText);
		dgvlistadopropuesta.Columns.Add(colText2);
		i++;
		if (i == 4)
		{
			i = 0;
		}
	}

	private void añadirListadoDeCotizacionADGV(List<clsCotizacionPropuestaDePedido> listado_cotizacion)
	{
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			añadiendoColumnaDeCotizacion(item.CodigoProveedor, item.DescripcionProveedor, item.DocCotizacion);
		}
	}

	private void cargaMoneda()
	{
		clsAdmMoneda AdmMoned = new clsAdmMoneda();
		cmbMoneda.DataSource = AdmMoned.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedIndex = 1;
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

	private void FormularioParaPruebas_Shown(object sender, EventArgs e)
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
									MessageBox.Show("Propuesta Guardada Con Exito", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									band_good_job = true;
									recargarPagina();
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
				if (det.Count <= 0)
				{
					throw new NullReferenceException();
				}
				aux2.Codigo = Convert.ToInt32(det[0].Field<object>("codigoDetallePropuesta"));
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

	private bool verificadorDeGuardadoDePropuesta()
	{
		bool band = true;
		if (prop_pedi.Codigo != 0)
		{
			if (listado_cotizacion_insertar.Count > 0 || listado_cotizacion_eliminar.Count > 0 || lista_detalle_prop_actualizar.Count > 0 || lista_detalle_prop_eliminar.Count > 0 || listado_cotizacion_detalle_insertar.Count > 0 || listado_cotizacion_detalle_actualizar.Count > 0 || listado_cotizacion_detalle_eliminar.Count > 0 || lista_detalle_prop_cot_selecc.Count > 0)
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
						dgvlistadopropuesta.Rows[ingresado - 1].Cells[e.ColumnIndex].Value = valor_inicial_celda_editar;
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
				}
				return;
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
			if (e.ColumnIndex == dgvlistadopropuesta.Columns["colPedidoFinal"].Index && Convert.ToInt32((dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value) ? ((object)1) : dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
			{
				dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valor_inicial_celda_editar;
				MessageBox.Show("La Columna Pedido Final no permite ingresar la cantidad cero. Se recomienda dejar vacio la celda", "Valor No Admitido", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			GridViewRowInfo filaActual = dgvlistadopropuesta.Rows[e.RowIndex];
			double valor2 = Convert.ToDouble((filaActual.Cells[e.ColumnIndex].Value == DBNull.Value || filaActual.Cells[e.ColumnIndex].Value == null || filaActual.Cells[e.ColumnIndex].Value == "") ? ((object)double.NaN) : filaActual.Cells[e.ColumnIndex].Value);
			if (!double.IsNaN(valor2))
			{
				filaActual.Cells[e.ColumnIndex].Value = $"{valor2:0.0000}";
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
					filaActual.Cells[nameColumnaSubTotalizado2].Value = "";
				}
				else if (ind_col_correspondiente_a_precio != 0)
				{
					string nameColumnaPrecio2 = getColumnaPrecioSegun(e.ColumnIndex);
					if (nameColumnaPrecio2 != "")
					{
						filaActual.Cells[nameColumnaPrecio2].Value = "";
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
			}
			Console.WriteLine("CellEndEdit " + dgvlistadopropuesta.Columns[e.ColumnIndex].Name + ": " + dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			if (proviene_shown || !modoEdicion)
			{
				return;
			}
			if (e.ColumnIndex == dgvlistadopropuesta.Columns["colPedidoFinal"].Index)
			{
				List<DataRow> fila = (from x in tableDGV.AsEnumerable()
					where Convert.ToInt32(x.Field<object>("codigoDetallePropuesta")) == Convert.ToInt32(filaActual.Cells["colCodigoDetalle"].Value)
					select x).ToList();
				if (fila.Count > 0)
				{
					clsDetallePropuestaDePedido item_prop_ins = convertirFilaEnClase(fila[0]);
					List<clsDetallePropuestaDePedido> item_prop_aux = Enumerable.Where<clsDetallePropuestaDePedido>(lista_detalle_prop_actualizar.AsEnumerable(), (Func<clsDetallePropuestaDePedido, bool>)((clsDetallePropuestaDePedido x) => x.Codigo == item_prop_ins.Codigo)).ToList();
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
						lista_detalle_prop_actualizar.Add(item_prop_ins);
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

	private void limpiaCotizaciones(int filaIndex)
	{
		foreach (clsCotizacionPropuestaDePedido item in listado_cotizacion)
		{
			dgvlistadopropuesta.Rows[filaIndex].Cells[item.DescripcionProveedor + " Totalizado"].Value = null;
		}
	}

	private void dgvlistadopropuesta_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
	{
		if (e.RowIndex == -1)
		{
			return;
		}
		if (dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
		{
			Console.WriteLine("CellBeginEdit " + dgvlistadopropuesta.Columns[e.ColumnIndex].Name + ": " + dgvlistadopropuesta.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
		}
		else
		{
			Console.WriteLine("CellBeginEdit " + dgvlistadopropuesta.Columns[e.ColumnIndex].Name + ": ");
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

	private void dgvlistadopropuesta_EditorRequired(object sender, EditorRequiredEventArgs e)
	{
	}

	private void dgvlistapropuestas_celltextbox_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.NumerosDecimales(e, sender as TextBox, 4);
	}

	private void dgvlistadopropuesta_CellEditorInitialized(object sender, GridViewCellEventArgs e)
	{
	}

	private void btnRecalcularPropuesta_Click(object sender, EventArgs e)
	{
		try
		{
			DataTable nueva_vis = recalculandoDatosParaPropuestaDeOrdenDeCompra(Visualizacion: true);
			DataTable nueva = recalculandoDatosParaPropuestaDeOrdenDeCompra(Visualizacion: false);
			int aux = nueva.Rows.Count;
			if (modoEdicion)
			{
				obteniendoListadosDeDataTable(nueva, visualizacion: false);
				obteniendoListadosDeDataTable(nueva_vis, visualizacion: true);
				if (verificadorDeRecalculo())
				{
					if (admPropuestaDePedido.actualizaDetallePropuestaRecalculo(lista_ins, lista_act, lista_del))
					{
						admPropuestaDePedido.actualizaDetallePropuestaRecalculoVisualizacion(lista_vis_act);
						recargarPagina();
					}
					else
					{
						MessageBox.Show("Ocurrio Un Error al Recalcular Propuesta", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("No se encontraron cambios para recalcular", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

	private bool verificadorDeRecalculo()
	{
		bool band = false;
		if (lista_ins.Count > 0 || lista_act.Count > 0 || lista_del.Count > 0 || lista_vis_act.Count > 0)
		{
			band = true;
		}
		return band;
	}

	private DataTable recalculandoDatosParaPropuestaDeOrdenDeCompra(bool Visualizacion)
	{
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
		foreach (DataRow fila in tableDatosTodos.Rows)
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
				aux.Rows.Add(i, modoEdicion ? detpro.Codigo.ToString() : "", detpro.Codigo_Producto, detpro.Ref_Producto, detpro.Descrip_Producto, detpro.Codigo_Unidad, detpro.Descripcion_Unidad, pro.StockDisponible, ctdad_reponer, ctdad_sugerida, "", ult_pre);
			}
		}
		return aux;
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
			if (algo.Count == 0)
			{
				clsDetallePropuestaDePedido aux = convertirFilaEnClase(fila);
				lista_ins.Add(aux);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.FormularioParaPruebas));
		this.dgvlistadopropuesta = new Telerik.WinControls.UI.RadGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.gbcontienedgvordenesgeneradas = new System.Windows.Forms.GroupBox();
		this.dgvOrdenesGeneradas = new System.Windows.Forms.DataGridView();
		this.colNumOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		this.gbcontienedgvordenesgeneradas.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenesGeneradas).BeginInit();
		this.gbCotizacion.SuspendLayout();
		base.SuspendLayout();
		this.dgvlistadopropuesta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvlistadopropuesta.AutoScroll = true;
		this.dgvlistadopropuesta.BeginEditMode = Telerik.WinControls.RadGridViewBeginEditMode.BeginEditOnEnter;
		this.dgvlistadopropuesta.EnableGestures = false;
		this.dgvlistadopropuesta.Location = new System.Drawing.Point(12, 278);
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
		gridViewTextBoxColumn3.FieldName = "refProd";
		gridViewTextBoxColumn3.HeaderText = "Referencia";
		gridViewTextBoxColumn3.Name = "colReferenciaProducto";
		gridViewTextBoxColumn3.ReadOnly = true;
		gridViewTextBoxColumn3.Width = 103;
		gridViewTextBoxColumn4.FieldName = "descripProd";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "colDescripProducto";
		gridViewTextBoxColumn4.ReadOnly = true;
		gridViewTextBoxColumn4.Width = 259;
		gridViewTextBoxColumn5.FieldName = "codUnidad";
		gridViewTextBoxColumn5.HeaderText = "CodigoUnidad";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodUnidad";
		gridViewTextBoxColumn6.FieldName = "descripUnidad";
		gridViewTextBoxColumn6.HeaderText = "Unidad";
		gridViewTextBoxColumn6.Name = "colDescripUnidad";
		gridViewTextBoxColumn6.ReadOnly = true;
		gridViewTextBoxColumn6.Width = 103;
		gridViewDecimalColumn2.FieldName = "stockDisponible";
		gridViewDecimalColumn2.HeaderText = "Stock Disponible";
		gridViewDecimalColumn2.Name = "colStockDisponible";
		gridViewDecimalColumn2.ReadOnly = true;
		gridViewDecimalColumn2.Width = 100;
		gridViewDecimalColumn3.FieldName = "ctdadReponer";
		gridViewDecimalColumn3.HeaderText = "Ctdad a Reponer";
		gridViewDecimalColumn3.Name = "colCtdadReponer";
		gridViewDecimalColumn3.ReadOnly = true;
		gridViewDecimalColumn3.Width = 100;
		gridViewTextBoxColumn7.AllowSort = false;
		gridViewTextBoxColumn7.FieldName = "ctdadSugerida";
		gridViewTextBoxColumn7.HeaderText = "Ctdad Sugerida";
		gridViewTextBoxColumn7.Name = "colCtdadSugerida";
		gridViewTextBoxColumn7.ReadOnly = true;
		gridViewTextBoxColumn7.Width = 103;
		gridViewDecimalColumn4.AllowReorder = false;
		gridViewDecimalColumn4.AllowResize = false;
		gridViewDecimalColumn4.AllowSort = false;
		gridViewDecimalColumn4.FieldName = "pedidoFinal";
		gridViewDecimalColumn4.HeaderText = "Pedido Final";
		gridViewDecimalColumn4.Name = "colPedidoFinal";
		gridViewDecimalColumn4.Width = 100;
		gridViewTextBoxColumn8.FieldName = "precioUnitarioCompra";
		gridViewTextBoxColumn8.HeaderText = "Precio Unit. Compra";
		gridViewTextBoxColumn8.Name = "colPrecioUnit";
		gridViewTextBoxColumn8.ReadOnly = true;
		gridViewTextBoxColumn8.Width = 104;
		gridViewTextBoxColumn8.WrapText = true;
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
		this.dgvlistadopropuesta.Size = new System.Drawing.Size(1256, 281);
		this.dgvlistadopropuesta.TabIndex = 86;
		this.dgvlistadopropuesta.EditorRequired += new Telerik.WinControls.UI.EditorRequiredEventHandler(dgvlistadopropuesta_EditorRequired);
		this.dgvlistadopropuesta.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(dgvlistadopropuesta_CellBeginEdit);
		this.dgvlistadopropuesta.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvlistadopropuesta_CellEditorInitialized);
		this.dgvlistadopropuesta.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvlistadopropuesta_CellEndEdit);
		this.dgvlistadopropuesta.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvlistadopropuesta_CellClick);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.txtFiltro);
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
		this.groupBox2.Size = new System.Drawing.Size(1280, 265);
		this.groupBox2.TabIndex = 87;
		this.groupBox2.TabStop = false;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.SteelBlue;
		this.label9.Location = new System.Drawing.Point(715, 243);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(12, 12);
		this.label9.TabIndex = 69;
		this.label9.Text = "X";
		this.label9.Visible = false;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.SteelBlue;
		this.label10.Location = new System.Drawing.Point(538, 224);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(29, 12);
		this.label10.TabIndex = 68;
		this.label10.Text = "Por :";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.Color.SteelBlue;
		this.label11.Location = new System.Drawing.Point(573, 224);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(12, 12);
		this.label11.TabIndex = 67;
		this.label11.Text = "X";
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.ForeColor = System.Drawing.Color.SteelBlue;
		this.label12.Location = new System.Drawing.Point(500, 224);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(32, 12);
		this.label12.TabIndex = 66;
		this.label12.Text = "Filtro";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(502, 239);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 65;
		this.gbcontienedgvordenesgeneradas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gbcontienedgvordenesgeneradas.Controls.Add(this.dgvOrdenesGeneradas);
		this.gbcontienedgvordenesgeneradas.Location = new System.Drawing.Point(583, 64);
		this.gbcontienedgvordenesgeneradas.Name = "gbcontienedgvordenesgeneradas";
		this.gbcontienedgvordenesgeneradas.Size = new System.Drawing.Size(303, 117);
		this.gbcontienedgvordenesgeneradas.TabIndex = 64;
		this.gbcontienedgvordenesgeneradas.TabStop = false;
		this.gbcontienedgvordenesgeneradas.Text = "Ordenes de Compra Generadas";
		this.gbcontienedgvordenesgeneradas.Visible = false;
		this.dgvOrdenesGeneradas.AllowUserToAddRows = false;
		this.dgvOrdenesGeneradas.AllowUserToDeleteRows = false;
		this.dgvOrdenesGeneradas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
		this.dgvOrdenesGeneradas.Columns.AddRange(this.colNumOC, this.colcodOC, this.colEstado);
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
		this.colNumOC.DataPropertyName = "numoc";
		this.colNumOC.HeaderText = "Nro. OC";
		this.colNumOC.Name = "colNumOC";
		this.colNumOC.ReadOnly = true;
		this.colcodOC.DataPropertyName = "codOC";
		this.colcodOC.HeaderText = "colCodOC";
		this.colcodOC.Name = "colcodOC";
		this.colcodOC.ReadOnly = true;
		this.colcodOC.Visible = false;
		this.colEstado.DataPropertyName = "estado";
		this.colEstado.HeaderText = "Estado";
		this.colEstado.Name = "colEstado";
		this.colEstado.ReadOnly = true;
		this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExcel.Location = new System.Drawing.Point(584, 12);
		this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnExcel.Name = "btnExcel";
		this.btnExcel.Size = new System.Drawing.Size(82, 46);
		this.btnExcel.TabIndex = 62;
		this.btnExcel.Text = "Excel";
		this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExcel.UseVisualStyleBackColor = true;
		this.btnReportePDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReportePDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReportePDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReportePDF.Image = SIGEFA.Properties.Resources.printer;
		this.btnReportePDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReportePDF.Location = new System.Drawing.Point(502, 12);
		this.btnReportePDF.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnReportePDF.Name = "btnReportePDF";
		this.btnReportePDF.Size = new System.Drawing.Size(74, 46);
		this.btnReportePDF.TabIndex = 61;
		this.btnReportePDF.Text = "PDF";
		this.btnReportePDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReportePDF.UseVisualStyleBackColor = true;
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
		this.btnSeleccionarMontosMenores.UseVisualStyleBackColor = true;
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
		this.btnLimpiarPedidoFinal.UseVisualStyleBackColor = true;
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
		this.btnRellenarConCantidadSugerida.UseVisualStyleBackColor = true;
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
		this.btnVerificadorParaCotizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnVerificadorParaCotizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnVerificadorParaCotizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnVerificadorParaCotizar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnVerificadorParaCotizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVerificadorParaCotizar.Location = new System.Drawing.Point(674, 12);
		this.btnVerificadorParaCotizar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnVerificadorParaCotizar.Name = "btnVerificadorParaCotizar";
		this.btnVerificadorParaCotizar.Size = new System.Drawing.Size(146, 46);
		this.btnVerificadorParaCotizar.TabIndex = 55;
		this.btnVerificadorParaCotizar.Text = "Verificador Para Cotizar";
		this.btnVerificadorParaCotizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVerificadorParaCotizar.UseVisualStyleBackColor = true;
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
		this.gbCotizacion.Location = new System.Drawing.Point(892, 64);
		this.gbCotizacion.Name = "gbCotizacion";
		this.gbCotizacion.Size = new System.Drawing.Size(375, 187);
		this.gbCotizacion.TabIndex = 54;
		this.gbCotizacion.TabStop = false;
		this.gbCotizacion.Text = "Cotizacion";
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.ForeColor = System.Drawing.Color.Blue;
		this.label8.Location = new System.Drawing.Point(53, 128);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(81, 13);
		this.label8.TabIndex = 58;
		this.label8.Text = "Tipo de Cambio";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
		this.txtTipoCambio.ForeColor = System.Drawing.Color.Blue;
		this.txtTipoCambio.Location = new System.Drawing.Point(144, 125);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.Size = new System.Drawing.Size(225, 20);
		this.txtTipoCambio.TabIndex = 57;
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
		this.btnAnadirCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnadirCotizacion.Location = new System.Drawing.Point(270, 151);
		this.btnAnadirCotizacion.Name = "btnAnadirCotizacion";
		this.btnAnadirCotizacion.Size = new System.Drawing.Size(99, 23);
		this.btnAnadirCotizacion.TabIndex = 0;
		this.btnAnadirCotizacion.Text = "Añadir Cotizacion";
		this.btnAnadirCotizacion.UseVisualStyleBackColor = true;
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
		this.btnQuitar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnQuitar.Location = new System.Drawing.Point(144, 151);
		this.btnQuitar.Name = "btnQuitar";
		this.btnQuitar.Size = new System.Drawing.Size(57, 23);
		this.btnQuitar.TabIndex = 50;
		this.btnQuitar.Text = "Quitar";
		this.btnQuitar.UseVisualStyleBackColor = true;
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
		this.btnSalir.Location = new System.Drawing.Point(1158, 12);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(109, 46);
		this.btnSalir.TabIndex = 49;
		this.btnSalir.Text = "Cancelar";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnGenerar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerar.Image = (System.Drawing.Image)resources.GetObject("btnGenerar.Image");
		this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerar.Location = new System.Drawing.Point(828, 12);
		this.btnGenerar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGenerar.Name = "btnGenerar";
		this.btnGenerar.Size = new System.Drawing.Size(216, 46);
		this.btnGenerar.TabIndex = 48;
		this.btnGenerar.Text = "Generar Orden de Compra";
		this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerar.UseVisualStyleBackColor = true;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.Location = new System.Drawing.Point(1052, 12);
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
		this.label3.Location = new System.Drawing.Point(12, 55);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(66, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "Descripcion:";
		this.txtDescripPropuesta.Location = new System.Drawing.Point(15, 71);
		this.txtDescripPropuesta.Multiline = true;
		this.txtDescripPropuesta.Name = "txtDescripPropuesta";
		this.txtDescripPropuesta.Size = new System.Drawing.Size(271, 65);
		this.txtDescripPropuesta.TabIndex = 6;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(87, 13);
		this.label2.TabIndex = 5;
		this.label2.Text = "Titulo Propuesta:";
		this.txtTituloPropuesta.Location = new System.Drawing.Point(15, 32);
		this.txtTituloPropuesta.Name = "txtTituloPropuesta";
		this.txtTituloPropuesta.Size = new System.Drawing.Size(271, 20);
		this.txtTituloPropuesta.TabIndex = 4;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1280, 571);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.dgvlistadopropuesta);
		base.Name = "FormularioParaPruebas";
		this.Text = "FormularioParaPruebas";
		base.Load += new System.EventHandler(FormularioParaPruebas_Load);
		base.Shown += new System.EventHandler(FormularioParaPruebas_Shown);
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvlistadopropuesta).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.gbcontienedgvordenesgeneradas.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenesGeneradas).EndInit();
		this.gbCotizacion.ResumeLayout(false);
		this.gbCotizacion.PerformLayout();
		base.ResumeLayout(false);
	}
}
