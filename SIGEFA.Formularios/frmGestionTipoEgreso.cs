using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmGestionTipoEgreso : Office2007Form
{
	private clsTipoPagoCaja tipoPag = new clsTipoPagoCaja();

	private clsAdmTipoPagoCaja admTipoPag = new clsAdmTipoPagoCaja();

	private int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvTipoPagoCaja;

	private GroupBox groupBox2;

	private TextBox txtDescripcion;

	private Label label2;

	private TextBox txtCodigo;

	private Label label1;

	private GroupBox groupBox3;

	private Button btnGuardar;

	private Button btnReporte;

	private Button btnEliminar;

	private Button btnEditar;

	private Button btnNuevo;

	private Button btnSalir;

	private ImageList imageList1;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private Label label5;

	private TextBox txtFiltro;

	private Label label4;

	private Label label3;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private CustomValidator customValidator1;

	public frmGestionTipoEgreso()
	{
		InitializeComponent();
	}

	private void frmGestionTipoEgreso_Load(object sender, EventArgs e)
	{
		CargaLista();
		groupBox1.Height = 270;
		label4.Text = "Descripcion";
		label5.Text = "Descripcion";
	}

	private void CargaLista()
	{
		dgvTipoPagoCaja.DataSource = data;
		data.DataSource = admTipoPag.ListaTipoPagoCaja();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvTipoPagoCaja.ClearSelection();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label5.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
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

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		btnGuardar.Enabled = !Estado;
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
		txtDescripcion.Focus();
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		Proceso = 2;
		groupBox2.Text = "Editar Registro";
		txtCodigo.Text = tipoPag.CodTipoPagoServicio.ToString();
		txtDescripcion.Text = tipoPag.Descripcion;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator1.Validate() || Proceso == 0 || !(txtDescripcion.Text != ""))
		{
			return;
		}
		tipoPag.Descripcion = txtDescripcion.Text;
		if (Proceso == 1)
		{
			tipoPag.CodUser = frmLogin.iCodUser;
			if (admTipoPag.insert(tipoPag))
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Tipo Egreso Caja", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista();
			}
		}
		else if (Proceso == 2 && admTipoPag.update(tipoPag))
		{
			MessageBox.Show("Los datos se actualizaron correctamente", "Gestion Tipo Egreso Caja", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CambiarEstados(Estado: true);
			CargaLista();
		}
		Proceso = 0;
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

	private void dgvTipoPagoCaja_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label4.Text = dgvTipoPagoCaja.Columns[e.ColumnIndex].HeaderText;
		label5.Text = dgvTipoPagoCaja.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvTipoPagoCaja_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvTipoPagoCaja.Rows.Count >= 1 && e.Row.Selected)
		{
			tipoPag.CodTipoPagoServicio = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			tipoPag.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			tipoPag.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			tipoPag.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
		}
		else if (dgvTipoPagoCaja.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvTipoPagoCaja.CurrentRow.Index != -1 && tipoPag.CodTipoPagoServicio != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Bancos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admTipoPag.delete(tipoPag.CodTipoPagoServicio))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Bancos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("TipoEgresoCaja");
		foreach (DataGridViewColumn column in dgvTipoPagoCaja.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvTipoPagoCaja.Rows.Count; i++)
		{
			DataGridViewRow row = dgvTipoPagoCaja.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvTipoPagoCaja.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\TipoEgresoCajaRPT.xml", XmlWriteMode.WriteSchema);
		CRTipoEgresoCaja rpt = new CRTipoEgresoCaja();
		frmGestionTipoEgresoRP frm = new frmGestionTipoEgresoRP();
		rpt.SetDataSource(ds);
		frm.cRVTipoEgresoCaja.ReportSource = rpt;
		frm.Show();
	}

	private void frmGestionTipoEgreso_Shown(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionTipoEgreso));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.dgvTipoPagoCaja = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTipoPagoCaja).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dgvTipoPagoCaja);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(505, 115);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Tipo Egreso Caja";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(436, 20);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(12, 13);
		this.label5.TabIndex = 5;
		this.label5.Text = "x";
		this.label5.Visible = false;
		this.txtFiltro.Location = new System.Drawing.Point(176, 17);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(254, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(81, 18);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(17, 16);
		this.label4.TabIndex = 3;
		this.label4.Text = "X";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(7, 20);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(77, 13);
		this.label3.TabIndex = 2;
		this.label3.Text = "Buscar Por: ";
		this.dgvTipoPagoCaja.AllowUserToAddRows = false;
		this.dgvTipoPagoCaja.AllowUserToDeleteRows = false;
		this.dgvTipoPagoCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTipoPagoCaja.Columns.AddRange(this.codigo, this.descripcion, this.estado, this.coduser, this.fecha);
		this.dgvTipoPagoCaja.Location = new System.Drawing.Point(6, 43);
		this.dgvTipoPagoCaja.Name = "dgvTipoPagoCaja";
		this.dgvTipoPagoCaja.ReadOnly = true;
		this.dgvTipoPagoCaja.RowHeadersVisible = false;
		this.dgvTipoPagoCaja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTipoPagoCaja.Size = new System.Drawing.Size(471, 219);
		this.dgvTipoPagoCaja.TabIndex = 0;
		this.dgvTipoPagoCaja.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvTipoPagoCaja_ColumnHeaderMouseClick);
		this.dgvTipoPagoCaja.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvTipoPagoCaja_RowStateChanged);
		this.codigo.DataPropertyName = "codTipoPagoCaja";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 350;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaRegistro";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Visible = false;
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(68, 121);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(362, 91);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Nuevo Registro";
		this.groupBox2.Visible = false;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(78, 56);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(272, 20);
		this.txtDescripcion.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator1);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(6, 59);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Descripción:";
		this.txtCodigo.Location = new System.Drawing.Point(78, 28);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.ReadOnly = true;
		this.txtCodigo.Size = new System.Drawing.Size(52, 20);
		this.txtCodigo.TabIndex = 1;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(6, 31);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Código:";
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 276);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(505, 63);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnSalir.ImageKey = "exit.png";
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(404, 19);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 5;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnGuardar.ImageKey = "guardar-documento-icono-7840-48.png";
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(321, 20);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 4;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnReporte.ImageKey = "document-print.png";
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(237, 20);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 3;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnEliminar.ImageKey = "Remove Document.png";
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(156, 20);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 2;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnEditar.ImageKey = "Write Document.png";
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(84, 20);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 1;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
		this.btnNuevo.ImageKey = "New Document.png";
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(7, 20);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 0;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Ingrese Descripcion";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(505, 339);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionTipoEgreso";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion de Tipo Egreso Caja";
		base.Load += new System.EventHandler(frmGestionTipoEgreso_Load);
		base.Shown += new System.EventHandler(frmGestionTipoEgreso_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTipoPagoCaja).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
