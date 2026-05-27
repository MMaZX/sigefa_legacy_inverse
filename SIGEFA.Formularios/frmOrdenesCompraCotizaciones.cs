using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmOrdenesCompraCotizaciones : RadForm
{
	public static BindingSource data = new BindingSource();

	private clsAdmOrdenCompraCotizacion AdmOrdenC = new clsAdmOrdenCompraCotizacion();

	private clsOrdenCompraCotizacion OrdenCompraCotizacion = new clsOrdenCompraCotizacion();

	private IContainer components = null;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private RadGroupBox radGroupBox1;

	private RadGroupBox radGroupBox2;

	private RadGridView rgvDatos;

	private RadButton btnGeneraOV;

	private RadButton btnmostrar;

	private Label label3;

	private Label label2;

	private Label label1;

	private RadDateTimePicker dtfin;

	private RadDateTimePicker dtinicio;

	private RadButton btnsalir;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private RadButton btnactualizar;

	public frmOrdenesCompraCotizaciones()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmOrdenesCompraCotizaciones_Load(object sender, EventArgs e)
	{
		dtinicio.Value = DateTime.Now.Date;
		dtfin.Value = DateTime.Now.Date;
		consultar();
	}

	private void consultar()
	{
		rgvDatos.DataSource = data;
		data.DataSource = AdmOrdenC.ListaOrdenesCompraCotizacionesxVigente(frmLogin.iCodAlmacen, dtinicio.Value, dtfin.Value);
		data.Filter = string.Empty;
		rgvDatos.ClearSelection();
	}

	private void btnmostrar_Click(object sender, EventArgs e)
	{
		if (rgvDatos.Rows.Count >= 1 && rgvDatos.CurrentRow != null)
		{
			frmOrdenCompraCotizacion form = new frmOrdenCompraCotizacion();
			form.MdiParent = base.MdiParent;
			form.CodOrdenCompraVar = OrdenCompraCotizacion.codOrdenCompra;
			form.Proceso = 2;
			form.Show();
		}
	}

	private void rgvDatos_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (rgvDatos.Rows.Count >= 1 && e.RowIndex != -1)
			{
				OrdenCompraCotizacion.codOrdenCompra = Convert.ToInt32(e.Row.Cells["codordencompra"].Value.ToString());
				OrdenCompraCotizacion.total = Convert.ToDecimal(e.Row.Cells["total"].Value);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnGeneraOV_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvDatos.CurrentRow != null)
			{
				if (Application.OpenForms["frmVenta2019"] != null)
				{
					Application.OpenForms["frmVenta2019"].Close();
					return;
				}
				frmVenta2019 form = new frmVenta2019();
				form.MdiParent = base.MdiParent;
				KeyPressEventArgs ee = new KeyPressEventArgs('\r');
				form.Show();
				form.btnInicioOV_Click(sender, ee);
				form.chbordencompra.Checked = true;
				form.txtnumeroordencompra.Text = OrdenCompraCotizacion.codOrdenCompra.ToString();
				form.txtmontoordencompra.Text = OrdenCompraCotizacion.total.ToString();
				form.txtnumeroordencompra_KeyPress(OrdenCompraCotizacion.codOrdenCompra.ToString(), ee);
				form.txtcodCotizacion.Visible = true;
				form.lblcotizacion.Visible = true;
				form.generadoDeCotizacion = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnactualizar_Click(object sender, EventArgs e)
	{
		consultar();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmOrdenesCompraCotizaciones));
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
		this.rgvDatos = new Telerik.WinControls.UI.RadGridView();
		this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
		this.btnactualizar = new Telerik.WinControls.UI.RadButton();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.dtfin = new Telerik.WinControls.UI.RadDateTimePicker();
		this.dtinicio = new Telerik.WinControls.UI.RadDateTimePicker();
		this.btnGeneraOV = new Telerik.WinControls.UI.RadButton();
		this.btnmostrar = new Telerik.WinControls.UI.RadButton();
		this.btnsalir = new Telerik.WinControls.UI.RadButton();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox1).BeginInit();
		this.radGroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDatos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDatos.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox2).BeginInit();
		this.radGroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnactualizar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtfin).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtinicio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnGeneraOV).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnmostrar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.radGroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.radGroupBox1.Controls.Add(this.rgvDatos);
		this.radGroupBox1.HeaderText = "Ordenes Compra";
		this.radGroupBox1.Location = new System.Drawing.Point(7, 67);
		this.radGroupBox1.Name = "radGroupBox1";
		this.radGroupBox1.Size = new System.Drawing.Size(1369, 357);
		this.radGroupBox1.TabIndex = 0;
		this.radGroupBox1.Text = "Ordenes Compra";
		this.radGroupBox1.ThemeName = "TelerikMetroTouch";
		this.rgvDatos.BackColor = System.Drawing.Color.White;
		this.rgvDatos.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDatos.Font = new System.Drawing.Font("Segoe UI", 8.25f);
		this.rgvDatos.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvDatos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvDatos.Location = new System.Drawing.Point(2, 18);
		this.rgvDatos.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "item";
		gridViewTextBoxColumn1.HeaderText = "N°";
		gridViewTextBoxColumn1.Name = "item";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.FieldName = "fecharegistro";
		gridViewTextBoxColumn2.HeaderText = "FECHA OC";
		gridViewTextBoxColumn2.Name = "fecharegistro";
		gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "almacen";
		gridViewTextBoxColumn3.HeaderText = "ALMACÉN";
		gridViewTextBoxColumn3.Name = "almacen";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 150;
		gridViewTextBoxColumn4.FieldName = "num_occliente";
		gridViewTextBoxColumn4.HeaderText = "N° OC DEL CLIENTE";
		gridViewTextBoxColumn4.Name = "num_occliente";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 100;
		gridViewTextBoxColumn5.FieldName = "cliente";
		gridViewTextBoxColumn5.HeaderText = "CLIENTE";
		gridViewTextBoxColumn5.Name = "cliente";
		gridViewTextBoxColumn5.Width = 300;
		gridViewTextBoxColumn6.FieldName = "fcotizacion";
		gridViewTextBoxColumn6.HeaderText = "FECHA DE COT";
		gridViewTextBoxColumn6.Name = "fcotizacion";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 150;
		gridViewTextBoxColumn7.FieldName = "NumeroCotizacion";
		gridViewTextBoxColumn7.HeaderText = "N° COT - FINAL";
		gridViewTextBoxColumn7.Name = "NumeroCotizacion";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 150;
		gridViewTextBoxColumn8.FieldName = "fcomprobante";
		gridViewTextBoxColumn8.HeaderText = "FECHA COMPROBANTE";
		gridViewTextBoxColumn8.Name = "fcomprobante";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 150;
		gridViewTextBoxColumn9.FieldName = "num_comprobante";
		gridViewTextBoxColumn9.HeaderText = "N° COMPROBANTE";
		gridViewTextBoxColumn9.Name = "num_comprobante";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn9.Width = 150;
		gridViewTextBoxColumn10.FieldName = "num_despacho";
		gridViewTextBoxColumn10.HeaderText = "N° DESPACHO";
		gridViewTextBoxColumn10.Name = "num_despacho";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 150;
		gridViewTextBoxColumn11.FieldName = "fechaguia";
		gridViewTextBoxColumn11.HeaderText = "FECHA DE GUÍA DE REMISIÓN";
		gridViewTextBoxColumn11.Name = "fechaguia";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 150;
		gridViewTextBoxColumn12.FieldName = "numguia";
		gridViewTextBoxColumn12.HeaderText = "N° GR";
		gridViewTextBoxColumn12.Name = "numguia";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 100;
		gridViewTextBoxColumn13.FieldName = "subtotala";
		gridViewTextBoxColumn13.HeaderText = "SUBTOTAL ATENDIDO";
		gridViewTextBoxColumn13.Name = "subtotala";
		gridViewTextBoxColumn13.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn13.Width = 160;
		gridViewTextBoxColumn14.FieldName = "subtotalp";
		gridViewTextBoxColumn14.HeaderText = "SUBTOTAL PENDIENTE";
		gridViewTextBoxColumn14.Name = "subtotalp";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 160;
		gridViewTextBoxColumn15.FieldName = "usuario";
		gridViewTextBoxColumn15.HeaderText = "VENDEDOR";
		gridViewTextBoxColumn15.Name = "usuario";
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 150;
		gridViewTextBoxColumn16.FieldName = "total";
		gridViewTextBoxColumn16.HeaderText = "Total Orden";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "total";
		gridViewTextBoxColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn16.Width = 100;
		gridViewTextBoxColumn17.FieldName = "fdespacho";
		gridViewTextBoxColumn17.HeaderText = "F.Despacho";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "fdespacho";
		gridViewTextBoxColumn17.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn17.Width = 100;
		gridViewTextBoxColumn18.FieldName = "estado";
		gridViewTextBoxColumn18.HeaderText = "Estado";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "estado";
		gridViewTextBoxColumn18.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.Width = 200;
		gridViewTextBoxColumn19.FieldName = "codordencompra";
		gridViewTextBoxColumn19.HeaderText = "codordencompra";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "codordencompra";
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 150;
		this.rgvDatos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19);
		this.rgvDatos.MasterTemplate.EnableFiltering = true;
		this.rgvDatos.MasterTemplate.EnableGrouping = false;
		this.rgvDatos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDatos.Name = "rgvDatos";
		this.rgvDatos.ReadOnly = true;
		this.rgvDatos.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvDatos.Size = new System.Drawing.Size(1365, 337);
		this.rgvDatos.TabIndex = 0;
		this.rgvDatos.ThemeName = "TelerikMetroTouch";
		this.rgvDatos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDatos_CellClick);
		this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.radGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.radGroupBox2.Controls.Add(this.btnactualizar);
		this.radGroupBox2.Controls.Add(this.label3);
		this.radGroupBox2.Controls.Add(this.label2);
		this.radGroupBox2.Controls.Add(this.label1);
		this.radGroupBox2.Controls.Add(this.dtfin);
		this.radGroupBox2.Controls.Add(this.dtinicio);
		this.radGroupBox2.Controls.Add(this.btnGeneraOV);
		this.radGroupBox2.Controls.Add(this.btnmostrar);
		this.radGroupBox2.Controls.Add(this.btnsalir);
		this.radGroupBox2.HeaderText = "+";
		this.radGroupBox2.Location = new System.Drawing.Point(0, 2);
		this.radGroupBox2.Name = "radGroupBox2";
		this.radGroupBox2.Size = new System.Drawing.Size(1376, 59);
		this.radGroupBox2.TabIndex = 1;
		this.radGroupBox2.Text = "+";
		this.radGroupBox2.ThemeName = "TelerikMetroTouch";
		this.btnactualizar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnactualizar.Image = (System.Drawing.Image)resources.GetObject("btnactualizar.Image");
		this.btnactualizar.Location = new System.Drawing.Point(969, 10);
		this.btnactualizar.Name = "btnactualizar";
		this.btnactualizar.Size = new System.Drawing.Size(104, 34);
		this.btnactualizar.TabIndex = 15;
		this.btnactualizar.Text = "Actualizar";
		this.btnactualizar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnactualizar.ThemeName = "TelerikMetroTouch";
		this.btnactualizar.Click += new System.EventHandler(btnactualizar_Click);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(477, 23);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(40, 13);
		this.label3.TabIndex = 14;
		this.label3.Text = "Estado";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(277, 27);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(42, 16);
		this.label2.TabIndex = 13;
		this.label2.Text = "F.Fin";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(65, 25);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(58, 16);
		this.label1.TabIndex = 12;
		this.label1.Text = "F.Inicio";
		this.dtfin.CalendarSize = new System.Drawing.Size(300, 300);
		this.dtfin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfin.Location = new System.Drawing.Point(325, 16);
		this.dtfin.Name = "dtfin";
		this.dtfin.Size = new System.Drawing.Size(120, 32);
		this.dtfin.TabIndex = 10;
		this.dtfin.TabStop = false;
		this.dtfin.Text = "29/09/2022";
		this.dtfin.ThemeName = "TelerikMetroTouch";
		this.dtfin.Value = new System.DateTime(2022, 9, 29, 7, 18, 19, 513);
		this.dtinicio.CalendarSize = new System.Drawing.Size(300, 300);
		this.dtinicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtinicio.Location = new System.Drawing.Point(129, 16);
		this.dtinicio.Name = "dtinicio";
		this.dtinicio.Size = new System.Drawing.Size(119, 32);
		this.dtinicio.TabIndex = 9;
		this.dtinicio.TabStop = false;
		this.dtinicio.Text = "29/09/2022";
		this.dtinicio.ThemeName = "TelerikMetroTouch";
		this.dtinicio.Value = new System.DateTime(2022, 9, 29, 7, 18, 19, 513);
		this.btnGeneraOV.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnGeneraOV.Image = (System.Drawing.Image)resources.GetObject("btnGeneraOV.Image");
		this.btnGeneraOV.Location = new System.Drawing.Point(1079, 10);
		this.btnGeneraOV.Name = "btnGeneraOV";
		this.btnGeneraOV.Size = new System.Drawing.Size(121, 34);
		this.btnGeneraOV.TabIndex = 8;
		this.btnGeneraOV.Text = "Orden Venta";
		this.btnGeneraOV.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGeneraOV.ThemeName = "TelerikMetroTouch";
		this.btnGeneraOV.Click += new System.EventHandler(btnGeneraOV_Click);
		this.btnmostrar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnmostrar.Image = (System.Drawing.Image)resources.GetObject("btnmostrar.Image");
		this.btnmostrar.Location = new System.Drawing.Point(1206, 10);
		this.btnmostrar.Name = "btnmostrar";
		this.btnmostrar.Size = new System.Drawing.Size(116, 34);
		this.btnmostrar.TabIndex = 7;
		this.btnmostrar.Text = "Ver Regsitro";
		this.btnmostrar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnmostrar.ThemeName = "TelerikMetroTouch";
		this.btnmostrar.Click += new System.EventHandler(btnmostrar_Click);
		this.btnsalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnsalir.Image = (System.Drawing.Image)resources.GetObject("btnsalir.Image");
		this.btnsalir.Location = new System.Drawing.Point(1334, 10);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(35, 34);
		this.btnsalir.TabIndex = 6;
		this.btnsalir.ThemeName = "TelerikMetroTouch";
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1388, 436);
		base.Controls.Add(this.radGroupBox2);
		base.Controls.Add(this.radGroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MinimizeBox = false;
		base.Name = "frmOrdenesCompraCotizaciones";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "frmOrdenCompraCotizaciones";
		base.ThemeName = "TelerikMetroBlue";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmOrdenesCompraCotizaciones_Load);
		((System.ComponentModel.ISupportInitialize)this.radGroupBox1).EndInit();
		this.radGroupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDatos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDatos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox2).EndInit();
		this.radGroupBox2.ResumeLayout(false);
		this.radGroupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnactualizar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtfin).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtinicio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnGeneraOV).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnmostrar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
