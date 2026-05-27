using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmNotasDebitoVentas : Office2007Form
{
	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox2;

	private Button btnAnular;

	private Button btnSalir;

	private Button btGenVenta;

	private Button btnIrGuia;

	private DataGridView dgvNotasCredito;

	private GroupBox groupBox1;

	private Button btnReporte;

	private Label label6;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private TextBox txtFiltro;

	private Label label2;

	private Label label1;

	private Label label5;

	private Label label7;

	private Label label4;

	private Label label3;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn CodCliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn codreferencia;

	private DataGridViewTextBoxColumn Total;

	private DataGridViewTextBoxColumn vendedor;

	private DataGridViewTextBoxColumn anulado;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn CodFacturaVentaRef;

	public frmNotasDebitoVentas()
	{
		InitializeComponent();
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvNotasCredito.DataSource = data;
		data.DataSource = AdmVenta.ListaNotasDebito(frmLogin.iCodAlmacen, dtpDesde.Value, dtpHasta.Value);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvNotasCredito.ClearSelection();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnIrGuia_Click(object sender, EventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && dgvNotasCredito.CurrentRow != null)
		{
			DataGridViewRow row = dgvNotasCredito.CurrentRow;
			frmNotadeDebito form = new frmNotadeDebito();
			form.MdiParent = base.MdiParent;
			form.CodNota = venta.CodFacturaVenta;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void dgvNotasCredito_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotadeDebito form = new frmNotadeDebito();
			form.MdiParent = base.MdiParent;
			form.CodNota = venta.CodFacturaVenta;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void dgvNotasCredito_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && e.Row.Selected)
		{
			venta.CodFacturaVenta = e.Row.Cells[codigo.Name].Value.ToString();
		}
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (venta.CodFacturaVenta != "")
		{
			if (Application.OpenForms["frmVenta"] != null)
			{
				Application.OpenForms["frmVenta"].Close();
				return;
			}
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.CodVenta = dgvNotasCredito.CurrentRow.Cells[CodFacturaVentaRef.Name].Value.ToString();
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvNotasCredito.CurrentRow != null && venta.CodFacturaVenta != "")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la nota seleccionada", "Notas de Débito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmVenta.anular(Convert.ToInt32(venta.CodFacturaVenta)))
			{
				MessageBox.Show("La nota ha sido anulada correctamente", "Notas de Débito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("ListaNotasDebito");
		foreach (DataGridViewColumn column in dgvNotasCredito.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvNotasCredito.Rows.Count; i++)
		{
			DataGridViewRow row = dgvNotasCredito.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvNotasCredito.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\ListaNotasDebitoRPT.xml", XmlWriteMode.WriteSchema);
		CRListaNotasDebito rpt = new CRListaNotasDebito();
		frmListaNotasDebito frm = new frmListaNotasDebito();
		rpt.SetDataSource(ds);
		frm.crvNotasDebito.ReportSource = rpt;
		frm.Show();
	}

	private void frmNotasDebitoVentas_Load(object sender, EventArgs e)
	{
		dtpDesde.Value = dtpDesde.Value.AddDays(-90.0);
		CargaLista();
	}

	private void frmNotasDebitoVentas_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
	}

	private void dgvNotasCredito_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvNotasCredito.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvNotasCredito.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvNotasCredito.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotasDebitoVentas));
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnAnular = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.dgvNotasCredito = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codreferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodFacturaVentaRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNotasCredito).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btGenVenta);
		this.groupBox2.Controls.Add(this.btnIrGuia);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 406);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1194, 56);
		this.groupBox2.TabIndex = 56;
		this.groupBox2.TabStop = false;
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(18, 13);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(102, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular Nota de Credito";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1113, 13);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(912, 13);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 37);
		this.btGenVenta.TabIndex = 3;
		this.btGenVenta.Text = "Documento Referencia";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnIrGuia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrGuia.ImageIndex = 1;
		this.btnIrGuia.ImageList = this.imageList1;
		this.btnIrGuia.Location = new System.Drawing.Point(1014, 13);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(93, 37);
		this.btnIrGuia.TabIndex = 2;
		this.btnIrGuia.Text = "Consultar";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Click += new System.EventHandler(btnIrGuia_Click);
		this.dgvNotasCredito.AllowUserToAddRows = false;
		this.dgvNotasCredito.AllowUserToDeleteRows = false;
		this.dgvNotasCredito.AllowUserToOrderColumns = true;
		this.dgvNotasCredito.AllowUserToResizeRows = false;
		this.dgvNotasCredito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvNotasCredito.Columns.AddRange(this.codigo, this.numdoc, this.CodCliente, this.cliente, this.fechaemision, this.docref, this.codreferencia, this.Total, this.vendedor, this.anulado, this.responsable, this.CodFacturaVentaRef);
		this.dgvNotasCredito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvNotasCredito.Location = new System.Drawing.Point(0, 70);
		this.dgvNotasCredito.MultiSelect = false;
		this.dgvNotasCredito.Name = "dgvNotasCredito";
		this.dgvNotasCredito.ReadOnly = true;
		this.dgvNotasCredito.RowHeadersVisible = false;
		this.dgvNotasCredito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNotasCredito.Size = new System.Drawing.Size(1194, 392);
		this.dgvNotasCredito.TabIndex = 55;
		this.dgvNotasCredito.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNotasCredito_CellDoubleClick);
		this.dgvNotasCredito.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvNotasCredito_ColumnHeaderMouseClick);
		this.dgvNotasCredito.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvNotasCredito_RowStateChanged);
		this.codigo.DataPropertyName = "codNotaIngreso";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Width = 80;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N. Doc.";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.CodCliente.DataPropertyName = "codCliente";
		this.CodCliente.HeaderText = "Cod. Cliente";
		this.CodCliente.Name = "CodCliente";
		this.CodCliente.ReadOnly = true;
		this.CodCliente.Visible = false;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cliente.Width = 270;
		this.fechaemision.DataPropertyName = "fecha";
		this.fechaemision.HeaderText = "F. Emision";
		this.fechaemision.Name = "fechaemision";
		this.fechaemision.ReadOnly = true;
		this.fechaemision.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fechaemision.Width = 120;
		this.docref.DataPropertyName = "docref";
		this.docref.HeaderText = "Doc. Ref.";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.codreferencia.DataPropertyName = "codReferencia";
		this.codreferencia.HeaderText = "Cod. ref";
		this.codreferencia.Name = "codreferencia";
		this.codreferencia.ReadOnly = true;
		this.codreferencia.Visible = false;
		this.Total.DataPropertyName = "total";
		this.Total.HeaderText = "Total";
		this.Total.Name = "Total";
		this.Total.ReadOnly = true;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.ReadOnly = true;
		this.vendedor.Width = 150;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 150;
		this.CodFacturaVentaRef.DataPropertyName = "documentoreferencia";
		this.CodFacturaVentaRef.HeaderText = "Codfactura";
		this.CodFacturaVentaRef.Name = "CodFacturaVentaRef";
		this.CodFacturaVentaRef.ReadOnly = true;
		this.CodFacturaVentaRef.Visible = false;
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1194, 70);
		this.groupBox1.TabIndex = 54;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Notas de Debito";
		this.btnReporte.ImageIndex = 11;
		this.btnReporte.Location = new System.Drawing.Point(500, 26);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 33);
		this.btnReporte.TabIndex = 60;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.SteelBlue;
		this.label6.Location = new System.Drawing.Point(462, 37);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(11, 12);
		this.label6.TabIndex = 59;
		this.label6.Text = "x";
		this.label6.Visible = false;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(99, 33);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(81, 20);
		this.dtpHasta.TabIndex = 58;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(12, 33);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 57;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(249, 33);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 56;
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(10, 18);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 12);
		this.label2.TabIndex = 55;
		this.label2.Text = "Desde";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(247, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(32, 12);
		this.label1.TabIndex = 54;
		this.label1.Text = "Filtro";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(285, 18);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 12);
		this.label5.TabIndex = 52;
		this.label5.Text = "Por :";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.SteelBlue;
		this.label7.Location = new System.Drawing.Point(320, 18);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(12, 12);
		this.label7.TabIndex = 51;
		this.label7.Text = "X";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(97, 18);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(35, 12);
		this.label4.TabIndex = 49;
		this.label4.Text = "Hasta";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(23, 349);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(37, 12);
		this.label3.TabIndex = 48;
		this.label3.Text = "Desde";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(1194, 462);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.dgvNotasCredito);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotasDebitoVentas";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notas de Debito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotasDebitoVentas_Load);
		base.Shown += new System.EventHandler(frmNotasDebitoVentas_Shown);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvNotasCredito).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
