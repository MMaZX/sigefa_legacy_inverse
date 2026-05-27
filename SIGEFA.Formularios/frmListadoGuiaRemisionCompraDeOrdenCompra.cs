using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoGuiaRemisionCompraDeOrdenCompra : Form
{
	private clsGuiaRemision grc = null;

	private clsAdmGuiaRemisionCompra admgrc = new clsAdmGuiaRemisionCompra();

	private clsOrdenCompra ord = null;

	private clsAdmOrdenCompra admoc = new clsAdmOrdenCompra();

	private BindingSource enlace = new BindingSource();

	private DataTable data = new DataTable();

	public int codOC = 0;

	internal frmNotaIngreso ventana = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private TextBox txtOC;

	private RadGridView rgvListadoGRCdeOC;

	public frmListadoGuiaRemisionCompraDeOrdenCompra()
	{
		InitializeComponent();
	}

	private void frmListadoDocumentoRelacionadoGuiaRemisionCompra_Load(object sender, EventArgs e)
	{
		try
		{
			if (codOC != 0)
			{
				ord = admoc.CargaOrdenCompra(codOC);
				txtOC.Text = "OC-" + ord.CodOrdenCompra.ToString().PadLeft(8, '0');
				data = admgrc.listarGRCdeOC(ord.CodOrdenCompra);
				enlace.DataSource = data;
				rgvListadoGRCdeOC.DataSource = enlace;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvListadoGRCdeOC_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codgrcseleccionada = Convert.ToInt32(e.Row.Cells["colCodGRC"].Value);
			if (ventana != null)
			{
				ventana.grc_anadir = admgrc.CargaGuiaRemision(codgrcseleccionada);
				base.DialogResult = DialogResult.Yes;
				Close();
			}
			else
			{
				base.DialogResult = DialogResult.Cancel;
			}
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvListadoGRCdeOC = new Telerik.WinControls.UI.RadGridView();
		this.label1 = new System.Windows.Forms.Label();
		this.txtOC = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGRCdeOC).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGRCdeOC.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvListadoGRCdeOC);
		this.groupBox1.Location = new System.Drawing.Point(12, 38);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(422, 325);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Listado de Guia de Remision de Compra";
		this.rgvListadoGRCdeOC.AutoScroll = true;
		this.rgvListadoGRCdeOC.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListadoGRCdeOC.EnableGestures = false;
		this.rgvListadoGRCdeOC.Location = new System.Drawing.Point(3, 16);
		this.rgvListadoGRCdeOC.MasterTemplate.AllowAddNewRow = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowColumnChooser = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowColumnReorder = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowColumnResize = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowDeleteRow = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowDragToGroup = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowEditRow = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AllowRowResize = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AutoGenerateColumns = false;
		this.rgvListadoGRCdeOC.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.AllowFiltering = false;
		gridViewTextBoxColumn1.FieldName = "nroItem";
		gridViewTextBoxColumn1.HeaderText = "Item";
		gridViewTextBoxColumn1.Name = "colItem";
		gridViewTextBoxColumn1.Width = 140;
		gridViewTextBoxColumn2.FieldName = "codGRC";
		gridViewTextBoxColumn2.HeaderText = "codGRC";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodGRC";
		gridViewTextBoxColumn3.FieldName = "docGRC";
		gridViewTextBoxColumn3.HeaderText = "Guia de Remision de Compra";
		gridViewTextBoxColumn3.Name = "colDocGRC";
		gridViewTextBoxColumn3.Width = 276;
		this.rgvListadoGRCdeOC.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3);
		this.rgvListadoGRCdeOC.MasterTemplate.EnableFiltering = true;
		this.rgvListadoGRCdeOC.MasterTemplate.EnableGrouping = false;
		this.rgvListadoGRCdeOC.MasterTemplate.EnableSorting = false;
		this.rgvListadoGRCdeOC.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListadoGRCdeOC.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvListadoGRCdeOC.Name = "rgvListadoGRCdeOC";
		this.rgvListadoGRCdeOC.Size = new System.Drawing.Size(416, 306);
		this.rgvListadoGRCdeOC.TabIndex = 1;
		this.rgvListadoGRCdeOC.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvListadoGRCdeOC_CellDoubleClick);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(14, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(93, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Orden de Compra:";
		this.txtOC.Location = new System.Drawing.Point(113, 12);
		this.txtOC.Name = "txtOC";
		this.txtOC.Size = new System.Drawing.Size(145, 20);
		this.txtOC.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(446, 372);
		base.Controls.Add(this.txtOC);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.groupBox1);
		this.MaximumSize = new System.Drawing.Size(462, 600);
		this.MinimumSize = new System.Drawing.Size(462, 411);
		base.Name = "frmListadoGuiaRemisionCompraDeOrdenCompra";
		this.Text = "Guias de Remision de Orden de Compra";
		base.Load += new System.EventHandler(frmListadoDocumentoRelacionadoGuiaRemisionCompra_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGRCdeOC.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListadoGRCdeOC).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
