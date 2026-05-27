using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmTransferencias : Office2007Form
{
	public int tipo = 0;

	private clsAdmNotaIngreso admNotaIngreso = new clsAdmNotaIngreso();

	private clsNotaIngreso notaIngreso = new clsNotaIngreso();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvOrdenes;

	private Button btnSalir;

	private Button btnIrCotizacion;

	private ImageList imageList1;

	private Button btnAnular;

	private ImageList imageList2;

	private ImageList imageList3;

	private Button button1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn serie;

	private DataGridViewTextBoxColumn numdocumento;

	private DataGridViewTextBoxColumn Documento;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn Comentario;

	private DataGridViewTextBoxColumn codAlmacen;

	private DataGridViewTextBoxColumn Transferencia;

	public frmTransferencias()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void CargaLista()
	{
		dgvOrdenes.DataSource = data;
		data.DataSource = admNotaIngreso.MuestraTransferenciasVigentes(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void btnIrCotizacion_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && dgvOrdenes.CurrentRow != null)
		{
			DataGridViewRow row = dgvOrdenes.CurrentRow;
			frmTranferenciaDirecta form = new frmTranferenciaDirecta();
			form.MdiParent = base.MdiParent;
			form.CodTransferencia = notaIngreso.CodNotaIngreso;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void dgvCotizaciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && e.Row.Selected)
		{
			notaIngreso.CodNotaIngreso = e.Row.Cells[codigo.Name].Value.ToString();
			notaIngreso.CodSerie = Convert.ToInt32(e.Row.Cells[serie.Name].Value);
			notaIngreso.NumDoc = e.Row.Cells[numdocumento.Name].Value.ToString();
		}
	}

	private void dgvCotizaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvOrdenes.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmTranferenciaDirecta form = new frmTranferenciaDirecta();
			form.MdiParent = base.MdiParent;
			form.CodTransferencia = notaIngreso.CodNotaIngreso;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.CurrentRow == null || Convert.ToInt32(notaIngreso.CodNotaIngreso) == 0)
		{
			return;
		}
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la transferencia seleccionada", "Transferencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult != DialogResult.No)
		{
			if (Application.OpenForms["frmVigenciaCotizacion"] != null)
			{
				Application.OpenForms["frmVigenciaCotizacion"].Close();
			}
			else
			{
				frmVigenciaCotizacion form = new frmVigenciaCotizacion();
				form.Proceso = 2;
				form.Procede = 2;
				form.groupBox2.Visible = true;
				form.groupBox1.Visible = false;
				form.serie = notaIngreso.CodSerie;
				form.numeracion = notaIngreso.NumDoc;
				form.Show();
				Close();
			}
			CargaLista();
		}
	}

	private void btnAtendido_Click(object sender, EventArgs e)
	{
		if (dgvOrdenes.CurrentRow != null && Convert.ToInt32(notaIngreso.CodNotaIngreso) != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aceptar la Transferencia seleccionada", "Transferencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admNotaIngreso.atender(Convert.ToInt32(notaIngreso.CodNotaIngreso), notaIngreso.CodSerie, notaIngreso.NumDoc, frmLogin.iCodUser))
			{
				MessageBox.Show("La Transferencia ha sido Atendida correctamente", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void frmTransferencias_Load(object sender, EventArgs e)
	{
		if (tipo == 1)
		{
			CargaLista();
		}
	}

	private void frmTransferencias_Shown(object sender, EventArgs e)
	{
		CargaLista();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTransferencias));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvOrdenes = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Transferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnIrCotizacion = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvOrdenes);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(636, 360);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vigentes";
		this.dgvOrdenes.AllowUserToAddRows = false;
		this.dgvOrdenes.AllowUserToDeleteRows = false;
		this.dgvOrdenes.AllowUserToOrderColumns = true;
		this.dgvOrdenes.AllowUserToResizeRows = false;
		this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvOrdenes.Columns.AddRange(this.codigo, this.serie, this.numdocumento, this.Documento, this.fecha, this.responsable, this.Comentario, this.codAlmacen, this.Transferencia);
		this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOrdenes.Location = new System.Drawing.Point(3, 16);
		this.dgvOrdenes.MultiSelect = false;
		this.dgvOrdenes.Name = "dgvOrdenes";
		this.dgvOrdenes.ReadOnly = true;
		this.dgvOrdenes.RowHeadersVisible = false;
		this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvOrdenes.Size = new System.Drawing.Size(630, 341);
		this.dgvOrdenes.TabIndex = 0;
		this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvCotizaciones_CellDoubleClick);
		this.dgvOrdenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvCotizaciones_RowStateChanged);
		this.codigo.DataPropertyName = "codNotaIngreso";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigo.Width = 160;
		this.serie.DataPropertyName = "serie";
		this.serie.HeaderText = "Serie";
		this.serie.Name = "serie";
		this.serie.ReadOnly = true;
		this.serie.Visible = false;
		this.numdocumento.DataPropertyName = "numdocumento";
		this.numdocumento.HeaderText = "numdocumento";
		this.numdocumento.Name = "numdocumento";
		this.numdocumento.ReadOnly = true;
		this.numdocumento.Visible = false;
		this.Documento.DataPropertyName = "documento";
		this.Documento.HeaderText = "Documento";
		this.Documento.Name = "Documento";
		this.Documento.ReadOnly = true;
		this.Documento.Width = 150;
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle1;
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 110;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 230;
		this.Comentario.DataPropertyName = "comentario";
		this.Comentario.HeaderText = "Comentario";
		this.Comentario.Name = "Comentario";
		this.Comentario.ReadOnly = true;
		this.Comentario.Visible = false;
		this.codAlmacen.DataPropertyName = "codAlmacen";
		this.codAlmacen.HeaderText = "codAlmacen";
		this.codAlmacen.Name = "codAlmacen";
		this.codAlmacen.ReadOnly = true;
		this.codAlmacen.Visible = false;
		this.Transferencia.DataPropertyName = "transferencia";
		this.Transferencia.HeaderText = "Transferencia";
		this.Transferencia.Name = "Transferencia";
		this.Transferencia.ReadOnly = true;
		this.Transferencia.Width = 150;
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
		this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList2.Images.SetKeyName(17, "Folder open.png");
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.imageList3.Images.SetKeyName(6, "search (1).png");
		this.imageList3.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList3.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList3.Images.SetKeyName(9, "document_delete.png");
		this.imageList3.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList3.Images.SetKeyName(11, "OK_Verde.png");
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(36, 369);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Rechazar";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btnIrCotizacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrCotizacion.ImageIndex = 1;
		this.btnIrCotizacion.ImageList = this.imageList1;
		this.btnIrCotizacion.Location = new System.Drawing.Point(153, 369);
		this.btnIrCotizacion.Name = "btnIrCotizacion";
		this.btnIrCotizacion.Size = new System.Drawing.Size(111, 37);
		this.btnIrCotizacion.TabIndex = 2;
		this.btnIrCotizacion.Text = "Ir Transferencia";
		this.btnIrCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrCotizacion.UseVisualStyleBackColor = true;
		this.btnIrCotizacion.Click += new System.EventHandler(btnIrCotizacion_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(549, 369);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 11;
		this.button1.ImageList = this.imageList3;
		this.button1.Location = new System.Drawing.Point(283, 369);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(111, 37);
		this.button1.TabIndex = 5;
		this.button1.Text = "Aceptar Transferencia";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(btnAtendido_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(636, 415);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btnIrCotizacion);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTransferencias";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Transferencia Vigentes";
		base.Load += new System.EventHandler(frmTransferencias_Load);
		base.Shown += new System.EventHandler(frmTransferencias_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenes).EndInit();
		base.ResumeLayout(false);
	}
}
