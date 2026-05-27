using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class FrmTPenPedido : Office2007Form
{
	public int CodCliente;

	public int CodDocumento;

	public int Tipo;

	private clsAdmTransferencia admtrans = new clsAdmTransferencia();

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsTransferencia trans = new clsTransferencia();

	private clsAdmSerie admSerie = new clsAdmSerie();

	private clsTransferencia extornacion = new clsTransferencia();

	private List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();

	public int Proceso = 0;

	private clsAdmAlmacen admalm = new clsAdmAlmacen();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsSerie ser = null;

	private bool bandera = true;

	private int codproducto_error = 0;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	public List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public string NombreCliente;

	public string CodPedido;

	public string NombreAlmacen;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private DataGridView dgvTransferenciasPendientes;

	private Button btnEliminar;

	private Button btnsalir;

	private Button btnnuevo;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn numerodoc;

	private DataGridViewTextBoxColumn colEstado;

	private DataGridViewTextBoxColumn colCodEstado;

	private DataGridViewTextBoxColumn AlmacenO;

	private DataGridViewTextBoxColumn AlmacenOrigen;

	private DataGridViewTextBoxColumn codAlmacenDestino;

	private DataGridViewTextBoxColumn almacendestino;

	private DataGridViewTextBoxColumn DecripcionRechazo;

	private Button btnEditar;

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void cargarlista()
	{
		try
		{
			DataTable newData = new DataTable();
			dgvTransferenciasPendientes.DataSource = admreqalm.CargaRequerimientosSegunPedido(Convert.ToInt32(CodPedido));
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnnuevo_Click(object sender, EventArgs e)
	{
		if (!(CodPedido != ""))
		{
			return;
		}
		DataTable almacenesOV = admalm.MuestraAlmacenesEmp2(int.Parse(CodPedido));
		DataTable almacenesConRA = admalm.MuestraAlmacenesConRA(Convert.ToInt32(CodPedido));
		List<int> listaCodAlmacenesConRA = (from x in almacenesConRA.AsEnumerable()
			select Convert.ToInt32(x.Field<object>("codAlmacenSolicitante").ToString())).ToList();
		List<DataRow> almacenesDispParaRA = (from x in almacenesOV.AsEnumerable()
			where !listaCodAlmacenesConRA.Contains(Convert.ToInt32(x.Field<object>("codAlmacen").ToString()))
			select x).ToList();
		if (almacenesDispParaRA.Count > 0)
		{
			if (Application.OpenForms["frmReqAlmacen"] != null)
			{
				Application.OpenForms["frmReqAlmacen"].Close();
			}
			frmReqAlmacen form = new frmReqAlmacen();
			form.TipoReq = 2;
			form.CodPedido = CodPedido;
			form.ventanaListaReqVentas = this;
			form.WindowState = FormWindowState.Maximized;
			DialogResult rpta = form.ShowDialog();
			if (rpta == DialogResult.Yes)
			{
				frmReqAlmacen form2 = new frmReqAlmacen();
				form2.TipoReq = 2;
				form2.codRequerimientoAlmacen = form.codRequerimientoAlmacen;
				form2.ventanaListaReqVentas = this;
				form2.Proceso = 1;
				form2.bandEditar = true;
				form2.WindowState = FormWindowState.Maximized;
				form2.ShowDialog();
			}
		}
		else
		{
			MessageBox.Show("No hay almacen disponible para crear requerimiento tipo venta", "Error De Creacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void FrmTPenPedido_Load(object sender, EventArgs e)
	{
		DataTable almacenesOV = admalm.MuestraAlmacenesEmp2(int.Parse(CodPedido));
		DataTable almacenesConRA = admalm.MuestraAlmacenesConRA(Convert.ToInt32(CodPedido));
		int a = 0;
		List<int> listaCodAlmacenesConRA = (from x in almacenesConRA.AsEnumerable()
			select Convert.ToInt32(x.Field<object>("codAlmacenSolicitante").ToString())).ToList();
		List<DataRow> almacenesDispParaRA = (from x in almacenesOV.AsEnumerable()
			where !listaCodAlmacenesConRA.Contains(Convert.ToInt32(x.Field<object>("codAlmacen").ToString()))
			select x).ToList();
		btnnuevo.Visible = almacenesDispParaRA.Count > 0;
		dgvTransferenciasPendientes.AutoGenerateColumns = false;
		cargarlista();
	}

	private void dgvTransferenciasPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvTransferenciasPendientes.Rows.Count <= 0 || e.RowIndex == -1)
		{
			return;
		}
		int codEstado = Convert.ToInt32(dgvTransferenciasPendientes.CurrentRow.Cells[colCodEstado.Name].Value);
		int codReqAlm = Convert.ToInt32(dgvTransferenciasPendientes.CurrentRow.Cells[codigo.Name].Value);
		if (codEstado != 8)
		{
			if (Application.OpenForms["frmReqAlmacen"] != null)
			{
				Application.OpenForms["frmReqAlmacen"].Close();
				return;
			}
			frmReqAlmacen form = new frmReqAlmacen();
			form.TipoReq = 2;
			form.bandEditar = codEstado == 7;
			form.Proceso = ((codEstado == 7) ? 1 : 2);
			form.CodPedido = CodPedido;
			form.codRequerimientoAlmacen = codReqAlm;
			form.MdiParent = base.MdiParent;
			form.ventanaListaReqVentas = this;
			form.WindowState = FormWindowState.Maximized;
			form.Show();
		}
		else
		{
			MessageBox.Show("No se puede abrir este req de almacen.\nDatos:\nCod Req: " + codReqAlm + " \tEstado: " + codEstado, "Requerimiento de Almacen", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void dgvTransferenciasPendientes_DoubleClick(object sender, EventArgs e)
	{
	}

	private void dgvTransferenciasPendientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	public FrmTPenPedido()
	{
		InitializeComponent();
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		clsAdmTransferencia admTransferencia = new clsAdmTransferencia();
		if (dgvTransferenciasPendientes.Rows.Count > 0)
		{
			int codEstado = Convert.ToInt32(dgvTransferenciasPendientes.CurrentRow.Cells[colCodEstado.Name].Value);
			int codReqAlm = Convert.ToInt32(dgvTransferenciasPendientes.CurrentRow.Cells[codigo.Name].Value);
			if (codEstado != 12)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el Requerimiento para Venta seleccionada", "Requerimiento para Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult == DialogResult.No)
				{
					return;
				}
				bool anular;
				if (codEstado == 7)
				{
					string anulado = "Requerimiento Anulado desde O.V N°: " + CodPedido;
					anular = admreqalm.anular(codReqAlm, frmLogin.iCodUser);
					DataTable listadoCodTrans = admreqalm.cargaTransferenciasPendientes(codReqAlm);
					if (listadoCodTrans != null)
					{
						foreach (DataRow fila in listadoCodTrans.Rows)
						{
							int codTransDir = Convert.ToInt32(fila.Field<object>(0));
							admTransferencia.rechazado(codTransDir, "Transferencia Anulada por anulacion de Requerimiento de O.V N°: " + CodPedido);
						}
					}
					clsRequerimientoAlmacen req_alm = admreqalm.CargaRequerimiento(codReqAlm);
					req_alm.ListadoDetalle = admreqalm.CargaDetalleRequerimientoAlmacen(codReqAlm);
					foreach (clsDetalleRequerimientoAlmacen item in req_alm.ListadoDetalle)
					{
						admreqalm.retornarStock(req_alm.CodAlmacenDespacho, item.CodProducto, item.CodUnidad, item.CantidadPendienteAprobada, modificarStockActual: true, item.Codigo);
					}
					if (anular)
					{
						MessageBox.Show("Se Anulo Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						btnnuevo.Visible = true;
						cargarlista();
					}
					else
					{
						MessageBox.Show("Ocurrio Un problema al anular", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					return;
				}
				anular = admreqalm.anular(codReqAlm, frmLogin.iCodUser);
				DataTable listadoCodTrans2 = admreqalm.cargaTransferenciasAprobadas(codReqAlm);
				if (listadoCodTrans2 != null)
				{
					if (listadoCodTrans2.Rows.Count > 1)
					{
						MessageBox.Show("Un Requerimiento de Almacen de Tipo Venta no puede tener mas de una Transferencia Activa.\ncodReq: " + codReqAlm, "Error Requerimiento No Anulado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						cargarlista();
						return;
					}
					if (listadoCodTrans2.Rows.Count == 1)
					{
						clsRequerimientoAlmacen req_alm2 = admreqalm.CargaRequerimiento(codReqAlm);
						int codTransDir2 = Convert.ToInt32(listadoCodTrans2.Rows[0].Field<object>(0));
						clsAdmTipoDocumento admTipoDocumento = new clsAdmTipoDocumento();
						clsTipoDocumento tipodoc = admTipoDocumento.BuscaTipoDocumento("DET");
						clsTransferencia transf = admtrans.CargaTransferencia(codTransDir2);
						extornacion = new clsTransferencia();
						extornacion.codTransAExtornar = codTransDir2;
						extornacion.CodAlmacenOrigen = req_alm2.CodAlmacenSolicitante;
						extornacion.CodAlmacenDestino = req_alm2.CodAlmacenDespacho;
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
						obtenerDetalleParaTransferencia(req_alm2);
						if (detalle.Count > 0 && admTransferencia.insert(extornacion))
						{
							foreach (clsDetalleTransferencia det in detalle)
							{
								det.CodTransDir = Convert.ToInt32(extornacion.CodTransDir);
								admTransferencia.insertdetalle(det);
							}
							apruebaTransferencia(extornacion);
						}
					}
				}
				if (anular)
				{
					MessageBox.Show("Se Anulo Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					btnnuevo.Visible = true;
					cargarlista();
				}
				else
				{
					MessageBox.Show("Ocurrio Un problema al anular", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("El Requerimiento Ya Esta Anulado", "Requerimiento Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				btnnuevo.Visible = true;
			}
		}
		else
		{
			MessageBox.Show("No se puede anular el Requerimiento", "Requerimiento Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
	}

	private void apruebaTransferencia(clsTransferencia transfer)
	{
		try
		{
			clsTipoDocumento doc = new clsTipoDocumento();
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			tran = AdmTran.MuestraTransaccion(15);
			doc = admtd.BuscaTipoDocumento("DET");
			NS.NumDoc = transfer.Numerodocumento;
			NS.CodAlmacen = transfer.CodAlmacenOrigen;
			NS.CodCliente = 0;
			NS.CodNotaCredito = 0;
			NS.CodSucursal = transfer.CodAlmacenOrigen;
			NS.RazonSocialCliente = "";
			NS.CodAutorizado = 0;
			NS.FechaSalida = DateTime.Now.Date;
			NS.DocumentoReferencia = 0;
			NS.CodTipoTransaccion = tran.CodTransaccion;
			NS.CodTipoDocumento = doc.CodTipoDocumento;
			NS.CodSerie = transfer.Codserie;
			NS.Serie = transfer.Serie;
			NS.Moneda = 1;
			NS.FechaSalida = DateTime.Now.Date;
			NS.FormaPago = 0;
			NS.FechaPago = DateTime.Now.Date;
			NS.Comentario = "";
			NS.MontoBruto = Convert.ToDouble(transfer.MontoBruto);
			NS.MontoDscto = 0.0;
			NS.Igv = 0.0;
			NS.Total = Convert.ToDouble(transfer.Total);
			NS.CodUser = transfer.CodUser;
			NS.Estado = 1;
			NS.Codtransferencia = Convert.ToInt32(transfer.CodTransDir);
			using (TransactionScope Scope = new TransactionScope())
			{
				if (admNS.insert(NS))
				{
					RecorreDetalleNS();
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
				NI.CodAlmacen = transfer.CodAlmacenDestino;
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
				NS.MontoBruto = Convert.ToDouble(transfer.MontoBruto);
				NI.MontoDscto = 0.0;
				NI.Igv = 0.0;
				NI.Total = Convert.ToDouble(transfer.Total);
				NI.CodUser = transfer.CodUser;
				NI.Estado = 1;
				NI.Codtransferencia = Convert.ToInt32(transfer.CodTransDir);
				using (TransactionScope Scope2 = new TransactionScope())
				{
					if (admNI.insert(NI))
					{
						RecorreDetalleNI();
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
					admtrans.Aprobar(Convert.ToInt32(transfer.CodTransDir));
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

	private void RecorreDetalleNS()
	{
		detalleNS.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNS(row);
		}
	}

	private void añadedetalleNS(clsDetalleTransferencia fila)
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

	private void RecorreDetalleNI()
	{
		detalleNI.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNI(row);
		}
	}

	private void añadedetalleNI(clsDetalleTransferencia fila)
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

	private void obtenerDetalleParaTransferencia(clsRequerimientoAlmacen req_alm)
	{
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
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnsalir = new System.Windows.Forms.Button();
		this.btnnuevo = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.dgvTransferenciasPendientes = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.numerodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.AlmacenO = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.AlmacenOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codAlmacenDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.almacendestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DecripcionRechazo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnEditar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransferenciasPendientes).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.btnEditar);
		this.groupBox1.Controls.Add(this.btnsalir);
		this.groupBox1.Controls.Add(this.btnnuevo);
		this.groupBox1.Controls.Add(this.btnEliminar);
		this.groupBox1.Controls.Add(this.dgvTransferenciasPendientes);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Location = new System.Drawing.Point(2, -1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(693, 376);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.btnsalir.BackColor = System.Drawing.Color.White;
		this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnsalir.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnsalir.Location = new System.Drawing.Point(576, 330);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(105, 38);
		this.btnsalir.TabIndex = 4;
		this.btnsalir.Text = "Salir";
		this.btnsalir.UseVisualStyleBackColor = false;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.btnnuevo.BackColor = System.Drawing.SystemColors.MenuHighlight;
		this.btnnuevo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnnuevo.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnnuevo.ForeColor = System.Drawing.Color.White;
		this.btnnuevo.Location = new System.Drawing.Point(465, 330);
		this.btnnuevo.Name = "btnnuevo";
		this.btnnuevo.Size = new System.Drawing.Size(105, 38);
		this.btnnuevo.TabIndex = 3;
		this.btnnuevo.Text = "Nuevo";
		this.btnnuevo.UseVisualStyleBackColor = false;
		this.btnnuevo.Click += new System.EventHandler(btnnuevo_Click);
		this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
		this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnEliminar.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnEliminar.ForeColor = System.Drawing.Color.DarkRed;
		this.btnEliminar.Location = new System.Drawing.Point(6, 330);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(98, 38);
		this.btnEliminar.TabIndex = 2;
		this.btnEliminar.Text = "Anular";
		this.btnEliminar.UseVisualStyleBackColor = false;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.dgvTransferenciasPendientes.AllowUserToAddRows = false;
		this.dgvTransferenciasPendientes.AllowUserToDeleteRows = false;
		this.dgvTransferenciasPendientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvTransferenciasPendientes.BackgroundColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.dgvTransferenciasPendientes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.DeepSkyBlue;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Modern No. 20", 8.999999f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvTransferenciasPendientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvTransferenciasPendientes.ColumnHeadersHeight = 26;
		this.dgvTransferenciasPendientes.Columns.AddRange(this.codigo, this.numerodoc, this.colEstado, this.colCodEstado, this.AlmacenO, this.AlmacenOrigen, this.codAlmacenDestino, this.almacendestino, this.DecripcionRechazo);
		this.dgvTransferenciasPendientes.EnableHeadersVisualStyles = false;
		this.dgvTransferenciasPendientes.Location = new System.Drawing.Point(6, 52);
		this.dgvTransferenciasPendientes.Name = "dgvTransferenciasPendientes";
		this.dgvTransferenciasPendientes.ReadOnly = true;
		this.dgvTransferenciasPendientes.RowHeadersVisible = false;
		this.dgvTransferenciasPendientes.Size = new System.Drawing.Size(675, 258);
		this.dgvTransferenciasPendientes.TabIndex = 1;
		this.dgvTransferenciasPendientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransferenciasPendientes_CellContentClick);
		this.dgvTransferenciasPendientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransferenciasPendientes_CellDoubleClick);
		this.codigo.DataPropertyName = "codReqAlmacen";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.numerodoc.DataPropertyName = "docReqAlmacen";
		this.numerodoc.HeaderText = "Nro. Documento";
		this.numerodoc.Name = "numerodoc";
		this.numerodoc.ReadOnly = true;
		this.colEstado.DataPropertyName = "descEstado";
		this.colEstado.HeaderText = "Estado";
		this.colEstado.Name = "colEstado";
		this.colEstado.ReadOnly = true;
		this.colCodEstado.DataPropertyName = "codEstado";
		this.colCodEstado.HeaderText = "codEstado";
		this.colCodEstado.Name = "colCodEstado";
		this.colCodEstado.ReadOnly = true;
		this.colCodEstado.Visible = false;
		this.AlmacenO.DataPropertyName = "codAlmacenSolicitante";
		this.AlmacenO.HeaderText = "CodAlmacenOrigen";
		this.AlmacenO.Name = "AlmacenO";
		this.AlmacenO.ReadOnly = true;
		this.AlmacenO.Visible = false;
		this.AlmacenOrigen.DataPropertyName = "descAlmacenSolicitante";
		this.AlmacenOrigen.HeaderText = "Almacen Solicitante";
		this.AlmacenOrigen.Name = "AlmacenOrigen";
		this.AlmacenOrigen.ReadOnly = true;
		this.codAlmacenDestino.DataPropertyName = "codAlmacenDespachador";
		this.codAlmacenDestino.HeaderText = "codAlmacenDestino";
		this.codAlmacenDestino.Name = "codAlmacenDestino";
		this.codAlmacenDestino.ReadOnly = true;
		this.codAlmacenDestino.Visible = false;
		this.almacendestino.DataPropertyName = "descAlmacenDespachador";
		this.almacendestino.HeaderText = "Almacen Despachador";
		this.almacendestino.Name = "almacendestino";
		this.almacendestino.ReadOnly = true;
		this.DecripcionRechazo.DataPropertyName = "comentarioDespacho";
		this.DecripcionRechazo.HeaderText = "DecripcionRechazo";
		this.DecripcionRechazo.Name = "DecripcionRechazo";
		this.DecripcionRechazo.ReadOnly = true;
		this.groupBox2.Location = new System.Drawing.Point(4, 10);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(677, 36);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "REQUERIMIENTOS DE PEDIDO";
		this.btnEditar.BackColor = System.Drawing.Color.LawnGreen;
		this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnEditar.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnEditar.ForeColor = System.Drawing.Color.Black;
		this.btnEditar.Location = new System.Drawing.Point(354, 330);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(105, 38);
		this.btnEditar.TabIndex = 5;
		this.btnEditar.Text = "Editar";
		this.btnEditar.UseVisualStyleBackColor = false;
		this.btnEditar.Visible = false;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(701, 383);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "FrmTPenPedido";
		this.Text = "LISTA DE REQUERIMIENTOS";
		base.Load += new System.EventHandler(FrmTPenPedido_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvTransferenciasPendientes).EndInit();
		base.ResumeLayout(false);
	}
}
