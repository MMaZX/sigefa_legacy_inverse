using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmVentasDiarias : Office2007Form
{
	private clsAdmProveedor AdmPro = new clsAdmProveedor();

	private clsProveedor pro = new clsProveedor();

	public int Proceso = 0;

	public int Procede = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int codVendedor;

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvProveedor;

	private ImageList imageList1;

	private Button btnAceptar;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn ruc;

	private DataGridViewTextBoxColumn razonsocial;

	private Button btnNuevo;

	private DataGridView dataGridView1;

	public DateTimePicker dtpFecha;

	private Label label4;

	private DataGridViewCheckBoxColumn Seleccionar;

	private DataGridViewTextBoxColumn codFactura;

	private DataGridViewTextBoxColumn documento1;

	private DataGridViewTextBoxColumn numdoc1;

	private DataGridViewTextBoxColumn cliente1;

	private DataGridViewTextBoxColumn moneda1;

	private DataGridViewTextBoxColumn importe1;

	private DataGridViewTextBoxColumn codvendedor1;

	public frmVentasDiarias()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dataGridView1.DataSource = data;
		data.DataSource = AdmVenta.VentasDiarias(frmLogin.iCodAlmacen, dtpFecha.Value, codVendedor);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dataGridView1.ClearSelection();
		if (dataGridView1.RowCount > 0)
		{
			btnAceptar.Enabled = true;
		}
		else
		{
			btnAceptar.Enabled = false;
		}
	}

	private void frmProveedoresLista_Load(object sender, EventArgs e)
	{
		CargaLista();
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

	private void dgvProveedor_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvProveedor.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvProveedor.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Procede == 1)
		{
			frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
			form.CodProveedor = pro.CodProveedor;
			form.txtCodProv.Text = pro.Ruc;
			form.txtNombreProv.Text = pro.RazonSocial;
			Close();
		}
		else if (Procede == 2)
		{
			frmGestionLetra form2 = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
			form2.CodProveedor = pro.CodProveedor;
			Close();
		}
	}

	private void dgvProveedor_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvProveedor.Rows.Count >= 1 && e.Row.Selected)
		{
			pro.CodProveedor = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			pro.Ruc = e.Row.Cells[ruc.Name].Value.ToString();
			pro.RazonSocial = e.Row.Cells[razonsocial.Name].Value.ToString();
		}
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		frmConsultorExt form = (frmConsultorExt)Application.OpenForms["frmConsultorExt"];
		form.CodVendedor = codVendedor;
		if (dataGridView1.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dataGridView1.Rows)
			{
				string flg = Convert.ToString(row.Cells[Seleccionar.Name].Value);
				if (flg == "1")
				{
					form.listNotas.Add(Convert.ToInt32(row.Cells[codFactura.Name].Value));
				}
			}
		}
		Close();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		frmGestionProveedor frm = new frmGestionProveedor();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaLista();
	}

	private void dgvProveedor_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnAceptar.Enabled = true;
		}
	}

	private void dgvProveedor_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void dgvProveedor_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && dgvProveedor.SelectedRows.Count > 0)
		{
			if (Procede == 1)
			{
				frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
				form.CodProveedor = pro.CodProveedor;
				form.txtCodProv.Text = pro.Ruc;
				form.txtNombreProv.Text = pro.RazonSocial;
				Close();
			}
			else if (Procede == 2)
			{
				frmGestionLetra form2 = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
				form2.CodProveedor = pro.CodProveedor;
				Close();
			}
		}
	}

	private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVentasDiarias));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codvendedor1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvProveedor = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvProveedor).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.dataGridView1);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvProveedor);
		this.groupBox1.Controls.Add(this.btnNuevo);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(524, 335);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Seleccionar Ventas";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(172, 45);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 21;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 49);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(43, 13);
		this.label4.TabIndex = 20;
		this.label4.Text = "Fecha :";
		this.dataGridView1.AllowDrop = true;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.Seleccionar, this.codFactura, this.documento1, this.numdoc1, this.cliente1, this.moneda1, this.importe1, this.codvendedor1);
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dataGridView1.Location = new System.Drawing.Point(6, 79);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidth = 40;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(512, 251);
		this.dataGridView1.TabIndex = 8;
		this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
		this.Seleccionar.DataPropertyName = "Seleccionar";
		this.Seleccionar.HeaderText = "Seleccionar";
		this.Seleccionar.Name = "Seleccionar";
		this.codFactura.DataPropertyName = "codFactura";
		this.codFactura.HeaderText = "codFactura";
		this.codFactura.Name = "codFactura";
		this.codFactura.ReadOnly = true;
		this.codFactura.Visible = false;
		this.documento1.DataPropertyName = "documento1";
		this.documento1.HeaderText = "TipoDoc.";
		this.documento1.Name = "documento1";
		this.documento1.ReadOnly = true;
		this.numdoc1.DataPropertyName = "numdoc1";
		this.numdoc1.HeaderText = "N° Documento";
		this.numdoc1.Name = "numdoc1";
		this.numdoc1.ReadOnly = true;
		this.cliente1.DataPropertyName = "cliente1";
		this.cliente1.HeaderText = "Cliente";
		this.cliente1.Name = "cliente1";
		this.cliente1.ReadOnly = true;
		this.moneda1.DataPropertyName = "moneda1";
		this.moneda1.HeaderText = "Moneda";
		this.moneda1.Name = "moneda1";
		this.moneda1.ReadOnly = true;
		this.importe1.DataPropertyName = "importe1";
		this.importe1.HeaderText = "Importe";
		this.importe1.Name = "importe1";
		this.importe1.ReadOnly = true;
		this.codvendedor1.DataPropertyName = "codvendedor1";
		this.codvendedor1.HeaderText = "codvendedor1";
		this.codvendedor1.Name = "codvendedor1";
		this.codvendedor1.ReadOnly = true;
		this.codvendedor1.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(369, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(77, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(16, 15);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(172, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(215, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvProveedor.AllowUserToAddRows = false;
		this.dgvProveedor.AllowUserToDeleteRows = false;
		this.dgvProveedor.AllowUserToResizeColumns = false;
		this.dgvProveedor.AllowUserToResizeRows = false;
		this.dgvProveedor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProveedor.Columns.AddRange(this.codigo, this.ruc, this.razonsocial);
		this.dgvProveedor.Location = new System.Drawing.Point(6, 101);
		this.dgvProveedor.Name = "dgvProveedor";
		this.dgvProveedor.ReadOnly = true;
		this.dgvProveedor.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvProveedor.RowHeadersVisible = false;
		this.dgvProveedor.RowHeadersWidth = 40;
		this.dgvProveedor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvProveedor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProveedor.Size = new System.Drawing.Size(272, 144);
		this.dgvProveedor.TabIndex = 2;
		this.dgvProveedor.Visible = false;
		this.dgvProveedor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProveedor_CellDoubleClick);
		this.dgvProveedor.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvProveedor_ColumnHeaderMouseClick);
		this.dgvProveedor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProveedor_CellClick);
		this.dgvProveedor.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvProveedor_KeyDown);
		this.dgvProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvProveedor_KeyPress);
		this.dgvProveedor.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvProveedor_RowStateChanged);
		this.codigo.DataPropertyName = "codProveedor";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.ruc.DataPropertyName = "ruc";
		this.ruc.HeaderText = "RUC";
		this.ruc.Name = "ruc";
		this.ruc.ReadOnly = true;
		this.ruc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.ruc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Razon Social";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.ReadOnly = true;
		this.razonsocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.razonsocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.razonsocial.Width = 400;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(52, 298);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 19;
		this.btnNuevo.Text = "&Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Visible = false;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(342, 341);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(96, 32);
		this.btnAceptar.TabIndex = 14;
		this.btnAceptar.Text = "Seleccionar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(444, 341);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 13;
		this.btnSalir.Text = "&Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AcceptButton = this.btnAceptar;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(524, 387);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVentasDiarias";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ventas Diarias";
		base.Load += new System.EventHandler(frmProveedoresLista_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvProveedor).EndInit();
		base.ResumeLayout(false);
	}
}
