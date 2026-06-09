using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmPuntoLlegada : RadForm
{
	public clsPuntoLlegada puntollegada = new clsPuntoLlegada();

	private clsAdmPuntoLlegada Admpunto = new clsAdmPuntoLlegada();

	private frmGuiaFacturacion frm = new frmGuiaFacturacion();

	private DataTable puntosllegada = new DataTable();

	private clsAdmPuntoLlegada AdmPunto = new clsAdmPuntoLlegada();

	private IContainer components = null;

	private RadTextBox txtdireccion;

	private RadButton btnguardar;

	private RadLabel radLabel1;

	private RadButton radButton1;

	private RadGridView rgvpuntos;

	public frmPuntoLlegada()
	{
		InitializeComponent();
	}

	private void btnguardar_Click(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(txtdireccion.Text))
		{
			MessageBox.Show("Error al guardar Dirreción", "Punto Llegada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtdireccion.Focus();
			return;
		}
		puntollegada.direccion = txtdireccion.Text.Trim();
		puntollegada.estado = true;
		puntollegada.fecharegistro = DateTime.Now;
		puntollegada.codusuario = frmLogin.iCodUser;
		if (Admpunto.insert(puntollegada))
		{
			MessageBox.Show("Datos Registrados", "Punto Llegada", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
		else
		{
			MessageBox.Show("Error en el Registro", "Punto Llegada", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void radButton1_Click(object sender, EventArgs e)
	{
		DialogResult dlgResult = MessageBox.Show("¿Esta seguro de Cerrar?", "Punto Llegada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.Yes)
		{
			Close();
		}
	}

	private void frmPuntoLlegada_Load(object sender, EventArgs e)
	{
		puntosllegada = AdmPunto.ListaPuntos();
		rgvpuntos.DataSource = puntosllegada;
	}

	private void rgvpuntos_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
		{
			GridViewRowInfo selectedRow = e.Row;
			puntollegada.direccion = Convert.ToString(selectedRow.Cells["direccion"].Value);
			Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPuntoLlegada));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.txtdireccion = new Telerik.WinControls.UI.RadTextBox();
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.radButton1 = new Telerik.WinControls.UI.RadButton();
		this.rgvpuntos = new Telerik.WinControls.UI.RadGridView();
		((System.ComponentModel.ISupportInitialize)this.txtdireccion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvpuntos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvpuntos.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.txtdireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtdireccion.Location = new System.Drawing.Point(4, 22);
		this.txtdireccion.Name = "txtdireccion";
		this.txtdireccion.Size = new System.Drawing.Size(406, 20);
		this.txtdireccion.TabIndex = 0;
		this.txtdireccion.ThemeName = "Material";
		this.btnguardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnguardar.Location = new System.Drawing.Point(416, 6);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(118, 36);
		this.btnguardar.TabIndex = 1;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnguardar.ThemeName = "Fluent";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		this.radLabel1.Location = new System.Drawing.Point(4, -2);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(55, 18);
		this.radLabel1.TabIndex = 2;
		this.radLabel1.Text = "Dirección:";
		this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.radLabel1.ThemeName = "Material";
		this.radButton1.Image = (System.Drawing.Image)resources.GetObject("radButton1.Image");
		this.radButton1.Location = new System.Drawing.Point(448, 331);
		this.radButton1.Name = "radButton1";
		this.radButton1.Size = new System.Drawing.Size(92, 36);
		this.radButton1.TabIndex = 2;
		this.radButton1.Text = "Salir";
		this.radButton1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.radButton1.ThemeName = "Fluent";
		this.radButton1.Click += new System.EventHandler(radButton1_Click);
		this.rgvpuntos.BackColor = System.Drawing.Color.Snow;
		this.rgvpuntos.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvpuntos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.rgvpuntos.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvpuntos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvpuntos.Location = new System.Drawing.Point(4, 63);
		this.rgvpuntos.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "codpuntollegada";
		gridViewTextBoxColumn1.HeaderText = "#";
		gridViewTextBoxColumn1.Name = "codpuntollegada";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.FieldName = "direccion";
		gridViewTextBoxColumn2.HeaderText = "Direccion";
		gridViewTextBoxColumn2.Name = "direccion";
		gridViewTextBoxColumn2.Width = 350;
		this.rgvpuntos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2);
		this.rgvpuntos.MasterTemplate.EnableFiltering = true;
		this.rgvpuntos.MasterTemplate.EnableGrouping = false;
		this.rgvpuntos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvpuntos.Name = "rgvpuntos";
		this.rgvpuntos.ReadOnly = true;
		this.rgvpuntos.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvpuntos.ShowGroupPanel = false;
		this.rgvpuntos.Size = new System.Drawing.Size(530, 259);
		this.rgvpuntos.TabIndex = 3;
		this.rgvpuntos.ThemeName = "TelerikMetroTouch";
		this.rgvpuntos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvpuntos_CellDoubleClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(546, 371);
		base.Controls.Add(this.rgvpuntos);
		base.Controls.Add(this.radButton1);
		base.Controls.Add(this.radLabel1);
		base.Controls.Add(this.btnguardar);
		base.Controls.Add(this.txtdireccion);
		base.MaximizeBox = false;
		base.Name = "frmPuntoLlegada";
		base.RootElement.ApplyShapeToControl = true;
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Puntos de Llegada";
		base.ThemeName = "Fluent";
		base.Load += new System.EventHandler(frmPuntoLlegada_Load);
		((System.ComponentModel.ISupportInitialize)this.txtdireccion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvpuntos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvpuntos).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
