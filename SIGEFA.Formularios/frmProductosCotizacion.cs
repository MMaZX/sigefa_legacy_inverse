using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmProductosCotizacion : Form
{
	public static BindingSource data = new BindingSource();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	private string filtro = string.Empty;

	private IContainer components = null;

	private RibbonBar ribbonBar2;

	private ButtonItem btnnuevoproducto;

	private RadGridView dgvprods;

	private ButtonItem buttonItem1;

	private ButtonItem btnnuevo;

	public frmProductosCotizacion()
	{
		InitializeComponent();
	}

	private void frmProductosCotizacion_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void CargaLista()
	{
		dgvprods.DataSource = data;
		data.DataSource = AdmPro.CatalogoProductosCotizacion();
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvprods.ClearSelection();
	}

	private void btnnuevoproducto_Click(object sender, EventArgs e)
	{
		if (dgvprods.CurrentRow != null && dgvprods.CurrentRow.Index != -1)
		{
			pro.CodProducto = Convert.ToInt32(dgvprods.CurrentRow.Cells["codproducto"].Value);
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 2;
			frm.Cotizacion = 1;
			frm.pro = pro;
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				AdmPro.ActualizaEstadoProductoCotizacion(pro.CodProducto);
				MessageBox.Show("Producto Se registro Correctamente en el Catalogo Principal", "Convertir Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			CargaLista();
		}
	}

	private void btnnuevo_Click(object sender, EventArgs e)
	{
		try
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 1;
			frm.Coti = 1;
			frm.ShowDialog();
			CargaLista();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductosCotizacion));
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.btnnuevo = new DevComponents.DotNetBar.ButtonItem();
		this.btnnuevoproducto = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.dgvprods = new Telerik.WinControls.UI.RadGridView();
		((System.ComponentModel.ISupportInitialize)this.dgvprods).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvprods.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar2.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.DragDropSupport = true;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[3] { this.btnnuevo, this.btnnuevoproducto, this.buttonItem1 });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(986, 58);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Metro;
		this.ribbonBar2.TabIndex = 4;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar2.TitleVisible = false;
		this.btnnuevo.Image = (System.Drawing.Image)resources.GetObject("btnnuevo.Image");
		this.btnnuevo.ImageIndex = 4;
		this.btnnuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnnuevo.Name = "btnnuevo";
		this.btnnuevo.SubItemsExpandWidth = 14;
		this.btnnuevo.Text = "Nuevo";
		this.btnnuevo.Click += new System.EventHandler(btnnuevo_Click);
		this.btnnuevoproducto.Image = (System.Drawing.Image)resources.GetObject("btnnuevoproducto.Image");
		this.btnnuevoproducto.ImageIndex = 4;
		this.btnnuevoproducto.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnnuevoproducto.Name = "btnnuevoproducto";
		this.btnnuevoproducto.SubItemsExpandWidth = 14;
		this.btnnuevoproducto.Text = "Transformar";
		this.btnnuevoproducto.Click += new System.EventHandler(btnnuevoproducto_Click);
		this.buttonItem1.Image = (System.Drawing.Image)resources.GetObject("buttonItem1.Image");
		this.buttonItem1.ImageIndex = 4;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Actualizar";
		this.dgvprods.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvprods.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvprods.EnableCustomDrawing = true;
		this.dgvprods.Location = new System.Drawing.Point(0, 58);
		this.dgvprods.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
		this.dgvprods.MasterTemplate.AllowAddNewRow = false;
		this.dgvprods.MasterTemplate.AllowEditRow = false;
		this.dgvprods.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codcotizacion";
		gridViewTextBoxColumn1.HeaderText = "Cotizacion";
		gridViewTextBoxColumn1.Name = "codcotizacion";
		gridViewTextBoxColumn1.Width = 95;
		gridViewTextBoxColumn2.FieldName = "codproducto";
		gridViewTextBoxColumn2.HeaderText = "codproducto";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "codproducto";
		gridViewTextBoxColumn3.FieldName = "coduniversal";
		gridViewTextBoxColumn3.HeaderText = "coduniversal";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "coduniversal";
		gridViewTextBoxColumn4.FieldName = "referencia";
		gridViewTextBoxColumn4.HeaderText = "Referencia";
		gridViewTextBoxColumn4.Name = "referencia";
		gridViewTextBoxColumn4.Width = 147;
		gridViewTextBoxColumn5.FieldName = "descripcion";
		gridViewTextBoxColumn5.HeaderText = "Descripcion";
		gridViewTextBoxColumn5.Multiline = true;
		gridViewTextBoxColumn5.Name = "descripcion";
		gridViewTextBoxColumn5.Width = 195;
		gridViewTextBoxColumn5.WrapText = true;
		gridViewTextBoxColumn6.FieldName = "codmarca";
		gridViewTextBoxColumn6.HeaderText = "codmarca";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "codmarca";
		gridViewTextBoxColumn6.Width = 52;
		gridViewTextBoxColumn7.FieldName = "marcadesc";
		gridViewTextBoxColumn7.HeaderText = "Marca";
		gridViewTextBoxColumn7.Name = "marcadesc";
		gridViewTextBoxColumn7.Width = 80;
		gridViewTextBoxColumn8.FieldName = "unidad";
		gridViewTextBoxColumn8.HeaderText = "Unidad Base";
		gridViewTextBoxColumn8.Name = "unidad";
		gridViewTextBoxColumn8.Width = 109;
		gridViewTextBoxColumn9.FieldName = "control";
		gridViewTextBoxColumn9.HeaderText = "Control";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "control";
		gridViewTextBoxColumn10.FieldName = "comision";
		gridViewTextBoxColumn10.HeaderText = "Comision";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "comision";
		gridViewTextBoxColumn11.FieldName = "preciocatalogo";
		gridViewTextBoxColumn11.HeaderText = "Precio Catalogo";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "preciocatalogo";
		gridViewTextBoxColumn11.Width = 123;
		gridViewTextBoxColumn12.FieldName = "preciocompra";
		gridViewTextBoxColumn12.HeaderText = "Precio Compra";
		gridViewTextBoxColumn12.Name = "preciocompra";
		gridViewTextBoxColumn12.Width = 31;
		gridViewTextBoxColumn13.FieldName = "precioventa";
		gridViewTextBoxColumn13.HeaderText = "Precio Venta";
		gridViewTextBoxColumn13.Name = "precioventa";
		gridViewTextBoxColumn13.Width = 33;
		gridViewTextBoxColumn14.FieldName = "codfamilia";
		gridViewTextBoxColumn14.HeaderText = "codfamilia";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "codfamilia";
		gridViewTextBoxColumn14.Width = 40;
		gridViewTextBoxColumn15.FieldName = "familiadesc";
		gridViewTextBoxColumn15.HeaderText = "Familia";
		gridViewTextBoxColumn15.Name = "familiadesc";
		gridViewTextBoxColumn15.Width = 62;
		gridViewTextBoxColumn16.FieldName = "codlinea";
		gridViewTextBoxColumn16.HeaderText = "codlinea";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "codlinea";
		gridViewTextBoxColumn16.Width = 53;
		gridViewTextBoxColumn17.FieldName = "lineadesc";
		gridViewTextBoxColumn17.HeaderText = "Linea";
		gridViewTextBoxColumn17.Name = "lineadesc";
		gridViewTextBoxColumn17.Width = 80;
		gridViewTextBoxColumn18.FieldName = "codmodelo";
		gridViewTextBoxColumn18.HeaderText = "codmodelo";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "codmodelo";
		gridViewTextBoxColumn18.Width = 43;
		gridViewTextBoxColumn19.FieldName = "modelodesc";
		gridViewTextBoxColumn19.HeaderText = "Modelo";
		gridViewTextBoxColumn19.Name = "modelodesc";
		gridViewTextBoxColumn19.Width = 67;
		gridViewTextBoxColumn20.FieldName = "stockminimo";
		gridViewTextBoxColumn20.HeaderText = "Stock Minimo";
		gridViewTextBoxColumn20.IsVisible = false;
		gridViewTextBoxColumn20.Name = "stockminimo";
		gridViewTextBoxColumn20.Width = 134;
		gridViewTextBoxColumn21.FieldName = "stockmaximo";
		gridViewTextBoxColumn21.HeaderText = "Stock Maximo";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "stockmaximo";
		gridViewTextBoxColumn21.Width = 48;
		gridViewTextBoxColumn22.FieldName = "capmax";
		gridViewTextBoxColumn22.HeaderText = "Cap. Maxima";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "capmax";
		gridViewTextBoxColumn22.Width = 47;
		gridViewTextBoxColumn23.FieldName = "ubicacion";
		gridViewTextBoxColumn23.HeaderText = "Rzn. Social";
		gridViewTextBoxColumn23.Name = "ubicacion";
		gridViewTextBoxColumn23.Width = 80;
		gridViewTextBoxColumn24.FieldName = "estado";
		gridViewTextBoxColumn24.HeaderText = "codestado";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "estado";
		gridViewTextBoxColumn24.Width = 47;
		gridViewTextBoxColumn25.FieldName = "nombreestado";
		gridViewTextBoxColumn25.HeaderText = "Estado";
		gridViewTextBoxColumn25.Name = "nombreestado";
		gridViewTextBoxColumn25.Width = 7;
		this.dgvprods.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25);
		this.dgvprods.MasterTemplate.EnableFiltering = true;
		this.dgvprods.MasterTemplate.EnableGrouping = false;
		this.dgvprods.MasterTemplate.EnablePaging = true;
		this.dgvprods.MasterTemplate.PageSize = 50;
		this.dgvprods.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.dgvprods.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvprods.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvprods.Name = "dgvprods";
		this.dgvprods.ReadOnly = true;
		this.dgvprods.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 58, 240, 150);
		this.dgvprods.RootElement.FocusBorderColor = System.Drawing.Color.Yellow;
		this.dgvprods.Size = new System.Drawing.Size(986, 353);
		this.dgvprods.TabIndex = 23;
		this.dgvprods.ThemeName = "Material";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(986, 411);
		base.Controls.Add(this.dgvprods);
		base.Controls.Add(this.ribbonBar2);
		base.Name = "frmProductosCotizacion";
		this.Text = "frmProductosCotizacion";
		base.Load += new System.EventHandler(frmProductosCotizacion_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvprods.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvprods).EndInit();
		base.ResumeLayout(false);
	}
}
