using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListadoMovimientoSegunTiposStock : Form
{
	private clsAdmMovimientoStock admMovimientoStock = new clsAdmMovimientoStock();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private RadGridView rgvListado;

	private DateTimePicker dtpFecha2;

	private DateTimePicker dtpFecha1;

	private Label label8;

	private Label label1;

	private ComboBox cmbAlmacen;

	private Label label3;

	private Button btnActualizar;

	public frmListadoMovimientoSegunTiposStock()
	{
		InitializeComponent();
	}

	private void cargaAlmacenes()
	{
		DataTable data = admalma.ListaAlmacen2();
		if (data != null)
		{
			DataRow fila = data.NewRow();
			fila.ItemArray = new object[2] { 0, "TODOS" };
			data.Rows.InsertAt(fila, 0);
		}
		cmbAlmacen.DataSource = data;
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		rgvListado.DataSource = admMovimientoStock.listaMovimientoStock(Convert.ToInt32(cmbAlmacen.SelectedValue), dtpFecha1.Value, dtpFecha2.Value);
	}

	private void frmListadoMovimientoSegunTiposStock_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		rgvListado.DataSource = admMovimientoStock.listaMovimientoStock(Convert.ToInt32(cmbAlmacen.SelectedValue), dtpFecha1.Value, dtpFecha2.Value);
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
		this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
		this.label8 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvListado = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvListado).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListado.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dtpFecha2);
		this.groupBox1.Controls.Add(this.dtpFecha1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1364, 88);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Filtros";
		this.btnActualizar.BackColor = System.Drawing.Color.White;
		this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnActualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
		this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizar.Location = new System.Drawing.Point(476, 22);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(89, 38);
		this.btnActualizar.TabIndex = 59;
		this.btnActualizar.Text = "Actualizar";
		this.btnActualizar.UseVisualStyleBackColor = false;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.cmbAlmacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(12, 39);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(162, 21);
		this.cmbAlmacen.TabIndex = 58;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.SteelBlue;
		this.label3.Location = new System.Drawing.Point(9, 18);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(68, 16);
		this.label3.TabIndex = 57;
		this.label3.Text = "Almacen";
		this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha2.Location = new System.Drawing.Point(338, 40);
		this.dtpFecha2.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha2.Name = "dtpFecha2";
		this.dtpFecha2.Size = new System.Drawing.Size(131, 20);
		this.dtpFecha2.TabIndex = 55;
		this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha1.Location = new System.Drawing.Point(199, 39);
		this.dtpFecha1.Margin = new System.Windows.Forms.Padding(4);
		this.dtpFecha1.Name = "dtpFecha1";
		this.dtpFecha1.Size = new System.Drawing.Size(131, 20);
		this.dtpFecha1.TabIndex = 56;
		this.label8.AutoSize = true;
		this.label8.BackColor = System.Drawing.Color.Transparent;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.SteelBlue;
		this.label8.Location = new System.Drawing.Point(335, 22);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(49, 16);
		this.label8.TabIndex = 54;
		this.label8.Text = "Hasta";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.SteelBlue;
		this.label1.Location = new System.Drawing.Point(197, 20);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(54, 16);
		this.label1.TabIndex = 53;
		this.label1.Text = "Desde";
		this.groupBox2.Controls.Add(this.rgvListado);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 88);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1364, 362);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.rgvListado.AutoScroll = true;
		this.rgvListado.AutoSizeRows = true;
		this.rgvListado.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvListado.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvListado.Location = new System.Drawing.Point(3, 16);
		this.rgvListado.MasterTemplate.AllowAddNewRow = false;
		this.rgvListado.MasterTemplate.AllowColumnReorder = false;
		this.rgvListado.MasterTemplate.AllowDeleteRow = false;
		this.rgvListado.MasterTemplate.AllowDragToGroup = false;
		this.rgvListado.MasterTemplate.AllowEditRow = false;
		this.rgvListado.MasterTemplate.AllowRowResize = false;
		this.rgvListado.MasterTemplate.AutoGenerateColumns = false;
		this.rgvListado.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.Expression = "";
		gridViewTextBoxColumn1.FieldName = "descripAlmacen";
		gridViewTextBoxColumn1.HeaderText = "Almacen";
		gridViewTextBoxColumn1.Name = "colDescAlmacen";
		gridViewTextBoxColumn1.Width = 106;
		gridViewTextBoxColumn2.FieldName = "fechaRegistro";
		gridViewTextBoxColumn2.HeaderText = "Fecha Operacion";
		gridViewTextBoxColumn2.Name = "colFechaRegistro";
		gridViewTextBoxColumn2.Width = 123;
		gridViewTextBoxColumn2.WrapText = true;
		gridViewTextBoxColumn3.FieldName = "tipoDoc";
		gridViewTextBoxColumn3.HeaderText = "Tipo Doc.";
		gridViewTextBoxColumn3.Name = "colTipoDoc";
		gridViewTextBoxColumn3.Width = 73;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.FieldName = "nro_serie";
		gridViewTextBoxColumn4.HeaderText = "Nro. Serie";
		gridViewTextBoxColumn4.Name = "colNroSerie";
		gridViewTextBoxColumn4.Width = 77;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.FieldName = "nro_doc";
		gridViewTextBoxColumn5.HeaderText = "Nro. Doc.";
		gridViewTextBoxColumn5.Name = "colNroDoc";
		gridViewTextBoxColumn5.Width = 79;
		gridViewTextBoxColumn5.WrapText = true;
		gridViewTextBoxColumn6.FieldName = "fechaRegistroDoc";
		gridViewTextBoxColumn6.HeaderText = "Fecha Registro";
		gridViewTextBoxColumn6.Name = "colFechaRegistroDoc";
		gridViewTextBoxColumn6.Width = 116;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.FieldName = "codProducto";
		gridViewTextBoxColumn7.HeaderText = "codProducto";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "colCodProducto";
		gridViewTextBoxColumn7.WrapText = true;
		gridViewTextBoxColumn8.FieldName = "referencia";
		gridViewTextBoxColumn8.HeaderText = "Referencia";
		gridViewTextBoxColumn8.Name = "colRefProducto";
		gridViewTextBoxColumn8.Width = 74;
		gridViewTextBoxColumn8.WrapText = true;
		gridViewTextBoxColumn9.FieldName = "descripcion";
		gridViewTextBoxColumn9.HeaderText = "Descripcion";
		gridViewTextBoxColumn9.Name = "colDescripcion";
		gridViewTextBoxColumn9.Width = 317;
		gridViewTextBoxColumn9.WrapText = true;
		gridViewTextBoxColumn10.FieldName = "unidad";
		gridViewTextBoxColumn10.HeaderText = "Unidad";
		gridViewTextBoxColumn10.Name = "colUnidad";
		gridViewTextBoxColumn10.Width = 108;
		gridViewTextBoxColumn10.WrapText = true;
		gridViewTextBoxColumn11.FieldName = "tipo_stock";
		gridViewTextBoxColumn11.HeaderText = "Tipo Stock";
		gridViewTextBoxColumn11.Name = "colTipoStock";
		gridViewTextBoxColumn11.Width = 75;
		gridViewTextBoxColumn11.WrapText = true;
		gridViewTextBoxColumn12.FieldName = "entrada";
		gridViewTextBoxColumn12.HeaderText = "Entrada";
		gridViewTextBoxColumn12.Name = "colCtdadEntrada";
		gridViewTextBoxColumn12.Width = 75;
		gridViewTextBoxColumn12.WrapText = true;
		gridViewTextBoxColumn13.FieldName = "salida";
		gridViewTextBoxColumn13.HeaderText = "Salida";
		gridViewTextBoxColumn13.Name = "colCtdadSalida";
		gridViewTextBoxColumn13.Width = 75;
		gridViewTextBoxColumn13.WrapText = true;
		gridViewTextBoxColumn14.FieldName = "stockFinal";
		gridViewTextBoxColumn14.HeaderText = "Stock Final";
		gridViewTextBoxColumn14.Name = "colStockFinal";
		gridViewTextBoxColumn14.Width = 71;
		gridViewTextBoxColumn14.WrapText = true;
		this.rgvListado.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14);
		this.rgvListado.MasterTemplate.EnableFiltering = true;
		this.rgvListado.MasterTemplate.EnableGrouping = false;
		this.rgvListado.MasterTemplate.MultiSelect = true;
		this.rgvListado.MasterTemplate.ShowFilteringRow = false;
		this.rgvListado.MasterTemplate.ShowHeaderCellButtons = true;
		this.rgvListado.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvListado.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvListado.Name = "rgvListado";
		this.rgvListado.ShowGroupPanel = false;
		this.rgvListado.ShowHeaderCellButtons = true;
		this.rgvListado.Size = new System.Drawing.Size(1358, 343);
		this.rgvListado.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1364, 450);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmListadoMovimientoSegunTiposStock";
		this.Text = "Listado de Movimientos de Stock";
		base.Load += new System.EventHandler(frmListadoMovimientoSegunTiposStock_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvListado.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvListado).EndInit();
		base.ResumeLayout(false);
	}
}
