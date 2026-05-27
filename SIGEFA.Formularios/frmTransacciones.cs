using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmTransacciones : Office2007Form
{
	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	public clsTransaccion tran = new clsTransaccion();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public List<int> config = new List<int>();

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvTransacciones;

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

	private TextBox txtSigla;

	private TextBox txtDescripcion;

	private Label label5;

	private Label label4;

	private GroupBox groupBox2;

	private ComboBox cbTipo;

	private Label label6;

	private System.Windows.Forms.TabControl tabControl1;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private CheckBox checkBox4;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	private CheckBox checkBox17;

	private CheckBox checkBox18;

	private CheckBox checkBox15;

	private CheckBox checkBox16;

	private CheckBox checkBox13;

	private CheckBox checkBox14;

	private CheckBox checkBox12;

	private CheckBox checkBox11;

	private CheckBox checkBox10;

	private CheckBox checkBox9;

	private CheckBox checkBox8;

	private CheckBox checkBox7;

	private CheckBox checkBox6;

	private CheckBox checkBox5;

	private CheckBox checkBox22;

	private CheckBox checkBox19;

	private CheckBox checkBox20;

	private CheckBox checkBox21;

	private CheckBox checkBox23;

	private CheckBox checkBox26;

	private CheckBox checkBox27;

	private CheckBox checkBox28;

	private CheckBox checkBox29;

	private CheckBox checkBox30;

	private CheckBox checkBox31;

	private CheckBox checkBox32;

	private CheckBox checkBox25;

	private CheckBox checkBox24;

	private CheckBox checkBox34;

	private CheckBox checkBox33;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator1;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn sigla;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn tipotext;

	private ImageList imageList1;

	private CustomValidator customValidator2;

	public frmTransacciones()
	{
		InitializeComponent();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
		LimpiaConfiguracion();
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
		cbTipo.SelectedIndex = -1;
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

	private void frmTransacciones_Load(object sender, EventArgs e)
	{
		if (Proceso == 3 || Proceso == 4)
		{
			CargaLista(0);
			tipotext.Visible = false;
			bloquearbotones();
		}
		else
		{
			CargaLista(2);
			label2.Text = "Sigla";
			label3.Text = "sigla";
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

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (dgvTransacciones.Rows.Count >= 1 && dgvTransacciones.SelectedRows.Count > 0)
		{
			CambiarEstados(Estado: false);
			groupBox2.Text = "Editar Registro";
			Proceso = 2;
			txtSigla.Text = tran.Sigla;
			txtDescripcion.Text = tran.Descripcion;
			cbTipo.SelectedIndex = tran.Tipo;
			CargaConfiguracion();
		}
	}

	private void CargaLista(int caso)
	{
		try
		{
			if (data.DataSource != null)
			{
				DataTable dt = (DataTable)data.DataSource;
				dt.Clear();
			}
			dgvTransacciones.DataSource = data;
			data.DataSource = AdmTran.MuestraTransacciones(caso);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvTransacciones.ClearSelection();
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
		}
	}

	private void CargaConfiguracion()
	{
		foreach (CheckBox c in tabPage1.Controls)
		{
			int con = Convert.ToInt32(c.TabIndex);
			if (tran.Configuracion.Contains(con))
			{
				c.Checked = true;
			}
			else
			{
				c.Checked = false;
			}
		}
		foreach (CheckBox c2 in tabPage2.Controls)
		{
			int con2 = Convert.ToInt32(c2.TabIndex);
			if (tran.Configuracion.Contains(con2))
			{
				c2.Checked = true;
			}
			else
			{
				c2.Checked = false;
			}
		}
	}

	private void frmTransacciones_Shown(object sender, EventArgs e)
	{
		txtFiltro.Focus();
		if (Proceso == 3)
		{
			CargaLista(0);
			tipotext.Visible = false;
		}
		else if (Proceso == 4)
		{
			CargaLista(1);
			tipotext.Visible = false;
		}
		else
		{
			CargaLista(2);
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
		if (btnGuardar.Text == "Guardar")
		{
			if (!superValidator1.Validate() || Proceso == 0 || !(txtSigla.Text != "") || !(txtDescripcion.Text != "") || cbTipo.SelectedIndex == -1)
			{
				return;
			}
			tran.Sigla = txtSigla.Text;
			tran.Descripcion = txtDescripcion.Text;
			tran.Tipo = cbTipo.SelectedIndex;
			RecorreConfiguracion();
			if (Proceso == 1)
			{
				tran.CodUser = frmLogin.iCodUser;
				if (AdmTran.insert(tran))
				{
					GuardaConfig();
					MessageBox.Show("Los datos se guardaron correctamente", "Gestion Transaccion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CambiarEstados(Estado: true);
					CargaLista(2);
				}
			}
			else if (Proceso == 2 && AdmTran.update(tran))
			{
				GuardaConfig();
				MessageBox.Show("Los datos se guardaron correctamente", "Gestion Transacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CambiarEstados(Estado: true);
				CargaLista(2);
			}
			Proceso = 0;
		}
		else if (btnGuardar.Text == "Aceptar" && (Proceso == 3 || Proceso == 4))
		{
			Close();
		}
	}

	private void GuardaConfig()
	{
		AdmTran.LimpiarConfiguracion(tran.CodTransaccion);
		if (config.Count <= 0)
		{
			return;
		}
		foreach (int con in tran.Configuracion)
		{
			AdmTran.insertConf(tran.CodTransaccion, con, frmLogin.iCodUser);
		}
	}

	private void dgvTransacciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvTransacciones.Rows.Count >= 1 && e.Row.Selected)
		{
			tran.CodTransaccion = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
			tran.Sigla = e.Row.Cells[sigla.Name].Value.ToString();
			tran.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
			tran.Tipo = Convert.ToInt32(e.Row.Cells[tipo.Name].Value);
			tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
			tran.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
			tran.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
			btnEditar.Enabled = true;
			btnEliminar.Enabled = true;
			if (Proceso == 3 || Proceso == 4)
			{
				btnGuardar.Enabled = true;
			}
		}
		else if (dgvTransacciones.SelectedRows.Count == 0)
		{
			btnEditar.Enabled = false;
			btnEliminar.Enabled = false;
		}
	}

	private void RecorreConfiguracion()
	{
		config.Clear();
		foreach (CheckBox c in tabPage1.Controls)
		{
			if (c.Checked)
			{
				config.Add(Convert.ToInt32(c.TabIndex));
			}
		}
		foreach (CheckBox c2 in tabPage2.Controls)
		{
			if (c2.Checked)
			{
				config.Add(Convert.ToInt32(c2.TabIndex));
			}
		}
		tran.Configuracion = config;
	}

	private void LimpiaConfiguracion()
	{
		config.Clear();
		foreach (CheckBox c in tabPage1.Controls)
		{
			c.Checked = false;
		}
		foreach (CheckBox c2 in tabPage2.Controls)
		{
			c2.Checked = false;
		}
		tabControl1.SelectedIndex = 0;
	}

	private void dgvTransacciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3 || Proceso == 4)
		{
			Close();
		}
	}

	private void dgvTransacciones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvTransacciones.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvTransacciones.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
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

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvTransacciones.CurrentRow.Index != -1 && tran.CodTransaccion != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Transaccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmTran.delete(tran.CodTransaccion))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Transaccion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista(2);
			}
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataTable dt = new DataTable("Transacciones");
		foreach (DataGridViewColumn column in dgvTransacciones.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvTransacciones.Rows.Count; i++)
		{
			DataGridViewRow row = dgvTransacciones.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvTransacciones.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		frmTransaccionesRP frm = new frmTransaccionesRP();
		frm.DTable = dt;
		frm.Show();
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTransacciones));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvTransacciones = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipotext = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtSigla = new System.Windows.Forms.TextBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.checkBox22 = new System.Windows.Forms.CheckBox();
		this.checkBox19 = new System.Windows.Forms.CheckBox();
		this.checkBox20 = new System.Windows.Forms.CheckBox();
		this.checkBox21 = new System.Windows.Forms.CheckBox();
		this.checkBox17 = new System.Windows.Forms.CheckBox();
		this.checkBox18 = new System.Windows.Forms.CheckBox();
		this.checkBox15 = new System.Windows.Forms.CheckBox();
		this.checkBox16 = new System.Windows.Forms.CheckBox();
		this.checkBox13 = new System.Windows.Forms.CheckBox();
		this.checkBox14 = new System.Windows.Forms.CheckBox();
		this.checkBox12 = new System.Windows.Forms.CheckBox();
		this.checkBox11 = new System.Windows.Forms.CheckBox();
		this.checkBox10 = new System.Windows.Forms.CheckBox();
		this.checkBox9 = new System.Windows.Forms.CheckBox();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.checkBox34 = new System.Windows.Forms.CheckBox();
		this.checkBox33 = new System.Windows.Forms.CheckBox();
		this.checkBox25 = new System.Windows.Forms.CheckBox();
		this.checkBox24 = new System.Windows.Forms.CheckBox();
		this.checkBox23 = new System.Windows.Forms.CheckBox();
		this.checkBox26 = new System.Windows.Forms.CheckBox();
		this.checkBox27 = new System.Windows.Forms.CheckBox();
		this.checkBox28 = new System.Windows.Forms.CheckBox();
		this.checkBox29 = new System.Windows.Forms.CheckBox();
		this.checkBox30 = new System.Windows.Forms.CheckBox();
		this.checkBox31 = new System.Windows.Forms.CheckBox();
		this.checkBox32 = new System.Windows.Forms.CheckBox();
		this.cbTipo = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransacciones).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.tabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dgvTransacciones);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(476, 358);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Transacciones";
		this.dgvTransacciones.AllowUserToAddRows = false;
		this.dgvTransacciones.AllowUserToDeleteRows = false;
		this.dgvTransacciones.AllowUserToResizeColumns = false;
		this.dgvTransacciones.AllowUserToResizeRows = false;
		this.dgvTransacciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTransacciones.Columns.AddRange(this.codigo, this.sigla, this.descripcion, this.tipo, this.estado, this.coduser, this.fecha, this.tipotext);
		this.dgvTransacciones.Location = new System.Drawing.Point(6, 45);
		this.dgvTransacciones.MultiSelect = false;
		this.dgvTransacciones.Name = "dgvTransacciones";
		this.dgvTransacciones.ReadOnly = true;
		this.dgvTransacciones.RowHeadersVisible = false;
		this.dgvTransacciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTransacciones.Size = new System.Drawing.Size(464, 307);
		this.dgvTransacciones.TabIndex = 8;
		this.dgvTransacciones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransacciones_CellDoubleClick);
		this.dgvTransacciones.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvTransacciones_ColumnHeaderMouseClick);
		this.dgvTransacciones.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvTransacciones_RowStateChanged);
		this.codigo.DataPropertyName = "codTransaccion";
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
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 250;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "Tipo";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.tipo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.tipo.Visible = false;
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
		this.tipotext.DataPropertyName = "tipotext";
		this.tipotext.HeaderText = "Tipo";
		this.tipotext.Name = "tipotext";
		this.tipotext.ReadOnly = true;
		this.tipotext.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.tipotext.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
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
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Location = new System.Drawing.Point(12, 366);
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
		this.txtSigla.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSigla.Location = new System.Drawing.Point(20, 32);
		this.txtSigla.Name = "txtSigla";
		this.txtSigla.Size = new System.Drawing.Size(71, 20);
		this.txtSigla.TabIndex = 4;
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Location = new System.Drawing.Point(97, 32);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(238, 20);
		this.txtDescripcion.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator1);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(36, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Sigla :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(94, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Descripcion :";
		this.groupBox2.Controls.Add(this.tabControl1);
		this.groupBox2.Controls.Add(this.cbTipo);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtDescripcion);
		this.groupBox2.Controls.Add(this.txtSigla);
		this.groupBox2.Location = new System.Drawing.Point(12, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(476, 358);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Transaccion";
		this.groupBox2.Visible = false;
		this.tabControl1.Controls.Add(this.tabPage1);
		this.tabControl1.Controls.Add(this.tabPage2);
		this.tabControl1.Location = new System.Drawing.Point(6, 58);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(464, 294);
		this.tabControl1.TabIndex = 10;
		this.tabPage1.Controls.Add(this.checkBox22);
		this.tabPage1.Controls.Add(this.checkBox19);
		this.tabPage1.Controls.Add(this.checkBox20);
		this.tabPage1.Controls.Add(this.checkBox21);
		this.tabPage1.Controls.Add(this.checkBox17);
		this.tabPage1.Controls.Add(this.checkBox18);
		this.tabPage1.Controls.Add(this.checkBox15);
		this.tabPage1.Controls.Add(this.checkBox16);
		this.tabPage1.Controls.Add(this.checkBox13);
		this.tabPage1.Controls.Add(this.checkBox14);
		this.tabPage1.Controls.Add(this.checkBox12);
		this.tabPage1.Controls.Add(this.checkBox11);
		this.tabPage1.Controls.Add(this.checkBox10);
		this.tabPage1.Controls.Add(this.checkBox9);
		this.tabPage1.Controls.Add(this.checkBox8);
		this.tabPage1.Controls.Add(this.checkBox7);
		this.tabPage1.Controls.Add(this.checkBox6);
		this.tabPage1.Controls.Add(this.checkBox5);
		this.tabPage1.Controls.Add(this.checkBox4);
		this.tabPage1.Controls.Add(this.checkBox3);
		this.tabPage1.Controls.Add(this.checkBox2);
		this.tabPage1.Controls.Add(this.checkBox1);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(456, 268);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "Cabecera";
		this.tabPage1.UseVisualStyleBackColor = true;
		this.checkBox22.AutoSize = true;
		this.checkBox22.Location = new System.Drawing.Point(226, 15);
		this.checkBox22.Name = "checkBox22";
		this.checkBox22.Size = new System.Drawing.Size(145, 17);
		this.checkBox22.TabIndex = 12;
		this.checkBox22.Text = "Obligatorio ingresar costo";
		this.checkBox22.UseVisualStyleBackColor = true;
		this.checkBox19.AutoSize = true;
		this.checkBox19.Location = new System.Drawing.Point(226, 245);
		this.checkBox19.Name = "checkBox19";
		this.checkBox19.Size = new System.Drawing.Size(84, 17);
		this.checkBox19.TabIndex = 22;
		this.checkBox19.Text = "Autorización";
		this.checkBox19.UseVisualStyleBackColor = true;
		this.checkBox20.AutoSize = true;
		this.checkBox20.Location = new System.Drawing.Point(226, 222);
		this.checkBox20.Name = "checkBox20";
		this.checkBox20.Size = new System.Drawing.Size(79, 17);
		this.checkBox20.TabIndex = 21;
		this.checkBox20.Text = "Comentario";
		this.checkBox20.UseVisualStyleBackColor = true;
		this.checkBox21.AutoSize = true;
		this.checkBox21.Location = new System.Drawing.Point(226, 199);
		this.checkBox21.Name = "checkBox21";
		this.checkBox21.Size = new System.Drawing.Size(59, 17);
		this.checkBox21.TabIndex = 20;
		this.checkBox21.Text = "Pedido";
		this.checkBox21.UseVisualStyleBackColor = true;
		this.checkBox17.AutoSize = true;
		this.checkBox17.Location = new System.Drawing.Point(226, 176);
		this.checkBox17.Name = "checkBox17";
		this.checkBox17.Size = new System.Drawing.Size(109, 17);
		this.checkBox17.TabIndex = 19;
		this.checkBox17.Text = "Orden de Trabajo";
		this.checkBox17.UseVisualStyleBackColor = true;
		this.checkBox18.AutoSize = true;
		this.checkBox18.Location = new System.Drawing.Point(18, 199);
		this.checkBox18.Name = "checkBox18";
		this.checkBox18.Size = new System.Drawing.Size(115, 17);
		this.checkBox18.TabIndex = 9;
		this.checkBox18.Text = "Nombre Proveedor";
		this.checkBox18.UseVisualStyleBackColor = true;
		this.checkBox15.AutoSize = true;
		this.checkBox15.Location = new System.Drawing.Point(226, 153);
		this.checkBox15.Name = "checkBox15";
		this.checkBox15.Size = new System.Drawing.Size(109, 17);
		this.checkBox15.TabIndex = 18;
		this.checkBox15.Text = "Orden de Compra";
		this.checkBox15.UseVisualStyleBackColor = true;
		this.checkBox16.AutoSize = true;
		this.checkBox16.Location = new System.Drawing.Point(18, 176);
		this.checkBox16.Name = "checkBox16";
		this.checkBox16.Size = new System.Drawing.Size(97, 17);
		this.checkBox16.TabIndex = 8;
		this.checkBox16.Text = "Cod Proveedor";
		this.checkBox16.UseVisualStyleBackColor = true;
		this.checkBox13.AutoSize = true;
		this.checkBox13.Location = new System.Drawing.Point(226, 130);
		this.checkBox13.Name = "checkBox13";
		this.checkBox13.Size = new System.Drawing.Size(75, 17);
		this.checkBox13.TabIndex = 17;
		this.checkBox13.Text = "Cotización";
		this.checkBox13.UseVisualStyleBackColor = true;
		this.checkBox14.AutoSize = true;
		this.checkBox14.Location = new System.Drawing.Point(18, 153);
		this.checkBox14.Name = "checkBox14";
		this.checkBox14.Size = new System.Drawing.Size(89, 17);
		this.checkBox14.TabIndex = 7;
		this.checkBox14.Text = "% Descuento";
		this.checkBox14.UseVisualStyleBackColor = true;
		this.checkBox12.AutoSize = true;
		this.checkBox12.Location = new System.Drawing.Point(226, 107);
		this.checkBox12.Name = "checkBox12";
		this.checkBox12.Size = new System.Drawing.Size(97, 17);
		this.checkBox12.TabIndex = 16;
		this.checkBox12.Text = "Forma de pago";
		this.checkBox12.UseVisualStyleBackColor = true;
		this.checkBox11.AutoSize = true;
		this.checkBox11.Location = new System.Drawing.Point(226, 84);
		this.checkBox11.Name = "checkBox11";
		this.checkBox11.Size = new System.Drawing.Size(99, 17);
		this.checkBox11.TabIndex = 15;
		this.checkBox11.Text = "Tipo de cambio";
		this.checkBox11.UseVisualStyleBackColor = true;
		this.checkBox10.AutoSize = true;
		this.checkBox10.Location = new System.Drawing.Point(226, 61);
		this.checkBox10.Name = "checkBox10";
		this.checkBox10.Size = new System.Drawing.Size(65, 17);
		this.checkBox10.TabIndex = 14;
		this.checkBox10.Text = "Moneda";
		this.checkBox10.UseVisualStyleBackColor = true;
		this.checkBox9.AutoSize = true;
		this.checkBox9.Location = new System.Drawing.Point(226, 38);
		this.checkBox9.Name = "checkBox9";
		this.checkBox9.Size = new System.Drawing.Size(50, 17);
		this.checkBox9.TabIndex = 13;
		this.checkBox9.Text = "Serie";
		this.checkBox9.UseVisualStyleBackColor = true;
		this.checkBox8.AutoSize = true;
		this.checkBox8.Location = new System.Drawing.Point(18, 245);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(96, 17);
		this.checkBox8.TabIndex = 11;
		this.checkBox8.Text = "N° Documento";
		this.checkBox8.UseVisualStyleBackColor = true;
		this.checkBox7.AutoSize = true;
		this.checkBox7.Location = new System.Drawing.Point(18, 222);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(81, 17);
		this.checkBox7.TabIndex = 10;
		this.checkBox7.Text = "Documento";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox6.AutoSize = true;
		this.checkBox6.Location = new System.Drawing.Point(18, 130);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(106, 17);
		this.checkBox6.TabIndex = 6;
		this.checkBox6.Text = "Direccion Cliente";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Location = new System.Drawing.Point(18, 107);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(49, 17);
		this.checkBox5.TabIndex = 5;
		this.checkBox5.Text = "RUC";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Location = new System.Drawing.Point(18, 84);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(45, 17);
		this.checkBox4.TabIndex = 4;
		this.checkBox4.Text = "DNI";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Location = new System.Drawing.Point(18, 61);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(92, 17);
		this.checkBox3.TabIndex = 3;
		this.checkBox3.Text = "Razon Cliente";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Location = new System.Drawing.Point(18, 38);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(98, 17);
		this.checkBox2.TabIndex = 2;
		this.checkBox2.Text = "Nombre Cliente";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(18, 15);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(83, 17);
		this.checkBox1.TabIndex = 1;
		this.checkBox1.Text = "Cod. Cliente";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.tabPage2.Controls.Add(this.checkBox34);
		this.tabPage2.Controls.Add(this.checkBox33);
		this.tabPage2.Controls.Add(this.checkBox25);
		this.tabPage2.Controls.Add(this.checkBox24);
		this.tabPage2.Controls.Add(this.checkBox23);
		this.tabPage2.Controls.Add(this.checkBox26);
		this.tabPage2.Controls.Add(this.checkBox27);
		this.tabPage2.Controls.Add(this.checkBox28);
		this.tabPage2.Controls.Add(this.checkBox29);
		this.tabPage2.Controls.Add(this.checkBox30);
		this.tabPage2.Controls.Add(this.checkBox31);
		this.tabPage2.Controls.Add(this.checkBox32);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(456, 268);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "Detalle";
		this.tabPage2.UseVisualStyleBackColor = true;
		this.checkBox34.AutoSize = true;
		this.checkBox34.Location = new System.Drawing.Point(223, 132);
		this.checkBox34.Name = "checkBox34";
		this.checkBox34.Size = new System.Drawing.Size(67, 17);
		this.checkBox34.TabIndex = 34;
		this.checkBox34.Text = "P. Venta";
		this.checkBox34.UseVisualStyleBackColor = true;
		this.checkBox33.AutoSize = true;
		this.checkBox33.Location = new System.Drawing.Point(223, 109);
		this.checkBox33.Name = "checkBox33";
		this.checkBox33.Size = new System.Drawing.Size(44, 17);
		this.checkBox33.TabIndex = 33;
		this.checkBox33.Text = "IGV";
		this.checkBox33.UseVisualStyleBackColor = true;
		this.checkBox25.AutoSize = true;
		this.checkBox25.Location = new System.Drawing.Point(223, 63);
		this.checkBox25.Name = "checkBox25";
		this.checkBox25.Size = new System.Drawing.Size(87, 17);
		this.checkBox25.TabIndex = 31;
		this.checkBox25.Text = "Monto Dscto";
		this.checkBox25.UseVisualStyleBackColor = true;
		this.checkBox24.AutoSize = true;
		this.checkBox24.Location = new System.Drawing.Point(223, 40);
		this.checkBox24.Name = "checkBox24";
		this.checkBox24.Size = new System.Drawing.Size(89, 17);
		this.checkBox24.TabIndex = 30;
		this.checkBox24.Text = "% Descuento";
		this.checkBox24.UseVisualStyleBackColor = true;
		this.checkBox23.AutoSize = true;
		this.checkBox23.Location = new System.Drawing.Point(223, 17);
		this.checkBox23.Name = "checkBox23";
		this.checkBox23.Size = new System.Drawing.Size(81, 17);
		this.checkBox23.TabIndex = 29;
		this.checkBox23.Text = "Precio Unit.";
		this.checkBox23.UseVisualStyleBackColor = true;
		this.checkBox26.AutoSize = true;
		this.checkBox26.Location = new System.Drawing.Point(223, 86);
		this.checkBox26.Name = "checkBox26";
		this.checkBox26.Size = new System.Drawing.Size(67, 17);
		this.checkBox26.TabIndex = 32;
		this.checkBox26.Text = "V. Venta";
		this.checkBox26.UseVisualStyleBackColor = true;
		this.checkBox27.AutoSize = true;
		this.checkBox27.Location = new System.Drawing.Point(18, 132);
		this.checkBox27.Name = "checkBox27";
		this.checkBox27.Size = new System.Drawing.Size(78, 17);
		this.checkBox27.TabIndex = 28;
		this.checkBox27.Text = "Costo Unit.";
		this.checkBox27.UseVisualStyleBackColor = true;
		this.checkBox28.AutoSize = true;
		this.checkBox28.Location = new System.Drawing.Point(18, 109);
		this.checkBox28.Name = "checkBox28";
		this.checkBox28.Size = new System.Drawing.Size(68, 17);
		this.checkBox28.TabIndex = 27;
		this.checkBox28.Text = "Cantidad";
		this.checkBox28.UseVisualStyleBackColor = true;
		this.checkBox29.AutoSize = true;
		this.checkBox29.Location = new System.Drawing.Point(18, 86);
		this.checkBox29.Name = "checkBox29";
		this.checkBox29.Size = new System.Drawing.Size(76, 17);
		this.checkBox29.TabIndex = 26;
		this.checkBox29.Text = "Serie/Lote";
		this.checkBox29.UseVisualStyleBackColor = true;
		this.checkBox30.AutoSize = true;
		this.checkBox30.Location = new System.Drawing.Point(18, 63);
		this.checkBox30.Name = "checkBox30";
		this.checkBox30.Size = new System.Drawing.Size(60, 17);
		this.checkBox30.TabIndex = 25;
		this.checkBox30.Text = "Unidad";
		this.checkBox30.UseVisualStyleBackColor = true;
		this.checkBox31.AutoSize = true;
		this.checkBox31.Location = new System.Drawing.Point(18, 40);
		this.checkBox31.Name = "checkBox31";
		this.checkBox31.Size = new System.Drawing.Size(82, 17);
		this.checkBox31.TabIndex = 24;
		this.checkBox31.Text = "Descripcion";
		this.checkBox31.UseVisualStyleBackColor = true;
		this.checkBox32.AutoSize = true;
		this.checkBox32.Location = new System.Drawing.Point(18, 17);
		this.checkBox32.Name = "checkBox32";
		this.checkBox32.Size = new System.Drawing.Size(59, 17);
		this.checkBox32.TabIndex = 23;
		this.checkBox32.Text = "Codigo";
		this.checkBox32.UseVisualStyleBackColor = true;
		this.cbTipo.FormattingEnabled = true;
		this.cbTipo.Items.AddRange(new object[2] { "INGRESO", "SALIDA" });
		this.cbTipo.Location = new System.Drawing.Point(341, 31);
		this.cbTipo.Name = "cbTipo";
		this.cbTipo.Size = new System.Drawing.Size(121, 21);
		this.cbTipo.TabIndex = 9;
		this.superValidator1.SetValidator1(this.cbTipo, this.customValidator2);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(338, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(34, 13);
		this.label6.TabIndex = 8;
		this.label6.Text = "Tipo :";
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Ingrese la descripción.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator2.ErrorMessage = "Seleccione un tipo";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(500, 426);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTransacciones";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Transacciones";
		base.Load += new System.EventHandler(frmTransacciones_Load);
		base.Shown += new System.EventHandler(frmTransacciones_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransacciones).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.tabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
