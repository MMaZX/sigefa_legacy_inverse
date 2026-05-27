using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmInterReqAlmTransf : Form
{
	public DataTable data = null;

	public int Proceso = 1;

	internal frmReqAlmacen ventana_ra = null;

	internal frmDespacho ventana_d = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnOk;

	private Button btnCancelar;

	private GroupBox groupBox2;

	private RadGridView rgvintermedia;

	public frmInterReqAlmTransf()
	{
		InitializeComponent();
	}

	private void frmInterReqAlmTransf_Load(object sender, EventArgs e)
	{
		foreach (GridViewDataColumn col in rgvintermedia.Columns)
		{
			col.ReadOnly = true;
			if (col.Name == "colCantidad")
			{
				col.ReadOnly = false;
			}
		}
		rgvintermedia.DataSource = data;
		foreach (GridViewRowInfo fila in rgvintermedia.Rows)
		{
			fila.Cells["colcantidadnoeditable"].Value = Convert.ToDouble(fila.Cells["colCantidad"].Value);
		}
		if (Proceso != 2)
		{
			return;
		}
		rgvintermedia.Columns["colDescripAlmacen"].IsVisible = true;
		foreach (GridViewRowInfo fila2 in rgvintermedia.Rows)
		{
			if (Convert.ToInt32(fila2.Cells["colEditar"].Value) == 0)
			{
				fila2.Cells["colCantidad"].ReadOnly = true;
				fila2.Cells["colCantidad"].Value = 0;
			}
		}
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		if (!verificarCantidades())
		{
			return;
		}
		if (data != null)
		{
			data.AcceptChanges();
		}
		switch (Proceso)
		{
		case 1:
			if (ventana_ra != null)
			{
				ventana_ra.dataCtdadGenerarTrans = data;
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Abort;
			}
			break;
		case 2:
			if (ventana_d != null)
			{
				ventana_d.dataCtdadGenerarEntrega = data;
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.Abort;
			}
			break;
		default:
			MessageBox.Show("proceso a seguir indefinido");
			break;
		}
	}

	private bool verificarCantidades()
	{
		try
		{
			bool band = true;
			foreach (GridViewRowInfo fila in rgvintermedia.Rows)
			{
				band = Convert.ToDouble(fila.Cells["colCantidad"].Value) <= Convert.ToDouble(fila.Cells["colcantidadnoeditable"].Value);
				if (!band)
				{
					MessageBox.Show("No puede ingresar una cantidad mayor que " + Convert.ToDouble(fila.Cells["colcantidadnoeditable"].Value) + " para " + fila.Cells["colDescProducto"].Value.ToString(), "Verificacion de Cantidades", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				}
			}
			return band;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Verificacion de Cantidades", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return false;
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private void rgvintermedia_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (Proceso == 2 && Convert.ToInt32(e.Row.Cells["colEditar"].Value ?? ((object)0)) == 0)
		{
			MessageBox.Show("No tiene permitido realizar entrega de este almacen.", "No Puede Entregar de Este Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void rgvintermedia_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			GridViewRowInfo fila = e.Row;
			if (!(Convert.ToDouble(fila.Cells["colCantidad"].Value) <= Convert.ToDouble(fila.Cells["colcantidadnoeditable"].Value)))
			{
				MessageBox.Show("No puede ingresar una cantidad mayor que " + Convert.ToDouble(fila.Cells["colcantidadnoeditable"].Value) + " para " + fila.Cells["colDescProducto"].Value.ToString(), "Verificacion de Cantidades", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				fila.Cells["colCantidad"].Value = Convert.ToDouble(fila.Cells["colcantidadnoeditable"].Value);
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvintermedia = new Telerik.WinControls.UI.RadGridView();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvintermedia).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvintermedia.MasterTemplate).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnOk);
		this.groupBox1.Controls.Add(this.btnCancelar);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 306);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(744, 84);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnOk.Location = new System.Drawing.Point(518, 24);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(107, 33);
		this.btnOk.TabIndex = 0;
		this.btnOk.Text = "Confirmar";
		this.btnOk.UseVisualStyleBackColor = true;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Location = new System.Drawing.Point(631, 24);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(107, 33);
		this.btnCancelar.TabIndex = 0;
		this.btnCancelar.Text = "Cancelar";
		this.btnCancelar.UseVisualStyleBackColor = false;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.groupBox2.Controls.Add(this.rgvintermedia);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 0);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(744, 306);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.rgvintermedia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvintermedia.EnableGestures = false;
		this.rgvintermedia.Location = new System.Drawing.Point(3, 16);
		this.rgvintermedia.MasterTemplate.AllowAddNewRow = false;
		this.rgvintermedia.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "descripAlmacen";
		gridViewTextBoxColumn1.HeaderText = "Almacen";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colDescripAlmacen";
		gridViewTextBoxColumn1.Width = 143;
		gridViewTextBoxColumn2.FieldName = "codDetalle";
		gridViewTextBoxColumn2.HeaderText = "codDetalle";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodDetalle";
		gridViewTextBoxColumn3.FieldName = "refProducto";
		gridViewTextBoxColumn3.HeaderText = "Referencia";
		gridViewTextBoxColumn3.Name = "colRefProducto";
		gridViewTextBoxColumn3.Width = 166;
		gridViewTextBoxColumn4.FieldName = "descProducto";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "colDescProducto";
		gridViewTextBoxColumn4.Width = 317;
		gridViewTextBoxColumn5.FieldName = "descUnidad";
		gridViewTextBoxColumn5.HeaderText = "Unidad";
		gridViewTextBoxColumn5.Name = "colDescUnidad";
		gridViewTextBoxColumn5.Width = 134;
		gridViewTextBoxColumn6.FieldName = "cantidad";
		gridViewTextBoxColumn6.HeaderText = "Cantidad";
		gridViewTextBoxColumn6.Name = "colCantidad";
		gridViewTextBoxColumn6.Width = 123;
		gridViewTextBoxColumn7.FieldName = "editar";
		gridViewTextBoxColumn7.HeaderText = "opcionEditar";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "colEditar";
		gridViewTextBoxColumn7.Width = 46;
		gridViewTextBoxColumn8.HeaderText = "CantidadNoEditable";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colcantidadnoeditable";
		gridViewTextBoxColumn8.ReadOnly = true;
		gridViewTextBoxColumn8.Width = 47;
		this.rgvintermedia.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8);
		this.rgvintermedia.MasterTemplate.EnableFiltering = true;
		this.rgvintermedia.MasterTemplate.EnableGrouping = false;
		this.rgvintermedia.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvintermedia.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvintermedia.Name = "rgvintermedia";
		this.rgvintermedia.Size = new System.Drawing.Size(738, 287);
		this.rgvintermedia.TabIndex = 0;
		this.rgvintermedia.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvintermedia_CellEndEdit);
		this.rgvintermedia.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvintermedia_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(744, 390);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmInterReqAlmTransf";
		this.Text = "GenerandoTransferencia";
		base.Load += new System.EventHandler(frmInterReqAlmTransf_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvintermedia.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvintermedia).EndInit();
		base.ResumeLayout(false);
	}
}
