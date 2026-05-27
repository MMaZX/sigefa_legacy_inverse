using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class F2TransferenciaEntreAlmacenes : Office2007Form
{
	private clsReporteTransferencias ds = new clsReporteTransferencias();

	private clsAdmAlmacen admAlmacen = new clsAdmAlmacen();

	private clsAlmacen almacen = new clsAlmacen();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion admTransaccion = new clsAdmTransaccion();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private clsTransferencia transfer = new clsTransferencia();

	private List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie admSerie = new clsAdmSerie();

	private clsAdmRequerimientoAlmacen admreq = new clsAdmRequerimientoAlmacen();

	public int CodSerie;

	public int num;

	public List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	public int CodTransaccion;

	public int CodDocumento;

	public int Proceso;

	public int CodTransDirecta;

	public int caso;

	public clsUsuario usuario_click = null;

	private bool bandera = true;

	private int codproducto_error = 0;

	private IContainer components = null;

	private GroupBox groupBox2;

	public DataGridView dgvDetalle;

	private GroupBox groupBox1;

	public TextBox txtDireccion;

	private Label label4;

	private Label lbNombreTransaccion;

	private ComboBox cmbMoneda;

	private Label label17;

	private TextBox txtAutorizacion;

	private Label label18;

	public TextBox txtOrigen;

	private Label label15;

	private Button btnDetalle;

	private TextBox txtComentario;

	private Label label9;

	private TextBox txtDocSal;

	private Label label7;

	public TextBox txtDocRef;

	private Label label5;

	private TextBox txtTransaccion;

	private Label label2;

	private DateTimePicker dtpFecha;

	private Label label1;

	private GroupBox groupBox3;

	private Button btnImprimir;

	private Button btnSalir;

	private Button btnGuardar;

	private ComboBox cmbDestino;

	private Label label10;

	public TextBox txtCodAlmacen;

	private GroupBox groupBox4;

	private TextBox txtBruto;

	private Label label3;

	private TextBox txtValorVenta;

	private Label label12;

	private TextBox txtCodTransDir;

	private Button btnRechazar;

	private Button btnAprobar;

	private TextBox txtDocIng;

	private Label label6;

	private TextBox txtdescripcion;

	private ImageList imageList1;

	private Label label8;

	private Button btnEliminar;

	public TextBox txtSerie;

	private TextBox txtcodserie;

	private TextBox txtNumero;

	private Label label11;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn codProv;

	private DataGridViewTextBoxColumn valorpromedio;

	private DataGridViewTextBoxColumn precioigv;

	private TextBox txtestado;

	private Label label13;

	private Button btnImprimirTicket;

	public F2TransferenciaEntreAlmacenes()
	{
		InitializeComponent();
	}

	private void CargaTransaccion()
	{
		tran = AdmTran.MuestraTransaccion(CodTransaccion);
		tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
		txtTransaccion.Text = tran.Sigla;
		lbNombreTransaccion.Text = tran.Descripcion;
		lbNombreTransaccion.Visible = true;
	}

	private void F2TransferenciaEntreAlmacenes_Load(object sender, EventArgs e)
	{
		doc = admtd.BuscaTipoDocumento("TD");
		CodTransaccion = 15;
		CargaTransaccion();
		txtDocRef.Text = doc.Sigla;
		CodDocumento = doc.CodTipoDocumento;
		cmbMoneda.SelectedIndex = 0;
		dtpFecha.MaxDate = DateTime.Today.Date;
		if (Proceso == 1)
		{
			txtCodAlmacen.Text = frmLogin.iCodAlmacen.ToString();
			almacen = admAlmacen.CargaAlmacen(frmLogin.iCodAlmacen);
			txtOrigen.Text = almacen.Descripcion;
			CargaAlmacenDestino(frmLogin.iCodEmpresa, frmLogin.iCodAlmacen);
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			btnAprobar.Visible = false;
			btnRechazar.Visible = false;
			sololectura(estado: true);
			cargaserie();
		}
		else if (Proceso == 2)
		{
			CargaTransferencia();
		}
		else
		{
			if (Proceso != 3)
			{
				return;
			}
			CargaTransferencia();
			verificarPermisoTransferencia();
			tran = admTransaccion.MuestraTransaccionS("TD", 1);
			doc = admtd.BuscaTipoDocumento(transfer.SiglaDocumento);
			txtCodAlmacen.Text = transfer.CodAlmacenOrigen.ToString();
			almacen = admAlmacen.CargaAlmacen(transfer.CodAlmacenOrigen);
			txtOrigen.Text = almacen.Descripcion;
			txtDocRef.Text = doc.Sigla;
			CargaAlmacenDestino(frmLogin.iCodEmpresa, transfer.CodAlmacenOrigen);
			sololectura(estado: false);
			if (caso == 0)
			{
				txtDocSal.Visible = false;
				txtDocIng.Visible = false;
				label7.Visible = false;
				label6.Visible = false;
				txtDocSal.Enabled = false;
				txtDocIng.Enabled = false;
				btnAprobar.Visible = true;
				btnRechazar.Visible = true;
				Button button = btnImprimirTicket;
				bool visible = (btnImprimir.Visible = false);
				button.Visible = visible;
			}
			else if (caso == 1)
			{
				btnAprobar.Visible = false;
				btnRechazar.Visible = false;
				label7.Visible = true;
				label6.Visible = true;
				txtDocSal.Enabled = false;
				txtDocIng.Enabled = false;
				txtdescripcion.Enabled = false;
				NS = admNS.CargaNS(CodTransDirecta);
				if (NS != null)
				{
					txtDocSal.Text = NS.CodNotaSalida;
				}
				NI = admNI.CargaNI(CodTransDirecta);
				if (NI != null)
				{
					txtDocIng.Text = NI.CodNotaIngreso;
				}
				txtdescripcion.Text = transfer.DescripcionRechazo;
			}
			else if (caso == 2 || caso == 3 || caso == 4)
			{
				btnAprobar.Visible = false;
				btnRechazar.Visible = false;
				txtDocSal.Enabled = false;
				txtDocIng.Enabled = false;
				txtDocSal.Visible = true;
				txtDocIng.Visible = true;
				label7.Visible = true;
				label6.Visible = true;
				txtdescripcion.Enabled = false;
				NS = admNS.CargaNS(CodTransDirecta);
				if (NS != null)
				{
					txtDocSal.Text = NS.CodNotaSalida;
				}
				NI = admNI.CargaNI(CodTransDirecta);
				if (NI != null)
				{
					txtDocIng.Text = NI.CodNotaIngreso;
				}
				txtdescripcion.Text = transfer.DescripcionRechazo;
			}
		}
	}

	private void verificarPermisoTransferencia()
	{
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		List<int> accesos = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
		int permiso = new clsAdmFormulario().getPermisoAceptarTransferencia();
		bool band = accesos.Contains(permiso) || frmLogin.iNivelUser == 1;
		btnAprobar.Visible = band;
		int permiso2 = new clsAdmFormulario().getPermisoRechazarTransferencia();
		bool band2 = accesos.Contains(permiso2) || frmLogin.iNivelUser == 1;
		btnRechazar.Visible = band2;
	}

	public void asignacionProductosDePlantilla(int codigo_plantilla)
	{
		throw new NotImplementedException();
	}

	private void cargaserie()
	{
		ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			num = ser.Numeracion;
			if (CodSerie != 0)
			{
				if (ser.PreImpreso)
				{
					CodSerie = ser.CodSerie;
					txtSerie.Text = ser.Serie;
					txtcodserie.Text = ser.CodSerie.ToString();
					txtNumero.Enabled = true;
					txtNumero.Text = "";
				}
				else
				{
					CodSerie = ser.CodSerie;
					txtSerie.Text = ser.Serie;
					txtcodserie.Text = ser.CodSerie.ToString().PadLeft(3, '0');
					txtNumero.Text = ser.Numeracion.ToString().PadLeft(6, '0');
					txtNumero.Enabled = false;
				}
			}
			else
			{
				txtSerie.Focus();
			}
		}
		else
		{
			MessageBox.Show("No existe numeracion para transferencia");
		}
	}

	private void sololectura(bool estado)
	{
		txtTransaccion.Enabled = estado;
		txtDocRef.Enabled = estado;
		txtCodAlmacen.Enabled = estado;
		txtComentario.Enabled = estado;
		dtpFecha.Enabled = estado;
		txtdescripcion.Visible = !estado;
		label8.Visible = !estado;
		cmbMoneda.Enabled = false;
		txtOrigen.Enabled = estado;
		cmbDestino.Enabled = estado;
		btnGuardar.Visible = estado;
		btnEliminar.Visible = estado;
		btnDetalle.Visible = estado;
		txtDocIng.Visible = !estado;
		txtDocSal.Visible = !estado;
		label7.Visible = !estado;
		label6.Visible = !estado;
		btnImprimir.Visible = !estado;
		btnImprimir.Enabled = !estado;
		btnImprimirTicket.Visible = !estado;
		btnImprimirTicket.Enabled = !estado;
	}

	private void CargaAlmacenDestino(int codempres, int codal)
	{
		cmbDestino.DataSource = admAlmacen.ListaAlmacen2();
		cmbDestino.DisplayMember = "nombre";
		cmbDestino.ValueMember = "codAlmacen";
		if (Proceso == 3)
		{
			cmbDestino.SelectedValue = transfer.CodAlmacenDestino;
			cmbDestino_SelectionChangeCommitted(new object(), new EventArgs());
		}
		else
		{
			cmbDestino.SelectedValue = -1;
		}
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmDetalleGuia"] != null)
			{
				Application.OpenForms["frmDetalleGuia"].Activate();
			}
			else
			{
				frmDetalleGuia form = new frmDetalleGuia();
				form.Procede = 3;
				form.codalmacen = frmLogin.iCodAlmacen;
				form.Codlista = 1;
				form.ShowDialog();
			}
			btnGuardar.Enabled = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		cerrarformulario();
	}

	private void cmbDestino_SelectionChangeCommitted(object sender, EventArgs e)
	{
		txtDireccion.Clear();
		if (Convert.ToInt32(cmbDestino.SelectedValue) >= 0)
		{
			almacen = admAlmacen.CargaAlmacen(Convert.ToInt32(cmbDestino.SelectedValue));
			txtDireccion.Text = almacen.Ubicacion;
			transfer.CodAlmacenDestino = Convert.ToInt32(cmbDestino.SelectedValue);
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso != 1)
		{
			return;
		}
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.RowCount > 0)
		{
			if (cmbDestino.SelectedIndex == -1)
			{
				MessageBox.Show("Seleccione almacen destino para guardar transferencia!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				if (Proceso == 0)
				{
					return;
				}
				if (txtcodserie.Text != "" && txtNumero.Text != "")
				{
					transfer.CodAlmacenOrigen = frmLogin.iCodAlmacen;
					transfer.CodTipoDocumento = 14;
					if (cmbMoneda.SelectedIndex == 0)
					{
						transfer.Moneda = 1;
					}
					else
					{
						transfer.Moneda = 2;
					}
					transfer.FechaEnvio = dtpFecha.Value;
					transfer.FechaEntrega = dtpFecha.Value;
					transfer.FormaPago = 0;
					transfer.FechaPago = dtpFecha.Value.Date;
					transfer.CodListaPrecio = 0;
					transfer.Comentario = txtComentario.Text;
					transfer.DescripcionRechazo = txtdescripcion.Text;
					transfer.MontoBruto = Convert.ToDecimal(txtBruto.Text);
					transfer.MontoDscto = 0.00m;
					transfer.Igv = 0.00m;
					transfer.Total = Convert.ToDecimal(txtValorVenta.Text);
					transfer.CodUser = frmLogin.iCodUser;
					transfer.Estado = 1;
					transfer.Codserie = Convert.ToInt32(txtcodserie.Text);
					transfer.Serie = txtSerie.Text;
					transfer.Numerodocumento = txtNumero.Text;
					if (Proceso == 1)
					{
						if (admTransferencia.insert(transfer))
						{
							RecorreDetalle();
							if (detalle.Count > 0)
							{
								foreach (clsDetalleTransferencia det in detalle)
								{
									admTransferencia.insertdetalle(det);
								}
							}
							MessageBox.Show("Los datos se guardaron correctamente!", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							if (transfer.CodTransDir != null)
							{
								CodTransDirecta = Convert.ToInt32(transfer.CodTransDir);
							}
							CargaTransferencia();
							sololectura(estado: true);
						}
					}
					else if (Proceso == 2 && admTransferencia.update(transfer))
					{
						RecorreDetalle();
						foreach (clsDetalleTransferencia det2 in transfer.Detalle)
						{
							foreach (clsDetalleTransferencia det3 in detalle)
							{
								if (det2.Equals(det3))
								{
									admTransferencia.updatedetalle(det3);
									return;
								}
							}
							admTransferencia.deletedetalle(det2.CodDetalleTransfer);
						}
						foreach (clsDetalleTransferencia deta in detalle)
						{
							if (deta.CodDetalleTransfer == 0)
							{
								admTransferencia.insertdetalle(deta);
							}
						}
						MessageBox.Show("Los datos se actualizaron correctamente!", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					cerrarformulario();
				}
				else
				{
					MessageBox.Show("Seleccione serie para la transferencia");
				}
			}
		}
		else
		{
			MessageBox.Show("Se necesita agregar datos a la tabla detalle para guardar!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtDocIng.Focus();
		}
	}

	private void CargaTransferencia()
	{
		try
		{
			transfer = admTransferencia.CargaTransferencia(Convert.ToInt32(CodTransDirecta));
			if (transfer != null)
			{
				txtCodTransDir.Text = transfer.CodTransDir;
				dtpFecha.Value = transfer.FechaEnvio.Date;
				txtcodserie.Text = transfer.Codserie.ToString();
				txtSerie.Text = transfer.Serie.ToString();
				txtNumero.Text = transfer.Numerodocumento.ToString();
				if (transfer.Moneda == 1)
				{
					cmbMoneda.SelectedIndex = 0;
				}
				else
				{
					cmbMoneda.SelectedIndex = 1;
				}
				if (txtDocRef.Enabled)
				{
					CodDocumento = transfer.CodTipoDocumento;
					txtDocRef.Text = transfer.SiglaDocumento;
				}
				txtComentario.Text = transfer.Comentario;
				txtBruto.Text = $"{transfer.MontoBruto:#,##0.0000}";
				txtValorVenta.Text = $"{transfer.Total - transfer.Igv:#,##0.0000}";
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = admTransferencia.CargaDetalle(Convert.ToInt32(transfer.CodTransDir));
		valorpromedio.Visible = false;
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleTransferencia deta = new clsDetalleTransferencia();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodTransDir = Convert.ToInt32(transfer.CodTransDir);
		deta.CodAlmacenOrigen = Convert.ToInt32(txtCodAlmacen.Text);
		deta.CodAlmacenDestino = transfer.CodAlmacenDestino;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.CantidadPendiente = deta.Cantidad;
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.Valorpromedio = Convert.ToDecimal(fila.Cells[valorpromedio.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		detalle.Add(deta);
	}

	private void btnAprobar_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtDocSal.Text != "")
			{
				if (txtDocIng.Text != "")
				{
					if (dgvDetalle.RowCount <= 0)
					{
						return;
					}
					usuario_click = null;
					int permiso = new clsAdmFormulario().getPermisoAceptarTransferencia();
					frmAutorizacion frm = new frmAutorizacion();
					frm.tipoAccion = 2;
					frm.permiso = permiso;
					frm.PermitirAdministradores = true;
					frm.tipoVentanaAAsignarUsuario = 9;
					frm.ventanaTransferencia = this;
					DialogResult dr = frm.ShowDialog();
					if (dr != DialogResult.OK || usuario_click == null)
					{
						return;
					}
					NS.NumDoc = txtNumero.Text;
					NS.CodAlmacen = Convert.ToInt32(txtCodAlmacen.Text);
					NS.CodCliente = 0;
					NS.CodNotaCredito = 0;
					NS.CodSucursal = frmLogin.iCodSucursal;
					NS.RazonSocialCliente = "";
					NS.CodAutorizado = 0;
					NS.FechaSalida = dtpFecha.Value.Date;
					NS.DocumentoReferencia = 0;
					NS.CodTipoTransaccion = tran.CodTransaccion;
					NS.CodTipoDocumento = doc.CodTipoDocumento;
					NS.CodSerie = Convert.ToInt32(txtcodserie.Text);
					NS.Serie = txtSerie.Text;
					if (cmbMoneda.SelectedIndex == 0)
					{
						NS.Moneda = 1;
					}
					else if (cmbMoneda.SelectedIndex == 1)
					{
						NS.Moneda = 2;
					}
					NS.FechaSalida = dtpFecha.Value.Date;
					NS.FormaPago = 0;
					NS.FechaPago = dtpFecha.Value.Date;
					NS.Comentario = txtComentario.Text;
					NS.MontoBruto = Convert.ToDouble(txtBruto.Text);
					NS.MontoDscto = 0.0;
					NS.Igv = 0.0;
					NS.Total = Convert.ToDouble(txtValorVenta.Text);
					NS.CodUser = transfer.CodUser;
					NS.Estado = 1;
					NS.Codtransferencia = CodTransDirecta;
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
							MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					if (bandera)
					{
						NI.NumDoc = txtNumero.Text;
						NI.CodAlmacen = Convert.ToInt32(cmbDestino.SelectedValue);
						NI.CodAutorizado = 0;
						NI.CodReferencia = 0;
						NI.CodTipoTransaccion = tran.CodTransaccion;
						NI.CodTipoDocumento = doc.CodTipoDocumento;
						NI.CodSerie = Convert.ToInt32(txtSerie.Text);
						NI.Serie = txtSerie.Text;
						if (cmbMoneda.SelectedIndex == 0)
						{
							NI.Moneda = 1;
						}
						else if (cmbMoneda.SelectedIndex == 1)
						{
							NI.Moneda = 1;
						}
						NI.FechaIngreso = dtpFecha.Value.Date;
						NI.FormaPago = 0;
						NI.FechaPago = dtpFecha.Value.Date;
						NI.Comentario = txtComentario.Text;
						NS.MontoBruto = Convert.ToDouble(txtBruto.Text);
						NI.MontoDscto = 0.0;
						NI.Igv = 0.0;
						NI.Total = Convert.ToDouble(txtValorVenta.Text);
						NI.CodUser = transfer.CodUser;
						NI.Estado = 1;
						NI.Codtransferencia = CodTransDirecta;
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
								MessageBox.Show("Se aprobo la transferencia, datos guardados correctamente!", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								Transaction.Current.Rollback();
								Scope2.Dispose();
								MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						if (bandera)
						{
							admTransferencia.Aprobar(CodTransDirecta);
							admTransferencia.atendido(CodTransDirecta, usuario_click.CodUsuario);
							Proceso = 3;
							caso = 1;
							CodTransDirecta = Convert.ToInt32(transfer.CodTransDir);
							if (transfer.codReqAlm > 0)
							{
								admreq.actualizaCantidadPendienteAprobadaReqAlmacen(transfer.codReqAlm);
							}
							F2TransferenciaEntreAlmacenes_Load(sender, e);
							((F2TransferenciasPendientes)Application.OpenForms["F2TransferenciasPendientes"])?.CargaLista();
						}
						else
						{
							MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else
					{
						MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("Ingrese el Numero de Documento correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtDocIng.Focus();
				}
			}
			else
			{
				MessageBox.Show("Ingrese el Numero de Documento correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtDocSal.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cerrarformulario()
	{
		Close();
	}

	private void RecorreDetalleNI()
	{
		detalleNI.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleNI(row);
		}
	}

	private void añadedetalleNI(DataGridViewRow fila)
	{
		clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
		deta1.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta1.CodNotaIngreso = Convert.ToInt32(NI.CodNotaIngreso);
		deta1.CodAlmacen = Convert.ToInt32(cmbDestino.SelectedValue);
		deta1.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta1.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta1.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta1.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta1.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta1.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta1.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta1.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta1.MontoDescuento = 0.0;
		deta1.ValoReal = deta1.PrecioUnitario / 1.18;
		deta1.Igv = deta1.ValoReal * 0.18;
		deta1.PrecioReal = deta1.ValoReal * 1.18;
		deta1.CodUser = frmLogin.iCodUser;
		deta1.Importe = deta1.PrecioUnitario * deta1.Cantidad;
		deta1.Subtotal = deta1.Importe;
		deta1.PrecioReal = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta1.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta1.CodProveedor = 0;
		deta1.FechaIngreso = dtpFecha.Value;
		detalleNI.Add(deta1);
	}

	private void RecorreDetalleNS()
	{
		detalleNS.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleNS(row);
		}
	}

	private void añadedetalleNS(DataGridViewRow fila)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodNotaSalida = Convert.ToInt32(NS.CodNotaSalida);
		deta.CodAlmacen = transfer.CodAlmacenOrigen;
		deta.CodAlmacen = Convert.ToInt32(txtCodAlmacen.Text);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.ValorPromedio = Convert.ToDouble(fila.Cells[valorpromedio.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		detalleNS.Add(deta);
	}

	private void btnRechazar_Click(object sender, EventArgs e)
	{
		if (txtdescripcion.Text != "")
		{
			if (caso != 0)
			{
				return;
			}
			usuario_click = null;
			int permiso = new clsAdmFormulario().getPermisoRechazarTransferencia();
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 2;
			frm.permiso = permiso;
			frm.PermitirAdministradores = true;
			frm.tipoVentanaAAsignarUsuario = 9;
			frm.ventanaTransferencia = this;
			DialogResult dr = frm.ShowDialog();
			if (dr == DialogResult.OK && usuario_click != null && admTransferencia.rechazado(CodTransDirecta, txtdescripcion.Text))
			{
				admTransferencia.atendido(CodTransDirecta, usuario_click.CodUsuario);
				MessageBox.Show("Se rechazo la transferencia, datos guardados correctamente!", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				if (transfer.codReqAlm > 0)
				{
					admreq.actualizaCantidadPendienteReqAlmacen(transfer.codReqAlm);
				}
				((F2TransferenciasPendientes)Application.OpenForms["F2TransferenciasPendientes"])?.CargaLista();
				cerrarformulario();
			}
		}
		else
		{
			MessageBox.Show("Describa el motivo del rechazo!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			txtdescripcion.Focus();
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			if (CodTransDirecta > 0)
			{
				string ruta = "C:\\tmp\\TransferenciasDirectas";
				string nombreArchivo = txtDocRef.Text + txtSerie.Text + "-" + txtNumero.Text.PadLeft(8, '0');
				CRTransferenciaDirecta rpt = new CRTransferenciaDirecta();
				rpt.SetDataSource(ds.RptTransferenciaDirecta1(caso, frmLogin.iCodAlmacen, CodTransDirecta, frmLogin.iCodEmpresa).Tables[0]);
				Directory.CreateDirectory(ruta);
				rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
				Process p = new Process();
				p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
				p.Start();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnImprimirTicket_Click(object sender, EventArgs e)
	{
		try
		{
			if (CodTransDirecta > 0)
			{
				clsTipoDocumento tip = admtd.BuscaTipoDocumento("TD");
				clsSerie ser = admSerie.BuscaSeriexDocumento(tip.CodTipoDocumento, frmLogin.iCodAlmacen);
				clsConsultasExternas ext = new clsConsultasExternas();
				CRTransferenciaDirectaFormatoContinuocont rpt1 = new CRTransferenciaDirectaFormatoContinuocont();
				rpt1.SetDataSource(ds.RptTransferenciaDirecta1(caso, frmLogin.iCodAlmacen, CodTransDirecta, frmLogin.iCodEmpresa).Tables[0]);
				PrintOptions rptoption = rpt1.PrintOptions;
				rptoption.PrinterName = ser.NombreImpresora;
				rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
				rptoption.ApplyPageMargins(new PageMargins(50, 5, 0, 10));
				rpt1.PrintToPrinter(1, collated: false, 1, 1);
				rpt1.Close();
				rpt1.Dispose();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count > 0 && dgvDetalle.Rows.Count > 0)
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
		}
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmSerie"] != null)
			{
				Application.OpenForms["frmSerie"].Activate();
				return;
			}
			frmSerie form = new frmSerie();
			form.DocSeleccionado = doc.CodTipoDocumento;
			form.Sigla = doc.Sigla;
			form.Proceso = 3;
			form.ShowDialog();
			ser = form.ser;
			CodSerie = ser.CodSerie;
			num = ser.Numeracion;
			if (CodSerie != 0)
			{
				if (ser.PreImpreso)
				{
					CodSerie = ser.CodSerie;
					txtSerie.Text = ser.Serie;
					txtcodserie.Text = ser.CodSerie.ToString();
					txtNumero.Enabled = true;
					txtNumero.Text = "";
				}
				else
				{
					CodSerie = ser.CodSerie;
					txtSerie.Text = ser.Serie;
					txtcodserie.Text = ser.CodSerie.ToString().PadLeft(3, '0');
					txtNumero.Text = ser.Numeracion.ToString().PadLeft(6, '0');
					txtNumero.Enabled = false;
				}
			}
			else
			{
				txtDocRef.Focus();
			}
			if (CodSerie != 0)
			{
				ProcessTabKey(forward: true);
			}
		}
		else if (e.KeyCode != Keys.Return)
		{
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.F2TransferenciaEntreAlmacenes));
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtestado = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtcodserie = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtDocIng = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtCodTransDir = new System.Windows.Forms.TextBox();
		this.txtCodAlmacen = new System.Windows.Forms.TextBox();
		this.cmbDestino = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtDireccion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.lbNombreTransaccion = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtAutorizacion = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtOrigen = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtDocSal = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnRechazar = new System.Windows.Forms.Button();
		this.btnAprobar = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label8 = new System.Windows.Forms.Label();
		this.txtdescripcion = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.btnImprimirTicket = new System.Windows.Forms.Button();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		base.SuspendLayout();
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Location = new System.Drawing.Point(0, 159);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(839, 275);
		this.groupBox2.TabIndex = 26;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.coduser, this.fecharegistro, this.codProv, this.valorpromedio, this.precioigv);
		this.dgvDetalle.Location = new System.Drawing.Point(11, 19);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(822, 250);
		this.dgvDetalle.TabIndex = 25;
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Codigo";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 110;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 500;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 80;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.serielote.Width = 80;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		dataGridViewCellStyle1.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 80;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N4";
		dataGridViewCellStyle2.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle2;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 80;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N4";
		dataGridViewCellStyle3.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle3;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle4;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle5.Format = "N2";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle5;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N2";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle6;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N2";
		dataGridViewCellStyle7.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle7;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.montodscto.Visible = false;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N2";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle10;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.Visible = false;
		this.precioreal.DataPropertyName = "precioreal";
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.ReadOnly = true;
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioreal.Visible = false;
		this.valoreal.DataPropertyName = "valoreal";
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.ReadOnly = true;
		this.valoreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valoreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valoreal.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		this.codProv.DataPropertyName = "codProveedor";
		this.codProv.HeaderText = "codProv";
		this.codProv.Name = "codProv";
		this.codProv.ReadOnly = true;
		this.codProv.Visible = false;
		this.valorpromedio.DataPropertyName = "valorpromedio";
		this.valorpromedio.HeaderText = "valorpromedio";
		this.valorpromedio.Name = "valorpromedio";
		this.valorpromedio.ReadOnly = true;
		this.valorpromedio.Visible = false;
		this.precioigv.DataPropertyName = "precioigv";
		this.precioigv.HeaderText = "precioigv";
		this.precioigv.Name = "precioigv";
		this.precioigv.ReadOnly = true;
		this.precioigv.Visible = false;
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.txtestado);
		this.groupBox1.Controls.Add(this.label13);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtcodserie);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.txtDocIng);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtCodTransDir);
		this.groupBox1.Controls.Add(this.txtCodAlmacen);
		this.groupBox1.Controls.Add(this.cmbDestino);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtDireccion);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.lbNombreTransaccion);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtAutorizacion);
		this.groupBox1.Controls.Add(this.label18);
		this.groupBox1.Controls.Add(this.txtOrigen);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtDocSal);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(6, 5);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(833, 151);
		this.groupBox1.TabIndex = 28;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera Transferencia Directa";
		this.txtestado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtestado.Enabled = false;
		this.txtestado.Location = new System.Drawing.Point(738, 119);
		this.txtestado.Name = "txtestado";
		this.txtestado.Size = new System.Drawing.Size(89, 20);
		this.txtestado.TabIndex = 83;
		this.txtestado.Text = ".";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(688, 122);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(46, 13);
		this.label13.TabIndex = 84;
		this.label13.Text = "Estado :";
		this.txtSerie.Location = new System.Drawing.Point(168, 45);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(61, 20);
		this.txtSerie.TabIndex = 79;
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtcodserie.Location = new System.Drawing.Point(313, 45);
		this.txtcodserie.Name = "txtcodserie";
		this.txtcodserie.Size = new System.Drawing.Size(16, 20);
		this.txtcodserie.TabIndex = 82;
		this.txtcodserie.Visible = false;
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Enabled = false;
		this.txtNumero.Location = new System.Drawing.Point(237, 45);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 80;
		this.txtNumero.Tag = "";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(120, 47);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(34, 13);
		this.label11.TabIndex = 81;
		this.label11.Text = "Serie.";
		this.txtDocIng.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocIng.Enabled = false;
		this.txtDocIng.Location = new System.Drawing.Point(521, 44);
		this.txtDocIng.Name = "txtDocIng";
		this.txtDocIng.Size = new System.Drawing.Size(89, 20);
		this.txtDocIng.TabIndex = 77;
		this.txtDocIng.Text = ".";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(423, 47);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(96, 13);
		this.label6.TabIndex = 78;
		this.label6.Text = "Num. Doc. Ingreso";
		this.txtCodTransDir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodTransDir.Location = new System.Drawing.Point(617, 20);
		this.txtCodTransDir.Name = "txtCodTransDir";
		this.txtCodTransDir.ReadOnly = true;
		this.txtCodTransDir.Size = new System.Drawing.Size(28, 20);
		this.txtCodTransDir.TabIndex = 76;
		this.txtCodTransDir.Visible = false;
		this.txtCodAlmacen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodAlmacen.Location = new System.Drawing.Point(77, 71);
		this.txtCodAlmacen.Name = "txtCodAlmacen";
		this.txtCodAlmacen.ReadOnly = true;
		this.txtCodAlmacen.Size = new System.Drawing.Size(28, 20);
		this.txtCodAlmacen.TabIndex = 75;
		this.txtCodAlmacen.Tag = "10";
		this.txtCodAlmacen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmbDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbDestino.FormattingEnabled = true;
		this.cmbDestino.Location = new System.Drawing.Point(407, 70);
		this.cmbDestino.Name = "cmbDestino";
		this.cmbDestino.Size = new System.Drawing.Size(203, 21);
		this.cmbDestino.TabIndex = 74;
		this.cmbDestino.Tag = "16";
		this.cmbDestino.SelectionChangeCommitted += new System.EventHandler(cmbDestino_SelectionChangeCommitted);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(339, 76);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(56, 13);
		this.label10.TabIndex = 73;
		this.label10.Tag = "5";
		this.label10.Text = "A. Destino";
		this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDireccion.Enabled = false;
		this.txtDireccion.Location = new System.Drawing.Point(77, 96);
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.ReadOnly = true;
		this.txtDireccion.Size = new System.Drawing.Size(533, 20);
		this.txtDireccion.TabIndex = 51;
		this.txtDireccion.Tag = "6";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 99);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(70, 13);
		this.label4.TabIndex = 69;
		this.label4.Tag = "6";
		this.label4.Text = "Direccion AD";
		this.lbNombreTransaccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreTransaccion.Location = new System.Drawing.Point(111, 25);
		this.lbNombreTransaccion.Name = "lbNombreTransaccion";
		this.lbNombreTransaccion.Size = new System.Drawing.Size(177, 18);
		this.lbNombreTransaccion.TabIndex = 66;
		this.lbNombreTransaccion.Text = "NombreTransaccion";
		this.lbNombreTransaccion.Visible = false;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(746, 35);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 45;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(688, 38);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(52, 13);
		this.label17.TabIndex = 64;
		this.label17.Text = "Moneda :";
		this.txtAutorizacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtAutorizacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtAutorizacion.Location = new System.Drawing.Point(746, 62);
		this.txtAutorizacion.Name = "txtAutorizacion";
		this.txtAutorizacion.Size = new System.Drawing.Size(81, 20);
		this.txtAutorizacion.TabIndex = 54;
		this.txtAutorizacion.Tag = "22";
		this.txtAutorizacion.Visible = false;
		this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(669, 65);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(71, 13);
		this.label18.TabIndex = 63;
		this.label18.Tag = "22";
		this.label18.Text = "Autorizacion :";
		this.label18.Visible = false;
		this.txtOrigen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtOrigen.Location = new System.Drawing.Point(114, 71);
		this.txtOrigen.Name = "txtOrigen";
		this.txtOrigen.ReadOnly = true;
		this.txtOrigen.Size = new System.Drawing.Size(174, 20);
		this.txtOrigen.TabIndex = 50;
		this.txtOrigen.Tag = "5";
		this.txtOrigen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(20, 75);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(51, 13);
		this.label15.TabIndex = 62;
		this.label15.Tag = "5";
		this.label15.Text = "A. Origen";
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Location = new System.Drawing.Point(746, 87);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(81, 23);
		this.btnDetalle.TabIndex = 61;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(77, 121);
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(533, 20);
		this.txtComentario.TabIndex = 59;
		this.txtComentario.Tag = "21";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(11, 126);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 60;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario";
		this.txtDocSal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocSal.Enabled = false;
		this.txtDocSal.Location = new System.Drawing.Point(521, 20);
		this.txtDocSal.Name = "txtDocSal";
		this.txtDocSal.Size = new System.Drawing.Size(89, 20);
		this.txtDocSal.TabIndex = 42;
		this.txtDocSal.Text = ".";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(429, 23);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(90, 13);
		this.label7.TabIndex = 53;
		this.label7.Text = "Num. Doc. Salida";
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.txtDocRef.Location = new System.Drawing.Point(77, 46);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 46;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(18, 53);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 49;
		this.label5.Tag = "10";
		this.label5.Text = "Doc. Ref.";
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(77, 22);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.ReadOnly = true;
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 41;
		this.txtTransaccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(5, 28);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 43;
		this.label2.Text = "Transacción";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(746, 12);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 44;
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(664, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(76, 13);
		this.label1.TabIndex = 40;
		this.label1.Text = "Fecha Transf :";
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.btnImprimirTicket);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnRechazar);
		this.groupBox3.Controls.Add(this.btnAprobar);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Location = new System.Drawing.Point(0, 510);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(839, 54);
		this.groupBox3.TabIndex = 29;
		this.groupBox3.TabStop = false;
		this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(7, 11);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 37);
		this.btnEliminar.TabIndex = 79;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "img_rechazar.png");
		this.imageList1.Images.SetKeyName(7, "img_aceptar.png");
		this.btnRechazar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnRechazar.ImageIndex = 6;
		this.btnRechazar.ImageList = this.imageList1;
		this.btnRechazar.Location = new System.Drawing.Point(91, 11);
		this.btnRechazar.Name = "btnRechazar";
		this.btnRechazar.Size = new System.Drawing.Size(85, 37);
		this.btnRechazar.TabIndex = 27;
		this.btnRechazar.Text = "Rechazar";
		this.btnRechazar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnRechazar.UseVisualStyleBackColor = true;
		this.btnRechazar.Visible = false;
		this.btnRechazar.Click += new System.EventHandler(btnRechazar_Click);
		this.btnAprobar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAprobar.ImageIndex = 7;
		this.btnAprobar.ImageList = this.imageList1;
		this.btnAprobar.Location = new System.Drawing.Point(185, 11);
		this.btnAprobar.Name = "btnAprobar";
		this.btnAprobar.Size = new System.Drawing.Size(79, 37);
		this.btnAprobar.TabIndex = 26;
		this.btnAprobar.Text = "Aprobar";
		this.btnAprobar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAprobar.UseVisualStyleBackColor = true;
		this.btnAprobar.Visible = false;
		this.btnAprobar.Click += new System.EventHandler(btnAprobar_Click);
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(523, 13);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 35);
		this.btnImprimir.TabIndex = 25;
		this.btnImprimir.Text = "Im&primir PDF";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(771, 13);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 35);
		this.btnSalir.TabIndex = 24;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(688, 11);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 37);
		this.btnGuardar.TabIndex = 23;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.Controls.Add(this.label8);
		this.groupBox4.Controls.Add(this.txtdescripcion);
		this.groupBox4.Controls.Add(this.txtValorVenta);
		this.groupBox4.Controls.Add(this.label12);
		this.groupBox4.Controls.Add(this.txtBruto);
		this.groupBox4.Controls.Add(this.label3);
		this.groupBox4.Location = new System.Drawing.Point(0, 434);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(839, 79);
		this.groupBox4.TabIndex = 30;
		this.groupBox4.TabStop = false;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(17, 15);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(50, 13);
		this.label8.TabIndex = 79;
		this.label8.Tag = "21";
		this.label8.Text = "Rechazo";
		this.label8.Visible = false;
		this.txtdescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtdescripcion.Location = new System.Drawing.Point(83, 12);
		this.txtdescripcion.Multiline = true;
		this.txtdescripcion.Name = "txtdescripcion";
		this.txtdescripcion.Size = new System.Drawing.Size(451, 61);
		this.txtdescripcion.TabIndex = 60;
		this.txtdescripcion.Tag = "21";
		this.txtdescripcion.Visible = false;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(716, 18);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 27;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(650, 22);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 26;
		this.label12.Text = "V. Venta :";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(716, 44);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(111, 20);
		this.txtBruto.TabIndex = 25;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(666, 48);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(38, 13);
		this.label3.TabIndex = 24;
		this.label3.Text = "Bruto :";
		this.btnImprimirTicket.ImageIndex = 3;
		this.btnImprimirTicket.ImageList = this.imageList1;
		this.btnImprimirTicket.Location = new System.Drawing.Point(439, 13);
		this.btnImprimirTicket.Name = "btnImprimirTicket";
		this.btnImprimirTicket.Size = new System.Drawing.Size(78, 35);
		this.btnImprimirTicket.TabIndex = 80;
		this.btnImprimirTicket.Text = "Imprimir TICKET";
		this.btnImprimirTicket.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimirTicket.UseVisualStyleBackColor = true;
		this.btnImprimirTicket.Visible = false;
		this.btnImprimirTicket.Click += new System.EventHandler(btnImprimirTicket_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(845, 567);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "F2TransferenciaEntreAlmacenes";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = " Transferencia Entre Almacenes";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(F2TransferenciaEntreAlmacenes_Load);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		base.ResumeLayout(false);
	}
}
