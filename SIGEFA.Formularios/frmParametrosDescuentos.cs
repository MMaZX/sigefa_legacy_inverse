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

namespace SIGEFA.Formularios;

public class frmParametrosDescuentos : RadForm
{
	public static BindingSource data = new BindingSource();

	private int Accion = 1;

	private int CodParametro = 0;

	private clsAdmParametroDescuento admPrametro = new clsAdmParametroDescuento();

	private clsParametroDescuento parametro;

	private Success sucess;

	private Errors errors;

	private Info info;

	private IContainer components = null;

	private GroupBox gbParametros;

	private GroupBox gbDatos;

	private RadTextBox txthasta;

	private RadGridView rgvparametros;

	private RadTextBox txtdesde;

	private RadButton btnnuevo;

	private RadLabel lblhasta;

	private RadLabel lbldesde;

	private RadButton btnGuardar;

	private RadButton btnsalir;

	private RadLabel radLabel1;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private RadLabel radLabel2;

	private RadTextBox txtvalor;

	public frmParametrosDescuentos()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtdesde.Text == "")
			{
				MessageBox.Show("Ingrese Valor", "Parametros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtdesde.Focus();
				return;
			}
			if (txthasta.Text == "")
			{
				MessageBox.Show("Ingrese Valor", "Parametros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtdesde.Focus();
				return;
			}
			if (txtvalor.Text == "")
			{
				MessageBox.Show("Ingrese Valor", "Parametros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtvalor.Focus();
				return;
			}
			parametro = new clsParametroDescuento
			{
				CodParametro = CodParametro,
				CodEmpresa = frmLogin.iCodEmpresa,
				Desde = txtdesde.Text,
				Hasta = txthasta.Text,
				Valor = Convert.ToDecimal(txtvalor.Text)
			};
			if (Accion == 1 && parametro != null)
			{
				if (admPrametro.insert(parametro))
				{
					sucess = new Success("Parámetro registrado correctamente!");
					sucess.ShowDialog();
				}
				else
				{
					errors = new Errors("Problemas al registrar Parámetro");
					errors.ShowDialog();
				}
			}
			else if (Accion == 2 && parametro != null)
			{
				if (admPrametro.update(parametro))
				{
					sucess = new Success("Parámetro actualizado correctamente!");
					sucess.ShowDialog();
				}
				else
				{
					errors = new Errors("Problemas al actualizar parámetro");
					errors.ShowDialog();
				}
			}
			CargaParametros();
			Limpiar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaParametros()
	{
		try
		{
			rgvparametros.DataSource = data;
			data.DataSource = admPrametro.ListadoParametros(frmLogin.iCodEmpresa);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void Limpiar()
	{
		Accion = 1;
		txtdesde.Text = "";
		txthasta.Text = "";
		txtvalor.Text = "";
		txtdesde.Focus();
		parametro = new clsParametroDescuento();
	}

	private void frmParametrosDescuentos_Load(object sender, EventArgs e)
	{
		CargaParametros();
	}

	private void rgvparametros_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvparametros.Rows.Count >= 1 && e.RowIndex != -1)
		{
			CodParametro = Convert.ToInt32(e.Row.Cells["codParametro"].Value.ToString());
			txtdesde.Text = e.Row.Cells["desde"].Value.ToString();
			txthasta.Text = e.Row.Cells["hasta"].Value.ToString();
			Accion = 2;
		}
	}

	private void btnnuevo_Click(object sender, EventArgs e)
	{
		Limpiar();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmParametrosDescuentos));
		this.gbParametros = new System.Windows.Forms.GroupBox();
		this.rgvparametros = new Telerik.WinControls.UI.RadGridView();
		this.gbDatos = new System.Windows.Forms.GroupBox();
		this.btnnuevo = new Telerik.WinControls.UI.RadButton();
		this.btnGuardar = new Telerik.WinControls.UI.RadButton();
		this.lblhasta = new Telerik.WinControls.UI.RadLabel();
		this.lbldesde = new Telerik.WinControls.UI.RadLabel();
		this.txtdesde = new Telerik.WinControls.UI.RadTextBox();
		this.txthasta = new Telerik.WinControls.UI.RadTextBox();
		this.btnsalir = new Telerik.WinControls.UI.RadButton();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		this.txtvalor = new Telerik.WinControls.UI.RadTextBox();
		this.gbParametros.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvparametros).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvparametros.MasterTemplate).BeginInit();
		this.gbDatos.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnnuevo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblhasta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lbldesde).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtdesde).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txthasta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.gbParametros.Controls.Add(this.rgvparametros);
		this.gbParametros.Location = new System.Drawing.Point(12, 36);
		this.gbParametros.Name = "gbParametros";
		this.gbParametros.Size = new System.Drawing.Size(486, 251);
		this.gbParametros.TabIndex = 0;
		this.gbParametros.TabStop = false;
		this.gbParametros.Text = "Parametros :";
		this.rgvparametros.BackColor = System.Drawing.SystemColors.Control;
		this.rgvparametros.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvparametros.Font = new System.Drawing.Font("Segoe UI", 8.25f);
		this.rgvparametros.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvparametros.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvparametros.Location = new System.Drawing.Point(6, 19);
		this.rgvparametros.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn1.FieldName = "codParametro";
		gridViewTextBoxColumn1.HeaderText = "Cod";
		gridViewTextBoxColumn1.Name = "codParametro";
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.FieldName = "desde";
		gridViewTextBoxColumn2.HeaderText = "Desde";
		gridViewTextBoxColumn2.Name = "desde";
		gridViewTextBoxColumn2.Width = 100;
		gridViewTextBoxColumn3.FieldName = "hasta";
		gridViewTextBoxColumn3.HeaderText = "Hasta";
		gridViewTextBoxColumn3.Name = "hasta";
		gridViewTextBoxColumn3.Width = 100;
		gridViewTextBoxColumn4.FieldName = "valor";
		gridViewTextBoxColumn4.HeaderText = "Valor";
		gridViewTextBoxColumn4.Name = "valor";
		gridViewTextBoxColumn4.Width = 100;
		this.rgvparametros.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4);
		this.rgvparametros.MasterTemplate.EnableFiltering = true;
		this.rgvparametros.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvparametros.Name = "rgvparametros";
		this.rgvparametros.ReadOnly = true;
		this.rgvparametros.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvparametros.RootElement.RippleAnimationColor = System.Drawing.Color.GreenYellow;
		this.rgvparametros.Size = new System.Drawing.Size(474, 216);
		this.rgvparametros.TabIndex = 7;
		this.rgvparametros.ThemeName = "TelerikMetroTouch";
		this.rgvparametros.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvparametros_CellDoubleClick);
		this.gbDatos.Controls.Add(this.radLabel2);
		this.gbDatos.Controls.Add(this.btnnuevo);
		this.gbDatos.Controls.Add(this.txtvalor);
		this.gbDatos.Controls.Add(this.btnGuardar);
		this.gbDatos.Controls.Add(this.lblhasta);
		this.gbDatos.Controls.Add(this.lbldesde);
		this.gbDatos.Controls.Add(this.txtdesde);
		this.gbDatos.Controls.Add(this.txthasta);
		this.gbDatos.Location = new System.Drawing.Point(504, 36);
		this.gbDatos.Name = "gbDatos";
		this.gbDatos.Size = new System.Drawing.Size(284, 251);
		this.gbDatos.TabIndex = 6;
		this.gbDatos.TabStop = false;
		this.gbDatos.Text = "Datos:";
		this.btnnuevo.Image = (System.Drawing.Image)resources.GetObject("btnnuevo.Image");
		this.btnnuevo.Location = new System.Drawing.Point(26, 167);
		this.btnnuevo.Name = "btnnuevo";
		this.btnnuevo.Size = new System.Drawing.Size(110, 34);
		this.btnnuevo.TabIndex = 4;
		this.btnnuevo.Text = "Nuevo";
		this.btnnuevo.ThemeName = "TelerikMetroTouch";
		this.btnnuevo.Click += new System.EventHandler(btnnuevo_Click);
		this.btnGuardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnGuardar.Location = new System.Drawing.Point(150, 167);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(118, 34);
		this.btnGuardar.TabIndex = 3;
		this.btnGuardar.Text = " Guardar";
		this.btnGuardar.ThemeName = "TelerikMetroTouch";
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.lblhasta.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblhasta.Location = new System.Drawing.Point(48, 87);
		this.lblhasta.Name = "lblhasta";
		this.lblhasta.Size = new System.Drawing.Size(40, 18);
		this.lblhasta.TabIndex = 5;
		this.lblhasta.Text = "Hasta:";
		this.lbldesde.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldesde.Location = new System.Drawing.Point(48, 36);
		this.lbldesde.Name = "lbldesde";
		this.lbldesde.Size = new System.Drawing.Size(45, 18);
		this.lbldesde.TabIndex = 4;
		this.lbldesde.Text = "Desde :";
		this.txtdesde.Location = new System.Drawing.Point(97, 34);
		this.txtdesde.Name = "txtdesde";
		this.txtdesde.Size = new System.Drawing.Size(147, 32);
		this.txtdesde.TabIndex = 1;
		this.txtdesde.ThemeName = "TelerikMetroTouch";
		this.txthasta.Location = new System.Drawing.Point(97, 81);
		this.txthasta.Name = "txthasta";
		this.txthasta.Size = new System.Drawing.Size(147, 32);
		this.txthasta.TabIndex = 2;
		this.txthasta.ThemeName = "TelerikMetroTouch";
		this.btnsalir.Image = (System.Drawing.Image)resources.GetObject("btnsalir.Image");
		this.btnsalir.Location = new System.Drawing.Point(753, 3);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(35, 34);
		this.btnsalir.TabIndex = 5;
		this.btnsalir.ThemeName = "TelerikMetroTouch";
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel1.Location = new System.Drawing.Point(12, 3);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(115, 18);
		this.radLabel1.TabIndex = 5;
		this.radLabel1.Text = "Registro Parametros";
		this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel2.Location = new System.Drawing.Point(48, 135);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(38, 18);
		this.radLabel2.TabIndex = 7;
		this.radLabel2.Text = "Valor:";
		this.txtvalor.Location = new System.Drawing.Point(97, 129);
		this.txtvalor.Name = "txtvalor";
		this.txtvalor.Size = new System.Drawing.Size(147, 32);
		this.txtvalor.TabIndex = 6;
		this.txtvalor.ThemeName = "TelerikMetroTouch";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.HighlightText;
		base.ClientSize = new System.Drawing.Size(794, 298);
		base.Controls.Add(this.radLabel1);
		base.Controls.Add(this.btnsalir);
		base.Controls.Add(this.gbDatos);
		base.Controls.Add(this.gbParametros);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmParametrosDescuentos";
		base.RootElement.ApplyShapeToControl = true;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmParametrosDescuentos";
		base.ThemeName = "TelerikMetroTouch";
		base.Load += new System.EventHandler(frmParametrosDescuentos_Load);
		this.gbParametros.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvparametros.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvparametros).EndInit();
		this.gbDatos.ResumeLayout(false);
		this.gbDatos.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnnuevo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnGuardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblhasta).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lbldesde).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtdesde).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txthasta).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalor).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
