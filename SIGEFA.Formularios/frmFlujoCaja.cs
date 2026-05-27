using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmFlujoCaja : Office2007Form
{
	private clsAdmFlujoCaja AdmFlu = new clsAdmFlujoCaja();

	private clsFlujoCaja flu = new clsFlujoCaja();

	private clsCaja aper = new clsCaja();

	private clsAdmAperturaCierre AdmAper = new clsAdmAperturaCierre();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private int proceso = 0;

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar1;

	private ButtonItem biNuevo;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem biActualizar;

	private ButtonItem buttonItem7;

	private ButtonItem biImprimir;

	private ButtonItem buttonItem9;

	public DataGridView dgvFlujoCaja;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn concepto;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn tipo;

	private DataGridViewTextBoxColumn codAlmacen;

	public frmFlujoCaja()
	{
		InitializeComponent();
	}

	private void frmFlujoCaja_Load(object sender, EventArgs e)
	{
		CargarFlujoCaja();
	}

	private void CargarFlujoCaja()
	{
		dgvFlujoCaja.DataSource = data;
		data.DataSource = AdmFlu.MuestraFlujoCaja(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvFlujoCaja.ClearSelection();
		DarFormato();
	}

	public void SOLONumeros(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void DarFormato()
	{
		if (dgvFlujoCaja.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvFlujoCaja.Rows)
		{
			if (Convert.ToString(row.Cells[tipo.Name].Value) == "EGRESO")
			{
				row.Cells[monto.Name].Style.ForeColor = Color.Red;
			}
			else
			{
				row.Cells[monto.Name].Style.ForeColor = Color.Blue;
			}
		}
	}

	private void dgvFlujoCaja_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		frmFlujoCajaRegistro frm = new frmFlujoCajaRegistro();
		frm.Proceso = 3;
		frm.CodFlujoCaja = Convert.ToInt32(dgvFlujoCaja.SelectedRows[0].Cells[codigo.Name].Value);
		frm.txtconcepto.Text = Convert.ToString(dgvFlujoCaja.SelectedRows[0].Cells[concepto.Name].Value);
		frm.txtmonto.Text = Convert.ToString(dgvFlujoCaja.SelectedRows[0].Cells[monto.Name].Value);
		frm.dtpfecha.Value = Convert.ToDateTime(dgvFlujoCaja.SelectedRows[0].Cells[fecha.Name].Value);
		string tipos = Convert.ToString(dgvFlujoCaja.SelectedRows[0].Cells[tipo.Name].Value);
		if (tipos == "INGRESO")
		{
			frm.cboTipo.SelectedItem = "INGRESO";
		}
		else if (tipos == "EGRESO")
		{
			frm.cboTipo.SelectedItem = "EGRESO";
		}
		frm.ShowDialog();
	}

	private void buttonItem3_Click(object sender, EventArgs e)
	{
		frmFlujoCajaRegistro frm = new frmFlujoCajaRegistro();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargarFlujoCaja();
	}

	private void biEditar_Click(object sender, EventArgs e)
	{
		frmFlujoCajaRegistro frm = new frmFlujoCajaRegistro();
		frm.Proceso = 2;
		frm.CodFlujoCaja = Convert.ToInt32(dgvFlujoCaja.SelectedRows[0].Cells[codigo.Name].Value);
		frm.txtconcepto.Text = Convert.ToString(dgvFlujoCaja.SelectedRows[0].Cells[concepto.Name].Value);
		frm.txtmonto.Text = Convert.ToString(dgvFlujoCaja.SelectedRows[0].Cells[monto.Name].Value);
		frm.dtpfecha.Value = Convert.ToDateTime(dgvFlujoCaja.SelectedRows[0].Cells[fecha.Name].Value);
		string tipos = Convert.ToString(dgvFlujoCaja.SelectedRows[0].Cells[tipo.Name].Value);
		if (tipos == "INGRESO")
		{
			frm.cboTipo.SelectedItem = "INGRESO";
		}
		else if (tipos == "EGRESO")
		{
			frm.cboTipo.SelectedItem = "EGRESO";
		}
		frm.ShowDialog();
		CargarFlujoCaja();
	}

	private void biEliminar_Click(object sender, EventArgs e)
	{
		flu.CodFlujoCaja = Convert.ToInt32(dgvFlujoCaja.SelectedRows[0].Cells[codigo.Name].Value);
		if (dgvFlujoCaja.CurrentRow.Index != -1 && flu.CodFlujoCaja != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "CONTROL DE FLUJO DE CAJA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmFlu.Delete(flu.CodFlujoCaja, frmLogin.iCodAlmacen))
			{
				CargarFlujoCaja();
				biEliminar.Enabled = false;
				biEditar.Enabled = false;
			}
		}
	}

	private void buttonItem6_Click(object sender, EventArgs e)
	{
		CargarFlujoCaja();
	}

	private void buttonItem8_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvFlujoCaja.RowCount > 0)
			{
				frmParamLiquidacionCaja frm = new frmParamLiquidacionCaja();
				frm.ShowDialog();
			}
			else
			{
				MessageBox.Show("No Tiene Datos para Mostrar en Reporte", "Alerta ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message.ToString());
		}
	}

	private void buttonItem9_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmFlujoCaja));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.biNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.dgvFlujoCaja = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgvFlujoCaja).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[7] { this.biNuevo, this.biEditar, this.biEliminar, this.biActualizar, this.buttonItem7, this.biImprimir, this.buttonItem9 });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(1146, 65);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 6;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.biNuevo.ImageIndex = 4;
		this.biNuevo.ImagePaddingHorizontal = 10;
		this.biNuevo.ImagePaddingVertical = 15;
		this.biNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNuevo.Name = "biNuevo";
		this.biNuevo.SubItemsExpandWidth = 14;
		this.biNuevo.Text = "Nuevo";
		this.biNuevo.Click += new System.EventHandler(buttonItem3_Click);
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePaddingHorizontal = 10;
		this.biEditar.ImagePaddingVertical = 15;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Editar";
		this.biEditar.Click += new System.EventHandler(biEditar_Click);
		this.biEliminar.ImageIndex = 5;
		this.biEliminar.ImagePaddingHorizontal = 10;
		this.biEliminar.ImagePaddingVertical = 15;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Click += new System.EventHandler(biEliminar_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingHorizontal = 10;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(buttonItem6_Click);
		this.buttonItem7.ImageIndex = 11;
		this.buttonItem7.ImagePaddingHorizontal = 10;
		this.buttonItem7.ImagePaddingVertical = 15;
		this.buttonItem7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem7.Name = "buttonItem7";
		this.buttonItem7.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlB);
		this.buttonItem7.SubItemsExpandWidth = 14;
		this.buttonItem7.Text = "Buscar";
		this.buttonItem7.Visible = false;
		this.biImprimir.ImageIndex = 7;
		this.biImprimir.ImagePaddingHorizontal = 10;
		this.biImprimir.ImagePaddingVertical = 15;
		this.biImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimir.Name = "biImprimir";
		this.biImprimir.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlP);
		this.biImprimir.SubItemsExpandWidth = 14;
		this.biImprimir.Text = "Imprimir";
		this.biImprimir.Click += new System.EventHandler(buttonItem8_Click);
		this.buttonItem9.ImageIndex = 18;
		this.buttonItem9.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.SubItemsExpandWidth = 14;
		this.buttonItem9.Text = "Salir";
		this.buttonItem9.Click += new System.EventHandler(buttonItem9_Click);
		this.dgvFlujoCaja.AllowUserToAddRows = false;
		this.dgvFlujoCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvFlujoCaja.Columns.AddRange(this.codigo, this.fecha, this.concepto, this.monto, this.tipo, this.codAlmacen);
		this.dgvFlujoCaja.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvFlujoCaja.Location = new System.Drawing.Point(0, 65);
		this.dgvFlujoCaja.MultiSelect = false;
		this.dgvFlujoCaja.Name = "dgvFlujoCaja";
		this.dgvFlujoCaja.ReadOnly = true;
		this.dgvFlujoCaja.RowHeadersVisible = false;
		this.dgvFlujoCaja.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvFlujoCaja.Size = new System.Drawing.Size(1146, 458);
		this.dgvFlujoCaja.TabIndex = 7;
		this.dgvFlujoCaja.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvFlujoCaja_CellDoubleClick);
		this.codigo.DataPropertyName = "codFlujoCaja";
		this.codigo.HeaderText = "CODIGO";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "FECHA";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Width = 90;
		this.concepto.DataPropertyName = "concepto";
		this.concepto.HeaderText = "CONCEPTO";
		this.concepto.Name = "concepto";
		this.concepto.ReadOnly = true;
		this.concepto.Width = 750;
		this.monto.DataPropertyName = "monto";
		this.monto.HeaderText = "MONTO";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.monto.Width = 120;
		this.tipo.DataPropertyName = "tipo";
		this.tipo.HeaderText = "TIPO";
		this.tipo.Name = "tipo";
		this.tipo.ReadOnly = true;
		this.tipo.Width = 250;
		this.codAlmacen.DataPropertyName = "codAlmacen";
		this.codAlmacen.HeaderText = "ALMACEN";
		this.codAlmacen.Name = "codAlmacen";
		this.codAlmacen.ReadOnly = true;
		this.codAlmacen.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1146, 523);
		base.Controls.Add(this.dgvFlujoCaja);
		base.Controls.Add(this.ribbonBar1);
		this.DoubleBuffered = true;
		base.Name = "frmFlujoCaja";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "CONTROL DE FLUJO CAJA";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmFlujoCaja_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvFlujoCaja).EndInit();
		base.ResumeLayout(false);
	}
}
