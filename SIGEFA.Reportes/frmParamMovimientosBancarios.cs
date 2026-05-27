using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamMovimientosBancarios : Office2007Form
{
	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	private IContainer components = null;

	private GroupBox groupBox1;

	private ImageList imageList2;

	private GroupBox groupBox5;

	private Button btnsalir;

	private Button btnvisualizar;

	private GroupBox groupBox4;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private Label label2;

	private Label label1;

	public frmParamMovimientosBancarios()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnvisualizar_Click(object sender, EventArgs e)
	{
		CRReporteMovimientosBancariosRPT rpt = new CRReporteMovimientosBancariosRPT();
		frmRptMovimientosBancario frm = new frmRptMovimientosBancario();
		rpt.SetDataSource(ds.ReporteMovimientosBancarios(dtpFecha1.Value.Date, dtpFecha2.Value.Date, frmLogin.iCodAlmacen).Tables[0]);
		frm.cRVMovimientosBancarios.ReportSource = rpt;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmParamMovimientosBancarios));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.btnsalir = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnvisualizar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox5.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(311, 88);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos del Reporte";
		this.groupBox4.Controls.Add(this.dtpFecha2);
		this.groupBox4.Controls.Add(this.dtpFecha1);
		this.groupBox4.Controls.Add(this.label2);
		this.groupBox4.Controls.Add(this.label1);
		this.groupBox4.Location = new System.Drawing.Point(6, 19);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(294, 58);
		this.groupBox4.TabIndex = 15;
		this.groupBox4.TabStop = false;
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(155, 32);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(133, 20);
		this.dtpFecha2.TabIndex = 3;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(9, 32);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(119, 20);
		this.dtpFecha1.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(152, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(57, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Fecha Fin:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha Inicio:";
		this.groupBox5.Controls.Add(this.btnsalir);
		this.groupBox5.Controls.Add(this.btnvisualizar);
		this.groupBox5.Location = new System.Drawing.Point(12, 106);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(311, 46);
		this.groupBox5.TabIndex = 1;
		this.groupBox5.TabStop = false;
		this.btnsalir.ForeColor = System.Drawing.Color.Black;
		this.btnsalir.ImageIndex = 5;
		this.btnsalir.ImageList = this.imageList2;
		this.btnsalir.Location = new System.Drawing.Point(150, 8);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(79, 32);
		this.btnsalir.TabIndex = 51;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnsalir.UseVisualStyleBackColor = true;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Write Document.png");
		this.imageList2.Images.SetKeyName(1, "New Document.png");
		this.imageList2.Images.SetKeyName(2, "Remove Document.png");
		this.imageList2.Images.SetKeyName(3, "document-print.png");
		this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList2.Images.SetKeyName(5, "exit.png");
		this.imageList2.Images.SetKeyName(6, "1407293033_105251.ico");
		this.imageList2.Images.SetKeyName(7, "1407293340_83564.ico");
		this.imageList2.Images.SetKeyName(8, "archivos.png");
		this.imageList2.Images.SetKeyName(9, "pdf.png");
		this.btnvisualizar.ForeColor = System.Drawing.Color.Black;
		this.btnvisualizar.ImageIndex = 8;
		this.btnvisualizar.ImageList = this.imageList2;
		this.btnvisualizar.Location = new System.Drawing.Point(35, 8);
		this.btnvisualizar.Name = "btnvisualizar";
		this.btnvisualizar.Size = new System.Drawing.Size(109, 32);
		this.btnvisualizar.TabIndex = 50;
		this.btnvisualizar.Text = "Visualizar";
		this.btnvisualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnvisualizar.UseVisualStyleBackColor = true;
		this.btnvisualizar.Click += new System.EventHandler(btnvisualizar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(330, 156);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox5);
		this.DoubleBuffered = true;
		base.Name = "frmParamMovimientosBancarios";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte de Movimientos Bancarios";
		this.groupBox1.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
