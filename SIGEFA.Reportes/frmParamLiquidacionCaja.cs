using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes;

public class frmParamLiquidacionCaja : Office2007Form
{
	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	private IContainer components = null;

	private DateTimePicker dtpFecha1;

	private Label label1;

	private Label label2;

	private DateTimePicker dtpFecha2;

	private Button btnVisualizar;

	private Button btnSalir;

	private ImageList imageList2;

	public frmParamLiquidacionCaja()
	{
		InitializeComponent();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		CRLiquidacionCaja rpt1 = new CRLiquidacionCaja();
		frmRptLiquidacionCaja frm1 = new frmRptLiquidacionCaja();
		rpt1.SetDataSource(ds.ReportePagosFacturaVenta(frmLogin.iCodAlmacen).Tables[0]);
		frm1.cRVLiquidacionCaja.ReportSource = rpt1;
		frm1.Show();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Reportes.frmParamLiquidacionCaja));
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnVisualizar = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(12, 49);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(112, 20);
		this.dtpFecha1.TabIndex = 0;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Fecha Inicio:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(162, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(57, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Fecha Fin:";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(165, 49);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(110, 20);
		this.dtpFecha2.TabIndex = 3;
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
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.ImageKey = "exit.png";
		this.btnSalir.ImageList = this.imageList2;
		this.btnSalir.Location = new System.Drawing.Point(141, 93);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(67, 36);
		this.btnSalir.TabIndex = 5;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(button2_Click);
		this.btnVisualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnVisualizar.ImageKey = "archivos.png";
		this.btnVisualizar.ImageList = this.imageList2;
		this.btnVisualizar.Location = new System.Drawing.Point(51, 93);
		this.btnVisualizar.Name = "btnVisualizar";
		this.btnVisualizar.Size = new System.Drawing.Size(84, 36);
		this.btnVisualizar.TabIndex = 4;
		this.btnVisualizar.Text = "Visualizar";
		this.btnVisualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnVisualizar.UseVisualStyleBackColor = true;
		this.btnVisualizar.Click += new System.EventHandler(button1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(305, 148);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnVisualizar);
		base.Controls.Add(this.dtpFecha2);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dtpFecha1);
		this.DoubleBuffered = true;
		base.Name = "frmParamLiquidacionCaja";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Reporte de Liquidacion Caja";
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
