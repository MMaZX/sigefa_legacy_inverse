using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmDetalleDespachos : Office2007Form
{
	public int Proceso = 0;

	private clsAdmTransferencia admtrans = new clsAdmTransferencia();

	private clsTransferencia trans = new clsTransferencia();

	public static BindingSource data = new BindingSource();

	public int CodTransDirecta;

	private IContainer components = null;

	private Label lbDocumento;

	private DataGridView dtgfecha;

	private TextBox txtfecha;

	private Label label1;

	private DataGridView dtgtransferencia;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private Button btnimprimir;

	private GroupBox groupBox3;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn fentrega;

	private Button btnsalir;

	private DataGridViewTextBoxColumn codprod;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn cantreq;

	private DataGridViewTextBoxColumn despachado;

	private DataGridViewTextBoxColumn pendiente;

	private TextBox txtvendedor;

	private Label label2;

	private TextBox txttrans;

	private TextBox txtdoc;

	private Label lbldoc;

	private Label label3;

	private Label label4;

	private TextBox txtdni;

	private Label label5;

	private TextBox txtcliente;

	public frmDetalleDespachos()
	{
		InitializeComponent();
	}

	public void cargar()
	{
		DataTable newData = new DataTable();
		dtgfecha.Rows.Clear();
		try
		{
			newData = admtrans.MuestraTranferenciasDesp(Convert.ToInt32(CodTransDirecta));
			foreach (DataRow row in newData.Rows)
			{
				dtgfecha.Rows.Add(row[0].ToString(), Convert.ToDateTime(row[1].ToString()).ToString("dd/MM/yyyy"));
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnimprimir_Click(object sender, EventArgs e)
	{
	}

	private void frmDetalleDespachos_Load(object sender, EventArgs e)
	{
		cargar();
	}

	private void cargardetalle(DateTime fechaE, int codigotra)
	{
		DataTable newData1 = new DataTable();
		dtgtransferencia.Rows.Clear();
		try
		{
			newData1 = admtrans.MuestraTranferenciaEntrega(Convert.ToDateTime(fechaE.ToString()), codigotra);
			foreach (DataRow row in newData1.Rows)
			{
				dtgtransferencia.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString());
				txtvendedor.Text = row[6].ToString();
				txtdoc.Text = row[7].ToString();
				lbldoc.Text = row[8].ToString();
				txttrans.Text = row[9].ToString();
				txtcliente.Text = row[10].ToString();
				txtdni.Text = row[11].ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		cerrarformulario();
	}

	private void cerrarformulario()
	{
		Close();
	}

	private void dtgfecha_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		txtfecha.Text = Convert.ToString(dtgfecha.CurrentRow.Cells[fentrega.Name].Value);
		DateTime fecha = Convert.ToDateTime(dtgfecha.CurrentRow.Cells[fentrega.Name].Value);
		int codigotra = Convert.ToInt32(dtgfecha.CurrentRow.Cells[codigo.Name].Value);
		cargardetalle(fecha, codigotra);
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
		this.lbDocumento = new System.Windows.Forms.Label();
		this.dtgfecha = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fentrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtfecha = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dtgtransferencia = new System.Windows.Forms.DataGridView();
		this.codprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.despachado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.lbldoc = new System.Windows.Forms.Label();
		this.txttrans = new System.Windows.Forms.TextBox();
		this.txtdoc = new System.Windows.Forms.TextBox();
		this.txtvendedor = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.btnsalir = new System.Windows.Forms.Button();
		this.btnimprimir = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtcliente = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtdni = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.dtgfecha).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtgtransferencia).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.lbDocumento.AutoSize = true;
		this.lbDocumento.Font = new System.Drawing.Font("Segoe UI", 10.25f, System.Drawing.FontStyle.Bold);
		this.lbDocumento.Location = new System.Drawing.Point(24, 56);
		this.lbDocumento.Name = "lbDocumento";
		this.lbDocumento.Size = new System.Drawing.Size(164, 19);
		this.lbDocumento.TabIndex = 49;
		this.lbDocumento.Tag = "22";
		this.lbDocumento.Text = "FECHA DE DESPACHOS:";
		this.lbDocumento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.dtgfecha.AllowUserToAddRows = false;
		this.dtgfecha.AllowUserToDeleteRows = false;
		this.dtgfecha.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dtgfecha.Columns.AddRange(this.codigo, this.fentrega);
		this.dtgfecha.Location = new System.Drawing.Point(50, 91);
		this.dtgfecha.Name = "dtgfecha";
		this.dtgfecha.ReadOnly = true;
		this.dtgfecha.RowHeadersVisible = false;
		this.dtgfecha.Size = new System.Drawing.Size(95, 216);
		this.dtgfecha.TabIndex = 50;
		this.dtgfecha.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dtgfecha_CellClick);
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.fentrega.HeaderText = "Fecha D.";
		this.fentrega.Name = "fentrega";
		this.fentrega.ReadOnly = true;
		this.fentrega.Width = 90;
		this.txtfecha.Enabled = false;
		this.txtfecha.Location = new System.Drawing.Point(536, 25);
		this.txtfecha.Name = "txtfecha";
		this.txtfecha.Size = new System.Drawing.Size(111, 20);
		this.txtfecha.TabIndex = 51;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(452, 28);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(80, 13);
		this.label1.TabIndex = 52;
		this.label1.Text = "Fecha Entrega:";
		this.dtgtransferencia.AllowUserToAddRows = false;
		this.dtgtransferencia.AllowUserToDeleteRows = false;
		this.dtgtransferencia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dtgtransferencia.Columns.AddRange(this.codprod, this.referencia, this.descripcion, this.cantreq, this.despachado, this.pendiente);
		this.dtgtransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dtgtransferencia.Location = new System.Drawing.Point(3, 16);
		this.dtgtransferencia.Name = "dtgtransferencia";
		this.dtgtransferencia.ReadOnly = true;
		this.dtgtransferencia.RowHeadersVisible = false;
		this.dtgtransferencia.Size = new System.Drawing.Size(639, 182);
		this.dtgtransferencia.TabIndex = 53;
		this.codprod.HeaderText = "codprod";
		this.codprod.Name = "codprod";
		this.codprod.ReadOnly = true;
		this.codprod.Visible = false;
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 90;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 300;
		this.cantreq.HeaderText = "Cant. Req";
		this.cantreq.Name = "cantreq";
		this.cantreq.ReadOnly = true;
		this.cantreq.Width = 80;
		this.despachado.HeaderText = "Cant. Despachado";
		this.despachado.Name = "despachado";
		this.despachado.ReadOnly = true;
		this.despachado.Width = 80;
		this.pendiente.HeaderText = "Cant. Pendiente";
		this.pendiente.Name = "pendiente";
		this.pendiente.ReadOnly = true;
		this.pendiente.Width = 80;
		this.groupBox1.Controls.Add(this.txtdni);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtcliente);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.lbldoc);
		this.groupBox1.Controls.Add(this.txttrans);
		this.groupBox1.Controls.Add(this.txtdoc);
		this.groupBox1.Controls.Add(this.txtvendedor);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.btnsalir);
		this.groupBox1.Controls.Add(this.btnimprimir);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Controls.Add(this.txtfecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(230, 9);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(657, 435);
		this.groupBox1.TabIndex = 54;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "DETALLE ENTREGA";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Segoe UI", 10.25f, System.Drawing.FontStyle.Bold);
		this.label3.ForeColor = System.Drawing.Color.Red;
		this.label3.Location = new System.Drawing.Point(15, 58);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(143, 19);
		this.label3.TabIndex = 62;
		this.label3.Tag = "22";
		this.label3.Text = "N° TRANSFERENCIA:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lbldoc.AutoSize = true;
		this.lbldoc.Font = new System.Drawing.Font("Segoe UI", 10.25f, System.Drawing.FontStyle.Bold);
		this.lbldoc.ForeColor = System.Drawing.Color.Red;
		this.lbldoc.Location = new System.Drawing.Point(15, 26);
		this.lbldoc.Name = "lbldoc";
		this.lbldoc.Size = new System.Drawing.Size(98, 19);
		this.lbldoc.TabIndex = 61;
		this.lbldoc.Tag = "22";
		this.lbldoc.Text = "DOCUMENTO";
		this.lbldoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txttrans.Enabled = false;
		this.txttrans.Location = new System.Drawing.Point(166, 58);
		this.txttrans.Name = "txttrans";
		this.txttrans.Size = new System.Drawing.Size(156, 20);
		this.txttrans.TabIndex = 60;
		this.txtdoc.Enabled = false;
		this.txtdoc.Location = new System.Drawing.Point(243, 26);
		this.txtdoc.Name = "txtdoc";
		this.txtdoc.Size = new System.Drawing.Size(156, 20);
		this.txtdoc.TabIndex = 58;
		this.txtvendedor.Enabled = false;
		this.txtvendedor.Location = new System.Drawing.Point(111, 357);
		this.txtvendedor.Name = "txtvendedor";
		this.txtvendedor.Size = new System.Drawing.Size(320, 20);
		this.txtvendedor.TabIndex = 56;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(16, 360);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(90, 13);
		this.label2.TabIndex = 57;
		this.label2.Text = "Despachado Por:";
		this.btnsalir.Location = new System.Drawing.Point(552, 390);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(76, 34);
		this.btnsalir.TabIndex = 55;
		this.btnsalir.Text = "Salir";
		this.btnsalir.UseVisualStyleBackColor = true;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.btnimprimir.Location = new System.Drawing.Point(447, 390);
		this.btnimprimir.Name = "btnimprimir";
		this.btnimprimir.Size = new System.Drawing.Size(85, 34);
		this.btnimprimir.TabIndex = 54;
		this.btnimprimir.Text = "Imprimir";
		this.btnimprimir.UseVisualStyleBackColor = true;
		this.btnimprimir.Click += new System.EventHandler(btnimprimir_Click);
		this.groupBox2.Controls.Add(this.dtgtransferencia);
		this.groupBox2.Location = new System.Drawing.Point(6, 106);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(645, 201);
		this.groupBox2.TabIndex = 53;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "PRODUCTOS REQUERIMIENTOS DE TRANSFERENCIA";
		this.groupBox3.Controls.Add(this.dtgfecha);
		this.groupBox3.Controls.Add(this.lbDocumento);
		this.groupBox3.Location = new System.Drawing.Point(13, 9);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(211, 435);
		this.groupBox3.TabIndex = 55;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "LISTA DE FECHA DE ENTREGA";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(16, 321);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(42, 13);
		this.label4.TabIndex = 63;
		this.label4.Text = "Cliente:";
		this.txtcliente.Enabled = false;
		this.txtcliente.Location = new System.Drawing.Point(64, 318);
		this.txtcliente.Name = "txtcliente";
		this.txtcliente.Size = new System.Drawing.Size(367, 20);
		this.txtcliente.TabIndex = 64;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(452, 321);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(57, 13);
		this.label5.TabIndex = 65;
		this.label5.Text = "DNI/RUC:";
		this.txtdni.Enabled = false;
		this.txtdni.Location = new System.Drawing.Point(515, 318);
		this.txtdni.Name = "txtdni";
		this.txtdni.Size = new System.Drawing.Size(132, 20);
		this.txtdni.TabIndex = 66;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(899, 456);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmDetalleDespachos";
		this.Text = "frmDetalleDespachos";
		base.Load += new System.EventHandler(frmDetalleDespachos_Load);
		((System.ComponentModel.ISupportInitialize)this.dtgfecha).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtgtransferencia).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		base.ResumeLayout(false);
	}
}
