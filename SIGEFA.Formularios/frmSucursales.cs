using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmSucursales : Office2007Form
{
	private clsAdmSucursal AdmSuc = new clsAdmSucursal();

	private clsSucursal suc = new clsSucursal();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem biNuevo;

	private ButtonItem biModificar;

	private ButtonItem biEliminar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimir;

	private DataGridView dgvEmpresas;

	private ExpandablePanel expandablePanel1;

	private TextBox txtFiltro;

	private Label label1;

	private Button btnSalir;

	private Label label2;

	private Label label3;

	private Label label4;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codEmpresa;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn ubicacion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fechareg;

	public frmSucursales()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvEmpresas.DataSource = data;
		data.DataSource = AdmSuc.ListaSucursales_Empresa(frmLogin.iCodEmpresa);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvEmpresas.ClearSelection();
	}

	private void frmEmpresas_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "nombre";
		label3.Text = "nombre";
	}

	private void dgvEmpresas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvEmpresas.Rows.Count >= 1 && e.Row.Selected)
		{
			suc.CodSucursal = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
		}
	}

	private void frmEmpresas_Shown(object sender, EventArgs e)
	{
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		expandablePanel1.Expanded = false;
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

	private void dgvEmpresas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvEmpresas.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvEmpresas.Columns[e.ColumnIndex].DataPropertyName;
		if (expandablePanel1.Expanded)
		{
			txtFiltro.Focus();
		}
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			expandablePanel1.Expanded = false;
		}
	}

	private void frmEmpresas_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B && e.Control)
		{
			expandablePanel1.Expanded = true;
			txtFiltro.Focus();
		}
	}

	private void dgvEmpresas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (dgvEmpresas.SelectedRows.Count > 0)
		{
			frmGestionSucursal frm = new frmGestionSucursal();
			frm.Proceso = 3;
			frm.suc = suc;
			frm.ShowDialog();
		}
	}

	private void biNuevo_Click(object sender, EventArgs e)
	{
		frmGestionSucursal frm = new frmGestionSucursal();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaLista();
	}

	private void biModificar_Click(object sender, EventArgs e)
	{
		if (dgvEmpresas.SelectedRows.Count > 0)
		{
			frmGestionSucursal frm = new frmGestionSucursal();
			frm.Proceso = 2;
			frm.suc = suc;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
		if (dgvEmpresas.CurrentRow.Index != -1 && suc.CodSucursal != 0 && dgvEmpresas.Rows.Count > 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Empresas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmSuc.delete(Convert.ToInt32(suc.CodSucursal)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Empresas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("No se Puede Eliminar el Registro");
		}
	}

	private void biActualizar_Click(object sender, EventArgs e)
	{
		CargaLista();
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

	private void biImprimir_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 4;
		frm.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmSucursales));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.biNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.biModificar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.dgvEmpresas = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codEmpresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ubicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechareg = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.expandablePanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEmpresas).BeginInit();
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
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackgroundMouseOverStyle.Class = "";
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.Class = "";
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[6] { this.biNuevo, this.biModificar, this.biEliminar, this.biActualizar, this.biBuscar, this.biImprimir });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(778, 65);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 4;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.Class = "";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.Class = "";
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.biNuevo.ImageIndex = 4;
		this.biNuevo.ImagePaddingVertical = 15;
		this.biNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNuevo.Name = "biNuevo";
		this.biNuevo.SubItemsExpandWidth = 14;
		this.biNuevo.Text = "Nuevo";
		this.biNuevo.Click += new System.EventHandler(biNuevo_Click);
		this.biModificar.ImageIndex = 3;
		this.biModificar.ImagePaddingVertical = 15;
		this.biModificar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biModificar.Name = "biModificar";
		this.biModificar.SubItemsExpandWidth = 14;
		this.biModificar.Text = "Modificar";
		this.biModificar.Click += new System.EventHandler(biModificar_Click);
		this.biEliminar.ImageIndex = 5;
		this.biEliminar.ImagePaddingVertical = 15;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(biActualizar_Click);
		this.biBuscar.ImageIndex = 11;
		this.biBuscar.ImagePaddingVertical = 15;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biBuscar.Click += new System.EventHandler(biBuscar_Click);
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Click += new System.EventHandler(biImprimir_Click);
		this.expandablePanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.expandablePanel1.AnimationTime = 200;
		this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.expandablePanel1.Controls.Add(this.label4);
		this.expandablePanel1.Controls.Add(this.label3);
		this.expandablePanel1.Controls.Add(this.label2);
		this.expandablePanel1.Controls.Add(this.txtFiltro);
		this.expandablePanel1.Controls.Add(this.label1);
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
		this.expandablePanel1.TabIndex = 6;
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
		this.label4.TabIndex = 11;
		this.label4.Text = "Por :";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
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
		this.label1.Size = new System.Drawing.Size(62, 12);
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
		this.dgvEmpresas.AllowUserToAddRows = false;
		this.dgvEmpresas.AllowUserToDeleteRows = false;
		this.dgvEmpresas.AllowUserToResizeColumns = false;
		this.dgvEmpresas.AllowUserToResizeRows = false;
		this.dgvEmpresas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvEmpresas.Columns.AddRange(this.codigo, this.codEmpresa, this.nombre, this.ubicacion, this.telefono, this.descripcion, this.estado, this.coduser, this.fechareg);
		this.dgvEmpresas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvEmpresas.Location = new System.Drawing.Point(0, 65);
		this.dgvEmpresas.MultiSelect = false;
		this.dgvEmpresas.Name = "dgvEmpresas";
		this.dgvEmpresas.ReadOnly = true;
		this.dgvEmpresas.RowHeadersVisible = false;
		this.dgvEmpresas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvEmpresas.Size = new System.Drawing.Size(778, 409);
		this.dgvEmpresas.TabIndex = 5;
		this.dgvEmpresas.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvEmpresas_ColumnHeaderMouseClick);
		this.dgvEmpresas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvEmpresas_CellMouseDoubleClick);
		this.dgvEmpresas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvEmpresas_RowStateChanged);
		this.codigo.DataPropertyName = "codSucursal";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 80;
		this.codEmpresa.DataPropertyName = "codEmpresa";
		this.codEmpresa.HeaderText = "codEmpresa";
		this.codEmpresa.Name = "codEmpresa";
		this.codEmpresa.ReadOnly = true;
		this.codEmpresa.Visible = false;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Width = 200;
		this.ubicacion.DataPropertyName = "ubicacion";
		this.ubicacion.HeaderText = "ubicacion";
		this.ubicacion.Name = "ubicacion";
		this.ubicacion.ReadOnly = true;
		this.ubicacion.Width = 300;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 300;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "coduser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fechareg.DataPropertyName = "fecharegistro";
		dataGridViewCellStyle2.Format = "d";
		dataGridViewCellStyle2.NullValue = null;
		this.fechareg.DefaultCellStyle = dataGridViewCellStyle2;
		this.fechareg.HeaderText = "Fecha Reg.";
		this.fechareg.Name = "fechareg";
		this.fechareg.ReadOnly = true;
		this.fechareg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(778, 474);
		base.Controls.Add(this.expandablePanel1);
		base.Controls.Add(this.dgvEmpresas);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmSucursales";
		this.RightToLeftLayout = true;
		this.Text = "Sucursales";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmEmpresas_Load);
		base.Shown += new System.EventHandler(frmEmpresas_Shown);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmEmpresas_KeyDown);
		this.expandablePanel1.ResumeLayout(false);
		this.expandablePanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEmpresas).EndInit();
		base.ResumeLayout(false);
	}
}
