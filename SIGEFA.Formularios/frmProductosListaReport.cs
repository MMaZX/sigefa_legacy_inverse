using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmProductosListaReport : Office2007Form
{
	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmTipoArticulo AdmTip = new clsAdmTipoArticulo();

	public clsProducto pro = new clsProducto();

	public int Proceso = 0;

	public int Inicio = 0;

	public int Procede = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public List<int> seleccion = new List<int>();

	public int codAlmacen;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvProductos;

	private ImageList imageList1;

	private Button button6;

	private ComboBox cbTipoArticulo;

	private Button btnAceptar;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	public frmProductosListaReport()
	{
		InitializeComponent();
	}

	private void frmProductosListaReport_Load(object sender, EventArgs e)
	{
		CargaTipoArticulos();
		cbTipoArticulo.SelectedIndex = 0;
		CargaLista(Inicio);
		label2.Text = "Referencia";
		label3.Text = "referencia";
	}

	private void dgvProductos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvProductos.Rows.Count >= 1 && e.Row.Selected)
		{
			pro.CodProducto = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			pro.Referencia = e.Row.Cells[referencia.Name].Value.ToString();
			pro.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
		}
	}

	private void CargaLista(int inicio)
	{
		dgvProductos.DataSource = data;
		data.DataSource = AdmPro.ListaProductosReporte(codAlmacen, Convert.ToInt32(cbTipoArticulo.SelectedValue), inicio);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvProductos.ClearSelection();
	}

	private void CargaTipoArticulos()
	{
		cbTipoArticulo.DataSource = AdmTip.MuestraTipoArticulos();
		cbTipoArticulo.DisplayMember = "descripcion";
		cbTipoArticulo.ValueMember = "codTipoArticulo";
		cbTipoArticulo.SelectedIndex = -1;
	}

	private void frmProductosListaReport_Shown(object sender, EventArgs e)
	{
		CargaLista(Inicio);
		txtFiltro.Focus();
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

	private void dgvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvProductos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvProductos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void button6_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		base.DialogResult = DialogResult.Yes;
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Yes;
	}

	private void cbTipoArticulo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLista(Inicio);
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvProductos.Focus();
		}
	}

	private void dgvProductos_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && dgvProductos.Rows.Count > 0)
		{
			int f = dgvProductos.CurrentRow.Index;
			pro.CodProducto = Convert.ToInt32(dgvProductos.Rows[f].Cells[codigo.Name].Value);
			pro.Referencia = dgvProductos.Rows[f].Cells[referencia.Name].Value.ToString();
			pro.Descripcion = dgvProductos.Rows[f].Cells[descripcion.Name].Value.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductosListaReport));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.cbTipoArticulo = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvProductos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button6 = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.cbTipoArticulo);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvProductos);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(444, 414);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Productos";
		this.cbTipoArticulo.FormattingEnabled = true;
		this.cbTipoArticulo.Location = new System.Drawing.Point(267, 39);
		this.cbTipoArticulo.Name = "cbTipoArticulo";
		this.cbTipoArticulo.Size = new System.Drawing.Size(165, 21);
		this.cbTipoArticulo.TabIndex = 2;
		this.cbTipoArticulo.Tag = "1";
		this.cbTipoArticulo.SelectionChangeCommitted += new System.EventHandler(cbTipoArticulo_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(258, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(87, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(19, 39);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(236, 20);
		this.txtFiltro.TabIndex = 1;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(16, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvProductos.AllowUserToAddRows = false;
		this.dgvProductos.AllowUserToDeleteRows = false;
		this.dgvProductos.AllowUserToResizeColumns = false;
		this.dgvProductos.AllowUserToResizeRows = false;
		this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProductos.Columns.AddRange(this.codigo, this.referencia, this.descripcion);
		this.dgvProductos.Location = new System.Drawing.Point(6, 73);
		this.dgvProductos.MultiSelect = false;
		this.dgvProductos.Name = "dgvProductos";
		this.dgvProductos.ReadOnly = true;
		this.dgvProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvProductos.RowHeadersVisible = false;
		this.dgvProductos.RowHeadersWidth = 40;
		this.dgvProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProductos.Size = new System.Drawing.Size(432, 335);
		this.dgvProductos.StandardTab = true;
		this.dgvProductos.TabIndex = 3;
		this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellDoubleClick);
		this.dgvProductos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvProductos_ColumnHeaderMouseClick);
		this.dgvProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvProductos_KeyDown);
		this.dgvProductos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvProductos_RowStateChanged);
		this.codigo.DataPropertyName = "codProducto";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.descripcion.Width = 400;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.button6.ImageIndex = 5;
		this.button6.ImageList = this.imageList1;
		this.button6.Location = new System.Drawing.Point(371, 420);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(68, 32);
		this.button6.TabIndex = 5;
		this.button6.Text = "Salir";
		this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(288, 420);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 4;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		base.AcceptButton = this.btnAceptar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.button6;
		base.ClientSize = new System.Drawing.Size(444, 458);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.button6);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmProductosListaReport";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Productos";
		base.Load += new System.EventHandler(frmProductosListaReport_Load);
		base.Shown += new System.EventHandler(frmProductosListaReport_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).EndInit();
		base.ResumeLayout(false);
	}
}
