using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using FinalXML.Librerias;
using SIGEFA.Administradores;
using SIGEFA.Data;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.SunatFacElec;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmGuiaFacturacion : Form
{
	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsAdmPuntoLlegada AdmPunto = new clsAdmPuntoLlegada();

	public clsPuntoLlegada puntollegada = new clsPuntoLlegada();

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	public clsUsuario vendedor;

	public int codventa = 0;

	public int proceso;

	public int codguia;

	public int codentrega = 0;

	public int codpuntollegada;

	private clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();

	private clsFacturaVenta venta = new clsFacturaVenta();

	private clsGuiaFacturacion guiafacturacion = new clsGuiaFacturacion();

	private clsAdmGuiaFacturacion AdmGuia = new clsAdmGuiaFacturacion();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie AdmSerie = new clsAdmSerie();

	public List<clsDetalleGuiaFacturacion> detalle = new List<clsDetalleGuiaFacturacion>();

	private clsDetalleGuiaFacturacion detalleguia = new clsDetalleGuiaFacturacion();

	private Facturacion con = new Facturacion();

	private clsAdmCliente AdmCliente = new clsAdmCliente();

	private clsCliente cliente = new clsCliente();

	private clsAdmSucursal AdmSucursal = new clsAdmSucursal();

	private clsSucursal sucursal = new clsSucursal();

	private bool valida = true;

	private DataTable puntosllegada = new DataTable();

	private clsAdmConductor AdmCon = new clsAdmConductor();

	public clsConductor cond = new clsConductor();

	private clsAdmUnidadEquivalente clsuniequ = new clsAdmUnidadEquivalente();

	public static BindingSource data = new BindingSource();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	public DataTable d = null;

	private string documento;

	private string razonsocial;

	private string filtro = string.Empty;

	private clsValidar ok = new clsValidar();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	public int CodCliente;

	private DataGridViewComboBoxEditingControl dgvCombo;

	private bool isAddingRow = false;

	private bool Edition = false;

	private IContainer components = null;

	private GroupBox groupBox2;

	private RadGridView rgvproductos;

	private GroupBox Acciones;

	private RadButton btnimprimir;

	private RadButton btnguardar;

	private RadButton btnsalir;

	private GroupBox gbdatoscliente;

	private LabelX labelX7;

	private RadDropDownList cmbMoneda;

	private DateTimePicker dtpFecha;

	private LabelX labelX5;

	private LabelX labelX4;

	private LabelX labelX3;

	private LabelX labelX2;

	private LabelX labelX1;

	private TextBoxX txtNombreVendedor;

	private TextBoxX txtCodigoVendedor;

	private TextBoxX txtDireccion;

	private TextBoxX txtNombreCliente;

	public TextBoxX txtCodCliente;

	private GroupBox gbtotales;

	private Label label13;

	private TextBoxX txtPrecioVenta;

	private Label label9;

	private TextBoxX txtIGV;

	private Label label8;

	private TextBoxX txtValorVenta;

	private Label label24;

	private Label label22;

	private TextBoxX textBoxX5;

	private TextBoxX textBoxX1;

	private TextBoxX textBoxX2;

	private TextBoxX textBoxX6;

	private GroupBox gbempresatransporte;

	private LabelX lblrazonsocialempresa;

	private LabelX labelX12;

	private TextBoxX txtrazonsocialempresa;

	public TextBoxX txtdocempresatransporte;

	private GroupBox gbdatosconductor;

	private LabelX labelX8;

	public TextBoxX txtplaca;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private GroupBox gbpuntos;

	private LabelX labelX9;

	private TextBoxX txtdescripciontranslado;

	private GroupBox groupBox9;

	private RadGridView rgvdetalle;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private LabelX labelX13;

	private LabelX labelX10;

	private TextBoxX txtpuntopartida;

	private GroupBox gbmotivotranslado;

	private LabelX labelX14;

	private ComboBox cmbmodotranslado;

	private LabelX labelX17;

	private LabelX labelX16;

	private ComboBox cmbmotivotranslado;

	private DateTimePicker dtfechatranslado;

	private GroupBox gbdatosgenerales;

	private LabelX labelX15;

	private ComboBox cmbserie;

	private LabelX labelX18;

	private LabelX labelX19;

	private TextBoxX txtrazonsocialconductor;

	public TextBoxX txtdocconductor;

	private LabelX labelX20;

	private TextBoxX txtglosa;

	private LabelX labelX6;

	private TextBoxX txtreferencia;

	private ComboBox cmbdistrito;

	private ComboBox cmbprovincia;

	private ComboBox cmbdepartamento;

	private LabelX labelX25;

	private LabelX labelX24;

	private LabelX labelX23;

	private LabelX labelX11;

	private ComboBox cmbdoctransportista;

	private LabelX labelX21;

	private ComboBox cmbdocconductor;

	private CheckBox chbafectacion;

	private LinkLabel linkLabel1;

	private LabelX labelX22;

	public TextBoxX txtlicencia;

	private CheckBox chkvehiculomenor;

	private Label label1;

	public TextBoxX txtnropallets;

	private LabelX labelX26;

	public TextBoxX txtpesobruto;

	private LabelX labelX27;

	private LabelX labelX28;

	private TextBoxX txtapellidoconductor;

	private TextBoxX txtpuntollegada;

	private LabelX lbloc;

	public TextBoxX txtnumoc;

	private LabelX labelX29;

	public TextBoxX txtflete;

	private RadButton btnedidar;

	private GroupBox gbbucaproducto;

	private Label labelx;

	private TextBox txtFiltro;

	private Label label10;

	private Label label11;

	private GroupBox gbproductos;

	private DataGridView dgvproductos;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn codUniversal;

	private DataGridViewTextBoxColumn nomAlma;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewComboBoxColumn unidad;

	private DataGridViewTextBoxColumn nmarca;

	private DataGridViewTextBoxColumn Modelo;

	private DataGridViewTextBoxColumn stockdisponible;

	private DataGridViewTextBoxColumn cant;

	private DataGridViewTextBoxColumn precio;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn codunidadmedida;

	private DataGridViewTextBoxColumn codsunatimpuesto;

	private DataGridViewTextBoxColumn codtimpuesto;

	private DataGridViewTextBoxColumn codalma;

	private DataGridViewTextBoxColumn unidadnombre;

	private DataGridViewTextBoxColumn icbper;

	private DataGridViewTextBoxColumn codlinea;

	private DataGridViewTextBoxColumn codfamilia;

	public DataTable unidadesequi { get; set; }

	public frmGuiaFacturacion()
	{
		InitializeComponent();
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		DialogResult dlgResult = MessageBox.Show("¿Esta seguro de Cerrar la Guia?", "Emitir Guia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		if (dlgResult == DialogResult.Yes)
		{
			Close();
		}
	}

	private void frmGuiaFacturacion_Load(object sender, EventArgs e)
	{
		dtfechatranslado.Value = DateTime.Now.Date;
		CargaMoneda();
		CargaPuntoPartida();
		CargaModoTransporte();
		CargaMotivoTransporte();
		if (proceso == 1)
		{
			cmbdoctransportista.SelectedIndex = 1;
			cmbdocconductor.SelectedIndex = 0;
			cargaVendedor(frmLogin.iCodUser);
			if (codventa == 0)
			{
				dtpFecha.Value = DateTime.Now.Date;
				CargaProductos(frmLogin.iCodAlmacen);
				gbdatoscliente.Enabled = true;
			}
			else
			{
				CargaVenta();
			}
			CargaSerie();
			CargaDepartamentos();
			cmbdepartamento.SelectedValue = 20;
			cmbprovincia.SelectedValue = 2007;
			cmbdistrito.SelectedValue = 200701;
			txtdescripciontranslado.Focus();
			HabilitaEdición();
		}
		else
		{
			CargaGuia();
			acciones(estado: true);
		}
	}

	private void HabilitaEdición()
	{
		foreach (GridViewDataColumn column in rgvdetalle.Columns)
		{
			if (column.Name != "cantidad")
			{
				column.ReadOnly = true;
			}
		}
		rgvdetalle.Columns["cantidad"].ReadOnly = false;
		rgvdetalle.Columns["producto"].ReadOnly = false;
		rgvdetalle.Columns["preciounitario"].ReadOnly = false;
		rgvdetalle.Columns["unidad"].ReadOnly = false;
	}

	private void CargaDepartamentos()
	{
		try
		{
			DBAccessMYSQL dBAccess = new DBAccessMYSQL();
			DataSet dsAlmacen = new DataSet();
			dsAlmacen = dBAccess.ExecuteDataSet("CargaDepartamentos");
			cmbdepartamento.DataSource = dsAlmacen.Tables[0];
			cmbdepartamento.DisplayMember = "description";
			cmbdepartamento.ValueMember = "id";
			cmbprovincia.SelectedValue = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaMoneda()
	{
		try
		{
			cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
			cmbMoneda.DisplayMember = "descripcion";
			cmbMoneda.ValueMember = "codMoneda";
			cmbMoneda.SelectedValue = 1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaPuntoPartida()
	{
		sucursal = AdmSucursal.CargaSucursal(frmLogin.iCodSucursal);
		txtpuntopartida.Text = sucursal.Ubicacion;
	}

	private void CargaSerie()
	{
		try
		{
			if (venta.CodAlmacen == 0)
			{
				venta.CodAlmacen = frmLogin.iCodAlmacen;
			}
			cmbserie.DataSource = AdmSerie.MuestraSeries(11, venta.CodAlmacen);
			cmbserie.DisplayMember = "Serie";
			cmbserie.ValueMember = "Codserie";
			cmbserie.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaModoTransporte()
	{
		try
		{
			cmbmodotranslado.DataSource = AdmGuia.ListaModoTransporte();
			cmbmodotranslado.DisplayMember = "description";
			cmbmodotranslado.ValueMember = "id";
			cmbmodotranslado.SelectedIndex = 0;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaMotivoTransporte()
	{
		try
		{
			cmbmotivotranslado.DataSource = AdmGuia.ListaMotivoTransporte();
			cmbmotivotranslado.DisplayMember = "description";
			cmbmotivotranslado.ValueMember = "id";
			cmbmotivotranslado.SelectedIndex = 0;
			txtdescripciontranslado.Text = cmbmotivotranslado.Text;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cargaVendedor(int coduser)
	{
		vendedor = admUsuario.MuestraUsuarioSinAdmin(coduser);
		if (vendedor != null)
		{
			txtCodigoVendedor.Text = vendedor.CodUsuario.ToString();
			txtNombreVendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
		}
		else
		{
			txtCodigoVendedor.Text = "";
			txtCodigoVendedor.Text = "";
			MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cargaCliente(int codcliente)
	{
		cliente = AdmCliente.MuestraCliente(codcliente);
		if (vendedor != null)
		{
			txtCodCliente.Text = cliente.RucDni.ToString();
			txtNombreCliente.Text = cliente.Nombre;
			txtDireccion.Text = cliente.DireccionLegal;
		}
		else
		{
			txtCodCliente.Text = "";
			txtNombreCliente.Text = "";
			MessageBox.Show("No se encontró ningún vendedor con el código ingresado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CargaVenta()
	{
		try
		{
			if (codentrega != 0)
			{
				try
				{
					DBAccessMYSQL dBAccess = new DBAccessMYSQL();
					DataSet ds = new DataSet();
					dBAccess.AddParameter("codentrega", codentrega);
					ds = dBAccess.ExecuteDataSet("cargacodventaentrega");
					DataTable costos = ds.Tables[0];
					codventa = Convert.ToInt32(costos.Rows[0]["codDocRelacionado"]);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			venta = AdmVenta.CargaFacturaVenta(codventa);
			if (venta != null)
			{
				cargaCliente(venta.CodCliente);
				dtpFecha.Value = DateTime.Now.Date;
				cmbMoneda.SelectedValue = venta.Moneda;
				cargaVendedor(venta.CodUser);
				txtValorVenta.Text = $"{venta.Total - venta.Igv:#,##0.00}";
				txtIGV.Text = $"{venta.Igv:#,##0.00}";
				txtPrecioVenta.Text = $"{venta.Total:#,##0.00}";
				CargaDetalleVenta();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleVenta()
	{
		try
		{
			if (codentrega != 0)
			{
				try
				{
					DBAccessMYSQL dBAccess = new DBAccessMYSQL();
					DataSet ds = new DataSet();
					dBAccess.AddParameter("codentrega", codentrega);
					ds = dBAccess.ExecuteDataSet("tempproductosentregaguia");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				rgvdetalle.DataSource = AdmVenta.CargaDetalle(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen, 2);
			}
			else
			{
				rgvdetalle.DataSource = AdmVenta.CargaDetalle(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen, 1);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	private void CargaGuia()
	{
		try
		{
			guiafacturacion = AdmGuia.ListaGuiaFacturacion(codguia);
			if (venta != null)
			{
				txtglosa.Text = guiafacturacion.glosa;
				txtCodCliente.Text = guiafacturacion.RucDni;
				txtNombreCliente.Text = guiafacturacion.RazonSocial;
				txtDireccion.Text = guiafacturacion.Direccion;
				dtpFecha.Value = guiafacturacion.fechaemision;
				dtfechatranslado.Value = guiafacturacion.fechatransporte;
				cmbMoneda.SelectedValue = guiafacturacion.codMoneda;
				txtdocempresatransporte.Text = guiafacturacion.doctransporte;
				lblrazonsocialempresa.Text = guiafacturacion.razonsocialtransporte;
				txtdescripciontranslado.Text = guiafacturacion.descripciontransporte;
				txtdocconductor.Text = guiafacturacion.docconductor;
				txtrazonsocialconductor.Text = guiafacturacion.razonsocialcondutor;
				txtpuntollegada.Text = guiafacturacion.puntollegada;
				txtpuntopartida.Text = guiafacturacion.puntopartida;
				txtrazonsocialempresa.Text = guiafacturacion.razonsocialtransporte;
				txtplaca.Text = guiafacturacion.placa;
				txtpesobruto.Text = guiafacturacion.pesobruto.ToString();
				txtnropallets.Text = guiafacturacion.nropallets.ToString();
				cargaVendedor(guiafacturacion.codUsuario);
				txtreferencia.Text = guiafacturacion.Referencia;
				txtnumoc.Text = guiafacturacion.NumOc;
				txtValorVenta.Text = $"{guiafacturacion.total - guiafacturacion.igv:#,##0.00}";
				txtIGV.Text = $"{guiafacturacion.igv:#,##0.00}";
				txtPrecioVenta.Text = $"{guiafacturacion.total:#,##0.00}";
				CargaDetalleGuia();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Guia Facturacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalleGuia()
	{
		rgvdetalle.DataSource = AdmGuia.ListaDetalleGuia(codguia);
	}

	private async void btnguardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (proceso == 1)
			{
				validaciones();
				if (!valida)
				{
					return;
				}
				ser = AdmSerie.CargaSerieEmpresa(venta.CodAlmacen, 11);
				if (ser == null)
				{
					throw new Exception("\tError Serie de Venta\nNo se encontro una serie para registrar la venta.\nError: AdmSerie.CargaSerieEmpresa(" + venta.CodAlmacen + ", " + 10 + ");");
				}
				guiafacturacion.codVenta = Convert.ToInt32(venta.CodFacturaVenta);
				guiafacturacion.codCliente = ((venta.CodCliente == 0) ? cli.CodCliente : venta.CodCliente);
				guiafacturacion.codOrdenCompra = venta.codordencompra;
				guiafacturacion.codSerie = ser.CodSerie;
				guiafacturacion.numSerie = ser.Serie;
				guiafacturacion.correlativo = ser.Numeracion;
				guiafacturacion.codalmacen = venta.CodAlmacen;
				guiafacturacion.estado = true;
				guiafacturacion.fecharegistro = DateTime.Now;
				guiafacturacion.fechaemision = dtpFecha.Value.Date;
				guiafacturacion.codUsuario = frmLogin.iCodUser;
				guiafacturacion.tipocambio = ((Convert.ToDecimal(venta.TipoCambio) == 0m) ? 1m : Convert.ToDecimal(venta.TipoCambio));
				guiafacturacion.codSucursal = ((venta.CodSucursal == 0) ? frmLogin.iCodSucursal : venta.CodSucursal);
				guiafacturacion.codTipoDocumento = 11;
				guiafacturacion.codMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				guiafacturacion.comentario = txtdescripciontranslado.Text;
				guiafacturacion.valorventa = Convert.ToDecimal(txtValorVenta.Text);
				guiafacturacion.igv = Convert.ToDecimal(txtIGV.Text);
				guiafacturacion.total = Convert.ToDecimal(txtPrecioVenta.Text);
				guiafacturacion.doctransporte = txtdocempresatransporte.Text;
				guiafacturacion.razonsocialtransporte = txtrazonsocialempresa.Text;
				guiafacturacion.codmodotransporte = cmbmodotranslado.SelectedValue.ToString();
				guiafacturacion.codmotivotransporte = cmbmotivotranslado.SelectedValue.ToString();
				guiafacturacion.descripciontransporte = txtdescripciontranslado.Text;
				guiafacturacion.fechatransporte = dtfechatranslado.Value.Date;
				guiafacturacion.docconductor = txtdocconductor.Text;
				guiafacturacion.razonsocialcondutor = txtrazonsocialconductor.Text;
				guiafacturacion.placa = txtplaca.Text;
				guiafacturacion.puntopartida = txtpuntopartida.Text;
				guiafacturacion.puntollegada = txtpuntollegada.Text;
				guiafacturacion.ubigueollegada = cmbdistrito.SelectedValue.ToString();
				guiafacturacion.vehiculomenor = chkvehiculomenor.Checked;
				guiafacturacion.nrolicencia = txtlicencia.Text;
				guiafacturacion.pesobruto = Convert.ToDecimal(txtpesobruto.Text);
				guiafacturacion.nropallets = Convert.ToInt32(txtnropallets.Text);
				guiafacturacion.estadosunat = "-1";
				guiafacturacion.apellidoconductor = txtapellidoconductor.Text;
				guiafacturacion.glosa = txtglosa.Text;
				guiafacturacion.NumOc = txtnumoc.Text;
				guiafacturacion.Flete = Convert.ToDecimal(txtflete.Text);
				if (AdmGuia.insert(guiafacturacion))
				{
					RecorreDetalle();
					if (detalle.Count > 0)
					{
						foreach (clsDetalleGuiaFacturacion det in detalle)
						{
							AdmGuia.InsertDetalle(det);
						}
					}
					MessageBox.Show("Los datos se guardaron correctamente ", "Guia Remisión", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					acciones(estado: true);
					txtglosa.Text = guiafacturacion.correlativo.ToString().PadLeft(6);
				}
				else
				{
					MessageBox.Show("Los datos No se  guardaron ", "Guia Remisión", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				if (proceso != 2)
				{
					return;
				}
				Edition = false;
				guiafacturacion.codGuia = codguia;
				guiafacturacion.fechaemision = dtpFecha.Value.Date;
				guiafacturacion.codUsuario = frmLogin.iCodUser;
				guiafacturacion.fechatransporte = dtfechatranslado.Value.Date;
				guiafacturacion.NumOc = txtnumoc.Text;
				guiafacturacion.valorventa = Convert.ToDecimal(txtValorVenta.Text);
				guiafacturacion.igv = Convert.ToDecimal(txtIGV.Text);
				guiafacturacion.total = Convert.ToDecimal(txtPrecioVenta.Text);
				if (!AdmGuia.UpdateGuiaFacturacion(guiafacturacion))
				{
					return;
				}
				RecorreDetalle();
				if (detalle.Count > 0)
				{
					foreach (clsDetalleGuiaFacturacion det2 in detalle)
					{
						AdmGuia.UpdateDetalle(det2);
					}
				}
				MessageBox.Show("Los datos se actualizaron correctamente ", "Guia Remisión", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				acciones(estado: true);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			MessageBox.Show(ex2.Message);
		}
	}

	private void validaciones()
	{
		if (string.IsNullOrEmpty(txtpesobruto.Text))
		{
			MessageBox.Show("Error al guardar la Guia Falta agregar Peso Bruto", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtpesobruto.Focus();
			valida = false;
		}
		else if (string.IsNullOrEmpty(txtpuntopartida.Text))
		{
			MessageBox.Show("Error al guardar la Guia Falta agregar Descripción Punto Partida", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtpuntopartida.Focus();
			valida = false;
		}
		else if (string.IsNullOrEmpty(txtpuntollegada.Text))
		{
			MessageBox.Show("Error al guardar la Guia Falta agregar Descripción Punto de llegada", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtpuntollegada.Focus();
			valida = false;
		}
		else if (string.IsNullOrEmpty(txtnropallets.Text))
		{
			MessageBox.Show("Error al guardar la Guia Falta agregar Nro Pallets", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtnropallets.Focus();
			valida = false;
		}
		else if (string.IsNullOrEmpty(txtplaca.Text) || txtplaca.Text.Contains("-"))
		{
			MessageBox.Show("Error al guardar  Guia , Revisar campo Placa puede estar : vacio o contiene '-' ", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtplaca.Focus();
			valida = false;
		}
		else if (cmbmodotranslado.SelectedIndex == 1 && !chkvehiculomenor.Checked)
		{
			if (string.IsNullOrEmpty(txtdocconductor.Text))
			{
				MessageBox.Show("Error al guardar la Guia Falta agregar Documento Conductor", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtdocconductor.Focus();
				valida = false;
			}
			else if (string.IsNullOrEmpty(txtrazonsocialconductor.Text))
			{
				MessageBox.Show("Error al guardar la Guia Falta agregar Nombre Conductor", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtrazonsocialconductor.Focus();
				valida = false;
			}
			else if (string.IsNullOrEmpty(txtlicencia.Text))
			{
				MessageBox.Show("Error al guardar la Guia Falta agregar Nro Licencia", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtlicencia.Focus();
				valida = false;
			}
			else if (string.IsNullOrEmpty(txtapellidoconductor.Text))
			{
				MessageBox.Show("Error al guardar la Guia Falta agregar Apellido Conductor", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtapellidoconductor.Focus();
				valida = false;
			}
			else
			{
				valida = true;
			}
		}
		else if (cmbmodotranslado.SelectedIndex == 0)
		{
			if (string.IsNullOrEmpty(txtdocempresatransporte.Text))
			{
				MessageBox.Show("Error al guardar la Guia Falta agregar Documento Transportista", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtdocempresatransporte.Focus();
				valida = false;
			}
			else if (string.IsNullOrEmpty(txtrazonsocialempresa.Text))
			{
				MessageBox.Show("Error al guardar la Guia Falta agregar Nombre Transportista", "Guias", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtrazonsocialempresa.Focus();
				valida = false;
			}
			else
			{
				valida = true;
			}
		}
		else
		{
			valida = true;
		}
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		if (rgvdetalle.Rows.Count <= 0)
		{
			return;
		}
		foreach (GridViewRowInfo row in rgvdetalle.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(GridViewRowInfo fila)
	{
		try
		{
			clsDetalleGuiaFacturacion deta = new clsDetalleGuiaFacturacion();
			deta.codGuia = guiafacturacion.codGuia;
			deta.codAlmacen = guiafacturacion.codalmacen;
			deta.codProducto = Convert.ToInt32(fila.Cells["referencia"].Value);
			deta.cantidad = Convert.ToDecimal(fila.Cells["cantidad"].Value);
			deta.preciounitario = Convert.ToDecimal(fila.Cells["preciounitario"].Value);
			deta.valorventa = Convert.ToDecimal(fila.Cells["valorventa"].Value);
			deta.igv = Convert.ToDecimal(fila.Cells["igv"].Value);
			deta.total = Convert.ToDecimal(fila.Cells["importe"].Value);
			deta.estado = true;
			deta.codUnidad = Convert.ToInt32(fila.Cells["codUnidadMedida"].Value);
			deta.unidad = Convert.ToString(fila.Cells["unidad"].Value);
			deta.codMoneda = guiafacturacion.codMoneda;
			deta.fecharegistro = DateTime.Now;
			deta.producto = fila.Cells["producto"].Value.ToString();
			detalle.Add(deta);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void acciones(bool estado)
	{
		btnimprimir.Enabled = estado;
		btnguardar.Enabled = !estado;
		gbempresatransporte.Enabled = !estado;
		gbdatosconductor.Enabled = !estado;
		gbdatoscliente.Enabled = !estado;
		gbdatosgenerales.Enabled = !estado;
		gbpuntos.Enabled = !estado;
		gbtotales.Enabled = !estado;
		gbmotivotranslado.Enabled = !estado;
		txtpesobruto.Enabled = !estado;
		txtnropallets.Enabled = !estado;
		txtglosa.Enabled = !estado;
		txtplaca.Enabled = !estado;
		txtnumoc.Enabled = !estado;
		txtflete.Enabled = !estado;
		btnedidar.Enabled = estado;
		gbbucaproducto.Enabled = !estado;
		gbproductos.Enabled = !estado;
		rgvdetalle.Enabled = !estado;
		dgvproductos.Enabled = !estado;
	}

	private void AcionesEditar(bool estado)
	{
		btnimprimir.Enabled = !estado;
		btnguardar.Enabled = estado;
		gbmotivotranslado.Enabled = estado;
		gbdatosgenerales.Enabled = estado;
		dtpFecha.Enabled = estado;
		dtfechatranslado.Enabled = estado;
		btnguardar.Enabled = estado;
		cmbserie.Enabled = !estado;
		txtCodigoVendedor.Enabled = !estado;
		cmbmodotranslado.Enabled = !estado;
		txtdescripciontranslado.Enabled = !estado;
		cmbmotivotranslado.Enabled = !estado;
		txtNombreVendedor.Enabled = !estado;
		txtnumoc.Enabled = estado;
		btnedidar.Enabled = !estado;
	}

	private void btnimprimir_Click(object sender, EventArgs e)
	{
		try
		{
			frmRptGuiaFacturacion form = new frmRptGuiaFacturacion();
			CRReporteGuiaFacturacion rpt = new CRReporteGuiaFacturacion();
			clsReporteGuiaFacturacion ds = new clsReporteGuiaFacturacion();
			DataSet jes = new DataSet();
			jes = ds.Imprimir(Convert.ToInt32(guiafacturacion.codGuia), Convert.ToInt32(chbafectacion.Checked));
			rpt.SetDataSource(jes);
			form.crvguia.ReportSource = rpt;
			form.ShowDialog();
			rpt.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtdocempresatransporte_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsControl(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsSeparator(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
		if (e.KeyChar == '\r')
		{
			try
			{
				documento = "";
				razonsocial = "";
				documento = txtdocempresatransporte.Text;
				BuscaConductor(documento);
				txtrazonsocialempresa.Text = razonsocial;
			}
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}
	}

	private void BuscaConductor(string documento)
	{
		switch (documento.Length)
		{
		case 1:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo un digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 2:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo dos digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 3:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo tres digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 4:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cuatro digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 5:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cinco digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 6:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo seis digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 7:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo siete digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 8:
		{
			cond = AdmCon.BuscaConductor(0, documento);
			if (cond != null)
			{
				razonsocial = cond.Nombre;
				break;
			}
			CargaDNI(documento);
			clsConductor con2 = new clsConductor();
			con2.Nombre = razonsocial;
			con2.Dni = documento;
			con2.Ruc = "";
			con2.Licencia = "";
			con2.Telefono = "-";
			con2.Direccion = "-";
			con2.CodUser = frmLogin.iCodUser;
			AdmCon.insert(con2);
			break;
		}
		case 9:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso nueve digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 10:
			MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso diez digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			break;
		case 11:
		{
			cond = AdmCon.BuscaConductor(1, documento);
			if (cond != null)
			{
				razonsocial = cond.Nombre;
				break;
			}
			CargaRUC(documento);
			clsConductor con = new clsConductor();
			con.Nombre = razonsocial;
			con.Ruc = documento;
			con.CodUser = frmLogin.iCodUser;
			AdmCon.insert(con);
			break;
		}
		}
	}

	private void CargaDNI(string documento)
	{
		try
		{
			ReniecAPI reniecAPI = new ReniecAPI();
			string result = reniecAPI.GetInfo(documento);
			string[] array = result.Split('|');
			razonsocial = ((array.Length != 0) ? array[0] : "");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "ERROR API RENIEC - frmGuiaFacturación");
		}
	}

	private void CargaRUC(string documento)
	{
		if (documento.Length == 11)
		{
			try
			{
				ReniecAPI reniecAPI = new ReniecAPI();
				string result = reniecAPI.GetInfoRuc(documento);
				string[] array = result.Split('|');
				razonsocial = array[0];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ERROR API RENIEC - frmGuiaFacturación");
			}
		}
	}

	private void txtdocconductor_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsDigit(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsControl(e.KeyChar))
		{
			e.Handled = false;
		}
		else if (char.IsSeparator(e.KeyChar))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
		if (e.KeyChar != '\r')
		{
			return;
		}
		try
		{
			documento = "";
			razonsocial = "";
			documento = txtdocconductor.Text;
			BuscaConductor(documento);
			int indiceEspacio = razonsocial.IndexOf(' ');
			int segundoEspacio = razonsocial.IndexOf(' ', indiceEspacio + 1);
			if (indiceEspacio != -1)
			{
				string primeraParte = razonsocial.Substring(0, segundoEspacio);
				txtapellidoconductor.Text = primeraParte;
				string segundaParte = razonsocial.Substring(segundoEspacio + 1);
				txtrazonsocialconductor.Text = segundaParte;
			}
			else
			{
				Console.WriteLine("No se encontró un espacio en blanco en la cadena.");
			}
		}
		catch (Exception ex)
		{
			Cursor = Cursors.Default;
			MessageBox.Show(ex.Message);
		}
	}

	private void txtdocempresatransporte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmConductores form = new frmConductores();
			form.provieneguias = 1;
			form.tipodocumento = Convert.ToInt32(cmbdoctransportista.SelectedIndex);
			form.ShowDialog();
			cond = form.cond;
			if (Convert.ToInt32(cmbdoctransportista.SelectedIndex) == 0)
			{
				txtdocempresatransporte.Text = cond.Dni;
			}
			else
			{
				txtdocempresatransporte.Text = cond.Ruc;
			}
			txtrazonsocialempresa.Text = cond.Nombre;
		}
	}

	private void cmbdoctransportista_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbdoctransportista.SelectedIndex) == 0)
		{
			txtdocempresatransporte.Text = "";
			txtrazonsocialempresa.Text = "";
			txtdocempresatransporte.MaxLength = 8;
			txtdocempresatransporte.Focus();
		}
		else
		{
			txtdocempresatransporte.Text = "";
			txtrazonsocialempresa.Text = "";
			txtdocempresatransporte.MaxLength = 11;
			txtdocempresatransporte.Focus();
		}
	}

	private void cmbdocconductor_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cmbdocconductor.SelectedIndex) == 0)
		{
			txtdocconductor.Text = "";
			txtdocconductor.MaxLength = 8;
			txtdocconductor.Focus();
		}
		else
		{
			txtdocconductor.Text = "";
			txtdocconductor.MaxLength = 11;
			txtdocconductor.Focus();
		}
	}

	private void cmbdepartamento_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cmbdepartamento.SelectedIndex != 0)
			{
				DBAccessMYSQL dBAccess = new DBAccessMYSQL();
				DataSet dsAlmacen = new DataSet();
				dBAccess.AddParameter("iddepartamento", cmbdepartamento.SelectedValue);
				dsAlmacen = dBAccess.ExecuteDataSet("CargaProvincias");
				cmbprovincia.DataSource = dsAlmacen.Tables[0];
				cmbprovincia.DisplayMember = "description";
				cmbprovincia.ValueMember = "id";
				cmbdistrito.SelectedValue = 0;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cmbprovincia_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (cmbprovincia.SelectedIndex != 0)
			{
				DBAccessMYSQL dBAccess = new DBAccessMYSQL();
				DataSet dsAlmacen = new DataSet();
				dBAccess.AddParameter("_province_id", cmbprovincia.SelectedValue);
				dsAlmacen = dBAccess.ExecuteDataSet("CargaDistritos");
				cmbdistrito.DataSource = dsAlmacen.Tables[0];
				cmbdistrito.DisplayMember = "description";
				cmbdistrito.ValueMember = "id";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void cmbmodotranslado_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cmbmodotranslado.SelectedIndex == 1)
		{
			gbdatosconductor.Enabled = true;
			txtdocempresatransporte.Text = "";
			txtrazonsocialempresa.Text = "";
			txtdocconductor.Focus();
			return;
		}
		gbempresatransporte.Enabled = true;
		gbdatosconductor.Enabled = false;
		txtdocconductor.Text = "";
		txtrazonsocialconductor.Text = "";
		chkvehiculomenor.Checked = false;
		txtplaca.Text = "";
		txtlicencia.Text = "";
		txtdocempresatransporte.Focus();
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		NuevaDireccion();
		txtpuntollegada.Text = puntollegada.direccion;
	}

	private void NuevaDireccion()
	{
		frmPuntoLlegada frm = new frmPuntoLlegada();
		frm.ShowDialog();
		puntollegada = frm.puntollegada;
	}

	private void chkvehiculomenor_CheckedChanged(object sender, EventArgs e)
	{
		if (chkvehiculomenor.Checked)
		{
			cmbdocconductor.Enabled = false;
			txtdocconductor.Enabled = false;
			txtrazonsocialconductor.Enabled = false;
			txtapellidoconductor.Enabled = false;
			txtlicencia.Enabled = false;
		}
		else
		{
			cmbdocconductor.Enabled = true;
			txtdocconductor.Enabled = true;
			txtrazonsocialconductor.Enabled = true;
			txtapellidoconductor.Enabled = true;
			txtlicencia.Enabled = true;
		}
	}

	private void txtpuntollegada_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			NuevaDireccion();
			txtpuntollegada.Text = puntollegada.direccion;
		}
	}

	private void rgvdetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void rgvdetalle_CellValueChanged(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (!isAddingRow && e.Column.Name == "cantidad" && !Edition)
			{
				decimal cantidadPendiente = Convert.ToDecimal(rgvdetalle.Rows[e.RowIndex].Cells["cantidad_pendiente"].Value ?? ((object)0));
				decimal cantidad = Convert.ToDecimal(rgvdetalle.Rows[e.RowIndex].Cells["cantidad"].Value ?? ((object)0));
				if (cantidad <= 0m)
				{
					MessageBox.Show("La cantidad no puede ser 0 o menor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					rgvdetalle.Rows[e.RowIndex].Cells["cantidad"].Value = cantidadPendiente;
				}
				else if (cantidad > cantidadPendiente)
				{
					MessageBox.Show("La cantidad entrega no puede ser mayor que la cantidad pendiente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					rgvdetalle.Rows[e.RowIndex].Cells["cantidad"].Value = cantidadPendiente;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se ha producido un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnedidar_Click(object sender, EventArgs e)
	{
		foreach (GridViewDataColumn column in rgvdetalle.Columns)
		{
			if (column.Name != "cantidad")
			{
				column.ReadOnly = true;
			}
		}
		rgvdetalle.Columns["cantidad"].ReadOnly = false;
		AcionesEditar(estado: true);
		Edition = true;
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyData == Keys.Down)
		{
			dgvproductos.Focus();
			base.ActiveControl = dgvproductos;
			if (dgvproductos.Rows.Count > 0)
			{
				dgvproductos.Rows[0].Cells[codigo.Name].Selected = false;
				dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
				dgvproductos.CurrentCell = dgvproductos.CurrentRow.Cells[cant.Name];
			}
		}
		if (e.KeyData == Keys.Return)
		{
			string f = txtFiltro.Text.Trim();
		}
	}

	private void txtdocconductor_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmConductores form = new frmConductores();
			form.provieneguias = 1;
			form.tipodocumento = Convert.ToInt32(cmbdoctransportista.SelectedIndex);
			form.ShowDialog();
			cond = form.cond;
			if (Convert.ToInt32(cmbdoctransportista.SelectedIndex) == 0)
			{
				txtdocconductor.Text = cond.Dni;
			}
			else
			{
				txtdocconductor.Text = cond.Ruc;
			}
			txtrazonsocialconductor.Text = cond.Nombre;
		}
	}

	private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
	{
		buscar();
	}

	private void txtFiltro_Leave(object sender, EventArgs e)
	{
		dgvproductos.Focus();
		base.ActiveControl = dgvproductos;
	}

	private void dgvproductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (e.RowIndex != -1 && dgvproductos.Rows[e.RowIndex].Cells[unidad.Name].ColumnIndex == e.ColumnIndex && dgvproductos.CurrentCell != null)
			{
				setUnidades();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void buscar()
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			dgvproductos.AutoGenerateColumns = false;
			if (txtFiltro.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltro.Text.Trim() != "")
				{
					string filterCod = txtFiltro.Text.Trim();
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					int tipoDato = obtenerTipoDatoColumna(labelx.Text.Trim());
					string concatenador = "LIKE";
					string[] validadores = new string[2] { "'%", "%'" };
					if (tipoDato == 1)
					{
						concatenador = "=";
						validadores = new string[2] { "", "" };
						string[] array = cad;
						foreach (string c in array)
						{
							if (!decimal.TryParse(c, out var _))
							{
								Cursor = Cursors.Default;
								return;
							}
						}
					}
					if (cad.Count() > 1)
					{
						string[] array2 = cad;
						foreach (string c2 in array2)
						{
							if (cont == 1)
							{
								queries.Add(string.Format("[{0}] " + concatenador + " " + validadores[0] + "{1}" + validadores[1], labelx.Text.Trim(), c2));
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add(string.Format("[{0}] " + concatenador + " " + validadores[0] + "{1}" + validadores[1], labelx.Text.Trim(), c2));
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = string.Format("[{0}] " + concatenador + " " + validadores[0] + "{1}" + validadores[1], labelx.Text.Trim(), filterCod);
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			setUnidades();
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
			Cursor = Cursors.Default;
		}
	}

	private void CargaProductos(int codAlmacen)
	{
		Cursor = Cursors.WaitCursor;
		unidadesequi = clsuniequ.listar_unidad_equivalente(frmLogin.iCodAlmacen);
		dgvproductos.AutoGenerateColumns = false;
		dgvproductos.DataSource = null;
		dgvproductos.RowHeadersVisible = false;
		dgvproductos.DataSource = data;
		data.DataSource = AdmPro.RelacionSalidaTodoSinStock(1, codAlmacen, 1);
		data.Filter = string.Empty;
		filtro = string.Empty;
		if (dgvproductos.Rows.Count > 0)
		{
			dgvproductos.Rows[0].Cells[codigo.Name].Selected = false;
			dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
			dgvproductos.CurrentCell = dgvproductos.CurrentRow.Cells[cant.Name];
			dgvproductos.Rows[0].Cells[cant.Name].Selected = true;
		}
		dgvproductos.ClearSelection();
		dgvproductos.Focus();
		base.ActiveControl = dgvproductos;
		Cursor = Cursors.Default;
	}

	private void setUnidades()
	{
		d = null;
		if (dgvproductos.CurrentRow != null)
		{
			DataGridViewRow row = dgvproductos.CurrentRow;
			DataGridViewComboBoxCell a = (DataGridViewComboBoxCell)row.Cells["unidad"];
			var query = from x in unidadesequi.AsEnumerable()
				where x.Field<int>("codProducto").ToString() == row.Cells[codigo.Name].Value.ToString() && x.Field<int>("codstockalma").ToString() == row.Cells[codalma.Name].Value.ToString()
				select new
				{
					codUnidadEquivalente = x.Field<int>("codUnidadEquivalente"),
					codUnidadMedida = x.Field<int>("codUnidadMedida"),
					descripcion = x.Field<string>("descripcion"),
					precio = x.Field<decimal>("Precio")
				};
			var lista = query.ToList();
			a.DataSource = lista;
			a.DisplayMember = "descripcion";
			a.ValueMember = "codUnidadEquivalente";
			row.Cells["precio"].Value = decimal.Parse(lista[0].precio.ToString());
			row.Cells["codUnidadMedida"].Value = lista[0].codUnidadMedida.ToString();
			row.Cells["unidadnombre"].Value = lista[0].descripcion.ToString();
		}
	}

	private void dgvproductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label11.Text = dgvproductos.Columns[e.ColumnIndex].HeaderText;
		labelx.Text = dgvproductos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void dgvproductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvCombo != null)
			{
				dgvCombo.SelectedIndexChanged -= dvgCombo_SelectedIndexChanged;
			}
			if (dgvproductos.Rows.Count <= 0 || !(dgvproductos.Columns[e.ColumnIndex].Name == "cant") || dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["cant"].Value == null)
			{
				return;
			}
			if (dgvproductos.Rows[dgvproductos.CurrentCell.RowIndex].Cells["codunidadmedida"].Value != null)
			{
				int cod = Convert.ToInt32(dgvproductos.Rows[e.RowIndex].Cells[referencia.Name].Value);
				bool existe = false;
				foreach (GridViewRowInfo row in rgvdetalle.Rows)
				{
					string nombreColumna = "referencia";
					if (row.Cells[nombreColumna].Value != null && Convert.ToInt32(row.Cells[nombreColumna].Value) == cod)
					{
						existe = true;
						break;
					}
				}
				if (!existe)
				{
					foreach (DataGridViewRow row2 in (IEnumerable)dgvproductos.Rows)
					{
						try
						{
							if (row2.Index == dgvproductos.CurrentCell.RowIndex)
							{
								string TextUnidad = row2.Cells["unidadnombre"].Value.ToString();
								string[] partes = TextUnidad.Split('-');
								string Unidad = partes[0];
								decimal Cantidad = Convert.ToDecimal(row2.Cells["cant"].Value);
								decimal PrecioUnitario = Convert.ToDecimal(row2.Cells["precio"].Value);
								decimal TotalImporte = Cantidad * PrecioUnitario;
								decimal ValorVenta = TotalImporte / Convert.ToDecimal(1.18);
								decimal igv = ValorVenta * Convert.ToDecimal(0.18);
								GridViewRowInfo nuevaFila = rgvdetalle.Rows.AddNew();
								nuevaFila.Cells["referencia"].Value = row2.Cells["referencia"].Value;
								nuevaFila.Cells["producto"].Value = row2.Cells["descripcion"].Value;
								nuevaFila.Cells["cantidad_pendiente"].Value = Cantidad;
								nuevaFila.Cells["cantidad"].Value = Cantidad;
								nuevaFila.Cells["unidad"].Value = Unidad;
								nuevaFila.Cells["preciounitario"].Value = Math.Round(PrecioUnitario, 2);
								nuevaFila.Cells["importe"].Value = Math.Round(TotalImporte, 2);
								nuevaFila.Cells["codUnidadMedida"].Value = row2.Cells["codunidadmedida"].Value;
								nuevaFila.Cells["valorventa"].Value = Math.Round(ValorVenta, 2);
								nuevaFila.Cells["igv"].Value = Math.Round(igv, 2);
								nuevaFila.Cells["TotalCant"].Value = Cantidad;
								calculatotales();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString(), "OrdenCompraCotización", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					return;
				}
				MessageBox.Show("El producto ya se encuentra agregado, primero elimine y vuelva a agregarlo");
			}
			else
			{
				MessageBox.Show("Error al agregar producto");
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
	{
		Cursor = Cursors.WaitCursor;
		ok.enteros(e);
		if (e.KeyChar == '\r')
		{
			try
			{
				frmGestionCliente frmGC = new frmGestionCliente();
				switch (txtCodCliente.Text.Length)
				{
				case 1:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo un digito Ingresado", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 2:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo dos digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 3:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo tres digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 4:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cuatro digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 5:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo cinco digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 6:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo seis digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 7:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Solo siete digitos Ingresados", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 8:
					cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
					if (cli != null)
					{
						if (cli.Habilitado)
						{
							txtNombreCliente.Text = cli.RazonSocial;
							txtDireccion.Text = cli.DireccionLegal;
							break;
						}
						CargaDNI(txtCodCliente.Text);
						txtNombreCliente.Text = razonsocial;
						txtDireccion.Text = "-";
						ValidaCliente(txtCodCliente.Text);
					}
					else
					{
						CargaDNI(txtCodCliente.Text);
						txtNombreCliente.Text = razonsocial;
						txtDireccion.Text = "-";
						ValidaCliente(txtCodCliente.Text);
					}
					break;
				case 9:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso nueve digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case 10:
					MessageBox.Show("Ingrese Numero de Documento Valido" + Environment.NewLine + "Ingreso diez digitos ", "Consulta Documento", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtCodCliente.SelectAll();
					txtCodCliente.Focus();
					break;
				case 11:
					cli = AdmCli.ConsultaCliente(txtCodCliente.Text);
					if (cli != null)
					{
						cli = AdmCli.MuestraCliente(cli.CodCliente);
						if (cli != null)
						{
							if (cli.RazonSocial == "")
							{
								CargaRUC(txtCodCliente.Text);
								txtNombreCliente.Text = razonsocial;
								txtDireccion.Text = "-";
								ValidaCliente(txtCodCliente.Text);
							}
							else
							{
								txtNombreCliente.Text = cli.RazonSocial;
								txtDireccion.Text = cli.DireccionLegal;
							}
						}
						else
						{
							CargaRUC(txtCodCliente.Text);
							txtNombreCliente.Text = razonsocial;
							txtDireccion.Text = "-";
							ValidaCliente(txtCodCliente.Text);
						}
					}
					else
					{
						CargaRUC(txtCodCliente.Text);
						txtNombreCliente.Text = razonsocial;
						txtDireccion.Text = "-";
						ValidaCliente(txtCodCliente.Text);
					}
					break;
				}
				Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(ex.Message);
			}
		}
		Cursor = Cursors.Default;
	}

	private void ValidaCliente(string rucdni)
	{
		cli = AdmCli.ConsultaCliente(rucdni);
		if (cli == null)
		{
			cli = new clsCliente();
			int id = AdmCli.GetUltimoId() + 1;
			if (rucdni.Length == 8)
			{
				cli.Dni = rucdni;
				cli.DireccionEntrega = "-";
				cli.DireccionLegal = "-";
			}
			else if (rucdni.Length == 11)
			{
				cli.Ruc = rucdni;
				cli.DireccionEntrega = txtDireccion.Text;
				cli.DireccionLegal = txtDireccion.Text;
			}
			cli.Nombre = txtNombreCliente.Text;
			cli.RazonSocial = txtNombreCliente.Text;
			cli.CodigoPersonalizado = "C" + id.ToString().PadLeft(6, '0').Trim();
			cli.FormaPago = 6;
			cli.Moneda = 1;
			cli.LineaCredito = 0m;
			cli.LineaCreditoDisponible = 0m;
			cli.Habilitado = true;
			cli.CodUser = frmLogin.iCodUser;
			cli.CodCliente = id;
			if (AdmCli.insert(cli))
			{
				AdmCli.updatecategoria(id, 6);
			}
		}
	}

	private int obtenerTipoDatoColumna(string nombreColumna)
	{
		Type tipo = dgvproductos.Columns[nombreColumna].ValueType;
		return ((object)tipo == null) ? 1 : ((!(tipo.Name == "String")) ? 1 : 2);
	}

	private void rgvdetalle_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Delete && proceso == 1 && codventa == 0)
		{
			calculatotales();
		}
	}

	private void txtCodCliente_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.F1)
			{
				return;
			}
			if (Application.OpenForms["frmClientesLista"] != null)
			{
				Application.OpenForms["frmClientesLista"].Activate();
				return;
			}
			frmClientesLista form = new frmClientesLista();
			form.Proceso = 3;
			form.ShowDialog();
			cli = form.cli;
			CodCliente = cli.CodCliente;
			if (CodCliente != 0)
			{
				txtCodCliente.Text = "";
				txtDireccion.Text = "";
				txtNombreCliente.Text = "";
				CargaCliente();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaCliente()
	{
		try
		{
			cli = AdmCli.MuestraCliente(CodCliente);
			txtCodCliente.Text = cli.RucDni;
			txtNombreCliente.Text = cli.RazonSocial;
			txtDireccion.Text = cli.DireccionLegal;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvproductos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		try
		{
			dgvCombo = e.Control as DataGridViewComboBoxEditingControl;
			if (dgvCombo != null)
			{
				dgvCombo.SelectedIndexChanged -= dvgCombo_SelectedIndexChanged;
				dgvCombo.SelectedIndexChanged += dvgCombo_SelectedIndexChanged;
			}
			if (!(e.Control is ComboBox))
			{
				DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
				dText.KeyPress -= dText_KeyPress;
				dText.KeyPress += dText_KeyPress;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void dText_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsDigit(e.KeyChar) && !char.IsNumber(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
		if (e.KeyChar == '-')
		{
			e.Handled = true;
		}
		if (e.KeyChar == ' ')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.Length == 0)
		{
			e.Handled = true;
		}
	}

	private void dvgCombo_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox combo = sender as ComboBox;
		try
		{
			d = unidadesequi;
			if (d != null && combo.SelectedValue != null && combo.SelectedIndex != -1 && combo.SelectedValue.ToString() != "System.Data.DataRowView" && dgvproductos.CurrentCell != null)
			{
				EnumerableRowCollection<decimal> a = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString()
					select x.Field<decimal>("Precio");
				EnumerableRowCollection<string> b = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString()
					select x.Field<string>("descripcion");
				EnumerableRowCollection<int> c = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString()
					select x.Field<int>("codunidadmedida");
				EnumerableRowCollection<decimal> de = from x in d.AsEnumerable()
					where x.Field<int>("codUnidadEquivalente").ToString() == combo.SelectedValue.ToString() && x.Field<int>("codstockalma") == Convert.ToInt32(dgvproductos.CurrentRow.Cells[codalma.Name].Value)
					select x.Field<decimal>("stockfactor");
				if (a.Any())
				{
					dgvproductos.CurrentRow.Cells["precio"].Value = a.ToList()[0];
					dgvproductos.CurrentRow.Cells["unidadnombre"].Value = b.ToList()[0].ToString();
					dgvproductos.CurrentRow.Cells["codunidadmedida"].Value = c.ToList()[0].ToString();
					dgvproductos.CurrentRow.Cells["stockdisponible"].Value = $"{de.ToList()[0]:0.00}";
				}
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void rgvdetalle_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
	{
		if (e.Row is GridViewNewRowInfo)
		{
			isAddingRow = true;
		}
	}

	private void rgvdetalle_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		if (e.Row is GridViewNewRowInfo)
		{
			isAddingRow = false;
		}
		if (e.Column.Name == "producto" || e.Column.Name == "unidad")
		{
			e.Row.Cells[e.Column.Name].Value = e.Row.Cells[e.Column.Name].Value?.ToString().ToUpper();
		}
	}

	private void rgvdetalle_RowValidated(object sender, RowValidatedEventArgs e)
	{
		if (e.Row is GridViewNewRowInfo)
		{
			ValidateAndCalculate(e.Row);
			decimal cantidad = Convert.ToDecimal(e.Row.Cells["cantidad"].Value ?? ((object)0));
			e.Row.Cells["referencia"].Value = 0;
			e.Row.Cells["cantidad_pendiente"].Value = cantidad;
			e.Row.Cells["totalcant"].Value = cantidad;
		}
	}

	private void ValidateAndCalculate(GridViewRowInfo row)
	{
		decimal cantidad = Convert.ToDecimal(row.Cells["cantidad"].Value ?? ((object)0));
		decimal precio = Convert.ToDecimal(row.Cells["preciounitario"].Value ?? ((object)0));
		decimal total = cantidad * precio;
		row.Cells["importe"].Value = total;
	}

	private void rgvdetalle_UserAddedRow(object sender, GridViewRowEventArgs e)
	{
		calculatotales();
	}

	public void calculatotales()
	{
		double total = 0.0;
		foreach (GridViewRowInfo row in rgvdetalle.Rows)
		{
			double Importe = Convert.ToDouble(row.Cells["importe"].Value);
			total += Importe;
		}
		txtPrecioVenta.Text = $"{total:#,##0.00}";
		txtValorVenta.Text = $"{total / 1.18:#,##0.00}";
		txtIGV.Text = $"{Convert.ToDouble(txtValorVenta.Text) * 0.18:#,##0.00}";
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
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGuiaFacturacion));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rgvdetalle = new Telerik.WinControls.UI.RadGridView();
		this.rgvproductos = new Telerik.WinControls.UI.RadGridView();
		this.Acciones = new System.Windows.Forms.GroupBox();
		this.btnedidar = new Telerik.WinControls.UI.RadButton();
		this.chbafectacion = new System.Windows.Forms.CheckBox();
		this.btnimprimir = new Telerik.WinControls.UI.RadButton();
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		this.btnsalir = new Telerik.WinControls.UI.RadButton();
		this.gbdatoscliente = new System.Windows.Forms.GroupBox();
		this.labelX6 = new DevComponents.DotNetBar.LabelX();
		this.txtreferencia = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX7 = new DevComponents.DotNetBar.LabelX();
		this.cmbMoneda = new Telerik.WinControls.UI.RadDropDownList();
		this.labelX3 = new DevComponents.DotNetBar.LabelX();
		this.labelX2 = new DevComponents.DotNetBar.LabelX();
		this.labelX1 = new DevComponents.DotNetBar.LabelX();
		this.txtDireccion = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtNombreCliente = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtCodCliente = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.labelX5 = new DevComponents.DotNetBar.LabelX();
		this.labelX4 = new DevComponents.DotNetBar.LabelX();
		this.txtNombreVendedor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtCodigoVendedor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.gbtotales = new System.Windows.Forms.GroupBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtPrecioVenta = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label9 = new System.Windows.Forms.Label();
		this.txtIGV = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label8 = new System.Windows.Forms.Label();
		this.txtValorVenta = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label24 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.textBoxX5 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.textBoxX6 = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.gbempresatransporte = new System.Windows.Forms.GroupBox();
		this.labelX11 = new DevComponents.DotNetBar.LabelX();
		this.cmbdoctransportista = new System.Windows.Forms.ComboBox();
		this.lblrazonsocialempresa = new DevComponents.DotNetBar.LabelX();
		this.labelX12 = new DevComponents.DotNetBar.LabelX();
		this.txtrazonsocialempresa = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtdocempresatransporte = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.gbdatosconductor = new System.Windows.Forms.GroupBox();
		this.labelX28 = new DevComponents.DotNetBar.LabelX();
		this.txtapellidoconductor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.label1 = new System.Windows.Forms.Label();
		this.chkvehiculomenor = new System.Windows.Forms.CheckBox();
		this.labelX22 = new DevComponents.DotNetBar.LabelX();
		this.txtlicencia = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX21 = new DevComponents.DotNetBar.LabelX();
		this.cmbdocconductor = new System.Windows.Forms.ComboBox();
		this.labelX18 = new DevComponents.DotNetBar.LabelX();
		this.labelX19 = new DevComponents.DotNetBar.LabelX();
		this.txtrazonsocialconductor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.txtdocconductor = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX8 = new DevComponents.DotNetBar.LabelX();
		this.txtplaca = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.gbpuntos = new System.Windows.Forms.GroupBox();
		this.txtpuntollegada = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.labelX25 = new DevComponents.DotNetBar.LabelX();
		this.labelX24 = new DevComponents.DotNetBar.LabelX();
		this.labelX23 = new DevComponents.DotNetBar.LabelX();
		this.cmbdistrito = new System.Windows.Forms.ComboBox();
		this.cmbprovincia = new System.Windows.Forms.ComboBox();
		this.cmbdepartamento = new System.Windows.Forms.ComboBox();
		this.labelX13 = new DevComponents.DotNetBar.LabelX();
		this.labelX10 = new DevComponents.DotNetBar.LabelX();
		this.txtpuntopartida = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX20 = new DevComponents.DotNetBar.LabelX();
		this.txtglosa = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX9 = new DevComponents.DotNetBar.LabelX();
		this.txtdescripciontranslado = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.gbmotivotranslado = new System.Windows.Forms.GroupBox();
		this.labelX17 = new DevComponents.DotNetBar.LabelX();
		this.labelX16 = new DevComponents.DotNetBar.LabelX();
		this.cmbmotivotranslado = new System.Windows.Forms.ComboBox();
		this.dtfechatranslado = new System.Windows.Forms.DateTimePicker();
		this.labelX14 = new DevComponents.DotNetBar.LabelX();
		this.cmbmodotranslado = new System.Windows.Forms.ComboBox();
		this.gbdatosgenerales = new System.Windows.Forms.GroupBox();
		this.labelX15 = new DevComponents.DotNetBar.LabelX();
		this.cmbserie = new System.Windows.Forms.ComboBox();
		this.labelX26 = new DevComponents.DotNetBar.LabelX();
		this.txtpesobruto = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX27 = new DevComponents.DotNetBar.LabelX();
		this.txtnropallets = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.lbloc = new DevComponents.DotNetBar.LabelX();
		this.txtnumoc = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.labelX29 = new DevComponents.DotNetBar.LabelX();
		this.txtflete = new DevComponents.DotNetBar.Controls.TextBoxX();
		this.gbbucaproducto = new System.Windows.Forms.GroupBox();
		this.labelx = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.gbproductos = new System.Windows.Forms.GroupBox();
		this.dgvproductos = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUniversal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nomAlma = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.nmarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockdisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidadmedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codsunatimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codtimpuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codalma = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidadnombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.icbper = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codlinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvproductos).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvproductos.MasterTemplate).BeginInit();
		this.Acciones.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btnedidar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnimprimir).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).BeginInit();
		this.gbdatoscliente.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda).BeginInit();
		this.gbtotales.SuspendLayout();
		this.gbempresatransporte.SuspendLayout();
		this.gbdatosconductor.SuspendLayout();
		this.gbpuntos.SuspendLayout();
		this.groupBox9.SuspendLayout();
		this.gbmotivotranslado.SuspendLayout();
		this.gbdatosgenerales.SuspendLayout();
		this.gbbucaproducto.SuspendLayout();
		this.gbproductos.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvproductos).BeginInit();
		base.SuspendLayout();
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox2.Controls.Add(this.rgvdetalle);
		this.groupBox2.Location = new System.Drawing.Point(579, 381);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(617, 218);
		this.groupBox2.TabIndex = 1;
		this.groupBox2.TabStop = false;
		this.rgvdetalle.BackColor = System.Drawing.Color.Snow;
		this.rgvdetalle.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvdetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvdetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.rgvdetalle.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvdetalle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvdetalle.Location = new System.Drawing.Point(3, 16);
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "referencia";
		gridViewTextBoxColumn1.HeaderText = "Referencia";
		gridViewTextBoxColumn1.Name = "referencia";
		gridViewTextBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn1.Width = 100;
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "producto";
		gridViewTextBoxColumn2.HeaderText = "Descripcion";
		gridViewTextBoxColumn2.Name = "producto";
		gridViewTextBoxColumn2.Width = 350;
		gridViewTextBoxColumn3.FieldName = "cantidad_pendiente";
		gridViewTextBoxColumn3.HeaderText = "Pendiente";
		gridViewTextBoxColumn3.Name = "cantidad_pendiente";
		gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn3.Width = 100;
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.FieldName = "cantidad";
		gridViewTextBoxColumn4.HeaderText = "Cantidad";
		gridViewTextBoxColumn4.Name = "cantidad";
		gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn4.Width = 100;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.FieldName = "unidad";
		gridViewTextBoxColumn5.HeaderText = "U.Medidad";
		gridViewTextBoxColumn5.Name = "unidad";
		gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn5.Width = 100;
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "preciounitario";
		gridViewTextBoxColumn6.HeaderText = "P.Unitario";
		gridViewTextBoxColumn6.Name = "preciounitario";
		gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn6.Width = 150;
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.FieldName = "importe";
		gridViewTextBoxColumn7.HeaderText = "Total";
		gridViewTextBoxColumn7.Name = "importe";
		gridViewTextBoxColumn7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn7.Width = 100;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn8.HeaderText = "codunidad";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "codUnidadMedida";
		gridViewTextBoxColumn8.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn8.Width = 100;
		gridViewTextBoxColumn9.EnableExpressionEditor = false;
		gridViewTextBoxColumn9.FieldName = "valorventa";
		gridViewTextBoxColumn9.HeaderText = "V.Venta";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "valorventa";
		gridViewTextBoxColumn9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn9.Width = 100;
		gridViewTextBoxColumn10.EnableExpressionEditor = false;
		gridViewTextBoxColumn10.FieldName = "igv";
		gridViewTextBoxColumn10.HeaderText = "Igv";
		gridViewTextBoxColumn10.IsVisible = false;
		gridViewTextBoxColumn10.Name = "igv";
		gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn10.Width = 100;
		gridViewTextBoxColumn11.FieldName = "TotalCant";
		gridViewTextBoxColumn11.HeaderText = "TotalCant";
		gridViewTextBoxColumn11.Name = "TotalCant";
		gridViewTextBoxColumn11.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn11.Width = 80;
		this.rgvdetalle.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11);
		this.rgvdetalle.MasterTemplate.EnableFiltering = true;
		this.rgvdetalle.MasterTemplate.EnableGrouping = false;
		this.rgvdetalle.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvdetalle.Name = "rgvdetalle";
		this.rgvdetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvdetalle.ShowGroupPanel = false;
		this.rgvdetalle.Size = new System.Drawing.Size(611, 199);
		this.rgvdetalle.TabIndex = 0;
		this.rgvdetalle.ThemeName = "TelerikMetroTouch";
		this.rgvdetalle.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(rgvdetalle_CellBeginEdit);
		this.rgvdetalle.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle_CellEndEdit);
		this.rgvdetalle.RowValidated += new Telerik.WinControls.UI.RowValidatedEventHandler(rgvdetalle_RowValidated);
		this.rgvdetalle.UserAddedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(rgvdetalle_UserAddedRow);
		this.rgvdetalle.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvdetalle_CellValueChanged);
		this.rgvdetalle.KeyDown += new System.Windows.Forms.KeyEventHandler(rgvdetalle_KeyDown);
		this.rgvdetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(rgvdetalle_KeyUp);
		this.rgvproductos.BackColor = System.Drawing.Color.Snow;
		this.rgvproductos.Cursor = System.Windows.Forms.Cursors.Default;
		this.rgvproductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvproductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.rgvproductos.ForeColor = System.Drawing.SystemColors.ControlText;
		this.rgvproductos.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rgvproductos.Location = new System.Drawing.Point(3, 16);
		gridViewTextBoxColumn12.EnableExpressionEditor = false;
		gridViewTextBoxColumn12.FieldName = "referencia";
		gridViewTextBoxColumn12.HeaderText = "Referencia";
		gridViewTextBoxColumn12.Name = "referencia";
		gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn12.Width = 100;
		gridViewTextBoxColumn13.EnableExpressionEditor = false;
		gridViewTextBoxColumn13.FieldName = "producto";
		gridViewTextBoxColumn13.HeaderText = "Descripcion";
		gridViewTextBoxColumn13.Name = "producto";
		gridViewTextBoxColumn13.Width = 250;
		gridViewTextBoxColumn14.EnableExpressionEditor = false;
		gridViewTextBoxColumn14.FieldName = "cantidad";
		gridViewTextBoxColumn14.HeaderText = "Cantidad";
		gridViewTextBoxColumn14.Name = "cantidad";
		gridViewTextBoxColumn14.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn14.Width = 100;
		gridViewTextBoxColumn15.FieldName = "preciounitario";
		gridViewTextBoxColumn15.HeaderText = "P.Unitario";
		gridViewTextBoxColumn15.Name = "preciounitario";
		gridViewTextBoxColumn15.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn15.Width = 150;
		gridViewTextBoxColumn16.FieldName = "codUnidadMedida";
		gridViewTextBoxColumn16.HeaderText = "codunidad";
		gridViewTextBoxColumn16.Name = "codUnidadMedida";
		gridViewTextBoxColumn16.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn16.Width = 100;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "unidad";
		gridViewTextBoxColumn17.HeaderText = "U.Medidad";
		gridViewTextBoxColumn17.Name = "unidad";
		gridViewTextBoxColumn17.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn17.Width = 100;
		gridViewTextBoxColumn18.EnableExpressionEditor = false;
		gridViewTextBoxColumn18.FieldName = "valorventa";
		gridViewTextBoxColumn18.HeaderText = "V.Venta";
		gridViewTextBoxColumn18.Name = "valorventa";
		gridViewTextBoxColumn18.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn18.Width = 100;
		gridViewTextBoxColumn19.EnableExpressionEditor = false;
		gridViewTextBoxColumn19.FieldName = "igv";
		gridViewTextBoxColumn19.HeaderText = "Igv";
		gridViewTextBoxColumn19.Name = "igv";
		gridViewTextBoxColumn19.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn19.Width = 100;
		gridViewTextBoxColumn20.EnableExpressionEditor = false;
		gridViewTextBoxColumn20.FieldName = "importe";
		gridViewTextBoxColumn20.HeaderText = "Total";
		gridViewTextBoxColumn20.Name = "importe";
		gridViewTextBoxColumn20.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		gridViewTextBoxColumn20.Width = 100;
		this.rgvproductos.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17, gridViewTextBoxColumn18, gridViewTextBoxColumn19, gridViewTextBoxColumn20);
		this.rgvproductos.MasterTemplate.EnableFiltering = true;
		this.rgvproductos.MasterTemplate.EnableGrouping = false;
		this.rgvproductos.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.rgvproductos.Name = "rgvproductos";
		this.rgvproductos.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.rgvproductos.ShowGroupPanel = false;
		this.rgvproductos.Size = new System.Drawing.Size(752, 564);
		this.rgvproductos.TabIndex = 0;
		this.rgvproductos.ThemeName = "TelerikMetroTouch";
		this.Acciones.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Acciones.Controls.Add(this.btnedidar);
		this.Acciones.Controls.Add(this.chbafectacion);
		this.Acciones.Controls.Add(this.btnimprimir);
		this.Acciones.Controls.Add(this.btnguardar);
		this.Acciones.Controls.Add(this.btnsalir);
		this.Acciones.Location = new System.Drawing.Point(39, 597);
		this.Acciones.Name = "Acciones";
		this.Acciones.Size = new System.Drawing.Size(1157, 62);
		this.Acciones.TabIndex = 1;
		this.Acciones.TabStop = false;
		this.Acciones.Text = "Acciones";
		this.btnedidar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnedidar.Enabled = false;
		this.btnedidar.Image = SIGEFA.Properties.Resources.edit_property_32px;
		this.btnedidar.Location = new System.Drawing.Point(867, 15);
		this.btnedidar.Name = "btnedidar";
		this.btnedidar.Size = new System.Drawing.Size(99, 38);
		this.btnedidar.TabIndex = 2;
		this.btnedidar.Text = "Editar";
		this.btnedidar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnedidar.ThemeName = "TelerikMetroTouch";
		this.btnedidar.Click += new System.EventHandler(btnedidar_Click);
		this.chbafectacion.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.chbafectacion.AutoSize = true;
		this.chbafectacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chbafectacion.Location = new System.Drawing.Point(622, 36);
		this.chbafectacion.Name = "chbafectacion";
		this.chbafectacion.Size = new System.Drawing.Size(130, 17);
		this.chbafectacion.TabIndex = 56;
		this.chbafectacion.Text = "Ocultar Valorizado";
		this.chbafectacion.UseVisualStyleBackColor = true;
		this.btnimprimir.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnimprimir.Enabled = false;
		this.btnimprimir.Image = SIGEFA.Properties.Resources.printer;
		this.btnimprimir.Location = new System.Drawing.Point(758, 15);
		this.btnimprimir.Name = "btnimprimir";
		this.btnimprimir.Size = new System.Drawing.Size(103, 38);
		this.btnimprimir.TabIndex = 1;
		this.btnimprimir.Text = "Imprimir";
		this.btnimprimir.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnimprimir.ThemeName = "TelerikMetroTouch";
		this.btnimprimir.Click += new System.EventHandler(btnimprimir_Click);
		this.btnguardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnguardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnguardar.Location = new System.Drawing.Point(972, 15);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(99, 38);
		this.btnguardar.TabIndex = 1;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnguardar.ThemeName = "TelerikMetroTouch";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		this.btnsalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnsalir.Image = (System.Drawing.Image)resources.GetObject("btnsalir.Image");
		this.btnsalir.Location = new System.Drawing.Point(1077, 15);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(74, 38);
		this.btnsalir.TabIndex = 0;
		this.btnsalir.Text = "Salir";
		this.btnsalir.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
		this.btnsalir.ThemeName = "TelerikMetroTouch";
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.gbdatoscliente.Controls.Add(this.labelX6);
		this.gbdatoscliente.Controls.Add(this.txtreferencia);
		this.gbdatoscliente.Controls.Add(this.labelX7);
		this.gbdatoscliente.Controls.Add(this.cmbMoneda);
		this.gbdatoscliente.Controls.Add(this.labelX3);
		this.gbdatoscliente.Controls.Add(this.labelX2);
		this.gbdatoscliente.Controls.Add(this.labelX1);
		this.gbdatoscliente.Controls.Add(this.txtDireccion);
		this.gbdatoscliente.Controls.Add(this.txtNombreCliente);
		this.gbdatoscliente.Controls.Add(this.txtCodCliente);
		this.gbdatoscliente.Enabled = false;
		this.gbdatoscliente.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbdatoscliente.Location = new System.Drawing.Point(34, 116);
		this.gbdatoscliente.Name = "gbdatoscliente";
		this.gbdatoscliente.Size = new System.Drawing.Size(371, 158);
		this.gbdatoscliente.TabIndex = 24;
		this.gbdatoscliente.TabStop = false;
		this.gbdatoscliente.Text = "DATOS DEL CLIENTE";
		this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX6.Location = new System.Drawing.Point(5, 119);
		this.labelX6.Name = "labelX6";
		this.labelX6.Size = new System.Drawing.Size(68, 14);
		this.labelX6.TabIndex = 135;
		this.labelX6.Text = "Referencia:";
		this.txtreferencia.Border.Class = "TextBoxBorder";
		this.txtreferencia.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtreferencia.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtreferencia.ForeColor = System.Drawing.Color.Red;
		this.txtreferencia.Location = new System.Drawing.Point(73, 115);
		this.txtreferencia.Name = "txtreferencia";
		this.txtreferencia.PreventEnterBeep = true;
		this.txtreferencia.Size = new System.Drawing.Size(252, 25);
		this.txtreferencia.TabIndex = 134;
		this.txtreferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX7.Enabled = false;
		this.labelX7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX7.Location = new System.Drawing.Point(200, 13);
		this.labelX7.Name = "labelX7";
		this.labelX7.Size = new System.Drawing.Size(47, 28);
		this.labelX7.TabIndex = 133;
		this.labelX7.Text = "Moneda:";
		this.labelX7.WordWrap = true;
		this.cmbMoneda.BackColor = System.Drawing.SystemColors.ControlLightLight;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.Location = new System.Drawing.Point(255, 16);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.RootElement.ControlBounds = new System.Drawing.Rectangle(255, 16, 125, 20);
		this.cmbMoneda.RootElement.StretchVertically = true;
		this.cmbMoneda.Size = new System.Drawing.Size(87, 24);
		this.cmbMoneda.TabIndex = 132;
		this.cmbMoneda.ThemeName = "TelerikMetroBlue";
		this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX3.Location = new System.Drawing.Point(12, 76);
		this.labelX3.Name = "labelX3";
		this.labelX3.Size = new System.Drawing.Size(55, 28);
		this.labelX3.TabIndex = 127;
		this.labelX3.Text = "Direccion:";
		this.labelX3.WordWrap = true;
		this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX2.Location = new System.Drawing.Point(23, 53);
		this.labelX2.Name = "labelX2";
		this.labelX2.Size = new System.Drawing.Size(48, 11);
		this.labelX2.TabIndex = 126;
		this.labelX2.Text = "Cliente:";
		this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX1.Enabled = false;
		this.labelX1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX1.Location = new System.Drawing.Point(18, 16);
		this.labelX1.Name = "labelX1";
		this.labelX1.Size = new System.Drawing.Size(47, 28);
		this.labelX1.TabIndex = 125;
		this.labelX1.Text = "Ruc/Dni   (F1):";
		this.labelX1.WordWrap = true;
		this.txtDireccion.Border.Class = "TextBoxBorder";
		this.txtDireccion.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtDireccion.Location = new System.Drawing.Point(73, 74);
		this.txtDireccion.Multiline = true;
		this.txtDireccion.Name = "txtDireccion";
		this.txtDireccion.PreventEnterBeep = true;
		this.txtDireccion.Size = new System.Drawing.Size(252, 35);
		this.txtDireccion.TabIndex = 5;
		this.txtNombreCliente.Border.Class = "TextBoxBorder";
		this.txtNombreCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreCliente.Location = new System.Drawing.Point(73, 46);
		this.txtNombreCliente.Name = "txtNombreCliente";
		this.txtNombreCliente.PreventEnterBeep = true;
		this.txtNombreCliente.Size = new System.Drawing.Size(252, 22);
		this.txtNombreCliente.TabIndex = 3;
		this.txtCodCliente.Border.Class = "TextBoxBorder";
		this.txtCodCliente.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtCodCliente.Location = new System.Drawing.Point(73, 18);
		this.txtCodCliente.Name = "txtCodCliente";
		this.txtCodCliente.PreventEnterBeep = true;
		this.txtCodCliente.Size = new System.Drawing.Size(119, 22);
		this.txtCodCliente.TabIndex = 1;
		this.txtCodCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodCliente_KeyPress);
		this.txtCodCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(txtCodCliente_KeyUp);
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(247, 22);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(97, 22);
		this.dtpFecha.TabIndex = 131;
		this.dtpFecha.Tag = "16";
		this.dtpFecha.Value = new System.DateTime(2019, 5, 23, 0, 0, 0, 0);
		this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX5.Location = new System.Drawing.Point(158, 26);
		this.labelX5.Name = "labelX5";
		this.labelX5.Size = new System.Drawing.Size(89, 14);
		this.labelX5.TabIndex = 129;
		this.labelX5.Text = "Fecha Emisión:";
		this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX4.Location = new System.Drawing.Point(33, 58);
		this.labelX4.Name = "labelX4";
		this.labelX4.Size = new System.Drawing.Size(60, 33);
		this.labelX4.TabIndex = 128;
		this.labelX4.Text = "Vendedor: (Alt+V)";
		this.labelX4.WordWrap = true;
		this.txtNombreVendedor.Border.Class = "TextBoxBorder";
		this.txtNombreVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtNombreVendedor.Location = new System.Drawing.Point(156, 61);
		this.txtNombreVendedor.Name = "txtNombreVendedor";
		this.txtNombreVendedor.PreventEnterBeep = true;
		this.txtNombreVendedor.Size = new System.Drawing.Size(191, 22);
		this.txtNombreVendedor.TabIndex = 123;
		this.txtNombreVendedor.Text = "<--  SELECCIONE UN VENDEDOR";
		this.txtCodigoVendedor.Border.Class = "TextBoxBorder";
		this.txtCodigoVendedor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtCodigoVendedor.Location = new System.Drawing.Point(95, 61);
		this.txtCodigoVendedor.Name = "txtCodigoVendedor";
		this.txtCodigoVendedor.PreventEnterBeep = true;
		this.txtCodigoVendedor.Size = new System.Drawing.Size(54, 22);
		this.txtCodigoVendedor.TabIndex = 122;
		this.gbtotales.Controls.Add(this.label13);
		this.gbtotales.Controls.Add(this.txtPrecioVenta);
		this.gbtotales.Controls.Add(this.label9);
		this.gbtotales.Controls.Add(this.txtIGV);
		this.gbtotales.Controls.Add(this.label8);
		this.gbtotales.Controls.Add(this.txtValorVenta);
		this.gbtotales.Enabled = false;
		this.gbtotales.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbtotales.Location = new System.Drawing.Point(34, 275);
		this.gbtotales.Name = "gbtotales";
		this.gbtotales.Size = new System.Drawing.Size(371, 76);
		this.gbtotales.TabIndex = 25;
		this.gbtotales.TabStop = false;
		this.gbtotales.Text = "TOTALES";
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(17, 22);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(48, 13);
		this.label13.TabIndex = 11;
		this.label13.Text = "V.Venta:";
		this.txtPrecioVenta.BackColor = System.Drawing.Color.AliceBlue;
		this.txtPrecioVenta.Border.Class = "TextBoxBorder";
		this.txtPrecioVenta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtPrecioVenta.Enabled = false;
		this.txtPrecioVenta.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVenta.Location = new System.Drawing.Point(234, 33);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.PreventEnterBeep = true;
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(110, 25);
		this.txtPrecioVenta.TabIndex = 10;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
		this.label9.Location = new System.Drawing.Point(163, 29);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(68, 25);
		this.label9.TabIndex = 9;
		this.label9.Text = "TOTAL";
		this.txtIGV.BackColor = System.Drawing.Color.AliceBlue;
		this.txtIGV.Border.Class = "TextBoxBorder";
		this.txtIGV.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtIGV.Enabled = false;
		this.txtIGV.Location = new System.Drawing.Point(71, 43);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.PreventEnterBeep = true;
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(86, 22);
		this.txtIGV.TabIndex = 8;
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(30, 46);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(29, 13);
		this.label8.TabIndex = 7;
		this.label8.Text = "Igv :";
		this.txtValorVenta.BackColor = System.Drawing.Color.AliceBlue;
		this.txtValorVenta.Border.Class = "TextBoxBorder";
		this.txtValorVenta.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtValorVenta.Enabled = false;
		this.txtValorVenta.Location = new System.Drawing.Point(71, 18);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.PreventEnterBeep = true;
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(86, 22);
		this.txtValorVenta.TabIndex = 6;
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Segoe UI Black", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.Location = new System.Drawing.Point(480, 17);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(15, 19);
		this.label24.TabIndex = 49;
		this.label24.Text = "-";
		this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label24.Visible = false;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Segoe UI Black", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.Location = new System.Drawing.Point(161, 20);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(15, 19);
		this.label22.TabIndex = 140;
		this.label22.Text = "-";
		this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.textBoxX5.Border.Class = "TextBoxBorder";
		this.textBoxX5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX5.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX5.Location = new System.Drawing.Point(501, 14);
		this.textBoxX5.Multiline = true;
		this.textBoxX5.Name = "textBoxX5";
		this.textBoxX5.PreventEnterBeep = true;
		this.textBoxX5.Size = new System.Drawing.Size(117, 24);
		this.textBoxX5.TabIndex = 45;
		this.textBoxX5.Text = "00000000";
		this.textBoxX5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBoxX1.Border.Class = "TextBoxBorder";
		this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX1.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX1.Location = new System.Drawing.Point(182, 17);
		this.textBoxX1.Name = "textBoxX1";
		this.textBoxX1.PreventEnterBeep = true;
		this.textBoxX1.ReadOnly = true;
		this.textBoxX1.Size = new System.Drawing.Size(117, 25);
		this.textBoxX1.TabIndex = 139;
		this.textBoxX1.Text = "00000000000";
		this.textBoxX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBoxX2.Border.Class = "TextBoxBorder";
		this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX2.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX2.Location = new System.Drawing.Point(82, 17);
		this.textBoxX2.Multiline = true;
		this.textBoxX2.Name = "textBoxX2";
		this.textBoxX2.PreventEnterBeep = true;
		this.textBoxX2.ReadOnly = true;
		this.textBoxX2.Size = new System.Drawing.Size(68, 30);
		this.textBoxX2.TabIndex = 138;
		this.textBoxX2.Text = "000";
		this.textBoxX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.textBoxX6.Border.Class = "TextBoxBorder";
		this.textBoxX6.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.textBoxX6.Font = new System.Drawing.Font("Segoe UI", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.textBoxX6.Location = new System.Drawing.Point(401, 14);
		this.textBoxX6.Multiline = true;
		this.textBoxX6.Name = "textBoxX6";
		this.textBoxX6.PreventEnterBeep = true;
		this.textBoxX6.Size = new System.Drawing.Size(68, 23);
		this.textBoxX6.TabIndex = 0;
		this.textBoxX6.Text = "000";
		this.textBoxX6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.gbempresatransporte.Controls.Add(this.labelX11);
		this.gbempresatransporte.Controls.Add(this.cmbdoctransportista);
		this.gbempresatransporte.Controls.Add(this.lblrazonsocialempresa);
		this.gbempresatransporte.Controls.Add(this.labelX12);
		this.gbempresatransporte.Controls.Add(this.txtrazonsocialempresa);
		this.gbempresatransporte.Controls.Add(this.txtdocempresatransporte);
		this.gbempresatransporte.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbempresatransporte.Location = new System.Drawing.Point(409, 197);
		this.gbempresatransporte.Name = "gbempresatransporte";
		this.gbempresatransporte.Size = new System.Drawing.Size(344, 118);
		this.gbempresatransporte.TabIndex = 134;
		this.gbempresatransporte.TabStop = false;
		this.gbempresatransporte.Text = "DATOS TRANSPORTISTA";
		this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX11.Location = new System.Drawing.Point(20, 21);
		this.labelX11.Name = "labelX11";
		this.labelX11.Size = new System.Drawing.Size(56, 21);
		this.labelX11.TabIndex = 133;
		this.labelX11.Text = "Tipo Doc:";
		this.cmbdoctransportista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbdoctransportista.Enabled = false;
		this.cmbdoctransportista.FormattingEnabled = true;
		this.cmbdoctransportista.Items.AddRange(new object[2] { "Dni", "Ruc" });
		this.cmbdoctransportista.Location = new System.Drawing.Point(99, 21);
		this.cmbdoctransportista.Name = "cmbdoctransportista";
		this.cmbdoctransportista.Size = new System.Drawing.Size(132, 21);
		this.cmbdoctransportista.TabIndex = 132;
		this.cmbdoctransportista.SelectedIndexChanged += new System.EventHandler(cmbdoctransportista_SelectedIndexChanged);
		this.lblrazonsocialempresa.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lblrazonsocialempresa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.lblrazonsocialempresa.Location = new System.Drawing.Point(23, 84);
		this.lblrazonsocialempresa.Name = "lblrazonsocialempresa";
		this.lblrazonsocialempresa.Size = new System.Drawing.Size(70, 21);
		this.lblrazonsocialempresa.TabIndex = 126;
		this.lblrazonsocialempresa.Text = "Razon Social:";
		this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX12.Location = new System.Drawing.Point(20, 53);
		this.labelX12.Name = "labelX12";
		this.labelX12.Size = new System.Drawing.Size(47, 28);
		this.labelX12.TabIndex = 125;
		this.labelX12.Text = "Ruc/Dni (F1):";
		this.labelX12.WordWrap = true;
		this.txtrazonsocialempresa.Border.Class = "TextBoxBorder";
		this.txtrazonsocialempresa.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtrazonsocialempresa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtrazonsocialempresa.Location = new System.Drawing.Point(99, 81);
		this.txtrazonsocialempresa.Name = "txtrazonsocialempresa";
		this.txtrazonsocialempresa.PreventEnterBeep = true;
		this.txtrazonsocialempresa.Size = new System.Drawing.Size(234, 22);
		this.txtrazonsocialempresa.TabIndex = 3;
		this.txtdocempresatransporte.Border.Class = "TextBoxBorder";
		this.txtdocempresatransporte.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtdocempresatransporte.Location = new System.Drawing.Point(99, 53);
		this.txtdocempresatransporte.MaxLength = 11;
		this.txtdocempresatransporte.Name = "txtdocempresatransporte";
		this.txtdocempresatransporte.PreventEnterBeep = true;
		this.txtdocempresatransporte.Size = new System.Drawing.Size(132, 22);
		this.txtdocempresatransporte.TabIndex = 1;
		this.txtdocempresatransporte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtdocempresatransporte_KeyDown);
		this.txtdocempresatransporte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtdocempresatransporte_KeyPress);
		this.gbdatosconductor.Controls.Add(this.labelX28);
		this.gbdatosconductor.Controls.Add(this.txtapellidoconductor);
		this.gbdatosconductor.Controls.Add(this.label1);
		this.gbdatosconductor.Controls.Add(this.chkvehiculomenor);
		this.gbdatosconductor.Controls.Add(this.labelX22);
		this.gbdatosconductor.Controls.Add(this.txtlicencia);
		this.gbdatosconductor.Controls.Add(this.labelX21);
		this.gbdatosconductor.Controls.Add(this.cmbdocconductor);
		this.gbdatosconductor.Controls.Add(this.labelX18);
		this.gbdatosconductor.Controls.Add(this.labelX19);
		this.gbdatosconductor.Controls.Add(this.txtrazonsocialconductor);
		this.gbdatosconductor.Controls.Add(this.txtdocconductor);
		this.gbdatosconductor.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbdatosconductor.Location = new System.Drawing.Point(759, 197);
		this.gbdatosconductor.Name = "gbdatosconductor";
		this.gbdatosconductor.Size = new System.Drawing.Size(437, 118);
		this.gbdatosconductor.TabIndex = 135;
		this.gbdatosconductor.TabStop = false;
		this.gbdatosconductor.Text = "DATOS CONDUCTOR";
		this.labelX28.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX28.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX28.Location = new System.Drawing.Point(234, 86);
		this.labelX28.Name = "labelX28";
		this.labelX28.Size = new System.Drawing.Size(57, 18);
		this.labelX28.TabIndex = 142;
		this.labelX28.Text = "Apellido:";
		this.txtapellidoconductor.Border.Class = "TextBoxBorder";
		this.txtapellidoconductor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtapellidoconductor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtapellidoconductor.Location = new System.Drawing.Point(297, 86);
		this.txtapellidoconductor.Name = "txtapellidoconductor";
		this.txtapellidoconductor.PreventEnterBeep = true;
		this.txtapellidoconductor.Size = new System.Drawing.Size(134, 22);
		this.txtapellidoconductor.TabIndex = 141;
		this.label1.AutoSize = true;
		this.label1.ForeColor = System.Drawing.Color.Red;
		this.label1.Location = new System.Drawing.Point(249, 15);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(139, 13);
		this.label1.TabIndex = 140;
		this.label1.Text = "Motos, Furgones y Autos";
		this.chkvehiculomenor.AutoSize = true;
		this.chkvehiculomenor.Location = new System.Drawing.Point(263, 32);
		this.chkvehiculomenor.Name = "chkvehiculomenor";
		this.chkvehiculomenor.Size = new System.Drawing.Size(107, 17);
		this.chkvehiculomenor.TabIndex = 139;
		this.chkvehiculomenor.Text = "Vehiculo menor";
		this.chkvehiculomenor.UseVisualStyleBackColor = true;
		this.chkvehiculomenor.CheckedChanged += new System.EventHandler(chkvehiculomenor_CheckedChanged);
		this.labelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX22.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX22.Location = new System.Drawing.Point(228, 53);
		this.labelX22.Name = "labelX22";
		this.labelX22.Size = new System.Drawing.Size(63, 25);
		this.labelX22.TabIndex = 137;
		this.labelX22.Text = "N° Licencia:";
		this.labelX22.WordWrap = true;
		this.txtlicencia.Border.Class = "TextBoxBorder";
		this.txtlicencia.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtlicencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtlicencia.Location = new System.Drawing.Point(297, 53);
		this.txtlicencia.MaxLength = 10;
		this.txtlicencia.Name = "txtlicencia";
		this.txtlicencia.PreventEnterBeep = true;
		this.txtlicencia.Size = new System.Drawing.Size(134, 22);
		this.txtlicencia.TabIndex = 136;
		this.labelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX21.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX21.Location = new System.Drawing.Point(11, 22);
		this.labelX21.Name = "labelX21";
		this.labelX21.Size = new System.Drawing.Size(56, 21);
		this.labelX21.TabIndex = 135;
		this.labelX21.Text = "Tipo Doc:";
		this.cmbdocconductor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbdocconductor.Enabled = false;
		this.cmbdocconductor.FormattingEnabled = true;
		this.cmbdocconductor.Items.AddRange(new object[2] { "Dni", "Ruc" });
		this.cmbdocconductor.Location = new System.Drawing.Point(73, 23);
		this.cmbdocconductor.Name = "cmbdocconductor";
		this.cmbdocconductor.Size = new System.Drawing.Size(105, 21);
		this.cmbdocconductor.TabIndex = 134;
		this.cmbdocconductor.SelectedIndexChanged += new System.EventHandler(cmbdocconductor_SelectedIndexChanged);
		this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX18.Location = new System.Drawing.Point(12, 88);
		this.labelX18.Name = "labelX18";
		this.labelX18.Size = new System.Drawing.Size(58, 15);
		this.labelX18.TabIndex = 130;
		this.labelX18.Text = "Nombre:";
		this.labelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX19.Location = new System.Drawing.Point(7, 50);
		this.labelX19.Name = "labelX19";
		this.labelX19.Size = new System.Drawing.Size(47, 28);
		this.labelX19.TabIndex = 129;
		this.labelX19.Text = "Ruc/Dni (F1):";
		this.labelX19.WordWrap = true;
		this.txtrazonsocialconductor.Border.Class = "TextBoxBorder";
		this.txtrazonsocialconductor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtrazonsocialconductor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtrazonsocialconductor.Location = new System.Drawing.Point(74, 86);
		this.txtrazonsocialconductor.Name = "txtrazonsocialconductor";
		this.txtrazonsocialconductor.PreventEnterBeep = true;
		this.txtrazonsocialconductor.Size = new System.Drawing.Size(154, 22);
		this.txtrazonsocialconductor.TabIndex = 128;
		this.txtdocconductor.Border.Class = "TextBoxBorder";
		this.txtdocconductor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtdocconductor.Location = new System.Drawing.Point(73, 53);
		this.txtdocconductor.MaxLength = 11;
		this.txtdocconductor.Name = "txtdocconductor";
		this.txtdocconductor.PreventEnterBeep = true;
		this.txtdocconductor.Size = new System.Drawing.Size(108, 22);
		this.txtdocconductor.TabIndex = 127;
		this.txtdocconductor.KeyDown += new System.Windows.Forms.KeyEventHandler(txtdocconductor_KeyDown);
		this.txtdocconductor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtdocconductor_KeyPress);
		this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX8.Location = new System.Drawing.Point(889, 318);
		this.labelX8.Name = "labelX8";
		this.labelX8.Size = new System.Drawing.Size(35, 25);
		this.labelX8.TabIndex = 125;
		this.labelX8.Text = "Placa:";
		this.labelX8.WordWrap = true;
		this.txtplaca.Border.Class = "TextBoxBorder";
		this.txtplaca.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtplaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtplaca.Location = new System.Drawing.Point(926, 321);
		this.txtplaca.MaxLength = 7;
		this.txtplaca.Name = "txtplaca";
		this.txtplaca.PreventEnterBeep = true;
		this.txtplaca.Size = new System.Drawing.Size(101, 20);
		this.txtplaca.TabIndex = 1;
		this.txtplaca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.gbpuntos.Controls.Add(this.txtpuntollegada);
		this.gbpuntos.Controls.Add(this.linkLabel1);
		this.gbpuntos.Controls.Add(this.labelX25);
		this.gbpuntos.Controls.Add(this.labelX24);
		this.gbpuntos.Controls.Add(this.labelX23);
		this.gbpuntos.Controls.Add(this.cmbdistrito);
		this.gbpuntos.Controls.Add(this.cmbprovincia);
		this.gbpuntos.Controls.Add(this.cmbdepartamento);
		this.gbpuntos.Controls.Add(this.labelX13);
		this.gbpuntos.Controls.Add(this.labelX10);
		this.gbpuntos.Controls.Add(this.txtpuntopartida);
		this.gbpuntos.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbpuntos.Location = new System.Drawing.Point(701, 12);
		this.gbpuntos.Name = "gbpuntos";
		this.gbpuntos.Size = new System.Drawing.Size(495, 178);
		this.gbpuntos.TabIndex = 136;
		this.gbpuntos.TabStop = false;
		this.gbpuntos.Text = "DATOS ENVIO";
		this.txtpuntollegada.Border.Class = "TextBoxBorder";
		this.txtpuntollegada.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtpuntollegada.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtpuntollegada.Location = new System.Drawing.Point(117, 142);
		this.txtpuntollegada.Name = "txtpuntollegada";
		this.txtpuntollegada.PreventEnterBeep = true;
		this.txtpuntollegada.Size = new System.Drawing.Size(347, 22);
		this.txtpuntollegada.TabIndex = 134;
		this.txtpuntollegada.KeyDown += new System.Windows.Forms.KeyEventHandler(txtpuntollegada_KeyDown);
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(114, 126);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(64, 13);
		this.linkLabel1.TabIndex = 148;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "Nueva (F1)";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.labelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX25.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX25.Location = new System.Drawing.Point(365, 75);
		this.labelX25.Name = "labelX25";
		this.labelX25.Size = new System.Drawing.Size(56, 21);
		this.labelX25.TabIndex = 146;
		this.labelX25.Text = "Distrito:";
		this.labelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX24.Location = new System.Drawing.Point(46, 75);
		this.labelX24.Name = "labelX24";
		this.labelX24.Size = new System.Drawing.Size(83, 21);
		this.labelX24.TabIndex = 145;
		this.labelX24.Text = "Departamento:";
		this.labelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX23.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX23.Location = new System.Drawing.Point(202, 72);
		this.labelX23.Name = "labelX23";
		this.labelX23.Size = new System.Drawing.Size(83, 21);
		this.labelX23.TabIndex = 144;
		this.labelX23.Text = "Provincia:";
		this.cmbdistrito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbdistrito.FormattingEnabled = true;
		this.cmbdistrito.Location = new System.Drawing.Point(320, 96);
		this.cmbdistrito.Name = "cmbdistrito";
		this.cmbdistrito.Size = new System.Drawing.Size(144, 21);
		this.cmbdistrito.TabIndex = 140;
		this.cmbprovincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbprovincia.FormattingEnabled = true;
		this.cmbprovincia.Location = new System.Drawing.Point(172, 96);
		this.cmbprovincia.Name = "cmbprovincia";
		this.cmbprovincia.Size = new System.Drawing.Size(144, 21);
		this.cmbprovincia.TabIndex = 139;
		this.cmbprovincia.SelectedIndexChanged += new System.EventHandler(cmbprovincia_SelectedIndexChanged);
		this.cmbdepartamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbdepartamento.FormattingEnabled = true;
		this.cmbdepartamento.Location = new System.Drawing.Point(22, 96);
		this.cmbdepartamento.Name = "cmbdepartamento";
		this.cmbdepartamento.Size = new System.Drawing.Size(144, 21);
		this.cmbdepartamento.TabIndex = 138;
		this.cmbdepartamento.SelectedIndexChanged += new System.EventHandler(cmbdepartamento_SelectedIndexChanged);
		this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX13.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX13.Location = new System.Drawing.Point(21, 53);
		this.labelX13.Name = "labelX13";
		this.labelX13.Size = new System.Drawing.Size(88, 22);
		this.labelX13.TabIndex = 130;
		this.labelX13.Text = "Punto Llegada:";
		this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX10.Location = new System.Drawing.Point(22, 19);
		this.labelX10.Name = "labelX10";
		this.labelX10.Size = new System.Drawing.Size(83, 21);
		this.labelX10.TabIndex = 128;
		this.labelX10.Text = "Punto Partida:";
		this.txtpuntopartida.Border.Class = "TextBoxBorder";
		this.txtpuntopartida.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtpuntopartida.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtpuntopartida.Location = new System.Drawing.Point(111, 18);
		this.txtpuntopartida.Name = "txtpuntopartida";
		this.txtpuntopartida.PreventEnterBeep = true;
		this.txtpuntopartida.Size = new System.Drawing.Size(347, 22);
		this.txtpuntopartida.TabIndex = 127;
		this.labelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX20.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX20.Location = new System.Drawing.Point(413, 319);
		this.labelX20.Name = "labelX20";
		this.labelX20.Size = new System.Drawing.Size(33, 23);
		this.labelX20.TabIndex = 135;
		this.labelX20.Text = "Glosa:";
		this.txtglosa.Border.Class = "TextBoxBorder";
		this.txtglosa.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtglosa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtglosa.Location = new System.Drawing.Point(452, 320);
		this.txtglosa.Multiline = true;
		this.txtglosa.Name = "txtglosa";
		this.txtglosa.PreventEnterBeep = true;
		this.txtglosa.Size = new System.Drawing.Size(146, 53);
		this.txtglosa.TabIndex = 134;
		this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX9.Location = new System.Drawing.Point(11, 109);
		this.labelX9.Name = "labelX9";
		this.labelX9.Size = new System.Drawing.Size(120, 25);
		this.labelX9.TabIndex = 126;
		this.labelX9.Text = "Descripcion Motivo:";
		this.txtdescripciontranslado.Border.Class = "TextBoxBorder";
		this.txtdescripciontranslado.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtdescripciontranslado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtdescripciontranslado.Location = new System.Drawing.Point(6, 140);
		this.txtdescripciontranslado.Name = "txtdescripciontranslado";
		this.txtdescripciontranslado.PreventEnterBeep = true;
		this.txtdescripciontranslado.Size = new System.Drawing.Size(274, 22);
		this.txtdescripciontranslado.TabIndex = 3;
		this.groupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox9.Controls.Add(this.label24);
		this.groupBox9.Controls.Add(this.label22);
		this.groupBox9.Controls.Add(this.textBoxX5);
		this.groupBox9.Controls.Add(this.textBoxX1);
		this.groupBox9.Controls.Add(this.textBoxX2);
		this.groupBox9.Controls.Add(this.textBoxX6);
		this.groupBox9.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.groupBox9.Location = new System.Drawing.Point(779, 12);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(371, 53);
		this.groupBox9.TabIndex = 142;
		this.groupBox9.TabStop = false;
		this.gbmotivotranslado.Controls.Add(this.labelX17);
		this.gbmotivotranslado.Controls.Add(this.labelX16);
		this.gbmotivotranslado.Controls.Add(this.cmbmotivotranslado);
		this.gbmotivotranslado.Controls.Add(this.dtfechatranslado);
		this.gbmotivotranslado.Controls.Add(this.labelX9);
		this.gbmotivotranslado.Controls.Add(this.txtdescripciontranslado);
		this.gbmotivotranslado.Controls.Add(this.labelX14);
		this.gbmotivotranslado.Controls.Add(this.cmbmodotranslado);
		this.gbmotivotranslado.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbmotivotranslado.Location = new System.Drawing.Point(409, 12);
		this.gbmotivotranslado.Name = "gbmotivotranslado";
		this.gbmotivotranslado.Size = new System.Drawing.Size(286, 180);
		this.gbmotivotranslado.TabIndex = 137;
		this.gbmotivotranslado.TabStop = false;
		this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX17.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX17.Location = new System.Drawing.Point(11, 83);
		this.labelX17.Name = "labelX17";
		this.labelX17.Size = new System.Drawing.Size(89, 14);
		this.labelX17.TabIndex = 132;
		this.labelX17.Text = "Fecha Translado:";
		this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX16.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX16.Location = new System.Drawing.Point(3, 46);
		this.labelX16.Name = "labelX16";
		this.labelX16.Size = new System.Drawing.Size(107, 19);
		this.labelX16.TabIndex = 133;
		this.labelX16.Text = "Motivo Traslado:";
		this.cmbmotivotranslado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbmotivotranslado.FormattingEnabled = true;
		this.cmbmotivotranslado.Location = new System.Drawing.Point(131, 54);
		this.cmbmotivotranslado.Name = "cmbmotivotranslado";
		this.cmbmotivotranslado.Size = new System.Drawing.Size(144, 21);
		this.cmbmotivotranslado.TabIndex = 132;
		this.dtfechatranslado.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfechatranslado.Location = new System.Drawing.Point(131, 81);
		this.dtfechatranslado.Name = "dtfechatranslado";
		this.dtfechatranslado.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.dtfechatranslado.Size = new System.Drawing.Size(144, 22);
		this.dtfechatranslado.TabIndex = 133;
		this.dtfechatranslado.Tag = "16";
		this.dtfechatranslado.Value = new System.DateTime(2023, 1, 19, 0, 0, 0, 0);
		this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX14.Location = new System.Drawing.Point(6, 21);
		this.labelX14.Name = "labelX14";
		this.labelX14.Size = new System.Drawing.Size(107, 19);
		this.labelX14.TabIndex = 131;
		this.labelX14.Text = "Modo Traslado:";
		this.cmbmodotranslado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbmodotranslado.FormattingEnabled = true;
		this.cmbmodotranslado.Location = new System.Drawing.Point(131, 21);
		this.cmbmodotranslado.Name = "cmbmodotranslado";
		this.cmbmodotranslado.Size = new System.Drawing.Size(144, 21);
		this.cmbmodotranslado.TabIndex = 0;
		this.cmbmodotranslado.SelectedIndexChanged += new System.EventHandler(cmbmodotranslado_SelectedIndexChanged);
		this.gbdatosgenerales.Controls.Add(this.labelX15);
		this.gbdatosgenerales.Controls.Add(this.cmbserie);
		this.gbdatosgenerales.Controls.Add(this.labelX5);
		this.gbdatosgenerales.Controls.Add(this.labelX4);
		this.gbdatosgenerales.Controls.Add(this.dtpFecha);
		this.gbdatosgenerales.Controls.Add(this.txtNombreVendedor);
		this.gbdatosgenerales.Controls.Add(this.txtCodigoVendedor);
		this.gbdatosgenerales.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbdatosgenerales.Location = new System.Drawing.Point(34, 13);
		this.gbdatosgenerales.Name = "gbdatosgenerales";
		this.gbdatosgenerales.Size = new System.Drawing.Size(371, 97);
		this.gbdatosgenerales.TabIndex = 138;
		this.gbdatosgenerales.TabStop = false;
		this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX15.Location = new System.Drawing.Point(6, 21);
		this.labelX15.Name = "labelX15";
		this.labelX15.Size = new System.Drawing.Size(37, 19);
		this.labelX15.TabIndex = 131;
		this.labelX15.Text = "Serie:";
		this.cmbserie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbserie.FormattingEnabled = true;
		this.cmbserie.Location = new System.Drawing.Point(43, 21);
		this.cmbserie.Name = "cmbserie";
		this.cmbserie.Size = new System.Drawing.Size(63, 21);
		this.cmbserie.TabIndex = 0;
		this.labelX26.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX26.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX26.Location = new System.Drawing.Point(603, 319);
		this.labelX26.Name = "labelX26";
		this.labelX26.Size = new System.Drawing.Size(48, 25);
		this.labelX26.TabIndex = 142;
		this.labelX26.Text = "P. Bruto:";
		this.labelX26.WordWrap = true;
		this.txtpesobruto.Border.Class = "TextBoxBorder";
		this.txtpesobruto.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtpesobruto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtpesobruto.Location = new System.Drawing.Point(652, 321);
		this.txtpesobruto.MaxLength = 10;
		this.txtpesobruto.Name = "txtpesobruto";
		this.txtpesobruto.PreventEnterBeep = true;
		this.txtpesobruto.Size = new System.Drawing.Size(73, 20);
		this.txtpesobruto.TabIndex = 141;
		this.txtpesobruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX27.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX27.Location = new System.Drawing.Point(731, 319);
		this.labelX27.Name = "labelX27";
		this.labelX27.Size = new System.Drawing.Size(58, 25);
		this.labelX27.TabIndex = 144;
		this.labelX27.Text = "NroPallets:";
		this.labelX27.WordWrap = true;
		this.txtnropallets.Border.Class = "TextBoxBorder";
		this.txtnropallets.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtnropallets.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtnropallets.Location = new System.Drawing.Point(791, 321);
		this.txtnropallets.MaxLength = 10;
		this.txtnropallets.Name = "txtnropallets";
		this.txtnropallets.PreventEnterBeep = true;
		this.txtnropallets.Size = new System.Drawing.Size(91, 20);
		this.txtnropallets.TabIndex = 143;
		this.txtnropallets.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lbloc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.lbloc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.lbloc.Location = new System.Drawing.Point(612, 350);
		this.lbloc.Name = "lbloc";
		this.lbloc.Size = new System.Drawing.Size(35, 25);
		this.lbloc.TabIndex = 146;
		this.lbloc.Text = "N° OC:";
		this.lbloc.WordWrap = true;
		this.txtnumoc.Border.Class = "TextBoxBorder";
		this.txtnumoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtnumoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtnumoc.Location = new System.Drawing.Point(655, 353);
		this.txtnumoc.MaxLength = 20;
		this.txtnumoc.Name = "txtnumoc";
		this.txtnumoc.PreventEnterBeep = true;
		this.txtnumoc.Size = new System.Drawing.Size(124, 20);
		this.txtnumoc.TabIndex = 145;
		this.txtnumoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.labelX29.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.labelX29.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.labelX29.Location = new System.Drawing.Point(1051, 318);
		this.labelX29.Name = "labelX29";
		this.labelX29.Size = new System.Drawing.Size(35, 25);
		this.labelX29.TabIndex = 148;
		this.labelX29.Text = "Flete:";
		this.labelX29.WordWrap = true;
		this.txtflete.Border.Class = "TextBoxBorder";
		this.txtflete.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.txtflete.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtflete.Location = new System.Drawing.Point(1090, 321);
		this.txtflete.MaxLength = 20;
		this.txtflete.Name = "txtflete";
		this.txtflete.PreventEnterBeep = true;
		this.txtflete.Size = new System.Drawing.Size(97, 20);
		this.txtflete.TabIndex = 147;
		this.txtflete.Text = "0.00";
		this.txtflete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.gbbucaproducto.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gbbucaproducto.Controls.Add(this.labelx);
		this.gbbucaproducto.Controls.Add(this.txtFiltro);
		this.gbbucaproducto.Controls.Add(this.label10);
		this.gbbucaproducto.Controls.Add(this.label11);
		this.gbbucaproducto.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbbucaproducto.Location = new System.Drawing.Point(34, 353);
		this.gbbucaproducto.Name = "gbbucaproducto";
		this.gbbucaproducto.Size = new System.Drawing.Size(415, 51);
		this.gbbucaproducto.TabIndex = 149;
		this.gbbucaproducto.TabStop = false;
		this.gbbucaproducto.Text = "BUSCAR";
		this.labelx.AutoSize = true;
		this.labelx.Location = new System.Drawing.Point(377, 23);
		this.labelx.Name = "labelx";
		this.labelx.Size = new System.Drawing.Size(13, 13);
		this.labelx.TabIndex = 309;
		this.labelx.Text = "x";
		this.labelx.Visible = false;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(153, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(218, 22);
		this.txtFiltro.TabIndex = 306;
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.txtFiltro.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyUp);
		this.txtFiltro.Leave += new System.EventHandler(txtFiltro_Leave);
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(2, 22);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(68, 13);
		this.label10.TabIndex = 307;
		this.label10.Text = "Buscar Por :";
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(73, 22);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(16, 15);
		this.label11.TabIndex = 308;
		this.label11.Text = "X";
		this.gbproductos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.gbproductos.Controls.Add(this.dgvproductos);
		this.gbproductos.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		this.gbproductos.Location = new System.Drawing.Point(34, 410);
		this.gbproductos.Name = "gbproductos";
		this.gbproductos.Size = new System.Drawing.Size(539, 186);
		this.gbproductos.TabIndex = 150;
		this.gbproductos.TabStop = false;
		this.gbproductos.Text = "PRODUCTOS";
		this.dgvproductos.AllowUserToAddRows = false;
		this.dgvproductos.AllowUserToDeleteRows = false;
		this.dgvproductos.AllowUserToResizeRows = false;
		this.dgvproductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvproductos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(81, 124, 210);
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvproductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvproductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvproductos.Columns.AddRange(this.codigo, this.referencia, this.codUniversal, this.nomAlma, this.descripcion, this.unidad, this.nmarca, this.Modelo, this.stockdisponible, this.cant, this.precio, this.total, this.codunidadmedida, this.codsunatimpuesto, this.codtimpuesto, this.codalma, this.unidadnombre, this.icbper, this.codlinea, this.codfamilia);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvproductos.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvproductos.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvproductos.EnableHeadersVisualStyles = false;
		this.dgvproductos.Location = new System.Drawing.Point(3, 18);
		this.dgvproductos.MultiSelect = false;
		this.dgvproductos.Name = "dgvproductos";
		this.dgvproductos.RowHeadersVisible = false;
		this.dgvproductos.RowTemplate.Height = 28;
		this.dgvproductos.Size = new System.Drawing.Size(533, 165);
		this.dgvproductos.TabIndex = 1;
		this.dgvproductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvproductos_CellClick);
		this.dgvproductos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvproductos_CellEndEdit);
		this.dgvproductos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvproductos_ColumnHeaderMouseClick);
		this.dgvproductos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvproductos_EditingControlShowing);
		this.codigo.DataPropertyName = "codProducto";
		this.codigo.HeaderText = "Codigo ";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Visible = false;
		this.referencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.FillWeight = 80f;
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 65;
		this.codUniversal.DataPropertyName = "codUniversal";
		this.codUniversal.HeaderText = "Cod Universal";
		this.codUniversal.Name = "codUniversal";
		this.codUniversal.ReadOnly = true;
		this.codUniversal.Visible = false;
		this.nomAlma.DataPropertyName = "nomAlma";
		this.nomAlma.HeaderText = "Almacen";
		this.nomAlma.Name = "nomAlma";
		this.nomAlma.Visible = false;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.FillWeight = 94.46439f;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.unidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.unidad.DisplayStyleForCurrentCellOnly = true;
		this.unidad.FillWeight = 11.97176f;
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
		this.unidad.Width = 70;
		this.nmarca.DataPropertyName = "nmarca";
		this.nmarca.HeaderText = "Marca";
		this.nmarca.Name = "nmarca";
		this.nmarca.ReadOnly = true;
		this.nmarca.Visible = false;
		this.Modelo.DataPropertyName = "modelo";
		this.Modelo.HeaderText = "Modelo";
		this.Modelo.Name = "Modelo";
		this.Modelo.ReadOnly = true;
		this.Modelo.Visible = false;
		this.stockdisponible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.stockdisponible.DataPropertyName = "stockdisponible";
		this.stockdisponible.HeaderText = "Stock";
		this.stockdisponible.Name = "stockdisponible";
		this.stockdisponible.ReadOnly = true;
		this.stockdisponible.Visible = false;
		this.cant.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.cant.FillWeight = 7.859087f;
		this.cant.HeaderText = "Cantidad";
		this.cant.Name = "cant";
		this.cant.Width = 50;
		this.precio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.precio.FillWeight = 8.713611f;
		this.precio.HeaderText = "Precio";
		this.precio.Name = "precio";
		this.precio.Width = 50;
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.ReadOnly = true;
		this.total.Visible = false;
		this.codunidadmedida.DataPropertyName = "codunidadmedida";
		this.codunidadmedida.HeaderText = "CodUnidad";
		this.codunidadmedida.Name = "codunidadmedida";
		this.codunidadmedida.ReadOnly = true;
		this.codunidadmedida.Visible = false;
		this.codsunatimpuesto.DataPropertyName = "codsunatimpuesto";
		this.codsunatimpuesto.HeaderText = "Codigo Sunat";
		this.codsunatimpuesto.Name = "codsunatimpuesto";
		this.codsunatimpuesto.ReadOnly = true;
		this.codsunatimpuesto.Visible = false;
		this.codtimpuesto.DataPropertyName = "codtimpuesto";
		this.codtimpuesto.HeaderText = "CodTipoImpuesto";
		this.codtimpuesto.Name = "codtimpuesto";
		this.codtimpuesto.ReadOnly = true;
		this.codtimpuesto.Visible = false;
		this.codalma.DataPropertyName = "codalma";
		this.codalma.HeaderText = "codalma";
		this.codalma.Name = "codalma";
		this.codalma.Visible = false;
		this.unidadnombre.DataPropertyName = "unidadnombre";
		this.unidadnombre.HeaderText = "UnidadNombre";
		this.unidadnombre.Name = "unidadnombre";
		this.unidadnombre.Visible = false;
		this.icbper.DataPropertyName = "icbper";
		this.icbper.HeaderText = "icbper";
		this.icbper.Name = "icbper";
		this.icbper.Visible = false;
		this.codlinea.DataPropertyName = "codlinea";
		this.codlinea.HeaderText = "codlinea";
		this.codlinea.Name = "codlinea";
		this.codlinea.Visible = false;
		this.codfamilia.DataPropertyName = "codfamilia";
		this.codfamilia.HeaderText = "codfamilia";
		this.codfamilia.Name = "codfamilia";
		this.codfamilia.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Snow;
		base.ClientSize = new System.Drawing.Size(1218, 659);
		base.Controls.Add(this.gbproductos);
		base.Controls.Add(this.gbbucaproducto);
		base.Controls.Add(this.labelX29);
		base.Controls.Add(this.txtflete);
		base.Controls.Add(this.lbloc);
		base.Controls.Add(this.txtnumoc);
		base.Controls.Add(this.labelX27);
		base.Controls.Add(this.txtnropallets);
		base.Controls.Add(this.labelX26);
		base.Controls.Add(this.txtpesobruto);
		base.Controls.Add(this.gbdatosgenerales);
		base.Controls.Add(this.gbmotivotranslado);
		base.Controls.Add(this.gbpuntos);
		base.Controls.Add(this.gbdatosconductor);
		base.Controls.Add(this.labelX8);
		base.Controls.Add(this.gbempresatransporte);
		base.Controls.Add(this.gbtotales);
		base.Controls.Add(this.txtplaca);
		base.Controls.Add(this.labelX20);
		base.Controls.Add(this.gbdatoscliente);
		base.Controls.Add(this.Acciones);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.txtglosa);
		base.Name = "frmGuiaFacturacion";
		this.Text = "frmGuiaFacturacion";
		base.Load += new System.EventHandler(frmGuiaFacturacion_Load);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvdetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvproductos.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvproductos).EndInit();
		this.Acciones.ResumeLayout(false);
		this.Acciones.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btnedidar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnimprimir).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).EndInit();
		this.gbdatoscliente.ResumeLayout(false);
		this.gbdatoscliente.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda).EndInit();
		this.gbtotales.ResumeLayout(false);
		this.gbtotales.PerformLayout();
		this.gbempresatransporte.ResumeLayout(false);
		this.gbdatosconductor.ResumeLayout(false);
		this.gbdatosconductor.PerformLayout();
		this.gbpuntos.ResumeLayout(false);
		this.gbpuntos.PerformLayout();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		this.gbmotivotranslado.ResumeLayout(false);
		this.gbdatosgenerales.ResumeLayout(false);
		this.gbbucaproducto.ResumeLayout(false);
		this.gbbucaproducto.PerformLayout();
		this.gbproductos.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvproductos).EndInit();
		base.ResumeLayout(false);
	}
}
