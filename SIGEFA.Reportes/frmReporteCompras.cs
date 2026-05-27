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

public class frmReporteCompras : Office2007Form
{
	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsReporteVentasxVendedor ds = new clsReporteVentasxVendedor();

	private clsVendedor vend = new clsVendedor();

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmVendedor admVen = new clsAdmVendedor();

	private CRCompras rpt;

	private CRNIngreso rptNI;

	private clsAdmZona admZona = new clsAdmZona();

	private DataTable dt = new DataTable();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label2;

	private ComboBox cmbtipo;

	private DateTimePicker dtpFecha1;

	private Label label1;

	private DateTimePicker dtpFecha2;

	private Label label8;

	private Button btnCancelar;

	private Button btnReporte;

	public frmReporteCompras()
	{
		InitializeComponent();
	}

	private void UtilidadProductos_Load(object sender, EventArgs e)
	{
		cmbtipo.SelectedIndex = 0;
		cmbtipo.SelectedIndex = -1;
	}

	private void CargaFormaPagos()
	{
		cmbtipo.DataSource = AdmPago.CargaFormaPagosReporte();
		cmbtipo.DisplayMember = "descripcion";
		cmbtipo.ValueMember = "codFormaPago";
		cmbtipo.SelectedIndex = 0;
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			if (cmbtipo.SelectedIndex == -1)
			{
				MessageBox.Show("Seleccione Tipo de Movimiento");
				return;
			}
			frmRptVentxVendedor frm = new frmRptVentxVendedor();
			switch (cmbtipo.SelectedIndex)
			{
			case 0:
				rpt = new CRCompras();
				dt = ds.ReporteCompras(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen).Tables[0];
				break;
			case 1:
				rptNI = new CRNIngreso();
				dt = ds.ReporteNotasIngreso(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen).Tables[0];
				break;
			case 2:
				dt = ds.ReporteNotasIngreso(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen).Tables[0];
				break;
			case 3:
				dt = ds.ReporteNotasIngreso(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen).Tables[0];
				break;
			}
			if (dt.Rows.Count > 0)
			{
				switch (cmbtipo.SelectedIndex)
				{
				case 0:
					rpt.SetDataSource(dt);
					frm.crvRptVentxVendedor.ReportSource = rpt;
					break;
				}
				frm.Show();
			}
			else
			{
				MessageBox.Show("No hay información para este rango de fechas..!", "Reportes", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.cmbtipo = new System.Windows.Forms.ComboBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.cmbtipo);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(452, 103);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos";
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(235, 16);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(35, 12);
		this.label8.TabIndex = 45;
		this.label8.Text = "Hasta";
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(17, 54);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(26, 12);
		this.label2.TabIndex = 43;
		this.label2.Text = "Tipo";
		this.cmbtipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbtipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbtipo.FormattingEnabled = true;
		this.cmbtipo.Items.AddRange(new object[4] { "COMPRAS", "NOTAS DE INGRESO", "NOTAS DE SALIDA", "TRANSFERECIAS" });
		this.cmbtipo.Location = new System.Drawing.Point(19, 69);
		this.cmbtipo.Name = "cmbtipo";
		this.cmbtipo.Size = new System.Drawing.Size(205, 20);
		this.cmbtipo.TabIndex = 42;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(237, 35);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha2.TabIndex = 28;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(19, 31);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(99, 20);
		this.dtpFecha1.TabIndex = 38;
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(17, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 26;
		this.label1.Text = "Desde";
		this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCancelar.ImageIndex = 0;
		this.btnCancelar.Location = new System.Drawing.Point(389, 121);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(75, 23);
		this.btnCancelar.TabIndex = 14;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReporte.ImageIndex = 1;
		this.btnReporte.Location = new System.Drawing.Point(308, 121);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 13;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		base.ClientSize = new System.Drawing.Size(477, 150);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnReporte);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmReporteCompras";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Reportes Generales";
		base.Load += new System.EventHandler(UtilidadProductos_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
