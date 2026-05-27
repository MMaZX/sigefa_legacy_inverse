using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmPromedioProductosVendidos : Office2007Form
{
	private clsAdmProducto admPro = new clsAdmProducto();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsProducto pro = new clsProducto();

	private DataTable aux_data;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private int codigoProductoAux;

	private int cantidadDiasAux;

	private bool detener_proceso = false;

	private int selectMetodoCalculo;

	private int selectFamilia;

	private int selectLinea;

	private int selectGrupo;

	private int selectMarca;

	private int seg = 0;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private TextBox txtFiltro;

	private Label label3;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private DataGridView dgvDetalle_1;

	private ImageList imageList1;

	private RadButton btnActualizar;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private MaterialTheme materialTheme1;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn producto;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn marca;

	private DataGridViewTextBoxColumn familia;

	private DataGridViewTextBoxColumn Proveedor;

	private RadButton btnReporte;

	private RadGridView dgvDetalle;

	private Label label6;

	private Label label5;

	private Label label4;

	private DateTimePicker dtpFechaFinal;

	private DateTimePicker dtpFechaInicio;

	private PictureBox pBCargando;

	private BackgroundWorker bgWCargaGrid;

	private System.Timers.Timer tiempoCarga;

	private Label lblTiempoCarga;

	private ComboBox cbLinea;

	private Label label9;

	private ComboBox cbGrupo;

	private Label label8;

	private ComboBox cbFamilia;

	private Label label7;

	private Label label10;

	private ComboBox cbMarca;

	private GroupBox groupBox4;

	public TextBox txtArticulo;

	public TextBox txtUnArt;

	private RadioButton rbArt;

	private RadioButton rbTodosArt;

	private Label label11;

	private ComboBox cbMetodoCalculo;

	private Label lblCodProd;

	private RadButton btnExportarExcel;

	public frmPromedioProductosVendidos()
	{
		InitializeComponent();
	}

	private void CargarPromedioDeProductosVendidos()
	{
		try
		{
			data.DataSource = null;
			dgvDetalle.DataSource = data;
			data.DataSource = aux_data;
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDetalle.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void ajustarColumnas()
	{
		foreach (GridViewDataColumn co in dgvDetalle.Columns)
		{
			switch (co.Index)
			{
			case 1:
				co.Width = 87;
				break;
			case 2:
				co.Width = 209;
				break;
			case 4:
				co.Width = 133;
				break;
			case 5:
				co.Width = 129;
				break;
			case 6:
				co.Width = 116;
				break;
			case 7:
				co.Width = 121;
				break;
			case 11:
				co.Width = 100;
				break;
			default:
				co.Width = 119;
				break;
			}
		}
	}

	private void frmPromedioProductosVendidos_Load(object sender, EventArgs e)
	{
		try
		{
			dtpFechaInicio.Value = dtpFechaFinal.Value.AddDays(-7.0);
			label2.Text = "producto";
			label3.Text = "producto";
			cargarComboFamilia();
			cargarComboLinea(-1);
			cargarComboGrupo(-1);
			cargarComboMarca();
			selectFamilia = -1;
			selectLinea = -1;
			selectGrupo = -1;
			selectMarca = -1;
			cbMetodoCalculo.SelectedIndex = 0;
			codigoProductoAux = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void frmPromedioProductosVendidos_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
		ajustarColumnas();
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
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
								queries.Add($"[{label2.Text}] LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"[{label2.Text}] LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"[{label2.Text}] LIKE '%{filterCod}%'";
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
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32((dtpFechaFinal.Value - dtpFechaInicio.Value).Days + 1) > 0)
			{
				cantidadDiasAux = Convert.ToInt32((dtpFechaFinal.Value - dtpFechaInicio.Value).Days + 1);
				if (rbTodosArt.Checked)
				{
					codigoProductoAux = -1;
				}
				else
				{
					if (!validaCajadeTextoProducto())
					{
						MessageBox.Show("Debe Seleccionar Un Articulo", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					codigoProductoAux = Convert.ToInt32(lblCodProd.Text);
				}
				if (!bgWCargaGrid.IsBusy)
				{
					cambiarCursorA(Cursors.WaitCursor);
					mostrarCargando();
					seg = 0;
					lblTiempoCarga.Text = seg + " seg.";
					bgWCargaGrid.RunWorkerAsync();
				}
				else
				{
					MessageBox.Show("Se está cargando el listado", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("El rango de fechas es incorrecto", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void muestraDatosEjecutar()
	{
		string aux = "";
		aux = aux + "\nFamilia: " + selectFamilia;
		aux = aux + "\nLinea: " + selectLinea;
		aux = aux + "\nGrupo: " + selectGrupo;
		aux = aux + "\nMarca: " + selectMarca;
		aux = aux + "\nMetodo: " + selectMetodoCalculo;
		aux = aux + "\nProducto: " + codigoProductoAux;
		aux = aux + "\ncantidad: " + cantidadDiasAux;
		aux += "\nFin";
		MessageBox.Show(aux);
		MessageBox.Show("Ejecutando...");
	}

	private bool validaCajadeTextoProducto()
	{
		bool band = false;
		if (int.TryParse(lblCodProd.Text, out var _))
		{
			band = true;
		}
		return band;
	}

	private void mostrarCargando()
	{
		int x_aux = base.Width / 2 - pBCargando.Width / 2;
		int y_aux = base.Height / 2 - pBCargando.Height / 2;
		Point p = new Point(x_aux, y_aux);
		pBCargando.Location = p;
		pBCargando.Visible = true;
	}

	private void cambiarCursorA(Cursor aux)
	{
		Cursor = aux;
		dgvDetalle.Cursor = aux;
		pBCargando.Cursor = aux;
	}

	private void CargaListaEquivalencias()
	{
	}

	private void dgvDetalle_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.Row == dgvDetalle.MasterView.TableHeaderRow && e.ColumnIndex > -1)
		{
			label2.Text = e.Column.Name;
			label3.Text = e.Column.Name;
		}
		else if (dgvDetalle.RowCount > 0 && e.RowIndex != -1)
		{
			pro.CodProducto = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
			CargaListaEquivalencias();
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(dgvDetalle);
		spreadStreamExport.ExportVisualSettings = true;
		spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
		string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx";
		if (File.Exists(ruta))
		{
			File.Delete(ruta);
		}
		spreadStreamExport.RunExport(AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx", new SpreadStreamExportRenderer());
		FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx");
		if (fi.Exists)
		{
			Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\exportStockAlmacenes.xlsx");
		}
	}

	private void dgvDetalle_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		e.CellElement.BorderLeftWidth = 1f;
		e.CellElement.BorderRightWidth = 1f;
		e.CellElement.BorderTopWidth = 1f;
		e.CellElement.BorderBottomWidth = 1f;
	}

	private void dgvDetalle_RowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (e.RowElement is GridTableHeaderRowElement)
		{
			e.RowElement.RowInfo.Height = 30;
		}
		if (Convert.ToInt32(e.RowElement.RowInfo.Cells["estado"].Value) == 0)
		{
			e.RowElement.DrawFill = true;
			e.RowElement.GradientStyle = GradientStyles.Solid;
			e.RowElement.BackColor = Color.LightCoral;
		}
		else
		{
			e.RowElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
			e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
			e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
		}
	}

	private void label6_Click(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void bgWCargaGrid_DoWork(object sender, DoWorkEventArgs e)
	{
		tiempoCarga.Start();
		if (selectMetodoCalculo == 0)
		{
			aux_data = admPro.GetPromedioProductosVendidos(dtpFechaInicio.Value, dtpFechaFinal.Value, selectFamilia, selectLinea, selectGrupo, selectMarca, codigoProductoAux, cantidadDiasAux);
		}
		else
		{
			aux_data = admPro.GetTotalizadoProductosVendidos(dtpFechaInicio.Value, dtpFechaFinal.Value, selectFamilia, selectLinea, selectGrupo, selectMarca, codigoProductoAux, cantidadDiasAux);
		}
	}

	private void bgWCargaGrid_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		CargarPromedioDeProductosVendidos();
		ajustarColumnas();
		Activate();
		tiempoCarga.Stop();
		pBCargando.Visible = false;
		cambiarCursorA(Cursors.Default);
		btnActualizar.Text = "Cargar";
		btnActualizar.Image = imageList1.Images[7];
		detener_proceso = false;
		MessageBox.Show("Se listaron " + dgvDetalle.Rows.Count + " productos.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
	}

	private void tiempoCarga_Elapsed(object sender, ElapsedEventArgs e)
	{
		seg++;
		lblTiempoCarga.Text = seg + " seg.";
	}

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 20;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				CargaProducto(frm.GetCodigoProducto());
			}
		}
		else
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			try
			{
				if (txtUnArt.Text != "")
				{
					int codPro = AdmPro.GetCodProducto_xDescripcion(txtUnArt.Text);
					if (codPro != 0)
					{
						CargaProducto(codPro);
					}
				}
				else
				{
					MessageBox.Show("Faltan datos..!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}

	private void CargaProducto(int Codigo)
	{
		pro = AdmPro.CargaProductoDetalle(Codigo, frmLogin.iCodAlmacen, 2, 0, 0);
		if (pro != null)
		{
			txtUnArt.Text = pro.Referencia;
			txtArticulo.Text = pro.Descripcion;
			lblCodProd.Text = pro.CodProducto.ToString();
			codigoProductoAux = pro.CodProducto;
		}
		else
		{
			MessageBox.Show("Producto no encontrado", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void cargarComboFamilia()
	{
		clsAdmFamilia admFamilia = new clsAdmFamilia();
		cbFamilia.ValueMember = "codFamilia";
		cbFamilia.DisplayMember = "descripcion";
		DataTable aux = admFamilia.MuestraFamilias();
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
		cbFamilia.DataSource = aux;
		cbFamilia.SelectedValue = -1;
		cbFamilia.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cbFamilia.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cbFamilia.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void cargarComboLinea(int CodigoFamilia)
	{
		clsAdmLinea admLinea = new clsAdmLinea();
		cbLinea.ValueMember = "codLinea";
		cbLinea.DisplayMember = "descripcion";
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
		cbLinea.DataSource = aux;
		cbLinea.SelectedValue = -1;
		cbLinea.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cbLinea.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cbLinea.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void cargarComboGrupo(int CodigoLinea)
	{
		clsAdmGrupo admGrupo = new clsAdmGrupo();
		cbGrupo.ValueMember = "codGrupo";
		cbGrupo.DisplayMember = "descripcion";
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
		cbGrupo.DataSource = aux;
		cbGrupo.SelectedValue = -1;
		cbGrupo.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cbGrupo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cbGrupo.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void cargarComboMarca()
	{
		clsAdmMarca admMarca = new clsAdmMarca();
		cbMarca.ValueMember = "codMarca";
		cbMarca.DisplayMember = "descripcion";
		DataTable aux = admMarca.MuestraMarcas();
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
		cbMarca.DataSource = aux;
		cbMarca.SelectedValue = -1;
		cbMarca.AutoCompleteCustomSource = CargaAutoComplete(aux);
		cbMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		cbMarca.AutoCompleteSource = AutoCompleteSource.CustomSource;
	}

	private void cbFamilia_SelectedIndexChanged(object sender, EventArgs e)
	{
		cargarComboLinea(Convert.ToInt32(cbFamilia.SelectedValue));
		selectFamilia = Convert.ToInt32(cbFamilia.SelectedValue);
	}

	private void cbLinea_SelectedIndexChanged(object sender, EventArgs e)
	{
		cargarComboGrupo(Convert.ToInt32(cbLinea.SelectedValue));
		selectLinea = Convert.ToInt32(cbLinea.SelectedValue);
	}

	public static AutoCompleteStringCollection CargaAutoComplete(DataTable dt)
	{
		AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
		foreach (DataRow row in dt.Rows)
		{
			stringCol.Add(Convert.ToString(row["descripcion"]));
		}
		return stringCol;
	}

	private void txtUnArt_TextChanged(object sender, EventArgs e)
	{
	}

	private void cbMetodoCalculo_SelectedIndexChanged(object sender, EventArgs e)
	{
		selectMetodoCalculo = cbMetodoCalculo.SelectedIndex;
	}

	private void cbGrupo_SelectedIndexChanged(object sender, EventArgs e)
	{
		selectGrupo = Convert.ToInt32(cbGrupo.SelectedValue);
	}

	private void cbMarca_SelectedIndexChanged(object sender, EventArgs e)
	{
		selectMarca = Convert.ToInt32(cbMarca.SelectedValue);
	}

	private void rbTodosArt_CheckedChanged(object sender, EventArgs e)
	{
		if (rbTodosArt.Checked)
		{
			codigoProductoAux = -1;
		}
	}

	private void cbMarca_SelectionChangeCommitted(object sender, EventArgs e)
	{
		selectMarca = Convert.ToInt32(cbMarca.SelectedValue ?? ((object)(-1)));
	}

	private void cbFamilia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cargarComboLinea(Convert.ToInt32(cbFamilia.SelectedValue ?? ((object)(-1))));
		selectFamilia = Convert.ToInt32(cbFamilia.SelectedValue ?? ((object)(-1)));
	}

	private void cbLinea_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cargarComboGrupo(Convert.ToInt32(cbLinea.SelectedValue ?? ((object)(-1))));
		selectLinea = Convert.ToInt32(cbLinea.SelectedValue ?? ((object)(-1)));
	}

	private void cbGrupo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		selectGrupo = Convert.ToInt32(cbGrupo.SelectedValue ?? ((object)(-1)));
	}

	private void rbArt_CheckedChanged(object sender, EventArgs e)
	{
		if (rbArt.Checked)
		{
			cargarComboFamilia();
			cargarComboLinea(-1);
			cargarComboGrupo(-1);
			cargarComboMarca();
			selectFamilia = -1;
			selectLinea = -1;
			selectGrupo = -1;
			selectMarca = -1;
		}
	}

	private void btnExportarExcel_Click(object sender, EventArgs e)
	{
		try
		{
			GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(dgvDetalle);
			spreadStreamExport.ExportVisualSettings = true;
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\exportProductosPromediosVendidos.xlsx";
			if (File.Exists(ruta))
			{
				File.Delete(ruta);
			}
			spreadStreamExport.RunExport(AppDomain.CurrentDomain.BaseDirectory + "\\exportProductosPromediosVendidos.xlsx", new SpreadStreamExportRenderer());
			FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\exportProductosPromediosVendidos.xlsx");
			if (fi.Exists)
			{
				Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\exportProductosPromediosVendidos.xlsx");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPromedioProductosVendidos));
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnExportarExcel = new Telerik.WinControls.UI.RadButton();
		this.label11 = new System.Windows.Forms.Label();
		this.cbMetodoCalculo = new System.Windows.Forms.ComboBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.lblCodProd = new System.Windows.Forms.Label();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.btnReporte = new Telerik.WinControls.UI.RadButton();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.cbLinea = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cbGrupo = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.cbFamilia = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.cbMarca = new System.Windows.Forms.ComboBox();
		this.lblTiempoCarga = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
		this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
		this.btnActualizar = new Telerik.WinControls.UI.RadButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.pBCargando = new System.Windows.Forms.PictureBox();
		this.dgvDetalle_1 = new System.Windows.Forms.DataGridView();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.bgWCargaGrid = new System.ComponentModel.BackgroundWorker();
		this.tiempoCarga = new System.Timers.Timer();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnExportarExcel).BeginInit();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnReporte).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnActualizar).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle.MasterTemplate).BeginInit();
		this.dgvDetalle.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pBCargando).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle_1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tiempoCarga).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(18, 90);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 20);
		this.label1.TabIndex = 1;
		this.label1.Text = "Filtro Por : ";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(94, 90);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(18, 20);
		this.label2.TabIndex = 2;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(22, 113);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(222, 20);
		this.txtFiltro.TabIndex = 3;
		this.txtFiltro.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(250, 116);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.groupBox1.Controls.Add(this.btnExportarExcel);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.cbMetodoCalculo);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.cbLinea);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.cbGrupo);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.cbFamilia);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.cbMarca);
		this.groupBox1.Controls.Add(this.lblTiempoCarga);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.dtpFechaFinal);
		this.groupBox1.Controls.Add(this.dtpFechaInicio);
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1370, 138);
		this.groupBox1.TabIndex = 5;
		this.groupBox1.TabStop = false;
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.btnExportarExcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f);
		this.btnExportarExcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnExportarExcel.Location = new System.Drawing.Point(1179, 87);
		this.btnExportarExcel.Name = "btnExportarExcel";
		this.btnExportarExcel.Size = new System.Drawing.Size(183, 45);
		this.btnExportarExcel.TabIndex = 6;
		this.btnExportarExcel.Text = "Exportar Listado";
		this.btnExportarExcel.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExportarExcel.TextWrap = true;
		this.btnExportarExcel.ThemeName = "Material";
		this.btnExportarExcel.Click += new System.EventHandler(btnExportarExcel_Click);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(404, 90);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(99, 13);
		this.label11.TabIndex = 73;
		this.label11.Text = "Metodo de Calculo:";
		this.cbMetodoCalculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbMetodoCalculo.FormattingEnabled = true;
		this.cbMetodoCalculo.Items.AddRange(new object[2] { "Promedio Mensual", "Total de Unidades" });
		this.cbMetodoCalculo.Location = new System.Drawing.Point(407, 106);
		this.cbMetodoCalculo.Name = "cbMetodoCalculo";
		this.cbMetodoCalculo.Size = new System.Drawing.Size(170, 21);
		this.cbMetodoCalculo.TabIndex = 74;
		this.cbMetodoCalculo.SelectedIndexChanged += new System.EventHandler(cbMetodoCalculo_SelectedIndexChanged);
		this.groupBox4.Controls.Add(this.lblCodProd);
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.btnReporte);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(680, 13);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox4.Size = new System.Drawing.Size(545, 82);
		this.groupBox4.TabIndex = 72;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "PRODUCTO";
		this.lblCodProd.AutoSize = true;
		this.lblCodProd.Location = new System.Drawing.Point(160, 51);
		this.lblCodProd.Name = "lblCodProd";
		this.lblCodProd.Size = new System.Drawing.Size(61, 13);
		this.lblCodProd.TabIndex = 64;
		this.lblCodProd.Text = "codigoProd";
		this.lblCodProd.Visible = false;
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(209, 25);
		this.txtArticulo.Margin = new System.Windows.Forms.Padding(4);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(321, 20);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Location = new System.Drawing.Point(122, 25);
		this.txtUnArt.Margin = new System.Windows.Forms.Padding(4);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(79, 20);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.TextChanged += new System.EventHandler(txtUnArt_TextChanged);
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.printer;
		this.btnReporte.Location = new System.Drawing.Point(546, 79);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(135, 36);
		this.btnReporte.TabIndex = 6;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.ThemeName = "Material";
		this.btnReporte.Visible = false;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.rbArt.AutoSize = true;
		this.rbArt.BackColor = System.Drawing.Color.Transparent;
		this.rbArt.Location = new System.Drawing.Point(8, 28);
		this.rbArt.Margin = new System.Windows.Forms.Padding(4);
		this.rbArt.Name = "rbArt";
		this.rbArt.Size = new System.Drawing.Size(79, 17);
		this.rbArt.TabIndex = 57;
		this.rbArt.Text = "Un Artículo";
		this.rbArt.UseVisualStyleBackColor = false;
		this.rbArt.CheckedChanged += new System.EventHandler(rbArt_CheckedChanged);
		this.rbTodosArt.AutoSize = true;
		this.rbTodosArt.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosArt.Checked = true;
		this.rbTodosArt.Location = new System.Drawing.Point(8, 52);
		this.rbTodosArt.Margin = new System.Windows.Forms.Padding(4);
		this.rbTodosArt.Name = "rbTodosArt";
		this.rbTodosArt.Size = new System.Drawing.Size(115, 17);
		this.rbTodosArt.TabIndex = 54;
		this.rbTodosArt.TabStop = true;
		this.rbTodosArt.Text = "Todos los artículos";
		this.rbTodosArt.UseVisualStyleBackColor = false;
		this.rbTodosArt.CheckedChanged += new System.EventHandler(rbTodosArt_CheckedChanged);
		this.cbLinea.FormattingEnabled = true;
		this.cbLinea.Items.AddRange(new object[1] { "Todos" });
		this.cbLinea.Location = new System.Drawing.Point(459, 21);
		this.cbLinea.Name = "cbLinea";
		this.cbLinea.Size = new System.Drawing.Size(214, 21);
		this.cbLinea.TabIndex = 20;
		this.cbLinea.SelectedIndexChanged += new System.EventHandler(cbLinea_SelectedIndexChanged);
		this.cbLinea.SelectionChangeCommitted += new System.EventHandler(cbLinea_SelectionChangeCommitted);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(456, 5);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(36, 13);
		this.label9.TabIndex = 19;
		this.label9.Text = "Linea:";
		this.cbGrupo.FormattingEnabled = true;
		this.cbGrupo.Items.AddRange(new object[1] { "Todos" });
		this.cbGrupo.Location = new System.Drawing.Point(230, 61);
		this.cbGrupo.Name = "cbGrupo";
		this.cbGrupo.Size = new System.Drawing.Size(214, 21);
		this.cbGrupo.TabIndex = 16;
		this.cbGrupo.SelectedIndexChanged += new System.EventHandler(cbGrupo_SelectedIndexChanged);
		this.cbGrupo.SelectionChangeCommitted += new System.EventHandler(cbGrupo_SelectionChangeCommitted);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(227, 45);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(39, 13);
		this.label8.TabIndex = 15;
		this.label8.Text = "Grupo:";
		this.cbFamilia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
		this.cbFamilia.FormattingEnabled = true;
		this.cbFamilia.Items.AddRange(new object[1] { "Todos" });
		this.cbFamilia.Location = new System.Drawing.Point(230, 21);
		this.cbFamilia.Name = "cbFamilia";
		this.cbFamilia.Size = new System.Drawing.Size(214, 21);
		this.cbFamilia.TabIndex = 14;
		this.cbFamilia.SelectedIndexChanged += new System.EventHandler(cbFamilia_SelectedIndexChanged);
		this.cbFamilia.SelectionChangeCommitted += new System.EventHandler(cbFamilia_SelectionChangeCommitted);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(227, 5);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(42, 13);
		this.label7.TabIndex = 13;
		this.label7.Text = "Familia:";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(456, 45);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(40, 13);
		this.label10.TabIndex = 17;
		this.label10.Text = "Marca:";
		this.cbMarca.FormattingEnabled = true;
		this.cbMarca.Items.AddRange(new object[1] { "Todos" });
		this.cbMarca.Location = new System.Drawing.Point(459, 61);
		this.cbMarca.Name = "cbMarca";
		this.cbMarca.Size = new System.Drawing.Size(214, 21);
		this.cbMarca.TabIndex = 18;
		this.cbMarca.SelectedIndexChanged += new System.EventHandler(cbMarca_SelectedIndexChanged);
		this.cbMarca.SelectionChangeCommitted += new System.EventHandler(cbMarca_SelectionChangeCommitted);
		this.lblTiempoCarga.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTiempoCarga.Location = new System.Drawing.Point(1232, 9);
		this.lblTiempoCarga.Name = "lblTiempoCarga";
		this.lblTiempoCarga.Size = new System.Drawing.Size(123, 23);
		this.lblTiempoCarga.TabIndex = 12;
		this.lblTiempoCarga.Text = "0 seg.";
		this.lblTiempoCarga.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(126, 28);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(38, 13);
		this.label6.TabIndex = 11;
		this.label6.Text = "Hasta:";
		this.label6.Click += new System.EventHandler(label6_Click);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(6, 28);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(41, 13);
		this.label5.TabIndex = 10;
		this.label5.Text = "Desde:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 9);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(153, 13);
		this.label4.TabIndex = 9;
		this.label4.Text = "Rango de Fecha Para Calculo:";
		this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaFinal.Location = new System.Drawing.Point(129, 47);
		this.dtpFechaFinal.Name = "dtpFechaFinal";
		this.dtpFechaFinal.Size = new System.Drawing.Size(84, 20);
		this.dtpFechaFinal.TabIndex = 8;
		this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaInicio.Location = new System.Drawing.Point(9, 47);
		this.dtpFechaInicio.Name = "dtpFechaInicio";
		this.dtpFechaInicio.Size = new System.Drawing.Size(84, 20);
		this.dtpFechaInicio.TabIndex = 7;
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.ImageIndex = 7;
		this.btnActualizar.ImageList = this.imageList1;
		this.btnActualizar.Location = new System.Drawing.Point(1239, 46);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(123, 36);
		this.btnActualizar.TabIndex = 5;
		this.btnActualizar.Text = "Cargar";
		this.btnActualizar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnActualizar.ThemeName = "Material";
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "x-button.png");
		this.imageList1.Images.SetKeyName(7, "ganancia.png");
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Controls.Add(this.dgvDetalle_1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 138);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1370, 398);
		this.groupBox2.TabIndex = 6;
		this.groupBox2.TabStop = false;
		this.dgvDetalle.Controls.Add(this.pBCargando);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.dgvDetalle.MasterTemplate.AllowDragToGroup = false;
		this.dgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codProducto";
		gridViewTextBoxColumn1.HeaderText = "codProducto";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codProducto";
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.ExcelExportFormatString = "0.00";
		gridViewTextBoxColumn2.ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Custom;
		gridViewTextBoxColumn2.FieldName = "referencia";
		gridViewTextBoxColumn2.HeaderText = "Referencia";
		gridViewTextBoxColumn2.Name = "referencia";
		gridViewTextBoxColumn2.Width = 110;
		gridViewTextBoxColumn3.FieldName = "producto";
		gridViewTextBoxColumn3.HeaderText = "Producto";
		gridViewTextBoxColumn3.Multiline = true;
		gridViewTextBoxColumn3.Name = "producto";
		gridViewTextBoxColumn3.Width = 533;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.FieldName = "estado";
		gridViewTextBoxColumn4.HeaderText = "Estado";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "estado";
		gridViewTextBoxColumn4.Width = 153;
		gridViewTextBoxColumn5.FieldName = "unidad";
		gridViewTextBoxColumn5.HeaderText = "Unidad";
		gridViewTextBoxColumn5.Name = "colUnidad";
		gridViewTextBoxColumn5.Width = 49;
		gridViewTextBoxColumn6.FieldName = "marca";
		gridViewTextBoxColumn6.HeaderText = "Marca";
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "marca";
		gridViewTextBoxColumn6.Width = 238;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "familia";
		gridViewTextBoxColumn7.HeaderText = "Familia";
		gridViewTextBoxColumn7.Multiline = true;
		gridViewTextBoxColumn7.Name = "familia";
		gridViewTextBoxColumn7.Width = 115;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "linea";
		gridViewTextBoxColumn8.HeaderText = "Linea";
		gridViewTextBoxColumn8.Name = "linea";
		gridViewTextBoxColumn8.Width = 106;
		gridViewTextBoxColumn9.FieldName = "grupo";
		gridViewTextBoxColumn9.HeaderText = "Grupo";
		gridViewTextBoxColumn9.Name = "grupo";
		gridViewTextBoxColumn9.Width = 213;
		gridViewTextBoxColumn10.FieldName = "proveedor";
		gridViewTextBoxColumn10.HeaderText = "Proveedor";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Multiline = true;
		gridViewTextBoxColumn10.Name = "proveedor";
		gridViewTextBoxColumn10.Width = 100;
		gridViewTextBoxColumn10.WrapText = true;
		this.dgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10);
		this.dgvDetalle.MasterTemplate.EnableFiltering = true;
		this.dgvDetalle.MasterTemplate.EnableGrouping = false;
		this.dgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.dgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.ShowGroupPanel = false;
		this.dgvDetalle.ShowHeaderCellButtons = true;
		this.dgvDetalle.Size = new System.Drawing.Size(1364, 379);
		this.dgvDetalle.TabIndex = 3;
		this.dgvDetalle.ThemeName = "Material";
		this.dgvDetalle.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(dgvDetalle_RowFormatting);
		this.dgvDetalle.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvDetalle_CellFormatting);
		this.dgvDetalle.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvDetalle_CellClick);
		this.pBCargando.Image = (System.Drawing.Image)resources.GetObject("pBCargando.Image");
		this.pBCargando.Location = new System.Drawing.Point(530, 75);
		this.pBCargando.Name = "pBCargando";
		this.pBCargando.Size = new System.Drawing.Size(203, 194);
		this.pBCargando.TabIndex = 12;
		this.pBCargando.TabStop = false;
		this.pBCargando.Visible = false;
		this.dgvDetalle_1.AllowUserToAddRows = false;
		this.dgvDetalle_1.AllowUserToDeleteRows = false;
		this.dgvDetalle_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle_1.Columns.AddRange(this.codproducto, this.referencia, this.producto, this.estado, this.marca, this.familia, this.Proveedor);
		this.dgvDetalle_1.Location = new System.Drawing.Point(254, 219);
		this.dgvDetalle_1.Name = "dgvDetalle_1";
		this.dgvDetalle_1.ReadOnly = true;
		this.dgvDetalle_1.RowHeadersVisible = false;
		this.dgvDetalle_1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle_1.Size = new System.Drawing.Size(60, 80);
		this.dgvDetalle_1.TabIndex = 2;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.producto.DataPropertyName = "producto";
		this.producto.HeaderText = "Producto";
		this.producto.Name = "producto";
		this.producto.ReadOnly = true;
		this.producto.Width = 300;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.marca.DataPropertyName = "marca";
		this.marca.HeaderText = "Marca";
		this.marca.Name = "marca";
		this.marca.ReadOnly = true;
		this.familia.DataPropertyName = "familia";
		this.familia.HeaderText = "Familia";
		this.familia.Name = "familia";
		this.familia.ReadOnly = true;
		this.Proveedor.DataPropertyName = "proveedor";
		this.Proveedor.HeaderText = "Proveedor";
		this.Proveedor.Name = "Proveedor";
		this.Proveedor.ReadOnly = true;
		this.Proveedor.Width = 220;
		this.bgWCargaGrid.WorkerSupportsCancellation = true;
		this.bgWCargaGrid.DoWork += new System.ComponentModel.DoWorkEventHandler(bgWCargaGrid_DoWork);
		this.bgWCargaGrid.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(bgWCargaGrid_RunWorkerCompleted);
		this.tiempoCarga.Interval = 1000.0;
		this.tiempoCarga.SynchronizingObject = this;
		this.tiempoCarga.Elapsed += new System.Timers.ElapsedEventHandler(tiempoCarga_Elapsed);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1370, 536);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmPromedioProductosVendidos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Promedio de Productos Vendidos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPromedioProductosVendidos_Load);
		base.Shown += new System.EventHandler(frmPromedioProductosVendidos_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnExportarExcel).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnReporte).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnActualizar).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.dgvDetalle.ResumeLayout(false);
		this.dgvDetalle.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pBCargando).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle_1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tiempoCarga).EndInit();
		base.ResumeLayout(false);
	}
}
