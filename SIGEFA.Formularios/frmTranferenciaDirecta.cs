using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmTranferenciaDirecta : Office2007Form
{
	private clsAdmEmpresaTransporte AdmET = new clsAdmEmpresaTransporte();

	private clsEmpresaTransporte empT = new clsEmpresaTransporte();

	private clsNotaIngreso notaIngreso = new clsNotaIngreso();

	private clsAdmNotaIngreso admNotaIngreso = new clsAdmNotaIngreso();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsAdmNotaSalida admnota = new clsAdmNotaSalida();

	private clsAdmGuiaRemision admGuia = new clsAdmGuiaRemision();

	private clsGuiaRemision guia = new clsGuiaRemision();

	private clsConsultasExternas ext = new clsConsultasExternas();

	private TextBox txtedit = new TextBox();

	private clsValidar val = new clsValidar();

	private clsAdmAlmacen Admalmac = new clsAdmAlmacen();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsSerie ser = new clsSerie();

	private clsAlmacen almacen = new clsAlmacen();

	private clsAdmSerie admSerie = new clsAdmSerie();

	public List<int> codProd = new List<int>();

	public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleingreso = new List<clsDetalleNotaIngreso>();

	public List<clsDetalleGuiaRemision> detalleguia = new List<clsDetalleGuiaRemision>();

	public string CodTransferencia;

	public int CodDocumento;

	public int CodSerie;

	public int num;

	public int Tipo;

	public int guarda = 0;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Procede = 0;

	public int Proceso_txd = 0;

	private clsGuia ds = new clsGuia();

	public int estado_vigente = 0;

	public int CodEmpresaTransporte;

	private clsVehiculoTransporte vehiculotransporte = new clsVehiculoTransporte();

	private clsAdmVehiculoTransporte admVehiculoTransporte = new clsAdmVehiculoTransporte();

	private clsConductor conductor = new clsConductor();

	private clsAdmConductor admConductor = new clsAdmConductor();

	private clsRequerimiento requerimiento = new clsRequerimiento();

	private clsDetalleRequerimiento detallerequerimiento = new clsDetalleRequerimiento();

	private clsAdmRequerimiento admrequerimiento = new clsAdmRequerimiento();

	public DataTable DtAuxiliar_pr = new DataTable();

	public DataTable DtAuxiliar_pasados_pr = new DataTable();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnDetalle;

	private TextBox txtComentario;

	private Label label9;

	private Label label1;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnGuardar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private Button btnImprimir;

	private Label lbAlmacen;

	private Label label20;

	private ComboBox cmbAlmacen;

	private DateTimePicker dtpFecha;

	public TextBox txtAlmacenDestino;

	private Label label5;

	private TextBox txtCodDoc;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	public TextBox txtDocRef;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private TextBox txtNumDoc;

	private Label label7;

	private TextBox txtTransaccion;

	private Label label2;

	private Label label8;

	private ComboBox cmbAlmacenDestino;

	private ComboBox cmbTransferencia;

	private TextBox txtAlmacenOri;

	public DataGridView dgvDetalle;

	private CheckBox chkReqDet;

	private DateTimePicker dtpTraslado;

	private Label label11;

	private DateTimePicker dtpEmision;

	private Label label10;

	public Button btnAceptarTransferencia;

	private ImageList imageList3;

	private CustomValidator customValidator1;

	private CustomValidator customValidator2;

	private GroupBox groupBox4;

	private ComboBox cmbTransportista;

	private ComboBox cmbVehiculo;

	private Label label6;

	private Label label4;

	private GroupBox groupBox5;

	public TextBox txtRazonSocialTransporte;

	private Label label14;

	private Label label13;

	public TextBox txtRUCTransporte;

	public TextBox txtReq;

	private TextBox txtTipoCambio;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn valorpromedio;

	private DataGridViewTextBoxColumn valorpromediosoles;

	private DataGridViewTextBoxColumn preciopromedio;

	private DataGridViewTextBoxColumn stockactual;

	private DataGridViewTextBoxColumn comentario_usu;

	private DataGridViewTextBoxColumn estadototal;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo1;

	private CachedCRCuotasPrestamo cachedCRCuotasPrestamo2;

	public TextBox txtSerie;

	private TextBox txtcodserie;

	private TextBox txtNumero;

	private Label label3;

	public frmTranferenciaDirecta()
	{
		InitializeComponent();
	}

	private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private bool BuscaTipoDocumento()
	{
		doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
		if (doc != null)
		{
			CodDocumento = doc.CodTipoDocumento;
			return true;
		}
		CodDocumento = 0;
		return false;
	}

	private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmDocumentos"] != null)
		{
			Application.OpenForms["frmDocumentos"].Activate();
			return;
		}
		frmDocumentos form = new frmDocumentos();
		form.Proceso = 3;
		form.Procedencia = 1;
		form.Transaccion = "TD";
		form.ShowDialog();
		doc = form.doc;
		CodDocumento = doc.CodTipoDocumento;
		txtDocRef.Text = doc.Sigla;
		txtCodDoc.Text = doc.CodTipoDocumento.ToString();
		if (CodDocumento != 0)
		{
			ProcessTabKey(forward: true);
		}
	}

	public void cargaTipoCambio()
	{
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		if (tc != null)
		{
			txtTipoCambio.Text = tc.Venta.ToString();
		}
	}

	private void frmNotaSalida_Load(object sender, EventArgs e)
	{
		txtTransaccion.Text = "TD";
		cmbTransferencia.SelectedIndex = 0;
		nota.CodTipoTransaccion = 15;
		notaIngreso.CodTipoTransaccion = 15;
		txtDocRef.Focus();
		cargaAlmacenes();
		cargaTipoCambio();
		cargaAlmacenesDestino();
		CargaTransportista();
		CargaVehiculoTrasnporte();
		if (Proceso == 2)
		{
			CargaTransferencia();
		}
		else if (Proceso == 3)
		{
			CargaTransferencia();
			label20.Visible = false;
			txtAlmacenDestino.Visible = false;
			dgvDetalle.Columns["stockactual"].Visible = false;
			dgvDetalle.Columns["comentario_usu"].Visible = false;
			sololectura(estado: true);
			sololecturatransferencia(p: true);
			txtComentario.Focus();
		}
		else if (Proceso == 4)
		{
			CargaTransferencia();
			sololectura(estado: true);
		}
		if (Proceso != 3)
		{
			string cod = "";
			cod = ((frmLogin.iCodAlmacen < 10) ? frmLogin.iCodAlmacen.ToString().PadLeft(2, '0') : frmLogin.iCodAlmacen.ToString());
			cmbAlmacen.SelectedValue = cod;
		}
	}

	private void sololecturatransferencia(bool p)
	{
		btnGuardar.Visible = p;
		txtComentario.Enabled = !p;
		txtComentario.ReadOnly = !p;
		cmbAlmacenDestino.Enabled = !p;
		cmbTransportista.Enabled = !p;
		cmbVehiculo.Enabled = !p;
		txtDocRef.Enabled = !p;
		txtSerie.Enabled = !p;
		dtpEmision.Enabled = !p;
		dtpTraslado.Enabled = !p;
	}

	private void cargaAlmacenes()
	{
		cmbAlmacen.DataSource = Admalmac.ListaAlmacen2();
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.Enabled = false;
	}

	private void cargaAlmacenesDestino()
	{
		cmbAlmacenDestino.DataSource = Admalmac.ListaAlmacen2();
		cmbAlmacenDestino.DisplayMember = "nombre";
		cmbAlmacenDestino.ValueMember = "codAlmacen";
		cmbAlmacenDestino.SelectedIndex = -1;
	}

	private void CargaTransportista()
	{
		cmbTransportista.DataSource = admConductor.CargaConductores();
		cmbTransportista.DisplayMember = "nombre";
		cmbTransportista.ValueMember = "codConductor";
		cmbTransportista.SelectedIndex = -1;
	}

	private void CargaVehiculoTrasnporte()
	{
		cmbVehiculo.DataSource = admVehiculoTransporte.CargaVehiculoTransportes();
		cmbVehiculo.DisplayMember = "placa";
		cmbVehiculo.ValueMember = "codVehiculoTransporte";
		cmbVehiculo.SelectedIndex = -1;
	}

	private void CargaTransferencia()
	{
		try
		{
			notaIngreso = admNotaIngreso.CargaNotaIngreso(Convert.ToInt32(CodTransferencia));
			if (nota != null)
			{
				txtNumDoc.Text = notaIngreso.CodNotaIngreso;
				cmbTransferencia.Text = notaIngreso.DescripcionTransaccion;
				txtDocRef.Text = notaIngreso.SiglaDocumento;
				doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
				txtSerie.Text = notaIngreso.Serie;
				txtNumero.Text = notaIngreso.NumDoc;
				txtcodserie.Text = notaIngreso.CodSerie.ToString();
				if (notaIngreso.CodAlmacen == 1)
				{
					cmbAlmacenDestino.SelectedValue = "01";
				}
				else
				{
					cmbAlmacenDestino.SelectedValue = notaIngreso.CodAlmacen;
				}
				if (notaIngreso.codalmacenemisor == 1)
				{
					cmbAlmacen.SelectedValue = "01";
				}
				else
				{
					cmbAlmacen.SelectedValue = notaIngreso.codalmacenemisor;
				}
				txtComentario.Text = notaIngreso.Comentario;
				cmbVehiculo.SelectedValue = notaIngreso.Codvehiculotransporte;
				cmbTransportista.SelectedValue = notaIngreso.Codconductor;
				if (cmbTransferencia.SelectedIndex == 0)
				{
					guia = admGuia.CargaGuiaTransferencia(Convert.ToInt32(notaIngreso.CodNotaIngreso));
					if (guia != null)
					{
						dtpEmision.Value = guia.FechaEmision;
						dtpTraslado.Value = guia.FechaTraslado;
					}
				}
				dtpFecha.Value = notaIngreso.FechaIngreso;
				CargaDetalle();
				ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
				btnGuardar.Enabled = false;
				btnEliminar.Enabled = false;
				btnDetalle.Enabled = false;
				btnImprimir.Enabled = false;
				btnAceptarTransferencia.Visible = true;
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void sololectura(bool estado)
	{
		dtpFecha.Enabled = !estado;
		txtDocRef.ReadOnly = estado;
		txtNumero.ReadOnly = estado;
		txtComentario.ReadOnly = estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
		btnImprimir.Visible = estado;
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = admNotaIngreso.CargaDetalleTransferencia(Convert.ToInt32(notaIngreso.CodNotaIngreso));
		RecorreDetalleIngreso();
		notaIngreso.Detalle = detalleingreso;
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
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

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		chkReqDet.Checked = false;
		if (!txtDocRef.Text.Equals(""))
		{
			if (Application.OpenForms["frmDetalleGuia"] != null)
			{
				Application.OpenForms["frmDetalleGuia"].Activate();
				return;
			}
			frmDetalleGuia form = new frmDetalleGuia();
			if (cmbTransferencia.SelectedIndex == 0)
			{
				form.Procede = 9;
				form.Proceso = 1;
			}
			else
			{
				form.Procede = 9;
				form.Proceso = 1;
				form.codalmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
			}
			form.Text = "Detalle de Productos";
			if (dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
				{
					codProd.Add(Convert.ToInt32(row.Cells[codproducto.Name].Value));
				}
			}
			else
			{
				codProd.Add(0);
			}
			form.ShowDialog();
		}
		else
		{
			MessageBox.Show("Debe elegir un documento");
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		int contador_veces = 0;
		int estado_vigente = 2;
		int contador_vigentes = 0;
		bool resultado_accion = false;
		int cantidad_ceros = 0;
		if (Proceso_txd == 0)
		{
			if (!superValidator1.Validate() || guarda != 0 || Proceso == 0)
			{
				return;
			}
			if (Proceso != 3)
			{
				cantidad_ceros = recorre_ceros();
				if (cantidad_ceros == dgvDetalle.Rows.Count)
				{
					MessageBox.Show("No se puede enviar el requerimiento");
					return;
				}
				if (chkReqDet.Checked)
				{
					for (int i = 0; i < DtAuxiliar_pr.Rows.Count; i++)
					{
						for (int j = 0; j < DtAuxiliar_pasados_pr.Rows.Count; j++)
						{
							if (Convert.ToInt32(DtAuxiliar_pr.Rows[i]["coddetalle"].ToString()) == Convert.ToInt32(DtAuxiliar_pasados_pr.Rows[j]["coddetalle"].ToString()))
							{
								if (Convert.ToDecimal(DtAuxiliar_pr.Rows[i]["Cantidad"].ToString()) <= Convert.ToDecimal(DtAuxiliar_pasados_pr.Rows[j]["Cantidad"].ToString()))
								{
									estado_vigente = 3;
									contador_vigentes++;
								}
								else
								{
									estado_vigente = 2;
								}
								contador_veces++;
							}
							admrequerimiento.actualiza_det_requerimientos_comentario(Convert.ToInt32(DtAuxiliar_pasados_pr.Rows[j]["coddetalle"].ToString()), DtAuxiliar_pasados_pr.Rows[j]["comentario_usu"].ToString());
						}
						if (contador_veces == 0)
						{
							estado_vigente = 2;
						}
						detallerequerimiento.CodDetalleRequerimiento = Convert.ToInt32(DtAuxiliar_pr.Rows[i]["coddetalle"].ToString());
						detallerequerimiento.CodProducto = Convert.ToInt32(DtAuxiliar_pr.Rows[i]["codProducto"].ToString());
						detallerequerimiento.EstadoVigente = estado_vigente;
						resultado_accion = admrequerimiento.actualiza_det_requerimientos_vigentes(detallerequerimiento);
						contador_veces = 0;
						estado_vigente = 2;
					}
					estado_vigente = ((contador_vigentes == DtAuxiliar_pr.Rows.Count) ? 3 : 2);
					requerimiento.CodRequerimiento = Convert.ToInt32(DtAuxiliar_pr.Rows[0]["codrequerimiento"].ToString());
					requerimiento.Atendido = estado_vigente;
					resultado_accion = admrequerimiento.actualiza_requerimientos_vigentes(requerimiento);
				}
				nota.CodSucursal = frmLogin.iCodSucursal;
				nota.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue.ToString());
				nota.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
				nota.CodSerie = Convert.ToInt32(txtcodserie.Text);
				nota.Serie = txtSerie.Text;
				if (txtNumero.Text != "")
				{
					nota.NumDoc = txtNumero.Text;
				}
				else
				{
					nota.NumDoc = ser.Numeracion.ToString();
				}
				txtNumero.Text = nota.NumDoc;
				nota.Comentario = txtComentario.Text;
				nota.FechaSalida = dtpFecha.Value;
				nota.CodUser = frmLogin.iCodUser;
				nota.FechaPago = dtpFecha.Value;
				if (cmbTransportista.SelectedValue == null)
				{
					nota.codConductor = 0;
				}
				else
				{
					nota.codConductor = Convert.ToInt32(cmbTransportista.SelectedValue.ToString());
				}
				if (cmbVehiculo.SelectedValue == null)
				{
					nota.codVehiculoTransporte = 0;
				}
				else
				{
					nota.codVehiculoTransporte = Convert.ToInt32(cmbVehiculo.SelectedValue.ToString());
				}
				if (cmbAlmacenDestino.SelectedValue == null)
				{
					nota.codalmacenreceptor = 0;
				}
				else
				{
					nota.codalmacenreceptor = Convert.ToInt32(cmbAlmacenDestino.SelectedValue.ToString());
				}
				nota.Moneda = 2;
				nota.Estado = 1;
				nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				if (Proceso != 1)
				{
					return;
				}
				if (admnota.insert(nota))
				{
					RecorreDetalle();
					if (detalle.Count > 0)
					{
						foreach (clsDetalleNotaSalida det in detalle)
						{
							if (det.Cantidad > 0.0)
							{
								admnota.insertdetalle(det);
							}
						}
					}
				}
				guia.CodAlmacen = frmLogin.iCodAlmacen;
				guia.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
				guia.CodSerie = Convert.ToInt32(txtcodserie.Text);
				guia.Serie = txtSerie.Text;
				if (txtNumero.Text != "")
				{
					guia.NumDoc = txtNumero.Text;
				}
				else
				{
					guia.NumDoc = ser.Numeracion.ToString();
				}
				if (cmbTransferencia.SelectedIndex == 0)
				{
					guia.CodMotivo = 5;
				}
				else if (cmbTransferencia.SelectedIndex == 1)
				{
					guia.CodMotivo = 4;
				}
				guia.FechaEmision = dtpEmision.Value.Date;
				guia.FechaTraslado = dtpTraslado.Value.Date;
				guia.CodCliente = 0;
				if (cmbVehiculo.SelectedValue == null)
				{
					guia.CodVehiculoTransporte = 0;
				}
				else
				{
					guia.CodVehiculoTransporte = Convert.ToInt32(cmbVehiculo.SelectedValue.ToString());
				}
				if (cmbTransportista.SelectedValue == null)
				{
					guia.CodConductor = 0;
				}
				else
				{
					guia.CodConductor = Convert.ToInt32(cmbTransportista.SelectedValue.ToString());
				}
				if (CodEmpresaTransporte != 0)
				{
					guia.CodEmpresaTransporte = empT.CodEmpresaTranporte;
				}
				guia.Facturado = 0;
				guia.Comentario = txtComentario.Text;
				guia.Estado = 1;
				guia.CodUser = frmLogin.iCodUser;
				guia.CodFactura = Convert.ToInt32(nota.CodNotaSalida);
				if (txtReq.Text == "")
				{
					guia.CodReq = 0;
				}
				else
				{
					guia.CodReq = Convert.ToInt32(txtReq.Text);
				}
				if (admGuia.insert(guia))
				{
					RecorreDetalleGuia();
					if (detalleguia.Count > 0)
					{
						foreach (clsDetalleGuiaRemision det2 in detalleguia)
						{
							if (det2.Cantidad > 0.0)
							{
								admGuia.insertdetalle(det2);
							}
						}
					}
				}
				notaIngreso.CodAlmacen = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
				notaIngreso.codalmacenemisor = Convert.ToInt32(cmbAlmacen.SelectedValue);
				notaIngreso.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
				notaIngreso.CodSerie = Convert.ToInt32(txtcodserie.Text);
				notaIngreso.Serie = txtSerie.Text;
				if (txtNumero.Text != "")
				{
					notaIngreso.NumDoc = txtNumero.Text;
				}
				else
				{
					notaIngreso.NumDoc = ser.Numeracion.ToString();
				}
				notaIngreso.Comentario = txtComentario.Text;
				notaIngreso.FechaIngreso = dtpFecha.Value;
				notaIngreso.CodUser = frmLogin.iCodUser;
				notaIngreso.CodReferencia = Convert.ToInt32(nota.CodNotaSalida);
				notaIngreso.Moneda = 2;
				notaIngreso.Estado = 1;
				notaIngreso.FechaPago = dtpFecha.Value;
				notaIngreso.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
				if (!admNotaIngreso.insert(notaIngreso))
				{
					return;
				}
				RecorreDetalleIngreso();
				if (detalleingreso.Count > 0)
				{
					foreach (clsDetalleNotaIngreso det3 in detalleingreso)
					{
						if (det3.Cantidad > 0.0)
						{
							admNotaIngreso.insertdetalle(det3);
						}
					}
				}
				MessageBox.Show("Los datos se guardaron correctamente", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtNumDoc.Text = nota.CodNotaSalida.ToString().PadLeft(11, '0');
				sololectura(estado: true);
				fnImprimir();
				Close();
			}
			else if (Proceso == 3)
			{
				notaIngreso.CodNotaIngreso = CodTransferencia;
				notaIngreso.Comentario = txtComentario.Text;
				if (admNotaIngreso.UpdateComentario(notaIngreso))
				{
					MessageBoxEx.Show("Se actualizo la Nota de Ingreso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		else
		{
			if (!superValidator1.Validate())
			{
				return;
			}
			nota.CodSucursal = frmLogin.iCodSucursal;
			nota.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue.ToString());
			nota.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
			nota.CodSerie = Convert.ToInt32(txtcodserie.Text);
			nota.Serie = txtSerie.Text;
			nota.NumDoc = ser.Numeracion.ToString();
			txtNumero.Text = nota.NumDoc;
			nota.Comentario = txtComentario.Text;
			nota.FechaSalida = dtpFecha.Value.Date;
			nota.CodUser = frmLogin.iCodUser;
			nota.codConductor = 0;
			nota.codVehiculoTransporte = 0;
			nota.codalmacenreceptor = 0;
			nota.Moneda = 2;
			nota.Estado = 1;
			if (Proceso == 1 && admnota.insert(nota))
			{
				RecorreDetalle();
				if (detalle.Count > 0)
				{
					foreach (clsDetalleNotaSalida det4 in detalle)
					{
						if (det4.Cantidad > 0.0)
						{
							admnota.insertdetalle(det4);
						}
					}
				}
			}
			notaIngreso.CodAlmacen = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
			notaIngreso.codalmacenemisor = Convert.ToInt32(cmbAlmacen.SelectedValue);
			notaIngreso.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
			notaIngreso.CodSerie = Convert.ToInt32(txtcodserie.Text);
			notaIngreso.Serie = txtSerie.Text;
			notaIngreso.NumDoc = ser.Numeracion.ToString();
			txtNumero.Text = nota.NumDoc;
			notaIngreso.Comentario = txtComentario.Text;
			notaIngreso.FechaIngreso = dtpFecha.Value.Date;
			notaIngreso.CodUser = frmLogin.iCodUser;
			notaIngreso.CodReferencia = Convert.ToInt32(nota.CodNotaSalida);
			notaIngreso.Moneda = 2;
			notaIngreso.Estado = 1;
			if (!admNotaIngreso.insert(notaIngreso))
			{
				return;
			}
			RecorreDetalleIngreso();
			if (detalleingreso.Count > 0)
			{
				foreach (clsDetalleNotaIngreso det5 in detalleingreso)
				{
					if (det5.Cantidad > 0.0)
					{
						admNotaIngreso.insertdetalle(det5);
					}
				}
			}
			MessageBox.Show("Los datos se guardaron correctamente", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtNumDoc.Text = nota.CodNotaSalida.ToString().PadLeft(11, '0');
			sololectura(estado: true);
		}
	}

	private int recorre_ceros()
	{
		int contador = 0;
		int valor = 0;
		if (dgvDetalle.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				if (Convert.ToUInt32(row.Cells[cantidad.Name].Value) == 0)
				{
					contador++;
				}
			}
		}
		return contador;
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

	private void RecorreDetalleIngreso()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleIngreso(row);
		}
	}

	private void RecorreDetalleGuia()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			añadedetalleGuia(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodNotaSalida = Convert.ToInt32(nota.CodNotaSalida);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value) * Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valorpromedio.Name].Value);
		deta.ValorRealSoles = Convert.ToDouble(fila.Cells[valorpromediosoles.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		detalle.Add(deta);
	}

	private void añadedetalleIngreso(DataGridViewRow fila)
	{
		clsDetalleNotaIngreso deta = new clsDetalleNotaIngreso();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodNotaIngreso = Convert.ToInt32(notaIngreso.CodNotaIngreso);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value) * Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valorpromedio.Name].Value);
		deta.ValorrealSoles = Convert.ToDouble(fila.Cells[valorpromediosoles.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);
		deta.CodAlmacen = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
		deta.CodDetalleRequerimiento = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
		deta.Bonificacion = false;
		detalleingreso.Add(deta);
	}

	private void añadedetalleGuia(DataGridViewRow fila)
	{
		clsDetalleGuiaRemision deta = new clsDetalleGuiaRemision();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.CodGuiaRemision = Convert.ToInt32(guia.CodGuiaRemision);
		deta.CodAlmacen = frmLogin.iCodAlmacen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = "";
		deta.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
		deta.CantidadPendiente = 0.0;
		deta.Peso = 0.0;
		deta.CodUser = frmLogin.iCodUser;
		detalleguia.Add(deta);
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (Proceso == 1)
		{
			codProd.Remove(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value));
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
		}
		else if (Proceso == 2 && dgvDetalle.CurrentRow.Index != -1)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Detalle Nota Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && admnota.deletedetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value)))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Detalle Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaDetalle();
			}
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDetalleGuia"] != null)
		{
			Application.OpenForms["frmDetalleGuia"].Activate();
			return;
		}
		frmDetalleGuia form = new frmDetalleGuia();
		form.Procede = 1;
		form.Proceso = 1;
		form.ShowDialog();
	}

	public void fnImprimir()
	{
		try
		{
			ser = admSerie.MuestraSerie(Convert.ToInt32(txtcodserie.Text), frmLogin.iCodAlmacen);
			if (cmbTransferencia.SelectedIndex == 0)
			{
				if (frmLogin.iCodAlmacen == 20 || frmLogin.iCodAlmacen == 21)
				{
					ReportDocument rd = new ReportDocument();
					rd.Load("CRGuiaRemisionNewFormat.rpt");
					CRGuiaRemisionNewFormat rpt = new CRGuiaRemisionNewFormat();
					rd.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), Convert.ToInt32(cmbAlmacen.SelectedValue), 15));
					PrintOptions rptoption = rd.PrintOptions;
					rptoption.PrinterName = ser.NombreImpresora;
					rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
					rd.PrintToPrinter(1, collated: false, 1, 1);
					rd.Close();
					rd.Dispose();
				}
				else
				{
					ReportDocument rd2 = new ReportDocument();
					rd2.Load("CRGuiaRemision.rpt");
					CRGuiaRemision rpt2 = new CRGuiaRemision();
					rd2.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(guia.CodGuiaRemision), Convert.ToInt32(cmbAlmacen.SelectedValue), 15));
					PrintOptions rptoption2 = rd2.PrintOptions;
					rptoption2.PrinterName = ser.NombreImpresora;
					rptoption2.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
					rd2.PrintToPrinter(1, collated: false, 1, 1);
					rd2.Close();
					rd2.Dispose();
				}
			}
			else if (cmbTransferencia.SelectedIndex == 1)
			{
				frmrptCotizacion frm = new frmrptCotizacion();
				frm.CodCotizacion = Convert.ToInt32(txtNumDoc.Text);
				frm.tipo = 16;
				frm.ShowDialog();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
	}

	private void txtRequerimiento_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1 && Convert.ToInt32(cmbAlmacen.SelectedValue) > 0)
		{
			frmConsolidadoTransferencia form = new frmConsolidadoTransferencia();
			chkReqDet.Checked = true;
			txtAlmacenDestino.Text = "";
			form.proceso = 9;
			form.proce = Proceso;
			form.Alm = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
			form.Almaori = Convert.ToInt32(cmbAlmacen.SelectedValue);
			dgvDetalle.Rows.Clear();
			if (Application.OpenForms["frmConsolidadoTransferencia"] != null)
			{
				Application.OpenForms["frmConsolidadoTransferencia"].Activate();
			}
			else
			{
				form.ShowDialog();
			}
		}
		else
		{
			MessageBox.Show("Seleccione el Almacen");
			cmbAlmacen.Focus();
		}
	}

	private void frmTranferenciaDirecta_Shown(object sender, EventArgs e)
	{
		txtDocRef.Focus();
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (txtRUCTransporte.Text == "")
		{
			if (c.Enabled)
			{
				if (Proceso != 0 && c.Visible)
				{
					if (c.SelectedIndex != -1)
					{
						e.IsValid = true;
					}
					else
					{
						e.IsValid = false;
					}
				}
				else
				{
					e.IsValid = true;
				}
			}
			else
			{
				e.IsValid = true;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (dgvDetalle.Rows.Count > 0)
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)
		{
			val.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "cantidad" && txtedit.Text != "")
			{
				if (Convert.ToDouble(txtedit.Text) > Convert.ToDouble(dgvDetalle.CurrentRow.Cells[stockactual.Name].Value))
				{
					MessageBox.Show("Cantidad Debe Ser Menor o Igual al Stock");
					guarda = 1;
				}
				else
				{
					guarda = 0;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void dgvDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvDetalle_KeyPress;
			txtedit.KeyPress += dgvDetalle_KeyPress;
			txtedit.KeyUp -= dgvDetalle_KeyUp;
			txtedit.KeyUp += dgvDetalle_KeyUp;
		}
	}

	private void cmbTransferencia_SelectedIndexChanged(object sender, EventArgs e)
	{
		txtSerie.Text = "";
		txtNumero.Text = "";
		txtAlmacenDestino.Text = "";
		txtDocRef.Text = "";
		CodDocumento = 0;
		txtTransaccion.Text = "";
		if (cmbTransferencia.SelectedIndex == 0)
		{
			txtSerie.Visible = true;
			txtNumero.Visible = true;
			label3.Visible = true;
			label20.Visible = true;
			txtAlmacenDestino.Visible = true;
			txtTransaccion.Text = "TD";
			notaIngreso.CodTipoTransaccion = 15;
			comentario_usu.Visible = true;
			Proceso_txd = 0;
		}
		else if (cmbTransferencia.SelectedIndex == 1)
		{
			label4.Visible = false;
			label6.Visible = false;
			label10.Visible = false;
			label11.Visible = false;
			cmbTransportista.Visible = false;
			cmbVehiculo.Visible = false;
			dtpEmision.Visible = false;
			dtpTraslado.Visible = false;
			label20.Visible = false;
			txtAlmacenDestino.Visible = false;
			txtTransaccion.Text = "TXD";
			notaIngreso.CodTipoTransaccion = 16;
			comentario_usu.Visible = false;
			Proceso_txd = 1;
		}
	}

	private void cmbAlmacenDestino_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbAlmacenDestino.SelectedValue) == frmLogin.iCodAlmacen)
		{
			MessageBox.Show("No se puede Hacer Transferencia en el mismo Almacen!");
			cmbAlmacenDestino.SelectedIndex = -1;
		}
		if (Convert.ToInt32(cmbAlmacenDestino.SelectedValue) > 0)
		{
			almacen = Admalmac.CargaAlmacen(Convert.ToInt32(cmbAlmacenDestino.SelectedValue));
			if (almacen != null)
			{
				txtAlmacenDestino.Text = almacen.Ubicacion;
			}
		}
	}

	private void btnAceptarTransferencia_Click(object sender, EventArgs e)
	{
		DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aceptar la Transferencia seleccionada", "Transferencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.No)
		{
			return;
		}
		foreach (clsDetalleNotaIngreso lista in detalleingreso)
		{
			if (admNotaIngreso.ActualizaStockPA(notaIngreso.codalmacenemisor, notaIngreso.CodAlmacen, lista.CodProducto, lista.CodNotaIngreso, Convert.ToDecimal(lista.Cantidad), Convert.ToDecimal(lista.PrecioUnitario), Convert.ToDecimal(lista.ValoReal), Convert.ToDecimal(lista.ValorrealSoles), frmLogin.iCodUser, notaIngreso.Serie, notaIngreso.NumDoc, notaIngreso.CodSerie))
			{
				admNotaIngreso.ActualizaStockAR(notaIngreso.codalmacenemisor, notaIngreso.CodAlmacen, lista.CodProducto, lista.CodNotaIngreso, Convert.ToDecimal(lista.Cantidad), Convert.ToDecimal(lista.PrecioUnitario), Convert.ToDecimal(lista.ValoReal), Convert.ToDecimal(lista.ValorrealSoles), frmLogin.iCodUser, notaIngreso.Serie, notaIngreso.NumDoc, notaIngreso.CodSerie);
			}
		}
	}

	private void customValidator1_ValidateValue_1(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Enabled)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator2_ValidateValue_1(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Text != "")
			{
				e.IsValid = true;
			}
			else
			{
				e.IsValid = false;
			}
		}
		else
		{
			e.IsValid = true;
		}
	}

	private void txtRUCTransporte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmEmpresaTransporte"] != null)
		{
			Application.OpenForms["frmEmpresaTransporte"].Activate();
			return;
		}
		frmEmpresaTransporte form = new frmEmpresaTransporte();
		form.Proceso = 3;
		form.ShowDialog();
		empT = form.emp;
		CodEmpresaTransporte = empT.CodEmpresaTranporte;
		if (CodEmpresaTransporte != 0)
		{
			CargaEmpresaTransporte();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaEmpresaTransporte()
	{
		empT = AdmET.MuestraEmpresaTranporte(empT.CodEmpresaTranporte);
		if (empT != null)
		{
			txtRUCTransporte.Text = empT.Ruc;
			txtRazonSocialTransporte.Text = empT.RazonSocial;
		}
		else
		{
			txtRUCTransporte.Text = "";
			txtRazonSocialTransporte.Text = "";
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmTranferenciaDirecta));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.txtReq = new System.Windows.Forms.TextBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtRazonSocialTransporte = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtRUCTransporte = new System.Windows.Forms.TextBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.cmbTransportista = new System.Windows.Forms.ComboBox();
		this.cmbVehiculo = new System.Windows.Forms.ComboBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.dtpTraslado = new System.Windows.Forms.DateTimePicker();
		this.label11 = new System.Windows.Forms.Label();
		this.dtpEmision = new System.Windows.Forms.DateTimePicker();
		this.label10 = new System.Windows.Forms.Label();
		this.chkReqDet = new System.Windows.Forms.CheckBox();
		this.cmbAlmacenDestino = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtNumDoc = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodDoc = new System.Windows.Forms.TextBox();
		this.txtAlmacenDestino = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.lbAlmacen = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbTransferencia = new System.Windows.Forms.ComboBox();
		this.txtTransaccion = new System.Windows.Forms.TextBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.txtAlmacenOri = new System.Windows.Forms.TextBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnAceptarTransferencia = new System.Windows.Forms.Button();
		this.imageList3 = new System.Windows.Forms.ImageList(this.components);
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromediosoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciopromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockactual = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.comentario_usu = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estadototal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.cachedCRCuotasPrestamo1 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.cachedCRCuotasPrestamo2 = new SIGEFA.Reportes.clsReportes.CachedCRCuotasPrestamo();
		this.label3 = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtcodserie = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.txtReq);
		this.groupBox1.Controls.Add(this.groupBox5);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.dtpTraslado);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.dtpEmision);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.chkReqDet);
		this.groupBox1.Controls.Add(this.cmbAlmacenDestino);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.txtNumDoc);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtcodserie);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtCodDoc);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtAlmacenDestino);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.lbAlmacen);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbTransferencia);
		this.groupBox1.Controls.Add(this.txtTransaccion);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.txtAlmacenOri);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(778, 319);
		this.groupBox1.TabIndex = 21;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Location = new System.Drawing.Point(689, 175);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 85;
		this.txtTipoCambio.Tag = "15";
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtReq.Location = new System.Drawing.Point(749, 98);
		this.txtReq.Name = "txtReq";
		this.txtReq.Size = new System.Drawing.Size(23, 20);
		this.txtReq.TabIndex = 84;
		this.txtReq.Visible = false;
		this.groupBox5.Controls.Add(this.txtRazonSocialTransporte);
		this.groupBox5.Controls.Add(this.label14);
		this.groupBox5.Controls.Add(this.label13);
		this.groupBox5.Controls.Add(this.txtRUCTransporte);
		this.groupBox5.Location = new System.Drawing.Point(353, 201);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(413, 75);
		this.groupBox5.TabIndex = 83;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Empresa de Tranportes";
		this.txtRazonSocialTransporte.Location = new System.Drawing.Point(78, 40);
		this.txtRazonSocialTransporte.Name = "txtRazonSocialTransporte";
		this.txtRazonSocialTransporte.ReadOnly = true;
		this.txtRazonSocialTransporte.Size = new System.Drawing.Size(327, 20);
		this.txtRazonSocialTransporte.TabIndex = 23;
		this.txtRazonSocialTransporte.Tag = "5";
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(42, 17);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(30, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "RUC";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(11, 43);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(61, 13);
		this.label13.TabIndex = 20;
		this.label13.Text = "Raz. Social";
		this.txtRUCTransporte.Location = new System.Drawing.Point(78, 14);
		this.txtRUCTransporte.Name = "txtRUCTransporte";
		this.txtRUCTransporte.Size = new System.Drawing.Size(147, 20);
		this.txtRUCTransporte.TabIndex = 22;
		this.txtRUCTransporte.Tag = "5";
		this.txtRUCTransporte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUCTransporte_KeyDown);
		this.groupBox4.Controls.Add(this.cmbTransportista);
		this.groupBox4.Controls.Add(this.cmbVehiculo);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Location = new System.Drawing.Point(6, 192);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(331, 81);
		this.groupBox4.TabIndex = 82;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos del Transporte / Conductor";
		this.cmbTransportista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTransportista.FormattingEnabled = true;
		this.cmbTransportista.Location = new System.Drawing.Point(91, 23);
		this.cmbTransportista.Name = "cmbTransportista";
		this.cmbTransportista.Size = new System.Drawing.Size(235, 21);
		this.cmbTransportista.TabIndex = 71;
		this.superValidator1.SetValidator1(this.cmbTransportista, this.customValidator3);
		this.cmbVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbVehiculo.FormattingEnabled = true;
		this.cmbVehiculo.Location = new System.Drawing.Point(91, 49);
		this.cmbVehiculo.Name = "cmbVehiculo";
		this.cmbVehiculo.Size = new System.Drawing.Size(235, 21);
		this.cmbVehiculo.TabIndex = 72;
		this.superValidator1.SetValidator1(this.cmbVehiculo, this.customValidator3);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(6, 52);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(51, 13);
		this.label6.TabIndex = 74;
		this.label6.Text = "Vehiculo:";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(6, 26);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(71, 13);
		this.label4.TabIndex = 73;
		this.label4.Text = "Transportista:";
		this.dtpTraslado.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpTraslado.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpTraslado.Location = new System.Drawing.Point(565, 282);
		this.dtpTraslado.Name = "dtpTraslado";
		this.dtpTraslado.Size = new System.Drawing.Size(81, 20);
		this.dtpTraslado.TabIndex = 11;
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(475, 288);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(84, 13);
		this.label11.TabIndex = 81;
		this.label11.Text = "Fecha Traslado:";
		this.dtpEmision.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpEmision.Location = new System.Drawing.Point(220, 282);
		this.dtpEmision.Name = "dtpEmision";
		this.dtpEmision.Size = new System.Drawing.Size(81, 20);
		this.dtpEmision.TabIndex = 10;
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(110, 288);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(104, 13);
		this.label10.TabIndex = 79;
		this.label10.Text = "Fecha Emision Guia:";
		this.chkReqDet.AutoSize = true;
		this.chkReqDet.Location = new System.Drawing.Point(689, 101);
		this.chkReqDet.Name = "chkReqDet";
		this.chkReqDet.Size = new System.Drawing.Size(60, 17);
		this.chkReqDet.TabIndex = 78;
		this.chkReqDet.Text = "chkRD";
		this.chkReqDet.UseVisualStyleBackColor = true;
		this.chkReqDet.Visible = false;
		this.cmbAlmacenDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbAlmacenDestino.FormattingEnabled = true;
		this.cmbAlmacenDestino.Location = new System.Drawing.Point(448, 72);
		this.cmbAlmacenDestino.Name = "cmbAlmacenDestino";
		this.cmbAlmacenDestino.Size = new System.Drawing.Size(235, 21);
		this.cmbAlmacenDestino.TabIndex = 6;
		this.cmbAlmacenDestino.Tag = "";
		this.superValidator1.SetValidator1(this.cmbAlmacenDestino, this.customValidator3);
		this.cmbAlmacenDestino.SelectionChangeCommitted += new System.EventHandler(cmbAlmacenDestino_SelectionChangeCommitted);
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(350, 75);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(87, 13);
		this.label8.TabIndex = 74;
		this.label8.Tag = "";
		this.label8.Text = "Almacen Destino";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 48);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 57;
		this.label5.Text = "Doc. Ref.";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Location = new System.Drawing.Point(102, 45);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(42, 20);
		this.txtDocRef.TabIndex = 2;
		this.txtDocRef.Tag = "";
		this.txtDocRef.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDocRef_KeyDown);
		this.txtNumDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumDoc.Enabled = false;
		this.txtNumDoc.Location = new System.Drawing.Point(516, 42);
		this.txtNumDoc.Name = "txtNumDoc";
		this.txtNumDoc.ReadOnly = true;
		this.txtNumDoc.Size = new System.Drawing.Size(115, 20);
		this.txtNumDoc.TabIndex = 66;
		this.txtNumDoc.Visible = false;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(447, 45);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(58, 13);
		this.label7.TabIndex = 67;
		this.label7.Text = "Num. Doc.";
		this.label7.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(17, 23);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 13);
		this.label2.TabIndex = 65;
		this.label2.Text = "Transacción";
		this.txtCodDoc.Enabled = false;
		this.txtCodDoc.Location = new System.Drawing.Point(431, 46);
		this.txtCodDoc.Name = "txtCodDoc";
		this.txtCodDoc.ReadOnly = true;
		this.txtCodDoc.Size = new System.Drawing.Size(10, 20);
		this.txtCodDoc.TabIndex = 60;
		this.txtCodDoc.Visible = false;
		this.txtAlmacenDestino.Location = new System.Drawing.Point(102, 99);
		this.txtAlmacenDestino.Name = "txtAlmacenDestino";
		this.txtAlmacenDestino.Size = new System.Drawing.Size(581, 20);
		this.txtAlmacenDestino.TabIndex = 44;
		this.txtAlmacenDestino.Tag = "";
		this.txtAlmacenDestino.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRequerimiento_KeyDown);
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(20, 102);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(76, 13);
		this.label20.TabIndex = 43;
		this.label20.Tag = "";
		this.label20.Text = "Direccion A.D.";
		this.lbAlmacen.AutoSize = true;
		this.lbAlmacen.Location = new System.Drawing.Point(17, 77);
		this.lbAlmacen.Name = "lbAlmacen";
		this.lbAlmacen.Size = new System.Drawing.Size(82, 13);
		this.lbAlmacen.TabIndex = 41;
		this.lbAlmacen.Tag = "";
		this.lbAlmacen.Text = "Almacen Origen";
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Location = new System.Drawing.Point(691, 283);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(75, 23);
		this.btnDetalle.TabIndex = 13;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtComentario.Location = new System.Drawing.Point(102, 125);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(581, 61);
		this.txtComentario.TabIndex = 12;
		this.txtComentario.Tag = "";
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(20, 128);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(60, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "";
		this.label9.Text = "Comentario";
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(685, 21);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 7;
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(636, 23);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.cmbTransferencia.FormattingEnabled = true;
		this.cmbTransferencia.Items.AddRange(new object[1] { "TRANSFERENCIA DIRECTA" });
		this.cmbTransferencia.Location = new System.Drawing.Point(102, 18);
		this.cmbTransferencia.Name = "cmbTransferencia";
		this.cmbTransferencia.Size = new System.Drawing.Size(339, 21);
		this.cmbTransferencia.TabIndex = 1;
		this.cmbTransferencia.Tag = "";
		this.superValidator1.SetValidator1(this.cmbTransferencia, this.customValidator3);
		this.cmbTransferencia.SelectedIndexChanged += new System.EventHandler(cmbTransferencia_SelectedIndexChanged);
		this.txtTransaccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtTransaccion.Location = new System.Drawing.Point(102, 20);
		this.txtTransaccion.Name = "txtTransaccion";
		this.txtTransaccion.Size = new System.Drawing.Size(28, 20);
		this.txtTransaccion.TabIndex = 64;
		this.txtTransaccion.Visible = false;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(102, 72);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(235, 21);
		this.cmbAlmacen.TabIndex = 5;
		this.cmbAlmacen.Tag = "";
		this.superValidator1.SetValidator1(this.cmbAlmacen, this.customValidator3);
		this.txtAlmacenOri.Location = new System.Drawing.Point(102, 72);
		this.txtAlmacenOri.Name = "txtAlmacenOri";
		this.txtAlmacenOri.Size = new System.Drawing.Size(235, 20);
		this.txtAlmacenOri.TabIndex = 77;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnAceptarTransferencia);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 560);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(778, 50);
		this.groupBox3.TabIndex = 24;
		this.groupBox3.TabStop = false;
		this.btnAceptarTransferencia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAceptarTransferencia.ImageIndex = 11;
		this.btnAceptarTransferencia.ImageList = this.imageList3;
		this.btnAceptarTransferencia.Location = new System.Drawing.Point(358, 12);
		this.btnAceptarTransferencia.Name = "btnAceptarTransferencia";
		this.btnAceptarTransferencia.Size = new System.Drawing.Size(147, 32);
		this.btnAceptarTransferencia.TabIndex = 19;
		this.btnAceptarTransferencia.Text = "Aceptar Transferencia";
		this.btnAceptarTransferencia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptarTransferencia.UseVisualStyleBackColor = true;
		this.btnAceptarTransferencia.Visible = false;
		this.btnAceptarTransferencia.Click += new System.EventHandler(btnAceptarTransferencia_Click);
		this.imageList3.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList3.ImageStream");
		this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList3.Images.SetKeyName(0, "Write Document.png");
		this.imageList3.Images.SetKeyName(1, "New Document.png");
		this.imageList3.Images.SetKeyName(2, "Remove Document.png");
		this.imageList3.Images.SetKeyName(3, "document-print.png");
		this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList3.Images.SetKeyName(5, "exit.png");
		this.imageList3.Images.SetKeyName(6, "search (1).png");
		this.imageList3.Images.SetKeyName(7, "Glossy-Open-icon.png");
		this.imageList3.Images.SetKeyName(8, "folder-open-icon (1).png");
		this.imageList3.Images.SetKeyName(9, "document_delete.png");
		this.imageList3.Images.SetKeyName(10, "DeleteRed.png");
		this.imageList3.Images.SetKeyName(11, "OK_Verde.png");
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(534, 12);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 16;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(704, 12);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 17;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(621, 12);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 15;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(20, 12);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 14;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 319);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(778, 241);
		this.groupBox2.TabIndex = 26;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.valorpromedio, this.valorpromediosoles, this.preciopromedio, this.stockactual, this.comentario_usu, this.estadototal);
		this.dgvDetalle.Location = new System.Drawing.Point(3, 19);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(772, 204);
		this.dgvDetalle.TabIndex = 25;
		this.superValidator1.SetValidator1(this.dgvDetalle, this.customValidator4);
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
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Format = "N2";
		dataGridViewCellStyle3.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle3;
		this.cantidad.HeaderText = "Cantidad a Enviar";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorpromedio.DataPropertyName = "valorpromedio";
		this.valorpromedio.HeaderText = "valorpromedio";
		this.valorpromedio.Name = "valorpromedio";
		this.valorpromedio.ReadOnly = true;
		this.valorpromediosoles.DataPropertyName = "valorpromediosoles";
		this.valorpromediosoles.HeaderText = "valorpromediosoles";
		this.valorpromediosoles.Name = "valorpromediosoles";
		this.valorpromediosoles.ReadOnly = true;
		this.preciopromedio.DataPropertyName = "preciopromedio";
		this.preciopromedio.HeaderText = "preciopromedio";
		this.preciopromedio.Name = "preciopromedio";
		this.preciopromedio.ReadOnly = true;
		this.stockactual.DataPropertyName = "stockactual";
		dataGridViewCellStyle4.Format = "N2";
		dataGridViewCellStyle4.NullValue = null;
		this.stockactual.DefaultCellStyle = dataGridViewCellStyle4;
		this.stockactual.HeaderText = "Stock Actual";
		this.stockactual.Name = "stockactual";
		this.stockactual.ReadOnly = true;
		this.comentario_usu.HeaderText = "Comentario de Usuario de Req.";
		this.comentario_usu.Name = "comentario_usu";
		this.comentario_usu.ReadOnly = true;
		this.estadototal.HeaderText = "estadototal";
		this.estadototal.Name = "estadototal";
		this.estadototal.ReadOnly = true;
		this.estadototal.Visible = false;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator3.ErrorMessage = "Seleccione almacen.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Your error message here.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue_1);
		this.customValidator1.ErrorMessage = "Your error message here.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue_1);
		this.customValidator4.ErrorMessage = "Llene Detalle.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(216, 48);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(34, 13);
		this.label3.TabIndex = 61;
		this.label3.Text = "Serie.";
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Enabled = false;
		this.txtNumero.Location = new System.Drawing.Point(333, 46);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 4;
		this.txtNumero.Tag = "";
		this.superValidator1.SetValidator1(this.txtNumero, this.customValidator1);
		this.txtcodserie.Location = new System.Drawing.Point(409, 46);
		this.txtcodserie.Name = "txtcodserie";
		this.txtcodserie.Size = new System.Drawing.Size(16, 20);
		this.txtcodserie.TabIndex = 62;
		this.txtcodserie.Visible = false;
		this.txtSerie.Location = new System.Drawing.Point(264, 46);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(61, 20);
		this.txtSerie.TabIndex = 3;
		this.superValidator1.SetValidator1(this.txtSerie, this.customValidator2);
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(778, 610);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MinimizeBox = false;
		base.Name = "frmTranferenciaDirecta";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "TransferenciaDirecta";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmNotaSalida_Load);
		base.Shown += new System.EventHandler(frmTranferenciaDirecta_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
