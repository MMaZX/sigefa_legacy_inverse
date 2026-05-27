using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Alertas;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmListaNCreditosSinAplicar : RadForm
{
	public int CodCliente = 0;

	public clsNotaIngreso nota = new clsNotaIngreso();

	public clsNotaSalida notaS = new clsNotaSalida();

	public clsNotaCredito notaC = new clsNotaCredito();

	public clsNotaCredito notaCC = new clsNotaCredito();

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int VentComp = 0;

	public clsFacturaVenta venta = new clsFacturaVenta();

	private clsAdmParametro admParam = new clsAdmParametro();

	private clsParametro param = new clsParametro();

	private IContainer components = null;

	private Button btnAceptar;

	private ImageList imageList1;

	private RadGridView dgvDocumentos;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	public frmListaNCreditosSinAplicar()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		if (param.valor != "")
		{
			dgvDocumentos.DataSource = data;
			data.DataSource = AdmNota.CargaNotaCreditoSinAplicar(CodCliente, VentComp, venta.CodAlmacen, param.valor);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvDocumentos.ClearSelection();
		}
		else
		{
			Info info = new Info("Configurar fecha para el filtrado de Notas de Crédito \n Se van a listar por la fecha actual");
			info.ShowDialog();
			param.valor = DateTime.Now.ToShortDateString();
			CargaLista();
		}
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		if (dgvDocumentos.SelectedRows.Count > 0)
		{
			Close();
		}
	}

	private void frmListaNCreditosSinAplicar_Load(object sender, EventArgs e)
	{
		try
		{
			param = admParam.ObtenerParametro(4);
			if (param.valor != "")
			{
				CargaLista();
				return;
			}
			Info info = new Info("Configurar fecha para el filtrado de Notas de Crédito");
			info.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dg_notascreditos_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (dgvDocumentos.SelectedRows.Count > 0)
		{
			if (VentComp == 1)
			{
				notaC.CodNotaCredito = dgvDocumentos.Rows[e.RowIndex].Cells["codnota"].Value.ToString();
				notaC.DocumentoNotaCredito = dgvDocumentos.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
				notaC.Pendiente = Convert.ToDouble(dgvDocumentos.Rows[e.RowIndex].Cells["Total"].Value.ToString());
				nota.CodReferencia = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells["codReferencia"].Value.ToString());
				notaC.FechaRegistro = Convert.ToDateTime(dgvDocumentos.Rows[e.RowIndex].Cells["fecha"].Value.ToString());
				notaC.CodAlmacen = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells["codAlmacen"].Value.ToString());
			}
			else
			{
				notaS.CodNotaSalida = "";
				notaCC.CodNotaCredito = dgvDocumentos.Rows[e.RowIndex].Cells["codnota"].Value.ToString();
				notaS.Docref = dgvDocumentos.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
				notaS.Total = Convert.ToDouble(dgvDocumentos.Rows[e.RowIndex].Cells["Total"].Value.ToString());
				notaS.DocumentoReferencia = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells["codReferencia"].Value.ToString());
				notaS.FechaRegistro = Convert.ToDateTime(dgvDocumentos.Rows[e.RowIndex].Cells["fecha"].Value.ToString());
				notaS.CodAlmacen = Convert.ToInt32(dgvDocumentos.Rows[e.RowIndex].Cells["codAlmacen"].Value.ToString());
			}
		}
	}

	private void dg_notascreditos_CellDoubleClick(object sender, GridViewCellEventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaNCreditosSinAplicar));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.dgvDocumentos = new Telerik.WinControls.UI.RadGridView();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.dgvDocumentos.Dock = System.Windows.Forms.DockStyle.Top;
		this.dgvDocumentos.Location = new System.Drawing.Point(0, 0);
		this.dgvDocumentos.MasterTemplate.AllowAddNewRow = false;
		gridViewTextBoxColumn1.FieldName = "codnota";
		gridViewTextBoxColumn1.HeaderText = "CODIGO";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codnota";
		gridViewTextBoxColumn2.FieldName = "numdocumento";
		gridViewTextBoxColumn2.HeaderText = "N° DOCUMENTO";
		gridViewTextBoxColumn2.Name = "numdoc";
		gridViewTextBoxColumn2.Width = 150;
		gridViewTextBoxColumn3.FieldName = "total2";
		gridViewTextBoxColumn3.HeaderText = "TOTAL";
		gridViewTextBoxColumn3.Name = "total2";
		gridViewTextBoxColumn3.Width = 120;
		gridViewTextBoxColumn4.FieldName = "total";
		gridViewTextBoxColumn4.HeaderText = "DISPONIBLE";
		gridViewTextBoxColumn4.Name = "Total";
		gridViewTextBoxColumn4.Width = 200;
		gridViewTextBoxColumn5.FieldName = "codReferencia";
		gridViewTextBoxColumn5.HeaderText = "codReferencia";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "codReferencia";
		gridViewTextBoxColumn6.FieldName = "fecha";
		gridViewTextBoxColumn6.HeaderText = "FECHA";
		gridViewTextBoxColumn6.Name = "fecha";
		gridViewTextBoxColumn6.Width = 120;
		gridViewTextBoxColumn7.FieldName = "codAlmacen";
		gridViewTextBoxColumn7.HeaderText = "codAlmacen";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "codAlmacen";
		this.dgvDocumentos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7);
		this.dgvDocumentos.MasterTemplate.EnableFiltering = true;
		this.dgvDocumentos.MasterTemplate.EnablePaging = true;
		this.dgvDocumentos.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvDocumentos.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvDocumentos.Name = "dgvDocumentos";
		this.dgvDocumentos.ReadOnly = true;
		this.dgvDocumentos.Size = new System.Drawing.Size(655, 517);
		this.dgvDocumentos.TabIndex = 8;
		this.dgvDocumentos.ThemeName = "TelerikMetroTouch";
		this.dgvDocumentos.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dg_notascreditos_CellClick);
		this.dgvDocumentos.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dg_notascreditos_CellDoubleClick);
		this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnAceptar.Image = SIGEFA.Properties.Resources.checkmark_yes_24px;
		this.btnAceptar.Location = new System.Drawing.Point(374, 523);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 7;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(655, 558);
		base.Controls.Add(this.dgvDocumentos);
		base.Controls.Add(this.btnAceptar);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmListaNCreditosSinAplicar";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Notas Creditos Sin Aplicar";
		base.ThemeName = "TelerikMetroTouch";
		base.Load += new System.EventHandler(frmListaNCreditosSinAplicar_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvDocumentos).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
