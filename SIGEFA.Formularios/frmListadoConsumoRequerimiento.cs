using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmListadoConsumoRequerimiento : Form
{
	private clsAdmRequerimientoAlmacen admreq = new clsAdmRequerimientoAlmacen();

	private GridViewTemplate template = null;

	private clsUsuario usuario_click = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RadGridView rgvListadoProductosRequerimientos;

	private GridViewTemplate gvtRequerimientos;

	private Label label1;

	private ComboBox cmbTipoFecha;

	private Button btnActualizar;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnReporteDetallado;

	private Button btnReporteResumen;

	public frmListadoConsumoRequerimiento()
	{
		InitializeComponent();
	}

	private void frmListadoConsumoRequerimiento_Load(object sender, EventArgs e)
	{
		template = rgvListadoProductosRequerimientos.MasterTemplate.Templates[0];
		cmbTipoFecha.SelectedIndex = 0;
		cargaLista();
	}

	private void cargaLista()
	{
		DataSet data = admreq.ListadoConsumoRequerimiento(cmbTipoFecha.SelectedIndex, dtpDesde.Value, dtpHasta.Value);
		using (rgvListadoProductosRequerimientos.DeferRefresh())
		{
			rgvListadoProductosRequerimientos.MasterTemplate.DataSource = data.Tables[0];
			if (template == null)
			{
				MessageBox.Show("Ocurrio un error al listar, cierre y vuelva abrir el Listado", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			template.DataSource = data.Tables[1];
			rgvListadoProductosRequerimientos.MasterTemplate.Templates.Add(template);
			GridViewRelation relacion = new GridViewRelation();
			relacion.ChildTemplate = template;
			relacion.ParentTemplate = rgvListadoProductosRequerimientos.MasterTemplate;
			relacion.RelationName = "ProductosRequerimientos";
			relacion.ParentColumnNames.Add(rgvListadoProductosRequerimientos.MasterTemplate.Columns["colCodProducto"].Name);
			relacion.ChildColumnNames.Add(template.Columns["colCodProducto"].Name);
			rgvListadoProductosRequerimientos.Relations.Add(relacion);
		}
	}

	private void rgvListadoProductosRequerimientos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.ColumnIndex > -1 && e.RowIndex <= -1)
		{
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		try
		{
			cargaLista();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnReporteResumen_Click(object sender, EventArgs e)
	{
		try
		{
			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\ReporteResumenConsumoRequerimiento.xlsx"))
			{
				try
				{
					File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\ReporteResumenConsumoRequerimiento.xlsx");
				}
				catch (Exception)
				{
					MessageBox.Show("Ocurrió un error al eliminar archivo de ventas temporal, se recomienda cerrar el archivo en\n los equipos que se estén usando");
				}
			}
			GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(rgvListadoProductosRequerimientos);
			spreadStreamExport.ExportVisualSettings = true;
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			spreadStreamExport.RunExport(AppDomain.CurrentDomain.BaseDirectory + "\\ReporteResumenConsumoRequerimiento.xlsx", new SpreadStreamExportRenderer());
			FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\ReporteResumenConsumoRequerimiento.xlsx");
			if (fi.Exists)
			{
				Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\ReporteResumenConsumoRequerimiento.xlsx");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	private void btnReporteDetallado_Click(object sender, EventArgs e)
	{
		try
		{
			SLDocument sl = new SLDocument();
			int indFilaInicial = 1;
			sl.SetCellValue(indFilaInicial, 1, "Requerimiento");
			sl.SetCellValue(indFilaInicial, 2, "Estado");
			sl.SetCellValue(indFilaInicial, 3, "Fecha Registro");
			sl.SetCellValue(indFilaInicial, 4, "Referencia");
			sl.SetCellValue(indFilaInicial, 5, "Descripcion");
			sl.SetCellValue(indFilaInicial, 6, "Unidad");
			sl.SetCellValue(indFilaInicial, 7, "Cantidad Pedida");
			sl.SetCellValue(indFilaInicial, 8, "Cantidad Confirmada");
			int indFilaContenido = indFilaInicial;
			foreach (GridViewRowInfo fila in rgvListadoProductosRequerimientos.Rows)
			{
				if (fila.Index <= -1)
				{
					continue;
				}
				foreach (GridViewRowInfo filahija in fila.ChildRows)
				{
					indFilaContenido++;
					sl.SetCellValue(indFilaContenido, 1, filahija.Cells["colDescRequerimiento"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 2, filahija.Cells["colEstado"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 3, filahija.Cells["colfechaRegistro"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 4, fila.Cells["colReferencia"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 5, fila.Cells["colDescProducto"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 6, fila.Cells["colUnidadBase"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 7, filahija.Cells["colCtdadPedida"].Value.ToString());
					sl.SetCellValue(indFilaContenido, 8, filahija.Cells["colCtdadConfirmada"].Value.ToString());
				}
			}
			SLStyle estilo = sl.CreateStyle();
			estilo.SetFontColor(System.Drawing.Color.White);
			estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
			sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 8, estilo);
			SLStyle aux_style = sl.CreateStyle();
			asignarBordes(aux_style);
			sl.SetCellStyle(indFilaInicial, 1, indFilaContenido, 8, aux_style);
			sl.SetColumnWidth(1, 30.0);
			sl.SetColumnWidth(2, 42.0);
			sl.SetColumnWidth(3, 18.0);
			sl.SetColumnWidth(4, 18.0);
			sl.SetColumnWidth(5, 18.0);
			sl.SetColumnWidth(6, 26.0);
			sl.SetColumnWidth(7, 25.0);
			sl.SetColumnWidth(8, 25.0);
			SLStyle style = sl.CreateStyle();
			style.SetWrapText(IsWrapped: true);
			sl.SetColumnStyle(1, 2, style);
			sl.SetColumnStyle(6, style);
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
				MessageBox.Show(ex.Message, base.Name + " - Line 179");
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

	private string obtenerRutaParaGuardar(string namefile = "ReporteDetalladoConsumoRequerimiento")
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
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Listado de Consumo de Requerimientos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn35 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn36 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn37 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn38 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn39 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn40 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn41 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn42 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn43 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn44 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition5 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn45 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn46 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn47 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn48 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn49 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn50 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn51 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition6 = new Telerik.WinControls.UI.TableViewDefinition();
		this.gvtRequerimientos = new Telerik.WinControls.UI.GridViewTemplate();
		this.rgvListadoProductosRequerimientos = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbTipoFecha = new System.Windows.Forms.ComboBox();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnReporteResumen = new System.Windows.Forms.Button();
		this.btnReporteDetallado = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.gvtRequerimientos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoProductosRequerimientos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoProductosRequerimientos.MasterTemplate).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.gvtRequerimientos.AllowAddNewRow = false;
		this.gvtRequerimientos.AllowColumnReorder = false;
		this.gvtRequerimientos.AllowDeleteRow = false;
		this.gvtRequerimientos.AllowDragToGroup = false;
		this.gvtRequerimientos.AllowEditRow = false;
		this.gvtRequerimientos.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn35.FieldName = "codProducto";
		gridViewTextBoxColumn35.HeaderText = "codProducto";
		gridViewTextBoxColumn35.IsVisible = false;
		gridViewTextBoxColumn35.Name = "colCodProducto";
		gridViewTextBoxColumn36.FieldName = "id_req_almacen";
		gridViewTextBoxColumn36.HeaderText = "codRequerimiento";
		gridViewTextBoxColumn36.IsVisible = false;
		gridViewTextBoxColumn36.Name = "colCodRequerimiento";
		gridViewTextBoxColumn37.FieldName = "descRequerimiento";
		gridViewTextBoxColumn37.HeaderText = "Requerimiento";
		gridViewTextBoxColumn37.Name = "colDescRequerimiento";
		gridViewTextBoxColumn37.Width = 120;
		gridViewTextBoxColumn38.FieldName = "fecha_registro";
		gridViewTextBoxColumn38.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn38.Name = "colFechaRegistro";
		gridViewTextBoxColumn38.Width = 110;
		gridViewTextBoxColumn39.FieldName = "codEstado";
		gridViewTextBoxColumn39.HeaderText = "codEstado";
		gridViewTextBoxColumn39.IsVisible = false;
		gridViewTextBoxColumn39.Name = "colCodEstado";
		gridViewTextBoxColumn40.FieldName = "estado";
		gridViewTextBoxColumn40.HeaderText = "Estado";
		gridViewTextBoxColumn40.Name = "colEstado";
		gridViewTextBoxColumn40.Width = 110;
		gridViewTextBoxColumn41.FieldName = "codUnidad";
		gridViewTextBoxColumn41.HeaderText = "codUnidad";
		gridViewTextBoxColumn41.IsVisible = false;
		gridViewTextBoxColumn41.Name = "colCodUnidad";
		gridViewTextBoxColumn42.FieldName = "descUnidad";
		gridViewTextBoxColumn42.HeaderText = "Unidad";
		gridViewTextBoxColumn42.Name = "colDescUnidad";
		gridViewTextBoxColumn42.Width = 100;
		gridViewTextBoxColumn43.FieldName = "cantidad_pedida";
		gridViewTextBoxColumn43.HeaderText = "Ctdad. Pedida";
		gridViewTextBoxColumn43.Name = "colCtdadPedida";
		gridViewTextBoxColumn43.Width = 100;
		gridViewTextBoxColumn44.FieldName = "cantidad_confirmada";
		gridViewTextBoxColumn44.HeaderText = "Ctdad. Confirmada";
		gridViewTextBoxColumn44.Name = "colCtdadConfirmada";
		gridViewTextBoxColumn44.Width = 100;
		this.gvtRequerimientos.Columns.AddRange(gridViewTextBoxColumn35, gridViewTextBoxColumn36, gridViewTextBoxColumn37, gridViewTextBoxColumn38, gridViewTextBoxColumn39, gridViewTextBoxColumn40, gridViewTextBoxColumn41, gridViewTextBoxColumn42, gridViewTextBoxColumn43, gridViewTextBoxColumn44);
		this.gvtRequerimientos.EnableFiltering = true;
		this.gvtRequerimientos.EnableGrouping = false;
		this.gvtRequerimientos.ReadOnly = true;
		this.gvtRequerimientos.ShowRowHeaderColumn = false;
		this.gvtRequerimientos.ViewDefinition = tableViewDefinition5;
		this.rgvListadoProductosRequerimientos.AutoGenerateHierarchy = true;
		this.rgvListadoProductosRequerimientos.AutoScroll = true;
		this.rgvListadoProductosRequerimientos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListadoProductosRequerimientos.Location = new System.Drawing.Point(3, 16);
		this.rgvListadoProductosRequerimientos.MasterTemplate.AllowAddNewRow = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.AllowColumnReorder = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.AllowDeleteRow = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.AllowDragToGroup = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.AllowEditRow = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		this.rgvListadoProductosRequerimientos.MasterTemplate.ClipboardCutMode = Telerik.WinControls.UI.GridViewClipboardCutMode.Disable;
		this.rgvListadoProductosRequerimientos.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
		gridViewTextBoxColumn45.FieldName = "codProducto";
		gridViewTextBoxColumn45.HeaderText = "codProducto";
		gridViewTextBoxColumn45.IsVisible = false;
		gridViewTextBoxColumn45.Name = "colCodProducto";
		gridViewTextBoxColumn46.FieldName = "referencia";
		gridViewTextBoxColumn46.HeaderText = "Referencia";
		gridViewTextBoxColumn46.Name = "colReferencia";
		gridViewTextBoxColumn46.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn46.Width = 122;
		gridViewTextBoxColumn47.FieldName = "descProducto";
		gridViewTextBoxColumn47.HeaderText = "Descripcion";
		gridViewTextBoxColumn47.Name = "colDescProducto";
		gridViewTextBoxColumn47.Width = 305;
		gridViewTextBoxColumn48.FieldName = "codUnidadBase";
		gridViewTextBoxColumn48.HeaderText = "codUnidadBase";
		gridViewTextBoxColumn48.IsVisible = false;
		gridViewTextBoxColumn48.Name = "colCodUnidadBase";
		gridViewTextBoxColumn49.FieldName = "unidadBase";
		gridViewTextBoxColumn49.HeaderText = "Unidad Base";
		gridViewTextBoxColumn49.Name = "colUnidadBase";
		gridViewTextBoxColumn49.Width = 141;
		gridViewTextBoxColumn50.FieldName = "cantidad_pedida";
		gridViewTextBoxColumn50.HeaderText = "Total Ctdad. Pedida";
		gridViewTextBoxColumn50.Name = "colCtdadPedida";
		gridViewTextBoxColumn50.Width = 99;
		gridViewTextBoxColumn50.WrapText = true;
		gridViewTextBoxColumn51.FieldName = "cantidad_confirmada";
		gridViewTextBoxColumn51.HeaderText = "Total Ctdad. Confirmada";
		gridViewTextBoxColumn51.Name = "colCtdadConfirmada";
		gridViewTextBoxColumn51.Width = 142;
		gridViewTextBoxColumn51.WrapText = true;
		this.rgvListadoProductosRequerimientos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn45, gridViewTextBoxColumn46, gridViewTextBoxColumn47, gridViewTextBoxColumn48, gridViewTextBoxColumn49, gridViewTextBoxColumn50, gridViewTextBoxColumn51);
		this.rgvListadoProductosRequerimientos.MasterTemplate.EnableFiltering = true;
		this.rgvListadoProductosRequerimientos.MasterTemplate.EnableGrouping = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.EnableHierarchyFiltering = true;
		this.rgvListadoProductosRequerimientos.MasterTemplate.MultiSelect = true;
		this.rgvListadoProductosRequerimientos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListadoProductosRequerimientos.MasterTemplate.Templates.AddRange(this.gvtRequerimientos);
		this.rgvListadoProductosRequerimientos.MasterTemplate.ViewDefinition = tableViewDefinition6;
		this.rgvListadoProductosRequerimientos.Name = "rgvListadoProductosRequerimientos";
		this.rgvListadoProductosRequerimientos.ReadOnly = true;
		this.rgvListadoProductosRequerimientos.Size = new System.Drawing.Size(846, 311);
		this.rgvListadoProductosRequerimientos.TabIndex = 0;
		this.rgvListadoProductosRequerimientos.ThemeName = "TelerikMetroTouch";
		this.rgvListadoProductosRequerimientos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvListadoProductosRequerimientos_CellDoubleClick);
		this.groupBox1.Controls.Add(this.btnReporteDetallado);
		this.groupBox1.Controls.Add(this.btnReporteResumen);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbTipoFecha);
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(852, 117);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(183, 13);
		this.label1.TabIndex = 38;
		this.label1.Text = "Fecha de Registro de Requerimiento:";
		this.cmbTipoFecha.FormattingEnabled = true;
		this.cmbTipoFecha.Items.AddRange(new object[1] { "Fecha Registro" });
		this.cmbTipoFecha.Location = new System.Drawing.Point(182, 13);
		this.cmbTipoFecha.Name = "cmbTipoFecha";
		this.cmbTipoFecha.Size = new System.Drawing.Size(162, 21);
		this.cmbTipoFecha.TabIndex = 37;
		this.cmbTipoFecha.Visible = false;
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.BackColor = System.Drawing.Color.White;
		this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizar.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizar.Location = new System.Drawing.Point(730, 19);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(110, 45);
		this.btnActualizar.TabIndex = 36;
		this.btnActualizar.Text = "Actualizar";
		this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnActualizar.UseVisualStyleBackColor = false;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(161, 51);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 35;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 51);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 34;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(57, 47);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 33;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(206, 47);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 32;
		this.groupBox2.Controls.Add(this.rgvListadoProductosRequerimientos);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 117);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(852, 330);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.btnReporteResumen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporteResumen.BackColor = System.Drawing.Color.FromArgb(128, 209, 118);
		this.btnReporteResumen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnReporteResumen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporteResumen.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporteResumen.Location = new System.Drawing.Point(498, 19);
		this.btnReporteResumen.Name = "btnReporteResumen";
		this.btnReporteResumen.Size = new System.Drawing.Size(110, 45);
		this.btnReporteResumen.TabIndex = 40;
		this.btnReporteResumen.Text = "Reporte Resumen";
		this.btnReporteResumen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporteResumen.UseVisualStyleBackColor = false;
		this.btnReporteResumen.Click += new System.EventHandler(btnReporteResumen_Click);
		this.btnReporteDetallado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporteDetallado.BackColor = System.Drawing.Color.FromArgb(128, 209, 118);
		this.btnReporteDetallado.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnReporteDetallado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporteDetallado.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporteDetallado.Location = new System.Drawing.Point(614, 19);
		this.btnReporteDetallado.Name = "btnReporteDetallado";
		this.btnReporteDetallado.Size = new System.Drawing.Size(110, 45);
		this.btnReporteDetallado.TabIndex = 41;
		this.btnReporteDetallado.Text = "Reporte Detallado";
		this.btnReporteDetallado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporteDetallado.UseVisualStyleBackColor = false;
		this.btnReporteDetallado.Click += new System.EventHandler(btnReporteDetallado_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(852, 447);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListadoConsumoRequerimiento";
		this.Text = "Listado de Consumo de Requerimiento";
		base.Load += new System.EventHandler(frmListadoConsumoRequerimiento_Load);
		((System.ComponentModel.ISupportInitialize)this.gvtRequerimientos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoProductosRequerimientos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoProductosRequerimientos).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
