using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmRotacionProductos : Office2007Form
{
	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsRotacionProductos ds = new clsRotacionProductos();

	private int mesInicio = 0;

	private int mesFin = 0;

	private IContainer components = null;

	private RadioButton rbFechas;

	private GroupBox groupBox1;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private Label label2;

	private Label label1;

	private Button btnImprimir;

	private Button btnSalir;

	private GroupBox groupBox4;

	private ComboBox cmbAlmacen;

	private RadioButton rbUnAlmacen;

	private RadioButton rbTodoAlmacen;

	private ImageList imageList1;

	private ImageList imageList2;

	private Label label5;

	private ComboBox cmbMes2;

	private ComboBox cmbMes1;

	private Label label3;

	private Label label4;

	private RadioButton rbMeses;

	private ComboBox cmbAño;

	public frmRotacionProductos()
	{
		InitializeComponent();
	}

	private void CargaAlmacen()
	{
		cmbAlmacen.DataSource = admAlm.CargaAlmacen2(frmLogin.iCodEmpresa);
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.SelectedIndex = -1;
	}

	private void frmRotacionProductos_Load(object sender, EventArgs e)
	{
		dtpFecha1.MaxDate = Convert.ToDateTime(DateTime.Now);
		dtpFecha2.MaxDate = Convert.ToDateTime(DateTime.Now);
		cmbMes1.SelectedIndex = 0;
		cmbMes2.SelectedIndex = 0;
		CargaAlmacen();
		rbFechas.Checked = true;
		rbTodoAlmacen.Checked = true;
		cmbAño.SelectedValue = DateTime.Now.Year;
		llenacombos();
	}

	private void llenacombos()
	{
		DataTable dt1 = new DataTable("Tabla1");
		dt1.Columns.Add("Codigo");
		dt1.Columns.Add("Descripcion");
		DataRow dr1 = dt1.NewRow();
		dr1[0] = "2013";
		dr1[1] = "2013";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2014";
		dr1[1] = "2014";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2015";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2016";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2017";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2018";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2019";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2020";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2021";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2022";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2023";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2024";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2025";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2026";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2027";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2028";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2029";
		dt1.Rows.Add(dr1);
		dr1 = dt1.NewRow();
		dr1[0] = "2015";
		dr1[1] = "2030";
		dt1.Rows.Add(dr1);
		cmbAño.DataSource = dt1;
		cmbAño.ValueMember = "Codigo";
		cmbAño.DisplayMember = "Descripcion";
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void rbFechas_CheckedChanged(object sender, EventArgs e)
	{
		if (rbFechas.Checked)
		{
			label1.Visible = true;
			label2.Visible = true;
			dtpFecha1.Visible = true;
			dtpFecha2.Visible = true;
		}
		else
		{
			label1.Visible = false;
			label2.Visible = false;
			dtpFecha1.Visible = false;
			dtpFecha2.Visible = false;
		}
	}

	private void rbMeses_CheckedChanged(object sender, EventArgs e)
	{
		if (rbMeses.Checked)
		{
			label3.Visible = true;
			label4.Visible = true;
			label5.Visible = true;
			cmbMes1.Visible = true;
			cmbMes2.Visible = true;
			cmbAño.Visible = true;
		}
		else
		{
			label3.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			cmbMes1.Visible = false;
			cmbMes2.Visible = false;
			cmbAño.Visible = false;
			cmbMes1.SelectedIndex = -1;
			cmbMes2.SelectedIndex = -1;
		}
	}

	private void rbUnAlmacen_CheckedChanged(object sender, EventArgs e)
	{
		if (rbUnAlmacen.Checked)
		{
			cmbAlmacen.Visible = true;
			return;
		}
		cmbAlmacen.Visible = false;
		cmbAlmacen.SelectedIndex = -1;
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			mesInicio = cmbMes1.SelectedIndex + 1;
			mesFin = cmbMes2.SelectedIndex + 1;
			CRRotacionProducto rpt = new CRRotacionProducto();
			frmRptRotacionProducto frm = new frmRptRotacionProducto();
			DataTable dt = ds.ReportRotacionProductos(rbTodoAlmacen.Checked, rbFechas.Checked, dtpFecha1.Value, dtpFecha2.Value, mesInicio, mesFin, cmbAño.SelectedText, Convert.ToInt32(cmbAlmacen.SelectedValue)).Tables[0];
			if (dt.Rows.Count > 0)
			{
				rpt.SetDataSource(dt);
				frm.cRVRotacionProducto.ReportSource = rpt;
				frm.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Rotación de Productos");
		}
	}

	private void frmRotacionProductos_Shown(object sender, EventArgs e)
	{
		cmbAño.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRotacionProductos));
		this.rbFechas = new System.Windows.Forms.RadioButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cmbMes2 = new System.Windows.Forms.ComboBox();
		this.cmbMes1 = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.rbMeses = new System.Windows.Forms.RadioButton();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.rbUnAlmacen = new System.Windows.Forms.RadioButton();
		this.rbTodoAlmacen = new System.Windows.Forms.RadioButton();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.cmbAño = new System.Windows.Forms.ComboBox();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.rbFechas.AutoSize = true;
		this.rbFechas.Location = new System.Drawing.Point(20, 19);
		this.rbFechas.Name = "rbFechas";
		this.rbFechas.Size = new System.Drawing.Size(79, 17);
		this.rbFechas.TabIndex = 0;
		this.rbFechas.TabStop = true;
		this.rbFechas.Text = "Por Fechas";
		this.rbFechas.UseVisualStyleBackColor = true;
		this.rbFechas.CheckedChanged += new System.EventHandler(rbFechas_CheckedChanged);
		this.groupBox1.Controls.Add(this.cmbAño);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.cmbMes2);
		this.groupBox1.Controls.Add(this.cmbMes1);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.rbMeses);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.rbFechas);
		this.groupBox1.Location = new System.Drawing.Point(9, -3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(691, 105);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(299, 83);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(35, 13);
		this.label5.TabIndex = 18;
		this.label5.Text = "Año : ";
		this.label5.Visible = false;
		this.cmbMes2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMes2.FormattingEnabled = true;
		this.cmbMes2.Items.AddRange(new object[12]
		{
			"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE",
			"NOVIEMBRE", "DICIEMBRE"
		});
		this.cmbMes2.Location = new System.Drawing.Point(543, 45);
		this.cmbMes2.Name = "cmbMes2";
		this.cmbMes2.Size = new System.Drawing.Size(139, 21);
		this.cmbMes2.TabIndex = 6;
		this.cmbMes2.Visible = false;
		this.cmbMes1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbMes1.FormattingEnabled = true;
		this.cmbMes1.Items.AddRange(new object[12]
		{
			"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE",
			"NOVIEMBRE", "DICIEMBRE"
		});
		this.cmbMes1.Location = new System.Drawing.Point(340, 45);
		this.cmbMes1.Name = "cmbMes1";
		this.cmbMes1.Size = new System.Drawing.Size(139, 21);
		this.cmbMes1.TabIndex = 4;
		this.cmbMes1.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(496, 49);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(41, 13);
		this.label3.TabIndex = 15;
		this.label3.Text = "Hasta: ";
		this.label3.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(290, 51);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(44, 13);
		this.label4.TabIndex = 14;
		this.label4.Text = "Desde: ";
		this.label4.Visible = false;
		this.rbMeses.AutoSize = true;
		this.rbMeses.Location = new System.Drawing.Point(273, 19);
		this.rbMeses.Name = "rbMeses";
		this.rbMeses.Size = new System.Drawing.Size(64, 17);
		this.rbMeses.TabIndex = 1;
		this.rbMeses.TabStop = true;
		this.rbMeses.Text = "Por Mes";
		this.rbMeses.UseVisualStyleBackColor = true;
		this.rbMeses.CheckedChanged += new System.EventHandler(rbMeses_CheckedChanged);
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(107, 77);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(120, 20);
		this.dtpFecha2.TabIndex = 3;
		this.dtpFecha2.Visible = false;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(107, 46);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(120, 20);
		this.dtpFecha1.TabIndex = 2;
		this.dtpFecha1.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(28, 83);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(63, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Fecha Fin : ";
		this.label2.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(20, 48);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(71, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Fecha Inicio: ";
		this.label1.Visible = false;
		this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimir.ImageKey = "document-print.png";
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(541, 185);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(75, 32);
		this.btnImprimir.TabIndex = 1;
		this.btnImprimir.Text = "Reporte";
		this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.ImageKey = "exit.png";
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(622, 185);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(76, 32);
		this.btnSalir.TabIndex = 2;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupBox4.Controls.Add(this.cmbAlmacen);
		this.groupBox4.Controls.Add(this.rbUnAlmacen);
		this.groupBox4.Controls.Add(this.rbTodoAlmacen);
		this.groupBox4.Location = new System.Drawing.Point(7, 106);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(691, 68);
		this.groupBox4.TabIndex = 8;
		this.groupBox4.TabStop = false;
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(147, 36);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(190, 21);
		this.cmbAlmacen.TabIndex = 2;
		this.cmbAlmacen.Visible = false;
		this.rbUnAlmacen.AutoSize = true;
		this.rbUnAlmacen.Location = new System.Drawing.Point(46, 40);
		this.rbUnAlmacen.Name = "rbUnAlmacen";
		this.rbUnAlmacen.Size = new System.Drawing.Size(86, 17);
		this.rbUnAlmacen.TabIndex = 1;
		this.rbUnAlmacen.TabStop = true;
		this.rbUnAlmacen.Text = "Un Almacen:";
		this.rbUnAlmacen.UseVisualStyleBackColor = true;
		this.rbUnAlmacen.CheckedChanged += new System.EventHandler(rbUnAlmacen_CheckedChanged);
		this.rbTodoAlmacen.AutoSize = true;
		this.rbTodoAlmacen.Location = new System.Drawing.Point(46, 12);
		this.rbTodoAlmacen.Name = "rbTodoAlmacen";
		this.rbTodoAlmacen.Size = new System.Drawing.Size(126, 17);
		this.rbTodoAlmacen.TabIndex = 0;
		this.rbTodoAlmacen.TabStop = true;
		this.rbTodoAlmacen.Text = "Todos los Almacenes";
		this.rbTodoAlmacen.UseVisualStyleBackColor = true;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Write Document.png");
		this.imageList2.Images.SetKeyName(1, "New Document.png");
		this.imageList2.Images.SetKeyName(2, "Remove Document.png");
		this.imageList2.Images.SetKeyName(3, "document-print.png");
		this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList2.Images.SetKeyName(5, "exit.png");
		this.imageList2.Images.SetKeyName(6, "search (1).png");
		this.imageList2.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList2.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList2.Images.SetKeyName(9, "document_delete.png");
		this.imageList2.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList2.Images.SetKeyName(11, "OK_Verde.png");
		this.cmbAño.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAño.FormattingEnabled = true;
		this.cmbAño.Location = new System.Drawing.Point(340, 80);
		this.cmbAño.Name = "cmbAño";
		this.cmbAño.Size = new System.Drawing.Size(139, 21);
		this.cmbAño.TabIndex = 19;
		this.cmbAño.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(709, 225);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnImprimir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmRotacionProductos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Rotacion de Productos";
		base.Load += new System.EventHandler(frmRotacionProductos_Load);
		base.Shown += new System.EventHandler(frmRotacionProductos_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
