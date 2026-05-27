using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmClientesCompletos : Office2007Form
{
	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int Tipo;

	private clsAdmCategoriaCliente admctgcliente = new clsAdmCategoriaCliente();

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem btnNuevo;

	private ButtonItem btnModificar;

	private ButtonItem btnEliminar;

	private ButtonItem btnCopiar;

	private ButtonItem btnActualizar;

	private ButtonItem btnBuscar;

	private ButtonItem btnImprimir;

	private DataGridView dgvClientes;

	private ExpandablePanel expandablePanel1;

	private Label label4;

	private Label label1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Button btnSalir;

	private ButtonItem buttonItem1;

	private ButtonItem buttonItem2;

	private ButtonItem biFiltros;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codperso;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn razonsocial;

	private DataGridViewTextBoxColumn direccionlegal;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewAutoFilterTextBoxColumn zona;

	private DataGridViewAutoFilterTextBoxColumn vendedor;

	private DataGridViewTextBoxColumn listaprecio;

	private DataGridViewTextBoxColumn habilitado;

	private DataGridViewAutoFilterTextBoxColumn lineacredito;

	private DataGridViewTextBoxColumn fregistro;

	private DataGridViewTextBoxColumn categoria;

	private DataGridViewCheckBoxColumn seleccionar;

	private ButtonItem btnactualizarctg;

	private ComboBox cmbctg;

	public frmClientesCompletos()
	{
		InitializeComponent();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		frmGestionCliente frm = new frmGestionCliente();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaLista();
	}

	public void CargaLista()
	{
		dgvClientes.DataSource = data;
		data.DataSource = AdmCli.MuestraClientes();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvClientes.ClearSelection();
		DarFormato();
	}

	private void dgvdetalleEditable(bool editable)
	{
		foreach (DataGridViewColumn col in dgvClientes.Columns)
		{
			col.ReadOnly = true;
			if (editable)
			{
				if (col.Name == seleccionar.Name)
				{
					col.ReadOnly = false;
				}
				else
				{
					col.ReadOnly = true;
				}
			}
		}
	}

	private void DarFormato()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvClientes.Rows)
		{
			if (row.Cells[habilitado.Name].Value.ToString() == "INHABILITADO" && row.Index != -1)
			{
				row.Cells[habilitado.Name].Style.BackColor = Color.Red;
				row.Cells[habilitado.Name].Style.ForeColor = Color.White;
			}
		}
	}

	private void dgvClientes_Sorted(object sender, EventArgs e)
	{
		DarFormato();
	}

	private void frmClientes_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Razon Social";
		label3.Text = "razonsocial";
		dgvdetalleEditable(editable: true);
		cargaCombocategoriaclientes();
	}

	public void cargaCombocategoriaclientes()
	{
		DataTable aux = admctgcliente.MuestraCategoriasCliente();
		cmbctg.DataSource = aux;
		cmbctg.DisplayMember = "descripcion";
		cmbctg.ValueMember = "codCategoriaCliente";
		cmbctg.SelectedIndex = -1;
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnModificar_Click(object sender, EventArgs e)
	{
		if (dgvClientes.SelectedRows.Count > 0)
		{
			frmGestionCliente frm = new frmGestionCliente();
			frm.Proceso = 2;
			frm.cli = cli;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void btnBuscar_Click(object sender, EventArgs e)
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

	private void frmClientesCompletos_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B && e.Control)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvClientes.CurrentRow.Index != -1 && cli.CodCliente != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Clientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmCli.delete(Convert.ToInt32(cli.CodCliente)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void dgvClientes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvClientes.SelectedRows.Count > 0)
		{
			frmGestionCliente frm = new frmGestionCliente();
			frm.Proceso = 3;
			frm.cli = cli;
			frm.ShowDialog();
		}
	}

	private void dgvClientes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvClientes.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvClientes.Columns[e.ColumnIndex].DataPropertyName;
		if (expandablePanel1.Expanded)
		{
			txtFiltro.Focus();
		}
	}

	private void dgvClientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvClientes.Rows.Count >= 1 && e.Row.Selected)
		{
			cli.CodCliente = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
		}
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label3.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnCopiar_Click(object sender, EventArgs e)
	{
		if (dgvClientes.SelectedRows.Count > 0)
		{
			frmGestionCliente frm = new frmGestionCliente();
			frm.Proceso = 3;
			frm.cli = cli;
			frm.ShowDialog();
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("Clientes");
		foreach (DataGridViewColumn column in dgvClientes.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvClientes.Rows.Count; i++)
		{
			DataGridViewRow row = dgvClientes.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvClientes.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\ClientesRPT.xml", XmlWriteMode.WriteSchema);
		CRClientesCompletos rpt = new CRClientesCompletos();
		frmClientesRP frm = new frmClientesRP();
		rpt.SetDataSource(ds);
		frm.cRVClientesRP.ReportSource = rpt;
		frm.Show();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void buttonItem1_Click(object sender, EventArgs e)
	{
		if (dgvClientes.CurrentRow.Index != -1 && cli.CodCliente != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Inhabilitar al cliente seleccionado", "Clientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmCli.CambioHabilitado(Convert.ToInt32(cli.CodCliente), Estado: false))
			{
				MessageBox.Show("El cliente se ha Inhabilitado", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		if (dgvClientes.CurrentRow.Index != -1 && cli.CodCliente != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Habilitar al cliente seleccionado", "Clientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmCli.CambioHabilitado(Convert.ToInt32(cli.CodCliente), Estado: true))
			{
				MessageBox.Show("El cliente ha sido Habilitado", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void frmClientesCompletos_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void biFiltros_Click(object sender, EventArgs e)
	{
		DataGridViewAutoFilterTextBoxColumn.RemoveFilter(dgvClientes);
	}

	private void btnactualizarctg_Click(object sender, EventArgs e)
	{
		try
		{
			if (cmbctg.SelectedIndex != -1)
			{
				int count = 0;
				foreach (DataGridViewRow row in (IEnumerable)dgvClientes.Rows)
				{
					if (Convert.ToInt32(row.Cells["seleccionar"].Value) == 1)
					{
						int CodCliente = Convert.ToInt32(row.Cells["codigo"].Value);
						AdmCli.updatecategoria(CodCliente, Convert.ToInt32(cmbctg.SelectedValue));
						count++;
					}
				}
				MessageBox.Show("Total Clientes Actualizados : " + count, "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				cmbctg.SelectedIndex = -1;
				CargaLista();
			}
			else
			{
				MessageBox.Show("Seleccione una categoria", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmClientesCompletos));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.btnNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.btnModificar = new DevComponents.DotNetBar.ButtonItem();
		this.btnEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.btnCopiar = new DevComponents.DotNetBar.ButtonItem();
		this.btnActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.btnBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.btnImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.biFiltros = new DevComponents.DotNetBar.ButtonItem();
		this.dgvClientes = new System.Windows.Forms.DataGridView();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codperso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccionlegal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.zona = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.vendedor = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.listaprecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.habilitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lineacredito = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fregistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.cmbctg = new System.Windows.Forms.ComboBox();
		this.btnactualizarctg = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvClientes).BeginInit();
		this.expandablePanel1.SuspendLayout();
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
		this.imageList1.Images.SetKeyName(16, "folder-open-icon.png");
		this.imageList1.Images.SetKeyName(17, "dagobert83-neg.png");
		this.imageList1.Images.SetKeyName(18, "pouce_bas.png");
		this.imageList1.Images.SetKeyName(19, "OK_png.png");
		this.imageList1.Images.SetKeyName(20, "clientes-satisfechos.png");
		this.imageList1.Images.SetKeyName(21, "13630_13798_128_filter_icon.png");
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Controls.Add(this.cmbctg);
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[11]
		{
			this.btnNuevo, this.btnModificar, this.btnEliminar, this.btnCopiar, this.btnActualizar, this.btnBuscar, this.btnImprimir, this.buttonItem1, this.buttonItem2, this.biFiltros,
			this.btnactualizarctg
		});
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(778, 55);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 5;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.btnNuevo.ImageIndex = 4;
		this.btnNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.SubItemsExpandWidth = 14;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnModificar.ImageIndex = 3;
		this.btnModificar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnModificar.Name = "btnModificar";
		this.btnModificar.SubItemsExpandWidth = 14;
		this.btnModificar.Text = "Modificar";
		this.btnModificar.Click += new System.EventHandler(btnModificar_Click);
		this.btnEliminar.ImageIndex = 5;
		this.btnEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.SubItemsExpandWidth = 14;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.Visible = false;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnCopiar.ImageIndex = 16;
		this.btnCopiar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnCopiar.Name = "btnCopiar";
		this.btnCopiar.SubItemsExpandWidth = 14;
		this.btnCopiar.Text = "Consultar";
		this.btnCopiar.Visible = false;
		this.btnCopiar.Click += new System.EventHandler(btnCopiar_Click);
		this.btnActualizar.ImageIndex = 8;
		this.btnActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.SubItemsExpandWidth = 14;
		this.btnActualizar.Text = "Actualizar";
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.btnBuscar.ImageIndex = 11;
		this.btnBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.SubItemsExpandWidth = 14;
		this.btnBuscar.Text = "Buscar";
		this.btnBuscar.Click += new System.EventHandler(btnBuscar_Click);
		this.btnImprimir.ImageIndex = 7;
		this.btnImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.SubItemsExpandWidth = 14;
		this.btnImprimir.Text = "Imprimir";
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.buttonItem1.ImageIndex = 17;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Inhabilitar";
		this.buttonItem1.Click += new System.EventHandler(buttonItem1_Click);
		this.buttonItem2.ImageIndex = 19;
		this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Text = "Habilitar";
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click);
		this.biFiltros.ImageIndex = 21;
		this.biFiltros.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biFiltros.Name = "biFiltros";
		this.biFiltros.SubItemsExpandWidth = 14;
		this.biFiltros.Text = "Filtro";
		this.biFiltros.Click += new System.EventHandler(biFiltros_Click);
		this.dgvClientes.AllowUserToAddRows = false;
		this.dgvClientes.AllowUserToDeleteRows = false;
		this.dgvClientes.AllowUserToResizeColumns = false;
		this.dgvClientes.AllowUserToResizeRows = false;
		this.dgvClientes.Columns.AddRange(this.codigo, this.codperso, this.dni, this.ruc, this.razonsocial, this.direccionlegal, this.telefono, this.zona, this.vendedor, this.listaprecio, this.habilitado, this.lineacredito, this.fregistro, this.categoria, this.seleccionar);
		this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvClientes.Location = new System.Drawing.Point(0, 55);
		this.dgvClientes.Name = "dgvClientes";
		this.dgvClientes.RowHeadersVisible = false;
		this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvClientes.Size = new System.Drawing.Size(778, 419);
		this.dgvClientes.TabIndex = 16;
		this.dgvClientes.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvClientes_CellMouseDoubleClick);
		this.dgvClientes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvClientes_ColumnHeaderMouseClick);
		this.dgvClientes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvClientes_RowStateChanged);
		this.dgvClientes.Sorted += new System.EventHandler(dgvClientes_Sorted);
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.label4);
		this.expandablePanel1.Controls.Add(this.label1);
		this.expandablePanel1.Controls.Add(this.label3);
		this.expandablePanel1.Controls.Add(this.label2);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.btnSalir);
		this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.expandablePanel1.ExpandButtonVisible = false;
		this.expandablePanel1.Expanded = false;
		this.expandablePanel1.ExpandedBounds = new System.Drawing.Rectangle(547, 0, 231, 93);
		this.expandablePanel1.Location = new System.Drawing.Point(547, 0);
		this.expandablePanel1.Name = "expandablePanel1";
		this.expandablePanel1.ShowFocusRectangle = true;
		this.expandablePanel1.Size = new System.Drawing.Size(231, 0);
		this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.Style.BackColor1.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.BackColor2.Color = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
		this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarPopupBorder;
		this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
		this.expandablePanel1.Style.GradientAngle = 90;
		this.expandablePanel1.TabIndex = 17;
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
		this.label4.Location = new System.Drawing.Point(10, -59);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 13);
		this.label4.TabIndex = 10;
		this.label4.Text = "Por :";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Lucida Sans", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(5, -89);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(62, 12);
		this.label1.TabIndex = 9;
		this.label1.Text = "Busqueda";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.ForeColor = System.Drawing.Color.LightBlue;
		this.label3.Location = new System.Drawing.Point(186, -59);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(45, -59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(15, 13);
		this.label2.TabIndex = 6;
		this.label2.Text = "X";
		this.txtFiltro.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtFiltro.Location = new System.Drawing.Point(13, -38);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 5;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.BackColor = System.Drawing.Color.Transparent;
		this.btnSalir.FlatAppearance.BorderSize = 0;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Location = new System.Drawing.Point(213, -93);
		this.btnSalir.Margin = new System.Windows.Forms.Padding(1);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(18, 22);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "x";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnSalir.UseVisualStyleBackColor = false;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.codigo.DataPropertyName = "codcliente";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.codperso.DataPropertyName = "codigopersonalizado";
		this.codperso.HeaderText = "Codigo P.";
		this.codperso.Name = "codperso";
		this.codperso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codperso.Width = 70;
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dni.Width = 80;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ruc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Razon Social";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.razonsocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.Width = 280;
		this.direccionlegal.DataPropertyName = "direccionlegal";
		this.direccionlegal.HeaderText = "Dirección Legal";
		this.direccionlegal.Name = "direccionlegal";
		this.direccionlegal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.direccionlegal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccionlegal.Width = 240;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "Teléfono";
		this.telefono.Name = "telefono";
		this.telefono.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.telefono.Width = 90;
		this.zona.AutomaticSortingEnabled = false;
		this.zona.DataPropertyName = "zona";
		this.zona.HeaderText = "Zona";
		this.zona.Name = "zona";
		this.zona.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.zona.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.zona.Width = 90;
		this.vendedor.AutomaticSortingEnabled = false;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.vendedor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.vendedor.Width = 80;
		this.listaprecio.DataPropertyName = "lista";
		this.listaprecio.HeaderText = "Lista Precio";
		this.listaprecio.Name = "listaprecio";
		this.listaprecio.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.listaprecio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.listaprecio.Visible = false;
		this.habilitado.DataPropertyName = "habi";
		this.habilitado.HeaderText = "Habilitado";
		this.habilitado.Name = "habilitado";
		this.lineacredito.DataPropertyName = "lineacredito";
		this.lineacredito.HeaderText = "Linea Credito";
		this.lineacredito.Name = "lineacredito";
		this.lineacredito.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.fregistro.DataPropertyName = "fregistro";
		this.fregistro.HeaderText = "Fecha Registro";
		this.fregistro.Name = "fregistro";
		this.categoria.DataPropertyName = "categoria";
		this.categoria.HeaderText = "Categoria";
		this.categoria.Name = "categoria";
		this.seleccionar.HeaderText = "seleccionar";
		this.seleccionar.Name = "seleccionar";
		this.cmbctg.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbctg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbctg.FormattingEnabled = true;
		this.cmbctg.Location = new System.Drawing.Point(654, 28);
		this.cmbctg.Name = "cmbctg";
		this.cmbctg.Size = new System.Drawing.Size(121, 21);
		this.cmbctg.TabIndex = 0;
		this.btnactualizarctg.Image = SIGEFA.Properties.Resources.acep;
		this.btnactualizarctg.ImagePaddingHorizontal = 15;
		this.btnactualizarctg.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnactualizarctg.Name = "btnactualizarctg";
		this.btnactualizarctg.SubItemsExpandWidth = 14;
		this.btnactualizarctg.Text = "Actualizar Categoria";
		this.btnactualizarctg.Click += new System.EventHandler(btnactualizarctg_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(778, 474);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvClientes);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmClientesCompletos";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Clientes Completos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmClientes_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmClientesCompletos_KeyDown);
		this.ribbonBar2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvClientes).EndInit();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
