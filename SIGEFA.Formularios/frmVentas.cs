using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using DocumentFormat.OpenXml.Spreadsheet;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.SunatFacElec;
using SpreadsheetLight;
using Telerik.WinControls.Export;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;
using Telerik.WinControls.UI.Export;
using WinApp.Comun.Dto.Intercambio;
using WinApp.Firmado;
using WinApp.Servicio;
using WinApp.Servicio.Soap;

namespace SIGEFA.Formularios;

public class frmVentas : Office2007Form
{
	private clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsNotaIngreso nota2 = new clsNotaIngreso();

	private clsAdmNotaCredito admnc = new clsAdmNotaCredito();

	private clsTransaccion trans = new clsTransaccion();

	private clsSerie ser = new clsSerie();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsAdmTransaccion admTrans = new clsAdmTransaccion();

	private clsAdmNotaIngreso AdmIngreso = new clsAdmNotaIngreso();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsParametro param = new clsParametro();

	private clsPago pag = new clsPago();

	private clsAdmPago admPago = new clsAdmPago();

	private DataTable dt_AnulaVenta = new DataTable();

	private DataTable dt_AnulaPago = new DataTable();

	private DataTable dt_tot_nota_Credito = new DataTable();

	public int Proceso = 0;

	private List<clsDetalleNotaIngreso> lstNotaIng = new List<clsDetalleNotaIngreso>();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private clsAdmParametro admParam = new clsAdmParametro();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmGuiaFacturacion AdmGuiaFacturacion = new clsAdmGuiaFacturacion();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsCliente cli = new clsCliente();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private DataTable detalleTableVenta;

	private DataTable detalleNC;

	public List<clsDetalleFacturaVenta> ListaDetalleVenta = new List<clsDetalleFacturaVenta>();

	public List<clsDetalleNotaCredito> listadetalleNC = new List<clsDetalleNotaCredito>();

	private clsNotaCredito nc;

	private clsAdmDocumentoIdentidad AdmDocumentoIdentidad = new clsAdmDocumentoIdentidad();

	private clsFacturaVenta ventaConsultada;

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private clsAdmSerie admSerie = new clsAdmSerie();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private clsAdmDespacho admdespacho = new clsAdmDespacho();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private bool bandera = true;

	private int codproducto_error = 0;

	private List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	private List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	internal clsUsuario usuario_click = null;

	private List<clsRepositorio> lista_repositorio = new List<clsRepositorio>();

	private clsAdmRepositorio clsadmrepo = new clsAdmRepositorio();

	private string tipoDocumento = "";

	private clsCaja Caja = new clsCaja();

	private clsAdmAperturaCierre AdmCaja = new clsAdmAperturaCierre();

	private clsAdmTarjetaPago admtarjeta = new clsAdmTarjetaPago();

	private string nombreDocumento = "";

	private int NumeroDocumento;

	private Facturacion facturacion = new Facturacion();

	private decimal totalVF = default(decimal);

	private decimal totalVT = default(decimal);

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvVentas;

	private Button btnSalir;

	private Button btnIrPedido;

	private ImageList imageList1;

	private Button btnAnular;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnReporte;

	private Button button1;

	private ImageList imageList2;

	private Button btnVistaSucursales;

	public Button btnGuardar;

	public Label lbfactbol;

	private Label label1;

	private Label label2;

	private Label label3;

	private RadDropDownList cmbAlmacenes;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewAutoFilterTextBoxColumn fecha;

	private DataGridViewAutoFilterTextBoxColumn documento;

	private DataGridViewTextBoxColumn numdoc;

	private DataGridViewAutoFilterTextBoxColumn codcliente;

	private DataGridViewTextBoxColumn cliente;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewAutoFilterTextBoxColumn formapago;

	private DataGridViewTextBoxColumn fechapago;

	private DataGridViewAutoFilterTextBoxColumn estado;

	private DataGridViewAutoFilterTextBoxColumn impreso;

	private DataGridViewTextBoxColumn NotaCredito;

	private DataGridViewTextBoxColumn enviadoS;

	private DataGridViewTextBoxColumn codTipoDocumento;

	private RadGridView dgvVentas1;

	private MaterialTheme materialTheme1;

	private Label lblicbper;

	private Label label7;

	private Button button2;

	private Button button3;

	private Button button4;

	private Label label4;

	private Label label8;

	private GroupBox groupBox2;

	private Label txtNombreProducto;

	private Label label9;

	private Label label10;

	private TextBox txtCodprod;

	private Button btnPdf;

	private Button btnXML;

	private Panel panel3;

	private Label label18;

	private Label lblNC;

	private Label label17;

	private Label lblPendiente;

	private Label lblVC;

	private Label label19;

	private Label label11;

	private Label label15;

	private Label lblCajaSeparacion;

	private Label label12;

	private Label lbCheque;

	private Label lbDeposito;

	private Label label13;

	private Label label14;

	private Label lblSaldoCaja;

	private Label label21;

	private Panel panel1;

	private Label label16;

	private Label lblNotaCredito;

	private Label label22;

	private Label lblPendientes;

	private Label lblVentaCredito;

	private Label label25;

	private Label label26;

	private Label label27;

	private Label lblTarjeta;

	private Label label29;

	private Label lblTransferencia;

	private Label lblDeposito;

	private Label label32;

	private Label label33;

	private Label lblEfectivo;

	private Label label35;

	private Label label23;

	private Label label20;

	private CheckBox chbverificarventas;

	private Button btnguiafacturacion;

	public byte[] firmadigital { get; set; }

	public frmVentas()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
		Dispose();
	}

	private void CargaLista()
	{
		try
		{
			int codProducto = 0;
			int verifica = 0;
			int.TryParse(txtCodprod.Text, out codProducto);
			verifica = (chbverificarventas.Checked ? 1 : 0);
			dt_tot_nota_Credito = AdmVenta.Ventas(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal, verifica, codProducto);
			dgvVentas1.DataSource = dt_tot_nota_Credito;
			ActualizaTotales();
			VerificaSaldoCajaVentas();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void ActualizaTotales()
	{
		totalVF = default(decimal);
		totalVT = default(decimal);
		decimal tot_icbper = default(decimal);
		foreach (GridViewRowInfo row in dgvVentas1.Rows)
		{
			if (Convert.ToString(row.Cells["estado"].Value) == "ANULADO")
			{
				totalVT += Convert.ToDecimal(row.Cells["importe"].Value);
			}
			else
			{
				totalVF += Convert.ToDecimal(row.Cells["importe"].Value);
			}
			tot_icbper += Convert.ToDecimal(row.Cells["icbper"].Value);
		}
		double total_nota_credito = AdmVenta.getTotalNotaCreditoSegunFechayAlmacen(Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value, frmLogin.iCodSucursal);
		double total_nc = ((total_nota_credito != double.NaN) ? total_nota_credito : 0.0);
		lbfactbol.Text = $"{totalVF:#,##0.00}";
		label3.Text = $"{totalVT:#,##0.00}";
		lblicbper.Text = $"{tot_icbper:#,##0.00}";
		label4.Text = $"{total_nc:#,##0.00}";
	}

	private void btnIrPedido_Click(object sender, EventArgs e)
	{
		if (dgvVentas1.Rows.Count >= 1 && dgvVentas1.CurrentRow != null && dgvVentas1.Rows.Count >= 1)
		{
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.CodVenta = venta.CodFacturaVenta;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void frmPedidosPendientes_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		CargaLista();
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		int permiso = new clsAdmFormulario().getPermisoAnularVentas();
		List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		bool band = accesos.Contains(permiso) || frmLogin.iNivelUser == 1;
		btnAnular.Visible = band;
	}

	public void cargaAlmacenes()
	{
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
	}

	private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (dgvVentas1.Rows.Count >= 1 && e.Row.Selected)
		{
			venta.CodFacturaVenta = e.Row.Cells[codigo.Name].Value.ToString();
		}
	}

	private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvVentas1.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.CodVenta = venta.CodFacturaVenta;
			form.Proceso = 3;
			form.Show();
		}
	}

	private void LeeProductos()
	{
		try
		{
			lstNotaIng.Clear();
			foreach (DataRow row3 in dt_AnulaVenta.Rows)
			{
				clsDetalleNotaIngreso DetIng = new clsDetalleNotaIngreso();
				DetIng.CodProducto = Convert.ToInt32(row3[1]);
				DetIng.CodNotaIngreso = Convert.ToInt32(nota2.CodNotaIngreso);
				DetIng.CodAlmacen = Convert.ToInt32(row3[3]);
				DetIng.UnidadIngresada = Convert.ToInt32(row3[4]);
				DetIng.Cantidad = Convert.ToDouble(row3[6]);
				DetIng.PrecioUnitario = Convert.ToDouble(row3[29]);
				DetIng.Descuento1 = Convert.ToDouble(row3[9]);
				DetIng.Descuento2 = Convert.ToDouble(row3[10]);
				DetIng.Descuento3 = Convert.ToDouble(row3[11]);
				DetIng.MontoDescuento = Convert.ToDouble(row3[12]);
				DetIng.Importe = Convert.ToDouble(row3[29]) * DetIng.Cantidad;
				DetIng.Subtotal = DetIng.PrecioUnitario * DetIng.Cantidad;
				DetIng.Igv = DetIng.Importe - DetIng.Subtotal;
				DetIng.PrecioReal = Convert.ToDouble(row3[29]) * Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				DetIng.ValoReal = Convert.ToDouble(row3[29]);
				DetIng.CodUser = Convert.ToInt32(row3[17]);
				DetIng.Estado = true;
				lstNotaIng.Add(DetIng);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbAlmacenes.SelectedValue) != 0)
		{
			if (dgvVentas1.Rows.Count >= 1 && Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value) > 0)
			{
				int codFacturaVenta = Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value);
				venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
				if (venta.Anulado == 1)
				{
					MessageBox.Show("Factura Venta ya se encuentra Anulada", "Venta Anulada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (venta.TieneNotaCredito != "N")
				{
					MessageBox.Show("Factura Venta ya tiene una Nota de Credito registrada", "Venta Anulada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (Convert.ToString(dgvVentas1.CurrentRow.Cells["enviadoS"].Value) == "Enviado" || (venta.CodTipoDocumento == 8 && venta.FechaRegistro.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd")))
				{
					if (Application.OpenForms["frmNotadeCredito"] != null)
					{
						Application.OpenForms["frmNotadeCredito"].Activate();
					}
					else
					{
						frmNotadeCredito form = new frmNotadeCredito();
						form.MdiParent = base.MdiParent;
						form.Proceso = 7;
						form.CodNotaS = codFacturaVenta;
						form.Show();
					}
				}
				else
				{
					usuario_click = null;
					frmAutorizacion frm = new frmAutorizacion();
					frm.tipoAccion = 2;
					int codPermiso = new clsAdmFormulario().getPermisoAnularVentas();
					frm.permiso = codPermiso;
					frm.PermitirAdministradores = true;
					frm.tipoVentanaAAsignarUsuario = 6;
					frm.ventanaVentas = this;
					DialogResult dr = frm.ShowDialog();
					if (dr == DialogResult.OK && usuario_click != null)
					{
						switch (admdespacho.VerificaEntregasActivasRespectoADespacho(1, venta.CodFacturaVenta))
						{
						case 1:
							MessageBox.Show("La Venta: " + dgvVentas1.CurrentRow.Cells["numdoc"].Value.ToString() + " no se puede ANULAR, por que tiene entregas de despacho activas", "Venta tiene Entregas de Desacho", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						case -1:
							MessageBox.Show("La Venta: " + dgvVentas1.CurrentRow.Cells["numdoc"].Value.ToString() + " no se puede ANULAR, por que ocurrio un error al intentar verificar entregas de despacho activas.", "Error en Verificacion de Entregas de Desacho", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						if (!AdmVenta.ValidaAnulacionVenta(Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value)))
						{
							bool bandera = false;
							using (TransactionScope Scope = new TransactionScope())
							{
								if (!(bandera = AdmVenta.anular(Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value), usuario_click.CodUsuario)))
								{
									Transaction.Current.Rollback();
									Scope.Dispose();
									MessageBox.Show("La Venta No Ah Sido Anulada", "Anulacion Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									return;
								}
								MessageBox.Show("El documento ha sido anulado correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								DataTable dtPagos = admPago.GetPagosVenta(Convert.ToInt32(cmbAlmacenes.SelectedValue), Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value));
								foreach (DataRow fila in dtPagos.Rows)
								{
									bandera = admPago.AnularPago(Convert.ToInt32(fila[0]));
									if (!bandera)
									{
										MessageBox.Show("No se pudo anular el pago especificado.\nCod Pago: " + Convert.ToInt32(fila[0]), "Anulacion de pago incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										Transaction.Current.Rollback();
										Scope.Dispose();
										return;
									}
								}
								int codNotaSalida = 0;
								nota2 = AdmVenta.BuscaNotaSalida(Convert.ToInt32(venta.CodFacturaVenta), Convert.ToInt32(cmbAlmacenes.SelectedValue));
								if (nota2 == null)
								{
									MessageBox.Show("Error al consultar Venta", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									Transaction.Current.Rollback();
									Scope.Dispose();
									return;
								}
								codNotaSalida = Convert.ToInt32(nota2.CodNotaIngreso);
								trans = admTrans.MuestraTransaccion(11);
								nota2.CodTipoTransaccion = trans.CodTransaccion;
								doc = Admdoc.BuscaTipoDocumento("DIA");
								ser = Admser.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
								nota2.Serie = nota2.Serie;
								nota2.NumDoc = Convert.ToString(nota2.NumDoc);
								nota2.DescripcionTransaccion = trans.Descripcion;
								nota2.CodTipoDocumento = doc.CodTipoDocumento;
								nota2.CodSerie = ser.CodSerie;
								nota2.CodReferencia = nota2.DocumentoReferencia;
								if (!AdmIngreso.insert(nota2))
								{
									MessageBox.Show("No se pudo registrar el ingreso de productos!", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									Transaction.Current.Rollback();
									Scope.Dispose();
									return;
								}
								dt_AnulaVenta = AdmVenta.CargaDetalleNotaSalida(Convert.ToInt32(codNotaSalida), nota2.CodAlmacen);
								LeeProductos();
								foreach (clsDetalleNotaIngreso det in lstNotaIng)
								{
									if (!AdmIngreso.insertdetalle(det))
									{
										MessageBox.Show("No se puede retornar el siguiente producto: \nProducto: " + det.CodProducto + " - " + det.DescripcionProducto, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
										Transaction.Current.Rollback();
										Scope.Dispose();
										return;
									}
								}
								venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
								if (!bandera)
								{
									MessageBox.Show("ocurrio un error inesperado al anular venta", "Anulacion Incompleta de Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									Transaction.Current.Rollback();
									Scope.Dispose();
									return;
								}
								Scope.Complete();
								Scope.Dispose();
							}
							if (bandera)
							{
								try
								{
									clsDespacho despacho = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
									if (despacho != null)
									{
										if (admdespacho.anular(despacho))
										{
											clsRequerimientoAlmacen req_alm = admreqalm.CargaRequerimientosSegun(venta.CodPedido, venta.CodAlmacen, -1);
											if (req_alm != null)
											{
												int codReqAlm = req_alm.Codigo;
												DataTable listadoCodTrans = admreqalm.cargaTransferenciasAprobadas(codReqAlm);
												if (listadoCodTrans != null)
												{
													if (listadoCodTrans.Rows.Count > 1)
													{
														MessageBox.Show("Documento de Extornacion No Creado.\nUn Requerimiento de Almacen de Tipo Venta no puede tener mas de una Transferencia Activa.\ncodReq: " + codReqAlm, "Error Requerimiento No Anulado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
														return;
													}
													if (listadoCodTrans.Rows.Count == 1)
													{
														bool rpta = admreqalm.anular(codReqAlm, frmLogin.iCodUser);
														int codTransDir = Convert.ToInt32(listadoCodTrans.Rows[0].Field<object>(0));
														clsTipoDocumento tipodoc = Admdoc.BuscaTipoDocumento("DET");
														clsTransferencia transf = admTransferencia.CargaTransferencia(codTransDir);
														clsTransferencia extornacion = new clsTransferencia();
														extornacion.codTransAExtornar = codTransDir;
														extornacion.CodAlmacenOrigen = req_alm.CodAlmacenSolicitante;
														extornacion.CodAlmacenDestino = req_alm.CodAlmacenDespacho;
														extornacion.CodTipoDocumento = tipodoc.CodTipoDocumento;
														extornacion.FechaEnvio = DateTime.Now;
														extornacion.FechaEntrega = DateTime.Now;
														extornacion.FormaPago = 0;
														extornacion.FechaPago = DateTime.Now.Date;
														extornacion.CodListaPrecio = 0;
														string comentario = "Documento de Extornacion para Transf: " + codTransDir;
														extornacion.Comentario = comentario;
														extornacion.DescripcionRechazo = "";
														extornacion.CodUser = frmLogin.iCodUser;
														extornacion.Estado = 1;
														extornacion.Codserie = transf.Codserie;
														extornacion.Serie = transf.Serie;
														extornacion.Numerodocumento = transf.Numerodocumento;
														extornacion.Moneda = 1;
														List<clsDetalleTransferencia> detalle = obtenerDetalleParaTransferencia(req_alm, extornacion);
														if (detalle.Count > 0 && admTransferencia.insert(extornacion))
														{
															foreach (clsDetalleTransferencia det2 in detalle)
															{
																det2.CodTransDir = Convert.ToInt32(extornacion.CodTransDir);
																admTransferencia.insertdetalle(det2);
															}
															apruebaTransferencia(extornacion, detalle);
														}
													}
												}
											}
											else
											{
												DataTable tiene_req = admdespacho.VerificaRequerimientoAnularVenta(venta.CodAlmacen, Convert.ToInt32(venta.CodFacturaVenta));
												if (Convert.ToBoolean(tiene_req.Rows[0].Field<object>(0)))
												{
													MessageBox.Show("Requerimiento de Almacen No Ha Sido Anulado", "Requerimiento No Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
												}
											}
										}
										else
										{
											MessageBox.Show("Despacho No Anulado", "Anulacion Despacho Incompleta", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										}
									}
								}
								catch (Exception ex)
								{
									MessageBox.Show(ex.Message, "Error Procedimiento de Despacho - Req. - Transf.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
						}
						else
						{
							MessageBox.Show("La Venta ya esta Anulada", "Anulacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
					}
					else
					{
						MessageBox.Show("Usuario No Reconocido", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
				CargaLista();
			}
			else
			{
				MessageBox.Show("Seleccione una venta...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		else if (Convert.ToInt32(cmbAlmacenes.SelectedValue) == 0)
		{
			MessageBox.Show("Seleccione un almacen...", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private List<clsDetalleTransferencia> obtenerDetalleParaTransferencia(clsRequerimientoAlmacen req_alm, clsTransferencia extornacion)
	{
		List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();
		DataTable detalleTransf = admTransferencia.CargaDetalle(extornacion.codTransAExtornar);
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
					admTransferencia.Aprobar(Convert.ToInt32(extornacion.CodTransDir));
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

	private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvVentas.Rows.Count >= 1 && e.RowIndex != -1)
		{
			btnIrPedido.Enabled = true;
			if (dgvVentas.Rows[e.RowIndex].Cells[estado.Name].Value.ToString() == "ACTIVO")
			{
				btnAnular.Text = "Anular";
				btnAnular.Enabled = true;
				btnAnular.ImageIndex = 4;
			}
		}
	}

	private void dtpDesde_ValueChanged(object sender, EventArgs e)
	{
	}

	private void dtpHasta_ValueChanged(object sender, EventArgs e)
	{
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			usuario_click = null;
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 2;
			int codPermiso = new clsAdmFormulario().getPermisoExportarExcelVentas();
			frm.permiso = codPermiso;
			frm.PermitirAdministradores = true;
			frm.tipoVentanaAAsignarUsuario = 6;
			frm.ventanaVentas = this;
			DialogResult dr = frm.ShowDialog();
			if (dr != DialogResult.OK || usuario_click == null)
			{
				return;
			}
			Cursor = Cursors.WaitCursor;
			SLDocument sl = new SLDocument();
			if (dgvVentas1.ChildRows.Count > 0)
			{
				int i = 0;
				int fila_excel = 4;
				int fila_a_concatenar = 0;
				int fila_first_concat = 0;
				int contador = 1;
				string desde = dtpDesde.Value.ToString();
				string hasta = dtpHasta.Value.ToString();
				sl.AddWorksheet("Listado de ventas");
				formatearFilaPrincipalHoja(sl, desde, hasta);
				contador = 1;
				DataTable table = new DataTable();
				foreach (GridViewDataColumn column in dgvVentas1.Columns)
				{
					table.Columns.Add(column.Name, column.DataType);
				}
				foreach (GridViewRowInfo row in dgvVentas1.MasterTemplate.DataView)
				{
					DataRow dataRow = table.NewRow();
					for (int o = 0; o < table.Columns.Count; o++)
					{
						dataRow[o] = row.Cells[o].Value;
					}
					table.Rows.Add(dataRow);
				}
				foreach (DataRow fila in table.Rows)
				{
					SLStyle aux_style = sl.CreateStyle();
					asignarBordes(aux_style);
					sl.SetCellStyle("A" + fila_excel, aux_style);
					sl.SetCellStyle("B" + fila_excel, aux_style);
					dandoValoresaFilaVentasExcel(sl, fila_excel, fila);
					fila_excel++;
					i++;
					contador++;
				}
			}
			Cursor = Cursors.Default;
			try
			{
				string cadenaGuardado = obtenerRutaParaGuardar();
				if (cadenaGuardado != null)
				{
					sl.SaveAs(cadenaGuardado);
					Process.Start("explorer.exe", cadenaGuardado);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Reporte Ventas");
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Ocurrió un error al generar reporte de ventas");
		}
	}

	public void button1_Click(object sender, EventArgs e)
	{
		button1.Text = "Buscando ventas...";
		button1.Enabled = false;
		Application.DoEvents();
		CargaLista();
		button1.Text = "Actualizar";
		button1.Enabled = true;
	}

	private void btnVistaSucursales_Click(object sender, EventArgs e)
	{
		if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow != null && btnVistaSucursales.Text == "Activar Vista" && dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow.Index != -1)
		{
			DialogResult dlgResult = MessageBox.Show("¿Esta seguro que desea activar la vista de este documento en otras sucursales?", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmVenta.VistaSucursal(Convert.ToInt32(venta.CodFacturaVenta), 1))
			{
				MessageBox.Show("El documento puede ser visualizado desde cualquier sucursal correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaLista();
			}
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea cambiar venta a pendiente de entrega", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.No)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvVentas.Rows)
		{
			DataGridViewCheckBoxCell cellSeleccion = row.Cells["pendiente"] as DataGridViewCheckBoxCell;
			if (Convert.ToBoolean(cellSeleccion.Value))
			{
				AdmVenta.VentaPendiente(Convert.ToInt32(row.Cells[codigo.Name].Value));
			}
		}
	}

	private void lbfactbol_Click(object sender, EventArgs e)
	{
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
	{
		CargaLista();
	}

	private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvVentas1_CellClick(object sender, GridViewCellEventArgs e)
	{
		if (dgvVentas1.Rows.Count >= 1 && e.RowIndex != -1)
		{
			venta.CodFacturaVenta = e.Row.Cells["codigo"].Value.ToString();
			nombreDocumento = e.Row.Cells["nombre_documento"].Value.ToString();
			tipoDocumento = e.Row.Cells["documento"].Value.ToString();
		}
	}

	private void dgvVentas1_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (dgvVentas1.Rows.Count < 1 || e.RowIndex == -1)
		{
			return;
		}
		if (e.Column.Name.ToString() == "NotaCredito")
		{
			string cadena = e.Row.Cells["NotaCredito"].Value.ToString();
			if (cadena.Contains("NCT"))
			{
				string[] splitCadena = cadena.Split('-');
				string codFactura = e.Row.Cells["codigo"].Value.ToString();
				string codNotaIngreso = e.Row.Cells["codNotaIngreso"].Value.ToString();
				string codNotaCredito = e.Row.Cells["codNotaCredito"].Value.ToString();
				frmNotadeCredito form = new frmNotadeCredito();
				form.MdiParent = base.MdiParent;
				form.CodNota = codFactura;
				form.CodNC = Convert.ToInt32(codNotaCredito);
				form.CodNotaS = Convert.ToInt32(codNotaIngreso);
				form.Proceso = 3;
				form.Show();
			}
		}
		else if (e.Column.Name.ToString() == "num_despacho")
		{
			int codDespacho = Convert.ToInt32(e.Row.Cells["cod_despacho"].Value);
			int estado = Convert.ToInt32(e.Row.Cells["estado_despacho"].Value);
			int anulado = Convert.ToInt32(e.Row.Cells["despacho_anulado"].Value);
			frmDespacho form2 = new frmDespacho();
			form2.MdiParent = base.MdiParent;
			form2.Dock = DockStyle.Fill;
			form2.WindowState = FormWindowState.Maximized;
			form2.codDespacho = codDespacho;
			form2.Proceso = ((estado == 1 && anulado == 0) ? 1 : 2);
			form2.Show();
		}
		else if (e.Column.Name.ToString() == "metodopago")
		{
			string metodopago = Convert.ToString(e.Row.Cells["metodopago"].Value);
			if (metodopago.Contains("PEND"))
			{
				venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
				if (venta != null)
				{
					frmCancelarPago form3 = new frmCancelarPago();
					form3.VentComp = 1;
					form3.tipo = 3;
					form3.CodCliente = venta.CodCliente;
					form3.venta = venta;
					form3.opcionSuma = 1;
					form3.ShowDialog();
					CargaLista();
				}
				else
				{
					MessageBox.Show("No se puedo cargar venta, por favor verifique!");
				}
			}
		}
		else if (e.Column.Name.ToString() == "canalventa")
		{
			string canalseleccionado = Convert.ToString(e.Row.Cells["canalventa"].Value);
			if (frmLogin.accesocanalventas || frmLogin.iNivelUser == 1)
			{
				venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
				if (venta != null)
				{
					frmCanalVentas form4 = new frmCanalVentas();
					form4.CanalSeleccionado = venta.CodCanalVenta;
					form4.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
					form4.ShowDialog();
					CargaLista();
				}
				else
				{
					MessageBox.Show("No se puedo cargar venta, por favor verifique!");
				}
			}
		}
		else if (e.Column.Name.ToString() == "banco" || e.Column.Name.ToString() == "cuenta")
		{
			if (frmLogin.AcesosUsuario.Contains(171) || frmLogin.iNivelUser == 1)
			{
				venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
				frmCambioCuentaBanco form5 = new frmCambioCuentaBanco();
				form5.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
				form5.ShowDialog();
				CargaLista();
			}
		}
		else
		{
			frmVenta form6 = new frmVenta();
			form6.MdiParent = base.MdiParent;
			form6.CodVenta = venta.CodFacturaVenta;
			form6.Proceso = 3;
			form6.VerBotonDespacho = true;
			form6.Show();
		}
	}

	private void dgvVentas1_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		if (e.CellElement is GridFilterCellElement)
		{
			e.CellElement.RowInfo.Height = 40;
		}
		if (e.CellElement is GridHeaderCellElement)
		{
			e.CellElement.RowInfo.Height = 20;
		}
	}

	private void dgvVentas1_ViewRowFormatting(object sender, RowFormattingEventArgs e)
	{
		if (e.RowElement is GridDataRowElement)
		{
			e.RowElement.RowInfo.Height = 35;
			e.RowElement.TextWrap = true;
		}
	}

	private void dgvVentas1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
	{
		e.CellElement.BorderLeftWidth = 1f;
		e.CellElement.BorderRightWidth = 1f;
		e.CellElement.BorderTopWidth = 1f;
		e.CellElement.BorderBottomWidth = 1f;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		GridViewPdfExport pdfExporter = new GridViewPdfExport(dgvVentas1);
		pdfExporter.HiddenColumnOption = HiddenOption.DoNotExport;
		pdfExporter.ExportVisualSettings = true;
		pdfExporter.PageSize = new SizeF(350f, 350f);
		pdfExporter.ShowHeaderAndFooter = true;
		string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\exportListaVentasPDF.pdf";
		pdfExporter.RunExport(fileName, new PdfExportRenderer());
		FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\exportListaVentasPDF.pdf");
		if (fi.Exists)
		{
			Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\exportListaVentasPDF.pdf");
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
	}

	private void CargaTransaccion(int CodTransaccion)
	{
		tran = AdmTran.MuestraTransaccion(CodTransaccion);
		tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
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
		ListaDetalleVenta.Add(deta);
	}

	private async void consultaCDR(clsFacturaVenta ventaConsultada)
	{
		ISerializador serializador = new Serializador();
		IServicioSunatConsultas servicioSunatDocumentos = new ServicioSunatConsultas();
		new RespuestaSincrono();
		new EnviarDocumentoResponse();
		DatosDocumento request = new DatosDocumento
		{
			TipoComprobante = ((ventaConsultada.CodTipoDocumento == 1) ? "03" : "01"),
			Serie = ((ventaConsultada.CodTipoDocumento == 1) ? ("B" + ventaConsultada.Serie) : ("F" + ventaConsultada.Serie)),
			Numero = ventaConsultada.NumDoc.PadLeft(8, '0'),
			RucEmisor = ventaConsultada.empresa.Ruc
		};
		servicioSunatDocumentos.Inicializar(new ParametrosConexion
		{
			Ruc = ventaConsultada.empresa.Ruc,
			UserName = ventaConsultada.empresa.UsuarioSunat,
			Password = ventaConsultada.empresa.ClaveSunat,
			EndPointUrl = "https://e-factura.sunat.gob.pe/ol-it-wsconscpegem/billConsultService"
		});
		RespuestaSincrono respuestaEnvio = servicioSunatDocumentos.ConsultarConstanciaDeRecepcion(request);
		if (respuestaEnvio.Exito)
		{
			EnviarDocumentoResponse response = await serializador.GenerarDocumentoRespuesta(respuestaEnvio.ConstanciaDeRecepcion);
			if (response.Exito)
			{
				File.WriteAllBytes(Program.CarpetaCdr + "\\R-" + request.RucEmisor + "-" + request.TipoComprobante + "-" + request.Serie + "-" + request.Numero + ".zip", Convert.FromBase64String(respuestaEnvio.ConstanciaDeRecepcion));
				string ruta = "C:\\DOCUMENTOS-" + request.RucEmisor + "\\CDR\\R-" + request.Numero + ".zip";
				File.WriteAllBytes(ruta, Convert.FromBase64String(respuestaEnvio.ConstanciaDeRecepcion));
			}
			else
			{
				MessageBox.Show("Mensaje del servidor SUNAT: " + response.MensajeError);
			}
		}
		else
		{
			MessageBox.Show("Mensaje del servidor SUNAT: " + respuestaEnvio.MensajeError);
		}
	}

	private async void button3_Click_1(object sender, EventArgs e)
	{
		try
		{
			frmAutorizacion frm = new frmAutorizacion();
			DialogResult dr = frm.ShowDialog();
			if (dr != DialogResult.OK)
			{
				return;
			}
			MessageBox.Show("Este proceso puede tardar varios minutos..!", "Generando XML");
			Cursor.Current = Cursors.WaitCursor;
			if (dgvVentas1.Rows.Count <= 0 || dgvVentas1.CurrentRow == null)
			{
				return;
			}
			foreach (GridViewRowInfo row in dgvVentas1.Rows)
			{
				if (Convert.ToInt32(row.Cells["codigo"].Value) == 4)
				{
					nc = new clsNotaCredito();
					nc = admnc.CargaNotaCredito_Regeneracion(Convert.ToInt32(row.Cells["codFactura"].Value));
					if (nc != null)
					{
						CargaTransaccion(nc.CodTipoTransaccion);
						cli = AdmCli.MuestraCliente(nc.CodCliente);
						CargaDetalleNC();
						RecorreDetalleNC();
						await facturacion.DatosNCredito(cli, nc, listadetalleNC);
						firmadigital = facturacion.LogoEmp;
						listadetalleNC.Clear();
					}
					else
					{
						MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					continue;
				}
				ventaConsultada = new clsFacturaVenta();
				ventaConsultada = AdmVenta.CargaFacturaVenta_Regeneracion(Convert.ToInt32(row.Cells["codigo"].Value));
				if (ventaConsultada != null)
				{
					CargaTransaccion(ventaConsultada.CodTipoTransaccion);
					cli = AdmCli.MuestraCliente(ventaConsultada.CodCliente);
					ventaConsultada.DocumentoIdentidad = AdmDocumentoIdentidad.MuestraDocumentoIdentidad(Convert.ToInt32(ventaConsultada.CodigoDocumentoIdentidad));
					CargaDetalle();
					RecorreDetalle();
					await facturacion.GeneraDocumento(cli, ventaConsultada, ListaDetalleVenta, 1);
					firmadigital = facturacion.LogoEmp;
					consultaCDR(ventaConsultada);
					ListaDetalleVenta.Clear();
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
			Exception a = ex;
			MessageBox.Show(a.Message);
			Cursor.Current = Cursors.Default;
		}
		finally
		{
			Cursor.Current = Cursors.Default;
			MessageBox.Show("Proceso terminado correctamente");
		}
	}

	private async void button4_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvVentas1.Rows.Count >= 1 && Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value) > 0)
			{
				Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value);
				ventaConsultada = new clsFacturaVenta();
				ventaConsultada = AdmVenta.CargaFacturaVenta_Regeneracion(Convert.ToInt32(venta.CodFacturaVenta));
				ISerializador serializador = new Serializador();
				IServicioSunatConsultas servicioSunatDocumentos = new ServicioSunatConsultas();
				new RespuestaSincrono();
				EnviarDocumentoResponse response = new EnviarDocumentoResponse();
				DatosDocumento request = new DatosDocumento
				{
					TipoComprobante = ((ventaConsultada.CodTipoDocumento == 1) ? "03" : "01"),
					Serie = ((ventaConsultada.CodTipoDocumento == 1) ? ("B" + ventaConsultada.Serie) : ("F" + ventaConsultada.Serie)),
					Numero = ventaConsultada.NumDoc.PadLeft(8, '0'),
					RucEmisor = ventaConsultada.empresa.Ruc
				};
				servicioSunatDocumentos.Inicializar(new ParametrosConexion
				{
					Ruc = ventaConsultada.empresa.Ruc,
					UserName = ventaConsultada.empresa.UsuarioSunat,
					Password = ventaConsultada.empresa.ClaveSunat,
					EndPointUrl = "https://e-factura.sunat.gob.pe/ol-it-wsconscpegem/billConsultService"
				});
				RespuestaSincrono respuestaEnvio = servicioSunatDocumentos.ConsultarConstanciaDeRecepcion(request);
				if (respuestaEnvio.Exito)
				{
					response = await serializador.GenerarDocumentoRespuesta(respuestaEnvio.ConstanciaDeRecepcion);
					if (response.Exito)
					{
						MessageBox.Show(response.MensajeRespuesta);
						File.WriteAllBytes(Program.CarpetaCdr + "\\R-" + request.RucEmisor + "-" + request.TipoComprobante + "-" + request.Serie + "-" + request.Numero + ".zip", Convert.FromBase64String(respuestaEnvio.ConstanciaDeRecepcion));
						string ruta = "C:\\DOCUMENTOS-" + request.RucEmisor + "\\CDR\\R-" + request.Numero + ".zip";
						File.WriteAllBytes(ruta, Convert.FromBase64String(respuestaEnvio.ConstanciaDeRecepcion));
					}
					else
					{
						MessageBox.Show("Mensaje del servidor SUNAT: " + response.MensajeError);
					}
				}
				else
				{
					MessageBox.Show("Mensaje del servidor SUNAT: " + response.MensajeError);
				}
			}
			else
			{
				MessageBox.Show("Selecciona una venta..!", "VENTAS");
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 6;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
	}

	private void formatearFilaPrincipalHoja(SLDocument sl, string desde, string hasta)
	{
		sl.SetCellValue("A1", "REPORTE DE VENTAS ");
		sl.MergeWorksheetCells("A1", "T1");
		sl.SetCellValue("A2", "DESDE: " + Convert.ToDateTime(desde).ToShortDateString() + " - HASTA: " + Convert.ToDateTime(hasta).ToShortDateString());
		sl.MergeWorksheetCells("A2", "T2");
		SLStyle style = sl.CreateStyle();
		style.SetFontBold(IsBold: true);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetRowStyle(1, 3, style);
		style = sl.CreateStyle();
		asignarBordes(style);
		sl.SetCellValue("A3", "DIA");
		sl.SetColumnWidth(1, 10.0);
		sl.SetCellValue("B3", "FECHA");
		sl.SetColumnWidth(2, 10.0);
		sl.SetCellValue("C3", "HORA");
		sl.SetColumnWidth(3, 10.0);
		sl.SetCellValue("D3", "N° Documento");
		sl.SetColumnWidth(4, 20.0);
		sl.SetCellValue("E3", "Doc. Cliente");
		sl.SetColumnWidth(5, 15.0);
		sl.SetCellValue("F3", "Cliente");
		sl.SetColumnWidth(6, 25.0);
		sl.SetCellValue("G3", "Moneda");
		sl.SetColumnWidth(7, 10.0);
		sl.SetCellValue("H3", "Importe");
		sl.SetColumnWidth(8, 20.0);
		sl.SetCellValue("I3", "ICBPER");
		sl.SetColumnWidth(9, 10.0);
		sl.SetCellValue("J3", "Retención");
		sl.SetColumnWidth(10, 10.0);
		sl.SetCellValue("K3", "F. Pago.");
		sl.SetColumnWidth(11, 11.0);
		sl.SetCellValue("L3", "Efectivo");
		sl.SetColumnWidth(12, 12.0);
		sl.SetCellValue("M3", "Deposito");
		sl.SetColumnWidth(13, 13.0);
		sl.SetCellValue("N3", "Tarjeta");
		sl.SetColumnWidth(14, 14.0);
		sl.SetCellValue("O3", "Transferencia");
		sl.SetColumnWidth(15, 15.0);
		sl.SetCellValue("P3", "NC");
		sl.SetColumnWidth(16, 16.0);
		sl.SetCellValue("Q3", "Estado");
		sl.SetColumnWidth(17, 17.0);
		sl.SetCellValue("R3", "Enviado Sunat");
		sl.SetColumnWidth(18, 18.0);
		sl.SetCellValue("S3", "Vendedor");
		sl.SetColumnWidth(19, 19.0);
		sl.SetCellValue("T3", "Despacho");
		sl.SetColumnWidth(20, 20.0);
	}

	private void asignarBordes(SLStyle style)
	{
		style.Border.LeftBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.LeftBorder.Color = System.Drawing.Color.Black;
		style.Border.TopBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.TopBorder.Color = System.Drawing.Color.Black;
		style.Border.RightBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.RightBorder.Color = System.Drawing.Color.Black;
		style.Border.BottomBorder.BorderStyle = BorderStyleValues.Thin;
		style.Border.BottomBorder.Color = System.Drawing.Color.Black;
	}

	private void dandoValoresaFilaVentasExcel(SLDocument sl, int fila_excel, DataRow fila)
	{
		SLStyle style = sl.CreateStyle();
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		asignarBordes(style);
		sl.SetCellValue("A" + fila_excel, fila[27].ToString());
		sl.SetCellStyle("A" + fila_excel, style);
		sl.SetCellValue("B" + fila_excel, Convert.ToDateTime(fila[1]).ToShortDateString());
		sl.SetCellStyle("B" + fila_excel, style);
		sl.SetCellValue("C" + fila_excel, fila[28].ToString());
		sl.SetCellStyle("C" + fila_excel, style);
		sl.SetCellValue("D" + fila_excel, fila[3].ToString());
		sl.SetCellStyle("D" + fila_excel, style);
		sl.SetCellValue("E" + fila_excel, fila[4].ToString());
		sl.SetCellStyle("E" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.General);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellStyle("F" + fila_excel, style);
		sl.SetCellValue("F" + fila_excel, fila[5].ToString());
		sl.SetCellStyle("F" + fila_excel, style);
		style.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
		style.SetVerticalAlignment(VerticalAlignmentValues.Center);
		sl.SetCellValue("G" + fila_excel, fila[6].ToString());
		sl.SetCellStyle("G" + fila_excel, style);
		sl.SetCellValue("H" + fila_excel, (fila[7] == DBNull.Value) ? 0.0 : Convert.ToDouble(fila[7]));
		sl.SetCellStyle("H" + fila_excel, style);
		sl.SetCellValue("I" + fila_excel, (fila[8] == DBNull.Value) ? 0.0 : Convert.ToDouble(fila[8]));
		sl.SetCellStyle("I" + fila_excel, style);
		sl.SetCellValue("J" + fila_excel, (fila[9] == DBNull.Value) ? 0.0 : Convert.ToDouble(fila[9]));
		style = sl.CreateStyle();
		asignarBordes(style);
		style.SetWrapText(IsWrapped: true);
		sl.SetCellStyle("J" + fila_excel, style);
		style = sl.CreateStyle();
		style.FormatCode = "#,##0.00";
		asignarBordes(style);
		sl.SetCellValue("K" + fila_excel, fila[10].ToString());
		sl.SetCellStyle("K" + fila_excel, style);
		sl.SetCellValue("L" + fila_excel, (fila[13].ToString() != "") ? Convert.ToDouble(fila[13]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("L" + fila_excel, style);
		style = sl.CreateStyle();
		style.FormatCode = "#,##0.00";
		asignarBordes(style);
		sl.SetCellValue("M" + fila_excel, (fila[14] != DBNull.Value) ? Convert.ToDouble(fila[14]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("M" + fila_excel, style);
		sl.SetCellValue("N" + fila_excel, (fila[15] != DBNull.Value) ? Convert.ToDouble(fila[15]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("N" + fila_excel, style);
		sl.SetCellValue("O" + fila_excel, (fila[16] != DBNull.Value) ? Convert.ToDouble(fila[16]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("O" + fila_excel, style);
		sl.SetCellValue("P" + fila_excel, (fila[17] != DBNull.Value) ? Convert.ToDouble(fila[17]) : Convert.ToDouble(0.0));
		sl.SetCellStyle("P" + fila_excel, style);
		sl.SetCellValue("Q" + fila_excel, fila[18].ToString());
		sl.SetCellStyle("Q" + fila_excel, style);
		sl.SetCellValue("R" + fila_excel, fila[21].ToString());
		sl.SetCellStyle("R" + fila_excel, style);
		sl.SetCellValue("S" + fila_excel, fila[23].ToString());
		sl.SetCellStyle("S" + fila_excel, style);
		sl.SetCellValue("T" + fila_excel, fila[32].ToString());
		sl.SetCellStyle("T" + fila_excel, style);
	}

	private string obtenerRutaParaGuardar(string namefile = "Ventas_diarias")
	{
		string cadena = null;
		try
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
			sfd.Title = "Guardando Archivo Excel de Exportacion";
			sfd.FileName = namefile;
			sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				cadena = sfd.FileName;
			}
			else
			{
				MessageBox.Show("Se Cancelo la Exportacion", "Exportacion de Ventas Diarias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString(), ex.Message);
		}
		return cadena;
	}

	private void frmVentas_Shown(object sender, EventArgs e)
	{
		try
		{
			if (frmLogin.iNivelUser != 1 && frmLogin.iNivelUser != 3)
			{
				return;
			}
			param = admParam.ObtenerParametro(3);
			if (param.valor != "")
			{
				int dias = Convert.ToInt32(param.valor);
				DateTime Hoy = DateTime.Now;
				DateTime FechaRestada = Hoy.AddDays(-dias);
				DateTime hasta = Hoy.AddDays(-1.0);
				lista_repositorio = clsadmrepo.listar_documentos_pendientes("-1", frmLogin.iCodSucursal, Convert.ToDateTime(FechaRestada), Convert.ToDateTime(hasta));
				if (lista_repositorio != null && lista_repositorio.Count > 0)
				{
					frmDocumentosPendientes documentos = new frmDocumentosPendientes();
					documentos.desde = Convert.ToDateTime(FechaRestada);
					documentos.hasta = Convert.ToDateTime(hasta);
					documentos.lista_repositorio = lista_repositorio;
					documentos.ShowDialog();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error al consultar envtas pendientes de envío:" + ex.Message);
		}
	}

	private void btnPdf_Click(object sender, EventArgs e)
	{
		try
		{
			if (nombreDocumento != "")
			{
				string directorio = "";
				if (tipoDocumento == "FT")
				{
					directorio = "Facturas";
				}
				else if (tipoDocumento == "BV")
				{
					directorio = "Boletas";
				}
				string[] rucEmpresa = nombreDocumento.Split('-');
				string RutaArchivo1 = AppDomain.CurrentDomain.BaseDirectory + "\\documentos\\" + directorio + "\\" + nombreDocumento + ".pdf";
				string RutaArchivo2 = "C:\\DOCUMENTOS-" + rucEmpresa[0] + "\\DOCUMENTOS ENVIAR\\" + directorio + "\\" + nombreDocumento + ".pdf";
				if (File.Exists(RutaArchivo1))
				{
					try
					{
						FileInfo fi = new FileInfo(RutaArchivo1);
						if (fi.Exists)
						{
							Process.Start(RutaArchivo1);
						}
						return;
					}
					catch (Exception)
					{
						MessageBox.Show("Error al abrir PDF");
						return;
					}
				}
				try
				{
					FileInfo fi2 = new FileInfo(RutaArchivo2);
					if (fi2.Exists)
					{
						Process.Start(RutaArchivo2);
					}
					return;
				}
				catch (Exception)
				{
					MessageBox.Show("Error al abrir PDF");
					return;
				}
			}
			MessageBox.Show("No existe documento para esta venta!");
		}
		catch (Exception ex3)
		{
			MessageBox.Show(ex3.Message);
		}
	}

	private void btnXML_Click(object sender, EventArgs e)
	{
		try
		{
			if (nombreDocumento != "")
			{
				string directorio = "";
				if (tipoDocumento == "FT")
				{
					directorio = "Facturas";
				}
				else if (tipoDocumento == "BV")
				{
					directorio = "Boletas";
				}
				string[] rucEmpresa = nombreDocumento.Split('-');
				string RutaArchivo1 = AppDomain.CurrentDomain.BaseDirectory + "\\documentos\\" + directorio + "\\" + nombreDocumento + ".xml";
				string RutaArchivo2 = "C:\\DOCUMENTOS-" + rucEmpresa[0] + "\\DOCUMENTOS ENVIAR\\" + directorio + "\\" + nombreDocumento + ".xml";
				if (File.Exists(RutaArchivo1))
				{
					try
					{
						FileInfo fi = new FileInfo(RutaArchivo1);
						if (fi.Exists)
						{
							Process.Start(RutaArchivo1);
						}
						return;
					}
					catch (Exception)
					{
						MessageBox.Show("Error al abrir PDF");
						return;
					}
				}
				try
				{
					FileInfo fi2 = new FileInfo(RutaArchivo2);
					if (fi2.Exists)
					{
						Process.Start(RutaArchivo2);
					}
					return;
				}
				catch (Exception)
				{
					MessageBox.Show("Error al abrir PDF");
					return;
				}
			}
			MessageBox.Show("No existe documento para esta venta!");
		}
		catch (Exception ex3)
		{
			MessageBox.Show(ex3.Message);
		}
	}

	private void VerificaSaldoCajaVentas()
	{
		double totPendiente = 0.0;
		double totNotaCredito = 0.0;
		double totVentaCredito = 0.0;
		double totDeposito = 0.0;
		double totTransferencia = 0.0;
		double totTarjeta = 0.0;
		double totEfectivo = 0.0;
		foreach (GridViewRowInfo fila in dgvVentas1.Rows)
		{
			dgvVentas1.Columns[13].IsVisible = true;
			dgvVentas1.Columns[14].IsVisible = true;
			dgvVentas1.Columns[15].IsVisible = true;
			dgvVentas1.Columns[16].IsVisible = true;
			dgvVentas1.Columns[17].IsVisible = true;
			dgvVentas1.Columns[33].IsVisible = true;
			dgvVentas1.Columns[34].IsVisible = true;
			if (Convert.ToString(fila.Cells["estado"].Value) != "ANULADO")
			{
				totPendiente += Convert.ToDouble((fila.Cells["tot_pendiente"].Value == DBNull.Value) ? ((object)0) : fila.Cells["tot_pendiente"].Value);
				totVentaCredito += Convert.ToDouble((fila.Cells["tot_credito"].Value == DBNull.Value) ? ((object)0) : fila.Cells["tot_credito"].Value);
				totNotaCredito += Convert.ToDouble((fila.Cells["notac"].Value == DBNull.Value) ? ((object)0) : fila.Cells["notac"].Value);
				totDeposito += Convert.ToDouble((fila.Cells["deposito"].Value == DBNull.Value) ? ((object)0) : fila.Cells["deposito"].Value);
				totTransferencia += Convert.ToDouble((fila.Cells["transferencia"].Value == DBNull.Value) ? ((object)0) : fila.Cells["transferencia"].Value);
				totTarjeta += Convert.ToDouble((fila.Cells["tarjeta"].Value == DBNull.Value) ? ((object)0) : fila.Cells["tarjeta"].Value);
				totEfectivo += Convert.ToDouble((fila.Cells["efectivo"].Value == DBNull.Value) ? ((object)0) : fila.Cells["efectivo"].Value);
			}
		}
		dgvVentas1.Columns[13].IsVisible = false;
		dgvVentas1.Columns[14].IsVisible = false;
		dgvVentas1.Columns[15].IsVisible = false;
		dgvVentas1.Columns[16].IsVisible = false;
		dgvVentas1.Columns[17].IsVisible = false;
		dgvVentas1.Columns[33].IsVisible = false;
		dgvVentas1.Columns[34].IsVisible = false;
		lblPendiente.Text = totPendiente.ToString("## ### ##0.00");
		lblNC.Text = totNotaCredito.ToString("## ### ##0.00");
		lblVC.Text = totVentaCredito.ToString("## ### ##0.00");
		lbDeposito.Text = totDeposito.ToString("## ### ##0.00");
		lbCheque.Text = totTransferencia.ToString("## ### ##0.00");
		lblCajaSeparacion.Text = totTarjeta.ToString("## ### ##0.00");
		lblSaldoCaja.Text = totEfectivo.ToString("## ### ##0.00");
	}

	private void calculoSumatoriaFiltrado()
	{
		double totPendiente = 0.0;
		double totNotaCredito = 0.0;
		double totVentaCredito = 0.0;
		double totDeposito = 0.0;
		double totTransferencia = 0.0;
		double totTarjeta = 0.0;
		double totEfectivo = 0.0;
		foreach (GridViewRowInfo fila in dgvVentas1.ChildRows)
		{
			dgvVentas1.Columns[13].IsVisible = true;
			dgvVentas1.Columns[14].IsVisible = true;
			dgvVentas1.Columns[15].IsVisible = true;
			dgvVentas1.Columns[16].IsVisible = true;
			dgvVentas1.Columns[17].IsVisible = true;
			dgvVentas1.Columns[33].IsVisible = true;
			dgvVentas1.Columns[34].IsVisible = true;
			if (Convert.ToString(fila.Cells["estado"].Value) != "ANULADO")
			{
				totPendiente += Convert.ToDouble((fila.Cells["tot_pendiente"].Value == DBNull.Value) ? ((object)0) : fila.Cells["tot_pendiente"].Value);
				totVentaCredito += Convert.ToDouble((fila.Cells["tot_credito"].Value == DBNull.Value) ? ((object)0) : fila.Cells["tot_credito"].Value);
				totNotaCredito += Convert.ToDouble((fila.Cells["notac"].Value == DBNull.Value) ? ((object)0) : fila.Cells["notac"].Value);
				totDeposito += Convert.ToDouble((fila.Cells["deposito"].Value == DBNull.Value) ? ((object)0) : fila.Cells["deposito"].Value);
				totTransferencia += Convert.ToDouble((fila.Cells["transferencia"].Value == DBNull.Value) ? ((object)0) : fila.Cells["transferencia"].Value);
				totTarjeta += Convert.ToDouble((fila.Cells["tarjeta"].Value == DBNull.Value) ? ((object)0) : fila.Cells["tarjeta"].Value);
				totEfectivo += Convert.ToDouble((fila.Cells["efectivo"].Value == DBNull.Value) ? ((object)0) : fila.Cells["efectivo"].Value);
			}
		}
		dgvVentas1.Columns[13].IsVisible = false;
		dgvVentas1.Columns[14].IsVisible = false;
		dgvVentas1.Columns[15].IsVisible = false;
		dgvVentas1.Columns[16].IsVisible = false;
		dgvVentas1.Columns[17].IsVisible = false;
		dgvVentas1.Columns[33].IsVisible = false;
		dgvVentas1.Columns[34].IsVisible = false;
		lblPendientes.Text = totPendiente.ToString("## ### ##0.00");
		lblVentaCredito.Text = totVentaCredito.ToString("## ### ##0.00");
		lblNotaCredito.Text = totNotaCredito.ToString("## ### ##0.00");
		lblDeposito.Text = totDeposito.ToString("## ### ##0.00");
		lblTransferencia.Text = totTransferencia.ToString("## ### ##0.00");
		lblTarjeta.Text = totTarjeta.ToString("## ### ##0.00");
		lblEfectivo.Text = totEfectivo.ToString("## ### ##0.00");
	}

	private void dgvVentas1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
	{
		try
		{
			RadGridView btn = (RadGridView)sender;
			if (btn.ActiveEditor != null)
			{
				if (btn.ActiveEditor.Value.ToString() != "")
				{
					calculoSumatoriaFiltrado();
				}
				else
				{
					resetTotales();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvVentas1_FilterExpressionChanged(object sender, FilterExpressionChangedEventArgs e)
	{
	}

	private void resetTotales()
	{
		lblPendientes.Text = "0.00";
		lblVentaCredito.Text = "0.00";
		lblNotaCredito.Text = "0.00";
		lblDeposito.Text = "0.00";
		lblTransferencia.Text = "0.00";
		lblTarjeta.Text = "0.00";
		lblEfectivo.Text = "0.00";
	}

	private void dgvVentas1_CustomSorting(object sender, GridViewCustomSortingEventArgs e)
	{
		if (dgvVentas1.SortDescriptors.Count == 0)
		{
			return;
		}
		int result = 0;
		for (int i = 0; i < dgvVentas1.SortDescriptors.Count; i++)
		{
			string cellValue1 = e.Row1.Cells[dgvVentas1.SortDescriptors[i].PropertyName].Value.ToString();
			string cellValue2 = e.Row2.Cells[dgvVentas1.SortDescriptors[i].PropertyName].Value.ToString();
			result = cellValue1.Length - cellValue2.Length;
			if (result != 0)
			{
				if (dgvVentas1.SortDescriptors[i].Direction == ListSortDirection.Descending)
				{
					result *= -1;
				}
				break;
			}
		}
		e.SortResult = result;
	}

	private void chbverificarventas_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			CargaLista();
			if (chbverificarventas.Checked && dgvVentas1.RowCount == 0)
			{
				MessageBox.Show("No tiene Ventas Contado Con error de Pago", "Ventas Contado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnguiafacturacion_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvVentas1.Rows.Count >= 1 && Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value) > 0)
			{
				int codFacturaVenta = Convert.ToInt32(dgvVentas1.CurrentRow.Cells["codigo"].Value);
				venta = AdmVenta.CargaFacturaVenta(codFacturaVenta);
				if (AdmGuiaFacturacion.VerificaGuias(codFacturaVenta, 0) == 0)
				{
					MessageBox.Show("Factura Venta ya se encuentra con Guía", "Venta Anulada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (venta.Anulado == 1)
				{
					MessageBox.Show("Factura Venta ya se encuentra Anulada", "Venta Anulada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				frmGuiaFacturacion form = new frmGuiaFacturacion();
				form.MdiParent = base.MdiParent;
				form.proceso = 1;
				form.codventa = codFacturaVenta;
				form.Show();
			}
			else
			{
				MessageBox.Show("Seleccione una venta...", "VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn22 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn23 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn24 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn25 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn26 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn27 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn28 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn29 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn30 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn31 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn32 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn33 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn34 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn35 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn36 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn37 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn38 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn39 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVentas));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvVentas1 = new Telerik.WinControls.UI.RadGridView();
		this.dgvVentas = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.documento = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codcliente = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.formapago = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.fechapago = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.impreso = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.NotaCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.enviadoS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codTipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.lbfactbol = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
		this.lblicbper = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.panel3 = new System.Windows.Forms.Panel();
		this.btnguiafacturacion = new System.Windows.Forms.Button();
		this.chbverificarventas = new System.Windows.Forms.CheckBox();
		this.label23 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.lblNotaCredito = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.lblPendientes = new System.Windows.Forms.Label();
		this.lblVentaCredito = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.lblTarjeta = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.lblTransferencia = new System.Windows.Forms.Label();
		this.lblDeposito = new System.Windows.Forms.Label();
		this.label32 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.lblEfectivo = new System.Windows.Forms.Label();
		this.label35 = new System.Windows.Forms.Label();
		this.btnSalir = new System.Windows.Forms.Button();
		this.label18 = new System.Windows.Forms.Label();
		this.btnXML = new System.Windows.Forms.Button();
		this.lblNC = new System.Windows.Forms.Label();
		this.btnPdf = new System.Windows.Forms.Button();
		this.label17 = new System.Windows.Forms.Label();
		this.lblPendiente = new System.Windows.Forms.Label();
		this.lblVC = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.button4 = new System.Windows.Forms.Button();
		this.lblCajaSeparacion = new System.Windows.Forms.Label();
		this.button3 = new System.Windows.Forms.Button();
		this.label12 = new System.Windows.Forms.Label();
		this.button2 = new System.Windows.Forms.Button();
		this.lbCheque = new System.Windows.Forms.Label();
		this.lbDeposito = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.label13 = new System.Windows.Forms.Label();
		this.button1 = new System.Windows.Forms.Button();
		this.label14 = new System.Windows.Forms.Label();
		this.btnReporte = new System.Windows.Forms.Button();
		this.lblSaldoCaja = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnIrPedido = new System.Windows.Forms.Button();
		this.btnVistaSucursales = new System.Windows.Forms.Button();
		this.panel1 = new System.Windows.Forms.Panel();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		this.groupBox2.SuspendLayout();
		this.panel3.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.BackColor = System.Drawing.Color.White;
		this.groupBox1.Controls.Add(this.dgvVentas1);
		this.groupBox1.Controls.Add(this.dgvVentas);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1419, 463);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Ventas";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.dgvVentas1.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvVentas1.Cursor = System.Windows.Forms.Cursors.Default;
		this.dgvVentas1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvVentas1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.dgvVentas1.ForeColor = System.Drawing.Color.FromArgb(32, 32, 32);
		this.dgvVentas1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.dgvVentas1.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas1.MasterTemplate.AllowAddNewRow = false;
		this.dgvVentas1.MasterTemplate.AllowDragToGroup = false;
		this.dgvVentas1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "codFactura";
		gridViewTextBoxColumn1.HeaderText = "Codigo";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codigo";
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "fecha";
		gridViewTextBoxColumn2.HeaderText = "Fecha";
		gridViewTextBoxColumn2.Name = "fecha";
		gridViewTextBoxColumn2.Width = 90;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "documento";
		gridViewTextBoxColumn3.HeaderText = "T. Doc.";
		gridViewTextBoxColumn3.IsVisible = false;
		gridViewTextBoxColumn3.Name = "documento";
		gridViewTextBoxColumn3.Width = 100;
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.FieldName = "numdoc";
		gridViewTextBoxColumn4.HeaderText = "N° Documento";
		gridViewTextBoxColumn4.Name = "numdoc";
		gridViewTextBoxColumn4.Width = 99;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.FieldName = "codcliente";
		gridViewTextBoxColumn5.HeaderText = "codcliente";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "codcliente";
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "cliente";
		gridViewTextBoxColumn6.HeaderText = "Cliente";
		gridViewTextBoxColumn6.Multiline = true;
		gridViewTextBoxColumn6.Name = "cliente";
		gridViewTextBoxColumn6.Width = 163;
		gridViewTextBoxColumn6.WrapText = true;
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.FieldName = "moneda";
		gridViewTextBoxColumn7.HeaderText = "Moneda";
		gridViewTextBoxColumn7.Name = "moneda";
		gridViewTextBoxColumn7.Width = 42;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.FieldName = "total";
		gridViewTextBoxColumn8.HeaderText = "Importe";
		gridViewTextBoxColumn8.Name = "importe";
		gridViewTextBoxColumn8.Width = 42;
		gridViewTextBoxColumn9.EnableExpressionEditor = false;
		gridViewTextBoxColumn9.FieldName = "icbper";
		gridViewTextBoxColumn9.HeaderText = "ICBPER";
		gridViewTextBoxColumn9.Name = "icbper";
		gridViewTextBoxColumn9.Width = 37;
		gridViewTextBoxColumn10.DataType = typeof(decimal);
		gridViewTextBoxColumn10.EnableExpressionEditor = false;
		gridViewTextBoxColumn10.FieldName = "retencion";
		gridViewTextBoxColumn10.FormatInfo = new System.Globalization.CultureInfo("es-PE");
		gridViewTextBoxColumn10.HeaderText = "Retencion";
		gridViewTextBoxColumn10.Name = "colRetencion";
		gridViewTextBoxColumn10.Width = 37;
		gridViewTextBoxColumn11.EnableExpressionEditor = false;
		gridViewTextBoxColumn11.FieldName = "fpago";
		gridViewTextBoxColumn11.HeaderText = "Forma Pago";
		gridViewTextBoxColumn11.Name = "fpago";
		gridViewTextBoxColumn11.Width = 66;
		gridViewTextBoxColumn12.EnableExpressionEditor = false;
		gridViewTextBoxColumn12.FieldName = "metodopago";
		gridViewTextBoxColumn12.HeaderText = "Metodo Pago";
		gridViewTextBoxColumn12.Name = "metodopago";
		gridViewTextBoxColumn12.Width = 81;
		gridViewTextBoxColumn13.AllowReorder = false;
		gridViewTextBoxColumn13.AllowSort = false;
		gridViewTextBoxColumn13.EnableExpressionEditor = false;
		gridViewTextBoxColumn13.FieldName = "fechapago";
		gridViewTextBoxColumn13.HeaderText = "F. Pago";
		gridViewTextBoxColumn13.Name = "fechapago";
		gridViewTextBoxColumn13.Width = 66;
		gridViewTextBoxColumn14.EnableExpressionEditor = false;
		gridViewTextBoxColumn14.FieldName = "efectivo";
		gridViewTextBoxColumn14.HeaderText = "Efectivo";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "efectivo";
		gridViewTextBoxColumn14.Width = 100;
		gridViewTextBoxColumn15.EnableExpressionEditor = false;
		gridViewTextBoxColumn15.FieldName = "deposito";
		gridViewTextBoxColumn15.HeaderText = "Deposito";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "deposito";
		gridViewTextBoxColumn15.Width = 100;
		gridViewTextBoxColumn16.EnableExpressionEditor = false;
		gridViewTextBoxColumn16.FieldName = "tarjeta";
		gridViewTextBoxColumn16.HeaderText = "Tarjeta";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "tarjeta";
		gridViewTextBoxColumn16.Width = 100;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "transferencia";
		gridViewTextBoxColumn17.HeaderText = "Transferencia";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "transferencia";
		gridViewTextBoxColumn17.Width = 100;
		gridViewTextBoxColumn18.EnableExpressionEditor = false;
		gridViewTextBoxColumn18.FieldName = "notac";
		gridViewTextBoxColumn18.HeaderText = "Nota Credito";
		gridViewTextBoxColumn18.IsVisible = false;
		gridViewTextBoxColumn18.Name = "notac";
		gridViewTextBoxColumn18.Width = 100;
		gridViewTextBoxColumn19.EnableExpressionEditor = false;
		gridViewTextBoxColumn19.FieldName = "anulado";
		gridViewTextBoxColumn19.HeaderText = "Estado";
		gridViewTextBoxColumn19.Name = "estado";
		gridViewTextBoxColumn19.Width = 66;
		gridViewTextBoxColumn20.EnableExpressionEditor = false;
		gridViewTextBoxColumn20.FieldName = "impreso";
		gridViewTextBoxColumn20.HeaderText = "Impreso";
		gridViewTextBoxColumn20.IsVisible = false;
		gridViewTextBoxColumn20.Name = "impreso";
		gridViewTextBoxColumn20.Width = 100;
		gridViewTextBoxColumn21.EnableExpressionEditor = false;
		gridViewTextBoxColumn21.FieldName = "NotaDeCredito";
		gridViewTextBoxColumn21.HeaderText = "NotaCredito";
		gridViewTextBoxColumn21.Name = "NotaCredito";
		gridViewTextBoxColumn21.Width = 81;
		gridViewTextBoxColumn22.EnableExpressionEditor = false;
		gridViewTextBoxColumn22.FieldName = "enviadoS";
		gridViewTextBoxColumn22.HeaderText = "Enviado Sunat";
		gridViewTextBoxColumn22.Name = "enviadoS";
		gridViewTextBoxColumn22.Width = 86;
		gridViewTextBoxColumn23.EnableExpressionEditor = false;
		gridViewTextBoxColumn23.FieldName = "codtipodocumento";
		gridViewTextBoxColumn23.HeaderText = "CodTipoDoc";
		gridViewTextBoxColumn23.IsVisible = false;
		gridViewTextBoxColumn23.Name = "codtipodocumento";
		gridViewTextBoxColumn24.EnableExpressionEditor = false;
		gridViewTextBoxColumn24.FieldName = "vendedor";
		gridViewTextBoxColumn24.HeaderText = "Vendedor";
		gridViewTextBoxColumn24.Name = "colVendedor";
		gridViewTextBoxColumn24.Width = 81;
		gridViewTextBoxColumn25.EnableExpressionEditor = false;
		gridViewTextBoxColumn25.FieldName = "codNotaIngreso";
		gridViewTextBoxColumn25.HeaderText = "codNotaIngreso";
		gridViewTextBoxColumn25.IsVisible = false;
		gridViewTextBoxColumn25.Name = "codNotaIngreso";
		gridViewTextBoxColumn25.Width = 48;
		gridViewTextBoxColumn26.EnableExpressionEditor = false;
		gridViewTextBoxColumn26.FieldName = "codNotaCredito";
		gridViewTextBoxColumn26.HeaderText = "codNotaCredito";
		gridViewTextBoxColumn26.IsVisible = false;
		gridViewTextBoxColumn26.Name = "codNotaCredito";
		gridViewTextBoxColumn27.EnableExpressionEditor = false;
		gridViewTextBoxColumn27.FieldName = "nombre_documento";
		gridViewTextBoxColumn27.HeaderText = "nombre_documento";
		gridViewTextBoxColumn27.IsVisible = false;
		gridViewTextBoxColumn27.Name = "nombre_documento";
		gridViewTextBoxColumn27.Width = 100;
		gridViewTextBoxColumn28.EnableExpressionEditor = false;
		gridViewTextBoxColumn28.FieldName = "nombre_dia";
		gridViewTextBoxColumn28.HeaderText = "nombre_dia";
		gridViewTextBoxColumn28.IsVisible = false;
		gridViewTextBoxColumn28.Name = "nombre_dia";
		gridViewTextBoxColumn28.Width = 100;
		gridViewTextBoxColumn29.EnableExpressionEditor = false;
		gridViewTextBoxColumn29.FieldName = "hora";
		gridViewTextBoxColumn29.HeaderText = "hora";
		gridViewTextBoxColumn29.IsVisible = false;
		gridViewTextBoxColumn29.Name = "hora";
		gridViewTextBoxColumn29.Width = 100;
		gridViewTextBoxColumn30.EnableExpressionEditor = false;
		gridViewTextBoxColumn30.FieldName = "cod_despacho";
		gridViewTextBoxColumn30.HeaderText = "CodDespacho";
		gridViewTextBoxColumn30.IsVisible = false;
		gridViewTextBoxColumn30.Name = "cod_despacho";
		gridViewTextBoxColumn30.Width = 47;
		gridViewTextBoxColumn31.EnableExpressionEditor = false;
		gridViewTextBoxColumn31.FieldName = "estado_despacho";
		gridViewTextBoxColumn31.HeaderText = "estado_despacho";
		gridViewTextBoxColumn31.IsVisible = false;
		gridViewTextBoxColumn31.Name = "estado_despacho";
		gridViewTextBoxColumn31.Width = 51;
		gridViewTextBoxColumn32.EnableExpressionEditor = false;
		gridViewTextBoxColumn32.FieldName = "despacho_anulado";
		gridViewTextBoxColumn32.HeaderText = "despacho_anulado";
		gridViewTextBoxColumn32.IsVisible = false;
		gridViewTextBoxColumn32.Name = "despacho_anulado";
		gridViewTextBoxColumn32.Width = 52;
		gridViewTextBoxColumn33.EnableExpressionEditor = false;
		gridViewTextBoxColumn33.FieldName = "num_despacho";
		gridViewTextBoxColumn33.HeaderText = "Despacho";
		gridViewTextBoxColumn33.Name = "num_despacho";
		gridViewTextBoxColumn33.Width = 81;
		gridViewTextBoxColumn34.EnableExpressionEditor = false;
		gridViewTextBoxColumn34.FieldName = "tot_credito";
		gridViewTextBoxColumn34.HeaderText = "TOTA_CREDITO";
		gridViewTextBoxColumn34.IsVisible = false;
		gridViewTextBoxColumn34.Name = "tot_credito";
		gridViewTextBoxColumn34.Width = 49;
		gridViewTextBoxColumn35.EnableExpressionEditor = false;
		gridViewTextBoxColumn35.FieldName = "tot_pendiente";
		gridViewTextBoxColumn35.HeaderText = "TOT_PENDIENTE";
		gridViewTextBoxColumn35.Name = "tot_pendiente";
		gridViewTextBoxColumn35.Width = 11;
		gridViewTextBoxColumn36.EnableExpressionEditor = false;
		gridViewTextBoxColumn36.FieldName = "canalventa";
		gridViewTextBoxColumn36.HeaderText = "Canal Venta";
		gridViewTextBoxColumn36.Name = "canalventa";
		gridViewTextBoxColumn36.Width = 81;
		gridViewTextBoxColumn37.EnableExpressionEditor = false;
		gridViewTextBoxColumn37.FieldName = "banco";
		gridViewTextBoxColumn37.HeaderText = "Banco";
		gridViewTextBoxColumn37.Name = "banco";
		gridViewTextBoxColumn37.Width = 73;
		gridViewTextBoxColumn38.EnableExpressionEditor = false;
		gridViewTextBoxColumn38.FieldName = "cuenta";
		gridViewTextBoxColumn38.HeaderText = "Cuenta";
		gridViewTextBoxColumn38.Name = "cuenta";
		gridViewTextBoxColumn38.Width = 81;
		gridViewTextBoxColumn39.FieldName = "guiaremision";
		gridViewTextBoxColumn39.HeaderText = "GuiaF";
		gridViewTextBoxColumn39.Name = "guiaremision";
		gridViewTextBoxColumn39.Width = 49;
		this.dgvVentas1.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25, gridViewTextBoxColumn26, gridViewTextBoxColumn27, gridViewTextBoxColumn28, gridViewTextBoxColumn29, gridViewTextBoxColumn30, gridViewTextBoxColumn31, gridViewTextBoxColumn32, gridViewTextBoxColumn33, gridViewTextBoxColumn34, gridViewTextBoxColumn35, gridViewTextBoxColumn36, gridViewTextBoxColumn37, gridViewTextBoxColumn38, gridViewTextBoxColumn39);
		this.dgvVentas1.MasterTemplate.EnableFiltering = true;
		this.dgvVentas1.MasterTemplate.PageSize = 100;
		this.dgvVentas1.MasterTemplate.ShowHeaderCellButtons = true;
		this.dgvVentas1.MasterTemplate.ShowRowHeaderColumn = false;
		this.dgvVentas1.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgvVentas1.Name = "dgvVentas1";
		this.dgvVentas1.ReadOnly = true;
		this.dgvVentas1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.dgvVentas1.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 16, 240, 150);
		this.dgvVentas1.ShowGroupPanel = false;
		this.dgvVentas1.ShowHeaderCellButtons = true;
		this.dgvVentas1.Size = new System.Drawing.Size(1413, 444);
		this.dgvVentas1.TabIndex = 1;
		this.dgvVentas1.ThemeName = "Material";
		this.dgvVentas1.ViewRowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(dgvVentas1_ViewRowFormatting);
		this.dgvVentas1.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvVentas1_CellFormatting);
		this.dgvVentas1.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(dgvVentas1_ViewCellFormatting);
		this.dgvVentas1.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvVentas1_CellClick);
		this.dgvVentas1.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgvVentas1_CellDoubleClick);
		this.dgvVentas1.FilterExpressionChanged += new Telerik.WinControls.UI.GridViewFilterExpressionChangedEventHandler(dgvVentas1_FilterExpressionChanged);
		this.dgvVentas1.FilterChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(dgvVentas1_FilterChanged);
		this.dgvVentas1.CustomSorting += new Telerik.WinControls.UI.GridViewCustomSortingEventHandler(dgvVentas1_CustomSorting);
		this.dgvVentas.AllowUserToAddRows = false;
		this.dgvVentas.AllowUserToDeleteRows = false;
		this.dgvVentas.AllowUserToOrderColumns = true;
		this.dgvVentas.AllowUserToResizeRows = false;
		this.dgvVentas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvVentas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		this.dgvVentas.Columns.AddRange(this.codigo, this.fecha, this.documento, this.numdoc, this.codcliente, this.cliente, this.moneda, this.importe, this.formapago, this.fechapago, this.estado, this.impreso, this.NotaCredito, this.enviadoS, this.codTipoDocumento);
		this.dgvVentas.Location = new System.Drawing.Point(3, 16);
		this.dgvVentas.MultiSelect = false;
		this.dgvVentas.Name = "dgvVentas";
		this.dgvVentas.ReadOnly = true;
		this.dgvVentas.RowHeadersVisible = false;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvVentas.RowsDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvVentas.Size = new System.Drawing.Size(58, 402);
		this.dgvVentas.TabIndex = 0;
		this.dgvVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVentas_CellClick);
		this.dgvVentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvVentas_CellContentClick);
		this.dgvVentas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvPedidosPendientes_CellDoubleClick);
		this.dgvVentas.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvPedidosPendientes_RowStateChanged);
		this.codigo.DataPropertyName = "codFactura";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.Visible = false;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.DataPropertyName = "documento";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.documento.DefaultCellStyle = dataGridViewCellStyle4;
		this.documento.HeaderText = "T. doc.";
		this.documento.Name = "documento";
		this.documento.ReadOnly = true;
		this.documento.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.documento.Visible = false;
		this.numdoc.DataPropertyName = "numdoc";
		this.numdoc.HeaderText = "N° Documento";
		this.numdoc.Name = "numdoc";
		this.numdoc.ReadOnly = true;
		this.codcliente.DataPropertyName = "codcliente";
		this.codcliente.HeaderText = "Cod. Cliente";
		this.codcliente.Name = "codcliente";
		this.codcliente.ReadOnly = true;
		this.codcliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.codcliente.Visible = false;
		this.cliente.DataPropertyName = "cliente";
		this.cliente.HeaderText = "Cliente";
		this.cliente.Name = "cliente";
		this.cliente.ReadOnly = true;
		this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.importe.DataPropertyName = "total";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.importe.DefaultCellStyle = dataGridViewCellStyle5;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.formapago.DataPropertyName = "formapago";
		this.formapago.HeaderText = "F. pago";
		this.formapago.Name = "formapago";
		this.formapago.ReadOnly = true;
		this.formapago.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.fechapago.DataPropertyName = "fechapago";
		this.fechapago.HeaderText = "Fech. Pago";
		this.fechapago.Name = "fechapago";
		this.fechapago.ReadOnly = true;
		this.estado.DataPropertyName = "anulado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.impreso.DataPropertyName = "impreso";
		this.impreso.HeaderText = "Impreso";
		this.impreso.Name = "impreso";
		this.impreso.ReadOnly = true;
		this.impreso.Visible = false;
		this.NotaCredito.DataPropertyName = "NotaDeCredito";
		this.NotaCredito.HeaderText = "NotaCredito";
		this.NotaCredito.Name = "NotaCredito";
		this.NotaCredito.ReadOnly = true;
		this.enviadoS.DataPropertyName = "enviadoS";
		this.enviadoS.HeaderText = "Enviado";
		this.enviadoS.Name = "enviadoS";
		this.enviadoS.ReadOnly = true;
		this.codTipoDocumento.DataPropertyName = "codTipoDocumento";
		this.codTipoDocumento.HeaderText = "codTipoDoc";
		this.codTipoDocumento.Name = "codTipoDocumento";
		this.codTipoDocumento.ReadOnly = true;
		this.codTipoDocumento.Visible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "document_print.png");
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(261, 91);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 13);
		this.label6.TabIndex = 17;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(116, 92);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(44, 13);
		this.label5.TabIndex = 16;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(166, 88);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(91, 20);
		this.dtpDesde.TabIndex = 15;
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(304, 88);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(83, 20);
		this.dtpHasta.TabIndex = 14;
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
		this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList2.Images.SetKeyName(17, "Folder open.png");
		this.imageList2.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
		this.imageList2.Images.SetKeyName(19, "img_visto.jpg");
		this.lbfactbol.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lbfactbol.AutoSize = true;
		this.lbfactbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lbfactbol.ForeColor = System.Drawing.Color.Blue;
		this.lbfactbol.Location = new System.Drawing.Point(81, 17);
		this.lbfactbol.Name = "lbfactbol";
		this.lbfactbol.Size = new System.Drawing.Size(36, 17);
		this.lbfactbol.TabIndex = 36;
		this.lbfactbol.Text = "0.00";
		this.lbfactbol.Click += new System.EventHandler(lbfactbol_Click);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(15, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(60, 15);
		this.label1.TabIndex = 37;
		this.label1.Text = "T. Activos:";
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(7, 35);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(74, 15);
		this.label2.TabIndex = 38;
		this.label2.Text = "T. Anulados:";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.Blue;
		this.label3.Location = new System.Drawing.Point(81, 35);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(36, 17);
		this.label3.TabIndex = 39;
		this.label3.Text = "0.00";
		this.cmbAlmacenes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbAlmacenes.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbAlmacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbAlmacenes.Location = new System.Drawing.Point(491, 92);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.RootElement.ControlBounds = new System.Drawing.Rectangle(491, 92, 125, 20);
		this.cmbAlmacenes.RootElement.StretchVertically = true;
		this.cmbAlmacenes.Size = new System.Drawing.Size(125, 24);
		this.cmbAlmacenes.TabIndex = 40;
		this.cmbAlmacenes.ThemeName = "TelerikMetroBlue";
		this.cmbAlmacenes.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.lblicbper.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblicbper.AutoSize = true;
		this.lblicbper.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblicbper.ForeColor = System.Drawing.Color.Blue;
		this.lblicbper.Location = new System.Drawing.Point(236, 15);
		this.lblicbper.Name = "lblicbper";
		this.lblicbper.Size = new System.Drawing.Size(36, 17);
		this.lblicbper.TabIndex = 42;
		this.lblicbper.Text = "0.00";
		this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(167, 15);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(67, 15);
		this.label7.TabIndex = 41;
		this.label7.Text = "T. ICBPER:";
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.Blue;
		this.label4.Location = new System.Drawing.Point(236, 32);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(36, 17);
		this.label4.TabIndex = 47;
		this.label4.Text = "0.00";
		this.label8.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(190, 32);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(40, 15);
		this.label8.TabIndex = 46;
		this.label8.Text = "T. NC:";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.Transparent;
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Controls.Add(this.lbfactbol);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.lblicbper);
		this.groupBox2.Location = new System.Drawing.Point(636, 52);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(319, 76);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(339, 56);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(16, 13);
		this.txtNombreProducto.TabIndex = 61;
		this.txtNombreProducto.Text = "---";
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(298, 56);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(35, 13);
		this.label9.TabIndex = 60;
		this.label9.Text = "Prod.:";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(2, 494);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(112, 13);
		this.label10.TabIndex = 59;
		this.label10.Text = "Busqueda x Producto:";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(117, 53);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(171, 20);
		this.txtCodprod.TabIndex = 58;
		this.txtCodprod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.panel3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.panel3.Controls.Add(this.btnguiafacturacion);
		this.panel3.Controls.Add(this.chbverificarventas);
		this.panel3.Controls.Add(this.label23);
		this.panel3.Controls.Add(this.label20);
		this.panel3.Controls.Add(this.label16);
		this.panel3.Controls.Add(this.lblNotaCredito);
		this.panel3.Controls.Add(this.label22);
		this.panel3.Controls.Add(this.lblPendientes);
		this.panel3.Controls.Add(this.lblVentaCredito);
		this.panel3.Controls.Add(this.label25);
		this.panel3.Controls.Add(this.label26);
		this.panel3.Controls.Add(this.label27);
		this.panel3.Controls.Add(this.lblTarjeta);
		this.panel3.Controls.Add(this.label29);
		this.panel3.Controls.Add(this.lblTransferencia);
		this.panel3.Controls.Add(this.lblDeposito);
		this.panel3.Controls.Add(this.label32);
		this.panel3.Controls.Add(this.label33);
		this.panel3.Controls.Add(this.lblEfectivo);
		this.panel3.Controls.Add(this.label35);
		this.panel3.Controls.Add(this.btnSalir);
		this.panel3.Controls.Add(this.label18);
		this.panel3.Controls.Add(this.btnXML);
		this.panel3.Controls.Add(this.label5);
		this.panel3.Controls.Add(this.lblNC);
		this.panel3.Controls.Add(this.btnPdf);
		this.panel3.Controls.Add(this.label17);
		this.panel3.Controls.Add(this.txtNombreProducto);
		this.panel3.Controls.Add(this.lblPendiente);
		this.panel3.Controls.Add(this.label9);
		this.panel3.Controls.Add(this.lblVC);
		this.panel3.Controls.Add(this.label19);
		this.panel3.Controls.Add(this.txtCodprod);
		this.panel3.Controls.Add(this.label11);
		this.panel3.Controls.Add(this.groupBox2);
		this.panel3.Controls.Add(this.label15);
		this.panel3.Controls.Add(this.button4);
		this.panel3.Controls.Add(this.lblCajaSeparacion);
		this.panel3.Controls.Add(this.button3);
		this.panel3.Controls.Add(this.label12);
		this.panel3.Controls.Add(this.button2);
		this.panel3.Controls.Add(this.lbCheque);
		this.panel3.Controls.Add(this.cmbAlmacenes);
		this.panel3.Controls.Add(this.lbDeposito);
		this.panel3.Controls.Add(this.btnGuardar);
		this.panel3.Controls.Add(this.label13);
		this.panel3.Controls.Add(this.button1);
		this.panel3.Controls.Add(this.label14);
		this.panel3.Controls.Add(this.btnReporte);
		this.panel3.Controls.Add(this.lblSaldoCaja);
		this.panel3.Controls.Add(this.label6);
		this.panel3.Controls.Add(this.label21);
		this.panel3.Controls.Add(this.btnAnular);
		this.panel3.Controls.Add(this.dtpDesde);
		this.panel3.Controls.Add(this.dtpHasta);
		this.panel3.Controls.Add(this.btnIrPedido);
		this.panel3.Controls.Add(this.btnVistaSucursales);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel3.Location = new System.Drawing.Point(0, 463);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(1419, 123);
		this.panel3.TabIndex = 65;
		this.btnguiafacturacion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnguiafacturacion.Image = (System.Drawing.Image)resources.GetObject("btnguiafacturacion.Image");
		this.btnguiafacturacion.Location = new System.Drawing.Point(12, 42);
		this.btnguiafacturacion.Name = "btnguiafacturacion";
		this.btnguiafacturacion.Size = new System.Drawing.Size(85, 34);
		this.btnguiafacturacion.TabIndex = 84;
		this.btnguiafacturacion.Text = "Emitir Guia";
		this.btnguiafacturacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnguiafacturacion.UseVisualStyleBackColor = true;
		this.btnguiafacturacion.Click += new System.EventHandler(btnguiafacturacion_Click);
		this.chbverificarventas.AutoSize = true;
		this.chbverificarventas.Location = new System.Drawing.Point(491, 65);
		this.chbverificarventas.Name = "chbverificarventas";
		this.chbverificarventas.Size = new System.Drawing.Size(118, 17);
		this.chbverificarventas.TabIndex = 82;
		this.chbverificarventas.Text = "Verficar V. Contado";
		this.chbverificarventas.UseVisualStyleBackColor = true;
		this.chbverificarventas.CheckedChanged += new System.EventHandler(chbverificarventas_CheckedChanged);
		this.label23.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label23.AutoSize = true;
		this.label23.BackColor = System.Drawing.Color.Transparent;
		this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.ForeColor = System.Drawing.Color.Teal;
		this.label23.Location = new System.Drawing.Point(56, 29);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(72, 13);
		this.label23.TabIndex = 81;
		this.label23.Text = "FILTRADO:";
		this.label20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label20.AutoSize = true;
		this.label20.BackColor = System.Drawing.Color.Transparent;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label20.Location = new System.Drawing.Point(56, 7);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(67, 13);
		this.label20.TabIndex = 80;
		this.label20.Text = "TOTALES:";
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.BackColor = System.Drawing.Color.Transparent;
		this.label16.ForeColor = System.Drawing.Color.Teal;
		this.label16.Location = new System.Drawing.Point(137, 31);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(94, 13);
		this.label16.TabIndex = 79;
		this.label16.Text = "PENDIENTES S/:";
		this.lblNotaCredito.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblNotaCredito.BackColor = System.Drawing.Color.Transparent;
		this.lblNotaCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblNotaCredito.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblNotaCredito.ForeColor = System.Drawing.Color.Teal;
		this.lblNotaCredito.Location = new System.Drawing.Point(423, 28);
		this.lblNotaCredito.Name = "lblNotaCredito";
		this.lblNotaCredito.Size = new System.Drawing.Size(100, 20);
		this.lblNotaCredito.TabIndex = 75;
		this.lblNotaCredito.Text = "0.00";
		this.lblNotaCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label22.AutoSize = true;
		this.label22.BackColor = System.Drawing.Color.Transparent;
		this.label22.ForeColor = System.Drawing.Color.Teal;
		this.label22.Location = new System.Drawing.Point(334, 30);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(87, 13);
		this.label22.TabIndex = 74;
		this.label22.Text = "N. CREDITO S/:";
		this.lblPendientes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblPendientes.BackColor = System.Drawing.Color.Transparent;
		this.lblPendientes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblPendientes.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblPendientes.ForeColor = System.Drawing.Color.Teal;
		this.lblPendientes.Location = new System.Drawing.Point(233, 28);
		this.lblPendientes.Name = "lblPendientes";
		this.lblPendientes.Size = new System.Drawing.Size(100, 20);
		this.lblPendientes.TabIndex = 78;
		this.lblPendientes.Text = "0.00";
		this.lblPendientes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblVentaCredito.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblVentaCredito.BackColor = System.Drawing.Color.Transparent;
		this.lblVentaCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblVentaCredito.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblVentaCredito.ForeColor = System.Drawing.Color.Teal;
		this.lblVentaCredito.Location = new System.Drawing.Point(612, 30);
		this.lblVentaCredito.Name = "lblVentaCredito";
		this.lblVentaCredito.Size = new System.Drawing.Size(100, 20);
		this.lblVentaCredito.TabIndex = 77;
		this.lblVentaCredito.Text = "0.00";
		this.lblVentaCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label25.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label25.AutoSize = true;
		this.label25.BackColor = System.Drawing.Color.Transparent;
		this.label25.ForeColor = System.Drawing.Color.Teal;
		this.label25.Location = new System.Drawing.Point(525, 33);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(86, 13);
		this.label25.TabIndex = 76;
		this.label25.Text = "V. CREDITO S/:";
		this.label26.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label26.BackColor = System.Drawing.Color.Transparent;
		this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label26.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.ForeColor = System.Drawing.Color.Teal;
		this.label26.Location = new System.Drawing.Point(423, 28);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(100, 20);
		this.label26.TabIndex = 73;
		this.label26.Text = "0.00";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label27.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label27.AutoSize = true;
		this.label27.BackColor = System.Drawing.Color.Transparent;
		this.label27.ForeColor = System.Drawing.Color.Teal;
		this.label27.Location = new System.Drawing.Point(343, 32);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(70, 13);
		this.label27.TabIndex = 72;
		this.label27.Text = "MASTER S/:";
		this.lblTarjeta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTarjeta.BackColor = System.Drawing.Color.Transparent;
		this.lblTarjeta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblTarjeta.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTarjeta.ForeColor = System.Drawing.Color.Teal;
		this.lblTarjeta.Location = new System.Drawing.Point(1133, 30);
		this.lblTarjeta.Name = "lblTarjeta";
		this.lblTarjeta.Size = new System.Drawing.Size(99, 20);
		this.lblTarjeta.TabIndex = 71;
		this.lblTarjeta.Text = "0.00";
		this.lblTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label29.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label29.AutoSize = true;
		this.label29.BackColor = System.Drawing.Color.Transparent;
		this.label29.ForeColor = System.Drawing.Color.Teal;
		this.label29.Location = new System.Drawing.Point(711, 33);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(80, 13);
		this.label29.TabIndex = 70;
		this.label29.Text = "DEPOSITO S/:";
		this.lblTransferencia.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblTransferencia.BackColor = System.Drawing.Color.Transparent;
		this.lblTransferencia.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblTransferencia.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTransferencia.ForeColor = System.Drawing.Color.Teal;
		this.lblTransferencia.Location = new System.Drawing.Point(959, 31);
		this.lblTransferencia.Name = "lblTransferencia";
		this.lblTransferencia.Size = new System.Drawing.Size(100, 20);
		this.lblTransferencia.TabIndex = 69;
		this.lblTransferencia.Text = "0.00";
		this.lblTransferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblDeposito.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblDeposito.BackColor = System.Drawing.Color.Transparent;
		this.lblDeposito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblDeposito.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblDeposito.ForeColor = System.Drawing.Color.Teal;
		this.lblDeposito.Location = new System.Drawing.Point(793, 30);
		this.lblDeposito.Name = "lblDeposito";
		this.lblDeposito.Size = new System.Drawing.Size(100, 20);
		this.lblDeposito.TabIndex = 68;
		this.lblDeposito.Text = "0.00";
		this.lblDeposito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label32.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label32.AutoSize = true;
		this.label32.BackColor = System.Drawing.Color.Transparent;
		this.label32.ForeColor = System.Drawing.Color.Teal;
		this.label32.Location = new System.Drawing.Point(892, 35);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(62, 13);
		this.label32.TabIndex = 67;
		this.label32.Text = "TRANS S/:";
		this.label33.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label33.AutoSize = true;
		this.label33.BackColor = System.Drawing.Color.Transparent;
		this.label33.ForeColor = System.Drawing.Color.Teal;
		this.label33.Location = new System.Drawing.Point(1058, 33);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(73, 13);
		this.label33.TabIndex = 66;
		this.label33.Text = "TARJETA S/:";
		this.lblEfectivo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblEfectivo.BackColor = System.Drawing.Color.Transparent;
		this.lblEfectivo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblEfectivo.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblEfectivo.ForeColor = System.Drawing.Color.Teal;
		this.lblEfectivo.Location = new System.Drawing.Point(1310, 29);
		this.lblEfectivo.Name = "lblEfectivo";
		this.lblEfectivo.Size = new System.Drawing.Size(100, 20);
		this.lblEfectivo.TabIndex = 65;
		this.lblEfectivo.Text = "0.00";
		this.lblEfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label35.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label35.AutoSize = true;
		this.label35.BackColor = System.Drawing.Color.Transparent;
		this.label35.ForeColor = System.Drawing.Color.Teal;
		this.label35.Location = new System.Drawing.Point(1234, 33);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(77, 13);
		this.label35.TabIndex = 64;
		this.label35.Text = "EFECTIVO S/:";
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1330, 64);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(86, 41);
		this.btnSalir.TabIndex = 1;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.BackColor = System.Drawing.Color.Transparent;
		this.label18.Location = new System.Drawing.Point(137, 7);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(94, 13);
		this.label18.TabIndex = 62;
		this.label18.Text = "PENDIENTES S/:";
		this.btnXML.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnXML.Image = (System.Drawing.Image)resources.GetObject("btnXML.Image");
		this.btnXML.Location = new System.Drawing.Point(954, 65);
		this.btnXML.Name = "btnXML";
		this.btnXML.Size = new System.Drawing.Size(46, 42);
		this.btnXML.TabIndex = 63;
		this.btnXML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnXML.UseVisualStyleBackColor = true;
		this.btnXML.Click += new System.EventHandler(btnXML_Click);
		this.lblNC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblNC.BackColor = System.Drawing.Color.Transparent;
		this.lblNC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblNC.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblNC.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblNC.Location = new System.Drawing.Point(423, 4);
		this.lblNC.Name = "lblNC";
		this.lblNC.Size = new System.Drawing.Size(100, 20);
		this.lblNC.TabIndex = 58;
		this.lblNC.Text = "0.00";
		this.lblNC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnPdf.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnPdf.Image = (System.Drawing.Image)resources.GetObject("btnPdf.Image");
		this.btnPdf.Location = new System.Drawing.Point(1001, 64);
		this.btnPdf.Name = "btnPdf";
		this.btnPdf.Size = new System.Drawing.Size(53, 42);
		this.btnPdf.TabIndex = 62;
		this.btnPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnPdf.UseVisualStyleBackColor = true;
		this.btnPdf.Click += new System.EventHandler(btnPdf_Click);
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.BackColor = System.Drawing.Color.Transparent;
		this.label17.Location = new System.Drawing.Point(334, 6);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(87, 13);
		this.label17.TabIndex = 57;
		this.label17.Text = "N. CREDITO S/:";
		this.lblPendiente.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblPendiente.BackColor = System.Drawing.Color.Transparent;
		this.lblPendiente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblPendiente.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblPendiente.ForeColor = System.Drawing.Color.OrangeRed;
		this.lblPendiente.Location = new System.Drawing.Point(233, 4);
		this.lblPendiente.Name = "lblPendiente";
		this.lblPendiente.Size = new System.Drawing.Size(100, 20);
		this.lblPendiente.TabIndex = 61;
		this.lblPendiente.Text = "0.00";
		this.lblPendiente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblVC.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblVC.BackColor = System.Drawing.Color.Transparent;
		this.lblVC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblVC.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblVC.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblVC.Location = new System.Drawing.Point(612, 6);
		this.lblVC.Name = "lblVC";
		this.lblVC.Size = new System.Drawing.Size(100, 20);
		this.lblVC.TabIndex = 60;
		this.lblVC.Text = "0.00";
		this.lblVC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label19.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label19.AutoSize = true;
		this.label19.BackColor = System.Drawing.Color.Transparent;
		this.label19.Location = new System.Drawing.Point(525, 9);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(86, 13);
		this.label19.TabIndex = 59;
		this.label19.Text = "V. CREDITO S/:";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.label11.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label11.Location = new System.Drawing.Point(423, 4);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(100, 20);
		this.label11.TabIndex = 56;
		this.label11.Text = "0.00";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.BackColor = System.Drawing.Color.Transparent;
		this.label15.Location = new System.Drawing.Point(343, 8);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(70, 13);
		this.label15.TabIndex = 55;
		this.label15.Text = "MASTER S/:";
		this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button4.Image = (System.Drawing.Image)resources.GetObject("button4.Image");
		this.button4.Location = new System.Drawing.Point(1054, 64);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(75, 42);
		this.button4.TabIndex = 45;
		this.button4.Text = "CDR";
		this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button4.UseVisualStyleBackColor = true;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.lblCajaSeparacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblCajaSeparacion.BackColor = System.Drawing.Color.Transparent;
		this.lblCajaSeparacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblCajaSeparacion.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCajaSeparacion.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblCajaSeparacion.Location = new System.Drawing.Point(1133, 6);
		this.lblCajaSeparacion.Name = "lblCajaSeparacion";
		this.lblCajaSeparacion.Size = new System.Drawing.Size(99, 20);
		this.lblCajaSeparacion.TabIndex = 54;
		this.lblCajaSeparacion.Text = "0.00";
		this.lblCajaSeparacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button3.Image = (System.Drawing.Image)resources.GetObject("button3.Image");
		this.button3.Location = new System.Drawing.Point(1130, 65);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(127, 41);
		this.button3.TabIndex = 44;
		this.button3.Text = "GENERAR XML";
		this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button3.UseVisualStyleBackColor = true;
		this.button3.Click += new System.EventHandler(button3_Click_1);
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.BackColor = System.Drawing.Color.Transparent;
		this.label12.Location = new System.Drawing.Point(711, 9);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(80, 13);
		this.label12.TabIndex = 53;
		this.label12.Text = "DEPOSITO S/:";
		this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.button2.ImageIndex = 7;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(1183, 66);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(74, 41);
		this.button2.TabIndex = 43;
		this.button2.Text = "PDF";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.lbCheque.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lbCheque.BackColor = System.Drawing.Color.Transparent;
		this.lbCheque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbCheque.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbCheque.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lbCheque.Location = new System.Drawing.Point(959, 7);
		this.lbCheque.Name = "lbCheque";
		this.lbCheque.Size = new System.Drawing.Size(100, 20);
		this.lbCheque.TabIndex = 50;
		this.lbCheque.Text = "0.00";
		this.lbCheque.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbDeposito.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lbDeposito.BackColor = System.Drawing.Color.Transparent;
		this.lbDeposito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lbDeposito.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDeposito.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lbDeposito.Location = new System.Drawing.Point(793, 6);
		this.lbDeposito.Name = "lbDeposito";
		this.lbDeposito.Size = new System.Drawing.Size(100, 20);
		this.lbDeposito.TabIndex = 49;
		this.lbDeposito.Text = "0.00";
		this.lbDeposito.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnGuardar.ImageIndex = 3;
		this.btnGuardar.ImageList = this.imageList2;
		this.btnGuardar.Location = new System.Drawing.Point(1186, 69);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(30, 38);
		this.btnGuardar.TabIndex = 35;
		this.btnGuardar.Text = "Enviar a Pendiente ";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Visible = false;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.BackColor = System.Drawing.Color.Transparent;
		this.label13.Location = new System.Drawing.Point(892, 11);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(62, 13);
		this.label13.TabIndex = 48;
		this.label13.Text = "TRANS S/:";
		this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.button1.ImageIndex = 8;
		this.button1.ImageList = this.imageList2;
		this.button1.Location = new System.Drawing.Point(392, 69);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(93, 46);
		this.button1.TabIndex = 31;
		this.button1.Text = "Actualizar";
		this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.BackColor = System.Drawing.Color.Transparent;
		this.label14.Location = new System.Drawing.Point(1058, 9);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(73, 13);
		this.label14.TabIndex = 47;
		this.label14.Text = "TARJETA S/:";
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.ImageIndex = 7;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(1258, 64);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(71, 41);
		this.btnReporte.TabIndex = 30;
		this.btnReporte.Text = "Excel";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.lblSaldoCaja.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblSaldoCaja.BackColor = System.Drawing.Color.Transparent;
		this.lblSaldoCaja.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblSaldoCaja.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSaldoCaja.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.lblSaldoCaja.Location = new System.Drawing.Point(1310, 5);
		this.lblSaldoCaja.Name = "lblSaldoCaja";
		this.lblSaldoCaja.Size = new System.Drawing.Size(100, 20);
		this.lblSaldoCaja.TabIndex = 46;
		this.lblSaldoCaja.Text = "0.00";
		this.lblSaldoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label21.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label21.AutoSize = true;
		this.label21.BackColor = System.Drawing.Color.Transparent;
		this.label21.Location = new System.Drawing.Point(1234, 9);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(77, 13);
		this.label21.TabIndex = 40;
		this.label21.Text = "EFECTIVO S/:";
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnAnular.ImageIndex = 4;
		this.btnAnular.ImageList = this.imageList1;
		this.btnAnular.Location = new System.Drawing.Point(12, 81);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(85, 36);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular / NC";
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btnIrPedido.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnIrPedido.ImageIndex = 1;
		this.btnIrPedido.ImageList = this.imageList1;
		this.btnIrPedido.Location = new System.Drawing.Point(1330, 64);
		this.btnIrPedido.Name = "btnIrPedido";
		this.btnIrPedido.Size = new System.Drawing.Size(86, 40);
		this.btnIrPedido.TabIndex = 2;
		this.btnIrPedido.Text = "Consulta";
		this.btnIrPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnIrPedido.UseVisualStyleBackColor = true;
		this.btnIrPedido.Click += new System.EventHandler(btnIrPedido_Click);
		this.btnVistaSucursales.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnVistaSucursales.ImageIndex = 19;
		this.btnVistaSucursales.ImageList = this.imageList2;
		this.btnVistaSucursales.Location = new System.Drawing.Point(120, 75);
		this.btnVistaSucursales.Name = "btnVistaSucursales";
		this.btnVistaSucursales.Size = new System.Drawing.Size(40, 14);
		this.btnVistaSucursales.TabIndex = 34;
		this.btnVistaSucursales.Text = "Activar Vista";
		this.btnVistaSucursales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnVistaSucursales.UseVisualStyleBackColor = true;
		this.btnVistaSucursales.Visible = false;
		this.btnVistaSucursales.Click += new System.EventHandler(btnVistaSucursales_Click);
		this.panel1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
		this.panel1.Controls.Add(this.groupBox1);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(1419, 463);
		this.panel1.TabIndex = 66;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1419, 586);
		base.Controls.Add(this.panel1);
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.label10);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmVentas";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Ventas";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmPedidosPendientes_Load);
		base.Shown += new System.EventHandler(frmVentas_Shown);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgvVentas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.panel3.ResumeLayout(false);
		this.panel3.PerformLayout();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
