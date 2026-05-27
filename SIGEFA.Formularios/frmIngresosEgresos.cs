using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Alertas;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SISTEMA_BUNIFU.Alertas;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;

namespace SIGEFA.Formularios;

public class frmIngresosEgresos : RadForm
{
	public static BindingSource data = new BindingSource();

	private int Accion = 1;

	private int codCategoria = 0;

	private AdmIngresosEgresos admIngresos = new AdmIngresosEgresos();

	private clsIngresoEgreso IngresoEgreso;

	private Success sucess;

	private Errors errors;

	private Info info;

	private IContainer components = null;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private TableLayoutPanel tableLayoutPanel1;

	private RadGridView dg_ingresos_egresos;

	private Panel panel1;

	private RadButton btnCancelar;

	private RadButton btnGuardar;

	private RadLabel radLabel3;

	private RadToggleSwitch optEstado;

	private RadLabel radLabel2;

	private RadDropDownList cmbCategoria;

	private RadLabel radLabel1;

	private RadTextBoxControl txtDescripcion;

	private RadLabel radLabel4;

	public frmIngresosEgresos()
	{
		InitializeComponent();
	}

	private void frmIngresosEgresos_Load(object sender, EventArgs e)
	{
		cmbCategoria.SelectedIndex = 0;
		CargaIngresosEgresos();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			int categoria = ((Convert.ToInt32(cmbCategoria.SelectedIndex) == 0) ? 1 : 2);
			if (txtDescripcion.Text == "")
			{
				info = new Info("Ingrese Descripcióm");
				info.ShowDialog();
				txtDescripcion.Focus();
				return;
			}
			IngresoEgreso = new clsIngresoEgreso
			{
				Id = codCategoria,
				Tipo = Convert.ToInt32(categoria),
				Descripcion = txtDescripcion.Text,
				Estado = Convert.ToInt32(optEstado.Value)
			};
			if (Accion == 1 && IngresoEgreso != null)
			{
				if (admIngresos.insert(IngresoEgreso))
				{
					sucess = new Success("Categoría registrada correctamente!");
					sucess.ShowDialog();
				}
				else
				{
					errors = new Errors("Problemas al registrar categoría");
					errors.ShowDialog();
				}
			}
			else if (Accion == 2 && IngresoEgreso != null)
			{
				if (admIngresos.update(IngresoEgreso))
				{
					sucess = new Success("Categoría actualizada correctamente!");
					sucess.ShowDialog();
				}
				else
				{
					errors = new Errors("Problemas al actualizar categoría");
					errors.ShowDialog();
				}
			}
			CargaIngresosEgresos();
			Limpiar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaIngresosEgresos()
	{
		try
		{
			int categoria = ((Convert.ToInt32(cmbCategoria.SelectedIndex) == 0) ? 1 : 2);
			dg_ingresos_egresos.DataSource = data;
			data.DataSource = admIngresos.ListadoIngresosEgresos(categoria);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dg_ingresos_egresos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (dg_ingresos_egresos.Rows.Count >= 1 && e.RowIndex != -1)
		{
			codCategoria = Convert.ToInt32(e.Row.Cells["id"].Value);
			cmbCategoria.SelectedIndex = ((Convert.ToInt32(e.Row.Cells["tipo"].Value) != 1) ? 1 : 0);
			txtDescripcion.Text = e.Row.Cells["descripcion"].Value.ToString();
			Accion = 2;
		}
	}

	private void cmbCategoria_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
	{
		CargaIngresosEgresos();
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Limpiar();
	}

	private void Limpiar()
	{
		Accion = 1;
		txtDescripcion.Text = "";
		txtDescripcion.Focus();
		IngresoEgreso = new clsIngresoEgreso();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
		Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.dg_ingresos_egresos = new Telerik.WinControls.UI.RadGridView();
		this.panel1 = new System.Windows.Forms.Panel();
		this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
		this.optEstado = new Telerik.WinControls.UI.RadToggleSwitch();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		this.cmbCategoria = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.txtDescripcion = new Telerik.WinControls.UI.RadTextBoxControl();
		this.btnCancelar = new Telerik.WinControls.UI.RadButton();
		this.btnGuardar = new Telerik.WinControls.UI.RadButton();
		this.tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dg_ingresos_egresos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dg_ingresos_egresos.MasterTemplate).BeginInit();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.optEstado).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbCategoria).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescripcion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnCancelar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.tableLayoutPanel1.ColumnCount = 2;
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.61272f));
		this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.38728f));
		this.tableLayoutPanel1.Controls.Add(this.dg_ingresos_egresos, 0, 0);
		this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
		this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.tableLayoutPanel1.Name = "tableLayoutPanel1";
		this.tableLayoutPanel1.RowCount = 1;
		this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.tableLayoutPanel1.Size = new System.Drawing.Size(902, 372);
		this.tableLayoutPanel1.TabIndex = 0;
		this.dg_ingresos_egresos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dg_ingresos_egresos.EnableCustomFiltering = true;
		this.dg_ingresos_egresos.Location = new System.Drawing.Point(3, 3);
		this.dg_ingresos_egresos.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn1.FieldName = "id";
		gridViewTextBoxColumn1.HeaderText = "ID";
		gridViewTextBoxColumn1.Name = "id";
		gridViewTextBoxColumn2.FieldName = "tipo";
		gridViewTextBoxColumn2.HeaderText = "Tipo";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "tipo";
		gridViewTextBoxColumn3.FieldName = "categoria";
		gridViewTextBoxColumn3.HeaderText = "Categoría";
		gridViewTextBoxColumn3.Name = "categoria";
		gridViewTextBoxColumn3.Width = 200;
		gridViewTextBoxColumn4.FieldName = "descripcion";
		gridViewTextBoxColumn4.HeaderText = "Descripción";
		gridViewTextBoxColumn4.Name = "descripcion";
		gridViewTextBoxColumn4.Width = 200;
		this.dg_ingresos_egresos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4);
		this.dg_ingresos_egresos.MasterTemplate.EnableCustomFiltering = true;
		this.dg_ingresos_egresos.MasterTemplate.EnableFiltering = true;
		this.dg_ingresos_egresos.MasterTemplate.ShowRowHeaderColumn = false;
		this.dg_ingresos_egresos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dg_ingresos_egresos.Name = "dg_ingresos_egresos";
		this.dg_ingresos_egresos.ReadOnly = true;
		this.dg_ingresos_egresos.Size = new System.Drawing.Size(477, 366);
		this.dg_ingresos_egresos.TabIndex = 2;
		this.dg_ingresos_egresos.ThemeName = "TelerikMetroTouch";
		this.dg_ingresos_egresos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dg_ingresos_egresos_CellClick);
		this.panel1.Controls.Add(this.radLabel4);
		this.panel1.Controls.Add(this.btnCancelar);
		this.panel1.Controls.Add(this.btnGuardar);
		this.panel1.Controls.Add(this.radLabel3);
		this.panel1.Controls.Add(this.optEstado);
		this.panel1.Controls.Add(this.radLabel2);
		this.panel1.Controls.Add(this.cmbCategoria);
		this.panel1.Controls.Add(this.radLabel1);
		this.panel1.Controls.Add(this.txtDescripcion);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(486, 3);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(413, 366);
		this.panel1.TabIndex = 3;
		this.radLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel4.ForeColor = System.Drawing.SystemColors.ButtonShadow;
		this.radLabel4.Location = new System.Drawing.Point(9, 3);
		this.radLabel4.Name = "radLabel4";
		this.radLabel4.Size = new System.Drawing.Size(246, 25);
		this.radLabel4.TabIndex = 8;
		this.radLabel4.Text = "Categoría de Ingresos y Egresos";
		this.radLabel4.ThemeName = "TelerikMetroTouch";
		this.radLabel3.Location = new System.Drawing.Point(42, 219);
		this.radLabel3.Name = "radLabel3";
		this.radLabel3.Size = new System.Drawing.Size(55, 23);
		this.radLabel3.TabIndex = 5;
		this.radLabel3.Text = "Estado:";
		this.radLabel3.ThemeName = "TelerikMetroTouch";
		this.optEstado.Location = new System.Drawing.Point(112, 219);
		this.optEstado.Name = "optEstado";
		this.optEstado.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.optEstado.Size = new System.Drawing.Size(60, 30);
		this.optEstado.TabIndex = 4;
		this.optEstado.ThemeName = "TelerikMetroTouch";
		this.optEstado.ThumbTickness = 30;
		this.radLabel2.Location = new System.Drawing.Point(9, 107);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(88, 23);
		this.radLabel2.TabIndex = 3;
		this.radLabel2.Text = "Descripción:";
		this.radLabel2.ThemeName = "TelerikMetroTouch";
		radListDataItem1.Text = "Ingresos";
		radListDataItem2.Text = "Egresos";
		this.cmbCategoria.Items.Add(radListDataItem1);
		this.cmbCategoria.Items.Add(radListDataItem2);
		this.cmbCategoria.Location = new System.Drawing.Point(112, 57);
		this.cmbCategoria.Name = "cmbCategoria";
		this.cmbCategoria.Size = new System.Drawing.Size(292, 32);
		this.cmbCategoria.TabIndex = 2;
		this.cmbCategoria.ThemeName = "TelerikMetroTouch";
		this.cmbCategoria.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cmbCategoria_SelectedIndexChanged);
		this.radLabel1.Location = new System.Drawing.Point(22, 66);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(75, 23);
		this.radLabel1.TabIndex = 1;
		this.radLabel1.Text = "Categoría:";
		this.radLabel1.ThemeName = "TelerikMetroTouch";
		this.txtDescripcion.Location = new System.Drawing.Point(112, 107);
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(292, 99);
		this.txtDescripcion.TabIndex = 0;
		this.txtDescripcion.ThemeName = "TelerikMetroTouch";
		this.btnCancelar.Image = SIGEFA.Properties.Resources.broom_32px;
		this.btnCancelar.Location = new System.Drawing.Point(260, 292);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(131, 38);
		this.btnCancelar.TabIndex = 7;
		this.btnCancelar.Text = "Limpiar";
		this.btnCancelar.ThemeName = "TelerikMetroTouch";
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.btnGuardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnGuardar.Location = new System.Drawing.Point(69, 292);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(130, 38);
		this.btnGuardar.TabIndex = 6;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.ThemeName = "TelerikMetroTouch";
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(902, 372);
		base.Controls.Add(this.tableLayoutPanel1);
		base.Name = "frmIngresosEgresos";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Categoría Ingresos - Egresos";
		base.ThemeName = "TelerikMetroTouch";
		base.Load += new System.EventHandler(frmIngresosEgresos_Load);
		this.tableLayoutPanel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dg_ingresos_egresos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dg_ingresos_egresos).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.optEstado).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbCategoria).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescripcion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnCancelar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
