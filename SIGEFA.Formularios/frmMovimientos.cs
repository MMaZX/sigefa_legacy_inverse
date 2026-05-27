using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmMovimientos : Office2007Form
{
	private clsAdmCtaCte admcta = new clsAdmCtaCte();

	private clsCtaCte cta = new clsCtaCte();

	private DataTable dt = new DataTable();

	public static BindingSource data = new BindingSource();

	private IContainer components = null;

	private ButtonItem buttonItem9;

	private ImageList imageList1;

	private RibbonBar ribbonBar1;

	private ButtonItem biNuevo;

	private ButtonItem biEditar;

	private ButtonItem biEliminar;

	private ButtonItem btnConsultar;

	private ButtonItem biActualizar;

	private ButtonItem buttonItem7;

	private ButtonItem biImprimir;

	private ButtonItem buttonItem1;

	private ButtonItem buttonItem2;

	private ButtonItem buttonItem3;

	private DataGridView dgvDetalle;

	private DataGridViewTextBoxColumn codMovimientos;

	private DataGridViewTextBoxColumn codBanco;

	private DataGridViewTextBoxColumn banco;

	private DataGridViewTextBoxColumn codCtaCorriente;

	private DataGridViewTextBoxColumn cuentacorriente;

	private DataGridViewTextBoxColumn numTransaccion;

	private DataGridViewTextBoxColumn fechaMov;

	private DataGridViewTextBoxColumn transaccion;

	private DataGridViewTextBoxColumn desctipomovimiento;

	private DataGridViewTextBoxColumn tipomovimiento;

	private DataGridViewTextBoxColumn haber;

	private DataGridViewTextBoxColumn debe;

	private DataGridViewTextBoxColumn tipoCV;

	private DataGridViewTextBoxColumn saldo;

	private DataGridViewTextBoxColumn tipoCC;

	private DataGridViewTextBoxColumn codnotasalida;

	private DataGridViewTextBoxColumn codnotaingreso;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn direccion;

	private DataGridViewTextBoxColumn dni;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn activo;

	private DataGridViewTextBoxColumn descripcionactivo;

	public frmMovimientos()
	{
		InitializeComponent();
	}

	private void CargaMovimientos()
	{
		dgvDetalle.DataSource = data;
		data.DataSource = admcta.ListaMovimientos(frmLogin.iCodAlmacen);
		data.Filter = string.Empty;
		dgvDetalle.ClearSelection();
		revisadesactivos();
	}

	private void revisadesactivos()
	{
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			string compara = row.Cells[activo.Name].Value.ToString();
			if (compara != "")
			{
				if (Convert.ToInt32(row.Cells[activo.Name].Value) == 2)
				{
					row.DefaultCellStyle.BackColor = Color.PeachPuff;
				}
				else if (Convert.ToInt32(row.Cells[activo.Name].Value) == 1)
				{
					row.DefaultCellStyle.BackColor = Color.White;
				}
			}
		}
	}

	private void DarFormato()
	{
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToDecimal(row.Cells[debe.Name].Value) > 0m)
			{
				row.Cells[debe.Name].Style.ForeColor = Color.Red;
			}
			else if (Convert.ToDecimal(row.Cells[haber.Name].Value) > 0m)
			{
				row.Cells[haber.Name].Style.ForeColor = Color.Blue;
			}
			else
			{
				row.DefaultCellStyle.ForeColor = Color.Black;
			}
		}
	}

	private void frmMovimientos_Load(object sender, EventArgs e)
	{
		CargaMovimientos();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		frmMovimientosControl frm = new frmMovimientosControl();
		frm.Proceso = 1;
		frm.ShowDialog();
		CargaMovimientos();
		DarFormato();
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		cta.CodMovi = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
		frmMovimientosControl frm = new frmMovimientosControl();
		frm.Proceso = 3;
		frm.ShowDialog();
	}

	private void frmMovimientos_Shown(object sender, EventArgs e)
	{
		DarFormato();
	}

	private void dgvDetalle_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		DarFormato();
	}

	private void btnConfigurar_Click(object sender, EventArgs e)
	{
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvDetalle.CurrentRow.Index != -1 && cta.CodMovi != 0)
			{
				frmMovimientosControl frm = new frmMovimientosControl();
				frm.CodMovimiento = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
				frm.tipo = Convert.ToString(dgvDetalle.SelectedRows[0].Cells[transaccion.Name].Value);
				frm.Proceso = 3;
				frm.ShowDialog();
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		try
		{
			cta.CodMovi = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
			if (dgvDetalle.CurrentRow.Index != -1 && cta.CodMovi != 0)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "CONTROL DE FLUJO DE CAJA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No && admcta.DeleteMov(cta.CodMovi, frmLogin.iCodAlmacen))
				{
					CargaMovimientos();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "btnEliminar_Click- frmMovimientos");
		}
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		CargaMovimientos();
	}

	private void buttonItem1_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		frmMovimientosControl frm = new frmMovimientosControl();
		frm.CodMovimiento = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
		frm.tipo = Convert.ToString(dgvDetalle.SelectedRows[0].Cells[transaccion.Name].Value);
		frm.Proceso = 3;
		frm.ShowDialog();
	}

	private void biImprimir_Click(object sender, EventArgs e)
	{
		frmParamMovimientosBancarios frm = new frmParamMovimientosBancarios();
		frm.ShowDialog();
	}

	private void ribbonBar1_ItemClick(object sender, EventArgs e)
	{
	}

	private void buttonItem2_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0)
		{
			int codigo = Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codMovimientos.Name].Value);
			dgvDetalle.CurrentRow.DefaultCellStyle.BackColor = Color.White;
			admcta.activar(codigo);
		}
	}

	private void buttonItem3_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0)
		{
			int codigo = Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codMovimientos.Name].Value);
			dgvDetalle.CurrentRow.DefaultCellStyle.BackColor = Color.PeachPuff;
			admcta.desactivar(codigo);
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmMovimientos));
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.codMovimientos = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.banco = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codCtaCorriente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cuentacorriente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numTransaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaMov = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.transaccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.desctipomovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipomovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.haber = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.debe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoCV = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipoCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codnotasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codnotaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.activo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcionactivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
		this.biNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.biEditar = new DevComponents.DotNetBar.ButtonItem();
		this.biEliminar = new DevComponents.DotNetBar.ButtonItem();
		this.btnConsultar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimir = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		base.SuspendLayout();
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.codMovimientos, this.codBanco, this.banco, this.codCtaCorriente, this.cuentacorriente, this.numTransaccion, this.fechaMov, this.transaccion, this.desctipomovimiento, this.tipomovimiento, this.haber, this.debe, this.tipoCV, this.saldo, this.tipoCC, this.codnotasalida, this.codnotaingreso, this.fecharegistro, this.nombre, this.direccion, this.dni, this.descripcion, this.activo, this.descripcionactivo);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(0, 59);
		this.dgvDetalle.MultiSelect = false;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1183, 382);
		this.dgvDetalle.TabIndex = 6;
		this.dgvDetalle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellDoubleClick);
		this.dgvDetalle.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvDetalle_ColumnHeaderMouseClick);
		this.codMovimientos.DataPropertyName = "codCtaCteMovimiento";
		this.codMovimientos.HeaderText = "codMovimientos";
		this.codMovimientos.Name = "codMovimientos";
		this.codMovimientos.ReadOnly = true;
		this.codBanco.DataPropertyName = "codBanco";
		this.codBanco.HeaderText = "codBanco";
		this.codBanco.Name = "codBanco";
		this.codBanco.ReadOnly = true;
		this.codBanco.Visible = false;
		this.banco.DataPropertyName = "NomBanco";
		this.banco.HeaderText = "Banco";
		this.banco.Name = "banco";
		this.banco.ReadOnly = true;
		this.banco.Width = 150;
		this.codCtaCorriente.DataPropertyName = "codCuentaCorriente";
		this.codCtaCorriente.HeaderText = "codCtaCorriente";
		this.codCtaCorriente.Name = "codCtaCorriente";
		this.codCtaCorriente.ReadOnly = true;
		this.codCtaCorriente.Visible = false;
		this.cuentacorriente.DataPropertyName = "cuentacorriente";
		this.cuentacorriente.HeaderText = "CuentaCorriente";
		this.cuentacorriente.Name = "cuentacorriente";
		this.cuentacorriente.ReadOnly = true;
		this.cuentacorriente.Width = 150;
		this.numTransaccion.DataPropertyName = "NumTransaccion";
		this.numTransaccion.HeaderText = "NumTransaccion";
		this.numTransaccion.Name = "numTransaccion";
		this.numTransaccion.ReadOnly = true;
		this.numTransaccion.Visible = false;
		this.fechaMov.DataPropertyName = "fechaMovimiento";
		this.fechaMov.HeaderText = "Fecha Movimiento";
		this.fechaMov.Name = "fechaMov";
		this.fechaMov.ReadOnly = true;
		this.transaccion.DataPropertyName = "Transaccion";
		this.transaccion.HeaderText = "Transaccion";
		this.transaccion.Name = "transaccion";
		this.transaccion.ReadOnly = true;
		this.desctipomovimiento.DataPropertyName = "desctipomovimiento";
		this.desctipomovimiento.HeaderText = "MOVIMIENTO";
		this.desctipomovimiento.Name = "desctipomovimiento";
		this.desctipomovimiento.ReadOnly = true;
		this.desctipomovimiento.Width = 200;
		this.tipomovimiento.HeaderText = "Tipo Movimiento";
		this.tipomovimiento.Name = "tipomovimiento";
		this.tipomovimiento.ReadOnly = true;
		this.tipomovimiento.Visible = false;
		this.tipomovimiento.Width = 200;
		this.haber.DataPropertyName = "ingreso";
		this.haber.HeaderText = "INGRESO (DEUDOR)";
		this.haber.Name = "haber";
		this.haber.ReadOnly = true;
		this.debe.DataPropertyName = "egreso";
		this.debe.HeaderText = "EGRESO (ACREEDOR)";
		this.debe.Name = "debe";
		this.debe.ReadOnly = true;
		this.tipoCV.DataPropertyName = "tcventa";
		this.tipoCV.HeaderText = "TipoCambio Venta";
		this.tipoCV.Name = "tipoCV";
		this.tipoCV.ReadOnly = true;
		this.tipoCV.Visible = false;
		this.saldo.DataPropertyName = "saldo";
		this.saldo.HeaderText = "SALDO";
		this.saldo.Name = "saldo";
		this.saldo.ReadOnly = true;
		this.saldo.Width = 80;
		this.tipoCC.DataPropertyName = "tccompra";
		this.tipoCC.HeaderText = "TipoCambio Compra";
		this.tipoCC.Name = "tipoCC";
		this.tipoCC.ReadOnly = true;
		this.tipoCC.Visible = false;
		this.codnotasalida.HeaderText = "codnotasalida";
		this.codnotasalida.Name = "codnotasalida";
		this.codnotasalida.ReadOnly = true;
		this.codnotasalida.Visible = false;
		this.codnotaingreso.HeaderText = "codnotaingreso";
		this.codnotaingreso.Name = "codnotaingreso";
		this.codnotaingreso.ReadOnly = true;
		this.codnotaingreso.Visible = false;
		this.fecharegistro.HeaderText = "fecharegistro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.nombre.DataPropertyName = "nombre";
		this.nombre.HeaderText = "NOMBRE";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Width = 150;
		this.direccion.DataPropertyName = "direccion";
		this.direccion.HeaderText = "DIRECCION";
		this.direccion.Name = "direccion";
		this.direccion.ReadOnly = true;
		this.direccion.Visible = false;
		this.direccion.Width = 150;
		this.dni.DataPropertyName = "dni";
		this.dni.HeaderText = "DNI";
		this.dni.Name = "dni";
		this.dni.ReadOnly = true;
		this.dni.Visible = false;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 200;
		this.activo.DataPropertyName = "activo";
		this.activo.HeaderText = "activo";
		this.activo.Name = "activo";
		this.activo.ReadOnly = true;
		this.activo.Visible = false;
		this.descripcionactivo.DataPropertyName = "descripcionactivo";
		this.descripcionactivo.HeaderText = "Desc. Pago";
		this.descripcionactivo.Name = "descripcionactivo";
		this.descripcionactivo.ReadOnly = true;
		this.buttonItem9.ImageIndex = 18;
		this.buttonItem9.Name = "buttonItem9";
		this.buttonItem9.SubItemsExpandWidth = 14;
		this.buttonItem9.Text = "Salir";
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
		this.imageList1.Images.SetKeyName(19, "icon_check.png");
		this.imageList1.Images.SetKeyName(20, "Symbol-Delete.png");
		this.ribbonBar1.AutoOverflowEnabled = true;
		this.ribbonBar1.BackColor = System.Drawing.SystemColors.Control;
		this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.ContainerControlProcessDialogKey = true;
		this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar1.DragDropSupport = true;
		this.ribbonBar1.Images = this.imageList1;
		this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[10] { this.biNuevo, this.biEditar, this.biEliminar, this.btnConsultar, this.biActualizar, this.buttonItem7, this.biImprimir, this.buttonItem1, this.buttonItem2, this.buttonItem3 });
		this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar1.Name = "ribbonBar1";
		this.ribbonBar1.Size = new System.Drawing.Size(1183, 59);
		this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar1.TabIndex = 7;
		this.ribbonBar1.Text = "ribbonBar1";
		this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.ribbonBar1.TitleVisible = false;
		this.ribbonBar1.ItemClick += new System.EventHandler(ribbonBar1_ItemClick);
		this.biNuevo.ImageIndex = 4;
		this.biNuevo.ImagePaddingHorizontal = 10;
		this.biNuevo.ImagePaddingVertical = 15;
		this.biNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNuevo.Name = "biNuevo";
		this.biNuevo.SubItemsExpandWidth = 14;
		this.biNuevo.Text = "Nuevo";
		this.biNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.biEditar.ImageIndex = 3;
		this.biEditar.ImagePaddingHorizontal = 10;
		this.biEditar.ImagePaddingVertical = 15;
		this.biEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEditar.Name = "biEditar";
		this.biEditar.SubItemsExpandWidth = 14;
		this.biEditar.Text = "Consultar";
		this.biEditar.Visible = false;
		this.biEditar.Click += new System.EventHandler(btnEditar_Click);
		this.biEliminar.ImageIndex = 5;
		this.biEliminar.ImagePaddingHorizontal = 10;
		this.biEliminar.ImagePaddingVertical = 15;
		this.biEliminar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biEliminar.Name = "biEliminar";
		this.biEliminar.SubItemsExpandWidth = 14;
		this.biEliminar.Text = "Eliminar";
		this.biEliminar.Visible = false;
		this.biEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnConsultar.Enabled = false;
		this.btnConsultar.ImageIndex = 17;
		this.btnConsultar.ImagePaddingVertical = 4;
		this.btnConsultar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.SubItemsExpandWidth = 14;
		this.btnConsultar.Text = "Consultar";
		this.btnConsultar.Visible = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePaddingHorizontal = 10;
		this.biActualizar.ImagePaddingVertical = 15;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biActualizar.Click += new System.EventHandler(btnActualizar_Click);
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
		this.biImprimir.Click += new System.EventHandler(biImprimir_Click);
		this.buttonItem1.ImageIndex = 18;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Salir";
		this.buttonItem1.Click += new System.EventHandler(buttonItem1_Click);
		this.buttonItem2.ImageIndex = 19;
		this.buttonItem2.ImagePaddingHorizontal = 10;
		this.buttonItem2.ImagePaddingVertical = 15;
		this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem2.Name = "buttonItem2";
		this.buttonItem2.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.buttonItem2.SubItemsExpandWidth = 14;
		this.buttonItem2.Text = "Activar";
		this.buttonItem2.Click += new System.EventHandler(buttonItem2_Click);
		this.buttonItem3.ImageIndex = 20;
		this.buttonItem3.ImagePaddingHorizontal = 10;
		this.buttonItem3.ImagePaddingVertical = 15;
		this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem3.Name = "buttonItem3";
		this.buttonItem3.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.F5);
		this.buttonItem3.SubItemsExpandWidth = 14;
		this.buttonItem3.Text = "Desactivar";
		this.buttonItem3.Click += new System.EventHandler(buttonItem3_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(1183, 441);
		base.Controls.Add(this.dgvDetalle);
		base.Controls.Add(this.ribbonBar1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmMovimientos";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmMovimientos";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmMovimientos_Load);
		base.Shown += new System.EventHandler(frmMovimientos_Shown);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		base.ResumeLayout(false);
	}
}
