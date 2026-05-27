using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;

namespace SIGEFA.Formularios;

public class frmCacularComision : Office2007Form
{
	private clsAdmVendedor vendedor = new clsAdmVendedor();

	private IContainer components = null;

	private Label label2;

	private Label label1;

	private DateTimePicker dtpFechaFinal;

	private DateTimePicker dtpFechaInicial;

	private Label label3;

	private GroupBox groupBox1;

	private DataGridView dgvComisiones;

	private ComboBox cbVendedor;

	private DataGridViewTextBoxColumn apellido;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn comision;

	private TextBox txtComisionTotal;

	private Label label4;

	public frmCacularComision()
	{
		InitializeComponent();
	}

	private void CargaComisiones()
	{
		dgvComisiones.DataSource = vendedor.MuestraComisones(dtpFechaInicial.Value, dtpFechaFinal.Value);
		dgvComisiones.ClearSelection();
	}

	private void filtroComisiones()
	{
		dgvComisiones.DataSource = vendedor.MuestraComisonesFiltros(Convert.ToInt32(cbVendedor.SelectedValue), Convert.ToDateTime(dtpFechaInicial.Value), Convert.ToDateTime(dtpFechaFinal.Value));
		dgvComisiones.ClearSelection();
	}

	private void CargarVendedores()
	{
		cbVendedor.DataSource = vendedor.MuestraVendedoresCombo();
		cbVendedor.DisplayMember = "apellido";
		cbVendedor.ValueMember = "codVendedor";
		cbVendedor.SelectedIndex = -1;
	}

	private void CalcularComision()
	{
		decimal comision_total = default(decimal);
		decimal comision1 = default(decimal);
		if (dgvComisiones.Rows.Count > 0)
		{
			for (int i = 0; i <= dgvComisiones.Rows.Count - 1; i++)
			{
				comision1 = Convert.ToDecimal(dgvComisiones.Rows[i].Cells[comision.Name].Value);
				comision_total += comision1;
			}
		}
		txtComisionTotal.Text = comision_total.ToString();
	}

	private void frmCacularComision_Load(object sender, EventArgs e)
	{
		DateTime iniciomes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		dtpFechaInicial.Value = iniciomes;
		CargarVendedores();
		CargaComisiones();
		CalcularComision();
	}

	private void btnCalcular_Click(object sender, EventArgs e)
	{
		filtroComisiones();
	}

	private void cbVendedor_SelectionChangeCommitted(object sender, EventArgs e)
	{
		filtroComisiones();
		CalcularComision();
	}

	private void dtpFechaInicial_ValueChanged(object sender, EventArgs e)
	{
		filtroComisiones();
		CalcularComision();
	}

	private void dtpFechaFinal_ValueChanged(object sender, EventArgs e)
	{
		filtroComisiones();
		CalcularComision();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
		this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvComisiones = new System.Windows.Forms.DataGridView();
		this.apellido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cbVendedor = new System.Windows.Forms.ComboBox();
		this.txtComisionTotal = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvComisiones).BeginInit();
		base.SuspendLayout();
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.SteelBlue;
		this.label2.Location = new System.Drawing.Point(288, 39);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 12);
		this.label2.TabIndex = 40;
		this.label2.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(60, 39);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(37, 12);
		this.label1.TabIndex = 39;
		this.label1.Text = "Desde";
		this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaFinal.Location = new System.Drawing.Point(329, 38);
		this.dtpFechaFinal.Name = "dtpFechaFinal";
		this.dtpFechaFinal.Size = new System.Drawing.Size(112, 20);
		this.dtpFechaFinal.TabIndex = 3;
		this.dtpFechaFinal.ValueChanged += new System.EventHandler(dtpFechaFinal_ValueChanged);
		this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaInicial.Location = new System.Drawing.Point(129, 39);
		this.dtpFechaInicial.Name = "dtpFechaInicial";
		this.dtpFechaInicial.Size = new System.Drawing.Size(136, 20);
		this.dtpFechaInicial.TabIndex = 2;
		this.dtpFechaInicial.ValueChanged += new System.EventHandler(dtpFechaInicial_ValueChanged);
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(60, 16);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(53, 12);
		this.label3.TabIndex = 41;
		this.label3.Text = "Vendedor";
		this.groupBox1.Controls.Add(this.dgvComisiones);
		this.groupBox1.Location = new System.Drawing.Point(12, 74);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(493, 231);
		this.groupBox1.TabIndex = 43;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Comisión total por vendedor";
		this.dgvComisiones.AllowUserToAddRows = false;
		this.dgvComisiones.AllowUserToDeleteRows = false;
		this.dgvComisiones.AllowUserToResizeRows = false;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvComisiones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
		this.dgvComisiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvComisiones.Columns.AddRange(this.apellido, this.descripcion, this.comision);
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvComisiones.DefaultCellStyle = dataGridViewCellStyle11;
		this.dgvComisiones.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvComisiones.Location = new System.Drawing.Point(3, 16);
		this.dgvComisiones.MultiSelect = false;
		this.dgvComisiones.Name = "dgvComisiones";
		this.dgvComisiones.ReadOnly = true;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvComisiones.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
		this.dgvComisiones.RowHeadersVisible = false;
		this.dgvComisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvComisiones.Size = new System.Drawing.Size(487, 212);
		this.dgvComisiones.TabIndex = 9;
		this.apellido.DataPropertyName = "apellido";
		this.apellido.HeaderText = "Apellido";
		this.apellido.Name = "apellido";
		this.apellido.ReadOnly = true;
		this.apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.apellido.Width = 180;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 180;
		this.comision.DataPropertyName = "comision";
		this.comision.HeaderText = "Comision";
		this.comision.Name = "comision";
		this.comision.ReadOnly = true;
		this.comision.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cbVendedor.FormattingEnabled = true;
		this.cbVendedor.Location = new System.Drawing.Point(129, 12);
		this.cbVendedor.Name = "cbVendedor";
		this.cbVendedor.Size = new System.Drawing.Size(312, 21);
		this.cbVendedor.TabIndex = 1;
		this.cbVendedor.SelectionChangeCommitted += new System.EventHandler(cbVendedor_SelectionChangeCommitted);
		this.txtComisionTotal.Location = new System.Drawing.Point(373, 311);
		this.txtComisionTotal.Name = "txtComisionTotal";
		this.txtComisionTotal.Size = new System.Drawing.Size(123, 20);
		this.txtComisionTotal.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.SteelBlue;
		this.label4.Location = new System.Drawing.Point(275, 315);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(92, 12);
		this.label4.TabIndex = 46;
		this.label4.Text = "Total Comisiones";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(517, 349);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.txtComisionTotal);
		base.Controls.Add(this.cbVendedor);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.dtpFechaFinal);
		base.Controls.Add(this.dtpFechaInicial);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCacularComision";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Comision por Vendedor";
		base.Load += new System.EventHandler(frmCacularComision_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvComisiones).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
