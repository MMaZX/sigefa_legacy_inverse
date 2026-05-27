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

public class frmUtilidad : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsReporteKardex ds = new clsReporteKardex();

	private clsProducto pro = new clsProducto();

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

	public frmUtilidad()
	{
		InitializeComponent();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		CRUtilidad2 rpt1 = new CRUtilidad2();
		frmRptUtilidad frm = new frmRptUtilidad();
		DataTable nuevo = new DataTable();
		try
		{
			if (rbArt.Checked)
			{
				if (txtUnArt.Text != "")
				{
					rpt1.SetDataSource(ds.UtilidadProducto(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen, frmLogin.iCodSucursal, Convert.ToInt32(pro.CodProducto)).Tables[0]);
					nuevo = ds.Utilidad(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen, frmLogin.iCodSucursal).Tables[0];
					frm.crvInventario.ReportSource = rpt1;
					frm.Show();
				}
				else
				{
					MessageBox.Show("Debe elegir un producto");
				}
			}
			if (rbTodosArt.Checked)
			{
				rpt1.SetDataSource(ds.Utilidad2(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen, frmLogin.iCodSucursal).Tables[0]);
				nuevo = ds.Utilidad(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen, frmLogin.iCodSucursal).Tables[0];
				frm.crvInventario.ReportSource = rpt1;
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
	}

	private void CargaProducto(int Codigo)
	{
		pro = AdmPro.CargaProducto(Codigo, frmLogin.iCodAlmacen);
		txtUnArt.Text = pro.Referencia;
		txtArticulo.Text = pro.Descripcion;
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
		this.groupBox4.SuspendLayout();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox4.Controls.Add(this.txtArticulo);
		this.groupBox4.Controls.Add(this.txtUnArt);
		this.groupBox4.Controls.Add(this.rbArt);
		this.groupBox4.Controls.Add(this.rbTodosArt);
		this.groupBox4.Location = new System.Drawing.Point(12, 87);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(452, 78);
		this.groupBox4.TabIndex = 71;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Por Artículo";
		this.txtArticulo.Enabled = false;
		this.txtArticulo.Location = new System.Drawing.Point(194, 20);
		this.txtArticulo.Name = "txtArticulo";
		this.txtArticulo.Size = new System.Drawing.Size(248, 20);
		this.txtArticulo.TabIndex = 63;
		this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnArt.Location = new System.Drawing.Point(104, 20);
		this.txtUnArt.Name = "txtUnArt";
		this.txtUnArt.Size = new System.Drawing.Size(84, 20);
		this.txtUnArt.TabIndex = 61;
		this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(txtUnArt_KeyDown);
		this.rbArt.AutoSize = true;
		this.rbArt.BackColor = System.Drawing.Color.Transparent;
		this.rbArt.Checked = true;
		this.rbArt.Location = new System.Drawing.Point(19, 23);
		this.rbArt.Name = "rbArt";
		this.rbArt.Size = new System.Drawing.Size(79, 17);
		this.rbArt.TabIndex = 57;
		this.rbArt.TabStop = true;
		this.rbArt.Text = "Un Artículo";
		this.rbArt.UseVisualStyleBackColor = false;
		this.rbArt.CheckedChanged += new System.EventHandler(rbArt_CheckedChanged);
		this.rbTodosArt.AutoSize = true;
		this.rbTodosArt.BackColor = System.Drawing.Color.Transparent;
		this.rbTodosArt.Location = new System.Drawing.Point(19, 48);
		this.rbTodosArt.Name = "rbTodosArt";
		this.rbTodosArt.Size = new System.Drawing.Size(106, 17);
		this.rbTodosArt.TabIndex = 54;
		this.rbTodosArt.Text = "Todos las ventas";
		this.rbTodosArt.UseVisualStyleBackColor = false;
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(389, 171);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 70;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(308, 171);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 69;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(452, 75);
		this.groupBox1.TabIndex = 68;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(258, 41);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha2.TabIndex = 51;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(83, 41);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha1.TabIndex = 52;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(256, 26);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 12);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(81, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(485, 199);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmUtilidad";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Utilidad";
		base.Load += new System.EventHandler(frmParamKardexArticulo_Load);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
