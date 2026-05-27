using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Base.WinForm;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmListadoReporteEntrega : Form
{
	private clsAdmDespacho admdespacho = new clsAdmDespacho();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private Atriform objfrm;

	private bool flgloadcboanalisis = false;

	private DataSet dsAlmacen = new DataSet();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RadGridView rgvEntregas;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Label label1;

	private Button btnBusqueda;

	private ComboBox cmbAlmacen;

	private Label label8;

	private Button btnReporte;

	private Button btnReporteDetallado;

	private ComboBox cbo_Analisis;

	private Label label9;

	public frmListadoReporteEntrega()
	{
		InitializeComponent();
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		cargaLista();
	}

	private void cargaLista()
	{
		DataTable data = admdespacho.ListadoReporteEntrega(dtpDesde.Value, dtpHasta.Value, cbo_Analisis.SelectedValue.ToString(), cmbAlmacen.SelectedValue.ToString());
		rgvEntregas.DataSource = data;
	}

	private void frmListadoReporteEntrega_Load(object sender, EventArgs e)
	{
		CargaAnalisis();
		flgloadcboanalisis = true;
		cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
		cargaLista();
	}

	private void CargaAnalisis()
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		DataSet ds = new DataSet();
		dBAccess.AddParameter("pcodigotabla", "002");
		ds = dBAccess.ExecuteDataSet("sp_get_tablas");
		cbo_Analisis.DataSource = ds.Tables[0];
		cbo_Analisis.DisplayMember = "DescTablaDetalle";
		cbo_Analisis.ValueMember = "codigo";
		string sSelEmpresa = "002001";
		foreach (DataRow fila in ds.Tables[0].Rows)
		{
			object valor = fila.Field<object>("Adicional1");
			valor = ((valor == null) ? "" : valor);
			string almacen = frmLogin.sAlmacen.Substring(8, 3);
			if (valor.ToString() == almacen)
			{
				sSelEmpresa = fila.Field<object>("codigo").ToString();
				break;
			}
		}
		if (sSelEmpresa.Trim().Length > 0)
		{
			cbo_Analisis.SelectedValue = sSelEmpresa;
		}
	}

	private void cargaAlmacenes(string codigodtabla)
	{
		DBAccessMYSQL dBAccess = new DBAccessMYSQL();
		dsAlmacen = new DataSet();
		dBAccess.AddParameter("pparentcodigo", codigodtabla);
		dsAlmacen = dBAccess.ExecuteDataSet("sp_get_tablasparents");
		cmbAlmacen.DataSource = dsAlmacen.Tables[0];
		cmbAlmacen.DisplayMember = "DescTablaDetalle";
		cmbAlmacen.ValueMember = "codigo";
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(rgvEntregas);
			spreadStreamExport.ExportVisualSettings = true;
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar("exportacionListadoEntregaResumen");
				if (cadenaGuardado != null)
				{
					spreadStreamExport.RunExport(cadenaGuardado, new SpreadStreamExportRenderer());
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, base.Name + " - Metodo btnReporte_Click");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnReporteDetallado_Click(object sender, EventArgs e)
	{
		try
		{
			DataTable data = admdespacho.ExportarListadoReporteEntrega(dtpDesde.Value, dtpHasta.Value, cbo_Analisis.SelectedValue.ToString(), cmbAlmacen.SelectedValue.ToString()).Tables[0];
			string ruta = "C:\\tmp\\Reportes";
			string nombreArchivo = "Resumen_Listado_Entregas";
			SLDocument sl = new SLDocument();
			int indFilaInicial = 1;
			int indCol = 0;
			foreach (DataColumn col in data.Columns)
			{
				indCol++;
				sl.SetCellValue(indFilaInicial, indCol, col.ColumnName);
			}
			int indFilaContenido = indFilaInicial;
			foreach (DataRow fila in data.Rows)
			{
				indFilaContenido++;
				indCol = 0;
				foreach (DataColumn col2 in data.Columns)
				{
					indCol++;
					object valor = fila.Field<object>(col2.ColumnName);
					if (valor == null)
					{
						valor = "";
					}
					sl.SetCellValue(indFilaContenido, indCol, valor.ToString());
				}
			}
			SLStyle estilo = sl.CreateStyle();
			estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(211, 234, 242), System.Drawing.Color.Blue);
			sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 18, estilo);
			SLStyle aux_style = sl.CreateStyle();
			asignarBordes(aux_style);
			sl.SetCellStyle(indFilaInicial, 1, indFilaContenido, 18, aux_style);
			sl.SetColumnWidth(1, 4.7);
			sl.SetColumnWidth(2, 14.4);
			sl.SetColumnWidth(3, 14.6);
			sl.SetColumnWidth(4, 17.3);
			sl.SetColumnWidth(5, 17.3);
			sl.SetColumnWidth(6, 34.0);
			sl.SetColumnWidth(7, 11.1);
			sl.SetColumnWidth(8, 9.3);
			sl.SetColumnWidth(9, 9.6);
			sl.SetColumnWidth(10, 34.0);
			sl.SetColumnWidth(11, 18.1);
			sl.SetColumnWidth(12, 18.0);
			sl.SetColumnWidth(13, 21.4);
			sl.SetColumnWidth(14, 20.1);
			sl.SetColumnWidth(15, 20.6);
			sl.SetColumnWidth(16, 19.3);
			sl.SetColumnWidth(17, 28.0);
			sl.SetColumnWidth(18, 16.6);
			SLStyle style = sl.CreateStyle();
			style.SetWrapText(IsWrapped: true);
			sl.SetColumnStyle(6, style);
			sl.SetColumnStyle(10, style);
			sl.SetColumnStyle(17, style);
			sl.SetColumnStyle(18, style);
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
				MessageBox.Show(ex.Message, base.Name + " - Metodo btnReporteDetallado_Click");
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

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Listado_Entrega")
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
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Listado de Entregas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void rgvEntregas_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex <= -1 || e.ColumnIndex <= -1)
			{
				return;
			}
			int codDespacho = Convert.ToInt32(e.Row.Cells["colCodDespacho"].Value);
			clsDespacho despacho = admdespacho.cargaDespacho(codDespacho);
			switch (e.Column.Name)
			{
			case "colNroEntrega":
			{
				int codEntrega = Convert.ToInt32(e.Row.Cells["colCodEntrega"].Value);
				clsEntrega entrega = admdespacho.cargaEntrega(codEntrega);
				frmEntrega form5 = new frmEntrega();
				form5.codEntrega = codEntrega;
				form5.codReqAlm = despacho.codReqAlmRelacionado;
				form5.ShowDialog();
				break;
			}
			case "colNroDespacho":
			{
				int estado = Convert.ToInt32(e.Row.Cells["colCodEstadoDespacho"].Value);
				int anulado = Convert.ToInt32(e.Row.Cells["colCodAnuladoDespacho"].Value);
				frmDespacho form4 = new frmDespacho();
				form4.MdiParent = base.MdiParent;
				form4.Dock = DockStyle.Fill;
				form4.WindowState = FormWindowState.Maximized;
				form4.codDespacho = codDespacho;
				form4.Proceso = ((estado == 1 && anulado == 0) ? 1 : 2);
				form4.Show();
				break;
			}
			case "colNroRequerimiento":
			{
				int codReqAlmacen = despacho.codReqAlmRelacionado;
				frmReqAlmacen form6 = mdi_Menu.buscarFrmReqAlmacen("frmReqAlmacen", codReqAlmacen, 2);
				if (form6 != null)
				{
					form6.Activate();
					break;
				}
				form6 = new frmReqAlmacen();
				form6.MdiParent = base.MdiParent;
				form6.codRequerimientoAlmacen = codReqAlmacen;
				form6.Proceso = 2;
				form6.Show();
				break;
			}
			case "colNroComprobante":
			{
				int codTablaDocRelacionada = despacho.CodTablaDocRelacionada;
				int num = codTablaDocRelacionada;
				if (num == 1)
				{
					frmVenta form3 = new frmVenta();
					form3.MdiParent = base.MdiParent;
					form3.CodVenta = despacho.CodDocRelacionado.ToString();
					form3.Proceso = 3;
					form3.Show();
					break;
				}
				throw new Exception("No se ha especificado la apertura de formulario para el tipo de tabla relacionada con el despacho seleccionado");
			}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cbo_Analisis_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (flgloadcboanalisis)
		{
			cargaAlmacenes(cbo_Analisis.SelectedValue.ToString());
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbo_Analisis = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.btnReporteDetallado = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvEntregas = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvEntregas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvEntregas.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.cbo_Analisis);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.btnReporteDetallado);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1301, 65);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.cbo_Analisis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbo_Analisis.FormattingEnabled = true;
		this.cbo_Analisis.Location = new System.Drawing.Point(418, 24);
		this.cbo_Analisis.Name = "cbo_Analisis";
		this.cbo_Analisis.Size = new System.Drawing.Size(183, 21);
		this.cbo_Analisis.TabIndex = 74;
		this.cbo_Analisis.SelectedIndexChanged += new System.EventHandler(cbo_Analisis_SelectedIndexChanged);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label9.Location = new System.Drawing.Point(363, 28);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(59, 13);
		this.label9.TabIndex = 73;
		this.label9.Text = "Análisis : ";
		this.btnReporteDetallado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporteDetallado.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporteDetallado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporteDetallado.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnReporteDetallado.Location = new System.Drawing.Point(891, 19);
		this.btnReporteDetallado.Name = "btnReporteDetallado";
		this.btnReporteDetallado.Size = new System.Drawing.Size(134, 36);
		this.btnReporteDetallado.TabIndex = 72;
		this.btnReporteDetallado.Text = "Reporte Detallado";
		this.btnReporteDetallado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporteDetallado.UseVisualStyleBackColor = true;
		this.btnReporteDetallado.Click += new System.EventHandler(btnReporteDetallado_Click);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnReporte.Location = new System.Drawing.Point(1031, 19);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(134, 36);
		this.btnReporte.TabIndex = 71;
		this.btnReporte.Text = "Reporte Resumen";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(666, 24);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(183, 21);
		this.cmbAlmacen.TabIndex = 70;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold);
		this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label8.Location = new System.Drawing.Point(607, 27);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(62, 13);
		this.label8.TabIndex = 69;
		this.label8.Text = "Almacén: ";
		this.btnBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnBusqueda.Image = SIGEFA.Properties.Resources.cambio;
		this.btnBusqueda.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBusqueda.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.btnBusqueda.Location = new System.Drawing.Point(1171, 19);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(118, 36);
		this.btnBusqueda.TabIndex = 42;
		this.btnBusqueda.Text = "Recargar Lista";
		this.btnBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label1.Location = new System.Drawing.Point(6, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(115, 13);
		this.label1.TabIndex = 41;
		this.label1.Text = "Fecha de Registro:";
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.SystemColors.Control;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label6.Location = new System.Drawing.Point(186, 28);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(48, 13);
		this.label6.TabIndex = 40;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.SystemColors.Control;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
		this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.label5.Location = new System.Drawing.Point(6, 28);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 39;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(59, 25);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(121, 20);
		this.dtpDesde.TabIndex = 38;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(236, 25);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(121, 20);
		this.dtpHasta.TabIndex = 37;
		this.groupBox2.Controls.Add(this.rgvEntregas);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 65);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1301, 385);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.rgvEntregas.AutoScroll = true;
		this.rgvEntregas.AutoSizeRows = true;
		this.rgvEntregas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvEntregas.Location = new System.Drawing.Point(3, 16);
		this.rgvEntregas.MasterTemplate.AllowAddNewRow = false;
		this.rgvEntregas.MasterTemplate.AllowColumnReorder = false;
		this.rgvEntregas.MasterTemplate.AllowDeleteRow = false;
		this.rgvEntregas.MasterTemplate.AllowDragToGroup = false;
		this.rgvEntregas.MasterTemplate.AllowEditRow = false;
		this.rgvEntregas.MasterTemplate.AutoGenerateColumns = false;
		gridViewTextBoxColumn1.AllowFiltering = false;
		gridViewTextBoxColumn1.FieldName = "nroItem";
		gridViewTextBoxColumn1.HeaderText = "Item";
		gridViewTextBoxColumn1.Name = "colNroItem";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 44;
		gridViewTextBoxColumn2.FieldName = "fechaEntrega";
		gridViewTextBoxColumn2.HeaderText = "Fecha Entrega";
		gridViewTextBoxColumn2.Name = "colFechaEntrega";
		gridViewTextBoxColumn2.Width = 70;
		gridViewTextBoxColumn2.WrapText = true;
		gridViewTextBoxColumn3.FieldName = "horaEntrega";
		gridViewTextBoxColumn3.HeaderText = "Hora Entrega";
		gridViewTextBoxColumn3.Name = "colHoraEntrega";
		gridViewTextBoxColumn3.Width = 61;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.FieldName = "nroEntrega";
		gridViewTextBoxColumn4.HeaderText = "Nro. Entrega";
		gridViewTextBoxColumn4.Name = "colNroEntrega";
		gridViewTextBoxColumn4.Width = 114;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.FieldName = "estadoEntrega";
		gridViewTextBoxColumn5.HeaderText = "Estado Entrega";
		gridViewTextBoxColumn5.Name = "colEstadoEntrega";
		gridViewTextBoxColumn5.Width = 72;
		gridViewTextBoxColumn5.WrapText = true;
		gridViewTextBoxColumn6.FieldName = "nombreCliente";
		gridViewTextBoxColumn6.HeaderText = "Cliente";
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "colNombreCliente";
		gridViewTextBoxColumn6.Width = 186;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "fechaDespacho";
		gridViewTextBoxColumn7.HeaderText = "Fecha Despacho";
		gridViewTextBoxColumn7.Name = "colFechaDespacho";
		gridViewTextBoxColumn7.Width = 113;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "nroDespacho";
		gridViewTextBoxColumn8.HeaderText = "Nro. Despacho";
		gridViewTextBoxColumn8.Name = "colNroDespacho";
		gridViewTextBoxColumn8.Width = 121;
		gridViewTextBoxColumn8.WrapText = true;
		gridViewTextBoxColumn9.FieldName = "fechaRequerimiento";
		gridViewTextBoxColumn9.HeaderText = "Fecha Requerimiento";
		gridViewTextBoxColumn9.Name = "colFechaRequerimiento";
		gridViewTextBoxColumn9.Width = 119;
		gridViewTextBoxColumn9.WrapText = true;
		gridViewTextBoxColumn10.FieldName = "nroRequerimiento";
		gridViewTextBoxColumn10.HeaderText = "Nro. Requerimiento";
		gridViewTextBoxColumn10.Name = "colNroRequerimiento";
		gridViewTextBoxColumn10.Width = 115;
		gridViewTextBoxColumn10.WrapText = true;
		gridViewTextBoxColumn11.FieldName = "fechaComprobante";
		gridViewTextBoxColumn11.HeaderText = "Fecha Comprobante";
		gridViewTextBoxColumn11.Name = "colFechaComprobante";
		gridViewTextBoxColumn11.Width = 116;
		gridViewTextBoxColumn11.WrapText = true;
		gridViewTextBoxColumn12.FieldName = "nroComprobante";
		gridViewTextBoxColumn12.HeaderText = "Nro. Comprobante";
		gridViewTextBoxColumn12.Name = "colNroComprobante";
		gridViewTextBoxColumn12.Width = 97;
		gridViewTextBoxColumn12.WrapText = true;
		gridViewTextBoxColumn13.FieldName = "usuarioDespachador";
		gridViewTextBoxColumn13.HeaderText = "Usuario Despachador";
		gridViewTextBoxColumn13.Multiline = true;
		gridViewTextBoxColumn13.Name = "colUsuarioDespachador";
		gridViewTextBoxColumn13.Width = 133;
		gridViewTextBoxColumn13.WrapText = true;
		gridViewTextBoxColumn14.FieldName = "almacenDespachador";
		gridViewTextBoxColumn14.HeaderText = "Almacen Despachador";
		gridViewTextBoxColumn14.Name = "colAlmacenDespachador";
		gridViewTextBoxColumn14.Width = 104;
		gridViewTextBoxColumn14.WrapText = true;
		gridViewTextBoxColumn15.FieldName = "codEntrega";
		gridViewTextBoxColumn15.HeaderText = "codEntrega";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "colCodEntrega";
		gridViewTextBoxColumn16.FieldName = "codDespacho";
		gridViewTextBoxColumn16.HeaderText = "codDespacho";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "colCodDespacho";
		gridViewTextBoxColumn17.FieldName = "codEstadoDespacho";
		gridViewTextBoxColumn17.HeaderText = "codEstadoDespacho";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "colCodEstadoDespacho";
		gridViewTextBoxColumn18.FieldName = "codAnuladoDespacho";
		gridViewTextBoxColumn18.HeaderText = "colAnuladoDespacho";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "colCodAnuladoDespacho";
		this.rgvEntregas.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18);
		this.rgvEntregas.MasterTemplate.EnableFiltering = true;
		this.rgvEntregas.MasterTemplate.EnableGrouping = false;
		this.rgvEntregas.MasterTemplate.MultiSelect = true;
		this.rgvEntregas.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvEntregas.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvEntregas.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvEntregas.Name = "rgvEntregas";
		this.rgvEntregas.ShowHeaderCellButtons = true;
		this.rgvEntregas.Size = new System.Drawing.Size(1295, 366);
		this.rgvEntregas.TabIndex = 0;
		this.rgvEntregas.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvEntregas_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1301, 450);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.MinimumSize = new System.Drawing.Size(1317, 39);
		base.Name = "frmListadoReporteEntrega";
		this.Text = "Listado de Reporte de Entrega";
		base.Load += new System.EventHandler(frmListadoReporteEntrega_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvEntregas.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvEntregas).EndInit();
		base.ResumeLayout(false);
	}
}
