using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmCombosVenta : Form
{
	private clsAdmProducto AdmPro = new clsAdmProducto();

	public int codcombo;

	private decimal stockcombo;

	private decimal stockdisponiblecombo;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private frmVenta2019 frmventa = new frmVenta2019();

	private IContainer components = null;

	private RadGridView rgvcombos;

	private GroupBox groupBox1;

	private Label label1;

	private Button btnEditar;

	private Button btnSalir;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	public frmCombosVenta()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmCombosVenta_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		try
		{
			rgvcombos.DataSource = data;
			data.DataSource = AdmPro.CatalogoCombosProductos(estado: true, 2);
			data.Filter = string.Empty;
			filtro = string.Empty;
			rgvcombos.ClearSelection();
			dgvdetalleEditable(editable: true);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvdetalleEditable(bool editable)
	{
		foreach (GridViewDataColumn col in rgvcombos.Columns)
		{
			col.ReadOnly = true;
			if (editable)
			{
				if (col.Name == "detalle")
				{
					col.ReadOnly = false;
				}
				else
				{
					col.ReadOnly = true;
				}
			}
		}
	}

	private void rgvcombos_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (rgvcombos.Rows.Count <= 0)
			{
				return;
			}
			stockcombo = Convert.ToInt32(e.Row.Cells["stock"].Value);
			stockdisponiblecombo = Convert.ToInt32(e.Row.Cells["stockcombodisponible"].Value);
			if (rgvcombos.Columns[e.ColumnIndex].Name == "detalle")
			{
				if (rgvcombos.CurrentRow != null && rgvcombos.CurrentRow.Index != -1)
				{
					codcombo = Convert.ToInt32(e.Row.Cells["codcombo"].Value);
					frmCombosProductos frm = new frmCombosProductos();
					frm.Proceso = 3;
					frm.codcombo = codcombo;
					frm.ShowDialog();
					CargaLista();
				}
			}
			else
			{
				codcombo = Convert.ToInt32(e.Row.Cells["codcombo"].Value);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (stockcombo > 0m)
		{
			if (stockdisponiblecombo > 0m)
			{
				frmventa.codcombo = codcombo;
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				MessageBox.Show("No se puede Agregar combo,se termino el stock comfigurado para venta.", "COMBOS VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			MessageBox.Show("No se puede Agregar combo,verficar si todos sus productos tienen stock.", "COMBOS VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void rgvcombos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (stockcombo > 0m)
		{
			if (stockdisponiblecombo > 0m)
			{
				frmventa.codcombo = codcombo;
				base.DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				MessageBox.Show("No se puede Agregar combo,se termino el stock comfigurado para venta.", "COMBOS VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else
		{
			MessageBox.Show("No se puede Agregar combo,verficar si todos sus productos tienen stock.", "COMBOS VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCombosVenta));
		this.rgvcombos = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		((System.ComponentModel.ISupportInitialize)this.rgvcombos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvcombos.MasterTemplate).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.rgvcombos.BackColor = System.Drawing.SystemColors.Control;
		this.rgvcombos.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvcombos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvcombos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.rgvcombos.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvcombos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvcombos.Location = new System.Drawing.Point(3, 16);
		this.rgvcombos.MasterTemplate.AllowAddNewRow = false;
		this.rgvcombos.MasterTemplate.AllowColumnReorder = false;
		gridViewTextBoxColumn1.FieldName = "numero_item";
		gridViewTextBoxColumn1.HeaderText = "#";
		gridViewTextBoxColumn1.Name = "numero_item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 80;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "codcombo";
		gridViewTextBoxColumn2.HeaderText = "codcombo";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "codcombo";
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "nombrecombo";
		gridViewTextBoxColumn3.HeaderText = "Descripción";
		gridViewTextBoxColumn3.Name = "nombrecombo";
		gridViewTextBoxColumn3.Width = 200;
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.FieldName = "stock";
		gridViewTextBoxColumn4.HeaderText = "Stock Productos";
		gridViewTextBoxColumn4.Name = "stock";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 150;
		gridViewTextBoxColumn5.FieldName = "stockcombodisponible";
		gridViewTextBoxColumn5.HeaderText = "Stock Configurado";
		gridViewTextBoxColumn5.Name = "stockcombodisponible";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 150;
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "total";
		gridViewTextBoxColumn6.HeaderText = "Total";
		gridViewTextBoxColumn6.Name = "total";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 100;
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.FieldName = "fechavencimiento";
		gridViewTextBoxColumn7.HeaderText = "Fecha Vencimiento";
		gridViewTextBoxColumn7.Name = "fechavencimiento";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 150;
		gridViewCheckBoxColumn1.FieldName = "detalle";
		gridViewCheckBoxColumn1.HeaderText = "Ver Detalle";
		gridViewCheckBoxColumn1.Name = "detalle";
		gridViewCheckBoxColumn1.Width = 120;
		gridViewTextBoxColumn8.FieldName = "FechaRegistro";
		gridViewTextBoxColumn8.HeaderText = "FechaRegistro";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "FechaRegistro";
		gridViewTextBoxColumn9.FieldName = "usuario";
		gridViewTextBoxColumn9.HeaderText = "usuario";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "usuario";
		gridViewTextBoxColumn10.FieldName = "nombreestado";
		gridViewTextBoxColumn10.HeaderText = "nombreestado";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "nombreestado";
		this.rgvcombos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewCheckBoxColumn1, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10);
		this.rgvcombos.MasterTemplate.EnableFiltering = true;
		this.rgvcombos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvcombos.Name = "rgvcombos";
		this.rgvcombos.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvcombos.ShowGroupPanel = false;
		this.rgvcombos.Size = new System.Drawing.Size(899, 281);
		this.rgvcombos.TabIndex = 0;
		this.rgvcombos.ThemeName = "TelerikMetroTouch";
		this.rgvcombos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvcombos_CellClick);
		this.rgvcombos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvcombos_CellDoubleClick);
		this.groupBox1.Controls.Add(this.rgvcombos);
		this.groupBox1.Location = new System.Drawing.Point(2, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(905, 300);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Combos";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(2, 312);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(339, 13);
		this.label1.TabIndex = 2;
		this.label1.Text = "Los Combos Registrados que no tienen stock, no se realizará la venta.";
		this.btnEditar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnEditar.Image = (System.Drawing.Image)resources.GetObject("btnEditar.Image");
		this.btnEditar.Location = new System.Drawing.Point(696, 309);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(103, 37);
		this.btnEditar.TabIndex = 77;
		this.btnEditar.Text = "Aceptar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(804, 309);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(103, 37);
		this.btnSalir.TabIndex = 76;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(910, 350);
		base.Controls.Add(this.btnEditar);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmCombosVenta";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmCombosVenta";
		base.Load += new System.EventHandler(frmCombosVenta_Load);
		((System.ComponentModel.ISupportInitialize)this.rgvcombos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvcombos).EndInit();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
