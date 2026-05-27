using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmArqueoFondoFijo : Office2007Form
{
	private clsAdmArqueoFondoFijo admarqueo = new clsAdmArqueoFondoFijo();

	private clsArqueoFondoFijo arqueo = new clsArqueoFondoFijo();

	private List<clsDetalleArqueFondoFijo> detalle = new List<clsDetalleArqueFondoFijo>();

	public decimal billetes = default(decimal);

	public decimal monedas = default(decimal);

	public decimal monto = default(decimal);

	public decimal montoacumulado = default(decimal);

	public decimal totalbilletes = default(decimal);

	public decimal totalmonedas = default(decimal);

	public int Proceso = 0;

	public int CodArqueo = 0;

	public int tipodinero = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private SplitContainer splitContainer1;

	private GroupBox groupBox2;

	private Label label1;

	private GroupBox groupBox3;

	private Label label2;

	private ComboBox cmbBilletes;

	private Button button1;

	private DataGridView dgvBilletes;

	private Label label3;

	private ComboBox cmbMonedas;

	private Button button2;

	private DataGridView dgvMonedas;

	private TextBox txtCantidadBilletes;

	private TextBox txtCantidadMonedas;

	private TextBox txtTotal;

	private Label label5;

	private TextBox txtmontoaevaluar;

	private Label label4;

	private Button btnSalir;

	private Button btnImprimir;

	private Button btnGuardar;

	private GroupBox groupBox4;

	private TextBox txthorafin;

	private Label label8;

	private TextBox txthorainicio;

	private Label label7;

	private TextBox txtnombre;

	private Label label6;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn denominacion;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn codigo1;

	private DataGridViewTextBoxColumn cantidad1;

	private DataGridViewTextBoxColumn denominacion1;

	private DataGridViewTextBoxColumn importe1;

	private TextBox txtTotalBilletes;

	private Label label9;

	private TextBox txtTotalMonedas;

	private Label label10;

	private ImageList imageList2;

	private ImageList imageList1;

	private Label label11;

	private TextBox txtcodarqueo;

	public frmArqueoFondoFijo()
	{
		InitializeComponent();
	}

	private void frmArqueoFondoFijo_Load(object sender, EventArgs e)
	{
		CargaBilletes(1);
		CargaMonedas(2);
		if (Proceso == 1)
		{
			txtmontoaevaluar.Text = $"{monto:#,##0.00}";
		}
	}

	private void CargaBilletes(int tipo)
	{
		try
		{
			cmbBilletes.DataSource = admarqueo.ListaDinero(tipo);
			cmbBilletes.DisplayMember = "denominacion";
			cmbBilletes.ValueMember = "coddinero";
			cmbBilletes.SelectedIndex = 0;
			cmbBilletes_SelectionChangeCommitted(new object(), new EventArgs());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void CargaMonedas(int tipo)
	{
		try
		{
			cmbMonedas.DataSource = admarqueo.ListaDinero(tipo);
			cmbMonedas.DisplayMember = "denominacion";
			cmbMonedas.ValueMember = "coddinero";
			cmbMonedas.SelectedIndex = 0;
			cmbMonedas_SelectionChangeCommitted(new object(), new EventArgs());
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void cmbBilletes_SelectionChangeCommitted(object sender, EventArgs e)
	{
		billetes = admarqueo.TraeValor(Convert.ToInt32(cmbBilletes.SelectedValue));
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (!(txtCantidadBilletes.Text != ""))
		{
			return;
		}
		if (Convert.ToDecimal(txtCantidadBilletes.Text) > 0m)
		{
			if (montoacumulado + Convert.ToDecimal(txtCantidadBilletes.Text) * billetes <= monto)
			{
				dgvBilletes.Rows.Add(Convert.ToInt32(cmbBilletes.SelectedValue), txtCantidadBilletes.Text, cmbBilletes.Text, Convert.ToDecimal(txtCantidadBilletes.Text) * billetes);
				montoacumulado += Convert.ToDecimal(txtCantidadBilletes.Text) * billetes;
				totalbilletes += Convert.ToDecimal(txtCantidadBilletes.Text) * billetes;
				txtCantidadBilletes.Text = "0";
				txtTotalBilletes.Text = $"{totalbilletes:#,##0.00}";
			}
			else
			{
				MessageBox.Show("el monto sobrepasa el monto a evaluar");
			}
		}
		txtTotal.Text = $"{montoacumulado:#,##0.00}";
	}

	private void cmbMonedas_SelectionChangeCommitted(object sender, EventArgs e)
	{
		monedas = admarqueo.TraeValor(Convert.ToInt32(cmbMonedas.SelectedValue));
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (!(txtCantidadMonedas.Text != ""))
		{
			return;
		}
		if (Convert.ToDecimal(txtCantidadMonedas.Text) > 0m)
		{
			if (montoacumulado + Convert.ToDecimal(txtCantidadMonedas.Text) * monedas <= monto)
			{
				dgvMonedas.Rows.Add(Convert.ToInt32(cmbMonedas.SelectedValue), txtCantidadMonedas.Text, cmbMonedas.Text, Convert.ToDecimal(txtCantidadMonedas.Text) * monedas);
				montoacumulado += Convert.ToDecimal(txtCantidadMonedas.Text) * monedas;
				totalmonedas += Convert.ToDecimal(txtCantidadMonedas.Text) * monedas;
				txtCantidadMonedas.Text = "0";
				txtTotalMonedas.Text = $"{totalmonedas:#,##0.00}";
			}
			else
			{
				MessageBox.Show("el monto sobrepasa el monto a evaluar");
			}
		}
		txtTotal.Text = $"{montoacumulado:#,##0.00}";
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		int contador = 0;
		arqueo.Encargado = txtnombre.Text;
		arqueo.Horainicio = txthorainicio.Text;
		arqueo.Horafin = txthorafin.Text;
		arqueo.Total = Convert.ToDecimal(txtTotal.Text);
		arqueo.Coduser = frmLogin.iCodUser;
		arqueo.Codsucursa = frmLogin.iCodSucursal;
		if (Proceso != 1 || !admarqueo.insert(arqueo))
		{
			return;
		}
		CodArqueo = arqueo.CodarqueofondodijoNuevo;
		recorrebilletes();
		recorremonedas();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleArqueFondoFijo det in detalle)
		{
			if (admarqueo.insertDetalle(det))
			{
				contador++;
			}
		}
		if (detalle.Count == contador)
		{
			MessageBox.Show("El Arqueo se Guardo Correctamente");
			sololectura();
			btnImprimir.Visible = true;
			btnImprimir.Enabled = true;
			txtcodarqueo.Visible = true;
			txtcodarqueo.Text = CodArqueo.ToString().PadLeft(5, '0');
			txtcodarqueo.ReadOnly = true;
			label11.Visible = true;
		}
	}

	private void sololectura()
	{
		txtnombre.ReadOnly = true;
		txthorainicio.ReadOnly = true;
		txthorafin.ReadOnly = true;
		txtCantidadBilletes.ReadOnly = true;
		txtCantidadMonedas.ReadOnly = true;
		txtTotalBilletes.ReadOnly = true;
		txtTotalMonedas.ReadOnly = true;
		txtTotal.ReadOnly = true;
		button1.Enabled = false;
		button2.Enabled = false;
		dgvBilletes.ReadOnly = true;
		dgvMonedas.ReadOnly = true;
		cmbBilletes.Enabled = false;
		cmbMonedas.Enabled = false;
	}

	private void recorremonedas()
	{
		if (dgvMonedas.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvMonedas.Rows)
		{
			añadedetallemod(row);
		}
	}

	private void añadedetallemod(DataGridViewRow fila)
	{
		clsDetalleArqueFondoFijo detallearqueo = new clsDetalleArqueFondoFijo();
		detallearqueo.Codarqueofondofijo = CodArqueo;
		detallearqueo.Coddinero = Convert.ToInt32(fila.Cells[codigo1.Name].Value);
		detallearqueo.Cantidad = Convert.ToInt32(fila.Cells[cantidad1.Name].Value);
		detallearqueo.Importe = Convert.ToDecimal(fila.Cells[importe1.Name].Value);
		detallearqueo.Coduser = frmLogin.iCodUser;
		detalle.Add(detallearqueo);
	}

	private void recorrebilletes()
	{
		if (dgvBilletes.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvBilletes.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleArqueFondoFijo detallearqueo = new clsDetalleArqueFondoFijo();
		detallearqueo.Codarqueofondofijo = CodArqueo;
		detallearqueo.Coddinero = Convert.ToInt32(fila.Cells[codigo.Name].Value);
		detallearqueo.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
		detallearqueo.Importe = Convert.ToDecimal(fila.Cells[importe.Name].Value);
		detallearqueo.Coduser = frmLogin.iCodUser;
		detalle.Add(detallearqueo);
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		clsReporteCaja dso = new clsReporteCaja();
		CRReporteArqueoFondoFijo rpt = new CRReporteArqueoFondoFijo();
		frmReporteArqueoFondoFijoRPT frm = new frmReporteArqueoFondoFijoRPT();
		rpt.SetDataSource(dso.ReporteArqueoFondoFijo(CodArqueo));
		frm.crvArqueoFondoFijo.ReportSource = rpt;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmArqueoFondoFijo));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.txtCantidadBilletes = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbBilletes = new System.Windows.Forms.ComboBox();
		this.button1 = new System.Windows.Forms.Button();
		this.dgvBilletes = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.denominacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtCantidadMonedas = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbMonedas = new System.Windows.Forms.ComboBox();
		this.button2 = new System.Windows.Forms.Button();
		this.dgvMonedas = new System.Windows.Forms.DataGridView();
		this.codigo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.denominacion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.txtTotal = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtmontoaevaluar = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txthorafin = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txthorainicio = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtnombre = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtTotalBilletes = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtTotalMonedas = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.txtcodarqueo = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvBilletes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvMonedas).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.splitContainer1);
		this.groupBox1.Location = new System.Drawing.Point(0, 113);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(643, 262);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.Location = new System.Drawing.Point(3, 16);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.txtTotalBilletes);
		this.splitContainer1.Panel1.Controls.Add(this.label9);
		this.splitContainer1.Panel1.Controls.Add(this.txtCantidadBilletes);
		this.splitContainer1.Panel1.Controls.Add(this.label2);
		this.splitContainer1.Panel1.Controls.Add(this.cmbBilletes);
		this.splitContainer1.Panel1.Controls.Add(this.button1);
		this.splitContainer1.Panel1.Controls.Add(this.dgvBilletes);
		this.splitContainer1.Panel2.Controls.Add(this.txtTotalMonedas);
		this.splitContainer1.Panel2.Controls.Add(this.label10);
		this.splitContainer1.Panel2.Controls.Add(this.txtCantidadMonedas);
		this.splitContainer1.Panel2.Controls.Add(this.label3);
		this.splitContainer1.Panel2.Controls.Add(this.cmbMonedas);
		this.splitContainer1.Panel2.Controls.Add(this.button2);
		this.splitContainer1.Panel2.Controls.Add(this.dgvMonedas);
		this.splitContainer1.Size = new System.Drawing.Size(637, 243);
		this.splitContainer1.SplitterDistance = 300;
		this.splitContainer1.TabIndex = 0;
		this.txtCantidadBilletes.Location = new System.Drawing.Point(17, 38);
		this.txtCantidadBilletes.Name = "txtCantidadBilletes";
		this.txtCantidadBilletes.Size = new System.Drawing.Size(62, 20);
		this.txtCantidadBilletes.TabIndex = 16;
		this.txtCantidadBilletes.Text = "0";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(14, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(43, 13);
		this.label2.TabIndex = 0;
		this.label2.Text = "Billetes:";
		this.cmbBilletes.FormattingEnabled = true;
		this.cmbBilletes.Location = new System.Drawing.Point(85, 37);
		this.cmbBilletes.Name = "cmbBilletes";
		this.cmbBilletes.Size = new System.Drawing.Size(121, 21);
		this.cmbBilletes.TabIndex = 11;
		this.cmbBilletes.SelectionChangeCommitted += new System.EventHandler(cmbBilletes_SelectionChangeCommitted);
		this.button1.Location = new System.Drawing.Point(212, 36);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(75, 23);
		this.button1.TabIndex = 0;
		this.button1.Text = "agregar";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.dgvBilletes.AllowUserToAddRows = false;
		this.dgvBilletes.AllowUserToDeleteRows = false;
		this.dgvBilletes.AllowUserToResizeRows = false;
		this.dgvBilletes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvBilletes.Columns.AddRange(this.codigo, this.cantidad, this.denominacion, this.importe);
		this.dgvBilletes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvBilletes.Location = new System.Drawing.Point(17, 74);
		this.dgvBilletes.MultiSelect = false;
		this.dgvBilletes.Name = "dgvBilletes";
		this.dgvBilletes.RowHeadersVisible = false;
		this.dgvBilletes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvBilletes.Size = new System.Drawing.Size(270, 119);
		this.dgvBilletes.TabIndex = 10;
		this.codigo.HeaderText = "codigo";
		this.codigo.Name = "codigo";
		this.codigo.Visible = false;
		this.cantidad.HeaderText = "CANTIDAD";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Width = 70;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.denominacion.DefaultCellStyle = dataGridViewCellStyle1;
		this.denominacion.HeaderText = "DENOMINACION";
		this.denominacion.Name = "denominacion";
		this.denominacion.ReadOnly = true;
		this.importe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.importe.DefaultCellStyle = dataGridViewCellStyle2;
		this.importe.HeaderText = "IMPORTE S/.";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.txtCantidadMonedas.Location = new System.Drawing.Point(15, 38);
		this.txtCantidadMonedas.Name = "txtCantidadMonedas";
		this.txtCantidadMonedas.Size = new System.Drawing.Size(62, 20);
		this.txtCantidadMonedas.TabIndex = 17;
		this.txtCantidadMonedas.Text = "0";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(12, 19);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(54, 13);
		this.label3.TabIndex = 13;
		this.label3.Text = "Monedas:";
		this.cmbMonedas.FormattingEnabled = true;
		this.cmbMonedas.Location = new System.Drawing.Point(104, 38);
		this.cmbMonedas.Name = "cmbMonedas";
		this.cmbMonedas.Size = new System.Drawing.Size(121, 21);
		this.cmbMonedas.TabIndex = 15;
		this.cmbMonedas.SelectionChangeCommitted += new System.EventHandler(cmbMonedas_SelectionChangeCommitted);
		this.button2.Location = new System.Drawing.Point(238, 37);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 23);
		this.button2.TabIndex = 12;
		this.button2.Text = "agregar";
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.dgvMonedas.AllowUserToAddRows = false;
		this.dgvMonedas.AllowUserToDeleteRows = false;
		this.dgvMonedas.AllowUserToResizeRows = false;
		this.dgvMonedas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvMonedas.Columns.AddRange(this.codigo1, this.cantidad1, this.denominacion1, this.importe1);
		this.dgvMonedas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvMonedas.Location = new System.Drawing.Point(15, 75);
		this.dgvMonedas.MultiSelect = false;
		this.dgvMonedas.Name = "dgvMonedas";
		this.dgvMonedas.RowHeadersVisible = false;
		this.dgvMonedas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvMonedas.Size = new System.Drawing.Size(298, 119);
		this.dgvMonedas.TabIndex = 14;
		this.codigo1.HeaderText = "codigo1";
		this.codigo1.Name = "codigo1";
		this.codigo1.Visible = false;
		this.cantidad1.HeaderText = "CANTIDAD";
		this.cantidad1.Name = "cantidad1";
		this.cantidad1.ReadOnly = true;
		this.cantidad1.Width = 70;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.denominacion1.DefaultCellStyle = dataGridViewCellStyle3;
		this.denominacion1.HeaderText = "DENOMINACION";
		this.denominacion1.Name = "denominacion1";
		this.denominacion1.ReadOnly = true;
		this.importe1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.importe1.DefaultCellStyle = dataGridViewCellStyle4;
		this.importe1.HeaderText = "IMPORTE S/.";
		this.importe1.Name = "importe1";
		this.importe1.ReadOnly = true;
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(643, 39);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(60, 9);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(470, 22);
		this.label1.TabIndex = 0;
		this.label1.Text = "ARQUEO DE FONDO FIJO PARA PAGOS EN EFECTIVO";
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.txtTotal);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.txtmontoaevaluar);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 381);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(643, 77);
		this.groupBox3.TabIndex = 2;
		this.groupBox3.TabStop = false;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(281, 25);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(75, 37);
		this.btnImprimir.TabIndex = 23;
		this.btnImprimir.Text = "Imprimir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(462, 25);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(78, 37);
		this.btnGuardar.TabIndex = 22;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(546, 25);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.btnSalir.Size = new System.Drawing.Size(74, 38);
		this.btnSalir.TabIndex = 21;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.txtTotal.Location = new System.Drawing.Point(138, 43);
		this.txtTotal.Name = "txtTotal";
		this.txtTotal.Size = new System.Drawing.Size(93, 20);
		this.txtTotal.TabIndex = 20;
		this.txtTotal.Text = "0";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(135, 25);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(34, 13);
		this.label5.TabIndex = 19;
		this.label5.Text = "Total:";
		this.txtmontoaevaluar.Enabled = false;
		this.txtmontoaevaluar.Location = new System.Drawing.Point(20, 43);
		this.txtmontoaevaluar.Name = "txtmontoaevaluar";
		this.txtmontoaevaluar.ReadOnly = true;
		this.txtmontoaevaluar.Size = new System.Drawing.Size(85, 20);
		this.txtmontoaevaluar.TabIndex = 18;
		this.txtmontoaevaluar.Text = "0";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 25);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(88, 13);
		this.label4.TabIndex = 17;
		this.label4.Text = "Monto a Evaluar:";
		this.groupBox4.Controls.Add(this.label11);
		this.groupBox4.Controls.Add(this.txtcodarqueo);
		this.groupBox4.Controls.Add(this.txthorafin);
		this.groupBox4.Controls.Add(this.label8);
		this.groupBox4.Controls.Add(this.txthorainicio);
		this.groupBox4.Controls.Add(this.label7);
		this.groupBox4.Controls.Add(this.txtnombre);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Location = new System.Drawing.Point(0, 40);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(643, 75);
		this.groupBox4.TabIndex = 3;
		this.groupBox4.TabStop = false;
		this.txthorafin.Location = new System.Drawing.Point(474, 45);
		this.txthorafin.Name = "txthorafin";
		this.txthorafin.Size = new System.Drawing.Size(93, 20);
		this.txthorafin.TabIndex = 26;
		this.txthorafin.Text = "10:45 AM";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(388, 46);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(61, 13);
		this.label8.TabIndex = 25;
		this.label8.Text = "Hora Inicio:";
		this.txthorainicio.Location = new System.Drawing.Point(94, 42);
		this.txthorainicio.Name = "txthorainicio";
		this.txthorainicio.Size = new System.Drawing.Size(93, 20);
		this.txthorainicio.TabIndex = 24;
		this.txthorainicio.Text = "10:45 AM";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(17, 45);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(61, 13);
		this.label7.TabIndex = 23;
		this.label7.Text = "Hora Inicio:";
		this.txtnombre.Location = new System.Drawing.Point(94, 13);
		this.txtnombre.Name = "txtnombre";
		this.txtnombre.Size = new System.Drawing.Size(473, 20);
		this.txtnombre.TabIndex = 22;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(74, 13);
		this.label6.TabIndex = 21;
		this.label6.Text = "Encargado(a):";
		this.txtTotalBilletes.Location = new System.Drawing.Point(194, 208);
		this.txtTotalBilletes.Name = "txtTotalBilletes";
		this.txtTotalBilletes.Size = new System.Drawing.Size(93, 20);
		this.txtTotalBilletes.TabIndex = 22;
		this.txtTotalBilletes.Text = "0";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(111, 211);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(70, 13);
		this.label9.TabIndex = 21;
		this.label9.Text = "Total Billetes:";
		this.txtTotalMonedas.Location = new System.Drawing.Point(220, 208);
		this.txtTotalMonedas.Name = "txtTotalMonedas";
		this.txtTotalMonedas.Size = new System.Drawing.Size(93, 20);
		this.txtTotalMonedas.TabIndex = 24;
		this.txtTotalMonedas.Text = "0";
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(137, 211);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(81, 13);
		this.label10.TabIndex = 23;
		this.label10.Text = "Total Monedas:";
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.txtcodarqueo.Location = new System.Drawing.Point(294, 41);
		this.txtcodarqueo.Name = "txtcodarqueo";
		this.txtcodarqueo.Size = new System.Drawing.Size(62, 20);
		this.txtcodarqueo.TabIndex = 27;
		this.txtcodarqueo.Visible = false;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(239, 44);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(43, 13);
		this.label11.TabIndex = 28;
		this.label11.Text = "Codigo:";
		this.label11.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(643, 458);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmArqueoFondoFijo";
		this.Text = "Arqueo de Fondo Fijo";
		base.Load += new System.EventHandler(frmArqueoFondoFijo_Load);
		this.groupBox1.ResumeLayout(false);
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvBilletes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvMonedas).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
