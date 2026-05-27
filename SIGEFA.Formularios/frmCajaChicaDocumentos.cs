using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AxAcroPDFLib;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmCajaChicaDocumentos : Office2007Form
{
	public int Tipo = 0;

	private IContainer components = null;

	private Panel panel1;

	private AxAcroPDF axAcroPDF1;

	private Panel panel4;

	private Panel panel2;

	private DataGridView dataGridView1;

	private Button btnSalir;

	private ImageList imageList1;

	private Button btnGuardar;

	private Button btnabrir;

	private ImageList imageList2;

	private Button btnmostrar;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private TextBox textBox1;

	private Label label1;

	private TextBox textBox2;

	private Label label2;

	public frmCajaChicaDocumentos()
	{
		InitializeComponent();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaChicaDocumentos));
		this.panel1 = new System.Windows.Forms.Panel();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel4 = new System.Windows.Forms.Panel();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.label2 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnabrir = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnmostrar = new System.Windows.Forms.Button();
		this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.panel4.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.axAcroPDF1).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.dataGridView1);
		this.panel1.Controls.Add(this.panel4);
		this.panel1.Controls.Add(this.panel2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
		this.panel1.Location = new System.Drawing.Point(503, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(315, 537);
		this.panel1.TabIndex = 0;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.Columns.AddRange(this.codigo, this.descripcion);
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dataGridView1.Location = new System.Drawing.Point(0, 174);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(315, 323);
		this.dataGridView1.TabIndex = 1;
		this.codigo.DataPropertyName = "codigo";
		this.codigo.HeaderText = "codigo";
		this.codigo.Name = "codigo";
		this.codigo.Visible = false;
		this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "DESCRIPCION";
		this.descripcion.Name = "descripcion";
		this.panel4.Controls.Add(this.btnSalir);
		this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel4.Location = new System.Drawing.Point(0, 497);
		this.panel4.Name = "panel4";
		this.panel4.Size = new System.Drawing.Size(315, 40);
		this.panel4.TabIndex = 0;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(243, 4);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 12;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.panel2.Controls.Add(this.label2);
		this.panel2.Controls.Add(this.textBox2);
		this.panel2.Controls.Add(this.textBox1);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.btnGuardar);
		this.panel2.Controls.Add(this.btnabrir);
		this.panel2.Controls.Add(this.btnmostrar);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(315, 174);
		this.panel2.TabIndex = 0;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 62);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 17;
		this.label2.Text = "Descripcion:";
		this.textBox2.Location = new System.Drawing.Point(9, 82);
		this.textBox2.MaxLength = 100;
		this.textBox2.Multiline = true;
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(302, 40);
		this.textBox2.TabIndex = 16;
		this.textBox1.Location = new System.Drawing.Point(8, 34);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(62, 20);
		this.textBox1.TabIndex = 15;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 14;
		this.label1.Text = "Codigo:";
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(220, 132);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(91, 36);
		this.btnGuardar.TabIndex = 13;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnabrir.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnabrir.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnabrir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnabrir.ImageIndex = 0;
		this.btnabrir.ImageList = this.imageList2;
		this.btnabrir.Location = new System.Drawing.Point(8, 132);
		this.btnabrir.Name = "btnabrir";
		this.btnabrir.Size = new System.Drawing.Size(109, 36);
		this.btnabrir.TabIndex = 7;
		this.btnabrir.Text = "Cargar Documento";
		this.btnabrir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnabrir.UseVisualStyleBackColor = true;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "pdf_documents.png");
		this.imageList2.Images.SetKeyName(1, "PDF_logo.jpg");
		this.imageList2.Images.SetKeyName(2, "PDF.png");
		this.btnmostrar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnmostrar.Enabled = false;
		this.btnmostrar.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnmostrar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnmostrar.ImageIndex = 2;
		this.btnmostrar.ImageList = this.imageList2;
		this.btnmostrar.Location = new System.Drawing.Point(123, 132);
		this.btnmostrar.Name = "btnmostrar";
		this.btnmostrar.Size = new System.Drawing.Size(91, 36);
		this.btnmostrar.TabIndex = 8;
		this.btnmostrar.Text = "Mostrar PDF";
		this.btnmostrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnmostrar.UseVisualStyleBackColor = true;
		this.axAcroPDF1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.axAcroPDF1.Enabled = true;
		this.axAcroPDF1.Location = new System.Drawing.Point(0, 0);
		this.axAcroPDF1.Name = "axAcroPDF1";
		this.axAcroPDF1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axAcroPDF1.OcxState");
		this.axAcroPDF1.Size = new System.Drawing.Size(503, 537);
		this.axAcroPDF1.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(818, 537);
		base.Controls.Add(this.axAcroPDF1);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.Name = "frmCajaChicaDocumentos";
		this.Text = "Caja Chica";
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.panel4.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.axAcroPDF1).EndInit();
		base.ResumeLayout(false);
	}
}
