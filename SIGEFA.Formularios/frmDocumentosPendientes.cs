using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmDocumentosPendientes : RadForm
{
	private string estado = "-1";

	private clsAdmRepositorio clsadmrepo = new clsAdmRepositorio();

	public List<clsRepositorio> lista_repositorio = new List<clsRepositorio>();

	private clsEmpresa empresa = null;

	private clsAdmEmpresa admemp = new clsAdmEmpresa();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsFacturaVenta fact = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private List<clsDetalleFacturaVenta> ListaDetalleVenta = new List<clsDetalleFacturaVenta>();

	private clsCliente client = new clsCliente();

	private clsAdmCliente admclient = new clsAdmCliente();

	private DataTable detalleTableVenta = new DataTable();

	public DateTime desde = Convert.ToDateTime(DateTime.Now.ToString());

	public DateTime hasta = Convert.ToDateTime(DateTime.Now.ToString());

	private IContainer components = null;

	private CustomInstaller customInstaller1;

	private RadGridView dg_documentos;

	private RadButton radButton1;

	public frmDocumentosPendientes()
	{
		InitializeComponent();
	}

	private void frmDocumentosPendientes_Load(object sender, EventArgs e)
	{
		listar_repositorio();
	}

	public void listar_repositorio()
	{
		try
		{
			if (lista_repositorio != null)
			{
				if (lista_repositorio.Count <= 0)
				{
					return;
				}
				dg_documentos.Rows.Clear();
				{
					foreach (clsRepositorio rep in lista_repositorio)
					{
						dg_documentos.Rows.Add(rep.Repoid, rep.Tipodoc, rep.Fechaemision, rep.documento, rep.Serie, rep.Correlativo, rep.Monto, rep.Estadosunat, rep.Mensajesunat, rep.Nombredoc, rep.Usuario, "", rep.CodEmpresa, rep.FechaActualiza);
					}
					return;
				}
			}
			dg_documentos.Rows.Clear();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void radButton1_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmVentas"] != null)
		{
		}
		frmEnvioSunat form = new frmEnvioSunat();
		form.dtpDesde.Value = desde;
		form.dtpHasta.Value = hasta;
		form.ShowDialog();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.customInstaller1 = new MySql.Data.MySqlClient.CustomInstaller();
		this.dg_documentos = new Telerik.WinControls.UI.RadGridView();
		this.radButton1 = new Telerik.WinControls.UI.RadButton();
		((System.ComponentModel.ISupportInitialize)this.dg_documentos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dg_documentos.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.dg_documentos.EnableCustomFiltering = true;
		this.dg_documentos.Location = new System.Drawing.Point(8, 4);
		gridViewTextBoxColumn1.HeaderText = "ID";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "column1";
		gridViewTextBoxColumn2.HeaderText = "T. DOC";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "column2";
		gridViewTextBoxColumn2.Width = 80;
		gridViewTextBoxColumn3.HeaderText = "F. EMISION";
		gridViewTextBoxColumn3.Name = "column3";
		gridViewTextBoxColumn3.Width = 80;
		gridViewTextBoxColumn4.FieldName = "documento";
		gridViewTextBoxColumn4.HeaderText = "DOCUMENTO";
		gridViewTextBoxColumn4.Name = "documento";
		gridViewTextBoxColumn4.Width = 90;
		gridViewTextBoxColumn5.HeaderText = "SERIE";
		gridViewTextBoxColumn5.Name = "column4";
		gridViewTextBoxColumn5.Width = 80;
		gridViewTextBoxColumn6.HeaderText = "CORRELATIVO";
		gridViewTextBoxColumn6.Name = "column5";
		gridViewTextBoxColumn6.Width = 90;
		gridViewTextBoxColumn7.HeaderText = "MONTO";
		gridViewTextBoxColumn7.Name = "column6";
		gridViewTextBoxColumn7.Width = 120;
		gridViewTextBoxColumn8.HeaderText = "EST. SUNAT";
		gridViewTextBoxColumn8.Name = "column7";
		gridViewTextBoxColumn8.Width = 150;
		gridViewTextBoxColumn9.HeaderText = "MENSAJE SUNAT";
		gridViewTextBoxColumn9.Name = "column8";
		gridViewTextBoxColumn9.Width = 180;
		gridViewTextBoxColumn10.HeaderText = "NOMBRE DOCUMENTO";
		gridViewTextBoxColumn10.Name = "column9";
		gridViewTextBoxColumn10.Width = 120;
		gridViewTextBoxColumn11.HeaderText = "CODEMPRESA";
		gridViewTextBoxColumn11.IsVisible = false;
		gridViewTextBoxColumn11.Name = "column10";
		gridViewTextBoxColumn12.HeaderText = "FECHA ENVIO";
		gridViewTextBoxColumn12.Name = "column11";
		gridViewTextBoxColumn12.Width = 90;
		this.dg_documentos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12);
		this.dg_documentos.MasterTemplate.EnableCustomFiltering = true;
		this.dg_documentos.MasterTemplate.EnableFiltering = true;
		this.dg_documentos.MasterTemplate.ShowRowHeaderColumn = false;
		this.dg_documentos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dg_documentos.Name = "dg_documentos";
		this.dg_documentos.Size = new System.Drawing.Size(1088, 471);
		this.dg_documentos.TabIndex = 0;
		this.dg_documentos.ThemeName = "ControlDefault";
		this.radButton1.Image = SIGEFA.Properties.Resources.searcg;
		this.radButton1.Location = new System.Drawing.Point(433, 484);
		this.radButton1.Name = "radButton1";
		this.radButton1.Size = new System.Drawing.Size(194, 36);
		this.radButton1.TabIndex = 1;
		this.radButton1.Text = "Ir a repositorio";
		this.radButton1.ThemeName = "Material";
		this.radButton1.Click += new System.EventHandler(radButton1_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1108, 530);
		base.Controls.Add(this.radButton1);
		base.Controls.Add(this.dg_documentos);
		base.Name = "frmDocumentosPendientes";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Documentos pendientes de envío";
		base.ThemeName = "TelerikMetroBlue";
		base.Load += new System.EventHandler(frmDocumentosPendientes_Load);
		((System.ComponentModel.ISupportInitialize)this.dg_documentos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dg_documentos).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
