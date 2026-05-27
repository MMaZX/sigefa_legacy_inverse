using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SpreadsheetLight;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmCatalogo : Office2007Form
{
	private clsReporteGananciaxArticulo rptart = new clsReporteGananciaxArticulo();

	private clsReportProductos ds = new clsReportProductos();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmComposicionQuimica admCompQuim = new clsAdmComposicionQuimica();

	private clsAdmDosis admDos = new clsAdmDosis();

	private clsProducto pro = new clsProducto();

	private clsAdmGrupo admgrupo = new clsAdmGrupo();

	private clsAdmLinea admlinea = new clsAdmLinea();

	private clsAdmFamilia admfam = new clsAdmFamilia();

	private clsAdmMarca admmarca = new clsAdmMarca();

	private clsAdmAlmacen admalmacen = new clsAdmAlmacen();

	private clsAdmUnidad unidad = new clsAdmUnidad();

	private clsAdmTipoPrecio tipPrecio = new clsAdmTipoPrecio();

	private List<List<clsUnidadEquivalente>> listaundventa = new List<List<clsUnidadEquivalente>>();

	private List<List<clsUnidadEquivalente>> listaundcompra = new List<List<clsUnidadEquivalente>>();

	public static BindingSource data = new BindingSource();

	public static BindingSource dataComp = new BindingSource();

	public static BindingSource dataDisis = new BindingSource();

	private string filtro = string.Empty;

	private TreeNode nodoselect = new TreeNode();

	private DataTable Arbol = new DataTable();

	public double tc_hoy = 0.0;

	private int rowindex = -1;

	private DataTable productostable = new DataTable();

	private DataTable unidadesTable = new DataTable();

	private int ultima_fila_seleccionada = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem buttonItem16;

	private ButtonItem buttonItem6;

	private ButtonItem buttonItem8;

	private ButtonItem buttonItem3;

	private ButtonItem buttonItem4;

	private ButtonItem buttonItem5;

	private ButtonItem buttonItem9;

	private GroupBox groupBox1;

	private ExpandablePanel expandablePanel1;

	private Label label4;

	private Label label6;

	private Label label7;

	private TextBox txtFiltro;

	private Button btnSalir;

	private ButtonItem biCatalogo;

	private ButtonItem buttonItem2;

	private ButtonItem buttonItem1;

	private MaterialTheme materialTheme1;

	private ButtonItem btnEditar;

	private RadLabel lblEdicion;

	private System.Windows.Forms.TabControl tabControl;

	private TabPage tpDetalleProducto;

	private Label label8;

	private TextBox txtPrecioCatalogoSoles;

	private TextBox txtPrecioCatalogo;

	private TextBox txtNombre;

	private TextBox txtReferencia;

	private TextBox txtCodProducto;

	private Label label39;

	private Label label3;

	private Label label2;

	private Label label1;

	private TabPage tpPrecios;

	private DataGridView dataGridView1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn codigoundMed;

	private DataGridViewTextBoxColumn unidad1;

	private DataGridViewTextBoxColumn Factor1;

	private DataGridViewTextBoxColumn codUndEqui;

	private DataGridViewTextBoxColumn equivalente;

	private DataGridViewTextBoxColumn precios1;

	private DataGridViewTextBoxColumn codTipo;

	private DataGridViewTextBoxColumn tipo;

	private TabPage tpStockAlmacenes;

	private DataGridView dgvAlmacenes;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn entregar;

	private DataGridViewTextBoxColumn disponible;

	private DataGridViewTextBoxColumn recibir;

	private DataGridViewTextBoxColumn futuro;

	private DataGridViewTextBoxColumn minimo;

	private DataGridViewTextBoxColumn maximo;

	private DataGridViewTextBoxColumn reposicion;

	private TabPage tpProveedores;

	public DataGridView dgvProxProducto;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn precio;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn pneto;

	private TabPage tabPage3;

	private DataGridView dgvNotas;

	private DataGridViewTextBoxColumn codnotaproducto;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn nota;

	private TabPage tabEdicion;

	private RadButton btnTexto;

	private RadButton btnCombo;

	private RadLabel lblTexto;

	private RadLabel lblCombo;

	private RadTextBox txtValor;

	private RadDropDownList cmbValor;

	private ButtonItem btnItemActualizaProdAsoc3;

	private ButtonItem btnItemActualizaProdAsoc;

	private ButtonItem btnexExcel;

	private RadGridView dgvprods;

	private ButtonItem btnactualizarproductos;

	private ButtonItem btnactualizarcategorizacion;

	private bool Editar { get; set; }

	private string NomCol { get; set; }

	private List<int> Ids { get; set; }

	private int codUnidadVentaEditar { get; set; }

	private int codUnidadCompraEditar { get; set; }

	private decimal precioVentaEditar { get; set; }

	private decimal precioCompraEditar { get; set; }

	public frmCatalogo()
	{
		InitializeComponent();
	}

	private void frmProductos_Load(object sender, EventArgs e)
	{
		tabEdicion.Hide();
		Editar = false;
		NomCol = "";
		label7.Text = "Código";
		label6.Text = "referencia";
		Ids = new List<int>();
		creaFilas(productostable);
		creaFilasUnidades(unidadesTable);
		CargaLista();
		if (frmLogin.iNivelUser == 5 || frmLogin.iNivelUser == 1)
		{
			btnEditar.Visible = true;
			btnEditar.Enabled = true;
			buttonItem8.Visible = true;
			buttonItem8.Enabled = true;
		}
		else
		{
			btnEditar.Visible = false;
			btnEditar.Enabled = false;
			buttonItem8.Visible = false;
			buttonItem8.Enabled = false;
		}
	}

	public void creaFilas(DataTable tabla)
	{
		tabla.Columns.Add(new DataColumn("codlin", typeof(int)));
		tabla.Columns.Add(new DataColumn("codfam", typeof(int)));
		tabla.Columns.Add(new DataColumn("codmar", typeof(int)));
		tabla.Columns.Add(new DataColumn("precioca", typeof(decimal)));
		tabla.Columns.Add(new DataColumn("preciocompra", typeof(decimal)));
		tabla.Columns.Add(new DataColumn("codgru", typeof(int)));
		tabla.Columns.Add(new DataColumn("codpro", typeof(int)));
	}

	public void creaFilasUnidades(DataTable tabla)
	{
		tabla.Columns.Add(new DataColumn("cod", typeof(int)));
		tabla.Columns.Add(new DataColumn("p", typeof(decimal)));
	}

	public void ajustesGrid()
	{
		dgvprods.Columns["codproducto"].VisibleInColumnChooser = false;
		dgvprods.Columns["coduniversal"].VisibleInColumnChooser = false;
		dgvprods.Columns["ubicacion"].VisibleInColumnChooser = false;
		dgvprods.Columns["control"].VisibleInColumnChooser = false;
		dgvprods.Columns["comision"].VisibleInColumnChooser = false;
		dgvprods.Columns["codmarca"].VisibleInColumnChooser = false;
		dgvprods.Columns["codfamilia"].VisibleInColumnChooser = false;
		dgvprods.Columns["codlinea"].VisibleInColumnChooser = false;
		dgvprods.Columns["codmodelo"].VisibleInColumnChooser = false;
		dgvprods.Columns["preciocatalogo"].VisibleInColumnChooser = false;
		dgvprods.Columns["referencia"].AllowHide = false;
		dgvprods.Columns["descripcion"].AllowHide = false;
		dgvprods.BestFitColumns();
	}

	private void buttonItem16_Click(object sender, EventArgs e)
	{
		try
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 1;
			frm.ShowDialog();
			CargaLista();
		}
		catch (Exception)
		{
		}
	}

	private void frmProductos_Shown(object sender, EventArgs e)
	{
	}

	private void CargaLista()
	{
		dgvprods.DataSource = data;
		data.DataSource = AdmPro.CatalogoProductos();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvprods.ClearSelection();
	}

	private void ConsultaArbol()
	{
		Arbol = AdmPro.ArbolProductos();
	}

	private void buttonItem6_Click(object sender, EventArgs e)
	{
		if (dgvprods.CurrentRow != null && dgvprods.CurrentRow.Index != -1)
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 2;
			frm.pro = pro;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void CargaListaNotas()
	{
		dgvNotas.DataSource = AdmPro.MuestraNotas(pro.CodProducto);
		dgvNotas.ClearSelection();
	}

	private void CargaListaEquivalencias()
	{
		dataGridView1.DataSource = AdmPro.MuestraUnidadesEquivalentesVenta1(pro.CodProducto, frmLogin.iCodAlmacen);
		dataGridView1.ClearSelection();
	}

	private void CargaStockProducto()
	{
		dgvAlmacenes.DataSource = AdmPro.StockProductoAlmacenes(frmLogin.iCodEmpresa, pro.CodProducto);
	}

	private void CargaProductosProveedor()
	{
		dgvProxProducto.DataSource = AdmPro.MuestraProductosProveedor(pro.CodProducto, pro.CodAlmacen);
	}

	private void buttonItem9_Click(object sender, EventArgs e)
	{
		try
		{
			GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(dgvprods);
			spreadStreamExport.ExportVisualSettings = false;
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\exportCatalogo.xlsx";
			if (File.Exists(ruta))
			{
				File.Delete(ruta);
			}
			spreadStreamExport.RunExport(AppDomain.CurrentDomain.BaseDirectory + "\\exportCatalogo.xlsx", new SpreadStreamExportRenderer());
			FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\exportCatalogo.xlsx");
			if (fi.Exists)
			{
				Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\exportCatalogo.xlsx");
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Hubo un error al guardar...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void buttonItem5_Click(object sender, EventArgs e)
	{
		if (!expandablePanel1.Expanded)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
		else
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void frmProductos_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B && e.Control)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
	}

	public void buttonItem4_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void buttonItem3_Click(object sender, EventArgs e)
	{
		if (dgvprods.SelectedRows.Count > 0)
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 3;
			frm.pro = pro;
			frm.ShowDialog();
		}
	}

	private void buttonItem8_Click(object sender, EventArgs e)
	{
		if (dgvprods.CurrentRow == null || dgvprods.CurrentRow.Index == -1)
		{
			return;
		}
		if (Convert.ToInt32(dgvprods.CurrentRow.Cells["estado"].Value) == 1)
		{
			if (!AdmPro.ValidaStockProducto(pro.CodProducto))
			{
				if (AdmPro.delete(pro.CodProducto))
				{
					MessageBox.Show("El producto se dio de baja correctamente");
				}
				else
				{
					MessageBox.Show("Hubo un error al dar de baja");
				}
				return;
			}
			DataTable tab = AdmPro.MuestraStockAlmacenes(pro.CodProducto);
			string mensaje = "El producto presenta stock en: \n";
			foreach (DataRow dr in tab.Rows)
			{
				mensaje = mensaje + "Almacen: " + dr[0]?.ToString() + " - Cantidad: " + dr[1]?.ToString() + " - Unidad: " + dr[2]?.ToString() + "\n";
			}
			MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		else
		{
			MessageBox.Show("El producto ya esta dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (!Editar && dgvprods.SelectedRows.Count > 0)
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 3;
			frm.pro = pro;
			frm.ShowDialog();
		}
	}

	private void biCatalogo_Click(object sender, EventArgs e)
	{
		CRCatalogoPrecios rpt = new CRCatalogoPrecios();
		frmCatalogoRP frm = new frmCatalogoRP();
		rpt.SetDataSource(ds.CatalogoConPrecio().Tables[0]);
		frm.cRVProductos.ReportSource = rpt;
		frm.Show();
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		if (dgvprods.SelectedRows.Count > 0)
		{
			frmCompQuimicaDosis frm = new frmCompQuimicaDosis();
			frm.codPro = pro.CodProducto;
			frm.ShowDialog();
			CargaLista();
		}
		else
		{
			MessageBox.Show("Seleccione un Producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void buttonItem1_Click(object sender, EventArgs e)
	{
		CRGananciaxArticuloCatalogo rpt = new CRGananciaxArticuloCatalogo();
		frmRptGananciaxArticulo frm = new frmRptGananciaxArticulo();
		DataTable dt = rptart.ReporteGananciaCatalogo().Tables[0];
		if (dt.Rows.Count > 0)
		{
			rpt.SetDataSource(dt);
			frm.crvRptGananciaxArticulo.ReportSource = rpt;
			frm.Show();
		}
		else
		{
			MessageBox.Show("No se encontraron resultados con los parámetros seleccionados", "Reporte de Ganancia por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		buscar();
	}

	private void buscar()
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			dgvprods.AutoGenerateColumns = false;
			if (txtFiltro.Text.Length >= 3)
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
								queries.Add($"Convert([{label6.Text.Trim()}], System.String) like '*{c}*'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"Convert([{label6.Text.Trim()}], System.String) like '*{c}*'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"Convert([{label6.Text.Trim()}], System.String) like '*{filterCod}*'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
			Cursor = Cursors.Default;
		}
	}

	private void dgvprods_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.Row == dgvprods.MasterView.TableHeaderRow && e.ColumnIndex > -1)
		{
			label6.Text = e.Column.Name;
			label7.Text = e.Column.Name;
		}
		else
		{
			if (dgvprods.Rows.Count <= 0 || e.RowIndex == -1)
			{
				return;
			}
			if (!Editar)
			{
				rowindex = e.RowIndex;
				pro.CodProducto = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
				GridViewRowInfo Row = dgvprods.Rows[e.RowIndex];
				txtCodProducto.Text = Row.Cells["codproducto"].Value.ToString();
				txtReferencia.Text = Row.Cells["referencia"].Value.ToString();
				txtNombre.Text = Row.Cells["descripcion"].Value.ToString();
				txtPrecioCatalogo.Text = (Convert.ToDouble(Row.Cells["preciocatalogo"].Value) / tc_hoy).ToString();
				txtPrecioCatalogoSoles.Text = Row.Cells["preciocatalogo"].Value.ToString();
				CargaListaNotas();
				CargaListaEquivalencias();
				CargaStockProducto();
				CargaProductosProveedor();
				if (tabControl.SelectedTab.Text == "Promedios")
				{
					CargaPromedios(e.RowIndex);
				}
				return;
			}
			if (!e.Column.Name.Equals(NomCol))
			{
				switch (e.Column.Name)
				{
				case "marcadesc":
					lblCombo.Text = "Marca";
					lblCombo.Visible = true;
					cmbValor.Visible = true;
					cmbValor.DataSource = null;
					cmbValor.DisplayMember = "descripcion";
					cmbValor.ValueMember = "codmarca";
					cmbValor.DataSource = admmarca.MuestraMarcas();
					cmbValor.SelectedIndex = 0;
					cmbValor.Visible = true;
					btnCombo.Visible = true;
					lblTexto.Visible = false;
					txtValor.Visible = false;
					btnTexto.Visible = false;
					NomCol = e.Column.Name;
					break;
				case "familiadesc":
					lblCombo.Text = "Familia";
					lblCombo.Visible = true;
					cmbValor.Visible = true;
					cmbValor.DataSource = null;
					cmbValor.DisplayMember = "referencia";
					cmbValor.ValueMember = "codfamilia";
					cmbValor.DataSource = admfam.MuestraFamilias();
					cmbValor.SelectedIndex = 0;
					cmbValor.Visible = true;
					btnCombo.Visible = true;
					lblTexto.Visible = false;
					txtValor.Visible = false;
					btnTexto.Visible = false;
					NomCol = e.Column.Name;
					break;
				case "modelodesc":
				{
					lblCombo.Text = "Modelo";
					NomCol = e.Column.Name;
					if (dgvprods.SelectedCells.Count <= 0)
					{
						break;
					}
					int tam3 = dgvprods.SelectedCells.Count;
					int cont3 = 0;
					if (!NomCol.Equals(""))
					{
						if (NomCol.Equals(e.Column.Name))
						{
							cont3 = Enumerable.Where<GridViewCellInfo>(dgvprods.SelectedCells.AsEnumerable(), (Func<GridViewCellInfo, bool>)((GridViewCellInfo x) => x.ColumnInfo.Name == NomCol)).Count();
						}
					}
					else if (NomCol.Equals(""))
					{
						cont3 = dgvprods.SelectedCells.Count;
					}
					if (tam3 == cont3)
					{
						int codLin = Convert.ToInt32(dgvprods.Rows[dgvprods.SelectedCells[0].RowInfo.Index].Cells["codlinea"].Value);
						cmbValor.DataSource = null;
						cmbValor.DisplayMember = "referencia";
						cmbValor.ValueMember = "codgrupo";
						cmbValor.DataSource = admgrupo.MuestraGrupos(codLin);
						cmbValor.Visible = true;
						btnCombo.Visible = true;
						lblCombo.Visible = true;
						lblTexto.Visible = false;
						txtValor.Visible = false;
						btnTexto.Visible = false;
					}
					else
					{
						MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
					}
					break;
				}
				case "lineadesc":
				{
					lblCombo.Text = "Linea";
					NomCol = e.Column.Name;
					if (dgvprods.SelectedCells.Count <= 0)
					{
						break;
					}
					int tam2 = dgvprods.SelectedCells.Count;
					int cont2 = 0;
					if (!NomCol.Equals(""))
					{
						if (NomCol.Equals(e.Column.Name))
						{
							cont2 = Enumerable.Where<GridViewCellInfo>(dgvprods.SelectedCells.AsEnumerable(), (Func<GridViewCellInfo, bool>)((GridViewCellInfo x) => x.ColumnInfo.Name == NomCol)).Count();
						}
					}
					else if (NomCol.Equals(""))
					{
						cont2 = dgvprods.SelectedCells.Count;
					}
					if (tam2 == cont2)
					{
						int codfam = Convert.ToInt32(dgvprods.Rows[dgvprods.SelectedCells[0].RowInfo.Index].Cells["codfamilia"].Value);
						cmbValor.DataSource = null;
						cmbValor.DisplayMember = "referencia";
						cmbValor.ValueMember = "codlinea";
						cmbValor.DataSource = admlinea.MuestraLineas(codfam);
						cmbValor.Visible = true;
						btnCombo.Visible = true;
						lblCombo.Visible = true;
						lblTexto.Visible = false;
						txtValor.Visible = false;
						btnTexto.Visible = false;
					}
					else
					{
						MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
					}
					break;
				}
				case "preciocompra":
					if (NomCol == e.Column.Name || NomCol == "")
					{
						lblCombo.Text = "Precio Compra";
						NomCol = e.Column.Name;
						if (dgvprods.SelectedCells.Count <= 0)
						{
							break;
						}
						int tam4 = dgvprods.SelectedCells.Count;
						int cont4 = 0;
						if (!NomCol.Equals(""))
						{
							if (NomCol.Equals(e.Column.Name))
							{
								cont4 = Enumerable.Where<GridViewCellInfo>(dgvprods.SelectedCells.AsEnumerable(), (Func<GridViewCellInfo, bool>)((GridViewCellInfo x) => x.ColumnInfo.Name == NomCol)).Count();
							}
						}
						else if (NomCol.Equals(""))
						{
							cont4 = dgvprods.SelectedCells.Count;
						}
						if (tam4 == cont4)
						{
							break;
						}
						MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
						listaundcompra = new List<List<clsUnidadEquivalente>>();
						foreach (GridViewCellInfo a in dgvprods.SelectedCells)
						{
							a.IsSelected = false;
						}
					}
					else
					{
						NomCol = e.Column.Name;
						MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
						listaundventa = new List<List<clsUnidadEquivalente>>();
						listaundcompra = new List<List<clsUnidadEquivalente>>();
						dgvprods.CurrentCell.IsSelected = false;
					}
					break;
				case "precioventa":
					if (NomCol == e.Column.Name || NomCol == "")
					{
						lblCombo.Text = "Precio Venta";
						NomCol = e.Column.Name;
						if (dgvprods.SelectedCells.Count <= 0)
						{
							break;
						}
						int tam = dgvprods.SelectedCells.Count;
						int cont = 0;
						if (!NomCol.Equals(""))
						{
							if (NomCol.Equals(e.Column.Name))
							{
								cont = Enumerable.Where<GridViewCellInfo>(dgvprods.SelectedCells.AsEnumerable(), (Func<GridViewCellInfo, bool>)((GridViewCellInfo x) => x.ColumnInfo.Name == NomCol)).Count();
							}
						}
						else if (NomCol.Equals(""))
						{
							cont = dgvprods.SelectedCells.Count;
						}
						if (tam != cont)
						{
							MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
							listaundventa = new List<List<clsUnidadEquivalente>>();
							listaundcompra = new List<List<clsUnidadEquivalente>>();
							dgvprods.CurrentCell.IsSelected = false;
						}
					}
					else
					{
						NomCol = e.Column.Name;
						MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
						listaundventa = new List<List<clsUnidadEquivalente>>();
						listaundcompra = new List<List<clsUnidadEquivalente>>();
						dgvprods.CurrentCell.IsSelected = false;
					}
					break;
				default:
					cmbValor.Visible = false;
					btnCombo.Visible = false;
					lblCombo.Visible = false;
					lblTexto.Visible = false;
					txtValor.Visible = false;
					btnTexto.Visible = false;
					NomCol = "";
					break;
				}
			}
			if (NomCol == "preciocompra" && dgvprods.SelectedCells.Count > 0)
			{
				int codprod = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
				bool existe = Enumerable.Where<GridViewCellInfo>(dgvprods.SelectedCells.AsEnumerable(), (Func<GridViewCellInfo, bool>)((GridViewCellInfo x) => Convert.ToInt32(x.RowInfo.Cells["codproducto"].Value) == codprod)).Any();
				bool existe2 = Enumerable.Where<clsUnidadEquivalente>(Enumerable.SelectMany<List<clsUnidadEquivalente>, clsUnidadEquivalente>((IEnumerable<List<clsUnidadEquivalente>>)listaundcompra, (Func<List<clsUnidadEquivalente>, IEnumerable<clsUnidadEquivalente>>)((List<clsUnidadEquivalente> d) => d.ToList())), (Func<clsUnidadEquivalente, bool>)((clsUnidadEquivalente x) => x.CodProducto == codprod)).Any();
				List<clsUnidadEquivalente> listueq = AdmPro.unidadCompraxProducto(codprod);
				if (existe)
				{
					if (!existe2)
					{
						if (listaundcompra.Count > 0)
						{
							int cuenta = Enumerable.Select<List<clsUnidadEquivalente>, List<clsUnidadEquivalente>>((IEnumerable<List<clsUnidadEquivalente>>)listaundcompra, (Func<List<clsUnidadEquivalente>, List<clsUnidadEquivalente>>)((List<clsUnidadEquivalente> d) => d.ToList())).ToList()[0].Count;
							if (cuenta == listueq.Count)
							{
								List<clsUnidadEquivalente> listaaux = new List<clsUnidadEquivalente>();
								listaaux = listaundcompra[0];
								int i = 0;
								int i2 = 0;
								foreach (clsUnidadEquivalente ueq in listaaux)
								{
									foreach (clsUnidadEquivalente ueq2 in listueq)
									{
										if (ueq2.CodUnidad == ueq.CodUnidad)
										{
											i++;
										}
									}
								}
								foreach (clsUnidadEquivalente ueq3 in listaaux)
								{
									foreach (clsUnidadEquivalente ueq4 in listueq)
									{
										if (ueq4.Precio == ueq3.Precio)
										{
											i2++;
										}
									}
								}
								if (i == cuenta)
								{
									if (i2 == cuenta)
									{
										listaundcompra.Add(listueq);
									}
									else
									{
										MessageBox.Show("Este producto no tiene los mismos precios que los ya seleccionados");
										dgvprods.Rows[e.RowIndex].Cells["preciocompra"].IsSelected = false;
									}
								}
								else
								{
									MessageBox.Show("Este producto no tiene las mismas unidades que los ya seleccionados");
									dgvprods.Rows[e.RowIndex].Cells["preciocompra"].IsSelected = false;
								}
							}
							else
							{
								MessageBox.Show("Este producto no tiene las mismas unidades que los ya seleccionados");
								dgvprods.Rows[e.RowIndex].Cells["preciocompra"].IsSelected = false;
							}
						}
						else
						{
							listaundcompra.Add(listueq);
						}
					}
				}
				else if (existe2 && listaundcompra.Remove(listueq))
				{
					Console.WriteLine("se elimino");
				}
				if (listaundcompra.Count > 0)
				{
					List<clsUnidadEquivalente> listaaux2 = new List<clsUnidadEquivalente>();
					listaaux2 = listaundcompra[0];
					cmbValor.DataSource = null;
					cmbValor.DisplayMember = "nombreUnd";
					cmbValor.ValueMember = "codUnidad";
					cmbValor.DataSource = listaaux2;
					cmbValor.Visible = true;
					btnCombo.Visible = false;
					lblCombo.Visible = true;
					lblTexto.Visible = true;
					txtValor.Visible = true;
					btnTexto.Visible = true;
				}
			}
			if (!(NomCol == "precioventa") || dgvprods.SelectedCells.Count <= 0)
			{
				return;
			}
			int codprod2 = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
			bool existe3 = Enumerable.Where<GridViewCellInfo>(dgvprods.SelectedCells.AsEnumerable(), (Func<GridViewCellInfo, bool>)((GridViewCellInfo x) => Convert.ToInt32(x.RowInfo.Cells["codproducto"].Value) == codprod2)).Any();
			bool existe4 = Enumerable.Where<clsUnidadEquivalente>(Enumerable.SelectMany<List<clsUnidadEquivalente>, clsUnidadEquivalente>((IEnumerable<List<clsUnidadEquivalente>>)listaundventa, (Func<List<clsUnidadEquivalente>, IEnumerable<clsUnidadEquivalente>>)((List<clsUnidadEquivalente> d) => d.ToList())), (Func<clsUnidadEquivalente, bool>)((clsUnidadEquivalente x) => x.CodProducto == codprod2)).Any();
			List<clsUnidadEquivalente> listueq2 = AdmPro.unidadVentaxProducto(codprod2);
			if (existe3)
			{
				if (!existe4)
				{
					if (listaundventa.Count > 0)
					{
						int cuenta2 = Enumerable.Select<List<clsUnidadEquivalente>, List<clsUnidadEquivalente>>((IEnumerable<List<clsUnidadEquivalente>>)listaundventa, (Func<List<clsUnidadEquivalente>, List<clsUnidadEquivalente>>)((List<clsUnidadEquivalente> d) => d.ToList())).ToList()[0].Count;
						if (cuenta2 == listueq2.Count)
						{
							List<clsUnidadEquivalente> listaaux3 = new List<clsUnidadEquivalente>();
							listaaux3 = listaundventa[0];
							int i3 = 0;
							int i4 = 0;
							foreach (clsUnidadEquivalente ueq5 in listaaux3)
							{
								foreach (clsUnidadEquivalente ueq6 in listueq2)
								{
									if (ueq6.CodUnidad == ueq5.CodUnidad)
									{
										i3++;
									}
								}
							}
							foreach (clsUnidadEquivalente ueq7 in listaaux3)
							{
								foreach (clsUnidadEquivalente ueq8 in listueq2)
								{
									if (ueq8.Precio == ueq7.Precio)
									{
										i4++;
									}
								}
							}
							if (i3 == cuenta2)
							{
								if (i4 == cuenta2)
								{
									listaundventa.Add(listueq2);
								}
								else
								{
									MessageBox.Show("Este producto no tiene los mismos precios que los ya seleccionados");
									dgvprods.Rows[e.RowIndex].Cells["precioventa"].IsSelected = false;
								}
							}
							else
							{
								MessageBox.Show("Este producto no tiene las mismas unidades que los ya seleccionados");
								dgvprods.Rows[e.RowIndex].Cells["precioventa"].IsSelected = false;
							}
						}
						else
						{
							MessageBox.Show("Este producto no tiene las mismas unidades que los ya seleccionados");
							dgvprods.Rows[e.RowIndex].Cells["precioventa"].IsSelected = false;
						}
					}
					else
					{
						listaundventa.Add(listueq2);
					}
				}
			}
			else if (existe4 && listaundventa.Remove(listueq2))
			{
				Console.WriteLine("se elimino");
			}
			if (listaundventa.Count > 0)
			{
				List<clsUnidadEquivalente> listaaux4 = new List<clsUnidadEquivalente>();
				listaaux4 = listaundventa[0];
				cmbValor.DataSource = null;
				cmbValor.DisplayMember = "nombreUnd";
				cmbValor.ValueMember = "codUnidad";
				cmbValor.DataSource = listaaux4;
				cmbValor.Visible = true;
				btnCombo.Visible = false;
				lblCombo.Visible = true;
				lblTexto.Visible = true;
				txtValor.Visible = true;
				btnTexto.Visible = true;
			}
		}
	}

	private void CargaPromedios(int fila)
	{
	}

	private void dgvprods_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (!Editar && e.RowIndex != -1 && dgvprods.CurrentRow != null)
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 3;
			frm.pro = pro;
			frm.ShowDialog();
		}
	}

	private void dgvprods_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		e.CellElement.BorderLeftWidth = 1f;
		e.CellElement.BorderRightWidth = 1f;
		e.CellElement.BorderTopWidth = 1f;
		e.CellElement.BorderBottomWidth = 1f;
	}

	private void dgvprods_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		if (e.CellElement is GridFilterCellElement)
		{
			e.CellElement.RowInfo.Height = 40;
		}
		if (e.CellElement is GridHeaderCellElement)
		{
			e.CellElement.RowInfo.Height = 20;
		}
	}

	private void dgvprods_ViewRowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (e.RowElement is GridDataRowElement)
		{
			e.RowElement.RowInfo.Height = 40;
			e.RowElement.TextWrap = true;
		}
	}

	private void buttonItem7_Click(object sender, EventArgs e)
	{
		Editar = !Editar;
		if (!Editar)
		{
			tabEdicion.Hide();
			btnEditar.Text = "Editar Grid";
			dgvprods.ClearSelection();
			dgvprods.FilterDescriptors.Expression = "";
			if (Ids.Count > 0)
			{
				foreach (int i in Ids)
				{
					GridViewRowInfo r = Enumerable.Select<GridViewRowInfo, GridViewRowInfo>(Enumerable.Where<GridViewRowInfo>(dgvprods.Rows.AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => Convert.ToInt32(x.Cells[0].Value) == i)), (Func<GridViewRowInfo, GridViewRowInfo>)((GridViewRowInfo x) => x)).ToList()[0];
					if (r != null)
					{
						DataRow prod = productostable.NewRow();
						prod["codpro"] = r.Cells["codproducto"].Value;
						prod["codgru"] = r.Cells["codmodelo"].Value;
						prod["codlin"] = r.Cells["codlinea"].Value;
						prod["codfam"] = r.Cells["codfamilia"].Value;
						prod["codmar"] = r.Cells["codmarca"].Value;
						prod["precioca"] = r.Cells["preciocatalogo"].Value;
						prod["preciocompra"] = 0.0;
						productostable.Rows.Add(prod);
					}
				}
				if (AdmPro.updateMasivo(productostable))
				{
					MessageBox.Show("Se actualizaron " + productostable.Rows.Count + " productos");
				}
				else
				{
					MessageBox.Show("Hubo un error al actualizar");
				}
			}
			if (listaundcompra.Count > 0)
			{
				foreach (List<clsUnidadEquivalente> un in listaundcompra)
				{
					foreach (clsUnidadEquivalente eq in un)
					{
						if (eq.CodUnidadEquivalente == codUnidadCompraEditar)
						{
							DataRow und = unidadesTable.NewRow();
							und["codpro"] = eq.CodUnidadEquivalente;
							und["codgru"] = precioCompraEditar;
							unidadesTable.Rows.Add(und);
						}
					}
				}
			}
			if (listaundventa.Count > 0)
			{
				foreach (List<clsUnidadEquivalente> un2 in listaundventa)
				{
					foreach (clsUnidadEquivalente eq2 in un2)
					{
						if (eq2.CodUnidad == codUnidadVentaEditar)
						{
							DataRow und2 = unidadesTable.NewRow();
							und2["cod"] = eq2.CodUnidadEquivalente;
							und2["p"] = precioVentaEditar;
							unidadesTable.Rows.Add(und2);
						}
					}
				}
			}
			if (listaundcompra.Count > 0 || listaundventa.Count > 0)
			{
				if (AdmPro.UpdateUnidadEquivalenteMasivo(unidadesTable))
				{
					MessageBox.Show("Se actualizaron " + unidadesTable.Rows.Count + " precios de productos (compra/venta)");
				}
				else
				{
					MessageBox.Show("Hubo un error al actualizar");
				}
			}
			CargaLista();
			productostable = new DataTable();
			creaFilas(productostable);
			unidadesTable = new DataTable();
			creaFilasUnidades(unidadesTable);
			Ids = new List<int>();
			listaundcompra = new List<List<clsUnidadEquivalente>>();
			listaundventa = new List<List<clsUnidadEquivalente>>();
			codUnidadVentaEditar = 0;
			codUnidadCompraEditar = 0;
			precioVentaEditar = 0m;
			precioCompraEditar = 0m;
		}
		else
		{
			tabEdicion.Show();
			cmbValor.Visible = false;
			cmbValor.DataSource = null;
			btnCombo.Visible = false;
			lblCombo.Visible = false;
			lblTexto.Visible = false;
			txtValor.Visible = false;
			txtValor.Text = "";
			btnTexto.Visible = false;
			NomCol = "";
			btnEditar.Text = "Guardar";
		}
		dgvprods.MultiSelect = Editar;
		lblEdicion.Visible = Editar;
		buttonItem16.Enabled = !Editar;
		buttonItem6.Enabled = !Editar;
		buttonItem4.Enabled = !Editar;
		buttonItem9.Enabled = !Editar;
	}

	private void dgvprods_CellValueChanged(object sender, GridViewCellEventArgs e)
	{
	}

	private void dgvprods_ValueChanged(object sender, EventArgs e)
	{
	}

	private void btnCombo_Click(object sender, EventArgs e)
	{
		if (dgvprods.SelectedCells.Count <= 0)
		{
			return;
		}
		int tam = dgvprods.SelectedCells.Count;
		int cont = 0;
		foreach (GridViewCellInfo gr in dgvprods.SelectedCells)
		{
			if (gr.ColumnInfo.Name.Equals(NomCol))
			{
				cont++;
			}
		}
		if (tam == cont)
		{
			foreach (GridViewCellInfo gr2 in dgvprods.SelectedCells)
			{
				if (!Enumerable.Where<int>(Ids.AsEnumerable(), (Func<int, bool>)((int x) => x == Convert.ToInt32(gr2.RowInfo.Cells["codproducto"].Value))).Any() && gr2.RowInfo.Cells["codproducto"].Value != null)
				{
					Ids.Add(Convert.ToInt32(gr2.RowInfo.Cells["codproducto"].Value));
				}
				gr2.Value = cmbValor.Text;
				dgvprods.Rows[gr2.RowInfo.Index].Cells[gr2.ColumnInfo.Index - 1].Value = cmbValor.SelectedValue;
			}
			return;
		}
		MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
	}

	private void btnTexto_Click(object sender, EventArgs e)
	{
		if (dgvprods.SelectedCells.Count <= 0)
		{
			return;
		}
		if (!txtValor.Equals(""))
		{
			int tam = dgvprods.SelectedCells.Count;
			int cont = 0;
			foreach (GridViewCellInfo gr in dgvprods.SelectedCells)
			{
				if (gr.ColumnInfo.Name.Equals(NomCol))
				{
					cont++;
				}
			}
			if (tam == cont)
			{
				foreach (GridViewCellInfo gr2 in dgvprods.SelectedCells)
				{
					gr2.Value = txtValor.Text;
				}
				if (NomCol.Equals("preciocompra"))
				{
					codUnidadCompraEditar = Convert.ToInt32(cmbValor.SelectedValue);
					precioCompraEditar = Convert.ToDecimal(txtValor.Text);
				}
				else if (NomCol.Equals("precioventa"))
				{
					codUnidadVentaEditar = Convert.ToInt32(cmbValor.SelectedValue);
					precioVentaEditar = Convert.ToDecimal(txtValor.Text);
				}
			}
			else
			{
				MessageBox.Show("Todas las celdas a editar tienen que ser de la misma columna");
			}
		}
		else
		{
			MessageBox.Show("No dejar el campo vacio");
		}
	}

	private void dgvprods_CellValueChanged_1(object sender, GridViewCellEventArgs e)
	{
	}

	private void dgvprods_RowPaint(object sender, GridViewRowPaintEventArgs e)
	{
		try
		{
			if (e.Row != null && e.Row is GridDataRowElement dataRow)
			{
				int value = 0;
				if (dataRow.RowInfo.Cells[22].Value != DBNull.Value)
				{
					value = Convert.ToInt32(dataRow.RowInfo.Cells[22].Value);
				}
				if (value == 0)
				{
					Pen pen = Pens.Red;
					Size rowSize = dataRow.Size;
					rowSize.Height -= 5;
					rowSize.Width -= 5;
					e.Graphics.DrawRectangle(pen, new Rectangle(new Point(2, 2), rowSize));
				}
			}
		}
		catch (Exception)
		{
		}
	}

	private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
	{
		string titulodeltab = tabControl.TabPages[e.TabPageIndex].Text;
		if (titulodeltab == "Promedios")
		{
			CargaPromedios(dgvprods.CurrentRow.Index);
		}
	}

	private void btnItemActualizaProdAsoc_Click(object sender, EventArgs e)
	{
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		if (true)
		{
			frmImportaData frm = new frmImportaData();
			frm.Proceso = 2;
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.ShowDialog();
		}
		else
		{
			MessageBox.Show("No tiene permiso para generar importacion.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnexExcel_Click(object sender, EventArgs e)
	{
		int ColInicial = 0;
		int RowInicial = 2;
		SLDocument sl = new SLDocument();
		SLStyle styleCenter = sl.CreateStyle();
		styleCenter.Alignment.Horizontal = HorizontalAlignmentValues.Center;
		styleCenter.Font.FontSize = 10.0;
		styleCenter.Font.Bold = true;
		asignarBordes(styleCenter);
		styleCenter.SetFontColor(System.Drawing.Color.White);
		styleCenter.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 0, 80), System.Drawing.Color.DarkBlue);
		foreach (GridViewDataColumn column in dgvprods.Columns)
		{
			if (column.IsVisible && column.HeaderText != string.Empty)
			{
				ColInicial++;
				sl.SetCellValue(1, 1, "Catalogo de Productos");
				sl.SetCellStyle(1, 1, styleCenter);
				sl.MergeWorksheetCells("A1", "N1");
				sl.SetCellValue(2, ColInicial, column.HeaderText.ToString());
				sl.SetCellStyle(RowInicial, ColInicial, styleCenter);
			}
		}
		sl.SetCellStyle(1, 1, 1, ColInicial, styleCenter);
		sl.SetColumnWidth(1, 18.0);
		sl.SetColumnWidth(2, 18.0);
		sl.SetColumnWidth(3, 18.0);
		sl.SetColumnWidth(4, 60.0);
		sl.SetColumnWidth(5, 15.0);
		sl.SetColumnWidth(6, 12.0);
		sl.SetColumnWidth(7, 18.0);
		sl.SetColumnWidth(8, 18.0);
		sl.SetColumnWidth(9, 18.0);
		sl.SetColumnWidth(10, 20.0);
		sl.SetColumnWidth(11, 15.0);
		sl.SetColumnWidth(12, 15.0);
		sl.SetColumnWidth(13, 15.0);
		sl.SetColumnWidth(14, 50.0);
		sl.SetColumnWidth(15, 12.0);
		SLStyle styleFormat = sl.CreateStyle();
		styleFormat.FormatCode = "[Black]#,##0.00";
		foreach (GridViewRowInfo row in dgvprods.Rows)
		{
			decimal morosidad = default(decimal);
			RowInicial++;
			string referencia = row.Cells["referencia"].Value.ToString();
			sl.SetCellValue(RowInicial, 1, referencia);
			sl.SetCellValue(RowInicial, 2, row.Cells["CUPC"].Value.ToString());
			sl.SetCellValue(RowInicial, 3, row.Cells["CUPV"].Value.ToString());
			sl.SetCellValue(RowInicial, 4, row.Cells["descripcion"].Value.ToString());
			sl.SetCellValue(RowInicial, 5, row.Cells["marcadesc"].Value.ToString());
			sl.SetCellValue(RowInicial, 6, row.Cells["unidad"].Value.ToString());
			decimal preciocatalogo = decimal.Parse(row.Cells["preciocatalogo"].Value?.ToString());
			sl.SetCellValue(RowInicial, 7, preciocatalogo);
			sl.SetCellStyle(RowInicial, 7, styleFormat);
			string preCompra = row.Cells["preciocompra"].Value?.ToString();
			if (preCompra == string.Empty)
			{
				sl.SetCellValue(RowInicial, 8, preCompra);
			}
			else
			{
				decimal preciocompra = decimal.Parse(preCompra);
				sl.SetCellValue(RowInicial, 8, preciocompra);
				sl.SetCellStyle(RowInicial, 8, styleFormat);
			}
			string preVenta = row.Cells["precioventa"].Value?.ToString();
			if (preVenta == string.Empty)
			{
				sl.SetCellValue(RowInicial, 9, preVenta);
			}
			else
			{
				decimal precioventa = decimal.Parse(preVenta);
				sl.SetCellValue(RowInicial, 9, precioventa);
				sl.SetCellStyle(RowInicial, 9, styleFormat);
			}
			sl.SetCellValue(RowInicial, 10, row.Cells["descPrecioVenta"].Value.ToString());
			sl.SetCellValue(RowInicial, 11, row.Cells["familiadesc"].Value.ToString());
			sl.SetCellValue(RowInicial, 12, row.Cells["lineadesc"].Value.ToString());
			sl.SetCellValue(RowInicial, 13, row.Cells["modelodesc"].Value.ToString());
			sl.SetCellValue(RowInicial, 14, row.Cells["ubicacion"].Value.ToString());
			sl.SetCellValue(RowInicial, 15, row.Cells["nombreestado"].Value.ToString());
		}
		DataTable dtUnd = unidad.MuestraUnidades1();
		int ColInicial2 = 1;
		int RowInicial2 = 2;
		foreach (DataColumn column2 in dtUnd.Columns)
		{
			ColInicial2++;
			sl.AddWorksheet("UNIDAD DE MEDIDA");
			sl.SetCellValue(RowInicial2, ColInicial2, column2.ColumnName.ToString());
			sl.SetCellStyle(RowInicial2, ColInicial2, styleCenter);
		}
		foreach (DataRow row2 in dtUnd.Rows)
		{
			RowInicial2++;
			string codigo = row2["codUnidadMedida"].ToString();
			sl.SetCellValue(RowInicial2, 2, codigo.PadLeft(3, '0'));
			sl.SetCellValue(RowInicial2, 3, row2["sigla"].ToString());
			sl.SetCellValue(RowInicial2, 4, row2["descripcion"].ToString());
			sl.SetCellValue(RowInicial2, 5, row2["estado"].ToString());
			sl.SetCellValue(RowInicial2, 6, row2["codUser"].ToString());
			sl.SetCellValue(RowInicial2, 7, row2["fecharegistro"].ToString());
		}
		sl.HideColumn(3, 3);
		sl.HideColumn(6, 7);
		sl.SetColumnWidth(2, 18.0);
		sl.SetColumnWidth(3, 20.0);
		sl.SetColumnWidth(4, 60.0);
		sl.SetColumnWidth(5, 12.0);
		sl.SetColumnWidth(6, 12.0);
		sl.SetColumnWidth(7, 18.0);
		DataTable dt = tipPrecio.listaPrecios();
		int ColInicial3 = 1;
		int RowInicial3 = 2;
		foreach (DataColumn column3 in dt.Columns)
		{
			ColInicial3++;
			sl.AddWorksheet("TIPO DE PRECIOS");
			sl.SetCellValue(RowInicial3, ColInicial3, column3.ColumnName.ToString());
			sl.SetCellStyle(RowInicial3, ColInicial3, styleCenter);
		}
		foreach (DataRow row3 in dt.Rows)
		{
			RowInicial3++;
			string codigo2 = row3["codT"].ToString();
			sl.SetCellValue(RowInicial3, 2, codigo2.PadLeft(2, '0'));
			sl.SetCellValue(RowInicial3, 3, row3["Sigla"].ToString());
			sl.SetCellValue(RowInicial3, 4, row3["Descripcion"].ToString());
		}
		sl.SetColumnWidth(2, 15.0);
		sl.SetColumnWidth(3, 20.0);
		sl.SetColumnWidth(4, 60.0);
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

	private void asignarBordes(SLStyle style)
	{
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.White;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.White;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.White;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.White;
	}

	private string obtenerRutaParaGuardar()
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo de Excel";
			sfd.FileName = "Exportacion_Catalogo_Productos";
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de catalogo de Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void btnactualizarproductos_Click(object sender, EventArgs e)
	{
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		if (true)
		{
			frmImportaData frm = new frmImportaData();
			frm.Proceso = 3;
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.ShowDialog();
		}
		else
		{
			MessageBox.Show("No tiene permiso para generar importacion.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnactualizarcategorizacion_Click(object sender, EventArgs e)
	{
		try
		{
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			dBAccess.AddParameter("valor", 0);
			dBAccess.AddParameter("_codproducto", 0);
			ds = dBAccess.ExecuteDataSet("totalproductosvendidos");
			ds = dBAccess.ExecuteDataSet("promedioproductosvendidos");
			CargaLista();
			MessageBox.Show("Estado Categorización Actualizada", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error : " + ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCatalogo));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
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
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.biCatalogo = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
		this.btnEditar = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemActualizaProdAsoc3 = new DevComponents.DotNetBar.ButtonItem();
		this.btnItemActualizaProdAsoc = new DevComponents.DotNetBar.ButtonItem();
		this.btnexExcel = new DevComponents.DotNetBar.ButtonItem();
		this.btnactualizarproductos = new DevComponents.DotNetBar.ButtonItem();
		this.btnactualizarcategorizacion = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblEdicion = new Telerik.WinControls.UI.RadLabel();
		this.tabControl = new System.Windows.Forms.TabControl();
		this.tpDetalleProducto = new System.Windows.Forms.TabPage();
		this.label8 = new System.Windows.Forms.Label();
		this.txtPrecioCatalogoSoles = new System.Windows.Forms.TextBox();
		this.txtPrecioCatalogo = new System.Windows.Forms.TextBox();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.txtCodProducto = new System.Windows.Forms.TextBox();
		this.label39 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.tpPrecios = new System.Windows.Forms.TabPage();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigoundMed = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Factor1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUndEqui = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.equivalente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precios1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tpStockAlmacenes = new System.Windows.Forms.TabPage();
		this.dgvAlmacenes = new System.Windows.Forms.DataGridView();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.entregar = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.disponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.recibir = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.futuro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.minimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.maximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.reposicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tpProveedores = new System.Windows.Forms.TabPage();
		this.dgvProxProducto = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pneto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.dgvNotas = new System.Windows.Forms.DataGridView();
		this.codnotaproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabEdicion = new System.Windows.Forms.TabPage();
		this.btnTexto = new Telerik.WinControls.UI.RadButton();
		this.btnCombo = new Telerik.WinControls.UI.RadButton();
		this.lblTexto = new Telerik.WinControls.UI.RadLabel();
		this.lblCombo = new Telerik.WinControls.UI.RadLabel();
		this.txtValor = new Telerik.WinControls.UI.RadTextBox();
		this.cmbValor = new Telerik.WinControls.UI.RadDropDownList();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.dgvprods = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.lblEdicion).BeginInit();
		this.tabControl.SuspendLayout();
		this.tpDetalleProducto.SuspendLayout();
		this.tpPrecios.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.tpStockAlmacenes.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).BeginInit();
		this.tpProveedores.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProxProducto).BeginInit();
		this.tabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNotas).BeginInit();
		this.tabEdicion.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnTexto).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnCombo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblTexto).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblCombo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbValor).BeginInit();
		this.expandablePanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvprods).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvprods.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "sites-icon-large.png");
		this.imageList1.Images.SetKeyName(19, "ksysguard.png");
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[13]
		{
			this.buttonItem16, this.buttonItem6, this.buttonItem2, this.buttonItem3, this.buttonItem4, this.buttonItem5, this.buttonItem9, this.biCatalogo, this.buttonItem1, this.buttonItem8,
			this.btnEditar, this.btnItemActualizaProdAsoc3, this.btnactualizarcategorizacion
		});
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(1002, 82);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 3;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.buttonItem16.ImageIndex = 4;
		this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem16.Name = "buttonItem16";
		this.buttonItem16.SubItemsExpandWidth = 14;
		this.buttonItem16.Text = "Nuevo";
		this.buttonItem16.Click += new System.EventHandler(buttonItem16_Click);
		this.buttonItem6.ImageIndex = 3;
		this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem6.Name = "buttonItem6";
		this.buttonItem6.SubItemsExpandWidth = 14;
		this.buttonItem6.Text = "Modificar";
		this.buttonItem6.Click += new System.EventHandler(buttonItem6_Click);
		this.buttonItem2.Enabled = false;
		this.buttonItem2.ImageIndex = 19;
		this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Text = "Composición Quimica - Dosis";
		this.buttonItem2.Visible = false;
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click);
		this.buttonItem3.Enabled = false;
		this.buttonItem3.ImageIndex = 17;
		this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem3.Name = "buttonItem3";
		this.buttonItem3.SubItemsExpandWidth = 14;
		this.buttonItem3.Text = "Consultar";
		this.buttonItem3.Visible = false;
		this.buttonItem3.Click += new System.EventHandler(buttonItem3_Click);
		this.buttonItem4.ImageIndex = 8;
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Text = "Actualizar";
		this.buttonItem4.Click += new System.EventHandler(buttonItem4_Click);
		this.buttonItem5.ImageIndex = 11;
		this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem5.Name = "buttonItem5";
		this.buttonItem5.SubItemsExpandWidth = 14;
		this.buttonItem5.Text = "Buscar";
		this.buttonItem5.Click += new System.EventHandler(buttonItem5_Click);
		this.buttonItem9.ImageIndex = 7;
		this.buttonItem9.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.SubItemsExpandWidth = 14;
		this.buttonItem9.Text = "Exportar";
		this.buttonItem9.Click += new System.EventHandler(buttonItem9_Click);
		this.biCatalogo.Enabled = false;
		this.biCatalogo.ImageIndex = 18;
		this.biCatalogo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biCatalogo.Name = "biCatalogo";
		this.biCatalogo.SubItemsExpandWidth = 14;
		this.biCatalogo.Text = "Catalogo";
		this.biCatalogo.Visible = false;
		this.biCatalogo.Click += new System.EventHandler(biCatalogo_Click);
		this.buttonItem1.Image = SIGEFA.Properties.Resources.ganancia;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Ganancia x articulo";
		this.buttonItem1.Click += new System.EventHandler(buttonItem1_Click);
		this.buttonItem8.ImageIndex = 5;
		this.buttonItem8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem8.Name = "buttonItem8";
		this.buttonItem8.SubItemsExpandWidth = 14;
		this.buttonItem8.Text = "Dar Baja";
		this.buttonItem8.Click += new System.EventHandler(buttonItem8_Click);
		this.btnEditar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.SubItemsExpandWidth = 14;
		this.btnEditar.Text = "Editar Grid";
		this.btnEditar.Click += new System.EventHandler(buttonItem7_Click);
		this.btnItemActualizaProdAsoc3.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnItemActualizaProdAsoc3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnItemActualizaProdAsoc3.Name = "btnItemActualizaProdAsoc3";
		this.btnItemActualizaProdAsoc3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.btnItemActualizaProdAsoc, this.btnexExcel, this.btnactualizarproductos });
		this.btnItemActualizaProdAsoc3.SubItemsExpandWidth = 14;
		this.btnItemActualizaProdAsoc3.Text = "Exportar";
		this.btnItemActualizaProdAsoc.Name = "btnItemActualizaProdAsoc";
		this.btnItemActualizaProdAsoc.Text = "Productos Asociados";
		this.btnItemActualizaProdAsoc.Click += new System.EventHandler(btnItemActualizaProdAsoc_Click);
		this.btnexExcel.Name = "btnexExcel";
		this.btnexExcel.Text = "Exportar Productos";
		this.btnexExcel.Click += new System.EventHandler(btnexExcel_Click);
		this.btnactualizarproductos.Name = "btnactualizarproductos";
		this.btnactualizarproductos.Text = "Actualiza Productos";
		this.btnactualizarproductos.Click += new System.EventHandler(btnactualizarproductos_Click);
		this.btnactualizarcategorizacion.Image = (System.Drawing.Image)resources.GetObject("btnactualizarcategorizacion.Image");
		this.btnactualizarcategorizacion.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnactualizarcategorizacion.Name = "btnactualizarcategorizacion";
		this.btnactualizarcategorizacion.SubItemsExpandWidth = 14;
		this.btnactualizarcategorizacion.Text = "Actualiza Categorización";
		this.btnactualizarcategorizacion.Click += new System.EventHandler(btnactualizarcategorizacion_Click);
		this.groupBox1.Controls.Add(this.lblEdicion);
		this.groupBox1.Controls.Add(this.tabControl);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 298);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1002, 152);
		this.groupBox1.TabIndex = 13;
		this.groupBox1.TabStop = false;
		this.lblEdicion.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.lblEdicion.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEdicion.ForeColor = System.Drawing.Color.Red;
		this.lblEdicion.Location = new System.Drawing.Point(722, 6);
		this.lblEdicion.Name = "lblEdicion";
		this.lblEdicion.RootElement.ControlBounds = new System.Drawing.Rectangle(722, 6, 100, 18);
		this.lblEdicion.Size = new System.Drawing.Size(108, 24);
		this.lblEdicion.TabIndex = 5;
		this.lblEdicion.Text = "Modo Edicion";
		this.lblEdicion.Visible = false;
		this.tabControl.Controls.Add(this.tpDetalleProducto);
		this.tabControl.Controls.Add(this.tpPrecios);
		this.tabControl.Controls.Add(this.tpStockAlmacenes);
		this.tabControl.Controls.Add(this.tpProveedores);
		this.tabControl.Controls.Add(this.tabPage3);
		this.tabControl.Controls.Add(this.tabEdicion);
		this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tabControl.Location = new System.Drawing.Point(3, 16);
		this.tabControl.Name = "tabControl";
		this.tabControl.SelectedIndex = 0;
		this.tabControl.Size = new System.Drawing.Size(996, 133);
		this.tabControl.TabIndex = 4;
		this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(tabControl_Selecting);
		this.tpDetalleProducto.Controls.Add(this.label8);
		this.tpDetalleProducto.Controls.Add(this.txtPrecioCatalogoSoles);
		this.tpDetalleProducto.Controls.Add(this.txtPrecioCatalogo);
		this.tpDetalleProducto.Controls.Add(this.txtNombre);
		this.tpDetalleProducto.Controls.Add(this.txtReferencia);
		this.tpDetalleProducto.Controls.Add(this.txtCodProducto);
		this.tpDetalleProducto.Controls.Add(this.label39);
		this.tpDetalleProducto.Controls.Add(this.label3);
		this.tpDetalleProducto.Controls.Add(this.label2);
		this.tpDetalleProducto.Controls.Add(this.label1);
		this.tpDetalleProducto.Location = new System.Drawing.Point(4, 22);
		this.tpDetalleProducto.Name = "tpDetalleProducto";
		this.tpDetalleProducto.Padding = new System.Windows.Forms.Padding(3);
		this.tpDetalleProducto.Size = new System.Drawing.Size(988, 107);
		this.tpDetalleProducto.TabIndex = 0;
		this.tpDetalleProducto.Text = "Detalle Producto";
		this.tpDetalleProducto.UseVisualStyleBackColor = true;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(526, 51);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(108, 13);
		this.label8.TabIndex = 39;
		this.label8.Text = "Precio Promedio(S/.):";
		this.txtPrecioCatalogoSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioCatalogoSoles.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtPrecioCatalogoSoles.Location = new System.Drawing.Point(529, 67);
		this.txtPrecioCatalogoSoles.Name = "txtPrecioCatalogoSoles";
		this.txtPrecioCatalogoSoles.ReadOnly = true;
		this.txtPrecioCatalogoSoles.Size = new System.Drawing.Size(105, 20);
		this.txtPrecioCatalogoSoles.TabIndex = 38;
		this.txtPrecioCatalogoSoles.Tag = "1";
		this.txtPrecioCatalogoSoles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioCatalogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioCatalogo.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtPrecioCatalogo.Location = new System.Drawing.Point(424, 67);
		this.txtPrecioCatalogo.Name = "txtPrecioCatalogo";
		this.txtPrecioCatalogo.ReadOnly = true;
		this.txtPrecioCatalogo.Size = new System.Drawing.Size(96, 20);
		this.txtPrecioCatalogo.TabIndex = 36;
		this.txtPrecioCatalogo.Tag = "1";
		this.txtPrecioCatalogo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombre.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtNombre.Location = new System.Drawing.Point(6, 67);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.ReadOnly = true;
		this.txtNombre.Size = new System.Drawing.Size(395, 20);
		this.txtNombre.TabIndex = 11;
		this.txtNombre.Tag = "1";
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtReferencia.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtReferencia.Location = new System.Drawing.Point(78, 28);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.ReadOnly = true;
		this.txtReferencia.Size = new System.Drawing.Size(100, 20);
		this.txtReferencia.TabIndex = 9;
		this.txtCodProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodProducto.ForeColor = System.Drawing.Color.SteelBlue;
		this.txtCodProducto.Location = new System.Drawing.Point(6, 28);
		this.txtCodProducto.Name = "txtCodProducto";
		this.txtCodProducto.ReadOnly = true;
		this.txtCodProducto.Size = new System.Drawing.Size(66, 20);
		this.txtCodProducto.TabIndex = 7;
		this.label39.AutoSize = true;
		this.label39.Location = new System.Drawing.Point(421, 51);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(99, 13);
		this.label39.TabIndex = 37;
		this.label39.Text = "Precio Promedio($):";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(3, 51);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(50, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "Nombre :";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(75, 12);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(62, 13);
		this.label2.TabIndex = 8;
		this.label2.Text = "Referencia:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(3, 12);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 6;
		this.label1.Text = "Código:";
		this.tpPrecios.Controls.Add(this.dataGridView1);
		this.tpPrecios.Location = new System.Drawing.Point(4, 22);
		this.tpPrecios.Name = "tpPrecios";
		this.tpPrecios.Padding = new System.Windows.Forms.Padding(3);
		this.tpPrecios.Size = new System.Drawing.Size(988, 107);
		this.tpPrecios.TabIndex = 1;
		this.tpPrecios.Text = "Precios";
		this.tpPrecios.UseVisualStyleBackColor = true;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.dataGridViewTextBoxColumn3, this.codigoundMed, this.unidad1, this.Factor1, this.codUndEqui, this.equivalente, this.precios1, this.codTipo, this.tipo);
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(982, 101);
		this.dataGridView1.TabIndex = 12;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "codUnidadEquivalente";
		this.dataGridViewTextBoxColumn3.HeaderText = "codigo";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.Visible = false;
		this.codigoundMed.DataPropertyName = "codUnidadMedida";
		this.codigoundMed.HeaderText = "codigoundMed";
		this.codigoundMed.Name = "codigoundMed";
		this.codigoundMed.ReadOnly = true;
		this.codigoundMed.Visible = false;
		this.unidad1.DataPropertyName = "descripcion";
		this.unidad1.HeaderText = "Unidad";
		this.unidad1.Name = "unidad1";
		this.unidad1.ReadOnly = true;
		this.unidad1.Width = 150;
		this.Factor1.DataPropertyName = "factor";
		this.Factor1.HeaderText = "Factor";
		this.Factor1.Name = "Factor1";
		this.Factor1.ReadOnly = true;
		this.Factor1.Visible = false;
		this.codUndEqui.DataPropertyName = "codUndEqui";
		this.codUndEqui.HeaderText = "codUndEqui";
		this.codUndEqui.Name = "codUndEqui";
		this.codUndEqui.ReadOnly = true;
		this.codUndEqui.Visible = false;
		this.equivalente.DataPropertyName = "equivalente";
		this.equivalente.HeaderText = "Equivalente";
		this.equivalente.Name = "equivalente";
		this.equivalente.ReadOnly = true;
		this.equivalente.Visible = false;
		this.precios1.DataPropertyName = "Precio";
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.precios1.DefaultCellStyle = dataGridViewCellStyle1;
		this.precios1.HeaderText = "Precio con IGV";
		this.precios1.Name = "precios1";
		this.codTipo.DataPropertyName = "codTipo";
		this.codTipo.HeaderText = "codTipo";
		this.codTipo.Name = "codTipo";
		this.codTipo.ReadOnly = true;
		this.codTipo.Visible = false;
		this.tipo.DataPropertyName = "tip";
		this.tipo.HeaderText = "Tipo Precio";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Width = 120;
		this.tpStockAlmacenes.Controls.Add(this.dgvAlmacenes);
		this.tpStockAlmacenes.Location = new System.Drawing.Point(4, 22);
		this.tpStockAlmacenes.Name = "tpStockAlmacenes";
		this.tpStockAlmacenes.Size = new System.Drawing.Size(988, 107);
		this.tpStockAlmacenes.TabIndex = 4;
		this.tpStockAlmacenes.Text = "Stock Almacenes";
		this.tpStockAlmacenes.UseVisualStyleBackColor = true;
		this.dgvAlmacenes.AllowUserToAddRows = false;
		this.dgvAlmacenes.AllowUserToDeleteRows = false;
		this.dgvAlmacenes.AllowUserToResizeRows = false;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvAlmacenes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgvAlmacenes.Columns.AddRange(this.almacen, this.dataGridViewTextBoxColumn2, this.entregar, this.disponible, this.recibir, this.futuro, this.minimo, this.maximo, this.reposicion);
		this.dgvAlmacenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvAlmacenes.Location = new System.Drawing.Point(0, 0);
		this.dgvAlmacenes.Name = "dgvAlmacenes";
		this.dgvAlmacenes.ReadOnly = true;
		this.dgvAlmacenes.RowHeadersVisible = false;
		this.dgvAlmacenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvAlmacenes.Size = new System.Drawing.Size(988, 107);
		this.dgvAlmacenes.TabIndex = 2;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.almacen.Width = 200;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "stockactual";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridViewTextBoxColumn2.HeaderText = "Stock Actual";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Width = 80;
		this.entregar.DataPropertyName = "entregar";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.entregar.DefaultCellStyle = dataGridViewCellStyle4;
		this.entregar.HeaderText = "P. Entregar";
		this.entregar.Name = "entregar";
		this.entregar.ReadOnly = true;
		this.entregar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.entregar.Visible = false;
		this.entregar.Width = 80;
		this.disponible.DataPropertyName = "stockdisponible";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.disponible.DefaultCellStyle = dataGridViewCellStyle5;
		this.disponible.HeaderText = "Stock Disp.";
		this.disponible.Name = "disponible";
		this.disponible.ReadOnly = true;
		this.disponible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.disponible.Width = 80;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.recibir.DefaultCellStyle = dataGridViewCellStyle6;
		this.recibir.HeaderText = "P. Recibir";
		this.recibir.Name = "recibir";
		this.recibir.ReadOnly = true;
		this.recibir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.recibir.Visible = false;
		this.recibir.Width = 80;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.futuro.DefaultCellStyle = dataGridViewCellStyle7;
		this.futuro.HeaderText = "Stock Futuro";
		this.futuro.Name = "futuro";
		this.futuro.ReadOnly = true;
		this.futuro.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.futuro.Visible = false;
		this.futuro.Width = 80;
		this.minimo.DataPropertyName = "stockminimo";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.minimo.DefaultCellStyle = dataGridViewCellStyle8;
		this.minimo.HeaderText = "Stock Minimo";
		this.minimo.Name = "minimo";
		this.minimo.ReadOnly = true;
		this.minimo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.maximo.DataPropertyName = "stockmaximo";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.maximo.DefaultCellStyle = dataGridViewCellStyle9;
		this.maximo.HeaderText = "Stock Maximo";
		this.maximo.Name = "maximo";
		this.maximo.ReadOnly = true;
		this.maximo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.reposicion.DataPropertyName = "stockreposicion";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.reposicion.DefaultCellStyle = dataGridViewCellStyle10;
		this.reposicion.HeaderText = "Stock Reposicion";
		this.reposicion.Name = "reposicion";
		this.reposicion.ReadOnly = true;
		this.reposicion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.reposicion.Visible = false;
		this.tpProveedores.Controls.Add(this.dgvProxProducto);
		this.tpProveedores.Location = new System.Drawing.Point(4, 22);
		this.tpProveedores.Name = "tpProveedores";
		this.tpProveedores.Size = new System.Drawing.Size(988, 107);
		this.tpProveedores.TabIndex = 3;
		this.tpProveedores.Text = "Proveedores";
		this.tpProveedores.UseVisualStyleBackColor = true;
		this.dgvProxProducto.AllowUserToAddRows = false;
		this.dgvProxProducto.AllowUserToDeleteRows = false;
		this.dgvProxProducto.AllowUserToResizeColumns = false;
		this.dgvProxProducto.AllowUserToResizeRows = false;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvProxProducto.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
		this.dgvProxProducto.Columns.AddRange(this.codigo, this.dataGridViewTextBoxColumn1, this.precio, this.dscto1, this.dscto2, this.dscto3, this.pneto);
		this.dgvProxProducto.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvProxProducto.Location = new System.Drawing.Point(0, 0);
		this.dgvProxProducto.Name = "dgvProxProducto";
		this.dgvProxProducto.ReadOnly = true;
		this.dgvProxProducto.RowHeadersVisible = false;
		this.dgvProxProducto.Size = new System.Drawing.Size(988, 107);
		this.dgvProxProducto.TabIndex = 1;
		this.codigo.DataPropertyName = "codProveedor";
		this.codigo.HeaderText = "Cod. Prov.";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codigo.Width = 60;
		this.dataGridViewTextBoxColumn1.DataPropertyName = "razonsocial";
		this.dataGridViewTextBoxColumn1.HeaderText = "Proveedor";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 200;
		this.precio.DataPropertyName = "precioofrecido";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle12.Format = "N2";
		this.precio.DefaultCellStyle = dataGridViewCellStyle12;
		this.precio.HeaderText = "Precio";
		this.precio.Name = "precio";
		this.precio.ReadOnly = true;
		this.precio.Width = 65;
		this.dscto1.DataPropertyName = "dscto1";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle13.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle13;
		this.dscto1.HeaderText = "Dscto. 1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Visible = false;
		this.dscto1.Width = 60;
		this.dscto2.DataPropertyName = "dscto2";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle14;
		this.dscto2.HeaderText = "Dscto. 2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Visible = false;
		this.dscto2.Width = 60;
		this.dscto3.DataPropertyName = "dscto3";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle15;
		this.dscto3.HeaderText = "Dscto. 3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Visible = false;
		this.dscto3.Width = 60;
		this.pneto.DataPropertyName = "precio";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		this.pneto.DefaultCellStyle = dataGridViewCellStyle16;
		this.pneto.HeaderText = "P. Neto";
		this.pneto.Name = "pneto";
		this.pneto.ReadOnly = true;
		this.pneto.Width = 65;
		this.tabPage3.Controls.Add(this.dgvNotas);
		this.tabPage3.Location = new System.Drawing.Point(4, 22);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(988, 107);
		this.tabPage3.TabIndex = 2;
		this.tabPage3.Text = "Notas";
		this.tabPage3.UseVisualStyleBackColor = true;
		this.dgvNotas.AllowUserToAddRows = false;
		this.dgvNotas.AllowUserToDeleteRows = false;
		this.dgvNotas.AllowUserToResizeColumns = false;
		this.dgvNotas.AllowUserToResizeRows = false;
		this.dgvNotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvNotas.Columns.AddRange(this.codnotaproducto, this.usuario, this.nota);
		this.dgvNotas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvNotas.Enabled = false;
		this.dgvNotas.Location = new System.Drawing.Point(3, 3);
		this.dgvNotas.Name = "dgvNotas";
		this.dgvNotas.ReadOnly = true;
		this.dgvNotas.RowHeadersVisible = false;
		this.dgvNotas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNotas.Size = new System.Drawing.Size(982, 101);
		this.dgvNotas.TabIndex = 5;
		this.codnotaproducto.DataPropertyName = "codNota";
		this.codnotaproducto.HeaderText = "Codigo";
		this.codnotaproducto.Name = "codnotaproducto";
		this.codnotaproducto.ReadOnly = true;
		this.codnotaproducto.Visible = false;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Usuario";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.usuario.Width = 200;
		this.nota.DataPropertyName = "nota";
		this.nota.HeaderText = "Nota";
		this.nota.Name = "nota";
		this.nota.ReadOnly = true;
		this.nota.Width = 600;
		this.tabEdicion.BackColor = System.Drawing.Color.Gainsboro;
		this.tabEdicion.Controls.Add(this.btnTexto);
		this.tabEdicion.Controls.Add(this.btnCombo);
		this.tabEdicion.Controls.Add(this.lblTexto);
		this.tabEdicion.Controls.Add(this.lblCombo);
		this.tabEdicion.Controls.Add(this.txtValor);
		this.tabEdicion.Controls.Add(this.cmbValor);
		this.tabEdicion.Location = new System.Drawing.Point(4, 22);
		this.tabEdicion.Name = "tabEdicion";
		this.tabEdicion.Padding = new System.Windows.Forms.Padding(3);
		this.tabEdicion.Size = new System.Drawing.Size(988, 107);
		this.tabEdicion.TabIndex = 5;
		this.tabEdicion.Text = "Edicion";
		this.btnTexto.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnTexto.Location = new System.Drawing.Point(594, 50);
		this.btnTexto.Name = "btnTexto";
		this.btnTexto.RootElement.ControlBounds = new System.Drawing.Rectangle(594, 50, 110, 24);
		this.btnTexto.Size = new System.Drawing.Size(120, 36);
		this.btnTexto.TabIndex = 5;
		this.btnTexto.Text = "Aceptar";
		this.btnTexto.ThemeName = "Material";
		this.btnTexto.Visible = false;
		this.btnTexto.Click += new System.EventHandler(btnTexto_Click);
		this.btnCombo.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnCombo.Location = new System.Drawing.Point(159, 50);
		this.btnCombo.Name = "btnCombo";
		this.btnCombo.RootElement.ControlBounds = new System.Drawing.Rectangle(159, 50, 110, 24);
		this.btnCombo.Size = new System.Drawing.Size(120, 36);
		this.btnCombo.TabIndex = 4;
		this.btnCombo.Text = "Aceptar";
		this.btnCombo.ThemeName = "Material";
		this.btnCombo.Visible = false;
		this.btnCombo.Click += new System.EventHandler(btnCombo_Click);
		this.lblTexto.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.lblTexto.Location = new System.Drawing.Point(473, 16);
		this.lblTexto.Name = "lblTexto";
		this.lblTexto.RootElement.ControlBounds = new System.Drawing.Rectangle(473, 16, 100, 18);
		this.lblTexto.Size = new System.Drawing.Size(14, 21);
		this.lblTexto.TabIndex = 3;
		this.lblTexto.Text = "x";
		this.lblTexto.ThemeName = "Material";
		this.lblTexto.Visible = false;
		this.lblCombo.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.lblCombo.Location = new System.Drawing.Point(31, 16);
		this.lblCombo.Name = "lblCombo";
		this.lblCombo.RootElement.ControlBounds = new System.Drawing.Rectangle(31, 16, 100, 18);
		this.lblCombo.Size = new System.Drawing.Size(14, 21);
		this.lblCombo.TabIndex = 2;
		this.lblCombo.Text = "x";
		this.lblCombo.ThemeName = "Material";
		this.lblCombo.Visible = false;
		this.txtValor.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.txtValor.Location = new System.Drawing.Point(558, 8);
		this.txtValor.Name = "txtValor";
		this.txtValor.RootElement.ControlBounds = new System.Drawing.Rectangle(558, 8, 100, 20);
		this.txtValor.RootElement.StretchVertically = true;
		this.txtValor.Size = new System.Drawing.Size(177, 36);
		this.txtValor.TabIndex = 1;
		this.txtValor.ThemeName = "Material";
		this.txtValor.Visible = false;
		this.cmbValor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbValor.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbValor.Location = new System.Drawing.Point(129, 7);
		this.cmbValor.Name = "cmbValor";
		this.cmbValor.RootElement.ControlBounds = new System.Drawing.Rectangle(129, 7, 125, 20);
		this.cmbValor.RootElement.StretchVertically = true;
		this.cmbValor.Size = new System.Drawing.Size(202, 36);
		this.cmbValor.TabIndex = 0;
		this.cmbValor.ThemeName = "Material";
		this.cmbValor.Visible = false;
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.label4);
		this.expandablePanel1.Controls.Add(this.label6);
		this.expandablePanel1.Controls.Add(this.label7);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.btnSalir);
		this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(603, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(771, -1);
		this.expandablePanel1.Name = "expandablePanel1";
		this.expandablePanel1.ShowFocusRectangle = true;
		this.expandablePanel1.Size = new System.Drawing.Size(231, 86);
		this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.Style.BackColor1.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.BackColor2.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarPopupBorder;
		this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
		this.expandablePanel1.Style.GradientAngle = 90;
		this.expandablePanel1.TabIndex = 18;
		this.expandablePanel1.TitleHeight = 0;
		this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
		this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.expandablePanel1.TitleStyle.GradientAngle = 90;
		this.expandablePanel1.TitleText = "Title Bar";
		this.expandablePanel1.Visible = false;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(10, 12);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 13);
		this.label4.TabIndex = 10;
		this.label4.Text = "Por :";
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.ForeColor = System.Drawing.Color.LightBlue;
		this.label6.Location = new System.Drawing.Point(186, 12);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(12, 13);
		this.label6.TabIndex = 7;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(45, 12);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(15, 13);
		this.label7.TabIndex = 6;
		this.label7.Text = "X";
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(13, 33);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 5;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.BackColor = System.Drawing.Color.Transparent;
		this.btnSalir.FlatAppearance.BorderSize = 0;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Location = new System.Drawing.Point(213, -5);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(1);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(18, 22);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "x";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.dgvprods.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvprods.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvprods.EnableCustomDrawing = true;
		this.dgvprods.Location = new System.Drawing.Point(0, 82);
		this.dgvprods.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
		this.dgvprods.MasterTemplate.AllowAddNewRow = false;
		this.dgvprods.MasterTemplate.AllowEditRow = false;
		this.dgvprods.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codproducto";
		gridViewTextBoxColumn1.HeaderText = "codproducto";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codproducto";
		gridViewTextBoxColumn2.FieldName = "coduniversal";
		gridViewTextBoxColumn2.HeaderText = "coduniversal";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "coduniversal";
		gridViewTextBoxColumn3.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn3.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn3.FieldName = "referencia";
		gridViewTextBoxColumn3.HeaderText = "Referencia";
		gridViewTextBoxColumn3.Name = "referencia";
		gridViewTextBoxColumn3.Width = 53;
		gridViewTextBoxColumn4.FieldName = "CUPC";
		gridViewTextBoxColumn4.HeaderText = "CUPC";
		gridViewTextBoxColumn4.Name = "CUPC";
		gridViewTextBoxColumn4.Width = 53;
		gridViewTextBoxColumn5.FieldName = "CUPV";
		gridViewTextBoxColumn5.HeaderText = "CUPV";
		gridViewTextBoxColumn5.Name = "CUPV";
		gridViewTextBoxColumn5.Width = 53;
		gridViewTextBoxColumn6.FieldName = "descripcion";
		gridViewTextBoxColumn6.HeaderText = "Descripcion";
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "descripcion";
		gridViewTextBoxColumn6.Width = 153;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "codmarca";
		gridViewTextBoxColumn7.HeaderText = "codmarca";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "codmarca";
		gridViewTextBoxColumn7.Width = 52;
		gridViewTextBoxColumn8.FieldName = "marcadesc";
		gridViewTextBoxColumn8.HeaderText = "Marca";
		gridViewTextBoxColumn8.Name = "marcadesc";
		gridViewTextBoxColumn8.Width = 64;
		gridViewTextBoxColumn9.FieldName = "unidad";
		gridViewTextBoxColumn9.HeaderText = "Unidad Base";
		gridViewTextBoxColumn9.Name = "unidad";
		gridViewTextBoxColumn9.Width = 47;
		gridViewTextBoxColumn10.FieldName = "control";
		gridViewTextBoxColumn10.HeaderText = "Control";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "control";
		gridViewTextBoxColumn11.FieldName = "comision";
		gridViewTextBoxColumn11.HeaderText = "Comision";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "comision";
		gridViewTextBoxColumn12.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn12.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn12.FieldName = "preciocatalogo";
		gridViewTextBoxColumn12.HeaderText = "Precio Catalogo";
		gridViewTextBoxColumn12.Name = "preciocatalogo";
		gridViewTextBoxColumn12.Width = 44;
		gridViewTextBoxColumn13.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn13.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn13.FieldName = "preciocompra";
		gridViewTextBoxColumn13.HeaderText = "Precio Compra";
		gridViewTextBoxColumn13.Name = "preciocompra";
		gridViewTextBoxColumn13.Width = 46;
		gridViewTextBoxColumn14.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn14.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn14.FieldName = "precioventa";
		gridViewTextBoxColumn14.HeaderText = "Precio Venta";
		gridViewTextBoxColumn14.Name = "precioventa";
		gridViewTextBoxColumn14.Width = 46;
		gridViewTextBoxColumn15.FieldName = "codTip_precVenta";
		gridViewTextBoxColumn15.HeaderText = "Tipo Prec. Venta";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "tipPrecioVenta";
		gridViewTextBoxColumn15.Width = 36;
		gridViewTextBoxColumn16.FieldName = "Tipo_Precio_Venta";
		gridViewTextBoxColumn16.HeaderText = "Precio Venta(desc)";
		gridViewTextBoxColumn16.Name = "descPrecioVenta";
		gridViewTextBoxColumn16.Width = 51;
		gridViewTextBoxColumn17.FieldName = "codfamilia";
		gridViewTextBoxColumn17.HeaderText = "codfamilia";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "codfamilia";
		gridViewTextBoxColumn17.Width = 40;
		gridViewTextBoxColumn18.FieldName = "familiadesc";
		gridViewTextBoxColumn18.HeaderText = "Familia";
		gridViewTextBoxColumn18.Name = "familiadesc";
		gridViewTextBoxColumn18.Width = 48;
		gridViewTextBoxColumn19.FieldName = "codlinea";
		gridViewTextBoxColumn19.HeaderText = "codlinea";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "codlinea";
		gridViewTextBoxColumn19.Width = 53;
		gridViewTextBoxColumn20.FieldName = "lineadesc";
		gridViewTextBoxColumn20.HeaderText = "Linea";
		gridViewTextBoxColumn20.Name = "lineadesc";
		gridViewTextBoxColumn20.Width = 64;
		gridViewTextBoxColumn21.FieldName = "codmodelo";
		gridViewTextBoxColumn21.HeaderText = "codmodelo";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "codmodelo";
		gridViewTextBoxColumn21.Width = 43;
		gridViewTextBoxColumn22.FieldName = "modelodesc";
		gridViewTextBoxColumn22.HeaderText = "Modelo";
		gridViewTextBoxColumn22.Name = "modelodesc";
		gridViewTextBoxColumn22.Width = 52;
		gridViewTextBoxColumn23.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn23.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn23.FieldName = "stockminimo";
		gridViewTextBoxColumn23.HeaderText = "Stock Minimo";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "stockminimo";
		gridViewTextBoxColumn23.Width = 134;
		gridViewTextBoxColumn24.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn24.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn24.FieldName = "stockmaximo";
		gridViewTextBoxColumn24.HeaderText = "Stock Maximo";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "stockmaximo";
		gridViewTextBoxColumn24.Width = 48;
		gridViewTextBoxColumn25.FieldName = "capmax";
		gridViewTextBoxColumn25.HeaderText = "Cap. Maxima";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "capmax";
		gridViewTextBoxColumn25.Width = 47;
		gridViewTextBoxColumn26.FieldName = "ubicacion";
		gridViewTextBoxColumn26.HeaderText = "Rzn. Social";
		gridViewTextBoxColumn26.Name = "ubicacion";
		gridViewTextBoxColumn26.Width = 64;
		gridViewTextBoxColumn27.FieldName = "estado";
		gridViewTextBoxColumn27.HeaderText = "codestado";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "estado";
		gridViewTextBoxColumn27.Width = 47;
		gridViewTextBoxColumn28.FieldName = "nombreestado";
		gridViewTextBoxColumn28.HeaderText = "Estado";
		gridViewTextBoxColumn28.Name = "nombreestado";
		gridViewTextBoxColumn28.Width = 56;
		gridViewTextBoxColumn29.FieldName = "categorizacion";
		gridViewTextBoxColumn29.HeaderText = "Categorizacón";
		gridViewTextBoxColumn29.Multiline = true;
		gridViewTextBoxColumn29.Name = "categorizacion";
		gridViewTextBoxColumn29.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn29.Width = 54;
		gridViewTextBoxColumn30.FieldName = "descontinuado";
		gridViewTextBoxColumn30.HeaderText = "Estado Logistico";
		gridViewTextBoxColumn30.Multiline = true;
		gridViewTextBoxColumn30.Name = "descontinuado";
		gridViewTextBoxColumn30.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn30.Width = 54;
		this.dgvprods.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30);
		this.dgvprods.MasterTemplate.EnableFiltering = true;
		this.dgvprods.MasterTemplate.EnableGrouping = false;
		this.dgvprods.MasterTemplate.EnablePaging = true;
		this.dgvprods.MasterTemplate.PageSize = 50;
		this.dgvprods.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.dgvprods.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvprods.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvprods.Name = "dgvprods";
		this.dgvprods.ReadOnly = true;
		this.dgvprods.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 82, 240, 150);
		this.dgvprods.RootElement.FocusBorderColor = System.Drawing.Color.Yellow;
		this.dgvprods.Size = new System.Drawing.Size(1002, 216);
		this.dgvprods.TabIndex = 22;
		this.dgvprods.ThemeName = "Material";
		this.dgvprods.RowPaint += new Telerik.WinControls.UI.GridViewRowPaintEventHandler(dgvprods_RowPaint);
		this.dgvprods.ViewRowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(dgvprods_ViewRowFormatting);
		this.dgvprods.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvprods_CellFormatting);
		this.dgvprods.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvprods_ViewCellFormatting);
		this.dgvprods.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvprods_CellClick);
		this.dgvprods.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvprods_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoSize = true;
		base.ClientSize = new System.Drawing.Size(1002, 450);
		base.Controls.Add(this.dgvprods);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCatalogo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Catalogo de Productos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmProductos_Load);
		base.Shown += new System.EventHandler(frmProductos_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmProductos_KeyDown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.lblEdicion).EndInit();
		this.tabControl.ResumeLayout(false);
		this.tpDetalleProducto.ResumeLayout(false);
		this.tpDetalleProducto.PerformLayout();
		this.tpPrecios.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.tpStockAlmacenes.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).EndInit();
		this.tpProveedores.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvProxProducto).EndInit();
		this.tabPage3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvNotas).EndInit();
		this.tabEdicion.ResumeLayout(false);
		this.tabEdicion.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnTexto).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnCombo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblTexto).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblCombo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbValor).EndInit();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvprods.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvprods).EndInit();
		base.ResumeLayout(false);
	}
}
