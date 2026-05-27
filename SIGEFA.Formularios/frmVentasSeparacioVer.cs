using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmVentasSeparacioVer : OfficeForm
{
	private clsAdmSeparacion admVentas = new clsAdmSeparacion();

	private clsSeparacion sepa = new clsSeparacion();

	private clsCuotasSeparacion cuotas = new clsCuotasSeparacion();

	private clsAdmCuotaSeparacion admCuota = new clsAdmCuotaSeparacion();

	private clsReporteVentSeparacion ds = new clsReporteVentSeparacion();

	private IContainer components = null;

	private Button button1;

	private Button btnReporte;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnAnular;

	private Button btnIrPedido;

	private Button btnSalir;

	private Button btnVistaSucursales;

	private Button button2;

	private Button button3;

	private Label label1;

	private Label label2;

	private DateTimePicker dateTimePicker1;

	private DateTimePicker dateTimePicker2;

	private Button button4;

	private Button button5;

	private Button button6;

	private GroupBox groupBox1;

	private DataGridView dgvVentasSeparacion;

	private ImageList imageList2;

	private ImageList imageList1;

	private ComboBox cmbEstado;

	private Label label3;

	private DataGridViewTextBoxColumn codigoFactura;

	private DataGridViewTextBoxColumn Fecha;

	private DataGridViewTextBoxColumn codCliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn pendiente;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn saldopen;

	private DataGridViewTextBoxColumn saldocan;

	private DataGridViewLinkColumn accion;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	public frmVentasSeparacioVer()
	{
		InitializeComponent();
	}

	private void frmVentasSeparacioVer_Load(object sender, EventArgs e)
	{
		dateTimePicker1.Value = dateTimePicker2.Value.AddDays(-90.0);
		cmbEstado.SelectedIndex = 0;
		CargarLista();
	}

	private void CargarLista()
	{
		try
		{
			dgvVentasSeparacion.DataSource = admVentas.CargarVentas(frmLogin.iCodAlmacen, dateTimePicker1.Value, dateTimePicker2.Value, Convert.ToInt32(cmbEstado.SelectedIndex));
			dgvVentasSeparacion.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		CargarLista();
	}

	private void dgvVentasSeparacion_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvVentasSeparacion.Rows.Count < 1 || e.RowIndex == -1 || e.ColumnIndex == -1)
		{
			return;
		}
		DataGridViewCell celda = dgvVentasSeparacion.CurrentCell;
		if (celda.Value.ToString() == "ABONAR")
		{
			frmPargarSeparacion form = new frmPargarSeparacion();
			int id = Convert.ToInt32(dgvVentasSeparacion.Rows[e.RowIndex].Cells[codigoFactura.Name].Value.ToString());
			form.codSeparacion = id;
			form.Proceso = 1;
			form.ShowDialog();
		}
		else if (celda.Value.ToString() == "GENERAR VENTA")
		{
			if (Application.OpenForms["frmVenta"] != null)
			{
				Application.OpenForms["frmVenta"].Close();
				return;
			}
			frmVenta form2 = new frmVenta();
			form2.MdiParent = base.MdiParent;
			form2.CodSeparacion = Convert.ToInt32(dgvVentasSeparacion.CurrentRow.Cells[codigoFactura.Name].Value.ToString());
			form2.label38.Text = "Separacion";
			form2.Proceso = 5;
			form2.Show();
		}
	}

	private void button6_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button4_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvVentasSeparacion.Rows.Count < 1 || dgvVentasSeparacion.CurrentRow == null)
			{
				return;
			}
			DataGridViewRow row = dgvVentasSeparacion.CurrentRow;
			if (!(btnAnular.Text == "Anular") || dgvVentasSeparacion.Rows.Count < 1 || dgvVentasSeparacion.CurrentRow.Index == -1)
			{
				return;
			}
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No)
			{
				sepa = admVentas.BuscarSeparacion(sepa.CodSeparacion, frmLogin.iCodAlmacen);
				cuotas = admCuota.BuscarCuotasSeparacion(sepa.CodSeparacion, frmLogin.iCodAlmacen);
				if (admVentas.anular(Convert.ToInt32(sepa.CodSeparacion)))
				{
					MessageBox.Show("El documento ha sido anulado correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("No se puede Anular Ventas ", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvVentasSeparacion_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvVentasSeparacion.Rows.Count >= 1 && e.RowIndex != -1)
		{
			if (dgvVentasSeparacion.Rows[e.RowIndex].Cells[estado.Name].Value.ToString() == "ACTIVO")
			{
				btnAnular.Text = "Anular";
				btnAnular.Enabled = true;
				btnAnular.ImageIndex = 4;
			}
			else
			{
				btnAnular.Text = "Activar";
				btnAnular.Enabled = true;
				btnAnular.ImageIndex = 6;
			}
		}
	}

	private void dgvVentasSeparacion_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvVentasSeparacion.Rows.Count >= 1 && e.Row.Selected)
		{
			sepa.CodSeparacion = Convert.ToInt32(e.Row.Cells[codigoFactura.Name].Value.ToString());
		}
	}

	private void cmbEstado_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargarLista();
	}

	private void frmVentasSeparacioVer_Shown(object sender, EventArgs e)
	{
		CargarLista();
	}

	private void button5_Click(object sender, EventArgs e)
	{
		if (dgvVentasSeparacion.Rows.Count >= 1 && dgvVentasSeparacion.CurrentRow != null)
		{
			DataGridViewRow row = dgvVentasSeparacion.CurrentRow;
			if (dgvVentasSeparacion.Rows.Count >= 1)
			{
				frmVenta form = new frmVenta();
				form.MdiParent = base.MdiParent;
				form.CodVenta = sepa.CodSeparacion.ToString();
				form.Proceso = 3;
				form.Show();
			}
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		try
		{
			CRListaVentasSeparacion rpt = new CRListaVentasSeparacion();
			frmListaVentasSeparacion frm = new frmListaVentasSeparacion();
			rpt.SetDataSource(ds.ReporteSeparacion(frmLogin.iCodAlmacen, dateTimePicker1.Value, dateTimePicker2.Value, Convert.ToInt32(cmbEstado.SelectedIndex)).Tables[0]);
			frm.crvListaGuias.ReportSource = rpt;
			frm.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVentasSeparacioVer));
		this.button1 = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnIrPedido = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnVistaSucursales = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.button2 = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button3 = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
		this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
		this.button4 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.button6 = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVentasSeparacion = new System.Windows.Forms.DataGridView();
		this.codigoFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.saldopen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.saldocan = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.accion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.cmbEstado = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentasSeparacion).BeginInit();
		base.SuspendLayout();
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button1.ImageIndex = 8;
		this.button1.Location = new System.Drawing.Point(1316, 570);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(93, 37);
		this.button1.TabIndex = 44;
		this.button1.Text = "Actualizar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.Location = new System.Drawing.Point(1479, 570);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(90, 37);
		this.btnReporte.TabIndex = 43;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(1152, 579);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 42;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(979, 579);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 41;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(1032, 576);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 40;
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(1199, 576);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 39;
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.Location = new System.Drawing.Point(568, 570);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 38;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnIrPedido.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrPedido.ImageIndex = 1;
		this.btnIrPedido.Location = new System.Drawing.Point(1575, 570);
		this.btnIrPedido.Name = "btnIrPedido";
		this.btnIrPedido.Size = new System.Drawing.Size(93, 37);
		this.btnIrPedido.TabIndex = 37;
		this.btnIrPedido.Text = "Consulta";
		this.btnIrPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrPedido.UseVisualStyleBackColor = true;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.Location = new System.Drawing.Point(1674, 570);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 36;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnVistaSucursales.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnVistaSucursales.ImageIndex = 19;
		this.btnVistaSucursales.ImageList = this.imageList2;
		this.btnVistaSucursales.Location = new System.Drawing.Point(123, 468);
		this.btnVistaSucursales.Name = "btnVistaSucursales";
		this.btnVistaSucursales.Size = new System.Drawing.Size(92, 37);
		this.btnVistaSucursales.TabIndex = 55;
		this.btnVistaSucursales.Text = "Activar Vista";
		this.btnVistaSucursales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVistaSucursales.UseVisualStyleBackColor = true;
		this.btnVistaSucursales.Visible = false;
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
		this.imageList2.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList2.Images.SetKeyName(19, "img_visto.jpg");
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button2.ImageIndex = 9;
		this.button2.ImageList = this.imageList2;
		this.button2.Location = new System.Drawing.Point(825, 468);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(93, 37);
		this.button2.TabIndex = 54;
		this.button2.Text = "Actualizar";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "document_print.png");
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button3.ImageIndex = 7;
		this.button3.ImageList = this.imageList1;
		this.button3.Location = new System.Drawing.Point(988, 468);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(90, 37);
		this.button3.TabIndex = 53;
		this.button3.Text = "Reporte";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(661, 477);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(41, 13);
		this.label1.TabIndex = 52;
		this.label1.Text = "Hasta :";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(488, 477);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(44, 13);
		this.label2.TabIndex = 51;
		this.label2.Text = "Desde :";
		this.dateTimePicker1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dateTimePicker1.Location = new System.Drawing.Point(541, 474);
		this.dateTimePicker1.Name = "dateTimePicker1";
		this.dateTimePicker1.Size = new System.Drawing.Size(100, 20);
		this.dateTimePicker1.TabIndex = 50;
		this.dateTimePicker2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dateTimePicker2.Location = new System.Drawing.Point(708, 474);
		this.dateTimePicker2.Name = "dateTimePicker2";
		this.dateTimePicker2.Size = new System.Drawing.Size(100, 20);
		this.dateTimePicker2.TabIndex = 49;
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button4.ImageIndex = 4;
		this.button4.ImageList = this.imageList1;
		this.button4.Location = new System.Drawing.Point(1, 468);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(96, 37);
		this.button4.TabIndex = 48;
		this.button4.Text = "Anular";
		this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button5.ImageIndex = 1;
		this.button5.ImageList = this.imageList1;
		this.button5.Location = new System.Drawing.Point(1084, 468);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(93, 37);
		this.button5.TabIndex = 47;
		this.button5.Text = "Consulta";
		this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Visible = false;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.button6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button6.ImageIndex = 0;
		this.button6.ImageList = this.imageList1;
		this.button6.Location = new System.Drawing.Point(1183, 468);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(75, 37);
		this.button6.TabIndex = 46;
		this.button6.Text = "Salir";
		this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvVentasSeparacion);
		this.groupBox1.Location = new System.Drawing.Point(-5, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1275, 461);
		this.groupBox1.TabIndex = 45;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Ventas";
		this.dgvVentasSeparacion.AllowUserToAddRows = false;
		this.dgvVentasSeparacion.AllowUserToDeleteRows = false;
		this.dgvVentasSeparacion.AllowUserToOrderColumns = true;
		this.dgvVentasSeparacion.AllowUserToResizeRows = false;
		this.dgvVentasSeparacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvVentasSeparacion.Columns.AddRange(this.codigoFactura, this.Fecha, this.codCliente, this.cliente, this.estado, this.pendiente, this.total, this.saldopen, this.saldocan, this.accion);
		this.dgvVentasSeparacion.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentasSeparacion.Location = new System.Drawing.Point(3, 16);
		this.dgvVentasSeparacion.MultiSelect = false;
		this.dgvVentasSeparacion.Name = "dgvVentasSeparacion";
		this.dgvVentasSeparacion.ReadOnly = true;
		this.dgvVentasSeparacion.RowHeadersVisible = false;
		this.dgvVentasSeparacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentasSeparacion.Size = new System.Drawing.Size(1269, 442);
		this.dgvVentasSeparacion.TabIndex = 0;
		this.dgvVentasSeparacion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVentasSeparacion_CellClick);
		this.dgvVentasSeparacion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVentasSeparacion_CellContentClick);
		this.dgvVentasSeparacion.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvVentasSeparacion_RowStateChanged);
		this.codigoFactura.DataPropertyName = "codFactura";
		this.codigoFactura.HeaderText = "Codigo Venta";
		this.codigoFactura.Name = "codigoFactura";
		this.codigoFactura.ReadOnly = true;
		this.Fecha.DataPropertyName = "fecha";
		this.Fecha.HeaderText = "Fecha Registro";
		this.Fecha.Name = "Fecha";
		this.Fecha.ReadOnly = true;
		this.codCliente.DataPropertyName = "codcliente";
		this.codCliente.HeaderText = "Codigo Cliente";
		this.codCliente.Name = "codCliente";
		this.codCliente.ReadOnly = true;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Width = 300;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.pendiente.DataPropertyName = "pendiente";
		this.pendiente.HeaderText = "Situacion";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.total.DataPropertyName = "total";
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.saldopen.DataPropertyName = "salpen";
		this.saldopen.HeaderText = "Pendiente";
		this.saldopen.Name = "saldopen";
		this.saldopen.ReadOnly = true;
		this.saldocan.DataPropertyName = "salcan";
		this.saldocan.HeaderText = "Cancelado";
		this.saldocan.Name = "saldocan";
		this.saldocan.ReadOnly = true;
		this.accion.DataPropertyName = "accion";
		this.accion.HeaderText = "accion";
		this.accion.Name = "accion";
		this.accion.ReadOnly = true;
		this.accion.Width = 200;
		this.cmbEstado.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbEstado.FormattingEnabled = true;
		this.cmbEstado.Items.AddRange(new object[2] { "CANCELADO", "PENDIENTE" });
		this.cmbEstado.Location = new System.Drawing.Point(344, 473);
		this.cmbEstado.Name = "cmbEstado";
		this.cmbEstado.Size = new System.Drawing.Size(121, 21);
		this.cmbEstado.TabIndex = 56;
		this.cmbEstado.SelectionChangeCommitted += new System.EventHandler(cmbEstado_SelectionChangeCommitted);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(255, 481);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(83, 13);
		this.label3.TabIndex = 57;
		this.label3.Text = "Estado de Pago";
		this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
		base.ClientSize = new System.Drawing.Size(1265, 506);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.cmbEstado);
		base.Controls.Add(this.btnVistaSucursales);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.dateTimePicker1);
		base.Controls.Add(this.dateTimePicker2);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button5);
		base.Controls.Add(this.button6);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.dtpDesde);
		base.Controls.Add(this.dtpHasta);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btnIrPedido);
		base.Controls.Add(this.btnSalir);
		this.DoubleBuffered = true;
		base.Name = "frmVentasSeparacioVer";
		base.ShowInTaskbar = false;
		this.Text = "frmVentasSeparacioVer";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmVentasSeparacioVer_Load);
		base.Shown += new System.EventHandler(frmVentasSeparacioVer_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVentasSeparacion).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
