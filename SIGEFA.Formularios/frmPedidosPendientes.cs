using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmPedidosPendientes : Office2007Form
{
	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	public int Proceso = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsSerie ser = null;

	private bool bandera = true;

	private clsAdmTransferencia admtrans = new clsAdmTransferencia();

	private int codproducto_error = 0;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	private List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	private clsAdmSerie admSerie = new clsAdmSerie();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnSalir;

	private Button btGenVenta;

	private ImageList imageList1;

	private Button btnAnular;

	private ButtonItem buttonItem4;

	private ButtonItem buttonItem1;

	private Button button1;

	private TextBox txtFiltro;

	private Label label10;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnIrNota;

	private Button btnBusqueda;

	private ImageList imageList2;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn Numeracion;

	private DataGridViewTextBoxColumn Pedido_;

	private DataGridViewTextBoxColumn RazonSocial;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn documento;

	private DataGridViewTextBoxColumn responsable;

	public DataGridView dgvPedidosPendientes;

	public frmVenta2019 venta2019 { get; set; }

	public frmPedidosPendientes()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void CargaLista()
	{
		dgvPedidosPendientes.DataSource = data;
		data.DataSource = AdmPedido.MuestraPedidos(frmLogin.iCodUser, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvPedidosPendientes.ClearSelection();
	}

	private void frmPedidosPendientes_Load(object sender, EventArgs e)
	{
		dtpDesde.Value = DateTime.Now.AddDays(-6.0);
		CargaLista();
	}

	private void btGenVenta_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null && pedido.CodPedido != "")
		{
			if (Application.OpenForms["frmGeneraVenta"] != null)
			{
				Application.OpenForms["frmGeneraVenta"].Close();
				return;
			}
			frmGeneraVenta form = new frmGeneraVenta();
			form.Proceso = 4;
			form.CodPedido = Convert.ToInt32(dgvPedidosPendientes.CurrentRow.Cells[codigo.Name].Value);
			form.FormClosed += Form_Closed;
			form.Show();
		}
	}

	private void Form_Closed(object sender, FormClosedEventArgs e)
	{
		frmGeneraVenta frm = (frmGeneraVenta)sender;
		CargaLista();
	}

	private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && e.Row.Selected)
		{
			pedido.CodPedido = e.Row.Cells[codigo.Name].Value.ToString();
		}
	}

	private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
		{
			venta2019.codPedidoVenta = Convert.ToInt32(dgvPedidosPendientes.CurrentRow.Cells[0].Value);
			venta2019.cargaPedido = true;
			Close();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.SelectedRows.Count <= 0 || dgvPedidosPendientes.CurrentRow == null || !(pedido.CodPedido != ""))
		{
			return;
		}
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el pedido seleccionado", "Pedidos Pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.No)
		{
			return;
		}
		DataTable dat_req = admreqalm.CargaRequerimientosSegunPedido(Convert.ToInt32(pedido.CodPedido));
		if (dat_req != null && verificarReqPorAnular(dat_req))
		{
			DialogResult rpta = MessageBox.Show("Este pedido tiene Requerimientos de Almacen Activos.\nSeran Anulados, esta seguro de continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta == DialogResult.No)
			{
				return;
			}
			foreach (DataRow fila in dat_req.Rows)
			{
				int codEstado = Convert.ToInt32(fila.Field<object>("codEstado"));
				if (codEstado == 12)
				{
					continue;
				}
				int codReqAlm = Convert.ToInt32(fila.Field<object>("codReqAlmacen"));
				bool anular;
				if (codEstado == 7)
				{
					string anulado = "Requerimiento Anulado desde O.V N°: " + pedido.CodPedido;
					anular = admreqalm.anular(codReqAlm, frmLogin.iCodUser);
					DataTable listadoCodTrans = admreqalm.cargaTransferenciasPendientes(codReqAlm);
					if (listadoCodTrans != null)
					{
						foreach (DataRow fila2 in listadoCodTrans.Rows)
						{
							int codTransDir = Convert.ToInt32(fila2.Field<object>(0));
							admTransferencia.rechazado(codTransDir, "Transferencia Anulada por anulacion de Requerimiento de O.V N°: " + pedido.CodPedido);
						}
					}
					clsRequerimientoAlmacen req_alm = admreqalm.CargaRequerimiento(codReqAlm);
					req_alm.ListadoDetalle = admreqalm.CargaDetalleRequerimientoAlmacen(codReqAlm);
					foreach (clsDetalleRequerimientoAlmacen item in req_alm.ListadoDetalle)
					{
						admreqalm.retornarStock(req_alm.CodAlmacenDespacho, item.CodProducto, item.CodUnidad, item.CantidadPendienteAprobada, modificarStockActual: true, item.Codigo);
					}
					if (!anular)
					{
						MessageBox.Show("Ocurrio Un problema al anular.\nReq: " + codReqAlm, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					continue;
				}
				DataTable listadoCodTrans2 = admreqalm.cargaTransferenciasAprobadas(codReqAlm);
				if (listadoCodTrans2 == null)
				{
					continue;
				}
				if (listadoCodTrans2.Rows.Count > 1)
				{
					MessageBox.Show("Pedido No Anulado.\nUn Requerimiento de Almacen de Tipo Venta no puede tener mas de una Transferencia Activa.\ncodReq: " + codReqAlm, "Error Requerimiento No Anulado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					break;
				}
				if (listadoCodTrans2.Rows.Count != 1)
				{
					continue;
				}
				clsRequerimientoAlmacen req_alm2 = admreqalm.CargaRequerimiento(codReqAlm);
				anular = admreqalm.anular(codReqAlm, frmLogin.iCodUser);
				int codTransDir2 = Convert.ToInt32(listadoCodTrans2.Rows[0].Field<object>(0));
				clsAdmTipoDocumento admTipoDocumento = new clsAdmTipoDocumento();
				clsTipoDocumento tipodoc = admTipoDocumento.BuscaTipoDocumento("DET");
				clsTransferencia transf = admtrans.CargaTransferencia(codTransDir2);
				clsTransferencia extornacion = new clsTransferencia();
				extornacion.codTransAExtornar = codTransDir2;
				extornacion.CodAlmacenOrigen = req_alm2.CodAlmacenSolicitante;
				extornacion.CodAlmacenDestino = req_alm2.CodAlmacenDespacho;
				extornacion.codReqAlm = codReqAlm;
				extornacion.CodTipoDocumento = tipodoc.CodTipoDocumento;
				extornacion.FechaEnvio = DateTime.Now;
				extornacion.FechaEntrega = DateTime.Now;
				extornacion.FormaPago = 0;
				extornacion.FechaPago = DateTime.Now.Date;
				extornacion.CodListaPrecio = 0;
				string comentario = "Documento de Extornacion para Transf: " + codTransDir2;
				extornacion.Comentario = comentario;
				extornacion.DescripcionRechazo = "";
				extornacion.CodUser = frmLogin.iCodUser;
				extornacion.Estado = 1;
				extornacion.Codserie = transf.Codserie;
				extornacion.Serie = transf.Serie;
				extornacion.Numerodocumento = transf.Numerodocumento;
				extornacion.Moneda = 1;
				List<clsDetalleTransferencia> detalle = obtenerDetalleParaTransferencia(req_alm2, extornacion);
				if (detalle.Count <= 0 || !admTransferencia.insert(extornacion))
				{
					continue;
				}
				foreach (clsDetalleTransferencia det in detalle)
				{
					det.CodTransDir = Convert.ToInt32(extornacion.CodTransDir);
					admTransferencia.insertdetalle(det);
				}
				apruebaTransferencia(extornacion, detalle);
			}
		}
		if (AdmPedido.delete(Convert.ToInt32(pedido.CodPedido)))
		{
			MessageBox.Show("El pedido ha sido anulado correctamente", "Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CargaLista();
		}
	}

	private List<clsDetalleTransferencia> obtenerDetalleParaTransferencia(clsRequerimientoAlmacen req_alm, clsTransferencia extornacion)
	{
		List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();
		DataTable detalleTransf = admtrans.CargaDetalle(extornacion.codTransAExtornar);
		foreach (DataRow fila in detalleTransf.Rows)
		{
			clsDetalleTransferencia deta = new clsDetalleTransferencia();
			double _cantidad = Convert.ToDouble(fila.Field<object>("cantidad"));
			deta.CodProducto = Convert.ToInt32(fila.Field<object>("codProducto"));
			deta.CodAlmacenOrigen = req_alm.CodAlmacenSolicitante;
			deta.CodAlmacenDestino = req_alm.CodAlmacenDespacho;
			deta.UnidadIngresada = Convert.ToInt32(fila.Field<object>("codUnidadMedida"));
			deta.SerieLote = "";
			deta.Cantidad = _cantidad;
			deta.CantidadPendiente = _cantidad;
			double ult_pre = (deta.PrecioUnitario = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(deta.CodProducto, deta.UnidadIngresada, 0)));
			deta.Subtotal = ult_pre * deta.Cantidad;
			deta.Descuento1 = 0.0;
			deta.Descuento2 = 0.0;
			deta.Descuento3 = 0.0;
			deta.MontoDescuento = 0.0;
			bool flag = true;
			deta.PrecioVenta = deta.Subtotal;
			double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			deta.ValorVenta = deta.PrecioVenta / factorigv;
			deta.PrecioReal = deta.PrecioVenta / deta.Cantidad;
			deta.ValoReal = deta.ValorVenta / deta.Cantidad;
			deta.Igv = deta.PrecioVenta - deta.ValorVenta;
			deta.Importe = deta.Subtotal;
			deta.Valorpromedio = Convert.ToDecimal(deta.PrecioUnitario);
			deta.CodUser = frmLogin.iCodUser;
			detalle.Add(deta);
			extornacion.MontoBruto += Convert.ToDecimal(deta.Importe);
			extornacion.MontoDscto += Convert.ToDecimal(deta.MontoDescuento);
			extornacion.Igv += Convert.ToDecimal(deta.Igv);
			extornacion.Total += Convert.ToDecimal(deta.Subtotal);
		}
		return detalle;
	}

	private void apruebaTransferencia(clsTransferencia extornacion, List<clsDetalleTransferencia> detalle)
	{
		try
		{
			clsTipoDocumento doc = new clsTipoDocumento();
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			tran = AdmTran.MuestraTransaccion(15);
			doc = admtd.BuscaTipoDocumento("DET");
			NS.NumDoc = extornacion.Numerodocumento;
			NS.CodAlmacen = extornacion.CodAlmacenOrigen;
			NS.CodCliente = 0;
			NS.CodNotaCredito = 0;
			NS.CodSucursal = extornacion.CodAlmacenOrigen;
			NS.RazonSocialCliente = "";
			NS.CodAutorizado = 0;
			NS.FechaSalida = DateTime.Now.Date;
			NS.DocumentoReferencia = 0;
			NS.CodTipoTransaccion = tran.CodTransaccion;
			NS.CodTipoDocumento = doc.CodTipoDocumento;
			NS.CodSerie = extornacion.Codserie;
			NS.Serie = extornacion.Serie;
			NS.Moneda = 1;
			NS.FechaSalida = DateTime.Now.Date;
			NS.FormaPago = 0;
			NS.FechaPago = DateTime.Now.Date;
			NS.Comentario = "";
			NS.MontoBruto = Convert.ToDouble(extornacion.MontoBruto);
			NS.MontoDscto = 0.0;
			NS.Igv = 0.0;
			NS.Total = Convert.ToDouble(extornacion.Total);
			NS.CodUser = extornacion.CodUser;
			NS.Estado = 1;
			NS.Codtransferencia = Convert.ToInt32(extornacion.CodTransDir);
			using (TransactionScope Scope = new TransactionScope())
			{
				if (admNS.insert(NS))
				{
					RecorreDetalleNS(extornacion, detalle);
					if (detalleNS.Count > 0)
					{
						foreach (clsDetalleNotaSalida det in detalleNS)
						{
							if (!admNS.insertdetalle(det))
							{
								bandera = false;
								codproducto_error = det.CodProducto;
								Transaction.Current.Rollback();
								Scope.Dispose();
								break;
							}
						}
						if (bandera)
						{
							Scope.Complete();
							Scope.Dispose();
						}
					}
				}
				else
				{
					Transaction.Current.Rollback();
					Scope.Dispose();
					MessageBox.Show("Hubo un error al registrar la salida de producto ", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (bandera)
			{
				NI.NumDoc = NS.NumDoc;
				NI.CodAlmacen = extornacion.CodAlmacenDestino;
				NI.CodAutorizado = 0;
				NI.CodReferencia = 0;
				NI.CodTipoTransaccion = tran.CodTransaccion;
				NI.CodTipoDocumento = doc.CodTipoDocumento;
				NI.CodSerie = NS.CodSerie;
				NI.Serie = NS.Serie;
				NI.Moneda = 1;
				NI.FechaIngreso = DateTime.Now.Date;
				NI.FormaPago = 0;
				NI.FechaPago = DateTime.Now.Date;
				NI.Comentario = "";
				NS.MontoBruto = Convert.ToDouble(extornacion.MontoBruto);
				NI.MontoDscto = 0.0;
				NI.Igv = 0.0;
				NI.Total = Convert.ToDouble(extornacion.Total);
				NI.CodUser = extornacion.CodUser;
				NI.Estado = 1;
				NI.Codtransferencia = Convert.ToInt32(extornacion.CodTransDir);
				using (TransactionScope Scope2 = new TransactionScope())
				{
					if (admNI.insert(NI))
					{
						RecorreDetalleNI(extornacion, detalle);
						if (detalleNI.Count > 0)
						{
							foreach (clsDetalleNotaIngreso det2 in detalleNI)
							{
								if (!admNI.insertdetalle(det2))
								{
									bandera = false;
									codproducto_error = det2.CodProducto;
									Transaction.Current.Rollback();
									Scope2.Dispose();
									break;
								}
							}
						}
						if (bandera)
						{
							Scope2.Complete();
							Scope2.Dispose();
						}
					}
					else
					{
						Transaction.Current.Rollback();
						Scope2.Dispose();
						MessageBox.Show("Hubo un error al registrar el ingreso de productos", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				if (bandera)
				{
					admtrans.Aprobar(Convert.ToInt32(extornacion.CodTransDir));
				}
				else
				{
					MessageBox.Show("Hubo un error al guardar el Documento de Extornacion", "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Documento de Extornacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void RecorreDetalleNS(clsTransferencia extornacion, List<clsDetalleTransferencia> detalle)
	{
		detalleNS.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNS(row, extornacion);
		}
	}

	private void añadedetalleNS(clsDetalleTransferencia fila, clsTransferencia extornacion)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = fila.CodProducto;
		deta.CodNotaSalida = Convert.ToInt32(NS.CodNotaSalida);
		deta.CodAlmacen = extornacion.CodAlmacenOrigen;
		deta.UnidadIngresada = fila.UnidadIngresada;
		deta.SerieLote = "0";
		deta.Cantidad = fila.Cantidad;
		deta.PrecioUnitario = fila.PrecioUnitario;
		deta.Subtotal = fila.Subtotal;
		deta.Descuento1 = fila.Descuento1;
		deta.Descuento2 = fila.Descuento2;
		deta.Descuento3 = fila.Descuento3;
		deta.Igv = fila.Igv;
		deta.Importe = fila.PrecioVenta;
		deta.PrecioReal = fila.PrecioReal;
		deta.ValoReal = fila.ValoReal;
		deta.ValorPromedio = Convert.ToDouble(fila.Valorpromedio);
		deta.CodUser = frmLogin.iCodUser;
		detalleNS.Add(deta);
	}

	private void RecorreDetalleNI(clsTransferencia extornacion, List<clsDetalleTransferencia> detalle)
	{
		detalleNI.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNI(row, extornacion);
		}
	}

	private void añadedetalleNI(clsDetalleTransferencia fila, clsTransferencia extornacion)
	{
		clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
		deta1.CodProducto = fila.CodProducto;
		deta1.CodNotaIngreso = Convert.ToInt32(NI.CodNotaIngreso);
		deta1.CodAlmacen = extornacion.CodAlmacenDestino;
		deta1.UnidadIngresada = fila.UnidadIngresada;
		deta1.SerieLote = "0";
		deta1.Cantidad = fila.Cantidad;
		deta1.PrecioUnitario = fila.PrecioUnitario;
		deta1.Subtotal = fila.Subtotal;
		deta1.Descuento1 = fila.Descuento1;
		deta1.Descuento2 = fila.Descuento2;
		deta1.Descuento3 = fila.Descuento3;
		deta1.MontoDescuento = 0.0;
		deta1.ValoReal = deta1.PrecioUnitario / 1.18;
		deta1.Igv = deta1.ValoReal * 0.18;
		deta1.PrecioReal = deta1.ValoReal * 1.18;
		deta1.CodUser = frmLogin.iCodUser;
		deta1.Importe = deta1.PrecioUnitario * deta1.Cantidad;
		deta1.Subtotal = deta1.Importe;
		deta1.PrecioReal = fila.PrecioUnitario;
		deta1.ValoReal = fila.ValoReal;
		deta1.CodProveedor = 0;
		deta1.FechaIngreso = DateTime.Now;
		detalleNI.Add(deta1);
	}

	private bool verificarReqPorAnular(DataTable dat_req)
	{
		foreach (DataRow fila in dat_req.Rows)
		{
			if (Convert.ToInt32(fila.Field<object>("codEstado")) != 12)
			{
				return true;
			}
		}
		return false;
	}

	public void button1_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = string.Format("[{0}] like '*{1}*'", "codPedido", txtFiltro.Text);
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnIrNota_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null && pedido.CodPedido != "")
		{
			if (Application.OpenForms["frmOrdenVenta"] != null)
			{
				Application.OpenForms["frmOrdenVenta"].Close();
				return;
			}
			frmOrdenVenta form = new frmOrdenVenta();
			form.Proceso = 2;
			form.CodPedido = dgvPedidosPendientes.CurrentRow.Cells[codigo.Name].Value.ToString();
			form.Show();
		}
	}

	private void btnBusqueda_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null && pedido.CodPedido != "")
		{
			if (Application.OpenForms["frmRTransferencia"] != null)
			{
				Application.OpenForms["frmRTransferencia"].Close();
				return;
			}
			frmRTransferencia form = new frmRTransferencia();
			form.Proceso = 2;
			form.CodPedido = dgvPedidosPendientes.CurrentRow.Cells[codigo.Name].Value.ToString();
			form.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmPedidosPendientes));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnBusqueda = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.dgvPedidosPendientes = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Pedido_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
		this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
		this.btnIrNota = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btGenVenta = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPedidosPendientes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.btnBusqueda);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.dgvPedidosPendientes);
		this.groupBox1.Location = new System.Drawing.Point(0, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(878, 359);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Pendientes";
		this.btnBusqueda.ImageIndex = 11;
		this.btnBusqueda.ImageList = this.imageList2;
		this.btnBusqueda.Location = new System.Drawing.Point(366, 13);
		this.btnBusqueda.Name = "btnBusqueda";
		this.btnBusqueda.Size = new System.Drawing.Size(80, 33);
		this.btnBusqueda.TabIndex = 31;
		this.btnBusqueda.Text = "Buscar";
		this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBusqueda.UseVisualStyleBackColor = true;
		this.btnBusqueda.Click += new System.EventHandler(btnBusqueda_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList2.Images.SetKeyName(1, "Add.png");
		this.imageList2.Images.SetKeyName(2, "Remove.png");
		this.imageList2.Images.SetKeyName(3, "Write Document.png");
		this.imageList2.Images.SetKeyName(4, "New Document.png");
		this.imageList2.Images.SetKeyName(5, "Remove Document.png");
		this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList2.Images.SetKeyName(7, "document-print.png");
		this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList2.Images.SetKeyName(9, "refresh_256.png");
		this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList2.Images.SetKeyName(11, "search (1).png");
		this.imageList2.Images.SetKeyName(12, "search (5).png");
		this.imageList2.Images.SetKeyName(13, "search (6).png");
		this.imageList2.Images.SetKeyName(14, "search (8).png");
		this.imageList2.Images.SetKeyName(15, "search_top.png");
		this.imageList2.Images.SetKeyName(16, "folder_open (1).png");
		this.imageList2.Images.SetKeyName(17, "folder-open-icon.png");
		this.imageList2.Images.SetKeyName(18, "Glossy-Open-icon.png");
		this.imageList2.Images.SetKeyName(19, "Ocean Blue Open.png");
		this.imageList2.Images.SetKeyName(20, "Open (1).png");
		this.imageList2.Images.SetKeyName(21, "open_folder_green.png");
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(197, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(24, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(77, 19);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(100, 20);
		this.dtpDesde.TabIndex = 15;
		this.dtpDesde.ValueChanged += new System.EventHandler(dtpDesde_ValueChanged);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(244, 19);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(100, 20);
		this.dtpHasta.TabIndex = 14;
		this.dtpHasta.ValueChanged += new System.EventHandler(dtpHasta_ValueChanged);
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(645, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 8;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(527, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(101, 13);
		this.label10.TabIndex = 7;
		this.label10.Text = "Buscar Por Codigo :";
		this.dgvPedidosPendientes.AllowUserToAddRows = false;
		this.dgvPedidosPendientes.AllowUserToDeleteRows = false;
		this.dgvPedidosPendientes.AllowUserToOrderColumns = true;
		this.dgvPedidosPendientes.AllowUserToResizeRows = false;
		this.dgvPedidosPendientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvPedidosPendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvPedidosPendientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvPedidosPendientes.Columns.AddRange(this.codigo, this.Numeracion, this.Pedido_, this.RazonSocial, this.cliente, this.importe, this.fecha, this.documento, this.responsable);
		this.dgvPedidosPendientes.Location = new System.Drawing.Point(0, 56);
		this.dgvPedidosPendientes.MultiSelect = false;
		this.dgvPedidosPendientes.Name = "dgvPedidosPendientes";
		this.dgvPedidosPendientes.ReadOnly = true;
		this.dgvPedidosPendientes.RowHeadersVisible = false;
		this.dgvPedidosPendientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvPedidosPendientes.Size = new System.Drawing.Size(878, 300);
		this.dgvPedidosPendientes.TabIndex = 0;
		this.dgvPedidosPendientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPedidosPendientes_CellDoubleClick);
		this.dgvPedidosPendientes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvPedidosPendientes_RowStateChanged);
		this.codigo.DataPropertyName = "codPedido";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Visible = false;
		this.codigo.Width = 80;
		this.Numeracion.DataPropertyName = "numeracion";
		this.Numeracion.HeaderText = "Numeracion";
		this.Numeracion.Name = "Numeracion";
		this.Numeracion.ReadOnly = true;
		this.Numeracion.Visible = false;
		this.Pedido_.DataPropertyName = "pedido";
		this.Pedido_.HeaderText = "Pedido";
		this.Pedido_.Name = "Pedido_";
		this.Pedido_.ReadOnly = true;
		this.RazonSocial.DataPropertyName = "cliente";
		this.RazonSocial.HeaderText = "Cliente";
		this.RazonSocial.Name = "RazonSocial";
		this.RazonSocial.ReadOnly = true;
		this.RazonSocial.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.RazonSocial.Width = 400;
		this.cliente.DataPropertyName = "clientebole";
		this.cliente.HeaderText = "cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Visible = false;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle5;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.Width = 120;
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle6;
		this.documento.HeaderText = "T. Doc.";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Width = 60;
		this.responsable.DataPropertyName = "responsable";
		this.responsable.HeaderText = "Vendedor";
		this.responsable.Name = "responsable";
		this.responsable.ReadOnly = true;
		this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.responsable.Width = 250;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.buttonItem4.ImageIndex = 8;
		this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem4.Name = "buttonItem4";
		this.buttonItem4.SubItemsExpandWidth = 14;
		this.buttonItem4.Text = "Actualizar";
		this.buttonItem1.ImageIndex = 8;
		this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.buttonItem1.Name = "buttonItem1";
		this.buttonItem1.SubItemsExpandWidth = 14;
		this.buttonItem1.Text = "Actualizar";
		this.btnIrNota.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrNota.ImageIndex = 2;
		this.btnIrNota.ImageList = this.imageList1;
		this.btnIrNota.Location = new System.Drawing.Point(582, 366);
		this.btnIrNota.Name = "btnIrNota";
		this.btnIrNota.Size = new System.Drawing.Size(101, 37);
		this.btnIrNota.TabIndex = 4;
		this.btnIrNota.Text = "Editar Pedido";
		this.btnIrNota.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrNota.UseVisualStyleBackColor = true;
		this.btnIrNota.Visible = false;
		this.btnIrNota.Click += new System.EventHandler(btnIrNota_Click);
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button1.ImageIndex = 10;
		this.button1.ImageList = this.imageList2;
		this.button1.Location = new System.Drawing.Point(121, 366);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(107, 37);
		this.button1.TabIndex = 4;
		this.button1.Text = "Actualizar Pedidos";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(19, 366);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(96, 37);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular Pedido";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btGenVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btGenVenta.ImageIndex = 2;
		this.btGenVenta.ImageList = this.imageList1;
		this.btGenVenta.Location = new System.Drawing.Point(689, 366);
		this.btGenVenta.Name = "btGenVenta";
		this.btGenVenta.Size = new System.Drawing.Size(96, 37);
		this.btGenVenta.TabIndex = 3;
		this.btGenVenta.Text = "Generar Venta";
		this.btGenVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btGenVenta.UseVisualStyleBackColor = true;
		this.btGenVenta.Visible = false;
		this.btGenVenta.Click += new System.EventHandler(btGenVenta_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(791, 366);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(75, 37);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(878, 415);
		base.Controls.Add(this.btnIrNota);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.btnAnular);
		base.Controls.Add(this.btGenVenta);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmPedidosPendientes";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Pedidos Pendientes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPedidosPendientes_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvPedidosPendientes).EndInit();
		base.ResumeLayout(false);
	}
}
