using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmNotasCreditoCompras : Office2007Form
{
	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaIngreso nota = new clsNotaIngreso();

	private clsNotaSalida notaS = new clsNotaSalida();

	private clsAdmNotaCreditoCompra admncc = new clsAdmNotaCreditoCompra();

	private string codigoNotaCredito = "";

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private int codnotaI = 0;

	private IContainer components = null;

	private GroupBox groupbox;

	private Button btnSalir;

	private Button btnIrGuia;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private Label label3;

	private Label label4;

	private Label label2;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Button btnBuscar;

	private GroupBox groupBox2;

	private ComboBox cmbAlmacenes;

	private Label label8;

	private RadGridView rgvlistado;

	private GroupBox groupBox3;

	private RadioButton rbFechaRegistro;

	private RadioButton rbFechaEmision;

	private Label txtNombreProducto;

	private Label label9;

	private Button btnBuscarProducto;

	private Label label10;

	private TextBox txtCodprod;

	public frmNotasCreditoCompras()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CargaLista()
	{
		rgvlistado.DataSource = data;
		int tipoFecha = (rbFechaEmision.Checked ? 1 : (rbFechaRegistro.Checked ? 2 : 0));
		data.DataSource = admncc.ListadoEstandarNotaCreditoCompra(Convert.ToInt32(cmbAlmacenes.SelectedValue), frmLogin.iCodSucursal, tipoFecha, dtpDesde.Value.Date, dtpHasta.Value.Date, Convert.ToInt32((txtCodprod.Text == "") ? "0" : txtCodprod.Text));
		data.Filter = string.Empty;
		filtro = string.Empty;
		rgvlistado.ClearSelection();
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (rgvlistado.Rows.Count >= 1 && rgvlistado.CurrentRow != null)
		{
			GridViewRowInfo row = rgvlistado.CurrentRow;
			frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
			form.MdiParent = base.MdiParent;
			form.CodNotaCC = Convert.ToInt32(codigoNotaCredito);
			form.Proceso = 2;
			form.Show();
		}
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
		if (rgvlistado.CurrentRow != null && codigoNotaCredito != "")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la nota seleccionada", "Notas de Credito", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmNotaS.anular(Convert.ToInt32(codigoNotaCredito)))
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
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
	}

	private void dgvNotasCredito_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
	}

	private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged_1(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvNotasCredito_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void frmNotasCreditoCompras_Load(object sender, EventArgs e)
	{
		try
		{
			cargaAlmacenes();
			CargaLista();
		}
		catch (Exception)
		{
		}
	}

	private void frmNotasCreditoCompras_Shown(object sender, EventArgs e)
	{
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			rgvlistado.Focus();
		}
	}

	private void dgvNotasCredito_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void rgvlistado_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvlistado.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
			form.MdiParent = base.MdiParent;
			form.CodNotaCC = Convert.ToInt32(codigoNotaCredito);
			form.Proceso = 2;
			form.Show();
		}
	}

	private void rgvlistado_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvlistado.Rows.Count >= 1 && e.RowIndex != -1)
		{
			codigoNotaCredito = rgvlistado.Rows[e.RowIndex].Cells["colCodigoNCC"].Value.ToString();
		}
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaLista();
		}
		else
		{
			MessageBox.Show("Seleccione un producto. En la casilla de producto aprete la tecla F1", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotasCreditoCompras));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupbox = new System.Windows.Forms.GroupBox();
		this.rbFechaRegistro = new System.Windows.Forms.RadioButton();
		this.rbFechaEmision = new System.Windows.Forms.RadioButton();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label10 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.cmbAlmacenes = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.btnBuscar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnIrGuia = new System.Windows.Forms.Button();
		this.rgvlistado = new Telerik.WinControls.UI.RadGridView();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupbox.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvlistado).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvlistado.MasterTemplate).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupbox.Controls.Add(this.rbFechaRegistro);
		this.groupbox.Controls.Add(this.rbFechaEmision);
		this.groupbox.Controls.Add(this.txtNombreProducto);
		this.groupbox.Controls.Add(this.label9);
		this.groupbox.Controls.Add(this.btnBuscarProducto);
		this.groupbox.Controls.Add(this.label10);
		this.groupbox.Controls.Add(this.txtCodprod);
		this.groupbox.Controls.Add(this.cmbAlmacenes);
		this.groupbox.Controls.Add(this.label8);
		this.groupbox.Controls.Add(this.btnBuscar);
		this.groupbox.Controls.Add(this.dtpHasta);
		this.groupbox.Controls.Add(this.dtpDesde);
		this.groupbox.Controls.Add(this.label2);
		this.groupbox.Controls.Add(this.label4);
		this.groupbox.Controls.Add(this.label3);
		this.groupbox.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupbox.Location = new System.Drawing.Point(0, 0);
		this.groupbox.Name = "groupbox";
		this.groupbox.Size = new System.Drawing.Size(1222, 108);
		this.groupbox.TabIndex = 0;
		this.groupbox.TabStop = false;
		this.groupbox.Text = "Notas de Credito de Compra";
		this.rbFechaRegistro.AutoSize = true;
		this.rbFechaRegistro.Location = new System.Drawing.Point(124, 28);
		this.rbFechaRegistro.Name = "rbFechaRegistro";
		this.rbFechaRegistro.Size = new System.Drawing.Size(97, 17);
		this.rbFechaRegistro.TabIndex = 71;
		this.rbFechaRegistro.Text = "Fecha Registro";
		this.rbFechaRegistro.UseVisualStyleBackColor = true;
		this.rbFechaEmision.AutoSize = true;
		this.rbFechaEmision.Checked = true;
		this.rbFechaEmision.Location = new System.Drawing.Point(18, 28);
		this.rbFechaEmision.Name = "rbFechaEmision";
		this.rbFechaEmision.Size = new System.Drawing.Size(94, 17);
		this.rbFechaEmision.TabIndex = 70;
		this.rbFechaEmision.TabStop = true;
		this.rbFechaEmision.Text = "Fecha Emision";
		this.rbFechaEmision.UseVisualStyleBackColor = true;
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(465, 46);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 69;
		this.txtNombreProducto.Text = "---";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(424, 46);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(35, 13);
		this.label9.TabIndex = 68;
		this.label9.Text = "Prod.:";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(384, 36);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 67;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(237, 30);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(112, 13);
		this.label10.TabIndex = 66;
		this.label10.Text = "Busqueda x Producto:";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(240, 46);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 65;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.cmbAlmacenes.FormattingEnabled = true;
		this.cmbAlmacenes.Location = new System.Drawing.Point(1042, 30);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(129, 21);
		this.cmbAlmacenes.TabIndex = 62;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(985, 33);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(51, 13);
		this.label8.TabIndex = 61;
		this.label8.Text = "Almacen:";
		this.btnBuscar.ImageIndex = 6;
		this.btnBuscar.ImageList = this.imageList1;
		this.btnBuscar.Location = new System.Drawing.Point(1042, 59);
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.Size = new System.Drawing.Size(88, 33);
		this.btnBuscar.TabIndex = 60;
		this.btnBuscar.Text = "Buscar";
		this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscar.UseVisualStyleBackColor = true;
		this.btnBuscar.Click += new System.EventHandler(btnBuscar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList1.Images.SetKeyName(6, "cambio.png");
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(124, 63);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(81, 20);
		this.dtpHasta.TabIndex = 58;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged_1);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(18, 63);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(81, 20);
		this.dtpDesde.TabIndex = 57;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged_1);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(16, 48);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 12);
		this.label2.TabIndex = 55;
		this.label2.Text = "Desde";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(122, 48);
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
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btGenVenta);
		this.groupBox2.Controls.Add(this.btnIrGuia);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 539);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1222, 56);
		this.groupBox2.TabIndex = 53;
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
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1141, 13);
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
		this.btGenVenta.Location = new System.Drawing.Point(940, 13);
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
		this.btnIrGuia.Location = new System.Drawing.Point(1042, 13);
		this.btnIrGuia.Name = "btnIrGuia";
		this.btnIrGuia.Size = new System.Drawing.Size(93, 37);
		this.btnIrGuia.TabIndex = 2;
		this.btnIrGuia.Text = "Consultar";
		this.btnIrGuia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrGuia.UseVisualStyleBackColor = true;
		this.btnIrGuia.Click += new System.EventHandler(btnIrPedido_Click);
		this.rgvlistado.AutoScroll = true;
		this.rgvlistado.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvlistado.Location = new System.Drawing.Point(3, 16);
		this.rgvlistado.MasterTemplate.AllowAddNewRow = false;
		this.rgvlistado.MasterTemplate.AllowColumnReorder = false;
		this.rgvlistado.MasterTemplate.AutoGenerateColumns = false;
		this.rgvlistado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "fechaemision";
		gridViewTextBoxColumn1.HeaderText = "Fecha Emision";
		gridViewTextBoxColumn1.Name = "colFechaEmision";
		gridViewTextBoxColumn1.Width = 88;
		gridViewTextBoxColumn2.FieldName = "numdoc";
		gridViewTextBoxColumn2.HeaderText = "Num. Doc.";
		gridViewTextBoxColumn2.Name = "colNumDoc";
		gridViewTextBoxColumn2.Width = 88;
		gridViewTextBoxColumn3.FieldName = "fecharegistro";
		gridViewTextBoxColumn3.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn3.Name = "colFechaRegistro";
		gridViewTextBoxColumn3.Width = 88;
		gridViewTextBoxColumn4.FieldName = "codNotaSalida";
		gridViewTextBoxColumn4.HeaderText = "codNotaSalida";
		gridViewTextBoxColumn4.IsVisible = false;
		gridViewTextBoxColumn4.Name = "colCodNotaSalida";
		gridViewTextBoxColumn5.FieldName = "numdocgrsp";
		gridViewTextBoxColumn5.HeaderText = "Guia Remision S.P.";
		gridViewTextBoxColumn5.Name = "colGRSP";
		gridViewTextBoxColumn5.Width = 88;
		gridViewTextBoxColumn6.FieldName = "codProveedor";
		gridViewTextBoxColumn6.HeaderText = "codProveedor";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "colCodProveedor";
		gridViewTextBoxColumn7.FieldName = "razonsocial";
		gridViewTextBoxColumn7.HeaderText = "Proveedor";
		gridViewTextBoxColumn7.Name = "colRazonSocial";
		gridViewTextBoxColumn7.Width = 88;
		gridViewTextBoxColumn8.FieldName = "total";
		gridViewTextBoxColumn8.HeaderText = "Total";
		gridViewTextBoxColumn8.Name = "colTotal";
		gridViewTextBoxColumn8.Width = 88;
		gridViewTextBoxColumn9.FieldName = "ctdadDiasEspera";
		gridViewTextBoxColumn9.HeaderText = "Dias Espera N.C.";
		gridViewTextBoxColumn9.Name = "colDiasEsperaNC";
		gridViewTextBoxColumn9.Width = 88;
		gridViewTextBoxColumn10.FieldName = "docRef";
		gridViewTextBoxColumn10.HeaderText = "Doc. Ref.";
		gridViewTextBoxColumn10.Name = "colDocRef";
		gridViewTextBoxColumn10.Width = 88;
		gridViewTextBoxColumn11.FieldName = "codDocRef";
		gridViewTextBoxColumn11.HeaderText = "codDocRef";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "colCodDocRef";
		gridViewTextBoxColumn12.FieldName = "estado";
		gridViewTextBoxColumn12.HeaderText = "Estado";
		gridViewTextBoxColumn12.Name = "colEstado";
		gridViewTextBoxColumn12.Width = 88;
		gridViewTextBoxColumn13.FieldName = "creador";
		gridViewTextBoxColumn13.HeaderText = "Creador";
		gridViewTextBoxColumn13.Name = "colCreador";
		gridViewTextBoxColumn13.Width = 88;
		gridViewTextBoxColumn14.FieldName = "codNotaCredito";
		gridViewTextBoxColumn14.HeaderText = "codNotaCreditoCompra";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "colCodigoNCC";
		gridViewTextBoxColumn14.Width = 44;
		gridViewTextBoxColumn15.FieldName = "asignador";
		gridViewTextBoxColumn15.HeaderText = "Asignador";
		gridViewTextBoxColumn15.Name = "colAsignador";
		gridViewTextBoxColumn15.Width = 88;
		gridViewTextBoxColumn16.FieldName = "fechaAsignacion";
		gridViewTextBoxColumn16.HeaderText = "Fecha Asignacion";
		gridViewTextBoxColumn16.Name = "colFechaAsignacion";
		gridViewTextBoxColumn16.Width = 88;
		gridViewTextBoxColumn17.FieldName = "formaPago";
		gridViewTextBoxColumn17.HeaderText = "Forma Pago";
		gridViewTextBoxColumn17.Name = "colFormaPago";
		gridViewTextBoxColumn17.Width = 88;
		gridViewTextBoxColumn18.FieldName = "comentario";
		gridViewTextBoxColumn18.HeaderText = "Comentario";
		gridViewTextBoxColumn18.Name = "colComentario";
		gridViewTextBoxColumn18.Width = 84;
		this.rgvlistado.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18);
		this.rgvlistado.MasterTemplate.EnableFiltering = true;
		this.rgvlistado.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvlistado.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvlistado.Name = "rgvlistado";
		this.rgvlistado.ReadOnly = true;
		this.rgvlistado.ShowGroupPanel = false;
		this.rgvlistado.ShowGroupPanelScrollbars = false;
		this.rgvlistado.Size = new System.Drawing.Size(1216, 413);
		this.rgvlistado.TabIndex = 55;
		this.rgvlistado.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvlistado_CellClick);
		this.rgvlistado.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvlistado_CellDoubleClick);
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.rgvlistado);
		this.groupBox3.Location = new System.Drawing.Point(0, 114);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1222, 432);
		this.groupBox3.TabIndex = 56;
		this.groupBox3.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1222, 595);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupbox);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmNotasCreditoCompras";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notas de Credito de Compra";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotasCreditoCompras_Load);
		base.Shown += new System.EventHandler(frmNotasCreditoCompras_Shown);
		this.groupbox.ResumeLayout(false);
		this.groupbox.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvlistado.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvlistado).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
