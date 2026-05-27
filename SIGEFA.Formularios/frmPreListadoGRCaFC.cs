using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmPreListadoGRCaFC : Form
{
	public DataTable data = null;

	public frmNotaIngreso ventana = null;

	private clsProducto pro = new clsProducto();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmProducto admPro = new clsAdmProducto();

	private IContainer components = null;

	private GroupBox groupBox2;

	private GroupBox groupBox1;

	private Button btnAgregarItem;

	private RadGridView rgvDetalle;

	public frmPreListadoGRCaFC()
	{
		InitializeComponent();
	}

	private void frmPreListadoGRCaFC_Load(object sender, EventArgs e)
	{
		rgvDetalle.DataSource = data;
	}

	private void btnAgregarItem_Click(object sender, EventArgs e)
	{
		try
		{
			foreach (GridViewRowInfo fila in rgvDetalle.SelectedRows)
			{
				pro.CodProducto = Convert.ToInt32(fila.Cells["codproducto"].Value.ToString());
				string unidad = fila.Cells["unidad"].Value.ToString();
				DataTable dt = admPro.MuestraUnidadesEquivalentes(pro.CodProducto, frmLogin.iCodAlmacen);
				if (!Enumerable.Any<DataRow>(dt.Rows.Cast<DataRow>(), (Func<DataRow, bool>)((DataRow x) => x["descripcion"].ToString() == unidad)))
				{
					MessageBox.Show("Falta agregar la unidad de medida " + unidad + " a la tabla equivalentes, por favor valide.", "AVISO", MessageBoxButtons.OK);
					return;
				}
			}
			if (ventana == null)
			{
				MessageBox.Show("ocurrio un error al intentar insertar los productos seleccionados, Intente cerrando y volviendo abrir la ventana.", "Imposible agregar productos a la Factura Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				if (rgvDetalle.SelectedRows.Count <= 0)
				{
					return;
				}
				List<GridViewRowInfo> items_eliminar = new List<GridViewRowInfo>();
				foreach (GridViewRowInfo fila2 in rgvDetalle.SelectedRows)
				{
					ventana.dgvDetalle.Rows.Add("", fila2.Cells["codproducto"].Value, fila2.Cells["referencia"].Value, fila2.Cells["descripcion"].Value, fila2.Cells["codunidad"].Value, "", fila2.Cells["unidad"].Value, "0", fila2.Cells["cantidad"].Value, fila2.Cells["preciounit"].Value, fila2.Cells["importe"].Value, fila2.Cells["dscto1"].Value, fila2.Cells["dscto2"].Value, fila2.Cells["dscto3"].Value, fila2.Cells["montodscto"].Value, fila2.Cells["valorventa"].Value, fila2.Cells["valorventa"].Value, 0.0, fila2.Cells["igv"].Value, fila2.Cells["precioventa"].Value, fila2.Cells["precioventa"].Value, fila2.Cells["precioreal"].Value, fila2.Cells["valorreal"].Value, "", "", "", "", "", "", "", fila2.Cells["coddetalle"].Value, fila2.Cells["stado"].Value);
					ventana.preciosGRC.Add(fila2.Cells["coddetalle"].Value.ToString(), fila2.Cells["preciounit"].Value.ToString());
					items_eliminar.Add(fila2);
				}
				if (items_eliminar.Count > 0)
				{
					foreach (GridViewRowInfo item in items_eliminar)
					{
						rgvDetalle.Rows.Remove(item);
					}
				}
				((DataTable)rgvDetalle.DataSource).AcceptChanges();
				ventana.facturagenerada = (DataTable)rgvDetalle.DataSource;
				base.DialogResult = DialogResult.Yes;
				Close();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Ocurrio un error al insertar productos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			base.DialogResult = DialogResult.Cancel;
		}
	}

	private DataTable conviertiendoDGVaData()
	{
		DataTable data = new DataTable();
		foreach (GridViewDataColumn col in rgvDetalle.Columns)
		{
			if (col.FieldName != "")
			{
				data.Columns.Add(col.FieldName);
			}
		}
		return data;
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn27 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn28 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn29 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn30 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn31 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvDetalle = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnAgregarItem = new System.Windows.Forms.Button();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).BeginInit();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.Controls.Add(this.rgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1170, 395);
		this.groupBox2.TabIndex = 21;
		this.groupBox2.TabStop = false;
		this.rgvDetalle.AutoScroll = true;
		this.rgvDetalle.AutoSizeRows = true;
		this.rgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.rgvDetalle.MasterTemplate.AllowAddNewRow = false;
		this.rgvDetalle.MasterTemplate.AllowColumnChooser = false;
		this.rgvDetalle.MasterTemplate.AllowColumnReorder = false;
		this.rgvDetalle.MasterTemplate.AllowDeleteRow = false;
		this.rgvDetalle.MasterTemplate.AllowDragToGroup = false;
		this.rgvDetalle.MasterTemplate.AllowEditRow = false;
		this.rgvDetalle.MasterTemplate.AllowRowResize = false;
		this.rgvDetalle.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codDetalle";
		gridViewTextBoxColumn1.HeaderText = "CodDetalle";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "coddetalle";
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.FieldName = "codProducto";
		gridViewTextBoxColumn2.HeaderText = "Código de Producto";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "codproducto";
		gridViewTextBoxColumn2.Width = 5;
		gridViewTextBoxColumn3.FieldName = "referencia";
		gridViewTextBoxColumn3.HeaderText = "Código de Producto";
		gridViewTextBoxColumn3.Name = "referencia";
		gridViewTextBoxColumn3.Width = 95;
		gridViewTextBoxColumn3.WrapText = true;
		gridViewTextBoxColumn4.FieldName = "producto";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "descripcion";
		gridViewTextBoxColumn4.Width = 216;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn5.HeaderText = "Cod. Unidad";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "codunidad";
		gridViewTextBoxColumn5.Width = 5;
		gridViewTextBoxColumn6.FieldName = "moneda";
		gridViewTextBoxColumn6.HeaderText = "Moneda";
		gridViewTextBoxColumn6.IsVisible = false;
		gridViewTextBoxColumn6.Name = "moneda";
		gridViewTextBoxColumn6.Width = 5;
		gridViewTextBoxColumn7.FieldName = "unidad";
		gridViewTextBoxColumn7.HeaderText = "Unidad";
		gridViewTextBoxColumn7.Name = "unidad";
		gridViewTextBoxColumn7.Width = 85;
		gridViewTextBoxColumn8.FieldName = "serielote";
		gridViewTextBoxColumn8.HeaderText = "Serie/Lote";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "serielote";
		gridViewTextBoxColumn8.Width = 5;
		gridViewTextBoxColumn9.FieldName = "cantidad";
		gridViewTextBoxColumn9.HeaderText = "Cantidad";
		gridViewTextBoxColumn9.Name = "cantidad";
		gridViewTextBoxColumn9.Width = 97;
		gridViewTextBoxColumn10.FieldName = "preciounitario";
		gridViewTextBoxColumn10.HeaderText = "P. Unit.";
		gridViewTextBoxColumn10.Name = "preciounit";
		gridViewTextBoxColumn10.Width = 97;
		gridViewTextBoxColumn11.FieldName = "subtotal";
		gridViewTextBoxColumn11.HeaderText = "Importe";
		gridViewTextBoxColumn11.Name = "importe";
		gridViewTextBoxColumn11.Width = 97;
		gridViewTextBoxColumn12.FieldName = "descuento1";
		gridViewTextBoxColumn12.HeaderText = "% Dscto 1";
		gridViewTextBoxColumn12.IsVisible = false;
		gridViewTextBoxColumn12.Name = "dscto1";
		gridViewTextBoxColumn12.Width = 5;
		gridViewTextBoxColumn13.FieldName = "descuento 2";
		gridViewTextBoxColumn13.HeaderText = "% Dscto 2";
		gridViewTextBoxColumn13.IsVisible = false;
		gridViewTextBoxColumn13.Name = "dscto2";
		gridViewTextBoxColumn13.Width = 5;
		gridViewTextBoxColumn14.FieldName = "descuento3";
		gridViewTextBoxColumn14.HeaderText = "% Dscto  3";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "dscto3";
		gridViewTextBoxColumn14.Width = 5;
		gridViewTextBoxColumn15.FieldName = "montodscto";
		gridViewTextBoxColumn15.HeaderText = "Monto Dscto";
		gridViewTextBoxColumn15.Name = "montodscto";
		gridViewTextBoxColumn15.Width = 97;
		gridViewTextBoxColumn16.FieldName = "valorventa";
		gridViewTextBoxColumn16.HeaderText = "V. Compra";
		gridViewTextBoxColumn16.Name = "valorventa";
		gridViewTextBoxColumn16.Width = 112;
		gridViewTextBoxColumn17.FieldName = "vvconflete";
		gridViewTextBoxColumn17.HeaderText = "vvconflete";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "valorventaconflete";
		gridViewTextBoxColumn17.Width = 5;
		gridViewTextBoxColumn18.FieldName = "flete";
		gridViewTextBoxColumn18.HeaderText = "Flete";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "flete0";
		gridViewTextBoxColumn18.Width = 5;
		gridViewTextBoxColumn19.FieldName = "igv";
		gridViewTextBoxColumn19.HeaderText = "IGV";
		gridViewTextBoxColumn19.Name = "igv";
		gridViewTextBoxColumn19.Width = 112;
		gridViewTextBoxColumn20.FieldName = "importe";
		gridViewTextBoxColumn20.HeaderText = "P. Compra";
		gridViewTextBoxColumn20.Name = "precioventa";
		gridViewTextBoxColumn20.Width = 116;
		gridViewTextBoxColumn21.FieldName = "pvconflete";
		gridViewTextBoxColumn21.HeaderText = "pvconflete";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "pvconflete";
		gridViewTextBoxColumn21.Width = 5;
		gridViewTextBoxColumn22.FieldName = "precioreal";
		gridViewTextBoxColumn22.HeaderText = "P. real";
		gridViewTextBoxColumn22.IsVisible = false;
		gridViewTextBoxColumn22.Name = "precioreal";
		gridViewTextBoxColumn22.Width = 5;
		gridViewTextBoxColumn23.FieldName = "valorreal";
		gridViewTextBoxColumn23.HeaderText = "V. real";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "valorreal";
		gridViewTextBoxColumn23.Width = 5;
		gridViewTextBoxColumn24.FieldName = "fechaingreso";
		gridViewTextBoxColumn24.HeaderText = "FechaIngre";
		gridViewTextBoxColumn24.IsVisible = false;
		gridViewTextBoxColumn24.Name = "fechaingreso";
		gridViewTextBoxColumn24.Width = 5;
		gridViewTextBoxColumn25.FieldName = "codUser";
		gridViewTextBoxColumn25.HeaderText = "CodUser";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "coduser";
		gridViewTextBoxColumn25.Width = 5;
		gridViewTextBoxColumn26.FieldName = "fecharegistro";
		gridViewTextBoxColumn26.HeaderText = "Fecha Reg.";
		gridViewTextBoxColumn26.IsVisible = false;
		gridViewTextBoxColumn26.Name = "fecharegistro";
		gridViewTextBoxColumn26.Width = 5;
		gridViewTextBoxColumn27.FieldName = "UltimoPrecioCompra";
		gridViewTextBoxColumn27.HeaderText = "Ultimo Precio de Compra";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "UltimoPrecioCompra";
		gridViewTextBoxColumn27.Width = 5;
		gridViewTextBoxColumn28.FieldName = "EstadoDeLaOrden";
		gridViewTextBoxColumn28.HeaderText = "EstadoOrden";
		gridViewTextBoxColumn28.IsVisible = false;
		gridViewTextBoxColumn28.Name = "EstadoDeLaOrden";
		gridViewTextBoxColumn28.Width = 5;
		gridViewTextBoxColumn29.FieldName = "ProductoSolicitado";
		gridViewTextBoxColumn29.HeaderText = "ProductoSolicitado";
		gridViewTextBoxColumn29.IsVisible = false;
		gridViewTextBoxColumn29.Name = "ProductoSolicitado";
		gridViewTextBoxColumn29.Width = 5;
		gridViewTextBoxColumn30.FieldName = "cantpendiente";
		gridViewTextBoxColumn30.HeaderText = "cantpendiente";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "cantpendiente";
		gridViewTextBoxColumn30.Width = 5;
		gridViewTextBoxColumn31.FieldName = "stado";
		gridViewTextBoxColumn31.HeaderText = "stado";
		gridViewTextBoxColumn31.Name = "stado";
		gridViewTextBoxColumn31.Width = 49;
		this.rgvDetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31);
		this.rgvDetalle.MasterTemplate.EnableFiltering = true;
		this.rgvDetalle.MasterTemplate.EnableGrouping = false;
		this.rgvDetalle.MasterTemplate.MultiSelect = true;
		this.rgvDetalle.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalle.Name = "rgvDetalle";
		this.rgvDetalle.Size = new System.Drawing.Size(1164, 376);
		this.rgvDetalle.TabIndex = 69;
		this.groupBox1.Controls.Add(this.btnAgregarItem);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 387);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1170, 58);
		this.groupBox1.TabIndex = 22;
		this.groupBox1.TabStop = false;
		this.btnAgregarItem.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAgregarItem.BackColor = System.Drawing.Color.White;
		this.btnAgregarItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnAgregarItem.ForeColor = System.Drawing.SystemColors.ControlText;
		this.btnAgregarItem.Image = SIGEFA.Properties.Resources.agregar;
		this.btnAgregarItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAgregarItem.Location = new System.Drawing.Point(946, 14);
		this.btnAgregarItem.Name = "btnAgregarItem";
		this.btnAgregarItem.Size = new System.Drawing.Size(212, 32);
		this.btnAgregarItem.TabIndex = 85;
		this.btnAgregarItem.Text = "Agregar Producto A Factura Compra";
		this.btnAgregarItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAgregarItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAgregarItem.UseVisualStyleBackColor = false;
		this.btnAgregarItem.Click += new System.EventHandler(btnAgregarItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1170, 445);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "frmPreListadoGRCaFC";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Pre Listado Guia de Remision de Compra";
		base.Load += new System.EventHandler(frmPreListadoGRCaFC_Load);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalle).EndInit();
		this.groupBox1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
