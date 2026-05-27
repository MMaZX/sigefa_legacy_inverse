using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.SunatFacElec;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Data;
using WinApp.API;
using WinApp.Comun.Dto.Intercambio;
using WinApp.Firmado;

namespace SIGEFA.Formularios;

public class frmEnvioSunat : Office2007Form
{
	private string estado = "-1";

	private clsAdmRepositorio clsadmrepo = new clsAdmRepositorio();

	private List<clsRepositorio> lista_repositorio = null;

	private clsEmpresa empresa = null;

	private clsAdmEmpresa admemp = new clsAdmEmpresa();

	private clsAdmAlmacen admalma = new clsAdmAlmacen();

	private clsFacturaVenta fact = new clsFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private List<clsDetalleFacturaVenta> ListaDetalleVenta = new List<clsDetalleFacturaVenta>();

	private clsCliente client = new clsCliente();

	private clsAdmCliente admclient = new clsAdmCliente();

	private DataTable detalleTableVenta = new DataTable();

	private Facturacion con = new Facturacion();

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private GroupBox groupBox2;

	private DataGridViewX dg_documentos;

	private ButtonX btn_envio;

	private ButtonX btnSalir;

	private LabelX lblTotalDocumentos;

	private ComboBox cb_estado;

	private Label label2;

	private LabelX totaldocs;

	private Label label3;

	private Label label4;

	private RadDropDownList cmbAlmacenes;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private Label label5;

	private DataGridViewTextBoxColumn Repoid;

	private DataGridViewTextBoxColumn Tipodoc;

	private DataGridViewTextBoxColumn Fechaemision;

	private DataGridViewTextBoxColumn Serie;

	private DataGridViewTextBoxColumn Correlativo;

	private DataGridViewTextBoxColumn Monto;

	private DataGridViewTextBoxColumn Estadosunat;

	private DataGridViewTextBoxColumn Mensajesunat;

	private DataGridViewTextBoxColumn Nombredoc;

	private DataGridViewTextBoxColumn codempresa;

	private DataGridViewTextBoxColumn fecha_envio;

	public DateTimePicker dtpDesde;

	public DateTimePicker dtpHasta;

	public frmEnvioSunat()
	{
		InitializeComponent();
	}

	public void listar_repositorio()
	{
		try
		{
			lista_repositorio = new List<clsRepositorio>();
			lista_repositorio = clsadmrepo.listar_repositorio(estado, frmLogin.iCodSucursal, Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value);
			if (lista_repositorio != null)
			{
				if (lista_repositorio.Count > 0)
				{
					dg_documentos.Rows.Clear();
					foreach (clsRepositorio rep in lista_repositorio)
					{
						dg_documentos.Rows.Add(rep.Repoid, rep.Tipodoc, rep.Fechaemision, rep.Serie, rep.Correlativo, rep.Monto, rep.Estadosunat, rep.Mensajesunat, rep.Nombredoc, rep.Usuario, "", rep.CodEmpresa, rep.FechaActualiza);
					}
				}
				totaldocs.Text = lista_repositorio.Count.ToString();
			}
			else
			{
				dg_documentos.Rows.Clear();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public async void Envio()
	{
		try
		{
			string tipodocumento = "";
			string _iddocumento = "";
			bool todocorrecto = false;
			if (lista_repositorio == null || lista_repositorio.Count <= 0)
			{
				return;
			}
			foreach (clsRepositorio r in lista_repositorio)
			{
				empresa = admemp.CargaEmpresa3(r.CodEmpresa);
				string rutacertificado = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\" + empresa.Certificado;
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
				}
				EnviarDocumentoResponse rpta;
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
						MessageBox.Show(respuestaFirmado.MensajeError);
						return;
					}
					await con.Enviar(empresa, _iddocumento, tipodocumento, respuestaFirmado.TramaXmlFirmado);
					rpta = con.rpta;
				}
				else
				{
					fact = AdmVenta.CargaFacturaVenta(r.CodFacturaVenta);
					client = admclient.MuestraCliente(fact.CodCliente);
					CargaDetalle();
					RecorreDetalle();
					await con.GeneraDocumentoEnvio(client, fact, ListaDetalleVenta);
					string sinfirma = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", r.Nombredoc + ".xml");
					string tramaXmlSinFirma2 = Convert.ToBase64String(File.ReadAllBytes(sinfirma));
					new FirmadoRequest
					{
						TramaXmlSinFirma = tramaXmlSinFirma2,
						CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutacertificado)),
						PasswordCertificado = empresa.Contrasena,
						UnSoloNodoExtension = true
					};
					await con.Enviar(empresa, _iddocumento, tipodocumento, tramaXmlSinFirma2);
					rpta = con.rpta;
				}
				if (rpta == null)
				{
					continue;
				}
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
				if (rpta.MensajeError != null)
				{
					if (rpta.CodigoRespuesta == "1033" || rpta.MensajeError.Contains("1033"))
					{
						r.Estadosunat = "0";
						clsadmrepo.actualiza_repositorio(r);
					}
					else
					{
						r.Estadosunat = "-1";
						clsadmrepo.actualiza_repositorio(r);
					}
				}
			}
			if (todocorrecto)
			{
				MessageBox.Show("Los documentos fueron enviados de forma correcta");
				listar_repositorio();
			}
			else
			{
				MessageBox.Show("No todos los documentos fueron enviados de forma correcta");
				listar_repositorio();
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
			listar_repositorio();
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	private void btn_envio_Click(object sender, EventArgs e)
	{
		btn_envio.Enabled = false;
		estado = "-1";
		listar_repositorio();
		Envio();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmEnvioSunat_Load(object sender, EventArgs e)
	{
		cargaAlmacenes();
		cb_estado.SelectedIndex = 0;
	}

	private void cargaAlmacenes()
	{
		DataTable aux = admalma.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		aux.Rows.RemoveAt(0);
		cmbAlmacenes.ValueMember = "cod";
		cmbAlmacenes.DisplayMember = "nombre";
		cmbAlmacenes.DataSource = aux;
		cmbAlmacenes.SelectedValue = frmLogin.iCodAlmacen;
		cmbAlmacenes.SelectedIndex = 0;
	}

	private void cb_estado_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cb_estado.SelectedIndex == 0)
		{
			btn_envio.Enabled = true;
			estado = "-1";
			listar_repositorio();
		}
		else
		{
			btn_envio.Enabled = false;
			estado = "0";
			listar_repositorio_Enviados();
		}
	}

	private void listar_repositorio_Enviados()
	{
		try
		{
			lista_repositorio = new List<clsRepositorio>();
			lista_repositorio = clsadmrepo.listar_repositorio_Enviados(estado, frmLogin.iCodSucursal, Convert.ToInt32(cmbAlmacenes.SelectedValue), dtpDesde.Value, dtpHasta.Value);
			if (lista_repositorio != null)
			{
				if (lista_repositorio.Count > 0)
				{
					dg_documentos.Rows.Clear();
					foreach (clsRepositorio rep in lista_repositorio)
					{
						dg_documentos.Rows.Add(rep.Repoid, rep.Tipodoc, rep.Fechaemision, rep.Serie, rep.Correlativo, rep.Monto, rep.Estadosunat, rep.Mensajesunat, rep.Nombredoc, rep.Usuario, rep.FechaActualiza, 0, rep.FechaActualiza);
					}
				}
				totaldocs.Text = lista_repositorio.Count.ToString();
			}
			else
			{
				dg_documentos.Rows.Clear();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void frmEnvioSunat_Shown(object sender, EventArgs e)
	{
		if (lista_repositorio != null && lista_repositorio.Count <= 0)
		{
		}
	}

	private void CargaDetalle()
	{
		detalleTableVenta = AdmVenta.CargaDetalle(Convert.ToInt32(fact.CodFacturaVenta), frmLogin.iCodAlmacen, 0);
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
		deta.CodVenta = Convert.ToInt32(fact.CodFacturaVenta);
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
		deta.Tipoimpuesto = "10";
		deta.Entregado = true;
		deta.TipoUnidad = 1;
		deta.CodDetalleCotizacion = 0;
		deta.CodDetallePedido = Convert.ToInt32(fila["codDetalle"]);
		ListaDetalleVenta.Add(deta);
	}

	private void cmbAlmacenes_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
	{
		if (cb_estado.SelectedIndex == 0)
		{
			btn_envio.Enabled = true;
			estado = "-1";
			listar_repositorio();
		}
		else
		{
			btn_envio.Enabled = false;
			estado = "0";
			listar_repositorio_Enviados();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmEnvioSunat));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.label4 = new System.Windows.Forms.Label();
		this.cmbAlmacenes = new Telerik.WinControls.UI.RadDropDownList();
		this.label3 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.cb_estado = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dg_documentos = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.Repoid = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tipodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Correlativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Estadosunat = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Mensajesunat = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Nombredoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codempresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha_envio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.lblTotalDocumentos = new DevComponents.DotNetBar.LabelX();
		this.totaldocs = new DevComponents.DotNetBar.LabelX();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.btnSalir = new DevComponents.DotNetBar.ButtonX();
		this.btn_envio = new DevComponents.DotNetBar.ButtonX();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).BeginInit();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dg_documentos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.BackColor = System.Drawing.Color.FloralWhite;
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dtpHasta);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cmbAlmacenes);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.dtpDesde);
		this.groupBox1.Controls.Add(this.cb_estado);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1123, 74);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Búsqueda";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label5.Location = new System.Drawing.Point(9, 46);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(64, 17);
		this.label5.TabIndex = 147;
		this.label5.Text = "HASTA:";
		this.dtpHasta.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(86, 45);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(103, 20);
		this.dtpHasta.TabIndex = 146;
		this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label4.Location = new System.Drawing.Point(637, 36);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(90, 17);
		this.label4.TabIndex = 145;
		this.label4.Text = "ALMACEN: ";
		this.cmbAlmacenes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbAlmacenes.Location = new System.Drawing.Point(742, 34);
		this.cmbAlmacenes.Name = "cmbAlmacenes";
		this.cmbAlmacenes.Size = new System.Drawing.Size(125, 24);
		this.cmbAlmacenes.TabIndex = 144;
		this.cmbAlmacenes.ThemeName = "TelerikMetroBlue";
		this.cmbAlmacenes.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cmbAlmacenes_SelectedIndexChanged);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label3.Location = new System.Drawing.Point(9, 20);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(65, 17);
		this.label3.TabIndex = 143;
		this.label3.Text = "DESDE:";
		this.dtpDesde.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(86, 19);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(103, 20);
		this.dtpDesde.TabIndex = 142;
		this.cb_estado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cb_estado.FormattingEnabled = true;
		this.cb_estado.Items.AddRange(new object[2] { "NO ENVIADOS", "ENVIADOS" });
		this.cb_estado.Location = new System.Drawing.Point(986, 32);
		this.cb_estado.Name = "cb_estado";
		this.cb_estado.Size = new System.Drawing.Size(121, 21);
		this.cb_estado.TabIndex = 3;
		this.cb_estado.SelectedIndexChanged += new System.EventHandler(cb_estado_SelectedIndexChanged);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label2.Location = new System.Drawing.Point(901, 35);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(81, 17);
		this.label2.TabIndex = 2;
		this.label2.Text = "ESTADO: ";
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
		this.label1.Location = new System.Drawing.Point(335, 34);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(265, 17);
		this.label1.TabIndex = 0;
		this.label1.Text = "DOCUMENTOS A ENVIAR A SUNAT";
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.FloralWhite;
		this.groupBox2.Controls.Add(this.dg_documentos);
		this.groupBox2.Location = new System.Drawing.Point(12, 92);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1123, 305);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Boletas y Notas de Crédito / Débito asociadas";
		this.dg_documentos.AllowUserToAddRows = false;
		this.dg_documentos.AllowUserToDeleteRows = false;
		this.dg_documentos.AllowUserToOrderColumns = true;
		this.dg_documentos.AllowUserToResizeRows = false;
		this.dg_documentos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dg_documentos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		this.dg_documentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dg_documentos.Columns.AddRange(this.Repoid, this.Tipodoc, this.Fechaemision, this.Serie, this.Correlativo, this.Monto, this.Estadosunat, this.Mensajesunat, this.Nombredoc, this.codempresa, this.fecha_envio);
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dg_documentos.DefaultCellStyle = dataGridViewCellStyle9;
		this.dg_documentos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dg_documentos.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dg_documentos.Location = new System.Drawing.Point(3, 16);
		this.dg_documentos.MultiSelect = false;
		this.dg_documentos.Name = "dg_documentos";
		this.dg_documentos.ReadOnly = true;
		this.dg_documentos.RowHeadersVisible = false;
		this.dg_documentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dg_documentos.Size = new System.Drawing.Size(1117, 286);
		this.dg_documentos.TabIndex = 0;
		this.Repoid.DataPropertyName = "Repoid";
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Repoid.DefaultCellStyle = dataGridViewCellStyle10;
		this.Repoid.HeaderText = "ID";
		this.Repoid.Name = "Repoid";
		this.Repoid.ReadOnly = true;
		this.Repoid.Visible = false;
		this.Tipodoc.DataPropertyName = "Tipodoc";
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Tipodoc.DefaultCellStyle = dataGridViewCellStyle11;
		this.Tipodoc.HeaderText = "T. DOC";
		this.Tipodoc.Name = "Tipodoc";
		this.Tipodoc.ReadOnly = true;
		this.Tipodoc.Visible = false;
		this.Fechaemision.DataPropertyName = "Fechaemision";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Fechaemision.DefaultCellStyle = dataGridViewCellStyle12;
		this.Fechaemision.HeaderText = "F. EMISION";
		this.Fechaemision.Name = "Fechaemision";
		this.Fechaemision.ReadOnly = true;
		this.Serie.DataPropertyName = "Serie";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Serie.DefaultCellStyle = dataGridViewCellStyle13;
		this.Serie.HeaderText = "SERIE";
		this.Serie.Name = "Serie";
		this.Serie.ReadOnly = true;
		this.Correlativo.DataPropertyName = "Correlativo";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Correlativo.DefaultCellStyle = dataGridViewCellStyle14;
		this.Correlativo.HeaderText = "CORRELATIVO";
		this.Correlativo.Name = "Correlativo";
		this.Correlativo.ReadOnly = true;
		this.Monto.DataPropertyName = "Monto";
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Monto.DefaultCellStyle = dataGridViewCellStyle15;
		this.Monto.HeaderText = "MONTO";
		this.Monto.Name = "Monto";
		this.Monto.ReadOnly = true;
		this.Estadosunat.DataPropertyName = "Estadosunat";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Estadosunat.DefaultCellStyle = dataGridViewCellStyle16;
		this.Estadosunat.HeaderText = "EST. SUNAT";
		this.Estadosunat.Name = "Estadosunat";
		this.Estadosunat.ReadOnly = true;
		this.Mensajesunat.DataPropertyName = "Mensajesunat";
		this.Mensajesunat.HeaderText = "MENSAJE SUNAT";
		this.Mensajesunat.Name = "Mensajesunat";
		this.Mensajesunat.ReadOnly = true;
		this.Nombredoc.DataPropertyName = "Nombredoc";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Nombredoc.DefaultCellStyle = dataGridViewCellStyle17;
		this.Nombredoc.HeaderText = "NOMBRE DOCUMENTO";
		this.Nombredoc.Name = "Nombredoc";
		this.Nombredoc.ReadOnly = true;
		this.codempresa.DataPropertyName = "codempresa";
		this.codempresa.HeaderText = "CODEMPRESA";
		this.codempresa.Name = "codempresa";
		this.codempresa.ReadOnly = true;
		this.codempresa.Visible = false;
		this.fecha_envio.DataPropertyName = "fecha_envio";
		this.fecha_envio.HeaderText = "FECHA ENVIO";
		this.fecha_envio.Name = "fecha_envio";
		this.fecha_envio.ReadOnly = true;
		this.lblTotalDocumentos.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblTotalDocumentos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblTotalDocumentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTotalDocumentos.Location = new System.Drawing.Point(411, 417);
		this.lblTotalDocumentos.Name = "lblTotalDocumentos";
		this.lblTotalDocumentos.Size = new System.Drawing.Size(132, 23);
		this.lblTotalDocumentos.TabIndex = 4;
		this.lblTotalDocumentos.Text = "Total Documentos: ";
		this.lblTotalDocumentos.TextAlignment = System.Drawing.StringAlignment.Center;
		this.totaldocs.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.totaldocs.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.totaldocs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.totaldocs.Location = new System.Drawing.Point(549, 417);
		this.totaldocs.Name = "totaldocs";
		this.totaldocs.Size = new System.Drawing.Size(63, 23);
		this.totaldocs.TabIndex = 5;
		this.totaldocs.Text = ".";
		this.totaldocs.TextAlignment = System.Drawing.StringAlignment.Center;
		this.btnSalir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnSalir.ColorTable = DevComponents.DotNetBar.eButtonColor.Office2007WithBackground;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(12, 417);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(100, 23);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btn_envio.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btn_envio.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btn_envio.Enabled = false;
		this.btn_envio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btn_envio.Image = (System.Drawing.Image)resources.GetObject("btn_envio.Image");
		this.btn_envio.Location = new System.Drawing.Point(1026, 417);
		this.btn_envio.Name = "btn_envio";
		this.btn_envio.Size = new System.Drawing.Size(106, 23);
		this.btn_envio.TabIndex = 2;
		this.btn_envio.Text = "ENVIAR";
		this.btn_envio.Click += new System.EventHandler(btn_envio_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.AliceBlue;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1147, 452);
		base.Controls.Add(this.totaldocs);
		base.Controls.Add(this.lblTotalDocumentos);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btn_envio);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		this.EnableGlass = false;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
		base.HelpButton = true;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.Name = "frmEnvioSunat";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Envío de documentos";
		base.Load += new System.EventHandler(frmEnvioSunat_Load);
		base.Shown += new System.EventHandler(frmEnvioSunat_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbAlmacenes).EndInit();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dg_documentos).EndInit();
		base.ResumeLayout(false);
	}
}
