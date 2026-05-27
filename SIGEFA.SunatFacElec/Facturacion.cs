using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using QRCoder;
using RestSharp;
using RestSharp.Authenticators;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Formularios;
using SIGEFA.Librerias;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using WinApp.API;
using WinApp.Comun.Dto.Intercambio;
using WinApp.Comun.Dto.Modelos;
using WinApp.Estructuras.CommonAggregateComponents;
using WinApp.Estructuras.CommonBasicComponents;
using WinApp.Firmado;
using WinApp.Servicio;
using WinApp.Servicio.Soap;

namespace SIGEFA.SunatFacElec;

public class Facturacion
{
	private DocumentoElectronico _documento;

	private Contribuyente dtsReceptor;

	private FirmadoResponse respuestaFirmado;

	private GuiaRemision documento;

	public int enviado = 0;

	public RespuestaComunConArchivo respuestaEnvio;

	public EnviarDocumentoResponse rpta;

	public int VerificaContribuyente = 0;

	public string nombredocumento;

	private clsProducto productos = new clsProducto();

	private clsTipoDocumento tipodocumento = new clsTipoDocumento();

	private clsTransaccion transacciones = new clsTransaccion();

	private clsRepositorio repositorio = new clsRepositorio();

	private clsNotasCreditoDebitoVenta ds1 = new clsNotasCreditoDebitoVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private Conversion conv = new Conversion();

	private Discrepancia discrepancia = new Discrepancia();

	private DocumentoRelacionado dr = new DocumentoRelacionado();

	public clsEmpresa empresa = new clsEmpresa();

	private clsAdmEmpresa admemp = new clsAdmEmpresa();

	private clsGuiaFacturacion guia = new clsGuiaFacturacion();

	private List<clsRepositorio> lista_repositorio = null;

	private clsAdmFacturaVenta admfac = new clsAdmFacturaVenta();

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsAdmTransaccion admTransacciones = new clsAdmTransaccion();

	private clsAdmTipoDocumento admTipodocumento = new clsAdmTipoDocumento();

	private clsAdmProducto admProductos = new clsAdmProducto();

	private clsAdmEmpresa admEmpresa = new clsAdmEmpresa();

	private clsReporteFactura ds = new clsReporteFactura();

	private clsAdmRepositorio admRepositorio = new clsAdmRepositorio();

	private clsAdmRepositorio clsadmrepo = new clsAdmRepositorio();

	private clsAdmNotaCredito admnc = new clsAdmNotaCredito();

	private clsAdmGuiaFacturacion AdmGuia = new clsAdmGuiaFacturacion();

	public EnviarResumenResponse rptaresumen = null;

	private int formpagofact;

	public int CodigoErrorEnvio = 0;

	public string RutaArchivo { get; set; }

	public string IdDocumento { get; set; }

	public string RutaAlterna { get; set; }

	public byte[] LogoEmp { get; set; }

	public string datosAdicionales_CDB { get; set; }

	public string CodigoCertificado { get; set; }

	public string firmadig { get; set; }

	public string resumenfirmadig { get; set; }

	public Facturacion()
	{
		_documento = new DocumentoElectronico();
		respuestaFirmado = new FirmadoResponse();
	}

	private int DatosComtribuyente(int CodEmpresa)
	{
		try
		{
			empresa = admEmpresa.CargaEmpresa3(CodEmpresa);
			if (empresa != null)
			{
				Contribuyente dtsEmisor = new Contribuyente
				{
					NroDocumento = empresa.Ruc,
					TipoDocumento = "6",
					Direccion = empresa.Direccion,
					Departamento = "PIURA",
					Provincia = "TALARA",
					Distrito = "TALARA",
					NombreLegal = empresa.RazonSocial,
					NombreComercial = "",
					Ubigeo = "200101",
					CodDomicilioFiscal = "0000"
				};
				_documento.Emisor = dtsEmisor;
				return 1;
			}
			return 2;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return 2;
		}
	}

	private string extraeHash(string ruta)
	{
		string result = "";
		using (XmlReader reader = XmlReader.Create(ruta))
		{
			while (reader.Read())
			{
				if (reader.IsStartElement() && reader.ReadToDescendant("DigestValue"))
				{
					reader.Read();
					result = reader.Value;
					break;
				}
			}
		}
		return result;
	}

	public async Task GeneraDocumento(clsCliente cliente, clsFacturaVenta venta, List<clsDetalleFacturaVenta> detalleventa, int isVenta)
	{
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			empresa = admemp.CargaEmpresa3(venta.CodEmpresa);
			tipodocumento = admTipodocumento.CargaTipoDocumento(venta.CodTipoDocumento);
			transacciones = admTransacciones.MuestraTransaccion(venta.CodTipoTransaccion);
			formpagofact = venta.FormaPago;
			dtsReceptor = new Contribuyente
			{
				NroDocumento = cliente.RucDni,
				TipoDocumento = venta.DocumentoIdentidad.CodigoSunat.ToString(),
				NombreLegal = cliente.RazonSocial,
				NombreComercial = "",
				Direccion = cliente.DireccionLegal
			};
			_documento.TipoDocumento = tipodocumento.Tipodoccodsunat.ToString();
			_documento.Receptor = dtsReceptor;
			_documento.FechaEmision = venta.FechaSalida.ToShortDateString();
			_documento.IssueTime = $"{DateTime.Now.ToLongTimeString():HH:mm:ss}";
			_documento.TipoOperacion = ((venta.valorRetencion == 1) ? "2002" : transacciones.Codsunat);
			_documento.Glosa = venta.Comentario;
			if (venta.Moneda == 1)
			{
				_documento.Moneda = "PEN";
			}
			else
			{
				_documento.Moneda = "USD";
			}
			VerificaContribuyente = DatosComtribuyente(venta.CodEmpresa);
			if (VerificaContribuyente == 2)
			{
				MessageBox.Show("No se puede generar documento\n Falta cargar datos de la empresa");
				return;
			}
			switch (_documento.TipoDocumento)
			{
			case "03":
				_documento.IdDocumento = "B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				DatosFactura(cliente, venta, detalleventa);
				break;
			case "01":
				_documento.IdDocumento = "F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				DatosFactura(cliente, venta, detalleventa);
				break;
			}
			_documento.MontoEnLetras = conv.enletras(venta.Total.ToString());
			ISerializador serializador = new Serializador();
			new DocumentoResponse().Exito = false;
			DocumentoResponse response = await new GenerarFactura(serializador).Post(_documento);
			if (!response.Exito)
			{
				MessageBox.Show(response.MensajeError);
			}
			RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml");
			File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(response.TramaXmlSinFirma));
			new EscribirLog("Firmando XML ", mostrarConsola: true);
			await Firmar();
			new EscribirLog("XML Firmado ", mostrarConsola: true);
			string codigoHash = "";
			string rutadocumentosboletas = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutaxmlboletas = Program.CarpetaBoletas + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutadocumentosfacturas = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutaxmlfacturas = Program.CarpetaFacturas + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string tipoDocumento = _documento.TipoDocumento;
			string text = tipoDocumento;
			string text2 = text;
			if (!(text2 == "03"))
			{
				if (text2 == "01")
				{
					File.WriteAllBytes(rutaxmlfacturas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
					File.WriteAllBytes(rutadocumentosfacturas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
					codigoHash = extraeHash(rutadocumentosfacturas);
				}
			}
			else
			{
				File.WriteAllBytes(rutaxmlboletas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
				File.WriteAllBytes(rutadocumentosboletas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
				codigoHash = extraeHash(rutaxmlboletas);
			}
			resumenfirmadig = respuestaFirmado.ResumenFirma;
			firmadig = respuestaFirmado.ValorFirma;
			GeneraPDF(Convert.ToInt32(venta.CodFacturaVenta));
			repositorio.Tipodoc = venta.CodTipoDocumento;
			repositorio.Nombredoc = _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento;
			repositorio.Fechaemision = venta.FechaPago;
			repositorio.Serie = venta.Serie;
			repositorio.Correlativo = venta.NumDoc;
			repositorio.Monto = Convert.ToDecimal(venta.Total);
			repositorio.CodEmpresa = venta.CodEmpresa;
			repositorio.CodSucursal = venta.CodSucursal;
			repositorio.CodAlmacen = venta.CodAlmacen;
			repositorio.CodFacturaVenta = Convert.ToInt32(venta.CodFacturaVenta);
			repositorio.Estadosunat = "-1";
			repositorio.Mensajesunat = "No enviada";
			if (repositorio.Tipodoc == 2)
			{
				_ = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + repositorio.Nombredoc + ".xml";
			}
			else
			{
				_ = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + repositorio.Nombredoc + ".xml";
			}
			repositorio.Usuario = frmLogin.iCodUser;
			repositorio.CodigoHash = codigoHash;
			if (isVenta == 0)
			{
				new EscribirLog("Guardando documento en Repositorio ", mostrarConsola: true);
				if (!admRepositorio.registra_repositorio(repositorio))
				{
					MessageBox.Show("Documento no se pudo enviar al repositorio");
					new EscribirLog("Error al guardar repositorio", mostrarConsola: true);
				}
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	public async Task GeneraDocumentoEnvio(clsCliente cliente, clsFacturaVenta venta, List<clsDetalleFacturaVenta> detalleventa)
	{
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			tipodocumento = admTipodocumento.CargaTipoDocumento(venta.CodTipoDocumento);
			transacciones = admTransacciones.MuestraTransaccion(venta.CodTipoTransaccion);
			if (cliente.RucDni.Length == 8)
			{
				cliente.DocumentoIdentidad = new clsDocumentoIdentidad();
				cliente.DocumentoIdentidad.CodigoSunat = 3;
			}
			else
			{
				cliente.DocumentoIdentidad = new clsDocumentoIdentidad();
				cliente.DocumentoIdentidad.CodigoSunat = 6;
			}
			dtsReceptor = new Contribuyente
			{
				NroDocumento = cliente.RucDni,
				TipoDocumento = cliente.DocumentoIdentidad.CodigoSunat.ToString(),
				NombreLegal = cliente.RazonSocial,
				NombreComercial = "",
				Direccion = cliente.DireccionLegal
			};
			_documento.TipoDocumento = tipodocumento.Tipodoccodsunat.ToString();
			_documento.Receptor = dtsReceptor;
			_documento.FechaEmision = venta.FechaSalida.ToShortDateString();
			_documento.TipoOperacion = transacciones.Codsunat;
			_documento.Glosa = venta.Comentario;
			_documento.IssueTime = $"{DateTime.Now:HH:mm:ss}";
			if (venta.Moneda == 1)
			{
				_documento.Moneda = "PEN";
			}
			else
			{
				_documento.Moneda = "USD";
			}
			VerificaContribuyente = DatosComtribuyente(frmLogin.iCodEmpresa);
			if (VerificaContribuyente == 2)
			{
				MessageBox.Show("No se puede generar documento\n Falta cargar datos de la empresa");
				return;
			}
			switch (_documento.TipoDocumento)
			{
			case "03":
				_documento.IdDocumento = "B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				DatosFactura(cliente, venta, detalleventa);
				break;
			case "01":
				_documento.IdDocumento = "F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0');
				DatosFactura(cliente, venta, detalleventa);
				break;
			}
			_documento.MontoEnLetras = conv.enletras(venta.Total.ToString());
			ISerializador serializador = new Serializador();
			new DocumentoResponse().Exito = false;
			DocumentoResponse response = await new GenerarFactura(serializador).Post(_documento);
			if (!response.Exito)
			{
				MessageBox.Show(response.MensajeError);
			}
			RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml");
			File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(response.TramaXmlSinFirma));
			await Firmar();
			string rutadocumentosboletas = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutaxmlboletas = Program.CarpetaBoletas + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutadocumentosfacturas = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutaxmlfacturas = Program.CarpetaFacturas + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string tipoDocumento = _documento.TipoDocumento;
			string text = tipoDocumento;
			string text2 = text;
			if (!(text2 == "03"))
			{
				if (text2 == "01")
				{
					File.WriteAllBytes(rutaxmlfacturas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
					File.WriteAllBytes(rutadocumentosfacturas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
					extraeHash(rutaxmlfacturas);
				}
			}
			else
			{
				File.WriteAllBytes(rutaxmlboletas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
				File.WriteAllBytes(rutadocumentosboletas, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
				extraeHash(rutaxmlboletas);
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	private void DatosFactura(clsCliente cliente, clsFacturaVenta venta, List<clsDetalleFacturaVenta> detalleventa)
	{
		try
		{
			_documento.Items.Clear();
			_documento.CalculoIgv = Convert.ToDecimal(frmLogin.Configuracion.IGV) / 100m;
			int contadori = 1;
			foreach (clsDetalleFacturaVenta lista in detalleventa)
			{
				decimal valorUnitario = lista.PrecioUnitario / 1.18m;
				decimal igv = lista.Igv;
				if (lista.Tipoimpuesto.StartsWith("1"))
				{
					igv = Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(lista.Importe) / 1.18m;
				}
				DetalleDocumento dtsItems = new DetalleDocumento
				{
					Id = contadori,
					Cantidad = Convert.ToDecimal(lista.Cantidad),
					UnidadMedida = admProductos.SiglaUnidadBase(lista.UnidadIngresada),
					CodigoItem = lista.CodProducto.ToString(),
					Descripcion = lista.Descripcion,
					PrecioUnitario = valorUnitario,
					PrecioReferencial = Convert.ToDecimal(lista.PrecioUnitario),
					TipoPrecio = "01",
					TipoImpuesto = lista.Tipoimpuesto,
					OtroImpuesto = 0m,
					Descuento = Convert.ToDecimal(lista.Descuento1),
					Suma = Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(igv),
					Impuesto = (Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(igv)) * _documento.CalculoIgv,
					ImpuestoSelectivo = 0m,
					ICBPER = lista.icbper_band,
					ImpuestoICBPER = (lista.icbper_band ? lista.icbper : 0m),
					PorcentajeIcbper = frmLogin.Configuracion.Icbper,
					TotalVenta = Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(igv) - Convert.ToDecimal(lista.Descuento1)
				};
				productos = admProductos.CargaProducto(lista.CodProducto, frmLogin.iCodAlmacen);
				if (productos.CodTipoArticulo == 2)
				{
					_documento.MontoDetraccion = Convert.ToDecimal(Convert.ToDouble(_documento.Gravadas) * Convert.ToDouble(productos.Porcentajerentencion));
				}
				_documento.Items.Add(dtsItems);
				contadori++;
			}
			_documento.TotalICBPER = venta.icbper;
			CalcularTotales();
			if (_documento.TipoOperacion == "2002")
			{
				_documento.TipoOperacion = "0101";
				_documento.isAgentRetention = true;
				_documento.Retencion = new AllowanceCharge
				{
					ChargeIndicator = false,
					Amount = new PayableAmount
					{
						CurrencyId = _documento.Moneda,
						MultiplierFactorNumeric = 0.03m,
						Value = _documento.TotalVenta * 0.03m
					},
					BaseAmount = _documento.TotalVenta
				};
				_documento.TotalVenta = _documento.TotalVenta;
			}
			else
			{
				_documento.isAgentRetention = false;
			}
			bool contado = false;
			int ctdadDias = 0;
			if (venta.FormaPago == 5 || venta.FormaPago == 6 || venta.FormaPago == 20)
			{
				contado = true;
			}
			else
			{
				clsAdmFormaPago admfp = new clsAdmFormaPago();
				clsFormaPago fp = admfp.CargaFormaPago(venta.FormaPago);
				ctdadDias = fp.Dias;
			}
			_documento.FormaPago = new WinApp.Comun.Dto.Modelos.PaymentMeans
			{
				ID = "FormaPago",
				PaymentMeansCode = (contado ? "Contado" : "Credito"),
				Monto = (contado ? 0m : _documento.TotalVenta)
			};
			if (!contado)
			{
				_documento.TerminosPagos = new List<WinApp.Comun.Dto.Modelos.PaymentTerms>
				{
					new WinApp.Comun.Dto.Modelos.PaymentTerms
					{
						PaymentMeansID = "Cuota001",
						Amount = _documento.TotalVenta - _documento.Retencion.Amount.Value,
						PaymentDueDate = venta.FechaPago.AddDays(1.0)
					}
				};
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public async Task DatosNCredito(clsCliente cliente, clsNotaCredito nc, List<clsDetalleNotaCredito> detalle_nc)
	{
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			_documento = new DocumentoElectronico();
			venta = admfac.CargaFacturaVenta(nc.CodReferencia);
			empresa = admEmpresa.CargaEmpresa3(venta.CodEmpresa);
			tipodocumento = admTipodocumento.CargaTipoDocumento(nc.CodTipoDocumento);
			transacciones = admTransacciones.MuestraTransaccion(nc.CodTipoTransaccion);
			dtsReceptor = new Contribuyente
			{
				NroDocumento = cliente.RucDni,
				TipoDocumento = cliente.DocumentoIdentidad.CodigoSunat.ToString(),
				NombreLegal = cliente.RazonSocial,
				NombreComercial = "",
				Direccion = cliente.DireccionLegal
			};
			_documento.TipoDocumento = tipodocumento.Tipodoccodsunat.ToString();
			_documento.Receptor = dtsReceptor;
			_documento.FechaEmision = nc.FechaIngreso.ToShortDateString();
			_documento.TipoOperacion = transacciones.Codsunat;
			_documento.Glosa = null;
			if (venta.Moneda == 1)
			{
				_documento.Moneda = "PEN";
			}
			else
			{
				_documento.Moneda = "USD";
			}
			VerificaContribuyente = DatosComtribuyente(venta.CodEmpresa);
			if (VerificaContribuyente == 2)
			{
				MessageBox.Show("No se puede generar documento\n Falta cargar datos de la empresa");
				return;
			}
			_documento.CalculoIgv = Convert.ToDecimal(frmLogin.Configuracion.IGV) / 100m;
			int contador = 1;
			foreach (clsDetalleNotaCredito lista in detalle_nc)
			{
				DetalleDocumento dtsItems = new DetalleDocumento
				{
					Id = contador,
					Cantidad = Convert.ToDecimal(lista.Cantidad),
					UnidadMedida = admProductos.SiglaUnidadBase(lista.UnidadIngresada),
					CodigoItem = contador.ToString(),
					Descripcion = lista.DescripcionNC,
					PrecioUnitario = Convert.ToDecimal(lista.ValoReal),
					PrecioReferencial = Convert.ToDecimal(lista.PrecioUnitario),
					TipoPrecio = "01",
					TipoImpuesto = lista.TipoImpuesto,
					OtroImpuesto = 0m,
					Descuento = 0m,
					Suma = Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(lista.Igv),
					Impuesto = (Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(lista.Igv)) * _documento.CalculoIgv,
					ImpuestoSelectivo = 0m,
					ICBPER = lista.icbper_band,
					ImpuestoICBPER = (lista.icbper_band ? lista.icbper : 0m),
					PorcentajeIcbper = frmLogin.Configuracion.Icbper,
					TotalVenta = Convert.ToDecimal(lista.Importe) - Convert.ToDecimal(lista.Igv) - Convert.ToDecimal(lista.Descuento1)
				};
				productos = admProductos.CargaProducto(lista.CodProducto, frmLogin.iCodAlmacen);
				_documento.Items.Add(dtsItems);
				contador++;
			}
			if (venta.CodTipoDocumento == 2)
			{
				_documento.IdDocumento = "F" + nc.Serie + "-" + nc.NumFac.PadLeft(8, '0');
				_documento.TipoDocumento = tipodocumento.Tipodoccodsunat;
				_documento.TipoOperacion = transacciones.Codsunat;
			}
			else if (venta.CodTipoDocumento == 1)
			{
				_documento.IdDocumento = "B" + nc.Serie + "-" + nc.NumFac.PadLeft(8, '0');
				_documento.TipoDocumento = tipodocumento.Tipodoccodsunat;
				_documento.TipoOperacion = transacciones.Codsunat;
			}
			_documento.Receptor = dtsReceptor;
			_documento.TotalICBPER = nc.icbper;
			CalcularTotales();
			discrepancia = new Discrepancia();
			dr = new DocumentoRelacionado();
			dr.NroDocumento = ((venta.CodTipoDocumento == 2) ? ("F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0')) : ("B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0')));
			dr.TipoDocumento = ((venta.CodTipoDocumento == 2) ? "01" : "03");
			_documento.Relacionados.Add(dr);
			discrepancia.NroReferencia = ((venta.CodTipoDocumento == 2) ? ("F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0')) : ("B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0')));
			discrepancia.Tipo = ((venta.CodTipoDocumento == 2) ? "01" : "03");
			discrepancia.Descripcion = nc.Comentario;
			_documento.Discrepancias.Add(discrepancia);
			_documento.MontoEnLetras = conv.enletras(venta.Total.ToString());
			bool contado = false;
			if (nc.FormaPago == 5 || nc.FormaPago == 6 || nc.FormaPago == 20)
			{
				contado = true;
			}
			else
			{
				clsAdmFormaPago admfp = new clsAdmFormaPago();
				clsFormaPago fp = admfp.CargaFormaPago(nc.FormaPago);
				_ = fp.Dias;
			}
			if (discrepancia.Tipo == "13")
			{
				_documento.FormaPago = new WinApp.Comun.Dto.Modelos.PaymentMeans
				{
					ID = "FormaPago",
					PaymentMeansCode = (contado ? "Contado" : "Credito"),
					Monto = (contado ? 0m : Convert.ToDecimal(nc.Total))
				};
				if (!contado)
				{
					_documento.TerminosPagos = new List<WinApp.Comun.Dto.Modelos.PaymentTerms>
					{
						new WinApp.Comun.Dto.Modelos.PaymentTerms
						{
							PaymentMeansID = "Cuota001",
							Amount = Convert.ToDecimal(nc.Total),
							PaymentDueDate = nc.FechaPago.AddDays(1.0)
						}
					};
				}
			}
			_documento.MontoEnLetras = conv.enletras(venta.Total.ToString());
			ISerializador serializador = new Serializador();
			new DocumentoResponse().Exito = false;
			DocumentoResponse response = await new GenerarNotaCredito(serializador).Post(_documento);
			if (!response.Exito)
			{
				MessageBox.Show(response.MensajeError);
			}
			RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml");
			File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(response.TramaXmlSinFirma));
			await Firmar();
			string rutadocumentosnc = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS CREDITO\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			string rutaxmlnc = Program.CarpetaNC + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			File.WriteAllBytes(rutaxmlnc, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
			File.WriteAllBytes(rutadocumentosnc, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
			string codigoHash = extraeHash(rutaxmlnc);
			resumenfirmadig = respuestaFirmado.ResumenFirma;
			firmadig = respuestaFirmado.ValorFirma;
			GeneraPDF_NC(Convert.ToInt32(nc.CodNotaCreditoNueva));
			repositorio.Tipodoc = nc.CodTipoDocumento;
			repositorio.Nombredoc = _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento;
			repositorio.Fechaemision = nc.FechaIngreso;
			repositorio.Serie = nc.Serie;
			repositorio.Correlativo = nc.NumFac;
			repositorio.Monto = Convert.ToDecimal(nc.Total);
			repositorio.Estadosunat = "-1";
			repositorio.Mensajesunat = "No enviada";
			_ = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS CREDITO\\" + repositorio.Nombredoc + ".xml";
			repositorio.Usuario = frmLogin.iCodUser;
			repositorio.CodEmpresa = venta.CodEmpresa;
			repositorio.CodSucursal = venta.CodSucursal;
			repositorio.CodAlmacen = venta.CodAlmacen;
			repositorio.CodFacturaVenta = Convert.ToInt32(nc.CodNotaCreditoNueva);
			repositorio.TipDocRelacion = dr.NroDocumento;
			repositorio.CodigoHash = codigoHash;
			if (!admRepositorio.registra_repositorio(repositorio))
			{
				MessageBox.Show("Documento no se pudo enviar al repositorio");
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	public async void DatosNDebito(clsCliente cliente, clsNotaDebito nd, List<clsDetalleNotaDebito> detalle_nc)
	{
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			empresa = admEmpresa.CargaEmpresa3(frmLogin.iCodEmpresa);
			tipodocumento = admTipodocumento.CargaTipoDocumento(nd.CodTipoDocumento);
			transacciones = admTransacciones.MuestraTransaccion(nd.CodTipoTransaccion);
			new clsFacturaVenta();
			clsFacturaVenta venta = admfac.CargaFacturaVenta(nd.CodReferencia);
			dtsReceptor = new Contribuyente
			{
				NroDocumento = cliente.RucDni,
				TipoDocumento = cliente.DocumentoIdentidad.CodigoSunat.ToString(),
				NombreLegal = cliente.RazonSocial,
				NombreComercial = "",
				Direccion = cliente.DireccionLegal
			};
			_documento.TipoDocumento = tipodocumento.Tipodoccodsunat.ToString();
			_documento.Receptor = dtsReceptor;
			_documento.FechaEmision = DateTime.Today.ToShortDateString();
			_documento.TipoOperacion = transacciones.Codsunat;
			if (venta.Moneda == 1)
			{
				_documento.Moneda = "PEN";
			}
			else
			{
				_documento.Moneda = "USD";
			}
			VerificaContribuyente = DatosComtribuyente(venta.CodEmpresa);
			if (VerificaContribuyente == 2)
			{
				MessageBox.Show("No se puede generar documento\n Falta cargar datos de la empresa");
				return;
			}
			int contador = 1;
			foreach (clsDetalleNotaDebito lista in detalle_nc)
			{
				DetalleDocumento dtsItems = new DetalleDocumento
				{
					Id = contador,
					Cantidad = Convert.ToDecimal(lista.Cantidad),
					UnidadMedida = admProductos.SiglaUnidadBase(lista.UnidadIngresada),
					CodigoItem = contador.ToString(),
					Descripcion = lista.DescripcionND,
					PrecioUnitario = Convert.ToDecimal(lista.ValoReal),
					PrecioReferencial = Convert.ToDecimal(lista.PrecioUnitario),
					TipoPrecio = "01",
					TipoImpuesto = lista.Tipoimpuesto,
					OtroImpuesto = 0m,
					Descuento = 0m,
					Suma = Convert.ToDecimal(lista.ValoReal) * Convert.ToDecimal(lista.Cantidad),
					Impuesto = Convert.ToDecimal(lista.ValoReal) * Convert.ToDecimal(lista.Cantidad) * _documento.CalculoIgv,
					ImpuestoSelectivo = 0m,
					TotalVenta = Convert.ToDecimal(lista.ValoReal) * Convert.ToDecimal(lista.Cantidad) - Convert.ToDecimal(lista.Descuento1)
				};
				productos = admProductos.CargaProducto(lista.CodProducto, frmLogin.iCodAlmacen);
				if (productos.CodTipoArticulo == 2)
				{
					_documento.MontoDetraccion = Convert.ToDecimal(Convert.ToDouble(_documento.Gravadas) * Convert.ToDouble(productos.Porcentajerentencion));
				}
				_documento.Items.Add(dtsItems);
				contador++;
			}
			if (venta.CodTipoDocumento == 2)
			{
				_documento.IdDocumento = "F" + nd.Serie + "-" + nd.NumFac.PadLeft(8, '0');
				_documento.TipoDocumento = tipodocumento.Tipodoccodsunat;
				_documento.TipoOperacion = transacciones.Codsunat;
			}
			else if (venta.CodTipoDocumento == 1)
			{
				_documento.IdDocumento = "B" + nd.Serie + "-" + nd.NumFac.PadLeft(8, '0');
				_documento.TipoDocumento = tipodocumento.Tipodoccodsunat;
				_documento.TipoOperacion = transacciones.Codsunat;
			}
			_documento.Receptor = dtsReceptor;
			CalcularTotales();
			DocumentoRelacionado dtsDocumentoRelacionado = new DocumentoRelacionado
			{
				NroDocumento = ((venta.CodTipoDocumento == 2) ? ("F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0')) : ("B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0'))),
				TipoDocumento = ((venta.CodTipoDocumento == 2) ? "01" : "03")
			};
			_documento.Relacionados.Add(dtsDocumentoRelacionado);
			Discrepancia dtsDiscrepancia = new Discrepancia
			{
				NroReferencia = ((venta.CodTipoDocumento == 2) ? ("F" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0')) : ("B" + venta.Serie + "-" + venta.NumDoc.PadLeft(8, '0'))),
				Tipo = ((venta.CodTipoDocumento == 2) ? "01" : "03"),
				Descripcion = nd.Comentario.ToString()
			};
			_documento.Discrepancias.Add(dtsDiscrepancia);
			ISerializador serializador = new Serializador();
			new DocumentoResponse().Exito = false;
			DocumentoResponse response = await new GenerarNotaDedito(serializador).Post(_documento);
			if (!response.Exito)
			{
				MessageBox.Show(response.MensajeError);
			}
			RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml");
			File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(response.TramaXmlSinFirma));
			await Firmar();
			File.WriteAllBytes(Program.CarpetaNC + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml", Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
			File.WriteAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS DEBITO\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml", Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
			resumenfirmadig = respuestaFirmado.ResumenFirma;
			firmadig = respuestaFirmado.ValorFirma;
			GeneraPDF_ND(Convert.ToInt32(nd.CodNotaDebitoNueva));
			repositorio.Tipodoc = nd.CodTipoDocumento;
			repositorio.Nombredoc = _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento;
			repositorio.Fechaemision = nd.FechaPago;
			repositorio.Serie = nd.Serie;
			repositorio.Correlativo = nd.NumFac;
			repositorio.Monto = Convert.ToDecimal(nd.Total);
			repositorio.Estadosunat = "-1";
			repositorio.Mensajesunat = "No enviada";
			string mirutadearchivo = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS DEBITO\\" + repositorio.Nombredoc + ".xml";
			repositorio.Xml = File.ReadAllBytes(mirutadearchivo);
			repositorio.Pdf = File.ReadAllBytes(mirutadearchivo.Replace(".xml", ".pdf"));
			repositorio.Usuario = frmLogin.iCodUser;
			repositorio.CodEmpresa = frmLogin.iCodEmpresa;
			repositorio.CodSucursal = frmLogin.iCodSucursal;
			repositorio.CodAlmacen = frmLogin.iCodAlmacen;
			repositorio.CodFacturaVenta = Convert.ToInt32(nd.CodNotaDebitoNueva);
			repositorio.TipDocRelacion = _documento.IdDocumento;
			if (!admRepositorio.registra_repositorio(repositorio))
			{
				MessageBox.Show("Documento no se pudo enviar al repositorio");
			}
		}
		catch (Exception ex)
		{
			Exception a = ex;
			MessageBox.Show(a.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	private void CalcularTotales()
	{
		_documento.TotalIgv = Enumerable.Sum<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.Impuesto));
		_documento.TotalIsc = Enumerable.Sum<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.ImpuestoSelectivo));
		_documento.TotalOtrosTributos = Enumerable.Sum<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.OtroImpuesto));
		_documento.Gravadas = Enumerable.Sum<DetalleDocumento>(Enumerable.Where<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, bool>)((DetalleDocumento d) => d.TipoImpuesto.StartsWith("1"))), (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.Suma));
		_documento.Exoneradas = Enumerable.Sum<DetalleDocumento>(Enumerable.Where<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, bool>)((DetalleDocumento d) => d.TipoImpuesto.Contains("20"))), (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.Suma));
		_documento.Inafectas = Enumerable.Sum<DetalleDocumento>(Enumerable.Where<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, bool>)((DetalleDocumento d) => d.TipoImpuesto.StartsWith("3") || d.TipoImpuesto.Contains("40"))), (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.Suma));
		_documento.Gratuitas = Enumerable.Sum<DetalleDocumento>(Enumerable.Where<DetalleDocumento>((IEnumerable<DetalleDocumento>)_documento.Items, (Func<DetalleDocumento, bool>)((DetalleDocumento d) => d.TipoImpuesto.Contains("21"))), (Func<DetalleDocumento, decimal>)((DetalleDocumento d) => d.Suma));
		_documento.LineCountNumeric = Convert.ToString(_documento.Items.Count());
		if (_documento.TotalIsc > 0m)
		{
			_documento.TotalIgv = (_documento.Gravadas + _documento.TotalIsc) * _documento.CalculoIgv;
		}
		if (_documento.TotalICBPER > 0m)
		{
			_documento.TotalVenta = _documento.Gravadas + _documento.Exoneradas + _documento.Inafectas + _documento.TotalIgv + _documento.TotalIsc + _documento.TotalOtrosTributos + _documento.TotalICBPER;
		}
		else
		{
			_documento.TotalVenta = _documento.Gravadas + _documento.Exoneradas + _documento.Inafectas + _documento.TotalIgv + _documento.TotalIsc + _documento.TotalOtrosTributos;
		}
	}

	private async Task Firmar()
	{
		try
		{
			if (string.IsNullOrEmpty(_documento.IdDocumento))
			{
				MessageBox.Show("La Serie y el Correlativo no pueden estar vacíos");
				return;
			}
			string tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml")));
			FirmadoRequest firmadoRequest = new FirmadoRequest
			{
				TramaXmlSinFirma = tramaXmlSinFirma,
				CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\" + empresa.Certificado)),
				PasswordCertificado = empresa.Contrasena,
				UnSoloNodoExtension = false
			};
			ICertificador certificador = new Certificador();
			respuestaFirmado = await new Firmar(certificador).Post(firmadoRequest);
			if (!respuestaFirmado.Exito)
			{
				MessageBox.Show(respuestaFirmado.MensajeError);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	public async Task Enviar(clsEmpresa empresa, string IdDocumento, string TipoDocumento, string TramaXmlFirmado)
	{
		try
		{
			EnviarDocumentoRequest enviarDocumentoRequest = new EnviarDocumentoRequest
			{
				Ruc = empresa.Ruc,
				UsuarioSol = empresa.UsuarioSunat,
				ClaveSol = empresa.ClaveSunat,
				EndPointUrl = empresa.Url,
				IdDocumento = IdDocumento,
				TipoDocumento = TipoDocumento,
				TramaXmlFirmado = TramaXmlFirmado
			};
			ISerializador serializador = new Serializador();
			IServicioSunatDocumentos servicioSunatDocumentos = new ServicioSunatDocumentos();
			if (TipoDocumento == "RC")
			{
				respuestaEnvio = await new EnviarResumen(serializador, servicioSunatDocumentos).PostSunat(enviarDocumentoRequest);
				rptaresumen = (EnviarResumenResponse)respuestaEnvio;
			}
			else
			{
				respuestaEnvio = await new EnviarDocumento(serializador, servicioSunatDocumentos).PostSunat(enviarDocumentoRequest);
			}
			if (TipoDocumento != "RC")
			{
				rpta = (EnviarDocumentoResponse)respuestaEnvio;
			}
			try
			{
				switch (TipoDocumento)
				{
				case "07":
					File.WriteAllBytes(Program.CarpetaNC + "\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					File.WriteAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS CREDITO\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					break;
				case "08":
					File.WriteAllBytes(Program.CarpetaND + "\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					File.WriteAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS DEBITO\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					break;
				case "03":
					File.WriteAllBytes(Program.CarpetaBoletas + "\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					File.WriteAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					break;
				case "01":
					File.WriteAllBytes(Program.CarpetaFacturas + "\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					File.WriteAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
					break;
				case "RC":
					if (rptaresumen == null)
					{
						break;
					}
					if (rptaresumen.NroTicket != null)
					{
						if (respuestaEnvio != null)
						{
							if (respuestaEnvio.Exito)
							{
								File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "documentos\\RESUMEN\\" + empresa.Ruc + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
								File.WriteAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\RESUMEN\\" + empresa.Ruc + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
							}
							else
							{
								File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "documentos\\RESUMEN\\" + empresa.Ruc + "-" + TipoDocumento + "-" + IdDocumento + ".xml", Convert.FromBase64String(TramaXmlFirmado));
							}
						}
					}
					else
					{
						MessageBox.Show("Sunat no a devuelto N° de ticket" + rptaresumen.MensajeError + "Ticket: " + rptaresumen.NroTicket);
					}
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex2)
		{
			Exception ex3 = ex2;
			MessageBox.Show(ex3.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	public void GeneraPDF(int codigo)
	{
		new EscribirLog("Generando PDF ", mostrarConsola: true);
		DataSet jes = new DataSet();
		DataSet abi = new DataSet();
		string RutaArch = "";
		string RutaXML = "";
		if (_documento.TipoDocumento == "01")
		{
			RutaArch = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\FACTURAS\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			RutaXML = Program.CarpetaFacturas + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
		}
		else
		{
			RutaArch = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\BOLETAS\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
			RutaXML = Program.CarpetaBoletas + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
		}
		string[] cad = _documento.IdDocumento.Split('-');
		string[] fecha = _documento.FechaEmision.Split('/');
		datosAdicionales_CDB = _documento.Emisor.NroDocumento + "|" + _documento.TipoDocumento + "|" + cad[0].ToString() + "|" + cad[1].ToString() + "|" + _documento.TotalIgv + "|" + _documento.TotalVenta + "|" + fecha[2] + "-" + fecha[1] + "-" + fecha[0] + "|" + _documento.Receptor.TipoDocumento + "|" + _documento.Receptor.NroDocumento;
		CodigoCertificado = datosAdicionales_CDB + "|" + resumenfirmadig;
		new EscribirLog("Generando QR ", mostrarConsola: true);
		QRCodeGenerator qrGenerator = new QRCodeGenerator();
		QRCodeData qrCodeData = qrGenerator.CreateQrCode(CodigoCertificado, QRCodeGenerator.ECCLevel.Q);
		QRCode qrCode = new QRCode(qrCodeData);
		Bitmap bm = qrCode.GetGraphic(20);
		string rutaQR = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg";
		bm.Save(rutaQR, System.Drawing.Imaging.ImageFormat.Jpeg);
		if (File.Exists(rutaQR))
		{
			new EscribirLog("QR Generado C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg", mostrarConsola: true);
		}
		else
		{
			new EscribirLog("Regenerando QR ", mostrarConsola: true);
			QRCodeGenerator qrGenerator2 = new QRCodeGenerator();
			QRCodeData qrCodeData2 = qrGenerator2.CreateQrCode(CodigoCertificado, QRCodeGenerator.ECCLevel.Q);
			QRCode qrCode2 = new QRCode(qrCodeData);
			Bitmap bm2 = qrCode.GetGraphic(20);
			bm2.Save(rutaQR, System.Drawing.Imaging.ImageFormat.Jpeg);
			new EscribirLog("QR Regenerado correctamente ", mostrarConsola: true);
		}
		LogoEmp = CargarImagen("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg");
		frmRptFactura form = new frmRptFactura();
		if (formpagofact == 5 || formpagofact == 6)
		{
			CRReporteFacturacont rpt = new CRReporteFacturacont();
			jes = ds.ReporteFactura2(Convert.ToInt32(codigo));
			foreach (DataTable mel in jes.Tables)
			{
				foreach (DataRow changesRow in mel.Rows)
				{
					changesRow["firma"] = LogoEmp;
				}
				if (!mel.HasErrors)
				{
					continue;
				}
				foreach (DataRow changesRow2 in mel.Rows)
				{
					if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
					{
						changesRow2.RejectChanges();
						changesRow2.ClearErrors();
					}
				}
			}
			rpt.SetDataSource(jes);
			rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
			rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
			rpt.Close();
			rpt.Dispose();
			new EscribirLog("PDF Generado ", mostrarConsola: true);
			return;
		}
		CRReporteFactura rpt2 = new CRReporteFactura();
		jes = ds.ReporteFactura2(Convert.ToInt32(codigo));
		foreach (DataTable mel2 in jes.Tables)
		{
			foreach (DataRow changesRow3 in mel2.Rows)
			{
				changesRow3["firma"] = LogoEmp;
			}
			if (!mel2.HasErrors)
			{
				continue;
			}
			foreach (DataRow changesRow4 in mel2.Rows)
			{
				if ((int)changesRow4["Item", DataRowVersion.Current] > 100)
				{
					changesRow4.RejectChanges();
					changesRow4.ClearErrors();
				}
			}
		}
		rpt2.SetDataSource(jes);
		rpt2.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
		rpt2.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
		rpt2.Close();
		rpt2.Dispose();
		new EscribirLog("PDF Generado ", mostrarConsola: true);
	}

	public void GeneraPDF_NC(int codigo)
	{
		DataSet jes = new DataSet();
		DataSet abi = new DataSet();
		string RutaArch = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS CREDITO\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
		string RutaXML = Program.CarpetaNC + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
		string[] cad = _documento.IdDocumento.Split('-');
		string[] fecha = _documento.FechaEmision.Split('/');
		datosAdicionales_CDB = _documento.Emisor.NroDocumento + "|" + _documento.TipoDocumento + "|" + cad[0].ToString() + "|" + cad[1].ToString() + "|" + _documento.TotalIgv + "|" + _documento.TotalVenta + "|" + fecha[2] + "-" + fecha[1] + "-" + fecha[0] + "|" + _documento.Receptor.TipoDocumento + "|" + _documento.Receptor.NroDocumento;
		CodigoCertificado = datosAdicionales_CDB + "|" + resumenfirmadig;
		QRCodeGenerator qrGenerator = new QRCodeGenerator();
		QRCodeData qrCodeData = qrGenerator.CreateQrCode(CodigoCertificado, QRCodeGenerator.ECCLevel.Q);
		QRCode qrCode = new QRCode(qrCodeData);
		Bitmap bm = qrCode.GetGraphic(20);
		bm.Save("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
		LogoEmp = CargarImagen("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg");
		frmRptNotaCredito form = new frmRptNotaCredito();
		CRNotaCreditoVenta rpt = new CRNotaCreditoVenta();
		jes = ds1.ReportNotaCreditoVenta(Convert.ToInt32(codigo), venta.CodAlmacen);
		foreach (DataTable mel in jes.Tables)
		{
			foreach (DataRow changesRow in mel.Rows)
			{
				changesRow["firma"] = LogoEmp;
			}
			if (!mel.HasErrors)
			{
				continue;
			}
			foreach (DataRow changesRow2 in mel.Rows)
			{
				if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
				{
					changesRow2.RejectChanges();
					changesRow2.ClearErrors();
				}
			}
		}
		rpt.SetDataSource(jes);
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
		rpt.Close();
		rpt.Dispose();
	}

	public void GeneraPDF_ND(int codigo)
	{
		DataSet jes = new DataSet();
		DataSet abi = new DataSet();
		string RutaArch = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\NOTAS DEBITO\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
		string RutaXML = Program.CarpetaND + "\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".xml";
		string[] cad = _documento.IdDocumento.Split('-');
		string[] fecha = _documento.FechaEmision.Split('/');
		datosAdicionales_CDB = _documento.Emisor.NroDocumento + "|" + _documento.TipoDocumento + "|" + cad[0].ToString() + "|" + cad[1].ToString() + "|" + _documento.TotalIgv + "|" + _documento.TotalVenta + "|" + fecha[2] + "-" + fecha[1] + "-" + fecha[0] + "|" + _documento.Receptor.TipoDocumento + "|" + _documento.Receptor.NroDocumento;
		CodigoCertificado = datosAdicionales_CDB + "|" + resumenfirmadig;
		BarcodePDF417 codigobarras = new BarcodePDF417();
		codigobarras.Options = 0;
		codigobarras.ErrorLevel = 5;
		codigobarras.YHeight = 6f;
		codigobarras.SetText(CodigoCertificado);
		Bitmap bm = new Bitmap(codigobarras.CreateDrawingImage(Color.Black, Color.White));
		bm.Save("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
		LogoEmp = CargarImagen("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + _documento.Emisor.NroDocumento + "-" + _documento.TipoDocumento + "-" + _documento.IdDocumento + ".jpeg");
		frmRptNotaDebito form = new frmRptNotaDebito();
		CRNotaDebitoVenta rpt = new CRNotaDebitoVenta();
		rpt.Load("CRNotaDebitoVenta.rpt");
		jes = ds1.ReportNotaDebitoVenta(Convert.ToInt32(codigo), frmLogin.iCodAlmacen);
		foreach (DataTable mel in jes.Tables)
		{
			foreach (DataRow changesRow in mel.Rows)
			{
				changesRow["firma"] = LogoEmp;
			}
			if (!mel.HasErrors)
			{
				continue;
			}
			foreach (DataRow changesRow2 in mel.Rows)
			{
				if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
				{
					changesRow2.RejectChanges();
					changesRow2.ClearErrors();
				}
			}
		}
		rpt.SetDataSource(jes);
		form.crvNotaDebito.ReportSource = rpt;
		form.ShowDialog();
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
		rpt.Close();
		rpt.Dispose();
	}

	public static byte[] CargarImagen(string rutaArchivo)
	{
		if (rutaArchivo != "")
		{
			try
			{
				FileStream Archivo = new FileStream(rutaArchivo, FileMode.Open);
				BinaryReader binRead = new BinaryReader(Archivo);
				byte[] imagenEnBytes = new byte[Archivo.Length];
				binRead.Read(imagenEnBytes, 0, (int)Archivo.Length);
				binRead.Close();
				Archivo.Close();
				return imagenEnBytes;
			}
			catch
			{
				return new byte[0];
			}
		}
		return new byte[0];
	}

	public async void Generar_Resumen_Diario(List<clsFacturaVenta> lista, DateTime fechaBoleta, int numero, int codigoempresa)
	{
		try
		{
			int codEmp = admEmpresa.empresaxalmacen(codigoempresa);
			empresa = admEmpresa.CargaEmpresa3(Convert.ToInt32(codEmp));
			VerificaContribuyente = DatosComtribuyente(codEmp);
			ResumenDiarioNuevo documentoResumenDiario = new ResumenDiarioNuevo
			{
				IdDocumento = string.Format("RC-{0:yyyyMMdd}-" + numero, fechaBoleta),
				FechaEmision = fechaBoleta.ToString("yyyy-MM-dd"),
				FechaReferencia = fechaBoleta.ToString("yyyy-MM-dd"),
				Emisor = _documento.Emisor,
				Resumenes = new List<GrupoResumenNuevo>()
			};
			string nomdoc = "RC-" + string.Format("{0:yyyyMMdd}-" + numero, fechaBoleta);
			int contador = 1;
			foreach (clsFacturaVenta mel in lista)
			{
				GrupoResumenNuevo resu = new GrupoResumenNuevo
				{
					IdDocumento = mel.Serie + "-" + mel.NumDoc,
					Id = contador,
					TipoDocumentoReceptor = "1",
					NroDocumentoReceptor = mel.Doc_Cliente,
					Moneda = ((mel.Moneda == 1) ? "PEN" : "USD"),
					TotalVenta = Convert.ToDecimal(mel.Total),
					TotalIgv = Convert.ToDecimal(mel.Igv),
					Gravadas = mel.Gravadas,
					Exoneradas = mel.Exoneradas,
					Exportacion = 0m,
					Inafectas = mel.Inafectas,
					Gratuitas = mel.Gratuitas,
					TipoDocumento = ((mel.CodTipoDocumento == 1) ? "03" : "99"),
					CodigoEstadoItem = 1
				};
				documentoResumenDiario.Resumenes.Add(resu);
				contador++;
			}
			ISerializador serializador = new Serializador();
			new DocumentoResponse().Exito = false;
			DocumentoResponse response = await new GenerarResumenDiario(serializador).ResumenNuevo(documentoResumenDiario);
			if (response.Exito)
			{
				string RutaArchivo2 = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\RESUMEN\\" + documentoResumenDiario.IdDocumento + ".xml";
				RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentos\\RESUMEN\\" + documentoResumenDiario.IdDocumento + ".xml");
				File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(response.TramaXmlSinFirma));
				File.WriteAllBytes(RutaArchivo2, Convert.FromBase64String(response.TramaXmlSinFirma));
				string tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(RutaArchivo));
				FirmadoRequest firmadoRequest = new FirmadoRequest
				{
					TramaXmlSinFirma = tramaXmlSinFirma,
					CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\" + empresa.Certificado)),
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
				if (!respuestaFirmado.Exito)
				{
					throw new ApplicationException(respuestaFirmado.MensajeError);
				}
				File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
				repositorio = new clsRepositorio();
				repositorio.Nombredoc = nomdoc;
				if (admRepositorio.Registra_Resumen(repositorio))
				{
					foreach (clsFacturaVenta mel2 in lista)
					{
						if (repositorio.id_resumen > 0)
						{
							admRepositorio.Registra_Det_Resumen(repositorio.id_resumen, Convert.ToInt32(mel2.CodFacturaVenta));
						}
					}
				}
				repositorio.Tipodoc = 40;
				repositorio.Nombredoc = nomdoc;
				repositorio.Fechaemision = fechaBoleta;
				repositorio.Serie = "";
				repositorio.Correlativo = numero.ToString();
				repositorio.Monto = 0.00m;
				repositorio.CodEmpresa = codEmp;
				repositorio.CodSucursal = codigoempresa;
				repositorio.CodAlmacen = codigoempresa;
				repositorio.CodFacturaVenta = Convert.ToInt32(repositorio.id_resumen);
				repositorio.Estadosunat = "-1";
				repositorio.Mensajesunat = "";
				repositorio.Pdf = null;
				repositorio.Usuario = frmLogin.iCodUser;
				if (!admRepositorio.registra_repositorio(repositorio))
				{
					MessageBox.Show("Documento no se pudo enviar al repositorio");
				}
				else
				{
					MessageBox.Show("Documentos fueron generados");
				}
			}
			else
			{
				MessageBox.Show(response.MensajeError);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.Message.ToString());
		}
	}

	public async Task DatosGuiaRemision(clsCliente cliente, clsGuiaFacturacion gf, List<clsDetalleGuiaFacturacion> dgf)
	{
		try
		{
			Cursor.Current = Cursors.WaitCursor;
			documento = new GuiaRemision();
			guia = AdmGuia.ListaGuiaFacturacion(gf.codGuia);
			int Codempresa = ((guia.codSucursal == 1 || guia.codSucursal == 2) ? 1 : 2);
			empresa = admEmpresa.CargaEmpresa3(Codempresa);
			tipodocumento = admTipodocumento.CargaTipoDocumento(gf.codTipoDocumento);
			RestRequest _request = new RestRequest("/v1/clientessol/" + empresa.IDSUNATAPI + "/oauth2/token/", Method.Post).AddParameter("grant_type", "password").AddParameter("scope", "https://api-cpe.sunat.gob.pe").AddParameter("client_id", empresa.IDSUNATAPI)
				.AddParameter("client_secret", empresa.CLAVESUNATAPI)
				.AddParameter("username", empresa.Ruc + empresa.UsuarioSunat)
				.AddParameter("password", empresa.ClaveSunat);
			new TokenDTO();
			RestClient _clientToken = new RestClient(new RestClientOptions("https://api-seguridad.sunat.gob.pe")
			{
				Expect100Continue = false
			});
			TokenDTO _response = _clientToken.Post<TokenDTO>(_request);
			string token = _response.access_token;
			Direccion dtsDireccionLeegada = new Direccion
			{
				Ubigeo = guia.ubigueollegada,
				DireccionCompleta = gf.puntollegada
			};
			Direccion dtsDireccionPartida = new Direccion
			{
				Ubigeo = "200701",
				DireccionCompleta = gf.puntopartida
			};
			documento.DireccionLlegada = dtsDireccionLeegada;
			documento.DireccionPartida = dtsDireccionPartida;
			if (gf.placa != null)
			{
				documento.NroPlacaVehiculo = gf.placa;
			}
			documento.FechaInicioTraslado = gf.fechatransporte.ToShortDateString();
			documento.ModalidadTraslado = gf.codmodotransporte;
			if (documento.ModalidadTraslado == "02")
			{
				if (gf.vehiculomenor)
				{
					documento.VehiculoMenor = 1;
				}
				else
				{
					documento.VehiculoMenor = 0;
					documento.NombreConductor = gf.razonsocialcondutor;
					documento.NroDocumentoConductor = gf.docconductor;
					documento.ApellidosConductor = gf.apellidoconductor;
					documento.TituloConductor = "Principal";
					documento.LicenciaConductor = gf.nrolicencia;
				}
			}
			else
			{
				documento.RazonSocialTransportista = gf.razonsocialtransporte;
				documento.RucTransportista = gf.doctransporte;
			}
			documento.CodigoMotivoTraslado = gf.codmotivotransporte;
			Contribuyente dtsDestinatario = new Contribuyente
			{
				NroDocumento = cliente.RucDni,
				TipoDocumento = cliente.DocumentoIdentidad.CodigoSunat.ToString(),
				NombreLegal = cliente.RazonSocial,
				NombreComercial = "",
				Direccion = cliente.DireccionLegal
			};
			Contribuyente dtsRemitente = new Contribuyente
			{
				NroDocumento = empresa.Ruc,
				TipoDocumento = "6",
				Direccion = empresa.Direccion,
				Departamento = "PIURA",
				Provincia = "TALARA",
				Distrito = "TALARA",
				NombreLegal = empresa.RazonSocial,
				NombreComercial = "",
				Ubigeo = "200101",
				CodDomicilioFiscal = "0000"
			};
			documento.PesoBrutoTotal = gf.pesobruto;
			documento.NroPallets = gf.nropallets;
			documento.Destinatario = dtsDestinatario;
			documento.Remitente = dtsRemitente;
			documento.Glosa = gf.comentario;
			documento.TipoDocumento = tipodocumento.Tipodoccodsunat.ToString();
			documento.FechaEmision = gf.fechaemision.ToShortDateString();
			documento.IssueTime = $"{DateTime.Now:HH:mm:ss}";
			documento.IdDocumento = "T" + guia.numSerie + "-" + guia.correlativo.ToString().PadLeft(8, '0');
			int contador = 1;
			documento.BienesATransportar = new List<DetalleGuia>();
			documento.BienesATransportar.Clear();
			foreach (clsDetalleGuiaFacturacion lista in dgf)
			{
				DetalleGuia dtsItems20 = new DetalleGuia
				{
					Correlativo = contador,
					CodigoItem = lista.codProducto.ToString(),
					Descripcion = lista.producto,
					UnidadMedida = lista.unidad,
					Cantidad = lista.cantidad,
					LineaReferencia = contador
				};
				documento.BienesATransportar.Add(dtsItems20);
				contador++;
			}
			if (gf.Referencia != "-")
			{
				documento.DocumentoRelacionado = new DocumentoRelacionado
				{
					DescripcionDocumento = "Factura",
					TipoDocumento = "01",
					NroDocumento = gf.Referencia,
					RucEmisor = empresa.Ruc
				};
			}
			ISerializador serializador = new Serializador();
			new DocumentoResponse().Exito = false;
			DocumentoResponse response = await new GenerarGuiaRemision(serializador).Post(documento);
			if (!response.Exito)
			{
				MessageBox.Show(response.MensajeError);
			}
			RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".xml");
			File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(response.TramaXmlSinFirma));
			await FirmarGuia();
			string rutadocumentosgr = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\GUIAS\\" + documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".xml";
			string rutaxmlgr = Program.CarpetaGR + "\\" + documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".xml";
			nombredocumento = documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento;
			File.WriteAllBytes(rutaxmlgr, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
			File.WriteAllBytes(rutadocumentosgr, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
			File.WriteAllBytes(bytes: Convert.FromBase64String(await serializador.GenerarZip(respuestaFirmado.TramaXmlFirmado, nombredocumento)), path: Program.CarpetaCdr + "\\" + nombredocumento + ".zip");
			byte[] fileCF = File.ReadAllBytes(Program.CarpetaCdr + "\\" + nombredocumento + ".zip");
			if (token != null)
			{
				SHA256 _sha256 = SHA256.Create();
				byte[] _computeHash = _sha256.ComputeHash(fileCF);
				string _hash = BitConverter.ToString(_computeHash).Replace("-", string.Empty).ToLower();
				RestClient _clientGre = new RestClient(new RestClientOptions("https://api-cpe.sunat.gob.pe")
				{
					Expect100Continue = false
				});
				new ResponseTicket();
				RestRequest request = RestRequestExtensions.AddJsonBody(obj: new RequestEnvioGre
				{
					archivo = new ArchivoDTO
					{
						nomArchivo = nombredocumento + ".zip",
						arcGreZip = fileCF,
						hashZip = _hash
					}
				}, request: new RestRequest("/v1/contribuyente/gem/comprobantes/" + nombredocumento, Method.Post));
				_clientGre.Authenticator = new JwtAuthenticator(token);
				try
				{
					ResponseTicket respuesta = _clientGre.Post<ResponseTicket>(request);
					if (respuesta.numTicket != null)
					{
						try
						{
							DBAccessMYSQL dBAccess = new DBAccessMYSQL();
							new DataSet();
							dBAccess.AddParameter("_numTicket", respuesta.numTicket);
							dBAccess.AddParameter("_codguia", gf.codGuia);
							dBAccess.ExecuteDataSet("GuardaTicketEnvioGuia");
						}
						catch (Exception ex)
						{
							Exception ex2 = ex;
							MessageBox.Show(ex2.Message);
						}
						RestRequest request2 = new RestRequest("/v1/contribuyente/gem/comprobantes/envios/" + respuesta.numTicket);
						ResponseConsultaGre response2 = _clientGre.Get<ResponseConsultaGre>(request2);
						switch (response2.codRespuesta)
						{
						default:
						{
							string _mensaje = "Aun en proceso";
							Actualizarmensajesunatguia(_mensaje, gf.codGuia, "-1", "");
							break;
						}
						case "0":
						{
							File.WriteAllBytes(bytes: Convert.FromBase64String(response2.arcCdr), path: Program.CarpetaCdr + "\\R-" + nombredocumento + ".zip");
							string _mensaje = "La GRE numero :" + documento.IdDocumento + ", ha sido aceptada";
							Actualizarmensajesunatguia(_mensaje, gf.codGuia, "0", "");
							break;
						}
						case "99":
							if (response2.error != null)
							{
								string _codigoMensaje = response2.error.numError;
								if (_codigoMensaje == "1033")
								{
									string _mensaje = "La GRE numero :" + documento.IdDocumento + ", ha sido aceptada";
									Actualizarmensajesunatguia(_mensaje, gf.codGuia, "0", response2.error.numError);
								}
								else
								{
									_ = response2.error.numError;
									string _mensaje = response2.error.desError;
									Actualizarmensajesunatguia(_mensaje, gf.codGuia, "-1", response2.error.numError);
								}
							}
							if (response2.indCdrGenerado == "1")
							{
								File.WriteAllBytes(bytes: Convert.FromBase64String(response2.arcCdr), path: Program.CarpetaCdr + "\\R-" + nombredocumento + ".zip");
							}
							break;
						}
						return;
					}
				}
				catch (Exception ex)
				{
					Exception a = ex;
					MessageBox.Show(a.Message);
				}
			}
			extraeHash(rutaxmlgr);
			resumenfirmadig = respuestaFirmado.ResumenFirma;
			firmadig = respuestaFirmado.ValorFirma;
			GeneraPDF_GR(Convert.ToInt32(guia.codGuia));
		}
		catch (Exception ex)
		{
			Exception a2 = ex;
			MessageBox.Show(a2.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	private void Actualizarmensajesunatguia(string mensaje, int codguia, string estado, string coderror)
	{
		try
		{
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet ds = new DataSet();
			dBAccess.AddParameter("_mensaje", mensaje);
			dBAccess.AddParameter("_estado", estado);
			dBAccess.AddParameter("_codguia", codguia);
			dBAccess.AddParameter("_coderror", coderror);
			ds = dBAccess.ExecuteDataSet("ActualizaEstadoSunatGuia");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public async Task ConsultaticketGuia(clsGuiaFacturacion gf)
	{
		string documentonumero = "T" + gf.numSerie + "-" + gf.correlativo.ToString().PadLeft(8, '0');
		int Codempresa = ((gf.codSucursal == 1 || gf.codSucursal == 2) ? 1 : 2);
		empresa = admEmpresa.CargaEmpresa3(Codempresa);
		RestRequest _request = new RestRequest("/v1/clientessol/" + empresa.IDSUNATAPI + "/oauth2/token/", Method.Post).AddParameter("grant_type", "password").AddParameter("scope", "https://api-cpe.sunat.gob.pe").AddParameter("client_id", empresa.IDSUNATAPI)
			.AddParameter("client_secret", empresa.CLAVESUNATAPI)
			.AddParameter("username", empresa.Ruc + empresa.UsuarioSunat)
			.AddParameter("password", empresa.ClaveSunat);
		new TokenDTO();
		RestClient _clientToken = new RestClient(new RestClientOptions("https://api-seguridad.sunat.gob.pe")
		{
			Expect100Continue = false
		});
		TokenDTO _response = _clientToken.Post<TokenDTO>(_request);
		string token = _response.access_token;
		if (token == null)
		{
			return;
		}
		RestClient _clientGre = new RestClient(new RestClientOptions("https://api-cpe.sunat.gob.pe")
		{
			Expect100Continue = false
		});
		RestRequest request1 = new RestRequest("/v1/contribuyente/gem/comprobantes/envios/" + gf.NroTicket);
		_clientGre.Authenticator = new JwtAuthenticator(token);
		ResponseConsultaGre response1 = _clientGre.Get<ResponseConsultaGre>(request1);
		nombredocumento = empresa.Ruc + "-09-" + documentonumero;
		switch (response1.codRespuesta)
		{
		default:
		{
			string _mensaje = "Aun en proceso";
			Actualizarmensajesunatguia(_mensaje, gf.codGuia, "-1", "");
			break;
		}
		case "0":
		{
			File.WriteAllBytes(bytes: Convert.FromBase64String(response1.arcCdr), path: Program.CarpetaCdr + "\\R-" + nombredocumento + ".zip");
			string _mensaje = "La GRE numero :" + documentonumero + ", ha sido aceptada";
			Actualizarmensajesunatguia(_mensaje, gf.codGuia, "0", "");
			break;
		}
		case "99":
			if (response1.error != null)
			{
				string _codigoMensaje = response1.error.numError;
				if (_codigoMensaje == "1033")
				{
					string _mensaje = "La GRE numero :" + documentonumero + ", ha sido aceptada";
					Actualizarmensajesunatguia(_mensaje, gf.codGuia, "0", response1.error.numError);
				}
				else
				{
					_ = response1.error.numError;
					string _mensaje = response1.error.desError;
					Actualizarmensajesunatguia(_mensaje, gf.codGuia, "-1", response1.error.numError);
				}
			}
			if (response1.indCdrGenerado == "1")
			{
				File.WriteAllBytes(bytes: Convert.FromBase64String(response1.arcCdr), path: Program.CarpetaCdr + "\\R-" + nombredocumento + ".zip");
			}
			break;
		}
	}

	private async Task FirmarGuia()
	{
		try
		{
			if (string.IsNullOrEmpty(documento.IdDocumento))
			{
				MessageBox.Show("La Serie y el Correlativo no pueden estar vacíos");
				return;
			}
			string tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "documentos\\", documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".xml")));
			FirmadoRequest firmadoRequest = new FirmadoRequest
			{
				TramaXmlSinFirma = tramaXmlSinFirma,
				CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\" + empresa.Certificado)),
				PasswordCertificado = empresa.Contrasena,
				UnSoloNodoExtension = false
			};
			ICertificador certificador = new Certificador();
			respuestaFirmado = await new Firmar(certificador).Post(firmadoRequest);
			if (!respuestaFirmado.Exito)
			{
				MessageBox.Show(respuestaFirmado.MensajeError);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.Message);
		}
		finally
		{
			Cursor.Current = Cursors.Default;
		}
	}

	public void GeneraPDF_GR(int codigo)
	{
		DataSet jes = new DataSet();
		DataSet abi = new DataSet();
		string RutaArch = "C:\\DOCUMENTOS-" + empresa.Ruc + "\\DOCUMENTOS ENVIAR\\GUIAS\\" + documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".xml";
		string RutaXML = Program.CarpetaNC + "\\" + documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".xml";
		string[] cad = documento.IdDocumento.Split('-');
		string[] fecha = documento.FechaEmision.Split('/');
		datosAdicionales_CDB = documento.Remitente.NroDocumento + "|" + documento.TipoDocumento + "|" + cad[0].ToString() + "|" + cad[1].ToString() + "||" + fecha[2] + "-" + fecha[1] + "-" + fecha[0] + "|" + documento.Destinatario.TipoDocumento + "|" + documento.Destinatario.NroDocumento;
		CodigoCertificado = datosAdicionales_CDB + "|" + resumenfirmadig;
		QRCodeGenerator qrGenerator = new QRCodeGenerator();
		QRCodeData qrCodeData = qrGenerator.CreateQrCode(CodigoCertificado, QRCodeGenerator.ECCLevel.Q);
		QRCode qrCode = new QRCode(qrCodeData);
		Bitmap bm = qrCode.GetGraphic(20);
		bm.Save("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
		LogoEmp = CargarImagen("C:\\DOCUMENTOS-" + empresa.Ruc + "\\CERTIFIK\\QR\\" + documento.Remitente.NroDocumento + "-" + documento.TipoDocumento + "-" + documento.IdDocumento + ".jpeg");
		frmRptGuiaFacturacion form = new frmRptGuiaFacturacion();
		CRReporteGuiaFacturacion rpt = new CRReporteGuiaFacturacion();
		clsReporteGuiaFacturacion ds = new clsReporteGuiaFacturacion();
		jes = ds.Imprimir(Convert.ToInt32(codigo), 0);
		foreach (DataTable mel in jes.Tables)
		{
			foreach (DataRow changesRow in mel.Rows)
			{
				changesRow["firma"] = LogoEmp;
			}
			if (!mel.HasErrors)
			{
				continue;
			}
			foreach (DataRow changesRow2 in mel.Rows)
			{
				if ((int)changesRow2["Item", DataRowVersion.Current] > 100)
				{
					changesRow2.RejectChanges();
					changesRow2.ClearErrors();
				}
			}
		}
		rpt.SetDataSource(jes);
		form.crvguia.ReportSource = rpt;
		form.ShowDialog();
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaArch.Replace(".xml", ".pdf"));
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, RutaXML.Replace(".xml", ".pdf"));
		rpt.Close();
	}
}
