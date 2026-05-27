using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmFacturasManuales : Office2007Form
{
	public List<int> num_correlativo = new List<int>();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public DataTable tabla = new DataTable();

	private int i = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	public DataGridView dgvNumSerie;

	private DataGridViewTextBoxColumn numSerie;

	private ImageList imageList1;

	public Button btnAceptar;

	private ImageList imageList2;

	private Button btnSalir;

	public frmFacturasManuales()
	{
		InitializeComponent();
	}

	private void llena_grilla()
	{
		DataColumn column = new DataColumn();
		column.DataType = Type.GetType("System.Int32");
		column.ColumnName = "NumeroSerie";
		tabla.Columns.Add(column);
		foreach (int c in num_correlativo)
		{
			if (i < num_correlativo.Count)
			{
				DataRow row = tabla.NewRow();
				row["NumeroSerie"] = c;
				tabla.Rows.Add(row);
				i++;
			}
		}
		dgvNumSerie.DataSource = tabla;
	}

	private void frmFacturasManuales_Load(object sender, EventArgs e)
	{
		llena_grilla();
	}

	private void dgvNumSerie_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvNumSerie_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		frmVenta frm = (frmVenta)Application.OpenForms["frmVenta"];
		frm.Proceso = 1;
		frm.Procede = 4;
		frm.numSerie = tabla.Rows[e.RowIndex][0].ToString();
		frm.txtNumero.Text = frm.numSerie;
		frm.dtpFecha.MinDate = frm.fecha1;
		frm.dtpFecha.MaxDate = frm.fecha2;
		frm.Show();
		Close();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		frmVenta frm = (frmVenta)Application.OpenForms["frmVenta"];
		frm.Proceso = 1;
		frm.Procede = 4;
		frm.numSerie = tabla.Rows[0]["NumeroSerie"].ToString();
		frm.txtNumero.Text = frm.numSerie;
		frm.dtpFecha.MinDate = frm.fecha1;
		frm.dtpFecha.MaxDate = frm.fecha2;
		frm.Show();
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmFacturasManuales));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvNumSerie = new System.Windows.Forms.DataGridView();
		this.numSerie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnAceptar = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvNumSerie).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.dgvNumSerie);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(140, 237);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Correlativos que Faltan";
		this.dgvNumSerie.AllowUserToAddRows = false;
		this.dgvNumSerie.AllowUserToDeleteRows = false;
		this.dgvNumSerie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvNumSerie.Columns.AddRange(this.numSerie);
		this.dgvNumSerie.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvNumSerie.Location = new System.Drawing.Point(3, 16);
		this.dgvNumSerie.MultiSelect = false;
		this.dgvNumSerie.Name = "dgvNumSerie";
		this.dgvNumSerie.ReadOnly = true;
		this.dgvNumSerie.RowHeadersVisible = false;
		this.dgvNumSerie.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvNumSerie.Size = new System.Drawing.Size(134, 218);
		this.dgvNumSerie.TabIndex = 0;
		this.dgvNumSerie.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNumSerie_CellDoubleClick);
		this.dgvNumSerie.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvNumSerie_CellContentDoubleClick);
		this.numSerie.HeaderText = "NumeroSerie";
		this.numSerie.Name = "numSerie";
		this.numSerie.ReadOnly = true;
		this.numSerie.Visible = false;
		this.numSerie.Width = 200;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.ImageIndex = 1;
		this.btnAceptar.ImageList = this.imageList2;
		this.btnAceptar.Location = new System.Drawing.Point(6, 255);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 18;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "cross.png");
		this.imageList2.Images.SetKeyName(1, "tick.png");
		this.imageList2.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(89, 255);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 19;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(168, 298);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmFacturasManuales";
		this.Text = "frmFacturasManuales";
		base.Load += new System.EventHandler(frmFacturasManuales_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvNumSerie).EndInit();
		base.ResumeLayout(false);
	}
}
