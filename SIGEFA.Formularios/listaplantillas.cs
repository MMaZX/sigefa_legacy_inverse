using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SpreadsheetLight;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class listaplantillas : Office2007Form
{
	public int tipoPlantillaReq = 0;

	public string tituloVentana = "Orden de Compra";

	private BindingSource data = new BindingSource();

	private clsAdmPlantillaDeProductos AdmPlantilla = new clsAdmPlantillaDeProductos();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	public int codmovi = 0;

	private int indDGV = 0;

	public bool listadoPorGenerar = false;

	internal clsUsuario usuario_click = null;

	private int codProdAbuscar = 0;

	private int in_fila_cms_dgv = -1;

	private IContainer components = null;

	private GroupBox gbgrillaplantilla;

	private Button btnEditar;

	public Button btnsalir;

	private GroupBox groupBox2;

	private Button button2;

	private Label label1;

	private ComboBox cmbAlmacenes;

	private Label txtNombreProducto;

	private Label label5;

	private Button btnBuscarProducto;

	private Label label8;

	private TextBox txtCodprod;

	private Button btnVer;

	private Button btnExcel;

	private RadGridView rgvPlantillas;

	private Label label4;

	private DateTimePicker dtpHasta;

	private Label label3;

	private DateTimePicker dtpDesde;

	private Label label2;

	private ComboBox cmbFechasFiltrar;

	private Button btnEvaluarPlantilla;

	private Button btnImportacion;

	private Button exportarDetplanillas;

	public listaplantillas()
	{
		InitializeComponent();
	}

	private void listaplantillas_Load(object sender, EventArgs e)
	{
		Text = ((tipoPlantillaReq == 0) ? (Text + " de RA") : (Text + " de OC"));
		tituloVentana = ((tipoPlantillaReq == 0) ? "Requerimiento de Almacen" : tituloVentana);
		gbgrillaplantilla.Text = gbgrillaplantilla.Text + " de " + tituloVentana;
		cmbFechasFiltrar.SelectedIndex = 0;
		cargaAlmacenes();
		listaplantilla();
		clsAdmFormulario admForm = new clsAdmFormulario();
		int cod = ((tipoPlantillaReq == 0) ? admForm.getPermisoEditarPlantilladeReqAlmacen() : admForm.getPermisoEditarPlantilla());
		btnEditar.Visible = frmLogin.AcesosUsuario.Contains(cod) || frmLogin.iNivelUser == 1;
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	public void listaplantilla()
	{
		rgvPlantillas.AutoGenerateColumns = false;
		rgvPlantillas.DataSource = data;
		if (listadoPorGenerar)
		{
			data.DataSource = AdmPlantilla.listaPlantillasPorGenerar(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal);
			ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.StartsWith, "", "", applyToRow: true);
			c2.RowBackColor = System.Drawing.Color.FromArgb(255, 179, 179);
			c2.CellBackColor = System.Drawing.Color.FromArgb(255, 179, 179);
			rgvPlantillas.Columns["colcodigoPlantilla"].ConditionalFormattingObjectList.Add(c2);
		}
		else
		{
			int tipoPlantilla = ((tipoPlantillaReq == 0) ? 1 : 2);
			int codProd = ((txtCodprod.Text.ToString() != "") ? codProdAbuscar : 0);
			data.DataSource = AdmPlantilla.listaplantillas(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, codProd, tipoPlantilla, Convert.ToInt32(cmbFechasFiltrar.SelectedIndex), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa);
			rgvPlantillas.Columns["colcodigoPlantilla"].ConditionalFormattingObjectList.Clear();
		}
		rgvPlantillas.ClearSelection();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (codmovi > 0)
		{
			ListadoProductos form = mdi_Menu.buscarFrmPlantilla("ListadoProductos", codmovi, tipoPlantillaReq);
			if (form != null)
			{
				form.Activate();
				return;
			}
			form = new ListadoProductos();
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.proceso = 1;
			form.codpla = codmovi;
			form.tipoPlantillaReq = tipoPlantillaReq;
			form.MdiParent = base.MdiParent;
			form.Text = rgvPlantillas.Rows[indDGV].Cells["coltitulo"].Value.ToString();
			form.Show();
		}
		else
		{
			MessageBox.Show("Seleccione una plantilla para mostrar", "Lista de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void generarTransferenciaDirectaToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (in_fila_cms_dgv == -1)
		{
			MessageBox.Show("Ocurrio un Error Al Seleccionar Fila", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		int codigo_fila_seleccionada = Convert.ToInt32(rgvPlantillas.Rows[in_fila_cms_dgv].Cells["colcodigoPlantilla"].Value);
		string n_plantilla = rgvPlantillas.Rows[in_fila_cms_dgv].Cells["coltitulo"].Value.ToString();
		MessageBox.Show("¡AUN NO TERMINADO!\nSelecciono: " + codigo_fila_seleccionada + " - \"" + n_plantilla + "\" Para Generar Transferencia", "Listado de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		if (codmovi > 0)
		{
			if (Application.OpenForms["F2TransferenciaEntreAlmacenes"] != null)
			{
				Application.OpenForms["F2TransferenciaEntreAlmacenes"].Activate();
				DialogResult rspta = MessageBox.Show("Debe cerrar esta ventana para poder consultar los datos de plantilla.\n¿Desea cerrar esta ventana?", "Listado de Plantillas dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rspta == DialogResult.Yes)
				{
					Application.OpenForms["F2TransferenciaEntreAlmacenes"].Close();
				}
			}
			else
			{
				F2TransferenciaEntreAlmacenes form = new F2TransferenciaEntreAlmacenes();
				form.Dock = DockStyle.Fill;
				form.WindowState = FormWindowState.Maximized;
				form.asignacionProductosDePlantilla(codigo_fila_seleccionada);
				form.Show();
			}
		}
		else
		{
			MessageBox.Show("Seleccione una plantilla para mostrar", "Lista de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void generarOrdendeCompraToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (in_fila_cms_dgv == -1)
		{
			MessageBox.Show("Ocurrio un Error Al Seleccionar Fila", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		int codigo_fila_seleccionada = Convert.ToInt32(rgvPlantillas.Rows[in_fila_cms_dgv].Cells["colcodigoPlantilla"].Value);
		string n_plantilla = rgvPlantillas.Rows[in_fila_cms_dgv].Cells["coltitulo"].Value.ToString();
		MessageBox.Show("¡AUN NO TERMINADO!\nSelecciono: " + codigo_fila_seleccionada + " - \"" + n_plantilla + "\" Para Generar Orden de Compra.", "Listado de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		if (codmovi > 0)
		{
			if (Application.OpenForms["frmOrdenCompra"] != null)
			{
				Application.OpenForms["frmOrdenCompra"].Activate();
				DialogResult rspta = MessageBox.Show("Debe cerrar esta ventana para poder consultar los datos de plantilla.\n¿Desea cerrar esta ventana?", "Listado de Plantillas dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rspta == DialogResult.Yes)
				{
					Application.OpenForms["frmOrdenCompra"].Close();
				}
			}
			else
			{
				frmOrdenCompra form = new frmOrdenCompra();
				form.Dock = DockStyle.Fill;
				form.WindowState = FormWindowState.Maximized;
				form.asignacionProductosDePlantilla(codigo_fila_seleccionada);
				form.Show();
			}
		}
		else
		{
			MessageBox.Show("Seleccione una plantilla para mostrar", "Lista de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		listadoPorGenerar = false;
		listaplantilla();
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			int tipoPlantilla = ((tipoPlantillaReq == 0) ? 1 : 2);
			int codProd = ((txtCodprod.Text.ToString() != "") ? codProdAbuscar : 0);
			data.DataSource = AdmPlantilla.listaplantillas(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, codProd, tipoPlantilla, Convert.ToInt32(cmbFechasFiltrar.SelectedIndex), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa);
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
	}

	private void CargaListaxProducto()
	{
		rgvPlantillas.DataSource = data;
		data.DataSource = AdmPlantilla.MuestraPlantillaxProducto(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, Convert.ToInt32(txtCodprod.Text));
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 6;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto().ToString().PadLeft(9, '0');
				codProdAbuscar = Convert.ToInt32(frm.GetCodigoProducto2().ToString());
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
		}
	}

	private void btnVer_Click(object sender, EventArgs e)
	{
		if (codmovi > 0)
		{
			ListadoProductos form = mdi_Menu.buscarFrmPlantilla("ListadoProductos", codmovi, tipoPlantillaReq);
			if (form != null)
			{
				form.Activate();
				return;
			}
			form = new ListadoProductos();
			form.Dock = DockStyle.Fill;
			form.WindowState = FormWindowState.Maximized;
			form.proceso = 2;
			form.codpla = codmovi;
			form.MdiParent = base.MdiParent;
			form.tipoPlantillaReq = tipoPlantillaReq;
			form.Text = rgvPlantillas.Rows[indDGV].Cells["coltitulo"].Value.ToString();
			form.Show();
		}
		else
		{
			MessageBox.Show("Seleccione una plantilla para mostrar", "Lista de Plantillas dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btnExcel_Click(object sender, EventArgs e)
	{
		try
		{
			SLDocument sl = new SLDocument();
			int indFilaInicial = 1;
			sl.SetCellValue(indFilaInicial, 1, "Titulo");
			sl.SetCellValue(indFilaInicial, 2, "Descripcion");
			sl.SetCellValue(indFilaInicial, 3, "Fecha Registro");
			sl.SetCellValue(indFilaInicial, 4, "Fecha Edicion");
			sl.SetCellValue(indFilaInicial, 5, "Almacen");
			sl.SetCellValue(indFilaInicial, 6, "Usuario");
			sl.SetCellValue(indFilaInicial, 7, "Tipo de Plantilla");
			int indFilaContenido = indFilaInicial;
			foreach (GridViewRowInfo fila in rgvPlantillas.Rows)
			{
				indFilaContenido++;
				sl.SetCellValue(indFilaContenido, 1, fila.Cells["coltitulo"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 2, fila.Cells["coldescripcion"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 3, fila.Cells["colfechaRegistro"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 4, fila.Cells["colFechaEdicion"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 5, fila.Cells["coldescripalmacen"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 6, fila.Cells["colnombreusuario"].Value.ToString());
				sl.SetCellValue(indFilaContenido, 7, fila.Cells["coltipo"].Value.ToString());
			}
			SLStyle estilo = sl.CreateStyle();
			estilo.SetFontColor(System.Drawing.Color.White);
			estilo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 153, 255), System.Drawing.Color.Blue);
			sl.SetCellStyle(indFilaInicial, 1, indFilaInicial, 7, estilo);
			SLStyle aux_style = sl.CreateStyle();
			asignarBordes(aux_style);
			sl.SetCellStyle(indFilaInicial, 1, indFilaContenido, 7, aux_style);
			sl.SetColumnWidth(1, 30.0);
			sl.SetColumnWidth(2, 42.0);
			sl.SetColumnWidth(3, 18.0);
			sl.SetColumnWidth(4, 18.0);
			sl.SetColumnWidth(5, 18.0);
			sl.SetColumnWidth(6, 26.0);
			sl.SetColumnWidth(7, 25.0);
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

	private string obtenerRutaParaGuardar(string namefile = "Exportacion_Listado_Plantilla_de_Productos")
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
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Listado de Plantilla de Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void rgvPlantillas_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			codmovi = Convert.ToInt32(rgvPlantillas.Rows[e.RowIndex].Cells["colcodigoPlantilla"].Value);
			indDGV = e.RowIndex;
		}
	}

	private void rgvPlantillas_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void rgvPlantillas_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
		{
			Point aux = rgvPlantillas.PointToClient(Cursor.Position);
			rgvPlantillas.ClearSelection();
			in_fila_cms_dgv = e.RowIndex;
			codmovi = e.RowIndex;
		}
	}

	private void rgvPlantillas_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			codmovi = Convert.ToInt32(rgvPlantillas.Rows[e.RowIndex].Cells["colcodigoPlantilla"].Value);
			indDGV = e.RowIndex;
		}
	}

	private void btnEvaluarPlantilla_Click(object sender, EventArgs e)
	{
		int tipoPlantilla = ((tipoPlantillaReq == 0) ? 1 : 2);
		data.DataSource = AdmPlantilla.listaPlantillasPorGenerar(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, tipoPlantilla);
		listadoPorGenerar = false;
		ConditionalFormattingObject c2 = new ConditionalFormattingObject("color a vacio", ConditionTypes.StartsWith, "", "", applyToRow: true);
		c2.RowBackColor = System.Drawing.Color.FromArgb(255, 179, 179);
		c2.CellBackColor = System.Drawing.Color.FromArgb(255, 179, 179);
		rgvPlantillas.Columns["colcodigoPlantilla"].ConditionalFormattingObjectList.Add(c2);
	}

	private void rgvPlantillas_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.Delete)
			{
				return;
			}
			if (rgvPlantillas.SelectedRows.Count == 1)
			{
				if (listadoPorGenerar)
				{
					MessageBox.Show("No puede eliminar plantillas de este listado.\nRecarge el listado", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				clsAdmAcceso AdmAcce = new clsAdmAcceso();
				int permiso = ((tipoPlantillaReq == 0) ? new clsAdmFormulario().getPermisoEliminarPlantillaAlmacen() : new clsAdmFormulario().getPermisoEliminarPlantillaOrdenCompra());
				List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
				if (accesos.Contains(permiso) || frmLogin.iNivelUser == 1)
				{
					GridViewRowInfo fila = rgvPlantillas.SelectedRows[0];
					clsPlantillaDeProductos plantilla = new clsPlantillaDeProductos();
					plantilla = AdmPlantilla.CargaProductoAgrupado(Convert.ToInt32(fila.Cells["colcodigoPlantilla"].Value));
					if (plantilla.Estado == 1)
					{
						string mensaje = "";
						if (AdmPlantilla.validarEliminacionPlantilla(plantilla.Codigo, plantilla.Tipo, out mensaje))
						{
							DialogResult rspta = MessageBox.Show("¿Esta seguro de elminar: " + plantilla.Nombre + "?", Text + " dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
							if (rspta != DialogResult.Yes)
							{
								return;
							}
							usuario_click = null;
							frmAutorizacion frm = new frmAutorizacion();
							frm.tipoAccion = 2;
							frm.permiso = permiso;
							frm.PermitirAdministradores = true;
							frm.tipoVentanaAAsignarUsuario = 8;
							frm.ventanaListaPlantillas = this;
							DialogResult dr = frm.ShowDialog();
							if (dr == DialogResult.OK && usuario_click != null)
							{
								if (AdmPlantilla.cambiaEstadoPlantilla(plantilla.Codigo, 0, usuario_click.CodUsuario))
								{
									MessageBox.Show("Se Elimino.");
								}
								else
								{
									MessageBox.Show("No Se Elimino.");
								}
								button2.PerformClick();
							}
						}
						else
						{
							MessageBox.Show(mensaje, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						MessageBox.Show("Esta plantilla ya se encuentra eliminada.\nRecarge el listado.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					MessageBox.Show("No tiene permiso para eliminar esta plantilla.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

	private void btnImportacion_Click(object sender, EventArgs e)
	{
		if (rgvPlantillas.SelectedRows.Count == 1)
		{
			if (listadoPorGenerar)
			{
				MessageBox.Show("No puede importar data a este listado.\nRecarge el listado", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			clsAdmAcceso AdmAcce = new clsAdmAcceso();
			List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
			if (true)
			{
				GridViewRowInfo fila = rgvPlantillas.SelectedRows[0];
				clsPlantillaDeProductos plantilla = new clsPlantillaDeProductos();
				plantilla = AdmPlantilla.CargaProductoAgrupado(Convert.ToInt32(fila.Cells["colcodigoPlantilla"].Value));
				if (plantilla.Estado == 1)
				{
					frmImportaData frm = new frmImportaData();
					frm.CodPlantilla = plantilla.Codigo;
					frm.TituloPlantilla = plantilla.Nombre;
					frm.StartPosition = FormStartPosition.CenterScreen;
					frm.ShowDialog();
				}
				else
				{
					MessageBox.Show("Esta plantilla ya se encuentra eliminada.\nRecarge el listado.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("No tiene permiso para generar importacion.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("Seleccione solo una plantilla para generar importacion.", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void exportarDetplanillas_Click(object sender, EventArgs e)
	{
		int ColInicial = 0;
		int RowInicial = 2;
		SLDocument sl = new SLDocument();
		DataTable dt = AdmPlantilla.listaDetPlantillas();
		SLStyle styleCenter = sl.CreateStyle();
		styleCenter.Alignment.Horizontal = HorizontalAlignmentValues.Center;
		styleCenter.Font.FontSize = 10.0;
		styleCenter.Font.Bold = true;
		asignarBordes1(styleCenter);
		styleCenter.SetFontColor(System.Drawing.Color.White);
		styleCenter.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.FromArgb(0, 0, 80), System.Drawing.Color.DarkBlue);
		foreach (DataColumn column in dt.Columns)
		{
			ColInicial++;
			sl.SetCellValue(1, 1, "DETALLES DE LAS PLANILLAS");
			sl.SetCellStyle(1, 1, styleCenter);
			sl.MergeWorksheetCells("A1", "L1");
			sl.SetCellValue(RowInicial, ColInicial, column.ColumnName.ToString());
			sl.SetCellStyle(RowInicial, ColInicial, styleCenter);
		}
		foreach (DataRow row in dt.Rows)
		{
			RowInicial++;
			sl.SetCellValue(RowInicial, 1, row["Cod. Planilla"].ToString());
			sl.SetCellValue(RowInicial, 2, row["Titulo"].ToString());
			sl.SetCellValue(RowInicial, 3, row["Id_detalleplantilla"].ToString());
			sl.SetCellValue(RowInicial, 4, row["Cod. Producto"].ToString());
			sl.SetCellValue(RowInicial, 5, row["Descripcion producto"].ToString());
			sl.SetCellValue(RowInicial, 6, row["Unidad"].ToString());
			sl.SetCellValue(RowInicial, 7, row["Marca"].ToString());
			sl.SetCellValue(RowInicial, 8, row["Familia"].ToString());
			sl.SetCellValue(RowInicial, 9, row["Linea"].ToString());
			sl.SetCellValue(RowInicial, 10, row["Grupo"].ToString());
			sl.SetCellValue(RowInicial, 11, row["Stockminimo"].ToString());
			sl.SetCellValue(RowInicial, 12, row["Stockmaximo"].ToString());
		}
		sl.HideColumn(3, 3);
		sl.SetColumnWidth(1, 10.0);
		sl.SetColumnWidth(2, 50.0);
		sl.SetColumnWidth(3, 15.0);
		sl.SetColumnWidth(4, 15.0);
		sl.SetColumnWidth(5, 60.0);
		sl.SetColumnWidth(6, 12.0);
		sl.SetColumnWidth(7, 20.0);
		sl.SetColumnWidth(8, 20.0);
		sl.SetColumnWidth(9, 20.0);
		sl.SetColumnWidth(10, 20.0);
		sl.SetColumnWidth(11, 20.0);
		sl.SetColumnWidth(12, 20.0);
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

	private void asignarBordes1(SLStyle style)
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
			sfd.FileName = "Exportacion_Detalle_Plantillas";
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.gbgrillaplantilla = new System.Windows.Forms.GroupBox();
		this.rgvPlantillas = new Telerik.WinControls.UI.RadGridView();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnsalir = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.exportarDetplanillas = new System.Windows.Forms.Button();
		this.btnImportacion = new System.Windows.Forms.Button();
		this.btnEvaluarPlantilla = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbFechasFiltrar = new System.Windows.Forms.ComboBox();
		this.btnExcel = new System.Windows.Forms.Button();
		this.btnVer = new System.Windows.Forms.Button();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label8 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.button2 = new System.Windows.Forms.Button();
		this.gbgrillaplantilla.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvPlantillas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvPlantillas.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.gbgrillaplantilla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbgrillaplantilla.BackColor = System.Drawing.Color.Transparent;
		this.gbgrillaplantilla.Controls.Add(this.rgvPlantillas);
		this.gbgrillaplantilla.Location = new System.Drawing.Point(0, 106);
		this.gbgrillaplantilla.Name = "gbgrillaplantilla";
		this.gbgrillaplantilla.Size = new System.Drawing.Size(1359, 427);
		this.gbgrillaplantilla.TabIndex = 1;
		this.gbgrillaplantilla.TabStop = false;
		this.gbgrillaplantilla.Text = "Listado De Plantillas";
		this.rgvPlantillas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvPlantillas.EnableGestures = false;
		this.rgvPlantillas.Location = new System.Drawing.Point(3, 16);
		this.rgvPlantillas.MasterTemplate.AllowAddNewRow = false;
		this.rgvPlantillas.MasterTemplate.AllowColumnChooser = false;
		this.rgvPlantillas.MasterTemplate.AllowDeleteRow = false;
		this.rgvPlantillas.MasterTemplate.AllowDragToGroup = false;
		this.rgvPlantillas.MasterTemplate.AllowEditRow = false;
		this.rgvPlantillas.MasterTemplate.AllowRowResize = false;
		this.rgvPlantillas.MasterTemplate.AutoGenerateColumns = false;
		this.rgvPlantillas.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codigoPlantilla";
		gridViewTextBoxColumn1.HeaderText = "id_plantillaproductos";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colcodigoPlantilla";
		gridViewTextBoxColumn2.FieldName = "nombre";
		gridViewTextBoxColumn2.HeaderText = "Titulo";
		gridViewTextBoxColumn2.Name = "coltitulo";
		gridViewTextBoxColumn2.Width = 195;
		gridViewTextBoxColumn3.FieldName = "descripcion";
		gridViewTextBoxColumn3.HeaderText = "Descripcion";
		gridViewTextBoxColumn3.Name = "coldescripcion";
		gridViewTextBoxColumn3.Width = 195;
		gridViewTextBoxColumn4.FieldName = "fechaRegistro";
		gridViewTextBoxColumn4.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn4.Name = "colfechaRegistro";
		gridViewTextBoxColumn4.Width = 195;
		gridViewTextBoxColumn5.FieldName = "fechaEdicion";
		gridViewTextBoxColumn5.HeaderText = "Fecha Edicion";
		gridViewTextBoxColumn5.Name = "colFechaEdicion";
		gridViewTextBoxColumn5.Width = 195;
		gridViewTextBoxColumn6.FieldName = "codigoAlmacen";
		gridViewTextBoxColumn6.HeaderText = "codalmacen";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "colcodalmacen";
		gridViewTextBoxColumn7.FieldName = "descripAlmacen";
		gridViewTextBoxColumn7.HeaderText = "Almacen";
		gridViewTextBoxColumn7.Name = "coldescripalmacen";
		gridViewTextBoxColumn7.Width = 195;
		gridViewTextBoxColumn8.FieldName = "codigoUsuario";
		gridViewTextBoxColumn8.HeaderText = "coduser";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colcoduser";
		gridViewTextBoxColumn9.FieldName = "nombreUsuario";
		gridViewTextBoxColumn9.HeaderText = "Usuario";
		gridViewTextBoxColumn9.Name = "colnombreusuario";
		gridViewTextBoxColumn9.Width = 195;
		gridViewTextBoxColumn10.FieldName = "tipoPlantilla";
		gridViewTextBoxColumn10.HeaderText = "Tipo de Plantilla";
		gridViewTextBoxColumn10.Name = "coltipo";
		gridViewTextBoxColumn10.Width = 188;
		this.rgvPlantillas.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10);
		this.rgvPlantillas.MasterTemplate.EnableFiltering = true;
		this.rgvPlantillas.MasterTemplate.EnableGrouping = false;
		this.rgvPlantillas.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvPlantillas.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvPlantillas.Name = "rgvPlantillas";
		this.rgvPlantillas.Size = new System.Drawing.Size(1353, 408);
		this.rgvPlantillas.TabIndex = 1;
		this.rgvPlantillas.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvPlantillas_CellClick);
		this.rgvPlantillas.KeyUp += new System.Windows.Forms.KeyEventHandler(rgvPlantillas_KeyUp);
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnEditar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEditar.Location = new System.Drawing.Point(1169, 22);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(87, 32);
		this.btnEditar.TabIndex = 2;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEditar.UseVisualStyleBackColor = false;
		this.btnEditar.Click += new System.EventHandler(button1_Click);
		this.btnsalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnsalir.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnsalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnsalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnsalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnsalir.Location = new System.Drawing.Point(1262, 22);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(75, 32);
		this.btnsalir.TabIndex = 3;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnsalir.UseVisualStyleBackColor = false;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.groupBox2.Controls.Add(this.exportarDetplanillas);
		this.groupBox2.Controls.Add(this.btnImportacion);
		this.groupBox2.Controls.Add(this.btnEvaluarPlantilla);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.dtpHasta);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.dtpDesde);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.cmbFechasFiltrar);
		this.groupBox2.Controls.Add(this.btnExcel);
		this.groupBox2.Controls.Add(this.btnVer);
		this.groupBox2.Controls.Add(this.txtNombreProducto);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.btnBuscarProducto);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.txtCodprod);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.cmbAlmacenes);
		this.groupBox2.Controls.Add(this.button2);
		this.groupBox2.Controls.Add(this.btnEditar);
		this.groupBox2.Controls.Add(this.btnsalir);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1359, 100);
		this.groupBox2.TabIndex = 4;
		this.groupBox2.TabStop = false;
		this.exportarDetplanillas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.exportarDetplanillas.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.exportarDetplanillas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.exportarDetplanillas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.exportarDetplanillas.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.exportarDetplanillas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.exportarDetplanillas.Location = new System.Drawing.Point(613, 62);
		this.exportarDetplanillas.Name = "exportarDetplanillas";
		this.exportarDetplanillas.Size = new System.Drawing.Size(218, 32);
		this.exportarDetplanillas.TabIndex = 79;
		this.exportarDetplanillas.Text = "Exportar Detalles de Planillas";
		this.exportarDetplanillas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.exportarDetplanillas.UseVisualStyleBackColor = false;
		this.exportarDetplanillas.Click += new System.EventHandler(exportarDetplanillas_Click);
		this.btnImportacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnImportacion.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnImportacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnImportacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImportacion.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnImportacion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImportacion.Location = new System.Drawing.Point(613, 22);
		this.btnImportacion.Name = "btnImportacion";
		this.btnImportacion.Size = new System.Drawing.Size(126, 32);
		this.btnImportacion.TabIndex = 78;
		this.btnImportacion.Text = "Importar Data";
		this.btnImportacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImportacion.UseVisualStyleBackColor = false;
		this.btnImportacion.Click += new System.EventHandler(btnImportacion_Click);
		this.btnEvaluarPlantilla.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnEvaluarPlantilla.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnEvaluarPlantilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnEvaluarPlantilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnEvaluarPlantilla.Image = SIGEFA.Properties.Resources.acep;
		this.btnEvaluarPlantilla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEvaluarPlantilla.Location = new System.Drawing.Point(837, 22);
		this.btnEvaluarPlantilla.Name = "btnEvaluarPlantilla";
		this.btnEvaluarPlantilla.Size = new System.Drawing.Size(144, 32);
		this.btnEvaluarPlantilla.TabIndex = 77;
		this.btnEvaluarPlantilla.Text = "Evaluar Plantillas";
		this.btnEvaluarPlantilla.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEvaluarPlantilla.UseVisualStyleBackColor = false;
		this.btnEvaluarPlantilla.Click += new System.EventHandler(btnEvaluarPlantilla_Click);
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Black;
		this.label4.Location = new System.Drawing.Point(312, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(39, 12);
		this.label4.TabIndex = 76;
		this.label4.Text = "Hasta:";
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(314, 31);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(81, 20);
		this.dtpHasta.TabIndex = 75;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Black;
		this.label3.Location = new System.Drawing.Point(214, 16);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(41, 12);
		this.label3.TabIndex = 74;
		this.label3.Text = "Desde:";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(216, 31);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 73;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.Black;
		this.label2.Location = new System.Drawing.Point(6, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(120, 12);
		this.label2.TabIndex = 72;
		this.label2.Text = "Tipo de Fecha a Listar:";
		this.cmbFechasFiltrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFechasFiltrar.FormattingEnabled = true;
		this.cmbFechasFiltrar.Items.AddRange(new object[3] { "Ninguno", "Fecha de Registro", "Fecha de Edicion" });
		this.cmbFechasFiltrar.Location = new System.Drawing.Point(8, 30);
		this.cmbFechasFiltrar.Name = "cmbFechasFiltrar";
		this.cmbFechasFiltrar.Size = new System.Drawing.Size(179, 21);
		this.cmbFechasFiltrar.TabIndex = 71;
		this.btnExcel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExcel.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnExcel.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExcel.Location = new System.Drawing.Point(745, 22);
		this.btnExcel.Name = "btnExcel";
		this.btnExcel.Size = new System.Drawing.Size(86, 32);
		this.btnExcel.TabIndex = 70;
		this.btnExcel.Text = "Excel";
		this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnExcel.UseVisualStyleBackColor = false;
		this.btnExcel.Click += new System.EventHandler(btnExcel_Click);
		this.btnVer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnVer.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.btnVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnVer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnVer.Image = SIGEFA.Properties.Resources.buscar;
		this.btnVer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVer.Location = new System.Drawing.Point(1100, 22);
		this.btnVer.Name = "btnVer";
		this.btnVer.Size = new System.Drawing.Size(63, 32);
		this.btnVer.TabIndex = 69;
		this.btnVer.Text = "Ver";
		this.btnVer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVer.UseVisualStyleBackColor = false;
		this.btnVer.Click += new System.EventHandler(btnVer_Click);
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(439, 54);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 68;
		this.txtNombreProducto.Text = "---";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(398, 54);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(35, 13);
		this.label5.TabIndex = 67;
		this.label5.Text = "Prod.:";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(545, 19);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 66;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(399, 17);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(115, 12);
		this.label8.TabIndex = 65;
		this.label8.Text = "Busqueda x Producto:";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(401, 31);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 64;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(6, 54);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 12);
		this.label1.TabIndex = 47;
		this.label1.Text = "Almacen:";
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(8, 69);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(179, 21);
		this.cmbAlmacenes.TabIndex = 46;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.button2.Image = SIGEFA.Properties.Resources.cambio;
		this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button2.Location = new System.Drawing.Point(987, 22);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(107, 32);
		this.button2.TabIndex = 45;
		this.button2.Text = "Actualizar";
		this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Menu;
		base.ClientSize = new System.Drawing.Size(1359, 533);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.gbgrillaplantilla);
		this.DoubleBuffered = true;
		this.MinimumSize = new System.Drawing.Size(1375, 39);
		base.Name = "listaplantillas";
		this.Text = "Listado de Plantillas";
		base.Load += new System.EventHandler(listaplantillas_Load);
		this.gbgrillaplantilla.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvPlantillas.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvPlantillas).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}
}
