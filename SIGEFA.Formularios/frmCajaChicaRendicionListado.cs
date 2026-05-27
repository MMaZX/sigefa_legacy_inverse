using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SIGEFA.Formularios;

public class frmCajaChicaRendicionListado : Office2007Form
{
	public int tipocaja = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private decimal Total = default(decimal);

	private IContainer components = null;

	private Panel panel1;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem tsmiAdjuntarDocumentacion;

	private Button btnSalir;

	private ImageList imageList2;

	private Panel panel2;

	private Label lblTotal;

	private Label label1;

	private DataGridView dgvRendiciones;

	private Button btnEliminar;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn codPersonalizado;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn numDocumento;

	private DataGridViewTextBoxColumn numGuia;

	private DataGridViewTextBoxColumn numRecLiquidacion;

	private DataGridViewTextBoxColumn codTipoPagoCaja;

	private DataGridViewTextBoxColumn tipopagocaja;

	private DataGridViewTextBoxColumn cargadescarga;

	private DataGridViewTextBoxColumn tipoMovimiento;

	private DataGridViewTextBoxColumn toneladas;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn comentario;

	private ButtonItem biImprimir;

	private ButtonItem buttonItem1;

	private Button btnImprimir;

	private ImageList imageList1;

	public frmCajaChicaRendicionListado()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmCajaChicaRendicionListado_Load(object sender, EventArgs e)
	{
	}

	private void tsmiEliminarRendicion(object sender, EventArgs e)
	{
	}

	private void dgvRendiciones_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.Button != MouseButtons.Right || e.RowIndex <= -1)
		{
			return;
		}
		foreach (DataGridViewRow dr in dgvRendiciones.SelectedRows)
		{
			dr.Selected = false;
		}
		dgvRendiciones.Rows[e.RowIndex].Selected = true;
		contextMenuStrip1.Show(Control.MousePosition.X, Control.MousePosition.Y);
	}

	private void CalculaMontos()
	{
		try
		{
			Total = default(decimal);
			if (dgvRendiciones.RowCount > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvRendiciones.Rows)
				{
					Total += Convert.ToDecimal(row.Cells[monto.Name].Value);
				}
			}
			else
			{
				Total = default(decimal);
			}
			lblTotal.Text = Total.ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void dgvRendiciones_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		CalculaMontos();
	}

	private void dgvRendiciones_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		CalculaMontos();
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.CodCotizacion = tipocaja;
		frm.tipo = 17;
		frm.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmCajaChicaRendicionListado));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnEliminar = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.tsmiAdjuntarDocumentacion = new System.Windows.Forms.ToolStripMenuItem();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.lblTotal = new System.Windows.Forms.Label();
		this.dgvRendiciones = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codPersonalizado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numGuia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numRecLiquidacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipoPagoCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipopagocaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cargadescarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.toneladas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.panel1.SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRendiciones).BeginInit();
		base.SuspendLayout();
		this.panel1.Controls.Add(this.btnImprimir);
		this.panel1.Controls.Add(this.btnEliminar);
		this.panel1.Controls.Add(this.btnSalir);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(0, 263);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(984, 42);
		this.panel1.TabIndex = 0;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(187, 5);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(94, 32);
		this.btnImprimir.TabIndex = 38;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEliminar.ImageIndex = 1;
		this.btnEliminar.ImageList = this.imageList2;
		this.btnEliminar.Location = new System.Drawing.Point(33, 4);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(128, 35);
		this.btnEliminar.TabIndex = 37;
		this.btnEliminar.Text = "Eliminar Seleccion";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
		this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
		this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
		this.imageList2.Images.SetKeyName(3, "Filter.png");
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList2;
		this.btnSalir.Location = new System.Drawing.Point(909, 4);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(70, 35);
		this.btnSalir.TabIndex = 36;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.tsmiAdjuntarDocumentacion });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(174, 26);
		this.tsmiAdjuntarDocumentacion.Image = (System.Drawing.Image)resources.GetObject("tsmiAdjuntarDocumentacion.Image");
		this.tsmiAdjuntarDocumentacion.Name = "tsmiAdjuntarDocumentacion";
		this.tsmiAdjuntarDocumentacion.Size = new System.Drawing.Size(173, 22);
		this.tsmiAdjuntarDocumentacion.Text = "Eliminar Rendicion";
		this.tsmiAdjuntarDocumentacion.Click += new System.EventHandler(tsmiEliminarRendicion);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.lblTotal);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(984, 40);
		this.panel2.TabIndex = 3;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(715, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(116, 13);
		this.label1.TabIndex = 47;
		this.label1.Text = "TOTAL RENDIDO S/.:";
		this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTotal.BackColor = System.Drawing.Color.Transparent;
		this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lblTotal.Font = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotal.ForeColor = System.Drawing.SystemColors.WindowText;
		this.lblTotal.Location = new System.Drawing.Point(837, 9);
		this.lblTotal.Name = "lblTotal";
		this.lblTotal.Size = new System.Drawing.Size(135, 23);
		this.lblTotal.TabIndex = 46;
		this.lblTotal.Text = "0000.000";
		this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.dgvRendiciones.AllowUserToAddRows = false;
		this.dgvRendiciones.AllowUserToDeleteRows = false;
		this.dgvRendiciones.AllowUserToResizeRows = false;
		this.dgvRendiciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvRendiciones.Columns.AddRange(this.codigo, this.codPersonalizado, this.concepto, this.fecha, this.numDocumento, this.numGuia, this.numRecLiquidacion, this.codTipoPagoCaja, this.tipopagocaja, this.cargadescarga, this.tipoMovimiento, this.toneladas, this.monto, this.comentario);
		this.dgvRendiciones.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvRendiciones.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvRendiciones.Location = new System.Drawing.Point(0, 40);
		this.dgvRendiciones.Name = "dgvRendiciones";
		this.dgvRendiciones.RowHeadersVisible = false;
		this.dgvRendiciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvRendiciones.Size = new System.Drawing.Size(984, 223);
		this.dgvRendiciones.TabIndex = 10;
		this.dgvRendiciones.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvRendiciones_CellMouseClick);
		this.dgvRendiciones.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvRendiciones_RowsAdded);
		this.dgvRendiciones.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvRendiciones_RowsRemoved);
		this.codigo.DataPropertyName = "codCajaChica";
		this.codigo.HeaderText = "COD";
		this.codigo.Name = "codigo";
		this.codigo.Visible = false;
		this.codigo.Width = 70;
		this.codPersonalizado.DataPropertyName = "codPersonalizado";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.codPersonalizado.DefaultCellStyle = dataGridViewCellStyle1;
		this.codPersonalizado.HeaderText = "CODIGO";
		this.codPersonalizado.Name = "codPersonalizado";
		this.codPersonalizado.Width = 70;
		this.concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.concepto.DataPropertyName = "concepto";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.concepto.DefaultCellStyle = dataGridViewCellStyle2;
		this.concepto.HeaderText = "CONCEPTO";
		this.concepto.Name = "concepto";
		this.fecha.DataPropertyName = "fecha";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle3;
		this.fecha.HeaderText = "FECHA";
		this.fecha.Name = "fecha";
		this.numDocumento.DataPropertyName = "NumDocumento";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numDocumento.DefaultCellStyle = dataGridViewCellStyle4;
		this.numDocumento.HeaderText = "N° DOC.";
		this.numDocumento.Name = "numDocumento";
		this.numDocumento.Width = 80;
		this.numGuia.DataPropertyName = "numGuia";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numGuia.DefaultCellStyle = dataGridViewCellStyle5;
		this.numGuia.HeaderText = "N° GUIA";
		this.numGuia.Name = "numGuia";
		this.numGuia.Width = 80;
		this.numRecLiquidacion.DataPropertyName = "numRecLiquidacion";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.numRecLiquidacion.DefaultCellStyle = dataGridViewCellStyle6;
		this.numRecLiquidacion.HeaderText = "N° REC. LIQ.";
		this.numRecLiquidacion.Name = "numRecLiquidacion";
		this.numRecLiquidacion.Width = 80;
		this.codTipoPagoCaja.DataPropertyName = "codTipoPagoCaja";
		this.codTipoPagoCaja.HeaderText = "COD. TIPO PAGO";
		this.codTipoPagoCaja.Name = "codTipoPagoCaja";
		this.codTipoPagoCaja.Visible = false;
		this.tipopagocaja.DataPropertyName = "TipoPagoCaja";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.tipopagocaja.DefaultCellStyle = dataGridViewCellStyle7;
		this.tipopagocaja.HeaderText = "TIPO PAGO";
		this.tipopagocaja.Name = "tipopagocaja";
		this.tipopagocaja.Width = 200;
		this.cargadescarga.DataPropertyName = "cargadescarga";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.cargadescarga.DefaultCellStyle = dataGridViewCellStyle8;
		this.cargadescarga.HeaderText = "CARGA/DESCARGA";
		this.cargadescarga.Name = "cargadescarga";
		this.cargadescarga.Visible = false;
		this.cargadescarga.Width = 120;
		this.tipoMovimiento.DataPropertyName = "TipoMovimiento";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
		this.tipoMovimiento.DefaultCellStyle = dataGridViewCellStyle9;
		this.tipoMovimiento.HeaderText = "TIPO MOV.";
		this.tipoMovimiento.Name = "tipoMovimiento";
		this.tipoMovimiento.Visible = false;
		this.tipoMovimiento.Width = 80;
		this.toneladas.DataPropertyName = "toneladas";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		dataGridViewCellStyle10.NullValue = null;
		dataGridViewCellStyle10.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.toneladas.DefaultCellStyle = dataGridViewCellStyle10;
		this.toneladas.HeaderText = "TONELADAS";
		this.toneladas.Name = "toneladas";
		this.toneladas.Visible = false;
		this.toneladas.Width = 80;
		this.monto.DataPropertyName = "monto";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Navy;
		dataGridViewCellStyle11.Format = "N2";
		dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.monto.DefaultCellStyle = dataGridViewCellStyle11;
		this.monto.HeaderText = "MONTO";
		this.monto.Name = "monto";
		this.comentario.DataPropertyName = "comentario";
		this.comentario.HeaderText = "COMENTARIO";
		this.comentario.Name = "comentario";
		this.comentario.Visible = false;
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingHorizontal = 10;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Visible = false;
		this.buttonItem1.ImageIndex = 7;
		this.buttonItem1.ImagePaddingHorizontal = 10;
		this.buttonItem1.ImagePaddingVertical = 15;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Imprimir";
		this.buttonItem1.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(984, 305);
		base.Controls.Add(this.dgvRendiciones);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCajaChicaRendicionListado";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Rendiciones";
		base.Load += new System.EventHandler(frmCajaChicaRendicionListado_Load);
		this.panel1.ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRendiciones).EndInit();
		base.ResumeLayout(false);
	}
}
