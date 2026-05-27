using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamKardexArticulo : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsReporteKardex ds = new clsReporteKardex();

	private clsProducto pro = new clsProducto();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsAdmFamilia admFamilia = new clsAdmFamilia();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private IContainer components = null;

	private GroupBox groupBox4;

	public TextBox txtArticulo;

	public TextBox txtUnArt;

	private RadioButton rbArt;

	private RadioButton rbTodosArt;

	private Button btnCancelar;

	private Button btnReporte;

	private GroupBox groupBox1;

	private Label label8;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private Button button1;

	private Button button2;

	private GroupBox groupBox2;

	private ComboBox cmbFamilia;

	private ComboBox cmbAlmacen;

	private Label label2;

	private Label label3;

	private Button button3;

	private CheckBox chkListarTransferencia;

	private System.Windows.Forms.ToolTip ttMensaje;

	public frmParamKardexArticulo()
	{
		InitializeComponent();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRKardex4 rpt1 = new CRKardex4();
		frmRptKardex frm = new frmRptKardex();
		DataTable nuevo = new DataTable();
		try
		{
			if (rbArt.Checked)
			{
				if (txtUnArt.Text != "")
				{
					nuevo = ds.kardex4(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal).Tables[0];
					rpt1.SetDataSource(nuevo);
					frm.crvKardex.ReportSource = rpt1;
					frm.Show();
				}
				else
				{
					MessageBox.Show("Debe elegir un producto");
				}
			}
			if (rbTodosArt.Checked)
			{
				nuevo = ds.kardex4(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal).Tables[0];
				rpt1.SetDataSource(nuevo);
				frm.crvKardex.ReportSource = rpt1;
				frm.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 20;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				CargaProducto(frm.GetCodigoProducto());
			}
		}
		else
		{
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			try
			{
				if (txtUnArt.Text != "")
				{
					string cod = txtUnArt.Text;
					txtUnArt.Text = cod.PadLeft(9, '0');
					int codPro = AdmPro.GetCodProducto_xDescripcion(txtUnArt.Text);
					if (codPro != 0)
					{
						CargaProducto(codPro);
					}
				}
				else
				{
					MessageBox.Show("Faltan datos..!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}

	private void CargaProducto(int Codigo)
	{
		pro = AdmPro.CargaProductoDetalle(Codigo, frmLogin.iCodAlmacen, 2, 0, 0);
		if (pro != null)
		{
			txtUnArt.Text = pro.Referencia;
			txtArticulo.Text = pro.Descripcion;
		}
		else
		{
			MessageBox.Show("Producto no encontrado", "Reporte de Mes por Articulo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void rbArt_CheckedChanged(object sender, EventArgs e)
	{
		txtUnArt.Text = "";
		txtArticulo.Text = "";
		txtUnArt.Enabled = rbArt.Checked;
		txtUnArt.Focus();
		pro.CodProducto = 0;
	}

	private void frmParamKardexArticulo_Load(object sender, EventArgs e)
	{
		cargarComboAlmacen();
		cargarComboFamilia();
	}

	private void cargarComboFamilia()
	{
		cmbFamilia.ValueMember = "codFamilia";
		cmbFamilia.DisplayMember = "descripcion";
		DataTable aux = admFamilia.MuestraFamilias();
		DataRow fila = aux.NewRow();
		fila.ItemArray = new object[6]
		{
			-1,
			"TODOS",
			"TODOS",
			0,
			0,
			DateTime.Now
		};
		aux.Rows.InsertAt(fila, 0);
		cmbFamilia.DataSource = aux;
		cmbFamilia.SelectedValue = -1;
	}

	private void cargarComboAlmacen()
	{
		cmbAlmacen.ValueMember = "cod";
		cmbAlmacen.DisplayMember = "nombre";
		DataTable aux = admAlm.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		cmbAlmacen.DataSource = aux;
		cmbAlmacen.SelectedValue = 0;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		CRKardexInterno rpt1 = new CRKardexInterno();
		frmRptKardex frm = new frmRptKardex();
		DataTable nuevo = new DataTable();
		try
		{
			if (rbArt.Checked)
			{
				if (txtUnArt.Text != "")
				{
					nuevo = ds.kardexInterno(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal).Tables[0];
					rpt1.SetDataSource(nuevo);
					frm.crvKardex.ReportSource = rpt1;
					frm.Show();
				}
				else
				{
					MessageBox.Show("Debe elegir un producto");
				}
			}
			if (rbTodosArt.Checked)
			{
				nuevo = ds.kardexInterno(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal).Tables[0];
				rpt1.SetDataSource(nuevo);
				frm.crvKardex.ReportSource = rpt1;
				frm.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		CRKardexInternoSimplificado rpt1 = new CRKardexInternoSimplificado();
		frmRptKardexSimplificado frm = new frmRptKardexSimplificado();
		DataSet nuevo = new DataSet();
		try
		{
			if (rbArt.Checked)
			{
				if (txtUnArt.Text != "")
				{
					nuevo = ds.kardexInternoConTransferencia(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal, chkListarTransferencia.Checked);
					rpt1.SetDataSource(nuevo);
					frm.crvKardex.ReportSource = rpt1;
					frm.Show();
				}
				else
				{
					MessageBox.Show("Debe elegir un producto");
				}
			}
			if (rbTodosArt.Checked)
			{
				nuevo = ds.kardexInternoConTransferencia(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal, chkListarTransferencia.Checked);
				rpt1.SetDataSource(nuevo);
				frm.crvKardex.ReportSource = rpt1;
				frm.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		CRKardexTransferencia_S rpt1 = new CRKardexTransferencia_S();
		frmRptKardexTranseferencia frm = new frmRptKardexTranseferencia();
		DataTable nuevo = new DataTable();
		try
		{
			if (rbArt.Checked)
			{
				if (txtUnArt.Text != "")
				{
					nuevo = ds.kardexTransferencia(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal).Tables[0];
					rpt1.SetDataSource(nuevo);
					frm.crvKardexTransferencia_s.ReportSource = rpt1;
					frm.Show();
				}
				else
				{
					MessageBox.Show("Debe elegir un producto");
				}
			}
			if (rbTodosArt.Checked)
			{
				nuevo = ds.kardexTransferencia(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text, Convert.ToInt32(cmbAlmacen.SelectedValue), Convert.ToInt32(cmbFamilia.SelectedValue), frmLogin.iCodSucursal).Tables[0];
				rpt1.SetDataSource(nuevo);
				frm.crvKardexTransferencia_s.ReportSource = rpt1;
				frm.Show();
			}
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
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtArticulo = new System.Windows.Forms.TextBox();
		this.txtUnArt = new System.Windows.Forms.TextBox();
		this.rbArt = new System.Windows.Forms.RadioButton();
		this.rbTodosArt = new System.Windows.Forms.RadioButton();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label8 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.chkListarTransferencia = new System.Windows.Forms.CheckBox();
		this.cmbFamilia = new System.Windows.Forms.ComboBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.button3 = new System.Windows.Forms.Button();
		this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
		this.groupBox4.SuspendLayout();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(16, 229);
		this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox4.Size = new System.Drawing.Size(603, 96);
		this.groupBox4.TabIndex = 71;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "PRODUCTO";
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(259, 25);
		this.txtArticulo.Margin = new System.Windows.Forms.Padding(4);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(321, 22);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Location = new System.Drawing.Point(139, 25);
		this.txtUnArt.Margin = new System.Windows.Forms.Padding(4);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(111, 22);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.rbArt.AutoSize = true;
		this.rbArt.BackColor = System.Drawing.Color.Transparent;
		this.rbArt.Checked = true;
		this.rbArt.Location = new System.Drawing.Point(25, 28);
		this.rbArt.Margin = new System.Windows.Forms.Padding(4);
		this.rbArt.Name = "rbArt";
		this.rbArt.Size = new System.Drawing.Size(90, 20);
		this.rbArt.TabIndex = 57;
		this.rbArt.TabStop = true;
		this.rbArt.Text = "Un Artículo";
		this.rbArt.UseVisualStyleBackColor = false;
		this.rbArt.CheckedChanged += new System.EventHandler(rbArt_CheckedChanged);
		this.rbTodosArt.AutoSize = true;
		this.rbTodosArt.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosArt.Location = new System.Drawing.Point(25, 59);
		this.rbTodosArt.Margin = new System.Windows.Forms.Padding(4);
		this.rbTodosArt.Name = "rbTodosArt";
		this.rbTodosArt.Size = new System.Drawing.Size(140, 20);
		this.rbTodosArt.TabIndex = 54;
		this.rbTodosArt.Text = "Todos los artículos";
		this.rbTodosArt.UseVisualStyleBackColor = false;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(522, 333);
		this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(100, 28);
		this.btnCancelar.TabIndex = 70;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(414, 333);
		this.btnReporte.Margin = new System.Windows.Forms.Padding(4);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 28);
		this.btnReporte.TabIndex = 69;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(16, 4);
		this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox1.Size = new System.Drawing.Size(603, 101);
		this.groupBox1.TabIndex = 68;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "FILTRO DE FECHAS";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(259, 50);
		this.dtpFecha2.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha2.TabIndex = 51;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(25, 50);
		this.dtpFecha1.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(131, 22);
		this.dtpFecha1.TabIndex = 52;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(256, 32);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(49, 16);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(23, 31);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 16);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button1.ImageIndex = 1;
		this.button1.Location = new System.Drawing.Point(287, 333);
		this.button1.Margin = new System.Windows.Forms.Padding(4);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(119, 28);
		this.button1.TabIndex = 72;
		this.button1.Text = "Reporte Interno";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button2.ImageIndex = 1;
		this.button2.Location = new System.Drawing.Point(133, 333);
		this.button2.Margin = new System.Windows.Forms.Padding(4);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(146, 28);
		this.button2.TabIndex = 73;
		this.button2.Text = "Reporte Simplificado";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.groupBox2.Controls.Add(this.chkListarTransferencia);
		this.groupBox2.Controls.Add(this.cmbFamilia);
		this.groupBox2.Controls.Add(this.cmbAlmacen);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Location = new System.Drawing.Point(16, 112);
		this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
		this.groupBox2.Size = new System.Drawing.Size(603, 109);
		this.groupBox2.TabIndex = 69;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "FILTRO EXTRA";
		this.chkListarTransferencia.AutoSize = true;
		this.chkListarTransferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkListarTransferencia.ForeColor = System.Drawing.Color.SteelBlue;
		this.chkListarTransferencia.Location = new System.Drawing.Point(25, 83);
		this.chkListarTransferencia.Name = "chkListarTransferencia";
		this.chkListarTransferencia.Size = new System.Drawing.Size(406, 20);
		this.chkListarTransferencia.TabIndex = 54;
		this.chkListarTransferencia.Text = "Visualizar Transferencias Pendientes Entre Almacenes";
		this.ttMensaje.SetToolTip(this.chkListarTransferencia, "Esta opcion solo es valida en Reporte Simplificado");
		this.chkListarTransferencia.UseVisualStyleBackColor = true;
		this.cmbFamilia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFamilia.FormattingEnabled = true;
		this.cmbFamilia.Location = new System.Drawing.Point(259, 52);
		this.cmbFamilia.Name = "cmbFamilia";
		this.cmbFamilia.Size = new System.Drawing.Size(258, 24);
		this.cmbFamilia.TabIndex = 53;
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(26, 52);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(162, 24);
		this.cmbAlmacen.TabIndex = 53;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(256, 32);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(59, 16);
		this.label2.TabIndex = 45;
		this.label2.Text = "Familia";
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(23, 31);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 16);
		this.label3.TabIndex = 26;
		this.label3.Text = "Almacen";
		this.button3.Location = new System.Drawing.Point(16, 336);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(107, 23);
		this.button3.TabIndex = 74;
		this.button3.Text = "Transferencia";
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Visible = false;
		this.button3.Click += new System.EventHandler(button3_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(637, 372);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "frmParamKardexArticulo";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "KARDEX POR PRODUCTO";
		base.Load += new System.EventHandler(frmParamKardexArticulo_Load);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
