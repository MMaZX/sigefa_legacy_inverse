using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmNotas : Office2007Form
{
	private clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

	private clsNotaIngreso notaI = new clsNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private DataTable dt1 = new DataTable();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private ImageList imageList1;

	private Button btnSalir;

	private Button btnIrNota;

	private ComboBox cmbTipoDocumento;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Label label4;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvDocumentos;

	private Label label6;

	private Label label5;

	private Button btnConsultar;

	private Button btnEliminar;

	private Button btnAnular;

	private Button btnReporte;

	private Label txtNombreProducto;

	private Label label7;

	private Button btnBuscarProducto;

	private Label label8;

	private TextBox txtCodprod;

	private RadioButton rbFechaRegistro;

	private RadioButton rbFechaNINS;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn numero;

	private DataGridViewTextBoxColumn transaccion;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn proveedor;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn numerodoc;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn bruto;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn usuario;

	private DataGridViewTextBoxColumn user_anulador;

	private DataGridViewTextBoxColumn formapago;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewTextBoxColumn anulado;

	private DataGridViewTextBoxColumn refe;

	public frmNotas()
	{
		InitializeComponent();
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaLista(cmbTipoDocumento.SelectedIndex);
		activarfiltro();
	}

	private void activarfiltro()
	{
		label1.Visible = true;
		label2.Visible = true;
		txtFiltro.Visible = true;
		label2.Text = "Numero";
		label3.Text = "codNotaIngreso";
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
			dgvDocumentos.DataSource = data;
			int tipoFecha = (rbFechaNINS.Checked ? 1 : (rbFechaRegistro.Checked ? 2 : 0));
			data.DataSource = AdmNotaI.MuestraNotasIngreso(caso, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date, tipoFecha);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDocumentos.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
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

	private void btnIrNota_Click(object sender, EventArgs e)
	{
		int f = dgvDocumentos.CurrentRow.Index;
		if (dgvDocumentos.Rows.Count >= 1)
		{
			if (dgvDocumentos.Rows[f].Cells[tipo.Name].Value.ToString() == "NI")
			{
				frmNotaIngreso form = new frmNotaIngreso();
				form.MdiParent = base.MdiParent;
				form.CodNota = notaI.CodNotaIngreso;
				form.Proceso = Proceso;
				form.Show();
			}
			else
			{
				frmNotaSalida form2 = new frmNotaSalida();
				form2.MdiParent = base.MdiParent;
				form2.CodNota = notaS.CodNotaSalida;
				form2.Proceso = Proceso;
				form2.Show();
			}
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmNotas_Load(object sender, EventArgs e)
	{
		cmbTipoDocumento.SelectedIndex = 0;
		if (Proceso == 4)
		{
			btnEliminar.Visible = true;
		}
		else if (Proceso == 5)
		{
			btnAnular.Visible = true;
		}
	}

	private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cmbTipoDocumento.SelectedIndex != -1)
		{
			btnConsultar.Enabled = true;
		}
		else
		{
			btnConsultar.Enabled = false;
		}
	}

	private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count >= 1 && e.RowIndex != -1)
		{
			if (dgvDocumentos.Rows[e.RowIndex].Cells[tipo.Name].Value.ToString() == "NI")
			{
				frmNotaIngreso form = new frmNotaIngreso();
				form.MdiParent = base.MdiParent;
				form.CodNota = notaI.CodNotaIngreso;
				form.Proceso = Proceso;
				form.Show();
			}
			else
			{
				frmNotaSalida form2 = new frmNotaSalida();
				form2.MdiParent = base.MdiParent;
				form2.CodNota = notaS.CodNotaSalida;
				form2.Proceso = Proceso;
				form2.Show();
			}
		}
	}

	private void dgvDocumentos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvDocumentos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvDocumentos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			btnIrNota.Enabled = true;
			btnEliminar.Enabled = true;
			if (dgvDocumentos.Rows.Count >= 1 && e.RowIndex != -1)
			{
				if (dgvDocumentos.Rows[e.RowIndex].Cells[tipo.Name].Value.ToString() == "NI")
				{
					notaI.CodNotaIngreso = dgvDocumentos.Rows[e.RowIndex].Cells[numero.Name].Value.ToString();
				}
				else
				{
					notaS.CodNotaSalida = dgvDocumentos.Rows[e.RowIndex].Cells[numero.Name].Value.ToString();
				}
				if (dgvDocumentos.Rows[e.RowIndex].Cells[anulado.Name].Value.ToString() == "ACTIVO")
				{
					btnAnular.Text = "Anular";
					btnAnular.Enabled = true;
					btnAnular.ImageIndex = 10;
				}
				else
				{
					btnAnular.Text = "Activar";
					btnAnular.Enabled = true;
					btnAnular.ImageIndex = 11;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		DataGridViewRow row = dgvDocumentos.SelectedRows[0];
		if (dgvDocumentos.Rows.Count < 1 || dgvDocumentos.CurrentRow.Index == -1)
		{
			return;
		}
		if (row.Cells[tipo.Name].Value.ToString() == "NI")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmNotaI.delete(Convert.ToInt32(notaI.CodNotaIngreso)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista(cmbTipoDocumento.SelectedIndex);
			}
		}
		else
		{
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Almacenes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 != DialogResult.No && AdmNotaS.delete(Convert.ToInt32(notaS.CodNotaSalida)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista(cmbTipoDocumento.SelectedIndex);
			}
		}
	}

	private void btrnAnular_Click(object sender, EventArgs e)
	{
		DataGridViewRow row = dgvDocumentos.CurrentRow;
		if (btnAnular.Text == "Anular")
		{
			if (dgvDocumentos.Rows.Count < 1 || dgvDocumentos.CurrentRow.Index == -1)
			{
				return;
			}
			if (row.Cells[tipo.Name].Value.ToString() == "NI")
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No && AdmNotaI.anular1(Convert.ToInt32(notaI.CodNotaIngreso)))
				{
					MessageBox.Show("El documento ha sido anulado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista(cmbTipoDocumento.SelectedIndex);
				}
			}
			else
			{
				DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult2 != DialogResult.No && AdmNotaS.anular(Convert.ToInt32(notaS.CodNotaSalida)))
				{
					MessageBox.Show("El documento ha sido anulado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista(cmbTipoDocumento.SelectedIndex);
				}
			}
		}
		else
		{
			if (!(btnAnular.Text == "Activar") || dgvDocumentos.Rows.Count < 1 || dgvDocumentos.CurrentRow.Index == -1)
			{
				return;
			}
			if (row.Cells[tipo.Name].Value.ToString() == "NI")
			{
				DialogResult dlgResult3 = MessageBox.Show("Esta seguro que desea activar el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult3 != DialogResult.No && AdmNotaI.activar(Convert.ToInt32(notaI.CodNotaIngreso)))
				{
					MessageBox.Show("El documento ha sido activado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista(cmbTipoDocumento.SelectedIndex);
				}
			}
			else
			{
				DialogResult dlgResult4 = MessageBox.Show("Esta seguro que desea activar el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult4 != DialogResult.No && AdmNotaS.activar(Convert.ToInt32(notaS.CodNotaSalida)))
				{
					MessageBox.Show("El documento ha sido activado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista(cmbTipoDocumento.SelectedIndex);
				}
			}
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable("NotasIngresoSalida");
		foreach (DataGridViewColumn column in dgvDocumentos.Columns)
		{
			DataColumn dc = new DataColumn(column.Name.ToString());
			dt.Columns.Add(dc);
		}
		for (int i = 0; i < dgvDocumentos.Rows.Count; i++)
		{
			DataGridViewRow row = dgvDocumentos.Rows[i];
			DataRow dr = dt.NewRow();
			for (int j = 0; j < dgvDocumentos.Columns.Count; j++)
			{
				dr[j] = ((row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString());
			}
			dt.Rows.Add(dr);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\NotasIngresoSaidaRPT.xml", XmlWriteMode.WriteSchema);
		CRNotasIngresoSalida rpt = new CRNotasIngresoSalida();
		frmNotasIngresoSalida frm = new frmNotasIngresoSalida();
		rpt.SetDataSource(ds);
		frm.crvNotasIngresoSalida.ReportSource = rpt;
		frm.Show();
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaListaxProducto(cmbTipoDocumento.SelectedIndex);
			activarfiltro();
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
	}

	private void CargaListaxProducto(int caso)
	{
		try
		{
			if (data.DataSource != null)
			{
				DataTable dt = (DataTable)data.DataSource;
				dt.Clear();
			}
			dgvDocumentos.DataSource = data;
			int tipoFecha = (rbFechaNINS.Checked ? 1 : (rbFechaRegistro.Checked ? 2 : 0));
			data.DataSource = AdmNotaI.MuestraNotasIngresoxProducto(caso, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date, Convert.ToInt32(txtCodprod.Text), tipoFecha);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDocumentos.ClearSelection();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
		}
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotas));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rbFechaRegistro = new System.Windows.Forms.RadioButton();
		this.rbFechaNINS = new System.Windows.Forms.RadioButton();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label8 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.cmbTipoDocumento = new System.Windows.Forms.ComboBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDocumentos = new System.Windows.Forms.DataGridView();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.transaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.bruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.user_anulador = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.refe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnIrNota = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.rbFechaRegistro);
		this.groupBox1.Controls.Add(this.rbFechaNINS);
		this.groupBox1.Controls.Add(this.txtNombreProducto);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.btnBuscarProducto);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.txtCodprod);
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.btnConsultar);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.cmbTipoDocumento);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1289, 97);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Buscar";
		this.rbFechaRegistro.AutoSize = true;
		this.rbFechaRegistro.Location = new System.Drawing.Point(171, 5);
		this.rbFechaRegistro.Name = "rbFechaRegistro";
		this.rbFechaRegistro.Size = new System.Drawing.Size(97, 17);
		this.rbFechaRegistro.TabIndex = 56;
		this.rbFechaRegistro.Text = "Fecha Registro";
		this.rbFechaRegistro.UseVisualStyleBackColor = true;
		this.rbFechaNINS.AutoSize = true;
		this.rbFechaNINS.Checked = true;
		this.rbFechaNINS.Location = new System.Drawing.Point(65, 5);
		this.rbFechaNINS.Name = "rbFechaNINS";
		this.rbFechaNINS.Size = new System.Drawing.Size(89, 17);
		this.rbFechaNINS.TabIndex = 55;
		this.rbFechaNINS.TabStop = true;
		this.rbFechaNINS.Text = "Fecha NI/NS";
		this.rbFechaNINS.UseVisualStyleBackColor = true;
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(608, 71);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 43;
		this.txtNombreProducto.Text = "---";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(567, 71);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(35, 13);
		this.label7.TabIndex = 42;
		this.label7.Text = "Prod.:";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(527, 61);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 41;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(380, 55);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(112, 13);
		this.label8.TabIndex = 40;
		this.label8.Text = "Busqueda x Producto:";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(383, 71);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 39;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.btnReporte.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReporte.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(714, 16);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(105, 32);
		this.btnReporte.TabIndex = 16;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = false;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "search (1).png");
		this.imageList1.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList1.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList1.Images.SetKeyName(9, "document_delete.png");
		this.imageList1.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(11, "OK_Verde.png");
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.ImageIndex = 6;
		this.btnConsultar.ImageList = this.imageList1;
		this.btnConsultar.Location = new System.Drawing.Point(611, 16);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(97, 32);
		this.btnConsultar.TabIndex = 14;
		this.btnConsultar.Text = "Buscar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(196, 31);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(23, 31);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Desde :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(380, 23);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(34, 13);
		this.label4.TabIndex = 11;
		this.label4.Text = "Tipo :";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(332, 51);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 10;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(95, 51);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 9;
		this.label2.Text = "X";
		this.label2.Visible = false;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(27, 70);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(317, 20);
		this.txtFiltro.TabIndex = 8;
		this.txtFiltro.Visible = false;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(24, 53);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(56, 13);
		this.label1.TabIndex = 7;
		this.label1.Text = "Filtrar por :";
		this.label1.Visible = false;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(76, 28);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 4;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(243, 28);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 3;
		this.cmbTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTipoDocumento.FormattingEnabled = true;
		this.cmbTipoDocumento.Items.AddRange(new object[3] { "Notas de Ingreso", "Notas de Salida", "Todo" });
		this.cmbTipoDocumento.Location = new System.Drawing.Point(420, 20);
		this.cmbTipoDocumento.Name = "cmbTipoDocumento";
		this.cmbTipoDocumento.Size = new System.Drawing.Size(171, 21);
		this.cmbTipoDocumento.TabIndex = 0;
		this.cmbTipoDocumento.SelectedIndexChanged += new System.EventHandler(cmbTipoDocumento_SelectedIndexChanged);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDocumentos);
		this.groupBox2.Location = new System.Drawing.Point(0, 103);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1289, 423);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Documentos";
		this.dgvDocumentos.AllowUserToAddRows = false;
		this.dgvDocumentos.AllowUserToDeleteRows = false;
		this.dgvDocumentos.AllowUserToResizeColumns = false;
		this.dgvDocumentos.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDocumentos.Columns.AddRange(this.tipo, this.numero, this.transaccion, this.fecha, this.fecharegistro, this.proveedor, this.cliente, this.docref, this.numerodoc, this.moneda, this.bruto, this.montodscto, this.igv, this.total, this.usuario, this.user_anulador, this.formapago, this.fechapago, this.anulado, this.refe);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDocumentos.DefaultCellStyle = dataGridViewCellStyle7;
		this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDocumentos.Location = new System.Drawing.Point(3, 16);
		this.dgvDocumentos.Name = "dgvDocumentos";
		this.dgvDocumentos.ReadOnly = true;
		this.dgvDocumentos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.dgvDocumentos.RowHeadersVisible = false;
		this.dgvDocumentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDocumentos.Size = new System.Drawing.Size(1283, 404);
		this.dgvDocumentos.TabIndex = 0;
		this.dgvDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellClick);
		this.dgvDocumentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvDocumentos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDocumentos_ColumnHeaderMouseClick);
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "Tipo";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Width = 40;
		this.numero.DataPropertyName = "codNota";
		dataGridViewCellStyle9.NullValue = null;
		this.numero.DefaultCellStyle = dataGridViewCellStyle9;
		this.numero.HeaderText = "Numero";
		this.numero.Name = "numero";
		this.numero.ReadOnly = true;
		this.transaccion.DataPropertyName = "movimiento";
		this.transaccion.HeaderText = "Trans.";
		this.transaccion.Name = "transaccion";
		this.transaccion.ReadOnly = true;
		this.transaccion.Width = 50;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha NI/NS";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.proveedor.DataPropertyName = "proveedor";
		this.proveedor.HeaderText = "Proveedor";
		this.proveedor.Name = "proveedor";
		this.proveedor.ReadOnly = true;
		this.proveedor.Visible = false;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Visible = false;
		this.docref.DataPropertyName = "documento";
		this.docref.HeaderText = "Doc. Ref.";
		this.docref.Name = "docref";
		this.docref.ReadOnly = true;
		this.docref.Width = 60;
		this.numerodoc.DataPropertyName = "numdocumento";
		this.numerodoc.HeaderText = "Num. Doc. Ref.";
		this.numerodoc.Name = "numerodoc";
		this.numerodoc.ReadOnly = true;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.bruto.DataPropertyName = "bruto";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		this.bruto.DefaultCellStyle = dataGridViewCellStyle10;
		this.bruto.HeaderText = "Bruto";
		this.bruto.Name = "bruto";
		this.bruto.ReadOnly = true;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "N2";
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle11;
		this.montodscto.HeaderText = "Monto Dscto.";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle12.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle12;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.total.DataPropertyName = "total";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle13.Format = "N2";
		this.total.DefaultCellStyle = dataGridViewCellStyle13;
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.usuario.DataPropertyName = "usuario";
		this.usuario.HeaderText = "Creado Por";
		this.usuario.Name = "usuario";
		this.usuario.ReadOnly = true;
		this.user_anulador.DataPropertyName = "user_anulador";
		this.user_anulador.HeaderText = "Usuario Anulador";
		this.user_anulador.Name = "user_anulador";
		this.user_anulador.ReadOnly = true;
		this.user_anulador.Width = 120;
		this.formapago.DataPropertyName = "formapago";
		this.formapago.HeaderText = "Forma Pago";
		this.formapago.Name = "formapago";
		this.formapago.ReadOnly = true;
		this.formapago.Visible = false;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "Fecha Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.fechapago.Visible = false;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.refe.DataPropertyName = "documentoreferencia";
		this.refe.HeaderText = "Referencia";
		this.refe.Name = "refe";
		this.refe.ReadOnly = true;
		this.refe.Visible = false;
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.Enabled = false;
		this.btnAnular.ImageIndex = 10;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(925, 532);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(98, 32);
		this.btnAnular.TabIndex = 15;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btrnAnular_Click);
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 9;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(1029, 532);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(84, 32);
		this.btnEliminar.TabIndex = 14;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Visible = false;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1209, 532);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 12;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnIrNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrNota.Enabled = false;
		this.btnIrNota.ImageIndex = 8;
		this.btnIrNota.ImageList = this.imageList1;
		this.btnIrNota.Location = new System.Drawing.Point(1119, 532);
		this.btnIrNota.Name = "btnIrNota";
		this.btnIrNota.Size = new System.Drawing.Size(84, 32);
		this.btnIrNota.TabIndex = 13;
		this.btnIrNota.Text = "Consultar";
		this.btnIrNota.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrNota.UseVisualStyleBackColor = true;
		this.btnIrNota.Click += new System.EventHandler(btnIrNota_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1289, 576);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btnEliminar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnIrNota);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmNotas";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Notas";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotas_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		base.ResumeLayout(false);
	}
}
