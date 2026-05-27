using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class ListadoProductos : Form
{
	public int tipoPlantillaReq = 0;

	public string tituloVentana = "Orden de Compra";

	public DataTable dtDGgvDetalle1 = new DataTable();

	private BindingSource dataDgvDetalle1 = new BindingSource();

	public BindingSource data = new BindingSource();

	private clsAdmFamilia admFam = new clsAdmFamilia();

	private clsFamilia fam = new clsFamilia();

	private clsGrupo gru = new clsGrupo();

	private clsAdmMarca admMar = new clsAdmMarca();

	private clsAdmProveedor admprov = new clsAdmProveedor();

	private clsAdmMarca admmarca = new clsAdmMarca();

	private TextBox txtedit = new TextBox();

	private clsValidar ok = new clsValidar();

	private DataTable d = new DataTable();

	public clsPlantillaDeProductos clsplantillaprod = new clsPlantillaDeProductos();

	private DataTable date = new DataTable();

	private clsAdmPlantillaDeProductos AdmPlantillaProductos = new clsAdmPlantillaDeProductos();

	public int proceso = 0;

	public int codpla = 0;

	public List<clsDetallePlantillaDeProductos> detalleproductosagrupados = new List<clsDetallePlantillaDeProductos>();

	private List<clsDetallePlantillaDeProductos> listainsertar = new List<clsDetallePlantillaDeProductos>();

	private List<clsDetallePlantillaDeProductos> listaactualizar = new List<clsDetallePlantillaDeProductos>();

	private List<clsDetallePlantillaDeProductos> listaeliminar = new List<clsDetallePlantillaDeProductos>();

	public int cuenta = 0;

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmProducto admprod = new clsAdmProducto();

	private clsAdmUnidadEquivalente clsuniequ = new clsAdmUnidadEquivalente();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private CheckBox chkBoxCabecera = new CheckBox();

	private List<int> lnselector = new List<int>();

	private CheckBox chkBoxCabecera_ESM = new CheckBox();

	private CheckBox chkBoxCabecera_LSM = new CheckBox();

	private List<int> lnselector_ll_s_m = new List<int>();

	private bool band_checkbox_principal_dgv = true;

	private DataGridViewComboBoxEditingControl ComboBoxDgv2;

	private System.Drawing.Color fondo_celda;

	private List<int> cod_productos_faltan_generar = new List<int>();

	private string primerValorUndXPaquete = "";

	private IContainer components = null;

	private ComboBox cmbfamilias;

	private Label label3;

	private GroupBox gbPlantilla;

	private Button btnfiltrar;

	public DataGridView dgvDetalle;

	private GroupBox groupBox2;

	private Label label14;

	private Label label13;

	private Label label12;

	public Button btnlimpiar;

	public Button button2;

	private ComboBox cmbmarca;

	private Label label1;

	private GroupBox groupBox3;

	private Label label6;

	private Label label7;

	private Label label10;

	private TextBox txtFiltro;

	private GroupBox groupBox5;

	private Label label62;

	private Label label72;

	private Label label9;

	private TextBox txtFiltro2;

	private ComboBox cmbGrupo;

	private Label label2;

	private ComboBox cmbLinea;

	private Label label8;

	private ComboBox cmbProveedores;

	private Label label11;

	private Button btnagregar;

	private ToolTip toolTip1;

	private DataGridViewCheckBoxColumn colChkSelectorDgv1;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codUniMedida;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn marca;

	private DataGridViewTextBoxColumn familia;

	private DataGridViewTextBoxColumn linea;

	private DataGridViewTextBoxColumn modelo;

	private DataGridViewTextBoxColumn Proveedor;

	public Button btnSalir;

	public Button btnGenerar;

	private Label lblModoPlantilla;

	private PictureBox pbCargando;

	private Label lblCantidadFiltradaUno;

	private Label label16;

	private Label lblCantidadFiltradaDos;

	private Label label18;

	private RadPageView rpvContenedorPestañas;

	private RadPageViewPage rpvListadoFiltro;

	private RadPageViewPage rpvListadoPlantilla;

	private ComboBox cmbTipoPlantilla;

	private Label label15;

	private Label dtfecha;

	private DateTimePicker dateTimePicker1;

	public TextBox txtnombreplantilla;

	private Label label5;

	public TextBox txtdescripcion;

	private Label label4;

	private GroupBox gbListadoFiltro;

	private GroupBox gbListadoPlantilla;

	private Label lbltotalproductos;

	private Label label19;

	private Label lblTotalListadoFiltro;

	private Label label20;

	private RadioButton rbBusquedaExacta1;

	private RadioButton rbBusquedaOptima1;

	private RadioButton rbBusquedaOptima;

	private RadioButton rbBusquedaExacta;

	public Button btnRecargarPlantilla;

	public Button btnVerificadorGenerarPropuesta;

	public Button btnExcel;

	private RadGridView rgvdetalle1;

	private DataGridView dgvdetalle1;

	private DataGridViewCheckBoxColumn colChkEvaluaStockMinimo;

	private DataGridViewCheckBoxColumn colChkLlenaStockMaximo;

	private DataGridViewTextBoxColumn CodigoProducto;

	private DataGridViewTextBoxColumn colCodDetallePlantilla;

	private DataGridViewTextBoxColumn referencias;

	private DataGridViewTextBoxColumn descripcionn;

	private DataGridViewTextBoxColumn colCodUniEqui;

	private DataGridViewTextBoxColumn codUnidadMedida;

	private DataGridViewTextBoxColumn unidadd;

	private DataGridViewComboBoxColumn colCmbUnidad;

	private DataGridViewTextBoxColumn marcaa;

	private DataGridViewTextBoxColumn familiaa;

	private DataGridViewTextBoxColumn lineaa;

	private DataGridViewTextBoxColumn grupoo;

	private DataGridViewTextBoxColumn colTxtStockMinimo;

	private DataGridViewTextBoxColumn colTxtStockMaximo;

	private DataGridViewTextBoxColumn colUndXPaquete;

	private DataGridViewTextBoxColumn difstockMax_sMaxpol;

	private DataGridViewTextBoxColumn promedio_final;

	private DataGridViewTextBoxColumn tipoitem;

	private DataGridViewTextBoxColumn stockmaxpoliticas;

	private DataGridViewTextBoxColumn situacion;

	private DataGridViewTextBoxColumn D36_FR;

	private DataGridViewTextBoxColumn G17_FR;

	private DataGridViewTextBoxColumn G17_LM;

	private DataGridViewTextBoxColumn D36_LM;

	private DataGridViewTextBoxColumn PROMEDIO_1_MES;

	private DataGridViewTextBoxColumn D36_FR1;

	private DataGridViewTextBoxColumn G17_FR1;

	private DataGridViewTextBoxColumn G17_LM1;

	private DataGridViewTextBoxColumn D36_LM1;

	private DataGridViewTextBoxColumn PROMEDIO_3_MES;

	public Button btnactualizacategorizacion;

	private DataTable unidadesequi { get; set; }

	public ListadoProductos()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void cargarComboTipoPlantilla()
	{
		DataTable aux = AdmPlantillaProductos.ListadoTipoPlantilla();
		cmbTipoPlantilla.DisplayMember = "descripcion";
		cmbTipoPlantilla.ValueMember = "codigoTipo";
		cmbTipoPlantilla.DataSource = aux;
		cmbTipoPlantilla.Enabled = false;
	}

	private void ListadoProductos_Load(object sender, EventArgs e)
	{
		tituloVentana = ((tipoPlantillaReq == 0) ? "Requerimiento de Almacen" : tituloVentana);
		unidadesequi = clsuniequ.listar_unidad_equivalente_plantilla_productos();
		btnagregar.Enabled = false;
		CargaFamilias();
		CargaMarcas();
		CargaLineas(-1);
		cargarGrupos(-1);
		CargaProveedores();
		cargarComboTipoPlantilla();
		cmbTipoPlantilla.SelectedIndex = tipoPlantillaReq;
		if (proceso == 1)
		{
			cargaCabeceraFrmListadoProductos();
			cargadetalleproductosagrupados();
			lblModoPlantilla.Text = "MODO EDICION";
			actualizarCHKPrincipal(1);
			actualizarCHKPrincipal(2);
			rpvContenedorPestañas.SelectedPage = rpvListadoPlantilla;
			int permiso = ((tipoPlantillaReq == 0) ? admForm.getPermisoGenerarPropuestaDeReqAlmacen() : admForm.getPermisoGenerarPropuestaDeCompra());
			btnGenerar.Visible = frmLogin.AcesosUsuario.Contains(permiso) || frmLogin.iNivelUser == 1;
		}
		else if (proceso == 2)
		{
			cargaCabeceraFrmListadoProductos();
			cargadetalleproductosagrupados();
			lblModoPlantilla.Text = "MODO VISUALIZACION";
			actualizarCHKPrincipal(1);
			actualizarCHKPrincipal(2);
			rpvContenedorPestañas.SelectedPage = rpvListadoPlantilla;
			btnVerificadorGenerarPropuesta.Enabled = false;
			btnRecargarPlantilla.Enabled = false;
		}
		else
		{
			inicializaDatatableSegunDgv(dtDGgvDetalle1);
			dataDgvDetalle1.DataSource = dtDGgvDetalle1;
			rgvdetalle1.DataSource = dataDgvDetalle1;
			rpvContenedorPestañas.SelectedPage = rpvListadoFiltro;
			btnVerificadorGenerarPropuesta.Visible = false;
			btnRecargarPlantilla.Visible = false;
			btnExcel.Visible = false;
		}
		ActivarEdicionDeColumna();
		AnulandoEdicionColumna();
		añadiendoCheckBoxCabecera();
		añadiendoCheckBoxCabecera1();
		añadiendoCheckBoxCabecera2();
		if (proceso == 2)
		{
			bloquearElementosParaVisualizacion();
		}
		label7.Text = dgvDetalle.Columns[descripcion.Name].HeaderText;
		label6.Text = dgvDetalle.Columns[descripcion.Name].DataPropertyName;
		label72.Text = dgvdetalle1.Columns[descripcionn.Name].HeaderText;
		label62.Text = dgvdetalle1.Columns[descripcionn.Name].DataPropertyName;
		lbltotalproductos.Text = dgvdetalle1.Rows.Count.ToString();
	}

	private void ListadoProductos_Shown(object sender, EventArgs e)
	{
		redibujarCasillasCabeceras();
	}

	private void bloquearElementosParaVisualizacion()
	{
		dgvDetalle.ReadOnly = true;
		dgvdetalle1.ReadOnly = true;
		btnagregar.Enabled = false;
		btnGenerar.Enabled = false;
		button2.Enabled = false;
		cmbTipoPlantilla.Enabled = false;
		txtdescripcion.Enabled = false;
		txtnombreplantilla.Enabled = false;
	}

	public void inicializaDatatableSegunDgv(DataTable aux)
	{
		rgvdetalle1.AutoGenerateColumns = false;
		foreach (GridViewDataColumn col in rgvdetalle1.Columns)
		{
			if (col.FieldName != "" || col.FieldName != "codUnidadMedida")
			{
				aux.Columns.Add(col.FieldName);
			}
		}
	}

	private void CargaFamilias()
	{
		DataTable aux = admFam.MuestraFamilias();
		cmbfamilias.DisplayMember = "descripcion";
		cmbfamilias.ValueMember = "codFamilia";
		DataRow fila = aux.NewRow();
		fila.ItemArray = new object[6]
		{
			-1,
			"TODOS",
			"TODOS",
			0,
			0,
			DateTime.Now
		};
		aux.Rows.InsertAt(fila, 0);
		cmbfamilias.DataSource = aux;
		cmbfamilias.SelectedIndex = 0;
		cmbfamilias.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cmbfamilias.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbfamilias.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void CargaLineas(int CodigoFamilia)
	{
		clsAdmLinea admLinea = new clsAdmLinea();
		cmbLinea.ValueMember = "codLinea";
		cmbLinea.DisplayMember = "descripcion";
		DataTable aux = admLinea.MuestraLineas(CodigoFamilia);
		DataRow fila = aux.NewRow();
		fila.ItemArray = new object[7]
		{
			-1,
			CodigoFamilia,
			"TODOS",
			"TODOS",
			0,
			0,
			DateTime.Now
		};
		aux.Rows.InsertAt(fila, 0);
		cmbLinea.DataSource = aux;
		cmbLinea.SelectedValue = -1;
		cmbLinea.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cmbLinea.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbLinea.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void cargarGrupos(int CodigoLinea)
	{
		clsAdmGrupo admGrupo = new clsAdmGrupo();
		cmbGrupo.ValueMember = "codGrupo";
		cmbGrupo.DisplayMember = "descripcion";
		DataTable aux = admGrupo.MuestraGrupos(CodigoLinea);
		DataRow fila = aux.NewRow();
		fila.ItemArray = new object[7]
		{
			-1,
			CodigoLinea,
			"TODOS",
			"TODOS",
			0,
			0,
			DateTime.Now
		};
		aux.Rows.InsertAt(fila, 0);
		cmbGrupo.DataSource = aux;
		cmbGrupo.SelectedValue = -1;
		cmbGrupo.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cmbGrupo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbGrupo.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void CargaMarcas()
	{
		DataTable aux = admMar.MuestraMarcas();
		cmbmarca.ValueMember = "codMarca";
		cmbmarca.DisplayMember = "descripcion";
		DataRow fila = aux.NewRow();
		fila.ItemArray = new object[5]
		{
			-1,
			"TODOS",
			0,
			0,
			DateTime.Now
		};
		aux.Rows.InsertAt(fila, 0);
		cmbmarca.DataSource = aux;
		cmbmarca.SelectedIndex = 0;
		cmbmarca.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cmbmarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbmarca.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void CargaProveedores()
	{
		DataTable aux = admprov.MuestraProveedores();
		cmbProveedores.DisplayMember = "razonsocial";
		cmbProveedores.ValueMember = "codProveedor";
		DataRow fila = aux.NewRow();
		fila.ItemArray = new object[11]
		{
			-1, 0, "TODOS", "", 0, "", "", 0, 0, 0,
			0
		};
		aux.Rows.InsertAt(fila, 0);
		DataRow fila2 = aux.NewRow();
		fila2.ItemArray = new object[11]
		{
			0, 0, "NINGUNO", "", 0, "", "", 0, 0, 0,
			0
		};
		aux.Rows.InsertAt(fila2, 1);
		cmbProveedores.DataSource = aux;
		cmbProveedores.SelectedValue = -1;
		cmbProveedores.AutoCompleteCustomSource = CargaAutoComplete(aux, "razonsocial");
		cmbProveedores.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cmbProveedores.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	public static AutoCompleteStringCollection CargaAutoComplete(DataTable dt, string nameFila = "descripcion")
	{
		AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
		foreach (DataRow row in dt.Rows)
		{
			stringCol.Add(Convert.ToString(row[nameFila]));
		}
		return stringCol;
	}

	private Size obtenerSizeDeCeldaCabecera(DataGridView Grilla, string nombreColumnaChk, int ctdadDisminuir = 2)
	{
		return new Size(Grilla.Columns[nombreColumnaChk].Width - ctdadDisminuir, Grilla.ColumnHeadersHeight - ctdadDisminuir);
	}

	private void añadiendoCheckBoxCabecera()
	{
		Point CeldaCabeceraLocacion = dgvDetalle.GetCellDisplayRectangle(dgvDetalle.Columns[colChkSelectorDgv1.Name].Index, -1, cutOverflow: true).Location;
		chkBoxCabecera.Size = obtenerSizeDeCeldaCabecera(dgvDetalle, colChkSelectorDgv1.Name);
		chkBoxCabecera.Location = CeldaCabeceraLocacion;
		chkBoxCabecera.CheckAlign = ContentAlignment.MiddleCenter;
		chkBoxCabecera.Click += chkBox_Principal_Clicked;
		if (proceso == 2)
		{
			chkBoxCabecera.Enabled = false;
		}
		dgvDetalle.Controls.Add(chkBoxCabecera);
	}

	private void chkBox_Principal_Clicked(object sender, EventArgs e)
	{
		try
		{
			dgvDetalle.EndEdit();
			lnselector.Clear();
			foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
			{
				DataGridViewCheckBoxCell checkbox = fila.Cells[colChkSelectorDgv1.Name] as DataGridViewCheckBoxCell;
				checkbox.Value = chkBoxCabecera.Checked;
				if (chkBoxCabecera.Checked)
				{
					lnselector.Add(fila.Index);
				}
			}
			if (lnselector.Count > 0)
			{
				btnagregar.Enabled = true;
			}
			else
			{
				btnagregar.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - ListadoProductos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void añadiendoCheckBoxCabecera1()
	{
		Point CeldaCabeceraLocacion = dgvdetalle1.GetCellDisplayRectangle(dgvdetalle1.Columns[colChkEvaluaStockMinimo.Name].Index, -1, cutOverflow: true).Location;
		chkBoxCabecera_ESM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkEvaluaStockMinimo.Name);
		chkBoxCabecera_ESM.Location = CeldaCabeceraLocacion;
		chkBoxCabecera_ESM.CheckAlign = ContentAlignment.MiddleCenter;
		chkBoxCabecera_ESM.Click += chkBox_Principal_1_Clicked;
		toolTip1.SetToolTip(chkBoxCabecera_ESM, "Evalua Stock Minimo");
		if (proceso == 2)
		{
			chkBoxCabecera_ESM.Enabled = false;
		}
		dgvdetalle1.Controls.Add(chkBoxCabecera_ESM);
	}

	private void chkBox_Principal_1_Clicked(object sender, EventArgs e)
	{
		try
		{
			dgvdetalle1.EndEdit();
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalle1.Rows)
			{
				DataGridViewCheckBoxCell checkbox = fila.Cells[colChkEvaluaStockMinimo.Name] as DataGridViewCheckBoxCell;
				DataGridViewCheckBoxCell checkbox2 = fila.Cells[colChkLlenaStockMaximo.Name] as DataGridViewCheckBoxCell;
				checkbox.Value = chkBoxCabecera_ESM.Checked;
				if (Convert.ToBoolean(checkbox.Value ?? ((object)false)) == Convert.ToBoolean(checkbox2.Value ?? ((object)false)))
				{
					checkbox2.Value = !Convert.ToBoolean(checkbox2.Value ?? ((object)false));
				}
				AccionNecesariaDeActualizarEnModoEdicion(fila.Index);
			}
			actualizarCHKPrincipal(2);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - ListadoProductos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void actualizarCHKPrincipal(int nro_columna)
	{
		if (nro_columna == 1)
		{
			bool principal_esm = chkBoxCabecera_ESM.Checked;
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalle1.Rows)
			{
				if (Convert.ToBoolean(fila.Cells[colChkEvaluaStockMinimo.Name].Value ?? ((object)false)) != principal_esm)
				{
					principal_esm = !principal_esm;
					break;
				}
			}
			chkBoxCabecera_ESM.Checked = principal_esm;
		}
		if (nro_columna != 2)
		{
			return;
		}
		bool principal_llsm = chkBoxCabecera_LSM.Checked;
		foreach (DataGridViewRow fila2 in (IEnumerable)dgvdetalle1.Rows)
		{
			if (Convert.ToBoolean(fila2.Cells[colChkLlenaStockMaximo.Name].Value ?? ((object)false)) != principal_llsm)
			{
				principal_llsm = !principal_llsm;
				break;
			}
		}
		chkBoxCabecera_LSM.Checked = principal_llsm;
	}

	private void añadiendoCheckBoxCabecera2()
	{
		Point CeldaCabeceraLocacion = dgvdetalle1.GetCellDisplayRectangle(dgvdetalle1.Columns[colChkLlenaStockMaximo.Name].Index, -1, cutOverflow: true).Location;
		chkBoxCabecera_LSM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkLlenaStockMaximo.Name);
		chkBoxCabecera_LSM.Location = CeldaCabeceraLocacion;
		chkBoxCabecera_LSM.CheckAlign = ContentAlignment.MiddleCenter;
		chkBoxCabecera_LSM.Click += chkBox_Principal_2_Clicked;
		toolTip1.SetToolTip(chkBoxCabecera_LSM, "Rellena Stock Maximo");
		if (proceso == 2)
		{
			chkBoxCabecera_LSM.Enabled = false;
		}
		dgvdetalle1.Controls.Add(chkBoxCabecera_LSM);
	}

	private void chkBox_Principal_2_Clicked(object sender, EventArgs e)
	{
		try
		{
			dgvdetalle1.EndEdit();
			int aux = dtDGgvDetalle1.Rows.Count;
			aux++;
			foreach (DataGridViewRow fila in (IEnumerable)dgvdetalle1.Rows)
			{
				DataGridViewCheckBoxCell checkbox = fila.Cells[colChkLlenaStockMaximo.Name] as DataGridViewCheckBoxCell;
				DataGridViewCheckBoxCell checkbox2 = fila.Cells[colChkEvaluaStockMinimo.Name] as DataGridViewCheckBoxCell;
				checkbox.Value = chkBoxCabecera_LSM.Checked;
				if (Convert.ToBoolean(checkbox.Value ?? ((object)false)) == Convert.ToBoolean(checkbox2.Value ?? ((object)false)))
				{
					checkbox2.Value = !Convert.ToBoolean(checkbox2.Value ?? ((object)false));
				}
				AccionNecesariaDeActualizarEnModoEdicion(fila.Index);
			}
			actualizarCHKPrincipal(1);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - ListadoProductos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void AnulandoEdicionColumna()
	{
		foreach (DataGridViewColumn col in dgvDetalle.Columns)
		{
			col.ReadOnly = true;
		}
	}

	private void todos_checkbox_columna(bool estado, DataGridView dgv, string nombrecolumnachk, List<int> ListaSelectores)
	{
		if (dgv.Rows.Count <= 0)
		{
			return;
		}
		ListaSelectores.Clear();
		foreach (DataGridViewRow fila in (IEnumerable)dgv.Rows)
		{
			fila.Cells[nombrecolumnachk].Value = estado;
			if (estado)
			{
				int n = fila.Index;
				dgv.ClearSelection();
				if (!ListaSelectores.Contains(n))
				{
					ListaSelectores.Add(n);
					int i = ListaSelectores.IndexOf(n);
				}
			}
		}
		if (estado && ListaSelectores.Count <= 0)
		{
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
	}

	private DataRow filaDeDgvaDgv1(DataGridViewRow fila)
	{
		try
		{
			DataRow auxf = dtDGgvDetalle1.NewRow();
			auxf.SetField(colChkEvaluaStockMinimo.DataPropertyName, (object)false);
			auxf.SetField(colChkLlenaStockMaximo.DataPropertyName, (object)false);
			auxf.SetField(CodigoProducto.DataPropertyName, fila.Cells[codproducto.Name].Value);
			auxf.SetField(referencias.DataPropertyName, fila.Cells[referencia.Name].Value);
			auxf.SetField(descripcionn.DataPropertyName, fila.Cells[descripcion.Name].Value);
			auxf.SetField(marcaa.DataPropertyName, fila.Cells[marca.Name].Value);
			auxf.SetField(familiaa.DataPropertyName, fila.Cells[familia.Name].Value);
			auxf.SetField(lineaa.DataPropertyName, fila.Cells[linea.Name].Value);
			auxf.SetField(grupoo.DataPropertyName, fila.Cells[modelo.Name].Value);
			auxf.SetField<object>(colTxtStockMinimo.DataPropertyName, null);
			auxf.SetField<object>(colTxtStockMaximo.DataPropertyName, null);
			auxf.SetField(colUndXPaquete.DataPropertyName, (object)0);
			return auxf;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return null;
		}
	}

	private DataGridViewComboBoxCell obtenerComboAux(int index)
	{
		DataGridViewComboBoxCell auxc = new DataGridViewComboBoxCell();
		DataTable auxt = new DataTable();
		auxt.Columns.Add("Id");
		auxt.Columns.Add("descripcion");
		if (index % 2 == 0)
		{
			auxt.Clear();
			auxt.Rows.Add(1, "Ejemplo 1");
			auxt.Rows.Add(2, "Ejemplo 2");
			auxc.DisplayMember = "descripcion";
			auxc.ValueMember = "Id";
			auxc.DataSource = auxt;
		}
		else
		{
			auxt.Clear();
			auxt.Rows.Add(3, "Ejemplo 3");
			auxt.Rows.Add(4, "Ejemplo 4");
			auxt.Rows.Add(5, "Ejemplo 5");
			auxc.DisplayMember = "descripcion";
			auxc.ValueMember = "Id";
			auxc.DataSource = auxt;
		}
		return auxc;
	}

	private void dgvDetalle_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		try
		{
			if (proceso == 2 || e.RowIndex <= -1 || e.ColumnIndex <= 0)
			{
				return;
			}
			int aux1 = dtDGgvDetalle1.Rows.Count;
			aux1++;
			if (!(dgvDetalle.Columns[e.ColumnIndex].Name != colChkSelectorDgv1.Name))
			{
				return;
			}
			DataRow aux2 = filaDeDgvaDgv1(dgvDetalle.Rows[e.RowIndex]);
			if (aux2 == null)
			{
				return;
			}
			int pos = -1;
			foreach (DataRow fila in dtDGgvDetalle1.Rows)
			{
				if (Convert.ToInt32(fila.Field<object>(CodigoProducto.DataPropertyName)) == Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[codproducto.Name].Value))
				{
					pos = dtDGgvDetalle1.Rows.IndexOf(fila);
					break;
				}
			}
			if (pos == -1)
			{
				dtDGgvDetalle1.Rows.Add(aux2);
				lblCantidadFiltradaDos.Text = dgvdetalle1.Rows.Count.ToString();
				pos = dgvdetalle1.Rows.Count - 1;
				MessageBox.Show("Item: " + dtDGgvDetalle1.Rows[pos].Field<string>(referencias.DataPropertyName) + " se añadio correctamente.", "Plantilla de Productos: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("Item: " + dtDGgvDetalle1.Rows[pos].Field<string>(referencias.DataPropertyName) + " ya está añadido.", "Plantilla de Productos: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Agregar Productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void AccionNecesariaDeInsertarEnModoEdicion(DataRow fila)
	{
		try
		{
			if (proceso == 1)
			{
				List<clsDetallePlantillaDeProductos> lista_aux = Enumerable.Select<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>(Enumerable.Where<clsDetallePlantillaDeProductos>(listaeliminar.AsEnumerable(), (Func<clsDetallePlantillaDeProductos, bool>)((clsDetallePlantillaDeProductos x) => x.Codigo_Producto == fila.Field<int>("CodigoProducto"))), (Func<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>)((clsDetallePlantillaDeProductos x) => x)).ToList();
				if (lista_aux.Count > 0)
				{
					clsDetallePlantillaDeProductos obj = lista_aux[0];
					clsDetallePlantillaDeProductos obj_eli_ins = añadedetalle(fila);
					int ind = listaeliminar.IndexOf(obj);
					obj_eli_ins.Codigo = obj.Codigo;
					listaeliminar.RemoveAt(ind);
					listaactualizar.Add(obj_eli_ins);
				}
				else
				{
					clsDetallePlantillaDeProductos auxdpp = new clsDetallePlantillaDeProductos();
					auxdpp = añadedetalle(fila);
					listainsertar.Add(auxdpp);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		Console.WriteLine("DgvDetalle Row Added:: indice: " + e.RowIndex + " - Cantidad: " + e.RowCount);
	}

	private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (proceso == 2 || e.RowIndex < 0 || e.ColumnIndex != 0)
			{
				return;
			}
			bool isChecked = true;
			foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
			{
				if (!Convert.ToBoolean(fila.Cells[colChkSelectorDgv1.Name].EditedFormattedValue))
				{
					isChecked = false;
					break;
				}
			}
			chkBoxCabecera.Checked = isChecked;
			int n = e.RowIndex;
			dgvDetalle.Rows[n].Cells[e.ColumnIndex].Value = false;
			dgvDetalle.Rows[n].Selected = false;
			dgvDetalle.ClearSelection();
			if (lnselector.Contains(n))
			{
				lnselector.Remove(n);
				band_checkbox_principal_dgv = true;
			}
			else
			{
				lnselector.Add(n);
			}
			foreach (int i in lnselector)
			{
				dgvDetalle.Rows[i].Cells[e.ColumnIndex].Value = true;
			}
			if (lnselector.Count > 0)
			{
				btnagregar.Enabled = true;
			}
			else
			{
				btnagregar.Enabled = false;
			}
			if (lnselector.Count == dgvDetalle.Rows.Count)
			{
				band_checkbox_principal_dgv = false;
				chkBoxCabecera.Checked = true;
			}
			if (lnselector.Count == dgvDetalle.Rows.Count - 1)
			{
				chkBoxCabecera.Checked = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Seleccionar Producto: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void ActivarEdicionDeColumna()
	{
		foreach (GridViewDataColumn dgvc in rgvdetalle1.Columns)
		{
			dgvc.ReadOnly = true;
		}
		rgvdetalle1.Columns["_colChkEvaluaStockMinimo"].ReadOnly = false;
		rgvdetalle1.Columns["_colChkLlenaStockMaximo"].ReadOnly = false;
		rgvdetalle1.Columns["colTxtStockMinimo"].ReadOnly = false;
		rgvdetalle1.Columns["colTxtStockMaximo"].ReadOnly = false;
		rgvdetalle1.Columns["colCmbUnidad1"].ReadOnly = false;
		rgvdetalle1.Columns["colUndXPaquete"].ReadOnly = false;
	}

	private void dgvdetalle1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label72.Text = dgvdetalle1.Columns[e.ColumnIndex].HeaderText;
		label62.Text = dgvdetalle1.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void dgvdetalle1_CellClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void AccionNecesariaDeActualizarEnModoEdicion(int ifilaDgv1)
	{
		try
		{
			if (proceso != 1)
			{
				return;
			}
			GridViewRowInfo row = rgvdetalle1.ChildRows[ifilaDgv1];
			List<clsDetallePlantillaDeProductos> lista = Enumerable.Select<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>(Enumerable.Where<clsDetallePlantillaDeProductos>(listainsertar.AsEnumerable(), (Func<clsDetallePlantillaDeProductos, bool>)((clsDetallePlantillaDeProductos x) => x.Codigo_Producto == Convert.ToInt32(row.Cells["CodigoProducto"].Value))), (Func<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>)((clsDetallePlantillaDeProductos x) => x)).ToList();
			if (lista.Count > 0)
			{
				clsDetallePlantillaDeProductos obj = lista[0];
				List<DataRow> lista2 = (from x in dtDGgvDetalle1.AsEnumerable()
					where x.Field<int>("CodigoProducto") == Convert.ToInt32(row.Cells["CodigoProducto"].Value)
					select (x)).ToList();
				if (lista2.Count > 0)
				{
					DataRow fila = lista2[0];
					clsDetallePlantillaDeProductos obj2 = new clsDetallePlantillaDeProductos();
					obj2 = añadedetalle(fila);
					int ind = listainsertar.IndexOf(obj);
					listainsertar.RemoveAt(ind);
					listainsertar.Insert(ind, obj2);
				}
				else
				{
					MessageBox.Show("Error Catastrofico.\nNo se puede actualizar esta plantilla.\nCierre la plantilla y vuelva abrir", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return;
			}
			List<clsDetallePlantillaDeProductos> lista3 = Enumerable.Select<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>(Enumerable.Where<clsDetallePlantillaDeProductos>(listaactualizar.AsEnumerable(), (Func<clsDetallePlantillaDeProductos, bool>)((clsDetallePlantillaDeProductos x) => x.Codigo_Producto == Convert.ToInt32(row.Cells["CodigoProducto"].Value))), (Func<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>)((clsDetallePlantillaDeProductos x) => x)).ToList();
			if (lista3.Count > 0)
			{
				clsDetallePlantillaDeProductos obj3 = lista3[0];
				List<DataRow> lista4 = (from x in dtDGgvDetalle1.AsEnumerable()
					where x.Field<int>("CodigoProducto") == Convert.ToInt32(row.Cells["CodigoProducto"].Value)
					select (x)).ToList();
				if (lista4.Count > 0)
				{
					DataRow fila2 = lista4[0];
					clsDetallePlantillaDeProductos obj4 = new clsDetallePlantillaDeProductos();
					obj4 = añadedetalle(fila2);
					obj4.Codigo = obj3.Codigo;
					int ind2 = listaactualizar.IndexOf(obj3);
					listaactualizar.RemoveAt(ind2);
					listaactualizar.Insert(ind2, obj4);
				}
				else
				{
					MessageBox.Show("Error Catastrofico.\nNo se puede actualizar esta plantilla.\nCierre la plantilla y vuelva abrir", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				List<DataRow> lista5 = (from x in dtDGgvDetalle1.AsEnumerable()
					where x.Field<int>("CodigoProducto") == Convert.ToInt32(row.Cells["CodigoProducto"].Value)
					select (x)).ToList();
				if (lista5.Count > 0)
				{
					DataRow fila3 = lista5[0];
					clsDetallePlantillaDeProductos auxdpp = new clsDetallePlantillaDeProductos();
					auxdpp = añadedetalle(fila3);
					listaactualizar.Add(auxdpp);
				}
				else
				{
					MessageBox.Show("Error Catastrofico.\nNo se puede actualizar esta plantilla.\nCierre la plantilla y vuelva abrir", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalle1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (proceso != 2 && e.RowIndex >= 0 && dgvdetalle1.Columns[e.ColumnIndex].Name != colChkEvaluaStockMinimo.Name && dgvdetalle1.Columns[e.ColumnIndex].Name != colChkLlenaStockMaximo.Name && dgvdetalle1.Columns[e.ColumnIndex].Name != colCmbUnidad.Name && dgvdetalle1.Columns[e.ColumnIndex].Name != colTxtStockMinimo.Name && dgvdetalle1.Columns[e.ColumnIndex].Name != colTxtStockMaximo.Name && dgvdetalle1.Columns[e.ColumnIndex].Name != colUndXPaquete.Name)
		{
			DialogResult rspta = MessageBox.Show("Esta seguro de elminar: \n> " + dgvdetalle1.Rows[e.RowIndex].Cells[referencias.Name].Value?.ToString() + "\n¿los items eliminados no se podrán recuperar?", Text + " dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rspta == DialogResult.Yes)
			{
				AccionNecesariaAntesDeEliminarEnModoEdicion(e.RowIndex);
				dgvdetalle1.Rows.Remove(dgvdetalle1.Rows[e.RowIndex]);
				dtDGgvDetalle1.AcceptChanges();
				MessageBox.Show("Eliminado");
				lblCantidadFiltradaDos.Text = dgvdetalle1.Rows.Count.ToString();
				lbltotalproductos.Text = dgvdetalle1.Rows.Count.ToString();
			}
		}
	}

	private void AccionNecesariaAntesDeEliminarEnModoEdicion(int ifila)
	{
		try
		{
			DataGridViewRow filadgv1 = dgvdetalle1.Rows[ifila];
			if (proceso != 1)
			{
				return;
			}
			List<clsDetallePlantillaDeProductos> lista_aux = Enumerable.Select<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>(Enumerable.Where<clsDetallePlantillaDeProductos>(listainsertar.AsEnumerable(), (Func<clsDetallePlantillaDeProductos, bool>)((clsDetallePlantillaDeProductos x) => x.Codigo_Producto == Convert.ToInt32(filadgv1.Cells[CodigoProducto.Name].Value))), (Func<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>)((clsDetallePlantillaDeProductos x) => x)).ToList();
			if (lista_aux.Count > 0)
			{
				clsDetallePlantillaDeProductos pp = lista_aux[0];
				int ind = listainsertar.IndexOf(pp);
				listainsertar.RemoveAt(ind);
				return;
			}
			List<clsDetallePlantillaDeProductos> lista_aux2 = Enumerable.Select<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>(Enumerable.Where<clsDetallePlantillaDeProductos>(listaactualizar.AsEnumerable(), (Func<clsDetallePlantillaDeProductos, bool>)((clsDetallePlantillaDeProductos x) => x.Codigo_Producto == Convert.ToInt32(filadgv1.Cells[CodigoProducto.Name].Value))), (Func<clsDetallePlantillaDeProductos, clsDetallePlantillaDeProductos>)((clsDetallePlantillaDeProductos x) => x)).ToList();
			if (lista_aux2.Count > 0)
			{
				clsDetallePlantillaDeProductos obj1 = lista_aux2[0];
				int ind2 = listaactualizar.IndexOf(obj1);
				listaactualizar.RemoveAt(ind2);
			}
			List<DataRow> lista_aux3 = (from x in dtDGgvDetalle1.AsEnumerable()
				where x.Field<int>(CodigoProducto.DataPropertyName) == Convert.ToInt32(filadgv1.Cells[CodigoProducto.Name].Value)
				select (x)).ToList();
			if (lista_aux3.Count > 0)
			{
				DataRow fila = lista_aux3[0];
				clsDetallePlantillaDeProductos obj2 = añadedetalle(fila);
				listaeliminar.Add(obj2);
			}
			else
			{
				MessageBox.Show("Error Catastrofico.\nNo se puede actualizar esta plantilla.\nCierre la plantilla y vuelva abrir", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalle1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		try
		{
			Console.WriteLine("DgvDetalle1:: indice: " + e.RowIndex + " - Cantidad" + e.RowCount);
			if (e.RowCount == 1)
			{
				dgvdetalle1.ClearSelection();
				dgvdetalle1.Rows[e.RowIndex].Selected = true;
				setUnidades(e.RowIndex);
			}
			else if (e.RowCount > 1)
			{
				for (int i = 0; i < e.RowCount; i++)
				{
					int ifila = e.RowIndex + i;
					DataGridViewRow fila = dgvdetalle1.Rows[ifila];
					if (fila != null)
					{
						Console.WriteLine(fila.Cells[referencias.Name].Value?.ToString() + "  --  " + fila.Cells[codUnidadMedida.Name].Value?.ToString() + "  --  " + fila.Cells[colCmbUnidad.Name].Value);
						setUnidades(fila.Index);
						asignarSeleccionadoACmb(fila);
					}
					else
					{
						Console.WriteLine("Error en dgvdetalle1_RowsAdded -> ListadoProductos.cs");
					}
				}
			}
			int aux = dtDGgvDetalle1.Rows.Count;
			aux++;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void asignarSeleccionadoACmb(DataGridViewRow fila)
	{
		DataGridViewComboBoxCell obj = (DataGridViewComboBoxCell)fila.Cells[colCmbUnidad.Name];
		if (obj.DataSource != null && obj.Value == null)
		{
			object codue = fila.Cells[colCodUniEqui.Name].Value;
			object codum = fila.Cells[codUnidadMedida.Name].Value;
			if (!string.IsNullOrEmpty(codum.ToString()))
			{
				int i = 0;
				i++;
				fila.Cells[colCmbUnidad.Name].Value = Convert.ToInt32(codum);
			}
		}
	}

	private void dgvdetalle1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
	{
		string name = e.Column.Name;
		string text = name;
		if (!(text == "colChkEvaluaStockMinimo"))
		{
			if (text == "colChkLlenaStockMaximo")
			{
				chkBoxCabecera_LSM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkLlenaStockMaximo.Name);
				chkBoxCabecera_LSM.Location = dgvdetalle1.GetCellDisplayRectangle(dgvdetalle1.Columns[colChkLlenaStockMaximo.Name].Index, -1, cutOverflow: true).Location;
			}
			else
			{
				Console.WriteLine("Valor Default -> " + e.Column.Index + ": " + e.Column.Name + " --- " + e.Column.Width);
			}
		}
		else
		{
			chkBoxCabecera_ESM.Location = dgvdetalle1.GetCellDisplayRectangle(dgvdetalle1.Columns[colChkEvaluaStockMinimo.Name].Index, -1, cutOverflow: true).Location;
		}
		Console.WriteLine(e.Column.Index + ": " + e.Column.Name + " --- " + e.Column.Width);
	}

	private void dgvdetalle1_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
	{
		chkBoxCabecera_ESM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkEvaluaStockMinimo.Name);
		chkBoxCabecera_LSM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkLlenaStockMaximo.Name);
	}

	private void setUnidades1()
	{
		d = null;
		if (rgvdetalle1.CurrentRow == null)
		{
			return;
		}
		GridViewRowInfo row = rgvdetalle1.CurrentRow;
		GridViewColumn col = rgvdetalle1.CurrentColumn;
		if (row.Index != -1 && col.Index != -1)
		{
			GridViewComboBoxColumn a = rgvdetalle1.Columns["colCmbUnidad1"] as GridViewComboBoxColumn;
			var query = from x in unidadesequi.AsEnumerable()
				where x.Field<int>("codProducto").ToString() == row.Cells["CodigoProducto"].Value.ToString()
				select new
				{
					codUnidadMedida = x.Field<int>("codUnidadMedida"),
					descripcion = x.Field<string>("descripcion")
				};
			var lista = query.ToList();
			a.DataSource = lista;
			a.ValueMember = "codUnidadMedida";
			a.DisplayMember = "descripcion";
			a.FieldName = "colCmbUnidad";
			colCmbUnidad.ReadOnly = false;
			if (string.IsNullOrEmpty(row.Cells[codUnidadMedida.Name].Value.ToString()))
			{
				object value = lista[0].codUnidadMedida;
				a.GetLookupValue(value);
				row.Cells["codUnidadMedida"].Value = lista[0].codUnidadMedida.ToString();
				row.Cells["unidadd"].Value = lista[0].descripcion.ToString();
			}
			else
			{
				object value2 = lista[0].codUnidadMedida;
				a.GetLookupValue(value2);
			}
		}
	}

	private void setUnidades(int ifila)
	{
		d = null;
		if (dgvdetalle1.Rows[ifila] != null)
		{
			DataGridViewRow row = dgvdetalle1.Rows[ifila];
			DataGridViewComboBoxCell a = (DataGridViewComboBoxCell)row.Cells[colCmbUnidad.Name];
			var query = from x in unidadesequi.AsEnumerable()
				where x.Field<int>("codProducto").ToString() == row.Cells[CodigoProducto.Name].Value.ToString()
				select new
				{
					codUnidadMedida = x.Field<int>("codUnidadMedida"),
					descripcion = x.Field<string>("descripcion")
				};
			var lista = query.ToList();
			a.DataSource = lista;
			a.DisplayMember = "descripcion";
			a.ValueMember = "codUnidadMedida";
			a.ReadOnly = false;
			if (string.IsNullOrEmpty(row.Cells[codUnidadMedida.Name].Value.ToString()))
			{
				a.Value = lista[0].codUnidadMedida;
				row.Cells[codUnidadMedida.Name].Value = lista[0].codUnidadMedida.ToString();
				row.Cells[unidadd.Name].Value = lista[0].descripcion.ToString();
			}
			else
			{
				a.Value = Convert.ToInt32(row.Cells[codUnidadMedida.Name].Value);
			}
		}
	}

	public void cargaCabeceraFrmListadoProductos()
	{
		clsplantillaprod = AdmPlantillaProductos.CargaProductoAgrupado(codpla);
		cmbTipoPlantilla.SelectedValue = clsplantillaprod.Tipo;
		txtnombreplantilla.Text = clsplantillaprod.Nombre;
		txtdescripcion.Text = clsplantillaprod.Descripcion;
		dateTimePicker1.Value = clsplantillaprod.FechaRegistro;
		dateTimePicker1.Enabled = false;
	}

	public void cargadetalleproductosagrupados()
	{
		dtDGgvDetalle1 = AdmPlantillaProductos.cargadetalleproductosagrupados_111(codpla, frmLogin.iCodEmpresa);
		dataDgvDetalle1.DataSource = dtDGgvDetalle1;
		rgvdetalle1.DataSource = dataDgvDetalle1;
		rgvdetalle1.AutoGenerateColumns = false;
	}

	public void Actualizar_Categorizacion()
	{
		try
		{
			rgvdetalle1.ClearSelection();
			if (dtDGgvDetalle1.Rows.Count <= 0)
			{
				return;
			}
			detalleproductosagrupados.Clear();
			foreach (DataRow fila in dtDGgvDetalle1.Rows)
			{
				int codproducto = Convert.ToInt32(fila[2]);
				DBAccessMYSQL dBAccess = new DBAccessMYSQL();
				DataSet ds = new DataSet();
				dBAccess.AddParameter("valor", 1);
				dBAccess.AddParameter("_codproducto", codproducto);
				ds = dBAccess.ExecuteDataSet("totalproductosvendidos");
				ds = dBAccess.ExecuteDataSet("promedioproductosvendidos");
			}
			cargadetalleproductosagrupados();
			MessageBox.Show("Categorización Actualizada", "Actualiza Categorización", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void btnfiltrar_Click(object sender, EventArgs e)
	{
		int codmarca = Convert.ToInt32(cmbmarca.SelectedValue);
		int familia = Convert.ToInt32(cmbfamilias.SelectedValue);
		int linea = Convert.ToInt32(cmbLinea.SelectedValue);
		int grupo = Convert.ToInt32(cmbGrupo.SelectedValue);
		int proveedor = Convert.ToInt32(cmbProveedores.SelectedValue);
		dgvDetalle.AutoGenerateColumns = false;
		dgvDetalle.DataSource = data;
		data.DataSource = admprod.listadodeProductos(frmLogin.iCodAlmacen, codmarca, familia, linea, grupo, proveedor);
		data.Filter = string.Empty;
		if (dgvDetalle.Rows.Count == 0)
		{
			MessageBox.Show("Sin Datos Para Mostrar", "Listado de Productos dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			lblCantidadFiltradaUno.Text = "0";
		}
		else
		{
			MessageBox.Show("Se Filtraron " + dgvDetalle.Rows.Count + " datos.", "Listado de Productos dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			lblCantidadFiltradaUno.Text = dgvDetalle.Rows.Count.ToString();
		}
		lnselector.Clear();
		lblTotalListadoFiltro.Text = dgvDetalle.Rows.Count.ToString();
	}

	private void btnlimpiar_Click(object sender, EventArgs e)
	{
		try
		{
			limpiacampos();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtnombreplantilla.Text != "")
			{
				rgvdetalle1.EndEdit();
				rellenaDatosPrincipalesPlantillaProducto();
				if (rgvdetalle1.ChildRows.Count <= 0)
				{
					MessageBox.Show("Debe Ingresar Registro para Poder Guardar", "Plantillas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else if (proceso != 1)
				{
					recorredetalle();
					int codigo_plantilla_creado = -1;
					if (int.TryParse(AdmPlantillaProductos.insertproductosagrupados(clsplantillaprod).ToString(), out codigo_plantilla_creado))
					{
						if (codigo_plantilla_creado == -1)
						{
							MessageBox.Show("Ocurrio Un Error Imprevisto Al Guardar La Plantilla", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						MessageBox.Show("Los datos se guardaron correctamente", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						AccionNecesariaParaSeguirEditandoDespuesdeGuardar(codigo_plantilla_creado);
						lblModoPlantilla.Text = "MODO EDICION";
						Text = txtnombreplantilla.Text;
						int permiso = ((tipoPlantillaReq == 0) ? admForm.getPermisoGenerarPropuestaDeReqAlmacen() : admForm.getPermisoGenerarPropuestaDeCompra());
						btnGenerar.Visible = frmLogin.AcesosUsuario.Contains(permiso) || frmLogin.iNivelUser == 1;
						btnVerificadorGenerarPropuesta.Visible = true;
						btnRecargarPlantilla.Visible = true;
						btnExcel.Visible = true;
					}
				}
				else if (AdmPlantillaProductos.actualizaPlantilla(clsplantillaprod, listainsertar, listaactualizar, listaeliminar))
				{
					MessageBox.Show("Los datos se actualizaron correctamente", Text + "dice: ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					AccionNecesariaParaSeguirEditandoDespuesdeGuardar(clsplantillaprod.Codigo);
					listainsertar.Clear();
					listaactualizar.Clear();
					listaeliminar.Clear();
					Text = txtnombreplantilla.Text;
					int permiso2 = ((tipoPlantillaReq == 0) ? admForm.getPermisoGenerarPropuestaDeReqAlmacen() : admForm.getPermisoGenerarPropuestaDeCompra());
					btnGenerar.Visible = frmLogin.AcesosUsuario.Contains(permiso2) || frmLogin.iNivelUser == 1;
					btnVerificadorGenerarPropuesta.Visible = true;
					btnRecargarPlantilla.Visible = true;
					btnExcel.Visible = true;
				}
			}
			else
			{
				MessageBox.Show("Indique Un Nombre de Plantilla", "Listado de Productos dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtnombreplantilla.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Listado de Productos dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void AccionNecesariaParaSeguirEditandoDespuesdeGuardar(int codigo_plantilla_creado)
	{
		try
		{
			proceso = 1;
			codpla = codigo_plantilla_creado;
			cargaCabeceraFrmListadoProductos();
			DataTable aux = new DataTable();
			inicializaDatatableSegunDgv(aux);
			dataDgvDetalle1.DataSource = aux;
			txtFiltro2.Text = "";
			cargadetalleproductosagrupados();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rellenaDatosPrincipalesPlantillaProducto()
	{
		clsplantillaprod.Nombre = txtnombreplantilla.Text;
		clsplantillaprod.Descripcion = txtdescripcion.Text;
		clsplantillaprod.Tipo = Convert.ToInt32(cmbTipoPlantilla.SelectedValue ?? ((object)0));
		clsplantillaprod.FechaEdicion = DateTime.Now;
		clsplantillaprod.Cod_usuario = frmLogin.iCodUser;
		clsplantillaprod.Nombre_usuario = frmLogin.sNombreUser + " " + frmLogin.sApellidoUSer;
		if (proceso != 1)
		{
			clsplantillaprod.FechaRegistro = DateTime.Now;
			clsplantillaprod.Cod_almacen = frmLogin.iCodAlmacen;
			clsplantillaprod.Descrip_almacen = frmLogin.sAlmacen;
		}
	}

	public void limpiacampos()
	{
		cmbmarca.SelectedValue = -1;
		cmbfamilias.SelectedValue = -1;
		txtdescripcion.Text = "";
		txtnombreplantilla.Text = "";
		if (dgvDetalle.Rows.Count != 0)
		{
			DataTable aux = (DataTable)data.DataSource;
			aux.Clear();
			data.DataSource = aux;
			lblTotalListadoFiltro.Text = dgvDetalle.Rows.Count.ToString();
		}
		txtFiltro.Text = "";
		txtFiltro2.Text = "";
	}

	public void recorredetalle()
	{
		try
		{
			rgvdetalle1.ClearSelection();
			if (dtDGgvDetalle1.Rows.Count <= 0)
			{
				return;
			}
			detalleproductosagrupados.Clear();
			foreach (DataRow fila in dtDGgvDetalle1.Rows)
			{
				clsDetallePlantillaDeProductos pdp = añadedetalle(fila);
				detalleproductosagrupados.Add(pdp);
			}
			clsplantillaprod.LDetalle = detalleproductosagrupados;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private clsDetallePlantillaDeProductos añadedetalle(DataRow fila)
	{
		clsDetallePlantillaDeProductos aux = new clsDetallePlantillaDeProductos();
		aux.Codigo_Producto = Convert.ToInt32(fila.Field<object>("CodigoProducto"));
		aux.Ref_Producto = Convert.ToString(fila.Field<object>("referencias"));
		aux.Descrip_Producto = Convert.ToString(fila.Field<object>("descripcionn"));
		aux.Codigo_Unidad = Convert.ToInt32(fila.Field<object>("codUnidadMedida"));
		aux.Descripcion_Unidad = Convert.ToString(fila.Field<object>("unidadd"));
		aux.Cantidad = Convert.ToDouble(fila.Field<object>("cantidad"));
		aux.Marca = Convert.ToString(fila.Field<object>("marcaa") ?? " ");
		aux.Famiilia = Convert.ToString(fila.Field<object>("familiaa") ?? " ");
		aux.Linea = Convert.ToString(fila.Field<object>("lineaa") ?? " ");
		aux.Grupo = Convert.ToString(fila.Field<object>("grupoo") ?? " ");
		if (double.TryParse((fila.Field<object>("stockminimo") ?? " ").ToString(), out var stockmin))
		{
			aux.StockMinimo = stockmin;
		}
		else
		{
			aux.StockMinimo = double.NaN;
		}
		if (double.TryParse((fila.Field<object>("stockmaximo") ?? " ").ToString(), out var stockmax))
		{
			aux.StockMaximo = stockmax;
		}
		else
		{
			aux.StockMaximo = double.NaN;
		}
		aux.StockActual = double.NaN;
		string stockMin = Convert.ToString(fila.Field<object>("evaluastockminimo"));
		string stockMax = Convert.ToString(fila.Field<object>("llenastockmaximo"));
		if (stockMin == "1" || stockMin == "On" || stockMin == "true")
		{
			aux.OpcionRecuentoCantidad = 1;
		}
		else if (stockMax == "1" || stockMax == "On" || stockMax == "true")
		{
			aux.OpcionRecuentoCantidad = 2;
		}
		else
		{
			aux.OpcionRecuentoCantidad = 0;
		}
		if (proceso == 1)
		{
			if (int.TryParse((fila.Field<object>("codigoDetallePlantilla") ?? " ").ToString(), out var codigoDetalle))
			{
				aux.Codigo = codigoDetalle;
			}
			else
			{
				aux.Codigo = -1;
			}
		}
		return aux;
	}

	private void button3_Click(object sender, EventArgs e)
	{
		bool flag = true;
		MessageBox.Show("\"Guardado\"");
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (rbBusquedaExacta.Checked)
			{
				if (txtFiltro.Text.Length >= 2)
				{
					data.Filter = $"[{label6.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
				}
				else
				{
					data.Filter = string.Empty;
				}
				dgvDetalle.DataSource = data;
			}
			else if (rbBusquedaOptima.Checked)
			{
				if (txtFiltro.Text.Length >= 2)
				{
					List<string> queries = new List<string>();
					if (txtFiltro.Text != "")
					{
						string filterCod = txtFiltro.Text;
						string[] cad = filterCod.Split(' ');
						int cont = 1;
						if (cad.Count() > 1)
						{
							string[] array = cad;
							foreach (string c in array)
							{
								if (cont == 1)
								{
									queries.Add($"[{label6.Text.Trim()}] LIKE '%{c}%'");
									string queryFilter = string.Join(" ", queries);
									data.Filter = queryFilter;
								}
								else
								{
									queries.Add($"[{label6.Text.Trim()}] LIKE '%{c}%'");
									string queryFilter2 = string.Join(" AND ", queries);
									data.Filter = queryFilter2;
								}
								cont++;
							}
						}
						if (cad.Count() == 1)
						{
							data.Filter = $"{label6.Text.Trim()} LIKE '%{filterCod}%'";
						}
					}
				}
				else
				{
					data.Filter = string.Empty;
				}
				dgvDetalle.DataSource = data;
			}
			else
			{
				MessageBox.Show("Ocurrio un problema al establecer el tipo de busqueda", "Informe de Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			lblCantidadFiltradaUno.Text = data.Count.ToString();
			lnselector.Clear();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error Filtrado - " + Text);
		}
	}

	private void txtFiltro2_TextChanged(object sender, EventArgs e)
	{
		try
		{
			int auxc = dtDGgvDetalle1.Rows.Count;
			auxc++;
			if (rbBusquedaExacta1.Checked)
			{
				if (txtFiltro2.Text.Length >= 2)
				{
					dataDgvDetalle1.Filter = $"[{label62.Text.Trim()}] like '*{txtFiltro2.Text.Trim()}*'";
				}
				else
				{
					dataDgvDetalle1.Filter = string.Empty;
				}
				dgvdetalle1.DataSource = dataDgvDetalle1;
			}
			else if (rbBusquedaOptima1.Checked)
			{
				if (txtFiltro2.Text.Length >= 2)
				{
					List<string> queries = new List<string>();
					if (txtFiltro2.Text != "")
					{
						string filterCod = txtFiltro2.Text;
						string[] cad = filterCod.Split(' ');
						int cont = 1;
						if (cad.Count() > 1)
						{
							string[] array = cad;
							foreach (string c in array)
							{
								if (cont == 1)
								{
									queries.Add($"[{label62.Text.Trim()}] LIKE '%{c}%'");
									string queryFilter = string.Join(" ", queries);
									dataDgvDetalle1.Filter = queryFilter;
								}
								else
								{
									queries.Add($"[{label62.Text.Trim()}] LIKE '%{c}%'");
									string queryFilter2 = string.Join(" AND ", queries);
									dataDgvDetalle1.Filter = queryFilter2;
								}
								cont++;
							}
						}
						if (cad.Count() == 1)
						{
							dataDgvDetalle1.Filter = $"{label62.Text.Trim()} LIKE '%{filterCod}%'";
						}
					}
				}
				else
				{
					dataDgvDetalle1.Filter = string.Empty;
				}
				dgvdetalle1.DataSource = dataDgvDetalle1;
			}
			else
			{
				MessageBox.Show("Ocurrio un problema al establecer el tipo de busqueda", "Informe de Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			lblCantidadFiltradaDos.Text = dataDgvDetalle1.Count.ToString();
			actualizarCHKPrincipal(1);
			actualizarCHKPrincipal(2);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error Filtrado - frmCobros");
		}
	}

	private void cmbfamilias_SelectedIndexChanged(object sender, EventArgs e)
	{
		CargaLineas(Convert.ToInt32(cmbfamilias.SelectedValue));
	}

	private void cmbLinea_SelectedIndexChanged(object sender, EventArgs e)
	{
		cargarGrupos(Convert.ToInt32(cmbLinea.SelectedValue));
	}

	private void btnagregar_Click(object sender, EventArgs e)
	{
		try
		{
			if (lnselector.Count > 0)
			{
				string productos_no_agregados = "";
				int cont_prod_no_agregados = 0;
				foreach (int ifila in lnselector)
				{
					DataRow aux = filaDeDgvaDgv1(dgvDetalle.Rows[ifila]);
					if (aux == null)
					{
						continue;
					}
					int pos = -1;
					foreach (DataRow fila in dtDGgvDetalle1.Rows)
					{
						if (Convert.ToInt32(fila.Field<object>(CodigoProducto.DataPropertyName)) == Convert.ToInt32(dgvDetalle.Rows[ifila].Cells[codproducto.Name].Value))
						{
							pos = dtDGgvDetalle1.Rows.IndexOf(fila);
							break;
						}
					}
					if (pos == -1)
					{
						dtDGgvDetalle1.Rows.Add(aux);
						lblCantidadFiltradaDos.Text = rgvdetalle1.Rows.Count.ToString();
						AccionNecesariaDeInsertarEnModoEdicion(aux);
					}
					else
					{
						productos_no_agregados = productos_no_agregados + "\n>> " + dtDGgvDetalle1.Rows[pos].Field<string>(referencias.DataPropertyName);
						cont_prod_no_agregados++;
					}
				}
				string sms = "Error";
				sms = ((cont_prod_no_agregados <= 0) ? ("Se agregaron " + lnselector.Count + " productos.") : ("Se agregaron " + (lnselector.Count - cont_prod_no_agregados) + " productos.\nLos siguientes productos ya estaban registrados:" + productos_no_agregados));
				lbltotalproductos.Text = rgvdetalle1.Rows.Count.ToString();
				MessageBox.Show(sms, "Productos Agregados a Plantilla:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				chkBoxCabecera.Checked = false;
			}
			else
			{
				MessageBox.Show("Ocurrio un Error Interno al agregar los productos seleccionados", "Error Al Agregar Productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Agregar Productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		foreach (GridViewDataColumn col3 in rgvdetalle1.Columns)
		{
		}
	}

	private void dgvdetalle1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		ComboBoxDgv2 = e.Control as DataGridViewComboBoxEditingControl;
		if (ComboBoxDgv2 != null)
		{
			ComboBoxDgv2.SelectedIndexChanged -= ComboBoxDgv2_SelectedIndexChanged;
			ComboBoxDgv2.SelectedIndexChanged += ComboBoxDgv2_SelectedIndexChanged;
		}
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvdetalle1_celltextbox_KeyPress;
			txtedit.KeyPress += dgvdetalle1_celltextbox_KeyPress;
		}
	}

	private void dgvdetalle1_celltextbox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvdetalle1.CurrentCell.OwningColumn.Name == colTxtStockMaximo.Name || dgvdetalle1.CurrentCell.OwningColumn.Name == colTxtStockMinimo.Name || dgvdetalle1.CurrentCell.OwningColumn.Name == colUndXPaquete.Name)
		{
			ok.NumerosDecimales(e, sender as TextBox);
		}
	}

	private void ComboBoxDgv2_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox combo = sender as ComboBox;
		Console.WriteLine(combo.SelectedValue);
		try
		{
			d = unidadesequi;
			if (d != null && combo.SelectedValue != null && combo.SelectedIndex != -1 && combo.SelectedValue.ToString() != "System.Data.DataRowView" && dgvdetalle1.CurrentCell != null)
			{
				string val = combo.SelectedValue.ToString();
				List<string> b = (from x in d.AsEnumerable()
					where x.Field<int>("codUnidadMedida").ToString() == combo.SelectedValue.ToString()
					select x.Field<string>("descripcion")).ToList();
				List<int> c = (from x in d.AsEnumerable()
					where x.Field<int>("codUnidadMedida").ToString() == combo.SelectedValue.ToString()
					select x.Field<int>("codUnidadMedida")).ToList();
				if (b.Any())
				{
					dgvdetalle1.CurrentRow.Cells["unidadd"].Value = b.ToList()[0].ToString();
					dgvdetalle1.CurrentRow.Cells["codUnidadMedida"].Value = c.ToList()[0].ToString();
				}
				AccionNecesariaDeActualizarEnModoEdicion(dgvdetalle1.CurrentRow.Index);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - ListadoProductos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalle1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvDetalle_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvDetalle.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvDetalle.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void dgvdetalle1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1 && e.ColumnIndex != -1 && colCmbUnidad.Index == e.ColumnIndex)
		{
			object cmbval = dgvdetalle1.Rows[e.RowIndex].Cells[colCmbUnidad.Name].Value;
			object codue = dgvdetalle1.Rows[e.RowIndex].Cells[colCodUniEqui.Name].Value;
			object codum = dgvdetalle1.Rows[e.RowIndex].Cells[codUnidadMedida.Name].Value;
			string namecol = dgvdetalle1.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name;
			Console.WriteLine("Fila " + e.RowIndex + " -- Columna: " + namecol + " -- CodeUE" + codum);
			asignarSeleccionadoACmb(dgvdetalle1.Rows[e.RowIndex]);
		}
	}

	private void dgvdetalle1_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		_ = fondo_celda;
		if (false)
		{
			fondo_celda = dgvdetalle1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
		}
		dgvdetalle1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = fondo_celda;
	}

	private void dgvdetalle1_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		fondo_celda = dgvdetalle1.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
		if (e.ColumnIndex == dgvdetalle1.Columns[colTxtStockMinimo.Name].Index || e.ColumnIndex == dgvdetalle1.Columns[colTxtStockMaximo.Name].Index || e.ColumnIndex == dgvdetalle1.Columns[colUndXPaquete.Name].Index)
		{
			dgvdetalle1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		}
	}

	private void dgvdetalle1_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (proceso == 2 || e.KeyCode != Keys.Delete)
			{
				return;
			}
			if (dgvdetalle1.SelectedRows.Count > 0)
			{
				string aux = "";
				foreach (DataGridViewRow fila in dgvdetalle1.SelectedRows)
				{
					aux = aux + "\n> " + fila.Cells[referencias.Name].Value;
				}
				DialogResult rspta = MessageBox.Show("Esta seguro de elminar: " + aux + "\n¿los items eliminados no se podrán recuperar?", Text + " dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rspta != DialogResult.Yes)
				{
					return;
				}
				foreach (DataGridViewRow fila2 in dgvdetalle1.SelectedRows)
				{
					AccionNecesariaAntesDeEliminarEnModoEdicion(fila2.Index);
				}
				foreach (DataGridViewRow fila3 in dgvdetalle1.SelectedRows)
				{
					dgvdetalle1.Rows.Remove(fila3);
				}
				dtDGgvDetalle1.AcceptChanges();
				MessageBox.Show("Items Eliminados", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				lblCantidadFiltradaDos.Text = dgvdetalle1.Rows.Count.ToString();
				lbltotalproductos.Text = dgvdetalle1.Rows.Count.ToString();
			}
			else
			{
				MessageBox.Show("Seleccione uno o mas items para eliminar con la tecla DEL.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Eliminar Items", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvdetalle1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		if (e.RowIndex > -1)
		{
			DataGridViewCell cell = dgvdetalle1.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (dgvdetalle1.Columns[e.ColumnIndex].Name == colChkEvaluaStockMinimo.Name)
			{
				cell.ToolTipText = "Evalua Stock Minimo";
			}
			if (dgvdetalle1.Columns[e.ColumnIndex].Name == colChkLlenaStockMaximo.Name)
			{
				cell.ToolTipText = "Llena a Stock Maximo";
			}
		}
	}

	private void btnSalir_Click_1(object sender, EventArgs e)
	{
		bool band_salir = false;
		if (codpla != 0 && listainsertar.Count == 0 && listaactualizar.Count == 0 && listaeliminar.Count == 0)
		{
			band_salir = true;
		}
		if (band_salir)
		{
			Close();
		}
		else
		{
			DialogResult rspta = MessageBox.Show("Esta seguro de salir. Los cambios que haya hecho sin guardar no se recuperaran.\n ¿Salir de todos modos?", Text + " dice: ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rspta == DialogResult.Yes)
			{
				Close();
			}
		}
		int aux1 = listainsertar.Count;
		int aux2 = listaactualizar.Count;
		int aux3 = listaeliminar.Count;
		aux3++;
	}

	private void btnGenerar_Click(object sender, EventArgs e)
	{
		try
		{
			bool band_guardado = false;
			pbCargando.Visible = true;
			Cursor = Cursors.WaitCursor;
			if (codpla != 0 && listainsertar.Count == 0 && listaactualizar.Count == 0 && listaeliminar.Count == 0)
			{
				band_guardado = true;
			}
			if (band_guardado)
			{
				if (validacionParaGeneracion())
				{
					if (Convert.ToInt32(cmbTipoPlantilla.SelectedValue ?? ((object)0)) == 1)
					{
						DataTable aux1 = creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: false);
						if (aux1.Rows.Count > 0)
						{
							frmPropuestaDeReqAlmacen form = new frmPropuestaDeReqAlmacen();
							form.asignandoDatosDePlantilla(aux1);
							DataTable aux2 = creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: true);
							form.tableDatosTodos = aux2;
							form.MdiParent = base.MdiParent;
							form.txtTituloPropuesta.Text = Text + " " + DateTime.Now.ToString();
							form.txtDescripPropuesta.Text = "Generado de " + Text;
							form.codPlantilla = clsplantillaprod.Codigo;
							form.Show();
						}
						else
						{
							DialogResult rpta = MessageBox.Show("Ningun Producto de la Plantilla a pasado los filtros. Continuar de todas formas?", Text + " dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
							if (rpta == DialogResult.Yes)
							{
								frmPropuestaDeReqAlmacen form2 = new frmPropuestaDeReqAlmacen();
								aux1 = asignandoDatosAMostrarEnPlantillaQueNoGeneraPropuesta();
								form2.asignandoDatosDePlantilla(aux1);
								DataTable aux3 = creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: true);
								form2.tableDatosTodos = aux3;
								form2.MdiParent = base.MdiParent;
								form2.txtTituloPropuesta.Text = Text + " " + DateTime.Now.ToString();
								form2.txtDescripPropuesta.Text = "Generado de " + Text;
								form2.codPlantilla = clsplantillaprod.Codigo;
								form2.Show();
							}
						}
					}
					else if (Convert.ToInt32(cmbTipoPlantilla.SelectedValue ?? ((object)0)) == 2)
					{
						DataTable aux4 = creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: false);
						if (aux4.Rows.Count > 0)
						{
							frmPropuestaDeOrdenCompra form3 = new frmPropuestaDeOrdenCompra();
							form3.asignandoDatosDePlantilla(aux4);
							DataTable aux5 = creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: true);
							form3.tableDatosTodos = aux5;
							form3.MdiParent = base.MdiParent;
							form3.txtTituloPropuesta.Text = Text + " " + DateTime.Now.ToString();
							form3.txtDescripPropuesta.Text = "Generado de " + Text;
							form3.codPlantilla = clsplantillaprod.Codigo;
							form3.Show();
						}
						else
						{
							DialogResult rpta2 = MessageBox.Show("Ningun Producto de la Plantilla a pasado los filtros. Continuar de todas formas?", Text + " dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
							if (rpta2 == DialogResult.Yes)
							{
								frmPropuestaDeOrdenCompra form4 = new frmPropuestaDeOrdenCompra();
								aux4 = asignandoDatosAMostrarEnPlantillaQueNoGeneraPropuesta();
								form4.asignandoDatosDePlantilla(aux4);
								DataTable aux6 = creandoDatosParaPropuestaOrdenDeCompra(Visualizacion: true);
								form4.tableDatosTodos = aux6;
								form4.MdiParent = base.MdiParent;
								form4.txtTituloPropuesta.Text = Text + " " + DateTime.Now.ToString();
								form4.txtDescripPropuesta.Text = "Generado de " + Text;
								form4.codPlantilla = clsplantillaprod.Codigo;
								form4.Show();
							}
						}
					}
					else
					{
						MessageBox.Show("Tiene que seleccionar un tipo de plantilla para poder generar.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						cmbTipoPlantilla.Focus();
					}
				}
				else
				{
					MessageBox.Show("Aun faltan datos que completar para poder generar la propuesta.", "Informacion de Generacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Hay cambios sin guardar.\nGuarda los cambios e intente volviendo a generar", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			pbCargando.Visible = false;
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - ListadoProductos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			pbCargando.Visible = false;
			Cursor = Cursors.Default;
		}
	}

	private DataTable asignandoDatosAMostrarEnPlantillaQueNoGeneraPropuesta()
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
		int i = 0;
		IEnumerator enumerator = dtDGgvDetalle1.Rows.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				DataRow fila = (DataRow)enumerator.Current;
				clsDetallePlantillaDeProductos detpla = añadedetalle(fila);
				string s_ult_pre = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(detpla.Codigo_Producto, detpla.Codigo_Unidad, 0)).ToString("C", CultureInfo.CreateSpecificCulture("es-PE"));
				clsProducto pro = AdmPro.CargaProductoDetalle(detpla.Codigo_Producto, clsplantillaprod.Cod_almacen, 1, 0, 1);
				if (pro == null)
				{
					int uno = 1;
				}
				double ctdad_reponer = 0.0;
				double ctdad_sugerida = 0.0;
				double ctdadFinalStockMin = 0.0;
				double ctdadFinalStockMax = 0.0;
				double factorUE = 1.0;
				if (detpla.Codigo_Unidad == pro.CodUnidadMedida)
				{
					ctdadFinalStockMin = detpla.StockMinimo;
					ctdadFinalStockMax = detpla.StockMaximo;
				}
				else
				{
					clsUnidadEquivalente undequi = AdmPro.CargaUnidadEquivalente(detpla.Codigo_Unidad, pro.CodProducto, 2);
					if (undequi != null)
					{
						factorUE = Convert.ToDouble(undequi.Factor);
						ctdadFinalStockMin = detpla.StockMinimo * factorUE;
						ctdadFinalStockMax = detpla.StockMaximo * factorUE;
					}
					else
					{
						ctdadFinalStockMin = detpla.StockMinimo;
						ctdadFinalStockMax = detpla.StockMaximo;
					}
				}
				if (detpla.OpcionRecuentoCantidad == 1 && Convert.ToDouble(pro.StockDisponible) < ctdadFinalStockMin)
				{
					ctdad_reponer = (ctdadFinalStockMax - Convert.ToDouble(pro.StockDisponible)) / factorUE;
					ctdad_sugerida = ctdad_reponer / detpla.Cantidad;
					ctdad_sugerida = Math.Truncate(ctdad_sugerida) * detpla.Cantidad;
				}
				if (detpla.OpcionRecuentoCantidad == 2 && Convert.ToDouble(pro.StockDisponible) <= ctdadFinalStockMax)
				{
					ctdad_reponer = (ctdadFinalStockMax - Convert.ToDouble(pro.StockDisponible)) / factorUE;
					ctdad_sugerida = ctdad_reponer / detpla.Cantidad;
					ctdad_sugerida = Math.Truncate(ctdad_sugerida) * detpla.Cantidad;
				}
				decimal stockdisponiblesegununidad = default(decimal);
				if (detpla.Codigo_Unidad != pro.CodUnidadMedida)
				{
					clsUnidadEquivalente undequi2 = AdmPro.CargaUnidadEquivalente(detpla.Codigo_Unidad, pro.CodProducto, 2);
					if (undequi2 != null)
					{
						factorUE = Convert.ToDouble(undequi2.Factor);
						stockdisponiblesegununidad = pro.StockDisponible / Convert.ToDecimal((factorUE == 0.0) ? 1.0 : factorUE);
					}
					else
					{
						stockdisponiblesegununidad = pro.StockDisponible;
					}
				}
				else
				{
					stockdisponiblesegununidad = pro.StockDisponible;
				}
				i++;
				aux.Rows.Add(i, "", detpla.Codigo_Producto, detpla.Ref_Producto, detpla.Descrip_Producto, detpla.Codigo_Unidad, detpla.Descripcion_Unidad, stockdisponiblesegununidad, ctdad_reponer, ctdad_sugerida, "", s_ult_pre);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return aux;
	}

	private DataTable generarProductosListoParaLaPropuesta(DataTable tabla)
	{
		DataTable aux = new DataTable();
		foreach (DataColumn col in tabla.Columns)
		{
			aux.Columns.Add(col.ColumnName);
		}
		int i = 0;
		foreach (DataRow fila in tabla.Rows)
		{
			if (Convert.ToDouble(fila.Field<object>("ctdadReponer")) > 0.0)
			{
				i++;
				fila.SetField(0, (object)i);
				aux.Rows.Add(fila.ItemArray);
			}
		}
		return aux;
	}

	public DataTable creandoDatosParaPropuestaOrdenDeCompra(bool Visualizacion)
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
		int j = 0;
		foreach (DataRow fila in dtDGgvDetalle1.Rows)
		{
			clsDetallePlantillaDeProductos detpla = añadedetalle(fila);
			string s_ult_pre = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(detpla.Codigo_Producto, detpla.Codigo_Unidad, 0)).ToString("C", CultureInfo.CreateSpecificCulture("es-PE"));
			clsProducto pro = AdmPro.CargaProductoDetalle(detpla.Codigo_Producto, clsplantillaprod.Cod_almacen, 1, 0, 1);
			if (pro == null)
			{
				int uno = 1;
			}
			double ctdad_reponer = 0.0;
			double ctdad_sugerida = 0.0;
			double ctdadFinalStockMin = 0.0;
			double ctdadFinalStockMax = 0.0;
			double factorUE = 1.0;
			if (detpla.Codigo_Unidad == pro.CodUnidadMedida)
			{
				ctdadFinalStockMin = detpla.StockMinimo;
				ctdadFinalStockMax = detpla.StockMaximo;
			}
			else
			{
				clsUnidadEquivalente undequi = AdmPro.CargaUnidadEquivalente(detpla.Codigo_Unidad, pro.CodProducto, 2);
				if (undequi != null)
				{
					factorUE = Convert.ToDouble(undequi.Factor);
					ctdadFinalStockMin = detpla.StockMinimo * factorUE;
					ctdadFinalStockMax = detpla.StockMaximo * factorUE;
				}
				else
				{
					ctdadFinalStockMin = detpla.StockMinimo;
					ctdadFinalStockMax = detpla.StockMaximo;
				}
			}
			if (detpla.OpcionRecuentoCantidad == 1 && Convert.ToDouble(pro.StockDisponible) <= ctdadFinalStockMin)
			{
				ctdad_reponer = (ctdadFinalStockMax - Convert.ToDouble(pro.StockDisponible)) / factorUE;
				ctdad_sugerida = ctdad_reponer / detpla.Cantidad;
				ctdad_sugerida = Math.Truncate(ctdad_sugerida) * detpla.Cantidad;
				ctdad_reponer = Math.Round(ctdad_reponer, 2);
			}
			if (detpla.OpcionRecuentoCantidad == 2 && Convert.ToDouble(pro.StockDisponible) < ctdadFinalStockMax)
			{
				ctdad_reponer = (ctdadFinalStockMax - Convert.ToDouble(pro.StockDisponible)) / factorUE;
				ctdad_sugerida = ctdad_reponer / detpla.Cantidad;
				ctdad_sugerida = Math.Truncate(ctdad_sugerida) * detpla.Cantidad;
				ctdad_reponer = Math.Round(ctdad_reponer, 2);
			}
			decimal stockdisponiblesegununidad = default(decimal);
			if (detpla.Codigo_Unidad != pro.CodUnidadMedida)
			{
				clsUnidadEquivalente undequi2 = AdmPro.CargaUnidadEquivalente(detpla.Codigo_Unidad, pro.CodProducto, 2);
				if (undequi2 != null)
				{
					factorUE = Convert.ToDouble(undequi2.Factor);
					stockdisponiblesegununidad = pro.StockDisponible / Convert.ToDecimal((factorUE == 0.0) ? 1.0 : factorUE);
					stockdisponiblesegununidad = Math.Round(stockdisponiblesegununidad, 2);
				}
				else
				{
					stockdisponiblesegununidad = pro.StockDisponible;
				}
			}
			else
			{
				stockdisponiblesegununidad = pro.StockDisponible;
			}
			if (Visualizacion)
			{
				double undxPaquete = detpla.Cantidad;
				double stockmin = detpla.StockMinimo;
				double stockmax = detpla.StockMaximo;
				if (double.IsNaN(detpla.StockMaximo))
				{
					throw new ArgumentOutOfRangeException();
				}
				int opcionRecuento = detpla.OpcionRecuentoCantidad;
				if (detpla.OpcionRecuentoCantidad == 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				j++;
				aux.Rows.Add(j, "", detpla.Codigo_Producto, detpla.Ref_Producto, detpla.Descrip_Producto, detpla.Codigo_Unidad, detpla.Descripcion_Unidad, stockdisponiblesegununidad, ctdad_reponer, ctdad_sugerida, "", s_ult_pre, opcionRecuento, stockmin, stockmax, undxPaquete);
			}
			else if (ctdad_reponer > 0.0)
			{
				i++;
				aux.Rows.Add(i, "", detpla.Codigo_Producto, detpla.Ref_Producto, detpla.Descrip_Producto, detpla.Codigo_Unidad, detpla.Descripcion_Unidad, stockdisponiblesegununidad, ctdad_reponer, ctdad_sugerida, null, s_ult_pre);
			}
		}
		return aux;
	}

	public bool validacionParaGeneracion()
	{
		bool band = true;
		foreach (DataRow fila in dtDGgvDetalle1.Rows)
		{
			clsDetallePlantillaDeProductos aux = añadedetalle(fila);
			if (aux.Cantidad == 0.0 || aux.StockMinimo == double.NaN || aux.StockMaximo == double.NaN)
			{
				band = false;
				break;
			}
			if (aux.OpcionRecuentoCantidad == 1)
			{
				if (!(aux.StockMinimo > 0.0) || !(aux.StockMaximo > 0.0))
				{
					band = false;
					break;
				}
			}
			else
			{
				if (aux.OpcionRecuentoCantidad != 2)
				{
					band = false;
					break;
				}
				if (!(aux.StockMaximo > 0.0))
				{
					band = false;
					break;
				}
			}
			if (!(aux.Cantidad <= aux.StockMaximo - aux.StockMinimo))
			{
				band = false;
				break;
			}
		}
		return band;
	}

	public void verificadorParaGeneracion()
	{
		foreach (DataRow fila in dtDGgvDetalle1.Rows)
		{
			clsDetallePlantillaDeProductos aux = añadedetalle(fila);
			if (aux.Cantidad == 0.0 || aux.StockMinimo == double.NaN || aux.StockMaximo == double.NaN)
			{
				cod_productos_faltan_generar.Add(fila.Field<int>(CodigoProducto.DataPropertyName));
				continue;
			}
			if (aux.OpcionRecuentoCantidad == 1)
			{
				if (!(aux.StockMinimo > 0.0) || !(aux.StockMaximo > 0.0))
				{
					cod_productos_faltan_generar.Add(fila.Field<int>(CodigoProducto.DataPropertyName));
				}
			}
			else if (aux.OpcionRecuentoCantidad == 2)
			{
				if (!(aux.StockMaximo > 0.0))
				{
					cod_productos_faltan_generar.Add(fila.Field<int>(CodigoProducto.DataPropertyName));
				}
			}
			else
			{
				cod_productos_faltan_generar.Add(fila.Field<int>(CodigoProducto.DataPropertyName));
			}
			if (!(aux.Cantidad <= aux.StockMaximo - aux.StockMinimo))
			{
				cod_productos_faltan_generar.Add(fila.Field<int>(CodigoProducto.DataPropertyName));
			}
		}
	}

	private void label15_Click(object sender, EventArgs e)
	{
	}

	private void gbListadoFiltro_Enter(object sender, EventArgs e)
	{
	}

	private void dgvdetalle1_Enter(object sender, EventArgs e)
	{
		redibujarCasillasCabeceras();
	}

	private void rpvContenedorPestañas_SelectedPageChanged(object sender, EventArgs e)
	{
		redibujarCasillasCabeceras();
	}

	private void redibujarCasillasCabeceras()
	{
		chkBoxCabecera.Size = obtenerSizeDeCeldaCabecera(dgvDetalle, colChkSelectorDgv1.Name);
		chkBoxCabecera.Location = dgvDetalle.GetCellDisplayRectangle(dgvDetalle.Columns[colChkSelectorDgv1.Name].Index, -1, cutOverflow: true).Location;
		chkBoxCabecera_ESM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkEvaluaStockMinimo.Name);
		chkBoxCabecera_ESM.Location = dgvdetalle1.GetCellDisplayRectangle(dgvdetalle1.Columns[colChkEvaluaStockMinimo.Name].Index, -1, cutOverflow: true).Location;
		chkBoxCabecera_LSM.Size = obtenerSizeDeCeldaCabecera(dgvdetalle1, colChkLlenaStockMaximo.Name);
		chkBoxCabecera_LSM.Location = dgvdetalle1.GetCellDisplayRectangle(dgvdetalle1.Columns[colChkLlenaStockMaximo.Name].Index, -1, cutOverflow: true).Location;
	}

	private void btnVerificadorGenerarPropuesta_Click(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(cmbTipoPlantilla.SelectedValue ?? ((object)0)) == 1)
			{
				cod_productos_faltan_generar = new List<int>();
				verificadorParaGeneracion();
				if (cod_productos_faltan_generar.Count > 0)
				{
					string nombreColumna = CodigoProducto.DataPropertyName;
					string filtro = "";
					for (int i = 0; i < cod_productos_faltan_generar.Count - 1; i++)
					{
						filtro = filtro + nombreColumna + " = " + cod_productos_faltan_generar[i] + " OR ";
					}
					filtro = filtro + nombreColumna + " = " + cod_productos_faltan_generar[cod_productos_faltan_generar.Count - 1];
					dataDgvDetalle1.Filter = filtro;
					MessageBox.Show("El siguiente listado de productos no cumplen con las condiciones para generar una Propuesta de Requerimiento de Almacen", "Verificador de Requerimiento de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					MessageBox.Show("Todos los productos cumplen con las condiciones para generar una Propuesta de Requerimiento de Almacen", "Verificador de Requerimiento de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				if (Convert.ToInt32(cmbTipoPlantilla.SelectedValue ?? ((object)0)) != 2)
				{
					return;
				}
				cod_productos_faltan_generar = new List<int>();
				verificadorParaGeneracion();
				if (cod_productos_faltan_generar.Count > 0)
				{
					string nombreColumna2 = CodigoProducto.DataPropertyName;
					string filtro2 = "";
					for (int j = 0; j < cod_productos_faltan_generar.Count - 1; j++)
					{
						filtro2 = filtro2 + nombreColumna2 + " = " + cod_productos_faltan_generar[j] + " OR ";
					}
					filtro2 = filtro2 + nombreColumna2 + " = " + cod_productos_faltan_generar[cod_productos_faltan_generar.Count - 1];
					dataDgvDetalle1.Filter = filtro2;
					MessageBox.Show("El siguiente listado de productos no cumplen con las condiciones para generar una Propuesta de Orden de Compra", "Verificador de Propuesta de Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					MessageBox.Show("Todos los productos cumplen con las condiciones para generar una Propuesta de Orden de Compra", "Verificador de Propuesta de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Verificador Para Propuesta De " + tituloVentana, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnRecargarPlantilla_Click(object sender, EventArgs e)
	{
		txtFiltro2.Text = "";
		dataDgvDetalle1.Filter = "";
	}

	private void btnExcel_Click(object sender, EventArgs e)
	{
		try
		{
			SLDocument sl = new SLDocument();
			sl.SetCellValue(1, 1, "Plantilla de Productos de " + tituloVentana);
			int indFilaInicial = 2;
			sl.SetCellValue(indFilaInicial, 1, "Item");
			sl.SetCellValue(indFilaInicial, 2, "Referencia");
			sl.SetCellValue(indFilaInicial, 3, "Descripcion");
			sl.SetCellValue(indFilaInicial, 4, "Unidad");
			sl.SetCellValue(indFilaInicial, 5, "Marca");
			sl.SetCellValue(indFilaInicial, 6, "Familia");
			sl.SetCellValue(indFilaInicial, 7, "Linea");
			sl.SetCellValue(indFilaInicial, 8, "Grupo");
			sl.SetCellValue(indFilaInicial, 9, "Stock Minimo");
			sl.SetCellValue(indFilaInicial, 10, "Stock Maximo");
			sl.SetCellValue(indFilaInicial, 11, "Und X Paquetes");
			int indFilaContenido = indFilaInicial;
			int i = 0;
			foreach (DataRow fila in dtDGgvDetalle1.Rows)
			{
				i++;
				indFilaContenido++;
				sl.SetCellValue(indFilaContenido, 1, i);
				sl.SetCellValue(indFilaContenido, 2, fila.Field<object>(referencias.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 3, fila.Field<object>(descripcionn.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 4, fila.Field<object>(unidadd.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 5, (fila.Field<object>(marcaa.DataPropertyName) == DBNull.Value || fila.Field<object>(marcaa.DataPropertyName) == null) ? "" : fila.Field<object>(marcaa.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 6, (fila.Field<object>(familiaa.DataPropertyName) == DBNull.Value || fila.Field<object>(familiaa.DataPropertyName) == null) ? "" : fila.Field<object>(familiaa.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 7, (fila.Field<object>(lineaa.DataPropertyName) == DBNull.Value || fila.Field<object>(lineaa.DataPropertyName) == null) ? "" : fila.Field<object>(lineaa.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 8, (fila.Field<object>(grupoo.DataPropertyName) == DBNull.Value || fila.Field<object>(grupoo.DataPropertyName) == null) ? "" : fila.Field<object>(grupoo.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 9, (fila.Field<object>(colTxtStockMinimo.DataPropertyName) == DBNull.Value || fila.Field<object>(colTxtStockMinimo.DataPropertyName) == null) ? "" : fila.Field<object>(colTxtStockMinimo.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 10, (fila.Field<object>(colTxtStockMaximo.DataPropertyName) == DBNull.Value || fila.Field<object>(colTxtStockMaximo.DataPropertyName) == null) ? "" : fila.Field<object>(colTxtStockMaximo.DataPropertyName).ToString());
				sl.SetCellValue(indFilaContenido, 11, (fila.Field<object>(colUndXPaquete.DataPropertyName) == DBNull.Value || fila.Field<object>(colUndXPaquete.DataPropertyName) == null) ? "" : fila.Field<object>(colUndXPaquete.DataPropertyName).ToString());
			}
			SLStyle estilo = sl.CreateStyle();
			estilo.SetFontColor(System.Drawing.Color.White);
			estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(10, 153, 255), System.Drawing.Color.Blue);
			sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 11, estilo);
			SLStyle aux_style = sl.CreateStyle();
			asignarBordes(aux_style);
			sl.SetCellStyle(indFilaInicial, 1, indFilaContenido, 11, aux_style);
			sl.SetColumnWidth(1, 5.0);
			sl.SetColumnWidth(2, 11.0);
			sl.SetColumnWidth(3, 60.0);
			sl.SetColumnWidth(4, 18.0);
			sl.SetColumnWidth(5, 20.0);
			sl.SetColumnWidth(6, 20.0);
			sl.SetColumnWidth(7, 20.0);
			sl.SetColumnWidth(8, 20.0);
			sl.SetColumnWidth(9, 13.0);
			sl.SetColumnWidth(10, 13.0);
			sl.SetColumnWidth(11, 14.0);
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

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Plantilla_De_Producto")
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
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Plantilla de Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void dgvdetalle1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		if (e.ColumnIndex == dgvdetalle1.Columns[colUndXPaquete.Name].Index)
		{
			string valorStockMin = dgvdetalle1.Rows[e.RowIndex].Cells[colTxtStockMinimo.Name].Value.ToString();
			string valorStockMax = dgvdetalle1.Rows[e.RowIndex].Cells[colTxtStockMaximo.Name].Value.ToString();
			primerValorUndXPaquete = dgvdetalle1.Rows[e.RowIndex].Cells[colUndXPaquete.Name].Value.ToString();
			if (!(valorStockMax != "") || !(valorStockMin != ""))
			{
				MessageBox.Show("Debe rellenar la  cantidad de Stock Min y Stock Max", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				bool rpta = dgvdetalle1.CancelEdit();
				dgvdetalle1.ClearSelection();
				DataGridViewCellEventArgs e2 = new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex);
				dgvdetalle1_CellEndEdit(sender, e2);
			}
		}
	}

	private void rgvdetalle1_RowHeightChanged(object sender, RowHeightChangedEventArgs e)
	{
	}

	private void rgvdetalle1_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (proceso != 2 && e.Row.Index != -1)
		{
			if (rgvdetalle1.Columns[e.Column.Index].Name == "colCmbUnidad1" && rgvdetalle1.CurrentCell != null)
			{
				setUnidades1();
			}
			if (rgvdetalle1.Columns[e.Column.Index].Name == "_colChkEvaluaStockMinimo")
			{
				rgvdetalle1.ChildRows[e.Row.Index].Cells["_colChkLlenaStockMaximo"].Value = 0;
				rgvdetalle1.ChildRows[e.Row.Index].Cells["_colChkEvaluaStockMinimo"].Value = 1;
				AccionNecesariaDeActualizarEnModoEdicion(e.RowIndex);
			}
			if (rgvdetalle1.Columns[e.Column.Index].Name == "_colChkLlenaStockMaximo")
			{
				rgvdetalle1.ChildRows[e.Row.Index].Cells["_colChkEvaluaStockMinimo"].Value = 0;
				rgvdetalle1.ChildRows[e.Row.Index].Cells["_colChkLlenaStockMaximo"].Value = 1;
				AccionNecesariaDeActualizarEnModoEdicion(e.RowIndex);
			}
		}
	}

	private void rgvdetalle1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
	{
		if (e.Row.Index != -1 && e.Column.Index != -1 && e.ColumnIndex == rgvdetalle1.Columns["colUndXPaquete"].Index)
		{
			string valorStockMin = rgvdetalle1.Rows[e.RowIndex].Cells["colTxtStockMinimo"].Value.ToString();
			string valorStockMax = rgvdetalle1.Rows[e.RowIndex].Cells["colTxtStockMaximo"].Value.ToString();
			primerValorUndXPaquete = rgvdetalle1.Rows[e.RowIndex].Cells["colUndXPaquete"].Value.ToString();
			if (!(valorStockMax != "") || !(valorStockMin != ""))
			{
				MessageBox.Show("Debe rellenar la  cantidad de Stock Min y Stock Max", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				bool rpta = rgvdetalle1.CancelEdit();
				rgvdetalle1.ClearSelection();
				DataGridViewCellEventArgs e2 = new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex);
			}
		}
	}

	private void rgvdetalle1_CellFormatting(object sender, CellFormattingEventArgs e)
	{
		if (e.Row.Index != -1 && e.Column.Index != -1)
		{
			if (rgvdetalle1.Columns[e.Column.Index].Name == "colChkEvaluaStockMinimo")
			{
				e.CellElement.ToolTipText = "Evalua Stock Minimo";
			}
			if (rgvdetalle1.Columns[e.ColumnIndex].Name == "colChkLlenaStockMaximo")
			{
				e.CellElement.ToolTipText = "Llena a Stock Maximo";
			}
		}
	}

	private void rgvdetalle1_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
	{
	}

	private void rgvdetalle1_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
	{
	}

	private void rgvdetalle1_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		if (e.Row.Index == -1 || e.Column.Index == -1)
		{
			return;
		}
		string valorStockMax = "";
		if (e.Column.Name == "colTxtStockMaximo")
		{
			valorStockMax = Convert.ToString(rgvdetalle1.Rows[e.RowIndex].Cells[colTxtStockMaximo.Name].Value?.ToString());
			if (valorStockMax == "")
			{
				rgvdetalle1.Rows[e.RowIndex].Cells[colTxtStockMinimo.Name].Value = "0";
				rgvdetalle1.Rows[e.RowIndex].Cells[colTxtStockMaximo.Name].Value = "0";
			}
			else
			{
				rgvdetalle1.Rows[e.RowIndex].Cells[colTxtStockMinimo.Name].Value = Convert.ToDouble(valorStockMax) / 2.0;
			}
			AccionNecesariaDeActualizarEnModoEdicion(e.RowIndex);
		}
	}

	private void rgvdetalle1_CellValueChanged(object sender, GridViewCellEventArgs e)
	{
	}

	private void rgvdetalle1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
	{
	}

	private void rgvdetalle1_SelectionChanged(object sender, EventArgs e)
	{
	}

	private void rgvdetalle1_CurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
	{
		setUnidades1();
	}

	private void rgvdetalle1_UserAddedRow(object sender, GridViewRowEventArgs e)
	{
	}

	private void rgvdetalle1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
	{
	}

	private void rgvdetalle1_CommandCellClick(object sender, GridViewCellEventArgs e)
	{
	}

	private void rgvdetalle1_EditorRequired(object sender, EditorRequiredEventArgs e)
	{
	}

	private void btnactualizacategorizacion_Click(object sender, EventArgs e)
	{
		Actualizar_Categorizacion();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.ListadoProductos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn27 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn28 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn29 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn30 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.cmbfamilias = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.gbPlantilla = new System.Windows.Forms.GroupBox();
		this.rbBusquedaOptima = new System.Windows.Forms.RadioButton();
		this.rbBusquedaExacta = new System.Windows.Forms.RadioButton();
		this.lblTotalListadoFiltro = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.lblCantidadFiltradaUno = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.btnagregar = new System.Windows.Forms.Button();
		this.cmbProveedores = new System.Windows.Forms.ComboBox();
		this.label11 = new System.Windows.Forms.Label();
		this.cmbGrupo = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbLinea = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnfiltrar = new System.Windows.Forms.Button();
		this.cmbmarca = new System.Windows.Forms.ComboBox();
		this.btnlimpiar = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.colChkSelectorDgv1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUniMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.linea = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.rgvdetalle1 = new Telerik.WinControls.UI.RadGridView();
		this.dgvdetalle1 = new System.Windows.Forms.DataGridView();
		this.colChkEvaluaStockMinimo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.colChkLlenaStockMaximo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.CodigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodDetallePlantilla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencias = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcionn = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodUniEqui = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUnidadMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidadd = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCmbUnidad = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.marcaa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.familiaa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lineaa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.grupoo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTxtStockMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTxtStockMaximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colUndXPaquete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.difstockMax_sMaxpol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.promedio_final = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoitem = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockmaxpoliticas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.situacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.D36_FR = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.G17_FR = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.G17_LM = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.D36_LM = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.PROMEDIO_1_MES = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.D36_FR1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.G17_FR1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.G17_LM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.D36_LM1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.PROMEDIO_3_MES = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.btnactualizacategorizacion = new System.Windows.Forms.Button();
		this.btnExcel = new System.Windows.Forms.Button();
		this.btnRecargarPlantilla = new System.Windows.Forms.Button();
		this.btnVerificadorGenerarPropuesta = new System.Windows.Forms.Button();
		this.rbBusquedaOptima1 = new System.Windows.Forms.RadioButton();
		this.rbBusquedaExacta1 = new System.Windows.Forms.RadioButton();
		this.lbltotalproductos = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.cmbTipoPlantilla = new System.Windows.Forms.ComboBox();
		this.label15 = new System.Windows.Forms.Label();
		this.dtfecha = new System.Windows.Forms.Label();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.txtnombreplantilla = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtdescripcion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.lblCantidadFiltradaDos = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.pbCargando = new System.Windows.Forms.PictureBox();
		this.lblModoPlantilla = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGenerar = new System.Windows.Forms.Button();
		this.label62 = new System.Windows.Forms.Label();
		this.label72 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtFiltro2 = new System.Windows.Forms.TextBox();
		this.button2 = new System.Windows.Forms.Button();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.rpvContenedorPestañas = new Telerik.WinControls.UI.RadPageView();
		this.rpvListadoFiltro = new Telerik.WinControls.UI.RadPageViewPage();
		this.gbListadoFiltro = new System.Windows.Forms.GroupBox();
		this.rpvListadoPlantilla = new Telerik.WinControls.UI.RadPageViewPage();
		this.gbListadoPlantilla = new System.Windows.Forms.GroupBox();
		this.gbPlantilla.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle1.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvdetalle1).BeginInit();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCargando).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rpvContenedorPestañas).BeginInit();
		this.rpvContenedorPestañas.SuspendLayout();
		this.rpvListadoFiltro.SuspendLayout();
		this.gbListadoFiltro.SuspendLayout();
		this.rpvListadoPlantilla.SuspendLayout();
		this.gbListadoPlantilla.SuspendLayout();
		base.SuspendLayout();
		this.cmbfamilias.FormattingEnabled = true;
		this.cmbfamilias.Location = new System.Drawing.Point(81, 51);
		this.cmbfamilias.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.cmbfamilias.Name = "cmbfamilias";
		this.cmbfamilias.Size = new System.Drawing.Size(221, 23);
		this.cmbfamilias.TabIndex = 2;
		this.cmbfamilias.SelectedIndexChanged += new System.EventHandler(cmbfamilias_SelectedIndexChanged);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(25, 55);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(55, 15);
		this.label3.TabIndex = 7;
		this.label3.Text = "Familia";
		this.gbPlantilla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbPlantilla.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.gbPlantilla.Controls.Add(this.rbBusquedaOptima);
		this.gbPlantilla.Controls.Add(this.rbBusquedaExacta);
		this.gbPlantilla.Controls.Add(this.lblTotalListadoFiltro);
		this.gbPlantilla.Controls.Add(this.label20);
		this.gbPlantilla.Controls.Add(this.lblCantidadFiltradaUno);
		this.gbPlantilla.Controls.Add(this.label16);
		this.gbPlantilla.Controls.Add(this.btnagregar);
		this.gbPlantilla.Controls.Add(this.cmbProveedores);
		this.gbPlantilla.Controls.Add(this.label11);
		this.gbPlantilla.Controls.Add(this.cmbGrupo);
		this.gbPlantilla.Controls.Add(this.label2);
		this.gbPlantilla.Controls.Add(this.cmbLinea);
		this.gbPlantilla.Controls.Add(this.label8);
		this.gbPlantilla.Controls.Add(this.label6);
		this.gbPlantilla.Controls.Add(this.label7);
		this.gbPlantilla.Controls.Add(this.label10);
		this.gbPlantilla.Controls.Add(this.txtFiltro);
		this.gbPlantilla.Controls.Add(this.btnfiltrar);
		this.gbPlantilla.Controls.Add(this.cmbfamilias);
		this.gbPlantilla.Controls.Add(this.label3);
		this.gbPlantilla.Controls.Add(this.cmbmarca);
		this.gbPlantilla.Controls.Add(this.btnlimpiar);
		this.gbPlantilla.Controls.Add(this.label1);
		this.gbPlantilla.Location = new System.Drawing.Point(6, 15);
		this.gbPlantilla.Name = "gbPlantilla";
		this.gbPlantilla.Size = new System.Drawing.Size(1373, 148);
		this.gbPlantilla.TabIndex = 8;
		this.gbPlantilla.TabStop = false;
		this.gbPlantilla.Text = "Filtrado Por :";
		this.rbBusquedaOptima.AutoSize = true;
		this.rbBusquedaOptima.Checked = true;
		this.rbBusquedaOptima.Location = new System.Drawing.Point(331, 117);
		this.rbBusquedaOptima.Name = "rbBusquedaOptima";
		this.rbBusquedaOptima.Size = new System.Drawing.Size(117, 17);
		this.rbBusquedaOptima.TabIndex = 70;
		this.rbBusquedaOptima.TabStop = true;
		this.rbBusquedaOptima.Text = "Busqueda Optima";
		this.rbBusquedaOptima.UseVisualStyleBackColor = true;
		this.rbBusquedaExacta.AutoSize = true;
		this.rbBusquedaExacta.Location = new System.Drawing.Point(331, 94);
		this.rbBusquedaExacta.Name = "rbBusquedaExacta";
		this.rbBusquedaExacta.Size = new System.Drawing.Size(111, 17);
		this.rbBusquedaExacta.TabIndex = 71;
		this.rbBusquedaExacta.Text = "Busqueda Exacta";
		this.rbBusquedaExacta.UseVisualStyleBackColor = true;
		this.lblTotalListadoFiltro.AutoSize = true;
		this.lblTotalListadoFiltro.BackColor = System.Drawing.Color.Transparent;
		this.lblTotalListadoFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalListadoFiltro.ForeColor = System.Drawing.Color.Black;
		this.lblTotalListadoFiltro.Location = new System.Drawing.Point(784, 104);
		this.lblTotalListadoFiltro.Name = "lblTotalListadoFiltro";
		this.lblTotalListadoFiltro.Size = new System.Drawing.Size(11, 12);
		this.lblTotalListadoFiltro.TabIndex = 65;
		this.lblTotalListadoFiltro.Text = "0";
		this.label20.AutoSize = true;
		this.label20.BackColor = System.Drawing.Color.Transparent;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.Color.Black;
		this.label20.Location = new System.Drawing.Point(687, 104);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(91, 12);
		this.label20.TabIndex = 64;
		this.label20.Text = "Cant. Productos:";
		this.lblCantidadFiltradaUno.AutoSize = true;
		this.lblCantidadFiltradaUno.BackColor = System.Drawing.Color.Transparent;
		this.lblCantidadFiltradaUno.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadFiltradaUno.ForeColor = System.Drawing.Color.Black;
		this.lblCantidadFiltradaUno.Location = new System.Drawing.Point(631, 123);
		this.lblCantidadFiltradaUno.Name = "lblCantidadFiltradaUno";
		this.lblCantidadFiltradaUno.Size = new System.Drawing.Size(11, 12);
		this.lblCantidadFiltradaUno.TabIndex = 51;
		this.lblCantidadFiltradaUno.Text = "0";
		this.label16.AutoSize = true;
		this.label16.BackColor = System.Drawing.Color.Transparent;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.Color.Black;
		this.label16.Location = new System.Drawing.Point(454, 122);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(171, 12);
		this.label16.TabIndex = 50;
		this.label16.Text = "Cantidad de Productos Filtrados:";
		this.btnagregar.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.btnagregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnagregar.Image = SIGEFA.Properties.Resources.agregar;
		this.btnagregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnagregar.Location = new System.Drawing.Point(935, 48);
		this.btnagregar.Name = "btnagregar";
		this.btnagregar.Size = new System.Drawing.Size(101, 34);
		this.btnagregar.TabIndex = 47;
		this.btnagregar.Text = "Agregar";
		this.btnagregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.toolTip1.SetToolTip(this.btnagregar, "Agrega todos los elementos con las casillas seleccionadas");
		this.btnagregar.UseVisualStyleBackColor = false;
		this.btnagregar.Click += new System.EventHandler(btnagregar_Click);
		this.cmbProveedores.FormattingEnabled = true;
		this.cmbProveedores.Location = new System.Drawing.Point(683, 38);
		this.cmbProveedores.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.cmbProveedores.Name = "cmbProveedores";
		this.cmbProveedores.Size = new System.Drawing.Size(221, 23);
		this.cmbProveedores.TabIndex = 45;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(609, 41);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(72, 15);
		this.label11.TabIndex = 46;
		this.label11.Text = "Proveedor";
		this.cmbGrupo.FormattingEnabled = true;
		this.cmbGrupo.Location = new System.Drawing.Point(380, 51);
		this.cmbGrupo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.cmbGrupo.Name = "cmbGrupo";
		this.cmbGrupo.Size = new System.Drawing.Size(221, 23);
		this.cmbGrupo.TabIndex = 42;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(328, 54);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(46, 15);
		this.label2.TabIndex = 44;
		this.label2.Text = "Grupo";
		this.cmbLinea.FormattingEnabled = true;
		this.cmbLinea.Location = new System.Drawing.Point(381, 22);
		this.cmbLinea.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.cmbLinea.Name = "cmbLinea";
		this.cmbLinea.Size = new System.Drawing.Size(220, 23);
		this.cmbLinea.TabIndex = 41;
		this.cmbLinea.SelectedIndexChanged += new System.EventHandler(cmbLinea_SelectedIndexChanged);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(331, 26);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(43, 15);
		this.label8.TabIndex = 43;
		this.label8.Text = "Linea";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(533, 85);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(35, 12);
		this.label6.TabIndex = 40;
		this.label6.Text = "{prod}";
		this.label6.Visible = false;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.Black;
		this.label7.Location = new System.Drawing.Point(515, 85);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 39;
		this.label7.Text = "X";
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.Black;
		this.label10.Location = new System.Drawing.Point(454, 85);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(55, 12);
		this.label10.TabIndex = 38;
		this.label10.Text = "Filtro por:";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(456, 100);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(225, 21);
		this.txtFiltro.TabIndex = 8;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.btnfiltrar.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.btnfiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnfiltrar.Image = SIGEFA.Properties.Resources.buscar;
		this.btnfiltrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnfiltrar.Location = new System.Drawing.Point(935, 8);
		this.btnfiltrar.Name = "btnfiltrar";
		this.btnfiltrar.Size = new System.Drawing.Size(101, 34);
		this.btnfiltrar.TabIndex = 5;
		this.btnfiltrar.Text = "Filtrar";
		this.btnfiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnfiltrar.UseVisualStyleBackColor = false;
		this.btnfiltrar.Click += new System.EventHandler(btnfiltrar_Click);
		this.cmbmarca.FormattingEnabled = true;
		this.cmbmarca.Location = new System.Drawing.Point(82, 22);
		this.cmbmarca.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.cmbmarca.Name = "cmbmarca";
		this.cmbmarca.Size = new System.Drawing.Size(220, 23);
		this.cmbmarca.TabIndex = 1;
		this.btnlimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnlimpiar.Image = (System.Drawing.Image)resources.GetObject("btnlimpiar.Image");
		this.btnlimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnlimpiar.Location = new System.Drawing.Point(1043, 8);
		this.btnlimpiar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnlimpiar.Name = "btnlimpiar";
		this.btnlimpiar.Size = new System.Drawing.Size(101, 34);
		this.btnlimpiar.TabIndex = 6;
		this.btnlimpiar.Tag = "Ejemplo de tag";
		this.btnlimpiar.Text = "Limpiar";
		this.btnlimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnlimpiar.UseVisualStyleBackColor = true;
		this.btnlimpiar.Click += new System.EventHandler(btnlimpiar_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(32, 26);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(47, 15);
		this.label1.TabIndex = 5;
		this.label1.Text = "Marca";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.colChkSelectorDgv1, this.codproducto, this.referencia, this.descripcion, this.codUniMedida, this.unidad, this.marca, this.familia, this.linea, this.modelo, this.Proveedor);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 18);
		this.dgvDetalle.MultiSelect = false;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dgvDetalle.RowsDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1367, 435);
		this.dgvDetalle.TabIndex = 7;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDetalle_CellMouseDoubleClick);
		this.dgvDetalle.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDetalle_ColumnHeaderMouseClick);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.colChkSelectorDgv1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.colChkSelectorDgv1.Frozen = true;
		this.colChkSelectorDgv1.HeaderText = "#";
		this.colChkSelectorDgv1.Name = "colChkSelectorDgv1";
		this.colChkSelectorDgv1.ReadOnly = true;
		this.colChkSelectorDgv1.Width = 25;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.FillWeight = 68.55676f;
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.FillWeight = 165.5112f;
		this.referencia.HeaderText = "Codigo";
		this.referencia.MinimumWidth = 80;
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.FillWeight = 0.3067119f;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.MinimumWidth = 300;
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.codUniMedida.DataPropertyName = "codUniMedida";
		this.codUniMedida.HeaderText = "Cod. Unidad";
		this.codUniMedida.Name = "codUniMedida";
		this.codUniMedida.ReadOnly = true;
		this.codUniMedida.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.FillWeight = 191.3256f;
		this.unidad.HeaderText = "Unidad";
		this.unidad.MinimumWidth = 100;
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.marca.DataPropertyName = "marca";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N4";
		dataGridViewCellStyle5.NullValue = null;
		this.marca.DefaultCellStyle = dataGridViewCellStyle5;
		this.marca.FillWeight = 80.12601f;
		this.marca.HeaderText = "Marca";
		this.marca.MinimumWidth = 100;
		this.marca.Name = "marca";
		this.marca.ReadOnly = true;
		this.marca.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.marca.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.familia.DataPropertyName = "familia";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N4";
		dataGridViewCellStyle6.NullValue = null;
		this.familia.DefaultCellStyle = dataGridViewCellStyle6;
		this.familia.FillWeight = 33.57734f;
		this.familia.HeaderText = "Familia";
		this.familia.MinimumWidth = 100;
		this.familia.Name = "familia";
		this.familia.ReadOnly = true;
		this.familia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.familia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.linea.DataPropertyName = "linea";
		this.linea.FillWeight = 14.09184f;
		this.linea.HeaderText = "Linea";
		this.linea.MinimumWidth = 100;
		this.linea.Name = "linea";
		this.linea.ReadOnly = true;
		this.modelo.DataPropertyName = "grupo";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N4";
		dataGridViewCellStyle7.NullValue = null;
		this.modelo.DefaultCellStyle = dataGridViewCellStyle7;
		this.modelo.FillWeight = 5.951732f;
		this.modelo.HeaderText = "Grupo";
		this.modelo.MinimumWidth = 100;
		this.modelo.Name = "modelo";
		this.modelo.ReadOnly = true;
		this.modelo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.modelo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Proveedor.DataPropertyName = "Proveedor";
		this.Proveedor.FillWeight = 2.527635f;
		this.Proveedor.HeaderText = "Proveedor";
		this.Proveedor.MinimumWidth = 200;
		this.Proveedor.Name = "Proveedor";
		this.Proveedor.ReadOnly = true;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Location = new System.Drawing.Point(6, 169);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1373, 456);
		this.groupBox2.TabIndex = 9;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Productos:";
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(442, 3448);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(68, 15);
		this.label14.TabIndex = 27;
		this.label14.Text = "P. Venta :";
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(255, 3449);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(49, 15);
		this.label13.TabIndex = 25;
		this.label13.Text = "I.G.V. :";
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(35, 3450);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(67, 15);
		this.label12.TabIndex = 23;
		this.label12.Text = "V. Venta :";
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.rgvdetalle1);
		this.groupBox3.Controls.Add(this.dgvdetalle1);
		this.groupBox3.Location = new System.Drawing.Point(6, 188);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1376, 440);
		this.groupBox3.TabIndex = 29;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Plantilla";
		this.rgvdetalle1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvdetalle1.Location = new System.Drawing.Point(3, 18);
		this.rgvdetalle1.MasterTemplate.AllowAddNewRow = false;
		this.rgvdetalle1.MasterTemplate.AllowColumnReorder = false;
		this.rgvdetalle1.MasterTemplate.AutoGenerateColumns = false;
		gridViewCheckBoxColumn1.CheckFilteredRows = false;
		gridViewCheckBoxColumn1.FieldName = "evaluastockminimo";
		gridViewCheckBoxColumn1.HeaderText = "";
		gridViewCheckBoxColumn1.Name = "_colChkEvaluaStockMinimo";
		gridViewCheckBoxColumn1.Width = 25;
		gridViewCheckBoxColumn2.CheckFilteredRows = false;
		gridViewCheckBoxColumn2.FieldName = "llenastockmaximo";
		gridViewCheckBoxColumn2.HeaderText = "";
		gridViewCheckBoxColumn2.Name = "_colChkLlenaStockMaximo";
		gridViewCheckBoxColumn2.Width = 25;
		gridViewTextBoxColumn1.FieldName = "CodigoProducto";
		gridViewTextBoxColumn1.HeaderText = "CodProducto";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "CodigoProducto";
		gridViewTextBoxColumn1.ReadOnly = true;
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.FieldName = "codigoDetallePlantilla";
		gridViewTextBoxColumn2.HeaderText = "Cod Detalle";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodDetallePlantilla";
		gridViewTextBoxColumn2.ReadOnly = true;
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "referencias";
		gridViewTextBoxColumn3.HeaderText = "Codigo";
		gridViewTextBoxColumn3.Name = "referencias";
		gridViewTextBoxColumn3.ReadOnly = true;
		gridViewTextBoxColumn3.Width = 74;
		gridViewTextBoxColumn4.FieldName = "descripcionn";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "descripcionn";
		gridViewTextBoxColumn4.ReadOnly = true;
		gridViewTextBoxColumn4.Width = 301;
		gridViewTextBoxColumn5.FieldName = "codUnidadEquivalente";
		gridViewTextBoxColumn5.HeaderText = "Cod. Unidad Equivalente\t";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodUniEqui";
		gridViewTextBoxColumn5.ReadOnly = true;
		gridViewTextBoxColumn5.Width = 100;
		gridViewTextBoxColumn6.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn6.HeaderText = "Cod. Unidad";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "codUnidadMedida";
		gridViewTextBoxColumn6.ReadOnly = true;
		gridViewTextBoxColumn6.Width = 100;
		gridViewTextBoxColumn7.FieldName = "unidadd";
		gridViewTextBoxColumn7.HeaderText = "Unidad";
		gridViewTextBoxColumn7.Name = "unidadd";
		gridViewTextBoxColumn7.ReadOnly = true;
		gridViewTextBoxColumn7.Width = 100;
		gridViewComboBoxColumn1.FieldName = "colCmbUnidad";
		gridViewComboBoxColumn1.HeaderText = "Unidad";
		gridViewComboBoxColumn1.IsVisible = false;
		gridViewComboBoxColumn1.Name = "colCmbUnidad1";
		gridViewComboBoxColumn1.Width = 100;
		gridViewTextBoxColumn8.FieldName = "marcaa";
		gridViewTextBoxColumn8.HeaderText = "Marca";
		gridViewTextBoxColumn8.Name = "marcaa";
		gridViewTextBoxColumn8.ReadOnly = true;
		gridViewTextBoxColumn8.Width = 73;
		gridViewTextBoxColumn9.FieldName = "familiaa";
		gridViewTextBoxColumn9.HeaderText = "Familia";
		gridViewTextBoxColumn9.Name = "familiaa";
		gridViewTextBoxColumn9.ReadOnly = true;
		gridViewTextBoxColumn9.Width = 74;
		gridViewTextBoxColumn10.FieldName = "lineaa";
		gridViewTextBoxColumn10.HeaderText = "Linea";
		gridViewTextBoxColumn10.Name = "lineaa";
		gridViewTextBoxColumn10.ReadOnly = true;
		gridViewTextBoxColumn10.Width = 73;
		gridViewTextBoxColumn11.FieldName = "grupoo";
		gridViewTextBoxColumn11.HeaderText = "Grupo";
		gridViewTextBoxColumn11.Name = "grupoo";
		gridViewTextBoxColumn11.ReadOnly = true;
		gridViewTextBoxColumn11.Width = 74;
		gridViewTextBoxColumn12.FieldName = "stockminimo";
		gridViewTextBoxColumn12.HeaderText = "Stock Minimo";
		gridViewTextBoxColumn12.Name = "colTxtStockMinimo";
		gridViewTextBoxColumn12.Width = 100;
		gridViewTextBoxColumn13.FieldName = "stockmaximo";
		gridViewTextBoxColumn13.HeaderText = "Stock Maximo";
		gridViewTextBoxColumn13.Name = "colTxtStockMaximo";
		gridViewTextBoxColumn13.Width = 100;
		gridViewTextBoxColumn14.FieldName = "cantidad";
		gridViewTextBoxColumn14.HeaderText = "UndXPaquete";
		gridViewTextBoxColumn14.Name = "colUndXPaquete";
		gridViewTextBoxColumn14.Width = 100;
		gridViewTextBoxColumn15.FieldName = "stock_Actual";
		gridViewTextBoxColumn15.HeaderText = "Stock_actual";
		gridViewTextBoxColumn15.Name = "stock_Actual";
		gridViewTextBoxColumn15.Width = 70;
		gridViewTextBoxColumn16.FieldName = "difStock";
		gridViewTextBoxColumn16.HeaderText = "Diferencia Stock";
		gridViewTextBoxColumn16.Name = "difstockMax_sMaxpol";
		gridViewTextBoxColumn16.Width = 100;
		gridViewTextBoxColumn17.FieldName = "PROMEDIO_FINAL";
		gridViewTextBoxColumn17.HeaderText = "Promedio Final";
		gridViewTextBoxColumn17.Name = "promedio_final";
		gridViewTextBoxColumn17.Width = 90;
		gridViewTextBoxColumn18.FieldName = "CATEGORIZACION";
		gridViewTextBoxColumn18.HeaderText = "Categorizacion";
		gridViewTextBoxColumn18.Name = "tipoitem";
		gridViewTextBoxColumn18.Width = 100;
		gridViewTextBoxColumn19.FieldName = "stockmaxpoliticas";
		gridViewTextBoxColumn19.HeaderText = "Stock Maximo Politicas";
		gridViewTextBoxColumn19.Name = "stockmaxpoliticas";
		gridViewTextBoxColumn19.Width = 100;
		gridViewTextBoxColumn20.FieldName = "situacion";
		gridViewTextBoxColumn20.HeaderText = "Situacion";
		gridViewTextBoxColumn20.Name = "situacion";
		gridViewTextBoxColumn20.Width = 110;
		gridViewTextBoxColumn21.FieldName = "D36_FR";
		gridViewTextBoxColumn21.HeaderText = "D36_FR";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "D36_FR";
		gridViewTextBoxColumn22.FieldName = "G17_FR";
		gridViewTextBoxColumn22.HeaderText = "G17_FR";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "G17_FR";
		gridViewTextBoxColumn23.FieldName = "G17_LM";
		gridViewTextBoxColumn23.HeaderText = "G17_LM";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "G17_LM";
		gridViewTextBoxColumn24.FieldName = "D36_LM";
		gridViewTextBoxColumn24.HeaderText = "D36_LM";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "D36_LM";
		gridViewTextBoxColumn25.FieldName = "PROMEDIO_1_MES";
		gridViewTextBoxColumn25.HeaderText = "PROMEDIO_1_MES";
		gridViewTextBoxColumn25.Name = "PROMEDIO_1_MES";
		gridViewTextBoxColumn25.Width = 100;
		gridViewTextBoxColumn26.FieldName = "D36_FR1";
		gridViewTextBoxColumn26.HeaderText = "D36_FR1";
		gridViewTextBoxColumn26.IsVisible = false;
		gridViewTextBoxColumn26.Name = "D36_FR1";
		gridViewTextBoxColumn27.FieldName = "G17_FR1";
		gridViewTextBoxColumn27.HeaderText = "G17_FR1";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "G17_FR1";
		gridViewTextBoxColumn28.FieldName = "G17_LM1";
		gridViewTextBoxColumn28.HeaderText = "G17_LM1";
		gridViewTextBoxColumn28.IsVisible = false;
		gridViewTextBoxColumn28.Name = "G17_LM1";
		gridViewTextBoxColumn29.FieldName = "D36_LM1";
		gridViewTextBoxColumn29.HeaderText = "D36_LM1";
		gridViewTextBoxColumn29.IsVisible = false;
		gridViewTextBoxColumn29.Name = "D36_LM1";
		gridViewTextBoxColumn30.FieldName = "PROMEDIO_3_MES";
		gridViewTextBoxColumn30.HeaderText = "PROMEDIO_3_MES";
		gridViewTextBoxColumn30.Name = "PROMEDIO_3_MES";
		gridViewTextBoxColumn30.Width = 100;
		this.rgvdetalle1.MasterTemplate.Columns.AddRange(gridViewCheckBoxColumn1, gridViewCheckBoxColumn2, gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewComboBoxColumn1, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30);
		this.rgvdetalle1.MasterTemplate.EnableFiltering = true;
		this.rgvdetalle1.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.rgvdetalle1.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvdetalle1.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvdetalle1.Name = "rgvdetalle1";
		this.rgvdetalle1.ShowGroupPanel = false;
		this.rgvdetalle1.Size = new System.Drawing.Size(1370, 419);
		this.rgvdetalle1.TabIndex = 10;
		this.rgvdetalle1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(rgvdetalle1_CellFormatting);
		this.rgvdetalle1.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(rgvdetalle1_CellBeginEdit);
		this.rgvdetalle1.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle1_CellEndEdit);
		this.rgvdetalle1.CurrentCellChanged += new Telerik.WinControls.UI.CurrentCellChangedEventHandler(rgvdetalle1_CurrentCellChanged);
		this.rgvdetalle1.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(rgvdetalle1_CurrentRowChanged);
		this.rgvdetalle1.RowsChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(rgvdetalle1_RowsChanged);
		this.rgvdetalle1.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle1_CellClick);
		this.rgvdetalle1.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle1_CellValueChanged);
		this.dgvdetalle1.AllowUserToAddRows = false;
		this.dgvdetalle1.AllowUserToDeleteRows = false;
		this.dgvdetalle1.AllowUserToResizeRows = false;
		this.dgvdetalle1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvdetalle1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvdetalle1.Columns.AddRange(this.colChkEvaluaStockMinimo, this.colChkLlenaStockMaximo, this.CodigoProducto, this.colCodDetallePlantilla, this.referencias, this.descripcionn, this.colCodUniEqui, this.codUnidadMedida, this.unidadd, this.colCmbUnidad, this.marcaa, this.familiaa, this.lineaa, this.grupoo, this.colTxtStockMinimo, this.colTxtStockMaximo, this.colUndXPaquete, this.difstockMax_sMaxpol, this.promedio_final, this.tipoitem, this.stockmaxpoliticas, this.situacion, this.D36_FR, this.G17_FR, this.G17_LM, this.D36_LM, this.PROMEDIO_1_MES, this.D36_FR1, this.G17_FR1, this.G17_LM1, this.D36_LM1, this.PROMEDIO_3_MES);
		this.dgvdetalle1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvdetalle1.Location = new System.Drawing.Point(3, 18);
		this.dgvdetalle1.Name = "dgvdetalle1";
		this.dgvdetalle1.RowHeadersVisible = false;
		this.dgvdetalle1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvdetalle1.Size = new System.Drawing.Size(1370, 419);
		this.dgvdetalle1.TabIndex = 9;
		this.dgvdetalle1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvdetalle1_CellBeginEdit);
		this.dgvdetalle1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalle1_CellClick);
		this.dgvdetalle1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalle1_CellEndEdit);
		this.dgvdetalle1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalle1_CellEnter);
		this.dgvdetalle1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dgvdetalle1_CellFormatting);
		this.dgvdetalle1.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalle1_CellLeave);
		this.dgvdetalle1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvdetalle1_CellMouseDoubleClick);
		this.dgvdetalle1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvdetalle1_CellValueChanged);
		this.dgvdetalle1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvdetalle1_ColumnHeaderMouseClick);
		this.dgvdetalle1.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(dgvdetalle1_ColumnWidthChanged);
		this.dgvdetalle1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvdetalle1_EditingControlShowing);
		this.dgvdetalle1.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(dgvdetalle1_RowHeightChanged);
		this.dgvdetalle1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvdetalle1_RowsAdded);
		this.dgvdetalle1.Enter += new System.EventHandler(dgvdetalle1_Enter);
		this.dgvdetalle1.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvdetalle1_KeyUp);
		this.colChkEvaluaStockMinimo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.colChkEvaluaStockMinimo.DataPropertyName = "evaluastockminimo";
		this.colChkEvaluaStockMinimo.Frozen = true;
		this.colChkEvaluaStockMinimo.HeaderText = "#E";
		this.colChkEvaluaStockMinimo.Name = "colChkEvaluaStockMinimo";
		this.colChkEvaluaStockMinimo.ReadOnly = true;
		this.colChkEvaluaStockMinimo.ToolTipText = "Evalua Stock MInimo";
		this.colChkEvaluaStockMinimo.Width = 25;
		this.colChkLlenaStockMaximo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.colChkLlenaStockMaximo.DataPropertyName = "llenastockmaximo";
		this.colChkLlenaStockMaximo.Frozen = true;
		this.colChkLlenaStockMaximo.HeaderText = "#L";
		this.colChkLlenaStockMaximo.Name = "colChkLlenaStockMaximo";
		this.colChkLlenaStockMaximo.ReadOnly = true;
		this.colChkLlenaStockMaximo.ToolTipText = "Llena Stock Maximo";
		this.colChkLlenaStockMaximo.Width = 25;
		this.CodigoProducto.DataPropertyName = "CodigoProducto";
		this.CodigoProducto.FillWeight = 79.25892f;
		this.CodigoProducto.Frozen = true;
		this.CodigoProducto.HeaderText = "CodProducto";
		this.CodigoProducto.Name = "CodigoProducto";
		this.CodigoProducto.ReadOnly = true;
		this.CodigoProducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.CodigoProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.CodigoProducto.Visible = false;
		this.colCodDetallePlantilla.DataPropertyName = "codigoDetallePlantilla";
		this.colCodDetallePlantilla.Frozen = true;
		this.colCodDetallePlantilla.HeaderText = "Cod Detalle";
		this.colCodDetallePlantilla.Name = "colCodDetallePlantilla";
		this.colCodDetallePlantilla.ReadOnly = true;
		this.colCodDetallePlantilla.Visible = false;
		this.referencias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.referencias.DataPropertyName = "referencias";
		this.referencias.FillWeight = 79.25892f;
		this.referencias.Frozen = true;
		this.referencias.HeaderText = "Codigo";
		this.referencias.Name = "referencias";
		this.referencias.ReadOnly = true;
		this.referencias.Width = 74;
		this.descripcionn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.descripcionn.DataPropertyName = "descripcionn";
		this.descripcionn.FillWeight = 323.5058f;
		this.descripcionn.Frozen = true;
		this.descripcionn.HeaderText = "Descripcion";
		this.descripcionn.Name = "descripcionn";
		this.descripcionn.ReadOnly = true;
		this.descripcionn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcionn.Width = 301;
		this.colCodUniEqui.DataPropertyName = "codUnidadEquivalente";
		this.colCodUniEqui.HeaderText = "Cod. Unidad Equivalente";
		this.colCodUniEqui.Name = "colCodUniEqui";
		this.colCodUniEqui.ReadOnly = true;
		this.colCodUniEqui.Visible = false;
		this.codUnidadMedida.DataPropertyName = "codUnidadMedida";
		this.codUnidadMedida.FillWeight = 98.04715f;
		this.codUnidadMedida.HeaderText = "Cod. Unidad";
		this.codUnidadMedida.Name = "codUnidadMedida";
		this.codUnidadMedida.ReadOnly = true;
		this.codUnidadMedida.Visible = false;
		this.unidadd.DataPropertyName = "unidadd";
		this.unidadd.FillWeight = 79.25892f;
		this.unidadd.HeaderText = "Unidad";
		this.unidadd.Name = "unidadd";
		this.unidadd.ReadOnly = true;
		this.unidadd.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidadd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCmbUnidad.HeaderText = "Unidad";
		this.colCmbUnidad.Name = "colCmbUnidad";
		this.colCmbUnidad.ReadOnly = true;
		this.colCmbUnidad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.colCmbUnidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.marcaa.DataPropertyName = "marcaa";
		this.marcaa.HeaderText = "Marca";
		this.marcaa.Name = "marcaa";
		this.marcaa.ReadOnly = true;
		this.familiaa.DataPropertyName = "familiaa";
		this.familiaa.HeaderText = "Familia";
		this.familiaa.Name = "familiaa";
		this.familiaa.ReadOnly = true;
		this.lineaa.DataPropertyName = "lineaa";
		this.lineaa.HeaderText = "Linea";
		this.lineaa.Name = "lineaa";
		this.lineaa.ReadOnly = true;
		this.grupoo.DataPropertyName = "grupoo";
		this.grupoo.HeaderText = "Grupo";
		this.grupoo.Name = "grupoo";
		this.grupoo.ReadOnly = true;
		this.colTxtStockMinimo.DataPropertyName = "stockminimo";
		this.colTxtStockMinimo.HeaderText = "Stock Minimo";
		this.colTxtStockMinimo.Name = "colTxtStockMinimo";
		this.colTxtStockMinimo.ReadOnly = true;
		this.colTxtStockMaximo.DataPropertyName = "stockmaximo";
		this.colTxtStockMaximo.HeaderText = "Stock Maximo";
		this.colTxtStockMaximo.Name = "colTxtStockMaximo";
		this.colTxtStockMaximo.ReadOnly = true;
		this.colUndXPaquete.DataPropertyName = "cantidad";
		this.colUndXPaquete.HeaderText = "UndXPaquete";
		this.colUndXPaquete.Name = "colUndXPaquete";
		this.colUndXPaquete.ReadOnly = true;
		this.difstockMax_sMaxpol.DataPropertyName = "difStock";
		this.difstockMax_sMaxpol.HeaderText = "Diferencia Stock";
		this.difstockMax_sMaxpol.Name = "difstockMax_sMaxpol";
		this.difstockMax_sMaxpol.ReadOnly = true;
		this.promedio_final.DataPropertyName = "PROMEDIO_FINAL";
		this.promedio_final.HeaderText = "Promedio";
		this.promedio_final.Name = "promedio_final";
		this.promedio_final.ReadOnly = true;
		this.tipoitem.DataPropertyName = "CATEGORIZACION";
		this.tipoitem.HeaderText = "Categorizacion";
		this.tipoitem.Name = "tipoitem";
		this.tipoitem.ReadOnly = true;
		this.stockmaxpoliticas.DataPropertyName = "stockmaxpoliticas";
		this.stockmaxpoliticas.HeaderText = "Stock Maximo Politicas";
		this.stockmaxpoliticas.Name = "stockmaxpoliticas";
		this.stockmaxpoliticas.ReadOnly = true;
		this.situacion.DataPropertyName = "situacion";
		this.situacion.HeaderText = "Situacion";
		this.situacion.Name = "situacion";
		this.situacion.ReadOnly = true;
		this.D36_FR.DataPropertyName = "D36_FR";
		this.D36_FR.HeaderText = "D36_FR";
		this.D36_FR.Name = "D36_FR";
		this.D36_FR.ReadOnly = true;
		this.D36_FR.Visible = false;
		this.G17_FR.DataPropertyName = "G17_FR";
		this.G17_FR.HeaderText = "G17_FR";
		this.G17_FR.Name = "G17_FR";
		this.G17_FR.ReadOnly = true;
		this.G17_FR.Visible = false;
		this.G17_LM.DataPropertyName = "G17_LM";
		this.G17_LM.HeaderText = "G17_LM";
		this.G17_LM.Name = "G17_LM";
		this.G17_LM.ReadOnly = true;
		this.G17_LM.Visible = false;
		this.D36_LM.DataPropertyName = "D36_LM";
		this.D36_LM.HeaderText = "D36_LM";
		this.D36_LM.Name = "D36_LM";
		this.D36_LM.ReadOnly = true;
		this.D36_LM.Visible = false;
		this.PROMEDIO_1_MES.DataPropertyName = "PROMEDIO_1_MES";
		this.PROMEDIO_1_MES.HeaderText = "PROMEDIO_1_MES";
		this.PROMEDIO_1_MES.Name = "PROMEDIO_1_MES";
		this.PROMEDIO_1_MES.ReadOnly = true;
		this.PROMEDIO_1_MES.Visible = false;
		this.D36_FR1.DataPropertyName = "D36_FR1";
		this.D36_FR1.HeaderText = "D36_FR1";
		this.D36_FR1.Name = "D36_FR1";
		this.D36_FR1.ReadOnly = true;
		this.D36_FR1.Visible = false;
		this.G17_FR1.DataPropertyName = "G17_FR1";
		this.G17_FR1.HeaderText = "G17_FR1";
		this.G17_FR1.Name = "G17_FR1";
		this.G17_FR1.ReadOnly = true;
		this.G17_FR1.Visible = false;
		this.G17_LM1.DataPropertyName = "G17_LM1";
		this.G17_LM1.HeaderText = "G17_LM1";
		this.G17_LM1.Name = "G17_LM1";
		this.G17_LM1.ReadOnly = true;
		this.G17_LM1.Visible = false;
		this.D36_LM1.DataPropertyName = "D36_LM1";
		this.D36_LM1.HeaderText = "D36_LM1";
		this.D36_LM1.Name = "D36_LM1";
		this.D36_LM1.ReadOnly = true;
		this.D36_LM1.Visible = false;
		this.PROMEDIO_3_MES.DataPropertyName = "PROMEDIO_3_MES";
		this.PROMEDIO_3_MES.HeaderText = "PROMEDIO_3_MES";
		this.PROMEDIO_3_MES.Name = "PROMEDIO_3_MES";
		this.PROMEDIO_3_MES.ReadOnly = true;
		this.PROMEDIO_3_MES.Visible = false;
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.Controls.Add(this.btnactualizacategorizacion);
		this.groupBox5.Controls.Add(this.btnExcel);
		this.groupBox5.Controls.Add(this.btnRecargarPlantilla);
		this.groupBox5.Controls.Add(this.btnVerificadorGenerarPropuesta);
		this.groupBox5.Controls.Add(this.rbBusquedaOptima1);
		this.groupBox5.Controls.Add(this.rbBusquedaExacta1);
		this.groupBox5.Controls.Add(this.lbltotalproductos);
		this.groupBox5.Controls.Add(this.label19);
		this.groupBox5.Controls.Add(this.cmbTipoPlantilla);
		this.groupBox5.Controls.Add(this.label15);
		this.groupBox5.Controls.Add(this.dtfecha);
		this.groupBox5.Controls.Add(this.dateTimePicker1);
		this.groupBox5.Controls.Add(this.txtnombreplantilla);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.txtdescripcion);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.lblCantidadFiltradaDos);
		this.groupBox5.Controls.Add(this.label18);
		this.groupBox5.Controls.Add(this.pbCargando);
		this.groupBox5.Controls.Add(this.lblModoPlantilla);
		this.groupBox5.Controls.Add(this.btnSalir);
		this.groupBox5.Controls.Add(this.btnGenerar);
		this.groupBox5.Controls.Add(this.label62);
		this.groupBox5.Controls.Add(this.label72);
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.txtFiltro2);
		this.groupBox5.Controls.Add(this.button2);
		this.groupBox5.Location = new System.Drawing.Point(6, 12);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(1376, 170);
		this.groupBox5.TabIndex = 30;
		this.groupBox5.TabStop = false;
		this.btnactualizacategorizacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnactualizacategorizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnactualizacategorizacion.Image = (System.Drawing.Image)resources.GetObject("btnactualizacategorizacion.Image");
		this.btnactualizacategorizacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnactualizacategorizacion.Location = new System.Drawing.Point(399, 118);
		this.btnactualizacategorizacion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnactualizacategorizacion.Name = "btnactualizacategorizacion";
		this.btnactualizacategorizacion.Size = new System.Drawing.Size(125, 46);
		this.btnactualizacategorizacion.TabIndex = 73;
		this.btnactualizacategorizacion.Text = "Actualiza Categorización";
		this.btnactualizacategorizacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnactualizacategorizacion.UseVisualStyleBackColor = true;
		this.btnactualizacategorizacion.Click += new System.EventHandler(btnactualizacategorizacion_Click);
		this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExcel.Location = new System.Drawing.Point(309, 118);
		this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnExcel.Name = "btnExcel";
		this.btnExcel.Size = new System.Drawing.Size(82, 46);
		this.btnExcel.TabIndex = 72;
		this.btnExcel.Text = "Excel";
		this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExcel.UseVisualStyleBackColor = true;
		this.btnExcel.Click += new System.EventHandler(btnExcel_Click);
		this.btnRecargarPlantilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRecargarPlantilla.Image = SIGEFA.Properties.Resources.sync;
		this.btnRecargarPlantilla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRecargarPlantilla.Location = new System.Drawing.Point(156, 118);
		this.btnRecargarPlantilla.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnRecargarPlantilla.Name = "btnRecargarPlantilla";
		this.btnRecargarPlantilla.Size = new System.Drawing.Size(145, 46);
		this.btnRecargarPlantilla.TabIndex = 71;
		this.btnRecargarPlantilla.Text = "Recargar Plantilla";
		this.btnRecargarPlantilla.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRecargarPlantilla.UseVisualStyleBackColor = true;
		this.btnRecargarPlantilla.Click += new System.EventHandler(btnRecargarPlantilla_Click);
		this.btnVerificadorGenerarPropuesta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnVerificadorGenerarPropuesta.Image = SIGEFA.Properties.Resources.acep;
		this.btnVerificadorGenerarPropuesta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVerificadorGenerarPropuesta.Location = new System.Drawing.Point(7, 118);
		this.btnVerificadorGenerarPropuesta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnVerificadorGenerarPropuesta.Name = "btnVerificadorGenerarPropuesta";
		this.btnVerificadorGenerarPropuesta.Size = new System.Drawing.Size(141, 46);
		this.btnVerificadorGenerarPropuesta.TabIndex = 70;
		this.btnVerificadorGenerarPropuesta.Text = "Verificador para Generar Propuesta";
		this.btnVerificadorGenerarPropuesta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVerificadorGenerarPropuesta.UseVisualStyleBackColor = true;
		this.btnVerificadorGenerarPropuesta.Click += new System.EventHandler(btnVerificadorGenerarPropuesta_Click);
		this.rbBusquedaOptima1.AutoSize = true;
		this.rbBusquedaOptima1.Checked = true;
		this.rbBusquedaOptima1.Location = new System.Drawing.Point(6, 76);
		this.rbBusquedaOptima1.Name = "rbBusquedaOptima1";
		this.rbBusquedaOptima1.Size = new System.Drawing.Size(117, 17);
		this.rbBusquedaOptima1.TabIndex = 69;
		this.rbBusquedaOptima1.TabStop = true;
		this.rbBusquedaOptima1.Text = "Busqueda Optima";
		this.rbBusquedaOptima1.UseVisualStyleBackColor = true;
		this.rbBusquedaExacta1.AutoSize = true;
		this.rbBusquedaExacta1.Location = new System.Drawing.Point(6, 53);
		this.rbBusquedaExacta1.Name = "rbBusquedaExacta1";
		this.rbBusquedaExacta1.Size = new System.Drawing.Size(111, 17);
		this.rbBusquedaExacta1.TabIndex = 69;
		this.rbBusquedaExacta1.Text = "Busqueda Exacta";
		this.rbBusquedaExacta1.UseVisualStyleBackColor = true;
		this.lbltotalproductos.AutoSize = true;
		this.lbltotalproductos.BackColor = System.Drawing.Color.Transparent;
		this.lbltotalproductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbltotalproductos.ForeColor = System.Drawing.Color.Black;
		this.lbltotalproductos.Location = new System.Drawing.Point(439, 63);
		this.lbltotalproductos.Name = "lbltotalproductos";
		this.lbltotalproductos.Size = new System.Drawing.Size(11, 12);
		this.lbltotalproductos.TabIndex = 63;
		this.lbltotalproductos.Text = "0";
		this.label19.AutoSize = true;
		this.label19.BackColor = System.Drawing.Color.Transparent;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.ForeColor = System.Drawing.Color.Black;
		this.label19.Location = new System.Drawing.Point(342, 63);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(91, 12);
		this.label19.TabIndex = 62;
		this.label19.Text = "Cant. Productos:";
		this.cmbTipoPlantilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipoPlantilla.FormattingEnabled = true;
		this.cmbTipoPlantilla.Location = new System.Drawing.Point(621, 41);
		this.cmbTipoPlantilla.Name = "cmbTipoPlantilla";
		this.cmbTipoPlantilla.Size = new System.Drawing.Size(195, 23);
		this.cmbTipoPlantilla.TabIndex = 61;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(568, 44);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(36, 13);
		this.label15.TabIndex = 60;
		this.label15.Text = "Tipo :";
		this.label15.Click += new System.EventHandler(label15_Click);
		this.dtfecha.AutoSize = true;
		this.dtfecha.Location = new System.Drawing.Point(561, 18);
		this.dtfecha.Name = "dtfecha";
		this.dtfecha.Size = new System.Drawing.Size(43, 13);
		this.dtfecha.TabIndex = 59;
		this.dtfecha.Text = "Fecha :";
		this.dateTimePicker1.Enabled = false;
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dateTimePicker1.Location = new System.Drawing.Point(621, 14);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(116, 21);
		this.dateTimePicker1.TabIndex = 55;
		this.txtnombreplantilla.Location = new System.Drawing.Point(621, 71);
		this.txtnombreplantilla.Name = "txtnombreplantilla";
		this.txtnombreplantilla.Size = new System.Drawing.Size(375, 21);
		this.txtnombreplantilla.TabIndex = 56;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(561, 74);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(43, 13);
		this.label5.TabIndex = 57;
		this.label5.Text = "Titulo :";
		this.txtdescripcion.Location = new System.Drawing.Point(621, 102);
		this.txtdescripcion.Multiline = true;
		this.txtdescripcion.Name = "txtdescripcion";
		this.txtdescripcion.Size = new System.Drawing.Size(375, 62);
		this.txtdescripcion.TabIndex = 58;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(531, 105);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(73, 13);
		this.label4.TabIndex = 54;
		this.label4.Text = "Descripcion :";
		this.lblCantidadFiltradaDos.AutoSize = true;
		this.lblCantidadFiltradaDos.BackColor = System.Drawing.Color.Transparent;
		this.lblCantidadFiltradaDos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadFiltradaDos.ForeColor = System.Drawing.Color.Black;
		this.lblCantidadFiltradaDos.Location = new System.Drawing.Point(307, 82);
		this.lblCantidadFiltradaDos.Name = "lblCantidadFiltradaDos";
		this.lblCantidadFiltradaDos.Size = new System.Drawing.Size(11, 12);
		this.lblCantidadFiltradaDos.TabIndex = 53;
		this.lblCantidadFiltradaDos.Text = "0";
		this.label18.AutoSize = true;
		this.label18.BackColor = System.Drawing.Color.Transparent;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.ForeColor = System.Drawing.Color.Black;
		this.label18.Location = new System.Drawing.Point(130, 82);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(171, 12);
		this.label18.TabIndex = 52;
		this.label18.Text = "Cantidad de Productos Filtrados:";
		this.pbCargando.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.pbCargando.Image = SIGEFA.Properties.Resources.cargando_1_pequeño;
		this.pbCargando.Location = new System.Drawing.Point(1156, 78);
		this.pbCargando.Name = "pbCargando";
		this.pbCargando.Size = new System.Drawing.Size(46, 46);
		this.pbCargando.TabIndex = 48;
		this.pbCargando.TabStop = false;
		this.pbCargando.Visible = false;
		this.lblModoPlantilla.AutoSize = true;
		this.lblModoPlantilla.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.lblModoPlantilla.ForeColor = System.Drawing.Color.Red;
		this.lblModoPlantilla.Location = new System.Drawing.Point(387, 26);
		this.lblModoPlantilla.Name = "lblModoPlantilla";
		this.lblModoPlantilla.Size = new System.Drawing.Size(106, 13);
		this.lblModoPlantilla.TabIndex = 47;
		this.lblModoPlantilla.Text = "NUEVA PLANTILLA";
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.Location = new System.Drawing.Point(1127, 26);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(88, 46);
		this.btnSalir.TabIndex = 46;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click_1);
		this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerar.Image = (System.Drawing.Image)resources.GetObject("btnGenerar.Image");
		this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerar.Location = new System.Drawing.Point(1055, 78);
		this.btnGenerar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnGenerar.Name = "btnGenerar";
		this.btnGenerar.Size = new System.Drawing.Size(160, 46);
		this.btnGenerar.TabIndex = 45;
		this.btnGenerar.Text = "Generar Propuesta";
		this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerar.UseVisualStyleBackColor = true;
		this.btnGenerar.Click += new System.EventHandler(btnGenerar_Click);
		this.label62.AutoSize = true;
		this.label62.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label62.ForeColor = System.Drawing.Color.SteelBlue;
		this.label62.Location = new System.Drawing.Point(209, 43);
		this.label62.Name = "label62";
		this.label62.Size = new System.Drawing.Size(35, 12);
		this.label62.TabIndex = 44;
		this.label62.Text = "{prod}";
		this.label62.Visible = false;
		this.label72.AutoSize = true;
		this.label72.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label72.ForeColor = System.Drawing.Color.Black;
		this.label72.Location = new System.Drawing.Point(191, 43);
		this.label72.Name = "label72";
		this.label72.Size = new System.Drawing.Size(12, 12);
		this.label72.TabIndex = 43;
		this.label72.Text = "X";
		this.label9.AutoSize = true;
		this.label9.BackColor = System.Drawing.Color.Transparent;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.Black;
		this.label9.Location = new System.Drawing.Point(130, 43);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(55, 12);
		this.label9.TabIndex = 42;
		this.label9.Text = "Filtro por:";
		this.txtFiltro2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro2.Location = new System.Drawing.Point(132, 58);
		this.txtFiltro2.Name = "txtFiltro2";
		this.txtFiltro2.Size = new System.Drawing.Size(203, 21);
		this.txtFiltro2.TabIndex = 41;
		this.txtFiltro2.TextChanged += new System.EventHandler(txtFiltro2_TextChanged);
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Image = (System.Drawing.Image)resources.GetObject("button2.Image");
		this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button2.Location = new System.Drawing.Point(1018, 26);
		this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(101, 46);
		this.button2.TabIndex = 13;
		this.button2.Text = "Guardar";
		this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.rpvContenedorPestañas.Controls.Add(this.rpvListadoFiltro);
		this.rpvContenedorPestañas.Controls.Add(this.rpvListadoPlantilla);
		this.rpvContenedorPestañas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rpvContenedorPestañas.Location = new System.Drawing.Point(0, 0);
		this.rpvContenedorPestañas.Name = "rpvContenedorPestañas";
		this.rpvContenedorPestañas.SelectedPage = this.rpvListadoPlantilla;
		this.rpvContenedorPestañas.Size = new System.Drawing.Size(1403, 676);
		this.rpvContenedorPestañas.TabIndex = 52;
		this.rpvContenedorPestañas.SelectedPageChanged += new System.EventHandler(rpvContenedorPestañas_SelectedPageChanged);
		((Telerik.WinControls.UI.RadPageViewStripElement)this.rpvContenedorPestañas.GetChildAt(0)).StripButtons = Telerik.WinControls.UI.StripViewButtons.Scroll;
		((Telerik.WinControls.UI.RadPageViewStripElement)this.rpvContenedorPestañas.GetChildAt(0)).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Center;
		((Telerik.WinControls.UI.RadPageViewStripElement)this.rpvContenedorPestañas.GetChildAt(0)).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.Fill;
		this.rpvListadoFiltro.AutoScroll = true;
		this.rpvListadoFiltro.Controls.Add(this.gbListadoFiltro);
		this.rpvListadoFiltro.ItemSize = new System.Drawing.SizeF(669f, 28f);
		this.rpvListadoFiltro.Location = new System.Drawing.Point(10, 37);
		this.rpvListadoFiltro.Name = "rpvListadoFiltro";
		this.rpvListadoFiltro.Size = new System.Drawing.Size(1382, 628);
		this.rpvListadoFiltro.Text = "Listado de Filtro";
		this.gbListadoFiltro.Controls.Add(this.gbPlantilla);
		this.gbListadoFiltro.Controls.Add(this.groupBox2);
		this.gbListadoFiltro.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gbListadoFiltro.Location = new System.Drawing.Point(0, 0);
		this.gbListadoFiltro.Name = "gbListadoFiltro";
		this.gbListadoFiltro.Size = new System.Drawing.Size(1382, 628);
		this.gbListadoFiltro.TabIndex = 0;
		this.gbListadoFiltro.TabStop = false;
		this.gbListadoFiltro.Enter += new System.EventHandler(gbListadoFiltro_Enter);
		this.rpvListadoPlantilla.AutoScroll = true;
		this.rpvListadoPlantilla.Controls.Add(this.gbListadoPlantilla);
		this.rpvListadoPlantilla.ItemSize = new System.Drawing.SizeF(683f, 28f);
		this.rpvListadoPlantilla.Location = new System.Drawing.Point(10, 37);
		this.rpvListadoPlantilla.Name = "rpvListadoPlantilla";
		this.rpvListadoPlantilla.Size = new System.Drawing.Size(1382, 628);
		this.rpvListadoPlantilla.Text = "Listado de Plantilla";
		this.gbListadoPlantilla.Controls.Add(this.groupBox5);
		this.gbListadoPlantilla.Controls.Add(this.groupBox3);
		this.gbListadoPlantilla.Dock = System.Windows.Forms.DockStyle.Fill;
		this.gbListadoPlantilla.Location = new System.Drawing.Point(0, 0);
		this.gbListadoPlantilla.Name = "gbListadoPlantilla";
		this.gbListadoPlantilla.Size = new System.Drawing.Size(1382, 628);
		this.gbListadoPlantilla.TabIndex = 0;
		this.gbListadoPlantilla.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
		base.ClientSize = new System.Drawing.Size(1403, 676);
		base.Controls.Add(this.rpvContenedorPestañas);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label12);
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		base.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		base.Name = "ListadoProductos";
		this.Text = "Nueva Plantilla de Productos";
		base.Load += new System.EventHandler(ListadoProductos_Load);
		base.Shown += new System.EventHandler(ListadoProductos_Shown);
		this.gbPlantilla.ResumeLayout(false);
		this.gbPlantilla.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle1.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvdetalle1).EndInit();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbCargando).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rpvContenedorPestañas).EndInit();
		this.rpvContenedorPestañas.ResumeLayout(false);
		this.rpvListadoFiltro.ResumeLayout(false);
		this.gbListadoFiltro.ResumeLayout(false);
		this.rpvListadoPlantilla.ResumeLayout(false);
		this.gbListadoPlantilla.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
