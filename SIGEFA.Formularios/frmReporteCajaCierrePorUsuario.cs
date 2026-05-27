using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

public class frmReporteCajaCierrePorUsuario : Form
{
	public DataTable data_cierre;

	public DataTable data_sucursal;

	public DataTable data_totales;

	private IContainer components = null;

	private DataGridView dgvDataCierre;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private Label label5;

	private Label label6;

	private Label label7;

	private Label label8;

	private GroupBox groupBox1;

	private TextBox txtsumaingreso;

	private TextBox txtsumaegreso;

	private Label label9;

	private TextBox txtMontoCierre;

	private TextBox txtMontoApertura;

	private TextBox txtUsuario;

	private TextBox txtTelefono;

	private TextBox txtAlmacen;

	private TextBox txtDireccion;

	private TextBox txtSucursal;

	private GroupBox groupBox2;

	private DataGridViewTextBoxColumn colTipo;

	private DataGridViewTextBoxColumn colcodTipo;

	private DataGridViewTextBoxColumn colTipoMovimiento;

	private DataGridViewTextBoxColumn colcodTipoMovimiento;

	private DataGridViewTextBoxColumn colcodtipopagocaja;

	private DataGridViewTextBoxColumn colPagoCaja;

	private DataGridViewTextBoxColumn colConcepto;

	private DataGridViewTextBoxColumn colCliente;

	private DataGridViewTextBoxColumn coldoccliente;

	private DataGridViewTextBoxColumn coldocreferencia;

	private DataGridViewTextBoxColumn colImporte;

	private DataGridViewTextBoxColumn colfecha;

	private DataGridViewTextBoxColumn colfecharegistro;

	private DataGridViewTextBoxColumn colcodUser;

	private DataGridViewTextBoxColumn colnombreusuario;

	private TextBox txtdeposito_ing;

	private TextBox txtefectivo_ing;

	private Label label10;

	private TextBox txttarjeta_ing;

	private TextBox txttrans_ing;

	private Label label11;

	private Label label12;

	private Label label13;

	private TextBox txtdeposito_egr;

	private TextBox txtefectivo_egr;

	private Label label14;

	private TextBox txttarjeta_egr;

	private TextBox txttrans_egr;

	private Label label15;

	private Label label16;

	private Label label17;

	public frmReporteCajaCierrePorUsuario()
	{
		InitializeComponent();
	}

	private void frmReporteCajaCierrePorUsuario_Load(object sender, EventArgs e)
	{
		try
		{
			añadiendoDatosaDgv();
			mostrandoDatosaTxt();
			calcularSumaTotales();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Reporte Cierre Caja Por Usuario dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void calcularSumaTotales()
	{
		decimal suma_ingreso = default(decimal);
		decimal suma_egreso = default(decimal);
		decimal suma_trans_ing = default(decimal);
		decimal suma_tarjeta_ing = default(decimal);
		decimal suma_efectivo_ing = default(decimal);
		decimal suma_depositivo_ing = default(decimal);
		decimal suma_trans_egr = default(decimal);
		decimal suma_tarjeta_egr = default(decimal);
		decimal suma_efectivo_egr = default(decimal);
		decimal suma_depositivo_egr = default(decimal);
		decimal suma_otro = default(decimal);
		foreach (DataGridViewRow fila in (IEnumerable)dgvDataCierre.Rows)
		{
			if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 1)
			{
				suma_ingreso += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
			}
			else if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 2)
			{
				suma_egreso += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
			}
			switch (Convert.ToInt32(fila.Cells[colcodtipopagocaja.Name].Value))
			{
			case 5:
				if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 1)
				{
					suma_efectivo_ing += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				else if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 2)
				{
					suma_efectivo_egr += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				break;
			case 6:
				if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 1)
				{
					suma_depositivo_ing += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				else if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 2)
				{
					suma_depositivo_egr += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				break;
			case 8:
				if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 1)
				{
					suma_tarjeta_ing += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				else if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 2)
				{
					suma_tarjeta_egr += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				break;
			case 9:
				if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 1)
				{
					suma_trans_ing += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				else if (Convert.ToInt32(fila.Cells[colcodTipo.Name].Value) == 2)
				{
					suma_trans_egr += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				}
				break;
			default:
				suma_otro += Convert.ToDecimal(fila.Cells[colImporte.Name].Value);
				break;
			case 7:
			case 10:
			case 11:
				break;
			}
		}
		txtsumaingreso.Text = suma_ingreso.ToString();
		txtsumaegreso.Text = suma_egreso.ToString();
		txttrans_ing.Text = suma_trans_ing.ToString();
		txttarjeta_ing.Text = suma_tarjeta_ing.ToString();
		txtefectivo_ing.Text = suma_efectivo_ing.ToString();
		txtdeposito_ing.Text = suma_depositivo_ing.ToString();
		txttrans_egr.Text = suma_trans_egr.ToString();
		txttarjeta_egr.Text = suma_tarjeta_egr.ToString();
		txtefectivo_egr.Text = suma_efectivo_egr.ToString();
		txtdeposito_egr.Text = suma_depositivo_egr.ToString();
		if (suma_otro > 0m)
		{
			string cadena = "Suma De Alguna Cantidad Sin Detallar: " + suma_otro;
			MessageBox.Show(cadena, "Caja Cierre Por Usuario dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void mostrandoDatosaTxt()
	{
		txtSucursal.Text = data_sucursal.Rows[0].Field<object>("nombre").ToString();
		txtUsuario.Text = data_cierre.Rows[0].Field<object>("nombreusuario").ToString();
		txtDireccion.Text = data_sucursal.Rows[0].Field<object>("ubicacion").ToString();
		txtAlmacen.Text = data_sucursal.Rows[0].Field<object>("almacen").ToString();
		txtTelefono.Text = data_sucursal.Rows[0].Field<object>("telefono").ToString();
		txtMontoApertura.Text = data_totales.Rows[0].Field<object>("montoapertura").ToString();
		txtMontoCierre.Text = data_totales.Rows[0].Field<object>("montocierre").ToString();
	}

	private void añadiendoDatosaDgv()
	{
		dgvDataCierre.DataSource = data_cierre;
	}

	private void txtDireccion_TextChanged(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dgvDataCierre = new System.Windows.Forms.DataGridView();
		this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodTipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodtipopagocaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colPagoCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coldoccliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coldocreferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colfecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colnombreusuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtsumaingreso = new System.Windows.Forms.TextBox();
		this.txtsumaegreso = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtMontoCierre = new System.Windows.Forms.TextBox();
		this.txtMontoApertura = new System.Windows.Forms.TextBox();
		this.txtUsuario = new System.Windows.Forms.TextBox();
		this.txtTelefono = new System.Windows.Forms.TextBox();
		this.txtAlmacen = new System.Windows.Forms.TextBox();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.txtSucursal = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtdeposito_ing = new System.Windows.Forms.TextBox();
		this.txtefectivo_ing = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txttarjeta_ing = new System.Windows.Forms.TextBox();
		this.txttrans_ing = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtdeposito_egr = new System.Windows.Forms.TextBox();
		this.txtefectivo_egr = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txttarjeta_egr = new System.Windows.Forms.TextBox();
		this.txttrans_egr = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.dgvDataCierre).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.dgvDataCierre.AllowUserToAddRows = false;
		this.dgvDataCierre.AllowUserToDeleteRows = false;
		this.dgvDataCierre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDataCierre.Columns.AddRange(this.colTipo, this.colcodTipo, this.colTipoMovimiento, this.colcodTipoMovimiento, this.colcodtipopagocaja, this.colPagoCaja, this.colConcepto, this.colCliente, this.coldoccliente, this.coldocreferencia, this.colImporte, this.colfecha, this.colfecharegistro, this.colcodUser, this.colnombreusuario);
		this.dgvDataCierre.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDataCierre.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvDataCierre.Location = new System.Drawing.Point(3, 16);
		this.dgvDataCierre.MultiSelect = false;
		this.dgvDataCierre.Name = "dgvDataCierre";
		this.dgvDataCierre.RowHeadersVisible = false;
		this.dgvDataCierre.RowHeadersWidth = 25;
		this.dgvDataCierre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDataCierre.Size = new System.Drawing.Size(1164, 362);
		this.dgvDataCierre.TabIndex = 0;
		this.colTipo.DataPropertyName = "ingresoegreso";
		this.colTipo.HeaderText = "Tipo";
		this.colTipo.Name = "colTipo";
		this.colcodTipo.DataPropertyName = "tipo";
		this.colcodTipo.HeaderText = "codtipo";
		this.colcodTipo.Name = "colcodTipo";
		this.colcodTipo.Visible = false;
		this.colTipoMovimiento.DataPropertyName = "tipomovimientodescripcion";
		this.colTipoMovimiento.HeaderText = "Tipo Movimiento";
		this.colTipoMovimiento.Name = "colTipoMovimiento";
		this.colcodTipoMovimiento.DataPropertyName = "tipomovimiento";
		this.colcodTipoMovimiento.HeaderText = "codTipoMovimiento";
		this.colcodTipoMovimiento.Name = "colcodTipoMovimiento";
		this.colcodTipoMovimiento.Visible = false;
		this.colcodtipopagocaja.DataPropertyName = "codTipoPagoCaja";
		this.colcodtipopagocaja.HeaderText = "codTipoPagoCaja";
		this.colcodtipopagocaja.Name = "colcodtipopagocaja";
		this.colcodtipopagocaja.Visible = false;
		this.colPagoCaja.DataPropertyName = "pagocaja";
		this.colPagoCaja.HeaderText = "Tipo de Pago";
		this.colPagoCaja.Name = "colPagoCaja";
		this.colConcepto.DataPropertyName = "concepto";
		this.colConcepto.HeaderText = "Concepto";
		this.colConcepto.Name = "colConcepto";
		this.colConcepto.Width = 150;
		this.colCliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.colCliente.DataPropertyName = "nomcli";
		this.colCliente.HeaderText = "Cliente";
		this.colCliente.Name = "colCliente";
		this.coldoccliente.DataPropertyName = "doccli";
		this.coldoccliente.HeaderText = "Documento";
		this.coldoccliente.Name = "coldoccliente";
		this.coldocreferencia.DataPropertyName = "documentorefencia";
		this.coldocreferencia.HeaderText = "Doc. Referencia";
		this.coldocreferencia.Name = "coldocreferencia";
		this.colImporte.DataPropertyName = "monto";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.colImporte.DefaultCellStyle = dataGridViewCellStyle1;
		this.colImporte.HeaderText = "Importe";
		this.colImporte.Name = "colImporte";
		this.colfecha.DataPropertyName = "fecha";
		this.colfecha.HeaderText = "fecha";
		this.colfecha.Name = "colfecha";
		this.colfecha.Visible = false;
		this.colfecharegistro.DataPropertyName = "fecharegistro";
		this.colfecharegistro.HeaderText = "Fecha Registro";
		this.colfecharegistro.Name = "colfecharegistro";
		this.colcodUser.DataPropertyName = "codUser";
		this.colcodUser.HeaderText = "coduser";
		this.colcodUser.Name = "colcodUser";
		this.colcodUser.Visible = false;
		this.colnombreusuario.DataPropertyName = "nombreusuario";
		this.colnombreusuario.HeaderText = "Usuario";
		this.colnombreusuario.Name = "colnombreusuario";
		this.colnombreusuario.Width = 150;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(376, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(83, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Monto Apertura:";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(23, 25);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(51, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Sucursal:";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(18, 48);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(55, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "Direccion:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(29, 116);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(46, 13);
		this.label4.TabIndex = 4;
		this.label4.Text = "Usuario:";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(23, 71);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 5;
		this.label5.Tag = "";
		this.label5.Text = "Almacen:";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(390, 45);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(70, 13);
		this.label6.TabIndex = 6;
		this.label6.Text = "Monto Cierre:";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(618, 112);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(75, 13);
		this.label7.TabIndex = 7;
		this.label7.Text = "Suma Ingreso:";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(23, 94);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(52, 13);
		this.label8.TabIndex = 8;
		this.label8.Text = "Telefono:";
		this.groupBox1.Controls.Add(this.txtdeposito_egr);
		this.groupBox1.Controls.Add(this.txtefectivo_egr);
		this.groupBox1.Controls.Add(this.label14);
		this.groupBox1.Controls.Add(this.txttarjeta_egr);
		this.groupBox1.Controls.Add(this.txttrans_egr);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtdeposito_ing);
		this.groupBox1.Controls.Add(this.txtefectivo_ing);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txttarjeta_ing);
		this.groupBox1.Controls.Add(this.txttrans_ing);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.txtsumaingreso);
		this.groupBox1.Controls.Add(this.txtsumaegreso);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtMontoCierre);
		this.groupBox1.Controls.Add(this.txtMontoApertura);
		this.groupBox1.Controls.Add(this.txtUsuario);
		this.groupBox1.Controls.Add(this.txtTelefono);
		this.groupBox1.Controls.Add(this.txtAlmacen);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.txtSucursal);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1170, 150);
		this.groupBox1.TabIndex = 9;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Datos de Reporte";
		this.txtsumaingreso.Enabled = false;
		this.txtsumaingreso.Location = new System.Drawing.Point(696, 109);
		this.txtsumaingreso.Name = "txtsumaingreso";
		this.txtsumaingreso.Size = new System.Drawing.Size(108, 20);
		this.txtsumaingreso.TabIndex = 18;
		this.txtsumaegreso.Enabled = false;
		this.txtsumaegreso.Location = new System.Drawing.Point(918, 109);
		this.txtsumaegreso.Name = "txtsumaegreso";
		this.txtsumaegreso.Size = new System.Drawing.Size(108, 20);
		this.txtsumaegreso.TabIndex = 17;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(840, 112);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(73, 13);
		this.label9.TabIndex = 16;
		this.label9.Text = "Suma Egreso:";
		this.txtMontoCierre.Enabled = false;
		this.txtMontoCierre.Location = new System.Drawing.Point(465, 42);
		this.txtMontoCierre.Name = "txtMontoCierre";
		this.txtMontoCierre.Size = new System.Drawing.Size(108, 20);
		this.txtMontoCierre.TabIndex = 15;
		this.txtMontoApertura.Enabled = false;
		this.txtMontoApertura.Location = new System.Drawing.Point(465, 18);
		this.txtMontoApertura.Name = "txtMontoApertura";
		this.txtMontoApertura.Size = new System.Drawing.Size(108, 20);
		this.txtMontoApertura.TabIndex = 14;
		this.txtUsuario.Enabled = false;
		this.txtUsuario.Location = new System.Drawing.Point(81, 113);
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.Size = new System.Drawing.Size(269, 20);
		this.txtUsuario.TabIndex = 13;
		this.txtTelefono.Enabled = false;
		this.txtTelefono.Location = new System.Drawing.Point(81, 90);
		this.txtTelefono.Name = "txtTelefono";
		this.txtTelefono.Size = new System.Drawing.Size(269, 20);
		this.txtTelefono.TabIndex = 12;
		this.txtAlmacen.Enabled = false;
		this.txtAlmacen.Location = new System.Drawing.Point(81, 67);
		this.txtAlmacen.Name = "txtAlmacen";
		this.txtAlmacen.Size = new System.Drawing.Size(269, 20);
		this.txtAlmacen.TabIndex = 11;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(81, 45);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.Size = new System.Drawing.Size(269, 20);
		this.txtDireccion.TabIndex = 10;
		this.txtDireccion.TextChanged += new System.EventHandler(txtDireccion_TextChanged);
		this.txtSucursal.Enabled = false;
		this.txtSucursal.Location = new System.Drawing.Point(81, 22);
		this.txtSucursal.Name = "txtSucursal";
		this.txtSucursal.Size = new System.Drawing.Size(269, 20);
		this.txtSucursal.TabIndex = 9;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDataCierre);
		this.groupBox2.Location = new System.Drawing.Point(0, 156);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1170, 381);
		this.groupBox2.TabIndex = 10;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Listado de Movimientos";
		this.txtdeposito_ing.Enabled = false;
		this.txtdeposito_ing.Location = new System.Drawing.Point(696, 86);
		this.txtdeposito_ing.Name = "txtdeposito_ing";
		this.txtdeposito_ing.Size = new System.Drawing.Size(108, 20);
		this.txtdeposito_ing.TabIndex = 26;
		this.txtefectivo_ing.Enabled = false;
		this.txtefectivo_ing.Location = new System.Drawing.Point(696, 62);
		this.txtefectivo_ing.Name = "txtefectivo_ing";
		this.txtefectivo_ing.Size = new System.Drawing.Size(108, 20);
		this.txtefectivo_ing.TabIndex = 25;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(622, 66);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(62, 13);
		this.label10.TabIndex = 24;
		this.label10.Text = "EFECTIVO:";
		this.txttarjeta_ing.Enabled = false;
		this.txttarjeta_ing.Location = new System.Drawing.Point(696, 39);
		this.txttarjeta_ing.Name = "txttarjeta_ing";
		this.txttarjeta_ing.Size = new System.Drawing.Size(108, 20);
		this.txttarjeta_ing.TabIndex = 23;
		this.txttrans_ing.Enabled = false;
		this.txttrans_ing.Location = new System.Drawing.Point(696, 15);
		this.txttrans_ing.Name = "txttrans_ing";
		this.txttrans_ing.Size = new System.Drawing.Size(108, 20);
		this.txttrans_ing.TabIndex = 22;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(635, 20);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(47, 13);
		this.label11.TabIndex = 19;
		this.label11.Text = "TRANS:";
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(620, 90);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(65, 13);
		this.label12.TabIndex = 21;
		this.label12.Text = "DEPOSITO:";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(625, 43);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(58, 13);
		this.label13.TabIndex = 20;
		this.label13.Text = "TARJETA:";
		this.txtdeposito_egr.Enabled = false;
		this.txtdeposito_egr.Location = new System.Drawing.Point(918, 86);
		this.txtdeposito_egr.Name = "txtdeposito_egr";
		this.txtdeposito_egr.Size = new System.Drawing.Size(108, 20);
		this.txtdeposito_egr.TabIndex = 34;
		this.txtefectivo_egr.Enabled = false;
		this.txtefectivo_egr.Location = new System.Drawing.Point(918, 62);
		this.txtefectivo_egr.Name = "txtefectivo_egr";
		this.txtefectivo_egr.Size = new System.Drawing.Size(108, 20);
		this.txtefectivo_egr.TabIndex = 33;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(844, 66);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(62, 13);
		this.label14.TabIndex = 32;
		this.label14.Text = "EFECTIVO:";
		this.txttarjeta_egr.Enabled = false;
		this.txttarjeta_egr.Location = new System.Drawing.Point(918, 39);
		this.txttarjeta_egr.Name = "txttarjeta_egr";
		this.txttarjeta_egr.Size = new System.Drawing.Size(108, 20);
		this.txttarjeta_egr.TabIndex = 31;
		this.txttrans_egr.Enabled = false;
		this.txttrans_egr.Location = new System.Drawing.Point(918, 15);
		this.txttrans_egr.Name = "txttrans_egr";
		this.txttrans_egr.Size = new System.Drawing.Size(108, 20);
		this.txttrans_egr.TabIndex = 30;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(857, 20);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(47, 13);
		this.label15.TabIndex = 27;
		this.label15.Text = "TRANS:";
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(842, 90);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(65, 13);
		this.label16.TabIndex = 29;
		this.label16.Text = "DEPOSITO:";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(847, 43);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(58, 13);
		this.label17.TabIndex = 28;
		this.label17.Text = "TARJETA:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1170, 537);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmReporteCajaCierrePorUsuario";
		this.Text = "Reporte de Cierre de Caja por Usuario:";
		base.Load += new System.EventHandler(frmReporteCajaCierrePorUsuario_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvDataCierre).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
