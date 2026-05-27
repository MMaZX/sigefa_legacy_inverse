using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;

namespace SIGEFA.Formularios;

public class frmMuestraNotasIngresoProveedor : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

	public int CodProveedor = 0;

	public int CodNotaI = 0;

	private DataTable dt1 = new DataTable();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnConsultar;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private GroupBox groupBox2;

	private DataGridView dgvDocumentos;

	private Button btnAceptar;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn codNota;

	private DataGridViewTextBoxColumn doc;

	private DataGridViewTextBoxColumn razonsocial;

	private DataGridViewTextBoxColumn almacen;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn comentario;

	public frmMuestraNotasIngresoProveedor()
	{
		InitializeComponent();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		Guardar();
		Close();
	}

	private void frmMuestraNotasIngresoProveedor_Load(object sender, EventArgs e)
	{
		dtpDesde.Value = dtpDesde.Value.AddDays(-90.0);
		CargaLista();
	}

	private void CargaLista()
	{
		dgvDocumentos.DataSource = data;
		data.DataSource = AdmNotaI.CargaNotaIngresoSD(CodProveedor, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDocumentos.ClearSelection();
	}

	private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count > 0 && e.RowIndex > -1)
		{
			CodNotaI = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[codNota.Name].Value.ToString());
		}
	}

	private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDocumentos.Rows.Count > 0 && e.RowIndex > -1)
		{
			CodNotaI = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells[codNota.Name].Value.ToString());
			Guardar();
			Close();
		}
	}

	public void Guardar()
	{
		dt1 = AdmNotaI.CargaDetalle(CodNotaI);
		frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
		form.dgvDetalle.Rows.Clear();
		foreach (DataRow dtRow in dt1.Rows)
		{
			decimal precioventa = Convert.ToDecimal(dtRow["importe"]);
			form.dgvDetalle.Rows.Add("", "", Convert.ToInt32(dtRow["codProducto"]), dtRow["referencia"].ToString(), dtRow["producto"].ToString(), Convert.ToInt32(dtRow["codUnidadMedida"]), dtRow["unidad"].ToString(), 0, Convert.ToInt32(dtRow["cantidad"]), "", Convert.ToDecimal(dtRow["preciounitario"]), Convert.ToDecimal(dtRow["subtotal"]), Convert.ToDecimal(dtRow["descuento1"]), "", "", Convert.ToDecimal(dtRow["montodscto"]), Convert.ToDecimal(dtRow["valorventa"]), Convert.ToDecimal(dtRow["igv"]), precioventa, Convert.ToDecimal(dtRow["precioreal"]), Convert.ToDecimal(dtRow["valoreal"]), 0);
		}
		form.CodNotaI = CodNotaI;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmMuestraNotasIngresoProveedor));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnConsultar = new System.Windows.Forms.Button();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDocumentos = new System.Windows.Forms.DataGridView();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.codNota = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.doc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.razonsocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.groupBox1.Controls.Add(this.btnConsultar);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(814, 64);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Buscar";
		this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
		this.btnConsultar.ImageIndex = 6;
		this.btnConsultar.Location = new System.Drawing.Point(367, 24);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(105, 27);
		this.btnConsultar.TabIndex = 14;
		this.btnConsultar.Text = " Consultar";
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(191, 31);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(18, 31);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(71, 28);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 4;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(238, 27);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 3;
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDocumentos);
		this.groupBox2.Location = new System.Drawing.Point(1, 66);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(813, 288);
		this.groupBox2.TabIndex = 2;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Documentos";
		this.dgvDocumentos.AllowUserToAddRows = false;
		this.dgvDocumentos.AllowUserToDeleteRows = false;
		this.dgvDocumentos.AllowUserToResizeColumns = false;
		this.dgvDocumentos.AllowUserToResizeRows = false;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDocumentos.Columns.AddRange(this.codNota, this.doc, this.razonsocial, this.almacen, this.fecha, this.comentario);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvDocumentos.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDocumentos.Location = new System.Drawing.Point(3, 16);
		this.dgvDocumentos.Name = "dgvDocumentos";
		this.dgvDocumentos.ReadOnly = true;
		this.dgvDocumentos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDocumentos.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgvDocumentos.RowHeadersVisible = false;
		this.dgvDocumentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvDocumentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDocumentos.Size = new System.Drawing.Size(807, 269);
		this.dgvDocumentos.TabIndex = 0;
		this.dgvDocumentos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellDoubleClick);
		this.dgvDocumentos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentos_CellClick);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(721, 360);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(90, 32);
		this.btnAceptar.TabIndex = 6;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.codNota.DataPropertyName = "codNota";
		this.codNota.HeaderText = "Codigo";
		this.codNota.Name = "codNota";
		this.codNota.ReadOnly = true;
		this.codNota.Width = 80;
		this.doc.DataPropertyName = "doc";
		this.doc.HeaderText = "Documento";
		this.doc.Name = "doc";
		this.doc.ReadOnly = true;
		this.doc.Width = 80;
		this.razonsocial.DataPropertyName = "razonsocial";
		this.razonsocial.HeaderText = "Proveedor";
		this.razonsocial.Name = "razonsocial";
		this.razonsocial.ReadOnly = true;
		this.razonsocial.Width = 180;
		this.almacen.DataPropertyName = "almacen";
		this.almacen.HeaderText = "Almacen";
		this.almacen.Name = "almacen";
		this.almacen.ReadOnly = true;
		this.almacen.Width = 150;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.comentario.DataPropertyName = "comentario";
		this.comentario.HeaderText = "Comentario";
		this.comentario.Name = "comentario";
		this.comentario.ReadOnly = true;
		this.comentario.Width = 200;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(207, 221, 238);
		base.ClientSize = new System.Drawing.Size(814, 394);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMuestraNotasIngresoProveedor";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notas Ingreso";
		base.Load += new System.EventHandler(frmMuestraNotasIngresoProveedor_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		base.ResumeLayout(false);
	}
}
