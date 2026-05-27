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
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmStockAlmacenesPendiente : Office2007Form
{
	private clsAdmProducto admPro = new clsAdmProducto();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsProducto pro = new clsProducto();

	private clsAdmAlmacen admalm = new clsAdmAlmacen();

	private List<string> todosAlmacenes = null;

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmDespacho admDespacho = new clsAdmDespacho();

	private Color[] colores_columnas = new Color[5]
	{
		Color.FromArgb(155, 201, 235),
		Color.FromArgb(102, 242, 158),
		Color.FromArgb(196, 219, 103),
		Color.FromArgb(245, 205, 120),
		Color.FromArgb(235, 177, 164)
	};

	private int i = 0;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private TextBox txtFiltro;

	private Label label3;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private ImageList imageList1;

	private RadButton btnActualizar;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private MaterialTheme materialTheme1;

	private RadButton btnReporte;

	private RadGridView dgvDetalle;

	private Label label4;

	private RadAutoCompleteBox racAlmacenes;

	private RadCheckedDropDownList cmbAlmacenes;

	private GroupBox groupBox3;

	private RadGridView rgvDocumentos;

	private RadButton btnexportar;

	public frmStockAlmacenesPendiente()
	{
		InitializeComponent();
	}

	private void CargaStock()
	{
		try
		{
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (GridViewDataColumn col in dgvDetalle.Columns)
				{
					if (col.Index > 7)
					{
						dgvDetalle.FilterDescriptors.Remove(col.Name);
					}
				}
			}
			data.DataSource = null;
			dgvDetalle.DataSource = data;
			data.DataSource = admPro.MuestraStockAlmacenesPendientes();
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
			if (co.Index > 7)
			{
				co.Width = 80;
				co.WrapText = true;
			}
		}
	}

	private void frmStockAlmacenesPendiente_Load(object sender, EventArgs e)
	{
		try
		{
			cargaAlmacenes();
			CargaStock();
			label2.Text = "Producto:";
			label3.Text = "producto";
			if (dgvDetalle.RowCount > 0)
			{
				cmbAlmacenes_ItemCheckedChanged(sender, new RadCheckedListDataItemEventArgs(null));
				dgvDetalle.GridViewElement.PagingPanelElement.NumericButtonsCount = 10;
				pintarColumnas();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void frmStockAlmacenesPendiente_Shown(object sender, EventArgs e)
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

	public void reiniciarScrollBarHorizontal()
	{
		RadScrollBarElement hScroll = dgvDetalle.TableElement.HScrollBar;
		hScroll.Value = 0;
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		reiniciarScrollBarHorizontal();
		CargaStock();
		ajustarColumnas();
		if (dgvDetalle.RowCount > 0)
		{
			cmbAlmacenes_ItemCheckedChanged(sender, new RadCheckedListDataItemEventArgs(null));
			pintarColumnas();
		}
		Cursor = Cursors.Default;
	}

	private void CargaListaEquivalencias()
	{
	}

	private void dgvDetalle_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.Row == dgvDetalle.MasterView.TableHeaderRow && e.ColumnIndex > -1)
		{
			label2.Text = e.Column.HeaderText + ":";
			label3.Text = e.Column.Name;
		}
		else if (dgvDetalle.RowCount > 0 && e.RowIndex != -1)
		{
			pro.CodProducto = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
			procedimientoParaListarDocumentos(e);
		}
	}

	private void procedimientoParaListarDocumentos(GridViewCellEventArgs e)
	{
		try
		{
			rgvDocumentos.DataSource = null;
			if (e.Column.Index <= 7 || !e.Column.HeaderText.Contains("SEPARADO"))
			{
				return;
			}
			List<string> almacenesAMostrar = Enumerable.Select<string, string>(todosAlmacenes.AsEnumerable(), (Func<string, string>)((string x) => x.Substring(8))).ToList();
			DataTable table = (DataTable)cmbAlmacenes.DataSource;
			int codProducto = Convert.ToInt32(e.Row.Cells["codproducto"].Value);
			int codUnidad = Convert.ToInt32(e.Row.Cells["codUnidad"].Value);
			int iFindAlmacenMostrar = -1;
			foreach (string alm in almacenesAMostrar)
			{
				if (e.Column.HeaderText.Contains(alm))
				{
					iFindAlmacenMostrar = almacenesAMostrar.IndexOf(alm);
					break;
				}
			}
			if (iFindAlmacenMostrar != -1)
			{
				int iCodAlmacen = Enumerable.Select<DataRow, int>(Enumerable.Where<DataRow>(table.Rows.Cast<DataRow>(), (Func<DataRow, bool>)((DataRow x) => x.Field<object>("nombre").ToString() == todosAlmacenes[iFindAlmacenMostrar])), (Func<DataRow, int>)((DataRow x) => Convert.ToInt32(x.Field<object>("codAlmacen")))).First();
				DataTable data = admForm.cargaDocumentosDeProductosADespachar2(codProducto, codUnidad, iCodAlmacen, iCodAlmacen);
				rgvDocumentos.DataSource = data;
			}
			else
			{
				MessageBox.Show("No se pudo identificar el almacen", "Error - Almacen No Identificado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error - Listado DOcumentos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
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
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvDetalle_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		if (e.ColumnIndex <= 7)
		{
			e.CellElement.BorderLeftWidth = 1f;
			e.CellElement.BorderRightWidth = 1f;
			e.CellElement.BorderTopWidth = 1f;
			e.CellElement.BorderBottomWidth = 1f;
			if (e.Column.IsPinned)
			{
				e.CellElement.DrawFill = false;
			}
			else
			{
				e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
			}
		}
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

	public void cargaAlmacenes()
	{
		DataTable data = admalm.ListaAlmacen2();
		if (data != null)
		{
			todosAlmacenes = (from x in data.AsEnumerable()
				select x.Field<object>("nombre").ToString()).ToList();
			DataRow fila = data.NewRow();
			fila.ItemArray = new object[2] { 0, "TODOS" };
			data.Rows.InsertAt(fila, 0);
		}
		racAlmacenes.AutoCompleteDataSource = data;
		racAlmacenes.AutoCompleteDisplayMember = "nombre";
		racAlmacenes.AutoCompleteValueMember = "codAlmacen";
		cmbAlmacenes.DataSource = data;
		cmbAlmacenes.ValueMember = "codAlmacen";
		cmbAlmacenes.DisplayMember = "nombre";
		DataTable almaceneslog = admalm.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		List<string> codalmaceneslog = new List<string>();
		if (almaceneslog != null && almaceneslog.Rows.Count > 0)
		{
			almaceneslog.Rows.RemoveAt(0);
			codalmaceneslog = Enumerable.Select<DataRow, string>(almaceneslog.Rows.Cast<DataRow>(), (Func<DataRow, string>)((DataRow x) => x.Field<object>("cod").ToString())).ToList();
		}
		foreach (RadCheckedListDataItem item in cmbAlmacenes.Items)
		{
			if (codalmaceneslog.Contains(item.Value.ToString()))
			{
				item.Checked = true;
			}
		}
	}

	private void racAlmacenes_TokenValidating(object sender, TokenValidatingEventArgs e)
	{
	}

	private void cmbAlmacenes_ItemCheckedChanged(object sender, RadCheckedListDataItemEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			reiniciarScrollBarHorizontal();
			List<string> val = Enumerable.Select<RadCheckedListDataItem, string>(cmbAlmacenes.CheckedItems.AsEnumerable(), (Func<RadCheckedListDataItem, string>)((RadCheckedListDataItem x) => x.Value.ToString())).ToList();
			List<string> almacenesAMostrar = getAlmacenesAMostrar();
			foreach (GridViewDataColumn col in dgvDetalle.Columns)
			{
				if (col.Index > 7)
				{
					dgvDetalle.FilterDescriptors.Remove(col.Name);
					col.IsVisible = false;
					foreach (string alm in almacenesAMostrar)
					{
						if (col.HeaderText.Contains(alm))
						{
							col.IsVisible = true;
						}
					}
				}
				if (col.Index == dgvDetalle.Columns.Count - 1)
				{
					col.IsVisible = true;
				}
			}
			Cursor = Cursors.Default;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error");
		}
	}

	private List<string> getAlmacenesAMostrar()
	{
		List<string> val2 = Enumerable.Select<RadCheckedListDataItem, string>(cmbAlmacenes.CheckedItems.AsEnumerable(), (Func<RadCheckedListDataItem, string>)((RadCheckedListDataItem x) => x.DisplayValue.ToString())).ToList();
		List<string> val3 = Enumerable.Where<string>(val2.AsEnumerable(), (Func<string, bool>)((string x) => x == "TODOS")).ToList();
		List<string> almacenesAMostrar = new List<string>();
		string mostrar = "Mostrar: ";
		if (val3.Count > 0)
		{
			return Enumerable.Select<string, string>(todosAlmacenes.AsEnumerable(), (Func<string, string>)((string x) => x.Substring(8))).ToList();
		}
		return Enumerable.Select<string, string>(val2.AsEnumerable(), (Func<string, string>)((string x) => x.Substring(8))).ToList();
	}

	public void pintarColumnas()
	{
		List<string> almacenesAMostrar = Enumerable.Select<string, string>(todosAlmacenes.AsEnumerable(), (Func<string, string>)((string x) => x.Substring(8))).ToList();
		int pintar = 0;
		foreach (GridViewDataColumn col in dgvDetalle.Columns)
		{
			if (col.Index <= 7)
			{
				continue;
			}
			foreach (string alm in almacenesAMostrar)
			{
				if (col.HeaderText.Contains(alm))
				{
					ConditionalFormattingObject c1 = new ConditionalFormattingObject("color applied to entire column", ConditionTypes.DoesNotContain, "teimaginasquetengaesto", "yesto", applyToRow: false);
					c1.RowBackColor = colores_columnas[i];
					c1.CellBackColor = colores_columnas[i];
					ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Equal, "", "", applyToRow: false);
					c2.RowBackColor = colores_columnas[i];
					c2.CellBackColor = colores_columnas[i];
					ConditionalFormattingObject c3 = new ConditionalFormattingObject("color a vacio", ConditionTypes.Contains, null, "", applyToRow: false);
					c3.RowBackColor = colores_columnas[i];
					c3.CellBackColor = colores_columnas[i];
					col.ConditionalFormattingObjectList.Add(c1);
					col.ConditionalFormattingObjectList.Add(c2);
					col.ConditionalFormattingObjectList.Add(c3);
					pintar++;
				}
				if (pintar == 4)
				{
					i++;
					if (i == 5)
					{
						i = 0;
					}
					pintar = 0;
				}
			}
		}
	}

	private void calcularTotalEnBaseAColumnasVisibles()
	{
		try
		{
			List<string> almacenesAMostrar = getAlmacenesAMostrar();
			string columnasumar = " DISPONIBLE";
			string columnatotal = "TOTAL KARDEX";
			foreach (GridViewRowInfo fila in dgvDetalle.Rows)
			{
				double sumatotal = 0.0;
				foreach (string item in almacenesAMostrar)
				{
					sumatotal += Convert.ToDouble(fila.Cells[item + columnasumar].Value);
				}
				fila.Cells[columnatotal].Value = $"{sumatotal:0.00}";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void rgvDocumentos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}
		try
		{
			string[] almacenes = e.Row.Cells["colCodAlmacenes"].Value.ToString().Split(',');
			bool ModoEdicion = false;
			if (almacenes.Contains(frmLogin.iCodAlmacen.ToString()))
			{
				ModoEdicion = true;
			}
			string name = e.Column.Name;
			string text = name;
			if (!(text == "colDescReqAlm"))
			{
				if (text == "colDescripDocumento")
				{
					int codDesp = Convert.ToInt32(e.Row.Cells["colCodDocumento"].Value);
					clsDespacho despacho = admDespacho.cargaDespacho(codDesp);
					if (despacho != null)
					{
						frmDespacho frm = new frmDespacho();
						frm.MdiParent = base.MdiParent;
						frm.Dock = DockStyle.Fill;
						frm.WindowState = FormWindowState.Maximized;
						frm.codDespacho = despacho.CodDespacho;
						frm.Proceso = ((!ModoEdicion) ? 2 : ((despacho.Estado == 1 && despacho.CodEstado == 18) ? 1 : 2));
						frm.Show();
					}
				}
			}
			else
			{
				int codReqAlm = Convert.ToInt32(e.Row.Cells["colCodReqAlm"].Value);
				frmReqAlmacen form = new frmReqAlmacen();
				form.MdiParent = base.MdiParent;
				form.codRequerimientoAlmacen = codReqAlm;
				form.Proceso = (ModoEdicion ? 1 : 2);
				form.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnexportar_Click(object sender, EventArgs e)
	{
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		if (true)
		{
			frmImportaData frm = new frmImportaData();
			frm.Proceso = 4;
			frm.StartPosition = FormStartPosition.CenterScreen;
			frm.ShowDialog();
		}
		else
		{
			MessageBox.Show("No tiene permiso para generar importacion.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmStockAlmacenesPendiente));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadCheckedDropDownList();
		this.label4 = new System.Windows.Forms.Label();
		this.racAlmacenes = new Telerik.WinControls.UI.RadAutoCompleteBox();
		this.btnReporte = new Telerik.WinControls.UI.RadButton();
		this.btnActualizar = new Telerik.WinControls.UI.RadButton();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.rgvDocumentos = new Telerik.WinControls.UI.RadGridView();
		this.btnexportar = new Telerik.WinControls.UI.RadButton();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.racAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnReporte).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnActualizar).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnexportar).BeginInit();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(6, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 20);
		this.label1.TabIndex = 1;
		this.label1.Text = "Filtro Por";
		this.label1.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(71, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(18, 20);
		this.label2.TabIndex = 2;
		this.label2.Text = "X";
		this.label2.Visible = false;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(101, 26);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(399, 20);
		this.txtFiltro.TabIndex = 3;
		this.txtFiltro.Visible = false;
		this.txtFiltro.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(415, 52);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.groupBox1.Controls.Add(this.btnexportar);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.racAlmacenes);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1179, 86);
		this.groupBox1.TabIndex = 5;
		this.groupBox1.TabStop = false;
		this.cmbAlmacenes.AutoScroll = true;
		this.cmbAlmacenes.AutoSize = false;
		this.cmbAlmacenes.Cursor = System.Windows.Forms.Cursors.Hand;
		this.cmbAlmacenes.Location = new System.Drawing.Point(95, 12);
		this.cmbAlmacenes.Multiline = true;
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(417, 62);
		this.cmbAlmacenes.TabIndex = 41;
		this.cmbAlmacenes.ThemeName = "ControlDefault";
		this.cmbAlmacenes.ItemCheckedChanged += new Telerik.WinControls.UI.RadCheckedListDataItemEventHandler(cmbAlmacenes_ItemCheckedChanged);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Arial Narrow", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(8, 19);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(81, 20);
		this.label4.TabIndex = 40;
		this.label4.Text = "Almacenes:";
		this.racAlmacenes.AcceptsReturn = true;
		this.racAlmacenes.AcceptsTab = true;
		this.racAlmacenes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.racAlmacenes.Location = new System.Drawing.Point(1035, 101);
		this.racAlmacenes.Multiline = true;
		this.racAlmacenes.Name = "racAlmacenes";
		this.racAlmacenes.ShowClearButton = true;
		this.racAlmacenes.Size = new System.Drawing.Size(259, 62);
		this.racAlmacenes.TabIndex = 39;
		this.racAlmacenes.Visible = false;
		this.racAlmacenes.TokenValidating += new Telerik.WinControls.UI.TokenValidatingEventHandler(racAlmacenes_TokenValidating);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.printer;
		this.btnReporte.Location = new System.Drawing.Point(941, 33);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(127, 36);
		this.btnReporte.TabIndex = 6;
		this.btnReporte.Text = "Imprimir";
		this.btnReporte.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.ThemeName = "Material";
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizar.Location = new System.Drawing.Point(800, 33);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(135, 36);
		this.btnActualizar.TabIndex = 5;
		this.btnActualizar.Text = "Actualizar";
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
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 86);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1179, 378);
		this.groupBox2.TabIndex = 6;
		this.groupBox2.TabStop = false;
		this.dgvDetalle.AutoScroll = true;
		this.dgvDetalle.AutoSizeRows = true;
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.dgvDetalle.MasterTemplate.AllowDragToGroup = false;
		this.dgvDetalle.MasterTemplate.AllowEditRow = false;
		gridViewTextBoxColumn1.FieldName = "codProducto";
		gridViewTextBoxColumn1.HeaderText = "codProducto";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codProducto";
		gridViewTextBoxColumn1.Width = 131;
		gridViewTextBoxColumn2.FieldName = "referencia";
		gridViewTextBoxColumn2.HeaderText = "Referencia";
		gridViewTextBoxColumn2.IsPinned = true;
		gridViewTextBoxColumn2.Name = "referencia";
		gridViewTextBoxColumn2.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "producto";
		gridViewTextBoxColumn3.HeaderText = "Producto";
		gridViewTextBoxColumn3.IsPinned = true;
		gridViewTextBoxColumn3.Multiline = true;
		gridViewTextBoxColumn3.Name = "producto";
		gridViewTextBoxColumn3.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn3.Width = 250;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.FieldName = "unidad";
		gridViewTextBoxColumn4.HeaderText = "Unidad";
		gridViewTextBoxColumn4.IsPinned = true;
		gridViewTextBoxColumn4.Name = "colunidad";
		gridViewTextBoxColumn4.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn4.Width = 120;
		gridViewTextBoxColumn5.FieldName = "estado";
		gridViewTextBoxColumn5.HeaderText = "Estado";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "estado";
		gridViewTextBoxColumn5.Width = 153;
		gridViewTextBoxColumn6.FieldName = "marca";
		gridViewTextBoxColumn6.HeaderText = "Marca";
		gridViewTextBoxColumn6.IsPinned = true;
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "marca";
		gridViewTextBoxColumn6.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
		gridViewTextBoxColumn6.Width = 101;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "familia";
		gridViewTextBoxColumn7.HeaderText = "Familia";
		gridViewTextBoxColumn7.Multiline = true;
		gridViewTextBoxColumn7.Name = "familia";
		gridViewTextBoxColumn7.Width = 101;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "proveedor";
		gridViewTextBoxColumn8.HeaderText = "Proveedor";
		gridViewTextBoxColumn8.Multiline = true;
		gridViewTextBoxColumn8.Name = "proveedor";
		gridViewTextBoxColumn8.Width = 125;
		gridViewTextBoxColumn8.WrapText = true;
		this.dgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.dgvDetalle.MasterTemplate.EnableFiltering = true;
		this.dgvDetalle.MasterTemplate.EnableGrouping = false;
		this.dgvDetalle.MasterTemplate.EnablePaging = true;
		this.dgvDetalle.MasterTemplate.MultiSelect = true;
		this.dgvDetalle.MasterTemplate.PageSize = 100;
		this.dgvDetalle.MasterTemplate.ShowHeaderCellButtons = true;
		this.dgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.ShowCellErrors = false;
		this.dgvDetalle.ShowGroupPanel = false;
		this.dgvDetalle.ShowHeaderCellButtons = true;
		this.dgvDetalle.ShowRowErrors = false;
		this.dgvDetalle.Size = new System.Drawing.Size(1173, 359);
		this.dgvDetalle.TabIndex = 3;
		this.dgvDetalle.ThemeName = "ControlDefault";
		this.dgvDetalle.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(dgvDetalle_RowFormatting);
		this.dgvDetalle.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvDetalle_CellFormatting);
		this.dgvDetalle.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvDetalle_CellClick);
		this.groupBox3.Controls.Add(this.rgvDocumentos);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox3.Location = new System.Drawing.Point(0, 464);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1179, 168);
		this.groupBox3.TabIndex = 7;
		this.groupBox3.TabStop = false;
		this.rgvDocumentos.AutoScroll = true;
		this.rgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDocumentos.Location = new System.Drawing.Point(3, 16);
		this.rgvDocumentos.MasterTemplate.AllowAddNewRow = false;
		this.rgvDocumentos.MasterTemplate.AllowColumnReorder = false;
		this.rgvDocumentos.MasterTemplate.AllowDeleteRow = false;
		this.rgvDocumentos.MasterTemplate.AllowDragToGroup = false;
		this.rgvDocumentos.MasterTemplate.AllowEditRow = false;
		this.rgvDocumentos.MasterTemplate.AllowRowResize = false;
		this.rgvDocumentos.MasterTemplate.AutoGenerateColumns = false;
		this.rgvDocumentos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn9.FieldName = "codDocumento";
		gridViewTextBoxColumn9.HeaderText = "codDocumento";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "colCodDocumento";
		gridViewTextBoxColumn9.Width = 5;
		gridViewTextBoxColumn10.FieldName = "codTipoDocumento";
		gridViewTextBoxColumn10.HeaderText = "codTipoDocumento";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "colCodTipoDocumento";
		gridViewTextBoxColumn10.Width = 5;
		gridViewTextBoxColumn11.FieldName = "descripDocumento";
		gridViewTextBoxColumn11.HeaderText = "Despacho";
		gridViewTextBoxColumn11.Name = "colDescripDocumento";
		gridViewTextBoxColumn11.Width = 131;
		gridViewTextBoxColumn12.FieldName = "codReqAlm";
		gridViewTextBoxColumn12.HeaderText = "codReqAlm";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "colCodReqAlm";
		gridViewTextBoxColumn12.Width = 5;
		gridViewTextBoxColumn13.FieldName = "descripReqAlm";
		gridViewTextBoxColumn13.HeaderText = "Requerimiento";
		gridViewTextBoxColumn13.Name = "colDescReqAlm";
		gridViewTextBoxColumn13.Width = 123;
		gridViewTextBoxColumn14.FieldName = "codDocRelac";
		gridViewTextBoxColumn14.HeaderText = "codDocRelac";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "colCodDocRelac";
		gridViewTextBoxColumn14.Width = 5;
		gridViewTextBoxColumn15.FieldName = "descripDocRelac";
		gridViewTextBoxColumn15.HeaderText = "Doc. Relacionado";
		gridViewTextBoxColumn15.Name = "colDescDocRelac";
		gridViewTextBoxColumn15.Width = 116;
		gridViewTextBoxColumn16.FieldName = "fechaDocumento";
		gridViewTextBoxColumn16.HeaderText = "Fecha Doc. Relacionado";
		gridViewTextBoxColumn16.Name = "colFechaDocumento";
		gridViewTextBoxColumn16.Width = 140;
		gridViewTextBoxColumn17.FieldName = "nombreCliente";
		gridViewTextBoxColumn17.HeaderText = "Cliente";
		gridViewTextBoxColumn17.Name = "colNombreCliente";
		gridViewTextBoxColumn17.Width = 305;
		gridViewTextBoxColumn18.FieldName = "fechaRegistro";
		gridViewTextBoxColumn18.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn18.Name = "colFechaRegistro";
		gridViewTextBoxColumn18.Width = 118;
		gridViewTextBoxColumn19.FieldName = "descripAlmacen";
		gridViewTextBoxColumn19.HeaderText = "Almacen";
		gridViewTextBoxColumn19.Name = "colDescripAlmacen";
		gridViewTextBoxColumn19.Width = 112;
		gridViewTextBoxColumn19.WrapText = true;
		gridViewTextBoxColumn20.FieldName = "ctdadProducto";
		gridViewTextBoxColumn20.HeaderText = "Cantidad De Producto";
		gridViewTextBoxColumn20.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn20.Name = "colCtdadProducto";
		gridViewTextBoxColumn20.Width = 134;
		gridViewTextBoxColumn20.WrapText = true;
		gridViewTextBoxColumn21.FieldName = "codAlmacenes";
		gridViewTextBoxColumn21.HeaderText = "codAlmacenes";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "colCodAlmacenes";
		gridViewTextBoxColumn21.Width = 5;
		this.rgvDocumentos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21);
		this.rgvDocumentos.MasterTemplate.EnableFiltering = true;
		this.rgvDocumentos.MasterTemplate.EnableGrouping = false;
		this.rgvDocumentos.MasterTemplate.ShowFilteringRow = false;
		this.rgvDocumentos.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvDocumentos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDocumentos.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvDocumentos.Name = "rgvDocumentos";
		this.rgvDocumentos.ReadOnly = true;
		this.rgvDocumentos.ShowHeaderCellButtons = true;
		this.rgvDocumentos.Size = new System.Drawing.Size(1173, 149);
		this.rgvDocumentos.TabIndex = 1;
		this.rgvDocumentos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDocumentos_CellDoubleClick);
		this.btnexportar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnexportar.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnexportar.Location = new System.Drawing.Point(620, 33);
		this.btnexportar.Name = "btnexportar";
		this.btnexportar.Size = new System.Drawing.Size(174, 36);
		this.btnexportar.TabIndex = 6;
		this.btnexportar.Text = "Actualizar Stock";
		this.btnexportar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnexportar.ThemeName = "Material";
		this.btnexportar.Click += new System.EventHandler(btnexportar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1179, 632);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmStockAlmacenesPendiente";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Control de Almacenes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmStockAlmacenesPendiente_Load);
		base.Shown += new System.EventHandler(frmStockAlmacenesPendiente_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.racAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnReporte).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnActualizar).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDocumentos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnexportar).EndInit();
		base.ResumeLayout(false);
	}
}
