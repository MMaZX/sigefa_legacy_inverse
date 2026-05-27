using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmMuestraPagos : Office2007Form
{
	private clsSerie ser = new clsSerie();

	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	public int CodNota;

	public bool InOut;

	public int tipo;

	public decimal montoTotal;

	public string CodPago;

	private clsTipoDocumento doc2 = new clsTipoDocumento();

	private clsSerie seri2 = new clsSerie();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private int codDocumentoPago = 0;

	private clsAdmPago AdmPagos = new clsAdmPago();

	public int codCuota;

	public int pagocompra = 0;

	private string siglaPago;

	private string seriePago;

	private string numeroPago;

	private IContainer components = null;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem finalizarToolStripMenuItem;

	private GroupBox groupBox1;

	private Button btnFinalizar;

	private DataGridView dgvPagos;

	private ImageList imageList1;

	private DataGridViewTextBoxColumn codpago;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn monto;

	private DataGridViewTextBoxColumn tipopago;

	private DataGridViewTextBoxColumn noperacion;

	private DataGridViewTextBoxColumn ncheque;

	private DataGridViewTextBoxColumn cobrador;

	private DataGridViewLinkColumn accion;

	private DataGridViewTextBoxColumn codfacturas;

	private DataGridViewTextBoxColumn aprobados;

	public int Almacen { get; set; }

	public frmMuestraPagos()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		if (tipo == 5)
		{
			dgvPagos.DataSource = data;
			data.DataSource = Admpag.MuestraListaPagosPorNota(codCuota, InOut, tipo, Almacen);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvPagos.ClearSelection();
		}
		else
		{
			dgvPagos.DataSource = data;
			data.DataSource = Admpag.MuestraListaPagosPorNota(CodNota, InOut, tipo, Almacen);
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvPagos.ClearSelection();
		}
	}

	private void frmMuestraPagos_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvPagos.Rows.Count < 1 || e.RowIndex <= -1)
			{
				return;
			}
			DataGridViewCell celda = dgvPagos.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (pagocompra == 19)
			{
				if (celda.Value.ToString() == "Imprimir pago")
				{
					Pag.CodPago = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codpago.Name].Value);
					CRImpresionCobro rpt = new CRImpresionCobro();
					frmRptImpresionPago frm = new frmRptImpresionPago();
					PrintOptions rptoption = rpt.PrintOptions;
					rptoption.PrinterName = ser.NombreImpresora;
					rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
					rpt.SetDataSource(ds.ReporteImpresionCobro(Pag.CodPago, Almacen));
					frm.cRVImpresionPago.ReportSource = rpt;
					frm.Show();
				}
			}
			else if (celda.Value.ToString() == "Imprimir pago")
			{
				Pag.CodPago = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codpago.Name].Value);
				CRImpresionPago rpt2 = new CRImpresionPago();
				frmRptImpresionPago frm2 = new frmRptImpresionPago();
				PrintOptions rptoption2 = rpt2.PrintOptions;
				rptoption2.PrinterName = ser.NombreImpresora;
				rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rpt2.SetDataSource(ds.ReporteImpresionPago(Pag.CodPago, Almacen));
				frm2.cRVImpresionPago.ReportSource = rpt2;
				frm2.Show();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "MuestraPago:dgvPagos_CellContentClick");
		}
	}

	private void finalizarToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			DataGridViewRow Row = dgvPagos.SelectedRows[0];
			Pag.CodPago = Convert.ToInt32(Row.Cells[codpago.Name].Value.ToString());
			CodPago = Pag.CodPago.ToString();
			if (ActualizaCobro(CodPago) && CodPago != "")
			{
				printaRecibo(CodPago);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void printaRecibo(string CodPago)
	{
		try
		{
			CRImpresionPago rpt = new CRImpresionPago();
			frmRptImpresionPago frm = new frmRptImpresionPago();
			PrintOptions rptoption = rpt.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rpt.SetDataSource(ds.ReporteImpresionPago(Convert.ToInt32(CodPago), Almacen));
			frm.cRVImpresionPago.ReportSource = rpt;
			frm.ShowDialog();
			if (dgvPagos.DataSource != null)
			{
				dgvPagos.AutoGenerateColumns = false;
				dgvPagos.DataSource = null;
				CargaLista();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private bool ActualizaCobro(string CodPago)
	{
		string sigl = "";
		bool devuelve = false;
		try
		{
			sigl = "RC";
			if (valida_serie(sigl))
			{
				seri2 = null;
				seri2 = Admser.BuscaSeriexDocumento(codDocumentoPago, Almacen);
				if (seri2 != null)
				{
					seriePago = seri2.Serie;
					numeroPago = seri2.Numeracion.ToString();
					devuelve = (AdmPagos.ActualizaPagoAprobado(seriePago, numeroPago, Convert.ToInt32(CodPago)) ? true : false);
				}
			}
			return devuelve;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private bool valida_serie(string sigl)
	{
		doc2 = null;
		try
		{
			doc2 = Admdoc.BuscaTipoDocumento(sigl);
			if (doc2 != null)
			{
				codDocumentoPago = doc2.CodTipoDocumento;
				siglaPago = doc2.Sigla;
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private void dgvPagos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		try
		{
			if (dgvPagos.Rows.Count < 1 || !e.Row.Selected)
			{
				return;
			}
			int contador = 0;
			foreach (DataGridViewRow row in (IEnumerable)dgvPagos.Rows)
			{
				if (row.Cells[aprobados.Name].Value.ToString() == "FINALIZADO")
				{
					contador++;
				}
			}
			if (contador == dgvPagos.Rows.Count)
			{
				btnFinalizar.Enabled = true;
			}
			else
			{
				btnFinalizar.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnFinalizar_Click(object sender, EventArgs e)
	{
		try
		{
			decimal montosTotal = default(decimal);
			montoTotal = decimal.Round(montoTotal, 2);
			foreach (DataGridViewRow row in (IEnumerable)dgvPagos.Rows)
			{
				Pag.CodNota = row.Cells[codfacturas.Name].Value.ToString();
				montosTotal += Convert.ToDecimal(row.Cells[monto.Name].Value.ToString());
			}
			if (montoTotal >= montosTotal)
			{
				if (Application.OpenForms["frmVenta"] != null)
				{
					Application.OpenForms["frmVenta"].Activate();
					return;
				}
				frmVenta form1 = new frmVenta();
				form1.Proceso = 3;
				form1.tip = 1;
				form1.CodVenta = Pag.CodNota;
				form1.CodPago = "0";
				form1.WindowState = FormWindowState.Normal;
				form1.StartPosition = FormStartPosition.CenterScreen;
				form1.ShowDialog();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmMuestraPagos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.finalizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnFinalizar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.dgvPagos = new System.Windows.Forms.DataGridView();
		this.codpago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tipopago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.noperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ncheque = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cobrador = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.accion = new System.Windows.Forms.DataGridViewLinkColumn();
		this.codfacturas = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.aprobados = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.contextMenuStrip1.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).BeginInit();
		base.SuspendLayout();
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.finalizarToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.Size = new System.Drawing.Size(155, 26);
		this.finalizarToolStripMenuItem.Name = "finalizarToolStripMenuItem";
		this.finalizarToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
		this.finalizarToolStripMenuItem.Text = "Generar Recibo";
		this.finalizarToolStripMenuItem.Click += new System.EventHandler(finalizarToolStripMenuItem_Click);
		this.groupBox1.Controls.Add(this.btnFinalizar);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox1.Location = new System.Drawing.Point(0, 244);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(995, 14);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.btnFinalizar.Enabled = false;
		this.btnFinalizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnFinalizar.ImageIndex = 4;
		this.btnFinalizar.ImageList = this.imageList1;
		this.btnFinalizar.Location = new System.Drawing.Point(430, 19);
		this.btnFinalizar.Name = "btnFinalizar";
		this.btnFinalizar.Size = new System.Drawing.Size(108, 36);
		this.btnFinalizar.TabIndex = 0;
		this.btnFinalizar.Text = "GENERAR \r\nDOC. VENTA";
		this.btnFinalizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnFinalizar.UseVisualStyleBackColor = true;
		this.btnFinalizar.Visible = false;
		this.btnFinalizar.Click += new System.EventHandler(btnFinalizar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.dgvPagos.AllowUserToAddRows = false;
		this.dgvPagos.AllowUserToDeleteRows = false;
		this.dgvPagos.AllowUserToResizeRows = false;
		this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvPagos.Columns.AddRange(this.codpago, this.fecha, this.moneda, this.monto, this.tipopago, this.noperacion, this.ncheque, this.cobrador, this.accion, this.codfacturas, this.aprobados);
		this.dgvPagos.ContextMenuStrip = this.contextMenuStrip1;
		this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvPagos.Location = new System.Drawing.Point(0, 0);
		this.dgvPagos.MultiSelect = false;
		this.dgvPagos.Name = "dgvPagos";
		this.dgvPagos.ReadOnly = true;
		this.dgvPagos.RowHeadersVisible = false;
		this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPagos.Size = new System.Drawing.Size(995, 244);
		this.dgvPagos.TabIndex = 2;
		this.dgvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPagos_CellContentClick);
		this.dgvPagos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvPagos_RowStateChanged);
		this.codpago.DataPropertyName = "codPago";
		this.codpago.HeaderText = "Codigo";
		this.codpago.Name = "codpago";
		this.codpago.ReadOnly = true;
		this.codpago.Width = 80;
		this.fecha.DataPropertyName = "fechapago";
		dataGridViewCellStyle1.Format = "d";
		dataGridViewCellStyle1.NullValue = null;
		this.fecha.DefaultCellStyle = dataGridViewCellStyle1;
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.monto.DataPropertyName = "montopagado";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.NullValue = null;
		this.monto.DefaultCellStyle = dataGridViewCellStyle2;
		this.monto.HeaderText = "Monto";
		this.monto.Name = "monto";
		this.monto.ReadOnly = true;
		this.tipopago.DataPropertyName = "tipo";
		this.tipopago.HeaderText = "Tipo";
		this.tipopago.Name = "tipopago";
		this.tipopago.ReadOnly = true;
		this.noperacion.DataPropertyName = "noperacion";
		this.noperacion.HeaderText = "N° Operacion";
		this.noperacion.Name = "noperacion";
		this.noperacion.ReadOnly = true;
		this.ncheque.DataPropertyName = "ncheque";
		this.ncheque.HeaderText = "N° Cheque";
		this.ncheque.Name = "ncheque";
		this.ncheque.ReadOnly = true;
		this.cobrador.DataPropertyName = "cobrador";
		this.cobrador.HeaderText = "Cobrador";
		this.cobrador.Name = "cobrador";
		this.cobrador.ReadOnly = true;
		this.accion.DataPropertyName = "accion";
		this.accion.HeaderText = "Acción";
		this.accion.Name = "accion";
		this.accion.ReadOnly = true;
		this.accion.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.accion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.codfacturas.DataPropertyName = "codfacturas";
		this.codfacturas.HeaderText = "codfactura";
		this.codfacturas.Name = "codfacturas";
		this.codfacturas.ReadOnly = true;
		this.codfacturas.Visible = false;
		this.aprobados.DataPropertyName = "aprobado";
		this.aprobados.HeaderText = "Aprobado";
		this.aprobados.Name = "aprobados";
		this.aprobados.ReadOnly = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(995, 258);
		base.Controls.Add(this.dgvPagos);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMuestraPagos";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Muestra Pagos";
		base.Load += new System.EventHandler(frmMuestraPagos_Load);
		this.contextMenuStrip1.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).EndInit();
		base.ResumeLayout(false);
	}
}
