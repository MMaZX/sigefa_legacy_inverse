using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmTablas : Office2007Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox textBox1;

	private Label label1;

	private DataGridView dataGridView1;

	private Button btnNuevo;

	private Button button2;

	private Button button3;

	private Button button4;

	private Button button5;

	private Button button6;

	private ImageList imageList1;

	public frmTablas()
	{
		InitializeComponent();
	}

	private void frmTablas_Load(object sender, EventArgs e)
	{
		groupBox1.Text = Text;
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		if (!(Text == "Unidades"))
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTablas));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button6 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dataGridView1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(465, 198);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.textBox1.Location = new System.Drawing.Point(136, 19);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(221, 20);
		this.textBox1.TabIndex = 4;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(84, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(46, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar :";
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Location = new System.Drawing.Point(6, 45);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.Size = new System.Drawing.Size(453, 147);
		this.dataGridView1.TabIndex = 2;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.button6.ImageIndex = 5;
		this.button6.ImageList = this.imageList1;
		this.button6.Location = new System.Drawing.Point(409, 216);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(68, 32);
		this.button6.TabIndex = 5;
		this.button6.Text = "Salir";
		this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button6.UseVisualStyleBackColor = true;
		this.button5.ImageIndex = 4;
		this.button5.ImageList = this.imageList1;
		this.button5.Location = new System.Drawing.Point(326, 216);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(77, 32);
		this.button5.TabIndex = 5;
		this.button5.Text = "Guardar";
		this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button5.UseVisualStyleBackColor = true;
		this.button4.ImageIndex = 3;
		this.button4.ImageList = this.imageList1;
		this.button4.Location = new System.Drawing.Point(242, 216);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(78, 32);
		this.button4.TabIndex = 4;
		this.button4.Text = "Reporte";
		this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button4.UseVisualStyleBackColor = true;
		this.button3.ImageIndex = 2;
		this.button3.ImageList = this.imageList1;
		this.button3.Location = new System.Drawing.Point(161, 216);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(75, 32);
		this.button3.TabIndex = 3;
		this.button3.Text = "Eliminar";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = true;
		this.button2.ImageIndex = 0;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(89, 216);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(66, 32);
		this.button2.TabIndex = 2;
		this.button2.Text = "Editar";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(12, 216);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 1;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(489, 260);
		base.Controls.Add(this.button6);
		base.Controls.Add(this.button5);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.button3);
		base.Controls.Add(this.button2);
		base.Controls.Add(this.btnNuevo);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTablas";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Tablas";
		base.Load += new System.EventHandler(frmTablas_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		base.ResumeLayout(false);
	}
}
