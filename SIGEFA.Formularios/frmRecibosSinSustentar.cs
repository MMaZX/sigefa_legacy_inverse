using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRecibosSinSustentar : Office2007Form
{
	private clsRecibos recibos = new clsRecibos();

	private clsAdmRecibo AdmRecibos = new clsAdmRecibo();

	private string doc = "";

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int tipocaja = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvRecibos;

	private ImageList imageList1;

	private Button btnAceptar;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numeracion;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn montopendiente;

	private DataGridViewTextBoxColumn montorendido;

	public frmRecibosSinSustentar()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvRecibos.DataSource = data;
		data.DataSource = AdmRecibos.ListaRecibosEgreso(frmLogin.iCodSucursal, tipocaja);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvRecibos.ClearSelection();
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

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (dgvRecibos.SelectedRows.Count > 0)
		{
			frmCajaChicaRegistro form = (frmCajaChicaRegistro)Application.OpenForms["frmCajaChicaRegistro"];
			form.codRec = recibos.CodRecibos;
			form.txtRecibo.Text = doc;
			form.monto = recibos.Monto;
			form.txtMontoPendiente.Text = recibos.Monto.ToString();
			Close();
		}
	}

	private void frmRecibosSinSustentar_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Documento";
		label3.Text = "documento";
	}

	private void dgvRecibos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		btnAceptar.Enabled = true;
	}

	private void dgvRecibos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvRecibos.Rows.Count >= 1 && e.Row.Selected)
		{
			recibos.CodRecibos = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			recibos.Monto = Convert.ToDecimal(e.Row.Cells[montopendiente.Name].Value);
			doc = e.Row.Cells[documento.Name].Value.ToString();
		}
	}

	private void dgvRecibos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvRecibos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvRecibos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvRecibos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		frmCajaChicaRegistro form = (frmCajaChicaRegistro)Application.OpenForms["frmCajaChicaRegistro"];
		form.codRec = recibos.CodRecibos;
		form.txtRecibo.Text = doc;
		form.monto = recibos.Monto;
		form.txtMontoPendiente.Text = recibos.Monto.ToString();
		Close();
	}

	private void frmRecibosSinSustentar_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRecibosSinSustentar));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvRecibos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montopendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montorendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRecibos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvRecibos);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(734, 393);
		this.groupBox1.TabIndex = 3;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Seleccionar Recibos";
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
		this.dgvRecibos.AllowUserToAddRows = false;
		this.dgvRecibos.AllowUserToDeleteRows = false;
		this.dgvRecibos.AllowUserToResizeColumns = false;
		this.dgvRecibos.AllowUserToResizeRows = false;
		this.dgvRecibos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvRecibos.Columns.AddRange(this.codigo, this.serie, this.numeracion, this.documento, this.concepto, this.monto, this.montopendiente, this.montorendido);
		this.dgvRecibos.Location = new System.Drawing.Point(6, 45);
		this.dgvRecibos.Name = "dgvRecibos";
		this.dgvRecibos.ReadOnly = true;
		this.dgvRecibos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvRecibos.RowHeadersVisible = false;
		this.dgvRecibos.RowHeadersWidth = 40;
		this.dgvRecibos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvRecibos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRecibos.Size = new System.Drawing.Size(706, 342);
		this.dgvRecibos.TabIndex = 2;
		this.dgvRecibos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvRecibos_CellDoubleClick);
		this.dgvRecibos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvRecibos_ColumnHeaderMouseClick);
		this.dgvRecibos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvRecibos_CellClick);
		this.dgvRecibos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvRecibos_RowStateChanged);
		this.codigo.DataPropertyName = "codRecibo";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.serie.DataPropertyName = "serie";
		this.serie.HeaderText = "serie";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serie.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serie.Visible = false;
		this.numeracion.DataPropertyName = "numeracion";
		this.numeracion.HeaderText = "numeracion";
		this.numeracion.Name = "numeracion";
		this.numeracion.ReadOnly = true;
		this.numeracion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.numeracion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.numeracion.Visible = false;
		this.numeracion.Width = 400;
		this.documento.DataPropertyName = "documento";
		this.documento.HeaderText = "Documento";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.concepto.DataPropertyName = "concepto";
		this.concepto.HeaderText = "Concepto";
		this.concepto.Name = "concepto";
		this.concepto.ReadOnly = true;
		this.concepto.Width = 300;
		this.monto.DataPropertyName = "monto";
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.montopendiente.DataPropertyName = "montopendiente";
		this.montopendiente.HeaderText = "Monto Pendiente";
		this.montopendiente.Name = "montopendiente";
		this.montopendiente.ReadOnly = true;
		this.montorendido.DataPropertyName = "montorendido";
		this.montorendido.HeaderText = "Monto Rendido";
		this.montorendido.Name = "montorendido";
		this.montorendido.ReadOnly = true;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.btnAceptar.Enabled = false;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(230, 399);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 14;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(313, 399);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 13;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(734, 440);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmRecibosSinSustentar";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ListaRecibos";
		base.Load += new System.EventHandler(frmRecibosSinSustentar_Load);
		base.Shown += new System.EventHandler(frmRecibosSinSustentar_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRecibos).EndInit();
		base.ResumeLayout(false);
	}
}
