using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmNotasDebitoCompras : Office2007Form
{
	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsNotaSalida notaS = new clsNotaSalida();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int codnotaI = 0;

	private int codFact = 0;

	private string DocRef = "";

	private clsFactura fac = new clsFactura();

	private clsAdmFactura AdmFact = new clsAdmFactura();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnSalir;

	private Button btnIrGuia;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private Label label3;

	private Label label4;

	private Label label1;

	private Label label5;

	private Label label7;

	private Label label2;

	private TextBox txtFiltro;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Label label6;

	private Button btnReporte;

	private GroupBox groupBox2;

	private DataGridView dgvNotasCredito;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn fechaemision;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn codreferencia;

	private DataGridViewTextBoxColumn anulado;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn numerorefencias;

	private DataGridViewTextBoxColumn codProveedors;

	private DataGridViewTextBoxColumn NotaIngreso;

	public frmNotasDebitoCompras()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvNotasCredito.DataSource = data;
		data.DataSource = AdmFact.ListaNotasDebitoCompra(frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvNotasCredito.ClearSelection();
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && dgvNotasCredito.CurrentRow != null)
		{
			DataGridViewRow row = dgvNotasCredito.CurrentRow;
			frmNotadeDebitoCompra form = new frmNotadeDebitoCompra();
			form.MdiParent = base.MdiParent;
			form.CodNotaS = Convert.ToInt32(codFact);
			form.DocRef = DocRef;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmNotasCredito_Load(object sender, EventArgs e)
	{
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (nota.CodNotaIngreso != "")
		{
			if (Application.OpenForms["frmNotaIngreso"] != null)
			{
				Application.OpenForms["frmNotaIngreso"].Close();
				return;
			}
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = Convert.ToString(codnotaI);
			form.Proceso = 3;
			form.Show();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvNotasCredito.CurrentRow != null && codFact != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la nota seleccionada", "Notas de Credito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmFact.anular(codFact))
			{
				MessageBox.Show("La nota ha sido anulada correctamente", "Notas de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label6.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("ListaNotasCredito");
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
		ds.WriteXml("C:\\XML\\ListaNotasCreditoRPT.xml", XmlWriteMode.WriteSchema);
		CRListaNotas rpt = new CRListaNotas();
		frmListaNotasCredito frm = new frmListaNotasCredito();
		rpt.SetDataSource(ds);
		frm.crvNotasCredito.ReportSource = rpt;
		frm.Show();
	}

	private void dgvNotasCredito_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label7.Text = dgvNotasCredito.Columns[e.ColumnIndex].HeaderText;
		label6.Text = dgvNotasCredito.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged_1(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvNotasCredito_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvNotasCredito.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotadeDebitoCompra form = new frmNotadeDebitoCompra();
			form.MdiParent = base.MdiParent;
			form.CodNotaS = Convert.ToInt32(codFact);
			form.DocRef = DocRef;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmNotasDebitoCompras_Load(object sender, EventArgs e)
	{
		dtpDesde.Value = dtpDesde.Value.AddDays(-90.0);
		CargaLista();
		label7.Text = "Cliente";
		label6.Text = "cliente";
	}

	private void dgvNotasCredito_RowStateChanged_1(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvNotasCredito.DataSource != null && dgvNotasCredito.Rows.Count >= 1 && e.Row.Selected)
		{
			codFact = Convert.ToInt32(e.Row.Cells[codigo.Name].Value.ToString());
			DocRef = e.Row.Cells[numerorefencias.Name].Value.ToString();
			codnotaI = Convert.ToInt32(e.Row.Cells[NotaIngreso.Name].Value.ToString());
		}
	}

	private void frmNotasDebitoCompras_Shown(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotasDebitoCompras));
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
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAnular = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvNotasCredito = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codreferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerorefencias = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProveedors = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.NotaIngreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNotasCredito).BeginInit();
		base.SuspendLayout();
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
		this.groupBox1.Size = new System.Drawing.Size(1204, 70);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Notas de Credito";
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
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged_1);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(12, 33);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 57;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged_1);
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(249, 33);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(207, 20);
		this.txtFiltro.TabIndex = 56;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
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
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
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
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(922, 13);
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
		this.btnIrGuia.Location = new System.Drawing.Point(1024, 13);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(93, 37);
		this.btnIrGuia.TabIndex = 2;
		this.btnIrGuia.Text = "Consultar";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Click += new System.EventHandler(btnIrPedido_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1123, 13);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btGenVenta);
		this.groupBox2.Controls.Add(this.btnIrGuia);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 420);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1204, 56);
		this.groupBox2.TabIndex = 53;
		this.groupBox2.TabStop = false;
		this.dgvNotasCredito.AllowUserToAddRows = false;
		this.dgvNotasCredito.AllowUserToDeleteRows = false;
		this.dgvNotasCredito.AllowUserToOrderColumns = true;
		this.dgvNotasCredito.AllowUserToResizeRows = false;
		this.dgvNotasCredito.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvNotasCredito.Columns.AddRange(this.codigo, this.numdoc, this.cliente, this.fechaemision, this.docref, this.codreferencia, this.anulado, this.responsable, this.numerorefencias, this.codProveedors, this.NotaIngreso);
		this.dgvNotasCredito.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvNotasCredito.Location = new System.Drawing.Point(0, 70);
		this.dgvNotasCredito.MultiSelect = false;
		this.dgvNotasCredito.Name = "dgvNotasCredito";
		this.dgvNotasCredito.ReadOnly = true;
		this.dgvNotasCredito.RowHeadersVisible = false;
		this.dgvNotasCredito.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNotasCredito.Size = new System.Drawing.Size(1204, 406);
		this.dgvNotasCredito.TabIndex = 52;
		this.dgvNotasCredito.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNotasCredito_CellDoubleClick);
		this.dgvNotasCredito.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvNotasCredito_ColumnHeaderMouseClick);
		this.dgvNotasCredito.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvNotasCredito_RowStateChanged_1);
		this.codigo.DataPropertyName = "codNotaSalida";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Width = 80;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N. Doc.";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.cliente.DataPropertyName = "razonsocial";
		this.cliente.HeaderText = "Proveedor";
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
		this.docref.DataPropertyName = "documentoreferencia";
		this.docref.HeaderText = "Doc. Ref.";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.docref.Visible = false;
		this.codreferencia.DataPropertyName = "codReferencia";
		this.codreferencia.HeaderText = "Cod. ref";
		this.codreferencia.Name = "codreferencia";
		this.codreferencia.ReadOnly = true;
		this.codreferencia.Visible = false;
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
		this.numerorefencias.DataPropertyName = "numerorefencia";
		this.numerorefencias.HeaderText = "Num. Doc. Ref";
		this.numerorefencias.Name = "numerorefencias";
		this.numerorefencias.ReadOnly = true;
		this.codProveedors.DataPropertyName = "codProveedor";
		this.codProveedors.HeaderText = "codProveedor";
		this.codProveedors.Name = "codProveedors";
		this.codProveedors.ReadOnly = true;
		this.codProveedors.Visible = false;
		this.NotaIngreso.DataPropertyName = "codNotaIngreso";
		this.NotaIngreso.HeaderText = "NotaIngreso";
		this.NotaIngreso.Name = "NotaIngreso";
		this.NotaIngreso.ReadOnly = true;
		this.NotaIngreso.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1204, 476);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.dgvNotasCredito);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotasDebitoCompras";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notas de Debito";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotasDebitoCompras_Load);
		base.Shown += new System.EventHandler(frmNotasDebitoCompras_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvNotasCredito).EndInit();
		base.ResumeLayout(false);
	}
}
