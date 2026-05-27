using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmAlmacenes : Office2007Form
{
	private clsAdmAlmacen AdmAlm = new clsAdmAlmacen();

	private clsAlmacen alm = new clsAlmacen();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private RibbonBar ribbonBar2;

	private ButtonItem biNuevo;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem biConsultar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private DataGridView dgvAlmacenes;

	private ExpandablePanel expandablePanel1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Button btnSalir;

	private Label label4;

	private Label label1;

	private ImageList imageList2;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn principal;

	private DataGridViewTextBoxColumn ubicacion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn fechareg;

	public frmAlmacenes()
	{
		InitializeComponent();
	}

	private void frmAlmacenes_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Nombre";
		label3.Text = "nombre";
	}

	private void CargaLista()
	{
		dgvAlmacenes.DataSource = data;
		data.DataSource = AdmAlm.MuestraAlmacenes(frmLogin.iCodEmpresa);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvAlmacenes.ClearSelection();
	}

	private void buttonItem16_Click(object sender, EventArgs e)
	{
		frmGestionAlmacen frm = new frmGestionAlmacen();
		frm.Proceso = 1;
		frm.ShowDialog();
	}

	private void buttonItem8_Click(object sender, EventArgs e)
	{
	}

	private void biBuscar_Click(object sender, EventArgs e)
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

	private void biActualizar_Click(object sender, EventArgs e)
	{
		CargaLista();
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

	private void frmAlmacenes_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B && e.Control)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
	}

	private void biEditar_Click(object sender, EventArgs e)
	{
		if (dgvAlmacenes.SelectedRows.Count > 0)
		{
			frmGestionAlmacen frm = new frmGestionAlmacen();
			frm.Proceso = 2;
			frm.alm = alm;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void frmAlmacenes_Shown(object sender, EventArgs e)
	{
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
		if (dgvAlmacenes.CurrentRow.Index != -1 && alm.CodAlmacen != 0 && dgvAlmacenes.Rows.Count > 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Almacenes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmAlm.delete(Convert.ToInt32(alm.CodAlmacen)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Almacenes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("No se Puede Eliminar el Registro ");
		}
	}

	private void dgvAlmacenes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvAlmacenes.SelectedRows.Count > 0)
		{
			frmGestionAlmacen frm = new frmGestionAlmacen();
			frm.Proceso = 3;
			frm.alm = alm;
			frm.ShowDialog();
		}
	}

	private void dgvAlmacenes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvAlmacenes.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvAlmacenes.Columns[e.ColumnIndex].DataPropertyName;
		if (expandablePanel1.Expanded)
		{
			txtFiltro.Focus();
		}
	}

	private void dgvAlmacenes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvAlmacenes.Rows.Count >= 1 && e.Row.Selected)
		{
			alm.CodAlmacen = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
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

	private void biCopiar_Click(object sender, EventArgs e)
	{
		if (dgvAlmacenes.SelectedRows.Count > 0)
		{
			frmGestionAlmacen frm = new frmGestionAlmacen();
			frm.Proceso = 3;
			frm.alm = alm;
			frm.ShowDialog();
		}
	}

	private void biImprimir_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("Almacenes");
		foreach (DataGridViewColumn column in dgvAlmacenes.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvAlmacenes.Rows.Count; i++)
		{
			DataGridViewRow row = dgvAlmacenes.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvAlmacenes.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmAlmacenesRP frm = new frmAlmacenesRP();
		frm.DTable = dt;
		frm.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmAlmacenes));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.biNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biConsultar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.dgvAlmacenes = new System.Windows.Forms.DataGridView();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.principal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ubicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechareg = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).BeginInit();
		this.expandablePanel1.SuspendLayout();
		base.SuspendLayout();
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.Images = this.imageList2;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[7] { this.biNuevo, this.biEditar, this.biEliminar, this.biConsultar, this.biActualizar, this.biBuscar, this.biImprimir });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(778, 60);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 5;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
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
		this.imageList2.Images.SetKeyName(16, "Folder open.png");
		this.biNuevo.ImageIndex = 4;
		this.biNuevo.ImagePaddingVertical = 12;
		this.biNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNuevo.Name = "biNuevo";
		this.biNuevo.SubItemsExpandWidth = 14;
		this.biNuevo.Text = "Nuevo";
		this.biNuevo.Click += new System.EventHandler(buttonItem16_Click);
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePaddingVertical = 12;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Modificar";
		this.biEditar.Click += new System.EventHandler(biEditar_Click);
		this.biEliminar.ImageIndex = 5;
		this.biEliminar.ImagePaddingVertical = 12;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.biConsultar.ImageIndex = 16;
		this.biConsultar.ImagePaddingVertical = 12;
		this.biConsultar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biConsultar.Name = "biConsultar";
		this.biConsultar.SubItemsExpandWidth = 14;
		this.biConsultar.Text = "Consultar";
		this.biConsultar.Click += new System.EventHandler(biCopiar_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingVertical = 12;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(biActualizar_Click);
		this.biBuscar.ImageIndex = 11;
		this.biBuscar.ImagePaddingVertical = 12;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biBuscar.Click += new System.EventHandler(biBuscar_Click);
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingVertical = 12;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Click += new System.EventHandler(biImprimir_Click);
		this.dgvAlmacenes.AllowUserToAddRows = false;
		this.dgvAlmacenes.AllowUserToDeleteRows = false;
		this.dgvAlmacenes.AllowUserToResizeColumns = false;
		this.dgvAlmacenes.AllowUserToResizeRows = false;
		this.dgvAlmacenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvAlmacenes.Columns.AddRange(this.codigo, this.nombre, this.principal, this.ubicacion, this.telefono, this.descripcion, this.estado, this.fechareg);
		this.dgvAlmacenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvAlmacenes.Location = new System.Drawing.Point(0, 60);
		this.dgvAlmacenes.Name = "dgvAlmacenes";
		this.dgvAlmacenes.ReadOnly = true;
		this.dgvAlmacenes.RowHeadersVisible = false;
		this.dgvAlmacenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvAlmacenes.Size = new System.Drawing.Size(778, 414);
		this.dgvAlmacenes.TabIndex = 6;
		this.dgvAlmacenes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvAlmacenes_ColumnHeaderMouseClick);
		this.dgvAlmacenes.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvAlmacenes_CellMouseDoubleClick);
		this.dgvAlmacenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvAlmacenes_RowStateChanged);
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
		this.codigo.DataPropertyName = "codAlmacen";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 80;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 300;
		this.principal.DataPropertyName = "principal";
		this.principal.HeaderText = "Principal";
		this.principal.Name = "principal";
		this.principal.ReadOnly = true;
		this.principal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ubicacion.DataPropertyName = "ubicacion";
		this.ubicacion.HeaderText = "Ubicacion";
		this.ubicacion.Name = "ubicacion";
		this.ubicacion.ReadOnly = true;
		this.ubicacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.ubicacion.Width = 300;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "Telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 300;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.Visible = false;
		this.estado.Width = 80;
		this.fechareg.DataPropertyName = "fecharegistro";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "d";
		dataGridViewCellStyle1.NullValue = null;
		this.fechareg.DefaultCellStyle = dataGridViewCellStyle1;
		this.fechareg.HeaderText = "Fecha Reg.";
		this.fechareg.Name = "fechareg";
		this.fechareg.ReadOnly = true;
		this.fechareg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(778, 474);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvAlmacenes);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmAlmacenes";
		this.Text = "Almacenes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmAlmacenes_Load);
		base.Shown += new System.EventHandler(frmAlmacenes_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmAlmacenes_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.dgvAlmacenes).EndInit();
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
