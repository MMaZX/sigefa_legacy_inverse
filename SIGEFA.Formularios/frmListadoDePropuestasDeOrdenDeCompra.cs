using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SpreadsheetLight;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoDePropuestasDeOrdenDeCompra : Form
{
	private BindingSource data = new BindingSource();

	private clsAdmPropuestaDePedido admPropuestaDePedido = new clsAdmPropuestaDePedido();

	private int codPropuesta = 0;

	private int indDGV;

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView rgvlistadopropuesta;

	public Button btnsalir;

	private Button btnVerEditar;

	private Button btnActualizar;

	private ComboBox cmbAlmacenes;

	private Label label1;

	private Button button1;

	private ComboBox cmbFechasFiltrar;

	private Label label2;

	private DateTimePicker dtpDesde;

	private Label label3;

	private DateTimePicker dtpHasta;

	private Label label4;

	private Button btnReporte;

	private Button btnExcel;

	private TextBox txtCodprod;

	private Label label8;

	private Button btnBuscarProducto;

	private Label label5;

	private Label txtNombreProducto;

	private GroupBox gbprincipal;

	private Label label6;

	private ComboBox cmbFiltroEstado;

	public frmListadoDePropuestasDeOrdenDeCompra()
	{
		InitializeComponent();
	}

	private void frmListadoDePropuestasDeOrdenDeCompra_Load(object sender, EventArgs e)
	{
		cmbFechasFiltrar.SelectedIndex = 0;
		listaPropuestaOrdenDeCompra();
		cargaAlmacenes();
		cmbFiltroEstado.SelectedIndex = 1;
	}

	public void cargaAlmacenes()
	{
		clsAdmAlmacen admalma = new clsAdmAlmacen();
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), "Error Filtrado - " + Text);
		}
	}

	private void rgvlistadopropuesta_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		if (cmbFechasFiltrar.SelectedIndex > 0)
		{
			listaPropuestaOrdenDeCompraSegunFecha();
		}
		else
		{
			listaPropuestaOrdenDeCompra();
		}
		cmbFiltroEstado.SelectedIndex = 0;
	}

	private void listaPropuestaOrdenDeCompraSegunFecha()
	{
		rgvlistadopropuesta.AutoGenerateColumns = false;
		rgvlistadopropuesta.DataSource = data;
		data.DataSource = admPropuestaDePedido.listadoPropuestaOrdenDeCompraSegunFecha(2, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, cmbFechasFiltrar.SelectedIndex, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		rgvlistadopropuesta.ClearSelection();
	}

	public void listaPropuestaOrdenDeCompra()
	{
		rgvlistadopropuesta.AutoGenerateColumns = false;
		rgvlistadopropuesta.DataSource = data;
		data.DataSource = admPropuestaDePedido.listadoPropuestaOrdenDeCompra(2, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal);
		data.Filter = string.Empty;
		rgvlistadopropuesta.ClearSelection();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmPropuestaDeOrdenCompra"] != null)
		{
			Application.OpenForms["frmPropuestaDeOrdenCompra"].Activate();
			return;
		}
		frmPropuestaDeOrdenCompra form = new frmPropuestaDeOrdenCompra();
		form.MdiParent = base.MdiParent;
		form.Show();
	}

	private void btnVerEditar_Click(object sender, EventArgs e)
	{
		if (codPropuesta > 0)
		{
			frmPropuestaDeOrdenCompra form = new frmPropuestaDeOrdenCompra();
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.modoEdicion = true;
			form.CodPropuesta = codPropuesta;
			form.MdiParent = base.MdiParent;
			form.Text = rgvlistadopropuesta.Rows[indDGV].Cells["coltitulo"].Value.ToString();
			form.Show();
		}
		else
		{
			MessageBox.Show("Seleccione una plantilla para mostrar", "Lista de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void rgvlistadopropuesta_CellClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			clsReportePropuestaDePedido dso = new clsReportePropuestaDePedido();
			CRListadoDePropuestasDePedido rpt = new CRListadoDePropuestasDePedido();
			frmRptPropuestaDePedido frm = new frmRptPropuestaDePedido();
			rpt.SetDataSource(dso.listadopropuestasdepedido(2, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, frmLogin.iCodEmpresa, cmbFechasFiltrar.SelectedIndex, dtpDesde.Value.Date, dtpHasta.Value.Date).Tables[0]);
			frm.crvPropuestaDePedido.ReportSource = rpt;
			frm.Text = "Listado de Propuestas de Pedido";
			frm.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message + " - Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnExcel_Click(object sender, EventArgs e)
	{
		try
		{
			SLDocument sl = new SLDocument();
			int indFilaInicial = 1;
			sl.SetCellValue(indFilaInicial, 1, "Listado de Propuestas de Orden de Compra");
			indFilaInicial++;
			foreach (GridViewDataColumn col in rgvlistadopropuesta.Columns)
			{
			}
			sl.SetCellValue(indFilaInicial, 1, "Titulo");
			sl.SetCellValue(indFilaInicial, 2, "Descripcion");
			sl.SetCellValue(indFilaInicial, 3, "Almacen");
			sl.SetCellValue(indFilaInicial, 4, "Fecha Registro");
			sl.SetCellValue(indFilaInicial, 5, "Fecha Edicion");
			sl.SetCellValue(indFilaInicial, 6, "Usuario");
			sl.SetCellValue(indFilaInicial, 7, "Fecha Generacion");
			sl.SetCellValue(indFilaInicial, 8, "Ctdad Dias");
			sl.SetCellValue(indFilaInicial, 9, "Estado");
			int indFilaContenido = indFilaInicial;
			foreach (GridViewRowInfo fila in rgvlistadopropuesta.Rows)
			{
				indFilaContenido++;
				sl.SetCellValue(indFilaContenido, 1, fila.Cells["coltitulo"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 2, fila.Cells["coldescripcion"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 3, fila.Cells["coldescripalmacen"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 4, fila.Cells["colfechaRegistro"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 5, fila.Cells["colFechaEdicion"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 6, fila.Cells["colnombreusuario"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 7, fila.Cells["colfechaGeneracion"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 8, fila.Cells["colCtdadDias"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 9, fila.Cells["colestado"].Value.ToString());
			}
			SLStyle estilo = sl.CreateStyle();
			estilo.SetFontColor(System.Drawing.Color.White);
			estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
			sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 9, estilo);
			SLStyle aux_style = sl.CreateStyle();
			asignarBordes(aux_style);
			sl.SetCellStyle(indFilaInicial, 1, indFilaContenido, 9, aux_style);
			sl.SetColumnWidth(1, 30.0);
			sl.SetColumnWidth(2, 30.0);
			sl.SetColumnWidth(3, 18.0);
			sl.SetColumnWidth(4, 18.0);
			sl.SetColumnWidth(5, 18.0);
			sl.SetColumnWidth(6, 20.0);
			sl.SetColumnWidth(7, 18.0);
			sl.SetColumnWidth(8, 10.0);
			sl.SetColumnWidth(9, 20.0);
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

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Listado_Propuesta_Orden_Compra")
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

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaListaxProducto();
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
	}

	private void CargaListaxProducto()
	{
		rgvlistadopropuesta.DataSource = data;
		data.DataSource = admPropuestaDePedido.MuestraPropuestaxProducto(2, cmbFechasFiltrar.SelectedIndex, dtpDesde.Value.Date, dtpHasta.Value.Date, Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, Convert.ToInt32(txtCodprod.Text));
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
		}
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void cmbFechasFiltrar_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void rgvlistadopropuesta_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			codPropuesta = Convert.ToInt32(rgvlistadopropuesta.Rows[e.RowIndex].Cells["colcodigoPropuesta"].Value);
			indDGV = e.RowIndex;
		}
	}

	private void rgvlistadopropuesta_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.Delete)
			{
				return;
			}
			if (rgvlistadopropuesta.SelectedRows.Count == 1)
			{
				GridViewRowInfo fila = rgvlistadopropuesta.SelectedRows[0];
				clsPropuestaDePedido prop_pedi = new clsPropuestaDePedido();
				prop_pedi = admPropuestaDePedido.cargaPropuestaDePedido(Convert.ToInt32(fila.Cells["colcodigoPropuesta"].Value));
				if (prop_pedi.Estado == 1)
				{
					DialogResult rspta = MessageBox.Show("¿Esta seguro de elminar: " + prop_pedi.Titulo + "?", Text + " dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (rspta == DialogResult.Yes)
					{
						if (admPropuestaDePedido.eliminarPropuesta(prop_pedi.Codigo, 1))
						{
							MessageBox.Show("Eliminado");
						}
						else
						{
							MessageBox.Show("No se Elimino");
						}
						btnActualizar.PerformClick();
					}
				}
				else
				{
					MessageBox.Show("No se puede eliminar una propuesta con estado diferente de NUEVO.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("Seleccione solo una propuesta para eliminar con la tecla DEL.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Eliminar Items", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cmbFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
	{
		string filtro = "";
		if (cmbFiltroEstado.SelectedIndex > -1)
		{
			switch (cmbFiltroEstado.SelectedIndex)
			{
			case 0:
				data.Filter = "";
				break;
			case 1:
				filtro = string.Format("[{0}] like '*{1}*' OR [{0}] like '*{2}*'", "estado", "COTIZANDO", "NUEVO");
				data.Filter = filtro;
				break;
			case 2:
				filtro = string.Format("[{0}] like '*{1}*'", "estado", "OC GENERADA");
				data.Filter = filtro;
				break;
			default:
				MessageBox.Show("No se indentifico la opion de estado a filtrar", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				data.Filter = "";
				break;
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvlistadopropuesta = new Telerik.WinControls.UI.RadGridView();
		this.btnsalir = new System.Windows.Forms.Button();
		this.btnVerEditar = new System.Windows.Forms.Button();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.cmbFechasFiltrar = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnExcel = new System.Windows.Forms.Button();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.gbprincipal = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.cmbFiltroEstado = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvlistadopropuesta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvlistadopropuesta.MasterTemplate).BeginInit();
		this.gbprincipal.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.Transparent;
		this.groupBox1.Controls.Add(this.rgvlistadopropuesta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 114);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1275, 508);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Listado De Propuestas Para Orden de Compra";
		this.rgvlistadopropuesta.AutoScroll = true;
		this.rgvlistadopropuesta.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvlistadopropuesta.Location = new System.Drawing.Point(3, 16);
		this.rgvlistadopropuesta.MasterTemplate.AllowAddNewRow = false;
		this.rgvlistadopropuesta.MasterTemplate.AllowColumnChooser = false;
		this.rgvlistadopropuesta.MasterTemplate.AllowDeleteRow = false;
		this.rgvlistadopropuesta.MasterTemplate.AllowDragToGroup = false;
		this.rgvlistadopropuesta.MasterTemplate.AllowEditRow = false;
		this.rgvlistadopropuesta.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codigoPropuesta";
		gridViewTextBoxColumn1.HeaderText = "Codigo";
		gridViewTextBoxColumn1.Name = "colcodigoPropuesta";
		gridViewTextBoxColumn2.FieldName = "nombre";
		gridViewTextBoxColumn2.HeaderText = "Titulo";
		gridViewTextBoxColumn2.Name = "coltitulo";
		gridViewTextBoxColumn2.Width = 195;
		gridViewTextBoxColumn3.FieldName = "descripcion";
		gridViewTextBoxColumn3.HeaderText = "Descripcion";
		gridViewTextBoxColumn3.Name = "coldescripcion";
		gridViewTextBoxColumn3.Width = 191;
		gridViewTextBoxColumn4.FieldName = "codigoAlmacen";
		gridViewTextBoxColumn4.HeaderText = "codalmacen";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "colcodAlmacen";
		gridViewTextBoxColumn5.FieldName = "descripAlmacen";
		gridViewTextBoxColumn5.HeaderText = "Almacen";
		gridViewTextBoxColumn5.Name = "coldescripalmacen";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 128;
		gridViewTextBoxColumn6.FieldName = "fechaRegistro";
		gridViewTextBoxColumn6.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn6.Name = "colfechaRegistro";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn6.Width = 152;
		gridViewTextBoxColumn7.FieldName = "fechaEdicion";
		gridViewTextBoxColumn7.HeaderText = "Fecha Edicion";
		gridViewTextBoxColumn7.Name = "colFechaEdicion";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn7.Width = 152;
		gridViewTextBoxColumn8.FieldName = "nombreUsuario";
		gridViewTextBoxColumn8.HeaderText = "coduser";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colnombreusuario";
		gridViewTextBoxColumn9.FieldName = "fechaGeneracion";
		gridViewTextBoxColumn9.HeaderText = "Fecha Generacion O.C.";
		gridViewTextBoxColumn9.Name = "colfechaGeneracion";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		gridViewTextBoxColumn9.Width = 152;
		gridViewTextBoxColumn10.FieldName = "ctdadDias";
		gridViewTextBoxColumn10.HeaderText = "Ctdad Dias";
		gridViewTextBoxColumn10.Name = "colCtdadDias";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 101;
		gridViewTextBoxColumn11.FieldName = "estado";
		gridViewTextBoxColumn11.HeaderText = "Estado";
		gridViewTextBoxColumn11.Name = "colestado";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 155;
		this.rgvlistadopropuesta.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11);
		this.rgvlistadopropuesta.MasterTemplate.EnableFiltering = true;
		this.rgvlistadopropuesta.MasterTemplate.EnableGrouping = false;
		this.rgvlistadopropuesta.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvlistadopropuesta.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvlistadopropuesta.Name = "rgvlistadopropuesta";
		this.rgvlistadopropuesta.Size = new System.Drawing.Size(1269, 489);
		this.rgvlistadopropuesta.TabIndex = 1;
		this.rgvlistadopropuesta.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvlistadopropuesta_CellClick);
		this.rgvlistadopropuesta.KeyUp += new System.Windows.Forms.KeyEventHandler(rgvlistadopropuesta_KeyUp);
		this.btnsalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnsalir.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnsalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnsalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnsalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnsalir.Location = new System.Drawing.Point(1194, 41);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(75, 32);
		this.btnsalir.TabIndex = 3;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnsalir.UseVisualStyleBackColor = false;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.btnVerEditar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnVerEditar.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnVerEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnVerEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnVerEditar.Image = SIGEFA.Properties.Resources.buscar;
		this.btnVerEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVerEditar.Location = new System.Drawing.Point(1081, 41);
		this.btnVerEditar.Name = "btnVerEditar";
		this.btnVerEditar.Size = new System.Drawing.Size(107, 32);
		this.btnVerEditar.TabIndex = 2;
		this.btnVerEditar.Text = "Ver / Editar";
		this.btnVerEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVerEditar.UseVisualStyleBackColor = false;
		this.btnVerEditar.Click += new System.EventHandler(btnVerEditar_Click);
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizar.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnActualizar.Location = new System.Drawing.Point(968, 41);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(107, 32);
		this.btnActualizar.TabIndex = 45;
		this.btnActualizar.Text = "Recargar";
		this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnActualizar.UseVisualStyleBackColor = false;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(8, 25);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(179, 21);
		this.cmbAlmacenes.TabIndex = 46;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(6, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(64, 12);
		this.label1.TabIndex = 47;
		this.label1.Text = "ALMACEN:";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Location = new System.Drawing.Point(696, 44);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 48;
		this.button1.Text = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.cmbFechasFiltrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFechasFiltrar.FormattingEnabled = true;
		this.cmbFechasFiltrar.Items.AddRange(new object[4] { "Ninguno", "Fecha de Registro", "Fecha de Edicion", "Fecha de Generacion O.C." });
		this.cmbFechasFiltrar.Location = new System.Drawing.Point(8, 70);
		this.cmbFechasFiltrar.Name = "cmbFechasFiltrar";
		this.cmbFechasFiltrar.Size = new System.Drawing.Size(179, 21);
		this.cmbFechasFiltrar.TabIndex = 49;
		this.cmbFechasFiltrar.SelectedIndexChanged += new System.EventHandler(cmbFechasFiltrar_SelectedIndexChanged);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(6, 56);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(146, 12);
		this.label2.TabIndex = 50;
		this.label2.Text = "TIPO DE FECHA A LISTAR:";
		this.label2.Click += new System.EventHandler(label2_Click);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(216, 71);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 53;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Location = new System.Drawing.Point(214, 56);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(41, 12);
		this.label3.TabIndex = 54;
		this.label3.Text = "Desde:";
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(314, 71);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(81, 20);
		this.dtpHasta.TabIndex = 55;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Black;
		this.label4.Location = new System.Drawing.Point(312, 56);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(39, 12);
		this.label4.TabIndex = 56;
		this.label4.Text = "Hasta:";
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.Image = SIGEFA.Properties.Resources.printer;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.Location = new System.Drawing.Point(869, 41);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(93, 32);
		this.btnReporte.TabIndex = 57;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = false;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExcel.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExcel.Location = new System.Drawing.Point(777, 41);
		this.btnExcel.Name = "btnExcel";
		this.btnExcel.Size = new System.Drawing.Size(86, 32);
		this.btnExcel.TabIndex = 58;
		this.btnExcel.Text = "Excel";
		this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExcel.UseVisualStyleBackColor = false;
		this.btnExcel.Click += new System.EventHandler(btnExcel_Click);
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(417, 36);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 59;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(415, 22);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(115, 12);
		this.label8.TabIndex = 60;
		this.label8.Text = "Busqueda x Producto:";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(561, 24);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 61;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(414, 59);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(35, 13);
		this.label5.TabIndex = 62;
		this.label5.Text = "Prod.:";
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(455, 59);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 63;
		this.txtNombreProducto.Text = "---";
		this.gbprincipal.Controls.Add(this.label6);
		this.gbprincipal.Controls.Add(this.cmbFiltroEstado);
		this.gbprincipal.Controls.Add(this.txtNombreProducto);
		this.gbprincipal.Controls.Add(this.label5);
		this.gbprincipal.Controls.Add(this.btnBuscarProducto);
		this.gbprincipal.Controls.Add(this.label8);
		this.gbprincipal.Controls.Add(this.txtCodprod);
		this.gbprincipal.Controls.Add(this.btnExcel);
		this.gbprincipal.Controls.Add(this.btnReporte);
		this.gbprincipal.Controls.Add(this.label4);
		this.gbprincipal.Controls.Add(this.dtpHasta);
		this.gbprincipal.Controls.Add(this.label3);
		this.gbprincipal.Controls.Add(this.dtpDesde);
		this.gbprincipal.Controls.Add(this.label2);
		this.gbprincipal.Controls.Add(this.cmbFechasFiltrar);
		this.gbprincipal.Controls.Add(this.button1);
		this.gbprincipal.Controls.Add(this.label1);
		this.gbprincipal.Controls.Add(this.cmbAlmacenes);
		this.gbprincipal.Controls.Add(this.btnActualizar);
		this.gbprincipal.Controls.Add(this.btnVerEditar);
		this.gbprincipal.Controls.Add(this.btnsalir);
		this.gbprincipal.Dock = System.Windows.Forms.DockStyle.Top;
		this.gbprincipal.Location = new System.Drawing.Point(0, 0);
		this.gbprincipal.Name = "gbprincipal";
		this.gbprincipal.Size = new System.Drawing.Size(1275, 114);
		this.gbprincipal.TabIndex = 5;
		this.gbprincipal.TabStop = false;
		this.label6.AutoSize = true;
		this.label6.BackColor = System.Drawing.Color.Transparent;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.Black;
		this.label6.Location = new System.Drawing.Point(214, 11);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(160, 12);
		this.label6.TabIndex = 65;
		this.label6.Text = "TIPO DE ESTADO A FILTRAR:";
		this.cmbFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFiltroEstado.FormattingEnabled = true;
		this.cmbFiltroEstado.Items.AddRange(new object[3] { "Todos", "Pendiente", "O.C. Generado" });
		this.cmbFiltroEstado.Location = new System.Drawing.Point(216, 25);
		this.cmbFiltroEstado.Name = "cmbFiltroEstado";
		this.cmbFiltroEstado.Size = new System.Drawing.Size(179, 21);
		this.cmbFiltroEstado.TabIndex = 64;
		this.cmbFiltroEstado.SelectedIndexChanged += new System.EventHandler(cmbFiltroEstado_SelectedIndexChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(1275, 622);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.gbprincipal);
		base.Name = "frmListadoDePropuestasDeOrdenDeCompra";
		this.Text = "Listado De Propuestas De Orden De Compra";
		base.Load += new System.EventHandler(frmListadoDePropuestasDeOrdenDeCompra_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvlistadopropuesta.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvlistadopropuesta).EndInit();
		this.gbprincipal.ResumeLayout(false);
		this.gbprincipal.PerformLayout();
		base.ResumeLayout(false);
	}
}
