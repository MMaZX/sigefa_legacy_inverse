using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmDestaques : Office2007Form
{
	private clsAdmZona AdmZona = new clsAdmZona();

	private clsAdmVendedor AdmVen = new clsAdmVendedor();

	private clsValidar valida = new clsValidar();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public clsDestaque des = new clsDestaque();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvDestaques;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private Label label5;

	private TextBox txtCodigo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private ImageList imageList1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator3;

	private ComboBox cmbZona;

	private Label label8;

	private ComboBox cmbVendedores;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private CustomValidator customValidator1;

	private CustomValidator customValidator4;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codvendedor;

	private DataGridViewTextBoxColumn vendedor;

	private DataGridViewTextBoxColumn codzona;

	private DataGridViewTextBoxColumn zona;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private Label label6;

	public frmDestaques()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvDestaques.DataSource = data;
		data.DataSource = AdmZona.MuestraDestaques();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDestaques.ClearSelection();
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		ext.limpiar(groupBox2.Controls);
		superValidator1.Validate();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
		ext.limpiar(groupBox2.Controls);
	}

	private void frmDestaques_Load(object sender, EventArgs e)
	{
		CargaLista();
		CargaVendedores();
		CargaZonas();
		label2.Text = "Codigo";
		label3.Text = "codDestaque";
		if (Proceso == 3)
		{
			bloquearbotones();
		}
	}

	private void bloquearbotones()
	{
		btnNuevo.Visible = false;
		btnEliminar.Visible = false;
		btnReporte.Visible = false;
		btnGuardar.Text = "Aceptar";
		btnGuardar.ImageIndex = 6;
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDestaques.CurrentRow.Index != -1 && des.CodDestaque != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Destaque", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmZona.deletedestaque(des.CodDestaque))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Destaque", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
				CargaVendedores();
				CargaZonas();
			}
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

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0)
		{
			return;
		}
		des.CodVendedor = Convert.ToInt32(cmbVendedores.SelectedValue);
		des.CodZona = Convert.ToInt32(cmbZona.SelectedValue);
		if (Proceso == 1)
		{
			des.CodUser = frmLogin.iCodUser;
			if (AdmZona.insertdestaque(des))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Destaques", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
				CargaVendedores();
				CargaZonas();
				Proceso = 0;
			}
		}
	}

	private void dgvDestaques_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvDestaques.Rows.Count >= 1 && e.Row.Selected)
		{
			des.CodDestaque = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			btnEliminar.Enabled = true;
		}
		else if (dgvDestaques.Rows.Count == 0)
		{
			btnEliminar.Enabled = false;
		}
	}

	private void dgvDestaques_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvDestaques.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvDestaques.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
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

	private void dgvDestaques_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
		}
	}

	private void dgvDestaques_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 14;
		frm.ShowDialog();
	}

	private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.enteros(e);
	}

	private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
	{
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

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (c.SelectedIndex != -1)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
			{
				if (c.SelectedIndex != -1)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void CargaVendedores()
	{
		cmbVendedores.DataSource = AdmVen.MuestraVendedoresDestaque();
		cmbVendedores.DisplayMember = "apellido";
		cmbVendedores.ValueMember = "codusuario";
		cmbVendedores.SelectedIndex = -1;
	}

	private void CargaZonas()
	{
		cmbZona.DataSource = AdmZona.MuestraZonasDestaque();
		cmbZona.DisplayMember = "descripcion";
		cmbZona.ValueMember = "codZona";
		cmbZona.SelectedIndex = -1;
	}

	private void frmDestaques_Shown(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmDestaques));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvDestaques = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codvendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codzona = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cmbZona = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.cmbVendedores = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDestaques).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dgvDestaques);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 412);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Destaques";
		this.dgvDestaques.AllowUserToAddRows = false;
		this.dgvDestaques.AllowUserToDeleteRows = false;
		this.dgvDestaques.AllowUserToOrderColumns = true;
		this.dgvDestaques.AllowUserToResizeColumns = false;
		this.dgvDestaques.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDestaques.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDestaques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDestaques.Columns.AddRange(this.codigo, this.codvendedor, this.vendedor, this.codzona, this.zona, this.estado, this.coduser, this.fecha);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDestaques.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvDestaques.Location = new System.Drawing.Point(6, 45);
		this.dgvDestaques.MultiSelect = false;
		this.dgvDestaques.Name = "dgvDestaques";
		this.dgvDestaques.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDestaques.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvDestaques.RowHeadersVisible = false;
		this.dgvDestaques.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDestaques.Size = new System.Drawing.Size(464, 361);
		this.dgvDestaques.TabIndex = 8;
		this.dgvDestaques.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDestaques_CellDoubleClick);
		this.dgvDestaques.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDestaques_ColumnHeaderMouseClick);
		this.dgvDestaques.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDestaques_CellClick);
		this.dgvDestaques.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDestaques_RowStateChanged);
		this.codigo.DataPropertyName = "codDestaque";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.codvendedor.DataPropertyName = "codVendedor";
		this.codvendedor.HeaderText = "Cod. Vendedor";
		this.codvendedor.Name = "codvendedor";
		this.codvendedor.ReadOnly = true;
		this.codvendedor.Visible = false;
		this.vendedor.DataPropertyName = "vendedor";
		this.vendedor.HeaderText = "Vendedor";
		this.vendedor.Name = "vendedor";
		this.vendedor.ReadOnly = true;
		this.vendedor.Width = 150;
		this.codzona.DataPropertyName = "codZona";
		this.codzona.HeaderText = "Cod. Zona";
		this.codzona.Name = "codzona";
		this.codzona.ReadOnly = true;
		this.codzona.Visible = false;
		this.zona.DataPropertyName = "zona";
		this.zona.HeaderText = "Zona";
		this.zona.Name = "zona";
		this.zona.ReadOnly = true;
		this.zona.Width = 150;
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
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fecha.Visible = false;
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
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(12, 430);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 18;
		this.groupBox3.TabStop = false;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(403, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 10;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
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
		this.groupBox2.Controls.Add(this.cmbZona);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.cmbVendedores);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(89, 124);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(327, 166);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.cmbZona.FormattingEnabled = true;
		this.cmbZona.Location = new System.Drawing.Point(24, 117);
		this.cmbZona.Name = "cmbZona";
		this.cmbZona.Size = new System.Drawing.Size(267, 21);
		this.cmbZona.TabIndex = 16;
		this.cmbZona.Tag = "6";
		this.superValidator1.SetValidator1(this.cmbZona, this.customValidator6);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(21, 101);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(32, 13);
		this.label8.TabIndex = 15;
		this.label8.Text = "Zona";
		this.cmbVendedores.FormattingEnabled = true;
		this.cmbVendedores.Items.AddRange(new object[2] { "DNI", "RUC" });
		this.cmbVendedores.Location = new System.Drawing.Point(22, 77);
		this.cmbVendedores.Name = "cmbVendedores";
		this.cmbVendedores.Size = new System.Drawing.Size(269, 21);
		this.cmbVendedores.TabIndex = 5;
		this.cmbVendedores.Tag = "5";
		this.superValidator1.SetValidator1(this.cmbVendedores, this.customValidator5);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(19, 61);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(53, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Vendedor";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Código :";
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(20, 38);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(80, 20);
		this.txtCodigo.TabIndex = 1;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator6.ErrorMessage = "Seleccione el modelo.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator5.ErrorMessage = "Seleccione la marca.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.customValidator1.ErrorMessage = "Ingrese Año Fabricación.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator4.ErrorMessage = "Ingrese la Placa.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator2.ErrorMessage = "Ingrese la Licencia.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el DNI.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 490);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmDestaques";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Destaques";
		base.Load += new System.EventHandler(frmDestaques_Load);
		base.Shown += new System.EventHandler(frmDestaques_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDestaques).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
