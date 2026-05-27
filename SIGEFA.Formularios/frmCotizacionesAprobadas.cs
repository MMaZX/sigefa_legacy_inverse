using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCotizacionesAprobadas : Office2007Form
{
	private clsAdmCotizacion AdmCotizacion = new clsAdmCotizacion();

	private clsCotizacion cotizacion = new clsCotizacion();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvCotizaciones;

	private ImageList imageList1;

	private GroupBox groupBox2;

	private Button btnAnular;

	private Button btnIrCotizacion;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn fechavence;

	private DataGridViewTextBoxColumn aprob;

	public frmCotizacionesAprobadas()
	{
		InitializeComponent();
	}

	private void frmCotizacionesAprobadas_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvCotizaciones.DataSource = data;
		data.DataSource = AdmCotizacion.MuestraCotizaciones(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvCotizaciones.ClearSelection();
	}

	private void btnIrCotizacion_Click(object sender, EventArgs e)
	{
		if (dgvCotizaciones.Rows.Count >= 1 && dgvCotizaciones.CurrentRow != null)
		{
			DataGridViewRow row = dgvCotizaciones.CurrentRow;
			frmGestionCotizacion form = new frmGestionCotizacion();
			form.MdiParent = base.MdiParent;
			form.CodCotizacion = cotizacion.CodCotizacion;
			form.txtDocRef.Text = "CT";
			form.aprobado = Convert.ToInt32(dgvCotizaciones.SelectedRows[0].Cells[aprob.Name].Value);
			form.Proceso = 3;
			form.Show();
		}
	}

	private void dgvCotizaciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvCotizaciones.Rows.Count >= 1 && e.Row.Selected)
		{
			cotizacion.CodCotizacion = e.Row.Cells[codigo.Name].Value.ToString();
		}
	}

	private void dgvCotizaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvCotizaciones.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmGestionCotizacion form = new frmGestionCotizacion();
			form.MdiParent = base.MdiParent;
			form.CodCotizacion = cotizacion.CodCotizacion;
			form.aprobado = Convert.ToInt32(dgvCotizaciones.SelectedRows[0].Cells[aprob.Name].Value);
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvCotizaciones.CurrentRow != null && cotizacion.CodCotizacion != "")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la cotizacion seleccionada", "Cotizaciones Vigentes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No)
			{
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCotizacionesAprobadas));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvCotizaciones = new System.Windows.Forms.DataGridView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnIrCotizacion = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechavence = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.aprob = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCotizaciones).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvCotizaciones);
		this.groupBox1.Location = new System.Drawing.Point(13, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(874, 420);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Aprobadas";
		this.dgvCotizaciones.AllowUserToAddRows = false;
		this.dgvCotizaciones.AllowUserToDeleteRows = false;
		this.dgvCotizaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCotizaciones.Columns.AddRange(this.codigo, this.cliente, this.importe, this.fecha, this.documento, this.responsable, this.fechavence, this.aprob);
		this.dgvCotizaciones.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvCotizaciones.Location = new System.Drawing.Point(3, 16);
		this.dgvCotizaciones.Name = "dgvCotizaciones";
		this.dgvCotizaciones.ReadOnly = true;
		this.dgvCotizaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvCotizaciones.Size = new System.Drawing.Size(868, 401);
		this.dgvCotizaciones.TabIndex = 0;
		this.dgvCotizaciones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCotizaciones_CellDoubleClick);
		this.dgvCotizaciones.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCotizaciones_RowStateChanged);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btnIrCotizacion);
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Location = new System.Drawing.Point(16, 439);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(869, 44);
		this.groupBox2.TabIndex = 8;
		this.groupBox2.TabStop = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(736, 7);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 7;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnIrCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrCotizacion.ImageIndex = 1;
		this.btnIrCotizacion.ImageList = this.imageList1;
		this.btnIrCotizacion.Location = new System.Drawing.Point(607, 7);
		this.btnIrCotizacion.Name = "btnIrCotizacion";
		this.btnIrCotizacion.Size = new System.Drawing.Size(97, 37);
		this.btnIrCotizacion.TabIndex = 6;
		this.btnIrCotizacion.Text = "Ir Cotizacion";
		this.btnIrCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrCotizacion.UseVisualStyleBackColor = true;
		this.btnIrCotizacion.Click += new System.EventHandler(btnIrCotizacion_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(61, 7);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 5;
		this.btnAnular.Text = "Anular Cotizacion";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.codigo.DataPropertyName = "codCotizacion";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Width = 80;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Width = 270;
		this.importe.DataPropertyName = "total";
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Width = 90;
		this.documento.DataPropertyName = "documento";
		this.documento.HeaderText = "T. doc";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Visible = false;
		this.documento.Width = 80;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Width = 180;
		this.fechavence.DataPropertyName = "fechavigencia";
		this.fechavence.HeaderText = "Vig. Hasta";
		this.fechavence.Name = "fechavence";
		this.fechavence.ReadOnly = true;
		this.aprob.DataPropertyName = "aprobado";
		this.aprob.HeaderText = "Aprobado";
		this.aprob.Name = "aprob";
		this.aprob.ReadOnly = true;
		this.aprob.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(899, 494);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCotizacionesAprobadas";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Cotizaciones Aprobadas";
		base.Load += new System.EventHandler(frmCotizacionesAprobadas_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvCotizaciones).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
