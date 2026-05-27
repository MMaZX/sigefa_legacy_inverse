using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.SunatFacElec;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using WinApp.API;
using WinApp.Comun.Dto.Intercambio;
using WinApp.Firmado;
using WinApp.Servicio;
using WinApp.Servicio.Soap;

namespace SIGEFA.Formularios;

public class frmResumenDiario : RadForm
{
	private DataTable dt_resumen = new DataTable();

	private clsAdmFacturaVenta admfv = new clsAdmFacturaVenta();

	private List<clsFacturaVenta> ltaFactVen = new List<clsFacturaVenta>();

	private clsAdmRepositorio AdmRepos = new clsAdmRepositorio();

	private clsAdmEmpresa admemp = new clsAdmEmpresa();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsAdmRepositorio clsadmrepo = new clsAdmRepositorio();

	private List<clsRepositorio> lista_repositorio = null;

	private clsEmpresa empresa = null;

	private Facturacion facturacion = new Facturacion();

	private string _ticket;

	private string NombreDocumento;

	private BackgroundWorker myBackgroundWorker;

	private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

	private IContainer components = null;

	private FluentTheme fluentTheme1;

	private RadDateTimePicker dtpFecha;

	private RadGroupBox radGroupBox1;

	private Button btnBuscar;

	private Button btnGenerar;

	private RadGridView dgVentas;

	private Button btnSalir;

	private RadDropDownList cmbAlmacenes;

	private RadLabel radLabel2;

	private RadLabel radLabel1;

	private RadLabel lblTotalDocumentos;

	private RadLabel radLabel3;

	private RadTextBox txtCodResumen;

	private RadPageView Generar;

	private RadPageViewPage radPageViewPage1;

	private RadPageViewPage Enviar;

	private RadGridView dgResumen;

	private RadGroupBox radGroupBox2;

	private RadToggleSwitch radToggleSwitch1;

	private RadLabel radLabel4;

	private RadDateTimePicker dtpHasta;

	private RadDropDownList cmbAlmacenes2;

	private RadLabel radLabel5;

	private RadLabel radLabel6;

	private Button btnEnviarResumen;

	private Button button2;

	private RadDateTimePicker dtpDesde;

	private Button button3;

	private Button btnConsultarTicket;

	private RadWaitingBar radWaitingBar1;

	private LineRingWaitingBarIndicatorElement lineRingWaitingBarIndicatorElement1;

	private BackgroundWorker backgroundWorker1;

	private RadWaitingBar radWaitingBar2;

	private LineRingWaitingBarIndicatorElement lineRingWaitingBarIndicatorElement2;

	private System.Windows.Forms.Timer timer1;

	public frmResumenDiario()
	{
		InitializeComponent();
	}

	private void cargaAlmacenes()
	{
		DataTable aux = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		aux.Rows.RemoveAt(0);
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = aux;
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
		cmbAlmacenes2.ValueMember = "cod";
		cmbAlmacenes2.DisplayMember = "nombre";
		cmbAlmacenes2.DataSource = aux;
		cmbAlmacenes2.SelectedValue = frmLogin.iCodAlmacen;
	}

	public void Listar()
	{
		try
		{
			int counter = 0;
			dgVentas.Rows.Clear();
			dt_resumen = admfv.ResumenDiario(dtpFecha.Value, frmLogin.iCodSucursal, Convert.ToInt32(cmbAlmacenes.SelectedValue));
			if (dt_resumen.Rows.Count > 0)
			{
				txtCodResumen.Text = admfv.getCantidadResumen(dtpFecha.Value, frmLogin.iCodSucursal, Convert.ToInt32(cmbAlmacenes.SelectedValue)).ToString();
				foreach (DataRow row in dt_resumen.Rows)
				{
					dgVentas.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15]);
					counter++;
				}
				lblTotalDocumentos.Text = counter + " DOCUMENTO(S) ENCONTRADO(S).";
			}
			else
			{
				dgVentas.Rows.Clear();
				lblTotalDocumentos.Text = "NINGUN DOCUMENTO ENCONTRADO.";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void Envio()
	{
		try
		{
			RecorreDetalle();
			facturacion.Generar_Resumen_Diario(ltaFactVen, dtpFecha.Value, Convert.ToInt32(txtCodResumen.Text), Convert.ToInt32(cmbAlmacenes.SelectedValue));
			Listar();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void RecorreDetalle()
	{
		if (dgVentas.Rows.Count <= 0)
		{
			return;
		}
		ltaFactVen.Clear();
		foreach (GridViewRowInfo row in dgVentas.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(GridViewRowInfo fila)
	{
		clsFacturaVenta fact = new clsFacturaVenta();
		fact.CodFacturaVenta = fila.Cells["codFacturaVenta"].Value.ToString();
		fact.CodTipoDocumento = Convert.ToInt32(fila.Cells["codTipoDocumento"].Value);
		fact.Serie = fila.Cells["serie"].Value.ToString();
		fact.NumDoc = fila.Cells["NumDocumento"].Value.ToString();
		fact.Moneda = Convert.ToInt32(fila.Cells["codMoneda"].Value);
		fact.Total = Convert.ToDecimal(fila.Cells["Total"].Value);
		fact.MontoDscto = Convert.ToDecimal(fila.Cells["Descuento"].Value);
		fact.Igv = Convert.ToDecimal(fila.Cells["Igv"].Value);
		fact.Gravadas = Convert.ToDecimal(fila.Cells["Gravadas"].Value);
		fact.Exoneradas = Convert.ToDecimal(fila.Cells["Exoneradas"].Value);
		fact.Inafectas = Convert.ToDecimal(fila.Cells["Inafectas"].Value);
		fact.Gratuitas = Convert.ToDecimal(fila.Cells["Gratuitas"].Value);
		fact.Tipoventa = Convert.ToInt32(fila.Cells["TipoVenta"].Value);
		fact.Doc_Cliente = fila.Cells["Documento_Cliente"].Value.ToString();
		ltaFactVen.Add(fact);
	}

	public void listar_repositorio(DateTime fechaInicio, DateTime fechaFin)
	{
		try
		{
			lista_repositorio = new List<clsRepositorio>();
			if (!radToggleSwitch1.Value)
			{
				lista_repositorio = clsadmrepo.listarDocumentoPendientesResumen(fechaInicio, fechaFin, frmLogin.iCodSucursal, Convert.ToInt32(cmbAlmacenes2.SelectedValue));
			}
			else
			{
				lista_repositorio = clsadmrepo.listarDocumentoEnviadosResumen(fechaInicio, fechaFin, frmLogin.iCodSucursal, Convert.ToInt32(cmbAlmacenes2.SelectedValue));
			}
			if (lista_repositorio != null)
			{
				if (lista_repositorio.Count > 0)
				{
					dgResumen.Rows.Clear();
					foreach (clsRepositorio rep in lista_repositorio)
					{
						dgResumen.Rows.Add(rep.Repoid, rep.Tipodoc, rep.Fechaemision.ToShortDateString(), rep.Serie, rep.Correlativo, rep.Monto, rep.Estadosunat, rep.Mensajesunat, rep.Nombredoc, rep.ticket);
					}
				}
				if (radToggleSwitch1.Value)
				{
					btnEnviarResumen.Enabled = false;
				}
				else
				{
					btnEnviarResumen.Enabled = true;
				}
				lblTotalDocumentos.Text = lista_repositorio.Count.ToString();
			}
			else
			{
				lblTotalDocumentos.Text = "0";
				dgResumen.Rows.Clear();
			}
		}
		catch (Exception ex)
		{
			RadMessageBox.Show(ex.Message);
		}
	}

	public async void EnvioResumen()
	{
		try
		{
			string tipodocumento = "";
			string _iddocumento = "";
			bool todocorrecto = false;
			if (lista_repositorio == null)
			{
				return;
			}
			if (lista_repositorio.Count > 0)
			{
				int codEmp = admemp.empresaxalmacen(Convert.ToInt32(cmbAlmacenes2.SelectedValue));
				empresa = admemp.CargaEmpresa3(codEmp);
				string rutacertificado = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\" + empresa.Certificado;
				foreach (clsRepositorio r in lista_repositorio)
				{
					string ruta1 = "";
					switch (r.Tipodoc)
					{
					case 1:
					{
						tipodocumento = "03";
						string[] iddocumento = r.Nombredoc.Split('-');
						_iddocumento = iddocumento[2] + "-" + iddocumento[3];
						ruta1 = Program.CarpetaBoletas + "\\" + r.Nombredoc + ".xml";
						break;
					}
					case 2:
					{
						tipodocumento = "01";
						string[] iddocumento = r.Nombredoc.Split('-');
						_iddocumento = iddocumento[2] + "-" + iddocumento[3];
						ruta1 = Program.CarpetaFacturas + "\\" + r.Nombredoc + ".xml";
						break;
					}
					case 4:
					{
						tipodocumento = "07";
						string[] iddocumento = r.Nombredoc.Split('-');
						_iddocumento = iddocumento[2] + "-" + iddocumento[3];
						ruta1 = Program.CarpetaNC + "\\" + r.Nombredoc + ".xml";
						break;
					}
					case 6:
					{
						tipodocumento = "08";
						string[] iddocumento = r.Nombredoc.Split('-');
						_iddocumento = iddocumento[2] + "-" + iddocumento[3];
						ruta1 = Program.CarpetaND + "\\" + r.Nombredoc + ".xml";
						break;
					}
					case 40:
						tipodocumento = "RC";
						_iddocumento = r.Nombredoc;
						ruta1 = Program.CarpetaResumen + "\\" + r.Nombredoc + ".xml";
						break;
					}
					EnviarDocumentoResponse rpta;
					EnviarResumenResponse rptaesu;
					if (File.Exists(ruta1))
					{
						string tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(ruta1));
						FirmadoRequest firmadoRequest = new FirmadoRequest
						{
							TramaXmlSinFirma = tramaXmlSinFirma,
							CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutacertificado)),
							PasswordCertificado = empresa.Contrasena,
							UnSoloNodoExtension = true
						};
						ICertificador certificador = new Certificador();
						FirmadoResponse respuestaFirmado = await new Firmar(certificador).Post(firmadoRequest);
						if (!respuestaFirmado.Exito)
						{
							RadMessageBox.Show(respuestaFirmado.MensajeError);
							return;
						}
						await facturacion.Enviar(empresa, _iddocumento, tipodocumento, respuestaFirmado.TramaXmlFirmado);
						rpta = facturacion.rpta;
						rptaesu = facturacion.rptaresumen;
					}
					else
					{
						string tramaXmlSinFirma2 = Convert.ToBase64String(r.Xml);
						new FirmadoRequest
						{
							TramaXmlSinFirma = tramaXmlSinFirma2,
							CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutacertificado)),
							PasswordCertificado = empresa.Contrasena,
							UnSoloNodoExtension = true
						};
						await facturacion.Enviar(empresa, _iddocumento, tipodocumento, tramaXmlSinFirma2);
						rpta = facturacion.rpta;
						rptaesu = facturacion.rptaresumen;
					}
					if (rpta != null)
					{
						if (rpta.CodigoRespuesta == "0" && rpta.TramaZipCdr != null)
						{
							r.Estadosunat = "0";
							r.Mensajesunat = ((rpta.MensajeRespuesta == null) ? "Documento Enviado" : rpta.MensajeRespuesta);
							string ruta2 = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\CDR\\R-" + r.Nombredoc + ".zip";
							File.WriteAllBytes(ruta2, Convert.FromBase64String(rpta.TramaZipCdr));
							File.WriteAllBytes(Program.CarpetaCdr + "\\R-" + r.Nombredoc + ".zip", Convert.FromBase64String(rpta.TramaZipCdr));
							if (File.Exists(ruta2))
							{
								r.CDR = File.ReadAllBytes(ruta2);
							}
							else
							{
								r.CDR = null;
							}
							todocorrecto = clsadmrepo.actualiza_repositorio(r);
							continue;
						}
						if (rpta.MensajeRespuesta != null)
						{
							r.Mensajesunat = rpta.MensajeRespuesta;
						}
						else
						{
							r.Mensajesunat = rpta.MensajeError;
						}
						if (rpta.CodigoRespuesta == "1033" || rpta.MensajeError.Contains("1033"))
						{
							r.Estadosunat = "0";
							clsadmrepo.actualiza_repositorio(r);
							continue;
						}
						r.Estadosunat = "-1";
						if (rpta.MensajeError != null && rpta.MensajeError != "")
						{
							r.Mensajesunat = rpta.MensajeError;
						}
						clsadmrepo.actualiza_repositorio(r);
					}
					else
					{
						if (rptaesu == null || rptaesu.NroTicket == null)
						{
							continue;
						}
						ConsultaTicketRequest consultaTicketRequest = new ConsultaTicketRequest
						{
							Ruc = empresa.Ruc,
							UsuarioSol = empresa.UsuarioSunat,
							ClaveSol = empresa.ClaveSunat,
							EndPointUrl = empresa.Url,
							IdDocumento = _iddocumento,
							NroTicket = rptaesu.NroTicket
						};
						ISerializador serializador = new Serializador();
						IServicioSunatDocumentos servicioSunatDocumentos = new ServicioSunatDocumentos();
						Thread.Sleep(4000);
						EnviarDocumentoResponse response = await new ConsultarTicket(servicioSunatDocumentos, serializador).PostSunat(consultaTicketRequest);
						if (response != null)
						{
							string ruta3 = AppDomain.CurrentDomain.BaseDirectory + "documentos\\CDR\\R-" + r.Nombredoc + ".zip";
							if (response.TramaZipCdr != null)
							{
								File.WriteAllBytes(ruta3, Convert.FromBase64String(response.TramaZipCdr));
								string ruta4 = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\CDR\\R-" + r.Nombredoc + ".zip";
								File.WriteAllBytes(ruta4, Convert.FromBase64String(response.TramaZipCdr));
							}
							r.CDR = (response.Exito ? File.ReadAllBytes(ruta3) : Convert.FromBase64String(""));
							r.Estadosunat = (response.Exito ? "0" : "-1");
							r.Mensajesunat = (response.Exito ? (response.MensajeRespuesta + "/ N° TICKET-" + rptaesu.NroTicket) : (response.MensajeRespuesta + " - " + response.MensajeError + "-" + response.CodigoRespuesta));
							r.ticket = rptaesu.NroTicket;
							if (!clsadmrepo.actualiza_repositorio(r))
							{
								RadMessageBox.Show("Problemas al actualizar estado de repositorio");
							}
							todocorrecto = true;
						}
						if (!response.Exito)
						{
							throw new ApplicationException(response.MensajeError);
						}
					}
				}
				if (todocorrecto)
				{
					RadMessageBox.Show("Los documentos fueron enviados de forma correcta");
					listar_repositorio(dtpDesde.Value, dtpHasta.Value);
				}
				else
				{
					RadMessageBox.Show("No todos los documentos fueron enviados de forma correcta");
					listar_repositorio(dtpDesde.Value, dtpHasta.Value);
				}
			}
			else
			{
				RadMessageBox.Show("No hay registros a enviar");
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			RadMessageBox.Show(a.Message);
			listar_repositorio(dtpDesde.Value, dtpHasta.Value);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	private void frmResumenDiario_Load(object sender, EventArgs e)
	{
		try
		{
			cargaAlmacenes();
			dtpFecha.Value = DateTime.Now;
			dtpDesde.Value = DateTime.Now;
			dtpHasta.Value = DateTime.Now;
			backgroundWorker1 = new BackgroundWorker();
			backgroundWorker1.WorkerReportsProgress = true;
			backgroundWorker1.WorkerSupportsCancellation = true;
			backgroundWorker1.DoWork += backgroundWorker1_DoWork;
			backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
			radWaitingBar1.Visible = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnBuscar_Click(object sender, EventArgs e)
	{
		timer.Interval = 1000;
		timer.Tick += timer1_Tick;
		radWaitingBar2.AssociatedControl = dgVentas;
		radWaitingBar2.StartWaiting();
		timer.Start();
	}

	private void btnGenerar_Click(object sender, EventArgs e)
	{
		if (dgVentas.Rows.Count > 0)
		{
			Cursor.Current = Cursors.WaitCursor;
			Envio();
			Listar();
			Cursor.Current = Cursors.Default;
		}
	}

	private void button2_Click(object sender, EventArgs e)
	{
		try
		{
			listar_repositorio(dtpDesde.Value, dtpHasta.Value);
		}
		catch (Exception ex)
		{
			RadMessageBox.Show(ex.Message);
		}
	}

	private void btnEnviarResumen_Click(object sender, EventArgs e)
	{
		EnvioResumen();
	}

	private async void button4_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgResumen.Rows.Count > 0)
			{
				try
				{
					if (_ticket != "")
					{
						int codEmp = admemp.empresaxalmacen(Convert.ToInt32(cmbAlmacenes2.SelectedValue));
						empresa = admemp.CargaEmpresa3(codEmp);
						ConsultaTicketRequest consultaTicketRequest = new ConsultaTicketRequest
						{
							Ruc = empresa.Ruc,
							UsuarioSol = empresa.UsuarioSunat,
							ClaveSol = empresa.ClaveSunat,
							EndPointUrl = empresa.Url,
							IdDocumento = NombreDocumento,
							NroTicket = _ticket
						};
						ISerializador serializador = new Serializador();
						IServicioSunatDocumentos servicioSunatDocumentos = new ServicioSunatDocumentos();
						EnviarDocumentoResponse response = await new ConsultarTicket(servicioSunatDocumentos, serializador).PostSunat(consultaTicketRequest);
						if (response.Exito)
						{
							RadMessageBox.Show(response.MensajeRespuesta);
						}
						else
						{
							RadMessageBox.Show(response.MensajeRespuesta + " - " + response.MensajeError);
						}
					}
					else
					{
						RadMessageBox.Show("No ha seleccionado fila o no existe N° de ticket ");
					}
					return;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}
			RadMessageBox.Show("No hay resumenes en el listado ");
		}
		catch (Exception ex2)
		{
			Exception a = ex2;
			RadMessageBox.Show(a.Message);
		}
	}

	private void dgResumen_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (dgResumen.Rows.Count > 0)
			{
				_ticket = Convert.ToString(dgResumen.CurrentRow.Cells["ticket"].Value);
				NombreDocumento = Convert.ToString(dgResumen.CurrentRow.Cells["Nombredoc"].Value);
				if (_ticket != "")
				{
					btnConsultarTicket.Visible = true;
				}
			}
		}
		catch (Exception ex)
		{
			RadMessageBox.Show(ex.Message);
		}
	}

	private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		radWaitingBar1.StopWaiting();
		radWaitingBar1.ResetWaiting();
		radWaitingBar1.Visible = false;
		DateTime end = DateTime.Now;
		if (e.Cancelled)
		{
			radLabel1.Text = "Calculations are canceled!";
		}
		else if (e.Error != null)
		{
			radLabel1.Text = "Error: " + e.Error.Message;
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		Listar();
		timer.Stop();
		radWaitingBar2.AssociatedControl = null;
		radWaitingBar2.StopWaiting();
		radWaitingBar2.ResetWaiting();
		radWaitingBar2.Visible = false;
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		this.fluentTheme1 = new Telerik.WinControls.Themes.FluentTheme();
		this.Generar = new Telerik.WinControls.UI.RadPageView();
		this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
		this.dgVentas = new Telerik.WinControls.UI.RadGridView();
		this.radWaitingBar2 = new Telerik.WinControls.UI.RadWaitingBar();
		this.lineRingWaitingBarIndicatorElement2 = new Telerik.WinControls.UI.LineRingWaitingBarIndicatorElement();
		this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
		this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
		this.txtCodResumen = new Telerik.WinControls.UI.RadTextBox();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.btnGenerar = new System.Windows.Forms.Button();
		this.btnBuscar = new System.Windows.Forms.Button();
		this.dtpFecha = new Telerik.WinControls.UI.RadDateTimePicker();
		this.btnSalir = new System.Windows.Forms.Button();
		this.Enviar = new Telerik.WinControls.UI.RadPageViewPage();
		this.dgResumen = new Telerik.WinControls.UI.RadGridView();
		this.radWaitingBar1 = new Telerik.WinControls.UI.RadWaitingBar();
		this.lineRingWaitingBarIndicatorElement1 = new Telerik.WinControls.UI.LineRingWaitingBarIndicatorElement();
		this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
		this.button2 = new System.Windows.Forms.Button();
		this.btnConsultarTicket = new System.Windows.Forms.Button();
		this.radToggleSwitch1 = new Telerik.WinControls.UI.RadToggleSwitch();
		this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
		this.dtpHasta = new Telerik.WinControls.UI.RadDateTimePicker();
		this.cmbAlmacenes2 = new Telerik.WinControls.UI.RadDropDownList();
		this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
		this.btnEnviarResumen = new System.Windows.Forms.Button();
		this.dtpDesde = new Telerik.WinControls.UI.RadDateTimePicker();
		this.lblTotalDocumentos = new Telerik.WinControls.UI.RadLabel();
		this.button3 = new System.Windows.Forms.Button();
		this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.Generar).BeginInit();
		this.Generar.SuspendLayout();
		this.radPageViewPage1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgVentas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgVentas.MasterTemplate).BeginInit();
		this.dgVentas.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radWaitingBar2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox1).BeginInit();
		this.radGroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCodResumen).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFecha).BeginInit();
		this.Enviar.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgResumen).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgResumen.MasterTemplate).BeginInit();
		this.dgResumen.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radWaitingBar1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox2).BeginInit();
		this.radGroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radToggleSwitch1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHasta).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDesde).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblTotalDocumentos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.Generar.Controls.Add(this.radPageViewPage1);
		this.Generar.Controls.Add(this.Enviar);
		this.Generar.Location = new System.Drawing.Point(12, 12);
		this.Generar.Name = "Generar";
		this.Generar.SelectedPage = this.Enviar;
		this.Generar.Size = new System.Drawing.Size(1285, 598);
		this.Generar.TabIndex = 6;
		this.Generar.ThemeName = "Fluent";
		this.Generar.ViewMode = Telerik.WinControls.UI.PageViewMode.NavigationView;
		this.radPageViewPage1.Controls.Add(this.dgVentas);
		this.radPageViewPage1.Controls.Add(this.radGroupBox1);
		this.radPageViewPage1.Controls.Add(this.btnSalir);
		this.radPageViewPage1.Image = SIGEFA.Properties.Resources.xml2;
		this.radPageViewPage1.ItemSize = new System.Drawing.SizeF(48f, 48f);
		this.radPageViewPage1.Location = new System.Drawing.Point(49, 37);
		this.radPageViewPage1.Name = "radPageViewPage1";
		this.radPageViewPage1.Size = new System.Drawing.Size(1235, 560);
		this.radPageViewPage1.Text = "Generar Resumen";
		this.dgVentas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgVentas.Controls.Add(this.radWaitingBar2);
		this.dgVentas.Location = new System.Drawing.Point(5, 109);
		this.dgVentas.MasterTemplate.AllowAddNewRow = false;
		this.dgVentas.MasterTemplate.AllowSearchRow = true;
		this.dgVentas.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.HeaderText = "COD_FACTURA_VENTA";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codFacturaVenta";
		gridViewTextBoxColumn2.HeaderText = "COD_TIPO_DOC";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "codTipoDocumento";
		gridViewTextBoxColumn3.HeaderText = "SERIE";
		gridViewTextBoxColumn3.Name = "serie";
		gridViewTextBoxColumn3.Width = 108;
		gridViewTextBoxColumn4.HeaderText = "NUMERO DOCUMENTO";
		gridViewTextBoxColumn4.Name = "NumDocumento";
		gridViewTextBoxColumn4.Width = 108;
		gridViewTextBoxColumn5.HeaderText = "COD_MONEDA";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "codMoneda";
		gridViewTextBoxColumn6.HeaderText = "MONEDA";
		gridViewTextBoxColumn6.Name = "Moneda";
		gridViewTextBoxColumn6.Width = 108;
		gridViewTextBoxColumn7.HeaderText = "TOTAL";
		gridViewTextBoxColumn7.Name = "Total";
		gridViewTextBoxColumn7.Width = 108;
		gridViewTextBoxColumn8.HeaderText = "MONTO DESCUENTO";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "Descuento";
		gridViewTextBoxColumn9.HeaderText = "IGV";
		gridViewTextBoxColumn9.Name = "Igv";
		gridViewTextBoxColumn9.Width = 108;
		gridViewTextBoxColumn10.HeaderText = "GRAVADAS";
		gridViewTextBoxColumn10.Name = "Gravadas";
		gridViewTextBoxColumn10.Width = 108;
		gridViewTextBoxColumn11.HeaderText = "EXONERADAS";
		gridViewTextBoxColumn11.Name = "Exoneradas";
		gridViewTextBoxColumn11.Width = 108;
		gridViewTextBoxColumn12.HeaderText = "INAFECTAS";
		gridViewTextBoxColumn12.Name = "Inafectas";
		gridViewTextBoxColumn12.Width = 108;
		gridViewTextBoxColumn13.HeaderText = "GRATUITAS";
		gridViewTextBoxColumn13.Name = "Gratuitas";
		gridViewTextBoxColumn13.Width = 109;
		gridViewTextBoxColumn14.HeaderText = "TIPO VENTA";
		gridViewTextBoxColumn14.IsVisible = false;
		gridViewTextBoxColumn14.Name = "TipoVenta";
		gridViewTextBoxColumn15.HeaderText = "DOCUENTO CLIENTE";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "Documento_Cliente";
		this.dgVentas.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15);
		this.dgVentas.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.dgVentas.Name = "dgVentas";
		this.dgVentas.ReadOnly = true;
		this.dgVentas.Size = new System.Drawing.Size(1003, 406);
		this.dgVentas.TabIndex = 2;
		this.dgVentas.ThemeName = "Fluent";
		this.radWaitingBar2.AssociatedControl = this.dgVentas;
		this.radWaitingBar2.Location = new System.Drawing.Point(571, 126);
		this.radWaitingBar2.Name = "radWaitingBar2";
		this.radWaitingBar2.Size = new System.Drawing.Size(70, 70);
		this.radWaitingBar2.TabIndex = 2;
		this.radWaitingBar2.Text = "radWaitingBar2";
		this.radWaitingBar2.ThemeName = "Fluent";
		this.radWaitingBar2.WaitingIndicators.Add(this.lineRingWaitingBarIndicatorElement2);
		this.radWaitingBar2.WaitingIndicatorSize = new System.Drawing.Size(100, 14);
		this.radWaitingBar2.WaitingSpeed = 50;
		this.radWaitingBar2.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.LineRing;
		this.lineRingWaitingBarIndicatorElement2.Name = "lineRingWaitingBarIndicatorElement2";
		this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.radGroupBox1.Controls.Add(this.radLabel3);
		this.radGroupBox1.Controls.Add(this.txtCodResumen);
		this.radGroupBox1.Controls.Add(this.cmbAlmacenes);
		this.radGroupBox1.Controls.Add(this.radLabel2);
		this.radGroupBox1.Controls.Add(this.radLabel1);
		this.radGroupBox1.Controls.Add(this.btnGenerar);
		this.radGroupBox1.Controls.Add(this.btnBuscar);
		this.radGroupBox1.Controls.Add(this.dtpFecha);
		this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(1);
		this.radGroupBox1.HeaderText = "Filtro";
		this.radGroupBox1.Location = new System.Drawing.Point(0, 0);
		this.radGroupBox1.Name = "radGroupBox1";
		this.radGroupBox1.Size = new System.Drawing.Size(1235, 87);
		this.radGroupBox1.TabIndex = 1;
		this.radGroupBox1.Text = "Filtro";
		this.radGroupBox1.ThemeName = "Fluent";
		this.radLabel3.Location = new System.Drawing.Point(5, 43);
		this.radLabel3.Name = "radLabel3";
		this.radLabel3.Size = new System.Drawing.Size(29, 18);
		this.radLabel3.TabIndex = 7;
		this.radLabel3.Text = "Cód.";
		this.radLabel3.ThemeName = "Fluent";
		this.txtCodResumen.Enabled = false;
		this.txtCodResumen.Location = new System.Drawing.Point(40, 37);
		this.txtCodResumen.Name = "txtCodResumen";
		this.txtCodResumen.Size = new System.Drawing.Size(85, 24);
		this.txtCodResumen.TabIndex = 6;
		this.txtCodResumen.ThemeName = "Fluent";
		this.cmbAlmacenes.Location = new System.Drawing.Point(445, 38);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(255, 24);
		this.cmbAlmacenes.TabIndex = 5;
		this.cmbAlmacenes.ThemeName = "Fluent";
		this.radLabel2.Location = new System.Drawing.Point(385, 40);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(52, 18);
		this.radLabel2.TabIndex = 4;
		this.radLabel2.Text = "Almacen:";
		this.radLabel2.ThemeName = "Fluent";
		this.radLabel1.Location = new System.Drawing.Point(131, 43);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(37, 18);
		this.radLabel1.TabIndex = 3;
		this.radLabel1.Text = "Fecha:";
		this.radLabel1.ThemeName = "Fluent";
		this.btnGenerar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerar.Image = SIGEFA.Properties.Resources.xml;
		this.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerar.Location = new System.Drawing.Point(853, 30);
		this.btnGenerar.Name = "btnGenerar";
		this.btnGenerar.Size = new System.Drawing.Size(139, 32);
		this.btnGenerar.TabIndex = 2;
		this.btnGenerar.Text = "Generar resumen";
		this.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerar.UseVisualStyleBackColor = true;
		this.btnGenerar.Click += new System.EventHandler(btnGenerar_Click);
		this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBuscar.Image = SIGEFA.Properties.Resources.searcg;
		this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBuscar.Location = new System.Drawing.Point(722, 30);
		this.btnBuscar.Name = "btnBuscar";
		this.btnBuscar.Size = new System.Drawing.Size(120, 32);
		this.btnBuscar.TabIndex = 1;
		this.btnBuscar.Text = "Buscar ventas";
		this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnBuscar.UseVisualStyleBackColor = true;
		this.btnBuscar.Click += new System.EventHandler(btnBuscar_Click);
		this.dtpFecha.CalendarSize = new System.Drawing.Size(290, 320);
		this.dtpFecha.Location = new System.Drawing.Point(177, 39);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(199, 24);
		this.dtpFecha.TabIndex = 0;
		this.dtpFecha.TabStop = false;
		this.dtpFecha.Text = "miércoles, 7 de octubre de 2020";
		this.dtpFecha.ThemeName = "Fluent";
		this.dtpFecha.Value = new System.DateTime(2020, 10, 7, 19, 36, 50, 402);
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Image = SIGEFA.Properties.Resources.x_button;
		this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSalir.Location = new System.Drawing.Point(1144, 521);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(88, 32);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "Salir";
		this.btnSalir.UseVisualStyleBackColor = true;
		this.Enviar.Controls.Add(this.dgResumen);
		this.Enviar.Controls.Add(this.radGroupBox2);
		this.Enviar.Controls.Add(this.lblTotalDocumentos);
		this.Enviar.Controls.Add(this.button3);
		this.Enviar.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.Enviar.ItemSize = new System.Drawing.SizeF(48f, 48f);
		this.Enviar.Location = new System.Drawing.Point(49, 37);
		this.Enviar.Name = "Enviar";
		this.Enviar.Size = new System.Drawing.Size(1235, 560);
		this.Enviar.Text = "Enviar Resumen";
		this.dgResumen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgResumen.Controls.Add(this.radWaitingBar1);
		this.dgResumen.Location = new System.Drawing.Point(2, 122);
		this.dgResumen.MasterTemplate.AllowAddNewRow = false;
		this.dgResumen.MasterTemplate.AllowSearchRow = true;
		this.dgResumen.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn16.FieldName = "Repoid";
		gridViewTextBoxColumn16.HeaderText = "ID";
		gridViewTextBoxColumn16.IsVisible = false;
		gridViewTextBoxColumn16.Name = "Repoid";
		gridViewTextBoxColumn17.FieldName = "Tipodoc";
		gridViewTextBoxColumn17.HeaderText = "T. DOC";
		gridViewTextBoxColumn17.IsVisible = false;
		gridViewTextBoxColumn17.Name = "Tipodoc";
		gridViewTextBoxColumn18.FieldName = "Fechaemision";
		gridViewTextBoxColumn18.HeaderText = "F. EMISION";
		gridViewTextBoxColumn18.Name = "Fechaemision";
		gridViewTextBoxColumn18.Width = 163;
		gridViewTextBoxColumn19.FieldName = "Serie";
		gridViewTextBoxColumn19.HeaderText = "SERIE";
		gridViewTextBoxColumn19.IsVisible = false;
		gridViewTextBoxColumn19.Name = "Serie";
		gridViewTextBoxColumn19.Width = 271;
		gridViewTextBoxColumn20.FieldName = "Correlativo";
		gridViewTextBoxColumn20.HeaderText = "CORRELATIVO";
		gridViewTextBoxColumn20.Name = "Correlativo";
		gridViewTextBoxColumn20.Width = 94;
		gridViewTextBoxColumn21.FieldName = "Monto";
		gridViewTextBoxColumn21.HeaderText = "MONTO";
		gridViewTextBoxColumn21.IsVisible = false;
		gridViewTextBoxColumn21.Name = "Monto";
		gridViewTextBoxColumn21.Width = 358;
		gridViewTextBoxColumn22.FieldName = "Estadosunat";
		gridViewTextBoxColumn22.HeaderText = "ESTADO SUNAT";
		gridViewTextBoxColumn22.Name = "Estadosunat";
		gridViewTextBoxColumn22.Width = 234;
		gridViewTextBoxColumn23.FieldName = "Mensajesunat";
		gridViewTextBoxColumn23.HeaderText = "MENSAJE SUNAT";
		gridViewTextBoxColumn23.Name = "Mensajesunat";
		gridViewTextBoxColumn23.Width = 190;
		gridViewTextBoxColumn24.FieldName = "Nombredoc";
		gridViewTextBoxColumn24.HeaderText = "NOMBRE DOCUMENTO";
		gridViewTextBoxColumn24.Name = "Nombredoc";
		gridViewTextBoxColumn24.Width = 142;
		gridViewTextBoxColumn25.FieldName = "ticket";
		gridViewTextBoxColumn25.HeaderText = "TICKET";
		gridViewTextBoxColumn25.Name = "ticket";
		gridViewTextBoxColumn25.Width = 289;
		this.dgResumen.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20, gridViewTextBoxColumn21, gridViewTextBoxColumn22, gridViewTextBoxColumn23, gridViewTextBoxColumn24, gridViewTextBoxColumn25);
		this.dgResumen.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.dgResumen.Name = "dgResumen";
		this.dgResumen.ReadOnly = true;
		this.dgResumen.Size = new System.Drawing.Size(1142, 397);
		this.dgResumen.TabIndex = 5;
		this.dgResumen.ThemeName = "Fluent";
		this.dgResumen.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(dgResumen_CellClick);
		this.radWaitingBar1.BackColor = System.Drawing.Color.Transparent;
		this.radWaitingBar1.Location = new System.Drawing.Point(433, 86);
		this.radWaitingBar1.Name = "radWaitingBar1";
		this.radWaitingBar1.Size = new System.Drawing.Size(122, 108);
		this.radWaitingBar1.TabIndex = 2;
		this.radWaitingBar1.Text = "radWaitingBar1";
		this.radWaitingBar1.ThemeName = "ControlDefault";
		this.radWaitingBar1.WaitingIndicators.Add(this.lineRingWaitingBarIndicatorElement1);
		this.radWaitingBar1.WaitingSpeed = 50;
		this.radWaitingBar1.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.LineRing;
		this.lineRingWaitingBarIndicatorElement1.ElementColor = System.Drawing.Color.FromArgb(13, 111, 126);
		this.lineRingWaitingBarIndicatorElement1.ElementColor2 = System.Drawing.Color.FromArgb(35, 155, 161);
		this.lineRingWaitingBarIndicatorElement1.ElementColor3 = System.Drawing.Color.FromArgb(116, 192, 227);
		this.lineRingWaitingBarIndicatorElement1.GradientStyle = Telerik.WinControls.GradientStyles.Glass;
		this.lineRingWaitingBarIndicatorElement1.Name = "lineRingWaitingBarIndicatorElement1";
		this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.radGroupBox2.Controls.Add(this.button2);
		this.radGroupBox2.Controls.Add(this.btnConsultarTicket);
		this.radGroupBox2.Controls.Add(this.radToggleSwitch1);
		this.radGroupBox2.Controls.Add(this.radLabel4);
		this.radGroupBox2.Controls.Add(this.dtpHasta);
		this.radGroupBox2.Controls.Add(this.cmbAlmacenes2);
		this.radGroupBox2.Controls.Add(this.radLabel5);
		this.radGroupBox2.Controls.Add(this.radLabel6);
		this.radGroupBox2.Controls.Add(this.btnEnviarResumen);
		this.radGroupBox2.Controls.Add(this.dtpDesde);
		this.radGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(1);
		this.radGroupBox2.HeaderText = "Filtro";
		this.radGroupBox2.Location = new System.Drawing.Point(0, 0);
		this.radGroupBox2.Name = "radGroupBox2";
		this.radGroupBox2.Size = new System.Drawing.Size(1235, 116);
		this.radGroupBox2.TabIndex = 4;
		this.radGroupBox2.Text = "Filtro";
		this.radGroupBox2.ThemeName = "Fluent";
		this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button2.Image = SIGEFA.Properties.Resources.searcg;
		this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button2.Location = new System.Drawing.Point(293, 70);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(127, 32);
		this.button2.TabIndex = 1;
		this.button2.Text = "Buscar resumen";
		this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.btnConsultarTicket.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnConsultarTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultarTicket.Image = SIGEFA.Properties.Resources.search;
		this.btnConsultarTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnConsultarTicket.Location = new System.Drawing.Point(590, 70);
		this.btnConsultarTicket.Name = "btnConsultarTicket";
		this.btnConsultarTicket.Size = new System.Drawing.Size(157, 32);
		this.btnConsultarTicket.TabIndex = 9;
		this.btnConsultarTicket.Text = "Consultar resumen";
		this.btnConsultarTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnConsultarTicket.UseVisualStyleBackColor = true;
		this.btnConsultarTicket.Click += new System.EventHandler(button4_Click);
		this.radToggleSwitch1.Location = new System.Drawing.Point(948, 40);
		this.radToggleSwitch1.Name = "radToggleSwitch1";
		this.radToggleSwitch1.OffText = "PENDIENTES";
		this.radToggleSwitch1.OnText = "ENVIADOS";
		this.radToggleSwitch1.Size = new System.Drawing.Size(128, 20);
		this.radToggleSwitch1.TabIndex = 8;
		this.radToggleSwitch1.ThemeName = "ControlDefault";
		((Telerik.WinControls.UI.RadToggleSwitchElement)this.radToggleSwitch1.GetChildAt(0)).ThumbOffset = 108;
		((Telerik.WinControls.UI.ToggleSwitchPartElement)this.radToggleSwitch1.GetChildAt(0).GetChildAt(1)).Text = "PENDIENTES";
		((Telerik.WinControls.UI.ToggleSwitchPartElement)this.radToggleSwitch1.GetChildAt(0).GetChildAt(1)).ForeColor = System.Drawing.Color.FromArgb(0, 236, 73, 73);
		this.radLabel4.Location = new System.Drawing.Point(293, 40);
		this.radLabel4.Name = "radLabel4";
		this.radLabel4.Size = new System.Drawing.Size(37, 18);
		this.radLabel4.TabIndex = 7;
		this.radLabel4.Text = "Hasta:";
		this.radLabel4.ThemeName = "Fluent";
		this.dtpHasta.CalendarSize = new System.Drawing.Size(290, 320);
		this.dtpHasta.Location = new System.Drawing.Point(339, 36);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(199, 24);
		this.dtpHasta.TabIndex = 6;
		this.dtpHasta.TabStop = false;
		this.dtpHasta.Text = "miércoles, 7 de octubre de 2020";
		this.dtpHasta.ThemeName = "Fluent";
		this.dtpHasta.Value = new System.DateTime(2020, 10, 7, 19, 36, 50, 402);
		this.cmbAlmacenes2.Location = new System.Drawing.Point(651, 38);
		this.cmbAlmacenes2.Name = "cmbAlmacenes2";
		this.cmbAlmacenes2.Size = new System.Drawing.Size(255, 24);
		this.cmbAlmacenes2.TabIndex = 5;
		this.cmbAlmacenes2.ThemeName = "Fluent";
		this.radLabel5.Location = new System.Drawing.Point(591, 40);
		this.radLabel5.Name = "radLabel5";
		this.radLabel5.Size = new System.Drawing.Size(52, 18);
		this.radLabel5.TabIndex = 4;
		this.radLabel5.Text = "Almacen:";
		this.radLabel5.ThemeName = "Fluent";
		this.radLabel6.Location = new System.Drawing.Point(16, 40);
		this.radLabel6.Name = "radLabel6";
		this.radLabel6.Size = new System.Drawing.Size(40, 18);
		this.radLabel6.TabIndex = 3;
		this.radLabel6.Text = "Desde:";
		this.radLabel6.ThemeName = "Fluent";
		this.btnEnviarResumen.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnEnviarResumen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnEnviarResumen.Image = SIGEFA.Properties.Resources.xml;
		this.btnEnviarResumen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEnviarResumen.Location = new System.Drawing.Point(435, 70);
		this.btnEnviarResumen.Name = "btnEnviarResumen";
		this.btnEnviarResumen.Size = new System.Drawing.Size(139, 32);
		this.btnEnviarResumen.TabIndex = 2;
		this.btnEnviarResumen.Text = "Enviar resumen";
		this.btnEnviarResumen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnEnviarResumen.UseVisualStyleBackColor = true;
		this.btnEnviarResumen.Click += new System.EventHandler(btnEnviarResumen_Click);
		this.dtpDesde.CalendarSize = new System.Drawing.Size(290, 320);
		this.dtpDesde.Location = new System.Drawing.Point(62, 36);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(199, 24);
		this.dtpDesde.TabIndex = 0;
		this.dtpDesde.TabStop = false;
		this.dtpDesde.Text = "miércoles, 7 de octubre de 2020";
		this.dtpDesde.ThemeName = "Fluent";
		this.dtpDesde.Value = new System.DateTime(2020, 10, 7, 19, 36, 50, 402);
		this.lblTotalDocumentos.Location = new System.Drawing.Point(184, 539);
		this.lblTotalDocumentos.Name = "lblTotalDocumentos";
		this.lblTotalDocumentos.Size = new System.Drawing.Size(31, 18);
		this.lblTotalDocumentos.TabIndex = 5;
		this.lblTotalDocumentos.Text = "Total";
		this.lblTotalDocumentos.ThemeName = "Fluent";
		this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.Image = SIGEFA.Properties.Resources.x_button;
		this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.button3.Location = new System.Drawing.Point(1141, 525);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(88, 32);
		this.button3.TabIndex = 6;
		this.button3.Text = "Salir";
		this.button3.UseVisualStyleBackColor = true;
		this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
		this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(1293, 622);
		base.Controls.Add(this.Generar);
		base.Name = "frmResumenDiario";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Resumen diario";
		base.ThemeName = "Fluent";
		base.Load += new System.EventHandler(frmResumenDiario_Load);
		((System.ComponentModel.ISupportInitialize)this.Generar).EndInit();
		this.Generar.ResumeLayout(false);
		this.radPageViewPage1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgVentas.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgVentas).EndInit();
		this.dgVentas.ResumeLayout(false);
		this.dgVentas.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radWaitingBar2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox1).EndInit();
		this.radGroupBox1.ResumeLayout(false);
		this.radGroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCodResumen).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFecha).EndInit();
		this.Enviar.ResumeLayout(false);
		this.Enviar.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgResumen.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgResumen).EndInit();
		this.dgResumen.ResumeLayout(false);
		this.dgResumen.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radWaitingBar1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox2).EndInit();
		this.radGroupBox2.ResumeLayout(false);
		this.radGroupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radToggleSwitch1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpHasta).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDesde).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblTotalDocumentos).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
