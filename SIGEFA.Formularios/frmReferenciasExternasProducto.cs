using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmReferenciasExternasProducto : Form
{
	public int codProducto = 0;

	public int codUnidadMedida = 0;

	private clsAdmProducto admproducto = new clsAdmProducto();

	private IContainer components = null;

	private GroupBox groupBox1;

	private RadGridView rgvReferencias;

	private GroupBox groupBox2;

	private Label label2;

	private Label label1;

	public Label lblUnidad;

	public Label lblProducto;

	public frmReferenciasExternasProducto()
	{
		InitializeComponent();
	}

	private void frmReferenciasExternasProducto_Load(object sender, EventArgs e)
	{
		try
		{
			cargarLista();
		}
		catch (Exception)
		{
			throw;
		}
	}

	private void cargarLista()
	{
		rgvReferencias.DataSource = admproducto.cargaReferenciasExternas(codProducto, codUnidadMedida);
	}

	private void rgvReferencias_UserAddedRow(object sender, GridViewRowEventArgs e)
	{
		try
		{
			GridViewRowInfo fila = e.Row;
			if (fila.Index < 0)
			{
				return;
			}
			string nombreTienda = obtenerString(fila.Cells["colNombreTienda"].Value);
			string precioReferencial = obtenerString(fila.Cells["colPVReferencial"].Value);
			string skuProducto = obtenerString(fila.Cells["colSKU"].Value);
			if (!(nombreTienda == "") || !(precioReferencial == "") || !(skuProducto == ""))
			{
				ReferenciaExterna obj = new ReferenciaExterna
				{
					codProducto = codProducto,
					codUnidadMedida = codUnidadMedida,
					nombreTienda = nombreTienda,
					skuProducto = skuProducto,
					precioReferencial = Convert.ToDouble(precioReferencial)
				};
				if (admproducto.registraReferenciaExterna(ref obj))
				{
					fila.Cells["colCodReferenciaExterna"].Value = obj.codReferenciaExterna;
					MessageBox.Show("Referencia Externa Registrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private string obtenerString(object value, string defecto = "")
	{
		if (value == DBNull.Value || value == null)
		{
			return defecto;
		}
		if (value.ToString().Trim() == "")
		{
			return defecto;
		}
		return value.ToString().Trim();
	}

	private void rgvReferencias_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex < 0)
			{
				return;
			}
			GridViewRowInfo fila = rgvReferencias.Rows[e.RowIndex];
			if (fila.Cells["colCodReferenciaExterna"].Value == null || fila.Cells["colCodReferenciaExterna"].Value == DBNull.Value)
			{
				return;
			}
			string nombreTienda = obtenerString(fila.Cells["colNombreTienda"].Value);
			string precioReferencial = obtenerString(fila.Cells["colPVReferencial"].Value);
			string skuProducto = obtenerString(fila.Cells["colSKU"].Value);
			if (!(nombreTienda == "") || !(precioReferencial == "") || !(skuProducto == ""))
			{
				ReferenciaExterna obj = new ReferenciaExterna
				{
					codReferenciaExterna = Convert.ToInt32(fila.Cells["colCodReferenciaExterna"].Value),
					codProducto = codProducto,
					codUnidadMedida = codUnidadMedida,
					nombreTienda = nombreTienda,
					skuProducto = skuProducto,
					precioReferencial = Convert.ToDouble(precioReferencial)
				};
				if (!admproducto.editaReferenciaExterna(obj))
				{
					MessageBox.Show("Error al editar campo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rgvReferencias_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
	{
		try
		{
			if (e.Rows.Length < 1)
			{
				return;
			}
			int contador = 0;
			int eliminar = e.Rows.Length;
			GridViewRowInfo[] rows = e.Rows;
			foreach (GridViewRowInfo fila in rows)
			{
				if (fila.Cells["colCodReferenciaExterna"].Value != null && fila.Cells["colCodReferenciaExterna"].Value != DBNull.Value)
				{
					int codReferencia = Convert.ToInt32(fila.Cells["colCodReferenciaExterna"].Value);
					if (admproducto.eliminaReferenciaExterna(codReferencia))
					{
						contador++;
					}
				}
			}
			MessageBox.Show("Se ha eliminado con exito " + contador + " referencia(s).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			cargarLista();
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvReferencias = new Telerik.WinControls.UI.RadGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lblUnidad = new System.Windows.Forms.Label();
		this.lblProducto = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvReferencias).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvReferencias.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.rgvReferencias);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 72);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(478, 202);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.rgvReferencias.AutoScroll = true;
		this.rgvReferencias.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvReferencias.Location = new System.Drawing.Point(3, 16);
		this.rgvReferencias.MasterTemplate.AllowDragToGroup = false;
		this.rgvReferencias.MasterTemplate.AutoGenerateColumns = false;
		this.rgvReferencias.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codReferenciaExterna";
		gridViewTextBoxColumn1.HeaderText = "codReferenciaExterna";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodReferenciaExterna";
		gridViewTextBoxColumn1.Width = 46;
		gridViewTextBoxColumn2.FieldName = "nombre_tienda";
		gridViewTextBoxColumn2.HeaderText = "TIENDA REFERENCIAL";
		gridViewTextBoxColumn2.Name = "colNombreTienda";
		gridViewTextBoxColumn2.Width = 169;
		gridViewTextBoxColumn2.WrapText = true;
		gridViewDecimalColumn1.FieldName = "precio_referencial";
		gridViewDecimalColumn1.HeaderText = "PRECIO DE VENTA REFERENCIAL";
		gridViewDecimalColumn1.Name = "colPVReferencial";
		gridViewDecimalColumn1.Width = 135;
		gridViewDecimalColumn1.WrapText = true;
		gridViewTextBoxColumn3.FieldName = "sku_producto";
		gridViewTextBoxColumn3.HeaderText = "SKU PRODUCTO";
		gridViewTextBoxColumn3.Name = "colSKU";
		gridViewTextBoxColumn3.Width = 169;
		gridViewTextBoxColumn3.WrapText = true;
		this.rgvReferencias.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewDecimalColumn1, gridViewTextBoxColumn3);
		this.rgvReferencias.MasterTemplate.EnableGrouping = false;
		this.rgvReferencias.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvReferencias.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvReferencias.Name = "rgvReferencias";
		this.rgvReferencias.Size = new System.Drawing.Size(472, 183);
		this.rgvReferencias.TabIndex = 1;
		this.rgvReferencias.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvReferencias_CellEndEdit);
		this.rgvReferencias.UserAddedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(rgvReferencias_UserAddedRow);
		this.rgvReferencias.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(rgvReferencias_UserDeletingRow);
		this.groupBox2.Controls.Add(this.lblUnidad);
		this.groupBox2.Controls.Add(this.lblProducto);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(478, 66);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.lblUnidad.AutoSize = true;
		this.lblUnidad.Location = new System.Drawing.Point(103, 40);
		this.lblUnidad.Name = "lblUnidad";
		this.lblUnidad.Size = new System.Drawing.Size(0, 13);
		this.lblUnidad.TabIndex = 3;
		this.lblProducto.AutoSize = true;
		this.lblProducto.Location = new System.Drawing.Point(71, 16);
		this.lblProducto.Name = "lblProducto";
		this.lblProducto.Size = new System.Drawing.Size(0, 13);
		this.lblProducto.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(12, 40);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(85, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Unidad Medida: ";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(12, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Producto:";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(478, 274);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmReferenciasExternasProducto";
		this.Text = "Listado Referencias Externas de Producto";
		base.Load += new System.EventHandler(frmReferenciasExternasProducto_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvReferencias.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvReferencias).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		base.ResumeLayout(false);
	}
}
