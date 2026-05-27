using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmCompQuimicaDosis : Office2007Form
{
	private clsComposicionQuimica compQuim = new clsComposicionQuimica();

	private clsDosis dosi = new clsDosis();

	private clsAdmComposicionQuimica admCompQuim = new clsAdmComposicionQuimica();

	private clsAdmDosis admDos = new clsAdmDosis();

	public static BindingSource dataComp = new BindingSource();

	public static BindingSource dataDisis = new BindingSource();

	public int codPro = 0;

	private IContainer components = null;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private DataGridView dgvComponente;

	private Label label2;

	private TextBox txtContenido;

	private Label label4;

	private TextBox txtDescripcion;

	private Button btnAgregar;

	private ImageList imageList1;

	private Button btnEliminar;

	private Button btnSalir;

	private DataGridView dgvDosis;

	private Button button1;

	private Button button2;

	private Label label1;

	private TextBox textBox1;

	private Label label3;

	private TextBox textBox2;

	private DataGridView dataGridView1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private Button button3;

	private Button button4;

	private Label label5;

	private TextBox textBox3;

	private Label label6;

	private TextBox textBox4;

	private DataGridView dataGridView2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn codComposicion;

	private DataGridViewTextBoxColumn codProducto;

	private DataGridViewTextBoxColumn componente;

	private DataGridViewTextBoxColumn contenido;

	private TextBox txtCultivo;

	private Label label7;

	private Label label9;

	private TextBox txtDosis;

	private Label label8;

	private TextBox txtAplicacion;

	private Button btmAgrDosis;

	private Button btnEliDosis;

	private Label label11;

	private TextBox txtPc;

	private Label label10;

	private TextBox txtLmr;

	private DataGridViewTextBoxColumn codDosis;

	private DataGridViewTextBoxColumn dosCodProducto;

	private DataGridViewTextBoxColumn cultivo;

	private DataGridViewTextBoxColumn aplicacion;

	private DataGridViewTextBoxColumn dosis;

	private DataGridViewTextBoxColumn lmrppm;

	private DataGridViewTextBoxColumn pc;

	public frmCompQuimicaDosis()
	{
		InitializeComponent();
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void CargaLista()
	{
		dgvComponente.DataSource = dataComp;
		dataComp.DataSource = admCompQuim.ListaComposicion(codPro);
		dgvComponente.ClearSelection();
	}

	private void CargaListaDosis()
	{
		dgvDosis.DataSource = dataDisis;
		dataDisis.DataSource = admDos.ListaDosis(codPro);
		dgvDosis.ClearSelection();
	}

	private void frmCompQuimicaDosis_Load(object sender, EventArgs e)
	{
		CargaLista();
		CargaListaDosis();
	}

	private void dgvComponente_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvComponente.Rows.Count >= 1 && e.Row.Selected)
		{
			compQuim.CodComposion = Convert.ToInt32(e.Row.Cells[codComposicion.Name].Value);
			btnEliminar.Enabled = true;
		}
		else if (dgvComponente.SelectedRows.Count == 0)
		{
			btnEliminar.Enabled = false;
		}
	}

	private void btnAgregar_Click(object sender, EventArgs e)
	{
		if (txtDescripcion.Text != "" && txtContenido.Text != "")
		{
			compQuim.CodProducto = codPro;
			compQuim.Componente = txtDescripcion.Text;
			compQuim.Cantidad = txtContenido.Text;
			if (admCompQuim.insert(compQuim))
			{
				txtDescripcion.Text = "";
				txtContenido.Text = "";
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("Verifique que el componente y el contenido no esten vacios", "Composición Química", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtDescripcion.Focus();
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvComponente.SelectedRows.Count == 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Composición Química", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admCompQuim.delete(compQuim.CodComposion))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Composición Química", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
		else
		{
			MessageBox.Show("Seleccione una Composición", "Composición Química", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Dispose();
		Close();
	}

	private void label2_Click(object sender, EventArgs e)
	{
	}

	private void txtContenido_TextChanged(object sender, EventArgs e)
	{
	}

	private void label7_Click(object sender, EventArgs e)
	{
	}

	private void btmAgrDosis_Click(object sender, EventArgs e)
	{
		if (txtCultivo.Text != "" && txtAplicacion.Text != "" && txtDosis.Text != "")
		{
			dosi.CodProducto = codPro;
			dosi.Cultivo = txtCultivo.Text;
			dosi.Aplicacion = txtAplicacion.Text;
			dosi.Dosis = txtDosis.Text;
			dosi.Lmrppm = txtLmr.Text;
			dosi.Pc = txtPc.Text;
			if (admDos.insert(dosi))
			{
				txtCultivo.Text = "";
				txtAplicacion.Text = "";
				txtDosis.Text = "";
				txtLmr.Text = "";
				txtPc.Text = "";
				CargaListaDosis();
			}
		}
		else
		{
			MessageBox.Show("Verifique que al menos el Cultivo-Forma de Aplicacion y Dosis no esten vacios", "Dosis", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtDescripcion.Focus();
		}
	}

	private void btnEliDosis_Click(object sender, EventArgs e)
	{
		if (dgvDosis.SelectedRows.Count == 1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Dosis", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admDos.delete(dosi.CodDosis))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Dosis", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListaDosis();
			}
		}
		else
		{
			MessageBox.Show("Seleccione una Dosis", "Dosis", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void dgvDosis_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvDosis.Rows.Count >= 1 && e.Row.Selected)
		{
			dosi.CodDosis = Convert.ToInt32(e.Row.Cells[codDosis.Name].Value);
			btnEliDosis.Enabled = true;
		}
		else if (dgvDosis.SelectedRows.Count == 0)
		{
			btnEliDosis.Enabled = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCompQuimicaDosis));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.btnAgregar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnEliminar = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.txtContenido = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.dgvComponente = new System.Windows.Forms.DataGridView();
		this.codComposicion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.componente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contenido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.btmAgrDosis = new System.Windows.Forms.Button();
		this.btnEliDosis = new System.Windows.Forms.Button();
		this.label11 = new System.Windows.Forms.Label();
		this.txtPc = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtLmr = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtDosis = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtAplicacion = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtCultivo = new System.Windows.Forms.TextBox();
		this.dgvDosis = new System.Windows.Forms.DataGridView();
		this.btnSalir = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.button3 = new System.Windows.Forms.Button();
		this.button4 = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.dataGridView2 = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codDosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dosCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cultivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.aplicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lmrppm = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvComponente).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDosis).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).BeginInit();
		base.SuspendLayout();
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(12, 12);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(821, 387);
		this.tabControl1.TabIndex = 1;
		this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
		this.tabPage1.Controls.Add(this.btnAgregar);
		this.tabPage1.Controls.Add(this.btnEliminar);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.txtContenido);
		this.tabPage1.Controls.Add(this.label4);
		this.tabPage1.Controls.Add(this.txtDescripcion);
		this.tabPage1.Controls.Add(this.dgvComponente);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(813, 361);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Composición Quimica";
		this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnAgregar.ImageIndex = 7;
		this.btnAgregar.ImageList = this.imageList1;
		this.btnAgregar.Location = new System.Drawing.Point(644, 39);
		this.btnAgregar.Name = "btnAgregar";
		this.btnAgregar.Size = new System.Drawing.Size(77, 32);
		this.btnAgregar.TabIndex = 12;
		this.btnAgregar.Text = "Agregar";
		this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAgregar.UseVisualStyleBackColor = true;
		this.btnAgregar.Click += new System.EventHandler(btnAgregar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "format_increaseindent.png");
		this.imageList1.Images.SetKeyName(7, "+.gif");
		this.imageList1.Images.SetKeyName(8, "delete.gif");
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 8;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(731, 39);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(76, 32);
		this.btnEliminar.TabIndex = 13;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(447, 17);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(61, 13);
		this.label2.TabIndex = 11;
		this.label2.Text = "Contenido :";
		this.label2.Click += new System.EventHandler(label2_Click);
		this.txtContenido.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtContenido.Location = new System.Drawing.Point(450, 33);
		this.txtContenido.Name = "txtContenido";
		this.txtContenido.Size = new System.Drawing.Size(113, 20);
		this.txtContenido.TabIndex = 10;
		this.txtContenido.TextChanged += new System.EventHandler(txtContenido_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(7, 17);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(73, 13);
		this.label4.TabIndex = 9;
		this.label4.Text = "Componente :";
		this.label4.Click += new System.EventHandler(label4_Click);
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(10, 33);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(383, 20);
		this.txtDescripcion.TabIndex = 8;
		this.dgvComponente.AllowUserToAddRows = false;
		this.dgvComponente.AllowUserToDeleteRows = false;
		this.dgvComponente.AllowUserToOrderColumns = true;
		this.dgvComponente.AllowUserToResizeColumns = false;
		this.dgvComponente.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvComponente.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvComponente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvComponente.Columns.AddRange(this.codComposicion, this.codProducto, this.componente, this.contenido);
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvComponente.DefaultCellStyle = dataGridViewCellStyle3;
		this.dgvComponente.Location = new System.Drawing.Point(6, 77);
		this.dgvComponente.MultiSelect = false;
		this.dgvComponente.Name = "dgvComponente";
		this.dgvComponente.ReadOnly = true;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvComponente.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvComponente.RowHeadersVisible = false;
		this.dgvComponente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvComponente.Size = new System.Drawing.Size(801, 278);
		this.dgvComponente.TabIndex = 3;
		this.dgvComponente.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvComponente_RowStateChanged);
		this.codComposicion.DataPropertyName = "codComposicion";
		dataGridViewCellStyle5.NullValue = null;
		this.codComposicion.DefaultCellStyle = dataGridViewCellStyle5;
		this.codComposicion.HeaderText = "Codigo";
		this.codComposicion.Name = "codComposicion";
		this.codComposicion.ReadOnly = true;
		this.codComposicion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codComposicion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codComposicion.Visible = false;
		this.codProducto.DataPropertyName = "codProducto";
		this.codProducto.HeaderText = "CodProd";
		this.codProducto.Name = "codProducto";
		this.codProducto.ReadOnly = true;
		this.codProducto.Visible = false;
		this.componente.DataPropertyName = "componente";
		this.componente.HeaderText = "Componente";
		this.componente.Name = "componente";
		this.componente.ReadOnly = true;
		this.componente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.componente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.componente.Width = 580;
		this.contenido.DataPropertyName = "contenido";
		this.contenido.HeaderText = "Contenido";
		this.contenido.Name = "contenido";
		this.contenido.ReadOnly = true;
		this.contenido.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.contenido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.contenido.Width = 200;
		this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
		this.tabPage2.Controls.Add(this.btmAgrDosis);
		this.tabPage2.Controls.Add(this.btnEliDosis);
		this.tabPage2.Controls.Add(this.label11);
		this.tabPage2.Controls.Add(this.txtPc);
		this.tabPage2.Controls.Add(this.label10);
		this.tabPage2.Controls.Add(this.txtLmr);
		this.tabPage2.Controls.Add(this.label9);
		this.tabPage2.Controls.Add(this.txtDosis);
		this.tabPage2.Controls.Add(this.label8);
		this.tabPage2.Controls.Add(this.txtAplicacion);
		this.tabPage2.Controls.Add(this.label7);
		this.tabPage2.Controls.Add(this.txtCultivo);
		this.tabPage2.Controls.Add(this.dgvDosis);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(813, 361);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Dosis";
		this.btmAgrDosis.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btmAgrDosis.ImageIndex = 7;
		this.btmAgrDosis.ImageList = this.imageList1;
		this.btmAgrDosis.Location = new System.Drawing.Point(634, 68);
		this.btmAgrDosis.Name = "btmAgrDosis";
		this.btmAgrDosis.Size = new System.Drawing.Size(77, 32);
		this.btmAgrDosis.TabIndex = 19;
		this.btmAgrDosis.Text = "Agregar";
		this.btmAgrDosis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btmAgrDosis.UseVisualStyleBackColor = true;
		this.btmAgrDosis.Click += new System.EventHandler(btmAgrDosis_Click);
		this.btnEliDosis.Enabled = false;
		this.btnEliDosis.ImageIndex = 8;
		this.btnEliDosis.ImageList = this.imageList1;
		this.btnEliDosis.Location = new System.Drawing.Point(721, 68);
		this.btnEliDosis.Name = "btnEliDosis";
		this.btnEliDosis.Size = new System.Drawing.Size(76, 32);
		this.btnEliDosis.TabIndex = 20;
		this.btnEliDosis.Text = "Eliminar";
		this.btnEliDosis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliDosis.UseVisualStyleBackColor = true;
		this.btnEliDosis.Click += new System.EventHandler(btnEliDosis_Click);
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(333, 63);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(33, 13);
		this.label11.TabIndex = 18;
		this.label11.Text = "P.C. :";
		this.txtPc.Location = new System.Drawing.Point(334, 79);
		this.txtPc.Name = "txtPc";
		this.txtPc.Size = new System.Drawing.Size(136, 20);
		this.txtPc.TabIndex = 17;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(182, 63);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(66, 13);
		this.label10.TabIndex = 16;
		this.label10.Text = "L.M.R Ppm :";
		this.txtLmr.Location = new System.Drawing.Point(183, 79);
		this.txtLmr.Name = "txtLmr";
		this.txtLmr.Size = new System.Drawing.Size(136, 20);
		this.txtLmr.TabIndex = 15;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(6, 63);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(39, 13);
		this.label9.TabIndex = 14;
		this.label9.Text = "Dosis :";
		this.txtDosis.Location = new System.Drawing.Point(7, 79);
		this.txtDosis.Name = "txtDosis";
		this.txtDosis.Size = new System.Drawing.Size(136, 20);
		this.txtDosis.TabIndex = 13;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(261, 11);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(119, 13);
		this.label8.TabIndex = 12;
		this.label8.Text = "Formas de aplicación : :";
		this.txtAplicacion.Location = new System.Drawing.Point(262, 27);
		this.txtAplicacion.Multiline = true;
		this.txtAplicacion.Name = "txtAplicacion";
		this.txtAplicacion.Size = new System.Drawing.Size(535, 33);
		this.txtAplicacion.TabIndex = 11;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(6, 11);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(45, 13);
		this.label7.TabIndex = 10;
		this.label7.Text = "Cultivo :";
		this.label7.Click += new System.EventHandler(label7_Click);
		this.txtCultivo.Location = new System.Drawing.Point(7, 27);
		this.txtCultivo.Name = "txtCultivo";
		this.txtCultivo.Size = new System.Drawing.Size(220, 20);
		this.txtCultivo.TabIndex = 4;
		this.dgvDosis.AllowUserToAddRows = false;
		this.dgvDosis.AllowUserToDeleteRows = false;
		this.dgvDosis.AllowUserToOrderColumns = true;
		this.dgvDosis.AllowUserToResizeColumns = false;
		this.dgvDosis.AllowUserToResizeRows = false;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDosis.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvDosis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDosis.Columns.AddRange(this.codDosis, this.dosCodProducto, this.cultivo, this.aplicacion, this.dosis, this.lmrppm, this.pc);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDosis.DefaultCellStyle = dataGridViewCellStyle7;
		this.dgvDosis.Location = new System.Drawing.Point(3, 106);
		this.dgvDosis.MultiSelect = false;
		this.dgvDosis.Name = "dgvDosis";
		this.dgvDosis.ReadOnly = true;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDosis.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.dgvDosis.RowHeadersVisible = false;
		this.dgvDosis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDosis.Size = new System.Drawing.Size(804, 249);
		this.dgvDosis.TabIndex = 3;
		this.dgvDosis.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDosis_RowStateChanged);
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(765, 405);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 14;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.button1.ImageIndex = 7;
		this.button1.ImageList = this.imageList1;
		this.button1.Location = new System.Drawing.Point(413, 39);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(77, 32);
		this.button1.TabIndex = 12;
		this.button1.Text = "Agregar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button2.Enabled = false;
		this.button2.ImageIndex = 8;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(500, 39);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(76, 32);
		this.button2.TabIndex = 13;
		this.button2.Text = "Eliminar";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(230, 17);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(61, 13);
		this.label1.TabIndex = 11;
		this.label1.Text = "Contenido :";
		this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.textBox1.Location = new System.Drawing.Point(233, 33);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(66, 20);
		this.textBox1.TabIndex = 10;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(7, 17);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(73, 13);
		this.label3.TabIndex = 9;
		this.label3.Text = "Componente :";
		this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.textBox2.Location = new System.Drawing.Point(10, 33);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(196, 20);
		this.textBox2.TabIndex = 8;
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.AllowUserToOrderColumns = true;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.dataGridViewTextBoxColumn1, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4);
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
		this.dataGridView1.Location = new System.Drawing.Point(6, 77);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(570, 219);
		this.dataGridView1.TabIndex = 3;
		this.dataGridViewTextBoxColumn1.DataPropertyName = "codComposicion";
		dataGridViewCellStyle13.NullValue = null;
		this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle13;
		this.dataGridViewTextBoxColumn1.HeaderText = "Codigo";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Visible = false;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "codProducto";
		this.dataGridViewTextBoxColumn2.HeaderText = "CodProd";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Visible = false;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "componente";
		this.dataGridViewTextBoxColumn3.HeaderText = "Componente";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.ReadOnly = true;
		this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 380;
		this.dataGridViewTextBoxColumn4.DataPropertyName = "contenido";
		this.dataGridViewTextBoxColumn4.HeaderText = "Contenido";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.ReadOnly = true;
		this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.Width = 150;
		this.button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.button3.ImageIndex = 7;
		this.button3.ImageList = this.imageList1;
		this.button3.Location = new System.Drawing.Point(413, 39);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(77, 32);
		this.button3.TabIndex = 12;
		this.button3.Text = "Agregar";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = true;
		this.button4.Enabled = false;
		this.button4.ImageIndex = 8;
		this.button4.ImageList = this.imageList1;
		this.button4.Location = new System.Drawing.Point(500, 39);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(76, 32);
		this.button4.TabIndex = 13;
		this.button4.Text = "Eliminar";
		this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button4.UseVisualStyleBackColor = true;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(230, 17);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(61, 13);
		this.label5.TabIndex = 11;
		this.label5.Text = "Contenido :";
		this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.textBox3.Location = new System.Drawing.Point(233, 33);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(66, 20);
		this.textBox3.TabIndex = 10;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(7, 17);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(73, 13);
		this.label6.TabIndex = 9;
		this.label6.Text = "Componente :";
		this.textBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.textBox4.Location = new System.Drawing.Point(10, 33);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(196, 20);
		this.textBox4.TabIndex = 8;
		this.dataGridView2.AllowUserToAddRows = false;
		this.dataGridView2.AllowUserToDeleteRows = false;
		this.dataGridView2.AllowUserToOrderColumns = true;
		this.dataGridView2.AllowUserToResizeColumns = false;
		this.dataGridView2.AllowUserToResizeRows = false;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
		this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView2.Columns.AddRange(this.dataGridViewTextBoxColumn5, this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn8);
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle15;
		this.dataGridView2.Location = new System.Drawing.Point(6, 77);
		this.dataGridView2.MultiSelect = false;
		this.dataGridView2.Name = "dataGridView2";
		this.dataGridView2.ReadOnly = true;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
		this.dataGridView2.RowHeadersVisible = false;
		this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView2.Size = new System.Drawing.Size(570, 219);
		this.dataGridView2.TabIndex = 3;
		this.dataGridViewTextBoxColumn5.DataPropertyName = "codComposicion";
		dataGridViewCellStyle17.NullValue = null;
		this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle17;
		this.dataGridViewTextBoxColumn5.HeaderText = "Codigo";
		this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
		this.dataGridViewTextBoxColumn5.ReadOnly = true;
		this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn5.Visible = false;
		this.dataGridViewTextBoxColumn6.DataPropertyName = "codProducto";
		this.dataGridViewTextBoxColumn6.HeaderText = "CodProd";
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.dataGridViewTextBoxColumn6.ReadOnly = true;
		this.dataGridViewTextBoxColumn6.Visible = false;
		this.dataGridViewTextBoxColumn7.DataPropertyName = "componente";
		this.dataGridViewTextBoxColumn7.HeaderText = "Componente";
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.ReadOnly = true;
		this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn7.Width = 380;
		this.dataGridViewTextBoxColumn8.DataPropertyName = "contenido";
		this.dataGridViewTextBoxColumn8.HeaderText = "Contenido";
		this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
		this.dataGridViewTextBoxColumn8.ReadOnly = true;
		this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn8.Width = 150;
		this.codDosis.DataPropertyName = "codDosis";
		dataGridViewCellStyle18.NullValue = null;
		this.codDosis.DefaultCellStyle = dataGridViewCellStyle18;
		this.codDosis.HeaderText = "Codigo";
		this.codDosis.Name = "codDosis";
		this.codDosis.ReadOnly = true;
		this.codDosis.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codDosis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codDosis.Visible = false;
		this.codDosis.Width = 5;
		this.dosCodProducto.DataPropertyName = "codProducto";
		this.dosCodProducto.HeaderText = "codProducto";
		this.dosCodProducto.Name = "dosCodProducto";
		this.dosCodProducto.ReadOnly = true;
		this.dosCodProducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dosCodProducto.Visible = false;
		this.cultivo.DataPropertyName = "cultivo";
		this.cultivo.HeaderText = "Cultivo";
		this.cultivo.Name = "cultivo";
		this.cultivo.ReadOnly = true;
		this.cultivo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cultivo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cultivo.Width = 200;
		this.aplicacion.DataPropertyName = "aplicacion";
		this.aplicacion.HeaderText = "Formas de Aplicación";
		this.aplicacion.Name = "aplicacion";
		this.aplicacion.ReadOnly = true;
		this.aplicacion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.aplicacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.aplicacion.Width = 300;
		this.dosis.DataPropertyName = "dosis";
		this.dosis.HeaderText = "Dosis";
		this.dosis.Name = "dosis";
		this.dosis.ReadOnly = true;
		this.dosis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dosis.Width = 150;
		this.lmrppm.DataPropertyName = "lmrppm";
		this.lmrppm.HeaderText = "L.M.R Ppm";
		this.lmrppm.Name = "lmrppm";
		this.lmrppm.ReadOnly = true;
		this.pc.DataPropertyName = "pc";
		this.pc.HeaderText = "P.C.";
		this.pc.Name = "pc";
		this.pc.ReadOnly = true;
		this.pc.Width = 50;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(845, 438);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.tabControl1);
		this.DoubleBuffered = true;
		base.Name = "frmCompQuimicaDosis";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Composición Química";
		base.Load += new System.EventHandler(frmCompQuimicaDosis_Load);
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvComponente).EndInit();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDosis).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView2).EndInit();
		base.ResumeLayout(false);
	}
}
