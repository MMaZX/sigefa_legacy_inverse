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

public class frmProductosNoAtendidosGRC : Office2007Form
{
	private BindingSource data = new BindingSource();

	private DataTable tablaDatos = new DataTable();

	public List<clsDetalleGuiaRemisionCompra> listaDatos = new List<clsDetalleGuiaRemisionCompra>();

	public int codalmacen = 0;

	public DateTime fecha_mostrar;

	public int codigoPropuesta = 0;

	private clsAdmPropuestaDePedido admPropuesta = new clsAdmPropuestaDePedido();

	public frmGuiaRemisionCompra ventana = null;

	private clsAdmProducto admProd = new clsAdmProducto();

	private int filaIndex = -1;

	private string referenciaProd = "";

	internal int estadoPropuesta;

	private IContainer components = null;

	private Label label1;

	private DataGridView dgvTodosProductos;

	private ImageList imageList1;

	private Button btnAceptar;

	private DateTimePicker dtpFechaPago;

	private Button button1;

	private Button btnAgregarItem;

	private DataGridViewTextBoxColumn colCodDetalleGR;

	private DataGridViewTextBoxColumn colCodDetalleOrdenCompra;

	private DataGridViewTextBoxColumn colCodOrdenCompra;

	private DataGridViewTextBoxColumn colCodProducto;

	private DataGridViewTextBoxColumn colReferencia;

	private DataGridViewTextBoxColumn colDescripcion;

	private DataGridViewTextBoxColumn colMoneda;

	private DataGridViewTextBoxColumn colCodUnidad;

	private DataGridViewTextBoxColumn colUnidad;

	private DataGridViewTextBoxColumn colSerieLote;

	private DataGridViewTextBoxColumn colCantidad;

	private DataGridViewTextBoxColumn colCantidadRespaldo;

	private DataGridViewTextBoxColumn colPrecioUnit;

	private DataGridViewTextBoxColumn colImporte;

	private DataGridViewTextBoxColumn coldscto1;

	private DataGridViewTextBoxColumn coldscto2;

	private DataGridViewTextBoxColumn coldscto3;

	private DataGridViewTextBoxColumn colmontodscto;

	private DataGridViewTextBoxColumn colvalorventa;

	private DataGridViewTextBoxColumn colvalorventaconflete;

	private DataGridViewTextBoxColumn coligv;

	private DataGridViewTextBoxColumn colflete;

	private DataGridViewTextBoxColumn colprecioventa;

	private DataGridViewTextBoxColumn colpvconflete;

	private DataGridViewTextBoxColumn colprecioreal;

	private DataGridViewTextBoxColumn colvaloreal;

	private DataGridViewTextBoxColumn colfechaingreso;

	private DataGridViewTextBoxColumn colcoduser;

	private DataGridViewTextBoxColumn colfecharegistro;

	private DataGridViewTextBoxColumn colcodDocumentoRelacionado;

	private DataGridViewTextBoxColumn colfletesinigv;

	private DataGridViewTextBoxColumn colfleteconigv;

	private RadGridView rgvTodosProductos;

	public frmProductosNoAtendidosGRC()
	{
		InitializeComponent();
	}

	private void frmProductosLista_Load(object sender, EventArgs e)
	{
		inicializaTablaConDgv();
		convertirListaADGV(listaDatos, tablaDatos);
		dgvTodosProductos.DataSource = tablaDatos;
		rgvTodosProductos.DataSource = tablaDatos;
	}

	private void inicializaTablaConDgv()
	{
		foreach (DataGridViewColumn col in dgvTodosProductos.Columns)
		{
			tablaDatos.Columns.Add(col.DataPropertyName);
		}
	}

	private void convertirListaADGV(List<clsDetalleGuiaRemisionCompra> listaDatos, DataTable tablaDatos)
	{
		foreach (clsDetalleGuiaRemisionCompra deta in listaDatos)
		{
			DataRow Ftemp = this.tablaDatos.NewRow();
			Ftemp.SetField(colCodDetalleGR.DataPropertyName, (object)deta.ICodDetalleGuiaRemisionCompra);
			Ftemp.SetField(colCodDetalleOrdenCompra.DataPropertyName, (object)deta.ICodDetalleOrdenCOmpra);
			Ftemp.SetField(colCodOrdenCompra.DataPropertyName, (object)deta.ICodOrdenCOmpra);
			Ftemp.SetField(colCodProducto.DataPropertyName, (object)deta.ICodProducto);
			Ftemp.SetField(colReferencia.DataPropertyName, (object)deta.SReferencia);
			Ftemp.SetField(colDescripcion.DataPropertyName, (object)deta.SDescripcion);
			Ftemp.SetField(colMoneda.DataPropertyName, (object)deta.IcodMoneda);
			Ftemp.SetField(colCodUnidad.DataPropertyName, (object)deta.IUnidadIngresada);
			Ftemp.SetField(colUnidad.DataPropertyName, (object)deta.SUnidad);
			Ftemp.SetField(colCantidad.DataPropertyName, (object)deta.DCantidad);
			Ftemp.SetField(colCantidadRespaldo.DataPropertyName, (object)deta.DCantidadRespaldo);
			Ftemp.SetField(colfechaingreso.DataPropertyName, (object)deta.FFechaIngreso);
			Ftemp.SetField(colcoduser.DataPropertyName, (object)deta.ICOdUser);
			Ftemp.SetField(colfecharegistro.DataPropertyName, (object)deta.FFechaRegistro);
			tablaDatos.Rows.Add(Ftemp);
		}
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

	private void dgvTodosProductos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
	}

	private clsDetallePropuestaDePedido convertirFilaEnClase(DataRow fila)
	{
		return new clsDetallePropuestaDePedido();
	}

	private void btnAgregarItem_Click(object sender, EventArgs e)
	{
		if (filaIndex != -1)
		{
			bool band_error = false;
			bool cerrar = false;
			referenciaProd = rgvTodosProductos.Rows[filaIndex].Cells["colReferencia"].Value.ToString();
			DialogResult rpta = MessageBox.Show("Esta seguro de agregar el item:\n> " + referenciaProd, "Agregando item a guia remision compra", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta != DialogResult.Yes)
			{
				return;
			}
			if (ventana != null)
			{
				ventana.codProdNoAtendido = Convert.ToInt32(rgvTodosProductos.Rows[filaIndex].Cells["colCodProducto"].Value);
				GridViewRowInfo fila_actual = rgvTodosProductos.Rows[filaIndex];
				List<clsDetalleGuiaRemisionCompra> buscado = Enumerable.Where<clsDetalleGuiaRemisionCompra>(listaDatos.AsEnumerable(), (Func<clsDetalleGuiaRemisionCompra, bool>)((clsDetalleGuiaRemisionCompra x) => x.ICodDetalleOrdenCOmpra == Convert.ToInt32(fila_actual.Cells["colCodDetalleOrdenCompra"].Value) && x.ICodDetalleGuiaRemisionCompra == Convert.ToInt32(fila_actual.Cells["colCodDetalleGR"].Value))).ToList();
				if (buscado.Count > 0)
				{
					ventana.prodNoAtendido = buscado[0];
				}
				else
				{
					band_error = true;
				}
				if (band_error)
				{
					base.DialogResult = DialogResult.No;
					MessageBox.Show("Ocurrio un error al intentar agregar el item.\nItem: " + referenciaProd + " NO agregado.", "Agregando item a guia remision compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					base.DialogResult = DialogResult.Yes;
					cerrar = true;
				}
				if (cerrar)
				{
					Close();
				}
			}
			else
			{
				MessageBox.Show("Ocurrio Un Error al Intentar de añadir el item.\nIntente colviendo a entrar en esta ventana", Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			MessageBox.Show("Seleccione un item para agregarlo a la guia remision compra.", "Agregando item a guia remision compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dgvTodosProductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			filaIndex = e.RowIndex;
		}
	}

	private void rgvTodosProductos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex != -1)
		{
			filaIndex = e.RowIndex;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductosNoAtendidosGRC));
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn31 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn32 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvTodosProductos = new System.Windows.Forms.DataGridView();
		this.colCodDetalleGR = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodDetalleOrdenCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodOrdenCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colSerieLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidadRespaldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colPrecioUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coldscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coldscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coldscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colmontodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colvalorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colvalorventaconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coligv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colprecioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colpvconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colprecioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colvaloreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcoduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodDocumentoRelacionado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfletesinigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfleteconigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.button1 = new System.Windows.Forms.Button();
		this.btnAgregarItem = new System.Windows.Forms.Button();
		this.rgvTodosProductos = new Telerik.WinControls.UI.RadGridView();
		((System.ComponentModel.ISupportInitialize)this.dgvTodosProductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(555, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Fecha";
		this.label1.Visible = false;
		this.dgvTodosProductos.AllowUserToAddRows = false;
		this.dgvTodosProductos.AllowUserToDeleteRows = false;
		this.dgvTodosProductos.AllowUserToResizeColumns = false;
		this.dgvTodosProductos.AllowUserToResizeRows = false;
		this.dgvTodosProductos.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvTodosProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvTodosProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTodosProductos.Columns.AddRange(this.colCodDetalleGR, this.colCodDetalleOrdenCompra, this.colCodOrdenCompra, this.colCodProducto, this.colReferencia, this.colDescripcion, this.colMoneda, this.colCodUnidad, this.colUnidad, this.colSerieLote, this.colCantidad, this.colCantidadRespaldo, this.colPrecioUnit, this.colImporte, this.coldscto1, this.coldscto2, this.coldscto3, this.colmontodscto, this.colvalorventa, this.colvalorventaconflete, this.coligv, this.colflete, this.colprecioventa, this.colpvconflete, this.colprecioreal, this.colvaloreal, this.colfechaingreso, this.colcoduser, this.colfecharegistro, this.colcodDocumentoRelacionado, this.colfletesinigv, this.colfleteconigv);
		this.dgvTodosProductos.Location = new System.Drawing.Point(13, 38);
		this.dgvTodosProductos.Name = "dgvTodosProductos";
		this.dgvTodosProductos.ReadOnly = true;
		this.dgvTodosProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvTodosProductos.RowHeadersVisible = false;
		this.dgvTodosProductos.RowHeadersWidth = 40;
		this.dgvTodosProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvTodosProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTodosProductos.Size = new System.Drawing.Size(727, 281);
		this.dgvTodosProductos.StandardTab = true;
		this.dgvTodosProductos.TabIndex = 3;
		this.dgvTodosProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTodosProductos_CellClick);
		this.dgvTodosProductos.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvTodosProductos_RowPostPaint);
		this.colCodDetalleGR.DataPropertyName = "codDetalleGuiaRemision";
		this.colCodDetalleGR.HeaderText = "CodDetalleGuiaRemision";
		this.colCodDetalleGR.Name = "colCodDetalleGR";
		this.colCodDetalleGR.ReadOnly = true;
		this.colCodDetalleGR.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCodDetalleGR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCodDetalleGR.Visible = false;
		this.colCodDetalleOrdenCompra.DataPropertyName = "codDetalleOrdenCompra";
		this.colCodDetalleOrdenCompra.HeaderText = "codDetalleOrdenCompra";
		this.colCodDetalleOrdenCompra.Name = "colCodDetalleOrdenCompra";
		this.colCodDetalleOrdenCompra.ReadOnly = true;
		this.colCodDetalleOrdenCompra.Visible = false;
		this.colCodOrdenCompra.DataPropertyName = "CodOrdenCompra";
		this.colCodOrdenCompra.HeaderText = "CodOrdenCompra";
		this.colCodOrdenCompra.Name = "colCodOrdenCompra";
		this.colCodOrdenCompra.ReadOnly = true;
		this.colCodOrdenCompra.Visible = false;
		this.colCodProducto.DataPropertyName = "codProducto";
		this.colCodProducto.HeaderText = "CodProducto";
		this.colCodProducto.Name = "colCodProducto";
		this.colCodProducto.ReadOnly = true;
		this.colCodProducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCodProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCodProducto.Visible = false;
		this.colReferencia.DataPropertyName = "referencia";
		this.colReferencia.FillWeight = 85.06322f;
		this.colReferencia.HeaderText = "Referencia";
		this.colReferencia.MinimumWidth = 100;
		this.colReferencia.Name = "colReferencia";
		this.colReferencia.ReadOnly = true;
		this.colDescripcion.DataPropertyName = "producto";
		this.colDescripcion.FillWeight = 110.7833f;
		this.colDescripcion.HeaderText = "Descripcion";
		this.colDescripcion.MinimumWidth = 250;
		this.colDescripcion.Name = "colDescripcion";
		this.colDescripcion.ReadOnly = true;
		this.colMoneda.DataPropertyName = "moneda";
		this.colMoneda.HeaderText = "Moneda";
		this.colMoneda.Name = "colMoneda";
		this.colMoneda.ReadOnly = true;
		this.colMoneda.Visible = false;
		this.colCodUnidad.DataPropertyName = "codUnidadMedida";
		this.colCodUnidad.HeaderText = "Cod. Unidad";
		this.colCodUnidad.Name = "colCodUnidad";
		this.colCodUnidad.ReadOnly = true;
		this.colCodUnidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCodUnidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCodUnidad.Visible = false;
		this.colUnidad.DataPropertyName = "unidad";
		this.colUnidad.FillWeight = 176.6497f;
		this.colUnidad.HeaderText = "Unidad";
		this.colUnidad.MinimumWidth = 100;
		this.colUnidad.Name = "colUnidad";
		this.colUnidad.ReadOnly = true;
		this.colUnidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colUnidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colSerieLote.DataPropertyName = "serielote";
		this.colSerieLote.HeaderText = "Serie/Lote";
		this.colSerieLote.Name = "colSerieLote";
		this.colSerieLote.ReadOnly = true;
		this.colSerieLote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colSerieLote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colSerieLote.Visible = false;
		this.colCantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.colCantidad.DefaultCellStyle = dataGridViewCellStyle1;
		this.colCantidad.FillWeight = 27.50378f;
		this.colCantidad.HeaderText = "Cantidad";
		this.colCantidad.MinimumWidth = 100;
		this.colCantidad.Name = "colCantidad";
		this.colCantidad.ReadOnly = true;
		this.colCantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colCantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colCantidadRespaldo.DataPropertyName = "cantidadrespaldo";
		this.colCantidadRespaldo.HeaderText = "Cant. Respaldo";
		this.colCantidadRespaldo.Name = "colCantidadRespaldo";
		this.colCantidadRespaldo.ReadOnly = true;
		this.colCantidadRespaldo.Visible = false;
		this.colPrecioUnit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N4";
		dataGridViewCellStyle2.NullValue = null;
		this.colPrecioUnit.DefaultCellStyle = dataGridViewCellStyle2;
		this.colPrecioUnit.HeaderText = "P. Unit.";
		this.colPrecioUnit.Name = "colPrecioUnit";
		this.colPrecioUnit.ReadOnly = true;
		this.colPrecioUnit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colPrecioUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colPrecioUnit.Visible = false;
		this.colImporte.DataPropertyName = "subtotal";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N4";
		dataGridViewCellStyle3.NullValue = null;
		this.colImporte.DefaultCellStyle = dataGridViewCellStyle3;
		this.colImporte.HeaderText = "Importe";
		this.colImporte.Name = "colImporte";
		this.colImporte.ReadOnly = true;
		this.colImporte.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colImporte.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colImporte.Visible = false;
		this.coldscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N4";
		this.coldscto1.DefaultCellStyle = dataGridViewCellStyle4;
		this.coldscto1.HeaderText = "% Dscto1";
		this.coldscto1.Name = "coldscto1";
		this.coldscto1.ReadOnly = true;
		this.coldscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coldscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coldscto1.Visible = false;
		this.coldscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N4";
		this.coldscto2.DefaultCellStyle = dataGridViewCellStyle5;
		this.coldscto2.HeaderText = "% Dscto2";
		this.coldscto2.Name = "coldscto2";
		this.coldscto2.ReadOnly = true;
		this.coldscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coldscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coldscto2.Visible = false;
		this.coldscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N4";
		this.coldscto3.DefaultCellStyle = dataGridViewCellStyle6;
		this.coldscto3.HeaderText = "% Dscto3";
		this.coldscto3.Name = "coldscto3";
		this.coldscto3.ReadOnly = true;
		this.coldscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coldscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coldscto3.Visible = false;
		this.colmontodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N4";
		dataGridViewCellStyle7.NullValue = null;
		this.colmontodscto.DefaultCellStyle = dataGridViewCellStyle7;
		this.colmontodscto.HeaderText = "Monto Dscto";
		this.colmontodscto.Name = "colmontodscto";
		this.colmontodscto.ReadOnly = true;
		this.colmontodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colmontodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colmontodscto.Visible = false;
		this.colvalorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N4";
		dataGridViewCellStyle8.NullValue = null;
		this.colvalorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.colvalorventa.HeaderText = "V. Venta";
		this.colvalorventa.Name = "colvalorventa";
		this.colvalorventa.ReadOnly = true;
		this.colvalorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colvalorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colvalorventa.Visible = false;
		this.colvalorventaconflete.DataPropertyName = "vvconflete";
		this.colvalorventaconflete.HeaderText = "vvconflete";
		this.colvalorventaconflete.Name = "colvalorventaconflete";
		this.colvalorventaconflete.ReadOnly = true;
		this.colvalorventaconflete.Visible = false;
		this.coligv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N4";
		this.coligv.DefaultCellStyle = dataGridViewCellStyle9;
		this.coligv.HeaderText = "IGV";
		this.coligv.Name = "coligv";
		this.coligv.ReadOnly = true;
		this.coligv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coligv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coligv.Visible = false;
		this.colflete.DataPropertyName = "flete";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.colflete.DefaultCellStyle = dataGridViewCellStyle10;
		this.colflete.HeaderText = "Flete";
		this.colflete.Name = "colflete";
		this.colflete.ReadOnly = true;
		this.colflete.Visible = false;
		this.colprecioventa.DataPropertyName = "importe";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N4";
		this.colprecioventa.DefaultCellStyle = dataGridViewCellStyle11;
		this.colprecioventa.HeaderText = "P. Venta";
		this.colprecioventa.Name = "colprecioventa";
		this.colprecioventa.ReadOnly = true;
		this.colprecioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colprecioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colprecioventa.Visible = false;
		this.colpvconflete.DataPropertyName = "pvconflete";
		this.colpvconflete.HeaderText = "pvconflete";
		this.colpvconflete.Name = "colpvconflete";
		this.colpvconflete.ReadOnly = true;
		this.colpvconflete.Visible = false;
		this.colprecioreal.DataPropertyName = "precioreal";
		this.colprecioreal.HeaderText = "P. real";
		this.colprecioreal.Name = "colprecioreal";
		this.colprecioreal.ReadOnly = true;
		this.colprecioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colprecioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colprecioreal.Visible = false;
		this.colvaloreal.DataPropertyName = "valoreal";
		this.colvaloreal.HeaderText = "V. real";
		this.colvaloreal.Name = "colvaloreal";
		this.colvaloreal.ReadOnly = true;
		this.colvaloreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.colvaloreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.colvaloreal.Visible = false;
		this.colfechaingreso.DataPropertyName = "fechaingreso";
		this.colfechaingreso.HeaderText = "FechaIngre";
		this.colfechaingreso.Name = "colfechaingreso";
		this.colfechaingreso.ReadOnly = true;
		this.colfechaingreso.Visible = false;
		this.colcoduser.DataPropertyName = "codUser";
		this.colcoduser.HeaderText = "CodUser";
		this.colcoduser.Name = "colcoduser";
		this.colcoduser.ReadOnly = true;
		this.colcoduser.Visible = false;
		this.colfecharegistro.DataPropertyName = "fecharegistro";
		this.colfecharegistro.HeaderText = "Fecha Reg";
		this.colfecharegistro.Name = "colfecharegistro";
		this.colfecharegistro.ReadOnly = true;
		this.colfecharegistro.Visible = false;
		this.colcodDocumentoRelacionado.DataPropertyName = "codDocumentoRelacionado";
		this.colcodDocumentoRelacionado.HeaderText = "codDocumentoRelacionado";
		this.colcodDocumentoRelacionado.Name = "colcodDocumentoRelacionado";
		this.colcodDocumentoRelacionado.ReadOnly = true;
		this.colcodDocumentoRelacionado.Visible = false;
		this.colfletesinigv.DataPropertyName = "fletesinigv";
		this.colfletesinigv.HeaderText = "Flete Sin Igv";
		this.colfletesinigv.Name = "colfletesinigv";
		this.colfletesinigv.ReadOnly = true;
		this.colfletesinigv.Visible = false;
		this.colfleteconigv.DataPropertyName = "fleteconigv";
		this.colfleteconigv.HeaderText = "Flete Con Igv";
		this.colfleteconigv.Name = "colfleteconigv";
		this.colfleteconigv.ReadOnly = true;
		this.colfleteconigv.Visible = false;
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
		this.btnAceptar.Location = new System.Drawing.Point(659, 325);
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
		this.dtpFechaPago.Location = new System.Drawing.Point(598, 12);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(142, 20);
		this.dtpFechaPago.TabIndex = 80;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 3;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(202, 325);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(85, 32);
		this.button1.TabIndex = 83;
		this.button1.Text = "Reporte";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnAgregarItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAgregarItem.ImageIndex = 1;
		this.btnAgregarItem.ImageList = this.imageList1;
		this.btnAgregarItem.Location = new System.Drawing.Point(402, 325);
		this.btnAgregarItem.Name = "btnAgregarItem";
		this.btnAgregarItem.Size = new System.Drawing.Size(251, 32);
		this.btnAgregarItem.TabIndex = 84;
		this.btnAgregarItem.Text = "Agregar Producto A Guia Remision Compra";
		this.btnAgregarItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAgregarItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAgregarItem.UseVisualStyleBackColor = true;
		this.btnAgregarItem.Click += new System.EventHandler(btnAgregarItem_Click);
		this.rgvTodosProductos.AutoScroll = true;
		this.rgvTodosProductos.EnableGestures = false;
		this.rgvTodosProductos.EnableTheming = false;
		this.rgvTodosProductos.Location = new System.Drawing.Point(13, 38);
		this.rgvTodosProductos.MasterTemplate.AllowAddNewRow = false;
		this.rgvTodosProductos.MasterTemplate.AllowColumnChooser = false;
		this.rgvTodosProductos.MasterTemplate.AllowColumnReorder = false;
		this.rgvTodosProductos.MasterTemplate.AllowDeleteRow = false;
		this.rgvTodosProductos.MasterTemplate.AllowDragToGroup = false;
		this.rgvTodosProductos.MasterTemplate.AllowEditRow = false;
		this.rgvTodosProductos.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codDetalleGuiaRemision";
		gridViewTextBoxColumn1.HeaderText = "CodDetalleGuiaRemision";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodDetalleGR";
		gridViewTextBoxColumn2.FieldName = "codDetalleOrdenCompra";
		gridViewTextBoxColumn2.HeaderText = "codDetalleOrdenCompra";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodDetalleOrdenCompra";
		gridViewTextBoxColumn3.FieldName = "CodOrdenCompra";
		gridViewTextBoxColumn3.HeaderText = "CodOrdenCompra";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "colCodOrdenCompra";
		gridViewTextBoxColumn4.FieldName = "codProducto";
		gridViewTextBoxColumn4.HeaderText = "CodProducto";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "colCodProducto";
		gridViewTextBoxColumn5.FieldName = "referencia";
		gridViewTextBoxColumn5.HeaderText = "Referencia";
		gridViewTextBoxColumn5.Name = "colReferencia";
		gridViewTextBoxColumn5.Width = 104;
		gridViewTextBoxColumn6.FieldName = "producto";
		gridViewTextBoxColumn6.HeaderText = "Descripcion";
		gridViewTextBoxColumn6.Name = "colDescripcion";
		gridViewTextBoxColumn6.Width = 272;
		gridViewTextBoxColumn7.FieldName = "moneda";
		gridViewTextBoxColumn7.HeaderText = "Moneda";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "colMoneda";
		gridViewTextBoxColumn8.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn8.HeaderText = "Cod. Unidad";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colCodUnidad";
		gridViewTextBoxColumn9.FieldName = "unidad";
		gridViewTextBoxColumn9.HeaderText = "Unidad";
		gridViewTextBoxColumn9.Name = "colUnidad";
		gridViewTextBoxColumn9.Width = 209;
		gridViewTextBoxColumn10.FieldName = "serielote";
		gridViewTextBoxColumn10.HeaderText = "Serie/Lote";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "colSerieLote";
		gridViewTextBoxColumn11.FieldName = "cantidad";
		gridViewTextBoxColumn11.HeaderText = "Cantidad";
		gridViewTextBoxColumn11.Name = "colCantidad";
		gridViewTextBoxColumn11.Width = 140;
		gridViewTextBoxColumn12.FieldName = "cantidadrespaldo";
		gridViewTextBoxColumn12.HeaderText = "Canti. Respaldo";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "colCantidadRespaldo";
		gridViewTextBoxColumn13.FieldName = "preciounitario";
		gridViewTextBoxColumn13.HeaderText = "P. Unit.";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "colPrecioUnit";
		gridViewTextBoxColumn14.FieldName = "subtotal";
		gridViewTextBoxColumn14.HeaderText = "Importe";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "colImporte";
		gridViewTextBoxColumn15.FieldName = "descuento1";
		gridViewTextBoxColumn15.HeaderText = "% Dscto 1";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "coldscto1";
		gridViewTextBoxColumn16.FieldName = "descuento2";
		gridViewTextBoxColumn16.HeaderText = "% Dscto 2";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "coldscto2";
		gridViewTextBoxColumn17.FieldName = "descuento3";
		gridViewTextBoxColumn17.HeaderText = "% Dscto 3";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "coldscto3";
		gridViewTextBoxColumn18.FieldName = "montodscto";
		gridViewTextBoxColumn18.HeaderText = "Monto Dscto";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "colmontodscto";
		gridViewTextBoxColumn19.FieldName = "valorventa";
		gridViewTextBoxColumn19.HeaderText = "V. Venta";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "colvalorventa";
		gridViewTextBoxColumn20.FieldName = "vvconflete";
		gridViewTextBoxColumn20.HeaderText = "vvconflete";
		gridViewTextBoxColumn20.IsVisible = false;
		gridViewTextBoxColumn20.Name = "colvalorventaconflete";
		gridViewTextBoxColumn21.FieldName = "igv";
		gridViewTextBoxColumn21.HeaderText = "IGV";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "coligv";
		gridViewTextBoxColumn22.FieldName = "flete";
		gridViewTextBoxColumn22.HeaderText = "Flete";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "colflete";
		gridViewTextBoxColumn23.FieldName = "importe";
		gridViewTextBoxColumn23.HeaderText = "P. Venta";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "colprecioventa";
		gridViewTextBoxColumn24.FieldName = "pvconflete";
		gridViewTextBoxColumn24.HeaderText = "pvconflete";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "colpvconflete";
		gridViewTextBoxColumn25.FieldName = "precioreal";
		gridViewTextBoxColumn25.HeaderText = "P. real";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "colprecioreal";
		gridViewTextBoxColumn26.FieldName = "valoreal";
		gridViewTextBoxColumn26.HeaderText = "V. real";
		gridViewTextBoxColumn26.IsVisible = false;
		gridViewTextBoxColumn26.Name = "colvaloreal";
		gridViewTextBoxColumn27.FieldName = "fechaingreso";
		gridViewTextBoxColumn27.HeaderText = "FechaIngre";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "colfechaingreso";
		gridViewTextBoxColumn28.FieldName = "codUser";
		gridViewTextBoxColumn28.HeaderText = "CodUser";
		gridViewTextBoxColumn28.IsVisible = false;
		gridViewTextBoxColumn28.Name = "colcoduser";
		gridViewTextBoxColumn29.FieldName = "fecharegistro";
		gridViewTextBoxColumn29.HeaderText = "Fecha Reg";
		gridViewTextBoxColumn29.IsVisible = false;
		gridViewTextBoxColumn29.Name = "colfecharegistro";
		gridViewTextBoxColumn30.FieldName = "codDocumentoRelacionado";
		gridViewTextBoxColumn30.HeaderText = "codDocumentoRelacionado";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "colcodDocumentoRelacionado";
		gridViewTextBoxColumn31.FieldName = "fletesinigv";
		gridViewTextBoxColumn31.HeaderText = "Flete Sin Igv";
		gridViewTextBoxColumn31.IsVisible = false;
		gridViewTextBoxColumn31.Name = "colfletesinigv";
		gridViewTextBoxColumn32.FieldName = "fleteconigv";
		gridViewTextBoxColumn32.HeaderText = "Flete Con Igv";
		gridViewTextBoxColumn32.IsVisible = false;
		gridViewTextBoxColumn32.Name = "colfleteconigv";
		this.rgvTodosProductos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32);
		this.rgvTodosProductos.MasterTemplate.EnableFiltering = true;
		this.rgvTodosProductos.MasterTemplate.EnableGrouping = false;
		this.rgvTodosProductos.MasterTemplate.EnableSorting = false;
		this.rgvTodosProductos.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvTodosProductos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvTodosProductos.Name = "rgvTodosProductos";
		this.rgvTodosProductos.Size = new System.Drawing.Size(727, 281);
		this.rgvTodosProductos.TabIndex = 85;
		this.rgvTodosProductos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvTodosProductos_CellClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(751, 369);
		base.Controls.Add(this.rgvTodosProductos);
		base.Controls.Add(this.btnAgregarItem);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.dtpFechaPago);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dgvTodosProductos);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmProductosNoAtendidosGRC";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Listado de Productos No Atendidos";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmProductosLista_FormClosing);
		base.Load += new System.EventHandler(frmProductosLista_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvTodosProductos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvTodosProductos).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
