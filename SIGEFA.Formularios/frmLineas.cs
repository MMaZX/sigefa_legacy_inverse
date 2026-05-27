using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmLineas : Office2007Form
{
	private clsAdmLinea AdmLin = new clsAdmLinea();

	public clsFamilia fam = new clsFamilia();

	private clsLinea lin = new clsLinea();

	public int FamiliaSeleccionada;

	public string Cabecera;

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int procede = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnGrupos;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvLineas;

	private GroupBox groupBox2;

	private Label label4;

	private Label label5;

	private TextBox txtDescripcion;

	private TextBox txtCodigo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private Label label6;

	private TextBox txtReferencia;

	private CustomValidator customValidator2;

	private Button btnSalir;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn familia;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	public frmLineas()
	{
		InitializeComponent();
	}

	private void frmLineas_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Codigo";
		label3.Text = "codLinea";
		groupBox1.Text = fam.Referencia + " " + fam.Descripcion + ": Lineas";
		if (procede == 1)
		{
			btnEditar.Visible = false;
			btnEliminar.Visible = false;
			btnGuardar.Visible = false;
			btnNuevo.Visible = false;
			btnReporte.Visible = false;
			btnSalir.Visible = false;
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		Proceso = 2;
		groupBox2.Text = "Editar Registro";
		txtCodigo.Text = lin.CodLinea.ToString();
		txtReferencia.Text = lin.Referencia;
		txtDescripcion.Text = lin.Descripcion;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtDescripcion.Text != ""))
		{
			return;
		}
		lin.CodFamilia = fam.CodFamilia;
		lin.Referencia = txtReferencia.Text;
		lin.Descripcion = txtDescripcion.Text;
		if (Proceso == 1)
		{
			lin.CodUser = frmLogin.iCodUser;
			if (AdmLin.insert(lin))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Linea", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
		}
		else if (Proceso == 2 && AdmLin.update(lin))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Linea", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
	}

	private void CargaLista()
	{
		dgvLineas.DataSource = data;
		data.DataSource = AdmLin.MuestraLineas(fam.CodFamilia);
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnGuardar.Visible = !Estado;
		btnGrupos.Visible = Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		txtCodigo.Text = "";
		txtReferencia.Text = "";
		txtDescripcion.Text = "";
		superValidator1.Validate();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label3.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void dgvLineas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvLineas.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvLineas.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvLineas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvLineas.Rows.Count >= 1 && e.Row.Selected)
		{
			lin.CodLinea = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			lin.CodFamilia = Convert.ToInt32(e.Row.Cells[familia.Name].Value);
			lin.Referencia = e.Row.Cells[referencia.Name].Value.ToString();
			lin.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			lin.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			lin.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			btnEditar.Enabled = true;
			btnGrupos.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvLineas.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnGrupos.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void btnGrupos_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmGrupo"] != null)
		{
			Application.OpenForms["frmGrupo"].Activate();
			return;
		}
		frmGrupo frm = new frmGrupo();
		frm.lin = lin;
		frm.ShowDialog();
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (groupBox1.Visible)
		{
			Close();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvLineas.CurrentRow.Index != -1 && lin.CodLinea != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Linea", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmLin.delete(lin.CodLinea))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Linea", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 5;
		frm.CodCotizacion = fam.CodFamilia;
		frm.ShowDialog();
	}

	private void frmLineas_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmLineas));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGrupos = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvLineas = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.familia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvLineas).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGrupos);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(12, 211);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 14;
		this.groupBox3.TabStop = false;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(402, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 13;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGrupos.ImageList = this.imageList1;
		this.btnGrupos.Location = new System.Drawing.Point(320, 10);
		this.btnGrupos.Name = "btnGrupos";
		this.btnGrupos.Size = new System.Drawing.Size(77, 32);
		this.btnGrupos.TabIndex = 12;
		this.btnGrupos.Text = "Grupos";
		this.btnGrupos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGrupos.UseVisualStyleBackColor = true;
		this.btnGrupos.Click += new System.EventHandler(btnGrupos_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(320, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 11;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 10);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 7;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(236, 10);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 9;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(155, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 8;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvLineas);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 198);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Linea";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(427, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(96, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(200, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvLineas.AllowUserToAddRows = false;
		this.dgvLineas.AllowUserToDeleteRows = false;
		this.dgvLineas.AllowUserToOrderColumns = true;
		this.dgvLineas.AllowUserToResizeColumns = false;
		this.dgvLineas.AllowUserToResizeRows = false;
		this.dgvLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvLineas.Columns.AddRange(this.codigo, this.familia, this.referencia, this.descripcion, this.estado, this.coduser, this.fecha);
		this.dgvLineas.Location = new System.Drawing.Point(6, 45);
		this.dgvLineas.MultiSelect = false;
		this.dgvLineas.Name = "dgvLineas";
		this.dgvLineas.ReadOnly = true;
		this.dgvLineas.RowHeadersVisible = false;
		this.dgvLineas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvLineas.Size = new System.Drawing.Size(464, 147);
		this.dgvLineas.TabIndex = 2;
		this.dgvLineas.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvLineas_ColumnHeaderMouseClick);
		this.dgvLineas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvLineas_RowStateChanged);
		this.codigo.DataPropertyName = "codLinea";
		dataGridViewCellStyle1.NullValue = null;
		this.codigo.DefaultCellStyle = dataGridViewCellStyle1;
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.familia.DataPropertyName = "codFamilia";
		this.familia.HeaderText = "Familia";
		this.familia.Name = "familia";
		this.familia.ReadOnly = true;
		this.familia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.familia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.familia.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.Width = 250;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.fecha.Visible = false;
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.txtReferencia);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(107, 58);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(298, 104);
		this.groupBox2.TabIndex = 18;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(102, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(65, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Referencia :";
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Location = new System.Drawing.Point(105, 32);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.Size = new System.Drawing.Size(153, 20);
		this.txtReferencia.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtReferencia, this.customValidator2);
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 55);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Descripción :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Código :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(20, 71);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 6;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator1);
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(20, 32);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(71, 20);
		this.txtCodigo.TabIndex = 4;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator2.ErrorMessage = "Ingrese la referencia.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Ingrese la descripción.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 271);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmLineas";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Lineas";
		base.Load += new System.EventHandler(frmLineas_Load);
		base.Shown += new System.EventHandler(frmLineas_Shown);
		this.groupBox3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvLineas).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
