using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmEntregasConsultExt : Office2007Form
{
	private clsConsultasExternas ext = new clsConsultasExternas();

	private clsSerie ser = new clsSerie();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int entregadovend;

	public string cadEstado;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvPedidosPendientes;

	private Button btnSalir;

	private Button btnIrPedido;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private Button button1;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private Button btnReporte;

	private Button btnConsultar;

	private Label label3;

	private Button button2;

	private Button button3;

	private DataGridViewTextBoxColumn codSalConsulExt;

	private DataGridViewTextBoxColumn codVendedor;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn responsable;

	private DataGridViewTextBoxColumn fechaentrega;

	private DataGridViewTextBoxColumn entregado;

	private DataGridViewTextBoxColumn codentregado;

	private Button button4;

	private ImageList imageList2;

	public frmEntregasConsultExt()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		dgvPedidosPendientes.DataSource = data;
		data.DataSource = AdmPedido.MuestraEntregasConsultorExt(frmLogin.iCodAlmacen, dtpDesde.Value, dtpHasta.Value);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvPedidosPendientes.ClearSelection();
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
		{
			DataGridViewRow row = dgvPedidosPendientes.CurrentRow;
			frmConsultorExt form = new frmConsultorExt();
			form.MdiParent = base.MdiParent;
			form.CodPedido = pedido.CodPedido;
			if (cadEstado == "ANULADO")
			{
				form.Proceso = 4;
			}
			else if (entregadovend == 1)
			{
				form.Proceso = 3;
			}
			else if (entregadovend == 0)
			{
				form.Proceso = 4;
			}
			form.Show();
			CargaLista();
		}
		Close();
	}

	private void frmPedidosPendientes_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
		{
			DataGridViewRow row = dgvPedidosPendientes.CurrentRow;
			frmConsultorExt form = new frmConsultorExt();
			form.MdiParent = base.MdiParent;
			form.CodPedido = pedido.CodPedido;
			form.Proceso = 2;
			form.Show();
		}
	}

	private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && e.Row.Selected)
		{
			pedido.CodPedido = e.Row.Cells[codigo.Name].Value.ToString();
			entregadovend = int.Parse(e.Row.Cells[codentregado.Name].Value.ToString());
			cadEstado = Convert.ToString(e.Row.Cells[entregado.Name].Value);
			btnIrPedido.Enabled = true;
			btnAnular.Enabled = true;
			btGenVenta.Enabled = true;
			if (cadEstado == "ANULADO")
			{
				btnAnular.Visible = false;
				btnIrPedido.Text = "Consultar";
			}
			else if (entregadovend == 1)
			{
				btnIrPedido.Text = "Liquidar Entrega";
				btnAnular.Visible = true;
			}
			else if (entregadovend == 0)
			{
				btnIrPedido.Text = "Consultar Liquidado";
				btnAnular.Visible = false;
			}
		}
	}

	private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
		{
			DataGridViewRow row = dgvPedidosPendientes.CurrentRow;
			frmConsultorExt form = new frmConsultorExt();
			form.MdiParent = base.MdiParent;
			form.CodPedido = pedido.CodPedido;
			form.Proceso = 4;
			form.Show();
			CargaLista();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.CurrentRow != null && pedido.CodPedido != "")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar la entrega seleccionada", "Entregas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmPedido.deleteEntConsExt(Convert.ToInt32(pedido.CodPedido)))
			{
				MessageBox.Show("La entrega ha sido anulada correctamente", "Entregas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null && pedido.CodPedido != "")
		{
			if (Application.OpenForms["frmVenta"] != null)
			{
				Application.OpenForms["frmVenta"].Close();
				return;
			}
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.Proceso = 1;
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			form.Show();
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

	private void dgvPedidosPendientes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvPedidosPendientes.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvPedidosPendientes.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
		{
			DataGridViewRow row = dgvPedidosPendientes.CurrentRow;
			frmConsultorExt form = new frmConsultorExt();
			form.MdiParent = base.MdiParent;
			form.CodPedido = pedido.CodPedido;
			form.Proceso = 4;
			form.Show();
			CargaLista();
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmVenta"] != null)
		{
			Application.OpenForms["frmVenta"].Activate();
			return;
		}
		frmVenta form1 = new frmVenta();
		form1.MdiParent = this;
		form1.consultorext = true;
		form1.Proceso = 1;
		form1.Show();
	}

	private void button4_Click(object sender, EventArgs e)
	{
		clsReportePedidos dso = new clsReportePedidos();
		CRConsultorEnt rpt = new CRConsultorEnt();
		frmRptEntregaConsultor frm = new frmRptEntregaConsultor();
		PrintOptions rptoption = rpt.PrintOptions;
		rptoption.PrinterName = ser.NombreImpresora;
		rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
		rpt.SetDataSource(dso.RptMuestraEntregaConsultorExt(Convert.ToInt32(pedido.CodPedido)).Tables[0]);
		frm.crvKardex.ReportSource = rpt;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmEntregasConsultExt));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvPedidosPendientes = new System.Windows.Forms.DataGridView();
		this.codSalConsulExt = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codVendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaentrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.entregado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codentregado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.button2 = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button1 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnIrPedido = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label3 = new System.Windows.Forms.Label();
		this.button4 = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPedidosPendientes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvPedidosPendientes);
		this.groupBox1.Controls.Add(this.button2);
		this.groupBox1.Controls.Add(this.button1);
		this.groupBox1.Controls.Add(this.button3);
		this.groupBox1.Location = new System.Drawing.Point(0, 76);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(832, 284);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Lista de Entregas:";
		this.dgvPedidosPendientes.AllowUserToAddRows = false;
		this.dgvPedidosPendientes.AllowUserToDeleteRows = false;
		this.dgvPedidosPendientes.AllowUserToOrderColumns = true;
		this.dgvPedidosPendientes.AllowUserToResizeRows = false;
		this.dgvPedidosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvPedidosPendientes.Columns.AddRange(this.codSalConsulExt, this.codVendedor, this.codigo, this.cliente, this.importe, this.fecha, this.documento, this.responsable, this.fechaentrega, this.entregado, this.codentregado);
		this.dgvPedidosPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvPedidosPendientes.Location = new System.Drawing.Point(3, 16);
		this.dgvPedidosPendientes.MultiSelect = false;
		this.dgvPedidosPendientes.Name = "dgvPedidosPendientes";
		this.dgvPedidosPendientes.ReadOnly = true;
		this.dgvPedidosPendientes.RowHeadersVisible = false;
		this.dgvPedidosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPedidosPendientes.Size = new System.Drawing.Size(826, 265);
		this.dgvPedidosPendientes.TabIndex = 0;
		this.dgvPedidosPendientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPedidosPendientes_CellDoubleClick);
		this.dgvPedidosPendientes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvPedidosPendientes_ColumnHeaderMouseClick);
		this.dgvPedidosPendientes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvPedidosPendientes_RowStateChanged);
		this.codSalConsulExt.DataPropertyName = "codSalConsulExt";
		this.codSalConsulExt.HeaderText = "codSalConsulExt";
		this.codSalConsulExt.Name = "codSalConsulExt";
		this.codSalConsulExt.ReadOnly = true;
		this.codSalConsulExt.Visible = false;
		this.codVendedor.DataPropertyName = "codVendedor";
		this.codVendedor.HeaderText = "codVendedor";
		this.codVendedor.Name = "codVendedor";
		this.codVendedor.ReadOnly = true;
		this.codVendedor.Visible = false;
		this.codigo.DataPropertyName = "codPedido";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Width = 80;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Vendedor";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cliente.Width = 270;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle1;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Visible = false;
		this.fecha.Width = 120;
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle2;
		this.documento.HeaderText = "T. doc.";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Width = 60;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Responsable";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 180;
		this.fechaentrega.DataPropertyName = "fechaentrega";
		this.fechaentrega.HeaderText = "fechaentrega";
		this.fechaentrega.Name = "fechaentrega";
		this.fechaentrega.ReadOnly = true;
		this.entregado.DataPropertyName = "entregado";
		this.entregado.HeaderText = "Estado";
		this.entregado.Name = "entregado";
		this.entregado.ReadOnly = true;
		this.codentregado.DataPropertyName = "codentregado";
		this.codentregado.HeaderText = "codentregado";
		this.codentregado.Name = "codentregado";
		this.codentregado.ReadOnly = true;
		this.codentregado.Visible = false;
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button2.ImageIndex = 2;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(524, 171);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(96, 37);
		this.button2.TabIndex = 24;
		this.button2.Text = "Ingresar Ventas";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 2;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(445, 214);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(96, 37);
		this.button1.TabIndex = 5;
		this.button1.Text = "Generar Venta";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Visible = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button3.ImageIndex = 1;
		this.button3.ImageList = this.imageList1;
		this.button3.Location = new System.Drawing.Point(327, 171);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(96, 37);
		this.button3.TabIndex = 25;
		this.button3.Text = "Consultar Entrega";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Visible = false;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(745, 366);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnIrPedido.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrPedido.Enabled = false;
		this.btnIrPedido.ImageIndex = 1;
		this.btnIrPedido.ImageList = this.imageList1;
		this.btnIrPedido.Location = new System.Drawing.Point(643, 366);
		this.btnIrPedido.Name = "btnIrPedido";
		this.btnIrPedido.Size = new System.Drawing.Size(96, 37);
		this.btnIrPedido.TabIndex = 2;
		this.btnIrPedido.Text = "Liquidar Entrega";
		this.btnIrPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrPedido.UseVisualStyleBackColor = true;
		this.btnIrPedido.Click += new System.EventHandler(btnIrPedido_Click);
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.Enabled = false;
		this.btGenVenta.ImageIndex = 1;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(177, 366);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 37);
		this.btGenVenta.TabIndex = 3;
		this.btGenVenta.Text = "Editar Entrega";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Visible = false;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.Enabled = false;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(77, 366);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Eliminar Entrega";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 36);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 13);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(65, 36);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 14;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(65, 10);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 15;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(256, 9);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 20;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(344, 6);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(317, 20);
		this.txtFiltro.TabIndex = 19;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(180, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(56, 13);
		this.label1.TabIndex = 18;
		this.label1.Text = "Filtrar por :";
		this.btnReporte.AllowDrop = true;
		this.btnReporte.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReporte.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(724, 44);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(105, 32);
		this.btnReporte.TabIndex = 22;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = false;
		this.btnReporte.Visible = false;
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.ImageIndex = 0;
		this.btnConsultar.ImageList = this.imageList1;
		this.btnConsultar.Location = new System.Drawing.Point(724, 6);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 32);
		this.btnConsultar.TabIndex = 21;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(180, 36);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(0, 13);
		this.label3.TabIndex = 23;
		this.label3.Visible = false;
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button4.ImageIndex = 3;
		this.button4.ImageList = this.imageList2;
		this.button4.Location = new System.Drawing.Point(378, 366);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(85, 32);
		this.button4.TabIndex = 84;
		this.button4.Text = "Reporte";
		this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Write Document.png");
		this.imageList2.Images.SetKeyName(1, "New Document.png");
		this.imageList2.Images.SetKeyName(2, "Remove Document.png");
		this.imageList2.Images.SetKeyName(3, "document-print.png");
		this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList2.Images.SetKeyName(5, "exit.png");
		this.imageList2.Images.SetKeyName(6, "OK_Verde.png");
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(832, 415);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.btnConsultar);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.txtFiltro);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btGenVenta);
		base.Controls.Add(this.btnIrPedido);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.KeyPreview = true;
		base.Name = "frmEntregasConsultExt";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Entregas  Articulos Vendedores";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPedidosPendientes_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvPedidosPendientes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
