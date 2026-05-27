using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmTodosProductosPropuestaOrdenDeCompra : Office2007Form
{
	private BindingSource data = new BindingSource();

	public DataTable tablaDatos = new DataTable();

	public int codalmacen = 0;

	public DateTime fecha_mostrar;

	public int codigoPropuesta = 0;

	private clsAdmPropuestaDePedido admPropuesta = new clsAdmPropuestaDePedido();

	private int filaIndex = -1;

	private string referenciaProd = "";

	internal int estadoPropuesta;

	internal frmPropuestaDeOrdenCompra ventana = null;

	private IContainer components = null;

	private Label label1;

	private ImageList imageList1;

	private Button btnAceptar;

	private DateTimePicker dtpFechaPago;

	private Button button1;

	private Button btnAgregarItem;

	private RadGridView rgvTodosProductos;

	public frmTodosProductosPropuestaOrdenDeCompra()
	{
		InitializeComponent();
	}

	private void frmProductosLista_Load(object sender, EventArgs e)
	{
		rgvTodosProductos.DataSource = tablaDatos;
		if (fecha_mostrar != DateTime.MinValue)
		{
			dtpFechaPago.Value = fecha_mostrar;
		}
		if (estadoPropuesta == 3)
		{
			btnAgregarItem.Enabled = false;
		}
		SetFormatoCondicionalaFilas();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.OK;
		Close();
	}

	private void frmProductosLista_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void button1_Click(object sender, EventArgs e)
	{
		clsReportePropuestaDePedido dso = new clsReportePropuestaDePedido();
		CRPropuestaDePedidoDeOrdenDeCompra rpt = new CRPropuestaDePedidoDeOrdenDeCompra();
		frmRptPropuestaDePedido frm = new frmRptPropuestaDePedido();
		rpt.SetDataSource(dso.propuestadepedidodeordendecompra(codigoPropuesta, 1, frmLogin.iCodEmpresa).Tables[0]);
		frm.crvPropuestaDePedido.ReportSource = rpt;
		frm.Show();
	}

	private clsDetallePropuestaDePedido convertirFilaEnClase(DataRow fila, int orden = 0)
	{
		clsDetallePropuestaDePedido aux = new clsDetallePropuestaDePedido();
		if (orden != 0)
		{
			aux.NroItem = orden;
		}
		aux.Cantidad_reponer = Convert.ToDouble(fila.Field<object>("ctdadReponer"));
		aux.Cantidad_sugerida = Convert.ToDouble(fila.Field<object>("ctdadSugerida"));
		if (int.TryParse((fila.Field<object>("codigoDetallePropuesta") ?? " ").ToString(), out var codigoDetalle))
		{
			aux.Codigo = codigoDetalle;
		}
		else
		{
			aux.Codigo = -1;
		}
		aux.Cod_Propuesta = codigoPropuesta;
		aux.Codigo_Producto = Convert.ToInt32(fila.Field<object>("codProd"));
		aux.Codigo_Unidad = Convert.ToInt32(fila.Field<object>("codUnidad"));
		aux.Descripcion_Unidad = fila.Field<object>("descripUnidad").ToString();
		aux.Descrip_Producto = fila.Field<object>("descripProd").ToString();
		aux.Pedido_final = double.NaN;
		aux.Precio_unit_actual = Convert.ToDouble(fila.Field<object>("precioUnitarioCompra"));
		aux.Ref_Producto = fila.Field<object>("refProd").ToString();
		aux.StockDisponible = Convert.ToDouble(fila.Field<object>("stockDisponible"));
		return aux;
	}

	private void btnAgregarItem_Click(object sender, EventArgs e)
	{
		string mostrar = "";
		int ctdad_a_mostrar = 15;
		foreach (GridViewRowInfo fila in rgvTodosProductos.SelectedRows)
		{
			ctdad_a_mostrar--;
			mostrar = mostrar + "\n>" + fila.Cells["colReferenciaProducto"].Value.ToString() + " -- " + fila.Cells["colDescripProducto"].Value.ToString();
			if (ctdad_a_mostrar == 0)
			{
				break;
			}
		}
		if (rgvTodosProductos.SelectedRows.Count > ctdad_a_mostrar)
		{
			mostrar = mostrar + "\n y " + (rgvTodosProductos.SelectedRows.Count - ctdad_a_mostrar) + " items más.";
		}
		if (codigoPropuesta == 0)
		{
			MessageBox.Show("Guarde la Propuesta para poder agregar items", "Agregando item a propuesta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else if (rgvTodosProductos.SelectedRows.Count > 0)
		{
			DialogResult rpta = MessageBox.Show("Esta seguro de agregar item: " + mostrar, "Agregando item a propuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta != DialogResult.Yes)
			{
				return;
			}
			string agregados = "";
			int ctdad_agregados = 0;
			string no_agregados = "";
			int ctdad_noagregados = 0;
			foreach (GridViewRowInfo fila2 in rgvTodosProductos.SelectedRows)
			{
				referenciaProd = fila2.Cells["colReferenciaProducto"].Value.ToString();
				List<DataRow> det = (from x in tablaDatos.AsEnumerable()
					where x.Field<object>("refProd").ToString() == referenciaProd
					select x).ToList();
				if (det.Count > 0)
				{
					DataTable datosdetaprop = admPropuesta.cargaDetallePropuestaDePedido(codigoPropuesta);
					int orden = datosdetaprop.Rows.Count + 1;
					clsDetallePropuestaDePedido aux = convertirFilaEnClase(det[0], orden);
					clsDetallePropuestaDePedido verificador = admPropuesta.getDetallePropuestaxCodProducto(aux.Codigo_Producto, codigoPropuesta);
					if (verificador == null)
					{
						List<clsDetallePropuestaDePedido> lista = new List<clsDetallePropuestaDePedido>();
						lista.Add(aux);
						List<clsDetallePropuestaDePedido> lista2 = new List<clsDetallePropuestaDePedido>();
						if (admPropuesta.actualizaDetallePropuestaRecalculo(lista, lista2, lista2))
						{
							if (ctdad_a_mostrar > 0)
							{
								agregados = agregados + "\n>" + fila2.Cells["colReferenciaProducto"].Value.ToString() + " -- " + fila2.Cells["colDescripProducto"].Value.ToString();
								ctdad_a_mostrar--;
							}
							else
							{
								ctdad_agregados++;
							}
						}
						else if (ctdad_a_mostrar > 0)
						{
							no_agregados = no_agregados + "\n>" + fila2.Cells["colReferenciaProducto"].Value.ToString() + " -- " + fila2.Cells["colDescripProducto"].Value.ToString();
							ctdad_a_mostrar--;
						}
						else
						{
							ctdad_noagregados++;
						}
					}
					else if (ctdad_a_mostrar > 0)
					{
						no_agregados = no_agregados + "\n>" + fila2.Cells["colReferenciaProducto"].Value.ToString() + " -- " + fila2.Cells["colDescripProducto"].Value.ToString();
						ctdad_a_mostrar--;
					}
					else
					{
						ctdad_noagregados++;
					}
				}
				else if (ctdad_a_mostrar > 0)
				{
					no_agregados = no_agregados + "\n>" + fila2.Cells["colReferenciaProducto"].Value.ToString() + " -- " + fila2.Cells["colDescripProducto"].Value.ToString();
					ctdad_a_mostrar--;
				}
				else
				{
					ctdad_noagregados++;
				}
			}
			if (no_agregados != "")
			{
				string aux2 = ((ctdad_noagregados > 0) ? ("\ny " + ctdad_noagregados + " no se pudieron agregar") : "");
				MessageBox.Show("Los sgtes item no se agregaron:" + no_agregados + aux2, "Agregando item a propuesta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			base.DialogResult = DialogResult.Yes;
			Close();
		}
		else
		{
			MessageBox.Show("Seleccione items para agregarlo a la propuesta.", "Agregando items a propuesta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dgvTodosProductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void rgvTodosProductos_RowFormatting(object sender, RowFormattingEventArgs e)
	{
	}

	private void SetFormatoCondicionalaFilas()
	{
		ConditionalFormattingObject c1 = new ConditionalFormattingObject("Formato Aplicada a fila entera", ConditionTypes.Equal, "0", "", applyToRow: true);
		c1.ApplyOnSelectedRows = false;
		c1.RowBackColor = Color.FromArgb(255, 126, 121);
		c1.CellBackColor = Color.FromArgb(255, 126, 121);
		c1.RowForeColor = Color.Black;
		c1.CellForeColor = Color.Black;
		rgvTodosProductos.Columns["colCtdadSugerida"].ConditionalFormattingObjectList.Add(c1);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTodosProductosPropuestaOrdenDeCompra));
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.label1 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		this.btnAgregarItem = new System.Windows.Forms.Button();
		this.rgvTodosProductos = new Telerik.WinControls.UI.RadGridView();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(716, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Fecha";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(820, 325);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 4;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.dtpFechaPago.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFechaPago.CustomFormat = "dd/MM/yyyy hh:mm:ss";
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
		this.dtpFechaPago.Location = new System.Drawing.Point(759, 12);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(142, 20);
		this.dtpFechaPago.TabIndex = 80;
		this.dtpFechaPago.Tag = "16";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 3;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(729, 325);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(85, 32);
		this.button1.TabIndex = 83;
		this.button1.Text = "Reporte";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnAgregarItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAgregarItem.ImageIndex = 1;
		this.btnAgregarItem.ImageList = this.imageList1;
		this.btnAgregarItem.Location = new System.Drawing.Point(565, 325);
		this.btnAgregarItem.Name = "btnAgregarItem";
		this.btnAgregarItem.Size = new System.Drawing.Size(158, 32);
		this.btnAgregarItem.TabIndex = 84;
		this.btnAgregarItem.Text = "Agregar item a propuesta";
		this.btnAgregarItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAgregarItem.UseVisualStyleBackColor = true;
		this.btnAgregarItem.Click += new System.EventHandler(btnAgregarItem_Click);
		this.rgvTodosProductos.AutoScroll = true;
		this.rgvTodosProductos.EnableGestures = false;
		this.rgvTodosProductos.Location = new System.Drawing.Point(13, 38);
		this.rgvTodosProductos.MasterTemplate.AllowAddNewRow = false;
		this.rgvTodosProductos.MasterTemplate.AllowColumnChooser = false;
		this.rgvTodosProductos.MasterTemplate.AllowDeleteRow = false;
		this.rgvTodosProductos.MasterTemplate.AllowDragToGroup = false;
		this.rgvTodosProductos.MasterTemplate.AllowEditRow = false;
		this.rgvTodosProductos.MasterTemplate.AllowRowResize = false;
		this.rgvTodosProductos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.AllowFiltering = false;
		gridViewTextBoxColumn1.FieldName = "nroItem";
		gridViewTextBoxColumn1.HeaderText = "Item";
		gridViewTextBoxColumn1.Name = "colNroItem";
		gridViewTextBoxColumn1.Width = 35;
		gridViewTextBoxColumn2.FieldName = "codigoDetallePropuesta";
		gridViewTextBoxColumn2.HeaderText = "CodigoDetallePropuesta";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodigoDetallePropuesta";
		gridViewTextBoxColumn3.FieldName = "codProd";
		gridViewTextBoxColumn3.HeaderText = "CodigoProducto";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "colCodigoProducto";
		gridViewTextBoxColumn4.FieldName = "refProd";
		gridViewTextBoxColumn4.HeaderText = "Referencia";
		gridViewTextBoxColumn4.Name = "colReferenciaProducto";
		gridViewTextBoxColumn4.Width = 101;
		gridViewTextBoxColumn5.FieldName = "descripProd";
		gridViewTextBoxColumn5.HeaderText = "Descripcion";
		gridViewTextBoxColumn5.Name = "colDescripProducto";
		gridViewTextBoxColumn5.Width = 253;
		gridViewTextBoxColumn6.FieldName = "codUnidad";
		gridViewTextBoxColumn6.HeaderText = "CodigoUnidad";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "colCodUnidad";
		gridViewTextBoxColumn7.FieldName = "descripUnidad";
		gridViewTextBoxColumn7.HeaderText = "Unidad";
		gridViewTextBoxColumn7.Name = "colDescripUnidad";
		gridViewTextBoxColumn7.Width = 101;
		gridViewTextBoxColumn8.FieldName = "stockDisponible";
		gridViewTextBoxColumn8.HeaderText = "Stock Disponible";
		gridViewTextBoxColumn8.Name = "colStockDisponible";
		gridViewTextBoxColumn8.Width = 101;
		gridViewTextBoxColumn9.FieldName = "ctdadReponer";
		gridViewTextBoxColumn9.HeaderText = "Ctdad a Reponer";
		gridViewTextBoxColumn9.Name = "colCtdadReponer";
		gridViewTextBoxColumn9.Width = 101;
		gridViewTextBoxColumn10.FieldName = "ctdadSugerida";
		gridViewTextBoxColumn10.HeaderText = "Ctdad Sugerida";
		gridViewTextBoxColumn10.Name = "colCtdadSugerida";
		gridViewTextBoxColumn10.Width = 101;
		gridViewTextBoxColumn11.FieldName = "pedidoFinal";
		gridViewTextBoxColumn11.HeaderText = "Pedido Final";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "colPedidoFinal";
		gridViewTextBoxColumn12.FieldName = "precioUnitarioCompra";
		gridViewTextBoxColumn12.HeaderText = "Precio Unit. Compra";
		gridViewTextBoxColumn12.Name = "colPrecioUnit";
		gridViewTextBoxColumn12.Width = 101;
		gridViewTextBoxColumn13.FieldName = "opcionRecuento";
		gridViewTextBoxColumn13.HeaderText = "OpcionRecuento";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "colOpcionRecuento";
		gridViewTextBoxColumn14.FieldName = "stockMinimo";
		gridViewTextBoxColumn14.HeaderText = "StockMinimo";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "colStockMinimo";
		gridViewTextBoxColumn15.FieldName = "stockMaximo";
		gridViewTextBoxColumn15.HeaderText = "StockMaximo";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "colStockMaximo";
		gridViewTextBoxColumn16.FieldName = "undxPaquete";
		gridViewTextBoxColumn16.HeaderText = "undXPaquete";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "colUndXPaquete";
		this.rgvTodosProductos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16);
		this.rgvTodosProductos.MasterTemplate.EnableFiltering = true;
		this.rgvTodosProductos.MasterTemplate.EnableGrouping = false;
		this.rgvTodosProductos.MasterTemplate.MultiSelect = true;
		this.rgvTodosProductos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvTodosProductos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvTodosProductos.Name = "rgvTodosProductos";
		this.rgvTodosProductos.Size = new System.Drawing.Size(888, 281);
		this.rgvTodosProductos.TabIndex = 85;
		this.rgvTodosProductos.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(rgvTodosProductos_RowFormatting);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(912, 369);
		base.Controls.Add(this.rgvTodosProductos);
		base.Controls.Add(this.btnAgregarItem);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dtpFechaPago);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.label1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTodosProductosPropuestaOrdenDeCompra";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Todos Los Productos de la Propuesta de Pedido de Orden de Compra";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmProductosLista_FormClosing);
		base.Load += new System.EventHandler(frmProductosLista_Load);
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
