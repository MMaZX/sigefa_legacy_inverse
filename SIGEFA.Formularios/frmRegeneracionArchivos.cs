using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.SunatFacElec;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmRegeneracionArchivos : Office2007Form
{
	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsAdmNotaCredito admnc = new clsAdmNotaCredito();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsAdmGuiaRemision AdmGuia = new clsAdmGuiaRemision();

	private clsAdmDocumentoIdentidad AdmDocumentoIdentidad = new clsAdmDocumentoIdentidad();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsNotaCredito nc;

	private clsSerie ser = new clsSerie();

	private clsTransaccion tran = new clsTransaccion();

	private clsFacturaVenta ventaConsultada;

	private clsCliente cli = new clsCliente();

	private clsGuiaRemision guia = new clsGuiaRemision();

	public List<clsDetalleFacturaVenta> ListaDetalleVenta = new List<clsDetalleFacturaVenta>();

	public List<clsDetalleNotaCredito> listadetalleNC = new List<clsDetalleNotaCredito>();

	private DataTable detalleTableVenta;

	private DataTable detalleNC;

	private clsAdmTipoDocumento admTipoDocumento = new clsAdmTipoDocumento();

	private int NumeroDocumento;

	private Facturacion facturacion = new Facturacion();

	private IContainer components = null;

	private DataGridView dgvVentaSinRepositorio;

	private Panel panel2;

	private Label label2;

	private Label label1;

	private DateTimePicker dtpHasta;

	private DateTimePicker dtpDesde;

	private Button btnGenerarArchivos;

	private Label label4;

	private Label label3;

	private Panel panel1;

	private Button btnBuscarDocumentos;

	private DataGridViewAutoFilterTextBoxColumn fecha;

	private DataGridViewAutoFilterTextBoxColumn tipo_documento;

	private DataGridViewAutoFilterTextBoxColumn numdoc;

	private DataGridViewTextBoxColumn numerocliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn codFactura;

	private DataGridViewTextBoxColumn codcliente;

	private Label label5;

	private RadDropDownList cmbAlmacenes;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private RadDropDownList cmbTipoDoc;

	private Label label6;

	public byte[] firmadigital { get; set; }

	public frmRegeneracionArchivos()
	{
		InitializeComponent();
	}

	private void CargaLista()
	{
		if (Convert.ToInt32(cmbTipoDoc.SelectedValue) != 4)
		{
			dgvVentaSinRepositorio.ClearSelection();
			dgvVentaSinRepositorio.AutoGenerateColumns = false;
			dgvVentaSinRepositorio.DataSource = data;
			data.DataSource = AdmVenta.VentaSinRepositorio(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value, Convert.ToInt32(cmbTipoDoc.SelectedValue));
			data.Filter = string.Empty;
			filtro = string.Empty;
			if (dgvVentaSinRepositorio.Rows.Count > 0)
			{
				btnGenerarArchivos.Enabled = true;
			}
			else
			{
				btnGenerarArchivos.Enabled = false;
			}
		}
		else
		{
			dgvVentaSinRepositorio.ClearSelection();
			dgvVentaSinRepositorio.AutoGenerateColumns = false;
			dgvVentaSinRepositorio.DataSource = data;
			data.DataSource = admnc.NCsinRepositorio(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value);
			data.Filter = string.Empty;
			filtro = string.Empty;
			if (dgvVentaSinRepositorio.Rows.Count > 0)
			{
				btnGenerarArchivos.Enabled = true;
			}
			else
			{
				btnGenerarArchivos.Enabled = false;
			}
		}
		dgvVentaSinRepositorio.Focus();
	}

	private async void btnGenerarArchivos_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvVentaSinRepositorio.Rows.Count <= 0 || dgvVentaSinRepositorio.CurrentRow == null)
			{
				return;
			}
			foreach (DataGridViewRow row in (IEnumerable)dgvVentaSinRepositorio.Rows)
			{
				if (Convert.ToInt32(cmbTipoDoc.SelectedValue) == 4)
				{
					nc = new clsNotaCredito();
					nc = admnc.CargaNotaCredito_Regeneracion(Convert.ToInt32(row.Cells[codFactura.Name].Value));
					if (nc != null)
					{
						CargaTransaccion(nc.CodTipoTransaccion);
						cli = AdmCli.MuestraCliente(nc.CodCliente);
						CargaDetalleNC();
						RecorreDetalleNC();
						await facturacion.DatosNCredito(cli, nc, listadetalleNC);
						firmadigital = facturacion.LogoEmp;
						listadetalleNC.Clear();
						row.DefaultCellStyle.BackColor = Color.Green;
					}
					else
					{
						MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					continue;
				}
				ventaConsultada = new clsFacturaVenta();
				ventaConsultada = AdmVenta.CargaFacturaVenta_Regeneracion(Convert.ToInt32(row.Cells[codFactura.Name].Value));
				if (ventaConsultada != null)
				{
					CargaTransaccion(ventaConsultada.CodTipoTransaccion);
					cli = AdmCli.MuestraCliente(ventaConsultada.CodCliente);
					ventaConsultada.DocumentoIdentidad = AdmDocumentoIdentidad.MuestraDocumentoIdentidad(Convert.ToInt32(ventaConsultada.CodigoDocumentoIdentidad));
					CargaDetalle();
					RecorreDetalle();
					await facturacion.GeneraDocumento(cli, ventaConsultada, ListaDetalleVenta, 0);
					firmadigital = facturacion.LogoEmp;
					ListaDetalleVenta.Clear();
					row.DefaultCellStyle.BackColor = Color.Green;
				}
				else
				{
					MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			CargaLista();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show("Ocurrió un error: " + ex2.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void RecorreDetalleNC()
	{
		if (detalleNC.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in detalleNC.Rows)
		{
			añadedetalleNC(row);
		}
	}

	private void CargaDetalleNC()
	{
		detalleNC = admnc.CargaDetalle(Convert.ToInt32(nc.CodNotaCredito));
	}

	private void añadedetalleNC(DataRow fila)
	{
		clsDetalleNotaCredito detafac = new clsDetalleNotaCredito();
		detafac.CodNotaCredito = Convert.ToInt32(nc.CodNotaCredito);
		detafac.CodProducto = Convert.ToInt32(fila["codproducto"]);
		detafac.CodAlmacen = Convert.ToInt32(cmbAlmacenes.SelectedValue);
		detafac.UnidadIngresada = Convert.ToInt32(fila["codUnidadMedida"]);
		detafac.SerieLote = "0";
		detafac.Cantidad = Convert.ToDouble(fila["cantidad"]);
		detafac.PrecioUnitario = Convert.ToDouble(fila["preciounitario"]);
		detafac.Subtotal = Convert.ToDouble(fila["subtotal"]);
		detafac.Descuento1 = Convert.ToDouble(fila["descuento1"]);
		detafac.Descuento2 = Convert.ToDouble(fila["descuento2"]);
		detafac.Descuento3 = Convert.ToDouble(fila["descuento3"]);
		detafac.MontoDescuento = Convert.ToDouble(fila["montodscto"]);
		detafac.Igv = Convert.ToDouble(fila["igv"]);
		detafac.Importe = Convert.ToDouble(fila["importe"]);
		detafac.PrecioReal = Convert.ToDouble(fila["precioreal"]);
		detafac.ValoReal = Convert.ToDouble(fila["valoreal"]);
		detafac.FechaIngreso = Convert.ToDateTime(fila["fechaingreso"]);
		detafac.DescripcionNC = "";
		detafac.Moneda = Convert.ToInt32(fila["moneda"]);
		detafac.CodUser = frmLogin.iCodUser;
		detafac.TipoImpuesto = "10";
		listadetalleNC.Add(detafac);
	}

	private void dgvVentaSinRepositorio_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void CargaDetalle()
	{
		detalleTableVenta = AdmVenta.CargaDetalle_Regeneracion(Convert.ToInt32(ventaConsultada.CodFacturaVenta), frmLogin.iCodAlmacen);
	}

	private void RecorreDetalle()
	{
		if (detalleTableVenta.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in detalleTableVenta.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataRow fila)
	{
		clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
		deta.CodProducto = Convert.ToInt32(fila["codProducto"]);
		deta.CodVenta = Convert.ToInt32(ventaConsultada.CodFacturaVenta);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila["codUnidadMedida"]);
		deta.SerieLote = "";
		deta.Cantidad = Convert.ToDecimal(fila["cantidad"]);
		deta.PrecioUnitario = Convert.ToDecimal(fila["preciounitario"]);
		deta.Subtotal = Convert.ToDecimal(fila["subtotal"]);
		deta.Descuento1 = Convert.ToDecimal(fila["descuento1"]);
		deta.MontoDescuento = Convert.ToDecimal(fila["montodscto"]);
		deta.Igv = Convert.ToDecimal(fila["igv"]);
		deta.Importe = Convert.ToDecimal(fila["importe"]);
		deta.PrecioReal = Convert.ToDecimal(fila["precioreal"]);
		deta.ValoReal = Convert.ToDecimal(fila["valoreal"]);
		deta.CodUser = frmLogin.iCodUser;
		deta.CantidadPendiente = Convert.ToDecimal(fila["cantidad"]);
		deta.Moneda = 1;
		deta.Descripcion = fila["producto"].ToString();
		deta.CodTipoArticulo = 1;
		deta.Tipoimpuesto = fila["tipoimpuesto"].ToString();
		deta.Entregado = true;
		deta.TipoUnidad = 1;
		deta.CodDetalleCotizacion = 0;
		deta.CodDetallePedido = Convert.ToInt32(fila["codDetalle"]);
		if (deta.Tipoimpuesto == "" || (deta.Tipoimpuesto.StartsWith("1") && deta.Igv == 0m))
		{
			decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			deta.Igv = deta.Importe - deta.Importe / factorigv;
			deta.ValoReal = deta.PrecioReal / factorigv;
			ventaConsultada.Igv += deta.Igv;
		}
		ListaDetalleVenta.Add(deta);
	}

	private void CargaTransaccion(int CodTransaccion)
	{
		tran = AdmTran.MuestraTransaccion(CodTransaccion);
		tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
	}

	private void btnBuscarDocumentos_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dgvVentaSinRepositorio_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Delete)
		{
			btnGenerarArchivos.PerformClick();
		}
	}

	private void frmRegeneracionArchivos_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void frmRegeneracionArchivos_Load(object sender, EventArgs e)
	{
		cargaTiposDoc();
		cargaAlmacenes();
		dtpDesde.Focus();
	}

	private void cargaTiposDoc()
	{
		cmbTipoDoc.ValueMember = "codTipoDocumento";
		cmbTipoDoc.DisplayMember = "descripcion";
		cmbTipoDoc.DataSource = admTipoDocumento.MuestraTipoDocumentosElectronicos_2();
		cmbTipoDoc.SelectedValue = 1;
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		cmbAlmacenes.Items.RemoveAt(0);
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void dtpDesde_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B)
		{
			btnBuscarDocumentos.PerformClick();
		}
	}

	private void dtpHasta_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.B)
		{
			btnBuscarDocumentos.PerformClick();
		}
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRegeneracionArchivos));
		this.dgvVentaSinRepositorio = new System.Windows.Forms.DataGridView();
		this.fecha = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.tipo_documento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numdoc = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numerocliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label6 = new System.Windows.Forms.Label();
		this.cmbTipoDoc = new Telerik.WinControls.UI.RadDropDownList();
		this.label5 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.btnBuscarDocumentos = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.btnGenerarArchivos = new System.Windows.Forms.Button();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		((System.ComponentModel.ISupportInitialize)this.dgvVentaSinRepositorio).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbTipoDoc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.dgvVentaSinRepositorio.AllowUserToAddRows = false;
		this.dgvVentaSinRepositorio.AllowUserToDeleteRows = false;
		this.dgvVentaSinRepositorio.AllowUserToResizeRows = false;
		this.dgvVentaSinRepositorio.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		this.dgvVentaSinRepositorio.BackgroundColor = System.Drawing.Color.White;
		this.dgvVentaSinRepositorio.ColumnHeadersHeight = 40;
		this.dgvVentaSinRepositorio.Columns.AddRange(this.fecha, this.tipo_documento, this.numdoc, this.numerocliente, this.cliente, this.moneda, this.total, this.codFactura, this.codcliente);
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvVentaSinRepositorio.DefaultCellStyle = dataGridViewCellStyle1;
		this.dgvVentaSinRepositorio.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentaSinRepositorio.Location = new System.Drawing.Point(0, 0);
		this.dgvVentaSinRepositorio.MultiSelect = false;
		this.dgvVentaSinRepositorio.Name = "dgvVentaSinRepositorio";
		this.dgvVentaSinRepositorio.ReadOnly = true;
		this.dgvVentaSinRepositorio.RowHeadersVisible = false;
		this.dgvVentaSinRepositorio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentaSinRepositorio.Size = new System.Drawing.Size(1062, 398);
		this.dgvVentaSinRepositorio.TabIndex = 0;
		this.dgvVentaSinRepositorio.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvVentaSinRepositorio_RowStateChanged);
		this.dgvVentaSinRepositorio.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvVentaSinRepositorio_KeyDown);
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha del Documento";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.fecha.Width = 140;
		this.tipo_documento.DataPropertyName = "tipo_documento";
		this.tipo_documento.HeaderText = "Tipo de Documento";
		this.tipo_documento.Name = "tipo_documento";
		this.tipo_documento.ReadOnly = true;
		this.tipo_documento.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.tipo_documento.Width = 180;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "Serie-Correlativo de Documento";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.numdoc.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.numdoc.Width = 180;
		this.numerocliente.DataPropertyName = "numerocliente";
		this.numerocliente.HeaderText = "Nº Documento Cliente";
		this.numerocliente.Name = "numerocliente";
		this.numerocliente.ReadOnly = true;
		this.numerocliente.Width = 140;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Razón Social Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Width = 300;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Width = 83;
		this.total.DataPropertyName = "total";
		this.total.HeaderText = "Monto Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.total.Width = 98;
		this.codFactura.DataPropertyName = "codFactura";
		this.codFactura.HeaderText = "Código Factura";
		this.codFactura.Name = "codFactura";
		this.codFactura.ReadOnly = true;
		this.codFactura.Visible = false;
		this.codFactura.Width = 114;
		this.codcliente.DataPropertyName = "codcliente";
		this.codcliente.HeaderText = "Codigo Cliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.codcliente.Visible = false;
		this.codcliente.Width = 111;
		this.panel2.Controls.Add(this.label6);
		this.panel2.Controls.Add(this.cmbTipoDoc);
		this.panel2.Controls.Add(this.label5);
		this.panel2.Controls.Add(this.cmbAlmacenes);
		this.panel2.Controls.Add(this.btnBuscarDocumentos);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.label3);
		this.panel2.Controls.Add(this.btnGenerarArchivos);
		this.panel2.Controls.Add(this.dtpHasta);
		this.panel2.Controls.Add(this.dtpDesde);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.label2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(1062, 120);
		this.panel2.TabIndex = 2;
		this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(304, 83);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(119, 20);
		this.label6.TabIndex = 18;
		this.label6.Text = "COMPROBANTE:";
		this.cmbTipoDoc.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbTipoDoc.Location = new System.Drawing.Point(425, 82);
		this.cmbTipoDoc.Name = "cmbTipoDoc";
		this.cmbTipoDoc.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
		this.cmbTipoDoc.RootElement.StretchVertically = true;
		this.cmbTipoDoc.Size = new System.Drawing.Size(165, 24);
		this.cmbTipoDoc.TabIndex = 17;
		this.cmbTipoDoc.ThemeName = "Fluent";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(8, 83);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(80, 20);
		this.label5.TabIndex = 16;
		this.label5.Text = "ALMACEN:";
		this.cmbAlmacenes.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbAlmacenes.Location = new System.Drawing.Point(93, 81);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
		this.cmbAlmacenes.RootElement.StretchVertically = true;
		this.cmbAlmacenes.Size = new System.Drawing.Size(177, 24);
		this.cmbAlmacenes.TabIndex = 15;
		this.cmbAlmacenes.ThemeName = "Fluent";
		this.btnBuscarDocumentos.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBuscarDocumentos.Image = (System.Drawing.Image)resources.GetObject("btnBuscarDocumentos.Image");
		this.btnBuscarDocumentos.Location = new System.Drawing.Point(429, 39);
		this.btnBuscarDocumentos.Name = "btnBuscarDocumentos";
		this.btnBuscarDocumentos.Size = new System.Drawing.Size(111, 33);
		this.btnBuscarDocumentos.TabIndex = 14;
		this.btnBuscarDocumentos.Text = "BUSCAR";
		this.btnBuscarDocumentos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBuscarDocumentos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarDocumentos.UseVisualStyleBackColor = true;
		this.btnBuscarDocumentos.Click += new System.EventHandler(btnBuscarDocumentos_Click);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(709, 26);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(302, 20);
		this.label4.TabIndex = 7;
		this.label4.Text = "Seleccione una venta y haga clic en el botón";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(14, 10);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(409, 20);
		this.label3.TabIndex = 6;
		this.label3.Text = "Seleccione las fechas para filtrar las ventas que desea buscar";
		this.btnGenerarArchivos.Enabled = false;
		this.btnGenerarArchivos.Font = new System.Drawing.Font("Segoe UI Semibold", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerarArchivos.Image = (System.Drawing.Image)resources.GetObject("btnGenerarArchivos.Image");
		this.btnGenerarArchivos.Location = new System.Drawing.Point(709, 50);
		this.btnGenerarArchivos.Name = "btnGenerarArchivos";
		this.btnGenerarArchivos.Size = new System.Drawing.Size(302, 35);
		this.btnGenerarArchivos.TabIndex = 5;
		this.btnGenerarArchivos.Text = "GENERAR ARCHIVOS (supr)";
		this.btnGenerarArchivos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerarArchivos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGenerarArchivos.UseVisualStyleBackColor = true;
		this.btnGenerarArchivos.Click += new System.EventHandler(btnGenerarArchivos_Click);
		this.dtpHasta.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(291, 42);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(132, 27);
		this.dtpHasta.TabIndex = 3;
		this.dtpHasta.KeyDown += new System.Windows.Forms.KeyEventHandler(dtpHasta_KeyDown);
		this.dtpDesde.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(77, 42);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(132, 27);
		this.dtpDesde.TabIndex = 2;
		this.dtpDesde.KeyDown += new System.Windows.Forms.KeyEventHandler(dtpDesde_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(13, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(58, 20);
		this.label1.TabIndex = 0;
		this.label1.Text = "DESDE:";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(227, 45);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(58, 20);
		this.label2.TabIndex = 1;
		this.label2.Text = "HASTA:";
		this.panel1.Controls.Add(this.dgvVentaSinRepositorio);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 120);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1062, 398);
		this.panel1.TabIndex = 3;
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1062, 518);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel2);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "frmRegeneracionArchivos";
		this.Text = "Generación de PDF y XML";
		base.Load += new System.EventHandler(frmRegeneracionArchivos_Load);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmRegeneracionArchivos_KeyDown);
		((System.ComponentModel.ISupportInitialize)this.dgvVentaSinRepositorio).EndInit();
		this.panel2.ResumeLayout(false);
		this.panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbTipoDoc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
