using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmNotasOrden : Office2007Form
{
	private clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

	private clsNotaIngreso notaI = new clsNotaIngreso();

	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida notaS = new clsNotaSalida();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private ImageList imageList1;

	private Button btnSalir;

	private Button btnIrNota;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private DataGridView dgvDocumentos;

	private Label label6;

	private Label label5;

	private Button btnConsultar;

	private Button btnEliminar;

	private Button btnAnular;

	private Button btnReporte;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn numero;

	private DataGridViewTextBoxColumn transaccion;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn proveedor;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn docref;

	private DataGridViewTextBoxColumn numerodoc;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn bruto;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn formapago;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewTextBoxColumn anulado;

	public frmNotasOrden()
	{
		InitializeComponent();
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaLista(0);
	}

	private void CargaLista(int caso)
	{
		dgvDocumentos.DataSource = data;
		data.DataSource = AdmNotaI.MuestraNotasIngreso(caso, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date, 0);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDocumentos.ClearSelection();
	}

	private void btnIrNota_Click(object sender, EventArgs e)
	{
		if (dgvDocumentos.Rows.Count > 0)
		{
			DataGridViewRow row = dgvDocumentos.SelectedRows[0];
			if (dgvDocumentos.Rows.Count >= 1 && row.Cells[tipo.Name].Value.ToString() == "NI")
			{
				frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
				form.CodNota = notaI.CodNotaIngreso;
				form.Proceso = Proceso;
				form.Show();
			}
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmNotas_Load(object sender, EventArgs e)
	{
		if (Proceso == 4)
		{
			btnEliminar.Visible = true;
		}
		else if (Proceso == 5)
		{
			btnAnular.Visible = true;
		}
	}

	private void dgvDocumentos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvDocumentos.Rows.Count >= 1 && e.Row.Selected && e.Row.Cells[tipo.Name].Value.ToString() == "NI")
		{
			notaI.CodNotaIngreso = e.Row.Cells[numero.Name].Value.ToString();
		}
	}

	private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count >= 1 && e.RowIndex != -1 && dgvDocumentos.Rows[e.RowIndex].Cells[tipo.Name].Value.ToString() == "NI")
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.CodNota = notaI.CodNotaIngreso;
			form.Proceso = Proceso;
			form.ShowDialog();
		}
	}

	private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count >= 1 && e.RowIndex != -1)
		{
			btnIrNota.Enabled = true;
			btnEliminar.Enabled = true;
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

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		DataGridViewRow row = dgvDocumentos.SelectedRows[0];
		if (dgvDocumentos.Rows.Count >= 1 && dgvDocumentos.CurrentRow.Index != -1 && row.Cells[tipo.Name].Value.ToString() == "NI")
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmNotaI.delete(Convert.ToInt32(notaI.CodNotaIngreso)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista(0);
			}
		}
	}

	private void btrnAnular_Click(object sender, EventArgs e)
	{
		DataGridViewRow row = dgvDocumentos.CurrentRow;
		if (btnAnular.Text == "Anular")
		{
			if (dgvDocumentos.Rows.Count >= 1 && dgvDocumentos.CurrentRow.Index != -1 && row.Cells[tipo.Name].Value.ToString() == "NI")
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No && AdmNotaI.anular1(Convert.ToInt32(notaI.CodNotaIngreso)))
				{
					MessageBox.Show("El documento ha sido anulado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaLista(0);
				}
			}
		}
		else if (btnAnular.Text == "Activar" && dgvDocumentos.Rows.Count >= 1 && dgvDocumentos.CurrentRow.Index != -1 && row.Cells[tipo.Name].Value.ToString() == "NI")
		{
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea activar el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 != DialogResult.No && AdmNotaI.activar(Convert.ToInt32(notaI.CodNotaIngreso)))
			{
				MessageBox.Show("El documento ha sido activado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista(0);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmNotasOrden));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDocumentos = new System.Windows.Forms.DataGridView();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.transaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.bruto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnIrNota = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnReporte);
		this.groupBox1.Controls.Add(this.btnConsultar);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(911, 97);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Buscar";
		this.btnReporte.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReporte.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReporte.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(648, 16);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(105, 32);
		this.btnReporte.TabIndex = 15;
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
		this.btnConsultar.Location = new System.Drawing.Point(510, 16);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 32);
		this.btnConsultar.TabIndex = 14;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(232, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(59, 26);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(112, 23);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 4;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(279, 23);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 3;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDocumentos);
		this.groupBox2.Location = new System.Drawing.Point(0, 97);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(911, 429);
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
		this.dgvDocumentos.Columns.AddRange(this.tipo, this.numero, this.transaccion, this.fecha, this.proveedor, this.cliente, this.docref, this.numerodoc, this.moneda, this.bruto, this.montodscto, this.igv, this.total, this.formapago, this.fechapago, this.anulado);
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
		this.dgvDocumentos.Size = new System.Drawing.Size(905, 410);
		this.dgvDocumentos.TabIndex = 0;
		this.dgvDocumentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellClick);
		this.dgvDocumentos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDocumentos_RowStateChanged);
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
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.proveedor.DataPropertyName = "proveedor";
		this.proveedor.HeaderText = "Proveedor";
		this.proveedor.Name = "proveedor";
		this.proveedor.ReadOnly = true;
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
		this.formapago.DataPropertyName = "formapago";
		this.formapago.HeaderText = "Forma Pago";
		this.formapago.Name = "formapago";
		this.formapago.ReadOnly = true;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "Fecha Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(831, 532);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 12;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnIrNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrNota.ImageIndex = 8;
		this.btnIrNota.ImageList = this.imageList1;
		this.btnIrNota.Location = new System.Drawing.Point(741, 532);
		this.btnIrNota.Name = "btnIrNota";
		this.btnIrNota.Size = new System.Drawing.Size(84, 32);
		this.btnIrNota.TabIndex = 13;
		this.btnIrNota.Text = "Consultar";
		this.btnIrNota.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrNota.UseVisualStyleBackColor = true;
		this.btnIrNota.Click += new System.EventHandler(btnIrNota_Click);
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 9;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(651, 532);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(84, 32);
		this.btnEliminar.TabIndex = 14;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Visible = false;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.ImageIndex = 10;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(547, 532);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(98, 32);
		this.btnAnular.TabIndex = 15;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btrnAnular_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(911, 576);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btnEliminar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnIrNota);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmNotasOrden";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "NotasOrden";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotas_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		base.ResumeLayout(false);
	}
}
