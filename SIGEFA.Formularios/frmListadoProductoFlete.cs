using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoProductoFlete : Form
{
	private clsProducto prod = new clsProducto();

	private clsAdmProducto admprod = new clsAdmProducto();

	public DataTable data = new DataTable();

	private TextBox txtedit = new TextBox();

	private clsValidar ok = new clsValidar();

	private clsAdmOrdenCompra admOrdenCompra = new clsAdmOrdenCompra();

	public int codordencompra;

	private double cantidad_previa;

	private Color fondo_celda;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvListadoProductoFlete;

	private GroupBox groupBox2;

	private TextBox txtTotalSinIgv;

	private Label label2;

	private TextBox txtTotalConIgv;

	private Label label1;

	private RadButton btnactualizarf;

	private RadCheckBox rdchbeditar;

	private DataGridViewTextBoxColumn colCodProducto;

	private DataGridViewTextBoxColumn colRefProducto;

	private DataGridViewTextBoxColumn colDescripProducto;

	private DataGridViewTextBoxColumn colCodUnidad;

	private DataGridViewTextBoxColumn colUnidad;

	private DataGridViewTextBoxColumn codCodEquivalente;

	private DataGridViewTextBoxColumn colCantidad;

	private DataGridViewTextBoxColumn colFlete;

	private DataGridViewTextBoxColumn colSubtotal;

	public frmListadoProductoFlete()
	{
		InitializeComponent();
	}

	private void frmListadoProductoFlete_Load(object sender, EventArgs e)
	{
		asignarDataADgv();
		foreach (DataGridViewColumn col in dgvListadoProductoFlete.Columns)
		{
			col.ReadOnly = true;
		}
		recalculaTotalesFlete();
	}

	private void recalculaTotalesFlete()
	{
		double Suma_Total = 0.0;
		foreach (DataGridViewRow fila in (IEnumerable)dgvListadoProductoFlete.Rows)
		{
			Suma_Total += Convert.ToDouble((fila.Cells[colSubtotal.Name].Value == "" || fila.Cells[colSubtotal.Name].Value == DBNull.Value) ? "0" : fila.Cells[colSubtotal.Name].Value);
		}
		double suma_sin_igv = Suma_Total / 1.18;
		txtTotalSinIgv.Text = suma_sin_igv.ToString("0.00##");
		txtTotalConIgv.Text = Suma_Total.ToString("0.00##");
	}

	private void asignarDataADgv()
	{
		dgvListadoProductoFlete.DataSource = data;
	}

	private void dgvListadoProductoFlete_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
	}

	private void dgvListadoProductoFlete_celltextbox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvListadoProductoFlete.CurrentCell.OwningColumn.Name == colFlete.Name)
		{
			ok.NumerosDecimales(e, sender as TextBox);
		}
	}

	private void dgvListadoProductoFlete_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		cantidad_previa = Convert.ToDouble(dgvListadoProductoFlete.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
	}

	private void dgvListadoProductoFlete_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			foreach (DataGridViewColumn col in dgvListadoProductoFlete.Columns)
			{
				if (!(col.Name == colFlete.Name))
				{
					continue;
				}
				DataGridViewRow fila = dgvListadoProductoFlete.Rows[e.RowIndex];
				object nuevo_valor = fila.Cells[e.ColumnIndex].Value;
				if (Convert.ToDouble(nuevo_valor) != cantidad_previa)
				{
					int codProd = Convert.ToInt32(fila.Cells[colCodProducto.Name].Value);
					int codUni = Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value);
					prod = admprod.CargaProducto(codProd, frmLogin.iCodAlmacen);
					double flete = 0.0;
					if (prod.CodUnidadMedida != codUni)
					{
						double auxflete = Convert.ToDouble(fila.Cells[colFlete.Name].Value);
						clsAdmUnidadEquivalente admundequi = new clsAdmUnidadEquivalente();
						clsUnidadEquivalente undequi = admprod.CargaUnidadEquivalente(codUni, codProd, 2);
						flete = auxflete / Convert.ToDouble(undequi.Factor);
					}
					else
					{
						flete = Convert.ToDouble(fila.Cells[colFlete.Name].Value);
					}
					fila.Cells[colSubtotal.Name].Value = Convert.ToDouble(nuevo_valor) * Convert.ToDouble(fila.Cells[colCantidad.Name].Value);
					recalculaTotalesFlete();
					DetalleModificarPrecio obj = new DetalleModificarPrecio
					{
						codProducto = Convert.ToInt32(dgvListadoProductoFlete.Rows[e.RowIndex].Cells["colCodProducto"].Value),
						codUnidadMedida = Convert.ToInt32(dgvListadoProductoFlete.Rows[e.RowIndex].Cells["colCodUnidad"].Value),
						codUnidadEquivalente = Convert.ToInt32(dgvListadoProductoFlete.Rows[e.RowIndex].Cells["codCodEquivalente"].Value),
						fleteNuevo = Math.Round(Convert.ToDouble(dgvListadoProductoFlete.Rows[e.RowIndex].Cells["colFlete"].Value) / 1.18, 4),
						precioCompraNuevo = 0.0,
						precioVentaNuevo = 0.0,
						codordencompra = codordencompra
					};
					admOrdenCompra.enviarDatoModificarPrecio(obj);
				}
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Problema al calcular los fletes");
		}
	}

	private void dgvListadoProductoFlete_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		fondo_celda = dgvListadoProductoFlete.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
		if (e.ColumnIndex == dgvListadoProductoFlete.Columns[colFlete.Name].Index)
		{
			dgvListadoProductoFlete.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = Color.FromArgb(0, 192, 0);
		}
	}

	private void dgvListadoProductoFlete_CellLeave(object sender, DataGridViewCellEventArgs e)
	{
		_ = fondo_celda;
		if (false)
		{
			fondo_celda = dgvListadoProductoFlete.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor;
		}
		dgvListadoProductoFlete.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.SelectionBackColor = fondo_celda;
	}

	private void rdchbeditar_ToggleStateChanged(object sender, StateChangedEventArgs args)
	{
		if (rdchbeditar.Checked)
		{
			frmAutorizacion frm = new frmAutorizacion();
			DialogResult dr = DialogResult.None;
			dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				dgvListadoProductoFlete.ReadOnly = false;
				dgvListadoProductoFlete.CurrentCell = dgvListadoProductoFlete.Rows[0].Cells["colFlete"];
				dgvListadoProductoFlete.CurrentRow.Cells["colFlete"].Selected = true;
				dgvListadoProductoFlete.CurrentRow.Cells["colSubtotal"].Selected = true;
				dgvListadoProductoFlete.BeginEdit(selectAll: true);
				{
					foreach (DataGridViewColumn col in dgvListadoProductoFlete.Columns)
					{
						if (col.Name == colFlete.Name)
						{
							col.ReadOnly = false;
						}
						else
						{
							col.ReadOnly = true;
						}
					}
					return;
				}
			}
			rdchbeditar.Checked = false;
		}
		else
		{
			rdchbeditar.Checked = false;
		}
	}

	private void btnactualizarf_Click(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvListadoProductoFlete = new System.Windows.Forms.DataGridView();
		this.colCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colRefProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescripProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codCodEquivalente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFlete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnactualizarf = new Telerik.WinControls.UI.RadButton();
		this.rdchbeditar = new Telerik.WinControls.UI.RadCheckBox();
		this.txtTotalSinIgv = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtTotalConIgv = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvListadoProductoFlete).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnactualizarf).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rdchbeditar).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvListadoProductoFlete);
		this.groupBox1.Location = new System.Drawing.Point(12, 95);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(660, 305);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.dgvListadoProductoFlete.AllowUserToAddRows = false;
		this.dgvListadoProductoFlete.AllowUserToDeleteRows = false;
		this.dgvListadoProductoFlete.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvListadoProductoFlete.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
		this.dgvListadoProductoFlete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvListadoProductoFlete.Columns.AddRange(this.colCodProducto, this.colRefProducto, this.colDescripProducto, this.colCodUnidad, this.colUnidad, this.codCodEquivalente, this.colCantidad, this.colFlete, this.colSubtotal);
		this.dgvListadoProductoFlete.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvListadoProductoFlete.Location = new System.Drawing.Point(3, 16);
		this.dgvListadoProductoFlete.Name = "dgvListadoProductoFlete";
		this.dgvListadoProductoFlete.ReadOnly = true;
		this.dgvListadoProductoFlete.RowHeadersVisible = false;
		this.dgvListadoProductoFlete.Size = new System.Drawing.Size(654, 286);
		this.dgvListadoProductoFlete.TabIndex = 0;
		this.dgvListadoProductoFlete.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvListadoProductoFlete_CellBeginEdit);
		this.dgvListadoProductoFlete.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvListadoProductoFlete_CellEndEdit);
		this.dgvListadoProductoFlete.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(dgvListadoProductoFlete_CellEnter);
		this.dgvListadoProductoFlete.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(dgvListadoProductoFlete_CellLeave);
		this.dgvListadoProductoFlete.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvListadoProductoFlete_EditingControlShowing);
		this.colCodProducto.DataPropertyName = "codProducto";
		this.colCodProducto.HeaderText = "colCodProducto";
		this.colCodProducto.Name = "colCodProducto";
		this.colCodProducto.ReadOnly = true;
		this.colCodProducto.Visible = false;
		this.colRefProducto.DataPropertyName = "refProducto";
		this.colRefProducto.FillWeight = 69.79695f;
		this.colRefProducto.HeaderText = "Referencia";
		this.colRefProducto.MinimumWidth = 95;
		this.colRefProducto.Name = "colRefProducto";
		this.colRefProducto.ReadOnly = true;
		this.colDescripProducto.DataPropertyName = "descripProducto";
		this.colDescripProducto.FillWeight = 220.8122f;
		this.colDescripProducto.HeaderText = "Descripcion";
		this.colDescripProducto.MinimumWidth = 200;
		this.colDescripProducto.Name = "colDescripProducto";
		this.colDescripProducto.ReadOnly = true;
		this.colCodUnidad.DataPropertyName = "codUnidad";
		this.colCodUnidad.HeaderText = "colCodUnidad";
		this.colCodUnidad.Name = "colCodUnidad";
		this.colCodUnidad.ReadOnly = true;
		this.colCodUnidad.Visible = false;
		this.colUnidad.DataPropertyName = "unidad";
		this.colUnidad.HeaderText = "Unidad";
		this.colUnidad.Name = "colUnidad";
		this.colUnidad.ReadOnly = true;
		this.codCodEquivalente.DataPropertyName = "codUnidadEquivalente";
		this.codCodEquivalente.HeaderText = "codCodEquivalente";
		this.codCodEquivalente.Name = "codCodEquivalente";
		this.codCodEquivalente.ReadOnly = true;
		this.codCodEquivalente.Visible = false;
		this.colCantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N4";
		dataGridViewCellStyle14.NullValue = "0";
		this.colCantidad.DefaultCellStyle = dataGridViewCellStyle14;
		this.colCantidad.FillWeight = 69.79695f;
		this.colCantidad.HeaderText = "Cantidad";
		this.colCantidad.Name = "colCantidad";
		this.colCantidad.ReadOnly = true;
		this.colFlete.DataPropertyName = "flete";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N4";
		dataGridViewCellStyle15.NullValue = null;
		this.colFlete.DefaultCellStyle = dataGridViewCellStyle15;
		this.colFlete.FillWeight = 69.79695f;
		this.colFlete.HeaderText = "Flete Unitario Con Igv";
		this.colFlete.Name = "colFlete";
		this.colFlete.ReadOnly = true;
		this.colSubtotal.DataPropertyName = "subtotal";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N4";
		dataGridViewCellStyle16.NullValue = null;
		this.colSubtotal.DefaultCellStyle = dataGridViewCellStyle16;
		this.colSubtotal.FillWeight = 69.79695f;
		this.colSubtotal.HeaderText = "Sub Total";
		this.colSubtotal.Name = "colSubtotal";
		this.colSubtotal.ReadOnly = true;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.btnactualizarf);
		this.groupBox2.Controls.Add(this.rdchbeditar);
		this.groupBox2.Controls.Add(this.txtTotalSinIgv);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtTotalConIgv);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Location = new System.Drawing.Point(12, 12);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(657, 77);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.btnactualizarf.Location = new System.Drawing.Point(107, 35);
		this.btnactualizarf.Name = "btnactualizarf";
		this.btnactualizarf.Size = new System.Drawing.Size(110, 24);
		this.btnactualizarf.TabIndex = 5;
		this.btnactualizarf.Text = "Actualizar F";
		this.btnactualizarf.Click += new System.EventHandler(btnactualizarf_Click);
		this.rdchbeditar.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.rdchbeditar.Location = new System.Drawing.Point(224, 36);
		this.rdchbeditar.Name = "rdchbeditar";
		this.rdchbeditar.Size = new System.Drawing.Size(61, 24);
		this.rdchbeditar.TabIndex = 4;
		this.rdchbeditar.Text = "Editar";
		this.rdchbeditar.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(rdchbeditar_ToggleStateChanged);
		this.txtTotalSinIgv.Location = new System.Drawing.Point(402, 51);
		this.txtTotalSinIgv.Name = "txtTotalSinIgv";
		this.txtTotalSinIgv.Size = new System.Drawing.Size(114, 20);
		this.txtTotalSinIgv.TabIndex = 3;
		this.txtTotalSinIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(399, 35);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(80, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Total Sin Igv";
		this.txtTotalConIgv.Location = new System.Drawing.Point(537, 51);
		this.txtTotalConIgv.Name = "txtTotalConIgv";
		this.txtTotalConIgv.Size = new System.Drawing.Size(114, 20);
		this.txtTotalConIgv.TabIndex = 1;
		this.txtTotalConIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(534, 35);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(84, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Total Con Igv";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(684, 412);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.MaximumSize = new System.Drawing.Size(700, 451);
		this.MinimumSize = new System.Drawing.Size(700, 451);
		base.Name = "frmListadoProductoFlete";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Listado de Flete de Producto";
		base.Load += new System.EventHandler(frmListadoProductoFlete_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvListadoProductoFlete).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnactualizarf).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rdchbeditar).EndInit();
		base.ResumeLayout(false);
	}
}
