using System;
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

public class frmMuestraPagosProveedor : Office2007Form
{
	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsPago Pag = new clsPago();

	private clsAdmPago Admpag = new clsAdmPago();

	public int CodNota;

	public bool InOut;

	public int tipo;

	public int tipodocumento = 0;

	private clsSerie ser = new clsSerie();

	private clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private IContainer components = null;

	private DataGridView dgvPagos;

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

	public frmMuestraPagosProveedor()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		dgvPagos.DataSource = data;
		if (tipodocumento == 1)
		{
			data.DataSource = Admpag.MuestraListaPagosPorNota(CodNota, InOut, tipo, frmLogin.iCodAlmacen);
		}
		else if (tipodocumento == 2)
		{
			data.DataSource = Admpag.MuestraListaPagosPorNota(CodNota, InOut, 1, frmLogin.iCodAlmacen);
		}
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvPagos.ClearSelection();
	}

	private void frmMuestraPagosProveedor_Load(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvPagos_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPagos.Rows.Count >= 1 && dgvPagos.Rows[e.RowIndex].Selected)
		{
			DataGridViewCell celda = dgvPagos.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if (celda.Value.ToString() == "Imprimir pago")
			{
				Pag.CodPago = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells[codpago.Name].Value);
				CRImpresionCobro rpt = new CRImpresionCobro();
				frmRptImpresionPago frm = new frmRptImpresionPago();
				PrintOptions rptoption = rpt.PrintOptions;
				rptoption.PrinterName = ser.NombreImpresora;
				rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rpt.SetDataSource(ds.ReporteImpresionCobro(Pag.CodPago, frmLogin.iCodAlmacen));
				frm.cRVImpresionPago.ReportSource = rpt;
				frm.Show();
			}
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).BeginInit();
		base.SuspendLayout();
		this.dgvPagos.AllowUserToAddRows = false;
		this.dgvPagos.AllowUserToDeleteRows = false;
		this.dgvPagos.AllowUserToResizeRows = false;
		this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvPagos.Columns.AddRange(this.codpago, this.fecha, this.moneda, this.monto, this.tipopago, this.noperacion, this.ncheque, this.cobrador, this.accion, this.codfacturas, this.aprobados);
		this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvPagos.Location = new System.Drawing.Point(0, 0);
		this.dgvPagos.MultiSelect = false;
		this.dgvPagos.Name = "dgvPagos";
		this.dgvPagos.ReadOnly = true;
		this.dgvPagos.RowHeadersVisible = false;
		this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPagos.Size = new System.Drawing.Size(903, 218);
		this.dgvPagos.TabIndex = 0;
		this.dgvPagos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPagos_CellContentClick);
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
		dataGridViewCellStyle2.Format = "N2";
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
		this.aprobados.HeaderText = "aprobado";
		this.aprobados.Name = "aprobados";
		this.aprobados.ReadOnly = true;
		this.aprobados.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(903, 218);
		base.Controls.Add(this.dgvPagos);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmMuestraPagosProveedor";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Muestra Pagos";
		base.Load += new System.EventHandler(frmMuestraPagosProveedor_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvPagos).EndInit();
		base.ResumeLayout(false);
	}
}
