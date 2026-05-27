using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmListadoDocumentoRelacionadoGuiaRemisionCompra : Form
{
	private clsGuiaRemision grc = null;

	private clsAdmGuiaRemisionCompra admgrc = new clsAdmGuiaRemisionCompra();

	private BindingSource enlace = new BindingSource();

	private DataTable data = new DataTable();

	public int codGRC = 0;

	private IContainer components = null;

	private DataGridView dgvDocumentosRelacionados;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox txtGRC;

	private DataGridViewTextBoxColumn colCod_GRC_DR;

	private DataGridViewTextBoxColumn colCod_GRC;

	private DataGridViewTextBoxColumn colCod_Doc_Relacionado;

	private DataGridViewTextBoxColumn colDocumento;

	private DataGridViewTextBoxColumn colBandFacturaGeneralProductos;

	private DataGridViewTextBoxColumn colCodTipoDocumento;

	public frmListadoDocumentoRelacionadoGuiaRemisionCompra()
	{
		InitializeComponent();
	}

	private void dgvDocumentosRelacionados_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void frmListadoDocumentoRelacionadoGuiaRemisionCompra_Load(object sender, EventArgs e)
	{
		try
		{
			if (codGRC != 0)
			{
				grc = admgrc.CargaGuiaRemision(codGRC);
				txtGRC.Text = grc.NumDoc;
				data = admgrc.MuestraDocumentosRelacionados(codGRC);
				enlace.DataSource = data;
				dgvDocumentosRelacionados.DataSource = enlace;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvDocumentosRelacionados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		bool flag = false;
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
		this.dgvDocumentosRelacionados = new System.Windows.Forms.DataGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtGRC = new System.Windows.Forms.TextBox();
		this.colCod_GRC_DR = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCod_GRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCod_Doc_Relacionado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colBandFacturaGeneralProductos = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodTipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentosRelacionados).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.dgvDocumentosRelacionados.AllowUserToAddRows = false;
		this.dgvDocumentosRelacionados.AllowUserToDeleteRows = false;
		this.dgvDocumentosRelacionados.AllowUserToResizeColumns = false;
		this.dgvDocumentosRelacionados.AllowUserToResizeRows = false;
		this.dgvDocumentosRelacionados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDocumentosRelacionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDocumentosRelacionados.Columns.AddRange(this.colCod_GRC_DR, this.colCod_GRC, this.colCod_Doc_Relacionado, this.colDocumento, this.colBandFacturaGeneralProductos, this.colCodTipoDocumento);
		this.dgvDocumentosRelacionados.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDocumentosRelacionados.Location = new System.Drawing.Point(3, 16);
		this.dgvDocumentosRelacionados.Name = "dgvDocumentosRelacionados";
		this.dgvDocumentosRelacionados.RowHeadersVisible = false;
		this.dgvDocumentosRelacionados.Size = new System.Drawing.Size(416, 306);
		this.dgvDocumentosRelacionados.TabIndex = 0;
		this.dgvDocumentosRelacionados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentosRelacionados_CellContentClick);
		this.dgvDocumentosRelacionados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDocumentosRelacionados_CellDoubleClick);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.dgvDocumentosRelacionados);
		this.groupBox1.Location = new System.Drawing.Point(12, 38);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(422, 325);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Listado Documentos Relacionados";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(14, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(132, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Guia de Remision Compra:";
		this.txtGRC.Location = new System.Drawing.Point(152, 12);
		this.txtGRC.Name = "txtGRC";
		this.txtGRC.Size = new System.Drawing.Size(145, 20);
		this.txtGRC.TabIndex = 3;
		this.colCod_GRC_DR.DataPropertyName = "colCod_GRC_DR";
		this.colCod_GRC_DR.HeaderText = "colCod_GRC_DR";
		this.colCod_GRC_DR.Name = "colCod_GRC_DR";
		this.colCod_GRC_DR.Visible = false;
		this.colCod_GRC.DataPropertyName = "colCod_GRC";
		this.colCod_GRC.HeaderText = "colCod_GRC";
		this.colCod_GRC.Name = "colCod_GRC";
		this.colCod_GRC.Visible = false;
		this.colCod_Doc_Relacionado.DataPropertyName = "colCod_Doc_Relacionado";
		this.colCod_Doc_Relacionado.HeaderText = "colCod_Doc_Relacionado";
		this.colCod_Doc_Relacionado.Name = "colCod_Doc_Relacionado";
		this.colCod_Doc_Relacionado.Visible = false;
		this.colDocumento.DataPropertyName = "descripcionDocumento";
		this.colDocumento.HeaderText = "Documento";
		this.colDocumento.Name = "colDocumento";
		this.colDocumento.ReadOnly = true;
		this.colBandFacturaGeneralProductos.DataPropertyName = "facturaCompra";
		this.colBandFacturaGeneralProductos.HeaderText = "colBandFacturaGeneralProductos";
		this.colBandFacturaGeneralProductos.Name = "colBandFacturaGeneralProductos";
		this.colBandFacturaGeneralProductos.Visible = false;
		this.colCodTipoDocumento.DataPropertyName = "codTipoDocumento";
		this.colCodTipoDocumento.HeaderText = "colCodTipoDocumento";
		this.colCodTipoDocumento.Name = "colCodTipoDocumento";
		this.colCodTipoDocumento.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(446, 372);
		base.Controls.Add(this.txtGRC);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.groupBox1);
		this.MaximumSize = new System.Drawing.Size(462, 600);
		this.MinimumSize = new System.Drawing.Size(462, 411);
		base.Name = "frmListadoDocumentoRelacionadoGuiaRemisionCompra";
		this.Text = "Documento Relacionado de Guia Remision Compra";
		base.Load += new System.EventHandler(frmListadoDocumentoRelacionadoGuiaRemisionCompra_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentosRelacionados).EndInit();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
