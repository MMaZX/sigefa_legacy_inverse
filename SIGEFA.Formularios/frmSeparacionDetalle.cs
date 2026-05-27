using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmSeparacionDetalle : Form
{
	private IContainer components = null;

	private GroupBox groupBox1;

	public DataGridView dgvDetalle;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codSalida1;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

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

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn MiTipoImpuesto;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn stockPend;

	private DataGridViewTextBoxColumn dsctoMax;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn cantreserva;

	private Label label37;

	public TextBox txtNumero;

	private TextBox txtSerie;

	private TextBox txtDocRef;

	private Label label5;

	private TextBox txtDireccionCliente;

	private Label label16;

	private TextBox txtNombreCliente;

	public TextBox txtCodCliente;

	private Label label15;

	private Button btnSalir;

	public frmSeparacionDetalle()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle77 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle78 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle79 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle80 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle81 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle82 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle83 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle84 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle85 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle86 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle87 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle88 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codSalida1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.MiTipoImpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockPend = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dsctoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantreserva = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label37 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDireccionCliente = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtNombreCliente = new System.Windows.Forms.TextBox();
		this.txtCodCliente = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvDetalle);
		this.groupBox1.Location = new System.Drawing.Point(10, 94);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(741, 270);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle Productos";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle66.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle66.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle66.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle66.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle66.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle66.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle66;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codSalida1, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.MiTipoImpuesto, this.precioreal, this.valoreal, this.stockPend, this.dsctoMax, this.coduser, this.fecharegistro, this.cantreserva);
		dataGridViewCellStyle77.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle77.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle77.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle77.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle77.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle77.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle77.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle77;
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		dataGridViewCellStyle78.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle78.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle78.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle78.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle78.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle78.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle78.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.RowHeadersDefaultCellStyle = dataGridViewCellStyle78;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(735, 251);
		this.dgvDetalle.TabIndex = 4;
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codSalida1.HeaderText = "CodSalida";
		this.codSalida1.Name = "codSalida1";
		this.codSalida1.ReadOnly = true;
		this.codSalida1.Visible = false;
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
		this.referencia.Width = 87;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 400;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 75;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle79.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle79.Format = "N2";
		dataGridViewCellStyle79.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle79;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 70;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle80.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle80.Format = "N2";
		dataGridViewCellStyle80.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle80;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Visible = false;
		this.preciounit.Width = 75;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle81.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle81.Format = "N2";
		dataGridViewCellStyle81.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle81;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.Visible = false;
		this.importe.Width = 85;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle82.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle82.Format = "N2";
		dataGridViewCellStyle82.NullValue = null;
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle82;
		this.dscto1.HeaderText = "Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle83.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle83;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle84.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle84;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle85.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle85.Format = "N2";
		dataGridViewCellStyle85.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle85;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.montodscto.Width = 80;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle86.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle86.Format = "N2";
		dataGridViewCellStyle86.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle86;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.valorventa.Width = 85;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle87.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle87.Format = "N2";
		dataGridViewCellStyle87.NullValue = null;
		this.igv.DefaultCellStyle = dataGridViewCellStyle87;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.igv.Width = 85;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle88.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle88.Format = "N2";
		dataGridViewCellStyle88.NullValue = null;
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle88;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Visible = false;
		this.precioventa.Width = 85;
		this.MiTipoImpuesto.DataPropertyName = "tipoimpuesto";
		this.MiTipoImpuesto.HeaderText = "TIPOIMPUESTO";
		this.MiTipoImpuesto.Name = "MiTipoImpuesto";
		this.MiTipoImpuesto.ReadOnly = true;
		this.MiTipoImpuesto.Visible = false;
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
		this.stockPend.DataPropertyName = "stockdisponible";
		this.stockPend.HeaderText = "StockDisponible";
		this.stockPend.Name = "stockPend";
		this.stockPend.ReadOnly = true;
		this.stockPend.Visible = false;
		this.dsctoMax.DataPropertyName = "maxPorcDescto";
		this.dsctoMax.HeaderText = "%DsctoMax";
		this.dsctoMax.Name = "dsctoMax";
		this.dsctoMax.ReadOnly = true;
		this.dsctoMax.Visible = false;
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
		this.cantreserva.HeaderText = "Cantidad Recojer";
		this.cantreserva.Name = "cantreserva";
		this.cantreserva.ReadOnly = true;
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.Location = new System.Drawing.Point(154, 12);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(8, 12);
		this.label37.TabIndex = 108;
		this.label37.Text = "-";
		this.txtNumero.Enabled = false;
		this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNumero.Location = new System.Drawing.Point(165, 10);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(69, 18);
		this.txtNumero.TabIndex = 106;
		this.txtSerie.BackColor = System.Drawing.Color.White;
		this.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.Location = new System.Drawing.Point(114, 10);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(38, 18);
		this.txtSerie.TabIndex = 105;
		this.txtSerie.Tag = "13";
		this.txtSerie.Visible = false;
		this.txtDocRef.BackColor = System.Drawing.Color.White;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(80, 10);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 18);
		this.txtDocRef.TabIndex = 104;
		this.txtDocRef.Tag = "10";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(11, 13);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(45, 12);
		this.label5.TabIndex = 107;
		this.label5.Text = "Doc. Ref.";
		this.txtDireccionCliente.BackColor = System.Drawing.Color.White;
		this.txtDireccionCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDireccionCliente.Location = new System.Drawing.Point(80, 62);
		this.txtDireccionCliente.Name = "txtDireccionCliente";
		this.txtDireccionCliente.ReadOnly = true;
		this.txtDireccionCliente.Size = new System.Drawing.Size(501, 18);
		this.txtDireccionCliente.TabIndex = 111;
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.Location = new System.Drawing.Point(10, 63);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(44, 12);
		this.label16.TabIndex = 113;
		this.label16.Text = "Direccion";
		this.txtNombreCliente.BackColor = System.Drawing.Color.White;
		this.txtNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtNombreCliente.Location = new System.Drawing.Point(186, 36);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.ReadOnly = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(395, 18);
		this.txtNombreCliente.TabIndex = 110;
		this.txtNombreCliente.Tag = "3";
		this.txtCodCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCodCliente.Location = new System.Drawing.Point(80, 36);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.Size = new System.Drawing.Size(100, 18);
		this.txtCodCliente.TabIndex = 109;
		this.txtCodCliente.Tag = "5";
		this.txtCodCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(10, 37);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(48, 12);
		this.label15.TabIndex = 112;
		this.label15.Text = "Doc. Ident";
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnSalir.Location = new System.Drawing.Point(686, 390);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(62, 38);
		this.btnSalir.TabIndex = 114;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(760, 440);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.txtDireccionCliente);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.txtNombreCliente);
		base.Controls.Add(this.txtCodCliente);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label37);
		base.Controls.Add(this.txtNumero);
		base.Controls.Add(this.txtSerie);
		base.Controls.Add(this.txtDocRef);
		base.Controls.Add(this.label5);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmSeparacionDetalle";
		this.Text = "frmSeparacionDetalle";
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
