using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmAutorizado : Office2007Form
{
	private clsAdmAutorizado AdmAut = new clsAdmAutorizado();

	public clsAutorizado aut = new clsAutorizado();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvAutorizados;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private Label label4;

	private Label label5;

	private TextBox txtDescripcion;

	private TextBox txtCodigo;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private ImageList imageList1;

	public frmAutorizado()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvAutorizados.DataSource = data;
		data.DataSource = AdmAut.MuestraAutorizados();
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnGuardar.Visible = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		txtCodigo.Text = "";
		txtDescripcion.Text = "";
		superValidator1.Validate();
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
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		txtCodigo.Text = aut.CodAutorizado.ToString();
		txtDescripcion.Text = aut.Nombre;
	}

	private void frmAutorizado_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Codigo";
		label3.Text = "codCaracteristica";
		if (Proceso == 3)
		{
			bloquearbotones();
		}
	}

	private void bloquearbotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
		btnReporte.Visible = false;
		btnGuardar.Text = "Aceptar";
		btnGuardar.ImageIndex = 6;
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvAutorizados.CurrentRow.Index != -1 && aut.CodAutorizado != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Autorizado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmAut.delete(aut.CodAutorizado))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Autorizado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
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
		if (!superValidator1.Validate() || Proceso == 0 || !(txtDescripcion.Text != ""))
		{
			return;
		}
		aut.Nombre = txtDescripcion.Text;
		if (Proceso == 1)
		{
			aut.CodUser = frmLogin.iCodUser;
			if (AdmAut.insert(aut))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Autorizado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
		}
		else if (Proceso == 2 && AdmAut.update(aut))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Autorizado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
	}

	private void dgvAutorizados_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvAutorizados.Rows.Count >= 1 && e.Row.Selected)
		{
			aut.CodAutorizado = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			aut.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
			aut.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			aut.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvAutorizados.Rows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void dgvAutorizados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvAutorizados.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvAutorizados.Columns[e.ColumnIndex].DataPropertyName;
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

	private void dgvAutorizados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
		}
	}

	private void dgvAutorizados_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("Autorizados");
		foreach (DataGridViewColumn column in dgvAutorizados.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvAutorizados.Rows.Count; i++)
		{
			DataGridViewRow row = dgvAutorizados.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvAutorizados.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmAutorizadoRP frm = new frmAutorizadoRP();
		frm.DTable = dt;
		frm.Show();
	}

	private void frmAutorizado_Shown(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmAutorizado));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvAutorizados = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvAutorizados).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dgvAutorizados);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 198);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Autorizado";
		this.dgvAutorizados.AllowUserToAddRows = false;
		this.dgvAutorizados.AllowUserToDeleteRows = false;
		this.dgvAutorizados.AllowUserToOrderColumns = true;
		this.dgvAutorizados.AllowUserToResizeColumns = false;
		this.dgvAutorizados.AllowUserToResizeRows = false;
		this.dgvAutorizados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvAutorizados.Columns.AddRange(this.codigo, this.nombre, this.estado, this.coduser, this.fecha);
		this.dgvAutorizados.Location = new System.Drawing.Point(6, 45);
		this.dgvAutorizados.MultiSelect = false;
		this.dgvAutorizados.Name = "dgvAutorizados";
		this.dgvAutorizados.ReadOnly = true;
		this.dgvAutorizados.RowHeadersVisible = false;
		this.dgvAutorizados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvAutorizados.Size = new System.Drawing.Size(464, 147);
		this.dgvAutorizados.TabIndex = 8;
		this.dgvAutorizados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvAutorizados_CellDoubleClick);
		this.dgvAutorizados.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvAutorizados_ColumnHeaderMouseClick);
		this.dgvAutorizados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvAutorizados_CellClick);
		this.dgvAutorizados.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvAutorizados_RowStateChanged);
		this.codigo.DataPropertyName = "codAutorizado";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.nombre.Width = 250;
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
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(12, 211);
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
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(107, 58);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(298, 104);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 55);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(107, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Personal Autorizado :";
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
		this.txtDescripcion.TabIndex = 5;
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
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAutorizado";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Autorizado";
		base.Load += new System.EventHandler(frmAutorizado_Load);
		base.Shown += new System.EventHandler(frmAutorizado_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvAutorizados).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
