using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmParametros : Office2007Form
{
	private clsAdmEmpresa AdmEmp = new clsAdmEmpresa();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label2;

	private TextBox txtIGV;

	private Label label1;

	private ImageList imageList1;

	private Button btnSalir;

	private Button btnGuardar;

	public frmParametros()
	{
		InitializeComponent();
	}

	private void frmParametros_Load(object sender, EventArgs e)
	{
		CargaConfiguracion();
	}

	private void CargaConfiguracion()
	{
		if (frmLogin.Configuracion != null)
		{
			txtIGV.Text = frmLogin.Configuracion.IGV.ToString();
		}
		else
		{
			frmLogin.Configuracion = new clsParametros();
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (txtIGV.Text != "")
		{
			frmLogin.Configuracion.IGV = Convert.ToDouble(txtIGV.Text);
		}
		if (AdmEmp.UpdateConfiguracion(frmLogin.Configuracion))
		{
			frmLogin.Configuracion = AdmEmp.CargaConfiguracion();
			MessageBox.Show("Los datos se guardaron correctamente", "Configuracion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmParametros));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label2 = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnSalir);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.btnGuardar);
		this.groupBox1.Controls.Add(this.txtIGV);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(205, 108);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Parametros Generales";
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(104, 70);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(69, 23);
		this.btnSalir.TabIndex = 19;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(158, 35);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(15, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "%";
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 1;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(23, 70);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(75, 23);
		this.btnGuardar.TabIndex = 18;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtIGV.Location = new System.Drawing.Point(104, 32);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.Size = new System.Drawing.Size(48, 20);
		this.txtIGV.TabIndex = 1;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(31, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(67, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Tasa de IGV";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(205, 117);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParametros";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Parametros";
		base.Load += new System.EventHandler(frmParametros_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
