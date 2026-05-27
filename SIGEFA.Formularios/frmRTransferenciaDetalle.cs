using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRTransferenciaDetalle : Office2007Form
{
	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	private clsTipoDocumento doc = new clsTipoDocumento();

	public int CodCliente;

	public int CodDocumento;

	public int Tipo;

	public int Proceso = 0;

	public int codalmacenselec;

	public int cboalma = 0;

	public string NombreCliente;

	public string CodPedido;

	public string NombreAlmacen;

	internal frmReqAlmacen ventanaRA = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox txtPedido;

	private Label label1;

	private TextBox txtSerie;

	public TextBox txtDocRef;

	private Label label12;

	private GroupBox groupBox2;

	private Button btnSalir;

	private Button btnAceptar;

	private DataGridView dgvdetalle2;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codprod;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

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

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valorpromedio;

	private DataGridViewTextBoxColumn precioigv;

	private DataGridViewCheckBoxColumn seleccion;

	private CheckBox chkTodos;

	private void chkTodos_CheckedChanged(object sender, EventArgs e)
	{
		foreach (DataGridViewRow dr in (IEnumerable)dgvdetalle2.Rows)
		{
			dr.Cells[20].Value = chkTodos.Checked;
		}
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		List<DataGridViewRow> rowSelected = new List<DataGridViewRow>();
		foreach (DataGridViewRow dr in (IEnumerable)dgvdetalle2.Rows)
		{
			if (Convert.ToBoolean(dr.Cells[20].Value))
			{
				rowSelected.Add(dr);
			}
		}
		if (rowSelected.Count > 0)
		{
			ventanaRA.filasSelecccionadasDetalle = rowSelected;
			base.DialogResult = DialogResult.Yes;
			Close();
		}
		else
		{
			MessageBox.Show("Tiene que seleccionar algun elemento", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	private void frmRTransferenciaDetalle_Load(object sender, EventArgs e)
	{
		CargaPedido();
	}

	public frmRTransferenciaDetalle()
	{
		InitializeComponent();
	}

	private void CargaDetalle()
	{
		DataTable newData = new DataTable();
		dgvdetalle2.Rows.Clear();
		try
		{
			newData = AdmPedido.CargaDetalle2(Convert.ToInt32(pedido.CodPedido), codalmacenselec);
			foreach (DataRow row in newData.Rows)
			{
				dgvdetalle2.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[19].ToString(), row[18].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaPedido()
	{
		try
		{
			pedido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
			if (pedido != null)
			{
				doc.CodTipoDocumento = pedido.CodTipoDocumento;
				pedido.CodCotizacion = 0;
				txtSerie.Text = pedido.SerieDoc;
				txtPedido.Text = pedido.Numeracion.ToString().PadLeft(8, '0');
				if (txtDocRef.Enabled)
				{
					CodDocumento = pedido.CodTipoDocumento;
					txtDocRef.Text = pedido.SiglaDocumento;
				}
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRTransferenciaDetalle));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtPedido = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvdetalle2 = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.seleccion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.chkTodos = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvdetalle2).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtPedido);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Location = new System.Drawing.Point(5, 4);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(508, 70);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Seleccionar Productos a Realizar Requerimiento de Almacen Tipo Venta";
		this.txtPedido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPedido.ForeColor = System.Drawing.Color.Red;
		this.txtPedido.Location = new System.Drawing.Point(350, 19);
		this.txtPedido.Multiline = true;
		this.txtPedido.Name = "txtPedido";
		this.txtPedido.ReadOnly = true;
		this.txtPedido.Size = new System.Drawing.Size(152, 37);
		this.txtPedido.TabIndex = 51;
		this.txtPedido.Text = "00000000";
		this.txtPedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.Black;
		this.label1.Location = new System.Drawing.Point(313, 14);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(31, 42);
		this.label1.TabIndex = 49;
		this.label1.Tag = "22";
		this.label1.Text = "-";
		this.label1.Visible = false;
		this.txtSerie.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtSerie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtSerie.ForeColor = System.Drawing.Color.Red;
		this.txtSerie.Location = new System.Drawing.Point(240, 20);
		this.txtSerie.Margin = new System.Windows.Forms.Padding(5);
		this.txtSerie.Multiline = true;
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(65, 37);
		this.txtSerie.TabIndex = 50;
		this.txtSerie.Text = "000";
		this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDocRef.Location = new System.Drawing.Point(74, 20);
		this.txtDocRef.Multiline = true;
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(45, 36);
		this.txtDocRef.TabIndex = 47;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.Text = "L";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(15, 31);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(53, 13);
		this.label12.TabIndex = 48;
		this.label12.Text = "Doc. Ref.";
		this.groupBox2.Controls.Add(this.dgvdetalle2);
		this.groupBox2.Location = new System.Drawing.Point(4, 84);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(710, 253);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle de Producto";
		this.dgvdetalle2.AllowUserToAddRows = false;
		this.dgvdetalle2.AllowUserToDeleteRows = false;
		this.dgvdetalle2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvdetalle2.Columns.AddRange(this.coddetalle, this.codprod, this.codigo, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.valoreal, this.precioreal, this.valorpromedio, this.precioigv, this.seleccion);
		this.dgvdetalle2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvdetalle2.Location = new System.Drawing.Point(3, 16);
		this.dgvdetalle2.Name = "dgvdetalle2";
		this.dgvdetalle2.RowHeadersVisible = false;
		this.dgvdetalle2.Size = new System.Drawing.Size(704, 234);
		this.dgvdetalle2.TabIndex = 0;
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Visible = false;
		this.codprod.HeaderText = "codprod";
		this.codprod.Name = "codprod";
		this.codprod.Visible = false;
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Width = 80;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 400;
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Visible = false;
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Width = 80;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Width = 60;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Visible = false;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Visible = false;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Visible = false;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Visible = false;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Visible = false;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.Visible = false;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.Visible = false;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Visible = false;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.Visible = false;
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.Visible = false;
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.Visible = false;
		this.valorpromedio.HeaderText = "Valor Promedio";
		this.valorpromedio.Name = "valorpromedio";
		this.valorpromedio.Visible = false;
		this.precioigv.HeaderText = "Precio IGV";
		this.precioigv.Name = "precioigv";
		this.precioigv.Visible = false;
		this.seleccion.HeaderText = "Seleccionar";
		this.seleccion.Name = "seleccion";
		this.seleccion.Width = 80;
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSalir.FlatAppearance.BorderSize = 2;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(608, 340);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(100, 43);
		this.btnSalir.TabIndex = 8;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnAceptar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnAceptar.FlatAppearance.BorderSize = 2;
		this.btnAceptar.Image = (System.Drawing.Image)resources.GetObject("btnAceptar.Image");
		this.btnAceptar.Location = new System.Drawing.Point(503, 341);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(99, 43);
		this.btnAceptar.TabIndex = 9;
		this.btnAceptar.Text = "ACEPTAR ";
		this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.chkTodos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.chkTodos.AutoSize = true;
		this.chkTodos.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
		this.chkTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkTodos.Location = new System.Drawing.Point(646, 47);
		this.chkTodos.Name = "chkTodos";
		this.chkTodos.Size = new System.Drawing.Size(46, 31);
		this.chkTodos.TabIndex = 10;
		this.chkTodos.Text = "Todos";
		this.chkTodos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.chkTodos.UseVisualStyleBackColor = true;
		this.chkTodos.CheckedChanged += new System.EventHandler(chkTodos_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(715, 387);
		base.Controls.Add(this.chkTodos);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmRTransferenciaDetalle";
		this.Text = "Detalle Productos Requerimiento Almacen Tipo Venta";
		base.Load += new System.EventHandler(frmRTransferenciaDetalle_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvdetalle2).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
