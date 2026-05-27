using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmComsionPorDocumento : Office2007Form
{
	private clsAdmVendedor vendedor = new clsAdmVendedor();

	private clsAdmZona AdmZona = new clsAdmZona();

	private IContainer components = null;

	private GroupBox groupBox2;

	private ComboBox cmbAño;

	private ComboBox cmbMes;

	private GroupBox groupBox1;

	private TextBox txtComisionTotal;

	private DataGridView dgvComisiones;

	private Label label2;

	private Label label1;

	private ComboBox cbVendedor;

	private Label label3;

	private Label label4;

	private DataGridViewTextBoxColumn apellido;

	private DataGridViewTextBoxColumn sigla;

	private DataGridViewTextBoxColumn nro_documento;

	private DataGridViewTextBoxColumn codper;

	private DataGridViewTextBoxColumn razonsocial;

	private DataGridViewTextBoxColumn zona;

	private DataGridViewTextBoxColumn fechasalida;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn anulado;

	private DataGridViewTextBoxColumn notaCredito;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn basex;

	private DataGridViewTextBoxColumn comison;

	private Label label5;

	private ComboBox cbZona;

	private Button btnReporte;

	public frmComsionPorDocumento()
	{
		InitializeComponent();
	}

	private void CalcularComision()
	{
		decimal comision_total = default(decimal);
		decimal comision = default(decimal);
		if (dgvComisiones.Rows.Count > 0)
		{
			for (int i = 0; i <= dgvComisiones.Rows.Count - 1; i++)
			{
				comision = Convert.ToDecimal(dgvComisiones.Rows[i].Cells[comison.Name].Value);
				comision_total += comision;
			}
		}
		txtComisionTotal.Text = comision_total.ToString();
	}

	private void CargarComisiones()
	{
		dgvComisiones.DataSource = vendedor.MuestraComisionesPorDocumentoFecha();
		dgvComisiones.ClearSelection();
	}

	private void FiltroComisiones()
	{
		dgvComisiones.DataSource = vendedor.MuestraComisionPorDocumentoFecha(Convert.ToInt32(cmbMes.SelectedValue), Convert.ToInt32(cmbAño.SelectedValue));
		dgvComisiones.ClearSelection();
	}

	private void FiltroComisionesVendedor()
	{
		dgvComisiones.DataSource = vendedor.MuestraComisionPorDocumentoPorVendedor(Convert.ToInt32(cmbMes.SelectedValue), Convert.ToInt32(cmbAño.SelectedValue), Convert.ToInt32(cbVendedor.SelectedValue));
		dgvComisiones.ClearSelection();
	}

	private void FiltroComisionesVendedorZona()
	{
		dgvComisiones.DataSource = vendedor.MuestraComisionPorDocumentoPorVendedorZona(Convert.ToInt32(cmbMes.SelectedValue), Convert.ToInt32(cmbAño.SelectedValue), Convert.ToInt32(cbVendedor.SelectedValue), Convert.ToInt32(cbZona.SelectedValue));
		dgvComisiones.ClearSelection();
	}

	private void frmComsionPorDocumento_Load(object sender, EventArgs e)
	{
		llenacombos();
		cmbAño.SelectedValue = DateTime.Now.Year;
		cmbMes.SelectedValue = DateTime.Now.Month;
		CargarVendedores();
		CargaZonas();
		FiltroComisiones();
		CalcularComision();
	}

	private void CargaZonas()
	{
		cbZona.DataSource = AdmZona.MuestraZonas();
		cbZona.DisplayMember = "descripcion";
		cbZona.ValueMember = "codZona";
		cbZona.SelectedIndex = -1;
	}

	private void CargarVendedores()
	{
		cbVendedor.DataSource = vendedor.MuestraVendedoresCombo();
		cbVendedor.DisplayMember = "apellido";
		cbVendedor.ValueMember = "codVendedor";
		cbVendedor.SelectedIndex = -1;
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void dgvComisiones_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void llenacombos()
	{
		DataTable dt = new DataTable("Tabla");
		dt.Columns.Add("Codigo");
		dt.Columns.Add("Descripcion");
		DataRow dr = dt.NewRow();
		dr[0] = "1";
		dr[1] = "ENERO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "2";
		dr[1] = "FEBRERO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "3";
		dr[1] = "MARZO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "4";
		dr[1] = "ABRIL";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "5";
		dr[1] = "MAYO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "6";
		dr[1] = "JUNIO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "7";
		dr[1] = "JULIO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "8";
		dr[1] = "AGOSTO";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "9";
		dr[1] = "SETIEMBRE";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "10";
		dr[1] = "OCTUBRE";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "11";
		dr[1] = "NOVIEMBRE";
		dt.Rows.Add(dr);
		dr = dt.NewRow();
		dr[0] = "12";
		dr[1] = "DICIEMBRE";
		dt.Rows.Add(dr);
		cmbMes.DataSource = dt;
		cmbMes.ValueMember = "Codigo";
		cmbMes.DisplayMember = "Descripcion";
		cmbMes.SelectedIndex = -1;
		DataTable dt2 = new DataTable("Tabla1");
		dt2.Columns.Add("Codigo");
		dt2.Columns.Add("Descripcion");
		DataRow dr2 = dt2.NewRow();
		dr2[0] = "2013";
		dr2[1] = "2013";
		dt2.Rows.Add(dr2);
		dr2 = dt2.NewRow();
		dr2[0] = "2014";
		dr2[1] = "2014";
		dt2.Rows.Add(dr2);
		cmbAño.DataSource = dt2;
		cmbAño.ValueMember = "Codigo";
		cmbAño.DisplayMember = "Descripcion";
		cmbAño.SelectedIndex = -1;
	}

	private void cmbAño_SelectionChangeCommitted(object sender, EventArgs e)
	{
		FiltroComisiones();
		CalcularComision();
	}

	private void cmbMes_SelectionChangeCommitted(object sender, EventArgs e)
	{
		FiltroComisiones();
		CalcularComision();
	}

	private void cbVendedor_SelectionChangeCommitted(object sender, EventArgs e)
	{
		FiltroComisionesVendedor();
		CalcularComision();
	}

	private void cbZona_SelectionChangeCommitted(object sender, EventArgs e)
	{
		FiltroComisionesVendedorZona();
		CalcularComision();
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		DataSet ds = new DataSet();
		DataTable dt = new DataTable();
		dt.Columns.Add("apellido", typeof(string));
		dt.Columns.Add("sigla", typeof(string));
		dt.Columns.Add("nro_documento", typeof(string));
		dt.Columns.Add("codper", typeof(string));
		dt.Columns.Add("razonsocial", typeof(string));
		dt.Columns.Add("zona", typeof(string));
		dt.Columns.Add("fechasalida", typeof(DateTime));
		dt.Columns.Add("total", typeof(decimal));
		dt.Columns.Add("anulado", typeof(string));
		dt.Columns.Add("notaCredito", typeof(string));
		dt.Columns.Add("monto", typeof(string));
		dt.Columns.Add("basex", typeof(string));
		dt.Columns.Add("comison", typeof(decimal));
		dt.Columns.Add("anio", typeof(string));
		dt.Columns.Add("mes", typeof(string));
		dt.Columns.Add("vendedor", typeof(string));
		foreach (DataGridViewRow dgv in (IEnumerable)dgvComisiones.Rows)
		{
			dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, Convert.ToDateTime(dgv.Cells[6].Value), Convert.ToDecimal(dgv.Cells[7].Value), dgv.Cells[8].Value, dgv.Cells[9].Value, dgv.Cells[10].Value, dgv.Cells[11].Value, Convert.ToDecimal(dgv.Cells[12].Value), cmbAño.Text, cmbMes.Text, cbVendedor.Text);
		}
		ds.Tables.Add(dt);
		ds.WriteXml("C:\\XML\\ComisionRPT.xml", XmlWriteMode.WriteSchema);
		CRComision rpt = new CRComision();
		frmRptComision frm = new frmRptComision();
		rpt.SetDataSource(ds);
		frm.crvComision.ReportSource = rpt;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.cbZona = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cbVendedor = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbAño = new System.Windows.Forms.ComboBox();
		this.cmbMes = new System.Windows.Forms.ComboBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtComisionTotal = new System.Windows.Forms.TextBox();
		this.dgvComisiones = new System.Windows.Forms.DataGridView();
		this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.sigla = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nro_documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codper = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.notaCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.basex = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comison = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnReporte = new System.Windows.Forms.Button();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvComisiones).BeginInit();
		base.SuspendLayout();
		this.groupBox2.AccessibleDescription = "";
		this.groupBox2.Controls.Add(this.btnReporte);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.cbZona);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.cbVendedor);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.cmbAño);
		this.groupBox2.Controls.Add(this.cmbMes);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1154, 56);
		this.groupBox2.TabIndex = 45;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Filtro";
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.label5.AutoSize = true;
		this.label5.BackColor = System.Drawing.Color.Transparent;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.SteelBlue;
		this.label5.Location = new System.Drawing.Point(820, 23);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(37, 12);
		this.label5.TabIndex = 48;
		this.label5.Text = "Zona :";
		this.cbZona.FormattingEnabled = true;
		this.cbZona.Location = new System.Drawing.Point(863, 19);
		this.cbZona.Name = "cbZona";
		this.cbZona.Size = new System.Drawing.Size(136, 21);
		this.cbZona.TabIndex = 47;
		this.cbZona.SelectionChangeCommitted += new System.EventHandler(cbZona_SelectionChangeCommitted);
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(12, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(32, 12);
		this.label2.TabIndex = 45;
		this.label2.Text = "Año :";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(186, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(34, 12);
		this.label1.TabIndex = 44;
		this.label1.Text = "Mes :";
		this.cbVendedor.FormattingEnabled = true;
		this.cbVendedor.Location = new System.Drawing.Point(536, 19);
		this.cbVendedor.Name = "cbVendedor";
		this.cbVendedor.Size = new System.Drawing.Size(221, 21);
		this.cbVendedor.TabIndex = 42;
		this.cbVendedor.SelectionChangeCommitted += new System.EventHandler(cbVendedor_SelectionChangeCommitted);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(467, 23);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(60, 12);
		this.label3.TabIndex = 43;
		this.label3.Text = "Vendedor :";
		this.cmbAño.FormattingEnabled = true;
		this.cmbAño.Location = new System.Drawing.Point(50, 19);
		this.cmbAño.Name = "cmbAño";
		this.cmbAño.Size = new System.Drawing.Size(93, 21);
		this.cmbAño.TabIndex = 11;
		this.cmbAño.SelectionChangeCommitted += new System.EventHandler(cmbAño_SelectionChangeCommitted);
		this.cmbMes.FormattingEnabled = true;
		this.cmbMes.Location = new System.Drawing.Point(226, 19);
		this.cmbMes.Name = "cmbMes";
		this.cmbMes.Size = new System.Drawing.Size(216, 21);
		this.cmbMes.TabIndex = 9;
		this.cmbMes.SelectionChangeCommitted += new System.EventHandler(cmbMes_SelectionChangeCommitted);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtComisionTotal);
		this.groupBox1.Controls.Add(this.dgvComisiones);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 56);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1154, 359);
		this.groupBox1.TabIndex = 47;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Documentos";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(985, 331);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(37, 12);
		this.label4.TabIndex = 48;
		this.label4.Text = "Total :";
		this.txtComisionTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtComisionTotal.Location = new System.Drawing.Point(1028, 327);
		this.txtComisionTotal.Name = "txtComisionTotal";
		this.txtComisionTotal.Size = new System.Drawing.Size(114, 20);
		this.txtComisionTotal.TabIndex = 47;
		this.dgvComisiones.AllowUserToAddRows = false;
		this.dgvComisiones.AllowUserToDeleteRows = false;
		this.dgvComisiones.AllowUserToResizeRows = false;
		this.dgvComisiones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvComisiones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvComisiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvComisiones.Columns.AddRange(this.apellido, this.sigla, this.nro_documento, this.codper, this.razonsocial, this.zona, this.fechasalida, this.total, this.anulado, this.notaCredito, this.monto, this.basex, this.comison);
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvComisiones.DefaultCellStyle = dataGridViewCellStyle6;
		this.dgvComisiones.Location = new System.Drawing.Point(6, 19);
		this.dgvComisiones.MultiSelect = false;
		this.dgvComisiones.Name = "dgvComisiones";
		this.dgvComisiones.ReadOnly = true;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvComisiones.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.dgvComisiones.RowHeadersVisible = false;
		this.dgvComisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvComisiones.Size = new System.Drawing.Size(1142, 302);
		this.dgvComisiones.TabIndex = 9;
		this.apellido.DataPropertyName = "apellido";
		this.apellido.HeaderText = "Vendedor";
		this.apellido.Name = "apellido";
		this.apellido.ReadOnly = true;
		this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.apellido.Width = 120;
		this.sigla.DataPropertyName = "sigla";
		this.sigla.HeaderText = "T. Doc.";
		this.sigla.Name = "sigla";
		this.sigla.ReadOnly = true;
		this.sigla.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.sigla.Width = 50;
		this.nro_documento.DataPropertyName = "nro_documento";
		this.nro_documento.HeaderText = "N Doc.";
		this.nro_documento.Name = "nro_documento";
		this.nro_documento.ReadOnly = true;
		this.nro_documento.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codper.DataPropertyName = "codigopersonalizado";
		this.codper.HeaderText = "Codigo";
		this.codper.Name = "codper";
		this.codper.ReadOnly = true;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Cliente";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.ReadOnly = true;
		this.razonsocial.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.razonsocial.Width = 250;
		this.zona.DataPropertyName = "zona";
		this.zona.HeaderText = "Zona";
		this.zona.Name = "zona";
		this.zona.ReadOnly = true;
		this.fechasalida.DataPropertyName = "fechasalida";
		this.fechasalida.HeaderText = "Fecha Salida";
		this.fechasalida.Name = "fechasalida";
		this.fechasalida.ReadOnly = true;
		this.fechasalida.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.total.DataPropertyName = "totalns";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "n2";
		this.total.DefaultCellStyle = dataGridViewCellStyle8;
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.anulado.DataPropertyName = "estado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.anulado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.notaCredito.DataPropertyName = "notaCredito";
		this.notaCredito.HeaderText = "Nota Credito";
		this.notaCredito.Name = "notaCredito";
		this.notaCredito.ReadOnly = true;
		this.notaCredito.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.monto.DataPropertyName = "totalni";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "n2";
		this.monto.DefaultCellStyle = dataGridViewCellStyle9;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.basex.DataPropertyName = "base";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "n2";
		this.basex.DefaultCellStyle = dataGridViewCellStyle10;
		this.basex.HeaderText = "Base";
		this.basex.Name = "basex";
		this.basex.ReadOnly = true;
		this.basex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.comison.DataPropertyName = "comison";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.Format = "n2";
		this.comison.DefaultCellStyle = dataGridViewCellStyle11;
		this.comison.HeaderText = "Comision";
		this.comison.Name = "comison";
		this.comison.ReadOnly = true;
		this.comison.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.comison.Width = 80;
		this.btnReporte.Location = new System.Drawing.Point(1055, 19);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(75, 23);
		this.btnReporte.TabIndex = 49;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1154, 415);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmComsionPorDocumento";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Comisiones por documento";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmComsionPorDocumento_Load);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvComisiones).EndInit();
		base.ResumeLayout(false);
	}
}
