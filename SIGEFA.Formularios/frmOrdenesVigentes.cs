using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmOrdenesVigentes : Office2007Form
{
	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	private clsOrdenCompra Ord = new clsOrdenCompra();

	public int Proceso = 0;

	private clsDetalleGuiaRemision detguia = new clsDetalleGuiaRemision();

	private clsDetalleOrdenCompra detord = new clsDetalleOrdenCompra();

	public static BindingSource data = new BindingSource();

	public DataTable ordenes = null;

	private string filtro = string.Empty;

	public int tcvalida;

	public int codmovi = 0;

	public int codEstadoOrden = 0;

	public string h = "";

	private clsAdmFormulario admForm = new clsAdmFormulario();

	public clsUsuario usuario_click = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnSalir;

	private Button btnIrOrdenCompra;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private Button btnActualizar;

	private ImageList imageList2;

	private Button button1;

	private ComboBox cmbestadoOrden;

	private Label label1;

	private Button btnListarGR;

	public Button btneditarorden;

	private GroupBox groupBox2;

	private Label txtNombreProducto;

	private Label label3;

	private Button btnBuscarProducto;

	private Label label2;

	private TextBox txtCodprod;

	private Label label4;

	private Label label5;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private RadioButton rbFechaRegistro;

	private RadioButton rbFecha;

	private RadGridView rgvOrdenes;

	public frmOrdenesVigentes()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void CargaLista()
	{
		int tipoFecha = (rbFecha.Checked ? 1 : (rbFechaRegistro.Checked ? 2 : 0));
		rgvOrdenes.DataSource = data;
		data.DataSource = AdmOrden.MuestraOrdenes(frmLogin.iCodAlmacen, tipoFecha, dtpFecha1.Value, dtpFecha2.Value, 0, Convert.ToInt32((txtCodprod.Text == "") ? "0" : txtCodprod.Text));
		data.Filter = string.Empty;
		filtro = string.Empty;
		btneditarorden.Visible = true;
		rgvOrdenes.ClearSelection();
	}

	private frmOrdenCompra buscarFrmOC(string tipoFormulario, int codOC, int proceso)
	{
		frmOrdenCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmOrdenCompra)frm).CodOrdenCompra == codOC && ((frmOrdenCompra)frm).Proceso == proceso)
			{
				form = (frmOrdenCompra)frm;
				break;
			}
		}
		return form;
	}

	private void btnIrOrdenCompra_Click(object sender, EventArgs e)
	{
		if (rgvOrdenes.Rows.Count < 1 || rgvOrdenes.CurrentRow == null)
		{
			return;
		}
		frmOrdenCompra form = buscarFrmOC("frmOrdenCompra", Ord.CodOrdenCompra, 3);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmOrdenCompra();
		form.MdiParent = base.MdiParent;
		form.CodOrdenCompra = Ord.CodOrdenCompra;
		form.Proceso = 3;
		foreach (GridViewRowInfo fila1 in rgvOrdenes.SelectedRows)
		{
			form.label21.Text = fila1.Cells["codigo"].Value.ToString();
			form.label22.Text = fila1.Cells["codprov"].Value.ToString();
		}
		form.Show();
	}

	private void frmOrdenesVigentes_Load(object sender, EventArgs e)
	{
		listacomboOrden();
		CargaLista();
		btnAnular.Visible = frmLogin.AcesosUsuario.Contains(admForm.getPermisoAnularOrdenCompra());
		if (!btnAnular.Visible)
		{
			btnAnular.Visible = frmLogin.iNivelUser == 1;
		}
		btnAnular.Enabled = false;
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (rgvOrdenes.CurrentRow == null || Ord.CodOrdenCompra == 0)
		{
			return;
		}
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la orden de compra seleccionada", "Órdenes de Compra", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult != DialogResult.No)
		{
			usuario_click = null;
			DialogResult dr = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			frm.permiso = admForm.getPermisoAnularOrdenCompra();
			frm.tipoAccion = 2;
			frm.tipoVentanaAAsignarUsuario = 2;
			frm.ventanaOrdVig = this;
			frm.PermitirAdministradores = true;
			dr = frm.ShowDialog();
			if (dr == DialogResult.OK && AdmOrden.anular(Ord.CodOrdenCompra))
			{
				AdmOrden.registrarModificacionDeOC(Ord.CodOrdenCompra, usuario_click.CodUsuario, DateTime.Now);
				MessageBox.Show("La orden de compra ha sido anulada correctamente", "Órdenes de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (codEstadoOrden == 3)
		{
			if (rgvOrdenes.Rows.Count >= 1 && rgvOrdenes.CurrentRow != null)
			{
				frmNotaIngreso form = new frmNotaIngreso();
				form.MdiParent = base.MdiParent;
				form.CodOrdenCompra = Ord.CodOrdenCompra;
				form.Proceso = 7;
				form.txtDocRef.Text = "FT";
				form.Show();
			}
		}
		else
		{
			MessageBox.Show("La Orden No Esta Al 100%", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		frmGuiaRemision form1 = new frmGuiaRemision();
		form1.MdiParent = base.MdiParent;
		form1.Proceso = 1;
		form1.CodOrdenCompra = codmovi;
		form1.Show();
	}

	private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
	{
	}

	public void listacomboOrden()
	{
		cmbestadoOrden.DataSource = AdmOrden.listacomboOrden();
		cmbestadoOrden.DisplayMember = "nombre";
		cmbestadoOrden.ValueMember = "codorden";
		cmbestadoOrden.DropDownStyle = ComboBoxStyle.DropDownList;
	}

	private void cmbestadoOrden_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void button3_Click(object sender, EventArgs e)
	{
		if (rgvOrdenes.SelectedRows.Count > 0)
		{
			frmListadoGuiaRemisionCompra form = new frmListadoGuiaRemisionCompra();
			form.MdiParent = base.MdiParent;
			form.codOCAMostrar = codmovi;
			form.Show();
		}
	}

	private void btneditarorden_Click(object sender, EventArgs e)
	{
		if (rgvOrdenes.Rows.Count < 1 || rgvOrdenes.CurrentRow == null)
		{
			return;
		}
		if (codEstadoOrden == 1 || codEstadoOrden == 19)
		{
			frmOrdenCompra form = buscarFrmOC("frmOrdenCompra", Ord.CodOrdenCompra, 2);
			if (form != null)
			{
				form.Activate();
				return;
			}
			form = new frmOrdenCompra();
			form.MdiParent = base.MdiParent;
			form.CodOrdenCompra = Ord.CodOrdenCompra;
			form.Proceso = 2;
			form.Procede = 10;
			foreach (GridViewRowInfo fila1 in rgvOrdenes.SelectedRows)
			{
				form.label21.Text = fila1.Cells["codigo"].Value.ToString();
				form.label22.Text = fila1.Cells["codprov"].Value.ToString();
			}
			form.Show();
		}
		else
		{
			MessageBox.Show("LA ORDEN YA TIENE GUIAS, NO PUEDE SER EDITADA", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaLista();
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
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

	private void rgvOrdenes_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex >= 0)
			{
				if (rgvOrdenes.Rows.Count >= 1 && e.Row.IsCurrent)
				{
					Ord.CodOrdenCompra = Convert.ToInt32(e.Row.Cells["codigo"].Value);
				}
				if (e.RowIndex != -1)
				{
					codmovi = Convert.ToInt32(rgvOrdenes.Rows[e.RowIndex].Cells["codigo"].Value);
					codEstadoOrden = Convert.ToInt32(rgvOrdenes.Rows[e.RowIndex].Cells["colCodEstado"].Value);
				}
				if (codEstadoOrden == 1 || codEstadoOrden == 19)
				{
					btneditarorden.Enabled = true;
				}
				else
				{
					btneditarorden.Enabled = false;
				}
				if ((codEstadoOrden == 1 || codEstadoOrden == 5 || codEstadoOrden == 6) && Convert.ToInt32(rgvOrdenes.Rows[e.RowIndex].Cells["anulado"].Value) == 0)
				{
					btnAnular.Enabled = true;
				}
				else
				{
					btnAnular.Enabled = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvOrdenes_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex == -1 || rgvOrdenes.Rows.Count < 1 || rgvOrdenes.CurrentRow == null)
		{
			return;
		}
		frmOrdenCompra form = buscarFrmOC("frmOrdenCompra", Ord.CodOrdenCompra, 3);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmOrdenCompra();
		form.MdiParent = base.MdiParent;
		form.CodOrdenCompra = Ord.CodOrdenCompra;
		form.Proceso = 3;
		form.consultacombo = 100;
		foreach (GridViewRowInfo fila1 in rgvOrdenes.SelectedRows)
		{
			form.label21.Text = fila1.Cells["codigo"].Value.ToString();
			form.label22.Text = fila1.Cells["codprov"].Value.ToString();
		}
		codmovi = Convert.ToInt32(rgvOrdenes.Rows[e.RowIndex].Cells["codigo"].Value);
		codEstadoOrden = Convert.ToInt32(rgvOrdenes.Rows[e.RowIndex].Cells["colCodEstado"].Value);
		form.Show();
	}

	private void rgvOrdenes_FilterPopupRequired(object sender, FilterPopupRequiredEventArgs e)
	{
		if (!(e.Column.Name == "estadoOrden"))
		{
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmOrdenesVigentes));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvOrdenes = new Telerik.WinControls.UI.RadGridView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.button1 = new System.Windows.Forms.Button();
		this.cmbestadoOrden = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnListarGR = new System.Windows.Forms.Button();
		this.btneditarorden = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.rbFechaRegistro = new System.Windows.Forms.RadioButton();
		this.rbFecha = new System.Windows.Forms.RadioButton();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrOrdenCompra = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvOrdenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvOrdenes.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvOrdenes);
		this.groupBox1.Location = new System.Drawing.Point(0, 63);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1227, 302);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vigentes";
		this.rgvOrdenes.AutoScroll = true;
		this.rgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvOrdenes.EnableGestures = false;
		this.rgvOrdenes.Location = new System.Drawing.Point(3, 16);
		this.rgvOrdenes.MasterTemplate.AllowAddNewRow = false;
		this.rgvOrdenes.MasterTemplate.AllowColumnChooser = false;
		this.rgvOrdenes.MasterTemplate.AllowColumnReorder = false;
		this.rgvOrdenes.MasterTemplate.AllowDeleteRow = false;
		this.rgvOrdenes.MasterTemplate.AllowDragToGroup = false;
		this.rgvOrdenes.MasterTemplate.AllowEditRow = false;
		gridViewTextBoxColumn1.FieldName = "codOrdenCompra";
		gridViewTextBoxColumn1.HeaderText = "codigooc";
		gridViewTextBoxColumn1.Name = "codigo";
		gridViewTextBoxColumn1.Width = 80;
		gridViewTextBoxColumn2.FieldName = "tituloOrdenCompra";
		gridViewTextBoxColumn2.HeaderText = "Codigo";
		gridViewTextBoxColumn2.Name = "titulo";
		gridViewTextBoxColumn2.Width = 125;
		gridViewTextBoxColumn3.FieldName = "codprov";
		gridViewTextBoxColumn3.HeaderText = "codprov";
		gridViewTextBoxColumn3.Name = "codprov";
		gridViewTextBoxColumn3.Width = 100;
		gridViewTextBoxColumn4.FieldName = "razonsocial";
		gridViewTextBoxColumn4.HeaderText = "Proveedor";
		gridViewTextBoxColumn4.Name = "cliente";
		gridViewTextBoxColumn4.Width = 200;
		gridViewTextBoxColumn5.FieldName = "total";
		gridViewTextBoxColumn5.HeaderText = "Importe";
		gridViewTextBoxColumn5.Name = "importe";
		gridViewTextBoxColumn5.Width = 100;
		gridViewTextBoxColumn6.FieldName = "moneda";
		gridViewTextBoxColumn6.HeaderText = "Moneda";
		gridViewTextBoxColumn6.Name = "moneda";
		gridViewTextBoxColumn6.Width = 100;
		gridViewTextBoxColumn7.FieldName = "fecha";
		gridViewTextBoxColumn7.HeaderText = "Fecha OC";
		gridViewTextBoxColumn7.Name = "fecha";
		gridViewTextBoxColumn7.Width = 110;
		gridViewTextBoxColumn8.FieldName = "documento";
		gridViewTextBoxColumn8.HeaderText = "T. doc.";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "documento";
		gridViewTextBoxColumn9.FieldName = "responsable";
		gridViewTextBoxColumn9.HeaderText = "Creador";
		gridViewTextBoxColumn9.Name = "responsable";
		gridViewTextBoxColumn9.Width = 100;
		gridViewTextBoxColumn10.FieldName = "fechaRegistro";
		gridViewTextBoxColumn10.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn10.Name = "colFechaRegistro";
		gridViewTextBoxColumn10.Width = 110;
		gridViewTextBoxColumn11.FieldName = "modificador";
		gridViewTextBoxColumn11.HeaderText = "Modificador";
		gridViewTextBoxColumn11.Name = "colModificador";
		gridViewTextBoxColumn11.Width = 100;
		gridViewTextBoxColumn12.FieldName = "fechaModificacion";
		gridViewTextBoxColumn12.HeaderText = "Fecha Modificacion";
		gridViewTextBoxColumn12.Name = "colFechaModificacion";
		gridViewTextBoxColumn12.Width = 110;
		gridViewTextBoxColumn13.FieldName = "aprobador";
		gridViewTextBoxColumn13.HeaderText = "Aprobador";
		gridViewTextBoxColumn13.Name = "colAprobador";
		gridViewTextBoxColumn13.Width = 100;
		gridViewTextBoxColumn14.FieldName = "fechaAprobacion";
		gridViewTextBoxColumn14.HeaderText = "Fecha Aprobacion";
		gridViewTextBoxColumn14.Name = "colFechaAprobacion";
		gridViewTextBoxColumn14.Width = 110;
		gridViewTextBoxColumn15.FieldName = "estado";
		gridViewTextBoxColumn15.HeaderText = "Estado";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "estado";
		gridViewTextBoxColumn16.FieldName = "numrelacionadoOC";
		gridViewTextBoxColumn16.HeaderText = "numrelacionadoOC";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "numrelacionadoOC";
		gridViewTextBoxColumn17.FieldName = "estadoOrden";
		gridViewTextBoxColumn17.HeaderText = "Estado Orden";
		gridViewTextBoxColumn17.Name = "estadoOrden";
		gridViewTextBoxColumn17.Width = 120;
		gridViewTextBoxColumn18.FieldName = "fechavigencia";
		gridViewTextBoxColumn18.HeaderText = "Vig. Hasta";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "fechavence";
		gridViewTextBoxColumn19.FieldName = "codEstadoOrden";
		gridViewTextBoxColumn19.HeaderText = "colCodEstado";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "colCodEstado";
		gridViewTextBoxColumn20.FieldName = "guiasrelacionadas";
		gridViewTextBoxColumn20.HeaderText = "Guias Relacionadas";
		gridViewTextBoxColumn20.Name = "guiasrelacionadas";
		gridViewTextBoxColumn20.Width = 150;
		gridViewTextBoxColumn21.FieldName = "anulado";
		gridViewTextBoxColumn21.HeaderText = "anulado";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "anulado";
		gridViewTextBoxColumn22.FieldName = "propuesta";
		gridViewTextBoxColumn22.HeaderText = "Propuesta OC";
		gridViewTextBoxColumn22.Name = "propuesta";
		gridViewTextBoxColumn22.Width = 200;
		gridViewTextBoxColumn23.FieldName = "id_propuesta";
		gridViewTextBoxColumn23.HeaderText = "codporpuesta";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "id_propuesta";
		this.rgvOrdenes.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23);
		this.rgvOrdenes.MasterTemplate.EnableFiltering = true;
		this.rgvOrdenes.MasterTemplate.EnableGrouping = false;
		this.rgvOrdenes.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvOrdenes.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvOrdenes.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvOrdenes.Name = "rgvOrdenes";
		this.rgvOrdenes.ShowHeaderCellButtons = true;
		this.rgvOrdenes.Size = new System.Drawing.Size(1221, 283);
		this.rgvOrdenes.TabIndex = 1;
		this.rgvOrdenes.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvOrdenes_CellClick);
		this.rgvOrdenes.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvOrdenes_CellDoubleClick);
		this.rgvOrdenes.FilterPopupRequired += new Telerik.WinControls.UI.FilterPopupRequiredEventHandler(rgvOrdenes_FilterPopupRequired);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList2.Images.SetKeyName(1, "Add.png");
		this.imageList2.Images.SetKeyName(2, "Remove.png");
		this.imageList2.Images.SetKeyName(3, "Write Document.png");
		this.imageList2.Images.SetKeyName(4, "New Document.png");
		this.imageList2.Images.SetKeyName(5, "Remove Document.png");
		this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList2.Images.SetKeyName(7, "document-print.png");
		this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList2.Images.SetKeyName(9, "refresh_256.png");
		this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList2.Images.SetKeyName(11, "search (1).png");
		this.imageList2.Images.SetKeyName(12, "search (5).png");
		this.imageList2.Images.SetKeyName(13, "search (6).png");
		this.imageList2.Images.SetKeyName(14, "search (8).png");
		this.imageList2.Images.SetKeyName(15, "search_top.png");
		this.imageList2.Images.SetKeyName(16, "folder_open (1).png");
		this.imageList2.Images.SetKeyName(17, "folder-open-icon.png");
		this.imageList2.Images.SetKeyName(18, "Glossy-Open-icon.png");
		this.imageList2.Images.SetKeyName(19, "Ocean Blue Open.png");
		this.imageList2.Images.SetKeyName(20, "Open (1).png");
		this.imageList2.Images.SetKeyName(21, "open_folder_green.png");
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.Location = new System.Drawing.Point(1036, 372);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(77, 37);
		this.button1.TabIndex = 6;
		this.button1.Text = "Generar G.Remision";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.cmbestadoOrden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmbestadoOrden.FormattingEnabled = true;
		this.cmbestadoOrden.Location = new System.Drawing.Point(673, 380);
		this.cmbestadoOrden.Name = "cmbestadoOrden";
		this.cmbestadoOrden.Size = new System.Drawing.Size(121, 21);
		this.cmbestadoOrden.TabIndex = 8;
		this.cmbestadoOrden.Visible = false;
		this.cmbestadoOrden.SelectedValueChanged += new System.EventHandler(comboBox1_SelectedValueChanged);
		this.cmbestadoOrden.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbestadoOrden_KeyPress);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(627, 383);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(40, 13);
		this.label1.TabIndex = 9;
		this.label1.Text = "Estado";
		this.label1.Visible = false;
		this.btnListarGR.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnListarGR.Location = new System.Drawing.Point(299, 371);
		this.btnListarGR.Name = "btnListarGR";
		this.btnListarGR.Size = new System.Drawing.Size(77, 37);
		this.btnListarGR.TabIndex = 10;
		this.btnListarGR.Text = "Lista G.Remision";
		this.btnListarGR.UseVisualStyleBackColor = true;
		this.btnListarGR.Click += new System.EventHandler(button3_Click);
		this.btneditarorden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btneditarorden.Location = new System.Drawing.Point(114, 371);
		this.btneditarorden.Name = "btneditarorden";
		this.btneditarorden.Size = new System.Drawing.Size(77, 37);
		this.btneditarorden.TabIndex = 11;
		this.btneditarorden.Text = "Editar Orden";
		this.btneditarorden.UseVisualStyleBackColor = true;
		this.btneditarorden.Click += new System.EventHandler(btneditarorden_Click);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.dtpFecha2);
		this.groupBox2.Controls.Add(this.dtpFecha1);
		this.groupBox2.Controls.Add(this.rbFechaRegistro);
		this.groupBox2.Controls.Add(this.rbFecha);
		this.groupBox2.Controls.Add(this.txtNombreProducto);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.btnBuscarProducto);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtCodprod);
		this.groupBox2.Location = new System.Drawing.Point(3, 3);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1038, 54);
		this.groupBox2.TabIndex = 12;
		this.groupBox2.TabStop = false;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(218, 7);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(35, 12);
		this.label4.TabIndex = 52;
		this.label4.Text = "Hasta";
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(129, 7);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(37, 12);
		this.label5.TabIndex = 51;
		this.label5.Text = "Desde";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(218, 21);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha2.TabIndex = 50;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(131, 20);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha1.TabIndex = 49;
		this.rbFechaRegistro.AutoSize = true;
		this.rbFechaRegistro.Location = new System.Drawing.Point(9, 24);
		this.rbFechaRegistro.Name = "rbFechaRegistro";
		this.rbFechaRegistro.Size = new System.Drawing.Size(97, 17);
		this.rbFechaRegistro.TabIndex = 54;
		this.rbFechaRegistro.Text = "Fecha Registro";
		this.rbFechaRegistro.UseVisualStyleBackColor = true;
		this.rbFecha.AutoSize = true;
		this.rbFecha.Checked = true;
		this.rbFecha.Location = new System.Drawing.Point(9, 9);
		this.rbFecha.Name = "rbFecha";
		this.rbFecha.Size = new System.Drawing.Size(73, 17);
		this.rbFecha.TabIndex = 53;
		this.rbFecha.TabStop = true;
		this.rbFecha.Text = "Fecha OC";
		this.rbFecha.UseVisualStyleBackColor = true;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(530, 21);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 48;
		this.txtNombreProducto.Text = "---";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(489, 21);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(35, 13);
		this.label3.TabIndex = 47;
		this.label3.Text = "Prod.:";
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(449, 8);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 46;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(302, 5);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(112, 13);
		this.label2.TabIndex = 45;
		this.label2.Text = "Busqueda x Producto:";
		this.txtCodprod.Location = new System.Drawing.Point(305, 21);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 44;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizar.ImageIndex = 10;
		this.btnActualizar.ImageList = this.imageList2;
		this.btnActualizar.Location = new System.Drawing.Point(1047, 12);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(87, 37);
		this.btnActualizar.TabIndex = 5;
		this.btnActualizar.Text = "Buscar";
		this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnActualizar.UseVisualStyleBackColor = true;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(197, 371);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular Orden";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(1119, 372);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 37);
		this.btGenVenta.TabIndex = 3;
		this.btGenVenta.Text = "Generar Compra";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Visible = false;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnIrOrdenCompra.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnIrOrdenCompra.ImageIndex = 1;
		this.btnIrOrdenCompra.ImageList = this.imageList1;
		this.btnIrOrdenCompra.Location = new System.Drawing.Point(11, 372);
		this.btnIrOrdenCompra.Name = "btnIrOrdenCompra";
		this.btnIrOrdenCompra.Size = new System.Drawing.Size(97, 37);
		this.btnIrOrdenCompra.TabIndex = 2;
		this.btnIrOrdenCompra.Text = "Ir Orden";
		this.btnIrOrdenCompra.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrOrdenCompra.UseVisualStyleBackColor = true;
		this.btnIrOrdenCompra.Click += new System.EventHandler(btnIrOrdenCompra_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1140, 12);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1227, 420);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.btneditarorden);
		base.Controls.Add(this.btnListarGR);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.cmbestadoOrden);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnActualizar);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btGenVenta);
		base.Controls.Add(this.btnIrOrdenCompra);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmOrdenesVigentes";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ordenes Vigentes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmOrdenesVigentes_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvOrdenes.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvOrdenes).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
