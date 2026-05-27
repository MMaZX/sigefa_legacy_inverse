using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmListadoProductosFacturaNotaCredito : Form
{
	public DataTable dataProductos = new DataTable();

	internal frmNotadeCreditoCompra ventana1 = null;

	private DataTable dataSeleccionados = new DataTable();

	private TextBox txtedit = new TextBox();

	private clsValidar ok = new clsValidar();

	private double cant_anterior = double.NaN;

	private clsAdmNotaCreditoCompra AdmNotaCompra = new clsAdmNotaCreditoCompra();

	private IContainer components = null;

	private GroupBox groupBox2;

	public DataGridView dgvDetalle;

	private Button btnAgregar;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn flete;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn fechaingreso;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	public frmListadoProductosFacturaNotaCredito()
	{
		InitializeComponent();
	}

	private void btnAgregar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count >= 1)
		{
			if (ventana1 != null)
			{
				string filas_seleccionadas = obtenerSeleccionados();
				DialogResult rpta = MessageBox.Show("Los siguientes items se agregaran:\n" + filas_seleccionadas + "\nContinuar?", "Agregando Productos", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rpta == DialogResult.Yes)
				{
					ventana1.dataSeleccionada = dataSeleccionados;
					base.DialogResult = DialogResult.Yes;
					Close();
				}
			}
		}
		else
		{
			MessageBox.Show("Seleccione un producto", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private string obtenerSeleccionados()
	{
		string seleccionados = "Ctdad\t-\tProducto\n";
		foreach (DataGridViewRow fila in dgvDetalle.SelectedRows)
		{
			int codProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			if (!AdmNotaCompra.verificarCodProductoAgregableANotaDeCredito(codProducto))
			{
				continue;
			}
			foreach (DataRow fila_dt in dataProductos.Rows)
			{
				if (fila.Index != dataProductos.Rows.IndexOf(fila_dt))
				{
					continue;
				}
				DataRow newFila = dataSeleccionados.NewRow();
				foreach (DataColumn col_dt in dataSeleccionados.Columns)
				{
					object valor = fila_dt.Field<object>(col_dt.ColumnName);
					newFila.SetField(col_dt.ColumnName, valor);
				}
				dataSeleccionados.Rows.Add(newFila);
				seleccionados = seleccionados + Convert.ToDouble(newFila.Field<object>(cantidad.DataPropertyName)) + "\t-\t" + newFila.Field<object>(referencia.DataPropertyName).ToString().PadLeft(11, '0') + "\n";
				break;
			}
		}
		return seleccionados;
	}

	private void frmListadoProductosFacturaNotaCredito_Load(object sender, EventArgs e)
	{
		if (dataProductos.Rows.Count > 0)
		{
			dgvDetalle.DataSource = dataProductos;
			{
				foreach (DataColumn col_dt in dataProductos.Columns)
				{
					dataSeleccionados.Columns.Add(col_dt.ColumnName);
				}
				return;
			}
		}
		MessageBox.Show("Listado de Productos No Especificado", "Listado de Productos de Nota de Credito de Comrpa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private void dgvDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvDetalle_KeyPress;
			txtedit.KeyPress += dgvDetalle_KeyPress;
			txtedit.KeyUp -= dgvDetalle_KeyUp;
			txtedit.KeyUp += dgvDetalle_KeyUp;
			txtedit.Leave -= dgvDetalle_Leave;
			txtedit.Leave += dgvDetalle_Leave;
		}
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == dgvDetalle.Columns[cantidad.Name].Index)
		{
			cant_anterior = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
		}
	}

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == dgvDetalle.Columns[referencia.Name].Index)
		{
			btnAgregar.PerformClick();
		}
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		double cant_nueva = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
		if (cant_nueva > cant_anterior)
		{
			MessageBox.Show("No puede ingresar una cantidad mayor a la indicada en el producto", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = cant_anterior;
		}
		cant_anterior = double.NaN;
	}

	private void frmListadoProductosFacturaNotaCredito_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (base.DialogResult != DialogResult.Yes)
		{
			ventana1.dataSeleccionada = null;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnAgregar = new System.Windows.Forms.Button();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		base.SuspendLayout();
		this.groupBox2.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(800, 316);
		this.groupBox2.TabIndex = 21;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Productos";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.moneda, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.flete, this.precioventa, this.precioreal, this.valoreal, this.fechaingreso, this.coduser, this.fecharegistro);
		this.dgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(791, 294);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvDetalle_CellBeginEdit);
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 90;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 250;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 80;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle2;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle3;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N2";
		dataGridViewCellStyle7.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle7;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.flete.DataPropertyName = "flete";
		this.flete.HeaderText = "Flete";
		this.flete.Name = "flete";
		this.flete.ReadOnly = true;
		this.flete.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle10;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.DataPropertyName = "precioreal";
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.ReadOnly = true;
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.fechaingreso.DataPropertyName = "fechaingreso";
		this.fechaingreso.HeaderText = "FechaIngre";
		this.fechaingreso.Name = "fechaingreso";
		this.fechaingreso.ReadOnly = true;
		this.fechaingreso.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.btnAgregar.Image = SIGEFA.Properties.Resources.agregar;
		this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAgregar.Location = new System.Drawing.Point(712, 322);
		this.btnAgregar.Name = "btnAgregar";
		this.btnAgregar.Size = new System.Drawing.Size(82, 33);
		this.btnAgregar.TabIndex = 22;
		this.btnAgregar.Text = "Agregar";
		this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAgregar.UseVisualStyleBackColor = true;
		this.btnAgregar.Click += new System.EventHandler(btnAgregar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(800, 362);
		base.Controls.Add(this.btnAgregar);
		base.Controls.Add(this.groupBox2);
		base.Name = "frmListadoProductosFacturaNotaCredito";
		this.Text = "Listado de Productos de Factura para Nota Credito de Compra";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmListadoProductosFacturaNotaCredito_FormClosing);
		base.Load += new System.EventHandler(frmListadoProductosFacturaNotaCredito_Load);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
