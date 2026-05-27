using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmOrdenCompra : Office2007Form
{
	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmProveedor AdmProv = new clsAdmProveedor();

	private clsProveedor prov = new clsProveedor();

	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	public clsOrdenCompra Ord = new clsOrdenCompra();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsFormaPago fpago = new clsFormaPago();

	private clsValidar ok = new clsValidar();

	public clsDetalleOrdenCompra detaSelec = new clsDetalleOrdenCompra();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto prodeta = new clsProducto();

	private clsReporteOrdeCompra ds = new clsReporteOrdeCompra();

	private clsTipoDocumento tidoc = new clsTipoDocumento();

	private clsAdmTipoDocumento Admtipodoc = new clsAdmTipoDocumento();

	private clsSerie serie = new clsSerie();

	private clsAdmSerie admser = new clsAdmSerie();

	public List<int> config = new List<int>();

	public List<clsDetalleOrdenCompra> detalleTemp = new List<clsDetalleOrdenCompra>();

	public List<clsDetalleOrdenCompra> detalle = new List<clsDetalleOrdenCompra>();

	public int CodProveedor;

	public int CodOrdenCompra = 0;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Procede = 0;

	public int Tipo;

	public int uno = 0;

	private TextBox txtedit = new TextBox();

	private clsAdmGuiaRemisionCompra admguiaremisioncompra = new clsAdmGuiaRemisionCompra();

	private clsAdmProveedor AdmET = new clsAdmProveedor();

	private clsProveedor empT = new clsProveedor();

	public int CodEmpresaTransporte = -1;

	public string dos = "";

	public int consultacombo = 0;

	private BindingSource bindingSource1 = new BindingSource();

	internal clsUsuario usuario_click = null;

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private bool Punto = true;

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private TextBox txtPrecioVenta;

	private Label label14;

	private TextBox txtIGV;

	private Label label13;

	private TextBox txtDscto;

	private TextBox txtValorVenta;

	private Label label12;

	private Label label11;

	private Label label10;

	private TextBox txtBruto;

	public DataGridView dgvDetalle;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator7;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private CustomValidator customValidator4;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private GroupBox groupBox1;

	private TextBox txtCodProveedor;

	private CheckBox cbValorVenta;

	private DateTimePicker dtpFechaPago;

	private ComboBox cmbFormaPago;

	private Label label17;

	private TextBox txtTipoCambio;

	private ComboBox cmbMoneda;

	private Label label16;

	private Label label15;

	private Button btnDetalle;

	private TextBox txtComentario;

	private Label label9;

	private Label label7;

	public TextBox txtNombreProv;

	public TextBox txtCodProv;

	private Label label4;

	private DateTimePicker dtpFecha;

	private Label label1;

	private Button btnImprimir;

	private Button btnGenerarGR;

	public TextBox txtOrdenCompra;

	public Button btnEditar;

	public TextBox txtestadoOrden;

	private Label label2;

	private Button button2;

	private GroupBox gbTransportista;

	public TextBox txtDireccionTransporte;

	private Label label3;

	public TextBox txtRazonSocialTransporte;

	private Label label5;

	private Label label6;

	public TextBox txtRUCTransporte;

	private GroupBox groupBox4;

	private Label label19;

	private Label label8;

	private Button btnAprobarOrdenCompra;

	private Button btncerrarordendecompra;

	public Button btnModificarFlete;

	private Button btnListarGR;

	public Button btnActualizarLIsta;

	private GroupBox gbcontienedgvordenesgeneradas;

	private DataGridView dgvOrdenesGeneradas;

	private Label lblUsuarioAnulador;

	private Label lblanuladopor;

	private Label label18;

	private ComboBox cmbtipoflete;

	private DataGridViewTextBoxColumn colItem;

	private DataGridViewTextBoxColumn colNumGRC;

	private DataGridViewTextBoxColumn colcodGRC;

	private DataGridViewTextBoxColumn colCodFactFlete;

	private DataGridViewTextBoxColumn colFacturaFlete;

	private DataGridViewTextBoxColumn colCodFactCompra;

	private DataGridViewTextBoxColumn colFacturaCompra;

	public Button btnUpdatePrecios;

	private TextBox txttipCambioProv;

	private Label label20;

	public TextBox txtTotalFleteConIgv;

	public TextBox txtTotalFleteSinIgv;

	public Label label22;

	public Label label21;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn moneda;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn cantidadpendiente;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn valorventaconflete;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn flete;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn pvconflete;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn fechaingreso;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn EstadoDeLaOrden;

	private DataGridViewTextBoxColumn ProductoSolicitado;

	private DataGridViewTextBoxColumn etiqueta;

	public TextBox txttiutlooc;

	private Label lbltitulo;

	public frmOrdenCompra()
	{
		InitializeComponent();
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
	}

	private void CargaProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProv.Text = prov.Ruc;
		txtNombreProv.Text = prov.RazonSocial;
		txtCodProveedor.Text = prov.CodProveedor.ToString();
	}

	private void BorrarProveedor()
	{
		prov = AdmProv.MuestraProveedor(CodProveedor);
		txtCodProv.Text = "";
		txtNombreProv.Text = "";
	}

	private bool BuscaProveedor()
	{
		prov = AdmProv.BuscaProveedor(txtCodProv.Text);
		if (prov != null)
		{
			txtNombreProv.Text = prov.RazonSocial;
			CodProveedor = prov.CodProveedor;
			return true;
		}
		txtNombreProv.Text = "";
		CodProveedor = 0;
		return false;
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		if (Application.OpenForms["frmDetalleIngreso"] != null)
		{
			Application.OpenForms["frmDetalleIngreso"].Activate();
			return;
		}
		frmDetalleIngreso form = new frmDetalleIngreso();
		form.Procede = Procede;
		form.formulario_o_c_padre = this;
		form.Proceso = 1;
		form.bvalorventa = cbValorVenta.Checked;
		form.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
		form.ShowDialog();
		rellenarTotalesFlete();
	}

	private void VerificarCabecera()
	{
		Validacion = true;
		if (txtCodProv.Visible && CodProveedor == 0)
		{
			Validacion = false;
		}
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmDetalleIngreso"] != null)
			{
				Application.OpenForms["frmDetalleIngreso"].Activate();
				return;
			}
			frmDetalleIngreso form = new frmDetalleIngreso();
			form.Procede = Procede;
			form.Proceso = 1;
			form.formulario_o_c_padre = this;
			form.bvalorventa = cbValorVenta.Checked;
			form.ShowDialog();
		}
		catch (Exception)
		{
		}
	}

	public void asignacionProductosDePlantilla(int codigo_plantilla)
	{
		DataTable data = new DataTable();
		clsAdmPlantillaDeProductos AdmPlantillaProductos = new clsAdmPlantillaDeProductos();
		dgvDetalle.Rows.Clear();
		data = AdmPlantillaProductos.cargadetalleproductosagrupados(codigo_plantilla);
		foreach (DataRow fila in data.Rows)
		{
			dgvDetalle.Rows.Add(fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), fila[5].ToString(), fila[6].ToString(), fila[7].ToString(), Convert.ToDecimal(fila[8].ToString()), Convert.ToDecimal(fila[9].ToString()), Convert.ToDecimal(fila[10].ToString()), Convert.ToDecimal(fila[11].ToString()), Convert.ToDecimal(fila[12].ToString()), Convert.ToDecimal(fila[13].ToString()), fila[14].ToString(), fila[15].ToString(), fila[16].ToString(), fila[17].ToString(), fila[18].ToString(), fila[19].ToString(), fila[20].ToString(), fila[21].ToString(), fila[22].ToString(), fila[23].ToString(), Convert.ToDateTime(fila[24].ToString()), fila[25].ToString(), Convert.ToDateTime(fila[26].ToString()), Convert.ToBoolean(fila[27].ToString()), Convert.ToBoolean(fila[28].ToString()), fila[29].ToString());
		}
		RecorreDetalleOrden();
		Ord.Detalle = detalle;
	}

	private void txtCodProv_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && txtCodProv.Text != "")
		{
			if (BuscaProveedor())
			{
				ProcessTabKey(forward: true);
			}
			else
			{
				MessageBox.Show("El proveedor no existe, Presione F1 para consultar la tabla de ayuda", "Orden Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void frmOrdenCompra_Load(object sender, EventArgs e)
	{
		dos = admguiaremisioncompra.getEtiquetaGRC(2);
		cargaMoneda();
		cmbMoneda.SelectedIndex = 1;
		CargaFormaPagos();
		tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
		tidoc = Admtipodoc.CargaTipoDocumento(13);
		serie = admser.BuscaSeriexDocumento(tidoc.CodTipoDocumento, frmLogin.iCodAlmacen);
		if (serie == null)
		{
			MessageBox.Show("Debe agregar Serie para este tipo de Documento" + Environment.NewLine + "Porfavor Cierre esta Ventana Cree la Serie y Vuelva Abrir", "Orden Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		if (Proceso == 1)
		{
			btnGenerarGR.Visible = false;
			btnEditar.Visible = false;
			btnEliminar.Visible = false;
			cbValorVenta.Visible = true;
			btnActualizarLIsta.Visible = false;
			cmbtipoflete.SelectedIndex = 2;
		}
		else
		{
			btnActualizarLIsta.Visible = true;
		}
		if (Proceso == 2)
		{
			CargaOrdenCompra();
			btnEditar.Visible = false;
			button2.Visible = false;
		}
		else if (Proceso == 3)
		{
			CargaOrdenCompra();
			sololectura(estado: true);
		}
		else if (Proceso == 4)
		{
			CargaOrdenCompra();
			sololectura(estado: true);
		}
		else if (Proceso == 5)
		{
			CargaOrdenCompra();
			sololectura(estado: true);
		}
		if (Ord.estadoOrden != 0)
		{
			switch (Ord.estadoOrden)
			{
			case 20:
				btncerrarordendecompra.Visible = true;
				btnGenerarGR.Visible = true;
				break;
			case 19:
				btncerrarordendecompra.Visible = true;
				btnGenerarGR.Visible = true;
				break;
			case 6:
				btnAprobarOrdenCompra.Visible = true;
				break;
			case 5:
				btncerrarordendecompra.Visible = true;
				btnGenerarGR.Visible = true;
				break;
			case 2:
				btncerrarordendecompra.Visible = true;
				btnGenerarGR.Visible = true;
				break;
			case 3:
				btncerrarordendecompra.Visible = false;
				btnGenerarGR.Visible = false;
				break;
			case 1:
				if (frmLogin.iNivelUser == 1)
				{
					btnEliminar.Visible = true;
				}
				break;
			}
		}
		int codigoOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
		if (admguiaremisioncompra.getCodigoPrimeraGRCGenerada(codigoOC) == 0)
		{
			btnListarGR.Visible = false;
		}
		else
		{
			dgvOrdenesGeneradas.DataSource = AdmOrden.muestraGRCGeneradas(codigoOC);
			gbcontienedgvordenesgeneradas.Visible = true;
			btnListarGR.Visible = true;
		}
		if (Ord.Anulado == 1)
		{
			btnEliminar.Visible = false;
		}
	}

	private void cargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.ListaMonedas();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
	}

	private void sololectura(bool estado)
	{
		dtpFecha.Enabled = !estado;
		txtCodProv.ReadOnly = estado;
		cmbMoneda.Enabled = !estado;
		txtCodProv.Enabled = !estado;
		btnDetalle.Enabled = !estado;
		cmbFormaPago.Enabled = !estado;
		txtOrdenCompra.ReadOnly = estado;
		txtComentario.ReadOnly = estado;
		txtBruto.ReadOnly = estado;
		txtDscto.ReadOnly = estado;
		txtValorVenta.ReadOnly = estado;
		txtIGV.ReadOnly = estado;
		txtPrecioVenta.ReadOnly = estado;
		btnNuevo.Visible = !estado;
		btnEditar.Visible = !estado;
		btnEliminar.Visible = !estado;
		btnGuardar.Visible = !estado;
		btnImprimir.Visible = estado;
		txttipCambioProv.Enabled = !estado;
	}

	private void CargaOrdenCompra()
	{
		try
		{
			consultacombo = 100;
			Ord = AdmOrden.CargaOrdenCompra(Convert.ToInt32(CodOrdenCompra));
			if (Ord != null)
			{
				if (Ord.Anulado == 1)
				{
					lblanuladopor.Visible = true;
					lblUsuarioAnulador.Visible = true;
					clsAdmUsuario admuser = new clsAdmUsuario();
					clsUsuario user = admuser.MuestraUsuario(Ord.UsuarioModificador);
					lblUsuarioAnulador.Text = user.Nombre + " " + user.Apellido;
				}
				txtOrdenCompra.Text = Ord.Serie + "-" + Ord.NumDoc.ToString().PadLeft(11, '0');
				if (txtCodProv.Enabled)
				{
					CodProveedor = Ord.CodProveedor;
					txtCodProv.Text = Ord.RUCProveedor;
					txtNombreProv.Text = Ord.RazonSocialProveedor;
					BuscaProveedor();
				}
				dtpFecha.Value = Ord.FechaIngreso;
				cmbMoneda.SelectedValue = Ord.Moneda;
				txtTipoCambio.Text = Ord.TipoCambio.ToString();
				txttiutlooc.Text = Ord.TituloOc;
				if (txtOrdenCompra.Enabled)
				{
				}
				cmbFormaPago.SelectedValue = Ord.FormaPago;
				if (Ord.FechaPago == DateTime.MinValue)
				{
					dtpFechaPago.Value = dtpFecha.MinDate;
				}
				else
				{
					dtpFechaPago.Value = Ord.FechaPago;
				}
				txtComentario.Text = Ord.Comentario;
				txtBruto.Text = $"{Ord.MontoBruto:#,##0.0000}";
				txtDscto.Text = $"{Ord.MontoDscto:#,##0.0000}";
				txtValorVenta.Text = $"{Ord.Total - Ord.Igv:#,##0.0000}";
				txtIGV.Text = $"{Ord.Igv:#,##0.0000}";
				txtPrecioVenta.Text = $"{Ord.Total:#,##0.0000}";
				txttipCambioProv.Text = $"{Ord.tipCambioProv:#,##0.0000}";
				btnDetalle.Enabled = true;
				txtestadoOrden.Text = AdmOrden.getEstadoOrdenCompra(Ord.estadoOrden);
				cmbtipoflete.SelectedIndex = Ord.fleteTransportista;
				gbTransportista.Visible = Ord.fleteTransportista == 2;
				if (Ord.codEmpresaTransporte != 0)
				{
					empT.CodProveedor = Ord.codEmpresaTransporte;
					CodEmpresaTransporte = Ord.codEmpresaTransporte;
					CargaEmpresaTransporte();
				}
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void CargaDetalle()
	{
		DataTable data = new DataTable();
		dgvDetalle.Rows.Clear();
		data = AdmOrden.CargaDetalle(Convert.ToInt32(Ord.CodOrdenCompra));
		foreach (DataRow r in data.Rows)
		{
			dgvDetalle.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), Convert.ToDecimal(r[8].ToString()), Convert.ToDecimal(r[9].ToString()), Convert.ToDecimal(r[10].ToString()), Convert.ToDecimal(r[11].ToString()), Convert.ToDecimal(r[12].ToString()), Convert.ToDecimal(r[13].ToString()), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), r[18].ToString(), r[19].ToString(), r[20].ToString(), r[21].ToString(), r[22].ToString(), r[23].ToString(), Convert.ToDateTime(r[24].ToString()), r[25].ToString(), Convert.ToDateTime(r[26].ToString()), Convert.ToInt32(r[27].ToString()), Convert.ToBoolean(r[28].ToString()), r[29].ToString());
		}
		RecorreDetalleOrden();
		Ord.Detalle = detalle;
	}

	private void txtCodProv_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmProveedoresLista"] != null)
		{
			Application.OpenForms["frmProveedoresLista"].Activate();
			return;
		}
		frmProveedoresLista form = new frmProveedoresLista();
		form.Proceso = 3;
		form.Procede = 3;
		if (form.ShowDialog() == DialogResult.OK)
		{
			CodProveedor = form.GetCodProveeder();
			txtCodProveedor.Text = CodProveedor.ToString();
			CargaProveedor();
			ProcessTabKey(forward: true);
		}
	}

	private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso == 1)
		{
			calculatotales();
		}
		else if (Proceso == 2)
		{
			calculatotales();
		}
	}

	private void calculatotales()
	{
		decimal bruto = default(decimal);
		decimal descuen = default(decimal);
		decimal valor = default(decimal);
		decimal igvt = default(decimal);
		decimal preciot = default(decimal);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			bruto += Convert.ToDecimal(row.Cells[importe.Name].Value);
			descuen += Convert.ToDecimal(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDecimal(row.Cells[valorventa.Name].Value);
			igvt += Convert.ToDecimal(row.Cells[igv.Name].Value);
			preciot += Convert.ToDecimal(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtDscto.Text = $"{descuen:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
		txtIGV.Text = $"{igvt:#,##0.0000}";
		txtPrecioVenta.Text = $"{preciot:#,##0.0000}";
	}

	private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtCodProv_Leave(object sender, EventArgs e)
	{
		if (CodProveedor == 0)
		{
			txtCodProv.Focus();
			return;
		}
		VerificarCabecera();
		if (Validacion)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
	{
		if (txtPrecioVenta.Text != "")
		{
			btnGuardar.Enabled = true;
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!validarDatosFormulario() || Proceso == 0)
		{
			return;
		}
		if (uno == 1)
		{
			detaSelec.uno = 1;
		}
		else
		{
			detaSelec.uno = 2;
		}
		Ord.CodAlmacen = frmLogin.iCodAlmacen;
		Ord.CodProveedor = CodProveedor;
		Ord.Comentario = txtComentario.Text;
		Ord.CodTipoDocumento = tidoc.CodTipoDocumento;
		Ord.CodSerie = serie.CodSerie;
		Ord.NumDoc = serie.Numeracion.ToString();
		Ord.FechaIngreso = dtpFecha.Value;
		Ord.CodUser = frmLogin.iCodUser;
		Ord.Moneda = Convert.ToString(cmbMoneda.SelectedValue);
		if (txtTipoCambio.Visible)
		{
			Ord.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text);
		}
		Ord.MontoBruto = Convert.ToDecimal(txtBruto.Text);
		Ord.MontoDscto = Convert.ToDecimal(txtDscto.Text);
		Ord.Igv = Convert.ToDecimal(txtIGV.Text);
		Ord.Total = Convert.ToDecimal(txtPrecioVenta.Text);
		Ord.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
		Ord.FechaPago = dtpFechaPago.Value;
		Ord.CodOrdenCompra = Ord.CodOrdenCompra;
		Ord.codEmpresaTransporte = ((CodEmpresaTransporte != -1) ? CodEmpresaTransporte : 0);
		Ord.fleteTransportista = cmbtipoflete.SelectedIndex;
		Ord.tipCambioProv = 0m;
		Ord.TituloOc = txttiutlooc.Text;
		if (Proceso == 1)
		{
			Ord.estadoOrden = 6;
			if (!AdmOrden.insert(Ord))
			{
				return;
			}
			int cuenta = 0;
			RecorreDetalleOrden();
			if (detalle.Count > 0)
			{
				foreach (clsDetalleOrdenCompra det in detalle)
				{
					if (AdmOrden.insertdetalle(det))
					{
						cuenta++;
					}
				}
			}
			if (detalle.Count == cuenta)
			{
				MessageBox.Show("Los datos se guardaron correctamente", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtOrdenCompra.Text = Ord.NumDoc.ToString().PadLeft(11, '0');
				sololectura(estado: true);
				btnAprobarOrdenCompra.Visible = true;
				txtestadoOrden.Text = AdmOrden.getEstadoOrdenCompra(Ord.estadoOrden);
				CodOrdenCompra = Ord.CodOrdenCompraNuevo;
				Proceso = 3;
				frmOrdenCompra_Load(sender, e);
				frmOrdenCompra_Shown(sender, e);
			}
			else
			{
				MessageBox.Show("No se guardaron todo los items", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			if (Proceso != 2)
			{
				return;
			}
			decimal tipcambioProv = Convert.ToDecimal(txttipCambioProv.Text);
			if (cmbMoneda.Text == "DOLARES AMERICANOS" && tipcambioProv == 0m)
			{
				MessageBox.Show("¡Ingrese el tipo de cambio del proveedor!", "AVISO");
				txttipCambioProv.Focus();
				return;
			}
			Ord.tipCambioProv = Convert.ToDecimal(txttipCambioProv.Text);
			List<clsDetalleOrdenCompra> detalleTemp = new List<clsDetalleOrdenCompra>();
			if (!AdmOrden.update(Ord))
			{
				return;
			}
			AdmOrden.registrarModificacionDeOC(Ord.CodOrdenCompra, frmLogin.iCodUser, DateTime.Now);
			if (Ord.estadoOrden == 1)
			{
				AdmOrden.setEstadoOrdenCompra(Ord.CodOrdenCompra, 6);
			}
			int cuenta2 = 0;
			RecorreDetalleOrden();
			foreach (clsDetalleOrdenCompra deta in detalle)
			{
				foreach (clsDetalleOrdenCompra det2 in detalle)
				{
					if (deta.CodDetalleOrdenCompra == det2.CodDetalleOrdenCompra && AdmOrden.updatedetalle(det2))
					{
						cuenta2++;
					}
				}
				AdmOrden.deletedetalleorden(detaSelec.CodProducto, detaSelec.CodOrdenCompra);
			}
			foreach (clsDetalleOrdenCompra deta2 in detalle)
			{
				if (deta2.CodDetalleOrdenCompra == 0)
				{
					AdmOrden.insertdetalle(deta2);
				}
			}
			MessageBox.Show("Los datos se actualizaron correctamente", "Orden Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Close();
		}
	}

	public void actualizanuevaestadorden(int CodDetalleOrdenCompra, int estado)
	{
		bool hola = AdmOrden.actualizanuevaestadorden(CodDetalleOrdenCompra, estado);
	}

	public void actualizanuevaestadocabeceraorden(int codOrdenCompra, int estado)
	{
		bool hola = AdmOrden.actualizanuevaestadocabeceraorden(codOrdenCompra, estado);
	}

	public void RecorreDetalleOrden()
	{
		detalle.Clear();
		if (dgvDetalle.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				añadedetalle(row);
			}
		}
		rellenarTotalesFlete();
	}

	public void añadedetalle(DataGridViewRow fila)
	{
		try
		{
			clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
			deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			deta.codOrdenNuevo = Convert.ToInt32(Ord.CodOrdenCompraNuevo);
			deta.CodOrdenCompra = Convert.ToInt32(Ord.CodOrdenCompra);
			if (Proceso == 2 || (Proceso == 3 && deta.CodDetalleOrdenCompra != 0 && deta.CodDetalleOrdenCompra.ToString() != ""))
			{
				deta.CodDetalleOrdenCompra = Convert.ToInt32((!(fila.Cells[coddetalle.Name].Value.ToString() == "")) ? Convert.ToInt32(fila.Cells[coddetalle.Name].Value) : 0);
			}
			else
			{
				deta.CodDetalleOrdenCompra = ((!(fila.Cells[coddetalle.Name].Value.ToString() == "")) ? Convert.ToInt32(fila.Cells[coddetalle.Name].Value) : 0);
			}
			deta.CodAlmacen = frmLogin.iCodAlmacen;
			deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
			deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
			deta.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.cantidadpendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
			deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
			deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
			deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
			deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
			deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
			deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
			deta.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
			deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
			deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
			deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
			deta.FechaIngreso = dtpFecha.Value;
			deta.CodUser = frmLogin.iCodUser;
			if (txtCodProveedor.Text == "")
			{
				txtCodProveedor.Text = "0";
			}
			deta.CodProveedor = Convert.ToInt32(Ord.CodProveedor);
			deta.estadoOrden = Convert.ToInt32(fila.Cells[EstadoDeLaOrden.Name].Value);
			deta.psoli = Convert.ToInt32(fila.Cells[ProductoSolicitado.Name].Value);
			deta.etiqueta = Convert.ToString(fila.Cells[etiqueta.Name].Value);
			deta.uno = uno;
			deta.TipoPrecio = ((!cbValorVenta.Checked) ? 1 : 0);
			detalle.Add(deta);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaFilaDetalle(DataGridViewRow fila)
	{
		try
		{
			detaSelec.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			detaSelec.CodOrdenCompra = Convert.ToInt32(Ord.CodOrdenCompra);
			detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
			detaSelec.Moneda = cmbMoneda.SelectedIndex;
			if (detaSelec.UnidadIngresada != 0)
			{
				detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			}
			detaSelec.SerieLote = fila.Cells[serielote.Name].Value.ToString();
			detaSelec.Cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			detaSelec.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
			detaSelec.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
			detaSelec.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
			detaSelec.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
			detaSelec.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
			detaSelec.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
			detaSelec.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
			detaSelec.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
			detaSelec.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
			detaSelec.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
			detaSelec.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
			detaSelec.FechaIngreso = dtpFecha.Value;
			detaSelec.CodUser = frmLogin.iCodUser;
			detaSelec.cantidadpendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			detaSelec.estadoOrden = Convert.ToInt32(fila.Cells[EstadoDeLaOrden.Name].Value);
			detaSelec.psoli = Convert.ToInt32(fila.Cells[ProductoSolicitado.Name].Value);
			detaSelec.etiqueta = Convert.ToString(fila.Cells[etiqueta.Name].Value);
			if (detaSelec.etiqueta == "P. Atendidos")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P. Atendidos";
			}
			else if (detaSelec.etiqueta == "P.No Atendidos")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P.No Atendidos";
			}
			else if (detaSelec.etiqueta == "P.De Promocion")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P.De Promocion";
			}
			else if (detaSelec.etiqueta == "P.Desc.Al Transportista")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P.Desc.Al Transportista";
			}
			else if (detaSelec.etiqueta == "P.Devolucion Al Proveedor")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P.Devolucion Al Proveedor";
			}
			else if (detaSelec.etiqueta == "P.No solicitado / Aceptado")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P.No solicitado / Aceptado";
			}
			else if (detaSelec.etiqueta == "P.No solicitado / Para Devolucion")
			{
				dgvDetalle.CurrentRow.Cells[29].Value = "P.No solicitado / Para Devolucion";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if ((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0))
		{
			DataGridViewRow row = dgvDetalle.SelectedRows[0];
			if (Application.OpenForms["frmDetalleIngreso"] != null)
			{
				Application.OpenForms["frmDetalleIngreso"].Activate();
				return;
			}
			frmDetalleIngreso form = new frmDetalleIngreso();
			form.Proceso = 2;
			form.Procede = 10;
			form.bvalorventa = cbValorVenta.Checked;
			form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
			form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
			form.txtReferencia.ReadOnly = true;
			form.txtDescripcion.Text = row.Cells[descripcion.Name].Value.ToString();
			form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
			form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
			form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
			form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
			form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
			form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
			form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
			form.txtCantidad.Focus();
			form.formulario_o_c_padre = this;
			form.ShowDialog();
		}
	}

	private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
		dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count != 1)
		{
			return;
		}
		if (dgvDetalle.Rows.Count == 1)
		{
			MessageBox.Show("No se pueden eliminar todos lo elementos de una Orden de Compra", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		DialogResult dr = MessageBox.Show("Desea eliminar este producto permanentemente?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (dr == DialogResult.Yes)
		{
			DialogResult dr2 = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			dr2 = frm.ShowDialog();
			if (dr2 == DialogResult.OK)
			{
				dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
				calculatotales();
			}
		}
	}

	private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
		if (Proceso == 1)
		{
			calculatotales();
		}
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

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
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

	private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa" && Proceso == 1)
		{
			calculatotales();
		}
	}

	private void txtPDescuento_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void recalculadetalle()
	{
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[valorventaconflete.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) + Convert.ToDecimal(row.Cells[flete.Name].Value);
			row.Cells[pvconflete.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) + Convert.ToDecimal(row.Cells[flete.Name].Value);
			if (Convert.ToDecimal(row.Cells[flete.Name].Value) > 0.00m && row.Cells[flete.Name].Value.ToString() != "")
			{
				row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventaconflete.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[pvconflete.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
			else
			{
				row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
				row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			}
		}
	}

	public void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (!base.Visible)
		{
			return;
		}
		if (dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
		{
			CargaFilaDetalle(dgvDetalle.CurrentRow);
		}
		try
		{
			if (e.RowIndex != -1)
			{
				if (dgvDetalle.Rows[e.RowIndex].Cells[etiqueta.Name].ColumnIndex != e.ColumnIndex || dgvDetalle.CurrentCell != null)
				{
				}
				clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();
				if (consultacombo != 100 && consultacombo == 80 && detaSelec.etiqueta == "P.No Atendidos")
				{
					dgvDetalle.Rows[e.RowIndex].Cells[etiqueta.Name].Value = "P.No Atendidos";
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dtpFecha_ValueChanged(object sender, EventArgs e)
	{
		if (txtTipoCambio.Visible)
		{
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			if (tc != null)
			{
				txtTipoCambio.Text = tc.Venta.ToString();
				return;
			}
			MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			dtpFecha.Value = DateTime.Now.Date;
			dtpFecha.Focus();
		}
	}

	private void frmOrdenCompra_Shown(object sender, EventArgs e)
	{
		if (Proceso == 1 && txtTipoCambio.Visible)
		{
			if (tc == null)
			{
				MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				txtTipoCambio.Text = tc.Venta.ToString();
			}
		}
	}

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		try
		{
			string ruta = "C:\\tmp\\OrdenesDeCompra";
			string nombreArchivo = "OC-" + Ord.CodOrdenCompra.ToString().PadLeft(8, '0');
			CROrdenCompraNuevoFormato rpt = new CROrdenCompraNuevoFormato();
			frmRptOrdenCompra frm = new frmRptOrdenCompra();
			rpt.SetDataSource(ds.OrdenCompra(Convert.ToInt32(Ord.CodOrdenCompra.ToString())).Tables[0]);
			Directory.CreateDirectory(ruta);
			rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
			Process p = new Process();
			p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
			p.Start();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void rellenarTotalesFlete()
	{
		double total_f_s_i = 0.0;
		double total_f_c_i = 0.0;
		foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
		{
			double flete_c_i = AdmPro.obtenerFleteDeProducto(Convert.ToDouble(fila.Cells[codproducto.Name].Value), 1, Convert.ToInt32(fila.Cells[codunidad.Name].Value), Convert.ToDouble(fila.Cells[cantidad.Name].Value));
			total_f_c_i += Convert.ToDouble(double.IsNaN(flete_c_i) ? 0.0 : flete_c_i);
		}
		double sinigv = 0.0;
		sinigv = total_f_c_i / 1.18;
		if (cmbtipoflete.SelectedIndex == 0)
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
		if (cmbtipoflete.SelectedIndex == 1)
		{
			txtTotalFleteConIgv.Text = total_f_c_i.ToString("##0.00");
			txtTotalFleteSinIgv.Text = sinigv.ToString("##0.00");
		}
		if (cmbtipoflete.SelectedIndex == 2)
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
	}

	private void btnGenerarGR_Click(object sender, EventArgs e)
	{
		frmGuiaRemisionCompra form = new frmGuiaRemisionCompra();
		int codigoOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
		form.dataGeneradaDeOC = AdmOrden.generarGuiaRemisionOrdenCompra(codigoOC);
		form.codOrdenCompraGenerada = codigoOC;
		form.CodEmpresaTransporte = CodEmpresaTransporte;
		form.codUltimaGuiaRemisionGenerada = AdmOrden.obtenerCodUltimaGuiaRemisionGenerada(codigoOC);
		form.nuevoMetodo = true;
		form.valFactFlete = cmbtipoflete.SelectedIndex;
		form.MdiParent = base.MdiParent;
		form.Generada = true;
		form.codmonedaoc = Convert.ToInt32(cmbMoneda.SelectedValue);
		form.Show();
	}

	private List<int> getEstadosParaGeneracion()
	{
		List<int> estados = new List<int>();
		DataTable dato = admguiaremisioncompra.estadosAlInicioGeneracion(frmLogin.iCodAlmacen);
		if (dato != null)
		{
			foreach (DataRow fila in dato.Rows)
			{
				estados.Add(fila.Field<int>("codigo"));
			}
		}
		return estados;
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
			txtedit.Leave -= dgvDetalle_Leave;
			txtedit.Leave += dgvDetalle_Leave;
		}
		if (e.Control is DataGridViewComboBoxEditingControl dgvCombo)
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
			if (combo.SelectedValue != null && combo.SelectedIndex != -1 && combo.SelectedValue.ToString() != "System.Data.DataRowView" && dgvDetalle.CurrentCell == null)
			{
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)
		{
			ok.SOLONumeros(sender, e);
		}
	}

	private void dgvDetalle_Leave(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		dgvDetalle.CurrentRow.Cells[cantidadpendiente.Name].Value = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
			row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value) + Convert.ToDecimal(row.Cells[igv.Name].Value);
			row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) * Convert.ToDecimal(row.Cells[cantidad.Name].Value);
		}
		calculatotales();
	}

	private void dgvDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		if (e.Exception.Message == "DataGridViewComboBoxCell Value is not valid")
		{
			object value = dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (!((DataGridViewComboBoxColumn)dgvDetalle.Columns[e.ColumnIndex]).Items.Contains(value))
			{
				((DataGridViewComboBoxColumn)dgvDetalle.Columns[e.ColumnIndex]).Items.Add(value);
				e.ThrowException = false;
			}
		}
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void button2_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count > 0)
		{
			dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
		}
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void dgvDetalle_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private Dictionary<string, object> extraendoValores(DataGridViewCellEventArgs e)
	{
		Dictionary<string, object> aux_datos = new Dictionary<string, object>();
		DataGridViewRow fila = dgvDetalle.Rows[e.RowIndex];
		aux_datos["Cantidad"] = fila.Cells[cantidad.Name].Value;
		aux_datos["PrecioUnitario"] = fila.Cells[preciounit.Name].Value;
		aux_datos["dscto1"] = fila.Cells[dscto1.Name].Value;
		aux_datos["dscto2"] = fila.Cells[dscto2.Name].Value;
		aux_datos["dscto3"] = fila.Cells[dscto3.Name].Value;
		aux_datos["PrecioTotal"] = fila.Cells[valorventa.Name].Value;
		return aux_datos;
	}

	private void txtRUCTransporte_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F1)
		{
			return;
		}
		if (Application.OpenForms["frmProveedoresLista"] != null)
		{
			Application.OpenForms["frmProveedoresLista"].Activate();
			return;
		}
		frmProveedoresLista form = new frmProveedoresLista();
		form.Proceso = 3;
		form.Procede = 13;
		form.ventana1 = this;
		if (form.ShowDialog() == DialogResult.OK)
		{
			CodEmpresaTransporte = form.GetCodProveeder();
			CargaEmpresaTransporte();
			ProcessTabKey(forward: true);
		}
	}

	private void CargaEmpresaTransporte()
	{
		empT = AdmET.MuestraProveedor(CodEmpresaTransporte);
		if (empT != null)
		{
			txtRUCTransporte.Text = empT.Ruc;
			txtRazonSocialTransporte.Text = empT.RazonSocial;
			txtDireccionTransporte.Text = empT.Direccion;
		}
		else
		{
			txtRUCTransporte.Text = "";
			txtRazonSocialTransporte.Text = "";
			txtDireccionTransporte.Text = "";
		}
	}

	private bool validarDatosFormulario(bool mostrarSms = true)
	{
		string sms = "";
		bool paso = true;
		if (string.IsNullOrEmpty(txttiutlooc.Text))
		{
			sms = "Ingresar Titulo de Orden Compra";
			txttiutlooc.Focus();
			paso = false;
		}
		if (cmbFormaPago.SelectedValue == null)
		{
			sms = "Seleccion una forma de pago para la orden de compra";
			paso = false;
		}
		else if (cmbtipoflete.SelectedIndex == 2 && (CodEmpresaTransporte == 0 || CodEmpresaTransporte == -1))
		{
			sms = "Asigne una empresa de transporte";
			paso = false;
		}
		if (!paso && mostrarSms)
		{
			MessageBox.Show(sms, "Datos Incompletos de Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return paso;
	}

	private void btnAprobarOrdenCompra_Click(object sender, EventArgs e)
	{
		if (validarDatosFormulario(mostrarSms: false))
		{
			int codOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
			if (Ord.estadoOrden == 6)
			{
				usuario_click = null;
				DialogResult dr = DialogResult.None;
				frmAutorizacion frm = new frmAutorizacion();
				frm.permiso = admForm.getPermisoAprobarOrdenCompra();
				frm.tipoAccion = 2;
				frm.tipoVentanaAAsignarUsuario = 1;
				frm.ventanaOC = this;
				frm.PermitirAdministradores = true;
				dr = frm.ShowDialog();
				if (dr == DialogResult.OK)
				{
					AdmOrden.setEstadoOrdenCompra(codOC, 5);
					AdmOrden.registrarAprobadorDeOC(codOC, usuario_click.CodUsuario, DateTime.Now);
					MessageBox.Show("ORDEN DE COMPRA APROBADA CON EXITO", "Aprobacion de Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					frmOrdenCompra form = new frmOrdenCompra();
					form.MdiParent = base.MdiParent;
					form.CodOrdenCompra = codOC;
					form.Proceso = 3;
					form.Show();
					Close();
				}
			}
			else
			{
				MessageBox.Show("Ocurrio un error al intentar cambiar el estado de la OC-" + codOC, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		else
		{
			MessageBox.Show("Complete Los Datos Necesarios de la Orden De Compra", "Mensaje De Orden De Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void btncerrarordendecompra_Click(object sender, EventArgs e)
	{
		try
		{
			int codOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
			usuario_click = null;
			DialogResult dr = DialogResult.None;
			frmAutorizacion frm = new frmAutorizacion();
			frm.permiso = admForm.getPermisoCerrarOrdenCompra();
			frm.tipoAccion = 2;
			frm.tipoVentanaAAsignarUsuario = 1;
			frm.ventanaOC = this;
			frm.PermitirAdministradores = true;
			dr = frm.ShowDialog();
			if (dr == DialogResult.OK)
			{
				if (AdmOrden.actualizaestadocabeceraorden(codOC, 4))
				{
					AdmOrden.registrarModificacionDeOC(codOC, usuario_click.CodUsuario, DateTime.Now);
					MessageBox.Show("Orden de Compra Cerrada Con Exito", "OREDEN DE COMPRA CERRADA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					btncerrarordendecompra.Visible = false;
					btnGenerarGR.Visible = false;
				}
				else
				{
					MessageBox.Show("Ocurrio un Error en la Actualizacion De EStado de Orden de Compra ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cmbtipoflete_SelectedIndexChanged(object sender, EventArgs e)
	{
		gbTransportista.Visible = cmbtipoflete.SelectedIndex == 2;
		if (cmbtipoflete.SelectedIndex != 2)
		{
			CodEmpresaTransporte = 0;
		}
		if (cmbtipoflete.Text == "Sin Flete")
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
		if (cmbtipoflete.Text == "Con Flete")
		{
			rellenarTotalesFlete();
		}
		if (cmbtipoflete.Text == "Flete Tercerizado")
		{
			txtTotalFleteConIgv.Text = "0";
			txtTotalFleteSinIgv.Text = "0";
		}
	}

	private void btnModificarFlete_Click(object sender, EventArgs e)
	{
		bool band = true;
		foreach (DataGridViewRow fila in (IEnumerable)dgvDetalle.Rows)
		{
			if (fila.Cells[coddetalle.Name].Value == "" || fila.Cells[coddetalle.Name].Value == DBNull.Value)
			{
				band = false;
			}
		}
		if (band)
		{
			frmListadoProductoFlete form = new frmListadoProductoFlete();
			int codigoOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
			if (codigoOC != 0)
			{
				form.data = AdmOrden.obtenerListadoProductoFleteDeGRC(codigoOC);
				form.codordencompra = codigoOC;
				form.ShowDialog();
				rellenarTotalesFlete();
				return;
			}
			frmMostrarMensaje form2 = new frmMostrarMensaje();
			form2.Text = "Advertencia de Guardado";
			form2.colorTexto = Color.White;
			form2.textoColor = "Debe Guardar para abrir la ventana de Fletes";
			form2.lblTextoColor.BackColor = Color.Yellow;
			form2.Height -= form2.lblTextoNegro.Height;
			form2.lblTextoNegro.Height = 0;
			form2.Ok = true;
			form2.ShowDialog();
		}
		else
		{
			frmMostrarMensaje form3 = new frmMostrarMensaje();
			form3.Text = "Advertencia de Guardado";
			form3.colorTexto = Color.White;
			form3.textoColor = "Debe Guardar para abrir la ventana de Fletes";
			form3.lblTextoColor.BackColor = Color.Yellow;
			form3.Height -= form3.lblTextoNegro.Height;
			form3.lblTextoNegro.Height = 0;
			form3.Ok = true;
			form3.ShowDialog();
		}
	}

	private void btnListarGR_Click(object sender, EventArgs e)
	{
		frmListadoGuiaRemisionCompra form = new frmListadoGuiaRemisionCompra();
		form.MdiParent = base.MdiParent;
		int codigoOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
		form.codOCAMostrar = codigoOC;
		form.Show();
	}

	private void btnActualizarLIsta_Click(object sender, EventArgs e)
	{
		frmOrdenCompra_Load(sender, e);
		frmOrdenCompra_Shown(sender, e);
	}

	private void dgvOrdenesGeneradas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			string valor = dgvOrdenesGeneradas.Columns[e.ColumnIndex].Name;
			string uno = colNumGRC.Name;
			string dos = colFacturaFlete.Name;
			string tres = colFacturaCompra.Name;
			if (valor == uno)
			{
				int codigoGRC = Convert.ToInt32(dgvOrdenesGeneradas.Rows[e.RowIndex].Cells[colcodGRC.Name].Value);
				if (codigoGRC != -1)
				{
					frmGuiaRemisionCompra form = buscarFrmGRC("frmGuiaRemisionCompra", codigoGRC);
					if (form != null)
					{
						form.Activate();
						return;
					}
					form = new frmGuiaRemisionCompra();
					form.Dock = DockStyle.Fill;
					form.WindowState = FormWindowState.Maximized;
					form.Editar = true;
					form.MdiParent = base.MdiParent;
					form.codGuiaRemisionCompraAEditar = codigoGRC;
					form.codmonedaoc = Convert.ToInt32(cmbMoneda.SelectedValue);
					form.Show();
				}
			}
			else if (valor == dos)
			{
				int CodFactura = Convert.ToInt32(dgvOrdenesGeneradas.Rows[e.RowIndex].Cells[colCodFactFlete.Name].Value);
				if (CodFactura != -1)
				{
					frmNotaIngreso form2 = new frmNotaIngreso();
					form2.MdiParent = base.MdiParent;
					form2.CodNota = CodFactura.ToString();
					form2.Proceso = 3;
					form2.Show();
				}
			}
			else if (valor == tres)
			{
				int CodFactura2 = Convert.ToInt32(dgvOrdenesGeneradas.Rows[e.RowIndex].Cells[colCodFactCompra.Name].Value);
				if (CodFactura2 != -1)
				{
					frmNotaIngreso form3 = new frmNotaIngreso();
					form3.MdiParent = base.MdiParent;
					form3.CodNota = CodFactura2.ToString();
					form3.Proceso = 3;
					form3.Show();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Doble CLick dgvOrdenesGeneradas", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private frmGuiaRemisionCompra buscarFrmGRC(string tipoFormulario, int codGRC)
	{
		frmGuiaRemisionCompra form = null;
		foreach (Form frm in Application.OpenForms)
		{
			if (frm.Name.ToString() == tipoFormulario && ((frmGuiaRemisionCompra)frm).codGuiaRemisionCompraAEditar == codGRC)
			{
				form = (frmGuiaRemisionCompra)frm;
				break;
			}
		}
		return form;
	}

	private void btnUpdatePrecios_Click(object sender, EventArgs e)
	{
		try
		{
			int codOC = ((Ord.CodOrdenCompra == 0) ? Ord.CodOrdenCompraNuevo : Ord.CodOrdenCompra);
			frmModificacionPreciosOrdenCompra frm = new frmModificacionPreciosOrdenCompra();
			frm.codOrdenCompra = codOC;
			frm.lblDescripcionOrdenCompra.Text = "OC " + txtOrdenCompra.Text;
			frm.WindowState = FormWindowState.Maximized;
			frm.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txttipCambioProv_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
		{
			e.Handled = true;
		}
		if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
		{
			e.Handled = true;
		}
	}

	private void txttipCambioProv_Leave(object sender, EventArgs e)
	{
		if (!(cmbMoneda.Text != "SOLES"))
		{
			return;
		}
		decimal tipcambioProv = Convert.ToDecimal(txttipCambioProv.Text);
		if (cmbMoneda.Text == "SOLES" && tipcambioProv == 0m)
		{
			MessageBox.Show("¡Ingrese el tipo de cambio del proveedor!", "AVISO");
			txttipCambioProv.Focus();
			return;
		}
		Ord.tipCambioProv = Convert.ToDecimal(txttipCambioProv.Text);
		if (AdmOrden.update(Ord))
		{
			AdmOrden.registrarModificacionDeOC(Ord.CodOrdenCompra, frmLogin.iCodUser, DateTime.Now);
		}
	}

	private void cmbMoneda_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (cmbMoneda.Text == "DOLARES AMERICANOS")
		{
			txttipCambioProv.Text = "0.0000";
			label20.Visible = true;
			txttipCambioProv.Visible = true;
			txttipCambioProv.Focus();
		}
		else
		{
			txttipCambioProv.Text = "0.0000";
			label20.Visible = false;
			txttipCambioProv.Visible = false;
		}
	}

	private void txttipCambioProv_Enter(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmOrdenCompra));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnListarGR = new System.Windows.Forms.Button();
		this.btncerrarordendecompra = new System.Windows.Forms.Button();
		this.btnAprobarOrdenCompra = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.btnGenerarGR = new System.Windows.Forms.Button();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.txtPrecioVenta = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtIGV = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.txtDscto = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.moneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidadpendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventaconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.flete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.pvconflete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fechaingreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.EstadoDeLaOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ProductoSolicitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.etiqueta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtCodProv = new System.Windows.Forms.TextBox();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.txtestadoOrden = new System.Windows.Forms.TextBox();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.label1 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.txtNombreProv = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtOrdenCompra = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.txtTipoCambio = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.cbValorVenta = new System.Windows.Forms.CheckBox();
		this.txtCodProveedor = new System.Windows.Forms.TextBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.txttipCambioProv = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.btnUpdatePrecios = new System.Windows.Forms.Button();
		this.lblUsuarioAnulador = new System.Windows.Forms.Label();
		this.lblanuladopor = new System.Windows.Forms.Label();
		this.gbcontienedgvordenesgeneradas = new System.Windows.Forms.GroupBox();
		this.dgvOrdenesGeneradas = new System.Windows.Forms.DataGridView();
		this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colNumGRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colcodGRC = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodFactFlete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFacturaFlete = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodFactCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colFacturaCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnActualizarLIsta = new System.Windows.Forms.Button();
		this.btnModificarFlete = new System.Windows.Forms.Button();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label18 = new System.Windows.Forms.Label();
		this.cmbtipoflete = new System.Windows.Forms.ComboBox();
		this.txtTotalFleteConIgv = new System.Windows.Forms.TextBox();
		this.txtTotalFleteSinIgv = new System.Windows.Forms.TextBox();
		this.label19 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.gbTransportista = new System.Windows.Forms.GroupBox();
		this.txtDireccionTransporte = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtRazonSocialTransporte = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtRUCTransporte = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txttiutlooc = new System.Windows.Forms.TextBox();
		this.lbltitulo = new System.Windows.Forms.Label();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox1.SuspendLayout();
		this.gbcontienedgvordenesgeneradas.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenesGeneradas).BeginInit();
		this.groupBox4.SuspendLayout();
		this.gbTransportista.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnListarGR);
		this.groupBox3.Controls.Add(this.btncerrarordendecompra);
		this.groupBox3.Controls.Add(this.btnAprobarOrdenCompra);
		this.groupBox3.Controls.Add(this.button2);
		this.groupBox3.Controls.Add(this.btnGenerarGR);
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 475);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(1347, 48);
		this.groupBox3.TabIndex = 19;
		this.groupBox3.TabStop = false;
		this.btnListarGR.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnListarGR.Location = new System.Drawing.Point(353, 8);
		this.btnListarGR.Name = "btnListarGR";
		this.btnListarGR.Size = new System.Drawing.Size(77, 37);
		this.btnListarGR.TabIndex = 11;
		this.btnListarGR.Text = "Lista G.Remision";
		this.btnListarGR.UseVisualStyleBackColor = true;
		this.btnListarGR.Click += new System.EventHandler(btnListarGR_Click);
		this.btncerrarordendecompra.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btncerrarordendecompra.Location = new System.Drawing.Point(857, 8);
		this.btncerrarordendecompra.Name = "btncerrarordendecompra";
		this.btncerrarordendecompra.Size = new System.Drawing.Size(77, 37);
		this.btncerrarordendecompra.TabIndex = 10;
		this.btncerrarordendecompra.Text = "Cerrar Orden de Compra";
		this.btncerrarordendecompra.UseVisualStyleBackColor = true;
		this.btncerrarordendecompra.Visible = false;
		this.btncerrarordendecompra.Click += new System.EventHandler(btncerrarordendecompra_Click);
		this.btnAprobarOrdenCompra.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAprobarOrdenCompra.Location = new System.Drawing.Point(940, 8);
		this.btnAprobarOrdenCompra.Name = "btnAprobarOrdenCompra";
		this.btnAprobarOrdenCompra.Size = new System.Drawing.Size(77, 37);
		this.btnAprobarOrdenCompra.TabIndex = 9;
		this.btnAprobarOrdenCompra.Text = "Aprobar O. de Compra";
		this.btnAprobarOrdenCompra.UseVisualStyleBackColor = true;
		this.btnAprobarOrdenCompra.Visible = false;
		this.btnAprobarOrdenCompra.Click += new System.EventHandler(btnAprobarOrdenCompra_Click);
		this.button2.ImageIndex = 2;
		this.button2.ImageList = this.imageList1;
		this.button2.Location = new System.Drawing.Point(174, 10);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(113, 32);
		this.button2.TabIndex = 8;
		this.button2.Text = "Elim.Ord.Nueva";
		this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button2.UseVisualStyleBackColor = true;
		this.button2.Visible = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.btnGenerarGR.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGenerarGR.Location = new System.Drawing.Point(1023, 8);
		this.btnGenerarGR.Name = "btnGenerarGR";
		this.btnGenerarGR.Size = new System.Drawing.Size(77, 37);
		this.btnGenerarGR.TabIndex = 7;
		this.btnGenerarGR.Text = "Generar G.Remision";
		this.btnGenerarGR.UseVisualStyleBackColor = true;
		this.btnGenerarGR.Visible = false;
		this.btnGenerarGR.Click += new System.EventHandler(btnGenerarGR_Click);
		this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(1106, 10);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 4;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(1273, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 3;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(97, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 6;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Visible = false;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(1190, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 2;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(150, 101);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 0;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(16, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 1;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.txtBruto);
		this.groupBox2.Controls.Add(this.txtPrecioVenta);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.txtIGV);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.txtDscto);
		this.groupBox2.Controls.Add(this.txtValorVenta);
		this.groupBox2.Controls.Add(this.label12);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.label10);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 263);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1347, 212);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(633, 134);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(78, 20);
		this.txtBruto.TabIndex = 23;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtPrecioVenta.Location = new System.Drawing.Point(1230, 186);
		this.txtPrecioVenta.Name = "txtPrecioVenta";
		this.txtPrecioVenta.ReadOnly = true;
		this.txtPrecioVenta.Size = new System.Drawing.Size(111, 20);
		this.txtPrecioVenta.TabIndex = 22;
		this.txtPrecioVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioVenta.TextChanged += new System.EventHandler(txtPrecioVenta_TextChanged);
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(1137, 189);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(54, 13);
		this.label14.TabIndex = 21;
		this.label14.Text = "P. Venta :";
		this.txtIGV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtIGV.Location = new System.Drawing.Point(1230, 160);
		this.txtIGV.Name = "txtIGV";
		this.txtIGV.ReadOnly = true;
		this.txtIGV.Size = new System.Drawing.Size(111, 20);
		this.txtIGV.TabIndex = 20;
		this.txtIGV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label13.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(1137, 163);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 19;
		this.label13.Text = "I.G.V. :";
		this.txtDscto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtDscto.Location = new System.Drawing.Point(805, 134);
		this.txtDscto.Name = "txtDscto";
		this.txtDscto.ReadOnly = true;
		this.txtDscto.Size = new System.Drawing.Size(75, 20);
		this.txtDscto.TabIndex = 18;
		this.txtDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(1230, 134);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 17;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(1137, 137);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(54, 13);
		this.label12.TabIndex = 16;
		this.label12.Text = "V. Venta :";
		this.label11.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(734, 137);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(65, 13);
		this.label11.TabIndex = 14;
		this.label11.Text = "Descuento :";
		this.label10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label10.AutoSize = true;
		this.label10.Location = new System.Drawing.Point(589, 137);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(38, 13);
		this.label10.TabIndex = 12;
		this.label10.Text = "Bruto :";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvDetalle.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.moneda, this.codunidad, this.unidad, this.serielote, this.cantidad, this.cantidadpendiente, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.valorventaconflete, this.igv, this.flete, this.precioventa, this.pvconflete, this.precioreal, this.valoreal, this.fechaingreso, this.coduser, this.fecharegistro, this.EstadoDeLaOrden, this.ProductoSolicitado, this.etiqueta);
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.MultiSelect = false;
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(1341, 112);
		this.dgvDetalle.TabIndex = 0;
		this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellClick);
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.dgvDetalle.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentDoubleClick);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellValueChanged);
		this.dgvDetalle.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetalle_DataError);
		this.dgvDetalle.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalle_EditingControlShowing);
		this.dgvDetalle.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dgvDetalle_RowsAdded);
		this.dgvDetalle.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalle_RowsRemoved);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalle_KeyPress);
		this.dgvDetalle.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalle_KeyUp);
		this.dgvDetalle.Leave += new System.EventHandler(dgvDetalle_Leave);
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
		this.referencia.FillWeight = 609.137f;
		this.referencia.HeaderText = "Referencia";
		this.referencia.MinimumWidth = 100;
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.descripcion.DataPropertyName = "producto";
		dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.descripcion.DefaultCellStyle = dataGridViewCellStyle33;
		this.descripcion.FillWeight = 30f;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.MinimumWidth = 275;
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.moneda.DataPropertyName = "moneda";
		this.moneda.HeaderText = "Moneda";
		this.moneda.Name = "moneda";
		this.moneda.ReadOnly = true;
		this.moneda.Visible = false;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.FillWeight = 27.68778f;
		this.unidad.HeaderText = "Unidad";
		this.unidad.MinimumWidth = 120;
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.DataPropertyName = "serielote";
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.ReadOnly = true;
		this.serielote.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.serielote.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.serielote.Visible = false;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle34.Format = "N2";
		dataGridViewCellStyle34.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle34;
		this.cantidad.FillWeight = 27.68778f;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.MinimumWidth = 60;
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidadpendiente.DataPropertyName = "cantidadpendiente";
		this.cantidadpendiente.FillWeight = 27.68778f;
		this.cantidadpendiente.HeaderText = "Pendiente";
		this.cantidadpendiente.MinimumWidth = 60;
		this.cantidadpendiente.Name = "cantidadpendiente";
		this.cantidadpendiente.ReadOnly = true;
		this.preciounit.DataPropertyName = "preciounitario";
		dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle35.Format = "N4";
		dataGridViewCellStyle35.NullValue = null;
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle35;
		this.preciounit.FillWeight = 27.68778f;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.MinimumWidth = 60;
		this.preciounit.Name = "preciounit";
		this.preciounit.ReadOnly = true;
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.importe.DataPropertyName = "subtotal";
		dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle36.Format = "N4";
		dataGridViewCellStyle36.NullValue = null;
		this.importe.DefaultCellStyle = dataGridViewCellStyle36;
		this.importe.FillWeight = 27.68778f;
		this.importe.HeaderText = "Importe";
		this.importe.MinimumWidth = 60;
		this.importe.Name = "importe";
		this.importe.ReadOnly = true;
		this.importe.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.importe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.DataPropertyName = "descuento1";
		dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle37.Format = "N4";
		this.dscto1.DefaultCellStyle = dataGridViewCellStyle37;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.ReadOnly = true;
		this.dscto1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto1.Visible = false;
		this.dscto2.DataPropertyName = "descuento2";
		dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle38.Format = "N4";
		this.dscto2.DefaultCellStyle = dataGridViewCellStyle38;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.ReadOnly = true;
		this.dscto2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto2.Visible = false;
		this.dscto3.DataPropertyName = "descuento3";
		dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle39.Format = "N4";
		this.dscto3.DefaultCellStyle = dataGridViewCellStyle39;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.ReadOnly = true;
		this.dscto3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dscto3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dscto3.Visible = false;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle40.Format = "N4";
		dataGridViewCellStyle40.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle40;
		this.montodscto.FillWeight = 27.68778f;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.MinimumWidth = 60;
		this.montodscto.Name = "montodscto";
		this.montodscto.ReadOnly = true;
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle41.Format = "N4";
		dataGridViewCellStyle41.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle41;
		this.valorventa.FillWeight = 27.68778f;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.MinimumWidth = 60;
		this.valorventa.Name = "valorventa";
		this.valorventa.ReadOnly = true;
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventaconflete.DataPropertyName = "vvconflete";
		this.valorventaconflete.HeaderText = "vvconflete";
		this.valorventaconflete.Name = "valorventaconflete";
		this.valorventaconflete.ReadOnly = true;
		this.valorventaconflete.Visible = false;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle42.Format = "N4";
		this.igv.DefaultCellStyle = dataGridViewCellStyle42;
		this.igv.FillWeight = 27.68778f;
		this.igv.HeaderText = "IGV";
		this.igv.MinimumWidth = 60;
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.flete.DataPropertyName = "flete";
		dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.flete.DefaultCellStyle = dataGridViewCellStyle43;
		this.flete.HeaderText = "Flete";
		this.flete.Name = "flete";
		this.flete.ReadOnly = true;
		this.flete.Visible = false;
		this.precioventa.DataPropertyName = "importe";
		dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle44.Format = "N4";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle44;
		this.precioventa.FillWeight = 27.68778f;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.MinimumWidth = 70;
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.pvconflete.DataPropertyName = "pvconflete";
		this.pvconflete.HeaderText = "pvconflete";
		this.pvconflete.Name = "pvconflete";
		this.pvconflete.ReadOnly = true;
		this.pvconflete.Visible = false;
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
		this.fechaingreso.DataPropertyName = "fechaingreso";
		this.fechaingreso.HeaderText = "FechaIngre";
		this.fechaingreso.Name = "fechaingreso";
		this.fechaingreso.ReadOnly = true;
		this.fechaingreso.Visible = false;
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
		this.EstadoDeLaOrden.DataPropertyName = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.HeaderText = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.Name = "EstadoDeLaOrden";
		this.EstadoDeLaOrden.ReadOnly = true;
		this.EstadoDeLaOrden.Visible = false;
		this.ProductoSolicitado.DataPropertyName = "ProductoSolicitado";
		this.ProductoSolicitado.HeaderText = "ProductosSolicitados";
		this.ProductoSolicitado.Name = "ProductoSolicitado";
		this.ProductoSolicitado.ReadOnly = true;
		this.ProductoSolicitado.Visible = false;
		this.etiqueta.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.etiqueta.DataPropertyName = "etiqueta";
		dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.etiqueta.DefaultCellStyle = dataGridViewCellStyle45;
		this.etiqueta.FillWeight = 30f;
		this.etiqueta.HeaderText = "Estado";
		this.etiqueta.MinimumWidth = 250;
		this.etiqueta.Name = "etiqueta";
		this.etiqueta.ReadOnly = true;
		this.etiqueta.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.txtCodProv.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
		this.txtCodProv.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.highlighter1.SetHighlightOnFocus(this.txtCodProv, true);
		this.txtCodProv.Location = new System.Drawing.Point(102, 44);
		this.txtCodProv.Name = "txtCodProv";
		this.txtCodProv.ReadOnly = true;
		this.txtCodProv.Size = new System.Drawing.Size(128, 20);
		this.txtCodProv.TabIndex = 0;
		this.txtCodProv.Tag = "8";
		this.txtCodProv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.superValidator1.SetValidator3(this.txtCodProv, this.customValidator2);
		this.txtCodProv.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodProv_KeyDown);
		this.txtCodProv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodProv_KeyPress);
		this.txtCodProv.Leave += new System.EventHandler(txtCodProv_Leave);
		this.cmbFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.cmbFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
		this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbFormaPago.FormattingEnabled = true;
		this.highlighter1.SetHighlightOnFocus(this.cmbFormaPago, true);
		this.cmbFormaPago.Location = new System.Drawing.Point(102, 121);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(128, 20);
		this.cmbFormaPago.TabIndex = 1;
		this.cmbFormaPago.Tag = "16";
		this.superValidator1.SetValidator3(this.cmbFormaPago, this.customValidator7);
		this.cmbFormaPago.SelectionChangeCommitted += new System.EventHandler(cmbFormaPago_SelectionChangeCommitted);
		this.dtpFecha.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.highlighter1.SetHighlightOnFocus(this.dtpFecha, true);
		this.dtpFecha.Location = new System.Drawing.Point(1254, 17);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 3;
		this.dtpFecha.ValueChanged += new System.EventHandler(dtpFecha_ValueChanged);
		this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dtpFecha_KeyPress);
		this.txtComentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.highlighter1.SetHighlightOnFocus(this.txtComentario, true);
		this.txtComentario.Location = new System.Drawing.Point(102, 70);
		this.txtComentario.MaxLength = 120;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(474, 20);
		this.txtComentario.TabIndex = 2;
		this.txtComentario.Tag = "21";
		this.txtComentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Enabled = false;
		this.highlighter1.SetHighlightOnFocus(this.btnDetalle, true);
		this.btnDetalle.Location = new System.Drawing.Point(1254, 121);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(81, 23);
		this.btnDetalle.TabIndex = 6;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.FormattingEnabled = true;
		this.highlighter1.SetHighlightOnFocus(this.cmbMoneda, true);
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(1254, 43);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 4;
		this.cmbMoneda.SelectedIndexChanged += new System.EventHandler(cmbMoneda_SelectedIndexChanged);
		this.cmbMoneda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbMoneda_KeyPress);
		this.txtestadoOrden.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtestadoOrden.Enabled = false;
		this.highlighter1.SetHighlightOnFocus(this.txtestadoOrden, true);
		this.txtestadoOrden.Location = new System.Drawing.Point(102, 96);
		this.txtestadoOrden.MaxLength = 120;
		this.txtestadoOrden.Name = "txtestadoOrden";
		this.txtestadoOrden.Size = new System.Drawing.Size(211, 20);
		this.txtestadoOrden.TabIndex = 46;
		this.txtestadoOrden.Tag = "21";
		this.customValidator2.ErrorMessage = "Escoja un proveedor.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator7.ErrorMessage = "Escoja la forma de pago.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator3.ErrorMessage = "Escoja un cliente.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator6.ErrorMessage = "Complete el campo requerido.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator5.ErrorMessage = "Ingrese el numero de documento.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator4.ErrorMessage = "Escoja un documento.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator1.ErrorMessage = "Escoja la Transaccion.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(1205, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(43, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Fecha :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(31, 46);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(62, 13);
		this.label4.TabIndex = 5;
		this.label4.Tag = "8";
		this.label4.Text = "Proveedor :";
		this.txtNombreProv.Enabled = false;
		this.txtNombreProv.Location = new System.Drawing.Point(232, 44);
		this.txtNombreProv.Name = "txtNombreProv";
		this.txtNombreProv.ReadOnly = true;
		this.txtNombreProv.Size = new System.Drawing.Size(344, 20);
		this.txtNombreProv.TabIndex = 8;
		this.txtNombreProv.Tag = "9";
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(12, 20);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(81, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Orden Compra :";
		this.txtOrdenCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtOrdenCompra.Location = new System.Drawing.Point(102, 16);
		this.txtOrdenCompra.MaxLength = 100;
		this.txtOrdenCompra.Name = "txtOrdenCompra";
		this.txtOrdenCompra.ReadOnly = true;
		this.txtOrdenCompra.Size = new System.Drawing.Size(153, 24);
		this.txtOrdenCompra.TabIndex = 2;
		this.txtOrdenCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(53, 73);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(40, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Glosa :";
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(1196, 46);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(52, 13);
		this.label15.TabIndex = 20;
		this.label15.Text = "Moneda :";
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(1174, 73);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(74, 13);
		this.label16.TabIndex = 21;
		this.label16.Tag = "15";
		this.label16.Text = "Tipo/Cambio :";
		this.txtTipoCambio.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtTipoCambio.Enabled = false;
		this.txtTipoCambio.Location = new System.Drawing.Point(1254, 70);
		this.txtTipoCambio.Name = "txtTipoCambio";
		this.txtTipoCambio.ReadOnly = true;
		this.txtTipoCambio.Size = new System.Drawing.Size(81, 20);
		this.txtTipoCambio.TabIndex = 6;
		this.txtTipoCambio.Tag = "15";
		this.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(8, 123);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(85, 13);
		this.label17.TabIndex = 41;
		this.label17.Tag = "16";
		this.label17.Text = "Forma de Pago :";
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(232, 120);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 15;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.cbValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cbValorVenta.AutoSize = true;
		this.cbValorVenta.Checked = true;
		this.cbValorVenta.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cbValorVenta.Location = new System.Drawing.Point(1254, 98);
		this.cbValorVenta.Name = "cbValorVenta";
		this.cbValorVenta.Size = new System.Drawing.Size(81, 17);
		this.cbValorVenta.TabIndex = 5;
		this.cbValorVenta.Text = "Valor Venta";
		this.cbValorVenta.UseVisualStyleBackColor = true;
		this.cbValorVenta.Visible = false;
		this.txtCodProveedor.Location = new System.Drawing.Point(582, 43);
		this.txtCodProveedor.Name = "txtCodProveedor";
		this.txtCodProveedor.Size = new System.Drawing.Size(67, 20);
		this.txtCodProveedor.TabIndex = 45;
		this.txtCodProveedor.Visible = false;
		this.groupBox1.Controls.Add(this.txttiutlooc);
		this.groupBox1.Controls.Add(this.lbltitulo);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.txttipCambioProv);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.btnUpdatePrecios);
		this.groupBox1.Controls.Add(this.lblUsuarioAnulador);
		this.groupBox1.Controls.Add(this.lblanuladopor);
		this.groupBox1.Controls.Add(this.gbcontienedgvordenesgeneradas);
		this.groupBox1.Controls.Add(this.btnActualizarLIsta);
		this.groupBox1.Controls.Add(this.btnModificarFlete);
		this.groupBox1.Controls.Add(this.groupBox4);
		this.groupBox1.Controls.Add(this.gbTransportista);
		this.groupBox1.Controls.Add(this.txtestadoOrden);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtCodProveedor);
		this.groupBox1.Controls.Add(this.cbValorVenta);
		this.groupBox1.Controls.Add(this.dtpFechaPago);
		this.groupBox1.Controls.Add(this.cmbFormaPago);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtTipoCambio);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label15);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.txtComentario);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtOrdenCompra);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtNombreProv);
		this.groupBox1.Controls.Add(this.txtCodProv);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.dtpFecha);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1347, 263);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(655, 50);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(41, 13);
		this.label22.TabIndex = 83;
		this.label22.Text = "label22";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(261, 20);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(41, 13);
		this.label21.TabIndex = 82;
		this.label21.Text = "label21";
		this.txttipCambioProv.Location = new System.Drawing.Point(459, 116);
		this.txttipCambioProv.Name = "txttipCambioProv";
		this.txttipCambioProv.Size = new System.Drawing.Size(117, 20);
		this.txttipCambioProv.TabIndex = 81;
		this.txttipCambioProv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txttipCambioProv.Enter += new System.EventHandler(txttipCambioProv_Enter);
		this.txttipCambioProv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txttipCambioProv_KeyPress);
		this.txttipCambioProv.Leave += new System.EventHandler(txttipCambioProv_Leave);
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(358, 119);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(94, 13);
		this.label20.TabIndex = 80;
		this.label20.Text = "Tipo Cambio(Prov)";
		this.btnUpdatePrecios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnUpdatePrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnUpdatePrecios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnUpdatePrecios.Image = SIGEFA.Properties.Resources.ganancia;
		this.btnUpdatePrecios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnUpdatePrecios.Location = new System.Drawing.Point(1041, 153);
		this.btnUpdatePrecios.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnUpdatePrecios.Name = "btnUpdatePrecios";
		this.btnUpdatePrecios.Size = new System.Drawing.Size(151, 46);
		this.btnUpdatePrecios.TabIndex = 79;
		this.btnUpdatePrecios.Text = "Modificar Precios";
		this.btnUpdatePrecios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnUpdatePrecios.UseVisualStyleBackColor = true;
		this.btnUpdatePrecios.Click += new System.EventHandler(btnUpdatePrecios_Click);
		this.lblUsuarioAnulador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblUsuarioAnulador.ForeColor = System.Drawing.Color.Red;
		this.lblUsuarioAnulador.Location = new System.Drawing.Point(743, 47);
		this.lblUsuarioAnulador.Name = "lblUsuarioAnulador";
		this.lblUsuarioAnulador.Size = new System.Drawing.Size(165, 23);
		this.lblUsuarioAnulador.TabIndex = 78;
		this.lblUsuarioAnulador.Text = "label20";
		this.lblUsuarioAnulador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblUsuarioAnulador.Visible = false;
		this.lblanuladopor.AutoSize = true;
		this.lblanuladopor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblanuladopor.ForeColor = System.Drawing.Color.Red;
		this.lblanuladopor.Location = new System.Drawing.Point(741, 20);
		this.lblanuladopor.Name = "lblanuladopor";
		this.lblanuladopor.Size = new System.Drawing.Size(167, 25);
		this.lblanuladopor.TabIndex = 77;
		this.lblanuladopor.Text = "ANULADO POR";
		this.lblanuladopor.Visible = false;
		this.gbcontienedgvordenesgeneradas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.gbcontienedgvordenesgeneradas.Controls.Add(this.dgvOrdenesGeneradas);
		this.gbcontienedgvordenesgeneradas.Location = new System.Drawing.Point(731, 131);
		this.gbcontienedgvordenesgeneradas.Name = "gbcontienedgvordenesgeneradas";
		this.gbcontienedgvordenesgeneradas.Size = new System.Drawing.Size(303, 126);
		this.gbcontienedgvordenesgeneradas.TabIndex = 76;
		this.gbcontienedgvordenesgeneradas.TabStop = false;
		this.gbcontienedgvordenesgeneradas.Text = "Guias de Remision Generadas";
		this.gbcontienedgvordenesgeneradas.Visible = false;
		this.dgvOrdenesGeneradas.AllowUserToAddRows = false;
		this.dgvOrdenesGeneradas.AllowUserToDeleteRows = false;
		this.dgvOrdenesGeneradas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvOrdenesGeneradas.BackgroundColor = System.Drawing.Color.FromArgb(207, 221, 238);
		this.dgvOrdenesGeneradas.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.dgvOrdenesGeneradas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle46.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
		dataGridViewCellStyle46.Font = new System.Drawing.Font("Times New Roman", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle46.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle46.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle46.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle46.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvOrdenesGeneradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle46;
		this.dgvOrdenesGeneradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvOrdenesGeneradas.Columns.AddRange(this.colItem, this.colNumGRC, this.colcodGRC, this.colCodFactFlete, this.colFacturaFlete, this.colCodFactCompra, this.colFacturaCompra);
		this.dgvOrdenesGeneradas.Cursor = System.Windows.Forms.Cursors.Arrow;
		dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle47.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle47.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvOrdenesGeneradas.DefaultCellStyle = dataGridViewCellStyle47;
		this.dgvOrdenesGeneradas.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvOrdenesGeneradas.EnableHeadersVisualStyles = false;
		this.dgvOrdenesGeneradas.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
		this.dgvOrdenesGeneradas.Location = new System.Drawing.Point(3, 16);
		this.dgvOrdenesGeneradas.Name = "dgvOrdenesGeneradas";
		this.dgvOrdenesGeneradas.ReadOnly = true;
		this.dgvOrdenesGeneradas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle48.BackColor = System.Drawing.SystemColors.ActiveCaption;
		dataGridViewCellStyle48.Font = new System.Drawing.Font("Times New Roman", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle48.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle48.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle48.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle48.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvOrdenesGeneradas.RowHeadersDefaultCellStyle = dataGridViewCellStyle48;
		this.dgvOrdenesGeneradas.RowHeadersVisible = false;
		this.dgvOrdenesGeneradas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dgvOrdenesGeneradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvOrdenesGeneradas.Size = new System.Drawing.Size(297, 107);
		this.dgvOrdenesGeneradas.TabIndex = 63;
		this.dgvOrdenesGeneradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvOrdenesGeneradas_CellDoubleClick);
		this.colItem.DataPropertyName = "indItem";
		this.colItem.FillWeight = 50.76142f;
		this.colItem.HeaderText = "Item";
		this.colItem.Name = "colItem";
		this.colItem.ReadOnly = true;
		this.colNumGRC.DataPropertyName = "numGRC";
		this.colNumGRC.FillWeight = 116.4129f;
		this.colNumGRC.HeaderText = "Nro. GRC";
		this.colNumGRC.Name = "colNumGRC";
		this.colNumGRC.ReadOnly = true;
		this.colcodGRC.DataPropertyName = "codGRC";
		this.colcodGRC.HeaderText = "colCodGRC";
		this.colcodGRC.Name = "colcodGRC";
		this.colcodGRC.ReadOnly = true;
		this.colcodGRC.Visible = false;
		this.colCodFactFlete.DataPropertyName = "codfacturaFlete";
		this.colCodFactFlete.HeaderText = "codFactFlete";
		this.colCodFactFlete.Name = "colCodFactFlete";
		this.colCodFactFlete.ReadOnly = true;
		this.colCodFactFlete.Visible = false;
		this.colFacturaFlete.DataPropertyName = "facturaFlete";
		this.colFacturaFlete.FillWeight = 116.4129f;
		this.colFacturaFlete.HeaderText = "Factura Flete";
		this.colFacturaFlete.Name = "colFacturaFlete";
		this.colFacturaFlete.ReadOnly = true;
		this.colCodFactCompra.DataPropertyName = "codfacturaCompra";
		this.colCodFactCompra.HeaderText = "codFactCompra";
		this.colCodFactCompra.Name = "colCodFactCompra";
		this.colCodFactCompra.ReadOnly = true;
		this.colCodFactCompra.Visible = false;
		this.colFacturaCompra.DataPropertyName = "facturaCompra";
		this.colFacturaCompra.FillWeight = 116.4129f;
		this.colFacturaCompra.HeaderText = "Factura Compra";
		this.colFacturaCompra.Name = "colFacturaCompra";
		this.colFacturaCompra.ReadOnly = true;
		this.btnActualizarLIsta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnActualizarLIsta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnActualizarLIsta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnActualizarLIsta.Image = SIGEFA.Properties.Resources.cambio;
		this.btnActualizarLIsta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnActualizarLIsta.Location = new System.Drawing.Point(1199, 204);
		this.btnActualizarLIsta.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnActualizarLIsta.Name = "btnActualizarLIsta";
		this.btnActualizarLIsta.Size = new System.Drawing.Size(136, 43);
		this.btnActualizarLIsta.TabIndex = 75;
		this.btnActualizarLIsta.Text = "Actualizar Lista";
		this.btnActualizarLIsta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnActualizarLIsta.UseVisualStyleBackColor = true;
		this.btnActualizarLIsta.Click += new System.EventHandler(btnActualizarLIsta_Click);
		this.btnModificarFlete.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnModificarFlete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnModificarFlete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnModificarFlete.Image = SIGEFA.Properties.Resources.ganancia;
		this.btnModificarFlete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnModificarFlete.Location = new System.Drawing.Point(1200, 153);
		this.btnModificarFlete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
		this.btnModificarFlete.Name = "btnModificarFlete";
		this.btnModificarFlete.Size = new System.Drawing.Size(135, 46);
		this.btnModificarFlete.TabIndex = 74;
		this.btnModificarFlete.Text = "Modificar Flete";
		this.btnModificarFlete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnModificarFlete.UseVisualStyleBackColor = true;
		this.btnModificarFlete.Click += new System.EventHandler(btnModificarFlete_Click);
		this.groupBox4.Controls.Add(this.label18);
		this.groupBox4.Controls.Add(this.cmbtipoflete);
		this.groupBox4.Controls.Add(this.txtTotalFleteConIgv);
		this.groupBox4.Controls.Add(this.txtTotalFleteSinIgv);
		this.groupBox4.Controls.Add(this.label19);
		this.groupBox4.Controls.Add(this.label8);
		this.groupBox4.Location = new System.Drawing.Point(11, 147);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(258, 100);
		this.groupBox4.TabIndex = 70;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Opciones de FLETE";
		this.label18.AutoSize = true;
		this.label18.Location = new System.Drawing.Point(7, 23);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(43, 13);
		this.label18.TabIndex = 80;
		this.label18.Text = "FLETE:";
		this.cmbtipoflete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbtipoflete.FormattingEnabled = true;
		this.cmbtipoflete.Items.AddRange(new object[3] { "Sin Flete", "Con Flete", "Flete Tercerizado" });
		this.cmbtipoflete.Location = new System.Drawing.Point(56, 20);
		this.cmbtipoflete.Name = "cmbtipoflete";
		this.cmbtipoflete.Size = new System.Drawing.Size(188, 21);
		this.cmbtipoflete.TabIndex = 79;
		this.cmbtipoflete.SelectedIndexChanged += new System.EventHandler(cmbtipoflete_SelectedIndexChanged);
		this.txtTotalFleteConIgv.Enabled = false;
		this.txtTotalFleteConIgv.Location = new System.Drawing.Point(127, 70);
		this.txtTotalFleteConIgv.Name = "txtTotalFleteConIgv";
		this.txtTotalFleteConIgv.Size = new System.Drawing.Size(117, 20);
		this.txtTotalFleteConIgv.TabIndex = 4;
		this.txtTotalFleteConIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtTotalFleteSinIgv.Enabled = false;
		this.txtTotalFleteSinIgv.Location = new System.Drawing.Point(127, 44);
		this.txtTotalFleteSinIgv.Name = "txtTotalFleteSinIgv";
		this.txtTotalFleteSinIgv.Size = new System.Drawing.Size(117, 20);
		this.txtTotalFleteSinIgv.TabIndex = 3;
		this.txtTotalFleteSinIgv.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label19.AutoSize = true;
		this.label19.Location = new System.Drawing.Point(7, 74);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(106, 13);
		this.label19.TabIndex = 2;
		this.label19.Text = "Total Flete (Con IGV)";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(7, 47);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(102, 13);
		this.label8.TabIndex = 1;
		this.label8.Text = "Total Flete (Sin IGV)";
		this.gbTransportista.Controls.Add(this.txtDireccionTransporte);
		this.gbTransportista.Controls.Add(this.label3);
		this.gbTransportista.Controls.Add(this.txtRazonSocialTransporte);
		this.gbTransportista.Controls.Add(this.label5);
		this.gbTransportista.Controls.Add(this.label6);
		this.gbTransportista.Controls.Add(this.txtRUCTransporte);
		this.gbTransportista.Location = new System.Drawing.Point(275, 147);
		this.gbTransportista.Name = "gbTransportista";
		this.gbTransportista.Size = new System.Drawing.Size(445, 100);
		this.gbTransportista.TabIndex = 48;
		this.gbTransportista.TabStop = false;
		this.gbTransportista.Text = "Empresa de Tranportes";
		this.txtDireccionTransporte.Location = new System.Drawing.Point(84, 71);
		this.txtDireccionTransporte.Name = "txtDireccionTransporte";
		this.txtDireccionTransporte.ReadOnly = true;
		this.txtDireccionTransporte.Size = new System.Drawing.Size(343, 20);
		this.txtDireccionTransporte.TabIndex = 20;
		this.txtDireccionTransporte.Tag = "5";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(17, 74);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(52, 13);
		this.label3.TabIndex = 16;
		this.label3.Text = "Dirección";
		this.txtRazonSocialTransporte.Location = new System.Drawing.Point(84, 45);
		this.txtRazonSocialTransporte.Name = "txtRazonSocialTransporte";
		this.txtRazonSocialTransporte.ReadOnly = true;
		this.txtRazonSocialTransporte.Size = new System.Drawing.Size(343, 20);
		this.txtRazonSocialTransporte.TabIndex = 19;
		this.txtRazonSocialTransporte.Tag = "5";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(17, 22);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(30, 13);
		this.label5.TabIndex = 14;
		this.label5.Text = "RUC";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(17, 48);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(61, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Raz. Social";
		this.txtRUCTransporte.BackColor = System.Drawing.SystemColors.Info;
		this.txtRUCTransporte.Location = new System.Drawing.Point(84, 19);
		this.txtRUCTransporte.Name = "txtRUCTransporte";
		this.txtRUCTransporte.Size = new System.Drawing.Size(147, 20);
		this.txtRUCTransporte.TabIndex = 18;
		this.txtRUCTransporte.Tag = "5";
		this.txtRUCTransporte.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRUCTransporte_KeyDown);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(8, 99);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(89, 13);
		this.label2.TabIndex = 47;
		this.label2.Tag = "21";
		this.label2.Text = "Estado De Orden";
		this.txttiutlooc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txttiutlooc.Location = new System.Drawing.Point(397, 16);
		this.txttiutlooc.MaxLength = 100;
		this.txttiutlooc.Name = "txttiutlooc";
		this.txttiutlooc.Size = new System.Drawing.Size(153, 24);
		this.txttiutlooc.TabIndex = 84;
		this.txttiutlooc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lbltitulo.AutoSize = true;
		this.lbltitulo.Location = new System.Drawing.Point(333, 20);
		this.lbltitulo.Name = "lbltitulo";
		this.lbltitulo.Size = new System.Drawing.Size(58, 13);
		this.lbltitulo.TabIndex = 85;
		this.lbltitulo.Text = "TItulo OC :";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(1347, 523);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmOrdenCompra";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Orden de Compra";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmOrdenCompra_Load);
		base.Shown += new System.EventHandler(frmOrdenCompra_Shown);
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.gbcontienedgvordenesgeneradas.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvOrdenesGeneradas).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.gbTransportista.ResumeLayout(false);
		this.gbTransportista.PerformLayout();
		base.ResumeLayout(false);
	}
}
