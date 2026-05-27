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

public class frmUnidades : Office2007Form
{
	private clsAdmUnidad AdmUni = new clsAdmUnidad();

	private clsUnidadMedida uni = new clsUnidadMedida();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox1;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvUnidades;

	private Button btnSalir;

	private Button btnGuardar;

	private Button btnReporte;

	private Button btnEliminar;

	private Button btnEditar;

	private Button btnNuevo;

	private Label label2;

	private Label label3;

	private GroupBox groupBox3;

	private GroupBox groupBox2;

	private Label label4;

	private Label label5;

	private TextBox txtDescripcion;

	private TextBox txtSigla;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn sigla;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	public frmUnidades()
	{
		InitializeComponent();
	}

	private void frmUnidades_Load(object sender, EventArgs e)
	{
		CargaLista();
		label2.Text = "Sigla";
		label3.Text = "sigla";
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnReporte.Enabled = Estado;
		txtSigla.Text = "";
		txtDescripcion.Text = "";
		superValidator1.Validate();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (dgvUnidades.SelectedRows.Count > 0)
		{
			CambiarEstados(Estado: false);
			Proceso = 2;
			groupBox2.Text = "Editar Registro";
			txtSigla.Text = uni.Sigla;
			txtDescripcion.Text = uni.Descripcion;
		}
	}

	private void dgvUnidades_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvUnidades.Rows.Count >= 1 && e.Row.Selected)
		{
			uni.CodUnidad = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			uni.Sigla = e.Row.Cells[sigla.Name].Value.ToString();
			uni.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			uni.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			uni.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvUnidades.Rows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void CargaLista()
	{
		dgvUnidades.DataSource = data;
		data.DataSource = AdmUni.MuestraUnidades();
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void frmUnidades_Shown(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvUnidades_CellClick(object sender, DataGridViewCellEventArgs e)
	{
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

	private void dgvUnidades_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvUnidades.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvUnidades.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void button6_Click(object sender, EventArgs e)
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
		if (!superValidator1.Validate() || Proceso == 0 || !(txtSigla.Text != "") || !(txtDescripcion.Text != ""))
		{
			return;
		}
		uni.Sigla = txtSigla.Text;
		uni.Descripcion = txtDescripcion.Text;
		if (Proceso == 1)
		{
			uni.CodUser = frmLogin.iCodUser;
			if (AdmUni.insert(uni))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Unidad", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
		}
		else if (Proceso == 2 && AdmUni.update(uni))
		{
			MessageBox.Show("Los datos se guardaron correctamente", "Gestion Unidad", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvUnidades.CurrentRow.Index != -1 && uni.CodUnidad != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Unidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmUni.delete(uni.CodUnidad))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Unidad", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
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
		DataTable dt = new DataTable("Unidades");
		foreach (DataGridViewColumn column in dgvUnidades.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvUnidades.Rows.Count; i++)
		{
			DataGridViewRow row = dgvUnidades.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvUnidades.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmUnidadesRP frm = new frmUnidadesRP();
		frm.DTable = dt;
		frm.Show();
	}

	private void frmUnidades_Shown_1(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmUnidades));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dgvUnidades = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.txtSigla = new System.Windows.Forms.TextBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvUnidades).BeginInit();
		this.groupBox3.SuspendLayout();
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
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dgvUnidades);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 198);
		this.groupBox1.TabIndex = 14;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Unidades";
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
		this.txtFiltro.TabIndex = 1;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.dgvUnidades.AllowUserToAddRows = false;
		this.dgvUnidades.AllowUserToDeleteRows = false;
		this.dgvUnidades.AllowUserToOrderColumns = true;
		this.dgvUnidades.AllowUserToResizeColumns = false;
		this.dgvUnidades.AllowUserToResizeRows = false;
		this.dgvUnidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvUnidades.Columns.AddRange(this.codigo, this.sigla, this.descripcion, this.estado, this.coduser, this.fecha);
		this.dgvUnidades.Location = new System.Drawing.Point(6, 45);
		this.dgvUnidades.MultiSelect = false;
		this.dgvUnidades.Name = "dgvUnidades";
		this.dgvUnidades.ReadOnly = true;
		this.dgvUnidades.RowHeadersVisible = false;
		this.dgvUnidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvUnidades.Size = new System.Drawing.Size(464, 147);
		this.dgvUnidades.TabIndex = 2;
		this.dgvUnidades.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvUnidades_ColumnHeaderMouseClick);
		this.dgvUnidades.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvUnidades_RowStateChanged);
		this.codigo.DataPropertyName = "codUnidadMedida";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.sigla.DataPropertyName = "sigla";
		this.sigla.HeaderText = "Sigla";
		this.sigla.Name = "sigla";
		this.sigla.ReadOnly = true;
		this.sigla.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.sigla.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.sigla.Width = 80;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
		this.descripcion.Width = 250;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(403, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 11;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(button6_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(320, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 10;
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
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Location = new System.Drawing.Point(12, 211);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(476, 48);
		this.groupBox3.TabIndex = 12;
		this.groupBox3.TabStop = false;
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtSigla);
		this.groupBox2.Location = new System.Drawing.Point(107, 58);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(298, 104);
		this.groupBox2.TabIndex = 16;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(17, 55);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Descripcion :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(36, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Sigla :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(20, 71);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator2);
		this.txtSigla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSigla.Location = new System.Drawing.Point(20, 32);
		this.txtSigla.Name = "txtSigla";
		this.txtSigla.Size = new System.Drawing.Size(71, 20);
		this.txtSigla.TabIndex = 4;
		this.superValidator1.SetValidator1(this.txtSigla, this.customValidator1);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator2.ErrorMessage = "Ingrese la descripción.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Debe ingresar la sigla.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 271);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmUnidades";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Unidades";
		base.Load += new System.EventHandler(frmUnidades_Load);
		base.Shown += new System.EventHandler(frmUnidades_Shown_1);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvUnidades).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
