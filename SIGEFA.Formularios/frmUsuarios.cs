using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmUsuarios : Office2007Form
{
	private clsAdmUsuario AdmUsu = new clsAdmUsuario();

	private clsUsuario usu = new clsUsuario();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private ButtonItem buttonItem1;

	private ButtonItem buttonItem2;

	private RibbonBar ribbonBar2;

	private ImageList imageList1;

	private ButtonItem biNuevo;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem buttonItem7;

	private ButtonItem biConsultar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private DataGridView dgvUsuarios;

	private ButtonItem biAccesos;

	private ExpandablePanel expandablePanel1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private Button btnSalir;

	private Label label4;

	private ButtonItem biAccesosSucursal;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn apellido;

	private DataGridViewTextBoxColumn fechanac;

	private DataGridViewTextBoxColumn direccion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn celular;

	private DataGridViewTextBoxColumn email;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn contra;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fechareg;

	private DataGridViewTextBoxColumn Nivel;

	private DataGridViewTextBoxColumn colCanalVenta;

	public frmUsuarios()
	{
		InitializeComponent();
	}

	private void buttonItem16_Click(object sender, EventArgs e)
	{
		frmGestionUsuario frm = new frmGestionUsuario();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaLista();
	}

	private void frmUsuarios_Load(object sender, EventArgs e)
	{
		try
		{
			CargaLista();
			label2.Text = "Nombres";
			label3.Text = "nombre";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void buttonItem4_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvUsuarios.DataSource = data;
		data.DataSource = AdmUsu.ListaUsuarios_Empresa(frmLogin.iCodEmpresa);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvUsuarios.ClearSelection();
	}

	private void buttonItem6_Click(object sender, EventArgs e)
	{
		if (dgvUsuarios.SelectedRows.Count > 0)
		{
			frmGestionUsuario frm = new frmGestionUsuario();
			frm.Proceso = 2;
			frm.usu = usu;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void frmUsuarios_Shown(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void buttonItem8_Click(object sender, EventArgs e)
	{
		if (dgvUsuarios.CurrentRow.Index != -1 && usu.CodUsuario != 0 && dgvUsuarios.Rows.Count > 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos del Usuario definitivamente", "Usuarios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmUsu.delete(Convert.ToInt32(usu.CodUsuario)))
			{
				MessageBox.Show("El Usuario ha sido eliminado", "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("No se Puede Eliminar el Registro ");
		}
	}

	private void buttonItem5_Click(object sender, EventArgs e)
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

	private void showbuscar()
	{
		frmBusqueda search = new frmBusqueda();
		search.Owner = this;
		search.label1.Text = dgvUsuarios.Columns[dgvUsuarios.CurrentCell.ColumnIndex].HeaderText;
		search.label2.Text = dgvUsuarios.Columns[dgvUsuarios.CurrentCell.ColumnIndex].DataPropertyName;
		search.Top = 50;
		search.Left = Application.OpenForms["mdi_Menu"].Width - search.Width - 20;
		search.Show();
	}

	private void dgvUsuarios_RowStateChanged_1(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvUsuarios.Rows.Count >= 1 && e.Row.Selected)
		{
			usu.CodUsuario = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			if (Convert.ToInt32(e.Row.Cells[Nivel.Name].Value) != 1)
			{
				biAccesosSucursal.Enabled = true;
			}
			else
			{
				biAccesosSucursal.Enabled = false;
			}
		}
	}

	private void buttonItem10_Click(object sender, EventArgs e)
	{
		if (dgvUsuarios.SelectedRows.Count > 0)
		{
			frmAccesos frm = new frmAccesos();
			frm.usu = usu;
			frm.ShowDialog();
		}
	}

	private void dgvUsuarios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvUsuarios.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvUsuarios.Columns[e.ColumnIndex].DataPropertyName;
		if (expandablePanel1.Expanded)
		{
			txtFiltro.Focus();
		}
		if (Application.OpenForms["frmBusqueda"] != null)
		{
			frmBusqueda search = (frmBusqueda)Application.OpenForms["frmBusqueda"];
			search.label1.Text = dgvUsuarios.Columns[e.ColumnIndex].HeaderText;
			search.label2.Text = dgvUsuarios.Columns[e.ColumnIndex].DataPropertyName;
			search.txtFiltro.Focus();
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

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
	}

	private void frmUsuarios_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B && e.Control)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
	}

	private void dgvUsuarios_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvUsuarios.SelectedRows.Count > 0)
		{
			frmGestionUsuario frm = new frmGestionUsuario();
			frm.Proceso = 3;
			frm.usu = usu;
			frm.ShowDialog();
		}
	}

	private void biConsultar_Click(object sender, EventArgs e)
	{
		if (dgvUsuarios.SelectedRows.Count > 0)
		{
			frmGestionUsuario frm = new frmGestionUsuario();
			frm.Proceso = 3;
			frm.usu = usu;
			frm.ShowDialog();
		}
	}

	private void biImprimir_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("Usuarios");
		foreach (DataGridViewColumn column in dgvUsuarios.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvUsuarios.Rows.Count; i++)
		{
			DataGridViewRow row = dgvUsuarios.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvUsuarios.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmUsuariosRP frm = new frmUsuariosRP();
		frm.DTable = dt;
		frm.Show();
	}

	private void buttonItem3_Click(object sender, EventArgs e)
	{
		if (dgvUsuarios.SelectedRows.Count > 0)
		{
			frmAccesosSucursal frm = new frmAccesosSucursal();
			frm.usu = usu;
			frm.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmUsuarios));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.biNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biAccesos = new DevComponents.DotNetBar.ButtonItem();
		this.biAccesosSucursal = new DevComponents.DotNetBar.ButtonItem();
		this.biConsultar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
		this.dgvUsuarios = new System.Windows.Forms.DataGridView();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechanac = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.celular = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.email = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechareg = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Nivel = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCanalVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvUsuarios).BeginInit();
		this.expandablePanel1.SuspendLayout();
		base.SuspendLayout();
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "buttonItem1";
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Text = "buttonItem2";
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[9] { this.biNuevo, this.biEditar, this.biEliminar, this.biAccesos, this.biAccesosSucursal, this.biConsultar, this.biActualizar, this.biBuscar, this.biImprimir });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(778, 70);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 2;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
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
		this.imageList1.Images.SetKeyName(9, "search (1).png");
		this.imageList1.Images.SetKeyName(10, "89868_65787_exit_64x64.png");
		this.imageList1.Images.SetKeyName(11, "folder_open (1).png");
		this.imageList1.Images.SetKeyName(12, "folder-open-icon (1).png");
		this.imageList1.Images.SetKeyName(13, "superSecure.png");
		this.biNuevo.ImageIndex = 4;
		this.biNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNuevo.Name = "biNuevo";
		this.biNuevo.SubItemsExpandWidth = 14;
		this.biNuevo.Text = "Nuevo";
		this.biNuevo.Click += new System.EventHandler(buttonItem16_Click);
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Modificar";
		this.biEditar.Click += new System.EventHandler(buttonItem6_Click);
		this.biEliminar.ImageIndex = 5;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Click += new System.EventHandler(buttonItem8_Click);
		this.biAccesos.ImageIndex = 10;
		this.biAccesos.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAccesos.Name = "biAccesos";
		this.biAccesos.SubItemsExpandWidth = 14;
		this.biAccesos.Text = "Accesos";
		this.biAccesos.Click += new System.EventHandler(buttonItem10_Click);
		this.biAccesosSucursal.Enabled = false;
		this.biAccesosSucursal.ImageIndex = 13;
		this.biAccesosSucursal.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biAccesosSucursal.Name = "biAccesosSucursal";
		this.biAccesosSucursal.SubItemsExpandWidth = 14;
		this.biAccesosSucursal.Text = "Accesos Sucursal";
		this.biAccesosSucursal.Click += new System.EventHandler(buttonItem3_Click);
		this.biConsultar.ImageIndex = 12;
		this.biConsultar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biConsultar.Name = "biConsultar";
		this.biConsultar.SubItemsExpandWidth = 14;
		this.biConsultar.Text = "Consultar";
		this.biConsultar.Click += new System.EventHandler(biConsultar_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(buttonItem4_Click);
		this.biBuscar.ImageIndex = 9;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biBuscar.Click += new System.EventHandler(buttonItem5_Click);
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Click += new System.EventHandler(biImprimir_Click);
		this.buttonItem7.ImageIndex = 6;
		this.buttonItem7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem7.Name = "buttonItem7";
		this.buttonItem7.SubItemsExpandWidth = 14;
		this.buttonItem7.Text = "Nota de Ingreso";
		this.dgvUsuarios.AllowUserToAddRows = false;
		this.dgvUsuarios.AllowUserToDeleteRows = false;
		this.dgvUsuarios.AllowUserToResizeColumns = false;
		this.dgvUsuarios.AllowUserToResizeRows = false;
		this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvUsuarios.Columns.AddRange(this.codigo, this.dni, this.nombre, this.apellido, this.fechanac, this.direccion, this.telefono, this.celular, this.email, this.usuario, this.contra, this.estado, this.coduser, this.fechareg, this.Nivel, this.colCanalVenta);
		this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvUsuarios.Location = new System.Drawing.Point(0, 70);
		this.dgvUsuarios.MultiSelect = false;
		this.dgvUsuarios.Name = "dgvUsuarios";
		this.dgvUsuarios.ReadOnly = true;
		this.dgvUsuarios.RowHeadersVisible = false;
		this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvUsuarios.Size = new System.Drawing.Size(778, 404);
		this.dgvUsuarios.TabIndex = 4;
		this.dgvUsuarios.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvUsuarios_CellMouseDoubleClick);
		this.dgvUsuarios.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvUsuarios_ColumnHeaderMouseClick);
		this.dgvUsuarios.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvUsuarios_RowStateChanged_1);
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.Color.Transparent;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.label4);
		this.expandablePanel1.Controls.Add(this.label3);
		this.expandablePanel1.Controls.Add(this.label2);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.label1);
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
		this.expandablePanel1.TabIndex = 7;
		this.expandablePanel1.TitleHeight = 0;
		this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
		this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
		this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.expandablePanel1.TitleStyle.GradientAngle = 90;
		this.expandablePanel1.TitleText = "Title Bar";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(10, -59);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(29, 13);
		this.label4.TabIndex = 8;
		this.label4.Text = "Por :";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
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
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Lucida Sans", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(5, -89);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(58, 12);
		this.label1.TabIndex = 4;
		this.label1.Text = "Busqueda";
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
		this.codigo.DataPropertyName = "codUsuario";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.dni.DataPropertyName = "dni";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.dni.DefaultCellStyle = dataGridViewCellStyle1;
		this.dni.HeaderText = "Dni";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.dni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dni.Width = 80;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombres";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 200;
		this.apellido.DataPropertyName = "apellido";
		this.apellido.HeaderText = "Apellidos";
		this.apellido.Name = "apellido";
		this.apellido.ReadOnly = true;
		this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.apellido.Width = 200;
		this.fechanac.DataPropertyName = "fechanac";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "d";
		dataGridViewCellStyle2.NullValue = null;
		this.fechanac.DefaultCellStyle = dataGridViewCellStyle2;
		this.fechanac.HeaderText = "Fecha Nac.";
		this.fechanac.Name = "fechanac";
		this.fechanac.ReadOnly = true;
		this.fechanac.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechanac.Width = 80;
		this.direccion.DataPropertyName = "direccion";
		this.direccion.HeaderText = "Direccion";
		this.direccion.Name = "direccion";
		this.direccion.ReadOnly = true;
		this.direccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccion.Width = 200;
		this.telefono.DataPropertyName = "telefono";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.telefono.DefaultCellStyle = dataGridViewCellStyle3;
		this.telefono.HeaderText = "Telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.telefono.Width = 80;
		this.celular.DataPropertyName = "celular";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.celular.DefaultCellStyle = dataGridViewCellStyle4;
		this.celular.HeaderText = "Celular";
		this.celular.Name = "celular";
		this.celular.ReadOnly = true;
		this.celular.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.celular.Width = 80;
		this.email.DataPropertyName = "email";
		this.email.HeaderText = "Email";
		this.email.Name = "email";
		this.email.ReadOnly = true;
		this.email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Username";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.usuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.contra.DataPropertyName = "contrasena";
		this.contra.HeaderText = "Contaseña";
		this.contra.Name = "contra";
		this.contra.ReadOnly = true;
		this.contra.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.contra.Visible = false;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.Width = 80;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coduser.Visible = false;
		this.fechareg.DataPropertyName = "fecharegistro";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "d";
		dataGridViewCellStyle5.NullValue = null;
		this.fechareg.DefaultCellStyle = dataGridViewCellStyle5;
		this.fechareg.HeaderText = "Fecha Reg.";
		this.fechareg.Name = "fechareg";
		this.fechareg.ReadOnly = true;
		this.fechareg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fechareg.Width = 80;
		this.Nivel.DataPropertyName = "nivel";
		this.Nivel.HeaderText = "nivel";
		this.Nivel.Name = "Nivel";
		this.Nivel.ReadOnly = true;
		this.colCanalVenta.DataPropertyName = "canalventa";
		this.colCanalVenta.HeaderText = "Canal Venta";
		this.colCanalVenta.Name = "colCanalVenta";
		this.colCanalVenta.ReadOnly = true;
		this.colCanalVenta.Width = 120;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(778, 474);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvUsuarios);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmUsuarios";
		this.Text = "Usuarios";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmUsuarios_Load);
		base.Shown += new System.EventHandler(frmUsuarios_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmUsuarios_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.dgvUsuarios).EndInit();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
