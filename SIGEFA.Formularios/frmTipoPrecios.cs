using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmTipoPrecios : Office2007Form
{
	private int proceso;

	private clsTipoPrecios tp = new clsTipoPrecios();

	private clsAdmTipoPrecio admp = new clsAdmTipoPrecio();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private DataGridView dataGridView1;

	private Button btNuevo;

	private Button btEditar;

	private Button btEliminar;

	private Button button4;

	private Button button5;

	private ImageList imageList1;

	private GroupBox groupBox1;

	private DataGridViewTextBoxColumn sigla;

	private DataGridViewTextBoxColumn codT;

	private DataGridViewTextBoxColumn Descripcion;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private RequiredFieldValidator requiredFieldValidator1;

	private CompareValidator compareValidator1;

	private GroupBox groupBox2;

	private Label label3;

	private TextBox txtcodigo;

	private Label label2;

	private Label label1;

	private TextBox txtdescripcion;

	private TextBox txtsigla;

	public frmTipoPrecios()
	{
		InitializeComponent();
	}

	private void button5_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		groupBox1.Visible = false;
		groupBox2.Visible = true;
		proceso = 1;
		btEditar.Enabled = false;
		btEliminar.Enabled = false;
	}

	private void button4_Click(object sender, EventArgs e)
	{
		if (proceso != 0 && txtdescripcion.Text != "")
		{
			tp.Sigla = txtsigla.Text;
			tp.Descripcion = txtdescripcion.Text;
			tp.CodAlmacen = Convert.ToInt32(frmLogin.iCodAlmacen);
			tp.User1 = Convert.ToInt32(frmLogin.iCodUser);
			if (proceso == 1)
			{
				if (admp.insert(tp))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Familia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					cargalista();
					groupBox1.Visible = true;
					groupBox2.Visible = false;
					btEditar.Enabled = true;
					btEliminar.Enabled = true;
				}
			}
			else if (proceso == 2)
			{
				tp.CodTipoPrecio = Convert.ToInt32(txtcodigo.Text);
				if (admp.update(tp))
				{
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Familia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					cargalista();
					groupBox1.Visible = true;
					groupBox2.Visible = false;
					btNuevo.Enabled = true;
					btEliminar.Enabled = true;
				}
			}
		}
		else
		{
			MessageBox.Show("INGRESE DATOS", "datos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void cargalista()
	{
		dataGridView1.DataSource = data;
		data.DataSource = admp.listaPrecios();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dataGridView1.ClearSelection();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		int codTipo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[codT.Name].Value);
		if (codTipo != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Bancos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admp.eliminar(codTipo))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Bancos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				cargalista();
				groupBox1.Visible = true;
				groupBox2.Visible = false;
			}
		}
	}

	private void frmTipoPrecios_Load(object sender, EventArgs e)
	{
		cargalista();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		proceso = 2;
		txtcodigo.Text = dataGridView1.CurrentRow.Cells[codT.Name].Value.ToString();
		txtsigla.Text = dataGridView1.CurrentRow.Cells[sigla.Name].Value.ToString();
		txtdescripcion.Text = dataGridView1.CurrentRow.Cells[Descripcion.Name].Value.ToString();
		groupBox2.Visible = true;
		groupBox1.Visible = false;
		btNuevo.Enabled = false;
		btEliminar.Enabled = false;
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTipoPrecios));
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codT = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btNuevo = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btEditar = new System.Windows.Forms.Button();
		this.btEliminar = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.button5 = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.txtsigla = new System.Windows.Forms.TextBox();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.requiredFieldValidator1 = new DevComponents.DotNetBar.Validator.RequiredFieldValidator();
		this.compareValidator1 = new DevComponents.DotNetBar.Validator.CompareValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtcodigo = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtdescripcion = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToOrderColumns = true;
		this.dataGridView1.AllowUserToResizeRows = false;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dataGridView1.Columns.AddRange(this.sigla, this.codT, this.Descripcion);
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dataGridView1.Location = new System.Drawing.Point(3, 16);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(414, 179);
		this.dataGridView1.TabIndex = 0;
		this.sigla.DataPropertyName = "Sigla";
		this.sigla.HeaderText = "sigla";
		this.sigla.Name = "sigla";
		this.sigla.ReadOnly = true;
		this.codT.DataPropertyName = "codT";
		this.codT.HeaderText = "codT";
		this.codT.Name = "codT";
		this.codT.ReadOnly = true;
		this.codT.Visible = false;
		this.Descripcion.DataPropertyName = "Descripcion";
		this.Descripcion.HeaderText = "Descripcion ";
		this.Descripcion.Name = "Descripcion";
		this.Descripcion.ReadOnly = true;
		this.btNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btNuevo.ImageIndex = 1;
		this.btNuevo.ImageList = this.imageList1;
		this.btNuevo.Location = new System.Drawing.Point(6, 219);
		this.btNuevo.Name = "btNuevo";
		this.btNuevo.Size = new System.Drawing.Size(82, 35);
		this.btNuevo.TabIndex = 1;
		this.btNuevo.Text = "Nuevo";
		this.btNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btNuevo.UseVisualStyleBackColor = true;
		this.btNuevo.Click += new System.EventHandler(button1_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btEditar.ImageIndex = 0;
		this.btEditar.ImageList = this.imageList1;
		this.btEditar.Location = new System.Drawing.Point(94, 219);
		this.btEditar.Name = "btEditar";
		this.btEditar.Size = new System.Drawing.Size(76, 35);
		this.btEditar.TabIndex = 2;
		this.btEditar.Text = "Editar";
		this.btEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btEditar.UseVisualStyleBackColor = true;
		this.btEditar.Click += new System.EventHandler(button2_Click);
		this.btEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btEliminar.ImageIndex = 2;
		this.btEliminar.ImageList = this.imageList1;
		this.btEliminar.Location = new System.Drawing.Point(176, 219);
		this.btEliminar.Name = "btEliminar";
		this.btEliminar.Size = new System.Drawing.Size(85, 35);
		this.btEliminar.TabIndex = 3;
		this.btEliminar.Text = "Eliminar";
		this.btEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btEliminar.UseVisualStyleBackColor = true;
		this.btEliminar.Click += new System.EventHandler(button3_Click);
		this.button4.ImageIndex = 4;
		this.button4.ImageList = this.imageList1;
		this.button4.Location = new System.Drawing.Point(267, 219);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(84, 35);
		this.button4.TabIndex = 4;
		this.button4.Text = "Guardar";
		this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button5.ImageKey = "exit.png";
		this.button5.ImageList = this.imageList1;
		this.button5.Location = new System.Drawing.Point(357, 219);
		this.button5.Name = "button5";
		this.button5.Size = new System.Drawing.Size(79, 35);
		this.button5.TabIndex = 5;
		this.button5.Text = "Salir";
		this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button5.UseVisualStyleBackColor = true;
		this.button5.Click += new System.EventHandler(button5_Click);
		this.groupBox1.Controls.Add(this.dataGridView1);
		this.groupBox1.Location = new System.Drawing.Point(12, 10);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(420, 198);
		this.groupBox1.TabIndex = 6;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "TipoPrecios";
		this.txtsigla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtsigla.Location = new System.Drawing.Point(111, 52);
		this.txtsigla.MaxLength = 50;
		this.txtsigla.Name = "txtsigla";
		this.txtsigla.Size = new System.Drawing.Size(197, 20);
		this.txtsigla.TabIndex = 0;
		this.superValidator1.SetValidator1(this.txtsigla, this.customValidator1);
		this.superValidator1.SetValidator2(this.txtsigla, this.requiredFieldValidator1);
		this.superValidator1.SetValidator3(this.txtsigla, this.compareValidator1);
		this.customValidator1.ErrorMessage = "Your error message here.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.requiredFieldValidator1.ErrorMessage = "Your error message here.";
		this.requiredFieldValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.compareValidator1.ErrorMessage = "Your error message here.";
		this.compareValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.txtcodigo);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.txtdescripcion);
		this.groupBox2.Controls.Add(this.txtsigla);
		this.groupBox2.Location = new System.Drawing.Point(56, 51);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(330, 119);
		this.groupBox2.TabIndex = 7;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Precios";
		this.groupBox2.Visible = false;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(53, 26);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(43, 13);
		this.label3.TabIndex = 8;
		this.label3.Text = "Codigo:";
		this.txtcodigo.Enabled = false;
		this.txtcodigo.Location = new System.Drawing.Point(111, 19);
		this.txtcodigo.MaxLength = 11;
		this.txtcodigo.Name = "txtcodigo";
		this.txtcodigo.Size = new System.Drawing.Size(39, 20);
		this.txtcodigo.TabIndex = 7;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(30, 92);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Descripcion:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(62, 59);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(33, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Sigla:";
		this.txtdescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtdescripcion.Location = new System.Drawing.Point(111, 89);
		this.txtdescripcion.MaxLength = 50;
		this.txtdescripcion.Name = "txtdescripcion";
		this.txtdescripcion.Size = new System.Drawing.Size(197, 20);
		this.txtdescripcion.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(443, 266);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.button5);
		base.Controls.Add(this.button4);
		base.Controls.Add(this.btEliminar);
		base.Controls.Add(this.btEditar);
		base.Controls.Add(this.btNuevo);
		this.Cursor = System.Windows.Forms.Cursors.Default;
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTipoPrecios";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Tipo Precios";
		base.Load += new System.EventHandler(frmTipoPrecios_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
