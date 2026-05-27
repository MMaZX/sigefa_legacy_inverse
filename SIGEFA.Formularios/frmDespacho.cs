using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios;

public class frmDespacho : Form
{
	public int codDespacho = 0;

	internal clsAdmDespacho admdespacho = new clsAdmDespacho();

	internal clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	internal clsUsuario usuario_click = null;

	private clsDespacho despacho = null;

	private clsAdmFormulario admForm = new clsAdmFormulario();

	internal clsSerie ser = null;

	internal DataTable dataCtdadGenerarEntrega = null;

	internal int Proceso;

	private clsAdmSerie admSerie = new clsAdmSerie();

	private int CodCliente = 0;

	private clsFacturaVenta venta = null;

	private clsAdmFacturaVenta admfactventa = new clsAdmFacturaVenta();

	internal string codFacturaVenta = "";

	private bool cerrarVentana = false;

	private clsValidar valida = new clsValidar();

	private clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();

	private IContainer components = null;

	private GroupBox groupBox1;

	private TextBox txtNombreRazonSocial;

	private TextBox txtRucDni;

	private Label label2;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Button btnSalir;

	private ImageList imageList1;

	private DataGridView dgvDetalle;

	private Button btnImprimir;

	private Button btnGenerar;

	private Button btnGuardar;

	private DataGridView dgvEntregas;

	private DataGridViewTextBoxColumn colCodEntrega;

	private DataGridViewTextBoxColumn colTituloEntrega;

	private DataGridViewTextBoxColumn colFecha;

	private DataGridViewTextBoxColumn colEstado;

	private TextBox txtDocumentoRelacionado;

	private Label label3;

	private DateTimePicker dtpFechaDespacho;

	private Label label4;

	private TextBox txtRefDespacho;

	private Label label1;

	private Button btnAnular;

	private GroupBox gbContactoAEntregar;

	private TextBox txtTelefonoContacto;

	private Label label7;

	private TextBox txtNombreContacto;

	private Label label6;

	private TextBox txtReqAlm;

	private Label lblRequerimiento;

	private TextBox txtEstado;

	private Label label8;

	private TextBox txtNotaCredito;

	private Label lblNotaCredito;

	private Button btnImprimirPDF;

	private Label lblAnulado;

	private TextBox txtusuariosolic;

	private Label lblusuariosolic;

	private Button btnLimpiarCantidadEntregar;

	private Button btnRellenarCantidadPendiente;

	private TextBox textBox1;

	private TextBox textBox2;

	private Label label5;

	private DataGridViewTextBoxColumn colCodDetalleDespacho;

	private DataGridViewTextBoxColumn colCodDespacho;

	private DataGridViewTextBoxColumn colCodAlmacen;

	private DataGridViewTextBoxColumn colDescAlmacen;

	private DataGridViewTextBoxColumn colCodProducto;

	private DataGridViewTextBoxColumn colReferencia;

	private DataGridViewTextBoxColumn colDescProducto;

	private DataGridViewTextBoxColumn colCodUnidad;

	private DataGridViewTextBoxColumn colDescUnidad;

	private DataGridViewTextBoxColumn colCantidadOriginal;

	private DataGridViewTextBoxColumn colCantidad;

	private DataGridViewTextBoxColumn colCantidadPendiente;

	private DataGridViewTextBoxColumn colCtdadEntregar;

	private DataGridViewTextBoxColumn orden;

	private Button btnResumenEntregas;

	private TextBox txtDelivery;

	private Label label9;

	private TextBox txtComentario;

	private Label label10;

	public frmDespacho()
	{
		InitializeComponent();
	}

	private void frmDespacho_Load(object sender, EventArgs e)
	{
		bool visible;
		if (codDespacho != 0)
		{
			despacho = admdespacho.cargaDespacho(codDespacho);
			dgvEntregas.DataSource = admdespacho.listaEntregas(codDespacho);
			muestraDespachoEnForm();
			cargaDetalle();
			Button button = btnImprimir;
			visible = (btnImprimirPDF.Visible = true);
			button.Visible = visible;
			btnGuardar.Enabled = false;
			btnGenerar.Visible = despacho.CodEstado != 16;
			if (Proceso == 2)
			{
				btnGenerar.Visible = false;
			}
			return;
		}
		GroupBox groupBox = gbContactoAEntregar;
		DataGridView dataGridView = dgvEntregas;
		bool flag2 = (btnGenerar.Visible = false);
		visible = (dataGridView.Visible = flag2);
		groupBox.Visible = visible;
		Label label = lblRequerimiento;
		TextBox textBox = txtReqAlm;
		Label label2 = lblNotaCredito;
		bool flag5 = (txtNotaCredito.Visible = false);
		flag2 = (label2.Visible = flag5);
		visible = (textBox.Visible = flag2);
		label.Visible = visible;
		txtEstado.Text = "NUEVO";
		txtRefDespacho.Text = 0.ToString().PadLeft(8, '0');
		if (Proceso != 3 || !(codFacturaVenta != ""))
		{
			return;
		}
		venta = admfactventa.CargaFacturaVenta(Convert.ToInt32(codFacturaVenta));
		CodCliente = venta.CodCliente;
		txtRucDni.Text = ((venta.RUCCliente == "") ? "00000000" : venta.RUCCliente);
		txtNombreRazonSocial.Text = venta.RazonSocialCliente;
		txtDocumentoRelacionado.Text = venta.SiglaDocumento + "-" + venta.NumDoc.PadLeft(8, '0');
		gbContactoAEntregar.Visible = true;
		TextBox textBox2 = txtTelefonoContacto;
		TextBox textBox3 = txtNombreContacto;
		flag2 = (txtDelivery.ReadOnly = false);
		visible = (textBox3.ReadOnly = flag2);
		textBox2.ReadOnly = visible;
		Label label3 = lblRequerimiento;
		TextBox textBox4 = txtReqAlm;
		Label label4 = lblNotaCredito;
		flag5 = (txtNotaCredito.Visible = true);
		flag2 = (label4.Visible = flag5);
		visible = (textBox4.Visible = flag2);
		label3.Visible = visible;
		int rpta = admdespacho.ObtenerDatoAntesDeListarDetalleDespacho3(1, Convert.ToInt32(venta.CodFacturaVenta));
		if (rpta == 2 || rpta == 3)
		{
			dgvDetalle.DataSource = admdespacho.ListaDetalleDespacho3(1, Convert.ToInt32(venta.CodFacturaVenta));
			dgvDetalle.Columns[colCtdadEntregar.Name].Visible = true;
			Button button2 = btnRellenarCantidadPendiente;
			visible = (btnLimpiarCantidadEntregar.Visible = true);
			button2.Visible = visible;
			if (rpta == 3)
			{
				despacho = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
				muestraDespachoEnForm();
				btnResumenEntregas.Visible = false;
			}
			return;
		}
		MessageBox.Show("No se puede asignar productos de despacho a la factura: " + txtDocumentoRelacionado.Text, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		if (rpta == 4)
		{
			DialogResult _rspta = MessageBox.Show("Ya existe un despacho para esta venta. Desea abrir el despacho?", "Despacho Generado", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
			if (_rspta == DialogResult.Yes)
			{
				despacho = admdespacho.cargaDespachoSegunDocRelacionado(1, codFacturaVenta);
				codDespacho = despacho.CodDespacho;
				recargarPagina(1);
			}
		}
		cerrarVentana = true;
	}

	private void cargaDetalle()
	{
		dgvDetalle.DataSource = admdespacho.ListaDetalleDespacho2(despacho.CodDespacho, frmLogin.iCodAlmacen);
	}

	private void pintaFilas()
	{
		foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
		{
			if (Convert.ToInt32(fila.Cells[colCantidadPendiente.Name].Value) > 0)
			{
				fila.DefaultCellStyle.BackColor = Color.FromArgb(250, 157, 206);
			}
			else
			{
				fila.DefaultCellStyle.BackColor = Color.FromArgb(105, 191, 255);
			}
		}
	}

	private void muestraDespachoEnForm()
	{
		dtpFechaDespacho.Value = despacho.FechaDespacho;
		txtRefDespacho.Text = despacho.Serie + "-" + despacho.Numeracion;
		txtRucDni.Text = despacho.RucDni.ToString();
		txtNombreRazonSocial.Text = ((despacho.RazonSocial == "") ? despacho.NombreCliente : despacho.RazonSocial);
		txtDocumentoRelacionado.Text = despacho.TituloDocRelacionado;
		txtNombreContacto.Text = despacho.NombreContacto;
		txtTelefonoContacto.Text = despacho.TelefonoContacto;
		txtReqAlm.Text = despacho.TituloReqAlmacen;
		txtEstado.Text = despacho.DescripEstado;
		txtNotaCredito.Text = despacho.DescripNotaCredito;
		dgvDetalle.Columns[colCantidadOriginal.Name].Visible = txtNotaCredito.Text != "";
		lblAnulado.Visible = despacho.CodEstado == 18;
		clsRequerimientoAlmacen reqalm = admreqalm.CargaRequerimiento(despacho.codReqAlmRelacionado);
		lblusuariosolic.Text = ((reqalm == null) ? "CREADO POR:" : lblusuariosolic.Text);
		txtusuariosolic.Text = ((reqalm == null) ? despacho.UserRegistro : reqalm.UserRegistro);
		txtDelivery.Text = despacho.DireccionDelivery;
		txtComentario.Text = despacho.Comentario;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (Proceso == 3)
		{
			if (MessageBox.Show("Esta seguro de salir sin guardar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				Close();
			}
		}
		else
		{
			Close();
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (Proceso == 3)
			{
				if (dgvDetalle.RowCount > 0)
				{
					if (!verificarEntrega() || !validarContacto())
					{
						return;
					}
					if (despacho == null)
					{
						clsTipoDocumento doc = admtd.BuscaTipoDocumento("DESP");
						ser = admSerie.CargaSerieEmpresa(venta.CodAlmacen, doc.CodTipoDocumento);
						if (ser == null)
						{
							return;
						}
						clsDespacho desp = new clsDespacho();
						desp = obtenerDespachoDeForm();
						desp.CodAlmacenRegistro = venta.CodAlmacen;
						desp.CodUserRegistro = ((usuario_click == null) ? frmLogin.iCodUser : usuario_click.CodUsuario);
						List<clsDetalleDespacho> detalleDespacho = obtenerDetalleDespacho();
						using TransactionScope Scope = new TransactionScope();
						try
						{
							bool rpta = admdespacho.insert(desp);
							if (rpta)
							{
								foreach (clsDetalleDespacho item in detalleDespacho)
								{
									item.CodDespacho = desp.CodDespacho;
									rpta = admdespacho.insertDetalle(item);
									if (!rpta)
									{
										break;
									}
								}
							}
							if (!rpta)
							{
								throw new Exception("No se pudo guardar \nDespacho relacionado a la venta: " + txtDocumentoRelacionado.Text + "\nCodAlmacen de Venta: " + venta.CodAlmacen);
							}
							Scope.Complete();
							Scope.Dispose();
							MessageBox.Show("Despacho Guardado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							admdespacho.cambioEstado(desp.CodDespacho, 14);
						}
						catch (Exception ex)
						{
							Transaction.Current.Rollback();
							Scope.Dispose();
							throw ex;
						}
						despacho = desp;
						generarEntrega(detalleDespacho);
						codDespacho = despacho.CodDespacho;
						recargarPagina(1);
						return;
					}
					if (MessageBox.Show("Se añadiran estos productos al Despacho Actual: DESP " + txtRefDespacho.Text + "\nDesea continuar?", "Asignando Productos a Despacho", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
					{
						return;
					}
					List<clsDetalleDespacho> detalleDespacho2 = obtenerDetalleDespacho();
					using TransactionScope Scope2 = new TransactionScope();
					try
					{
						despacho.NombreContacto = txtNombreContacto.Text.Trim();
						despacho.TelefonoContacto = txtTelefonoContacto.Text.Trim();
						despacho.DireccionDelivery = txtDelivery.Text.Trim();
						bool rpta2 = admdespacho.update(despacho);
						if (rpta2)
						{
							foreach (clsDetalleDespacho item2 in detalleDespacho2)
							{
								item2.CodDespacho = despacho.CodDespacho;
								rpta2 = admdespacho.updateDetalle(item2);
								if (!rpta2)
								{
									break;
								}
							}
						}
						if (!rpta2)
						{
							throw new Exception("No se pudo asignar productos al DESP " + txtRefDespacho.Text + " \nDespacho relacionado a la venta: " + txtDocumentoRelacionado.Text + "\nCodAlmacen de Venta: " + venta.CodAlmacen);
						}
						Scope2.Complete();
						Scope2.Dispose();
						MessageBox.Show("Despacho Actualizado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					catch (Exception ex2)
					{
						Transaction.Current.Rollback();
						Scope2.Dispose();
						throw ex2;
					}
					generarEntrega(detalleDespacho2);
					codDespacho = despacho.CodDespacho;
					recargarPagina(1);
					return;
				}
				MessageBox.Show("no hay productos que guardar para despacho", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MessageBox.Show("Proceso no definido para despacho", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex3)
		{
			MessageBox.Show(ex3.Message, "Ocurrio Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void generarEntrega(List<clsDetalleDespacho> detalleDespacho)
	{
		try
		{
			List<clsDetalleEntrega> detalle = obtenerDetalleParaEntrega2(detalleDespacho);
			if (detalle.Count > 0)
			{
				using (TransactionScope Scope = new TransactionScope())
				{
					clsEntrega entrega = new clsEntrega();
					clsTipoDocumento tipodoc = admtd.BuscaTipoDocumento("ENTREGA");
					clsSerie serent = admSerie.CargaSerieEmpresa(despacho.CodAlmacenRegistro, tipodoc.CodTipoDocumento);
					entrega.CodSerie = serent.CodSerie;
					entrega.Serie = despacho.Serie;
					entrega.Numeracion = serent.Numeracion.ToString().PadLeft(8, '0');
					entrega.CodAlmacenRegistro = frmLogin.iCodAlmacen;
					entrega.CodDespacho = ((codDespacho > 0) ? codDespacho : despacho.CodDespacho);
					entrega.CodUsuario = ((usuario_click == null) ? frmLogin.iCodUser : usuario_click.CodUsuario);
					DateTime fechaRegistro = (entrega.FechaEntrega = DateTime.Now);
					entrega.FechaRegistro = fechaRegistro;
					entrega.NombreCliente = txtNombreRazonSocial.Text;
					bool rpta = admdespacho.registrarEntrega(entrega);
					if (rpta)
					{
						foreach (clsDetalleEntrega det in detalle)
						{
							det.CodEntrega = entrega.CodEntrega;
							rpta = admdespacho.insertDetalleEntrega(det);
							if (!rpta)
							{
								break;
							}
						}
						if (rpta)
						{
							admdespacho.actualizaCantidadPendienteDespacho((codDespacho > 0) ? codDespacho : despacho.CodDespacho);
						}
					}
					if (!rpta)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						throw new Exception("No se pudo guardar Entrega");
					}
					Scope.Complete();
					Scope.Dispose();
					MessageBox.Show("Entrega Generada Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					int codEstado = admdespacho.obtenerCodEstado(despacho.CodDespacho);
					if (codEstado != -1)
					{
						admdespacho.cambioEstado(despacho.CodDespacho, codEstado);
					}
					if (despacho.codReqAlmRelacionado > 0 && codEstado == 16)
					{
						admreqalm.actualizaEstadoReqAlmacen(despacho.codReqAlmRelacionado, 11);
					}
					return;
				}
			}
			MessageBox.Show("No Se Ah Generado Entrega", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private List<clsDetalleEntrega> obtenerDetalleParaEntrega2(List<clsDetalleDespacho> detalleDespacho)
	{
		List<clsDetalleEntrega> detalle = new List<clsDetalleEntrega>();
		foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
		{
			object valor = fila.Cells[colCtdadEntregar.Name].Value;
			string cadena = ((valor == null || valor == "") ? "0" : valor.ToString());
			double cantidad = Convert.ToDouble(cadena);
			if (cantidad > 0.0)
			{
				clsDetalleEntrega deta = new clsDetalleEntrega();
				List<clsDetalleDespacho> encontrado = Enumerable.Where<clsDetalleDespacho>(detalleDespacho.Cast<clsDetalleDespacho>(), (Func<clsDetalleDespacho, bool>)((clsDetalleDespacho x) => Convert.ToInt32(fila.Cells[colCodProducto.Name].Value.ToString()) == x.CodProducto && Convert.ToInt32(fila.Cells[colCodUnidad.Name].Value.ToString()) == x.CodUnidad && Convert.ToInt32(fila.Cells[colCodAlmacen.Name].Value.ToString()) == x.CodAlmacenEntregar)).ToList();
				if (encontrado.Count <= 0)
				{
					throw new Exception("Ocurrio un error al tratar de generar el detalle de la entrega.\nIntente nuevamente.");
				}
				clsDetalleDespacho clase = encontrado[0];
				deta.Cantidad = cantidad;
				deta.CodAlmacenEntregar = clase.CodAlmacenEntregar;
				deta.CodDetalleDespacho = clase.CodDetalleDespacho;
				deta.CodProducto = clase.CodProducto;
				deta.CodUnidad = clase.CodUnidad;
				detalle.Add(deta);
			}
		}
		return detalle;
	}

	private bool verificarEntrega()
	{
		return true;
	}

	private List<clsDetalleDespacho> obtenerDetalleDespacho()
	{
		List<clsDetalleDespacho> detalle = new List<clsDetalleDespacho>();
		if (dgvDetalle.RowCount > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				int codProducto = Convert.ToInt32(row.Cells[colCodProducto.Name].Value);
				int codUnidad = Convert.ToInt32(row.Cells[colCodUnidad.Name].Value);
				double cantidad = Convert.ToDouble(row.Cells[colCantidad.Name].Value);
				double cantidadPendiente = Convert.ToDouble(row.Cells[colCantidadPendiente.Name].Value);
				int codAlmacen = Convert.ToInt32(row.Cells[colCodAlmacen.Name].Value);
				clsDetalleDespacho deta = new clsDetalleDespacho();
				deta.CodDespacho = Convert.ToInt32(row.Cells[colCodDespacho.Name].Value);
				deta.CodDetalleDespacho = Convert.ToInt32(row.Cells[colCodDetalleDespacho.Name].Value);
				deta.CodProducto = codProducto;
				deta.CodUnidad = codUnidad;
				deta.Estado = 1;
				deta.Cantidad = cantidad;
				deta.CantidadPendiente = cantidadPendiente;
				deta.CodAlmacenEntregar = codAlmacen;
				detalle.Add(deta);
			}
		}
		return detalle;
	}

	private clsDespacho obtenerDespachoDeForm()
	{
		clsDespacho aux = new clsDespacho();
		aux.CodSerie = ser.CodSerie;
		aux.Serie = ser.Serie;
		aux.Numeracion = ser.Numeracion.ToString().PadLeft(8, '0');
		aux.CodAlmacenRegistro = frmLogin.iCodAlmacen;
		aux.CodCliente = venta.CodCliente;
		aux.CodTablaDocRelacionada = 1;
		aux.CodDocRelacionado = Convert.ToInt32(venta.CodFacturaVenta);
		DateTime fechaDespacho = (aux.FechaRegistro = DateTime.Now);
		aux.FechaDespacho = fechaDespacho;
		aux.NombreContacto = txtNombreContacto.Text.Trim();
		aux.TelefonoContacto = txtTelefonoContacto.Text.Trim();
		aux.DireccionDelivery = txtDelivery.Text;
		aux.Comentario = txtComentario.Text;
		return aux;
	}

	private void btnGenerar_Click(object sender, EventArgs e)
	{
		try
		{
			clsTipoDocumento tipodoc = admtd.BuscaTipoDocumento("ENTREGA");
			clsSerie serent = admSerie.CargaSerieEmpresa(despacho.CodAlmacenRegistro, tipodoc.CodTipoDocumento);
			if (serent == null)
			{
				MessageBox.Show("No se encontro una serie registrada ñara realizar entrega");
				return;
			}
			int codPermiso = admForm.getPermisoGenerarEntrega();
			int codDespacho = despacho.CodDespacho;
			usuario_click = null;
			DialogResult dr = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 2;
			frm.tipoVentanaAAsignarUsuario = 3;
			frm.permiso = codPermiso;
			frm.PermitirAdministradores = true;
			frm.ventanaDespacho = this;
			dr = frm.ShowDialog();
			if (dr != DialogResult.OK)
			{
				return;
			}
			frmInterReqAlmTransf form = new frmInterReqAlmTransf();
			form.data = admdespacho.generarDatosParaFormularioIntermedioTransferencia(despacho.CodDespacho, 0, frmLogin.iCodSucursal);
			form.Proceso = 2;
			form.ventana_d = this;
			form.Text = "Generando Entrega de Despacho: DESP " + txtRefDespacho.Text;
			form.ShowDialog();
			if (form.DialogResult != DialogResult.OK)
			{
				return;
			}
			clsEntrega entrega = new clsEntrega();
			entrega.CodAlmacenRegistro = frmLogin.iCodAlmacen;
			entrega.CodDespacho = despacho.CodDespacho;
			entrega.CodUsuario = usuario_click.CodUsuario;
			DateTime fechaRegistro = (entrega.FechaEntrega = DateTime.Now);
			entrega.FechaRegistro = fechaRegistro;
			entrega.NombreCliente = txtNombreRazonSocial.Text;
			entrega.CodSerie = serent.CodSerie;
			entrega.Serie = despacho.Serie;
			entrega.Numeracion = serent.Numeracion.ToString().PadLeft(8, '0');
			List<clsDetalleEntrega> detalle = obtenerDetalleParaEntrega();
			if (detalle.Count > 0)
			{
				using (TransactionScope Scope = new TransactionScope())
				{
					bool rpta = admdespacho.registrarEntrega(entrega);
					if (rpta)
					{
						foreach (clsDetalleEntrega det in detalle)
						{
							det.CodEntrega = entrega.CodEntrega;
							rpta = admdespacho.insertDetalleEntrega(det);
							if (!rpta)
							{
								break;
							}
						}
						if (rpta)
						{
							admdespacho.actualizaCantidadPendienteDespacho(despacho.CodDespacho);
						}
					}
					if (!rpta)
					{
						Transaction.Current.Rollback();
						Scope.Dispose();
						throw new Exception("No se pudo guardar Entrega");
					}
					int codEstado = admdespacho.obtenerCodEstado(despacho.CodDespacho);
					if (codEstado != -1)
					{
						admdespacho.cambioEstado(despacho.CodDespacho, codEstado);
					}
					if (despacho.codReqAlmRelacionado > 0 && codEstado == 16)
					{
						admreqalm.actualizaEstadoReqAlmacen(despacho.codReqAlmRelacionado, 11);
					}
					if (despacho.codReqAlmRelacionado > 0 && codEstado == 15)
					{
						admreqalm.actualizaEstadoReqAlmacen(despacho.codReqAlmRelacionado, 10);
					}
					Scope.Complete();
					Scope.Dispose();
					setEstadoReqAlmSegunEntregasDespacho(despacho);
					MessageBox.Show("Entrega Generada Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				recargarPagina(1);
			}
			else
			{
				MessageBox.Show("No Se Ah Generado Entrega", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void setEstadoReqAlmSegunEntregasDespacho(clsDespacho despacho)
	{
		if (despacho.codReqAlmRelacionado <= 0)
		{
			return;
		}
		clsRequerimientoAlmacen reqalm = admreqalm.CargaRequerimiento(despacho.codReqAlmRelacionado);
		if (reqalm.IEstado == 12)
		{
			return;
		}
		List<clsDetalleRequerimientoAlmacen> detareqalm = admreqalm.CargaDetalleRequerimientoAlmacen(despacho.codReqAlmRelacionado);
		List<clsDetalleDespacho> detadesp = admdespacho.ListaDetalleDespacho(despacho.CodDespacho);
		bool band = true;
		foreach (clsDetalleRequerimientoAlmacen detreq in detareqalm)
		{
			List<clsDetalleDespacho> encontrado = Enumerable.Where<clsDetalleDespacho>(detadesp.AsEnumerable(), (Func<clsDetalleDespacho, bool>)((clsDetalleDespacho x) => x.CodAlmacenEntregar == reqalm.CodAlmacenDespacho && x.CodProducto == detreq.CodProducto && x.CodUnidad == detreq.CodUnidad)).ToList();
			if (encontrado.Count <= 0)
			{
				continue;
			}
			double cantidad = 0.0;
			foreach (clsDetalleDespacho detdes in encontrado)
			{
				cantidad += detdes.CantidadPendiente;
			}
			if (cantidad > 0.0)
			{
				band = false;
				break;
			}
		}
		if (band)
		{
			admreqalm.actualizaEstadoReqAlmacen(reqalm.Codigo, 11);
		}
		else if (reqalm.IEstado == 11)
		{
			admreqalm.actualizaEstadoReqAlmacen(reqalm.Codigo, 10);
		}
	}

	private void recargarPagina(int tipo)
	{
		frmDespacho form = new frmDespacho();
		form.MdiParent = base.MdiParent;
		form.codDespacho = codDespacho;
		form.Show();
		try
		{
			Close();
		}
		catch (Exception)
		{
		}
	}

	private List<clsDetalleEntrega> obtenerDetalleParaEntrega()
	{
		List<clsDetalleEntrega> detalle = new List<clsDetalleEntrega>();
		if (dataCtdadGenerarEntrega != null)
		{
			foreach (DataRow row in dataCtdadGenerarEntrega.Rows)
			{
				if (Convert.ToDouble(row.Field<object>("cantidad") ?? ((object)0)) > 0.0)
				{
					clsDetalleEntrega deta = new clsDetalleEntrega();
					List<DataGridViewRow> encontrado = Enumerable.Where<DataGridViewRow>(dgvDetalle.Rows.Cast<DataGridViewRow>(), (Func<DataGridViewRow, bool>)((DataGridViewRow x) => Convert.ToInt32(row.Field<object>("codDetalle")) == Convert.ToInt32(x.Cells[colCodDetalleDespacho.Name].Value))).ToList();
					if (encontrado.Count <= 0)
					{
						throw new Exception("Ocurrio un error al tratar de generar el detalle de la entrega.\nIntente nuevamente.");
					}
					DataGridViewRow filaRGV = encontrado[0];
					deta.Cantidad = Convert.ToDouble(row.Field<object>("cantidad"));
					deta.CodAlmacenEntregar = Convert.ToInt32(filaRGV.Cells[colCodAlmacen.Name].Value);
					deta.CodDetalleDespacho = Convert.ToInt32(row.Field<object>("codDetalle"));
					deta.CodProducto = Convert.ToInt32(filaRGV.Cells[colCodProducto.Name].Value);
					deta.CodUnidad = Convert.ToInt32(filaRGV.Cells[colCodUnidad.Name].Value);
					detalle.Add(deta);
				}
			}
		}
		return detalle;
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
	}

	private void dgvEntregas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			int codEntrega = Convert.ToInt32(dgvEntregas.Rows[e.RowIndex].Cells[colCodEntrega.Name].Value);
			clsEntrega entrega = admdespacho.cargaEntrega(codEntrega);
			frmEntrega form = new frmEntrega();
			form.codEntrega = codEntrega;
			form.codReqAlm = despacho.codReqAlmRelacionado;
			form.ShowDialog();
			recargarPagina(1);
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
	}

	private void txtDocumentoRelacionado_DoubleClick(object sender, EventArgs e)
	{
		if (despacho.CodDocRelacionado != 0)
		{
			int codTablaDocRelacionada = despacho.CodTablaDocRelacionada;
			int num = codTablaDocRelacionada;
			if (num == 1)
			{
				frmVenta form = new frmVenta();
				form.MdiParent = base.MdiParent;
				form.CodVenta = despacho.CodDocRelacionado.ToString();
				form.Proceso = 3;
				form.Show();
			}
		}
	}

	private void txtReqAlm_DoubleClick(object sender, EventArgs e)
	{
		int codReqAlmacen = despacho.codReqAlmRelacionado;
		frmReqAlmacen form = mdi_Menu.buscarFrmReqAlmacen("frmReqAlmacen", codReqAlmacen, 2);
		if (form != null)
		{
			form.Activate();
			return;
		}
		form = new frmReqAlmacen();
		form.MdiParent = base.MdiParent;
		form.codRequerimientoAlmacen = codReqAlmacen;
		form.Proceso = 2;
		form.Show();
	}

	private void btnImprimirPDF_Click(object sender, EventArgs e)
	{
		try
		{
			string ruta = "C:\\tmp\\Despachos";
			string nombreArchivo = "DESP-" + despacho.CodDespacho.ToString().PadLeft(8, '0');
			CRDespacho rpt = new CRDespacho();
			clsAdmAlmacen admalm = new clsAdmAlmacen();
			clsAlmacen almac = admalm.CargaAlmacen(despacho.CodAlmacenRegistro);
			rpt.SetDataSource(admdespacho.ReporteImprimirDespacho(Convert.ToInt32(despacho.CodDespacho), almac.CodEmpresa).Tables[0]);
			Directory.CreateDirectory(ruta);
			rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
			Process p = new Process();
			p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
			p.Start();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnImprimirTicket_Click(object sender, EventArgs e)
	{
		try
		{
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			clsTipoDocumento doc = admtd.BuscaTipoDocumento("DESP");
			clsSerie ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
			clsConsultasExternas ext = new clsConsultasExternas();
			CRDespachoFormatoContinuo rpt1 = new CRDespachoFormatoContinuo();
			clsAdmAlmacen admalma = new clsAdmAlmacen();
			clsAlmacen almac = admalma.CargaAlmacen(despacho.CodAlmacenRegistro);
			rpt1.SetDataSource(admdespacho.ReporteImprimirDespacho(Convert.ToInt32(despacho.CodDespacho), almac.CodEmpresa).Tables[0]);
			PrintOptions rptoption = rpt1.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(40, 5, 0, 10));
			rpt1.PrintToPrinter(1, collated: false, 1, 1);
			rpt1.Close();
			rpt1.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtDocumentoRelacionado_KeyDown(object sender, KeyEventArgs e)
	{
		if (Proceso != 3)
		{
			return;
		}
		try
		{
			if (CodCliente == 0)
			{
				MessageBox.Show("Seleccione un cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtRucDni.Focus();
			}
			else
			{
				if (e.KeyCode != Keys.F1)
				{
					return;
				}
				if (Application.OpenForms["frmListaDocumentosPorCliente"] != null)
				{
					Application.OpenForms["frmListaDocumentosPorCliente"].Activate();
					return;
				}
				limpiaDespachoEnForm();
				despacho = null;
				frmListaDocumentosPorCliente form = new frmListaDocumentosPorCliente();
				form.Text = "Documentos";
				form.tipo = 1;
				form.CodCliente = CodCliente;
				form.ShowDialog();
				if (form.venta != null && form.venta.CodFacturaVenta != null)
				{
					venta = form.venta;
					if (venta == null)
					{
						return;
					}
					venta = admfactventa.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
					txtDocumentoRelacionado.Text = venta.SiglaDocumento + "-" + venta.NumDoc.PadLeft(8, '0');
					gbContactoAEntregar.Visible = true;
					Label label = lblRequerimiento;
					TextBox textBox = txtReqAlm;
					Label label2 = lblNotaCredito;
					bool flag = (txtNotaCredito.Visible = true);
					bool flag3 = (label2.Visible = flag);
					bool visible = (textBox.Visible = flag3);
					label.Visible = visible;
					int rpta = admdespacho.ObtenerDatoAntesDeListarDetalleDespacho3(1, Convert.ToInt32(venta.CodFacturaVenta));
					if (rpta == 2 || rpta == 3)
					{
						dgvDetalle.DataSource = admdespacho.ListaDetalleDespacho3(1, Convert.ToInt32(venta.CodFacturaVenta));
						if (rpta == 3)
						{
							despacho = admdespacho.cargaDespachoSegunDocRelacionado(1, venta.CodFacturaVenta);
							muestraDespachoEnForm();
						}
					}
					else
					{
						MessageBox.Show("No se puede asignar productos de despacho a la factura: " + txtDocumentoRelacionado.Text, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					txtDocumentoRelacionado.Text = "";
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void limpiaDespachoEnForm()
	{
		dtpFechaDespacho.Value = DateTime.Now;
		txtRefDespacho.Text = 0.ToString().PadLeft(8, '0');
		TextBox textBox = txtNotaCredito;
		TextBox textBox2 = txtNombreContacto;
		TextBox textBox3 = txtTelefonoContacto;
		string text = (txtReqAlm.Text = "");
		string text3 = (textBox3.Text = text);
		string text5 = (textBox2.Text = text3);
		textBox.Text = text5;
		txtEstado.Text = "NUEVO";
		txtDocumentoRelacionado.Text = "";
		if (dgvDetalle.DataSource != null)
		{
			DataTable data = new DataTable();
			data = (DataTable)dgvDetalle.DataSource;
			data.Rows.Clear();
			dgvDetalle.DataSource = data;
		}
	}

	private void txtRucDni_KeyDown(object sender, KeyEventArgs e)
	{
		if (Proceso != 3)
		{
			return;
		}
		try
		{
			if (e.KeyCode != Keys.F1)
			{
				return;
			}
			limpiaDespachoEnForm();
			despacho = null;
			if (Application.OpenForms["frmClientesLista"] != null)
			{
				Application.OpenForms["frmClientesLista"].Activate();
				return;
			}
			frmClientesLista form = new frmClientesLista();
			form.Proceso = 3;
			DialogResult rpta = form.ShowDialog();
			if (rpta == DialogResult.OK)
			{
				CodCliente = form.cli.CodCliente;
				if (CodCliente != 0)
				{
					txtRucDni.Text = ((form.cli.Ruc == "") ? "00000000" : form.cli.Ruc);
					txtNombreRazonSocial.Text = form.cli.RazonSocial;
				}
			}
			else
			{
				CodCliente = 0;
				TextBox textBox = txtRucDni;
				string text = (txtNombreRazonSocial.Text = "");
				textBox.Text = text;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvDetalle_KeyDown(object sender, KeyEventArgs e)
	{
		if (Proceso != 3)
		{
			return;
		}
		try
		{
			if (e.KeyCode == Keys.Delete && dgvDetalle.SelectedRows.Count <= 0)
			{
				MessageBox.Show("Seleccione uno o mas elementos de la lista para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void frmDespacho_Shown(object sender, EventArgs e)
	{
		if (cerrarVentana)
		{
			Close();
		}
		pintaFilas();
	}

	private void dgvDetalle_Sorted(object sender, EventArgs e)
	{
		pintaFilas();
	}

	private void btnRellenarCantidadPendiente_Click(object sender, EventArgs e)
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[colCtdadEntregar.Name].Value = row.Cells[colCantidadPendiente.Name].Value;
		}
	}

	private void btnLimpiarCantidadEntregar_Click(object sender, EventArgs e)
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[colCtdadEntregar.Name].Value = "";
		}
	}

	private void btnResumenEntregas_Click(object sender, EventArgs e)
	{
		try
		{
			string ruta = "C:\\tmp\\Reportes";
			string nombreArchivo = "Resumen_Entregas";
			CRResumenEntregas_Despacho rpt = new CRResumenEntregas_Despacho();
			rpt.SetDataSource(admdespacho.ReporteResumenEntregas(Convert.ToInt32(despacho.CodDespacho)).Tables[0]);
			Directory.CreateDirectory(ruta);
			rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
			Process p = new Process();
			p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
			p.Start();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Resumen de Entregas de Despacho", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void txtTelefonoContacto_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.telefono(e);
	}

	private bool validarContacto()
	{
		bool band = true;
		string cadena = "";
		if (txtNombreContacto.Text == "")
		{
			band = false;
			cadena = "Debe ingresar el nombre de contacto";
		}
		else if (txtTelefonoContacto.Text.Length != 9)
		{
			band = false;
			cadena = "Debe indicar un numero de contacto";
		}
		if (!band)
		{
			MessageBox.Show(cadena, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return band;
	}

	private void txtNotaCredito_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (txtNotaCredito.Text.Length < 7)
		{
			return;
		}
		try
		{
			frmNotadeCredito form = new frmNotadeCredito();
			form.MdiParent = base.MdiParent;
			form.CodNota = despacho.CodDocRelacionado.ToString();
			form.CodNC = Convert.ToInt32(despacho.CodNotaCredito);
			form.CodNotaS = Convert.ToInt32(despacho.CodNotaIngresoNC);
			form.Proceso = 3;
			form.MdiParent = base.MdiParent;
			form.Show();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmDespacho));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtDelivery = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.btnResumenEntregas = new System.Windows.Forms.Button();
		this.label5 = new System.Windows.Forms.Label();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.btnLimpiarCantidadEntregar = new System.Windows.Forms.Button();
		this.btnRellenarCantidadPendiente = new System.Windows.Forms.Button();
		this.txtusuariosolic = new System.Windows.Forms.TextBox();
		this.lblusuariosolic = new System.Windows.Forms.Label();
		this.lblAnulado = new System.Windows.Forms.Label();
		this.txtNotaCredito = new System.Windows.Forms.TextBox();
		this.lblNotaCredito = new System.Windows.Forms.Label();
		this.txtEstado = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.gbContactoAEntregar = new System.Windows.Forms.GroupBox();
		this.txtTelefonoContacto = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtNombreContacto = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtReqAlm = new System.Windows.Forms.TextBox();
		this.lblRequerimiento = new System.Windows.Forms.Label();
		this.dgvEntregas = new System.Windows.Forms.DataGridView();
		this.colCodEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colTituloEntrega = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtDocumentoRelacionado = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpFechaDespacho = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.txtNombreRazonSocial = new System.Windows.Forms.TextBox();
		this.txtRucDni = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtRefDespacho = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.colCodDetalleDespacho = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodDespacho = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colReferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colDescUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidadOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCantidadPendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCtdadEntregar = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnImprimirPDF = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnGenerar = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.label10 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		this.gbContactoAEntregar.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEntregas).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox3.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtDelivery);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.btnResumenEntregas);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.textBox2);
		this.groupBox1.Controls.Add(this.textBox1);
		this.groupBox1.Controls.Add(this.btnLimpiarCantidadEntregar);
		this.groupBox1.Controls.Add(this.btnRellenarCantidadPendiente);
		this.groupBox1.Controls.Add(this.txtusuariosolic);
		this.groupBox1.Controls.Add(this.lblusuariosolic);
		this.groupBox1.Controls.Add(this.lblAnulado);
		this.groupBox1.Controls.Add(this.txtNotaCredito);
		this.groupBox1.Controls.Add(this.lblNotaCredito);
		this.groupBox1.Controls.Add(this.txtEstado);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.gbContactoAEntregar);
		this.groupBox1.Controls.Add(this.txtReqAlm);
		this.groupBox1.Controls.Add(this.lblRequerimiento);
		this.groupBox1.Controls.Add(this.dgvEntregas);
		this.groupBox1.Controls.Add(this.txtDocumentoRelacionado);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dtpFechaDespacho);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtNombreRazonSocial);
		this.groupBox1.Controls.Add(this.txtRucDni);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtRefDespacho);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1190, 286);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.txtDelivery.Location = new System.Drawing.Point(97, 199);
		this.txtDelivery.MaxLength = 250;
		this.txtDelivery.Name = "txtDelivery";
		this.txtDelivery.ReadOnly = true;
		this.txtDelivery.Size = new System.Drawing.Size(418, 20);
		this.txtDelivery.TabIndex = 16;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(22, 202);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(48, 13);
		this.label9.TabIndex = 15;
		this.label9.Text = "Delivery:";
		this.btnResumenEntregas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnResumenEntregas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnResumenEntregas.Location = new System.Drawing.Point(802, 242);
		this.btnResumenEntregas.Name = "btnResumenEntregas";
		this.btnResumenEntregas.Size = new System.Drawing.Size(94, 38);
		this.btnResumenEntregas.TabIndex = 193;
		this.btnResumenEntregas.Text = "Resumen de Entregas";
		this.btnResumenEntregas.UseVisualStyleBackColor = true;
		this.btnResumenEntregas.Click += new System.EventHandler(btnResumenEntregas_Click);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(581, 180);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 192;
		this.label5.Text = "Leyenda:";
		this.textBox2.BackColor = System.Drawing.Color.FromArgb(105, 191, 255);
		this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox2.Location = new System.Drawing.Point(584, 211);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(129, 13);
		this.textBox2.TabIndex = 191;
		this.textBox2.Text = "ENTREGADO";
		this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBox1.BackColor = System.Drawing.Color.FromArgb(250, 157, 206);
		this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox1.Location = new System.Drawing.Point(584, 196);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(129, 13);
		this.textBox1.TabIndex = 190;
		this.textBox1.Text = "PENDIENTE";
		this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.btnLimpiarCantidadEntregar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnLimpiarCantidadEntregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnLimpiarCantidadEntregar.Location = new System.Drawing.Point(667, 242);
		this.btnLimpiarCantidadEntregar.Name = "btnLimpiarCantidadEntregar";
		this.btnLimpiarCantidadEntregar.Size = new System.Drawing.Size(129, 38);
		this.btnLimpiarCantidadEntregar.TabIndex = 189;
		this.btnLimpiarCantidadEntregar.Text = "Limpiar Cantidad A Entregar";
		this.btnLimpiarCantidadEntregar.UseVisualStyleBackColor = true;
		this.btnLimpiarCantidadEntregar.Visible = false;
		this.btnLimpiarCantidadEntregar.Click += new System.EventHandler(btnLimpiarCantidadEntregar_Click);
		this.btnRellenarCantidadPendiente.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnRellenarCantidadPendiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnRellenarCantidadPendiente.Location = new System.Drawing.Point(532, 242);
		this.btnRellenarCantidadPendiente.Name = "btnRellenarCantidadPendiente";
		this.btnRellenarCantidadPendiente.Size = new System.Drawing.Size(129, 38);
		this.btnRellenarCantidadPendiente.TabIndex = 188;
		this.btnRellenarCantidadPendiente.Text = "Rellenar Con Cantidad Pendiente";
		this.btnRellenarCantidadPendiente.UseVisualStyleBackColor = true;
		this.btnRellenarCantidadPendiente.Visible = false;
		this.btnRellenarCantidadPendiente.Click += new System.EventHandler(btnRellenarCantidadPendiente_Click);
		this.txtusuariosolic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtusuariosolic.Enabled = false;
		this.txtusuariosolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtusuariosolic.Location = new System.Drawing.Point(190, 93);
		this.txtusuariosolic.Name = "txtusuariosolic";
		this.txtusuariosolic.ReadOnly = true;
		this.txtusuariosolic.Size = new System.Drawing.Size(342, 20);
		this.txtusuariosolic.TabIndex = 187;
		this.txtusuariosolic.Tag = "";
		this.lblusuariosolic.AutoSize = true;
		this.lblusuariosolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblusuariosolic.Location = new System.Drawing.Point(8, 96);
		this.lblusuariosolic.Name = "lblusuariosolic";
		this.lblusuariosolic.Size = new System.Drawing.Size(176, 13);
		this.lblusuariosolic.TabIndex = 186;
		this.lblusuariosolic.Text = "USUARIO SOLICITANTE DE REQ:";
		this.lblusuariosolic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblAnulado.AutoSize = true;
		this.lblAnulado.Font = new System.Drawing.Font("Microsoft Sans Serif", 30f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAnulado.ForeColor = System.Drawing.Color.Red;
		this.lblAnulado.Location = new System.Drawing.Point(558, 108);
		this.lblAnulado.Name = "lblAnulado";
		this.lblAnulado.Size = new System.Drawing.Size(221, 46);
		this.lblAnulado.TabIndex = 180;
		this.lblAnulado.Text = "ANULADO";
		this.lblAnulado.Visible = false;
		this.txtNotaCredito.Location = new System.Drawing.Point(522, 67);
		this.txtNotaCredito.Name = "txtNotaCredito";
		this.txtNotaCredito.ReadOnly = true;
		this.txtNotaCredito.Size = new System.Drawing.Size(129, 20);
		this.txtNotaCredito.TabIndex = 179;
		this.txtNotaCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtNotaCredito.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(txtNotaCredito_MouseDoubleClick);
		this.lblNotaCredito.AutoSize = true;
		this.lblNotaCredito.Location = new System.Drawing.Point(431, 71);
		this.lblNotaCredito.Name = "lblNotaCredito";
		this.lblNotaCredito.Size = new System.Drawing.Size(84, 13);
		this.lblNotaCredito.TabIndex = 178;
		this.lblNotaCredito.Text = "Nota de Credito:";
		this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.4f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtEstado.ForeColor = System.Drawing.Color.Red;
		this.txtEstado.Location = new System.Drawing.Point(327, 16);
		this.txtEstado.Name = "txtEstado";
		this.txtEstado.ReadOnly = true;
		this.txtEstado.Size = new System.Drawing.Size(205, 19);
		this.txtEstado.TabIndex = 17;
		this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(236, 20);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(43, 13);
		this.label8.TabIndex = 16;
		this.label8.Text = "Estado:";
		this.gbContactoAEntregar.Controls.Add(this.txtTelefonoContacto);
		this.gbContactoAEntregar.Controls.Add(this.label7);
		this.gbContactoAEntregar.Controls.Add(this.txtNombreContacto);
		this.gbContactoAEntregar.Controls.Add(this.label6);
		this.gbContactoAEntregar.Location = new System.Drawing.Point(11, 119);
		this.gbContactoAEntregar.Name = "gbContactoAEntregar";
		this.gbContactoAEntregar.Size = new System.Drawing.Size(520, 74);
		this.gbContactoAEntregar.TabIndex = 15;
		this.gbContactoAEntregar.TabStop = false;
		this.gbContactoAEntregar.Text = "Contacto de Entrega";
		this.txtTelefonoContacto.Location = new System.Drawing.Point(87, 45);
		this.txtTelefonoContacto.MaxLength = 15;
		this.txtTelefonoContacto.Name = "txtTelefonoContacto";
		this.txtTelefonoContacto.ReadOnly = true;
		this.txtTelefonoContacto.Size = new System.Drawing.Size(328, 20);
		this.txtTelefonoContacto.TabIndex = 16;
		this.txtTelefonoContacto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTelefonoContacto_KeyPress);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(12, 48);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(52, 13);
		this.label7.TabIndex = 15;
		this.label7.Text = "Telefono:";
		this.txtNombreContacto.Location = new System.Drawing.Point(87, 19);
		this.txtNombreContacto.MaxLength = 250;
		this.txtNombreContacto.Name = "txtNombreContacto";
		this.txtNombreContacto.ReadOnly = true;
		this.txtNombreContacto.Size = new System.Drawing.Size(328, 20);
		this.txtNombreContacto.TabIndex = 14;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(47, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Nombre:";
		this.txtReqAlm.Location = new System.Drawing.Point(296, 67);
		this.txtReqAlm.Name = "txtReqAlm";
		this.txtReqAlm.ReadOnly = true;
		this.txtReqAlm.Size = new System.Drawing.Size(129, 20);
		this.txtReqAlm.TabIndex = 12;
		this.txtReqAlm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtReqAlm.DoubleClick += new System.EventHandler(txtReqAlm_DoubleClick);
		this.lblRequerimiento.AutoSize = true;
		this.lblRequerimiento.Location = new System.Drawing.Point(205, 71);
		this.lblRequerimiento.Name = "lblRequerimiento";
		this.lblRequerimiento.Size = new System.Drawing.Size(78, 13);
		this.lblRequerimiento.TabIndex = 11;
		this.lblRequerimiento.Text = "Requerimiento:";
		this.dgvEntregas.AllowUserToAddRows = false;
		this.dgvEntregas.AllowUserToDeleteRows = false;
		this.dgvEntregas.AllowUserToResizeColumns = false;
		this.dgvEntregas.AllowUserToResizeRows = false;
		this.dgvEntregas.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvEntregas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvEntregas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvEntregas.Columns.AddRange(this.colCodEntrega, this.colTituloEntrega, this.colFecha, this.colEstado);
		this.dgvEntregas.Location = new System.Drawing.Point(814, 19);
		this.dgvEntregas.Name = "dgvEntregas";
		this.dgvEntregas.ReadOnly = true;
		this.dgvEntregas.RowHeadersVisible = false;
		this.dgvEntregas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvEntregas.Size = new System.Drawing.Size(355, 205);
		this.dgvEntregas.TabIndex = 10;
		this.dgvEntregas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvEntregas_CellDoubleClick);
		this.colCodEntrega.DataPropertyName = "codEntrega";
		this.colCodEntrega.HeaderText = "codEntrega";
		this.colCodEntrega.Name = "colCodEntrega";
		this.colCodEntrega.ReadOnly = true;
		this.colCodEntrega.Visible = false;
		this.colTituloEntrega.DataPropertyName = "tituloEntrega";
		this.colTituloEntrega.HeaderText = "Entrega";
		this.colTituloEntrega.Name = "colTituloEntrega";
		this.colTituloEntrega.ReadOnly = true;
		this.colTituloEntrega.Width = 110;
		this.colFecha.DataPropertyName = "fecha";
		this.colFecha.HeaderText = "Fecha";
		this.colFecha.Name = "colFecha";
		this.colFecha.ReadOnly = true;
		this.colFecha.Width = 120;
		this.colEstado.DataPropertyName = "estado";
		this.colEstado.HeaderText = "Estado";
		this.colEstado.Name = "colEstado";
		this.colEstado.ReadOnly = true;
		this.colEstado.Width = 120;
		this.txtDocumentoRelacionado.Location = new System.Drawing.Point(99, 67);
		this.txtDocumentoRelacionado.Name = "txtDocumentoRelacionado";
		this.txtDocumentoRelacionado.ReadOnly = true;
		this.txtDocumentoRelacionado.Size = new System.Drawing.Size(100, 20);
		this.txtDocumentoRelacionado.TabIndex = 9;
		this.txtDocumentoRelacionado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtDocumentoRelacionado.DoubleClick += new System.EventHandler(txtDocumentoRelacionado_DoubleClick);
		this.txtDocumentoRelacionado.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocumentoRelacionado_KeyDown);
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(8, 71);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 13);
		this.label3.TabIndex = 8;
		this.label3.Text = "Documento:";
		this.dtpFechaDespacho.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaDespacho.Location = new System.Drawing.Point(696, 19);
		this.dtpFechaDespacho.Name = "dtpFechaDespacho";
		this.dtpFechaDespacho.Size = new System.Drawing.Size(100, 20);
		this.dtpFechaDespacho.TabIndex = 7;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(650, 22);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Fecha:";
		this.txtNombreRazonSocial.Location = new System.Drawing.Point(205, 41);
		this.txtNombreRazonSocial.Name = "txtNombreRazonSocial";
		this.txtNombreRazonSocial.ReadOnly = true;
		this.txtNombreRazonSocial.Size = new System.Drawing.Size(327, 20);
		this.txtNombreRazonSocial.TabIndex = 5;
		this.txtRucDni.Location = new System.Drawing.Point(99, 41);
		this.txtRucDni.Name = "txtRucDni";
		this.txtRucDni.ReadOnly = true;
		this.txtRucDni.Size = new System.Drawing.Size(100, 20);
		this.txtRucDni.TabIndex = 3;
		this.txtRucDni.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRucDni_KeyDown);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(8, 44);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(42, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Cliente:";
		this.txtRefDespacho.Location = new System.Drawing.Point(99, 15);
		this.txtRefDespacho.Name = "txtRefDespacho";
		this.txtRefDespacho.ReadOnly = true;
		this.txtRefDespacho.Size = new System.Drawing.Size(100, 20);
		this.txtRefDespacho.TabIndex = 1;
		this.txtRefDespacho.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(8, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(59, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Despacho:";
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 286);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1190, 69);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Productos:";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.colCodDetalleDespacho, this.colCodDespacho, this.colCodAlmacen, this.colDescAlmacen, this.colCodProducto, this.colReferencia, this.colDescProducto, this.colCodUnidad, this.colDescUnidad, this.colCantidadOriginal, this.colCantidad, this.colCantidadPendiente, this.colCtdadEntregar, this.orden);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.Size = new System.Drawing.Size(1184, 50);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.Sorted += new System.EventHandler(dgvDetalle_Sorted);
		this.dgvDetalle.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyDown);
		this.colCodDetalleDespacho.DataPropertyName = "codDetalleDespacho";
		this.colCodDetalleDespacho.HeaderText = "codDetalleDespacho";
		this.colCodDetalleDespacho.Name = "colCodDetalleDespacho";
		this.colCodDetalleDespacho.Visible = false;
		this.colCodDespacho.DataPropertyName = "codDespacho";
		this.colCodDespacho.HeaderText = "codDespacho";
		this.colCodDespacho.Name = "colCodDespacho";
		this.colCodDespacho.Visible = false;
		this.colCodAlmacen.DataPropertyName = "codAlmacen";
		this.colCodAlmacen.HeaderText = "codAlmacen";
		this.colCodAlmacen.Name = "colCodAlmacen";
		this.colCodAlmacen.Visible = false;
		this.colDescAlmacen.DataPropertyName = "descAlmacen";
		this.colDescAlmacen.HeaderText = "Almacen";
		this.colDescAlmacen.Name = "colDescAlmacen";
		this.colDescAlmacen.ReadOnly = true;
		this.colCodProducto.DataPropertyName = "codProducto";
		this.colCodProducto.HeaderText = "codProducto";
		this.colCodProducto.Name = "colCodProducto";
		this.colCodProducto.ReadOnly = true;
		this.colCodProducto.Visible = false;
		this.colReferencia.DataPropertyName = "referencia";
		this.colReferencia.HeaderText = "Referencia";
		this.colReferencia.Name = "colReferencia";
		this.colReferencia.ReadOnly = true;
		this.colDescProducto.DataPropertyName = "descProducto";
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.colDescProducto.DefaultCellStyle = dataGridViewCellStyle2;
		this.colDescProducto.HeaderText = "Descripcion";
		this.colDescProducto.Name = "colDescProducto";
		this.colDescProducto.ReadOnly = true;
		this.colDescProducto.Width = 450;
		this.colCodUnidad.DataPropertyName = "codUnidad";
		this.colCodUnidad.HeaderText = "codUnidad";
		this.colCodUnidad.Name = "colCodUnidad";
		this.colCodUnidad.ReadOnly = true;
		this.colCodUnidad.Visible = false;
		this.colDescUnidad.DataPropertyName = "descUnidad";
		this.colDescUnidad.HeaderText = "Unidad";
		this.colDescUnidad.Name = "colDescUnidad";
		this.colDescUnidad.ReadOnly = true;
		this.colCantidadOriginal.DataPropertyName = "cantidadOriginal";
		this.colCantidadOriginal.HeaderText = "Cantidad Original";
		this.colCantidadOriginal.Name = "colCantidadOriginal";
		this.colCantidadOriginal.ReadOnly = true;
		this.colCantidadOriginal.Visible = false;
		this.colCantidad.DataPropertyName = "cantidad";
		this.colCantidad.HeaderText = "Cantidad";
		this.colCantidad.Name = "colCantidad";
		this.colCantidad.ReadOnly = true;
		this.colCantidadPendiente.DataPropertyName = "ctdadPendiente";
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.colCantidadPendiente.DefaultCellStyle = dataGridViewCellStyle3;
		this.colCantidadPendiente.HeaderText = "Cantidad Pendiente";
		this.colCantidadPendiente.Name = "colCantidadPendiente";
		this.colCantidadPendiente.ReadOnly = true;
		this.colCtdadEntregar.HeaderText = "Cantidad a Entregar";
		this.colCtdadEntregar.Name = "colCtdadEntregar";
		this.colCtdadEntregar.Visible = false;
		this.orden.DataPropertyName = "orden";
		this.orden.HeaderText = "orden";
		this.orden.Name = "orden";
		this.orden.ReadOnly = true;
		this.orden.Visible = false;
		this.groupBox3.Controls.Add(this.btnImprimirPDF);
		this.groupBox3.Controls.Add(this.btnAnular);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnGenerar);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 355);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1190, 94);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.btnImprimirPDF.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnImprimirPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimirPDF.Image = SIGEFA.Properties.Resources.printer;
		this.btnImprimirPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimirPDF.Location = new System.Drawing.Point(679, 31);
		this.btnImprimirPDF.Name = "btnImprimirPDF";
		this.btnImprimirPDF.Size = new System.Drawing.Size(118, 33);
		this.btnImprimirPDF.TabIndex = 5;
		this.btnImprimirPDF.Text = "Imprimir PDF";
		this.btnImprimirPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimirPDF.UseVisualStyleBackColor = true;
		this.btnImprimirPDF.Visible = false;
		this.btnImprimirPDF.Click += new System.EventHandler(btnImprimirPDF_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAnular.Image = SIGEFA.Properties.Resources.x_button;
		this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAnular.Location = new System.Drawing.Point(603, 31);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(69, 33);
		this.btnAnular.TabIndex = 4;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Visible = false;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimir.Image = SIGEFA.Properties.Resources.printer;
		this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimir.Location = new System.Drawing.Point(803, 31);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(90, 33);
		this.btnImprimir.TabIndex = 3;
		this.btnImprimir.Text = "Imprimir";
		this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimirTicket_Click);
		this.btnGenerar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGenerar.Image = SIGEFA.Properties.Resources.agregar;
		this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerar.Location = new System.Drawing.Point(899, 31);
		this.btnGenerar.Name = "btnGenerar";
		this.btnGenerar.Size = new System.Drawing.Size(133, 33);
		this.btnGenerar.TabIndex = 2;
		this.btnGenerar.Text = "Generar Entrega";
		this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerar.UseVisualStyleBackColor = true;
		this.btnGenerar.Click += new System.EventHandler(btnGenerar_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = SIGEFA.Properties.Resources.save;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.Location = new System.Drawing.Point(1038, 31);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(79, 33);
		this.btnGuardar.TabIndex = 1;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.Location = new System.Drawing.Point(1123, 31);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(55, 33);
		this.btnSalir.TabIndex = 0;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "products.png");
		this.imageList1.Images.SetKeyName(1, "cliente.png");
		this.imageList1.Images.SetKeyName(2, "1211811759.png");
		this.imageList1.Images.SetKeyName(3, "user-icon-512.png");
		this.imageList1.Images.SetKeyName(4, "1325228163.jpg");
		this.imageList1.Images.SetKeyName(5, "cal3png.png");
		this.imageList1.Images.SetKeyName(6, "Get Document.ico");
		this.imageList1.Images.SetKeyName(7, "Send Document.ico");
		this.imageList1.Images.SetKeyName(8, "Transfer Document.ico");
		this.imageList1.Images.SetKeyName(9, "compras_a_proveedores_01.png");
		this.imageList1.Images.SetKeyName(10, "boxes copy_thumb.png");
		this.imageList1.Images.SetKeyName(11, "TarjetasKardex-1-PNG.png");
		this.imageList1.Images.SetKeyName(12, "a-propos-bleu-utilisateur-icone-7595-96.png");
		this.imageList1.Images.SetKeyName(13, "inventarios1.jpg");
		this.imageList1.Images.SetKeyName(14, "d9dc81882f20a4fb51dadd294dd1b4d5.png");
		this.imageList1.Images.SetKeyName(15, "Almacen.png");
		this.imageList1.Images.SetKeyName(16, "proveedor.png");
		this.imageList1.Images.SetKeyName(17, "company_256.png");
		this.imageList1.Images.SetKeyName(18, "iEngrenages.png");
		this.imageList1.Images.SetKeyName(19, "bag.png");
		this.imageList1.Images.SetKeyName(20, "venta.png");
		this.imageList1.Images.SetKeyName(21, "boleta-link.png");
		this.imageList1.Images.SetKeyName(22, "cotizacion.png");
		this.imageList1.Images.SetKeyName(23, "factura-icon.jpg");
		this.imageList1.Images.SetKeyName(24, "icon_shippingbox_withcalendar.png");
		this.imageList1.Images.SetKeyName(25, "images (1).jpg");
		this.imageList1.Images.SetKeyName(26, "pedido.png");
		this.imageList1.Images.SetKeyName(27, "pedidos.png");
		this.imageList1.Images.SetKeyName(28, "DocumentSearch.png");
		this.imageList1.Images.SetKeyName(29, "editar-una-pluma-para-escribir-icono-6827-96.png");
		this.imageList1.Images.SetKeyName(30, "Icono-Borrar-Anuncio.gif");
		this.imageList1.Images.SetKeyName(31, "lista-de-regalos.png");
		this.imageList1.Images.SetKeyName(32, "database-backup-cd-512.png");
		this.imageList1.Images.SetKeyName(33, "database-backup-icon-512.png");
		this.imageList1.Images.SetKeyName(34, "pagos.png");
		this.imageList1.Images.SetKeyName(35, "pagossol.png");
		this.imageList1.Images.SetKeyName(36, "lista-de-regalos.png");
		this.imageList1.Images.SetKeyName(37, "ICONO-INVENTARIO.jpg");
		this.imageList1.Images.SetKeyName(38, "Porcentaje (1).png");
		this.imageList1.Images.SetKeyName(39, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(40, "credit-note.png");
		this.imageList1.Images.SetKeyName(41, "Report.ico");
		this.imageList1.Images.SetKeyName(42, "stocks-icon.png");
		this.imageList1.Images.SetKeyName(43, "icon-requerimiento.ico");
		this.imageList1.Images.SetKeyName(44, "logog.png");
		this.imageList1.Images.SetKeyName(45, "ReporteProblemas.png");
		this.imageList1.Images.SetKeyName(46, "sucursales.png");
		this.imageList1.Images.SetKeyName(47, "stock.jpg");
		this.imageList1.Images.SetKeyName(48, "stock2.png");
		this.imageList1.Images.SetKeyName(49, "productoscarga.jpg");
		this.imageList1.Images.SetKeyName(50, "libro.png");
		this.imageList1.Images.SetKeyName(51, "logout1.png");
		this.imageList1.Images.SetKeyName(52, "bloggif_57aa8110e7163.jpeg");
		this.imageList1.Images.SetKeyName(53, "119aac0aa4ed9b90205078ecda0550af.png");
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(22, 228);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(63, 13);
		this.label10.TabIndex = 194;
		this.label10.Text = "Comentario:";
		this.txtComentario.Location = new System.Drawing.Point(97, 225);
		this.txtComentario.MaxLength = 250;
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.ReadOnly = true;
		this.txtComentario.Size = new System.Drawing.Size(418, 55);
		this.txtComentario.TabIndex = 195;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1190, 449);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Name = "frmDespacho";
		this.Text = "Despacho";
		base.Load += new System.EventHandler(frmDespacho_Load);
		base.Shown += new System.EventHandler(frmDespacho_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.gbContactoAEntregar.ResumeLayout(false);
		this.gbContactoAEntregar.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEntregas).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox3.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
