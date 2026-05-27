using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmEmpresas : Office2007Form
{
	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private clsEmpresa emp = new clsEmpresa();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem buttonItem16;

	private ButtonItem buttonItem6;

	private ButtonItem buttonItem8;

	private ButtonItem buttonItem4;

	private ButtonItem buttonItem5;

	private ButtonItem buttonItem9;

	private DataGridView dgvEmpresas;

	private ExpandablePanel expandablePanel1;

	private TextBox txtFiltro;

	private Label label1;

	private Button btnSalir;

	private Label label2;

	private Label label3;

	private Label label4;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn razon;

	private DataGridViewTextBoxColumn direccion;

	private DataGridViewTextBoxColumn telefono;

	private DataGridViewTextBoxColumn fax;

	private DataGridViewTextBoxColumn representante;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn fechareg;

	public frmEmpresas()
	{
		InitializeComponent();
	}

	private void buttonItem16_Click(object sender, EventArgs e)
	{
		frmGestionEmpresa frm = new frmGestionEmpresa();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaLista();
	}

	private void CargaLista()
	{
		dgvEmpresas.DataSource = data;
		data.DataSource = AdmEmp.MuestraEmpresas();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvEmpresas.ClearSelection();
	}

	private void frmEmpresas_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "RUC";
		label3.Text = "ruc";
	}

	private void buttonItem6_Click(object sender, EventArgs e)
	{
		if (dgvEmpresas.SelectedRows.Count > 0)
		{
			frmGestionEmpresa frm = new frmGestionEmpresa();
			frm.Proceso = 2;
			frm.emp = emp;
			frm.ShowDialog();
			CargaLista();
		}
	}

	private void dgvEmpresas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvEmpresas.Rows.Count >= 1 && e.Row.Selected)
		{
			emp.CodEmpresa = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
		}
	}

	private void buttonItem8_Click(object sender, EventArgs e)
	{
		if (dgvEmpresas.CurrentRow.Index != -1 && emp.CodEmpresa != 0 && dgvEmpresas.Rows.Count > 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Empresas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmEmp.delete(Convert.ToInt32(emp.CodEmpresa)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Empresas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("No se Puede Eliminar la empresa");
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
			frmGestionEmpresa frm = new frmGestionEmpresa();
			frm.Proceso = 3;
			frm.emp = emp;
			frm.ShowDialog();
		}
	}

	private void buttonItem4_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void buttonItem9_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("Empresas");
		foreach (DataGridViewColumn column in dgvEmpresas.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvEmpresas.Rows.Count; i++)
		{
			DataGridViewRow row = dgvEmpresas.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvEmpresas.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmEmpresasRP frm = new frmEmpresasRP();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmEmpresas));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.dgvEmpresas = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razon = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.representante = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[6] { this.buttonItem16, this.buttonItem6, this.buttonItem8, this.buttonItem4, this.buttonItem5, this.buttonItem9 });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(778, 55);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 4;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleVisible = false;
		this.buttonItem16.ImageIndex = 4;
		this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem16.Name = "buttonItem16";
		this.buttonItem16.SubItemsExpandWidth = 14;
		this.buttonItem16.Text = "Nuevo";
		this.buttonItem16.Click += new System.EventHandler(buttonItem16_Click);
		this.buttonItem6.ImageIndex = 3;
		this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem6.Name = "buttonItem6";
		this.buttonItem6.SubItemsExpandWidth = 14;
		this.buttonItem6.Text = "Modificar";
		this.buttonItem6.Click += new System.EventHandler(buttonItem6_Click);
		this.buttonItem8.ImageIndex = 5;
		this.buttonItem8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem8.Name = "buttonItem8";
		this.buttonItem8.SubItemsExpandWidth = 14;
		this.buttonItem8.Text = "Eliminar";
		this.buttonItem8.Click += new System.EventHandler(buttonItem8_Click);
		this.buttonItem4.ImageIndex = 8;
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Text = "Actualizar";
		this.buttonItem4.Click += new System.EventHandler(buttonItem4_Click);
		this.buttonItem5.ImageIndex = 11;
		this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem5.Name = "buttonItem5";
		this.buttonItem5.SubItemsExpandWidth = 14;
		this.buttonItem5.Text = "Buscar";
		this.buttonItem5.Click += new System.EventHandler(buttonItem5_Click);
		this.buttonItem9.ImageIndex = 7;
		this.buttonItem9.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.SubItemsExpandWidth = 14;
		this.buttonItem9.Text = "Imprimir";
		this.buttonItem9.Click += new System.EventHandler(buttonItem9_Click);
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
		this.dgvEmpresas.Columns.AddRange(this.codigo, this.ruc, this.razon, this.direccion, this.telefono, this.fax, this.representante, this.estado, this.fechareg);
		this.dgvEmpresas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvEmpresas.Location = new System.Drawing.Point(0, 55);
		this.dgvEmpresas.Name = "dgvEmpresas";
		this.dgvEmpresas.ReadOnly = true;
		this.dgvEmpresas.RowHeadersVisible = false;
		this.dgvEmpresas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvEmpresas.Size = new System.Drawing.Size(778, 419);
		this.dgvEmpresas.TabIndex = 5;
		this.dgvEmpresas.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvEmpresas_ColumnHeaderMouseClick);
		this.dgvEmpresas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvEmpresas_CellMouseDoubleClick);
		this.dgvEmpresas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvEmpresas_RowStateChanged);
		this.codigo.DataPropertyName = "codEmpresa";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 80;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "R.U.C.";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razon.DataPropertyName = "razonsocial";
		this.razon.HeaderText = "Razon Social";
		this.razon.Name = "razon";
		this.razon.ReadOnly = true;
		this.razon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razon.Width = 300;
		this.direccion.DataPropertyName = "direccion";
		this.direccion.HeaderText = "Direccion";
		this.direccion.Name = "direccion";
		this.direccion.ReadOnly = true;
		this.direccion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.direccion.Width = 300;
		this.telefono.DataPropertyName = "telefono";
		this.telefono.HeaderText = "Telefono";
		this.telefono.Name = "telefono";
		this.telefono.ReadOnly = true;
		this.telefono.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fax.DataPropertyName = "fax";
		this.fax.HeaderText = "Fax";
		this.fax.Name = "fax";
		this.fax.ReadOnly = true;
		this.fax.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.representante.DataPropertyName = "representante";
		this.representante.HeaderText = "Representante";
		this.representante.Name = "representante";
		this.representante.ReadOnly = true;
		this.representante.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.representante.Width = 200;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.fechareg.DataPropertyName = "fecharegistro";
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
		base.Controls.Add(this.dgvEmpresas);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.KeyPreview = true;
		base.Name = "frmEmpresas";
		this.RightToLeftLayout = true;
		this.Text = "Empresas";
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
